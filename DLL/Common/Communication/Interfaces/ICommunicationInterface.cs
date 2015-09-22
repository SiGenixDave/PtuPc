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
 *  File name:  ICommunicationInterface
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/13/10    1.0     K.McDonald      1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

namespace Common.Communication
{
    /// <summary>
    /// Defines the interface requirements for any class that communicates with the vehicle control unit.
    /// </summary>
    /// <typeparam name="T">The communication interface type e.g. ICommunicationWatch, ICommunicationEvent etc.</typeparam>
    public interface ICommunicationInterface<T> where T : ICommunicationParent
    {
        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the target.
        /// </summary>
        T CommunicationInterface { get; set; }
    }
}
