#region --- Revision History ---
/*
 * 
 *  This assembly is the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information. The reproduction,
 *  distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    SelfTest
 * 
 *  File name:  AssemblyInfo.cs
 * 
 *  Revision History
 *  ----------------
 */
#region - [1.0 to 1.4] -
/*
 *  Date        Version Author          Comments
 *  05/25/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/22/11    1.1     K.McD           1.  Added the PTUDLL32SelfTest and CommunicationSelfTest classes to support the PTUDLL32 communication methods
 *                                          associated with the self test sub-system.
 *                                          
 *                                      2.  Rationalized the directory structure to be consistent with the other sub-systems.
 *                                      
 *  06/30/11    1.2     K.McD           1.  Target framework updated to .NET Framework 4.0.
 *                                      2.  Removed the 'Not yet implemented' message box from the MenuInterfaceSelfTest class.
 *
 *  07/11/11    1.3     K.McD           1.  Major changes to the SelfTest subsystem, included support for logic and passive tests.
 *                                      2.  Changes to a number of XML tags and resources.
 *                                      
 *  07/13/11    1.3.1   K.McD           1. Added the StopHS resource.
 *  
 *  07/25/11    1.3.2   K.McD           1.  Updated to support off-line mode. Modified the signature of the constructors associated with the 
 *                                          FormViewTestResult and CommunicationSelfTest classes to use the ICommunicationParent interface.
 *                                      2.  Modified the GetSelfTestResult() definition in the PTUDLL32 class and implementation in the 
 *                                          CommunicationSelfTest class to pass the self test variable values as a byte array rather than an array of 
 *                                          InteractiveResults_t structures as the structure storage in memory is different in C#.
 *                                      3.  Modified the FormTestListDefine class to display the help window within the bounds of the GroupBox
 *                                          controls.
 *                                      4.  Major modifications to the FormViewTestResults class to include support for the interactive tests.
 *                                      
 *  07/26/11    1.3.3   K.McD           1.  Added the m_PanelHelpWindowTestList and m_PanelHelpWindowAvailable panels to the FormTestListDefine class
 *                                          to help position the Windows help window.
 *                                          
 *                                      2.  Entered the FormViewTestResults class into TortoiseSVN for security reasons, however, the form is not yet
 *                                          complete.
 *                                      
 *  07/29/11    1.3.4   K.McD           1.  Modified the FormTestResult class.
 *                                              (a) Replaced the ToolStripComboBox control with a ComboBox control.
 *                                              (b) Added support to allow the user to view individual bits of the bitmask self test variables.
 *                                              (c) Now uses a GroupBox control to contain the interactive test information.
 *                                              (d) Initialized the loop count to 1 every time the loop forever CheckBox control is selected.
 *                                              (e) Added screen capture support.
 *                                              
 *                                      2.  Modified the project file to generate a self test documentation XML file.
 *                                      
 *  08/04/11    1.3.5   K.McD           1.  Added the ConvertUInt32() method to the FormViewTestResults class to convert the UInt32 data corresponding
 *                                          the the self test variable values to the appropriate signed data type.
 *                                      2.  Restructured the FormViewTestResults class to make it easier to collect the results on a seperate thread on
 *                                          a later date, if deemed necessary.
 *                                          
 *  08/10/11    1.3.6   K.McD           1.  Set the BackgroundImage property of the ToolStrip control associated with the Abort and Continue buttons
 *                                          for the interactive tests in the FormViewTestResults class.
 *                                      2.  Minor adjustments to the Size and Location property of one or more controls in the FormTestListDefine and 
 *                                          FormViewTestResults classes.
 *                                      3.  Set the BackColor property of one or more controls in the FormViewTestResults class to Transparent.
 *                                      
 *  08/18/11    1.3.7   K.McD/S.D       1.  Included support for offline mode. Added the CommunicationSelfTestOffline class.
 *  
 *                                      2.  Added support for offline mode. Modified the constructor of the FormViewTestResults class to conditionally
 *                                          choose CommunicationSelfTest or CommunicationSelfTestOffline depending upon the current mode.
 *
 *                                      3.  Modified the implementation of the FormViewTestResults class to collect the test results using a Windows
 *                                          timer.
 *                                      
 *  08/24/11    1.3.8   K.McD           1.  Modified the FormViewTestResults class as follows: (a) now disables the Execute button if in online mode,
 *                                          (b) changed the image associated with the Execute button and (c) correctly sets the mode before exiting the
 *                                          self test screen.  Also changed the width of the panel associated with the interactive tests and modified
 *                                          the GetSelfTestMessage() method to set the message to be Resources.EMResultFailed regardless of the state
 *                                          of the result parameter.
 *                                          
 *                                      2.  Removed support for debug mode from the CommunicationSelfTestOffline class to be consistent with other
 *                                          sub-systems.
 *                                      
 *  09/30/11    1.3.9   K.McD           1.  FormTestListDefine - Modified the Remove() method to use the RemoveAt() method of the ListBox class rather
 *                                          than the Remove() method. This is necessary when the ListBox may contain multiple copies of the same item
 *                                          as the RemoveAt will target the specified index whereas the Remove will target the first occurrence of the
 *                                          item, which may not necessarily be the selected item.
 *                                          
 *  10/13/11    1.3.10  K.McD           1.  Rationalized the Resources.resx file and renamed a number of resources. Note: No revision history updates
 *                                          were carried out on individual files that were auto-updated as a result of the resource name changes.
 *                                          
 *  10/26/11    1.3.11  K.McD           1.  Modified the FormTestListDefine class. Enabled the TabStop property of the OK button.
 *                                      2.  Modified the FormViewTestResults class - SNCR002.41. Added a check to the F2 function key event handler to
 *                                          ensure that the event handler code is ignored if the Enabled property of the control is not asserted.
 *                                          
 *  11/23/11    1.3.12  K.McD           1.  Modified the FormViewTestResults class.
 *                                              (a) Ensured that all event handler methods were detached.
 *                                              (b) Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced
 *                                                  after the Close() method had been called.
 *                                                  
 *  12/01/11    1.3.14  K.McD           1.  Asserted the Visible property of the ToolStrip control associated with the interactive test Abort/Continue
 *                                          buttons in the FormViewTestResults class.
 *
 *  06/04/12    1.3.15  Sean.D          1.  Within FormViewTestResults.cs, modified ExitSelfTestTask() to drop us into Offline mode if there's a
 *                                          communications error.
 *  
 *  07//04/13   1.4     K.McD           1.  Modified the constructor of the FormViewTetResults class such that the call to ExitSelfTestTask(), issued
 *                                          in case the Self Test task on the hardware is already running, is not made if the project identifier
 *                                          corresponds to the NYCT - R188 project i.e. the embedded software is running on a COM-C device. If this
 *                                          call is made while connected to the COM-C hardware the PTU does not enter self test mode and hangs.
 *                                          
 *                                      2.  Modified ProcessSTCountResult() and ProcessSTListResult() such that any value for the testResult parameter
 *                                          other than ResultPassed (0) is treated as a fail.
 *                                          
 *                                      3.  Unticked the 'Enable the Visual Studio hosting process' and the 'Enable unmanaged code debugging' boxes on
 *                                          the Debug tab page of the project Properties page for the Release configuration. Note: Both are enabled for
 *                                          the Debug configuration.
 *                                          
 *  08/07/13    1.4.1   K.McD           1.  Modified the MenuInterfaceSelfTest class. In those methods where it is applicable, the cursor was set to
 *                                          the wait cursor after the call to the MainWindow.CloseChildForms() method as if any child forms are open,
 *                                          the cursor may be set to the default cursor as part of the call to the Exit() method within the child
 *                                          form.                                       
 */
#endregion - [1.0 to 1.4] -

#region - [1.5] -
/*
 *  03/22/15    1.5     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                              to support both 32 and 64 bit architecture.
 *  
 *                                          2.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                          
 *                                              1.  MOC-0171-06. All references to fault logs, including menu options and directory 
 *                                                  names to be replaced by 'data streams' for all projects.
 *                                                  
 *                                              2.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                                  options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                                  
 *                                              3.  MOC-0171-41. Wherever possible, i.e. where there is room within the existing control to accommodate the text
 *                                                  ‘Vehicle Control Unit’ without having significant impact on the screen layout, VCU will be replaced with 
 *                                                  Vehicle Control Unit.
 *                                                  
 *                                      2.  Updated Resources with a number of premium 28 x 28 images purchased from Iconfinder.
 *                                              
 *                                  Modifications
 *                                  1.  Renamed PTUDLL32SelfTest.cs to VcuCommunication32SelfTest.cs. Rev. 1.2. - Ref. 1.1.
 *                                  2.  Renamed PTUDLL64SelfTest.cs to VcuCommunication64SelfTest.cs. Rev. 1.1.  - Ref.: 1.1.
 *                                  3.  Modified CommunicationSelfTest.cs. Rev. 1.2. - Ref.: 1.1.
 *                                  4.  Modified Resources.res. - Ref. 1.2.1, 1.2.2, 1.2.3, 2. Also changed ToolTipText to be contained within
 *                                      square brackets e.g. F4 - [Continue].
 *                                  5.  Modified: FormTestListDefine.Designer.cs. Rev. 1.3. - Ref.: 1.2.2.
 *                                  6.  Modified: FormViewTestResults.cs. Rev. 1.10, FormViewTestResults.Designer.cs. Rev. 1.4. - Ref.: 1.2.2, 2.
 */
#endregion - [1.5] -

#region - [1.6] -
/*
 *  07/23/15    1.6     K.McD       References
 *                                  1.  Part 1 of the upgrade to the Chicago 5000 PTU software that allows the user to download the configuration and help files for
 *                                      a particular Chicago 5000 vehicle control unit (VCU) via an ethernet connection to the FTP (File Transfer Protocol) server
 *                                      running on the VCU. The scope of Part 1 of the upgrade is defined in purchase order 4800011369-CU2 07.07.2015.
 *                                      
 *                                      The upgrade is implemented in two parts, the first part, Part 1, replaces the existing screens and logic with those outlined
 *                                      in slides 6, 7, 8 and 9 of the PowerPoint presentation '076_CTA - PTU file pullback from VCU - 20150127.pptx', but does NOT
 *                                      implement the file transfer; it merely calls an empty external batch file from within the PTU application. The second stage,
 *                                      Part 2, implements the batch file that downloads the configuration and help files from the Vehicle Control Unit (VCU) to the
 *                                      appropriate directory on the PTU computer. As described in the PowerPoint Presentation, this download is only carried out if the
 *                                      appropriate configuration file is not already present on the PTU computer.
 *                                      
 *                                  2.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 24. On selecting the ‘Exit’ function key on the ‘Diagnostics/Event Log’ and
 *                                      the ‘Diagnostics/Self Tests’ screens, the cursor doesn’t go to the Cursors.WaitCursor cursor on the R188 project.
 *                                      
 *                                  Modifications
 *                                  1.  FormViewTestResults.cs. Rev. 1.11. - Ref.: 1, 2.
 */
#endregion - [1.6] -

#region - [1.7] -
/*
 *  08/11/15    1.7    K.McD        References
 *                                  1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                      from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                      
 *                                      1.  On the R188 project only, all references to 'PTU' are to be replaced with 'PTE' and all occurrences of 'Portable Test Unit'
 *                                          are to be replaced with 'Portable Test Equipment'. Where support for multiple legends is not possible, 'Portable Test
 *                                          Application' is to be used as an alternative to 'Portable Test Unit'/'Portable Test Equipment.
 *                                              
 *                                  Modifications
 *                                  1.  AssemblyInfo.cs. Rev. 1.7. Changed the ApplicationProduct attribute to 'Portable Test Application'.
 *                                  2.  Resources.resx. Removed any reference to PTU in the message box captions e.g. replaced 'PTU - Error' with 'Error'.
 */
#endregion - [1.7] -
#endregion --- Revision History ---

using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyCompany("Bombardier Transportation (Holdings) USA Inc.")]
[assembly: AssemblyProduct("Portable Test Application")]
[assembly: AssemblyCopyright("(C) 2010 - 2015 Bombardier Transportation (Holdings) USA Inc.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguageAttribute("")]

[assembly: AssemblyTitle("Portable Test Application - Self Test Sub-system")]
[assembly: AssemblyDescription("A library of classes to support diagnostic self tests.")]
[assembly: AssemblyConfiguration("")]

// Setting ComVisible to false makes the types in this assembly not visible to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("BCDCC21D-4A3E-464d-A791-C246926FA981")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers by using the '*'.

[assembly: AssemblyVersion("1.7.0.0")]