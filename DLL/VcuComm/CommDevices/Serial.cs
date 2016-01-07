#region --- Revision History ---

/*
 *
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.
 *  Offenders will be held liable for the payment of damages.
 *
 *  (C) 2016    Bombardier Inc. or its subsidiaries. All rights reserved.
 *
 *  Solution:   PTU
 *
 *  Project:    VcuComm
 *
 *  File name:  Serial.cs
 *
 *  Revision History
 *  ----------------
 *
 *  Date        Version Author       Comments
 *  03/01/2016  1.0     D.Smail      First Release.
 *
 */

#endregion --- Revision History ---

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
        /// <summary>
        ///
        /// </summary>
        private static readonly String NO_SERIAL_ISSUES = "No Exceptions Raised";

        #region --- Member Variables ---

        /// <summary>
        /// Maintains the exception message thrown when a serial port error occurs. Allows the calling function
        /// to ascertain more detail about the error.
        /// </summary>
        private String m_ExceptionMessage = NO_SERIAL_ISSUES;

        /// <summary>
        /// Used to adjust the read timeout of the serial port. -1 means the receive call will wait forever until
        /// a character is received
        /// </summary>
        private Int32 m_ReadTimeout = -1;

        /// <summary>
        /// Stores the most recent serial port error. Cleared whenever a calling function reads the state.
        /// </summary>
        private ProtocolPTU.Errors m_SerialError = ProtocolPTU.Errors.None;

        /// <summary>
        /// object that contains the serial port object
        /// </summary>
        private SerialPort m_SerialPort;

        /// <summary>
        /// contains the start of message byte received from the target
        /// </summary>
        private byte m_TargetStartOfMessage;

        #endregion --- Member Variables ---

        #region --- Properties ---

        /// <summary>
        /// TODO
        /// </summary>
        public ProtocolPTU.Errors Error
        {
            get
            {
                ProtocolPTU.Errors serialErrCopy = m_SerialError;
                // Reset the error after the error code is read; if it isn't read
                // than the most recent error will be saved until another error occurs
                m_SerialError = ProtocolPTU.Errors.None;
                return serialErrCopy;
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
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

        /// <summary>
        /// TODO
        /// </summary>
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

        #endregion --- Properties ---

        #region --- Methods ---

        #region --- Public Methods ---

        /// <summary>
        /// Closes the serial port
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int32 Close()
        {
            try
            {
                m_SerialPort.Close();
            }
            catch (Exception e)
            {
                m_SerialError = ProtocolPTU.Errors.Close;
                m_ExceptionMessage = e.Message;
                return -1;
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
        public bool IsTargetBigEndian()
        {
            if (m_TargetStartOfMessage == ProtocolPTU.TARGET_BIG_ENDIAN_SOM)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Opens the desired serial port based on the comma delimited string passed as the argument. The first argument
        /// is the COM port (e.g. COM1). The 2nd argument in the string is the baud rate (e.g. 19200). The 3rd argument is
        /// the parity (e.g. none, odd, or even). The 4th argument is the number of data bits (e.g. 8). The 5th argument is
        /// the number of stop bits (e.g. 1).
        /// </summary>
        /// <param name="commaDelimitedOptions">COM port settings - "COMX,BaudRate,Parity,DataBits,StopBits"</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int32 Open(string commaDelimitedOptions)
        {
            String[] options;
            options = commaDelimitedOptions.Split(',');

            // verify 5 comma delimited arguments
            if (options.Length != 5)
            {
                m_SerialError = ProtocolPTU.Errors.OptionsLengthIncorrect;
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
                m_SerialError = ProtocolPTU.Errors.BaudRateConversion;
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
                m_SerialError = ProtocolPTU.Errors.DataBitsConversion;
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
                m_SerialError = ProtocolPTU.Errors.OpenSerialPort;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            // Flush the receive buffer
            Int32 errorCode = FlushRxBuffer();
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
        /// Reads the serial port and verifies the target acknowledged the message. Target acknowledges
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
                    // bytesRead in this case is an error code
                    return bytesRead;
                }

                if (bytesRead == 1)
                {
                    // Verify ACK received
                    if (rxMessage[0] != ProtocolPTU.PTU_ACK)
                    {
                        m_SerialError = ProtocolPTU.Errors.AckNotReceieved;
                        return -1;
                    }
                }
                else if (bytesRead > 1)
                {
                    // too many bytes read
                    m_SerialError = ProtocolPTU.Errors.ExcessiveBytesReceived;
                    FlushRxBuffer();
                    return -1;
                }
            }

            return 0;
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
                // read the serial receive buffer
                bytesRead = ReceiveMessage(rxMessage, totalBytesRead);
                if (errorCode < 0)
                {
                    return errorCode;
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
                    m_SerialError = ProtocolPTU.Errors.ExcessiveBytesReceived;
                    FlushRxBuffer();
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Send a message to the embedded PTU target. The SOM is sent and then waits for an echo from the target.
        /// The message is then sent and an echo that is identical to the message sent is verified.
        /// </summary>
        /// <param name="txMessage">the message to be sent to the target</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        public Int32 SendMessageToTarget(byte[] txMessage)
        {
            // Transmit the message to the target
            Int32 errorCode = TransmitMessage(txMessage);
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
                bytesRead = ReceiveMessage(rxMessage, totalBytesRead);
                if (bytesRead < 0)
                {
                    return bytesRead;
                }
                totalBytesRead += bytesRead;
                if (totalBytesRead > rxMessage.Length)
                {
                    // too many bytes read
                    m_SerialError = ProtocolPTU.Errors.ExcessiveBytesReceived;
                    FlushRxBuffer();
                    return -1;
                }
            }

            // This compares the contents of the 2 arrays
            if (txMessage.SequenceEqual(rxMessage) == false)
            {
                // log error
                m_SerialError = ProtocolPTU.Errors.MessageEcho;
                return -1;
            }

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Int32 SendReceiveSOM()
        {
            Int32 errorCode;
            errorCode = SendStartOfMessage();

            if (errorCode < 0)
            {
                return errorCode;
            }

            errorCode = ReceiveStartOfMessage();
            return errorCode;
        }

        #endregion --- Public Methods ---

        #region --- Private Methods ---

        /// <summary>
        /// Flushes the serial port receive buffer
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int32 FlushRxBuffer()
        {
            try
            {
                m_SerialPort.DiscardInBuffer();
            }
            catch (Exception e)
            {
                m_SerialError = ProtocolPTU.Errors.SerialBufferFlush;
                m_ExceptionMessage = e.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Flushes the serial port transmit buffer
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int32 FlushTxBuffer()
        {
            try
            {
                m_SerialPort.DiscardOutBuffer();
            }
            catch (Exception e)
            {
                m_SerialError = ProtocolPTU.Errors.SerialBufferFlush;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return 0;
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
                bytesRead = m_SerialPort.Read(rxMessage, bufferOffset, rxMessage.Length - bufferOffset);
            }
            catch (Exception e)
            {
                m_SerialError = ProtocolPTU.Errors.ReceiveMessage;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return bytesRead;
        }

        /// <summary>
        /// Receives the Start of Message byte from the target
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
                    if ((startOfMessage[0] != ProtocolPTU.THE_SOM) && (startOfMessage[0] != ProtocolPTU.TARGET_BIG_ENDIAN_SOM))
                    {
                        m_TargetStartOfMessage = 0;
                        m_SerialError = ProtocolPTU.Errors.InvalidSOM;
                        return -1;
                    }
                }
                else if (bytesRead > 1)
                {
                    // too many bytes read
                    m_SerialError = ProtocolPTU.Errors.ExcessiveBytesReceived;
                    FlushRxBuffer();
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Sends the Start of Message byte to the target
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        private Int32 SendStartOfMessage()
        {
            byte[] startOfMessage = { ProtocolPTU.THE_SOM };

            Int32 errorCode = TransmitMessage(startOfMessage);

            return errorCode;
        }

        /// <summary>
        /// Sends a message to the target via the serial port
        /// </summary>
        /// <param name="txMessage">the byte array to be sent to the embedded PTU</param>
        /// <returns>less than 0 if any failure occurs; number of bytes sent if successful</returns>
        private Int32 TransmitMessage(Byte[] txMessage)
        {
            // Send the entire message to the serial port
            try
            {
                m_SerialPort.Write(txMessage, 0, txMessage.Length);
            }
            catch (Exception e)
            {
                m_SerialError = ProtocolPTU.Errors.TransmitMessage;
                m_ExceptionMessage = e.Message;
                return -1;
            }
            return txMessage.Length;
        }

        #endregion --- Private Methods ---

        #endregion --- Methods ---
    }
}