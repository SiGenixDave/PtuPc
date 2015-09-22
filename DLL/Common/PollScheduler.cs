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
 *  File name:  PollScheduler.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  12/17/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;
using System.Runtime.InteropServices;

namespace Common
{
    /// <summary>
    /// Used by the <c>WorkerThread</c> derived classes that are used to poll the VCU to ensure that polling occurs at a specific interval.
    /// </summary>
    /// <remarks>Uses the <c>winmm.dll</c> dynamic link library to control the timing.</remarks>
    public class PollScheduler
    {
        #region --- DLL Import Functions ---
        [DllImport("winmm.dll")]
        private static extern int timeGetTime();
        #endregion --- DLL Import Functions ---

        #region --- Member Variables ---
        /// <summary>
        /// The time, as returned from the call to the timeGetTime() method of winmm.dll, associated with the previous call to the Wait() method. 
        /// </summary>
        private long m_TimePreviousPass;

        /// <summary>
        /// The flag that can terminate the Wait() method immediately. True, to terminate the Wait() method immediately; otherwise, false.
        /// </summary>
        private bool m_TerminateFlag;

        /// <summary>
        /// The flag to indicate whether the current call to the Wait() method is the first call since the class was instantiated.
        /// </summary>
        private bool m_IsFirstCall = true;
        #endregion --- Member Variables ---

        /// <summary>
        /// Wait on the current thread until: (a) <paramref name="intervalMs"/> ms has elapsed since the last call to the Wait() method or (b) the TerminateFlag property 
        /// is asserted.
        /// </summary>
        /// <param name="intervalMs">The number of ms to wait since the last call to the Wait() method before continuing.</param>
        public void Wait(int intervalMs)
        {
            long currentIntervalMs, timeCurrentPassMs = 0;

            // Check whether this is the first poll.
            if (m_IsFirstCall)
            {
                m_TimePreviousPass = timeGetTime();
                m_IsFirstCall = false;
            }
            else
            {
                // Wait until the specified interval has elapsed.
                do
                {
                    // Break out of the loop if the Terminate property has been asserted.
                    if (TerminateFlag == true)
                    {
                        break;
                    }
                    timeCurrentPassMs = timeGetTime();
                    currentIntervalMs = timeCurrentPassMs - m_TimePreviousPass;
                }
                while (currentIntervalMs < intervalMs);

                m_TimePreviousPass = timeCurrentPassMs;
            }
        }

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the flag that can terminate the Wait() method immediately. True, to terminate the Wait() method immediately; otherwise, false.
        /// </summary>
        public bool TerminateFlag
        {
            get { return m_TerminateFlag; }
            set { m_TerminateFlag = value; }
        }
        #endregion --- Properties ---
    }
}
