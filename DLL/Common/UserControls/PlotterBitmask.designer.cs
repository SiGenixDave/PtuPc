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
 *  File name:  PlotterBitmask.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  10/02/11    1.1     K.McD           1.  Added the context menu options associated with the plot layout.
 *  
 *  12/01/11    1.2     K.McD           1.  Attached the m_Plotter_MouseDown() event handler method to the MouseDown event associated with the 'Description' Label to 
 *                                          ensure that the plotter is selected if the user clicks on the Description Label.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.UserControls
{
    partial class PlotterBitmask
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
        ~PlotterBitmask()
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
            this.m_LabelBit = new System.Windows.Forms.Label();
            this.m_LogicAnalyzer = new CodeProject.GraphComponents.LogicAnalyzer();
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
            this.m_LabelDescription.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_LabelDescription.Location = new System.Drawing.Point(47, 0);
            this.m_LabelDescription.Name = "m_LabelDescription";
            this.m_LabelDescription.Size = new System.Drawing.Size(500, 16);
            this.m_LabelDescription.TabIndex = 0;
            this.m_LabelDescription.Text = "Description";
            this.m_LabelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_LabelDescription.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_Plotter_MouseDown);
            // 
            // m_LabelBit
            // 
            this.m_LabelBit.Location = new System.Drawing.Point(0, 28);
            this.m_LabelBit.Name = "m_LabelBit";
            this.m_LabelBit.Size = new System.Drawing.Size(25, 16);
            this.m_LabelBit.TabIndex = 9;
            this.m_LabelBit.Text = "18";
            this.m_LabelBit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LogicAnalyzer
            // 
            this.m_LogicAnalyzer.AlarmState = true;
            this.m_LogicAnalyzer.AnalogueType = CodeProject.GraphComponents.AnalogueType.TripleAnalogue;
            this.m_LogicAnalyzer.BackColor = System.Drawing.Color.Transparent;
            this.m_LogicAnalyzer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_LogicAnalyzer.CompressedMode = false;
            this.m_LogicAnalyzer.DataIntervalMs = ((long)(50));
            this.m_LogicAnalyzer.GraduationsX = 10;
            this.m_LogicAnalyzer.GraduationsY = 1;
            this.m_LogicAnalyzer.GraphArea = new System.Drawing.Rectangle(50, 20, 790, 35);
            this.m_LogicAnalyzer.GraphAreaColor = System.Drawing.Color.WhiteSmoke;
            this.m_LogicAnalyzer.GraphMarginBottom = 20;
            this.m_LogicAnalyzer.GraphMarginLeft = 50;
            this.m_LogicAnalyzer.GraphMarginRight = 20;
            this.m_LogicAnalyzer.GraphMarginTop = 20;
            this.m_LogicAnalyzer.GridlineColor = System.Drawing.Color.DarkGray;
            this.m_LogicAnalyzer.Gridlines = ((CodeProject.GraphComponents.GridStyles)((CodeProject.GraphComponents.GridStyles.Horizontal | CodeProject.GraphComponents.GridStyles.Vertical)));
            this.m_LogicAnalyzer.IsFaultLog = false;
            this.m_LogicAnalyzer.Location = new System.Drawing.Point(0, 0);
            this.m_LogicAnalyzer.MouseHoverCoordinates = new System.Drawing.Point(0, 0);
            this.m_LogicAnalyzer.Name = "m_LogicAnalyzer";
            this.m_LogicAnalyzer.PlotIntervalMs = 50;
            this.m_LogicAnalyzer.Size = new System.Drawing.Size(860, 75);
            this.m_LogicAnalyzer.StartTime = new System.DateTime(((long)(0)));
            this.m_LogicAnalyzer.TabIndex = 0;
            this.m_LogicAnalyzer.TimeDisplayStyle = CodeProject.GraphComponents.TimeAxisStyle.Absolute;
            this.m_LogicAnalyzer.TotalTimeElapsed = ((long)(0));
            this.m_LogicAnalyzer.Transparency = ((byte)(100));
            this.m_LogicAnalyzer.TripTime = new System.DateTime(2008, 3, 13, 19, 15, 3, 639);
            this.m_LogicAnalyzer.ValueFormat = "{0:F}";
            this.m_LogicAnalyzer.XAxisColor = System.Drawing.Color.Black;
            this.m_LogicAnalyzer.XRange = System.TimeSpan.Parse("00:00:09");
            this.m_LogicAnalyzer.YAxisColor = System.Drawing.Color.Black;
            this.m_LogicAnalyzer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_Plotter_MouseDown);
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
            // PlotterBitmask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.m_LabelBit);
            this.Controls.Add(this.m_LabelDescription);
            this.Controls.Add(this.m_LogicAnalyzer);
            this.Name = "PlotterBitmask";
            this.Size = new System.Drawing.Size(909, 75);
            this.m_ContextMenuStripPlotLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_LabelDescription;
        private System.Windows.Forms.Label m_LabelBit;
        private CodeProject.GraphComponents.LogicAnalyzer m_LogicAnalyzer;
        private System.Windows.Forms.ContextMenuStrip m_ContextMenuStripPlotLayout;
        private System.Windows.Forms.ToolStripSeparator m_ToolStripSeparatorPlotLayoutBegin;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemShowDefinition;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemRemoveSelected;
        private System.Windows.Forms.ToolStripSeparator m_ToolStripSeparatorPlotLayoutEnd;
    }
}
