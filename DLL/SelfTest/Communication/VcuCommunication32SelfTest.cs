#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    SelfTest
 * 
 *  File name:  VcuCommunication32SelfTest.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  05/26/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  07/25/11    1.1     K.McD           1.  Corrected the XML tag associated with the results parameter of the GetSelfTestResult() method.
 *                                      2.  Modified the GetSelfTestResult() method to pass the self test variable values as a byte array rather than 
 *                                          an array of InteractiveResults_t structures as the structure storage in memory is different in C#.
 *                                          
 *  03/22/15    1.2     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  Changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                              to support both 32 and 64 bit architecture.
 *                                          
 *                                          2.  Changes to allow the PTU to handle both 2 and 4 character date codes.
 *                                      
 *                                      Modifications
 *                                      1.  Renamed class to VcuCommunication32SelfTest. Ref.: 1.1.
 *                                      2.  Changed the DLLImport parameter to VcuCommunication32.dll. Ref.: 1.1.
 * 
 *
 */
#endregion --- Revision History ---

using System.Runtime.InteropServices;
using Common.Communication;

namespace SelfTest.Communication
{
    /// <summary>
    /// A managed wrapper class to allow the unmanaged Self Test C++ functions associated with the VcuCommunication32 dynamic link library (DLL) to be 
    /// accessed from within managed code. The VcuCommunication32 DLL supports the low level communication protocol with the 
    /// Vehicle Control Unit (VCU) when running on a 32 bit platform. VcuCommunication32.dll and VcuCommunication64.dll are functionally 
    /// identical, however, VcuCommunication32 is targeted towards Windows 32 bit operating systems.
    /// </summary>
    public class VcuCommunication32SelfTest
    {
        #region - [VcuCommunication - SelfTest.CPP Prototypes] -
        /*
        INT16 WINAPI StartSelfTestTask	        (INT16  *, INT16  *);
        INT16 WINAPI GetSelfTestSpecialMessage  (INT16  *, INT16  *);
        INT16 WINAPI ExitSelfTestTask           (INT16  *, INT16  *);
        INT16 WINAPI AbortSTSequence            (void);
        INT16 WINAPI SendOperatorAcknowledge	(void);
        INT16 WINAPI UpdateSTTestList			(INT16, INT16  *);
        INT16 WINAPI RunPredefinedSTTests		(INT16);
        INT16 WINAPI UpdateSTLoopCount			(INT16);
        INT16 WINAPI ExecuteSTTestList			(INT16);
        INT16 WINAPI UpdateSTMode				(INT16);
        INT16 WINAPI GetSelfTestResult			(INT16  *, INT16 	*, INT16  *, INT16  *, INT16 	*, INT16 	*, INT16  *, InteractiveResults_t  *);
        */
        #endregion - [VcuCommunication - SelfTest.CPP Prototypes] -

        #region - [VB Equivalents ] -
        /*
        Declare Function StartSelfTestTask Lib "ptudll32.dll" (ByRef result As Short, ByRef Reason As Short) As Short
	    Declare Function ExitSelfTestTask Lib "ptudll32.dll" (ByRef result As Short, ByRef Reason As Short) As Short
	    Declare Function AbortSTSequence Lib "ptudll32.dll" () As Short
	    Declare Function UpdateSTMode Lib "ptudll32.dll" (ByVal NewMode As Short) As Short
	    Declare Function UpdateSTTestList Lib "ptudll32.dll" (ByVal NumberofTests As Short, ByRef TestList As Short) As Short
	    Declare Function UpdateSTLoopCount Lib "ptudll32.dll" (ByVal LoopCount As Short) As Short
	    Declare Function ExecuteSTTestList Lib "ptudll32.dll" (ByVal TruckInformation As Short) As Short
	    'UPGRADE_WARNING: Structure InteractiveResults_t may require marshalling attributes to be passed as an argument in this Declare statement.
         Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	    Declare Function GetSelfTestResult Lib "ptudll32.dll" (ByRef ValidResult As Short, ByRef MessageMode As Short, ByRef TestID As Short,
                                                               ByRef TestCase As Short, ByRef TestResult As Short, ByRef SetInfo As Short,
                                                               ByRef NumOfVariables As Short, ByRef Results As InteractiveResults_t) As Short
	    Declare Function RunPredefinedSTTests Lib "ptudll32.dll" (ByVal TestID As Short) As Short
	    Declare Function SendOperatorAcknowledge Lib "ptudll32.dll" () As Short
	    Declare Function GetSelfTestSpecialMessage Lib "ptudll32.dll" (ByRef result As Short, ByRef Reason As Short) As Short
	    */
        #endregion - [VB Equivalents ] -

        /// <summary>
        /// Get the self test special message.
        /// </summary>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the
        /// <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetSelfTestSpecialMessage(out short result, out short reason);

        /// <summary>
        /// Start the self test task.
        /// </summary>
        /// <remarks>This request will start the self test process on the VCU. 
        /// process.</remarks>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the
        /// <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short StartSelfTestTask(out short result, out short reason);

        /// <summary>
        /// Exit the self test task. 
        /// </summary>
        /// <remarks>This request will exit the self-test process on the VCU and turn control over to the propulsion software.</remarks>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the
        /// <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short ExitSelfTestTask(out short result, out short reason);

        /// <summary>
        /// Abort the self test sequence.
        /// </summary>
        /// <remarks>This request will stop the execution of the self-test process on the VCU and return control to the propulsion software.</remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short AbortSTSequence();

        /// <summary>
        /// Send an operator acknowledge message.
        /// </summary>
        /// <remarks>This request allows the operator to move to the next step of an interactive test.</remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short SendOperatorAcknowledge();

        /// <summary>
        /// Update the list of individually selected self tests that are to be executed. 
        /// </summary>
        /// <remarks>This method will define the list of self-tests that are to be executed once the tester selects the execute command. The self tests
        /// are defined using the self test identifiers defined in the data dictionary.</remarks>
        /// <param name="testCount">The number of tests in the list.</param>
        /// <param name="tests">A list of the self test identifiers.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short UpdateSTTestList(short testCount, short[] tests);

        /// <summary>
        /// Run the predefined self tests associated with the specified test list identifier, these tests are defined in the data dictionary. 
        /// </summary>
        /// <param name="testListIdentifier">The test list identifier of the predefined self tests that are to be executed.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short RunPredefinedSTTests(short testListIdentifier);

        /// <summary>
        /// Update the number of times that the selected tests are to be run.
        /// </summary>
        /// <param name="loopCount">The number of cycles/loops of the defined tests that are to be performed.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short UpdateSTLoopCount(short loopCount);

        /// <summary>
        /// Execute the self tests that are defined in the current list.
        /// </summary>
        /// <param name="truckInformation">The truck to which the self tests apply. This does not apply on the CTA project, separate self-tests are set
        /// up for each truck.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short ExecuteSTTestList(short truckInformation);

        /// <summary>
        /// Get the self test results.
        /// </summary>
        /// <param name="resultAvailable">A flag to indicate whether a valid result is available. A value of 1 indicates that a valid result is
        /// available; otherwise, 0.</param>
        /// <param name="messageMode">The type of message returned from the VCU.</param>
        /// <param name="testIdentifier">The test result identifier; the interpretation of this value is dependent upon the message mode. For detailed
        /// messages, this value represents the self test identifier.</param>
        /// <param name="testCase">The test case number associated with the message.</param>
        /// <param name="testResult">Used with the passive and logic self tests to define whether the test passed or failed. A value of 1 indicates
        /// that the test passed; otherwise, the test failed.</param>
        /// <param name="truckInformation">An enumerator to define the truck information associated with the message.</param>
        /// <param name="variableCount">The number of variables associated with the message.</param>
        /// <param name="results">A pointer to a byte array that is used to store the value of each self test variable associated with the current
        /// interactive test. This byte array must be mapped to an array of <see cref="InteractiveResults_t"/> structures to obtain the results. Data
        /// is passed this way as the size of the InteractiveResults_t structure in C# is different to the value used in VcuCommunication32. Each
        /// element of the array of the InteractiveResults_t structures in VcuCommunication32 consists of a double (8 bytes) followed by an integer
        /// (4 byte) making a total of 12 bytes.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short GetSelfTestResult(out short resultAvailable, out MessageMode messageMode, out short testIdentifier,
                                                            out short testCase, out short testResult, out TruckInformation truckInformation,
                                                            out short variableCount, byte* results);

        /// <summary>
        /// Update the self test mode.
        /// </summary>
        /// <remarks>This call is used to check whether communication with the VCU has been lost.</remarks>
        /// <param name="selfTestMode">The required self test mode.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        [DllImport("VcuCommunication32.dll")]
        public static extern unsafe short UpdateSTMode(SelfTestMode selfTestMode);
    }
}
