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
 *  File name:  IHistoricDataManager.cs
 * 
 *  Revision History
 *  ----------------
 *
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/19/10    1.1     K.McD           1.  Added the Workset property.
 * 
 *  03/28/11    1.2     K.McD           1.  Renamed the ToWatchElementIndex() method to GetWatchElementIndex.
 *                                      2.  Modified the signature of the GetWatchElementIndex() method.
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;

using Common.Communication;
using Common.Configuration;

namespace Common

{
    /// <summary>
    /// Interface to support the display of historic data, allowing the time range to be zoomed in and out. Creates a subset of frames to be included in the plot
    /// defined by the <c>StartTime</c> and <c>EndTime</c> properties.
    /// </summary>
    public interface IHistoricDataManager
    {
        #region - [Methods] -
        /// <summary>
        /// Get the watch element index corresponding to the specified old identifier. 
        /// </summary>
        /// <remarks>This method uses the AutoScaleWatchI</remarks>
        /// <returns>NotFound, if the watch index corresponding to the specified old identifier cannot be found; otherwise, returns the watch element index 
        /// corresponding to the specified old identifier.
        /// </returns>
        short GetWatchElementIndex(short oldIdentifier);

        /// <summary>
        /// Reset the: <c>StartTime</c>; <c>StopTime</c> and <c>FramesToDisplay</c> properties to their initial values.
        /// </summary>
        void Reset();

        /// <summary>
        /// Updates the <c>FramesToDisplay</c> list with those records that have a date/time that is greater than or equal to the <c>StartTime</c> property
        /// value but less than or equal to the <c>StopTime</c> property value.
        /// </summary>
        void UpdateFrames();

        /// <summary>
        /// Searches through the <c>FramesToDisplay</c> list to match the entry associated with <paramref name="time"/> +(DataIntervalMs)-(0ms). 
        /// </summary>
        /// <returns>
        /// NotFound (-1) if the specified time cannot be found in the generic list; otherwise, returns the index of the array list that
        /// contains the specified time.
        /// </returns>
        int ContainsTime(DateTime time);
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets or sets the stop time associated with the plot.
        /// </summary>
        DateTime StopTime { get; set; }

        /// <summary>
        /// Gets or sets the start time associated with the plot.
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// Gets the list of watch element frames associated with the specified start and stop times.
        /// </summary>
        List<WatchFrame_t> FramesToDisplay { get; }

        /// <summary>
        /// Gets the list of all watch element frames.
        /// </summary>
        List<WatchFrame_t> AllFrames { get; }

        /// <summary>
        /// Gets the type of log associated with the data.
        /// </summary>
        LogType LogType { get; }

        /// <summary>
        /// Gets the workset associated with the data.
        /// </summary>
        Workset_t Workset { get; }
        #endregion - [Properties] -
    }
}
