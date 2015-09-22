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
 *  File name:  WatchVariableTable.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/10/15    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/22/11    1.1     K.McD           1.  Modified a number of XML tags and comments.
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;

namespace Common.Configuration
{
    /// <summary>
    /// A class to help access the watch variables defined in the <c>WATCHVARIABLES</c> table of the data dictionary. The watch variables are accessed using 
    /// the watch identifier value.
    /// </summary>
    public class WatchVariableTable : VariableTable<WatchVariable, DataDictionary.WATCHVARIABLESDataTable, DataDictionary.WATCHENUMBITDataTable>
    {
        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="watchVariableDataTable">Reference to the <c>WATCHVARIABLES</c> table of the data dictionary.</param>
        /// <param name="enumBitDataTable">Reference to the <c>WATCHENUMBIT</c> table of the data dictionary i.e. the enumerator/bitmask data table associated with 
        /// watch variables.</param>
        public WatchVariableTable(DataDictionary.WATCHVARIABLESDataTable watchVariableDataTable, DataDictionary.WATCHENUMBITDataTable enumBitDataTable) 
            : base(watchVariableDataTable, enumBitDataTable)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Build an array of the watch variable records contained within the <c>WATCHVARIABLES</c> table of the data dictionary. This array is used to access the 
        /// parameters associated with the watch variables defined in the table. The array element is mapped to the watch identifier field of the table.
        /// </summary>
        /// <param name="variableDataTable">Reference to the <c>WATCHVARIABLES</c> table of the data dictionary.</param>
        /// <returns>An array of the records contained within the <c>WATCHVARIABLES</c> table of the data dictionary, if the specified table is valid; otherwise, null.</returns>
        protected override WatchVariable[] BuildDataTable(DataDictionary.WATCHVARIABLESDataTable variableDataTable)
        {
            // Local copy of the table.
            WatchVariable[] variables;

            if (variableDataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the identifier field in the WatchVariables table, it cannot be assumed that the table is sorted by WatchID value.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int record = 0; record < variableDataTable.Count; record++)
                {
                    iDCurrent = variableDataTable[record].WATCHID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                m_IdentifierMax = iDMax;

                // Instantiate the lookup array.
                variables = new WatchVariable[iDMax + 1];

                // Populate the lookup table;
                int identifier;
                DataDictionary.WATCHVARIABLESRow row;
                for (int record = 0; record < variableDataTable.Count; record++)
                {
                    row = variableDataTable[record];
                    identifier = row.WATCHID;

                    // Instantiate a new structure to contain the variable data and copy the data across.
                    WatchVariable watchVariable = new WatchVariable() as WatchVariable;
                    watchVariable.Identifier = row.WATCHID;
                    watchVariable.OldIdentifier = row.OLDID;
                    watchVariable.Name = row.VARIABLENAME;
                    watchVariable.FormatString = row.FORMATSTRING;
                    watchVariable.EmbeddedName = row.EMBEDDEDVARNAME;
                    watchVariable.DataType = (DataType_e)row.TYPEID;
                    watchVariable.ScaleFactor = row.SCALEFACTOR;
                    watchVariable.EnumBitIdentifier = row.ENUMBITID;
                    watchVariable.IsBitMask = (row.BITMASK == 0) ? true : false;
                    watchVariable.Units = row.UNITS;
                    watchVariable.MinChartScale = row.MINCHARTSCALE;
                    watchVariable.MaxChartScale = row.MAXCHARTSCALE;
                    watchVariable.AttributeFlags = row.FLAGS;
                    watchVariable.HelpIndex = row.HELPINDEX;
                    watchVariable.MinModifyValue = row.MINMODIFYVALUE;
                    watchVariable.MaxModifyValue = row.MAXMODIFYVALUE;

                    // Determine the variable type.
                    if (watchVariable.IsBitMask)
                    {
                        watchVariable.VariableType = VariableType.Bitmask;
                    }
                    else
                    {
                        if (watchVariable.EnumBitIdentifier == 0)
                        {
                            watchVariable.VariableType = VariableType.Scalar;
                        }
                        else
                        {
                            watchVariable.VariableType = VariableType.Enumerator;
                        }
                    }

                    // Add the structure to the correct element of the array.
                    variables[identifier] = watchVariable;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return variables;
        }

        /// <summary>
        /// Build an array of lists of enumerator/bitmask records associated with each unique <c>ENUMBITID</c> value defined in the <c>WATCHENUMBIT</c> table of 
        /// the data dictionary. The array element is mapped to the <c>ENUMBITID</c> field of the table.
        /// </summary>
        /// <param name="enumBitDataTable">Reference to the <c>WATCHENUMBIT</c> table of the data dictionary.</param>
        /// <returns>An array of lists of enumerator/bitmask records associated with each unique <c>ENUMBITID</c> value of the <c>WATCHENUMBIT</c> table of the 
        /// data dictionary, if the parameters are valid; otherwise, null.</returns>
        protected override List<IEnumBit>[] BuildEnumBitLists(DataDictionary.WATCHENUMBITDataTable enumBitDataTable)
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
                DataDictionary.WATCHENUMBITRow row;
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
