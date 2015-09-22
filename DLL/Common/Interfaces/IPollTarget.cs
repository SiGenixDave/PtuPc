#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    Common
 * 
 *  File name:  IPollTarget.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/04/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  08/24/11    1.1     K.McD           1.  Modified the signature of the SetPauseAndWait() method.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common
{
    /// <summary>
    /// An interface to control polling of the target hardware. 
    /// </summary>
    /// <remarks>The interface in independent of the type of command used to poll the target hardware.</remarks>
    public interface IPollTarget
    {
        #region - [Methods] -
        /// <summary>
        /// Start polling the target hardware.
        /// </summary>
        void StartPolling();
        
        /// <summary>
        /// Stop polling the target hardware. 
        /// </summary>
        void StopPolling();

        /// <summary>
        /// Set the Pause property and wait until the feedback signal is received or until the timeout has elapsed.
        /// </summary>
        /// <param name="timeoutMs">The timeout period, in ms.</param>
        /// <returns>A flag to indicate whether the pause feedback signal was asserted within the specified timeout. True, if the pause feedback signal was asserted 
        /// within the specified timeout; otherwise, false.</returns>
        bool SetPauseAndWait(int timeoutMs);
        #endregion - [Methods] -

        #region - [Properties ] -
        /// <summary>
        /// Gets or sets the flag that controls the polling of the target hardware. True, inhibits polling of the target hardware; otherwise, false, resumes polling.
        /// </summary>
        bool Pause { get; set; }

        /// <summary>
        /// Gets the feedback flag that indicates whether polling of the target hardware has been suspended.  
        /// </summary>
        /// <remarks>This flag is asserted when the class has entered the pause state, i.e. the current communication request is complete and 
        /// no further requests will be issued until the pause property has been cleared.</remarks>
        bool PauseFeedback { get;}
        #endregion - [Properties ] -
    }
}
