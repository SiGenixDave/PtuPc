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
 *  File name:  EventBitmaskControl.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  03/18/11    1.2     K.McD           1.  Modified the Value property to correctly handle the case where an invalid watch identifier is specified for the Identifier 
 *                                          property of the control.
 * 
 *                                      2.  Added the event handlers for: (a) the name field 'DoubleClick' event, (b) the 'Show Flags' context menu option 'Click' event and 
 *                                          (c) the 'Show Definition' context menu option 'Click' event.
 * 
 *  03/28/11    1.3     K.McD           1.  Auto-modified as a result of name changes to a number of constants.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

using Common;
using Common.Configuration;
using Common.Forms;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// The bitmask event variable user control. Displays the asserted flags corresponding to the specified <c>Value</c> property for the bit mask event variable specified
    /// by the <c>Identifier</c> property.
    /// </summary>
    public partial class EventBitmaskControl : EventControl
    {
        #region --- Member Variables ---
        /// <summary>
        /// The value cast to a uint.
        /// </summary>
        uint m_ValueUINT;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes anew instance of the user control.
        /// </summary>
        public EventBitmaskControl()
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
                this.m_LabelNameField.DoubleClick -= new System.EventHandler(this.m_MenuItemShowDefinition_Click);
                this.m_LabelUnitsField.DoubleClick -= new System.EventHandler(this.m_MenuItemShowFlags_DoubleClick);
                this.m_MenuItemShowDefinition.Click -= new System.EventHandler(this.m_MenuItemShowDefinition_Click);
                this.m_MenuItemShowFlags.DoubleClick -= new System.EventHandler(this.m_MenuItemShowFlags_DoubleClick);
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

        #region --- Delegated Methods ---
        #region - [Context Menu] -
        /// <summary>
        /// Event handler for the 'Show Definition' context menu option <c>Click</c> event. Call the ShowHelpPopup() method.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemShowDefinition_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ShowHelpPopup();
        }

        /// <summary>
        /// Event handler for the 'Show Flags' context menu option <c>Click</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemShowFlags_DoubleClick(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the ClientForm propery is null.
            if (ClientForm != null)
            {
                FormShowFlagsEvent formShowFlagsEvent = new FormShowFlagsEvent(this);
                formShowFlagsEvent.CalledFrom = m_ClientForm;
                formShowFlagsEvent.Show();
            }
        }
        #endregion - [Context Menu] -

        /// <summary>
        /// Event handler for the <c>DoubleClick</c> event associated with the name field label. Call the ShowHelpPopup() method.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_LabelNameField_DoubleClick(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ShowHelpPopup();
        }
        #endregion --- Delegated Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the current value of the bit mask event variable.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("false"),
        Description("The current value of the bitmask watch variable.")
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
                        // The text that is to be displayed in the value field.
                        string valueText;
                        valueText = HexValueIdentifier + m_ValueUINT.ToString(FormatStringHex);

                        m_LabelValueField.Text = valueText;
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
        #endregion --- Properties ---
    }
}
