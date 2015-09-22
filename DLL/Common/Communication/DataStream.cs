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
 *  File name:  DataStream.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/11/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/26/10    1.1     K.McD           1.  Moved the RecordingMultiple variable to the workset structure, renamed to SampleMultiple.
 * 
 *  01/26/11    1.2     K.McD           1.  Added the DataStreamType member varable and rationalized the member variables accordingly.
 *                                      2.  Renamed a number of member variables and modified a number of XML tags.
 * 
 *  03/28/11    1.3     K.McD           1.  Auto-modified as a result of name changes to a number of properties assiciated with the AutoScale_t structure.
 *                                      2.  Refactored the ConfigureAutoScale() method to use the EvaluateAutoScaleLimits() method.
 *                                      3.  Modified the EvaluateAutoScaleLimits() method to determine the old identifier value of each watch variable contained 
 *                                          in the WatchElementsList property of the workset and to use this to configure the properties of the AutoScale_t 
 *                                          structures.
 *                                          
 *  04/16/15    1.4     K.McD           References
 *              
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, Kawasaki Rail Car and NYTC on
 *                                              12th April 2013 - MOC-0171:
 *                                              
 *                                              1.  MOC-0171-06. All references to 'Fault Logs', including menu options and directory names to be replaced by
 *                                                  'Data Streams' for all projects.
 *
 *                                          2.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified
 *                                              to meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the
 *                                              current naming convention will still apply.
 *                                      
 *                                      Modifications
 *                                      1. Auto-update following renaming of the FileHandling.LogType.DataStream enumerator. Ref.: 1.1.1, 1.2.
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;

using Common.Configuration;

namespace Common.Communication
{
    /// <summary>
    /// A structure to store the fields associated with a data stream of watch variable values.
    /// </summary>
    /// <remarks>
    /// A data stream contains all of the information required to plot historic watch variable values.
    /// </remarks>
    [Serializable]
    public struct DataStream_t
    {
        #region - [Member Variables] -
        /// <summary>
        /// The data stream parameters that are derived from the data dictionary.
        /// </summary>
        public DataStreamTypeParameters_t DataStreamTypeParameters;

        /// <summary>
        /// The number of data samples associated with the data stream.
        /// </summary>
        public short SampleCount;

        /// <summary>
        /// The interval, in ms, between successive data frames.
        /// </summary>
        /// <remarks>
        /// This is equivalent to the sample interval multiplied by the sample multiple.
        /// </remarks>
        public short FrameIntervalMs;

        /// <summary>
        /// The type of log associated with the data stream.
        /// </summary>
        public LogType LogType;

        /// <summary>
        /// The data stream number.
        /// </summary>
        public short StreamNumber;

        /// <summary>
        /// The description of the event that triggered the data stream.
        /// </summary>
        public string EventDescription;

        /// <summary>
        /// The duration of the data stream specified in ms.
        /// </summary>
        /// <remarks>
        /// The duration is equivalent to the number of entries multiplied by the frame interval. 
        /// </remarks>
        public int DurationMs;

        /// <summary>
        /// The duration, in ms, between the first entry and the time of the actual trip.
        /// </summary>
        public int DurationPreTripMs;

        /// <summary>
        /// The duration, in ms, between the time of the actual trip and the last log entry.
        /// </summary>
        public int DurationPostTripMs;

        /// <summary>
        /// The workset used to define the watch elements contained within the data stream.
        /// </summary>
        public Workset_t Workset;

        /// <summary>
        /// The start time of the data stream.
        /// </summary>
        public short TimeOrigin;

        /// <summary>
        /// The stream as a list of individual time stamped data frames.
        /// </summary>
        public List<WatchFrame_t> WatchFrameList;

        /// <summary>
        /// The auto-scale information associated with each watch element.
        /// </summary>
        /// <remarks>Use for auto-scaling of the plotter.</remarks>
        public AutoScale_t[] AutoScaleWatchValues;
        #endregion - [Member Variables] -

        #region - [Constructors] -
        /// <summary>
        /// Initializes a new instance of the structure. Used when initializing a datastream to store fault log data retrieved from the VCU. 
        /// </summary>
        /// <param name="eventRecord">The event record associated with the data stream.</param>
        /// <param name="watchCount">The number of watch variables contained within the data stream.</param>
        /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
        /// <param name="sampleMultiple">The multiple of the base sample interval at which the data is recorded.</param>
        /// <param name="workset">The workset associated with the data stream.</param>
        public DataStream_t(EventRecord eventRecord, short watchCount, short sampleCount, short sampleMultiple, Workset_t workset)
        {
            // Get the log information associated with the event record.
            Log log = Lookup.LogTable.Items[eventRecord.LogIdentifier];

            DataStreamTypeParameters = log.DataStreamTypeParameters;
            StreamNumber = eventRecord.StreamNumber;
            LogType = LogType.DataStream;
            EventDescription = eventRecord.Description;
            FrameIntervalMs = (short)(sampleMultiple * log.SampleIntervalMs);

            SampleCount = sampleCount;
            DurationMs = (SampleCount - 1) * FrameIntervalMs;

            DurationPreTripMs = DataStreamTypeParameters.TripIndex * FrameIntervalMs;
            DurationPostTripMs = DurationMs - DurationPreTripMs;

            Workset = new Workset_t();
            Workset = workset;

            WatchFrameList = new List<WatchFrame_t>(SampleCount);
            AutoScaleWatchValues = new AutoScale_t[workset.Count];
            TimeOrigin = CommonConstants.NotDefined;
        }

        /// <summary>
        /// Initializes a new instance of the structure. Used when instantiating a data stream structure to store the simulated fault log.
        /// </summary>
        /// <param name="logType">The type of log.</param>
        /// <param name="durationMs">The duration of the log, in ms.</param>
        /// <param name="durationPostTripMs">The duration of the log, in ms, following the trip.</param>
        /// <param name="frameIntervalMs">The interval, in ms, between successive data frames.</param>
        public DataStream_t(LogType logType, double durationMs, double durationPostTripMs, short frameIntervalMs)
        {
            DataStreamTypeParameters = new DataStreamTypeParameters_t();
            StreamNumber = CommonConstants.NotDefined;
            LogType = LogType.SimulatedDataStream;
            EventDescription = string.Empty;
            FrameIntervalMs = frameIntervalMs;

            DurationMs = (int)durationMs;
            SampleCount = (short)(DurationMs / frameIntervalMs);
            DurationPostTripMs = (int)durationPostTripMs;
            DurationPreTripMs = (int)(durationMs - durationPostTripMs);

            DataStreamTypeParameters.TripIndex = (short)(((durationMs - durationPostTripMs) / durationMs) * SampleCount);

            Workset = new Workset_t();
            WatchFrameList = new List<WatchFrame_t>(SampleCount);
            AutoScaleWatchValues = new AutoScale_t[Parameter.WatchSize];
            TimeOrigin = CommonConstants.NotDefined;
        }

        /// <summary>
        /// Initializes a new instance of the structure. Used when initializing a data stream to store recorded watch values.
        /// </summary>
        /// <param name="frameIntervalMs">The interval, in ms, between successive data frames.</param>
        public DataStream_t(short frameIntervalMs)
        {
            DataStreamTypeParameters = new DataStreamTypeParameters_t();
            DataStreamTypeParameters.SetToDefaulDataStreamType();

            StreamNumber = CommonConstants.NotDefined;
            LogType = LogType.Watch;
            EventDescription = string.Empty;
            FrameIntervalMs = frameIntervalMs;

            DurationMs = CommonConstants.NotDefined;
            SampleCount = CommonConstants.NotDefined;
            DurationPostTripMs = CommonConstants.NotDefined;
            DurationPreTripMs = CommonConstants.NotDefined;
            
            Workset = new Workset_t();
            WatchFrameList = new List<WatchFrame_t>();
            AutoScaleWatchValues = new AutoScale_t[Parameter.WatchSize];
            TimeOrigin = CommonConstants.NotDefined;
        }
        #endregion - [Constructors] -

        #region - [Methods] -
        /// <summary>
        /// Evaluate the engineering limits from the raw maximum and minimum values contained within the <c>AutoScaleWatchValues</c> property.
        /// </summary>
        /// <remarks>
        /// The <c>Workset</c> property and the maximum and minimum raw values associated with the <c>AutoScalesWatchValues</c> property must be defined 
        /// before using this method.
        /// </remarks>
        public void EvaluateAutoScaleLimits()
        {
            short watchIdentifier;
            WatchVariable watchVariable;
            for (short watchElementIndex = 0; watchElementIndex < Workset.WatchElementList.Count; watchElementIndex++)
            {
                watchIdentifier = Workset.WatchElementList[watchElementIndex];

                try
                {
                    watchVariable = Lookup.WatchVariableTable.Items[watchIdentifier];
                    if (watchVariable == null)
                    {
                        AutoScaleWatchValues[watchElementIndex].WatchElementIndex = watchElementIndex;
                        AutoScaleWatchValues[watchElementIndex].WatchIdentifier = CommonConstants.WatchIdentifierNotDefined;
                        AutoScaleWatchValues[watchElementIndex].OldIdentifier = CommonConstants.OldIdentifierNotDefined;
                        AutoScaleWatchValues[watchElementIndex].ScaleFactor = (double)1.0;
                    }
                    else
                    {
                        AutoScaleWatchValues[watchElementIndex].WatchElementIndex = watchElementIndex;
                        AutoScaleWatchValues[watchElementIndex].WatchIdentifier = watchIdentifier;
                        AutoScaleWatchValues[watchElementIndex].OldIdentifier = watchVariable.OldIdentifier;
                        AutoScaleWatchValues[watchElementIndex].ScaleFactor = watchVariable.ScaleFactor;
                    }
                }
                catch (Exception)
                {
                    AutoScaleWatchValues[watchElementIndex].WatchElementIndex = watchElementIndex;
                    AutoScaleWatchValues[watchElementIndex].WatchIdentifier = CommonConstants.WatchIdentifierNotDefined;
                    AutoScaleWatchValues[watchElementIndex].OldIdentifier = CommonConstants.OldIdentifierNotDefined;
                    AutoScaleWatchValues[watchElementIndex].ScaleFactor = (double)1.0;
                }
            }
        }

        /// <summary>
        /// Configure the <c>AutoScaleWatchValues</c> property from the information contained within the <c>WatchFrameList</c> and <c>Workset</c> properties.
        /// </summary>
        /// <reamarks>
        /// This method is used when the <c>AutoScalesWatchValues</c> property is empty/null. The <c>WatchFrameList</c> and <c>Workset</c> properties must 
        /// be defined before using this method.
        /// </reamarks>
        public void ConfigureAutoScale()
        {
            Debug.Assert(WatchFrameList != null, "DataStream_t.ConfigureAutoScale - [WatchFrameList != null]");
            Debug.Assert(Workset.WatchElementList != null, "DataStream_t.ConfigureAutoScale - [(Workset.WatchElementList != null]");

            AutoScaleWatchValues = new AutoScale_t[Workset.WatchElementList.Count];

            for (int watchElementIndex = 0; watchElementIndex < Workset.WatchElementList.Count; watchElementIndex++)
            {
                AutoScaleWatchValues[watchElementIndex] = new AutoScale_t();
            }

            double valueCurrent;
            for (int frameIndex = 0; frameIndex < WatchFrameList.Count; frameIndex++)
            {
                if (frameIndex == 0)
                {
                    // Initialize both the maximum and minimum values to the current value.
                    for (int watchElementIndex = 0; watchElementIndex < Workset.WatchElementList.Count; watchElementIndex++)
                    {
                        valueCurrent = WatchFrameList[frameIndex].WatchElements[watchElementIndex].Value;
                        AutoScaleWatchValues[watchElementIndex].MaximumRaw = valueCurrent;
                        AutoScaleWatchValues[watchElementIndex].MinimumRaw = valueCurrent;
                    }
                    continue;
                }

                for (int watchElementIndex = 0; watchElementIndex < Workset.WatchElementList.Count; watchElementIndex++)
                {
                    valueCurrent = WatchFrameList[frameIndex].WatchElements[watchElementIndex].Value;
                    if (valueCurrent > AutoScaleWatchValues[watchElementIndex].MaximumRaw)
                    {
                        AutoScaleWatchValues[watchElementIndex].MaximumRaw = valueCurrent;
                    }
                    else if (valueCurrent < AutoScaleWatchValues[watchElementIndex].MinimumRaw)
                    {
                        AutoScaleWatchValues[watchElementIndex].MinimumRaw = valueCurrent;
                    }
                }
            }

            EvaluateAutoScaleLimits();
        }
        #endregion - [Methods] -
    }
}
