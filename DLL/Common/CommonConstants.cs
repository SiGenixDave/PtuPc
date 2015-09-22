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
 *  File name:  Constants.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  03/31/10    1.0     K.McD           1   First entry into TortoiseSVN.
 * 
 *  08/24/10    1.1     K.McD           1.  Added the key constants used to access the menu options.
 * 
 *  09/29/10    1.2     K.McD           1.  Added the BitsPerByte constant.
 * 
 *  11/02/10    1.3     K.McD           1.  Added the NotFound constant.
 * 
 *  01/06/11    1.4     K.McD           1.  Added the costants BindingComma, BindingPeriod and TextNotDefined.
 * 
 *  02/28/11    1.5     K.McD           1.  Modified a number of the menu access key constants in line with the latest directory structure.
 *                                      2.  Removed the Padding.MBT constant as it is no longer required.
 *                                      3.  Added the Century constant.
 * 
 *  03/18/11    1.6     K.McD           1.  Added the text strings used to define the text that is to be displayed in the name and units field of user controls if 
 *                                          the variable associated with the control cannot be found in the data dictionary.
 * 
 *                                      2.  Added the Windows help file extension.
 * 
 *  03/28/11    1.7     K.McD           1.  Updated to support image files.
 *                                      2.  Renamed a number of constants and modified the associated XML tags.
 *                                      3.  Included support for undefined watch variables.
 * 
 *  04/27/11    1.8     K.McD           1.  Modified the class such that only those menu option keys that are actually used are included.
 *                                      2.  Added the menu option keys associated with setting the chart mode.
 *                                      
 *  05/25/11    1.9     K.McD           1.  Added the ChartScaleValueNotDefinedString constant.
 *                                      2.  Renamed a number of constants.
 *                                      
 *  06/21/11    1.10    K.McD           1.  Added the TestNotDefinedString constant.
 *  
 *  06/29/11    1.11    K.McD           1.  Added the [Formatting] constants.
 *  
 *  07/25/11    1.11.1  K.McD           1.  Added the BindingTestNumber constant.
 *  
 *  08/24/11    1.11.2  K.McD           1.  Removed the TimeFormString constant.
 *                                      2.  Added the IPollTarget constants and the FormatStringTime constant.
 *                                      
 *  08/25/11    1.11.2  K.McD           1.  Added the FormatStringDateFromVCU constant.
 *                                      2.  Increased the TimeoutMsPauseFeedback value to 1,000 ms.
 *                                      
 *  10/01/11    1.11.3  K.McD           1.  Added the AsciiDelete and BindingLabelStatusMessage constants.
 *  
 *  06/19/13    1.11.4  K.McD           1.  Added the NewPara constant.
 *                                      2.  Added the project identifier strings.
 *                                      
 *  03/27/15    1.12    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 * 
 *                                              1.  MOC-0171-29/34. Kawasaki requested that any function that is not available to the current mode, either Maintenance
 *                                                  or Administrative, should not be displayed, or be 'greyed out'. BTPC agreed to review the software to ensure that
 *                                                  non-active menu items are ‘greyed out’ or not shown.
 *                                                  
 *                                      2.  A FunctionFlags bitmask field is to be added to the  CONFIGUREPTU table. This field is a bitmask that specifies the function
 *                                          options that are required for the current project. So far, the following flags have been defined:
 *
 *                                              Bit 0   -   Use4DigitYearCode - Flag to specify whether the project VCU uses a 2 or 4 digit year code. True, if the
 *                                                          project uses a 4 digit year code; otherwise, false.
 *                                              Bit 1   -   ShowEventLogType - Flag to specify whether the event log type field is to be shown when saved event logs are
 *                                                          displayed. True, if the log type is to be displayed; otherwise, false.
 *                                              .
 *                                              .
 *                                              .
 *                                              Bit 7   -   Not Used.  
 *                                              
 *                                      Modifications
 *                                      1.  Added a number of menu option keys to allow the corresponding menu options to be easily enabled/disabled from any form
 *                                          that inherits from FormPTU by using the SetMenuEnabled() method. - Ref.: 1.1.1.
 *                                      2.  Corrected the modifiers of the bitmask bits to be public. - Ref.: 2.
 *                                      
 *  07/13/15    1.13        K.McD       References
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
 *                                      1.  Extensive additions to the Keys used to access various controls used within the PTU system, especially the: Form, MenuOption,
 *                                          StatusLabel and ToolStripSeparator controls.
 */
#endregion --- Revision History ---

using Common.Properties;

namespace Common
{
    /// <summary>
    /// Defines the general constants associated with the assembly.
    /// </summary>
    public class CommonConstants
    {
        #region - [Project Identifiers] -
        /// <summary>
        /// The project Identifier associated with the New York City Transit Authority (NYCT) R188 Propulsion Car Control Unit. Value: "R8PR".
        /// </summary>
        public const string ProjectIdNYCT = "R8PR";

        /// <summary>
        /// The project identifier associate with the Chicago Transit Authority (CTA) Propulsion and Friction Brake project. Value: "CTPA".
        /// </summary>
        public const string ProjectIdCTA = "CTPA";
        #endregion - [Project Identifiers] -

        #region - [Keys] -
        /// <summary>
        /// Key corresponding to the Multiple Document Interface (MDI) ControlPanel user control that is used on some projects. Value: "m_ControlPanel".
        /// </summary>
        public const string KeyControlPanel = "m_ControlPanel";

        /// <summary>
        /// Key corresponding to the Multiple Document Interface (MDI) Status Panel control. Value: "m_PanelStatus".
        /// </summary>
        public const string KeyPanelStatus = "m_PanelStatus";

        /// <summary>
        /// Key corresponding to the Multiple Document Interface (MDI) MenuStrip control. Value: "m_MenuStrip". 
        /// </summary>
        public const string KeyMenuStrip = "m_MenuStrip";

        /// <summary>
        /// Key corresponding to the Multiple Document Interface (MDI) ToolStrip control. Value: "m_ToolStripFunctionKeys". 
        /// </summary>
        public const string KeyToolStripFunctionKeys = "m_ToolStripFunctionKeys";

        /// <summary>
        /// Key corresponding to the  'Go Online' ToolStripButton control. Value: "m_TSBOnline". 
        /// </summary>
        public const string KeyToolStripButtonOnline = "m_TSBOnline";

        /// <summary>
        /// Key corresponding to the 'Go Offline' ToolStripButton control. Value: "m_TSBOffline". 
        /// </summary>
        public const string KeyToolStripButtonOffline = "m_TSBOffline";

        #region - [Forms] -
        /// <summary>
        /// Key corresponding to the 'Open/Event Log' form. Value: "FormOpenEventLog".
        /// </summary>
        public const string KeyFormOpenEventLog = "FormOpenEventLog";

        /// <summary>
        /// Key corresponding to the 'Open/Fault Log' form. Value: "FormOpenFaultLog".
        /// </summary>
        public const string KeyFormOpenFaultLog = "FormOpenFaultLog";

        /// <summary>
        /// Key corresponding to the 'View/Fault Log' form. Value: "FormViewFaultLog".
        /// </summary>
        public const string KeyFormViewFaultLog = "FormViewFaultLog";

        /// <summary>
        /// Key corresponding to the 'View/Event Log' form. Value: "FormViewEventLog".
        /// </summary>
        public const string KeyFormViewEventLog = "FormViewEventLog";

        /// <summary>
        /// Key corresponding to the 'Open/Simulated Fault Log' form. Value: "FormOpenSimulatedFaultLog".
        /// </summary>
        public const string KeyFormOpenSimulatedFaultLog = "FormOpenSimulatedFaultLog";

        /// <summary>
        /// Key corresponding to the 'Open/Watch Window' form. Value: "FormOpenWatch".
        /// </summary>
        public const string KeyFormOpenWatch = "FormOpenWatch";

        /// <summary>
        /// Key corresponding to the 'View/Watch Window' form. Value: "FormViewWatch".
        /// </summary>
        public const string KeyFormViewWatch = "FormViewWatch";
        #endregion - [Forms] -

        #region - [ToolStripSeparators] -
        /// <summary>
        /// Key corresponding to the 'Go Online' ToolStripButton Left-Hand-Side Separator. Value: "m_SeparatorOnlineLHS". 
        /// </summary>
        public const string KeyToolStripSeparatorOnlineLHS = "m_SeparatorOnlineLHS";

        /// <summary>
        /// Key corresponding to the 'Go Offline' ToolStripButton Left-Hand-Side Separator. Value: "m_SeparatorOfflineLHS". 
        /// </summary>
        public const string KeyToolStripSeparatorOfflineLHS = "m_SeparatorOfflineLHS";

        /// <summary>
        /// Key to access the 'File/Select Data Dictionary' Separator. Value: ""m_SeparatorFileOpenDataDictionary".
        /// </summary>
        public const string KeyToolStripSeparatorFileOpenDataDictionary = "m_SeparatorFileOpenDataDictionary";

        /// <summary>
        /// Key to access the 'Configure/Password Protection' Separator. Value: ""m_SeparatorConfigurePasswordProtection".
        /// </summary>
        public const string KeyToolStripSeparatorConfigurePasswordProtection = "m_SeparatorConfigurePasswordProtection";

        /// <summary>
        /// Key to access the 'Help/PTU Help' Separator. Value: ""m_SeparatorHelpPTUHelp".
        /// </summary>
        public const string KeyToolStripSeparatorHelpPTUHelp = "m_SeparatorHelpPTUHelp";
        #endregion - [ToolStripSeparators] -

        #region - [Status Labels] -
        /// <summary>
        /// Key corresponding to the 'Log Status' ToolStripStatusLabel. Value: "m_StatusLabelLogStatus". 
        /// </summary>
        public const string KeyToolStripStatusLabelLogStatus = "m_StatusLabelLogStatus";

        /// <summary>
        /// Key corresponding to the 'Wibu Box Status' ToolStripStatusLabel. Value: "m_StatusLabelWibuBoxStatus". 
        /// </summary>
        public const string KeyToolStripStatusLabelWibuBoxStatus = "m_StatusLabelWibuBoxStatus";

        /// <summary>
        /// Key corresponding to the 'Car No.' ToolStripStatusLabel. Value: "m_StatusLabelCarNumber". 
        /// </summary>
        public const string KeyToolStripStatusLabelCarNumber = "m_StatusLabelCarNumber";

        /// <summary>
        /// Key corresponding to the 'Mode' ToolStripStatusLabel. Value: "m_StatusLabelMode". 
        /// </summary>
        public const string KeyToolStripStatusLabelMode = "m_StatusLabelMode";

        /// <summary>
        /// Key corresponding to the 'SecurityLevel' ToolStripStatusLabel. Value: "m_StatusLabelSecurityLevel". 
        /// </summary>
        public const string KeyToolStripStatusLabelSecurityLevel = "m_StatusLabelSecurityLevel";
        #endregion - [Status Labels] -

        #region - [Menu Option Keys] -
        /// <summary>
        /// Key to access the 'File' menu option. Value: "m_MenuItemFile".
        /// </summary>
        public const string KeyMenuItemFile = "m_MenuItemFile";

        /// <summary>
        /// Key to access the 'File/Exit' menu option. Value: "m_MenuItemFileExit".
        /// </summary>
        public const string KeyMenuItemFileExit = "m_MenuItemFileExit";

        /// <summary>
        /// Key to access the 'File/Open' menu option. Value: "m_MenuItemFileOpen".
        /// </summary>
        public const string KeyMenuItemFileOpen = "m_MenuItemFileOpen";

        /// <summary>
        /// Key to access the 'View' menu option. Value: "m_MenuItemView".
        /// </summary>
        public const string KeyMenuItemView = "m_MenuItemView";

        /// <summary>
        /// Key to access the 'View/Watch Window' menu option. Value: "m_MenuItemViewWatchWindow".
        /// </summary>
        public const string KeyMenuItemViewWatchWindow = "m_MenuItemViewWatchWindow";

        /// <summary>
        /// Key to access the 'View/System Information Window' menu option. Value: "m_MenuItemViewSystemInformation".
        /// </summary>
        public const string KeyMenuItemViewSystemInformation = "m_MenuItemViewSystemInformation";

        /// <summary>
        /// Key to access the 'Diagnostics' menu option. Value: "m_MenuItemDiagnostics".
        /// </summary>
        public const string KeyMenuItemDiagnostics = "m_MenuItemDiagnostics";

        /// <summary>
        /// Key to access the 'Diagnostics/Event Log' menu option. Value: "m_MenuItemDiagnosticsEventLog".
        /// </summary>
        public const string KeyMenuItemDiagnosticsEventLog = "m_MenuItemDiagnosticsEventLog";

        /// <summary>
        /// Key to access the 'Diagnostics/SelfTests' menu option. Value: "m_MenuItemDiagnosticsSelfTests".
        /// </summary>
        public const string KeyMenuItemDiagnosticsSelfTests = "m_MenuItemDiagnosticsSelfTests";

        /// <summary>
        /// Key to access the 'Diagnostics' menu option. Value: "m_MenuItemDiagnosticsInitializeEventLogs".
        /// </summary>
        public const string KeyMenuItemDiagnosticsInitializeEventLogs = "m_MenuItemDiagnosticsInitializeEventLogs";

        /// <summary>
        /// Key to access the 'Configure' menu option. Value: "m_MenuItemConfigure".
        /// </summary>
        public const string KeyMenuItemConfigure = "m_MenuItemConfigure";

        /// <summary>
        /// Key to access the 'Configure/Workset/Watch Window' menu option. Value: "m_MenuItemConfigureWorksetsWatchWindow".
        /// </summary>
        public const string KeyMenuItemConfigureWorksetsWatchWindow = "m_MenuItemConfigureWorksetsWatchWindow";

        /// <summary>
        ///  Key to access the 'Configure/Workset/DataStream' menu option. Value: "m_MenuItemConfigureWorksetsFaultLog".
        /// </summary>
        public const string KeyMenuItemConfigureWorksetsFaultLog = "m_MenuItemConfigureWorksetsFaultLog";

        /// <summary>
        /// Key to access the 'Configure/Workset/Chart Recorder' menu option. Value: "m_MenuItemConfigureWorksetsChartRecorder".
        /// </summary>
        public const string KeyMenuItemConfigureWorksetsChartRecorder = "m_MenuItemConfigureWorksetsChartRecorder";

        /// <summary>
        /// Key to access the 'Configure/Real Time Clock' menu option. Value: "m_MenuItemConfigureRealTimeClock".
        /// </summary>
        public const string KeyMenuItemConfigureRealTimeClock = "m_MenuItemConfigureRealTimeClock";

        /// <summary>
        /// Key to access the 'Configure/Password Protection' menu option. Value: ""m_MenuItemConfigurePasswordProtection".
        /// </summary>
        public const string KeyMenuItemConfigurePasswordProtection = "m_MenuItemConfigurePasswordProtection";

        /// <summary>
        /// Key to access the 'Configure/Chart Recorder' menu option. Value: ""m_MenuItemConfigureChartRecorder".
        /// </summary>
        public const string KeyMenuItemConfigureChartRecorder = "m_MenuItemConfigureChartRecorder";

        /// <summary>
        /// Key to access the 'Configure/Chart Mode' menu option. Value: "m_MenuItemConfigureChartMode".
        /// </summary>
        public const string KeyMenuItemConfigureChartMode = "m_MenuItemConfigureChartMode";

        /// <summary>
        /// Key to access the 'Configure/Chart Mode/Data Mode' menu option. Value: "m_MenuItemConfigureChartModeData".
        /// </summary>
        public const string KeyMenuItemConfigureChartModeData = "m_MenuItemConfigureChartModeData";

        /// <summary>
        /// Key to access the 'Configure/Chart Mode/RampMode' menu option. Value: "m_MenuItemConfigureChartModeRamp".
        /// </summary>
        public const string KeyMenuItemConfigureChartModeRamp = "m_MenuItemConfigureChartModeRamp";

        /// <summary>
        /// Key to access the 'Configure/Chart Mode/Zero Output' menu option. Value: "m_MenuItemConfigureChartModeZeroOutput".
        /// </summary>
        public const string KeyMenuItemConfigureChartModeZeroOutput = "m_MenuItemConfigureChartModeZeroOutput";

        /// <summary>
        /// Key to access the 'Configure/Chart Mode/Full Scale' menu option. Value: "m_MenuItemConfigureChartModeFullScale".
        /// </summary>
        public const string KeyMenuItemConfigureChartModeFullScale = "m_MenuItemConfigureChartModeFullScale";

        /// <summary>
        /// Key to access the 'Configure/Enumeration' menu option. Value: "m_MenuItemConfigureEnumeration".
        /// </summary>
        public const string KeyMenuItemConfigureEnumeration = "m_MenuItemConfigureEnumeration";

        /// <summary>
        /// Key to access the 'Help' menu option. Value: "m_MenuItemHelp".
        /// </summary>
        public const string KeyMenuItemHelp = "m_MenuItemHelp";

        /// <summary>
        /// Key to access the 'Help/PTU Help' menu option. Value: "m_MenuItemHelpPTUHelp".
        /// </summary>
        public const string KeyMenuItemHelpPTUHelp = "m_MenuItemHelpPTUHelp";

        /// <summary>
        /// Key to access the 'Tools' menu option. Value: "m_MenuItemTools".
        /// </summary>
        public const string KeyMenuItemTools = "m_MenuItemTools";
        #endregion - [Menu Option Keys] -
        #endregion - [Keys] -

        #region - [File Extensions] -
        /// <summary>
        /// The file extension associated with an executable file. Value: ".exe".
        /// </summary>
        public const string ExtensionExecutable = ".exe";

        /// <summary>
        /// The file extension associated with a batch file. Value: ".bat".
        /// </summary>
        public const string ExtensionBatchFile = ".bat";

        /// <summary>
        /// The file extension associated with an Access engineering database derived using the Data Dictionary Builder (DDB) utility. Value: ".e1".
        /// </summary>
        public const string ExtensionEngineeringDatabase = ".e1";

        /// <summary>
        /// The file extension associated with an XML data dictionary. Value: ".xml".
        /// </summary>
        public const string ExtensionDataDictionary = ".xml";

        /// <summary>
        /// The file extension associated with a recorded watch file. Value: ".watch".
        /// </summary>
        public const string ExtensionWatchFile = ".watch";

        /// <summary>
        /// The file extension associated with an event log. Value: ".xml".
        /// </summary>
        public const string ExtensionEventLog = ".xml";

        /// <summary>
        /// The file extension associated with a workset file. Value: ".work".
        /// </summary>
        public const string ExtensionWorksetFile = ".work";

        /// <summary>
        /// The file extension associated with a simulated fault log file. Value ".sfl".
        /// </summary>
        public const string ExtensionSimulatedFaultLog = ".sfl";

        /// <summary>
        /// The file extension associated with a fault log file. Value ".flt".
        /// </summary>
        public const string ExtensionFaultLog = ".flt";

        /// <summary>
        /// The file extension associated with the diagnostic help file. Value ".hlp".
        /// </summary>
        public const string ExtensionHelpFile = ".hlp";

        /// <summary>
        /// The file extension associated with the bitmap image files. Value ".bmp".
        /// </summary>
        public const string ExtensionBmp = ".bmp";

        /// <summary>
        /// The file extension associated with the JPEG image files. Value ".jpg".
        /// </summary>
        public const string ExtensionJPeg = ".jpg";
        #endregion - [File Extensions] -

        #region - [Masks] -
        /// <summary>
        /// Mask corresponding to bit 0 of a status byte. Value: 0x01.
        /// </summary>
        public const byte MaskBit0 = 0x01;

        /// <summary>
        /// Mask corresponding to bit 1 of a status byte. Value: 0x02.
        /// </summary>
        public const byte MaskBit1 = 0x02;

        /// <summary>
        /// Mask corresponding to bit 2 of a status byte. Value 0x04.
        /// </summary>
        public const byte MaskBit2 = 0x04;

        /// <summary>
        /// Mask corresponding to bit 3 of a status byte. Value: 0x08.
        /// </summary>
        public const byte MaskBit3 = 0x08;

        /// <summary>
        /// Mask corresponding to bit 4 of a status byte. Value: 0x10.
        /// </summary>
        public const byte MaskBit4 = 0x10;

        /// <summary>
        /// Mask corresponding to bit 5 of a status byte. Value: 0x20.
        /// </summary>
        public const byte MaskBit5 = 0x20;

        /// <summary>
        /// Mask corresponding to bit 6 of a status byte. Value: 0x40.
        /// </summary>
        public const byte MaskBit6 = 0x40;

        /// <summary>
        /// Mask corresponding to bit 7 of a status byte. Value: 0x80.
        /// </summary>
        public const byte MaskBit7 = 0x80;
        #endregion - [Masks] -

        #region - [ASCII Characters] -
        /// <summary>
        /// Byte representation of the escape character.
        /// </summary>
        public const byte AsciiEscape = 27;

        /// <summary>
        /// Byte representation of the delete character.
        /// </summary>
        public const byte AsciiDelete = 127;
        #endregion - [ASCII Characters] -

        #region - [Time] -
        /// <summary>
        /// The number of years in a century. Used to convert year format from 1997, 2010 to 97, 10 etc. Value: 100.
        /// </summary>
        public const int Century = 100;

        /// <summary>
        /// Centisecond (cs) represented as a number of milliseconds. Value: 10.
        /// </summary>
        public const long CentiSecondMs = 10;

        /// <summary>
        /// Second represented as a number of milliseconds. Value: 1,000.
        /// </summary>
        public const long SecondMs = 1000;

        /// <summary>
        /// Minute represented as a number of milliseconds. Value: 60,000.
        /// </summary>
        public const long MinuteMs = 60000;

        /// <summary>
        /// Hour represented as a number of milliseconds. Value: 3,600,000.
        /// </summary>
        public const long HourMs = 3600000;

        /// <summary>
        /// Day represented as a number of milliseconds. Value: 86,400,000.
        /// </summary>
        public const long DayMs = 86400000;
        #endregion - [Time] -

        #region - [IPollTarget] -
        /// <summary>
        /// The default timeout, in ms, on the check to see whether the PauseFeedback signal has been asserted. Value: 1,000.
        /// </summary>
        public const int TimeoutMsPauseFeedback = 1000;

        /// <summary>
        /// The default sleep interval, in ms, on the check to see whether the PauseFeedback signal has been asserted. Value: 10.
        /// </summary>
        public const int SleepMsPauseFeedback = 10;
        #endregion - [IPollTarget] -

        #region - [Formatting] -
        /// <summary>
        /// The value of the FORMATSTRING field of the WATCHVARIABLES table of the data dictionary that indicates that the watch variable is to be displayed in 
        /// hexadecimal format. Value: "hexadecimal".
        /// </summary>
        public const string DDFormatStringHex = "hexadecimal";

        /// <summary>
        /// The format string, converted to lower case, used in the data dictionary to represent a hexadecimal number. Value: "X";
        /// </summary>
        public const string FormatStringHex = "X";

        /// <summary>
        /// The .NET format string used to display the upper and lower bounds. Value: "###,###,##0.####"
        /// </summary>
        public const string FormatStringNumeric = "###,###,##0.####";

        /// <summary>
        /// Identifier used to identify a value as a hexadecimal value. Value: "0x"
        /// </summary>
        public const string HexValueIdentifier = "0x";

        /// <summary>
        /// The CultureInfo string. Value: "en-US".
        /// </summary>
        public const string CultureInfoString = "en-US";

        /// <summary>
        /// The format string to be used when displaying the time in hours:minutes:seconds. Value: "HH:mm:ss".
        /// </summary>
        public const string FormatStringTimeSec = "HH:mm:ss";

        /// <summary>
        /// The format string to be used when displaying the time in hours:minutes:seconds.centi-second. Value: "HH:mm:ss.ff".
        /// </summary>
        public const string FormatStringTimeCs = "HH:mm:ss.ff";

        /// <summary>
        /// The format string to be used when simulating the date information returned by the VCU. Value: "MM/dd/yyyy".
        /// </summary>
        public const string FormatStringDateFromVCU = "MM/dd/yyyy";

        /// <summary>
        /// The format string to be used when displaying list numbered items. Value: "##".
        /// </summary>
        public const string FormatStringListNumbering = "##";
        #endregion - [Formatting] -

        /// <summary>
        /// A new paragraph character string. Value: "\n\n".
        /// </summary>
        public const string NewPara = "\n\n";

        /// <summary>
        /// A new line character. Value: "\n".
        /// </summary>
        public const string NewLine = "\n";

        /// <summary>
        /// A question mark character. Value: "?".
        /// </summary>
        public const string Question = "?";

        /// <summary>
        /// A single space character. Value: " ".
        /// </summary>
        public const string Space = " ";

        /// <summary>
        /// An underscore character. Value: "_".
        /// </summary>
        public const string UnderScore = "_";

        /// <summary>
        /// A colon character. Value: ": ".
        /// </summary>
        public const string Colon = ": ";

        /// <summary>
        /// A comma character. Value: ",".
        /// </summary>
        public const string Comma = ", ";

        /// <summary>
        /// A period character. Value: ".".
        /// </summary>
        public const string Period = ".";

        /// <summary>
        /// A full stop character. Value: ".".
        /// </summary>
        public const string FullStop = ". ";

        /// <summary>
        /// Four consecutive space characters. Value = "    ".
        /// </summary>
        public const string SpaceX4 = "    ";

        /// <summary>
        /// The binding string used when writing the text to the status message labe to ensure that it does not overlay the Image property (Assumes an image of 16x16).
        /// Value: "        ";
        /// </summary>
        public const string BindingLabelStatusMessage = "        ";

        /// <summary>
        /// The binding string used when appending the self test description to the self test number. Value: " | ".
        /// </summary>
        public const string BindingTestNumber = " | ";

        /// <summary>
        /// The binding string used when appending a supplemental message to an existing message. Value: " - ".
        /// </summary>
        public const string BindingMessage = " - ";

        /// <summary>
        /// The binding string used when appending a filename to a path. Value: "\\".
        /// </summary>
        public const string BindingFilename = "\\";

        /// <summary>
        /// The binding string used when appending a sub-directory to a path. Value: "\\".
        /// </summary>
        public const string BindingDirectory = "\\";

        /// <summary>
        /// The text string used to represent a field of a variable that is not defined in the data dictionary. Value: ". . . ".
        /// </summary>
        public const string VariableNotDefinedString = ". . .";

        /// <summary>
        /// The text string used to represent a self test that has not been defined. Value: ". . . ".
        /// </summary>
        public const string TestNotDefinedString = ". . .";

        /// <summary>
        /// The text string used to represent the units field of a variable that is not defined in the data dictionary. Value: ". . . ".
        /// </summary>
        public const string ChartScaleUnitsNotDefinedString = ". . .";

        /// <summary>
        /// The text string used to represent a chart scale value that has not been defined. Value: ". . . ".
        /// </summary>
        public const string ChartScaleValueNotDefinedString = ". . ."; 

        /// <summary>
        /// The watch identifier value that is to be used if the specified watch variable is not defined. Value: 0.
        /// </summary>
        public const short WatchIdentifierNotDefined = 0;

        /// <summary>
        /// The old identifier value that is to be used if the specified watch variable is not defined. Value: 0.
        /// </summary>
        public const short OldIdentifierNotDefined = 0;

        /// <summary>
        /// The number of bits that make up a single byte of data. Value: 8.
        /// </summary>
        public const byte BitsPerByte = 8;

        /// <summary>
        /// Value corresponding to entry not found. Value: -1;
        /// </summary>
        public const short NotFound = -1;

        /// <summary>
        /// Value corresponding to not-defined. Value = -1;
        /// </summary>
        public const short NotDefined = -1;

        /// <summary>
        /// Value corresponding to not-used. Value: -1.
        /// </summary>
        public const short NotUsed = -1;

        /// <summary>
        /// Value corresponding to false. Value: -1.
        /// </summary>
        public const short False = -1;

        /// <summary>
        /// Value corresponding to true. Value: 1.
        /// </summary>
        public const short True = 1;
    }
}
