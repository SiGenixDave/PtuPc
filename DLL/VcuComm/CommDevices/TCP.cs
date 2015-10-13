using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace VcuComm
{
    public class TCP : ICommDevice
    {
        private readonly UInt16 PTU_SERVER_SOCKET = 5001;
        private IPHostEntry m_Host;
        private TcpClient m_Client;
        private byte m_TargetStartOfMessage;

        public Int16 Open(String commaDelimitedOptions)
        {
            String url = commaDelimitedOptions;

            // Any PTU instance can only support 1 TCP client
            if (m_Client != null)
            {
                return -1;
            }

            m_Client = new TcpClient();

            m_Host = Dns.GetHostEntry(url);

            if (m_Host.AddressList.Length == 0)
            {
                return -1;
            }

            IPAddress ipv4Addr = null;
            foreach (IPAddress addr in m_Host.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    Debug.WriteLine("IPv4 Address: {0}", addr);
                    ipv4Addr = addr;
                    break;
                }
            }

            // Can't resolve URL into IP Address
            if (ipv4Addr == null)
            {
                return -3;
            }

            try
            {
                m_Client.Connect(ipv4Addr, PTU_SERVER_SOCKET);
            }
            catch (SocketException e)
            {
                Debug.Print(e.Message);
                return -2;
            }
            return 0;
        }

        public Int16 SendStartOfMessage()
        {
            byte[] startOfMessage = { Protocol.THE_SOM };

            m_Client.Client.Send(startOfMessage);

            return 0;
        }

        public Int16 ReceiveStartOfMessage()
        {
            byte[] startOfMessage = new byte[1];

            m_Client.Client.Receive(startOfMessage);

            m_TargetStartOfMessage = startOfMessage[0];

            return -1;
        }

        public Int16 SendDataToTarget(Byte[] txMessage)
        {
            SendStartOfMessage();
            ReceiveStartOfMessage();

            m_Client.Client.Send(txMessage);

            return 0;
        }

        public Int16 ReceiveTargetDataPacket(out Byte[] rxMessage)
        {
            rxMessage = new Byte[4096];
            ReceiveStartOfMessage();
            m_Client.Client.Receive(rxMessage);

            return 0;
        }

        public Int16 ReceiveTargetAcknowledge()
        {
            throw new NotImplementedException();
        }

        public Int16 Close(String commaDelimitedOptions)
        {
            m_Client.Client.Shutdown(SocketShutdown.Send);
            m_Client.Close();

            return 0;
        }

        private Boolean TargetBigEndian()
        {
            // TODO determine if target big endian by examining returned SOM
            if (m_TargetStartOfMessage == Protocol.TARGET_BIG_ENDIAN_SOM)
            {
                return true;
            }
            return false;
        }
    }
}