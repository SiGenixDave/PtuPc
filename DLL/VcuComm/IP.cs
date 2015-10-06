using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace VcuComm
{

    public class IP
    {
        private readonly UInt16 PTU_SERVER_SOCKET = 5001;
        private IPHostEntry m_Host;
        private TcpClient m_Client;
        private byte m_TargetStartOfMessage;


        public void ReceiveTargetAcknowledge()
        {
        }

        public void ReceiveTargetDataPacket()
        {
        }


        public Int16 InitializeSockets(String url)
        {
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

        // original function signature public Int16 SendDataPacket(Header_t DataPacket)
        public Byte[] SendData(Byte []message)
        {
            Byte []receiveMsg = new Byte[4096];

            SendStartOfMessage();
            ReceiveStartOfMessage();

            m_Client.Client.Send(message);
            ReceiveStartOfMessage(); 
            m_Client.Client.Receive(receiveMsg);

            return receiveMsg;
        }

        public Int16 SendStartOfMessage()
        {
            byte[] startOfMessage = { Protocol.THE_SOM };

            m_Client.Client.Send(startOfMessage);

            return -1;
        }

        public Int16 ReceiveStartOfMessage()
        {
            byte[] startOfMessage = new byte[1];

            m_Client.Client.Receive(startOfMessage);

            m_TargetStartOfMessage = startOfMessage[0];

            return -1;
        }

        public void TerminateSocket()
        {
            m_Client.Client.Shutdown(SocketShutdown.Send);
            m_Client.Close();
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