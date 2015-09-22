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
 *  File name:  FormChangeScalar.cs
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
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Common.Configuration;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// Form used to change the value of a write-enabled scalar watch variable.
    /// </summary>
    public partial class FormChangeScalar : FormChangeWatch
    {
        #region --- Constants ---
        /// <summary>
        /// The .NET format string used to display the upper and lower bounds.
        /// </summary>
        private const string FormatStringNumericString = "###,###,##0.####";

        /// <summary>
        /// Identifier used to identify a value as a hexadecimal value.
        /// </summary>
        private const string HexValueIdentifier = "0x";

        /// <summary>
        /// The format string, converted to lower case, used in the data dictionary to represent a general number. Value: "general number";
        /// </summary>
        private const string FormatStringGeneralNumber = "general number";

        /// <summary>
        /// The format string, converted to lower case, used in the data dictionary to represent a hexadecimal number. Value: "hexadecimal";
        /// </summary>
        private const string FormatStringHexadecimal = "hexadecimal";
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The current engineering value of the scalar watch variable.
        /// </summary>
        protected double m_CurrentEngineeringValue;

        /// <summary>
        /// The number of decimal places to be used when displaying the data.
        /// </summary>
        protected int m_DecimalPlaces;

        /// <summary>
        /// Flag to indicate whether the scalar is to be specified and displayed in hexadecimal format. True, indicated hexadecimal format; otherwise, false indicates 
        /// general format.
        /// </summary>
        protected bool m_HexFormat;

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
        public FormChangeScalar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="scalarControl">The <c>WatchControl</c> derived user control that called this form.</param>
        public FormChangeScalar(WatchScalarControl scalarControl) : base(scalarControl)
        {
            InitializeComponent();
            Debug.Assert(m_WatchVariable != null, "FormChangeScalar.Ctor() - [m_WatchVariable != null]");
            Debug.Assert(m_WatchVariable.VariableType == VariableType.Scalar, "FormChangeScalar.Ctor() - [m_WatchVariable.VariableType == VariableType.Scalar]");

            if (m_WatchVariable.FormatString.ToLower() == FormatStringHexadecimal)
            {
                m_HexFormat = true;
            }

            #region - [NumericUpDown] -
            if (m_HexFormat == true)
            {
                m_NumericUpDownNewValue.Hexadecimal = true;

                m_NumericUpDownNewValue.DecimalPlaces = 0;
                m_NumericUpDownNewValue.Increment = 1;
            }
            else
            {
                m_NumericUpDownNewValue.Hexadecimal = false;

                // Evaluate the DecimalPlaces and Increment properties associated with the NumericUpDown control based upon the watch variable scale-factor.
                double scaleFactor = m_WatchVariable.ScaleFactor;
                int decimalPlaces;
                decimal increment;
                General.GetDecimalPlaces(scaleFactor, out decimalPlaces);
                General.GetIncrement(scaleFactor, out increment);

                m_NumericUpDownNewValue.DecimalPlaces = decimalPlaces;
                m_NumericUpDownNewValue.Increment = increment;
            }

            m_NumericUpDownNewValue.Maximum = (decimal)m_WatchVariable.MaxModifyValue;
            m_NumericUpDownNewValue.Minimum = (decimal)m_WatchVariable.MinModifyValue;

            // Initialize the NumericUpDown control Value property.
            m_DecimalPlaces = m_NumericUpDownNewValue.DecimalPlaces;
            m_CurrentEngineeringValue = m_WatchControl.Value * m_WatchVariable.ScaleFactor;
            m_CurrentEngineeringValue = Math.Round(m_CurrentEngineeringValue, m_DecimalPlaces);
            try
            {
                m_NumericUpDownNewValue.Value = (decimal)m_CurrentEngineeringValue;
            }
            catch (Exception)
            {
                // The specified initial value is outside of the limits, set to the minimum value.
                m_NumericUpDownNewValue.Value = m_NumericUpDownNewValue.Minimum;
            }
            #endregion - [NumericUpDown] -

            #region - [Allowable Range] -
            if (m_HexFormat == true)
            {
                m_LabelAllowableRangeLowerValue.Text = HexValueIdentifier + ((long)m_WatchVariable.MinModifyValue).ToString("X");
                m_LabelAllowableRangeUpperValue.Text = HexValueIdentifier + ((long)m_WatchVariable.MaxModifyValue).ToString("X");
            }
            else
            {
                m_LabelAllowableRangeLowerValue.Text = m_WatchVariable.MinModifyValue.ToString(FormatStringNumericString);
                m_LabelAllowableRangeUpperValue.Text = m_WatchVariable.MaxModifyValue.ToString(FormatStringNumericString);
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
                UploadValue(m_ICommunicationInterface.CommunicationInterface, m_OldIdentifier, (double)m_NumericUpDownNewValue.Value);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MBTWriteFail, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_ButtonApply.Enabled = true;
            }

            m_IPollTarget.Pause = false;
        }

        /// <summary>
        /// Event handler for the <c>ValueChanged</c> event associated with the <c>NumericUpDown</c> control used to enter the new watch value. Enable the Apply button.
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
        private void m_NumericUpDownNewValue_KeyPress(object sender, KeyPressEventArgs e)
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
        #endregion --- Delegated Methods ---
    }
}