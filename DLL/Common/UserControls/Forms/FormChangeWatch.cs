#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Common
 * 
 *  File name:  FormChangeWatch.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/21/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  10/04/10    1.1     K.McD           1. Include support to interface with a class inherited from the IPollTarget interface.
 * 
 *  10/15/10    1.2     K.McD           1.  Modified to use the generic version of PTUDialog<>.
 * 
 *  03/28/11    1.3     K.McD           1.  Modified to use the old identifier of the watch variable associated with the control that called this form.
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

using Common;
using Common.Communication;
using Common.Configuration;
using Common.Forms;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// Form used to change the value of a write-enabled scalar watch variable.
    /// </summary>
    public partial class FormChangeWatch : FormPTUDialog
    {
        #region --- Constants
        /// <summary>
        /// The sleep period,in ms, after asserting the Pause property before applying the change.
        /// </summary>
        protected const int SleepApplyChange = 250;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Reference to the <c>WatchControl</c> derived user control that showed this form.
        /// </summary>
        protected readonly WatchControl m_WatchControl;

        /// <summary>
        /// Reference to the watch variable associated with the user control that called this form
        /// </summary>
        protected readonly WatchVariable m_WatchVariable;

        /// <summary>
        /// The old identifier of the watch variable associated with the user control that called this form.
        /// </summary>
        protected readonly short m_OldIdentifier;

        /// <summary>
        /// The name of the watch variable that appears in the form title.
        /// </summary>
        protected readonly string m_WatchVariableName;

        /// <summary>
        /// Reference to the class that implements the <c>ICommunicationInterface</c> interface.
        /// </summary>
        protected readonly ICommunicationInterface<ICommunicationWatch> m_ICommunicationInterface;

        /// <summary>
        /// Reference to the class that implements the <c>IDataUpdate</c> interface.
        /// </summary>
        protected readonly IDataUpdate m_IDataUpdate;

        /// <summary>
        /// Reference to the class that implements the <c>IPollTarget</c> interface.
        /// </summary>
        protected readonly IPollTarget m_IPollTarget;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Zero parameter constructor. Required for Visual Studio.
        /// </summary>
        public FormChangeWatch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="watchControl">The watch control derived user control that called this form.</param>
        /// <exception cref="Exception">Thrown if the old Identifier associated with the control that called this form is not defined in the current data dictionary.</exception>
        public FormChangeWatch(WatchControl watchControl)
        {
            InitializeComponent();

            m_WatchControl = watchControl;

            // Use the communication interface associated with the client form.
            m_ICommunicationInterface = m_WatchControl.ClientForm as ICommunicationInterface<ICommunicationWatch>;
            Debug.Assert(m_ICommunicationInterface != null);

            // Register the event handler for the data update event.
            m_IDataUpdate = m_WatchControl.ClientForm as IDataUpdate;
            if (m_IDataUpdate != null)
            {
                m_IDataUpdate.DataUpdate += new EventHandler(DataUpdate);
            }

            m_IPollTarget = m_WatchControl.ClientForm as IPollTarget;
            Debug.Assert(m_IPollTarget != null);

            m_OldIdentifier = (short)m_WatchControl.Identifier;
            try
            {
                m_WatchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[m_OldIdentifier];
                if (m_WatchVariable == null)
                {
                    throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
                }
            }
            catch (Exception)
            {
                throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
            }

            m_WatchVariableName = m_WatchControl.VariableNameFieldText;
            Text = m_WatchVariableName;

            #region - [Units] -
            string units = m_WatchVariable.Units;
            m_LabelCurrentValueUnits.Text = units;
            m_LabelAllowableRangeUnits.Text = units;
            m_LabelNewValueUnits.Text = units;
            #endregion - [Units] -

            // Update the display by calling the DataUpdate event handler.
            DataUpdate(this, new EventArgs());
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Cleanup(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    if (m_IDataUpdate != null)
                    {
                        // De-register the event handler for the data update event.
                        m_IDataUpdate.DataUpdate -= new EventHandler(DataUpdate);
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.

                #region --- Windows Form Designer Variables ---
                // Detach the event handler delegates.

                // Set the Windows Form Designer Variables to null.

                #endregion --- Windows Form Designer Variables ---
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
            }
            finally
            {
                base.Cleanup(disposing);
            }
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        #region - [Buttons] -
        /// <summary>
        /// Event handler for the OK button <c>Click</c> event. If a new value has been specified but has not been applied, apply the change and then close the form; 
        /// otherwise just close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ButtonOK_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Check whether a change is still to be applied and, if so, apply the change.
            if (m_ButtonApply.Enabled)
            {
                m_ButtonApply_Click(this, new EventArgs());
            }

            Close();
        }

        /// <summary>
        /// Event handler for the Cancel button <c>Click</c> event. Close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Close();
        }

        /// <summary>
        /// Event handler for the Apply button <c>Click</c> event. Logic is defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ButtonApply_Click(object sender, EventArgs e)
        {
        }
        #endregion - [Buttons] -

        /// <summary>
        /// Event handler associated with the <c>DataUpdate</c> event. Update the form with the latest watch value.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void DataUpdate(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_LabelCurrentValue.Text = m_WatchControl.ValueFieldText;
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Upload the specified value to the specified watch variable.
        /// </summary>
        /// <param name="communicationInterface">Reference to the selected communication interface.</param>
        /// <param name="oldIdentifier">The old identifier associated with the watch variable.</param>
        /// <param name="value">The value that is to be uploaded to the VCU.</param>
        /// <exception cref="ArgumentNullException">Thrown if the communication interface is not specified.</exception>
        public static void UploadValue(ICommunicationWatch communicationInterface, short oldIdentifier, double value)
        {
            if (communicationInterface == null)
            {
                throw new ArgumentNullException("communicationInterface");
            }

            WatchVariable watchVariable;

            try
            {
                watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                if (watchVariable == null)
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }

            VariableType variableType = watchVariable.VariableType;

            // The actual value that is to be uploaded to the VCU.
            double uploadValue;

            switch (variableType)
            {
                case VariableType.Scalar:
                    uploadValue = value / watchVariable.ScaleFactor;
                    break;
                default:
                    uploadValue = value;
                    break;
            }

            // Note: the DataType must be of type u32 for the write operation to work correctly.
            communicationInterface.SendVariable(watchVariable.Identifier, (short)DataType_e.u32, uploadValue);
        }
        #endregion --- Methods ---
    }
}