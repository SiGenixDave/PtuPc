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
 *  Project:    Event
 * 
 *  File name:  FormViewEventLog.cs
 * 
 *  Revision History
 *  ----------------
 */

/* 
 *  Date        Version Author          Comments
 *  10/26/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  12/01/10    1.1     K.McD           1.  Added the event handler for the F3-Save function key. This saves the currently selected event log to disk in XML format.
 *                                      2.  Added the event handler for the F8-Stream function key. This displays the form that allows the user to define the data 
 *                                          stream parameters.
 * 
 *  01/07/11    1.2     K.McD           1.  Added support for the dynamic update of events using the IPollTarget interface and the ThreadPollEvent class.
 *                                      2.  Minor changes as follows: (1) added a number of constants, (2) changed the modifiers of a number of member variables so that 
 *                                          they can be accessed by child classes (3) changed the values of a number of constants and (4) modified a number of XML 
 *                                          tags and comments.
 *                                      3.  Added the following properties: (1) MutexCommunicationInterface,  (2) EventRecordList, (3) EventCount and (4) Log.
 *                                      4.  Added mutexes to control the read/write access to the: (1) DataGridView control, (2) EventCount property and (3) 
 *                                          communication interface.
 *                                      5.  Added the date and time information labels.
 *                                      6.  Renamed the AddRecordsToDataGridView() method to AddEventRecordListToDataGridView() and included a call to the Sort() method 
 *                                          of the DataGridView control to ensure that the records are sorted by the selected field.
 *                                      7.  Modified the m_DataGridViewEventLog_CellContentDoubleClick() method to be a virtual method.
 *                                      8.  Modified the m_DataGridViewEventLog_SelectionChanged() method to be a virtual method and removed the section of code that 
 *                                          retrieved the event variables associated with the selected event from the VCU as this is no longer necessary because the 
 *                                          CommunicationEvent.GetEventRecord() method has been modified to include retrieval of the event variables associated with 
 *                                          the record.
 *                                      9.  Modified the ConvertToDataGridViewRow() method to accommodate the additional fields in the DataGridView control.
 *                                      10. Replaced the ClearDownloadedEventLog() method with the following methods: (1) ClearEventMemory() and (2) 
 *                                          ClearEventLogControls().
 *                                      11. Included support to: (1) save the event log to disk in XML format and (2) save all data streams associated with the current 
 *                                          event log to disk.
 * 
 *  01/12/11    1.3     K.McD           1.  Bug fix SNCR001.84. Added the second parameter to the call to the System.Threading.WaitHandle.WaitOne() method, as advised by 
 *                                          the following blog entries: 
 *                                              (1) http://www.mikeplate.com/missingmethodexception-and-waitone/ and (2) 
 *                                              (2) http://blog.darrenstokes.com/2009/03/30/watch-out-for-those-waitone-overloads-when-you-need-backwards-compatibility 
 * 
 *  01/14/11    1.4     K.McD           1.  Bug fix SNCR001.85. There appears to be a bug in the CheckFaultLogger() method of the PTUDLL322 dynamic link library in that 
 *                                          the event count value returned from the call can sometimes increment by the number of existing entries in the event log every 
 *                                          time that the call is made. This problem is sometimes cleared by calling the ClearEvent() method the same library, however, 
 *                                          in order to circumvent the problem the design of the dynamic update of the event log was modified to use the event index 
 *                                          value that is returned from the call instead as this value is correct. As part of this bug fix the EventIndex property 
 *                                          was added to this class.
 * 
 *  01/18/11    1.5     K.McD           1.  Bug fix SNCR001.79. Modified the GetEventLog() method to update the StreamNumber property of the EventRecord class. If 
 *                                          the record does not have an associated data stream the stream number property will be set to CommonConstants.NotUsed.
 * 
 *  01/19/11    1.6     K.McD           1.  SNCR001.79/SNCR001.76. Added the StreamNumber property and made sure that it wrapped around back to 0 when the count reaches 
 *                                          the maximum number of data streams that the VCU is capable of recording.
 * 
 *  01/26/11    1.7     K.McD           1.  Modified the GetEventRecord() method to use the DataStreamCount property of the current log to determine the value of 
 *                                          the StreamNumber property.
 * 
 *                                      2.  Modified the m_ToolStripComboBox1_SelectedIndexChanged() method to get the current log directly from the SelectedItem 
 *                                          property of the ComboBox control. 
 * 
 *  01/31/11    1.8     K.McD           1.  Request - SNCR001.72. The user should have the ability to save an event log to an individual event log XML files or 
 *                                          append to an existing file. Implemented the following modification to accommodate this request:
 * 
 *                                              a.  Refactored the Save, Clear and Reset function key event handlers.
 *                                              b.  As part of the refactoring process created the following methods: Save(), SaveAll(), SaveCurrent(), ClearOrReset(), 
 *                                                  ClearCurrent(), CheckForAppend().
 *                                              c.  Renamed a number of methods.
 * 
 *                                      2.  Modifications to accommodate the communication mutex introduced into the Common.CommunicationParent class in version 1.11 of 
 *                                          Common.dll.
 * 
 *  02/02/11    1.9     K.McD           1.  Standardized the function key event handlers to: display the wait cursor, enable the Checked property of the function key and 
 *                                          clear any status message.
 *                                      2.  Added the DateTime and Log fields to the DataGridView control.
 *                                      3.  Added an event handler for the SortCompare event of the DataGridView control to control the sort order of the rows.
 *                                      4.  Modified a number of XML tags.
 * 
 *  02/15/11    1.9.1   K.McD           1.  Removed the StreamNumber property.
 *                                      2.  Modified the GetEventLog() method such that the StreamNumber parameter of each event record is no longer calculated as 
 *                                          this is now derived from the call to PTUDLL32.GetFaultHdr() method.
 * 
 *  02/21/11    1.9.2   K.McD           1.  Renamed and changed the modifiers of a number of delegates and methods.
 *                                      2.  Minor changes to a number of XML tags.
 *                                      3.  Included support for the FormGetStream() method which shows the progress of the data stream download.
 *                                      4.  Added the DataStreamCurrent property - required to retrieve any data stream downloaded using the FormGetStream class.
 *                                      5.  Renamed the 'Reset' function key to 'Initialize'.
 *                                      6.  Don't show the history and flags funtion keys until these features have been implemented.
 *                                      7.  Modified the Save, Clear and Reset function key event handlers to use the StartPolling/StopPolling() methods to control 
 *                                          polling of the VCU for new events.
 *                                      8.  Deprecated the SaveCurrent() and ClearCurrent() methods. These methods are no longer required as the current log need not 
 *                                          be cleared when modifying the data stream parameters.
 *                                      9.  Bug - fix - SNCR001.102. Modified the GetEventLog() method to ignore any event where a match for the LOGID, TASKID and 
 *                                          EVENTID values returned from the VCU cannot be found in the EVENTS table of the data dictionary.
 *                                      10. Moved the GetFullyQualifiedFaultLOgFilename() methods to the Common.General class.
 *                                      11. Modified the SaveAll() method to restore the current log after saving the event logs and data streams.
 * 
 *  02/28/11    1.9.3   K.McD           1.  Modified a number of XML tags.
 *                                      2.  Removed the constants associated with the size of the DataGridView control margins.
 *                                      3.  Modified the values associated with the event variable control margins and the event variable control width.
 *                                      4.  Removed the member variable recoding the size of the DataGridView control.
 *                                      5.  Modified the image associated with the 'F5-Initialize' function key.
 *                                      6.  Added the event count information label.
 *                                      7.  Add the header label to the event variable list.
 *                                      8.  Modified the code to call the StopPolling() and StartPolling() methods to suspend polling of the target hardware for new
 *                                          events in: (a) the event handlers for the 'F3-Save', F4-Clear', F5-Initialize' and 'F8-Setup Stream' function keys, (b) the
 *                                          event handler for the CellContentDoubleClick event of the DataGridView control and (c) the Exit() method rather than relying
 *                                          on the SetPauseAndWait() method /Pause property as this sometimes resulted in an AccessViolation exception following the
 *                                          dowload of the data streams.
 *                                      9.  Included support for (a) blinking the data received icon and (b) communication fault processing in the DisplayUpdate() method.
 *                                      10. Modified the StopPolling() method to set the polling thread to null once the thraed has been killed.
 *                                      11. Modified the StartPolling() method such that the thread responsible for polling the VCU is only started if the thread
 *                                          is currently null.
 *                                      12. Include calls to the SuspendLayout() and PerformLayout() methods in the Exit() method.
 *                                      13. Changed the modifiers associated with a number of methods.
 *                                      14. Modified SaveAll() method to display the event variable list once all events have been saved.
 * 
 *  03/02/11    1.9.4   K.McD           1.  Added the FormatStringDateTime constant to define the string format that is to be used when converting a .NET DateTime 
 *                                          value to an entry in the DateTime column of the DataGridView control.
 * 
 *                                      2.  Modified the m_DataGridViewEventLog_SortCompare() method such that the value stored in the Log column of the DataGridView 
 *                                          control is ignored when determining the sort order.
 * 
 *                                      3.  Modified the signature of the SortBy() method such that only one fieldIndex parameter is passed. This modification is linked
 *                                          to the modification associated with (2).
 * 
 *                                      4.  Modified the GetEventLog() method such that the returned event record list is sorted in descending date/time order i.e. most 
 *                                          recent event first. This ensures that when the records are added to the DataGridView control the first row of the 
 *                                          DataGridView control will be selected.
 * 
 *                                      5.  Added the CompareByDateTimeDescending() method. This is the Comparison<T> delegate used to sort the event record list by 
 *                                          date/time descending order.
 * 
 *  03/17/11    1.9.5   K.McD           1.  Modified a number of XML tags and comments.
 *                                      2.  Modified the width of the event variable user controls.
 *                                      3.  Added support for the WinHlp32 help engine to display the help diagnostic information for the events and event variables.
 *                                      4.  Modified the event handler for the DataGridView SelectionChanged event to close any FormShowFlags forms that may be 
 *                                          open.
 * 
 *                                      5.  Modified the event handler for the DataGridView CellContentDoubleClick event to include a check to determine 
 *                                          which column of the DataGridView has been selected by the user and then either: (a) display the diagnostic help 
 *                                          information associated with the selected event or (b) download and display the fault log associated with the 
 *                                          selected event, as appropriate.
 * 
 *                                      6.  Refactored the event handler for the DataGridView CellContentDoubleClick event such that the code responsible for 
 *                                          downloading and displaying the fault log is now included in the newly created ShowFaultLog() method.
 * 
 *  03/21/11    1.9.6   K.McD           1.  Removed the Virtual modifier from the event handlers associated with the DataGridView 'SelectionChanged' and 
 *                                          'CellContentDoubleClick' events as these event handlers are no longer overridden in the FormOpenEventLog child class.
 *                                      2.  Added the Virtual modifier to the ShowFaultLog() method as this is now overridden in the FormOpenEventLog child class.
 *                                      3.  Added the member variable to record the number of common event variables per event associated with the current data
 *                                          dictionary.
 *                                      4.  Modified the ConfigureControls() method such that the individual event variable user controls are laid out on the panel 
 *                                          similar to rows on a DataGridView control. The first entry in the specified list of event variables, eventVariableList[0], 
 *                                          is positioned at row 0, the second at row 1 etc. Also modified the signature to allow the programmer to specify the starting 
 *                                          index in the list of event variables where configuration is to begin, 0 to configure all event variables in the list, etc.
 *                                      5.  Modified the DisplayEventVariableList() method such that only those event variable user controls which are specific to 
 *                                          the selected event are re-configured. The common event variable user controls are no longer re-configured for each selected 
 *                                          event.
 *                                      6.  Added the CompareByDateTimeAscending() method to allow the list of event records to be sorted in ascending order i.e. oldest 
 *                                          event first.
 * 
 *  03/28/11    1.9.7   K.McD           1.  Modified the event handlers associated with the Save and Clear function keys to clear the status message on completion.
 *                                      2.  Modified the names of a number of local variables.
 * 
 *  04/06/11    1.9.8   K.McD           1.  Modified the class constructor to include function keys that: (a) allow the user to configure the event flags associated 
 *                                          with the current log and (b) display the event history.
 * 
 *                                      2.  Added the event handlers associated with the function keys mentioned in item 1.
 * 
 *  04/08/11    1.9.9   K.McD           1.  Corrected the caption parameter associated with a number of message boxes.
 *  
 *  07/20/11    1.9.10  K.McD           1.  Modified the signature of the constructor to use the ICommunicationParent interface.
 *  
 *  07/27/11    1.10    K.McD           1.  SNCR001.148. Bug fix associated with the CTA site visit on 15th July 2011. An exception was thrown when the user selected 
 *                                          an event from the downloaded event log. Further investigation showed that the exception was thrown because the previously 
 *                                          selected event had no event secific event variables associated with it.
 *                                          
 *                                              (a) Changed the modifier of the member variable that stores the number of common event variables recorded for each 
 *                                                  system event to protected.
 *                                                  
 *                                              (b) Modified the DisplayEventVariables() method to check whether the number of controls added to the Panel control 
 *                                                  associated with the list of event variables for the previous event was greater than or equal to the number of 
 *                                                  common event variables rather than simply greater than.
 *                                                  
 *                                      2.  SNCR001.135. Bug fix associated with the event log displaying the incorrect number of events in the current list if the list 
 *                                          is empty. Modified the m_ToolStripComboBox1_SelectedIndexChanged() method to update the label that shows the number of 
 *                                          events in the list to display zero if the list is empty.
 *                                          
 *  08/10/11    1.11    Sean.D          1.  Included support for offline mode. Modified the constructor to conditionally choose CommunicationEvent or 
 *                                          CommunicationEventOffline depending upon the current mode.
 *                                          
 *                                      2.  Minor adjustments to the Visible property of the event variables panel to improve screen build/clear appearance.
 *                                      
 *  08/24/11    1.12    K.McD           1.  Added a number of constants and renamed a number of variables.
 *                                      2.  Modified the SetPauseAndWait() method to include a timeout.
 *                                      3.  Included support to monitor the watchdog associated with the thread that is responsible for communication with the target.
 *                                      4.  Included checks on the success of the call to the IPollTarget.SetPauseAndWait() method in a number of methods.
 *                                      5.  Removed the deprecated methods.
 *                                      
 *  10/26/11    1.13    K.McD           1.  SNCR002.41. Added checks to the event handlers associated with all ToolStripButton controls to ensure that the event handler 
 *                                          code is ignored if the Enabled property of the control is not asserted.
 *                                          
 *  11/23/11    1.14    K.McD           1.  Modified the FormViewEventLog class.
 *                                              (a) Ensured that all event handler methods were detached.
 *                                              (b) Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after the Close() 
 *                                                  method had been called.
 *                                                  
 *  07/24/13    1.14.1  K.McD           1.  Modified the event handler for the F8-Stream function key. Now passes a reference to the current event log when instantiating 
 *                                          the  FormConfigureFaultLogParameters class so that the number of watch variables associated with the current event log are 
 *                                          known by the form that is used to configure the watch variables and a check may be made to ensure that the selected workset
 *                                          does not include more watch variables than can be supported by the event log. This requirement is only associated with
 *                                          projects that do not record a fixed number of watch variables for each event log.
 *                                          
 *                                      2.  Modified the Exit() method to close the communication port and to set the mode to configuration mode if there is a
 *                                          communication fault or the watchdog has tripped i.e. the port is locked. Modified to be consistent with the FormViewWatch
 *                                          class.
 *                                          
 *  04/07/15    1.15    K.McD           References
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
 *                                                  As a result of the review, it is proposed that the following modifications are carried out:
 *                                              
 *                                                  1.  The 'Select Data Dictionary' menu option should only visible when logged in as a BT engineer (Factory).
 *                                                      Also when visible, it should only be enabled when not in onlide, offline or self test mode.
 *                                                      
 *                                                  2.  When in Self Test mode only the 'File/Exit', 'Help' and 'Login' main menu options should be enabled.
 *                                                  
 *                                                  3.  When displaying event logs only the File/Exit', 'Configure/Worksets/Data Stream', 'Configure/Enumeration',
 *                                                      'Help', and 'Login' main menu options should be enabled.
 *                                                          
 *                                                  4.  When the Watch Window is paused or data is being recorded only the 'File/Exit', 'Help' and 'Login' main menu
 *                                                      options should be enabled.
 *
 *                                      Modifications
 *                                      1.  Modified the image resource references specified in the calls to the DisplayFunctionKey() method for function keys
 *                                          F6, F7, F8 in the constructor.
 *                                          
 *                                      2.  Modified the 'Shown' event handler to disable the appropriate menu options using calls to the SetMenuEnabled() method. 
 */

/*
 *  04/15/15    1.16    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 4800010525-CU2/19.03.2015.
 * 
 *                                          1.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified
 *                                              to meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the
 *                                              current naming convention will still apply.
 *                                              
 *                                          2.  NK-U-6505 Section 1.7.6. Data Sorting Capabilities. The proposal is to include an additional column in the spreadsheet
 *                                              that is used to view event logs (File/Open/Event Logs) that identifies the event log associated with the entry e.g.
 *                                              Maintenance, Engineering etc and allow the user to sort by this column. The format of the event log will obviously also
 *                                              have to be modified to include this information.
 *                                              
 *                                      2.  The 'F2-Print' function key should be enabled when viewing event logs.
 *                                      
 *                                      3.  The height of the event variable user control and the DataGridView Row Height must be increased to allow characters such as
 *                                          'g', 'j', 'p', 'q', 'y' to be displayed correctly when using the default font.
 *                                          
 *                                      4.  SNCR - R188 PTU [20-Mar-2015] Item 12. While viewing a saved event log, it was noticed that the event variables associated
 *                                          with some of the selected saved events were incorrect. On further investigation it was discovered that a bug has been
 *                                          introduced as a result of the changes made to address item 11 of SNCR - R188 PTU [20-Mar-2015].
 *                                      
 *                                          Initially, before the changes associated with item 11, each event index in the DataGridView control was unique,  therefore
 *                                          it was relatively easy to find the EventRecord in the list of EventRecords that matched the selected row of the DataGridView
 *                                          by doing a find on the event index e.g.
 *                                      
 *                                          short eventIndex = short.Parse((string)dataGridViewRow.Cells[ColumnIndexEventIndex].Value);
 *                                          // Find the entry in the list that matches the event index.
 *                                          EventRecord selectedEventRecord = EventRecordList.Find(delegate(EventRecord eventRecord)
 *                                          { return eventRecord.EventIndex == eventIndex; }); 
 *
 *                                          As a result of the changes associated with item 11 the event index value is no longer unique, therefore it is necessary to do
 *                                          a search on the following fields of the row/event record: date/time, event index, event name, log name, car identifier
 *                                          in order to match the selected row with the correct event record.
 *                                              
 *                                      Modifications
 *                                      1.  Modified the signature in the call to the General.DeriveName() method in the Save() method. The original
 *                                          signature has been removed in order to simplify file naming. - Ref. 1.1.
 *                                      2.  Removed the 'F2.Enabled = false' statement in the constructor. - Ref.: 2.
 *                                      3.  Modified the height of the EventControl user control to 28 pixels. - Ref.: 3.
 *                                      4.  Modified the  m_DataGridViewEventLog_SortCompare() method to include the LogName field in the sort process if
 *                                          the LogName column is visible. Also included the event index field to determine the sequence of events for events that have
 *                                          the same time value. - Ref.: 1.2.
 *                                      5.  Added an additional signature to the SortBy() method to allow the LogName field to be included in the sort process. Also
 *                                          included the event index field to determine the sequence of events for events that have the same time value. - Ref.: 1.2.
 *                                      6.  Modified the  CompareByDateTimeAscending() and  CompareByDateTimeDescending() methods to include the LogName field in the
 *                                          sort process if the LogName column is visible. Also included the event index field to determine the sequence of events for
 *                                          events that have the same time value. - Ref.: 1.2.
 *                                      7.  Included the method FindEventRecord() that finds an event record in the specified event record list that matches
 *                                          the specified row of the DataGridView by matching: date/time, event index, event name, log name and car identifier.
 *                                      8.  Replaced the Find shown in reference 4, above, with a call to the FindEventRecord() method in the following methods:
 *                                      
 *                                              m_MenuItemShowDefinition_Click()
 *                                              m_DataGridViewEventLog_SelectionChanged()
 *                                              m_DataGridViewEventLog_CellContentDoubleClick()   
 *                                              
 *                                      9.  Added a check to the 'Resize' event handler to return from the method if the class has been disposed.
 */

/*
 *  05/11/15    2.0     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status strip at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘Wibu-Key: [Present | Not Present]’.
 *  
 *                                      Modifications
 *                                      1.  Added the m_LogSaved flag.
 *                                      2.  Modified the FormViewEventLog_Shown() event handler to synchronise the m_LogSaved flag and the MainWindow.LogSaved
 *                                          property.
 *                                      3.  Modified the AddList() method to check whether the most recent dowloaded event is newer than the most recent
 *                                          saved event, which is stored in the MostRecentEventSaved project setting, and to clear the MainWindow.LogSaved property and
 *                                          m_LogSaved flag if this is true.
 *                                      4.  Modified the SaveAll() method to keep a record of the most recent saved event in the MostRecentEventSaved project
 *                                          settings.
 * 
 */

/*
 *  07/13/15    2.1     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 4800010525-CU2/19.03.2015.
 *                                      
 *                                          1. NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. Following a conference call on 9-Jul-15,
 *                                             the current proposal is to extend the options associated with the log saved status StatusLabel to include:
 *                                             ‘[Saved | Unsaved | Unknown | Not Applicable (-)]’. The log saved StatusLabel should also operate on a per
 *                                             car basis.
 *                                             
 *                                          2.  MOC-0171-29/34. Kawasaki requested that any function that is not available to the current mode, either Maintenance
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
 *                                      2.  Review of the code discovered that a number of void methods did not return if the Dispose() method had been called.
 *                                          
 *                                      3.  Following the conference call on 9-Jul-15, it was decided that the Clear and Initialize event log functions should only
 *                                          be available to the Engineering account.
 *                                          
 *                                      4.  Bug Fix - SNCR - R188 PTU [20-Mar-2015]. If the user tries to clear or initialize an empty event log and opts to save 
 *                                          the event log first, an exception is thrown (ER-150712).
 *                                              
 *                                      Modifications
 *                                      1.  Added a check to all methods that return a void to return if the Dispose() method has been called. - Ref.: 2.
 *                                      2.  Added the KeyInitializeEventLog constant to access the InitializeEvent Log function. - Ref.: 3.
 *                                      3.  Added the m_EventLogSavedStatus variable.
 *                                      4.  Modified the signature of the class to include a reference to the MainWindow interface. This allows the 
 *                                          properties of the MainWindow interface to be accessed within the constructor. Ref.: 1.1.
 *                                      5.  Included a call in the constructor to initialize the MostRecentDownloadedEventsSaved setting if it is a null value.
 *                                          Ref.: 1.1.
 *                                      6.  Modified the Cleanup() method to detach the event handlers for the m_TimerDisplayUpdate.Tick and
 *                                          MainWindow.MenuUpdated events and to set m_TimerDisplayUpdate to null. - Ref.: N/A.
 *                                      7.  Removed any references to the m_LogSaved variable and MainWindow.LogSaved property. - Ref.: 1.1.
 *                                      8.  Modified the Shown event handler to: disable the 'Configure/Real Time Clock' and 'Configure/Password Protection' menu
 *                                          options, register the SecurityChanged event handler to the MainWindow.MenuUpdated event, and to call the SecurityChanged
 *                                          event handler to update the Enabled properties of the Initualize and Clear function keys.. - Ref.: 1.2.3.
 *                                      9.  Added the Security changed event handler for the MainWindow.MenuUpdated event. This handler updates the Enabled property
 *                                          of those function keys that are Security dependent. - Ref.: 3.
 *                                      10. Modified the AddList method to: (a) set the Enabled property of the Initialize function key to true and to call the
 *                                          SecurityChanged event handler to ensure that the current Enabled property values are valid for the current security level;
 *                                          (b) set the m_EventLogSavedStatus variable depending upon the result of the comparison between the current most recent
 *                                          event and the saved most recent event; and (c) update the MainWindow LogStatus StatusLabel. - Ref.: 1.1, 3.
 *                                      11. Modified the SaveAll() method to: (a) update the MostRecentDownloadedEventsSaved setting with the most recent downloaded event
 *                                          for the current car; and (b) only make the call to update the event variable list if the number of selected DataGridView
 *                                          row is one or more. - Ref.: 1.1, 4.
 */

/*
 *  07/28/15    2.2     K.McD           References
 *                                      1.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 24. On selecting the ‘Exit’ function key on the ‘Diagnostics/Event Log’ and
 *                                          the ‘Diagnostics/Self Tests’ screens, the cursor doesn’t go to the Cursors.WaitCursor cursor on the R188 project.
 *                                          
 *                                      Modifications
 *                                      1.  Modified the Exit() method to use ‘Cursor.Current = Cursors.WaitCursor’ rather than ‘this.Cursor = Cursors.WaitCursor’.
 *                                      - Ref.: 1.
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using Common;
using Common.Communication;
using Common.Configuration;
using Common.Forms;
using Common.UserControls;
using Event.Communication;
using Event.Properties;

namespace Event.Forms
{
    #region --- Delegates ---
    /// <summary>
    /// A delegate for a method that has generic list of event records as an input parameter and does not return a value.
    /// </summary>
    /// <param name="eventRecordList">The list of <c>EventRecord</c> types.</param>
    public delegate void AddListDelegate(List<EventRecord> eventRecordList);
    #endregion --- Delegates ---

    /// <summary>
    /// A form to allow the user to: (1) view and save the selected event log, (2) view the event variables associated with an individual event and (c) call the 
    /// form that displays the fault log associated with an individual event, if available.
    /// </summary>
    public partial class FormViewEventLog : FormPTU, IPollTarget, ICommunicationInterface<ICommunicationEvent>
    {
        #region --- Delegates ---
        /// <summary>
        /// A delegate for a method that takes no parameters and does not return a value.
        /// </summary>
        /// <returns></returns>
        public delegate void ZeroParameterDelegate();
        #endregion --- Delegates ---

        #region --- Constants ---
        /// <summary>
        /// The interval, in ms, between successive display updates. Value: 500 ms.
        /// </summary>
        private const int IntervalDisplayUpdateMs = 500;

        /// <summary>
        /// The countdown value associated the watchdog trip. Value: 10.
        /// </summary>
        private int WatchdogTripCountdown = 10;

        /// <summary>
        /// The index of the initial log that is to be shown. Value: 0.
        /// </summary>
        public const short InitialLogIndex = 0;

        /// <summary>
        /// The horizontal border associated with the event log <c>DataGridViewControl.</c> Value: 20.
        /// </summary>
        protected const int DataGridViewEventLogHorizontalBorder = 25;

        /// <summary>
        /// The default number of ms to wait before releasing the mutex. Value: 2000 ms.
        /// </summary>
        protected const int DefaultMutexWaitDurationMs = 2000;

        /// <summary>
        /// The length of the string used to represent days of the week.
        /// </summary>
        private const int LengthDayOfWeekString = 3;

        /// <summary>
        /// The format string that is to be used to display the time of the event. Value: "HH:mm:ss".
        /// </summary>
        private const string FormatStringEventTime = "HH:mm:ss";

        /// <summary>
        /// The format string to be used when converting the date/time to the entry in the DateTime column of the <c>DataGridView</c>. control. Value:
        /// "yyyy/MM/dd HH:mm:ss.ff".
        /// </summary>
        protected const string FormatStringDateTime = "yyyy/MM/dd HH:mm:ss.ff";

        /// <summary>
        /// The key string used to represent the CommunicationEvent.ClearEvent() method. Value: "ClearEvent".
        /// </summary>
        private const string KeyClearEvent = "ClearEvent";

        /// <summary>
        /// The key string used to represent the CommunicationEvent.InitializeEventLog() method. Value: "InitializeEventLog".
        /// </summary>
        private const string KeyInitializeEventLog = "InitializeEventLog";

        /// <summary>
        /// The text representation associated with a true state. Value: "true".
        /// </summary>
        private const string True = "true";

        /// <summary>
        /// The text representation associated with a false state. Value: "false".
        /// </summary>
        private const string False = "false";

        #region - [DataGridView Column Indices] -
        /// <summary>
        /// The event index column index. Value: 0;
        /// </summary>
        protected const int ColumnIndexEventIndex = 0;

        /// <summary>
        /// The identifier column index. Value: 1;
        /// </summary>
        protected const int ColumnIndexIdentifier = 1;

        /// <summary>
        /// The car identifier column index. Value: 2.
        /// </summary>
        protected const int ColumnIndexCarIdentifier = 2;

        /// <summary>
        /// The log column index. Value: 3.
        /// </summary>
        protected const int ColumnIndexLog = 3;

        /// <summary>
        /// The event name column index. Value: 4.
        /// </summary>
        protected const int ColumnIndexEventName = 4;

        /// <summary>
        /// The DateTime column index. Value: 5.
        /// </summary>
        protected const int ColumnIndexDateTime = 5;

        /// <summary>
        /// The date column index. Value: 6.
        /// </summary>
        protected const int ColumnIndexDate = 6;

        /// <summary>
        /// The time column index. Value: 7.
        /// </summary>
        protected const int ColumnIndexTime = 7;

        /// <summary>
        /// The day of the week column index. Value: 8.
        /// </summary>
        protected const int ColumnIndexDay = 8;

        /// <summary>
        /// The stream available text column index. Value: 9.
        /// </summary>
        protected const int ColumnIndexStreamAvailableText = 9;

        /// <summary>
        /// The stream available image column index. Value: 10.
        /// </summary>
        protected const int ColumnIndexStreamAvailableImage = 10;
        #endregion - [DataGridView Column Indices] -

        #region - [Heights] -
        /// <summary>
        /// The height, in pixels, of the event variable user control. Value: 22.
        /// </summary>
        public const int HeightEventControl = 28;
        #endregion - [Heights] -

        #region - [Margins] -
        /// <summary>
        /// The right margin to be applied to the <c>DataGridView</c> control. Value: 2.
        /// </summary>
        public const int MarginRightDataGridViewControl = 2;

        /// <summary>
        /// The left margin associated with the event variable user control. Value: 10.
        /// </summary>
        public const int MarginLeftEventControl = 10;

        /// <summary>
        /// The right margin associated with the event variable user control. Value: 2.
        /// </summary>
        public const int MarginRightEventControl = 2;

        /// <summary>
        /// The top margin associated with the event variable user control. Value: 2.
        /// </summary>
        public const int MarginTopEventControl = 2;

        /// <summary>
        /// The bottom margin associated with the event variable user control. Value: 2.
        /// </summary>
        public const int MarginBottomEventControl = 2;
        #endregion - [Margins] -

        #region - [Widths] -
        /// <summary>
        /// The width, in pixels, of the variable name field of the event variable user control. Value: 200.
        /// </summary>
        public const int WidthEventControlVariableNameField = 200;

        /// <summary>
        /// The width, in pixels, of the value field of the event variable user control. Value: 170.
        /// </summary>
        public const int WidthEventControlValueField = 170;

        /// <summary>
        /// The width, in pixels, of the units field of the watch variable user control. Value: 75.
        /// </summary>
        public const int WidthEventControlUnitsField = 75;
        #endregion - [Widths] -
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Mutex to control read/write access to the <c>DataGridView</c> control.
        /// </summary>
        internal Mutex m_MutexDataGridView;

        /// <summary>
        /// Mutex to control read/write access to the <c>EventCount</c> property.
        /// </summary>
        internal Mutex m_MutexEventCount;

        /// <summary>
        /// Mutex to control read/write access to the <c>EventIndex</c> property.
        /// </summary>
        internal Mutex m_MutexEventIndex;

        /// <summary>
        /// Reference to the structure that defines the size of the event variable user controls.
        /// </summary>
        protected VariableControlSize_t m_EventVariableControlSize;

        /// <summary>
        /// Reference to the structure that defines the size of the <c>DataGridView</c> control.
        /// </summary>
        protected UserControlSize_t m_DataGridViewEventLogSize;

        /// <summary>
        /// The list of the event records retrieved from the VCU.
        /// </summary>
        protected List<EventRecord> m_EventRecordList = new List<EventRecord>();

        /// <summary>
        /// The number of common event variables associated with each event. These are the event variables contained within the STRUCT generic list corresponding to 
        /// structure identifier 0, less those event variables defined as header event variables.  
        /// </summary>
        protected int m_CommonEventVariableCount;

        /// <summary>
        /// The selected event log.
        /// </summary>
        private Log m_Log;

        /// <summary>
        /// The saved status of the event logs. 
        /// </summary>
        private EventLogSavedStatus m_EventLogSavedStatus = EventLogSavedStatus.Undefined;

        /// <summary>
        /// The System.Windows.Forms timer used to manage the display update.
        /// </summary>
        private System.Windows.Forms.Timer m_TimerDisplayUpdate;

        /// <summary>
        /// The number of events in the current event log.
        /// </summary>
        private short m_EventCount;

        /// <summary>
        /// The event index of the next entry in the event log.
        /// </summary>
        private uint m_EventIndex;

        /// <summary>
        /// Reference to the latest data stream downloaded from the VCU.
        /// </summary>
        private DataStream_t m_DataStream;

        #region - [Communication] -
        /// <summary>
        /// Reference to the class that is responsible for polling the VCU for new events.
        /// </summary>
        private ThreadPollEvent m_ThreadPollEvent;

        /// <summary>
        /// A record of the packet count. Used to determine if polling is active so that the packet received icon can be blinked in a thread-safe way.
        /// </summary>
        private long m_PacketCount;

        #region - [Watchdog] -
        /// <summary>
        /// A record of the watchdog count. Used to determine if the thread on which the polling is carried out has locked.
        /// </summary>
        private int m_Watchdog;

        /// <summary>
        /// A flag that indicates whether a watchdog trip has occurred.
        /// </summary>
        private bool m_WatchdogTrip;

        /// <summary>
        /// The countdown to the watchdog trip.
        /// </summary>
        private int m_WatchdogTripCountdown;
        #endregion - [Watchdog] -

        /// <summary>
        /// A flag that indicates whether a communication fault has been detected.
        /// </summary>
        private bool m_CommunicationFault = false;

        /// <summary>
        /// A flag to control the display update. True, stops the display update i.e pauses the display; false, re-starts the display update.
        /// </summary>
        private bool m_Pause = false;

        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        private ICommunicationEvent m_CommunicationInterface;
        #endregion - [Communication] -
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormViewEventLog()
        {
            InitializeComponent();

            m_MutexDataGridView = new Mutex();
        }

        /// <summary>
        /// Initializes a new instance of the class. Initializes: (1) the communication interface, (2) the size of each control, (3) the 
        /// function keys, (4) the <c>ComboBox</c> control (5) the display update timer and (6) the title. 
        /// </summary>
        /// <param name="communicationInterface">Reference to the communicaton interface that is to be used to communicate with the VCU.</param>
        /// <param name="mainWindow">Reference to the main window.</param>
        /// <remarks>The IMainWindow reference must be passed in the constructor as some of its properties must be accessed in the constructor and the normal method
        /// where it is defined prior to the form being show is not suitable.</remarks>
        public FormViewEventLog(ICommunicationParent communicationInterface, IMainWindow mainWindow)
        {
            InitializeComponent();

            // Initialize the communication interface.
            if (communicationInterface is CommunicationParent)
            {
                CommunicationInterface = new CommunicationEvent(communicationInterface);
            }
            else
            {
                CommunicationInterface = new CommunicationEventOffline(communicationInterface);
            }

            Debug.Assert(CommunicationInterface != null, "FormViewEventLog.Constructor() - CommunicationInterface != null");

            MainWindow = mainWindow;
            Debug.Assert(MainWindow != null, "FormViewEventLog.Constructor() - MainWindow != null");

            // Check whether the setting corresponding to an array of the the most recent downloaded event for each car (Settings.Default.
            // MostRecentDownloadedEventsSaved) is null and, if so, instantiate it. This must be done before any calls to AddList() are made.
            // Note: AddList() is called by the m_ToolStripComboBox1_SelectedIndexChanged() event handler.
            if (Settings.Default.MostRecentDownloadedEventsSaved == null)
            {
                Settings.Default.MostRecentDownloadedEventsSaved = new MostRecentDownloadedEvents();
                Settings.Default.Save();
            }

            m_MutexDataGridView = new Mutex();
            m_MutexEventCount = new Mutex();
            m_MutexEventIndex = new Mutex();

            #region - [Size Definitions] -
            m_EventVariableControlSize = new VariableControlSize_t();
            m_EventVariableControlSize.Margin.Left = MarginLeftEventControl;
            m_EventVariableControlSize.Margin.Right = MarginRightEventControl;
            m_EventVariableControlSize.Margin.Top = MarginTopEventControl;
            m_EventVariableControlSize.Margin.Bottom = MarginBottomEventControl;
            m_EventVariableControlSize.WidthVariableNameField = WidthEventControlVariableNameField;
            m_EventVariableControlSize.WidthValueField = WidthEventControlValueField;
            m_EventVariableControlSize.WidthUnitsField = WidthEventControlUnitsField;
            m_EventVariableControlSize.Height = HeightEventControl;
            #endregion - [Size Definitions] -

            #region - [Function Keys] -
            // Escape - Exit
            // F1 - Help
            // F2 - Print
            // F3 - Save
            // F4 - Clear
            // F5 - Reset
            // F6 - Flags
            // F7 - History
            // F8 - Setup Stream
            DisplayFunctionKey(F3, Resources.FunctionKeyTextSave, Resources.FunctionKeyToolTipSaveEventLogs, Resources.Save);
            DisplayFunctionKey(F4, Resources.FunctionKeyTextClear, Resources.FunctionKeyToolTipClear, Resources.Delete);
            DisplayFunctionKey(F5, Resources.FunctionKeyTextInitialize, Resources.FunctionKeyToolTipInitialize, Resources.Initialize);
            DisplayFunctionKey(F6, Resources.FunctionKeyTextFlags, Resources.FunctionKeyToolTipFlags, Resources.ConfigureEventFlags);
            DisplayFunctionKey(F7, Resources.FunctionKeyTextHistory, Resources.FunctionKeyToolTipHistory, Resources.EventHistory);
            DisplayFunctionKey(F8, Resources.FunctionKeyTextSetupStream, Resources.FunctionKeyToolTipSetupStream, Resources.Modify);
            #endregion - [Function Keys] -

            // InformationLabel 1  - Date
            // InformationLabel 2  - Time
            // InformationLabel 3  - Event Count
            DisplayLabel(InformationLabel1, Resources.InformationLegendDate, Color.MintCream);
            DisplayLabel(InformationLabel2, Resources.InformationLegendTime, Color.LightSteelBlue);
            DisplayLabel(InformationLabel3, Resources.InformationLegendEventCount, Color.FromKnownColor(KnownColor.Info));

            #region - [ComboBox] -
            m_ToolStripComboBox1.Visible = true;
            m_ToolStripLegendComboBox1.Text = Resources.LegendCurrentEventLog;
            m_ToolStripLegendComboBox1.Visible = true;

            // Create an entry for each event log.
            for (int recordIndex = 0; recordIndex < Lookup.LogTable.RecordList.Count; recordIndex++)
            {
                // Check that a valid entry exists.
                if (Lookup.LogTable.Items[recordIndex] != null)
                {
                    m_ToolStripComboBox1.Items.Add(Lookup.LogTable.Items[recordIndex]);
                }
            }

            m_ToolStripComboBox1.SelectedIndex = InitialLogIndex;
            #endregion - [ComboBox] -

            #region - [Display Timer] -
            m_TimerDisplayUpdate = new System.Windows.Forms.Timer();
            m_TimerDisplayUpdate.Tick += new EventHandler(DisplayUpdate);
            m_TimerDisplayUpdate.Interval = IntervalDisplayUpdateMs;
            m_TimerDisplayUpdate.Enabled = true;
            m_TimerDisplayUpdate.Stop();
            #endregion - [Display Timer] -

            // Initialize the watchdog trip countdown.
            m_WatchdogTripCountdown = WatchdogTripCountdown;

            m_CommonEventVariableCount = Lookup.EventTable.CommonEventVariableCount;
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Cleanup(bool disposing)
        {
            try
            {
                ClearControls();

                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    if (m_TimerDisplayUpdate != null)
                    {
                        // Detach the event handler.
                        m_TimerDisplayUpdate.Tick -= new EventHandler(DisplayUpdate);
                        m_TimerDisplayUpdate.Dispose();
                    }

                    if (m_ThreadPollEvent != null)
                    {
                        m_ThreadPollEvent.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                if (m_EventRecordList != null)
                {
                    EventRecordList.Clear();
                }

                m_MutexDataGridView = null;
                m_MutexEventCount = null;
                m_MutexEventIndex = null;
                m_Log = null;
                m_TimerDisplayUpdate = null;
                m_ThreadPollEvent = null;
                m_CommunicationInterface = null;
                
                #region - [Detach the event handler methods.] -
                if (MainWindow != null)
                {
                    MainWindow.MenuUpdated -= new EventHandler(SecurityChanged);
                }

                this.m_MenuItemShowDefinition.Click -= new System.EventHandler(this.m_MenuItemShowDefinition_Click);
                this.m_MenuItemShowFaultLog.Click -= new System.EventHandler(this.m_MenuItemShowFaultLog_Click);
                this.m_DataGridViewEventLog.SelectionChanged -= new System.EventHandler(this.m_DataGridViewEventLog_SelectionChanged);
                this.m_DataGridViewEventLog.SortCompare -= new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.m_DataGridViewEventLog_SortCompare);
                this.m_DataGridViewEventLog.Sorted -= new System.EventHandler(this.m_DataGridViewEventLog_Sorted);
                this.Shown -= new System.EventHandler(this.FormViewEventLog_Shown);
                #endregion - [Detach the event handler methods.] -
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
            }
            finally
            {
                base.Cleanup(disposing);
            }
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        #region - [Form] -
        /// <summary>
        /// Event handler for the form <c>Shown</c> event. Set the size of the GroupBox controls.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormViewEventLog_Shown(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Update();

            // Ensure that an exception isn't thrown when a child form is opened in the Visual Studio development environment.
            if (MainWindow == null)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            #region - [Event Variables Panel] -
            m_PanelEventVariables.Width = m_EventVariableControlSize.Size.Width;

            m_LegendEventVariables.Location = new Point(m_EventVariableControlSize.Margin.Left, 0);
            m_LegendEventVariables.Size = new Size(m_EventVariableControlSize.Size.Width, m_EventVariableControlSize.Size.Height + m_EventVariableControlSize.Margin.Top
                                                   + m_EventVariableControlSize.Margin.Bottom);
            m_PanelEventVariableHeader.Padding = new Padding(0, 0, 0, m_EventVariableControlSize.Margin.Bottom);
            #endregion - [Event Variables Panel] -

            #region - [DataGridView Panel] -
            // Get the combined width of all visible DataGridView columns.
            int dataGridViewWidth = 0;
            DataGridViewColumn dataGridViewColumn;
            for (int columnIndex = 0; columnIndex < m_DataGridViewEventLog.Columns.Count; columnIndex++)
            {
                dataGridViewColumn = m_DataGridViewEventLog.Columns[columnIndex];
                if (dataGridViewColumn.Visible == true)
                {
                    dataGridViewWidth += dataGridViewColumn.Width;
                }
            }

            m_PanelDataGridViewEventLog.Width = dataGridViewWidth + MarginRightDataGridViewControl;
            #endregion - [DataGridView Panel] -

            // Only start polling and timer update if the communication interface has been specified.
            if (CommunicationInterface != null)
            {
                m_TimerDisplayUpdate.Start();
                StartPolling();
            }

            // As the MainWindow wasn't defined during the initial sort, the sort order won't have been written to the status message.
            if (EventRecordList.Count > 0)
            {
                MainWindow.WriteStatusMessage(string.Format(Resources.SMSortOrderDateTimeDescending));
            }
            else
            {
                MainWindow.WriteStatusMessage(Resources.SMEventListEmpty);
            }

            m_PanelEventVariables.Visible = true;

            // Update the form specific changes to the main menu options.
            SetMenuEnabled(CommonConstants.KeyMenuItemFileOpen, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemView, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemDiagnostics, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureWorksetsWatchWindow, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureWorksetsChartRecorder, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureRealTimeClock, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigurePasswordProtection, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureChartRecorder, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureChartMode, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemTools, false);

            // Register the event handler for the MainWindow.MenuUpdated event.
            MainWindow.MenuUpdated += new EventHandler(SecurityChanged);

            // Update the Enabled property of those function keys and menu options that are Security dependent by simulating a MenuUpdated event.
            SecurityChanged(this, new EventArgs());

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the form <c>ResizeEnd</c> event. Re-draw the <c>DataGridView</c> control.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void FormPTU_Resize(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            try
            {
                m_DataGridViewEventLog.ResumeLayout();
                m_DataGridViewEventLog.Refresh();
            }
            catch (Exception)
            {
                // Catch any exceptions that may be thrown when using the Visual Studio forms designer.
            }
        }
        #endregion - [Form] -

        #region - [Function Keys] -
        #region - [Escape] -
        /// <summary>
        /// Event handler for the escape key <c>Click</c> event. Close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void Escape_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (Escape.Enabled == false)
            {
                return;
            }

            Exit();
        }
         #endregion - [Escape] -

        #region - [F2-Print] -
        /// <summary>
        /// Event handler for the 'F2-Print' button <c>Click</c> event. Capture the window and save the image to the specified file.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F2_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F2.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F2.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            ScreenCaptureType = ScreenCaptureType.Event;
            base.F2_Click(sender, e);

            F2.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F2-Print] -

        #region - [F3-Save] -
        /// <summary>
        /// Event handler for the 'F3-Save' function key <c>Click</c> event. Saves the event records to disk in XML format.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F3_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F3.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F3.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                F3.Checked = false;
                Cursor = Cursors.Default;
                return;
            }
            StopPolling();

            Save();

            F3.Checked = false;
            Cursor = Cursors.Default;
            StartPolling();
        }
        #endregion - [F3-Save] -

        #region - [F4-Clear] -
        /// <summary>
        /// Event handler for the 'F4-Clear' button <c>Click</c> event. Clear all events/faults contained in the active event log. This also erases any data logs 
        /// associated with the event log.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F4_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F4.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F4.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                F4.Checked = false;
                Cursor = Cursors.Default;
                return;
            }
            StopPolling();

            // Create a delegate for the function that will clear the event logs.
            ZeroParameterDelegate clearEventLogs = new ZeroParameterDelegate(CommunicationInterface.ClearEvent);
            ClearOrInitialize(clearEventLogs);

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            F4.Checked = false;
            Cursor = Cursors.Default;
            StartPolling();
        }
        #endregion - [F4-Clear] -

        #region - [F5-Initialize] -
        /// <summary>
        /// Event handler for the 'F5-Initialize' button <c>Click</c> event. Clear all event information stored on battery backed RAM for both the maintenance and 
        /// engineering logs. This also clears both the cumulative history and recent history columns. This function is typically used to establish a zero event/fault 
        /// reference base when a replacement VCU is installed in a car.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F5_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F5.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F5.Checked = true;

            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                F5.Checked = false;
                Cursor = Cursors.Default;
                return;
            }
            StopPolling();

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // Create a delegate for the function that will initialize the event logs.
            ZeroParameterDelegate initializeEventEventLogs = new ZeroParameterDelegate(CommunicationInterface.InitializeEventLog);
            ClearOrInitialize(initializeEventEventLogs);

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            F5.Checked = false;
            Cursor = Cursors.Default;
            StartPolling();
        }
        #endregion - [F5-Initialize] -

        #region - [F6-Flags] -
        /// <summary>
        /// Event handler for the 'F6-Flags' button <c>Click</c> event. Show the dialog box which allows the user to configure the event flags associated with the 
        /// current event log. This allows the user to define which event types are enabled and which will trigger the recording of a fault log.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F6_Click(object sender, EventArgs e)
        {
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F6.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F6.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                Pause = false;
                return;
            }

            StopPolling();

            try
            {
                FormConfigureEventFlags formConfigureEventFlags = new FormConfigureEventFlags(MainWindow.CommunicationInterface, Log);
                formConfigureEventFlags.CalledFrom = this;
                formConfigureEventFlags.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Reset the communication port.
                CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                CommunicationInterface.InitCommunication(CommunicationInterface.CommunicationSetting);
            }
            finally
            {
                F6.Checked = false;
                Cursor = Cursors.Default;
                StartPolling();
            }
        }
        #endregion - [F6-Flags] -

        #region - [F7-History] -
        /// <summary>
        /// Event handler for the 'F7-History' button <c>Click</c> event. Show the dialog box which displays the event history.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F7_Click(object sender, EventArgs e)
        {
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F7.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F7.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                F7.Checked = false;
                Cursor = Cursors.Default;
                return;
            }
            StopPolling();

            try
            {
                FormShowEventHistory formShowEventHistory = new FormShowEventHistory(MainWindow.CommunicationInterface, Log);
                formShowEventHistory.CalledFrom = this;
                formShowEventHistory.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Reset the communication port.
                CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                CommunicationInterface.InitCommunication(CommunicationInterface.CommunicationSetting);
            }
            finally
            {
                F7.Checked = false;
                Cursor = Cursors.Default;
                StartPolling();
            }
        }
        #endregion - [F7-History] -

        #region - [F8-Setup Stream] -
        /// <summary>
        /// Event handler for the 'F8-Setup Stream' button <c>Click</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F8_Click(object sender, EventArgs e)
        {
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F8.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F8.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                F8.Checked = false;
                Cursor = Cursors.Default;
                return;
            }
            StopPolling();

            try
            {
                FormConfigureFaultLogParameters formConfigureFaultLogParameters = new FormConfigureFaultLogParameters(MainWindow.CommunicationInterface,
                                                                                                                      Workset.FaultLog, m_Log);
                formConfigureFaultLogParameters.CalledFrom = this;
                formConfigureFaultLogParameters.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Reset the communication port.
                CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                CommunicationInterface.InitCommunication(CommunicationInterface.CommunicationSetting);
            }
            finally
            {
                F8.Checked = false;
                Cursor = Cursors.Default;
                StartPolling();
            }
        }
        #endregion - [F8-Setup Stream] -
        #endregion - [Function Keys] -

        #region - [ComboBox] -
        /// <summary>
        /// Event handler for the <c>ToolStripComboBox</c> control <c>SelectedIndexChanged</c> event. Download and display the selected event log.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                return;
            }

            Cursor = Cursors.WaitCursor;
            ClearControls();
            ClearEventMemory();
            InformationLabel3.Text = string.Empty;

            // Keep a record of the selected log.
            m_Log = (Log)m_ToolStripComboBox1.SelectedItem;

            // Update the tab with the name of the event log.
            m_TabPage1.Text = m_Log.Description;

            bool communicationException;
            EventRecordList = GetEventLog(m_Log, out communicationException);
            if (communicationException == true)
            {
                MessageBox.Show(Resources.MBTEventLogDownloadFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Reset the communication port.
                CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                CommunicationInterface.InitCommunication(CommunicationInterface.CommunicationSetting);
            }
            else
            {
                if (EventRecordList.Count == 0)
                {
                    // This method may be called indirectly from the constructor before the MainWindow reference has been defined therefore check that
                    // the MainWindow  reference has been defined before calling the MainWindow.WriteStatusMessage() method.
                    if (MainWindow != null)
                    {
                        MainWindow.WriteStatusMessage(Resources.SMEventListEmpty);
                    }

                    // If the event count information label is visible, display the error count.
                    InformationLabel3.Text = EventRecordList.Count.ToString();
                }
                else
                {
                    AddList(EventRecordList);
                    m_DataGridViewEventLog.Sort(m_DataGridViewEventLog.Columns[ColumnIndexDate], ListSortDirection.Descending);

                    // Simulate a SelectionChanged event in order to display the event variables associated with row 1 of the selected log.
                    m_DataGridViewEventLog_SelectionChanged(m_DataGridViewEventLog, new EventArgs());
                }
            }

            m_DataGridViewEventLog.Focus();
            Pause = false;
            Cursor = Cursors.Default;
        }
        #endregion - [ComboBox] -

        #region - [Context Menu] -
        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Show Fault Log' context menu. Displays the fault log associated with the selected event if 
        /// it is available.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_MenuItemShowFaultLog_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Get the selected row.
            m_MutexDataGridView.WaitOne(DefaultMutexWaitDurationMs, false);
            DataGridViewRow dataGridViewRow = m_DataGridViewEventLog.SelectedRows[0];
            m_MutexDataGridView.ReleaseMutex();

            if (dataGridViewRow != null)
            {
                m_DataGridViewEventLog_CellContentDoubleClick(m_DataGridViewEventLog, new DataGridViewCellEventArgs(ColumnIndexStreamAvailableImage,
                                                              dataGridViewRow.Index));
            }
        }

        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Show Definition' context menu. Show the help information associated with the event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemShowDefinition_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Get the selected row.
            Cursor = Cursors.WaitCursor;
            m_MutexDataGridView.WaitOne(DefaultMutexWaitDurationMs, false);
            DataGridViewRow dataGridViewRow = m_DataGridViewEventLog.SelectedRows[0];
            m_MutexDataGridView.ReleaseMutex();

            if (dataGridViewRow != null)
            {
                EventRecord selectedEventRecord = FindEventRecord(dataGridViewRow, EventRecordList);
                if (selectedEventRecord == null)
                {
                    Cursor = Cursors.Default;
                    return;
                }

                if (selectedEventRecord.HelpIndex != CommonConstants.NotDefined)
                {
                    WinHlp32.ShowPopup(this.Handle.ToInt32(), selectedEventRecord.HelpIndex);
                }
            }

            Cursor = Cursors.Default;
        }
        #endregion - [Context Menu] -

        #region - [Timer Events] -
        /// <summary>
        /// Called periodically by the System.Windows.Forms.Timer event. Update the event display. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void DisplayUpdate(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Update the local variables with the appropriate property values of the thread that is responsible for VCU communications.
            int watchdog;
            bool communicationFault;
            long packetCount;
            if (m_ThreadPollEvent != null)
            {
                watchdog = m_ThreadPollEvent.Watchdog;
                communicationFault = m_ThreadPollEvent.CommunicationFault;
                packetCount = m_ThreadPollEvent.PacketCount;
            }
            else
            {
                // Skip, if the thread has not been instantiated.
                return;
            }

            #region - [Port Locked] -
            // ------------------------------------------
            // Check if the communication port is locked.
            // ------------------------------------------
            bool watchdogTrip = false;
            if (watchdog == m_Watchdog)
            {
                // Don't assert the watchdog trip flag until the countdown has elapsed. 
                if (m_WatchdogTripCountdown <= 0)
                {
                    watchdogTrip = true;
                }
                else
                {
                    m_WatchdogTripCountdown--;
                }
            }
            else
            {
                m_WatchdogTripCountdown = WatchdogTripCountdown;
                m_Watchdog = watchdog;
            }

            // Only update on transitions of the flag.
            if (watchdogTrip != m_WatchdogTrip)
            {
                if (watchdogTrip == true)
                {
                    // Disable the display until the fault has been cleared.
                    MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultPortLocked, Color.Red, Color.Black);
                    WatchControl.InvalidValue = true;
                }
                else
                {
                    // Restore the display.
                    MainWindow.WriteStatusMessage(string.Empty);
                    WatchControl.InvalidValue = false;
                }
                m_WatchdogTrip = watchdogTrip;
            }
            #endregion - [Port Locked] -

            #region - [ReadTimeout] -
            if (communicationFault != m_CommunicationFault)
            {
                if (communicationFault == true)
                {
                    // Disable the display until the fault has been cleared.
                    MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                    WatchControl.InvalidValue = true;
                }
                else
                {
                    // Restore the display.
                    MainWindow.WriteStatusMessage(string.Empty);
                    WatchControl.InvalidValue = false;
                }
                m_CommunicationFault = communicationFault;
            }
            #endregion - [ReadTimeout] -

            // Update the status information.
            // Label 1 - Date.
            InformationLabel1.Text = DateTime.Now.ToShortDateString();

            // Label 2 - Time.
            InformationLabel2.Text = DateTime.Now.ToString(CommonConstants.FormatStringTimeSec);

            // Blink the icon to show that event table is being updated.
            if (packetCount != m_PacketCount)
            {
                MainWindow.BlinkUpdateIcon();
                m_PacketCount = packetCount;
            }
        }
        #endregion - [Timer Events] -

        #region - [DataGridView] -
        /// <summary>
        /// <para>Event handler for the <c>DataGridView</c> control <c>SortCompare</c> event.</para>
        /// <para>As part of the overall sort process, this event handler is called whenever a comparison is made between successive rows of the <c>DataGridView</c>
        /// control. The final sort order is determined by: (a) the value passed to the SortResult property of the <c>DataGridViewSortCompareEventArgs</c> when the 
        /// two rows, e.RowIndex1 and e.RowIndex2, are compared, and (b) the <c>ListSortDirection</c> property.</para>
        /// <para>The sort direction of the CarIdentifier, LogName and EventName fields is in the opposite direction to that of the DateTime field and the EventIndex
        /// field; the sort precedence is shown below, where [] is used to identify the key sort field.</para>
        /// <para>[DateTime], EventIndex, CarIdentifier, LogName, EventName.</para>
        /// <para>[CarIdentifier], DateTime, EventIndex, LogName, EventName.</para>
        /// <para>[LogName], DateTime, EventIndex, CarIdentifier, EventName.</para>
        /// <para>[EventName], DateTime, EventIndex, CarId, LogName.</para>
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_DataGridViewEventLog_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Determine the key sort field i.e. the field defined by the [] in the sort criteria.
            switch(e.Column.Index)
            {
                case ColumnIndexDate:
                    // Sort the rows based upon the the date/time field.
                    e.SortResult = String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexDateTime].Value.ToString(),
                                                  m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexDateTime].Value.ToString());

                    // It is possible for a number of events to occur within the same 1 second time frame, therefore, sort by event index to determine
                    // the order in which the events occurred. The most recent event will have the higher event index.
                    if (e.SortResult == 0)
                    {
                        // Sort the rows based upon event index, in the same direction as the date/time field.
                        e.SortResult = String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexEventIndex].Value.ToString(),
                                                      m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexEventIndex].Value.ToString());

                        // ---------------------------------------------------------------------------------------------------------------------
                        // As the sort direction for the car identifier, log name and event name fields is specified to be in the opposite direction to that of the
                        // date/time field, the row order is swapped when making the comparison for these fields.
                        // ---------------------------------------------------------------------------------------------------------------------

                        // Although highly improbable, if the date/time and event index values are equal for both cells, sort the rows based upon the car identifier
                        // field.
                        if (e.SortResult == 0)
                        {
                            e.SortResult = String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexCarIdentifier].Value.ToString(),
                                                          m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexCarIdentifier].Value.ToString());

                            // If the car identifier values are equal, either sort the rows based upon the log name, if this column is visible;
                            // otherwise, sort the rows based upon the event name field.
                            if (e.SortResult == 0)
                            {
                                // Check whether the log name field is visible.
                                if (m_DataGridViewEventLog.Columns[ColumnIndexLog].Visible == true)
                                {
                                    e.SortResult = String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexLog].Value.ToString(),
                                                                  m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexLog].Value.ToString());

                                    // If the log name values are equal, sort by the event name.
                                    if (e.SortResult == 0)
                                    {
                                        e.SortResult = String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexEventName].Value.ToString(),
                                                                      m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexEventName].Value.ToString());
                                    }

                                }
                                else
                                {
                                    e.SortResult = String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexEventName].Value.ToString(),
                                                                  m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexEventName].Value.ToString());
                                }
                            }
                        }
                    }
                    e.Handled = true;
                    break;

                case ColumnIndexCarIdentifier:
                    // Check whether the LogName column of the DataGridView control is visible.
                    if (m_DataGridViewEventLog.Columns[ColumnIndexLog].Visible == true)
                    {
                        SortBy(e, ColumnIndexCarIdentifier, ColumnIndexLog, ColumnIndexEventName);
                    }
                    else
                    {
                        SortBy(e, ColumnIndexCarIdentifier, ColumnIndexEventName);
                    }
                    break;

                case ColumnIndexLog:
                    // The LogName column of the DataGridView control must be visible for the user to be allowed to select this sort field.
                    SortBy(e, ColumnIndexLog, ColumnIndexCarIdentifier, ColumnIndexEventName);
                    break;

                case ColumnIndexEventName:
                    // Check whether the LogName column of the DataGridView control is visible.
                    if (m_DataGridViewEventLog.Columns[ColumnIndexLog].Visible == true)
                    {
                        SortBy(e, ColumnIndexEventName, ColumnIndexCarIdentifier, ColumnIndexLog);
                    }
                    else
                    {
                        SortBy(e, ColumnIndexEventName, ColumnIndexCarIdentifier);
                    }
                    break;

                default:
                    // Sort the rows based upon the cells in the current column.
                    e.SortResult = String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
                    break;
            }
        }

        /// <summary>
        /// Event handler for the <c>DataGridView</c> control <c>Sorted</c> event. Write the current sort order to the status message display.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_DataGridViewEventLog_Sorted(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            string statusMessage;
            switch (m_DataGridViewEventLog.SortedColumn.Index)
            {
                case ColumnIndexDate:
                    if (m_DataGridViewEventLog.SortOrder == SortOrder.Descending)
                    {
                        statusMessage = Resources.SMSortOrderDateTimeDescending;
                    }
                    else
                    {
                        statusMessage = Resources.SMSortOrderDateTimeAscending;
                    }
                    break;
                default:
                    if (m_DataGridViewEventLog.SortOrder == SortOrder.Descending)
                    {
                        statusMessage = string.Format(Resources.SMSortOrdedDescending, m_DataGridViewEventLog.SortedColumn.HeaderText);
                    }
                    else
                    {
                        statusMessage = string.Format(Resources.SMSortOrdedAscending, m_DataGridViewEventLog.SortedColumn.HeaderText);
                    }
                    break;
            }

            // This method may be called indirectly from the constructor before the MainWindow reference has been defined therefore check that the MainWindow 
            // reference has been defined before calling the MainWindow.WriteStatusMessage() method.
            if (MainWindow != null)
            {
                MainWindow.WriteStatusMessage(statusMessage);
            }
        }

        /// <summary>
        /// Event handler fo the <c>SelectionChanged</c> event associated with the <c>DataGridView</c> control. Display the event variables associated with the selected 
        /// event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_DataGridViewEventLog_SelectionChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            CloseShowFlags();

            m_MutexDataGridView.WaitOne(DefaultMutexWaitDurationMs, false);
            DataGridView dataGridView = (sender as DataGridView);
            Debug.Assert(dataGridView != null);

            // Check that at least one row has been selected.
            if (dataGridView.SelectedRows.Count <= 0)
            {
                m_MutexDataGridView.ReleaseMutex();
                return;
            }

            DataGridViewRow dataGridViewRow = dataGridView.SelectedRows[0];
            m_MutexDataGridView.ReleaseMutex();

            EventRecord selectedEventRecord = FindEventRecord(m_DataGridViewEventLog.SelectedRows[0], EventRecordList);
            if (selectedEventRecord == null)
            {
                Cursor = Cursors.Default;
                return;
            }

            DisplayEventVariableList(selectedEventRecord);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the <c>CellContentDoubleClick</c> event associated with the event log <c>DataGridView</c> control. Depending upon which column has been 
        /// selected either: (1) retrieve and display the fault log data stream associated with the selected event or (2) show the event definition.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_DataGridViewEventLog_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_MutexDataGridView.WaitOne(DefaultMutexWaitDurationMs, false);
            DataGridView dataGridView = (sender as DataGridView);
            Debug.Assert(dataGridView != null);

            // Check that at least one row has been selected.
            if (dataGridView.SelectedRows.Count <= 0)
            {
                m_MutexDataGridView.ReleaseMutex();
                return;
            }

            DataGridViewRow dataGridViewRow = dataGridView.SelectedRows[0];
            m_MutexDataGridView.ReleaseMutex();

            EventRecord selectedEventRecord = FindEventRecord(dataGridViewRow, EventRecordList);
            if (selectedEventRecord == null)
            {
                Cursor = Cursors.Default;
                return;
            }

            // Check which column has been selected.
            if (e.ColumnIndex == ColumnIndexStreamAvailableImage)
            {
                ShowFaultLog(selectedEventRecord);
            }
            else if (e.ColumnIndex == ColumnIndexEventName)
            {
                if (selectedEventRecord.HelpIndex != CommonConstants.NotDefined)
                {
                    WinHlp32.ShowPopup(this.Handle.ToInt32(), selectedEventRecord.HelpIndex);
                }
            }

            Cursor = Cursors.Default;
            return;
        }
        #endregion - [DataGridView] -

        /// <summary>
        /// Event handler for the <c>MainWindow.MenuUpdated</c> event. Updates the Enabled property of those function keys that are Security dependent.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void SecurityChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Ensure that the MainWindow property has been initialized.
            if (MainWindow == null)
            {
                return;
            }

            // Enable all Function Keys that are Security dependent.
            F4.Enabled = true;
            F5.Enabled = true;

            // Disable any function keys that are not appropriate to the current security level.
            if ((short)Security.SecurityLevelCurrent > (short)Security.SecurityLevelHighest)
            {
                // ----------------
                // System Developer
                // ----------------

                // Leave the Enabled properties of all function keys asserted.
            }
            else if ((short)Security.SecurityLevelCurrent == (short)Security.SecurityLevelHighest)
            {
                // ------------------------------------------------
                // Highest Security Level Appropriate to the Client
                // ------------------------------------------------

                // Clear the Enabled properties of those function keys that are not applicable to client engineers.
            }
            else if ((short)Security.SecurityLevelCurrent >= (short)Security.SecurityLevelBase)
            {
                // -----------
                // Maintenance
                // -----------

                // Clear the Enabled properties of those function keys that are not applicable at the base security level.
                F4.Enabled = false;
                F5.Enabled = false;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Security.SecurityLevelCurrent", "FormViewEventLog.Ctor()");
            }
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Close the form cleanly. Simulates the user pressing the Exit button.
        /// </summary>
        public override void Exit()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            Escape.Checked = true;

            // It is possible that this method can be indirectly called by the constructor, therefore check that the MainWindow is defined before attempting to 
            // write a status message.
            if (MainWindow != null)
            {
                MainWindow.WriteStatusMessage(string.Empty);
            }

            if (m_TimerDisplayUpdate != null)
            {
                m_TimerDisplayUpdate.Stop();
            }

            this.SuspendLayout();
            ClearControls();

            try
            {
                // Only call the following methods if the communication interface has been defined.
                if (CommunicationInterface != null)
                {
                    try
                    {
                        SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback);
                        StopPolling();
                        ClearEventMemory();
                        CommunicationInterface.ExitEventLog();

                        // If there are problems with the communication link, set the PTU to configuration mode and close the communication port.
                        if (m_CommunicationFault == true || m_WatchdogTrip == true)
                        {
                            CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                            MainWindow.SetMode(Mode.Configuration);
                        }
                    }
                    catch (CommunicationException)
                    {
                        // Ensure that an exception is not thrown if the call fails.
                    }
                }
            }
            catch (Exception)
            {
                // Ensure that an exception isn't thrown if any of the the calls fail.
            }
            finally
            {
                m_TabControl.TabPages.Clear();

                // Write the car identifier associated with the data stream.
                if (MainWindow != null)
                {
                    MainWindow.WriteCarIdentifier(FileHeader.HeaderCurrent.TargetConfiguration.CarIdentifier);
                }
                this.PerformLayout();

                Escape.Checked = false;
                base.Exit();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Retrieve the specified event log from the VCU: (1) Change the event log to the specified log, (2) load the event log into memory, (3) retrieve each
        /// event record from the log and add the event to the list (4) sort the event record list in descending date/time order i.e. most recent event first.
        /// </summary>
        /// <remarks>This method does not retrieve the event variables associated with each event.</remarks>
        /// <param name="log">The selected event log.</param>
        /// <returns>The list of event records contained within the specified event log.</returns>
        internal List<EventRecord> GetEventLog(Log log)
        {
            List<EventRecord> eventRecordList = new List<EventRecord>();
            bool communicationFault;
            return GetEventLog(log, out communicationFault);
        }

        /// <summary>
        /// Retrieve the specified event log from the VCU: (1) Change the event log to the specified log, (2) load the event log into memory, (3) retrieve each
        /// event record from the log and add the event to the list (4) sort the event record list in descending date/time order i.e. most recent event first.
        /// </summary>
        /// <remarks>This method does not retrieve the event variables associated with each event.</remarks>
        /// <param name="log">The selected event log.</param>
        /// <param name="communicationException">A flag to indicate whether a communication exception was thrown. True, if a communication exception was thrown;
        /// otherwise, false. </param>
        /// <returns>The list of event records contained within the specified event log.</returns>
        internal List<EventRecord> GetEventLog(Log log, out bool communicationException)
        {
            List<EventRecord> eventRecordList = new List<EventRecord>();
            communicationException = false;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return eventRecordList;
            }

            // This method may be called indirectly from the constructor before the MainWindow reference has been defined therefore check that the MainWindow 
            // reference has been defined before calling the MainWindow.WriteStatusMessage() method.
            if (MainWindow != null)
            {
                MainWindow.WriteStatusMessage(string.Format(Resources.SMEventLogRetrieve, log.Description));
            }

            uint oldIndex;
            try
            {
                CommunicationInterface.ChangeEventLog(log);
                CommunicationInterface.LoadEventLog(out m_EventCount, out oldIndex, out m_EventIndex);
            }
            catch (CommunicationException)
            {
                communicationException = true;
                return eventRecordList;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            // Load the retrieved event records into the event record list.
            Cursor = Cursors.WaitCursor;

            EventRecord eventRecord; 
            for (short eventIndex = 0; eventIndex < m_EventCount; eventIndex++)
            {
                try
                {
                    // No need for a Mutex as the GetEventRecord() method does not communicate with the VCU it merely accesses the fault descriptor table stored in
                    // memory.
                    CommunicationInterface.GetEventRecord(log, eventIndex, out eventRecord);

                    // If the event record cannot be found, continue.
                    if (eventRecord == null)
                    {
                        continue;
                    }

                    eventRecord.CarIdentifier = FileHeader.HeaderCurrent.TargetConfiguration.CarIdentifier;
                }
                catch (CommunicationException)
                {
                    continue;
                }

                // Store the record retrieved from the VCU.
                eventRecordList.Add(eventRecord);
            }

            // Sort the list of events, most recent event first. This ensures that the first row of the DataGridView is selected.
            eventRecordList.Sort(CompareByDateTimeDescending);

            return eventRecordList;
        }

        /// <summary>
        /// Add the specified records to the <c>DataGridView</c> control. If the specified list does not contain any records no action will be taken.
        /// </summary>
        /// <remarks>De-registers the <c>SelectionChanged</c> event handler while the rows are being added.</remarks>
        internal void AddList(List<EventRecord> eventRecordList)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (eventRecordList.Count <= 0)
            {
                return;
            }

            // Enable the Save and Clear function keys.
            F3.Enabled = true;
            F4.Enabled = true;
            F5.Enabled = true;

            // Ensure the the changes to the F4 anf F5 Enabled property is appropriate to the current security level.
            SecurityChanged(this, new EventArgs());

            m_MutexDataGridView.WaitOne(DefaultMutexWaitDurationMs, false);

            // Ensure that the SelectionChanged event is not triggered as a result of updating the rows of the DataGridView control.
            m_DataGridViewEventLog.SelectionChanged -= new EventHandler(m_DataGridViewEventLog_SelectionChanged);
            m_DataGridViewEventLog.SuspendLayout();

            // Add the specified list of event records to the DataGridView control.
            EventRecord eventRecord;
            for (short eventIndex = 0; eventIndex < eventRecordList.Count; eventIndex++)
            {
                eventRecord = eventRecordList[eventIndex];

                DataGridViewRow dataGridViewRow = Convert(eventRecord);

                // Add the new row to the DataGridView control.
                m_DataGridViewEventLog.Rows.Add(dataGridViewRow);
            }

            // If the sorted column hasn't yet been defined default to: sorted by index in descending order i.e. most recent  first.
            if (m_DataGridViewEventLog.SortedColumn == null)
            {
                m_DataGridViewEventLog.Sort(m_DataGridViewEventLog.Columns[0], System.ComponentModel.ListSortDirection.Descending);
            }
            else
            {
                System.ComponentModel.ListSortDirection listSortDirection;
                if (m_DataGridViewEventLog.SortOrder == SortOrder.Ascending)
                {
                    listSortDirection = System.ComponentModel.ListSortDirection.Ascending;
                }
                else
                {
                    listSortDirection = System.ComponentModel.ListSortDirection.Descending;
                }

                m_DataGridViewEventLog.Sort(m_DataGridViewEventLog.SortedColumn, listSortDirection);
            }

            m_DataGridViewEventLog.PerformLayout();
            m_DataGridViewEventLog.Update();

            // Check whether the most recent downloaded event is newer that the most recent saved event.
            int compareResult;
            try
            {
                compareResult = eventRecordList[0].DateTime.CompareTo(Settings.Default.MostRecentDownloadedEventsSaved.DownloadedEvents[MainWindow.CarNumber].DateTime);
            }
            catch (Exception)
            {
                compareResult = 1;
            }

            if (compareResult == 1)
            {
                // Yes, the most recent event is newer than the most recent saved event, update the log saved status.
                m_EventLogSavedStatus = EventLogSavedStatus.Unsaved;
            }
            else if (compareResult == 0)
            {
                // The most recent event for the car is the same as the one saved in the Settings file, update the log saved status.
                m_EventLogSavedStatus = EventLogSavedStatus.Saved;
            }

            // Update the LogStatus status label.
            switch (m_EventLogSavedStatus)
            {
                case EventLogSavedStatus.Unsaved:
                case EventLogSavedStatus.Saved:
                    MainWindow.LogStatus = m_EventLogSavedStatus;
                    break;
                default:
                    MainWindow.LogStatus = EventLogSavedStatus.Unknown;
                    break;
            }

            // Re-register for the SelectionChanged event.
            m_DataGridViewEventLog.SelectionChanged += new EventHandler(m_DataGridViewEventLog_SelectionChanged);

            // If the event count information label is visible, display the error count.
            InformationLabel3.Text = m_DataGridViewEventLog.Rows.Count.ToString();

            m_MutexDataGridView.ReleaseMutex();
        }

        /// <summary>
        /// Convert the specified event record to a <c>DataGridViewRow</c> so that it may be added to the <c>DataGridView</c> control used to display the events.
        /// </summary>
        /// <param name="eventRecord">The event record that is to be converted.</param>
        /// <returns>The specified event record converted to a <c>DataGridViewRow</c>.</returns>
        protected DataGridViewRow Convert(EventRecord eventRecord)
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return dataGridViewRow;
            }

            // Get the log description;
            string logName;
            try
            {
                logName = Lookup.LogTable.Items[eventRecord.LogIdentifier].Description;
            }
            catch (Exception)
            {
                logName = string.Empty;
            }

            // Load the appropriate stream-saved graphic.
            object streamAvailableImage;
            string streamAvailableText;
            if (eventRecord.StreamSaved == true)
            {
                streamAvailableImage = (object)Resources.DataStream;
                streamAvailableText = True;
            }
            else
            {
                streamAvailableImage = (object)Resources.Blank;
                streamAvailableText = False;
            }

            string dayOfWeek = eventRecord.DateTime.DayOfWeek.ToString().Substring(0, LengthDayOfWeekString);

            dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.CreateCells(m_DataGridViewEventLog, new object[]   {
                                                                                (object)eventRecord.EventIndex.ToString("0000"),
                                                                                (object)eventRecord.Identifier.ToString("0000"),
                                                                                (object)eventRecord.CarIdentifier,
                                                                                (object)logName,
                                                                                (object)eventRecord.Description,
                                                                                (object)eventRecord.DateTime.ToString(FormatStringDateTime),
                                                                                (object)eventRecord.DateTime.ToShortDateString(),
                                                                                (object)eventRecord.DateTime.ToString(FormatStringEventTime),
                                                                                (object)dayOfWeek,
                                                                                streamAvailableText,
                                                                                streamAvailableImage
                                                                               });
            return dataGridViewRow;
        }

        /// <summary>
        /// If a fault log data stream has been saved for the selected event record, retrieve the fault log from the VCU and call the form used to plot the fault log.
        /// </summary>
        /// <param name="eventRecord">The selected event record.</param>
        protected virtual void ShowFaultLog(EventRecord eventRecord)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if a data stream hasn't been saved for the selected record.
            if ((eventRecord == null) || (eventRecord.StreamSaved == false))
            {
                MessageBox.Show(Resources.MBTFaultLogNotAvailable, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ---------------------------------------------------------------------------
            // Retrieve the data stream corresponding to the selected record from the VCU.
            // ---------------------------------------------------------------------------
            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                return;
            }
            StopPolling();

            try
            {
                FormGetStream formGetStream = new FormGetStream(CommunicationInterface, eventRecord);
                formGetStream.CalledFrom = this;
                formGetStream.CancelEnable = false;

                // The FormGetStream class will copy the downloaded data stream to the DataStream property of this class.
                DialogResult dialogResult = formGetStream.ShowDialog();
                if (dialogResult == DialogResult.Abort)
                {
                    // This method may be called indirectly from the constructor before the MainWindow reference has been defined therefore check that the MainWindow 
                    // reference has been defined before calling the MainWindow.WriteStatusMessage() method.
                    MessageBox.Show(string.Format(Resources.MBTGetStreamFailed, eventRecord.Description), Resources.MBCaptionError, MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);

                    // Reset the communication port.
                    CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                    CommunicationInterface.InitCommunication(CommunicationInterface.CommunicationSetting);
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                MainWindow.WriteStatusMessage(string.Empty);
                StartPolling();
            }

            // -----------------------------
            // Plot the retrieved fault log.
            // -----------------------------
            WatchFile_t watchFile = new WatchFile_t(FileHeader.HeaderCurrent, DataStreamCurrent);
            watchFile.Filename = eventRecord.Description + CommonConstants.BindingMessage + eventRecord.DateTime.ToLongTimeString();
            try
            {
                FormViewFaultLog formViewFaultLog = new FormViewFaultLog(watchFile);
                formViewFaultLog.MdiParent = MdiParent;

                // The CalledFrom property is used to allow the called form to reference back to this form.
                formViewFaultLog.CalledFrom = this;
                formViewFaultLog.Show();
                Hide();
            }
            catch (Exception exception)
            {
                MessageBox.Show(Resources.MBTShowFaultLogFailed + CommonConstants.NewLine + CommonConstants.NewLine + exception.Message, Resources.MBCaptionError,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return;
        }

        #region - [EventVariables] -
        /// <summary>
        /// Configure the specified event variable user controls. The individual controls are laid out on the panel similar to rows on a DataGridView control. The 
        /// first entry in the list, eventVariableList[0], is positioned at row 0, the second at row 1 etc. To configure all event variables defined in the list specify 
        /// a start index of zero, however, to configure only the event specific event variables specify a start index corresponding to index of the event variable 
        /// list where the specific event variables start. Note, the user controls correponding to the event variables that are specific to the event will be 
        /// positioned starting at the row corresponding to the start index value.
        /// </summary>
        /// <param name="panel">The panel to which the event controls are to be added.</param>
        /// <param name="eventControlSize">The structure that is used to define the size of each event variable user control.</param>
        /// <param name="startIndex">The start index in the list of event variables associated with the first event variable that is to be displayed.</param>
        /// <param name="eventVariableList">A list of the event variables that are to be displayed.</param>
        protected void ConfigureControls(Panel panel, VariableControlSize_t eventControlSize, short startIndex, List<EventVariable> eventVariableList)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Work out the spacing between consecutive rows.
            int rowSpacing = eventControlSize.Size.Height + eventControlSize.Margin.Vertical;

            EventControl eventVariableControl;
            short watchIdentifier;
            for (short index = startIndex; index < eventVariableList.Count; index++)
            {
                switch (eventVariableList[index].VariableType)
                {
                    case VariableType.Scalar:
                        eventVariableControl = new EventScalarControl();
                        break;
                    case VariableType.Enumerator:
                        eventVariableControl = new EventEnumeratorControl();
                        break;
                    case VariableType.Bitmask:
                        eventVariableControl = new EventBitmaskControl();
                        break;
                    default:
                        eventVariableControl = new EventControl();
                        break;
                }

                watchIdentifier = eventVariableList[index].Identifier;
                
                eventVariableControl.WidthVariableNameField = eventControlSize.WidthVariableNameField;
                eventVariableControl.WidthValueField = eventControlSize.WidthValueField;
                eventVariableControl.WidthUnitsField = eventControlSize.WidthUnitsField;

                eventVariableControl.ClientForm = this;
                eventVariableControl.TabIndex = 0;

                eventVariableControl.Location = new System.Drawing.Point(eventControlSize.Margin.Left, index * rowSpacing);

                eventVariableControl.ForeColorValueFieldZero = Color.ForestGreen;
                eventVariableControl.ForeColorValueFieldNonZero = Color.ForestGreen;

                eventVariableControl.Identifier = watchIdentifier;
                eventVariableControl.Value = 0;

                panel.Controls.Add(eventVariableControl);
            }
        }

        /// <summary>
        /// Display the event variable data corresponding to the selected event record.
        /// </summary>
        /// <param name="selectedEventRecord">The event record containing the event variables that are to be displayed.</param>
        protected void DisplayEventVariableList(EventRecord selectedEventRecord)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // -------------------------------------------
            // Configure the event variable user controls.
            // -------------------------------------------
            // Check the number of event variable user controls that have been added to the panel.
            if (m_PanelEventVariableList.Controls.Count == 0)
            {
                // None, configure user controls for each event variable associated with the selected event.
                ConfigureControls(m_PanelEventVariableList, m_EventVariableControlSize, 0, selectedEventRecord.EventVariableList);
            }
            else if (m_PanelEventVariableList.Controls.Count >= m_CommonEventVariableCount)
            {
                int eventVariableListCount = m_PanelEventVariableList.Controls.Count;

                // Delete the user controls associated with the event specific event variables.
                for (int index = m_CommonEventVariableCount; index < eventVariableListCount; index++)
                {
                    m_PanelEventVariableList.Controls.RemoveAt(m_CommonEventVariableCount);
                }

                // Configure the user controls associated with the event specific event variables.
                ConfigureControls(m_PanelEventVariableList, m_EventVariableControlSize, (short)m_CommonEventVariableCount, selectedEventRecord.EventVariableList);
            }
            
            // --------------------------------------------------------------------------------------------------
            // Display the engineering values using the pre-configured user controls.
            // --------------------------------------------------------------------------------------------------
            for (int index = 0; index < selectedEventRecord.EventVariableList.Count; index++)
            {
                switch (selectedEventRecord.EventVariableList[index].VariableType)
                {
                    case VariableType.Scalar:
                        (m_PanelEventVariableList.Controls[index] as EventScalarControl).Value = selectedEventRecord.EventVariableList[index].ValueFromTarget;
                        break;
                    case VariableType.Enumerator:
                        (m_PanelEventVariableList.Controls[index] as EventEnumeratorControl).Value = selectedEventRecord.EventVariableList[index].ValueFromTarget;
                        break;
                    case VariableType.Bitmask:
                        (m_PanelEventVariableList.Controls[index] as EventBitmaskControl).Value = selectedEventRecord.EventVariableList[index].ValueFromTarget;
                        break;
                    default:
                        throw new ArgumentException(Resources.EMEventVariableTypeInvalid, "selectedEventRecord.EventVariableList[index].VariableType");
                }
            }
        }

        /// <summary>
        /// Find the event record from the specified <c>List</c> that matches, exactly, the specified row of the <c>DataGridView</c>. If no match is found, a null
        /// value is returned.
        /// </summary>
        /// <param name="dataGridViewRow">The <c>DataGridViewRow</c> that is to be matched.</param>
        /// <param name="eventRecordList">The list of event records.</param>
        /// <returns>The event record that matches the specified row of the <c>DataGridView, if a match is found; otherwise, null.</c>.</returns>
        private EventRecord FindEventRecord(DataGridViewRow dataGridViewRow, List<EventRecord> eventRecordList)
        {
            // Prior to Rev. 1.16, the Find was implemented by using the event index associated with the selected row to find the record, as this was unique.
            // Following bug fix SNCR - R188 PTU [20 Mar 2015] Item 11, the event index field is no longer unique, therefore a search must be carried out to
            // match the following fields of DataGridViewRow:
            string  dateTime        = (string)dataGridViewRow.Cells[ColumnIndexDateTime].Value;
            short   eventIndex      = short.Parse((string)dataGridViewRow.Cells[ColumnIndexEventIndex].Value);
            string  eventName       = (string)dataGridViewRow.Cells[ColumnIndexEventName].Value;
            string  logIdentifier   = (string)dataGridViewRow.Cells[ColumnIndexLog].Value;
            string  carIdentifier   = (string)dataGridViewRow.Cells[ColumnIndexCarIdentifier].Value;

            // Find the record that matches the field values for the selected row and use this record to display the associated event variables.
            EventRecord selectedEventRecord = eventRecordList.Find(delegate(EventRecord eventRecord)
            {
                if ((eventRecord.DateTime.ToString(FormatStringDateTime) == dateTime) &&
                    (eventRecord.EventIndex == eventIndex) &&
                    (eventRecord.Description == eventName) &&
                    (Lookup.LogTable.Items[eventRecord.LogIdentifier].Description == logIdentifier) &&
                    (eventRecord.CarIdentifier == carIdentifier))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

            return selectedEventRecord;
        }  
        #endregion - [EventVariables] -

        #region - [Sort] -
        /// <summary>
        /// <para>Sort the <c>DataGridView</c> rows by the specified key field in the order defined by the <c>ListSortDirection</c> property. This signature is
        /// used if the LogName column of the <c>DataGridView</c> is visible.</para>
        /// <para>The sort order is as follows:</para>
        /// <para>[fieldIndexKey] sorted by <c>ListSortDirection</c>,</para>
        /// <para>[date/time] sorted by !<c>ListSortDirection</c>,</para>
        /// <para>[fieldIndex1] sorted by <c>ListSortDirection</c>,</para>
        /// <para>[fieldIndex2] sorted by <c>ListSortDirection</c>.</para>
        /// <para>The date/time field is always sorted in the opposite direction to the key field. Where the key field values are equal for both rows, the most
        /// recent event will be displayed first if the <c>ListSortDirection</c> is defined as <c>Ascending</c> and the oldest event will be displayed first if the
        /// <c>ListSortDirection</c> is defined as <c>Descending</c>.</para>
        /// <para>Where both the key field and the date/time field values are equal, the rows will be sorted by the field corresponding to the fieldIndex1 
        /// parameter followed by the field corresponding to the fieldIndex2 parameter.</para>
        /// </summary>
        /// <remarks>The fields defined by the parameters cannot include the date/time field of the <c>DataGridView</c> control.</remarks>
        /// <param name="e">The event argument passed to the event handler corresponding to the <c>SortCompare</c> event.</param>
        /// <param name="fieldIndexKey">The field index corresponding to the key sort field.</param>
        /// <param name="fieldIndex1">The field index corresponding to the primary backup sort field in case both the key field and the date/time field values are
        /// equal.</param>
        /// <param name="fieldIndex2">The field index corresponding to the secondary backup sort field in case the key field, date/time field and fieldIndex1 field
        /// values are equal.</param>
        private void SortBy(DataGridViewSortCompareEventArgs e, int fieldIndexKey, int fieldIndex1, int fieldIndex2)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Sort the rows based upon the key field.
            e.SortResult = System.String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[fieldIndexKey].Value.ToString(),
                                                 m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[fieldIndexKey].Value.ToString());

            // If the key field values are equal, sort the rows based upon the DateTime field. 
            if (e.SortResult == 0)
            {
                // Switch the row order when comparing the date/time field values to ensure that the most recent event is displayed first if the ListSortDirection
                // enumerator is set to Ascending.
                e.SortResult = System.String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexDateTime].Value.ToString(),
                                                     m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexDateTime].Value.ToString());

                // If the date/time values are equal, sort the records based upon the event index, in the same direction as the date/time field.
                if (e.SortResult == 0)
                {
                    e.SortResult = System.String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexEventIndex].Value.ToString(),
                                                         m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexEventIndex].Value.ToString());

                    // If the date/time and EventIndex field values are equal, sort the rows based upon the fieldIndex1 field. 
                    if (e.SortResult == 0)
                    {
                        e.SortResult = System.String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[fieldIndex1].Value.ToString(),
                                                             m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[fieldIndex1].Value.ToString());

                        // If the fieldIndex1 field values are equal, sort the rows based upon the fieldIndex 2 field.
                        if (e.SortResult == 0)
                        {
                            e.SortResult = System.String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[fieldIndex2].Value.ToString(),
                                                                 m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[fieldIndex2].Value.ToString());
                        }
                    }
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// <para>Sort the <c>DataGridView</c> rows by the specified key field in the order defined by the <c>ListSortDirection</c> property.</para>
        /// <para>The sort order is as follows:</para>
        /// <para>[fieldIndexKey] sorted by <c>ListSortDirection</c>,</para>
        /// <para>[date/time] sorted by !<c>ListSortDirection</c>,</para>
        /// <para>[fieldIndex1] sorted by <c>ListSortDirection</c>,</para>
        /// <para>The date/time field is always sorted in the opposite direction to the key field. Where the key field values are equal for both rows, the most
        /// recent event will be displayed first if the <c>ListSortDirection</c> is defined as <c>Ascending</c> and the oldest event will be displayed first if the
        /// <c>ListSortDirection</c> is defined as <c>Descending</c>.</para>
        /// <para>Where both the key field and the date/time field values are equal the rows will be sorted by the field corresponding to the fieldIndex1 
        /// parameter.</para>
        /// </summary>
        /// <remarks>The fields defined by the parameters cannot include the date/time field of the <c>DataGridView</c> control.</remarks>
        /// <param name="e">The event argument passed to the event handler corresponding to the <c>SortCompare</c> event.</param>
        /// <param name="fieldIndexKey">The field index corresponding to the key sort field.</param>
        /// <param name="fieldIndex1">The field index corresponding to the primary backup sort field in case both the key field and the date/time field values are
        /// equal.</param>
        private void SortBy(DataGridViewSortCompareEventArgs e, int fieldIndexKey, int fieldIndex1)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Sort the rows based upon the key field.
            e.SortResult = System.String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[fieldIndexKey].Value.ToString(),
                                                 m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[fieldIndexKey].Value.ToString());

            // If the key field values are equal, sort the rows based upon the date/time field. 
            if (e.SortResult == 0)
            {
                // Switch the row order when comparing the date/time field values to ensure that the most recent event is displayed first if the ListSortDirection
                // enumerator is set to Ascending.
                e.SortResult = System.String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexDateTime].Value.ToString(),
                                                     m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexDateTime].Value.ToString());

                // If the date/time values are equal, sort the records based upon the event index, in the same direction as the date/time field.
                if (e.SortResult == 0)
                {
                    e.SortResult = System.String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[ColumnIndexEventIndex].Value.ToString(),
                                                         m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[ColumnIndexEventIndex].Value.ToString());

                    // If the date/time and the event index values are equal, sort the rows based upon the fieldIndex1 field. 
                    if (e.SortResult == 0)
                    {
                        e.SortResult = System.String.Compare(m_DataGridViewEventLog.Rows[e.RowIndex1].Cells[fieldIndex1].Value.ToString(),
                                                             m_DataGridViewEventLog.Rows[e.RowIndex2].Cells[fieldIndex1].Value.ToString());
                    }
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// <para>This is a comparison delegate for the List.Sort() method and is used to sort the list of event records into date/time order, most recent
        /// event first, i.e. in date/time descending order. If the date/time fields of a number of event records in the list are equal, the records are sorted
        /// by event index, in descending order, in order to determine the sequence of events. Although it is highly improbable that both the date/time field and the
        /// event index of two records are equal, if this does occur, the records are sorted by: car identifier, log name (if visible) and event description, in ascending
        /// order.</para>
        /// <para>The method compares two <c>EventRecord</c> types and returns an integer that determines their relative positions in the required sort order of
        /// the list.</para>
        /// </summary>
        /// <remarks>string A - first alphabetically, string B - second alphabetically. String.Compare(A, B): -1, String.Compare(B, A): 1, String.Compare(A, A): 0.
        /// </remarks>
        /// <param name="eventRecordA">The first event record that is to be compared.</param>
        /// <param name="eventRecordB">The second event record that is to be compared.</param>
        /// <returns>If eventRecordA is deemed to be higher in the required sort order, i.e. further down the list, than eventRecordB, a value of 1 is returned, if both
        /// records are equal a value of 0 is returned and if eventRecordA is deemed lower in the required sort order, i.e. higher up the list, a value of -1 is
        /// returned.</returns>
        protected int CompareByDateTimeDescending(EventRecord eventRecordA, EventRecord eventRecordB)
        {
            int result = 0;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return result;
            }

            if (eventRecordA == null)
            {
                if (eventRecordB == null)
                {
                    // If A is null and B is null, the records are equal.
                    result = 0;
                }
                else
                {
                    // If A is null and B is not null, B is higher in the sort order.
                    result = -1;
                }
            }
            else
            {
                if (eventRecordB == null)
                {
                    // If A is not null and B is null, A is higher in the sort order.
                    return 1;
                }
                else
                {
                    // Sort the date/time field in descending order i.e. most recent event first, so swap the parameter order when making the string comparison.
                    result = String.Compare(eventRecordB.DateTime.ToString(FormatStringDateTime), eventRecordA.DateTime.ToString(FormatStringDateTime));

                    // If the date/time values are equal, sort the records based upon the event index, in descending order.
                    if (result == 0)
                    {
                        result = String.Compare(eventRecordB.EventIndex.ToString(), eventRecordA.EventIndex.ToString());

                        // Although it is very improbable that both the date/time values and the event index values of two records are equal, if this is the case,
                        // sort the records based upon the car identifier, in ascending order. 
                        if (result == 0)
                        {
                            result = String.Compare(eventRecordA.CarIdentifier, eventRecordB.CarIdentifier);

                            // If the car identifier fields are equal, either sort the records based upon the log name, if this column is visible, otherwise, sort the
                            // records based upon the event name.
                            if (result == 0)
                            {
                                // Check whether the Log column is visible.
                                if (m_DataGridViewEventLog.Columns[ColumnIndexLog].Visible == true)
                                {
                                    // Get the log name associated with each event record;
                                    string logNameA, logNameB;
                                    try
                                    {
                                        logNameA = Lookup.LogTable.Items[eventRecordA.LogIdentifier].Description;
                                        logNameB = Lookup.LogTable.Items[eventRecordB.LogIdentifier].Description;
                                    }
                                    catch (Exception)
                                    {
                                        logNameA = string.Empty;
                                        logNameB = string.Empty;
                                    }

                                    // Sort the records by the log name, in ascending order.
                                    result = String.Compare(logNameA, logNameB);

                                    // If the log names are equal, sort the records based upon the the event name property, in ascending order.
                                    if (result == 0)
                                    {
                                        // Sort the records based upon the event name, in ascending order.
                                        result = String.Compare(eventRecordA.Description, eventRecordB.Description);
                                    }
                                }
                                else
                                {
                                    // Sort the records based upon the event name, in ascending order.
                                    result = String.Compare(eventRecordA.Description, eventRecordB.Description);
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
        #endregion - [Sort] -

        #region - [Save] -
        /// <summary>
        /// Ask the user to specify the filename where the events are to be saved and call the SaveAll() method to save ALL events and associated data streams to disk. 
        /// If the specified file already exists the current events are appended to the existing data.
        /// </summary>
        /// <remarks>Polling for new events must be suspended before making a call to this method.</remarks>
        /// <returns>A flag to indicate whether the user cancelled the operation. True, indicates that the operation was cancelled by the user; otherwise, false.
        /// </returns>
        private bool Save()
        {
            bool userCancelled = false;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return userCancelled;
            }

            DateTime createdTime = DateTime.Now;
            string defaultFilename = General.DeriveName(FileHeader.HeaderCurrent.TargetConfiguration.CarIdentifier, createdTime, CommonConstants.ExtensionEventLog,
                                                        string.Empty);
            string fullFilename = General.FileDialogSaveEventLog(defaultFilename, InitialDirectory.EventLogsWrite);
            if (fullFilename != string.Empty)
            {
                EventLogFile_t selectedEventLogFile;
                Header_t header;

                userCancelled = CheckForAppend(fullFilename, ref createdTime, out header, out selectedEventLogFile);
                if (userCancelled == true)
                {
                    return userCancelled;
                }

                // Update the initial directory with the path of the selected file.
                InitialDirectory.EventLogsWrite = Path.GetDirectoryName(fullFilename);

                // Create a new event log file structure to store all event information.
                EventLogFile_t eventLogFile = new EventLogFile_t(header);

                // Check whether any saved events are to be added to the event log file structure.
                if (selectedEventLogFile.EventRecordList.Count > 0)
                {
                    eventLogFile.AppendEventRecordList(selectedEventLogFile.EventRecordList);
                }

                userCancelled = SaveAll(ref eventLogFile, fullFilename);

                if (eventLogFile.EventRecordList.Count <= 0)
                {
                    MainWindow.WriteStatusMessage(Resources.SMEventListEmpty);
                }
                else
                {
                    MainWindow.WriteStatusMessage(string.Empty);
                }
            }
            return userCancelled;
        }

        /// <summary>
        /// Save the events associated with all event logs to the specified event log file structure, serialize this to the specified file in XML format and save 
        /// the associated data streams to disk using their default filenames.
        /// </summary>
        /// <remarks>Polling for new events must be suspended before making a call to this method.</remarks>
        /// <param name="eventLogFile">The event log file structure where the event records are to be saved.</param>
        /// <param name="fullFilename">The fully qualified filename of the file.</param>
        /// <returns>A flag to indicate whether the user cancelled the operation. True, indicates that the operation was cancelled by the user; otherwise, false.
        /// </returns>
        private bool SaveAll(ref EventLogFile_t eventLogFile, string fullFilename)
        {
            bool userCancelled = false;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return userCancelled;
            }

            // Add the event records associated with the current log to the event log file.
            eventLogFile.AppendEventRecordList(EventRecordList);

            // Save the data streams associated with the current log.
            userCancelled = SaveDataStreams(CommunicationInterface, eventLogFile.Header, m_Log, EventRecordList);
            if (userCancelled == true)
            {
                // The SaveDataStreams() method is responsible for updating the status message.
                return userCancelled;
            }

            // Now add the event records associated with the remaining logs.
            Log log;
            List<EventRecord> eventRecordList;
            for (int logIndex = 0; logIndex < Lookup.LogTable.RecordList.Count; logIndex++)
            {
                Cursor = Cursors.WaitCursor;
                eventRecordList = new List<EventRecord>();
                log = Lookup.LogTable.RecordList[logIndex];

                // Only process those logs that have been defined.
                if (log != null)
                {
                    // Only process the log if it is not the current log.
                    if (log.Identifier != m_Log.Identifier)
                    {
                        eventRecordList = GetEventLog(log);
                        eventLogFile.AppendEventRecordList(eventRecordList);
                        userCancelled = SaveDataStreams(CommunicationInterface, eventLogFile.Header, log, eventRecordList);
                        if (userCancelled == true)
                        {
                            Cursor = Cursors.Default;
                            return userCancelled;
                        }
                    }
                }
            }

            // Only serialize the structure if it contains one or more events.
            if (eventLogFile.EventRecordList.Count > 0)
            {
                FileInfo fileInfo = new FileInfo(fullFilename);
                MainWindow.WriteStatusMessage(string.Format(Resources.SMSaveFile, fileInfo.Name));

                // Serialize the data to the specified file.
                FileHandling.Serialize<EventLogFile_t>(fullFilename, eventLogFile, FileHandling.FormatType.Xml);

                eventLogFile.EventRecordList.Sort(CompareByDateTimeDescending);

                // Check whether the Settings need to be updated.
                if (eventLogFile.EventRecordList[0] != Settings.Default.MostRecentDownloadedEventsSaved.DownloadedEvents[MainWindow.CarNumber])
                {
                    // Keep a record of the most recent event that was saved to disk.
                    MostRecentDownloadedEvents mostRecentDownloadedEvents = Settings.Default.MostRecentDownloadedEventsSaved;
                    mostRecentDownloadedEvents.DownloadedEvents[MainWindow.CarNumber] = eventLogFile.EventRecordList[0];
                    Settings.Default.Save();
                }
            }

            m_EventLogSavedStatus = EventLogSavedStatus.Saved;

            // Update the MainWindow StatusLabel.
            MainWindow.LogStatus = m_EventLogSavedStatus;
            
            // Restore the current log.
            ClearControls();
            ClearEventMemory();

            // Retrieve and display the current event log in preparation for new events.
            EventRecordList = GetEventLog(m_Log);
            AddList(EventRecordList);

            // -------------------------------------------------------------------------
            // Display the event variable list if the list contains one or more entries.
            // -------------------------------------------------------------------------
            if (m_DataGridViewEventLog.SelectedRows.Count > 0)
            {
                EventRecord selectedEventRecord = FindEventRecord(m_DataGridViewEventLog.SelectedRows[0], EventRecordList);
                if (selectedEventRecord != null)
                {
                    DisplayEventVariableList(selectedEventRecord);
                }
            }

            Cursor = Cursors.Default;
            return userCancelled;
        }

        /// <summary>
        /// Save all available data streams associated with the specified event log to disk.
        /// </summary>
        /// <remarks>Polling for new events must be suspended before making a call to this method.</remarks>
        /// <param name="communicationInterface">Reference to the communicaton interface that is to be used to communicate with the VCU.</param>
        /// <param name="header">The header associated with each file.</param>
        /// <param name="log">The log for which the data streams are to be saved.</param>
        /// <param name="eventRecordList">The list of event records associated with the log.</param>
        /// <returns>A flag to indicate whether the user cancelled the operation. True, indicates that the operation was cancelled by the user; otherwise, false.
        /// </returns>
        private bool SaveDataStreams(ICommunicationEvent communicationInterface, Header_t header, Log log, List<EventRecord> eventRecordList)
        {
            bool userCancelled = false;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return userCancelled;
            }

            if (eventRecordList.Count <= 0)
            {
                return userCancelled;
            }

            Debug.Assert(MainWindow != null, "FormViewEventLog - [SaveDataStreams - [MainWindow != null]");
            Debug.Assert(log != null, "FormViewEventLog.SaveDataStreams - [log != null]");

            MainWindow.WriteStatusMessage(string.Format(Resources.SMDataStreamsSave, log.Description));

            // Determine the number of data streams that must be saved for this log.
            EventRecord eventRecord;
            short streamsToSaveCount = 0;
            for (int eventIndex = 0; eventIndex < eventRecordList.Count; eventIndex++)
            {
                eventRecord = eventRecordList[eventIndex];
                string defaultFilename;
                Debug.Assert(eventRecord != null, "FormViewEventLog.SaveAllDataStreams() - [eventRecord != null]");
                if (eventRecord.StreamSaved == true)
                {
                    string fullyQualifiedFaultLogFilename = General.GetFullyQualifiedFaultLogFilename(eventRecord, out defaultFilename);

                    // Check whether the fault log associated with the event record has already been saved to disk.
                    FileInfo fileInfoFaultLog = new FileInfo(fullyQualifiedFaultLogFilename);
                    if (fileInfoFaultLog.Exists == false)
                    {
                        streamsToSaveCount++;
                    }
                }
            }

            // Save those data streams that haven't already been save to disk.
            short streamCount = 1;
            FormGetStream formGetStream;
            for (int eventIndex = 0; eventIndex < eventRecordList.Count; eventIndex++)
            {
                eventRecord = eventRecordList[eventIndex];
                string defaultFilename;
                if (eventRecord.StreamSaved == true)
                {
                    // Clear the DataStreamCurrent property;
                    DataStreamCurrent = new DataStream_t();

                    string fullyQualifiedFaultLogFilename = General.GetFullyQualifiedFaultLogFilename(eventRecord, out defaultFilename);

                    // Check whether the fault log associated with the event record has already been saved to disk.
                    FileInfo fileInfoFaultLog = new FileInfo(fullyQualifiedFaultLogFilename);
                    if (fileInfoFaultLog.Exists == false)
                    {
                        try
                        {
                            MainWindow.WriteStatusMessage(string.Format(Resources.SMDataStreamSaveXofY, log.Description, streamCount++, streamsToSaveCount));
                            formGetStream = new FormGetStream(CommunicationInterface, eventRecord);
                            formGetStream.CalledFrom = this;

                            // The FormGetStream class will copy the downloaded data stream to the DataStream property of this class.
                            DialogResult dialogResult = formGetStream.ShowDialog();
                            if (dialogResult == DialogResult.Cancel)
                            {
                                MessageBox.Show(Resources.MBTUserCancelled, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                userCancelled = true;
                                return userCancelled;
                            }
                            else if (dialogResult == DialogResult.Abort)
                            {
                                MessageBox.Show(string.Format(Resources.MBTSaveDataStreamsFailed, eventRecord.Description), Resources.MBCaptionError,
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                                // Reset the communication port.
                                CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                                CommunicationInterface.InitCommunication(CommunicationInterface.CommunicationSetting);

                                userCancelled = true;
                                return userCancelled;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(string.Format(Resources.MBTGetStreamFailed, eventRecord.Description), Resources.MBCaptionError, MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);

                            // Reset the communication port.
                            CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                            CommunicationInterface.InitCommunication(CommunicationInterface.CommunicationSetting);
                            continue;
                        }
                        finally
                        {
                            formGetStream = null;
                        }

                        WatchFile_t watchFile = new WatchFile_t(header, DataStreamCurrent);

                        // Serialize the data to the specified file.
                        try
                        {
                            FileHandling.Serialize<WatchFile_t>(fullyQualifiedFaultLogFilename, watchFile, FileHandling.FormatType.Binary);
                        }
                        catch (InvalidDataException)
                        {
                            MessageBox.Show(Resources.MBTFaultLogSerializationFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                    }
                }
            }

            userCancelled = false;
            return userCancelled;
        }

        /// <summary>
        /// Check whether the user has selected an existing file and, if so, ask the user whether the data is to be appended to the existing data or the file is 
        /// to be over-written. If the data is to be appended to the existing data load the existing data into the event log file structure.
        /// </summary>
        /// <param name="fullFilename">The fully qualified filename of the selected file.</param>
        /// <param name="createdTime">The date and time reference that is to be used for the creation date of the file.</param>
        /// <param name="header">The header information that is to be used when saving the file.</param>
        /// <param name="savedEventLogFile"></param>
        /// <returns>A flag to indicate whether the user cancelled the operation. True, indicates that the operation was cancelled by the user; otherwise, false.
        /// </returns>
        protected bool CheckForAppend(string fullFilename, ref DateTime createdTime, out Header_t header, out EventLogFile_t savedEventLogFile)
        {
            bool userCancelled = false;
            header = FileHeader.HeaderCurrent;

            // Create an empty event log file structure to contain any saved event information that is to be added to the event log file structure.
            savedEventLogFile = new EventLogFile_t();
            savedEventLogFile.EventRecordList = new List<EventRecord>();

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return userCancelled;
            }

            FormAddComments formAddComments = new FormAddComments(header, createdTime);
            formAddComments.CalledFrom = this;

            // Check whether the user has selected an existing file.
            FileInfo fileInfo = new FileInfo(fullFilename);
            if (fileInfo.Exists == true)
            {
                // Yes. Ask whether the current events are to be appended to the existing events ar whether the file is to be over-written.
                DialogResult dialogResult = MessageBox.Show(Resources.MBTAppendOverwrite, Resources.MBCaptionInformation, MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Information);
                Update();
                if (dialogResult == DialogResult.Yes)
                {
                    // The current events are to be appended to the existing events. Retrieve the existing records.
                    savedEventLogFile = FileHandling.Load<EventLogFile_t>(fullFilename, FileHandling.FormatType.Xml);

                    // Check whether the existing file was corrupt.
                    if (savedEventLogFile.EventRecordList == null)
                    {
                        // Yes. Ask whether to over-write the existing data or cancel.
                        dialogResult = MessageBox.Show(Resources.MBTFileCorrupt, Resources.MBCaptionError, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        if (dialogResult == DialogResult.Cancel)
                        {
                            userCancelled = true;
                            return userCancelled;
                        }

                        // Clear the existing records.
                        savedEventLogFile.EventRecordList = new List<EventRecord>();
                    }
                    // Ensure that the selected log file is associated with the current project.
                    else if (savedEventLogFile.Header.ProjectInformation.ProjectIdentifier != Parameter.ProjectInformation.ProjectIdentifier)
                    {
                        // Mismatch. Inform the user and cancel the save operation.
                        MessageBox.Show(Resources.MBTAppendProjectMismatch, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return userCancelled;
                    }
                    else
                    {
                        // Over-write the existing events.
                        formAddComments.Header = savedEventLogFile.Header;
                    }
                }
            }

            formAddComments.ShowDialog();
            header = formAddComments.Header;

            return userCancelled;
        }
        #endregion - [Save] -

        #region - [Clear/Initialize] -
        /// <summary>
        /// Clear or initialize all event logs depending upon which function delegate is used.
        /// </summary>
        /// <param name="function">A delegate for the function that is to be called. This will be a delegate for the function to clear the event logs or a delegate for 
        /// the function to reset the event logs.</param>
        /// <remarks>Polling for new events must be suspended before making a call to this method.</remarks>
        /// <returns>A flag to indicate whether the user cancelled the operation. True, indicates that the operation was cancelled by the user; otherwise, false.
        /// </returns>
        private bool ClearOrInitialize(ZeroParameterDelegate function)
        {
            bool userCancelled = false;
            DialogResult dialogResult;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return userCancelled;
            }

            // Set up the appropriate status and error messages.
            string statusMessage, errorMessage, confirmationMessage;
            if (function.Method.ToString().Contains(KeyClearEvent))
            {
                statusMessage = Resources.SMEventLogsClear;
                errorMessage = Resources.MBTEventLogClearFailed;
                confirmationMessage = Resources.MBTConfirmClearEventLogs;
            }
            else
            {
                statusMessage = Resources.SMEventLogsInitialize;
                errorMessage = Resources.MBTEventLogInitializeFailed;
                confirmationMessage = Resources.MBTConfirmInitializeEventLogs;
            }

            // Ask the user to confirm that they want to clear/initialize the fault log.
            dialogResult = MessageBox.Show(confirmationMessage, Resources.MBCaptionQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Update();
            if (dialogResult == DialogResult.No)
            {
                userCancelled = true;
                return userCancelled;
            }

            // Ask the user whether they want to save the current event logs and data streams before proceeding.
            dialogResult = MessageBox.Show(Resources.MBTQueryEventLogSave, Resources.MBCaptionInformation, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Update();
            if (dialogResult == DialogResult.Yes)
            {
                // Save the current events and check whether the save was cancelled by the user.
                userCancelled = Save();
                if (userCancelled == true)
                {
                    // The current events were not saved to disk, ask the user whether they want to continue.
                    dialogResult = MessageBox.Show(Resources.MBTEventLogSaveFailed, Resources.MBCaptionWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    Update();
                    if (dialogResult == DialogResult.Cancel)
                    {
                        userCancelled = true;
                        return userCancelled;
                    }
                }
            }

            // Clear/Initialize each event log.
            Log log;
            for (int logIndex = 0; logIndex < Lookup.LogTable.RecordList.Count; logIndex++)
            {
                log = Lookup.LogTable.RecordList[logIndex];

                // Only process those logs that have been defined.
                if (log != null)
                {
                    MainWindow.WriteStatusMessage(string.Format(statusMessage, log.Description));

                    try
                    {
                        m_CommunicationInterface.ChangeEventLog(log);

                        // Call the function that was passed as a parameter. This function will either clear or initialize the event logs.
                        function();
                    }
                    catch (CommunicationException)
                    {
                        MessageBox.Show(errorMessage, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return userCancelled;
                    }

                    MainWindow.WriteStatusMessage(string.Empty);
                }
            }

            ClearControls();
            ClearEventMemory();

            // Retrieve and display the current event log in preparation for new events.
            EventRecordList = GetEventLog(m_Log);
            AddList(EventRecordList);

            // Disable the Save function key and either the Clear only, or the Clear and Initialize function keys, depending upon which function key was selected.
            // These keys will remain disabled until a new event is detected.
            if (function.Method.ToString().Contains(KeyClearEvent))
            {
                // Disable the Clear function key but still allow the user to initialize the event logs.
                F4.Enabled = false;
            }
            else if (function.Method.ToString().Contains(KeyInitializeEventLog))
            {
                // Disable the Clear and Initialize function keys.
                F4.Enabled = false;
                F5.Enabled = false;
            }

            // Disable the Saave function key.
            F3.Enabled = false;

            return userCancelled;
        }

        /// <summary>
        /// Clear the event log memory contained within the 'PTUDLL32' dynamic link library.
        /// </summary>
        private void ClearEventMemory()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            try
            {
                m_CommunicationInterface.FreeEventLogMemory();
            }
            catch (Exception)
            {
                // Do nothing, just ensure that an exception isn't thrown. 
            }
        }

        /// <summary>
        /// Clear the rows of the <c>DataGridView</c> control.
        /// </summary>
        /// <remarks>De-registers the <c>SelectionChanged</c> event handler while the rows are being cleared.</remarks>
        protected void ClearDataGridViewRows()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_MutexDataGridView.WaitOne(DefaultMutexWaitDurationMs, false);
            if (m_DataGridViewEventLog != null)
            {
                // Ensure that the SelectionChanged event is not triggered as a result of clearing the rows of the DataGridView control.
                m_DataGridViewEventLog.SelectionChanged -= new EventHandler(m_DataGridViewEventLog_SelectionChanged);
                m_DataGridViewEventLog.Rows.Clear();
                m_DataGridViewEventLog.SelectionChanged += new EventHandler(m_DataGridViewEventLog_SelectionChanged);
            }
            m_MutexDataGridView.ReleaseMutex();
        }

        /// <summary>
        /// Clear the event log related controls.
        /// </summary>
        internal void ClearControls()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ClearDataGridViewRows();

            // Clear the list of event variables that is currently on display.
            if (m_PanelEventVariableList != null)
            {
                m_PanelEventVariableList.Controls.Clear();
            }

            // Clear the list of event records that were downloaded from the VCU.
            if (EventRecordList != null)
            {
                EventRecordList.Clear();
            }

            Update();
        }
        #endregion - [Clear/Initialize] -

        #region - [IPollTarget] -
        /// <summary>
        /// Start polling the target hardware for new events. If polling is already underway, no action ill be taken.
        /// </summary>
        public void StartPolling()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_ThreadPollEvent == null)
            {
                m_ThreadPollEvent = new ThreadPollEvent(CommunicationInterface, this);
                m_ThreadPollEvent.Start();
            }
        }

        /// <summary>
        /// Stop polling the target hardware. If polling has already been suspended, no action will be taken. 
        /// </summary>
        /// <remarks>Ignores the request if the class used to poll the target hardware is null.</remarks>
        public void StopPolling()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_ThreadPollEvent != null)
            {
                m_ThreadPollEvent.Dispose();
                m_ThreadPollEvent = null;
            }
        }

        /// <summary>
        /// Set the Pause property and wait until the feedback signal is received or until the timeout has elapsed.
        /// </summary>
        /// <param name="timeoutMs">The timeout period, in ms.</param>
        /// <returns>A flag to indicate whether the pause feedback signal was asserted within the specified timeout. True, if the pause feedback signal was asserted 
        /// within the specified timeout; otherwise, false.</returns>
        public bool SetPauseAndWait(int timeoutMs)
        {
            // Return true if the thread is not yet instantiated.
            bool pauseFeedbackAsserted = true;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return pauseFeedbackAsserted;
            }

            // Skip, if the ThreadPollWatch class has not been instantiated.
            if (m_ThreadPollEvent == null)
            {
                return pauseFeedbackAsserted;
            }

            Pause = true;

            // Wait until the feedback signal is asserted or for timeout.
            DateTime startTime = DateTime.Now;
            while (PauseFeedback == false && (DateTime.Now < startTime.Add(new TimeSpan(0, 0, 0, 0, timeoutMs))))
            {
                Thread.Sleep(CommonConstants.SleepMsPauseFeedback);
            }

            if (PauseFeedback == false)
            {
                pauseFeedbackAsserted = false;

                // Clear the Pause property if the feedback was not asserted within the timeout period.
                Pause = false;
            }

            return pauseFeedbackAsserted;
        }
        #endregion - [IPollTarget] -
        #endregion --- Methods ---

        #region --- Properties ---
        #region - [IPollTarget] -
        /// <summary>
        /// Gets or sets the flag that controls the polling of the target hardware. True, inhibits polling of the target hardware; otherwise, false, resumes polling.
        /// </summary>
        public bool Pause
        {
            set
            {
                m_Pause = value;

                // If the ThreadPollWatch class has been instantiated update the Pause property of that class.
                if (m_ThreadPollEvent != null)
                {
                    m_ThreadPollEvent.Pause = m_Pause;
                }
            }

            get
            {
                // Check whether the ThreadPollWatch class has been instantiated.
                if (m_ThreadPollEvent != null)
                {
                    // Yes, return the Pause property of that class.
                    return m_ThreadPollEvent.Pause;
                }
                else
                {
                    return m_Pause;
                }

            }
        }

        /// <summary>
        /// Gets the feedback flag that indicates whether polling of the target hardware has been suspended.  
        /// </summary>
        /// <remarks>This flag is asserted when the <c>ThreadPollWatch</c> class has entered the pause state, i.e. the current communication request is complete and 
        /// no further requests will be issued until the pause property has been cleared.</remarks>
        public bool PauseFeedback
        {
            get
            {
                // If the ThreadPollWatch class has been instantiated return the PauseFeedback property of that class.
                if (m_ThreadPollEvent != null)
                {
                    return m_ThreadPollEvent.PauseFeedback;
                }
                else
                {
                    // Just report back the state of the Pause property.
                    return Pause;
                }
            }
        }
        #endregion - [IPollTarget] -

        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the VCU.
        /// </summary>
        public ICommunicationEvent CommunicationInterface
        {
            get { return m_CommunicationInterface; }
            set { m_CommunicationInterface = value; }
        }

        /// <summary>
        /// Gets the list of the event records retrieved from the VCU.
        /// </summary>
        internal List<EventRecord> EventRecordList
        {
            get 
            {
                lock (m_EventRecordList)
                {
                    return m_EventRecordList;
                }
            }

            set
            {
                lock (m_EventRecordList)
                {
                    m_EventRecordList = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of events in the current event log.
        /// </summary>
        internal short EventCount
        {
            get 
            {
                short result;
                m_MutexEventCount.WaitOne(DefaultMutexWaitDurationMs, false);
                result = m_EventCount;
                m_MutexEventCount.ReleaseMutex();
                return result;
            }
            
            set 
            {
                m_MutexEventCount.WaitOne(DefaultMutexWaitDurationMs, false);
                m_EventCount = value;
                m_MutexEventCount.ReleaseMutex();
            }
        }

        /// <summary>
        /// Gets or sets the event index of the next entry in the event log.
        /// </summary>
        internal uint EventIndex
        {
            get
            {
                uint result;
                m_MutexEventIndex.WaitOne(DefaultMutexWaitDurationMs, false);
                result = m_EventIndex;
                m_MutexEventIndex.ReleaseMutex();
                return result;
            }

            set
            {
                m_MutexEventIndex.WaitOne(DefaultMutexWaitDurationMs, false);
                m_EventIndex = value;
                m_MutexEventIndex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Gets the selected event log.
        /// </summary>
        internal Log Log
        {
            get { return m_Log; }
        }

        /// <summary>
        /// Gets or sets the reference to the latest data stream downloaded from the VCU.
        /// </summary>
        internal DataStream_t DataStreamCurrent
        {
            get { return m_DataStream; }
            set { m_DataStream = value; }
        }
        #endregion --- Properties ---
    }
}
