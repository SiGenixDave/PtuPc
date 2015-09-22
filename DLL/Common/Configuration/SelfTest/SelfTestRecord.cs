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
 *  File name:  SelfRecord.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/01/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.Configuration
{
    /// <summary>
    /// A class to store the fields associated with a self test.
    /// </summary>
    [Serializable]
    [XmlRoot()]
    public class SelfTestRecord : Record
    {
        #region --- Member Variables ---
        #region - [Data Dictionary] -
        /// <summary>
        /// The self test number associated with the test.
        /// </summary>
        private short m_SelfTestNumber;

        /// <summary>
        /// The description of the test.
        /// </summary>
        private string m_Description;

        /// <summary>
        /// A list of the self test variables that are associated with the test.
        /// </summary>
        private List<SelfTestVariable> m_SelfTestVariableList;
        #endregion - [Data Dictionary] -

        #region - [VCU] -
        /// <summary>
        /// The date and time of the test as a .NET <c>DateTime</c> object.
        /// </summary>
        private DateTime m_DateTime;
        #endregion - [VCU] -
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class. Zero parameter constructor.
        /// </summary>
        public SelfTestRecord()
        {
        }

        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="identifier">The identifier assoiated with the record.</param>
        public SelfTestRecord(short identifier)
        {
            Identifier = identifier;
            SelfTestNumber = Lookup.SelfTestTable.Items[identifier].SelfTestNumber;
            Description = Lookup.SelfTestTable.Items[identifier].Description;
            HelpIndex = Lookup.SelfTestTable.Items[identifier].HelpIndex;
            m_SelfTestVariableList = new List<SelfTestVariable>();
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Override the ToString() method to return the description of the record.
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
        /// Gets or sets the self test number associated with the test.
        /// </summary>
        [XmlElement()]
        public short SelfTestNumber
        {
            get { return m_SelfTestNumber; }
            set { m_SelfTestNumber = value; }
        }

        /// <summary>
        /// Gets or sets the description of the test.
        /// </summary>
        [XmlElement()]
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        /// <summary>
        /// Gets or sets the list of the self test variables that are associated with the test.
        /// </summary>
        [XmlElement()]
        public List<SelfTestVariable> SelfTestVariableList
        {
            get { return m_SelfTestVariableList; }
            set { m_SelfTestVariableList = value; }
        }
        #endregion - [Data Dictionary] -

        #region - [VCU] -
        /// <summary>
        /// Gets or sets the date and time of the test as a .NET <c>DateTime</c> object.
        /// </summary>
        [XmlElement()]
        public DateTime DateTime
        {
            get { return m_DateTime; }
            set { m_DateTime = value; }
        }
        #endregion - [VCU] -
        #endregion --- Properties ---
    }
}
