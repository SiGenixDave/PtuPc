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
 *  File name:  LogTable.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/20/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/26/11    1.1     K.McD           1.  Added support for the configuration of the fault log data streams from the data dictionary.
 * 
 *  02/15/11    1.2     K.McD           1.  Removed the references to the DataStreamCount property of the Log class in the BuildDataTable() method as the 
 *                                          stream number field of each event record is now derived directly from the call to the PTULL32.GetFaultHdr() method, 
 *                                          see event.dll, version 1.12.2.
 *                                          
 *  06/22/11    1.3     K.McD           1.  Corrected a number of XML tags.
 *                                      2.  Re-ordered the member methods.
 *                                      
 *  07/24/13    1.4     K.McD           1.  Modified the BuildDataStreamTypeParametersTable() method to include the WatchVariablesMax and Name fields from the DataStreamTypes table.
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Data;

namespace Common.Configuration
{
    /// <summary>
    /// A class to help access the event log records contained within the <c>LOGS</c> table of the data dictionary. The records are accessed using 
    /// the log identifier value.
    /// </summary>
    public class LogTable : Table<Log, DataDictionary.LOGSDataTable>
    {
        #region --- Member Variables ---
        /// <summary>
        /// An array of <c>DataStreamTypeParameters_t</c> structures that define the parameters associated with each type of data stream.
        /// </summary>
        private DataStreamTypeParameters_t[] m_DataStreamTypes;

        /// <summary>
        /// Reference to the specified events data table from the data dictionary.
        /// </summary>
        private DataDictionary.LOGSDataTable m_LogsDataTable;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="dataTable">Reference to the <c>LOGS</c> table of the data dictionary.</param>
        /// <param name="dataStreamTypesDataTable">Reference to the <c>DataStreamTypes</c> table of the data dictionary i.e. the data stream parameters associated with each log.</param>
        public LogTable(DataDictionary.LOGSDataTable dataTable, DataDictionary.DataStreamTypesDataTable dataStreamTypesDataTable)
            : base(dataTable)
        {
            if (dataStreamTypesDataTable == null)
            {
                return;
            }

            m_LogsDataTable = dataTable;

            m_DataStreamTypes = BuildDataStreamTypeParametersTable(dataStreamTypesDataTable);

            // Update each log with the data stream type parameters associated with the DataStreamTypeIdentifier property/member variable.
            for (int recordIndex = 0; recordIndex < Items.Length; recordIndex++)
            {
                if (Items[recordIndex] != null)
                {
                    try
                    {
                        Items[recordIndex].DataStreamTypeParameters = m_DataStreamTypes[Items[recordIndex].DataStreamTypeIdentifier];
                    }
                    catch(Exception)
                    {
                        Items[recordIndex].DataStreamTypeParameters.SetToDefaulDataStreamType();
                    }
                }
            }

            // Convert the variable array to a generic list for convenience and manipulation.
            m_RecordList = new List<Log>();
            m_RecordList.AddRange(Items);
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Build an array of the event log records contained within the <c>LOGS</c> table of the data dictionary. This array is used to access the 
        /// parameters associated with the event logs defined in the table. The array element is mapped to the log identifier field of the table.
        /// </summary>
        /// <param name="dataTable">Reference to the <c>LOGS</c> data table of the data dictionary.</param>
        /// <returns>An array of the event log records contained within the <c>LOGS</c> table of the data dictionary, if the parameters are valid; otherwise, null.</returns>
        protected override Log[] BuildDataTable(DataDictionary.LOGSDataTable dataTable)
        {
            // Local copy of the data table.
            Log[] records;

            if (dataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the identifier field in the data table, it cannot be assumed that the table is sorted by identifier.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int recordIndex = 0; recordIndex < dataTable.Count; recordIndex++)
                {
                    iDCurrent = dataTable[recordIndex].LOGID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                m_IdentifierMax = iDMax;

                // Instantiate the lookup array.
                records = new Log[iDMax + 1];

                // Populate the lookup table;
                int identifier;
                DataDictionary.LOGSRow row;
                for (int recordIndex = 0; recordIndex < dataTable.Count; recordIndex++)
                {
                    row = dataTable[recordIndex];
                    identifier = row.LOGID;

                    // Instantiate a new structure to contain the log information and copy the data across.
                    Log record = new Log();
                    record.Identifier = (short)row.LOGID;
                    record.Description = row.DESCRIPTION;

                    // As these fields are not auto-generated, include a try catch block in case they have not been defined in the data dictionary.
                    try
                    {
                        record.DataStreamTypeIdentifier = row.DataStreamTypeIdentifier;
                    }
                    catch (StrongTypingException)
                    {
                        // Use the default values defined in the parameter class.
                        record.DataStreamTypeIdentifier = Parameter.DefaultDataStreamTypeIdentifier;
                    }

                    // Add the record to the correct element of the array.
                    records[identifier] = record;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return records;
        }

        /// <summary>
        /// Build an array of the records contained within the <c>DataStreamTypes</c> table of the data dictionary. This is used to access the data stream parameters 
        /// associated with a particular log. The array element is mapped to the data stream type identifier field of the table.
        /// </summary>
        /// <param name="dataStreamTypesDataTable">Reference to the <c>DataStreamTypes</c> table of the data dictionary.</param>
        /// <returns>An array of the records contained within the <c>DataStreamTypes</c> table of the data dictionary, if the parameters are valid; otherwise, null.</returns>
        private DataStreamTypeParameters_t[] BuildDataStreamTypeParametersTable(DataDictionary.DataStreamTypesDataTable dataStreamTypesDataTable)
        {
            // Local copy of the data table.
            DataStreamTypeParameters_t[] dataStreamTypes;

            if (dataStreamTypesDataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the identifier field in the data table, it cannot be assumed that the table is sorted by identifier.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int recordIndex = 0; recordIndex < dataStreamTypesDataTable.Count; recordIndex++)
                {
                    iDCurrent = dataStreamTypesDataTable[recordIndex].DataStreamTypeIdentifier;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                // Instantiate the lookup array.
                dataStreamTypes = new DataStreamTypeParameters_t[iDMax + 1];

                // Initialize all elements of the array to the default data stream type.
                for (int dataStreamTypeIndex = 0; dataStreamTypeIndex < dataStreamTypes.Length; dataStreamTypeIndex++)
                {
                    dataStreamTypes[dataStreamTypeIndex].SetToDefaulDataStreamType();
                }

                // Overlay any data stream type definitions that have been defined in the data dictionary.
                short identifier;
                DataDictionary.DataStreamTypesRow row;
                for (int recordIndex = 0; recordIndex < dataStreamTypesDataTable.Count; recordIndex++)
                {
                    try
                    {
                        row = dataStreamTypesDataTable[recordIndex];
                        identifier = row.DataStreamTypeIdentifier;
                        dataStreamTypes[identifier] = new DataStreamTypeParameters_t();

                        // Copy the data from the record in the data dictionary.
                        dataStreamTypes[identifier].Identifier = identifier;
                        dataStreamTypes[identifier].TripIndex = row.TripIndex;
                        dataStreamTypes[identifier].WatchVariablesMax = row.WatchVariablesMax;
                        dataStreamTypes[identifier].Name = row.DataStreamTypeName;
                    }
                    catch (StrongTypingException)
                    {
                        dataStreamTypes[recordIndex].SetToDefaulDataStreamType();
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return dataStreamTypes;
        }
        #endregion --- Methods ---
    }
}
