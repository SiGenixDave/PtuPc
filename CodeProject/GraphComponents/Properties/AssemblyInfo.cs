#region --- Revision History ---
/*
 * 
 *  This assembly was originally written by Anup. V (anupshubha@yahoo.com) under the terms of the Code Project Open License (CPOL). 
 *  The CPOL is intended to provide developers, who choose to share their code, with a license that protects them and provides users 
 *  of their code with a clear statement regarding how the code can be used.
 * 
 *  Under the terms and conditions of the CPOL, all derivative work must also be developed under the same licence agreement. The full 
 *  CPOL terms and conditions are given in the file CPOL.html located in the 'Solution Items' directory.
 * 
 *  (C) The Code Project Open Source Licence Agreement
 *
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  AssemblyInfo.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  03/29/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  10/15/10    1.1     K.McD           1.  Link to the SolutionInfo.cs file broken so that the configuration management of this subsystem is now controlled independently.
 *                                      2   Set the lower parameter limit of FindY() to 3 data intervals so that the plotter does not display a blank line during the write
 *                                          watch variable operation.
 * 
 *  02/28/10    1.2     K.McD           1.  Changed the modifier associated with the m_GraphAreaNormalColor member variable of the Plotter class.
 * 
 *  03/28/11    1.2.1   K.McD           1.  Added the ScreenCapture class.
 * 
 *  03/31/11    1.2.2   K.McD           1.  Included support in the Plotter user control to write the y axis values and the cursor value in the specified format.
 *                                      2.  Made the DrawYAxisValues() method of the Plotter user control protected and virtual to allow the enumerator plotter child 
 *                                          class to be implemented as this user control does not display any y axis values.
 *                                          
 *  05/23/11    1.3     K.McD           1.  Migrated to Visual Studio 2010.
 *                                      2.  Corrected the revision history asssociated with a number of classes.
 *                                      3.  Corrected a number of XML tags.
 *                                      
 *  06/30/11    1.4     K.McD           1.  Target framework updated to .NET Framework 4.0.
 *  
 *  07/29/11    1.4.1   K.McD           1.  Corrected a number of XML documentation tags in the ScreenCapture class.
 *                                      2.  Modified the project file.
 *                                              (a) Removed the Trace constant from the Release build.
 *                                              (b) Now generates an XML documentation file.
 *
 *	09/26/11	1.4.2	Sean.D	        1.	Plotter:
 *	                                            (a) Modified the FindY() method to make use of the return value to find the next lowest value without using recursive 
 *	                                                calls.
 *	                                            (b) Modified the DrawXY() method to take into account the changes to the signature of the FindXY() method.
 *                                              (c) Changed the name of a number of controls.
 *                                              
 *                                      2.  LogicAnalyzer:
 *                                              (a) Modified the DrawXY() method to take into account the changes to the signature of the FindXY() method.
 *                                              (b) Auto-modified as a result of name changes to a number of controls in the parent class. 
 *	
 *	10/05/11	1.4.3	Sean.D			Made the following changes to Plotter.cs and its Designer class:
 *										1.	Added the m_MultiCursor member variable and the corresponding property of MultiCursor to allow the PlotControlLayout to
 *											change certain properties of the methods when multiple cursors are displayed.
 *										2.	Modified Plotter_MouseLeave to not set the hover coordinates to zero when fired in MultiCursor is true. This allows cursor
 *											display even after leaving the plot.
 *										3.	Removed Plotter_MouseMove so that PlotterControlLayout handles it.
 *										4.	Added MouseHoverCoordinates property and changed references to m_MouseHoverCoordinates to instead refer to the property.
 *										5.	Modified UpdateDisplay to check visibility before choosing to refresh the display on advice from Keith.
 *										
 *  10/10/11    1.4.4   K.McD           1.  Minor modifications to the multi cursor implementation associated with revision 1.4.2 of the Plotter class.
 *  
 *                                              (a) Added the MultiCursorMouseMove static event.
 *                                              (b) Modified the Plotter_MouseLeave() and Plotter_MouseMove() event handlers to raise the MultiCursorMouseMove event 
 *                                                  if the multiple cursor property is asserted.
 *                                              (c) Added the event handler for the MultiCursorMouseMove event. This updates the mouse hover coordinates with the 
 *                                                  values specified in the event argument and re-draws the XYText box and cross hairs.
 *                                              (d) Modified the Cancel context menu option event handler to refresh the display.
 *                                              (e) Added the OnMultiCursorMouseMove() event manager method.
 *                                              (f) Removed the check as to whether the Y hover coordinates were within the bounds of the control graph area from 
 *                                                  the DrawXYText() and DrawCrossHair() methods. This modification was carried out as, firstly, the Y coordinate is 
 *                                                  not used and secondly, when displaying multiple/simultaneous cursors the Y coordinate of the mouse cursor may 
 *                                                  well fall outside the graph area of the individual control as common coordinates are used and the heigh of each 
 *                                                  control may well be different.
 *                                              (g) Modified the DrawCrossHair() method to use the m_CrossHairColor variable to specify the color of the cross hair 
 *                                                  and set this color to be Royal Blue.
 *                                                  
 *                                      2.  Removed the check as to whether the Y hover coordinates were within the bounds of the control graph area from the 
 *                                          DrawXYText() method of the LogicAnalyzer class, see (f) of item (1).
 *                                          
 *  10/13/10    1.4.5   K.McD           1.  Rationalized the Resources.resx file and renamed a number of resources. Note: No revision history updates were carried out 
 *                                          on individual files that were auto-updated as a result of the resource name changes.
 *                                          
 *  10/26/11    1.4.6   K.McD           1.  Modified the Plotter class - version 1.4.4.
 *                                          1.  Modified the design to enter the state which allows the user to modify the range as soon as the control has received 
 *                                              focus rather than requiring the user to initiate this via a context menu option.
 *                                                  
 *                                          2.  Added the support for multiple user control selection. Added the SelectedControlList property, a generic list of selected 
 *                                              user controls.
 * 
 *  11/14/11    1.4.7   Sean.D		    1.	AxisLine.cs, Gridline.cs, LogicAnalyzer.cs, Plotter.cs:	Modified methods to use "using" to ensure Pen, Graphics, StringFormat,
 *                                          and Brush object disposal.
 *										2.	Plotter.cs:	Modified DrawXYText to use "using" to ensure proper disposal of StringFormat and Pen objects.
 *										3. 		"		Modified DrawTripTime and DrawCrossHair to use "using" to ensure proper disposal of Pen objects.
 *										4. 		"		Modified DrawSelectedArea to use "using" to ensure proper disposal of Brush objects and added braces to the
 *										                return statements at the top.
 *										5.		"		Modified DrawXAxisValues and DrawYAxisValues to use "using" to ensure proper disposal of Brush and StringFormat
 *										                objects.
 *										6. Plotter.Designer.cs:	Modified Dispose by moving the removal of the MultiCursorMouseMove event into the Windows Forms section
 *										    and in its place, adding a call to clear the selected control list.
 *										
 *  11/23/11    1.4.8   K.McD           1.  Implemented the standard Dispose()/Cleanup() implementation for the Plotter, LogicAnalyzer and Graph classes.
 *                                      2.  Ensured that all event handler methods were detached and that the component designer variables were set to null on disposal.
 *                                      
 *  12/01/11    1.4.9   K.McD           1.  Modified the Plotter class.
 *                                              1.  Added to ZoomedStartTime and ZoomedStopTime static fields to the PlotterRangeSelection structure to record the 
 *                                                  start and stop times when the plot has been successfully zoomed.
 *                                          
 *                                              2.  Modified the section of code responsible for resetting the plotter in the Plotter_Leave(), Plotter_MouseUp()  
 *                                                  and m_ToolStripMenuItemCancel_Click() methods to set the start and stop times back to the initial start and stop 
 *                                                  times or the zoomed start and stop times depending upon whether the plot is currently zoomed or not.
 *                                          
 *                                              3.  Modified the m_ToolStripMenuItemResetTimeSpan_Click() method to initialize the zoomed start and stop time fields to 
 *                                                  DateTime.MinValue.
 *                                      
 *                                              4.  Modified the m_ToolStripMenuItemZoom_Click() method to update the zoomed start and stop time fields.
 *                                              
 *  07/12/13    1.5     K.McD           1.  Unticked the 'Enable the Visual Studio hosting process' and the 'Enable unmanaged code debugging' boxes on the Debug tab page 
 *                                          of the project Properties page for the Release configuration. Note: Both are enabled for the Debug configuration.
 *                                          
 *  03/11/15    2.0     K.McD           1.  Changed the: AssemblyProduct, AssemblyTitle and AssemblyVersion assembly attributes.
 *  
 *  05/11/15    2.1     K.McD           References
 *                                      1.  SNCR - R188 PTU [20-Mar-2015] Item 14. If the data stream downloaded from the VCU cannot be plotted, rather than throwing
 *                                          an exception, the Plotter control should simply display an error message on the plot.
 *                                          
 *                                      Modifications
 *                                      1.  Resources.resx.
 *                                      2.  Plotter.cs. Rev. 2.0.
 *                                      3.  LogicAnalyzer.cs. Rev. 2.0.
 */

/*
 *  08/11/15    2.2    K.McD            References
 *                                      1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                          from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                      
 *                                          1.  On the R188 project only, all references to 'PTU' are to be replaced with 'PTE' and all occurrences of 'Portable Test Unit'
 *                                              are to be replaced with 'Portable Test Equipment'. Where support for multiple legends is not possible, 'Portable Test
 *                                              Application' is to be used as an alternative to 'Portable Test Unit'/'Portable Test Equipment.
 *                                              
 *                                      Modifications
 *                                      1.  AssemblyInfo.cs. Rev. 2.2. Changed the ApplicationProduct attribute to 'Portable Test Application'.
 */
#endregion --- Revision History ---

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Resources;

// General Information about an assembly is controlled through the following set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyCompany("Code Project")]
[assembly: AssemblyProduct("Portable Test Application")]
[assembly: AssemblyCopyright("Code Project Open Source Licence Agreement (CPOL)")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguageAttribute("")]

// General Information about an assembly is controlled through the following set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Portable Test Application - Graphical Plotter User Control")]
[assembly: AssemblyDescription("A collection of user controls to display real time and historic data.")]
[assembly: AssemblyConfiguration("")]

// Setting ComVisible to false makes the types in this assembly not visible to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("789FDAC0-E7A4-4aa1-B3EB-4E3E18B7B50C")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers by using the '*'.

[assembly: AssemblyVersion("2.2.0.0")]