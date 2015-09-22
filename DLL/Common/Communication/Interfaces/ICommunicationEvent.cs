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
 *  Project:    Common
 * 
 *  File name:  ICommunicationEvent.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  12/30/10    1.1     K.McD           2.  Included the CheckFaultLogger() method.
 * 
 *  01/26/11    1.2     K.McD           1.  Renamed a number of variables and modified a number of XML tags.
 * 
 *  04/07/11    1.3     K.McD           1.  Added the methods required to configure the event flags and show the event history.
 *  
 *  05/23/11    1.4     K.McD           1.  Corrected an XML tag.
 *
 */
#endregion --- Revision History ---

using System.Collections.Generic;

using Common.Configuration;

namespace Common.Communication
{
    /// <summary>
    /// An interface to define the communication methods associated with the event sub-system - Event.dll.
    /// </summary>
    public interface ICommunicationEvent : ICommunicationParent
    {
        /// <summary>
        /// Change the current event log on the VCU to the specified log and initialize the: DataRecordingRate, ChangeStatus, MaxTasks and MaxEventsPerTask properties 
        /// of the log structure with the values returned from the VCU.
        /// </summary>
        /// <param name="log">The required event log.</param>
        /// <remarks>The identifier field of the specified log must be initialized prior to calling this method. Note: The call to the ChangeEventLog() method in 
        /// PTUDLL32.dll refers to the eventLogIndex, whereas the log structure uses the event log identifier. The event log index is zero based and is equivalent 
        /// to the event log identifier - 1.</remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ChangeEventLog() method is not
        /// CommunicationError.Success.</exception>
        void ChangeEventLog(Common.Configuration.Log log);

        /// <summary>
        /// Load the current event log into memory.
        /// </summary>
        /// <param name="eventCount">The number of events that have been loaded into memory.</param>
        /// <param name="oldIndex">The old event index.</param>
        /// <param name="newIndex">The new event index.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.LoadFaultlog() method is not
        /// CommunicationError.Success.</exception>
        void LoadEventLog(out short eventCount, out uint oldIndex, out uint newIndex);

        /// <summary>
        /// Get the event record corresponding to the specified index for the event log that is currently loaded into memory.
        /// </summary>
        /// <param name="currentEventLog">The event log that has been loaded into memory using the LoadEventLog() method.</param>
        /// <param name="eventIndex">The event index of the required event record.</param>
        /// <param name="eventRecord">The event record corresponding to the specified event index.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFaultHdr() method is not
        /// CommunicationError.Success.</exception>
        void GetEventRecord(Log currentEventLog, short eventIndex, out EventRecord eventRecord);

        /// <summary>
        /// Clear the event log memory.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.FreeEventLogMemory() method is not
        /// CommunicationError.Success.</exception>
        void FreeEventLogMemory();

        /// <summary>
        /// Get the index of the event log that is currently loaded into memory.
        /// </summary>
        /// <param name="eventLogIndex">The index of the event log that is currently loaded into memory.</param>
        /// <param name="eventLogCount">The number of event logs supported by the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetEventLog() method is not
        /// CommunicationError.Success.</exception>
        void GetEventLogIndex(out short eventLogIndex, out short eventLogCount);

        /// <summary>
        /// Retrieve the event variables associated with the specified event.
        /// </summary>
        /// <remarks>The number of events consist of the event variables that are collected for every event plus the event specific variables. The event variables that 
        /// are collected for each event consist of those event variables associated with the structure identifier value of 0 less those defined as event header
        /// variables.</remarks>
        /// <param name="eventIndex">The index of the event.</param>
        /// <param name="eventVariableCount">The number of event variable that are to be retrieved.</param>
        /// <param name="dataTypes">The data types associated with each of the variables.</param>
        /// <param name="values">The event variable values associated with each event.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFaultVar() method is not
        /// CommunicationError.Success.</exception>
        void GetFaultVar(short eventIndex, short eventVariableCount, short[] dataTypes, out double[] values);

        /// <summary>
        /// Initialize the event log. Clears all event information stored on battery backed RAM for both the maintenance and engineering logs. This also clears 
        /// both the cumulative history, recent history columns and all data logs. This function is typically used to establish a zero event/fault reference base when a
        /// replacement VCU is installed in a car.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.InitializeEventLog() method is not
        /// CommunicationError.Success.</exception>
        void InitializeEventLog();

        /// <summary>
        /// Clear the current event log. Remove all events contained in the active event log. This also erases any data logs associated with the event log.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ClearEvent() method is not
        /// CommunicationError.Success.</exception>
        void ClearEvent();

        /// <summary>
        /// Exit the event sub-system.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ExitEventLog() method is not
        /// CommunicationError.Success.</exception>
        void ExitEventLog();

        /// <summary>
        /// Get the parameters associated with the default stream.
        /// </summary>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables contained within the data stream.</param>
        /// <param name="dataTypes">The data types corresponding to each of the watch variables contained within the data stream.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetDefaultStreamInformation() method is not
        /// CommunicationError.Success.</exception>
        void GetDefaultStreamInformation(out short watchVariableCount, out short sampleCount, out short sampleMultiple, out short[] watchIdentifiers,
                                         out short[] dataTypes);

        /// <summary>
        /// Get the parameters associated with the specified stream.
        /// </summary>
        /// <param name="streamNumber">The stream number.</param>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables contained within the data stream.</param>
        /// <param name="dataTypes">The data type corresponding to each watch variable contained within the data stream.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetStreamInformation() method is not
        /// CommunicationError.Success.</exception>
        void GetStreamInformation(short streamNumber, out short watchVariableCount, out short sampleCount, out short sampleMultiple, out short[] watchIdentifiers,
                                  out short[] dataTypes);

        /// <summary>
        /// Get the fault log/snapshot log data stream corresponding to the specified record.
        /// </summary>
        /// <param name="eventRecord">The event record associated with the data stream that is to be downloaded.</param>
        /// <returns>The data stream corresponding to the specified event record.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the calls to the PTUDLL32.GetStreamInformation() or PTUDLL32Event.GetStream() 
        /// methods is not CommunicationError.Success.</exception>
        DataStream_t GetStream(EventRecord eventRecord);

        /// <summary>
        /// Set the default stream parameters.
        /// </summary>
        /// <param name="sampleMultiple">The sample multiple of the recording interval at which the data is to be recorded.</param>
        /// <param name="watchIdentifierList">The list of watch identifiers that are to be recorded.</param>
        void SetDefaultStreamInformation(short sampleMultiple, List<short> watchIdentifierList);

        /// <summary>
        /// Check the current event log for new events.
        /// </summary>
        /// <param name="eventCount">The number of new events that have been added to the event log.</param>
        /// <param name="newIndex">The new index of the latest event.</param>
        void CheckFaultlogger(out short eventCount, out uint newIndex);

        /// <summary>
        /// Get the status of the flags that control: (a) whether the event type is enabled and (b) whether the event type triggers the recoding of a data stream. 
        /// </summary>
        /// <param name="validFlags">An array of flags that define which of the available event types are valid for the current log. The total length of the array is 
        /// the maximum number of events per task multiplied by the maximum number of tasks and the array element corresponding to a particular event type is defined as: 
        /// {task identifier} * {maximum number of events per task} + {event identifier}. True, indicates that the event type is valid; otherwise, false.</param>
        /// <param name="enabledFlags">An array of flags that indicate whether the event type is enabled. True, indicates that the event type is enabled; otherwise,
        /// false.</param>
        /// <param name="streamTriggeredFlags">An array of flags that indicate whether the event type triggers the recording of a data stream. True, indicates that the 
        /// event type triggers the recording of a data stream; otherwise false.</param>
        /// <param name="eventCount">The maximum number of event types i.e. the maximum number of event types per task multiplied by the maximum number of tasks.</param>
        /// <remarks>The size of the <paramref name="enabledFlags"/> and <paramref name="streamTriggeredFlags"/> arrays is equal to the number of defined 
        /// event types associated with the current log. The array index is mapped to a table that is derived by sorting the records of the EVENTS table of the data 
        /// dictionary corresponding to the LOGID field associated with the current log by the TASKID, EVENTID fields, in ascending order. </remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFltFlagInfo() method is not
        /// CommunicationError.Success.</exception>
        void GetFltFlagInfo(short[] validFlags, ref short[] enabledFlags, ref short[] streamTriggeredFlags, short eventCount);

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
        void SetFaultFlags(short taskIdentfier, short eventIdentifier, short enabledFlag, short streamTriggeredFlag);

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
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFltHistInfo() method is not
        /// CommunicationError.Success.</exception>
        void GetFltHistInfo(short[] validFlags, ref short[] cumulativeHistoryCounts, ref short[] recentHistoryCounts, short maxTasks, short maxEventsPerTask);

        /// <summary>
        /// Convert the specified parameters into a valid workset.
        /// </summary>
        /// <remarks>
        /// All of the watch identifiers are added to Column[0] of the workset in the order in which they appear in <paramref name="variableIdentifiers"/> and 
        /// the security level of the workset is set to the lowest security level.
        /// </remarks>
        /// <param name="name">The name of the workset.</param>
        /// <param name="variableIdentifiers">The array of watch identifiers.</param>
        /// <param name="sampleMultiple">The multiple of the recording interval at which the data is recorded.</param>
        /// <returns>The watch identifiers contained within <paramref name="variableIdentifiers"/> as a valid workset.</returns>
        Workset_t ConvertToWorkset(string name, short[] variableIdentifiers, short sampleMultiple);
    }
}
