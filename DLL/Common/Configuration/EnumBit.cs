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
 *  File name:  EnumBit.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/20/10    1.0     K.McDonald      First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

namespace Common.Configuration
{
    /// <summary>
    /// A structure to store the fields associated with a record from the <c>WATCHENUMBIT</c>, <c>EVENTENUMBIT</c> <c>ANNUNENUMBIT</c> and <c>SELFTESTENUMBIT</c> data tables of the 
    /// data dictionary.
    /// 
    /// Each record defines: (a) the descriptive text corresponding to a particular value or (b) the flag description corresponding to the bit that is represented by the value.
    /// </summary>
    /// <example>
    /// An example of the entries corresponding to the WatchEnumBit table of the data dictionary are shown below.
    /// </example>
    /// <code>
    /// ENUMBITID	VALUE	DESCRIPTION                 - Enumerator Entry.
    /// 553	        0	    Apply                       
    /// 553	        3	    Hold
    /// 553	        7	    Dump
    /// 
    /// ENUMBITID	VALUE	DESCRIPTION                 - Bit Mask Entry.
    /// 501	        1	    Charging Contactor Opening Failure
    /// 501	        2	    Charging Contactor Closing Failure
    /// 501	        4	    Charging Resistor Overtemperature
    /// 501	        8	    DCU/M Hardware Failure
    /// 501	        16	    DC - link Voltage Measurement Failure
    /// 501	        32	    DC - link Input Current Measurement Failure
    /// 501	        64	    Phase 1 Current Measurement Failure
    /// </code>
    public struct EnumBit_t : Common.Configuration.IEnumBit
    {
        #region --- Member Variables ---
        /// <summary>
        /// The enumerator/bitmask identifier associated with the record.
        /// </summary>
        private int m_EnumBitIdentifier;

        /// <summary>
        /// The value associated with the description field for this enumerator/bitmask identifier.
        /// </summary>
        private double m_Value;

        /// <summary>
        /// The description associated with a the value field for this enumerator/bitmask identifier.
        /// </summary>
        private string m_Description;
        #endregion --- Member Variables ---

        #region --- Constructors ---

        /// <summary>
        /// Initializes a new instance of the structure.
        /// </summary>
        /// <param name="enumBitIdentifier">The enumerator/bitmask identifier associated with the record.</param>
        /// <param name="value">The value associated with the description for the specified identifier.</param>
        /// <param name="description">The description associated with the value for the specified identifier.</param>
        public EnumBit_t(int enumBitIdentifier, double value, string description)
        {
            m_EnumBitIdentifier = enumBitIdentifier;
            m_Value = value;
            m_Description = description;
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Override the ToString() method so that the description field is returned.
        /// </summary>
        /// <returns>The description text.</returns>
        public override string ToString()
        {
            return m_Description;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the enumerator/bitmask identifier associated with the record.
        /// </summary>
        public int EnumBitIdentifier
        {
            get { return m_EnumBitIdentifier; }
            set { m_EnumBitIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the value associated with the description field for this enumerator/bitmask identifier.
        /// </summary>
        public double Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        /// <summary>
        /// Gets or sets the description associated with a the value field for this enumerator/bitmask identifier.
        /// </summary>
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
        #endregion --- Properties ---
    }
}