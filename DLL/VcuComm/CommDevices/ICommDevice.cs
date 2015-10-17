using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VcuComm
{
    public interface ICommDevice
    {
        Int16 Open(String commaDelimitedOptions);

        Int16 SendStartOfMessage();
        
        Int16 ReceiveStartOfMessage();

        Int16 SendDataToTarget(Byte[] txMessage);

        Int16 ReceiveTargetDataPacket(Byte []rxMessage);

        Int16 ReceiveTargetAcknowledge();

        Int16 Close(String commaDelimitedOptions);

        Boolean IsTargetBigEndian();

    }
}
