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
 *  File name:  CommunicationSelfTest.cs
 * 
 *  Revision History
 *  ----------------
 */

/*
 *  Date        Version Author          Comments
 *  05/26/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  08/10/11    1.1     Sean.D          1.  Minor corrections to a number of comments.
 *
 */

#region - [1.2] -
/*
 *  03/22/15    1.2     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  Implement changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                              to support both 32 and 64 bit architecture.
 *                                      
 *                                      Modifications
 *                                      1.  Added delegate declarations for all of the VcuCommunication32.dll and VcuCommunication64 methods that
 *                                          are associated with the diagnostic self test sub-system.
 *                                          
 *                                      2.  Added member delegates for each of the delegate declarations.
 *                                          
 *                                      3.  Modified the zero parameter constructor to instantiate the delegates with either the 
 *                                          32 or 64 bit version of the corresponding method depending upon the current state of the 
 *                                          'Environment.Is64BitOperatingSystem' variable.
 *                                          
 *                                      4.  Modified each of the methods to check that the function delegate has been initialized prior to its use.
 *                                          
 *                                      5.  Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted, 
 *                                          the method was modified to a check that the Mutex has been initialized prior to its use.
 *                                          
 *                                      6.  Replaced all calls to the methods within PTUDLL32.dll with calls via function delegates. This allows 
 *                                          support for both 32 and 64 bit systems.
 *                                          
 *                                      7. Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted, 
 *                                          a 'finally' block was added to each 'try' block to ensure that the Mutex is released even if an exception 
 *                                          occurs. The code pattern was modified to use the following template:
 *                                          
 *                                          CommunicationError errorCode = CommunicationError.UnknownError;
 *                                          try
 *                                          {
 *                                              m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
 *                                              errorCode = (CommunicationError)m_<function-name>( ... );
 *                                          }
 *                                          catch (Exception)
 *                                          {
 *                                              errorCode = CommunicationError.SystemException;
 *                                              throw new CommunicationException(Resources.EMGetTargetConfigurationFailed, errorCode);
 *                                          }
 *                                          finally
 *                                          {
 *                                              m_MutexCommuncationInterface.ReleaseMutex();
 *                                          }
 *                                          
 *                                          if (DebugMode.Enabled == true)
 *                                          {
 *                                              ...
 *                                          }
 *
 *                                          if (errorCode != CommunicationError.Success)
 *                                          {
 *                                              throw new CommunicationException("<function-name>", errorCode);
 *                                          }
 *                                          
 *                                      8.  Added the unsafe keyword to the delegate declaration for the GetSelfTestResultDelegate() method as
 *                                          the '(byte*) results' parameter can only be used in an unsafe environment. Also modified the
 *                                          GetSelfTestResult() method to make the call via the function delegate within a section of code marked
 *                                          as unsafe.
 *                                          
 *                                      9.  Corrected the name of the StartSelfTestTask() method.
 */
#endregion - [1.2] -
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using Common;
using Common.Communication;
using Common.Configuration;

namespace SelfTest.Communication
{
    /// <summary>
    /// Class to manage the communication with the target hardware with respect to the diagnostic self test sub-system commands.
    /// </summary>
    public class CommunicationSelfTest : CommunicationParent, ICommunicationSelfTest
    {
        #region --- Delegate Declarations ---
        /// <summary>
        /// Get the self test special message.
        /// </summary>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the
        /// <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short GetSelfTestSpecialMessageDelegate(out short result, out short reason);

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
        protected delegate short StartSelfTestTaskDelegate(out short result, out short reason);

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
        protected delegate short ExitSelfTestTaskDelegate(out short result, out short reason);

        /// <summary>
        /// Abort the self test sequence.
        /// </summary>
        /// <remarks>This request will stop the execution of the self-test process on the VCU and return control to the propulsion software.</remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short AbortSTSequenceDelegate();

        /// <summary>
        /// Send an operator acknowledge message.
        /// </summary>
        /// <remarks>This request allows the operator to move to the next step of an interactive test.</remarks>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short SendOperatorAcknowledgeDelegate();

        /// <summary>
        /// Update the list of individually selected self tests that are to be executed. 
        /// </summary>
        /// <remarks>This method will define the list of self-tests that are to be executed once the tester selects the execute command. The self tests
        /// are defined using the self test identifiers defined in the data dictionary.</remarks>
        /// <param name="testCount">The number of tests in the list.</param>
        /// <param name="tests">A list of the self test identifiers.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short UpdateSTTestListDelegate(short testCount, short[] tests);

        /// <summary>
        /// Run the predefined self tests associated with the specified test list identifier, these tests are defined in the data dictionary. 
        /// </summary>
        /// <param name="testListIdentifier">The test list identifier of the predefined self tests that are to be executed.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short RunPredefinedSTTestsDelegate(short testListIdentifier);

        /// <summary>
        /// Update the number of times that the selected tests are to be run.
        /// </summary>
        /// <param name="loopCount">The number of cycles/loops of the defined tests that are to be performed.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short UpdateSTLoopCountDelegate(short loopCount);

        /// <summary>
        /// Execute the self tests that are defined in the current list.
        /// </summary>
        /// <param name="truckInformation">The truck to which the self tests apply. This does not apply on the CTA project, separate self-tests are set
        /// up for each truck.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short ExecuteSTTestListDelegate(short truckInformation);

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
        unsafe protected delegate short GetSelfTestResultDelegate(out short resultAvailable, out MessageMode messageMode, out short testIdentifier,
                                                                  out short testCase, out short testResult, out TruckInformation truckInformation,
                                                                  out short variableCount, byte* results);

        /// <summary>
        /// Update the self test mode.
        /// </summary>
        /// <remarks>This call is used to check whether communication with the VCU has been lost.</remarks>
        /// <param name="selfTestMode">The required self test mode.</param>
        /// <returns>Success, if the communication request was successful; otherwise, an error code.</returns>
        protected delegate short UpdateSTModeDelegate(SelfTestMode selfTestMode);
        #endregion --- Delegate Declarations ---

        #region --- Member Variables ---
        #region --- Function Delegates  ---
        /// <summary>
        /// Delegate for the GetSelfTestSpecialMessage() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetSelfTestSpecialMessageDelegate m_GetSelfTestSpecialMessage;

        /// <summary>
        /// Delegate for the StartSelfTestTask() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected StartSelfTestTaskDelegate m_StartSelfTestTask;
        
        /// <summary>
        /// Delegate for the ExitSelfTestTask() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected ExitSelfTestTaskDelegate m_ExitSelfTestTask;

        /// <summary>
        /// Delegate for the AbortSTSequence() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected AbortSTSequenceDelegate m_AbortSTSequence;

        /// <summary>
        /// Delegate for the SendOperatorAcknowledge() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected SendOperatorAcknowledgeDelegate m_SendOperatorAcknowledge;

        /// <summary>
        /// Delegate for the UpdateSTTestList() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected UpdateSTTestListDelegate m_UpdateSTTestList;

        /// <summary>
        /// Delegate for the RunPredefinedSTTests() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected RunPredefinedSTTestsDelegate m_RunPredefinedSTTests;

        /// <summary>
        /// Delegate for the UpdateSTLoopCount() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected UpdateSTLoopCountDelegate m_UpdateSTLoopCount;

        /// <summary>
        /// Delegate for the ExecuteSTTestList() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected ExecuteSTTestListDelegate m_ExecuteSTTestList;

        /// <summary>
        /// Delegate for the GetSelfTestResult() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected GetSelfTestResultDelegate m_GetSelfTestResult;

        /// <summary>
        /// Delegate for the UpdateSTMode() method of VcuCommunication32.dll/VcuCommunication64.dll.
        /// </summary>
        protected UpdateSTModeDelegate m_UpdateSTMode;
        #endregion --- Function Delegates  ---
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class and set the function delegates, properties and member variables to those values associated with the
        /// specified communication interface.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface containing the properties and member variables that are to be
        /// used to initialize the class.</param>
        public CommunicationSelfTest(ICommunicationParent communicationInterface)
            : base(communicationInterface)
        {
            #region - [Initialize VcuCommunication.selftest.cpp Function Delegates] -
            // ----------------------------------------------------------------------
            // Initialize the function delegates to either the VcuCommunication32.dll  
            // or VcuCommunication64.dll functions depending upon whether the 
            // Windows operating system is 64 bit or 32 bit.
            // ----------------------------------------------------------------------
            if (m_Is64BitOperatingSystem == true)
            {
                m_GetSelfTestSpecialMessage = VcuCommunication64SelfTest.GetSelfTestSpecialMessage;
                m_StartSelfTestTask = VcuCommunication64SelfTest.StartSelfTestTask;
                m_ExitSelfTestTask = VcuCommunication64SelfTest.ExitSelfTestTask;
                m_AbortSTSequence = VcuCommunication64SelfTest.AbortSTSequence;
                m_SendOperatorAcknowledge = VcuCommunication64SelfTest.SendOperatorAcknowledge;
                m_UpdateSTTestList = VcuCommunication64SelfTest.UpdateSTTestList;
                m_RunPredefinedSTTests = VcuCommunication64SelfTest.RunPredefinedSTTests;
                m_UpdateSTLoopCount = VcuCommunication64SelfTest.UpdateSTLoopCount;
                m_ExecuteSTTestList = VcuCommunication64SelfTest.ExecuteSTTestList;
                unsafe
                {
                    m_GetSelfTestResult = VcuCommunication64SelfTest.GetSelfTestResult;
                }
                m_UpdateSTMode = VcuCommunication64SelfTest.UpdateSTMode;
            }
            else
            {
                m_GetSelfTestSpecialMessage = VcuCommunication32SelfTest.GetSelfTestSpecialMessage;
                m_StartSelfTestTask = VcuCommunication32SelfTest.StartSelfTestTask;
                m_ExitSelfTestTask = VcuCommunication32SelfTest.ExitSelfTestTask;
                m_AbortSTSequence = VcuCommunication32SelfTest.AbortSTSequence;
                m_SendOperatorAcknowledge = VcuCommunication32SelfTest.SendOperatorAcknowledge;
                m_UpdateSTTestList = VcuCommunication32SelfTest.UpdateSTTestList;
                m_RunPredefinedSTTests = VcuCommunication32SelfTest.RunPredefinedSTTests;
                m_UpdateSTLoopCount = VcuCommunication32SelfTest.UpdateSTLoopCount;
                m_ExecuteSTTestList = VcuCommunication32SelfTest.ExecuteSTTestList;
                unsafe
                {
                    m_GetSelfTestResult = VcuCommunication32SelfTest.GetSelfTestResult;
                }
                m_UpdateSTMode = VcuCommunication32SelfTest.UpdateSTMode;
            }
            #endregion - [Initialize VcuCommunication.selftest.cpp Function Delegates] -
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Get the self test special message.
        /// </summary>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the
        /// <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetSelfTestSpecialMessage()
        /// method is not CommunicationError.Success.</exception>
        public void GetSelfTestSpecialMessage(out short result, out short reason)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetSelfTestSpecialMessage != null,
                         "CommunicationSelfTest.GetSelfTestSpecialMessage() - [m_GetSelfTestSpecialMessage != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.GetSelfTestSpecialMessage() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetSelfTestSpecialMessage(out result, out reason);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.GetSelfTestSpecialMessage()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetSelfTestSpecialMessage_t getSelfTestSpecialMessage = new DebugMode.GetSelfTestSpecialMessage_t(result, reason, errorCode);
                DebugMode.Write(getSelfTestSpecialMessage.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.GetSelfTestSpecialMessage()", errorCode);
            }
        }

        /// <summary>
        /// Start the self test task.
        /// </summary>
        /// <remarks>This request will start the self test process on the VCU.</remarks>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the
        /// <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.StartSelfestTask() method is not 
        /// CommunicationError.Success.</exception>
        public void StartSelfTestTask(out short result, out short reason)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_StartSelfTestTask != null,
                         "CommunicationSelfTest.StartSelfestTask() - [m_StartSelfTestTask != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.StartSelfTestTask() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_StartSelfTestTask(out result, out reason);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.StartSelfTestTask()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.StartSelfTestTask_t startSelfTestTask = new DebugMode.StartSelfTestTask_t(result, reason, errorCode);
                DebugMode.Write(startSelfTestTask.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.StartSelfTestTask()", errorCode);
            }
        }

        /// <summary>
        /// Exit the self test task.
        /// </summary>
        /// <remarks>This request will end the self test process on the VCU.</remarks>
        /// <param name="result">The result of the call. A value of: (1) 1 represents success; (2) indicates that the error message defined by the 
        /// <paramref name="reason"/> parameter applies and (3) represents an unknown error.</param>
        /// <param name="reason">A value of 1 represents success; otherwise, the value is mapped to the <c>ERRID</c> field of the
        /// <c>SELFTESTERRMESS</c> table 
        /// of the data dictionary in order to determine the error message returned from the VCU.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.ExitSelfestTask() method is not 
        /// CommunicationError.Success.</exception>
        public void ExitSelfTestTask(out short result, out short reason)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_ExitSelfTestTask != null,
                         "CommunicationSelfTest.ExitSelfTestTask() - [m_ExitSelfTestTask != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.ExitSelfTestTask() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_ExitSelfTestTask(out result, out reason);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.ExitSelfTestTask()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.ExitSelfTestTask_t exitSelfTestTask = new DebugMode.ExitSelfTestTask_t(result, reason, errorCode);
                DebugMode.Write(exitSelfTestTask.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.ExitSelfestTask()", errorCode);
            }
        }

        /// <summary>
        /// Abort the self test sequence.
        /// </summary>
        /// <remarks>This request will stop the execution of the self-test process on the VCU and return control to the propulsion software.</remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.AbortSTSequence() method is not 
        /// CommunicationError.Success.</exception>
        public void AbortSTSequence()
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_AbortSTSequence != null,
                         "CommunicationSelfTest.AbortSTSequence() - [m_AbortSTSequence != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.AbortSTSequence() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_AbortSTSequence();
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.AbortSTSequence()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.AbortSTSequence_t abortSTSequence = new DebugMode.AbortSTSequence_t(errorCode);
                DebugMode.Write(abortSTSequence.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.AbortSTSequence()", errorCode);
            }
        }

        /// <summary>
        /// Send an operator acknowledge message.
        /// </summary>
        /// <remarks>This request allows the operator to move to the next step of an interactive test.</remarks>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SendOperatorAcknowledge() method
        /// is not CommunicationError.Success.</exception>
        public void SendOperatorAcknowledge()
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_SendOperatorAcknowledge != null,
                         "CommunicationSelfTest.SendOperatorAcknowledge() - [m_SendOperatorAcknowledge != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.SendOperatorAcknowledge() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_SendOperatorAcknowledge();
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.SendOperatorAcknowledge()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.SendOperatorAcknowledge_t sendOperatorAcknowledge = new DebugMode.SendOperatorAcknowledge_t(errorCode);
                DebugMode.Write(sendOperatorAcknowledge.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.SendOperatorAcknowledge()", errorCode);
            }
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
            // Check that the function delegate has been initialized.
            Debug.Assert(m_UpdateSTTestList != null,
                         "CommunicationSelfTest.UpdateSTTestList() - [m_UpdateSTTestList != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.UpdateSTTestList() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_UpdateSTTestList(testCount, tests);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.UpdateSTTestList()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.UpdateSTTestList_t updateSTTestList = new DebugMode.UpdateSTTestList_t(testCount, tests, errorCode);
                DebugMode.Write(updateSTTestList.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.UpdateSTTestList()", errorCode);
            }
        }

        /// <summary>
        /// Run the predefined self tests associated with the specified test list identifier, these tests are defined in the data dictionary. 
        /// </summary>
        /// <param name="testListIdentifier">The test list identifier of the predefined self tests that are to be executed.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.RunPredefinedSTTests() method is
        /// not CommunicationError.Success.</exception>
        public void RunPredefinedSTTests(short testListIdentifier)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_RunPredefinedSTTests != null,
                         "CommunicationSelfTest.RunPredefinedSTTests() - [m_RunPredefinedSTTests != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.RunPredefinedSTTests() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_RunPredefinedSTTests(testListIdentifier);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.RunPredefinedSTTests()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.RunPredefinedSTTests_t runPredefinedSTTests = new DebugMode.RunPredefinedSTTests_t(testListIdentifier, errorCode);
                DebugMode.Write(runPredefinedSTTests.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.RunPredefinedSTTests()", errorCode);
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
            // Check that the function delegate has been initialized.
            Debug.Assert(m_UpdateSTLoopCount != null,
                         "CommunicationSelfTest.UpdateSTLoopCount() - [m_UpdateSTLoopCount != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.UpdateSTLoopCount() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_UpdateSTLoopCount(loopCount);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.UpdateSTLoopCount()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.UpdateSTLoopCount_t updateSTLoopCount = new DebugMode.UpdateSTLoopCount_t(loopCount, errorCode);
                DebugMode.Write(updateSTLoopCount.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.UpdateSTLoopCount()", errorCode);
            }
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
            // Check that the function delegate has been initialized.
            Debug.Assert(m_ExecuteSTTestList != null,
                         "CommunicationSelfTest.ExecuteSTTestList() - [m_ExecuteSTTestList != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.ExecuteSTTestList() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_ExecuteSTTestList((short)truckInformation);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.ExecuteSTTestList()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.ExecuteSTTestList_t executeSTTestList = new DebugMode.ExecuteSTTestList_t(truckInformation, errorCode);
                DebugMode.Write(executeSTTestList.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.ExecuteSTTestList()", errorCode);
            }
        }

        /// <summary>
        /// Get the self test results.
        /// </summary>
        /// <param name="resultAvailable">A flag to indicate whether a valid result is available. A value of 1 indicates that a valid result is
        /// available; otherwise,  0.</param>
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
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetSelfTestResult != null,
                         "CommunicationSelfTest.GetSelfTestResult() - [m_GetSelfTestResult != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.GetSelfTestResult() - [m_MutexCommuncationInterface != null]");

            results = new InteractiveResults_t[Parameter.WatchSizeInteractiveTest];
            int sizeOfInteractiveResultInPTUDLL32 = sizeof(double) + sizeof(int);
            byte[] interactiveResultsAsByteArray = new byte[Parameter.WatchSizeInteractiveTest * sizeOfInteractiveResultInPTUDLL32];

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                unsafe
                {
                    fixed (byte* interactiveResults = &interactiveResultsAsByteArray[0])
                    {
                        errorCode = (CommunicationError)m_GetSelfTestResult(out resultAvailable, out messageMode,
                                                                            out testIdentifier, out testCase, out testResult,
                                                                            out truckInformation, out variableCount,
                                                                            interactiveResults);
                    }
                }
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.GetSelfTestResult()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            // Convert the byte array returned from the call to an array of InteractiveResults_t structures.
            int offset = 0;
            for (int index = 0; index < variableCount; index++)
            {
                offset = index * sizeOfInteractiveResultInPTUDLL32;
                results[index].Value = BitConverter.ToDouble(interactiveResultsAsByteArray, offset);
                results[index].Tag = BitConverter.ToInt32(interactiveResultsAsByteArray, offset + sizeof(double));
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.GetSelfTestResult_t getSelfTestResult = new DebugMode.GetSelfTestResult_t(resultAvailable, messageMode, testIdentifier,
                                                                                                    testCase, testResult, truckInformation,
                                                                                                    variableCount, results, errorCode);
                DebugMode.Write(getSelfTestResult.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.GetSelfTestResult()", errorCode);
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
            // Check that the function delegate has been initialized.
            Debug.Assert(m_UpdateSTMode != null,
                         "CommunicationSelfTest.UpdateSTMode() - [m_UpdateSTMode != null]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationSelfTest.UpdateSTMode() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_UpdateSTMode(selfTestMode);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException("CommunicationSelfTest.UpdateSTMode()", errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.UpdateSTMode_t updateSTMode = new DebugMode.UpdateSTMode_t(selfTestMode, errorCode);
                DebugMode.Write(updateSTMode.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException("CommunicationSelfTest.UpdateSTMode()", errorCode);
            }
        }
        #endregion --- Methods ---
    }
}
