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
 *  File name:  WibuBox.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  06/19/13    1.0     K.McDonald      First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Configuration
{
    /// <summary>
    /// A structure to store the WibuBox security device fields stored in the <c>CONFIGUREPTU</c> data table of the data dictionary.
    /// </summary>
    [Serializable]
    public struct WibuBox
    {
        #region --- Member Variables ---
        /// <summary>
        /// The client's Firm Code.
        /// </summary>
        private int m_FirmCode;

        /// <summary>
        /// The client's User code.
        /// </summary>
        private int m_UserCode;

        /// <summary>
        /// The slot where the Firm and User codes are programmed.
        /// </summary>
        private short m_SlotId;

        /// <summary>
        /// The port number where the WibuBox is connected (Not Used).
        /// </summary>
        private short m_PortId;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the structure.
        /// </summary>
        /// <param name="firmCode">The client's Firm code.</param>
        /// <param name="userCode">The client's User code.</param>
        /// <param name="slotId">The slot where the Firm and User codes are programmed.</param>
        /// <param name="portId">The port number where the WibuBox is connected (Not Used).</param>
        public WibuBox(int firmCode, int userCode, short slotId, short portId)
        {
            m_FirmCode = firmCode;
            m_UserCode = userCode;
            m_SlotId = slotId;
            m_PortId = portId;
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the client's Firm Code.
        /// </summary>
        public int FirmCode
        {
            get { return m_FirmCode; }
            set { m_FirmCode = value; }
        }

        /// <summary>
        /// Gets or sets the client's User code.
        /// </summary>
        public int UserCode
        {
            get { return m_UserCode; }
            set { m_UserCode = value; }
        }

        /// <summary>
        /// Gets or sets the slot where the Firm and User codes are programmed.
        /// </summary>
        public short SlotId
        {
            get { return m_SlotId; }
            set { m_SlotId = value; }
        }

        /// <summary>
        /// Gets or sets the port number where the WibuBox is connected (Not Used).
        /// </summary>
        public short PortId
        {
            get { return m_PortId; }
            set { m_PortId = value; }
        }
        #endregion --- Properties ---
    }
}
