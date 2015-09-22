#region --- Revision History ---
/*
 * 
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  PlotterEventArgs.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  04/13/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System;

namespace CodeProject.GraphComponents
{
	/// <summary>
	/// Support for events associated with the <c>Plotter</c> class.
	/// </summary>
	public class PlotterEventArgs : EventArgs
    {
        #region --- Member Variables ---
        /// <summary>
		/// Reference to the plotter that caused the event to be raised.
		/// </summary>
		private Plotter m_Plotter;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
		/// Initailizes a new instance of the class.
		/// </summary>
		/// <param name="plotter">The plotter that caused the event to be raised</param>
		public PlotterEventArgs (Plotter plotter)
		{
			m_Plotter = plotter;
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// Gets the plotter that caused the event to be raised.
        /// </summary>
        public Plotter CurrentPlotter
        {
            get { return m_Plotter; }
        }
        #endregion --- Properties ---
    }
}
