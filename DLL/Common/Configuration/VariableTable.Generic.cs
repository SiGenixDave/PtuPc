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
 *  File name:  VariableDataTable.Generic.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  10/20/10    1.1     K.McD           1.  Added a try/catch block to method BuildBitCountTable() to cater for null entries in the variable data table i.e. if the
 *                                          identifier count is non contiguous.
 * 
 *  03/28/11    1.2     K.McD           1.  Modified the BuildBitCountTable() method to skip processing of the current entry if it is a null value.
 * 
 *  03/31/11    1.3     K.McD           1.  Modified a number of XML tags and renamed a number of parameters.
 *                                      2.  Modified the following methods such that any checks made on the validity of the watch variable associated with the identifier 
 *                                          value were moved to be within the try/catch block to ensure that an exception wasn't thrown: GetFlagStateList(), 
 *                                          GetAssertedFlagList(), GetBitCount() and GetFlagDescriptions().
 *                                          
 *  08/04/11    1.4     K.McD           1.  Added the i08 data type to the DataType_e enumertor.
 *  
 *  10/02/11    1.5     K.McD           1.  Corrected the implemetation of the check to determine which bits of a bitmask watch variable were set in the: 
 *                                          GetFlagStateList(), GetAssertedFlagList(), GetFlagDescription() and the BuildBitCountTable() methods.
 *                                          
 *  05/06/15    1.6     K.McD           References
 *                                      1.  SNCR - R188 PTU [20 Mar 2015] Item 13. On project R188, the bitmask flags associated with the ‘IPA Status’ bitmask do not tie
 *                                          in with the bitmask value of 0x1034 as the FormShowFlags dialog box shows the first two flags associated with the bitmask as
 *                                          being asserted. On further investigation it was found that the first two bits of the ‘IPA Status’ bitmask had not been defined
 *                                          and were therefore not added to the list of bitmask flags shown by the FormShowFlags dialog box.
 *                                          
 *                                      Modifications
 *                                      1.  Modified the GetFlagStateList() method to include an entry with the description "-" in the list shown by the FormShowFlags
 *                                          dialog box if a flag within the bitmask is not defined.
 *                                      
 *          
 */
#endregion --- Revision History ---

using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace Common.Configuration
{
    #region - [Enumerated Data Types] -
    /// <summary>
    /// The type of watch variable i.e. scalar; bitmask or enumerator.
    /// </summary>
    public enum VariableType
    {
        /// <summary>
        /// Scalar watch variable.
        /// </summary>
        Scalar,

        /// <summary>
        /// Enumerator watch variable.
        /// </summary>
        Enumerator,

        /// <summary>
        /// Bitmask watch variable.
        /// </summary>
        Bitmask,

        /// <summary>
        /// Undefined watch variable i.e any of the above.
        /// </summary>
        Undefined
    }


    /// <summary>
    /// The data types that are supported e.g. : unsigned 8 bit; unsigned 16 bit; signed 16 bit etc.
    /// </summary>
    public enum DataType_e
    {
        /// <summary>
        /// Unsigned 8 bit value - byte.
        /// </summary>
        u08 = 0,

        /// <summary>
        /// Unsigned 16 bit value - ushort. 
        /// </summary>
        u16 = 1,

        /// <summary>
        /// Unsigned 32 bit value - uint.
        /// </summary>
        u32 = 2,

        /// <summary>
        /// Signed 8 bit value.
        /// </summary>
        i08 = 3,

        /// <summary>
        /// Signed 16 bit value - short.
        /// </summary>
        i16 = 4,

        /// <summary>
        /// Signed 32 bit value - int.
        /// </summary>
        i32 = 5,
    }
    #endregion - [Enumerated Data Types] -

    #region - [Structures] -
    /// <summary>
    /// Structure defining the current state of a bitmask flag.
    /// </summary>
    public struct FlagState_t
    {
        /// <summary>
        /// The bit index associated with the flag.
        /// </summary>
        public byte Bit;

        /// <summary>
        /// The state i.e. true or false of the flag.
        /// </summary>
        public bool State;

        /// <summary>
        /// The description associated with the flag.
        /// </summary>
        public string Description;
    }
    #endregion - [Structures] -

    /// <summary>
    /// An abstract generic class to help simplify access to the vehicle control unit (VCU) variables contained within the data dictionary. The type variable T can be of
    /// types: DataDictionary.WATCHVARIABLESDataTable; DataDictionary.EVENTVARIABLESDataTable; or DataDictionary.SELFTESTVARIABLESDataTable and E can be of types:  
    /// DataDictionary.WATCHENUMBITDataTable; DataDictionary.EVENTVARIABLESDataTable; DataDictionary.SELFTESTENUMBITDataTable.
    /// </summary>
    /// <typeparam name="R">The class/interface defining the fields associated with the records of the data table specified by the type parameter T.</typeparam>>
    /// <typeparam name="T">The primary key data table of the data dictionary containing the variables.</typeparam>
    /// <typeparam name="E">The data of the data dictionary containing the enumerator/bitmask records.</typeparam>
    public abstract class VariableTable<R, T, E> : Table<R, T>
        where R : Variable
        where T : DataTable, IEnumerable
        where E : DataTable, IEnumerable
    {
        #region --- Constants ---
        /// <summary>
        /// The number of bits associated with the data type u08. Value: 8.
        /// </summary>
        private const uint u08 = 8;

        /// <summary>
        /// The number of bits associated with the data type u16. Value: 16.
        /// </summary>
        private const uint u16 = 16;

        /// <summary>
        /// The number of bits associated with the data type u32. Value: 32.
        /// </summary>
        private const uint u32 = 32;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// An array containing the lists of enumerator/bitmask records associated with each enumerator/bitmask identifier.
        /// identifier.
        /// </summary>
        /// <remarks>The array index corresponds to the enumerator/bitmask identifier.</remarks>
        private List<IEnumBit>[] m_EnumBitLists;

        /// <summary>
        /// An array containing the number of bits that are used for each bitmask variable. The array index corresponds to the variable identifier.
        /// </summary>
        /// <remarks>This is only relevant to bitmask watch variables, for all other watch variable types the bit count will be 0.</remarks>
        private uint[] m_BitCounts;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public VariableTable(T variableDataTable, E enumBitDataTable)
            : base(variableDataTable)
        {
            if (enumBitDataTable == null)
            {
                return;
            }

            m_EnumBitLists = BuildEnumBitLists(enumBitDataTable);
            m_BitCounts = BuildBitCountTable(RecordList, m_EnumBitLists);
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Get the list of the flag states for the specified bitmask variable corresponding to the specified value. 
        /// Get the list of flag states associated with the specified bitmask watch variable and value.
        /// </summary>
        /// <remarks>The watch variable associated with the specified identifier must be a bitmask watch variable.</remarks>
        /// <param name="identifier">The appropriate identifier, i.e. the watch identifier or the old identifier depending upon which VariableTable is 
        /// being accessed, of the bitmask watch variable.</param>
        /// <param name="value">The value of the bitmask watch variable.</param>
        /// <returns>A list of the flag states corresponding to the specified bitmask watch variable and value if the variable exists; otherwise, an empty list.</returns>
        public List<FlagState_t> GetFlagStateList(int identifier, uint value)
        {
            List<FlagState_t> flagStateList = new List<FlagState_t>();

            try
            {
                // Skip, if the specified watch identifier doesn't correspond to a bitmask watch variable.
                if (RecordList[identifier].IsBitMask != true)
                {
                    // Return an empty list.
                    return flagStateList;
                }

                uint bitCount = GetBitCount(identifier);

                // The EnumBit entry associated with the specified watch variable.
                List<IEnumBit> enumBitList = m_EnumBitLists[RecordList[identifier].EnumBitIdentifier];

                // Find out which of the bits are set in the value.
                ulong bitmask;
                IEnumBit enumBit;
                FlagState_t flagState;
                for (byte bitIndex = 0; bitIndex < bitCount; bitIndex++)
                {
                    flagState = new FlagState_t();
                    bitmask = (ulong)0x01 << bitIndex;
                    
                    enumBit = enumBitList.Find(delegate(IEnumBit enumBitEntry) { return enumBitEntry.Value == bitmask; });
                    if (enumBit == null)
                    {
                        // Add a blank entry to the list so that the user can relate the bitmask value to the flags that are shown as asserted.
                        flagState.Bit = bitIndex;
                        flagState.Description = "-";
                        flagStateList.Add(flagState);
                        continue;
                    }

                    Debug.Assert(enumBit != null);
                    flagState.Bit = bitIndex;
                    flagState.Description = enumBit.Description;
                    
                    if (bitmask > value)
                    {
                        flagStateList.Add(flagState);
                        continue;
                    }

                    if ((value & bitmask) == bitmask)
                    {
                        flagState.State = true;
                    }

                    flagStateList.Add(flagState);
                }
            }
            catch (Exception)
            {
                // Return an empty list.
                return new List<FlagState_t>();
            }

            return flagStateList;
        }

        /// <summary>
        /// Get the list of asserted flag descriptions associated with the specified bitmask watch variable and value.
        /// </summary>
        /// <remarks>The watch variable associated with the specified identifier must be a bitmask watch variable.</remarks>
        /// <param name="identifier">The appropriate identifier, i.e. the watch identifier or the old identifier depending upon which VariableTable is 
        /// being accessed, of the bitmask watch variable.</param>
        /// <param name="value">The value of the watch variable.</param>
        /// <returns>A list of the asserted flag descriptions corresponding to the specified bitmask watch variable and value if the variable exists; 
        /// otherwise, an empty list.</returns>
        public List<string> GetAssertedFlagList(int identifier, uint value)
        {
            // A generic list of the descriptions corresponding to the asserted flags.
            List<string> assertedFlagList = new List<string>();

            try
            {
                // If: (1) the specified value is equal to 0; (2) the specified watch identifier is not of type bitmask or (3) the class has not been initialized return
                // an empty list.
                if ((value == 0) || (RecordList[identifier].IsBitMask != true))
                {
                    // Return an empty list.
                    return assertedFlagList;
                }

                uint bitCount = GetBitCount(identifier);

                // The bit mask that is to be applied to the value.
                ulong bitMask;

                // The EnumBit entry associated with the specified watch variable.
                List<IEnumBit> enumBit = m_EnumBitLists[RecordList[identifier].EnumBitIdentifier];

                // Find out which of the bits are set in the value and add the corresponding flag description to the list.
                for (int bit = 0; bit < bitCount; bit++)
                {
                    bitMask = (ulong)0x01 << bit;
                    if (bitMask > value)
                    {
                        break;
                    }

                    if ((value & bitMask) == bitMask)
                    {
                        // The current bit is set, find out if the corresponding value is contained within the current EnumBit entry.
                        string flag = enumBit.Find(delegate(IEnumBit enumBitEntry) { return enumBitEntry.Value == bitMask; }).Description;
                        if (flag != null)
                        {
                            // Add the flag description to the list.
                            assertedFlagList.Add(flag);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Return an empty list.
                return new List<string>();
            }

            return assertedFlagList;
        }

        /// <summary>
        /// Get the number of flags/bits associated with the specified bitmask variable.
        /// </summary>
        /// <remarks>The watch variable associated with the specified identifier must be a bitmask watch variable.</remarks>
        /// <param name="identifier">The appropriate identifier, i.e. the watch identifier or the old identifier depending upon which VariableTable is 
        /// being accessed, of the bitmask watch variable.</param>
        /// <returns>The number of flags/bits associated with the specified bitmask watch variable if the variable exists; otherwise, zero.</returns>
        public uint GetBitCount(int identifier)
        {
            uint bitCount;
            try
            {
                bitCount = m_BitCounts[identifier];
                return bitCount;
            }
            catch(Exception)
            {
                bitCount = 0;
            }
            return bitCount;
        }

        /// <summary>
        /// Get the flag description associated with the specified bitmask watch variable and bit reference.
        /// </summary>
        /// <remarks>The watch variable associated with the specified identifier must be a bitmask watch variable.</remarks>
        /// <param name="identifier">The appropriate identifier, i.e. the watch identifier or the old identifier depending upon which VariableTable is 
        /// being accessed, of the bitmask watch variable.</param>
        /// <param name="bit">The bit reference for the flag.</param>
        /// <returns>The flag description associated with the specified bit of the specified bitmask watch variable if the variable exists; otherwise, an empty 
        /// string.</returns>
        public string GetFlagDescription(int identifier, byte bit)
        {
            string flagDescription = string.Empty;

            // The EnumBitIdentifier corresponding to the specified watch variable.
            int enumBitIdentifier;
            try
            {
                if (RecordList[identifier].VariableType != VariableType.Bitmask)
                {
                    return string.Empty;
                }

                enumBitIdentifier = RecordList[identifier].EnumBitIdentifier;

                if (enumBitIdentifier != 0)
                {
                    ulong bitmask = (ulong)0x01 << bit;
                    flagDescription = m_EnumBitLists[enumBitIdentifier].Find(delegate(IEnumBit enumBit) { return enumBit.Value == bitmask; }).Description;

                    // If an entry was not found then return an empty string.
                    if (flagDescription == null)
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return flagDescription;
        }

        /// <summary>
        /// Get the enumerator text associated with the specified enumerator watch variable and value.
        /// </summary>
        /// <remarks>The watch variable associated with the specified identifier must be an enumerator watch variable.</remarks>
        /// <param name="identifier">The appropriate identifier, i.e. the watch identifier or the old identifier depending upon which VariableTable is 
        /// being accessed, of the enumerator watch variable.</param>
        /// <param name="value">The value of the enumerator watch variable.</param>
        /// <returns>The text associated with the specified enumerator watch variable and value if the variable exists; otherwise, an empty string.</returns>
        public string GetEnumeratorText(int identifier, uint value)
        {
            string enumeratorText = string.Empty;

            // The EnumBitIdentifier corresponding to the specified watch variable.
            int enumBitIdentifier;

            try
            {
                enumBitIdentifier = RecordList[identifier].EnumBitIdentifier;

                if (enumBitIdentifier != 0)
                {
                    enumeratorText = m_EnumBitLists[enumBitIdentifier].Find(delegate(IEnumBit enumBit) { return enumBit.Value == value; }).Description;

                    // If value was not found then return an empty string.
                    if (enumeratorText == null)
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return enumeratorText;
        }

        /// <summary>
        /// Build an array of bit count values. This is used to determine the number of bits that are used by each bitmask watch variable.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="variableList">The list of watch variable data.</param>
        /// <param name="enumBitLists">An array of enumerator/bitmask lists.</param>
        /// <returns>The bit count values corresponding to each variable identifier.</returns>
        private uint[] BuildBitCountTable(List<R> variableList, List<IEnumBit>[] enumBitLists)
        {
            uint[] bitCounts = new uint[variableList.Count];

            int enumBitIdentifier;
            for (int identifierIndex = 0; identifierIndex < variableList.Count; identifierIndex++)
            {
                if (variableList[identifierIndex] == null)
                {
                    continue;
                }

                // Include try/catch block, just in case.
                try
                {
                    if (variableList[identifierIndex].IsBitMask)
                    {
                        // Get the EnumBitIdentifier associated with the bitmask watch variable.
                        enumBitIdentifier = variableList[identifierIndex].EnumBitIdentifier;

                        // Get the maximum number of bits associated with the bitmask watch identifier.
                        uint bitCountMax;
                        DataType_e typeID = variableList[identifierIndex].DataType;
                        switch (typeID)
                        {
                            case DataType_e.u08:
                                bitCountMax = u08;
                                break;
                            case DataType_e.u16:
                            case DataType_e.i16:
                                bitCountMax = u16;
                                break;
                            case DataType_e.u32:
                            case DataType_e.i32:
                                bitCountMax = u32;
                                break;
                            default:
                                bitCountMax = 0;
                                break;
                        }

                        ulong bitmask;
                        for (int bitCount = (int)bitCountMax; bitCount > 0; bitCount--)
                        {
                            bitmask = (ulong)0x01 << (bitCount - 1);

                            if (enumBitLists[enumBitIdentifier].Find(delegate(IEnumBit enumBit) { return enumBit.Value == bitmask; }) != null)
                            {
                                bitCounts[identifierIndex] = (uint)bitCount;
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return bitCounts;
        }

        #region - [Virtual Methods] -
        /// <summary>
        /// Build an array of watch variable records. This used to access the variable information associated with each watch identifier. The logic is performed 
        /// in the child class.
        /// </summary>
        /// <param name="variableDataTable">The variable data table of the data dictionary.</param>
        /// <returns>An array of the records contained within the variable data table if the table is valid; otherwise, null.</returns>
        protected override R[] BuildDataTable(T variableDataTable)
        {
            return null;
        }

        /// <summary>
        /// Build an array of enumerator/bitmask record lists. This is used to access the enumerator bitmask records associated with each <c>ENUMBITID</c> 
        /// value. The logic is performed in the child class.
        /// </summary>
        /// <param name="enumBitDataTable">Reference to the enumerator/bitmask table of the data dictionary.</param>
        /// <returns>The array containing the lists of enumerator/bitmask records, if the parameters are valid; otherwise, null.</returns>
        protected virtual List<IEnumBit>[] BuildEnumBitLists(E enumBitDataTable)
        {
            return null;
        }
        #endregion - [Virtual Methods] -
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Get the array of the lists of enumerator/bitmask records sorted by <c>ENUMBITID</c> value.
        /// </summary>
        public List<IEnumBit>[] EnumBitLists
        {
            get { return m_EnumBitLists; }
        }
        #endregion --- Properties ---
    }
}
