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
 *  File name:  Record.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/13/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/22/11    1.1     K.McD           1.  Modified a number of XML tags.
 *  
 *
 */
#endregion --- Revision History ---

using System;
using System.Xml.Serialization;

namespace Common.Configuration
{
    /// <summary>
    /// A class that is used to store the fields that are common to all record types associated with the vehicle control unit (VCU).
    /// </summary>
    [Serializable]
    [XmlRoot()]
    public class Record
    {
        #region --- Member Variables ---
        /// <summary>
        /// The primary key identifier associated with the record.
        /// </summary>
        private short m_Identifier;

        /// <summary>
        /// The help index associated with the record.
        /// </summary>
        private int m_HelpIndex;
        #endregion --- Member Variables ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the primary key identifier of the record.
        /// </summary>
        [XmlElement()]
        public short Identifier
        {
            get { return m_Identifier; }
            set { m_Identifier = value; }
        }

        /// <summary>
        /// Gets or sets the help index associated with the record.
        /// </summary>
        [XmlElement()]
        public int HelpIndex
        {
            get { return m_HelpIndex; }
            set { m_HelpIndex = value; }
        }
        #endregion --- Properties ---
    }
}
