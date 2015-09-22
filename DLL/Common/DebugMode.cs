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
 *  File name:  DebugMode.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  02/08/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/28/11    1.1     K.McD           1.  Moved the Century constant to the CommonConstants class.
 *  
 *  06/21/11    1.2     K.McD           1.  Re-ordered the structures to match the order of the communication interfaces.
 *                                      2.  Added support for the self test communication methods.
 *                                      
 *  06/23/11    1.3     K.McD           1.  Added support for the following communication methods: GetChartMode(), SetChartMode(), SetChartIndex() and SetChartScale().
 *  
 *  07/20/11    1.4     K.McD           1.  Modified the ToXML() method of the GetSelfTestResult_t structure to use an int for the Tag element of the 
 *                                          InteractiveResults_t structure rather than short.
 *                                          
 *  06/19/13    1.5     K.McD           1.  Bug Fix. Now Specifies the fully qualified filename of the debug file in the call to StreamWriter() 
 *                                          within the Open() method by adding the start-up path to the default filename.
 *  
 */
#endregion --- Revision History ---

using System;
using System.IO;
using System.Text;

using Common.Communication;
using Common.Properties;

namespace Common
{
    /// <summary>
    /// A class to support debugging of the parameter values associated with calls to the methods within the PTUDLL32 dynamic link library.
    /// </summary>
    public static class DebugMode
    {
        #region --- Structures ---
        #region - [CommunicationParent] -
        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.InitCommunication() method.
        /// </summary>
        public struct InitCommunication_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The protocol type.
            /// </summary>
            public Protocol Protocol;

            /// <summary>
            /// The port identifier.
            /// </summary>
            public string PortID;

            /// <summary>
            /// The baud rate.
            /// </summary>
            public Baud BaudRate;

            /// <summary>
            /// The number of bits per character.
            /// </summary>
            public BitsPerCharacter BitsPerByte;

            /// <summary>
            /// The parity.
            /// </summary>
            public Parity Parity;

            /// <summary>
            /// The number of stop bits.
            /// </summary>
            public StopBits StopBits;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="protocol">The protocol type.</param>
            /// <param name="portID">The port identifier.</param>
            /// <param name="baudRate">The baud rate.</param>
            /// <param name="bitsPerByte">The number of bits per character.</param>
            /// <param name="parity">The parity.</param>
            /// <param name="stopBits">The number of stop bits.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public InitCommunication_t(Protocol protocol, string portID, Baud baudRate, BitsPerCharacter bitsPerByte, Parity parity, StopBits stopBits, CommunicationError errorCode)
            {
                Signature = "short InitCommunication(short protocol, string portID, int baudRate, short bitsPerByte, short parity, short stopBits)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                Protocol = protocol;
                PortID = portID; ;
                BaudRate = baudRate; ;
                BitsPerByte = bitsPerByte;
                Parity = parity;
                StopBits = stopBits;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<InitCommunication>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<Protocol>{0}</Protocol>\r\n", Protocol.ToString());
                stringBuilder.AppendFormat("\t\t<PortID>{0}</PortID>\r\n", PortID);
                stringBuilder.AppendFormat("\t\t<BaudRate>{0}</BaudRate>\r\n", BaudRate.ToString());
                stringBuilder.AppendFormat("\t\t<BitsPerByte>{0}</BitsPerByte>\r\n", BitsPerByte.ToString());
                stringBuilder.AppendFormat("\t\t<Parity>{0}</Parity>\r\n", Parity.ToString());
                stringBuilder.AppendFormat("\t\t<StopBits>{0}</StopBits>\r\n", StopBits.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</InitCommunication>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.CloseCommunication() method.
        /// </summary>
        public struct CloseCommunication_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The protocol type.
            /// </summary>
            public Protocol Protocol;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="protocol">The protocol type.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public CloseCommunication_t(Protocol protocol, CommunicationError errorCode)
            {
                Signature = "short CloseCommunication(short protocol)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                Protocol = protocol;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<CloseCommunication>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<Protocol>{0}</Protocol>\r\n", Protocol.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</CloseCommunication>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.GetEmbeddedInformation() method.
        /// </summary>
        public struct GetEmbeddedInformation_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The version reference of the embedded software.
            /// </summary>
            public String SoftwareVersion;

            /// <summary>
            /// The car identifier.
            /// </summary>
            public String CarID;

            /// <summary>
            /// The sub-system name.
            /// </summary>
            public String SubSystemName;

            /// <summary>
            /// The project identifier.
            /// </summary>
            public String IdentifierString;

            /// <summary>
            /// The conversion mask.
            /// </summary>
            public double ConversionMask;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="softwareVersion">The version reference of the embedded software.</param>
            /// <param name="carID">The car identifier.</param>
            /// <param name="subSystemName">The sub-system name.</param>
            /// <param name="identifierString">The project identifier.</param>
            /// <param name="conversionMask">The conversion mask.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetEmbeddedInformation_t(String softwareVersion, String carID, String subSystemName, String identifierString, double conversionMask, CommunicationError errorCode)
            {
                Signature = "short GetEmbeddedInformation(out String softwareVersion, out String carID, out String subSystemName, out String identifierString, out double conversionMask);";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                SoftwareVersion = softwareVersion;
                CarID = carID;
                SubSystemName = subSystemName; 
                IdentifierString = identifierString;
                ConversionMask = conversionMask;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetEmbeddedInformation>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<SoftwareVersion>{0}</SoftwareVersion>\r\n", SoftwareVersion);
                stringBuilder.AppendFormat("\t\t<CarID>{0}</CarID>\r\n", CarID);
                stringBuilder.AppendFormat("\t\t<SubSystemName>{0}</SubSystemName>\r\n", SubSystemName);
                stringBuilder.AppendFormat("\t\t<IdentifierString>{0}</IdentifierString>\r\n", IdentifierString);
                stringBuilder.AppendFormat("\t\t<ConversionMask>{0}</ConversionMask>\r\n", ConversionMask.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetEmbeddedInformation>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.GetChartMode() method.
        /// </summary>
        public struct GetChartMode_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The current chart mode.
            /// </summary>
            public ChartMode ChartMode;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="chartMode">The current chart mode.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetChartMode_t(ChartMode chartMode, CommunicationError errorCode)
            {
                Signature = "short GetChartMode(ref ChartMode chartMode);";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                ChartMode = chartMode;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetChartMode>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ChartMode>{0}</ChartMode>\r\n", ChartMode);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetChartMode>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.SetChartMode() method.
        /// </summary>
        public struct SetChartMode_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The current chart mode.
            /// </summary>
            public ChartMode ChartMode;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="chartMode">The current chart mode.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public SetChartMode_t(ChartMode chartMode, CommunicationError errorCode)
            {
                Signature = "short SetChartMode(ChartMode chartMode);";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                ChartMode = chartMode;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<SetChartMode>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ChartMode>{0}</ChartMode>\r\n", ChartMode);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</SetChartMode>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.SetChartIndex() method.
        /// </summary>
        public struct SetChartIndex_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The chart recorder channel index that is to be configured.
            /// </summary>
            public short ChannelIndex;

            /// <summary>
            /// The watch identifier of the watch variable that is to be assigned to the channel.
            /// </summary>
            public short WatchIdentifier;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="channelIndex">The chart recorder channel index.</param>
            /// <param name="watchIdentifier">The watch identifier of the watch variable that is to be assigned to the channel.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public SetChartIndex_t(short channelIndex, short watchIdentifier, CommunicationError errorCode)
            {
                Signature = "short SetChartIndex(short channelIndex, short watchIdentifier);";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                ChannelIndex = channelIndex;
                WatchIdentifier = watchIdentifier;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<SetChartIndex>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ChannelIndex>{0}</ChannelIndex>\r\n", ChannelIndex);
                stringBuilder.AppendFormat("\t\t<WatchIdentifier>{0}</WatchIdentifier>\r\n", WatchIdentifier);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</SetChartIndex>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.SetChartScale() method.
        /// </summary>
        public struct SetChartScale_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The watch identifier of the watch variable that is to be assigned to the channel.
            /// </summary>
            public short WatchIdentifier;

            /// <summary>
            /// The watch variable raw value associated with the maximum Y axis value.
            /// </summary>
            public double MaxChartScaleRaw;

            /// <summary>
            /// The watch variable raw value associated with the minimum Y axis value.
            /// </summary>
            public double MinChartScaleRaw;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="watchIdentifier">The watch identifier of the watch variables that is to be scaled.</param>
            /// <param name="maxChartScaleRaw">The watch variable raw value associated with the maximum Y axis value.</param>
            /// <param name="minChartScaleRaw">The watch variable raw value associated with the minimum Y axis value.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public SetChartScale_t(short watchIdentifier, double maxChartScaleRaw, double minChartScaleRaw, CommunicationError errorCode)
            {
                Signature = "short watchIdentifier, double maxChartScale, double minChartScale);";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                WatchIdentifier = watchIdentifier;
                MaxChartScaleRaw = maxChartScaleRaw;
                MinChartScaleRaw = minChartScaleRaw;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<SetChartScale>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<WatchIdentifier>{0}</WatchIdentifier>\r\n", WatchIdentifier);
                stringBuilder.AppendFormat("\t\t<MaxChartScaleRaw>{0}</MaxChartScaleRaw>\r\n", MaxChartScaleRaw);
                stringBuilder.AppendFormat("\t\t<MinChartScaleRaw>{0}</MinChartScaleRaw>\r\n", MinChartScaleRaw);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</SetChartScale>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }
        #endregion - [CommunicationParent] -

        #region - [CommunicationWatch] -
        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.SendVariable() method.
        /// </summary>
        public struct SendVariable_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The watch identifier of the variable that is to be updated.
            /// </summary>
            public short DictionaryIndex;

            /// <summary>
            /// The data type of the watch variable that is to be updated.
            /// </summary>
            public short DataType;

            /// <summary>
            /// The new value of the watch variable that is to be updated.
            /// </summary>
            public double Data;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="dictionaryIndex">The watch identifier of the variable that is to be updated.</param>
            /// <param name="dataType">The data type of the watch variable that is to be updated.</param>
            /// <param name="data">The new value of the watch variable that is to be updated.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public SendVariable_t(short dictionaryIndex, short dataType, double data, CommunicationError errorCode)
            {
                Signature = "short SendVariable(short dictionaryIndex, short dataType, double data)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                DictionaryIndex = dictionaryIndex;
                DataType = dataType;
                Data = data;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<SendVariable>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<DictionaryIndex>{0}</DictionaryIndex>\r\n", DictionaryIndex.ToString());
                stringBuilder.AppendFormat("\t\t<DataType>{0}</DataType>\r\n", DataType.ToString());
                stringBuilder.AppendFormat("\t\t<Data>{0}</Data>\r\n", Data.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</SendVariable>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.SetWatchElements() method.
        /// </summary>
        public struct SetWatchElements_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The watch identifiers associated with the watch elements.
            /// </summary>
            public short[] WatchElements;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="watchElements">The watch identifiers associated with the watch elements.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public SetWatchElements_t(short[] watchElements, CommunicationError errorCode)
            {
                Signature = "short SetWatchElements(short[] watchElements)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                WatchElements = watchElements;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<SetWatchElements>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendLine(GetXML<short>("WatchElements", WatchElements));
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</SetWatchElements>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.UpdateWatchElements() method.
        /// </summary>
        public struct UpdateWatchElements_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// Flag to control whether all data values are to be updated. 0x01 to force update; otherwise, 0x0.
            /// </summary>
            public short ForceUpdate;

            /// <summary>
            /// The values of the watch variables.
            /// </summary>
            public double[] WatchValues;

            /// <summary>
            /// The data types of the watch variables.
            /// </summary>
            public short[] WatchDataTypes;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="forceUpdate">Flag to control whether all data values are to be updated. 0x01 to force update; otherwise, 0x0.</param>
            /// <param name="watchValues">The values of the watch variables.</param>    
            /// <param name="watchDataTypes">The data types of the watch variables.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public UpdateWatchElements_t(short forceUpdate, double[] watchValues, short[] watchDataTypes, CommunicationError errorCode)
            {
                Signature = "short UpdateWatchElements(short forceUpdate, double[] watchValues, short[] watchDataTypes)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                ForceUpdate = forceUpdate;
                WatchValues = watchValues;
                WatchDataTypes = watchDataTypes;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<UpdateWatchElements>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ForceUpdate>{0}</ForceUpdate>\r\n", ForceUpdate.ToString());
                stringBuilder.AppendLine(GetXML<double>("WatchValues", WatchValues));
                stringBuilder.AppendLine(GetXML<short>("WatchDataTypes", WatchDataTypes));
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</UpdateWatchElements>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }
        #endregion - [CommunicationWatch] -

        #region - [CommunicationEvent] -
        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.ChangeEventLog() method.
        /// </summary>
        public struct ChangeEventLog_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The index of the event log.
            /// </summary>
            public short EventLogIndex;

            /// <summary>
            /// The base interval, in ms, between samples.
            /// </summary>
            public short SampleIntervalMs;

            /// <summary>
            /// The change status of the log.
            /// </summary>
            public short ChangeStatus;

            /// <summary>
            /// The maximum number of tasks supported by the log.
            /// </summary>
            public short MaxTasks;

            /// <summary>
            /// The maximum number of events per task.
            /// </summary>
            public short MaxEventsPerTask;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="eventLogIndex">The index of the event log.</param>
            /// <param name="sampleIntervalMs">The base interval, in ms, between samples.</param>
            /// <param name="changeStatus">The change status of the log.</param>
            /// <param name="maxTasks">The maximum number of tasks supported by the log.</param>
            /// <param name="maxEventsPerTask">The maximum number of events per task.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public ChangeEventLog_t(short eventLogIndex, short sampleIntervalMs, short changeStatus, short maxTasks, short maxEventsPerTask, CommunicationError errorCode)
            {
                Signature = "short ChangeEventLog(short eventLogIndex, out short sampleIntervalMs, out short changeStatus, out short maxTasks, out short maxEventsPerTask)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                EventLogIndex = eventLogIndex;
                SampleIntervalMs = sampleIntervalMs;
                ChangeStatus = changeStatus;
                MaxTasks = maxTasks;
                MaxEventsPerTask = maxEventsPerTask;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<ChangeEventLog>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<EventLogIndex>{0}</EventLogIndex>\r\n", EventLogIndex.ToString());
                stringBuilder.AppendFormat("\t\t<SampleIntervalMs>{0}</SampleIntervalMs>\r\n", SampleIntervalMs.ToString());
                stringBuilder.AppendFormat("\t\t<ChangeStatus>{0}</ChangeStatus>\r\n", ChangeStatus.ToString());
                stringBuilder.AppendFormat("\t\t<MaxTasks>{0}</MaxTasks>\r\n", MaxTasks.ToString());
                stringBuilder.AppendFormat("\t\t<MaxEventsPerTask>{0}</MaxEventsPerTask>\r\n", MaxEventsPerTask.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</ChangeEventLog>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.LoadFaultlog() method.
        /// </summary>
        public struct LoadFaultlog_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The number of events associated with the log.
            /// </summary>
            public short EventCount;

            /// <summary>
            /// The old index value.
            /// </summary>
            public uint OldIndex;

            /// <summary>
            /// The new index value.
            /// </summary>
            public uint NewIndex;
            #endregion - [Member Variables] -

            #region - [Constructors] 
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="eventCount">The number of events associated with the log.</param>
            /// <param name="oldIndex">The old index value.</param>
            /// <param name="newIndex">The new index value.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public LoadFaultlog_t(short eventCount, uint oldIndex, uint newIndex, CommunicationError errorCode)
            {
                Signature = "short LoadFaultlog(out short eventCount, out uint oldIndex, out uint newIndex)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                EventCount = eventCount;
                OldIndex = oldIndex;
                NewIndex = newIndex;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<LoadFaultlog>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<EventCount>{0}</EventCount>\r\n", EventCount.ToString());
                stringBuilder.AppendFormat("\t\t<OldIndex>{0}</OldIndex>\r\n", OldIndex.ToString());
                stringBuilder.AppendFormat("\t\t<NewIndex>{0}</NewIndex>\r\n", NewIndex.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</LoadFaultlog>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.GetFaultHdr() method.
        /// </summary>
        public struct GetFaultHdr_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The event index.
            /// </summary>
            public short EventIndex;

            /// <summary>
            /// The event identifier.
            /// </summary>
            public short EventIdentifier;

            /// <summary>
            /// The task identifier.
            /// </summary>
            public short TaskIdentifier;

            /// <summary>
            /// The time of the event.
            /// </summary>
            public string Time;

            /// <summary>
            /// The date of the event.
            /// </summary>
            public string Date;

            /// <summary>
            /// The stream number associated with the event, if available; otherwise -1.
            /// </summary>
            public short StreamSaved;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="eventIndex">The event index.</param>
            /// <param name="eventIdentifier">The event identifier.</param>
            /// <param name="taskIdentifier">The task identifier.</param>
            /// <param name="time">The time of the event.</param>
            /// <param name="date">The date of the event.</param>
            /// <param name="streamSaved">The stream number associated with the event, if available; otherwise -1.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetFaultHdr_t(short eventIndex, short eventIdentifier, short taskIdentifier, String time, String date, short streamSaved, CommunicationError errorCode)
            {
                Signature = "short GetFaultHdr(short eventIndex, out short eventIdentifier, out short taskIdentifier, out String time, out String date, out short streamSaved)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                EventIndex = eventIndex;
                EventIdentifier = eventIdentifier;
                TaskIdentifier = taskIdentifier;
                Time = time;
                Date = date;
                StreamSaved = streamSaved;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetFaultHdr>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<EventIndex>{0}</EventIndex>\r\n", EventIndex.ToString());
                stringBuilder.AppendFormat("\t\t<EventIdentifier>{0}</EventIdentifier>\r\n", EventIdentifier.ToString());
                stringBuilder.AppendFormat("\t\t<TaskIdentifier>{0}</TaskIdentifier>\r\n", TaskIdentifier).ToString();
                stringBuilder.AppendFormat("\t\t<Time>{0}</Time>\r\n", Time);
                stringBuilder.AppendFormat("\t\t<Date>{0}</Date>\r\n", Date);
                stringBuilder.AppendFormat("\t\t<StreamSaved>{0}</StreamSaved>\r\n", StreamSaved.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetFaultHdr>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.GetEventLog() method.
        /// </summary>
        public struct GetEventLog_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The index of the current event log.
            /// </summary>
            public short EventLogIndex;

            /// <summary>
            /// The number of events associated with the current event log.
            /// </summary>
            public short EventLogCount;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="eventLogIndex">The index of the current event log.</param>
            /// <param name="eventLogCount">The number of events associated with the current event log.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetEventLog_t(short eventLogIndex, short eventLogCount, CommunicationError errorCode)
            {
                Signature = "short GetEventLog(out short eventLogIndex, out short eventLogCount)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                EventLogIndex = eventLogIndex;
                EventLogCount = eventLogCount;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetEventLog>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<EventLogIndex>{0}</EventLogIndex>\r\n", EventLogIndex.ToString());
                stringBuilder.AppendFormat("\t\t<EventLogCount>{0}</EventLogCount>\r\n", EventLogCount.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetEventLog>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.FreeEventLogMemory() method.
        /// </summary>
        public struct FreeEventLogMemory_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="errorCode">The error code returned from the call.</param>
            public FreeEventLogMemory_t(CommunicationError errorCode)
            {
                Signature = "short FreeEventLogMemory()";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<FreeEventLogMemory>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</FreeEventLogMemory>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.ExitEventLog() method.
        /// </summary>
        public struct ExitEventLog_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="errorCode">The error code returned from the call.</param>
            public ExitEventLog_t(CommunicationError errorCode)
            {
                Signature = "short ExitEventLog()";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<ExitEventLog>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</ExitEventLog>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.InitializeEventLog() method.
        /// </summary>
        public struct InitializeEventLog_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="errorCode">The error code returned from the call.</param>
            public InitializeEventLog_t(CommunicationError errorCode)
            {
                Signature = "short ExitEventLog()";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<InitializeEventLog>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</InitializeEventLog>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.ClearEvent() method.
        /// </summary>
        public struct ClearEvent_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="errorCode">The error code returned from the call.</param>
            public ClearEvent_t(CommunicationError errorCode)
            {
                Signature = "short ExitEventLog()";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<ClearEvent>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</ClearEvent>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.GetFaultVar() method.
        /// </summary>
        public struct GetFaultVar_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The event index.
            /// </summary>
            public short EventIndex;

            /// <summary>
            /// The number of event variables associated with the event.
            /// </summary>
            public short EventVariableCount;

            /// <summary>
            /// The data types associated with each event variable.
            /// </summary>
            public short[] DataTypes;

            /// <summary>
            /// The values of the event variables.
            /// </summary>
            public double[] Values;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="eventIndex">The event index.</param>
            /// <param name="eventVariableCount">The number of event variables associated with the event.</param>
            /// <param name="dataTypes">The data types associated with each event variable.</param>
            /// <param name="values">The values of the event variables.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetFaultVar_t(short eventIndex, short eventVariableCount, short[] dataTypes, double[] values, CommunicationError errorCode)
            {
                Signature = "short GetFaultVar(short eventIndex, short eventVariableCount, short[] dataTypes, double[] values)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                EventIndex = eventIndex;
                EventVariableCount = eventVariableCount;
                DataTypes = dataTypes;
                Values = values;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetFaultVar>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<EventIndex>{0}</EventIndex>\r\n", EventIndex.ToString());
                stringBuilder.AppendFormat("\t\t<EventVariableCount>{0}</EventVariableCount>\r\n", EventVariableCount.ToString());
                stringBuilder.AppendLine(GetXML<short>("DataTypes", DataTypes));
                stringBuilder.AppendLine(GetXML<double>("Values", Values));
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetFaultVar>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.GetDefaultStreamInformation() method.
        /// </summary>
        public struct GetDefaultStreamInformation_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The number of watch variables included in the data stream.
            /// </summary>
            public short WatchVariableCount;

            /// <summary>
            /// The number of data samples associated with the data stream.
            /// </summary>
            public short SampleCount;

            /// <summary>
            /// The multiple of the base interval at which the data is recorded.
            /// </summary>
            public short SampleMultiple;

            /// <summary>
            /// The watch identifiers associated with the data stream.
            /// </summary>
            public short[] WatchIdentifiers;

            /// <summary>
            /// The data type corresponding to each watch variable contained within the data stream.
            /// </summary>
            public short[] DataTypes;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
            /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
            /// <param name="sampleMultiple">The multiple of the base interval at which the data is recorded.</param>
            /// <param name="watchIdentifiers">The watch identifiers associated with the data stream.</param>
            /// <param name="dataTypes">The data type corresponding to each watch variable contained within the data stream.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetDefaultStreamInformation_t(short watchVariableCount, short sampleCount, short sampleMultiple, short[] watchIdentifiers, short[] dataTypes, CommunicationError errorCode)
            {
                Signature = "short GetDefaultStreamInformation(out short watchVariableCount, out short sampleCount, out short sampleMultiple, short[] watchIdentifiers, short[] dataTypes)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                WatchVariableCount = watchVariableCount;
                SampleCount = sampleCount;
                SampleMultiple = sampleMultiple;
                WatchIdentifiers = watchIdentifiers;
                DataTypes = dataTypes;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetDefaultStreamInformation>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<WatchVariableCount>{0}</WatchVariableCount>\r\n", WatchVariableCount.ToString());
                stringBuilder.AppendFormat("\t\t<SampleCount>{0}</SampleCount>\r\n", SampleCount.ToString());
                stringBuilder.AppendFormat("\t\t<SampleMultiple>{0}</SampleMultiple>\r\n", SampleMultiple.ToString());
                stringBuilder.AppendLine(GetXML<short>("WatchIdentifiers", WatchIdentifiers));
                stringBuilder.AppendLine(GetXML<short>("DataTypes", DataTypes));
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetDefaultStreamInformation>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.GetStreamInformation() method.
        /// </summary>
        public struct GetStreamInformation_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The stream number for which the stream information is required.
            /// </summary>
            public short StreamNumber;

            /// <summary>
            /// The number of watch variables included in the data stream.
            /// </summary>
            public short WatchVariableCount;

            /// <summary>
            /// The number of data samples associated with the data stream.
            /// </summary>
            public short SampleCount;

            /// <summary>
            /// The multiple of the base interval at which the data is recorded.
            /// </summary>
            public short SampleMultiple;

            /// <summary>
            /// The watch identifiers associated with the data stream.
            /// </summary>
            public short[] WatchIdentifiers;

            /// <summary>
            /// The data type corresponding to each watch variable contained within the data stream.
            /// </summary>
            public short[] DataTypes;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="streamNumber">The stream number for which the stream information is required.</param>
            /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
            /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
            /// <param name="sampleMultiple">The multiple of the base interval at which the data is recorded.</param>
            /// <param name="watchIdentifiers">The watch identifiers associated with the data stream.</param>
            /// <param name="dataTypes">The data type corresponding to each watch variable contained within the data stream.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetStreamInformation_t(short streamNumber, short watchVariableCount, short sampleCount, short sampleMultiple, short[] watchIdentifiers, short[] dataTypes, CommunicationError errorCode)
            {
                Signature = "short GetStreamInformation(short streamNumber, out short watchVariableCount, out short sampleCount, out short sampleMultiple, short[] watchIdentifiers, short[] dataTypes)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                StreamNumber = streamNumber;
                WatchVariableCount = watchVariableCount;
                SampleCount = sampleCount;
                SampleMultiple = sampleMultiple;
                WatchIdentifiers = watchIdentifiers;
                DataTypes = dataTypes;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetStreamInformation>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<StreamNumber>{0}</StreamNumber>\r\n", StreamNumber.ToString());
                stringBuilder.AppendFormat("\t\t<WatchVariableCount>{0}</WatchVariableCount>\r\n", WatchVariableCount.ToString());
                stringBuilder.AppendFormat("\t\t<SampleCount>{0}</SampleCount>\r\n", SampleCount.ToString());
                stringBuilder.AppendFormat("\t\t<SampleMultiple>{0}</SampleMultiple>\r\n", SampleMultiple.ToString());
                stringBuilder.AppendLine(GetXML<short>("WatchIdentifiers", WatchIdentifiers));
                stringBuilder.AppendLine(GetXML<short>("DataTypes", DataTypes));
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetStreamInformation>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.GetStream() method.
        /// </summary>
        public struct GetStream_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The stream number for which the stream information is required.
            /// </summary>
            public short StreamNumber;

            /// <summary>
            /// The values of the watch variables.
            /// </summary>
            public int[] DataBuffer;

            /// <summary>
            /// The start time of the plot.
            /// </summary>
            public short TimeOrigin;

            /// <summary>
            /// The number of watch variables included in the data stream.
            /// </summary>
            public short WatchVariableCount;

            /// <summary>
            /// The number of data samples associated with the data stream.
            /// </summary>
            public short SampleCount;

            /// <summary>
            /// The data type corresponding to each watch variable contained within the data stream.
            /// </summary>
            public short[] DataTypes;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="streamNumber">The stream number.</param>
            /// <param name="dataBuffer">The values of the watch variables.</param>
            /// <param name="timeOrigin">The start time of the plot.</param>
            /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
            /// <param name="sampleCount">The number of data samples associated with the data stream.</param>
            /// <param name="dataTypes">The data type corresponding to each watch variable contained within the data stream.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetStream_t(short streamNumber, int[] dataBuffer, short timeOrigin, short watchVariableCount, short sampleCount, short[] dataTypes, CommunicationError errorCode)
            {
                Signature = "short GetStream(short streamNumber, int[] dataBuffer, out short timeOrigin, short watchVariableCount, short sampleCount, short[] dataTypes)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                StreamNumber = streamNumber;
                DataBuffer = dataBuffer;
                TimeOrigin = timeOrigin;
                WatchVariableCount = watchVariableCount;
                SampleCount = sampleCount;
                DataTypes = dataTypes;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetStream>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<StreamNumber>{0}</StreamNumber>\r\n", StreamNumber.ToString());
                stringBuilder.AppendLine(GetXML<int>("DataBuffer", DataBuffer));
                stringBuilder.AppendFormat("\t\t<TimeOrigin>{0}</TimeOrigin>\r\n", TimeOrigin.ToString());
                stringBuilder.AppendFormat("\t\t<WatchVariableCount>{0}</WatchVariableCount>\r\n", WatchVariableCount.ToString());
                stringBuilder.AppendFormat("\t\t<SampleCount>{0}</SampleCount>\r\n", SampleCount.ToString());
                stringBuilder.AppendLine(GetXML<short>("DataTypes", DataTypes));
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetStream>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.SetDefaultStreamInformation() method.
        /// </summary>
        public struct SetDefaultStreamInformation_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The number of watch variables included in the data stream.
            /// </summary>
            public short WatchVariableCount;

            /// <summary>
            /// The multiple of the base interval at which the data is recorded.
            /// </summary>
            public short SampleMultiple;

            /// <summary>
            /// The watch identifiers associated with the data stream.
            /// </summary>
            public short[] WatchIdentifiers;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="watchVariableCount">The number of watch variables included in the data stream.</param>
            /// <param name="sampleMultiple">The multiple of the base interval at which the data is recorded.</param>
            /// <param name="watchIdentifiers">The watch identifiers associated with the data stream.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public SetDefaultStreamInformation_t(short watchVariableCount, short sampleMultiple, short[] watchIdentifiers, CommunicationError errorCode)
            {
                Signature = "short SetDefaultStreamInformation(short watchVariableCount, short sampleMultiple, short[] watchIdentifiers)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                WatchVariableCount = watchVariableCount;
                SampleMultiple = sampleMultiple;
                WatchIdentifiers = watchIdentifiers;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<SetDefaultStreamInformation>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<WatchVariableCount>{0}</WatchVariableCount>\r\n", WatchVariableCount.ToString());
                stringBuilder.AppendFormat("\t\t<SampleMultiple>{0}</SampleMultiple>\r\n", SampleMultiple.ToString());
                stringBuilder.AppendLine(GetXML<short>("watchIdentifiers", WatchIdentifiers));
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</SetDefaultStreamInformation>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32.CheckFaultlogger() method.
        /// </summary>
        public struct CheckFaultlogger_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The number of events associated with the log.
            /// </summary>
            public short EventCount;

            /// <summary>
            /// The index value for any new event.
            /// </summary>
            public uint NewIndex;

            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="eventCount">The number of events associated with the log.</param>
            /// <param name="newIndex">The index value for any new event.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public CheckFaultlogger_t(short eventCount, uint newIndex, CommunicationError errorCode)
            {
                Signature = "short CheckFaultlogger(ref short eventCount, out uint newIndex)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
                EventCount = eventCount;
                NewIndex = newIndex;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<CheckFaultlogger>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<EventCount>{0}</EventCount>\r\n", EventCount.ToString());
                stringBuilder.AppendFormat("\t\t<NewIndex>{0}</NewIndex>\r\n", NewIndex.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</CheckFaultlogger>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }
        // TODO DebugMode.Structures: [Event] GetFltFlagInfo(), SetFaultFlags(), GetFltHistInfo().
        #endregion - [CommunicationEvent] -

        #region - [CommunicationSelfTest] -
        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.GetSelfTestSpecialMessage() method.
        /// </summary>
        public struct GetSelfTestSpecialMessage_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
            /// 'readon' parameter applies and (3) represents an unknown error.
            /// </summary>
            public short Result;

            /// <summary>
            /// A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS</c> table 
            /// of the data dictionary in order to determine the error message returned from the VCU.
            /// </summary>
            public short Reason;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
            /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
            /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS</c> table 
            /// of the data dictionary in order to determine the error message returned from the VCU.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetSelfTestSpecialMessage_t(short result, short reason, CommunicationError errorCode)
            {
                Signature = "short GetSelfTestSpecialMessage(out short result, out short reason)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;

                Result = result;
                Reason = reason;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetSelfTestSpecialMessage>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<Result>{0}</Result>\r\n", Result.ToString());
                stringBuilder.AppendFormat("\t\t<Reason>{0}</Reason>\r\n", Reason.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetSelfTestSpecialMessage>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.StartSelfTestTask() method.
        /// </summary>
        public struct StartSelfTestTask_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
            /// 'reason' parameter applies and (3) represents an unknown error.
            /// </summary>
            public short Result;

            /// <summary>
            /// A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS</c> table 
            /// of the data dictionary in order to determine the error message returned from the VCU.
            /// </summary>
            public short Reason;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
            /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
            /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS</c> table 
            /// of the data dictionary in order to determine the error message returned from the VCU.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public StartSelfTestTask_t(short result, short reason, CommunicationError errorCode)
            {
                Signature = "short StartSelfTestTask(out short result, out short reason)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;

                Result = result;
                Reason = reason;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<StartSelfTestTask>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<Result>{0}</Result>\r\n", Result.ToString());
                stringBuilder.AppendFormat("\t\t<Reason>{0}</Reason>\r\n", Reason.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</StartSelfTestTask>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.ExitSelfTestTask() method.
        /// </summary>
        public struct ExitSelfTestTask_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
            /// 'reason' parameter applies and (3) represents an unknown error.
            /// </summary>
            public short Result;

            /// <summary>
            /// A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS</c> table 
            /// of the data dictionary in order to determine the error message returned from the VCU.
            /// </summary>
            public short Reason;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
            /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
            /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS</c> table 
            /// of the data dictionary in order to determine the error message returned from the VCU.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public ExitSelfTestTask_t(short result, short reason, CommunicationError errorCode)
            {
                Signature = "short ExitSelfTestTask(out short result, out short reason)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;

                Result = result;
                Reason = reason;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<ExitSelfTestTask>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<Result>{0}</Result>\r\n", Result.ToString());
                stringBuilder.AppendFormat("\t\t<Reason>{0}</Reason>\r\n", Reason.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</ExitSelfTestTask>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.AbortSTSequence() method.
        /// </summary>
        public struct AbortSTSequence_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="errorCode">The error code returned from the call.</param>
            public AbortSTSequence_t(CommunicationError errorCode)
            {
                Signature = "short AbortSTSequence()";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<AbortSTSequence>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</AbortSTSequence>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.SendOperatorAcknowledge() method.
        /// </summary>
        public struct SendOperatorAcknowledge_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="errorCode">The error code returned from the call.</param>
            public SendOperatorAcknowledge_t(CommunicationError errorCode)
            {
                Signature = "short SendOperatorAcknowledge()";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<SendOperatorAcknowledge>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</SendOperatorAcknowledge>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.UpdateSTTestList() method.
        /// </summary>
        public struct UpdateSTTestList_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The number of self tests that are defined in the list.
            /// </summary>
            public short TestCount;

            /// <summary>
            /// An array of self test identifiers that define which self tests are included in the list.
            /// </summary>
            public short[] Tests;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <remarks>This method will define the list of self-tests that are to be executed once the tester selects the execute command. The self tests are defined 
            /// using the self test identifiers defined in the data dictionary.</remarks>
            /// <param name="testCount">The number of tests in the list.</param>
            /// <param name="tests">A list of the selfTestIdentifiers.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public UpdateSTTestList_t(short testCount, short[] tests, CommunicationError errorCode)
            {
                Signature = "short UpdateSTTestList(short testCount, short[] tests)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;

                TestCount = testCount;
                Tests = tests;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<UpdateSTTestList>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<TestCount>{0}</TestCount>\r\n", TestCount.ToString());
                stringBuilder.AppendLine(GetXML<short>("Tests", Tests));
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</UpdateSTTestList>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.RunPredefinedSTTests() method.
        /// </summary>
        public struct RunPredefinedSTTests_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The test list identifier of the predefined self tests that are to be executed.
            /// </summary>
            public short TestListIdentifier;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <remarks>This method will define the list of self-tests that are to be executed once the tester selects the execute command. The self tests are defined 
            /// using the self test identifiers defined in the data dictionary.</remarks>
            /// <param name="testListIdentifier">The test list identifier of the predefined self tests that are to be executed.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public RunPredefinedSTTests_t(short testListIdentifier, CommunicationError errorCode)
            {
                Signature = "short RunPredefinedSTTests(short testListIdentifier)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;

                TestListIdentifier = testListIdentifier;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<RunPredefinedSTTests>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<TestListIdentifier>{0}</TestListIdentifier>\r\n", TestListIdentifier.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</RunPredefinedSTTests>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.UpdateSTLoopCount() method.
        /// </summary>
        public struct UpdateSTLoopCount_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The number of cycles/loops of the defined tests that are to be performed.
            /// </summary>
            public short LoopCount;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="loopCount">The number of cycles/loops of the defined tests that are to be performed.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public UpdateSTLoopCount_t(short loopCount, CommunicationError errorCode)
            {
                Signature = "short UpdateSTLoopCount(short loopCount)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;

                LoopCount = loopCount;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<UpdateSTLoopCount>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<LoopCount>{0}</LoopCount>\r\n", LoopCount.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</UpdateSTLoopCount>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.ExecuteSTTestList() method.
        /// </summary>
        public struct ExecuteSTTestList_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The truck to which the self tests apply. This does not apply on the CTA project, separate self-tests are set up for each truck.
            /// </summary>
            public TruckInformation TruckInformation;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="truckInformation">The truck to which the self tests apply. This does not apply on the CTA project, separate self-tests are set up for each truck.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public ExecuteSTTestList_t(TruckInformation truckInformation, CommunicationError errorCode)
            {
                Signature = "short ExecuteSTTestList(TruckInformation truckInformation)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;

                TruckInformation = truckInformation;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<ExecuteSTTestList>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<TruckInformation>{0}</TruckInformation>\r\n", TruckInformation.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</ExecuteSTTestList>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.GetSelfTestResult() method.
        /// </summary>
        public struct GetSelfTestResult_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// A flag to indicate whether a valid result is available. A value of 1 indicates that a valid result is available; otherwise, 
            /// 0.
            /// </summary>
            public short ResultAvailable;
            
            /// <summary>
            /// An enumerator that defines the type of message returned from the VCU.
            /// </summary>
            public MessageMode MessageMode;
            
            /// <summary>
            /// The test identifier associated with the message.
            /// </summary>
            public short TestID;
            
            /// <summary>
            /// The test case number associated with the message.
            /// </summary>
            public short TestCase;
            
            /// <summary>
            /// Used with the passive and logic tests to define whether the test passed or failed. A value of 1 indicates that the test passed, 
            /// whereas a value of 2 indicates that the test failed.
            /// </summary>
            public short TestResult;
            
            /// <summary>
            /// An enumerator to define the truck information associated with the message.
            /// </summary>
            public TruckInformation TruckInformation;
            
            /// <summary>
            /// The number of self test variables associated with the message.
            /// </summary>
            public short VariableCount;

            /// <summary>
            /// An array containing the interactive results for the current test.
            /// </summary>
            public InteractiveResults_t[] Results;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <param name="resultAvailable">A flag to indicate whether a valid result is available. A value of 1 indicates that a valid result is available; otherwise, 
            /// 0.</param>
            /// <param name="messageMode">The type of message returned from the VCU.</param>
            /// <param name="testIdentifier">The test result identifier; the interpretation of this value is dependent upon the message mode. For detailed messages, this 
            /// value represents the self test identifier.</param>
            /// <param name="testCase">The test case number associated with the message.</param>
            /// <param name="testResult">Used with the passive and logic self tests to define whether the test passed or failed. A value of 1 indicates that the test 
            /// passed; otherwise, the test failed.</param>
            /// <param name="truckInformation">An enumerator to define the truck information associated with the message.</param>
            /// <param name="variableCount">The number of variables associated with the message.</param>
            /// <param name="results">An array containing the interactive results for the current test.</param>
            /// <param name="errorCode">The error code returned from the call.</param>
            public GetSelfTestResult_t(short resultAvailable, MessageMode messageMode, short testIdentifier, short testCase, short testResult, TruckInformation truckInformation, short variableCount, InteractiveResults_t[] results, CommunicationError errorCode)
            {
                Signature = "short GetSelfTestResult(out short resultAvailable, out MessageMode messageMode, out short testIdentifier, out short testCase, out short testResult, out TruckInformation truckInformation, out short variableCount, out InteractiveResults_t[] results)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;

                ResultAvailable = resultAvailable;
                MessageMode = messageMode;
                TestID = testIdentifier;
                TestCase = testCase;
                TestResult = testResult;
                TruckInformation = truckInformation;
                VariableCount = variableCount;
                Results = results;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<GetSelfTestResult>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<ResultAvailable>{0}</ResultAvailable>\r\n", ResultAvailable);
                stringBuilder.AppendFormat("\t\t<MessageMode>{0}</MessageMode>\r\n", MessageMode);
                stringBuilder.AppendFormat("\t\t<TestID>{0}</TestID>\r\n", TestID);
                stringBuilder.AppendFormat("\t\t<TestCase>{0}</TestCase>\r\n", TestCase);
                stringBuilder.AppendFormat("\t\t<TestResult>{0}</TestResult>\r\n", TestResult);
                stringBuilder.AppendFormat("\t\t<TruckInformation>{0}</TruckInformation>\r\n", TruckInformation.ToString());
                stringBuilder.AppendFormat("\t\t<VariableCount>{0}</VariableCount>\r\n", VariableCount.ToString());

                // ---------------------------------
                // Process the interactive results.
                // ---------------------------------
                if (VariableCount > 0)
                {
                    try
                    {
                        double[] value = new double[VariableCount];
                        int[] tag = new int[VariableCount];
                        for (int index = 0; index < VariableCount; index++)
                        {
                            value[index] = Results[index].Value;
                            tag[index] = Results[index].Tag;
                        }

                        stringBuilder.AppendLine(GetXML<double>("Value", value));
                        stringBuilder.AppendLine(GetXML<int>("Tag", tag));
                    }
                    catch (Exception)
                    {
                        // Don't do anything, just ensure that an exception isn't thrown.
                    }
                }

                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</GetSelfTestResult>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }

        /// <summary>
        /// The structure used to contain the parameter information associated with the PTUDLL32SelfTest.UpdateSTMode() method.
        /// </summary>
        public struct UpdateSTMode_t
        {
            #region - [Member Variables] -
            /// <summary>
            /// The signature of the call.
            /// </summary>
            public string Signature;

            /// <summary>
            /// The time stamp associated with the call.
            /// </summary>
            public DateTime TimeStamp;

            /// <summary>
            /// The error code returned from the call.
            /// </summary>
            public CommunicationError ErrorCode;

            /// <summary>
            /// The required self test mode.
            /// </summary>
            public SelfTestMode SelfTestMode;
            #endregion - [Member Variables] -

            #region - [Constructors] -
            /// <summary>
            /// Initializes a new instance of the structure.
            /// </summary>
            /// <remarks>This call is used to check whether communication with the VCU has been lost.</remarks>
            /// <param name="selfTestMode">The required self test mode.</param>
            /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
            /// <param name="errorCode">The error code returned from the call.</param>
            public UpdateSTMode_t(SelfTestMode selfTestMode, CommunicationError errorCode)
            {
                Signature = "short UpdateSTMode(SelfTestMode selfTestMode)";
                TimeStamp = DateTime.Now;
                ErrorCode = errorCode;

                SelfTestMode = selfTestMode;
            }
            #endregion - [Constructors] -

            #region - [Methods] -
            /// <summary>
            /// Convert the structure to an XML element.
            /// </summary>
            /// <returns>The structure converted to an XML element.</returns>
            public string ToXML()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("\t<UpdateSTMode>\r\n");
                stringBuilder.AppendFormat("\t\t<TimeStamp>{0}</TimeStamp>\r\n", TimeStamp.ToString("HH:mm:ss.fff"));
                stringBuilder.AppendFormat("\t\t<Signature>{0}</Signature>\r\n", Signature);
                stringBuilder.AppendFormat("\t\t<SelfTestMode>{0}</SelfTestMode>\r\n", SelfTestMode.ToString());
                stringBuilder.AppendFormat("\t\t<ErrorCode>{0}</ErrorCode>\r\n", ErrorCode.ToString());
                stringBuilder.AppendFormat("\t</UpdateSTMode>\r\n");
                return stringBuilder.ToString();
            }
            #endregion - [Methods] -
        }
        #endregion - [CommunicationSelfTest]
        #endregion --- Structures ---

        #region --- Constants ---
        /// <summary>
        /// The format specifier to be used when displaying debug information in decimal format. Value: "d3".
        /// </summary>
        private const string DecimalFormatSpecifier = "d3";

        /// <summary>
        /// The XML header associated with the debug log.
        /// </summary>
        private const string XMLHeader = "<?xml version=\"1.0\"?>\r\n<LogFile xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">";
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The <c>StreamWriter</c> reference corresponding to the debug mode log file.
        /// </summary>
        private static StreamWriter m_LogFile;

        /// <summary>
        /// Flag to control whether debug mode is enabled. True, to enable debug mode; otherwise, false.
        /// </summary>
        private static bool m_Enabled;

        /// <summary>
        /// The time that the current log was started.
        /// </summary>
        private static DateTime m_StartTime;
        #endregion --- Member Variables ---

        #region --- Methods ---
        /// <summary>
        /// Open the debug mode log file so that new debug information can be written to the file.
        /// </summary>
        public static void Open()
        {
            m_LogFile = new StreamWriter(System.Windows.Forms.Application.StartupPath + CommonConstants.BindingFilename + Resources.FilenameDefaultDebugFile, false);
            m_StartTime = DateTime.Now;

            // Write the debug log header text.
            StringBuilder stringBuilder = new StringBuilder();

            // The XML header.
            stringBuilder.AppendLine(XMLHeader);

            // The time stamp element.
            stringBuilder.AppendFormat("<DateTime{0:D2}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}>",
                                                    m_StartTime.Year % CommonConstants.Century,
                                                    m_StartTime.Month,
                                                    m_StartTime.Day,
                                                    m_StartTime.Hour,
                                                    m_StartTime.Minute,
                                                    m_StartTime.Second);
            m_LogFile.WriteLine(stringBuilder);
            m_Enabled = true;
        }

        /// <summary>   
        /// Close the debug mode log file.
        /// </summary>
        public static void Close()
        {
            // Skip if the log file has not been opened.
            if (m_LogFile != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("</DateTime{0:D2}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}>",
                                                    m_StartTime.Year % CommonConstants.Century,
                                                    m_StartTime.Month,
                                                    m_StartTime.Day,
                                                    m_StartTime.Hour,
                                                    m_StartTime.Minute,
                                                    m_StartTime.Second);

                m_LogFile.WriteLine(stringBuilder);
                m_LogFile.WriteLine("</LogFile>");
                m_LogFile.Close();
            }

            m_LogFile = null;
            m_Enabled = false;
        }

        /// <summary>
        /// Write the specified string to the log file.
        /// </summary>
        /// <param name="text">The text string that is to be written.</param>
        public static void Write(string text)
        {
            // Skip if the log file has not been opened.
            if (m_LogFile != null)
            {
                m_LogFile.Write(text);
            }
        }

        /// <summary>
        /// Gets the XML element corresponding to the specified array.
        /// </summary>
        /// <typeparam name="T">The object type associated with each element of the array.</typeparam>
        /// <param name="tagName">The XML tag name for the array.</param>
        /// <param name="array">The array values.</param>
        /// <returns>The XML string corresponding to the array.</returns>
        public static string GetXML<T>(string tagName, T[] array)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("\t\t<{0}>\r\n", tagName);
            for (int index = 0; index < array.Length; index++)
            {
                stringBuilder.AppendFormat("\t\t\t<I{0}>{1}</I{0}>\r\n", index.ToString(DecimalFormatSpecifier), array[index].ToString());
            }

            stringBuilder.AppendFormat("\t\t</{0}>", tagName);
            return stringBuilder.ToString();
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the flag that controls whether debug mode is enabled. True, to enable debud mode; otherwise, false.
        /// </summary>
        public static bool Enabled
        {
            get { return m_Enabled; }
        }
        #endregion --- Properties ---
    }
}
