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
 *  File name:  LookupTable.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/10/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/26/11    1.1     K.McD           1.  Modified to accommodate the change in signature of the LogTable class.
 * 
 *  03/28/11    1.2     K.McD           1.  Added the WatchTableByOldIdentifier static property.
 *  
 *  06/22/11    1.3     K.McD           1.  Added support for the self test sub-system.
 *  
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Configuration
{
    /// <summary>
    /// A static class used to access/lookup the records contained within the primary key data tables defined in the data dictionary.
    /// </summary>
    public static class Lookup
    {
        #region --- Member Variables ---
        /// <summary>
        /// Reference to the table containing the watch variables, accessed by watch identifier.
        /// </summary>
        private static WatchVariableTable m_WatchVariableTable;

        /// <summary>
        /// Reference to the table containing the watch variables, accessed by old watch identifier.
        /// </summary>
        private static WatchVariableTableByOldIdentifier m_WatchVariableTableByOldIdentifier;

        /// <summary>
        /// Reference to the table containing the event variables.
        /// </summary>
        private static EventVariableTable m_EventVariableTable;

        /// <summary>
        /// Reference to the table containing the self-test variables. 
        /// </summary>
        private static SelfTestVariableTable m_SelfTestVariableTable;

        /// <summary>
        /// Reference to the table containing the system events.
        /// </summary>
        private static EventTable m_EventTable;

        /// <summary>
        /// Reference to the table containing the self test definitions, accessed using the self test identifier.
        /// </summary>
        private static SelfTestTable m_SelfTestTable;

        /// <summary>
        /// Reference to the table containing the self test definitions, accessed using the self test number value.
        /// </summary>
        private static SelfTestTableBySelfTestNumber m_SelfTestTableBySelfTestNumber;

        /// <summary>
        /// Reference to the table containing the system event logs that are available.
        /// </summary>
        private static LogTable m_LogTable;

        /// <summary>
        ///  Reference to the table containing the pre-defined self test groups.
        /// </summary>
        private static GroupListTable m_GroupListTable;

        /// <summary>
        ///  Reference to the table containing the pre-defined tests.
        /// </summary>
        private static TestListTable m_TestListTable;

        /// <summary>
        /// Reference to the data dictionary.
        /// </summary>
        private static DataDictionary m_DataDictionary;
        #endregion --- Member Variables ---

        #region --- Methods ---
        /// <summary>
        /// Initializes the static properties that allow the class user to access the primary key data tables defined within the data dictionary.
        /// </summary>
        /// <param name="dataDictionary">The data dictionary containing the data tables.</param>
        public static void Initialize(DataDictionary dataDictionary)
        {
            m_DataDictionary = dataDictionary;

            m_WatchVariableTable = new WatchVariableTable(dataDictionary.WATCHVARIABLES, dataDictionary.WATCHENUMBIT);
            m_WatchVariableTableByOldIdentifier = new WatchVariableTableByOldIdentifier(dataDictionary.WATCHVARIABLES, dataDictionary.WATCHENUMBIT);
            m_EventVariableTable = new EventVariableTable(dataDictionary.EVENTVARIABLES, dataDictionary.EVENTENUMBIT);
            m_SelfTestVariableTable = new SelfTestVariableTable(dataDictionary.SELFTESTVARIABLES, dataDictionary.SELFTESTENUMBIT);

            m_EventTable = new EventTable(dataDictionary.EVENTS, dataDictionary.STRUCT);
            m_LogTable = new LogTable(dataDictionary.LOGS, dataDictionary.DataStreamTypes);

            m_SelfTestTable = new SelfTestTable(dataDictionary.SELFTEST, dataDictionary.SELFTESTIDS, dataDictionary.TESTMESSAGES);
            m_SelfTestTableBySelfTestNumber = new SelfTestTableBySelfTestNumber(dataDictionary.SELFTEST, dataDictionary.SELFTESTIDS, dataDictionary.TESTMESSAGES);
            m_GroupListTable = new GroupListTable(dataDictionary.GROUPLIST, dataDictionary.GROUPLISTIDS);
            m_TestListTable = new TestListTable(dataDictionary.TESTLIST, dataDictionary.TESTLISTIDS);
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the table containing the watch variables, accessed by watch identifier.
        /// </summary>
        public static WatchVariableTable WatchVariableTable
        {
            get { return m_WatchVariableTable; }
        }

        /// <summary>
        /// Gets the table containing the watch variables, accesed by old watch identifier.
        /// </summary>
        public static WatchVariableTableByOldIdentifier WatchVariableTableByOldIdentifier
        {
            get { return m_WatchVariableTableByOldIdentifier; }
        }

        /// <summary>
        /// Gets the table containing the event variables.
        /// </summary>
        public static EventVariableTable EventVariableTable
        {
            get { return m_EventVariableTable; }
        }

        /// <summary>
        /// Gets the table containing the self-test variables.
        /// </summary>
        public static SelfTestVariableTable SelfTestVariableTable
        {
            get { return m_SelfTestVariableTable; }
        }

        /// <summary>
        /// Gets the table containing the system events.
        /// </summary>
        public static EventTable EventTable
        {
            get { return m_EventTable; }
        }

        /// <summary>
        /// Gets the table containing the self test definitions accessed by self test identifier.
        /// </summary>
        public static SelfTestTable SelfTestTable
        {
            get { return m_SelfTestTable; }
        }

        /// <summary>
        /// Gets the table containing the self test definitions accessed by self test number.
        /// </summary>
        public static SelfTestTable SelfTestTableBySelfTestNumber
        {
            get { return m_SelfTestTableBySelfTestNumber; }
        }

        /// <summary>
        /// Gets the table containing the system event logs that are available.
        /// </summary>
        public static LogTable LogTable
        {
            get { return m_LogTable; }
        }

        /// <summary>
        /// Gets the table containing the pre-defined self test groups.
        /// </summary>
        public static GroupListTable GroupListTable
        {
            get { return m_GroupListTable; }
        }

        /// <summary>
        /// Gets the table containing the pre-defined tests.
        /// </summary>
        public static TestListTable TestListTable
        {
            get { return m_TestListTable; }
        }

        /// <summary>
        /// Gets the reference to the data dictionary.
        /// </summary>
        public static DataDictionary DataDictionary
        {
            get { return m_DataDictionary; }
        }
        #endregion --- Properties ---
    }
}
