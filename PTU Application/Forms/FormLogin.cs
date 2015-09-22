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
 *  File name:  FormSecurity.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/22/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/25/10    1.1     K.McD           1.  Replaced the overidden FprmPTUDialog_Shown() event handler with the FormLogin_Shown() event handler to ensure that the 
 *                                          FormPTUDialog_Shown() event handler was called.
 * 
 *                                      2.  Inherited ApplicationWindow property renamed to MainWindow.
 * 
 *  03/21/11    1.2     K.McD           1.  Modified the design such that user simply enters the password associated with the required security level to log into 
 *                                          the account i.e. the required security level need not be selected from a drop down list.
 *
 *	02/08/12	1.2.1	Sean.D			1.	Modified m_TxtPassword_KeyPress to use the new password hashing function.
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;

using Common;
using Common.Forms;
using Bombardier.PTU.Properties;

namespace Bombardier.PTU.Forms
{
    /// <summary>
    /// Form to allow the user to log into the different security levels associated with the PTU application.
    /// </summary>
    public partial class FormLogin : FormPTUDialog
    {
        #region --- Constants ---
        /// <summary>
        /// The maximum allowed number of attempts at logging in before terminating the process. Value: 3.
        /// </summary>
        public const int MaxAttempts = 3;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The number of invalid attempts at inputting the correct password.
        /// </summary>
        private int m_Attempts;

        /// <summary>
        /// The hash code associated with the password.
        /// </summary>
        private long m_Hashcode;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        public FormLogin()
        {
            InitializeComponent();

            Debug.Assert(Security != null);
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
        /// Event handler for the form <c>Shown</c> event. Initialize the combo box used to request the user's security level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLogin_Shown(object sender, EventArgs e)
        {
            Update();

            // This is only called from the PTU main application window therefor ensure that the ApplicationWindow property has been set.
            Debug.Assert(MainWindow != null);

            #region - [Code Snippet to Generate Hash Code] -
            // Initially use the debug facility with this section of code to determine the hashcodes associated with the desired passwords. Thereafter
            // delete the passwords.
            // Security security = new Security();
            // m_Hashcode = security.GetHashCode("Password1");
            //
            #endregion - [Code Snippet to Generate Hash Code] -
        }

        /// <summary>
        /// Event handler for the KeyPress event. Check if the user has depressed the escape key and if so close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            byte keyChar = (byte)e.KeyChar;
            if (keyChar == CommonConstants.AsciiEscape)
            {
                Close();
            }
        }

        /// <summary>
        /// Event handler for the Cancel button 'Click' event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event handler for the TextBox KeyPress event. Checks if the user entered a [CR] and, if so, starts processing the input.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                // Check if the number of attempts at logging on has expired.
                m_Attempts++;
                if (m_Attempts >= MaxAttempts)
                {
                    MessageBox.Show(Resources.MBTSecurityLoginFailed, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                // Get the hash code corresponding to the entered password.
				m_Hashcode = Security.GetHashCode(m_TextBoxPassword.Text);

                // Check whether the password is valid.
                if (m_Hashcode == Security.HashCodeLevel1)
                {
                    Security.SecurityLevelCurrent = SecurityLevel.Level1;
                    MainWindow.ShowSecurityLevelChange(Security);
                    Close();
                }
                else if (m_Hashcode == Security.HashCodeLevel2)
                {
                    Security.SecurityLevelCurrent = SecurityLevel.Level2;
                    MainWindow.ShowSecurityLevelChange(Security);
                    Close();
                }
                else if (m_Hashcode == Security.HashCodeLevel3)
                {
                    Security.SecurityLevelCurrent = SecurityLevel.Level3;
                    MainWindow.ShowSecurityLevelChange(Security);
                    Close();
                }
                else
                {
                    m_TextBoxPassword.Text = "";
                    MessageBox.Show(Resources.MBTSecurityPasswordIncorrect, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion --- Delegated Methods ---
    }
}