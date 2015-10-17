using System;
using System.Text;
using Common.Communication;

namespace VcuComm
{
    public class Comm
    {
        private ICommDevice m_CommDevice;

        Byte[] m_RxMessage = new Byte[1024];


        private Comm () 
        {}

        public Comm(ICommDevice device)
        {
            m_CommDevice = device;
        }

        public CommunicationError GetEmbeddedInformation(ref Protocol.GetEmbeddedInfoRes getEmbInfo)
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.GET_EMBEDDED_INFORMATION, Protocol.ResponseType.DATAREQUEST, m_CommDevice.IsTargetBigEndian());

            m_CommDevice.SendDataToTarget(txMessage);

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);

            // Map rxMessage to GetEmbeddedInfoRes;
            getEmbInfo.SoftwareVersion = Encoding.UTF8.GetString(m_RxMessage, 8, 41).Replace("\0", String.Empty);
            getEmbInfo.CarID = Encoding.UTF8.GetString(m_RxMessage, 49, 11).Replace("\0", String.Empty);
            getEmbInfo.SubSystemName = Encoding.UTF8.GetString(m_RxMessage, 60, 41).Replace("\0", String.Empty);
            getEmbInfo.IdentifierString = Encoding.UTF8.GetString(m_RxMessage, 101, 5).Replace("\0", String.Empty);
            getEmbInfo.ConfigurationMask = BitConverter.ToUInt32(m_RxMessage, 106);

            return CommunicationError.Success;
        }

        public CommunicationError GetChartMode(ref Protocol.GetChartModeRes getChartMode)
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.GET_CHART_MODE, Protocol.ResponseType.DATAREQUEST, false);

            m_CommDevice.SendDataToTarget(txMessage);

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);


            // Map rxMessage to GetEmbeddedInfoRes;
            getChartMode.CurrentChartMode = m_RxMessage[8];

            return CommunicationError.Success;
        }

        public CommunicationError StartClock()
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.START_CLOCK, Protocol.ResponseType.COMMANDREQUEST, false);

            m_CommDevice.SendDataToTarget(txMessage);

            return CommunicationError.Success;
        }

        public CommunicationError StopClock()
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.STOP_CLOCK, Protocol.ResponseType.COMMANDREQUEST, false);

            m_CommDevice.SendDataToTarget(txMessage);

            return CommunicationError.Success;
        }

        public CommunicationError SendVariable(UInt16 DictionaryIndex, UInt16 DataType, Double Data)
        {
            UInt32 data = (UInt32)Data;

            Protocol.SendVariableReq request = new Protocol.SendVariableReq(DictionaryIndex, data);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            m_CommDevice.SendDataToTarget(txMessage);

            return CommunicationError.Success;
        }

        public CommunicationError SetWatchElements(UInt16[] WatchElements)
        {
            Protocol.SetWatchElementsReq request = new Protocol.SetWatchElementsReq(WatchElements);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            m_CommDevice.SendDataToTarget(txMessage);

            return CommunicationError.Success;
        }

        public CommunicationError SetWatchElement(UInt16 ElementIndex, UInt16 DictionaryIndex)
        {
            Protocol.SetWatchElementReq request = new Protocol.SetWatchElementReq(ElementIndex, DictionaryIndex);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            m_CommDevice.SendDataToTarget(txMessage);

            return CommunicationError.Success;
        }

        public CommunicationError UpdateWatchElements(Byte ForceUpdate, Double[] WatchValues, Int16[] DataType)
        {
            Protocol.UpdateWatchElementsReq request = new Protocol.UpdateWatchElementsReq(ForceUpdate);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());
            m_CommDevice.SendDataToTarget(txMessage);

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);

            // TODO place data into arrays

            return CommunicationError.Success;
        }

        public CommunicationError SetCarID(UInt16 NewCarID)
        {
            Protocol.SetCarIDReq request = new Protocol.SetCarIDReq(NewCarID);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            m_CommDevice.SendDataToTarget(txMessage);

            return CommunicationError.Success;
        }

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

        public CommunicationError GetTimeDate(Boolean Use4DigitYearCode, ref UInt16 Year, ref Byte Month, ref Byte Day, ref Byte Hour, ref Byte Minute, ref Byte Second)
        {
            // TODO

            return CommunicationError.Success;

        }

        public CommunicationError SetChartScale(UInt16 DictionaryIndex, Double MaxScale, Double MinScale)
        {
            // TODO

            return CommunicationError.Success;
        }

        public CommunicationError SetChartIndex(UInt16 ChartIndex, UInt16 VariableIndex)
        {
            // TODO

            return CommunicationError.Success;
        }

        public CommunicationError GetChartIndex(UInt16 ChartIndex, ref UInt16 VariableIndex)
        {
            Protocol.GetChartIndexReq request = new Protocol.GetChartIndexReq((Byte)ChartIndex);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());
            m_CommDevice.SendDataToTarget(txMessage);

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);

            VariableIndex = BitConverter.ToUInt16(m_RxMessage, 8);

            // TODO check for endianess
            if (m_CommDevice.IsTargetBigEndian())
            {
                VariableIndex = Utils.ReverseByteOrder(VariableIndex);
            }

            return CommunicationError.Success;
        }

        public CommunicationError SetChartMode(UInt16 TargetChartMode)
        {
            Protocol.SetChartModeReq request = new Protocol.SetChartModeReq((byte)TargetChartMode);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            m_CommDevice.SendDataToTarget(txMessage);

            return CommunicationError.Success;
        }

        public CommunicationError GetChartMode(ref UInt16 CurrentChartMode)
        {
            Protocol.DataPacketProlog request = new Protocol.DataPacketProlog();

            Byte[] txMessage = request.GetByteArray(null, Protocol.PacketType.GET_CHART_MODE, Protocol.ResponseType.DATAREQUEST, m_CommDevice.IsTargetBigEndian());
            m_CommDevice.SendDataToTarget(txMessage);

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);

            CurrentChartMode = BitConverter.ToUInt16(m_RxMessage, 8);

            // check for endianess
            if (m_CommDevice.IsTargetBigEndian())
            {
                CurrentChartMode = Utils.ReverseByteOrder(CurrentChartMode);
            }

            return CommunicationError.Success;
        }

    }
}