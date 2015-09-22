#region --- Revision History ---
/*
 * 
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  Gridline.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  09/23/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *	11/14/11	1.0.1	Sean.D			1.	Modified Draw to use "using" to ensure Pen and Brush disposal.
 */
#endregion --- Revision History ---

using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeProject.GraphComponents
{
	/// <summary>
	/// Draws the grid lines for the graph.
	/// </summary>
	public class Gridline : IGraphElement
    {
        #region --- Member Variables ---
        /// <summary>
        /// Reference to the Graph class associated with the parent control.
        /// </summary>
        Graph m_ParentGraph;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="parentGraph">The graph associated with the grid lines.</param>
		public Gridline(Graph parentGraph)
		{
			this.m_ParentGraph = parentGraph;
        }
        #endregion --- Constructors ---

        #region --- IGraphElement Members ---
        /// <summary>
        /// Draw the gridlines.
        /// </summary>
        /// <param name="graphics">Reference to the GDI+ drawing surface.</param>
		public void Draw(System.Drawing.Graphics graphics)
		{
			using (Pen graphAreaPen = new Pen(m_ParentGraph.GridlineColor))
			{
				graphAreaPen.DashStyle = DashStyle.Dash;
				using (Brush graphAreaBrush = new SolidBrush(m_ParentGraph.GraphAreaColor))
				{
					graphics.FillRectangle(graphAreaBrush, m_ParentGraph.GraphArea);
					graphics.DrawRectangle(graphAreaPen, m_ParentGraph.GraphArea);

					if ((m_ParentGraph.Gridlines & GridStyles.Horizontal) == GridStyles.Horizontal)
					{
						graphics.SetClip(m_ParentGraph.GraphArea);

						int gridSize = m_ParentGraph.GraphArea.Height / m_ParentGraph.GraduationsY;
						for (int i = 0; i < m_ParentGraph.GraphArea.Height; i += gridSize)
						{
							graphics.DrawLine(graphAreaPen, m_ParentGraph.GraphArea.Left, m_ParentGraph.GraphArea.Top + i, m_ParentGraph.GraphArea.Right, m_ParentGraph.GraphArea.Top + i);
						}
					}

					if ((m_ParentGraph.Gridlines & GridStyles.Vertical) == GridStyles.Vertical)
					{
						graphics.SetClip(m_ParentGraph.GraphArea);

						int gridSize = m_ParentGraph.GraphArea.Width / m_ParentGraph.GraduationsX;
						for (int i = 0; i < m_ParentGraph.GraphArea.Width; i += gridSize)
						{
							graphics.DrawLine(graphAreaPen, m_ParentGraph.GraphArea.Left + i, m_ParentGraph.GraphArea.Bottom, m_ParentGraph.GraphArea.Left + i, m_ParentGraph.GraphArea.Top);
						}
					}
				}
			}
        }
        #endregion --- IGraphElement Members ---
    }
}
