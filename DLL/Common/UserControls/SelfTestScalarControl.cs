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
 *  File name:  SelfTestScalarControl.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/07/21    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

using Common;
using Common.Configuration;
using Common.Forms;

using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// The scalar event variable user control. Displays the engineering value corresponding to the specified raw <c>Value</c> property for the scalar event variable specified
    /// by the <c>Identifier</c> property.
    /// </summary>
    public partial class SelfTestScalarControl : SelfTestControl
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of th class.
        /// </summary>
        public SelfTestScalarControl()
        {
            InitializeComponent();
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

                #region - [Detach the event handler methods.] -
                #endregion - [Detach the event handler methods.] -
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

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the current value of watch variable.
        /// </summary>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Data"),
        Description("The current value.")
        ]
        public override double Value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;

                if (InvalidValue == true)
                {
                    m_LabelValueField.Text = string.Empty;
                }
                else
                {
                    // Check whether the watch variable is defined.
                    if (m_LabelNameField.Text != CommonConstants.VariableNotDefinedString)
                    {
                        // Set the background and foreground colours according to the value.
                        // m_LblValueField.BackColor = (value == 0) ? m_BackColorValueFieldZero : m_BackColorValueFieldNonZero;
                        // m_LblValueField.ForeColor = (value == 0) ? m_ForeColorValueFieldZero : m_ForeColorValueFieldNonZero;

                        double engineeringValue;
                        try
                        {
                            engineeringValue = m_Value * Lookup.SelfTestVariableTable.Items[m_Identifier].ScaleFactor;
                            engineeringValue = Math.Round(engineeringValue, m_DecimalPlaces);

                            if (Lookup.SelfTestVariableTable.Items[m_Identifier].FormatString.ToLower() == FormatStringFieldGeneralNumber)
                            {
                                m_LabelValueField.Text = engineeringValue.ToString(FormatStringNumericString);
                            }
                            else if (Lookup.SelfTestVariableTable.Items[m_Identifier].FormatString.ToLower() == FormatStringFieldHexadecimal)
                            {
                                m_LabelValueField.Text = HexValueIdentifier + ((long)engineeringValue).ToString("X");
                            }
                            else
                            {
                                m_LabelValueField.Text = engineeringValue.ToString();
                            }
                        }
                        catch (Exception)
                        {
                            m_Value = double.NaN;
                            m_LabelValueField.Text = string.Empty;
                            return;
                        }
                    }
                    else
                    {
                        m_Value = double.NaN;
                        m_LabelValueField.Text = string.Empty;
                        return;
                    }
                }
            }
        }
        #endregion --- Properties ---
    }
}
