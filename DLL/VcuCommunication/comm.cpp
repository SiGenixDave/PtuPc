/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:	Windows PTU
*
*   File Name:	comm.cpp
*
*   SubSystem:	PTUDLL
*
*   Procedures:
*				
*				Transaction
*				InitCommunication
*				CloseCommunication
*				SendVariable
*				SetWatchElements
*				SetWatchElement
*				UpdateWatchElements
*				GetVariableInformation
*				GetEmbeddedInformation
*				GetChartMode
*				SetChartMode
*				GetChartIndex
*				SetChartIndex
*				SetChartScale
*				GetTimeDate
*				SetTimeDate
*				SetCarID
*				StartClock
*				StopClock
*				PTU_MVB_Interface
*				SetWatchSize
*
*   EPROM Drawing:
*
* .b
*
* History:  ??/??/??    	Creation of Version 1.0         jsl
* Revised:  04/12/96		Dev Pradhan			@ Adtranz
*			- Added Function Headers, Added SMRS's.
*			05/03/96		Dev Pradhan			@ Adtranz
*			- Updated SMRS.
*			04/30/99		Dev Pradhan			@ Adtranz
*			- Added PTU_MVB_Interface for Test Group.
*			07/01/99		Jason O'Connor		@ Adtranz
*			  - Ported to Visual C++ 5.0.
*			  - Added extern "C", WINAPI to function declarations for export.
*			  -	Removed obsolete functions LibMain and WEP. 
*			  -	Added include of stdafx.h for VC++ compiling.
*			  - Removed Vbapi.h; Updated include of winsock.h.
*			  - Modified Vbapi.h associated data types in GetEmbeddedInformation and ParseBitMask.
*			  - Changed string copy calls in GetEmbeddedInformation and SetCarID and used the _T macro for UNICODE/ANSI 
*				compatability.
* Revised:  04/01/09		Vijay Kumar Bhoga	@ BTECI
*			  - Logic for 1 byte Acknowledgement is Added in Transaction() function.
* Revised:  07/21/11		Sean Duggan			@ BTNA
*			  - Changed CloseCommunication to return the correct error code for success or failure for Ethernet.
*			  - Moved definition of WATCHSIZE from comm.h to here to avoid redefinition errors and to make it changeable.
*			  - Modified SetWatchElements to use new templated SetWatchElementsReq_t templated structure with values based off of WATCHSIZE
*			  - Modified UpdateWatchElements to use new templated UpdateWatchElementsRes_t templated structure with values based off of WATCHSIZE
*			  - Added SetWatchSize function to set WATCHSIZE remotely.
* Revised: 	11/08/13		Keith McDonald		@ Northern Software Engineering
*			  -	Replaced the calls to SysReAllocString() in GetEmbeddedInformation() with calls to _bstr_t( ... ).copy() as there were problems 
*				associated with using SysReAllocString when the BSTR types were marshalled through unmanaged C#. This came about as a result of 
*				trying to get the C# managed PTU to run under 64 bit Windows 7.
*			  - included <comdef.h> to support the _bstr_t class.
*
* Revised:	03/26/15	Ver. 2.0	K McDonald	@ Northern Software Engineering
*			  -	Changes to allow the PTU to handle both 2 and 4 character year codes. Removed the Set4TimeDate() and Get4TimeDate() methods
*				and added a parameter to the SetTimeDate() and GetTImeDate() methods to specify whether the VCU supports a 2 or 4 digit year
*				code. Also implemented support for 4 digit year code within these methods.
*****************************************************************************/

/* Includes */
#include "stdafx.h"
#include <windows.h>
#include <winsock.h>
#include <ole2.h>
#include <time.h>
#include <string.h>
#include <stdlib.h>
#include "selftest.h"
#include "comm.h"
#include "clntsock.h"
#include <iostream>
#include <comdef.h>

/* Externs */
extern INT16	GetACKEthernet();

#ifdef TORONTO
	UINT16	WATCHSIZE = 40;
#else
	UINT16	WATCHSIZE = 40;
#endif

	
/**************************************************************************
*
* .b
* Procedure Name : Transaction
*
* Abstract :
*
*	Conducts a communication transaction.
*
* INPUTS :
*
*   Globals :
*				CommunicationProtocol
*
*   Constants :
*				COMMANDREQUEST
*				DATAREQUEST
*				NOERROR
*				NULL
*				RS232
*
*	Procedure Parameters :
*				Header_t	*Request
*				Header_t 	*Response
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
*	Determines the type of request (COMMAND or DATA) and then sends out
*	the request and retrieves the Response.
*
* .b
*
* History : ??/??/??		Jeff Lyon			@ AEG		Created
* Revised : 04/12/96		Dev Pradhan			@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan			@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan			@ Adtranz
*			- Updated SMRS.
* Revised : 04/01/09		Vijay Kumar Bhoga	@ BTECI
*			- Added New Variable "req_TypeSent".
*			- Logic for 1 byte Acknowledgement is Added.
**************************************************************************/
INT16 Transaction(Header_t* Request, Header_t* Response)
{
	INT16	ReturnValue;
	INT16   req_TypeSent;

	req_TypeSent=Request->PacketType;
	if (Response == NULL)
		Request->ResponseType = COMMANDREQUEST;
	else
		Request->ResponseType = DATAREQUEST;

	ReturnValue = SendDataPacket(Request);

	if (ReturnValue != NOERROR)
		return ReturnValue;

	//Check for Close Connection Request
	if (Response == NULL && req_TypeSent==TERMINATECONNECTION)
	{
		return NOERROR;
	}

	if (Response == NULL && CommunicationProtocol == RS232)
		ReturnValue = GetACK();

#if MOBILEPTU
	else if(Response == NULL && CommunicationProtocol ==TCPIP) //Check for 1 byte ACK
	{
		ReturnValue = GetACKEthernet();
	}
#endif
	else
	{
		ReturnValue = GetDataPacket(Response);
	}
	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : InitCommunication
*
* Abstract :
*
*	Calls the communication initialisation routines.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				TCPIP
*				UDP
*				RS232
*
*	Procedure Parameters :
*				INT16 	Protocol
*				LPSTR 	PortNumber
*				INT16 	BaudRate
*				INT16 	ByteSize
*				INT16 	Parity
*				INT16 	StopBits
*
* OUTPUTS :
*
*   Globals :
*				CommunicationProtocol
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	Depending on the passed Protocol, this routine will attempt to
*	initialise the Comm Ports or Sockets specified by PortNumber.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI InitCommunication(INT16 Protocol, LPSTR PortNumber,
	INT32 BaudRate, INT16 ByteSize, INT16 Parity,
	INT16 StopBits)
{
	INT16	ReturnValue;

	/// Test Comment. 
	CommunicationProtocol = Protocol;
	switch(Protocol)
	{
	case TCPIP:
	case UDP:
#if MOBILEPTU
		ReturnValue = InitTCPIP(PortNumber);
		break;
#endif
	case RS232:
	default	:
		ReturnValue = InitCommPort(atoi(PortNumber),
			(BaudRate),
			(ByteSize),
			(Parity),
			(StopBits));
		break;
	}

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : CloseCommunication
*
* Abstract :
*
*	Calls the communication termination routines.
*
* INPUTS :
*
*   Globals :
*				CommID
*
*   Constants :
*				TCPIP
*				UDP
*				CLOSECOMMPORT
*				NULL
*				RS232
*
*	Procedure Parameters :
*				INT16 	Protocol
*
* OUTPUTS :
*
*   Globals :
*				CommunicationProtocol
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*	Depending on the passed Protocol, this routine will attempt to
*	close the open Comm Ports or terminate the currently active Sockets.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*			- Replaced obsolete CloseComm with CloseHandle.
*
**************************************************************************/
extern "C" INT16 WINAPI CloseCommunication(INT16 Protocol)
{
#if MOBILEPTU
	Header_t	Request;
#endif
	INT16			ReturnValue;

	CommunicationProtocol = Protocol;
	switch(Protocol)
	{
#if MOBILEPTU
	case TCPIP:
	case UDP:
		Request.PacketType	 = TERMINATECONNECTION;
		Request.PacketLength = sizeof(Header_t);
		ReturnValue 		 = Transaction(&Request, NULL);
		ReturnValue = RPTUClient_TerminateSocket();
		WSACleanup();

		return ReturnValue;
		break;
#endif
	case RS232:
	default	:
		if (CommID > 0)
		{
			ReturnValue = (INT16)CloseHandle((HANDLE)CommID);
			CommID = 0;
		}
		break;
	}

	if (ReturnValue)
		return NOERROR;
	else 
		return UNKNOWNERROR;
}


/**************************************************************************
*
* .b
* Procedure Name : SendVariable
*
* Abstract :
*
*	Sends a new value for a variable to the EPTUI.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SEND_VARIABLE_VALUE
*				MAPINT
*				MAPLONG
*				SIGNED
*				UNSIGNED
*				BADREQUEST
*				NULL
*
*	Procedure Parameters :
*				INT16 	DictionaryIndex
*				INT16 	DataType
*				double	Data
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
*	Builds a packet of SEND_VARIABLE_VALUE type and sends it to the EPTUI.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI SendVariable(INT16 DictionaryIndex, INT16 DataType, double Data)
{
	INT16						ReturnValue;
	SendVariableReq_t		Request;


	Request.PacketType		= SEND_VARIABLE_VALUE;
	Request.PacketLength	= sizeof(SendVariableReq_t);
	Request.DictionaryIndex	= MAPINT(DictionaryIndex);

	if (DataType == SIGNED)
		Request.NewValue.Signed		= (signed long)(MAPLONG((long)Data));
	else if (DataType == UNSIGNED)
		Request.NewValue.Unsigned	= MAPLONG((unsigned long)Data);
	else
		return BADREQUEST;

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : SetWatchElements
*
* Abstract :
*
*	Sets the Watch Window elements on the EPTUI. The EPTUI must know this
*	to know which data values to send to the WPTU.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SET_WATCH_ELEMENTS
*				WATCHSIZE
*				MAPINT
*				NULL
*
*	Procedure Parameters :
*				INT16 	*WatchElements
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
*	Builds a packet of SET_WATCH_ELEMENTS type and sends it to the
*	EPTUI. This packet contains in particular the Data Dictionary
*	Indices of the variables to be displayed on the Watch Window
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor	 @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
**************************************************************************/
extern "C" INT16 WINAPI SetWatchElements(INT16 *WatchElements)
{
	INT16						Counter;
	INT16						ReturnValue;

	// I tried all kinds of clever inheritance schemes before I found out that that messed up the signature of the method, so we
	// use the inelegant but correct copy-pasted chunks of code.
	if (WATCHSIZE == 40)
	{
		SetWatchElementsReq_t<40> req40;

		req40.PacketType		= SET_WATCH_ELEMENTS;
		req40.PacketLength	= sizeof(req40);

		for (Counter = 0; Counter < WATCHSIZE; Counter++)
			req40.WatchElement[Counter] = MAPINT(WatchElements[Counter]);

		ReturnValue = Transaction((Header_t *)&req40, NULL);
	}
	else if (WATCHSIZE == 80)
	{
		SetWatchElementsReq_t<80> req80;

		req80.PacketType		= SET_WATCH_ELEMENTS;
		req80.PacketLength	= sizeof(req80);

		for (Counter = 0; Counter < WATCHSIZE; Counter++)
			req80.WatchElement[Counter] = MAPINT(WatchElements[Counter]);

		ReturnValue = Transaction((Header_t *)&req80, NULL);
	}
	else
	{
		SetWatchElementsReq_t<20> req20;

		req20.PacketType		= SET_WATCH_ELEMENTS;
		req20.PacketLength	= sizeof(req20);

		for (Counter = 0; Counter < WATCHSIZE; Counter++)
			req20.WatchElement[Counter] = MAPINT(WatchElements[Counter]);

		ReturnValue = Transaction((Header_t *)&req20, NULL);
	}

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : SetWatchElement
*
* Abstract :
*
*	Sets a Watch Window element on the EPTUI.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SET_WATCH_ELEMENT
*				MAPINT
*				NULL
*
*	Procedure Parameters :
*				INT16 	ElementIndex
*				INT16		DictionaryIndex
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
*	Builds a packet of SET_WATCH_ELEMENT type and sends it to the
*	EPTUI. This packet contains in particular the Data Dictionary
*	Index of a variable being displayed on the Watch Window.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3.
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI SetWatchElement(INT16 ElementIndex, INT16 DictionaryIndex)
{
	INT16					ReturnValue;
	SetWatchElementReq_t	Request;

	Request.PacketType		= SET_WATCH_ELEMENT;
	Request.PacketLength	= sizeof(SetWatchElementReq_t);
	Request.ElementIndex	= MAPINT(ElementIndex);
	Request.DictionaryIndex	= MAPINT(DictionaryIndex);

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : UpdateWatchElements
*
* Abstract :
*
*	Requests data values for Variables in the Watch Window.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				UPDATE_WATCH_ELEMENTS
*				MAPINT
*				MAPLONG
*				SIGNED
*				UNSIGNED
*				NOERROR
*				BADRESPONSE
*
*	Procedure Parameters :
*				INT16			ForceUpdate
*				double		*WatchValues
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
*	Builds a packet of UPDATE_WATCH_ELEMENTS type and sends it to the
*	EPTUI. Retrieves from the Response packet the values of each of
*	the variables on the Watch Window.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*			- Replaced (unsigned char) with _TUCHAR to avoid possible UNICODE/ANSI conflicts.
*
**************************************************************************/
extern "C" INT16 WINAPI UpdateWatchElements(INT16	ForceUpdate,
											double	WatchValues[],
											INT16	DataType[] )
{
	UINT16						Counter;
	INT16						ReturnValue;
	UpdateWatchElementsReq_t	Request;
	UINT16						NumberOfUpdates;
	INT16						CurrentDataType;
	UINT32						templong;

	Request.PacketType		= UPDATE_WATCH_ELEMENTS;
	Request.PacketLength	= sizeof(UpdateWatchElementsReq_t);
	Request.ForceFullUpdate	= (_TUCHAR)ForceUpdate;

	if (WATCHSIZE == 40)
	{
		UpdateWatchElementsRes_t<40>	Response;
		ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);
		if (ReturnValue == NOERROR)
		{
			NumberOfUpdates = MAPINT(Response.NumberOfUpdates);

			for (Counter = 0; Counter < NumberOfUpdates; Counter++)
			{
				templong = sizeof(WatchElement_t);
				CurrentDataType = MAPINT(Response.WatchElement[Counter].DataType);

				DataType[MAPINT(Response.WatchElement[Counter].Index)] = 
					CurrentDataType;

				if (CurrentDataType == (INT16)SIGNED)
					WatchValues[MAPINT(Response.WatchElement[Counter].Index)] =
					(signed long)(MAPLONG(Response.WatchElement[Counter].NewValue.Signed));

				else if (CurrentDataType == (INT16)UNSIGNED)
				{
					WatchValues[MAPINT(Response.WatchElement[Counter].Index)] =
						MAPLONG(Response.WatchElement[Counter].NewValue.Unsigned);
				}
				else
					return BADRESPONSE;
			}
		}
	} 
	else if (WATCHSIZE == 80)
	{
		UpdateWatchElementsRes_t<80>	Response;
		ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);
		if (ReturnValue == NOERROR)
		{
			NumberOfUpdates = MAPINT(Response.NumberOfUpdates);

			for (Counter = 0; Counter < NumberOfUpdates; Counter++)
			{
				templong = sizeof(WatchElement_t);
				CurrentDataType = MAPINT(Response.WatchElement[Counter].DataType);

				DataType[MAPINT(Response.WatchElement[Counter].Index)] = 
					CurrentDataType;

				if (CurrentDataType == (INT16)SIGNED)
					WatchValues[MAPINT(Response.WatchElement[Counter].Index)] =
					(signed long)(MAPLONG(Response.WatchElement[Counter].NewValue.Signed));

				else if (CurrentDataType == (INT16)UNSIGNED)
				{
					WatchValues[MAPINT(Response.WatchElement[Counter].Index)] =
						MAPLONG(Response.WatchElement[Counter].NewValue.Unsigned);
				}
				else
					return BADRESPONSE;
			}
		}
	} 
	else
	{
		UpdateWatchElementsRes_t<20>	Response;
		ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);
		if (ReturnValue == NOERROR)
		{
			NumberOfUpdates = MAPINT(Response.NumberOfUpdates);

			for (Counter = 0; Counter < NumberOfUpdates; Counter++)
			{
				templong = sizeof(WatchElement_t);
				CurrentDataType = MAPINT(Response.WatchElement[Counter].DataType);

				DataType[MAPINT(Response.WatchElement[Counter].Index)] = 
					CurrentDataType;

				if (CurrentDataType == (INT16)SIGNED)
					WatchValues[MAPINT(Response.WatchElement[Counter].Index)] =
					(signed long)(MAPLONG(Response.WatchElement[Counter].NewValue.Signed));

				else if (CurrentDataType == (INT16)UNSIGNED)
				{
					WatchValues[MAPINT(Response.WatchElement[Counter].Index)] =
						MAPLONG(Response.WatchElement[Counter].NewValue.Unsigned);
				}
				else
					return BADRESPONSE;
			}
		}
	}


	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : GetVariableInformation
*
* Abstract :
*
*	Retrieves Data Dictionary variable attributes.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_VARIABLE_INFORMATION
*				MAPINT
*				NOERROR
*				MAPLONG
*
*	Procedure Parameters :
*				INT16			DictionaryIndex
*				INT16 		*DataType
*				double		*MaxScale
*				double		*MinScale
*				long		*AttributeFlags
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
*	Builds a packet of GET_VARIABLE_INFORMATION type and sends it to the
*	EPTUI. Retrieves from the Response packet the type, chart scalings
*	and attributes of the variable in the Data Dictionary indexed by the
*	passed in DictionaryIndex.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			05/06/96		Dev Pradhan		@ Adtranz
*			- Changed MaxScale and MinScale to doubles from longs
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI GetVariableInformation(	INT16			DictionaryIndex,
	INT16 		*DataType,
	double		*MaxScale,
	double		*MinScale,
	long		*AttributeFlags	)
{
	INT16						ReturnValue;
	GetVariableInfoReq_t	Request;
	GetVariableInfoRes_t	Response;

	Request.PacketType		= GET_VARIABLE_INFORMATION;
	Request.PacketLength	= sizeof(GetVariableInfoReq_t);
	Request.DictionaryIndex	= MAPINT(DictionaryIndex);

	ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);

	if (ReturnValue == NOERROR)
	{
		*DataType		= MAPINT(Response.DataType);
		*MaxScale		= (double)MAPLONG(Response.MaxScale);
		*MinScale		= (double)MAPLONG(Response.MinScale);
		*AttributeFlags	= MAPLONG(Response.AttributeFlags);
	}

	return ReturnValue;
}


//extern "C" INT16 WINAPI SetConfigNum( double	*ConfigNum)
//{
//	INT16						ReturnValue;
//	SetConfigNumRes_t		Response;
//
//	ReturnValue = Transaction((Header_t *)&Response, NULL);
//
//	if (ReturnValue == NOERROR)
//	{
//		*ConfigNum		= (double)MAPLONG(Response.ConfigNum);
//	}
//
//	return ReturnValue;
//}


/**************************************************************************
*
* .b
* Procedure Name : GetEmbeddedInformation
*
* Abstract :
*
*	Retrieves connected Logic attributes.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_EMBEDDED_INFORMATION
*				NOERROR
*
*	Procedure Parameters :
*				BSTR		SoftwareVersion
*				BSTR		CarID
*				BSTR		SubSystemName
*				BSTR		IdentifierString
*				double		*ConfigurationMask
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
*	Builds a packet of GET_EMBEDDED_INFORMATION type and sends it to the
*	EPTUI. Retrieves from the Response packet the Version of the Logic S/W
*	the Car Id on the Logic and the Sub System Name and Identifer String
*	which are unique attributes. It also receives a Configuration Mask that
*	represents the available EPTUI functions.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3.
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			- Added ConfigurationMask.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*			- Replace obsolete VBSetHlstr with SysReAllocString.
*			- Changed type of parameter from HLSTR to *BSTR.
*			- Replaced _fstrcpy() with _tcsncpy().
*			11/08/13		Keith McDonald	@ Northern Software Engineering
*			- Modified to support export to managed C#.
*			- Replaced the calls to SysReAllocString() with calls to 
*			- _bstr_t( ... ).copy() in order to convert a standard string 
*			- to a BSTR.
**************************************************************************/
extern "C" INT16 WINAPI GetEmbeddedInformation(BSTR	*SoftwareVersion,
	BSTR	*CarID,
	BSTR	*SubSystemName,
	BSTR	*IdentifierString,
	double 	*ConfigurationMask )
{
	INT16						ReturnValue;
	Header_t				Request;
	GetEmbeddedInfoRes_t	Response;
	_TCHAR					TempStr[50];

	Request.PacketType		= GET_EMBEDDED_INFORMATION;
	Request.PacketLength	= sizeof(Header_t);
	
	ReturnValue = Transaction(&Request, (Header_t *)&Response);

	// The SoftwareVersion etc output parameters must be passed as BSTR types for them 
	// to be accessible in managed C#. Used the '_bstr_t()' approach as there were problems 
	// associated with SysReAllocString() - K.McD.

	if (ReturnValue == NOERROR)
	{
		_tcsnset_s(TempStr, sizeof(TempStr), ' ', 50 );
		_tcsncpy_s(TempStr, sizeof(TempStr), Response.SoftwareVersion, 40);
		//SysReAllocString(SoftwareVersion, (BSTR)TempStr);
		*SoftwareVersion = _bstr_t(TempStr).copy();

		_tcsnset_s(TempStr, sizeof(TempStr), ' ', 50 );
		_tcsncpy_s(TempStr, sizeof(TempStr), Response.CarID, 10);
		//SysReAllocString(CarID, (BSTR)TempStr);
		*CarID = _bstr_t(TempStr).copy();

		_tcsnset_s(TempStr, sizeof(TempStr), ' ', 50 );	

#if DOCUMENT
		//SysReAllocString(SubSystemName, (BSTR)"<Project><Application>");
		*SubSystemName =_bstr_t(L "<Project><Application>").copy();
#else
		_tcsncpy_s(TempStr, sizeof(TempStr), Response.SubSystemName, 50);
		//SysReAllocString(SubSystemName, (BSTR)TempStr);
		*SubSystemName =_bstr_t(TempStr).copy();
#endif

		_tcsnset_s(TempStr, sizeof(TempStr), ' ', 50 );
		_tcsncpy_s(	TempStr, sizeof(TempStr), Response.IdentifierString, 4);
		//SysReAllocString(IdentifierString, (BSTR)TempStr);
		*IdentifierString = _bstr_t(TempStr).copy();

		if (Response.PacketLength > 106)
			*ConfigurationMask = (double)MAPLONG(Response.ConfigurationMask);
		else
			*ConfigurationMask = 0;
	}

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : GetChartMode
*
*	Retrieves the current Chart Recording Mode.
*
* Abstract :
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_CHART_MODE
*				NOERROR
*
*	Procedure Parameters :
*				INT16		*CurrentChartMode
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
*	Builds a packet of GET_CHART_MODE type and sends it to the EPTUI.
*	Retrieves from the Response packet the current chart recording mode
*	on the Logic.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI GetChartMode(INT16 *CurrentChartMode)
{
	INT16					ReturnValue;
	Header_t			Request;
	GetChartModeRes_t	Response;

	Request.PacketType		= GET_CHART_MODE;
	Request.PacketLength	= sizeof(Header_t);

	ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);

	if (ReturnValue == NOERROR)
		*CurrentChartMode = Response.CurrentChartMode;

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : SetChartMode
*
* Abstract :
*
*	Sets the current Chart Recording Mode.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SET_CHART_MODE
*				NULL
*
*	Procedure Parameters :
*				INT16		TargetChartMode
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
*	Builds a packet of SET_CHART_MODE type and sends it to the EPTUI.
*	Sends the new Chart Mode in the Request packet.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3.
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI SetChartMode(INT16 TargetChartMode)
{
	INT16					ReturnValue;
	SetChartModeReq_t	Request;

	Request.PacketType		= SET_CHART_MODE;
	Request.PacketLength	= sizeof(SetChartModeReq_t);
	Request.TargetChartMode	= (_TUCHAR)TargetChartMode;

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : GetChartIndex
*
* Abstract :
*
*	Retrieves the current Data Dictionary Index of the variable being
*	Charted on a given Chart Recorder Channel
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_CHART_INDEX
*				NOERROR
*				MAPINT
*
*	Procedure Parameters :
*				INT16		ChartIndex
*				INT16		*VariableIndex
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
*	Builds a packet of GET_CHART_INDEX type and sends it to the EPTUI.
*	Retrieves from the Response packet the Data Dictionary Index of the
*	variable being Charted on the requested chart channel.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*			- Replaced (unsigned char) to _TUCHAR to avoid possible UNICODE/ANSI conflicts.
*
**************************************************************************/
extern "C" INT16 WINAPI GetChartIndex(INT16 ChartIndex, INT16 *VariableIndex)
{
	INT16					ReturnValue;
	GetChartIndexReq_t	Request;
	GetChartIndexRes_t	Response;

	Request.PacketType		= GET_CHART_INDEX;
	Request.PacketLength	= sizeof(GetChartIndexReq_t);
	Request.ChartIndex		= (_TUCHAR)ChartIndex;

	ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);

	if (ReturnValue == NOERROR)
		*VariableIndex = MAPINT(Response.VariableIndex);

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : SetChartIndex
*
* Abstract :
*
*	Sets the passed in variable to be Charted on the passed in
*	Chart Recorder Channel
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SET_CHART_INDEX
*				MAPINT
*				NULL
*
*	Procedure Parameters :
*				INT16		ChartIndex
*				INT16		VariableIndex
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
*	Builds a packet of SET_CHART_INDEX type and sends it to the EPTUI.
*	Sends the Data Dictionary Index of the variable and the desired
*	Chart channel that this variable should be charted to in the Request
*	packet.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3.
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*			- Replaced (unsigned char) to _TUCHAR to avoid possible UNICODE/ANSI conflicts.
*
**************************************************************************/
extern "C" INT16 WINAPI SetChartIndex(INT16 ChartIndex, INT16 VariableIndex)
{
	INT16					ReturnValue;
	SetChartIndexReq_t	Request;

	Request.PacketType		= SET_CHART_INDEX;
	Request.PacketLength	= sizeof(SetChartIndexReq_t);
	Request.ChartIndex		= (_TUCHAR)ChartIndex;
	Request.VariableIndex	= MAPINT(VariableIndex);

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : SetChartScale
*
* Abstract :
*
*	Sets the Minimum and Maximum Chart Scaling for a variable in the
*	Data Dictionary
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SET_CHART_INDEX
*				MAPINT
*				MAPLONG
*				NULL
*
*	Procedure Parameters :
*				INT16		DictionaryIndex
*				long	MaxScale
*				long 	MinScale
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
*	Builds a packet of SET_CHART_SCALE type and sends it to the EPTUI.
*	Sends the Data Dictionary Index of the variable and the desired
*	Minimum and Maximum Scalings to be used when this variable is
*	chart recorded.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI SetChartScale(INT16 DictionaryIndex, double MaxScale,
	double MinScale)
{
	INT16					ReturnValue;
	SetChartScaleReq_t		Request;

	Request.PacketType		= SET_CHART_SCALE;
	Request.PacketLength	= sizeof(SetChartScaleReq_t);
	Request.DictionaryIndex	= MAPINT(DictionaryIndex);
	Request.MaxScale		= MAPLONG((long)MaxScale);
	Request.MinScale		= MAPLONG((long)MinScale);

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : GetTimeDate
*
* Abstract :
*
*	Retrieves the time and date on the Real Time Clock on the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_TIME_DATE
*				NOERROR
*
*	Procedure Parameters :
*				INT16		 	*Year
*				INT16			*Month
*				INT16			*Day
*				INT16			*Hour
*				INT16			*Minute
*				INT16			*Second
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
*	Builds a packet of GET_TIME_DATE type and sends it to the EPTUI.
*	Retrieves the Year, Month, Day, Hour, Minute and Seconds from the
*	Response Packet.
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3.
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*			03/25/15		K.McDonald		@ NSEL
*			- Added support for 4 digit year codes.
*
**************************************************************************/
extern "C" INT16 WINAPI GetTimeDate(INT16 Use4DigitYearCode, INT16 *Year, INT16 *Month, INT16 *Day,
									INT16 *Hour, INT16 *Minute, INT16 *Second)
{
	INT16				ReturnValue;
	Header_t			Request;

	Request.PacketType		= GET_TIME_DATE;
	Request.PacketLength	= sizeof(Header_t);

	// Check whether the VCU uses a 4 digit year code.
	if (Use4DigitYearCode == TRUE)
	{
		// Yes, use the 4 digit year code response packet structure.
		Get4TimeDateRes_t	Response;
		ReturnValue = Transaction(&Request, (Header_t *)&Response);

		if (ReturnValue == NOERROR)
		{
			*Year	= MAPINT((UINT)Response.Year);
			*Month	= Response.Month;
			*Day	= Response.Day;
			*Hour	= Response.Hour;
			*Minute	= Response.Minute;
			*Second	= Response.Second;
		}
	}
	else
	{
		// No, use the 2 digit year code packet structure.
		GetTimeDateRes_t	Response;
		ReturnValue = Transaction(&Request, (Header_t *)&Response);

		if (ReturnValue == NOERROR)
		{
			*Year	= Response.Year;
			*Month	= Response.Month;
			*Day	= Response.Day;
			*Hour	= Response.Hour;
			*Minute	= Response.Minute;
			*Second	= Response.Second;
		}
	}

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : SetTimeDate
*
* Abstract :
*
*	Sets the time and date on the Real Time Clock on the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SET_TIME_DATE
*				NULL
*
*	Procedure Parameters :
*				INT16		Year
*				INT16		Month
*				INT16		Day
*				INT16		Hour
*				INT16		Minute
*				INT16		Second
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
*	Builds a packet of SET_TIME_DATE type and sends it to the EPTUI.
*	Sends the Year, Month, Day, Hour, Minute and Seconds in the
*	Request Packet.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*			- Replaced (unsigned char) to _TUCHAR to avoid possible UNICODE/ANSI conflicts.
*			03/25/15		K.McDonald		@ NSEL
*			- Added support for 4 digit year codes.
*
**************************************************************************/
extern "C" INT16 WINAPI SetTimeDate(INT16 Use4DigitYearCode, INT16 Year, INT16 Month, INT16 Day, INT16 Hour,
	INT16 Minute, INT16 Second)
{
	INT16				ReturnValue;

	if (Use4DigitYearCode == TRUE)
	{
		Set4TimeDateReq_t		Request;

		Request.PacketType		= SET_TIME_DATE;
		Request.PacketLength	= sizeof(Set4TimeDateReq_t);
		Request.Year			= MAPINT((UINT)Year);
		Request.Month			= (_TUCHAR)Month;
		Request.Day				= (_TUCHAR)Day;
		Request.Hour			= (_TUCHAR)Hour;
		Request.Minute			= (_TUCHAR)Minute;
		Request.Second			= (_TUCHAR)Second;

		ReturnValue = Transaction((Header_t *)&Request, NULL);
	}
	else
	{
		SetTimeDateReq_t	Request;

		Request.PacketType		= SET_TIME_DATE;
		Request.PacketLength	= sizeof(SetTimeDateReq_t);
		Request.Year			= (_TUCHAR)Year;
		Request.Month			= (_TUCHAR)Month;
		Request.Day				= (_TUCHAR)Day;
		Request.Hour			= (_TUCHAR)Hour;
		Request.Minute			= (_TUCHAR)Minute;
		Request.Second			= (_TUCHAR)Second;

		ReturnValue = Transaction((Header_t *)&Request, NULL);
	}

	return ReturnValue;
}

/**************************************************************************
*
* .b
* Procedure Name : SetCarID
*
* Abstract :
*
*	Sets the Car ID on the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SET_CARID
*				NULL
*
*	Procedure Parameters :
*				BSTR	NewCarID
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
*	Builds a packet of SET_CARID type and sends it to the EPTUI.
*	Sends the new Car ID in the Request Packet.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*			- Replaced obsolete _fstrcpy() with _tcsncpy().
*			- Removed VBDerefStr(). 
*
**************************************************************************/
extern "C" INT16 WINAPI SetCarID(INT16 NewCarID)
{
	INT16				ReturnValue;
	SetCarIDReq_t		Request;

	_TCHAR				NewCarStr[11];    

	Request.PacketType		= SET_CARID;
	Request.PacketLength	= sizeof(SetCarIDReq_t);

	sprintf_s(NewCarStr, "%11d", NewCarID);

	_tcsncpy_s(Request.NewCarID, NewCarStr, 11);

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : StartClock
*
* Abstract :
*
*	Starts the Real Time Clock on the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				START_CLOCK
*				NULL
*
*	Procedure Parameters :
*				None
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
*	Builds a packet of START_CLOCK type and sends it to the EPTUI.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3.
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI StartClock(void)
{
	INT16				ReturnValue;
	Header_t		Request;

	Request.PacketType		= START_CLOCK;
	Request.PacketLength	= sizeof(Header_t);

	ReturnValue = Transaction(&Request, NULL);

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : StopClock
*
* Abstract :
*
*	Stops the Real Time Clock on the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				STOP_CLOCK
*				NULL
*
*	Procedure Parameters :
*				None
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
*	Builds a packet of STOP_CLOCK type and sends it to the EPTUI.
*
* .b
*
* History : ??/??/??		Jeff Lyon		@ AEG		Created
* Revised : 04/12/96		Dev Pradhan		@ Adtranz
*			- Added SMRS.
*			04/15/96		Dev Pradhan		@ Adtranz
*			- Revised for WPTU v1.3
*			05/03/96		Dev Pradhan		@ Adtranz
*			- Updated SMRS.
*			07/01/99		Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI StopClock(void)
{
	INT16				ReturnValue;
	Header_t		Request;

	Request.PacketType		= STOP_CLOCK;
	Request.PacketLength	= sizeof(Header_t);

	ReturnValue = Transaction(&Request, NULL);

	return ReturnValue;
}


/**************************************************************************
*
* .b
* Procedure Name : PTU_MVB_Interface
*
* Abstract :
*
*	Interface for Test Group s/w
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				BTU
*
*	Procedure Parameters :
*				RequestBuffer
*				ResultsBuffer
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
*	Builds a packet of BTU type and sends it to the EPTUI and receives a
*	packet from the EPTUI.
*	This is an interface for the Test Group for the BTU.
*
* .b
*
* History : 04/30/99		Dev Pradhan		@ Adtranz Created
* Revised : 07/01/99        Jason O'Connor	@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI PTU_MVB_Interface(UINT16 Mode,
	UINT16 *RequestBuffer,
	UINT16 *ResultBuffer)
{
	INT16				ReturnValue;
	INT16				Counter;
	BTU_test		Request;
	BTU_test		Response;

	Request.PacketType		= BTU;
	Request.PacketLength	= sizeof(BTU_test);
	Request.mode			= Mode;

	for (Counter = 0; Counter < 17; Counter++)
	{
		Request.Buffer[Counter] = RequestBuffer[Counter];
	}

	ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);
	if (ReturnValue == NOERROR)
	{
		for (Counter = 0; Counter < 17; Counter++)
		{
			ResultBuffer[Counter] = Response.Buffer[Counter];
		}
	}

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name :  ParseBitMask
*
* Abstract :
*
*	Returns a string delimited by CR that represents information about each
*	bit set for a bit masked value.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				NOERROR
*
*	Procedure Parameters :
*				INT16 			NumberValues
*				EnumValue_t		EnumValue[]
*				double 			RawValue
*				BSTR			BitMaskString
*
* OUTPUTS :
*
*   Globals :
*				None
*
*   Returned Values :
*				NOERROR - 0
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : 08/13/96		Dev Pradhan			@ Adtranz
* Revised : 07/01/99		Jason O'Connor		@ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*			- Replace obsolete VBSetHlstr with SysAllocStringLen.
*			- Changed type of parameter from HLSTR to BSTR.
*
****************************************************************************/
extern "C" INT16 WINAPI ParseBitMask( INT16 			NumberValues,
	EnumValue_t	EnumValue[],
	double 		RawValue,
	BSTR			BitMaskString )
{
	TCHAR 					ValueString[1000];
	INT16						Counter;
	unsigned long 	Templong;
	INT16						BitsSet;

	Templong = (unsigned long)RawValue;
	ValueString[0] = '\0';
	BitsSet = 0;
	for (Counter = 0; Counter < NumberValues; Counter++)
	{
		if (Templong & (unsigned long)EnumValue[Counter].Value)
		{
			strncat_s(ValueString, EnumValue[Counter].Description, 40);
			BitsSet++;
		}
	}
	BitMaskString = SysAllocStringLen((LPCWSTR)ValueString, strlen(ValueString));
	return BitsSet;
}

/**************************************************************************
*
* .b
* Procedure Name : SetWatchSize
*
* Abstract :
*
*	Sets the number of watch variables supported. Only 20, 40, and 80 are valid values.
*
* INPUTS :
*	Procedure Parameters :
*				UINT16	WatchSize
*
* OUTPUTS :
*				None
* Globals :
*				None
*
*	Returned Values :
*				Current Watch Size.
*
* Functional Description :
*
*		If the Input is 20, 40, or 80, it sets the internal variable to that size for subsequent operations. If it is none of the above,
* the old value is retained.
*
* .b
*
* History : 09/16/2011		Sean Duggan			@ Bombardier Transportation		Created
**************************************************************************/
extern "C" INT16 WINAPI SetWatchSize(UINT16	WatchSize)
{
	if (WatchSize == 20 || WatchSize == 40 || WatchSize == 80)
	{
		WATCHSIZE = WatchSize;
	}

	return WATCHSIZE;
}