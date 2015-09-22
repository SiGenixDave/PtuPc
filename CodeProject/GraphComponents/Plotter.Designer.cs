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
 *  Solution:   
 * 
 *  Project:    GraphComponents
 * 
 *  File name:  Plotter.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/30/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  10/06/10    1.1     K.McD           1.  Reformatted the layout in accordance with the iDesign coding standards. Specifically, comments now appear at the same level of 
 *                                              indentation as the code.
 * 
 *                                      2.  Modified the text associated with a number of XML tags.
 * 
 *                                      3.  Specified the lower limit parameter of the FindY() in method DrawXYText using a constant rather than a numeric value.
 *                                      
 *  10/01/11    1.2     K.McD           1.  Renamed a number of controls.
 *  
 *  10/10/11    1.3     K.McD           1.  De-registered the event handler for the MultiCursorMouseMove event.
 *                                      2.  Modified the tool tip text for a number of context menu options.
 *
 *	11/14/11	1.3.1	Sean.D			1.	Modified Dispose by moving the removal of the MultiCursorMouseMove event into the Windows Forms section and in its place, adding a call to clear the selected control list.
 *
 */
#endregion --- Revision History ---

using System;

namespace CodeProject.GraphComponents
{
    partial class Plotter
    {
        /// <summary> 
        /// Required Designer Variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region --- Component Designer Generated Code ---
        /// <summary> 
        /// Required method for Designer support - do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_HScrollBar = new System.Windows.Forms.HScrollBar();
            this.m_ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ToolStripMenuItemResetRange = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItemChangeRange = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItemZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItemCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripSeparatorCancel = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripMenuItemNextChannel = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItemPreviousChannel = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItemResetAxis = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_HScrollBar
            // 
            this.m_HScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_HScrollBar.Location = new System.Drawing.Point(0, 239);
            this.m_HScrollBar.Name = "m_HScrollBar";
            this.m_HScrollBar.Size = new System.Drawing.Size(344, 17);
            this.m_HScrollBar.TabIndex = 1;
            this.m_HScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.m_HScrollBar_Scroll);
            // 
            // m_ContextMenuStrip
            // 
            this.m_ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItemResetRange,
            this.m_ToolStripMenuItemChangeRange,
            this.m_ToolStripMenuItemZoom,
            this.m_ToolStripMenuItemCancel,
            this.m_ToolStripSeparatorCancel,
            this.m_ToolStripMenuItemNextChannel,
            this.m_ToolStripMenuItemPreviousChannel,
            this.m_ToolStripMenuItemResetAxis});
            this.m_ContextMenuStrip.Name = "m_ContextMenuStrip";
            this.m_ContextMenuStrip.Size = new System.Drawing.Size(172, 186);
            // 
            // m_ToolStripMenuItemResetRange
            // 
            this.m_ToolStripMenuItemResetRange.Name = "m_ToolStripMenuItemResetRange";
            this.m_ToolStripMenuItemResetRange.Size = new System.Drawing.Size(171, 22);
            this.m_ToolStripMenuItemResetRange.Text = "&Reset";
            this.m_ToolStripMenuItemResetRange.ToolTipText = "Reset the start and stop times.";
            this.m_ToolStripMenuItemResetRange.Click += new System.EventHandler(this.m_ToolStripMenuItemResetTimeSpan_Click);
            // 
            // m_ToolStripMenuItemChangeRange
            // 
            this.m_ToolStripMenuItemChangeRange.Name = "m_ToolStripMenuItemChangeRange";
            this.m_ToolStripMenuItemChangeRange.Size = new System.Drawing.Size(171, 22);
            this.m_ToolStripMenuItemChangeRange.Text = "Ne&w Range";
            this.m_ToolStripMenuItemChangeRange.ToolTipText = "Select new start and stop times.";
            this.m_ToolStripMenuItemChangeRange.Visible = false;
            this.m_ToolStripMenuItemChangeRange.Click += new System.EventHandler(this.m_ToolStripMenuItemNewTimeSpan_Click);
            // 
            // m_ToolStripMenuItemZoom
            // 
            this.m_ToolStripMenuItemZoom.Name = "m_ToolStripMenuItemZoom";
            this.m_ToolStripMenuItemZoom.Size = new System.Drawing.Size(171, 22);
            this.m_ToolStripMenuItemZoom.Text = "&Zoom";
            this.m_ToolStripMenuItemZoom.ToolTipText = "Zoom to the selected start and stop times.";
            this.m_ToolStripMenuItemZoom.Click += new System.EventHandler(this.m_ToolStripMenuItemZoom_Click);
            // 
            // m_ToolStripMenuItemCancel
            // 
            this.m_ToolStripMenuItemCancel.Name = "m_ToolStripMenuItemCancel";
            this.m_ToolStripMenuItemCancel.Size = new System.Drawing.Size(171, 22);
            this.m_ToolStripMenuItemCancel.Text = "&Cancel";
            this.m_ToolStripMenuItemCancel.ToolTipText = "Cancel the \'New Range\' operation.";
            this.m_ToolStripMenuItemCancel.Click += new System.EventHandler(this.m_ToolStripMenuItemCancel_Click);
            // 
            // m_ToolStripSeparatorCancel
            // 
            this.m_ToolStripSeparatorCancel.Name = "m_ToolStripSeparatorCancel";
            this.m_ToolStripSeparatorCancel.Size = new System.Drawing.Size(168, 6);
            this.m_ToolStripSeparatorCancel.Visible = false;
            // 
            // m_ToolStripMenuItemNextChannel
            // 
            this.m_ToolStripMenuItemNextChannel.Image = global::CodeProject.GraphComponents.Properties.Resources.MoveNext;
            this.m_ToolStripMenuItemNextChannel.Name = "m_ToolStripMenuItemNextChannel";
            this.m_ToolStripMenuItemNextChannel.Size = new System.Drawing.Size(171, 22);
            this.m_ToolStripMenuItemNextChannel.Text = "&Next Channel";
            this.m_ToolStripMenuItemNextChannel.Visible = false;
            this.m_ToolStripMenuItemNextChannel.Click += new System.EventHandler(this.m_ToolStripMenuItemNextChannel_Click);
            // 
            // m_ToolStripMenuItemPreviousChannel
            // 
            this.m_ToolStripMenuItemPreviousChannel.Image = global::CodeProject.GraphComponents.Properties.Resources.MovePrevious;
            this.m_ToolStripMenuItemPreviousChannel.Name = "m_ToolStripMenuItemPreviousChannel";
            this.m_ToolStripMenuItemPreviousChannel.Size = new System.Drawing.Size(171, 22);
            this.m_ToolStripMenuItemPreviousChannel.Text = "&Previous Channel";
            this.m_ToolStripMenuItemPreviousChannel.Visible = false;
            this.m_ToolStripMenuItemPreviousChannel.Click += new System.EventHandler(this.m_ToolStripMenuItemPreviousChannel_Click);
            // 
            // m_ToolStripMenuItemResetAxis
            // 
            this.m_ToolStripMenuItemResetAxis.Image = global::CodeProject.GraphComponents.Properties.Resources.Edit_Undo;
            this.m_ToolStripMenuItemResetAxis.Name = "m_ToolStripMenuItemResetAxis";
            this.m_ToolStripMenuItemResetAxis.Size = new System.Drawing.Size(171, 22);
            this.m_ToolStripMenuItemResetAxis.Text = "Reset &Y Axis Limits";
            this.m_ToolStripMenuItemResetAxis.Visible = false;
            this.m_ToolStripMenuItemResetAxis.Click += new System.EventHandler(this.m_ToolStripMenuItemResetAxis_Click);
            // 
            // Plotter
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ContextMenuStrip = this.m_ContextMenuStrip;
            this.Controls.Add(this.m_HScrollBar);
            this.DoubleBuffered = true;
            this.Name = "Plotter";
            this.Size = new System.Drawing.Size(344, 256);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Plotter_MouseDown);
            this.MouseLeave += new System.EventHandler(this.Plotter_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Plotter_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Plotter_MouseUp);
            this.m_ContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion --- Component Designer Generated Code ---
        /// <summary>
        /// Reference to the HScrollBar control.
        /// </summary>
        protected System.Windows.Forms.HScrollBar m_HScrollBar;

        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemResetRange;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemZoom;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemChangeRange;
        private System.Windows.Forms.ToolStripSeparator m_ToolStripSeparatorCancel;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemNextChannel;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemPreviousChannel;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemResetAxis;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemCancel;

        /// <summary>
        /// Object reference to the context menu associated with the plotter.
        /// </summary>
        public System.Windows.Forms.ContextMenuStrip m_ContextMenuStrip;
    }
}
