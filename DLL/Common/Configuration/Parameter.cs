#region --- Revision History ---
/*
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Configuration
 * 
 *  File name:  Parameters.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/23/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  10/11/10    1.1     K.McD           1.  Modified the default interval between successive frames of the recorded data files for the following: (a)
 *                                          recorded watch files; (b) simulated fault logs and (c) fault logs to 240ms, 240ms and 60ms respectively.
 * 
 *  11/16/10    1.2     K.McD           1.  Removed the DataStreamPlotParameters structure.
 *                                      2.  Removed the FaultLog and SimulatedFaultLog properties.
 *                                      3.  Added the InvalidDateTime property.
 *                                      4.  Changed a number of int variables to short variables.
 *                                      5.  Modified a number of variable names and XML tags.
 *                                      6.  Removed the InitializeDataStreamParameters() method.
 * 
 *  11/26/10    1.3     K.McD           1.  Added the WatchSizeFaultLog property.
 *                                      2.  Modified a number of constants to be of type short rather than int.
 * 
 *  01/10/11    1.4     K.McD           1.  Renamed the InstructionUseDefault resource to PathUseDefault to be consistent with the resource name used
 *                                          in the PTU application.
 * 
 *  01/18/11    1.5     K.McD           1.  SNCR001.76. Added the StreamNumberMax property to allow the programmer to determine the maximum number of
 *                                          data streams that FormViewEventLog class supportsi.e. to ensure that it wraps around back to zero on the
 *                                          next increment when the StreamNumber value reaches StreamNumberMax - 1.
 * 
 *                                      2.  SNCR001.42. Modified the default font to be 'Trebuchet MS' 8pt as this is available on the basic
 *                                          installation of Windows XP.
 *
 *                                      3.  SNCR001.87. Modified the value of the DefaultWatchSizeFaultLog constant to 20 to cater for the number of
 *                                          data stream parameters associated with the Propulsion, Engineering and Snapshot event logs on the CTA
 *                                          project.
 * 
 *  01/26/11    1.6     K.McD           1.  Added support to allow the IntervalDataStreamMs, PathPTUApplicationData and WatchSize properties to be
 *                                          configured from the data dictionary.
 *                                      2.  Added support to allow the fault log data streams to be configured from the data dictionary.
 *                                      3.  Removed the WatchSizeFaultLog and StreamNumberMax and PageDuration properties and made these constant
 *                                          values.
 * 
 *  02/28/11    1.7     K.McD           1.  Removed the: DefaultDataStreamCount, DefaultDataStreamTypeDescription, DefaultDataStreamTypeParameterCount
 *                                          and DefaultDataStreamTypeSampleCount constants as these are no longer used. Where required, the values are
 *                                          now obtained from calls to GetDefaultStreamInformation(), GetStreamInformation() and ChangeEventLog()
 *                                          methods of the PTUDLL32 dynamic link library.
 * 
 *                                      2.  Removed the IntervalDataStreamMs property as this is no longer used. This value is now obtained from a
 *                                          call to the ChangeEventLog() method of the PTUDLL32 dynamic link library.
 * 
 *  03/18/11    1.8     K.McD           1.  Included support for the Security table of the data dictionary.
 *                                      2.  Refactored the code and added the SetToDefault() and Initialize() static methods.
 * 
 *  04/27/11    1.9     K.McD           1.  Added a constant to define the number of chart recorder channels -  WatchSizeChartRecorder.
 *  
 *  06/22/11    1.10    K.McD           1.  Added a number of public constants associated with the self test sub-system.
 *  
 *  07/07/11    1.10.1  K.McD           1.  Modified the maximum number of self test variables that can be supported by each interactive tests.
 *  
 *  07/20/11    1.10.2  K.McD           1.  Added the value information associated with the enumerator/constant to a number of XML tags.
 *  
 *  08/10/11    1.10.3  K.McD           1.  Set the default font to be 'Segoe UI'.
 *  
 *  09/12/11	1.10.4	Sean.D			1.	Added the m_URIList and m_CommunicationProtocol member variables. These are initialized from fields in the
 *                                          data dictionary and (a) record the list of URIs that are to be checked and (b) specify whether the
 *                                          the: serial, ethernet or both communication ports, are to be checked.
 *											
 *	09/21/11	1.10.5	Sean.D			1.	Added code to attempt to set the number of watch variables in PTUDLL32 based upon the WatchSize field of
 *	                                        the ConfigurePTU table of the data dictionary.
 *	
 *  10/02/11    1.10.6  K.McD           1.  Minor changes to ensure compliance with the coding standard.
 *                                      2.  Changed the default CommunicationTypeEnum to RS232.
 *                                      
 *  10/28/11    1.10.7  K.McD           1.  Changed the CommunicationTypeEnum definition to be: 0 - RS232, 1 - TCPIP, 2 - Both.
 *  
 *  06/19/13    1.10.8  K.McD           1.  Added support for the WibuBox security device.
 *  
 *  07/22/13    1.11    K.McD           1.  Removed the WatchSizeFaultLogMax constant.
 *                                      2.  Added support for the WatchSizeFaultLog and SupportsMultipleDataStreamTypes properties.
 *                                      3.  Added the DefaultDataStreamTypeWatchVariablesMax constant.
 *                                      
 *  08/05/13    1.11.1  K.McD           1.  Added support to allow different default fonts to be specified for Windows XP and Windows 7.
 *                                              1.  Replaced the constants DefaultFontFamily and DefaultFontSize with DefaultFontFamilyXP and
 *                                                  DefaultFontSizeXP respectively.
 *                                              2.  Added the constants DefaultFontFamilyWin7 and DefaultFontSizeWin7.
 *                                              3.  Set the default font and sizes to be Tahoma - 8pt for Windows XP and Segoe UI - 9pt for Windows 7.
 *                                              
 *  03/11/15    1.12    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  Implement changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                              to support both 32 and 64 bit architecture.
 *                                              
 *                                          2.  Changes to allow the PTU to handle both 2 and 4 character year codes.
 *                                              
 *                                      2.  SNCR - R188 PTU [20 Mar 2015] item 1. When trying to build version 1.19 of Common.dll the following
 *                                          error message was displayed "Error 4 'Common.Configuration.DataDictionary' does not contain a definition
 *                                          for 'LOG' and no extension method 'LOG' accepting a first argument of type
 *                                          'Common.Configuration.DataDictionary' could be found (are you missing a using directive or an assembly
 *                                          reference?)".
 *                                      
 *                                      Modifications
 *                                      1.  Ref.: 1.1. Rather than accessing the PTUDLL32.SetWatchSize() method directly, the Initialize() method
 *                                          was modified to instantiate a new CommunicationParent object and use the SetWatchSize() method to set
 *                                          the number of watch variables associated with the VCU.
 *                                          
 *                                      2.  Ref.: 1.2. Added the YearCodeSize property and associated member variables/constants to allow the
 *                                          application to support Vehicle Control Units that use both 2 digit and 4 digit year codes.
 *                                          
 *                                      3.  Ref.: 2. Changed the LOG back to LOGS in the dataDictionary.LOGS[recordIndex].DataStreamTypeIdentifier
 *                                          index reference in the Initialize() method as it had been accidentally changed while changing the
 *                                          layout to 150 characters per row.
 *                                          
 *  03/21/15    1.13    K.McD           References
 *                                      1.  A FunctionFlags bitmask field is to be added to the  CONFIGUREPTU table. This field is a bitmask that specifies the function
 *                                          options that are required for the current project. So far, the following flags have been defined:
 *
 *                                          Bit 0   -   Use4DigitYearCode - Flag to specify whether the project VCU uses a 2 or 4 digit year code. True, if the project
 *                                                      uses a 4 digit year code; otherwise, false.
 *                                          Bit 1   -   ShowEventLogType - Flag to specify whether the event log type field is to be shown when saved event logs are
 *                                                      displayed. True, if the log type is to be displayed; otherwise, false.
 *                                          .
 *                                          .
 *                                          .
 *                                          Bit 7   -   Not Used.
 *                                          
 *                                      Modifications
 *                                      1.  Added support for the following member variables/properties: bool: Use4DigitYearCode, bool: ShowLogName, short: FunctionFlags.
 *                                          These are derived from the newly created FunctionFlags field of the CONFIGUREPTU table in the project PTU configuration
 *                                          database.
 *                                      2.  Removed the following member variables/properties: short: YearCodeSize.
 *                                              
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;
using System.Collections.Generic;

using Common.Communication;
using Common.Properties;

namespace Common.Configuration
{
    /// <summary>
    /// Class to manage the PTU configurable parameters.
    /// </summary>
    public static class Parameter
    {
        #region --- Constants ---
        /// <summary>
        /// The number of year code digits associated with the long year code format. Value: 4.
        /// </summary>
        private const short YearCodeSizeLong = 4;

        /// <summary>
        /// The number of year code digits associated with the short year code format. Value: 2.
        /// </summary>
        private const short YearCodeSizeShort = 2;

        /// <summary>
        /// The number of seconds in a minute. Value: 60.
        /// </summary>
        private const short SecondsPerMinute = 60;

        /// <summary>
        ///  The number of watch variables supported by the chart recorder. Value: 8.
        /// </summary>
        public const short WatchSizeChartRecorder = 8;

        /// <summary>
        /// The number of self tests that can be defined in a test list. Value: 40.
        /// </summary>
        public const short SizeTestList = 40;

        /// <summary>
        /// The number of self test variables that can be supported by each interactive self test. Value: 16.
        /// </summary>
        public const short WatchSizeInteractiveTest = 16;

        /// <summary>
        /// The default interval, in ms, between successive polls to the VCU for watch variable data. Value: 240ms.
        /// </summary>
        public const short IntervalWatchMs = 240;

        /// <summary>
        /// The duration, in minutes, of a single page worth of recorded data. Value: 30 minutes.
        /// </summary>
        public const short DurationPageMinutes = 30;

        #region - [Simulated Fault Log] -
        /// <summary>
        /// The total duration, in ms, of the simulated fault log data-stream. Value: 20,000 ms.
        /// </summary>
        public const short DurationMsSimulatedFaultLog = 20000;

        /// <summary>
        /// The duration, in ms, of the simulated fault log data stream following the trip. VAlue: 5,000 ms.
        /// </summary>
        public const short DurationPostTripMsSimulatedFaultLog = 5000;
        #endregion - [Simulated Fault Log] -

        #region - [DisplayArea] -
        /// <summary>
        /// The padding to be applied to the top of the <c>Screen.PrimaryScreen.WorkingArea</c>.
        /// </summary>
        private const int PaddingTopWorkingArea = 30;

        /// <summary>
        /// The padding to be applied to the bottom of the <c>Screen.PrimaryScreen.WorkingArea</c>.
        /// </summary>
        private const int PaddingBottomWorkingArea = 30;

        /// <summary>
        /// The combined top and bottom padding.
        /// </summary>
        private const int PaddingVerticalWorkingArea = 60;

        /// <summary>
        /// The padding to be applied to the left of the <c>Screen.PrimaryScreen.WorkingArea</c>.
        /// </summary>
        private const int PaddingLeftWorkingArea = 30;

        /// <summary>
        /// The padding to be applied to the right of the <c>Screen.PrimaryScreen.WorkingArea</c>.
        /// </summary>
        private const int PaddingRightWorkingArea = 30;

        /// <summary>
        /// The combined left and right padding.
        /// </summary>
        private const int PaddingHorizontalWorkingArea = 60;
        #endregion - [DisplayArea] -

		/// <summary>
		/// An enumeration tracking the type of PTU communication being done
		/// </summary>
		public enum CommunicationTypeEnum
		{
			/// <summary>
			/// Standard RS232 TCMS protocol. Value: 1.
			/// </summary>
			RS232 = 0,

			/// <summary>
			/// TCPIP protocol. Value: 2.
			/// </summary>
			TCPIP = 1,

            /// <summary>
			/// Both TCPIP and RS232 TCMS protocol. Value: 0.
			/// </summary>
			Both = 2
		}

        #region - [Default Values] -
        #region - [DataStreamType] -
        /// <summary>
        /// The identifier associated with the default data stream type. Value: 0.
        /// </summary>
        public const short DefaultDataStreamTypeIdentifier = 0;

        /// <summary>
        /// The sample index corresponding to the time of the trip that is associated with the default data stream type. Value: 75.
        /// </summary>
        public const int DefaultDataStreamTypeTripIndex = 75;

        /// <summary>
        /// The number of recorded watch variables associated with the default data stream type. Value: 8.
        /// </summary>
        public const short DefaultDataStreamTypeWatchVariablesMax = 8;
        #endregion - [DataStreamType] -

        /// <summary>
        /// The default <c>YearCodeSize</c>. Value: 2.
        /// </summary>
        private const short DefaultYearCodeSize = 2;

        /// <summary>
        /// The default <c>WatchSize</c> supported by the target hardware. Value: 20.
        /// </summary>
        /// <remarks>The <c>WatchSize</c> is the number of watch variables that can be downloaded from the target hardware in a single transaction.
        /// </remarks>
        private const short DefaultWatchSize = 20;

        /// <summary>
        /// The default number of watch variables that can be recorded by each event log.
        /// </summary>
        private const short DefaultWatchSizeFaultLog = 8;

        /// <summary>
        /// The default port identifier associated with the WibuBox security device. Value: 38.
        /// </summary>
        private const short DefaultWibuBoxPortId = 38;

        #region - [Font] -
        /// <summary>
        /// The default font for Windows XP systems. Value: "Tahoma".
        /// </summary>
        public const string DefaultFontFamilyXP = "Tahoma";

        /// <summary>
        /// The default font size for Windows XP systems. Value: 8F.
        /// </summary>
        public const float DefaultFontSizeXP = 8F;

        /// <summary>
        /// The default font for Windows 7 systems. Value: "Segoe UI".
        /// </summary>
        public const string DefaultFontFamilyWin7 = "Segoe UI";

        /// <summary>
        /// The default font size for Windows 7 systems. Value: 9F.
        /// </summary>
        public const float DefaultFontSizeWin7 = 9F;
        #endregion - [Font] -

        #region - [Security] -
        /// <summary>
        /// The default base security level, i.e. the security level that the PTU is set to on startup.
        /// </summary>
        private const SecurityLevel DefaultSecurityLevelBase = SecurityLevel.Level0;

        /// <summary>
        /// The default highest security level appropriate to the client.
        /// </summary>
        private const SecurityLevel DefaultSecurityLevelHighest = SecurityLevel.Level2;
        #endregion - [Security] -
        #endregion - [Default Values] -
        #endregion --- Constants ---

        #region --- Member Variables ---
        #region - [Data Dictionary Defined] -
        /// <summary>
        /// A flag to specify whether saved event logs are to display the log name associated with the event or not. True, if the log name is to be displayed; otherwise,
        /// false.
        /// </summary>
        private static bool m_ShowLogName = false;

        /// <summary>
        /// A flag to specify whether the Vehicle Control Unit for the current project uses a 2 digit year code or a 4 digit year code. True, if the Vehicle Control
        /// Unit for the current project uses a 4 digit year code; otherwise, false.
        /// <para>Valid 2 digit year code values are within the range 0 to 99, where 0 to 69 represent years 2000 to 2069, and 70 to 99 represent years 1970
        /// to 1999. Valid 4 digit year codes are within the range 1900 to 2199.</para>
        /// </summary>
        private static bool m_Use4DigitYearCode = false;

        /// <summary>
        /// <para>A bitmask used to specify which programmable function options are to be used.</para>
        /// Bit 7   -   Not Used.
        /// Bit 6   -   Not Used.
        /// Bit 5   -   Not Used.
        /// Bit 4   -   Not Used.
        /// Bit 3   -   Not Used.
        /// Bit 2   -   Not Used.
        /// <para>
        /// Bit 1   -   ShowLogName - Flag to specify whether the event log name field is to be shown when saved event logs are displayed. True, if the
        ///             log name is to be displayed; otherwise, false.
        /// </para>
        /// <para>
        /// Bit 0   -   Use4DigitYearCode - Flag to specify whether the project VCU uses a 2 or 4 digit year code. True, if the project uses a 4 digit year code;
        ///             otherwise, false.
        /// </para>
        /// </summary>
        private static short m_FunctionFlags = 0;

        /// <summary>
        /// The <c>WatchSize</c> supported by the target hardware.
        /// </summary>
        /// <remarks>The <c>WatchSize</c> is the number of watch variables that can be downloaded from the target hardware in a single transaction.
        /// </remarks>
        private static short m_WatchSize;

        /// <summary>
        /// The number of watch variables that can be recorded by each event log.
        /// </summary>
        /// <remarks>On most systems this value is the same for each event log, however, if the event log sub-system supports more than one datastream
        /// type then this value is set to the maximum value. For example, the CTA project support two types of event log, the snapshot log and the
        /// normal log. The datastream corresponding to the snapshot log supports 20 watch variables and the datastream corresponding to the normal
        /// event logs supports 16 watch variables. For this project this value is set to 20.</remarks>
        private static short m_WatchSizeFaultLog;

        /// <summary>
        /// A flag that indicates whether the event log sub-system supports multiple datastream types.
        /// </summary>
        private static bool m_SupportsMultipleDataStreamTypes = false;

        /// <summary>
        /// The project parameters associated with the current data dictionary i.e. the fields associated with the FILEINFO table.
        /// </summary>
        private static DataDictionaryInformation_t m_ProjectInformation;

        /// <summary>
        /// The parameters associated with the WibuBox security device, if applicable.
        /// </summary>
        private static WibuBox m_WibuBox;

        /// <summary>
        /// The specified path for PTU Application Data directory.
        /// </summary>
        private static string m_PathPTUApplicationData;

		/// <summary>
		/// List of URIs to try when connecting if the network option is set.
		/// </summary>
		private static List<string> m_URIList;

		/// <summary>
		/// Setting as to whether the PTU will attempt to make access via Ethernet, the RS232 port, or both.
		/// </summary>
		private static CommunicationTypeEnum m_CommunicationType;

        #region - [Security] -
        /// <summary>
        /// The default description associated with the security level 0.
        /// </summary>
        private static readonly string m_DefaultDescriptionLevel0 = Resources.DescriptionSecurityLevel0;

        /// <summary>
        /// The default description associated with the security level 1.
        /// </summary>
        private static readonly string m_DefaultDescriptionLevel1 = Resources.DescriptionSecurityLevel1;

        /// <summary>
        /// The default description associated with the security level 2.
        /// </summary>
        private static readonly string m_DefaultDescriptionLevel2 = Resources.DescriptionSecurityLevel2;

        /// <summary>
        /// The default description associated with the security level 3.
        /// </summary>
        private static readonly string m_DefaultDescriptionLevel3 = Resources.DescriptionSecurityLevel3;

        /// <summary>
        /// The parameters used to configure the security class.
        /// </summary>
        private static SecurityConfiguration_t m_SecurityConfiguration;
        #endregion - [Security] -
        #endregion - [Data Dictionary Defined] -

        /// <summary>
        /// The display area associated with the application.
        /// </summary>
        private readonly static Rectangle m_DisplayArea;

        /// <summary>
        /// The <c>DateTime</c> value that is used to represent an invalid date/time value.
        /// </summary>
        private readonly static DateTime m_InvalidDateTime;

        /// <summary>
        /// The <c>Font</c> associated with the PTU.
        /// </summary>
        private static Font m_Font;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Static constructor. Initialize the class properties to their default values.
        /// </summary>
        static Parameter()
        {
            // ---------------
            // Data Dictionary
            // ---------------
            SetToDefault();

            // -------------
            // Display Area
            // -------------
            m_DisplayArea = new Rectangle(0, 0, Screen.PrimaryScreen.WorkingArea.Width - PaddingHorizontalWorkingArea,
                                          Screen.PrimaryScreen.WorkingArea.Height - PaddingVerticalWorkingArea);

            // ------------------
            // Invalid Date/Time
            // ------------------
            m_InvalidDateTime = new DateTime(1, 1, 1, 0, 0, 0);
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Set the data dictionary defined parameters to their default values.
        /// </summary>
        private static void SetToDefault()
        {
            // --------------------
            // Function Flags
            // .
            // .
            // .
            // Bit 3   -   Not Used.
            // Bit 2   -   Not Used. 
            // Bit 1   -   ShowLogName - Flag to specify whether the event log name field is to be shown when saved event logs are displayed. True, if the
            //             log name is to be displayed; otherwise, false.
            // Bit 0   -   Use4DigitYearCode - Flag to specify whether the project VCU uses a 2 or 4 digit year code. True, if the project uses a 4 digit year code;
            //             otherwise, false.
            //
            // --------------------
            m_FunctionFlags = 0;

            m_ShowLogName = false;
            m_Use4DigitYearCode = false;

            // --------------------
            // Recorded Watch Data
            // --------------------
            m_WatchSize = DefaultWatchSize;

            // --------------------
            // Fault Log
            // --------------------
            m_WatchSizeFaultLog = DefaultWatchSizeFaultLog;
            m_SupportsMultipleDataStreamTypes = false;

            // --------------------
            // Project Information
            // --------------------
            m_ProjectInformation = new DataDictionaryInformation_t();
            m_ProjectInformation.DataDictionaryName = Resources.TextNotAvailable;
            m_ProjectInformation.ProjectIdentifier = string.Empty;
            m_ProjectInformation.Version = Resources.TextNotAvailable;
            m_ProjectInformation.DataDictionaryBuilderVersion = Resources.TextNotAvailable;
            m_ProjectInformation.WatchIdentifierCount = 0;

            // ----------------------
            // WibuBox Parameters
            // ----------------------
            m_WibuBox.FirmCode = 0;
            m_WibuBox.UserCode = 0;
            m_WibuBox.SlotId = 0;
            m_WibuBox.PortId = DefaultWibuBoxPortId;

            // ----------------------
            // Application Data Path
            // ----------------------
            m_PathPTUApplicationData = Resources.PathUseDefault;

            // --------
            // Security
            // --------
            m_SecurityConfiguration = new SecurityConfiguration_t();
            m_SecurityConfiguration.DescriptionLevel0 = m_DefaultDescriptionLevel0;
            m_SecurityConfiguration.DescriptionLevel1 = m_DefaultDescriptionLevel1;
            m_SecurityConfiguration.DescriptionLevel2 = m_DefaultDescriptionLevel2;
            m_SecurityConfiguration.DescriptionLevel3 = m_DefaultDescriptionLevel3;
            m_SecurityConfiguration.SecurityLevelBase = DefaultSecurityLevelBase;
            m_SecurityConfiguration.SecurityLevelHighest = DefaultSecurityLevelHighest;

			// ----------------------
			// Communication Variables
			// ----------------------
			m_URIList = new List<String>();
            m_CommunicationType = CommunicationTypeEnum.TCPIP;
        }

        /// <summary>
        /// Initializes the class properties to the parameter values contained within the specified configuration file. If any configuration file
        /// parameters are invalid all properties associated with the table to which the parameter belongs will be left at their default values.
        /// </summary>
        /// <param name="dataDictionary">The <c>DataSet</c> corresponding to the current data dictionary.</param>
        public static void Initialize(DataDictionary dataDictionary)
        {
            // Set the data dictionary defined properties to their default values.
            SetToDefault();

            // Now overlay the values defined in the data dictionary. If an exception is thrown the properties will be left in their default state.

            // --------------------
            // Function Flags
            // --------------------
            try
            {
                m_FunctionFlags = dataDictionary.CONFIGUREPTU[0].FunctionFlags;
                m_Use4DigitYearCode = ((m_FunctionFlags & CommonConstants.MaskBit0) == CommonConstants.MaskBit0) ? true : false;
                m_ShowLogName = ((m_FunctionFlags & CommonConstants.MaskBit1) == CommonConstants.MaskBit1) ? true : false;
            }
            catch (Exception)
            {
                // Use the default values set up by the static constructor instead.
            }

            // --------------------
            // Recorded Watch Data
            // --------------------
			try
			{
				m_WatchSize = dataDictionary.CONFIGUREPTU[0].WatchSize;
                short watchReturn = (short)CommunicationError.UnknownError;

                // Inform the VcuCommunication32/VcuCommunication64 dynamic link library of the number of watch variables that are associated with the
                // project.
                CommunicationParent communicationInterface = new CommunicationParent();
                watchReturn = communicationInterface.SetWatchSize(m_WatchSize);
                communicationInterface = null;

				if (watchReturn != m_WatchSize)
				{
					// Use the returned value.
					m_WatchSize = watchReturn;

                    MessageBox.Show(Resources.MBTWatchSizeInvalid, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (System.EntryPointNotFoundException)
			{
                MessageBox.Show(Resources.MBTSetWatchSizeNotSupported, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception)
			{
				// Use the default values set up by the static constructor instead.
			}

            // --------------------
            // Fault Log WatchSize
            // --------------------
            try
            {
                // Scan the number of watch variables that are recorded by each event log and determine the maximum.
                short watchVariablesMax = 0;
                short watchVariablesPrev = 0;
                short watchVariableCurrent;
                bool firstPass = true;
                m_SupportsMultipleDataStreamTypes = false;
                for (short recordIndex = 0; recordIndex < dataDictionary.LOGS.Count; recordIndex++)
                {
                    watchVariableCurrent = dataDictionary.DataStreamTypes[dataDictionary.LOGS[recordIndex].DataStreamTypeIdentifier].WatchVariablesMax;

                    if (firstPass == true)
                    {
                        watchVariablesMax = watchVariableCurrent;
                        watchVariablesPrev = watchVariableCurrent;
                        firstPass = false;
                    }
                    else
                    {
                        if (watchVariableCurrent > watchVariablesMax)
                        {
                            watchVariablesMax = watchVariableCurrent;
                        }

                        if (watchVariableCurrent != watchVariablesPrev)
                        {
                            m_SupportsMultipleDataStreamTypes = true;
                        }
                    }
                }

                m_WatchSizeFaultLog = watchVariablesMax;
            }
            catch (Exception)
            {
                // Use the default values set up by the static constructor instead.
            }

            // --------------------
            // Project Information
            // --------------------
            try
            {
                m_ProjectInformation.DataDictionaryName = dataDictionary.FILEINFO[0].DDNAME;
                m_ProjectInformation.ProjectIdentifier = dataDictionary.FILEINFO[0].PROJECTSTRING;
                m_ProjectInformation.Version = dataDictionary.FILEINFO[0].VERSION;
                m_ProjectInformation.DataDictionaryBuilderVersion = dataDictionary.FILEINFO[0].DDBVersion;
                m_ProjectInformation.WatchIdentifierCount = dataDictionary.FILEINFO[0].NUMOFVARS;
            }
            catch (Exception)
            {
                // Use the default values set up by the static constructor instead.
            }

            // ----------------------
            // WibuBox Parameters
            // ----------------------
            try
            {
                m_WibuBox.FirmCode = dataDictionary.CONFIGUREPTU[0].FIRMCODE;
                m_WibuBox.UserCode = dataDictionary.CONFIGUREPTU[0].USERCODE;
                m_WibuBox.SlotId = dataDictionary.CONFIGUREPTU[0].SLOTID;
                m_WibuBox.PortId = dataDictionary.CONFIGUREPTU[0].PORTID;
            }
            catch(Exception)
            {
                // Use the default values set up by the static constructor instead.
            }

            // ----------------------
            // Application Data Path
            // ----------------------
            try
            {
                m_PathPTUApplicationData = dataDictionary.CONFIGUREPTU[0].ApplicationDataPath;
            }
            catch (Exception)
            {
                // Use the default values set up by the static constructor instead.
            }

            // --------
            // Security
            // --------
            try
            {
                m_SecurityConfiguration.DescriptionLevel0 = dataDictionary.Security[0].DescriptionLevel0;
                m_SecurityConfiguration.DescriptionLevel1 = dataDictionary.Security[0].DescriptionLevel1;
                m_SecurityConfiguration.DescriptionLevel2 = dataDictionary.Security[0].DescriptionLevel2;
                m_SecurityConfiguration.DescriptionLevel3 = dataDictionary.Security[0].DescriptionLevel3;
                m_SecurityConfiguration.SecurityLevelBase = (SecurityLevel)dataDictionary.Security[0].SecurityLevelBase;
                m_SecurityConfiguration.SecurityLevelHighest = (SecurityLevel)dataDictionary.Security[0].SecurityLevelHighest;
            }
            catch (Exception)
            {
                // Use the default values set up by the static constructor instead.
			}

			// ----------------------
			// Communication Variables
			// ----------------------
			try
			{
				m_CommunicationType = (CommunicationTypeEnum)dataDictionary.CONFIGUREPTU[0].CommunicationType;
			}
			catch (Exception)
			{
				// Use the default values set up by the static constructor instead.
			}

			try
			{
				m_URIList = new List<string>(dataDictionary.URI.Count);
				foreach (System.Data.DataRow dataRow in dataDictionary.URI.Rows)
				{
					string URI = (string)dataRow.ItemArray[1];
					if (URI.Length > 0)
                    {
						m_URIList.Add(URI);
                    }
				}
			}
			catch (Exception)
			{
				// Use the default values set up by the static constructor instead.
			}
        }
        #endregion --- Methods ---

        #region --- Properties ---
        #region - [Data Dictionary Defined] -
        /// <summary>
        /// Get the flag that specifies whether saved event logs are to display the log name associated with the event or not. True, if the log name is to be displayed;
        /// otherwise, false.
        /// </summary>
        public static bool ShowLogName
        {
            get { return m_ShowLogName; }
        }

        /// <summary>
        /// <para>Get the flag that specifies whether the Vehicle Control Unit for the current project uses a 2 digit year code or a 4 digit year code. True, if the
        /// Vehicle Control Unit for the current project uses a 4 digit year code; otherwise, false.</para>
        /// <para>Valid 2 digit year code values are within the range 0 to 99, where 0 to 69 represent years 2000 to 2069, and 70 to 99 represent years 1970
        /// to 1999. Valid 4 digit year codes are within the range 1900 to 2199.</para>
        /// </summary>
        public static bool Use4DigitYearCode
        {
            get { return m_Use4DigitYearCode; }
        }

        /// <summary>
        /// Get the <c>WatchSize</c> supported by the target hardware.
        /// </summary>
        /// <remarks>The <c>WatchSize</c> is the number of watch variables that can be downloaded from the target hardware in a single transaction.</remarks>
        public static short WatchSize
        {
            get { return m_WatchSize; }
        }

        /// <summary>
        /// The number of watch variables that can be recorded by each event log.
        /// </summary>
        /// <remarks>On most systems this value is the same for each event log, however, if the event log sub-system supports more than one datastream
        /// type then this value is set to the maximum value. For example, the CTA project support two types of event log, the snapshot log and the
        /// normal log. The datastream corresponding to the snapshot log supports 20 watch variables and the datastream corresponding to the normal
        /// event logs supports 16 watch variables.</remarks>
        public static short WatchSizeFaultLog
        {
            get { return m_WatchSizeFaultLog; }
        }

        /// <summary>
        /// A flag to indicate whether the event log sub-system supports multiple datastream types.
        /// </summary>
        public static bool SupportsMultipleDataStreamTypes
        {
            get { return m_SupportsMultipleDataStreamTypes; }
        }

        /// <summary>
        /// Gets the project parameters i.e. the fields associated with the FILEINFO table of the data dictionary.
        /// </summary>
        public static DataDictionaryInformation_t ProjectInformation
        {
            get { return m_ProjectInformation; }
        }

        /// <summary>
        /// Gets the parameters associated with the WibuBox.
        /// </summary>
        public static WibuBox WibuBox
        {
            get { return m_WibuBox; }
        }

        /// <summary>
        /// Gets the parameters used to configure the security class.
        /// </summary>
        public static SecurityConfiguration_t SecurityConfiguration
        {
            get { return m_SecurityConfiguration; }
        }

        /// <summary>
        /// Gets the specified path for the PTU Application Data directory.
        /// </summary>
        /// <remarks>An empty string is used to represent the Windows default path.</remarks>
        public static string PathPTUApplicationData
        {
            get {return m_PathPTUApplicationData; }
        }

		/// <summary>
		/// Gets the list of URIs to check if IP Access is indicated.
		/// </summary>
		public static List<string> URIList
		{
			get { return m_URIList; }
		}

		/// <summary>
		/// Gets the enumeration indicating whether access is indicated for RS232, IP, or both channels.
		/// </summary>
		/// <remarks>An empty string is used to represent the Windows default path.</remarks>
		public static CommunicationTypeEnum CommunicationType
		{
			get { return m_CommunicationType; }
		}
        #endregion - [Data Dictionary Defined] -

        /// <summary>
        /// Gets the display area associated with the application.
        /// </summary>
        public static Rectangle DisplayArea
        {
            get { return m_DisplayArea; }
        }

        /// <summary>
        /// Gets the <c>DateTime</c> value that is used to represent an invalid date/time value.
        /// </summary>
        public static DateTime InvalidDateTime
        {
            get { return m_InvalidDateTime; }
        }

        /// <summary>
        /// Get or sets the font associated with the PTU application.
        /// </summary>
        public static Font Font
        {
            get { return m_Font; }
            set { m_Font = value; }
        }
        #endregion --- Properties ---
    }
}
