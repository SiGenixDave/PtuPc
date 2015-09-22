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
 *  File name:  CommunicationException.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/24/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

using System;

using Common;

namespace Common.Communication
{
    /// <summary>
    /// User defined exception class for throwing a communication exceptions.
    /// </summary>
    public class CommunicationException : Exception
    {
        #region --- Member Variables ---
        /// <summary>
        /// The communication error code.
        /// </summary>
        private CommunicationError m_CommunicationError;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the communications exception class.
        /// </summary>
        /// <param name="auxMessage">A message to be passed to the exception handler.</param>
        public CommunicationException(string auxMessage)
            : base(String.Format("{0}", auxMessage))
        {
        }

        /// <summary>
        /// Initializes a new instance of the communications exception class. This constructor is used when the exception is thrown as a result of receiving an error
        /// code other than CommunicationError.Success from a call to one of the methods included in PTUDLL32.dll.
        /// </summary>
        /// <remarks>The PTUDLL32.dll dynamic link library is a set of communication methods developed in C++ to support communication with the VCU.</remarks>
        /// <param name="auxMessage">A message to be passed to the exception handler.</param>
        /// <param name="communicationError">The error code returned from the call to the PTUDLL32 dynamic link library..</param>
        public CommunicationException(string auxMessage, CommunicationError communicationError)
            : base(String.Format("{0}", auxMessage))
        {
            m_CommunicationError = communicationError;
        }

        /// <summary>
        /// Initializes a new instance of the communications exception class. This constructor is used when the exception is thrown as a result of catching a 
        /// different type of exception. The initial exception information is also passed to the exception handler to supplement the information passed via this
        /// exception.
        /// </summary>
        /// <param name="auxMessage">A message to be passed to the exception handler.</param>
        /// <param name="innerException">The initial exception which triggered this exception.</param>
        public CommunicationException(string auxMessage, Exception innerException)
            : base(String.Format("{0}", auxMessage), innerException)
        {

        }
        #endregion

        #region --- Properties ---
        /// <summary>
        /// Gets the communication error code.
        /// </summary>
        public CommunicationError CommunicationError
        {
            get { return m_CommunicationError; }
        }
        #endregion --- Properties ---
    }

}
