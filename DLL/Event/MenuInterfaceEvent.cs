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
 *  Project:    Events
 * 
 *  File name:  MenuInterfaceEvent.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/26/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/26/10    1.1     K.McD           1.  Added the ConfigureWorksetFaultLog() method.
 * 
 *  01/06/11    1.2     K.McD           1.  Renamed the OpenDataStreamFile() method to ShowDataStreamFile().
 *                                      2.  Added the OpenEventLog() method.
 *                                      3.  Added the ShowEventLog() method.
 *                                      4.  Renamed the ShowEventLog() method to ViewEventLog().
 * 
 *  01/27/11    1.3     K.McD           1.  Bug fix - SNCR001.89. Modified the ShowDataStreamFile() method such that the selected data stream file is only shown 
 *                                          if the selected file is valid.
 * 
 *  02/03/11    1.4     K.McD           1.  Modified a number of XML tags.
 *                                      2.  SNCR001.73. Included support to allow the user to use the Windows multi-select feature to load a number of event log files. 
 *                                          Deleted the ShowEventLogFile() method and replaced this with the ImportEventLogFiles() method.
 *                                      3.  Modified the OpenEventLog() method to use the ImportEventLogFiles() method.
 * 
 *  02/14/11    1.4.1   K.McD           1.  Added the reference to the MBTViewEventLogFail resource in the constructor.
 * 
 *  02/28/11    1.4.2   K.McD           1.  Auto-modified as a result of a number of resource and class name changes.
 *                                      2.  Renamed a number of local variables.
 *                                      3.  Modified the InitializeEventLogs() method to ask for user confirmation before proceeding.
 * 
 *  03/28/11    1.4.3   K.McD           1.  Auto-modified as a result of a number of name changes to the properties and methods of external classes.
 *  
 *  06/29/11    1.4.4   K.McD           1.  Changed the message box caption and icon to 'Error' on the message box used to inform the user that the PTU was unable to 
 *                                          display the form used to display event logs.
 *                                          
 *  07/07/11    1.4.5   K.McD           1.  Modified the ViewEventLog() method to inform the user of the name of the log that is being retrieved prior to showing the 
 *                                          form used to display the event logs.
 *                                          
 *  08/10/11    1.5     Sean.D          1.  Included support for offline mode. Modified the InitializeEventLogs() method to conditionally choose CommunicationEvent or 
 *                                          CommunicationEventOffline depending upon the current mode.
 *                                          
 *  09/30/11    1.6     K.McD           1.  Modified the ImportEventLogFiles() method to initialize the FullFilename property of the EventLogFile_t structure.
 *  
 *  08/07/13    1.6.1   K.McD           1.  In those methods where it is applicable, the cursor was set to the wait cursor after the call to the
 *                                          MainWindow.CloseChildForms() method as if any child forms are open, the cursor may be set to the default cursor as part
 *                                          of the call to the Exit() method within the child form.
 *                                          
 *  07/13/15    1.7     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 4800010525-CU2/19.03.2015.
 *                                  
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. Following a conference call on 9-Jul-15,
 *                                              the current proposal is to extend the options associated with the log saved status StatusLabel to include:
 *                                              ‘[Saved | Unsaved | Unknown | Not Applicable (-)]’. The log saved StatusLabel should also operate on a per
 *                                              car basis.
 *                                              
 *                                      Modifications
 *                                      1.  Modified the ViewEventLog() method to use the new signature to call the ViewEventLog form. The new signature includes
 *                                          a reference to the MainWindow interface to allow the form to access the LogStatus property of the MainWindow interface
 *                                          from within the constructor.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using Common;
using Common.Communication;
using Common.Configuration;
using Common.Forms;

using Event.Communication;
using Event.Forms;
using Event.Properties;

namespace Event
{
    /// <summary>
    /// Methods called by the menu options associated with the event sub-system - Event.dll.
    /// </summary>
    public class MenuInterfaceEvent : MenuInterface
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="mainWindow">Reference to the main application window interface.</param>
        public MenuInterfaceEvent(IMainWindow mainWindow) : base(mainWindow)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        #region - [File] -
        /// <summary>
        /// Call the <c>ShowDataStreamFile</c> method specifying the parameters associated with a fault log.
        /// </summary>
        public void OpenFaultLog()
        {
            MainWindow.CloseChildForms();
            MainWindow.Cursor = Cursors.WaitCursor;

            try
            {
                ShowDataStreamFile(Resources.FileDialogOpenTitleFaultLog, CommonConstants.ExtensionFaultLog,
                                   Resources.FileDialogOpenFilterFaultLog, InitialDirectory.FaultLogsRead);
            }
            catch (Exception exception)
            {
                MessageBox.Show(Resources.MBTOpenFaultLogFailed + CommonConstants.NewLine + CommonConstants.NewLine + exception.Message,
                                Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Call the <c>ShowEventLogFiles</c> method specifying the parameters associated with an event log.
        /// </summary>
        public void OpenEventLog()
        {
            MainWindow.CloseChildForms();
            MainWindow.Cursor = Cursors.WaitCursor;

            try
            {
                EventLogFile_t eventLogFile = ImportEventLogFiles();

                // Ensure that the selected files contained one or more events.
                if (eventLogFile.EventRecordList.Count <= 0)
                {
                    MainWindow.Cursor = Cursors.Default;
                    return;
                }

                FormOpenEventLog formOpenEventLog = new FormOpenEventLog(eventLogFile);
                MainWindow.ShowMdiChild(formOpenEventLog);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Ask the user to select one or more event log files, import the data contained in each of the files into an event log file structure and return 
        /// this event log file structure.
        /// </summary>
        /// <remarks>The filename and header fileds associated with the new event log file structure will be set to those values associated with 
        /// the first file included in the list.</remarks>
        /// <returns>The event log file containing all of the imported event date.</returns>
        public EventLogFile_t ImportEventLogFiles()
        {
            // Create a structure to contain all of the imported data.
            EventLogFile_t eventLogFile = new EventLogFile_t();
            eventLogFile.EventRecordList = new List<EventRecord>();

            Debug.Assert(MainWindow != null, "MenuInterface.ShowEventLogFile() - [MainWindow != null]");


            string[] fullFilenames = General.FileDialogOpenFileMultiSelect(Resources.FileDialogOpenTitleEventLog,
                                                                                     CommonConstants.ExtensionEventLog,
                                                                                     Resources.FileDialogOpenFilterEventLog,
                                                                                     InitialDirectory.EventLogsRead);

            // Skip, if the user didn't select a file.
            if (fullFilenames.Length == 0)
            {
                MainWindow.Cursor = Cursors.Default;
                return eventLogFile;
            }

            // Update the initial directory with the path of the selected files.
            InitialDirectory.EventLogsRead = Path.GetDirectoryName(fullFilenames[0]);

            // Create a structure to contain the data contained within each selected file.
            EventLogFile_t currentEventLogFile;
            FileInfo fileInfo;
            for (int fileIndex = 0; fileIndex < fullFilenames.Length; fileIndex++)
            {
                fileInfo = new FileInfo(fullFilenames[fileIndex]);
                MainWindow.WriteStatusMessage(string.Format(Resources.SMLoadFile,fileInfo.Name));

                currentEventLogFile = FileHandling.Load<EventLogFile_t>(fullFilenames[fileIndex], FileHandling.FormatType.Xml);

                // Ensure that the de-serialized file contains data.
                if (currentEventLogFile.EventRecordList == null)
                {
                    // File format is not recognised, report message.
                    MessageBox.Show(string.Format(Resources.MBTFormatNotRecognized, fileInfo.Name), Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                // Ensure that the selected log file is associated with the current project.
                if (currentEventLogFile.Header.ProjectInformation.ProjectIdentifier != Parameter.ProjectInformation.ProjectIdentifier)
                {
                    MessageBox.Show(string.Format(Resources.MBTProjectIdMismatchMultipleImport, fileInfo.Name), Resources.MBCaptionError, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    continue;
                }

                // -------------------------------------
                // The current file is valid, import it.
                // -------------------------------------
                // Use the header and filename associated with the first entry in the file list.
                if (fileIndex == 0)
                {
                    eventLogFile.Header = currentEventLogFile.Header;
                    eventLogFile.Filename = fileInfo.Name;
                    eventLogFile.FullFilename = fileInfo.FullName;
                }

                // Append the events contained within the current file into the existing event records.
                bool duplicationsFound;
                eventLogFile.AppendEventRecordList(currentEventLogFile.EventRecordList, out duplicationsFound);
            }

            MainWindow.WriteStatusMessage(string.Empty);
            return eventLogFile;
        }

        /// <summary>
        /// Ask the user to select a data-stream file, de-serialized this to the appropriate object type and then display the data using the 
        /// <c>FormDataStreamPlot</c> class.
        /// </summary>
        /// <param name="title">The title that is to appear on the <c>OpenFileDialog</c> form.</param>
        /// <param name="defaultExtension">The default extension associated with the type of log.</param>
        /// <param name="filterText">The filter text. Used to filter the list of available files.</param>
        /// <param name="initialDirectory">The initial directory that will be show.</param>
        /// <returns>A flag to indicate whether a valid watch file was selected. True, indicates that the selected file was valid; otherwise, false.</returns>
        public override bool ShowDataStreamFile(string title, string defaultExtension, string filterText, string initialDirectory)
        {
            // Default to the selected file being invalid.
            bool selectedFileIsValid = false;

            selectedFileIsValid = base.ShowDataStreamFile(title, defaultExtension, filterText, initialDirectory);

            if (selectedFileIsValid == true)
            {
                // The data-stream file is valid , display it using the appropriate form.
                MainWindow.Cursor = Cursors.WaitCursor;

                // The data-stream file is valid , display it.
                FormDataStreamPlot formDataStreamPlot;
                if (m_WatchFile.DataStream.LogType == LogType.DataStream)
                {
                    formDataStreamPlot = new FormOpenFaultLog(m_WatchFile);
                    MainWindow.ShowMdiChild(formDataStreamPlot);
                }
                else
                {
                    MessageBox.Show(Resources.MBTLogFileTypeNotSupported, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MainWindow.Cursor = Cursors.Default;
            }

            return selectedFileIsValid;
        }
        #endregion - [File] -

        #region - [Diagnostics] -
        /// <summary>
        /// Show the child form which displays the current event logs.
        /// </summary>
        public void ViewEventLog()
        {
            MainWindow.CloseChildForms();
            MainWindow.Cursor = Cursors.WaitCursor;

            // ---------------------------------------------------------------
            // Inform the user of the name of the log that is being retrieved.
            // ---------------------------------------------------------------
            // Retrieve the first valid log from the LogTable as this is the log that will be displayed by the FormViewEventLog class.
            Log log = null;
            for (int recordIndex = 0; recordIndex < Lookup.LogTable.RecordList.Count; recordIndex++)
            {
                // Check that a valid entry exists.
                if (Lookup.LogTable.Items[recordIndex] != null)
                {
                    log = Lookup.LogTable.Items[recordIndex];
                    break;
                }
            }

            MainWindow.WriteStatusMessage(string.Format(Resources.SMEventLogRetrieve, log.Description));
            
            // --------------
            // Show the form.
            // --------------
            try
            {
                FormViewEventLog formEventLog = new FormViewEventLog(MainWindow.CommunicationInterface, MainWindow);
                MainWindow.ShowMdiChild(formEventLog);
            }
            catch (Exception exception)
            {
                MessageBox.Show(Resources.MBTViewEventLogFailed + CommonConstants.NewLine + CommonConstants.NewLine + exception.Message, Resources.MBCaptionError,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.WriteStatusMessage(string.Empty);
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Initialize the event logs.
        /// </summary>
        public void InitializeEventLogs()
        {
            MainWindow.CloseChildForms();
            MainWindow.Cursor = Cursors.WaitCursor;

            ICommunicationEvent communicationEvent;

            // Initialize the communication interface.
            if (MainWindow.CommunicationInterface is CommunicationParent)
            {
                communicationEvent = new CommunicationEvent(MainWindow.CommunicationInterface);
            }
            else
            {
                communicationEvent = new CommunicationEventOffline(MainWindow.CommunicationInterface);
            }

            // Ask the user for confirmation.
            DialogResult dialogResult = MessageBox.Show(Resources.MBTConfirmInitializeEventLogs, Resources.MBCaptionQuestion, MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);
            if (MainWindow != null)
            {
                MainWindow.Update();
            }

            if (dialogResult == DialogResult.No)
            {
                MainWindow.Cursor = Cursors.Default;
                return;
            }

            // Clear each event logs.
            Log log;
            for (int logIndex = 0; logIndex < Lookup.LogTable.RecordList.Count; logIndex++)
            {
                log = Lookup.LogTable.RecordList[logIndex];

                // Only process those logs that have been defined.
                if (log != null)
                {
                    MainWindow.WriteStatusMessage(string.Format(Resources.SMEventLogsInitialize, log.Description));

                    try
                    {
                        communicationEvent.ChangeEventLog(log);
                        communicationEvent.InitializeEventLog();
                    }
                    catch (CommunicationException)
                    {
                        MessageBox.Show(Resources.MBTEventLogInitializeFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        MainWindow.WriteStatusMessage(string.Empty);
                        MainWindow.Cursor = Cursors.Default;
                    }
                }
            }
        }
        #endregion - [Diagnostics] -

        #region - [Configure] -
        /// <summary>
        /// Show the form which allows the user the configure the worksets associated with the fault log data stream type.
        /// </summary>
        public void ConfigureWorksetsFaultLog()
        {
            MainWindow.Cursor = Cursors.WaitCursor;
            try
            {
                FormWorksetManagerFaultLog formWorksetManagerFaultLog = new FormWorksetManagerFaultLog(Workset.FaultLog);
                MainWindow.ShowDialog(formWorksetManagerFaultLog);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion - [Configure] -
        #endregion --- Methods ---
    }
}
