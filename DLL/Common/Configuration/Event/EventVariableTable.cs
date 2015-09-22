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
 *  File name:  EventVariableTable.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/10/15    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/22/11    1.1     K.McD           1.  Corrected a number of XML tags.
 *  
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;

namespace Common.Configuration
{
    /// <summary>
    /// A class to help access the event variables defined in the <c>EVENTVARIABLES</c> table of the data dictionary. The event variables are accessed using 
    /// the event variable identifier value.
    /// </summary>
    public class EventVariableTable : VariableTable<EventVariable, DataDictionary.EVENTVARIABLESDataTable, DataDictionary.EVENTENUMBITDataTable>
    {
        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="eventVariableDataTable">Reference to the <c>EVENTVARIABLES</c> table of the data dictionary.</param>
        /// <param name="enumBitDataTable">Reference to the <c>EVENTENUMBIT</c> table of the data dictionary i.e. the enumerator/bitmask data table associated with 
        /// event variables.</param>
        public EventVariableTable(DataDictionary.EVENTVARIABLESDataTable eventVariableDataTable, DataDictionary.EVENTENUMBITDataTable enumBitDataTable)
            : base(eventVariableDataTable, enumBitDataTable)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Build an array of the event variable records contained within the <c>EVENTVARIABLES</c> table of the data dictionary. This array is used to access the 
        /// parameters associated with the event variables defined in the table. The array element is mapped to the event variable identifier field of the table.
        /// </summary>
        /// <param name="variableDataTable">Reference to the <c>EVENTVARIABLES</c> table of the data dictionary.</param>
        /// <returns>An array of the records contained within the <c>EVENTVARIABLES</c> table of the data dictionary, if the specified table is valid; otherwise, null.</returns>
        protected override EventVariable[] BuildDataTable(DataDictionary.EVENTVARIABLESDataTable variableDataTable)
        {
            // Local copy of the table.
            EventVariable[] variables;

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
                    iDCurrent = variableDataTable[record].EVENTVARID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                m_IdentifierMax = iDMax;

                // Instantiate the lookup array.
                variables = new EventVariable[iDMax + 1];

                // Populate the lookup table;
                int identifier;
                DataDictionary.EVENTVARIABLESRow row;
                for (int record = 0; record < variableDataTable.Count; record++)
                {
                    row = variableDataTable[record];
                    identifier = row.EVENTVARID;

                    // Instantiate a new structure to contain the variable data and copy the data across.
                    EventVariable eventVariable = new EventVariable();
                    eventVariable.Identifier = row.EVENTVARID;
                    eventVariable.Name = row.DESCRIPTION;
                    eventVariable.DataType = (DataType_e)row.TYPEID;
                    eventVariable.ScaleFactor = row.SCALEFACTOR;
                    eventVariable.EnumBitIdentifier = row.ENUMBITID;
                    eventVariable.IsBitMask = (row.BITMASK == 0) ? true : false;
                    eventVariable.Units = row.UNITS;
                    eventVariable.FormatString = row.FORMATSTRING;
                    eventVariable.HelpIndex = row.HELPINDEX;

                    eventVariable.ConversionFactor = row.CONVERSIONFACTOR;

                    // Determine the variable type.
                    if (eventVariable.IsBitMask)
                    {
                        eventVariable.VariableType = VariableType.Bitmask;
                    }
                    else
                    {
                        if (eventVariable.EnumBitIdentifier == 0)
                        {
                            eventVariable.VariableType = VariableType.Scalar;
                        }
                        else
                        {
                            eventVariable.VariableType = VariableType.Enumerator;
                        }
                    }

                    // Add the structure to the correct element of the array.
                    variables[identifier] = eventVariable;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return variables;
        }

        /// <summary>
        /// Build an array of lists of enumerator/bitmask records associated with each unique <c>ENUMBITID</c> value defined in the <c>EVENTENUMBIT</c> table of 
        /// the data dictionary. The array element is mapped to the <c>ENUMBITID</c> field of the table.
        /// </summary>
        /// <param name="enumBitDataTable">Reference to the enumerator/bitmask table of the data dictionary.</param>
        /// <returns>The array containing the lists of enumerator/bitmask records, if the parameters are valid; otherwise, null.</returns>
        protected override List<IEnumBit>[] BuildEnumBitLists(DataDictionary.EVENTENUMBITDataTable enumBitDataTable)
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
                DataDictionary.EVENTENUMBITRow row;
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
