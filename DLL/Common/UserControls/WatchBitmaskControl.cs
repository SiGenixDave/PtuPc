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
 *  File name:  WatchBitmaskControl.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/28/11    1.1     K.McD           1.  Modified the DoubleClick event handler such that if the user control is not write enabled and the user attempts to modify 
 *                                          the current value, the information message reported to the user will differentiate between a read-only watch variable and 
 *                                          one where the current security level is insufficient to allow the user to modify the value.
 * 
 *  03/18/11    1.2     K.McD           1.  Replaced stub calls for the diagnostic help information with calls to the ShowHelpPopup() method.
 *                                      2.  Modified the Value property to correctly handle the case where an invalid watch identifier is specified for the Identifier 
 *                                          property of the control.
 *                                      3.  Added checks for IsDisposed in a number of methods.
 * 
 *  03/28/11    1.3     K.McD           1.  Auto-modified as a result of name changes to a number of resources and constants.
 *                                      2.  Modified to use the old identifier lookup table.
 *                                      3.  Included a number of try/catch blocks.
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

using Common;
using Common.Configuration;
using Common.Communication;
using Common.Forms;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// The bitmask watch variable user control. Displays the asserted flags corresponding to the specified <c>Value</c> property for the bit mask watch variable specified
    /// by the <c>Identifier</c> property.
    /// </summary>
    public partial class WatchBitmaskControl : WatchControl
    {
        #region --- Member Variables ---
        /// <summary>
        /// The value cast to a uint.
        /// </summary>
        uint m_ValueUINT;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the bit mask control.
        /// </summary>
        public WatchBitmaskControl()
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
                this.m_LabelValueField.DoubleClick -= new System.EventHandler(this.m_LabelValueField_DoubleClick);
                this.m_LabelUnitsField.DoubleClick -= new System.EventHandler(this.m_MenuItemShowFlags_Click);
                this.m_MenuItemShowDefinition.Click -= new System.EventHandler(this.m_MenuItemShowDefinition_Click);
                this.m_MenuItemChangeValue.Click -= new System.EventHandler(this.m_MenuItemChangeValue_Click);
                this.m_MenuItemShowFlags.Click -= new System.EventHandler(this.m_MenuItemShowFlags_Click);
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
        /// Event handler for the 'Change Value' context menu option <c>Click</c> event. If applicable, shows the form which allows the user to change the value of the 
        /// enumerator watch variable.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemChangeValue_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            FormPTU clientAsFormPTU = ClientForm as FormPTU;
            if (clientAsFormPTU != null)
            {
                try
                {
                    FormChangeBitmask formChangeBitmask = new FormChangeBitmask(this);
                    formChangeBitmask.CalledFrom = m_ClientForm;
                    formChangeBitmask.ShowDialog();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        /// <summary>
        /// Event handler for the 'Show Flags' context menu option <c>Click</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemShowFlags_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the ClientForm propery is null.
            if (ClientForm != null)
            {
                try
                {
                    FormShowFlags formShowFlags = new FormShowFlags(this);
                    formShowFlags.CalledFrom = m_ClientForm;
                    formShowFlags.Show();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
        #endregion - [Context Menu] -

        /// <summary>
        /// Event handler for the <c>DoubleClick</c> event associated with the value field label. Check whether the watch variable is write-enabled and, if so, 
        /// simulate the user selecting the 'Change Value' context menu option; otherwise, report a warning.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_LabelValueField_DoubleClick(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (WriteEnabled)
            {
                m_MenuItemChangeValue_Click(this, new EventArgs());
            }
            else
            {
                // Check whether the ReadOnly attribute is set.
                if ((AttributeFlags & AttributeFlags.PTUD_READONLY) == AttributeFlags.PTUD_READONLY)
                {
                    MessageBox.Show(Resources.MBTReadOnly, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Resources.MBTUnauthorizedChangeValue, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Set the state of the <c>WriteEnabled</c> property and perform any associated logic.
        /// </summary>
        /// <param name="writeEnabled">The required state of the WriteEnabled property.</param>
        protected override void SetWriteEnabledProperty(bool writeEnabled)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (writeEnabled)
            {
                m_MenuItemChangeValue.Enabled = true;
            }
            else
            {
                m_MenuItemChangeValue.Enabled = false;
            }

            // Call the base method.
            base.SetWriteEnabledProperty(writeEnabled);
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the current value of the bit mask watch variable.
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

                // Call the base property.
                base.Value = m_Value;
            }
        }
        #endregion --- Properties ---
    }
}