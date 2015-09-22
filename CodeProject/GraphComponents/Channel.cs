#region --- Revision History ---
/*
 * 
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  Channels.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  04/23/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Drawing;

namespace CodeProject.GraphComponents
{
	/// <summary>
	/// This class defines the channel information associated with the plot; in this case, the word channel is derived from the name used to represent the input 
    /// terminals of an oscilloscope.
	/// </summary>
	public class Channel : IDisposable
    {
        #region --- Constants ---
        /// <summary>
        /// To use the plotter in LogicAnalyser mode, the channel value is set to 0.9 to represent a true state. Value: 0.9.
        /// </summary>
        public const float FloatRepresentationOfSet = (float)0.9;

        /// <summary>
        /// To use the plotter in LogicAnalyser mode, the channel value is set to 0.1 to represent a clear state. Value: 0.1.
        /// </summary>
        public const float FloatRepresentationOfClear = (float)0.1;

        /// <summary>
        /// The default active channel. Value: 0.
        /// </summary>
        public const int DefaultActiveChannel = 0;

        /// <summary>
        /// The default maximum value. Value: 100.
        /// </summary>
        public const int DefaultMaxValue = 100;

        /// <summary>
        /// The default minimum value. Value: 0.
        /// </summary>
        public const int DefaultMinValue = 0;

        /// <summary>
        /// The default interval, in ms, between successive data entries saved to the channel. Value: 50.
        /// </summary>
        public const int DefaultDataIntervalMs = 50;

        /// <summary>
        /// Size the generic list to cope with 40 minutes worth of data recorded every 50ms. Value: 48,000.
        /// </summary>
        private const int MaxPoints = 48000;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Ensure Dispose method is only called once.
        /// </summary>
        protected bool m_IsDisposed;

        /// <summary>
        /// The default minimum value i.e before manipulating the Y axis limits.
        /// </summary>
        private float m_DefaultMinimumValue = DefaultMinValue;
        
        /// <summary>
        /// The default maximum value i.e before manipulating the Y axis limits.
        /// </summary>
        private float m_DefaultMaximumValue = DefaultMaxValue;          
        
        /// <summary>
        /// The minimum value to display on the Y axis.
        /// </summary>
        private float m_MinimumValue = DefaultMinValue;
        
        /// <summary>
        /// The maximum value to display on the Y axis.
        /// </summary>
        private float m_MaximumValue = DefaultMaxValue;                 
        
        /// <summary>
        /// The current y value of the channel.
        /// </summary>
        private float m_CurrentValue;
		
        /// <summary>
        /// The Y axis name e.g. Voltage, Current etc.
		/// </summary>
        private string m_Name;
		
        /// <summary>
        /// A flag to indicate whether the channel is enabled.  True, indicates that the channel is enabled; otherwise, false.
        /// </summary>
        private bool m_Enabled;
		
        /// <summary>
        /// The pen colour associated with the channel.
        /// </summary>
        private Color m_ChannelColor = Color.Green;

        /// <summary>
        /// The offset of the cursor from the center line.
        /// </summary>
        private float m_CursorOffset;

        /// <summary>
        /// The X axis cursor line associated with the channel.
        /// </summary>
        private Cursor m_ChannelCursor;
        
        /// <summary>
        /// The total time elapsed, in ms, since the start of the plot.
        /// </summary>
        private long m_TotalTimeElapsed;

        /// <summary>
        /// The interval, in ms, between successive plots, default = 100ms.
        /// </summary>
		private int m_PlotIntervalMs;
        
        /// <summary>
        /// A generic list of PointF items containing the Y and corresponding time values of the data that is to be plotted.
        /// </summary>
        private List<PointF> m_Points = new List<PointF>(MaxPoints);

        /// <summary>
        /// A generic list of long integers containing only the PointF.X values of the data that is to be plotted. Helps with the search facility.
        /// </summary>
        private List<long> m_X = new List<long>(MaxPoints);

        /// <summary>
        /// A generic list of long integers containing those PointF.X values marking a break in the plot sequence i.e.transparent pen should be used 
        /// to plot between this plot entry and the next.
        /// </summary>
        private List<long> m_BreakPoint = new List<long>();
        #endregion --- Member Variables ---

		#region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
		public Channel()
		{
			m_ChannelCursor = new Cursor (this);
            m_PlotIntervalMs = DefaultDataIntervalMs;                   // Set the plot-interval to the default value;
		}

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
		public Channel (float minValue, float maxValue, string yAxisName) : this ()
		{
			this.MinimumValue = minValue;
			this.MaximumValue = maxValue;
			this.YAxisName = yAxisName;
		}

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
		public Channel (float minValue, float maxValue, string yAxisName, bool enabled) :this (minValue, maxValue, yAxisName)
		{
			this.Enabled = enabled;
		}

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
		public Channel (float minValue, float maxValue, string yAxisName, bool enabled, Color channelColor) : this (minValue, maxValue, yAxisName, enabled)
		{
			this.ChannelColor = channelColor;
        }
        #endregion --- Constructors ---

        #region --- Disposal ---
        /// <summary>
        /// Destructor / Finalizer. Because Dispose calls GC.SuppressFinalize, this method is called by the garbage collection process only
        /// if the consumer of the object doesn't call Dispose as it should.
        /// </summary>
        ~Channel()
        {
            Dispose(false);
        }

        /// <summary>
        /// Public implementation of the IDisposable.Dispose method, called by the consumer of the object in order to free unmanaged resources
        /// deterministically.
        /// </summary>
        public void Dispose()
        {
            // Call the protected Dispose overload and pass a value of 'true' to indicate that the Dispose is being called by consumer code, not
            // by the garbage collector.
            Dispose(true);

            // Because the Dispose method performs all necessary cleanup, ensure the garbage collector does not call the class destructor.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Tidy-up all resources.
        /// </summary>
        /// <param name="disposing">Specifies whether the method is being called by the consumer code (true), or the garbage collector (false).</param>
        protected void Dispose(bool disposing)
        {
            // Don't try to dispose of the object twice.
            if (m_IsDisposed == false)
            {
                try
                {
                    if (disposing)
                    {
                        // Method called by consumer code. Call the Dispose method of any managed data members that implement the dispose method.
                    }

                    // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data 
                    // members to null.
                    if (m_BreakPoint != null)
                    {
                        m_BreakPoint.Clear();
                        m_BreakPoint = null;
                    }

                    if (m_Points != null)
                    {
                        m_Points.Clear();
                        m_Points = null;
                    }

                    if (m_X != null)
                    {
                        m_X.Clear();
                        m_X = null;
                    }
                }
                catch (Exception)
                {
                    // Don't do anything, just ensure that we don't throw an exception.
                }
            }
            m_IsDisposed = true;
        }
        #endregion --- Disposal ---

        #region --- Methods ---
        /// <summary>
        /// Add the y value and corresponding monitor real time clock offset since start of recording into an ArrayList for use by the plotter control.
        /// </summary>
        /// <param name="yValue">The y value associated with the specified time.</param>
        /// <param name="totalTimeElapsed">The time, in ms, of the 'CurrentMonitorTime' value of the current record minus the 'StartTime' of the
        /// X axis display.</param>
        public void SetYTValue(float yValue, long totalTimeElapsed)
        {
            m_CurrentValue = yValue;
            m_TotalTimeElapsed = totalTimeElapsed;
            PointF p = new PointF(this.m_TotalTimeElapsed, this.m_CurrentValue);

            // Add the PointF item to the generic list.
            m_Points.Add(p);

            // Add only the time component to this generic list. Used to search for the time and get the index value as a lookup into m_Points.
            m_X.Add(m_TotalTimeElapsed);
     
        }

        /// <summary>
        /// Add the state and the corresponding monitor real time clock offset since start of recording into an ArrayList for use by the plotter control.
        /// </summary>
        /// <param name="state">The digital state associated with the specified time.</param>
        /// <param name="totalTimeElapsed">The time, in ms, of the 'CurrentMonitorTime' value of the current record minus the 'StartTime' of the X axis display.</param>
        public void SetState(bool state, long totalTimeElapsed)
        {
            m_CurrentValue = (state) ? FloatRepresentationOfSet : FloatRepresentationOfClear;
            m_TotalTimeElapsed = totalTimeElapsed;
            PointF p = new PointF(this.m_TotalTimeElapsed, this.m_CurrentValue);

            // Add the PointF item to the generic list.
            m_Points.Add(p);

            // Add only the time component to this generic list. Used to search for the time and get the index value as a lookup into m_Points.
            m_X.Add(m_TotalTimeElapsed);
        }

        /// <summary>
        /// Record a break in the plot sequence of a log i.e the start of a period where the unit has been powered down. The break 
        /// point is specified as the time, in ms, since the start of the plot. During these power down periods the pen colour of the plot is set 
        /// to transparent.
        /// </summary>
        /// <param name="breakPoint">The time, in ms, since the 'StartTime' of the X axis display, where the break point is to be recorded.</param>
        public void SetBreakPoint(long breakPoint)
        {
            // Add this break point to the current list.
            m_BreakPoint.Add(breakPoint);
        }

        /// <summary>
        /// Clear the generic lists associated with the channel. This must be carried out whenever the data values to be plotted are updated.
        /// </summary>
        public void Reset()
        {
            m_BreakPoint.Clear();
            m_Points.Clear();
            m_X.Clear();
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the minimum value associated with the Y axis.
        /// </summary>
        public float MinimumValue
        {
            get { return m_MinimumValue; }
            set { m_MinimumValue = value; }
        }

        /// <summary>
        /// Gets or sets the maximum value associated with the Y axis.
        /// </summary>
        public float MaximumValue
        {
            get { return m_MaximumValue; }
            set { m_MaximumValue = value; }
        }

        /// <summary>
        /// Gets or sets the default maximum value i.e. the maximum value associated with the Y axis before manipulating the limits.
        /// </summary>
        public float DefaultMaximumValue
        {
            get { return m_DefaultMaximumValue; }
            set { m_DefaultMaximumValue = value; }
        }

        /// <summary>
        /// Gets or sets the default minimum value i.e. the minimum value associated with the Y axis before manipulating the limits.
        /// </summary>
        public float DefaultMinimumValue
        {
            get { return m_DefaultMinimumValue; }
            set { m_DefaultMinimumValue = value; }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public float CurrentValue
        {
            get { return m_CurrentValue; }
            set
            {
                m_CurrentValue = value;
                PointF p = new PointF(m_TotalTimeElapsed, m_CurrentValue);

                // Add the PointF item to the generic list.
                m_Points.Add(p);

                // Add only the time component to this generic list. Used to search for the time and get the index value for lookup into m_Points.
                m_X.Add(m_TotalTimeElapsed);

                // Increment the elapsed time by the plot rate.
                m_TotalTimeElapsed += this.PlotIntervalMs;
            }
        }

        /// <summary>
        /// Gets or sets the current state.
        /// </summary>
        /// <remarks>
        /// Only applicable if the channel is being used to plot digital values.
        /// </remarks>
        public bool State
        {
            get
            {
                return (m_CurrentValue.Equals(FloatRepresentationOfSet)) ? true : false;
            }

            set
            {
                this.CurrentValue = (value) ? FloatRepresentationOfSet : FloatRepresentationOfClear;
            }
        }

        /// <summary>
        /// Gets or sets the flag that indicates whether the channel is enabled. True, indicates that the channel is enabled; otherwise, false.
        /// </summary>
        public bool Enabled
        {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }

        /// <summary>
        /// Get or sets the name for the Y axis. Eg. Voltage, Current etc.
        /// </summary>
        public string YAxisName
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        /// <summary>
        /// Gets or sets the channel colour.
        /// </summary>
        public Color ChannelColor
        {
            get { return m_ChannelColor; }
            set { m_ChannelColor = value; }
        }

        /// <summary>
        /// Gets or sets the offset of the cursor from the centre line.
        /// </summary>
        internal float CursorOffset
        {
            get { return m_CursorOffset; }
            set
            {
                m_CursorOffset = value;
                m_MaximumValue -= m_CursorOffset;
                m_MinimumValue += m_CursorOffset;
            }
        }

        /// <summary>
        /// Gets or sets the total time, in ms, between the start of the plot and the time of the current plot.
        /// </summary>
        internal long TotalTimeElapsed
        {
            get { return m_TotalTimeElapsed; }
            set { m_TotalTimeElapsed = value; }
        }

        /// <summary>
        /// Gets or sets the interval, in ms, between successive plots.
        /// </summary>
        internal int PlotIntervalMs
        {
            get { return m_PlotIntervalMs; }
            set { m_PlotIntervalMs = value; }
        }

        /// <summary>
        /// Gets or sets the X axis cursor line associated with the channel.
        /// </summary>
        internal Cursor ChannelCursor
        {
            get { return m_ChannelCursor; }
        }

        /// <summary>
        /// Generic list of <c>PointF</c> items containing the Y and corresponding time values of the data that is to be plotted.
        /// </summary>
        public List<PointF> Points
        {
            get { return m_Points; }
        }

        /// <summary>
        /// Generic list of <c>long</c> integers containing the PointF.X values i.e. RTC values of the data that is to be plotted.
        /// </summary>
        public List<long> X
        {
            get { return m_X; }
        }

        /// <summary>
        /// Generic list of <c>long</c> integers containing those PointF.X values marking the breaks in the plot sequence of a min/hour/day log 
        /// or RTD log i.e the times, in ms, since the start of the plot corresponding to those plot entries where the unit has been powered down until the 
        /// period defined by the following plot entry time. During these power down periods the pen colour of the plot is set to transparent.
        /// </summary>
        public List<long> BreakPoint
        {
            get { return m_BreakPoint; }
        }
        #endregion --- Properties ---
	}
}
