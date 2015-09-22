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
 *  File name:  PlotterEnumeratorParent.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  02/16/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  10/02/11    1.1     K.McD           1.  Modified the DrawXYText() method to accommodate the changes to the signature of the Plotter.FindY() method.
 *  
 *  10/07/11    1.2     K.McD           1.  Removed the check that the mouse hover Y coordinate is within the bounds of the graph area for the current user control 
 *                                          from the DrawXYText() method. This modification was carried out as, firstly, the Y coordinate is not used and secondly, 
 *                                          when displaying multiple/simultaneous cursors the Y coordinate of the mouse cursor may well fall outside the bounds of the 
 *                                          control as common coordinates are used for each control and the scalar plotter controls are a different height to the 
 *                                          enumerator and bitmask plotter controls.
 *                                          
 *	11/14/11	1.2.1	Sean.D			1.	Modified DrawXYText to use "using" to ensure proper disposal of StringFormat and Pen objects.
 *	
  */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;

using CodeProject.GraphComponents;
using Common.Configuration;
using Common.Forms;

namespace Common.UserControls
{
    /// <summary>
    /// User control to plot enumerator watch variables.
    /// </summary>
    public partial class PlotterEnumeratorParent : Plotter
    {
        #region --- Constants ---
        /// <summary>
        /// The transparency of the XYText. Value: 130.
        /// </summary>
        private const int TransparencyXYText = 130;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The old identifier of the enumerator watch variable that is to be plotted. User to determine the text that is to be displayed corresponding to the 
        /// current value.
        /// </summary>
        protected short m_OldIdentifier;

        /// <summary>
        /// Reference to the client form as type <c>FormPTU</c>.
        /// </summary>
        private FormPTU m_ClientAsFormPTU;

        /// <summary>
        /// The watch variable that is to be plotted;
        /// </summary>
        private WatchVariable m_WatchVariable;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the user control.
        /// </summary>
        public PlotterEnumeratorParent() : base()
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
                m_WatchVariable = null;
                m_ClientAsFormPTU = null;

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

            if (m_MouseHoverCoordinates.X < m_GraphArea.Left || m_MouseHoverCoordinates.X > m_GraphArea.Right)
            {
                return;
            }

            // For normal logs, round the selected time to the nearest 10ms i.e. 376 ms mapped to 380ms, 371ms mapped to 370ms.
            Channel activeCh = Channels[m_ActiveChannelIndex];
            PointF hoveringValue = GetValueFromPixel(activeCh, m_MouseHoverCoordinates.X, m_MouseHoverCoordinates.Y);

            // The actual time, in ms, selected by the user and the remainder value when dividing by the display interval.
            long actualX, modulo;

            // The y value associated with the selected time.
            float y;
            actualX = m_LeftDisplayLimit + (long)(hoveringValue.X + CursorResolutionHalf);
            modulo = actualX % CursorResolution;
            actualX -= modulo;
            y = FindY(actualX, activeCh);

            // Derive the string value that is to be displayed in the box.
            string coordinate = string.Empty;
            string enumeratorText;
            if (float.IsNaN(y))
            {
                // y value not found, display -.
                coordinate = GetFormatForTime(actualX) + ", -";
            }
            else
            {
                uint yAsUint = (uint)y;
                try
                {
                    // Check whether the actual enumerator value is to be displayed or the corresponding enumerated text.
                    if (m_ClientAsFormPTU.MainWindow.Enumeration == true)
                    {
                        enumeratorText = yAsUint.ToString() + " - " +Lookup.WatchVariableTableByOldIdentifier.GetEnumeratorText(m_OldIdentifier, yAsUint);
                    }
                    else
                    {
                        enumeratorText = yAsUint.ToString();
                    }
                }
                catch (Exception)
                {
                    enumeratorText = yAsUint.ToString();
                }

                coordinate = GetFormatForTime(actualX) + ", " + enumeratorText;
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

				using (Pen borderPen = new Pen(m_GridlineColor))
				{
					// Draw the box.
					graphics.DrawRectangle(borderPen, rect);
					// Use the colour appropriate to the active channel.
					Color rectColor = Color.FromArgb(TransparencyXYText, activeCh.ChannelColor);
					Brush rectBrush = new SolidBrush(rectColor);
					graphics.FillRectangle(rectBrush, rect);

					using (Brush textBrush = new SolidBrush(m_GraphAreaNormalColor))
					{
						// Write the string that is enclosed within the box.
						graphics.DrawString(coordinate, Font, textBrush, rect, sf);
					}
				}
			}
        }

        /// <summary>
        /// Draws the y axis value for the currently selected channel
        /// </summary>
        /// <param name="graphics">Reference to the GDI+ drawing surface.</param>
        protected override void DrawYAxisValues(Graphics graphics)
        {
            // Dont draw the Y axis values when plotting an enumerator watch variable. 
        }
        #endregion --- Methods ---

        #region --- Properties ---
        #region OldIdentifier
        /// <summary>
        /// Gets or sets the old identifier of the enumerator watch variable that is to be plotted. User to determine the text that is to be displayed corresponding to the 
        /// current value.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Design"),
        Description("The old identifier of the enumerator watch variable that is to be plotted. User to determine the text that is to be displayed corresponding to the current value.")
        ]
        public short OldIdentifier
        {
            get { return m_OldIdentifier; }
            set
            {
                m_OldIdentifier = value;
                try
                {
                    m_WatchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[OldIdentifier];
                }
                catch (Exception)
                {
                    m_WatchVariable = null;
                }
            }
        }
        #endregion OldIdentifier

        #region ClientForm
        /// <summary>
        /// Gets or sets the client form associated with the control.
        /// </summary>
        /// <remarks>The client form must inherit from the FormPTU class.</remarks>
        [
        Browsable(false),
        ]
        public Form ClientForm
        {
            get { return m_ClientAsFormPTU; }
            set
            {
                m_ClientAsFormPTU = value as FormPTU;
            }
        }
        #endregion ClientForm
        #endregion --- Properties ---
    }
}