using System;
using System.Runtime.InteropServices;
using System.IO;

namespace VcuComm
{
    public class Protocol
    {
        public static readonly byte TARGET_BIG_ENDIAN_SOM = (byte)'S';
        public static readonly byte THE_SOM = (byte)':';
        public const UInt16 MAX_WATCH_ELEMENTS = 40;

        public class DataPacketProlog
        {
            private UInt16 packetLength;
            private UInt16 packetType;
            private UInt16 checksum;
            private UInt16 responseType;

            private readonly UInt16 HEADER_SIZE_BYTES = 8;

            public Byte[] GetByteArray(Byte []payload, PacketType packetType, ResponseType responseType, Boolean targetIsBigEndian)
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

        public class SetWatchElementReq : ICommRequest
        {
            private UInt16 ElementIndex;
            private UInt16 DictionaryIndex;

            private SetWatchElementReq() { }
            public SetWatchElementReq(UInt16 ElementIndex, UInt16 DictionaryIndex)
            {
                this.ElementIndex = ElementIndex;
                this.DictionaryIndex = DictionaryIndex;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.ElementIndex = Utils.ReverseByteOrder(this.ElementIndex);
                    this.DictionaryIndex = Utils.ReverseByteOrder(this.DictionaryIndex);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.ElementIndex);
                bw.Write(this.DictionaryIndex);

                return dpp.GetByteArray(ms.ToArray(), PacketType.SET_WATCH_ELEMENT, ResponseType.COMMANDREQUEST, targetIsBigEndian);
            }
        }

        public class SetWatchElementsReq : ICommRequest
        {
            public UInt16[] WatchElement;  // WatchSize

            private SetWatchElementsReq() { }
            public SetWatchElementsReq(UInt16[] WatchElement)
            {
                this.WatchElement = new UInt16[WatchElement.Length];
                for (UInt16 i = 0; i < WatchElement.Length; i++)
                {
                    this.WatchElement[i] = WatchElement[i];
                }
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    for (UInt16 i = 0; i < this.WatchElement.Length; i++)
                    {
                        this.WatchElement[i] = Utils.ReverseByteOrder(this.WatchElement[i]);
                    }
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                for (UInt16 i = 0; i < this.WatchElement.Length; i++)
                {
                    bw.Write(this.WatchElement[i]);
                }

                return dpp.GetByteArray(ms.ToArray(), PacketType.SET_WATCH_ELEMENTS, ResponseType.COMMANDREQUEST, targetIsBigEndian);

            }
        };

        public class UpdateWatchElementsReq : ICommRequest
        {
            public byte ForceFullUpdate;
            private UpdateWatchElementsReq() { }
            public UpdateWatchElementsReq(byte ForceFullUpdate)
            {
                this.ForceFullUpdate = ForceFullUpdate;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                Byte[] payload = { ForceFullUpdate };

                return dpp.GetByteArray(payload, PacketType.SET_WATCH_ELEMENTS, ResponseType.COMMANDREQUEST, targetIsBigEndian);

            }


        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct UpdateWatchElementsRes
        {
            public DataPacketProlog Header;
            public UInt16 NumberOfUpdates;
            public WatchElement[] Watch;  // WatchSize
        }

        public struct ReadVariableReq
        {
            public DataPacketProlog Header;
            public UInt16 DictionaryIndex;
        }

        public struct ReadVariableRes
        {
            public DataPacketProlog Header;
            public UInt32 CurrentValue;
            public UInt16 DataType;
        }

        public struct SendVariableReq
        {
            public DataPacketProlog Header;
            public UInt16 DictionaryIndex;
            public UInt32 NewValue;
        }

        public struct GetDictionarySizeRes
        {
            public DataPacketProlog Header;
            public UInt16 DictionarySize;
        }

        public struct GetVariableInfoReq
        {
            public DataPacketProlog Header;
            public UInt16 DictionaryIndex;
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

        public class SetChartModeReq
        {
            public Byte TargetChartMode;
            private SetChartModeReq() { }
            public SetChartModeReq(byte TargetChartMode)
            {
                this.TargetChartMode = TargetChartMode;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                Byte[] payload = { this.TargetChartMode };

                return dpp.GetByteArray(payload, PacketType.SET_CHART_MODE, ResponseType.COMMANDREQUEST, targetIsBigEndian);

            }

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetChartIndexReq
        {
            public DataPacketProlog Header;
            public Byte ChartIndex;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetChartIndexRes
        {
            public DataPacketProlog Header;
            public UInt16 VariableIndex;
        }

        public class SetChartIndexReq_t
        {
            public DataPacketProlog Header;
            public UInt16 VariableIndex;
            public Byte ChartIndex;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SetChartScaleReq
        {
            public DataPacketProlog Header;
            public UInt16 DictionaryIndex;
            public Int32 MaxScale;
            public Int32 MinScale;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetWatchValuesReq
        {
            public DataPacketProlog Header;
            public UInt16[] WatchIndexes; //WatchSize;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetWatchValuesRes
        {
            public DataPacketProlog Header;
            public UInt32[] WatchValues; //WatchSize;
            public Byte[] DataType; //WatchSize;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SetTimeDateReq
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
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Set4TimeDateReq
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SelfTestCommandReq
        {
            public DataPacketProlog Header;
            public Byte CommandID;
            public Byte TruckInformation;
            public UInt16 Data;
            public UInt16[] TestSet; //100;
        }

#if DAS
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SetFaultLogReq
        {
            public DataPacketProlog Header;
            public Byte TargetState;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetFaultIndicesRes
        {
            public DataPacketProlog Header;
            public UInt32 Newest;
            public UInt32 Oldest;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetFaultDataReq
        {
            public DataPacketProlog Header;
            public UInt32 FaultIndex;
            public UInt16 NumberOfFaults;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetFaultDataRes
        {
            public DataPacketProlog Header;
            public UInt16 BufferSize;
            public Byte[] Buffer; //MAXFAULTBUFFERSIZE;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetFaultFlagRes
        {
            public DataPacketProlog Header;
            private Int16 BufferSize;
            public UInt16[] EnableFlag; //(MAXTASKS * MAXEVENTS / 16) + 1;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetStreamFlagRes
        {
            public DataPacketProlog Header;
            private Int16 BufferSize;
            public UInt16[] DatalogFlag; //(MAXTASKS * MAXEVENTS / 16) + 1;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SetFaultFlagReq
        {
            public DataPacketProlog Header;
            public UInt16 TaskID;
            public UInt16 FaultID;
            public Byte EnableFlag;
            public Byte DatalogFlag;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetFaultHistoryReq
        {
            public DataPacketProlog Header;
            public UInt16 TaskID;
            public UInt16 FaultID;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetFaultHistoryRes
        {
            public DataPacketProlog Header;
            public UInt16 StaticHistory;
            public UInt16 DynamicHistory;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetDatalogBufferReq
        {
            public DataPacketProlog Header;
            public UInt16 DatalogIndex;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetDatalogBufferRes
        {
            public DataPacketProlog Header;
            public UInt16 TimeOrigin;
            public UInt16 BufferSize;
            public Byte[] DatalogBuffer; //MAXDLBUFFERSIZE;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SetCarIDReq
        {
            public DataPacketProlog Header;
            public String NewCarID; //11;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SetStreamInfoReq
        {
            public DataPacketProlog Header;
            private StreamInformation Information;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetStreamInfoReq
        {
            public DataPacketProlog Header;
            public UInt16 StreamNumber;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetStreamInfoRes
        {
            public DataPacketProlog Header;
            private StreamInformation Information;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GetEventLogRes
        {
            public DataPacketProlog Header;
            public UInt16 CurrentEventLog;
            public UInt16 NumberEventLogs;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ChangeEventLogReq
        {
            public DataPacketProlog Header;
            public UInt16 NewEventLog;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ChangeEventLogRes
        {
            public DataPacketProlog Header;
            public UInt16 ChangeStatus;
            public UInt16 DataRecordingRate;
            public UInt16 MaxTasks;
            public UInt16 MaxEvents;
        }

        // BTU REQUEST/RESPONSE PACKET
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BTU_test
        {
            public DataPacketProlog Header;
            public UInt16 mode;
            public UInt16[] Buffer; //17;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct EnumValue
        {
            private char[] Description;  // 40
            private double Value;
        }

        /// /////////////////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////////////////

        public enum ResponseType
        {
            COMMANDREQUEST = 1,
            DATAREQUEST = 2,
        }

        public enum PacketType
        {
            SET_WATCH_ELEMENT = 2,
            SET_WATCH_ELEMENTS = 3,
            UPDATE_WATCH_ELEMENTS = 4,
            SET_CHART_SCALE = 5,

            // Intentionally empty [6]
            SEND_VARIABLE_VALUE = 7,

            GET_DICTIONARY_SIZE = 8,
            GET_VARIABLE_INFORMATION = 9,
            GET_EMBEDDED_INFORMATION = 10,
            GET_CHART_MODE = 11,
            SET_CHART_MODE = 12,
            GET_CHART_INDEX = 13,
            SET_CHART_INDEX = 14,
            GET_WATCH_VALUES = 15,
            GET_TIME_DATE = 16,
            SET_TIME_DATE = 17,
            START_SELF_TEST_TASK = 18,
            SELF_TEST_COMMAND = 19,
            GET_SELF_TEST_PACKET = 20,
            EXIT_SELF_TEST_TASK = 21,
            SET_FAULT_LOG = 22,
            GET_FAULT_INDICES = 23,
            GET_FAULT_HISTORY = 24,
            GET_FAULT_DATA = 25,
            GET_FAULT_FLAG = 26,
            SET_FAULT_FLAG = 27,

            // Intentionally empty [28-30]
            GET_DATALOG_STATUS = 31,

            GET_DATALOG_BUFFER = 32,

            //
            // Intentionally empty [33]
            //
            SET_CARID = 34,

            CLEAR_EVENTLOG = 35,
            INITIALIZE_EVENTLOG = 36,
            SET_STREAM_INFORMATION = 37,
            GET_STREAM_INFORMATION = 38,
            GET_DEFAULT_STREAM = 39,

            // Intentionally empty [40-49]
            START_CLOCK = 50,

            STOP_CLOCK = 51,
            CHANGE_EVENT_LOG = 52,
            GET_EVENT_LOG = 53,
            GET_STREAM_FLAG = 54,
            BTU = 55,

            // Intentionally empty [56-99]
            INITIALIZECOMMPORT = 100,

            CLOSECOMMPORT = 101,
            TERMINATECONNECTION = 102,
        }

        private enum VariableType
        {
            UINT_8_TYPE = 0,
            UINT_16_TYPE = 1,
            UINT_32_TYPE = 2,
            INT_8_TYPE = 3,
            INT_16_TYPE = 4,
            INT_32_TYPE = 5,
        }
    }
}