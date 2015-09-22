#region --- Revision History ---
/*
 * 
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  GraphTypedefs.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  09/23/10    1.1     K.McD           1.  Minor text changes to the XML tags associated with the TimeAxisStyle enumerator.
 * 
 */
#endregion --- Revision History ---

using System;

namespace CodeProject.GraphComponents
{
    #region --- Enumerated Data Types ---
    /// <summary>
	/// Style in which the gridlines appear on the graph.
	/// </summary>
	[Flags]
	public enum GridStyles : int
	{
        /// <summary>
        /// No Grid.
        /// </summary>
		None       = 0,

        /// <summary>
        /// Horizontal lines only.
        /// </summary>
		Horizontal = 1,

        /// <summary>
        /// Vertical lines only.
        /// </summary>
		Vertical   = 2,

        /// <summary>
        /// Grid lines.
        /// </summary>
		Grid       = 3,
	}

	/// <summary>
	/// Orientation of the bar on the <c>BarGraph</c>.
	/// </summary>
	public enum Orientation
	{
        /// <summary>
        /// Vertical.
        /// </summary>
		Vertical,

        /// <summary>
        /// Horizontal.
        /// </summary>
		Horizontal,
	}

	/// <summary>
	/// The location where the graduations must appear.
	/// </summary>
	public enum Graduation
	{
        /// <summary>
        /// No graduations.
        /// </summary>
		None,

        /// <summary>
        /// Centre.
        /// </summary>
		Center,

        /// <summary>
        /// Edge 1.
        /// </summary>
		Edge1,

        /// <summary>
        /// Edge 2.
        /// </summary>
		Edge2,
	}

	/// <summary>
	/// Style in which the bar's values are to be displayed on the bar.
	/// </summary>
	public enum TextAlignment
	{
        /// <summary>
        /// Absolute centre.
        /// </summary>
		AbsoluteCenter,

        /// <summary>
        /// Bar value centre.
        /// </summary>
		BarValueCenter,

        /// <summary>
        /// Use smart positioning.
        /// </summary>
		Smart,

        /// <summary>
        /// None.
        /// </summary>
		None,
	}

	/// <summary>
	/// Style in which the time axis value is to be shown.
	/// </summary>
	public enum TimeAxisStyle
	{
		/// <summary>
        /// Shows the time in milliseconds relative to the start of the plot eg. 10, 20, ..., 50000.
		/// </summary>
		Millisecond,

		/// <summary>
        /// Shows the time in milliseconds relative to the start of the plot, with the 'ms' suffix eg. 10ms, 20ms, ..., 50000ms.
		/// </summary>
		MillisecondWithUnitDisplay,

		/// <summary>
        /// Shows the time in seconds relative to the start of the plot eg. 10, 20, ..., 50000.
		/// </summary>		
		Second,

		/// <summary>
        /// Shows the time in seconds relative to the start of the plot, with the 's' suffix eg. 10s, 20s, ..., 50000s.
		/// </summary>
		SecondWithUnitDisplay,

		/// <summary>
        /// Shows the time in the most appropriate format relative to the start of the plot.
		/// </summary>
		Smart,

        /// <summary>
        /// Shows the absolute time in the format HH:MM:SS.xx e.g. 01:24:37.67. Used when displaying historic data.
        /// </summary>
        Absolute
    }
    #endregion --- Enumerated Data Types ---
}
