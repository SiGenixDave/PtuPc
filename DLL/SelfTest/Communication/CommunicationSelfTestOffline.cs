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
 *  File name:  CommunicationSelfTestOffline.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  08/10/11    1.0     Sean.D          1.  First entry into TortoiseSVN.
 *  
 *  08/24/11    1.1     K.McD           1.  Removed support for debug mode to be consistent with other sub-systems.
 *                                          
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Common;
using Common.Communication;
using Common.Configuration;

namespace SelfTest.Communication
{
    /// <summary>
    ///Class to simulate communication with the target hardare with respect to the diagnostic self-test sub-system.
    /// </summary>
    public class CommunicationSelfTestOffline : CommunicationParentOffline, ICommunicationSelfTest
    {
        #region --- Constants ---
        /// <summary>
        /// The value of the resultAvailable parameter returned from the GetSelfTestResult() method corresponding to a valid result being available.
        /// Value: 1.
        /// </summary>
        private short ResultAvailable = 1;

        /// <summary>
        /// The value of the testResult parameter returned from the GetSelfTestResult() method corresponding to the test having passed. Value: 1.
        /// </summary>
        private short ResultPassed = 1;
        #endregion --- Constants ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class and set the properties and member variables to those values associated with the specified
        /// communication interface.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface containing the properties and member variables that are to be
        /// used to initialize the class.</param>
        public CommunicationSelfTestOffline(ICommunicationParent communicationInterface)
            : base(communicationInterface)
        {
        }
        #endregion --- Constructors ---

        #region --- Member Variables ---
        /// <summary>
        /// A list of the test numbers of the self tests that are to be executed during the current run.
        /// </summary>
        private ArrayList m_QueuedTestList = new ArrayList();

        /// <summary>
        /// Used to keep a record of the number of loops that have been executed during the current run.
        /// </summary>
        private short m_LoopsExecuted = 0;

        /// <summary>
        /// The index of the queued test list associated with the current test.
        /// </summary>
        private short m_CurrentTestIndex = 0;

        /// <summary>
        /// The number of loops that are to be executed in the current run.
        /// </summary>
        private short m_STLoopCount = 1;

        /// <summary>
        /// A flag to indicate whether the user has initiated an abort request. True, if the user has initiated an abort request; oterwise, false.
        /// </summary>
        private bool m_UserAbort;
        #endregion --- Member Variables ---

        #region --- Methods ---
        /// <summary>
        /// Get the self test special message.
        /// </summary>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the <c>SELFTESTERRMESS
        /// </c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetSelfTestSpecialMessage() method
        /// is not CommunicationError.Success.</exception>
        public void GetSelfTestSpecialMessage(out short result, out short reason)
        {
            result = ResultPassed;
            reason = ResultPassed;
        }

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
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.StartSelfestTask() method is not 
        /// CommunicationError.Success.</exception>
        public void StartSelfTestTask(out short result, out short reason)
        {
            result = ResultPassed;
            reason = ResultPassed;

            m_LoopsExecuted = 0;
            m_CurrentTestIndex = 0;
        }

        /// <summary>
        /// Exit the self test task.
        /// </summary>
        /// <remarks>This request will halt the self test process on the VCU. 
        /// process.</remarks>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the
        /// <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ExitSelfestTask() method is not 
        /// CommunicationError.Success.</exception>
        public void ExitSelfTestTask(out short result, out short reason)
        {
            result = ResultPassed;
            reason = ResultPassed;
        }

        /// <summary>
        /// Abort the self test sequence.
        /// </summary>
        /// <remarks>This request will stop the execution of the self-test process on the VCU and return control to the propulsion software.</remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.AbortSTSequence() method is not 
        /// CommunicationError.Success.</exception>
        public void AbortSTSequence()
        {
            m_UserAbort = true;
            m_QueuedTestList.Clear();
            m_LoopsExecuted = 0;
        }

        /// <summary>
        /// Send an operator acknowledge message.
        /// </summary>
        /// <remarks>This request allows the operator to move to the next step of an interactive test.</remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SendOperatorAcknowledge() method
        /// is not CommunicationError.Success.</exception>
        public void SendOperatorAcknowledge()
        {
        }

        /// <summary>
        /// Update the list of individually selected self tests that are to be executed. 
        /// </summary>
        /// <remarks>This method will define the list of self-tests that are to be executed once the tester selects the execute command. The self tests
        /// are defined using the self test identifiers defined in the data dictionary.</remarks>
        /// <param name="testCount">The number of tests in the list.</param>
        /// <param name="tests">A list of the selfTestIdentifiers.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.UpdateSTTestList() method is not 
        /// CommunicationError.Success.</exception>
        public void UpdateSTTestList(short testCount, short[] tests)
        {
            m_QueuedTestList.Clear();
            m_QueuedTestList.AddRange(tests);
        }

        /// <summary>
        /// Run the predefined self tests associated with the specified test list identifier, these tests are defined in the data dictionary. 
        /// </summary>
        /// <param name="testListIdentifier">The test list identifier of the predefined self tests that are to be executed.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.RunPredefinedSTTests() method is
        /// not CommunicationError.Success.</exception>
        public void RunPredefinedSTTests(short testListIdentifier)
        {
            for (short testIndex = 0; testIndex < Lookup.TestListTable.RecordList.Count; ++testIndex)
            {
                m_QueuedTestList.Add(Lookup.TestListTable.RecordList[testIndex].Identifier);
            }
        }

        /// <summary>
        /// Update the number of times that the selected tests are to be run.
        /// </summary>
        /// <param name="loopCount">The number of cycles/loops of the defined tests that are to be performed.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.UpdateSTLoopCount() method is not 
        /// CommunicationError.Success.</exception>
        public void UpdateSTLoopCount(short loopCount)
        {
            m_STLoopCount = loopCount;
        }

        /// <summary>
        /// Execute the self tests that are defined in the current list.
        /// </summary>
        /// <param name="truckInformation">The truck to which the self tests apply. This does not apply on the CTA project as separate self-tests are
        /// set up for each truck.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ExecuteSTTestList() method is not 
        /// CommunicationError.Success.</exception>
        public void ExecuteSTTestList(TruckInformation truckInformation)
        {
            m_LoopsExecuted = 0;
            m_CurrentTestIndex = 0;
        }

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
        /// <param name="results">An array of <see cref="InteractiveResults_t"/> structures containing the value of each self test variable associated
        /// with the current interactive test.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetSelfTestResult() method is not 
        /// CommunicationError.Success.</exception>
        /// <remarks>In C# the sizeof the InteractiveResults_t structure is 16 bytes as the size is rounded up to the nearest quad word. This is 
        /// inconsistent with the size of the InteractiveResults_t structure used in PTUDLL32.dll - 12 bytes. To ensure that the results are 
        /// interpreted correctly the results are passed as a byte array which is then mapped to an array of InteractiveResults structures.</remarks>
        public unsafe void GetSelfTestResult(out short resultAvailable, out MessageMode messageMode, out short testIdentifier, out short testCase,
                                             out short testResult, out TruckInformation truckInformation, out short variableCount,
                                             out InteractiveResults_t[] results)
        {
            messageMode = MessageMode.Brief;
            testCase = 0;
            testResult = ResultPassed;
            truckInformation = TruckInformation.None;
            resultAvailable = ResultAvailable;

            if ((m_UserAbort == false) && (m_LoopsExecuted < m_STLoopCount) && (m_QueuedTestList.Count > 0))
            {
                testIdentifier = (short)m_QueuedTestList[m_CurrentTestIndex];
                m_CurrentTestIndex += 1;

                if (m_CurrentTestIndex >= m_QueuedTestList.Count)
                {
                    m_CurrentTestIndex = 0;
                    m_LoopsExecuted += 1;
                }
            }
            else
            {
                if (m_UserAbort == true)
                {
                    testIdentifier = (short)SpecialMessageIdentifier.TestAborted;
                    m_UserAbort = false;
                }
                else
                {
                    testIdentifier = (short)SpecialMessageIdentifier.TestComplete;
                }

                messageMode = MessageMode.Special;
                variableCount = 0;
                results = new InteractiveResults_t[0];

                m_LoopsExecuted = 0;
                m_CurrentTestIndex = 0;
                return;
            }

            try
            {
                List<SelfTestVariable> variableList = Lookup.SelfTestTableBySelfTestNumber.Items[testIdentifier].SelfTestVariableList;
                variableCount = (short)variableList.Count;
                throw new Exception(); 
            }
            catch
            {
                variableCount = 0;
            }

            results = new InteractiveResults_t[variableCount];

            for (int index = 0; index < variableCount; index++)
            {
                results[index].Value = 0.0;
                results[index].Tag = 0;
            }
        }

        /// <summary>
        /// Update the self test mode.
        /// </summary>
        /// <remarks>This call is used to check whether communication with the VCU has been lost.</remarks>
        /// <param name="selfTestMode">The required self test mode.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.UpdateSTMode() method is not 
        /// CommunicationError.Success.</exception>
        public void UpdateSTMode(SelfTestMode selfTestMode)
        {
        }
        #endregion --- Methods ---
    }
}
