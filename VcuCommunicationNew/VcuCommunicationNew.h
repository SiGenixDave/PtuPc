// VcuCommunicationNew.h : main header file for the VcuCommunicationNew DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CVcuCommunicationNewApp
// See VcuCommunicationNew.cpp for the implementation of this class
//

class CVcuCommunicationNewApp : public CWinApp
{
public:
	CVcuCommunicationNewApp();

// Overrides
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
