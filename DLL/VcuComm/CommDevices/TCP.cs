using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace VcuComm
{
    /// <summary>
    /// Class that implements the TCP client communication with the embedded TCP PTU
    /// </summary>
    public class TCP : ICommDevice
    {
        #region --- Enumerations ---

        /// <summary>
        /// These errors are logged whenever any error is detected when a TCP transaction occurs
        /// </summary>
        public enum TCPErrors
        {
            None,
            ClientPreviouslyCreated,
            InvalidURL,
            UnresolvableURL,
            ConnectionError,
            TransmitMessage,
            ReceiveMessage,
            AckNotReceieved,
            RxTimeout,
            ExcessiveBytesReceived,
            Close,
        }

        #endregion --- Enumerations ---

        #region --- Constants ---

        /// <summary>
        /// This is the fixed server port number for any embedded TCP PTU.
        /// </summary>
        private readonly UInt16 PTU_SERVER_SOCKET = 5001;

        #endregion --- Constants ---

        #region --- Member Variables ---

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
        private TCPErrors m_TCPError = TCPErrors.None;

        /// <summary>
        /// Property for m_TCPError
        /// </summary>
        public TCPErrors TCPError
        {
            get
            {
                TCPErrors tcpErrCopy = m_TCPError;
                // Reset the error after the error code is read; if it isn't read
                // than the most recent error will be saved until another error occurs
                m_TCPError = TCPErrors.None;
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
        public Int16 Open(String commaDelimitedOptions)
        {
            String url;
            IPHostEntry ipHost;

            // There are no other options supported except passing the URL (could be IP address or 
            // IPTCOM address)
            url = commaDelimitedOptions;

            // Any PTU object can only support 1 TCP client; ensures Open() is called only once per created object
            if (m_Client != null)
            {
                m_TCPError = TCPErrors.ClientPreviouslyCreated;
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
                m_TCPError = TCPErrors.InvalidURL;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            // Verify at least one IP address is resolved
            if (ipHost.AddressList.Length == 0)
            {
                m_TCPError = TCPErrors.InvalidURL;
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
                m_TCPError = TCPErrors.UnresolvableURL;
                return -1;
            }

            // Try to establish a connection with the embedded PTU. This establishes th 3 way handshake with the 
            // target
            try
            {
                m_Connected = false;
                m_Client.BeginConnect(ipv4Addr, PTU_SERVER_SOCKET, new AsyncCallback(ConnectCallback), m_Client);
                //m_Client.Connect(ipv4Addr, PTU_SERVER_SOCKET);
            }
            catch (SocketException e)
            {
                m_TCPError = TCPErrors.ConnectionError;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            try
            {
                Thread.Sleep(1000);
                if (m_Connected == false)
                {
                    m_TCPError = TCPErrors.ConnectionError;
                    return -1;
                }
            }
            catch (Exception e)
            {
                m_TCPError = TCPErrors.ConnectionError;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            
            return 0;
        }

        private Boolean m_Connected;
        private void ConnectCallback(IAsyncResult result)
        {
            m_Connected = true;
        }

        /// <summary>
        /// Sends the Start Of Message (SOM) to the embedded PTU. 
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 SendStartOfMessage()
        {
            byte[] startOfMessage = { Protocol.THE_SOM };

            Int16 errorCode = TransmitMessage(startOfMessage);
            
            return errorCode;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 ReceiveStartOfMessage()
        {
            byte[] startOfMessage = new byte[1];
            Int32 bytesRead = 0;

            Int16 errorCode = ReceiveMessage(startOfMessage, ref bytesRead);
            if (errorCode < 0)
            {
                return errorCode;
            }

            // Verify the correct number of bytes were read
            if (bytesRead != 1)
            {
                m_TCPError = TCPErrors.ExcessiveBytesReceived;
                return -1;
            }

            m_TargetStartOfMessage = startOfMessage[0];

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="txMessage"></param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 SendDataToTarget(Byte[] txMessage)
        {
            Int16 errorCode;
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
        ///
        /// </summary>
        /// <param name="rxMessage"></param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 ReceiveTargetDataPacket(Byte[] rxMessage, out Int32 bytesReceived)
        {
            bytesReceived = 0;

            Int16 errorCode = ReceiveStartOfMessage();
            if (errorCode < 0)
            {
                return errorCode;
            }

            errorCode = ReceiveMessage(rxMessage, ref bytesReceived);

            return errorCode;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 ReceiveTargetAcknowledge()
        {
            byte[] rxMessage = new Byte[1];
            Int32 bytesRead = 0;

            Int16 errorCode = ReceiveMessage (rxMessage, ref bytesRead);
            if (errorCode < 0)
            {
                return errorCode;
            }

            // Verify the correct number of bytes were read
            if (bytesRead != 1)
            {
                m_TCPError = TCPErrors.ReceiveMessage;
                return -1;
            }

            // Verify ACK received
            if (rxMessage[0] != Protocol.PTU_ACK)
            {
                m_TCPError = TCPErrors.AckNotReceieved;
                return -1;
            }

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 Close()
        {
            try
            {
                m_Client.Client.Shutdown(SocketShutdown.Send);
                m_Client.Close();
            }
            catch (Exception e)
            {
                m_TCPError = TCPErrors.Close;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// The target is responsible for reporting whether it is a big or little endian machine. The start of
        /// message received from the target indicates the machine type of target
        /// </summary>
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
        /// Sends a message to the embedded TCP server target from the TCP client
        /// </summary>
        /// <param name="txMessage"></param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int16 TransmitMessage(Byte[] txMessage)
        {
            // Send the entire message to the serial port
            try
            {
                m_Client.Client.Send(txMessage);
            }
            catch (Exception e)
            {
                m_TCPError = TCPErrors.TransmitMessage;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rxMessage">buffer where the received message is stored</param>
        /// <param name="bytesRead">updated with the number of bytes read</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int16 ReceiveMessage(Byte[] rxMessage, ref Int32 bytesRead)
        {
            try
            {
                bytesRead = m_Client.Client.Receive(rxMessage);
            }
            catch (Exception e)
            {
                m_TCPError = TCPErrors.ReceiveMessage;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return 0;
        }

        #endregion --- Methods ---
    }
}