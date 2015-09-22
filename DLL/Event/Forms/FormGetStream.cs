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
 *  File name:  FormGetStream.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  02/18/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  04/07/11    1.1     K.McD           1.  Added a number of missing XML tags.
 *                                      2.  Corrected the title of a #region and modified a Debug.Assert() message.
 *                                      
 *  08/24/11    1.1     K.McD           1.  Renamed a number of constants.
 *                                      2.  Included a timeout on the completion of the BackgroundWorker thread.
 *                                      3.  Include a try/catch block in the m_BackgroundWorker_DoWork() method check whether an an exception is thrown and, if so, 
 *                                          set the DialogResult value to Abort.
 *                                          
 *  07/16/13    1.2     K.McD           1.  Renamed the constants:
 *                                              a.  DataStreamTypeFaultLog to DataStreamTypeStandardFaultLog
 *                                              b.  DurationSecDownloadFaultLog to DurationSecDownloadStandardFaultLog.
 *                                              
 *                                      2.  Modified the XML tags associated with the updated constants.
 *                                      
 *                                      3.  Modified the default case of the switch(dataStreamTypeIdentifier) statement in the constructor to set the 
 *                                          progress bar parameters and thread complete timeout to be the same as those for the standard fault log in 
 *                                          case additional datastream types are added to the DataStreamType table of the .E1 database.
 *                                          
 *                                          This modification arose because a new datastream type was added to the DataStreamType table of the .E1 database 
 *                                          to define the fault log datastream associated with the R188 project. The datastream associated with this project 
 *                                          is identical to that of the standard fault log however the trip time index i.e. the sample corresponding to the 
 *                                          time of the actual trip is 25 rather than 75. When trying to download the fault log with this new datastream 
 *                                          type the download would terminate immediately and report that the PTU was unable to download the fault log as the 
 *                                          download complete timeout had not been initialized.
 *                                          
 *                                      
 *  
 */
#endregion --- Revision History ---

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using Common;
using Common.Communication;
using Common.Configuration;
using Common.Forms;

namespace Event.Forms
{
    /// <summary>
    /// A class to support downloading of the fault logs from the VCU.
    /// </summary>
    public partial class FormGetStream : FormPTUDialog
    {
        #region --- Constants ---
        /// <summary>
        /// The format string that is to be used to display the time of the event. Value: "HH:mm:ss".
        /// </summary>
        private const string FormatStringEventTime = "HH:mm:ss";

        /// <summary>
        /// The interval, in ms, between successive display updates. Value: 1000 ms.
        /// </summary>
        private const int IntervalMsDisplayUpdate = 250;

        /// <summary>
        /// The data stream type identifier associated with a standard fault log. Value: 0.
        /// </summary>
        private const short DataStreamTypeStandardFaultLog = 0;

        /// <summary>
        /// The data stream type identifier associated with a CTA fault log. Value: 1.
        /// </summary>
        private const short DataStreamTypeFaultLogCTA = 1;

        /// <summary>
        /// The data stream type identifier associated with a snapshot log. Value: 2.
        /// </summary>
        private const short DataStreamTypeSnapshotLog = 2;

        /// <summary>
        /// The download duration, in seconds, of a standard fault log. Value: 3 seconds.
        /// </summary>
        private const int DurationSecDownloadStandardFaultLog = 3;

        /// <summary>
        /// The download duration, in seconds, of a CTA fault log. Value: 4 seconds.
        /// </summary>
        private const int DurationSecDownloadFaultLogCTA = 4;

        /// <summary>
        /// The download duration, in seconds, of a CTA snapshotlog. Value: 25 seconds.
        /// </summary>
        private const int DurationSecDownloadSnapshotLog = 25;

        /// <summary>
        /// The factor by which to multiply the expected downloadduration - in seconds, to obtain the timeout period, in ms. Value: 2000.
        /// </summary>
        private const float TimeoutFactor = 2000;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        private ICommunicationEvent m_CommunicationInterface;

        /// <summary>
        /// Reference to the event record that was passed as a parameter.
        /// </summary>
        private EventRecord m_EventRecord;

        /// <summary>
        /// The System.Windows.Forms timer used to manage the display update.
        /// </summary>
        private System.Windows.Forms.Timer m_TimerProgressBarUpdate;

        /// <summary>
        /// Reference to the background worker task.
        /// </summary>
        private BackgroundWorker m_BackgroundWorker;

        /// <summary>
        /// The timeout, in ms, for the thread that is responsible for downloading the fault log to complete. 
        /// </summary>
        private int m_TimeoutMsThreadComplete;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FormGetStream()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communicaton interface that is to be used to communicate with the VCU</param>
        /// <param name="eventRecord">The event record associated with the data stream.</param>
        public FormGetStream(ICommunicationEvent communicationInterface, EventRecord eventRecord)
        {
            InitializeComponent();

            CommunicationInterface = communicationInterface;
            Debug.Assert(CommunicationInterface != null);

            m_EventRecord = eventRecord;
            m_LabelDataStreamName.Text = string.Format("{0} {1} - {2}", m_EventRecord.Description, m_EventRecord.DateTime.ToShortDateString(), m_EventRecord.DateTime.ToString("HH:mm:ss"));

            #region - [Background Worker] -
            // Instantiate BackgroundWorker and attach handlers to its DoWork and RunWorkerCompleted events.
            m_BackgroundWorker = new BackgroundWorker();
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_DoWork);
            //m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_BackgroundWorker_RunWorkerCompleted);
            #endregion - [Background Worker] -

            #region - [ProgressBar Timer] -
            m_TimerProgressBarUpdate = new System.Windows.Forms.Timer();
            m_TimerProgressBarUpdate.Tick += new EventHandler(ProgressBarUpdate);
            m_TimerProgressBarUpdate.Interval = IntervalMsDisplayUpdate;
            m_TimerProgressBarUpdate.Enabled = true;
            m_TimerProgressBarUpdate.Stop();
            #endregion - [ProgressBar Timer] -

            #region - [Progress Bar] -
            short dataStreamTypeIdentifier;
            try
            {
                dataStreamTypeIdentifier = Lookup.LogTable.Items[m_EventRecord.LogIdentifier].DataStreamTypeIdentifier;
            }
            catch (Exception)
            {
                dataStreamTypeIdentifier = 0;
            }

            int updatesPerSec = (int)(CommonConstants.SecondMs / IntervalMsDisplayUpdate);

            switch (dataStreamTypeIdentifier)
            {
                case DataStreamTypeStandardFaultLog:
                    m_ProgressBarDownload.Maximum = DurationSecDownloadStandardFaultLog * updatesPerSec;
                    m_TimeoutMsThreadComplete = (int)(DurationSecDownloadStandardFaultLog * TimeoutFactor);
                    break;
                case DataStreamTypeFaultLogCTA:
                    m_ProgressBarDownload.Maximum = DurationSecDownloadFaultLogCTA * updatesPerSec;
                    m_TimeoutMsThreadComplete = (int)(DurationSecDownloadFaultLogCTA * TimeoutFactor);
                    break;
                case DataStreamTypeSnapshotLog:
                    m_ProgressBarDownload.Maximum = DurationSecDownloadSnapshotLog * updatesPerSec;
                    m_TimeoutMsThreadComplete = (int)(DurationSecDownloadSnapshotLog * TimeoutFactor);
                    break;
                default:
                    m_ProgressBarDownload.Maximum = DurationSecDownloadStandardFaultLog * updatesPerSec;
                    m_TimeoutMsThreadComplete = (int)(DurationSecDownloadStandardFaultLog * TimeoutFactor);
                    break;
            }
            
            m_ProgressBarDownload.Minimum = 0;
            m_ProgressBarDownload.Value = 0;
            #endregion - [Progress Bar] -
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
                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    if (m_TimerProgressBarUpdate != null)
                    {
                        m_TimerProgressBarUpdate.Stop();
                        m_TimerProgressBarUpdate.Dispose();
                    }

                    if (m_BackgroundWorker != null)
                    {
                        m_BackgroundWorker.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                m_TimerProgressBarUpdate = null;
                m_CommunicationInterface = null;
                m_EventRecord = null;
                m_BackgroundWorker = null;
                
                #region --- Windows Form Designer Variables ---
                // Detach the event handler delegates.

                // Set the Windows Form Designer Variables to null.

                #endregion --- Windows Form Designer Variables ---
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
        /// Event handler for the <c>Shown</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormGetStream_Shown(object sender, EventArgs e)
        {
            Update();

            if (m_TimerProgressBarUpdate != null)
            {
                m_TimerProgressBarUpdate.Start();
            }

            // Default the DialogResult to report that the operation was not cancelled by the user or aborted by the background worker thread. 
            DialogResult = DialogResult.OK;

            // Start the download operation in the background.
            m_BackgroundWorker.RunWorkerAsync();

            DateTime startTime = DateTime.Now;
            while (m_BackgroundWorker.IsBusy && (DateTime.Now < startTime.Add(new TimeSpan(0, 0, 0, 0, m_TimeoutMsThreadComplete))))
            {
                // Ensure that the form remains responsive during the asynchronous operation.
                Application.DoEvents();
            }

            // Check that operation was not cancelled by the user or aborted by the background worker thread 
            if (DialogResult == DialogResult.OK)
            {
                // No - now check whether the thread timed out.
                if (m_BackgroundWorker.IsBusy == true)
                {
                    DialogResult = System.Windows.Forms.DialogResult.Abort;
                }
            }

            Close();
        }
        #endregion - [Form] -

        #region - [Buttons] -
        /// <summary>
        /// Event handler for the Cancel button.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        public virtual void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_ButtonCancel.Enabled = false;
            DialogResult = DialogResult.Cancel;
        }

        #endregion - [Buttons] -

        /// <summary>
        /// Called periodically by the System.Windows.Forms.Timer event. Update the progress bar.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void ProgressBarUpdate(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_ProgressBarDownload.Value < m_ProgressBarDownload.Maximum)
            {
                m_ProgressBarDownload.Value++;
            }
        }

        #region - [Background Worker] -
        /// <summary>
        /// The background worker task. Download the data stream corresponding to the event record specified in the constructor and update the DataSteamCurrent property 
        /// of the FormViewEventLog class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            DataStream_t dataStream;
            try
            {
                dataStream = CommunicationInterface.GetStream(m_EventRecord);
            }
            catch (Exception)
            {
                DialogResult = DialogResult.Abort;
                return;
            }

            // Get the reference to the calling form.
            FormViewEventLog calledFromAsFormViewEventLog = CalledFrom as FormViewEventLog;
            if (calledFromAsFormViewEventLog != null)
            {
                // Update the DataStreamCurrent property of the form.
                calledFromAsFormViewEventLog.DataStreamCurrent = dataStream;
            }
        }
        #endregion - [Background Worker] -
        #endregion --- Delegated Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the VCU.
        /// </summary>
        public ICommunicationEvent CommunicationInterface
        {
            get { return m_CommunicationInterface; }
            set { m_CommunicationInterface = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Enabled</c> property of the Cancel button.
        /// </summary>
        /// <remarks>The Cancel button is enables by default.</remarks>
        public bool CancelEnable
        {
            get { return m_ButtonCancel.Enabled; }
            set { m_ButtonCancel.Enabled = value; }
        }
        #endregion --- Properties ---
    }
}