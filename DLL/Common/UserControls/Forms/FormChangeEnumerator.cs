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
 *  File name:  FormChangeEnumerator.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/21/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  10/06/10    1.1     K.McD           1.  Included support to interface with a class inherited from the IPollTarget interface.
 * 
 *  03/28/11    1.2     K.McD           1.  Modified to use the properties of the inherited watch variable reference.
 *  
 *  08/24/11    1.3     K.McD           1.  Modified to accommodate the changes to the signature of the IPollTarget.SetPauseAndWait() method.
 *  
 *  08/24/11    1.4     K.McD           1.  Modified the Apply button event handler. Added a check on the success of the call to the IPollTarget.SetPauseAndWait() method 
 *                                          before attempting to write the new value to the target.
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Common.Configuration;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// Form used to change the value of a write-enabled enumerator watch variable.
    /// </summary>
    public partial class FormChangeEnumerator : FormChangeWatch
    {
        #region --- Member Variables ---
        /// <summary>
        /// The new value for the enumerator watch variable that is to be downloaded to the VCU.
        /// </summary>
        protected double m_NewEnumeratorValue;

        /// <summary>
        /// The current value of the enumerator watch variable.
        /// </summary>
        protected double m_CurrentEnumeratorValue;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Zero parameter constructor. Required for Visual Studio.
        /// </summary>
        public FormChangeEnumerator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="enumeratorControl">The <c>WatchControl</c> derived user control that called this form.</param>
        public FormChangeEnumerator(WatchEnumeratorControl enumeratorControl) : base(enumeratorControl)
        {
            InitializeComponent();
            Debug.Assert(m_WatchVariable != null, "FormChangeEnumerator.Ctor() - [m_WatchVariable != null]");
            Debug.Assert(m_WatchVariable.VariableType == VariableType.Enumerator, "FormChangeEnumerator.Ctor() - [m_WatchVariable.VariableType == VariableType.Enumerator]");

            // ------------------------
            // Initialize the ComboBox.
            // ------------------------
            // Get the list of enumerator/bitmask (EnumBit) description/value pairs associated with the enumerator watch identifier.
            List<IEnumBit> enumBitList = Lookup.WatchVariableTableByOldIdentifier.EnumBitLists[m_WatchVariable.EnumBitIdentifier];
            for (int enumBitListIndex = 0; enumBitListIndex < enumBitList.Count; enumBitListIndex++)
            {
                // Add the description/value pairs to the combo box control.
                m_ComboBoxNewValue.Items.Add(enumBitList[enumBitListIndex]);
            }
            // Initialize the Value property.
            m_ComboBoxNewValue.Text = m_WatchControl.ValueFieldText;

            #region - [Allowable Range] -
            // Check that the ComboBox control contains at least 2 entries.
            if (m_ComboBoxNewValue.Items.Count > 1)
            {
                m_LabelAllowableRangeLowerValue.Text = m_ComboBoxNewValue.Items[0].ToString();
                m_LabelAllowableRangeUpperValue.Text = m_ComboBoxNewValue.Items[m_ComboBoxNewValue.Items.Count - 1].ToString();
            }
            #endregion - [Allowable Range] -

            // Now that the display has been initialized, disable the apply button. This will only be re-enabled when the user has specified a new watch value.
            m_ButtonApply.Enabled = false;
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
        /// <summary>
        /// Event handler for the Apply button <c>Click</c> event. If a new value has been specified, apply the change.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ButtonApply_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_ButtonApply.Enabled = false;
            if (m_IPollTarget.SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                if (MainWindow != null)
                {
                    MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                }
                m_ButtonApply.Enabled = true;
                return;
            }

            try
            {
                UploadValue(m_ICommunicationInterface.CommunicationInterface, m_OldIdentifier, (double)((IEnumBit)m_ComboBoxNewValue.SelectedItem).Value);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MBTWriteFail, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_ButtonApply.Enabled = true;
            }

            m_IPollTarget.Pause = false;
        }

        /// <summary>
        /// Event handler for the <c>SelectedValueChanged</c> event associated with the <c>ComboBox</c> control used to enter the new enumerated text. Enable the Apply 
        /// button.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_ButtonApply.Enabled = true;
        }
        #endregion --- Delegated Methods ---
    }
}