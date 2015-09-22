#region --- Revision History ---
/*
 * 
 *  This class was developed under the terms of the Code Project Open Source License agreement (CPOL), see CPOL.html, and was originally 
 *  written by Anup. V (anupshubha@yahoo.com). The CPOL is intended to provide those developers who choose to share their code with a 
 *  license that protects them and provides users of their code with a clear statement regarding how the code can be used.
 * 
 *  Under the terms and conditions of the CPOL, all derivative work must also be developed under the same licence agreement. The full 
 *  CPOL terms and conditions are given in the file CPOL.html located in the 'Solution Items' directory.
 * 
 *  (C) 2007 - 2010    The Code Project
 *
 *  Solution:   PTU
 * 
 *  Project:    GraphComponents
 * 
 *  File name:  Plotter.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/30/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  10/06/10    1.1     K.McD           1.  Reformatted the layout in accordance with the iDesign coding standards. Specifically, comments now appear at the same level
 *                                          of indentation as the code.
 * 
 *                                      2.  Modified the text associated with a number of XML tags.
 * 
 *                                      3.  Specified the lower limit parameter of the call to FindY() in method DrawXYText() using a constant rather than a numeric
 *                                          value.
 * 
 *  10/15/10    1.2     K.McD           1.  Set the lower parameter limit of FindY() to 3 data intervals so that the plotter does not display a blank line during the
 *                                          write watch variable operation.
 * 
 *  02/28/11    1.3     K.McD           1.  Changed the modifier associated with the m_GraphAreaNormalColor member variable.
 * 
 *  03/31/11    1.4     K.McD           1.  Included support to write the y axis values and the cursor value in the specified format.
 * 
 *                                              a.  Added a number of FormatString constants.
 *                                              b.  Added a flag to specify whether the values are to be wriiten in hexadecimal format.
 *                                              c.  Overrode the ValueFormat property of the child e class to update the flag that specifies whether the values are to 
 *                                                  be plotted in hexadecimal format.
 *                                              d.  Modified the DrawYAxisValues() and DrawXYText() methods to check whether the value is to be displayed in hexadecimal 
 *                                                  format and, if so, to convert the y value to a long integer in preparation for converting it to a hexadecimal string.
 * 
 *                                      2.  Modified a number of XML tags.
 *                                      
 *                                      3.  Modified the layout to match the coding standard.
 * 
 *                                      4.  Made the DrawYAxisValues() method protected and virtual to allow the enumerator plotter child class to be implemented as 
 *                                          this user control does not display any y axis values.
 *                                          
 *	09/26/11	1.4.1	Sean.D			1.	Modified the FindY() method to make use of the return value to find the next lowest value without using recursive calls.
 *	                                    2.  Modified the DrawXY() method to take into account the changes to the signature of the FindXY() method.
 *                                      3.  Auto-modified as a result of name changes to a number of controls in the designer class.
 *                                      
 *  10/05/11	1.4.2	Sean.D			1.	Added the m_MultiCursor member variable and the corresponding property of MultiCursor to allow the PlotControlLayout to
 *											change certain properties of the methods when multiple cursors are displayed.
 *										2.	Modified Plotter_MouseLeave to not set the hover coordinates to zero when fired in MultiCursor is true. This allows cursor
 *											display even after leaving the plot.
 *										3.	Removed Plotter_MouseMove so that PlotterControlLayout handles it.
 *										4.	Added MouseHoverCoordinates property and changed references to m_MouseHoverCoordinates to instead refer to the property.
 *										5.	Modified UpdateDisplay to check visibility before choosing to refresh the display on advice from Keith.
 *
 *  10/08/11    1.4.3   K.McD           1.  Minor modifications to the multi cursor implementation associated with revision 1.4.2.
 *  
 *                                              (a) Added the MultiCursorMouseMove static event.
 *                                              (b) Modified the Plotter_MouseLeave() and Plotter_MouseMove() event handlers to raise the MultiCursorMouseMove event 
 *                                                  if the multiple cursor property is asserted.
 *                                              (c) Added the event handler for the MultiCursorMouseMove event. This updates the mouse hover coordinates with the 
 *                                                  values specified in the event argument and re-draws the XYText box and cross hairs.
 *                                              (d) Modified the Cancel context menu option event handler to refresh the display.
 *                                              (e) Added the OnMultiCursorMouseMove() event manager method.
 *                                              (f) Removed the check as to whether the Y hover coordinates were within the bounds of the control graph area from 
 *                                                  the DrawXYText() and DrawCrossHair() methods. This modification was carried out as, firstly, the Y coordinate is 
 *                                                  not used and secondly, when displaying multiple/simultaneous cursors the Y coordinate of the mouse cursor may 
 *                                                  well fall outside the graph area of the individual control as common coordinates are used and the heigh of each 
 *                                                  control may well be different.
 *                                              (g) Modified the DrawCrossHair() method to use the m_CrossHairColor variable to specify the color of the cross hair 
 *                                                  and set this color to be Royal Blue.
 *                                                  
 * 10/26/11     1.4.4   K.McD           1.  Modified the design to enter the state which allows the user to modify the range as soon as the control has received focus 
 *                                          rather than requiring the user to initiate this via a context menu option.
 *                                              (a) Removed the 'New Range' context menu option and added event handlers for the Leave and GotFocus events.
 *                                              (b) Added a call to the RefreshDisplay() method in the event handler for the 'Reset' context menu option. Also now sets 
 *                                                  the control to the 'NewPlotterRangeSelected' state.
 *                                              (c) Added a call to the PlotterRangeSelection.Reset() method in the event handler for the 'Cancel' context menu option. 
 *                                                  Also now sets the control to the 'NewPlotterRangeSelected' state and raises a StartTimeChanged event.
 *                                              (d) Modified the  Plotter_MouseDown() event handler to include a check that the event was generated by a click on the 
 *                                                  left mouse button and removed a check that the control was in the 'NewPlotterRangeSelected' state. Also added a 
 *                                                  call to the RefreshDisplay() method.
 *                                              (e) Modified the Plotter_MouseUp() event handler to set the control to the 'NewPlotterRangeSelected' state if an invalid 
 *                                                  stop time is selected.
 *                                                  
 *                                      2.  Added the support for multiple user control selection. Added the SelectedControlList property, a generic list of selected 
 *                                          user controls.
 *                                      
 *                                      3.  Modified the layout of a number of properties.
 *                                      4.  Renamed and moved position of one or more methods.
 *                                      5.  Removed the redundant m_ButtonYAxisPlus_Click() and m_ButtonYAxisMinus_Click() event handlers.  
 *	                                        
 * 11/14/11     1.4.4.1 Sean.D          1.  Modified DrawXYText to use "using" to ensure proper disposal of StringFormat and Pen objects.
 *										2.	Modified DrawTripTime and DrawCrossHair to use "using" to ensure proper disposal of Pen objects.
 *										3.	Modified DrawSelectedArea to use "using" to ensure proper disposal of Brush objects and added braces to the return statements
 *										    at the top.
 *										4.	Modified DrawXAxisValues and DrawYAxisValues to use "using" to ensure proper disposal of Brush and StringFormat objects.
 *										
 *  12/01/11    1.4.5   K.McD           1.  Added to ZoomedStartTime and ZoomedStopTime static fields to the PlotterRangeSelection structure to record the start and stop 
 *                                          times when the plot has been successfully zoomed.
 *                                          
 *                                      2.  Modified the section of code responsible for resetting the plotter in the Plotter_Leave(), Plotter_MouseUp()  
 *                                          and m_ToolStripMenuItemCancel_Click() methods to set the start and stop times back to the initial start and stop times or 
 *                                          the zoomed start and stop times depending upon whether the plot is currently zoomed or not.
 *                                          
 *                                      3.  Modified the m_ToolStripMenuItemResetTimeSpan_Click() method to initialize the zoomed start and stop time fields to
 *                                          DateTime.MinValue.
 *                                      
 *                                      4.  Modified the m_ToolStripMenuItemZoom_Click() method to update the zoomed start and stop time fields.
 *                                      
 *  05/11/15    2.0     K.McD           References
 *                                      1.  SNCR - R188 PTU [20-Mar-2015] Item 14. If the data stream downloaded from the VCU cannot be plotted, rather than throwing
 *                                          an exception, the Plotter control should simply display an error message on the plot.
 *                                          
 *                                      Modifications
 *                                      1.  Added a try/catch block to the Graphics.DrawLines() call in the PlotPointsListArray() method.
 *                                      2.  Added the m_InvalidData flag and included a check in all Mouse event handlers to return from the handler if the
 *                                          flag is asserted.
 *                                      3.  Included code in the catch block to draw the error message string and to assert the m_InvalidData flag.
 *
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

using CodeProject.GraphComponents.Properties;

namespace CodeProject.GraphComponents
{
    #region --- Structures ---
    /// <summary>
    /// A structure to manage the range selection process.
    /// </summary>
    public struct PlotterRangeSelection
    {
        #region --- Static Public Variables ---
        /// <summary>
        /// The initial start time of the plot.
        /// </summary>
        public static DateTime InitialStartTime;

        /// <summary>
        /// The initial stop time of the plot.
        /// </summary>
        public static DateTime InitialStopTime;

        /// <summary>
        /// The start time of the plot if it is zoomed in; otherwise, DateTime.MinValue.
        /// </summary>
        public static DateTime ZoomedStartTime;

        /// <summary>
        /// The stop time of the plot if it is zoomed in; otherwise, DateTime.MinValue.
        /// </summary>
        public static DateTime ZoomedStopTime;

        /// <summary>
        /// The actual time of the trip, if the data corresponds to a fault log.
        /// </summary>
        public static DateTime TripTime;

        /// <summary>
        /// The selected start time of the new plot.
        /// </summary>
        public static DateTime StartTime;

        /// <summary>
        /// The selected stop time of the new plot.
        /// </summary>
        public static DateTime StopTime;

        /// <summary>
        /// The time span associated with the selected start and stop times.
        /// </summary>
        public static TimeSpan TimeSpan;

        /// <summary>
        /// The interval, in ms, between successive plot value updates.
        /// </summary>
        /// <remarks>This is associated with the live data display.</remarks>
        public static int PlotIntervalMs;

        /// <summary>
        /// The interval,in ms, between successive plot values saved in the channel reference.
        /// </summary>
        /// <remarks>This is associated with the historic data display.</remarks>
        public static int DataIntervalMs;

        /// <summary>
        /// The state associated with the last individual plotter instance state that was modified.
        /// </summary>
        public static PlotterRangeSelectionState LastPlotterRangeSelectionState = PlotterRangeSelectionState.InitialState;
        #endregion --- Static Public Variables ---

        #region --- Private Member Variables ---
        /// <summary>
        /// Stores the state of an individual plotter instance.
        /// </summary>
        private PlotterRangeSelectionState m_PlotterRangeSelectionState;
        #endregion --- Private Member Variables ---

        #region --- Methods ---
        /// <summary>
        /// Sets the time span of the plot to zero, this is used when the start time has been set but the stop time has not yet been selected.
        /// </summary>
        public static void SetTimeSpanToZero()
        {
            TimeSpan = TimeSpan.Zero; 
        }

        /// <summary>
        /// Calculate the new time span of the plot from the specified start and stop times.
        /// </summary>
        public static void CalculateTimeSpan()
        {
            TimeSpan = StopTime.Subtract(StartTime);
        }

        /// <summary>
        /// Reset the start and stop times to the initial/default values and update the time span accordingly.
        /// </summary>
        public static void Reset()
        {
            StartTime = InitialStartTime;
            StopTime = InitialStopTime;
            CalculateTimeSpan();
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the state corresponding to an individual plotter instance.
        /// </summary>
        public PlotterRangeSelectionState PlotterRangeSelectionState
        {
            get { return m_PlotterRangeSelectionState; }
            set 
            { 
                m_PlotterRangeSelectionState = value;

                // Make sure the static copy of the state reflects this change.
                LastPlotterRangeSelectionState = m_PlotterRangeSelectionState;
            }
        }
        #endregion --- Properties ---
    }
    #endregion --- Structures ---

    #region --- Enumerators ---
    /// <summary>
    /// An enumerator to specify the type of analogue IO that is to be plotted. TripleAnalogue for triple-analogue IO points and SingleAnalogue for single-analogue 
    /// IO points. 
    /// </summary>
    public enum AnalogueType
    {
        /// <summary>
        /// Plot for triple analogues.
        /// </summary>
        TripleAnalogue,

        /// <summary>
        /// Plot for single analogues.
        /// </summary>
        SingleAnalogue
    }

    /// <summary>
    /// The different states associated with the plotter range selection process.
    /// </summary>
    public enum PlotterRangeSelectionState
    {
        /// <summary>
        /// The 'Select New Plotter Range' context menu option has been selected.               
        /// </summary>
        NewPlotterRangeSelected,

        /// <summary>
        /// The start time of the plotter range has been selected.
        /// </summary>
        StartTimeSelected,

        /// <summary>
        /// Both the start and stop times of the plotter range have been selected.
        /// </summary>
        StopTimeSelected,

        /// <summary>
        /// Valid start and stop times have been configured and the 'Zoom' context menu option has been selected.
        /// </summary>
        ZoomSelected,

        /// <summary>
        /// In the initial state i.e. awaiting the 'Select Plotter Range' context menu option to be selected.
        /// </summary>
        InitialState
    }

    /// <summary>
    /// The possible states that the plotter can take.
    /// </summary>
    public enum PlotterState
    {
        /// <summary>
        /// The plotter is reset. All the graphs are erased and the plotter is waiting to run.
        /// </summary>
        Reset,

        /// <summary>
        /// The plotter is running. From this state the plotter may be stopped, paused or reset.
        /// </summary>
        Running,

        /// <summary>
        /// The plotter has stopped plotting. The graphs can now be viewed.
        /// </summary>
        Stopped
    }
    #endregion --- Enumerators ---

    /// <summary>
	/// A user control to plot the value (y) of a variable against time (t).
	/// </summary>
	public partial class Plotter : Graph
    {
        #region --- Delegates and Events ---
        /// <summary>
        /// Delegate for the event handler associated with PlotterStateChanged event.
        /// </summary>
        public delegate void PlotterEventHandler(object sender, PlotterEventArgs e);

        /// <summary>
        /// Raised when the user selects a new start time for the plot.
        /// </summary>
        public event EventHandler StartTimeChanged;

        /// <summary>
        /// Raised when the user selects a new stop time for the plot.
        /// </summary>
        public event EventHandler StopTimeChanged;

        /// <summary>
        /// Raised whenever a parameter of the plotter has changed.
        /// </summary>
        public event PlotterEventHandler PlotterStateChanged;

        /// <summary>
        /// Occurs whenever the mouse pointer is moved over the component and the property that controls whether all visible cursors are displayed is asserted.
        /// </summary>
        public static event MouseEventHandler MultiCursorMouseMove;
        #endregion --- Delegates and Events ---

        #region --- Constants ---
        /// <summary>
        /// The .NET format string used to display a value in numeric format. Value: "{0}".
        /// </summary>
        public const string FormatStringNumeric = "{0:F}";

        /// <summary>
        /// The format string used to display a value in hexadecimal format e.g. 0F0A. Value: "0x{0:X}".
        /// </summary>
        public const string FormatStringHex = "0x{0:X}";

        /// <summary>
        /// Centisecond (cs) represented as a number of milliseconds. Value: 100.
        /// </summary>
        public const long CS = 100;

        /// <summary>
        /// Second represented as a number of milliseconds. Value: 1,000.
        /// </summary>
        public const long Second = 1000;

        /// <summary>
        /// Minute represented as a number of milliseconds. value: 60,000.
        /// </summary>
        public const long Minute = 60000;

        /// <summary>
        /// Hour represented as a number of milliseconds. Value: 3,600,000.
        /// </summary>
        public const long Hour = 3600000;

        /// <summary>
        /// Day represented as a number of milliseconds. Value: 86,400,000
        /// </summary>
        public const long Day = 86400000;

        /// <summary>
        /// The maximum resolution, in ms, for the cursor position associated with the XYText display. Value: 10
        /// </summary>
        protected const int CursorResolution = 10;

        /// <summary>
        /// Half the maximum resolution, in ms, for the cursor position associated with the XYText display. Value: CursorResolution/2.
        /// </summary>
        protected const int CursorResolutionHalf = (int)(CursorResolution / 2);

        /// <summary>
        /// Used to specify the lower limit, as a multiple of the <c>DataInterval</c>, of the search associated with the FindY() method. This method is used to find
        /// the Y value corresponding to the specified time value, however, as the method uses iterative procedures a lower limit must be specified to terminate
        /// the search in case no corresponding value exists. Value: 3 data intervals. Value: 3.
        /// </summary>
        protected const int FindYLowerLimitAsMultiple = 3;

        /// <summary>
        /// The default plot interval, in ms. Value: 50.
        /// </summary>
        private const int DefaultPlotIntervalMs = 50;

        /// <summary>
        /// The default data interval for historic plots. Value: 50.
        /// </summary>
        private const int DefaultDataIntervalMs = 50;

        /// <summary>
        /// The resolution, in ms, associated with the FixdY method i.e. the value by which the search is decremented every pass. Value: 10.
        /// </summary>
        private const int FindYResolution = 10;

        /// <summary>
        /// The width of the vertical line drawn to represent the time of the trip. Value: 2.
        /// </summary>
        private const float TripLineWidth = (float)2.0;

        /// <summary>
        /// The transparency of the XYText. Value: 130.
        /// </summary>
        private const int TransparencyXYText = 130;

        #region - Border Values -
        /// <summary>
        /// The required border around the X axis text between graduations. Value: 10.0F.
        /// </summary>
        private const float GraduationBorder = 10.0F;

        /// <summary>
        /// The selected area border. Value: 2.
        /// </summary>
        private const int SelectedAreaBorder = 2;

        /// <summary>
        /// The X axis border. Value: 6.
        /// </summary>
        private const int XAxisBorder = 6;

        /// <summary>
        /// The border value for the status message. Value: 6.
        /// </summary>
        protected const int StatusMessageBorder = 6;
        #endregion - Border Values -
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// A generic list of the selected user controls.
        /// </summary>
        public static List<UserControl> m_SelectedControlList = new List<UserControl>();

        /// <summary>
        /// A static flag to control whether: (true) the cursors for all visible plots are displayed or (false) only the cursor associated with the selected control 
        /// is displayed.
        /// </summary>
        protected static bool m_MultiCursor = false;

        /// <summary>
        /// A flag that indicates whether the data associated with the current plot is invalid. True, if the data is invalid; otherwise, false.
        /// </summary>
        protected bool m_InvalidData = false;

        /// <summary>
        /// The coordinates of the mouse when it is hovering over the plotter.
        /// </summary>
        protected Point m_MouseHoverCoordinates = new Point();

        /// <summary>
        /// A flag to indicate whether the data corresponds to fault log data. True, if the data corresponds to a fault log; otherwise, false.
        /// </summary>
        protected bool m_IsFaultLog = false;

        /// <summary>
        /// A flag to indicate whether the value is to be displayed in hexadecimal format. True, if the value is to be displayed in hexadecimal format; otherwise, false.
        /// </summary>
        protected bool m_UseHexFormat;

        /// <summary>
        /// A record of the selected new start and stop times for adjusting the time span of the display. 
        /// </summary>
        protected PlotterRangeSelection m_XRangeSelection;

        /// <summary>
        /// The default range, as a TimeSpan, to use for the X axis.
        /// </summary>
        protected TimeSpan m_XRange = new TimeSpan(0, 0, 10);

        /// <summary>
        /// The interval, in ms, between successive plot updates.
        /// </summary>
        /// <remarks>This variable is associated with live data plots.</remarks>
        protected int m_PlotIntervalMs = DefaultPlotIntervalMs;

        /// <summary>
        /// The total time elapsed, in ms, since plotting began.
        /// </summary>
        /// <remarks>This variable is associated with live data plots.</remarks>
        protected long m_TotalTimeElapsed;

        /// <summary>
        /// The interval,in ms, between successive data entries saved in the channel reference.
        /// </summary>
        /// <remarks>This is associated with historic data display.</remarks>
        protected long m_DataIntervalMs = DefaultDataIntervalMs;

        /// <summary>
        /// The index of the currently active channel.
        /// </summary>
        protected int m_ActiveChannelIndex;

        /// <summary>
        /// Used to draw the gridline.
        /// </summary>
        protected Gridline m_Gridline;
        
        /// <summary>
        /// Used to draw the x and y axis line.
        /// </summary>
        protected AxisLine m_AxisLineXandY;

        /// <summary>
        /// The time value, in ms since the start of the plot, associated with the left edge of the plotter.
        /// </summary>
        protected int m_LeftDisplayLimit;

        /// <summary>
        /// The time value, in ms since the start of the plot, associated with the left edge of the plotter.
        /// </summary>
        protected int m_RightDisplayLimit;

        /// <summary>
        /// The number of points to remove from the left side as we go on scrolling. If the scroll bar is at the complete left, the number of points to remove is 0. 
        /// If it is at the complete right, then m_PointsToRemove is m_TotalPointsToRemove.	 
        /// </summary>
        protected int m_PointsToRemove;

        /// <summary>
        /// The number of points to remove from the left as the plotter continues scrolling.
        /// </summary>
        protected int m_TotalPointsToRemove;
       
        /// <summary>
        /// The current state of the plotter.
        /// </summary>
        protected PlotterState m_CurrentState;

        /// <summary>
        /// The default color of the GraphArea.
        /// </summary>
        protected Color m_GraphAreaNormalColor = Color.WhiteSmoke;

        /// <summary>
        /// The actual time of trip, if the data corresponds to a fault log.
        /// </summary>
        private DateTime m_TripTime = DateTime.Now;
 
        /// <summary>
        /// The start time of the recorded data. Only used in Stop Mode.
        /// </summary>
        private DateTime m_StartTime = DateTime.Now;
        
        /// <summary>
        /// Defines the type of plot i.e. Single Analogue, Triple Analogue, defaults to TripleAnalogue.
        /// </summary>
        private AnalogueType m_AnalogueIOType = AnalogueType.TripleAnalogue;

        /// <summary>
        /// The GraphArea colour to be used when the component is selected.
        /// </summary>
        private Color m_GraphAreaSelectedColor = Color.Linen;
        
        /// <summary>
        /// The color of the vertical cross hair line used to show the position of the cursor.
        /// </summary>
        private Color m_CrossHairColor = Color.RoyalBlue;

        /// <summary>
        /// The left side margin width of the graph.
        /// </summary>
        private int m_GraphMarginLeft = 50;

        /// <summary>
        /// The upper margin width of the graph.
        /// </summary>
        private int m_GraphMarginTop = 20;

        /// <summary>
        /// The right side margin width of the graph.
        /// </summary>
        private int m_GraphMarginRight = 50;

        /// <summary>
        /// The lower margin width of the graph. In stopped/pause mode a scroll bar appears if the graphs dont fit, so take the scroll bar height into account.
        /// </summary>
		private int m_GraphMarginBottom = 20;

        /// <summary>
        /// The collection that contains all the individual channels.
        /// </summary>
        private ChannelCollection m_ChannelCollection;

        /// <summary>
        /// The position from where the plotter's pen is displayed.
        /// </summary>
        private TimeSpan m_PlotterPenPosition;

        /// <summary>
        /// The format in which the values on the time axis are to be shown.
        /// </summary>
        private TimeAxisStyle m_TimeDisplayStyle = TimeAxisStyle.Smart;

        /// <summary>
        /// Coordinates of the mouse when the MouseDown event occurs.
        /// </summary>
        private Point m_MouseDownCoordinates = new Point();

        /// <summary>
        /// Coordinates of the mouse when the MouseUp event occurs.
        /// </summary>
        private Point m_MouseUpCoordinates = new Point();

        /// <summary>
        /// Initial plot rate. This is required as the user can change the plot rate during the plotting.
        /// </summary>
        private int m_InitialPlotIntervalMs;

        /// <summary>
        /// The time value on the left edge of the plotter when the plotter was stopped.
        /// </summary>
        private int m_StoppedLeftDisplayLimit;

		#region - Compressed Mode Variables -
        /// <summary>
        /// A flag that indicates whether the compressed mode is active. True, indicates that the plotter is showing the graphs in compressed format; otherwise, false.
        /// </summary>
        private bool m_CompressedMode;

        /// <summary>
        /// The previously stored x range. This is used while toggling the compressedMode.
        /// </summary>
        private TimeSpan m_SavedXRange;

        /// <summary>
        /// The previously stored leftDisplayLimit. This is used while toggling the compressedMode.
        /// </summary>
        private int m_SavedLeftDisplayLimit;

        /// <summary>
        /// The previously stored pointsToRemove. This is used while toggling the compressedMode.
        /// </summary>
        private int m_SavedPointsToRemove;
        #endregion - Compressed Mode Variables -
		#endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Creates and initializes a new plotter component.
        /// </summary>
        public Plotter()
        {   
            InitializeComponent();

            m_InvalidData = false;

            // Keep track of the initial plot interval.
            m_InitialPlotIntervalMs = m_PlotIntervalMs;

            // Set the GraphAreaColour to the default value.
            m_GraphAreaColor = m_GraphAreaNormalColor;

            // Keep track of the time span selection process.
            m_XRangeSelection = new PlotterRangeSelection();
            m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.InitialState;

            // Add the default channel definitions.
            m_ChannelCollection = new ChannelCollection();

            // Add a channel to the collection.
            Channels.Add(new Channel(Channel.DefaultMinValue, Channel.DefaultMaxValue, "", true, Color.ForestGreen));

            // Add a further two channels to the collection for triple analogue plots.
            if (m_AnalogueIOType == AnalogueType.TripleAnalogue)
            {
                Channels.Add(new Channel(Channel.DefaultMinValue, Channel.DefaultMaxValue, "", true, Color.Blue));
                Channels.Add(new Channel(Channel.DefaultMinValue, Channel.DefaultMaxValue, "", false, Color.Red));
            }

            // Instantiate the Gridline and AxisLine references.
            m_Gridline = new Gridline(this);
            m_AxisLineXandY = new AxisLine(this);

            // Set the Style of the UserControl.
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            // Calculate the available graph area of the UserControl.
            CalculateGraphArea();

            Debug.Assert(m_GraphArea.Height == (m_GraphArea.Bottom - m_GraphArea.Top), "Problem Ctor");

            m_PlotterPenPosition = new TimeSpan((long)(m_XRange.Duration().Ticks * 0.9));

            // Evaluate the range, in ms, of the plot and set up the horizontal scroll bar accordingly.
            int xRangeInMs = (int)(m_XRange.Duration().Ticks / TimeSpan.TicksPerMillisecond);
            m_HScrollBar.Maximum = xRangeInMs / PlotIntervalMs;
            m_HScrollBar.Value = xRangeInMs / PlotIntervalMs;
            m_HScrollBar.Visible = false;

            m_RightDisplayLimit = xRangeInMs + m_LeftDisplayLimit;
            m_SavedXRange = m_XRange;

            // Register the event handler for the MultiCursorMouseMove event.
            Plotter.MultiCursorMouseMove += new MouseEventHandler(Plotter_MultiCursorMouseMove);

            // Register the event handlers for the Leave and GotFocus events.
            Leave += new EventHandler(Plotter_Leave);
            GotFocus += new EventHandler(Plotter_GotFocus);
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the user control.
        /// </summary>
        /// <param name="disposing">True, to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Cleanup(bool disposing)
        {
            try
            {
                // Clear all controls from the selected control list.
                m_SelectedControlList.Clear();

                if (disposing)
                {
                    // Method called by consumer code. Call the Dispose method of any managed data members that implement the dispose method.
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    // Check each of the channels in the channel collection.
                    if (m_ChannelCollection != null)
                    {
                        // Dispose of each channel in the collection.
                        foreach (Channel channel in m_ChannelCollection)
                        {
                            channel.Dispose();
                        }

                        m_ChannelCollection.Clear();
                        m_ChannelCollection = null;
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data 
                // members to null.
                m_Gridline = null;
                m_AxisLineXandY = null;

                #region  - [Detach the event handler methods.] -
                this.m_HScrollBar.Scroll -= new System.Windows.Forms.ScrollEventHandler(this.m_HScrollBar_Scroll);
                this.m_ToolStripMenuItemResetRange.Click -= new EventHandler(m_ToolStripMenuItemResetTimeSpan_Click);
                this.m_ToolStripMenuItemChangeRange.Click -= new EventHandler(m_ToolStripMenuItemNewTimeSpan_Click);
                this.m_ToolStripMenuItemZoom.Click -= new EventHandler(m_ToolStripMenuItemZoom_Click);
                this.m_ToolStripMenuItemCancel.Click -= new EventHandler(m_ToolStripMenuItemCancel_Click);
                this.m_ToolStripMenuItemNextChannel.Click -= new EventHandler(m_ToolStripMenuItemNextChannel_Click);
                this.m_ToolStripMenuItemPreviousChannel.Click -= new EventHandler(m_ToolStripMenuItemPreviousChannel_Click);
                this.m_ToolStripMenuItemResetAxis.Click -= new EventHandler(m_ToolStripMenuItemResetAxis_Click);
                this.MouseDown -= new System.Windows.Forms.MouseEventHandler(Plotter_MouseDown);
                this.MouseUp -= new System.Windows.Forms.MouseEventHandler(Plotter_MouseUp);
                this.MouseLeave -= new EventHandler(Plotter_MouseLeave);
                this.MouseMove -= new System.Windows.Forms.MouseEventHandler(Plotter_MouseMove);
                this.Leave -= new EventHandler(Plotter_Leave);
                this.GotFocus -= new EventHandler(Plotter_GotFocus);
                Plotter.MultiCursorMouseMove -= new System.Windows.Forms.MouseEventHandler(Plotter_MultiCursorMouseMove);
                #endregion  - [Detach the event handler methods.] -

                #region - [Component Designer Variables] -
                this.m_HScrollBar = null;
                this.m_ContextMenuStrip = null;
                this.m_ToolStripMenuItemResetRange = null;
                this.m_ToolStripMenuItemChangeRange = null;
                this.m_ToolStripMenuItemZoom = null;
                this.m_ToolStripMenuItemCancel = null;
                this.m_ToolStripSeparatorCancel = null;
                this.m_ToolStripMenuItemNextChannel = null;
                this.m_ToolStripMenuItemPreviousChannel = null;
                this.m_ToolStripMenuItemResetAxis = null;
                #endregion - [Component Designer Variables] -
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception is not thrown.
            }
            finally
            {
                base.Cleanup(disposing);
            }
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        #region - [UserControl] -
        /// <summary>
        /// Event handler for the <c>Paint</c> event.
        /// </summary>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (!Visible)
            {
                return;
            }

			Draw(e.Graphics);

            base.OnPaint(e);
        }

        /// <summary>
        /// Event handler for the <c>Leave</c> event. Update the <c>PlotterRangeSelection</c> static structure and trigger a <c>StartTimeChanged</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void Plotter_Leave(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_GraphAreaColor = m_GraphAreaNormalColor;
            m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.InitialState;

            // Check whether the plot is currently zoomed in and update the start, stop and duration labels accordingly.
            if (PlotterRangeSelection.ZoomedStartTime != DateTime.MinValue)
            {
                PlotterRangeSelection.StartTime = PlotterRangeSelection.ZoomedStartTime;
                PlotterRangeSelection.StopTime = PlotterRangeSelection.ZoomedStopTime;
                PlotterRangeSelection.CalculateTimeSpan();
            }
            else
            {
                PlotterRangeSelection.Reset();
            }

            RefreshDisplay();

            // Raise a Start Time changed event to trigger an update of the displayed start and stop times.
            OnStartTimeChanged(this, new EventArgs());
        }

        /// <summary>
        /// Event handler for the <c>GotFocus</c> event. Update the <c>PlotterRangeSelection</c> static structure and trigger a <c>StartTimeChanged</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void Plotter_GotFocus(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_GraphAreaColor = m_GraphAreaNormalColor;
            m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.NewPlotterRangeSelected;
            RefreshDisplay();

            // Raise a Start Time changed event to trigger an update of the displayed start and stop times.
            OnStartTimeChanged(this, new EventArgs());
        }

        #region - [Mouse] -
        /// <summary>
        /// Event handler for the MouseLeave event. Tidies up the XY text box and cross hairs. If the multi cursor property is asserted this event handler will 
        /// raise the <c>MultiCursorMouseMove</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void Plotter_MouseLeave(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CurrentState != PlotterState.Stopped)
            {
                return;
            }

            if (m_InvalidData == true)
            {
                return;
            }

            // Hide the cursor by setting the hover coordinates to be outside the control graphics area.
            MouseHoverCoordinates = new Point(0, 0);

            if (m_MultiCursor)
            {
                OnMultiCursorMouseMove(this, new MouseEventArgs(MouseButtons.None, 0, MouseHoverCoordinates.X, MouseHoverCoordinates.Y, 0));
            }
            else
            {
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Event handler for the <c>MouseMove</c> event. When the mouse pointer moves over the control the coordinates are captured and are used to display the 
        /// coordinate text box in stop mode. If the multi cursor property is asserted this event handler will raise the <c>MultiCursorMouseMove</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void Plotter_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CurrentState != PlotterState.Stopped)
            {
                return;
            }

            if (m_InvalidData == true)
            {
                return;
            }

            MouseHoverCoordinates = new Point(e.X, e.Y);

            if (m_MultiCursor)
            {
                OnMultiCursorMouseMove(this, new MouseEventArgs(MouseButtons.None, 0, MouseHoverCoordinates.X, MouseHoverCoordinates.Y, 0));
            }
            else
            {
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Event handler for the <c>MultiCursorMouseMove</c> event. If the <c>Visible</c> property is asserted, sets the mouse hover coordinates to the 
        /// values passed by the event arguments and then refreshes the display.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void Plotter_MultiCursorMouseMove(object sender, MouseEventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_InvalidData == true)
            {
                return;
            }

            if (Visible == true)
            {
                MouseHoverCoordinates = new Point(e.X, e.Y);
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Event handler for the MouseDown event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void Plotter_MouseDown(object sender, MouseEventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CurrentState != PlotterState.Stopped)
            {
                return;
            }

            if (m_InvalidData == true)
            {
                return;
            }

            // Skip if this was raised by any button other than the left mouse button e.g. context menu button etc.
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }

            if (e.X < m_GraphArea.Left || e.X > m_GraphArea.Right)
            {
                return;
            }

            if (e.Y < m_GraphArea.Top || e.Y > m_GraphArea.Bottom)
            {
                return;
            }

            m_MouseDownCoordinates.X = e.X;
            m_MouseDownCoordinates.Y = e.Y;

            Channel activeCh = Channels[m_ActiveChannelIndex];

            // Get the value associated with the plotter coordinates.
            PointF mouseDownValue = GetValueFromPixel(activeCh, m_MouseDownCoordinates.X, m_MouseDownCoordinates.Y);

            // Round the selected time, in ms, DOWN to the nearest data interval for the channel.
            long selectedTimeToLong = (long)mouseDownValue.X;
            long modulo = selectedTimeToLong % m_DataIntervalMs;
            selectedTimeToLong -= modulo;

            //Update the Zoom structure with the selected StartTime.
            PlotterRangeSelection.StartTime = m_StartTime.AddMilliseconds((double)selectedTimeToLong);
            PlotterRangeSelection.SetTimeSpanToZero();
            m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.StartTimeSelected;
            RefreshDisplay();

            // Raise a Start Time changed event.
            OnStartTimeChanged(this, new EventArgs());
        }

        /// <summary>
        /// Event handler for the MouseUp event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void Plotter_MouseUp(object sender, MouseEventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CurrentState != PlotterState.Stopped)
            {
                return;
            }

            if (m_InvalidData == true)
            {
                return;
            }

            // Don't do anyting unless the user has selected a valid start time.
            if (m_XRangeSelection.PlotterRangeSelectionState != PlotterRangeSelectionState.StartTimeSelected)
            {
                return;
            }

            if (e.X < m_GraphArea.Left || e.X > m_GraphArea.Right)
            {
                return;
            }

            if (e.Y < m_GraphArea.Top || e.Y > m_GraphArea.Bottom)
            {
                return;
            }

            m_MouseUpCoordinates.X = e.X;
            m_MouseUpCoordinates.Y = e.Y;

            Channel activeCh = Channels[m_ActiveChannelIndex];

            // Get the value associated with the plotter coordinates.
            PointF mouseUpValue = GetValueFromPixel(activeCh, m_MouseUpCoordinates.X, m_MouseUpCoordinates.Y);

            // Round the selected time, in ms, DOWN to the nearest data interval for the channel.
            long selectedTimeToLong = (long)mouseUpValue.X;

            // Remove the fraction of a ms component.
            long modulo = selectedTimeToLong % m_DataIntervalMs;
            selectedTimeToLong -= modulo;

            //Update the StopTime with this new value.
            DateTime stopTime = m_StartTime.AddMilliseconds((double)selectedTimeToLong);

            // Check if Stop Time is Greater than Start Time
            if (stopTime.CompareTo(PlotterRangeSelection.StartTime) > 0)
            {
                PlotterRangeSelection.StopTime = stopTime;
                PlotterRangeSelection.CalculateTimeSpan();
                m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.StopTimeSelected;

                // Raise a Stop Time changed event.
                OnStopTimeChanged(this, new EventArgs());
            }
            else
            {
                m_GraphAreaColor = m_GraphAreaNormalColor;
                m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.NewPlotterRangeSelected;

                // Check whether the plot is currently zoomed in and update the start, stop and duration labels accordingly.
                if (PlotterRangeSelection.ZoomedStartTime != DateTime.MinValue)
                {
                    PlotterRangeSelection.StartTime = PlotterRangeSelection.ZoomedStartTime;
                    PlotterRangeSelection.StopTime = PlotterRangeSelection.ZoomedStopTime;
                    PlotterRangeSelection.CalculateTimeSpan();
                }
                else
                {
                    PlotterRangeSelection.Reset();
                }
                
                // Raise a Start Time changed event to trigger an update of the displayed start and stop times.
                OnStartTimeChanged(this, new EventArgs());
            }
        }
        #endregion - [Mouse] -
        #endregion - [UserControl] -

        #region - [ToolStripMenuItems] -
        /// <summary>
        /// Event handler for the context menu 'Zoom' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemZoom_Click(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Only zoom to selected timespan if valid timespan has been selected.
            if (m_XRangeSelection.PlotterRangeSelectionState == PlotterRangeSelectionState.StopTimeSelected)
            {
                m_GraphAreaColor = m_GraphAreaNormalColor;
                m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.ZoomSelected;
                
                // Update the zoomed start and stop times with the curresnt start and stop times.
                PlotterRangeSelection.ZoomedStartTime = PlotterRangeSelection.StartTime;
                PlotterRangeSelection.ZoomedStopTime = PlotterRangeSelection.StopTime;
            }
        }

        /// <summary>
        /// Event handler for the context menu 'Select New TimeSpan' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemNewTimeSpan_Click(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_GraphAreaColor = m_GraphAreaSelectedColor;
            m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.NewPlotterRangeSelected;

            // Raise a Start Time changed event to trigger an update of the displayed start and stop times.
            OnStartTimeChanged(this, new EventArgs());
        }

        /// <summary>
        /// Event handler for the context menu 'Reset TimeSpan' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemResetTimeSpan_Click(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_GraphAreaColor = m_GraphAreaNormalColor;
            m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.NewPlotterRangeSelected;

            // Set the zoomed start and stop times to DateTime.MinValue to indicate that the plot is no longer zoomed in.
            PlotterRangeSelection.ZoomedStartTime = DateTime.MinValue;
            PlotterRangeSelection.ZoomedStopTime = DateTime.MinValue;

            PlotterRangeSelection.Reset();

            RefreshDisplay();

            // Raise a Start Time changed event to trigger an update of the displayed start and stop times.
            OnStartTimeChanged(this, new EventArgs());
        }

        /// <summary>
        /// Event handler for the context menu 'Cancel' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemCancel_Click(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_GraphAreaColor = m_GraphAreaNormalColor;
            m_XRangeSelection.PlotterRangeSelectionState = PlotterRangeSelectionState.NewPlotterRangeSelected;
            
            // Check whether the plot is currently zoomed in and update the start, stop and duration labels accordingly.
            if (PlotterRangeSelection.ZoomedStartTime != DateTime.MinValue)
            {
                PlotterRangeSelection.StartTime = PlotterRangeSelection.ZoomedStartTime;
                PlotterRangeSelection.StopTime = PlotterRangeSelection.ZoomedStopTime;
                PlotterRangeSelection.CalculateTimeSpan();
            }
            else
            {
                PlotterRangeSelection.Reset();
            }

            RefreshDisplay();

            // Raise a Start Time changed event to trigger an update of the displayed start and stop times.
            OnStartTimeChanged(this, new EventArgs());
        }

        /// <summary>
        /// Event handler for the context menu 'Next Channel' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemNextChannel_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            NextChannel();
        }

        /// <summary>
        /// Event handler for the context menu 'Previous Channel' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemPreviousChannel_Click(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            PreviousChannel();
        }

        /// <summary>
        /// Event handler for the context menu 'Reset Axis' menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemResetAxis_Click(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            for (int index = 0; index < Channels.Count; index++)
            {
                Channels[index].MaximumValue = Channels[0].DefaultMaximumValue;
                Channels[index].MinimumValue = Channels[0].DefaultMinimumValue;
            }

            // Set the active channel to be the channel representing the average value.
            m_ActiveChannelIndex = Channel.DefaultActiveChannel;

            // Refresh only if we are not running, when we are running refresh occurs automatically.
			if (m_CurrentState != PlotterState.Running)
				RefreshDisplay ();

            OnPlotterStateChanged(this, new PlotterEventArgs(this));   // Raise the plotter state changed event.     
        }
        #endregion - [ToolStripMenuItems] -

        #region - [HScrollBar] -
        /// <summary>
        /// Set the scroll position and the number of points to remove from the LHS of the display.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_HScrollBar_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_PointsToRemove = e.NewValue;
            m_LeftDisplayLimit = m_PointsToRemove * m_InitialPlotIntervalMs;

            RefreshDisplay();
        }
        #endregion - [HScrollBar] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        #region - IGraphElement Members -
        /// <summary>
		/// The main drawing routine.
		/// </summary>
		/// <param name="graphics">Reference to the The GDI+ drawing surface.</param>
		public override void Draw(Graphics graphics)
		{
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Evaluate the graph area associated with the control.
            CalculateGraphArea();

            if (m_GraphArea.Width == 0 || m_GraphArea.Height == 0)
            {
                return;
            }

			graphics.SetClip (m_GraphArea);

            // Draw the grid within the graph area.
            m_Gridline.Draw(graphics);

            m_YAxisColor = ActiveChannel.ChannelColor;

            // Draw the X and Y axes.
            m_AxisLineXandY.Draw(graphics);

            // Draw the Y axis values.
            DrawYAxisValues(graphics);

            // Draw the X axis values.
            DrawXAxisValues(graphics);

            // Only draw the vertical line representing the time of the trip if the data corresponds to a fault log.
            if (m_IsFaultLog)
            {
                DrawTripTime(graphics);
            }
            
			graphics.SetClip (m_GraphArea);

            if (m_CurrentState == PlotterState.Reset)
            {
                return;
            }

            #region - Check for Scrolling -
            int xRangeInMs = (int) (m_XRange.Duration().Ticks / TimeSpan.TicksPerMillisecond);
			m_RightDisplayLimit = m_LeftDisplayLimit + xRangeInMs;

			if (m_TotalTimeElapsed > m_RightDisplayLimit)
			{				
				if (m_CurrentState == PlotterState.Running)
				{
					m_HScrollBar.Maximum = m_PointsToRemove;
					m_HScrollBar.Value   = m_PointsToRemove;
					m_PointsToRemove ++;					

					PointF leftPosition = (PointF) ActiveChannel.Points[m_PointsToRemove];
					#region - Adjust Scrolling Rate -
					// if the user has changed the plotting rate, then we need to adjust the scrolling at the left, as it has to scroll at the old plotting rate.
					// If the scrolling is too slow, the new points won't appear on the right edge.
					int leftDisplayLimitDelta = (int) (leftPosition.X - m_LeftDisplayLimit);

                    if (leftDisplayLimitDelta > m_PlotIntervalMs)
                    {
                        m_LeftDisplayLimit = (int)leftPosition.X;
                    }
                    else
                    {
                        m_LeftDisplayLimit += m_PlotIntervalMs;
                    }
					#endregion - Adjust Scrolling Rate - 
				}
			}
			#endregion - Check for Scrolling - 

			bool scrollAdjusted = false;

            // ----------------------------------------------------------
            // Plot the data values for each channel.
            // ----------------------------------------------------------
            for (int i = 0; i < Channels.Count; i ++)                   
			{
				Channel channel = Channels[i];

                // Skip if the channel hasn't been enabled.
                if (channel.Enabled == false)
                {
                    continue;
                }
				
				if (!scrollAdjusted)
				{
                    m_TotalTimeElapsed = channel.TotalTimeElapsed;
					if (m_TotalTimeElapsed > m_RightDisplayLimit)
					{				
						if (m_CurrentState == PlotterState.Running)
						{
							m_HScrollBar.Maximum = m_PointsToRemove;
							m_HScrollBar.Value   = m_PointsToRemove;
							m_PointsToRemove ++;					

							PointF leftPosition = (PointF) channel.Points[m_PointsToRemove];
							#region - Adjust Scrolling Rate - 
                            // if the user has changed the plotting rate, then we need to adjust the scrolling at the left, as it has to scroll at the old plotting rate.
                            // If the scrolling is too slow, the new points won't appear on the right edge.
							int leftDisplayLimitDelta = (int) (leftPosition.X - m_LeftDisplayLimit);

                            if (leftDisplayLimitDelta > m_PlotIntervalMs)
                            {
                                m_LeftDisplayLimit = (int)leftPosition.X;
                            }
                            else
                            {
                                m_LeftDisplayLimit += m_PlotIntervalMs;
                            }

							scrollAdjusted = true;
							#endregion - Adjust Scrolling Rate - 
						}
					}
				}

                // The plot may contain gaps in the data, for example when the unit is powered down, and these gaps should be drawn using a transparent pen. The
                // continuous data between these gaps are known as blocks of data and the points associated with each block of data are stored in an ArrayList.
                // Each element of the array of ArrayLists contains the data associated with a particular block. If there are no gaps in the data there will only be 1
                // array element.

                // Create an array of ArrayLists containing the points to be plotted associated with the specified channel.
                ArrayList[] pointsList = AddToPointsListArray(channel);
                // Plot the points defined in each element of the ArrayList.
                PlotPointsListArray(graphics, (i == m_ActiveChannelIndex)? true : false, channel, pointsList);
			}

            // If the user is in the process of selecting new start and stop times, highlight the area.
            DrawSelectedArea(graphics);

            // Draw the cross hair if the mouse is within the graph area.
            DrawCrossHair(graphics);

            // Draw the XYText if the mouse is within the graph area.
            DrawXYText(graphics);
        }
        #endregion - IGraphElement Members -

        /// <summary>
        /// Draw the X and Y coordinates for the active channel.
        /// </summary>
        /// <remarks>The plotter must be in stop mode and the mouse hover coordinates must be within the graph area.</remarks>
        /// <param name="graphics">Reference to the GDI+ drawing surface.</param>
        protected virtual void DrawXYText(Graphics graphics)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CurrentState != PlotterState.Stopped)
            {
                return;
            }

            if (MouseHoverCoordinates.X < m_GraphArea.Left || MouseHoverCoordinates.X > m_GraphArea.Right)
            {
                return;
            }

            // For normal logs, round the selected time to the nearest 10ms i.e. 376 ms mapped to 380ms, 371ms mapped to 370ms. For minute, hour and day logs round
            // to the nearest data interval i.e. 60,000 for minute logs etc.
            Channel activeCh = Channels[m_ActiveChannelIndex];
            PointF hoveringValue = GetValueFromPixel(activeCh, MouseHoverCoordinates.X, MouseHoverCoordinates.Y);

            // The actual time, in ms, selected by the user and the remainder value when dividing by the display interval.
            long actualX, modulo;

            // The y value associated with the selected time.
            float y;

            // Check if the data corresponds to a minute, hour or day log.
            if ((m_DataIntervalMs == Plotter.Minute) || (m_DataIntervalMs == Plotter.Hour) || (m_DataIntervalMs == Plotter.Day))
            {
                actualX = m_LeftDisplayLimit + (long)(hoveringValue.X + m_DataIntervalMs/2);
                modulo = actualX % m_DataIntervalMs;
                actualX -= modulo;
                y = FindY(actualX, activeCh);
            }
            else
            {
                actualX = m_LeftDisplayLimit + (long)(hoveringValue.X + CursorResolutionHalf);
                modulo = actualX % CursorResolution;
                actualX -= modulo;
                y = FindY(actualX, activeCh);
            }
            
            // Derive the string value that is to be displayed in the box.
            string coordinate = string.Empty;
            if (float.IsNaN(y))
            {
                // y value not found, display -.
                coordinate = GetFormatForTime(actualX) + ", -";
            }
            else
            {
                string val = string.Empty;
                if (m_UseHexFormat == true)
                {
                    long yAsLong = (long)y;
                    val = string.Format(CultureInfo.InstalledUICulture, m_ValueFormat, yAsLong);
                }
                else
                {
                    val = string.Format(CultureInfo.CurrentUICulture, FormatStringNumeric, y);
                }

                coordinate = GetFormatForTime(actualX) + ", " + val + " " + activeCh.YAxisName;
            }            

            // Measure the width of the rectangle. Pad the coordinate with extra spaces so that the bounding rectangle looks nice.
			using (StringFormat sf = new StringFormat())
			{
				sf.Trimming = StringTrimming.Character;
				sf.FormatFlags = StringFormatFlags.NoWrap;
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				SizeF size = graphics.MeasureString(" " + coordinate + " ", Font, m_GraphArea.Width, sf);
				Rectangle rect = GetCoordinateTextRect(size);


				// Draw the box.
				using (Pen borderPen = new Pen(m_GridlineColor))
				{
					graphics.DrawRectangle(borderPen, rect);
					// Use the colour appropriate to the active channel.
					Color rectColor = Color.FromArgb(TransparencyXYText, activeCh.ChannelColor);
					using (Brush rectBrush = new SolidBrush(rectColor))
					{
						graphics.FillRectangle(rectBrush, rect);
					}

					// Write the string that is enclosed within the box.
					using (Brush textBrush = new SolidBrush(m_GraphAreaNormalColor))
					{
						graphics.DrawString(coordinate, Font, textBrush, rect, sf);
					}
				}
			}
        }

        /// <summary>
        /// Draw a vertical line to represent the time of the actual trip.
        /// </summary>
        /// <remarks>Only relevant if the plot is a fault log and the plotter is in stop mode.</remarks>
        /// <param name="graphics">Reference to the GDI+ drawing surface.</param>
        protected void DrawTripTime(Graphics graphics)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CurrentState != PlotterState.Stopped)
            {
                return;
            }

            // Only applicable to fault logs.
            if (m_IsFaultLog != true)
            {
                return;
            }

            // Draw a vertical line to represent the time of the actual trip. The TripTime value will be the same for all channels.
            Channel channel = Channels[0];

            // Get the X coordinate pixel value corresponding to the trip time.
            TimeSpan tripTimeOffset = m_TripTime.Subtract(m_StartTime);
            PointF pointF = GetPixelFromValue(channel, (int)tripTimeOffset.TotalMilliseconds, channel.MinimumValue);
			using (Pen crossHairPen = new Pen(Color.Indigo, TripLineWidth))
			{
				graphics.DrawLine(crossHairPen, pointF.X, m_GraphArea.Top, pointF.X, m_GraphArea.Bottom);
			}
        }

        /// <summary>
        /// Draw the selected area.
        /// </summary>
        /// <remarks>The plotter must be in stop mode.</remarks>
        /// <param name="graphics">Reference to the GDI+ drawing surface.</param>
        protected void DrawSelectedArea(Graphics graphics)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

			if (m_CurrentState != PlotterState.Stopped)
			{
				return;
			}

            // Skip if the user hasn't selected a valid start time or stop time.
			if (m_XRangeSelection.PlotterRangeSelectionState != PlotterRangeSelectionState.StartTimeSelected &&
				m_XRangeSelection.PlotterRangeSelectionState != PlotterRangeSelectionState.StopTimeSelected)
			{
				return;
			}

			using (Brush brush = new HatchBrush(HatchStyle.Percent50, Color.DimGray, Color.Transparent))
			{
				// Highlight the selected area.
				if (m_XRangeSelection.PlotterRangeSelectionState == PlotterRangeSelectionState.StartTimeSelected)
				{
					graphics.FillRectangle(brush, m_MouseDownCoordinates.X, m_GraphArea.Top, MouseHoverCoordinates.X - m_MouseDownCoordinates.X - SelectedAreaBorder,
                                           m_GraphArea.Height - SelectedAreaBorder);
				}
				else if (m_XRangeSelection.PlotterRangeSelectionState == PlotterRangeSelectionState.StopTimeSelected)
				{
					graphics.FillRectangle(brush, m_MouseDownCoordinates.X, m_GraphArea.Top, m_MouseUpCoordinates.X - m_MouseDownCoordinates.X - SelectedAreaBorder,
                                           m_GraphArea.Height - SelectedAreaBorder);
				}
			}
        }

        /// <summary>
        /// Draw the cross hair vertical line.
        /// </summary>
        /// <remarks>The plotter must be in stop mode and the mouse hover coordinates must be within the graph area.</remarks>
        /// <param name="graphics">Reference to the GDI+ drawing surface.</param>
        protected void DrawCrossHair(Graphics graphics)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CurrentState != PlotterState.Stopped)
            {
                return;
            }

            if (MouseHoverCoordinates.X < m_GraphArea.Left || MouseHoverCoordinates.X > m_GraphArea.Right)
            {
                return;
            }

			using (Pen crossHairPen = new Pen(m_CrossHairColor))
			{
				graphics.DrawLine(crossHairPen, MouseHoverCoordinates.X, m_GraphArea.Top, MouseHoverCoordinates.X, m_GraphArea.Bottom);
			}
        }

        /// <summary>
        /// Draw the x axis values for the currently selected channel.
        /// </summary>
        /// <param name="graphics">Reference to the GDI+ drawing surface.</param>
        protected void DrawXAxisValues(Graphics graphics)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            graphics.SetClip(ClientRectangle);

            // Evaluate the range of the plot, in ms.
            int xRangeInMs = (int)(m_XRange.Duration().Ticks / TimeSpan.TicksPerMillisecond);

            // Evaluate the time difference, in ms, between graduations cast to a float.
            float timeDifference = xRangeInMs / m_GraduationsX;

            // Check if the time string can be written within the graduations, if not only display the time value every other graduation.
            // Get the start time of the plot.  
            float timeValue = m_LeftDisplayLimit;

            // Convert this time to a string depending upon the TimeAxisStyle property.
            string val = GetFormatForTime((int)timeValue);

            // Get the size of the time string in pixels. Used to see if it will fit between graduations.
            SizeF numberSize = graphics.MeasureString(val, Font);

            // Half the width of the time string, used to position the string on the graduation line.
            float numberHalfWidth = numberSize.Width / 2;

            // Evaluate the X pixel difference between graduations.
            float graduationPixelDiff = m_GraphArea.Width / m_GraduationsX;

            // Instantiate a text brush with the colour set to the X axis colour.
			using (Brush textBrush = new SolidBrush(m_XAxisColor))
			{
				// Evaluate the X pixel offset associated with the start position of the axis.

				float offset = m_GraphArea.Left;

				// Initialize the StringFormat.
				using (StringFormat sf = new StringFormat())
				{
					sf.Trimming = StringTrimming.Character;
					sf.FormatFlags = StringFormatFlags.NoWrap;
					sf.Alignment = StringAlignment.Near;
					sf.LineAlignment = StringAlignment.Near;

					// Check if the time string will fit between graduations, if not, display time string every other graduation.
					if (numberSize.Width > graduationPixelDiff - GraduationBorder)
					{
						// Only write time value every other graduation.
						// Pixel interval is now twice that of the pixel interval between graduations.
						graduationPixelDiff *= 2;

						// Time interval is now twice that of the normal time interval.
						timeDifference *= 2;

						// Evaluate the adjusted number of graduations.
						int modifiedGraduationsX = (int)(m_GraduationsX / 2);
						for (int i = 0; i <= modifiedGraduationsX; i++)
						{
							timeValue = (m_LeftDisplayLimit + timeDifference) * i;
							val = GetFormatForTime((int)timeValue);

							RectangleF axisValuesRect = new RectangleF(offset - numberHalfWidth, m_GraphArea.Bottom + XAxisBorder, numberSize.Width, Font.Height);
							graphics.DrawString(val, Font, textBrush, axisValuesRect, sf);

							// Increment the offset to reposition the axisValuesRect Rectangle.
							offset += graduationPixelDiff;
						}
					}
					else
					{
						for (int i = 0; i <= m_GraduationsX; i++)
						{
							timeValue = m_LeftDisplayLimit + timeDifference * i;
							val = GetFormatForTime((int)timeValue);

							RectangleF axisValuesRect = new RectangleF(offset - numberHalfWidth, m_GraphArea.Bottom + XAxisBorder, numberSize.Width, Font.Height);
							graphics.DrawString(val, Font, textBrush, axisValuesRect, sf);


							// Increment the offset to reposition the axisValuesRect Rectangle.
							offset += graduationPixelDiff;
						}
					}
				}
			}
        }

        /// <summary>
        /// Draw the y axis value for the currently selected channel
        /// </summary>
        /// <remarks>
        /// To display the digital state on the Logic Analyser, the number of Y graduations is set to 3 and the Max and Min values are set to (float) 3.0 and 0.0 
        /// respectively. The true state of the digital is represented by setting the channel value to 2.0 and the false state is represented by setting the 
        /// channel value to 1.0. These values are then mapped to display 1 and 0 respectively.
        /// </remarks>
        /// <param name="graphics">Reference to the GDI+ drawing surface.</param>
        protected virtual void DrawYAxisValues(Graphics graphics)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            graphics.SetClip(ClientRectangle);

            // Evaluate the Y pixel offset associated with the start position of the axis.
            float offset = m_GraphArea.Top - Font.Height / 2;

            // Evaluate the engineering value increment between graduations.
            float graduationDiff = (ActiveChannel.MaximumValue - ActiveChannel.MinimumValue) / m_GraduationsY;

            // Initialize the valueOffset, used to evaluate the engineering value associated with the graduation.
            float valueOffset = 0;

            // Draw the values in black if the plotter is being used as a Logic Analyzer; otherwise use the channel colour.
			using (Brush textBrush = new SolidBrush(ActiveChannel.ChannelColor))
			{
				// Evaluate the Y pixel increment between graduations.
				float graduationPixelDiff = m_GraphArea.Height / m_GraduationsY;

				for (int i = m_GraduationsY; i >= 0; i--)
				{
					// Instantiate the rectangle used to draw the Y axis value.
					RectangleF axisValuesRect = new RectangleF(ClientRectangle.Left, offset, m_GraphMarginLeft - Font.Height / 2, Font.Height);

					// Derive the string value that is to be drawn on the axis.
					string val = string.Empty;

					// Evaluate the engineering value associated with the current graduation.
					float graduationValue = ActiveChannel.MaximumValue - graduationDiff * valueOffset;
					valueOffset++;

					if (m_UseHexFormat == true)
					{
						long graduationValueAsLong = (long)graduationValue;
						val = string.Format(CultureInfo.InstalledUICulture, m_ValueFormat, graduationValueAsLong);
					}
					else
					{
						val = string.Format(CultureInfo.CurrentUICulture, FormatStringNumeric, graduationValue);
					}

					// Draw the value.
					using (StringFormat sf = new StringFormat())
					{
						sf.Trimming = StringTrimming.Character;
						sf.FormatFlags = StringFormatFlags.NoWrap;
						sf.Alignment = StringAlignment.Far;
						sf.LineAlignment = StringAlignment.Center;
						graphics.DrawString(val, Font, textBrush, axisValuesRect, sf);
					}

					// Increment the offset to reposition the axisValuesRect Rectangle.
					offset += graduationPixelDiff;
				}
			}
        }

        /// <summary>
        /// Create a jagged array of ArrayLists containing the points to be plotted for each block of data.
        /// </summary>
        /// <param name="channel">The channel reference.</param>
        /// <returns>An array of ArrayLists containing the points to be plotted for each block.</returns>
        private ArrayList[] AddToPointsListArray(Channel channel)
        {
            // Create an ArrayList of PointF values for each group of PointF values separated by a break point e.g. zero breakpoints would
            // require a single list, 1 break point would require two lists etc, where each break point specifies the elapsed time value 
            // corresponding to a plot entry where the pen colour is to be set to transparent between this particular plot entry and the next 
            // plot entry. Break points are associated with min/hour/day logs and RTD logs and record periods where the unit has been powered down. 
            ArrayList[] pointsList = new ArrayList[channel.BreakPoint.Count + 1];

            for (int block = 0; block <= channel.BreakPoint.Count; block++)
            {
                pointsList[block] = new ArrayList();
            }

            // Local variable to store the current entry of the generic list associated with the channel.
            PointF pointStored = new PointF();

            // Local variable to store the pixel value corresponding to the current entry of the generic list associated with te channel.
            Point p;
            if (m_CurrentState == PlotterState.Stopped)
            {
                // The index of the current block of data.
                int blockIndex = 0;
                for (int index = 0; index < channel.Points.Count; index++)
                {
                    // Get the current entry of the generic list.
                    pointStored = channel.Points[index];

                    // Convert the PointF values to pixel coordinates.
                    p = GetPixelFromValue(channel, (int)pointStored.X, pointStored.Y);
                    pointsList[blockIndex].Add(p);

                    // Check if the generic list contains further breakpoints.
                    if (blockIndex < channel.BreakPoint.Count)
                    {
                        // If the current time value corresponds to a latest break point, add the remaining data values to the next 
                        // pointsList element.
                        if (pointStored.X == channel.BreakPoint[blockIndex])
                        {
                            pointsList[blockIndex].Add(p);
                            blockIndex++;
                        }
                    }
                }
            }
            else
            {
                // Note: There are no break points associated with real time mode.
                for (int index = 0; index < channel.Points.Count - m_PointsToRemove; index++)
                {
                    pointStored = (PointF)channel.Points[index + m_PointsToRemove];

                    // Convert the PointF values to pixel coordinates.
                    p = GetPixelFromValue(channel, (int)pointStored.X, pointStored.Y);
                    pointsList[0].Add(p);
                }
            }
            return pointsList;
        }

        /// <summary>
        /// Plot the points contained within the specified ArrayList[]. Each element of ArrayList[] contains a continuous set of points to be plotted. 
        /// If there are no gaps in the data to be plotted the array will contain a single element.
        /// </summary>
        /// <param name="graphics">Reference to the The GDI+ drawing surface.</param>
        /// <param name="activeChannel">True, if the specified channel is the active channel; otherwise false.</param>
        /// <param name="channel">The channel reference.</param>
        /// <param name="pointsList">An array of ArrayLists where each element of the array contains the points to be plotted for each continuous block of data.</param>
        private void PlotPointsListArray(Graphics graphics, bool activeChannel, Channel channel, ArrayList[] pointsList)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Convert the ArrayList[] of pixel coordinates to a jagged array i.e. an array whose elements are an array, ready for plotting.
            Point[][] pointsToPlot = new Point[channel.BreakPoint.Count + 1][];
            for (int block = 0; block <= channel.BreakPoint.Count; block++)
            {
                pointsToPlot[block] = new Point[pointsList[block].Count];
                for (int index = 0; index < pointsList[block].Count; index++)
                {
                    pointsToPlot[block][index] = (Point)pointsList[block][index];
                }
            }

            graphics.SetClip(m_GraphArea);

            try
            {
                // Plot each block of pixel coordinates.
                for (int block = 0; block <= channel.BreakPoint.Count; block++)
                {
                    // Skip if there aren't at least 2 points to plot.
                    if (pointsToPlot[block].Length < 2)
                    {
                        continue;
                    }

                    if (activeChannel)
                    {
                        graphics.DrawLines(new Pen(channel.ChannelColor, 1.5F), pointsToPlot[block]);
                    }
                    else
                    {
                        graphics.DrawLines(new Pen(channel.ChannelColor, 1.5F), pointsToPlot[block]);
                    }
                }
            }
            catch (Exception)
            {
                // -------------------------------------------------
                // Catch the exception and report this to the user.
                // -------------------------------------------------
                m_InvalidData = true;

                string statusMessage = Resources.SMPlotInvalid;
                SizeF stringSize = graphics.MeasureString(statusMessage, Font);
                int top = m_GraphArea.Bottom - Font.Height - StatusMessageBorder;
                int left = (int)((m_GraphArea.Width - (int)stringSize.Width) / 2);
                using (Brush textBrush = new SolidBrush(Color.Red))
                {
                    graphics.DrawString(statusMessage, Font, textBrush, left, top);
                }
            }
        }

        /// <summary>
        /// Increase the active channel Y axis values by 5%.
        /// </summary>
        public void YAxisPlus()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Raise graph by 5%.
            float graduationDiff = (ActiveChannel.MaximumValue - ActiveChannel.MinimumValue) / 20;
            ActiveChannel.MaximumValue += graduationDiff;
            ActiveChannel.MinimumValue += graduationDiff;

            if (m_CurrentState != PlotterState.Running)
            {
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Lower the active channel Y axis values by 5%.
        /// </summary>
        public void YAxisMinus()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Reduce graph by 5%.
            float graduationDiff = (ActiveChannel.MaximumValue - ActiveChannel.MinimumValue) / 20;
            ActiveChannel.MaximumValue -= graduationDiff;
            ActiveChannel.MinimumValue -= graduationDiff;

            if (m_CurrentState != PlotterState.Running)
            {
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Change to the previous channel. Just like a remote control channel changer for a TV.
        /// </summary>
        public void PreviousChannel()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_ActiveChannelIndex--;

            if (m_ActiveChannelIndex < 0)
            {
                m_ActiveChannelIndex = Channels.Count - 1;
            }

            // Refresh only if we are not running. Whilst running, display is automatically refreshed.
            if (m_CurrentState != PlotterState.Running)
            {
                RefreshDisplay();
            }

            OnPlotterStateChanged(this, new PlotterEventArgs(this));   // Raise the plotter state changed event.     
        }

        /// <summary>
        /// Change to the next channel. Just like a remote control channel changer for a TV.
        /// </summary>
        public void NextChannel()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_ActiveChannelIndex++;

            if (m_ActiveChannelIndex == Channels.Count)
            {
                m_ActiveChannelIndex = 0;
            }

            // Refresh only if we are not running. Whilst running, display is automatically refreshed.
            if (m_CurrentState != PlotterState.Running)
            {
                RefreshDisplay();
            }

            OnPlotterStateChanged(this, new PlotterEventArgs(this));   // Raise the plotter state changed event.     
        }

		/// <summary>
		/// Update the graph display. This method is called when all plot values have been set and the values are to be displayed.
		/// </summary>		
		public void UpdateDisplay ()
		{
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (Visible)
            {
                RefreshDisplay();
            }
		}
		
		/// <summary>
		/// Start plotting.
		/// </summary>
		public void Start ()
		{
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CurrentState == PlotterState.Running)
            {
                return;
            }

            if (CompressedMode)
            {
                CompressedMode = false;
            }

			m_CurrentState = PlotterState.Running;

			m_HScrollBar.Visible = false;
			m_PointsToRemove            = m_TotalPointsToRemove;			
			m_LeftDisplayLimit          = m_StoppedLeftDisplayLimit;		
		}
		
		/// <summary>
		/// Stop plotting. The user can now view the graphs.
		/// </summary>
		public void Stop ()
		{
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

			m_CurrentState = PlotterState.Stopped;

            if (m_PointsToRemove == 0)
            {
                return;
            }
			
			m_StoppedLeftDisplayLimit   = m_LeftDisplayLimit;
			m_HScrollBar.Visible = true;
			m_HScrollBar.Maximum = m_PointsToRemove + m_HScrollBar.LargeChange; 
			m_HScrollBar.Value   = 0;
			m_TotalPointsToRemove       = m_PointsToRemove - 1;
			
			RefreshDisplay ();
		}

		/// <summary>
		/// Reset the plotter. Erases the graphs and gets ready to start the whole plotting process again. To start once again, call Start ()
		/// after calling Reset ().
		/// </summary>
		public void Reset ()
		{
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CurrentState == PlotterState.Running)
            {
                Stop();
            }
						
			CompressedMode = false;
			m_CurrentState = PlotterState.Reset;
			m_LeftDisplayLimit        = 0;
			m_SavedLeftDisplayLimit   = 0; 
			m_TotalPointsToRemove     = 0;
			m_PointsToRemove          = 0;
			m_StoppedLeftDisplayLimit = 0;
            m_TotalTimeElapsed        = 0;

			m_HScrollBar.Visible = false;
            int xRangeInMs = (int)(m_XRange.Duration().Ticks / TimeSpan.TicksPerMillisecond);
			m_HScrollBar.Maximum = xRangeInMs / m_InitialPlotIntervalMs;
			m_HScrollBar.Value   = m_HScrollBar.Maximum;
			
            // Reset the channel data for each control.
			foreach (Channel channel in Channels)
			{
				channel.CursorOffset = 0;
				channel.Points.Clear ();
                channel.X.Clear();
				channel.TotalTimeElapsed = 0;
			}

			RefreshDisplay ();
		}

		/// <summary>
		/// Get a string representation of the time to be displayed on the X axis depending upon the TimeAxisStyle property.
		/// </summary>
		/// <param name="timeInMillisecond">The time, in ms, relative to the start of the plot.</param>
		/// <returns>A string representation of the specified time.</returns>
        protected string GetFormatForTime(long timeInMillisecond)
		{
            string timeValue = string.Empty;
			switch (m_TimeDisplayStyle)
			{
				case TimeAxisStyle.Millisecond:
					timeValue = string.Format (CultureInfo.CurrentUICulture, "{0:G}", timeInMillisecond);
					break;
				case TimeAxisStyle.MillisecondWithUnitDisplay:
					timeValue = string.Format (CultureInfo.CurrentUICulture, "{0:G}ms", timeInMillisecond);
					break;
				case TimeAxisStyle.Second:					
					timeValue = string.Format (CultureInfo.CurrentUICulture, "{0:D2}.{1:D3}", timeInMillisecond / 1000, timeInMillisecond % 1000);
					break;
				case TimeAxisStyle.SecondWithUnitDisplay:
					timeValue = string.Format (CultureInfo.CurrentUICulture, "{0:D2}.{1:D3}s", timeInMillisecond / 1000, timeInMillisecond % 1000);
					break;
				case TimeAxisStyle.Smart:
                    if (m_PlotIntervalMs < Second)
					{
						if (timeInMillisecond < 10 * Second)
						{
							timeValue = string.Format (CultureInfo.CurrentUICulture, "{0:G}", timeInMillisecond);
						}
						else if (timeInMillisecond < Minute)
						{
							int ms  = (int)timeInMillisecond % (int)Second;
							int sec = (int)timeInMillisecond / (int)Second;
							timeValue = string.Format (CultureInfo.CurrentUICulture, "{0:D2}.{1:D3}", sec, ms);
						}
					}
					else
					{
						// if the plot rate is more than 1000, then dont show the millisecond part.
						if (timeInMillisecond < Minute)
						{								
							int sec = (int)timeInMillisecond / (int)Second;
							timeValue = string.Format (CultureInfo.CurrentUICulture, "{0:G}", sec);
						}
					}

                    if (timeInMillisecond < Hour)
                    {
                        // Convert the input time, specified in ms, to mm:hh.ff.
                        int tempTime;                                   // Temporary storage for the performing time conversion.
                        tempTime = (int)timeInMillisecond;              // Copy the input parameter to the temporary storage.
                        int ms = tempTime % (int)Second;                // Evaluate the ms part.
                        tempTime /= 1000;
                        int sec = tempTime % 60;                        // Evaluate the seconds part.
                        tempTime /= 60;
                        int min = tempTime;                             // Evaluate the minutes part.

                        timeValue = string.Format(CultureInfo.CurrentUICulture, "{0}:{1:D2}.{2:D3}", min, sec, ms);
                    }
                    else if (timeInMillisecond >= Hour)
                    {
                        // Convert the input time, specified in ms, to HH:mm:hh.ff.
                        long tempTime;                                  // Temporary storage for the performing time conversion.
                        tempTime = timeInMillisecond;                   // Copy the input parameter to the temporary storage.
                        long ms = tempTime % (int)Second;               // Evaluate the ms part.           
                        tempTime /= 1000;
                        long sec = tempTime % 60;                       // Evaluate the seconds part.
                        tempTime /= 60;
                        long min = tempTime % 60;                       // Evaluate the minutes part.
                        tempTime /= 60;
                        long hr = tempTime;                             // Evaluate the hours part.

                        timeValue = string.Format(CultureInfo.CurrentUICulture, "{0}:{1:D2}:{2:D2}.{3:D3}", hr, min, sec, ms);
                    }
					break;
                case TimeAxisStyle.Absolute:

                    // This TimeAxisStyle differs from the rest in that the returned time string is the absolute time rather than the time relative to the
                    // start of the plot. It is currently only used on historic plots.

                    if (m_DataIntervalMs < 100)                         // Data interval < 100ms display to 1/100th of second
                    {
                        timeValue = StartTime.AddMilliseconds(timeInMillisecond).ToString("HH:mm:ss.ff");
                    }
                    else if (m_DataIntervalMs < Second)
                    {
                        timeValue = StartTime.AddMilliseconds(timeInMillisecond).ToString("HH:mm:ss.f");
                    }
                    else if (m_DataIntervalMs < Minute)
                    {
                        timeValue = StartTime.AddMilliseconds(timeInMillisecond).ToString("HH:mm:ss");
                    }
                    else if (m_DataIntervalMs < Hour)
                    {
                        timeValue = StartTime.AddMilliseconds(timeInMillisecond).ToString("HH:mm");
                    }
                    else if (m_DataIntervalMs < Day)
                    {
                        timeValue = StartTime.AddMilliseconds(timeInMillisecond).ToString("HH");
                    }
                    else if (m_DataIntervalMs >= Day)
                    {
                        timeValue = StartTime.AddMilliseconds(timeInMillisecond).ToString("dd/MM/yy");
                    }
                    break;
			}
			return timeValue;
		}		

		/// <summary>
		/// Get the coordinates of the coordinate text box that is used to display the current value of the active channel graph on the plotter.
		/// </summary>
		/// <param name="stringSize"></param>
		/// <returns></returns>
        protected Rectangle GetCoordinateTextRect(SizeF stringSize)
		{
			int left = 0;

            if (MouseHoverCoordinates.X + stringSize.Width + 2 > m_GraphArea.Right)
            {
                left = MouseHoverCoordinates.X - (int)(stringSize.Width + 0.5) - 2;
            }
            else
            {
                left = MouseHoverCoordinates.X + 2;
            }

			int rectHeight = (int) (2 * Font.Height);
			int top = m_GraphArea.Bottom - rectHeight - 2;
            
			// Adding 0.5 and casting to an integer is equivalent to rounding the value to the the nearest integer value eg. 45.4 becomes 45 and 45.6 becomes 46.
			Rectangle rect  = new Rectangle (left, top, (int) (stringSize.Width + 0.5), rectHeight);
			return rect;
		}

        /// <summary>
        /// Convert the PointF.X and PoinF.Y values (time and engineering value) for a particular channel into the relative pixel values for the GraphArea.
        /// </summary>
        /// <remarks>
        /// GraphArea.Top, GraphArea.Left corresponds to the pixel coordinates 0,0.
        /// </remarks>
        /// <param name="channel">The channel reference.</param>
        /// <param name="xInValue">The PointF.X value i.e. time, in ms, since the start of the plot.</param>
        /// <param name="yInValue">The PointF.Y value i.e. engineering value.</param>
        /// <returns>The pixel coordinates.</returns>
        protected Point GetPixelFromValue(Channel channel, int xInValue, float yInValue)
        {
            // yInPixel.
            float yRange = channel.MaximumValue - channel.MinimumValue;
            float y = (yInValue - channel.MinimumValue) / yRange;
            int yInPixel = m_GraphArea.Bottom - (int)(y * (float)m_GraphArea.Height);

            // xInPixel.
            int xOffsetValue = xInValue;
            xOffsetValue -= (m_LeftDisplayLimit);
            int xRangeInMs = (int)(m_XRange.Duration().Ticks / TimeSpan.TicksPerMillisecond);
            float xOffsetAbs = (float)((float)xOffsetValue / (float)xRangeInMs);
            int xInPixel = m_GraphArea.Left + (int)(xOffsetAbs * m_GraphArea.Width);

            return new Point(xInPixel, yInPixel);
        }

        /// <summary>
        /// Convert the specified pixel position into the corresponding PointF.X (time, in ms, since the start of the plot) and PointF.Y 
        /// (engineering value) for the specified channel. Used when the mouse coordinates need to be converted to real values.
        /// </summary>
        /// <remarks>
        /// GraphArea.Top, GraphArea.Left corresponds to the pixel coordinates 0,0.
        /// </remarks>
        /// <param name="channel">The channel reference.</param>
        /// <param name="xInPixel">The X pixel value.</param>
        /// <param name="yInPixel">The Y pixel value.</param>
        /// <returns>A PointF value containing the converted time, in ms, from the start of the plot and the corresponding engineering value.</returns>
        protected PointF GetValueFromPixel(Channel channel, int xInPixel, int yInPixel)
        {
            // yInValue.
            float yAbsolute = (float)(m_GraphArea.Bottom - yInPixel) / (float)m_GraphArea.Height;
            float yRange = channel.MaximumValue - channel.MinimumValue;
            float yInValue = channel.MinimumValue + (yAbsolute * yRange);

            // xInValue.
            float xOffsetPixel = (float)(xInPixel - m_GraphArea.Left) / (float)m_GraphArea.Width;
            int xRangeInMs = (int)(m_XRange.Duration().Ticks / TimeSpan.TicksPerMillisecond);
            float xInValue = (xOffsetPixel * (float)xRangeInMs);

            return new PointF(xInValue, yInValue);
        }

        /// <summary>
        /// Find the engineering value corresponding to the specified time. If an entry corresponding to the specified time does not exist, the value corresponding to 
        /// the nearest valid time entry which is greater than the specified time is returned.   
        /// </summary>
        /// <param name="timeInMs">The time, in ms, from the start of the plot.</param>
        /// <param name="channel">The channel reference that is currently being plotted.</param>
        /// <returns>The engineering value corresponding to the specified time or, if this does not exist, the value corresponding to the nearest valid time entry which 
        /// is greater than the specified time. If no entry for the specified time exists and there are no valid time entries that are greater than the specified time 
        /// Float.NaN is returned.</returns>
        protected float FindY(long timeInMs, Channel channel)
        {
            // The array index of the Points generic list where the specified time is located.
            int index;

            // Search the List for an entry containing the specified time. The BinarySearch() call returns the zero-based index of the entry corresponding to the 
            // specified time, if it exists; otherwise, it will return a negative number that is the bitwise complement of the index of the entry corresponding to the 
            // time that is the nearest to but greater than the specified time. If no entry is found, the bitwise complement of the Count is returned. 
            index = channel.X.BinarySearch(timeInMs);

            // Default to 'Not a Number'.
            float y = float.NaN;
            if (index >= 0)
            {
                // An entry matching the specified time was found, return the corresponding Y value.
                y = channel.Points[index].Y;
            }
            else
            {
                // Check whether a valid entry exists. 
                if (index != ~channel.X.Count)
				{
                    // Get the Y value associated with the entry corresponding to the time that that is nearest to but greater than the specified time.
					y = channel.Points[~index].Y;
				}
				else
				{
					// Do nothing, y stays NaN.
				}
            }
            return y;
        }

        /// <summary>
        /// Calculate the graph area.
        /// </summary>
        protected void CalculateGraphArea()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Calculate the graph area based upon the ClientRectange taking into account the margins.
            m_GraphArea = new Rectangle(ClientRectangle.Left + m_GraphMarginLeft,
                                        ClientRectangle.Top + m_GraphMarginTop,
                                        ClientRectangle.Width - m_GraphMarginRight - m_GraphMarginLeft,
                                        ClientRectangle.Height - m_GraphMarginBottom - m_GraphMarginTop);

            // Adjust the GraphArea so that it is a multiple of the gridsize. This improves the correllation between the time shown on the XYText box and the 
            // time shown on the X axis.
            int gridSizeX = (int)m_GraphArea.Width / m_GraduationsX;
            if ((gridSizeX % m_GraduationsX) != 0)
            {
                m_GraphArea.Width = gridSizeX * m_GraduationsX;
            }
        }

        #region - Event Processing -
        /// <summary>
        /// Raise a StartTimeChanged event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="eventArgs">Parameter passed from the object that raised the event.</param>
        protected void OnStartTimeChanged(object sender, EventArgs eventArgs)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (StartTimeChanged != null)
            {
                StartTimeChanged(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raise a StopTimeChanged event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="eventArgs">Parameter passed from the object that raised the event.</param>
        protected void OnStopTimeChanged(object sender, EventArgs eventArgs)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (StopTimeChanged != null)
            {
                StopTimeChanged(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raise the PlotterStateChanged event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="plotterEventArgs">Parameter passed from the object that raised the event.</param>
        protected void OnPlotterStateChanged(object sender, PlotterEventArgs plotterEventArgs)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (PlotterStateChanged != null)
            {
                PlotterStateChanged(sender, plotterEventArgs);
            }
        }

        /// <summary>
        /// Raise the <c>MultiCursorMouseMove</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="mouseEventArgs">Parameter passed from the object that raised the event.</param>
        protected static void OnMultiCursorMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (MultiCursorMouseMove != null)
            {
                MultiCursorMouseMove(sender, mouseEventArgs);
            }
        }
        #endregion - Event Processing -
        #endregion --- Methods ---

        #region --- Properties ---
        #region CurrentRangeSelection
        /// <summary>
        /// Gets or sets the current status of the range selection process.
        /// </summary>
        [
        Browsable(false)
        ]
        public PlotterRangeSelection CurrentRangeSelection
        {
            get { return m_XRangeSelection; }
            set { m_XRangeSelection = value; }
        }
        #endregion CurrentRangeSelection

        #region StartTime
        /// <summary>
        /// Gets or sets the start time of the plot.
        /// </summary>
        [
        Browsable(false)
        ]
        public DateTime StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }
        #endregion StartTime

        #region IsFaultLog
        /// <summary>
        /// Gets or sets whether the plot corresponds to fault log data. True, represents fault log data; otherwise false. 
        /// </summary>
        [
        Browsable(false)
        ]
        public bool IsFaultLog
        {
            get { return m_IsFaultLog; }
            set { m_IsFaultLog = value; }
        }
        #endregion IsFaultLog

        #region TripTime
        /// <summary>
        /// Gets or sets the time of the actual trip, as a .NET DateTime object.
        /// </summary>
        /// <remarks>Only applicable if the data being plotted corresponds to a fault log.</remarks>
        [
        Browsable(false)
        ]
        public DateTime TripTime
        {
            get { return m_TripTime; }
            set { m_TripTime = value; }
        }
        #endregion TripTime

        #region AnalogueType
        /// <summary>
        /// Gets or sets the type of analogue to be plotted: single-analogue or triple-analogue.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The type of analogue to be plotted: single-analogue or triple-analogue.")
        ]
        public AnalogueType AnalogueType
        {
            get { return m_AnalogueIOType; }
            set { m_AnalogueIOType = value; }
        }
        #endregion AnalogueType

        #region YAxisColor
        /// <summary>
        /// Gets or sets the color of the Y axis.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("Color of the Y axis.")
        ]
        public override Color YAxisColor
        {
            get { return m_YAxisColor; }
            set { m_YAxisColor = value; }
        }
        #endregion YAxisColor

        #region GraphMargin
        /// <summary>
        /// Gets or sets the left side margin width of the graph.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The left side margin width of the graph.")
        ]
        public int GraphMarginLeft
        {
            get { return m_GraphMarginLeft; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid property value. Margin cannot be negative.");

                m_GraphMarginLeft = value;
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Gets or sets the upper margin width of the graph.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The upper margin width of the graph.")
        ]
        public int GraphMarginTop
        {
            get { return m_GraphMarginTop; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid property value. Margin cannot be negative.");

                m_GraphMarginTop = value;
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Gets or sets the right side margin width of the graph.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The right side margin width of the graph.")
        ]
        public int GraphMarginRight
        {
            get { return m_GraphMarginRight; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid property value. Margin cannot be negative.");

                m_GraphMarginRight = value;
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Gets or sets the lower margin width of the graph.
        /// </summary>
        /// <remarks>In stopped/pause a scroll bar appears if the graphs don't fit. Ensure that the scroll bar height is also taken into account.</remarks>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The lower margin width of the graph.")
        ]
        public int GraphMarginBottom
        {
            get { return m_GraphMarginBottom; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid property value. Margin cannot be negative.");

                m_GraphMarginBottom = value;
                RefreshDisplay();
            }
        }
        #endregion GraphMargin

        #region Channels
        /// <summary>
        /// Gets the collection of channels associated with the plot.
        /// </summary>
        [
        Browsable(false)
        ]
        public ChannelCollection Channels
        {
            get { return m_ChannelCollection; }
        }
        #endregion Channels

        #region XRange
        /// <summary>
        /// Gets or sets the range of the plot i.e. the time-span between the start-time to the stop-time.
        /// </summary>
        [
        Browsable(false)
        ]
        public TimeSpan XRange
        {
            get { return m_XRange; }
            set { m_XRange = value; }
        }
        #endregion XRange

        #region PlotIntervalMs
        /// <summary>
        /// Gets or sets the interval, in ms, between successive plot updates. Used when displaying live data.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Design"),
        Description("The interval, in ms, between successive plot updates. Used when displaying live data.")
        ]
        public int PlotIntervalMs
        {
            get { return m_PlotIntervalMs; }
            set
            {
                m_PlotIntervalMs = value;
                if (m_PlotIntervalMs > m_InitialPlotIntervalMs)
                    m_InitialPlotIntervalMs = m_PlotIntervalMs;

                foreach (Channel channel in Channels)
                    channel.PlotIntervalMs = m_PlotIntervalMs;
            }
        }
        #endregion PlotIntervalMs

        #region DataIntervalMs
        /// <summary>
        /// Gets or sets the interval, in ms, between successive data entries saved in the channel reference. Used when displaying historic data. 
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Design"),
        Description("The interval, in ms, between successive data entries saved in the channel reference. Used when displaying historic data.")
        ]
        public long DataIntervalMs
        {
            get { return m_DataIntervalMs; }
            set { m_DataIntervalMs = value;}
        }
        #endregion DataIntervalMs

        #region CompressedMode
        /// <summary>
        /// Gets or sets whether the plotter is to display the graphs in a compressed format so that all the data fits in the graph area. 
        /// This mode can be set only when the plotter is either paused or stopped.
        /// </summary>
        [
        Browsable(false)
        ]
        public bool CompressedMode
        {
            get { return m_CompressedMode; }
            set
            {
                if (m_CurrentState != PlotterState.Stopped)
                {
                    m_CompressedMode = false;
                    m_XRange = m_SavedXRange;
                    return;
                }

                if (m_CompressedMode == value)
                    return;

                m_CompressedMode = value;

                if (m_CompressedMode)
                {
                    // Save the current settings.
                    m_SavedXRange = m_XRange;
                    m_SavedLeftDisplayLimit = m_LeftDisplayLimit;
                    m_SavedPointsToRemove = m_PointsToRemove;

                    // Set the new settings.
                    m_XRange = new TimeSpan(m_TotalTimeElapsed * TimeSpan.TicksPerMillisecond);
                    m_LeftDisplayLimit = 0;
                    m_PointsToRemove = 0;
                }
                else
                {
                    // Restore the settings back again.
                    m_XRange = m_SavedXRange;
                    m_LeftDisplayLimit = m_SavedLeftDisplayLimit;
                    m_PointsToRemove = m_SavedPointsToRemove;
                }

                m_HScrollBar.Visible = !m_CompressedMode;
                RefreshDisplay();
            }
        }
        #endregion CompressedMode

        #region CurrentState
        /// <summary>
        /// Gets the current state of the plotter.
        /// </summary>
        [
        Browsable(false)
        ]
        public PlotterState CurrentState
        {
            get { return m_CurrentState; }
        }
        #endregion CurrentState

        #region ActiveChannelIndex
        /// <summary>
        /// Gets the index of the currently active channel. To change to the next channel, use NextChannel () and PrevChannel ().
        /// </summary>
        [
        Browsable(false)
        ]
        public int ActiveChannelIndex
        {
            get { return m_ActiveChannelIndex; }
        }
        #endregion ActiveChannelIndex

        #region ActiveChannel
        /// <summary>
        /// Gets the currently active channel. To change to the next channel, use NextChannel () and PrevChannel ().
        /// </summary>
        [
        Browsable(false)
        ]
        public Channel ActiveChannel
        {
            get { return m_ChannelCollection[m_ActiveChannelIndex]; }
        }
        #endregion ActiveChannel

        #region TimeDisplayStyle
        /// <summary>
        /// Gets or sets the style in which the values on the time axis (X axis) are to be shown.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("Style in which the values on the time axis (X axis) are to be shown.")
        ]
        public TimeAxisStyle TimeDisplayStyle
        {
            get { return m_TimeDisplayStyle; }
            set { m_TimeDisplayStyle = value; }
        }
        #endregion TimeDisplayStyle

        #region TotalTimeElapsed
        /// <summary>
        /// Gets or sets the total time elapsed, in ms, since plotting began.
        /// </summary>
        [
        Browsable(false)
        ]
        public long TotalTimeElapsed
        {
            get { return m_TotalTimeElapsed; }
            set
            {
                m_TotalTimeElapsed = value;
            }
        }
        #endregion TotalTimeElapsed

        #region ValueFormat
        /// <summary>
        /// Gets or sets the .NET style of format for displaying the value and axis. Eg. {0:F}, {0:E} etc.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The .NET style of format for displaying the value. Eg. {0:F}, {0:E} etc.")
        ]
        public override string ValueFormat
        {
            get { return m_ValueFormat; }
            set
            {
                m_ValueFormat = value;
                if (m_ValueFormat == FormatStringHex)
                {
                    m_UseHexFormat = true;
                }

                RefreshDisplay();
            }
        }
        #endregion ValueFormat

        #region MouseHoverCoordinates
        /// <summary>
        /// Gets or sets the current mouse hover coordinates.
        /// </summary>
        [
        Browsable(false)
        ]
        public Point MouseHoverCoordinates
        {
            get { return m_MouseHoverCoordinates; }
            set { m_MouseHoverCoordinates = value; }
        }
        #endregion MouseHoverCoordinates

        #region MultiCursor
        /// <summary>
        /// Gets or sets the static flag that controls whether: (true) the cursors for all visible plots are displayed or (false) only the cursor associated with 
        /// the selected control is displayed.
        /// </summary>
        public static bool MultiCursor
        {
            get { return m_MultiCursor; }
            set 
            {
                m_MultiCursor = value;

                // Ensure that the hover coordinates are outside the control graphics area.
                OnMultiCursorMouseMove(null, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
            }
        }
        #endregion MultiCursor

        #region SelectedControlList
        /// <summary>
        /// Gets a generic list of the selected user controls.
        /// </summary>
        [
        Browsable(false),
        ]
        public static List<UserControl> SelectedControlList
        {
            get { return m_SelectedControlList; }
        }
        #endregion SelectedControlList
        #endregion --- Properties ---
    }
}
