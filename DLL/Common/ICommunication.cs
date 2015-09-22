#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    Common
 * 
 *  File name:  ICommunication.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McDonald      First Release.
 * 
 *  08/25/10    1.1     K.McD           1.  Removed the PortIsOpen property, no longer required.
 *                                      2.  Added the bool forceUpdate parameter to the method UpdateWatchElements().
 * 
 *  09/29/10    1.2     K.McD           1.  Added SendVariable() method and restructured file.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Communication
{
    #region --- Enumerators ---
    /// <summary>
    /// The baud rate associated with the serial communication link.
    /// </summary>
    public enum Baud
    {
        /// <summary>
        /// A baud rate of 110 bits per second.
        /// </summary>
        Baud000110 = 110,

        /// <summary>
        /// A baud rate of 150 bits per second.
        /// </summary>
        Baud000150 = 150,

        /// <summary>
        /// A baud rate of 300 bits per second.
        /// </summary>
        Baud000300 = 300,

        /// <summary>
        /// A baud rate of 600 bits per second.
        /// </summary>
        Baud000600 = 600,

        /// <summary>
        /// A baud rate of 1,200 bits per second.
        /// </summary>
        Baud001200 = 1200,

        /// <summary>
        /// A baud rate of 2,400 bits per second.
        /// </summary>
        Baud002400 = 2400,

        /// <summary>
        /// A baud rate of 4,800 bits per second.
        /// </summary>
        Baud004800 = 4800,

        /// <summary>
        /// A baud rate of 9,600 bits per second.
        /// </summary>
        Baud009600 = 9600,

        /// <summary>
        /// A baud rate of 19,200 bits per second.
        /// </summary>
        Baud019200 = 19200,

        /// <summary>
        /// A baud rate of 38,400 bits per second.
        /// </summary>
        Baud038400 = 38400,

        /// <summary>
        /// A baud rate of 57,600 bits per second.
        /// </summary>
        Baud057600 = 57600,

        /// <summary>
        /// A baud rate of 115,200 bits per second.
        /// </summary>
        Baud115200 = 115200
    }

    /// <summary>
    ///  The parity associated with the serial communication link.
    /// </summary>
    public enum Parity
    {
        /// <summary>
        /// No parity bit.
        /// </summary>
        None = 0,

        /// <summary>
        /// Odd parity.
        /// </summary>
        Odd = 1,

        /// <summary>
        /// Even parity.
        /// </summary>
        Even = 2,

        /// <summary>
        /// The parity bit is always set.
        /// </summary>
        Mark = 3,

        /// <summary>
        /// The parity bit is always clear.
        /// </summary>
        Space = 4
    }

    /// <summary>
    /// The number of stop bits associated with the serial communication link.
    /// </summary>
    public enum StopBits
    {
        /// <summary>
        /// One stop bit.
        /// </summary>
        One = 0,

        /// <summary>
        /// One and a half stop bits.
        /// </summary>
        OnePointFive = 1,

        /// <summary>
        /// Two stop bits.
        /// </summary>
        Two = 2
    }

    /// <summary>
    /// The number of bits per character associated with the serial communication link.
    /// </summary>
    public enum BitsPerCharacter
    {
        /// <summary>
        /// 4 bits per byte.
        /// </summary>
        Four = 4,

        /// <summary>
        /// 5 bits per byte.
        /// </summary>
        Five = 5,

        /// <summary>
        /// 6 bits per byte.
        /// </summary>
        Six = 6,

        /// <summary>
        /// 7 bits per byte.
        /// </summary>
        Seven = 7,

        /// <summary>
        /// 8 bits per byte.
        /// </summary>
        Eight = 8
    }

    /// <summary>
    /// The protocol used to communicate with the target hardware.
    /// </summary>
    public enum Protocol
    {
        /// <summary>
        /// Standard RS232 TCMS protocol.
        /// </summary>
        RS232 = 0,

        /// <summary>
        /// TCPIP protocol.
        /// </summary>
        TCPIP = 1,

        /// <summary>
        /// UDP protocol.
        /// </summary>
        UDP = 2
    }

    /// <summary>
    /// The type of communication port e.g. COM; VCP; USB or Ethernet.
    /// </summary>
    public enum PortType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Physical RS232 communication COM port.
        /// </summary>
        COM = 1,

        /// <summary>
        /// Virtual RS232 communication COM port.
        /// </summary>
        /// <remarks>Each USB to serial converter sets up a virtual communications COM port.</remarks>
        VCP = 2,

        /// <summary>
        /// Direct access to the USB communication port using the FTD2XX.DLL interface.
        /// </summary>
        USB = 3,

        /// <summary>
        /// Ethernet communication interface.
        /// </summary>
        ETHERNET = 4
    }


    /// <summary>
    /// The chart recorder mode of operation, used to calibrate the chart recorder.
    /// </summary>
    public enum ChartMode
    {
        /// <summary>
        /// Display the current value of the watch variable associated with each chart recorder channel.
        /// </summary>
        DataMode = 1,

        /// <summary>
        /// Display the maximum value of the watch variable associated with each chart recorder channel.
        /// </summary>
        FullMode = 2,

        /// <summary>
        /// Ramp between the minimum and maximum value of the watch variable associated with each chart recorder channel.
        /// </summary>
        RampMode = 3,

        /// <summary>
        /// Display the minimum value of the watch variable associated with each chart recorder channel.
        /// </summary>
        ZeroMode = 4
    }

    /// <summary>
    /// The attribute flags associated with a watch variable.
    /// </summary>
    [Flags]
    public enum AttributeFlags
    {
        /// <summary>
        /// The watch variable is stored in battery backed RAM.
        /// </summary>
        PTUD_BBRAM = 128,

        PTUD_DESC1 = 64,
        PTUD_DESC2 = 32,
        PTUD_DESC3 = 16,

        /// <summary>
        /// Must have level 1 security clearance in order to modify the watch variable.
        /// </summary>
        PTUD_PSSWD1 = 8,

        /// <summary>
        /// Must have level 2 security clearance in order to modify the watch variable.
        /// </summary>
        PTUD_PSSWD2 = 4,

        /// <summary>
        /// The watch variable is read-only.
        /// </summary>
        PTUD_READONLY = 2,

        /// <summary>
        /// This flag is not used and should always be clear.
        /// </summary>
        PTUD_NOTUSED = 1
    }

    /// <summary>
    /// The error returns from the communication functions associated with the PTUDLL32 dynamic link library.
    /// </summary>
    public enum CommunicationError
    {
        /// <summary>
        /// The function call was successful i.e. there were no errors.
        /// </summary>
        Success = 0,

        /// <summary>
        /// The port is already open.
        /// </summary>
        PortOpen = -1,

        /// <summary>
        /// Cannot find the required hardware.
        /// </summary>
        NoHardware = -2,

        /// <summary>
        /// There was no reply within the timeout period.
        /// </summary>
        TimeOut = -10,

        /// <summary>
        /// The response packet was invalid.
        /// </summary>
        BadResponse = -11,

        /// <summary>
        /// There was a checksum error.
        /// </summary>
        ChecksumError = -12,

        /// <summary>
        /// The request packet was invalid.
        /// </summary>
        BadRequest = -13,

        /// <summary>
        /// Unknown error.
        /// </summary>
        UnknownError = -100
    }
    #endregion --- Enumerators ---

    /// <summary>
    /// Class to manage communication with the target hardware.
    /// </summary>
    public interface ICommunication
    {
        #region - [Methods] -
        /// <summary>
        /// Scan the specified serial communication port to determine if it is connected to a target logic controller. If a target is found
        /// the target configuration information is written to the output parameter <paramref name="targetConfiguration"/>.
        /// </summary>
        /// <param name="communicationSetting">The communication settings that are to be used to communicate with the target.</param>
        /// <param name="targetConfiguration">The target configuration information returned from the target hardware if a target is found.</param>
        /// <returns>A flag to indicate whether a target was found; true, if a target was found, otherwise, false.</returns>
        bool ScanPort(CommunicationSetting_t communicationSetting, out TargetConfiguration_t targetConfiguration);

        /// <summary>
        /// Initialize the target hardware communication port.
        /// </summary>
        /// <param name="communicationsSetting">The communication settings.</param>
        /// <exception cref="InvalidOperationException">Throw if the call to the <c>PTUDLL32.InitCommunication()</c> method returns an error code other than
        /// <c>CommunicationError.Success</c></exception>
        void InitCommunication(CommunicationSetting_t communicationsSetting);

        /// <summary>
        /// Close the target hardware communication port.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throw if the call to the <c>PTUDLL32.CloseCommunication()</c> method returns an error code other than
        /// <c>CommunicationError.Success</c></exception>
        void CloseCommunication(Protocol protocol);

        /// <summary>
        /// Write the specified data to the watch variable specified by the <paramref name="dictionaryIndex"/> parameter.
        /// </summary>
        /// <param name="dictionaryIndex">The disctionary index.</param>
        /// <param name="dataType">The data type.</param>
        /// <param name="data">The data.</param>
        void SendVariable(short dictionaryIndex, short dataType, double data);

        /// <summary>
        /// Map the watch identifiers listed in <paramref name="watchElementList"/> to the watch element array monitored by the target hardware.
        /// </summary>
        /// <remarks> The number of watch identifiers in the list must mot exceed <c>WatchSize</c>.</remarks>
        /// <param name="watchElementList">The list containing the watch identifiers that are to be mapped to each element of the watch element array.</param>
        void SetWatchElements(System.Collections.Generic.List<short> watchElementList);

        /// <summary>
        /// Map an individual watch identifier to a specific watch element.
        /// </summary>
        /// <param name="elementIndex">The index of the watch element array that is to be set.</param>
        /// <param name="watchIdentifier">The watch identifier corresponding to the watch variable that is to be mapped into the watch element table at the 
        /// specified index.</param>
        void SetWatchElement(short elementIndex, short watchIdentifier);

        /// <summary>
        /// Retrieve the watch elements from the target hardware.
        /// </summary>
        /// <remarks>The watch elements are the watch values that are being monitored by the target hardware as defined by the <c>SetWatchElements()> method.</c></remarks>
        /// <returns>The retrieved watch element table, if successful; otherwise, null.</returns>
        WatchElement_t[] UpdateWatchElements(bool forceUpdate);

        /// <summary>
        /// Get the configuration information associated with the target hardware.
        /// </summary>
        /// <param name="targetConfiguration">The target configuration information retrieved from the target.</param>
        void GetEmbeddedInformation(out TargetConfiguration_t targetConfiguration);

        /// <summary>
        /// Update the watch variable lookup table with the latest watch element data retrieved from the targer hardware.
        /// </summary>
        /// <param name="watchElements">The watch element table retrieved from the target hardware.</param>
        void UpdateWatchVariableTable(WatchElement_t[] watchElements);

        /// <summary>
        /// Get the date and time from the target hardware. Implementation is defined in the child class.
        /// </summary>
        /// <param name="dateTime">The the date and time as a .NET <c>DateTime</c> object.</param>
        void GetTimeDate(out DateTime dateTime);

        /// <summary>
        /// Sets the date and time of the target hardware. Implementation is defined in the child class.
        /// </summary>
        /// <param name="dateTime">The date and time as a .NET <c>DateTime</c> object.</param>
        void SetTimeDate(DateTime dateTime);

        /// <summary>
        /// Set the car identifier. Implementation is defined in the child class.
        /// </summary>
        /// <param name="carIdentifier"></param>
        void SetCarID(string carIdentifier);
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets or sets the communication settings associated with the selected target.
        /// </summary>
        CommunicationSetting_t CommunicationSetting { get; set;}

        /// <summary>
        /// Gets the maximum number of watch elements that can be read in a single transaction; 
        /// </summary>
        int WatchSize { get; }
        #endregion - [Properties] -
    }
}
