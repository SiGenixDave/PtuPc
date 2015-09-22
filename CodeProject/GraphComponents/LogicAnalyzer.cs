#region --- Revision History ---
/*
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  LogicAnalyzer.cs
 * 
 *  Revision History
 *  ----------------
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
 *  03/31/11    1.2     K.McD           1.  Replaced a number of string constants with readonly member variables initialized by a string resource.
 *                                      2.  Extended the color definitions such that the plot and cursor colors can be specified seperately.
 *                                      
 *  10/01/11    1.3     K.McD           1.  Modified the DrawXY() method to take into account the changes to the signature of the FindXY() method.
 *                                      2.  Auto-modified as a result of name changes to a number of controls in the parent class.
 *                                      
 *  10/10/11    1.4     K.McD           1.  Removed the check as to whether the Y hover coordinates were within the bounds of the control graph area from 
 *                                          the DrawXYText() method. This modification was carried out as, firstly, the Y coordinate is not used and secondly, when 
 *                                          displaying multiple/simultaneous cursors the Y coordinate of the mouse cursor may well fall outside the graph area of 
 *                                          the individual control as common coordinates are used and the heigh of each control may well be different.
 *
 *	11/14/11	1.4.1	Sean.D			1.	Modified the DrawXYText method to use "using" to ensure object disposal.
 *	
 *  05/11/15    2.0     K.McD           References
 *                                      1.  SNCR - R188 PTU [20-Mar-2015] Item 14. If the data stream downloaded from the VCU cannot be plotted, rather than throwing
 *                                          an exception, the Plotter control should simply display an error message on the plot.
 *                                          
 *                                      Modifications
 *                                      1.  Added a try/catch block to the Graphics.DrawLines() call in the PlotPointsListArray() method.
 *                                      2.  Included a check in all Mouse event handlers to return from the handler if the m_InvalidData flag is asserted.
 *                                      3.  Included code in the catch block to draw the error message string and to assert the m_InvalidData flag.
 */
#endregion --- Revision History ---

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

using CodeProject.GraphComponents.Properties;

namespace CodeProject.GraphComponents
{
    /// <summary>
    /// A user control to plot the state of a digital variable against time.
    /// </summary>
    public partial class LogicAnalyzer : Plotter
    {
        #region --- Constants ---
        /// <summary>
        /// The transparency of the XYText. Value: 50.
        /// </summary>
        private const int TransparencyXYText = 50;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The text that is to appear in the cursor if the digital is in the alarm state.
        /// </summary>
        private readonly string m_TextAsserted = Resources.TextAsserted;

        /// <summary>
        /// The text that is to appear in the cursor if the digital is in the clear state.
        /// </summary>
        private readonly string m_TextClear = Resources.TextClear;

        /// <summary>
        /// The pen color that is to be used to draw the plot when the digital value is in the alarm state.
        /// </summary>
        private readonly Color m_AlarmStatePlotColor = Color.Black;

        /// <summary>
        /// The pen color that is to be used to highlight the cursor when the digital value is in the alarm state.
        /// </summary>
        private readonly Color m_AlarmStateCursorColor = Color.Yellow;

        /// <summary>
        /// The pen color that is to be used to highlight the cursor when the digital value is in the clear state.
        /// </summary>
        private readonly Color m_ClearStateCursorColor = Color.ForestGreen;

        /// <summary>
        /// The pen color that is to be used to draw the plot when the digital value is in the clear state.
        /// </summary>
        private readonly Color m_ClearStatePlotColor = Color.ForestGreen;

        /// <summary>
        /// The digital state that represents an active alarm. The default state is TRUE i.e. low to high transition represents an alarm.
        /// </summary>
        private bool m_AlarmState = true;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Creates and initializes a new logic analyzer user control.
        /// </summary>
        public LogicAnalyzer() : base()
        {
            InitializeComponent();
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
                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                #region - [Detach the event handler methods.] -
                #endregion - [Detach the event handler methods.] -

                #region - [Component Designer Variables] -
                #endregion - [Component Designer Variables] -
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
            }
            finally
            {
                base.Cleanup(disposing);
            }
        }
        #endregion --- Cleanup ---

        #region --- Methods ---
        #region - IGraphElement Members -
        /// <summary>
        /// The main drawing routine that renders the points onto the graph.
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

            graphics.SetClip(m_GraphArea);

            // Draw the grid within the graph area.
            m_Gridline.Draw(graphics);
            m_YAxisColor = Color.Black;

            // Draw the X and Y axes.
            m_AxisLineXandY.Draw(graphics);

            // Draw the X axis values, no Y axis values for the logic analyzer control.
            DrawXAxisValues(graphics);
           
            graphics.SetClip(m_GraphArea);

            if (m_CurrentState == PlotterState.Reset)
            {
                return;
            }

            // Only draw the vertical line representing the time of the trip if the data corresponds to a fault log.
            if (m_IsFaultLog)
            {
                DrawTripTime(graphics);
            }

            #region - Check for Scrolling -
            int xRangeInMs = (int)(m_XRange.Duration().Ticks / TimeSpan.TicksPerMillisecond);
            m_RightDisplayLimit = m_LeftDisplayLimit + xRangeInMs;

            if (m_TotalTimeElapsed > m_RightDisplayLimit)
            {
                if (m_CurrentState == PlotterState.Running)
                {
                    m_HScrollBar.Maximum = m_PointsToRemove;
                    m_HScrollBar.Value = m_PointsToRemove;
                    m_PointsToRemove++;

                    PointF leftPosition = (PointF)ActiveChannel.Points[m_PointsToRemove];
                    #region - Adjust Scrolling Rate -
                    // if the user has changed the plotting rate, then we need to adjust the scrolling at the left, as it has to scroll at the old plotting rate.
                    // If the scrolling is too slow, the new points won't appear on the right edge.
                    int leftDisplayLimitDelta = (int)(leftPosition.X - m_LeftDisplayLimit);

                    if (leftDisplayLimitDelta > m_PlotIntervalMs)
                        m_LeftDisplayLimit = (int)leftPosition.X;
                    else
                        m_LeftDisplayLimit += m_PlotIntervalMs;
                    #endregion - Adjust Scrolling Rate -
                }
            }
            #endregion - Check for Scrolling -

            bool scrollAdjusted = false;
            // ----------------------------------------------------------
            // Plot the data values.
            // ----------------------------------------------------------
            // Only 1 channel is used for the logic analyzer user control.
            Channel channel = Channels[0];

            // Skip, if the channel hasn't been enabled.
            if (!channel.Enabled)
            {
                return;
            }

            if (!scrollAdjusted)
            {
                m_TotalTimeElapsed = channel.TotalTimeElapsed;
                if (m_TotalTimeElapsed > m_RightDisplayLimit)
                {
                    if (m_CurrentState == PlotterState.Running)
                    {
                        m_HScrollBar.Maximum = m_PointsToRemove;
                        m_HScrollBar.Value = m_PointsToRemove;
                        m_PointsToRemove++;

                        PointF leftPosition = (PointF)channel.Points[m_PointsToRemove];
                        #region - Adjust Scrolling Rate -
                        // if the user has changed the plotting rate, then we need to adjust the scrolling at the left, as it has to scroll at the old plotting rate.
                        // If the scrolling is too slow, the new points won't appear on the right edge.
                        int leftDisplayLimitDelta = (int)(leftPosition.X - m_LeftDisplayLimit);

                        if (leftDisplayLimitDelta > m_PlotIntervalMs)
                            m_LeftDisplayLimit = (int)leftPosition.X;
                        else
                            m_LeftDisplayLimit += m_PlotIntervalMs;

                        scrollAdjusted = true;
                        #endregion - Adjust Scrolling Rate -
                    }
                }
            }

            // -------------------------------------------------------------------------------------------------
            // The plot may contain gaps in the data, for example when the unit is powered down, and these gaps should be drawn using a 
            // transparent pen. The continuous data between these gaps are known as blocks of data and the points associated with each 
            // block of data are stored in an ArrayList. Each element of the array of ArrayLists contains the data associated with a 
            // particular block. If there are no gaps in the data there will only be 1 array element.
            // -------------------------------------------------------------------------------------------------

            // Create an array of ArrayLists containing the points to be plotted associated with the specified channel.
            ArrayList[] pointsList = AddToPointsListArray(channel);

            // Plot the points defined in each element of the ArrayList.
            PlotPointsListArray(graphics, channel, pointsList);

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
        protected override void DrawXYText(Graphics graphics)
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

            // For normal log files, round the selected time to the nearest 10ms i.e. 376 ms mapped to 380ms, 371ms mapped to 370ms. For minute, hour and day logs round
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
                actualX = m_LeftDisplayLimit + (long)(hoveringValue.X + m_DataIntervalMs / 2);
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
                // Display the current alarm state depending upon the active alarm state and the current value.
                if (m_AlarmState == true)
                {
                    // HIGH represents the alarm state, LOW represents clear.
                    if (y.Equals(Channel.FloatRepresentationOfSet))
                    {
                        val = m_TextAsserted;
                    }
                    else if (y.Equals(Channel.FloatRepresentationOfClear))
                    {
                        val = m_TextClear;
                    }
                    else
                    {
                        val = string.Empty;
                    }
                }
                else
                {
                    // LOW represents the alarm state, HIGH represents clear.
                    if (y.Equals(Channel.FloatRepresentationOfSet))
                    {
                        val = m_TextClear;
                    }
                    else if (y.Equals(Channel.FloatRepresentationOfClear))
                    {
                        val = m_TextAsserted;
                    }
                    else
                    {
                        val = string.Empty;
                    }
                }
                coordinate = GetFormatForTime(actualX) + ", " + val;
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

					// Use the colour appropriate to the alarm state and the current value.
					Color rectColor;
					if (m_AlarmState == true)
					{
						rectColor = (y.Equals(Channel.FloatRepresentationOfSet)) ? Color.FromArgb(TransparencyXYText, m_AlarmStateCursorColor) : 
                                                                                   Color.FromArgb(TransparencyXYText, m_ClearStateCursorColor);
					}
					else
					{
						rectColor = (y.Equals(Channel.FloatRepresentationOfSet)) ? Color.FromArgb(TransparencyXYText, m_ClearStateCursorColor) : 
                                                                                   Color.FromArgb(TransparencyXYText, m_AlarmStateCursorColor);
					}

					using (Brush rectBrush = new SolidBrush(rectColor))
					{
						graphics.FillRectangle(rectBrush, rect);
					}

					// Write the string that is enclosed within the box.
					using (Brush textBrush = new SolidBrush(activeCh.ChannelColor))
					{
						graphics.DrawString(coordinate, Font, textBrush, rect, sf);
					}
				}
			}
        }

        /// <summary>
        /// Create an array of ArrayLists containing the points to be plotted for each block of data. This method also adds the required points 
        /// into the ArrayList[] so that the digital state transitions appear as a vertical line.
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

            // -----------------------------------------------------------------------------------------------------------------
            // Populate the ArrayList[] with the points to be plotted. For state transitions don't draw directly to the next point, instead, make transition edges
            // vertical. Also  only include points where transitions occur, this improves the processing time of the method.
            // -----------------------------------------------------------------------------------------------------------------

            // Local variable to store the current element of the generic list of Points associated with the channel.
            PointF pointStored = new PointF();

            // Local variable to store the pixel value corresponding to the current element of the generic list of Points associated with the channel.
            Point p;

            // Local variable to store the previous element of the generic list of Points associated with the channel.
            PointF pointStoredPrev = new PointF();
            if (m_CurrentState == PlotterState.Stopped)
            {
                // The index of the current block of data.
                int blockIndex = 0;

                // True, if this is the first entry of the generic list; otherwise, false.
                bool firstEntry = true;

                // True, if this is the first entry of a new block;
                bool newBlock = false;
                for (int index = 0; index < channel.Points.Count; index++ )
                {
                    // Get the current entry of the generic list.
                    pointStored = channel.Points[index];

                    #region - First Entry -
                    // Check whether this is the first entry in the generic list.
                    if (firstEntry)
                    {
                        firstEntry = false;

                        // Add the pixel value corresponding to the first entry of the generic list to the ArrayList[].
                        p = GetPixelFromValue(channel, (int)pointStored.X, pointStored.Y);
                        pointsList[blockIndex].Add(p);

                        // Check if the generic list contains further breakpoints.
                        if (blockIndex < channel.BreakPoint.Count)
                        {
                            // If the current time value corresponds to a break point increment the block index.
                            if (pointStored.X == channel.BreakPoint[blockIndex])
                            {
                                blockIndex++;

                                // Ensure that the next entry in the generic list is recorded to the next ArrayList[] element.
                                newBlock = true;
                            }
                        }
                    }
                    #endregion - First Entry -

                    #region - Last Entry -
                    else if (index == channel.Points.Count - 1)                   // Check whether this is the last entry in the generic list.
                    {
                        // Add the pixel value corresponding to the current entry of the generic list to the ArrayList[] and include an interim plot value so that
                        // the transition is drawn as a vertical line if a state change has occurred.
                        if (pointStored.Y != pointStoredPrev.Y)
                        {
                            // Add interim plot value so that transition is drawn as a vertical line.
                            p = GetPixelFromValue(channel, (int)pointStored.X, pointStoredPrev.Y);
                            pointsList[blockIndex].Add(p);
                        }
                        p = GetPixelFromValue(channel, (int)pointStored.X, pointStored.Y);
                        pointsList[blockIndex].Add(p);
                    }
                    #endregion - Last Entry -

                    #region - New Block -
                    else if (newBlock)
                    {
                        newBlock = false;

                        // Add the pixel value corresponding to the current entry of the generic list to the ArrayList[] and include an interim plot value so that the
                        // transition is drawn as a vertical line if a state change has occurred.
                        if (pointStored.Y != pointStoredPrev.Y)
                        {
                            // Add interim plot value so that transition is drawn as a vertical line.
                            p = GetPixelFromValue(channel, (int)pointStored.X, pointStoredPrev.Y);
                            pointsList[blockIndex].Add(p);
                        }
                        p = GetPixelFromValue(channel, (int)pointStored.X, pointStored.Y);
                        pointsList[blockIndex].Add(p);

                        // Check if the generic list contains further breakpoints.
                        if (blockIndex < channel.BreakPoint.Count)
                        {
                            // If the current time value corresponds to a break point increment the block index.
                            if (pointStored.X == channel.BreakPoint[blockIndex])
                            {
                                blockIndex++;

                                // Ensure that the next entry in the generic list is recorded to the next ArrayList[] element.
                                newBlock = true;
                            }
                        }
                    }
                    #endregion - New Block -

                    #region - Process Entry -
                    else
                    {
                        // Only record values where the state of the digital IO has changed.
                        if (pointStored.Y != pointStoredPrev.Y)
                        {
                            // A transition has occurred, add the interim plot value so that transition is drawn as a vertical line.
                            p = GetPixelFromValue(channel, (int)pointStored.X, pointStoredPrev.Y);
                            pointsList[blockIndex].Add(p);

                            p = GetPixelFromValue(channel, (int)pointStored.X, pointStored.Y);
                            pointsList[blockIndex].Add(p);

                            // Check if the generic list contains further breakpoints.
                            if (blockIndex < channel.BreakPoint.Count)
                            {
                                // If the current time value corresponds to a break point increment the block index.
                                if (pointStored.X == channel.BreakPoint[blockIndex])
                                {
                                    blockIndex++;
                                    newBlock = true;
                                }
                            }

                        }
                        else
                        {
                            if (blockIndex < channel.BreakPoint.Count)  // Check if the generic list contains further breakpoints.
                            {
                                // If the current time value corresponds to a break point increment the block index and add the pixel value corresponding to the 
                                // current entry of the generic list to the ArrayList[].
                                if (pointStored.X == channel.BreakPoint[blockIndex])
                                {
                                    // Add the pixel value corresponding to the current entry of the generic list to the ArrayList[].
                                    p = GetPixelFromValue(channel, (int)pointStored.X, pointStored.Y);
                                    pointsList[blockIndex].Add(p);

                                    blockIndex++;
                                    newBlock = true;
                                }
                            }
                        }
                    }

                    // Copy the current value to the previous value in order to detect state changes.
                    pointStoredPrev = pointStored;
                    #endregion - Process Entry -
                }
            }
            else
            {
                // --------------------------------------------------------------
                // Note: There are no break points associated with real time mode.
                // --------------------------------------------------------------
                for (int j = 0; j < channel.Points.Count - m_PointsToRemove; j++)
                {
                    pointStored = (PointF)channel.Points[j + m_PointsToRemove];
                    if ((j != 0) && (pointStoredPrev.Y != pointStored.Y))
                    {
                        // Transition has occurred, add interim plot value so that transition is drawn as a vertical line.
                        p = GetPixelFromValue(channel, (int)pointStored.X, pointStoredPrev.Y);
                        pointsList[0].Add(p);
                    }
                    p = GetPixelFromValue(channel, (int)pointStored.X, pointStored.Y);
                    pointsList[0].Add(p);
                    pointStoredPrev = pointStored;
                }
            }
            return pointsList;
        }

        /// <summary>
        /// Plot the points contained within the specified ArrayList[]. Each element of ArrayList[] contains a continuous set of points to be plotted. 
        /// If there are no gaps in the data to be plotted the array will contain a single element.
        /// </summary>
        /// <param name="graphics">Reference to the The GDI+ drawing surface.</param>
        /// <param name="channel">The channel reference.</param>
        /// <param name="pointsList">An array of ArrayLists where each element of the array contains the points to be plotted for each continuous block of data.</param>
        private void PlotPointsListArray(Graphics graphics, Channel channel, ArrayList[] pointsList)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Convert the ArrayList[] of pixel coordinates to a jagged array i.e. an array whose elements are an array, ready for plotting.
            Point p;
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

            // Plot each block of pixel coordinates.
            Point pointFloatRepresentationOfSet = GetPixelFromValue(channel, 0, Channel.FloatRepresentationOfSet);

            // The pen colour.
            try
            {
                Color color;
                for (int block = 0; block <= channel.BreakPoint.Count; block++)
                {
                    for (int index = 1; index < pointsToPlot[block].Length; index++)
                    {
                        // Skip if there aren't at least 2 points to plot.
                        if (pointsToPlot[block].Length <= 1)
                        {
                            continue;
                        }

                        p = pointsToPlot[block][index];

                        // Set the pen colour depending on the active alarm state of the plot and the current value; clear: green, alarm: red.
                        if (m_AlarmState == true)
                        {
                            color = (p.Y == pointFloatRepresentationOfSet.Y) ? m_AlarmStatePlotColor : m_ClearStatePlotColor;
                        }
                        else
                        {
                            color = (p.Y == pointFloatRepresentationOfSet.Y) ? m_ClearStatePlotColor : m_AlarmStatePlotColor;
                        }

                        graphics.DrawLine(new Pen(color, 1.5F), pointsToPlot[block][index - 1], pointsToPlot[block][index]);
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
            return;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        #region AlarmState
        /// <summary>
        /// Gets or sets the alarm state associated with a digital plot.
        /// </summary>
        /// <remarks>The default state is TRUE i.e. low to high transition represents an alarm.</remarks>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Design"),
        Description("The alarm state associated with a digital plot."),
        ]
        public bool AlarmState
        {
            get { return m_AlarmState; }
            set { m_AlarmState = value; }
        }
        #endregion AlarmState
        #endregion --- Properties ---
    }
}
