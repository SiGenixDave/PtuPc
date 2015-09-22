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
 *  File name:  SelfTestVariableTable.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  10/10/15    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;

namespace Common.Configuration
{
    /// <summary>
    /// A class to help access the self test variables defined in the <c>SELFTESTVARIABLES</c> table of the data dictionary. The self test variables are accessed using 
    /// the self test variable identifier value.
    /// </summary>
    public class SelfTestVariableTable : VariableTable<SelfTestVariable, DataDictionary.SELFTESTVARIABLESDataTable, DataDictionary.SELFTESTENUMBITDataTable>
    {
        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="selfTestVariableDataTable">Reference to the <c>SELFTESTVARIABLES</c> table of the data dictionary.</param>
        /// <param name="enumBitDataTable">Reference to the <c>SELFTESTENUMBIT</c> table of the data dictionary i.e. the enumerator/bitmask data table associated with 
        /// self test variables.</param>
        public SelfTestVariableTable(DataDictionary.SELFTESTVARIABLESDataTable selfTestVariableDataTable, DataDictionary.SELFTESTENUMBITDataTable enumBitDataTable)
            : base(selfTestVariableDataTable, enumBitDataTable)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Build an array of the self test variable records contained within the <c>SELFTESTVARIABLES</c> table of the data dictionary. This array is used to access the 
        /// parameters associated with the self test variables defined in the table. The array element is mapped to the self test variable identifier field of the table.
        /// </summary>
        /// <param name="variableDataTable">Reference to the <c>SELFTESTVARIABLES</c> table of the data dictionary.</param>
        /// <returns>An array of the records contained within the <c>SELFTESTVARIABLES</c> table of the data dictionary, if the specified table is valid; otherwise, null.</returns>
        protected override SelfTestVariable[] BuildDataTable(DataDictionary.SELFTESTVARIABLESDataTable variableDataTable)
        {
            // Local copy of the table.
            SelfTestVariable[] variables;

            if (variableDataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the identifier field in the data table, it cannot be assumed that the table is sorted by identifier value.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int record = 0; record < variableDataTable.Count; record++)
                {
                    iDCurrent = variableDataTable[record].SELFTESTVARID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                m_IdentifierMax = iDMax;

                // Instantiate the lookup array.
                variables = new SelfTestVariable[iDMax + 1];

                // Populate the lookup table;
                int identifier;
                DataDictionary.SELFTESTVARIABLESRow row;
                for (int record = 0; record < variableDataTable.Count; record++)
                {
                    row = variableDataTable[record];
                    identifier = row.SELFTESTVARID;

                    // Instantiate a new structure to contain the variable data and copy the data across.
                    SelfTestVariable selfTestVariable = new SelfTestVariable();
                    selfTestVariable.Identifier = (short)row.SELFTESTVARID;
                    selfTestVariable.Name = row.VARNAME;
                    selfTestVariable.DataType = (DataType_e)row.TYPEID;
                    selfTestVariable.ScaleFactor = row.SCALEFACTOR;
                    selfTestVariable.EnumBitIdentifier = row.ENUMBITID;
                    selfTestVariable.IsBitMask = (row.BITMASK == 0) ? true : false;
                    selfTestVariable.Units = row.UNITS;
                    selfTestVariable.FormatString = row.FORMATSTRING;
                    selfTestVariable.HelpIndex = row.HELPINDEX;

                    selfTestVariable.EmbeddedName = row.EMBEDDEDVARNAME;

                    // Determine the variable type.
                    if (selfTestVariable.IsBitMask)
                    {
                        selfTestVariable.VariableType = VariableType.Bitmask;
                    }
                    else
                    {
                        if (selfTestVariable.EnumBitIdentifier == 0)
                        {
                            selfTestVariable.VariableType = VariableType.Scalar;
                        }
                        else
                        {
                            selfTestVariable.VariableType = VariableType.Enumerator;
                        }
                    }

                    // Add the structure to the correct element of the array.
                    variables[identifier] = selfTestVariable;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return variables;
        }

        /// <summary>
        /// Build an array of lists of enumerator/bitmask records associated with each unique <c>ENUMBITID</c> value defined in the <c>SELFTESTENUMBIT</c> table of 
        /// the data dictionary. The array element is mapped to the <c>ENUMBITID</c> field of the table.
        /// </summary>
        /// <param name="enumBitDataTable">Reference to the <c>SELFTESTENUMBIT</c> table of the data dictionary.</param>
        /// <returns>An array of lists of enumerator/bitmask records associated with each unique <c>ENUMBITID</c> value of the <c>SELFTESTENUMBIT</c> table of the 
        /// data dictionary, if the parameters are valid; otherwise, null.</returns>
        protected override List<IEnumBit>[] BuildEnumBitLists(DataDictionary.SELFTESTENUMBITDataTable enumBitDataTable)
        {
            // Local copy of the data table.
            List<IEnumBit>[] records;

            if (enumBitDataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the identifier field in the data table, it cannot be assumed that the table is sorted by identifier.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int recordIndex = 0; recordIndex < enumBitDataTable.Count; recordIndex++)
                {
                    iDCurrent = enumBitDataTable[recordIndex].ENUMBITID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                // Instantiate the lookup array.
                records = new List<IEnumBit>[iDMax + 1];

                // Instantiate a generic list for each array element.
                for (int recordIndex = 0; recordIndex < iDMax + 1; recordIndex++)
                {
                    records[recordIndex] = new List<IEnumBit>();
                }

                // Populate the lookup table;
                int identifier;
                DataDictionary.SELFTESTENUMBITRow row;
                for (int recordIndex = 0; recordIndex < enumBitDataTable.Count; recordIndex++)
                {
                    row = enumBitDataTable[recordIndex];
                    identifier = row.ENUMBITID;

                    // Instantiate a new structure to contain the variable data and copy the data across.
                    IEnumBit record = new EnumBit_t();
                    record.EnumBitIdentifier = row.ENUMBITID;
                    record.Value = row.VALUE;
                    record.Description = row.DESCRIPTION;

                    // Add the record to the correct element of the array.
                    records[identifier].Add(record);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return records;
        }
        #endregion --- Methods ---
    }
}