/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:	Windows PTU
*
*   File Name:	sockets.h
*
*   SubSystem:	PTUDLL
*
*   Procedures:	<n/a>
*
*   EPROM Drawing:
*
* .b
*
*   History:        ??/??/??    Creation of Version 1.0         ddp
*   Revised:		06/09/99	Ported into VC++.
*****************************************************************************/
#define	NOERROR			0
#define	UNKNOWNERROR	(-100)
#define	ACCEPTERROR		(-101)
#define	NOREQUESTS		(-200)
#define RPTU1			20000/tcp
#define RPTU2			20000/tcp

INT16 HandleError(void);
