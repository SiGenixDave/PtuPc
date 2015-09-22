/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:	Windows PTU
*
*   File Name:	VcuCommunication.h - Main header file for the VcuCommunication project.
*
*   SubSystem:	VcuCommunication
*
*   Procedures:	<n/a>
*
*   EPROM Drawing:
*
* .b
*
*	History:        07/20/11	Version 1.0.1	Sean.D @ Bombardier Transportation
*					1.	First entry into TortoiseSVN configuration management.
*					2.	Added Version.rc.
*					3.	Imported into Visual Studio 2011.
*
*   Revised:		08/25/11	Version 1.2.	K.McD @ Bombardier Transportation
*					1.	Modified comm.h. Added a conditional compilation symbol to the WATCHSIZE definition.
*					2.	Modified the read and write communication port timeouts in function InitCommPort( ... ).
*					3.	Optimized the layout of a number of files for use with the Visual Studio editor.
*
*	Revised:		08/30/11	Version 1.3		Sean.D @ Bombardier Transportation
*					1.	Reverted project to earlier version due to issues in communication since the debug symbols were added
*					2.	Made changes to restore version and generation of the PDB file.
*
*	Revised:		09/21/11	Version 1.4		Sean.D @ Bombardier Transportation
*					1.	Changed RPTUClient_InitializeSockets in clntsock.cpp to properly handle URIs
*					2.	Moved definition of WATCHSIZE from comm.h to comm.cpp
*					3.	Changed CloseCommunication in comm.cpp to return the correct error code for success or failure for Ethernet.
*					4.	Modified SetWatchElements in comm.cpp to use new templated SetWatchElementsReq_t templated structure with values based off of WATCHSIZE
*					5.	Modified UpdateWatchElements in commp.cpp to use new templated UpdateWatchElementsRes_t templated structure with values based off of WATCHSIZE
*					6.	Added SetWatchSize function in comm.cpp to set WATCHSIZE remotely.
*					7.	Set up in comm.h SetWatchElementsReq_t, UpdateWatchElementsRes_t, GetWatchValuesReq_t, and GetWatchValuesRes_t (the latter two are unused)
*					8.	Added SetWatchSize entry point to ptudll32.def
*
*	Revised:		10/05/11	Version 1.4.1	Sean.D @ Bombardier Transportation
*					1.	Minor changes in cpp files, running Tabify and Format on them to clean up some anomalous spacing.
*					2.	Replaced calls to to _tcscpy in primcomm.cpp and event.cpp to refer to _tcscpy_s, a more secure version which checks for buffer overflows.*
*					3.	Replaced calls to to _tcsnset in comm.cpp to refer to _tcsnset_s, a more secure version which checks for buffer overflows*.
*					4.	Replaced calls to to _tcsncpy in comm.cpp to refer to _tcsncpy_s, a more secure version which checks for buffer overflows.
*					5.	Replaced calls to to sprintf in comm.cpp to refer to sprintf_s, a more secure version which checks for buffer overflows.
*					6.	Replaced calls to to strncat in comm.cpp to refer to strncat_s, a more secure version which checks for buffer overflows.
*					7.	Replaced calls to to fopen in event.cpp to refer to fopen_s, a more secure version which better handles error values.
*					8.	Replaced calls to to filelength and fileno in event.cpp to refer to _filelength and _fileno, more secure versions.
*					9.	Removed deprecated DEFINITION statement in ptudll32.def.
*
*	Revised:		03/26/15	Version 2.0		K.McD @ Bombardier Transportation
*					References
*					1.	Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
*						4800010525-CU2/19.03.2015.
*  
*						1.  Changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
*                           to support both 32 and 64 bit architecture.
*                                          
*						2.  Changes to allow the PTU to handle both 2 and 4 character year codes.
*
*					Modifications
*					1.	Modified comm.h. Rev. 2.0.
*					2.	Modified comm.cpp. Rev. 2.0.
*					3.	Modified VcuCommunication32.def, VcuCommunication64.def. Removed the Set4TimeDate() and Get4TimeDate() methods from the list
*						of exports.
*****************************************************************************/

#if !defined(AFX_VcuCommunication_H__F144058B_1DAB_11D3_9B9B_0020AFEDFB1E__INCLUDED_)
	#define AFX_VcuCommunication_H__F144058B_1DAB_11D3_9B9B_0020AFEDFB1E__INCLUDED_

#if _MSC_VER >= 1000
	#pragma once
#endif // _MSC_VER >= 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

	#include "Resource.h"		// main symbols

	const int RS232 =	0;

	/////////////////////////////////////////////////////////////////////////////
	// CVcuCommunicationApp
	// See VcuCommunication.cpp for the implementation of this class
	//

	class CVcuCommunicationApp : public CWinApp
	{
	public:
		CVcuCommunicationApp();

		// Overrides
		// ClassWizard generated virtual function overrides
		//{{AFX_VIRTUAL(CPtudll32App)
		public:
		virtual BOOL InitInstance();
		//}}AFX_VIRTUAL

		//{{AFX_MSG(CPtudll32App)
			// NOTE - the ClassWizard will add and remove member functions here.
			//    DO NOT EDIT what you see in these blocks of generated code !
		//}}AFX_MSG
		DECLARE_MESSAGE_MAP()
	};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_Ptudll32_H__F144058B_1DAB_11D3_9B9B_0020AFEDFB1E__INCLUDED_)
