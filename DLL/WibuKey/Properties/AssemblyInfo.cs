#region --- Revision History ---
/*
 * 
 *  This assembly is the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information. The reproduction, distribution, 
 *  utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited. Offenders will be held liable for 
 *  the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    WibuKey
 * 
 *  File name:  AssemblyInfo.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/19/13    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  02/26/14    1.1     K.McD           1.  Added a number of <remarks> XML tags to the MenuInterfaceWibuKey class.
 *  
 *  05/06/15    1.2     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘WibuBox: [Present | Not Present]’.
 *                                              
 *                                      Modifications
 *                                      1.  Modified MenuInterfaceWibuKey. Rev. 1.2.
 *                                      2.  Modified Resources.resx. Added the following paragraph to the 'MBTWubuBoxUserMessage' string resource "Please shut down the
 *                                          PTU, insert a valid WibuBox security device and then retry this operation".
 *                                          
 *  07/24/15    1.3     K.McD           References
 *                                      1.  An informal review of version 6.11 of the PTU concluded that, where possible - i.e. if the PTU is started from a shortcut
 *                                          that passes the project identifier as a shortcut parameter, the project specific PTU initialization should be carried out
 *                                          in the MDI Form contructor that has the parameter string array as its signature rather than by the LoadDictionary() method.
 *                                          This streamlines the display construction of the Control Panel associated with the R188 project. In the 6.11 implementation
 *                                          the CTA layout is momentarily displayed before the Control Panel is drawn, however by initializing the project specific
 *                                          features in the constructor the Control Panel associated with the R188 project is drawn immediately and the CTA layout
 *                                          is not shown at all.
 *  
 *                                      Modifications
 *                                      1.  MenuInterfaceWibuKey.cs. Rev. 1.3. - Ref.: 1.
 *                                      2.  Resources.resx. - Ref.: 1.
 */

/*
 *  08/11/15    1.4  K.McD              References
 *                                      1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                          from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                      
 *                                          1.  On the R188 project only, all references to 'PTU' are to be replaced with 'PTE' and all occurrences of 'Portable Test Unit'
 *                                              are to be replaced with 'Portable Test Equipment'. Where support for multiple legends is not possible, 'Portable Test
 *                                              Application' is to be used as an alternative to 'Portable Test Unit'/'Portable Test Equipment.
 *                                              
 *                                          2.  The legends on the control panel buttons and labels are to be modified to match those specified in the KRC PTE Uniform
 *                                              Interface Specification Rev. B.
 *                                              
 *                                      Modifications
 *                                      1.  AssemblyInfo.cs. Rev. 1.4. Changed the ApplicationProduct attribute to 'Portable Test Application'.
 *                                      2.  Resources.resx. Removed any reference to PTU in the message box captions e.g. replaced 'PTU - Error' with 'Error'. Also
 *                                          changed 'WibuBox' to 'Wibu-Key' to match the legend used in the KRC PTE Uniform Interface Specification. Rev. B.
 * 
 * 
 */
#endregion --- Revision History ---

using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyCompany("Bombardier Transportation (Holdings) USA Inc.")]
[assembly: AssemblyProduct("Portable Test Application")]
[assembly: AssemblyCopyright("Copyright © 2013 - Bombardier Transportation (Holdings) USA Inc.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguageAttribute("")]

[assembly: AssemblyTitle("Portable Test Application - Wibu-Key Sub-system")]
[assembly: AssemblyDescription("A library of classes to support the WIBU Systems Wibu-Key security USB stick.")]
[assembly: AssemblyConfiguration("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("7C22CB59-4982-4A1F-B1BE-38204B4D3E4F")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.4.0.0")]
