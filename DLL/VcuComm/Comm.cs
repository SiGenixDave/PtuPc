using System;
using System.Net.Sockets;
using System.Text;

namespace VcuComm
{
    public class Comm
    {
        private IP m_TCPComm;

        public Comm(IP client)
        {
            m_TCPComm = client;
        }

        public Protocol.GetEmbeddedInfoRes GetEmbeddedInformation()
        {
            Protocol.GetEmbeddedInfoRes getEmbInfo = new Protocol.GetEmbeddedInfoRes();

            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txData = dpp.GetByteArray(null, Protocol.PacketType.GET_EMBEDDED_INFORMATION, Protocol.ResponseType.DATAREQUEST, false);

            Byte []rxData = m_TCPComm.SendData(txData);

            // Map rxData to GetEmbeddedInfoRes;
            getEmbInfo.SoftwareVersion = Encoding.UTF8.GetString(rxData, 8, 41).Replace("\0", String.Empty);
            getEmbInfo.CarID = Encoding.UTF8.GetString(rxData, 49, 11).Replace("\0", String.Empty);
            getEmbInfo.SubSystemName = Encoding.UTF8.GetString(rxData, 60, 41).Replace("\0", String.Empty);
            getEmbInfo.IdentifierString = Encoding.UTF8.GetString(rxData, 101, 5).Replace("\0", String.Empty);
            getEmbInfo.ConfigurationMask = BitConverter.ToUInt32(rxData, 106);

            return getEmbInfo;
        }
    }
}