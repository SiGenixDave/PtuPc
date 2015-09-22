/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:	Windows PTU
*
*   File Name:	comm.h
*
*   SubSystem:	VcuCommunication (Originally named PTUDLL32)
*
*   Procedures:	<n/a>
*
*   EPROM Drawing:
*
* .b
*
*   History:	??/??/??    Creation of Version 1.0         jsl
*   Revised:    05/25/95    ddp			@ ATSI
*               - Added Prototype definitions.
*               - Added Comments.
*               11/17/95    ddp			@ ATSI
*               - Modified to handle a Motorola Logic.
*               - Modified for TCP/IP communication.
*				12/12/95	ddp			@ ATSI
*				- Upped MAXTASKS from 20 to 120
*				12/12/95	ddp			 @ ATSI
*				- Changed DataType in WatchElement_t to an UINT16.
*				- Changed SetInfo in GetSelfTestPacketRes_t to an UINT16.
*				01/29/96	ddp			@ Adtranz
*				- Modified to handle upto 256 Datalog Variables.
*				03/14 /96	ddp			@ Adtranz
*				- Moved socket specific defines to clntsock.h.
*				05/03/96	ddp			@ Adtranz
*				- Changed GetVariableInfoRes_t to reflect Database approach where we send minimal information back from embedded.
*				- Added ConfigurationMask element to GetEmbeddedInfoRes_t.
*				- Changed prototypes for GetVariableInformation() and GetEmbeddedInformation() to reflect previous 2 changes.
*				11/06/97	ddp			@ Adtranz
*				- changed the define TIMEOUTCONST to reflect a time (seconds)
*				  interval.
*				- Removed prototypes of functions not used any more.
*				02/05/99   RAC			@ Adtranz
*				- Changed TestSet array dim from 15 to 100 in struct SelfTestCommandReq_t.
*               03/11/02   RAC			@ Bombardier Transportation
*				- Changed struct GetTimeDateRes_t and SetTimeDateReq_t to use a 4-digit year.
*				08/25/11	K.McD		@ Bombardier Transportation.
*				- Added a conditional compilation symbol to the WATCHSIZE definition.
*				09/21/11	Sean.D		@ Bombardier Transportation.
*				- Set up SetWatchElementsReq_t, UpdateWatchElementsRes_t, GetWatchValuesReq_t, and GetWatchValuesRes_t (the latter two are unused)
*	Revised:	03/26/15	Ver. 2.0	K McDonald	@ Northern Software Engineering
*			  -	Changes to allow the PTU to handle both 2 and 4 character year codes. Added the structures Get4TimeDateRes_t and Set4TimeDateRes_t
*				to define the packet structure for supporting VCU's that use a 4 digit year code.
*			  -	Added a parameter to specify whether the VCU supports 2 or 4 digit year codes to the the SetTimeDate() and GetTimeDate() functions
*				defined within the extern "C" { ... } function definitions.
*
*****************************************************************************/

/* General Structures */
typedef UINT16 CHECKSUM_t;

#define INT16	_int16
#define UINT16	unsigned _int16
#define INT32	_int32	
#define UINT32	unsigned _int32
#define INT8	_int8
#define UINT8	unsigned _int8

typedef union
{
	UINT32   Unsigned;
	INT32    Signed;
} SignedUnion_t;

/* Defines */
#define DPP3                UINT16		    ResponseType;
#define DPP2                CHECKSUM_t      CheckSum;       DPP3
#define DPP1                UINT16		    PacketType;     DPP2
#define DATAPACKETPROLOG    UINT16		    PacketLength;   DPP1

#define NOERROR             0
#define PORTOPEN            (-1)
#define NOHARDWARE          (-2)
#define TIMEOUT             (-10)
#define BADRESPONSE         (-11)
#define CHECKSUMERROR       (-12)
#define BADREQUEST          (-13)
#define UNKNOWNERROR        (-100)

#define INQUEUESIZE         1024
#define OUTQUEUESIZE        1024

#define TIMEOUTCONST        5 /* seconds */
#define MAXFILTERCONST      1024

#define CHECKSUMMAXVALUE    0xffff

#define SYNC_SOM            ':'
#define ASYNC_SOM           ';'
#define MOTOROLA_SOM        'S'
#define SOM                 ':'
#define ACK                 (0x04)
#define	NAK					(0x06)
#define	COMMANDREQUEST		(0x01)
#define	DATAREQUEST			(0x02)

#define MAXFAULTSIZE        256
#define MAXFAULTBUFFERSIZE	4096
#define MAXDLBUFFERSIZE     (unsigned)(12000 * sizeof(long))
#define MAXDLVARIABLES      256
#define MAXSTVARIABLES      16
#define MAXEVENTLOGS        10
#define MAXTASKS            120
#define MAXEVENTS           100

#define SET_WATCH_ELEMENT           2
#define SET_WATCH_ELEMENTS          3
#define UPDATE_WATCH_ELEMENTS       4
#define SET_CHART_SCALE             5

#define SEND_VARIABLE_VALUE         7
#define GET_DICTIONARY_SIZE         8
#define GET_VARIABLE_INFORMATION    9
#define GET_EMBEDDED_INFORMATION    10
#define GET_CHART_MODE              11
#define SET_CHART_MODE              12
#define GET_CHART_INDEX             13
#define SET_CHART_INDEX             14
#define GET_WATCH_VALUES            15
#define GET_TIME_DATE               16
#define SET_TIME_DATE               17
#define START_SELF_TEST_TASK        18
#define SELF_TEST_COMMAND           19
#define GET_SELF_TEST_PACKET        20
#define EXIT_SELF_TEST_TASK         21
#define SET_FAULT_LOG               22
#define GET_FAULT_INDICES           23
#define GET_FAULT_HISTORY           24
#define GET_FAULT_DATA              25
#define GET_FAULT_FLAG              26
#define SET_FAULT_FLAG              27

#define GET_DATALOG_STATUS          31
#define GET_DATALOG_BUFFER          32

#define SET_CARID                   34
#define CLEAR_EVENTLOG              35
#define INITIALIZE_EVENTLOG         36
#define SET_STREAM_INFORMATION      37
#define GET_STREAM_INFORMATION      38
#define GET_DEFAULT_STREAM          39

#define START_CLOCK                 50
#define STOP_CLOCK                  51
#define CHANGE_EVENT_LOG            52
#define GET_EVENT_LOG				53
#define GET_STREAM_FLAG				54
#define BTU							55

#define	INITIALIZECOMMPORT			100
#define	CLOSECOMMPORT				101
#define TERMINATECONNECTION			102

#define SIGNED                      01
#define UNSIGNED                    02

#define RS232						0		/* Communcation Protocols */
#define TCPIP                       1
#define UDP                         2

#define UINT_8_TYPE					0		/* Variable Types From Logic */
#define UINT_16_TYPE                1
#define UINT_32_TYPE                2
#define INT_8_TYPE                  3
#define INT_16_TYPE                 4
#define INT_32_TYPE                 5

#define MAPLONG(i)      (GlobalSOM == MOTOROLA_SOM) ?   \
							((i & 0xff000000) >> 24) |  \
							((i & 0x00ff0000) >> 8)  |  \
							((i & 0x0000ff00) << 8)  |  \
							((i & 0x000000ff) << 24)    \
							: i

#define MAPINT(i)       (GlobalSOM == MOTOROLA_SOM) ?   \
							((i & 0xff00) >> 8)  |      \
							((i & 0x00ff) << 8)         \
							: i

/* Message Structures */
typedef struct 
{
	UINT16			Index;
	SignedUnion_t   NewValue;
	UINT16			DataType;
}WatchElement_t; 

typedef struct 
{
	UINT16	StreamVariable;
	UINT16	StreamVariableType;
}StreamVariable_t;

typedef struct 
{
	UINT16				NumberOfVariables;
	UINT16				NumberOfSamples;
	UINT16				SampleRate;
	StreamVariable_t	StreamVariableInfo[MAXDLVARIABLES];
}StreamInformation_t;

typedef struct
{
	DATAPACKETPROLOG
} Header_t;

typedef struct 
{
	DATAPACKETPROLOG
	UINT16    ElementIndex;
	UINT16    DictionaryIndex;
}SetWatchElementReq_t;

template <unsigned int WatchSize>
struct  SetWatchElementsReq_t
{
	DATAPACKETPROLOG
	UINT16    WatchElement[WatchSize];
};

typedef struct 
{
	DATAPACKETPROLOG
	unsigned char   ForceFullUpdate;
}UpdateWatchElementsReq_t;

template <unsigned int WatchSize>
struct UpdateWatchElementsRes_t
{
	DATAPACKETPROLOG
	UINT16			NumberOfUpdates;
	WatchElement_t  WatchElement[WatchSize];
};

typedef struct
{
	DATAPACKETPROLOG
	UINT16    DictionaryIndex;
}ReadVariableReq_t;

typedef struct 
{
	DATAPACKETPROLOG
	SignedUnion_t   CurrentValue;
	UINT16			DataType;
}ReadVariableRes_t;

typedef struct 
{
	DATAPACKETPROLOG
	UINT16			DictionaryIndex;
	SignedUnion_t   NewValue;
} SendVariableReq_t;

typedef struct 
{
	DATAPACKETPROLOG
	UINT16    DictionarySize;
}GetDictionarySizeRes_t;

typedef struct 
{
	DATAPACKETPROLOG
	UINT16    DictionaryIndex;
}GetVariableInfoReq_t;

typedef struct 
{
	DATAPACKETPROLOG
	UINT16	DataType;
	long            MaxScale;
	long            MinScale;
	unsigned long   AttributeFlags;
}GetVariableInfoRes_t;

typedef struct
{
	DATAPACKETPROLOG;
	long			ConfigNum;
}SetConfigNumRes_t;

typedef struct
{
	DATAPACKETPROLOG
	char   			SoftwareVersion[41];
	char   			CarID[11];
	char   		  	SubSystemName[41];
	char   			IdentifierString[5];
	unsigned long	ConfigurationMask;
} GetEmbeddedInfoRes_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned char   CurrentChartMode;
} GetChartModeRes_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned char   TargetChartMode;
} SetChartModeReq_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned char   ChartIndex;
} GetChartIndexReq_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16    VariableIndex;
} GetChartIndexRes_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16			VariableIndex;
	unsigned char   ChartIndex;
} SetChartIndexReq_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16		DictionaryIndex;
	long		MaxScale;
	long		MinScale;
} SetChartScaleReq_t;

template <unsigned int WatchSize>
struct GetWatchValuesReq_t
{
	DATAPACKETPROLOG
	UINT16    WatchIndexes[WatchSize];
};

template <unsigned int WatchSize>
struct GetWatchValuesRes_t
{
	DATAPACKETPROLOG
	SignedUnion_t   WatchValues[WatchSize];
	unsigned char   DataType[WatchSize];
};

typedef struct
{
	DATAPACKETPROLOG
	unsigned char   Hour;
	unsigned char   Minute;
	unsigned char   Second;
	unsigned char   Year;
	unsigned char   Month;
	unsigned char   Day;
} GetTimeDateRes_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned char   Hour;
	unsigned char   Minute;
	unsigned char   Second;
	unsigned char   Year;
	unsigned char   Month;
	unsigned char   Day;
} SetTimeDateReq_t;

// Some projects, e.g. R188 use a 4 digit year code for the time/date functions. 
// These structures are included to support these projects.
typedef struct
{
	DATAPACKETPROLOG
	unsigned char   Hour;
	unsigned char   Minute;
	unsigned char   Second;
	unsigned char   Temp;	// Introduced to ensure the structure still lies on a word boundary.
	UINT16			Year;
	unsigned char   Month;
	unsigned char   Day;
} Get4TimeDateRes_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned char   Hour;
	unsigned char   Minute;
	unsigned char   Second;
	unsigned char   Temp;	// Introduced to ensure the structure still lies on a word boundary.
	UINT16			Year;
	unsigned char   Month;
	unsigned char   Day;
} Set4TimeDateReq_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned char   CommandID;
	unsigned char   TruckInformation;
	UINT16			Data;
	UINT16			TestSet[100];
} SelfTestCommandReq_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned char               Valid;
	unsigned char               MessageMode;
	UINT16						SetInformation;
	UINT16						TestID;
	union st_test_result_data   ResultsData;
	unsigned long               Flags;
	UINT16						Reserved[2];
	//UINT8						Reserved[4];
	struct st_msg_var_str       VariableMsg[MAXSTVARIABLES];
} GetSelfTestPacketRes_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned char   TargetState;
} SetFaultLogReq_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned long   Newest;
	unsigned long   Oldest;
} GetFaultIndicesRes_t;

typedef struct
{
	DATAPACKETPROLOG
	unsigned long   FaultIndex;
	UINT16			NumberOfFaults;
} GetFaultDataReq_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16			BufferSize;
	unsigned char   Buffer[MAXFAULTBUFFERSIZE];
} GetFaultDataRes_t;

typedef struct
{
    DATAPACKETPROLOG
    INT16	BufferSize;
    UINT16	EnableFlag[(MAXTASKS * MAXEVENTS / 16) + 1];
} GetFaultFlagRes_t;

typedef struct
{
    DATAPACKETPROLOG
    INT16	BufferSize;
    UINT16	DatalogFlag[(MAXTASKS * MAXEVENTS / 16) + 1];
} GetStreamFlagRes_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16			TaskID;
	UINT16			FaultID;
	unsigned char   EnableFlag;
	unsigned char   DatalogFlag;
} SetFaultFlagReq_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16    TaskID;
	UINT16    FaultID;
} GetFaultHistoryReq_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16    StaticHistory;
	UINT16    DynamicHistory;
} GetFaultHistoryRes_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16    DatalogIndex;
} GetDatalogBufferReq_t;

typedef struct
{
    DATAPACKETPROLOG
    UINT16			TimeOrigin;
    UINT16			BufferSize;
    unsigned char	DatalogBuffer[MAXDLBUFFERSIZE];
} GetDatalogBufferRes_t;

typedef struct
{
	DATAPACKETPROLOG
	char	NewCarID[11];
} SetCarIDReq_t;

typedef struct
{
	DATAPACKETPROLOG
	StreamInformation_t Information;
} SetStreamInfoReq_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16    StreamNumber;
} GetStreamInfoReq_t;

typedef struct
{
	DATAPACKETPROLOG
	StreamInformation_t Information;
} GetStreamInfoRes_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16 CurrentEventLog;
	UINT16 NumberEventLogs;
} GetEventLogRes_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16 NewEventLog;
} ChangeEventLogReq_t;

typedef struct
{
	DATAPACKETPROLOG
	UINT16 ChangeStatus;
	UINT16 DataRecordingRate;
	UINT16 MaxTasks;
	UINT16 MaxEvents;
} ChangeEventLogRes_t;

typedef struct          /* BTU REQUEST/RESPONSE PACKET */
{
	DATAPACKETPROLOG
	UINT16  mode;
	UINT16  Buffer[17];
} BTU_test;

typedef struct
{
    char 	VariableName[40];
    char 	EmbeddedName[50];
    char 	FormatString[20];
    double 	ScaleFactor;
    char 	TargetUnits[50];
    double 	MaxScale;
    double 	MinScale;
    double 	UpperBounds;
    double 	LowerBounds;
    long 	AttributeFlags;
    long 	EnumbitID;
    long 	HelpIndex;
    double 	BitMask;
    INT16	DataType;
} DataDictionary_t;

typedef struct
{
    char 	Description[40];
    double 	Value;
} EnumValue_t;

/* Externs */
extern INT16  	GlobalSOM;
extern INT16	CommunicationProtocol;
extern INT16	CommID;
extern INT16    PackSize;

/* PRIMCOMM.CPP Prototypes */
INT16 InitCommPort					(INT16, INT32, INT16, INT16, INT16);
#if MOBILEPTU
INT16 InitTCPIP						(LPSTR);
#endif
INT16 SendDataPacket                (Header_t *);
INT16 GetDataPacket                 (Header_t *);
INT16 GetACK                        (void);

/* COMM.CPP Prototypes */
INT16 Transaction					(Header_t *, Header_t *);
extern "C"{
INT16 WINAPI InitCommunication		(INT16, LPSTR, INT32, INT16, INT16, INT16);
INT16 WINAPI CloseCommunication		(INT16);
INT16 WINAPI SendVariable			(INT16, INT16, double);
INT16 WINAPI SetWatchElements		(INT16 *);
INT16 WINAPI SetWatchElement		(INT16, INT16);
__declspec(dllexport) INT16 WINAPI UpdateWatchElements	(INT16, double *, INT16 *);
INT16 WINAPI GetVariableInformation	(INT16, INT16 *, double *, double *, long *);
INT16 WINAPI GetEmbeddedInformation	(BSTR*, BSTR*, BSTR*, BSTR*, double *);
INT16 WINAPI GetChartMode			(INT16 *);
INT16 WINAPI SetChartMode			(INT16);
INT16 WINAPI GetChartIndex			(INT16, INT16 *);
INT16 WINAPI SetChartIndex			(INT16, INT16);
INT16 WINAPI SetChartScale			(INT16, double, double);
INT16 WINAPI GetTimeDate			(INT16, INT16 *, INT16 *, INT16 *, INT16 *, INT16 *, INT16 *);
INT16 WINAPI SetTimeDate			(INT16, INT16, INT16, INT16, INT16, INT16, INT16);
INT16 WINAPI SetCarID				(INT16);
INT16 WINAPI StartClock				(void);
INT16 WINAPI StopClock				(void);
INT16 WINAPI PTU_MVB_Interface		(UINT16, UINT16 *, UINT16 *);
INT16 WINAPI ParseBitMask			(INT16, EnumValue_t *, double, BSTR);
INT16 WINAPI SetWatchSize			(UINT16);
}