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
 *  File name:  EventEnumeratorControl.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  03/18/11    1.2     K.McD           1.  Added a try/catch block to the Value property to ensure that an exception isn't thrown if an invalid watch identifier 
 *                                          is specified for the Identifier property of the control.
 * 
 *  03/28/11    1.3     K.McD           1.  Auto-modified as a result of name changes to a number of constants.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.ComponentModel;

using Common;
using Common.Configuration;
using Common.Forms;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// The enumerator self test variable user control. Displays the enumerator text corresponding to the specified <c>Value</c> property for the enumerator self test
    /// variable specified by the <c>Identifier</c> property.
    /// </summary>
    public partial class SelfTestEnumeratorControl : SelfTestControl
    {
        #region --- Member Variables ---
        /// <summary>
        /// The value cast to a uint.
        /// </summary>
        uint m_ValueUINT;

        /// <summary>
        /// Reference to the client form as type <c>FormPTU</c>.
        /// </summary>
        private FormPTU m_ClientAsFormPTU;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SelfTestEnumeratorControl()
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
        /// Gets or sets the current value of the enumerator self test variable.
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
                m_ValueUINT = (uint)m_Value;

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
                        // m_LblValueField.BackColor = (m_ValueUINT == 0) ? m_BackColorValueFieldZero : m_BackColorValueFieldNonZero;
                        // m_LblValueField.ForeColor = (m_ValueUINT == 0) ? m_ForeColorValueFieldZero : m_ForeColorValueFieldNonZero;

                        if (m_ClientAsFormPTU.MainWindow != null)
                        {
                            if (m_ClientAsFormPTU.MainWindow.Enumeration == false)
                            {
                                m_LabelValueField.Text = m_ValueUINT.ToString();

                                // Call the base property.
                                base.Value = m_Value;
                                return;
                            }
                        }

                        try
                        {
                            // Display the status text corresponding to the value, this is derived from the list of <c>t_EnumBit</c> items associated with the bitmask.
                            m_LabelValueField.Text = Lookup.SelfTestVariableTable.GetEnumeratorText(m_Identifier, m_ValueUINT);
                        }
                        catch (Exception)
                        {
                            m_Value = double.NaN;
                            m_ValueUINT = 0;
                            m_LabelValueField.Text = string.Empty;
                            return;
                        }
                    }
                    else
                    {
                        m_Value = double.NaN;
                        m_ValueUINT = 0;
                        m_LabelValueField.Text = string.Empty;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the client form associated with the control.
        /// </summary>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Data"),
        Description("The client form associated with the control.")
        ]
        public override Form ClientForm
        {
            get { return m_ClientForm; }
            set
            {
                base.ClientForm = value;
                m_ClientAsFormPTU = m_ClientForm as FormPTU;
            }
        }
        #endregion --- Properties ---
    }
}
