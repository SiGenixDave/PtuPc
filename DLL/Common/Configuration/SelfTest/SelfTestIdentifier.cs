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
 *  File name:  SelfTestIdentifier.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  06/01/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

namespace Common.Configuration
{
    /// <summary>
    /// A structure to store the fields associated with an entry from the <c>SELFTESTIDS</c> table of the data dictionary.
    /// </summary>
    public struct SelfTestIdentifier_t
    {
        #region --- Member Variables ---
        /// <summary>
        /// The self test identifier associated with the record.
        /// </summary>
        private short m_SelfTestIdentifier;

        /// <summary>
        /// The self test number associated with the record.
        /// </summary>
        private short m_SelfTestNumber;

        /// <summary>
        /// The self test variable identifier associated with the record.
        /// </summary>
        private short m_SelfTestVariableIdentifier;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the structure.
        /// </summary>
        /// <param name="selfTestIdentifier">The value of the <c>SELFTESTID</c> field.</param>
        /// <param name="selfTestNumber">The value of the <c>SELFTESTNUMBER</c> field.</param>
        /// <param name="selfTestVariableIdentifier">The value of the <c>SELFTESTVARID</c> field.</param>
        public SelfTestIdentifier_t(short selfTestIdentifier, short selfTestNumber, short selfTestVariableIdentifier)
        {
            m_SelfTestIdentifier = selfTestIdentifier; ;
            m_SelfTestNumber = selfTestNumber;
            m_SelfTestVariableIdentifier = selfTestVariableIdentifier;
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the self test identifier associated with the record.
        /// </summary>
        public short SelfTestIdentifier
        {
            get { return m_SelfTestIdentifier; }
            set { m_SelfTestIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the self test number associated with the record.
        /// </summary>
        public short SelfTestNumber
        {
            get { return m_SelfTestNumber; }
            set { m_SelfTestNumber = value; }
        }

        /// <summary>
        /// Gets or sets the self test variable identifier associated with the record.
        /// </summary>
        public short SelfTestVariableIdentifier
        {
            get { return m_SelfTestVariableIdentifier; }
            set { m_SelfTestVariableIdentifier = value; }
        }
        #endregion --- Properties ---
    }
}
