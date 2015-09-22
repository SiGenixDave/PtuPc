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
 *  File name:  DataDictionaryFileInfo.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author      Comments
 *  10/20/10    1.0     K.McD       1.  First entry into TortoiseSVN.
 *  
 *  06/19/13    1.1     K.McD       1.  Corrected the filename in the header.
 *                                  2.  Modified the XML tag associated with the DataDictionaryInformation_t structure.
 *                                  
 *  04/20/15    1.2     K.McD       1.  Renamed
 */
#endregion --- Revision History ---

using System;

namespace Common.Configuration
{
    /// <summary>
    /// A structure to store the fields associated with the <c>FILEINFO</c> data table of the data dictionary i.e. the project information fields.
    /// </summary>
    [Serializable]
    public struct DataDictionaryInformation_t
    {
        #region --- Member Variables ---
        /// <summary>
        /// The version number of the data dictionary builder utility used to build the data dictionary.
        /// </summary>
        private string m_DataDictionaryBuilderVersion;

        /// <summary>
        /// The name of the data dictionary.
        /// </summary>
        private string m_DataDictionaryName;

        /// <summary>
        /// The project identifier associated with the data dictionary.
        /// </summary>
        private string m_ProjectIdentifier;

        /// <summary>
        /// The number of watch variables in the data dictionary.
        /// </summary>
        private short m_WatchIdentifierCount;

        /// <summary>
        /// The version number of the data dictionary.
        /// </summary>
        private string m_Version;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the structure.
        /// </summary>
        /// <param name="dataDictionaryBuilderVersion">The version number of the data dictionary builder utility used to build the data dictionary.</param>
        /// <param name="dataDictionaryName">The name of the data dictionary.</param>
        /// <param name="projectIdentifier">The project identifier associated with the data dictionary.</param>
        /// <param name="watchIdentifierCount">The number of watch variables in the data dictionary.</param>
        /// <param name="version">The version number of the data dictionary.</param>
        public DataDictionaryInformation_t(string dataDictionaryBuilderVersion, string dataDictionaryName, string projectIdentifier, short watchIdentifierCount,
                                           string version)
        {
            m_DataDictionaryBuilderVersion = dataDictionaryBuilderVersion;
            m_DataDictionaryName = dataDictionaryName;
            m_ProjectIdentifier = projectIdentifier;
            m_WatchIdentifierCount = watchIdentifierCount;
            m_Version = version;
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the version number of the data dictionary builder utility used to build the data dictionary.
        /// </summary>
        public string DataDictionaryBuilderVersion
        {
            get { return m_DataDictionaryBuilderVersion; }
            set { m_DataDictionaryBuilderVersion = value; }
        }

        /// <summary>
        /// Gets or sets the name of the data dictionary.
        /// </summary>
        public string DataDictionaryName
        {
            get { return m_DataDictionaryName; }
            set { m_DataDictionaryName = value; }
        }

        /// <summary>
        /// Gets or sets the project identifier associated with the data dictionary.
        /// </summary>
        public string ProjectIdentifier
        {
            get { return m_ProjectIdentifier; }
            set { m_ProjectIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the number of watch variables in the data dictionary.
        /// </summary>
        public short WatchIdentifierCount
        {
            get { return m_WatchIdentifierCount; }
            set { m_WatchIdentifierCount = value; }
        }

        /// <summary>
        /// Gets or sets the version number of the data dictionary.
        /// </summary>
        public string Version
        {
            get { return m_Version; }
            set { m_Version = value; }
        }
        #endregion --- Properties ---
    }
}
