/*****************************************************************************
*
*   Project:	Windows PTU
*
*   File Name:	Ptudll32.cpp
*
*   SubSystem:	PTUDLL
*
*   Procedures/Classes:
*
*   EPROM Drawing:
*
*	History:	7/01/99		Created		Jason O'Connor @ Adtranz
*
***********************************************************************/

#include "stdafx.h"
#include "VcuCommunication.h"
#include <windows.h>
#include <winsock.h>
#include <stdlib.h>
#include <string.h>
#include "selftest.h"
#include "comm.h"
#include "clntsock.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

//
//	Note!
//
//		If this DLL is dynamically linked against the MFC
//		DLLs, any functions exported from this DLL which
//		call into MFC must have the AFX_MANAGE_STATE macro
//		added at the very beginning of the function.
//
//		For example:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// normal function body here
//		}
//
//		It is very important that this macro appear in each
//		function, prior to any calls into MFC.  This means that
//		it must appear as the first statement within the 
//		function, even before any object variable declarations
//		as their constructors may generate calls into the MFC
//		DLL.
//
//		Please see MFC Technical Notes 33 and 58 for additional
//		details.
//

/////////////////////////////////////////////////////////////////////////////
// CVcuCommunicationApp

BEGIN_MESSAGE_MAP(CVcuCommunicationApp, CWinApp)
	//{{AFX_MSG_MAP(CPtudll32App)
	// NOTE - the ClassWizard will add and remove mapping macros here.
	//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CVcuCommunicationApp construction

CVcuCommunicationApp::CVcuCommunicationApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CVcuCommunicationApp object

CVcuCommunicationApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CVcuCommunicationApp initialization

BOOL CVcuCommunicationApp::InitInstance()
{
	if (!AfxSocketInit())
	{
		AfxMessageBox(IDP_SOCKETS_INIT_FAILED);
		return FALSE;
	}

	// Register all OLE server (factories) as running.  This enables the
	//  OLE libraries to create objects from other applications.
	COleObjectFactory::RegisterAll();

	return TRUE;
}

/////////////////////////////////////////////////////////////////////////////
// Special entry points required for inproc servers

STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	return AfxDllGetClassObject(rclsid, riid, ppv);
}

STDAPI DllCanUnloadNow(void)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	return AfxDllCanUnloadNow();
}

// by exporting DllRegisterServer, you can use regsvr.exe
STDAPI DllRegisterServer(void)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	COleObjectFactory::UpdateRegistryAll();
	return S_OK;
}
