#region --- Revision History ---
/*
 * 
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  Graph.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  04/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System;
using System.ComponentModel;
using System.Drawing;

namespace CodeProject.GraphComponents
{
	/// <summary>
	/// The base class for graphs. Provides the basic framework of a graph.
	/// </summary>
	public partial class Graph : System.Windows.Forms.UserControl, IGraphElement
	{
		#region --- Member Variables ---
        /// <summary>
        /// Flag to ensure that the Dispose method is only called once.
        /// </summary>
        protected bool m_IsDisposed;

        /// <summary>
        /// The area of the rectangle that is bounded by the 2 axes. This is the area of the graph without any margins.
        /// </summary>
        protected Rectangle m_GraphArea;

        /// <summary>
        /// The color of the GraphArea.
        /// </summary>
        protected Color m_GraphAreaColor = Color.White;

        /// <summary>
        /// The style of the gridlines.
        /// </summary>
        protected GridStyles m_Gridlines = GridStyles.Grid;

        /// <summary>
        /// The color of the gridlines.
        /// </summary>
        protected Color m_GridlineColor = Color.LightGray;

        /// <summary>
        /// The amount of transparency in the bar. Values range from 0 to 255, 0 is fully transparent, 255 is opaque.
        /// </summary>
        protected byte m_Transparency = 100;

        /// <summary>
        /// The number of graduations on the X axis.
        /// </summary>
        protected int m_GraduationsX = 5;

        /// <summary>
        /// The number of graduations on the Y axis.
        /// </summary>
        protected int m_GraduationsY = 5;

        /// <summary>
        /// Color of the X axis.
        /// </summary>
        protected Color m_XAxisColor = Color.Black;

        /// <summary>
        /// Color of the Y axis.
        /// </summary>
        protected Color m_YAxisColor = Color.Blue;

        /// <summary>
        /// The .NET style of format for displaying the value. Eg. {0:F}, {0:E} etc.
        /// </summary>
        protected string m_ValueFormat = string.Empty;
        #endregion --- Member Variables ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the user control.
        /// </summary>
        /// <param name="disposing">True, to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Cleanup(bool disposing)
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
        }
        #endregion --- Cleanup ---

        #region --- Methods ---
		/// <summary>
		/// Refreshes the display by updating the control. This is the refresh method used during runtime and in the designer.
		/// </summary>
		protected void RefreshDisplay ()
		{
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

			Invalidate ();
			this.Update ();
		}

		/// <summary>
		/// Draw the graph component. This method is overridden in the derived class. 
		/// </summary>
		/// <param name="graphics"></param>
		public virtual void Draw(Graphics graphics)
		{
		}
		#endregion --- Methods ---

        #region --- Properties ---
        #region IsDisposed
        /// <summary>
        /// Gets or sets the flag which indicates whether the Dispose() method has been called. True, indicates that the Dispose() method has been called; otherwise, 
        /// false.
        /// </summary>
        protected new bool IsDisposed
        {
            get
            {
                lock (this)
                {
                    return m_IsDisposed;
                }
            }

            set
            {
                lock (this)
                {
                    m_IsDisposed = value;
                }
            }
        }
        #endregion IsDisposed

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
        public virtual string ValueFormat
        {
            get { return m_ValueFormat; }
            set
            {
                m_ValueFormat = value;
                RefreshDisplay();
            }
        }
        #endregion ValueFormat

        #region XAxisColor
        /// <summary>
        /// Gets or sets the color of the X axis.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("Color of the X axis.")
        ]
        public virtual Color XAxisColor
        {
            get { return m_XAxisColor; }
            set
            {
                m_XAxisColor = value;
                RefreshDisplay();
            }
        }
        #endregion XAxisColor

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
        public virtual Color YAxisColor
        {
            get { return m_YAxisColor; }
            set
            {
                m_YAxisColor = value;
            }
        }
        #endregion YAxisColor

        #region Transparency
        /// <summary>
        /// Gets or sets the amount of transparency in the bar. Values range from 0 to 255, 0 is fully transparent, 255 is opaque.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The amount of transparency in the bar. Values range from 0 to 255, 0 is fully transparent, 255 is opaque.")
        ]
        public byte Transparency
        {
            get { return m_Transparency; }
            set
            {
                m_Transparency = value;
                RefreshDisplay();
            }
        }
        #endregion Transparency

        #region GridlineColor
        /// <summary>
        /// Gets or sets the color of the gridlines.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("Color of the grid lines.")
        ]
        public Color GridlineColor
        {
            get { return m_GridlineColor; }
            set
            {
                m_GridlineColor = value;
                RefreshDisplay();
            }
        }
        #endregion GridlineColor

        #region GraphAreaColor
        /// <summary>
        /// Gets or sets the color of the graph's area.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("Color of the graph's area.")
        ]
        public virtual Color GraphAreaColor
        {
            get { return m_GraphAreaColor; }
            set
            {
                m_GraphAreaColor = value;
                RefreshDisplay();
            }
        }
        #endregion GraphAreaColor

        #region Gridlines

        /// <summary>
        /// Gets or sets the style of the gridlines.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("Style of the gridlines.")
        ]
        public virtual GridStyles Gridlines
        {
            get { return m_Gridlines; }
            set
            {
                m_Gridlines = value;
                RefreshDisplay();
            }
        }
        #endregion Gridlines

        #region GraduationsX
        /// <summary>
        /// Gets or sets the number of graduations on the X axis.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The number of graduations on the X axis.")
        ]
        public int GraduationsX
        {
            get { return m_GraduationsX; }
            set
            {
                m_GraduationsX = value;
                RefreshDisplay();
            }
        }
        #endregion GraduationsX

        #region GraduationsY
        /// <summary>
        /// Gets or sets the number of graduations on the Y axis.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The number of graduations on the Y axis.")
        ]
        public int GraduationsY
        {
            get { return m_GraduationsY; }
            set
            {
                m_GraduationsY = value;
                RefreshDisplay();
            }
        }
        #endregion GraduationsY

        #region GraphArea
        /// <summary>
        /// Gets or sets the area of the rectangle that is bounded by the 2 axes i.e. the area of the graph without any margins.
        /// </summary>
        [
        Browsable(false)
        ]
        public Rectangle GraphArea
        {
            get { return m_GraphArea; }
            set
            {
                m_GraphArea = value;
            }
        }
        #endregion GraphArea
        #endregion --- Properties ---
	}
}
