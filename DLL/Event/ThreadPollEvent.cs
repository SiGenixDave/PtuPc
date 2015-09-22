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
 *  File name:  ThreadPollEvent.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  12/14/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/12/11    1.1     K.McD           1.  Bug fix SNCR001.84. Added the second parameter to the call to the System.Threading.WaitHandle.WaitOne() method, as advised by 
 *                                          the following blog entries: 
 *                                              (1) http://www.mikeplate.com/missingmethodexception-and-waitone/ and (2) 
 *                                              (2) http://blog.darrenstokes.com/2009/03/30/watch-out-for-those-waitone-overloads-when-you-need-backwards-compatibility
 * 
 *  01/14/11    1.2     K.McD           1.  Bug fix SNCR001.85. There appears to be a bug in the CheckFaultLogger() method of the PTUDLL322 dynamic link library in that 
 *                                          the event count value returned from the call can sometimes increment by the number of existing entries in the event log every 
 *                                          time that the call is made. This problem is sometimes cleared by calling the ClearEvent() method the same library, however, 
 *                                          in order to circumvent the problem the design of the dynamic update of the event log was modified to use the event index 
 *                                          value that is returned from the call instead as this value is correct. As part of this bug fix the Run() method of this class 
 *                                          was modified the check for new events by comparing the event index rather than the event count.
 * 
 *  01/31/11    1.3     K.McD           1.  Modified to accommodate the communication mutex introduced into the Common.CommunicationParent class in version 1.11 of 
 *                                          Common.dll.
 *                                          
 *  08/24/11    1.4     K.McD           1.  Added a number of constants, modified a number of variable and property names, and modified a number of XML tags.
 *                                      2.  Added checks on whether the dispose method has already been called to a number of methods.
 *                                      3.  Added a sleep statement if the thread is in the paused state.
 *                                      4.  Added support for the watchdog counter.
 *                                      5.  Added support for a countdown on the number of read timeouts that are allowed before triggering a communication fault.
 *                                      
 *  07/26/13    1.5     K.McD           1.  Modified the Run() method such that if a communication fault is detected, instead of trying to re-establish communication,
 *                                          the thread just sleeps until the Dispose() method is called by the client. While in this state, the thread is periodically
 *                                          awoken to update the watchdog counter so that the client can determine whether the communication port has locked. Modified
 *                                          to be consistent with the ThreadPollWatch class.
 *                                          
 *  08/01/13    1.6     K.McD           1.  Modified the Run() method to close the communication port as soon as the communication fault flag is asserted so that it is
 *                                          consistent with the Watch.ThreadPollWatch class.
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

using Common;
using Common.Communication;
using Common.Configuration;
using Event.Forms;

namespace Event
{
    /// <summary>
    /// Polls the target hardware for watch values and logs the retrieved values to a cyclic buffer. Also records the values to a second, larger, cyclic buffer if
    /// the <c>Record</c> property is asserted.
    /// </summary>
    class ThreadPollEvent : WorkerThread
    {
        #region --- Constants ---
        /// <summary>
        /// The number of new simulated events per pass. Value: 1.
        /// </summary>
        public const int NewEventsPerPass = 1;

        /// <summary>
        /// The interval, in ms, between event updates. Value: 2,000 ms.
        /// </summary>
        private const int IntervalMsEventUpdate = 2000;

        /// <summary>
        /// The countdown value associated the read timeout. Value: 3.
        /// </summary>
        private int ReadTimeoutCountdown = 3;

        /// <summary>
        /// The thread sleep interval, in ms, between checking the state of the Pause property. Value: 200 ms.
        /// </summary>
        private int SleepMsCheckPause = 200;

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
        private ICommunicationEvent m_CommunicationInterface;

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
        #endregion - [Mutex] -

        /// <summary>
        /// Reference to the calling form.
        /// </summary>
        private FormViewEventLog m_FormViewEventLog;

        /// <summary>
        /// Reference to the class that schedules polling for new events.
        /// </summary>
        private PollScheduler m_PollScheduler;

        /// <summary>
        /// The countdown to the read timeout.
        /// </summary>
        private int m_ReadTimeoutCountdown;
        #endregion - [Member Variables] -

        #region - [Constructors] -
        /// <summary>
        /// Initializes a new instance of the class. Initializes the communication interface and read/write locks.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface used to communicate with the target hardware.</param>
        /// <param name="formViewEventLog">Reference to the form that called this form.</param>
        public ThreadPollEvent(ICommunicationEvent communicationInterface, FormViewEventLog formViewEventLog)
        {
            CommunicationInterface = communicationInterface;
            Debug.Assert(m_CommunicationInterface != null, "ThreadPollEvent.Ctor() - [m_CommunicationInterface != null]");

            m_FormViewEventLog = formViewEventLog;
            Debug.Assert(m_FormViewEventLog != null, "ThreadPollEvent.Ctor() - [m_FormViewEventLog != null]");

            m_MutexPause = new Mutex();
            m_MutexPauseFeedback = new Mutex();
            m_MutexCommunicationFault = new Mutex();

            m_PacketCount = 0;
            m_PollScheduler = new PollScheduler();
            m_ReadTimeoutCountdown = ReadTimeoutCountdown;
        }
        #endregion - [Constructors] -

        #region - [Methods] -
        /// <summary>
        /// Poll the target hardware for new events.
        /// </summary>
        /// <remarks>Runs on a separate thread.</remarks>
        public override void Run()
        {
            try
            {
                m_CommunicationFault = false;
                short newEventCount;
                uint newEventIndex;
                while (StopThread == false)
                {
                    if (Pause == false)
                    {
                        PauseFeedback = false;
                        m_PollScheduler.Wait(IntervalMsEventUpdate);
                        newEventCount = 0;
                        if (Pause == true)
                        {
                            m_Watchdog++;
                            continue;
                        }

                        // Check for new events.
                        newEventCount = m_FormViewEventLog.EventCount;
                        newEventIndex = m_FormViewEventLog.EventIndex;
                        try
                        {
                            m_Watchdog++;
                            CommunicationInterface.CheckFaultlogger(out newEventCount, out newEventIndex);
                            m_ReadTimeoutCountdown = ReadTimeoutCountdown;
                        }
                        catch(CommunicationException)
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

                        // Skip if the Pause property has been asserted.
                        if (Pause == true)
                        {
                            continue;
                        }
#if SimulateNewEvent
                        // ------------------------------------------------------------------
                        // Simulate NewEventsPerPass events being triggered per pass.
                        // ------------------------------------------------------------------
                        // Check that there are enough existing records to allow the new events to be simulated.
                        if (m_FormViewEventLog.EventCount >= NewEventsPerPass)
                        {
                            // Retrieve the simulated new events.
                            newEventCount = (short)(m_FormViewEventLog.EventCount + NewEventsPerPass);
                            List<EventRecord> eventRecordList = new List<EventRecord>();
                            EventRecord eventRecord;

                            // Get the oldest 'NewEventsPerPass' events from the VCU.
                            for (short eventIndex = 0; eventIndex < NewEventsPerPass; eventIndex++)
                            {
                                try
                                {
                                    CommunicationInterface.GetEventRecord(m_FormViewEventLog.Log, eventIndex, out eventRecord);
                                    eventRecord.CarIdentifier = FileHeader.HeaderCurrent.TargetConfiguration.CarIdentifier;
                                    eventRecord.EventIndex = (short)(m_FormViewEventLog.EventCount + eventIndex);
                                    eventRecord.DateTime = DateTime.Now;
                                }
                                catch (CommunicationException)
                                {
                                    continue;
                                }

                                // Store the record retrieved from the VCU.
                                eventRecordList.Add(eventRecord);
                                m_FormViewEventLog.EventRecordList.Add(eventRecord);
                            }

                            m_FormViewEventLog.EventCount = newEventCount;
                            m_FormViewEventLog.EventIndex += NewEventsPerPass;

                            // Update the DataGridView control in a thread safe way.
                            m_FormViewEventLog.Invoke(new AddListDelegate(m_FormViewEventLog.AddList), new object[] { eventRecordList });
                            Console.WriteLine("New Event(s) Added. EventCount: {0}, EventIndex: {1}", newEventCount, newEventIndex);
                        }                        
#else
                        // Check whether any new events have been triggered.
                        if (newEventIndex != m_FormViewEventLog.EventIndex)
                        {
                            // Yes, new events have been triggered. Check for case of a wraparound of the uint index value.
                            if (newEventIndex > m_FormViewEventLog.EventIndex)
                            {
                                // No wraparound of the uint index value, process normally.
                                // Download the new events and add them to the DataGridView control.
                                List<EventRecord> eventRecordList = new List<EventRecord>();

                                // Load the retrieved event records into the list.
                                EventRecord eventRecord;
                                for (short eventIndex = (short)m_FormViewEventLog.EventIndex; eventIndex < (short)newEventIndex; eventIndex++)
                                {
                                    try
                                    {
                                        CommunicationInterface.GetEventRecord(m_FormViewEventLog.Log, eventIndex, out eventRecord);

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
                                    m_FormViewEventLog.EventRecordList.Add(eventRecord);
                                }

                                m_FormViewEventLog.EventCount += (short)(newEventIndex - m_FormViewEventLog.EventIndex);
                                m_FormViewEventLog.EventIndex = newEventIndex;

                                // Update the DataGridView control in a thread safe way.
                                m_FormViewEventLog.Invoke(new AddListDelegate(m_FormViewEventLog.AddList), new object[] { eventRecordList });

                                // Hold the thread until the invoked method has completed.
                                m_FormViewEventLog.m_MutexDataGridView.WaitOne(DefaultMutexWaitDurationMs, false);
                                m_FormViewEventLog.m_MutexDataGridView.ReleaseMutex();
                            }
                            else
                            {
                                // TODO - ThreadPollEvent.Run(). Wraparound of the uint index value, requires special processing.
                            }
                        }
#endif
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
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the VCU.
        /// </summary>
        public ICommunicationEvent CommunicationInterface
        {
            get { return m_CommunicationInterface; }
            set { m_CommunicationInterface = value; }
        }

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
        /// Gets the feedback flag that indicates whether polling of the target hardware has been suspended.  
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
            get { return m_CommunicationFault; }
        }

        /// <summary>
        /// Gets the number of packets received since the class was instantiated.
        /// </summary>
        public long PacketCount
        {
            get { return m_PacketCount; }
        }
        #endregion - [Properties] -
    }
}
