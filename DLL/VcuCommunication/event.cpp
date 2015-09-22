/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:	Windows PTU
*
*   File Name:	event.cpp
*
*   SubSystem:	PTUDLL
*
*   Procedures:
*				LoadFaultlog
*				CheckFaultlogger
*				GetFaultHdr
*				GetFaultVar
*				GetFltFlagInfo
*				GetFltHistInfo
*				FreeEventLogMemory
*				GetFaultData
*				SetFaultFlags
*				GetStreamInformation
*				GetStream
*				ClearEvent
*				InitializeEventLog
*				GetDefaultStreamInformation
*				SetDefaultStreamInformation
*				GetEventLog
*				ChangeEventLog
*				ExitEventLog
*				ReadCMSFaultData
*
*   EPROM Drawing:
*
* .b
*
*  History: ??/??/??    	Creation of Version 1.0         ddp
*          	10/06/95    	DDP @ ATSI
*          	- Modified to handle multiple fault and data logs.
*			01/10/96		DDP @ Adtranz
*			- Fixed bug in Receive_Data().
*			01/29/96		DDP @ Adtranz
*			- Modified to handle upto 256 Datalog Variables.
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS.
*			05/06/97		Dev Pradhan @ Adtranz
*			- Modified MapFltVarVal().
*			10/31/97		Dev Pradhan @ Adtranz
*			- Modified MapFltVarVal().
*			11/11/97		Dev Pradhan @ Adtranz
*			- Modified MapFltVarVal().
*			03/24/99		Dev Pradhan @ Adtranz
*			- Fixed problem where if the number of faults uploaded from target 
*			  exceeded MAXNUMOFFAULTS the FaultStartLocation array would get 
*			  full and then go out of bounds. The solution was to dynamically 
*			  allocate more memory in chunks of MAXNUMOFFAULTS * 2 bytes. 
*			  Functions affected are ...
*				. LoadFaultlog()
*				. CheckFaultlogger()
*				. GetFaultHdr()
*				. GetFaultVar().
*			07/01/99		Jason O'Connor @ Adtranz
*			- Ported to Visual C++ 5.0
*			  - Added extern "C" to function declarations for export.
*			  - Removed Vbapi.h .
*			  - Modified Vbapi.h associated data types in GetFaultHdr.
*			  - Replaced char with TCHAR (_T macro) for UNICODE/ANSI 
*				compatability.
*			11/08/13		Keith McDonald @ Northern Software Engineering
*			  -	Replaced the calls to SysReAllocString() in GetFaultHdr() with calls to _bstr_t( ... ).copy() as there were problems 
*				associated with using SysReAllocString when the BSTR types were marshalled through unmanaged C#. This came about as a result of 
*				trying to get the C# managed PTU to run under 64 bit Windows 7.
*			  - included <comdef.h> to support the _bstr_t class.
*
*****************************************************************************/

/* Includes */
#include	"stdafx.h"
#include    <windows.h>
#include    <string.h>
#include	<io.h>
#include	<stdio.h>
#include    "selftest.h"
#include	"event.h"
#include    "comm.h"
#include	<comdef.h>

/* Local Prototypes */
INT16 GetFaultData		(unsigned short, unsigned long, INT16 *);
INT16 MapFltVarVal		(INT16, INT16 *, unsigned long *, double *);
INT16	VerifyTime		(INT16, INT16, INT16);
INT16	VerifyDate		(INT16, INT16, INT16);
INT16	SetFaultLog     (INT16);
INT16	GetFaultIndices (unsigned long *, unsigned long *);
INT16 RetrieveData		(_TUCHAR *Source, long 	Dest [], UINT16 	SourceSize, UINT16 	NoVariables, 
	UINT16 	NoSamples, INT16 	VariableType []	);	

/* Globals */
INT16					CurrentNumberOfFaults;
long					GlobalIndex;
GLOBALHANDLE			hFBuf;
GLOBALHANDLE			hFaultStartLocation;
INT16					LocationSize;

static GetDatalogBufferRes_t	DatalogResponse;

/**************************************************************************
*
* .b
* Procedure Name : LoadFaultlog
*
* Abstract :
*
*	Gets Fault Data and puts them into the Fault Descriptor Table
*	which keeps them in memory.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				NOERROR
*				NULL
*				DISABLE
*				MAXFAULTSIZE
*				FALSE
*				ENABLE
*
*	Procedure Parameters :
*				LPSTR				FileName
*				INT16				*NumberOfFaults
*				unsigned long		*OldestIndex
*				unsigned long		*NewestIndex
*
* OUTPUTS :
*
*   Globals :
*				CurrentNumberOfFaults
*				hFDT
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
* .b
*
* History : 7/29/93         Dev Pradhan @ AEG
* Revised : 8/31/95			Dev Pradhan @ ATSI
*			- Changed so that if we get corrupt data in the fault buffer we ignore it and continue on, instead of stopping !!!
*			12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			09/10/98		Dev Pradhan @ Adtranz
*			- Fixed bug with the recording of the start location in the FaultBuffer. The GlobalIndex variable was not being updated
*			  correctly.
*			03/24/99		Dev Pradhan @ Adtranz
*			- Fixed for situation where more than MAXNUMOFFAULTS are uploaded from target.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for VC++ 5.0.
*
**************************************************************************/
extern "C" INT16 WINAPI LoadFaultlog(	INT16					*NumberOfFaults,
	unsigned long	*OldestIndex,
	unsigned long	*NewestIndex	)
{
	INT16					FaultSize;
	INT16					ReturnCode;
	INT16			 		Index;
	unsigned long		RemoteFaults;
	unsigned long		FaultCounter;
	INT16					BufferSize;
	_TUCHAR		*FaultBuffer;
	INT16					*FaultStartLocation;

	/* LOOP ONCE ... EXIT ON ERROR */
	do
	{
		CurrentNumberOfFaults	= 0;
		GlobalIndex				= 0;
		ReturnCode 				= NOERROR;
		hFBuf 					= NULL;
		FaultCounter 			= 0;
		LocationSize			= MAXNUMOFFAULTS * 2;
		hFaultStartLocation		= GlobalAlloc(GMEM_MOVEABLE | GMEM_ZEROINIT,
			LocationSize);
		if (hFaultStartLocation == NULL)
		{
			ReturnCode = E_MEM_ALLOC;
			break;
		}
		FaultStartLocation = (INT16 *)GlobalLock(hFaultStartLocation);

		/* Disable Fault Logging */
		if (ReturnCode = SetFaultLog(DISABLE)) break;

		/* Get Fault Log Indices */
		if (ReturnCode = GetFaultIndices(OldestIndex, NewestIndex))
			break;

		/* Check if  Fault Log is Empty */
		if ( (*OldestIndex == 0xffffffff) &&
			(*NewestIndex == 0xffffffff) )
		{
			*NumberOfFaults = 0;
			break;
		}

#ifndef DAS_FIX
		if (*NewestIndex < *OldestIndex)
		{
			RemoteFaults = 0x10000 + *NewestIndex - *OldestIndex + 1;
		}
		else
		{
			RemoteFaults = *NewestIndex - *OldestIndex + 1;
		}

		if (RemoteFaults == 0)
		{
			break;
		}

#else

		/* Compute number of Faults */
		if ((RemoteFaults = (*NewestIndex - *OldestIndex + 1)) == 0)
			break;
#endif
		/* GetFaultData can only get a max of MAXFAULTBUFFERSIZE bytes of */
		/* Data. So if there are more than faults in the Fault Log than */
		/* this it has do this several times to get ALL the fault data */
		do
		{
			/* Get the Fault Data */
			if (ReturnCode = GetFaultData(*OldestIndex + FaultCounter,
				RemoteFaults - FaultCounter,
				&BufferSize))
				break;

			if (BufferSize == 0) break;

			FaultBuffer = (_TUCHAR *)GlobalLock(hFBuf);

			/* Loop thru the fault buffer, pulling out the size and data */
			/* for each fault */
			for (Index = 0; Index < BufferSize;)
			{
				FaultCounter++;

				/* Get the size of the next fault */
				FaultSize = MAPINT(*(INT16 *)&FaultBuffer[Index + GlobalIndex]);

				/* Add the fault to the FDT */
				if (FaultSize < MAXFAULTSIZE && FaultSize > 0)
				{
					FaultStartLocation[CurrentNumberOfFaults] = Index + (INT16)GlobalIndex;
 					if (++CurrentNumberOfFaults >= (LocationSize / 2))
					{
						GlobalUnlock(hFaultStartLocation);
						LocationSize += (MAXNUMOFFAULTS * 2);
						hFaultStartLocation = GlobalReAlloc(hFaultStartLocation,
							LocationSize,
							GMEM_MOVEABLE|GMEM_ZEROINIT);
						FaultStartLocation = (INT16 *)GlobalLock(hFaultStartLocation);
					}
				}
				else
				{
					/* Fault Buffer is corrupt beyond hope at this point */
					ReturnCode = E_FLT_CORRUPT;
					break;
				}

				/* Increment the Index to point to the size of the next fault */
				Index += (FaultSize + 2);
			}

			/* Increment the Global Index to maintain the size of the buffer */
			GlobalIndex += Index;

		} while ((FaultCounter < RemoteFaults) && (ReturnCode == NOERROR));

		/* Force the Return Code so we can extract all valid faults */
		ReturnCode = NOERROR;

		/* Save the number of good faults we retrieved */
		*NumberOfFaults = CurrentNumberOfFaults;

	} while (FALSE);

	/* Enable Fault Logging here in case we left the while loop early */
	SetFaultLog(ENABLE);

	if (hFBuf != NULL) GlobalUnlock(hFBuf);
	if (hFaultStartLocation != NULL) GlobalUnlock(hFaultStartLocation);

	return ReturnCode;
}


/**************************************************************************
*
* .b
* Procedure Name : CheckFaultLogger
*
* Abstract :
*
*	Checks for new Faults
*
* INPUTS :
*
*   Globals :
*				CurrentNumberOfFaults
*
*   Constants :
*				NOERROR
*				NULL
*				DISABLE
*				MAXFAULTSIZE
*				FALSE
*				ENABLE
*
*	Procedure Parameters :
*				LPSTR				FileName
*				INT16					*PassedNumOfFaults
*				unsigned long		*orig_new
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
* .b
*
* History : 8/11/93         Dev Pradhan @ AEG
* Revised : 8/31/95			Dev Pradhan @ ATSI
*			- Changed so that if we get corrupt data in the fault buffer
*			  we ignore it and continue on, instead of stopping !!!
*			12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			03/24/99		Dev Pradhan @ Adtranz
*			- Fixed for situation where more than MAXNUMOFFAULTS
*			  are uploaded from target.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
**************************************************************************/
extern "C" INT16 WINAPI CheckFaultlogger( INT16				*PassedNumOfFaults,
	unsigned long *orig_new )
{
	INT16					NewFaultBufferSize;
	INT16					FaultSize;
	unsigned long		OldestIndex;
	unsigned long		NewestIndex;
	INT16					RemoteFaults;
	INT16					Index;
	unsigned long		FaultIndex;
	INT16					ReturnCode;
	_TUCHAR		*FaultBuffer;
	INT16					*FaultStartLocation;

	/* LOOP ONCE ... EXIT ON ERROR */
	do
	{
		if (ReturnCode = SetFaultLog(DISABLE)) break;

		if (ReturnCode = GetFaultIndices(&OldestIndex, &NewestIndex))
			break;

		if (*orig_new == -1)
			FaultIndex = OldestIndex;
		else
			FaultIndex = *orig_new + 1;

		/* Check if Fault Log is Empty */
		if ( (NewestIndex == 0xFFFFFFFF) &&
			(OldestIndex == 0xFFFFFFFF) )
		{
			RemoteFaults = 0;
			break;
		}

		/* Compute number of Faults */
		if ((RemoteFaults = (INT16)(NewestIndex - FaultIndex + 1)) == 0)
			break;

		/* Get ALL the Fault Data */
		if (ReturnCode = GetFaultData(	FaultIndex,
			RemoteFaults,
			&NewFaultBufferSize ) )
			break;

		/* Enable Fault Logging */
		SetFaultLog(ENABLE);

		if (NewFaultBufferSize == 0) break;

		FaultBuffer = (_TUCHAR *)GlobalLock(hFBuf);
		FaultStartLocation = (INT16 *)GlobalLock(hFaultStartLocation);

		/* Loop thru the fault buffer, pulling out the size and data */
		/* for each fault */
		for (Index = 0; Index < NewFaultBufferSize;)
		{
			/* Get the size of the next fault */
			FaultSize = MAPINT(*(INT16 *)&FaultBuffer[Index + GlobalIndex]);

			/* Add the fault to the FDT */
			if (FaultSize < MAXFAULTSIZE && FaultSize > 0)
			{
				FaultStartLocation[CurrentNumberOfFaults] = Index + (INT16)GlobalIndex;
				if (++CurrentNumberOfFaults >= (LocationSize / 2))
				{
					GlobalUnlock(hFaultStartLocation);
					LocationSize += (MAXNUMOFFAULTS * 2);
					hFaultStartLocation = GlobalReAlloc(hFaultStartLocation,
						LocationSize,
						GMEM_MOVEABLE|GMEM_ZEROINIT);
					FaultStartLocation = (INT16 *)GlobalLock(hFaultStartLocation);
				}
			}
			else
			{
				break;
			}

			/* Increment the Index to point to the size of the next fault */
			Index += (FaultSize + 2);
		}

		/* Update Position of Next Available Location in Fault Buffer */
		GlobalIndex += Index;

		/* Force the Return Code so we can extract all valid faults */
		ReturnCode = NOERROR;

	} while (FALSE);

	/* Enable Fault Logging here in case we left the while loop early */
	SetFaultLog(ENABLE);

	if (ReturnCode == NOERROR && RemoteFaults > 0)
	{
		*orig_new			= NewestIndex;
		*PassedNumOfFaults	= CurrentNumberOfFaults;
	}

	if (hFBuf != NULL) GlobalUnlock(hFBuf);
	if (hFaultStartLocation != NULL) GlobalUnlock(hFaultStartLocation);

	return ReturnCode;
}


/**************************************************************************
*
* .b
* Procedure Name : GetFaultHdr
*
* Abstract :
*
* INPUTS :
*
*   Globals :
*				CurrentNumberOfFaults
*				FaultStartLocation
*				FaultBuffer
*
*   Constants :
*				NOERROR
*				NULL
*				E_FDT_FLT_INDEX
*
*	Procedure Parameters :
*				INT16		index
*				INT16		*faultnum
*				BSTR	Flttime
*				BSTR	Fltdate
*				INT16		*datalognum
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
* .b
*
* History : 8/4/93			Dev Pradhan @ AEG
* Revised :
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			03/24/99		Dev Pradhan @ Adtranz
*			- Fixed for situation where more than MAXNUMOFFAULTS
*			  are uploaded from target.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Replaced VBSetHlstr() with SysReAllocString() and
*			  changed parameter types from HLSTR to *BSTR
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*			11/08/13		Keith McDoonald @ Northern Software Engineering
*			- Replaced the calls to SysReAllocString() with calls to _bstr_t( ... ).copy() as there were problems 
*				associated with using SysReAllocString when the BSTR types were marshalled through unmanaged C#. 
*				This came about as a result of trying to get the C# managed PTU to run under 64 bit Windows 7.
**************************************************************************/
extern "C" INT16 WINAPI GetFaultHdr(INT16	index,
	INT16			*faultnum,
	INT16			*tasknum,
	BSTR			*Flttime,
	BSTR			*Fltdate,
	INT16			*datalognum )
{
	struct flthdr	FaultHeader;
	_TCHAR			TempString[30];
	_TUCHAR			*FaultBuffer;
	INT16			*FaultStartLocation;

	/* Check the Validity of the desired index */
	if (index >= CurrentNumberOfFaults)
	{
		_tcscpy_s(TempString, "N/A");
		SysReAllocString(Flttime, (BSTR)TempString);
		SysReAllocString(Fltdate, (BSTR)TempString);
		*datalognum = -1;
		*faultnum 	= 0;
		*tasknum	= 0;
		return E_FDT_FLT_INDEX;
	}

	/* Retrieve the Header of this Fault */
	FaultBuffer = (_TUCHAR *)GlobalLock(hFBuf);
	FaultStartLocation = (INT16 *)GlobalLock(hFaultStartLocation);

	//changed from outdated _fmemmove (parameters the same)
	memcpy((_TCHAR *)&FaultHeader,
			&FaultBuffer[FaultStartLocation[index] + 2],
			sizeof(struct flthdr) );

	GlobalUnlock(hFBuf);
	GlobalUnlock(hFaultStartLocation);

	/* Check Time */
	if (VerifyTime(	(INT16)FaultHeader.datetime.hr,
					(INT16)FaultHeader.datetime.min,
					(INT16)FaultHeader.datetime.sec))
	{
		wsprintf((_TCHAR *)TempString, "%.2d:%.2d:%.2d",
				 (INT16)FaultHeader.datetime.hr,
				 (INT16)FaultHeader.datetime.min,
				 (INT16)FaultHeader.datetime.sec);
	}
	else
	{
		_tcscpy_s(TempString, "N/A");
	}

	// The Flttime and Fltdate output parameters must be passed as a BSTR type for them 
	// to be accessible in managed C#. Used the '_bstr_t()' approach as there were problems 
	// associated with SysReAllocString().

	//VBSetHlstr(&Flttime, TempString, strlen(TempString));
	//SysReAllocString(Flttime, (BSTR)TempString);
	*Flttime = _bstr_t(TempString).copy();

	/* Check Date */
	if (VerifyDate(	(INT16)FaultHeader.datetime.month,
					(INT16)FaultHeader.datetime.day,
					(INT16)FaultHeader.datetime.year))
	{
		wsprintf(	(_TCHAR *)TempString,
		"%.2d/%.2d/%.2d",
		(INT16)FaultHeader.datetime.month,
		(INT16)FaultHeader.datetime.day,
		(INT16)FaultHeader.datetime.year );
	}
	else
	{
		_tcscpy_s(TempString, "N/A");
	}

	//SysReAllocString(Fltdate, (BSTR)TempString);
	*Fltdate = _bstr_t(TempString).copy();
	*datalognum = MAPINT((INT16)FaultHeader.datalognum);
	*faultnum 	= MAPINT((INT16)FaultHeader.faultnum);
	*tasknum	= MAPINT((INT16)FaultHeader.tasknum);

	TRACE("datalognum = %d\r\n", &datalognum); 

	return NOERROR;
}


/**************************************************************************
*
* .b
* Procedure Name : GetFaultVar
*
* Abstract :
*
* INPUTS :
*
*   Globals :
*				CurrentNumberOfFaults
*				FaultStartLocation
*				FaultBuffer
*
*   Constants :
*				NOERROR
*				E_FDT_FLT_INDEX
*
*	Procedure Parameters :
*				INT16			FaultIndex
*				INT16			VariableNumber
*				INT16			VariableType
*				double		*VariableValue
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
* .b
*
* History : 8/4/93         Dev Pradhan @ AEG
* Revised :
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			03/24/99		Dev Pradhan @ Adtranz
*			- Fixed for situation where more than MAXNUMOFFAULTS
*			  are uploaded from target.
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
**************************************************************************/
extern "C" INT16 WINAPI GetFaultVar(	INT16		FaultIndex,
	INT16	NumberOfVariables,
	INT16		*VariableType,
	double 	*VariableValue	)
{
	INT16					ReturnCode;
	unsigned long	 	*Dataptr;
	INT16					DataLocation;
	_TUCHAR		*FaultBuffer;
	INT16					*FaultStartLocation;

	/* Check the Validity of the desired index */
	if (FaultIndex >= CurrentNumberOfFaults)
		return E_FDT_FLT_INDEX;

	/* Compute the location of the data for this Fault */
	FaultStartLocation = (INT16 *)GlobalLock(hFaultStartLocation);
	DataLocation  = FaultStartLocation[FaultIndex];
	GlobalUnlock(hFaultStartLocation);

	DataLocation += 2;									/* size */
	DataLocation += sizeof(struct flthdr);              /* header */

	/* Validate location */
	if (DataLocation > (INT16)GlobalIndex) return E_FDT_FLT_INDEX;

	/* Get a pointer to the data .. long */
	FaultBuffer = (_TUCHAR *)GlobalLock(hFBuf);

	Dataptr = (unsigned long *)&FaultBuffer[DataLocation];

	GlobalUnlock(hFBuf);

	/* Based on Variable Type retrieve the data */
	ReturnCode = MapFltVarVal(	NumberOfVariables,
		VariableType,
		Dataptr,
		VariableValue );

	return ReturnCode;
}


/**************************************************************************
*
* .b
* Procedure Name : GetFltFlagInfo
*
* Abstract :
*
*	Retrieve the Fault Flags from the Logic
*
* INPUTS :
*
*   Globals :
*
*   Constants :
*				GET_FAULT_FLAG
*				NOERROR
*				GET_STREAM_FLAG
*
*	Procedure Parameters :
*				INT16		*EnableFlag
*				INT16		*TriggerFlag
*				INT16		EntryCount
*
* OUTPUTS :
*
*   Globals :
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
* .b
*
* History : 8/6/93			Dev Pradhan @ AEG
* Revised :
*			12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
**************************************************************************/
extern "C" INT16 WINAPI GetFltFlagInfo(	INT16		*Valid,
	INT16		*EnableFlag,
	INT16		*TriggerFlag,
	INT16		EntryCount		)
{
	INT16					ReturnCode;
	INT16					Index;
	INT16					NumberOfEntries;
	INT16					NumberOfWords;
	INT16					Counter;
	Header_t			Request;
	GetFaultFlagRes_t	Response1;
	GetStreamFlagRes_t	Response2;
	UINT16 		mask;
	UINT16        temp_response1;
	UINT16       temp_response2;

	/* Get Event Flags */
	Request.PacketType		= GET_FAULT_FLAG;
	Request.PacketLength	= sizeof(Header_t);

	ReturnCode = Transaction(&Request, (Header_t *)&Response1);
	if (ReturnCode != NOERROR) return ReturnCode;
	NumberOfWords = MAPINT(Response1.BufferSize) / 2;

	/* Get Stream Flags */
	Request.PacketType		= GET_STREAM_FLAG;
	Request.PacketLength	= sizeof(Header_t);

	ReturnCode = Transaction(&Request, (Header_t *)&Response2);
	if (ReturnCode != NOERROR) return ReturnCode;

	/* Loop thru all the TaskId/FaultId Combinations and */
	/* set/reset a bit for each one */
	for (	Counter = 0, NumberOfEntries = 0, mask = 0x0001;
		NumberOfEntries < EntryCount;
		NumberOfEntries++	)
	{
		Index = NumberOfEntries / 16;
		temp_response1 = MAPINT(Response1.EnableFlag[Index]);
		temp_response2 = MAPINT(Response2.DatalogFlag[Index]);
		if ((Index < NumberOfWords) && Valid[NumberOfEntries])
		{
			EnableFlag[Counter] =
				(temp_response1 & mask) ? 1 : 0;
			TriggerFlag[Counter++] =
				(temp_response2 & mask) ? 1 : 0;
		}
		if (mask == 0x8000) mask = 0x0001;
		else mask = mask << 1;
	}

	return NOERROR;
}


/**************************************************************************
*
* .b
* Procedure Name : GetFltHistInfo
*
* Abstract :
*
*	Retrieve the Fault History from the Logic
*
* INPUTS :
*
*   Globals :
*
*   Constants :
*				GET_FAULT_HISTORY
*				NOERROR
*
*	Procedure Parameters :
*				INT16	 	*logid
*				INT16		*faultid
*				INT16		*StaticHistory
*				INT16		*DynamicHistory
*				INT16		EntryCount
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
* .b
*
* History : 8/11/93         Dev Pradhan @ AEG
* Revised :
*			12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
**************************************************************************/
extern "C" INT16 WINAPI GetFltHistInfo(	INT16		*Valid,
	INT16		*StaticHistory,
	INT16		*DynamicHistory,
	INT16		MaxTasks,
	INT16		MaxEventsPerTask	)
{
	INT16						NumberOfEntries;
	INT16						ReturnCode;
	GetFaultHistoryReq_t	Request;
	GetFaultHistoryRes_t	Response;
	INT16						TaskId;
	INT16						EventId;
	INT16						DynamicTemp[1000];

	NumberOfEntries = 0;

	/* Loop thru all the legal TaskId/FaultId Combinations and */
	/* pull the histories for each combination */
	for (	TaskId = 0;
		TaskId < MaxTasks;
		TaskId++	)
	{
		for (	EventId = 0;
			EventId < MaxEventsPerTask;
			EventId++	)
		{
			if (Valid[(TaskId * MaxEventsPerTask) + EventId])
			{
				Request.PacketType		= GET_FAULT_HISTORY;
				Request.PacketLength	= sizeof(GetFaultHistoryReq_t);
				Request.TaskID			= MAPINT(TaskId);
				Request.FaultID			= MAPINT(EventId);

				ReturnCode = Transaction((Header_t *)&Request, (Header_t *)&Response);
				if (ReturnCode != NOERROR) return ReturnCode;

				StaticHistory[NumberOfEntries]	= MAPINT(Response.StaticHistory);
				DynamicHistory[NumberOfEntries]	= MAPINT(Response.DynamicHistory);
				DynamicTemp[NumberOfEntries]	= DynamicHistory[NumberOfEntries];
				NumberOfEntries++;
			}
		}
	}

	return NOERROR;
}


/***************************************************************************
*
* .b
* Procedure Name : FreeEventLogMemory
*
* Abstract :
*
*	Release all memory being used to store Fault Structure information
*
* INPUTS :
*
*   Globals :
*				hFDT
*				CurrentNumberOfFaults
*
*   Constants :
*				NOERROR
*				NULL
*
*	Procedure Parameters :
*
* OUTPUTS :
*
*   Globals :
*				CurrentNumberOfFaults
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
* .b
*
* History : 1/29/93         Ramesh Vasu @ AEG Westinghouse
* Revised : 8/11/93         Dev Pradhan @ AEG
*			10/10/94		Jeffrey Lyon @ AEG
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI FreeEventLogMemory()
{
	CurrentNumberOfFaults = 0;

	if (hFBuf != NULL)
	{
		GlobalUnlock(hFBuf);
		GlobalFree(hFBuf);
		hFBuf = NULL;
	}

	return NOERROR;
}


/***************************************************************************
*
* .b
* Procedure Name : GetFaultData
*
* Abstract :
*
*	Retrieve the Fault Data for a given Fault Index
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GMEM_MOVEABLE
*				GMEM_ZEROINIT
*				E_MEM_ALLOC
*				GET_FAULT_DATA
*				NOERROR
*				MAXFAULTBUFFERSIZE
*				E_FLT_CORRUPT
*
*	Procedure Parameters :
*				unsigned long 	FaultIndex
*				unsigned long 	NumberOfFaults
*				_TUCHAR 	*Buffer
*				INT16 			*BufferSize
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
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			01/17/97		Dev Pradhan @ Adtranz
*			- Changed so that an error is NOT returned when the BufferSize
*			  is zero.
*
****************************************************************************/
#ifndef DAS_FIX
INT16 GetFaultData(	unsigned short 	FaultIndex,
#else
INT16 GetFaultData(	unsigned long 	FaultIndex,
#endif
	unsigned long 	NumberOfFaults,
	INT16 			*BufferSize )
{
	INT16						ReturnValue;
	GetFaultDataReq_t		Request;
	GetFaultDataRes_t		Response;
	INT16						Counter;
	_TUCHAR			*FaultBuffer;

	Request.PacketType		= GET_FAULT_DATA;
	Request.PacketLength	= sizeof(GetFaultDataReq_t);
	Request.FaultIndex		= MAPLONG(FaultIndex);
	Request.NumberOfFaults	= MAPINT((UINT16)NumberOfFaults);

	ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);
	if (ReturnValue == NOERROR)
	{
		/* Save Buffer Size */
		*BufferSize = MAPINT(Response.BufferSize);

		/* Check for corrupt Data */
		if (*BufferSize >= MAXFAULTBUFFERSIZE)
			return E_FLT_CORRUPT;

		/* Check for corrupt Data */
		if (*BufferSize == 0)
			return NOERROR;

		/* Allocate an area for these Faults */
		if (hFBuf == NULL)
			hFBuf = GlobalAlloc( GMEM_MOVEABLE | GMEM_ZEROINIT, *BufferSize);
		else
			hFBuf = GlobalReAlloc( hFBuf, GlobalIndex + *BufferSize,
			GMEM_MOVEABLE | GMEM_ZEROINIT );

		if (hFBuf == NULL) return E_MEM_ALLOC;
		FaultBuffer = (_TUCHAR *)GlobalLock(hFBuf);

		/* Load the Fault Buffer with the latest Faults */
		for (Counter = 0; Counter < *BufferSize; Counter++)
		{
			FaultBuffer[GlobalIndex + Counter] = Response.Buffer[Counter];
		}

		GlobalUnlock(hFBuf);
	}

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name : SetFaultFlags
*
* Abstract :
*
*	Set the Fault Flags for a specific fault
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SET_FAULT_FLAG
*				NULL
*
*	Procedure Parameters :
*				INT16		TaskNumber
*				INT16		FaultNumber
*				INT16		EnableFlag
*				INT16		DatalogFlag
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
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI SetFaultFlags(	INT16	TaskNumber,
	INT16	FaultNumber,
	INT16	EnableFlag,
	INT16	DatalogFlag		)
{
	INT16					ReturnValue;
	SetFaultFlagReq_t	Request;

	Request.PacketType		= SET_FAULT_FLAG;
	Request.PacketLength	= sizeof(SetFaultFlagReq_t);
	Request.TaskID			= MAPINT(TaskNumber);
	Request.FaultID			= MAPINT(FaultNumber);
	Request.EnableFlag		= (_TUCHAR)EnableFlag;
	Request.DatalogFlag		= (_TUCHAR)DatalogFlag;

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name : GetStreamInformation
*
* Abstract :
*
*	Retrieves the Stream Configuration information for a particular
*	Stream Number from the Logic
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_STREAM_INFORMATION
*				MAXDLVARIABLES
*				NOERROR
*
*	Procedure Parameters :
*				INT16 		StreamNumber
*				INT16 		*NumberOfVariables
*				INT16 		*NumberOfSamples
*				INT16			*SampleRate
*				INT16 		*VariableIndex
*				INT16 		*VariableType
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
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 11/01/95		Dev Pradhan @ ATSI
*			- Added Stream Variable Type information
*			12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			01/29/96		Dev Pradhan @ Adtranz
*			- Added check on Number of Variables
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI GetStreamInformation(	INT16		StreamNumber,
	INT16 	*NumberOfVariables,
	INT16 	*NumberOfSamples,
	INT16		 *SampleRate,
	INT16		VariableIndex[],
	INT16		VariableType[]	)
{
	INT16					ReturnValue;
	INT16					Counter;
	GetStreamInfoReq_t	Request;
	GetStreamInfoRes_t	Response;

	Request.PacketType		= GET_STREAM_INFORMATION;
	Request.PacketLength	= sizeof(GetStreamInfoReq_t);
	Request.StreamNumber	= MAPINT(StreamNumber);

	ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);
	if (ReturnValue == NOERROR)
	{
		*NumberOfVariables	= MAPINT(Response.Information.NumberOfVariables);
		*NumberOfSamples	= MAPINT(Response.Information.NumberOfSamples);
		*SampleRate			= MAPINT(Response.Information.SampleRate);
		if (*NumberOfVariables > MAXDLVARIABLES)
			*NumberOfVariables = MAXDLVARIABLES;
		for (Counter = 0; Counter < *NumberOfVariables; Counter++)
		{
			VariableIndex[Counter] =
				MAPINT(Response.Information.StreamVariableInfo[Counter].StreamVariable);
			VariableType[Counter] =
				MAPINT(Response.Information.StreamVariableInfo[Counter].StreamVariableType);
		}
	}

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name : GetStream
*
* Abstract :
*
*	Retrieve the Stream Data for a particular Stream Number from the Logic
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_DATALOG_BUFFER
*				NOERROR
*
*	Procedure Parameters :
*				INT16			StreamNumber
*				long	 	DatalogBuffer[]
*				INT16 		*TimeOrigin
*				INT16			NumberOfVariables
*				INT16  		NumberOfSamples
*				INT16			VariableType[]
*
* OUTPUTS :
*
*   Globals :
*				DatalogResponse
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 09/13/95		Dev Pradhan @ ATSI
*			- Added Decompression Layer
*			12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI GetStream(	INT16			StreamNumber,
	long	 	DatalogBuffer[],
	INT16			*TimeOrigin,
	INT16		NumberOfVariables,
	INT16  		NumberOfSamples,
	INT16			VariableType[] )
{
	INT16						ReturnValue;
	GetDatalogBufferReq_t   Request;

	Request.PacketType		= GET_DATALOG_BUFFER;
	Request.PacketLength	= sizeof(GetDatalogBufferReq_t);
	Request.DatalogIndex	= MAPINT(StreamNumber);

	ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&DatalogResponse);
	if (ReturnValue == NOERROR)
	{
		/*
		ReturnValue = Decompress((void *)DatalogResponse.DatalogBuffer,
		(void *)DatalogBuffer,
		DatalogResponse.BufferSize,
		NumberOfSamples * NumberOfVariables * sizeof(long));

		_fmemcpy(	DatalogBuffer,
		&DatalogResponse.DatalogBuffer[0],
		DatalogResponse.BufferSize);
		*/

		ReturnValue = RetrieveData( DatalogResponse.DatalogBuffer,
			DatalogBuffer,
			MAPINT(DatalogResponse.BufferSize),
			NumberOfVariables,
			NumberOfSamples,
			VariableType);

		*TimeOrigin = MAPINT(DatalogResponse.TimeOrigin);
	}

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name : ClearEvent
*
* Abstract :
*
*	Clear the current event (fault) log on the logic
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				CLEAR_EVENTLOG
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
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI ClearEvent()
{
	Header_t	Request;

	Request.PacketType		= CLEAR_EVENTLOG;
	Request.PacketLength	= sizeof(Header_t);

	return Transaction(&Request, NULL);
}


/***************************************************************************
*
* .b
* Procedure Name : InitalizeEventLog
*
* Abstract :
*
*	Initialize the current event (fault) log on the logic. This resets the
*	fault indices and resets the flags as well.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				INITIALIZE_EVENTLOG
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
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI InitializeEventLog()
{
	Header_t	Request;

	Request.PacketType		= INITIALIZE_EVENTLOG;
	Request.PacketLength	= sizeof(Header_t);

	return Transaction(&Request, NULL);
}


/***************************************************************************
*
* .b
* Procedure Name : GetDefaultStreamInformation
*
* Abstract :
*
*	Retrieves the Default Stream Configuration information for the Logic
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_DEFAULT_STREAM
*				NOERROR
*				MAXDLVARIABLES
*
*	Procedure Parameters :
*				INT16 		*NumberOfVariables
*				INT16 		*NumberOfSamples
*				INT16  		*SampleRate
*				INT16		 	VariableIndex[]
*				INT16			VariableType[]
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
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			01/29/96		Dev Pradhan @ Adtranz
*			- Added check on Number of Variables
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI GetDefaultStreamInformation(	INT16	*NumberOfVariables,
	INT16		*NumberOfSamples,
	INT16		*SampleRate,
	INT16		VariableIndex[],
	INT16		VariableType[] )
{
	INT16					ReturnValue;
	INT16					Counter;
	GetStreamInfoReq_t	Request;
	GetStreamInfoRes_t	Response;

	Request.PacketType		= GET_DEFAULT_STREAM;
	Request.PacketLength	= sizeof(Header_t);

	ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);
	if (ReturnValue == NOERROR)
	{
		*NumberOfVariables	= MAPINT(Response.Information.NumberOfVariables);
		*NumberOfSamples	= MAPINT(Response.Information.NumberOfSamples);
		*SampleRate			= MAPINT(Response.Information.SampleRate);
		if (*NumberOfVariables > MAXDLVARIABLES)
			*NumberOfVariables = MAXDLVARIABLES;
		for (Counter = 0; Counter < *NumberOfVariables; Counter++)
		{
			VariableIndex[Counter] =
				MAPINT(Response.Information.StreamVariableInfo[Counter].StreamVariable);
			VariableType[Counter] =
				MAPINT(Response.Information.StreamVariableInfo[Counter].StreamVariableType);
		}
	}

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name : SetDefaultStreamInformation
*
* Abstract :
*
*	Sets the Default Stream Configuration information for the Logic
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				MAXDLVARIABLES
*				SET_STREAM_INFORMATION
*				NULL
*
*	Procedure Parameters :
*				INT16			NumberOfVariables
*				INT16			SampleRate
*				INT16 		VariableIndex[]
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
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			01/29/96		Dev Pradhan @ Adtranz
*			- Added check on Number of Variables
*			02/06/96		Dev Pradhan @ Adtranz
*			- Changed packet length computation to work off of
*			  NumberofVariables
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI SetDefaultStreamInformation(	INT16		NumberOfVariables,
	INT16		SampleRate,
	INT16 	VariableIndex[]	)
{
	INT16					Counter;
	SetStreamInfoReq_t	Request;

	if (NumberOfVariables > MAXDLVARIABLES)
		NumberOfVariables = MAXDLVARIABLES;

	Request.PacketType	 = SET_STREAM_INFORMATION;
	Request.PacketLength = sizeof(Header_t) + 6 +
		(NumberOfVariables * sizeof(StreamVariable_t));

	Request.Information.SampleRate 			= MAPINT(SampleRate);
	Request.Information.NumberOfVariables 	= MAPINT(NumberOfVariables);
	Request.Information.NumberOfSamples		= 0;	/* not valid or used */

	for (Counter = 0; Counter < NumberOfVariables; Counter++)
	{
		/* Send the new variable's index */
		Request.Information.StreamVariableInfo[Counter].StreamVariable =
			MAPINT(VariableIndex[Counter]);

		/* This info does not need to be sent */
		Request.Information.StreamVariableInfo[Counter].StreamVariableType = 0;
	}

	return Transaction((Header_t *)&Request, NULL);
}


/***************************************************************************
*
* .b
* Procedure Name : GetEventLog
*
* Abstract :
*
*	Retrieves the Number of the current Event Log on the Logic
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_EVENT_LOG
*				NOERROR
*
*	Procedure Parameters :
*				INT16		 	*CurrentEventLog
*				INT16			*NumberEventLogs
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
* .b
*
* History : 10/09/95		Dev Pradhan @ ATSI
* Revised : 12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI GetEventLog(	INT16 *CurrentEventLog,
	INT16 *NumberEventLogs	)
{
	INT16					ReturnValue;
	Header_t			Request;
	GetEventLogRes_t	Response;

	Request.PacketType		= GET_EVENT_LOG;
	Request.PacketLength	= sizeof(Header_t);

	ReturnValue = Transaction(&Request, (Header_t *)&Response);
	if (ReturnValue == NOERROR)
	{
		*CurrentEventLog = MAPINT(Response.CurrentEventLog);
		*NumberEventLogs = MAPINT(Response.NumberEventLogs);
	}

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name : ChangeEventLog
*
* Abstract :
*
*	Changes the current Event Log on the Logic to the new passed Number
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				CHANGE_EVENT_LOG
*				NOERROR
*
*	Procedure Parameters :
*				INT16 		NewEventLogNumber
*				INT16		 	*DataRecordingRate
*				INT16			*ChangeStatus
*
* OUTPUTS :
*
*   Globals :
*				CurrentMaxTasks
*				CurrentMaxFaultsPerTask
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
* .b
*
* History : 10/06/95		Dev Pradhan @ ATSI
* Revised : 12/05/95		Dev Pradhan @ ATSI
*			- Added Motorola conversion
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI ChangeEventLog(	INT16 	NewEventLogNumber,
	INT16		*DataRecordingRate,
	INT16		*ChangeStatus,
	INT16		*MaxTasks,
	INT16		*MaxEventsPerTask )
{
	INT16					ReturnValue;
	ChangeEventLogReq_t	Request;
	ChangeEventLogRes_t	Response;

	Request.PacketType		= CHANGE_EVENT_LOG;
	Request.PacketLength	= sizeof(ChangeEventLogReq_t);
	Request.NewEventLog		= MAPINT(NewEventLogNumber);

	ReturnValue = Transaction((Header_t *)&Request, (Header_t *)&Response);
	if (ReturnValue == NOERROR)
	{
		*ChangeStatus	 	= MAPINT(Response.ChangeStatus);
		*DataRecordingRate	= MAPINT(Response.DataRecordingRate);
		*MaxTasks	 	 	= MAPINT(Response.MaxTasks);
		*MaxEventsPerTask 	= MAPINT(Response.MaxEvents);

		if (*MaxTasks >= MAXTASKS) *MaxTasks = MAXTASKS - 1;
		if (*MaxEventsPerTask >= MAXTASKS) *MaxEventsPerTask = MAXEVENTS - 1;

	}

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name : ExitEventLog
*
* Abstract :
*
*	Performs a cleanup on all Event Log specific functions
*
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
* .b
*
* History : 10/06/95		Dev Pradhan @ ATSI
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI ExitEventLog()
{
	INT16 ReturnCode;

	ReturnCode = FreeEventLogMemory();

	return NOERROR;
}


/*=========================================================================*/
/*                          LOCAL FUNCTIONS                                */
/*=========================================================================*/

/**************************************************************************
*
* .b
* Procedure Name : VerifyTime
*
* Abstract :
*
*	Validates the time passed in.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				FALSE
*
*	Procedure Parameters :
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				FALSE if FAILED else TRUE
*
* Functional Description :
*
* .b
*
* History : 8/25/93         Dev Pradhan @ AEG
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*
**************************************************************************/
INT16 VerifyTime( INT16 hr, INT16 min, INT16 sec )
{
	if ((hr < 0) || (hr > 23))
		return FALSE;

	if ((min < 0) || (min > 59))
		return FALSE;

	if ((sec < 0) || (sec > 59))
		return FALSE;

	return TRUE;
}


/**************************************************************************
*
* .b
* Procedure Name : VerifyDate
*
* Abstract :
*
*	Validates the date passed in.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				FALSE
*
*	Procedure Parameters :
*				INT16 	month
*				INT16 	day
*				INT16 	year
*
* OUTPUTS :
*
*   Globals :
*				None
*
*	Returned Values :
*				FALSE if FAILED else TRUE
*
* Functional Description :
*
* .b
*
* History : 8/25/93         Dev Pradhan @ AEG
* Revised :	05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*
**************************************************************************/
INT16 VerifyDate( INT16 month,
	INT16 day,
	INT16 year )
{
	if ((month < 1) || (month > 12))
		return FALSE;

	if ((day < 1) || (day > 31))
		return FALSE;

	if ((year < 00) || (year > 99))
		return FALSE;

	return TRUE;
}


/***************************************************************************
*
* .b
* Procedure Name : SetFaultLog
*
* Abstract :
*
*	Commands the Fault Logger on the Logic to the passed in State.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				SET_FAULT_LOG
*				NULL
*
*	Procedure Parameters :
*				INT16		NewState
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
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*
****************************************************************************/
INT16 SetFaultLog(INT16 NewState)
{
	INT16					ReturnValue;
	SetFaultLogReq_t    Request;

	Request.PacketType		= SET_FAULT_LOG;
	Request.PacketLength	= sizeof(SetFaultLogReq_t);
	Request.TargetState		= (_TUCHAR)NewState;

	ReturnValue = Transaction((Header_t *)&Request, NULL);

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name : GetFaultIndices
*
* Abstract :
*
*	Retrieves the oldest and newest indices of the current Fault Log
*	on the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GET_FAULT_INDICES
*				NOERROR
*
*	Procedure Parameters :
*				unsigned long  	*Oldest
*				unsigned long  	*Newest
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
* .b
*
* History : ??/??/??		Jeffrey Lyon @ AEG
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*
****************************************************************************/
INT16 GetFaultIndices(	unsigned long  *Oldest,
	unsigned long  *Newest )
{
	INT16						ReturnValue;
	Header_t				Request;
	GetFaultIndicesRes_t	Response;

	Request.PacketType		= GET_FAULT_INDICES;
	Request.PacketLength	= sizeof(Header_t);

	ReturnValue = Transaction(&Request, (Header_t *)&Response);
	if (ReturnValue == NOERROR)
	{
		*Oldest	= MAPLONG(Response.Oldest);
		*Newest	= MAPLONG(Response.Newest);
	}

	return ReturnValue;
}


/***************************************************************************
*
* .b
* Procedure Name : RetrieveData
*
* Abstract :
*
*	Unpack Stream Data
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				INT_8_TYPE
*				UINT_8_TYPE
*				INT_16_TYPE
*				UINT_16_TYPE
*				INT_32_TYPE
*				UINT_32_TYPE
*				E_STREAM_CORRUPT
*				NOERROR
*
*	Procedure Parameters :
*				Source				Stream Source Buffer
*				Dest				Stream Destination Buffer
*				SourceSize			Stream Source Buffer Size
*				NoVariables			No Variables logged in a Stream
*				NoSamples			No Samples per stream
*				VariableType		Array of the type of each Variable
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
* .b
*
* History : 10/03/95		Dev Pradhan @ ATSI
* Revised : 01/10/96		Dev Pradhan @ Adtranz
*			- Fixed bug with left over byte calculation
*			- Changed error reported to E_STREAM_CORRUPT
*			05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			01/17/97		Dev Pradhan @ Adtranz
*			- Fixed bug with signed variables
*			07/08/99		Jason O'Connor @ Adtranz
*			- Added _T macro to char for compatability
*
****************************************************************************/


INT16 RetrieveData(_TUCHAR *Source,
	long 	Dest [],
	UINT16 	SourceSize,
	UINT16 	NoVariables,
	UINT16 	NoSamples,
	INT16 	VariableType []	)
{
	UINT16		Index;
	UINT16		ByteCount;
	UINT16		DestCount;
	_TUCHAR		*TempPtr;
	UINT32 		templong;
	INT16       tempint;
	_TCHAR      tempchar;

	/* Initialise Counters */
	ByteCount = 0;
	DestCount = 0;

	/* Loop thru Source Buffer */
	while (ByteCount < SourceSize)
	{
		/* Loop thru the variables */
		for (Index = 0; Index < NoVariables; Index++)
		{
			/* Make sure we dont go over destination buffer limits */
			if (DestCount >= NoSamples * NoVariables)
				return E_STREAM_CORRUPT;

			/* Point to current location in Source Buffer */
			TempPtr = (_TUCHAR *)Source + ByteCount;

			/* Grab number of bytes depending on variable type */
			/* Also account for sign of the variable */
			switch (VariableType[Index])
			{
			case INT_8_TYPE :
				tempchar = *(_TCHAR *)TempPtr;
				Dest[DestCount++] =	(INT32)tempchar;
				//		(_TSCHAR)(MAPINT((INT16)*(_TCHAR *)TempPtr));
				ByteCount++;
				break;

			case UINT_8_TYPE :
				Dest[DestCount++] =	(UINT32)(*(_TUCHAR *)TempPtr);
				ByteCount++;
				break;

			case INT_16_TYPE :
				tempint = (*(UINT16 *)TempPtr);
				tempint = (INT16)(MAPINT(tempint));
				templong = (INT32)tempint;
				Dest[DestCount++] =	(INT32)templong;
				ByteCount += 2;
				break;

			case UINT_16_TYPE :
				Dest[DestCount++] =	(UINT32)(MAPINT(*(UINT16 *)TempPtr));
				ByteCount += 2;
				break;

			case INT_32_TYPE :
				templong = MAPLONG(*(UINT32 *)TempPtr);
				Dest[DestCount++] =	(INT32)templong;
				ByteCount += 4;
				break;

			case UINT_32_TYPE :
				Dest[DestCount++] =	(UINT32)MAPLONG(*(UINT32 *)TempPtr);
				ByteCount += 4;
				break;

			default :
				return E_STREAM_CORRUPT;
				break;
			}											/* end switch */
		}                                               /* end for */
		/* Account for left over bytes */
		if (ByteCount % 4) ByteCount += (4 - (ByteCount % 4));
	}                                                   /* end while */

	return NOERROR;
}


/***************************************************************************
*
* .b
* Procedure Name : MapFltVarVal
*
* Abstract :
*
*	Maps the variables from the Fault Descriptor Table to the actual Fault
*	Data retrieved from the Logic.
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				INT_8_TYPE
*				UINT_8_TYPE
*				INT_16_TYPE
*				UINT_16_TYPE
*				INT_32_TYPE
*				UINT_32_TYPE
*				E_NO_FAULT_VARS
*				E_INVALID_VAR_TYPE
*				NOERROR
*
*	Procedure Parameters :
*				INT16 				number_variables
*				INT16 				*vartype
*				unsigned long		*lpdata
*				double     			*varval
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
* .b
*
* History : 1/26/93         Ramesh Vasu @ AEG Westinghouse
* Revised : 05/08/96		Dev Pradhan @ Adtranz
*			- Updated SMRS
*			01/17/97		Dev Pradhan @ Adtranz
*			- Fixed bug with signed variables
*			05/06/97		Dev Pradhan @ Adtranz
*			- Attempted to fix bug with signed variables AGAIN
*			10/31/97		Dev Pradhan @ Adtranz
*			- Fixed problem with putting long values into the double array
*			  by passing in the double array.
*			11/11/97		Dev Pradhan @ Adtranz
*			- Fixed bug with signed variables AGAIN
*           01/20/08        Rebecca Cirinelli
*           - Fixed signed vars and unsigned vars to display correctly
****************************************************************************/
INT16 MapFltVarVal(	INT16					number_variables,
	INT16				*vartype,
	unsigned long	 	*lpdata,
	double 				*varval )
{
	INT16					Counter;
	_TUCHAR			 	*cpdata;
	_TCHAR				tempchar;
	INT16 				tempint;
	long				templong;

	if ( number_variables <= 0 ) return E_NO_FAULT_VARS;

	cpdata = (_TUCHAR *)lpdata;

	for (Counter = 0; Counter < number_variables; Counter++)
	{
		if (cpdata == NULL) break;

		switch( vartype[Counter] )
		{
		case UINT_8_TYPE :
			*(varval + Counter) = (UINT32)(*(_TUCHAR *)cpdata);
			cpdata += sizeof(_TCHAR);
			break;

		case INT_8_TYPE :
			tempchar = *(_TCHAR *)cpdata;
			*(varval + Counter) = (INT32)tempchar;
			cpdata += sizeof(_TCHAR);
			break;

		case UINT_16_TYPE :
			*(varval + Counter) = (UINT32)(MAPINT(*(UINT16 *)cpdata));
			cpdata += sizeof(INT16);
			break;

		case INT_16_TYPE :
			tempint = (*(UINT16 *)cpdata);
			tempint = (INT16)MAPINT(tempint);
			*(varval + Counter) = (INT32)tempint;
			cpdata += sizeof(INT16);
			break;

		case UINT_32_TYPE :
			*(varval + Counter) = (UINT32)(MAPLONG(*( UINT32 *)cpdata));
			cpdata += sizeof(INT32);
			break;

		case INT_32_TYPE :
			templong = MAPLONG(*(UINT32 *)cpdata);
			*(varval + Counter) = (INT32)templong;
			cpdata += sizeof(INT32);
			break;

		default :
			return E_INVALID_VAR_TYPE;
			break;
		}                                            	/* end switch */
	}													/* end for */

	return NOERROR;
}                                                  		/* end MapFltVarVal */


/***************************************************************************
*
* .b
* Procedure Name : ReadCMSFaultData
*
* Abstract :
*
*	Retrieve the Fault Data for a given Fault Index
*
* INPUTS :
*
*   Globals :
*				None
*
*   Constants :
*				GMEM_MOVEABLE
*				GMEM_ZEROINIT
*				NULL
*				MAXFAULTSIZE
*				NOERROR
*				E_MEM_ALLOC
*				E_FILE_READ
*
*	Procedure Parameters :
*				LPSTR		 	FileName
*				unsigned long 	*NumberOfFaults
*				INT16 			*BufferSize
*
* OUTPUTS :
*
*   Globals :
*				CurrentNumberOfFaults
*				hFBuf
*
*	Returned Values :
*				Error Code if FAILED else NOERROR
*
* Functional Description :
*
* .b
*
* History : 1/28/98		Dev Pradhan @ Adtranz
* Revised : 07/01/99		Jason O'Connor @ Adtranz
*			- Added extern "C", WINAPI function declarations for
*			  VC++ 5.0
*
****************************************************************************/
extern "C" INT16 WINAPI ReadCMSFaultData(LPSTR 	FileName,
	INT16		*NumberOfFaults)
{
	INT16				ReturnValue = NOERROR;
	_TUCHAR			*FaultBuffer;
	FILE				*fp;
	_TCHAR			tempstring[MAXFAULTSIZE];
	INT16				FaultSize;
	INT16				*FaultStartLocation;
	errno_t			error;

	/* Loop once ... exit on error */
	do
	{
		/* Open Source File  */

		if ((error = fopen_s(&fp, FileName, "rb")) != NULL)
		{ ReturnValue = E_FILE_READ; break; }

		/* Allocate an area for these Faults */
		hFBuf = GlobalAlloc(GMEM_MOVEABLE | GMEM_ZEROINIT, _filelength(_fileno(fp)));
		if (hFBuf == NULL)
		{ ReturnValue = E_MEM_ALLOC; break; }

		FaultBuffer = (_TUCHAR *)GlobalLock(hFBuf);
		FaultStartLocation = (INT16 *)GlobalLock(hFaultStartLocation);

		/* Read in Fault Data from file */
		CurrentNumberOfFaults = 0;
		GlobalIndex = 0;
		while (!feof(fp) && (GlobalIndex < _filelength(_fileno(fp))))
		{
			/* Load the Fault Buffer with the next Fault */
			if (fgets(tempstring, MAXFAULTSIZE, fp) == NULL)
				break;

			/* Save the starting location of this Faults Information */
			FaultStartLocation[*NumberOfFaults] = (INT16)GlobalIndex;

			/* Update Fault Count */
			CurrentNumberOfFaults++;

			/* Determine size of this fault, do not include EOL */
			FaultSize = strlen(tempstring) - 1;

			/* Save the fault size to the buffer */
			memcpy( &FaultBuffer[GlobalIndex], &FaultSize, 2);

			/* Save the fault data to the buffer */
			memcpy( &FaultBuffer[GlobalIndex + 2], tempstring, FaultSize);

			/* Update the Buffer Position */
			GlobalIndex += (FaultSize + 2);
		}
		GlobalUnlock(hFBuf);
		GlobalUnlock(hFaultStartLocation);

	} while (TRUE);

	*NumberOfFaults = CurrentNumberOfFaults;

	/* Close File */
	if (fp != NULL) fclose(fp);

	return ReturnValue;
}

