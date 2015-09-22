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
 *  Project:    Watch
 * 
 *  File name:  IChartScale
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/19/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;

namespace Watch.Forms
{
    /// <summary>
    /// An interface to allow the FormChangeChartScale form to modify the chart recorder Y axis upper and lower limits. 
    /// 
    /// </summary>
    internal interface IChartScale
    {
        /// <summary>
        /// The <c>ListBox</c> control that contains the lower limits of the Y axis for each chart recorder channel.
        /// </summary>
        System.Windows.Forms.ListBox ListBoxChartScaleLowerLimit { get; }

        /// <summary>
        /// The <c>ListBox</c> control that contains the upper limits of the Y axis for each chart recorder channel.
        /// </summary>
        System.Windows.Forms.ListBox ListBoxChartScaleUpperLimit { get; }
    }
}
