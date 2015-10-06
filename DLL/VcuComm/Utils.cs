using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace VcuComm
{
    public static class Utils
    {
        public static Byte ReverseByteOrder(Byte value)
        {
            return value;
        }

        public static UInt32 ReverseByteOrder(UInt32 value)
        {
            UInt32 reversedValue;

            reversedValue = (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                            (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;

            return reversedValue;
        }

        public static UInt16 ReverseByteOrder(UInt16 value)
        {
            UInt16 reversedValue;

            reversedValue = (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);

            return reversedValue;
        }

    }
}