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
 *  File name:  ProtocolPTURequests.cs
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
using System.IO;
using System.Text;

namespace VcuComm
{
    /// <summary>
    /// Every request from the PTU to the embedded target that has a payload must implement this
    /// interface.
    /// </summary>
    public interface ICommRequest
    {
        /// <summary>
        /// Method that formats the message going to the embedded PTU target. The format of the message
        /// is specific to the type of request made.
        /// </summary>
        /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
        /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
        Byte[] GetByteArray(Boolean targetIsBigEndian);
    }

    public partial class ProtocolPTU
    {
        /// <summary>
        /// This is the maximum size of the outgoing message from PTU to the embedded PTU target. This
        /// size includes the packet header. This is used to allocate memory for a payload of indeterminate
        /// size or when stream writing is required to order the payload properly.
        /// </summary>
        private const UInt16 MAX_TX_STREAM_SIZE = 1024;

        /// <summary>
        /// Used to format and create a byte array used to change the event log in the embedded PTU target PTU
        /// </summary>
        public class ChangeEventLogReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.CHANGE_EVENT_LOG;

            /// <summary>
            /// Informs the embedded PTU target that this request expects some data response
            /// in return
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// Id of the event log to change to
            /// </summary>
            private Int16 NewEventLog;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="NewEventLog">Id of the event log to change to</param>
            public ChangeEventLogReq(Int16 NewEventLog)
            {
                this.NewEventLog = NewEventLog;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private ChangeEventLogReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.NewEventLog = Utils.ReverseByteOrder(this.NewEventLog);
                }
                // Even though "ms" is allocated MAX_TX_STREAM_SIZE, the ToArray() returns the size
                // of the array that was populated with data
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.NewEventLog);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// Used to format and create a byte array used to get the variable index of the desired chart channel
        /// </summary>
        public class GetChartIndexReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.GET_CHART_INDEX;

            /// <summary>
            /// Informs the embedded PTU target that this request expects some data response
            /// in return
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// The desired chart index to retrieve; if there 8 chart channels then this
            /// value can range from 0 to 7
            /// </summary>
            private Byte ChartIndex;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="ChartIndex">The desired chart index to retrieve</param>
            public GetChartIndexReq(Byte ChartIndex)
            {
                this.ChartIndex = ChartIndex;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private GetChartIndexReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
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

        /// <summary>
        /// TODO
        /// </summary>
        public class GetDatalogBufferReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.GET_DATALOG_BUFFER;

            /// <summary>
            /// Informs the embedded PTU target that this request expects some data response
            /// in return
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private UInt16 DatalogIndex;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="DatalogIndex"></param>
            public GetDatalogBufferReq(UInt16 DatalogIndex)
            {
                this.DatalogIndex = DatalogIndex;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private GetDatalogBufferReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.DatalogIndex = Utils.ReverseByteOrder(this.DatalogIndex);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.DatalogIndex);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class GetFaultDataReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.GET_FAULT_DATA;

            /// <summary>
            /// Informs the embedded PTU target that this request expects some data response
            /// in return
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private UInt32 FaultIndex;

            /// <summary>
            /// TODO
            /// </summary>
            private UInt16 NumberOfFaults;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="FaultIndex"></param>
            /// <param name="NumberOfFaults"></param>
            public GetFaultDataReq(UInt32 FaultIndex, UInt16 NumberOfFaults)
            {
                this.FaultIndex = FaultIndex;
                this.NumberOfFaults = NumberOfFaults;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private GetFaultDataReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.FaultIndex = Utils.ReverseByteOrder(this.FaultIndex);
                    this.NumberOfFaults = Utils.ReverseByteOrder(this.NumberOfFaults);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.FaultIndex);
                bw.Write(this.NumberOfFaults);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class GetFaultHistoryReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.GET_FAULT_HISTORY;

            /// <summary>
            /// Informs the embedded PTU target that this request expects some data response
            /// in return
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private UInt16 FaultID;
            
            /// <summary>
            /// TODO
            /// </summary>
            private UInt16 TaskID;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="TaskID"></param>
            /// <param name="FaultID"></param>
            public GetFaultHistoryReq(UInt16 TaskID, UInt16 FaultID)
            {
                this.TaskID = TaskID;
                this.FaultID = FaultID;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private GetFaultHistoryReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.TaskID = Utils.ReverseByteOrder(this.TaskID);
                    this.FaultID = Utils.ReverseByteOrder(this.FaultID);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.TaskID);
                bw.Write(this.FaultID);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class GetStreamInfoReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.GET_STREAM_INFORMATION;

            /// <summary>
            /// Informs the embedded PTU target that this request expects some data response
            /// in return
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Int16 StreamNumber;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="StreamNumber"></param>
            public GetStreamInfoReq(Int16 StreamNumber)
            {
                this.StreamNumber = StreamNumber;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private GetStreamInfoReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.StreamNumber = Utils.ReverseByteOrder(this.StreamNumber);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.StreamNumber);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class GetVariableInfoReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.GET_VARIABLE_INFORMATION;

            /// <summary>
            /// Informs the embedded PTU target that this request expects some data response
            /// in return
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.DATAREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private UInt16 DictionaryIndex;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="DictionaryIndex"></param>
            public GetVariableInfoReq(UInt16 DictionaryIndex)
            {
                this.DictionaryIndex = DictionaryIndex;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private GetVariableInfoReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.DictionaryIndex = Utils.ReverseByteOrder(this.DictionaryIndex);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.DictionaryIndex);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class SendVariableReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SEND_VARIABLE_VALUE;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Int16 DictionaryIndex;
            /// <summary>
            /// TODO
            /// </summary>
            private UInt32 NewValue;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="DictionaryIndex"></param>
            /// <param name="NewValue"></param>
            public SendVariableReq(Int16 DictionaryIndex, UInt32 NewValue)
            {
                this.DictionaryIndex = DictionaryIndex;
                this.NewValue = NewValue;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SendVariableReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.DictionaryIndex = (Int16)Utils.ReverseByteOrder((UInt16)this.DictionaryIndex);
                    this.NewValue = Utils.ReverseByteOrder(this.NewValue);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.DictionaryIndex);
                bw.Write(this.NewValue);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class SetCarIDReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_CARID;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private String NewCarId;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="NewCarId"></param>
            public SetCarIDReq(UInt16 NewCarId)
            {
                // TODO may have to perform a hex formatter... TBD
                this.NewCarId = NewCarId.ToString().PadRight(11, '\0'); ;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SetCarIDReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
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

        /// <summary>
        /// TODO
        /// </summary>
        public class SetChartIndexReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_CHART_INDEX;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Byte ChartIndex;

            /// <summary>
            /// TODO
            /// </summary>
            private Int16 VariableIndex;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="ChartIndex"></param>
            /// <param name="VariableIndex"></param>
            public SetChartIndexReq(Int16 ChartIndex, Int16 VariableIndex)
            {
                this.ChartIndex = (Byte)ChartIndex;
                this.VariableIndex = VariableIndex;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SetChartIndexReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.ChartIndex = Utils.ReverseByteOrder(this.ChartIndex);
                    this.VariableIndex = Utils.ReverseByteOrder(this.VariableIndex);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.VariableIndex);
                bw.Write(this.ChartIndex);

                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class SetChartModeReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_CHART_MODE;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Byte TargetChartMode;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="TargetChartMode"></param>
            public SetChartModeReq(byte TargetChartMode)
            {
                this.TargetChartMode = TargetChartMode;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SetChartModeReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
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

        /// <summary>
        /// TODO
        /// </summary>
        public class SetChartScaleReq : ICommRequest
        {
            /// <summary>
            /// TODO
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_CHART_SCALE;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Int16 DictionaryIndex;
            /// <summary>
            /// TODO
            /// </summary>
            private Int32 MaxScale;
            /// <summary>
            /// TODO
            /// </summary>
            private Int32 MinScale;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="DictionaryIndex"></param>
            /// <param name="MaxScale"></param>
            /// <param name="MinScale"></param>
            public SetChartScaleReq(Int16 DictionaryIndex, Int32 MaxScale, Int32 MinScale)
            {
                this.DictionaryIndex = DictionaryIndex;
                this.MaxScale = MaxScale;
                this.MinScale = MinScale;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SetChartScaleReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.DictionaryIndex = Utils.ReverseByteOrder(this.DictionaryIndex);
                    this.MaxScale = Utils.ReverseByteOrder(this.MaxScale);
                    this.MinScale = Utils.ReverseByteOrder(this.MinScale);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.DictionaryIndex);
                bw.Write(this.MaxScale);
                bw.Write(this.MinScale);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class SetFaultFlagReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_FAULT_FLAG;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Byte DatalogFlag;
            /// <summary>
            /// TODO
            /// </summary>
            private Byte EnableFlag;
            /// <summary>
            /// TODO
            /// </summary>
            private Int16 FaultID;
            /// <summary>
            /// TODO
            /// </summary>
            private Int16 TaskID;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="TaskID"></param>
            /// <param name="FaultID"></param>
            /// <param name="EnableFlag"></param>
            /// <param name="DatalogFlag"></param>
            public SetFaultFlagReq(Int16 TaskID, Int16 FaultID, Int16 EnableFlag, Int16 DatalogFlag)
            {
                this.TaskID = TaskID;
                this.FaultID = FaultID;
                this.EnableFlag = (Byte)EnableFlag;
                this.DatalogFlag = (Byte)DatalogFlag;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SetFaultFlagReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
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
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.TaskID);
                bw.Write(this.FaultID);
                bw.Write(this.EnableFlag);
                bw.Write(this.DatalogFlag);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class EnableFaultLoggingReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_FAULT_LOG;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Byte TargetState;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="TargetState"></param>
            public EnableFaultLoggingReq(Byte TargetState)
            {
                this.TargetState = TargetState;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private EnableFaultLoggingReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
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

        /// <summary>
        /// TODO
        /// </summary>
        public class SetStreamInfoReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_STREAM_INFORMATION;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private StreamInformation streamInformation;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="NumberOfVariables"></param>
            /// <param name="SampleRate"></param>
            /// <param name="VariableIndex"></param>
            public SetStreamInfoReq(Int16 NumberOfVariables, Int16 SampleRate, Int16[] VariableIndex)
            {
                streamInformation.NumberOfVariables = (UInt16)NumberOfVariables;
                streamInformation.SampleRate = (UInt16)SampleRate;
                streamInformation.NumberOfSamples = 0;

                streamInformation.StreamVariableInfo = new StreamVariable[NumberOfVariables];

                for (Int16 i = 0; i < NumberOfVariables; i++)
                {
                    streamInformation.StreamVariableInfo[i].Variable = (UInt16)VariableIndex[i];
                    streamInformation.StreamVariableInfo[i].VariableType = 0;
                }
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SetStreamInfoReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                UInt16 numVars = streamInformation.NumberOfVariables;

                if (targetIsBigEndian)
                {
                    streamInformation.NumberOfVariables = Utils.ReverseByteOrder(streamInformation.NumberOfVariables);
                    streamInformation.NumberOfSamples = Utils.ReverseByteOrder(streamInformation.NumberOfSamples);
                    streamInformation.SampleRate = Utils.ReverseByteOrder(streamInformation.SampleRate);

                    for (UInt16 i = 0; i < numVars; i++)
                    {
                        streamInformation.StreamVariableInfo[i].Variable = Utils.ReverseByteOrder(streamInformation.StreamVariableInfo[i].Variable);
                        streamInformation.StreamVariableInfo[i].VariableType = Utils.ReverseByteOrder(streamInformation.StreamVariableInfo[i].VariableType);
                    }
                }

                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(streamInformation.NumberOfVariables);
                bw.Write(streamInformation.NumberOfSamples);
                bw.Write(streamInformation.SampleRate);
                for (UInt16 i = 0; i < numVars; i++)
                {
                    bw.Write(streamInformation.StreamVariableInfo[i].Variable);
                    bw.Write(streamInformation.StreamVariableInfo[i].VariableType);
                }
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class SetTimeDateReq : ICommRequest
        {
            /// <summary>
            /// TODO
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_TIME_DATE;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Byte Day;

            /// <summary>
            /// TODO
            /// </summary>
            private Boolean fourDigitYear;
            /// <summary>
            /// TODO
            /// </summary>
            private Byte Hour;
            /// <summary>
            /// TODO
            /// </summary>
            private Byte Minute;
            /// <summary>
            /// TODO
            /// </summary>
            private Byte Month;
            /// <summary>
            /// TODO
            /// </summary>
            private Byte Second;
            /// <summary>
            /// TODO
            /// </summary>
            private UInt16 Year;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="Hour"></param>
            /// <param name="Minute"></param>
            /// <param name="Second"></param>
            /// <param name="Year"></param>
            /// <param name="Month"></param>
            /// <param name="Day"></param>
            public SetTimeDateReq(Boolean fourDigitYear, Byte Hour, Byte Minute, Byte Second, UInt16 Year, Byte Month, Byte Day)
            {
                this.fourDigitYear = fourDigitYear;
                this.Hour = Hour;
                this.Minute = Minute;
                this.Second = Second;
                this.Year = Year;
                this.Month = Month;
                this.Day = Day;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SetTimeDateReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
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

                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.Hour);
                bw.Write(this.Minute);
                bw.Write(this.Second);
                if (this.fourDigitYear)
                {
                    // Used to fill a boundary
                    bw.Write(0x00);
                    bw.Write(this.Year);
                }
                else
                {
                    Byte bYear = (Byte)this.Year;
                    bw.Write(bYear);
                }
                bw.Write(this.Month);
                bw.Write(this.Day);
                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }
        /// <summary>
        /// TODO
        /// </summary>
        public class SetWatchElementReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_WATCH_ELEMENT;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private UInt16 DictionaryIndex;

            /// <summary>
            /// TODO
            /// </summary>
            private UInt16 ElementIndex;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="ElementIndex"></param>
            /// <param name="DictionaryIndex"></param>
            public SetWatchElementReq(UInt16 ElementIndex, UInt16 DictionaryIndex)
            {
                this.ElementIndex = ElementIndex;
                this.DictionaryIndex = DictionaryIndex;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SetWatchElementReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.ElementIndex = Utils.ReverseByteOrder(this.ElementIndex);
                    this.DictionaryIndex = Utils.ReverseByteOrder(this.DictionaryIndex);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.ElementIndex);
                bw.Write(this.DictionaryIndex);

                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class SetWatchElementsReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SET_WATCH_ELEMENTS;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Int16[] WatchElement;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="WatchElement"></param>
            public SetWatchElementsReq(Int16[] WatchElement)
            {
                this.WatchElement = new Int16[WatchElement.Length];
                for (UInt16 i = 0; i < WatchElement.Length; i++)
                {
                    this.WatchElement[i] = WatchElement[i];
                }
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SetWatchElementsReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
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
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                for (UInt16 i = 0; i < this.WatchElement.Length; i++)
                {
                    bw.Write(this.WatchElement[i]);
                }

                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public class UpdateWatchElementsReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.UPDATE_WATCH_ELEMENTS;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Int16 ForceFullUpdate;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="ForceFullUpdate"></param>
            public UpdateWatchElementsReq(Int16 ForceFullUpdate)
            {
                this.ForceFullUpdate = ForceFullUpdate;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private UpdateWatchElementsReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                Byte[] payload = { this.ForceFullUpdate != 0 ? (Byte)1 : (Byte)0 };

                return dpp.GetByteArray(payload, PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }
        }



#if DAS
        public struct SelfTestCommandReq
        {
            public Byte CommandID;
            public UInt16 Data;
            public DataPacketProlog Header;
            public UInt16[] TestSet;
            public Byte TruckInformation;
        }
#endif

        /// <summary>
        /// TODO
        /// </summary>
        public class SelfTestCommand : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SELF_TEST_COMMAND;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Byte CommandId;

            /// <summary>
            /// TODO
            /// </summary>
            private Byte TruckId;

            /// <summary>
            /// TODO
            /// </summary>
            private UInt16 Data;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="ElementIndex"></param>
            /// <param name="DictionaryIndex"></param>
            public SelfTestCommand(Byte CommandId, Byte TruckId, UInt16 Data)
            {
                this.CommandId = CommandId;
                this.TruckId = TruckId;
                this.Data = Data;
            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SelfTestCommand()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.CommandId = Utils.ReverseByteOrder(this.CommandId);
                    this.Data = Utils.ReverseByteOrder(this.Data);
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(this.CommandId);
                bw.Write(this.TruckId);
                bw.Write(this.Data);

                return dpp.GetByteArray(ms.ToArray(), PACKET_TYPE, RESPONSE_TYPE, targetIsBigEndian);
            }

        }



        /// <summary>
        /// TODO
        /// </summary>
        public class SelfTestUpdateListReq : ICommRequest
        {
            /// <summary>
            /// Sets the packet type used to identify the message contents
            /// </summary>
            private const PacketType PACKET_TYPE = PacketType.SELF_TEST_COMMAND;

            /// <summary>
            /// Informs the embedded PTU target that this request is a command only and expects
            /// no data response in return; only an acknowledge that the message was received
            /// </summary>
            private const ResponseType RESPONSE_TYPE = ResponseType.COMMANDREQUEST;

            /// <summary>
            /// TODO
            /// </summary>
            private Byte CommandId;

            /// <summary>
            /// TODO
            /// </summary>
            private Int16 NumberOfTests;

            /// <summary>
            /// TODO
            /// </summary>
            private Int16[] TestList;

            /// <summary>
            /// Public constructor that is the only one permitted to create this object
            /// </summary>
            /// <param name="ElementIndex"></param>
            /// <param name="DictionaryIndex"></param>
            public SelfTestUpdateListReq(Byte CommandId, Int16 NumberOfTests, Int16[] TestList)
            {
                this.CommandId = CommandId;
                this.NumberOfTests = NumberOfTests;
                this.TestList = new Int16[TestList.Length];

                for (UInt16 i = 0; i < TestList.Length; i++)
                {
                    this.TestList[i] = TestList[i]; 
                }

            }

            /// <summary>
            /// Private 0 argument constructor that forces the instantiation of this class
            /// to use the public constructor
            /// </summary>
            private SelfTestUpdateListReq()
            {
            }

            /// <summary>
            /// Method that formats the message going to the embedded PTU target. The format of the message
            /// is specific to the type of request made.
            /// </summary>
            /// <param name="targetIsBigEndian">true if the embedded PTU target is a Big Endian machine; false otherwise</param>
            /// <returns>ordered byte array that is to be sent to the embedded PTU target</returns>
            public Byte[] GetByteArray(Boolean targetIsBigEndian)
            {
                DataPacketProlog dpp = new DataPacketProlog();

                if (targetIsBigEndian)
                {
                    this.CommandId = Utils.ReverseByteOrder(this.CommandId);
                    this.NumberOfTests = Utils.ReverseByteOrder(this.NumberOfTests);

                    for (UInt16 i = 0; i < this.TestList.Length; i++)
                    {
                        this.TestList[i] = Utils.ReverseByteOrder(this.TestList[i]);
                    }
                }
                MemoryStream ms = new MemoryStream(MAX_TX_STREAM_SIZE);
                BinaryWriter bw = new BinaryWriter(ms);

                bw.Write(this.CommandId);
                bw.Write(0);
                bw.Write(NumberOfTests);

                for (UInt16 i = 0; i < this.TestList.Length; i++)
                {
                    bw.Write(this.TestList[i]);
                }

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
    }
}