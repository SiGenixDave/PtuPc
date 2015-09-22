#region --- Revision History ---
/*
 * 
 *  This assembly is the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information. The reproduction,
 *  distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Watch
 * 
 *  File name:  AssemblyInfo.cs
 * 
 *  Revision History
 *  ----------------
 */

#region - [1.0 - 1.22] -
/*
 *  Date        Version Author          Comments
 *  03/29/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  10/15/10    1.1     K.McD           1.  Link to the SolutionInfo.cs file broken so that the configuration management of the watch subsystem is now controlled independently.
 *                                      2.  Created the Forms, UserControls and Communication directories and sorted the classes associated with the project into the relevant
 *                                          directories.
 *                                      3.  Included support for the CommunicationWatch class instead of the ICommunication interface.
 *                                      4.  Dialog box forms that communicate with the vehicle control unit now inherit from the generic version of FormPTUDialog<>.
 *  
 *  10/19/10    1.2     K.McD           1.  Modified: all forms, user-controls, CommunicationWatch, PlottterControlLayout and WatchControlLayout classes to access the fields of
 *                                          the watch variable data table contained within the data dictionary using the static class 'Lookup' rather than WatchTable.
 * 
 *  10/22/10    1.3     K.McD           1.  Changes resulting from the renaming of the 'LookupTable' class and properties.
 * 
 *  10/25/10    1.4     K.McD           1.  Added support for the static ActiveWorksetModified event associated with the WorksetManager class.
 * 
 *  10/29/10    1.5     K.McD           1.  SNCR 001.30. Include facility to allow the user to modify the workset that is currently on display.
 * 
 *  11/02/10    1.6     K.McD           1.  Minor changes to a number of XML tags and variable names.
 * 
 *  11/04/10    1.7     K.McD           1.  Minor changes to variable names resulting from changes in Common.dll - Ver. 1.7.
 * 
 *  11/16/10    1.8     K.McD           1.  Created FormViewDataStreamSimulatedFaultLog.cs and FormViewDataStreamWatchRecording.cs child classes.
 *                                      2.  Renamed a number of variables and XML tags - no functional changes.
 *                                      3.  Now uses a data stream member variable to generate and store the simulated fault log and recorded watch variables.
 *                                      4.  Changes resulting from changes to the watch file structure.
 *                                      5.  On the form used to display watch variables, disable the edit workset function key while data is being recorded.
 *                                      6.  Use the checked property of function keys, where appropriate, to give operator feedback.
 *                                      7.  On the form used to display watch variables, disable the edit workset function key if the baseline workset is the active 
 *                                          workset.
 * 
 *  11/17/10    1.9     K.McD           1.  Modifications resulting from the replacement of the WorksetManager class.
 * 
 *  11/19/10    1.10    K.McD           1.  Added support to configure the watch worksets using forms derived from FormWorksetManager and FormWorksetDefine.
 * 
 *  11/26/10    1.11    K.McD           1.  Bug fix - SNCR001.60. Modified the FormWorksetDefineWatch class so that the watch item array is copied using the Array.Copy() method.
 * 
 *  12/01/10    1.12    K.McD           1.  Bug fix - SNCR001.61. Ensured that the Checked property of the F3-Save function key associated with the form used to show the 
 *                                          watch variables is not asserted until after a valid file name has been selected.
 * 
 *  01/06/11    1.13    K.McD           1.  Modified the FormWatchView class to included the MutexCommunicationInterface property to help control access to the 
 *                                          communication interface.
 *                                      2.  Now dynamically updates the ToolStripComboBox control associated with the FormWatchView class with the new worksets if any 
 *                                          of the worksets are modified.
 *                                      3.  Polling of the target is now controlled using the PollScheduler class.
 * 
 *  01/12/11    1.14    K.McD           1.  Bug fix SNCR001.84. Added the second parameter to all calls to the System.Threading.WaitHandle.WaitOne() method, as advised by 
 *                                          the following blog entries: 
 *                                              (1) http://www.mikeplate.com/missingmethodexception-and-waitone/ and (2) 
 *                                              (2) http://blog.darrenstokes.com/2009/03/30/watch-out-for-those-waitone-overloads-when-you-need-backwards-compatibility
 * 
 *  01/14/11    1.15    K.McD           1.  Added a mutex to control read/write access to the communication port when trying to restore communication in the Run() method 
 *                                          of the ThreadPollWatch class.
 * 
 *  01/16/11    1.16    K.McD           1.  Bug fix - SNCR001.86. Under the previous implementation, if a workset contained less than WatchSize watch variables, there was 
 *                                          a possibility that the display values associated with one or more watch variables would, under certain circumstances, be 
 *                                          incorrect. This bug was caused because not all elements of the m_WatchElements array were updated in the SetWatchElements() 
 *                                          method if the new workset being displayed contained less than WatchSize entries.
 * 
 *                                          This problem was addressed by making the following modifications:
 *                                              (a) Used the Array.Copy() method to copy the watchElementList parameter to the initialized watchElements array in the 
 *                                                  CommunicationWatch.SetWatchElements() method. This ensures that all WatchSize watch elements are updated when making a 
 *                                                  call to the PTUDLL32.SetWatchElements() method. The watch identifiers of those watch elements that are not used are 
 *                                                  set to 0.
 * 
 *                                              (b) Modified the for loop within the SetWatchElements() method of the CommunicationWatch class to update all elements of 
 *                                                  the m_WatchElements array.
 * 
 *                                      2.  Removed the SimulateCommunicationLink conditional compilation.
 * 
 *  01/26/11    1.17    K.McD           1.  Minor modifications to accommodate the changes introduced in version 1.10.10 of Common.dll.
 * 
 *  01/27/11    1.18    K.McD           1.  Bug fix - SNCR001.88. Modified the ShowDataStreamFile() method of the MenuInterfaceWatch class such that the selected data stream 
 *                                          file is only shown if the selected file is valid.
 * 
 *                                      2.  Modified the: CommunicationWatch, FormWatchView and ThreadPollWatch classes to accommodate the communication mutex introduced 
 *                                          into the Common.CommunicationParent class in version 1.11 of Common.dll.
 * 
 *  02/04/11    1.19    K.McD           1.  Implemented the following modifications to the FormViewWatch class:
 *                                              (a) Standardized the function key event handlers to: display the wait cursor, enable the Checked property of the 
 *                                                  function key and clear any status message.
 *                                              (b) Modified the Exit() method to check whether recording is in progress and, if so, simulate the user pressing the 
 *                                                  F3-Record key to stop recording before closing the form.
 *                                              (c) Modified the F3-Record function key event handler so that the escape key is no longer disabled while in record mode.
 *                                              (d) Modified the F3-Record function key event handler so that the user does not have access to any of the main menu 
 *                                                  options while in record mode.
 * 
 *  02/14/11    1.19.1  K.McD           1.  Included support for debug mode.
 *                                      2.  Modified the FormWatchReplay class so that it did not modify the MainWindow.TaskProgressBar.Visible property as the 
 *                                          progress bar is now permanently on display.
 * 
 *  02/21/11    1.19.2  K.McD           1.  Modified the F3_Click() and SaveSimulatedFaultLog()methods of the FormWatchView class to included the name of the workset 
 *                                          in the default filename.
 * 
 *  02/27/11    1.19.3  K.McD           1.  Bug fix SNCR001.105. Modified the event handler associated with the F5-Edit function key of the FormViewWatch class to check 
 *                                          that the user has sufficient privileges before showing the dialog box that allows the user to edit the current workset.
 *                                      2.  Bug fix SNCR001.105. Added the UpdateMenu() method to the FormViewWatch class to process any form specific changes to the menu 
 *                                          system or function keys resulting from a change in the security level of the user.
 *                                      3.  Modified the FormViewWatch class such that the progress bar is only displayed when data is being recorded.
 *                                      4.  Modified the LoadWorkset() method of the FormViewWatch class such that the F5-Edit function key is only enabled if the security 
 *                                          level allows the user to modify the current workset.
 *                                      5.  Removed the reference to the 'Watch/Replace Watch' menu option as this is no longer used.
 * 
 *  02/28/11    1.19.4  K.McD           1.  Modified the AutoScaleMode property of the FormWorksetManagerWatch class to Inherit.
 * 
 *  03/17/11    1.19.5  K.McD           1.  Auto-modified to support the name changes to: (a) a number of properties associated with the Common.Security class and (b) 
 *                                          a method in the Common.FormPTU class.
 * 
 *  03/28/11    1.19.6  K.McD           1.  Bug fix SNCR001.112. Modified the FormWorksetDefineWatch class to use the old identifier field of the data dictionary, rather 
 *                                          than the watch identifier field, to define the watch variables that are to be included in the workset as these remain 
 *                                          consistent following a data dictionary update.
 * 
 *                                      2.  Minor changes as a result of name changes to a number of methods and properties of external and internal classes.
 * 
 *  04/08/11    1.19.7  K.McD           1.  Minor modifications to support the screen capture feature.
 * 
 *  04/27/11    1.19.8  K.McD           1.  Added support to allow the user to configure the chart recorder.
 * 
 *  04/27/11    1.20    K.McD           1.  Modified the SetChartModeRadioButtonsAndMenuOptions() method of the FormConfigureChartRecorder class to use menu keys rather 
 *                                          than strings to define the menu options that are to be modified.
 *                                          
 *  05/23/11    1.21    K.McD           1.  Significant modifications to accommodate the changes to the FormWorksetDefine parent class introduced in version 1.16 of 
 *                                          Common.dll. These were made to allow the user to define, store and download the chart recorder scaling information associated 
 *                                          with each chart recorder workset.
 *                                          
 *  05/24/11    1.21.1  K.McD           1.  Moved the SetChartMode(), SetChartIndex and SetChartScale() methods from the CommunicationWatch class to the 
 *                                          Common.CommunicationParent class.
 *                                      2.  Corrected the chart recorder scaling assignments in the ConvertToWorkset() method of the FormWorksetDefineChartRecorder class. 
 *                                      3.  Corrected the chart recorder scaling assignments in the ConvertToWorkset() and WatchItemsAddRange() methods of the 
 *                                          FormConfigureChartRecorder class.
 *                                      4.  Added the overridden DownloadWorkset() method to the FormConfigureChartRecorder class and refactored class to use this method.
 *                                      
 *  05/26/11    1.21.2  K.McD           1.  Auto-modified the FormWorksetDefineWatch class as a result of name changes to a number of constants in the CommonConstants class.
 *  
 *                                      2.  Modified the FormConfigureChartRecorder class to ensure that the string representing 'not defined' is displayed in the 
 *                                          ListBox controls, if the upper and lower Y axis values are not defined, rather than 'NaN'. Also ensured that all ListBox 
 *                                          controls associated with the workset are cleared if the user opts to create a new workset. Finally, changed the 
 *                                          constructor to use the method that includes a check on the chart recorder scaling values when comparing the 
 *                                          downloaded workset against existing worksets.
 *                                          
 *                                      3.  Modified the FormWorksetDefineChartRecorder class to ensure that the string representing 'not defined' is displayed in the 
 *                                          ListBox controls, if the upper and lower Y axis values are not defined, rather than 'NaN'.
 *                                          
 *  06/21/11    1.21.3  K.McD           1.  SNCR001.141. Removed the feature that allows the user to modify the chart mode from the form that is used to configure the 
 *                                          chart recorder, this can now only be carried out from the main menu.
 *                                      
 *                                      2.  SNCR001.142. Modified the design of the form that is used to configure the chart recorder such that the default chart 
 *                                          recorder workset is presented for download to the VCU when the form is first shown.
 *                                          
 *  06/24/11    1.21.4  K.McD           1.  Modified the AutoScaleMode property of the FormWorksetConfigureChartRecorder, FormWorksetDefineChartRecorder and 
 *                                          FormWorksetDefineWatch classes to AutoScaleMode.Font to ensure that the vertical scrollbar on the row header and 
 *                                          Y axis limit ListBox controls isn't activated if the font is changed.
 *                                          
 *  06/30/11    1.21.5  K.McD           1.  Target framework updated to .NET Framework 4.0.
 *                                      2.  Added an number of string resources.
 *                                      3.  Modified the following classes to include support for hexadecimal chart scale values: FormChangeChartScale, 
 *                                          FormConfigureChartRecorder and FormWorksetDefineChartRecorder.
 *                                          
 *  07/11/11    1.21.6  K.McD           1.  Set the TabStop property to false in class FormWorksetDefineChartRecorder.
 *                                      2.  Skipped call to the Escape_Click() method of the FormViewWatch class if the esacpe key is enabled.
 *                                      
 *  07/25/11    1.21.7  K.McD           1.  Updated to support off-line mode.
 *                                              (a) Added the SetChartMode() method to the MenuInterfaceWatch class and modified the ConfigureChartModeFullScale, 
 *                                                  ConfigureChartModeZeroOutput(), ConfigureChartModeRamp() and ConfigureChartModeData() methods to call this method. 
 *                                                  This method checks the mode of the PTU and instantiates the appropriate ICommunicationWatch interface.
 *                                              (b) Modified the signature of the constructors associated with the FormViewWatch, FormConfigureChartRecorder and 
 *                                                  CommunicationWatch classes to use the ICommunicationParent interface.
 *                                              (c) Modified the constructor of the FormViewWatch class to check the mode of the PTU and instantiate the appropriate 
 *                                                  ICommunicationWatch interface.
 *                                              (d) Added the CommunicationWatchOffline class.
 *                                              
 *  08/04/11    1.21.8  K.McD           1.  Minor changes to the Pause and PauseFeedback properties of the FormViewWatch and ThreadPollWatch classes.
 *  
 *  08/05/11    1.21.9  K.McD           1.  Modified the FormConfigureChartRecorder class to reflect the changes to the Common.FormConfigure class introduced in 
 *                                          version 1.16.10 of Common.dll.
 *                                          
 *                                              (a) Added an override to the FormConfigure.CompareWorkset() method to include a check on the chart scaling information 
 *                                                  when comparing the worksets.
 *                                          
 *                                              (b) No longer de-registers the DataUpdate event in the Cleanup() method as this is not required.
 *                                              
 *  08/17/11    1.21.10 Sean.D          1.  Added support for offline mode. Modified the constructors of the FormConfigureChartRecorder and FormViewWatch classes to 
 *                                          conditionally choose CommunicationWatch or CommunicationWatchOffline depending upon the current mode.
 *                                          
 *                                      2.  Added the code for the SetWatchElement() method to the CommunicationWatchOffline class.
 *                                      
 *  08/24/11    1.21.11 K.McD           1.  Added support for a watchdog counter and a read-timeout countdown to the ThreadPollWatch class.
 *                                      2.  Modified the SetPauseAndWait() method of the FormViewWatch class to include a timeout and included support to 
 *                                          monitor the watchdog of the thread that is responsible for communication with the target. Also: (a) included checks on the 
 *                                          success of the calls to the IPollTarget.SetPauseAndWait() method and (b) changed the order of the PortLocked and ReadTimeout 
 *                                          checks in the DisplayUpdate() method.
 *                                          
 *  09/30/11    1.21.12 K.McD           1.  FormViewWatch - Added a call to the FormPTU.CloseFlags() method to the Exit() method as this call was removed from the 
 *                                          Cleanup() method in the FormPTU class.
 *                                          
 *                                      2.  FormViewWatch - Refactored the implementation of the IPollTarget and ICommunicationInterface interfaces.
 *                                      3.  FormOpenWatch/FormOpenSimulatedFaultLog - Replaced any references to the inherited m_WatchFile variable with the 
 *                                          inherited WatchFile property reference.
 *                                      4.  FormOpenSimulatedFaultLog - Removed the overridden PlotHistoricData() method as this is no longer required.
 *                                      5.  FormChangeChartScale - Renamed the variable associated with the reference to the class that implements the IChartScale 
 *                                          interface.
 *                                          
 *  10/13/11    1.21.14 K.McD           1.  Rationalized the Resources.resx file and renamed a number of resources. Note: No revision history updates were carried out 
 *                                          on individual files that were auto-updated as a result of the resource name changes.
 *
 *	10/19/11	1.21.15	Sean.D			1.	Modified FormViewWatch_Shown() in FormViewWatch to ensure that a valid workset exists prior to trying to select the first 
 *	                                        item in the combo box.
 *										2.	Added a check in the ComboBoxUpdateWorksets() method of FormViewWatch as to whether a given workset has a valid number of 
 *										    watch variables. If no valid worksets exist, it tries to create a new baseline with the correct number of variables.
 *										3.	Added MBTCreatingBackupWorkset to the resources.
 *                                          
 *  10/19/11    1.21.15 K.McD           1.  FormViewWatch - Added an event handler for the Click event associated with the TabPage control to move focus away from the 
 *                                          currently selected user control.
 *                                          
 *                                      2.  FormViewWatch - SNCR002.41. Added checks to the event handlers associated with all ToolStripButton controls to ensure that 
 *                                          the event handler code is ignored if the Enabled property of the control is not asserted.
 *                                          
 *  10/26/11    1.21.16 K.McD           1.  Modified the FormConfigureChartRecorder class. Moved the location of the Panel control associated with the status 
 *                                          message.
 *                                          
 *  11/23/11    1.21.17 K.McD           1.  Modified the FormViewWatch class.
 *                                              (a) Ensured that all event handler methods were detached.
 *                                              (b) Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after the Close() 
 *                                                  method had been called.
 *                                                  
 *  11/23/11    1.21.18 K.McD           1.  Minor correction to the Exit() method of the FormViewWatch class. Set the Cursor to the Cursor.Default state rather than 
 *                                          the Cursor.WaitCursor state before calling base.Exit().
 *                                          
 *  12/01/11    1.21.19 K.McD           1.  Set the CountMax property of the workset to Parameter.WatchSizeChartRecorder in the ConvertToWorkset() method of the 
 *                                          FormConfigureChartRecorder class.
 *                                          
 *  07/04/13    1.22    K.McD           1.  Unticked the 'Allow unsafe code' box on the Build tab page of the project properties form for both the Debug and Release configurations.
 *                                      2.  Unticked the 'Enable the Visual Studio hosting process' and the 'Enable unmanaged code debugging' boxes on the Debug tab page 
 *                                          of the project Properties page for the Release configuration. Note: Both are enabled for the Debug configuration.
 *                                          
 *  07/26/13    1.22.1  K.MCD           Bug fix to enable the user to use Windows Terminal to download the embedded software to the COM-C card if the 'Flash Programming Enabled' 
 *                                      flag is asserted. Asserting this flag terminates the normal master/slave PTU communications link at the COM-C COM port and puts the port into 
 *                                      terminal mode.
 *                                      
 *                                      1.  Modified the Exit() method of the FormViewWatch class to close the communication port and to set the mode to configuration mode if there 
 *                                          is a communication fault or the watchdog has tripped i.e. the port is locked.
 *                                          
 *                                      2.  Modified the Run() method of the ThreadPollWatch class such that if a communication fault is detected, instead of trying to re-establish 
 *                                          communication, the thread just sleeps until the Dispose() method is called by the client. While in this state, the thread is periodically awoken to update the 
 *                                          watchdog counter so that the client can determine whether the communication port has locked.
 *                                          
 *  08/01/13    1.22.2  K.McD           1. Modified the Run() method of the ThreadPollWatch class to close the communication port as soon as the communication fault flag is asserted.
 *  
 *  08/07/13    1.22.3  K.McD           1.  Modified the MenuInterfaceWatch class. In those methods where it is applicable, the cursor was set to the wait cursor after the call to the 
 *                                          MainWindow.CloseChildForms() method as if any child forms are open, the cursor may be set to the default cursor as part of the call to the Exit() method within 
 *                                          the child form.
 *                                          
 *                                      2.  Modified the call to PTUDLL32.GetChartIndex() in the GetChartConfiguration() method of the CommunicationWatch class to match the 
 *                                          new parameter type i.e. the watchidentifier parameter is now passed as an 'out' instead of by 'ref'.
 *                                          
 */
#endregion - [1.0 - 1.22] -

#region - [1.23] -
/*                                           
 *  03/11/15     1.23   K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *  
 *                                      1.  Changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                          to support both 32 and 64 bit architecture.
 *  
 *                                      2.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                          Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                          
 *                                              1.  MOC-0171-06. All references to fault logs, including menu options and directory 
 *                                                  names to be replaced by 'data streams' for all projects.
 *                                                  
 *                                              2.  MOC-0171-18. The ‘Time’ legend will be reserved for system information time, the legend 
 *                                                  ‘PC Time’ will be used when displaying the PC time.
 *                                                  
 *                                              3.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                                  options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                                  
 *                                              4.  MOC-0171-30. The ‘F4 – Rec.’ Function key will toggle between the industry standard icons
 *                                                  representing start/stop recording and pause/continue display update.
 *                                                  
 *                                              5.  MOC-0171-26. Kawasaki pointed out that Chart Recorder tabs are identified as ‘COLUMNS’ and requested that this
 *                                                  be changed.
 *                                                  
 *                                              6.  MOC-0171-28. A check will be included as part of the ‘Save’ procedure to ensure that an empty workset cannot be saved.
 *                                                  
 *                                  2.  Updated Resources with a number of premium 28 x 28 images purchased from Iconfinder.
 *                                  
 *                                  3.  SNCR - R188 PTU [20 Mar 2015] Item 6. Bug associated with saving chart recorder worksets that contain watch variables
 *                                      that have different types of FormatString values e.g. 'General Number' and 'Hexadecimal'.
 *                                              
 *                                  Modifications
 *                                  1.  Modified CommunicationWatch.cs. Rev. 1.13. - Ref.: 1.1.
 *                                  2.  Modified Resources.resx. - Ref.: 1.2.1, 1.2.2, 1.2.3, 2. Also changed ToolTipText to be contained within
 *                                      square brackets e.g. F4 - [Continue].
 *                                  3.  Modified FormOpenSimulatedFaultLog.Designer.cs. Rev.: 1.1. - Ref.:2.5. 1.2.1.
 *                                  4.  Modified FormViewWatch.cs Rev.: 1.12. - Ref.: 1.2.4.
 *                                  5.  Modified FormWorksetDefineChartRecorder.Designer.cs. Rev.: 1.3. - Ref.: 1.2.5, 1.2.3.
 *                                  6.  Modified FormWorksetDefineWatch.Designer.cs. Rev.: 1.1. - Ref.: 1.2.3.
 *                                  7.  Modified FormConfigureChartRecorder.Designer.cs Rev. 1.2. - Ref.: 1.5.
 *                                  8.  Updated and rationalized the assembly image resources. Ref.: 1.2.4, 2.
 *                                  9.  Modified: FormConfigureChartRecorder.cs. Rev. 1.12, FormWorksetDefineChartRecorder.cs. Rev. 1.6. - Ref.: 1.2.6, 3.
 */
#endregion - [1.23] -

#region - [1.24] -
/*
 *  04/16/15    1.24    K.McD       References
 *              
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *                                      
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, Kawasaki Rail Car and NYTC on
 *                                          12th April 2013 - MOC-0171:
 *                                              
 *                                          1.  MOC-0171-06. All references to 'Fault Logs', including menu options and directory names to be replaced by 'Data Streams'
 *                                              for all projects.
 *
 *                                      2.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified to
 *                                          meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the current naming
 *                                          convention will still apply.
 *                                          
 *                                      3.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                          labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                          ‘Wibu-Key: [Present | Not Present]’.
 *                                          
 *                                          
 *                                  2.  SNCR - R188 PTU [20 Mar 2015] - Item 10. Bug associated with closing of the FormShowFlags dialog boxes.
 *                                      
 *                                  Modifications
 *                                  1.  Modified MenuInterfaceWatch.cs. Rev. 1.15. - Ref.: 1.1.1, 1.2.
 *                                  2.  FormViewWatch.cs. Rev. 1.13. - Ref.: 1.1.1, 1.2, 1.3, 2.
 */
#endregion - [1.24] -

#region - [1.25] -
/*
 *  07/13/15    1.25    K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *                                      
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                          Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                          
 *                                          1.  MOC-0171-29/34. Kawasaki requested that any function that is not available to the current mode, either Maintenance
 *                                              or Administrative, should not be displayed, or be 'greyed out'. BTPC agreed to review the software to ensure that
 *                                              non-active menu items are ‘greyed out’ or not shown.
 *                                              
 *                                              As a result of a further review, it is proposed that the following modifications are carried out:
 *                                              
 *                                                  1.  The 'Select Data Dictionary' menu option should only visible when logged in as a BT engineer (Factory).
 *                                                      Also when visible, it should only be enabled when not in onlide, offline or self test mode.
 *                                                      
 *                                                  2.  When in Self Test mode only the 'File/Exit', 'Help' and 'Login' main menu options should be enabled.
 *                                                  
 *                                                  3.  When displaying event logs only the File/Exit', 'Configure/Worksets/Data Stream', 'Configure/Enumeration',
 *                                                      'Help', and 'Login' main menu options should be enabled.
 *                                                          
 *                                                  4.  When displaying the Watch Window only the 'File/Exit', 'Configure/Worksets/Data Stream', 'Configure/Enumeration',
 *                                                      'Help', and 'Login' main menu options should be enabled.
 *                                      
 *                                  Modifications
 *                                  1.  FormViewWatch.cs. Rev. 1.14. - Ref.: 1.1.1.4.
 */
#endregion - [1.25] -

#region - [1.26] -
/*
 *  07/28/15    1.26    K.McD       References
 *                                  1.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 23. In the Rev. 1.25 implementation, if the ‘F5-Modify’ function key is disabled,
 *                                      because, for example, the user is in Maintenance mode and the active workset is at the Engineering security level, and the
 *                                      user selects either the ‘F4-Pause’ or the ‘F3-Rec’ function key; the ‘F5-Modify’ function key remains disabled while the
 *                                      PTU is in the record or the paused state, however, on returning to the normal state, it becomes active. This is incorrect
 *                                      and should be corrected in the next revision.
 *                                      
 *                                  2.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 24. On selecting the ‘Exit’ function key on the ‘Diagnostics/Event Log’ and
 *                                      the ‘Diagnostics/Self Tests’ screens, the cursor doesn’t go to the Cursors.WaitCursor cursor on the R188 project.
 *                                      
 *                                  Modifications
 *                                  1. FormViewWatch.cs Rev. 1.15. - Ref.: 1, 2.
 * 
 */
#endregion - [1.26] -

#region - [1.27] -
/*
 *  08/11/15    1.27    K.McD           References
 *                                      1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                          from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                      
 *                                          1.  On the R188 project only, all references to 'PTU' are to be replaced with 'PTE' and all occurrences of 'Portable Test Unit'
 *                                              are to be replaced with 'Portable Test Equipment'. Where support for multiple legends is not possible, 'Portable Test
 *                                              Application' is to be used as an alternative to 'Portable Test Unit'/'Portable Test Equipment.
 *                                              
 *                                      Modifications
 *                                      1.  AssemblyInfo.cs. Rev. 1.27. Changed the ApplicationProduct attribute to 'Portable Test Application'.
 *                                      2.  Resources.resx. Removed any reference to PTU in the message box captions e.g. replaced 'PTU - Error' with 'Error'.
 */
#endregion - [1.27] -
#endregion --- Revision History ---

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Resources;

// General Information about an assembly is controlled through the following set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyCompany("Bombardier Transportation (Holdings) USA Inc.")]
[assembly: AssemblyProduct("Portable Test Application")]
[assembly: AssemblyCopyright("(C) 2010 - 2015 Bombardier Transportation (Holdings) USA Inc.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguageAttribute("")]

[assembly: AssemblyTitle("Portable Test Application - Watch/Data Monitoring Sub-system")]
[assembly: AssemblyDescription("A library of classes to support the monitoring and recording of watch variables.")]
[assembly: AssemblyConfiguration("")]

// Setting ComVisible to false makes the types in this assembly not visible to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("C515014E-CA1A-47cb-8DF6-75FDBE7E9AD0")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers by using the '*'.

[assembly: AssemblyVersion("1.27.0.0")]