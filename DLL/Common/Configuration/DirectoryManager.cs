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
 *  File name:  DirectoryManager.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author      Comments
 *  04/20/10    1.0     K.McD       First Release.
 *  
 *  07/04/13    1.1     K.McD       1.  Modified the PathWorksetFiles property to use the the '\Bombardier\Portable Test Unit\Workset Files' sub-directory of the
 *                                      directory that serves as the common repository for the current roaming user (User Application Data directory) rather than
 *                                      the startup directory. 
 *                                          
 *                                      Files installed to the startup directory in Windows 7/8 are not installed as write-enabled for the standard user. This causes
 *                                      problems during initialization of the PTU as the PTU does not have the required permissions to open the workset files unless
 *                                      the properties of the PTU.exe file are modified to 'Run as Administrator' following installation of the PTU. If this i carried
 *                                      out, the the PTU triggers a User Account Control (UAC) message every time the PTU is run. 
 *                                          
 *                                      Note:
 *                                      On Windows 7/8 systems the User Application Data directory is 'C:\<User>\AppData\Roaming', if the application is installed on a
 *                                      single user basis. On XP systems the directory is 'C:\Documents and Settings\<User>\Application Data' or 'C:\Documents and
 *                                      Settings\All Users\Application Data' depending upon whether the application was installed on an all user or single user basis
 *                                      (By default, all of the above directories are hidden).
 *                                          
 *                                      On Windows 7/8 systems the directory that serves as a common repository for application-specific data that is used by all users
 *                                      is 'C:\ProgramData' (By default, This is a hidden directory). On XP systems the directory is either:
 *                                      'C:\Documents and Settings\<User>\Application Data' or 'C:\Documents and Settings\All Users\Application Data' depending upon
 *                                      whether the application was installed on an all user or single user basis (By default, these directories are, again, hidden).
 *                                          
 *  07/16/13    1.2     K.McD       1.  Modified the PathPTUConfigurationFiles property to use the '\Bombardier\Portable Test Unit\PTU Configuration Files' sub-directory
 *                                      of the directory that serves as the common repository for the current roaming user (User Application Data directory) rather than
 *                                      the startup directory.
 *                                          
 *                                      This modification was the result of an exception being thrown when, following an update of the configuration file to a newer
 *                                      version, the PTU detected a mismatch between the version of the configuration file and that of the embedded software. This
 *                                      mismatch resulted in the PTU asking if the user wanted to select the data dictionary associated with the current embedded
 *                                      software. On selecting Yes, an UnauthorizedAccessException was thrown when the PTU tried to copy the matched configuration file
 *                                      to PTU Configuration.xml because the \Configuration Files sub-directory did not have write-enabled access for the standard user.
 *                                      
 *  08/12/15    1.3     K.McD       References
 *                                  1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                      from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                     
 *                                      1.  On the R188 project only, all references to 'PTU' are to be replaced with 'PTE' and all occurrences of 'Portable Test Unit'
 *                                          are to be replaced with 'Portable Test Equipment'. Where support for multiple legends is not possible, 'Portable Test
 *                                          Application' is to be used as an alternative to 'Portable Test Unit'/'Portable Test Equipment.
 *  
 *                                  Modifications.
 *                                  1.  Auto-update as a result of name changes to a number of string resources.
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Common.Properties;

namespace Common.Configuration
{
    /// <summary>
    /// Class to create and manage the PTU configuration and data directories.
    /// </summary>
    public static class DirectoryManager
    {
        #region --- Member Variables ---
        /// <summary>
        /// The path for the PTU application data directory.
        /// </summary>
        private static string m_PathPTUApplicationData;

        #region - [Read Only] -
        /// <summary>
        /// The path for current users MyDocuments folder.
        /// </summary>
        private static readonly string m_PathMyDocuments;

        /// <summary>
        /// The path for the start-up directory.
        /// </summary>
        private static readonly string m_PathPTUApplicationStartup;

        #region - [Data Directories] -
        /// <summary>
        /// The path, relative to the directory that serves as a common repository for : (a) the application-specific data that is used by all users or 
        /// (b) the current roaming user, where the PTU data is stored.
        /// </summary>
        private static readonly string m_PathRelativePTUApplicationData = Resources.PathRelativeApplicationData;

        /// <summary>
        /// The path, relative to the PTU Application Data directory, where the saved watch files are stored. 
        /// </summary>
        private static readonly string m_PathRelativeWatchFiles = Resources.PathRelativeWatchFiles;

        /// <summary>
        /// The path, relative to the PTU Application Data directory, where fault logs are stored.
        /// </summary>
        private static readonly string m_PathRelativeFaultLogs = Resources.PathRelativeFaultLogs;

        /// <summary>
        /// The path, relative to the PTU Application Data directory, where simulated fault logs are stored.
        /// </summary>
        private static readonly string m_PathRelativeSimulatedFaultLogs = Resources.PathRelativeSimulatedFaultLogs;

        /// <summary>
        /// The path, relative to the PTU Application Data directory, where event logs are stored.
        /// </summary>
        private static readonly string m_PathRelativeEventLogs = Resources.PathRelativeEventLogs;

        /// <summary>
        /// The path, relative to the PTU Application Data directory, where the screen capture files are stored.
        /// </summary>
        private static readonly string m_PathRelativeScreenCaptureFiles = Resources.PathRelativeScreenCaptureFiles;
        #endregion - [Data Directories] -

        #region - [ Configuration Directories] -
        /// <summary>
        /// The path, relative to the User PTU Application Data Directory, where the configuration files are stored.
        /// </summary>
        private static readonly string m_PathRelativePTUConfigurationFiles = Resources.PathRelativeConfigurationFiles;

        /// <summary>
        /// The path, relative to the User PTU Application Data Directory, where the workset files are stored.
        /// </summary>
        private static readonly string m_PathRelativeWorksetFiles = Resources.PathRelativeWorksetFiles;

        /// <summary>
        /// The path, relative to the start-up directory, where the diagnostic help files are stored.
        /// </summary>
        private static readonly string m_PathRelativeDiagnosticHelpFiles = Resources.PathRelativeDiagnosticHelpFiles;
        #endregion - [ Configuration Directories] -
        #endregion - [Read Only] -
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Static constructor. Initializes the class.
        /// </summary>
        static DirectoryManager()
        {
            //Set the path for the PTU application data directory to the default value.
            SetPTUApplicationDataPathToDefault();

            // Set the path for the start-up directory.
            m_PathPTUApplicationStartup = Application.StartupPath;

            // Set the path for current users MyDocuments folder.
            m_PathMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Sets the path for the PTU Application Data directory.
        /// </summary>
        /// <param name="pTUApplicationDataPath">The required path for the PTU Application Data directory.</param>
        public static void SetPTUApplicationDataPath(string pTUApplicationDataPath)
        {
            m_PathPTUApplicationData = pTUApplicationDataPath;
        }

        /// <summary>
        /// Sets the path for the PTU Application Data directory to the default value.
        /// </summary>
        public static void SetPTUApplicationDataPathToDefault()
        {
            m_PathPTUApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + m_PathRelativePTUApplicationData;
        }

        /// <summary>
        /// Checks whether each of the the configuration directories exist and, if not, creates them.
        /// </summary>
        public static void CreateConfigurationSubDirectories()
        {
            // Reference to the DirectoryInfo class.
            DirectoryInfo directoryInfo;

            directoryInfo = new DirectoryInfo(PathPTUConfigurationFiles);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            directoryInfo = new DirectoryInfo(PathWorksetFiles);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            directoryInfo = new DirectoryInfo(PathDiagnosticHelpFiles);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        /// <summary>
        /// Checks whether each of the data directories exist and, if not, creates them.
        /// </summary>
        /// <remarks>If using a PTU Application Data directory other than the default, the SetPTUApplicationDataPath method must called prior to making 
        /// this call.</remarks>
        public static void CreateDataSubDirectories()
        {
            // Reference to the DirectoryInfo class.
            DirectoryInfo directoryInfo;

            directoryInfo = new DirectoryInfo(PathWatchFiles);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            // Fault Log directory.
            directoryInfo = new DirectoryInfo(PathFaultLogs);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            // Simulated Fault Log directory.
            directoryInfo = new DirectoryInfo(PathSimulatedFaultLogs);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            // Event Log directory.
            directoryInfo = new DirectoryInfo(PathEventLogs);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            // Screen Capture Files directory.
            directoryInfo = new DirectoryInfo(PathScreenCaptureFiles);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the path for the PTU start-up directory.
        /// </summary>
        public static string PathPTUApplicationStartup
        {
            get
            {
                return m_PathPTUApplicationStartup;
            }
        }

        /// <summary>
        /// Gets the path for the PTU Application Data directory.
        /// </summary>
        public static string PathPTUApplicationData
        {
            get
            {
                return m_PathPTUApplicationData;
            }
        }

        /// <summary>
        /// Gets the default path for PTU Application Data directory.
        /// </summary>
        public static string PathDefaultPTUApplicationData
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + Resources.PathRelativeApplicationData;
            }
        }

        /// <summary>
        /// Gets the path for current users personal data files.
        /// </summary>
        public static string PathMyDocuments
        {
            get { return m_PathMyDocuments; }
        }

        #region - [Data Directories] -
        /// <summary>
        /// Gets the path for the recorded watch/evaluate files.
        /// </summary>
        public static string PathWatchFiles
        {
            get
            {
                return m_PathPTUApplicationData + m_PathRelativeWatchFiles;
            }
        }

        /// <summary>
        /// Gets the path for the fault logs.
        /// </summary>
        public static string PathFaultLogs
        {
            get
            {
                return m_PathPTUApplicationData + m_PathRelativeFaultLogs;
            }
        }

        /// <summary>
        /// Gets the path for the simulated fault logs.
        /// </summary>
        public static string PathSimulatedFaultLogs
        {
            get
            {
                return m_PathPTUApplicationData + m_PathRelativeSimulatedFaultLogs;
            }
        }

        /// <summary>
        /// Gets the path for the event logs.
        /// </summary>
        public static string PathEventLogs
        {
            get
            {
                return m_PathPTUApplicationData + m_PathRelativeEventLogs;
            }
        }

        /// <summary>
        /// Gets the path for the screen capture files.
        /// </summary>
        public static string PathScreenCaptureFiles
        {
            get
            {
                return m_PathPTUApplicationData + m_PathRelativeScreenCaptureFiles;
            }
        }
        #endregion - [Data Directories] -

        #region - [Configuration Directories] -
        /// <summary>
        /// Gets the path for the PTU configuration files.
        /// </summary>
        public static string PathPTUConfigurationFiles
        {
            get
            {
                // The configuration files are now located in the 'Bombardier\Portable Test Unit\Configuration Files' sub-directory of the directory that serves as a
                // common repository for the current roaming user, see Revision History 1.2.
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Resources.PathRelativeApplicationData +
                       Resources.PathRelativeConfigurationFiles;
            }
        }

        /// <summary>
        /// Gets the path for the workset files.
        /// </summary>
        public static string PathWorksetFiles
        {
            get
            {
                // The workset files are now located in the 'Bombardier\Portable Test Unit\Workset Files' sub-directory of the directory that serves as a common
                // repository for the current roaming user, see Revision History 1.1.
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Resources.PathRelativeApplicationData +
                       Resources.PathRelativeWorksetFiles;
            }
        }

        /// <summary>
        /// Gets path for the diagnostic help files.
        /// </summary>
        public static string PathDiagnosticHelpFiles
        {
            get
            {
                return m_PathPTUApplicationStartup + Resources.PathRelativeDiagnosticHelpFiles;
            }
        }
        #endregion - [Configuration Directories] -
        #endregion --- Properties ---
    }
}