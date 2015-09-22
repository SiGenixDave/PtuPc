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
 *  File name:  MenuInterface.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/18/10    1.1     K.McD           1.  Added the self-test menu options.
 * 
 *  10/06/10    1.2     K.McD           1.  Added a number of Debug.Assert statements.
 * 
 *  10/29/10    1.3     K.McD           1.  Added the MainWindow property.
 *                                      2.  Removed all virtual methods.
 * 
 *  12/01/10    1.4     K.McD           1.  Added the OpenDataStreamFile() method.
 * 
 *  01/06/11    1.5     K.McD           1.  Renamed the OpenDataStreamFile() method to ShowDataStream().
 *                                      2.  Modifications resulting from the name changes associated with a number of resources.
 *                                      3.  Modified the ShowDataStream() method to copy the file name of the selected data stream file to the Filename property of the 
 *                                          WatchFile_t structure.
 * 
 *  01/27/11    1.6     K.McD           1.  Bug fix - SNCR001.88/89. Modified the ShowDataStreamFile() method to return a flag to indicate whether the selected data 
 *                                          stream file was valid.
 * 
 *  02/03/11    1.7     K.McD           1.  Removed the try catch block for an InvalidDataException when calling the FileHandling.Load() method from within the 
 *                                          ShowDataStreamFile() method as this is no longer appropriate.
 * 
 *  02/28/11    1.8     K.McD           1.  Auto-modified as a result of a number of class name changes.
 * 
 *  03/28/11    1.9     K.McD           1.  Modified the name of a number of local variables.
 * 
 *  04/21/11    1.10    K.McD           1.  Added the PauseCommunication() method. This method is identical to the method in the FormPTU class and will suspend 
 *                                          polling of the VCU by any multiple document child forms that may be running.
 *                                          
 *  08/24/11    1.11    K.McD           1.  Modified the PauseCommunication() method to to accommodate the changes to the signature of the IPollTarget.SetPauseAndWait() 
 *                                          method.
 *                                          
 *  10/01/11    1.11.1  K.McD           1.  Initialized the FullFilename property of the WatchFile_t structure in the ShowDataStreamFile() method.
 *  
 *  07/04/13    1.11.2  K.McD           1.  Added support for a WibuBox security device.
 *                                          1.  Added the following protected, static flags: m_WibuBoxIsRequired, m_WibuBoxDevelopment.
 *                                          2.  Added the following protected, static integer: m_FirmCode.
 *                                          3.  Added th following protected, static constants: FirmCodeR8PR.
 *                                          4.  Added the WibuBoxCheckIfRequired() method.
 *                                          
 *  08/07/13    1.11.3  K.McD           1.  In those methods where it is applicable, the cursor was set to the wait cursor after the call to the 
 *                                          MainWindow.CloseChildForms() method as if any child forms are open, the cursor may be set to the default cursor as part of
 *                                          the call to the Exit() method within the child form.
 *                                          
 *  07/24/15    1.12    K.McD           References         
 *                                      1.  An informal review of version 6.11 of the PTU concluded that, where possible - i.e. if the PTU is started from a shortcut
 *                                          that passes the project identifier as a shortcut parameter, the project specific PTU initialization should be carried out
 *                                          in the MDI Form contructor that has the parameter string array as its signature rather than by the LoadDictionary() method.
 *                                          This streamlines the display construction of the Control Panel associated with the R188 project. In the 6.11 implementation
 *                                          the CTA layout is momentarily displayed before the Control Panel is drawn, however by initializing the project specific
 *                                          features in the constructor the Control Panel associated with the R188 project is drawn immediately and the CTA layout
 *                                          is not shown at all.
 *  
 *                                      Modifications
 *                                      1.  Modified both the WibuBoxCheckIfRemoved() and WibuBoxCheckForValidEntry() methods associated with the MenuInterfaceWibuKey
 *                                          child class so that they did not rely upon the Parameter class for the WibuBox UserCode and SlotId. This allows them to be
 *                                          used in the constructor of the MdiPTUMultiple Document Interface Form before the Parameter class is initialized.
 *                                          
 *                                          The UserCode and SlotId for the WibuBox are now be derived from member variables that are initialized from constants 
 *                                          by the WibuBoxCheckIfRequired() method, much like the WibuBox FirmCode.
 *                                          
 *                                          1.  Added the constants: UserCodeR8PR, SlotIdentifierR8PR.
 *                                          2.  Added the static member variables: m_UserCode, m_SlotId.
 *                                          3.  Modified the WibuBoxCheckIfRequired() method to initialize the m_UserCode and m_SlotId variables as well as the
 *                                              m_FirmCode variable.
 *                                              
 *                                      2.  Renamed m_WibuBoxDevelopment to m_WibuBoxIsDevelopment.
 *                                      3.  Renamed m_WibuBoxIsRequired to m_WibuBoxIsInitialized                                        
 */

/*
 *  08/12/15    1.13    K.McD           References
 *                                      1.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 28. If the project requires a Wibu-Key and the files ‘Configuration.xml’ and
 *                                          ‘project-identifier.Configuration.xml’ do not exist, there is a problem trying to log on as a Factory user in order to
 *                                          select the required data dictionary file.
 *                                          
 *                                          As the project-identifier is now passed as a desktop shortcut parameter, the Wibu-Key timer is initialized in the MDI
 *                                          constructor, if required; as soon as the user tries to log on they are automatically logged out as the initialized timer
 *                                          calls the WibuBoxCheckIfRemoved() method which returns a value of true because the FormLogin Form is instantiated without
 *                                          first calling the WibuBoxCheckForValidEntry() method as the Parameter.ProjectInformation.ProjectIdentifier parameter used
 *                                          in the call to WibuBoxCheckIfRequires() is still set to string.Empty at that stage as no data dictionary had been selected.
 *                                      
 *                                      Modifications
 *                                      1.  Modified the WibuBoxCheckIfRequired() method to assert the static 'm_WibuBoxIsInitialized' flag if the 
 *                                          method is called and to return true if the current project requires a Wibu-Key; otherwise, returns false. 
 */
#endregion --- Revision History ---

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Common.Communication;
using Common.Configuration;
using Common.Properties;

namespace Common
{
    /// <summary>
    /// Base class for the menu interface.
    /// </summary>
    public class MenuInterface
    {
        #region --- Constants ---
        #region --- WibuBox Codes ---
        /// <summary>
        /// The WibuKey Firm code associated with the NYCT - R188 Propulsion Car Control Unit project. Value: 3651. 
        /// </summary>
        protected const int FirmCodeR8PR = 3651;

        /// <summary>
        /// The WibuKey User code associated with the NYCT - R188 Propulsion Car Control Unit project. Value: 11513069. 
        /// </summary>
        protected const int UserCodeR8PR = 11513069;

        /// <summary>
        /// The WibuKey Slot Identifier associated with the NYCT - R188 Propulsion Car Control Unit project. Value: 4. 
        /// </summary>
        protected const int SlotIdentifierR8PR = 4;
        #endregion --- WibuBox Codes ---
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The watch file that has been de-serialized from disk.
        /// </summary>
        protected WatchFile_t m_WatchFile;

        /// <summary>
        /// Reference to the main application window interface.
        /// </summary>
        private IMainWindow m_MainWindow;

        /// <summary>
        /// A flag to indicate whether the WibuBox security device has been initialized. True, if the WibuBox device has been initialized; otherwise, false.
        /// </summary>
        protected static bool m_WibuBoxIsInitialized = false;

        /// <summary>
        /// A flag to indicate that the WibuBox that was found is a development WibuBox dongle, not the client WibuBox. 
        /// True, if the current WibuBox is a development WibuBox; otherwise, false.
        /// </summary>
        protected static bool m_WibuBoxIsDevelopment = false;

        /// <summary>
        /// The WibuKey Firm Code associated with the current project.
        /// </summary>
        protected static int m_FirmCode;

        /// <summary>
        /// The WibuKey User Code associated with the current project.
        /// </summary>
        protected static int m_UserCode;

        /// <summary>
        /// The WibuKey Slot Identifier associated with the current project.
        /// </summary>
        protected static int m_SlotId;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="mainWindow">Reference to the main application window interface.</param>
        public MenuInterface(IMainWindow mainWindow)
        {
            m_MainWindow = mainWindow;
            Debug.Assert(m_MainWindow != null);
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Ask the user to select a data-stream file, de-serialized this to the appropriate object type and then display the data using the appropriate  
        /// <c>FormDataStreamPlot</c> derived class.
        /// </summary>
        /// <param name="title">The title that is to appear on the <c>OpenFileDialog</c> form.</param>
        /// <param name="defaultExtension">The default extension associated with the type of log.</param>
        /// <param name="filterText">The filter text. Used to filter the list of available files.</param>
        /// <param name="initialDirectory">The initial directory that will be show.</param>
        /// <returns>A flag to indicate whether a valid watch file was selected. True, indicates that the selected file was valid; otherwise, false.</returns>
        public virtual bool ShowDataStreamFile(string title, string defaultExtension, string filterText, string initialDirectory)
        {
            // Default to the selected file being invalid.
            bool selectedFileIsValid = false;

            MainWindow.CloseChildForms();
            MainWindow.Cursor = Cursors.WaitCursor;

            string fullFilename = General.FileDialogOpenFile(title, defaultExtension, filterText, initialDirectory);

            // Skip, if the user didn't select a simulated fault log file.
            if (fullFilename == string.Empty)
            {
                MainWindow.Cursor = Cursors.Default;
                return selectedFileIsValid;
            }

            // Update the appropriate InitialDirectory property with the path of the specified data stream file.
            switch (defaultExtension)
            {
                case CommonConstants.ExtensionFaultLog:
                    InitialDirectory.FaultLogsRead = Path.GetDirectoryName(fullFilename);
                    break;
                case CommonConstants.ExtensionSimulatedFaultLog:
                    InitialDirectory.SimulatedFaultLogsRead = Path.GetDirectoryName(fullFilename);
                    break;
                case CommonConstants.ExtensionWatchFile:
                    InitialDirectory.WatchFilesRead = Path.GetDirectoryName(fullFilename);
                    break;
                default:
                    break;
            }

            // De-serialize the selected file.
            m_WatchFile = FileHandling.Load<WatchFile_t>(fullFilename, FileHandling.FormatType.Binary);
            
            // Ensure that the de-serialized file contains data.
            if (m_WatchFile.DataStream.WatchFrameList == null)
            {
                // File format is not recognised, report message.
                MessageBox.Show(Resources.MBTInvalidFormat, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainWindow.Cursor = Cursors.Default;
                return selectedFileIsValid;
            }

            // Ensure that the selected log file is associated with the current project.
            if (m_WatchFile.Header.ProjectInformation.ProjectIdentifier != Parameter.ProjectInformation.ProjectIdentifier)
            {
                MessageBox.Show(Resources.MBTProjectIdMismatch, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainWindow.Cursor = Cursors.Default;
                return selectedFileIsValid;
            }

            FileInfo fileInfo = new FileInfo(fullFilename);
            m_WatchFile.Filename = fileInfo.Name;
            m_WatchFile.FullFilename = fileInfo.FullName;

            selectedFileIsValid = true;
            MainWindow.Cursor = Cursors.Default;
            return selectedFileIsValid;
        }

        /// <summary>
        /// Suspend or resume polling of the vehicle control unit by the multiple document interface child form that is currently being displayed.
        /// </summary>
        /// <remarks>
        /// This method will only suspend or resume polling if the dialog box is called from the main application window. If called from another dialog box, it is assumed 
        /// that the calling dialog box will have taken care of suspending/resuming the polling.
        /// </remarks>
        /// <typeparam name="T">The communication interface type.</typeparam>
        /// <param name="communicationInterface">Reference to the communication interface.</param>
        /// <param name="pause">A flag to control whether polling of the VCU by the multiple document interface child form is to be resumed or suspended. True, 
        /// suspends polling of the VCU; false, resumes polling of the VCU.</param>
        protected void PauseCommunication<T>(T communicationInterface, bool pause) where T : ICommunicationParent
        {
            // Check that the specified communication interface has been initialized.
            if (communicationInterface != null)
            {
                // Only suspend polling if the dialog box was called from the main application window. If called from another dialog box, assume that the calling
                // dialog box has taken care of suspending the polling.
                if (MainWindow != null)
                {
                    // Check whether any multiple document interface child forms are running.
                    if (MainWindow.MdiChildren.Length > 0)
                    {
                        // Check which of the multiple document interface child forms implement the IPollTarget interface i.e. actually poll the VCU.
                        for (int child = 0; child < MainWindow.MdiChildren.Length; child++)
                        {
                            if (MainWindow.MdiChildren[child] as IPollTarget != null)
                            {
                                if (pause == false)
                                {
                                    // Resume polling - Clear the pause property.
                                    (MainWindow.MdiChildren[child] as IPollTarget).Pause = false;
                                    MainWindow.WriteStatusMessage(string.Empty);
                                }
                                else
                                {
                                    // Pause polling - Call the SetPauseAndWait() method.
                                    (MainWindow.MdiChildren[child] as IPollTarget).SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback);
                                    MainWindow.WriteStatusMessage(Resources.SMPaused);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check whether the specified project requires a WibuBox device.
        /// </summary>
        /// <param name="projectIdentifier">The current project identifier.</param>
        /// <returns>A flag to indicate whether the specified project requires a WibuBox security device. True, if the project requires a WibuBox device; otherwise,
        /// false.</returns>
        public bool WibuBoxCheckIfRequired(string projectIdentifier)
        {
            bool wibuBoxIsRequired = false;

            switch (projectIdentifier)
            {
                case CommonConstants.ProjectIdNYCT:
                    m_FirmCode = FirmCodeR8PR;
                    m_UserCode = UserCodeR8PR;
                    m_SlotId = SlotIdentifierR8PR;
                    wibuBoxIsRequired = true;
                    break;

                case CommonConstants.ProjectIdCTA:
                    break;

                default:
                    break;
            }

            m_WibuBoxIsInitialized = true;

            return wibuBoxIsRequired;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the reference to the main application window.
        /// </summary>
        protected IMainWindow MainWindow
        {
            get { return m_MainWindow; }
        }
        #endregion --- Properties ---
    }
}