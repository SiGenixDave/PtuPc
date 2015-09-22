/*****************************************************************************
*
* .b
*   Copyright (c) 1991-1996 ABB Daimler-Benz Transportation(North America) Inc.
*
*   Project:	Windows PTU
*
*   File Name:	selftest.h
*
*   SubSystem:	PTUDLL
*
*   Procedures:	<n/a>
*
*   EPROM Drawing:
*
* .b
*
*   History:        ??/??/??    Creation of Version 1.0         jsl
*   Revised:		05/25/95	ddp @ ATSI
*								- Added Prototype definitions.
*								- Added Comments.
*****************************************************************************/

#define INT16	_int16
#define UINT16	unsigned _int16
#define INT32	_int32	
#define UINT32	unsigned _int32
#define INT8	_int8
#define UINT8	unsigned _int8

/* Defines */
#define	STC_MSG_MODE_SPECIAL			4
#define	STC_MSG_MODE_INTERACTIVE		5

#define	STC_SPEC_MSG_ID_ENTER_ST		1
#define	STC_SPEC_MSG_ID_NO_ENTER		2
#define	STC_SPEC_MSG_ID_TEST_COMPLETE	3
#define	STC_SPEC_MSG_ID_TEST_ABORTED	4
#define	STC_SPEC_MSG_ID_EXIT_ST			5

#define	STC_CMD_UPDT_MODE				0
#define	STC_CMD_SEL_LIST				1
#define	STC_CMD_EXECUTE_LIST			2
#define	STC_CMD_UPDT_LIST				3
#define	STC_CMD_ABORT_SEQ				4
#define	STC_CMD_ABORT_SES				5
#define	STC_CMD_ACK_MSG					6
#define	STC_CMD_OPRTR_ACK				7
#define	STC_CMD_UPDT_LOOP_CNT			8

#define	STC_HIGH_LEVEL					0
#define	STC_SUPERVISOR					1
#define	STC_ENG_MODE_1					2
#define	STC_ENG_MODE_2					3
#define	STC_ENG_MODE_3					4
#define	STC_ENG_MODE_4					5
#define	STC_POWER_UP					6
#define	STC_PUSH_BUTTON					7
#define	STC_NETWORK						8

#define	SELFTESTTIMEOUT					1000    // was 100, for Arshad

/* Structures */
struct st_cc_result_data
{
	UINT16	num_of_fatal_failures;
	UINT16	num_of_non_fatal_failure;
};

struct st_pcptu_result_data1
{
	unsigned char	version;
	unsigned char	test_case;
	unsigned char	num_of_vars;
	unsigned char	test_result;
};

struct st_pcptu_result_data2
{
	unsigned char	version;
	unsigned char	test_result;
	UINT16			loop_count;
	UINT16			num_of_failures;
};

struct st_network_result_data
{
	unsigned char	test_case;
	unsigned char	test_result;
};

union st_test_result_data
{
	struct st_cc_result_data		cc_result;
	struct st_pcptu_result_data1	pcptu_result1;
	struct st_pcptu_result_data2	pcptu_result2;
	struct st_network_result_data	network_result;
};

struct st_msg_var_str
{
	UINT32	Value;
	char	Tag;
	char	Type;
};

typedef struct
{
    double	  Value;
    int     Tag;
} InteractiveResults_t;

/* Prototypes */
extern "C" {
INT16 WINAPI StartSelfTestTask			(INT16 *, INT16 *);
INT16 WINAPI GetSelfTestSpecialMessage	(INT16 *, INT16 *);
INT16 WINAPI ExitSelfTestTask			(INT16 *, INT16 *);
INT16 WINAPI AbortSTSequence			(void);
INT16 WINAPI SendOperatorAcknowledge	(void);
INT16 WINAPI UpdateSTTestList			(INT16, INT16 *);
INT16 WINAPI RunPredefinedSTTests		(INT16);
INT16 WINAPI UpdateSTLoopCount			(INT16);
INT16 WINAPI ExecuteSTTestList			(INT16);
INT16 WINAPI UpdateSTMode				(INT16);
INT16 WINAPI GetSelfTestResult			(INT16 *, INT16 *, INT16 *, INT16 *, INT16 *, 
										 INT16 *, INT16 *, InteractiveResults_t *);
}

			