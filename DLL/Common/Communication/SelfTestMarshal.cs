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
 *  Project:    Common
 *
 *  File name:  EventStreamMarshal.cs
 *
 *  Revision History
 *  ----------------
 *
 *  Date        Version Author       Comments
 *  03/01/2015  1.0     D.Smail      First Release.
 *
 */

#endregion --- Revision History ---

using System;
using System.Threading;
using VcuComm;

namespace Common.Communication
{
    /// <summary>
    /// This class contains methods used to generate commands and data requests to the embedded target
    /// and process the responses. All methods are related to handling self test communication.
    /// </summary>
    public class SelfTestMarshal
    {
        #region --- Constants ---

        /// <summary>
        /// The maximum amount of self test data that can be sent from the embedded target to this application
        /// on any given message.
        /// </summary>
        private const UInt16 MAX_BUFFER_SIZE = 4096;

        private const UInt16 STC_MSG_MODE_SPECIAL = 4;

        private const Byte STC_CMD_ABORT_SEQ = 4;

        private const Byte STC_CMD_OPRTR_ACK = 7;

        private const Byte STC_CMD_UPDT_LIST = 3;

        private const Byte STC_CMD_SEL_LIST = 1;

        private const Byte STC_CMD_UPDT_LOOP_CNT = 8;

        private const Byte STC_CMD_EXECUTE_LIST = 2;

        private const Byte STC_CMD_UPDT_MODE = 0;

        private const byte STC_MSG_MODE_INTERACTIVE = 5;

        //#define	STC_MSG_MODE_SPECIAL			4
        //#define	STC_MSG_MODE_INTERACTIVE		5

        //#define	STC_SPEC_MSG_ID_ENTER_ST		1
        //#define	STC_SPEC_MSG_ID_NO_ENTER		2
        //#define	STC_SPEC_MSG_ID_TEST_COMPLETE	3
        //#define	STC_SPEC_MSG_ID_TEST_ABORTED	4
        //#define	STC_SPEC_MSG_ID_EXIT_ST			5

        //#define	STC_CMD_UPDT_MODE				0
        //#define	STC_CMD_SEL_LIST				1
        //#define	STC_CMD_EXECUTE_LIST			2
        //#define	STC_CMD_UPDT_LIST				3
        //#define	STC_CMD_ABORT_SEQ				4
        //#define	STC_CMD_ABORT_SES				5
        //#define	STC_CMD_ACK_MSG					6
        //#define	STC_CMD_OPRTR_ACK				7
        //#define	STC_CMD_UPDT_LOOP_CNT			8

        //#define	STC_HIGH_LEVEL					0
        //#define	STC_SUPERVISOR					1
        //#define	STC_ENG_MODE_1					2
        //#define	STC_ENG_MODE_2					3
        //#define	STC_ENG_MODE_3					4
        //#define	STC_ENG_MODE_4					5
        //#define	STC_POWER_UP					6
        //#define	STC_PUSH_BUTTON					7
        //#define	STC_NETWORK						8

        #endregion --- Constants ---

        #region --- Member Variables ---

        /// <summary>
        /// The type of communication device used to interface with the embedded target (RS-232, TCP, etc.)
        /// </summary>
        private ICommDevice m_CommDevice;

        /// <summary>
        /// Object used to handle the standard embedded target communication protocol
        /// </summary>
        private PtuTargetCommunication m_PtuTargetCommunication = new PtuTargetCommunication();

        /// <summary>
        /// Buffer used to store data responses from the embedded target. Need to add the header
        /// size.
        /// </summary>
        private Byte[] m_RxMessage = new Byte[MAX_BUFFER_SIZE];

        #endregion --- Member Variables ---

        #region --- Constructors ---

        /// <summary>
        /// Constructor that must be used to create an object of this class.
        /// </summary>
        /// <param name="device">the type of communication device (RS-232, TCP, etc.)</param>
        public SelfTestMarshal(ICommDevice device)
        {
            m_CommDevice = device;
        }

        /// <summary>
        /// The default constructor is made private to force the use of the multi-argument constructor
        /// when creating an instance of this class.
        /// </summary>
        private SelfTestMarshal()
        { }

        #endregion --- Constructors ---

        #region --- Methods ---

        #region --- Public Methods ---

        /// <summary>
        ///
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public CommunicationError GetSelfTestSpecialMessage(ref Int16 Result, ref Int16 Reason)
        {
            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_SELF_TEST_PACKET, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            // Extract all of the information from the received data
            Byte valid = m_RxMessage[8];
            Byte messageMode = m_RxMessage[9];

            if (m_CommDevice.IsTargetBigEndian())
            {
                valid = Utils.ReverseByteOrder(valid);
                messageMode = Utils.ReverseByteOrder(messageMode);
            }

            if (valid != 1)
            {
                return CommunicationError.UnknownError;
            }

            if (messageMode != STC_MSG_MODE_SPECIAL)
            {
                return CommunicationError.BadResponse;
            }

            Result = BitConverter.ToInt16(m_RxMessage, 12);
            Reason = BitConverter.ToInt16(m_RxMessage, 15);

            if (m_CommDevice.IsTargetBigEndian())
            {
                Result = Utils.ReverseByteOrder(Result);
                Reason = Utils.ReverseByteOrder(Reason);
            }

            return CommunicationError.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public CommunicationError StartSelfTestTask(ref Int16 Result, ref Int16 Reason)
        {
            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, ProtocolPTU.PacketType.START_SELF_TEST_TASK);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            // original code has a hard-coded while--loop delay
            Thread.Sleep(100);

            commError = GetSelfTestSpecialMessage(ref Result, ref Reason);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public CommunicationError ExitSelfTestTask(ref Int16 Result, ref Int16 Reason)
        {
            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, ProtocolPTU.PacketType.EXIT_SELF_TEST_TASK);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            commError = GetSelfTestSpecialMessage(ref Result, ref Reason);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public CommunicationError AbortSTSequence()
        {
            ProtocolPTU.SelfTestCommand request = new ProtocolPTU.SelfTestCommand(STC_CMD_ABORT_SEQ, 0, 0);

            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public CommunicationError SendOperatorAcknowledge()
        {
            ProtocolPTU.SelfTestCommand request = new ProtocolPTU.SelfTestCommand(STC_CMD_OPRTR_ACK, 0, 0);

            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="NumberOfTests"></param>
        /// <param name="TestList"></param>
        /// <returns></returns>
        public CommunicationError UpdateSTTestList(Int16 NumberOfTests, Int16[] TestList)
        {
            ProtocolPTU.SelfTestUpdateListReq request = new ProtocolPTU.SelfTestUpdateListReq(STC_CMD_UPDT_LIST, NumberOfTests, TestList);

            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="TestID"></param>
        /// <returns></returns>
        public CommunicationError RunPredefinedSTTests(Int16 TestID)
        {
            ProtocolPTU.SelfTestCommand request = new ProtocolPTU.SelfTestCommand(STC_CMD_SEL_LIST, 0, (UInt16)TestID);

            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="TestID"></param>
        /// <returns></returns>
        public CommunicationError UpdateSTLoopCount(Int16 TestID)
        {
            ProtocolPTU.SelfTestCommand request = new ProtocolPTU.SelfTestCommand(STC_CMD_UPDT_LOOP_CNT, 0, (UInt16)TestID);

            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="TruckInformation"></param>
        /// <returns></returns>
        public CommunicationError ExecuteSTTestList(Int16 TruckInformation)
        {
            ProtocolPTU.SelfTestCommand request = new ProtocolPTU.SelfTestCommand(STC_CMD_EXECUTE_LIST, (Byte)TruckInformation, 0);

            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="NewMode"></param>
        /// <returns></returns>
        public CommunicationError UpdateSTMode(Int16 NewMode)
        {
            ProtocolPTU.SelfTestCommand request = new ProtocolPTU.SelfTestCommand(STC_CMD_UPDT_MODE, 0, (UInt16)NewMode);

            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        public CommunicationError GetSelfTestResult(ref Int16 ValidResult, ref Int16 MessageMode, ref Int16 TestID,
                                                     ref Int16 TestCase, ref Int16 TestResult, ref Int16 SetInfo,
                                                     ref Int16 NumOfVars, InteractiveResults_t[] InteractiveResults)
        {
            const Byte MAXSTVARIABLES = 16;

            // Initiate transaction with embedded target
            CommunicationError commError = m_PtuTargetCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_SELF_TEST_PACKET, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            // Extract all of the information from the received data
            Byte validResult = m_RxMessage[8];
            Byte messageMode = m_RxMessage[9];
            UInt16 setInfo = BitConverter.ToUInt16(m_RxMessage, 10);
            UInt16 testID = BitConverter.ToUInt16(m_RxMessage, 12);
            Byte testCase = m_RxMessage[15];
            Byte numOfVars = m_RxMessage[16];
            Byte testResult = m_RxMessage[17];


            if (m_CommDevice.IsTargetBigEndian())
            {
                validResult = Utils.ReverseByteOrder(validResult);
                messageMode = Utils.ReverseByteOrder(messageMode);
                setInfo = Utils.ReverseByteOrder(setInfo);
                testID = Utils.ReverseByteOrder(testID);
                testCase = Utils.ReverseByteOrder(testCase);
                numOfVars = Utils.ReverseByteOrder(numOfVars);
                testResult = Utils.ReverseByteOrder(testResult);
            }

            if (numOfVars > MAXSTVARIABLES)
            {
                numOfVars = MAXSTVARIABLES;
            }

            if ((messageMode == STC_MSG_MODE_INTERACTIVE) && (validResult == 1))
            {

                UInt16 valueOffset = 28;
                UInt16 tagOffset = 32;
                UInt16 typeOffSet = 33;
                for (Byte index = 0; index < numOfVars; index++)
                {
                    Byte varType = m_RxMessage[typeOffSet];
                    Byte tag = m_RxMessage[tagOffset];
                    if (m_CommDevice.IsTargetBigEndian())
                    {
                        varType = Utils.ReverseByteOrder(varType);
                        tag = Utils.ReverseByteOrder(tag);
                    }

                    InteractiveResults[index].Tag = tag;

                    // All data written to "value" is 32 bits. Therefore, all values
                    // can be extracted as a 32 bit number
                    switch ((ProtocolPTU.VariableType)varType)
                    {
                        case ProtocolPTU.VariableType.UINT_8_TYPE:
                        case ProtocolPTU.VariableType.UINT_16_TYPE:
                        case ProtocolPTU.VariableType.UINT_32_TYPE:
                            UInt32 u32 = BitConverter.ToUInt32(m_RxMessage, valueOffset);
                            if (m_CommDevice.IsTargetBigEndian())
                            {
                                u32 = Utils.ReverseByteOrder(u32);
                            }
                            InteractiveResults[index].Value = (double)u32;;
                            break;

                        case ProtocolPTU.VariableType.INT_8_TYPE:
                        case ProtocolPTU.VariableType.INT_16_TYPE:
                        case ProtocolPTU.VariableType.INT_32_TYPE:
                            Int32 i32 = BitConverter.ToInt32(m_RxMessage, valueOffset);
                            if (m_CommDevice.IsTargetBigEndian())
                            {
                                i32 = Utils.ReverseByteOrder(i32);
                            }
                            InteractiveResults[index].Value = (double)i32;
                            break;

                        default:
                            InteractiveResults[index].Value = 0;
                            break;

                    }

                    typeOffSet += 6;
                    valueOffset += 6;
                    tagOffset += 6;
                }
            }
            
            return CommunicationError.Success;
        }

#if DAS
                struct st_msg_var_str
                {
	                UINT32	Value;
	                char	Tag;
	                char	Type;
                };

extern "C" INT16 WINAPI GetSelfTestResult(INT16	*ValidResult,
	INT16 *MessageMode,
	INT16 *TestID,
	INT16 *TestCase,
	INT16 *TestResult,
	INT16 *SetInfo,
	INT16 *NumOfVars,
	InteractiveResults_t  *InteractiveResults)
{
	INT16					ReturnValue;
	INT16					Index;
	Header_t				Request;
	GetSelfTestPacketRes_t	Response;
	INT16					temp;
	_TCHAR					tempchar;
	INT16 					tempint;
	INT32					templong;

	Request.PacketType			= GET_SELF_TEST_PACKET;
	Request.PacketLength		= sizeof(Header_t);

	ReturnValue = Transaction(&Request, (Header_t *)&Response);
	if (ReturnValue == NOERROR)
	{
		*ValidResult	= Response.Valid;
		*MessageMode	= Response.MessageMode;
		*TestID			= MAPINT(Response.TestID);
		*SetInfo		= MAPINT(Response.SetInformation);
		*TestCase		= Response.ResultsData.pcptu_result1.test_case;
		*TestResult		= Response.ResultsData.pcptu_result1.test_result;
		*NumOfVars		= Response.ResultsData.pcptu_result1.num_of_vars;

		if (*NumOfVars > MAXSTVARIABLES)
			*NumOfVars = MAXSTVARIABLES;

		if ((*MessageMode == STC_MSG_MODE_INTERACTIVE) &&
			(*ValidResult == 1))
		{
			for (Index = 0; Index < *NumOfVars; Index++)
			{
				if (Index == 2 &&
					(Response.VariableMsg[Index].Value < 0 ||
					Response.VariableMsg[Index].Value > 60000))
				{
					temp = 0;
				}	

				switch (Response.VariableMsg[Index].Type)
				{
				case UINT_8_TYPE:
					InteractiveResults[Index].Value	=   /* Added Maplong */
						(double)MAPLONG(Response.VariableMsg[Index].Value);
					break;

				case UINT_16_TYPE:
					InteractiveResults[Index].Value	=   /* Changed Mapint to maplong*/
						(double)MAPLONG((UINT16)Response.VariableMsg[Index].Value);
					break;

				case UINT_32_TYPE:						
					InteractiveResults[Index].Value	= 
						(double)MAPLONG(Response.VariableMsg[Index].Value);
					break;

				case INT_8_TYPE:
					tempchar = (_TCHAR)Response.VariableMsg[Index].Value;
					InteractiveResults[Index].Value	= (double)MAPLONG(tempchar); /*Added MapLong*/
					break;

				case INT_16_TYPE:
					tempint = (INT16)Response.VariableMsg[Index].Value;  /*Changed MapInt to MapLong */
					InteractiveResults[Index].Value	= (double)MAPLONG(tempint);
					break;

				case INT_32_TYPE:
					templong = MAPLONG(Response.VariableMsg[Index].Value);
					InteractiveResults[Index].Value	= (double)templong;
					break;

				default:
					InteractiveResults[Index].Value	= 0;
					break;
				}

				InteractiveResults[Index].Tag =
					Response.VariableMsg[Index].Tag;
			}
		}
	}

	return ReturnValue;
}
#endif



        #endregion --- Public Methods ---


        #endregion --- Methods ---
    }
}