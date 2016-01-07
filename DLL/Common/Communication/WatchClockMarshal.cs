#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2016    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    Common
 * 
 *  File name:  WatchClockMarshal.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author       Comments
 *  03/01/2015  1.0     D.Smail      First Release.
 *
 */
#endregion --- Revision History ---


using System;
using System.Text;
using VcuComm;

namespace Common.Communication
{
    /// <summary>
    /// This class is a replacement of the unmanaged DLL used to access embedded target configuration 
    /// information, watch variables, and the embedded target real time clock. 
    /// 
    /// NOTE: There are no try{} catch{} in this class because all exceptions thrown here are handled by the 
    /// calling object methods.
    /// </summary>
    public class WatchClockMarshal
    {
        #region --- Member Variables ---

        /// <summary>
        /// The type of communication platform used. The 2 available options currently available are serial
        /// or TCP.
        /// </summary>
        private ICommDevice m_CommDevice;

        /// <summary>
        /// Stores the information received from the target when a data request is made. This array is then
        /// parsed and the information is extracted based on the type of data request that is made.
        /// </summary>
        private Byte[] m_RxMessage = new Byte[1024];

        /// <summary>
        /// Object used to handle the standard embedded target communication protocol
        /// </summary>
        private PtuTargetCommunication m_PtuTargetCommunication;

        #endregion --- Member Variables ---

        #region --- Constructors ---

        /// <summary>
        /// Constructor that initializes a new instance of the CommGen class
        /// </summary>
        /// <param name="device">The communication vehicle used to access the PTU target (VCU)</param>
        public WatchClockMarshal(ICommDevice device)
        {
            m_CommDevice = device;
            m_PtuTargetCommunication = new PtuTargetCommunication();
        }

        /// <summary>
        /// Private 0 argument constructor that forces the instantiation of this class
        /// to use a non-private constructor.
        /// </summary>
        private WatchClockMarshal()
        {
            // Intentionally empty
        }
        #endregion --- Constructors ---

        #region --- Methods ---

        /// <summary>
        /// Method requests and retrieves from the embedded target the chart variable information based on
        /// the chart index provided. The PTU variable index is retrieved
        /// </summary>
        /// <param name="ChartIndex">The chart variable index (starting at 0) and not to equal or exceed the amount
        /// of chart recorder variables</param>
        /// <param name="VariableIndex">The variable index that is currently part of the chart recorder outputs</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError GetChartIndex(Int16 ChartIndex, ref Int16 VariableIndex)
        {
            ProtocolPTU.GetChartIndexReq request = new ProtocolPTU.GetChartIndexReq((Byte)ChartIndex);

            CommunicationError commError = m_PtuTargetCommunication.SendDataRequestToEmbedded(m_CommDevice, request, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            // Get the desired variable index from the receive message
            VariableIndex = BitConverter.ToInt16(m_RxMessage, 8);

            // check for endianess
            if (m_CommDevice.IsTargetBigEndian())
            {
                VariableIndex = Utils.ReverseByteOrder(VariableIndex);
            }

            return CommunicationError.Success;
        }

 

        /// <summary>
        /// This method retrieves the current chart recorder mode from the embedded target. The supported modes are
        /// ramp, zero-output, full-scale output, and data. 
        /// </summary>
        /// <param name="CurrentChartMode">The current mode of the chart recorder</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError GetChartMode(ref Int16 CurrentChartMode)
        {
            CommunicationError commError = m_PtuTargetCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_CHART_MODE, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            CurrentChartMode = BitConverter.ToInt16(m_RxMessage, 8);

            // check for endianess
            if (m_CommDevice.IsTargetBigEndian())
            {
                CurrentChartMode = Utils.ReverseByteOrder(CurrentChartMode);
            }

            // Use only the lower byte
            CurrentChartMode &= 0x00FF;

            return CommunicationError.Success;
        }

        /// <summary>
        /// Gets the embedded information stored on the target which is used to determine the project
        /// car ID and software version.
        /// </summary>
        /// <param name="getEmbInfo">structure that stores all of the target information, which includes project,
        /// version number, car ID, etc.</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError GetEmbeddedInformation(ref ProtocolPTU.GetEmbeddedInfoRes getEmbInfo)
        {
            CommunicationError commError = m_PtuTargetCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_EMBEDDED_INFORMATION, m_RxMessage);

            if (commError == CommunicationError.Success)
            {
                // Map bytes in m_RxMessage to GetEmbeddedInfoRes
                getEmbInfo.SoftwareVersion = Encoding.UTF8.GetString(m_RxMessage, 8, 41).Replace("\0", String.Empty);
                getEmbInfo.CarID = Encoding.UTF8.GetString(m_RxMessage, 49, 11).Replace("\0", String.Empty);
                getEmbInfo.SubSystemName = Encoding.UTF8.GetString(m_RxMessage, 60, 41).Replace("\0", String.Empty);
                getEmbInfo.IdentifierString = Encoding.UTF8.GetString(m_RxMessage, 101, 5).Replace("\0", String.Empty);
                getEmbInfo.ConfigurationMask = BitConverter.ToUInt32(m_RxMessage, 106);
            }

            return commError;
        }

        /// <summary>
        /// This method requests the current date and time as maintained on the embedded target.
        /// </summary>
        /// <param name="Use4DigitYearCode">true if a 4 digit (2 byte) year code is used; false if only 2 digits (1 byte) is used</param>
        /// <param name="Year">Year</param>
        /// <param name="Month">Month</param>
        /// <param name="Day">Day</param>
        /// <param name="Hour">Hour</param>
        /// <param name="Minute">Minute</param>
        /// <param name="Second">Second</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError GetTimeDate(Boolean Use4DigitYearCode, ref Int16 Year, ref Byte Month, ref Byte Day, ref Byte Hour, ref Byte Minute, ref Byte Second)
        {
            CommunicationError commError = m_PtuTargetCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_TIME_DATE, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            Hour = m_RxMessage[8];
            Minute = m_RxMessage[9];
            Second = m_RxMessage[10];

            // There's a different structure orientation based o whether or not a four digit year is being used
            if (Use4DigitYearCode)
            {
                Year = BitConverter.ToInt16(m_RxMessage, 12);
                Month = m_RxMessage[14];
                Day = m_RxMessage[15];
            }
            else
            {
                Year = m_RxMessage[11];
                Month = m_RxMessage[12];
                Day = m_RxMessage[13];
            }

            // check for endianess
            if (m_CommDevice.IsTargetBigEndian())
            {
                Hour = Utils.ReverseByteOrder(Hour);
                Minute = Utils.ReverseByteOrder(Minute);
                Second = Utils.ReverseByteOrder(Second);
                Year = Utils.ReverseByteOrder(Year);
                Month = Utils.ReverseByteOrder(Month);
                Day = Utils.ReverseByteOrder(Day);
            }

            return CommunicationError.Success;
        }

        /// <summary>
        /// Write the specified data to the watch variable specified by the <paramref name="DictionaryIndex"/> parameter.
        /// </summary>
        /// <param name="DictionaryIndex">The dictionary index.</param>
        /// <param name="DataType">The data type.</param>
        /// <param name="Data">The value of the data to be written.</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SendVariable(Int16 DictionaryIndex, Int16 DataType, Double Data)
        {
            UInt32 data = (UInt32)Data;

            ProtocolPTU.SendVariableReq request = new ProtocolPTU.SendVariableReq(DictionaryIndex, data);

            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="NewCarID"></param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SetCarID(String NewCarID)
        {
#if TODO
            ProtocolPTU.SetCarIDReq request = new ProtocolPTU.SetCarIDReq(NewCarID);

            CommunicationError commError = SendCommandToEmbedded(request);

            return commError;
#else
            return CommunicationError.Success;
#endif
        }

        /// <summary>
        /// Assign the specified watch variable to the specified chart recorder channel index.
        /// </summary>
        /// <param name="ChartIndex">The chart recorder channel index.</param>
        /// <param name="VariableIndex">The watch identifier of the watch variable that is to be assigned to the channel.</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SetChartIndex(Int16 ChartIndex, Int16 VariableIndex)
        {
            ProtocolPTU.SetChartIndexReq request = new ProtocolPTU.SetChartIndexReq(ChartIndex, VariableIndex);

            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        /// Sets the chart mode of the chart recorder outputs
        /// </summary>
        /// <param name="TargetChartMode">the desired chart mode (data, ramp, full scale or zero)</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SetChartMode(Int16 TargetChartMode)
        {
            ProtocolPTU.SetChartModeReq request = new ProtocolPTU.SetChartModeReq((byte)TargetChartMode);

            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        /// Set the chart scaling for the specified watch variable.
        /// </summary>
        /// <param name="DictionaryIndex">The watch identifier of the watch variables that is to be scaled.</param>
        /// <param name="MaxScale">The watch variable engineering value associated with the maximum Y axis value.</param>
        /// <param name="MinScale">The watch variable engineering value associated with the minimum Y axis value.</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SetChartScale(Int16 DictionaryIndex, Double MaxScale, Double MinScale)
        {
            Int32 maxScale = (Int32)MaxScale;
            Int32 minScale = (Int32)MinScale;

            ProtocolPTU.SetChartScaleReq request = new ProtocolPTU.SetChartScaleReq(DictionaryIndex, maxScale, minScale);

            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        /// This method updates the embedded target real time clock with the desired date and time. 
        /// </summary>
        /// <param name="Use4DigitYearCode">true if the embedded target expects a 4 digit year code; false otherwise</param>
        /// <param name="Year">Year</param>
        /// <param name="Month">Month</param>
        /// <param name="Day">Day</param>
        /// <param name="Hour">Hour</param>
        /// <param name="Minute">Minute</param>
        /// <param name="Second">Second</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SetTimeDate(Boolean Use4DigitYearCode, Int16 Year, Int16 Month, Int16 Day, Int16 Hour, Int16 Minute, Int16 Second)
        {
            ProtocolPTU.SetTimeDateReq request =
                new ProtocolPTU.SetTimeDateReq(Use4DigitYearCode, (Byte)Hour, (Byte)Minute, (Byte)Second,
                                                                    (UInt16)Year, (Byte)Month, (Byte)Day);
            
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);
            
            return commError;
        }

        /// <summary>
        /// Updates a single watch element with a new dictionary index.
        /// </summary>
        /// <param name="ElementIndex">the array index in the watch element</param>
        /// <param name="DictionaryIndex">the data dictionary index of the watch element</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SetWatchElement(UInt16 ElementIndex, UInt16 DictionaryIndex)
        {
            ProtocolPTU.SetWatchElementReq request = new ProtocolPTU.SetWatchElementReq(ElementIndex, DictionaryIndex);

            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        /// Map the watch identifiers listed in <paramref name="WatchElements"/> to the watch element array monitored by the embedded target.
        /// </summary>
        /// <param name="WatchElements">TArray containing the watch identifiers that are to be mapped to each element of the watch element array.
        /// </param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SetWatchElements(Int16[] WatchElements)
        {
            ProtocolPTU.SetWatchElementsReq request = new ProtocolPTU.SetWatchElementsReq(WatchElements);

            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        /// Starts the real time clock on the embedded target.
        /// </summary>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError StartClock()
        {
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, ProtocolPTU.PacketType.START_CLOCK);

            return commError;
        }

        /// <summary>
        /// Stops the real time clock on the embedded target
        /// </summary>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError StopClock()
        {
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, ProtocolPTU.PacketType.STOP_CLOCK);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ForceUpdate"></param>
        /// <param name="WatchValues"></param>
        /// <param name="DataType"></param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError UpdateWatchElements(Int16 ForceUpdate, Double[] WatchValues, Int16[] DataType)
        {
            ProtocolPTU.UpdateWatchElementsReq request = new ProtocolPTU.UpdateWatchElementsReq(ForceUpdate);

            CommunicationError commError = m_PtuTargetCommunication.SendDataRequestToEmbedded(m_CommDevice, request, m_RxMessage);

            if (commError == CommunicationError.Success)
            {
                UInt16 numUpdates = BitConverter.ToUInt16(m_RxMessage, 8);
                if (m_CommDevice.IsTargetBigEndian())
                {
                    numUpdates = Utils.ReverseByteOrder(numUpdates);
                }

                for (UInt16 i = 0; i < numUpdates; i++)
                {
                    UInt16 index = BitConverter.ToUInt16(m_RxMessage, ((i * 8) + 10));
                    UInt32 newValue = BitConverter.ToUInt32(m_RxMessage, ((i * 8) + 12));
                    Int16 dataType = BitConverter.ToInt16(m_RxMessage, ((i * 8) + 16));

                    if (m_CommDevice.IsTargetBigEndian())
                    {
                        index = Utils.ReverseByteOrder(index);
                        newValue = Utils.ReverseByteOrder(newValue);
                        dataType = Utils.ReverseByteOrder(dataType);
                    }
                    WatchValues[index] = newValue;
                    DataType[index] = dataType;
                }
            }

            return commError;
        }
        #endregion --- Methods ---
    }
}