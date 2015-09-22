#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Common
 * 
 *  File name:  General.cs
 * 
 *  Revision History
 *  ----------------
 */

/* 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN
 * .
 *  08/13/10    1.1     K.McD           1.  Included GetUserName() method.
 * 
 *  08/24/10    1.2     K.McD           1.  Added a number of methods to modify the Enabled and Visible properties of menu options: 
 *                                          SetMenuStripVisible(), SetToolStripMenuItemVisible(), SetMenuStripEnabled(), SetToolStripMenuItemEnabled()
 *                                          and GetToolStripMenuItem().
 * 
 *  08/25/10    1.3     K.McD           1.  Added the method ConvertYear().
 * 
 *  08/30/10    1.4     K.McD           1.  Renamed ConvertYear() to ConverYearToLong() and added ConvertYearToShort().
 * 
 *  09/29/10    1.5     K.McD           1.  Added the following methods: GetDecimalPlaces(), GetIncrement() and UploadValue().
 * 
 *  10/15/10    1.6     K.McD           1.  Moved the UploadValue() method to the FormChangeWatch class im Watch.Forms.
 * 
 *  11/04/10    1.7     K.McD           1.  Extended the GetDecimalPlaces() and GetIncrement() method to support ScaleFactor values below 0.00001.
 * 
 *  12/01/10    1.8     K.McD           1.  Added the FileDialogSaveFaultLog() and FileDialogSaveEventLog() methods.
 *                                      2.  Additional signature added to the DeriveName() method.
 * 
 *  01/06/11    1.9     K.McD           1.  Added the FileDialogOpenFile() method.
 *                                      2.  Minor changes to a number of XML tags and comments.
 *                                      3.  Modified the signature of the DeriveName() method to include the car identifier.
 *                                      4.  Modified the LoadDataDictionary() method to update the 'PTU Configuration.xml' file if a new data
 *                                          dictionary is selected.
 * 
 *  01/31/11    1.10    K.McD           1.  Added another signature to the DeriveName() method.
 * 
 *  02/03/11    1.11    K.McD           1.  Added the FileDialogOpenFileMultiSelect() method.
 * 
 *  02/28/11    1.12    K.McD           1.  Changed a number of XML tags.
 *                                      2.  Deprecated one of the DeriveName() method signatures.
 *                                      3.  Auto-modified as a result of a number of resource name changes.
 *                                      4.  Modified the signatures associated with the DeriveName() methods.
 *                                      5.  Added the GetFullyQualifiedFaultLogFilename() method.
 *                                      6.  Moved the Century constant to the CommonConstants class.
 * 
 *  03/01/11    1.12.1  K.McD           1.  Corrected the parameters passed to the DeriveName() method in the GetFullyQualifiedFaulLogFilename()
 *                                          methods.
 * 
 *  03/18/11    1.12.2  K.McD           1.  Modified the LoadDataDictionary() method such that the same call to the FileInfo.CopyTo() method is made
 *                                          nomatter whether the destination file exists or not.
 * 
 *  03/28/11    1.12.3  K.McD           1.  Renamed a number of local variables.
 *                                      2.  Included the FileDialogSaveImageFile() method.
 * 
 *  04/21/11    1.12.4  K.McD           1.  Corrected the GetToolStripMenuItem() methods.
 *
 *	10/17/11	1.12.5	Sean.D			1.	Changed FileDialogSaveImageFile to override the file extension based on the format we're saving as.
 *
 *	11/03/11	1.12.6	Sean.D			1.	Changed LoadDataDictionary to reject trying to load PTU Configuration.xml file with a clear error message.
 *	
 *  07/31/13    1.12.7  K.McD           1.  Modified the LoadDictionary() method to copy the source file to the file that is defined as the current XML
 *                                          data dictionary file rather than the default XML data dictionary file. This could be either the default XML
 *                                          data dictionary file or the project XML data dictionary file.
 *                                          
 *  03/24/15    1.13    K.McD           1.  Deleted the ConvertYearToLong() and ConvertYearToShort() methods and associated constants.
 */

/*
 *  04/15/15    1.14    K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *
 *                                      1.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified to
 *                                          meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the current naming
 *                                          convention will still apply.
 *                                          
 *                                  2.  The 'Tools/Convert Engineering Database' menu option is to be modified to generate the XML file from two separate (.e1) files.
 *                                      The first file is the engineering data dictionary (.e1) database that is generated automatically from the Database Builder
 *                                      Utility. The second file is a project specific PTU configuration database that contains the following supplemental tables that
 *                                      are required for the correct operation of the PTU: CONFIGUREPTU, DataStreamTypes, LOGS, Security and URI. Partly populated
 *                                      CONFIGUREPTU and LOGS tables are created by the Database Builder Utility in the engineering data dictionary (.e1) database,
 *                                      however, these tables are ignored in the conversion process. The information contained in these partly populated tables can be
 *                                      useful when initially setting up the tables in the project PTU configuration database. 
 *                                      
 *                                      Both files are selected by the user and should be, ideally, located in the 
 *                                      '<User Application Data>\Bombardier\Portable Test Unit\PTU Configuration Files' sub-directory. The convention
 *                                      is to name the project PTU configuration database '<project-id>.PTU Configuration.e1' e.g. R8PR.PRU Configuration.e1.
 *                                      
 *                                      The purpose for this change is that once the project PTU configuration database is set up, new vesions of the XML file can
 *                                      be easily created using the Database Builder Utility as the output of the utility can be directly used to create the new XML 
 *                                      file whereas before, the supplemental tables had to be added manually to the (.e1) file output from the Database Builder Utility.
 *
 *                                  Modifications
 *                                  1.  Removed the DeriveName(string carIdentifier, DateTime dateTime, string extension) signature to simplify file naming.
 *                                  2.  Modified the DeriveName() method to include support for the file naming convention defined in section 5.1.4. of the
 *                                      Kawasaki PTE Uniform Interface Specification.
 *                                  3.  Modified the ConvertEngineeringDatabaseToXML() method to ask the user for the location of the engineering data dictionary
 *                                      database (.e1) and the project PTU configuration database (.e1) and to create separate connections to both. Once created,
 *                                      these connections are passed as parameters to the call to the newly updated DataDictionary.WriteDataSetToXml() method.
 */

/*
 *  05/12/15    1.15    K.McD       References
 *                                  1.  The 'Tools/Convert Engineering Database' menu option is to be modified to track the read directory where the engineering
 *                                      data dictionary (.e1) database and the project specific PTU configuration(.e1) database are located. This directory will
 *                                      remain the default directory for the remainder of the session.
 *  
 *                                  Modifications
 *                                  1.  Modified the ConvertEngineeringDatabaseToXML() method to use and update the InitialDirectory.E1FilesRead path to specify
 *                                      the default location for the the engineering data dictionary (.e1) database and the project specific PTU configuration(.e1)
 *                                      database
 */

/*
 *  07/13/15    1.16    K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *                                      
 *                                     1.   NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified to
 *                                          meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the current naming
 *                                          convention will still apply.
 *                                              
 *                                      Modifications
 *                                      1.  Added a number of NYCT specific constants that are used to derive the NYCT filename.
 *                                      2.  Extended the DeriveName() method to include support for the NYCT filename format.
 *                                      
 *  07/24/15    1.17    K.McD       References
 *                                  1.  An informal review of version 6.11 of the PTU concluded that, where possible - i.e. if the PTU is started from a shortcut
 *                                      that passes the project identifier as a shortcut parameter, the project specific PTU initialization should be carried out
 *                                      in the MDI Form contructor that has the parameter string array as its signature rather than by the LoadDictionary() method.
 *                                      This streamlines the display construction of the Control Panel associated with the R188 project. In the 6.11 implementation
 *                                      the CTA layout is momentarily displayed before the Control Panel is drawn, however by initializing the project specific
 *                                      features in the constructor the Control Panel associated with the R188 project is drawn immediately and the CTA layout
 *                                      is not shown at all.
 *  
 *                                  2.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 21. It is possible to load a data dictionary that is not associated with the
 *                                      current project, i.e. the project associated with the project identifier that was passed via the desktop shortcut, when using
 *                                      the 'File/Select Data Dictionary' menu option. It is also possible to select the '<project-identifier>.PTU Configuration.xml'
 *                                      file.
 *                                      
 *                                  Modifications
 *                                  1.  Modified the LoadDataDictionary() method to check that: (a) the project identifier associated with the selected data dictionary
 *                                      matches the project identifier that was passed to the application as a desktop shortcut parameter; and (b) the default
 *                                      configuration file, 'PTU Configuration.xml',  or one of the project default configuration files, 
 *                                      '<project-identifier>.PTU Configuration.xml' was not selected by the user. If any of these conditions are true, then the
 *                                      method terminates. - Ref.: 2.
 *                                      
 *                                  2.  Modified the LoadDictionary() method to initiate a restart of the PTU if a valid data dictionary was selected and successfully
 *                                      loaded. - Ref 1.
 */

/*
 *  08/12/15    1.18    K.McD       References
 *                                  1.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 27. If the Factory user uses the ‘Select Data Dictionary’ menu option to update
 *                                      the configuration file, or, the PTU tries to update the configuration file because of a mismatch between the version of
 *                                      the embedded software and that of the XML configuration file; the selected file is not copied across to the project default
 *                                      configuration file, consequently, the next time that the application is run; the PTU reports that it cannot locate the default
 *                                      project configuration file.
 *                                  
 *                                  Modifications
 *                                  1.  Modified the LoadDataDictionary() method to copy the selected file to the correct default configuration file according to
 *                                      the value of the project-identifier that was passed as a desktop shortcut parameter. If the project-identifier is string.Empty
 *                                      the selected file is copied to 'Configuration.xml'; otherwise, it is copied to '<project-identifier>.Configuration.xml.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Common.Configuration;
using Common.Properties;

namespace Common
{
    /// <summary>
    /// A general collection of useful, but unrelated, static methods.
    /// </summary>
    public class General
    {
        #region --- Constants ---
        /// <summary>
        /// The filter index corresponding to the JPeg image format.Value: 1.
        /// </summary>
        private const int FilterIndexJPeg = 1;

        /// <summary>
        /// The filter index corresponding to the Bmp image format. Value: 2.
        /// </summary>
        private const int FilterIndexBmp = 2;

        #region - [NYCT Specific Constants] -
        /// <summary>
        /// The NYCT location code that is used to specify which connector the PTU is connected to in to the car. This field is blank where the location is not ambiguous
        /// i.e. only one PTU connector exists on a car for this application. Value: string.Empty.
        /// </summary>
        private const string LocationCode = "";

        /// <summary>
        /// The NYCT subsystem code associated with the Bombardier PTU. Value: "PTEP".
        /// </summary>
        private const string Subsystem = "PTEP";

        /// <summary>
        /// The text that is pre-pended to the standard NYCT filename format to identify it as an event log file. Value: "Log".
        /// </summary>
        private const string LogIdentifier = "LOG";

        /// <summary>
        /// The text that is used on the NYCT project to identify the file as screen capture image. Value: "Image".
        /// </summary>
        private const string ImageIdentifier = "IMAGE";
        #endregion - [NYCT Specific Constants] -
        #endregion --- Constants ---

        #region --- Methods ---
        #region - [FileDialog] -
        /// <summary>
        /// Use the <c>OpenFileDialog</c>class to ask the user to select one or more file(s) of the specified type.
        /// </summary>
        /// <param name="title">The title that is to appear in the file dialog box.</param>
        /// <param name="defaultExtension">The default extension associated with the type of file that is to be selected.</param>
        /// <param name="filterText">The filter text that appears in the dialog box.</param>
        /// <param name="initialDirectory">The path corresponding to the initial directory that is selected.</param>
        /// <remarks>The dialog box will only display those files with the specified default extension.</remarks>
        /// <returns>An array containing the fully qualified filenames of the selected files, if at least one valid file was selected; otherwise,
        /// null.</returns>
        public static string[] FileDialogOpenFileMultiSelect(string title, string defaultExtension, string filterText, string initialDirectory)
        {
            // Ask the user to select the file.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.DefaultExt = defaultExtension.Remove(0, 1);
            openFileDialog.Filter = filterText + "|*" + defaultExtension;
            openFileDialog.InitialDirectory = initialDirectory;
            openFileDialog.Multiselect = true;

            // Show the form which allows the user to select multiple files.
            openFileDialog.ShowDialog();
            return openFileDialog.FileNames;
        }

        /// <summary>
        /// Use the <c>OpenFileDialog</c>class to ask the user to select a single file of the specified type.
        /// </summary>
        /// <param name="title">The title that is to appear in the file dialog box.</param>
        /// <param name="defaultExtension">The default extension associated with the type of file that is to be selected.</param>
        /// <param name="filterText">The filter text that appears in the dialog box.</param>
        /// <param name="initialDirectory">The path corresponding to the initial directory that is selected.</param>
        /// <remarks>The dialog box will only display those files with the specified default extension.</remarks>
        /// <returns>The fully qualified filename of the selected file, if a valid file was selected; otherwise, an empty string.</returns>
        public static string FileDialogOpenFile(string title, string defaultExtension, string filterText, string initialDirectory)
        {
            // Ask the user to select the file.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.DefaultExt = defaultExtension.Remove(0, 1);
            openFileDialog.Filter = filterText + "|*" + defaultExtension;
            openFileDialog.InitialDirectory = initialDirectory;
            
            // Show the form which allows the user to select a file.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Set-up the <c>SaveFileDialog</c> parameters associated with saving a data dictionary to disk.
        /// </summary>
        /// <param name="defaultFilename">The default filename.</param>
        /// <param name="initialDirectory">The path of the initial directory that is to be displayed.</param>
        /// <returns>The fully qualified filename of the specified file or 'string.Empty' if no file is specified.</returns>
        public static string FileDialogSaveDataDictionary(string defaultFilename, string initialDirectory)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = initialDirectory;
            saveFileDialog.DefaultExt = CommonConstants.ExtensionDataDictionary.Remove(0, 1);
            saveFileDialog.Title = Resources.FileDialogSaveTitleDataDictionary;
            saveFileDialog.Filter = Resources.FileDialogSaveFilterDataDictionary + "|*" + CommonConstants.ExtensionDataDictionary;
            saveFileDialog.FileName = defaultFilename;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Set-up the <c>SaveFileDialog</c> parameters associated with saving a simulated fault log to disk.
        /// </summary>
        /// <param name="defaultFilename">The default filename.</param>
        /// <param name="initialDirectory">The path of the initial directory that is to be displayed.</param>
        /// <returns>The fully qualified filename of the specified file or 'string.Empty' if no file is specified.</returns>
        public static string FileDialogSaveSimulatedFaultLog(string defaultFilename, string initialDirectory)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = initialDirectory;
            saveFileDialog.DefaultExt = CommonConstants.ExtensionSimulatedFaultLog.Remove(0, 1);
            saveFileDialog.Title = Resources.FileDialogSaveTitleSimulatedFaultLog;
            saveFileDialog.Filter = Resources.FileDialogSaveFilterSimulatedFaultLog + "|" + Parameter.ProjectInformation.ProjectIdentifier + "*" +
                                    CommonConstants.ExtensionSimulatedFaultLog;
            saveFileDialog.FileName = defaultFilename;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Set-up the <c>SaveFileDialog</c> parameters associated with saving a fault log to disk.
        /// </summary>
        /// <param name="defaultFilename">The default filename.</param>
        /// <param name="initialDirectory">The path of the initial directory that is to be displayed.</param>
        /// <returns>The fully qualified filename of the specified file or 'string.Empty' if no file is specified.</returns>
        public static string FileDialogSaveFaultLog(string defaultFilename, string initialDirectory)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = initialDirectory;
            saveFileDialog.DefaultExt = CommonConstants.ExtensionFaultLog.Remove(0, 1);
            saveFileDialog.Title = Resources.FileDialogSaveTitleFaultLog;
            saveFileDialog.Filter = Resources.FileDialogSaveFilterFaultLog + "|" + Parameter.ProjectInformation.ProjectIdentifier + "*" +
                                    CommonConstants.ExtensionFaultLog;
            saveFileDialog.FileName = defaultFilename;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Set-up the <c>SaveFileDialog</c> parameters associated with saving a recorded watch variable file to disk.
        /// </summary>
        /// <param name="defaultFilename">The default filename.</param>
        /// <param name="initialDirectory">The path of the initial directory that is to be displayed.</param>
        /// <returns>The fully qualified filename of the specified file or 'string.Empty' if no file is specified.</returns>
        public static string FileDialogSaveRecordedWatchFile(string defaultFilename, string initialDirectory)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = initialDirectory;
            saveFileDialog.DefaultExt = CommonConstants.ExtensionWatchFile.Remove(0, 1);
            saveFileDialog.Title = Resources.FileDialogSaveTitleRecordedWatchFile;
            saveFileDialog.Filter = Resources.FileDialogSaveFilterRecordedWatchFile + "|" + "*" + CommonConstants.ExtensionWatchFile;
            saveFileDialog.FileName = defaultFilename;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Set-up the <c>SaveFileDialog</c> parameters associated with saving an event log to disk.
        /// </summary>
        /// <param name="defaultFilename">The default filename.</param>
        /// <param name="initialDirectory">The path of the initial directory that is to be displayed.</param>
        /// <returns>The fully qualified filename of the specified file or 'string.Empty' if no file is specified.</returns>
        public static string FileDialogSaveEventLog(string defaultFilename, string initialDirectory)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = initialDirectory;
            saveFileDialog.DefaultExt = CommonConstants.ExtensionEventLog.Remove(0, 1);
            saveFileDialog.Title = Resources.FileDialogSaveTitleEventLog;
            saveFileDialog.Filter = Resources.FileDialogSaveFilterEventLog + "|" + "*" + CommonConstants.ExtensionEventLog;
            saveFileDialog.FileName = defaultFilename;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Set-up the <c>SaveFileDialog</c> parameters associated with saving a screen image.
        /// </summary>
        /// <param name="defaultFilename">The default filename.</param>
        /// <param name="initialDirectory">The path of the initial directory that is to be displayed.</param>
        /// <param name="imageFormat">The selected image format.</param>
        /// <returns>The fully qualified filename of the specified file or 'string.Empty' if no file is specified.</returns>
        public static string FileDialogSaveImageFile(string defaultFilename, string initialDirectory, out ImageFormat imageFormat)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = InitialDirectory.ScreenCaptureFilesWrite;
            saveFileDialog.DefaultExt = CommonConstants.ExtensionJPeg.Remove(0, 1);
            saveFileDialog.Title = Resources.FileDialogSaveTitleImageFile;
            saveFileDialog.Filter = Resources.FileDialogSaveFilterJPEG + "|" + "*" + CommonConstants.ExtensionJPeg + "|" + 
                                    Resources.FileDialogSaveFilterBMP + "|" + "*" + CommonConstants.ExtensionBmp;

            saveFileDialog.FileName = defaultFilename;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case FilterIndexJPeg:
                        imageFormat = ImageFormat.Jpeg;
						saveFileDialog.FileName = Path.ChangeExtension(saveFileDialog.FileName, CommonConstants.ExtensionJPeg);
                        break;
                    case FilterIndexBmp:
                        imageFormat = ImageFormat.Bmp;
						saveFileDialog.FileName = Path.ChangeExtension(saveFileDialog.FileName, CommonConstants.ExtensionBmp);
                        break;
                    default:
                        imageFormat = ImageFormat.Jpeg;
						saveFileDialog.FileName = Path.ChangeExtension(saveFileDialog.FileName, CommonConstants.ExtensionJPeg);
                        break;
                }

                return saveFileDialog.FileName;
            }
            else
            {
                imageFormat = ImageFormat.Jpeg;
                return string.Empty;
            }
        }
        #endregion - [FileDialog] -

        /// <summary>
        /// Get the fully qualified filename and the filename of the fault log associated with the specified event record.
        /// </summary>
        /// <remarks>
        /// Returns string.Empty if a stream has not been saved for the specified event record.
        /// </remarks>
        /// <param name="eventRecord">The event record.</param>
        /// <param name="defaultFilename">The default fault log filename.</param>
        /// <returns>The fully qualified filename of the fault log associated with the specified event record.</returns>
        public static string GetFullyQualifiedFaultLogFilename(EventRecord eventRecord, out string defaultFilename)
        {
            defaultFilename = string.Empty;
            string fullFilename = string.Empty;

            if (eventRecord.StreamSaved == true)
            {
                Debug.Assert(eventRecord.CarIdentifier != string.Empty,
                             "FormViewEventLog.GetFullyQualifiedFaultLogFilename() - [eventRecord.CarIdentifier != string.Empty]");
                Debug.Assert(eventRecord.Description != string.Empty,
                             "FormViewEventLog.GetFullyQualifiedFaultLogFilename() - [eventRecord.Description != string.Empty]");

                defaultFilename = General.DeriveName(eventRecord.CarIdentifier, eventRecord.DateTime, CommonConstants.ExtensionFaultLog,
                                                     eventRecord.Description);
                fullFilename = DirectoryManager.PathFaultLogs + CommonConstants.BindingFilename + defaultFilename;
                return fullFilename;
            }
            else
            {
                return fullFilename;
            }
        }

        /// <summary>
        /// Get the fully qualified filename of the fault log associated with the specified event record.
        /// </summary>
        /// <remarks>
        /// Returns string.Empty if a stream has not been saved for the specified event record.
        /// </remarks>
        /// <param name="eventRecord">The event record.</param>
        /// <returns>The fully qualified filename of the fault log associated with the specified event record.</returns>
        public static string GetFullyQualifiedFaultLogFilename(EventRecord eventRecord)
        {
            if (eventRecord.StreamSaved == true)
            {
                Debug.Assert(eventRecord.CarIdentifier != string.Empty,
                             "FormViewEventLog.GetFullyQualifiedFaultLogFilename() - [eventRecord.CarIdentifier != string.Empty]");
                Debug.Assert(eventRecord.Description != string.Empty,
                             "FormViewEventLog.GetFullyQualifiedFaultLogFilename() - [eventRecord.Description != string.Empty]");

                string defaultFilename = General.DeriveName(eventRecord.CarIdentifier, eventRecord.DateTime, CommonConstants.ExtensionFaultLog,
                                                            eventRecord.Description);
                string fullFilename = DirectoryManager.PathFaultLogs + CommonConstants.BindingFilename + defaultFilename;
                return fullFilename;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Derive the default filename for a for a file based upon the specified parameters. The format of the standard filename is as follows:
        /// 'project-ID [car-identifier] [description] YYMMDD-HHmmss.extension'. This method was extended as part of purchase order 4800010525-CU2/19.03.2015 to 
        /// include support for the NYCT filename format.
        /// </summary>
        /// <param name="carIdentifier">The car identifier associated with the event.</param>
        /// <param name="dateTime">The time stamp associated with file.</param>
        /// <param name="extension">The file extension including the period e.g. .xml.</param>
        /// <param name="description">The description that is to be added to the file.</param>
        /// <returns>The derived default filename of the file.</returns>
        public static string DeriveName(string carIdentifier, DateTime dateTime, string extension, string description)
        {
            // The StringBuilder class is more efficient than the &= operator for concatenating many strings.
            StringBuilder stringBuilder = new StringBuilder();

            // The derived name of the file.
            string saveName = string.Empty;

            // The formatting string associated with the StringBuilder.Append() method.
            string format = string.Empty;

            // Rev. 1.14. The naming convention is now project specific.
            switch (Parameter.ProjectInformation.ProjectIdentifier)
            {
                case CommonConstants.ProjectIdNYCT:

                    // ------------------------------------------------------------------------------------------------
                    // Generate the name based upon section 5.1.4. of the Kawasaki PTE Uniform Interface Specification.
                    // LOG_ (Only applicable to event logs)
                    // <Car#>_
                    // <subsystem-code>_    (defined in Table 8 of the Kawasaki specification)
                    // <location-code>_     (defined by the supplier>
                    // <file-type>_         (defined by the supplier)
                    // <MM-DD-YYYY>_        (date when log data was uploaded from the local equipment)
                    // <HH-MM-SS>_          (timetime when the log data was uploaded from the local equipment)
                    // <supplier-data>      (if needed)
                    // .<extension>
                    // ------------------------------------------------------------------------------------------------

                    // Derive the 'Log Type' field of the filename based upon the extension. As this method is also used to process image files, the name of the
                    // local variable is fileType rather than logType.
                    string fileType = string.Empty;
                    switch (extension)
                    {
                        case CommonConstants.ExtensionWatchFile:
                            fileType = LogType.Watch.ToString().ToUpper();
                            break;
                        case CommonConstants.ExtensionSimulatedFaultLog:
                            fileType = LogType.SimulatedDataStream.ToString().ToUpper();
                            break;
                        case CommonConstants.ExtensionFaultLog:
                            fileType = LogType.DataStream.ToString().ToUpper();
                            break;
                        case CommonConstants.ExtensionEventLog:
                            // If the file is an event log prepend the name with "LOG_".
                            stringBuilder.AppendFormat(LogIdentifier.ToUpper() + CommonConstants.UnderScore);
                            fileType = LogType.Event.ToString().ToUpper();
                            break;
                        default:
                            // If the file is none of the above it is assumed that the file is an image file. The extension associated with image files take the
                            // form '.<ScreenCatureType>.jpg' or '.<ScreenCatureType>.bmp'.

                            // Break the extension down into its constituent components.
                            string[] delimiters = new string[] { CommonConstants.Period };
                            string[] extensionComponents = extension.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                            string screenCaptureType = extensionComponents[0];
                            extension = CommonConstants.Period + extensionComponents[extensionComponents.Length - 1];
                            fileType = ImageIdentifier.ToUpper();

                            // If the description parameter is empty replay it with the screen capture type (WATCH, EVENT, PLOT) of the image.
                            if (description.Equals(string.Empty))
                            {
                                description = screenCaptureType.ToUpper();
                            }
                            break;
                    }

                    if (description.Equals(string.Empty))
                    {
                        format = carIdentifier + CommonConstants.UnderScore +
                                 Subsystem + CommonConstants.UnderScore +
                                 fileType + CommonConstants.UnderScore +
                                 "{0:D2}-{1:D2}-{2:D2}_{3:D2}-{4:D2}-{5:D2}";
                    }
                    else
                    {
                        format = carIdentifier + CommonConstants.UnderScore +
                                 Subsystem + CommonConstants.UnderScore +
                                 fileType + CommonConstants.UnderScore +
                                 "{0:D2}-{1:D2}-{2:D2}_{3:D2}-{4:D2}-{5:D2}" + CommonConstants.UnderScore +
                                 description.ToUpper();
                    }

                    stringBuilder.AppendFormat(format, dateTime.Month, dateTime.Day, dateTime.Year, dateTime.Hour, dateTime.Minute, dateTime.Second);
                    break;
                default:
                    // Generate the name based upon the specified dateTime - '<project-ID> [<car-identifier>] [<description>] YYMMDD-HHmmss.<extension>'.
                    if (description.Equals(string.Empty))
                    {
                        format = FileHeader.HeaderCurrent.ProjectInformation.ProjectIdentifier + CommonConstants.Space +
                                 "[" + carIdentifier + "]" + CommonConstants.Space +
                                 "{0:D2}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}";
                    }
                    else
                    {
                        format = FileHeader.HeaderCurrent.ProjectInformation.ProjectIdentifier + CommonConstants.Space + 
                                 "[" + carIdentifier + "]" + CommonConstants.Space +
                                 "[" + description + "]" + CommonConstants.Space +
                                 "{0:D2}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}";
                    }

                    stringBuilder.AppendFormat(format, dateTime.Year % CommonConstants.Century, dateTime.Month, dateTime.Day,
                                               dateTime.Hour, dateTime.Minute, dateTime.Second);
                    break;
            }
            saveName = stringBuilder.ToString() + extension;
            return saveName;
        }

        /// <summary>
        /// Get the ToolStripItemCollection associated with a ToolStrip.
        /// </summary>
        /// <param name="toolStrip">The toolstrip for which the collection is to be made.</param>
        /// <returns>The items contained within the specified toolstrip.</returns>
        public static ToolStripItemCollection GetToolStripItemCollection(ToolStrip toolStrip)
        {
            ToolStripItemCollection toolStripItemCollection;

            ToolStripItem[] toolstripItems = new ToolStripItem[toolStrip.Items.Count];
            for (int index = 0; index < toolStrip.Items.Count; index++)
            {
                toolstripItems[index] = toolStrip.Items[index];
            }
            toolStripItemCollection = new ToolStripItemCollection(toolStrip, toolstripItems);
            return toolStripItemCollection;
        }

        /// <summary>
        /// Ask the user to specify an engineering data dictionary (.e1) and a project PTU configuration database (.e1) and convert them to a
        /// single XML file.
        /// </summary>
        public static void ConvertEngineeringDatabaseToXML()
        {
            // Ask the user to specify the engineering data dictionary database.
            string fullQualifiedEngineeringDatabaseFilename = General.FileDialogOpenFile(Resources.FileDialogOpenTitleEngineeringDatabase,
                                                                                         CommonConstants.ExtensionEngineeringDatabase,
                                                                                         Resources.FileDialogOpenFilterEngineeringDatabase,
                                                                                         InitialDirectory.E1FilesRead);

            // Skip, if the user didn't specify an engineering data dictionary database.
            if (fullQualifiedEngineeringDatabaseFilename == string.Empty)
            {
                return;
            }

            // Update the initial directory with the path of the selected file.
            InitialDirectory.E1FilesRead = Path.GetDirectoryName(fullQualifiedEngineeringDatabaseFilename);

            // Ask the user to specify the project PTU configuration database.
            string fullQualifiedProjectConfigurationDatabaseFilename = General.FileDialogOpenFile(Resources.FileDialogOpenTitleProjectConfigurationDatabase,
                                                                                                  CommonConstants.ExtensionEngineeringDatabase,
                                                                                                  Resources.FileDialogOpenFilterEngineeringDatabase,
                                                                                                  InitialDirectory.E1FilesRead);

            // Skip, if the user didn't specify a project PTU configuration database.
            if (fullQualifiedProjectConfigurationDatabaseFilename == string.Empty)
            {
                return;
            }

            // Update the initial directory with the path of the selected file.
            InitialDirectory.E1FilesRead = Path.GetDirectoryName(fullQualifiedProjectConfigurationDatabaseFilename);

            // Generate a default output filename for the xml file by replacing the database extension (.e1) with the xml extension for the engineering data dictionary
            // database.
            string defaultDataDictionaryFilename = Path.GetFileName(Path.ChangeExtension(fullQualifiedEngineeringDatabaseFilename,
                                                                    CommonConstants.ExtensionDataDictionary));

            // Create an empty OleDbConnection object for the engineering data dictionary database connection.
            using (OleDbConnection oleDbConnection = new OleDbConnection())
            {
                // And another for the project PTU configuration database connection.
                using (OleDbConnection oleDbPTUConnection = new OleDbConnection())
                {
                    // Configure the OleDbConnection connection strings for the engineering data dictionary database and the project PTU configuration database.
                    oleDbConnection.ConnectionString = Resources.ConnectionStringDataDictionary + fullQualifiedEngineeringDatabaseFilename + ";";
                    oleDbPTUConnection.ConnectionString = Resources.ConnectionStringDataDictionary + fullQualifiedProjectConfigurationDatabaseFilename + ";";

                    try
                    {
                        // Open the connections to the specified data dictionary database and the project PTU configuration database.
                        oleDbConnection.Open();
                        oleDbPTUConnection.Open();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Ask the user to specify the name of the XML file that is to be exported, offering the default filename generated above.
                    string fullyQualifiedDataDictionaryFilename = General.FileDialogSaveDataDictionary(defaultDataDictionaryFilename,
                                                                                                       DirectoryManager.PathPTUConfigurationFiles);

                    // Skip, if the user didn't specify an output filename.
                    if (fullyQualifiedDataDictionaryFilename == string.Empty)
                    {
                        return;
                    }

                    // Check whether both connections are open.
                    if ((oleDbConnection.State == ConnectionState.Open) && (oleDbPTUConnection.State == ConnectionState.Open))
                    {
                        try
                        {
                            // Write the DataSet generated from the selected access database and the project PTU database to the specified XML file.
                            DataDictionary.WriteDataSetToXml(oleDbPTUConnection, oleDbConnection, fullyQualifiedDataDictionaryFilename);
                            MessageBox.Show(Resources.MBTSuccessDataDictionaryCreate, Resources.MBCaptionInformation, MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            oleDbConnection.Close();
                            oleDbPTUConnection.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(Resources.MBTDatabaseConnectionFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Ask the user to select an XML data dictionary and then load this data dictionary into the PTU application.
        /// </summary>
        /// <param name="mainWindow">Reference to the main application window interface.</param>
        public static void LoadDataDictionary(IMainWindow mainWindow)
        {
            string fullyQualifiedSourceFilename = General.FileDialogOpenFile(Resources.FileDialogOpenTitleDataDictionary,
                                                                             CommonConstants.ExtensionDataDictionary, 
                                                                             Resources.FileDialogOpenFilterDataDictionary,
                                                                             DirectoryManager.PathPTUConfigurationFiles);
            // Skip, if the user didn't select a data dictionary XML file.
            if (fullyQualifiedSourceFilename == string.Empty)
            {
                return;
            }

            #region - [Exclude 'PTU Configuration.xml' or '*.PTU Configuration.xml'] -
            // if the user has selected either the default configuration file, 'PTU Configuration.xml',  or one of the project default configuration files,
            // '<project-identifier>.PTU Configuration.xml', terminate the operation.
            if (Path.GetFileName(fullyQualifiedSourceFilename).ToLower().Contains(Resources.FilenameDefaultDataDictionary.ToLower()))
			{
                MessageBox.Show(string.Format(Resources.MBTConfigSelectionInvalid, Resources.FilenameDefaultDataDictionary), Resources.MBCaptionError,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
            }
            #endregion - [Exclude 'PTU Configuration.xml' or '*.PTU Configuration.xml'] -

            FileInfo fileInfoSource = new FileInfo(fullyQualifiedSourceFilename);
            DataDictionary dataDictionary = new DataDictionary();
            try
            {
                // Load the specified XML configuration file.
                FileHandling.LoadDataSet<DataDictionary>(fullyQualifiedSourceFilename, ref dataDictionary);

                #region - [Check whether the selected data dictionary is valid for the current project] -
                // If the project identifier was passed to the application as a desktop shortcut parameter, ensure that the project identifier associated with
                // the selected data dictionary matches this, and if not, terminate the operation.
                if (mainWindow.ProjectIdentifierPassedAsParameter.Equals(string.Empty))
                {
                    // Do nothing. It is perfectly acceptable to select the configuration file associated with ANY project if no project identifier was passed to the
                    // application as the desktop shortcut parameter. Indeed, this is the recommended way of quickly changing between different projects
                    // for Bombardier field engineers. Simply set up a desktop shortcut that points to the PTU application but do not supply any shortcuts.
                    ;
                }
                else
                {
                    // The project identifier was passed to the application as a desktop shortcut parameter. Check that the project identifier associated with the
                    // selected configuration file matches this and, if not, terminate the operation.
                    if (dataDictionary.FILEINFO[0].PROJECTSTRING != mainWindow.ProjectIdentifierPassedAsParameter)
                    {
                        // The data dictionary is not associated with the current project, terminate the operation.
                        MessageBox.Show(string.Format(Resources.MBTConfigProjectAsParameterMismatch, mainWindow.ProjectIdentifierPassedAsParameter),
                                        Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                #endregion - [Check whether the selected data dictionary is valid for the current project] -

                // Update the appropriate default configuration file.\
                string fullyQualifiedDestinationFilename;
                if (mainWindow.ProjectIdentifierPassedAsParameter.Equals(string.Empty))
                {
                    // Copy to the default configuration file.
                    fullyQualifiedDestinationFilename = DirectoryManager.PathPTUConfigurationFiles + CommonConstants.BindingFilename +
                                                        Resources.FilenameDefaultDataDictionary;
                }
                else
                {
                    // Copy to the default project configuration file.
                    fullyQualifiedDestinationFilename = DirectoryManager.PathPTUConfigurationFiles + CommonConstants.BindingFilename +
                                                        dataDictionary.FILEINFO[0].PROJECTSTRING + CommonConstants.Period + Resources.FilenameDefaultDataDictionary;
                }
                
                FileInfo fileInfoDestination = new FileInfo(fullyQualifiedDestinationFilename);
                fileInfoSource.CopyTo(fullyQualifiedDestinationFilename, true);

                if (mainWindow != null)
                {
                    mainWindow.SetRestart(true);
                    mainWindow.Close();
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show(Resources.MBTConfigurationFileLoadFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get the <c>Username</c> of the current user.
        /// </summary>
        /// <returns>The <c>Username</c> of the current user.</returns>
        public static string GetUsername()
        {
            // Get the current user's username.
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            // The delimiters between the machine name and the name of the user.
            string[] delimiters = new string[] { "\\" };

            // Get the name of the current user. The Name property includes the machine name, split this up using the delimiters to get the user name
            // excluding the machine name.
            string[] identityName = identity.Name.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string windowsUsername = identityName[identityName.Length - 1];
            return windowsUsername;
        }

        #region - [NumericUpDown] -
        /// <summary>
        /// Get the number of decimal places corresponding to the specified watch variable scale-factor. This can be used to program a numeric up/down
        /// control or specify the format string used to display the variable.
        /// </summary>
        /// <param name="scaleFactor">The scale-factor associated with the watch variable.</param>
        /// <param name="decimalPlaces">The number of decimal places to be used when displaying the value.</param>
        public static void GetDecimalPlaces(double scaleFactor, out int decimalPlaces)
        {
            if ((scaleFactor > 1) && (scaleFactor <= 10))
            {
                decimalPlaces = 0;
            }
            else if ((scaleFactor > 0.1) && (scaleFactor <= 1))
            {
                decimalPlaces = 0;
            }
            else if ((scaleFactor > 0.01) && (scaleFactor <= 0.1))
            {
                decimalPlaces = 1;
            }
            else if ((scaleFactor > 0.001) && (scaleFactor <= 0.01))
            {
                decimalPlaces = 2;
            }
            else if ((scaleFactor > 0.0001) && (scaleFactor <= 0.001))
            {
                decimalPlaces = 3;
            }
            else if ((scaleFactor > 0.00001) && (scaleFactor <= 0.0001))
            {
                decimalPlaces = 4;
            }
            else
            {
                decimalPlaces = 5;
            }
        }

        /// <summary>
        /// Get the numeric up/down control increment value corresponding to the specified watch variable scale-factor.
        /// </summary>
        /// <param name="scaleFactor">The scale-factor associated with the watch variable.</param>
        /// <param name="increment">The increment value corresponding to the specified scale-factor.</param>
        public static void GetIncrement(double scaleFactor, out decimal increment)
        {
            if ((scaleFactor > 1) && (scaleFactor <= 10))
            {
                increment = (decimal)1;
            }
            else if ((scaleFactor > 0.1) && (scaleFactor <= 1))
            {
                increment = (decimal)1;
            }
            else if ((scaleFactor > 0.01) && (scaleFactor <= 0.1))
            {
                increment = (decimal)0.1;
            }
            else if ((scaleFactor > 0.001) && (scaleFactor <= 0.01))
            {
                increment = (decimal)0.01;
            }
            else if ((scaleFactor > 0.0001) && (scaleFactor <= 0.001))
            {
                increment = (decimal)0.001;
            }
            else if ((scaleFactor > 0.00001) && (scaleFactor <= 0.0001))
            {
                increment = (decimal)0.0001;
            }
            else
            {
                increment = (decimal)0.00001;
            }
        }
        #endregion - [NumericUpDown] -

        #region - [Menu] -
        /// <summary>
        /// Sets the <c>Visible</c> property of ALL <c>ToolStripMenuItem</c> items associated with the specified <c>MenuStrip</c> to the specified
        /// state. 
        /// </summary>
        /// <param name="menuStrip">The <c>MenuStrip</c>.</param>
        /// <param name="state">The required state of the <c>Visible</c> property.</param>
        public static void SetMenuStripVisible(MenuStrip menuStrip, bool state)
        {
            // Set the Visible property of ALL menu options associated with the specified menu strip to the specified state. 
            for (int index = 0; index < menuStrip.Items.Count; index++)
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)menuStrip.Items[index];
                SetToolStripMenuItemVisible(menuItem, state);
            }
        }

        /// <summary>
        /// Sets the <c>Visible</c> property of ALL <c>ToolStripMenuItem</c> items associated with the specified <c>ToolStripMenuItem</c> to the
        /// specified state.
        /// </summary>
        /// <param name="toolStripMenuItem">The <c>ToolStripMenuItem</c>.</param>
        /// <param name="state">The required state of the <c>Visible</c> property.</param>
        public static void SetToolStripMenuItemVisible(ToolStripMenuItem toolStripMenuItem, bool state)
        {
            // Set the Visible property of the ToolStripMenuItem to the specified state.
            toolStripMenuItem.Visible = state;

            // Continue on down the collection until no more sub-menu items are found.
            for (int index = 0; index < toolStripMenuItem.DropDown.Items.Count; index++)
            {
                // Filter out menu seperators.
                ToolStripItem toolStripItem = toolStripMenuItem.DropDown.Items[index];
                if (toolStripItem.GetType() == typeof(ToolStripMenuItem))
                {
                    toolStripMenuItem.DropDown.Items[index].Visible = state;
                    // Recursive, keep going down the list until no more dropdown menu options are found.
                    ToolStripMenuItem menuItemChild = ((ToolStripMenuItem)toolStripMenuItem.DropDown.Items[index]);
                    SetToolStripMenuItemVisible(menuItemChild, state);
                }
                else if (toolStripItem.GetType() == typeof(ToolStripSeparator))
                {
                    // Ensure that the separator is visible.
                    toolStripItem.Visible = state;
                }
            }
        }

        /// <summary>
        /// Sets the <c>Enabled</c> property of ALL <c>ToolStripMenuItem</c> items associated with the specified <c>MenuStrip</c> to the specified
        /// state. 
        /// </summary>
        /// <param name="menuStrip">The <c>MenuStrip</c>.</param>
        /// <param name="state">The required state of the <c>Enabled</c> property of ALL <c>ToolStripMenuItem</c> items associated with the specified
        /// <c>MenuStrip</c>.</param>
        public static void SetMenuStripEnabled(MenuStrip menuStrip, bool state)
        {
            // Enable/Disable ALL menu options associated with the specified menu strip. 
            for (int index = 0; index < menuStrip.Items.Count; index++)
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)menuStrip.Items[index];
                SetToolStripMenuItemEnabled(menuItem, state);
            }
        }

        /// <summary>
        /// Sets the <c>Enabled</c> property of ALL <c>ToolStripMenuItem</c> items associated with the specified <c>ToolStripMenuItem</c> to the
        /// specified state.
        /// </summary>
        /// <param name="toolStripMenuItem">The <c>ToolStripMenuItem</c>.</param>
        /// <param name="state">The required state of the <c>Enabled</c> property of ALL <c>ToolStripMenuItem</c> items associated with the specified
        /// <c>ToolStripMenuItem</c>.</param>
        public static void SetToolStripMenuItemEnabled(ToolStripMenuItem toolStripMenuItem, bool state)
        {
            // Set the Enabled property of the tool strip menu item to the state of the enabled parameter.
            toolStripMenuItem.Enabled = state;

            // Continue on down the collection until no more sub-menu items are found.
            for (int index = 0; index < toolStripMenuItem.DropDown.Items.Count; index++)
            {
                // Filter out menu seperators.
                ToolStripItem toolStripItem = toolStripMenuItem.DropDown.Items[index];
                if (toolStripItem.GetType() == typeof(ToolStripMenuItem))
                {
                    toolStripMenuItem.DropDown.Items[index].Enabled = state;
                    // Recursive, keep going down the list until no more dropdown menu options are found.
                    ToolStripMenuItem menuItemChild = ((ToolStripMenuItem)toolStripMenuItem.DropDown.Items[index]);
                    SetToolStripMenuItemEnabled(menuItemChild, state);
                }
            }
        }

        /// <summary>
        /// Searches the specified <c>MenuStrip</c> and returns the <c>ToolStripMenuItem</c> associated with the specified key.
        /// </summary>
        /// <param name="menuStrip">The <c>MenuStrip</c> that is to be searched.</param>
        /// <param name="key">The key used to access the <c>ToolStripMenuItem</c> i.e. the value of the <c>Name</c> property.</param>
        /// <returns>The <c>ToolStripMenuItem</c> associated with the specified key, if found; otherwise, null.</returns>
        public static ToolStripMenuItem GetToolStripMenuItem(MenuStrip menuStrip, string key)
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            for (int index = 0; index < menuStrip.Items.Count; index++)
            {
                toolStripMenuItem = menuStrip.Items[index] as ToolStripMenuItem;
                if (toolStripMenuItem.Name == key)
                {
                    return toolStripMenuItem;
                }
                else
                {
                    toolStripMenuItem = GetToolStripMenuItem(toolStripMenuItem, key);
                    if (toolStripMenuItem != null)
                    {
                        return toolStripMenuItem;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Searches ALL <c>ToolStripMenuItem</c> controls associated with the specified <c>ToolStripMenuItem</c> and returns the
        /// <c>ToolStripMenuItem</c> corresponding to the specified key.
        /// </summary>
        /// <param name="toolStripMenuItem">The <c>ToolStripMenuItem</c> this is to be searched.</param>
        /// <param name="key">The key used to access the <c>ToolStripMenuItem</c> i.e. the value of the <c>Name</c> property.</param>
        /// <returns>The <c>ToolStripMenuItem</c> associated with the specified key, if found; otherwise, null.</returns>
        public static ToolStripMenuItem GetToolStripMenuItem(ToolStripMenuItem toolStripMenuItem, string key)
        {
            ToolStripMenuItem childToolStripMenuItem;
            if (toolStripMenuItem.Name == key)
            {
                return toolStripMenuItem;
            }
            else
            {
                // Continue on down the collection until no more sub-menu items are found.
                for (int index = 0; index < toolStripMenuItem.DropDown.Items.Count; index++)
                {
                    // Filter out menu seperators.
                    ToolStripItem toolStripItem = toolStripMenuItem.DropDown.Items[index];
                    if (toolStripItem.GetType() == typeof(ToolStripMenuItem))
                    {
                        childToolStripMenuItem = toolStripItem as ToolStripMenuItem;
                        if (childToolStripMenuItem.Name == key)
                        {
                            return childToolStripMenuItem;
                        }
                        else
                        {
                            if (childToolStripMenuItem.DropDownItems.Count > 0)
                            {
                                childToolStripMenuItem = GetToolStripMenuItem(childToolStripMenuItem as ToolStripMenuItem, key);
                                if (childToolStripMenuItem != null)
                                {
                                    return childToolStripMenuItem;
                                }
                            }
                        }
                    }
                }
                return null;
            }
        }
        #endregion - [Menu] -
        #endregion --- Methods ---
    }
}
