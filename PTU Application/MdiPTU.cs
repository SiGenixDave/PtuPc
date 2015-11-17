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
 *  File name:  MdiPTU.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  03/22/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/16/10    1.1     K.McD           1.  Bug fix SNCR 001.009. If the user exits the replay screen directly rather than returning back via the YT plot screen, 
 *                                          the function keys are not restored correctly. Added the ToolStripItemCollectionMainWindow property.
 * 
 *  08/18/10    1.2     K.McD           1.  Added support for the self-test menu options.
 * 
 *  09/30/10    1.3     K.McD           1.  Added support fot the 'Configuration/Enumeration' menu option.
 *                                      2.  Changed the name of a number of resource references.
 * 
 *  10/11/10    1.4     K.McD           1.  Bug fix SNCR001.27. Ensure that any menu options that are not yet implemented display a message box informing the user of
 *                                          their current status.
 * 
 *  10/15/10    1.5     K.McD           1.  Modified to use the CommunicationParent class rather than the ICommunication interface.
 * 
 *  11/02/10    1.6     K.McD           1.  Added support for the event menu interface.
 * 
 *  11/26/10    1.7     K.McD           1.  Removed the event handler for the 'Configure/Worksets' menu option.       
 *                                      2.  Added the event handler for the 'Configure/Worksets/Fault Log' menu option.
 *                                      3.  Added the event handler for the 'Configure/Worksets/Watch Window' menu option.
 * 
 *  01/06/11    1.8     K.McD           1.  Initialized the data dictionary filename to the default value - 'PTU Configuration.xml'.
 *                                      2.  Report an error if the specified configuration file canot be found.
 *                                      3.  Modified the Shown() event handler to check whether the default configuration file or a specific configuration file is to
 *                                          be loaded and then verify that the file exists.
 *                                      4.  Modified the 'File/Open/Event Log' menu option event handler to include a call to the OpenEventLog() method of the event
 *                                          menu interface.
 *                                      5.  Modifications arising from the name change of the ShowEventLog() method of the event interface to ViewEventLog().
 *                                      6.  Bug fix - SNCR001.47. Modified the m_TSBOnline_Click() event handler to ensure that a check on the version number of the 
 *                                          data dictionary is carried out when connecting to the target. If a mismatch is detected the PTU will attempt to load the 
 *                                          correct data dictionary.
 *                                      7.  Modified the m_TSBOffline_Click() event handler so that it no longer writes an empty string to the car identifier status
 *                                          label, this is now updated by the SetMode() method.
 * 
 *  02/14/11    1.9     K.McD           1.  Removed any references to the ISecurity interface and replaced it with a reference to the Security class.
 *                                      2.  Included support for the ByPassVersionCheck conditional compilation symbol.
 *                                      3.  Included support for the 'Tools/Debug Mode' menu option.
 *                                      4.  Removed the 'Configure/StartRTC' and 'Configure.StopRTC' menu options.
 *                                      5.  Removed the m_ProgressBar_VisibleChanged() event handler.
 * 
 *  02/22/11    1.10    K.McD           1.  Moved the Century constant to the CommonConstants class.
 *                                      2.  Modified a number of XML tags.
 *                                      3.  Changes to accomodate the modifications to the menu system.
 * 
 *  03/21/11    1.11    K.McD           1.  Modified the code such that the Security class is not instantiated until after the data dictionary has been loaded an a 
 *                                          call the the Security.Initialize() static method has been made.
 *                                      2.  Addde support for the Windows help engine.
 *                                      3.  Auto-modified as a result of a method name change associated with the MenuInterfaceApplication class.
 *                                      4.  Included support for the form used to configure the VCU date and time.
 * 
 *  03/28/11    1.11.1  K.McD           1.  Modified the name of a number of local variables.
 * 
 *  04/27/11    1.12    K.McD           1.  Modified the event handler for the on-line button to retrieve the mode of the chart recorder.
 *                                      2.  Added support for the menu options associated with the configuration of the chart recorder and setting the mode of the 
 *                                          chart recorder.
 *                                          
 *  06/22/11    1.12.1  K.McD           1.  Added support for the self test menu interface.
 *  
 *  07/13/11    1.12.2  K.McD           1.  Modified the function keys to take into account the redefinition of off-line mode and the addition of diagnostic mode 
 *                                          as discussed in the June sprint review.
 *                                          
 *  07/24/11    1.12.3  K.McD           1.  Added support for the KeyDown and KeyUp events.
 *                                      2.  Removed support for the diagnostic mode ToolStripButton control.
 *                                      3.  Implemented a toggle function for the on-line and off-line mode keys.
 *                                      4.  Ensured that the event handlers for the on-line and off-line ToolStripButton controls are inhibited if the ToolStripButton is 
 *                                          disabled. Note: The event handler is also called if the user presses a key rather than selecting the ToolStripButton control 
 *                                          using the mouse.
 *                                      5   Ensured that the Checked properties of the chart recorder mode menu options are initialized to false before the call to the 
 *                                          GetChartMode() method in the on-line and off-line event handlers.
 *                                          
 *  09/21/11    1.12.4  Sean.D          1.	Changed code in m_TSBOnline_Click to check for a CommunicationException when trying to close the socket so that 
 *											we don't get repeated errors trying to close a port that's already closed.
 *											
 *  10/10/11    1.12.5  K.McD           1.  Included support for the 'Help/PTU Help' menu option.
 *  
 *  10/26/11    1.12.6  K.McD           1.  Auto-modified as a result of enumerator name changes. Mode.Diagnostic renamed to Mode.Configuration.
 *  
 *  07/04/13    1.12.7  K.McD           1.  Included support for the WibuBox security device.
 *                                          1.  Added the IntervalMsWibuBoxUpdate constant.
 *                                          2.  Added the m_TimerWibuBox Timer.
 *                                          3.  Added the m_MenuInterfaceWibuKey reference to the MenuInterfaceWibuKey class.
 *                                          4.  Modified the Cleanup() method to include the WibuBox timer.
 *                                          5.  Modified the Cleanup() method to set the appropriate member variables to null.
 *                                          6.  Added the event handler for the WibuBox timer Tick event to check whether the WibuBox has been removed.
 *                                          7.  Modified the Shown event to check whether a WibuBox device is required for the current project and, if so, 
 *                                              to instantiate a MenuInterfaceWibuKey class and to initialize the WibuBox timer.
 *                                              
 *  07/26/13    1.12.8  K.McD           1.  Modified the Cleanup() method to close the communication port.
 *  
 *  07/31/13    1.12.9  K.McD           1.  Added the FilenameDataDictionary property in accordance with the modified IMainWindow interface definition.
 *                                      2.  Modified the constructor that is called if any command line arguments are passed to the PTU to update the
 *                                          FilenameDataDictionary property with the filename associated with the project XML data dictionary file if a valid project
 *                                          XML data dictionary file is found.
 *                                      3.  Modified the m_TSBOnline_Click() method to copy the source file to the file that is defined as the current XML data
 *                                          dictionary file rather than the default XML data dictionary file if a new XML data dictionary file is selected as a 
 *                                          result of a mismatch between the embedded software version reference and the current data dictionary version reference.
 *                                          
 *  08/02/13    1.12.10 K.McD           1.  Bug fix - The font associated with the user message did not change when the font was modified using the Tools/Options menu.
 *                                          Modified the  MdiPTU_FontChanged() method to update the Font property of the user message StatusStrip control.
 *                                          
 *  02/27/14    1.12.11 K.McD           1.  Corrected 'Y' Location values in MdiPTU.resx for m_StatusStripCurrentSetup (4), m_StatusStripUserMessage (4), 
 *                                          m_LegendStatusInformation (6), m_LegendRx (6) and m_DigitalControlPacketReceived (9) as these 
 *                                          values had somehow been corrupted.
 *                                          
 *                                      2.  Updated the m_MenuItemHelpAboutPTU_Click() method to use the renamed m_MenuInterfaceApplication.HelpAbout() method.
 *                                      
 *  04/15/15    1.13    K.McD           References
 *                                          
 *                                      1.  Although only relevant to Bombardier Field Service Engineers that support a number of PTU projects, the Software User Manual
 *                                          and Release Notes documents are to be made project specific by prepending the project identifier to the file name e.g.
 *                                          'R8PR.Portable Test Unit - Release Notes.pdf', 'CTA.Portable Test Unit - Release Notes.pdf' etc.
 *                                          
 *                                      Modifications
 *                                      1.  Removed the member variable m_HelpDocument.
 *                                      
 *  05/13/15    1.14    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                          
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘WibuBox: [Present | Not Present]’.
 *                                              
 *                                      2.  SNCR - R188 PTU [20-Mar-2015] Item 4. If the PTU is being used in a development environment, i.e. there is a possibility of
 *                                          switching between multiple projects, and the PTU is started up using the R188 configuration but the user agrees to switch to
 *                                          the CTA configuration when an attempt is made to connect to a CTA VCU, there is a bug which results in the PTU continuing to
 *                                          check whether a Wibu Key is present.
 *                                          
 *                                      Modifications
 *                                      1.  Added the LogSaved and WibuBoxPresent flag properties and associated member variable flags. Ref.: 1.1.
 *                                      2.  Modified the MdiPTU(string[] args) constructor to assert the Visible property of the WibuBox status label if the
 *                                          project associated with the project-identifier shortcut parameter requires a WibuBox to be present. Ref.: 1.1.
 *                                      3.  Auto-Update as result of name changes to the status label controls. Ref.: 1.1.
 *                                      4.  Modified the WibuBoxCheck() method to update the WibuBox status label if the WibuBox has been removed. Ref.: 1.1.
 *                                      5.  Changed the definition of the TaskProgressBar property to ToolStripProgressBar. Ref.: 1.1.
 *                                      6.  Moved the section of code in the 'Shown' event handler that is associated with the WibuBox to the LoadDictionary()
 *                                          method. Ref.:2.
 */

/*                                         
 *  07/13/15    1.15    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                          
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. Following a conference call on 9-Jul-15,
 *                                              the current proposal is to extend the options associated with the log saved status StatusLabel to include:
 *                                              ‘[Saved | Unsaved | Unknown | Not Applicable (-)]’.
 *                                              
 *                                          2.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 2.  Addition of the Control Panel window.
 *                                          
 *                                      Modifications
 *                                      1.  Replaced the LogSaved property and associated variable with the LogStatus property and its associated variable. This property
 *                                          gets and sets the event log saved status associated with the current car. The LogStatus StatusLabel is also updated
 *                                          whenever the property is written to. This property was added to the IMainWindow interface. - Ref.: 1.1.
 *                                          
 *                                      2.  Added the CarNumber property and associated variable. This property gets the current car number if the PTU is connected to
 *                                          the target logic. If not connected to the car logic, the value that will be returned is short.MinVal. This property was added
 *                                          to the IMainWindow interface.  Ref.: 1.1.
 *                                          
 *                                      3.  Removed the set MenuStrip property and added the get and set StatusStrip property. This property was added to the IMainWindow
 *                                          interface. - Ref.: 1.2.
 *                                          
 *                                      4.  Modified the event handlers for the 'Online' and 'Offline' ToolStripButtons to only display the Watch Window when the unit
 *                                          goes online if the current project uses a control panel. - Ref.: 1.2.
 *                                          
 *                                      5.  Updated the 'Online' event handler to toggle the LogStatus value between 'Not Applicable ("-")' and 'Unknown' depending
 *                                          upon the Checked state. - Ref.: 1.1
 *                                          
 *                                      6.  Modified the Cleanup() method the include the ControlPanel UserControl. Also sets the cursor to the Cursors.WaitCursor
 *                                          value as soon as the Cleanup() method is called. - Ref.: 1.2.
 *                                          
 *                                      7. Removed the section of code relating to the WibuBox StatusLabel from the constructor. - Ref.: 1.2.
 */

/*
 *  07/28/15    1.16    K.McD           References
 *                                      1.  Part 1 of the upgrade to the Chicago 5000 PTU software that allows the user to download the configuration and help files for
 *                                          a particular Chicago 5000 vehicle control unit (VCU) via an ethernet connection to the FTP (File Transfer Protocol) server
 *                                          running on the VCU. The scope of Part 1 of the upgrade is defined in purchase order 4800011369-CU2 07.07.2015.
 *                                      
 *                                          The upgrade is implemented in two parts, the first part, Part 1, replaces the existing screens and logic with those outlined
 *                                          in slides 6, 7, 8 and 9 of the PowerPoint presentation '076_CTA - PTU file pullback from VCU - 20150127.pptx', but does NOT
 *                                          implement the file transfer; it merely calls an empty external batch file from within the PTU application. The second stage,
 *                                          Part 2, implements the batch file that downloads the configuration and help files from the Vehicle Control Unit (VCU) to the
 *                                          appropriate directory on the PTU computer. As described in the PowerPoint Presentation, this download is only carried out
 *                                          if the appropriate configuration file is not already present on the PTU computer.
 *                                      
 *                                      Modifications
 *                                      1.  Added the FTPError enumerator.
 *                                      2.  Added the Restart and ProjectIdentifierPassedAsParameter properties and associated member variables.
 *                                      3.  Removed the ToolStripProgressBar TaskProgressBar. The progress bar used to display the recording and playback of data
 *                                          streams now appears in the 'Information' Panel of the FormWatch Form. The progress bar was moved to allow the status
 *                                          message display to be extended to support some of the longer messages required to support the upgrade shown above.
 *                                      4.  Replaced the local projectIdentifier string with the m_ProjectIdentifierPassedAsParameter member variable and modified
 *                                          the error message in the MdiPTU(string[] args) constructor.
 *                                      5.  Refactored the m_TSBOnline_Click() event handler to create the CheckConfiguration() and UpdateChartMode() methods.
 *                                      6.  Modified the CheckConfiguration() method to implement Part 1 of reference 1.
 */

/*
 *  08/11/15    1.17    K.McD       References
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
 *                                  1.  Modified the m_MenuItemLogin_Click() event handler to pass the 'm_ProjectIdentifierPassedAsParameter' member variable in the
 *                                      call to the MenuInterfaceApplication.Login() method. - Ref.: 2.
 *                                      
 *                                  2.  Removed the reference to 'PTU' from the 'About PTU' and the 'PTU Help' menu options. - Ref.: 1.1. 
 *                                      
 * 
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using Bombardier.PTU.Forms;
using Bombardier.PTU.Properties;
using Common;
using Common.Communication;
using Common.Configuration;
using Event;
using SelfTest;
using Watch;
using WibuKey;

namespace Bombardier.PTU
{
    /// <summary>
    /// The error returns from the FTP command batch file.
    /// </summary>
    public enum FTPError
    {
        /// <summary>
        /// A system exception was thrown. Value: 1.
        /// </summary>
        SystemException = 1,

        /// <summary>
        /// The function call was successful i.e. there were no errors. Value: 0.
        /// </summary>
        Success = 0,

        /// <summary>
        /// There was no reply within the timeout period. Value: -10.
        /// </summary>
        TimeOut = -10,

        /// <summary>
        /// Undefined. Value: -100.
        /// </summary>
        Undefined = -100
    }
    /// <summary>
    /// Main user interface for the Portable Test Unit (PTU) application.
    /// </summary>
    public partial class MdiPTU : Form, IMainWindow
    {
        #region --- Constants ---
        /// <summary>
        /// Th interval, in ms, between successive checks to see whether the WibuBox is still attached to the system. Value: 5,000.
        /// </summary>
        private const int IntervalMsWibuBoxUpdate = 5000;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// A static flag that controls whether the PTU does an automatic restart when the main PTU application is closed. True, if the PTU is to do an automatic
        /// restart; otherwise, false.
        /// </summary>
        public static bool m_Restart = false;

        /// <summary>
        /// Flag to indicate whether the Dispose() method has been called. True indicates that the Dispose() method has been called; otherwise, false.
        /// </summary>
        private bool m_IsDisposed;

        /// <summary>
        /// The project identifier that was passed to the application as a shortcut parameter. If no shortcut parameter was passed to the
        /// application then the value is set to string.Empty.
        /// </summary>
        private string m_ProjectIdentifierPassedAsParameter = string.Empty;

        /// <summary>
        /// The timer that is used to check that the WibuKey security device is still attached to the system.
        /// </summary>
        private Timer m_TimerWibuBox;

        /// <summary>
        /// Reference to the KeyEventArgs object associated with the last recorded key press. 
        /// </summary>
        private KeyEventArgs m_KeyEventArgs;

        /// <summary>
        /// The <c>DataSet</c> used to store the information loaded from the XML data dictionary.
        /// </summary>
        private DataDictionary m_DataDictionary;

        /// <summary>
        /// The filename of the XML data dictionary file.
        /// </summary>
        private string m_FilenameDataDictionary = Resources.FilenameDefaultDataDictionary;

        /// <summary>
        /// The current mode of operation: setup, online, diagnostic or offline.
        /// </summary>
        private Mode m_Mode;

        /// <summary>
        /// Reference to the <c>Security</c> class associated with the PTU.
        /// </summary>
        private Security m_Security;

        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        private ICommunicationParent m_CommunicationInterface;

        /// <summary>
        /// Reference to the class which calls the menu options associated with the main PTU application. 
        /// </summary>
        private MenuInterfaceApplication m_MenuInterfaceApplication;

        /// <summary>
        /// Reference to the class which calls the menu options associated with the subsystem which shows and records the watch variables - Watch.dll.
        /// </summary>
        private MenuInterfaceWatch m_MenuInterfaceWatch;

        /// <summary>
        /// Reference to the class which calls the menu options associated with the events subsystem - Events.dll.
        /// </summary>
        private MenuInterfaceEvent m_MenuInterfaceEvent;

        /// <summary>
        /// Reference to the class which calls the menu options associated with the self-test subsystem - SelfTest.dll.
        /// </summary>
        private MenuInterfaceSelfTest m_MenuInterfaceSelfTest;
 
        /// <summary>
        /// Reference to the class which calls the menu options associated with the WibuKey subsystem - WibuKey.dll.
        /// </summary>
        private MenuInterfaceWibuKey m_MenuInterfaceWibuKey; 

        /// <summary>
        /// The collection of function keys associated with the form. This allows any child form that is called indirectly to restore the function keys 
        /// on exit.
        /// </summary>
        private ToolStripItemCollection m_ToolStripItemCollectionMainWindow;

        /// <summary>
        /// Flag to control whether the enumarator watch variables are to have their values displayed as the enumerated text value or the actual numeric value. True, 
        /// displays the value as enumerated text; false, displays the values as numeric data.
        /// </summary>
        private bool m_Enumeration = true;

        /// <summary>
        /// Flag to control whether logging of the parameter values associated with the calls to those methods within the PTUDLL32 dynamic link library is enabled. 
        /// True, to enable debug mode; otherwise, false.
        /// </summary>
        private bool m_DebugMode = false;

        /// <summary>
        /// Flag to control whether the animation showing that the PTU is busy processing data is visible. True, to show the animation; otherwise, false.
        /// </summary>
        private bool m_ShowBusyAnimation = false;

        /// <summary>
        /// The event log saved saved associated with the current car.
        /// </summary>
        private EventLogSavedStatus m_EventLogSavedStatus = EventLogSavedStatus.Undefined;

        /// <summary>
        /// Flag to indicate whether a WibuBox security device is present or not. True, if a WibuBox security device is present; otherwise, false.
        /// </summary>
        private bool m_WibuBoxPresent = false;

        /// <summary>
        /// The current car number, if the PTU is connected to the target logic; otherwise, if not connected to the car logic, the vale that is returned is
        /// short.MinVal.
        /// </summary>
        private short m_CarNumber = short.MinValue;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. This constructor is called if one or more shortcut parameters are passed to the PTU by the desktop shortcut.
        /// </summary>
        /// <param name="args">An array of the parameter strings that were passed to the PTU by the desktop shortcut. Note: only one parameter, arg[0], should be passed
        /// and this must specify the project identifier e.g. "CTPA", "R8PR" etc</param>
        public MdiPTU(string[] args) : this()
        {
            // Check whether a project identifier parameter was passed by the desktop shortcut.
            if (args.Length == 1)
            {
                // Yes, check whether a XML data dictionary file for the project exists. This takes the format '<project-identifier>.PTU Configuration.xml'.
                m_ProjectIdentifierPassedAsParameter = args[0];

                InitializePTUProjectSpecific(m_ProjectIdentifierPassedAsParameter);

                string fullFilename = DirectoryManager.PathPTUConfigurationFiles + CommonConstants.BindingDirectory + m_ProjectIdentifierPassedAsParameter +
                                      CommonConstants.Period + Resources.FilenameDefaultDataDictionary;
                FileInfo fileInfo = new FileInfo(fullFilename);
                if (fileInfo.Exists)
                {
                    // The file exists, update the XML data dictionary filename property to specify the project XML data dictionary filename rather than the default XML
                    // data dictionary filename.
                    m_FilenameDataDictionary = m_ProjectIdentifierPassedAsParameter + CommonConstants.Period + Resources.FilenameDefaultDataDictionary;

                }
                else
                {
                    // Inform the user that the specified project XML data dictionary file does not exist.
                    MessageBox.Show(string.Format(Resources.MBTConfigProjectNotFound, Resources.FilenameDefaultDataDictionary), Resources.MBCaptionWarning,
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if ((args.Length > 1) || (args.Length < 0))
            {
                // Inform the user that an invalid number of parameters were passed by the desktop shortcut.
                MessageBox.Show(string.Format(Resources.MBTShortcutParameterCountInvalid, Resources.FilenameDefaultDataDictionary),
                                Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Initialises a new instance of the class. 
        /// </summary>
        public MdiPTU()
        {
            InitializeComponent();
            
            InitializePTU();
        }
        #endregion  --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Cleanup(bool disposing)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // Provided that the form isn't minimized, update the WindowState setting.
                if (this.WindowState != FormWindowState.Minimized)
                {
                    Settings.Default.WindowState = this.WindowState;
                    Settings.Default.Save();
                }

                // If the WindowState property is normal, update the: Location and Size settings.
                if (this.WindowState == FormWindowState.Normal)
                {
                    Settings.Default.FormLocation = this.Location;
                    Settings.Default.FormSize = this.Size;
                    Settings.Default.Save();
                }

                CloseChildForms();
                DebugMode.Close();
                WinHlp32.Close(this.Handle.ToInt32());

                // Ensure that the communication port is closed.
                if (m_CommunicationInterface != null)
                {
                    CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                }

                if (disposing)
                {
                    // Method called by consumer code. Call the Dispose method of any managed data members that implement the dispose method.
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    if (m_DataDictionary != null)
                    {
                        m_DataDictionary.Dispose();
                    }

                    if (m_ControlPanel != null)
                    {
                        m_ControlPanel.Dispose();
                    }

                    if (m_TimerWibuBox != null)
                    {
                        m_TimerWibuBox.Stop();
                        m_TimerWibuBox.Enabled = false;
                        m_TimerWibuBox.Tick -= new EventHandler(WibuBoxCheck);
                        m_TimerWibuBox.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data 
                // members to null.
                m_DataDictionary = null;
                m_TimerWibuBox = null;
                m_ControlPanel = null;
                m_CommunicationInterface = null;
                m_MenuInterfaceApplication = null;
                m_MenuInterfaceEvent = null;
                m_MenuInterfaceSelfTest = null;
                m_MenuInterfaceWatch = null;
                m_MenuInterfaceWibuKey = null;
                m_Security = null;
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception is not thrown.
            }
            this.Cursor = Cursors.Default;
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        #region - [Form] -
        /// <summary>
        /// Event handler for the form 'Shown' event. Loads the PTU configuration file and initializes the communication interface.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void MdiPTU_Shown(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            this.Update();
            Cursor = Cursors.WaitCursor;

            // ----------------------------------
            // Load the XML data dictionary file.
            // ----------------------------------
            m_DataDictionary = new DataDictionary();

            // Check whether the default XML configuration file is to be used.
            if (m_FilenameDataDictionary == Resources.FilenameDefaultDataDictionary)
            {
                // Yes - Check whether the default configuration file exists.
                string fullFilename = DirectoryManager.PathPTUConfigurationFiles + @"\" + m_FilenameDataDictionary;
                FileInfo fileInfo = new FileInfo(fullFilename);
                if (fileInfo.Exists == false)
                {
                    MessageBox.Show(string.Format(Resources.MBTConfigDefaultNotFound, Resources.FilenameDefaultDataDictionary), Resources.MBCaptionWarning,
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Security.Initialize();
                    m_Security = new Security();
                    ShowSecurityLevelChange(m_Security);
                    Cursor = Cursors.Default;
                    SetMode(Mode.Setup);
                    return;
                }
            }
            
            // Read the XML configuration file.
            try
            {
                // If the XML file hasn't been updated to include the YearCodeSize field of the CONFIGUREPTU table, the other fields of the table are still
                // read in correctly. If an attempt is made to access 'm_DataDictionary.CONFIGUREPTU[0].YearCodeSize' an exception is thrown.
                m_DataDictionary.ReadXml(DirectoryManager.PathPTUConfigurationFiles + CommonConstants.BindingFilename + m_FilenameDataDictionary);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Resources.MBTConfigInvalid, m_FilenameDataDictionary) + CommonConstants.Space + Resources.MBTConfigReselect,
                                              Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Security.Initialize();
                m_Security = new Security();
                ShowSecurityLevelChange(m_Security);
                Cursor = Cursors.Default;
                SetMode(Mode.Setup);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            Cursor = Cursors.WaitCursor;
            LoadDictionary(m_DataDictionary);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the form 'ResizeEnd' event. Only update the FormLocation and FormSize settings if the window is in the normal state.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void MdiPTU_ResizeEnd(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.FormSize = this.Size;
                Settings.Default.FormLocation = this.Location;
                Settings.Default.Save();
            }
        }
        #endregion  - [Form] -

        #region - Key Events -
        /// <summary>
        /// Event handler for the <c>KeyDown</c> event. Maps the Function keys to the <c>ToolStrip</c> buttons.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void MdiPTU_KeyDown(object sender, KeyEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    break;
                case Keys.F1:
                    break;
                case Keys.F2:
                    m_TSBOnline_Click(sender, e);
                    break;
                case Keys.F3:
                    m_TSBOffline_Click(sender, e);
                    break;
                default:
                    break;
            }
            // Keep a record of the KeyEventArgs object.
            m_KeyEventArgs = e;
        }

        /// <summary>
        /// Event handler for the <c>KeyUp</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void MdiPTU_KeyUp(object sender, KeyEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Update the KeyEventArgs object.
            m_KeyEventArgs = null;
        }
        #endregion - Key Events -

        #region - [Menu Options] -
        #region - [FILE] -
        /// <summary>
        /// Event handler for the 'File/Open - Data File/Recorded Watch File' menu option. Dispatch to the appropriate menu interface method.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileOpenWatchFile_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceWatch.OpenRecordedWatchFile();
        }

        /// <summary>
        /// Event handler for the 'File/Open - Data File/Simulated Fault Log' menu option. Dispatch to the appropriate menu interface method.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileOpenSimulatedFaultLog_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceWatch.OpenSimulatedFaultLog();
        }

        /// <summary>
        /// Event handler for the 'File/Open - Data File/Fault Log' menu option <c>Click</c> event. Dispatch to the appropriate menu interface method.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileOpenFaultLog_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceEvent.OpenFaultLog();
        }

        /// <summary>
        /// Event handler for the 'File/Open - Data File/Event Log' menu option <c>Click</c> event. Dispatch to the appropriate menu interface method.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileOpenEventLog_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceEvent.OpenEventLog();
        }

        /// <summary>
        /// Event handler for the 'File/Open - Data File/Screen Capture' menu option <c>Click</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileOpenScreenCapture_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.MBTNotYetImplemented, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler for the 'File/Select Data Dictionary' menu option. Allows the user to select a new data dictionary.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileOpenDataDictionary_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceApplication.OpenDataDictionary();
        }

        /// <summary>
        /// Event handler for the 'File/Save/Fault Logs' menu option <c>Click</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileSaveFaultLogs_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.MBTNotYetImplemented, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler for the 'File/Save/Event Log' menu option <c>Click</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileSaveEventLog_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.MBTNotYetImplemented, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler for the 'File/Save All' menu option <c>Click</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileSaveAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.MBTNotYetImplemented, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler for the 'File/Exit' menu option. Closes the PTU application.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion - [FILE] -

        #region - [VIEW] -
        /// <summary>
        /// Event handler for the 'View/Watch Window' menu option. Show the child form which displays the watch variables defined by the current workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemWatchViewWatchWindow_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceWatch.ViewWatchWindow();
        }

        /// <summary>
        /// Event handler for the 'View/System Information' menu option. Shows the dialog box which displays the system information retrieved from the VCU.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemViewSystemInformation_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceApplication.ShowSystemInformation();
        }
        #endregion - [VIEW] -

        #region - [DIAGNOSTICS] -
        /// <summary>
        /// Event handler for the 'Diagnostics/Self-Test' menu option. Allows the user to run the self tests.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemDiagnosticsSelfTests_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceSelfTest.ConfigureSelfTests();
        }

        /// <summary>
        /// Event handler for the 'Diagnostics/Event Log' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemDiagnosticsEventLog_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceEvent.ViewEventLog();
        }

        /// <summary>
        /// Event handler for the 'Diagnostics/Annunciators' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemDiagnosticsAnnunciators_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.MBTNotYetImplemented, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler for the 'Diagnostics/Macros' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemDiagnosticsMacros_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.MBTNotYetImplemented, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler for the 'Diagnostics/Initialize Event Logger' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemDiagnosticsInitializeEventLogs_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceEvent.InitializeEventLogs();
        }

        #endregion - [DIAGNOSTICS] -

        #region - [CONFIGURE] -
        #region - [Worksets] -
        /// <summary>
        /// Event handler for the 'Configure/Worksets/Watch Window' menu option. Instantiate and show the FormWorksetManager dialog box. This allows the user to
        /// manage the worksets associated with viewing and recording watch variables.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureWorksetsWatchWindow_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceWatch.ConfigureWorksetsWatchWindow();
        }

        /// <summary>
        /// Event handler for the 'Configure/Worksets/Fault Log' menu option. Instantiate and show the FormWorksetManagerFaultLog dialog box. This allows the user to
        /// manage the worksets associated with the fault log data stream.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureWorksetsFaultLog_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceEvent.ConfigureWorksetsFaultLog();
        }

        /// <summary>
        /// Event handler for the 'Configure/Worksets/Chart Recorder' menu option. Instantiate and show the FormWorksetManagerChartRecorder dialog box. This allows 
        /// the user to manage the worksets associated with the chart recorder.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureWorksetsChartRecorder_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceWatch.ConfigureWorksetsChartRecorder();
        }
        #endregion - [Worksets] -

        /// <summary>
        /// Event handler for the 'Configure/Real Time Clock' menu option. Call the menu interface method that is responsible for showing the dialog box which 
        /// allows the user to configure the VCU real time clock.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureRealTimeClock_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceApplication.ConfigureRealTimeClock();
        }

        /// <summary>
        /// Event handler for the 'Configure/Password Protection' menu option. Show the form which allows user to manage password protection.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigurePasswordProtection_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceApplication.ConfigurePasswordProtection();
        }

        /// <summary>
        /// Event handler for the 'Configure/Chart Recorder' menu option. Show the form which allows user to configure the chart recorder.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureChartRecorder_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceWatch.ConfigureChartRecorder();
        }

        /// <summary>
        /// Event handler for the 'Configure/Chart Mode/Data Mode' menu option. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureChartModeData_Click(object sender, EventArgs e)
        {
            try
            {
                m_MenuInterfaceWatch.ConfigureChartModeData();

                m_MenuItemConfigureChartModeData.Checked = true;
                m_MenuItemConfigureChartModeRamp.Checked = false;
                m_MenuItemConfigureChartModeZeroOutput.Checked = false;
                m_MenuItemConfigureChartModeFullScale.Checked = false;
            }
            catch(Exception)
            {
                MessageBox.Show(Resources.MBTModifyChartRecorderModeFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event handler for the 'Configure/Chart Mode/Ramp Mode' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureChartModeRamp_Click(object sender, EventArgs e)
        {
            try
            {
                m_MenuInterfaceWatch.ConfigureChartModeRamp();

                m_MenuItemConfigureChartModeData.Checked = false;
                m_MenuItemConfigureChartModeRamp.Checked = true;
                m_MenuItemConfigureChartModeZeroOutput.Checked = false;
                m_MenuItemConfigureChartModeFullScale.Checked = false;
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MBTModifyChartRecorderModeFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event handler for the 'Configure/Chart Mode/Zero Output' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureChartModeZeroOutput_Click(object sender, EventArgs e)
        {
            try
            {
                m_MenuInterfaceWatch.ConfigureChartModeZeroOutput();

                m_MenuItemConfigureChartModeData.Checked = false;
                m_MenuItemConfigureChartModeRamp.Checked = false;
                m_MenuItemConfigureChartModeZeroOutput.Checked = true;
                m_MenuItemConfigureChartModeFullScale.Checked = false;
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MBTModifyChartRecorderModeFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event handler for the 'Configure/Chart Mode/Full Scale' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureChartModeFullScale_Click(object sender, EventArgs e)
        {
            try
            {
                m_MenuInterfaceWatch.ConfigureChartModeFullScale();

                m_MenuItemConfigureChartModeData.Checked = false;
                m_MenuItemConfigureChartModeRamp.Checked = false;
                m_MenuItemConfigureChartModeZeroOutput.Checked = false;
                m_MenuItemConfigureChartModeFullScale.Checked = true;
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MBTModifyChartRecorderModeFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event handler for the 'Configure/Enumeration' menu option. Toggle the Enumeration property.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemConfigureEnumeration_Click(object sender, EventArgs e)
        {
            m_Enumeration = !m_Enumeration;
            m_MenuItemConfigureEnumeration.Checked = m_Enumeration;
        }
        #endregion - [CONFIGURE] -

        #region - [TOOLS] -
        /// <summary>
        /// Event handler for the 'Tools/Data Dictionary/Convert to XML Format' menu option. Allows the user to create an XML based data dictionary using an existing
        /// Access database data dictionary derived from the Data Dictionary Builder utility.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemToolsConvertEngineeringDatabase(object sender, EventArgs e)
        {
            m_MenuInterfaceApplication.ConvertEngineeringDatabase();
        }

        /// <summary>
        /// Event handler for the 'Tools/Debug Mode' menu option. Enable/disable debugging.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemToolsDebugMode_Click(object sender, EventArgs e)
        {
            m_DebugMode = !m_DebugMode;
            m_MenuItemToolsDebugMode.Checked = m_DebugMode;

            if (m_DebugMode)
            {
                DebugMode.Open();          
            }
            else
            {
                DebugMode.Close();
            }
        }

        /// <summary>
        /// Event handler for the 'Tools/Options' menu option. Displays the form which allows the user to set up the user specific parameters.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemToolsOptions_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceApplication.Options();
        }
        #endregion - [TOOLS] -

        #region - [HELP] -
        /// <summary>
        /// Event handler for the 'Help/PTU Help' menu option. Show the PTU user manual.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemHelpPTUHelp_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceApplication.ShowUserManual();
        }

        /// <summary>
        /// Event handler for the 'Help/About PTU' menu option. Display the About form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemHelpAboutPTU_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceApplication.HelpAbout();
        }
        #endregion - [HELP] -

        #region - [LOGIN] -
        /// <summary>
        /// Event handler for the 'Login/Logout' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemLogin_Click(object sender, EventArgs e)
        {
            m_MenuInterfaceApplication.Login(m_ProjectIdentifierPassedAsParameter);
        }
        #endregion - [LOGIN] -
        #endregion - [Menu Options] -

        #region - [ToolStrip Buttons] -
        /// <summary>
        /// Event handler for the on-line button <c>Click</c> event. Initializes the communication port specified in the <c>Communication</c> project user settings 
        /// and, if successful: (a) updates the mode setting and then (b) displays the form to show the live watch variable data.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TSBOnline_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the button isn't enabled.
            if (m_TSBOnline.Enabled == false)
            {
                return;
            }

            // If online mode is already selected, toggle to configuration mode.
            if (m_TSBOnline.Checked)
            {
                #region - [Return to Configuration Mode] -
                // -------------------------------------------------------
                // The PTU is already online, go to configuration mode.
                // -------------------------------------------------------
				try
				{
					this.Cursor = Cursors.WaitCursor;
					CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
				}
				catch (CommunicationException ex)
				{
					// Check to see if it's a failure to close, which we can potentially ignore.
					if (ex.CommunicationError != CommunicationError.SystemException)
					{
						// This is a recoverable error, so we allow the port to stay open, but display the error.
						MessageBox.Show(Resources.MBTPortCloseFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						MessageBox.Show(Resources.MBTPortCloseFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				catch (Exception)
				{
					MessageBox.Show(Resources.MBTPortCloseFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                SetMode(Mode.Configuration);

                // Update the LogStatus StatusStrip.
                LogStatus = EventLogSavedStatus.NotApplicable;
                #endregion -[Return to Configuration Mode] -
                return;
            }

            // ---------------------------------------------------------
            // Show the form to allow the user to select a valid target.
            // ---------------------------------------------------------
            this.Cursor = Cursors.WaitCursor;
            FormSelectTarget formSelectTarget = new FormSelectTarget();
            ShowDialog(formSelectTarget);
            this.Cursor = Cursors.WaitCursor;

            // Skip, if no target logic was selected.
            if (formSelectTarget.TargetSelected != true)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            // -------------------------------------------------------------------------------------------------
            // A valid target was selected, check that the PTU configuration and the target configuration match.
            // -------------------------------------------------------------------------------------------------
            bool configurationMatch = CheckConfiguration(formSelectTarget.TargetConfiguration.ProjectIdentifier, formSelectTarget.TargetConfiguration.Version);
            if (configurationMatch == true)
            {
                #region - [Go Online] -
                // -------------------------------------------------------------------
                // PTU configuration and target configuration match, enter online mode.
                // -------------------------------------------------------------------
                CommunicationInterface = new CommunicationParent(formSelectTarget.CommunicationSetting);
                Debug.Assert(CommunicationInterface != null);

                try
                {
                    // Initialize the serial communications port associated with the selected target.
                    CommunicationInterface.InitCommunication(CommunicationInterface.CommunicationSetting);
                }
                catch (InvalidOperationException)
                {
                    // An error occurred trying to initialize the communication port, do not enter online mode.
                    CommunicationInterface = null;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(Resources.MBTPortInitializationFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update the header information with the target configuration.
                Header_t header = new Header_t();
                header = FileHeader.HeaderCurrent;
                header.TargetConfiguration = formSelectTarget.TargetConfiguration;
                FileHeader.HeaderCurrent = header;

                SetMode(Mode.Online);

                // Check whether the most recently downloaded event log was saved to disk and update the LogStatus StatusStrip.
                LogStatus = EventLogSavedStatus.Unknown;

                UpdateChartMode();

                // Display the Watch Window only if the project doesn't use a Control Panel.
                if (this.Controls[CommonConstants.KeyControlPanel] == null)
                {
                    m_MenuInterfaceWatch.ViewWatchWindow();
                }
                #endregion - [Go Online] -
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the off-line button <c>Click</c> event. Enter offline mode.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TSBOffline_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the button isn't enabled.
            if (m_TSBOffline.Enabled == false)
            {
                return;
            }

            // If offline mode is already selected, toggle to configuration mode.
            if (m_TSBOffline.Checked)
            {
                #region - [Return to Configuration Mode] -
                // -------------------------------------------------------
                // The PTU is already offline, go to configuration mode.
                // -------------------------------------------------------
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                }
                catch (Exception)
                {
                    MessageBox.Show(Resources.MBTPortCloseFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                SetMode(Mode.Configuration);

                // Update the LogStatus StatusStrip.
                LogStatus = EventLogSavedStatus.NotApplicable;
                #endregion - [Return to Configuration Mode] -
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            #region - [Go Offline] -
            // ---------------------------------------------------------------------------------------------
            // Enter offline mode. Instantiate a the communication interface which returns simulated values.
            // ---------------------------------------------------------------------------------------------
            CommunicationSetting_t communicationSetting = new CommunicationSetting_t();
            communicationSetting.Port = new Port_t();
            communicationSetting.PortIdentifier = string.Empty;
            communicationSetting.Protocol = Protocol.SIMULATOR;
            CommunicationInterface = new CommunicationParentOffline(communicationSetting);
            TargetConfiguration_t targetConfiguration;
            CommunicationInterface.GetEmbeddedInformation(out targetConfiguration);

            // Update the header information with the target configuration.
            Header_t header = new Header_t();
            header = FileHeader.HeaderCurrent;
            header.TargetConfiguration = targetConfiguration;
            FileHeader.HeaderCurrent = header;

            SetMode(Mode.Offline);

            // Check whether the most recently downloaded event log was saved to disk and update the LogStatus StatusStrip.
            LogStatus = EventLogSavedStatus.Unknown;

            UpdateChartMode();

            // Display the Watch Window only if the project doesn't use a Control Panel.
            if (this.Controls[CommonConstants.KeyControlPanel] == null)
            {
                m_MenuInterfaceWatch.ViewWatchWindow();
            }
            #endregion - [Go Offline] -
            this.Cursor = Cursors.Default;
        }
        #endregion - [ToolStrip Buttons] -

        /// <summary>
        /// Event handler for the <c>FontChanged</c> event. Update the font associated with the controls on this form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void MdiPTU_FontChanged(object sender, EventArgs e)
        {
            // Change the font associated with the various controls contained within the form.
            m_ToolStripFunctionKeys.Font = Font;
            m_StatusStrip.Font = Font;
            m_MenuStrip.Font = Font;
        }

        /// <summary>
        /// Event handler for the WibuBox Timer Click event. Checks whether a valid WibuBox is connected to the USB or Parallel port.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void WibuBoxCheck(object sender, EventArgs e)
        {
            if (m_TimerWibuBox != null)
            {
                m_TimerWibuBox.Stop();
            }

            // Check whether the WibuBox has been removed.
            if (m_MenuInterfaceWibuKey.WibuBoxCheckIfRemoved() == true)
            {
                // The WibuBox has been removed, update the WibuKey status label.
                m_StatusLabelWibuBoxStatus.Text = Resources.LabelWibuBoxStatusNotPresent;

                // Set the current security level to the lowest security access level.
                m_Security.SetLevel(m_Security.SecurityLevelBase);
                ShowSecurityLevelChange(m_Security);
                MessageBox.Show(Resources.MBTWibuBoxRemoved + CommonConstants.NewPara + Resources.MBTWibuBoxReInsert,
                                Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (m_TimerWibuBox != null)
                {
                    m_TimerWibuBox.Start();
                }
            }
        }
        #endregion  --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Check whether the PTU configuration and the propulsion system software match. This method checks whether the PTU configuration matches that of the
        /// propulsion system software and attempts to load the correct configuration file if they do not match.
        /// </summary>
        /// <param name="targetProjectIdentifier">The project identifier of the propulsion system software.</param>
        /// <param name="targetVersion">The version reference of the propulsion system software.</param>
        /// <returns>A flag that indicates whether the PTU configuration matches that of the propulsion system software. True, if they are matched; otherwise, false.
        /// </returns>
        private bool CheckConfiguration(string targetProjectIdentifier, string targetVersion)
        {
            // A flag that indicates whether the PTU configuration matches that of the propulsion system software. True, if they are matched; otherwise, false.
            bool configurationMatch = false;

            // The DialogResult returned from the call to the MessageBox() method.
            DialogResult dialogResult;

#if ByPassVersionCheck
            while (Parameter.ProjectInformation.ProjectIdentifier != targetProjectIdentifier)
#else
            // Repeat until the PTU configuration matches that of the propulsion system software.
            while ((Parameter.ProjectInformation.ProjectIdentifier != targetProjectIdentifier) ||
                   (Parameter.ProjectInformation.Version != targetVersion))
#endif
            {
                #region - [Configuration Mismatch] -
                this.Cursor = Cursors.WaitCursor;
                // ---------------------------------------------------------------------------------------------------------------------------------------------
                // There is a mismatch between the PTU configuration and the propulsion system software. Determine whether the mismatch is a result of a version
                // number mismatch or because of a project mismatch.
                // ---------------------------------------------------------------------------------------------------------------------------------------------

                // Check whether the project identifiers match.
                if (Parameter.ProjectInformation.ProjectIdentifier == targetProjectIdentifier)
                {
                    // The project identifiers match, therefore the mismatch is between the version numbers. Ask whether an attempt is to be made to load the
                    // data dictionary associated with the target version number.
                    dialogResult = MessageBox.Show(Resources.MBTConfigVCUMismatchVersion, Resources.MBCaptionWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
                else
                {
                    // The PTU project identifier does not match that of the propulsion system software. Ask whether an attempt is to be made to load the data dictionary
                    // associated with the project-identifier and version reference of the embedded software loaded into the VCU.
                    dialogResult = MessageBox.Show(Resources.MBTConfigVCUMismatchProjectId, Resources.MBCaptionWarning, MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);
                }

                // Skip, if the user does not wish to load the appropriate data dictionary.
                if (dialogResult == DialogResult.No)
                {
                    this.Cursor = Cursors.Default;
                    configurationMatch = false;
                    return (configurationMatch);
                }

                // ------------------------------------------------------------------------------------------------------
                // The user has selected to update the PTU configuration to match that of the propulsion system software.
                // ------------------------------------------------------------------------------------------------------

                // Check whether the correct PTU configuration file associated with the propulsion system software exists in the default 'PTU Configuration Files'
                // sub-directory.
                string fullyQualifiedSourceFilename = DirectoryManager.PathPTUConfigurationFiles + CommonConstants.BindingFilename +
                                                      targetVersion + CommonConstants.ExtensionDataDictionary;
                FileInfo fileInfoSource = new FileInfo(fullyQualifiedSourceFilename);
                m_DataDictionary = new DataDictionary();

                if (fileInfoSource.Exists == false)
                {
                    #region - [Locate PTU Configuration File] -
                    switch (Parameter.ProjectInformation.ProjectIdentifier)
                    {
                        case CommonConstants.ProjectIdCTA:
                            #region - [Download From VCU] -
                            // Modified for the CTA contract. Ref.: P.O. 4800011369-CU2 07.07.2015. Attempt to download the PTU configuration files
                            // from the propulsion system software.

                            // The error code that was returned from the batch file.
                            FTPError ftpErrorCode = FTPError.Undefined;
                            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                            string newInstruction = CommonConstants.NewLine + CommonConstants.NewLine;
                            stringBuilder.AppendFormat(Resources.MBTConfigVCUDownloadStart, newInstruction, newInstruction, newInstruction);
                            dialogResult = MessageBox.Show(stringBuilder.ToString(), Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            try
                            {
                                Process batchProcess = new Process();
                                batchProcess.StartInfo.WorkingDirectory = DirectoryManager.PathPTUConfigurationFiles;
                                batchProcess.StartInfo.FileName = Resources.FilenameConfigDownload;
                                batchProcess.StartInfo.Arguments = string.Empty;
                                batchProcess.Start();
                                WriteStatusMessage(Resources.MBTConfigVCUDownloadInProgress, System.Drawing.Color.White, System.Drawing.Color.Red);
                                batchProcess.WaitForExit();
                                ftpErrorCode = (FTPError)batchProcess.ExitCode;
                            }
                            catch (Exception)
                            {
                                ftpErrorCode = FTPError.SystemException;
                            }

                            // Check the error code that was returned from the batch file.
                            if (ftpErrorCode == FTPError.Success)
                            {
                                // Check that the correct PTU configuration file now exists.
                                string fullyQualifiedDownloadedFilename = DirectoryManager.PathPTUConfigurationFiles + CommonConstants.BindingFilename +
                                                      targetVersion + CommonConstants.ExtensionDataDictionary;
                                FileInfo fileInfoDownloadedFile = new FileInfo(fullyQualifiedDownloadedFilename);
                                if (fileInfoDownloadedFile.Exists == true)
                                {
                                    WriteStatusMessage(string.Empty);
                                    MessageBox.Show(Resources.MBTConfigVCUDownloadComplete, Resources.MBCaptionInformation, MessageBoxButtons.OK,
                                               MessageBoxIcon.Information);
                                }
                                else
                                {
                                    WriteStatusMessage(string.Empty);
                                    MessageBox.Show(Resources.MBTConfigVCUDownloadFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.Cursor = Cursors.Default;
                                    configurationMatch = false;
                                    return (configurationMatch);
                                }
                            }
                            else
                            {
                                WriteStatusMessage(string.Empty);
                                MessageBox.Show(Resources.MBTConfigVCUDownloadFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Cursor = Cursors.Default;
                                configurationMatch = false;
                                return (configurationMatch);
                            }
                            #endregion - [Download From VCU] -
                            break;
                        default:
                            #region - [Manual File Selection] -
                            // The PTU configuration file associated with the propulsion system software cannot be found in the default 'PTU Configuration Files'
                            // sub-directory. Ask the user whether the file is to be located manually.
                            dialogResult = MessageBox.Show(Resources.MBTConfigVCUMatchNotFoundManualSelection, Resources.MBCaptionWarning, MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Warning);

                            if (dialogResult == DialogResult.Yes)
                            {
                                // -----------------------------------------------------------------
                                // Allow the user to select an alternative data dictionary XML file.
                                // -----------------------------------------------------------------
                                fullyQualifiedSourceFilename = General.FileDialogOpenFile(Resources.FileDialogOpenTitleDataDictionary,
                                                                                          CommonConstants.ExtensionDataDictionary,
                                                                                          Resources.FileDialogOpenFilterDataDictionary,
                                                                                          DirectoryManager.PathPTUConfigurationFiles);

                                // Skip, if no alternative data dictionary XML file is selected.
                                if (fullyQualifiedSourceFilename == string.Empty)
                                {
                                    this.Cursor = Cursors.Default;
                                    configurationMatch = false;
                                    return (configurationMatch);
                                }
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                configurationMatch = false;
                                return (configurationMatch);
                            }
                            #endregion - [Manual File Selection] -

                            break;
                    }
                    #endregion - [Locate PTU Configuration File]] -
                }

                #region - [Load the PTU Configuration File] -
                // ------------------------------------------------------------------------------------------
                // The PTU configuration file corresponding to the propulsion system software exists, load it.
                // ------------------------------------------------------------------------------------------
                try
                {
                    FileHandling.LoadDataSet<DataDictionary>(fullyQualifiedSourceFilename, ref m_DataDictionary);

                    // Copy the selected PTU configuration file to the default directory and rename it to the default filename.
                    string fullyQualifiedDestinationFilename = DirectoryManager.PathPTUConfigurationFiles + CommonConstants.BindingFilename + FilenameDataDictionary;
                    FileInfo fileInfoDestination = new FileInfo(fullyQualifiedDestinationFilename);
                    fileInfoSource = new FileInfo(fullyQualifiedSourceFilename);
                    fileInfoSource.CopyTo(fullyQualifiedDestinationFilename, true);
                    LoadDictionary(m_DataDictionary);

                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show(Resources.MBTConfigReadFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    configurationMatch = false;
                    return (configurationMatch);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show(Resources.MBTConfigVCUMatchNotFoundTryAgain, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    configurationMatch = false;
                    return (configurationMatch);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
                #endregion - [Load the PTU Configuration File] -
                #endregion - [Configuration Mismatch] -
            }

            configurationMatch = true;
            return (configurationMatch);
        }

        /// <summary>
        /// Update the 'Configure/Chart Mode' sub-menus 'Checked' property to show which mode is currently enabled. 
        /// </summary>
        private void UpdateChartMode()
        {
            // The current chart mode: Data; Ramp; ZeroOutput; ullScale.
            ChartMode chartMode = ChartMode.Undefined;

            m_MenuItemConfigureChartModeData.Checked = false;
            m_MenuItemConfigureChartModeRamp.Checked = false;
            m_MenuItemConfigureChartModeZeroOutput.Checked = false;
            m_MenuItemConfigureChartModeFullScale.Checked = false;
            try
            {
                // Get the current chart mode.
#if DAS
                chartMode = CommunicationInterface.GetChartMode();
#else
                chartMode = ChartMode.DataMode;
#endif
                switch (chartMode)
                {
                    case ChartMode.DataMode:
                        m_MenuItemConfigureChartModeData.Checked = true;
                        break;
                    case ChartMode.RampMode:
                        m_MenuItemConfigureChartModeRamp.Checked = true;
                        break;
                    case ChartMode.ZeroOutputMode:
                        m_MenuItemConfigureChartModeZeroOutput.Checked = true;
                        break;
                    case ChartMode.FullScaleMode:
                        m_MenuItemConfigureChartModeFullScale.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the state of the static flag that controls whether the PTU does an automatic restart when the main PTU application is closed. True, if the PTU is to do
        /// an automatic restart; otherwise, false.
        /// </summary>
        public static bool Restart
        {
            get { return m_Restart; }
        }

        /// <summary>
        /// Gets or sets the flag which indicates whether the Dispose() method has been called. True indicates that the Dispose() method has been called; otherwise,
        /// false.
        /// </summary>
        protected new bool IsDisposed
        {
            get
            {
                lock (this)
                {
                    return m_IsDisposed;
                }
            }

            set
            {
                lock (this)
                {
                    m_IsDisposed = value;
                }
            }
        }

        #region - [IMainWindow] -
        /// <summary>
        /// Gets the reference to the main menu strip.
        /// </summary>
        public MenuStrip MenuStrip
        {
            get { return m_MenuStrip; }
        }

        /// <summary>
        /// Gets the reference to the main status strip.
        /// </summary>
        public StatusStrip StatusStrip
        {
            get { return m_StatusStrip; }
        }

        /// <summary>
        /// Gets or sets the reference to the <c>ToolStrip</c> control containing the function key buttons.
        /// </summary>
        public ToolStrip ToolStripFunctionKeys
        {
            get { return m_ToolStripFunctionKeys; }
            set { m_ToolStripFunctionKeys = value; }
        }

        /// <summary>
        /// Gets the mode of operation of the PTU application: setup, online, diagnostic or offline.
        /// </summary>
        public Mode Mode
        {
            get { return m_Mode; }
        }

        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the target.
        /// </summary>
        public ICommunicationParent CommunicationInterface
        {
            get { return m_CommunicationInterface; }
            set { m_CommunicationInterface = value; }
        }

        /// <summary>
        /// Gets the collection of function keys associated with the form. This allows any child form that is called indirectly to restore the function keys 
        /// on exit.
        /// </summary>
        public ToolStripItemCollection ToolStripItemCollectionMainWindow
        {
            get { return m_ToolStripItemCollectionMainWindow; }
        }

        /// <summary>
        /// Gets the flag that controls whether enumerator watch variables are to have their values displayed as the enumerated text value or the actual numeric value.
        /// True, displays the value as enumerated text; false, displays the values as numeric data.
        /// </summary>
        public bool Enumeration
        {
            get { return m_Enumeration; }
        }

        /// <summary>
        /// Gets or sets the flag that controls whether the animation showing that the PTU is busy processing data is visible or not. True, to show the animation; 
        /// otherwise, false.
        /// </summary>
        public bool ShowBusyAnimation
        {
            get { return m_ShowBusyAnimation; }
            set 
            {
                m_ShowBusyAnimation = value;
                m_PictureBoxBusy.Visible = m_ShowBusyAnimation;
            }
        }

        /// <summary>
        /// Gets the filename of the XML data dictionary file.
        /// </summary>
        public string FilenameDataDictionary
        {
            get { return m_FilenameDataDictionary; }
        }

        /// <summary>
        /// Gets or sets the event log saved status associated with the current car. This property also updates the LogStatus StatusLabel whenever the property is
        /// written to.
        /// </summary>
        public EventLogSavedStatus LogStatus
        {
            get { return m_EventLogSavedStatus; }
            set
            {
                m_EventLogSavedStatus = value;
                switch (m_EventLogSavedStatus)
                {
                    case EventLogSavedStatus.Unsaved:
                    case EventLogSavedStatus.Saved:
                    case EventLogSavedStatus.Unknown:
                    case EventLogSavedStatus.Undefined:
                         m_StatusLabelLogStatus.Text = Resources.LegendLogStatus + CommonConstants.Space + m_EventLogSavedStatus.ToString();
                        break;
                    case EventLogSavedStatus.NotApplicable:
                    default:
                        m_StatusLabelLogStatus.Text = Resources.LegendLogStatus + CommonConstants.Space;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Flag that indicates whether a WibuBox security device is present or not. True, if a WibuBox security device is present; otherwise, false.
        /// </summary>
        public bool WibuBoxPresent
        {
            get { return m_WibuBoxPresent; }
            set
            {
                m_WibuBoxPresent = value;
                m_StatusLabelWibuBoxStatus.Text = (m_WibuBoxPresent == true) ? Resources.LabelWibuBoxStatusPresent : Resources.LabelWibuBoxStatusNotPresent;
            }
        }

        /// <summary>
        /// Gets the current car number, if the PTU is connected to the target logic. If not connected to the car logic, the value that will be returned is
        /// short.MinVal.
        /// </summary>
        public short CarNumber
        {
            get { return m_CarNumber; }
            set { m_CarNumber = value; }
        }

        /// <summary>
        /// Gets the project identifier that was passed to the application as a shortcut parameter. If no shortcut parameter was passed to the
        /// application then the value is set to string.Empty.
        /// </summary>
        public string ProjectIdentifierPassedAsParameter
        {
            get { return m_ProjectIdentifierPassedAsParameter; }
        }
        #endregion - [IMainWindow] -
        #endregion --- Properties ---
    }
}
