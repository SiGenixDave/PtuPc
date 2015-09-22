#region --- Revision History ---
/*
 * 
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  AxisLine.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  09/23/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *	11/14/11	1.0.1	Sean.D			1.	Modified Draw to use "using" to ensure Pen disposal.
 */
#endregion --- Revision History ---

using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeProject.GraphComponents
{
	/// <summary>
	/// Draws the axis lines for the graph.
	/// </summary>
	public class AxisLine : IGraphElement
    {
        #region --- Private Member Variables ---
        /// <summary>
        /// Reference to the Graph class associated with the parent control.
        /// </summary>
        private Graph m_ParentGraph;
        #endregion --- Private Member Variables ---

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="parentGraph">The graph to which the axis line belongs.</param>
		public AxisLine(Graph parentGraph)
		{
			m_ParentGraph = parentGraph;
		}

		#region --- IGraphElement Members ---
        /// <summary>
        /// Draw the axis lines.
        /// </summary>
        /// <param name="graphics">Reference to the GDI+ drawing surface.</param>
		public void Draw(Graphics graphics)
		{
			graphics.SetClip(m_ParentGraph.ClientRectangle);

			// Y axis.
			using (Pen axisPen = new Pen(new SolidBrush(m_ParentGraph.YAxisColor), 2F))
			{
				// Set the cap style used at the end of the line.
				axisPen.EndCap = LineCap.ArrowAnchor;
				RectangleF graphArea = m_ParentGraph.GraphArea;
				graphics.DrawLine(axisPen, graphArea.Left, graphArea.Bottom, graphArea.Left, graphArea.Top);

				// X axis.
				axisPen.Color = m_ParentGraph.XAxisColor;
				axisPen.EndCap = LineCap.ArrowAnchor;
				graphics.DrawLine(axisPen, graphArea.Left, graphArea.Bottom, graphArea.Right, graphArea.Bottom);
			}
		}
        #endregion --- IGraphElement Members ---
    }
}
