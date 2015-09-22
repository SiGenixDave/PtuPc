#region --- Revision History ---
/*
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Event
 * 
 *  File name:  VcuCommunication32Event.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/26/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/06/11    1.1     K.McD           2.  Modified the signatures associated with the CheckFaultLogger(), GetFltFlagInfo() and 
 *											GetFltHstory() methods.
 * 
 *  01/26/11    1.2     K.McD           1.  Modified the names of a number of variables.
 * 
 *  02/14/11    1.3     K.McD           1.  Modified the parameter name associated with the ChangeEventLog() method from 
 *											sampleMultiple to sampleIntervalMs.
 * 
 *  02/15/11    1.3.1   K.McD           1.  Changed the name of the streamSaved parameter associated with the GetFaultHdr() method
 *											to streamNumber.
 * 
 *  02/28/11    1.3.2   K.McD           1.  Changed the signature of the CheckFaultLogger() method.
 * 
 *  04/06/11    1.4     K.McD           1.  Added the methods required to: (a) configure the event flags associated with the 
 *											current log - GetFltFlagInfo(), 
 *                                          SetFaultFlags() and (b) display the event history - GetFltHistInfo().
 *                                          
 *  06/01/11    1.4.1   K.McD           1.  Removed the comments showing the PTUDLL32 prototypes and the Visual Basic function 
 *											declarations. No functional changes.
 *  
 *  02/27/14    1.5     K.McD           1.  Included the Event.cpp prototype information and re-ordered the methods so 
 *                                          that they appear in the same order as the prototype list.
 *                                          
 *                                      2.  Changed the String parameter modifiers of the GetFaultHdr() method to use 
 *											'[MarshalAs(UnmanagedType.BStr)] out String'.
 *                                      
 *	03/11/15	1.6		K.McD			References
 *	                                    1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order TBD.
 *	                                    
 *                                          1.  Implement changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications to
 *                                              support both 32 and 64 bit architecture.
 *                                      
 *                                      Modifications
 *                                      1.  Renamed class to VcuCommunicationEvent32.
 *                                      2.  Changed the DLLImport parameter to VcuCommunication32.dll.
 *
 */
#endregion --- Revision History ---

using System;
using System.Runtime.InteropServices;

namespace Common.Communication
{
    /// <summary>
    /// A managed wrapper class to allow the unmanaged C++ functions associated with the VcuCommunication32 dynamic link library (DLL) 
    /// to be accessed from within managed code. The VcuCommunication32 DLL supports the low level communication protocol with the 
    /// Vehicle Control Unit (VCU) when running on a 32 bit platform. VcuCommunication32.dll and VcuCommunication64.dll are functionally 
    /// identical, however, VcuCommunication32 is targeted towards Windows 32 bit operating systems.
    /// </summary>
    public class VcuCommunication32Event
    {
        #region - [VcuCommunication32.dll - Event.cpp Prototypes] -
        /*
        INT16 WINAPI LoadFaultlog					(INT16 *, unsigned long *, unsigned long *);
        INT16 WINAPI CheckFaultlogger				(INT16 *, unsigned long *);
        INT16 WINAPI GetFaultHdr					(INT16, INT16 *, INT16 *, BSTR*, BSTR*, INT16 *);
        INT16 WINAPI GetFaultVar					(INT16, INT16, INT16 *, double *);
        INT16 WINAPI GetFltFlagInfo					(INT16 *, INT16 *, INT16 *, INT16);
        INT16 WINAPI GetFltHistInfo					(INT16 *, INT16 *, INT16 *, INT16, INT16);
        INT16 WINAPI FreeEventLogMemory				(void);
        INT16 WINAPI SetFaultFlags					(INT16, INT16, INT16, INT16);
        INT16 WINAPI GetDatalogStatus				(INT16 *);
        INT16 WINAPI ClearEvent    					(void);
        INT16 WINAPI InitializeEventLog				(void);
        INT16 WINAPI GetStream						(INT16, long *, INT16 *, INT16, INT16, INT16 *);
        INT16 WINAPI GetStreamInformation			(INT16, INT16 *, INT16 *, INT16 *, INT16 *, INT16 *);
        INT16 WINAPI GetDefaultStreamInformation	(INT16 *, INT16 *, INT16 *, INT16 *, INT16 *);
        INT16 WINAPI SetDefaultStreamInformation	(INT16, INT16, INT16 *);
        INT16 WINAPI GetEventLog					(INT16 *, INT16 *);
        INT16 WINAPI ChangeEventLog					(INT16, INT16 *, INT16 *, INT16 *, INT16 *);
        INT16 WINAPI ExitEventLog					(void);
        INT16 WINAPI ReadCMSFaultData				(LPSTR, INT16 *);
        */
        #endregion - [VcuCommunication32.dll - Event.cpp Prototypes] -

        #region - [VcuCommunication32.dll - Event Functions] -
        /// <summary>
        /// Load the current event log into memory.
        /// </summary>
        /// <param name="eventCount">The number of events that have been loaded into memory.</param>
        /// <param name="oldIndex">The old event index.</param>
        /// <param name="newIndex">The new event index.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short LoadFaultlog(out short eventCount, out uint oldIndex, out uint newIndex);

        /// <summary>
        /// Check whether any new events have been added to the active event log.
        /// </summary>
        /// <param name="eventCount">The number of new events that have been added to the active event log.</param>
        /// <param name="newIndex">The index of the first new event.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short CheckFaultlogger(out short eventCount, out uint newIndex);

        /// <summary>
        /// Get the event record corresponding to the specified index for the event log that is currently loaded into memory.
        /// </summary>
        /// <param name="eventIndex">The event index of the required event.</param>
        /// <param name="eventIdentifier">The event identifier associated with the event.</param>
        /// <param name="taskIdentifier">The task identifier associated with the event.</param>
        /// <param name="time">The time that the event was raised.</param>
        /// <param name="date">The date that the event was raised.</param>
        /// <param name="streamNumber">The stream number of the data stream associated with the event, if one is available; otherwise, 
        /// -1.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetFaultHdr(short eventIndex, out short eventIdentifier, out short taskIdentifier, 
                                                      [MarshalAs(UnmanagedType.BStr)] out String time, 
                                                      [MarshalAs(UnmanagedType.BStr)] out String date, out short streamNumber);

        /// <summary>
        /// Retrieve the event variables associated with the specified event.
        /// </summary>
        /// <param name="eventIndex">The index of the event.</param>
        /// <param name="eventVariableCount">The number of event variables that are to be retrieved.</param>
        /// <param name="dataTypes">The data types associated with each of the event variables.</param>
        /// <param name="values">The values associated with each event variable.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetFaultVar(short eventIndex, short eventVariableCount, short[] dataTypes, double[] values);

        /// <summary>
        /// Get the status of the flags that control: (a) whether the event type is enabled and (b) whether the event type triggers the 
        /// recoding of a data stream. 
        /// </summary>
        /// <param name="validFlags">An array of flags that define which of the available event types are valid for the current log. The 
        /// total length of the array is the maximum number of events per task multiplied by the maximum number of tasks and the array 
        /// element corresponding to a particular event type is defined as: {task identifier} * {maximum number of events per task} + 
        /// {event identifier}. True, indicates that the event type is valid; otherwise, false.</param>
        /// <param name="enabledFlags">An array of flags that indicate whether the event type is enabled. True, indicates that the event 
        /// type is enabled; otherwise, false.</param>
        /// <param name="streamTriggeredFlags">An array of flags that indicate whether the event type triggers the recording of a data 
        /// stream. True, indicates that the 
        /// event type triggers the recording of a data stream; otherwise false.</param>
        /// <param name="eventCount">The maximum number of event types i.e. the maximum number of event types per task multiplied by the maximum
        /// number of tasks.</param>
        /// <remarks>The size of the <paramref name="enabledFlags"/> and <paramref name="streamTriggeredFlags"/> arrays is equal to the number of
        /// defined event types associated with the current log. The array index is mapped to a table that is derived by sorting the records of the
        /// EVENTS table of the data dictionary corresponding to the LOGID field associated with the current log by the TASKID, EVENTID fields,
        /// in ascending order. </remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetFltFlagInfo(short[] validFlags, short[] enabledFlags, short[] streamTriggeredFlags, 
                                                         short eventCount);

        /// <summary>
        /// Get the event history associated with the current log.
        /// </summary>
        /// <param name="valid">An array of flags that define which of the available event types are valid for the current log. The total length of
        /// the array is the maximum number of events per task multiplied by the maximum number of tasks and the array element corresponding to a
        /// particular event type is defined as: {task identifier} * {maximum number of events per task} + {event identifier}. True, indicates that
        /// the event type is valid; otherwise, false.</param>
        /// <param name="cumulativeHistoryCount">An array that contains the cumulative number of events of each event type, not including recent
        /// history.</param>
        /// <param name="recentHistoryCount">An array that contains the recent number of events of each event type.</param>
        /// <param name="maxTasks">The maximum number of tasks that are supported by the current event log.</param>
        /// <param name="maxEventsPerTask">The maximum number of events per task that are supported by the current event log.</param>
        /// <remarks>The size of the <paramref name="cumulativeHistoryCount"/> and <paramref name="recentHistoryCount"/> arrays is equal to the number
        /// of defined event types associated with the current log. The array index is mapped to a table that is derived by sorting the records of the
        /// EVENTS table of the data dictionary corresponding to the LOGID field associated with the current log by the TASKID, EVENTID fields, in
        /// ascending order. </remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetFltHistInfo(short[] valid, short[] cumulativeHistoryCount, short[] recentHistoryCount, 
                                                         short maxTasks, short maxEventsPerTask);

        /// <summary>
        /// Clear the event log memory.
        /// </summary>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short FreeEventLogMemory();

        /// <summary>
        /// Set the flag that controls: (a) whether the specified event type is enabled and (b) whether the event type triggers the recoding of a 
        /// data stream. 
        /// </summary>
        /// <param name="taskIdentfier">The task identifier associated with the event type.</param>
        /// <param name="eventIdentifier">The event identifier associated with the event type.</param>
        /// <param name="enabledFlag">A flag to control whether the event type corresponding to the specified task and event identifiers is to be
        /// enabled. True, if the event type is to be enabled; otherwise, false.</param>
        /// <param name="streamTriggeredFlag">A flag to control whether the event type corresponding to the specified task and event identifiers is 
        /// to trigger the recording of a data stream. True, if the event type is to trigger the recording of a data stream; otherwise, false.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short SetFaultFlags(short taskIdentfier, short eventIdentifier, short enabledFlag, 
                                                        short streamTriggeredFlag);

        /// <summary>
        /// Clear the active event log.
        /// </summary>
        /// <remarks>
        /// This call removes all events contained in the active event log and erases any data logs associated with the event log.
        /// </remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short ClearEvent();

        /// <summary>
        /// Initialize the event sub-system.
        /// </summary>
        /// <remarks>
        /// Clears all event information stored on battery backed RAM for both the maintenance and engineering logs. This call also clears the cumulative history, 
        /// recent history columns and all data logs. This function is typically used to establish a zero event/fault reference base when a replacement 
        /// VCU is installed in a car.
        /// </remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short InitializeEventLog();

        /// <summary>
        /// Get the data stream.
        /// </summary>
        /// <param name="streamNumber">The stream number.</param>
        /// <param name="dataBuffer">The point values for each watch variable.</param>
        /// <param name="timeOrigin">The start time of the plot.</param>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="dataTypes">The data type corresponding to each watch variable contained within the data stream.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetStream(short streamNumber, int[] dataBuffer, out short timeOrigin, short watchVariableCount, 
                                                    short sampleCount, short[] dataTypes);

        /// <summary>
        /// Get the parameters associated with the specified stream.
        /// </summary>
        /// <param name="streamNumber">The stream number.</param>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables contained within the data stream.</param>
        /// <param name="dataTypes">The data type corresponding to each watch variable contained within the data stream.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetStreamInformation(short streamNumber, out short watchVariableCount, out short sampleCount, 
                                                               out short sampleMultiple, short[] watchIdentifiers, short[] dataTypes);

        /// <summary>
        /// Get the parameters associated with the default stream.
        /// </summary>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables contained within the data stream.</param>
        /// <param name="dataTypes">The data types corresponding to each of the watch variables contained within the data stream.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetDefaultStreamInformation(out short watchVariableCount, out short sampleCount, 
                                                                      out short sampleMultiple, short[] watchIdentifiers, 
                                                                      short[] dataTypes);

        /// <summary>
        /// Set the default stream parameters.
        /// </summary>
        /// <param name="watchVariableCount">The number of watch variables that are to be included in the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is to be sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables that are to be included in the data stream.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short SetDefaultStreamInformation(short watchVariableCount, short sampleMultiple, 
                                                                      short[] watchIdentifiers);

        /// <summary>
        /// Get the index of the event log that is currently loaded into memory.
        /// </summary>
        /// <param name="eventLogIndex">The index of the event log that is currently loaded into memory.</param>
        /// <param name="eventLogCount">The number of event logs supported by the VCU.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetEventLog(out short eventLogIndex, out short eventLogCount);

        /// <summary>
        /// Change the current event log on the VCU to the specified log.
        /// </summary>
        /// <param name="eventLogIndex">The index of the required event log.</param>
        /// <param name="sampleIntervalMs">The base interval, in ms, between samples.</param>
        /// <param name="changeStatus">A flag to indicate whether new events have been added to the log.</param>
        /// <param name="maxTasks">The maximum number of tasks associated with the log.</param>
        /// <param name="maxEventsPerTask">The maximum number of events per task.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short ChangeEventLog(short eventLogIndex, out short sampleIntervalMs, out short changeStatus, 
                                                         out short maxTasks, out short maxEventsPerTask);

        /// <summary>
        /// Exit the event sub-system.
        /// </summary>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("PTUDLL32.dll")]
        public static extern unsafe short ExitEventLog();
        #endregion - [VcuCommunication32.dll - Event Functions] -
    }
}
