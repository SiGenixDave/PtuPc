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
 *  File name:  WatchFrame.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/13/10    1.0     K.McDonald      1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Communication
{
    /// <summary>
    /// The watch element data associated with an individual frame.
    /// </summary>
    /// <remarks>A frame corresponds to all the watch elements retrieved from a single communiation transaction.</remarks>
    [Serializable]
    public struct WatchFrame_t
    {
        /// <summary>
        /// The date/time associated with the current frame as a .NET <c>DateTime</c> object.
        /// </summary>
        private DateTime m_CurrentDateTime;

        /// <summary>
        /// The array of watch elements retrieved from the target hardware.
        /// </summary>
        private WatchElement_t[] m_WatchElements;

        /// <summary>
        /// Gets or sets the date/time associated with the current frame.
        /// </summary>
        public DateTime CurrentDateTime
        {
            get { return m_CurrentDateTime; }
            set { m_CurrentDateTime = value; }
        }

        /// <summary>
        /// Gets or sets the array of watch elements retrieved from the target hardware.
        /// </summary>
        public WatchElement_t[] WatchElements
        {
            get { return m_WatchElements; }
            set { m_WatchElements = value; }
        }
    }
}
