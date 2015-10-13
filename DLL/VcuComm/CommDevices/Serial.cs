using System;

namespace VcuComm
{
    public class Serial : ICommDevice
    {
        public short Open(string commaDelimitedOptions)
        {
            throw new NotImplementedException();
        }

        public short SendStartOfMessage()
        {
            throw new NotImplementedException();
        }

        public short ReceiveStartOfMessage()
        {
            throw new NotImplementedException();
        }

        public short SendDataToTarget(byte[] txMessage)
        {
            throw new NotImplementedException();
        }

        public short ReceiveTargetDataPacket(out byte[] rxMessage)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}