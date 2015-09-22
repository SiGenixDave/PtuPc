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
 *  File name:  CommunicationEvent.cs
 * 
 *  Revision History
 *  ----------------
 */

#region - [1.0 to 1.10] -
/* 
 *  Date        Version Author          Comments
 *  10/26/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/17/10    1.1     K.McD           1.  Modified the ConvertToWorkset() method to take into account the changes to the Workset_t structure
 *                                          constructor.
 * 
 *  11/26/10    1.2     K.McD           1.  Added the SetDefaultStreamInformation() method.
 *                                      2.  Modified the GetStream() method to use the GetDefaultStreamInformation() method rather than the
 *                                          GetStreamInformation() method.
 * 
 *  01/06/11    1.3     K.McD           1.  Minor changes to a number of XML tags and variable names.
 *                                      2.  Modified the GetEventRecord() method to also retrieve the event variables associated with the record.
 *                                      3.  Added the CheckFaultLogger() method to check the current event log for new events.
 *                                      4.  Modified the signature associated with the ConvertToWatchFrameList() method so that the workset is passed
 *                                          rather than an array of watch identifiers.
 * 
 *  01/10/11    1.4     K.McD           1.  Modified the GetStream() method to set the data stream number to be equal to the event record index + 1.
 * 
 *  01/18/11    1.5     K.McD           1.  Bug fix SNCR001.79 - Modified the GetStream() method to use the StreamNumber property of the EventRecord
 *                                          class as the streamNumber parameter when making the call to the PTUDLL32.GetStream() method.
 * 
 *  01/26/11    1.6     K.McD           1.  Auto-modified as a result of the property name changes associated with the Log class.
 *                                      2.  Modified the XML tags and the names of a number of variables.
 *                                      3.  Modified the ConvertToWorkset() method to create a workset based on the number of parameters asssociated
 *                                          with the retrieved data stream.
 *                                      4.  Modified the GetStream() method to use the new signature associated with the DataStream_t structure.
 * 
 *  01/26/11    1.7     K.McD           1.  Bug fix SNCR001.97. Modified the ConvertToWorkset() method to use the Parameter.WatchSizeFaultLogMax
 *                                          constant to specify the entryCountMax parameter when instantiating the Workset_t structure.
 * 
 *  01/31/11    1.8     K.McD           1.  Included support for the mutex, introduced in version 1.11 of Common.dll, used to control read/write
 *                                          access to the communication port.
 * 
 *  02/14/11    1.9     K.McD           1.  Removed unused constructors.
 *                                      2.  Removed the constant StreamSize and modified the GetStreamInformation() method to use the
 *                                          Parameter.WatchSizeFaultLogMax property instead.
 *                                      3.  Included support for debug mode.
 * 
 *  02/15/11    1.9.1   K.McD           1.  Modified the GetEventRecord() method to store the stream number parameter returned from the call to
 *                                          PTUDLL32.GetaultHdr() in the StreamNumber field of the event record parameter.
 * 
 *  02/15/11    1.9.2   K.McD           1.  Bug fix - SNCR001.102. Modified the GetEventRecord() method to return a null value for the eventRecord
 *                                          parameter and to inform the user if an event corresponding to the specified LOGID, TASKID and EVENTID
 *                                          fields cannot be found in the EVENTS table of the data dictionary.
 * 
 *                                      2.  Modified the GetStream() method the use the PTUDLL32.GetStreamInformation() method rather than 
 *                                          PTUDLL32.GetDefaultStreamInformation(). This means that the current event log need not be cleared when
 *                                          updating the data stream parameters. 
 * 
 *  02/28/11    1.9.3   K.McD           1.  Modified the CheckFaultLogger() method signature.
 *                                      2.  Modified the CheckFaultLogger() method  such that the newIndex local variable used in the call to the
 *                                          PTUDLL32.CheckFaultLogger() method in not initilalized to zero.
 *                                      3.  Corrected the text associated with a number of CommunicationException message strings.
 * 
 *  03/28/11    1.9.4   K.McD           1.  Auto-modified as a result of a number of name changes to the properties and methods of external classes.
 *                                      2.  Modified the GetStream() method to use the event description as the name of the workset.
 *                                      3.  Modified the SetDefaultStreamInformation() method to derive the watch identifier values from the old
 *                                          identifier values stored in the Column property of the workset.
 *                                      4.  Modified the ConvertToWatchFrameList() to support the modified Workset_t structure.  
 *                                      5.  Modified the ConvertToWorkset() method to support the modified Workset_t structure and to use the
 *                                          constructor of the Workset_t structure to create the new workset.
 *                                      6.  Removed the CompareWatchIdentifiers() method.
 * 
 *  04/06/11    1.9.5   K.McD           1.  Added the methods required to: (a) configure the event flags associated with the current log -
 *                                          GetFltFlagInfo(), SetFaultFlags() and (b) display the event history - GetFltHistInfo().
 * 
 *                                      2.  Modified a number of XML tags.
 *                                      
 *  07/20/11    1.9.6   K.McD           1.  Modified the signature of the constructor to use the ICommunicationParent interface.
 *  
 *  08/10/11    1.9.7   Sean.D          1.  Added debug assertions in GetDefaultStreamInformation and GetStreamInformation to ensure that there are
 *                                          not more watch variables brought in than our maximum.
 *                                          
 *  07/24/13    1.10    K.McD           1.  Automatic update when all references to the Parameter.WatchSizeFaultLogMax constant were replaced by
 *                                          references to the Parameter.WatchSizeFaultLog property.
 */
#endregion - [1.0 to 1.10] -

#region - [1.11] -
/*                                         
 *  03/11/15    1.11    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                              to support both 32 and 64 bit architecture.
 *                                          
 *                                      Modifications
 *                                      1.  Added delegate declarations for all of the VcuCommunication32.dll and VcuCommunication64 methods that
 *                                          are associated with the event sub-system.
 *                                          
 *                                      2.  Added member delegates for each of the delegate declarations.
 *                                          
 *                                      3.  Modified the zero parameter constructor to instantiate the delegates with either the 
 *                                          32 or 64 bit version of the corresponding method depending upon the current state of the 
 *                                          'Environment.Is64BitOperatingSystem' variable.
 *                                          
 *                                      5.  Modified each of the methods to check that the function delegate has been initialized prior to its use.
 *                                          
 *                                      6.  Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted, 
 *                                          the method was modified to a check that the Mutex has been initialized prior to its use.
 *                                          
 *                                      7.  Replaced all calls to the methods within PTUDLL32.dll with calls via function delegates. This allows 
 *                                          support for both 32 and 64 bit systems.
 *                                          
 *                                      8.  Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted, 
 *                                          a 'finally' block was added to each 'try' block to ensure that the Mutex is released even if an exception 
 *                                          occurs. The code pattern was modified to use the following template:
 *                                          
 *                                          CommunicationError errorCode = CommunicationError.UnknownError;
 *                                          try
 *                                          {
 *                                              m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
 *                                              errorCode = (CommunicationError)m_<function-name>( ... );
 *                                          }
 *                                          catch (Exception)
 *                                          {
 *                                              errorCode = CommunicationError.SystemException;
 *                                              throw new CommunicationException(" ... ", errorCode);
 *                                          }
 *                                          finally
 *                                          {
 *                                              m_MutexCommuncationInterface.ReleaseMutex();
 *                                          }
 *                                          
 *                                          if (DebugMode.Enabled == true)
 *                                          {
 *                                              ...
 *                                          }
 *
 *                                          if (errorCode != CommunicationError.Success)
 *                                          {
 *                                              throw new CommunicationException("<function-name>", errorCode);
 *                                          }
 *                                          
 *                                      9.  Initialized the local errorCode variable to CommunicationError.UnknownError value for each of the
 *                                          communication methods within the class.
 *                                          
 */
#endregion - [1.11] -
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Common;
using Common.Communication;
using Common.Configuration;
using Event.Properties;

namespace Event.Communication
{
    /// <summary>
    /// Class to manage the communication with the target hardware with respect to the event sub-system.
    /// </summary>
    public class CommunicationEvent : CommunicationParent, ICommunicationEvent
    {
        #region --- Delegate Declarations ---
        /// <summary>
        /// Delegate declaration for the LoadFaultlog() method of VcuCommunication32.dll/VcuCommunication64.dll. This method Loads 
        /// the current event log into memory.
        /// </summary>
        /// <param name="eventCount">The number of events that have been loaded into memory.</param>
        /// <param name="oldIndex">The old event index.</param>
        /// <param name="newIndex">The new event index.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short LoadFaultlogDelegate(out short eventCount, out uint oldIndex, out uint newIndex);

        /// <summary>
        /// Delegate declaration for the CheckFaultlogger() method of VcuCommunication32.dll/VcuCommunication64.dll. This method checks 
        /// whether any new events have been added to the active event log.
        /// </summary>
        /// <param name="eventCount">The number of new events that have been added to the active event log.</param>
        /// <param name="newIndex">The index of the first new event.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short CheckFaultloggerDelegate(out short eventCount, out uint newIndex);

        /// <summary>
        /// Delegate declaration for the GetFaultHdr() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets 
        /// the event record corresponding to the specified index for the event log that is currently loaded into memory.
        /// </summary>
        /// <param name="eventIndex">The event index of the required event.</param>
        /// <param name="eventIdentifier">The event identifier associated with the event.</param>
        /// <param name="taskIdentifier">The task identifier associated with the event.</param>
        /// <param name="time">The time that the event was raised.</param>
        /// <param name="date">The date that the event was raised.</param>
        /// <param name="streamNumber">The stream number of the data stream associated with the event, if one is available; otherwise, -1.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short GetFaultHdrDelegate(short eventIndex, out short eventIdentifier, out short taskIdentifier,
                                                     [MarshalAs(UnmanagedType.BStr)] out String time,
                                                     [MarshalAs(UnmanagedType.BStr)] out String date, out short streamNumber);

        /// <summary>
        /// Delegate declaration for the GetFaultVar() method of VcuCommunication32.dll/VcuCommunication64.dll. This method retrieves 
        /// the event variables associated with the specified event.
        /// </summary>
        /// <param name="eventIndex">The index of the event.</param>
        /// <param name="eventVariableCount">The number of event variables that are to be retrieved.</param>
        /// <param name="dataTypes">The data types associated with each of the event variables.</param>
        /// <param name="values">The values associated with each event variable.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short GetFaultVarDelegate(short eventIndex, short eventVariableCount, short[] dataTypes, double[] values);

        /// <summary>
        /// Delegate declaration for the GetFltFlagInfo() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets 
        /// the status of the flags that control: (a) whether the event type is enabled and (b) whether the event type triggers the recoding of a
        /// data stream. 
        /// </summary>
        /// <param name="validFlags">An array of flags that define which of the available event types are valid for the current log. The total length
        /// of the array is the maximum number of events per task multiplied by the maximum number of tasks and the array element corresponding to a
        /// particular event type is defined as: {task identifier} * {maximum number of events per task} + {event identifier}. True, indicates that
        /// the event type is valid; otherwise, false.</param>
        /// <param name="enabledFlags">An array of flags that indicate whether the event type is enabled. True, indicates that the event type is
        /// enabled; otherwise, false.</param>
        /// <param name="streamTriggeredFlags">An array of flags that indicate whether the event type triggers the recording of a data stream.
        /// True, indicates that the event type triggers the recording of a data stream; otherwise false.</param>
        /// <param name="eventCount">The maximum number of event types i.e. the maximum number of event types per task multiplied by the maximum
        /// number of tasks.</param>
        /// <remarks>The size of the <paramref name="enabledFlags"/> and <paramref name="streamTriggeredFlags"/> arrays is equal to the number of
        /// defined event types associated with the current log. The array index is mapped to a table that is derived by sorting the records of the
        /// EVENTS table of the data dictionary corresponding to the LOGID field associated with the current log by the TASKID, EVENTID fields,
        /// in ascending order. </remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short GetFltFlagInfoDelegate(short[] validFlags, short[] enabledFlags, short[] streamTriggeredFlags, short eventCount);

        /// <summary>
        /// Delegate declaration for the GetFltHistInfo() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets 
        /// the event history associated with the current log.
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
        /// of defined event types associated with the current log. The array index is mapped to a table that is derived by sorting the records of 
        /// the EVENTS table of the data dictionary corresponding to the LOGID field associated with the current log by the TASKID, EVENTID fields,
        /// in ascending order.</remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short GetFltHistInfoDelegate(short[] valid, short[] cumulativeHistoryCount, short[] recentHistoryCount,
                                                        short maxTasks, short maxEventsPerTask);

        /// <summary>
        /// Delegate declaration for the FreeEventLogMemory() method of VcuCommunication32.dll/VcuCommunication64.dll. This method clears the event
        /// log memory.
        /// </summary>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short FreeEventLogMemoryDelegate();

        /// <summary>
        /// Delegate declaration for the SetFaultFlags() method of VcuCommunication32.dll/VcuCommunication64.dll. This method sets 
        /// the flag that controls: (a) whether the specified event type is enabled and (b) whether the event type triggers the recoding 
        /// of a data stream. 
        /// </summary>
        /// <param name="taskIdentfier">The task identifier associated with the event type.</param>
        /// <param name="eventIdentifier">The event identifier associated with the event type.</param>
        /// <param name="enabledFlag">A flag to control whether the event type corresponding to the specified task and event identifiers is to be
        /// enabled. True, if the event type is to be enabled; otherwise, false.</param>
        /// <param name="streamTriggeredFlag">A flag to control whether the event type corresponding to the specified task and event identifiers is
        /// to trigger the recording of a data stream. True, if the event type is to trigger the recording of a data stream; otherwise, false.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short SetFaultFlagsDelegate(short taskIdentfier, short eventIdentifier, short enabledFlag, short streamTriggeredFlag);

        /// <summary>
        /// Delegate declaration for the ClearEvent() method of VcuCommunication32.dll/VcuCommunication64.dll. This method clears the active event
        /// log.
        /// </summary>
        /// <remarks>
        /// This call removes all events contained in the active event log and erases any data logs associated with the event log.
        /// </remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short ClearEventDelegate();

        /// <summary>
        /// Delegate declaration for the InitializeEventLog() method of VcuCommunication32.dll/VcuCommunication64.dll. This method initializes the
        /// event sub-system.
        /// </summary>
        /// <remarks>
        /// Clears all event information stored on battery backed RAM for both the maintenance and engineering logs. This call also clears the
        /// cumulative history, recent history columns and all data logs. This function is typically used to establish a zero event/fault reference
        /// base when a replacement VCU is installed in a car.
        /// </remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short InitializeEventLogDelegate();

        /// <summary>
        /// Delegate declaration for the GetStream() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets the data stream.
        /// </summary>
        /// <param name="streamNumber">The stream number.</param>
        /// <param name="dataBuffer">The point values for each watch variable.</param>
        /// <param name="timeOrigin">The start time of the plot.</param>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="dataTypes">The data type corresponding to each watch variable contained within the data stream.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short GetStreamDelegate(short streamNumber, int[] dataBuffer, out short timeOrigin, short watchVariableCount,
                                                   short sampleCount, short[] dataTypes);

        /// <summary>
        /// Delegate declaration for the GetStreamInformation() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets the 
        /// parameters associated with the specified stream.
        /// </summary>
        /// <param name="streamNumber">The stream number.</param>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables contained within the data stream.</param>
        /// <param name="dataTypes">The data type corresponding to each watch variable contained within the data stream.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short GetStreamInformationDelegate(short streamNumber, out short watchVariableCount, out short sampleCount,
                                                              out short sampleMultiple, short[] watchIdentifiers, short[] dataTypes);

        /// <summary>
        /// Delegate declaration for the GetDefaultStreamInformation() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets the
        /// parameters associated with the default stream.
        /// </summary>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables contained within the data stream.</param>
        /// <param name="dataTypes">The data types corresponding to each of the watch variables contained within the data stream.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short GetDefaultStreamInformationDelegate(out short watchVariableCount, out short sampleCount, 
                                                                     out short sampleMultiple, short[] watchIdentifiers, short[] dataTypes);

        /// <summary>
        /// Delegate declaration for the SetDefaultStreamInformation() method of VcuCommunication32.dll/VcuCommunication64.dll. This method sets the
        /// default stream parameters.
        /// </summary>
        /// <param name="watchVariableCount">The number of watch variables that are to be included in the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is to be sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables that are to be included in the data stream.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short SetDefaultStreamInformationDelegate(short watchVariableCount, short sampleMultiple, short[] watchIdentifiers);

        /// <summary>
        /// Delegate declaration for the GetEventLog() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets the 
        /// index of the event log that is currently loaded into memory.
        /// </summary>
        /// <param name="eventLogIndex">The index of the event log that is currently loaded into memory.</param>
        /// <param name="eventLogCount">The number of event logs supported by the VCU.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short GetEventLogDelegate(out short eventLogIndex, out short eventLogCount);

        /// <summary>
        /// Delegate declaration for the ChangeEventLog() method of VcuCommunication32.dll/VcuCommunication64.dll. This method changes 
        /// the current event log on the VCU to the specified log.
        /// </summary>
        /// <param name="eventLogIndex">The index of the required event log.</param>
        /// <param name="sampleIntervalMs">The base interval, in ms, between samples.</param>
        /// <param name="changeStatus">A flag to indicate whether new events have been added to the log.</param>
        /// <param name="maxTasks">The maximum number of tasks associated with the log.</param>
        /// <param name="maxEventsPerTask">The maximum number of events per task.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short ChangeEventLogDelegate(short eventLogIndex, out short sampleIntervalMs, out short changeStatus,
                                                        out short maxTasks, out short maxEventsPerTask);

        /// <summary>
        /// Delegate declaration for the ExitEventLog() method of VcuCommunication32.dll/VcuCommunication64.dll. This method exits the event 
        /// sub-system.
        /// </summary>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short ExitEventLogDelegate();
        #endregion --- Delegate Declarations ---

        #region --- Constants ---
        /// <summary>
        /// The <c>CultureInfo</c> string used to represent - english (US). Value: "en-US";
        /// </summary>
        private const string CultureEnglishUS = "en-US";

        /// <summary>
        /// The maximum number of columns in a workset generated from the parameters of a datastream retrieved from the VCU.
        /// </summary>
        private const short VCUDataStreamColumnCountMax = 1;
        #endregion --- Constants ---

        #region --- Member Variables ---
        #region --- Function Delegates  ---
        /// <summary>
        /// Delegate for the LoadFaultlog() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected LoadFaultlogDelegate m_LoadFaultlog;

        /// <summary>
        /// Delegate for the LoadFaultlog() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected CheckFaultloggerDelegate m_CheckFaultlogger;

        /// <summary>
        /// Delegate for the CheckFaultlogger() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetFaultHdrDelegate m_GetFaultHdr;

        /// <summary>
        /// Delegate for the GetFaultVar() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetFaultVarDelegate m_GetFaultVar;

        /// <summary>
        /// Delegate for the GetFltFlagInfo() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetFltFlagInfoDelegate m_GetFltFlagInfo;

        /// <summary>
        /// Delegate for the GetFltHistInfo() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetFltHistInfoDelegate m_GetFltHistInfo;

        /// <summary>
        /// Delegate for the FreeEventLogMemory() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected FreeEventLogMemoryDelegate m_FreeEventLogMemory;

        /// <summary>
        /// Delegate for the SetFaultFlags() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SetFaultFlagsDelegate m_SetFaultFlags;

        /// <summary>
        /// Delegate for the ClearEvent() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected ClearEventDelegate m_ClearEvent;

        /// <summary>
        /// Delegate for the InitializeEventLog() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected InitializeEventLogDelegate m_InitializeEventLog;

        /// <summary>
        /// Delegate for the GetStream() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetStreamDelegate m_GetStream;

        /// <summary>
        /// Delegate for the GetStreamInformation() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetStreamInformationDelegate m_GetStreamInformation;

        /// <summary>
        /// Delegate for the  GetDefaultStreamInformation() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetDefaultStreamInformationDelegate m_GetDefaultStreamInformation;
        
        /// <summary>
        /// Delegate for the SetDefaultStreamInformation() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SetDefaultStreamInformationDelegate m_SetDefaultStreamInformation;
        
        /// <summary>
        /// Delegate for the GetEventLog() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetEventLogDelegate m_GetEventLog;

        /// <summary>
        /// Delegate for the ChangeEventLog() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected ChangeEventLogDelegate m_ChangeEventLog;

        /// <summary>
        /// Delegate for the ExitEventLog() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected ExitEventLogDelegate m_ExitEventLog;
        #endregion --- Function Delegates  ---
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class and set the function delegates, properties and member variables to those values associated with the
        /// specified communication interface.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface containing the properties and member variables that are to
        /// be used to initialize the class.</param>
        public CommunicationEvent(ICommunicationParent communicationInterface)
            : base(communicationInterface)
        {
            #region - [Initialize VcuCommunication.event.cpp Function Delegates] -
            // ----------------------------------------------------------------------
            // Initialize the function delegates to either the VcuCommunication32.dll  
            // or VcuCommunication64.dll functions depending upon whether the 
            // Windows operating system is 64 bit or 32 bit.
            // ----------------------------------------------------------------------
            if (m_Is64BitOperatingSystem == true)
            {
                m_LoadFaultlog = VcuCommunication64Event.LoadFaultlog;
                m_CheckFaultlogger = VcuCommunication64Event.CheckFaultlogger;
                m_GetFaultHdr = VcuCommunication64Event.GetFaultHdr;
                m_GetFaultVar = VcuCommunication64Event.GetFaultVar;
                m_GetFltFlagInfo = VcuCommunication64Event.GetFltFlagInfo;
                m_GetFltHistInfo = VcuCommunication64Event.GetFltHistInfo;
                m_FreeEventLogMemory = VcuCommunication64Event.FreeEventLogMemory;
                m_SetFaultFlags = VcuCommunication64Event.SetFaultFlags;
                m_ClearEvent = VcuCommunication64Event.ClearEvent;
                m_InitializeEventLog = VcuCommunication64Event.InitializeEventLog;
                m_GetStream = VcuCommunication64Event.GetStream;
                m_GetStreamInformation = VcuCommunication64Event.GetStreamInformation;
                m_GetDefaultStreamInformation = VcuCommunication64Event.GetDefaultStreamInformation;
                m_SetDefaultStreamInformation = VcuCommunication64Event.SetDefaultStreamInformation;
                m_GetEventLog = VcuCommunication64Event.GetEventLog;
                m_ChangeEventLog = VcuCommunication64Event.ChangeEventLog;
                m_ExitEventLog = VcuCommunication64Event.ExitEventLog;
            }
            else
            {
                m_LoadFaultlog = VcuCommunication32Event.LoadFaultlog;
                m_CheckFaultlogger = VcuCommunication32Event.CheckFaultlogger;
                m_GetFaultHdr = VcuCommunication32Event.GetFaultHdr;
                m_GetFaultVar = VcuCommunication32Event.GetFaultVar;
                m_GetFltFlagInfo = VcuCommunication32Event.GetFltFlagInfo;
                m_GetFltHistInfo = VcuCommunication32Event.GetFltHistInfo;
                m_FreeEventLogMemory = VcuCommunication32Event.FreeEventLogMemory;
                m_SetFaultFlags = VcuCommunication32Event.SetFaultFlags;
                m_ClearEvent = VcuCommunication32Event.ClearEvent;
                m_InitializeEventLog = VcuCommunication32Event.InitializeEventLog;
                m_GetStream = VcuCommunication32Event.GetStream;
                m_GetStreamInformation = VcuCommunication32Event.GetStreamInformation;
                m_GetDefaultStreamInformation = VcuCommunication32Event.GetDefaultStreamInformation;
                m_SetDefaultStreamInformation = VcuCommunication32Event.SetDefaultStreamInformation;
                m_GetEventLog = VcuCommunication32Event.GetEventLog;
                m_ChangeEventLog = VcuCommunication32Event.ChangeEventLog;
                m_ExitEventLog = VcuCommunication32Event.ExitEventLog;
            }
            #endregion - [Initialize VcuCommunication.event.cpp Function Delegates] -

        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Change the current event log on the VCU to the specified log and initialize the: DataRecordingRate, ChangeStatus, MaxTasks and
        /// MaxEventsPerTask properties of the log structure with the values returned from the VCU.
        /// </summary>
        /// <param name="log">The required event log.</param>
        /// <remarks>The identifier field of the specified log must be initialized prior to calling this method. Note: The call to the ChangeEventLog
        /// () method in PTUDLL32.dll refers to the eventLogIndex, whereas the log structure uses the log identifier. The event log index is zero
        /// based and is equivalent to the event log identifier - 1.</remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ChangeEventLog() method is not
        /// CommunicationError.Success.</exception>
        public void ChangeEventLog(Log log)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_ChangeEventLog != null, "CommunicationEvent.ChangeEventLog() - [m_ChangeEventLog != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.ChangeEventLog() - [m_MutexCommuncationInterface != null]");

            short logIndex, sampleIntervalMs, changeStatus, maxTasks, maxEventsPerTask;
            logIndex = (short)(log.Identifier - 1);
            Debug.Assert(logIndex >= 0, "CommunicationEvent.ChangeEventLog() - [logIndex >= 0]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_ChangeEventLog(logIndex, out sampleIntervalMs, out changeStatus, out maxTasks,
                                                                 out maxEventsPerTask);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.ChangeEventLog()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }
            
            if (DebugMode.Enabled == true)
            {
                DebugMode.ChangeEventLog_t changeEventLog = new DebugMode.ChangeEventLog_t(logIndex, sampleIntervalMs, changeStatus, maxTasks,
                                                                                           maxEventsPerTask, errorCode);
                DebugMode.Write(changeEventLog.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.ChangeEventLog()", errorCode);
            }

            log.SampleIntervalMs = sampleIntervalMs;
            log.ChangeStatus = changeStatus;
            log.MaxTasks = maxTasks;
            log.MaxEventsPerTask = maxEventsPerTask;
        }

        /// <summary>
        /// Load the current event log into memory.
        /// </summary>
        /// <param name="eventCount">The number of events that have been loaded into memory.</param>
        /// <param name="oldIndex">The old event index.</param>
        /// <param name="newIndex">The new event index.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.LoadFaultlog() method is not
        /// CommunicationError.Success.</exception>
        public void LoadEventLog(out short eventCount, out uint oldIndex, out uint newIndex)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_LoadFaultlog != null, "CommunicationEvent.LoadEventLog() - [m_LoadFaultlog != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.LoadEventLog() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_LoadFaultlog(out eventCount, out oldIndex, out newIndex);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.LoadEventLog()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.LoadFaultlog_t loadFaultlog = new DebugMode.LoadFaultlog_t(eventCount, oldIndex, newIndex, errorCode);
                DebugMode.Write(loadFaultlog.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.LoadFaultLog()", errorCode);
            }
        }

        /// <summary>
        /// Get the event record, including the event variable values, corresponding to the specified event index for the event log that is currently
        /// loaded into memory.
        /// </summary>
        /// <param name="currentEventLog">The event log that has been loaded into memory using the LoadEventLog() method.</param>
        /// <param name="eventIndex">The event index of the required event record.</param>
        /// <param name="eventRecord">The event record corresponding to the specified event index.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFaultHdr() method is not
        /// CommunicationError.Success.</exception>
        public void GetEventRecord(Log currentEventLog, short eventIndex, out EventRecord eventRecord)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetFaultHdr != null, "CommunicationEvent.GetEventRecord() - [m_GetFaultHdr != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.GetEventRecord() - [m_MutexCommuncationInterface != null]");

            short eventIdentifier, taskIdentifier, streamNumber;
            String time, date;

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetFaultHdr(eventIndex, out eventIdentifier, out taskIdentifier, out time, out date,
                                                              out streamNumber);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.GetEventRecord()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetFaultHdr_t getFaultHdr = new DebugMode.GetFaultHdr_t(eventIndex, eventIdentifier, taskIdentifier, time, date,
                                                                                  streamNumber, errorCode);
                DebugMode.Write(getFaultHdr.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.GetEventRecord()", errorCode);
            }

            // Get the identifier field from the events table that matches the specified event, task and log identifiers.
            short identifier;
            short logIndex = (short)(currentEventLog.Identifier - 1);
            identifier = Lookup.EventTable.GetIdentifier(logIndex, taskIdentifier, eventIdentifier);

            // Check that the event corresponding to specified LOGID, TASKID and EVENTID was found in the EVENTS table of the data dictionary.
            if (identifier < 0)
            {
                MessageBox.Show(string.Format(Resources.MBTEventNotFound, logIndex, taskIdentifier, eventIdentifier), Resources.MBCaptionError,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                eventRecord = null;
                return;
            }

            // Instantiate new event record based upon this identifier.
            eventRecord = new EventRecord(identifier);

            // Add the record VCU fields.
            eventRecord.LogIdentifier = currentEventLog.Identifier;
            eventRecord.EventIndex = eventIndex;
            eventRecord.Time = time.ToString();
            eventRecord.Date = date.ToString();

            // Convert the date and time retrieved from the VCU to a .NET DateTime object.
            try
            {
                eventRecord.DateTime = DateTime.Parse(date + CommonConstants.Space + time, new CultureInfo(CultureEnglishUS, false));
            }
            catch (FormatException)
            {
                eventRecord.DateTime = Parameter.InvalidDateTime;
            }

            eventRecord.StreamNumber = streamNumber;
            if (streamNumber == CommonConstants.False)
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
        }

        /// <summary>
        /// Clear the event log memory.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.FreeEventLogMemory() method is
        /// not CommunicationError.Success.</exception>
        public void FreeEventLogMemory()
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_FreeEventLogMemory != null, "CommunicationEvent.FreeEventLogMemory() - [m_FreeEventLogMemory != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.FreeEventLogMemory() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_FreeEventLogMemory();
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.FreeEventLogMemory()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.FreeEventLogMemory_t freeEventLogMemory = new DebugMode.FreeEventLogMemory_t(errorCode);
                DebugMode.Write(freeEventLogMemory.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.FreeEventLogMemory()", errorCode);
            }
        }

        /// <summary>
        /// Get the index of the event log that is currently loaded into memory.
        /// </summary>
        /// <param name="eventLogIndex">The index of the event log that is currently loaded into memory.</param>
        /// <param name="eventLogCount">The number of event logs supported by the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetEventLog() method is not
        /// CommunicationError.Success.</exception>
        public void GetEventLogIndex(out short eventLogIndex, out short eventLogCount)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetEventLog != null, "CommunicationEvent.GetEventLogIndex() - [m_GetEventLog != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.GetEventLogIndex() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetEventLog(out eventLogIndex, out eventLogCount);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.GetEventLogIndex()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetEventLog_t getEventLog = new DebugMode.GetEventLog_t(eventLogIndex, eventLogCount, errorCode);
                DebugMode.Write(getEventLog.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.GetEventLogIndex()", errorCode);
            }
        }

        /// <summary>
        /// Retrieve the event variables associated with the specified event.
        /// </summary>
        /// <remarks>The number of events consist of the event variables that are collected for every event plus the event specific variables. The
        /// event variables that are collected for each event consist of those event variables associated with the structure identifier value of 0
        /// less those defined as event header variables.</remarks>
        /// <param name="eventIndex">The index of the event.</param>
        /// <param name="eventVariableCount">The number of event variable that are to be retrieved.</param>
        /// <param name="dataTypes">The data types associated with each of the variables.</param>
        /// <param name="values">The event variable values associated with each event.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFaultVar() method is not
        /// CommunicationError.Success.</exception>
        public void GetFaultVar(short eventIndex, short eventVariableCount, short[] dataTypes, out double[] values)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetFaultVar != null, "CommunicationEvent.GetFaultVar() - [m_GetFaultVar != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.GetFaultVar() - [m_MutexCommuncationInterface != null]");

            // Instantiate an array of doubles to hold the return values.
            values = new double[dataTypes.Length];

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetFaultVar(eventIndex, eventVariableCount, dataTypes, values);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.GetFaultVar()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetFaultVar_t getFaultVar = new DebugMode.GetFaultVar_t(eventIndex, eventVariableCount, dataTypes, values, errorCode);
                DebugMode.Write(getFaultVar.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.GetFaultVar()", errorCode);
            }
        }

        /// <summary>
        /// Initialize the event log. Clears all event information stored on battery backed RAM for both the maintenance and engineering logs. This
        /// also clears both the cumulative history, recent history columns and all data logs. This function is typically used to establish a zero
        /// event/fault reference base when a replacement VCU is installed in a car.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.InitializeEventLog() method is
        /// not CommunicationError.Success.</exception>
        public void InitializeEventLog()
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_InitializeEventLog != null, "CommunicationEvent.InitializeEventLog() - [m_InitializeEventLog != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.InitializeEventLog() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_InitializeEventLog();
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.InitializeEventLog()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.InitializeEventLog_t initializeEventLog = new DebugMode.InitializeEventLog_t(errorCode);
                DebugMode.Write(initializeEventLog.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.InitializeEventLog()", errorCode);
            }
        }

        /// <summary>
        /// Clear the current event log. Remove all events contained in the active event log.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ClearEvent() method is not
        /// CommunicationError.Success.</exception>
        public void ClearEvent()
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_ClearEvent != null, "CommunicationEvent.ClearEvent() - [m_ClearEvent != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.ClearEvent() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_ClearEvent();
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.ClearEvent()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.ClearEvent_t clearEvent = new DebugMode.ClearEvent_t(errorCode);
                DebugMode.Write(clearEvent.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.ClearEvent()", errorCode);
            }
        }

        /// <summary>
        /// Exit the event sub-system.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ExitEventLog() method is not
        /// CommunicationError.Success.</exception>
        public void ExitEventLog()
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_ExitEventLog != null, "CommunicationEvent.ExitEventLog() - [m_ExitEventLog != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.ExitEventLog() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_ExitEventLog();
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.ExitEventLog()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.ExitEventLog_t exitEventLog = new DebugMode.ExitEventLog_t(errorCode);
                DebugMode.Write(exitEventLog.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.ExitEventLog()", errorCode);
            }
        }

        /// <summary>
        /// Get the parameters associated with the default stream.
        /// </summary>
        /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base recording interval (60ms) at which the data is sampled.</param>
        /// <param name="watchIdentifiers">The watch identifiers of the watch variables contained within the data stream.</param>
        /// <param name="dataTypes">The data types corresponding to each of the watch variables contained within the data stream.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetDefaultStreamInformation()
        /// method is not CommunicationError.Success.</exception>
        public void GetDefaultStreamInformation(out short watchVariableCount, out short sampleCount, out short sampleMultiple,
                                                out short[] watchIdentifiers, out short[] dataTypes)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetDefaultStreamInformation != null,
                         "CommunicationEvent.GetDefaultStreamInformation() - [m_GetDefaultStreamInformation != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationEvent.GetDefaultStreamInformation() - [m_MutexCommuncationInterface != null]");

            short[] tempWatchIdentifiers = new short[Parameter.WatchSizeFaultLog];
            short[] tempDataTypes = new short[Parameter.WatchSizeFaultLog];

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetDefaultStreamInformation(out watchVariableCount, out sampleCount,
                                                                              out sampleMultiple, tempWatchIdentifiers, tempDataTypes);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.GetDefaultStreamInformation()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetDefaultStreamInformation_t getDefaultStreamInformation =
                    new DebugMode.GetDefaultStreamInformation_t(watchVariableCount, sampleCount, sampleMultiple, tempWatchIdentifiers, tempDataTypes,
                                                                errorCode);
                DebugMode.Write(getDefaultStreamInformation.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.GetDefaultStreamInformation()", errorCode);
            }

            Debug.Assert(watchVariableCount <= Parameter.WatchSizeFaultLog,
                         "CommunicationEvent.GetDefaultStreamInformation() - [watchVariableCount <= Parameter.WatchSizeFaultLog]");

            // Adjust the length of the array to the correct size.
            watchIdentifiers = new short[watchVariableCount];
            dataTypes = new short[watchVariableCount];

            Array.Copy(tempWatchIdentifiers, watchIdentifiers, watchVariableCount);
            Array.Copy(tempDataTypes, dataTypes, watchVariableCount);
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
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetStreamInformation() method is
        /// not CommunicationError.Success.</exception>
        public void GetStreamInformation(short streamNumber, out short watchVariableCount, out short sampleCount, out short sampleMultiple,
                                         out short[] watchIdentifiers, out short[] dataTypes)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetStreamInformation != null, "CommunicationEvent.GetStreamInformation() - [m_GetStreamInformation != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.GetStreamInformation() - [m_MutexCommuncationInterface != null]");

            short[] tempWatchIdentifiers = new short[Parameter.WatchSizeFaultLog];
            short[] tempDataTypes = new short[Parameter.WatchSizeFaultLog];

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetStreamInformation(streamNumber, out watchVariableCount, out sampleCount,
                                                                       out sampleMultiple, tempWatchIdentifiers, tempDataTypes);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.GetStreamInformation()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetStreamInformation_t getStreamInformation =
                    new DebugMode.GetStreamInformation_t(streamNumber, watchVariableCount, sampleCount, sampleMultiple, tempWatchIdentifiers,
                                                         tempDataTypes, errorCode);
                DebugMode.Write(getStreamInformation.ToXML());
            }
            
            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.GetStreamInformation()", errorCode);
            }

            Debug.Assert(watchVariableCount <= Parameter.WatchSizeFaultLog,
                         "CommunicationEvent.GetStreamInformation() - [watchVariableCount <= Parameter.WatchSizeFaultLog]");

            // Adjust the length of the array to the correct size.
            watchIdentifiers = new short[watchVariableCount];
            dataTypes = new short[watchVariableCount];

            Array.Copy(tempWatchIdentifiers, watchIdentifiers, watchVariableCount);
            Array.Copy(tempDataTypes, dataTypes, watchVariableCount);
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
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetStream != null, "CommunicationEvent.GetStream() - [m_GetStream != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.GetStream() - [m_MutexCommuncationInterface != null]");

            Debug.Assert(eventRecord.StreamSaved == true, "CommuncationEvent.GetStream() - [eventRecord.StreamSaved == true]");
            Debug.Assert(eventRecord.StreamNumber != CommonConstants.NotUsed,
                         "CommuncationEvent.GetStream() - [eventRecord.StreamNumber != CommonConstants.NotUsed]");

            short watchCount, sampleCount, sampleMultiple;
            short[] watchIdentifiers, dataTypes;

            GetStreamInformation(eventRecord.StreamNumber, out watchCount, out sampleCount, out sampleMultiple, out watchIdentifiers, out dataTypes);
            
            int[] buffer = new int[sampleCount * watchCount];
            short timeOrigin;

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);

                // TODO - CommunicationEvent.GetStream(). Check on the significance of the timeOrigin parameter in the GetStream() method.
                errorCode = (CommunicationError)m_GetStream(eventRecord.StreamNumber, buffer, out timeOrigin, watchCount, sampleCount, dataTypes);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.GetStream()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetStream_t getStream = new DebugMode.GetStream_t(eventRecord.StreamNumber, buffer, timeOrigin, watchCount,
                                                                            sampleCount, dataTypes, errorCode);
                DebugMode.Write(getStream.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.GetStream()", errorCode);
            }

            Workset_t workset = ConvertToWorkset(eventRecord.Description, watchIdentifiers, sampleMultiple);
            DataStream_t dataStream = new DataStream_t(eventRecord, watchCount, sampleCount, sampleMultiple, workset);

            DateTime startTime = eventRecord.DateTime.Subtract(new TimeSpan(0, 0, 0, 0, dataStream.DurationPreTripMs));

            // Convert the retrieved data stream into a list of individual, time-stamped watch variable data frames.
            List<WatchFrame_t> watchFrameList = ConvertToWatchFrameList(startTime, sampleCount, dataStream.FrameIntervalMs, buffer, dataTypes,
                                                                        workset);

            dataStream.WatchFrameList = watchFrameList;

            // Configure the upper and lower display limits.
            dataStream.ConfigureAutoScale();

            return dataStream;
        }

        /// <summary>
        /// Set the default stream parameters.
        /// </summary>
        /// <param name="sampleMultiple">The sample multiple of the recording interval at which the data is to be recorded.</param>
        /// <param name="oldIdentifierList">The list of old identifiers associated with the watch variables that are to be recorded in the fault log,
        /// in the order in which they are to appear.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetDefaultStreamInformation()
        /// method is not CommunicationError.Success.</exception>
        public void SetDefaultStreamInformation(short sampleMultiple, List<short> oldIdentifierList)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_SetDefaultStreamInformation != null,
                         "CommunicationEvent.SetDefaultStreamInformation() - [m_SetDefaultStreamInformation != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationEvent.SetDefaultStreamInformation() - [m_MutexCommuncationInterface != null]");

            short[] watchIdentifiers = new short[oldIdentifierList.Count];

            WatchVariable watchVariable;
            for (int elementIndex = 0; elementIndex < oldIdentifierList.Count; elementIndex++)
            {
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifierList[elementIndex]];
                    if (watchVariable == null)
                    {
                        watchIdentifiers[elementIndex] = CommonConstants.WatchIdentifierNotDefined;
                    }
                    else
                    {
                        watchIdentifiers[elementIndex] = watchVariable.Identifier;
                    }
                }
                catch (Exception)
                {
                    watchIdentifiers[elementIndex] = CommonConstants.WatchIdentifierNotDefined;
                }
            }

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_SetDefaultStreamInformation((short)oldIdentifierList.Count, sampleMultiple, watchIdentifiers);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.SetDefaultStreamInformation()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.SetDefaultStreamInformation_t setDefaultStreamInformation =
                    new DebugMode.SetDefaultStreamInformation_t((short)oldIdentifierList.Count, sampleMultiple, watchIdentifiers, errorCode);
                DebugMode.Write(setDefaultStreamInformation.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.SetDefaultStreamInformation()", errorCode);
            }
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
            // Check that the function delegate has been initialized.
            Debug.Assert(m_CheckFaultlogger != null, "CommunicationEvent.CheckFaultlogger() - [m_CheckFaultlogger != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.CheckFaultlogger() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_CheckFaultlogger(out eventCount, out newIndex);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.CheckFaultlogger()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.CheckFaultlogger_t checkFaultlogger = new DebugMode.CheckFaultlogger_t(eventCount, newIndex, errorCode);
                DebugMode.Write(checkFaultlogger.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.CheckFaultlogger()", errorCode);
            }
        }

        /// <summary>
        /// Get the status of the flags that control: (a) whether the event type is enabled and (b) whether the event type triggers the recoding of a
        /// data stream. 
        /// </summary>
        /// <param name="validFlags">An array of flags that define which of the available event types are valid for the current log. The total length
        /// of the array is the maximum number of events per task multiplied by the maximum number of tasks and the array element corresponding to a
        /// particular event type is defined as: {task identifier} * {maximum number of events per task} + {event identifier}. True, indicates that
        /// the event type is valid; otherwise, false.</param>
        /// <param name="enabledFlags">An array of flags that indicate whether the event type is enabled. True, indicates that the event type is
        /// enabled; otherwise, false.</param>
        /// <param name="streamTriggeredFlags">An array of flags that indicate whether the event type triggers the recording of a data stream.
        /// True, indicates that the event type triggers the recording of a data stream; otherwise false.</param>
        /// <param name="eventCount">The maximum number of event types i.e. the maximum number of event types per task multiplied by the maximum
        /// number of tasks.</param>
        /// <remarks>The size of the <paramref name="enabledFlags"/> and <paramref name="streamTriggeredFlags"/> arrays is equal to the number of
        /// defined event types associated with the current log. The array index is mapped to a table that is derived by sorting the records of the
        /// EVENTS table of the data dictionary corresponding to the LOGID field associated with the current log by the TASKID, EVENTID fields, in
        /// ascending order. </remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFltFlagInfo() method is not
        /// CommunicationError.Success.</exception>
        public void GetFltFlagInfo(short[] validFlags, ref short[] enabledFlags, ref short[] streamTriggeredFlags, short eventCount)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetFltFlagInfo != null, "CommunicationEvent.GetFltFlagInfo() - [m_GetFltFlagInfo != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.GetFltFlagInfo() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetFltFlagInfo(validFlags, enabledFlags, streamTriggeredFlags, eventCount);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.GetFltFlagInfo()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.GetFltFlagInfo()", errorCode);
            }
        }

        /// <summary>
        /// Set the flag that controls: (a) whether the specified event type is enabled and (b) whether the event type triggers the recoding of a data
        /// stream. 
        /// </summary>
        /// <param name="taskIdentfier">The task identifier associated with the event type.</param>
        /// <param name="eventIdentifier">The event identifier associated with the event type.</param>
        /// <param name="enabledFlag">A flag to control whether the event type corresponding to the specified task and event identifiers is to be
        /// enabled. True, if the event type is to be enabled; otherwise, false.</param>
        /// <param name="streamTriggeredFlag">A flag to control whether the event type corresponding to the specified task and event identifiers is to
        /// trigger the recording of a data stream. True, if the event type is to trigger the recording of a data stream; otherwise, false.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetFaultFlags() method is not 
        /// CommunicationError.Success.</exception>
        public void SetFaultFlags(short taskIdentfier, short eventIdentifier, short enabledFlag, short streamTriggeredFlag)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_SetFaultFlags != null, "CommunicationEvent.SetFaultFlags() - [m_SetFaultFlags != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.SetFaultFlags() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_SetFaultFlags(taskIdentfier, eventIdentifier, enabledFlag, streamTriggeredFlag);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.SetFaultFlags()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.SetFaultFlags()", errorCode);
            }
        }

        /// <summary>
        /// Get the event history associated with the current log.
        /// </summary>
        /// <param name="validFlags">An array of flags that define which of the available event types are valid for the current log. The total length
        /// of the array is the maximum number of events per task multiplied by the maximum number of tasks and the array element corresponding to a
        /// particular event type is defined as: {task identifier} * {maximum number of events per task} + {event identifier}. True, indicates that
        /// the event type is valid; otherwise, false.</param>
        /// <param name="cumulativeHistoryCounts">An array that contains the cumulative number of events of each event type, not including recent
        /// history.</param>
        /// <param name="recentHistoryCounts">An array that contains the recent number of events of each event type.</param>
        /// <param name="maxTasks">The maximum number of tasks that are supported by the current event log.</param>
        /// <param name="maxEventsPerTask">The maximum number of events per task that are supported by the current event log.</param>
        /// <remarks>The size of the <paramref name="cumulativeHistoryCounts"/> and <paramref name="recentHistoryCounts"/> arrays is equal to the
        /// number of defined event types associated with the current log. The array index is mapped to a table that is derived by sorting the records
        /// of the EVENTS table of the data dictionary corresponding to the LOGID field associated with the current log by the TASKID, EVENTID fields,
        /// in ascending order. </remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetFltHistInfo() method is not
        /// CommunicationError.Success.</exception>
        public void GetFltHistInfo(short[] validFlags, ref short[] cumulativeHistoryCounts, ref short[] recentHistoryCounts, short maxTasks,
                                   short maxEventsPerTask)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetFltHistInfo != null, "CommunicationEvent.GetFltHistInfo() - [m_GetFltHistInfo != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationEvent.GetFltHistInfo() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetFltHistInfo(validFlags, cumulativeHistoryCounts, recentHistoryCounts, maxTasks,
                                                                 maxEventsPerTask);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationEvent.GetFltHistInfo()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationEvent.GetFltHistInfo()", errorCode);
            }
        }

        #region - [Support Methods] -
        /// <summary>
        /// Convert the data stream values retrieved from the VCU to a format that can be plotted by the <c>FormViewDataStream</c> class.
        /// </summary>
        /// <remarks>
        /// The value array retrieved by from the VCU is initially mapped to the WatchIdentifierList property of Column[0] of the workset, however,
        /// the WatchElement array associated with each frame must be mapped to the WatchElementList property of the workset.
        /// </remarks>
        /// <param name="startTime">The start time of the fault log.</param>
        /// <param name="sampleCount">The number of samples in the data stream.</param>
        /// <param name="frameIntervalMs">The interval, in ms, between consecutive data frames.</param>
        /// <param name="values">The point values corresponding to each variable.</param>
        /// <param name="dataTypes">The data types associated with each value.</param>
        /// <param name="workset">The workset that is to be used to define the output format.</param>
        /// <returns>The watch variable values in the format that can be plotted by the <c>FormViewDataStream</c> class.</returns>
        /// <exception cref="ArgumentException">Thrown if one or more of the the watch identifiers defined in the WatchElementList property of the
        /// workset does nor exist.</exception>
        private List<WatchFrame_t> ConvertToWatchFrameList(DateTime startTime, short sampleCount, short frameIntervalMs, int[] values,
                                                           short[] dataTypes, Workset_t workset)
        {
            Debug.Assert(sampleCount > 1, "CommunicationEvent.ConvertToWatchFrameList() - [pointCount > 1]");
            Debug.Assert(frameIntervalMs > 0 , "CommunicationEvent.ConvertToWatchFrameList() - [frameIntervalMs > 0");
            Debug.Assert(values != null, "CommunicationEvent.ConvertToWatchFrameList() - [values != null]");
            Debug.Assert(dataTypes != null, "CommunicationEvent.ConvertToWatchFrameList() - [dataTypes != null]");

            short watchCount = (short)workset.WatchElementList.Count;

            // Create a look-up array to translate the watch variable data retrieved from the VCU to the order defined by the WatchElementList
            // property of the workset.
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
                Debug.Assert(((columnIndex != CommonConstants.NotFound) && (rowIndex != CommonConstants.NotFound)),
                             "CommunicationEvent.ConvertToWatchFrameList() - [((columnIndex != 0) || (rowIndex != CommonConstants.NotFound))]");

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
        /// All of the watch identifiers returned from the VCU s are added to Column[0] of the workset in the order in which they appear in
        /// <paramref name="variableIdentifiers"/> and the security level of the workset is set to the lowest security level.
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

            Workset_t workset = new Workset_t(name, watchIdentifierList, Parameter.WatchSizeFaultLog, VCUDataStreamColumnCountMax,
                                              new Security().SecurityLevelBase);

            workset.SampleMultiple = sampleMultiple;
            return workset;
        }
        #endregion - [Support Methods] -
        #endregion --- Methods ---
    }
}