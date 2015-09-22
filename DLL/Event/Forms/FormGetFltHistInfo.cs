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
 *  File name:  FormGetFltHistInfo.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/06/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  04/08/11    1.1     K.McD           1.  Modified a number of XML tags and comments.
 *                                      2.  Removed a number on unused constants.
 *                                      3.  Added a number of Debug.Assert() statements.
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using Common;
using Common.Communication;
using Common.Configuration;
using Common.Forms;
using Event.Properties;

namespace Event.Forms
{
    /// <summary>
    /// A class to download the event history associated with the current log from the VCU.
    /// </summary>
    public partial class FormGetFltHistInfo : FormPTUDialog
    {
        #region --- Constants ---
        /// <summary>
        /// The approximate number of records that can be downloaded from the VCU per second.
        /// </summary>
        private const int RecordsPerSec = 4;

        /// <summary>
        /// The interval, in ms, between successive display updates. Value: 1000 ms.
        /// </summary>
        private const int IntervalDisplayUpdateMs = 250;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        private ICommunicationEvent m_CommunicationInterface;

        /// <summary>
        /// A reference to the event record list that was passed as a parameter.
        /// </summary>
        private List<EventRecord> m_EventRecordList;

        /// <summary>
        /// A reference to the event log that was passed as a parameter.
        /// </summary>
        private Log m_Log;

        /// <summary>
        /// The System.Windows.Forms timer used to manage the display update.
        /// </summary>
        private System.Windows.Forms.Timer m_TimerProgressBarUpdate;

        /// <summary>
        /// A reference to the background worker task.
        /// </summary>
        private BackgroundWorker m_BackgroundWorker;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormGetFltHistInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communicaton interface that is to be used to communicate with the VCU</param>
        /// <param name="eventRecordList">The list of event records associated with the current log.</param>
        /// <param name="log">The current event log.</param>
        public FormGetFltHistInfo(ICommunicationEvent communicationInterface, List<EventRecord> eventRecordList, Log log)
        {
            InitializeComponent();

            CommunicationInterface = communicationInterface;
            Debug.Assert(CommunicationInterface != null);
            m_Log = log;
            Debug.Assert(m_Log != null);
            m_EventRecordList = eventRecordList;
            Debug.Assert(m_EventRecordList != null);

            m_LabelDescription.Text = Resources.TitleEventHistory;

            #region - [Background Worker] -
            // Instantiate a BackgroundWorker and attach handlers to its DoWork and RunWorkerCompleted events.
            m_BackgroundWorker = new BackgroundWorker();
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_DoWork);
            //m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_BackgroundWorker_RunWorkerCompleted);
            #endregion - [Background Worker] -

            #region - [ProgressBar Timer] -
            m_TimerProgressBarUpdate = new System.Windows.Forms.Timer();
            m_TimerProgressBarUpdate.Tick += new EventHandler(ProgressBarUpdate);
            m_TimerProgressBarUpdate.Interval = IntervalDisplayUpdateMs;
            m_TimerProgressBarUpdate.Enabled = true;
            m_TimerProgressBarUpdate.Stop();
            #endregion - [ProgressBar Timer] -

            #region - [Progress Bar] -
            int updatesPerSec = (int)(CommonConstants.SecondMs / IntervalDisplayUpdateMs);
            int downloadDurationSec = (int)(m_EventRecordList.Count / RecordsPerSec);

            m_ProgressBarDownload.Maximum = downloadDurationSec * updatesPerSec;
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

                    if (m_EventRecordList != null)
                    {
                        m_EventRecordList.Clear();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                m_TimerProgressBarUpdate = null;
                m_CommunicationInterface = null;
                m_EventRecordList = null;
                m_BackgroundWorker = null;
                m_Log = null;
                
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

            // Default the DialogResult to report that the operation was not cancelled by the user.
            DialogResult = DialogResult.OK;

            // Start the download operation in the background.
            m_BackgroundWorker.RunWorkerAsync();

            while (m_BackgroundWorker.IsBusy)
            {
                // Ensure that the form remains responsive during the asynchronous operation.
                Application.DoEvents();
            }

            Close();
        }
        #endregion - [Form] -

        #region - [Buttons] -
        /// <summary>
        /// Event handler for the Cancel button.
        /// </summary>
        /// <remarks>The Cancel button is disabled for this form.</remarks>
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
        /// The background worker task. Download the event history associated with the current event log and update the EventStatusList property of the 
        /// FormShowEventHistory class.
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

            // ---------------------------------------------------------------
            // Get the history information associated with the current log.
            // ---------------------------------------------------------------
            int eventCount = (short)m_EventRecordList.Count;
            int maxEventCount = m_Log.MaxEventsPerTask * m_Log.MaxTasks;

            // Initialize the validFlags array to define which of the events are used in the current log.
            short[] validFlags = new short[maxEventCount];
            int eventIndex;
            for (int index = 0; index < m_EventRecordList.Count; index++)
            {
                eventIndex = m_EventRecordList[index].TaskIdentifier * m_Log.MaxEventsPerTask + m_EventRecordList[index].EventIdentifier;
                validFlags[eventIndex] = CommonConstants.True;
            }

            short[] cumulativeHistoryCounts = new short[eventCount];
            short[] recentHistoryCounts = new short[eventCount];
            
            CommunicationInterface.GetFltHistInfo(validFlags, ref cumulativeHistoryCounts, ref recentHistoryCounts, m_Log.MaxTasks, m_Log.MaxEventsPerTask);

            // ---------------------------------
            // Initialise the event status list.
            // ---------------------------------
            List<EventStatus_t> eventStatusList = new List<EventStatus_t>(eventCount);
            EventStatus_t eventStatus;
            for (int eventStatusIndex = 0; eventStatusIndex < eventCount; eventStatusIndex++)
            {
                eventStatus = new EventStatus_t();
                eventStatus.Index = eventStatusIndex;
                eventStatus.Identifier = m_EventRecordList[eventStatusIndex].Identifier;
                eventStatus.CumulativeHistoryCount = cumulativeHistoryCounts[eventStatusIndex];
                eventStatus.RecentHistoryCount = recentHistoryCounts[eventStatusIndex];

                eventStatusList.Add(eventStatus);
            }

            // Get the reference to the calling form.
            FormShowEventHistory calledFromAsFormShowEventHistory = CalledFrom as FormShowEventHistory;
            Debug.Assert(calledFromAsFormShowEventHistory != null, "FormGetFltHistInfo.m_BackgroundWorker_DoWork() - [calledFromAsFormShowEventHistory != null]");

            // Update the EventStatusList property of the form.
            calledFromAsFormShowEventHistory.EventStatusList = eventStatusList;
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