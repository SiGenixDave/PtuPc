using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VcuComm
{
    interface ICommRequest
    {
        Byte[] GetByteArray(Boolean targetIsBigEndian);
    }
}
