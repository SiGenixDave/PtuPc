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
 *  Project:    Watch
 * 
 *  File name:  ThreadPollWatch
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/28/10    1.0     K.McDonald      1.  First entry into TortoiseSVN.
 * 
 *  08/26/10    1.1     K.McD           1.  Implemented code which attempts to recover from a communication fault.
 * 
 *  10/04/10    1.2     K.McD           1.  Bug fix SNCR001.21. Included support for the PauseFeedback property. This flag is asserted when the polling has entered the pause state, 
 *                                          i.e. the current communication request is complete and no further requests will be issued until the pause property has been cleared.
 * 
 *  10/15/10    1.3     K.McD           1.  Made the CommunicationInterface member variable non-static.
 *                                      2.  Modified the implementation to use the CommunicationWatch class instead of ICommunication.
 * 
 *  10/29/10    1.4     K.McD           1.  CommunicationWatch replaced with ICommunicationWatch.
 * 
 *  01/06/11    1.5     K.McD           1.  Added a number of constants.
 *                                      2.  Added the m_FormWatchView member variable to record the reference of the calling form.
 *                                      3.  Included a mutex on the communication interface to ensure that any independent communication on the calling form will not 
 *                                          interfere with the polling.
 *                                      4.  Included support for the PollScheduler class.
 * 
 *  01/12/11    1.6     K.McD           1.  Bug fix SNCR001.84. Added the second parameter to the call to the System.Threading.WaitHandle.WaitOne() method, as advised by 
 *                                          the following blog entries: 
 *                                              (1) http://www.mikeplate.com/missingmethodexception-and-waitone/ and (2) 
 *                                              (2) http://blog.darrenstokes.com/2009/03/30/watch-out-for-those-waitone-overloads-when-you-need-backwards-compatibility 
 * 
 *  01/14/11    1.7     K.McD           1.  Added a mutex to control read/write access to the communication port when trying to restore communication in the Run() method.
 * 
 *  01/31/11    1.8     K.McD           1.  Modified to accommodate the communication mutex introduced into the Common.CommunicationParent class in version 1.11 of 
 *                                          Common.dll.
 * 
 *  02/14/11    1.9     K.McD           1.  Added a mutex to control read/write access to the PauseFeedback property.
 *                                      2.  Replaced reference to the CommunicationInterface.WatchSize property with Parameters.WatchSize.
 * 
 *  02/27/11    1.10    K.McD           1.  Auto-modified as a result of a number of class name changes.
 *                                      2.  Modified a number of variables.
 *                                      3.  Modified the Run() method such that repeated calls to the PTUDLL32.UpdateWatchElements() method are made rather than calls to the 
 *                                          GetEmbeddedInformation() method if a communication fault is detected.
 * 
 *  03/28/11    1.11    K.McD           1.  Automodified as a result of name changes to a number of AutoScale_t variables.
 *  
 *  08/04/11    1.11.1  K.McD           1.  Replaced the reference to m_PauseFeedback in the Run() method with a reference to the PauseFeedback property so that 
 *                                          the Mutex is applied.
 *                                          
 *  08/24/11    1.11.2  K.McD           1.  Reordered the properties, added a number of constants, modified a number of variable and property names, and modified a number 
 *                                          of XML tags.
 *                                      2.  Added checks on whether the dispose method has already been called to a number of methods.
 *                                      3.  Added a sleep statement if the thread is in the paused state.
 *                                      4.  Added support for the watchdog counter.
 *                                      5.  Added support for a countdown on the number of read timeouts that are allowed before triggering a communication fault.
 *                                      
 *  07/26/13    1.12    K.McD           1.  Modified the Run() method such that if a communication fault is detected, instead of trying to re-establish communication, the thread 
 *                                          just sleeps until the Dispose() method is called by the client. While in this state, the thread is periodically awoken to update the 
 *                                          watchdog counter so that the client can determine whether the communication port has locked.
 *                                          
 *  08/01/13    1.12.1  K.McD           1. Modified the Run() method to close the communication port as soon as the communication fault flag is asserted.
 *
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Threading;

using Common;
using Common.Communication;
using Common.Configuration;
using Watch.Forms;

namespace Watch
{
    /// Polls the target hardware for watch values and logs the retrieved values to a cyclic buffer. Also records the values to a second, larger, cyclic buffer if
    /// the <c>Record</c> property is asserted.
    /// </summary>
    class ThreadPollWatch : WorkerThread
    {
        #region --- Constants ---
        /// <summary>
        /// The interval, in ms, between watch variable updates. Value: 250 ms.
        /// </summary>
        private const int IntervalMsUpdate = 250;

        /// <summary>
        /// The countdown value associated the read timeout. Value: 3.
        /// </summary>
        private int ReadTimeoutCountdown = 3;

        /// <summary>
        /// The thread sleep interval, in ms, between checking the state of the Pause property. Value: 10 ms.
        /// </summary>
        private int SleepMsCheckPause = 10;

        /// <summary>
        /// The thread sleep interval, in ms, between watchdog refreshes once a communication fault has been detected.  Value: 50 ms. 
        /// </summary>
        private int SleepMsRefreshWatchdog = 50;
        #endregion --- Constants ---

        #region - [Member Variables] -
        #region - [Properties] -
        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        private ICommunicationWatch m_CommunicationInterface;

        /// <summary>
        /// Flag to control polling of the target hardware. True, inhibits polling of the target hardware; otherwise, false, resumes polling.
        /// </summary>
        private bool m_Pause;

        /// <summary>
        /// Feedback flag to indicate that the polling of the target hardware has been inhibited.
        /// </summary>
        private bool m_PauseFeedback;

        /// <summary>
        /// Flag used to indicate that there is a communication fault. True, indicates that a fault exists; otherwise, false, indicates that communication is OK.
        /// </summary>
        private bool m_CommunicationFault;

        /// <summary>
        /// The number of packets that have been received since the class was instantiated;
        /// </summary>
        /// <remarks> 
        /// Used as a thread-safe way of blinking the packet received icon on the main window. The property correponding to this value is read by the display 
        /// update method on the main thread and provided it has incremented since the previous display update that method will blink the icon. As the method
        /// is on the same thread on which the icon was created it is inherently safe.
        /// </remarks>
        private long m_PacketCount;

        /// <summary>
        /// Flag to control the recording of watch data to the cyclic buffer. True, records the data; otherwise, false, does not record the data.
        /// </summary>
        private bool m_Record;

        /// <summary>
        /// Generic cyclic queue used to store logged watch data.
        /// </summary>
        private CyclicQueue<WatchFrame_t> m_CyclicQueueLog;

        /// <summary>
        /// Generic cyclic queue used to store recorded watch data.
        /// </summary>
        private CyclicQueue<WatchFrame_t> m_CyclicQueueRecord;

        /// <summary>
        /// The auto-scale information for each watch element over the duration of the recording period.
        /// </summary>
        private AutoScale_t[] m_AutoScaleWatchValues;

        /// <summary>
        /// The number of frames that have been added to the cyclic buffer used to store recorded watch data.
        /// </summary>
        /// <remarks>Used as a thread-safe way of updating the progress bar on the main window.</remarks>
        private long m_RecordCount;
        #endregion - [Properties] -

        #region - [Mutex] -
        /// <summary>
        /// Mutex to control read/write access to the <c>Pause</c> property.
        /// </summary>
        private Mutex m_MutexPause;

        /// <summary>
        /// Mutex to control read/write access to the <c>PauseFeedback</c> property.
        /// </summary>
        private Mutex m_MutexPauseFeedback;

        /// <summary>
        /// Mutex to control read/write access to the <c>CommunicationFault</c> property.
        /// </summary>
        private Mutex m_MutexCommunicationFault;

        /// <summary>
        /// Mutex to control read/write access to the <c>Record</c> property.
        /// </summary>
        private Mutex m_MutexRecord;

        /// <summary>
        /// Mutex to control read/write access to the <c>AutoScaleWatchValues</c> property.
        /// </summary>
        private Mutex m_MutexAutoScaleWatchValues;
        #endregion - [Mutex] -

        /// <summary>
        /// Reference to the calling form.
        /// </summary>
        private FormViewWatch m_FormViewWatch;

        /// <summary>
        /// Reference to the class that schedules polling for new events.
        /// </summary>
        private PollScheduler m_PollScheduler;

        /// <summary>
        /// The countdown to the read timeout.
        /// </summary>
        private int m_ReadTimeoutCountdown;

        /// <summary>
        /// Flag to record whether it is the first pass of the loop while in record mode.
        /// </summary>
        private bool m_FirstRecordPass;
        #endregion - [Member Variables] -

        #region - [Constructors] -
        /// <summary>
        /// Initializes a new instance of the class. Initializes the: communication interface; main window interface; cyclic queues; max/min values and
        /// read/write locks.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface used to communicate with the target hardware.</param>
        /// <param name="cyclicQueueRecordSize">The required size of the cyclic buffer used to record watch data.</param>
        /// <param name="cyclicQueueLogSize">The required size of the cyclic buffer used to log watch data.</param>
        /// <param name="formWatchView">Reference to the form that called this form.</param>
        public ThreadPollWatch(ICommunicationWatch communicationInterface, int cyclicQueueRecordSize, int cyclicQueueLogSize, FormViewWatch formWatchView)
        {
            m_CommunicationInterface = communicationInterface;
            Debug.Assert(m_CommunicationInterface != null);

            m_FormViewWatch = formWatchView;
            Debug.Assert(m_FormViewWatch != null, "ThreadPollWatch.Ctor() - [m_FormViewWatch != null]");

            m_CyclicQueueRecord = new CyclicQueue<WatchFrame_t>(cyclicQueueRecordSize);
            m_CyclicQueueLog = new CyclicQueue<WatchFrame_t>(cyclicQueueLogSize);

            m_MutexPause = new Mutex();
            m_MutexPauseFeedback = new Mutex();
            m_MutexCommunicationFault = new Mutex();
            m_MutexRecord = new Mutex();
            m_MutexAutoScaleWatchValues = new Mutex();

            #region - [AutoScale] -
            m_AutoScaleWatchValues = new AutoScale_t[Parameter.WatchSize];
            #endregion - [AutoScale] -

            m_PacketCount = 0;
            m_PollScheduler = new PollScheduler();
            m_ReadTimeoutCountdown = ReadTimeoutCountdown;
        }
        #endregion - [Constructors] -

        #region - [Methods] -
        /// <summary>
        /// Poll the target hardware for watch values and store the retrieve values in the log cyclic queue. If the <c>Record</c> property is asserted also store
        /// the watch values in the cyclic queue used to store recorded data.
        /// </summary>
        /// <remarks>Runs on the underlying thread.</remarks>
        public override void Run()
        {
            try
            {
                m_CommunicationFault = false;
                while (StopThread == false)
                {
                    if (Pause == false)
                    {
                        PauseFeedback = false;
                        m_PollScheduler.Wait(IntervalMsUpdate);
                        if (Pause == true)
                        {
                            m_Watchdog++;
                            continue;
                        }

                        WatchFrame_t watchFrame;
                        watchFrame = new WatchFrame_t();
                        watchFrame.CurrentDateTime = DateTime.Now;

                        // Get the watch values from the target. 
                        try
                        {
                            m_Watchdog++;
                            watchFrame.WatchElements = m_CommunicationInterface.UpdateWatchElements(true);
                            m_ReadTimeoutCountdown = ReadTimeoutCountdown;
                        }
                        catch (CommunicationException)
                        {
                            // Don't assert the communication fault flag until the countdown has elapsed. 
                            if (m_ReadTimeoutCountdown <= 0)
                            {
                                // Assert the CommunicationFault property.
                                m_CommunicationFault = true;

                                // Close the communication Port.
                                m_CommunicationInterface.CloseCommunication(m_CommunicationInterface.CommunicationSetting.Protocol);

                                // Keep the watchdog ticking over so that the client can deteremine whether the port has locked. 
                                do
                                {
                                    m_Watchdog++;
                                    Thread.Sleep(SleepMsRefreshWatchdog);
                                }
                                while (m_CommunicationFault == true);
                            }
                            else
                            {
                                m_ReadTimeoutCountdown--;
                                continue;
                            }
                        }
                        m_CommunicationFault = false;

                        // Keep a count of the number of packets received. Used as a thread-safe way of blinking the packet received icon on the main window. This value
                        // is read by the display update method on the main thread and provided it has incremented since the timeout last expired it will blink the icon.
                        m_PacketCount++;

                        // If currently in record mode copy the data to the cyclic buffer.
                        if (Record == true)
                        {
                            #region - [Recording] -
                            // Lock the cyclic queue and write the new data to the queue.
                            lock (m_CyclicQueueRecord.SyncRoot)
                            {
                                m_CyclicQueueRecord.Enqueue(watchFrame);
                            }

                            #region - [AutoScale] -
                            double valueCurrent;
                            m_MutexAutoScaleWatchValues.WaitOne(DefaultMutexWaitDurationMs, false);
                            if (m_FirstRecordPass == true)
                            {
                                // Recording has just started, initialize both the maximum and minimum values to the current value.
                                for (short watchElementIndex = 0; watchElementIndex < watchFrame.WatchElements.Length; watchElementIndex++)
                                {
                                    valueCurrent = watchFrame.WatchElements[watchElementIndex].Value;
                                    m_AutoScaleWatchValues[watchElementIndex].MaximumRaw = valueCurrent;
                                    m_AutoScaleWatchValues[watchElementIndex].MinimumRaw = valueCurrent;
                                }
                                m_FirstRecordPass = false;
                            }
                            else
                            {
                                // Keep the maximum and minimum value for each watch element up to date.
                                for (short watchElementIndex = 0; watchElementIndex < watchFrame.WatchElements.Length; watchElementIndex++)
                                {
                                    valueCurrent = watchFrame.WatchElements[watchElementIndex].Value;
                                    if (valueCurrent > m_AutoScaleWatchValues[watchElementIndex].MaximumRaw)
                                    {
                                        m_AutoScaleWatchValues[watchElementIndex].MaximumRaw = valueCurrent;
                                    }
                                    else if (valueCurrent < m_AutoScaleWatchValues[watchElementIndex].MinimumRaw)
                                    {
                                        m_AutoScaleWatchValues[watchElementIndex].MinimumRaw = valueCurrent;
                                    }
                                }
                            }
                            m_MutexAutoScaleWatchValues.ReleaseMutex();
                            #endregion - [AutoScale] -

                            // Update the record count up to the size of the cyclic buffer. Used to keep track of progress.
                            if (m_RecordCount < m_CyclicQueueRecord.Size)
                            {
                                m_RecordCount++;
                            }

                            #region - [Queue Full] -
                            // ---------------------------
                            // Check if the queue is full.
                            // ---------------------------
                            if (m_CyclicQueueRecord.Count >= m_CyclicQueueRecord.Size)
                            {
                                // TODO - ThreadPollWatch.Run(). Implement code which archives the current log after 30 minutes of recording.
                                /*
                                 * // --------------------------------------------
                                 * // Still to be implemented.
                                 * // --------------------------------------------
                                lock (m_CyclicQueueRecord.SyncRoot)
                                {
                                    // ------------------------------
                                    // Save the current cyclic queue to disk.
                                    // ------------------------------
                                    // Creates a SORTED array whose size is equal to the number of entries in the cyclic queue.
                                    cyclicQueueRecordArray = m_CyclicQueueRecord.ToArray();

                                    // Clear the cyclic buffer.
                                    m_CyclicQueueRecord.Clear();
                                }

                           
                                // The save process must be carried out on a separate thread to enable data collection to continue without
                                // loosing packets.
                                m_ThreadSaveArrayToDisk = new Thread(SaveArrayToDisk);

                                // The parameters to SaveArrayToDisk must be passed as an array of objects in this case.
                                //m_ThreadSaveArrayToDisk.Start(new object[] { m_FullyQualifiedFileMnemonic + "." + m_Page.ToString() + CommonConstants.ExtensionWatchFile, cyclicQueueRecordArray });
                                m_ThreadSaveArrayToDisk.Start(new object[] { m_FullyQualifiedFilename + CommonConstants.ExtensionWatchFile, cyclicQueueRecordArray, m_AutoScaleWatchValues });

                                // Increment the page number.
                                m_Page++;

                                // Reset the progress bar.
                                m_ApplicationProgressBarValue = 0;

                                // Check if the maximum number of pages has been exceeded, if so, end the recording.
                                if (m_Page > Parameter.PageReferenceMax)
                                {
                                    // Do the processing on a separate thread.
                                    Thread threadEndRecording = new Thread(EndRecording);
                                    threadEndRecording.Start();
                                }
                                */
                            }
                            #endregion - [Queue Full] -
                            #endregion - [Recording] -
                        }

                        // Record the data to the simulated fault log cyclic buffer.
                        lock (m_CyclicQueueLog.SyncRoot)
                        {
                            m_CyclicQueueLog.Enqueue(watchFrame);
                        }

                        // Write the new data to the lookup table.
                        m_CommunicationInterface.UpdateWatchVariableTable(watchFrame.WatchElements);
                    }
                    else
                    {
                        PauseFeedback = true;
                        m_Watchdog++;
                        Thread.Sleep(SleepMsCheckPause);
                    }
                }
            }
            finally
            {
                base.Run();
            }
        }

        /// <summary>
        /// Start recording the watch data.
        /// </summary>
        private void StartRecord()
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            Debug.Assert(m_CyclicQueueRecord != null);
            lock(m_CyclicQueueRecord.SyncRoot)
            {
                m_CyclicQueueRecord.Clear();
            }

            // Ensure that all max/min values are recorded on the first pass.
            m_FirstRecordPass = true;
            m_AutoScaleWatchValues = new AutoScale_t[Parameter.WatchSize];
            m_RecordCount = 0;
        }

        /// <summary>
        /// Stop recording the watch data.
        /// </summary>
        private void StopRecord()
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            Debug.Assert(m_CyclicQueueRecord != null);
            WatchFrame_t[] cyclicQueueRecordArray;
            lock (m_CyclicQueueRecord.SyncRoot)
            {
                // ------------------------------
                // Save the current cyclic queue to disk.
                // ------------------------------
                // Creates a SORTED array whose size is equal to the number of entries in the cyclic queue.
                cyclicQueueRecordArray = m_CyclicQueueRecord.ToArray();
            }
        }
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets or sets the flag that controls the polling of the target hardware. True, inhibits polling of the target hardware; otherwise, false, resumes polling.
        /// </summary>
        public bool Pause
        {
            get
            {
                bool result = false;
                m_MutexPause.WaitOne(DefaultMutexWaitDurationMs, false);
                result = m_Pause;
                m_MutexPause.ReleaseMutex();
                return result;
            }

            set
            {
                m_MutexPause.WaitOne(DefaultMutexWaitDurationMs, false);
                m_Pause = value;
                if (m_PollScheduler != null)
                {
                    // Terminate the scheduler if the Pause property is asserted.
                    m_PollScheduler.TerminateFlag = m_Pause;
                }
                m_MutexPause.ReleaseMutex();
            }
        }

        /// <summary>
        /// Gets or sets the feedback flag that indicates whether polling of the target hardware has been suspended.  
        /// </summary>
        /// <remarks>This flag is asserted when the polling has entered the pause state, i.e. the current communication request is complete and no further requests will 
        /// be issued until the pause property has been cleared.</remarks>
        public bool PauseFeedback
        {
             get
            {
                bool result = false;
                m_MutexPauseFeedback.WaitOne(DefaultMutexWaitDurationMs, false);
                result = m_PauseFeedback;
                m_MutexPauseFeedback.ReleaseMutex();
                return result;
            }

            set
            {
                m_MutexPauseFeedback.WaitOne(DefaultMutexWaitDurationMs, false);
                m_PauseFeedback = value;
                m_MutexPauseFeedback.ReleaseMutex();
            }
        }

        /// <summary>
        /// Gets the flag used to indicate that there is a communication fault. True, indicates that a fault exists; otherwise, false, indicates that communication is OK.
        /// </summary>
        public bool CommunicationFault
        {
            get
            {
                return m_CommunicationFault;
            }
        }

        /// <summary>
        /// Gets the number of packets received since the class was instantiated.
        /// </summary>
        public long PacketCount
        {
            get { return m_PacketCount; }
        }

        /// <summary>
        /// Gets or sets the flag that controls recording of the data to the cyclic buffer.
        /// </summary>
        public bool Record
        {
            get
            {
                bool result = false;
                m_MutexRecord.WaitOne(DefaultMutexWaitDurationMs, false);
                result = m_Record;
                m_MutexRecord.ReleaseMutex();
                return result;
            }

            set
            {
                m_MutexRecord.WaitOne(DefaultMutexWaitDurationMs, false);

                // Check for a change in state and call the appropriate method.
                if (m_Record != value)
                {
                    if (value == true)
                    {
                        StartRecord();
                        m_Record = value;
                        m_MutexRecord.ReleaseMutex();
                    }
                    else
                    {
                        m_Record = false;
                        m_MutexRecord.ReleaseMutex();
                        StopRecord();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the array of frames corresponding to the contents of the cyclic buffer associated with logging of watch values.
        /// </summary>
        public WatchFrame_t[] CyclicQueueLogArray
        {
            get
            {
                lock (m_CyclicQueueLog.SyncRoot)
                {
                    return m_CyclicQueueLog.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets the array of frames corresponding to the contents of the cyclic buffer associated with recording of watch values.
        /// </summary>
        public WatchFrame_t[] CyclicQueueRecordArray
        {
            get
            {
                lock (m_CyclicQueueRecord.SyncRoot)
                {
                    return m_CyclicQueueRecord.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets the array containing the maximum, minimum and range values of all watch elements over the period of the recording.
        /// </summary>
        public AutoScale_t[] AutoScaleWatchValues
        {
            get 
            {
                AutoScale_t[] result;
                m_MutexAutoScaleWatchValues.WaitOne(DefaultMutexWaitDurationMs, false);
                result = m_AutoScaleWatchValues;
                m_MutexAutoScaleWatchValues.ReleaseMutex();
                return result;
            }
        }

        /// <summary>
        /// Gets the number of frames that have been added to the cyclic buffer used to store recorded watch data.
        /// </summary>
        public long RecordCount
        {
            get { return m_RecordCount; }
        }
        #endregion - [Properties] -
    }
}
