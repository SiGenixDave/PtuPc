#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    PTU Application
 * 
 *  File name:  FormConfigurePasswords.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  06/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  03/21/11    1.1     K.McD           1.  Modified to support the changes to the Security class.
 *
 *	02/08/12	1.1.1	Sean.D			1.	Modified m_BtnApply_Click to use the updated password hashing function.
 *	
 *  02/27/14    1.2     K.McD           1.  Changed the name to FormConfigurePasswordProtection to be consistent with 
 *                                          the MenuInterfaceApplication class.
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;

using Common;
using Common.Configuration;
using Common.Forms;
using Bombardier.PTU.Properties;

namespace Bombardier.PTU.Forms
{
    /// <summary>
    /// Form to allow the engineering user to manage password settings.
    /// </summary>
    public partial class FormConfigurePasswordProtection : FormPTUDialog
    {
        #region --- Member Variables ---
        /// <summary>
        /// Flag to control event processing associated with the <c>TextBox</c> controls. True, to skip the <c>Leave</c> event processing; otherwise, false.
        /// </summary>
        private bool m_SkipLeaveEvent;
   
        /// <summary>
        /// The new password entered by the user.
        /// </summary>
        private string m_NewPassword;

        /// <summary>
        /// The password verification string entered by the user.
        /// </summary>
        private string m_VerifyPassword;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class. Add the description text to the <c>ComboBox</c> control.
        /// </summary>
        public FormConfigurePasswordProtection()
        {
            InitializeComponent();
            
            // For those accounts that require a password, add the descriptions to the drop down list.
            m_ComboBoxSecurityLevelDescription.Items.Clear();

            // Populate the ComboBox control with the names of those client security levels that require password access.
            for (short securityLevel = (short)Security.SecurityLevelBase; securityLevel <= (short)Security.SecurityLevelHighest; securityLevel++)
            {
                // Don't add the base level as this level does not require password access.
                if (securityLevel == (short)Security.SecurityLevelBase)
                {
                    continue;
                }
                string description = Security.GetSecurityDescription((SecurityLevel)securityLevel);
                m_ComboBoxSecurityLevelDescription.Items.Add(description);
            }

            // Display the account description for the lowest level that requires a password in the combo box.
            int passwordCount = m_ComboBoxSecurityLevelDescription.Items.Count;
            if (passwordCount > 0)
            {
                m_ComboBoxSecurityLevelDescription.Text = (string)m_ComboBoxSecurityLevelDescription.Items[0];
            }
            else
            {
                m_ComboBoxSecurityLevelDescription.Items.Clear();
                m_ComboBoxSecurityLevelDescription.Text = string.Empty;
                m_ComboBoxSecurityLevelDescription.Enabled = false;
                throw new InvalidOperationException(Resources.MBTNoPasswordsToConfigure);
            }
            
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
        #region - Radio Button -
        /// <summary>
        /// Event handler for the <c>CheckedChange</c> event. Check which radio button option the user has selected and enable/disable the appropriate controls.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_RadioReset_CheckedChanged(object sender, EventArgs e)
        {
            if (m_RadioButtonResetPassword.Checked)
            {
                // Reset password to the hard coded default.
                m_ButtonApply.Enabled = true;
                m_TextBoxNewPassword.Text = "";
                m_TextBoxVerifyPassword.Text = "";
                m_GroupBoxVerifyPassword.Enabled = false;
            }
            else
            {
                // Create a new password.
                // Don't allow user to select Apply until a verified password has been entered.
                m_ButtonApply.Enabled = false;
                m_GroupBoxVerifyPassword.Enabled = true;

                // If a TextBox looses focus process the corresponding Leave event. 
                m_SkipLeaveEvent = false;
            }
        }
        #endregion - Radio Button -

        #region - Buttons -
        /// <summary>
        /// Event handler for the 'Apply' button <c>Click</c> event. Check whether the user has opted to: (a) create a new password or to (b) reset the password and 
        /// update the <c>Security</c> class accordingly.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnApply_Click(object sender, EventArgs e)
        {
            if (m_RadioButtonNewPassword.Checked)
            {
                // The user has opted to replace the old password with a new password.
                // Update the appropriate settings with the new password

				Int64 newPasswordHash = Security.GetHashCode(m_NewPassword);

                // Check which security level has been selected.
                if (m_ComboBoxSecurityLevelDescription.Text == Security.DescriptionLevel1)
                {
                    Security.HashCodeLevel1 = newPasswordHash;
                }
                else if (m_ComboBoxSecurityLevelDescription.Text == Security.DescriptionLevel2)
                {
                    Security.HashCodeLevel2 = newPasswordHash;
                }
            }
            else
            {
                // The user has opted to reset the password to the default setting.
                // Check which security level has been selected.
                if (m_ComboBoxSecurityLevelDescription.Text == Security.DescriptionLevel1)
                {
                    Security.SetHashCodeToDefault(SecurityLevel.Level1);
                }
                else if (m_ComboBoxSecurityLevelDescription.Text == Security.DescriptionLevel2)
                {
                    Security.SetHashCodeToDefault(SecurityLevel.Level2);
                }
            }
            DialogResult = DialogResult.Yes;
            Close();
        }

        /// <summary>
        /// Event handler for the Cancel button 'Click' event. Close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion - Buttons -

        #region - TextBox -
        /// <summary>
        /// Event handler for the <c>KeyPress</c> event associated with the 'New Password' <c>TextBox</c>. Check whether the user has entered a string and, if so, enable
        /// the 'Verify Password' <c>TextBox</c> control.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TxtNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check whether the user has just entered a [CR].
            if (e.KeyChar.Equals('\r'))
            {
                // Get rid of any leading or trailing whitespace.
                m_NewPassword = m_TextBoxNewPassword.Text.Trim();

                // Check whether the the user entered a string.
                if (m_TextBoxNewPassword.Text != string.Empty)
                {
                    // Enable the 'Verify Password' TextBox and Label controls.
                    m_TextBoxVerifyPassword.Enabled = true;       
                    m_LegendVerifyPassword.Enabled = true;

                    // Move the cursor to the 'Verify Password' TextBox.
                    m_TextBoxVerifyPassword.Focus();
                }
            }
        }

        /// <summary>
        /// Event handler for the <c>KeyPress</c> event associated with the 'Verify Password' <c>TextBox</c>. Check whether the user has entered a string and, if so,
        /// compare the string with the one entered into the 'New Password <c>TextBox</c> control. If the strings match, enable and set the focus to the 
        /// 'Apply' button; otherwise: (a) inform the user; (b) clear the <c>TextBox</c> controls and (c) set the focus back to the 'New Password' <c>TextBox</c> control.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TxtVerify_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check whether the user has just entered a [CR].
            if (e.KeyChar.Equals('\r'))
            {
                // Get rid of any leading or trailing whitespace.
                m_VerifyPassword = m_TextBoxVerifyPassword.Text.Trim();

                // Check whether the the user entered a string.
                if (m_TextBoxVerifyPassword.Text != string.Empty)
                {
                    // Compare the string with the string entered into the 'New Password' TextBox control.
                    int compareTo = m_VerifyPassword.CompareTo(m_NewPassword);
                    if (compareTo == 0)
                    {
                        // Match.
                        // Skip the processing associated with the Leave event of the 'Verify Password' TextBox control.
                        m_SkipLeaveEvent = true;

                        // Enable and move the focus to the Apply button.
                        m_ButtonApply.Enabled = true;
                        m_ButtonApply.Focus();
                    }
                    else
                    {
                        // No match, retry.
                        MessageBox.Show(Resources.MBTSecurityMismatch, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear down the TextBoxe controls and set the focus to the New Password TextBox.
                        m_TextBoxNewPassword.Text = string.Empty;
                        m_TextBoxVerifyPassword.Text = string.Empty;
                        m_TextBoxNewPassword.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for the <c>TextBox</c>, <c>Leave</c> event. Provided that the <c>SkipLeaveEvent</c> flag is not asserted, simulate the user having entered
        /// a carriage return into the <c>TextBox</c> control that raised the event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void TextBox_Leave(object sender, EventArgs e)
        {
            // Skip processing if the SkipLeaveEvent flag has been asserted.
            if (m_SkipLeaveEvent == true)
            {
                return;
            }

            TextBox textbox = (TextBox)sender;

            // Instantiate a KeyPressEventArgs object associated with a [CR] having been depressed.
            KeyPressEventArgs keyPressEventArgs = new KeyPressEventArgs('\r');

            // Determine which TextBox raised the event and simulate the operator having just entered a [CR].
            if (textbox.Equals(m_TextBoxNewPassword))
            {
                m_TxtNew_KeyPress(sender, keyPressEventArgs);
            }
            else if (textbox.Equals(m_TextBoxVerifyPassword))
            {
                m_TxtVerify_KeyPress(sender, keyPressEventArgs);
            }
        }
        #endregion - TextBox -
        #endregion --- Delegated Methods ---
    }
}