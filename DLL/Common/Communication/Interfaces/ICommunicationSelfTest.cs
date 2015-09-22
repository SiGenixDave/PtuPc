#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Common
 * 
 *  File name:  ICommunicationSelfTest.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  05/31/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  07/25/11    1.1     K.McD           1.  Modified the Tag definition in the InteractiveResults_t structure to be an int rather than short.
 *                                      2.  Modified the XML tag associated with the results parameter of the GetSelfTestResult() method.
 * 
 */
#endregion --- Revision History ---

using Common.Configuration;

namespace Common.Communication
{
    #region --- Enumerators ---
    /// <summary>
    /// The self test message types supported by the VCU.
    /// </summary>
    public enum MessageMode
    {
        /// <summary>
        /// Brief Message. Value: 1.
        /// </summary>
        Brief = 1,

        /// <summary>
        /// Detailed Message. Value: 2.
        /// </summary>
        Detailed = 2,

        /// <summary>
        /// Counter. Value: 3.
        /// </summary>
        Counter = 3,

        /// <summary>
        /// Special Message. Value: 4.
        /// </summary>
        Special = 4,

        /// <summary>
        /// Interactive Test Result. Value: 5.
        /// </summary>
        Interactive = 5,

        /// <summary>
        /// Undefined. Value: 6.
        /// </summary>
        Undefined = 6
    }

    /// <summary>
    /// The truck associated with the current test, if applicable.
    /// </summary>
    public enum TruckInformation
    {
        /// <summary>
        /// Applies to neither truck. Value: 0.
        /// </summary>
        None = 0,

        /// <summary>
        /// Applies to the X truck only. Value: 1.
        /// </summary>
        X = 1,

        /// <summary>
        /// Applies to the Y truck only. VAlue: 2.
        /// </summary>
        Y = 2,

        /// <summary>
        /// Applies to both the X and Y trucks. VAlue: 3.
        /// </summary>
        XY = 3,

        /// <summary>
        /// Undefined. VAlue: 4.
        /// </summary>
        Undefined = 4
    }

    /// <summary>
    /// The self test mode of operation.
    /// </summary>
    public enum SelfTestMode
    {
        /// <summary>
        /// None. Value: 0.
        /// </summary>
        None = 0,

        /// <summary>
        /// To Be Defined. Value: 1.
        /// </summary>
        TBD = 1,

        /// <summary>
        /// Engineering Mode. Value: 2.
        /// </summary>
        Engineering = 2
    }

    /// <summary>
    /// The self test special message identifier.
    /// </summary>
    public enum SpecialMessageIdentifier
    {
        /// <summary>
        /// Not defined. Value: 0.
        /// </summary>
        None = 0,

        /// <summary>
        /// Enter self test mode. Value: 1.
        /// </summary>
        EnterSelfTest = 1,

        /// <summary>
        /// Unable to enter self test mode. Value: 2.
        /// </summary>
        NoEnterSelfTest = 2,

        /// <summary>
        /// Tests Complete. Value: 3.
        /// </summary>
        TestComplete = 3,

        /// <summary>
        /// Tests Aborted. Value: 4.
        /// </summary>
        TestAborted = 4,

        /// <summary>
        /// Exit self test mode. Value: 5.
        /// </summary>
        ExitSelfTest = 5
    }
    #endregion --- Enumerators ---

    #region --- Structures ---
    /// <summary>
    /// The results of an interactive test.
    /// </summary>
    public struct InteractiveResults_t
    {
        /// <summary>
        /// The value associated with the interactive test result.
        /// </summary>
        public double Value;

        /// <summary>
        /// The tag associated with the interactive test result.
        /// </summary>
        public int Tag;
    }
    #endregion --- Structures ---

    /// <summary>
    /// An interface to define the communication methods associated with the self test sub-system - SelfTest.dll.
    /// </summary>
    public interface ICommunicationSelfTest : ICommunicationParent
    {
        /// <summary>
        /// Get the self test special message.
        /// </summary>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetSelfTestSpecialMessage() method is not 
        /// CommunicationError.Success.</exception>
        void GetSelfTestSpecialMessage(out short result, out short reason);

        /// <summary>
        /// Start the self test task.
        /// </summary>
        /// <remarks>This request will start the self test process on the VCU. 
        /// process.</remarks>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.StartSelfestTask() method is not 
        /// CommunicationError.Success.</exception>
        void StartSelfTestTask(out short result, out short reason);

        /// <summary>
        /// Exit the self test task.
        /// </summary>
        /// <remarks>This request will start the self test process on the VCU. 
        /// process.</remarks>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ExitSelfestTask() method is not 
        /// CommunicationError.Success.</exception>
        void ExitSelfTestTask(out short result, out short reason);

        /// <summary>
        /// Abort the self test sequence.
        /// </summary>
        /// <remarks>This request will stop the execution of the self-test process on the VCU and return control to the propulsion software.</remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.AbortSTSequence() method is not 
        /// CommunicationError.Success.</exception>
        void AbortSTSequence();

        /// <summary>
        /// Send an operator acknowledge message.
        /// </summary>
        /// <remarks>This request allows the operator to move to the next step of an interactive test.</remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SendOperatorAcknowledge() method is not 
        /// CommunicationError.Success.</exception>
        void SendOperatorAcknowledge();

        /// <summary>
        /// Update the list of individually selected self tests that are to be executed. 
        /// </summary>
        /// <remarks>This method will define the list of self-tests that are to be executed once the tester selects the execute command. The self tests are defined 
        /// using the self test identifiers defined in the data dictionary.</remarks>
        /// <param name="testCount">The number of tests in the list.</param>
        /// <param name="tests">A list of the selfTestIdentifiers.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.UpdateSTTestList() method is not 
        /// CommunicationError.Success.</exception>
        void UpdateSTTestList(short testCount, short[] tests);

        /// <summary>
        /// Run the predefined self tests associated with the specified test list identifier, these tests are defined in the data dictionary. 
        /// </summary>
        /// <param name="testListIdentifier">The test list identifier of the predefined self tests that are to be executed.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.RunPredefinedSTTests() method is not 
        /// CommunicationError.Success.</exception>
        void RunPredefinedSTTests(short testListIdentifier);

        /// <summary>
        /// Update the number of times that the selected tests are to be run.
        /// </summary>
        /// <param name="loopCount">The number of cycles/loops of the defined tests that are to be performed.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.UpdateSTLoopCount() method is not 
        /// CommunicationError.Success.</exception>
        void UpdateSTLoopCount(short loopCount);

        /// <summary>
        /// Execute the self tests that are defined in the current list.
        /// </summary>
        /// <param name="truckInformation">The truck to which the self tests apply. This does not apply on the CTA project as separate self-tests are set up for each
        /// truck.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ExecuteSTTestList() method is not 
        /// CommunicationError.Success.</exception>
        void ExecuteSTTestList(TruckInformation truckInformation);

        /// <summary>
        /// Get the self test results.
        /// </summary>
        /// <param name="resultAvailable">A flag to indicate whether a valid result is available. A value of 1 indicates that a valid result is available; otherwise, 
        /// 0.</param>
        /// <param name="messageMode">The type of message returned from the VCU.</param>
        /// <param name="testIdentifier">The test result identifier; the interpretation of this value is dependent upon the message mode. For detailed messages, this 
        /// value represents the self test identifier.</param>
        /// <param name="testCase">The test case number associated with the message.</param>
        /// <param name="testResult">Used with the passive and logic self tests to define whether the test passed or failed. A value of 1 indicates that the test 
        /// passed; otherwise, the test failed.</param>
        /// <param name="truckInformation">An enumerator to define the truck information associated with the message.</param>
        /// <param name="variableCount">The number of variables associated with the message.</param>
        /// <param name="results">An array of <see cref="InteractiveResults_t"/> structures containing the value of each self test variable associated with the current 
        /// interactive test.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetSelfTestResult() method is not 
        /// CommunicationError.Success.</exception>
        void GetSelfTestResult(out short resultAvailable, out MessageMode messageMode, out short testIdentifier, out short testCase, out short testResult,
                               out TruckInformation truckInformation, out short variableCount, out InteractiveResults_t[] results);

        /// <summary>
        /// Update the self test mode.
        /// </summary>
        /// <remarks>This call is used to check whether communication with the VCU has been lost.</remarks>
        /// <param name="selfTestMode">The required self test mode.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.UpdateSTMode() method is not 
        /// CommunicationError.Success.</exception>
        void UpdateSTMode(SelfTestMode selfTestMode);
    }
}
