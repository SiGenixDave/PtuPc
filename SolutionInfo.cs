#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2015    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    -
 * 
 *  File name:  SolutionInfo.cs
 * 
 *  Conditional Compilation Symbols
 *  -------------------------------
 *  Project                 Symbol                      Description
 *  Event                   SimulateNewEvent            Simulate new events being generated while the user is viewing event logs.
 *  PTU Application         ByPassVersionCheck          Do not check that the version number of the embedded software matche that of the data
 *                                                      dictionary.
 * 
 * 
 *  Revision History
 *  ----------------
 */
#region - [Revision History 6.0] -
/* 
 *  Date        Version     Author          Comments
 *  08/12/10    6.0.0.0     K.McDonald      1.  First entry into TortoiseSVN.
 * 
 *  08/13/10    6.0.1.0     K.McDonald      1.  Added XML tags and revision history to the auto-generated Interface definitions.
 *                                          2.  Renamed IFrame to IWatchFrame.
 *                                          3.  Deleted IWatchMenuInterface.
 */
#endregion - [Revision History 6.0] -

#region - [Revision History 6.1] -
/*
 *  Date        Version     Author          Comments
 *  08/13/10    6.1         K.McDonald      1.  Added FormShowHeaderInformation to display the file header information.
 *                                          2.  Updated Watch.FormViewDataStream to version 1.1. Included event handler for the 'F12-Header Information' function key.
 *                                          3.  Moved the GetUserName() method from class FormAddComments to class General.  
 *                                              (a) Updated Common.General.cs to version 1.1. Included GetUserName() method.
 *                                              (b) Updated FormAddComments to version 1.1. Moved GetUserName() method.
 *                                              (c) Updated FileHeader.cs to version 1.1.
 */
#endregion - [Revision History 6.1] -

#region - [Revision History 6.2] -
/*
 *  Date        Version     Author          Comments
 *  08/16/10    6.2         K.McDonald      1.  Updated Watch.FormWatchReplay to version 1.1. Included event handler for the 'F12-Header Information' function key.
 *                                          2.  Bug fix SNCR 001.009. If the user exits the replay screen directly rather than returning back via the YT plot screen, 
 *                                              the function keys are not restored correctly.
 * 
 *  08/18/10    6.2.1       K.McDonald      1.  Added the self-test project to the solution.
 *                                          2.  Added support for the self-test menu options.
 * 
 *  08/18/10    6.2.2       K.McD           1.  Minor documentation changes and renaming of variables, however, no functional changes.
 * 
 *  08/19/10    6.2.3       K.McD           1.  Added support for plotting the individual bits/flags of a bitmask watch variable.
 * 
 *                                              (a) Added Common.IPlotterWatch, IPlotterScalar and IPlotterBitmask interface definitions.
 *                                              (b) Added Common.PlotterBitmask user control.
 *                                              (c) Added Common.PlotterScalar user control, originally named PlotterControl.
 *                                              (d) Modified PlotterControlLayout, FormViewDataStream - Modified design to use IPlotterWatch, IPlotterBitmask and 
 *                                                  IPlotterScalar interface definitions.
 * 
 *  08/20/10    6.2.4       K.McD           1.  Modified the form used to display the flag states associated with a bitmask watch variable so that the current state of 
 *                                              ALL of the flags associated with the bitmask are displayed.
 * 
 *                                          2.  Ensured that any form showing the bitmask states is closed if the child form containing the bitmask that called the 
 *                                              form is disposed of.
 * 
 *                                          3.  Modified the form used to display the flag states associated with a bitmask watch variable such that the flag state update 
 *                                              is triggered by an event raised by the parent form associated with the forms used to display the live watch variable data
 *                                              and the saved watch variable data.
 * 
 *                                          4.  Minor variable name changes and documentation changes.
 * 
 *  08/21/10    6.2.5       K.McD           1.  Modified Common.CyclicQueue.cs, removed the reference to the ICyclicQueue interface definition.
 *                                          2.  Removed the Common.ICyclicQueue interface definition from the solution.
 *                                          3.  Modified the OpenDataStreamFile() method of Watch.MenuInterfaceWatch to ensure that the wait cursor of the main
 *                                              application window was displayed correctly.
 * 
 *  08/20/10    6.2.6       K.McD           1.  Include support for the BITMASK_DEMO conditional compilation symbol.
 * 
 *  08/21/10    6.2.7       K.McD           1.  Modified Common.FormShowFlags class. Changed HeightAdjust constant from 30 to 25 px.
 */
#endregion - [Revision History 6.2] -

#region - [Revision History 6.3] -
/*
 *  Date        Version     Author          Comments 
 *  08/23/10    6.3         K.McD           1.  Added support to include ALL tables associated with the Microsoft Access engineering database in the XML data dictionary. 
 * 
 *  08/24/10    6.3.1       K.McD           1.  Added support to enable the programmer to modify and restore the Enabled property of individual menu options.
 *                                          2.  Moved the SetMenuStripVisible(), SetToolStripMenuItemVisible(), SetMenuStripEnabled(), SetToolStripMenuItemEnabled()
 *                                              methods to the Common.General class.
 * 
 *  08/24/10    6.3.1.1     K.McD           1.  Included the modifications to the FormPTU class required to enable the programmer to modify and restore the Enabled 
 *                                              property of individual menu options.
 * 
 *  08/25/10    6.3.1.2     K.McD           1.  Added the CommunicationException class.
 *                                          2.  Modified the ICommunication interface definition: 
 *                                                  (a) Removed the PortIsOpen property, 
 *                                                  (b) Added the bool forceUpdate parameter to the method UpdateWatchElements().
 *                                          3.  Extended the available signatures associated with the WriteStatusMessage() method to allow the programmer to specify the
 *                                              background and foreground colours of the status message.
 * 
 *  08/25/10    6.3.1.3     K.McD           1.  Modified MdiPTU.Designer.cs, removed a number of menu options that were no longer required.
 *                                          2.  Renamed a number of variables for consistency.
 *                                          3.  Added support for the 'Configure/System Information' menu option.
 *                                          4.  Added the Pause property to the FormPTU class to allow other dialog forms that need to communicate with the target to 
 *                                              suspend/pause communication requests of the Mdi child form to the target until the dialog form is disposed of.
 *                                          5.  Modified the FormPTUDialog class to automatically assert the Pause property of any multiple-document interface (MDI) child 
 *                                              forms that are open if the CommunicationInterface property is defined. Also clears the Pause property on disposal. 
 * 
 *  08/26/10    6.3.1.4     K.McD           1.  Ensure that any dialog forms are disposed of immediately if closed using the [X] icon.
 *                                          2.  Added General.ConvertYear() method.
 *                                          3.  Added the ToolStripComboBox and ToolStripLabel controls to the ToolStrip on FormPTU.
 *                                          4.  Documentation changes to the MdiPTU form.
 * 
 *  08/26/10    6.3.1.5     K.McD           1.  Modified the ThreadPollWatch class. Implemented code which attempts to recover from a communication fault.
 *                                          2.  Modified the PlotterControlLayout class. Modified the SetBreakPoints() method to round the breakpoint time down to the
 *                                              nearest 10ms.
 *                                          3.  Major changes the the FormWatchView class, see the revision history associated with version 1.1. Added Pause property etc.
 * 
 *  08/30/10    6.3.1.6     K.McD           1.  Modifications following Aug sprint review.
 *                                          2.  SNCR PTU-001.12 - Modified the Common.BitmaskControl user control so that value field is displayed even when the value is
 *                                              zero.
 *                                          3.  SNCR PTU-001.13 - Modified the Common.FormShowFlags form so that the asserted state is shown as black on yellow.
 *                                          4.  SNCR PTU-001.13 - Modified GraphComponents.LogicAnalyzer so that: (a) the pen color associated with the alarm state is
 *                                              yellow, and (b) the text that appears in the cursor is 'Asserted' rather than alarm.
 *                                          5.  Modified Common.General class. Renamed ConvertYear() to ConverYearToLong() and added ConvertYearToShort().
 *                                          6.  Modified the Common.FormPTUDialog class. Modified the 'Shown' event handler so that polling is only suspended an any child 
 *                                              forms if the dialog form is called from the main application window.
 *                                          7.  Layout changes to the FormShowHeaderInformation class and 
 *                                          9.  Modified the Watch.FormWatchView class. Replaced the ComboBox used to select the workset with the ToolStripComboBox 
 *                                              inherited from the FormPTU class.
 * 
 *  09/01/10    6.3.1.7     K.McD           1.  Included the classes required to support the 'Configure/System Information' menu option. Added FormShowSystemConfiguration,
 *                                              FormConfigureDateTime and FormConfigureCarIdentifier.
 * 
 *  09/02/10    6.3.1.8     K.McD           1.  Refactoring of the communication classes so that the communication methods associated with the various sub-systems can be 
 *                                              developed in the individual projects.
 * 
 *  09/02/10    6.3.1.9     K.McD           1.  Added the configuration files, workset files and PTUDLL32.dll to the PTU Application project. These must be moved to the
 *                                              appropriate folders following the build.
 * 
 *  09/29/10    6.3.2       K.McD           1.  Added support to allow the user to write watch data to the Vehicle Control Unit (VCU).
 *                                          2.  Removed the files added in build 6.3.1.9. These have been added to the subversion repository for safekeeping, however, 
 *                                              they are no longer included in the Visual Studio project.
 * 
 *  09/29/10    6.3.2.1     K.McD           1.  Added the new Bombardier logo.
 * 
 *  09/30/10    6.3.2.2     K.McD           1.  Added support for the 'Configure/Enumeration' context menu.
 *                                          2.  Alt Key activation for the 'Change Value' context menu option of the bitmask user control changed to V for consistency.
 *                                          3.  Bug fix associated with the form which allows the user to modify the bitmask values, see FormChangeBitmask.cs version 1.1.
 *                                          4.  Modified the form used to display the system information so that it now publishes a DataUpdate event whenever the date 
 *                                              and time is updated.
 *                                          5.  Modified the form used to configure the date and time so that the date and time display is now updated using the DataUpdate 
 *                                              event published by the for used to display the system information.
 * 
 *  10/06/10    6.3.2.3     K.McD           1.  Bug fix SNCR001.18. Registered the KeyPress event on FormWatchView to trigger the simulated fault log whenever the user
 *                                              presses the space bar.
 *                                          2.  Bug fix SNCR001.21. Included support for the <IPollTarget> interface rather than using a fixed Sleep period to minimize the
 *                                              delay in polling the VCU.
 *                                          3.  Bug fix SNCR001.14. Ensured that all forms used to view the status of individual bitmask bits are closed when the user
 *                                              changes the active workset by including the OpenedDialogBoxList property and implementing the CloseAllDialogBoxForms()
 *                                              method on FormPTU.
 *                                          4.  Bug fix SNCR001.23. Ensured that the 'Configure/System Information' menu option is disabled while data is being recorded.
 *                                          5.  Bug fix SNCR001.24. Ensured that any active child forms are closed prior to implementing any menu option that may cause
 *                                              conflict.
 *                                          6.  Bug Fix SNCR001.28. Included a try/catch block within the Shown() event handler associated with FormSelectTarget form to
 *                                              catch any exception thrown as a result of an attempt to read the device drivers that have been registered in the Registry.
 *                                          7.  Bug fix SNCR001.15. Removed the call to WriteCarIdentifier() in the ConfigureWorksets() method of the
 *                                              MenuInterfaceApplication class.
 *                                          8.  Bug fix SNCR001.17. Modified the WatchControl user control to check whether the the font property of the client form has
 *                                              changed and update the font of the label used to display the value of the watch variable accordingly.
 *                                          9.  Modified the default interval between successive frames of the recorded data files for the following: (a) recorded watch
 *                                              files; (b) simulated fault logs and (c) fault logs to 240ms, 240ms and 60ms respectively.
 * 
 *  10/11/10    6.3.2.4     K.McD           1.  Corrected the location of the user message status strip back to (390,0). The reason why this keeps changing is, as yet,
 *                                              unclear.
 *                                          2.  Modified the default location of the application data directory to C:\ProgramData\Portable Test Unit in Common.dll.
 *                                          3.  Set the Font property of the user application settings to: Segoe UI, 8.25pt in the PTU Application project.
 *                                          4.  Removed a number of unused user settings from the PTU Application project.
 * 
 *  10/13/10    6.3.2.5     K.McD           1.  Updated Template.cs header.
 *                                          2.  Rationalized the: app.config, AssemblyInfo and Settings for all projects:
 * 
 *                                                  a.  Updated AssemblyInfo.cs for the: GraphComponents, Common, PTU Application and Watch projects.
 *                                                  b.  Deleted: Settings.cs, Settings.Designer.cs and Settings.settings from the Watch project.
 *                                                  c.  Deleted the app.config file from the Watch project and modified the app.config file for the PTU Application and 
 *                                                      Common projects.
 *                                                  d.  Deleted Settings.config from the Common project.
 *                                                  e.  Modified the Resources and Settings associated with the PTU Application project.
 * 
 *                                          3.  Modified the Resources associated with the PTU Apllication project.
 *                                          4.  Updated PTUDLL32.dll.
 *                                          
 *                                          
 */
#endregion - [Revision History 6.3] -

#region - [Revision History 6.4] -
/* 
 *  Date        Version     Author          Comments
 *  10/18/10    6.4         K.McD           1.  Removed the link to this file for all dynamic link library projects so that they can be individually managed.
 * 
 *                                          2.  All forms, usercontrols and communication classes moved to the appropriate sub-directories throughout the solution.
 * 
 *                                          3.  Re-designed the communication interface so that the vehicle control unit communication classes for each sub-system can 
 *                                              be developed independently.
 * 
 *  10/22/10    6.4.0.1     K.McD           1.  Updated to use Watch.dll version 1.3.
 *                                          2.  Updated to use Common.dll version 1.4.
 * 
 *  11/02/10    6.4.0.2     K.McD           1.  Added support for the event menu interface.
 * 
 *  11/16/10    6.4.0.3     K.McD           1.  Included the event log sub-system dynamic link library - Event.dll.
 *                                          2.  Modifications resulting from the inclusion of the event log sub-system.
 * 
 *  11/17/10    6.4.0.4     K.McD           1.  Replaced the WorksetManager static class with the combination of: (a) the WorksetCollection class, a non-static class used 
 *                                              to manage the workset collections associated with individual sub-systems and (b) the Workset class, a static class
 *                                              containing WorksetCollection classes for each sub-system.
 * 
 *                                          2.  Bug fix - SNCR001.58. Now plots negative static values correctly.
 *                                          3.  Bug fix - SNCR001.16. Modified the form used to manage the worksets so that it is scrollable.
 * 
 *  11/26/10    6.4.0.5     K.McD           1.  Included support to allow the user to configure fault log worksets.
 *                                          2.  Replaced the 'Configure/Worksets' menu option with the 'Configure/Worksets/Watch Window' and 'Configure/Worksets/Fault Log'
 *                                              menu options.
 *                                          3.  Included support to allow the user to save the downloaded fault log to disk.
 *                                          4.  Included support to allow the user to view saved fault logs.
 *                                          5.  Included support to allow the user to save the current event log to disk in XML format.
 *                                          6.  Included support to allow the user to modify the fault log data stream parameters.
 *                                          7.  Bug fix - SNCR001.60. The Copy context menu option associate with the form used to manage worksets now creates a fully
 *                                              independent copy of the workset i.e. all arrays are copied using the Array.Copy() method and each element of every list is
 *                                              manually copied.
 * 
 *                                          8.  SNCR001.29. Added support for a security tag associated with each workset to prevent accidental deletion of worksets.
 * 
 *  12/08/10    6.4.0.6     K.McD           1.  Included form to allow the user to view saved event logs.
 * 
 *                                          2.  Bug fix - SNCR001.19. Modified Common.VariableControl.cs to include a static ReadOnly property which forces all user 
 *                                              controls derived from this class to be read-only. This allows all watch variable user controls to be easily set to 
 *                                              read-only while in Replay mode.
 * 
 *                                          3.  Bug fix - SNCR001.47. Ensure that a check on the version number of the data dictionary is carried out when connecting to 
 *                                              the target. If a mismatch is detected the PTU will attempt to load the correct data dictionary.
 * 
 *                                          4.  Modified the Common.WorksetCollection class so that any previously defined worksets will continue to work if any watch 
 *                                              variables are added to or removed from the data dictionary, provided that all of the watch variables defined in the 
 *                                              workset are still contained within the updated data dictionary.
 * 
 *                                          5.  Bug fix - SNCR001.51/56. Modified the FormWatchView class so that all worksets attached to the Items property of the 
 *                                              ToolStripComboBox control are updated if any of the worksets are modified.
 * 
 *                                          6.  Included support for dynamic update of the event log display if new events are detected while the event log is on display.
 * 
 *                                          7.  Bug fix SNCR001.66. Modified the Event.FormSetupStream class to save the current event log to disk and then clear it down 
 *                                              before updating the data stream parameters. If this is not carried out, any attempt to view a data stream generated before 
 *                                              the parameters were modified will display incorrect results as the values will correspond to the watch identifiers
 *                                              associated with the previous parameters, however, they will be interpreted as being the values associated with the updated
 *                                              parameters.
 * 
 *                                          8.  SNCR001.72. Included the facility to load multiple files when viewing saved event log files.
 * 
 *  01/10/11    6.4.1       K.McD           1.  Minor bug fixes in preparation for release:
 *                                                  (a) Modified the constructor of Common.FormWatchReplay to initialize the timer used to play back the recorded data.
 *                                                  (b) Modified the PathRelativePTUApplicationData resource in Common.dll as a temporary fix so that the default directory for PTU application data 
 *                                                      is 'ProgramData/Bombardier/Portable Test Unit'.
 *                                                  (c) Modified the GetStream() method of the Event.CommunicationEvent class to set the data stream number to be equal to 
 *                                                      the event record index + 1.
 * 
 *  01/12/11    6.4.2       K.McD           1.  Bug fix SNCR001.84. Added the second parameter to all calls to the System.Threading.WaitHandle.WaitOne() method, as advised by 
 *                                              the following blog entries: 
 *                                              (1) http://www.mikeplate.com/missingmethodexception-and-waitone/ and (2) 
 *                                              (2) http://blog.darrenstokes.com/2009/03/30/watch-out-for-those-waitone-overloads-when-you-need-backwards-compatibility
 * 
 *  01/14/11    6.4.3       K.McD           1.  Bug fix SNCR001.85. There appears to be a bug in the CheckFaultLogger() method of the PTUDLL322 dynamic link library in
 *                                              that the event count value returned from the call can sometimes increment by the number of existing entries in the event
 *                                              log every time that the call is made. This problem is sometimes cleared by calling the ClearEvent() method the same
 *                                              library, however, in order to circumvent the problem the design of the dynamic update of the event log was modified to use
 *                                              the event index value that is returned from the call instead as this value is correct. 
 * 
 *                                              The bug fix is implemented by adding an EventIndex property to the Event.FormViewEventLog class in conjunction with a 
 *                                              modification to the Run() method of the Event.ThreadPollEvent class.
 * 
 *  01/17/11    6.4.4       K.McD           1.  Bug fix - SNCR001.86. Under the previous implementation, if a workset contained less than WatchSize watch variables, 
 *                                              there was a possibility that the display values associated with one or more watch variables would, under certain 
 *                                              circumstances, be incorrect. This bug was caused because not all elements of the m_WatchElements array were updated in 
 *                                              the SetWatchElements() method if the new workset being displayed contained less than WatchSize entries.
 * 
 *                                              This problem was addressed by making the following modifications:
 *                                              (a) Used the Array.Copy() method to copy the watchElementList parameter to the initialized watchElements array in the 
 *                                                  Watch.CommunicationWatch.SetWatchElements() method. This ensures that all WatchSize watch elements are updated when 
 *                                                  making a call to the PTUDLL32.SetWatchElements() method. The watch identifiers of those watch elements that are not 
 *                                                  used are set to 0.
 * 
 *                                              (b) Modified the for loop within the SetWatchElements() method of the Watch.CommunicationWatch class to update all 
 *                                                  elements of the m_WatchElements array.
 * 
 *                                          2.  Removed the SimulateCommunicationLink conditional compilation.
 * 
 *  01/18/11    6.4.5       K.McD           1.  Bug fix SNCR001.79 - In previous releases, the contents of the data streams associated with some of the events 
 *                                              occasionally had all watch variable values set to 0. This bug has been addressed by correcting the stream number value 
 *                                              passed to the call to the PTUDLL32.GetStream() method in Event.CommunicationEvent.GetStream(). 
 * 
 *                                              The data stream number associated with each event record is determined as follows. Starting from the oldest event record, 
 *                                              whenever an event record is found that has an associated data stream, the stream number for the event record is incremented 
 *                                              from a starting value of 0. Any event record that does not have an associated data stream will have the stream number set 
 *                                              to CommonConstants.NotUsed.
 * 
 *                                          2.  Bug fix - SNCR001.87. When attempting to download a datastream using the latest version of the CTA embedded software 
 *                                              a System.ArgumentOutOfRangeException was thrown. 
 * 
 *                                              The bug fix was implemented by modifying the value of the DefaultWatchSizeFaultLog constant in the Parameter class to 20 
 *                                              to cater for the increased number of data stream parameters associated with the Propulsion, Engineering and Snapshot 
 *                                              event logs on the CTA project.
 * 
 *  01/26/11    6.4.6       K.McD           1.  Request (B-03005) - SNCR001.48. Modified the design so that the: (1) fault log data streams, (2) application data
 *                                              directory, (3) WatchSize and (4) VCU sample interval are configured using the data dictionary.
 * 
 *  01/26/11    6.4.6.1     K.McD           1.  Bug fix SNCR001.97. Modified the ConvertToWorkset() method of the CommunicationEvent class to use the 
 *                                              Parameter.WatchSizeFaultLogMax constant to specify the entryCountMax parameter when instantiating the Workset_t structure.
 * 
 *  01/27/11    6.4.7       K.McD           1.  Request - SNCR001.68/57. Modified the Event.FormViewEventLog class to save, clear and reset ALL event logs rather than 
 *                                              just the current event log when the user presses the appropriate function keys on the event log display.
 * 
 *                                          2.  Request - SNCR001.72. Included a feature whereby the user can save the event logs to an individual event log XML 
 *                                              file or append the events to an existing file.
 * 
 *                                          3.  Bug fix - SNCR001.88/89. Modified the ShowDataStreamFile() method of the Common.MenuInterface class to return a flag 
 *                                              to indicate whether the selected data stream file is valid and used this in the overridden ShowDataStreamFile() 
 *                                              methods in the child Event.MenuInterfaceEvent and Watch.MenuInterface.Watch classes such that the data stream file 
 *                                              is only displayed if the selected file is valid.
 * 
 *                                          4.  Modified the Common.CommunicationParent class to include a mutex to ensure that multiple processes can't access the 
 *                                              serial port at the same time.
 * 
 *                                          5.  Request - SNCR001.73. Created the Event.MenuInterfaceEvent.ImportEventLogFiles() method to allow the user 
 *                                              to use the Windows multi-select feature to load a number of event log files.
 * 
 *                                          6.  Modified the form used to display and record watch variables, Watch.FormWatchView, so that: (a) the escape key is no longer
 *                                              disabled while in record mode (b) access to the main menu options is disabled while in record mode and (c) if the 
 *                                              user exits the form or the application while in record mode the recording will be cleanly terminated and the existing 
 *                                              data will be saved to disk before the application is closed.
 * 
 *  02/04/11    6.4.7.1     K.McD           1.  Removed those menu options that are not required for the CTA project.
 * 
 *  02/14/11    6.4.7.2     K.McD           1.  Added support for debug mode; this allows users to log the parameter values associated with all calls to the methods 
 *                                              associated with the PTUDLL32 dynamic link library.
 * 
 *                                          2.  Bug fix - SNCR001.100/53. Added the UpdateMenu() virtual method to the CommonFormPTU class; this re-applies any form 
 *                                              specific changes to the main menu that were applied after the form was instantiated. Used following a change to 
 *                                              the security level of the user and this action will 'reset' the main menu to its initial state.
 * 
 *                                          3.  Bug fix - SNCR001.40. Modified the FormShowSystemInformation class so that the button used to show the form which allows 
 *                                              the user to configure the real time clock is only enabled if the user is logged into engineering mode.
 * 
 *                                          4.  Modified the MdiPTU.Support class to include code within the UpdateMenu() method to call the UpdateMenu() function 
 *                                              of any child classes that are open. This ensures that any form specific changes to the menu system are updated whenever 
 *                                              the security level of the user is changed.
 * 
 *  02/21/11    6.4.7.3     K.McD           1.  Modified the Watch library such that the name of the workset is included in the default filename of saved watch files.
 *                                          2.  Modified the Event library such that it no longer requires the sample interval value when configuring the fault log 
 *                                              worksets..
 *                                          3.  Modified the Event library such that it no longer clears the current log before downloading new data stream parameters.
 * 
 *  02/28/11    6.4.7.4     K.McD           1.  Minor reorganization of the menu system.
 * 
 *                                          2.  Removed the button that allows the user to modify the VCU date and time from the FormShowSystemInformation class.
 * 
 *                                          3.  Modified the DoubleClick event handler of those user controls derived from the Common.WatchControl class such that if 
 *                                              the user control is not write enabled and the user attempts to modify the current value, the information message reported 
 *                                              to the user will differentiate between a read-only watch variable and one where the current security level is insufficient 
 *                                              to allow the user to modify the value.
 * 
 *                                          4.  Modified the Common.FormDataStreamReplay class to display the appropriate text and image on the escape key ToolStrip button
 *                                              and return to the correct form when the escape key is pressed.
 * 
 *                                          5.  Included support to manage the Visible property of the main application progress bar as this is no longer permanently on 
 *                                              display.
 * 
 *                                          6.  Added an event handler for the context menu Opened event to the Common.FormWorksetManager class so that the Enabled 
 *                                              property of each context menu is set or cleared depending upon the selected context menu and current security level.
 *                                   
 *                                          7.  Modified the event handler for the 'Set As Default' context menu option associated with the Common.FormWorksetManager class 
 *                                              to ensure that the security level of the workset that has been set is assigned to the highest security level.
 * 
 *                                          8.  Bug fix SNCR001.105. Modified the event handler associated with the F5-Edit function key of the Watch.FormViewWatch class 
 *                                              to check that the user has sufficient privileges before showing the dialog box that allows the user to edit the current 
 *                                              workset.
 * 
 *                                          9.  Bug fix SNCR001.105. Added the UpdateMenu() override method to the FormViewWatch class to process any form specific changes
 *                                              to the menu system or function keys resulting from a change in the security level of the user.
 * 
 *                                          10. Modified the Watch.FormViewWatch class such that the progress bar is only displayed when data is being recorded.
 * 
 *                                          11. Modified the LoadWorkset() method of the Watch.FormViewWatch class such that the F5-Edit function key is only enabled 
 *                                              if the security level allows the user to modify the current workset.
 * 
 *                                          12. Bug fix. Modified the CheckFaultlogger() method of the Event.CommunicationEvent class such that the newIndex local variable
 *                                              used in the call to the PTUDLL32.CheckFaultLogger() method in not initilalized to zero.
 * 
 *                                          13. Modified the InitializeEventLogs() method of the Event.MenuInterfaceEvent class to ask for user confirmation before 
 *                                              proceeding.
 * 
 *                                          14. Bug fix. Improved the layout of the event log display and fixed the AccessViolation exception thrown following the 
 *                                              download of a data stream.
 * 
 *                                          15. Added event handlers for the Shown events raised by the Event.FormViewFaultLog and Event.FormOpenFaultLog classes to 
 *                                              modify the image and text of the escape key ToolStrip button if the forms were called from the form used to display the 
 *                                              event log rather than the main application window as under these circumstances the escape key will return the user to 
 *                                              that form rather than home.
 * 
 *                                          16. Modified the event handler for the Save function key Click event of the Event.FormSetupStream class to ask for confirmation
 *                                              before updating a saved workset.
 * 
 *                                          17. Modified the Event.FormSetupStream class such that the combo box control is now used to both display the name of the 
 *                                              workset and to select a new workset.
 * 
 *  03/01/11    6.4.7.5     K.McD           1.  Corrected the parameters passed to the DeriveName() method in the Common.General.GetFullyQualifiedFaulLogFilename()
 *                                              methods.
 *                                          
 *                                          2.  Modified the event handler for the 'F5-Trip' function key 'Click' event in the Common.FormDataStreamReplay class such that,
 *                                              if an entry corresponding to the time of the trip is not found, the 'F5-Trip' function key is unchecked and the cursor is 
 *                                              set to the default cursor.
 * 
 *  03/02/11    6.4.7.6     K.McD       1.  Modified the Event.FormViewEventLog and Event.FormOpenEventLog classes such that the EventRecordList property is sorted by 
 *                                          descending date/time order, i.e. most recent event first, so that the first row of the DataGridView control is selected 
 *                                          when the forms are first shown.
 * 
 *                                      2.  Modified the DataGridView SortCompare() event handler in the Event.FormViewEventLog class such that the value stored in 
 *                                          the Log column of the DataGridView control is ignored when determining the sort order.
 * 
 *  03/21/11    6.4.7.7     K.McD       1.  Modified the design of the Event.FormViewEventLog class such that when displaying the event variables associated with the 
 *                                          selected event only those event variable user controls which are specific to the selected event are re-configured. 
 * 
 *                                      2.  Modified the philosopy used to find the selected event in the list of loaded event records for the FormOpenEventLog class. 
 *                                          The event record list is now sorted by DateTime order, oldest event first and the event index of each record is updated so 
 *                                          that it is unique and matches the sort order of the list i.e. 0 will represent the oldest event, 1 the following event etc. 
 *                                          When the user selects a row from the DataGridView control, the index of the event corresponding to the selected row is 
 *                                          determined and the list of event records is searched for a match.
 * 
 *                                      3.  Modified the WriteDataSetToXml() method of the Common.DataDictionary class to include try/catch blocks for those tables 
 *                                          that are not automatically created by the data dictionary builder utility in case those tables have not been manually 
 *                                          added to the data dictionary.
 * 
 *                                      4.  Added support for the Security table of the Access 'E1' database and modified the Common.Security class to use the security 
 *                                          configuration parameters defined in this table.
 * 
 *                                      5.  Added the WinHlp32 class to support the Windows help engine.
 * 
 *                                      6.  Modified the LoadDataDictionary() method of the Common.General class such that the same call to the FileInfo.CopyTo() method 
 *                                          is made nomatter whether the destination file exists or not.
 * 
 *                                      7.  Modified the AppendEventRecordList() method of the FileHandling class to include a match for the event index in the 
 *                                          Find() method call used when trying to check for duplicate records. This was added to cater for the case where multiple 
 *                                          events with the same description and date/time stamp are generated by the VCU, but with different event variable information 
 *                                          e.g. in the case of the 'Inverter Fault' event.
 * 
 *                                      8.  Modified the watch and event user controls to:
 *                                              (a) include support for diagnostic help information.
 *                                              (b) ensure that an exception is not thrown and to correctly handle the case where an invalid watch identifier is specified 
 *                                                  for the Identifier property of the control.
 * 
 *                                      9.  Modified the FormShowFlags class to support the FormShowFlagsEvent child class and created the FormShowFlagsEvent child class 
 *                                          to display the state of individual bits within an event bitmask variable.
 * 
 *                                      10. Added the CopyTo() method to the EventVariable class to allow the selected event variable to be copied to a newly instantiated 
 *                                          event variable.
 * 
 *                                      11. Bug fix - SNCR001.114. Modified the CreateEventVariableList() of the EventTable class to create a list of newly instantiated 
 *                                          event variables to store the event variable values associated with the the specified event identifier.
 * 
 *                                      12  Added the ConfigureRealTimeClock() method to the MenuInterfaceApplication class.
 * 
 *                                      13. Modified the design of the FormConfigureDateTime class such that the form is now self sufficient i.e. it is no longer called 
 *                                          from the FormShowSystemInformation class.
 * 
 *                                      14. Modified the design of the FormLogin class such that user simply enters the password associated with the required security 
 *                                          level to log into the account i.e. the required security level need not be selected from a drop down list.
 * 
 *                                      15.  Removed the feature to allow the user to configure the VCU date and time from within the form used to show the system
 *                                          information.
 * 
 *  03/28/11    6.4.7.8     K.McD       1.  Bug fix SNCR001.112. Modified the application to use the old identifier field of the data dictionary rather 
 *                                          than the watch identifier field to define the watch variables that are to be included in each workset as these remain 
 *                                          consistent following a data dictionary update.
 * 
 *                                      2.  Added the Common.WatchVariableTableByOldIdentifier class to support access to the watch variables defined in the data 
 *                                          dictionary using the old identifier field of the watch variables.
 * 
 *  03/31/11    6.4.7.9     K.McD       1.  SNCR001.025. Added support for the enumerator user control.
 *                                      2.  SNCR001.062. Included support to write the y axis values and the cursor value in the format specified by the FORMATSTRING 
 *                                          field of the data dictionary.
 * 
 *  04/06/11    6.4.8       K.McD       1.  Added support to allow the user to configure the event flags associated with the current log.
 *                                      2.  Added support to show the event history.
 *                                      3.  SNCR001.115. Modified the Common.FormShowHeaderInformation class such that the username field displays the saved username 
 *                                          rather than the current Windows username.                                          
 */
#endregion - [Revision History 6.4] -

#region - [Revision History 6.5] -
/*
 *  04/08/11    6.5.        K.McD       1.  Minor modifications to support the screen capture feature.
 * 
 *  04/21/11    6.5.1       K.McD       1.  SNCR001.126. Added support to configure the chart recorder.
 *                                      2.  Added support to set the mode of the chart recorder.
 *                                      3.  SNCR001.119. Corrected the data stream available graphic displayed on the form used to view the event logs such that 
 *                                          if a data stream is not available for a specific event and that event has been selected then the graphic is not shown as 
 *                                          a white box.
 */
#endregion - [Revision History 6.5] -

#region - [Revision History 6.6] -
/*                                         
 *  05/23/11    6.6         K.McD       1.  Migrated to Visual Studio 2010.
 *                                      2.  Significant modifications to allow the user to define, store and download the chart recorder scaling information associated 
 *                                          with each chart recorder workset.
 *                                      3.  Added support to allow the user to define the chart recorder scaling information in the chart recorder worksets
 *                                      4.  Remove support for defining and displaying a default workset on fault log parameter worksets.
 *                                      
 *  05/24/11    6.6.1       K.McD       1.  Bug fix associated with assignment of chart recorder scaling values.
 * 
 *  05/26/11    6.6.2       K.McD       1.  Corrected the SetChartScale() method in the Common.CommunicationParent class to download the specified parameter values 
 *                                          rather than the upper and lower limits defined in the data dictionary.  
 *                                      2.  Bug fix. Ensured that the ListBox controls associated with the lower and upper Y axis values of the chart recorder workset 
 *                                          were cleared when the user opted to create a new workset.
 *                                      3.  Bug fix. Corrected the Common.FormConfigure.CompareWorkset() method such that the correct chart recorder lower and upper 
 *                                          Y axis limits were compared. 
 *                                      4.  Ensured the the forms used to configure the chart recorder and define the chart recorder worksets display the string 
 *                                          representation of 'not defined' rather than 'NaN' if the values for the lower and upper limits of the Y axis of the 
 *                                          chart recorder are not defined.
 *                                      5   Added the infrastructure for the self test sub-system.
 *                                      
 *  06/22/11    6.6.3       K.McD       1.  Updated the Watch dynamic link library to version 1.21.3. 
 *                                              a.  SNCR001.141. Removed the feature that allows the user to modify the chart mode from the form that is used to 
 *                                                  configure the chart recorder, this can now only be carried out from the main menu.
 *                                      
 *                                              b.  SNCR001.142. Modified the design of the form that is used to configure the chart recorder such that the default 
 *                                                  chart recorder workset is presented for download to the VCU when the form is first shown.
 *                                          
 *                                      2.  Updated the Common dynamic link library to version 1.16.3.
 *                                              a.  Added the ShowHelpWindow() method to the WinHlp32 class. This method shows the specified help information using 
 *                                                  the standard Windows help window.     
 *                                              b.  Added the self test communication methods to the DebugMode class.
 *                                              c.  Modified the FormConfigure class to display the name of the selected workset in the 'TabPage' header.
 *                                              d.  Modified the FormWorksetDefine class to support the changes to the chart recorder configuration discussed in the 
 *                                                  May 2011 sprint review.
 *                                              e.  Added the communication interface associated with the self test methods.
 *                                              f.  Added support for the self test sub-system to the Lookup class.
 *                                              g.  Added the classes required to support access to the data dictionary tables associated with the self test sub-system.
 *                                              
 *                                      3.  Updated the SelfTest dynamic link library to version 1.1.
 *                                              a.  Added the PTUDLL32SelfTest and CommunicationSelfTest classes to support the PTUDLL32 communication methods associated 
 *                                                  with the self test sub-system.
 *                                          
 *                                              b.  Rationalized the directory structure to be consistent with the other sub-systems.
 *                                          
 *                                      4.  Included support for the self test menu interface.
 *                                      
 *  06/23/11    6.6.4       K.McD       1.  Updated the Common dynamic link library to version 1.16.4. Added debug mode information to the following communication 
 *                                          methods: GetChartMode(), SetChartMode(), SetChartIndex() and SetChartScale().
 *                                          
 *  06/23/11    6.6.5       K.McD       1.  Changed the project files and app.config files for all projects within the solution so that the target framework is 
 *                                          set to .NET Framework 2.0.
 *                                          
 *  06/24/11    6.6.7       K.McD       1.  Modified the AutoScaleMode property of the FormWorksetConfigureChartRecorder, FormWorksetDefineChartRecorder,
 *                                          FormWorksetDefineWatch, FormWorksetDefineFaultLog and FormConfigureFaultLogParameters classes to AutoScaleMode.Font 
 *                                          to ensure that the vertical scrollbar on the row header and Y axis limit ListBox controls isn't activated if the font 
 *                                          is changed.
 *                                          
 *  06/30/11    6.6.8       K.McD       1.  Target framework updated to .NET Framework 4.0.
 *                                      2.  Included support for hexadecimal chart scale values.
 *                                      3.  Set the BackColor property of a number of Label controls to Transparent.
 *                                      4.  Minor adjustments to the Size and Location properties of a number of controls associated with the Common.FormWorksetDefine 
 *                                          class and those classes derived from this class.
 *                                          
 *  07/11/11    6.6.9       K.McD       1.  Major changes to the SelfTest subsystem, included support for logic and passive tests.
 *  
 *  07/13/11    6.6.9.1     K.McD       1.  Modified the function keys to take into account the redefinition of off-line mode and the addition of diagnostic mode 
 *                                          as discussed in the June sprint review.
 *                                          
 *  07/25/11    6.6.9.2     K.McD       1.  Updated Common.dll to version 1.16.8.1.
 *                                              (a)     Added support for off-line mode.
 *                                              (b)     Added the user controls to support self test variables.
 *                                              (c)     Modified the WinHlp32 class.
 *                                              
 *                                      2.  Updated Event.dll to version 1.15.8.0 to include support for off-line mode.
 *                                      3.  Updated SelfTest.dll to version 1.3.2.
 *                                              (a) Added support for off-line mode.
 *                                              (b) Included support for interactive tests.
 *                                                      (a) Modified the GetSelfTestResult() definition in the PTUDLL32 class and implementation in the 
 *                                                          CommunicationSelfTest class to pass the self test variable values as a byte array rather than an 
 *                                                          array of InteractiveResults_t structures.
 *                                                      (b) Major modifications to the FormViewTestResults class.
 *                                              (c) Modified the FormTestListDefine class to display the help window within the bounds of the GroupBox controls.
 *                                      4.  Updated Watch.dll to version 1.21.7.
 *                                              (a) Added support for off-line mode.
 *                                      5.  Implemented the following changes to the MdiPTU class in an attempt to resolve the problem of returning focus 
 *                                          to the function keys after the help window for the current test case has been displayed during the 
 *                                          interactive tests.
 *                                              (a) The TabStop property of the ToolStripFunctionKeys control was asserted.
 *                                              (b) The DoubleBuffered property of the form was asserted.
 *                                      6.  Modified the MdiPTU class to include support for off-line mode.
 *                                              (a) Modified the CommunicationInterface property to inherit from ICommunicationParent.
 *                                              (b) Added the m_TSBDiagnostics_Click() event handler.
 *                                              (c) Modified the m_TSBOffline_Click() event handler.
 *                                              
 *  07/25/11    6.6.10      K.McD       1.  Updated Common.dll to version 1.16.9.1.
 *                                              (a) Added support for the Universal Resource Indicator (URI) table of the data dictionary. This table defines valid 
 *                                                  tcp/ip addresses in URI format and is required for ethernet communication.
 *                                              (b) Modified the signature of the ShowHelpWindow() method in the WinHlp32 class to allow the programmer to specify the 
 *                                                  hWndInsertAfter parameter.
 *                                                  
 *                                      2.  Updated SelfTest.dll to version 1.3.3.
 *                                              (a) Added the m_PanelHelpWindowTestList and m_PanelHelpWindowAvailable panels to the FormTestListDefine class to 
 *                                                  help position the Windows help window.
 *                                              (b) Entered the FormViewTestResults class into TortoiseSVN for security reasons, however, the form is not yet complete.
 *                                              
 *  07/27/11    6.6.11      K.McD       1.  Updated SelfTest.dll to version 1.3.4.
 *                                              (a) Replaced the ToolStripComboBox control with a ComboBox control.
 *                                              (b) Added support to allow the user to view individual bits of the bitmask self test variables.
 *                                              (c) Now uses a GroupBox control to contain the interactive test information.
 *                                              (d) Initialized the loop count to 1 every time the loop forever CheckBox control is selected.
 *                                              (e) Added screen capture support.
 *                                              
 *                                      2.  Updated Event.dll to version 1.15.10.
 *                                              (a) SNCR001.148. Bug fix associated with the CTA site visit on 15th July 2011. An exception was thrown when the user 
 *                                                  selected an event from the downloaded event log. Further investigation showed that the exception was thrown because 
 *                                                  the previously selected event had no event secific event variables associated with it.
 *                                                  
 *                                              (b) SNCR001.135. Bug fix associated with the event log displaying the incorrect number of events in the current list 
 *                                                  if the list is empty. Modified the m_ToolStripComboBox1_SelectedIndexChanged() method to update the label that 
 *                                                  shows the number of events in the list to display zero if the list is empty.
 *                                                  
 *                                              (c) Modified the class constructor of the FormOpenEventLog class to initialize the member variable that stores the 
 *                                                  number of common event variables that are recorded for each event.
 *                                                  
 *                                      3.  Updated Common.dll to version 1.16.9.2 and GraphComponents.dll to version 1.4.1 for minor modifications.
 *                                      
 *                                      4.  Updated the PTUDLL32 project to VS2010, modified the configuration parameters to generate debug information and added the 
 *                                          project to the solution.
 *                                              
 *  08/04/11    6.6.12      K.McD       1.  Updated the PTUDLL32.dll file stored in the bin\debug an bin\Release directory to be the Release version of the 
 *                                          dll generated from the ptudll32 project.
 *                                          
 *                                      2.  SelfTest.dll updated to 1.3.5. Bug fix associated with the display of the self test variable values associated with the 
 *                                          interactive tests.
 *                                      
 *                                      3.  Watch.dll updated to 1.21.8. Minor modifications associated with the Pause and PauseFeedback properties of the 
 *                                          FormViewWatch and ThreadPollWatch classes.
 *                                          
 *  08/10/11    6.6.14      K.McD       1.  Performed the following modifications to the dialogue box layout:
 *                                              (a) Padding associated with PanelOuter set to 10,10,10,12 (Left, Top, Right, Bottom).
 *                                              (b) Margins around all controls except Buttons set to 3,3,3,3 (Left, Top, Right, Bottom).
 *                                              (c) Margins around Button controls set to 3,8,3,8 (Left, Top, Right, Bottom).
 *                                              (d) Location origin for GroupBox controls set to new Point(10, 23) i.e. GroupBox header height = 13.
 *                                              (e) Button Y value location set to be 11px below PanelOuter/TabControl control i.e. margin 3 + 8.
 *                                              (f) Form.Size(Y) set to Button.Location.Point(Y) + Button.Size(Height) + 11 + 28 i.e Form header height = 28.
 *                                              (g) TabControl header height = 26.
 *                                              (h) Ensured that the BackColor property of controls was set to Transparent. 
 *                                              
 *                                          These were applied to the following forms:
 *                                              (a) FormAddComments.
 *                                              (b) FormConfigure, Watch.FormConfigureChartRecorder, Event.FormChangeFaultLogParameters.
 *                                              (c) FormSetSecurityLevel.
 *                                              (d) FormShowHeaderInformation.
 *                                              (e) FormWorksetDefine, Watch.FormWorksetDefineWatch, Watch.FormWorksetDefineChartRecorder, Event.FormWorksetDefineFaultLog.
 *                                              (f) FormWorksetManager, Watch.FormWorksetManagerWatch, Watch.FormWorksetManagerChartRecorder,
 *                                                  Event.FormWorksetManagerFaultLog.
 *                                              (g) FormChangeWatch, FormChangeScalar, FormChangeEnumerator, FormChangeBitmask.
 *                                              (h) Watch.FormChangeChartScale.
 *                                              (i) SelfTest.FormTestListDefine.
 *                                              (j) FormGetStream.
 *                                              (k) PTUApplication.FormConfigureDateTime.
 *                                              (l) FormConfigurePassword.
 *                                              (m) FormHelpAbout.
 *                                              (n) FormLogin.
 *                                              (o) FormOptions.
 *                                              (p) FormSelectTarget.
 *                                              (q) FormShowSystemInformation.
 *                                              
 *  08/10/11    6.6.14.1    K.McD       1.  Included a call to the Update() method following any call to MessageBox.Show() that may be immediately followed by another 
 *                                          call to MessageBox.Show() in order to improve the user interface performance.
 *                                          
 *                                          These changes were applied to the following forms:
 *                                              (a) FormConfigure().
 *                                              (b) FormViewEventLog.
 *                                              (c) MenuInterfaceEvent.
 *                                              (d) FormSelectTarget.
 *                                              
 *  08/10/11    6.6.14.2    K.McD       1.  Ensured that the Button.Size property of all OK, Cancel and Apply buttons was set to (73,25) and that the Margin property 
 *                                          was set to (3,8,3,8).
 *                                          
 *  08/10/11    6.6.14.3    K.McD       1.  Replaced the ptudll32.dll files in the '\bin\Release' and '\bin\Debug' directories with the original one supplied by 
 *                                          John Paul until the ones generated by compiling the ptudll32 project can be verified.
 *                                          
 *                                      2.  Modified the TabIndex and TabStop properties on the PTUApplication.FormConfigurePassword and PTUApplication.FormOptions 
 *                                          classes.
 *                                      
 *                                      3.  Modified the Watch.FormChangeChartScale class to ensure that the value shown in the upper limit TextBox control was selected.
 *                                      
 *                                      4.  Modified MdiPTU.Support. Set the default BackColor property used by the WriteStatusMessage() method to be Transparent.
 *                                      
 *                                      5.  Modified the MdiPTU class. Added a call to the Update() method following a call to the MessageBox.Show() method whenever 
 *                                          the call may be immediately followed by another call to the MessageBox.Show() method. Used to improve the user interface 
 *                                          performance.
 *                                          
 *  08/10/11    6.6.14.4    K.McD       1.  Added support for offline mode to the Watch.FormConfigureChartRecorder class.
 *                                      2.  Minor adjustments to the Size and Location properties of the ToolStripStatusLabelProgressBar and ProgressBar controls.
 *                                      3.  Added a number of constants to use with the GetEmbeddedInformation() method of the CommunicationParentOffline class.
 *                                      4.  Introduced a delay before returning from the DownloadChartRecorderWorkset() method to simulate actual download in the 
 *                                          CommunicationParentOffline class.
 *                                         
 *  08/24/11    6.6.15      K.McD       1.  Modified a number of classes in the main application to improve the error handling if communication with the target 
 *                                          hardware fails. 
 *                                              (a) FormShowSystemInformation.
 *                                              (b) FormConfigureDateTime.
 *                                              
 *                                      2.  Added support for the KeyDown and KeyUp events and removed the diagnostic mode ToolStripButton control in the multiple 
 *                                          document interface form - MdiPTU. Also changed the images associated with the on-line and off-line buttons.
 *                                          
 *                                      3.  Modified each method of the CommunicationApplication class to use the standard pattern in order to improve error handling 
 *                                          in the event that communications to the target hardware is lost.
 *                                          
 *                                      4.  In order to improve the error handling in the event that communication to the target hardware is lost, the following 
 *                                          dynamic link library projects were updated:
 *                                              (a) Common.dll  - version 1.17.2.
 *                                              (b) Watch.dll   - version 1.21.11.
 *                                              (c) Event.dll   - version 1.15.15.
 *                                              
 *                                      5.  Updated the SelfTest.dll project to version 1.3.8. Modified the FormViewTestResults class as follows:
 *                                              (a) now disables the Execute button if in online mode; 
 *                                              (b) changed the image associated with the Execute button;
 *                                              (c) correctly sets the mode before exiting the self test screen;  
 *                                              (d) changed the width of the panel associated with the interactive tests and 
 *                                              (e) modified the GetSelfTestMessage() method to set the message to be Resources.EMResultFailed regardless of the 
 *                                                  state of the result parameter. 
 *                                    
 *                                      6.  Updated ptudll32.dll to version 1.2. Modified the communication port read and write timeouts and included a conditional 
 *                                          compilation symbol so that the WATCHSIZE definition is project dependent.
 *  
 *  09/21/11    6.6.16      Sean.D      1.	Changed code in MdiPTU.cs for m_TSBOnline_Click to check for a CommunicationException when trying to close the socket so 
 *											that we don't get repeated errors trying to close a port that's already closed.
 *										2.	Changed code in FormSelectTarget.cs for GetTargets to print appropriate status messages while attempting to connect to URIs.
 *										
 *  10/02/11    6.6.17      K.McD       1.  Updated Common.dll to version 1.17.5. 
 *                                              (a) SNCR 002.27. Included support to allow the user to define the plot layout of saved watch data.
 *                                              (b) SNCR 002.14. Included support to allow the user to edit the comments associated with saved watch data.
 *                                              (c) SNCR 002.13. Now ensures that the specified workset name does not exist before allowing the user to save the workset.
 *                                              (d) SNCR 002.17. Included support to allow the number of watch variables supported by the PTUDLL32 dynamic link library 
 *                                                  to be programmed based upon the WatchSize field of the ConfigurePTU table of the data dictionary.
 *                                      2.  Updated Watch.dll to version 1.21.12 to accommodate changes in Common.dll. No functional changes.
 *                                      3.  Updated SelfTest.dll to version 1.3.9. Modified the Remove() method of the form that is used to define which self tests are 
 *                                          to be executed to ensure that the correct occurrence of the item is removed from the ListBox control whenever the self test 
 *                                          is defined to run more than once.
 *                                      4.  Updated Event.dll to version 1.15.16 to accommodate changes in Common.dll. No functional changes.
 *                                      5.  Updated GraphComponents.dll to version 1.4.2. Improved the implementation of the FindY() method of the Plotter user control 
 *                                          to make use of the return value of the List.BinarySearch()to find the next lowest value without using recursive calls.
 *                                      6.  Updated PTUDLL32.dll to version 1.4. SNCR 002.19. Included support for ethernet communication.
 *                                      
 *  10/07/11    6.6.18      Sean.D/     1.  SNCR 002.34 - Bug associated with viewing system information in off-line mode.
 *                          K.McD               (a) Added CommunicationApplicationOffline class to project.
 *										        (b) Modified FormConfigurDateTime and FormShowSystemInformation constructor to invoke CommunicationApplicationOffline 
 *										            when in Offline mode.
 *
 *                                      2.  Updated GraphComponents.dll to version 1.4.4.  Minor modifications to the multi cursor implementation associated with 
 *                                          revision 1.4.3 of the dynamic link library.
 *                                          
 *                                      3.  Added support for the 'Help/PTU Help' menu option.
 *                                      
 *                                      4.  Added a LinkLabel control to the FormHelpAbout class to display the release notes. 
 *
 *                                      5.  Updated Common.dll to version 1.17.8.
 *										        (a) Reverted the PlotterControlLayout class back to the previous revision as the the current multiple cursor 
 *										            implementation is not compatible with the changes associated with revision 1.11.
 *
 *                                              (b) Added support for the Remove context menu option to the scalar, enumerator and bitmask plotter controls. This menu 
 *                                                  option removes the control from the current plot display.
 *
 *                                              (c) Modified the FormDataStreamPlot class.
 *                                                      (a) Included support to allow the user to remove individual plots from the display by means of the Remove context 
 *                                                          menu option associated with each of the plotter user controls.
 *
 *                                                      (b) Modified the Shown event handler to size the scalar, enumerator and bitmask plot controls based upon the 
 *                                                          height defined in the PlotterControlLayout class rather than upon the size of the form.
 *
 *                                              (d) Bug fix. Modified the FormWorksetDefine class to ensure that the status message label is only visible if the text in 
 *                                                  the text box matches an existing workset name.
 *
 *                                              (e) Bug fix. Modified the Serialize<T>() method of the FileHandling class to delete the file if it already exists.
 *                                              (f) Bug Fix - FormShowHeaderInformation class. SNCR002.12. - Improved the regular expression logic to find 
 *                                                  consecutive multiple carriage returns/line feeds.
 *                                                  
 *  10/10/11    6.6.19      K.McD       1.  As a result of problems trying to add/edit text resource strings, the 'Run Custom Tool' context menu option was run on 
 *                                          the Resources.resx files for all projects to ensure that the corresponding Resources.Designer.cs files were up to date. 
 *                                          This resolved the issue.
 *                                      2.  Deleted unused Settings files.
 *                                      3.  Bug fix associated with the error message 'Found conflicts between different versions of the same dependent assembly.
 *                                      
 *                                              (a) Removed and then added the System reference to the 'PTU Application' project. The problem was caused by the 
 *                                                  insertion of the <HintPath> item associated with the System reference in the 'PTU Appplication' project file, 
 *                                                  see below. The action outlined above removed the <HintPath> item and the error message cleared.
 *                                          
 *                                                      <ItemGroup>
 *                                                          <Reference Include="System">
 *                                                              <HintPath>..\..\..\..\..\..\..\Windows\Microsoft.NET\Framework\v2.0.50727\System.dll</HintPath>
 *                                                          </Reference>
 *                                                          <Reference Include="System.Data" />
 *                                                          <Reference Include="System.Drawing" />
 *                                                          <Reference Include="System.Windows.Forms" />
 *                                                          <Reference Include="System.XML" />
 *                                                      </ItemGroup>
 *                                                      
 *                                              (b) As a precaution the 'Edit/Find and Replace/Find in Files' menu option was executed to check that there were no 
 *                                                  more occurrences of the <HintPath> item in the Solution.
 *                                                  
 *                                              (c) During the investigation it was found that there were many unused references included in the project files. As 
 *                                                  an additional precaution these unused references were deleted.
 *                                                  
 *  10/13/11    6.6.20      K.McD       1.  Rationalized the Resources.resx files and renamed a number of resources for all projects in the solution. Note: No revision 
 *                                          history updates were carried out on individual files that were auto-updated as a result of the resource name changes.
 *                                          
 *                                      2.  Added icons to a number of form classes.
 *                                      
 *                                      3.  Updated the following dynamic link libraries.
 *                                      
 *                                              (a) Watch (Ver. 1.21.14).
 *                                              (b) SelfTest (Ver. 1.3.10).
 *                                              (c) Event - (Ver. 1.15.17).
 *                                              (d) GraphComponents (Ver. 1.4.5).
 *                                              (e) Common (Ver. 1.17.9).
 *                                              
 *  10/26/11    6.6.21      K.McD       1.  Modified the OpenDataDictionary() method of the MenuInterfaceApplication class - SNCR002.38. As the 'File/Select Data
 *                                          Dictionary' menu option is now permanently enabled this method must now check whether the current mode of the PTU and the
 *                                          security level of the user is appropriate for this action and, if so, will select a new data dictionary. If this action is not
 *                                          appropriate the PTU will report an error.
 *                                          
 *                                      2.  MdiPTU class (Ver. 1.12.6). Auto-modified as a result of enumerator name changes. Mode.Diagnostic renamed to 
 *                                          Mode.Configuration. Also removed the 'File/Open/Screen Capture' menu option and associated separator.
 *                                          
 *                                      3.  Mdi.Support.cs (Ver. 1.16.5). 
 *                                              1.  Auto-modified as a result of enumerator name changes. Mode.Diagnostic renamed to Mode.Configuration.
 *                                              2.  Modified the LoadWorksetCollection() method to take into account the fact that the signature of the 
 *                                                  WorksetCollection.Load() method has been modified and that error reporting of workset incompatibilities is now 
 *                                                  performed in the WorksetCollection.Update() method.
 *                                              3.  Modified the UpdateMenu() method to ensure that the 'Set Data Dictionary' menu option is always visible, see 
 *                                                  SNCR002.38.
 *                                                  
 *                                      4.  Updated Common.dll (Ver. 1.17.12).
 *                                              1.  WorksetCollection (Ver. 1.8.2). Now checks whether the WatchSize parameter of the ConfigurePTU table of the data 
 *                                                  dictionary has been modified since the last update of the recorded watch workset file and, if so, automatically 
 *                                                  updates the workset file to be compatible with the new WatchSize.
 *                                                  
 *                                              2.  WinHlp32 (Ver. 1.3.3). Bug fix SNCR002.42. Ensured that the ShowHelpWindow(), ShowPopup() and HideHelpWindow() 
 *                                                  methods don't throw an exception if the class hasn't been initialized.
 *                                                  
 *                                              3.  Bug fix - SNCR002.41. Ensured that the KeyPreview events are not sent to the main application window whenever a child 
 *                                                  window is on display. Also added checks to the event handlers associated with the ToolStripButton controls associated 
 *                                                  with all multiple document interface child class to ensure that the event handler code is ignored if the Enabled 
 *                                                  property of the control is not asserted.
 *                                                  
 *                                              4.  Added a status message panel to the FormworksetDefine class (Ver. 1.9.5). Now reports if the number of watch 
 *                                                  variables exceeds the WatchSize defined in the data dictionary.
 *                                                  
 *                                              5.  Modified the PlotterScalar (Ver. 1.4), PlotterEnumerator (Ver. 1.4) and PlotterBitmask (Ver. 1.4) user controls and 
 *                                                  the IPlotterWatch interface (Ver. 1.3). Added support to allow the user to select and remove multiple plots from 
 *                                                  the plot layout using the ‘Remove Selected Plot(s)’ context menu option.                                     
 * 
 *                                      5.  Updated Watch.dll (Ver. 1.21.16). Bug fix - SNCR002.41. Added checks to the event handlers associated with the ToolStripButton 
 *                                          controls associated with all multiple document interface child class to ensure that the event handler code is ignored if the 
 *                                          Enabled property of the control is not asserted.
 *                                                  
 *                                      6.  Updated Event.dll (Ver. 1.16.1). Bug fix - SNCR002.41. Added checks to the event handlers associated with the ToolStripButton 
 *                                          controls associated with all multiple document interface child class to ensure that the event handler code is ignored if the 
 *                                          Enabled property of the control is not asserted.
 *                                          
 *                                      7.  Updated SelfTest.dll (Ver. 1.3.11). Bug fix - SNCR002.41. Added checks to the F2 event handler associated with the FormTestView
 *                                          class to ensure that the event handler code is ignored if the Enabled property of the control is not asserted.
 *                                          
 *                                      8   Updated GraphComponents.dll (Ver. 1.4.6). Modified the Plotter class (Ver. 1.4.4).
 *                                              1.  Modified the design to enter the state which allows the user to modify the range as soon as the control has received 
 *                                                  focus rather than requiring the user to initiate this via a context menu option.
 *                                                  
 *                                              2.  Added the support for multiple user control selection. Added the SelectedControlList property, a generic list of
 *                                                  selected user controls.
 *                                                  
 *  11/02/11    6.6.22      K.McD       1.  Updated Common.dll (Ver. 1.17.13). Changed the CommunicationTypeEnum definition in the Parameter class to be: 
 *                                          0 - RS232, 1 - TCPIP, 2 - Both.
 *                                              							
 *	11/14/11	6.6.23		Sean.D		1.	Updated Common.dll and GraphComponents to handle SNCR002-03 (Memory leak when opening multiple watch files) which had recurred.
 *										2.	In FormOptions.cs, added code to m_BtnDefaultFont_Click to run Dispose on the old Font item to ensure it releases all
 *										    resources.
 *										
 *  11/23/11    6.6.24      K.McD       1.  Updated GraphComponents.dll (1.4.8).
 *                                              1.  Implemented the standard Dispose()/Cleanup() implementation for the Plotter, LogicAnalyzer and Graph classes.
 *                                              2.  Ensured that all event handler methods were detached and that the component designer variables were set to null on 
 *                                                  disposal.
 *                                                  
 *                                      2.  Updated Common.dll (1.17.16).
 *                                              1.  Modified the FormDataStreamPlot and FormDataDtreamReplay classes.
 *                                                      (a) Ensured that all event handler methods were detached and that the component designer variables were set to null
 *                                                          on disposal.
 *                                                      (b) Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after the 
 *                                                          Close() method had been called.
 *                                                  
 *                                              2. Ensured that all event handler methods were detached and that the component designer variables were set to null on 
 *                                                  disposal in the FormPTU class.
 *                                      
 *                                              3.  Implemented the standard Dispose()/Cleanup() implementation for the PlotterBitmask, PlotterEnumerator, 
 *                                                  PlotterEnumerator and the PlotterScalar classes.
 *                                          
 *                                              4.  Ensured that all event handler methods were detached on disposal of all VariableControl derived user controls.
 *                                      
 *                                              5.  Ensured that all event handler methods were detached and that the component designer variables were set to null on
 *                                                  disposal of the Plotter user controls.
 *                                      
 *                                      3.  Updated Watch.dll (1.21.18).
 *                                              (1) Modified the FormViewWatch class.
 *                                                      (a) Ensured that all event handler methods were detached.
 *                                                      (b) Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after the 
 *                                                          Close() method had been called.
 *                                                          
 *                                      4.  Updated Event.dll (1.16.2).
 *                                              (1) Modified the FormViewEventLog class.
 *                                                      (a) Ensured that all event handler methods were detached.
 *                                                      (b) Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after the 
 *                                                          Close() method had been called.
 *                                                          
 *                                      5.  Updated SelfTest.dll (1.3.12).
 *                                              (1) Modified the FormViewTestResults class.
 *                                                      (a) Ensured that all event handler methods were detached.
 *                                                      (b) Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after the 
 *                                                          Close() method had been called.
 *                                                          
 *  12/01/11    6.6.25      K.McD       1.  Updated GraphComponents.dll (1.4.9). Bug fix - Now keeps track of the zoomed start and stop times whenever the plot is
 *                                          successfully zoomed in and out to ensure that the status fields of the display are correctly updated.
 *                                          
 *                                      2.  Updated Common.dll (1.17.16).
 *                                      
 *                                              1.  Modified the PlotterScalar, PlotterEnumerator and PlotterBitmask classes to ensure that the plotter is selected if the 
 *                                                  user clicks on the Description or Units fields of the plotter.
 *                                                  
 *                                              2.  Modified FormDataStreamPlot. Bug fix to ensure that the plot layout is drawn correctly if the plot is zoomed in and 
 *                                                  the user restores the plot layout to the default state.
 *                                                  
 *                                              3.  Modified the PlotterControlLayout class to ensure that an exception isn't thrown if the plotterWatch parameter is 
 *                                                  null when passed to the SetAestheticProperties(), SetRangeProperties() and Reset() methods.
 *                                      
 *                                      3.  Updated Watch.dll (1.21.19). Bug fix to ensure that the Download and Save buttons are correctly displayed when configuring 
 *                                          the chart recorder worksets. 
 *                                      
 *                                      4.  Updated Event.dll (1.16.3). Bug fix to ensure that the Download and Save buttons are correctly displayed when configuring 
 *                                          the fault log parameters.
 *                                          
 *                                      5.  Updated SelfTest.dll (1.3.14). Bug fix to ensure that the Continue and Abort buttons are displayed when carrying out the 
 *                                          interactive tests.
 *                                      
 *  02/08/12    6.6.26      Sean.D      1.	Updated Event.dll (1.16.4). Bugfix for situations where no dataset is loaded for FormOpenFaultLog and FormViewFaultLog.
 *										2.	Updated Common.dll (1.17.19). Changed hashing implementation for passwords and updated stored hashes for default passwords.
 *										3.	Updated FormConfigurePassword.cs and FormLogin.cs to use the updated hashing implementation.
 *										
 *  06/04/12    6.6.26.1    Sean.D      All changes are being checked in as an interim measure rather than as an official release.
 *                                      
 *                                          1.  Within MdiPTU.Support.c:
 *                                                  1.  Add the VERSION_LENGTH constant to indicate the number of characters which are snipped off of the tail end of the
 *                                                      version string to generate the default filenames for the help and database files.
 *                                          2.  Modified the call to LoadHelpFile in LoadDictionary to use the new method signature.
 *                                          3.  Modifed LoadHelpFile to attempt to use the legacy naming method if the initial attempt to access the help file fails.
 *                                          4.  Updated Common.DLL (1.17.20).   Changed handling of removed watch variables to keep track of them and re-add them if the
 *                                              operation is cancelled.
 *                                          5.  Updated SelfTest.DLL (1.3.15).  Changed FormViewTestResults.cs to set us in Offline Mode if there is a communications
 *                                              error.
 */
#endregion - [Revision History 6.6] -

#region - [Revision History 6.7] -
/*                                  
 *  07/02/13    6.7    K.McD       Modifications associated with the NYCT - R188 Propulsion Car Control Unit Project. Added support for the WibuBox security device.
 *  
 *                                      1.  Modified MdiPTU.cs.
 *                                              1.  Added the IntervalMsWibuBoxUpdate constant.
 *                                              2.  Added the m_TimerWibuBox Timer.
 *                                              3.  Added the m_MenuInterfaceWibuKey reference to the MenuInterfaceWibuKey class.
 *                                              4.  Modified the Cleanup() method to include the WibuBox timer.
 *                                              5.  Modified the Cleanup() method to set the appropriate member variables to null.
 *                                              6.  Added the event handler for the WibuBox timer Tick event to check whether the WibuBox has been removed.
 *                                              7.  Modified the Shown event to check whether a WibuBox device is required for the current project and, if so, 
 *                                                  to instantiate a MenuInterfaceWibuKey class and to initialize the WibuBox timer.
 *                                      2.  Modified MdiPTU.Support.cs. Added support for the WibuBox security device. Modified UpdateMenu() to start the m_TimerWibuBox
 *                                          timer when the user logs into the Engineering or Factory account and to stop the timer when the PTU enters the Maintenance
 *                                          account. The Click event of this timer is used to check whether the WibuBox has been removed.
 *                                      3.  Modified the MenuInterfaceApplication class.
 *                                              1.  Modified the Login() method to check whether the project requires a WibuBox security device and, if so, checks that a
 *                                                  valid WibuBox is attached to the system before displaying the log-on screen. If a valid WibuBox is not found an error
 *                                                  message is displayed.
 *                                      4.  Added WibuKey.dll (1.0)
 *                                      5.  Updated Watch.dll (1.21.20). Unticked the 'Allow unsafe code' box for the Debug configuration on the Build tab page of the
 *                                          project properties form.
 *                                      6.  Updated SelfTest.dll (1.3.16).
 *                                              1.  Modified the constructor of the FormViewTetResults class such that the call to ExitSelfTestTask(), issued in case the
 *                                                  Self Test task on the hardware is already running, is not made if the project identifier corresponds to the
 *                                                  NYCT - R188 project i.e. the embedded software is running on a COM-C device. If this call is made while connected to
 *                                                  the COM-C hardware the PTU does not enter self test mode and hangs.
 *                                                  
 *                                              2.  Modified ProcessSTCountResult() and ProcessSTListResult() such that any value for the testResult parameter other than
 *                                                  ResultPassed (0) is treated as a fail.
 *                                      7.  Updated Common.dll (1.18)
 *                                              1.  Corrected the file name in the header of the DataDictionaryInformation.cs file.
 *                                              2.  Modified the XML tag associated with the DataDictionaryInformation_t structure.
 *                                              3.  Added the NewPara constant to the CommonConstants class.
 *                                              4.  Added the project information strings to the CommonConstants class.
 *                                              5.  Added support for the WibuBox parameters in the Parameter class.
 *                                              6.  Added the WibuBox_t structure definition (WibuBox.cs).
 *                                              7.  Corrected the Open() method in the DebugMode class. The call to StreamWriter() now specified the fully qualified
 *                                                  filename of the debug file.
 *                                              8.  Modified the DirectoryManager class.
 *                                                      1.  Modified the default application data directory to use the ApplicationData special folder rather than the 
 *                                                          CommonApplicationData folder as the root directory i.e. <User>\AppData\Roaming\Bombardier\Portable Test Unit.
 *                                          
 *                                                      2.  Set the root directory of the '\Workset Files' directory to be the default application directory rather than
 *                                                          the startup directory.
 *                                                      
 *                                              9.  Added support for a WibuBox security device to the MenuInterface class.
 *                                              
 *                                      8.  Modified the URL associated with FormHelpAbout to http://www.bombardier.com/en/transportation.html.
 *                                      9.  Unticked the 'Enable the Visual Studio hosting process' and the 'Enable unmanaged code debugging' boxes on the Debug tab page 
 *                                          of the 'PTU Application' Properties page for the Release configuration. Note: Both are enabled for the Debug configuration.
 *                                          
 * 07/16/13     6.7.1   K.McD       1.  Updated Event.dll to version 1.17.2.
 * 
 *                                          1.  Modified the FormViewFaultLog class - 1.17.1. Added a try/catch block to the F3 function key event handler to prevent an
 *                                              exception being thrown in the event that the number of records in m_HistoricDataManager.AllFrames is less that the
 *                                              TripIndex value of the current DataStream type. This can occur if the DataStreamType field of the Logs table in the
 *                                              .E1 database is set up incorrectly.
 * 
 *                                          2.  Modified the FormGetStream class - 1.17.2.
 *                                              1.  Renamed the constants:
 *                                                  a.  DataStreamTypeFaultLog to DataStreamTypeStandardFaultLog
 *                                                  b.  DurationSecDownloadFaultLog to DurationSecDownloadStandardFaultLog.
 *                                              
 *                                              2.  Modified the XML tags associated with the updated constants.
 *                                      
 *                                              3.  Modified the default case of the switch(dataStreamTypeIdentifier) statement in the constructor to set the 
 *                                                  progress bar parameters and thread complete timeout to be the same as those for the standard fault log in 
 *                                                  case additional datastream types are added to the DataStreamType table of the .E1 database.
 *                                          
 *                                                  This modification arose because a new datastream type was added to the DataStreamType table of the .E1 database 
 *                                                  to define the fault log datastream associated with the R188 project. The datastream associated with this project 
 *                                                  is identical to that of the standard fault log however the trip time index i.e. the sample corresponding to the 
 *                                                  time of the actual trip is 25 rather than 75. When trying to download the fault log with this new datastream 
 *                                                  type the download would terminate immediately and report that the PTU was unable to download the fault log as the 
 *                                                  download complete timeout had not been initialized.
 *                                                  
 *  07/16/13    6.7.2   K.McD       1.  Updated Common.dll to version 1.18.1. Modified the PathPTUConfigurationFiles property of the DirectoryManager class to use the
 *                                      '\Bombardier\Portable Test Unit\PTU Configuration Files' sub-directory of the directory that serves as the common repository for
 *                                      the current roaming user (User Application Data directory) rather than the startup directory.
 *                                      
 *  07/24/13    6.7.3   K.McD       1.  Updated Common.dll to version 1.18.2.
 *                                          1.  To allow the form that is used to configure the fault log parameters to display the number of watch variables associated
 *                                              with the current event log and to check that the configured number of watch variables does not exceed the permitted limit,
 *                                              the following changes were implemented:
 *                                              
 *                                                  1.  Modified the BuildDataStreamTypeParametersTable() method of the LogTable class to include the WatchVariablesMax
 *                                                      field from the DataStreamTypes table.
 *                                                  2.  Added the WatchVariablesMax variable to the DataStreamTypeParameters_t structure.
 *                                                  3.  Added the WatchVariablesMax field to the DataStreamTypes table of DataDictonary.mdb.
 *                                                  4.  Updated DataDictionary.xsd to reflect the modified structure of the DataStreamType table. Note: Automatic update of
 *                                                      DataDictionary.Designer.cs.
 *                                                  5.  Modified the FormWorksetDefine class to allow the EntryCountMax property to be updated by child classes.
 *                                                      
 *                                          2.  Added the WatchSizeFaultLog and SupportsMultipleDataStreamTypes properties to the Parameter class and replaced all
 *                                              references to the Parameter.WatchSizeFaultLogMax constant with references to the property throughout. Instead of the
 *                                              number of watch variables associated with the Fault Log worksets being constant it is now derived from the 
 *                                              value of the WatchVariablesMax field of the DataStreamTypes table in the data dictionary. The class also determines if
 *                                              the number of fault logs that can be recorded by each event log is fixed and, if so, asserts the
 *                                              SupportsMultipleDataStreamTypes flag.
 *                                              
 * 
 *                                  2.  Updated Event.dll to version 1.17.3.
 *                                          1.  Now passes the reference to the current event log to the form that is used to configure the fault log parameters so that a
 *                                              check can be made on the number of watch variables that are currently configured before attempting to download them to the
 *                                              VCU.
 *                                              
 *                                          2.  Now displays the number of watch variables that are associated with the current event log on the form used to configure the
 *                                              fault log parameters.
 *                                          
 *                                          3.  Modified the form that is used to configure the fault log worksets so that the allowed number of watch variables is
 *                                              displayed on the form if the number of watch variables recorded by every event log is fixed. On projects, such as CTA,
 *                                              where the number of watch variables that can be recorded by various event logs is not fixed e.g. the snapshot log can
 *                                              record 20 watch variables whereas the propulsion and engineering logs can only record 16, the form only displays the number
 *                                              of watch variables that have been configured so far. For such projects the PTU does not know whether the user is defining a
 *                                              workset for the snaphot, propulsion or engineering event log and it is the responsibility of the user to define the correct
 *                                              number of watch variables for the workset.
 *                                              
 *                                  3.  Updated Watch.dll to version 1.22.1.
 *                                          1.  Bug fix to enable the user to use Windows Terminal to download the embedded software to the COM-C card if the 'Flash
 *                                              Programming Enabled' flag is asserted. Asserting this flag terminates the normal master/slave PTU communications link at
 *                                              the COM-C COM port and puts the port into terminal mode.
 *                                      
 *                                                  1.  Modified the Exit() method of the FormViewWatch class to close the communication port and to set the mode to
 *                                                      configuration mode if there is a communication fault or the watchdog has tripped i.e. the port is locked.
 *                                          
 *                                                  2.  Modified the Run() method of the ThreadPollWatch class such that if a communication fault is detected, instead of
 *                                                      trying to re-establish communication, the thread just sleeps until the Dispose() method is called by the client.
 *                                                      While in this state, the thread is periodically awoken to update the watchdog counter so that the client can
 *                                                      determine whether the communication port has locked.
 *                                                      
 *                                  4.  Modified the Cleanup() method of the MdiPTU class to close the communication port.
 *                                  
 *                                  5.  Modified the icon associated with MdiPTE. Added the icon file to the Resources directory.
 *                                  
 *  07/31/13    6.7.4   K.McD       The following modifications were carried out as a result of problems associated with moving the 'PTU Configuration Files' directory to
 *                                  the user application data directory. The project identifier is now passed as a command line parameter to the PTU by the desktop
 *                                  shortcut so that, provided that a valid project XML data dictionary file is found in the configuration files directory, each PTU
 *                                  shortcut is associated with a specific project XML data dictionary with the filename '<project-identifier>.PTU Configuration.xml' 
 *                                  e.g. 'R8PR.PTU Configuration.xml'. This means that each PTU project installation will now operate independently whereas previously,
 *                                  each project used the same XML configuration file, 'PTU Configuration.xml'. 
 *                                  
 *                                  On earlier versions of the PTU where the configuration files directory was a sub-directory of the main project start-up directory,
 *                                  this was not an issue as each project had a unique XML data dictionary file. It was only after the configuration files directory was
 *                                  moved to the user application data directory and was common to all installed PTU applications that it became 
 *                                  an issue.
 *                                  
 *                                  1.  Updated Common.dll to version 1.18.3.
 *                                          1.  Modified the LoadDictionary() method of the General class to copy the source file to the file that is defined as the
 *                                              current XML data dictionary file rather than the default XML data dictionary file. The current XML data dictionary filename
 *                                              can specify either: (a) the default XML data dictionary file or (b) the project XML data dictionary file.
 *                                          
 *                                          2.  Added the FilenameDataDictionary property to the IMainWindow interface definition, this specifies the filename of the
 *                                              current XML data dictionary filename.
 *                                          
 *                                  2.  Modified the MdiPTE class.
 *                                          1.  Added the FilenameDataDictionary property in accordance with the modified IMainWindow interface definition.
 *                                          2.  Modified the constructor that is called if any command line arguments are passed to the PTU to update the
 *                                              FilenameDataDictionary property with the filename associated with the project XML data dictionary file if a valid project
 *                                              XML data dictionary file is found.
 *                                          3.  Modified the m_TSBOnline_Click() method to copy the source file to the file that is defined as the current XML data
 *                                              dictionary file rather than the default XML data dictionary file if a new XML data dictionary file is selected as a result
 *                                              of a mismatch between the embedded software version reference and the current data dictionary version reference.
 *                                              
 *  08/01/13    6.7.5   K.McD       1.  Updated Watch.dll to version 1.22.2. Modified the Run() method of the ThreadPollWatch class to close the communication port as soon
 *                                      as the communication fault flag is asserted.
 *                                  2.  Updated the Event.dll class to version 1.17.4. Modified the Run() method of the ThreadPollEvent class to close the communication
 *                                      port as soon as the communication fault flag is asserted so that it is consistent with the Watch.ThreadPollWatch class.
 *                                      
 *  08/02/13    6.7.6   K.McD       1.  Updated Common.dll to version 1.18.4. Increased the width, in pixels, of the name field associated with the watch control from 200
 *                                      to 230 in the WatchControlLayout class to prevent some of the longer variable names being displayed incorrectly. 
 *
 *                                  2.  Updated Event.dll to version 1.17.5. Increased the width of the date field of the event log DataGridView control associated with
 *                                      the FormViewEventLog class from 70 pixels to 75 pixels to allow a 4 digit year code to be displayed in Segoe UI 9 font.
 *                                      
 *  08/02/13    6.7.7   K.McD       1.  Modified the MdiPTU class to fix the bug Bug fix associated with the user message not changing when the font is modified using the
 *                                      Tools/Options menu. Modified the MdiPTU_FontChanged() method to update the Font property of the user message StatusStrip control.
 *                                      
 *  08/05/13    6.7.8   K.McD       1.  Replaced the 'Operating System' legend with 'O.S.' in the FormHelpAbout class to prevent an overlap when using larger fonts.
 *  
 *                                  2.  Replaced the single font setting, Font with FontXP and FontWin7 settings so that there are different font settings for Windows XP
 *                                      and Windows 7 operating systems. The default/initial font for the Windows XP setting was set to Tahoma, 8pt and the default/initial
 *                                      font for the Windows 7 setting was set to Segoe UI - 8.25pt. These fonts are the default fonts for Windows XP and Windows 7
 *                                      respectively.
 *                                      
 *                                  3.  Modified the FormSize setting from 800 x 600 px to 1010  x 720 px so that on first install, the form will be sized to take up the
 *                                      full screen for a display resolution of 1024 x 768.
 *                                      
 *                                  4.  Bug fix associated with an ArgumentException being thrown soon after the user had selected the default font. Removed the call to
 *                                      dispose of the m_Font member variable from the m_BtnDefaultFont_Click() method of the FormOptions class.
 *                                          
 *                                  5.  Modified the FormOptions class to support separate fonts for Windows XP and Windows 7.
 *                                          1.  Created the static GetOSFont() method to get the current font or the default font, depending upon the state of the
 *                                              requestDefaultFont flag, asssociated with the operating system.
 *                                          2.  Created the static SetOSFont() method to set the appropriate font setting, FontXP / FontWin7, depending upon the operating
 *                                              system.
 *                                          3.  Modified the constructor to use the GetOSFont() method to retrieve the current font rather than retrieving it directly
 *                                              from the Font setting.
 *                                          4.  Modified the m_BtnDefaultFont_Click() method to use the GetOSFont() method to retrieve the default font.
 *                                          5.  Modified the m_BtnOK_Click() method to use the SetOSFont() method to set the appropriate font setting rather than writing
 *                                              it directly to the Font setting.
 *                                          6.  Added the MajorBuildXP and MajorBuildWin7 constants to define the values of the Major component of the version number for
 *                                              the Window XP and Window 7 operating systems.
 *                                  
 *                                  6.  When updating the Font property of the Parameter class in the InitializePTU() method of the MdiPTU class, instead of getting the
 *                                      value directly from the Font setting, now calls the FormOptions.GetOSFont() static method to get the font setting (FontXP/FontWin7)
 *                                      that is appropriate to the operating system.
 *                                  
 *                                  7.  Updated Common.dll to version 1.18.5.
 *                                          1.  For all Label controls that make up the VariableControl user control, changed the TextAlign property from MiddleLeft/
 *                                              MiddleRight to TopLeft/TopRight, as appropriate, and modified the Padding property to 0,5,0,0. This prevents misalignment
 *                                              of the display when the width of the text string associated with the label exceeds the width of the label.
 *                                          
 *                                          2.  Changed the WidthWatchControlVariableNameField constant in the WatchControlLayout class to 210 to optimize the size of the
 *                                              the Watch Window user controls for a display resolution of 1280 x 960.
 *                                          
 *                                          3.  Modified the FormPTU class to change the text associated with the escape key from 'Esc - Home' to 'Esc' to prevent overflow
 *                                              whee using larger fonts.
 *                                      
 *                                          4.  Modified the Parameter class to allow different default fonts to be specified for Windows XP and Windows 7.
 *                                                  1.  Replaced the constants DefaultFontFamily and DefaultFontSize with DefaultFontFamilyXP and DefaultFontSizeXP
 *                                                      respectively.
 *                                                  2.  Added the constants DefaultFontFamilyWin7 and DefaultFontSizeWin7.
 *                                                  3.  Set the default font and sizes to be Tahoma - 8pt for Windows XP and Segoe UI - 8.25pt for Windows 7. 
 *                                  
 *                                  8.  Updated Event.dll to version 1.17.6.
 *                                          1.  Increased the width of the event name field of the event log DataGridView control from 200 pixels to 250 pixels to allow
 *                                              the full event text to be displayed.
 *                                          
 *                                          2.  Changed the SleepMsVerisimilitudeGetRecord constant in the CommunicationEventOffline class from 25 ms to 0 ms as the
 *                                              simulated time to load the event list was excessive.
 *                                              
 *  08/06/13    6.7.9   K.McD       1.  Updated Common.dll to version 1.18.6.
 *                                          1.  Added the 'Remove Selected Plot(s)' ToolStripMenuItem control as a property to the IPlotterWatch interface definition to
 *                                              allow client programs to access the properties of this context menu option. Specifically to allow the menu option to be
 *                                              enabled/disabled as it is not appropriate when viewing fault logs that have just been downloaded from the VCU. Updated
 *                                              PlotterBitmask, PlotterScalar and PlotterEnumerator, which all include the interface definition, accordingly.
 *                                          
 *                                  2.  Updated Event.dll to version 1.17.7.
 *                                          1.  Disabled the 'F5-Edit' function key in the contructor of the FormViewFaultLog class as changing the plot layout is not
 *                                              supported on live fault log data. This feature is only available once the fault log has been saved to disk.
 *                                          
 *                                          2.  Modified the 'Shown' event handler of the FormViewFaultLog class to hide the 'Remove Selected Plot(s)' context menu option
 *                                              as this feature is not supported on live fault log data.
 *                                              
 *  02/27/14    6.7.10  K.McD       1.  Changed the parameter modifiers from 'ref' to 'out' when calling PTUDLL32.GetTimeDate() 
 *                                      in the GetTimeDate() method of the CommunicationApplication class.
 *                                      
 *                                  2.  Changed the names of the FormConfigurePassword and FormConfigureDateTime 
 *                                      classes to be consistent with the MenuInterfaceApplication class. They are 
 *                                      now called FormConfigurePasswordProtection and FormConfigureRealTimeClock.
 *                                      
 *                                  3.  Updated the MenuInterfaceApplication class to reflect the class name changes 
 *                                      associated with item 2 of this revision.
 *                                      
 *                                  4.  Updated the MdiPTU class as follows:
 *                                          1.  Corrected the 'Y' Location values in MdiPTU.resx for m_StatusStripCurrentSetup (4), m_StatusStripUserMessage (4), 
 *                                              m_LegendStatusInformation (6), m_LegendRx (6) and m_DigitalControlPacketReceived (9) as these 
 *                                              values had somehow been corrupted.
 *                                          
 *                                          2.  Updated the m_MenuItemHelpAboutPTU_Click() method to use the renamed m_MenuInterfaceApplication.HelpAbout() method.
 *                                          
 *                                  5.  Updated Common.dll to version 1.18.7. Summary: Cursor display changes, use of '[MarshalAs(UnmanagedType.BStr)] out String' 
 *                                      when retrieving string values from PTUDLL32.dll and a number of parameter modifiers changed 
 *                                      from 'ref' to 'out' 
 *                                  6.  Updated Event.dll to version 1.17.8. Summary: Cursor display changes and use of '[MarshalAs(UnmanagedType.BStr)] out String' 
 *                                      when retrieving string values from PTUDLL32.dll.
 *                                  7.  Updated SelfTest.dll to version 1.4.1. Summary: Cursor display changes.
 *                                  8.  Updated Watch.dll to version 1.22.3. Summary: A number of parameter modifiers changed 
 *                                      from 'ref' to 'out' and cursor display changes.
 *                                  9.  Updated WibuKey.dll to version 1.1. No functional changes.
 *                                                                  
 */
#endregion - [Revision History 6.7] -

#region - [Revision History 6.8] -
/*
 *  
 *  03/22/15    6.8.0   K.McD       References
 *              
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *  
 *                                      1.  Changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                          to support both 32 and 64 bit architecture.
 *                                          
 *                                      2.  Changes to allow the PTU to handle both 2 and 4 character year codes.
 *  
 *                                      3.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                          Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                          
 *                                          1.  MOC-0171-06. All references to 'Fault Logs', including menu options and directory 
 *                                              names to be replaced by 'Data Streams' for all projects.
 *                                                  
 *                                          2.  MOC-0171-18. The ‘Time’ legend will be reserved for system information time, the legend 
 *                                              ‘PC Time’ will be used when displaying the PC time.
 *                                                  
 *                                          3.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                              options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                                  
 *                                          4.  MOC-0171-30. The ‘F4 – Rec.’ Function key will toggle between the industry standard icons
 *                                              representing stop and start recording.
 *                                              
 *                                          5.  MOC-0171-41. Wherever possible, i.e. where there is room within the existing control to accommodate the text
 *                                              ‘Vehicle Control Unit’ without having significant impact on the screen layout, VCU will be replaced with 
 *                                              Vehicle Control Unit.
 *                                              
 *                                          6.  MOC-0171-26. Kawasaki pointed out that Chart Recorder tabs are identified as ‘COLUMNS’ and requested that this
 *                                              be changed.
 *                                              
 *                                              For Chart Recorder and Data Stream worksets, the GroupBox will now be titled 'Workset Layout', as per the
 *                                              'Watch Window' workset, and the TabPage header will show the type of workset i.e. 'Data Stream' or
 *                                              'Chart Recorder'. For the Data Stream 'Create/Edit Workset' and 'Configure' dialog boxes, if the project does
 *                                              not support multiple data stream types and the number of data stream channels does not exceed 16, it is proposed to
 *                                              add a row header showing the channel numbers 1 - 16. If the project supports multiple data streams or the number
 *                                              of parameters exceeds 16 then the row header will not be shown. The form used to define the plot layout of
 *                                              data stream files will display the name of the event that generated the data stream in the TabPage header.
 *                                              
 *                                          7.  MOC-0171-29/34. Kawasaki requested that any function that is not available to the current mode, either Maintenance
 *                                              or Administrative, should not be displayed, or be 'greyed out'. BTPC agreed to review the software to ensure that
 *                                              non-active menu items are ‘greyed out’ or not shown.
 *                                              
 *                                              As a result of the review, it is proposed that the following modifications are carried out:
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
 *                                          8.  MOC-0171-28. A check will be included as part of the ‘Save’ procedure to ensure that an empty workset cannot be saved.
 *                                          
 *                                              As a result of a review of the software, it is proposed that it should only be possible to save a workset if the
 *                                              following criteria are met:
 *                                              
 *                                                  1.  The workset must contain at least one watch variable.
 *                                                  2.  The workset must have a valid name that is not currently in use.
 *                                                  
 *                                      2.  Updated Resources with a number of premium 28 x 28 images purchased from Iconfinder.
 *                                      
 *                                      3.  SNCR - R188 PTU [20 Mar 2015] item 1. When trying to build version 1.19 of Common.dll the following error message was
 *                                          displayed "Error 4 'Common.Configuration.DataDictionary' does not contain a definition for 'LOG' and no extension method
 *                                          'LOG' accepting a first argument of type 'Common.Configuration.DataDictionary' could be found (are you missing a using
 *                                          directive or an assembly reference?)".
 *                                      
 *                                      4.  SNCR - R188 PTU [20 Mar 2015] Item 3. Menu option 'Configuration/Worksets/Data Stream'. If, while editing a workset, the user
 *                                          removes one or more watch variables from the workset and then selects the 'Cancel' button; when the workset is re-opened,
 *                                          the 'Available' ListBox contains the watch variables that were previosly removed from the workset.
 *                                          
 *                                      5.  SNCR - R188 PTU [20 Mar 2015] Item 6. Bug associated with saving chart recorder worksets that contain watch variables
 *                                          that have different types of FormatString values e.g. 'General Number' and 'Hexadecimal'.
 *                                          
 *                                      6.  SNCR - R188 PTU [20 Mar 2015] Item 7. While attempting to configure a data stream, the set of parameters that were downloaded
 *                                          from the VCU were not defined in an existing workset, consequently FormConfigureFaultLogParameters entered 'Create' mode.
 *                                          While in this mode, the PTU displayed the workset parameters that were downloaded from the VCU and gave the user the
 *                                          opportunity to name the workset but not the opportunity to Save the workset. Modify the code to ensure that the new workset
 *                                          can be saved.
 *                                          
 *                                      7.  SNCR - R188 PTU [20 Mar 2015] Item 8. When using the 'Tools/Convert Engineering Database' menu option (Factory Mode Only)
 *                                          to convert the .e1 database to an XML configuration file; if any of the tables that are not automatically generated by the
 *                                          Data Dictionary Builder utility have not been manually updated correctly then the conversion should be terminated and the
 *                                          table where the error was detected should be reported to the user.
 *                                          
 *                                      8.  SNCR - R188 PTU [20 Mar 2015] Item 9. Do not throw an exception if the flag returned from a call to the WinHelp engine isn't
 *                                          true.
 *
 *                                              
 *                                  Modifications
 *                                  1.  Renamed the PTUDLL32 project to VcuCommunication. Ref.: 1.1.
 *                                  2.  PTU Application.Resources.resx, MdiPTU.resx. - Ref.: 1.3.1, 1.3.2, 1.3.3, 1.3.5, 2.
 *                                  3.  CommunicationApplication.cs. Rev. 1.6. - Ref.: 1.1, 1.2.
 *                                  4.  General.cs Rev. 1.13. - Ref.: 1.2.
 *                                  5.  FormShowSystemInformation.cs. Rev. 1.11. - FormConfigureRealTimeClock.cs. Rev. 1.8.  Ref.: 1.2.
 *                                  6.  MdiPTU.Support.cs. Rev. 1.17. Ref.: 1.3.7.
 *                                          
 *                                  Dynamic Link Library Updates
 *                                  1.  Watch Application Extension Assembly - Rev. 1.23. - Ref.: 1.1, 1.3, 2, 5.
 *                                  2.  Event Application Extension Assembly - Rev. 1.18. - Ref.: 1.1, 1.3.1, 1.3.2, 1.3.3, 1.3.7, 2, 6.
 *                                  3.  SelfTest Application Extension Assembly - Rev. 1.5. - Ref.: 1.1, 1.3.1, 1.3.2, 1.3.3, 2.
 *                                  4.  VCUCommunication Application Extension Assembly - Rev. 2.0. - Ref.: 1.1, 1.2.
 *                                  5.  Common Application Extension Assembly - Rev. 1.19. - Ref.: 1.1, 1.2, 1.3.1, 1.3.2, 1.3.3,
 *                                      1.3.7, 1.3.8, 2, 3, 4, 7, 8.
 */
#endregion - [Revision History 6.8] -

#region - [Revision History 6.9] -
/*
 *  03/22/15    6.9.0   K.McD       References
 *              
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *                                      
 *                                      1.  Changes to allow the PTU to handle both 2 and 4 character year codes.
 *                                      
 *                                      2.  NK-U-6505 Section 5.2. Mandatory Folder Structure. The folder structure of the PTU will be modified to meet Table 9 of the
 *                                          Kawasaki PTE Uniform Interface Specification.
 *                                          
 *                                      3.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified to
 *                                          meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the current naming
 *                                          convention will still apply.
 *                                          
 *                                      4.  NK-U-6505 Section 1.7.6. Data Sorting Capabilities. The proposal is to include an additional column in the spreadsheet that
 *                                          is used to view event logs (File/Open/Event Logs) that identifies the event log associated with the entry e.g. Maintenance,
 *                                          Engineering etc and allow the user to sort by this column. The format of the event log will obviously also have to be modified
 *                                          to include this information.
 *                                          
 *                                      5.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                          labels to the status strip at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                          ‘Wibu-Key: [Present | Not Present]’.
 *                                          
 *                                  2.  Although only relevant to Bombardier Field Service Engineers that support a number of PTU projects, the Software User Manual and
 *                                      Release Notes documents are to be made project specific by prepending the project identifier to the file name e.g.
 *                                      'R8PR.Portable Test Unit - Release Notes.pdf', 'CTPA.Portable Test Unit - Release Notes.pdf' etc.
 *                                      
 *                                  3.  SNCR - R188 PTU [20 Mar 2015] Item 10. If, while viewing a saved datasteam in replay mode, that has been accessed from the
 *                                      'Open/Event Log' screen, the user opens one or more 'Show Flags' dialog boxes and then selects the 'Back' (Esc) key, these
 *                                      dialog boxes are not automatically closed. If the user then tries to close these dialog boxes once the PTU has returned to the
 *                                      Open/Event Log' screen, an exception is thrown. On further investigation it was discovered that these dialog boxes are not
 *                                      automatically closed if the user uses the F4 key to toggle between the 'Replay' and the'YTPlot' screens.
 *                                      
 *                                  4.  The 'Tools/Convert Engineering Database' menu option is to be modified to generate the XML file from two separate (.e1) files.
 *                                      The first file is the engineering data dictionary (.e1) database that is generated automatically from the Database Builder
 *                                      Utility. The second file is a project specific PTU configuration database that contains the following supplemental tables that
 *                                      are required for the correct operation of the PTU: CONFIGUREPTU, DataStreamTypes, LOGS, Security and URI. Partly populated
 *                                      CONFIGUREPTU and LOGS tables are created by the Database Builder Utility in the engineering data dictionary (.e1) database,
 *                                      however, these tables are ignored in the conversion process. The information contained in these partly populated tables can be
 *                                      useful when initially setting up the tables in the project PTU configuration database. 
 *                                      
 *                                      Both files are selected by the user and should be, ideally, located in the 
 *                                      '<User Application Data>\Bombardier\Portable Test Unit\PTU Configuration Files' sub-directory. The convention
 *                                      is to name the project PTU configuration database '<project-id>.PTU Configuration.e1' e.g. R8PR.PRU Configuration.e1.
 *                                      
 *                                      The purpose for this change is that once the project PTU configuration database is set up, new vesions of the XML file can
 *                                      be easily created using the Database Builder Utility as the output of the utility can be directly used to create the new XML 
 *                                      file whereas before, the supplemental tables had to be added manually to the (.e1) file output from the Database Builder Utility.
 *                                      
 *                                  5.  A FunctionFlags bitmask field is to be added to the  CONFIGUREPTU table. This field is a bitmask that specifies the function
 *                                      options that are required for the current project. So far, the following flags have been defined:
 *
 *                                          Bit 0   -   Use4DigitYearCode - Flag to specify whether the project VCU uses a 2 or 4 digit year code. True, if the project
 *                                                      uses a 4 digit year code; otherwise, false.
 *                                          Bit 1   -   ShowEventLogType - Flag to specify whether the event log type field is to be shown when saved event logs are
 *                                                      displayed. True, if the log type is to be displayed; otherwise, false.
 *                                          .
 *                                          .
 *                                          .
 *                                          Bit 7   -   Not Used.
 *                                          
 *                                  6.  The height of the event variable user control and the DataGridView Row Height must be increased to allow characters such as
 *                                      'g', 'j', 'p', 'q', 'y' to be displayed correctly when using the default font.
 *                                      
 *                                  Modifications
 *                                  
 *                                  1.  PTU Application.Resources.resx.- Ref.: 2.
 *                                  2.  MenuInterfaceApplication.cs. Rev. 1.12. - Ref.: 2.
 *                                  3.  MdiPTU.cs. Rev. 1.13. - Ref.: 2.
 *                                  4.  FormHelpAbout.cs. Rev. 1.2. - Ref.: 2.
 *                                  5.  FormConfigureRealTimeClock.cs. Rev. 1.8, FormShowSystemInformation.cs. Rev. 1.11. - Ref.: 1.1, 5.
 *                                  
 *                                  Dynamic Link Library Updates
 *                                  1.  Common Application Extension Assembly. Rev. 1.20. - Ref.: 1.5, 2, 3, 4, 5, 6.
 *                                  2.  Event Application Extension Assembly. Rev. 1.19. - Ref.: 1.3, 1.4, 6.
 *                                  3.  WibuKey Application Extension Assembly. Rev. 1.2. - Ref.: 1.5.
 */
#endregion - [Revision History 6.9] -

#region - [Revision History 6.10] -
/*
 *  05/12/15    6.10    K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *                                      
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, Kawasaki Rail Car and NYTC
 *                                          on 12th April 2013 - MOC-0171:
 *                                                  
 *                                              1.  MOC-0171-30. The ‘F4 – Rec.’ Function key will toggle between the industry standard icons
 *                                                  representing stop and start recording.
 *                                                  
 *                                              2.  MOC-0171-26. Kawasaki pointed out that Chart Recorder tabs are identified as ‘COLUMNS’ and requested that this
 *                                                  be changed.
 *                                              
 *                                                  For Chart Recorder and Data Stream worksets, the GroupBox will now be titled 'Workset Layout', as per the
 *                                                  'Watch Window' workset, and the TabPage header will show the type of workset i.e. 'Data Stream' or
 *                                                  'Chart Recorder'. For the Data Stream 'Create/Edit Workset' and 'Configure' dialog boxes, if the project does
 *                                                  not support multiple data stream types and the number of data stream channels does not exceed 16, it is proposed to
 *                                                  add a row header showing the channel numbers 1 - 16. If the project supports multiple data streams or the number
 *                                                  of parameters exceeds 16 then the row header will not be shown. The form used to define the plot layout of
 *                                                  data stream files will also display the type of workset in the TabPage header.
 *                                      
 *                                      2.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified to
 *                                          meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the current naming
 *                                          convention will still apply.
 *                                      
 *                                      3.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                          labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                          ‘WibuBox: [Present | Not Present]’.
 *                                          
 *                                  2.  SNCR - R188 PTU [20 Mar 2015] - Item 10. Bug associated with closing of the FormShowFlags dialog boxes.
 *                                          
 *                                  3.  SNCR - R188 PTU [20 Mar 2015] Item 15. While trying to modify the plot layout of a data stream that had been created as part
 *                                      of the CTA PTU installation, the exception ‘ER-150506 – Attempt to modify default plot layout’ was thrown. On further
 *                                      investigation, it was discovered that files that are created as part of the PTU installation are read only for the current
 *                                      user, even if they have administrative privileges. On changing the plot layout the exception is thrown on attempting to
 *                                      write the new layout to disk.
 *                                      
 *                                  4.  The 'Tools/Convert Engineering Database' menu option is to be modified to track the read directory where the engineering
 *                                      data dictionary (.e1) database and the project specific PTU configuration(.e1) database are located. This directory will
 *                                      remain the default directory for the remainder of the session.
 *                                      
 *                                  5.  SNCR - R188 PTU [20 Mar 2015] Item 17. If the user uses the ‘Remove Selected Plot(s)’ context menu option to remove one or more
 *                                      plots from the 'File/Open/Watch File', 'File/Open/Data Stream', or 'File/Open/Simulated Data Stream' screen, the
 *                                      ‘Modified Layout’ status message is not displayed until the screen is closed and then re-loaded from disk. Also, if the user
 *                                      toggles between the Replay and Plot screens the ‘Modified Layout’ status message is cleared.
 *                                      
 *                                  6.  SNCR - R188 PTU [20 Mar 15] Item 4. If the PTU is being used in a development environment, i.e. there is a possibility of
 *                                      switching between multiple projects, and the PTU is started up using the R188 configuration but the user agrees to switch to the
 *                                      CTA configuration when an attempt is made to connect to a CTA VCU, there is a bug which results in the PTU continuing to check
 *                                      whether a Wibu Key is present.
 *                                      
 *                                  7.  SNCR - R188 PTU [20 Mar 2015] Item 13. On project R188, the bitmask flags associated with the ‘IPA Status’ bitmask do not tie
 *                                      in with the bitmask value of 0x1034 as the FormShowFlags dialog box shows the first two flags associated with the bitmask as
 *                                      being asserted. On further investigation it was found that the first two bits of the ‘IPA Status’ bitmask had not been defined
 *                                      and were therefore not added to the list of bitmask flags shown by the FormShowFlags dialog box.
 *                                      
 *                                  8.  Hide the ToolTip text for the [First], [Next], [Previous] and [Last] function keys on the 'Replay' screens as these ToolTips
 *                                      obscure the [Frame No.] information label.
 *                                      
 *                                  9.  SNCR - R188 PTU [20-Mar-2015] Item 18. If the user uses the dragdrop feature of FormPlotDefine to modify the plot layout, 
 *                                      the OK and Apply buttons are not enabled.
 *                                      
 *                                  10. The YearCodeSize field of the CONFIGUREPTU table of the project PTU configuration (.e1) database has been deleted and replaced
 *                                      by the Use4DigitYearCode flag corresponding to bit 0 of the FunctionFlags bitmask field. This field is a bitmask that specifies
 *                                      which of the programmable function options are to be enabled for the current project. So far, the following flags have been
 *                                      defined:
 *
 *                                          Bit 0   -   Use4DigitYearCode - Flag to specify whether the project VCU uses a 2 or 4 digit year code. True, if the project
 *                                                      uses a 4 digit year code; otherwise, false.
 *                                          Bit 1   -   ShowEventLogType - Flag to specify whether the event log type field is to be shown when saved event logs are
 *                                                      displayed. True, if the log type is to be displayed; otherwise, false.
 *                                          .
 *                                          .
 *                                          .
 *                                          Bit 7   -   Not Used.
 *                                          
 *                                  11.  Although only relevant to Bombardier Field Service Engineers that support a number of PTU projects, the Software User Manual
 *                                       and Release Notes documents are to be made project specific by prepending the project identifier to the file name e.g.
 *                                       'R8PR.Portable Test Unit - Release Notes.pdf', 'CTA.Portable Test Unit - Release Notes.pdf' etc.
 *                                       
 *                                  12. SNCR - R188 PTU [20-Mar-15] Item 19. Until the engineering password is actively set to the default value using the
 *                                      'Configure/Password Protection' menu option it does not allow access to the engineering security level. On further investigation
 *                                      it was found that the HashCodeLevel1 and HashCodeLevel2 values associated with the Common Settings file had not been updated since
 *                                      the hashing algorithm was changed by Sean Duggan on 7-Feb-12. The HashCodeLevel1 and HashCodeLevel2 values were updated in
 *                                      the Settings file and the problem appears to be resolved. An investigation is still required as to why the password appeared to
 *                                      work on earlier releases of the PTU.
 *                                      
 *                                  Modifications
 *                                  1.  Resources.resx. Ref.: 1.3
 *                                  2.  Settings.settings. Ref.: 1.3.
 *                                  3.  FormConfigureRealTimeClock.cs. Rev. 1.8. Ref.: 10.
 *                                  4.  MenuInterfaceApplication.cs. Rev. 1.12. Ref.: 1.3, 11.
 *                                  5.  MdiPTU.cs. Rev. 1.14, MdiPTU.Designer.cs. Rev. 1.11. Ref.: 1.3, 6.
 *                                  6.  MdiPTU.Support.cs. Rev. 1.18. Ref.: 1.3, 6.
 *                                  
 *                                  Dynamic Link Library Updates
 *                                  1.  Common Application Extension Assembly. Rev. 1.21. - Ref.: 1.1.1, 1.1.2, 1.3, 3, 4, 5, 7, 8, 9, 12.
 *                                  2.  Event Application Extension Assembly. Rev. 1.20. - Ref.: 1.3.
 *                                  3.  WibuKey Application Extension Assembly. Rev. 1.2. - Ref.: 1.3.
 *                                  4.  Watch Application Extension Assembly. Rev. 1.24. - Ref.: 1.2, 1.3, 2.
 *                                  
 */
#endregion - [Revision History 6.10] -

#region - [Revision History 6.11] -
/*
 *  07/13/15    6.11    K.McD       References
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
 *                                      2.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. Following a conference call on 9-Jul-15,
 *                                          the current proposal is to extend the options associated with the log saved status StatusLabel to include:
 *                                          ‘[Saved | Unsaved | Unknown | Not Applicable (-)]’.
 *                                      
 *                                      3.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 2.  Addition of the Control Panel window.
 *                                      
 *                                      4.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be
 *                                          modified to meet the KRC specification. This modification will be specific to the KRC project; for all other projects,
 *                                          the current naming convention will still apply.
 *                                          
 *                                  2.  Following the conference call on 9-Jul-15, it was decided that the Clear and Initialize event log functions should only
 *                                      be available to the Engineering account.
 *                                          
 *                                  3.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 20. If the user tries to clear or initialize an empty event log and opts to save 
 *                                      the event log first, an exception is thrown (ER-150712).
 *                                      
 *                                  Modifications
 *                                  1.  Added the ControlPanel.cs. Rev. 1.0, ControlPanel.Designer.cs. Rev. 1.0 ControlPanel.resx. - Ref.: -1.3
 *                                  2.  Modified Resources.resx. - Ref.: 1.3.
 *                                  3.  Modified MdiPTU.Support.cs. Rev. 1.19. - Ref.:1.2, 1.3, 2.
 *                                  2.  Modified MdiPTU.cs, MdiPTU.Designer.cs, MdiPTU.resx.
 *                                  
 *                                  Dynamic Link Library Updates
 *                                  1.  Common Application Extension Assembly. Rev. 1.22. - Ref.: 1.2, 1.3, 1.4.
 *                                  2.  Event Application Extension Assembly. Rev. 1.21. - Ref.: 1.1.1.3, 1.2, 2, 3.
 *                                  3.  Watch Application Extension Assembly. Rev. 1.25. - Ref.: 1.1.1.4.                                  
 */
#endregion - [Revision History 6.11] -

#region - [Revision History 6.12] -
/*
 *  07/23/15    6.12    K.McD       References
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
 *                                  2.  An informal review of version 6.11 of the PTU concluded that, where possible - i.e. if the PTU is started from a shortcut
 *                                      that passes the project identifier as a shortcut parameter, the project specific PTU initialization should be carried out
 *                                      in the MDI Form constructor that has the parameter string array as its signature rather than by the LoadDictionary() method.
 *                                      This streamlines the display construction of the Control Panel associated with the R188 project. In the 6.11 implementation
 *                                      the CTA layout is momentarily displayed before the Control Panel is drawn, however, by initializing the project specific
 *                                      features in the constructor, the Control Panel associated with the R188 project is drawn immediately and the CTA layout
 *                                      is not shown at all.
 *                                      
 *                                  3.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 21. It is possible to load a data dictionary that is not associated with the
 *                                      current project, i.e. the project associated with the project identifier that was passed via the desktop shortcut, when using
 *                                      the 'File/Select Data Dictionary' menu option. It is also possible to select the '<project-identifier>.PTU Configuration.xml'
 *                                      file.
 *                                      
 *                                  4.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 22. The toggling of the ToolTipText for the Online and Offline buttons on the
 *                                      R188 project doesn’t work correctly.
 *                                      
 *                                  5.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 23. In the Rev. 1.25 implementation of Watch.dll, if the ‘F5-Modify’ function
 *                                      key of the 'View/Watch' screen is disabled, because, for example, the user is in Maintenance mode and the active workset
 *                                      is at the Engineering security level, and the user selects either the ‘F4-Pause’ or the ‘F3-Rec’ function key;
 *                                      the ‘F5-Modify’ function key remains disabled while the PTU is in the record or the paused state, however, on returning to the
 *                                      normal state, it becomes active. This is incorrect and should be corrected in the next revision.
 *                                      
 *                                  6.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 24. On selecting the ‘Exit’ function key on the ‘Diagnostics/Event Log’ and
 *                                      the ‘Diagnostics/Self Tests’ screens, the cursor doesn’t go to the Cursors.WaitCursor cursor on the R188 project.
 *                                      
 *                                  7.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 25. The Font of the ‘NYCT R188’, ‘PTU STATUS’, ‘MAINTENANCE’ and
 *                                      ‘ADMINISTRATION’  labels is not automatically updated when the user changes the Font via the ‘Tools/Options’ menu. 
 *                                      
 *                                  7. Informal NSEL review of control names.
 *  
 *                                  Modifications
 *                                  1.  Program.cs. Rev. 1.1. - Ref.: 2.
 *                                  2.  Resources.resx. - Ref.: 1.
 *                                  3.  MdiPTU.Support.cs. Rev. 1.20. Ref.: 1, 2.
 *                                  4.  ControlPanel.cs. Rev. 1.1. - Ref.: 2, 4, 7.
 *                                  5.  MdiPTU.cs. Rev. 1.16, MdiPTU.Designer.cs. Rev. 1.12, MdiPTU.resx. - Ref.: 1.
 *                                  6.  FormShowSystemInformation.Designer.cs. Rev. 1.3. - Ref.: 8.
 *                                  
 *                                  Dynamic Link Library Updates
 *                                  1.  Common Application Extension Assembly. Rev. 1.23. - Ref.: 1, 2, 3.
 *                                  2.  SelfTest Application Extension Assembly. Rev. 1.6. Ref.:1, 6.
 *                                  3.  WibuKey Application Extension Assembly. Rev. 1.3. - Ref.: 2.
 *                                  4.  Watch Application Extension Assembly. Rev. 1.26. - Ref.: 5, 6.
 *                                  5.  Event Application Extension Assembly. Rev. 1.22. - Ref.: 6.
 * 
 */
#endregion - [Revision History 6.12] -

#region - [Revision History 6.13] -
/*
 *  08/11/15    6.13    K.McD       References
 *                                  1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                      from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                      
 *                                      1.  MOC-0171-005. KRC have requested that the 'Log' button is to be disabled in simulation mode to avoid confusion if the
 *                                          simulated event data were ever saved to disk.
 *                                          
 *                                      2.  On the R188 project only, all references to 'PTU' are to be replaced with 'PTE' and all occurrences of 'Portable Test Unit'
 *                                          are to be replaced with 'Portable Test Equipment'. Where support for multiple legends is not possible, 'Portable Test
 *                                          Application' is to be used as an alternative to 'Portable Test Unit'/'Portable Test Equipment.
 *                                          
 *                                      3.  The legends on the control panel buttons and labels are to be modified to match those specified in the KRC PTE Uniform
 *                                          Interface Specification Rev. B.
 *                                          
 *                                      4.  For the R188 project, the splash screen is no longer to be displayed on start-up.
 *                                          
 *                                  2.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 27. If the Factory user uses the ‘Select Data Dictionary’ menu option to update
 *                                      the configuration file, or, the PTU tries to update the configuration file because of a mismatch between the version of the
 *                                      embedded software and that of the XML configuration file; the selected file is not copied across to the project default
 *                                      configuration file, consequently, the next time that the application is run; the PTU reports that it cannot locate the default
 *                                      project configuration file.
 *                                      
 *                                  3.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 28. If the project requires a Wibu-Key and the files ‘Configuration.xml’ and
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
 *                                  1.  ControlPanel.cs. Rev. 1.2. Modified the DiagnosticsEventLog_EnabledChanged() method to disable the 'Log' button if the
 *                                      PTE is in simulation mode. - Ref.: 1.1.
 *                                      ControlPanel.Designer.cs. Rev. 1.2. - Ref.: 1.3.
 *                                  2.  FormHelpAbout.cs. Rev. 1.3. - Ref.: 1.2.
 *                                  3.  Resources.resx. - Ref.: 1.2. Removed any reference to PTU in the message box captions e.g. replaced 'PTU - Error' with 'Error'.
 *                                  4.  Modified the 'AssemblyProduct' string for the 'Portable Test Application Assembly' and related dynamic link library projects
 *                                      to 'Portable Test Application' instead of 'Portable Test Unit (PTU).
 *                                  5.  Modified MenuInterfaceApplication.cs. Rev. 1.13. - Ref.: 1.2, 3.
 *                                  6.  Modified MdiPTU.Support.cs. Rev. 1.21. - Ref.: 1.2, 1.4, 3.
 *                                  7.  Modified MdiPTU.cs. Rev. 1.17. - Ref.: 1.2, 3.
 *                                      
 *                                  Dynamic Link Library Updates
 *                                  1.  Common Application Extension Assembly. Rev. 1.24. - Ref.: 1.2, 2, 3. 
 *                                  2.  SelfTest Application Extension Assembly. Rev. 1.7. Ref.: 1.2.
 *                                  3.  WibuKey Application Extension Assembly. Rev. 1.4. - Ref.: 1.2, 1.3.
 *                                  4.  Watch Application Extension Assembly. Rev. 1.27. - Ref.: 1.2.
 *                                  5.  Event Application Extension Assembly. Rev. 1.23. - Ref.: 1.2.
 *                                  6.  CodeProject.GraphComponents Application Extension Assembly. Rev. 2.2. - Ref.: 1.2.
 */
#endregion - [Revision History 6.13] -
#endregion --- Revision History ---

using System.Reflection;
using System.Resources;

// General Information about an assembly is controlled through the following set of attributes. Change these attribute values to modify the
// information associated with an assembly.
[assembly: AssemblyCompany("Bombardier Transportation (Holdings) USA Inc.")]
[assembly: AssemblyProduct("Portable Test Application")]
[assembly: AssemblyCopyright("(C) 2010 - 2015 Bombardier Transportation (Holdings) USA Inc.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguageAttribute("")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers by using the '*'.

[assembly: AssemblyVersion("6.13.0.0")]