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
 *  File name:  FileHeader.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/13/10    1.1     K.McD           1.  Changes required to support moving the GetUserName() method from class FormAddComments to class General.
 * 
 *  01/06/11    1.2     K.McD           1.  Included the SetToOffline() method in the Header_t structure.
 * 
 *  01/31/11    1.3     K.MCD           1.  Changed a number of XML tags and tidied the layout.
 *  
 *  07/25/11    1.4     K.McD           1.  Modified the name of the SetToOffline() method associated with the Header_t structure to be SetToDiagnostic() in accordance 
 *                                          with the June 2011 sprint review..
 * 
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;

using Common.Communication;
using Common.Configuration;
using Common.Properties;

namespace Common
{
    #region --- Structures ---
    /// <summary>
    /// A structure to store the header information that is included in all data files that are saved to disk;
    /// </summary>
    [Serializable]
    public struct Header_t
    {
        /// <summary>
        /// Flag to indicate whether header information is available. True indicates that header information is available; otherwise, false.
        /// </summary>
        public bool Available;

        /// <summary>
        /// The project information associated with the downloaded data.
        /// </summary>
        public DataDictionaryInformation_t ProjectInformation;

        /// <summary>
        /// The configuration information associated with the target hardware.
        /// </summary>
        public TargetConfiguration_t TargetConfiguration;

        /// <summary>
        /// The name of the user who requested the download.
        /// </summary>
        public string UserName;

        /// <summary>
        /// Any engineer comments associated with the downloaded data.
        /// </summary>
        public string Comments;

        /// <summary>
        /// The date and time when the file was created.
        /// </summary>
        public DateTime DateTimeCreated;

        /// <summary>
        /// The <c>ProductName</c> reference of the PTU application used to collect and save the file.
        /// </summary>
        public string ProductName;

        /// <summary>
        /// The <c>ProductVersion</c> reference of the PTU application used to collect and save the file.
        /// </summary>
        public string ProductVersion;

        /// <summary>
        /// Sets the header to reflect diagnostic mode status i.e. all target configuration parameters will be cleared.
        /// </summary>
        public void SetToDiagnostic()
        {
            TargetConfiguration.CarIdentifier = string.Empty;
            TargetConfiguration.ConversionMask = 0;
            TargetConfiguration.ProjectIdentifier = string.Empty;
            TargetConfiguration.SubSystemName = string.Empty;
            TargetConfiguration.Version = string.Empty;
        }
    }
    #endregion --- Structures ---

    /// <summary>
    /// Class to manage the header information associated with each of the data files.
    /// </summary>
    public static class FileHeader
    {
        #region --- Member Variables ---
        /// <summary>
        /// Header information associated with data downloaded from the target hardware.
        /// </summary>
        private static Header_t m_HeaderCurrent;

        /// <summary>
        /// Header information associated with the last file retrieved from disk.
        /// </summary>
        private static Header_t m_HeaderLastRetrieved;

        /// <summary>
        /// Header information used by the Save All menu option.
        /// </summary>
        private static Header_t m_HeaderSaveAll;
        #endregion --- Member Variables ---

        #region --- Methods ---
        /// <summary>
        /// Initializes the specified header as unavailable.
        /// </summary>
        /// <param name="header">The header that is to be marked as unavailable.</param>
        public static void Initialize(ref Header_t header)
        {
            header.Available = true;
            header.Comments = string.Empty;
            header.DateTimeCreated = DateTime.Now;
            header.ProductName = Application.ProductName;
            header.ProductVersion = Application.ProductVersion;
            header.ProjectInformation = new DataDictionaryInformation_t();
            header.ProjectInformation.DataDictionaryBuilderVersion = Resources.TextUnavailable;
            header.ProjectInformation.DataDictionaryName = Resources.TextUnavailable;
            header.ProjectInformation.ProjectIdentifier = Resources.TextUnavailable;
            header.ProjectInformation.Version = Resources.TextUnavailable;
            header.ProjectInformation.WatchIdentifierCount = 0;
            header.TargetConfiguration.CarIdentifier = string.Empty;
            header.TargetConfiguration.ConversionMask = 0;
            header.TargetConfiguration.ProjectIdentifier = Resources.TextUnavailable;
            header.TargetConfiguration.SubSystemName = Resources.TextUnavailable;
            header.TargetConfiguration.Version = Resources.TextUnavailable;
            header.UserName = General.GetUsername();
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the header information associated with the data downloaded from the target hardware. 
        /// </summary>
        /// <remarks>All values will be null or 0, as appropriate, until valid communications is established.</remarks>
        public static Header_t HeaderCurrent
        {
            get { return m_HeaderCurrent; }
            set { m_HeaderCurrent = value; }
        }

        /// <summary>
        /// Gets or sets the header information associated with the last file retrieved from disk.
        /// </summary>
        /// <remarks>All values will be null or 0, as appropriate, until valid communications is established. </remarks>
        public static Header_t HeaderLastRetrieved
        {
            get { return m_HeaderLastRetrieved; }
            set { m_HeaderLastRetrieved = value; }
        }

        /// <summary>
        /// Gets or sets the header information used by the Save All menu option.
        /// </summary>
        /// <remarks>All values will be null or 0, as appropriate, until valid communications is established. </remarks>
        public static Header_t HeaderSaveAll
        {
            get { return m_HeaderSaveAll; }
            set { m_HeaderSaveAll = value; }
        }
        #endregion --- Properties ---
    }
}
