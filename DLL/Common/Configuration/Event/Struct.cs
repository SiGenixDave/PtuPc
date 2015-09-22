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
 *  File name:  Struct.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/20/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/22/11    1.1     K.McD           1.  Corrected one or more XML tags.
 *
 */
#endregion --- Revision History ---

namespace Common.Configuration
{
    /// <summary>
    /// A structure to store the fields associated with an entry from the <c>STRUCT</c> table of the data dictionary.
    /// </summary>
    public struct Struct_t
    {
        #region --- Member Variables ---
        /// <summary>
        /// The structure identifier associated with the record.
        /// </summary>
        private int m_StructureIdentifier;

        /// <summary>
        /// The event variable identifier associated with the record.
        /// </summary>
        private int m_EventVariableIdentifier;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the structure.
        /// </summary>
        /// <param name="structureIdentifier">The structure identifier associated with the record.</param>
        /// <param name="eventVariableIdentifier">The event variable identifier associated with the record.</param>
        public Struct_t(int structureIdentifier, int eventVariableIdentifier)
        {
            m_StructureIdentifier = structureIdentifier; ;
            m_EventVariableIdentifier = eventVariableIdentifier;
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the structure identifier associated with the record.
        /// </summary>
        public int StructureIdentifier
        {
            get { return m_StructureIdentifier; }
            set { m_StructureIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the event variable identifier associated with the record.
        /// </summary>
        public int EventVariableIdentifier
        {
            get { return m_EventVariableIdentifier; }
            set { m_EventVariableIdentifier = value; }
        }
        #endregion --- Properties ---
    }
}
