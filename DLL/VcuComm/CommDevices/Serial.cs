using System;
using System.IO.Ports;
using System.Linq;

namespace VcuComm
{
    /// <summary>
    /// Used to communicate to a target PTU via RS-232 (Serial)
    /// </summary>
    public class Serial : ICommDevice
    {
        private static readonly String NO_SERIAL_ISSUES = "No Exceptions Raised";


        #region --- Enumerations ---

        /// <summary>
        /// Types of serial errors that can be encountered during the course of normal operation
        /// </summary>
        public enum SerialErrors
        {
            None,
            OptionsLengthIncorrect,
            BaudRateConversion,
            DataBitsConversion,
            OpenSerialPort,
            SerialBufferFlush,
            TransmitMessage,
            ReceiveMessage,
            AckNotReceieved,
            MessageEcho,
            RxTimeout,
            ExcessiveBytesReceived,
            Close,
        }

        #endregion --- Enumerations ---

        #region --- Member Variables ---

        /// <summary>
        /// object that contains the serial port object
        /// </summary>
        private SerialPort m_SerialPort;

        /// <summary>
        /// contains the start of message byte received from the target
        /// </summary>
        private byte m_TargetStartOfMessage;

        /// <summary>
        /// Used to adjust the read timeout of the serial port. -1 means the receive call will wait forever until
        /// a character is received
        /// </summary>
        private Int32 m_ReadTimeout = -1;

        public Int32 ReadTimeout
        {
            get
            {
                return m_ReadTimeout;
            }
            set
            {
                m_ReadTimeout = value;
            }
        }

        /// <summary>
        /// Stores the most recent serial port error. Cleared whenever a calling function reads the state.
        /// </summary>
        private SerialErrors m_SerialError = SerialErrors.None;

        public SerialErrors SerialError
        {
            get
            {
                SerialErrors serialErrCopy = m_SerialError;
                // Reset the error after the error code is read; if it isn't read
                // than the most recent error will be saved until another error occurs
                m_SerialError = SerialErrors.None;
                return serialErrCopy;
            }
        }

        /// <summary>
        /// Maintains the exception message thrown when a serial port error occurs. Allows the calling function
        /// to ascertain more detail about the error.
        /// </summary>
        private String m_ExceptionMessage = NO_SERIAL_ISSUES;

        public String ExceptionMessage
        {
            get
            {
                String exceptionMsgCopy = m_ExceptionMessage;
                // Reset the error after the error message is read; if it isn't read
                // than the most recent error will be saved until another error occurs
                m_ExceptionMessage = NO_SERIAL_ISSUES;
                return exceptionMsgCopy;
            }
        }

        #endregion --- Member Variables ---

        #region --- Methods ---

        /// <summary>
        /// Gets and returns all available serial ports that currently exist on the PC.
        /// </summary>
        /// <returns>All serial ports currently available on the PC</returns>
        static public String[] GetAvailableSerialPorts()
        {
            return SerialPort.GetPortNames();
        }


        /// <summary>
        /// Opens the desired serial port based on the comma delimited string passed as the argument. The first argument
        /// is the COM port (e.g. COM1). The 2nd argument in the string is the baud rate (e.g. 19200). The 3rd argument is
        /// the parity (e.g. none, odd, or even). The 4th argument is the number of data bits (e.g. 8). The 5th argument is
        /// the number of stop bits (e.g. 1).
        /// </summary>
        /// <param name="commaDelimitedOptions">COM port settings - "COMX,BaudRate,Parity,DataBits,StopBits"</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 Open(string commaDelimitedOptions)
        {
            String[] options;
            options = commaDelimitedOptions.Split(',');

            // verify 5 comma delimited arguments
            if (options.Length != 5)
            {
                m_SerialError = SerialErrors.OptionsLengthIncorrect;
                return -1;
            }

            // Save the port name (e.g. COM1)
            String portName = options[0];

            Int32 baudRate;
            try
            {
                baudRate = Convert.ToInt32(options[1]);
            }
            catch (Exception e)
            {
                m_SerialError = SerialErrors.BaudRateConversion;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            // Default to no parity check
            Parity parity = Parity.None;
            switch (options[2].ToLower())
            {
                case "odd":
                    parity = Parity.Odd;
                    break;

                case "even":
                    parity = Parity.Even;
                    break;

                default:
                    break;
            }

            // Set the number of data bits
            Int32 dataBits;
            try
            {
                dataBits = Convert.ToInt32(options[3]);
            }
            catch (Exception e)
            {
                m_SerialError = SerialErrors.DataBitsConversion;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            // Default to 1 stop bit
            StopBits stopBits = StopBits.One;
            switch (options[4].ToLower())
            {
                case "0":
                    stopBits = StopBits.None;
                    break;

                case "2":
                    stopBits = StopBits.Two;
                    break;

                default:
                    break;
            }

            // Open the serial port
            try
            {
                m_SerialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
                m_SerialPort.Open();
                m_SerialPort.ReadTimeout = m_ReadTimeout;
            }
            catch (Exception e)
            {
                m_SerialError = SerialErrors.OpenSerialPort;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            // Flush the receive buffer
            Int16 errorCode = FlushRxBuffer();
            if (errorCode < 0)
            {
                return errorCode;
            }

            // Flush the transmit buffer
            errorCode = FlushTxBuffer();
            if (errorCode < 0)
            {
                return errorCode;
            }

            return 0;
        }

        /// <summary>
        /// Sends the Start of Message byte to the target
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 SendStartOfMessage()
        {
            byte[] startOfMessage = { Protocol.THE_SOM };

            Int16 errorCode = TransmitMessage(startOfMessage);

            return errorCode;
        }

        /// <summary>
        /// Receives the Start of Message byte from the target
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 ReceiveStartOfMessage()
        {
            // Allocate memory for the received byte
            byte[] startOfMessage = new byte[1];

            Int32 bytesRead = 0;
            while (bytesRead == 0)
            {
                Int16 errorCode = ReceiveMessage(startOfMessage, 0, ref bytesRead);

                // Verify a valid call was made to ReceiveMessage()
                if (errorCode < 0)
                {
                    return errorCode;
                }
                // Verify only 1 byte was read; if so save the byte
                if (bytesRead == 1)
                {
                    // TODO verify a valid SOM
                    m_TargetStartOfMessage = startOfMessage[0];
                }
                else if (bytesRead > 1)
                {
                    // too many bytes read
                    m_SerialError = SerialErrors.ExcessiveBytesReceived;
                    FlushRxBuffer();
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Send a message to the target. The SOM is sent and then waits for an echo from the target.
        /// The message is then sent and an echo that is identical to the message sent is verified.
        /// </summary>
        /// <param name="txMessage">the message to be sent to the target</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 SendDataToTarget(byte[] txMessage)
        {
            // Send the start of message
            Int16 errorCode = SendStartOfMessage();
            if (errorCode < 0)
            {
                return errorCode;
            }

            // Wait for the receive start of message
            errorCode = ReceiveStartOfMessage();
            if (errorCode < 0)
            {
                return errorCode;
            }

            // Transmit the message to the target
            errorCode = TransmitMessage(txMessage);
            if (errorCode < 0)
            {
                return errorCode;
            }

            // Create a buffer for the receive message which should be identical to the
            // message just sent
            Byte[] rxMessage = new Byte[txMessage.Length];

            // wait for the target logic to echo back the exact message just sent by the application
            // (NOTE: this is different than TCP which doesn't expect an echo)
            Int32 bytesRead = 0;
            Int32 totalBytesRead = 0;
            while (totalBytesRead != rxMessage.Length)
            {
                errorCode = ReceiveMessage(rxMessage, totalBytesRead, ref bytesRead);
                if (errorCode < 0)
                {
                    return errorCode;
                }
                totalBytesRead += bytesRead;
                if (totalBytesRead > rxMessage.Length)
                {
                    // too many bytes read
                    m_SerialError = SerialErrors.ExcessiveBytesReceived;
                    FlushRxBuffer();
                    return -1;
                }
            }

            // This compares the contents of the 2 arrays
            if (txMessage.SequenceEqual(rxMessage) == false)
            {
                // log error
                m_SerialError = SerialErrors.MessageEcho;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Receives a message from the target. It is assumed that the target sends a message with the first 2 bytes
        /// indicating the size of the message.
        /// </summary>
        /// <param name="rxMessage">array where the received message will be stored</param>
        /// <param name="bytesReceived">number of bytes in the received message</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 ReceiveTargetDataPacket(Byte[] rxMessage, out Int32 bytesReceived)
        {
            bytesReceived = 0;

            // Verify the target responds with a SOM first
            Int16 errorCode = ReceiveStartOfMessage();
            if (errorCode < 0)
            {
                return errorCode;
            }

            Int32 bytesRead = 0;
            Int32 totalBytesRead = 0;
            // When the first 2 bytes are read, the received message size will be calculated
            UInt32 messageSize = UInt32.MaxValue;
            while (totalBytesRead != messageSize)
            {
                // read the serial receive buffer
                errorCode = ReceiveMessage(rxMessage, totalBytesRead, ref bytesRead);
                if (errorCode < 0)
                {
                    return errorCode;
                }

                // adjust the index into the receive buffer in case the entire message wasn't received
                totalBytesRead += bytesRead;

                if ((totalBytesRead >= 2) && (messageSize == UInt32.MaxValue))
                {
                    // 1st 2 bytes of the message is the message length
                    messageSize = (UInt32)BitConverter.ToUInt16(rxMessage, 0);
                    if (IsTargetBigEndian())
                    {
                        messageSize = Utils.ReverseByteOrder(messageSize);
                    }
                }
                // Verify the expected receive message length isn't too long
                if (totalBytesRead > messageSize)
                {
                    // too many bytes read
                    m_SerialError = SerialErrors.ExcessiveBytesReceived;
                    FlushRxBuffer();
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Reads the serial port and verifies the target acknowledged the message. Target acknowledges
        /// the message sent from the application when no data is sent back from the target (i.e. a command was sent)
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 ReceiveTargetAcknowledge()
        {
            // Allocate memory for the acknowledge
            byte[] rxMessage = new Byte[1];

            Int32 bytesRead = 0;
            while (bytesRead == 0)
            {
                // Read the serial port
                Int16 errorCode = ReceiveMessage(rxMessage, 0, ref bytesRead);
                if (errorCode < 0)
                {
                    return errorCode;
                }

                if (bytesRead == 1)
                {
                    // Verify ACK received
                    if (rxMessage[0] != Protocol.PTU_ACK)
                    {
                        m_SerialError = SerialErrors.AckNotReceieved;
                        return -1;
                    }
                }
                else if (bytesRead > 1)
                {
                    // too many bytes read
                    m_SerialError = SerialErrors.ExcessiveBytesReceived;
                    FlushRxBuffer();
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Closes the serial port
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int16 Close()
        {
            try
            {
                m_SerialPort.Close();
            }
            catch (Exception e)
            {
                m_SerialError = SerialErrors.Close;
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
        public bool IsTargetBigEndian()
        {
            if (m_TargetStartOfMessage == Protocol.TARGET_BIG_ENDIAN_SOM)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sends a message to the target via the serial port
        /// </summary>
        /// <param name="txMessage"></param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int16 TransmitMessage(Byte[] txMessage)
        {
            // Send the entire message to the serial port
            try
            {
                m_SerialPort.Write(txMessage, 0, txMessage.Length);
            }
            catch (Exception e)
            {
                m_SerialError = SerialErrors.TransmitMessage;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rxMessage">buffer where the received message is stored</param>
        /// <param name="bufferOffset">offset into the receive buffer</param>
        /// <param name="bytesRead">updated with the number of bytes read</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int16 ReceiveMessage(Byte[] rxMessage, Int32 bufferOffset, ref Int32 bytesRead)
        {
            try
            {
                bytesRead = m_SerialPort.Read(rxMessage, bufferOffset, rxMessage.Length - bufferOffset);
            }
            catch (Exception e)
            {
                m_SerialError = SerialErrors.ReceiveMessage;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Flushes the serial port receive buffer
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int16 FlushRxBuffer()
        {
            try
            {
                m_SerialPort.DiscardInBuffer();
            }
            catch (Exception e)
            {
                m_SerialError = SerialErrors.SerialBufferFlush;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Flushes the serial port transmit buffer
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int16 FlushTxBuffer()
        {
            try
            {
                m_SerialPort.DiscardOutBuffer();
            }
            catch (Exception e)
            {
                m_SerialError = SerialErrors.SerialBufferFlush;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return 0;
        }

        #endregion --- Methods ---
    }
}