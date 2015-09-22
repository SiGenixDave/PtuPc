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
 *  File name:  IPlotterWach.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/16/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  03/28/11    1.1     K.McD           1.  Renamed the WatchIdentifier property.
 *  
 *  10/07/11    1.2     K.McD           1.  Added the Removed property to the interface definition. This flag indicates whether the user has actively removed the plotter 
 *                                          control from the current plot display.
 *                                          
 *  10/25/11    1.3     K.McD           1.  Added the Visible property of the control.
 *                                      2.  Made the Removed property read/write.
 *                                      3.  Included the SetHighlight() method to set the <c>UserControl</c> to the specified highlighted state.
 *                                      
 *  08/06/13    1.4     K.McD           1.  Added the 'Remove Selected Plot(s)' ToolStripMenuItem control as a property to allow client programs to access the properties of this 
 *                                          context menu option.
 *
 */
#endregion --- Revision History ---

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.UserControls
{
    /// <summary>
    /// Interface associated with any user control which can be used to plot historic data.
    /// </summary>
    public interface IPlotterWatch
    {
        #region - [Methods] -
        /// <summary>
        /// Dispose of the user control.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Reset the <c>Plotter</c> user control. Erases the graph and gets ready to start the whole plotting process again.
        /// </summary>
        void Reset();

        /// <summary>
        /// Start plotting.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop plotting. The user can now view the graphs
        /// </summary>
        void Stop();

        /// <summary>
        /// Update the graph display. Call this method once all the data values have been set so that the changes are displayed on the graph.	
        /// </summary>
        void UpdateDisplay();

        /// <summary>
        /// Set the <c>UserControl</c> to the specified highlighted state.
        /// </summary>
        /// <param name="value">True, to highlight the <c>UserControl</c>; otherwise, false.</param>
        void SetHighlight(bool value);
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets the reference to <c>Plotter</c> user control; enables the programmer to access the <c>Plotter</c> properties and events.
        /// </summary>
        CodeProject.GraphComponents.Plotter Plot { get; }

        /// <summary>
        /// Gets the channel collection associated with the plot.
        /// </summary>
        CodeProject.GraphComponents.ChannelCollection Channels { get; }

        /// <summary>
        /// Gets or sets the description text associated with the watch variable.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the watch identifier associated with the watch variable that is to be plotted.
        /// </summary>
        short Identifier { get; set; }

        /// <summary>
        /// Gets or sets the flag used to indicate that the data corresponds to a fault log.
        /// </summary>
        bool IsFaultLog { get; set; }

        /// <summary>
        /// Gets or sets the time interval, in ms, between successive plots. Used when displaying live data.
        /// </summary>
        int PlotIntervalMs { get; set; }

        /// <summary>
        /// Gets or sets the start time of the plot.
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the time span of the X axis.
        /// </summary>
        TimeSpan XRange { get; set; }

        /// <summary>
        /// Gets or sets the time of the actal trip.
        /// </summary>
        DateTime TripTime { get; set; }

        /// <summary>
        /// Gets or sets the display style of the time axis.
        /// </summary>
        CodeProject.GraphComponents.TimeAxisStyle TimeDisplayStyle { get; set; }

        /// <summary>
        /// Gets or sets the BackColor of the user control.
        /// </summary>
        Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the Font of the user control.
        /// </summary>
        Font Font { get; set; }

        /// <summary>
        /// Gets or sets the size of the control, in pixels.
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// Gets or sets the flag that indicates whether the user has removed this plot from the display.
        /// </summary>
        bool Removed { get; set; }

        /// <summary>
        /// Gets or sets the flag that determines whether the <c>Control</c> is visible or hidden.
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Gets the ToolStripMenuItem control associated with the 'Remove Selected Plot(s)' context menu option.
        /// </summary>
        ToolStripMenuItem RemoveSelectedPlot { get; } 
        #endregion - [Properties] -
    }
}
