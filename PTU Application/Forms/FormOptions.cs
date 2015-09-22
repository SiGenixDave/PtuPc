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
 *  Project:    PTU Application
 * 
 *  File name:  FormOptions.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/06/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/25/10    1.1     K.McD           1.  Inherited ApplicationWindow property renamed to MainWindow.
 * 
 *  01/26/11    1.2     K.McD           1.  Auto-modified as a result of name changes to a number of resources.
 * 
 *  03/21/11    1.3     K.McD           1.  Auto-modified as a result of the proprety name changes to the Security class.
 *
 *	11/14/11	1.3.1	Sean.D			1.	Added code to m_BtnDefaultFont_Click to run Dispose on the old Font item to ensure it releases all resources.
 *	
 *  08/05/13    1.4     K.McD           1.  Bug fix associated with an ArgumentException being thrown soon after the user had selected the default font. Removed the call to dispose 
 *                                          of the m_Font member variable from the m_BtnDefaultFont_Click() method.
 *                                          
 *                                      2.  Added support for operating system specific default fonts to the m_BtnDefaultFont_Click() method.
 *                                      
 *  08/06/13    1.5     K.McD           1.  Created the static GetOSFont() method to get the current font or the default font, depending upon the state of the requestDefaultFont flag, asssociated with the operating system.
 *                                      2.  Created the static SetOSFont() method to set the appropriate font setting, FontXP / FontWin7, depending upon the operating system.
 *                                      3.  Modified the constructor to use the GetOSFont() method to retrieve the current font rather than retrieving it directly from the Font setting.
 *                                      4.  Modified the m_BtnDefaultFont_Click() method to use the GetOSFont() method to retrieve the default font.
 *                                      5.  Modified the m_BtnOK_Click() method to use the SetOSFont() method to set the appropriate font setting rather than writing it directly to the Font setting.
 *                                      6.  Added the MajorBuildXP and MajorBuildWin7 constants to define the values of the Major component of the version number for the Window XP and Window 7 operating systems
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

using Common;
using Common.Configuration;
using Common.Forms;
using Bombardier.PTU.Properties;

namespace Bombardier.PTU.Forms
{
    /// <summary>
    /// Form to allow the user to modify the user settings.
    /// </summary>
    public partial class FormOptions : FormPTUDialog
    {
        #region --- Constants ---
        /// <summary>
        /// The index of the <c>TabPage</c> associated with the configuration of the third party application software.
        /// </summary>
        private const int TabPageIndexThirdParty = 1;

        /// <summary>
        ///  The value of the Major component of the version number corresponding to the Windows 2000, Windows XP and Windows Server 2003 operating systems. Value: 5.
        /// </summary>
        private const int MajorBuildXP = 5;

        /// <summary>
        /// The value of the Major component of the version number corresponding to the Windows 7, Windows Server 2008 and Windows 8 operating systems. Value: 6.
        /// </summary>
        private const int MajorBuildWin7 = 6;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The selected Font.
        /// </summary>
        private Font m_Font;

        /// <summary>
        /// The fully qualified filename corresponding to the TCMS Programmer application. 
        /// </summary>
        private string m_TCMSProgrammer;

        /// <summary>
        /// The fully qualified filename corresponding to the FTP Client application. 
        /// </summary>
        private string m_FTPClient;

        /// <summary>
        /// The fully qualified filename corresponding to the web browser.
        /// </summary>
        private string m_Browser;

        #region - [Read Only] -
        /// <summary>
        /// The default fully qualified filename for the APU programmer application.
        /// </summary>
        public readonly string DefaultTCMSProgrammer = Resources.PathDefaultTCMSProgrammer;

        /// <summary>
        /// The default fully qualified filename for the FTP Client browser.
        /// </summary>
        public readonly string DefaultFTPClient = Resources.PathDefaultFTPClient;

        /// <summary>
        /// The default fully qualified filename for the web browser.
        /// </summary>
        public readonly string DefaultBrowser = Resources.PathDefaultBrowser;
        #endregion - [Read Only] -
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Copy the current user settings to the appropriate member variables and enable/disable options according to
        /// the users security level.
        /// </summary>
        public FormOptions()
        {
            InitializeComponent();

            Debug.Assert(Security != null);

            // Copy the current settings to the appropriate member variables.
            m_Font = GetOSFont(false);
            m_Browser = Settings.Default.PathBrowser;

            if (m_Font != null)
            {
                // Display the font and size associated with the user settings.
                m_LabelFontName.Text = m_Font.Name;
                m_LabelFontSize.Text = m_Font.Size.ToString();
            }
            else
            {
                m_LabelFontName.Text = this.Font.Name;
                m_LabelFontSize.Text = this.Font.Size.ToString();
            }


            m_TextBoxBrowser.Text = m_Browser;

            // Only allow the user to configure the third party applications if the user has the appropriate privileges.
            if (Security.SecurityLevelCurrent >= Security.SecurityLevelHighest)
            {
                m_GroupBoxBrowser.Enabled = true;
                m_ButtonDefaultApplications.Enabled = true;
            }
            else
            {
                m_GroupBoxBrowser.Enabled = false;
                m_ButtonDefaultApplications.Enabled = false;
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
        #region - [Font] -
        /// <summary>
        /// Event handler for the 'Font - Select' button 'Click' event. Display the font dialog box and update the <c>Font</c> property with the selected font to
        /// give the user some idea of what the font looks like.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnSelectFont_Click(object sender, EventArgs e)
        {
            // Select the current font when the dialog box is shown.
            m_FontDialog.Font = m_Font;
            
            m_FontDialog.ShowDialog();

            // Set the local copy of the font to equal the selected font.
            m_Font = m_FontDialog.Font;

            // Update the font for this form to give the user some idea what the selected font looks like.
            this.Font = m_Font;

            // Update the font name and size.
            m_LabelFontName.Text = m_Font.Name;
            m_LabelFontSize.Text = m_Font.Size.ToString();
        }

        /// <summary>
        /// Event handler for the 'Font - Default' button 'Click' event. Set the current font to be the default font.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnDefaultFont_Click(object sender, EventArgs e)
        {
            m_Font = GetOSFont(true);

            // Update the font for this form to give the user some idea what the font will look like.
            this.Font = m_Font;

            // Display the default font name and size.
            m_LabelFontName.Text = m_Font.Name;
            m_LabelFontSize.Text = m_Font.Size.ToString();
        }
        #endregion - [Font] -

        #region - [Third Party Software] -
        /// <summary>
        /// Event handler for the 'Third Party Software - Browser - Specify' button Click event. Show the <c>OpenFileDialog</c> form so that the user can select 
        /// the appropriate web browser application.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnSpecifyBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = CommonConstants.ExtensionExecutable.Remove(0, 1); ;
            openFileDialog.Title = Resources.FileDialogOpenTitleWebBrowser;
            openFileDialog.Filter = "*" + CommonConstants.ExtensionExecutable + "|*" + CommonConstants.ExtensionExecutable;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                m_TextBoxBrowser.Text = openFileDialog.FileName;
            }

            // Set the web browser fully qualified filename to the default value.
            m_Browser = m_TextBoxBrowser.Text;
        }

        /// <summary>
        /// Event handler for the Application Data button Click event. Display the <c>FolderBrowserDialog</c> class and update the 
        /// Application Data <c>TextBox</c> with the selected directory.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnDefaultApplications_Click(object sender, EventArgs e)
        {
            // Set the local copies of the fully qualified file names for the APU Programmer and Browser applications to their default values.
            m_TCMSProgrammer = DefaultTCMSProgrammer;
            m_FTPClient = DefaultFTPClient;
            m_Browser = DefaultBrowser;
            
            // Display the default applications.
            m_TextBoxBrowser.Text = m_Browser;
        }
        #endregion - [Third Party Software] -

        /// <summary>
        /// Event handler for the <c>TabControl</c>, <c>SelectedIndexChanged</c> event. Display a message box if the user selects the 'Third Party Software' tab page
        /// and is not logged into the required security level.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_TabControl.SelectedIndex == TabPageIndexThirdParty)
            {
                if (Security.SecurityLevelCurrent < Security.SecurityLevelHighest)
                {
                    MessageBox.Show(Resources.MBTOptionThirdPartyUnauthorized, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Event handler for the Cancel button 'Click' event. Close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event handler for the OK button 'Click' event. Update the user settings and then close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnOK_Click(object sender, EventArgs e)
        {
            #region - [Font] -
            Parameter.Font = m_Font;

            // Change the font for the main window.
            MainWindow.Font = m_Font;

            // Update the font associated with this form
            Font = m_Font;

            // Update the user settings.
            SetOSFont(m_Font);
            #endregion - [Font] -

            #region - [Third Party Software] -
            Settings.Default.PathBrowser = m_Browser;
            #endregion - [Third Party Software]-

            // Save the updated settings.
            Settings.Default.Save();
            this.Close();
        }
        #endregion --- Delegated Methods ---

        #region --- Static Methods ---
        /// <summary>
        /// Get the default or current font associated with the operating system, depending upon the state of the <paramref name="requestDefaultFont"/> flag.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The current Windows operating system is determined by checking the value of the Major amd Minor properties of 
        /// the Environment.OSVersion.Version class.
        /// </para>
        /// <para>The version references associated with each Windows operating system are given below: 
        /// </para>
        ///     <list type="bullet">
        ///         <listheader><osname>Operating System</osname><version>Version Number</version></listheader>
        ///         <item><osname>Windows 8</osname><version>6.2</version></item> 
        ///         <item><osname>Windows Server 2012</osname><version>6.2</version></item> 
        ///         <item><osname>Windows 7</osname><version>6.1</version></item> 
        ///         <item><osname>Windows Server 2008 R2</osname><version>6.1</version></item> 
        ///         <item><osname>Windows Server 2008</osname><version>6.0</version></item> 
        ///         <item><osname>Windows Vista</osname><version>6.0</version></item> 
        ///         <item><osname>Windows Server 2003 R2</osname><version>5.2</version></item> 
        ///         <item><osname>Windows Server 2003</osname><version>5.2</version></item> 
        ///         <item><osname>Windows XP 64-Bit Edition</osname><version>5.2</version></item> 
        ///         <item><osname>Windows XP</osname><version>5.1</version></item> 
        ///         <item><osname>Windows 2000</osname><version>5.0</version></item> 
        ///     </list>
        /// </remarks>
        /// <param name="requestDefaultFont">A flag to specify whether the default or current font is required. True, specifies 
        /// that the default font is required; otherwise, false indicates that the current font is required.</param>
        /// <returns>The default or the current font associated with the operating system, depending upon which font was requested.</returns>
        internal static Font GetOSFont(bool requestDefaultFont)
        {
            // Get the version of the operating system.
            Version osVersion = Environment.OSVersion.Version;

            // Local variables used to store the default and current fonts associated with the operating system.
            Font currentFont, defaultFont;

            if (osVersion.Major == MajorBuildXP)
            {
                currentFont = Settings.Default.FontXP;
                defaultFont = new Font(Parameter.DefaultFontFamilyXP, Parameter.DefaultFontSizeXP);
            }
            else if (osVersion.Major == MajorBuildWin7)
            {
                currentFont = Settings.Default.FontWin7;
                defaultFont = new Font(Parameter.DefaultFontFamilyWin7, Parameter.DefaultFontSizeWin7);
            }
            else
            {
                // Not recognised, use default XP font.
                currentFont = Settings.Default.FontXP;
                defaultFont = new Font(Parameter.DefaultFontFamilyXP, Parameter.DefaultFontSizeXP);
            }

            if (requestDefaultFont == true)
            {
                return defaultFont;
            }
            else
            {
                return currentFont;
            }
        }

        /// <summary>
        /// Set the font setting associated with the operating system to the font specified by the <paramref name="font"/> parameter.
        /// </summary>
        /// <remarks>This method does not save the current settings to disk.</remarks>
        /// <param name="font">The font that is to be used to update the appropriate font setting.</param>
        internal static void SetOSFont(Font font)
        {
            // Get the version of the operating system.
            Version osVersion = Environment.OSVersion.Version;

            if (osVersion.Major == MajorBuildXP)
            {
                Settings.Default.FontXP = font;
            }
            else if (osVersion.Major == MajorBuildWin7)
            {
                Settings.Default.FontWin7 = font;
            }
            else
            {
                // Not recognised, use default XP font.
                Settings.Default.FontXP = font;
            }

            return;
        }
        #endregion --- Static Methods ---
    }
}