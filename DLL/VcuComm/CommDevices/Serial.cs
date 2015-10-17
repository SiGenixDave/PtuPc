using System;
using System.IO.Ports;
using System.Diagnostics;

namespace VcuComm
{
    public class Serial : ICommDevice
    {
        private SerialPort m_SerialPort;
        private byte m_TargetStartOfMessage;

        public short Open(string commaDelimitedOptions)
        {
            String[] options = commaDelimitedOptions.Split(',');

            String portName = options[0];
            Int32 baudRate = Convert.ToInt32(options[1]);
            
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

            Int32 dataBits = Convert.ToInt32(options[3]);
            
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


            m_SerialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            m_SerialPort.Open();

            m_SerialPort.DiscardOutBuffer();
            m_SerialPort.DiscardInBuffer();

            return 0;
        }

        public short SendStartOfMessage()
        {
            byte[] startOfMessage = { Protocol.THE_SOM };

            m_SerialPort.Write(startOfMessage, 0, startOfMessage.Length);

            return 0;
        }

        public short ReceiveStartOfMessage()
        {
            byte[] startOfMessage = new byte[1];

            m_SerialPort.Read(startOfMessage, 0, startOfMessage.Length);

            m_TargetStartOfMessage = startOfMessage[0];

            return -1;
        }

        public short SendDataToTarget(byte[] txMessage)
        {
            SendStartOfMessage();
            ReceiveStartOfMessage();

            m_SerialPort.Write(txMessage, 0, txMessage.Length);
            
            // wait for the serial logic to echo back the message just received
            Int32 bytesRead = 0;
            while (true)
            {
                bytesRead += m_SerialPort.Read(txMessage, 0, txMessage.Length);
                if (bytesRead >= txMessage.Length)
                {
                    break;
                }
            }

            return 0;
        }

        public short ReceiveTargetDataPacket(Byte[] rxMessage)
        {
            ReceiveStartOfMessage();

            Int32 offset = 0;
            Int32 bytesRead = 0;

            try
            {
                while (true)
                {
                    bytesRead = m_SerialPort.Read(rxMessage, offset, rxMessage.Length - offset);
                    offset += bytesRead;
                    if (offset >= rxMessage[0])
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            
            return 0;
        }

        public short ReceiveTargetAcknowledge()
        {
            throw new NotImplementedException();
        }

        public short Close(string commaDelimitedOptions)
        {
            throw new NotImplementedException();
        }

        public bool IsTargetBigEndian()
        {
            if (m_TargetStartOfMessage == Protocol.TARGET_BIG_ENDIAN_SOM)
            {
                return true;
            }
            return false;
        }
    }
}