﻿using System;
using System.IO;
using System.Text;

namespace VcuComm
{
    /// <summary>
    /// 
    /// </summary>
    internal interface ICommRequest
    {
        Byte[] GetByteArray(Boolean targetIsBigEndian);
    }

    // TODO DATA_REQUESTS
    // UpdateWatchElements, GetVariableInfo, GetChartMode, GetChartIndex, GetTimeDate, GetSelfTestResult, GetSelfTestSpecialMessage,
    // GetFaultIndices, ChangeEventLog, GetEventLog,GetDefaultStreamInformation, GetStream, GetStreamInformation, GetFaultData,
    // GetFltHistInfo, GetFltFlag, GetFltFlagInfo

    public partial class Protocol
    {
        const UInt16 TX_STREAM_SIZE = 1024;

        /// <summary>
        /// 
        /// </summary>
        public class SetWatchElementReq : ICommRequest
        {
            private UInt16 ElementIndex;
            private UInt16 DictionaryIndex;
            private const PacketType PACKET_TYPE = PacketType.SET_WATCH_ELEMENT;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SetWatchElementReq()
            {
            }

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
                MemoryStream ms = new MemoryStream(TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.ElementIndex);
                bw.Write(this.DictionaryIndex);

                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class SetWatchElementsReq : ICommRequest
        {
            private UInt16[] WatchElement;  // WatchSize
            private const PacketType PACKET_TYPE = PacketType.SET_WATCH_ELEMENTS;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SetWatchElementsReq()
            {
            }

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

                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        };

        public class UpdateWatchElementsReq : ICommRequest
        {
            private byte ForceFullUpdate;
            private const PacketType PACKET_TYPE = PacketType.UPDATE_WATCH_ELEMENTS;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private UpdateWatchElementsReq()
            {
            }

            public UpdateWatchElementsReq(byte ForceFullUpdate)
            {
                this.ForceFullUpdate = ForceFullUpdate;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.ForceFullUpdate = Utils.ReverseByteOrder(this.ForceFullUpdate);
                }

                Byte[] payload = { this.ForceFullUpdate };

                return dpp.GetByteArray(payload, PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class SetChartModeReq : ICommRequest
        {
            private Byte TargetChartMode;
            private const PacketType PACKET_TYPE = PacketType.SET_CHART_MODE;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SetChartModeReq()
            {
            }

            public SetChartModeReq(byte TargetChartMode)
            {
                this.TargetChartMode = TargetChartMode;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.TargetChartMode = Utils.ReverseByteOrder(this.TargetChartMode);
                }

                Byte[] payload = { this.TargetChartMode };

                return dpp.GetByteArray(payload, PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class SetChartIndexReq : ICommRequest
        {
            private UInt16 VariableIndex;
            private Byte ChartIndex;
            private const PacketType PACKET_TYPE = PacketType.SET_CHART_INDEX;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SetChartIndexReq()
            {
            }

            public SetChartIndexReq(UInt16 VariableIndex, Byte ChartIndex)
            {
                this.VariableIndex = VariableIndex;
                this.ChartIndex = ChartIndex;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.VariableIndex = Utils.ReverseByteOrder(this.VariableIndex);
                    this.ChartIndex = Utils.ReverseByteOrder(this.ChartIndex);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.VariableIndex);
                bw.Write(this.ChartIndex);

                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

#if NOT_USED_AS_FAR_I_CAN_TELL
        public class ReadVariableReq : ICommRequest
        {
            private UInt16 DictionaryIndex;

            private ReadVariableReq()
            {
            }

            public ReadVariableReq(UInt16 DictionaryIndex)
            {
                this.DictionaryIndex = DictionaryIndex;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.DictionaryIndex = Utils.ReverseByteOrder(this.DictionaryIndex);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.DictionaryIndex);
                                                        //TODO ??? check below PacketType
                return dpp.GetByteArray(ms.ToArray(), PacketType.GET_VARIABLE_INFORMATION, ResponseType.COMMANDREQUEST, targetIsBigEndian);
            }
        }
#endif

        public class SendVariableReq : ICommRequest
        {
            private UInt16 DictionaryIndex;
            private UInt32 NewValue;
            private const PacketType PACKET_TYPE = PacketType.SEND_VARIABLE_VALUE;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SendVariableReq()
            {
            }

            public SendVariableReq(UInt16 DictionaryIndex, UInt32 NewValue)
            {
                this.DictionaryIndex = DictionaryIndex;
                this.NewValue = NewValue;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.DictionaryIndex = Utils.ReverseByteOrder(this.DictionaryIndex);
                    this.NewValue = Utils.ReverseByteOrder(this.NewValue);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.DictionaryIndex);
                bw.Write(this.NewValue);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class GetVariableInfoReq : ICommRequest
        {
            private UInt16 DictionaryIndex;
            private const PacketType PACKET_TYPE = PacketType.GET_VARIABLE_INFORMATION;
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private GetVariableInfoReq()
            {
            }

            public GetVariableInfoReq(UInt16 DictionaryIndex)
            {
                this.DictionaryIndex = DictionaryIndex;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.DictionaryIndex = Utils.ReverseByteOrder(this.DictionaryIndex);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.DictionaryIndex);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class GetChartIndexReq : ICommRequest
        {
            private Byte ChartIndex;
            private const PacketType PACKET_TYPE = PacketType.GET_CHART_INDEX;
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private GetChartIndexReq()
            {
            }

            public GetChartIndexReq(Byte ChartIndex)
            {
                this.ChartIndex = ChartIndex;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.ChartIndex = Utils.ReverseByteOrder(this.ChartIndex);
                }

                Byte[] payload = { this.ChartIndex };

                return dpp.GetByteArray(payload, PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class SetChartScaleReq : ICommRequest
        {
            private UInt16 DictionaryIndex;
            private Int32 MaxScale;
            private Int32 MinScale;
            private const PacketType PACKET_TYPE = PacketType.SET_CHART_SCALE;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SetChartScaleReq()
            {
            }

            public SetChartScaleReq(UInt16 DictionaryIndex, Int32 MaxScale, Int32 MinScale)
            {
                this.DictionaryIndex = DictionaryIndex;
                this.MaxScale = MaxScale;
                this.MinScale = MinScale;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.DictionaryIndex = Utils.ReverseByteOrder(this.DictionaryIndex);
                    this.MaxScale = (Int32)Utils.ReverseByteOrder((UInt32)(this.MaxScale));
                    this.MinScale = (Int32)Utils.ReverseByteOrder((UInt32)(this.MinScale));
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.DictionaryIndex);
                bw.Write(this.MaxScale);
                bw.Write(this.MinScale);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

#if NOT_USED_AS_FAR_I_CAN_TELL
        public class GetWatchValuesReq
        {
            private UInt16[] WatchIndexes; //WatchSize;
            private GetWatchValuesReq()
            {
            }

            public GetWatchValuesReq(UInt16[] WatchIndexes)
            {
                this.WatchIndexes = new UInt16[WatchIndexes.Length];
                for (UInt16 i = 0; i < WatchIndexes.Length; i++)
                {
                    this.WatchIndexes[i] = WatchIndexes[i];
                }
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    for (UInt16 i = 0; i < this.WatchIndexes.Length; i++)
                    {
                        this.WatchIndexes[i] = Utils.ReverseByteOrder(this.WatchIndexes[i]);
                    }
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                for (UInt16 i = 0; i < this.WatchIndexes.Length; i++)
                {
                    bw.Write(this.WatchIndexes[i]);
                }

                return dpp.GetByteArray(ms.ToArray(), PacketType.SET_WATCH_ELEMENTS, ResponseType.COMMANDREQUEST, targetIsBigEndian);
            }
        }
#endif

        public class SetTimeDateReq: ICommRequest
        {
            private Byte Hour;
            private Byte Minute;
            private Byte Second;
            private Byte Year;
            private Byte Month;
            private Byte Day;

            private const PacketType PACKET_TYPE = PacketType.SET_TIME_DATE;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SetTimeDateReq()
            {
            }

            public SetTimeDateReq(Byte Hour, Byte Minute, Byte Second, Byte Year, Byte Month, Byte Day)
            {
                this.Hour = Hour;
                this.Minute = Minute;
                this.Second = Second;
                this.Year = Year;
                this.Month = Month;
                this.Day = Day;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.Hour = Utils.ReverseByteOrder(this.Hour);
                    this.Minute = Utils.ReverseByteOrder(this.Minute);
                    this.Second = Utils.ReverseByteOrder(this.Second);
                    this.Year = Utils.ReverseByteOrder(this.Year);
                    this.Month = Utils.ReverseByteOrder(this.Month);
                    this.Day = Utils.ReverseByteOrder(this.Day);
                }

                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.Hour);
                bw.Write(this.Minute);
                bw.Write(this.Second);
                bw.Write(this.Year);
                bw.Write(this.Month);
                bw.Write(this.Day);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class Set4TimeDateReq : ICommRequest
        {
            private Byte Hour;
            private Byte Minute;
            private Byte Second;
            private Byte Temp;	// Introduced to ensure the structure still lies on a word boundary.
            private UInt16 Year;
            private Byte Month;
            private Byte Day;

            private const PacketType PACKET_TYPE = PacketType.SET_TIME_DATE;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private Set4TimeDateReq()
            {
            }

            public Set4TimeDateReq(Byte Hour, Byte Minute, Byte Second, UInt16 Year, Byte Month, Byte Day)
            {
                this.Hour = Hour;
                this.Minute = Minute;
                this.Second = Second;
                this.Year = Year;
                this.Month = Month;
                this.Day = Day;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.Hour = Utils.ReverseByteOrder(this.Hour);
                    this.Minute = Utils.ReverseByteOrder(this.Minute);
                    this.Second = Utils.ReverseByteOrder(this.Second);
                    this.Year = Utils.ReverseByteOrder(this.Year);
                    this.Month = Utils.ReverseByteOrder(this.Month);
                    this.Day = Utils.ReverseByteOrder(this.Day);
                }

                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.Hour);
                bw.Write(this.Minute);
                bw.Write(this.Second);
                bw.Write(this.Temp);
                bw.Write(this.Year);
                bw.Write(this.Month);
                bw.Write(this.Day);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }

        }

        public class SetFaultLogReq : ICommRequest
        {
            private Byte TargetState;
            private const PacketType PACKET_TYPE = PacketType.SET_FAULT_LOG;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SetFaultLogReq()
            {
            }

            public SetFaultLogReq(Byte TargetState)
            {
                this.TargetState = TargetState;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.TargetState = Utils.ReverseByteOrder(this.TargetState);
                }

                Byte[] payload = { this.TargetState };

                return dpp.GetByteArray(payload, PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }

        }

        public class GetFaultDataReq : ICommRequest
        {
            private UInt32 FaultIndex;
            private UInt16 NumberOfFaults;
            private const PacketType PACKET_TYPE = PacketType.GET_FAULT_DATA;
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            
            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private GetFaultDataReq()
            {
            }

            public GetFaultDataReq(UInt32 FaultIndex, UInt16 NumberOfFaults)
            {
                this.FaultIndex = FaultIndex;
                this.NumberOfFaults = NumberOfFaults;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.FaultIndex = Utils.ReverseByteOrder(this.FaultIndex);
                    this.NumberOfFaults = Utils.ReverseByteOrder(this.NumberOfFaults);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.FaultIndex);
                bw.Write(this.NumberOfFaults);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class GetFaultHistoryReq : ICommRequest
        {
            private UInt16 TaskID;
            private UInt16 FaultID;
            private const PacketType PACKET_TYPE = PacketType.GET_FAULT_HISTORY;
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private GetFaultHistoryReq()
            {
            }

            public GetFaultHistoryReq(UInt16 TaskID, UInt16 FaultID)
            {
                this.TaskID = TaskID;
                this.FaultID = FaultID;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.TaskID = Utils.ReverseByteOrder(this.TaskID);
                    this.FaultID = Utils.ReverseByteOrder(this.FaultID);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.TaskID);
                bw.Write(this.FaultID);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }

        }

        public class GetDatalogBufferReq : ICommRequest
        {
            private UInt16 DatalogIndex;

            private const PacketType PACKET_TYPE = PacketType.GET_DATALOG_BUFFER;
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            
            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private GetDatalogBufferReq()
            {
            }

            public GetDatalogBufferReq(UInt16 DatalogIndex)
            {
                this.DatalogIndex = DatalogIndex;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.DatalogIndex = Utils.ReverseByteOrder(this.DatalogIndex);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.DatalogIndex);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }

        }

        public class SetCarIDReq : ICommRequest
        {
            private String NewCarId;
            private const PacketType PACKET_TYPE = PacketType.SET_CARID;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SetCarIDReq()
            {
            }

            public SetCarIDReq(UInt16 NewCarId)
            {
                // TODO may have to perform a hex formatter... TBD
                this.NewCarId = NewCarId.ToString().PadRight(11, '\0'); ;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    // Intentionally do nothing since the only item to be converted is a string
                }

                // Convert string to byte array
                Byte[] payload = Encoding.ASCII.GetBytes(this.NewCarId);
                
                return dpp.GetByteArray(payload, PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }

        }

        public class SetStreamInfoReq//TODO : ICommRequest
        {
            private StreamInformation Information;
        }

        public class GetStreamInfoReq : ICommRequest
        {
            private UInt16 StreamNumber;
            private const PacketType PACKET_TYPE = PacketType.GET_STREAM_INFORMATION;
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private GetStreamInfoReq()
            {
            }

            public GetStreamInfoReq(UInt16 StreamNumber)
            {
                this.StreamNumber = StreamNumber;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.StreamNumber = Utils.ReverseByteOrder(this.StreamNumber);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.StreamNumber);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class ChangeEventLogReq : ICommRequest
        {
            private UInt16 NewEventLog;
            private const PacketType PACKET_TYPE = PacketType.CHANGE_EVENT_LOG;
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private ChangeEventLogReq()
            {
            }

            public ChangeEventLogReq(UInt16 NewEventLog)
            {
                this.NewEventLog = NewEventLog;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.NewEventLog = Utils.ReverseByteOrder(this.NewEventLog);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.NewEventLog);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        public class SetFaultFlagReq : ICommRequest
        {
            private UInt16 TaskID;
            private UInt16 FaultID;
            private Byte EnableFlag;
            private Byte DatalogFlag;

            private const PacketType PACKET_TYPE = PacketType.SET_FAULT_FLAG;
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the constructor below
            /// </summary>
            private SetFaultFlagReq()
            {
            }

            public SetFaultFlagReq(UInt16 TaskID, UInt16 FaultID, Byte EnableFlag, Byte DatalogFlag)
            {
                this.TaskID = TaskID;
                this.FaultID = FaultID;
                this.EnableFlag = EnableFlag;
                this.DatalogFlag = DatalogFlag;
            }

            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.TaskID = Utils.ReverseByteOrder(this.TaskID);
                    this.FaultID = Utils.ReverseByteOrder(this.FaultID);
                    this.EnableFlag = Utils.ReverseByteOrder(this.EnableFlag);
                    this.DatalogFlag = Utils.ReverseByteOrder(this.DatalogFlag);
                }
                MemoryStream ms = new MemoryStream(1024);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.TaskID);
                bw.Write(this.FaultID);
                bw.Write(this.EnableFlag);
                bw.Write(this.DatalogFlag);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }

        }

    }
}