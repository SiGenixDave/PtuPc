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
 *  Project:    Event
 * 
 *  File name:  AssemblyInfo.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 */

#region - [1.0 - 1.14] -
/*
 *  10/26/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/17/10    1.1     K.McD           1.  Modified the ConvertToWorkset() method in the CommunicationEvent class to take into account the changes to
 *                                          the Workset_t structure constructor in Common.dll.
 * 
 *  11/26/10    1.2     K.McD           1.  Included support to allow the user to manage and define the fault log worksets.
 *                                      2.  Included support to allow the user to modify the fault log data stream parameters.
 *                                      3.  Included support to allow the user to save the current event log to disk in XML format.
 *                                      4.  Included support to allow the user to save the downloaded fault log to disk.
 *                                      5.  Included support to allow the user to view saved fault log files.
 * 
 *  12/08/10    1.3     K.McD           1.  Modified the FormViewEventLog class so that all event variables are downloaded while the event log is being
 *                                          downloaded.
 * 
 *  01/06/10    1.4     K.McD           1.  Included support for the dynamic update of the event log using the IPollTarget interface and the
 *                                          ThreadPollEvent class.
 *                                      2.  Include the filename in the title of forms used to display saved event logs.
 *                                      3.  Included support to: (1) save the event log to disk in XML format and (2) save all data streams associated
 *                                          with the current event log to disk.
 *                                      4.  Include support to allow the user to view saved event logs and load multiple saved event logs from
 *                                          different propulsion units.
 *                                      5.  Bug fix SNCR001.66. Modified the FormSetupStream class to save the current event log to disk and then clear
 *                                          it down before updating the data stream parameters.
 * 
 *  01/10/11    1.5     K.McD           1.  Modified the GetStream() method of the CommunicationEvent class to set the data stream number to be equal
 *                                          to the event record index + 1.
 * 
 *  01/12/11    1.6     K.McD           1.  Bug fix SNCR001.84. Added the second parameter to all calls to the System.Threading.WaitHandle.WaitOne()
 *                                          method, as advised by the following blog entries: 
 *                                              (1) http://www.mikeplate.com/missingmethodexception-and-waitone/ and (2) 
 *                                              (2) http://blog.darrenstokes.com/2009/03/30/watch-out-for-those-waitone-overloads-when-you-need-backwards-compatibility
 * 
 *  01/14/11    1.7     K.McD           1.  Bug fix SNCR001.85. There appears to be a bug in the CheckFaultLogger() method of the PTUDLL322 dynamic
 *                                          link library in that the event count value returned from the call can sometimes increment by the number of
 *                                          existing entries in the event log everytime that the call is made. This problem is sometimes cleared by
 *                                          calling the ClearEvent() method the same library, however, in order to circumvent the problem the design of
 *                                          the dynamic update of the event log was modified to use the event index value that is returned from the
 *                                          call instead as this value is correct. 
 * 
 *                                          The bug fix is implemented by adding an EventIndex property to the Event.FormViewEventLog class in
 *                                          conjunction with a modification to the Run() method of the Event.ThreadPollEvent class.
 * 
 *  01/18/11    1.8     K.McD           1.  Bug fix SNCR001.79 - In previous releases, the contents of the data streams associated with some of the
 *                                          events occasionally had all watch variable values set to 0. This bug has been addressed by correcting the
 *                                          stream number value passed to the call to the PTUDLL32.GetStream() method in
 *                                          Event.CommunicationEvent.GetStream(). 
 * 
 *                                          The data stream number associated with each event record is determined as follows. Starting from the oldest
 *                                          event record, whenever an event record is found that has an associated data stream, the stream number for
 *                                          the event record is incremented from a starting value of 0. Any event record that does not have an
 *                                          associated data stream will have the stream number set to CommonConstants.NotUsed.
 * 
 *  01/19/11    1.9     K.McD           1.  Bug fix SNCR001.79/SNCR001.76. Added the StreamNumber property to the FormViewEventLog class and made sure
 *                                          that it wrapped around back to 0 when the count reaches the maximum number of data streams that the VCU is
 *                                          capable of recording.
 * 
 *  01/26/11    1.10    K.McD           1.  Minor modifications to accommodate the changes introduced in version 1.10.10 of Common.dll.
 * 
 *  01/26/11    1.10.1  K.McD           1.  Bug fix SNCR001.97. Modified the ConvertToWorkset() method of the CommunicationEvent class to use the 
 *                                          Parameter.WatchSizeFaultLogMax constant to specify the entryCountMax parameter when instantiating the
 *                                          Workset_t structure.
 * 
 *  01/27/11    1.11    K.McD           1.  Bug fix - SNCR001.89. Modified the ShowDataStreamFile() method of the MenuInterfaceEvent class such that
 *                                          the selected data stream file is only shown if the selected file is valid.
 * 
 *                                      2.  Modified the: CommunicationEvent, FormViewEventLog, FormSetupStream and ThreadPollEvent classes to
 *                                          accommodate thecommunication mutex introduced into the Common.CommunicationParent class in version 1.11 of
 *                                          Common.dll.
 * 
 *                                      3.  Request - SNCR001.72. Included a feature whereby the user can save the event logs to an individual event
 *                                          log XML file or append the events to an existing file.
 * 
 *  02/03/11    1.12    K.McD           1.  Modified the F3-Save function key event handler in the FormViewSavedEventLog class to support appending
 *                                          events to an existing file.
 *                                      2.  Modified the F4-Load function key event handler in the FormViewSavedEventLog class to support loading
 *                                          multiple saved event log files.
 *                                      3.  Standardized the function key event handlers to: display the wait cursor, enable the Checked property of
 *                                          the function key and clear any status message.
 *                                      4.  Added the DateTime and Log fields to the DataGridView control.
 *                                      5.  Added an event handler for the SortCompare event of the DataGridView control to control the sort order of
 *                                          the rows.
 *                                      6.  SNCR001.73. Created the MenuInterfaceEvent.ImportEventLogFiles() method to allow the user to use the
 *                                          Windows multi-select feature to load a number of event log files.
 * 
 *  02/14/11    1.12.1  K.McD           1.  Included support for debug mode.
 * 
 *  02/15/11    1.12.2  K.McD           1.  Modified the design such that the StreamNumber field associated with each downloaded event record is now
 *                                          derived from the call to the PTUDLL32.GetFaultHdr() method.
 * 
 *  02/21/11    1.12.3  K.McD           1.  Modified the FormWorksetDefineFaultLog class such that it no longer uses the sample interval value.
 *                                      2.  Modified the FormSetupStream class such that it no longer clears the current log before downloading the new
 *                                          parameters.
 *                                      3.  Bug fix - SNCR001.102. Modified the GetEventRecord() method of the CommunicationError class to check that
 *                                          the LOGID, TASKID and EVENTID fields retrieved from the VCU for the specified event record are valid.
 * 
 *                                      4.  Modified the GetStream() method of the CommunicationEvent class the use the PTUDLL32.GetStreamInformation()
 *                                          method rather than PTUDLL32.GetDefaultStreamInformation(). This means that the current event log need not
 *                                          be cleared when updating the data stream parameters.
 * 
 *                                      5.  Modified the FormViewEventLog class as follows:
 *                                              (a) Now uses the FormGetStream class to show progess on the data stream downloads.
 *                                              (b) The 'Reset' function key was renamed 'Initialize'.
 *                                              (c) Bug fix - SNCR001.102. Ignore any events where a match for the LOGID, TASKID and EVENTID downloaded
 *                                                  from the VCU cannot be found in the data dictionary.
 *                                              (d) Bug fix. Modified the SaveAll() method to restore the current log after saving the event logs and
 *                                                  data streams.
 * 
 *  02/28/11    1.12.4  K.McD           1.  Bug fix. Modified the CheckFaultlogger() method of the CommunicationEvent class such that the newIndex
 *                                          local variable used in the call to the PTUDLL32.CheckFaultLogger() method in not initilalized to zero.
 * 
 *                                      2.  Modified the InitializeEventLogs() method of the MenuInterfaceEvent class to ask for user confirmation
 *                                          before proceeding.
 * 
 *                                      3.  Bug fix. Improved the layout of the event log display and fixed the AccessViolation exception thrown
 *                                          following the download of a data stream.
 * 
 *                                      4.  Added event handlers for the Shown events raised by the FormViewFaultLog and FormOpenFaultLog classes to
 *                                          modify the image and text of the escape key ToolStrip button if the forms were called from the form used to
 *                                          display the event log rather than the main application window as under these circumstances the escape key
 *                                          will return the user to that form rather than home.
 * 
 *                                      5.  Modified the event handler for the Save function key Click event of the FormSetupStream class to ask for
 *                                          confirmation before updating a saved workset.
 * 
 *                                      6.  Modified the FormSetupStream class such that the combo box control is now used to both display the name of
 *                                          the workset and to select a new workset.
 * 
 *  03/02/11    1.12.5  K.McD           1.  Modified the FormViewEventLog and FormOpenEventLog classes such that the EventRecordList property is sorted
 *                                          by descending date/time order, i.e. most recent event first, so that the first row of the DataGridView
 *                                          control is selected when the forms are first shown.
 * 
 *                                      2.  Modified the DataGridView SortCompare() event handler in the FormViewEventLog class such that the value
 *                                          stored in the Log column of the DataGridView control is ignored when determining the sort order.
 * 
 *  03/18/11    1.12.6  K.McD           1.  Auto-modified to support the name changes to: (a) a number of properties associated with the
 *                                          Common.Security class and (b) a method in the Common.FormPTU class.
 * 
 *                                      2.  Modified the width of the event variable user controls.
 * 
 *                                      3.  Added support for the WinHlp32 help engine to display the help diagnostic information for the events and
 *                                          event variables.
 * 
 *                                      4.  Modified the event handler for the DataGridView SelectionChanged event in the FormViewEventLog and
 *                                          FormOpenEventLog classes to close any FormShowFlags forms that may be open.
 * 
 *                                      5.  Modified the event handler for the DataGridView CellContentDoubleClick event in the the FormViewEventLog
 *                                          and FormOpenEventLog classes to include a check to determine which column of the DataGridView has been
 *                                          selected by the user and then either: (a) display the diagnostic help information associated with the
 *                                          selected event or (b) display the fault log associated with the selected event, as appropriate.
 * 
 *                                      6.  Refactored the event handler for the DataGridView CellContentDoubleClick event in the the FormViewEventLog
 *                                          and FormOpenEventLog classes such that the code responsible for loading and displaying the fault log is
 *                                          now included in the newly created ShowFaultLog() method.
 * 
 *                                      7.  Modified the event handlers for the DataGridView CellContentDoubleClick and SelectionChanged events in the
 *                                          FormOpenEventLog class to include a match for the event index in the Find() method calls when trying to
 *                                          locate the correct record in the list of event records. This was added to cater for the case where multiple
 *                                          events with the same description and date/time stamp are generated by the VCU, but with different event
 *                                          variable information e.g. in the case of the 'Inverter Fault' event.
 * 
 *  03/21/11    1.12.7  K.McD           1.  Modified the ConfigureControls() method of the FormViewEventLog class such that the individual event
 *                                          variable user controls are laid out on the panel similar to rows on a DataGridView control. The first entry
 *                                          in the specified list of event variables, eventVariableList[0], Is positioned at row 0, the second at row 1
 *                                          etc. Also modified the signature to allow the programmer to specify the starting index in the list of event
 *                                          variables where configuration is to begin, 0 to configure all event variables in the list, etc.
 * 
 *                                      2.  Modified the DisplayEventVariableList() method of the FormViewEventLog class such that only those event
 *                                          variable user controls which are specific to the selected event are re-configured. The common event
 *                                          variable user controls are no longer re-configured for each selected event.
 * 
 *                                      3.  Added the CompareByDateTimeAscending() method to allow the list of event records to be sorted in ascending
 *                                          order i.e. oldest event first. This is related the following item.
 * 
 *                                      4.  Modified the philosopy used to find the selected event in the list of event records for the
 *                                          FormOpenEventLog class. The event record list is now sorted by DateTime order, oldest event first and the
 *                                          event index of each record is updated so that it is unique and matches the sort order of the list i.e.
 *                                          0 will represent the oldest event, 1 the following event etc. When the user selects an event from the
 *                                          DataGridView control, the index of the event corresponding to the selected row is determined and the list
 *                                          of event records is searched for a match.
 * 
 *  03/28/11    1.12.8  K.McD           1.  Bug fix SNCR001.112. Modified the assembly to use the old identifier field of the data dictionary, rather 
 *                                          than the watch identifier field, to define the watch variables that are to be included in the workset as
 *                                          these remain consistent following a data dictionary update.
 * 
 *                                      2.  Minor changes as a result of name changes to a number of methods and properties of external and internal
 *                                          classes.
 * 
 *  04/06/11    1.12.9  K.McD           1.  Modified the FormViewEventLog class to include the function keys that call the forms which: (a) allow the
 *                                          user to configure the event flags associated with the current log and (b) show the cumulative event
 *                                          history.
 * 
 *                                      2.  Added the FormConfigureEventFlags class. This form allows the user to configure the event flags associated
 *                                          with the current log.
 * 
 *                                      3.  Added the FormShowEventHistory class. This form shows a summary of the cumulative and recent event history
 *                                          i.e. the number of occurrences of each event type.
 * 
 *                                      4.  Added the FormGetFltHistoryInfo class to download the fault history information from the VCU.
 * 
 *                                      5.  Modified the PTUDLL32 class to include the communication methods required to configure the event flags and
 *                                          show the event history.
 * 
 *                                      6.  Modified the CommunicationEvent class to include the communication methods required to configure the event
 *                                          flags and show the event history.
 * 
 *                                      7.  Added a number of resources.
 * 
 *                                      8.  Minor XML tag, #region and Debug.Assert() text corrections to the FormGetStream class.
 * 
 *  04/08/11    1.12.10 K.McD           1.  Minor modifications to support the screen capture feature.
 *                                      2.  Minor tidy up of XML tags, comments etc.
 * 
 *  04/28/11    1.14    K.McD           1.  SNCR001.119. Modified the graphic associated with a data stream not being available in the
 *                                          'DataStream Available' column of the DataGridView control of the FormViewEventLog class to be transparent
 *                                          instead of white.
 * 
 *                                      2.  Renamed the FormSetupStream class to FormConfigureFaultLogParameters and made a number of minor
 *                                          modifications.
 */
#endregion - [1 - 1.14] -

#region - [1.15] -
/*                                      
 *  05/23/11    1.15    K.McD           1.  Migrated to Visual Studio 2010.
 *                                      2.  Modified the design of the FormWorksetManagerFaultLog class to exclude support for a default workset.
 *                                      3.  Modified the FormWorksetDefineFaultLog class to accommodate the changes to the parent class,
 *                                          FormWorksetDefine, introduced in version 1.16 of Common.dll.
 *                                          
 *  05/23/11    1.15.1  K.McD           1.  Applied the 'Organize Usings/Remove and Sort' context menu to all classes.
 *  
 *  05/26/11    1.15.2  K.McD           1.  Auto-modified the following classes as a result of name changes to a number of constants in the
 *                                          CommonConstants class:
 *  
 *                                              (a) CommunicationEvent.
 *                                              (b) FormWorksetDefineFaultLog.
 *                                              (c) MenuInterfaceEvent.
 *                                              
 *                                          Note: The revision history associated with each of the above classes was not updated to reflect this
 *                                          change.
 *                                          
 *  06/03/11    1.15.3  K.McD           1.  Removed the comments showing the PTUDLL32 prototypes and the Visual Basic function declarations from the
 *                                          PTUDLL32Event class. No functional changes.
 *                                          
 *  06/21/11    1.15.4  K.McD           1.  Auto-modified as a result of a name change to an inherited member variable.
 *  
 *  06/24/11    1.15.5  K.McD           1.  Modified the AutoScaleMode property of the FormWorksetConfigureFaultLogParameters and
 *                                          FormWorksetDefineFaultLog classes to AutoScaleMode.Font to ensure that the vertical scrollbar on the row
 *                                          header ListBox control isn't activated if the font size is increased.
 *                                          
 *                                      2.  Moved the position of the NumericUpDown control associated with the sample multiple and also the z-order to
 *                                          ensure that the legend does not overlap the control if the font size is increased.
 *                                          
 *  06/30/11    1.15.6  K.McD           1.  Target framework updated to .NET Framework 4.0.
 *                                      2.  Minor correction to the caption field of a message box in the MenuInterfaceEvent class.
 *                                      3.  Set a Label BackColor property to Transparent in the FormWorksetDefineFaultLog class.
 *                                      
 *  07/11/11    1.15.7  K.McD           1.  Modified the ViewEventLog() method to inform the user of the name of the log that is being retrieved prior
 *                                          to showing the form used to display the event logs.
 *                                          
 *                                      2.  Set the TabStop property of the 'Column' TabControl to false in class FormWorksetDefineFaultLog.
 *                                      
 *  07/25/11    1.15.8  K.McD           1.  Updated to support off-line mode. Modified the signature of the constructors associated with the 
 *                                          FormViewEventLog, FormShowEventHistory, FormConfigureFaultLogParameters, FormConfigureEventFlags and 
 *                                          CommunicationEvent classes to use the ICommunicationParent interface.
 *                                          
 *  07/27/11    1.15.9  K.McD           1.  Modified the FormOpenEventLog class constructor to initialize the member variable that stores the number of
 *                                          common event variables that are recorded for each event.
 *                                          
 *                                      2.  SNCR001.148. Bug fix associated with an exception being thrown when the user selects an event from the
 *                                          event log that is downloaded from the VCU that has a number of event specific event variables after having
 *                                          selected an event that has no specific event variables. Updated the FormViewEventLog class to version 1.10.
 *
 *                                      3.  SNCR001.135. Bug fix associated with the event log displaying the incorrect number of events in the current
 *                                          list if the list is empty. Modified the m_ToolStripComboBox1_SelectedIndexChanged() method of the
 *                                          FormViewEventLog class to update the label that shows the number of events in the list to display zero if
 *                                          the list is empty.
 *                                          
 *  07/29/11    1.15.10 K.McD           1.  Corrected a number of XML documentation tags that were thrown up as warnings because:
 *                                              (a) the paramref name did not match the name of the parameter;
 *                                              (b) the tag formatting was incorrect;
 *                                              (c) the protected member variable did not have an XML tag or
 *                                              (d) the exception tag was specified but was no longer used.
 *                                              
 *                                      2.  Modified the FormViewEventLog class.
 *                                              (a) SNCR001.148. Bug fix associated with the CTA site visit on 15th July 2011. An exception was thrown
 *                                                  when the user selected an event from the downloaded event log. Further investigation showed that
 *                                                  the exception was thrown because the previously selected event had no event secific event variables
 *                                                  associated with it.
 *                                          
 *                                                  (a) Changed the modifier of the member variable that stores the number of common event variables
 *                                                      recorded for each system event to protected.
 *                                                  
 *                                                  (b) Modified the DisplayEventVariables() method to check whether the number of controls added to
 *                                                      the Panel control associated with the list of event variables for the previous event was
 *                                                      greater than or equal to the number of common event variables rather than simply greater than.
 *                                                  
 *                                              (b) SNCR001.135. Bug fix associated with the event log displaying the incorrect number of events in the
 *                                                  current list if the list is empty. Modified the m_ToolStripComboBox1_SelectedIndexChanged() method
 *                                                  to update the label that shows the number of events in the list to display zero if the list is
 *                                                  empty.
 *                                                  
 *                                      3.  Modified the class constructor of the FormOpenEventLog class to initialize the member variable that stores
 *                                          the number of common event variables that are recorded for each event.
 *                                          
 *                                      4.  Modified the project file.
 *                                              (a) Removed the Trace constant from the Release build.
 *                                              (b) Forced the build to generate an event documentation XML file.
 *                                              
 *  08/05/11    1.15.11 K.McD           1.  Minor adjustments to the Size and Location properties of one or more controls in a number of classes
 *                                          derived from FormPTUDialog.
 *                                      2.  Changed the BackColor property of one or more controls to Transparent.
 *                                      3.  Major changes to the FormConfigureFaultLogParameters class, now inherits from the Common.FormConfigure
 *                                          class.
 *                                      4.  Minor adjustments to the Size and Location properties of one or more controls in the FormViewEventLog
 *                                          class.
 *                                      5.  Included a call to the Update() method following any call to MessageBox.Show() that may be immediately
 *                                          followed by another call to MessageBox.Show() in the FormViewEventLog class.
 *                                          
 *  08/17/11    1.15.12 K.McD           1.  Included support for offline mode. Added the CommunicationEventOffline class.
 *  
 *                                      2.  Included support for offline mode. Modified the constructors of the following classes to conditionally
 *                                          choose CommunicationEvent or CommunicationEventOffline depending upon the current mode: 
 *                                          
 *                                             FormViewEventLog
 *                                             FormConfigureFaultLogParameters
 *                                             FormConfigureEventFlags
 *                                             FormShowEventHistory
 *                                             
 *                                      3.  Included support for offline mode. Modified the InitializeEventLogs() method of the MenuInterfaceEvent
 *                                          class to conditionally choose CommunicationEvent or CommunicationEventOffline depending upon the current
 *                                          mode.
 *                                          
 *                                      4.  Bug fix associated with the position of the buttons associated with the FormShowEventistory class.
 *                                      
 *                                      5.  Aesthetic improvements to the FormViewEventLog build/clear appearance.
 *                                      
 *  08/24/11    1.15.14 K.McD           1.  Added support for a watchdog counter and a read-timeout countdown to the ThreadPollEvent class.
 *                                      2.  Modified the FormGetStream class to included a timeout on the completion of the BackgroundWorker thread and
 *                                          to include a try/catch block in the m_BackgroundWorker_DoWork() method.
 *                                      3.  Modified the FormViewEventLog class to: (a) include a timeout in the SetPauseAndWait() method, (b) monitor
 *                                          the watchdog associated with the thread that is responsible for communication with the target and
 *                                          (c) include checks on the success of the call to the IPollTarget.SetPauseAndWait() method in a number of
 *                                          methods.
 *                                          
 *  08/25/11    1.15.15 Sean.D          1.  Modified the CommunicationEventOffline class to correct the configuration of the fault log parameters and
 *                                          the display of the fault log plots.
 *                                          
 *  09/30/11    1.15.16 K.McD           1.  MenuInterfaceEvent class - Modified the ImportEventLogFiles() method to initialize the FullFilename
 *                                          property of the EventLogFile_t structure.
 *                                          
 *                                      2.  FormOpenFaultLog/FormViewFaultLog classes:
 *                                              (a) Replaced all references the inherited m_WatchFile variable with a reference to the inherited
 *                                                  WatchFile property.
 *                                              (b) Removed the overridded PlotHistoricData() method as this is no longer required.
 *                                              
 *                                      3.  FormOpenEventLog class: 
 *                                              (a) Refactored the implementation of the IEventLogFile interface.
 *                                              (b) Modified the ShowFaultLog() method to update the FullFilename property of the WatchFile_t
 *                                                  structure.
 *                                              
 *                                      4.  Reverted the FormWorksetDefineFaultLog class back to revision 1.7.
 *                                      
 *  10/13/11    1.15.17 K.McD           1.  Rationalized the Resources.resx file and renamed a number of resources. Note: No revision history updates
 *                                          were carried out on individual files that were auto-updated as a result of the resource name changes.
 */
#endregion - [1.15] -

#region - [1.16] -
/*                                          
 *  10/26/11    1.16    K.McD           1.  FormViewFaultLog - version 1.8, FormOpenEventLog - version 1.12, FornViewEventLog - vetsion 1.13, 
 *                                              1.  SNCR002.41. Added checks to the event handlers associated with all ToolStripButton controls to
 *                                                  ensure that the event handler code is ignored if the Enabled property of the control is not
 *                                                  asserted.
 *                                                  
 *                                      2.  Asserted the 'StandardTab' property of the DataGridViewControl in the FormConfigureEventFlags class to
 *                                          ensure normal Tab operation.
 *                                          
 *  10/26/11    1.16.1  K.McD           1.  Modified the FormConfigureFaultLogParameters class. Moved the location of the Panel control associated with
 *                                          the status message.
 *                                          
 *  11/23/11    1.16.2  K.McD           1.  Modified the FormViewEventLog class.
 *                                              (a) Ensured that all event handler methods were detached.
 *                                              (b) Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced
 *                                                  after the Close() method had been called.
 *                                                  
 *  12/01/11    1.16.3  K.McD           1.  Modified the FormConfigureFaultLogParameters class.
 *                                              1.  Set the CountMax property of the workset to Parameter.WatchSizeFaultLogMax in the ConvertToWorkset
 *                                                  () method.
 *                                              2.  Modified the m_NumericUpDownSampleMultiple_ValueChanged() method such that the DataUpdate event is
 *                                                  only triggered if the ModifyEnabled property is asserted.
 *                                                  
 *  12/06/11    1.16.4  Sean.D          1.  Modified parameterized constructors of FormOpenFaultLog and FormViewFaultLog to check to be sure that the
 *                                          tabpages are populated before setting the name of the first.
 */
#endregion - [1.16] -

#region - [1.17] -
/*											
 *  07/12/13    1.17    K.McD           1.  Unticked the 'Enable the Visual Studio hosting process' and the 'Enable unmanaged code debugging' boxes on
 *                                          the Debug tab page of the project Properties page for the Release configuration. Note: Both are enabled for
 *                                          the Debug configuration.
 *  07/16/13    1.17.1  K.McD           1.  Modified the FormViewFaultLog class. Added a try/catch block to the F3 function key event handler to
 *                                          prevent an exception being thrown in the event that the number of records in
 *                                          m_HistoricDataManager.AllFrames is less that the TripIndex value of the current DataStream type. This can
 *                                          occur if the DataStreamType field  of the Logs table in the .E1 database is set up incorrectly.
 *                                          
 *  07/16/13    1.17.2  K.McD           1.  Modified the FormGetStream class.
 *                                              1.  Renamed the constants:
 *                                                  a.  DataStreamTypeFaultLog to DataStreamTypeStandardFaultLog
 *                                                  b.  DurationSecDownloadFaultLog to DurationSecDownloadStandardFaultLog.
 *                                              
 *                                              2.  Modified the XML tags associated with the updated constants.
 *                                      
 *                                              3.  Modified the default case of the switch(dataStreamTypeIdentifier) statement in the constructor to
 *                                                  set the progress bar parameters and thread complete timeout to be the same as those for the
 *                                                  standard fault log in case additional datastream types are added to the DataStreamType table of
 *                                                  the .E1 database.
 *                                          
 *                                                  This modification arose because a new datastream type was added to the DataStreamType table of
 *                                                  the .E1 database to define the fault log datastream associated with the R188 project. The
 *                                                  datastream associated with this project is identical to that of the standard fault log however the
 *                                                  trip time index i.e. the sample corresponding to the time of the actual trip is 25 rather than 75.
 *                                                  When trying to download the fault log with this new datastream type the download would terminate
 *                                                  immediately and report that the PTU was unable to download the fault log as the download complete
 *                                                      timeout had not been initialized.
 *                                                  
 *  07/24/13    1.17.3  K.McD           1.  Modified the event handler for the F8-Stream function key in the FormViewEventLog class. Now passes a
 *                                          reference to the current event log when instantiating the  FormConfigureFaultLogParameters class so that
 *                                          the number of watch variables associated with the current event log are known by the form that is used to
 *                                          configure the watch variables and a check may be made to ensure that the selected workset does not include
 *                                          more watch variables than can be supported by the event log. This requirement is only associated with
 *                                          projects that do not record a fixed number of watch variables for each event log.
 *                                          
 *                                      2.  Automatic update of the CommunicationEvent, CommunicationEventOffline and FormConfigureFaultLogParameters
 *                                          classes when all references to the Parameter.WatchSizeFaultLogMax constant were replaced by references to
 *                                          the Parameter.WatchSizeFaultLog property.
 *                                          
 *                                      3.  Set the Visible property of the 'Total Count' and 'Count' labels to true and false respectively in the
 *                                          FormConfigureFaultLogParameters and FormWorksetDefineFaultLogParameters classes.
 *                                          
 *                                      4.  Included update of the 'Total Count' label in the UpdateCount() methods in the FormConfigureChartRecorder
 *                                          and FormWorksetDefineChartRecorder classes.
 *                                          
 *                                      5.  Modified the event handler for the Download key in the FormConfigureFaultLogParameters class to check
 *                                          whether the number of watch variables associated with the current workset exceeds the number supported by the current event log.
 *                                          
 *                                      6.  Modified the signature of the constructor for the FormConfigureFaultLogParameters class to include a
 *                                          reference to the current event log and used this to update the EntryCountMax property of the class.
 *                                          
 *                                      7.  Modified the Exit() method of the FormViewEventLog class to close the communication port and to set the
 *                                          mode to configuration mode if there is a communication fault or the watchdog has tripped i.e. the port is
 *                                          locked. Modified to be consistent with the FormViewWatch class.
 *                                          
 *                                      8.  Modified the Run() method of the ThreadPollEvent class such that if a communication fault is detected,
 *                                          instead of trying to re-establish communication, the thread just sleeps until the Dispose() method is
 *                                          called by the client. While in this state, the thread is periodically awoken to update the watchdog counter
 *                                          so that the client can determine whether the communication port has locked. Modified to be consistent with
 *                                          the ThreadPollWatch class.
 *                                          
 *  08/01/13    1.17.4  K.McD           1.  Modified the Run() method of the ThreadPollEvent class to close the communication port as soon as the
 *                                          communication fault flag is asserted so that it is consistent with the Watch.ThreadPollWatch class.
 *                                          
 *  08/02/13    1.17.5  K.McD           1.  Increased the width of the date field of the event log DataGridView control associated with the
 *                                          FormViewEventLog class from 70 pixels to 75 pixels to allow a 4 digit year code to be displayed in
 *                                          Segoe UI9 font.
 *                                          
 *  08/05/13    1.17.6  K.McD           1.  Increased the width of the event name field of the event log DataGridView control from 200 pixels to 250
 *                                          pixels to allow the full event text to be displayed.
 *                                          
 *                                      2.  Changed the SleepMsVerisimilitudeGetRecord constant in the CommunicationEventOffline class from 25 ms to 0
 *                                          ms as the simulated time to load the event list was excessive.
 *                                          
 *  08/06/13    1.17.7  K.McD           1.  Disabled the 'F5-Edit' function key in the contructor of the FormViewFaultLog class as changing the plot
 *                                          layout is not supported on live fault log data. This feature is only available once the fault log has been
 *                                          saved to disk.
 *                                          
 *                                      2.  Modified the 'Shown' event handler of the FormViewFaultLog class to hide the 'Remove Selected Plot(s)'
 *                                          context menu option as this feature is not supported on live fault log data.
 *                                          
 *  08/07/13    1.17.8  K.McD           1.  Modified the MenuInterfaceEvent class. In those methods where it is applicable, 
 *                                          the cursor was set to the wait cursor after the call to the MainWindow.CloseChildForms() 
 *                                          method as if any child forms are open, the cursor may be set to the default cursor as 
 *                                          part of the call to the Exit() method within the child form.
 *                                          
 *                                      2.  Modified the PTUDLL32Event class as follows:
 *                                              1.  Included the Event.cpp prototype information and re-ordered the methods so 
 *                                                  that they appear in the same order as the prototype list.
 *                                              2.  Changed the String parameter modifiers of the GetFaultHdr() method to use 
 *                                                  '[MarshalAs(UnmanagedType.BStr)] out String'.
 */
#endregion - [1.17] -

#region  - [1.18] -
/*
 *  03/11/15     1.18   K.McD       References
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
 *                                              4.  MOC-0171-41. Wherever possible, i.e. where there is room within the existing control to accommodate the text
 *                                                  ‘Vehicle Control Unit’ without having significant impact on the screen layout, VCU will be replaced with 
 *                                                  Vehicle Control Unit.
 *                                                  
 *                                              5.  MOC-0171-26. Kawasaki pointed out that Chart Recorder tabs are identified as ‘COLUMNS’ and requested that this
 *                                                  be changed.
 *                                                  
 *                                              6.  MOC-0171-28. A check will be included as part of the ‘Save’ procedure to ensure that an empty workset cannot be saved.
 *                                              
 *                                              7.  MOC-0171-29/34. Kawasaki requested that any function that is not available to the current mode, either Maintenance
 *                                                  or Administrative, should not be displayed, or be 'greyed out'. BTPC agreed to review the software to ensure that
 *                                                  non-active menu items are ‘greyed out’ or not shown.
 *                                                  
 *                                  2.  Updated Resources with a number of premium 28 x 28 images purchased from Iconfinder.
 *                                  
 *                                  3.  SNCR - R188 PTU [20 Mar 2015] Item 7. While attempting to configure a data stream, the set of parameters that were downloaded
 *                                      from the VCU were not defined in an existing workset, consequently FormConfigureFaultLogParameters entered 'Create' mode.
 *                                      While in this mode, the PTU displayed the workset parameters that were downloaded from the VCU and gave the user the opportunity
 *                                      to name the workset but not the opportunity to Save the workset. Modify the code to ensure that the new workset can be saved.
 *                                              
 *                                  Modifications
 *                                  1.  Renamed PTUDLL32Event.cs to VcuCommunication32Event.cs. Rev. 1.6. - Ref. 1.1.
 *                                  2.  Created VcuCommunication64Event.cs. Rev. 1.0. - Ref. 1.1.
 *                                  3.  Modified CommunicationEvent.cs. Rev. 1.11. - Ref.: 1.1.
 *                                  4.  Modified Resources.res. - Ref. 1.2.1, 1.2.2, 1.2.3, 1.2.4, 2. Also changed ToolTipText to be contained within
 *                                      square brackets e.g. F4 - [Continue].
 *                                  5.  Modified FormConfigureFaultLogParameters.Designer.cs Rev. 1.4, FormViewFaultLog.Designer.cs
 *                                      Rev. 1.1 and FormGetStream Rev. 1.1, FormViewEventLog.Designer.cs Rev. 1.3. - Ref. 1.2.1.
 *                                  6.  Modified FormWorksetDefineFaultLog.Designer.cs. Rev. 1.7 - Ref. 1.2.1, 1.2.3, 1.2.5.
 *                                  7.  Modified FormWorksetDefineFaultLog. Rev. 1.9 - Ref.: 1.2.5, 1.2.6.
 *                                  8.  Modified FormWorksetManagerFaultLog.resx. - Ref. 1.2.3.
 *                                  9.  Modified FormWorksetManagerFaultLog.Designer.cs. Rev. 1.3. - Ref.: 1.2.1.
 *                                  10. Modified FormConfigureFaultLogParameters.cs. Rev. 2.4. FormConfigureFaultLogParameters.Designer.cs. Rev. 1.4. - Ref.: 1.2.5,
 *                                      1.2.6, 3.
 *                                  11. Modified FormViewEventLog.cs. Rev. 1.15. - Ref.: 1.2.7.
 *
 */
#endregion - [1.18] -

#region - [1.19] -
/*
 *  04/15/15    1.19    K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 4800010525-CU2/19.03.2015.
 * 
 *                                      1.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified
 *                                          to meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the
 *                                          current naming convention will still apply.
 *                                          
 *                                      2.  NK-U-6505 Section 1.7.6. Data Sorting Capabilities. The proposal is to include an additional column in the spreadsheet that
 *                                          is used to view event logs (File/Open/Event Logs) that identifies the event log associated with the entry e.g. Maintenance,
 *                                          Engineering etc and allow the user to sort by this column. The format of the event log will obviously also have to be modified
 *                                          to include this information.
 *                                          
 *                                  2.  The height of the event variable user control and the DataGridView Row Height must be increased to allow characters such as
 *                                      'g', 'j', 'p', 'q', 'y' to be displayed correctly when using the default font.
 *                                      
 *                                  3.  SNCR - R188 PTU [20-Mar-2015] Item 11. Loaded up a number of event logs supplied by PIPPC into the PTU and discovered that
 *                                      a number of duplicate records were added to the DataGridView control.
 *                                          
 *                                      When multiple event log files are loaded into the PTU, the event index field of a number of events may be modified
 *                                      as part of the process that ensures that each index is unique. This is carried out in both the constructor and the event
 *                                      handler for the [F4-Load] key 'Click' event of the FormOpenEventLog class. When loading additional files, the PTU checks
 *                                      for duplicate entries by comparing the: car identifier, log name, event name, date/time and event index of each imported
 *                                      record against the current list of entries, however, if the event index field of records in the current list has been
 *                                      modified as part of the previous load process, duplicate records will be added to the list as they will now have a different
 *                                      event index value.
 *                                      
 *                                  4.  SNCR - R188 PTU [20-Mar-2015] Item 12. While viewing a saved event log, it was noticed that the event variables associated with
 *                                      some of the selected events were incorrect. On further investigation it was discovered that a bug has been introduced as a
 *                                      result of the changes made to address item 11 of SNCR - R188 PTU [20-Mar-2015].
 *                                      
 *                                      Initially, before the changes associated with item 11, each event index in the DataGridView control was unique,  therefore
 *                                      it was relatively easy to find the EventRecord in the list of EventRecords that matched the selected row of the DataGridView
 *                                      by doing a find on the event index e.g.
 *                                      
 *                                          short eventIndex = short.Parse((string)dataGridViewRow.Cells[ColumnIndexEventIndex].Value);
 *                                          // Find the entry in the list that matches the event index.
 *                                          EventRecord selectedEventRecord = EventRecordList.Find(delegate(EventRecord eventRecord)
 *                                          { return eventRecord.EventIndex == eventIndex; }); 
 *
 *                                      As a result of the changes associated with item 11 the event index value is no longer unique, therefore it is necessary to do
 *                                      a search on the following fields of the row/event record: date/time, event index, event name, log name, car identifier in order
 *                                      to match the selected row with the correct event record.       
 *                                              
 *                                      Modifications
 *                                  1.  Modified FormViewEventLog.Designer.cs. Rev. 1.4. - Ref.: 1.2, 2.
 *                                  2.  Modified FormOpenEventLog.cs Rev. 1.13. - Ref.: 1.1, 1.2, 3.
 *                                  3.  Modified FormViewEventLog.cs Rev. 1.16. - Ref.: 1.1, 1.2, 4.
 */
#endregion - [1.19] -

#region - [1.20] -
/*
 *  05/11/15    1.20    K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 4800010525-CU2/19.03.2015.
 *                                  
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                          Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                              
 *                                          1.  MOC-0171-29/34. Kawasaki requested that any function that is not available to the current mode, either Maintenance
 *                                              or Administrative, should not be displayed, or be 'greyed out'. BTPC agreed to review the software to ensure that
 *                                              non-active menu items are ‘greyed out’ or not shown.
 *                                      
 *                                      2.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                          labels to the status strip at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                          ‘Wibu-Key: [Present | Not Present]’.
 *                                          
 *                                  Modifications
 *                                  1.  Modified Resources.resx. - Ref.: 1.2.
 *                                  2.  Added EventRecord MostRecentEventSaved setting. Ref.: 1.2.
 *                                  3.  Modified FormViewEventLog.cs. Rev. 2.0. - Ref.: 1.2.
 *                                  4.  Modified FormOpenEvent.cs. Rev. 1.14. - Ref.: 1.1.1.
 */
#endregion - [1.20] -

#region - [1.21] -
/*
 *  07/13/15    1.21    K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 4800010525-CU2/19.03.2015.
 *                                  
 *                                      1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. Following a conference call on 9-Jul-15,
 *                                          the current proposal is to extend the options associated with the log saved status StatusLabel to include:
 *                                          ‘[Saved | Unsaved | Unknown | Not Applicable (-)]’. The log saved StatusLabel should also operate on a per
 *                                          car basis.
 *                                          
 *                                      2.  MOC-0171-29/34. Kawasaki requested that any function that is not available to the current mode, either Maintenance
 *                                          or Administrative, should not be displayed, or be 'greyed out'. BTPC agreed to review the software to ensure that
 *                                          non-active menu items are ‘greyed out’ or not shown.
 *                                              
 *                                          As a result of a further review, it is proposed that the following modifications are carried out:
 *                                              
 *                                              1.  The 'Select Data Dictionary' menu option should only visible when logged in as a BT engineer (Factory).
 *                                                  Also when visible, it should only be enabled when not in onlide, offline or self test mode.
 *                                                      
 *                                              2.  When in Self Test mode only the 'File/Exit', 'Help' and 'Login' main menu options should be enabled.
 *                                                  
 *                                              3.  When displaying event logs only the File/Exit', 'Configure/Worksets/Data Stream', 'Configure/Enumeration',
 *                                                  'Help', and 'Login' main menu options should be enabled.
 *                                                          
 *                                              4.  When displaying the Watch Window only the 'File/Exit', 'Configure/Worksets/Data Stream', 'Configure/Enumeration',
 *                                                  'Help', and 'Login' main menu options should be enabled.
 *                                          
 *                                  2.  Following the conference call on 9-Jul-15, it was decided that the Clear and Initialize event log functions should only
 *                                      be available to the Engineering account.
 *                                          
 *                                  3.  Bug Fix - SNCR - R188 PTU [20-Mar-2015]. If the user tries to clear or initialize an empty event log and opts to save 
 *                                      the event log first, an exception is thrown (ER-150712).
 *                                          
 *                                  Modifications
 *                                  1.  Replaced the 'EventRecord MostRecentEventSaved' setting with 'MostRecentDownloadedEvents MostRecentDownloadedEventsSaved'. The
 *                                      MostRecentDownloadedEvents setting is an array of the most recent downloaded event for each car. - Ref.: 1.1.
 *                                  2.  MenuInterfaceEvent.cs. Rev.: 1.7. - Ref.: 1.1.
 *                                  3.  FormViewEventLog.cs. Rev.: 2.1. - Ref.: 1.1, 1.2.3, 2, 3.
 */
#endregion - [1.21] -

#region - [1.22] -
/*
 *  07/28/15    1.22     K.McD      References
 *                                  1.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 24. On selecting the ‘Exit’ function key on the ‘Diagnostics/Event Log’ and
 *                                      the ‘Diagnostics/Self Tests’ screens, the cursor doesn’t go to the Cursors.WaitCursor cursor on the R188 project.
 *                                          
 *                                  Modifications
 *                                  1.  FormViewEventLog.cs. Rev. 2.2. - Ref.: 1.                                 
 */
#endregion - [1.22] -

#region - [1.23] -
/*
 *  08/11/15    1.23    K.McD       References
 *                                  1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                      from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                      
 *                                      1.  On the R188 project only, all references to 'PTU' are to be replaced with 'PTE' and all occurrences of 'Portable Test Unit'
 *                                          are to be replaced with 'Portable Test Equipment'. Where support for multiple legends is not possible, 'Portable Test
 *                                          Application' is to be used as an alternative to 'Portable Test Unit'/'Portable Test Equipment.
 *                                              
 *                                  Modifications
 *                                  1.  AssemblyInfo.cs. Rev. 1.23. Changed the ApplicationProduct attribute to 'Portable Test Application'.
 *                                  2.  Resources.resx. Removed any reference to PTU in the message box captions e.g. replaced 'PTU - Error' with 'Error'.
 */
#endregion - [1.23] -
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

[assembly: AssemblyTitle("Portable Test Application - Event Sub-system")]
[assembly: AssemblyDescription("A library of classes to support event recording.")]
[assembly: AssemblyConfiguration("")]

// Setting ComVisible to false makes the types in this assembly not visible to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("501ab9fd-7b89-417e-9afd-b25dc457a9a3")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers by using the '*'.
[assembly: AssemblyVersion("1.23.0.0")]