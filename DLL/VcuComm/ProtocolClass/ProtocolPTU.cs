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
 *  File name:  ProtocolPTU.cs
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

namespace VcuComm
{
    public partial class ProtocolPTU
    {
        /// <summary>
        /// This is the response that is returned from the embedded target PTU when it acknowledges
        /// a command and no data from the embedded target is to be returned.
        /// </summary>
        public static readonly byte PTU_ACK = (byte)0x04;

        /// <summary>
        /// This is the byte that is returned from the embedded target PTU when the machine that
        /// the embedded target resides on is Big Endian.
        /// </summary>
        public static readonly byte TARGET_BIG_ENDIAN_SOM = (byte)'S';

        /// <summary>
        /// This is the byte that is returned from the embedded target PTU when the machine that
        /// the embedded target resides on is Little Endian.
        /// </summary>
        public static readonly byte THE_SOM = (byte)':';

        /// <summary>
        /// The size of every PTU header to and from the embedded target PTU. The header consists
        /// of 4 16 bit words of which the checksum is not used on either the PTU or the embedded
        /// target.
        /// </summary>
        private const UInt16 HEADER_SIZE_BYTES = 8;
        /// <summary>
        /// These errors are logged whenever any error is detected when a transaction occurs
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
            ConnectionErrorAsync,
            TransmitMessage,
            TransmitMessageAsync,
            ReceiveMessage,
            ReceiveMessageAsync,
            AckNotReceieved,
            RxTimeout,
            MessageEcho,
            ExcessiveBytesReceived,
            InvalidSOM,
            ServerClosedSocketUnexpectedly,
            Close,
        }

        public struct GetEmbeddedInfoRes
        {
            public String CarID;
            public UInt32 ConfigurationMask;
            public String IdentifierString;
            public String SoftwareVersion;
            public String SubSystemName;
        }

        public struct GetFaultDataRes
        {
            public Byte[] Buffer;
            public UInt16 BufferSize;
        }


        public struct SelfTestCommandReq
        {
            public Byte CommandID;
            public UInt16 Data;
            public DataPacketProlog Header;
            public UInt16[] TestSet;
            public Byte TruckInformation;
        }

        public struct StreamInformation
        {
            public UInt16 NumberOfSamples;
            public UInt16 NumberOfVariables;
            public UInt16 SampleRate;
            public StreamVariable[] StreamVariableInfo;
        }

        public struct StreamVariable
        {
            public UInt16 Variable;
            public UInt16 VariableType;
        }

        public class DataPacketProlog
        {
            private UInt16 checksum;
            private UInt16 packetLength;
            private UInt16 packetType;
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
        /// /////////////////////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////////////////////
    }
}