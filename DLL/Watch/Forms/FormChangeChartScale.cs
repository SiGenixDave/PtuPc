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
 *  Project:    Watch
 * 
 *  File name:  FormChangeChartScale.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/18/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/30/11    1.1     K.McD           1.  Moved the formatting constants to the CommonConstants class.
 *                                      2.  Modified a number of XML tags and renamed a number of member and local variables.
 *                                      3.  Replaced the form title with the name of the selected watch variable.
 *                                      4.  Modified to take into account the fact that the ListBox controls associated with the upper and lower Y axis limits on 
 *                                          the form that calls this class now store the limits as text rather than double values.
 *                                      5.  Included support for hexadecimal upper and lower limits.
 *                                      
 *  09/30/11    1.2     K.McD           1.  Renamed the variable associated with the reference to the class that implements the IChartScale interface. 
 *
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

using Common;
using Common.Configuration;
using Common.Forms;
using Watch.Properties;

namespace Watch.Forms
{
    /// <summary>
    /// Form to allow the user to modify the upper and lower limits of the Y axis associated with an individual chart recorder channel.
    /// </summary>
    public partial class FormChangeChartScale : FormPTUDialog
    {
        #region --- Member Variables ---
        /// <summary>
        /// The selected watch variable.
        /// </summary>
        WatchVariable m_WatchVariable;

        /// <summary>
        /// Reference to the class that implements the IChartScale interface.
        /// </summary>
        IChartScale m_IChartScale;

        /// <summary>
        /// Reference to the calling form as a <c>FormConfigureChartRecorder</c> class. Used to enable the 'Save' ToolStripButton if the class was called from the form 
        /// used to configure the chart recorder.
        /// </summary>
        FormConfigureChartRecorder m_CalledFromAsFormConfigureChartRecorder;

        /// <summary>
        /// The selected index of the the <c>ListBox</c> control containing the list of old identifiers.
        /// </summary>
        int m_SelectedIndex;

        /// <summary>
        /// Flag to indicate whether the scalar is to be specified and displayed in hexadecimal format. True, indicated hexadecimal format; otherwise, false indicates 
        /// general format.
        /// </summary>
        private bool m_HexFormat;

        /// <summary>
        /// The current lower limit of the Y axis associated with the chart recorder channel.
        /// </summary>
        private double m_ChartScaleLowerLimit;

        /// <summary>
        /// The current upper limit of the Y axis associated with the chart recorder channel.
        /// </summary>
        private double m_ChartScaleUpperLimit;
        #endregion --- Member Variables ---

        #region --- Constructors ---
         /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormChangeChartScale()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>.
        /// <param name="listBox">The ListBox control containing the chart recorder channel scaling information.</param>
        public FormChangeChartScale(ListBox listBox)
        {
            InitializeComponent();
            Debug.Assert(listBox != null, "FormChangeChartScale.Ctor() - ");

            int oldIdentifier = ((WatchItem_t)listBox.SelectedItem).OldIdentifier;
            m_SelectedIndex = listBox.SelectedIndex;

            try
            {
                m_WatchVariable = Lookup.WatchVariableTableByOldIdentifier.RecordList[oldIdentifier];
                if (m_WatchVariable == null)
                {
                    throw new ArgumentException();
                }

                Text = m_WatchVariable.Name;
            }
            catch (Exception)
            {
                throw new ArgumentException(Resources.MBTWatchVariableNotFound);
            }

            // Display the engineering units of the watch variable associated with the current channel.
            m_LabelUnitsMin.Text = m_WatchVariable.Units;
            m_LabelUnitsMax.Text = m_WatchVariable.Units;

            // Set the display format.
            if (m_WatchVariable.FormatString.ToLower().Equals(CommonConstants.DDFormatStringHex))
            {
                m_HexFormat = true;
            }

            // Display the default upper and lower chart scale limits of the watch variable associated with the current channel.
            if (m_HexFormat == true)
            {
                m_LabelDefaultChartScaleMin.Text = CommonConstants.HexValueIdentifier + ((uint)m_WatchVariable.MinChartScale).ToString(CommonConstants.FormatStringHex);
                m_LabelDefaultChartScaleMax.Text = CommonConstants.HexValueIdentifier + ((uint)m_WatchVariable.MaxChartScale).ToString(CommonConstants.FormatStringHex);
            }
            else
            {
                m_LabelDefaultChartScaleMin.Text = m_WatchVariable.MinChartScale.ToString(CommonConstants.FormatStringNumeric);
                m_LabelDefaultChartScaleMax.Text = m_WatchVariable.MaxChartScale.ToString(CommonConstants.FormatStringNumeric);
            }

            m_LabelDefaultUnits.Text = m_WatchVariable.Units;
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
                m_WatchVariable = null;
                m_IChartScale = null;
                m_CalledFromAsFormConfigureChartRecorder = null;

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
        #region - [Form] -
        /// <summary>
        /// Event handler for the form <c>Shown</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormChangeChartScale_Shown(object sender, EventArgs e)
        {
            m_IChartScale = CalledFrom as IChartScale;
            Debug.Assert(m_IChartScale != null, "FormChargeChartScale.FormChangeChartScale_Shown() - [m_CalledFromAsFormWorksetDefineChartRecorder != null]");

            string chartScaleLowerLimitText = (string)m_IChartScale.ListBoxChartScaleLowerLimit.Items[m_SelectedIndex];
            string chartScaleUpperLimitText = (string)m_IChartScale.ListBoxChartScaleUpperLimit.Items[m_SelectedIndex];

            CultureInfo provider = new CultureInfo(CommonConstants.CultureInfoString);
            bool successfulParse;
            if (m_HexFormat == true)
            {
                uint lowerLimitAsUInt32;
                uint upperLimitAsUInt32;
                string strippedChartScaleLowerLimitText, strippedChartScaleUpperLimitText;

                // Strip out the leading HexValueIdentifier.
                Debug.Assert(chartScaleLowerLimitText.Contains(CommonConstants.HexValueIdentifier), "FormChangeChartScale.FormChangeChartScale_Shown() - [chartScaleLowerLimitText.Contains(HexValueIdentifier)]");
                Debug.Assert(chartScaleUpperLimitText.Contains(CommonConstants.HexValueIdentifier), "FormChangeChartScale.FormChangeChartScale_Shown() - [chartScaleUpperLimitText.Contains(HexValueIdentifier)]");

                strippedChartScaleLowerLimitText = chartScaleLowerLimitText.Remove(0, CommonConstants.HexValueIdentifier.Length);
                strippedChartScaleUpperLimitText = chartScaleUpperLimitText.Remove(0, CommonConstants.HexValueIdentifier.Length);

                // Check that the value entered is a valid 32 bit hexadecimal value.
                successfulParse = UInt32.TryParse(strippedChartScaleLowerLimitText, NumberStyles.HexNumber, provider, out lowerLimitAsUInt32);
                if (successfulParse == false)
                {
                    m_ChartScaleLowerLimit = double.NaN;
                }
                else
                {
                    m_ChartScaleLowerLimit = lowerLimitAsUInt32;
                }

                successfulParse = UInt32.TryParse(strippedChartScaleUpperLimitText, NumberStyles.HexNumber, provider, out upperLimitAsUInt32);
                if (successfulParse == false)
                {
                    m_ChartScaleUpperLimit = double.NaN;
                }
                else
                {
                    m_ChartScaleUpperLimit = upperLimitAsUInt32;
                }
            }
            else
            {
                double lowerLimitAsDouble;
                double upperLimitAsDouble;

                // Check that the value entered is a valid 32 bit decimal value.
                successfulParse = double.TryParse(chartScaleLowerLimitText, out lowerLimitAsDouble);
                if (successfulParse == false)
                {
                    m_ChartScaleLowerLimit = double.NaN;
                }
                else
                {
                    m_ChartScaleLowerLimit = lowerLimitAsDouble;
                }

                successfulParse = double.TryParse(chartScaleUpperLimitText, out upperLimitAsDouble);
                if (successfulParse == false)
                {
                    m_ChartScaleUpperLimit = double.NaN;
                }
                else
                {
                    m_ChartScaleUpperLimit = upperLimitAsDouble;
                }
            }
            
            if ((m_ChartScaleLowerLimit.Equals(double.NaN)) || (m_ChartScaleUpperLimit.Equals(double.NaN)))
            {
                MessageBox.Show(Resources.MBTChartScaleLimitsInvalid, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_ChartScaleLowerLimit = m_WatchVariable.MinChartScale;
                m_ChartScaleUpperLimit = m_WatchVariable.MaxChartScale;
            }
            
            if (m_HexFormat == true)
            {
                m_TextBoxNewChartScaleMin.Text = ((uint)m_ChartScaleLowerLimit).ToString(CommonConstants.FormatStringHex);
                m_TextBoxNewChartScaleMax.Text = ((uint)m_ChartScaleUpperLimit).ToString(CommonConstants.FormatStringHex);
                m_LabelDataFormat.Text = Resources.LegendFormatHex;
            }
            else
            {
                m_TextBoxNewChartScaleMin.Text = m_ChartScaleLowerLimit.ToString(CommonConstants.FormatStringNumeric);
                m_TextBoxNewChartScaleMax.Text = m_ChartScaleUpperLimit.ToString(CommonConstants.FormatStringNumeric);
                m_LabelDataFormat.Text = string.Empty;
            }

            m_TextBoxNewChartScaleMax.SelectAll();
        }
        #endregion - [Form] -

        /// <summary>
        /// Event handler for the <c>KeyPress</c> event associated with all <c>TextBox</c> controls. Checks if the user entered a [CR] and, if so, moves the focus 
        /// to the next <c>TextBox</c> control on the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TextBoxM_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                // Reference to the TextBox which raised the event.
                TextBox textBox = (TextBox)sender;

                // If TextBox is multi-line, ignore the [CR].
                if (!textBox.Multiline)
                {
                    // Scan the form for TextBox controls and move the focus to the next TextBox control in the collection.
                    Control nextControl = this.GetNextControl((Control)sender, true);
                    while (!(nextControl.GetType().Equals(typeof(TextBox))) || (nextControl.Enabled == false))
                    {
                        nextControl = this.GetNextControl(nextControl, true);

                        // Terminate if there are no more controls.
                        if (nextControl == null)
                        {
                            m_ButtonOK.Focus();
                            return;
                        }
                    }

                    // This will trigger a Validating event on the current TextBox control.
                    nextControl.Focus();
                }
            }
            else
            {
                m_ButtonApply.Enabled = true;
            }
        }

        /// <summary>
        /// Event handler for the <c>Validating</c> event associated with all <c>TextBox</c> controls. Validates the user entry and warns the user, using an ErrorProvider 
        /// control, if the value entered is outside the valid range.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void TextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Reference to the TextBox which raised the event.
            TextBox textBox = (TextBox)sender;
            CultureInfo provider = new CultureInfo(CommonConstants.CultureInfoString);
            bool successfulParse;
            double chartScaleLowerLimit = 0, chartScaleUpperLimit = 0;
            if ((textBox.Equals(m_TextBoxNewChartScaleMin)) || (textBox.Equals(m_TextBoxNewChartScaleMax)))
            {
                try
                {
                    // Check whether the watch variable value is to be entered in hexadecimal format.
                    if (m_HexFormat == true)
                    {
                        uint entryAsUInt32;

                        // Check that the value entered is a valid 32 bit hexadecimal value.
                        successfulParse = UInt32.TryParse(textBox.Text, NumberStyles.HexNumber, provider, out entryAsUInt32);
                        if (successfulParse == false)
                        {
                            throw new FormatException();
                        }

                        // Check that the value entered is consistent with the other limit.
                        if (textBox.Equals(m_TextBoxNewChartScaleMin))
                        {
                            chartScaleLowerLimit = entryAsUInt32;

                            if (chartScaleLowerLimit > m_ChartScaleUpperLimit)
                            {
                                MessageBox.Show(Resources.MBTChartScaleLowerLimitInvalid, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                if (chartScaleLowerLimit != m_ChartScaleLowerLimit)
                                {
                                    m_ButtonApply.Enabled = true;
                                    m_ChartScaleLowerLimit = chartScaleLowerLimit;
                                }
                            }

                            m_TextBoxNewChartScaleMin.Text = ((uint)m_ChartScaleLowerLimit).ToString(CommonConstants.FormatStringHex);
                        }
                        else if (textBox.Equals(m_TextBoxNewChartScaleMax))
                        {
                            chartScaleUpperLimit = entryAsUInt32;

                            if (chartScaleUpperLimit < m_ChartScaleLowerLimit)
                            {
                                MessageBox.Show(Resources.MBTChartScaleUpperLimitInvalid, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                if (chartScaleUpperLimit != m_ChartScaleUpperLimit)
                                {
                                    m_ButtonApply.Enabled = true;
                                    m_ChartScaleUpperLimit = chartScaleUpperLimit;
                                }
                            }

                            m_TextBoxNewChartScaleMax.Text = ((uint)m_ChartScaleUpperLimit).ToString(CommonConstants.FormatStringHex);
                        }
                    }
                    else
                    {
                        double entryAsDouble;

                        // Check that the value entered is a valid 32 bit integer.
                        successfulParse = double.TryParse(textBox.Text, NumberStyles.Any, provider, out entryAsDouble);
                        if (successfulParse == false)
                        {
                            throw new FormatException();
                        }

                        // Check that the value is within the limits of a 32 bit integer.
                        if ((entryAsDouble > int.MaxValue) || (entryAsDouble < int.MinValue))
                        {
                            throw new FormatException();
                        }

                        // Check that the value entered is consistent with the other limit.
                        if (textBox.Equals(m_TextBoxNewChartScaleMin))
                        {
                            chartScaleLowerLimit = entryAsDouble;

                            if (chartScaleLowerLimit > m_ChartScaleUpperLimit)
                            {
                                MessageBox.Show(Resources.MBTChartScaleLowerLimitInvalid, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                if (chartScaleLowerLimit != m_ChartScaleLowerLimit)
                                {
                                    m_ButtonApply.Enabled = true;
                                    m_ChartScaleLowerLimit = chartScaleLowerLimit;
                                }
                            }

                            m_TextBoxNewChartScaleMin.Text = m_ChartScaleLowerLimit.ToString(CommonConstants.FormatStringNumeric);
                        }
                        else if (textBox.Equals(m_TextBoxNewChartScaleMax))
                        {
                            chartScaleUpperLimit = entryAsDouble;

                            if (chartScaleUpperLimit < m_ChartScaleLowerLimit)
                            {
                                MessageBox.Show(Resources.MBTChartScaleUpperLimitInvalid, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                if (chartScaleUpperLimit != m_ChartScaleUpperLimit)
                                {
                                    m_ButtonApply.Enabled = true;
                                    m_ChartScaleUpperLimit = chartScaleUpperLimit;
                                }
                            }

                            m_TextBoxNewChartScaleMax.Text = m_ChartScaleUpperLimit.ToString(CommonConstants.FormatStringNumeric);
                        }
                    }
                }
                catch (System.FormatException)
                {
                    // The user has entered invalid data, restore the previous value.
                    if (m_HexFormat == true)
                    {
                        MessageBox.Show(Resources.MBTDataEntryHexInvalid, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        if (textBox.Equals(m_TextBoxNewChartScaleMin))
                        {
                            textBox.Text = ((uint)m_ChartScaleLowerLimit).ToString(CommonConstants.FormatStringHex);
                        }
                        else if (textBox.Equals(m_TextBoxNewChartScaleMax))
                        {
                            textBox.Text = ((uint)m_ChartScaleUpperLimit).ToString(CommonConstants.FormatStringHex);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Resources.MBTDataEntryDecimalInvalid, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // The user has entered a non numeric, restore the previous value.
                        if (textBox.Equals(m_TextBoxNewChartScaleMin))
                        {
                            textBox.Text = m_ChartScaleLowerLimit.ToString(CommonConstants.FormatStringNumeric);
                        }
                        else if (textBox.Equals(m_TextBoxNewChartScaleMax))
                        {
                            textBox.Text = m_ChartScaleUpperLimit.ToString(CommonConstants.FormatStringNumeric);
                        }
                    }
                }
            }
        }

        #region - [Buttons] -
        /// <summary>
        /// Event handler for the OK button <c>Click</c> event. Apply any otstanding changes.
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

        /// <summary>
        /// Event handler for the Apply button <c>Click</c> event. Update the ListBox controls in the calling form with the modified limits.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonApply_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            try
            {
                if (m_HexFormat == true)
                {
                    m_IChartScale.ListBoxChartScaleLowerLimit.Items[m_SelectedIndex] = CommonConstants.HexValueIdentifier + ((uint)m_ChartScaleLowerLimit).ToString(CommonConstants.FormatStringHex);
                    m_IChartScale.ListBoxChartScaleUpperLimit.Items[m_SelectedIndex] = CommonConstants.HexValueIdentifier + ((uint)m_ChartScaleUpperLimit).ToString(CommonConstants.FormatStringHex);
                }
                else
                {
                    m_IChartScale.ListBoxChartScaleLowerLimit.Items[m_SelectedIndex] = m_ChartScaleLowerLimit.ToString(CommonConstants.FormatStringNumeric);
                    m_IChartScale.ListBoxChartScaleUpperLimit.Items[m_SelectedIndex] = m_ChartScaleUpperLimit.ToString(CommonConstants.FormatStringNumeric);
                }

                m_ButtonApply.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MBTModifyLimitFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_ButtonApply.Enabled = true;
                return;
            }

            // Check whether the form was called from the form used to configure the chart recorder and, if so, get the reference to it.
            m_CalledFromAsFormConfigureChartRecorder = CalledFrom as FormConfigureChartRecorder;
            if (m_CalledFromAsFormConfigureChartRecorder != null)
            {
                // Assert the Save ToolStripButton control.
                m_CalledFromAsFormConfigureChartRecorder.TSBSave.Enabled = true;
            }

            return;
        }
        #endregion - [Buttons] -
        #endregion --- Delegated Methods ---
    }
}