#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2014    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    Common
 * 
 *  File name:  VcuCommunication64.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  03/11/15    1.0     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order TBD.
 *                                      
 *                                          1.  Changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                              to support both 32 and 64 bit architecture.
 *                                              
 *                                          2.  Changes to allow the PTU to handle both 2 and 4 character date codes.
 *  
 *                                      Modifications
 *                                      1.  First entry into TortoiseSVN. This file is created from ver. 1.6 of VcuCommunication32.cs but has been
 *                                          modified to import the VcuCommunication64.dll library.
 *                                          
 *                                      2.  Modified the SetTimeDate() and GetTimeDate() methods to include an additional parameter that specifies
 *                                          whether the Vehicle Control Unit uses 2 or 4 digit year code format.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Runtime.InteropServices;

namespace Common.Communication
{
    /// <summary>
    /// A managed wrapper class to allow the unmanaged C++ functions associated with the VcuCommunication64 dynamic link library (DLL) to be 
    /// accessed from within managed code. The VcuCommunication64 DLL supports the low level communication protocol with the 
    /// Vehicle Control Unit (VCU) when running on a 64 bit platform. VcuCommunication32.dll and VcuCommunication64.dll are functionally identical, however, 
    /// VcuCommunication64.dll is targeted towards Windows 64 bit operating systems.
    /// </summary>
    public class VcuCommunication64
    {
        #region - [VcuCommunication64.dll - Comm.cpp Prototypes] -
        /*
        INT16 WINAPI InitCommunication		(INT16, LPSTR, INT32, INT16, INT16, INT16);
        INT16 WINAPI CloseCommunication		(INT16);
        INT16 WINAPI SendVariable			(INT16, INT16, double);
        INT16 WINAPI SetWatchElements		(INT16 *);
        INT16 WINAPI SetWatchElement		(INT16, INT16);
        INT16 WINAPI UpdateWatchElements	(INT16, double *, INT16 *);
        INT16 WINAPI GetVariableInformation	(INT16, INT16 *, double *, double *, long *);
        INT16 WINAPI GetEmbeddedInformation	(BSTR*, BSTR*, BSTR*, BSTR*, double *);
        INT16 WINAPI GetChartMode			(INT16 *);
        INT16 WINAPI SetChartMode			(INT16);
        INT16 WINAPI GetChartIndex			(INT16, INT16 *);
        INT16 WINAPI SetChartIndex			(INT16, INT16);
        INT16 WINAPI SetChartScale			(INT16, double, double);
        INT16 WINAPI GetTimeDate			(INT16, INT16 *, INT16 *, INT16 *,INT16 *, INT16 *, INT16 *);
        INT16 WINAPI SetTimeDate			(INT16, INT16, INT16, INT16, INT16, INT16, INT16);
        INT16 WINAPI SetCarID				(LPSTR);
        INT16 WINAPI StartClock				(void);
        INT16 WINAPI StopClock				(void);
        INT16 WINAPI SetWatchSize			(UINT16);
        */
        #endregion - [VcuCommunication64.dll - Comm.cpp Prototypes] -

        #region - [VcuCommunication64.dll - Comm Functions] -
        /// <summary>
        /// Initialize the specified communication port.
        /// </summary>
        /// <param name="protocol">The protocol associated with the communication port.</param>
        /// <param name="portID">The port identifier, this varies depending upon the protocol setting. For physical and virtual serial COM ports this takes the form
        /// 1, 2, ... etc.</param>
        /// <param name="baudRate">The baud rate: 110, 150, 300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200. Only applicable to RS232 communication.
        /// </param>
        /// <param name="bitsPerByte">The number of bits per byte: 4-8. Only applicable to RS232 communication.</param>
        /// <param name="parity">The parity: 0-4 represent None, Odd, Even, Mark, Space respectively. Only applicable to RS232 communication.</param>
        /// <param name="stopBits">The number of stop bits: 0, 1, 2 represent 1, 1.5, 2 respectively. Only applicable to RS232 communication.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short InitCommunication(short protocol, string portID, int baudRate, short bitsPerByte, short parity, short stopBits);

        /// <summary>
        /// Close the specified communication port.
        /// </summary>
        /// <param name="protocol">The protocol associated with the communication port.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short CloseCommunication(short protocol);
        
        /// <summary>
        /// Write the specified data to the watch variable specified by the <paramref name="dictionaryIndex"/> parameter.
        /// </summary>
        /// <param name="dictionaryIndex">The disctionary index.</param>
        /// <param name="dataType">The data type.</param>
        /// <param name="data">The data.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short SendVariable(short dictionaryIndex, short dataType, double data);
        
        /// <summary>
        /// Set the watch variables that are to be monitored by the target hardware i.e. the watch elements. The target hardware is only capable of monitoring
        /// <c>WatchSize</c> watch variables at a time. The array of <c>WatchSize</c> watch elements specifies the watch identifier and corresponding data value and
        /// type for each of the watch elements.
        /// </summary>
        /// <param name="watchElements">An array of watch identifier defining the watch variables that are to be monitored.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short SetWatchElements(short[] watchElements);

        /// <summary>
        /// Set an individual watch element to a specific watch variable.
        /// </summary>
        /// <param name="elementIndex">The index of the watch element array that is to be set.</param>
        /// <param name="watchID">The watch identifier corresponding to the watch variable that is to be mapped into the watch element array at the 
        /// specified index.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short SetWatchElement(short elementIndex, short watchID);

        /// <summary>
        /// Retrieve the array of watch variable data values defined by the SetWatchElements()/SetWatchElement() functions.
        /// </summary>
        /// <param name="forceUpdate">True, to force a full update; otherwise, false.</param>
        /// <param name="watchValues">An array of watch variable values mapped to the watch elements defined by the SetWatchElements() function.</param>
        /// <param name="watchDataType">An array of watch variable data types mapped to the watch elements defined by the SetWatchElements() function.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short UpdateWatchElements(short forceUpdate, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] watchValues, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] short[] watchDataType);

        /// <summary>
        /// Get the configuration information associated with the target hardware.
        /// </summary>
        /// <remarks>The function uses 16 bit unicode strings, therefore all strings must be declared using the <c>String</c> typedef.</remarks>
        /// <param name="softwareVersion">The sofware version.</param>
        /// <param name="carID">The car identifier.</param>
        /// <param name="subSystemName">The title of the sub-system.</param>
        /// <param name="identifierString">The project identifier.</param>
        /// <param name="conversionMask">The conversion bitmask.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short GetEmbeddedInformation([MarshalAs(UnmanagedType.BStr)] out String softwareVersion,
                                                                 [MarshalAs(UnmanagedType.BStr)] out String carID, 
                                                                 [MarshalAs(UnmanagedType.BStr)] out String subSystemName,
                                                                 [MarshalAs(UnmanagedType.BStr)] out String identifierString,
                                                                 out double conversionMask);

        /// <summary>
        /// Get the mode of the chart recorder.
        /// </summary>
        /// <param name="chartMode">The current mode of the chart recorder: ramp, zero-output, full-scale, data.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short GetChartMode(out short chartMode);

        /// <summary>
        /// Set the mode of the chart recorder.
        /// </summary>
        /// <param name="chartMode">The required mode of the chart recorder: ramp, zero-output, full-scale, data.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short SetChartMode(short chartMode);

        /// <summary>
        /// Get the watch variable identifier that is currently assigned to the specified chart recorder channel index.
        /// </summary>
        /// <param name="chartIndex">The channel index of the chart recorder: 0 - 7.</param>
        /// <param name="watchIdentifier">The watch identifier of the watch variable that is assigned to the specified channel.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short GetChartIndex(short chartIndex, out short watchIdentifier);

        /// <summary>
        /// Set the watch variable identifier that is to be assigned to the specified chart recorder channel index.
        /// </summary>
        /// <param name="chartIndex">The chart index of the chart recorder channel that is to be configured: 0 - 7.</param>
        /// <param name="watchIdentifier">The watch identifier of the watch variable that is to be assigned to the specified channel.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short SetChartIndex(short chartIndex, short watchIdentifier);

        /// <summary>
        /// Set the scale of the chart recorder.
        /// </summary>
        /// <param name="watchIdentifier">The watch identifier of the watch variable that is to be configured.</param>
        /// <param name="maxChartScaleRaw">The maximum raw value of the Y scale.</param>
        /// <param name="minChartScaleRaw">The minimum raw value of the Y scale.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short SetChartScale(short watchIdentifier, double maxChartScaleRaw, double minChartScaleRaw);

        /// <summary>
        /// Get the date and time from the VCU.
        /// </summary>
        /// <param name="use4DigitYearCode">A flag that specifies whether the Vehicle Control Unit uses 2 or 4 digit year code format. True (0x01), if it
        /// uses 4 digit year code format; otherwise, false (0x00).</param>
        /// <param name="year">The year, as a value between 0-99. Values above 70 are assumed to be associated with years 1970-1999, 
        /// values below 70 are assumed to be associated with years 2000-2069.</param>
        /// <param name="month">The month value.</param>
        /// <param name="day">The day value.</param>
        /// <param name="hour">The hours value.</param>
        /// <param name="minute">The minutes value</param>
        /// <param name="second">The seconds value.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short GetTimeDate(short use4DigitYearCode, out short year, out short month, out short day, out short hour, out short minute,
                                                      out short second);

        /// <summary>
        /// Set the date and time on the VCU.
        /// </summary>
        /// <param name="use4DigitYearCode">A flag that specifies whether the Vehicle Control Unit uses 2 or 4 digit year code format. True (0x01), if it
        /// uses 4 digit year code format; otherwise, false (0x00).</param>
        /// <param name="year">The year, as a value between 0-99. Values above 70 are assumed to be associated with years 1970-1999, 
        /// values below 70 are assumed to be associated with years 2000-2069.</param>
        /// <param name="month">The month value.</param>
        /// <param name="day">The day value.</param>
        /// <param name="hour">The hours value.</param>
        /// <param name="minute">The minutes value</param>
        /// <param name="second">The seconds value.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short SetTimeDate(short use4DigitYearCode, short year, short month, short day, short hour, short minute, short second);

        /// <summary>
        /// Set the car identifier on the VCU.
        /// </summary>
        /// <param name="carIdentifier"></param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short SetCarID(string carIdentifier);

        /// <summary>
        /// Start the real time clock on the VCU.
        /// </summary>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short StartClock();

        /// <summary>
        /// Stop the real time clock on the VCU.
        /// </summary>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short StopClock();
        
		/// <summary>
        /// Set the number of watch variables associated with the project.
        /// </summary>
        /// <param name="watchSize">The number of watch variables associated with the project. Watch sizes of {20, 40 and 80} are currently supported.</param>
        /// <returns>The number of watch variables currently supported by PTUDLL32.</returns>
        [DllImport("VcuCommunicationNew.dll")]
        public static extern unsafe short SetWatchSize(short watchSize);
        #endregion - [VcuCommunicationNew.dll - Comm Functions] -
    }
}
