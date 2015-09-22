/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:
*               Windows PTU
*
*   File Name:
*               selftest.cpp
*
*   SubSystem:
*               PTUDLL
*
*   Procedures:
*				GetSelfTestSpecialMessage
*				StartSelfTestTask
*				ExitSelfTestTask
*				AbortSTSequence
*				SendOperatorAcknowledge
*				UpdateSTTestList
*				RunPredefinedSTTests
*				UpdateSTLoopCount
*				ExecuteSTTestList
*				UpdateSTMode
*				GetSelfTestResult
*
*   EPROM Drawing:
*
* .b
*
*   History:        ??/??/??    Creation of Version 1.0         jsl
* 	Revised: 		05/09/96	Dev Pradhan @ Adtranz
*					- Added SMRS's.
*					11/06/97	Dev Pradhan @ Adtranz
*					- removed SixteenTwosBitComplement().
*					11/11/97	Dev Pradhan @ Adtranz
*					- Updated GetSelfTestResult().
*					07/01/99		Jason O'Connor @ Adtranz
*					- Ported to Visual C++ 5.0.
*						- Added extern "C", WINAPI to function declarations 
*						  for export.
*			  	   	    - Added include of stdafx.h for VC++ compiling.
*						- Removed Vbapi.h.
*					    - Replaced char with _T macros for UNICODE/ANSI compatability.
*			  
*****************************************************************************/

/* Includes */
#include	"stdafx.h"
#include	<windows.h>
#include	"selftest.h"
#include	"comm.h"


/**************************************************************************
*
* .b
* Procedure Name : GetSelfTestSpecialMessage
*
* Abstract :
*
*	Sends a Self Test Message packet to the Logic (EPTUI).
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SELFTESTTIMEOUT
*				NOERROR
*				FALSE
*				TIMEOUT
*				TRUE
*				UNKNOWNERROR
*				STC_MSG_MODE_SPECIAL
*				BADRESPONSE
*
*	Procedure Parameters :
*				INT16		 	*Result
*				INT16			*Reason
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of GET_SELF_TEST_PACKEY type and sends it to the
*	Logic. Continually checks for a result from the Logic until it
*	receives a result or times out. Also checks the response to
*	see if it is valid. If it is not valid the function returns
*	and error code. If it is valid the function returns the Test ID
*	and the Test Results.

* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created.
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI GetSelfTestSpecialMessage(INT16 *Result, INT16 *Reason)
{
	INT16					ReturnValue;
	INT16					Counter;
	Header_t				Request;
	GetSelfTestPacketRes_t	Response;
	INT16 Count=0;

	Count = sizeof(GetSelfTestPacketRes_t);

	Request.PacketType		= GET_SELF_TEST_PACKET;
	Request.PacketLength	= sizeof(Header_t);

	Counter = 0;

	do
	{
		Request.PacketType		= GET_SELF_TEST_PACKET;
		Request.PacketLength	= sizeof(Header_t);
		Request.CheckSum=0;
		Request.ResponseType=0;

		ReturnValue = Transaction(&Request, (Header_t *)&Response);

	}			
	while (	(Counter++ < SELFTESTTIMEOUT) 	&&
		(ReturnValue == NOERROR) 		&&
		(Response.Valid == FALSE) );


	if (ReturnValue != NOERROR)
		return ReturnValue;

	if (Counter > SELFTESTTIMEOUT)
		return TIMEOUT;

	if (Response.Valid != TRUE)
		return UNKNOWNERROR;

	if (Response.MessageMode != STC_MSG_MODE_SPECIAL)
		return BADRESPONSE;

	*Result	= MAPINT(Response.TestID);
	*Reason	= Response.ResultsData.pcptu_result1.test_case;

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : StartSelfTestTask
*
* Abstract :
*
*	Sends a Start Self Test Task message to the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				START_SELF_TEST_TASK
*				NULL
*				NOERROR
*
*	Procedure Parameters :
*				INT16		*Result
*				INT16		*Reason
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of START_SELF_TEST_TASK type and sends it to the
*	Logic. Then calls GetSelfTestSpecialMessage() for the response
*	to this command.
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI StartSelfTestTask(INT16 *Result, INT16 *Reason)
{
	INT16		ReturnValue;
	Header_t	Request;


	Request.PacketType		= START_SELF_TEST_TASK;
	Request.PacketLength	= sizeof(Header_t);

#if SIM
	*Result = 1;
	*Reason = 1;

	return 0;
#else

	ReturnValue = Transaction(&Request, NULL);
	if (ReturnValue == NOERROR)
	{
		for (ReturnValue = 0; ReturnValue < 0x0fff; ReturnValue++)
			ReturnValue = ReturnValue;

		ReturnValue = GetSelfTestSpecialMessage(Result, Reason);
	}

	return ReturnValue;
#endif
}

/**************************************************************************
*
* .b
* Procedure Name : ExitSelfTestTask
*
* Abstract :
*
*	Sends a Exit Self Test Task message to the Logic
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				EXIT_SELF_TEST_TASK
*				NULL
*				NOERROR
*
*	Procedure Parameters :
*				INT16  	*Result
*				INT16  	*Reason
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of EXIT_SELF_TEST_TASK type and sends it to the
*	Logic. Then calls GetSelfTestSpecialMessage() for the response
*	to this command.
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
**************************************************************************/
extern "C" INT16 WINAPI ExitSelfTestTask(INT16  *Result, INT16  *Reason)
{
	INT16		ReturnValue;
	Header_t	Request;

	Request.PacketType		= EXIT_SELF_TEST_TASK;
	Request.PacketLength	= sizeof(Header_t);

	ReturnValue = Transaction(&Request, NULL);
	if (ReturnValue == NOERROR)
		ReturnValue = GetSelfTestSpecialMessage(Result, Reason);

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : AbortSTSequence
*
* Abstract :
*
*	Sends an Abort Self Test Sequence message to the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SELF_TEST_COMMAND
*				STC_CMD_ABORT_SEQ
*				NULL
*
*	Procedure Parameters :
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of SELF_TEST_COMMAND type with the
*	Command STC_CMD_ABORT_SEQ and sends it to the Logic.
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI AbortSTSequence(void)
{
	INT16					ReturnValue;
	SelfTestCommandReq_t	Request;

	Request.PacketType		= SELF_TEST_COMMAND;
	Request.PacketLength	= sizeof(Header_t) + 4;
	Request.CommandID		= STC_CMD_ABORT_SEQ;

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : SendOperatorAcknowledge
*
* Abstract :
*
*	Sends a Operator Acknowledge message to the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SELF_TEST_COMMAND
*				STC_CMD_OPRTR_ACK
*				NULL
*
*	Procedure Parameters :
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of SELF_TEST_COMMAND type with the
*	Command STC_CMD_OPRTR_ACK and sends it to the Logic.
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI SendOperatorAcknowledge(void)
{
	INT16					ReturnValue;
	SelfTestCommandReq_t	Request;

	Request.PacketType		= SELF_TEST_COMMAND;
	Request.PacketLength	= sizeof(Header_t) + 4;
	Request.CommandID		= STC_CMD_OPRTR_ACK;

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : UpdateSTTestList
*
* Abstract :
*
*	Sends a Update Self Test Testlist message to the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SELF_TEST_COMMAND
*				STC_CMD_UPDT_LIST
*				NULL
*
*	Procedure Parameters :
*				INT16 	NumberOfTests
*				INT16  TestList[]
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of SELF_TEST_COMMAND type with the
*	Command STC_CMD_UPDT_LIST and sends it to the Logic.
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI UpdateSTTestList(INT16 NumberOfTests, INT16  TestList[])
{
	INT16					ReturnValue;
	INT16					Counter;
	SelfTestCommandReq_t	Request;

	Request.PacketType		= SELF_TEST_COMMAND;
	Request.PacketLength	= sizeof(Header_t) + 4 + (2 * NumberOfTests);
	Request.CommandID		= STC_CMD_UPDT_LIST;
	Request.Data			= MAPINT(NumberOfTests);

	for (Counter = 0; Counter < 100; Counter++)
		Request.TestSet[Counter] = 0;

	for (Counter = 0; Counter < NumberOfTests; Counter++)
		Request.TestSet[Counter] = MAPINT(TestList[Counter]);

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : RunPredefinedSTTests
*
* Abstract :
*
*	Sends a Run Self Test Testlist message to the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SELF_TEST_COMMAND
*				STC_CMD_SEL_LIST
*				NULL
*
*	Procedure Parameters :
*				INT16		TestID
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of SELF_TEST_COMMAND type with the
*	Command STC_CMD_SEL_LIST and sends it to the Logic.
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI RunPredefinedSTTests(INT16 TestID)
{
	INT16					ReturnValue;
	SelfTestCommandReq_t	Request;

	Request.PacketType		= SELF_TEST_COMMAND;
	Request.PacketLength	= sizeof(Header_t) + 4;
	Request.CommandID		= STC_CMD_SEL_LIST;
	Request.Data			= MAPINT(TestID);

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : UpdateSTLoopCount
*
* Abstract :
*
*	Sends a Update Self Test Loop Counr message to the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SELF_TEST_COMMAND
*				STC_CMD_UPDT_LOOP_CNT
*				NULL
*
*	Procedure Parameters :
*				INT16		LoopCount
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of SELF_TEST_COMMAND type with the
*	Command STC_CMD_UPDT_LOP_CNT and sends it to the Logic.
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
**************************************************************************/
extern "C" INT16 WINAPI UpdateSTLoopCount(INT16 LoopCount)
{
	INT16					ReturnValue;
	SelfTestCommandReq_t	Request;

	Request.PacketType		= SELF_TEST_COMMAND;
	Request.PacketLength	= sizeof(Header_t) + 4;
	Request.CommandID		= STC_CMD_UPDT_LOOP_CNT;
	Request.Data			= MAPINT(LoopCount);

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : ExecuteSTTestList
*
* Abstract :
*
*	Sends a Execute Self Test Testlist message to the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SELF_TEST_COMMAND
*				STC_CMD_EXECUTE_LIST
*				NULL
*
*	Procedure Parameters :
*				INT16		TruckInformation
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of SELF_TEST_COMMAND type with the
*	Command STC_CMD_EXECUTE_LIST and sends it to the Logic.
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI ExecuteSTTestList(INT16 TruckInformation)
{
	INT16					ReturnValue;
	SelfTestCommandReq_t	Request;

	Request.PacketType			= SELF_TEST_COMMAND;
	Request.PacketLength		= sizeof(Header_t) + 4;
	Request.CommandID			= STC_CMD_EXECUTE_LIST;
	Request.TruckInformation	= (_TUCHAR)TruckInformation;

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : UpdateSTMode
*
* Abstract :
*
*	Sends a Update Self Test Mode message to the Logic.
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SELF_TEST_COMMAND
*				STC_CMD_UPDT_MODE
*				NULL
*
*	Procedure Parameters :
*				INT16		NewMode
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of SELF_TEST_COMMAND type with the
*	Command STC_CMD_UPDT_MODE and sends it to the Logic.
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
**************************************************************************/
extern "C" INT16 WINAPI UpdateSTMode(INT16 NewMode)
{
	INT16					ReturnValue;
	SelfTestCommandReq_t	Request;

	Request.PacketType		= SELF_TEST_COMMAND;
	Request.PacketLength	= sizeof(Header_t) + 4;
	Request.CommandID		= STC_CMD_UPDT_MODE;
	Request.Data			= MAPINT(NewMode);

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : GetSelfTestResult
*
* Abstract :
*
*	Gets the result of a Self Test from the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_SELF_TEST_PACKET
*				NOERROR
*				MAXSTVARIABLES
*				STC_MSG_MODE_INTERACTIVE
*
*	Procedure Parameters :
*				INT16 						*ValidResult
*				INT16 						*MessageMode
*				INT16 						*TestID
*				INT16 						*TestCase
*				INT16 						*TestResult
*				INT16 						*SetInfo
*				INT16 						*NumOfVars
*				InteractiveResults_t 	*InteractiveResults
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Builds a packet of GET_SELF_TEST_PACKET type and sends it to
*	the logic. If a valid response is retrieved the following
*	data is parsed from the Response packet
*
*		- ValidResult
*		- MessageMode
*		- TestID
*		- TestCase
*		- TestResult
*		- SetInfo
*		- NumOfVars
*		- InteractiveResults
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/09/96		Dev Pradhan @ Adtranz
*			- Added SMRS.
*			11/11/97		Dev Pradhan @ Adtranz
*			- Fixed bug with signed variables.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0.
*
**************************************************************************/
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


