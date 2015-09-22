/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:	Windows PTU
*
*   File Name:	event.h
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
*   Revised:		05/25/95	ddp @ ATSI
*					- Added Prototype definitions.
*					- Added Comments.
*   				06/06/95	ddp @ ATSI
*					- Added E_FLT_CORRUPT error define.
*   				10/03/95	ddp @ ATSI
*					- Revised Prototypes for GetStream() and GetStreamInformation() to pass variable type information.
*               	10/06/95    DDP @ ATSI
*               	- Modified to handle multiple fault and data logs.
*               	01/10/96    DDP @ ATSI
*					- Added E_STREAM_CORRUPT.
*
*****************************************************************************/

/* Defines */
#define INT16	_int16
#define UINT16	unsigned _int16
#define INT32	_int32	
#define UINT32	unsigned _int32
#define INT8	_int8
#define UINT8	unsigned _int8

#define UINT_8_TYPE                 0
#define UINT_16_TYPE                1
#define UINT_32_TYPE                2
#define INT_8_TYPE                  3
#define INT_16_TYPE                 4
#define INT_32_TYPE                 5

#define MAXNUMOFFAULTS				400

#define DISABLE                     0
#define ENABLE                      1

#define NOERROR                     0
#define E_MEM_ALLOC  				1002
#define E_FILE_READ					1010
#define E_INVALID_VAR_TYPE          1301
#define E_FDT_FLT_INDEX             1400
#define E_NO_FAULT_VARS             1401
#define E_FLT_CORRUPT               1600
#define E_STREAM_CORRUPT			1601

/* Structures */
typedef struct date_time_type
{
    unsigned char  hr;
    unsigned char  min;
    unsigned char  sec;
    unsigned char  month;
    unsigned char  day;
    unsigned char  year;
} DATETIMETYPE;

struct flthdr
{
    UINT16			faultnum;
    UINT16			tasknum;
    unsigned long	index;
    DATETIMETYPE	datetime;
    UINT16			datalognum;
};

typedef struct embedded_struct
{
  char SubSystemName[40];
  char IdentifierString[4];
  char SoftwareVersion[40];
  char CarID[10];
  INT16  DataDictionarySize;
} EmbeddedInfo_t;

/* Prototypes */
extern "C" {
INT16 WINAPI LoadFaultlog					(INT16 *, unsigned long *, unsigned long *);
INT16 WINAPI CheckFaultlogger				(INT16 *, unsigned long *);
INT16 WINAPI GetFaultHdr					(INT16, INT16 *, INT16 *, BSTR*, BSTR*, INT16 *);
INT16 WINAPI GetFaultVar					(INT16, INT16, INT16 *, double *);
INT16 WINAPI GetFltFlagInfo					(INT16 *, INT16 *, INT16 *, INT16);
INT16 WINAPI GetFltHistInfo					(INT16 *, INT16 *, INT16 *, INT16, INT16);
INT16 WINAPI FreeEventLogMemory				(void);
INT16 WINAPI SetFaultFlags					(INT16, INT16, INT16, INT16);
INT16 WINAPI GetDatalogStatus				(INT16 *);
INT16 WINAPI ClearEvent    					(void);
INT16 WINAPI InitializeEventLog				(void);
INT16 WINAPI GetStream						(INT16, long *, INT16 *, INT16, INT16, INT16 *);
INT16 WINAPI GetStreamInformation			(INT16, INT16 *, INT16 *, INT16 *, INT16 *, INT16 *);
INT16 WINAPI GetDefaultStreamInformation	(INT16 *, INT16 *, INT16 *, INT16 *, INT16 *);
INT16 WINAPI SetDefaultStreamInformation	(INT16, INT16, INT16 *);
INT16 WINAPI GetEventLog					(INT16 *, INT16 *);
INT16 WINAPI ChangeEventLog					(INT16, INT16 *, INT16 *, INT16 *, INT16 *);
INT16 WINAPI ExitEventLog					(void);
INT16 WINAPI ReadCMSFaultData				(LPSTR, INT16 *);
}