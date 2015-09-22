/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:
*               Windows PTU
*
*   File Name:
*               primcomm.cpp
*
*   SubSystem:
*               PTUDLL
*
*   Procedures:
*				InitCommPort
*				InitTCPIP
*				SendDataPacket
*				GetDataPacket
*				GetACK
*
*   EPROM Drawing:
*
* .b
*
*   History:        ??/??/??    Creation of Version 1.0         jsl
*	Revised:		11/17/95	ddp @ ATSI
*					- Added hooks for a Motorola Logic.
*					11/27/95	ddp @ ATSI
*					- Creation of Version 2.0.
*					12/07/95	ddp @ ATSI
*					- Added MOBILEPTU flag.
*					11/06/97	ddp @ Adtranz
*					- Changed GetByte().
*					07/01/99		Jason O'Connor @ Adtranz
*					- Ported to Visual C++ 5.0.
*					- Added include of stdafx.h for VC++ compiling.
*					- Removed Vbapi.h; Updated include of winsock.h.
*					- Updated InitCommPort.
*					- Replaced char with _T macros for UNICODE/ANSI compatability
*					04/01/09		Vijay Kumar Bhoga @ BTECI
*			    	- Added Logic for TCP/IP Send Data Packet in function INT16 SendDataPacket(Header_t *DataPacket).
*			    	- Added Logic for TCP/IP Get Data Packet in function INT16 GetDataPacket(Header_t *DataPacket).
*					08/25/11		K.McD @ Bombardier Tranportation
*					- Modified the read and write communication port timeouts in function InitCommPort( ... ).
*					- Optimized layout for the Visual Studio editor.
*					11/08/13		Keith McDonald @ Northern Software Engineering
*					- Modified the read and write communication port timeouts in function InitCommPort( ... ) following 
*					  reports from CTA that there were occasional communication problems.
*****************************************************************************/
/* Includes */
#include "stdafx.h"
#include <windows.h>
#include <time.h>
#include <string.h>
#include <winsock.h>
#include "selftest.h"
#include "comm.h"
#include "clntsock.h"

/* Prototypes */

INT16 	RS232_SendDataPacket	(Header_t *);
INT16	RS232_GetDataPacket		(Header_t *);
#if MOBILEPTU
INT16 	TCPIP_GetDataPacket		(Header_t *);
#endif
INT16 	GetByte					(void);
INT16 	SendByte				(INT16);
INT16 	SendSOM					(void);
INT16 	GetSOM					(void);

/* Globals */
INT16	CommunicationProtocol;
INT16	CommID;
INT16	GlobalSOM;
_TUCHAR	LocalPacket[20];
BOOL	EventMask;
INT16   PackSize;

/**************************************************************************
*
* .b
* Procedure Name : InitCommPort
*
* Abstract :
*
*	Initialise a Comm Port.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				INQUEUESIZE
*				OUTQUEUESIZE
*				IE_OPEN
*				PORTOPEN
*				IE_HARDWARE
*				NOHARDWARE
*				UNKNOWNERROR
*				EV_RXCHAR
*
*	Procedure Parameters :
*				INT16 	PortName
*				INT16 	BaudRate
*				INT16 	ByteSize
*				INT16 	Parity
*				INT16 	StopBits
*
* OUTPUTS :
*
*   Globals :
*				CommID
*
*	Returned Values :
*				Error Code if FAILED else NOERROR.
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS.
*			07/01/99		Jason O'Connor @ Adtranz	
*			- Replaced obsolete OpenComm() with CreateFile().
*			- Updated DCB block specifications.
*			- Replaced char with TCHAR for UNICODE/ANSI compatability.
* Revised : 11/08/13		Keith McDonald @ Northern Software Engineering
*			- Modified the read and write communication port timeouts in function InitCommPort( ... ) following 
*			  reports from CTA that there were occasional communication problems.
*
**************************************************************************/
INT16 InitCommPort(	INT16 PortName,
	INT32 BaudRate,
	INT16 ByteSize,
	INT16 Parity,
	INT16 StopBits	)
{
	HANDLE			CReturnValue;	
	DCB				DCBBlock;
	BOOL			SetReturnValue;	
	unsigned long	errorNumber;
	TCHAR			PortString[] = _T("COM0:");
	COMMTIMEOUTS	CommTimeOuts;

	switch (PortName)
	{
	case 1:
		PortString[3] = '1';
		break;

	case 2:
		PortString[3] = '2';
		break;

	case 3:
		PortString[3] = '3';
		break;

	case 4:
		PortString[3] = '4';
		break;

	case 5:
		PortString[3] = '5';
		break;

	case 6:
		PortString[3] = '6';
		break;

	case 7:
		PortString[3] = '7';
		break;

	case 8:
		PortString[3] = '8';
		break;
	}

	CommID = 0;

	CReturnValue = CreateFile(PortString, 
							  GENERIC_READ|GENERIC_WRITE,
							  0, NULL, OPEN_EXISTING, 0, NULL);
	errorNumber = GetLastError();

	if (CReturnValue == (HANDLE)IE_OPEN)
	{
		return PORTOPEN;
	}
	else if (CReturnValue == (HANDLE)IE_HARDWARE)
	{
		return NOHARDWARE;
	}
	else if (CReturnValue < 0)
	{
		return UNKNOWNERROR;
	}

	CommTimeOuts.ReadIntervalTimeout = MAXDWORD;
	CommTimeOuts.ReadTotalTimeoutMultiplier = MAXDWORD;
	CommTimeOuts.ReadTotalTimeoutConstant = 2000;

	CommTimeOuts.WriteTotalTimeoutMultiplier = 0;
	CommTimeOuts.WriteTotalTimeoutConstant = 0;

	/*
	If an application sets ReadIntervalTimeout and ReadTotalTimeoutMultiplier to MAXDWORD 
	and sets ReadTotalTimeoutConstant to a value greater than zero and less than MAXDWORD, 
	one of the following occurs when the ReadFile function is called:

		1. If there are any bytes in the input buffer, ReadFile returns immediately with the bytes in the buffer.
		2. If there are no bytes in the input buffer, ReadFile waits until a byte arrives and then returns immediately.
		3. If no bytes arrive within the time specified by ReadTotalTimeoutConstant, ReadFile times out.

	A value of zero for both the WriteTotalTimeoutMultiplier and WriteTotalTimeoutConstant members indicates that 
	total time-outs are not used for write operations.
	*/

	errorNumber = SetCommTimeouts(CReturnValue, &CommTimeOuts);
	errorNumber = GetLastError();

	CommID 					= (INT16)CReturnValue;

	DCBBlock.BaudRate		=  BaudRate;
	DCBBlock.ByteSize		= (BYTE)ByteSize;
	DCBBlock.Parity			= (BYTE)Parity;
	DCBBlock.StopBits		= (BYTE)StopBits;
	DCBBlock.fBinary		= 1;
	DCBBlock.fRtsControl	= RTS_CONTROL_DISABLE;
	DCBBlock.fParity		= 0;
	DCBBlock.fOutxCtsFlow	= 0;
	DCBBlock.fOutxDsrFlow	= 0;
	DCBBlock.fDtrControl	= DTR_CONTROL_DISABLE;
	DCBBlock.fOutX			= 0;
	DCBBlock.fInX			= 0;
	DCBBlock.fNull			= 0;
	DCBBlock.XonChar		= (char)0x11;
	DCBBlock.XoffChar		= (char)0x13;
	DCBBlock.XonLim			= 0;
	DCBBlock.XoffLim		= 0;
	DCBBlock.EofChar		= 0;
	DCBBlock.EvtChar		= 0;

	DCBBlock.DCBlength		= sizeof(DCB);
	DCBBlock.ErrorChar		= (char)0x78;
	DCBBlock.fAbortOnError	= 0;
	DCBBlock.fDsrSensitivity = 0;
	DCBBlock.fTXContinueOnXoff = 0;
	DCBBlock.fErrorChar = 0;

	SetReturnValue = SetCommState(CReturnValue, &DCBBlock);
	errorNumber = GetLastError();

	// Purge both the transmit and receive buffers.
	PurgeComm(CReturnValue, PURGE_RXCLEAR);
	PurgeComm(CReturnValue, PURGE_TXCLEAR);

	if (!SetReturnValue)
	{
		CommID = 0;
		return UNKNOWNERROR;
	}

	EventMask = SetCommMask(CReturnValue, EV_RXCHAR);
	if (!EventMask)
	{
		CommID = 0;
		return UNKNOWNERROR;
	}

	return NOERROR;
}


#if MOBILEPTU
/**************************************************************************
*
* .b
* Procedure Name : InitTCPIP
*
* Abstract :
*
*	Initialise Client for Communication, send message to Server to
*	initialise its Comm Port.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				None
*
*	Procedure Parameters :
*				LPSTR 	Portname
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*
**************************************************************************/
INT16 InitTCPIP(LPSTR Portname)
{
	INT16		ReturnValue;
	//Header_t	Request;
	_TCHAR		TempString[20];

	_tcscpy_s(TempString, Portname);
	ReturnValue = RPTUClient_InitializeSockets(TempString);

	return ReturnValue;
}
#endif

/**************************************************************************
*
* .b
* Procedure Name : SendDataPacket
*
* Abstract :
*
*	Depending on type of Communication (TCP/IP or RS-232) call appropriate
*	routines to send a data packet.
*
* INPUTS :
*
*   Globals :
*				CommunicationProtocol
*
*   Constants :
*				None
*
*	Procedure Parameters :
*				Header_t 	*DataPacket
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
* Revised : 04/01/09		Vijay Kumar Bhoga @ BTECI
*			- Added Logic for TCP/IP Send Data Packet.
*
**************************************************************************/
INT16 SendDataPacket(Header_t *DataPacket)
{
	INT16	ReturnCode;

	switch (CommunicationProtocol)
	{
#if MOBILEPTU
	case TCPIP:
	case UDP:

		ReturnCode = RPTUClient_SendDataPacket(DataPacket);
		break;
#endif
	case RS232:
	default:

		ReturnCode = RS232_SendDataPacket(DataPacket);
		break;
	}

	return ReturnCode;
}

/**************************************************************************
*
* .b
* Procedure Name : GetDataPacket
*
* Abstract :
*
*	Depending on type of Communication (TCP/IP or RS-232) call appropriate
*	routines to receive a data packet.
*
* INPUTS :
*
*   Globals :
*				CommunicationProtocol
*
*   Constants :
*				None
*
*	Procedure Parameters :
*				Header_t 	*DataPacket
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
* Revised : 04/01/09		Vijay Kumar Bhoga @ BTECI
*			- Added Logic for TCP/IP Get Data Packet.
*
**************************************************************************/
INT16 GetDataPacket(Header_t *DataPacket)
{
	INT16	ReturnCode;

	switch (CommunicationProtocol)
	{
#if MOBILEPTU
	case TCPIP:
	case UDP:
		ReturnCode = TCPIP_GetDataPacket(DataPacket);

		DataPacket->PacketLength = MAPINT(DataPacket->PacketLength);
		DataPacket->ResponseType = MAPINT(DataPacket->ResponseType);
		DataPacket->CheckSum = MAPINT(DataPacket->CheckSum);
		DataPacket->PacketType = MAPINT(DataPacket->PacketType);
		break;
#endif
	case RS232:
	default:
		ReturnCode = RS232_GetDataPacket(DataPacket);
		break;
	}
	return ReturnCode;
}

/**************************************************************************
*
* .b
* Procedure Name : GetACK
*
* Abstract :
*
*	Read an ACK from the Serial Port
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				ACK
*				NOERROR
*				BADRESPONSE
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
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*
**************************************************************************/
INT16 GetACK()
{
	INT16	ReturnValue;

	ReturnValue = GetByte();

	if (ReturnValue < 0)
		return ReturnValue;
	else if (ReturnValue == ACK)
		return NOERROR;
	else
		return BADRESPONSE;
}

/**************************************************************************
*
* .b
* Procedure Name : RS232_SendDataPacket
*
* Abstract :
*
*	Send a data packet to a Serial Port
*
* INPUTS :
*
*   Globals :
*				CommID
*
*   Constants :
*				MOTOROLA_SOM
*				ASYNC_SOM
*				NOERROR
*
*	Procedure Parameters :
*				Header_t 	*DataPacket
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*
**************************************************************************/
INT16 RS232_SendDataPacket(Header_t *DataPacket)
{
	INT16	ReturnValue;
	INT16	CommType;
	UINT16	ByteCounter;

	CommType = SendSOM();

	if (CommType < 0)
		return CommType;

	/* Read in the header, 2 bytes at a time */
	for (ByteCounter = 0; ByteCounter < sizeof(Header_t); ByteCounter += 2)
	{
		switch(CommType)
		{
		case MOTOROLA_SOM:
			/* Switch the bytes here */
			ReturnValue = SendByte(	*((_TUCHAR *)DataPacket + ByteCounter + 1));
			ReturnValue = SendByte(	*((_TUCHAR *)DataPacket + ByteCounter));
			break;

		case ASYNC_SOM:
			ReturnValue = TransmitCommChar(	(HANDLE)CommID, *((_TUCHAR *)DataPacket + ByteCounter));
			ReturnValue = TransmitCommChar(	(HANDLE)CommID, *((_TUCHAR *)DataPacket + ByteCounter + 1));
			break;

		default:
			ReturnValue = SendByte(	*((_TUCHAR *)DataPacket + ByteCounter));
			ReturnValue = SendByte(	*((_TUCHAR *)DataPacket + ByteCounter + 1));
			break;
		}
		if (ReturnValue)
			return ReturnValue;
	}

	/* Read in the rest of the packet */
	for (; ByteCounter < DataPacket->PacketLength; ByteCounter++)
	{
		switch(CommType)
		{
		case ASYNC_SOM:
			ReturnValue = TransmitCommChar(	(HANDLE)CommID, *((_TUCHAR *)DataPacket + ByteCounter));
			break;

		default:
			ReturnValue = SendByte(	*((_TUCHAR *)DataPacket + ByteCounter));
			break;
		}
		if (ReturnValue) 
			return ReturnValue;
	}

	return NOERROR;
}


/**************************************************************************
*
* .b
* Procedure Name : RS232_GetDataPacket
*
* Abstract :
*
*	Get a data packet from a Serial Port
*
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				BADRESPONSE
*				TIMEOUT
*				MOTOROLA_SOM
*				NOERROR
*
*	Procedure Parameters :
*				Header_t 	*DataPacket
*
* OUTPUTS :
*
*   Globals :
*				GlobalSOM
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*
**************************************************************************/
INT16 RS232_GetDataPacket(Header_t *DataPacket)
{
	UINT16	ByteCounter;
	INT16	ReturnValue;

	GlobalSOM = GetSOM();
	if (GlobalSOM < 0 || GlobalSOM == BADRESPONSE)
		return TIMEOUT;

	for (ByteCounter = 0; ByteCounter < sizeof(Header_t); ByteCounter += 2)
	{
		switch(GlobalSOM)
		{
		case MOTOROLA_SOM:
			ReturnValue = GetByte();
			*((_TUCHAR *)(DataPacket) + ByteCounter + 1) = (_TUCHAR)ReturnValue;

			ReturnValue = GetByte();
			*((_TUCHAR *)(DataPacket) + ByteCounter) = (_TUCHAR)ReturnValue;
			break;

		default:
			ReturnValue = GetByte();
			*((_TUCHAR *)(DataPacket) + ByteCounter) = (_TUCHAR)ReturnValue;

			ReturnValue = GetByte();
			*((_TUCHAR *)(DataPacket) + ByteCounter + 1) = (_TUCHAR)ReturnValue;
			break;
		}
		if (ReturnValue < 0) 
			return ReturnValue;
	}

	for (; ByteCounter < DataPacket->PacketLength; ByteCounter++)
	{
		ReturnValue = GetByte();
		if (ReturnValue < 0)
			return ReturnValue;

		*((_TUCHAR *)(DataPacket) + ByteCounter) = (_TUCHAR)ReturnValue;
	}

	return NOERROR;
}

#if MOBILEPTU
/**************************************************************************
*
* .b
* Procedure Name : TCPIP_GetDataPacket
*
* Abstract :
*
*	Get a data packet from a Socket
*
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				NULL
*				NAK
*				BADRESPONSE
*				NOERROR
*
*	Procedure Parameters :
*				Header_t 	*DataPacket
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*
**************************************************************************/
INT16 TCPIP_GetDataPacket(Header_t *DataPacket)
{
	INT16 	ReturnCode;
	UINT16 	ByteCounter;

	if (DataPacket == NULL)
	{

		ReturnCode = RPTUClient_GetDataPacket((void *)LocalPacket, &ByteCounter);

		if (ReturnCode)	
			return ReturnCode;

		if (ByteCounter && ((Header_t *)LocalPacket)->ResponseType == NAK)
			return BADRESPONSE;
	}
	else
	{

		ReturnCode = RPTUClient_GetDataPacket((void *)DataPacket, &ByteCounter);
		if (ReturnCode)
			return ReturnCode;

		if (ByteCounter && DataPacket->ResponseType == NAK)
			return BADRESPONSE;
	}

	return NOERROR;
}
#endif

/**************************************************************************
*
* .b
* Procedure Name : GetByte
*
* Abstract :
*
*	Read a Byte from the current Comm Port
*
*
* INPUTS :
*
*   Globals :
*				CommID
*
*   Constants :
*				EV_RXCHAR
*				TIMEOUTCONST
*				TIMEOUT
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
*				a valid byte or TIMEOUT if FAILED
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*			11/06/97		Dev Pradhan @ Adtranz
*			- Changed the timeout to be time-based rather than a
*			  counter.
*
**************************************************************************/
INT16 GetByte()
{
	INT16			ReturnValue=0;
	BYTE			ReceivedByte=0;
	time_t			timestart, timecurrent;
	DWORD			OneByte = 1;	//added variable 
	unsigned long	errorNumber=0;
	DWORD			numBytes=0;
	LPDWORD			lpnumBytes=NULL;

	lpnumBytes = &numBytes;
	time( &timestart );
	do
	{
		if (EventMask)
		{
			ReturnValue = ReadFile((HANDLE)CommID, &ReceivedByte, OneByte, lpnumBytes, NULL);
			errorNumber = GetLastError();

			if (ReturnValue == 0)
			{
				ReturnValue = (INT16)GetLastError();
				return ReturnValue;
			}
		}
		else
		{
			ReturnValue = 0;
		}

		time( &timecurrent );
	}
	while ((timecurrent < timestart + TIMEOUTCONST) && (ReturnValue != 1));

	if (ReturnValue != 1)
		return TIMEOUT;
	else
		return ReceivedByte;
}

/**************************************************************************
*
* .
* Procedure Name : SendByte
*
* Abstract :
*
*	Send a Byte to the current Comm Port and check to see if it is sent
*	back
*
* INPUTS :
*
*   Globals :
*				CommID
*
*   Constants :
*				BADRESPONSE
*				NOERROR
*
*	Procedure Parameters :
*				INT16		PassedByte
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*
**************************************************************************/
INT16 SendByte(INT16 PassedByte)
{
	INT16	ReturnValue;

	ReturnValue = TransmitCommChar((HANDLE)CommID, (_TCHAR)PassedByte);

	ReturnValue = GetByte();
	if (ReturnValue < 0)
		return ReturnValue;
	else if (ReturnValue != PassedByte)
		return BADRESPONSE;
	else
		return NOERROR;
}


/**************************************************************************
*
* .b
* Procedure Name : SendSOM
*
* Abstract :
*
*	Send Start Of Message Byte to the current Comm Port
*
* INPUTS :
*
*   Globals :
*				CommID
*
*   Constants :
*				SYNC_SOM
*				ASYNC_SOM
*				MOTOROLA_SOM
*				BADRESPONSE
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
*				Error Code if FAILED else Start Of Message byte
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*
**************************************************************************/
INT16 SendSOM()
{
	INT16		ReturnValue;
	ReturnValue = TransmitCommChar((HANDLE)CommID, (_TCHAR)SYNC_SOM);

	ReturnValue = GetByte();
	if (ReturnValue < 0)
		return ReturnValue;
	else if ( (ReturnValue == SYNC_SOM)  ||
		(ReturnValue == ASYNC_SOM) ||
		(ReturnValue == MOTOROLA_SOM) )
		return ReturnValue;
	else
		return BADRESPONSE;
}


/**************************************************************************
*
* .b
* Procedure Name : GetSOM
*
* Abstract :
*
*	Check for Start Of Message Byte on the current Comm Port
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SYNC_SOM
*				ASYNC_SOM
*				MOTOROLA_SOM
*				BADRESPONSE
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
*				Error Code if FAILED else Start Of Message Byte
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : ??/??/??		Jeff Lyon @ AEG		Created
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Added SMRS
*
**************************************************************************/
INT16 GetSOM()
{
	INT16	ReturnValue;

	ReturnValue = GetByte();

	if (ReturnValue < 0)
		return ReturnValue;
	else if ( (ReturnValue == SYNC_SOM)  ||
		(ReturnValue == ASYNC_SOM) ||
		(ReturnValue == MOTOROLA_SOM) )
	{
		GlobalSOM = ReturnValue;
		return ReturnValue;
	}
	else
		return BADRESPONSE;
}
