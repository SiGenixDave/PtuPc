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
 *  File name:  SelfTestTableBySelfTestNumber.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version     Author          Comments
 *  06/01/11    1.0.0       K.McD           1.  First entry into TortoiseSVN.
 *  07/11/11    1.16.6      K.McD           1.  Added System.Diagnostics to the Using directive
 *                                          2.  Added testMessagesDataTable parameter to constructor
 *                                          3.  Added the CreateSelfTestVariableList method.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Common.Configuration
{
    /// <summary>
    /// A class to help access the self tests defined in the <c>SELFTEST</c> table of the data dictionary. The self tests are accessed using the self test 
    /// number value.
    /// </summary>
    public class SelfTestTableBySelfTestNumber : SelfTestTable
    {
        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="dataTable">>Reference to the <c>SELFTEST</c> table of the data dictionary. This table contains the self test definitions.</param>
        /// <param name="selfTestIdentifiersDataTable">Reference to the <c>SELFTESTIDS</c> table of the data dictionary. This table defines which self test 
        /// variables are associated with each self test.</param>
        /// <param name="testMessagesDataTable">Reference to the <c>TESTMESSAGES</c> table of the data dictionary. This table defines the help index of the 
        /// test messages associated with the test case value for each test number.</param>
        public SelfTestTableBySelfTestNumber(DataDictionary.SELFTESTDataTable dataTable, DataDictionary.SELFTESTIDSDataTable selfTestIdentifiersDataTable, 
                                             DataDictionary.TESTMESSAGESDataTable testMessagesDataTable)
            : base(dataTable, selfTestIdentifiersDataTable, testMessagesDataTable)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Create a new list to store the self test variable values associated with a self test.
        /// </summary>
        /// <param name="testNumber">The test number associated with the test.</param>
        /// <returns>A new list of self test variables specific to a test with the specified self test number.</returns>
        public override List<SelfTestVariable> CreateSelfTestVariableList(short testNumber)
        {
            Debug.Assert(testNumber >= 0, "SelfTestTableBySelfTestNumber.CreateSelfTestVariableList");
            List<SelfTestIdentifier_t> selfTestIdentifierList;
            List<SelfTestVariable> selfTestVariableList = new List<SelfTestVariable>();

            try
            {
                // -----------------------------------------------------------------------------
                // Get the self test variables that are associated with the specified self test.
                // -----------------------------------------------------------------------------
                SelfTestVariable selfTestVariable;
                selfTestIdentifierList = m_SelfTestIdentifierLists[testNumber];
                for (int index = 0; index < selfTestIdentifierList.Count; index++)
                {
                    selfTestVariable = new SelfTestVariable();
                    Lookup.SelfTestVariableTable.Items[selfTestIdentifierList[index].SelfTestVariableIdentifier].CopyTo(ref selfTestVariable);
                    selfTestVariableList.Add(selfTestVariable);
                }
            }
            catch (Exception)
            {
                // Ensure that an exception isn't thrown.
            }

            return selfTestVariableList;
        }

        /// <summary>
        /// Build an array of the records contained within the <c>SELFTEST</c> table of the data dictionary. This array is used to access the 
        /// parameters associated with the self tests defined in the table. The array element is mapped to the self test number field of the table. 
        /// </summary>
        /// <param name="dataTable">Reference to the <c>SELFTEST</c> table of the data dictionary.</param>
        /// <returns>An array of the records contained within the <c>SELFTEST</c> table of the data dictionary, if the parameters are valid; otherwise, null.</returns>
        protected override SelfTestRecord[] BuildDataTable(DataDictionary.SELFTESTDataTable dataTable)
        {
            // Local copy of the data table.
            SelfTestRecord[] records;

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
                    iDCurrent = dataTable[recordIndex].SELFTESTNUMBER;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                m_IdentifierMax = iDMax;

                // Instantiate the lookup array.
                records = new SelfTestRecord[iDMax + 1];

                // Populate the lookup table;
                int identifier;
                DataDictionary.SELFTESTRow row;
                for (int recordIndex = 0; recordIndex < dataTable.Count; recordIndex++)
                {
                    row = dataTable[recordIndex];
                    identifier = row.SELFTESTNUMBER;

                    // Instantiate a new structure to contain the data and copy the data across.
                    SelfTestRecord record = new SelfTestRecord();
                    record.Identifier = (short)row.SELFTESTID;
                    record.SelfTestNumber = row.SELFTESTNUMBER;
                    record.Description = row.DESCRIPTION;
                    record.HelpIndex = row.HELPINDEX;

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
        /// Add the self test variables associated with each self test to the <c>SelfTestVariableList</c> property.
        /// </summary>
        /// <remarks>
        /// Externals
        /// <list type="table">
        /// <listheader><term>Member Variable/Property</term><description>Description</description></listheader>
        /// <item><term>Items</term><description>Gets an indexed array of the records contained in the <c>DataDictionary.SELFTESTDataTable</c> table.</description></item>
        /// <item><term>m_SelfTestIdentifierLists</term><description>An array containing the lists of those records from the <c>SELFTESTIDS</c> data table associated with
        /// each self test identifier. The array index corresponds to the self test identifier e.g. m_SelfTestIdentifierList[22] would return a list of those records 
        /// that had a value of 22 for <c>SELFTESTID</c> field.</description></item>
        /// </list>
        /// </remarks>
        protected override void AddSelfTestVariableLists()
        {
            List<SelfTestIdentifier_t> selfTestIdentifierList;
            for (short selfTestNumber = 0; selfTestNumber < m_IdentifierMax + 1; selfTestNumber++)
            {
                // Include a try/catch block in case not all of the primary key identifiers are defined.
                if (Items[selfTestNumber] == null)
                {
                    continue;
                }

                try
                {
                    // Instantiate an empty list.
                    Items[selfTestNumber].SelfTestVariableList = new List<SelfTestVariable>();

                    try
                    {
                        selfTestIdentifierList = m_SelfTestIdentifierLists[selfTestNumber];

                        // Add the self Test variables corresponding to the event variable identifier contained within the list.
                        for (int index = 0; index < selfTestIdentifierList.Count; index++)
                        {
                            Items[selfTestNumber].SelfTestVariableList.Add(Lookup.SelfTestVariableTable.Items[selfTestIdentifierList[index].SelfTestVariableIdentifier]);
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
        /// Build the array that is used to access the list of <c>SelfTestIdentifier</c> records associated with a specified self test identifier.
        /// Build an array of lists of <c>SelfTestIdentifier_t</c> records associated with each unique <c>SELFTESTID</c> value defined in the <c>SELFTESTIDS</c> 
        /// table of the data dictionary. The array element is mapped to the <c>SELFTESTNUMBER</c> field of the table.
        /// </summary>
        /// <param name="selfTestIdentifiersDataTable">Reference to the <c>SELFTESTIDS</c> data table of the data dictionary.</param>
        /// <returns>An array of lists of <c>SelfTestIdentifier_t</c> records associated with each unique <c>SELFTESTID</c> value in the <c>SELFTESTIDS</c> table of the 
        /// data dictionary, if the parameters are valid; otherwise, null.</returns>
        protected override List<SelfTestIdentifier_t>[] BuildSelfTestIdentifierLists(DataDictionary.SELFTESTIDSDataTable selfTestIdentifiersDataTable)
        {
            // Local copy of the data table.
            List<SelfTestIdentifier_t>[] records;

            if (selfTestIdentifiersDataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the identifier field in the data table, it cannot be assumed that the table is sorted by identifier.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int recordIndex = 0; recordIndex < selfTestIdentifiersDataTable.Count; recordIndex++)
                {
                    iDCurrent = selfTestIdentifiersDataTable[recordIndex].SELFTESTNUMBER;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                // Instantiate the lookup array.
                records = new List<SelfTestIdentifier_t>[iDMax + 1];

                // Instantiate a generic list for each element of the array. 
                for (int recordIndex = 0; recordIndex < iDMax + 1; recordIndex++)
                {
                    records[recordIndex] = new List<SelfTestIdentifier_t>();
                }

                // Populate the lookup table;
                int identifier;
                DataDictionary.SELFTESTIDSRow row;
                for (int recordIndex = 0; recordIndex < selfTestIdentifiersDataTable.Count; recordIndex++)
                {
                    row = selfTestIdentifiersDataTable[recordIndex];
                    identifier = row.SELFTESTNUMBER;

                    // Instantiate a new structure to contain the data and copy the data across.
                    SelfTestIdentifier_t record = new SelfTestIdentifier_t();
                    record.SelfTestIdentifier = row.SELFTESTID;
                    record.SelfTestNumber = row.SELFTESTNUMBER;
                    record.SelfTestVariableIdentifier = row.SELFTESTVARID;

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
