using System;
using System.Text;
using VcuComm;

namespace Common.Communication
{
    /// <summary>
    ///
    /// </summary>
    public class CommGen
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
        ///
        /// </summary>
        private VcuCommunication m_VcuCommunication;

        #endregion --- Member Variables ---

        #region --- Constructors ---

        /// <summary>
        /// Constructor that initializes a new instance of the CommGen class
        /// </summary>
        /// <param name="device">The communication vehicle used to access the PTU target (VCU)</param>
        public CommGen(ICommDevice device)
        {
            m_CommDevice = device;
            m_VcuCommunication = new VcuCommunication();
        }

        /// <summary>
        /// Private 0 argument constructor that forces the instantiation of this class
        /// to use the constructor below.
        /// </summary>
        private CommGen()
        {
            // Intentionally empty
        }
        #endregion --- Constructors ---

        #region --- Methods ---

        /// <summary>
        ///
        /// </summary>
        /// <param name="ChartIndex"></param>
        /// <param name="VariableIndex"></param>
        /// <returns></returns>
        public CommunicationError GetChartIndex(Int16 ChartIndex, ref Int16 VariableIndex)
        {
            ProtocolPTU.GetChartIndexReq request = new ProtocolPTU.GetChartIndexReq((Byte)ChartIndex);

            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, request, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            VariableIndex = BitConverter.ToInt16(m_RxMessage, 8);

            // check for endianess
            if (m_CommDevice.IsTargetBigEndian())
            {
                VariableIndex = Utils.ReverseByteOrder(VariableIndex);
            }

            return CommunicationError.Success;
        }

 

        /// <summary>
        ///
        /// </summary>
        /// <param name="CurrentChartMode"></param>
        /// <returns></returns>
        public CommunicationError GetChartMode(ref Int16 CurrentChartMode)
        {
            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_CHART_MODE, m_RxMessage);

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
        /// <returns></returns>
        public CommunicationError GetEmbeddedInformation(ref ProtocolPTU.GetEmbeddedInfoRes getEmbInfo)
        {
            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_EMBEDDED_INFORMATION, m_RxMessage);

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
        ///
        /// </summary>
        /// <param name="Use4DigitYearCode"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Day"></param>
        /// <param name="Hour"></param>
        /// <param name="Minute"></param>
        /// <param name="Second"></param>
        /// <returns></returns>
        public CommunicationError GetTimeDate(Boolean Use4DigitYearCode, ref Int16 Year, ref Byte Month, ref Byte Day, ref Byte Hour, ref Byte Minute, ref Byte Second)
        {
            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_TIME_DATE, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            Hour = m_RxMessage[8];
            Minute = m_RxMessage[9];
            Second = m_RxMessage[10];
            Year = m_RxMessage[11];
            Month = m_RxMessage[12];
            Day = m_RxMessage[13];

            // check for endianess
            if (m_CommDevice.IsTargetBigEndian())
            {
                //TODO
            }

            return CommunicationError.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DictionaryIndex"></param>
        /// <param name="DataType"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public CommunicationError SendVariable(Int16 DictionaryIndex, Int16 DataType, Double Data)
        {
            UInt32 data = (UInt32)Data;

            ProtocolPTU.SendVariableReq request = new ProtocolPTU.SendVariableReq(DictionaryIndex, data);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="NewCarID"></param>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="ChartIndex"></param>
        /// <param name="VariableIndex"></param>
        /// <returns></returns>
        public CommunicationError SetChartIndex(Int16 ChartIndex, Int16 VariableIndex)
        {
            ProtocolPTU.SetChartIndexReq request = new ProtocolPTU.SetChartIndexReq(ChartIndex, VariableIndex);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="TargetChartMode"></param>
        /// <returns></returns>
        public CommunicationError SetChartMode(Int16 TargetChartMode)
        {
            ProtocolPTU.SetChartModeReq request = new ProtocolPTU.SetChartModeReq((byte)TargetChartMode);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DictionaryIndex"></param>
        /// <param name="MaxScale"></param>
        /// <param name="MinScale"></param>
        /// <returns></returns>
        public CommunicationError SetChartScale(Int16 DictionaryIndex, Double MaxScale, Double MinScale)
        {
            Int32 maxScale = (Int32)MaxScale;
            Int32 minScale = (Int32)MinScale;

            ProtocolPTU.SetChartScaleReq request =
                new ProtocolPTU.SetChartScaleReq(DictionaryIndex, maxScale, minScale);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Use4DigitYearCode"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Day"></param>
        /// <param name="Hour"></param>
        /// <param name="Minute"></param>
        /// <param name="Second"></param>
        /// <returns></returns>
        public CommunicationError SetTimeDate(Boolean Use4DigitYearCode, Int16 Year, Int16 Month, Int16 Day, Int16 Hour, Int16 Minute, Int16 Second)
        {
            // TODO
            // ProtocolPTU.SetTimeDateReq();
            if (Use4DigitYearCode == true)
            {
            }
            else
            {
            }

            return CommunicationError.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ElementIndex"></param>
        /// <param name="DictionaryIndex"></param>
        /// <returns></returns>
        public CommunicationError SetWatchElement(UInt16 ElementIndex, UInt16 DictionaryIndex)
        {
            ProtocolPTU.SetWatchElementReq request = new ProtocolPTU.SetWatchElementReq(ElementIndex, DictionaryIndex);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="WatchElements"></param>
        /// <returns></returns>
        public CommunicationError SetWatchElements(Int16[] WatchElements)
        {
            ProtocolPTU.SetWatchElementsReq request = new ProtocolPTU.SetWatchElementsReq(WatchElements);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public CommunicationError StartClock()
        {
            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, ProtocolPTU.PacketType.START_CLOCK);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public CommunicationError StopClock()
        {
            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, ProtocolPTU.PacketType.STOP_CLOCK);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ForceUpdate"></param>
        /// <param name="WatchValues"></param>
        /// <param name="DataType"></param>
        /// <returns></returns>
        public CommunicationError UpdateWatchElements(Int16 ForceUpdate, Double[] WatchValues, Int16[] DataType)
        {
            ProtocolPTU.UpdateWatchElementsReq request = new ProtocolPTU.UpdateWatchElementsReq(ForceUpdate);

            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, request, m_RxMessage);

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