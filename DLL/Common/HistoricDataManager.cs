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
 *  File name:  HistoricDataManager.cs
 * 
 *  Revision History
 *  ----------------
 *
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/16/10    1.1     K.McD           1.  Added member variable m_FrameIntervalMs.
 *                                      2.  Modified the ContainsTime() method to take into account the fact that the frame interval is now contained within the watch file. 
 * 
 *  11/19/10    1.2     K.McD           1.  Added the Workset property so that users can access the workset associated with the data contained within the class.
 * 
 *  01/26/11    1.3     K.McD           1.  Modified the Debug.Assert() check on the frame interval to be less stringent.
 * 
 *  03/28/11    1.4     K.McD           1.  Modified to support the modifications to the workset structure associated with the use of the old identifier field 
 *                                          of the watch variables in the definition of the Columns property of a workset.
 * 
 *                                      2.  Renamed the ToWatchElementIndex() method to GetWatchElementIndex. The signature of this method now specifies the old 
 *                                          identifier of the watch variable rather than the watch identifier.
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;

using Common.Communication;
using Common.Configuration;
using Common.Properties;

namespace Common
{
    #region --- Enumerated Data Types ---
    /// <summary>
    /// The different types of data that may be retrieved from the monitor card.
    /// </summary>
    public enum DataType_HistoricDataManager
    {
        /// <summary>
        /// Unassigned data type.
        /// </summary>
        Unassigned = 0x00,

        /// <summary>
        /// Minute Log.
        /// </summary>
        MinuteLog = 0x01,

        /// <summary>
        /// Hour Log.
        /// </summary>
        HourLog = 0x02,

        /// <summary>
        /// Day Log.
        /// </summary>
        DayLog = 0x03,

        /// <summary>
        /// Fault Log.
        /// </summary>
        FaultLog = 0x04,

        /// <summary>
        /// Simulated Fault Log.
        /// </summary>
        SimulatedFaultLog = 0x05,

        /// <summary>
        /// Real Time Data.
        /// </summary>
        RTD = 0x06,

        /// <summary>
        /// Event Log.
        /// </summary>
        EventLog = 0x07,

        /// <summary>
        /// Event Count.
        /// </summary>
        EventCount = 0x08,

        /// <summary>
        /// Configuration Data.
        /// </summary>
        Configuration = 0x09,

        /// <summary>
        /// Snapshot Log.
        /// </summary>
        SnapshotLog = 0x0A,

        /// <summary>
        /// Test Mode.
        /// </summary>
        TestMode = 0x0B
    }

    /// <summary>
    /// The status of the data analysed by the <c>VerifyLog</c> method.
    /// </summary>
    public enum LogStatus
    {
        /// <summary>
        /// Not defined.
        /// </summary>
        Unassigned,

        /// <summary>
        /// Valid fault log.
        /// </summary>
        ValidFaultLog,

        /// <summary>
        /// Valid start-up fault log.
        /// </summary>
        ValidStartUpFaultLog,

        /// <summary>
        /// Valid snapshot log.
        /// </summary>
        ValidSnapshotLog,

        /// <summary>
        /// Valid start-up snapshot log.
        /// </summary>
        ValidStartUpSnapshotLog,

        /// <summary>
        /// Valid general purpose real-time-data.
        /// </summary>
        ValidRTD,

        /// <summary>
        /// The interval between one or more frames of the specified fault/snapshot log was not equal to the expected data interval but was within acceptable limits.
        /// </summary>
        InvalidDataInterval,

        /// <summary>
        /// The interval between one or more frames of the specified fault/snapshot log was outside the acceptable limits.
        /// </summary>
        InvalidDataIntervalOutsideLimits,

        /// <summary>
        /// The interval between one or more frames of the specified fault/snapshot log was negative.
        /// </summary>
        InvalidDataIntervalNegative,

        /// <summary>
        /// The data contained invalid empty log entries, but was plottable.
        /// </summary>
        InvalidEmptyLogEntry
    }
    #endregion --- Enumerated Data Types ---

    /// <summary>
    /// Supports the display of historic data, allowing the time range to be zoomed in and out. Creates a subset of frames to be included in the plot
    /// defined by the <c>StartTime</c> and <c>EndTime</c> properties.
    /// </summary>
    [Serializable]
    public class HistoricDataManager : IHistoricDataManager
    {
        #region --- Constants ---
        /// <summary>
        /// The index value returned if an entry cannot be found. Value: -1.
        /// </summary>
        public const int NotFound = -1;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Reference to the structure containing the watch file information.
        /// </summary>
        private WatchFile_t m_WatchFile;

        /// <summary>
        /// List containing ALL of the data frames in the set.
        /// </summary>
        private List<WatchFrame_t> m_FramesAll;

        /// <summary>
        /// List containing the data frames associated with the specified start and stop times.
        /// </summary>
        private List<WatchFrame_t> m_FramesToPlot;

        /// <summary>
        /// The start time associated with the plot.
        /// </summary>
        private DateTime m_StartTime;

        /// <summary>
        /// The stop time associated with the plot.
        /// </summary>
        private DateTime m_StopTime;

        /// <summary>
        /// The type of log associated with the data.
        /// </summary>
        private LogType m_LogType;

        /// <summary>
        /// The interval, in ms, between successive frames.
        /// </summary>
        private short m_FrameIntervalMs;

        /// <summary>
        /// A list of the AutoScale values associated with the watch file. This is used to determine the watch element index corresponding to a specified old identifier.
        /// </summary>
        private List<AutoScale_t> m_AutoScaleWatchValueList;

        /// <summary>
        /// The workset associated with the data.
        /// </summary>
        private Workset_t m_Workset;
        #endregion -- Member Variables ---

        #region --- Constructor ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="watchFile">The file structure of the log that was saved to disk.</param>
        public HistoricDataManager(WatchFile_t watchFile)
        {
            // Create the list containing all the frames associated with the set.
            Debug.Assert(watchFile.DataStream.WatchFrameList.Count > 1, "HistoricDataManager.Ctor() - [watchFile.WatchFrameList.Count > 1]");
            m_FramesAll = new List<WatchFrame_t>();
            m_FramesAll = watchFile.DataStream.WatchFrameList;

            // Copy All the records in the above list to the list associated with the frames associated with the start and stop times.
            m_FramesToPlot = new List<WatchFrame_t>();
            m_FramesToPlot = m_FramesAll;

            m_StartTime = m_FramesAll[0].CurrentDateTime;
            m_StopTime = m_FramesAll[m_FramesAll.Count - 1].CurrentDateTime;

            m_LogType = watchFile.DataStream.LogType;

            Debug.Assert(watchFile.DataStream.FrameIntervalMs > 0, "HistoricDataManager.Ctor() - [watchFile.DataStream.FrameIntervalMs > 0]");
            m_FrameIntervalMs = watchFile.DataStream.FrameIntervalMs;
            m_Workset = watchFile.DataStream.Workset;

            m_WatchFile = watchFile;

            // Convert the DataStream.AutoScaleWatchValues property of the watch file to a list.
            m_AutoScaleWatchValueList = new List<AutoScale_t>();
            AutoScale_t autoScale;
            for (int watchElementIndex = 0; watchElementIndex < m_WatchFile.DataStream.AutoScaleWatchValues.Length; watchElementIndex++)
            {
                autoScale = m_WatchFile.DataStream.AutoScaleWatchValues[watchElementIndex];
                m_AutoScaleWatchValueList.Add(autoScale);
            }
        }
        #endregion --- Constructor ---

        #region --- Methods ---
        /// <summary>
        /// Get the watch element index corresponding to the specified old identifier. 
        /// </summary>
        /// <remarks>This method uses the information in the <c>AutoScaleWatchValues</c>property of the <c>DataStream</c> associated with the watch file to determine 
        /// the watch element index corresponding to the specified old identifier in the saved data file.</remarks>
        /// <returns>NotFound, if the watch element index associated with the specified old identifier cannot be found; otherwise, returns the watch element index 
        /// corresponding to the specified old identifier.
        /// </returns>
        public short GetWatchElementIndex(short oldIdentifier)
        {
            // Try to find the specified watchIdentifier.
            AutoScale_t returnedAutoScale = m_AutoScaleWatchValueList.Find(delegate(AutoScale_t autoScale) { return autoScale.OldIdentifier == oldIdentifier; });
            if (returnedAutoScale.WatchVariable == null)
            {
                return NotFound;
            }
            else
            {
                return returnedAutoScale.WatchElementIndex;
            }
        }

        /// <summary>
        /// Reset the: <c>StartTime</c>; <c>StopTime</c> and <c>FramesToDisplay</c> properties to their initial values.
        /// </summary>
        public void Reset()
        {
            m_FramesToPlot = m_FramesAll;
            m_StartTime = m_FramesAll[0].CurrentDateTime;
            m_StopTime = m_FramesAll[m_FramesAll.Count - 1].CurrentDateTime;
        }

        /// <summary>
        /// Updates the <c>FramesToDisplay</c> list with those records that have a date/time that is greater than or equal to the <c>StartTime</c> property
        /// value but less than or equal to the <c>StopTime</c> property value.
        /// </summary>
        public void UpdateFrames()
        {
            m_FramesToPlot = new List<WatchFrame_t>();

            // TimeSpan representation of the time between the date/time value associated with the current frame and the start time of the plot.
            TimeSpan currentToStartDiff;

            // TimeSpan representation of the time between the stop time of the plot and the date/time value associated with the current frame.
            TimeSpan stopToCurrentDiff;

            // Scan each of the frames in turn.
            for (int index = 0; index < m_FramesAll.Count; index++)
            {
                // Evaluate the time difference between the time of the current record and the specified start time of the display.
                currentToStartDiff = m_FramesAll[index].CurrentDateTime.Subtract(m_StartTime);

                // Evaluate the time difference between the specified stop time of the display and the time of the current record.
                stopToCurrentDiff = m_StopTime.Subtract(m_FramesAll[index].CurrentDateTime);

                // If the time of the current record falls between these two times add the record to the ArrayList.
                if ((currentToStartDiff.TotalMilliseconds >= 0) && (stopToCurrentDiff.TotalMilliseconds >= 0))
                {
                    m_FramesToPlot.Add(m_FramesAll[index]);
                }
            }
        }

        /// <summary>
        /// Searches through the <c>FramesToDisplay</c> list to match the entry associated with <paramref name="time"/> +(DataIntervalMs)-(0ms). 
        /// </summary>
        /// <returns>
        /// NotFound (-1) if the specified time cannot be found in the generic list; otherwise, returns the index of the array list that
        /// contains the specified time.
        /// </returns>
        public int ContainsTime(DateTime time)
        {
            // Index of entry that matches the specified time.
            int matchedIndex = NotFound;
            for (int index = 0; index < m_FramesToPlot.Count; index++)
            {
                if (m_FramesToPlot[index].CurrentDateTime.Equals(time))
                {
                    matchedIndex = index;
                    return matchedIndex;
                }
            }

            // If no match was found increment the trip time by the interval between successive frames and re-try.
            for (int index = 0; index < m_FramesToPlot.Count; index++)
            {
                if (m_FramesToPlot[index].CurrentDateTime.Equals(time.Add(new TimeSpan(0, 0, 0, 0, m_FrameIntervalMs))))
                {
                    matchedIndex = index;
                    return matchedIndex;
                }
            }
            return matchedIndex;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the stop time associated with the plot.
        /// </summary>
        public DateTime StopTime
        {
            get { return m_StopTime; }
            set { m_StopTime = value; }
        }

        /// <summary>
        /// Gets or sets the start time associated with the plot.
        /// </summary>
        public DateTime StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }

        /// <summary>
        /// Gets the list of watch element frames associated with the specified start and stop times.
        /// </summary>
        public List<WatchFrame_t> FramesToDisplay
        {
            get { return m_FramesToPlot; }
        }

        /// <summary>
        /// Gets the list of all watch element frames.
        /// </summary>
        public List<WatchFrame_t> AllFrames
        {
            get { return m_FramesAll; }
        }

        /// <summary>
        /// Gets the type of log associated with the data.
        /// </summary>
        public LogType LogType
        {
            get { return m_LogType; }
        }

        /// <summary>
        /// Gets the workset associated with the data.
        /// </summary>
        public Workset_t Workset
        {
            get { return m_Workset; }
        }
        #endregion --- Properties ---
    }
}
