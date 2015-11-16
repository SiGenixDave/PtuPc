using System;
using System.Text;
using Common.Communication;

namespace VcuComm
{
    /// <summary>
    /// 
    /// </summary>
    public class Comm
    {
        #region --- Member Variables ---
        
        /// <summary>
        /// 
        /// </summary>
        private ICommDevice m_CommDevice;

        /// <summary>
        /// 
        /// </summary>
        private Byte[] m_RxMessage = new Byte[1024];
        
        #endregion --- Member Variables ---

        #region --- Constructors ---

        /// <summary>
        /// Private 0 argument constructor that forces the instantiation of this class
        /// to use the constructor below
        /// </summary>
        private Comm () 
        {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        public Comm(ICommDevice device)
        {
            m_CommDevice = device;
        }
        #endregion --- Constructors ---

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getEmbInfo"></param>
        /// <returns></returns>
        public CommunicationError GetEmbeddedInformation(ref Protocol.GetEmbeddedInfoRes getEmbInfo)
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.GET_EMBEDDED_INFORMATION, Protocol.ResponseType.DATAREQUEST, m_CommDevice.IsTargetBigEndian());

            CommunicationError commError = SendDataRequestToEmbedded(txMessage);

            if (commError == CommunicationError.Success)
            {
                // Map rxMessage to GetEmbeddedInfoRes;
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
        /// <param name="getChartMode"></param>
        /// <returns></returns>
        public CommunicationError GetChartMode(ref Protocol.GetChartModeRes getChartMode)
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.GET_CHART_MODE, Protocol.ResponseType.DATAREQUEST, false);

            CommunicationError commError = SendDataRequestToEmbedded(txMessage);

            if (commError == CommunicationError.Success)
            {
                getChartMode.CurrentChartMode = m_RxMessage[8];
            }

            return commError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CommunicationError StartClock()
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.START_CLOCK, Protocol.ResponseType.COMMANDREQUEST, false);

            CommunicationError commError = SendCommandToEmbedded(txMessage);

            return commError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CommunicationError StopClock()
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.STOP_CLOCK, Protocol.ResponseType.COMMANDREQUEST, false);

            CommunicationError commError = SendCommandToEmbedded(txMessage);

            return commError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DictionaryIndex"></param>
        /// <param name="DataType"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public CommunicationError SendVariable(UInt16 DictionaryIndex, UInt16 DataType, Double Data)
        {
            UInt32 data = (UInt32)Data;

            Protocol.SendVariableReq request = new Protocol.SendVariableReq(DictionaryIndex, data);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            CommunicationError commError = SendCommandToEmbedded(txMessage);

            return commError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WatchElements"></param>
        /// <returns></returns>
        public CommunicationError SetWatchElements(UInt16[] WatchElements)
        {
            Protocol.SetWatchElementsReq request = new Protocol.SetWatchElementsReq(WatchElements);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            CommunicationError commError = SendCommandToEmbedded(txMessage);

            return commError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ElementIndex"></param>
        /// <param name="DictionaryIndex"></param>
        /// <returns></returns>
        public CommunicationError SetWatchElement(UInt16 ElementIndex, UInt16 DictionaryIndex)
        {
            Protocol.SetWatchElementReq request = new Protocol.SetWatchElementReq(ElementIndex, DictionaryIndex);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            CommunicationError commError = SendCommandToEmbedded(txMessage);

            return commError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ForceUpdate"></param>
        /// <param name="WatchValues"></param>
        /// <param name="DataType"></param>
        /// <returns></returns>
        public CommunicationError UpdateWatchElements(Byte ForceUpdate, Double[] WatchValues, Int16[] DataType)
        {
            Protocol.UpdateWatchElementsReq request = new Protocol.UpdateWatchElementsReq(ForceUpdate);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            CommunicationError commError = SendDataRequestToEmbedded(txMessage);

            if (commError == CommunicationError.Success)
            {
                UInt16 numUpdates = BitConverter.ToUInt16(m_RxMessage, 8);
                if (m_CommDevice.IsTargetBigEndian())
                {
                    numUpdates = Utils.ReverseByteOrder(numUpdates);
                }

                for (UInt16 i = 0; i < numUpdates; i++)
                {
                    UInt16 index = BitConverter.ToUInt16(m_RxMessage, ((i * 8) + 9)) ;
                    UInt32 newValue = BitConverter.ToUInt32(m_RxMessage, ((i * 8) + 11));
                    UInt16 dataType = BitConverter.ToUInt16(m_RxMessage, ((i * 8) + 15));
                    
                    if (m_CommDevice.IsTargetBigEndian())
                    {
                        index = Utils.ReverseByteOrder(index);
                        newValue = Utils.ReverseByteOrder(newValue);
                        dataType = Utils.ReverseByteOrder(dataType);
                    }
                    // TODO may have to adjust value based on data type
                    WatchValues[index] = newValue;

                }

            }

            return commError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewCarID"></param>
        /// <returns></returns>
        public CommunicationError SetCarID(UInt16 NewCarID)
        {
            Protocol.SetCarIDReq request = new Protocol.SetCarIDReq(NewCarID);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            CommunicationError commError = SendCommandToEmbedded(txMessage);

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
        public CommunicationError SetTimeDate(Boolean Use4DigitYearCode, UInt16 Year, Byte Month, Byte Day, Byte Hour, Byte Minute, Byte Second)
        {
            // TODO
            // Protocol.SetTimeDateReq();
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
        /// <param name="Use4DigitYearCode"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Day"></param>
        /// <param name="Hour"></param>
        /// <param name="Minute"></param>
        /// <param name="Second"></param>
        /// <returns></returns>
        public CommunicationError GetTimeDate(Boolean Use4DigitYearCode, ref UInt16 Year, ref Byte Month, ref Byte Day, ref Byte Hour, ref Byte Minute, ref Byte Second)
        {
            // TODO

            return CommunicationError.Success;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DictionaryIndex"></param>
        /// <param name="MaxScale"></param>
        /// <param name="MinScale"></param>
        /// <returns></returns>
        public CommunicationError SetChartScale(UInt16 DictionaryIndex, Double MaxScale, Double MinScale)
        {
            // TODO

            return CommunicationError.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ChartIndex"></param>
        /// <param name="VariableIndex"></param>
        /// <returns></returns>
        public CommunicationError SetChartIndex(UInt16 ChartIndex, UInt16 VariableIndex)
        {
            // TODO

            return CommunicationError.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ChartIndex"></param>
        /// <param name="VariableIndex"></param>
        /// <returns></returns>
        public CommunicationError GetChartIndex(UInt16 ChartIndex, ref UInt16 VariableIndex)
        {
            Protocol.GetChartIndexReq request = new Protocol.GetChartIndexReq((Byte)ChartIndex);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            Int32 errorCode = m_CommDevice.SendMessageToTarget(txMessage);

            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);

            VariableIndex = BitConverter.ToUInt16(m_RxMessage, 8);

            // TODO check for endianess
            if (m_CommDevice.IsTargetBigEndian())
            {
                VariableIndex = Utils.ReverseByteOrder(VariableIndex);
            }

            return CommunicationError.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TargetChartMode"></param>
        /// <returns></returns>
        public CommunicationError SetChartMode(UInt16 TargetChartMode)
        {
            Protocol.SetChartModeReq request = new Protocol.SetChartModeReq((byte)TargetChartMode);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            CommunicationError commError = SendCommandToEmbedded(txMessage);

            return commError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrentChartMode"></param>
        /// <returns></returns>
        public CommunicationError GetChartMode(ref UInt16 CurrentChartMode)
        {
            Protocol.DataPacketProlog request = new Protocol.DataPacketProlog();

            Byte[] txMessage = request.GetByteArray(null, Protocol.PacketType.GET_CHART_MODE, Protocol.ResponseType.DATAREQUEST, m_CommDevice.IsTargetBigEndian());

            Int32 errorCode = m_CommDevice.SendMessageToTarget(txMessage);

            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);

            CurrentChartMode = BitConverter.ToUInt16(m_RxMessage, 8);

            // check for endianess
            if (m_CommDevice.IsTargetBigEndian())
            {
                CurrentChartMode = Utils.ReverseByteOrder(CurrentChartMode);
            }

            return CommunicationError.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txMessage"></param>
        /// <returns></returns>
        private CommunicationError SendDataRequestToEmbedded(byte []txMessage)
        {
            Int32 errorCode = m_CommDevice.SendMessageToTarget(txMessage);

            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            errorCode = m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txMessage"></param>
        /// <returns></returns>
        private CommunicationError SendCommandToEmbedded(byte[] txMessage)
        {
            Int32 errorCode = m_CommDevice.SendMessageToTarget(txMessage);

            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            errorCode = m_CommDevice.ReceiveTargetAcknowledge();
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;
        }

    }
}