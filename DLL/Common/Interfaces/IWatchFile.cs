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
 *  Project:    Common
 * 
 *  File name:  IWatchFile.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/20/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common
{
    /// <summary>
    /// Interface for any class that plots saved watch data.
    /// </summary>
    public interface IWatchFile
    {
        #region - [Methods] -
        /// <summary>
        /// Save the WatchFile_t structure to disk.
        /// </summary>
        void SaveWatchFile();
        #endregion - [Methods] -

        #region - [Properties ] -
        /// <summary>
        /// Gets or sets the de-serialized recorded watch data that is to be displayed.
        /// </summary>
        WatchFile_t WatchFile { get; set; }
        #endregion - [Properties ] -
    }
}

