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
 *  File name:  DataDictionary.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TOrtoiseSVN.
 * 
 *  08/23/10    1.1     K.McD           1.  Added support to include ALL tables associated with the Microsoft Access engineering database in the XML data dictionary.
 * 
 *  11/02/10    1.2     K.McD           1.  Renamed variables.
 * 
 *  01/26/11    1.3     K.McD           1.  Added support for the DataStreamTypes table of the data dictionary.
 * 
 *  03/17/11    1.4     K.McD           1.  Included try/catch blocks for those tables that are not automatically created by the data dictionary builder 
 *                                          utility in case those tables have not been included in the data dictionary.
 * 
 *                                      2.  Modified the layout so that all actions associated with each table adapter are grouped together.
 * 
 *  03/28/11    1.5     K.McD           1.  Modified the names of a number of local variables.
 *  
 *  07/25/11    1.6     K.McD           1.  Added support for the (Universal Resource Identifier) URI table. This is required to support ethernet communications.
 *  
 *  04/08/15    1.7     K.McD           Reference
 *                                      1.  SNCR - R188 PTU [20 Mar 2015] Item 8. When using the 'Tools/Convert Engineering Database' menu option (Factory Mode Only)
 *                                          to convert the .e1 database to an XML configuration file; if any of the tables that are not automatically generated
 *                                          by the Data Dictionary Builder utility have not been manually updated correctly then the conversion should be terminated
 *                                          and the table where the error was detected should be reported to the user.
 *                                          
 *                                      2.  The 'Tools/Convert Engineering Database' menu option is to be modified to generate the XML file from two separate (.e1) files.
 *                                          The first file is the engineering data dictionary (.e1) database that is generated automatically from the Database Builder
 *                                          Utility. The second file is a project specific PTU configuration database that contains the following supplemental tables that
 *                                          are required for the correct operation of the PTU: CONFIGUREPTU, DataStreamTypes, LOGS, Security and URI. Partly populated
 *                                          CONFIGUREPTU and LOGS tables are created by the Database Builder Utility in the engineering data dictionary (.e1) database,
 *                                          however, these tables are ignored in the conversion process. The information contained in these partly populated tables can be
 *                                          useful when initially setting up the tables in the project PTU configuration database. 
 *                                      
 *                                          Both files are selected by the user and should be, ideally, located in the 
 *                                          '<User Application Data>\Bombardier\Portable Test Unit\PTU Configuration Files' sub-directory. The convention
 *                                          is to name the project PTU configuration database '<project-id>.PTU Configuration.e1' e.g. R8PR.PRU Configuration.e1.
 *                                      
 *                                          The purpose for this change is that once the project PTU configuration database is set up, new vesions of the XML file can
 *                                          be easily created using the Database Builder Utility as the output of the utility can be directly used to create the new XML 
 *                                          file whereas before, the supplemental tables had to be added manually to the (.e1) file output from the Database Builder
 *                                          Utility.
 *                                          
 *                                      Modifications
 *                                      1.  While trying to fill the data dictionary tables, now throws an exception in the catch block of those tables that are
 *                                          not automatically created using the data dictionary builder utility.
 *                                      2.  Modified the signature of the WriteDataSetToXml() method to include the connection to the project PTU configuration
 *                                          database.
 *                                      3.  Modified the WriteDataSetToXml() method to fill the: CONFIGUREPTU, DataStreamTypes, LOGS, Security and URI tables
 *                                          of the DataSet from the contents of the project PTU configuration database rather than the engineering data dictionary
 *                                          database.
 */
#endregion --- Revision History ---

using System;
using System.Data;
using System.Data.OleDb;

using Common.Configuration.DataDictionaryTableAdapters;
using Common.Properties;

namespace Common.Configuration
{
    /// <summary>
    /// Represents a strongly typed in-memory cache of data.
    /// </summary>
    public partial class DataDictionary
    {
        /// <summary>
        /// Populate All tables in the 'DataDictionary' DataSet with data extracted from the specified project PTU configuration database and the specified engineering
        /// data dictionary database and then write this data to the specified XML file. 
        /// </summary>
        /// <param name="oleDbPTUConfigurationConnection">The 'OleDbConnection' connection to a valid Microsoft Access project PTU configuration database.</param>
        /// <param name="oleDbConnection">The 'OleDbConnection' connection to a valid Microsoft Access engineering data dictionary database (.e1).</param>
        /// <param name="fullFilename">The fully qualified filename of the output XML file.</param>
        public static void WriteDataSetToXml(OleDbConnection oleDbPTUConfigurationConnection, OleDbConnection oleDbConnection, string fullFilename)
        {

            // Instantiate an empty DataSet into which the data is to be loaded.
            DataDictionary dataDictionary = new DataDictionary();

            // --------------------------------------------------------------------------------------------------------
            // Instantiate and fill those tables that are automatically created by the data dictionary builder utility.
            // --------------------------------------------------------------------------------------------------------

            ANNBITSTableAdapter annBitsTableAdapter = new ANNBITSTableAdapter();
            annBitsTableAdapter.Connection = oleDbConnection;
            annBitsTableAdapter.Fill(dataDictionary.ANNBITS);

            ANNIDSTableAdapter annIdsTableAdapter = new ANNIDSTableAdapter();
            annIdsTableAdapter.Connection = oleDbConnection;
            annIdsTableAdapter.Fill(dataDictionary.ANNIDS);

            ANNLIGHTTableAdapter annLightTableAdapter = new ANNLIGHTTableAdapter();
            annLightTableAdapter.Connection = oleDbConnection;
            annLightTableAdapter.Fill(dataDictionary.ANNLIGHT);

            ANNLISTTableAdapter annListTableAdapter = new ANNLISTTableAdapter();
            annListTableAdapter.Connection = oleDbConnection;
            annListTableAdapter.Fill(dataDictionary.ANNLIST);

            ANNUNENUMBITTableAdapter annEnumBitTableAdapter = new ANNUNENUMBITTableAdapter();
            annEnumBitTableAdapter.Connection = oleDbConnection;
            annEnumBitTableAdapter.Fill(dataDictionary.ANNUNENUMBIT);

            EVENTENUMBITTableAdapter eventEnumBitTableAdapter = new EVENTENUMBITTableAdapter();
            eventEnumBitTableAdapter.Connection = oleDbConnection;
            eventEnumBitTableAdapter.Fill(dataDictionary.EVENTENUMBIT);

            EVENTSTableAdapter eventsTableAdapter = new EVENTSTableAdapter();
            eventsTableAdapter.Connection = oleDbConnection;
            eventsTableAdapter.Fill(dataDictionary.EVENTS);

            EVENTVARIABLESTableAdapter eventVariablesTableAdapter = new EVENTVARIABLESTableAdapter();
            eventVariablesTableAdapter.Connection = oleDbConnection;
            eventVariablesTableAdapter.Fill(dataDictionary.EVENTVARIABLES);

            FILEINFOTableAdapter fileInfoTableAdapter = new FILEINFOTableAdapter();
            fileInfoTableAdapter.Connection = oleDbConnection;
            fileInfoTableAdapter.Fill(dataDictionary.FILEINFO);

            GROUPLISTTableAdapter groupListTableAdapter = new GROUPLISTTableAdapter();
            groupListTableAdapter.Connection = oleDbConnection;
            groupListTableAdapter.Fill(dataDictionary.GROUPLIST);

            GROUPLISTIDSTableAdapter groupListIdsTableAdapter = new GROUPLISTIDSTableAdapter();
            groupListIdsTableAdapter.Connection = oleDbConnection;
            groupListIdsTableAdapter.Fill(dataDictionary.GROUPLISTIDS);

            MACROCMDSTableAdapter macroCmdsTableAdapter = new MACROCMDSTableAdapter();
            macroCmdsTableAdapter.Connection = oleDbConnection;
            macroCmdsTableAdapter.Fill(dataDictionary.MACROCMDS);

            MACROSTableAdapter macrosTableAdapter = new MACROSTableAdapter();
            macrosTableAdapter.Connection = oleDbConnection;
            macrosTableAdapter.Fill(dataDictionary.MACROS);

            MAINTENANCETableAdapter maintenanceTableAdapter = new MAINTENANCETableAdapter();
            maintenanceTableAdapter.Connection = oleDbConnection;
            maintenanceTableAdapter.Fill(dataDictionary.MAINTENANCE);

            SELFTESTTableAdapter selfTestTableAdapter = new SELFTESTTableAdapter();
            selfTestTableAdapter.Connection = oleDbConnection;
            selfTestTableAdapter.Fill(dataDictionary.SELFTEST);

            SELFTESTENUMBITTableAdapter selfTestEnumBitTableAdapter = new SELFTESTENUMBITTableAdapter();
            selfTestEnumBitTableAdapter.Connection = oleDbConnection;
            selfTestEnumBitTableAdapter.Fill(dataDictionary.SELFTESTENUMBIT);

            SELFTESTERRMESSTableAdapter selfTestTerrMessTableAdapter = new SELFTESTERRMESSTableAdapter();
            selfTestTerrMessTableAdapter.Connection = oleDbConnection;
            selfTestTerrMessTableAdapter.Fill(dataDictionary.SELFTESTERRMESS);

            SELFTESTIDSTableAdapter selfTestIdsTableAdapter = new SELFTESTIDSTableAdapter();
            selfTestIdsTableAdapter.Connection = oleDbConnection;
            selfTestIdsTableAdapter.Fill(dataDictionary.SELFTESTIDS);

            SELFTESTVARIABLESTableAdapter selfTestVariablesTableAdapter = new SELFTESTVARIABLESTableAdapter();
            selfTestVariablesTableAdapter.Connection = oleDbConnection;
            selfTestVariablesTableAdapter.Fill(dataDictionary.SELFTESTVARIABLES);

            STRUCTTableAdapter structTableAdapter = new STRUCTTableAdapter();
            structTableAdapter.Connection = oleDbConnection;
            structTableAdapter.Fill(dataDictionary.STRUCT);

            TASKSTableAdapter tasksTableAdapter = new TASKSTableAdapter();
            tasksTableAdapter.Connection = oleDbConnection;
            tasksTableAdapter.Fill(dataDictionary.TASKS);

            TESTLISTTableAdapter testListTableAdapter = new TESTLISTTableAdapter();
            testListTableAdapter.Connection = oleDbConnection;
            testListTableAdapter.Fill(dataDictionary.TESTLIST);

            TESTLISTIDSTableAdapter testListIdsTableAdapter = new TESTLISTIDSTableAdapter();
            testListIdsTableAdapter.Connection = oleDbConnection;
            testListIdsTableAdapter.Fill(dataDictionary.TESTLISTIDS);

            TESTMESSAGESTableAdapter testMessagesTableAdapter = new TESTMESSAGESTableAdapter();
            testMessagesTableAdapter.Connection = oleDbConnection;
            testMessagesTableAdapter.Fill(dataDictionary.TESTMESSAGES);

            WATCHENUMBITTableAdapter watchEnumBitTableAdapter = new WATCHENUMBITTableAdapter();
            watchEnumBitTableAdapter.Connection = oleDbConnection;
            watchEnumBitTableAdapter.Fill(dataDictionary.WATCHENUMBIT);

            WATCHVARIABLESTableAdapter watchVariablesTableAdapter = new WATCHVARIABLESTableAdapter();
            watchVariablesTableAdapter.Connection = oleDbConnection;
            watchVariablesTableAdapter.Fill(dataDictionary.WATCHVARIABLES);

            // ----------------------------------------------------------------------------------------------------------------------
            // Include try/catch blocks on those tables that are not automatically created using the data dictionary builder utility. 
            // If an OleDbException is thrown, the XML generation process will be terminated and the user will be informed which table
            // threw the exception. These tables are populated from the project PTU configuration database, <project-id>.PTU Configuration.mdb,
            // rather than the engineering data dictionary database generated from the Database Builder Utility.
            // ----------------------------------------------------------------------------------------------------------------------

            // --------------------------------------------------------------------------------------------------------
            // Instantiate and fill those tables that are derived from the project PTU configuration database.
            // --------------------------------------------------------------------------------------------------------
            try
            {
                CONFIGUREPTUTableAdapter configurePTUTableAdapter = new CONFIGUREPTUTableAdapter();
                configurePTUTableAdapter.Connection = oleDbPTUConfigurationConnection;
                configurePTUTableAdapter.Fill(dataDictionary.CONFIGUREPTU);
            }
            catch (OleDbException)
            {
                throw new Exception(string.Format(Resources.MBTTableColumnMissing, "CONFIGUREPTU"));
            }

            try
            {
                DataStreamTypesTableAdapter dataStreamTypesTableAdapter = new DataStreamTypesTableAdapter();
                dataStreamTypesTableAdapter.Connection = oleDbPTUConfigurationConnection;
                dataStreamTypesTableAdapter.Fill(dataDictionary.DataStreamTypes);
            }
            catch (OleDbException)
            {
                throw new Exception(string.Format(Resources.MBTTableColumnMissing, "DataStreamTypes"));
            }

            try
            {
                // TODO - DataDictionary.WriteDataSetToXml(). Include code to create and fill a standard LOGSTableAdapter if an exception is thrown.
                LOGSTableAdapter logsTableAdapter = new LOGSTableAdapter();
                logsTableAdapter.Connection = oleDbPTUConfigurationConnection;
                logsTableAdapter.Fill(dataDictionary.LOGS);
            }
            catch (OleDbException)
            {
                throw new Exception(string.Format(Resources.MBTTableColumnMissing, "LOGS"));
            }

            try
            {
                SecurityTableAdapter securityTableAdapter = new SecurityTableAdapter();
                securityTableAdapter.Connection = oleDbPTUConfigurationConnection;
                securityTableAdapter.Fill(dataDictionary.Security);
            }
            catch (OleDbException)
            {
                throw new Exception(string.Format(Resources.MBTTableColumnMissing, "Security"));
            }

            try
            {
                URITableAdapter uRITableAdapter = new URITableAdapter();
                uRITableAdapter.Connection = oleDbPTUConfigurationConnection;
                uRITableAdapter.Fill(dataDictionary.URI);
            }
            catch (OleDbException)
            {
                throw new Exception(string.Format(Resources.MBTTableColumnMissing, "URI"));
            }
            
            // Write the DataSet containing the data to the specified XML file.
            dataDictionary.WriteXml(fullFilename, XmlWriteMode.WriteSchema);
        }
    }
}
