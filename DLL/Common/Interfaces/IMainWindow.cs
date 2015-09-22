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
 *  Project:    Common
 * 
 *  File name:  IMainWindow.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/16/10    1.1     K.McD           1.  Bug fix SNCR 001.009. If the user exits the replay screen directly rather than returning back via the YT plot screen, 
 *                                          the function keys are not restored correctly. Added the ToolStripItemCollectionMainWindow property.
 * 
 *  08/25/10    1.2     K.McD           1.  Extended the available signatures associated with the WriteStatusMessage() method to allow the programmer to specify the 
 *                                          background and foreground colours of the status message.
 * 
 *  08/25/10    1.3     K.McD           1.  Added the MdiChildren property to allow programmers to search for all multiple-document interface (MDI) child forms that are 
 *                                          parented by this form.
 * 
 *  09/30/10    1.4     K.McD           1.  Added the Enumeration property. This property determines whether the value associated with enumerator watch variables is 
 *                                          displayed as the enumerated text value or as a numeric value.
 * 
 *  10/06/10    1.5     K.McD           1.  Bug fix SNCR001.24. Added the CloseChildForms() method.
 * 
 *  10/08/10    1.6     K.McD           1.  Corrected the name of the parameter in the method WriteCarIdentifier().
 *                                      2.  Added the method CloseChildForms(). A call to this method closes any child forms that may be open.
 * 
 *  10/15/10    1.7     K.McD           1.  Modified to use the CommunicationParent class rather than the ICommunication interface.
 * 
 *  02/14/11    1.8     K.McD           1.  Modified the signature of the ShowSecurityLevelChange() method to use the Security class rather than the ISecurity interface.
 * 
 *  02/28/11    1.9     K.McD           1.  Added the Update() and WriteProgressBarLegend() methods.
 *  
 *  07/07/11    1.10    K.McD           1.  Added the ShowBusyAnimation property.
 *                                      2.  Added the SetMode() method to allow the current mode of the application to be modified.
 *                                      3.  Added the SelfTest and Simulation modes to the Mode enumerator.
 *                                      4.  Modified one or more XML tags.
 *                                      
 *  07/13/11    1.10.1  K.McD           1.  Changed the definition of the Mode enumerator to take into account the redefinition of off-line mode and diagnostic mode 
 *                                          discussed in the June sprint review.
 *                                          
 *  07/20/11    1.10.2  K.McD           1.  Modified the CommunicationInterface property to inherit from ICommunicationParent.
 *  
 *  10/24/11    1.10.3  K.McD           1.  Added the KeyPreview property.
 *                                      2.  Modified the Mode enumerator such that the startup mode of the PTU was renamed to be Configuration rather than Diagnostic.
 *                                      
 *  07/31/13    1.11    K.McD           1.  Added the FilenameDataDictionary property, this specifies the filename of the current XML data dictionary filename.
 *  
 *  05/06/15    1.12    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘WibuBox: [Present | Not Present]’.
 *                                              
 *                                      Modifications
 *                                      1.  Added the LogSaved and WibuBoxPresent properties.
 *                                      2.  Modified the definition of the TaskProgressBar property from ProgressBar to ToolStripProgressBar.
 *                                      
 *  07/13/15    1.13    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                          
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. Following a conference call on 9-Jul-15,
 *                                              the current proposal is to extend the options associated with the log saved status StatusLabel to include:
 *                                              ‘[Saved | Unsaved | Unknown | Not Applicable (-)]’.
 *                                              
 *                                      Modifications
 *                                      1.  Added the EventLogSavedStatus enumerator. This enumerator represents the different event log saved status options.
 *                                      2.  Added the MenuUpdated event. This event is raised when the main menu is updated to reflect a new mode or security level.
 *                                      3.  Removed the MenuStrip 'set' property.
 *                                      4.  Added the StatusStrip 'get' property.
 *                                      5.  Added the LogStatus property.
 *                                      6.  Added the CarNumber property.
 *                                      
 *  07/24/15    1.14    K.McD           References
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
 *                                      2.  An informal review of version 6.11 of the PTU concluded that, where possible - i.e. if the PTU is started from a shortcut
 *                                          that passes the project identifier as a shortcut parameter, the project specific PTU initialization should be carried out
 *                                          in the MDI Form contructor that has the parameter string array as its signature rather than by the LoadDictionary() method.
 *                                          This streamlines the display construction of the Control Panel associated with the R188 project. In the 6.11 implementation
 *                                          the CTA layout is momentarily displayed before the Control Panel is drawn, however by initializing the project specific
 *                                          features in the constructor the Control Panel associated with the R188 project is drawn immediately and the CTA layout
 *                                          is not shown at all.
 *  
 *                                      Modifications
 *                                      1.  Removed the WriteProgressBarLegend() methods and the TaskProgressBar ToolStripProgressBar as these no longer exist.
 *                                          The progress bar used to display the recording and playback of data streams now appears in the 'Information' Panel of
 *                                          the FormWatch Form. The progress bar was moved to allow the status message display to be extended to support some of the
 *                                          longer messages required to support the upgrade shown above. - Ref.: 1.
 *                                          
 *                                      2.  Added the ProjectIdentifierPassedAsParameter string property and the Close() and SetRestart() methods to support the
 *                                          changes associated with reference 2, above. The Close() and SetRestart() methods allow any object that can access the
 *                                          IMainWindow interface to initiate a shutdown of the PTU application whereas the ProjectIdentifierPassedAsParameter variable
 *                                          keeps a record of the project identifier that was passed as a shortcut parameter. If no shortcut parameter is passed
 *                                          then the ProjectIdentifierPassedAsParameter string is set to string.Empty. - Ref.: 2.
 */
#endregion --- Revision History ---

using System;
using System.Drawing;
using System.Windows.Forms;

using Common.Communication;
using Common.Configuration;
using Common.Forms;

namespace Common
{
    #region --- Enumerators ---
    /// <summary>
    /// Defines the mode of operation of the PTU application: setup, configuration, online or offline.
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// Setup. One or more of the configuration files were not installed during the installation process and the PTU is waiting for these files to be installed 
        /// before proceeding.
        /// </summary>
        Setup,

        /// <summary>
        /// Online. The PTU is connected to the VCU and all menu options, appropriate to the current security level, are available.
        /// </summary>
        Online,

        /// <summary>
        /// Configuration. The PTU is not currently connected to the VCU and only those menu options associated with setting up the project worksets and analyzing data 
        /// collected from site are available.
        /// </summary>
        Configuration,

        /// <summary>
        /// Self Test. The VCU and PTU are in self test mode.
        /// </summary>
        SelfTest,

        /// <summary>
        /// Offline. The PTU is not currently connected to the VCU; all screens that are available in Online mode are available but display dummy values instead of 
        /// live data. This mode allows the user to gain familiarity with the menu options of the PTU and to look at the layout of the worksets that have been created 
        /// in diagnostic mode without being connected to the VCU.
        /// </summary>
        Offline
    }

    /// <summary>
    /// The saved status of an event log.
    /// </summary>
    public enum EventLogSavedStatus
    {
        /// <summary>
        /// Undefined. Value: 0
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The last downloaded maintenance log associated with the current car has been saved to disk. Value: 1.
        /// </summary>
        Saved = 1,

        /// <summary>
        /// The last downloaded maintenance log associated with the current car has not yet been saved to disk. Value: 2.
        /// </summary>
        Unsaved = 2,

        /// <summary>
        /// The log status associated with the current car is not known. Value: 3.
        /// </summary>
        Unknown = 3,

        /// <summary>
        /// The log status is not applicable until the unit is connected to a car. Value: 4.
        /// </summary>
        NotApplicable = 4
    }
    #endregion - [Enumerators] -

    /// <summary>
    /// Defines the interface to the main application window of the PTU application.
    /// </summary>
    public interface IMainWindow
    {
        #region - [Events] -
        /// <summary>
        /// Raised if the Font property is changed.
        /// </summary>
        event EventHandler FontChanged;

        /// <summary>
        /// Raised when the menu is updated to reflect a new mode or security level.
        /// </summary>
        event EventHandler MenuUpdated;
        #endregion - [Events] -

        #region - [Methods] -
        /// <summary>
        /// Close any child form that may be open. For this to work the child form must inherit from <c>FormPTU</c>.
        /// </summary>
        /// <remarks>The child forms are closed cleanly by simulating the user having pressed the escape key associated with the form.</remarks>
        void CloseChildForms();

        /// <summary>
        ///  Write the specified car identifier in the status label used to display the car identifier.
        /// </summary>
        /// <param name="carIdentifier">The car identifier.</param>
        void WriteCarIdentifier(string carIdentifier);

        /// <summary>
        /// Show that data from the target hardware has been updated by blinking the data update icon on the screen.
        /// </summary>
        void BlinkUpdateIcon();

        /// <summary>
        /// Show that the security clearance has been updated by modifying the: (a) Login/Logout text associated with the menu and button; (b) status line text 
        /// and (c) menu options to reflect the new clearance level.
        /// </summary>
        /// <param name="security">Reference to the security class for which the security clearance level is to be displayed.</param>
        void ShowSecurityLevelChange(Security security);

        /// <summary>
        /// Write the specified message to the status message control using the default <c>BackColor</c> and <c>ForeColor</c> properties.
        /// </summary>
        /// <param name="message">The message text.</param>
        void WriteStatusMessage(string message);

        /// <summary>
        /// Write the specified message to the status message control using the specified <c>BackColor</c> and <c>ForeColor</c> properties.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="backColor">The <c>BackColor</c> of the message.</param>
        /// <param name="foreColor">The <c>ForeColor</c> of the message.</param>
        void WriteStatusMessage(string message, Color backColor, Color foreColor);

        ///<summary>
        /// Show the specified child form.
        ///</summary>
        ///<param name="childForm">The child form that is to be displayed.</param>
        void ShowMdiChild(FormPTU childForm);

        ///<summary>
        /// Show the specified dialog form.
        ///</summary>
        ///<param name="dialogForm">The child form that is to be displayed.</param>
        DialogResult ShowDialog(FormPTUDialog dialogForm);

        /// <summary>
        /// Configures the PTU appplication using the specified data dictionary. (1) Updates the <c>Parameter</c> class; (2) Updates the <c>WatchVariableTable</c> class; 
        /// (3) Creates the application data sub-directories, if they do not exist; (4) Updates the main menu options to reflect the current project; (5) Loads the
        /// default workset associated with the specified data dictionary, if it exists; (6) Updates the form title and (7) Updates the file header information.
        /// </summary>
        /// <param name="dataDictionary">The data dictionary that is to be used to configure the PTU.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dataDictionary"/> is null.</exception>
        void LoadDictionary(DataDictionary dataDictionary);

        /// <summary>
        /// Set the current mode of operation. This will set the on-line/off-line buttons; status line text and menu options to reflect the specified mode.
        /// </summary>
        /// <param name="mode">The new mode of operation.</param>
        void SetMode(Mode mode);

        /// <summary>
        /// Sets the static flag that controls whether the PTU does an automatic restart when the main PTU application is closed. True, if the PTU is to do
        /// an automatic restart; otherwise, false.
        /// </summary>
        void SetRestart(bool value);

        /// <summary>
        /// Causes the control to redraw the invalidated regions within its client area.
        /// </summary>
        void Update();

        /// <summary>
        /// Closes the form.
        /// </summary>
        void Close();
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets the reference to the main menu strip.
        /// </summary>
        MenuStrip MenuStrip { get; }

        /// <summary>
        /// Gets the reference to the main status strip.
        /// </summary>
        StatusStrip StatusStrip { get; }

        /// <summary>
        /// Gets or sets the reference to the <c>ToolStrip</c> user control containing the function key buttons.
        /// </summary>
        ToolStrip ToolStripFunctionKeys { get; set;}

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        Font Font { get; set;}

        /// <summary>
        /// Gets the mode of operation of the PTU application: setup; online, offline or self-test.
        /// </summary>
        Mode Mode { get; }

        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the target.
        /// </summary>
        ICommunicationParent CommunicationInterface { get;}

        /// <summary>
        /// Gets or sets the cursor associated with the main window.
        /// </summary>
        Cursor Cursor { get; set; }

        /// <summary>
        /// Gets the rectangle that represents the virtual display area of the control.
        /// </summary>
        Rectangle DisplayRectangle { get;}

        /// <summary>
        /// Gets the collection of function keys associated with the form. This allows any child form that is called indirectly to restore the function keys 
        /// on exit.
        /// </summary>
        ToolStripItemCollection ToolStripItemCollectionMainWindow { get;}

        /// <summary>
        /// Gets an array of forms that represent the multiple-document interface (MDI) child forms that are parented by this form.
        /// </summary>
        Form[] MdiChildren { get;}

        /// <summary>
        /// Gets the flag that controls whether enumerator watch variables are to have their values displayed as the enumerated text value or the actual numeric value.
        /// True, displays the value as enumerated text; false, displays the values as numeric data.
        /// </summary>
        bool Enumeration {get; }

        /// <summary>
        /// Gets or sets the flag that controls whether the animation showing that the PTU is busy processing data is visible or not. True, to show the animation; 
        /// otherwise, false.
        /// </summary>{}
        bool ShowBusyAnimation { get; set; }

        /// <summary>
        /// Determines whether keyboard events for controls on the form are registered or not.
        /// </summary>
        bool KeyPreview { get; set; }

        /// <summary>
        /// Gets the filename of the XML data dictionary file.
        /// </summary>
        string FilenameDataDictionary { get; }

         /// <summary>
        /// Gets or sets the saved status of the event logs. Saved status options are: Saved, Unsaved, Unknown, Not Applicable (-), Undefined.
        /// </summary>
        EventLogSavedStatus LogStatus { get; set; }

        /// <summary>
        /// Gets or sets the Flag that indicates whether a WibuBox security device is present or not. True, if a WibuBox security device is present; otherwise, false.
        /// </summary>
        bool WibuBoxPresent { get; set; }

        /// <summary>
        /// Gets the current car number if the PTU is connected to the target logic. If not connected to the car logic, the vale is set int.MinVal.
        /// </summary>
        short CarNumber { get; set; }

        /// <summary>
        /// Gets the project identifier that was passed to the application as a shortcut parameter. If no shortcut parameter was passed to the
        /// application then the value is set to string.Empty.
        /// </summary>
        string ProjectIdentifierPassedAsParameter { get; }
        #endregion - [Properties] -
    }
}
