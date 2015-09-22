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
 *  File name:  IEnumBit.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Configuration
{
    /// <summary>
    /// <para>
    /// A structure to store the fields associated with a record from the <c>WATCHENUMBIT</c>, <c>EVENTENUMBIT</c> <c>ANNUNENUMBIT</c> and <c>SELFTESTENUMBIT</c> data tables of the 
    /// data dictionary.
    /// </para>
    /// <para>
    /// Each record defines: (a) the descriptive text corresponding to a particular value or (b) the flag description corresponding to the bit that is represented by the value.
    /// </para>
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
    public interface IEnumBit
    {
        /// <summary>
        /// Gets or sets the enumerator/bitmask identifier associated with the record.
        /// </summary>
        int EnumBitIdentifier {get; set; }

        /// <summary>
        /// Gets or sets the value associated with the description field for this enumerator/bitmask identifier.
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Gets or sets the description associated with the value field for this enumerator/bitmask identifier.
        /// </summary>
        string Description { get; set; }
    }
}
