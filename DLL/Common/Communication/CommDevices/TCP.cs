using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Common.Communication.CommDevices
{
    /// <summary>
    /// Class that implements the TCP client communication with the embedded TCP PTU
    /// </summary>
    public class TCP : ICommDevice
    {
        #region --- Enumerations ---

        #endregion --- Enumerations ---

        #region --- Constants ---

        /// <summary>
        /// This is the fixed server port number for any embedded TCP PTU.
        /// </summary>
        private readonly UInt16 PTU_SERVER_SOCKET = 5001;

        #endregion --- Constants ---

        #region --- Member Variables ---

        /// <summary>
        /// Becomes true if the client connects to the PTU target. The client connection is done
        /// via a non-blocking (asynchronous) call in order to prevent the PTU from locking up
        /// because the connection can't be established.
        /// </summary>
        private Boolean m_Connected;

        /// <summary>
        /// TCP client object that is created and used to send requests to the embedded TCP PTU
        /// server as well as read and process responses from the server.
        /// </summary>
        private TcpClient m_Client = null;

        /// <summary>
        /// Stores the most recent server start of message response. In theory, once updated by the
        /// embedded PTU, this value should never change because it is not possible to establish
        /// a TCP connection with another embedded PTU.
        /// </summary>
        private byte m_TargetStartOfMessage;

        /// <summary>
        /// Stores the most recent TCP error. Cleared whenever a calling function reads the state.
        /// </summary>
        private Common.Communication.ProtocolClass.Protocol.Errors m_TCPError = 
                                    Common.Communication.ProtocolClass.Protocol.Errors.None;

        /// <summary>
        /// Property for m_TCPError
        /// </summary>
        public Common.Communication.ProtocolClass.Protocol.Errors Error
        {
            get
            {
                Common.Communication.ProtocolClass.Protocol.Errors tcpErrCopy = m_TCPError;
                // Reset the error after the error code is read; if it isn't read
                // than the most recent error will be saved until another error occurs
                m_TCPError = Common.Communication.ProtocolClass.Protocol.Errors.None;
                return tcpErrCopy;
            }
        }

        /// <summary>
        /// Maintains the exception message thrown when a serial port error occurs. Allows the calling function
        /// to ascertain more detail about the error.
        /// </summary>
        private String m_ExceptionMessage = "No Exceptions Raised";

        /// <summary>
        /// Property for m_ExceptionMessage
        /// </summary>
        public String ExceptionMessage
        {
            get
            {
                String exceptionMsgCopy = m_ExceptionMessage;
                // Reset the error after the error message is read; if it isn't read
                // than the most recent error will be saved until another error occurs
                m_ExceptionMessage = "No Exceptions Raised";
                return exceptionMsgCopy;
            }
        }

        #endregion --- Member Variables ---

        #region --- Methods ---

        /// <summary>
        /// This function attempts to open a new connection with an embedded PTU.
        /// </summary>
        /// <param name="commaDelimitedOptions">valid URL or valid IP address</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int32 Open(String commaDelimitedOptions)
        {
            String url;
            IPHostEntry ipHost;

            // There are no other options supported except passing the URL (could be IP address or
            // IPTCOM address)
            url = commaDelimitedOptions;

            // Any PTU object can only support 1 TCP client; ensures Open() is called only once per created object
            if (m_Client != null)
            {
                m_TCPError = Protocol.Errors.ClientPreviouslyCreated;
                return -1;
            }

            // Create the TCPClient object
            m_Client = new TcpClient();

            // Attempt to resolve the URL to an IP address
            try
            {
                ipHost = Dns.GetHostEntry(url);
            }
            catch (Exception e)
            {
                m_TCPError = Protocol.Errors.InvalidURL;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            // Verify at least one IP address is resolved
            if (ipHost.AddressList.Length == 0)
            {
                m_TCPError = Protocol.Errors.InvalidURL;
                return -1;
            }

            // Scan through all of the resolved IP addresses and try connecting to the first
            // IP v4 address in the list; ignore the rest
            IPAddress ipv4Addr = null;
            foreach (IPAddress addr in ipHost.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    Debug.WriteLine("IPv4 Address: {0}", addr);
                    ipv4Addr = addr;
                    break;
                }
            }

            // Can't resolve URL into IPv4 Address
            if (ipv4Addr == null)
            {
                m_TCPError = Protocol.Errors.UnresolvableURL;
                return -1;
            }

            // Try to establish a connection with the embedded PTU. This establishes the 3 way handshake with the
            // target
            try
            {
                // This is an asynchronous (non--blocking) TCP connection attempt. If a successful connection
                // occurs, the ConnectCallback() method will be invoked and m_Connected will be made true
                m_Connected = false;
                m_Client.BeginConnect(ipv4Addr, PTU_SERVER_SOCKET, new AsyncCallback(ConnectCallback), m_Client);
            }
            catch (SocketException e)
            {
                m_TCPError = Protocol.Errors.ConnectionError;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            try
            {
                // Sleep for a while so that ample time for a connection can be made
                Thread.Sleep(1000);
                // m_Connected becomes true (asynchronously) if a valid connection was made
                if (m_Connected == false)
                {
                    m_TCPError = Protocol.Errors.ConnectionError;
                    return -1;
                }
            }
            catch (Exception e)
            {
                m_TCPError = Protocol.Errors.ConnectionError;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            // Connection to PTU server was successful
            return 0;
        }

        /// <summary>
        /// Closes the TCP connection gracefully by issuing a shutdown which effectively disables sends
        /// and receives on the socket and then closes the socket (issues a [FIN,ACK]).
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int32 Close()
        {
            try
            {
                m_Client.Client.Shutdown(SocketShutdown.Send);
                m_Client.Close();
            }
            catch (Exception e)
            {
                m_TCPError = Protocol.Errors.Close;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            return 0;
        }



        /// <summary>
        /// Send a message to the target. The SOM is sent and then waits for an echo from the target.
        /// The message is then sent and an echo that is identical to the message sent is verified.
        /// </summary>
        /// <param name="txMessage">the message to be sent to the target</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int32 SendMessageToTarget(Byte[] txMessage)
        {
            Int32 errorCode;
            errorCode = SendStartOfMessage();

            if (errorCode < 0)
            {
                return errorCode;
            }

            errorCode = ReceiveStartOfMessage();
            if (errorCode < 0)
            {
                return errorCode;
            }

            errorCode = TransmitMessage(txMessage);

            return errorCode;
        }

        /// <summary>
        /// Receives a message from the target. It is assumed that the target sends a message with the first 2 bytes
        /// indicating the size of the message.
        /// </summary>
        /// <param name="rxMessage">array where the received message will be stored</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int32 ReceiveTargetDataPacket(Byte[] rxMessage)
        {
            // Verify the target responds with a SOM first
            Int32 errorCode = ReceiveStartOfMessage();
            if (errorCode < 0)
            {
                return errorCode;
            }

            Int32 bytesRead = 0;
            Int32 totalBytesRead = 0;
            // When the first 2 bytes are read, the received message size will be calculated
            UInt16 messageSize = UInt16.MaxValue;
            while (totalBytesRead != messageSize)
            {
                // read the TCP receive buffer
                bytesRead = ReceiveMessage(rxMessage, totalBytesRead);
                if (bytesRead < 0)
                {
                    return bytesRead;
                }

                // adjust the index into the receive buffer in case the entire message wasn't received
                totalBytesRead += bytesRead;

                if ((totalBytesRead >= 2) && (messageSize == UInt16.MaxValue))
                {
                    // 1st 2 bytes of the message is the message length
                    messageSize = BitConverter.ToUInt16(rxMessage, 0);
                    if (IsTargetBigEndian())
                    {
                        messageSize = Utils.ReverseByteOrder(messageSize);
                    }
                }
                // Verify the expected receive message length isn't too long
                if (totalBytesRead > messageSize)
                {
                    // too many bytes read
                    m_TCPError = Protocol.Errors.ExcessiveBytesReceived;
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// The target is responsible for reporting whether it is a big or little endian machine. The start of
        /// message received from the target indicates the machine type of target
        /// </summary>
        /// <remarks>It is imperative that the calling function perform all error checking prior to invoking this
        /// method. That includes verification that the transmitted SOM was echoed before making assumptions that there
        /// is an embedded PTU connected.
        /// </remarks>
        /// <returns>true if target is Big Endian; false otherwise</returns>
        public Boolean IsTargetBigEndian()
        {
            if (m_TargetStartOfMessage == Protocol.TARGET_BIG_ENDIAN_SOM)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads the data from the TCP port and verifies the target acknowledged the message. Target acknowledges
        /// the message sent from the application when no data is sent back from the target (i.e. a command was sent)
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int32 ReceiveTargetAcknowledge()
        {
            // Allocate memory for the acknowledge
            byte[] rxMessage = new Byte[1];

            Int32 bytesRead = 0;
            while (bytesRead == 0)
            {
                // Read the serial port
                bytesRead = ReceiveMessage(rxMessage, 0);
                if (bytesRead < 0)
                {
                    return bytesRead;
                }

                if (bytesRead == 1)
                {
                    // Verify ACK received
                    if (rxMessage[0] != Protocol.PTU_ACK)
                    {
                        m_TCPError = Protocol.Errors.AckNotReceieved;
                        return -1;
                    }
                }
                else if (bytesRead > 1)
                {
                    // too many bytes read
                    m_TCPError = Protocol.Errors.ExcessiveBytesReceived;
                    return -1;
                }
            }

            return 0;
        }


        /// <summary>
        /// Sends the Start Of Message (SOM) to the embedded PTU.
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int32 SendStartOfMessage()
        {
            byte[] startOfMessage = { Protocol.THE_SOM };

            Int32 errorCode = TransmitMessage(startOfMessage);

            return errorCode;
        }

        /// <summary>
        /// Receives the Start Of Message (SOM) from the embedded the embedded PTU
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int32 ReceiveStartOfMessage()
        {
            // Allocate memory for the received byte
            byte[] startOfMessage = new byte[1];

            Int32 bytesRead = 0;
            while (bytesRead == 0)
            {
                bytesRead = ReceiveMessage(startOfMessage, 0);

                // Verify a valid call was made to ReceiveMessage()
                if (bytesRead < 0)
                {
                    return bytesRead;
                }
                // Verify only 1 byte was read; if so save the byte
                if (bytesRead == 1)
                {
                    // Verify a valid SOM
                    if ((startOfMessage[0] != Protocol.THE_SOM) && (startOfMessage[0] != Protocol.TARGET_BIG_ENDIAN_SOM))
                    {
                        m_TargetStartOfMessage = 0;
                        m_TCPError = Protocol.Errors.InvalidSOM;
                        return -1;
                    }
                }
                else if (bytesRead > 1)
                {
                    // too many bytes read
                    m_TCPError = Protocol.Errors.ExcessiveBytesReceived;
                    return -1;
                }
            }

            return 0;
        }


        /// <summary>
        /// This method is invoked after a successful TCP connection (3 way handshake) with the embedded PTU
        /// is established. It sets a flag to inform the connection was established.
        /// </summary>
        /// <param name="result">delegate parameter required; UNUSED</param>
        private void ConnectCallback(IAsyncResult result)
        {
            m_Connected = true;
        }

        /// <summary>
        /// Sends a message to the embedded TCP server target from the TCP client
        /// </summary>
        /// <param name="txMessage">the byte array to be sent to the embedded PTU</param>
        /// <returns>less than 0 if any failure occurs; number of bytes sent if successful</returns>
        private Int32 TransmitMessage(Byte[] txMessage)
        {
            Int32 bytesSent = 0;

            // Send the entire message to the serial port
            try
            {
                bytesSent = m_Client.Client.Send(txMessage);
            }
            catch (Exception e)
            {
                m_TCPError = Protocol.Errors.TransmitMessage;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return bytesSent;
        }

        /// <summary>
        /// This method is responsible for reading all available chars that are in the serial port
        /// sent by the embedded PTU. All characters are copied to the <paramref name="rxMessage"/>
        /// starting at the <paramref name="bufferOffset"/>. This feature allows multiple calls to
        /// this method until the entire message is received.
        /// </summary>
        /// <param name="rxMessage">buffer where the received message is stored</param>
        /// <param name="bufferOffset">offset into the receive buffer</param>
        /// <returns>less than 0 if any failure occurs; number of bytes read if successful</returns>
        private Int32 ReceiveMessage(Byte[] rxMessage, Int32 bufferOffset)
        {
            Int32 bytesRead = 0;

            try
            {
                bytesRead = m_Client.Client.Receive(rxMessage, bufferOffset, rxMessage.Length - bufferOffset, SocketFlags.None);
            }
            catch (Exception e)
            {
                m_TCPError = Protocol.Errors.ReceiveMessage;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return bytesRead;
        }

        #endregion --- Methods ---
    }
}