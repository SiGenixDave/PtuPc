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
 *  File name:  ICommunicationApplication.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  03/26/15    1.1    K.McD            1.  Modified the SetTimeDate() and GetTimeDate() methods to include the additional parameter that was introduced 
 *                                          to specify whether the Vehicle Control Unit uses a 2 or 4 digit year code.
 */
#endregion --- Revision History ---

using System;

namespace Common.Communication
{
    /// <summary>
    /// An interface to define the communication methods associated with the main Portable Test Unit application - PTU.exe.
    /// </summary>
    public interface ICommunicationApplication : ICommunicationParent
    {
        /// <summary>
        /// Get the date and time from the target hardware.
        /// </summary>
        /// <param name="use4DigitYearCode">A flag that specifies whether the Vehicle Control Unit uses a 2 or 4 digit year code. True, if it
        /// uses a 4 digit year code; otherwise, false.</param>
        /// <param name="dateTime">The the date and time as a .NET <c>DateTime</c> object.</param>
        void GetTimeDate(bool use4DigitYearCode, out DateTime dateTime);

        /// <summary>
        /// Set the date and time of the target hardware.
        /// </summary>
        /// <param name="use4DigitYearCode">A flag that specifies whether the Vehicle Control Unit uses a 2 or 4 digit year code. True, if it
        /// uses a 4 digit year code; otherwise, false.</param>
        /// <param name="dateTime">The date and time as a .NET <c>DateTime</c> object.</param>
        void SetTimeDate(bool use4DigitYearCode, DateTime dateTime);

        /// <summary>
        /// Set the car identifier.
        /// </summary>
        /// <param name="carIdentifier"></param>
        void SetCarID(string carIdentifier);

        /// <summary>
        /// Scan the specified serial communication port to determine if it is connected to a target logic controller. If a target is found
        /// the target configuration information is written to the output parameter <paramref name="targetConfiguration"/>.
        /// </summary>
        /// <param name="communicationSetting">The communication settings that are to be used to communicate with the target.</param>
        /// <param name="targetConfiguration">The target configuration information returned from the target hardware if a target is found.</param>
        /// <returns>A flag to indicate whether a target was found; true, if a target was found, otherwise, false.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.InitCommunication() method is not
        /// CommunicationError.Success.</exception>
        bool ScanPort(Common.Communication.CommunicationSetting_t communicationSetting, out Common.Communication.TargetConfiguration_t targetConfiguration);
    }
}
