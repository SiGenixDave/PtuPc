using System;
using System.Text;
using Common.Communication;

namespace VcuComm
{
    public class Comm
    {
        private IP m_TCPComm;

        public Comm(IP client)
        {
            m_TCPComm = client;
        }

        public CommunicationError GetEmbeddedInformation(ref Protocol.GetEmbeddedInfoRes getEmbInfo)
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txData = dpp.GetByteArray(null, Protocol.PacketType.GET_EMBEDDED_INFORMATION, Protocol.ResponseType.DATAREQUEST, false);

            Byte[] rxData = m_TCPComm.SendData(txData);

            // Map rxData to GetEmbeddedInfoRes;
            getEmbInfo.SoftwareVersion = Encoding.UTF8.GetString(rxData, 8, 41).Replace("\0", String.Empty);
            getEmbInfo.CarID = Encoding.UTF8.GetString(rxData, 49, 11).Replace("\0", String.Empty);
            getEmbInfo.SubSystemName = Encoding.UTF8.GetString(rxData, 60, 41).Replace("\0", String.Empty);
            getEmbInfo.IdentifierString = Encoding.UTF8.GetString(rxData, 101, 5).Replace("\0", String.Empty);
            getEmbInfo.ConfigurationMask = BitConverter.ToUInt32(rxData, 106);

            return CommunicationError.Success;
        }

        public Protocol.GetChartModeRes GetChartMode()
        {
            Protocol.GetChartModeRes getChartMode;

            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txData = dpp.GetByteArray(null, Protocol.PacketType.GET_CHART_MODE, Protocol.ResponseType.DATAREQUEST, false);

            Byte[] rxData = m_TCPComm.SendData(txData);

            // Map rxData to GetEmbeddedInfoRes;
            getChartMode.CurrentChartMode = rxData[8];

            return getChartMode;
        }

        public CommunicationError StartClock()
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txData = dpp.GetByteArray(null, Protocol.PacketType.START_CLOCK, Protocol.ResponseType.COMMANDREQUEST, false);

            m_TCPComm.SendData(txData);

            return CommunicationError.Success;
        }

        public CommunicationError StopClock()
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txData = dpp.GetByteArray(null, Protocol.PacketType.STOP_CLOCK, Protocol.ResponseType.COMMANDREQUEST, false);

            m_TCPComm.SendData(txData);

            return CommunicationError.Success;
        }

        public CommunicationError SendVariable(UInt16 DictionaryIndex, UInt16 DataType, Double Data)
        {
            UInt32 data = (UInt32)Data;

            Protocol.SendVariableReq request = new Protocol.SendVariableReq(DictionaryIndex, data);

            Byte[] txData = request.GetByteArray(false);

            m_TCPComm.SendData(txData);

            return CommunicationError.Success;
        }

        public CommunicationError SetWatchElements(UInt16[] WatchElements)
        {
            Protocol.SetWatchElementsReq request = new Protocol.SetWatchElementsReq(WatchElements);

            Byte[] txData = request.GetByteArray(false);

            m_TCPComm.SendData(txData);

            return CommunicationError.Success;
        }

        public CommunicationError SetWatchElement(UInt16 ElementIndex, UInt16 DictionaryIndex)
        {
            Protocol.SetWatchElementReq request = new Protocol.SetWatchElementReq(ElementIndex, DictionaryIndex);

            Byte[] txData = request.GetByteArray(false);

            m_TCPComm.SendData(txData);

            return CommunicationError.Success;
        }

        public CommunicationError UpdateWatchElements(Byte ForceUpdate, Double[] WatchValues, Int16[] DataType)
        {
            Protocol.UpdateWatchElementsReq request = new Protocol.UpdateWatchElementsReq(ForceUpdate);

            Byte[] txData = request.GetByteArray(false);

            Byte[] rxData = m_TCPComm.SendData(txData);

            // TODO place data into arrays

            return CommunicationError.Success;
        }

        public CommunicationError SetCarID(UInt16 NewCarID)
        {
            Protocol.SetCarIDReq request = new Protocol.SetCarIDReq(NewCarID);

            Byte[] txData = request.GetByteArray(false);

            m_TCPComm.SendData(txData);

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

            Byte[] txData = request.GetByteArray(false);

            Byte[] rxData = m_TCPComm.SendData(txData);

            // TODO place response into VariableIndex

            return CommunicationError.Success;
        }

        public CommunicationError SetChartMode(UInt16 TargetChartMode)
        {
            // TODO

            return CommunicationError.Success;
        }

        public CommunicationError GetChartMode(ref UInt16 CurrentChartMode)
        {
            Protocol.DataPacketProlog request = new Protocol.DataPacketProlog();

            Byte[] txData = request.GetByteArray(null, Protocol.PacketType.GET_CHART_MODE, Protocol.ResponseType.DATAREQUEST, false);

            Byte[] rxData = m_TCPComm.SendData(txData);

            // TODO place response in CurrentChartMode

            return CommunicationError.Success;
        }

    }
}