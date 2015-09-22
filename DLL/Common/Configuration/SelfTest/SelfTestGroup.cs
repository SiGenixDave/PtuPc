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
 *  File name:  GroupList.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/01/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System.Collections.Generic;

namespace Common.Configuration
{
    /// <summary>
    /// A structure to store the fields associated with an entry from the <c>GROUPLIST</c> data table of the data dictionary.
    /// </summary>
    public class SelfTestGroup : Record
    {
        #warning 1st June 2011
        #region --- Member Variables ---
        #region - [Data Dictionary] -
        /// <summary>
        /// The description of the group.
        /// </summary>
        private string m_Description;

        /// <summary>
        /// The attribute field of the group.
        /// </summary>
        private short m_Attribute;

        /// <summary>
        /// The list of self tests associated with the group.
        /// </summary>
        private List<SelfTestRecord> m_SelfTestRecordList;
        #endregion - [Data Dictionary] -

        #region - [VCU] -
        #endregion - [VCU] -
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        public SelfTestGroup()
        {
            Identifier = 0;
            HelpIndex = 0;
            m_Description = string.Empty;
            m_Attribute = 0;
            m_SelfTestRecordList = new List<SelfTestRecord>();

            #region - [VCU Fields] -
            #endregion - [VCU Fields] -
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Override the ToString() method to return the description of the group.
        /// </summary>
        /// <returns>The log description.</returns>
        public override string ToString()
        {
            return Description;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        #region - [Data Dictionary] -
        /// <summary>
        /// Gets or sets the description of the group.
        /// </summary>
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        /// <summary>
        /// Gets or sets the attribute field of the group.
        /// </summary>
        public short Attribute
        {
            get { return m_Attribute; }
            set { m_Attribute = value; }
        }

        /// <summary>
        /// Gets or sets the list of self tests associated with the group.
        /// </summary>
        public List<SelfTestRecord> SelfTestRecordList
        {
            get { return m_SelfTestRecordList; }
            set { m_SelfTestRecordList = value; }
        }
        #endregion - [Data Dictionary] -

        #region - [VCU] -
        #endregion - [VCU] -
        #endregion --- Properties ---
    }
}
