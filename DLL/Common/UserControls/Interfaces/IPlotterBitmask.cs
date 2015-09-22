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
 *  File name:  IPlotterBitmask.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.UserControls
{
    /// <summary>
    /// Interface associated with a user control that is used to plot individual bits of a bitmask watch variables.
    /// </summary>
    public interface IPlotterBitmask : IPlotterWatch
    {
        /// <summary>
        /// Gets or sets the alarm state associated with the plot. The default state is true i.e. a low to high transition represents an alarm.
        /// </summary>
        bool AlarmState { get; set; }

        /// <summary>
        /// Gets or sets the bit of the watch variable value that is to be plotted.
        /// </summary>
        byte Bit { get; set; }
    }
}
