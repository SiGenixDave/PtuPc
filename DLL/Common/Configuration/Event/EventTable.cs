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
 *  File name:  EventTable.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/04/10    1.1     K.McD           1.  Added the GetEventVariableList() method.
 * 
 *  01/26/11    1.2     K.McD           1.  Modified a number of XML tags and the names of a number of variables.
 * 
 *  02/15/11    1.3     K.McD           1.  Modified the GetIdentifier() method such that an exception isn't thrown if an event corresponding to 
 *                                          the specified LOGID, TASKID and EVENTID does not exist.
 * 
 *  03/21/11    1.3     K.McD           1.  Renamed a number of constants and methods and modified a number of XML tags.
 * 
 *                                      2.  Added the CommonEventVariableCount property. This gives the number of common event variables per event associated with 
 *                                          the current data dictionary.
 * 
 *                                      3.  Modified the CreateEventVariableList() to create a list of newly instantiated event variables to store the event variable 
 *                                          values associated with the the specified event identifier.
 *                                          
 *  06/22/11    1.4     K.McD           1.  Corrected a number of XML tags.
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Common.Configuration
{
    /// <summary>
    /// A class to help access the system events defined in the <c>EVENTS</c> table of the data dictionary. The system events are accessed using 
    /// the identifier value.
    /// </summary>
    public class EventTable : Table<EventRecord, DataDictionary.EVENTSDataTable>
    {
        #region --- Constants ---
        /// <summary>
        /// The key associated with the log index field of the <c>Events</c> data table. Value: "LOGID".
        /// </summary>
        /// <remarks>Although the field name in the <c>Events</c> table of the data dictionary is given as <c>LOGID</c> the values represent the log index, not the 
        /// log identifier. The log index is equivalent to the log identifier -1.</remarks>
        private const string KeyLogIndexField = "LOGID";

        /// <summary>
        /// The key associated with the task identifier field of the <c>Events</c> data table. Value: "TASKID".
        /// </summary>
        private const string KeyTaskIdentifierField = "TASKID";

        /// <summary>
        /// The key associated with the event identifier field of the <c>Events</c> data table. Value: "EVENTID".
        /// </summary>
        private const string KeyEventIdentifierField = "EVENTID";

        /// <summary>
        /// The text associated with the AND logic function in a <c>FilterExpression</c>. Value: " AND ";
        /// </summary>
        private const string And = " AND ";

        /// <summary>
        /// The text associated with the = logic function in a <c>FilterExpression</c>. Value: " = ";
        /// </summary>
        private new const string Equals = " = ";

        /// <summary>
        /// The number of header event variables associated with the project. These are the first 'HeaderEventVariableCount' event variables defined in the EVENTVARIABLES 
        /// table of the data dictionary. Value: 5.
        /// </summary>
        /// <remarks>The value is fixed for all data dictionaries.</remarks>
        public const int HeaderEventVariableCount = 5;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// An array containing the lists of those records from the <c>STRUCT</c> table associated with each structure identifier. The array index corresponds to the 
        /// structure identifier e.g. m_StructLists[27] would return a list of those records that had a value of 27 for <c>STRUCTID</c> field.
        /// </summary>
        private List<Struct_t>[] m_StructLists;

        /// <summary>
        /// Reference to the <c>EVENTS</c> table of the data dictionary.
        /// </summary>
        private DataDictionary.EVENTSDataTable m_EventsDataTable;

        /// <summary>
        /// The number of common event variables associated with each event. These are the event variables contained within the STRUCT generic list corresponding to 
        /// structure identifier 0, less those event variables defined as header event variables.  
        /// </summary>
        private int m_CommonEventVariableCount;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="dataTable">Reference to the <c>EVENTS</c> table of the data dictionary. This table contains the VCU system event definitions.</param>
        /// <param name="structDataTable">Reference to the <c>STRUCT</c> table of the data dictionary. This table defines which event variables are associated 
        /// with each VCU system event.</param>
        public EventTable(DataDictionary.EVENTSDataTable dataTable, DataDictionary.STRUCTDataTable structDataTable)
            : base(dataTable)
        {
            if (structDataTable == null)
            {
                return;
            }

            m_EventsDataTable = dataTable;

            m_StructLists = BuildStructLists(structDataTable);

            AddEventVariableLists();

            m_CommonEventVariableCount = m_StructLists[0].Count - HeaderEventVariableCount;
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Get the identifier field of the record in the <c>Events</c> data table containing values in the <c>LogID</c>, <c>TaskId</c> and <c>EventId</c> fields that
        /// match the specified parameters.
        /// </summary>
        /// <param name="logIndex">The log index, this is equivalent to the log identifier - 1.</param>
        /// <param name="taskIdentifier">The task identifier.</param>
        /// <param name="eventIdentifier">The event identifier.</param>
        /// <remarks>Although the field name in the <c>Events</c> table of the data dictionary is given as <c>LOGID</c> the values are actually the values 
        /// of the event log index, this is equivalent to the actual log identifier -1.</remarks>
        /// <returns>The identifier of the record that matches the specified search criteria, if found; otherwise returns <c>NotFound</c>.</returns>
        public short GetIdentifier(short logIndex, short taskIdentifier, short eventIdentifier)
        {
            string filterExpression = KeyLogIndexField + Equals + logIndex.ToString() + And;
            filterExpression += KeyTaskIdentifierField + Equals + taskIdentifier.ToString() + And;
            filterExpression += KeyEventIdentifierField + Equals + eventIdentifier.ToString();

            DataDictionary.EVENTSRow[] dataRowArray = new DataDictionary.EVENTSRow[1];
            short identifier;
            try
            {
                dataRowArray = (DataDictionary.EVENTSRow[])m_EventsDataTable.Select(filterExpression);
                identifier = dataRowArray[0].ID;
            }
            catch (Exception)
            {
                return CommonConstants.NotFound;
            }

            return identifier;
        }

        /// <summary>
        /// Create a new list to store the event variable values associated with an event of the specified type.
        /// </summary>
        /// <param name="identifier">The identifier associated with the event.</param>
        /// <returns>The list of event variables specific to the event with the specified identifier.</returns>
        public List<EventVariable> CreateEventVariableList(short identifier)
        {
            Debug.Assert(identifier >= 0, "EventTable.GetEventVariableList");

            int structIdentifier;
            List<Struct_t> structList;
 
            List<EventVariable> eventVariableList = new List<EventVariable>();
            try
            {
                // ----------------------------------------------------------------------------------------------------------------------------------------------
                // Get the event variables that are collected for each event i.e. those defined with the structure identifier 0 less those defined as event header
                // variables.
                // ----------------------------------------------------------------------------------------------------------------------------------------------
                structList = m_StructLists[0];

                // Add the event variables that are common to all events. These are the event variables contained within the STRUCT generic list corresponding to 
                // structure identifier 0, less those event variables defined as header event variables. 
                for (int index = HeaderEventVariableCount; index < structList.Count; index++)
                {
                    // Ensure that a new instance of the event variable is created.
                    EventVariable eventVariable = new EventVariable();
                    Lookup.EventVariableTable.Items[structList[index].EventVariableIdentifier].CopyTo(ref eventVariable);
                    eventVariableList.Add(eventVariable);
                }

                structIdentifier = Items[identifier].StructureIdentifier;
                structList = m_StructLists[structIdentifier];

                // Add the event variables corresponding to the event variable identifier contained within the list.
                for (int index = 0; index < structList.Count; index++)
                {
                    EventVariable eventVariable = new EventVariable();
                    Lookup.EventVariableTable.Items[structList[index].EventVariableIdentifier].CopyTo(ref eventVariable);
                    eventVariableList.Add(eventVariable);
                }
            }
            catch (Exception)
            {
                // Ensure that an exception isn't thrown.
            }

            return eventVariableList;
        }

        /// <summary>
        /// Build an array of the records contained within the <c>EVENTS</c> table of the data dictionary. This array is used to access the 
        /// parameters associated with the system events defined in the table. The array element is mapped to the identifier field of the table.
        /// </summary>
        /// <param name="dataTable">Reference to the <c>EVENTS</c> table of the data dictionary.</param>
        /// <returns>An array of the records contained within the <c>EVENTS</c> data table, if the parameters are valid; otherwise, null.</returns>
        protected override EventRecord[] BuildDataTable(DataDictionary.EVENTSDataTable dataTable)
        {
            // Local copy of the data table.
            EventRecord[] records;

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
                    iDCurrent = dataTable[recordIndex].ID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                m_IdentifierMax = iDMax;

                // Instantiate the lookup array.
                records = new EventRecord[iDMax + 1];

                // Populate the lookup table;
                int identifier;
                DataDictionary.EVENTSRow row;
                for (int recordIndex = 0; recordIndex < dataTable.Count; recordIndex++)
                {
                    row = dataTable[recordIndex];
                    identifier = row.ID;

                    // Instantiate a new structure to contain the variable data and copy the data across.
                    EventRecord record = new EventRecord();
                    record.Identifier = row.ID;
                    record.EventIdentifier = row.EVENTID;
                    record.Description = row.DESCRIPTION;
                    record.TaskIdentifier = row.TASKID;
                    record.HelpIndex = row.HELPINDEX;
                    record.LogIdentifier = row.LOGID;
                    record.StructureIdentifier = row.STRUCTID;

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
        /// Add the event variables associated with each event to the <c>EventVariableList</c> property.
        /// </summary>
        /// <remarks>
        /// Externals
        /// <list type="table">
        /// <listheader><term>Member Variable/Property</term><description>Description</description></listheader>
        /// <item><term>Items</term><description>Gets an indexed array of the records contained in the <c>DataDictionary.EVENTSDataTable</c> table.</description></item>
        /// <item><term>m_StructLists</term><description>An array containing the lists of those records from the <c>Struct</c> data table associated with each structure identifier. The array index corresponds to the structure identifier 
        /// e.g. m_StructureList[27] would return a list of those records that had a value of 27 for <c>StructId</c> field.</description></item>
        /// <item><term></term><description></description></item>
        /// </list>
        /// </remarks>
        private void AddEventVariableLists()
        {
            int structIdentifier;
            List<Struct_t> structList;
            for (short eventIdentifier = 0; eventIdentifier < m_IdentifierMax + 1; eventIdentifier++)
            {
                // Include a try/catch block in case not all of the primary key identifiers are defined.
                try
                {
                    // Instantiate an empty list.
                    Items[eventIdentifier].EventVariableList = new List<EventVariable>();

                    try
                    {
                        structIdentifier = Items[eventIdentifier].StructureIdentifier;
                        structList = m_StructLists[structIdentifier];

                        // Add the event variables corresponding to the event variable identifier contained within the list.
                        for (int index = 0; index < structList.Count; index++)
                        {
                            Items[eventIdentifier].EventVariableList.Add(Lookup.EventVariableTable.Items[structList[index].EventVariableIdentifier]);
                        }
                    }
                    catch (NullReferenceException)
                    {
                        continue;
                    }
                }
                catch (NullReferenceException)
                {
                    // Primary key identifier is not defined.
                    continue;
                }
            }
        }

        /// <summary>
        /// Build an array of lists of <c>Struct_t</c> records associated with each unique <c>STRUCTID</c> value defined in the <c>STRUCT</c> table of 
        /// the data dictionary. The array element is mapped to the <c>STRUCTID</c> field of the table.
        /// </summary>
        /// <param name="structDataTable">Reference to the <c>STRUCT</c> table of the data dictionary.</param>
        /// <returns>An array of lists of <c>Struct_t</c> records associated with each unique <c>STRUCTID</c> value in the <c>STRUCT</c> table of the 
        /// data dictionary, if the parameters are valid; otherwise, null.</returns>
        private List<Struct_t>[] BuildStructLists(DataDictionary.STRUCTDataTable structDataTable)
        {
            // Local copy of the data table.
            List<Struct_t>[] records;

            if (structDataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the identifier field in the data table, it cannot be assumed that the table is sorted by identifier.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int recordIndex = 0; recordIndex < structDataTable.Count; recordIndex++)
                {
                    iDCurrent = structDataTable[recordIndex].STRUCTID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                // Instantiate the lookup array.
                records = new List<Struct_t>[iDMax + 1];

                // Instantiate a generic list for each 
                for (int recordIndex = 0; recordIndex < iDMax + 1; recordIndex++)
                {
                    records[recordIndex] = new List<Struct_t>();
                }

                // Populate the lookup table;
                int identifier;
                DataDictionary.STRUCTRow row;
                for (int recordIndex = 0; recordIndex < structDataTable.Count; recordIndex++)
                {
                    row = structDataTable[recordIndex];
                    identifier = row.STRUCTID;

                    // Instantiate a new structure to contain the data and copy the data across.
                    Struct_t record = new Struct_t();
                    record.StructureIdentifier = row.STRUCTID;
                    record.EventVariableIdentifier = row.EVENTVARID;

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

        #region --- Properties ---
        /// <summary>
        /// Gets the number of common event variables associated with each event. These are the event variables contained within the STRUCT generic list corresponding to 
        /// structure identifier 0, less those event variables defined as header event variables.  
        /// </summary>
        public int CommonEventVariableCount
        {
            get { return m_CommonEventVariableCount; }
        }
        #endregion --- Properties ---
    }
}
