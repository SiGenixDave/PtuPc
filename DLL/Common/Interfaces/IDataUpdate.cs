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
 *  File name:  IDataUpdate.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/20/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common
{
    /// <summary>
    /// Interface for any form that publishes a DataUpdate event to indicate that the data that is being displayed on the form has been updated.
    /// </summary>
    public interface IDataUpdate
    {
        /// <summary>
        /// Occurs whenever the data on display is updated.
        /// </summary>
        event EventHandler DataUpdate;
    }
}
