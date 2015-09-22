/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:	Windows PTU
*
*   File Name:	clntsock.h
*
*   SubSystem:	PTUDLL
*
*   Procedures:	<n/a>
*
*   EPROM Drawing:
*
* .b
*
*   History:	11/17/95    Creation of Version 1.0		ddp
*   Revised:	03/14/96	ddp
*				- Added defines.
*				06/08/99	Ported in to VC++.
*****************************************************************************/

#if MOBILEPTU
/* Defines */
#define NORECEIVE					0
#define NOSEND						1
#define NOSENDRECEIVE				2


/* Prototypes */
INT16 RPTUClient_InitializeSockets	(LPSTR);
INT16 RPTUClient_SendDataPacket		(Header_t *);
INT16 RPTUClient_GetDataPacket		(void *, UINT16 *);
INT16 RPTUClient_TerminateSocket	(void);
INT16 RPTUClient_SendSOM();
INT16 RPTUClient_GetSOM();
#endif
