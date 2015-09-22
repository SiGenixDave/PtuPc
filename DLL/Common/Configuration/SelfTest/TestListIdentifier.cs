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
 *  File name:  TestListIdentifier.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/02/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

namespace Common.Configuration
{
    /// <summary>
    /// A structure to store the fields associated with an entry from the <c>TESTLISTIDS</c> table of the data dictionary.
    /// </summary>
    public struct TestListIdentifier_t
    {
        #region --- Member Variables ---
        /// <summary>
        /// The test list identifier associated with the record.
        /// </summary>
        private int m_TestListIdentifier;

        /// <summary>
        /// The self test identfier associated with the record.
        /// </summary>
        private int m_SelfTestIdentifier;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the structure.
        /// </summary>
        /// <param name="testListIdentifier">The group list identifier associated with the record.</param>
        /// <param name="selfTestIdentifier">The self test identifier associated with the record.</param>
        public TestListIdentifier_t(int testListIdentifier, int selfTestIdentifier)
        {
            m_TestListIdentifier = testListIdentifier; ;
            m_SelfTestIdentifier = selfTestIdentifier;
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the test list identifier associated with the record.
        /// </summary>
        public int TestListIdentifier
        {
            get { return m_TestListIdentifier; }
            set { m_TestListIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the self test identfier associated with the record.
        /// </summary>
        public int SelfTestIdentifier
        {
            get { return m_SelfTestIdentifier; }
            set { m_SelfTestIdentifier = value; }
        }
        #endregion --- Properties ---
    }
}
