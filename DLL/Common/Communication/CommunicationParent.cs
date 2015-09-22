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
 *  File name:  CommunicationParent.cs
 * 
 *  Revision History
 *  ----------------
 */

#region - [1.0 to 1.11] -
/* 
 *  Date        Version Author          Comments
 *  10/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/16/10    1.1     K.McD           1.  Removed the AutoScale_t structure.
 * 
 *  01/17/11    1.2     K.McD           1.  Removed the SimulateCommunicationLink conditional compilation.
 * 
 *  01/31/11    1.3     K.McD           1.  Added support for a mutex to control read/write access to the communication port.
 * 
 *  02/14/11    1.2     K.McD           1.  Removed the WatchSize property.
 *                                      2.  Added support for debug mode.
 * 
 *  02/28/11    1.3     K.McD           1.  Auto modified as a result of a resource name change.
 * 
 *  03/18/11    1.4     K.McD           1.  Modified a number of comments and XML tags.
 * 
 *  04/27/11    1.5     K.McD           1.  Modified the enumerator used to specify the different operational modes of the chart recorder.
 *                                      2.  Corrected a number of exception messages.
 *                                      3.  Added the communication method used to retrieve the current mode of the chart recorder from the VCU.
 *                                      
 *  05/24/11    1.6     K.McD           1.  Added the SetChartMode(), SetChartIndex(), SetChartScale() and DownloadChartRecorderWorkset() methods.
 *  
 *  05/26/11    1.7     K.McD           1.  Corrected the SetChartScale() method to download the specified parameter values rather than the upper and
 *                                          lower limits defined in the data dictionary.
 *                                          
 *  06/23/11    1.8     K.McD           1.  Added support for displaying debug mode information to the following communication methods: GetChartMode(),
 *                                          SetChartMode(), SetChartIndex() and SetChartScale().
 *                                      2.  Modified a number of XML tags.
 *                                      3.  Re-ordered the ChartMode enumerator elements.
 *                                      
 *  07/20/11    1.9     K.McD           1.  Added the value information associated with the enumerator/constant to a number of XML tags.
 *                                      2.  Added the SIMULATOR element to the Protocol enumerator.
 *                                      3.  Modified the constructor signature to use the ICommunicationParent interface.
 *                                      
 *  08/23/11    1.10    K.McD           1.  Modified each method to use the following pattern in order to improve error handling in the event that
 *                                          communications to the target hardware is lost.
 *  
 *                                              public virtual void myMethod( ... )
 *                                              {
 *                                                  CommunicationError errorCode;
 *                                                  try
 *                                                  {
 *                                                      m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
 *                                                      errorCode = (CommunicationError)PTUDLL32.MethodName( ... );
 *                                                      m_MutexCommuncationInterface.ReleaseMutex();
 *                                                  }
 *                                                  catch(Exception)
 *                                                  {
 *                                                      errorCode = CommunicationError.SystemException;
 *                                                      throw new CommunicationException(Resources.ResourceName, errorCode);
 *                                                  }
 *                                                  
 *                                                  if (errorCode != CommunicationError.Success)
 *                                                  {
 *                                                      throw new CommunicationException(Resources.ResourceName, errorCode);
 *                                                  }
 *                                                  
 *                                                  if (DebugMode.Enabled == true)
 *                                                  {
 *                                                      DebugMode.MethodName_t methodName = new DebugMode.MethodName_t( ... );
 *                                                      DebugMode.Write(methodName.ToXML());
 *                                                  }
 *                                                  
 *                                                  .
 *                                                  .
 *                                                  .
 *                                              }
 *                                         
 *  02/27/14    1.11    K.McD           1.  Corrected the call to PTUDLL32.GetEmbeddedInformation() in the GetEmbeddedInformation() method to pass
 *                                          'out localTargetConfiguration.ConversionMask' rather than 'out targetConfiguration.ConversionMask'.
 *
 *                                      2.  Changed the call to PTUDLL32.GetChartMode() in the GetChartMode() method to 
 *                                          match the new prototype, now uses 'out' rather than 'ref'.
 */
#endregion - [1.0 to 1.11] -

#region- [2.0] -
/*
 *  03/11/15    2.0     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  Implement changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                              to support both 32 and 64 bit architecture.
 *                                          
 *                                          2.  Implement changes to allow the PTU to handle both 2 and 4 character date codes.
 *                                      
 *                                      Modifications
 *                                      1.  Added DelegateIsNull = 50 definition to the CommunicationError enumerator and initialized the local 
 *                                          errorCode variable to CommunicationError.UnknownError for each of the communication methods within the
 *                                          class.
 *                                          
 *                                      2.  Added delegate declarations for all of the VcuCommunication32.dll and VcuCommunication64 methods that
 *                                          are associated with the Common dynamic link library.
 *                                          
 *                                      3.  Added member delegates for each of the delegate declarations.
 *                                          
 *                                      4.  Removed the member variables and properties associated with the MeasureUpdateWatchElements
 *                                          conditional compilation symbol.
 *                                          
 *                                      5.  Rationalized the design of the constructors to use the 'this()' call.
 *                                          
 *                                      6.  Modified the zero parameter constructor to instantiate the delegates with either the 
 *                                          32 or 64 bit version of the corresponding method depending upon the current state of the 
 *                                          'Environment.Is64BitOperatingSystem' variable.
 *                                          
 *                                      7.  Modified each of the methods to check that the function delegate has been initialized prior to its use.
 *                                          
 *                                      8.  Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted, 
 *                                          the method was modified to a check that the Mutex has been initialized prior to its use.
 *                                          
 *                                      9.  Replaced all calls to the methods within PTUDLL32.dll with calls via function delegates. This allows 
 *                                          support for both 32 and 64 bit systems.
 *                                          
 *                                      10. Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted, 
 *                                          a 'finally' block was added to each 'try' block to ensure that the Mutex is released even if an exception 
 *                                          occurs. The code pattern was modified to use the following template:
 *                                          
 *                                          CommunicationError errorCode = CommunicationError.UnknownError;
 *                                          try
 *                                          {
 *                                              m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
 *                                              errorCode = (CommunicationError)m_<function-name>( ... );
 *                                          }
 *                                          catch (Exception)
 *                                          {
 *                                              errorCode = CommunicationError.SystemException;
 *                                              throw new CommunicationException(Resources.EMGetTargetConfigurationFailed, errorCode);
 *                                          }
 *                                          finally
 *                                          {
 *                                              m_MutexCommuncationInterface.ReleaseMutex();
 *                                          }
 *                                          
 *                                          if (DebugMode.Enabled == true)
 *                                          {
 *                                              ...
 *                                          }
 *
 *                                          if (errorCode != CommunicationError.Success)
 *                                          {
 *                                              throw new CommunicationException("<function-name>", errorCode);
 *                                          }
 *                                          
 *                                      11. Added the SetWatchSize() method to include support for projects that do not update use the standard number
 *                                          of watch variables on each poll of the VCU.
 *                                          
 *                                      12. Modified the delegate declarations for the SetTimeDate() and GetTimeDate() methods to include the additional parameter that
 *                                          was introduced to specify whether the Vehicle Control Unit uses 2 or 4 digit year code format.
 */
#endregion- [2.0] -
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Common.Configuration;
using Common.Properties;

namespace Common.Communication
{
    #region --- Enumerators ---
    /// <summary>
    /// The mode of the chart recorder, used to calibrate the chart recorder.
    /// </summary>
    public enum ChartMode
    {
        /// <summary>
        /// Undefined. Value: 0
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Display the values of the configured watch variables. Value: 1.
        /// </summary>
        DataMode = 1,

        /// <summary>
        /// Display all chart recorder channels as full-scale. Value: 2.
        /// </summary>
        FullScaleMode = 2,

        /// <summary>
        /// Ramp all chart recorder channels between the lower and upper limits. Value: 3.
        /// </summary>
        RampMode = 3,

        /// <summary>
        /// Set all chart recorder channels to give zero output. Value: 4.
        /// </summary>
        ZeroOutputMode = 4
    }

    /// <summary>
    /// The baud rate associated with the serial communication link.
    /// </summary>
    public enum Baud
    {
        /// <summary>
        /// A baud rate of 110 bits per second. Value: 110.
        /// </summary>
        Baud000110 = 110,

        /// <summary>
        /// A baud rate of 150 bits per second. Value: 150.
        /// </summary>
        Baud000150 = 150,

        /// <summary>
        /// A baud rate of 300 bits per second. Value: 300.
        /// </summary>
        Baud000300 = 300,

        /// <summary>
        /// A baud rate of 600 bits per second. Value: 600.
        /// </summary>
        Baud000600 = 600,

        /// <summary>
        /// A baud rate of 1,200 bits per second. Value: 1,200.
        /// </summary>
        Baud001200 = 1200,

        /// <summary>
        /// A baud rate of 2,400 bits per second. Value: 2,400.
        /// </summary>
        Baud002400 = 2400,

        /// <summary>
        /// A baud rate of 4,800 bits per second. Value: 4,800.
        /// </summary>
        Baud004800 = 4800,

        /// <summary>
        /// A baud rate of 9,600 bits per second. Value: 9,600.
        /// </summary>
        Baud009600 = 9600,

        /// <summary>
        /// A baud rate of 19,200 bits per second. Value: 19,200.
        /// </summary>
        Baud019200 = 19200,

        /// <summary>
        /// A baud rate of 38,400 bits per second. Value: 38,400.
        /// </summary>
        Baud038400 = 38400,

        /// <summary>
        /// A baud rate of 57,600 bits per second. Value: 57,600.
        /// </summary>
        Baud057600 = 57600,

        /// <summary>
        /// A baud rate of 115,200 bits per second. Value: 115,200.
        /// </summary>
        Baud115200 = 115200
    }

    /// <summary>
    ///  The parity associated with the serial communication link.
    /// </summary>
    public enum Parity
    {
        /// <summary>
        /// No parity bit. Value: 0.
        /// </summary>
        None = 0,

        /// <summary>
        /// Odd parity. Value: 1.
        /// </summary>
        Odd = 1,

        /// <summary>
        /// Even parity. Value: 2.
        /// </summary>
        Even = 2,

        /// <summary>
        /// The parity bit is always set. Value: 3.
        /// </summary>
        Mark = 3,

        /// <summary>
        /// The parity bit is always clear. Value: 4.
        /// </summary>
        Space = 4
    }

    /// <summary>
    /// The number of stop bits associated with the serial communication link.
    /// </summary>
    public enum StopBits
    {
        /// <summary>
        /// One stop bit. Value: 0.
        /// </summary>
        One = 0,

        /// <summary>
        /// One and a half stop bits. Value: 1.
        /// </summary>
        OnePointFive = 1,

        /// <summary>
        /// Two stop bits. Value: 2.
        /// </summary>
        Two = 2
    }

    /// <summary>
    /// The number of bits per character associated with the serial communication link.
    /// </summary>
    public enum BitsPerCharacter
    {
        /// <summary>
        /// 4 bits per byte. Value: 4.
        /// </summary>
        Four = 4,

        /// <summary>
        /// 5 bits per byte. Value: 5.
        /// </summary>
        Five = 5,

        /// <summary>
        /// 6 bits per byte. Value: 6.
        /// </summary>
        Six = 6,

        /// <summary>
        /// 7 bits per byte. Value: 7.
        /// </summary>
        Seven = 7,

        /// <summary>
        /// 8 bits per byte. Value: 8.
        /// </summary>
        Eight = 8
    }

    /// <summary>
    /// The protocol used to communicate with the target hardware.
    /// </summary>
    public enum Protocol
    {
        /// <summary>
        /// Standard RS232 TCMS protocol. Value: 0.
        /// </summary>
        RS232 = 0,

        /// <summary>
        /// TCPIP protocol. Value: 1.
        /// </summary>
        TCPIP = 1,

        /// <summary>
        /// UDP protocol. Value: 2.
        /// </summary>
        UDP = 2,

        /// <summary>
        /// Simulate VCU data. Value: 3.
        /// </summary>
        SIMULATOR = 3
    }

    /// <summary>
    /// The type of communication port e.g. COM; VCP; USB or Ethernet.
    /// </summary>
    public enum PortType
    {
        /// <summary>
        /// Undefined. Value: 0.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Physical RS232 communication COM port. Value: 1.
        /// </summary>
        COM = 1,

        /// <summary>
        /// Virtual RS232 communication COM port. Value: 2.
        /// </summary>
        /// <remarks>Each USB to serial converter sets up a virtual communications COM port.</remarks>
        VCP = 2,

        /// <summary>
        /// Direct access to the USB communication port using the FTD2XX.DLL interface. Value: 3.
        /// </summary>
        USB = 3,

        /// <summary>
        /// Ethernet communication interface. Value: 4.
        /// </summary>
        ETHERNET = 4
    }

    /// <summary>
    /// The attribute flags associated with a watch variable.
    /// </summary>
    [Flags]
    public enum AttributeFlags
    {
        /// <summary>
        /// The watch variable is stored in battery backed RAM. Value: 128.
        /// </summary>
        PTUD_BBRAM = 128,

        /// <summary>
        /// Spare. Value: 64.
        /// </summary>
        PTUD_DESC1 = 64,

        /// <summary>
        /// Spare. Value: 32.
        /// </summary>
        PTUD_DESC2 = 32,

        /// <summary>
        /// Spare. Value: 16.
        /// </summary>
        PTUD_DESC3 = 16,

        /// <summary>
        /// The user must have level 1 security clearance in order to modify the watch variable. Value: 8.
        /// </summary>
        PTUD_PSSWD1 = 8,

        /// <summary>
        /// The user must have level 2 security clearance in order to modify the watch variable. VAlue: 4.
        /// </summary>
        PTUD_PSSWD2 = 4,

        /// <summary>
        /// The watch variable is read-only. Value: 2.
        /// </summary>
        PTUD_READONLY = 2,

        /// <summary>
        /// This flag is not used and should always be clear. Value: 1.
        /// </summary>
        PTUD_NOTUSED = 1
    }

    /// <summary>
    /// The error returns from the communication functions associated with the PTUDLL32 dynamic link library.
    /// </summary>
    public enum CommunicationError
    {
        /// <summary>
        /// A system exception was thrown.
        /// </summary>
        SystemException = 1,

        /// <summary>
        /// The function call was successful i.e. there were no errors. Value: 0.
        /// </summary>
        Success = 0,

        /// <summary>
        /// The port is already open. Value: -1.
        /// </summary>
        PortOpen = -1,

        /// <summary>
        /// Cannot find the required hardware. Value: -2.
        /// </summary>
        NoHardware = -2,

        /// <summary>
        /// There was no reply within the timeout period. Value: -10.
        /// </summary>
        TimeOut = -10,

        /// <summary>
        /// The response packet was invalid. Value: -11.
        /// </summary>
        BadResponse = -11,

        /// <summary>
        /// There was a checksum error. Value: -12.
        /// </summary>
        ChecksumError = -12,

        /// <summary>
        /// The request packet was invalid. Value: -13.
        /// </summary>
        BadRequest = -13,

        /// <summary>
        /// The delegate used to call the appropriate VcuCommunication function has not been initialized.
        /// </summary>
        DelegateIsNull = -50,

        /// <summary>
        /// Unknown error. Value: -100.
        /// </summary>
        UnknownError = -100
    }
    #endregion --- Enumerators ---

    #region --- Structures ---
    /// <summary>
    /// Structure containing the fields of an individual watch element. 
    /// </summary>
    [Serializable]
    public struct WatchElement_t
    {
        /// <summary>
        /// The watch identifier of the watch variable that is mapped to the watch element.
        /// </summary>
        public short WatchIdentifier;

        /// <summary>
        /// The current value of the watch variable that is mapped to the watch element.
        /// </summary>
        public double Value;

        /// <summary>
        /// The current data type of the watch variable that is mapped to the watch element. 
        /// </summary>
        public short DataType;

        /// <summary>
        /// The index of the watch element.
        /// </summary>
        public short ElementIndex;
    }

    /// <summary>
    /// structure used to store the configuration information associated with the target hardware.
    /// </summary>
    [Serializable]
    public struct TargetConfiguration_t
    {
        /// <summary>
        /// The version number of the embedded software.
        /// </summary>
        public String Version;

        /// <summary>
        /// The car identifier.
        /// </summary>
        public String CarIdentifier;

        /// <summary>
        /// The name of the sub-system.
        /// </summary>
        public String SubSystemName;

        /// <summary>
        /// The project identifier associated with the embedded software.
        /// </summary>
        public String ProjectIdentifier;

        /// <summary>
        /// The conversion mask.
        /// </summary>
        public double ConversionMask;
    }

    /// <summary>
    /// Structure used to define the communication settings.
    /// </summary>
    [Serializable]
    public struct CommunicationSetting_t
    {
        /// <summary>
        /// The protocol associated with the port.
        /// </summary>
        public Protocol Protocol;

        /// <summary>
        /// The port identifier associated with the port, for physical and virtual COM ports this takes the form: 1, 2, 3 ... etc.
        /// </summary>
        /// <remarks>This parameter is unique to the PTUDLL32 dynamic link library.</remarks>
        public string PortIdentifier;

        /// <summary>
        /// The port definition.
        /// </summary>
        /// <remarks>This is derived from the registry.</remarks>
        public Port_t Port;

        /// <summary>
        /// The serial communication parameters i.e. baud rate, bits per character, parity and stop bits.
        /// </summary>
        /// <remarks>Only applicable to serial communication ports.</remarks>
        public SerialCommunicationParameters_t SerialCommunicationParameters;
    }

    /// <summary>
    /// Structure used to define the communication port.
    /// </summary>
    [Serializable]
    public struct Port_t
    {
        /// <summary>
        /// The name of the port 
        /// </summary>
        public string Name;

        /// <summary>
        /// The full specification for the comms port e.g. Device\Port21 - COM21.
        /// </summary>
        public string FullSpecification;

        /// <summary>
        /// The communication port type.
        /// </summary>
        public PortType Type;

        /// <summary>
        /// Overrides the ToString() method to ensure that the name of the port is returned.
        /// </summary>
        /// <returns>The name of the communications device.</returns>
        public override string ToString()
        {
            return FullSpecification;
        }
    }

    /// <summary>
    /// Structure used to define the serial communication port parameters i.e. baud rate, parity, bits per character and stop bits.
    /// </summary>
    [Serializable]
    public struct SerialCommunicationParameters_t
    {
        /// <summary>
        /// The baud rate associated with the serial communication port.
        /// </summary>
        /// <remarks>Only applicable to serial communication ports.</remarks>
        public Baud BaudRate;

        /// <summary>
        /// The parity associated with the serial communication port.
        /// </summary>
        /// <remarks>Only applicable to serial communication ports.</remarks>
        public Parity Parity;

        /// <summary>
        /// The number of data bits associated with the serial communication port.
        /// </summary>
        /// <remarks>Only applicable to serial communication ports.</remarks>
        public BitsPerCharacter BitsPerCharacter;

        /// <summary>
        /// The number of stop bits associated with the serial communication port.
        /// </summary>
        /// <remarks>Only applicable to serial communication ports.</remarks>
        public StopBits StopBits;

        /// <summary>
        /// Sets the serial communication parameters to their default values.
        /// </summary>
        public void SetToDefault()
        {
            // Set the serial communication parameters to their default values.
            BaudRate = Baud.Baud019200;
            Parity = Parity.None;
            BitsPerCharacter = BitsPerCharacter.Eight;
            StopBits = StopBits.One; 
        }
    }
    #endregion --- Structures ---

    /// <summary>
    /// Base class to manage communication with the target hardware.
    /// </summary>
    public class CommunicationParent : ICommunicationParent
    {
        #region --- Delegate Declarations ---
        /// <summary>
        /// Delegate declaration for the InitCommunication() method of VcuCommunication32.dll/VcuCommunication64.dll. This method initializes the
        /// specified communication port.
        /// </summary>
        /// <param name="protocol">The protocol associated with the communication port.</param>
        /// <param name="portID">The port identifier, this varies depending upon the protocol setting. For physical and virtual serial COM ports this
        /// takes the form 1, 2, ... etc.</param>
        /// <param name="baudRate">The baud rate: 110, 150, 300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200. Only applicable to RS232
        /// communication.</param>
        /// <param name="bitsPerByte">The number of bits per byte: 4-8. Only applicable to RS232 communication.</param>
        /// <param name="parity">The parity: 0-4 represent None, Odd, Even, Mark, Space respectively. Only applicable to RS232 communication.</param>
        /// <param name="stopBits">The number of stop bits: 0, 1, 2 represent 1, 1.5, 2 respectively. Only applicable to RS232 communication.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short InitCommunicationDelegate(short protocol, string portID, int baudRate, short bitsPerByte, short parity,
                                                           short stopBits);

        /// <summary>
        /// Delegate declaration for the CloseCommunication() method of VcuCommunication32.dll/VcuCommunication64.dll. This method closes the specified
        /// communication port.
        /// </summary>
        /// <param name="protocol">The protocol associated with the communication port.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short CloseCommunicationDelegate(short protocol);

        /// <summary>
        /// Delegate declaration for the SendVariable() method of VcuCommunication32.dll/VcuCommunication64.dll. This method writes the specified data
        /// to the watch variable 
        /// specified by the <paramref name="dictionaryIndex"/> parameter.
        /// </summary>
        /// <param name="dictionaryIndex">The disctionary index.</param>
        /// <param name="dataType">The data type.</param>
        /// <param name="data">The data.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short SendVariableDelegate(short dictionaryIndex, short dataType, double data);

        /// <summary>
        /// Delegate declaration for the SetWatchElements() method of VcuCommunication32.dll/VcuCommunication64.dll. This method sets the watch
        /// variables that are to be monitored by the target hardware i.e. the watch elements. The target hardware is only capable of monitoring
        /// <c>WatchSize</c> watch variables at a time. The array of <c>WatchSize</c> watch elements specifies the watch identifier and corresponding
        /// data value and type for each of the watch elements.
        /// </summary>
        /// <param name="watchElements">An array of watch identifier defining the watch variables that are to be monitored.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short SetWatchElementsDelegate(short[] watchElements);

        /// <summary>
        /// Delegate declaration for the UpdateWatchElements() method of VcuCommunication32.dll/VcuCommunication64.dll. This method retrieves the array
        /// of watch variable data values defined by the SetWatchElements()/SetWatchElement() functions.
        /// </summary>
        /// <param name="forceUpdate">True, to force a full update; otherwise, false.</param>
        /// <param name="watchValues">An array of watch variable values mapped to the watch elements defined by the SetWatchElements() function.
        /// </param>
        /// <param name="watchDataType">An array of watch variable data types mapped to the watch elements defined by the SetWatchElements() 
        /// function.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short UpdateWatchElementsDelegate(short forceUpdate, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] watchValues, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] short []watchDataType);

        /// <summary>
        /// Delegate declaration for the GetEmbeddedInformation() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets the
        /// configuration information associated with the target hardware.
        /// </summary>
        /// <remarks>The function uses 16 bit unicode strings, therefore all strings must be declared using the <c>String</c> typedef.</remarks>
        /// <param name="softwareVersion">The sofware version.</param>
        /// <param name="carID">The car identifier.</param>
        /// <param name="subSystemName">The title of the sub-system.</param>
        /// <param name="identifierString">The project identifier.</param>
        /// <param name="conversionMask">The conversion bitmask.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short GetEmbeddedInformationDelegate([MarshalAs(UnmanagedType.BStr)] out String softwareVersion,
                                                                [MarshalAs(UnmanagedType.BStr)] out String carID,
                                                                [MarshalAs(UnmanagedType.BStr)] out String subSystemName,
                                                                [MarshalAs(UnmanagedType.BStr)] out String identifierString,
                                                                out double conversionMask);

        /// <summary>
        /// Delegate declaration for the GetChartMode() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets the mode of the chart
        /// recorder.
        /// </summary>
        /// <param name="chartMode">The current mode of the chart recorder: ramp, zero-output, full-scale, data.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short GetChartModeDelegate(out short chartMode);

        /// <summary>
        /// Delegate declaration for the SetChartMode() method of VcuCommunication32.dll/VcuCommunication64.dll. This method sets the mode of the chart
        /// recorder.
        /// </summary>
        /// <param name="chartMode">The required mode of the chart recorder: ramp, zero-output, full-scale, data.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short SetChartModeDelegate(short chartMode);

        /// <summary>
        /// Delegate declaration for the GetChartIndex() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets the watch variable
        /// identifier that is currently assigned to the specified chart recorder channel index.
        /// </summary>
        /// <param name="chartIndex">The channel index of the chart recorder: 0 - 7.</param>
        /// <param name="watchIdentifier">The watch identifier of the watch variable that is assigned to the specified channel.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short GetChartIndexDelegate(short chartIndex, out short watchIdentifier);

        /// <summary>
        /// Delegate declaration for the SetChartIndex() method of VcuCommunication32.dll/VcuCommunication64.dll. This method sets the watch variable
        /// identifier 
        /// that is to be assigned to the specified chart recorder channel index.
        /// </summary>
        /// <param name="chartIndex">The chart index of the chart recorder channel that is to be configured: 0 - 7.</param>
        /// <param name="watchIdentifier">The watch identifier of the watch variable that is to be assigned to the specified channel.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short SetChartIndexDelegate(short chartIndex, short watchIdentifier);

        /// <summary>
        /// Delegate declaration for the SetChartScale() method of VcuCommunication32.dll/VcuCommunication64.dll. This method sets the scale of the
        /// chart recorder.
        /// </summary>
        /// <param name="watchIdentifier">The watch identifier of the watch variable that is to be configured.</param>
        /// <param name="maxChartScaleRaw">The maximum raw value of the Y scale.</param>
        /// <param name="minChartScaleRaw">The minimum raw value of the Y scale.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short SetChartScaleDelegate(short watchIdentifier, double maxChartScaleRaw, double minChartScaleRaw);

        /// <summary>
        /// Delegate declaration for the GetTimeDate() method of VcuCommunication32.dll/VcuCommunication64.dll. This method gets the time and date from
        /// the VCU. This delegate is also used for the Get4TimeDate() method that gets the time and date from VCU's that use 4 digit year codes.
        /// </summary>
        /// <param name="use4DigitYearCode">A flag that specifies whether the Vehicle Control Unit uses a 2 or 4 digit year code. True (0x01), if it
        /// uses a 4 digit year code; otherwise, false (0x00).</param>
        /// <param name="year">The year, as a value between 0-99. Values above 70 are assumed to be associated with years 1970-1999, values below 70
        /// are assumed to be associated with years 2000-2069.</param>
        /// <param name="month">The month value.</param>
        /// <param name="day">The day value.</param>
        /// <param name="hour">The hours value.</param>
        /// <param name="minute">The minutes value</param>
        /// <param name="second">The seconds value.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short GetTimeDateDelegate(short use4DigitYearCode, out short year, out short month, out short day, out short hour, out short minute,
                                                     out short second);
        /// <summary>
        /// Delegate declaration for the SetTimeDate() method of VcuCommunication32.dll/VcuCommunication64.dll. This method sets the time and date on
        /// VCU. This delegate is also used for the Set4TimeDate() method that sets the time and date on VCU's that use 4 digit year codes.
        /// </summary>
        /// <param name="use4DigitYearCode">A flag that specifies whether the Vehicle Control Unit uses a 2 or 4 digit year code. True (0x01), if it
        /// uses a 4 digit year code; otherwise, false (0x00).</param>
        /// <param name="year">The year, as a value between 0-99. Values above 70 are assumed to be associated with years 1970-1999, values below 70
        /// are assumed to be associated with years 2000-2069.</param>
        /// <param name="month">The month value.</param>
        /// <param name="day">The day value.</param>
        /// <param name="hour">The hours value.</param>
        /// <param name="minute">The minutes value</param>
        /// <param name="second">The seconds value.</param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short SetTimeDateDelegate(short use4DigitYearCode, short year, short month, short day, short hour, short minute, short second);

        /// <summary>
        /// Delegate declaration for the SetCarID() method of VcuCommunication32.dll/VcuCommunication64.dll. This method sets the car identifier on the
        /// VCU.
        /// </summary>
        /// <param name="carIdentifier"></param>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short SetCarIDDelegate(string carIdentifier);

        /// <summary>
        /// Delegate declaration for the StartClock() method of VcuCommunication32.dll/VcuCommunication64.dll. This method starts the real time clock
        /// on the VCU.
        /// </summary>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short StartClockDelegate();

        /// <summary>
        /// Delegate declaration for the StopClock() method of VcuCommunication32.dll/VcuCommunication64.dll. This method stops the real time clock on
        /// the VCU.
        /// </summary>
        /// <returns>Zero, if successful; otherwise, an error code.</returns>
        protected delegate short StopClockDelegate();

        /// <summary>
        /// Delegate declaration for the SetWatchSize() method of VcuCommunication32.dll/VcuCommunication64.dll. This method sets the number of watch
        /// variables associated with the project.
        /// </summary>
        /// <param name="watchSize">The number of watch variables associated with the project. Watch sizes of {20, 40 and 80} are currently 
        /// supported.</param>
        /// <returns>The number of watch variables currently supported by PTUDLL32.</returns>
        protected delegate short SetWatchSizeDelegate(short watchSize);
        #endregion --- Delegate Declarations ---

        #region --- Constants ---
        /// <summary>
        /// The parameter value required for the UpdateWatchElements() function of the PTUDLL32 dynamic link library to force an update of all watch
        /// elements. Value: 1.
        /// </summary>
        protected const short ForceUpdateTrue = 1;

        /// <summary>
        /// The parameter value required for the UpdateWatchElements() function of the PTUDLL32 dynamic link library not to force an update of all
        /// watch elements. Value: 0.
        /// </summary>
        protected const short ForceUpdateFalse = 0;

        /// <summary>
        /// The default baud rate of the serial communication link. Value: Baud.Baud019200.
        /// </summary>
        /// <remarks>Only applicable to serial communication.</remarks>
        protected const Baud DefaultBaud = Baud.Baud019200;

        /// <summary>
        /// The default parity of the serial communication link. Value: Parity.None.
        /// </summary>
        protected const Parity DefaultParity = Parity.None;

        /// <summary>
        /// The default number of bits per character of the serial communication link. Value: BitsPerCharacter.Eight.
        /// </summary>
        protected const BitsPerCharacter DefaultBitsPerCharacter = BitsPerCharacter.Eight;

        /// <summary>
        /// The default number of stop bits of the serial communication link. Value: StopBits.One.
        /// </summary>
        protected const StopBits DefaultStopBits = StopBits.One;

        /// <summary>
        /// The default number of ms to wait before releasing the communication mutex.
        /// </summary>
        protected const int DefaultMutexWaitDurationMs = 2000;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Mutex to control read/write access to the <c>CommunicationInterface</c> used to communicate with the VCU.
        /// </summary>
        protected Mutex m_MutexCommuncationInterface;

        /// <summary>
        /// Random number generator.
        /// </summary>
        protected Random m_Random;

        /// <summary>
        /// A table of the current watch elements, i.e. those watch variables that are currently monitored by the target hardware, mapped by element
        /// index.
        /// </summary>
        /// <remarks>Each watch element contains the: watch identifier; corresponding data type and current value of the watch variable being
        /// monitored.</remarks>
        protected WatchElement_t[] m_WatchElements;

        /// <summary>
        /// The communication settings associated with the selected target.
        /// </summary>
        protected CommunicationSetting_t m_CommunicationSetting;

        /// <summary>
        /// A flag that specifies whether the application is running on a 64 bit operating system. True, if
        /// running on a 64 bit operating system; otherwise, false.
        /// </summary>
        protected bool m_Is64BitOperatingSystem = false;

        #region --- Function Delegates  ---
        /// <summary>
        ///  Delegate for the InitCommunication() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected InitCommunicationDelegate m_InitCommunication;

        /// <summary>
        ///  Delegate for the CloseCommunication() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected CloseCommunicationDelegate m_CloseCommunication;

        /// <summary>
        ///  Delegate for the SendVariable() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SendVariableDelegate m_SendVariable;

        /// <summary>
        /// Delegate for the SetWatchElements() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SetWatchElementsDelegate m_SetWatchElements;

        /// <summary>
        /// Delegate for the UpdateWatchElements() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected UpdateWatchElementsDelegate m_UpdateWatchElements;

        /// <summary>
        /// Delegate for the GetEmbeddedInformation() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetEmbeddedInformationDelegate m_GetEmbeddedInformation;

        /// <summary>
        /// Delegate for the GetChartMode() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetChartModeDelegate m_GetChartMode;

        /// <summary>
        /// Delegate for the SetChartMode() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SetChartModeDelegate m_SetChartMode;

        /// <summary>
        /// Delegate for the GetChartIndex() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetChartIndexDelegate m_GetChartIndex;

        /// <summary>
        /// Delegate for the SetChartIndex() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SetChartIndexDelegate m_SetChartIndex;

        /// <summary>
        /// Delegate for the SetChartScale() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SetChartScaleDelegate m_SetChartScale;

        /// <summary>
        /// Delegate for the GetTimeDate() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetTimeDateDelegate m_GetTimeDate;

        /// <summary>
        /// Delegate for the SetTimeDate() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SetTimeDateDelegate m_SetTimeDate;

        /// <summary>
        /// Delegate for the SetCarID() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SetCarIDDelegate m_SetCarID;

        /// <summary>
        /// Delegate for the StartClock() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected StartClockDelegate m_StartClock;

        /// <summary>
        /// Delegate for the StopClock() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected StopClockDelegate m_StopClock;

        /// <summary>
        /// Delegate for the SetWatchSize() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SetWatchSizeDelegate m_SetWatchSize;
        #endregion --- Function Delegates ---
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class and set the function delegates, properties and member variables.
        /// </summary>
        public CommunicationParent()
        {
            // Random number generator, used to simulate communication values.
            m_Random = new Random();

            // Initialize the watch element array.
            m_WatchElements = new WatchElement_t[Parameter.WatchSize];

            m_CommunicationSetting = new CommunicationSetting_t();
            m_MutexCommuncationInterface = new Mutex();

            #region - [Initialize VcuCommunication.comm.cpp Function Delegates] -
            // ----------------------------------------------------------------------
            // Initialize the function delegates to either the VcuCommunication32.dll  
            // or VcuCommunication64.dll functions depending upon whether the 
            // Windows operating system is 64 bit or 32 bit.
            // ----------------------------------------------------------------------
            if (Environment.Is64BitOperatingSystem == true)
            {
                m_Is64BitOperatingSystem = true;

                // CommunicationParent
                m_InitCommunication = VcuCommunication64.InitCommunication;
                m_CloseCommunication = VcuCommunication64.CloseCommunication;
                m_GetEmbeddedInformation = VcuCommunication64.GetEmbeddedInformation;
                m_GetChartMode = VcuCommunication64.GetChartMode;
                m_SetChartMode = VcuCommunication64.SetChartMode;
                m_GetChartIndex = VcuCommunication64.GetChartIndex;
                m_SetChartIndex = VcuCommunication64.SetChartIndex;
                m_SetChartScale = VcuCommunication64.SetChartScale;
                m_SetWatchSize = VcuCommunication64.SetWatchSize;

                // CommunicationWatch
                m_SendVariable = VcuCommunication64.SendVariable;
                m_SetWatchElements = VcuCommunication64.SetWatchElements;
                m_UpdateWatchElements = VcuCommunication64.UpdateWatchElements;

                // CommunicationPTUApplication
                m_GetTimeDate = VcuCommunication64.GetTimeDate;
                m_SetTimeDate = VcuCommunication64.SetTimeDate;
                m_SetCarID = VcuCommunication64.SetCarID;

                // Not Used
                m_StartClock = VcuCommunication64.StartClock;
                m_StopClock = VcuCommunication64.StopClock;
            }
            else
            {
                // CommunicationParent
                m_InitCommunication = VcuCommunication32.InitCommunication;
                m_CloseCommunication = VcuCommunication32.CloseCommunication;
                m_GetEmbeddedInformation = VcuCommunication32.GetEmbeddedInformation;
                m_GetChartMode = VcuCommunication32.GetChartMode;
                m_SetChartMode = VcuCommunication32.SetChartMode;
                m_GetChartIndex = VcuCommunication32.GetChartIndex;
                m_SetChartIndex = VcuCommunication32.SetChartIndex;
                m_SetChartScale = VcuCommunication32.SetChartScale;
                m_SetWatchSize = VcuCommunication32.SetWatchSize;

                // CommunicationWatch
                m_SendVariable = VcuCommunication32.SendVariable;
                m_SetWatchElements = VcuCommunication32.SetWatchElements;
                m_UpdateWatchElements = VcuCommunication32.UpdateWatchElements;

                // CommunicationPTUApplication
                m_GetTimeDate = VcuCommunication32.GetTimeDate;
                m_SetTimeDate = VcuCommunication32.SetTimeDate;
                m_SetCarID = VcuCommunication32.SetCarID;

                // Not Used
                m_StartClock = VcuCommunication32.StartClock;
                m_StopClock = VcuCommunication32.StopClock;
            }
            #endregion - [Initialize VcuCommunication.comm.cpp Function Delegates] -
        }

        /// <summary>
        /// Initialize a new instance of the class and set the function delegates, properties and member variables.
        /// </summary>
        /// <param name="communicationSetting">The communication setting that is to be used to initialize the <c>CommunicationSetting</c>
        /// property.</param>
        public CommunicationParent(CommunicationSetting_t communicationSetting) : this()
        {
            m_CommunicationSetting = communicationSetting;
        }

        /// <summary>
        /// Initialize a new instance of the class and set the function delegates, properties and member variables.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface containing the properties and member variables that are to
        /// be used to initialize the class.</param>
        public CommunicationParent(ICommunicationParent communicationInterface) : this()
        {
            m_CommunicationSetting = communicationInterface.CommunicationSetting;
        }
        #endregion --- Constructors ---

        #region --- Disposal ---
        #endregion --- Disposal ---

        #region --- Methods ---
        /// <summary>
        /// Initialize the communication port.
        /// </summary>
        /// <param name="communicationsSetting">The communication settings.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.InitCommunication() method is
        /// not CommunicationError.Success.</exception>
        public virtual void InitCommunication(CommunicationSetting_t communicationsSetting)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_InitCommunication != null, "CommunicationParent.InitCommunication() - [m_InitCommunication != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                errorCode = (CommunicationError)m_InitCommunication((short)communicationsSetting.Protocol, communicationsSetting.PortIdentifier,
                                                                    (int)communicationsSetting.SerialCommunicationParameters.BaudRate,
                                                                    (short)communicationsSetting.SerialCommunicationParameters.BitsPerCharacter,
                                                                    (short)communicationsSetting.SerialCommunicationParameters.Parity,
                                                                    (short)communicationsSetting.SerialCommunicationParameters.StopBits);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMPortInitializationFailed, errorCode);
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.InitCommunication_t initCommunication =
                    new DebugMode.InitCommunication_t(communicationsSetting.Protocol,
                                                      communicationsSetting.PortIdentifier,
                                                      communicationsSetting.SerialCommunicationParameters.BaudRate,
                                                      communicationsSetting.SerialCommunicationParameters.BitsPerCharacter,
                                                      communicationsSetting.SerialCommunicationParameters.Parity,
                                                      communicationsSetting.SerialCommunicationParameters.StopBits,
                                                      errorCode);
                DebugMode.Write(initCommunication.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMPortInitializationFailed, errorCode);
            }
        }

        /// <summary>
        /// Close the communication port.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.CloseCommunication() method is
        /// not CommunicationError.Success.</exception>
        public virtual void CloseCommunication(Protocol protocol)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_CloseCommunication != null, "CommunicationParent.CloseCommunication() - [m_CloseCommunication != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                errorCode = (CommunicationError)m_CloseCommunication((short)protocol);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMPortCloseFailed, errorCode);
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.CloseCommunication_t closeCommunication = new DebugMode.CloseCommunication_t(protocol, errorCode);
                DebugMode.Write(closeCommunication.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
				throw new CommunicationException(Resources.EMPortCloseFailed, errorCode);
            }
        }

        /// <summary>
        /// Get the embedded software information.
        /// </summary>
        /// <param name="targetConfiguration">The target configuration information retrieved from the target.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetEmbeddedInformation() method
        /// is not CommunicationError.Success.</exception>
        public void GetEmbeddedInformation(out TargetConfiguration_t targetConfiguration)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetEmbeddedInformation != null, "CommunicationParent.GetEmbeddedInformation() - [m_GetEmbeddedInformation != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationParent.GetEmbeddedInformation() - [m_MutexCommuncation != null]");

            targetConfiguration = new TargetConfiguration_t();
            TargetConfiguration_t localTargetConfiguration = new TargetConfiguration_t();
            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetEmbeddedInformation(   out localTargetConfiguration.Version,
                                                                            out localTargetConfiguration.CarIdentifier,
                                                                            out localTargetConfiguration.SubSystemName,
                                                                            out localTargetConfiguration.ProjectIdentifier,
                                                                            out localTargetConfiguration.ConversionMask);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMGetTargetConfigurationFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetEmbeddedInformation_t getEmbeddedInformation =
                    new DebugMode.GetEmbeddedInformation_t( localTargetConfiguration.Version,
                                                            localTargetConfiguration.CarIdentifier,
                                                            localTargetConfiguration.SubSystemName,
                                                            localTargetConfiguration.ProjectIdentifier,
                                                            localTargetConfiguration.ConversionMask,
                                                            errorCode);
                DebugMode.Write(getEmbeddedInformation.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMGetTargetConfigurationFailed, errorCode);
            }

            targetConfiguration.Version = localTargetConfiguration.Version.Trim();
            targetConfiguration.CarIdentifier = localTargetConfiguration.CarIdentifier.Trim();
            targetConfiguration.SubSystemName = localTargetConfiguration.SubSystemName.Trim();
            targetConfiguration.ProjectIdentifier = localTargetConfiguration.ProjectIdentifier.Trim();
            targetConfiguration.ConversionMask = localTargetConfiguration.ConversionMask;
        }

        /// <summary>
        /// Get the mode of the chart recorder. 
        /// </summary>
        /// <returns>The mode of the chart recorder: ramp, zero-output, full-scale, data.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetChartMode() method is
        /// not CommunicationError.Success.</exception>
        public ChartMode GetChartMode()
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetChartMode != null, "CommunicationParent.GetChartMode() - [m_GetChartMode != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationParent.GetChartMode() - [m_MutexCommuncationInterface != null]");

            ChartMode chartMode = ChartMode.Undefined;
            short chartModeAsShort = 0;
            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetChartMode(out chartModeAsShort);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMChartModeGetFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetChartMode_t getChartMode = new DebugMode.GetChartMode_t((ChartMode)chartModeAsShort, errorCode);
                DebugMode.Write(getChartMode.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMChartModeGetFailed, errorCode);
            }

            chartMode = (ChartMode)chartModeAsShort;
            return chartMode;
        }

        /// <summary>
        /// Set the mode of the chart recorder.
        /// </summary>
        /// <param name="chartMode">The required mode of the chart recorder.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetChartMode() method is
        /// not CommunicationError.Success.</exception>
        public void SetChartMode(ChartMode chartMode)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_SetChartMode != null, "CommunicationParent.SetChartMode() - [m_SetChartMode != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationParent.SetChartMode() - [m_MutexCommuncationInterface != null]");

            short chartModeAsShort = (short)chartMode;
            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_SetChartMode(chartModeAsShort);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMChartModeSetFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.SetChartMode_t setChartMode = new DebugMode.SetChartMode_t((ChartMode)chartModeAsShort, errorCode);
                DebugMode.Write(setChartMode.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMChartModeSetFailed, errorCode);
            }
        }

        /// <summary>
        /// Assign the specified watch variable to the specified chart recorder channel index.
        /// </summary>
        /// <param name="channelIndex">The chart recorder channel index.</param>
        /// <param name="watchIdentifier">The watch identifier of the watch variable that is to be assigned to the channel.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from a call to the PTUDLL32.SetChartIndex() method is not
        /// CommunicationError.Success.</exception>
        public void SetChartIndex(short channelIndex, short watchIdentifier)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_SetChartIndex != null, "CommunicationParent.SetChartIndex() - [m_SetChartIndex != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationParent.SetChartIndex() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_SetChartIndex(channelIndex, watchIdentifier);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMChartModeSetFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.SetChartIndex_t setChartIndex = new DebugMode.SetChartIndex_t(channelIndex, watchIdentifier, errorCode);
                DebugMode.Write(setChartIndex.ToXML());
            }

            short chartRecorderChannel;
            if (errorCode != CommunicationError.Success)
            {
                chartRecorderChannel = (short)(channelIndex + 1);
                throw new CommunicationException(string.Format(Resources.EMSetChartIndexFailed, chartRecorderChannel.ToString()), errorCode);
            }
        }

        /// <summary>
        /// Set the chart scaling for the specified watch variable.
        /// </summary>
        /// <param name="watchIdentifier">The watch identifier of the watch variables that is to be scaled.</param>
        /// <param name="maxChartScale">The watch variable engineering value associated with the maximum Y axis value.</param>
        /// <param name="minChartScale">The watch variable engineering value associated with the minimum Y axis value.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from a call to the PTUDLL32.SetChartScale() method is
        /// not CommunicationError.Success.</exception>
        /// <exception cref="ArgumentException">Thrown if the specified <paramref name="watchIdentifier"/> does not exist in the current
        /// data dictionary.</exception>"
        public void SetChartScale(short watchIdentifier, double maxChartScale, double minChartScale)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_SetChartScale != null, "CommunicationParent.SetChartScale() - [m_SetChartScale != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationParent.SetChartScale() - [m_MutexCommuncationInterface != null]");

            // Convert the engineering values back to raw Values.
            WatchVariable watchVariable;
            double scaleFactor = 0, maxChartScaleRaw = 0, minChartScaleRaw = 0;
            try
            {
                watchVariable = Lookup.WatchVariableTable.RecordList[watchIdentifier];
                if (watchVariable == null)
                {
                    throw new ArgumentException("CommunicationParent.SetChartScale()", "watchIdentifier");
                }

                scaleFactor = watchVariable.ScaleFactor;
                if (scaleFactor > 0)
                {
                    minChartScaleRaw = minChartScale / scaleFactor;
                    maxChartScaleRaw = maxChartScale / scaleFactor;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("CommunicationParent.SetChartScale()", "watchIdentifier");
            }

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_SetChartScale(watchIdentifier, maxChartScaleRaw, minChartScaleRaw);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMSetChartScaleFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.SetChartScale_t setChartScale = new DebugMode.SetChartScale_t(watchIdentifier, maxChartScaleRaw, minChartScaleRaw, errorCode);
                DebugMode.Write(setChartScale.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMSetChartScaleFailed, errorCode);
            }
        }

        /// <summary>
        /// Download the specified chart recorder workset.
        /// </summary>
        /// <param name="workset">The workset that is to be downloaded to the VCU.</param>
        public void DownloadChartRecorderWorkset(Workset_t workset)
        {
            WatchVariable watchVariable;
            short watchIdentifier;
            double chartScaleUpperLimit, chartScaleLowerLimit;
            for (short channelIndex = 0; channelIndex < Parameter.WatchSizeChartRecorder; channelIndex++)
            {
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.RecordList[workset.Column[0].OldIdentifierList[channelIndex]];
                    if (watchVariable == null)
                    {
                        throw new Exception("CommunicationParent.DownloadChartRecorderWorkset() - [watchVariable == null]");
                    }

                    watchIdentifier = watchVariable.Identifier;
                    chartScaleUpperLimit = workset.Column[0].ChartScaleList[channelIndex].ChartScaleUpperLimit;
                    chartScaleLowerLimit = workset.Column[0].ChartScaleList[channelIndex].ChartScaleLowerLimit;
                }
                catch (Exception)
                {
                    watchIdentifier = CommonConstants.WatchIdentifierNotDefined;
                    chartScaleUpperLimit = 0;
                    chartScaleLowerLimit = 0;
                }

                SetChartIndex(channelIndex, watchIdentifier);
                SetChartScale(watchIdentifier, chartScaleUpperLimit, chartScaleLowerLimit);
            }
        }

        /// <summary>
        /// Set the number of watch variables associated with the project.
        /// </summary>
        /// <param name="watchSize">The number of watch variables associated with the project. Watch sizes of {20, 40 and 80} are currently
        /// supported.</param>
        /// <returns>The number of watch variables currently supported by VcuCommunication32/VcuCommunication64.</returns>
        public short SetWatchSize(short watchSize)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_SetWatchSize != null, "CommunicationParent.SetWatchSize() - [m_SetWatchSize != null]");

            short watchReturn = m_SetWatchSize(watchSize);
            return watchReturn;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the communication settings associated with the selected target.
        /// </summary>
        public CommunicationSetting_t CommunicationSetting
        {
            get { return m_CommunicationSetting; }
            set { m_CommunicationSetting = value; }
        }
        #endregion --- Properties ---
    }
}
