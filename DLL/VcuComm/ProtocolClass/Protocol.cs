using System;
using System.IO;

namespace VcuComm
{
    public partial class Protocol
    {
        public static readonly byte TARGET_BIG_ENDIAN_SOM = (byte)'S';
        public static readonly byte THE_SOM = (byte)':';
        public static readonly byte PTU_ACK = (byte)0x04;
        public const UInt16 MAX_WATCH_ELEMENTS = 40;
        public const UInt16 HEADER_SIZE_BYTES = 8;

        /// <summary>
        /// These errors are logged whenever any error is detected when a TCP transaction occurs
        /// </summary>
        public enum Errors
        {
            None,
            OptionsLengthIncorrect,
            ClientPreviouslyCreated,
            InvalidURL,
            UnresolvableURL,
            DataBitsConversion,
            BaudRateConversion,
            OpenSerialPort,
            SerialBufferFlush,
            ConnectionError,
            TransmitMessage,
            ReceiveMessage,
            AckNotReceieved,
            RxTimeout,
            MessageEcho,
            ExcessiveBytesReceived,
            InvalidSOM,
            Close,
        }


        public class DataPacketProlog
        {
            private UInt16 packetLength;
            private UInt16 packetType;
            private UInt16 checksum;
            private UInt16 responseType;

            public Byte[] GetByteArray(Byte[] payload, PacketType packetType, ResponseType responseType, Boolean targetIsBigEndian)
            {
                UInt16 payloadLength = 0;

                if (payload != null)
                {
                    payloadLength = (UInt16)(payload.Length);
                }
                this.packetLength = (UInt16)(HEADER_SIZE_BYTES + payloadLength);
                this.packetType = (UInt16)(packetType);
                this.responseType = (UInt16)(responseType);
                this.checksum = 0;

                if (targetIsBigEndian)
                {
                    this.packetLength = Utils.ReverseByteOrder(this.packetLength);
                    this.responseType = Utils.ReverseByteOrder(this.responseType);
                    this.checksum = Utils.ReverseByteOrder(this.checksum);
                    this.packetType = Utils.ReverseByteOrder(this.packetType);
                }
                MemoryStream ms = new MemoryStream(4096);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.packetLength);
                bw.Write(this.packetType);
                bw.Write(this.checksum);
                bw.Write(this.responseType);
                if (payload != null)
                {
                    bw.Write(payload);
                }
                return ms.ToArray();
            }
        }

        // Message Structures
        public struct WatchElement
        {
            public UInt16 Index;
            public UInt32 NewValue;
            public UInt16 DataType;
        }

        public struct StreamVariable
        {
            public UInt16 Variable;
            public UInt16 VariableType;
        }

        public struct StreamInformation
        {
            public UInt16 NumberOfVariables;
            public UInt16 NumberOfSamples;
            public UInt16 SampleRate;
            public StreamVariable[] StreamVariableInfo; //256
        }


        public struct UpdateWatchElementsRes
        {
            public DataPacketProlog Header;
            public UInt16 NumberOfUpdates;
            public WatchElement[] Watch;  // WatchSize
        }

        public struct ReadVariableRes
        {
            public DataPacketProlog Header;
            public UInt32 CurrentValue;
            public UInt16 DataType;
        }

        public struct GetDictionarySizeRes
        {
            public DataPacketProlog Header;
            public UInt16 DictionarySize;
        }

        public struct GetVariableInfoRes
        {
            public DataPacketProlog Header;
            public UInt16 DataType;
            public Int32 MaxScale;
            public Int32 MinScale;
            public UInt32 AttributeFlags;
        };

        public struct SetConfigNumRes
        {
            public DataPacketProlog Header;
            public Int32 ConfigNum;
        }

        public struct GetEmbeddedInfoRes
        {
            public String SoftwareVersion;   //41
            public String CarID;  //11
            public String SubSystemName; //41
            public String IdentifierString; //5;
            public UInt32 ConfigurationMask;
        }

        public struct GetChartModeRes
        {
            public Byte CurrentChartMode;
        }


        public struct GetChartIndexRes
        {
            public DataPacketProlog Header;
            public UInt16 VariableIndex;
        }

        public struct GetWatchValuesRes
        {
            public DataPacketProlog Header;
            public UInt32[] WatchValues; //WatchSize;
            public Byte[] DataType; //WatchSize;
        }

        public struct GetTimeDateRes
        {
            public DataPacketProlog Header;
            public Byte Hour;
            public Byte Minute;
            public Byte Second;
            public Byte Year;
            public Byte Month;
            public Byte Day;
        }

        // Some projects, e.g. R188 use a 4 digit year code for the time/date functions.
        // These structures are included to support these projects.
        public struct Get4TimeDateRes
        {
            public DataPacketProlog Header;
            public Byte Hour;
            public Byte Minute;
            public Byte Second;
            public Byte Temp;	// Introduced to ensure the structure still lies on a word boundary.
            public UInt16 Year;
            public Byte Month;
            public Byte Day;
        }

        public struct SelfTestCommandReq
        {
            public DataPacketProlog Header;
            public Byte CommandID;
            public Byte TruckInformation;
            public UInt16 Data;
            public UInt16[] TestSet; //100;
        }

#if DAS
        public struct GetSelfTestPacketRes
        {
	        public DataPacketProlog    Header;
	        public Byte                Valid;
	        public Byte                MessageMode;
	        public UInt16			    SetInformation;
	        public UInt16				TestID;
	        st_test_result_data ResultsData;
	        public UInt32              Flags;
	        public UInt16				Reserved[2];
	        //UINT8				Reserved[4];
	        st_msg_var_str      VariableMsg[MAXSTVARIABLES];
        }
#endif



        public struct GetFaultIndicesRes
        {
            public DataPacketProlog Header;
            public UInt32 Newest;
            public UInt32 Oldest;
        }



        public struct GetFaultDataRes
        {
            public UInt16 BufferSize;
            public Byte[] Buffer; //MAXFAULTBUFFERSIZE;
        }

        public struct GetFaultFlagRes
        {
            public DataPacketProlog Header;
            public Int16 BufferSize;
            public UInt16[] EnableFlag; //(MAXTASKS * MAXEVENTS / 16) + 1;
        }

        public struct GetStreamFlagRes
        {
            public DataPacketProlog Header;
            public Int16 BufferSize;
            public UInt16[] DatalogFlag; //(MAXTASKS * MAXEVENTS / 16) + 1;
        }



        public struct GetFaultHistoryRes
        {
            public DataPacketProlog Header;
            public UInt16 StaticHistory;
            public UInt16 DynamicHistory;
        }


        public struct GetDatalogBufferRes
        {
            public DataPacketProlog Header;
            public UInt16 TimeOrigin;
            public UInt16 BufferSize;
            public Byte[] DatalogBuffer; //MAXDLBUFFERSIZE;
        }

        public struct GetStreamInfoRes
        {
            public StreamInformation Information;
        }

        public struct GetEventLogRes
        {
            public DataPacketProlog Header;
            public UInt16 CurrentEventLog;
            public UInt16 NumberEventLogs;
        }


        public struct ChangeEventLogRes
        {
            public DataPacketProlog Header;
            public UInt16 ChangeStatus;
            public UInt16 DataRecordingRate;
            public UInt16 MaxTasks;
            public UInt16 MaxEvents;
        }

        // BTU REQUEST/RESPONSE PACKET
        public struct BTU_test
        {
            public DataPacketProlog Header;
            public UInt16 mode;
            public UInt16[] Buffer; //17;
        }

#if NOT_USED_AS_FAR_I_CAN_TELL
        public struct DataDictionary
        {
            private char[] VariableName;   //40
            private char[] EmbeddedName;   //50
            private char[] FormatString;   //20
            private double ScaleFactor;
            private char[] TargetUnits;    //50
            private double MaxScale;
            private double MinScale;
            private double UpperBounds;
            private double LowerBounds;
            public Int32 AttributeFlags;
            public Int32 EnumbitID;
            public Int32 HelpIndex;
            private double BitMask;
            private Int16 DataType;
        }
#endif
        public struct EnumValue
        {
            public char[] Description;  // 40
            public double Value;
        }

        /// /////////////////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////////////////

    }
}