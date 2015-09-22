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
 *  File name:  IPlotterEnumerator.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.UserControls
{
    /// <summary>
    /// Interface associated with a user control that is used to plot scalar watch variables.
    /// </summary>
    public interface IPlotterEnumerator : IPlotterWatch
    {
        /// <summary>
        /// Gets or sets the engineering units associated with the watch variable.
        /// </summary>
        string Units { get; set; }
    }
}
