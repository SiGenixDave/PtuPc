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
 *  File name:  TestListTable.cs
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
using System.Data;

namespace Common.Configuration
{
    /// <summary>
    /// A class to help access the self test group records contained within the <c>TESTLIST</c> table of the data dictionary. The records are accessed using 
    /// the test list identifier value.
    /// </summary>
    public class TestListTable : Table<TestListRecord, DataDictionary.TESTLISTDataTable>
    {
        #region --- Member Variables ---
        /// <summary>
        /// An array containing the lists of those records from the <c>TESTLISTIDS</c> table associated with each test list identifier. The array index 
        /// corresponds to the test list identifier e.g. m_TestListIdentifierLists[3] would return a list of those records that had a value of 3 for the 
        /// <c>TESTLISTID</c> field.
        /// </summary>
        private List<TestListIdentifier_t>[] m_TestListIdentifierLists;

        /// <summary>
        /// Reference to the <c>TESTLIST</c> table of the data dictionary.
        /// </summary>
        private DataDictionary.TESTLISTDataTable m_TestListDataTable;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="dataTable">Reference to the <c>TESTLIST</c> table of the data dictionary.</param>
        /// <param name="testListIdentifiersDataTable">Reference to the <c>TESTLISTIDS</c> table of the data dictionary. This table defines the self tests associated 
        /// with each test list.</param>
        public TestListTable(DataDictionary.TESTLISTDataTable dataTable, DataDictionary.TESTLISTIDSDataTable testListIdentifiersDataTable)
            : base(dataTable)
        {
            if (testListIdentifiersDataTable == null)
            {
                return;
            }

            m_TestListDataTable = dataTable;

            m_TestListIdentifierLists = BuildTestListIdentifierLists(testListIdentifiersDataTable);

            AddSelfTestRecordLists();
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Build an array of the records contained within the <c>TESTLIST</c> table of the data dictionary. This array is used to access the 
        /// parameters associated with the test lists defined in the table. The array element is mapped to the test list identifier field of the table.
        /// </summary>
        /// <param name="dataTable">Reference to the <c>TESTLIST</c> table of the data dictionary.</param>
        /// <returns>An array of the records contained within the <c>TESTLIST</c> table of the data dictionary, if the parameters are valid; otherwise, null.</returns>
        protected override TestListRecord[] BuildDataTable(DataDictionary.TESTLISTDataTable dataTable)
        {
            // Local copy of the data table.
            TestListRecord[] records;

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
                    iDCurrent = dataTable[recordIndex].TESTLISTID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                m_IdentifierMax = iDMax;

                // Instantiate the lookup array.
                records = new TestListRecord[iDMax + 1];

                // Populate the lookup table;
                int identifier;
                DataDictionary.TESTLISTRow row;
                for (int recordIndex = 0; recordIndex < dataTable.Count; recordIndex++)
                {
                    row = dataTable[recordIndex];
                    identifier = row.TESTLISTID;

                    // Instantiate a new structure to contain the log information and copy the data across.
                    TestListRecord record = new TestListRecord();
                    record.Identifier = (short)row.TESTLISTID;
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
        /// Add the self tests associated with each test list to the <c>SelfTestRecordList</c> property.
        /// </summary>
        /// <remarks>
        /// Externals
        /// <list type="table">
        /// <listheader><term>Member Variable/Property</term><description>Description</description></listheader>
        /// <item><term>Items</term><description>Gets an indexed array of the records contained in the <c>GROUPLIST</c> table.</description></item>
        /// <item><term>m_TestListIdentifierLists</term><description>An array containing the lists of those records from the <c>TESTLISTIDS</c> table associated with
        /// each test list identifier. The array index corresponds to the test list identifier e.g. m_TestListIdentifierLists[3] would return a list of those records 
        /// that had a value of 3 for the <c>TESTLISTID</c> field.</description></item>
        /// </list>
        /// </remarks>
        private void AddSelfTestRecordLists()
        {
            List<TestListIdentifier_t> testListIdentifierList;
            for (short testListIdentifier = 0; testListIdentifier < m_IdentifierMax + 1; testListIdentifier++)
            {
                // Include a try/catch block in case not all of the primary key identifiers are defined.
                try
                {
                    // Instantiate an empty list.
                    Items[testListIdentifier].SelfTestRecordList = new List<SelfTestRecord>();

                    try
                    {
                        testListIdentifierList = m_TestListIdentifierLists[testListIdentifier];

                        // Add the self Tests corresponding to the group list identifier contained within the list.
                        for (int index = 0; index < testListIdentifierList.Count; index++)
                        {
                            Items[testListIdentifier].SelfTestRecordList.Add(Lookup.SelfTestTable.Items[testListIdentifierList[index].SelfTestIdentifier]);
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
        /// Build an array of lists of <c>TestListIdentifier_t</c> records associated with each unique <c>TESTLISTID</c> value defined in the <c>TESTLISTIDS</c> 
        /// table of the data dictionary. The array element is mapped to the <c>TESTLISTID</c> field of the table.
        /// </summary>
        /// <param name="testListIdentifiersDataTable">Reference to the <c>TESTLISTIDS</c> table of the data dictionary.</param>
        /// <returns>An array of lists of <c>TestListIdentifier_t</c> records associated with each unique <c>TESTLISTID</c> value in the <c>TESTLISTIDS</c> table 
        /// of the data dictionary, if the parameters are valid; otherwise, null.</returns>
        private List<TestListIdentifier_t>[] BuildTestListIdentifierLists(DataDictionary.TESTLISTIDSDataTable testListIdentifiersDataTable)
        {
            // Local copy of the data table.
            List<TestListIdentifier_t>[] records;

            if (testListIdentifiersDataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the identifier field in the data table, it cannot be assumed that the table is sorted by identifier.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int recordIndex = 0; recordIndex < testListIdentifiersDataTable.Count; recordIndex++)
                {
                    iDCurrent = testListIdentifiersDataTable[recordIndex].TESTLISTID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                // Instantiate the lookup array.
                records = new List<TestListIdentifier_t>[iDMax + 1];

                // Instantiate a generic list for each element of the array. 
                for (int recordIndex = 0; recordIndex < iDMax + 1; recordIndex++)
                {
                    records[recordIndex] = new List<TestListIdentifier_t>();
                }

                // Populate the lookup table;
                int identifier;
                DataDictionary.TESTLISTIDSRow row;
                for (int recordIndex = 0; recordIndex < testListIdentifiersDataTable.Count; recordIndex++)
                {
                    row = testListIdentifiersDataTable[recordIndex];
                    identifier = row.TESTLISTID;

                    // Instantiate a new structure to contain the data and copy the data across.
                    TestListIdentifier_t record = new TestListIdentifier_t();
                    record.TestListIdentifier = row.TESTLISTID;
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
