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
 *  File name:  Workset.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/17/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/26/10    1.1     K.McD           1.  Modifications resulting from the changes to the WorksetCollection class constructor signatures.
 * 
 *  01/26/11    1.2     K.McD           1.  Auto-modified as a result of the property name changes associated with the Parameter class.
 * 
 *  04/27/11    1.3     K.McD           1.  Corrected a number of XML tags.
 *                                      2.  Added the constant used to define the number of display columns associated with a chart recorder workset.
 *                                      3.  Added a static workset collection to manage the worksets associated with the chart recorder.
 *                                      
 *  07/25/13    1.4     K.McD           1.  Modified the Initialize() method to use the WatchSizeFaultLog property of the Parameter class rather than the 
 *                                          WatchSizeFaultLogMax constant.
 *  
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Configuration
{
    /// <summary>
    /// A static class used to access the various workset collections used in the PTU application.
    /// </summary>
    [Serializable]
    public static class Workset
    {
        #region --- Constants ---
        /// <summary>
        /// The maximum number of display columns that the each workset associated with recorded watch variables can support. Value: 3.
        /// </summary>
        public const short ColumnCountMaxRecordedWatch = 3;

        /// <summary>
        /// The maximum number of display columns that the each workset associated with fault log watch variables can support. Value: 1.
        /// </summary>
        public const short ColumnCountMaxFaultLog = 1;

        /// <summary>
        /// The maximum number of display columns that the each workset associated with the chart recorder can support. Value: 1.
        /// </summary>
        public const short ColumnCountMaxChartRecorder = 1;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The workset collection associated with the recording and displaying of watch variables.
        /// </summary>
        private static WorksetCollection m_WorksetCollectionRecordedWatch;

        /// <summary>
        /// The workset collection associated with the recording and displaying of fault log watch variables.
        /// </summary>
        private static WorksetCollection m_WorksetCollectionFaultLog;

        /// <summary>
        /// The workset collection associated with the chart recorder.
        /// </summary>
        private static WorksetCollection m_WorksetCollectionChartRecorder;
        #endregion --- Member Variables ---

        #region --- Methods ---
        /// <summary>
        /// Initializes the static instances of the workset collection classes corresponding to each sub-system.
        /// </summary>
        public static void Initialize()
        {
            m_WorksetCollectionRecordedWatch = new WorksetCollection(WorksetCollectionType.RecordedWatch, Parameter.WatchSize, ColumnCountMaxRecordedWatch);
            m_WorksetCollectionFaultLog = new WorksetCollection(WorksetCollectionType.FaultLog, Parameter.WatchSizeFaultLog, ColumnCountMaxFaultLog);
            m_WorksetCollectionChartRecorder = new WorksetCollection(WorksetCollectionType.Chart, Parameter.WatchSizeChartRecorder, ColumnCountMaxChartRecorder);
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the collection of worksets associated with the recording and displaying of watch variables.
        /// </summary>
        public static WorksetCollection RecordedWatch
        {
            get { return m_WorksetCollectionRecordedWatch; }
        }

        /// <summary>
        /// Gets the collection of worksets associated with the recording and displaying of fault log watch variables.
        /// </summary>
        public static WorksetCollection FaultLog
        {
            get { return m_WorksetCollectionFaultLog; }
        }

        /// <summary>
        /// Gets the collection of worksets associated with the chart recorder.
        /// </summary>
        public static WorksetCollection ChartRecorder
        {
            get { return m_WorksetCollectionChartRecorder; }
        }
        #endregion --- Properties ---
    }
}
