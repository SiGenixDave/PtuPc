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
 *  File name:  SelfTestTable.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version     Author          Comments
 *  06/01/11    1.0         K.McD           1.  First entry into TortoiseSVN.
 *  07/11/11    1.16.6      K.McD           1.  Added the TestMessage_t struct
 *                                          2.  Added the m_TestMessageLists member variable for tracking Test Messages
 *                                          3.  Added the testMessagesDataTable field to the Constructor
 *                                          4.  Made the CreateSelfTestVariableList method virtual
 *                                          5.  Added the BuildTestMessageLists method.
 *                                          6.  Added the GetTestMessageHelpIndex method.
  */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Common.Configuration
{
    #region --- Structures ---
    /// <summary>
    /// A structure to store the fields associated with a record from the <c>TESTMESSAGES</c> table of the data dictionary.
    /// </summary>
    public struct TestMessage_t
    {
        #region --- Member Variables ---
        /// <summary>
        /// The test case value associated with the record.
        /// </summary>
        private short m_TestCase;

        /// <summary>
        /// The test number associated with the record.
        /// </summary>
        private short m_TestNumber;

        /// <summary>
        /// The help index corresponding to the test case value and test number combination.
        /// </summary>
        private int m_HelpIndex;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the structure.
        /// </summary>
        /// <param name="testCase">The test case value associated with the record.</param>
        /// <param name="testNumber">The test number associated with the record.</param>
        /// <param name="helpIndex">The help index corresponding to the test case value and test number combination.</param>
        public TestMessage_t(short testCase, short testNumber, int helpIndex)
        {
            m_TestCase = testCase;
            m_TestNumber = testNumber;
            m_HelpIndex = helpIndex;
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the test case value associated with the record.
        /// </summary>
        public short TestCase
        {
            get { return m_TestCase; }
            set { m_TestCase = value; }
        }

        /// <summary>
        /// Gets or sets the test number associated with the record.
        /// </summary>
        public short TestNumber
        {
            get { return m_TestNumber; }
            set { m_TestNumber = value; }
        }

        /// <summary>
        /// Gets or sets the help index corresponding to the test case value and test number combination.
        /// </summary>
        public int HelpIndex
        {
            get { return m_HelpIndex; }
            set { m_HelpIndex = value; }
        }
        #endregion --- Properties ---
    }
    #endregion --- Structures ---

    /// <summary>
    /// A class to help access the self tests defined in the <c>SELFTEST</c> table of the data dictionary. The self tests are accessed using the self test 
    /// identifier value.
    /// </summary>
    public class SelfTestTable : Table<SelfTestRecord, DataDictionary.SELFTESTDataTable>
    {
        #region --- Member Variables ---
        /// <summary>
        /// An array containing the lists of those records from the <c>SELFTESTIDS</c> table associated with each self test identifier. The array index 
        /// corresponds to the self test identifier e.g. m_SelfTestIdentifiersLists[22] would return a list of those records that had a value of 22 for the 
        /// <c>SELFTESTID</c> field.
        /// </summary>
        protected List<SelfTestIdentifier_t>[] m_SelfTestIdentifierLists;

        /// <summary>
        /// Reference to the <c>SELFTEST</c>table of the data dictionary.
        /// </summary>
        private DataDictionary.SELFTESTDataTable m_SelfTestDataTable;

        /// <summary>
        /// An array containing the lists of records from the <c>TESTMESSAGES</c> table of the data dictionary associated with each test number.
        /// </summary>
        private List<TestMessage_t>[] m_TestMessageLists;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="dataTable">>Reference to the <c>SELFTEST</c> table of the data dictionary. This table contains the self test definitions.</param>
        /// <param name="selfTestIdentifiersDataTable">Reference to the <c>SELFTESTIDS</c> table of the data dictionary. This table defines which self test 
        /// variables are associated with each self test.</param>
        /// <param name="testMessagesDataTable">Reference to the <c>TESTMESSAGES</c> table of the data dictionary. This table defines the help index of the 
        /// test messages associated with the test case value for each test number.</param>
        public SelfTestTable(DataDictionary.SELFTESTDataTable dataTable, DataDictionary.SELFTESTIDSDataTable selfTestIdentifiersDataTable, 
                             DataDictionary.TESTMESSAGESDataTable testMessagesDataTable)
            : base(dataTable)
        {
            if (selfTestIdentifiersDataTable == null)
            {
                return;
            }

            m_SelfTestDataTable = dataTable;
            m_SelfTestIdentifierLists = BuildSelfTestIdentifierLists(selfTestIdentifiersDataTable);
            AddSelfTestVariableLists();
            m_TestMessageLists = BuildTestMessageLists(testMessagesDataTable);
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Create a new list to store the self test variable values associated with a self test.
        /// </summary>
        /// <param name="identifier">The self test identifier associated with the test.</param>
        /// <returns>A new list of self test variables specific to a test with the specified self test identifier.</returns>
        public virtual List<SelfTestVariable> CreateSelfTestVariableList(short identifier)
        {
            Debug.Assert(identifier >= 0, "SelfTestTable.CreateSelfTestVariableList");
            List<SelfTestIdentifier_t> selfTestIdentifierList;
            List<SelfTestVariable> selfTestVariableList = new List<SelfTestVariable>();

            try
            {
                // -----------------------------------------------------------------------------
                // Get the self test variables that are associated with the specified self test.
                // -----------------------------------------------------------------------------
                SelfTestVariable selfTestVariable;
                selfTestIdentifierList = m_SelfTestIdentifierLists[identifier];
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
        /// parameters associated with the self tests defined in the table. The array element is mapped to the self test identifier field of the table. 
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
                    iDCurrent = dataTable[recordIndex].SELFTESTID;
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
                    identifier = row.SELFTESTID;

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
        protected virtual void AddSelfTestVariableLists()
        {
            List<SelfTestIdentifier_t> selfTestIdentifierList;
            for (short selfTestIdentifier = 0; selfTestIdentifier < m_IdentifierMax + 1; selfTestIdentifier++)
            {
                // Include a try/catch block in case not all of the primary key identifiers are defined.
                try
                {
                    // Instantiate an empty list.
                    Items[selfTestIdentifier].SelfTestVariableList = new List<SelfTestVariable>();

                    try
                    {
                        selfTestIdentifierList = m_SelfTestIdentifierLists[selfTestIdentifier];

                        // Add the self Test variables corresponding to the event variable identifier contained within the list.
                        for (int index = 0; index < selfTestIdentifierList.Count; index++)
                        {
                            Items[selfTestIdentifier].SelfTestVariableList.Add(Lookup.SelfTestVariableTable.Items[selfTestIdentifierList[index].SelfTestVariableIdentifier]);
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
        /// Build an array of the list of <c>SelfTestIdentifier_t</c> records associated with each unique <c>SELFTESTID</c> value defined in the <c>SELFTESTIDS</c> 
        /// table of the data dictionary. The array element is mapped to the <c>SELFTESTID</c> field of the table.
        /// </summary>
        /// <param name="selfTestIdentifiersDataTable">Reference to the <c>SELFTESTIDS</c> data table of the data dictionary.</param>
        /// <returns>An array of lists of <c>SelfTestIdentifier_t</c> records associated with each unique <c>SELFTESTID</c> value in the <c>SELFTESTIDS</c> table of the 
        /// data dictionary, if the parameters are valid; otherwise, null.</returns>
        protected virtual List<SelfTestIdentifier_t>[] BuildSelfTestIdentifierLists(DataDictionary.SELFTESTIDSDataTable selfTestIdentifiersDataTable)
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
                    iDCurrent = selfTestIdentifiersDataTable[recordIndex].SELFTESTID;
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
                    identifier = row.SELFTESTID;

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

        /// <summary>
        /// Build an array containing the list of records from the <c>TESTMESSAGES</c> table of the data dictionary for each unique test number. The array element 
        /// is mapped to the <c>TESTID</c> field of the table. Note: Although the field is called <c>TESTID</c> in the data dictionary, the values correspond to 
        /// the test number rather than test identifier.
        /// </summary>
        /// <param name="testMessagedDataTable">Reference to the <c>TESTMESSAGES</c> table of the data dictionary.</param>
        /// <returns>An array containing the list of records from the <c>TESTMESSAGES</c> table of the data dictionary for each unique test number, if the parameters 
        /// are valid; otherwise, null.</returns>
        private List<TestMessage_t>[] BuildTestMessageLists(DataDictionary.TESTMESSAGESDataTable testMessagedDataTable)
        {
            // Local copy of the data table.
            List<TestMessage_t>[] records;

            if (testMessagedDataTable == null)
            {
                return null;
            }

            try
            {
                // Determine the maximum value of the TESTID field in the data table, it cannot be assumed that the table is sorted by TESTID.
                int iDMax = 0;
                int iDCurrent = 0;
                for (int recordIndex = 0; recordIndex < testMessagedDataTable.Count; recordIndex++)
                {
                    iDCurrent = testMessagedDataTable[recordIndex].TESTID;
                    if (iDCurrent > iDMax)
                    {
                        iDMax = iDCurrent;
                    }
                }

                // Instantiate the lookup array.
                records = new List<TestMessage_t>[iDMax + 1];

                // Instantiate a generic list for each array element.
                for (int recordIndex = 0; recordIndex < iDMax + 1; recordIndex++)
                {
                    records[recordIndex] = new List<TestMessage_t>();
                }

                // Populate the lookup table;
                int identifier;
                DataDictionary.TESTMESSAGESRow row;
                for (int recordIndex = 0; recordIndex < testMessagedDataTable.Count; recordIndex++)
                {
                    row = testMessagedDataTable[recordIndex];
                    identifier = row.TESTID;

                    // Instantiate a new structure to contain the variable data and copy the data across.
                    TestMessage_t record = new TestMessage_t();
                    record.TestCase = row.TESTCASE;
                    record.TestNumber = row.TESTID;
                    record.HelpIndex = row.HELPINDEX;

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

        /// <summary>
        /// Get the help index associated with the test message for the specified test number/test case combination.
        /// </summary>
        /// <param name="testNumber">The test number.</param>
        /// <param name="testCase">The test case.</param>
        /// <returns>The help index associated with the specified test number/test case combination.</returns>
        public int GetTestMessageHelpIndex(short testNumber, short testCase)
        {
            int helpIndex = CommonConstants.NotDefined;

            try
            {
                // Get the list of records associated with the specified test number.
                List<TestMessage_t> testMessageList = m_TestMessageLists[testNumber];

                // Find the record corresponding to the specified test case.
                helpIndex = testMessageList.Find(delegate(TestMessage_t searchTestMessage) { return searchTestMessage.TestCase == testCase; }).HelpIndex;

                // Check whether a record exists for the specified test case.
                if (helpIndex == 0)
                {
                    // No, set the help index to be undefined.
                    helpIndex = CommonConstants.NotDefined;
                }
            }
            catch(Exception)
            {
                // Do nothing, just ensure that an exception isn't thrown.
            }

            return helpIndex;
        }
        #endregion --- Methods ---
    }
}
