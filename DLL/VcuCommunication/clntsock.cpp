/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:	Windows PTU
*
*   File Name:	clntsock.cpp
*
*   SubSystem:	PTUDLL.DLL
*
*   Procedures:
*				RPTUClient_InitializeSockets
*				RPTUClient_TerminateSocket
*				RPTUClient_GetDataPacket
*				RPTUClient_SendDataPacket
*				HandleError
*
*   EPROM Drawing:
*
* .b
*
*   History:        ??/??/??    Creation of Version 1.0         ddp
*	Revised:		05/10/96	Dev Pradhan @ Adtranz
*					- Added SMRS's.
*					07/01/99	Jason O'Connor @ Adtranz
*					- Ported to Visual C++ 5.0.
*					- Added include of stdafx.h for VC++ compiling   
*					- Removed Vbapi.h and updated include of winsock.h. 	
*					- Replaced char with TCHAR for UNICODE/ANSI compatability.
*
*****************************************************************************/
#include "stdafx.h"
#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include "selftest.h"
#include "comm.h"
#include "sockets.h"
#include "clntsock.h"
#include <errno.h>

#if MOBILEPTU

SOCKET  ClientSocket;

bool m_bBlockedReceiveData;
bool m_bSBlockedCheckSOM;
bool m_bBlockedCheckSendSOM;
bool m_bBlockedACKEtherent;
INT16 m_ReturnCode;
INT16 m_ReturnSOMCode;
INT16 m_ReturnSendSOMCode;
INT16 m_ReturnACKCode;
UINT16	*ByteCounter1=NULL;
void *Packet1=NULL;

UINT ReceiveData(LPVOID pParam);
UINT CheckGetSOM(LPVOID pParam);
UINT CheckSendSOM(LPVOID pParam);
UINT CheckACKEtherent(LPVOID pParam);

char Rvalue;
char RSendSOMvalue;
char RxAckValue;


/**************************************************************************
*
* .b
* Procedure Name : RPTUClient_InitializeSockets
*
* Abstract :
*
*	Initializes the TCP\IP sockets for communication and then
*	starts a conversation with the server.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				PF_INET
*				SOCK_STREAM
*				INVALID_SOCKET
*				NOERROR
*
*	Procedure Parameters :
*				LPSTR 	DestIp
*
* OUTPUTS :
*
*   Globals :
*				ClientSocket
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
*	- Calls WSAStartup() [winsock] to initialise winsock (setup stack etc.)
*	- Creates a TCP socket using socket() [winsock]
*	- Calls connect() [winsock] to hook into the Server
*
* .b
*
* History : ??/??/??		Dev Pradhan @ Adtranz - Created
* Revised : 05/10/96		Dev Pradhan @ Adtranz
*			- Added SMRS
* Revised : 04/01/09		Vijay Kumar Bhoga @ BTECI
- Constants are used in place of ReturnCode.
- Changed sin_port value to htons(5001);
**************************************************************************/
INT16 RPTUClient_InitializeSockets(LPSTR DestIp)
{
	WORD			VersionRequired;
	WSADATA			WinSockData;
	SOCKADDR_IN		ServiceAddress;
	INT16				ReturnCode;

	VersionRequired = 0x0101;

	if (WSAStartup(VersionRequired, &WinSockData))
		return HandleError();

	/*	Create a socket for outgoing and incoming requests.	*/
	/*  PF_INET		-	TCP\IP connection					*/
	/*	SOCK_STREAM	-	Create a TCP\IP socket				*/
	ClientSocket = socket(PF_INET, SOCK_STREAM, 0);
	if (ClientSocket == INVALID_SOCKET)
		return HandleError();

	struct addrinfo *result = NULL;
	ReturnCode = getaddrinfo (DestIp, "5001", 0, &result);

	if (0 == ReturnCode)
	{
		struct sockaddr_in  *sockaddr_ipv4;
		sockaddr_ipv4 = (struct sockaddr_in *) result->ai_addr;

		/*	Connect the request socket to the RPTU service.		*/
		ServiceAddress.sin_family			= PF_INET;
		ServiceAddress.sin_port				= htons(5001);
		ServiceAddress.sin_addr				= sockaddr_ipv4->sin_addr;

		struct hostent* host = gethostbyname(DestIp);

		if (connect( ClientSocket,
			(const struct sockaddr *)&ServiceAddress,
			sizeof(ServiceAddress) ))
			return HandleError();


		return NOERROR;
	}
	else
	{
		return HandleError();
	}
}


/**************************************************************************
*
* .b
* Procedure Name : RPTUClient_TerminateSocket
*
* Abstract :
*
*	Terminates open sockets and performs a cleanup
*
* INPUTS :
*
*   Globals :
*				ClientSocket
*
*   Constants :
*				NOSENDRECEIVE
*				NOERROR
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
*				NOERROR - 0
*
* Functional Description :
*
*	- Calls shutdown() [winsock] to stop sending and receiving on the
*	  Client Socket
*	- Calls closesocket() [winsock] to destroy the Client Socket
*
* .b
*
* History : ??/??/??		Dev Pradhan @ Adtranz - Created
* Revised : 05/10/96		Dev Pradhan @ Adtranz
*			- Added SMRS
**************************************************************************/
INT16 RPTUClient_TerminateSocket(void)
{
	INT16 returnValue;

	/*	Destroy the send and receive possibility on this scocket.	*/
	returnValue = shutdown(ClientSocket, SD_SEND);
	// We ignore the return value as it sometimes returns a value saying that it's in the process of shutting down.

	/*	Destroy the passed socket.	*/
	returnValue = closesocket(ClientSocket);

	// We currently have no scenario where executing these two steps does not solve our problem.
	return NOERROR;
}


/**************************************************************************
*
* .b
* Procedure Name : RPTUClient_SendDataPacket
*
* Abstract :
*
*	Sends a data packet to the server.
*
* INPUTS :
*
*   Globals :
*				ClientSocket
*
*   Constants :
*				None
*
*	Procedure Parameters :
*				Header_t *DataPacket
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
*	- Calls send() [winsock] and passes the packet into the
*	  Client Socket for theServer to receive.
*
* .b
*
* History : ??/??/??		Dev Pradhan @ Adtranz - Created
* Revised : 05/10/96		Dev Pradhan @ Adtranz
*			- Added SMRS
* Revised : 04/01/09		Vijay Kumar Bhoga @ BTECI
*			- Moved MAPINT handling from SendDataPacket to RPTUClient_SendDataPacket.
*			- Procedure Parameter type is changed from void * to Header_t*.
*			06/25/09		BTECI - Handled recieve code within a Thread
* Revised : 07/21/11		Sean Duggan @ BTNA
*			- Changed RPTUClient_InitializeSockets to properly handle URIs 
**************************************************************************************/
INT16 RPTUClient_SendDataPacket(Header_t *DataPacket)
{
	INT16	ReturnCode;
	INT16	PacketSize;
	INT16 Count=0;

	GlobalSOM = RPTUClient_SendSOM();

	//GlobalSOM = MOTOROLA_SOM;


	PacketSize = DataPacket->PacketLength;
	DataPacket->PacketLength = MAPINT(DataPacket->PacketLength);
	DataPacket->ResponseType = MAPINT(DataPacket->ResponseType);
	DataPacket->CheckSum = MAPINT(DataPacket->CheckSum);
	DataPacket->PacketType = MAPINT(DataPacket->PacketType);

	ReturnCode = send(	ClientSocket,
		(const TCHAR *)DataPacket,
		PacketSize,
		0 );



	if (ReturnCode < 0)
		return HandleError();

	return NOERROR;
}
/**************************************************************************
*
* .b
* Procedure Name : GetACKEthernet
*
* Abstract :
*
*	Read an Acknowledgment from Ethernet.
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
* Revised : 04/01/09		Vijay Kumar Bhoga @ BTECI - Created.
*		   : 06/25/09		BTECI - Handled recieve code within a Thread .
*
**************************************************************************/
INT16 GetACKEthernet()
{
	//	INT16	ReturnValue;
	char RxValue;
	/* BTECI */
	DWORD iStatus; 
	unsigned long lpExitCode=0; 
	int j=0; 
	unsigned long PreviousTime=0;
	unsigned long CurrentTime=0;
	CWinThread *objThread; 
	SYSTEMTIME systime;
	CString Sec="";


	m_ReturnACKCode=0;
	m_bBlockedACKEtherent=true;

	//Create Thread
	objThread = AfxBeginThread(CheckACKEtherent,&RxValue,THREAD_PRIORITY_NORMAL,0,0,NULL);
	if (objThread->m_hThread == 0)
	{
		//		AfxMessageBox("Error in Thread Creation");
	}

	//Check if Thread gets data within 5 seconds
	GetLocalTime(&systime);
	PreviousTime= ((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
	GetLocalTime(&systime);
	CurrentTime=((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
	while (abs((signed long)(PreviousTime - CurrentTime)) != 5)
	{
		GetLocalTime(&systime);
		CurrentTime=((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
		if (m_bBlockedACKEtherent == false)
		{
			break;
		}
	}


	//If Thread blocked then terminate it otherwsie return status
	if (m_bBlockedACKEtherent == false)
	{
		RxValue=RxAckValue;
		if (m_ReturnACKCode < 0)
			return m_ReturnACKCode;
		else if ((int)RxValue == ACK)
			return NOERROR;
		else
			return BADRESPONSE;
	}
	else
	{


		iStatus=GetExitCodeThread(objThread->m_hThread,&lpExitCode);
		TerminateThread(objThread->m_hThread,lpExitCode);

		return TIMEOUT;
	}
	/*ReturnValue = recv(ClientSocket,&RxValue,1,0);

	if (ReturnValue < 0)
	return ReturnValue;
	else if ((int)RxValue == ACK)
	return NOERROR;
	else
	return BADRESPONSE;*/
}

/**************************************************************************
*
* .b
* Procedure Name : CheckACKEtherent
*
* Abstract :
*
*	Read an Acknowledgment from Ethernet.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*			 
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
*				return 0
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* Revised : 06/25/09		BTECI - Created.
*
**************************************************************************/
UINT CheckACKEtherent(LPVOID pParam)
{
	m_bBlockedACKEtherent=true;
	m_ReturnACKCode = recv(ClientSocket,(char*)&pParam,1,0);
	RxAckValue=(char)pParam;
	m_bBlockedACKEtherent=false;
	return 0;
}

/**************************************************************************
*
* .b
* Procedure Name : RPTUClient_GetDataPacket
*
* Abstract :
*
*	Gets a data packet from the server.
*
* INPUTS :
*
*   Globals :
*				ClientSocket
*
*   Constants :
*				SOCKET_ERROR
*				NOERROR
*
*	Procedure Parameters :
*				void 			*Packet
*				UINT16	*ByteCounter
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
*	- Calls recv() [winsock] and retrieves the packet from the Client
*	  Socket from the Server.
*
* .b
*
* History : ??/??/??		Dev Pradhan @ Adtranz - Created
* Revised : 05/10/96		Dev Pradhan @ Adtranz
*			- Added SMRS
* Revised : 04/01/09		Vijay Kumar Bhoga @ BTECI
- Added a Variable "pCktSize".
- While Loop Condition is changed.
*			06/25/09		BTECI - Handled recieve code within a Thread
**************************************************************************/
INT16	RPTUClient_GetDataPacket(void *Packet, UINT16	*ByteCounter)
{
	//	INT16	ReturnCode;
	//	INT16	pCktSize;
	/*BTECI*/
	DWORD iStatus; 
	unsigned long lpExitCode=0; 
	int j=0; 
	unsigned long PreviousTime=0;
	unsigned long CurrentTime=0;
	CWinThread *objThread=NULL; 
	SYSTEMTIME systime;
	CString Sec="";
	INT16 iTimeOut=0;

	/*	Initialize the byte counter. */


	GlobalSOM =  RPTUClient_GetSOM();

	if (GlobalSOM < 0 || GlobalSOM == BADRESPONSE)/*BTECI*/
		return TIMEOUT;/*BTECI*/


	ByteCounter1=ByteCounter;/*BTECI*/
	*ByteCounter1=0;/*BTECI*/
	Packet1=Packet;/*BTECI*/

	/*BTECI*/
	m_bBlockedReceiveData=true;
	objThread= AfxBeginThread(ReceiveData,NULL,THREAD_PRIORITY_NORMAL,0,0,NULL);
	if (objThread->m_hThread == 0)
	{
		//		AfxMessageBox("Error in Thread Creation");
	}


	GetLocalTime(&systime);
	PreviousTime= ((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
	GetLocalTime(&systime);
	CurrentTime=((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;



	iTimeOut=5;


	while (abs((signed long)(PreviousTime - CurrentTime)) <= iTimeOut)
	{
		GetLocalTime(&systime);
		CurrentTime=((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
		if (m_bBlockedReceiveData == false)
		{
			break;
		}
	}



	if (m_bBlockedReceiveData == false)
	{
		Packet=Packet1;
		objThread=NULL;
		ByteCounter1=NULL;
		Packet1 = NULL;
		if (m_ReturnCode == SOCKET_ERROR)
		{
			return HandleError();
		}
		else if (m_ReturnCode = 0) 
		{
			return NOERROR;
		}

	}
	else
	{
		iStatus=GetExitCodeThread(objThread->m_hThread,&lpExitCode);
		TerminateThread(objThread->m_hThread,lpExitCode);
		ByteCounter1=NULL;
		Packet1 = NULL;
		objThread=NULL;
		return TIMEOUT;
	}


	//BTECI Commented following code and placed logic within a thread
	/*	Check to see if any bytes are pending. */
	/*	ReturnCode = recv( ClientSocket, (char*)Packet, sizeof(Header_t), 0 );

	if (ReturnCode == SOCKET_ERROR)
	return HandleError();


	if (ReturnCode == 0) return NOERROR;*/

	/*	Save the number of bytes received.	*/
	//*ByteCounter = ReturnCode;



	/*	Get the packet header.	*/
	/*while (*ByteCounter < sizeof(Header_t))
	{
	ReturnCode = recv(	ClientSocket,
	((TCHAR *)Packet + *ByteCounter),
	(sizeof(Header_t) - *ByteCounter),
	0 );

	if (ReturnCode == SOCKET_ERROR)
	return HandleError();
	else
	*ByteCounter += ReturnCode;
	}

	pCktSize = MAPINT(((Header_t*)Packet)->PacketLength);*/


	/*	Get the rest of the packet.	*/
	/*while (*ByteCounter < pCktSize && *ByteCounter !=1)
	{


	ReturnCode = recv(	ClientSocket,((TCHAR *)Packet + *ByteCounter), (pCktSize- *ByteCounter),0	);		
	if (ReturnCode == SOCKET_ERROR)
	return HandleError();
	else
	*ByteCounter +=ReturnCode;
	}*/
	return NOERROR;
}


/**************************************************************************
*
* .b
* Procedure Name : ReceiveData
*
* Abstract :
*
*	Gets a data packet from the server.
*
* INPUTS :
*
*   Globals :
*				ClientSocket
*
*   Constants :
*				 
*
*	Procedure Parameters :
*			 
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*			return 0
*
* Functional Description :
*
*
*
* History : 06/26/09		BTECI - Created
* 
**************************************************************************/
UINT ReceiveData(LPVOID pParam)
{


	INT16	pCktSize;

	*ByteCounter1 = 0;
	m_bBlockedReceiveData=true;


	m_ReturnCode = recv(ClientSocket,(char*)Packet1,sizeof(Header_t),0);


	if (m_ReturnCode != SOCKET_ERROR)

	{	

		if (m_ReturnCode != 0)
		{
			/*	Save the number of bytes received.	*/
			*ByteCounter1 = m_ReturnCode;


			/*	Get the packet header.	*/
			while (*ByteCounter1 < sizeof(Header_t))
			{
				m_ReturnCode = recv(	ClientSocket,
					((TCHAR *)Packet1 + *ByteCounter1),
					(sizeof(Header_t) - *ByteCounter1),
					0 );

				if (m_ReturnCode == SOCKET_ERROR)
					return 0;
				else
					*ByteCounter1 += m_ReturnCode;
			}

			pCktSize = MAPINT(((Header_t*)Packet1)->PacketLength);


			/*	Get the rest of the packet.	*/
			while (*ByteCounter1 < pCktSize && *ByteCounter1 !=1)
			{


				m_ReturnCode = recv(ClientSocket,((TCHAR *)Packet1 + *ByteCounter1), (pCktSize- *ByteCounter1),0	);		

				if (m_ReturnCode == SOCKET_ERROR)
					return 0;
				else
					*ByteCounter1 +=m_ReturnCode;
			}	 

		}
	}	 

	m_bBlockedReceiveData=false;
	//	Packet1 = pParam;
	//	pParam = NULL;

	return 0;
} 
/**************************************************************************
*
* .b
* Procedure Name : HandleError
*
* Abstract :
*
*	Returns the Winsock Error Code.
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
*	Note: Used for debugging purposes only.
*
* .b
*
* History : ??/??/??		Dev Pradhan @ Adtranz - Created
* Revised : 05/10/96		Dev Pradhan @ Adtranz
*			- Added SMRS
**************************************************************************/
INT16 HandleError(void)
{

	INT16 error;
	INT16 ReturnCode;
	error = WSAGetLastError();

	ReturnCode = RPTUClient_TerminateSocket();

	return error;
}


/**************************************************************************
*
* .b
* Procedure Name : RPTUClient_SendSOM
*
* Abstract :
*
*	Send Start Of Message Byte to the current Comm Port
*
* INPUTS :
*
*   Globals :
*				ClientSocket
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
* History : 05/28/09		Shajahan S M @ BTECI	Created
*
**************************************************************************/
INT16 RPTUClient_SendSOM()
{
	INT16 ReturnValue;
	char RxValue;
	char som =SYNC_SOM;
	/*BTECI*/
	DWORD iStatus; 
	unsigned long lpExitCode=0; 
	int j=0; 
	unsigned long PreviousTime=0;
	unsigned long CurrentTime=0;
	CWinThread *objThread=NULL; 
	SYSTEMTIME systime;
	CString Sec="";
	INT16 iTimeout=0;


	ReturnValue = send(ClientSocket,
		&som,
		1,
		0 );

	if (ReturnValue < 0)
		return ReturnValue;


	m_ReturnSendSOMCode=0;
	m_bBlockedCheckSendSOM=true;

	//Create Thread
	objThread = AfxBeginThread(CheckSendSOM,&RxValue,THREAD_PRIORITY_NORMAL,0,0,NULL);
	if (objThread->m_hThread == 0)
	{
		//		AfxMessageBox("Error in Thread Creation");
	}


	iTimeout=5;

	//Check if Thread gets data within 5 seconds
	GetLocalTime(&systime);
	PreviousTime= ((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
	GetLocalTime(&systime);
	CurrentTime=((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
	while (abs((signed long)(PreviousTime - CurrentTime)) <= iTimeout)
	{
		GetLocalTime(&systime);
		CurrentTime=((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
		if (m_bBlockedCheckSendSOM == false)
		{
			break;
		}
	}

	//If Thread blocked then terminate it otherwsie return status
	if (m_bBlockedCheckSendSOM == false)
	{
		RxValue=RSendSOMvalue;
		objThread=NULL;
		if (m_ReturnSendSOMCode < 0)
			return m_ReturnSendSOMCode;

		else if ( (RxValue == SYNC_SOM)  ||
			(RxValue == ASYNC_SOM) ||
			(RxValue == MOTOROLA_SOM) )
			return (INT16)RxValue;
		else
			return BADRESPONSE;
	}
	else
	{


		iStatus=GetExitCodeThread(objThread->m_hThread,&lpExitCode);
		TerminateThread(objThread->m_hThread,lpExitCode);

		return TIMEOUT;
	}
	//	}

	/*	ReturnValue = recv(ClientSocket,&RxValue,1,0);
	if (ReturnValue < 0)
	return ReturnValue;
	else if ( (RxValue == SYNC_SOM)  ||
	(RxValue == ASYNC_SOM) ||
	(RxValue == MOTOROLA_SOM) )
	return (INT16)RxValue;
	else
	return BADRESPONSE;*/
}

/**************************************************************************
*
* .b
* Procedure Name : CheckSendSOM
*
* Abstract :
*
*	gets response for send SOM
*
* INPUTS :
*
*   Globals :
*				ClientSocket
*
*   Constants :
*				 
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
*			return 0
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : 06/26/09		BTECI	Created
*
**************************************************************************/

UINT CheckSendSOM(LPVOID pParam)
{
	m_bBlockedCheckSendSOM=true;
	RSendSOMvalue=0;

	m_ReturnSendSOMCode = recv(ClientSocket,(char*)&pParam,1,0);

	RSendSOMvalue=(char)pParam;
	m_bBlockedCheckSendSOM=false;
	pParam=NULL;
	return 0;
}

/**************************************************************************
*
* .b
* Procedure Name : RPTUClient_GetSOM
*
* Abstract :
*
*	Check for Start Of Message Byte on the current Comm Port
*
* INPUTS :
*
*   Globals :
*				ClientSocket
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
* History : 05/28/09		Shajahan S M @ BTECI	Created
*			06/25/09		BTECI - Handled recieve code within a Thread
**************************************************************************/
INT16 RPTUClient_GetSOM()
{
	//	INT16	ReturnValue;
	char RxValue;

	/*BTECI*/
	DWORD iStatus; 
	unsigned long lpExitCode=0; 
	int j=0; 
	unsigned long PreviousTime=0;
	unsigned long CurrentTime=0;
	CWinThread *objThread=NULL; 
	SYSTEMTIME systime;
	CString Sec="";
	int iTimeOut=0;



	m_ReturnSOMCode=0;
	m_bSBlockedCheckSOM=true;

	//Create Thread
	objThread = AfxBeginThread(CheckGetSOM,&RxValue,THREAD_PRIORITY_NORMAL,0,0,NULL);
	if (objThread->m_hThread == 0)
	{
		//		AfxMessageBox("Error in Thread Creation");
	}


	iTimeOut=5;

	//Check if Thread gets data within 5 seconds
	GetLocalTime(&systime);
	PreviousTime= ((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
	GetLocalTime(&systime);
	CurrentTime=((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
	while (abs((signed long)(PreviousTime - CurrentTime)) <= iTimeOut)
	{
		GetLocalTime(&systime);
		CurrentTime=((systime.wHour * 60 + systime.wMinute) * 60) + systime.wSecond;
		if (m_bSBlockedCheckSOM == false)
		{
			break;
		}
	}


	//If Thread blocked then terminate it otherwsie return status
	if (m_bSBlockedCheckSOM == false)
	{
		RxValue=Rvalue;
		objThread=NULL;
		if (m_ReturnSOMCode < 0)
			return m_ReturnSOMCode;
		else if ( (RxValue == SYNC_SOM)  ||
			(RxValue == ASYNC_SOM) ||
			(RxValue == MOTOROLA_SOM) )
		{
			return (INT16)RxValue;
		}
		else
			return BADRESPONSE;
	}
	else
	{


		iStatus=GetExitCodeThread(objThread->m_hThread,&lpExitCode);
		TerminateThread(objThread->m_hThread,lpExitCode);
		objThread=NULL;
		return TIMEOUT;
	}

	/*ReturnValue = recv(ClientSocket,&RxValue,1,0);

	if (ReturnValue < 0)
	return ReturnValue;
	else if ( (RxValue == SYNC_SOM)  ||
	(RxValue == ASYNC_SOM) ||
	(RxValue == MOTOROLA_SOM) )
	{
	return (INT16)RxValue;
	}
	else
	return BADRESPONSE;*/
}


/**************************************************************************
*
* .b
* Procedure Name : CheckGetSOM
*
* Abstract :
*
*	Check for Start Of Message Byte on the current Comm Port
*
* INPUTS :
*
*   Globals :
*				ClientSocket
*
*   Constants :
*				 
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
*				return 0
*
*
* Functional Description :
*
*	See Abstract
*
* .b
*
* History : 06/25/09		BTECI	Created
*
**************************************************************************/
UINT CheckGetSOM(LPVOID pParam)
{
	m_bSBlockedCheckSOM=true;
	Rvalue=0;
	m_ReturnSOMCode = recv(ClientSocket,(char*)&pParam,1,0);
	Rvalue=(char)pParam;
	m_bSBlockedCheckSOM=false;
	pParam=NULL;
	return 0;
}

#endif
