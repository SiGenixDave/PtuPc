#region --- Revision History ---
/*
 * 
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  Cursor.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  09/23/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System.Drawing;

namespace CodeProject.GraphComponents
{
	/// <summary>
	/// Summary description for Cursor.
	/// </summary>
	public class Cursor : IGraphElement
    {
        #region --- Member Variables ---
        /// <summary>
        /// Reference to the graph area.
        /// </summary>
        private Rectangle m_GraphArea;

        /// <summary>
        /// Reference to the channel.
        /// </summary>
		private Channel m_Channel;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="channel">Channel reference.</param>
		public Cursor(Channel channel)
		{						
			m_Channel = channel;
        }
        #endregion --- Constructors ---

        #region --- IGraphElement Members ---
        /// <summary>
        /// Draw the cursor.
        /// </summary>
        /// <param name="graphics">The graphics area.</param>
		public void Draw(Graphics graphics)
		{
			float range = m_Channel.MaximumValue - m_Channel.MinimumValue;
			int offsetInPixel = (int) ((float) (m_GraphArea.Height / range) * m_Channel.CursorOffset);

            // Must ensure that GraphArea is defined.
            int halfGraphHeight = m_GraphArea.Height / 2;
			Point leftPoint = new Point ();
			leftPoint.X = m_GraphArea.Left;
			leftPoint.Y = m_GraphArea.Bottom - halfGraphHeight + offsetInPixel;
			
			Point rightPoint = new Point (m_GraphArea.Right, leftPoint.Y);

			graphics.DrawLine (new Pen(m_Channel.ChannelColor), leftPoint, rightPoint);
        }
        #endregion  --- IGraphElement Members ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the graph area associated with the cursor.
        /// </summary>
        public Rectangle GraphArea
        {
            get { return m_GraphArea; }
            set { m_GraphArea = value; }
        }
        #endregion --- Properties ---
    }
}
