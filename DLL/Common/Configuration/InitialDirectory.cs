#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    Configuration
 * 
 *  File name:  InitialDirectory.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/21/10    1.0     K.McD           First Release.
 *  
 *  05/13/15    1.1     K.McD           References
 *                                      1.  The 'Tools/Convert Engineering Database' menu option is to be modified to track the read directory where the engineering
 *                                          data dictionary (.e1) database and the project specific PTU configuration(.e1) database are located. This directory will
 *                                          remain the default directory for the remainder of the session.
 *                                          
 *                                      Modifications
 *                                      1.  Added the E1FilesRead property and the associated static string member variable, m_PathE1FilesRead. This property gets and
 *                                          sets the default read directory for the (.e1) database files for the duration of the current session.
 *                                      2.  Modified the Reset() method to include the initialization of the m_PathE1FilesRead member variable.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Configuration
{
    /// <summary>
    /// Class to manage the initial directory paths when reading and writing data from/to the PTU application data directories using the <c>FileDialog</c> derived classes.
    /// </summary>
    public static class InitialDirectory
    {
        #region --- Member Variables ---
        /// <summary>
        /// The initial directory when reading '.E1' database files.
        /// </summary>
        private static string m_PathE1FilesRead;

        /// <summary>
        /// The initial directory when reading watch files.
        /// </summary>
        private static string m_PathWatchFilesRead;

        /// <summary>
        /// The initial directory when writing watch files.
        /// </summary>
        private static string m_PathWatchFilesWrite;

        /// <summary>
        /// The initial directory when reading fault logs.
        /// </summary>
        private static string m_PathFaultLogsRead;

        /// <summary>
        /// The initial directory when writing fault logs.
        /// </summary>
        private static string m_PathFaultLogsWrite;

        /// <summary>
        /// The initial directory when reading simulated fault logs.
        /// </summary>
        private static string m_PathSimulatedFaultLogsRead;

        /// <summary>
        /// The initial directory when writing simulated fault logs.
        /// </summary>
        private static string m_PathSimulatedFaultLogsWrite;

        /// <summary>
        /// The initial directory when reading event logs.
        /// </summary>
        private static string m_PathEventLogsRead;

        /// <summary>
        /// The initial directory when writing event logs.
        /// </summary>
        private static string m_PathEventLogsWrite;

        /// <summary>
        /// The initial directory when reading screen capture files.
        /// </summary>
        private static string m_PathScreenCaptureFilesRead;

        /// <summary>
        /// The initial directory when writing screen capture files.
        /// </summary>
        private static string m_PathScreenCaptureFilesWrite;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Static constructor. Sets the initial directory paths to their default values.
        /// </summary>
        static InitialDirectory()
        {
            Reset();
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Resets the initial directory paths to their default values.
        /// </summary>
        /// <remarks>A call to this method should be made whenever the PTU application data directory is changed.</remarks>
        public static void Reset()
        {
            m_PathWatchFilesRead = DirectoryManager.PathWatchFiles;
            m_PathWatchFilesWrite = DirectoryManager.PathWatchFiles;

            m_PathFaultLogsRead = DirectoryManager.PathFaultLogs;
            m_PathFaultLogsWrite = DirectoryManager.PathFaultLogs;

            m_PathSimulatedFaultLogsRead = DirectoryManager.PathSimulatedFaultLogs;
            m_PathSimulatedFaultLogsWrite = DirectoryManager.PathSimulatedFaultLogs;

            m_PathEventLogsRead = DirectoryManager.PathEventLogs;
            m_PathEventLogsWrite = DirectoryManager.PathEventLogs;

            m_PathScreenCaptureFilesRead = DirectoryManager.PathScreenCaptureFiles;
            m_PathScreenCaptureFilesWrite = DirectoryManager.PathScreenCaptureFiles;

            m_PathE1FilesRead = DirectoryManager.PathPTUConfigurationFiles;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the initial directory when reading watch files. 
        /// </summary>
        public static string WatchFilesRead
        {
            get { return m_PathWatchFilesRead; }
            set { m_PathWatchFilesRead = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory when writing watch files. 
        /// </summary>
        public static string WatchFilesWrite
        {
            get { return m_PathWatchFilesWrite; }
            set { m_PathWatchFilesWrite = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory when reading fault logs. 
        /// </summary>
        public static string FaultLogsRead
        {
            get { return m_PathFaultLogsRead; }
            set { m_PathFaultLogsRead = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory when writing fault logs.
        /// </summary>
        public static string FaultLogsWrite
        {
            get { return m_PathFaultLogsWrite; }
            set { m_PathFaultLogsWrite = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory when reading simulated fault logs. 
        /// </summary>
        public static string SimulatedFaultLogsRead
        {
            get { return m_PathSimulatedFaultLogsRead; }
            set { m_PathSimulatedFaultLogsRead = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory when writing simulated fault logs.
        /// </summary>
        public static string SimulatedFaultLogsWrite
        {
            get { return m_PathSimulatedFaultLogsWrite; }
            set { m_PathSimulatedFaultLogsWrite = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory when reading event logs. 
        /// </summary>
        public static string EventLogsRead
        {
            get { return m_PathEventLogsRead; }
            set { m_PathEventLogsRead = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory when writing event logs. 
        /// </summary>
        public static string EventLogsWrite
        {
            get { return m_PathEventLogsWrite; }
            set { m_PathEventLogsWrite = value; }
        }


        /// <summary>
        /// Gets or sets the initial directory when reading screen capture files. 
        /// </summary>
        public static string ScreenCaptureFilesRead
        {
            get { return m_PathScreenCaptureFilesRead; }
            set { m_PathScreenCaptureFilesRead = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory when writing screen capture files. 
        /// </summary>
        public static string ScreenCaptureFilesWrite
        {
            get { return m_PathScreenCaptureFilesWrite; }
            set { m_PathScreenCaptureFilesWrite = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory when reading '.E1' database files. 
        /// </summary>
        public static string E1FilesRead
        {
            get { return m_PathE1FilesRead; }
            set { m_PathE1FilesRead = value; }
        }
        #endregion --- Properties ---
    }
}
