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
 *  File name:  FormChangeBitmask.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/21/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  09/30/10    1.1     K.McD           1.  Bug fix associated with the Apply button being re-enabled if the user enters data directly and then selects the Apply button. 
 *                                          De- register the 'ValueChanged' event handler whilst the 'CheckBox' properties are being set up in the method 
 *                                          UpdateCheckBoxChecked().
 * 
 *                                      2.  Included the hex characters 'A-F' in the event handler for the 'KeyPress' event associated with the numeric up/down control.
 * 
 *                                      3.  Bug fix associated with the OK button not updating the value. Cleared the Pause property of the client form in the OK button 
 *                                          event handler.
 * 
 *  10/06/10    1.2     K.McD           1.  Included support to interface with a class inherited from the IPollTarget interface.
 * 
 *  10/15/10    1.3     K.McD           1.  Modified to use the generic version of PTUDialog<>.
 * 
 *  03/28/11    1.4     K.McD           1.  Modified to use the old identifier of the watch variable associated with the control that called this form.
 *  
 *  08/24/11    1.5     K.McD           1.  Modified to accommodate the changes to the signature of the IPollTarget.SetPauseAndWait() method.
 *  
 *  08/24/11    1.6     K.McD           1.  Modified the Apply button event handler. Added a check on the success of the call to the IPollTarget.SetPauseAndWait() method 
 *                                          before attempting to write the new value to the target.
 *
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;

using Common;
using Common.Communication;
using Common.Forms;
using Common.Configuration;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// Form used to change the value of a write-enabled bitmask watch variable.
    /// </summary>
    public partial class FormChangeBitmask : FormPTUDialog
    {
        #region --- Constants ---
        /// <summary>
        /// The format string used to display a hexadecimal value e.g. 0F0A. Value: "X".
        /// </summary>
        protected const string FormatStringHex = "X";

        /// <summary>
        /// The .NET format string used to display a decimal value. Value: "###,###,##0".
        /// </summary>
        protected const string FormatStringDecimal = "###,###,##0";

        /// <summary>
        /// The string that is to appear before a value displayed in hexadecimal format e.g.0x0A. Value: "0x".
        /// </summary>
        protected const string FormatStringHexHeader = "0x";

        /// <summary>
        /// The sleep period,in ms, after asserting the Pause property before applying the change. Value: 250 ms.
        /// </summary>
        private const int SleepApplyChange = 250;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The watch variable old identifier associated with the bitmask user control that called this form.
        /// </summary>
        protected short m_OldIdentifier;

        /// <summary>
        /// Reference to the watch variable associated with the user control that called this form
        /// </summary>
        protected WatchVariable m_WatchVariable;

        /// <summary>
        /// Reference to the class that implements the ICheckBoxUInt32 interface.
        /// </summary>
        protected ICheckBoxUInt32 m_ICheckBoxUInt32;

        /// <summary>
        /// Reference to the <c>WatchControl</c> derived user control that showed this form.
        /// </summary>
        private readonly WatchControl m_WatchControl;

        /// <summary>
        /// Reference to the class that implements the <c>ICommunicationInterface</c> interface.
        /// </summary>
        private readonly ICommunicationInterface<ICommunicationWatch> m_ICommunicationInterface;

        /// <summary>
        /// Reference to the class that implements the <c>IDataUpdate</c> interface.
        /// </summary>
        private readonly IDataUpdate m_IDataUpdate;

        /// <summary>
        /// Reference to the class that implements the <c>IPollTarget</c> interface.
        /// </summary>
        private readonly IPollTarget m_IPollTarget;

        /// <summary>
        /// A flag to inhibit the Apply button from being enabled in the ValueChanged event handler associated with the NumericUpDown control. This is used to stop the 
        /// Apply button being re-enabled if the user modifies the value manually and then presses the Apply button.
        /// </summary>
        private bool m_InhibitApplyEnabled;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Zero parameter constructor. Required for Visual Studio.
        /// </summary>
        public FormChangeBitmask()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="bitmaskControl">The <c>WatchControl</c> derived user control that called this form.</param>
        public FormChangeBitmask(WatchBitmaskControl bitmaskControl)
        {
            InitializeComponent();

            m_WatchControl = bitmaskControl;

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
            catch(Exception)
            {
                throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
            }

            Debug.Assert(m_WatchVariable.VariableType == VariableType.Bitmask, "FormChangeBitmask.Ctor() - [m_WatchVariable.VariableType == VariableType.Bitmask]");

            Text = m_WatchVariable.Name;

            #region - [Units] -
            string units = m_WatchVariable.Units;
            m_LabelCurrentValueUnits.Text = units;
            m_LabelNewValueUnits.Text = units;
            #endregion - [Units] -

            #region - [NumericUpDown] -
            m_NumericUpDownNewValue.Hexadecimal = m_RadioButtonHex.Checked;
            m_NumericUpDownNewValue.DecimalPlaces = 0;
            m_NumericUpDownNewValue.Increment = 1;

            m_NumericUpDownNewValue.Maximum = (decimal)m_WatchVariable.MaxModifyValue;
            m_NumericUpDownNewValue.Minimum = (decimal)m_WatchVariable.MinModifyValue;
            
            // Initialize the NumericUpDown control Value property.
            try
            {
                m_NumericUpDownNewValue.Value = (decimal)m_WatchControl.Value;
            }
            catch (Exception)
            {
                // The specified initial value is outside of the limits, set to the minimum value.
                m_NumericUpDownNewValue.Value = m_NumericUpDownNewValue.Minimum;
            }
            #endregion - [NumericUpDown] -

            #region - [ICheckBoxUInt32] -
            m_ICheckBoxUInt32 = new CheckBoxUInt32();
            CheckBox[] checkBoxes;
            ConfigureCheckBoxes(out checkBoxes);
            m_ICheckBoxUInt32.CheckBoxes = checkBoxes;
            m_ICheckBoxUInt32.SetText(m_OldIdentifier);
            m_ICheckBoxUInt32.SetChecked((uint)m_WatchControl.Value);
            #endregion - [ICheckBoxUInt32] -

            // Update the display by calling the DataUpdate event handler.
            DataUpdate(this, new EventArgs());

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

                    if (m_IDataUpdate != null)
                    {
                        // De-register the event handler for the data update event.
                        m_IDataUpdate.DataUpdate -= new EventHandler(DataUpdate);
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                m_WatchVariable = null;
                m_ICheckBoxUInt32 = null;

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
        /// Event handler associated with the <c>DataUpdate</c> event. Update the form using the latest watch value. The format of the display is determined by 
        /// the state of the <c>Checked</c> property of the hex radio button.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void DataUpdate(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            uint currentBitmaskValueUINT = (uint)m_WatchControl.Value;

            if (m_RadioButtonHex.Checked == true)
            {
                string valueText;
                valueText = FormatStringHexHeader + currentBitmaskValueUINT.ToString(FormatStringHex);

                m_LabelCurrentValue.Text = valueText;
            }
            else
            {
                m_LabelCurrentValue.Text = currentBitmaskValueUINT.ToString(FormatStringDecimal);
            }
        }

        #region - [Buttons] -
        /// <summary>
        /// Event handler for the Apply button <c>Click</c> event. Apply the changes.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ButtonApply_Click(object sender, EventArgs e)
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
                FormChangeWatch.UploadValue(m_ICommunicationInterface.CommunicationInterface, m_OldIdentifier, (double)m_NumericUpDownNewValue.Value);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MBTWriteFail, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_ButtonApply.Enabled = true;
            }

            m_IPollTarget.Pause = false;
        }

        /// <summary>
        /// Event handler for the OK button <c>Click</c> event. If a new value has been specified but has not been applied, apply the change and then close the form; 
        /// otherwise just close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonOK_Click(object sender, EventArgs e)
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
        private void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Close();
        }
        #endregion - [Buttons] -

        /// <summary>
        /// Event handler for the <c>CheckedChanged</c> event associated with the radio button used to select the decimal format display. Set the <c>Hexadecimal</c> 
        /// property of the <c>NumericUpDown</c> control according to the <c>Checked</c> property of the hex radio button and then update the display by calling the 
        /// <c>DataUpdate()</c> event handler.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_RadioButtonDecimal_CheckedChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_NumericUpDownNewValue.Hexadecimal = m_RadioButtonHex.Checked;
            DataUpdate(this, new EventArgs());
        }

        /// <summary>
        /// Event handler for the <c>ValueChanged</c> event associated with the <c>NumericUpDown</c> control used to enter the new watch value. Update the <c>Checked</c> 
        /// <c>BackColor</c> and <c>ForeColor</c> properties of each <c>CheckBox</c> control associated with the member variable <see cref="m_ICheckBoxUInt32"/> to reflect the 
        /// <c>Value</c> field of the <c>NumericUpDown</c> control and then enable the Apply button.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the ICheckBoxUInt32 interface has not yet been instantiated.
            if (m_ICheckBoxUInt32 == null)
            {
                return;
            }

            // De-register the event handler so that this event handler is not called every time a Checked property is changed.
            this.m_NumericUpDownNewValue.ValueChanged -= new System.EventHandler(this.m_NumericUpDown_ValueChanged);

            m_ICheckBoxUInt32.SetChecked((uint)m_NumericUpDownNewValue.Value);

            // Re-register the event handler.
            this.m_NumericUpDownNewValue.ValueChanged += new System.EventHandler(this.m_NumericUpDown_ValueChanged);

            if (m_InhibitApplyEnabled == true)
            {
                // Single shot only.
                m_InhibitApplyEnabled = false;
            }
            else
            {
                m_ButtonApply.Enabled = true;
            }
        }
        
        /// <summary>
        /// Event handler for the <c>KeyPress</c>event associated with the <c>NumericUpDown</c> control used to enter the new watch value. Enable the Apply button if the 
        /// user enters a numeric character.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_NumericUpDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            switch (e.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    m_ButtonApply.Enabled = true;
                    m_InhibitApplyEnabled = true;
                    break;

                case 'A':
                case 'a':
                case 'B':
                case 'b':
                case 'C':
                case 'c':
                case 'D':
                case 'd':
                case 'E':
                case 'e':
                case 'F':
                case 'f':
                    if (m_NumericUpDownNewValue.Hexadecimal == true)
                    {
                        m_ButtonApply.Enabled = true;
                        m_InhibitApplyEnabled = true;
                    }
                    break;

                // Backspace.
                case '\b':
                    m_ButtonApply.Enabled = true;
                    m_InhibitApplyEnabled = true;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Event handler for each <c>CheckBox</c> control associated with the member variable <see cref="m_ICheckBoxUInt32"/>.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        public virtual void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the ICheckBoxUInt32 interface has not yet been instantiated.
            if (m_ICheckBoxUInt32 == null)
            {
                return;
            }

            m_ICheckBoxUInt32.SetColors();
            try
            {
                m_NumericUpDownNewValue.Value = (decimal)m_ICheckBoxUInt32.ToValue();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show(Resources.MBTOutsideOfRange, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Configure the <c>CheckBox</c> array.
        /// </summary>
        /// <param name="checkBoxes">Reference to the 32 element array of <c>CheckBox</c> controls where each <c>CheckBox</c> control represents an individual 
        /// bit of a 32 bit bitmask watch variable.</param>
        protected void ConfigureCheckBoxes(out CheckBox[] checkBoxes)
        {
            checkBoxes = new CheckBox[32]   {  m_CheckBox00, m_CheckBox01, m_CheckBox02, m_CheckBox03, m_CheckBox04, m_CheckBox05, m_CheckBox06, m_CheckBox07, 
                                               m_CheckBox08, m_CheckBox09, m_CheckBox10, m_CheckBox11, m_CheckBox12, m_CheckBox13, m_CheckBox14, m_CheckBox15, 
                                               m_CheckBox16, m_CheckBox17, m_CheckBox18, m_CheckBox19, m_CheckBox20, m_CheckBox21, m_CheckBox22, m_CheckBox23, 
                                               m_CheckBox24, m_CheckBox25, m_CheckBox26, m_CheckBox27, m_CheckBox28, m_CheckBox29, m_CheckBox30, m_CheckBox31,
                                            };
        }
        #endregion --- Methods ---
    }
}