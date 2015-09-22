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
 *  File name:  MenuInterfaceApplication.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/29/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/25/10    1.1     K.McD           1.  Modified ConfigureSystemInformation() to call the FormShowSystemInformation class.
 * 
 *  10/08/10    1.2     K.McD           1.  Bug fix SNCR001.15. Deleted the call to WriteCarIdentifier() in the method ConfigureWorksets().
 * 
 *  10/29/10    1.3     K.McD           1.  Now uses the MainWindow property inherited from the parent class.
 *                                      2.  Methods no longer use the override keyword as these methods have been deleted from the parent class.
 * 
 *  11/19/10    1.4     K.McD           1.  Moved the ConfigureWorksets() method to the Watch namespace.
 *                                      2.  Rearranged the order of the methods so that they are grouped by menu item groups.
 * 
 *  01/26/11    1.5     K.McD           1.  Modified the message text asssociated with the OpenDataDictionary() method.
 * 
 *  03/21/11    1.6     K.McD           1.  Auto-modified as a result of the property name changes associated with the Security class.
 *                                      2.  Modified a number of XML tags and error messages.
 *                                      3.  Renamed the method ConfigureSystemInformation() method.
 *                                      4.  Added the ConfigureRealTimeClock() method.
 *                                      
 *  07/24/11    1.7     K.McD           1.  Corrected the user message in the ConfigureRealTimeClock() method.
 *  
 *  10/10/11    1.8     K.McD           1.  Included support for the 'Help/PTU Help' menu option. Added the ShowUserManual() method.
 *  
 *  10/26/11    1.9     K.McD           1.  Modified the OpenDataDictionary() method - SNCR002.38. As the 'File/Select Data Dictionary' menu option is now permanently 
 *                                          enabled this method must now check whether the current mode of the PTU and the security level of the user is appropriate 
 *                                          for this action and, if so, will select a new data dictionary. If this action is not appropriate the PTU will report an error.
 *                                          
 *  06/19/13    1.10    K.McD           1.  Modified the Login() method to check whether the project requires a WibuBox security device and, if so, checks that a valid 
 *                                          WibuBox is attached to the system before displaying the log-on screen. If a valid WibuBox is not found an error message is
 *                                          displayed.
 *                                          
 *  25/02/14    1.11    K.McD           1.  Updated to reflect class name changes.
 *  
 *  04/15/15    1.12    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘WibuBox: [Present | Not Present]’.
 *                                              
 *                                      2.  Although only relevant to Bombardier Field Service Engineers that support a number of PTU projects, the Software User Manual
 *                                          and Release Notes documents are to be made project specific by prepending the project identifier to the file name e.g.
 *                                          'R8PR.Portable Test Unit - Release Notes.pdf', 'CTA.Portable Test Unit - Release Notes.pdf' etc.
 *                                          
 *                                      Modifications
 *                                      1.  Modified the ShowUserManual() method to include the project identifier in the filename for the Software User Manual. Ref.: 2.
 *                                      2   Modified the signature associated with the call to WibuBoxCheckForValidEntry() in the Login() method. The suppressMessageBox
 *                                          flag parameter is set to false to ensure that error reporting is not suppressed. Ref.: 1.1.
 */

/*
 *  08/12/15    1.13    K.McD       References
 *                                  1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                      from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                     
 *                                      1.  On the R188 project only, all references to 'PTU' are to be replaced with 'PTE' and all occurrences of 'Portable Test Unit'
 *                                          are to be replaced with 'Portable Test Equipment'. Where support for multiple legends is not possible, 'Portable Test
 *                                          Application' is to be used as an alternative to 'Portable Test Unit'/'Portable Test Equipment.
 *                                      
 *                                  2.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 28. If the project requires a Wibu-Key and the files ‘Configuration.xml’ and
 *                                      ‘project-identifier.Configuration.xml’ do not exist, there is a problem trying to log on as a Factory user in order to
 *                                      select the required data dictionary file.
 *                                          
 *                                      As the project-identifier is now passed as a desktop shortcut parameter, the Wibu-Key timer is initialized in the MDI
 *                                      constructor, if required; as soon as the user tries to log on they are automatically logged out as the initialized timer
 *                                      calls the WibuBoxCheckIfRemoved() method which returns a value of true because the FormLogin Form is instantiated without
 *                                      first calling the WibuBoxCheckForValidEntry() method as the Parameter.ProjectInformation.ProjectIdentifier parameter used
 *                                      in the call to WibuBoxCheckIfRequires() is still set to string.Empty at that stage as no data dictionary had been selected.
 *  
 *                                  Modifications
 *                                  1.  Modified the  ShowUserManual() method to support both 'Portable Test Unit - Software User Manual.pdf' and
 *                                      'Portable Test Equipment - Software User Manual.pdf'. - Ref.: 1.1.
 *                                  2.  Modified the HelpAbout() method to pass the correct product name when instantiating the FormHelpAbout Form. - Ref.: 1.1.
 *                                  3.  Added the 'projectIdentifier' parameter to the signature of the Login() method and used this value to call the
 *                                      WibuBoxCheckIfRequired() method. - Ref.: 2.
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using Bombardier.PTU.Forms;
using Bombardier.PTU.Properties;
using Common;
using Common.Configuration;
using WibuKey;

namespace Bombardier.PTU
{
    /// <summary>
    /// Methods called by the menu options associated with the main application - PTU.exe.
    /// </summary>
    public class MenuInterfaceApplication : MenuInterface
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="mainWindow">Reference to the main application window interface.</param>
        public MenuInterfaceApplication(IMainWindow mainWindow) : base(mainWindow)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        #region - [File] -
        /// <summary>
        /// Calls the method which asks the user to specify an XML data dictionary and then loads this into the PTU application.
        /// </summary>
        public void OpenDataDictionary()
        {
            Security security = new Security();

            #region - [Mode and SecurityLevel Checks] -
            // -------------------------------------------------------------------------------------------
            // The user must be in either configuration or setup mode in order to set the data dictionary.
            // -------------------------------------------------------------------------------------------
            if ((MainWindow.Mode == Mode.Offline) || (MainWindow.Mode == Mode.Online))
            {
                MessageBox.Show(string.Format(Resources.MBTSetDataDictionaryNotAllowed, MainWindow.Mode.ToString().ToLower(),
                                              security.GetSecurityDescription(security.SecurityLevelHighest).ToLower()),
                                Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (MainWindow.Mode == Mode.SelfTest)
            {
                MessageBox.Show(string.Format(Resources.MBTSetDataDictionaryNotAllowedSelfTest, MainWindow.Mode.ToString().ToLower(),
                                              security.GetSecurityDescription(security.SecurityLevelHighest).ToLower()),
                                Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // --------------------------------------------------------------------------------------------
            // The user must be logged into the highest security level in order to set the data dictionary.
            // --------------------------------------------------------------------------------------------
            if (security.SecurityLevelCurrent < security.SecurityLevelHighest)
            {
                MessageBox.Show(string.Format(Resources.MBTUnauthorizedSetDataDictionary, security.GetSecurityDescription(security.SecurityLevelHighest).ToLower()),
                                Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion - [Mode and SecurityLevel Checks] -

            MainWindow.Cursor = Cursors.WaitCursor;

            try
            {
                General.LoadDataDictionary(MainWindow);
            }
            catch (Exception exception)
            {
                MessageBox.Show(Resources.MBTDataDictionaryLoadFailed + CommonConstants.NewLine + CommonConstants.NewLine + exception.Message,
                                Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion - [File] -

        #region - [View] -
        /// <summary>
        /// Show the dialog box which displays the system information retrieved from the VCU.
        /// </summary>
        public void ShowSystemInformation()
        {
            MainWindow.Cursor = Cursors.WaitCursor;
            try
            {
                FormShowSystemInformation formShowSystemInformation = new FormShowSystemInformation(MainWindow.CommunicationInterface);
                MainWindow.ShowDialog(formShowSystemInformation);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion - [View] -

        #region - [Configure] -
        /// <summary>
        /// Show the dialog box which allows the user to configure the VCU real time clock.
        /// </summary>
        public void ConfigureRealTimeClock()
        {
            MainWindow.Cursor = Cursors.WaitCursor;

            try
            {
                FormConfigureRealTimeClock formConfigureRealTimeClock = new FormConfigureRealTimeClock(MainWindow.CommunicationInterface);
                DialogResult dialogResult = MainWindow.ShowDialog(formConfigureRealTimeClock);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show(Resources.MBTDateTimeSetSuccess, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Show the form which allows the user to configure paswords.
        /// </summary>
        public void ConfigurePasswordProtection()
        {
            MainWindow.Cursor = Cursors.WaitCursor;
            try
            {
                FormConfigurePasswordProtection formConfigurePasswordProtection = new FormConfigurePasswordProtection();
                DialogResult dialogResult = MainWindow.ShowDialog(formConfigurePasswordProtection);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show(Resources.MBTSecuritySuccess, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (InvalidOperationException invalidOperationException)
            {
                MessageBox.Show(invalidOperationException.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion - [Configure] -

        #region - [Tools] -
        /// <summary>
        /// Calls the method which asks the user to specify an Access engineering data dictionary database (.e1) and then converts it into an XML file.
        /// </summary>
        public void ConvertEngineeringDatabase()
        {
            MainWindow.Cursor = Cursors.WaitCursor;
            try
            {
                General.ConvertEngineeringDatabaseToXML();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Show the form which allows the user to configure user options.
        /// </summary>
        public void Options()
        {
            MainWindow.Cursor = Cursors.WaitCursor;
            try
            {
                FormOptions formOptions = new FormOptions();
                MainWindow.ShowDialog(formOptions);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion - [Tools] -

        #region - [Help] -
        /// <summary>
        /// Show the PTU user manual.
        /// </summary>
        public void ShowUserManual()
        {
            // Determine the filename of the Software User Manual associated with this project.
            string filenameSoftwareUserManual = string.Empty;
            switch (Parameter.ProjectInformation.ProjectIdentifier)
            {
                case CommonConstants.ProjectIdNYCT:
                    filenameSoftwareUserManual = Resources.FilenameSoftwareUserManualPTE;
                    break;
                default:
                    filenameSoftwareUserManual = Resources.FilenameSoftwareUserManualPTU;
                    break;
            }

            // Check that the help document exists.
            string fullyQualifiedFilenameSoftwareUserManual = DirectoryManager.PathPTUApplicationStartup + Resources.PathRelativeDocumentation + CommonConstants.BindingFilename +
                                                              Parameter.ProjectInformation.ProjectIdentifier + CommonConstants.Period + filenameSoftwareUserManual;

            FileInfo helpDocumentFileInfo = new FileInfo(fullyQualifiedFilenameSoftwareUserManual);
            if (helpDocumentFileInfo.Exists)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = Settings.Default.PathBrowser;
                processStartInfo.Arguments = fullyQualifiedFilenameSoftwareUserManual;

                //Call the Process.Start method to open the PDF file in the specified browser.
                try
                {
                    System.Diagnostics.Process.Start(processStartInfo);
                }
                catch (Exception)
                {
                    MessageBox.Show(Resources.MBTShowHelpFileFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(Resources.MBTHelpFileNotFound, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Show the splash screen.
        /// </summary>
        public void HelpAbout()
        {
            // Determine the filename of the help document associated with this project.
            string productName;
            switch (Parameter.ProjectInformation.ProjectIdentifier)
            {
                case CommonConstants.ProjectIdNYCT:
                    productName = Resources.ProductNamePTE;
                    break;
                default:
                    productName = Resources.ProductNamePTU;
                    break;
            }

            MainWindow.Cursor = Cursors.WaitCursor;
            try
            {
                FormHelpAbout formHelpAbout = new FormHelpAbout(productName);
                MainWindow.ShowDialog(formHelpAbout);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion - [Help] -

        #region - [Login] -
        /// <summary>
        /// Show the form which allows the user to log in, if currently logged out, and vice-versa.
        /// </summary>
        /// <param name="projectIdentifier">The project-identifier of the active project.</param>
        public void Login(string projectIdentifier)
        {
            MainWindow.Cursor = Cursors.WaitCursor;

            // If the user is currently logged out, display the login screen, else log the user out.
            Security security = new Security();
            if (security.SecurityLevelCurrent <= security.SecurityLevelBase)
            {
                try
                {
                    // ----------------------------------------------------
                    // Check whether a WibuBox security device is required.
                    // ----------------------------------------------------
                    if (WibuBoxCheckIfRequired(projectIdentifier) == true)
                    {
                        try
                        {
                            // Yes, check that a valid WibuBox is attached.
                            MenuInterfaceWibuKey menuInterfaceWibuKey = new MenuInterfaceWibuKey(MainWindow);
                            if (menuInterfaceWibuKey.WibuBoxCheckForValidEntry(false) == true)
                            {
                                FormLogin formLogin = new FormLogin();
                                MainWindow.ShowDialog(formLogin);
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(Resources.MBTWibuKeyDeviceDriverNotInstalled + CommonConstants.NewPara + Resources.MBTWibuKeyInstall,
                                            Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        FormLogin formLogin = new FormLogin();
                        MainWindow.ShowDialog(formLogin);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    MainWindow.Cursor = Cursors.Default;
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(Resources.MBTSecurityQueryLogOut, Resources.MBCaptionInformation, MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    // Update the security clearance.
                    security.SecurityLevelCurrent = security.SecurityLevelBase;
                    MainWindow.ShowSecurityLevelChange(security);
                }
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion - [Login] -
        #endregion --- Methods ---
    }
}
