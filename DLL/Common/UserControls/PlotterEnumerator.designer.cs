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
 *  File name:  PlotterEnumerator.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  10/02/11    1.1     K.McD           1.  Added the context menu options associated with the plot layout.
 *  
 *  12/01/11    1.2     K.McD           1.  Attached the m_Plotter_MouseDown() event handler method to the MouseDown events associated with the 'Description' and 'Units' 
 *                                          Labels to ensure that the plotter is selected if the user clicks on either of those controls.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.UserControls
{
    partial class PlotterEnumerator
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region --- Disposal ---
        /// <summary>
        /// Destructor / Finalizer. Because Dispose() calls the GC.SuppressFinalize() method, this method is called by the garbage collection process only
        /// if the consumer of the object doesn't call the Dispose() method, as it should.
        /// </summary>
        ~PlotterEnumerator()
        {
            Dispose(false);
        }

        /// <summary>
        /// Public implementation of the IDisposable.Dispose method. Called by the consumer of the object in order to free unmanaged resources
        /// deterministically.
        /// </summary>
        public new void Dispose()
        {
            // Call the protected Dispose overload and pass a value of 'true' to indicate that the Dispose is being called by consumer code, not
            // by the garbage collector.
            Dispose(true);

            // Because the Dispose method performs all necessary cleanup, ensure the garbage collector does not call the class destructor.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources that are currently being used.
        /// </summary>
        /// <param name="disposing">True, if the managed resources should be disposed of; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            lock (this)
            {
                if (m_IsDisposed == false)
                {
                    Cleanup(disposing);

                    m_IsDisposed = true;
                    base.Dispose(disposing);
                }
            }
        }
        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_LabelDescription = new System.Windows.Forms.Label();
            this.m_LabelUnits = new System.Windows.Forms.Label();
            this.m_ButtonYAxisPlus = new System.Windows.Forms.Button();
            this.m_ButtonYAxisMinus = new System.Windows.Forms.Button();
            this.m_EnumPlotter = new Common.UserControls.PlotterEnumeratorParent();
            this.m_ContextMenuStripPlotLayout = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ToolStripSeparatorPlotLayoutBegin = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripMenuItemShowDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItemRemoveSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripSeparatorPlotLayoutEnd = new System.Windows.Forms.ToolStripSeparator();
            this.m_ContextMenuStripPlotLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_LabelDescription
            // 
            this.m_LabelDescription.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelDescription.Location = new System.Drawing.Point(48, 0);
            this.m_LabelDescription.Name = "m_LabelDescription";
            this.m_LabelDescription.Size = new System.Drawing.Size(500, 16);
            this.m_LabelDescription.TabIndex = 0;
            this.m_LabelDescription.Text = "Description";
            this.m_LabelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_LabelDescription.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_Plotter_MouseDown);
            // 
            // m_LabelUnits
            // 
            this.m_LabelUnits.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelUnits.Location = new System.Drawing.Point(800, 0);
            this.m_LabelUnits.Name = "m_LabelUnits";
            this.m_LabelUnits.Size = new System.Drawing.Size(31, 16);
            this.m_LabelUnits.TabIndex = 0;
            this.m_LabelUnits.Text = "Units";
            this.m_LabelUnits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_LabelUnits.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_Plotter_MouseDown);
            // 
            // m_ButtonYAxisPlus
            // 
            this.m_ButtonYAxisPlus.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonYAxisPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ButtonYAxisPlus.Image = global::Common.Properties.Resources.MoveDown;
            this.m_ButtonYAxisPlus.Location = new System.Drawing.Point(866, 117);
            this.m_ButtonYAxisPlus.Name = "m_ButtonYAxisPlus";
            this.m_ButtonYAxisPlus.Size = new System.Drawing.Size(19, 23);
            this.m_ButtonYAxisPlus.TabIndex = 0;
            this.m_ButtonYAxisPlus.TabStop = false;
            this.m_ButtonYAxisPlus.UseVisualStyleBackColor = false;
            this.m_ButtonYAxisPlus.Visible = false;
            // 
            // m_ButtonYAxisMinus
            // 
            this.m_ButtonYAxisMinus.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonYAxisMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ButtonYAxisMinus.Image = global::Common.Properties.Resources.MoveUp;
            this.m_ButtonYAxisMinus.Location = new System.Drawing.Point(866, 20);
            this.m_ButtonYAxisMinus.Name = "m_ButtonYAxisMinus";
            this.m_ButtonYAxisMinus.Size = new System.Drawing.Size(19, 23);
            this.m_ButtonYAxisMinus.TabIndex = 0;
            this.m_ButtonYAxisMinus.TabStop = false;
            this.m_ButtonYAxisMinus.UseVisualStyleBackColor = false;
            this.m_ButtonYAxisMinus.Visible = false;
            // 
            // m_EnumPlotter
            // 
            this.m_EnumPlotter.AnalogueType = CodeProject.GraphComponents.AnalogueType.TripleAnalogue;
            this.m_EnumPlotter.AutoScroll = true;
            this.m_EnumPlotter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_EnumPlotter.BackColor = System.Drawing.Color.Transparent;
            this.m_EnumPlotter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_EnumPlotter.ClientForm = null;
            this.m_EnumPlotter.CompressedMode = false;
            this.m_EnumPlotter.DataIntervalMs = ((long)(50));
            this.m_EnumPlotter.GraduationsX = 10;
            this.m_EnumPlotter.GraduationsY = 5;
            this.m_EnumPlotter.GraphArea = new System.Drawing.Rectangle(50, 20, 790, 120);
            this.m_EnumPlotter.GraphAreaColor = System.Drawing.Color.WhiteSmoke;
            this.m_EnumPlotter.GraphMarginBottom = 20;
            this.m_EnumPlotter.GraphMarginLeft = 50;
            this.m_EnumPlotter.GraphMarginRight = 20;
            this.m_EnumPlotter.GraphMarginTop = 20;
            this.m_EnumPlotter.GridlineColor = System.Drawing.Color.DarkGray;
            this.m_EnumPlotter.Gridlines = ((CodeProject.GraphComponents.GridStyles)((CodeProject.GraphComponents.GridStyles.Horizontal | CodeProject.GraphComponents.GridStyles.Vertical)));
            this.m_EnumPlotter.IsFaultLog = false;
            this.m_EnumPlotter.Location = new System.Drawing.Point(0, 0);
            this.m_EnumPlotter.MouseHoverCoordinates = new System.Drawing.Point(0, 0);
            this.m_EnumPlotter.Name = "m_EnumPlotter";
            this.m_EnumPlotter.OldIdentifier = ((short)(0));
            this.m_EnumPlotter.PlotIntervalMs = 50;
            this.m_EnumPlotter.Size = new System.Drawing.Size(860, 160);
            this.m_EnumPlotter.StartTime = new System.DateTime(((long)(0)));
            this.m_EnumPlotter.TabIndex = 0;
            this.m_EnumPlotter.TabStop = false;
            this.m_EnumPlotter.TimeDisplayStyle = CodeProject.GraphComponents.TimeAxisStyle.Absolute;
            this.m_EnumPlotter.TotalTimeElapsed = ((long)(0));
            this.m_EnumPlotter.Transparency = ((byte)(100));
            this.m_EnumPlotter.TripTime = new System.DateTime(2007, 5, 17, 14, 14, 47, 316);
            this.m_EnumPlotter.ValueFormat = "{0:F}";
            this.m_EnumPlotter.XAxisColor = System.Drawing.Color.Black;
            this.m_EnumPlotter.XRange = System.TimeSpan.Parse("00:00:09");
            this.m_EnumPlotter.YAxisColor = System.Drawing.Color.ForestGreen;
            this.m_EnumPlotter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_Plotter_MouseDown);
            // 
            // m_ContextMenuStripPlotLayout
            // 
            this.m_ContextMenuStripPlotLayout.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripSeparatorPlotLayoutBegin,
            this.m_ToolStripMenuItemShowDefinition,
            this.m_ToolStripMenuItemRemoveSelected,
            this.m_ToolStripSeparatorPlotLayoutEnd});
            this.m_ContextMenuStripPlotLayout.Name = "m_ContextMenuStripPlotLayout";
            this.m_ContextMenuStripPlotLayout.Size = new System.Drawing.Size(202, 82);
            // 
            // m_ToolStripSeparatorPlotLayoutBegin
            // 
            this.m_ToolStripSeparatorPlotLayoutBegin.Name = "m_ToolStripSeparatorPlotLayoutBegin";
            this.m_ToolStripSeparatorPlotLayoutBegin.Size = new System.Drawing.Size(198, 6);
            // 
            // m_ToolStripMenuItemShowDefinition
            // 
            this.m_ToolStripMenuItemShowDefinition.Image = global::Common.Properties.Resources.Help;
            this.m_ToolStripMenuItemShowDefinition.Name = "m_ToolStripMenuItemShowDefinition";
            this.m_ToolStripMenuItemShowDefinition.Size = new System.Drawing.Size(201, 22);
            this.m_ToolStripMenuItemShowDefinition.Text = "Show &Definition";
            this.m_ToolStripMenuItemShowDefinition.Click += new System.EventHandler(this.m_ToolStripMenuItemShowDefinition_Click);
            // 
            // m_ToolStripMenuItemRemoveSelected
            // 
            this.m_ToolStripMenuItemRemoveSelected.AutoToolTip = true;
            this.m_ToolStripMenuItemRemoveSelected.Image = global::Common.Properties.Resources.Delete;
            this.m_ToolStripMenuItemRemoveSelected.Name = "m_ToolStripMenuItemRemoveSelected";
            this.m_ToolStripMenuItemRemoveSelected.Size = new System.Drawing.Size(201, 22);
            this.m_ToolStripMenuItemRemoveSelected.Text = "&Remove Selected Plot(s)";
            this.m_ToolStripMenuItemRemoveSelected.ToolTipText = "Remove the selected plot(s) from the display.";
            this.m_ToolStripMenuItemRemoveSelected.Click += new System.EventHandler(this.m_ToolStripMenuItemRemoveSelected_Click);
            // 
            // m_ToolStripSeparatorPlotLayoutEnd
            // 
            this.m_ToolStripSeparatorPlotLayoutEnd.Name = "m_ToolStripSeparatorPlotLayoutEnd";
            this.m_ToolStripSeparatorPlotLayoutEnd.Size = new System.Drawing.Size(198, 6);
            // 
            // PlotterEnumerator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.m_ButtonYAxisPlus);
            this.Controls.Add(this.m_ButtonYAxisMinus);
            this.Controls.Add(this.m_LabelUnits);
            this.Controls.Add(this.m_LabelDescription);
            this.Controls.Add(this.m_EnumPlotter);
            this.DoubleBuffered = true;
            this.Name = "PlotterEnumerator";
            this.Size = new System.Drawing.Size(909, 160);
            this.m_ContextMenuStripPlotLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label m_LabelDescription;
        private System.Windows.Forms.Label m_LabelUnits;
        private Common.UserControls.PlotterEnumeratorParent m_EnumPlotter;
        private System.Windows.Forms.Button m_ButtonYAxisMinus;
        private System.Windows.Forms.Button m_ButtonYAxisPlus;
        private System.Windows.Forms.ContextMenuStrip m_ContextMenuStripPlotLayout;
        private System.Windows.Forms.ToolStripSeparator m_ToolStripSeparatorPlotLayoutBegin;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemShowDefinition;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemRemoveSelected;
        private System.Windows.Forms.ToolStripSeparator m_ToolStripSeparatorPlotLayoutEnd;
    }
}
