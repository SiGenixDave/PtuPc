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
 *  File name:  GroupListTable.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/01/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;

namespace Common.Configuration
{
    /// <summary>
    /// A class to help access the self test group records contained within the <c>GROUPLIST</c> table of the data dictionary. The records are accessed using 
    /// the group list identifier value.
    /// </summary>
    public class GroupListTable : Table<GroupListRecord, DataDictionary.GROUPLISTDataTable>
    {
        #region --- Member Variables ---
        /// <summary>
        /// An array containing the lists of those records from the <c>GROUPLISTIDS</c> table associated with each group list identifier. The array index 
        /// corresponds to the group list identifier e.g. m_GroupListIdentifierLists[3] would return a list of those records that had a value of 3 for the 
        /// <c>GROUPLISTID</c> field.
        /// </summary>
        private List<GroupListIdentifier_t>[] m_GroupListIdentifierLists;

        /// <summary>
        /// Reference to the <c>GROUPLIST</c> table of the data dictionary.
        /// </summary>
        private DataDictionary.GROUPLISTDataTable m_GroupListDataTable;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="dataTable">Reference to the <c>GROUPLIST</c> table of the data dictionary.</param>
        /// <param name="groupListIdentifiersDataTable">Reference to the <c>GROUPLISTIDS</c> table of the data dictionary. This table defines the self tests associated 
        /// with each self test group list.</param>
        public GroupListTable(DataDictionary.GROUPLISTDataTable dataTable, DataDictionary.GROUPLISTIDSDataTable groupListIdentifiersDataTable)
            : base(dataTable)
        {
            if (groupListIdentifiersDataTable == null)
            {
                return;
            }

            m_GroupListDataTable = dataTable;

            m_GroupListIdentifierLists = BuildGroupListIdentifierLists(groupListIdentifiersDataTable);

            AddSelfTestRecordLists();
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Build an array of the records contained within the <c>GROUPLIST</c> table of the data dictionary. This array is used to access the 
        /// parameters associated with the self test groups defined in the table. The array element is mapped to the group list identifier field of the table.
        /// </summary>
        /// <param name="dataTable">Reference to the <c>GROUPLIST</c> table of the data dictionary.</param>
        /// <returns>An array of the records contained within the <c>GROUPLIST</c> table of the data dictionary, if the parameters are valid; otherwise, null.</returns>
        protected override GroupListRecord[] BuildDataTable(DataDictionary.GROUPLISTDataTable dataTable)
        {
            // Local copy of the data table.
            GroupListRecord[] records;

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
                    iDCurrent = dataTable[recordIndex].GROUPLISTID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                m_IdentifierMax = iDMax;

                // Instantiate the lookup array.
                records = new GroupListRecord[iDMax + 1];

                // Populate the lookup table;
                int identifier;
                DataDictionary.GROUPLISTRow row;
                for (int recordIndex = 0; recordIndex < dataTable.Count; recordIndex++)
                {
                    row = dataTable[recordIndex];
                    identifier = row.GROUPLISTID;

                    // Instantiate a new structure to contain the log information and copy the data across.
                    GroupListRecord record = new GroupListRecord();
                    record.Identifier = (short)row.GROUPLISTID;
                    record.Description = row.DESCRIPTION;
                    record.HelpIndex = row.HELPINDEX;
                    record.Attribute = row.ATTRIBUTE;

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
        /// Add the self tests associated with each group to the <c>SelfTestRecordList</c> property.
        /// </summary>
        /// <remarks>
        /// Externals
        /// <list type="table">
        /// <listheader><term>Member Variable/Property</term><description>Description</description></listheader>
        /// <item><term>Items</term><description>Gets an indexed array of the records contained in the <c>GROUPLIST</c> table.</description></item>
        /// <item><term>m_GroupListIdentifierLists</term><description>An array containing the lists of those records from the <c>GROUPLISTIDS</c> table associated with
        /// each group list identifier. The array index corresponds to the group list identifier e.g. m_GroupListIdentifierLists[3] would return a list of those records 
        /// that had a value of 3 for the <c>GROUPLISTID</c> field.</description></item>
        /// </list>
        /// </remarks>
        private void AddSelfTestRecordLists()
        {
            List<GroupListIdentifier_t> groupListIdentifierList;
            for (short groupListIdentifier = 0; groupListIdentifier < m_IdentifierMax + 1; groupListIdentifier++)
            {
                // Include a try/catch block in case not all of the primary key identifiers are defined.
                try
                {
                    // Instantiate an empty list.
                    Items[groupListIdentifier].SelfTestRecordList = new List<SelfTestRecord>();

                    try
                    {
                        groupListIdentifierList = m_GroupListIdentifierLists[groupListIdentifier];

                        // Add the self Tests corresponding to the group list identifier contained within the list.
                        for (int index = 0; index < groupListIdentifierList.Count; index++)
                        {
                            Items[groupListIdentifier].SelfTestRecordList.Add(Lookup.SelfTestTableBySelfTestNumber.Items[groupListIdentifierList[index].SelfTestIdentifier]);
                        }
                    }
                    catch (NullReferenceException)
                    {
                        continue;
                    }
                    catch (IndexOutOfRangeException)
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
        /// Build an array of lists of <c>GroupListIdentifier_t</c> records associated with each unique <c>GROUPLISTID</c> value defined in the <c>GROUPLISTIDS</c> 
        /// table of the data dictionary. The array element is mapped to the <c>GROUPLISTID</c> field of the table.
        /// </summary>
        /// <param name="groupListIdentifiersDataTable">Reference to the <c>GROUPLISTIDS</c> table of the data dictionary.</param>
        /// <returns>An array of lists of <c>GroupListIdentifier_t</c> records associated with each unique <c>GROUPLISTID</c> value in the <c>GROUPLISTIDS</c> table 
        /// of the data dictionary, if the parameters are valid; otherwise, null.</returns>
        private List<GroupListIdentifier_t>[] BuildGroupListIdentifierLists(DataDictionary.GROUPLISTIDSDataTable groupListIdentifiersDataTable)
        {
            // Local copy of the data table.
            List<GroupListIdentifier_t>[] records;

            if (groupListIdentifiersDataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the identifier field in the data table, it cannot be assumed that the table is sorted by identifier.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int recordIndex = 0; recordIndex < groupListIdentifiersDataTable.Count; recordIndex++)
                {
                    iDCurrent = groupListIdentifiersDataTable[recordIndex].GROUPLISTID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                // Instantiate the lookup array.
                records = new List<GroupListIdentifier_t>[iDMax + 1];

                // Instantiate a generic list for each element of the array. 
                for (int recordIndex = 0; recordIndex < iDMax + 1; recordIndex++)
                {
                    records[recordIndex] = new List<GroupListIdentifier_t>();
                }

                // Populate the lookup table;
                int identifier;
                DataDictionary.GROUPLISTIDSRow row;
                for (int recordIndex = 0; recordIndex < groupListIdentifiersDataTable.Count; recordIndex++)
                {
                    row = groupListIdentifiersDataTable[recordIndex];
                    identifier = row.GROUPLISTID;

                    // Instantiate a new structure to contain the data and copy the data across.
                    GroupListIdentifier_t record = new GroupListIdentifier_t();
                    record.GroupListIdentifier = row.GROUPLISTID;
                    record.SelfTestIdentifier = row.SELFTESTID;

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
