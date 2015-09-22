#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Event
 * 
 *  File name:  CommunicationEventOffline.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/03/11    1.0     Sean.D          1.  First entry into TortoiseSVN.
 * 
 *  08/11/11    1.1     Sean.D          1.  Minor modifications to: (a) dynamically the load event list for specific data dictionaries; (b) add fake delays to the calls; 
 *                                          and (c) change the fetched data to be all zeroes.
 *                                          
 *  08/25/11    1.1     K.McD           1.  Replaced the Verisimilitude sleep values with constants.
 *                                      2.  Removed support for debug mode to be consistent with other sub-systems.
 *                                      3.  Added member variables to record: (a) the workset that is to be used to simulate the fault log parameters stored on the VCU 
 *                                          and the fault log data streams associated with the selected event log and (b) the sample count for all fault 
 *                                          logs associated with the selected event log.
 *                                      4.  Modified the constructor to initialize the workset used to simulate the fault log parameters etc.
 *                                      5.  Replaced hard coded values, other than 1 and 0, with constants.
 *                                      6.  Modified the ChangeEventLog() method to initialize the sample count associated with the selected event log.
 *                                      7.  Modified the LoadEventLog method to set the event count parameter to a value that ensures that all events defined in the 
 *                                          data dictionary are analyzed.
 *                                      8.  Modified the GetDefaultStreamInformation() method to use the: (a) sample count and (b) workset defined in the ChangeEventLog() 
 *                                          method and constructor respectively.
 *                                      9.  Modified the GetStreamInformation() method to call the GetDefaultStreamInformation() method.
 *                                      10. Modified the GetStream() method to use the GetStreamInformation() method as a basis for the data stream.
 *                                      
 *  07/24/13    1.2     K.McD           1.  Automatic update when all references to the Parameter.WatchSizeFaultLogMax constant were replaced by references to the 
 *                                          Parameter.WatchSizeFaultLog property.
 *                                          
 *  08/05/13    1.3     K.McD           1.  Changed the SleepMsVerisimilitudeGetRecord constant from 25 ms to 0 ms as the simulated time to load the event list was excessive.
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Common;
using Common.Communication;
using Common.Configuration;

using Event.Properties;

namespace Event.Communication
{
    /// <summary>
    /// Class to simulate communication with the target hardware with respect to the event sub-system.
    /// </summary>
    public class CommunicationEventOffline : CommunicationParentOffline, ICommunicationEvent
    {
        #region --- Constants ---
        /// <summary>
        /// The <c>CultureInfo</c> string used to represent - english (US). Value: "en-US";
        /// </summary>
        private const string CultureEnglishUS = "en-US";

        /// <summary>
        /// The maximum number of columns in a workset generated from the parameters of a datastream retrieved from the VCU.
        /// </summary>
        private const short VCUDataStreamColumnCountMax = 1;

        #region -[Sleep] -
        /// <summary>
        /// The sleep interval, in ms, to simulate real time delays for standard event log operations. Value: 250 ms.
        /// </summary>
        private const int SleepMsVerisimilitude = 250;

        /// <summary>
        /// The sleep interval, in ms, to simulate real time delays in retrieving the event record and event variable information. Value: 0 ms.
        /// </summary>
        private const int SleepMsVerisimilitudeGetRecord = 0;

        /// <summary>
        /// The sleep interval, in ms, to simulate real time delays in retrieving the fault history information. Value: 2,250 ms.
        /// </summary>
        private const int SleepMsVerisimilitudeGetFaultHistory = 2250;

        /// <summary>
        /// The sleep interval, in ms, to simulate real time delays in retrieving the data stream. Value: 3,000 ms.
        /// </summary>
        private const int SleepMsVerisimilitudeGetStream = 3000;

        /// <summary>
        /// The sleep interval, in ms, to simulate real time delays in geting and setting the stream information. Value: 2,000 ms.
        /// </summary>
        private const int SleepMsVerisimilitudeStreamInformation = 2000;
        #endregion -[Sleep] -

        /// <summary>
        /// The sample interval, in ms, for a standard fault log. Value: 30 ms.
        /// </summary>
        private const int SampleIntervalMsFaultLog = 30;

        /// <summary>
        /// The number of data samples in a standard fault log. Value: 167.
        /// </summary>
        private const int SampleCountFaultLog = 167;

        /// <summary>
        /// The conversion factor to convert the trip index to the sample count.
        /// </summary>
        private const double ConversionTripIndexToSampleCount = 1.6667;

        /// <summary>
        /// The number of watch variables defined in a standard fault log. Value: 16.
        /// </summary>
        private const int WatchVariableCountFaultLog = 16;

        /// <summary>
        /// The maximum number of tasks associated with the project. Value: 16.
        /// </summary>
        private const int MaxTasks = 16;

        /// <summary>
        /// The maximum number of events that can be handled per task. Value: 80.
        /// </summary>
        private const int MaxEventsPerTasks = 80;

        /// <summary>
        /// The sample multiple of the log i.e. the multiple of the base sampling rate at which the data is to be sampled. Value: 1.
        /// </summary>
        private const int SampleMultiple = 1;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The workset that is to be used to simulate the fault log parameters and data stream.
        /// </summary>
        private Workset_t m_Workset;

        /// <summary>
        /// The sample count of all fault logs associated with the current event log.
        /// </summary>
        private int m_SampleCountFaultLog;
        #endregion --- Memeber Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class and set the properties and member variables to those values associated with the specified communication interface.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface containing the properties and member variables that are to be used to 
        /// initialize the class.</param>
        public CommunicationEventOffline(ICommunicationParent communicationInterface)
            : base(communicationInterface)
        {
            // --------------------------------------------------------------------------------------------
            // Get the workset that is to be used to simulate the fault log parameters and the data stream.
            // --------------------------------------------------------------------------------------------

            // If it has been configured, get the first valid workset other than the baseline workset; otherwise, use the baseline workset.
            int worksetIndex = (Workset.FaultLog.Worksets.Count > 0) ? 1 : 0;
            Workset_t workset = Workset.FaultLog.Worksets[worksetIndex];
            m_Workset = workset;
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Change the current event log on the VCU to the specified log and initialize the: DataRecordingRate, ChangeStatus, MaxTasks and MaxEventsPerTask properties 
        /// of the log structure with the values returned from the VCU.
        /// </summary>
        /// <param name="log">The required event log.</param>
        /// <remarks>The identifier field of the specified log must be initialized prior to calling this method. Note: The call to the ChangeEventLog() method in 
        /// PTUDLL32.dll refers to the eventLogIndex, whereas the log structure uses the log identifier. The event log index is zero based and is equivalent 
        /// to the event log identifier - 1.</remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ChangeEventLog() method is not CommunicationError.Success.</exception>
        public void ChangeEventLog(Log log)
        {
            short logIndex, sampleIntervalMs, changeStatus, maxTasks, maxEventsPerTask;
            logIndex = (short)(log.Identifier - 1);
            Debug.Assert(logIndex >= 0, "CommunicationEventOffline.ChangeEventLog() - [logIndex >= 0]");

            // Get the sample count of all fault logs associated with the selected event log.
            int tripIndex = Lookup.LogTable.Items[log.Identifier].DataStreamTypeParameters.TripIndex;
            m_SampleCountFaultLog = (int)Math.Round(tripIndex * ConversionTripIndexToSampleCount,0);

            sampleIntervalMs = SampleIntervalMsFaultLog;
            changeStatus = 1;
            maxTasks = MaxTasks;
            maxEventsPerTask = MaxEventsPerTasks;

            log.SampleIntervalMs = sampleIntervalMs;
            log.ChangeStatus = changeStatus;
            log.MaxTasks = maxTasks;
            log.MaxEventsPerTask = maxEventsPerTask;
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Load the current event log into memory.
        /// </summary>
        /// <param name="eventCount">The number of events that have been loaded into memory.</param>
        /// <param name="oldIndex">The old event index.</param>
        /// <param name="newIndex">The new event index.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.LoadFaultlog() method is not CommunicationError.Success.</exception>
        public void LoadEventLog(out short eventCount, out uint oldIndex, out uint newIndex)
        {
            // Ensure that the event count exceeds the maximum number of events that have been defined for the project. 
            eventCount= (short)(Lookup.EventTable.IdentifierMax + 1);

            // The following values are not used by the calling class.
            oldIndex = 0;
            newIndex = 0;
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Get the event record, including the event variable values, corresponding to the specified event index for the event log that is currently loaded into memory.
        /// </summary>
        /// <param name="currentEventLog">The event log that has been loaded into memory using the LoadEventLog() method.</param>
        /// <param name="eventIndex">The event index of the required event record.</param>
        /// <param name="eventRecord">The event record corresponding to the specified event index.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFaultHdr() method is not CommunicationError.Success.</exception>
        public void GetEventRecord(Log currentEventLog, short eventIndex, out EventRecord eventRecord)
        {
            if (eventIndex < 0)
            {
                eventRecord = null;
                return;
            }

            String time, date;
            time = DateTime.Now.ToString(CommonConstants.FormatStringTimeSec);
            date = DateTime.Now.ToString(CommonConstants.FormatStringDateFromVCU);

            short logIndex = (short)(currentEventLog.Identifier - 1);

            short lookupIndex = 0, i = 0;
            if (eventIndex < Lookup.EventTable.IdentifierMax)
            {
                do
                {
                    while (Lookup.EventTable.Items[lookupIndex] == null)
                        ++lookupIndex;
                    if (i == eventIndex)
                        break;
                    ++i;
                    ++lookupIndex;
                }
                while (lookupIndex < Lookup.EventTable.IdentifierMax);

                eventRecord = new EventRecord(lookupIndex);

                if (eventRecord.LogIdentifier != logIndex)
                {
                    eventRecord = null;
                    return;
                }
            }
            else    // Invalid index
            {
                eventRecord = null;
                return;
            }

            // Add the record VCU fields.
            eventRecord.LogIdentifier = currentEventLog.Identifier;
            eventRecord.EventIndex = lookupIndex;
            eventRecord.Time = time;
            eventRecord.Date = date;

            // Convert the date and time retrieved from the VCU to a .NET DateTime object.
            try
            {
                eventRecord.DateTime = DateTime.Parse(date + CommonConstants.Space + time, new CultureInfo(CultureEnglishUS, false));
            }
            catch (FormatException)
            {
                eventRecord.DateTime = Parameter.InvalidDateTime;
            }

            if (eventRecord.StreamNumber == CommonConstants.False)
            {
                eventRecord.StreamSaved = false;
            }
            else
            {
                eventRecord.StreamSaved = true;
            }

            // ---------------------------------------------------------
            // Get the event variables associated with the event record.
            // ---------------------------------------------------------
            
            // Define the data types associated with each event variable aasociated with the selected event.
            short[] types = new short[eventRecord.EventVariableList.Count];
            for (int index = 0; index < eventRecord.EventVariableList.Count; index++)
            {
                types[index] = (short)eventRecord.EventVariableList[index].DataType;
            }

            // -----------------------------------------------------------------------------------------------------
            // Retrieve and store the raw values for each of the event variables associated with the event.
            // -----------------------------------------------------------------------------------------------------
            double[] values;
            GetFaultVar(eventRecord.EventIndex, (short)eventRecord.EventVariableList.Count, types, out values);

            // Store the raw event variable values.
            for (int index = 0; index < eventRecord.EventVariableList.Count; index++)
            {
                eventRecord.EventVariableList[index].ValueFromTarget = values[index];
            }
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitudeGetRecord);
#endif
        }

        /// <summary>
        /// Clear the event log memory.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.FreeEventLogMemory() method is not CommunicationError.Success.</exception>
        public void FreeEventLogMemory()
        {
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Get the index of the event log that is currently loaded into memory.
        /// </summary>
        /// <remarks>This method is not used.</remarks>
        /// <param name="eventLogIndex">The index of the event log that is currently loaded into memory.</param>
        /// <param name="eventLogCount">The number of event logs supported by the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetEventLog() method is not CommunicationError.Success.</exception>
        public void GetEventLogIndex(out short eventLogIndex, out short eventLogCount)
        {
            eventLogIndex = 0;
            eventLogCount = 1;
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Retrieve the event variables associated with the specified event.
        /// </summary>
        /// <remarks>The number of events consist of the event variables that are collected for every event plus the event specific variables. The event variables that 
        /// are collected for each event consist of those event variables associated with the structure identifier value of 0 less those defined as event header variables.</remarks>
        /// <param name="eventIndex">The index of the event.</param>
        /// <param name="eventVariableCount">The number of event variable that are to be retrieved.</param>
        /// <param name="dataTypes">The data types associated with each of the variables.</param>
        /// <param name="values">The event variable values associated with each event.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFaultVar() method is not CommunicationError.Success.</exception>
        public void GetFaultVar(short eventIndex, short eventVariableCount, short[] dataTypes, out double[] values)
        {
            // Instantiate an array of doubles to hold the return values. Default values are all zeroes
            values = new double[dataTypes.Length];
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitudeGetRecord);
#endif
        }

        /// <summary>
        /// Initialize the event log. Clears all event information stored on battery backed RAM for both the maintenance and engineering logs. This also clears 
        /// both the cumulative history, recent history columns and all data logs. This function is typically used to establish a zero event/fault reference base when a replacement 
        /// VCU is installed in a car.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.InitializeEventLog() method is not CommunicationError.Success.</exception>
        public void InitializeEventLog()
        {
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Clear the current event log. Remove all events contained in the active event log.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ClearEvent() method is not CommunicationError.Success.</exception>
        public void ClearEvent()
        {
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Exit the event sub-system.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ExitEventLog() method is not CommunicationError.Success.</exception>
        public void ExitEventLog()
        {
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Get the parameters associated with the default stream.
        /// </summary>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables contained within the data stream.</param>
        /// <param name="dataTypes">The data types corresponding to each of the watch variables contained within the data stream.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetDefaultStreamInformation() method is not CommunicationError.Success.</exception>
        public void GetDefaultStreamInformation(out short watchVariableCount, out short sampleCount, out short sampleMultiple, out short[] watchIdentifiers, out short[] dataTypes)
        {
            sampleCount = (short)m_SampleCountFaultLog;
            sampleMultiple = SampleMultiple;

            // If it has been configured, get the first valid workset other than the baseline workset; otherwise, use the baseline workset.
            watchVariableCount = (short)m_Workset.Column[0].OldIdentifierList.Count;

            // Adjust the length of the output array to the correct size.
            watchIdentifiers = new short[watchVariableCount];
            dataTypes = new short[watchVariableCount];

            // Populate the watchIdentifiers array with the watch identifiers associated with the baseline workset.
            WatchVariable watchVariable = new WatchVariable();
            for (int watchElementIndex = 0; watchElementIndex < m_Workset.Column[0].OldIdentifierList.Count; watchElementIndex++)
            {
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[m_Workset.Column[0].OldIdentifierList[watchElementIndex]];
                    if (watchVariable == null)
                    {
                        // Set to the 'not defined' values.
                        watchVariable.Identifier = CommonConstants.WatchIdentifierNotDefined;
                        watchVariable.DataType = DataType_e.u32;
                    }
                    watchIdentifiers[watchElementIndex] = watchVariable.Identifier;
                    dataTypes[watchElementIndex] = (short)watchVariable.DataType;
                }
                catch (Exception)
                {
                    // Set to the 'not defined' values.
                    watchVariable.Identifier = CommonConstants.WatchIdentifierNotDefined;
                    watchVariable.DataType = DataType_e.u32;
                }

                watchIdentifiers[watchElementIndex] = watchVariable.Identifier;
                dataTypes[watchElementIndex] = (short)watchVariable.DataType;
            }
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitudeStreamInformation);
#endif
        }

        /// <summary>
        /// Get the parameters associated with the specified stream.
        /// </summary>
        /// <param name="streamNumber">The stream number.</param>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables contained within the data stream.</param>
        /// <param name="dataTypes">The data type corresponding to each watch variable contained within the data stream.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetStreamInformation() method is not CommunicationError.Success.</exception>
        public void GetStreamInformation(short streamNumber, out short watchVariableCount, out short sampleCount, out short sampleMultiple, out short[] watchIdentifiers, out short[] dataTypes)
        {
            GetDefaultStreamInformation(out watchVariableCount, out sampleCount, out sampleMultiple, out watchIdentifiers, out dataTypes);
        }

        /// <summary>
        /// Get the fault log/snapshot log data stream corresponding to the specified record.
        /// </summary>
        /// <param name="eventRecord">The event record associated with the data stream that is to be downloaded.</param>
        /// <returns>The data stream corresponding to the specified event record.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the calls to the PTUDLL32.GetStreamInformation() or PTUDLL32Event.GetStream() 
        /// methods is not CommunicationError.Success.</exception>
        public DataStream_t GetStream(EventRecord eventRecord)
        {
            short watchVariableCount, sampleCount, sampleMultiple;
            short[] watchIdentifiers, dataTypes;
            GetStreamInformation(0, out watchVariableCount, out sampleCount, out sampleMultiple, out watchIdentifiers, out dataTypes);

            int[] buffer = new int[sampleCount * watchVariableCount];   // Default values are all zeroes.

            // Not used by the calling class.
            // short timeOrigin = 0;

            Workset_t workset = ConvertToWorkset(eventRecord.Description, watchIdentifiers, sampleMultiple);
            DataStream_t dataStream = new DataStream_t(eventRecord, watchVariableCount, sampleCount, sampleMultiple, workset);

            DateTime startTime = eventRecord.DateTime.Subtract(new TimeSpan(0, 0, 0, 0, dataStream.DurationPreTripMs));

            // Convert the retrieved data stream into a list of individual, time-stamped watch variable data frames.
            List<WatchFrame_t> watchFrameList = ConvertToWatchFrameList(startTime, sampleCount, dataStream.FrameIntervalMs, buffer, dataTypes, workset);

            dataStream.WatchFrameList = watchFrameList;

            // Configure the upper and lower display limits.
            dataStream.ConfigureAutoScale();
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitudeGetStream);
#endif
            return dataStream;
        }

        /// <summary>
        /// Set the default stream parameters.
        /// </summary>
        /// <param name="sampleMultiple">The sample multiple of the recording interval at which the data is to be recorded.</param>
        /// <param name="oldIdentifierList">The list of old identifiers associated with the watch variables that are to be recorded in the fault log, in the 
        /// order in which they are to appear.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetDefaultStreamInformation() method is not 
        /// CommunicationError.Success.</exception>
        public void SetDefaultStreamInformation(short sampleMultiple, List<short> oldIdentifierList)
        {
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitudeStreamInformation);
#endif
        }

        /// <summary>
        /// Check the current event log for new events.
        /// </summary>
        /// <param name="eventCount">The number of new events that have been added to the event log.</param>
        /// <param name="newIndex">The new index of the latest event.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.CheckFaultlogger() method is not 
        /// CommunicationError.Success.</exception>
        public void CheckFaultlogger(out short eventCount, out uint newIndex)
        {
            eventCount = 1;
            newIndex = 0;
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Get the status of the flags that control: (a) whether the event type is enabled and (b) whether the event type triggers the recoding of a data stream. 
        /// </summary>
        /// <param name="validFlags">An array of flags that define which of the available event types are valid for the current log. The total length of the array is 
        /// the maximum number of events per task multiplied by the maximum number of tasks and the array element corresponding to a particular event type is defined as: 
        /// {task identifier} * {maximum number of events per task} + {event identifier}. True, indicates that the event type is valid; otherwise, false.</param>
        /// <param name="enabledFlags">An array of flags that indicate whether the event type is enabled. True, indicates that the event type is enabled; otherwise, false.</param>
        /// <param name="streamTriggeredFlags">An array of flags that indicate whether the event type triggers the recording of a data stream. True, indicates that the 
        /// event type triggers the recording of a data stream; otherwise false.</param>
        /// <param name="eventCount">The maximum number of event types i.e. the maximum number of event types per task multiplied by the maximum number of tasks.</param>
        /// <remarks>The size of the <paramref name="enabledFlags"/> and <paramref name="streamTriggeredFlags"/> arrays is equal to the number of defined 
        /// event types associated with the current log. The array index is mapped to a table that is derived by sorting the records of the EVENTS table of the data 
        /// dictionary corresponding to the LOGID field associated with the current log by the TASKID, EVENTID fields, in ascending order. </remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFltFlagInfo() method is not CommunicationError.Success.</exception>
        public void GetFltFlagInfo(short[] validFlags, ref short[] enabledFlags, ref short[] streamTriggeredFlags, short eventCount)
        {
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Set the flag that controls: (a) whether the specified event type is enabled and (b) whether the event type triggers the recoding of a data stream. 
        /// </summary>
        /// <param name="taskIdentfier">The task identifier associated with the event type.</param>
        /// <param name="eventIdentifier">The event identifier associated with the event type.</param>
        /// <param name="enabledFlag">A flag to control whether the event type corresponding to the specified task and event identifiers is to be enabled. True, if 
        /// the event type is to be enabled; otherwise, false.</param>
        /// <param name="streamTriggeredFlag">A flag to control whether the event type corresponding to the specified task and event identifiers is to trigger the 
        /// recording of a data stream. True, if the event type is to trigger the recording of a data stream; otherwise, false.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetFaultFlags() method is not 
        /// CommunicationError.Success.</exception>
        public void SetFaultFlags(short taskIdentfier, short eventIdentifier, short enabledFlag, short streamTriggeredFlag)
        {
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitude);
#endif
        }

        /// <summary>
        /// Get the event history associated with the current log.
        /// </summary>
        /// <param name="validFlags">An array of flags that define which of the available event types are valid for the current log. The total length of the array is 
        /// the maximum number of events per task multiplied by the maximum number of tasks and the array element corresponding to a particular event type is defined as: 
        /// {task identifier} * {maximum number of events per task} + {event identifier}. True, indicates that the event type is valid; otherwise, false.</param>
        /// <param name="cumulativeHistoryCounts">An array that contains the cumulative number of events of each event type, not including recent history.</param>
        /// <param name="recentHistoryCounts">An array that contains the recent number of events of each event type.</param>
        /// <param name="maxTasks">The maximum number of tasks that are supported by the current event log.</param>
        /// <param name="maxEventsPerTask">The maximum number of events per task that are supported by the current event log.</param>
        /// <remarks>The size of the <paramref name="cumulativeHistoryCounts"/> and <paramref name="recentHistoryCounts"/> arrays is equal to the number of defined 
        /// event types associated with the current log. The array index is mapped to a table that is derived by sorting the records of the EVENTS table of the data 
        /// dictionary corresponding to the LOGID field associated with the current log by the TASKID, EVENTID fields, in ascending order. </remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFltHistInfo() method is not CommunicationError.Success.</exception>
        public void GetFltHistInfo(short[] validFlags, ref short[] cumulativeHistoryCounts, ref short[] recentHistoryCounts, short maxTasks, short maxEventsPerTask)
        {
#if NODELAY
#else
            // Include a delay to provide verisimilitude.
            System.Threading.Thread.Sleep(SleepMsVerisimilitudeGetFaultHistory);
#endif
        }

        #region - [Support Methods] -
        /// <summary>
        /// Convert the data stream values retrieved from the VCU to a format that can be plotted by the <c>FormViewDataStream</c> class.
        /// </summary>
        /// <remarks>
        /// The value array retrieved by from the VCU is initially mapped to the WatchIdentifierList property of Column[0] of the workset, however, the WatchElement 
        /// array associated with each frame must be mapped to the WatchElementList property of the workset.
        /// </remarks>
        /// <param name="startTime">The start time of the fault log.</param>
        /// <param name="sampleCount">The number of samples in the data stream.</param>
        /// <param name="frameIntervalMs">The interval, in ms, between consecutive data frames.</param>
        /// <param name="values">The point values corresponding to each variable.</param>
        /// <param name="dataTypes">The data types associated with each value.</param>
        /// <param name="workset">The workset that is to be used to define the output format.</param>
        /// <returns>The watch variable values in the format that can be plotted by the <c>FormViewDataStream</c> class.</returns>
        /// <exception cref="ArgumentException">Thrown if one or more of the the watch identifiers defined in the WatchElementList property of the workset does nor exist.</exception>
        private List<WatchFrame_t> ConvertToWatchFrameList(DateTime startTime, short sampleCount, short frameIntervalMs, int[] values, short[] dataTypes, Workset_t workset)
        {
            Debug.Assert(sampleCount > 1, "CommunicationEvent.ConvertToWatchFrameList() - [pointCount > 1]");
            Debug.Assert(frameIntervalMs > 0 , "CommunicationEvent.ConvertToWatchFrameList() - [frameIntervalMs > 0");
            Debug.Assert(values != null, "CommunicationEvent.ConvertToWatchFrameList() - [values != null]");
            Debug.Assert(dataTypes != null, "CommunicationEvent.ConvertToWatchFrameList() - [dataTypes != null]");

            short watchCount = (short)workset.WatchElementList.Count;

            // Create a look-up array to translate the watch variable data retrieved from the VCU to the order defined by the WatchElementList property of the workset.
            short[] translate = new short[watchCount];
            short watchIdentifier, rowIndex, columnIndex;
            WatchVariable watchVariable;
            for (short watchElementIndex = 0; watchElementIndex < watchCount; watchElementIndex++)
            {
                watchIdentifier = workset.WatchElementList[watchElementIndex];
                try
                {
                    watchVariable = Lookup.WatchVariableTable.Items[watchIdentifier];
                    if (watchVariable == null)
                    {
                        throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
                    }
                }
                catch (Exception)
                {
                    throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
                }

                workset.GetWatchVariableLocation(watchVariable.OldIdentifier, out columnIndex, out rowIndex);
                Debug.Assert(((columnIndex != CommonConstants.NotFound) && (rowIndex != CommonConstants.NotFound)), "CommunicationEvent.ConvertToWatchFrameList() - [((columnIndex != 0) || (rowIndex != CommonConstants.NotFound))]");

                translate[watchElementIndex] = rowIndex;
            }

            // Translate the values of the watch variables retrieved from the VCU to a list of watch frames.
            List<WatchFrame_t> watchFrameList = new List<WatchFrame_t>();
            WatchFrame_t watchFrame;
            WatchElement_t watchElement;
            for (int frameIndex = 0; frameIndex < sampleCount; frameIndex++)
            {
                watchFrame = new WatchFrame_t();
                watchFrame.CurrentDateTime = startTime.AddMilliseconds(frameIndex * frameIntervalMs);
                watchFrame.WatchElements = new WatchElement_t[watchCount];
                for (short watchElementIndex = 0; watchElementIndex < watchCount; watchElementIndex++)
                {
                    watchElement = new WatchElement_t();
                    watchElement.ElementIndex = watchElementIndex;
                    watchElement.WatchIdentifier = workset.WatchElementList[watchElementIndex];
                    watchElement.DataType = dataTypes[translate[watchElementIndex]];
                    watchElement.Value = (double)values[(frameIndex * watchCount) + translate[watchElementIndex]];
                    watchFrame.WatchElements[watchElementIndex] = watchElement;
                }
                watchFrameList.Add(watchFrame);
            }
            return watchFrameList;
        }

        /// <summary>
        /// Convert the specified data stream watch variable identifiers/parameters into a valid workset.
        /// </summary>
        /// <remarks>
        /// All of the watch identifiers returned from the VCU s are added to Column[0] of the workset in the order in which they appear in <paramref name="variableIdentifiers"/> and 
        /// the security level of the workset is set to the lowest security level.
        /// </remarks>
        /// <param name="name">The name of the workset.</param>
        /// <param name="variableIdentifiers">The array of watch identifiers.</param>
        /// <param name="sampleMultiple">The multiple of the recording interval at which the data is recorded.</param>
        /// <returns>The watch identifiers contained within <paramref name="variableIdentifiers"/> as a valid workset.</returns>
        public Workset_t ConvertToWorkset(string name, short[] variableIdentifiers, short sampleMultiple)
        {
            Debug.Assert(variableIdentifiers != null, "CommunicationEvent.ConvertToWorkset() - [variableIdentifiers != null]");
            Debug.Assert(name != string.Empty, "CommunicationEvent.ConvertToWorkset() - [name != string.Empty]");

            // Create a workset based upon those watch identifiers contained within the array.
            List<short> watchIdentifierList = new List<short>(variableIdentifiers.Length);
            for (short index = 0; index < variableIdentifiers.Length; index++)
            {
                watchIdentifierList.Add(variableIdentifiers[index]);
            }

            Workset_t workset = new Workset_t(name, watchIdentifierList, Parameter.WatchSizeFaultLog, VCUDataStreamColumnCountMax, new Security().SecurityLevelBase);

            workset.SampleMultiple = sampleMultiple;
            return workset;
        }
        #endregion - [Support Methods] -
        #endregion --- Methods ---
    }
}