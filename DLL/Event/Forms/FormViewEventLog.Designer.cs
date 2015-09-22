#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Event
 * 
 *  File name:  FormViewEventLog.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author      Comments
 *  10/26/10    1.0     K.McD       1.  First entry into TortoiseSVN.
 *  
 *  08/02/13    1.1     K.McD       1.  Increased the width of the date field of the event log DataGridView control from 70 pixels to 75 pixels to
 *                                      allow a 4 digit year code to be displayed in Segoe UI 9 font.
 *                                      
 *  08/05/13    1.2     K.McD       1.  Increased the width of the event name field of the event log DataGridView control from 200 pixels to 250 pixels
 *                                      to allow the full event text to be displayed.
 *                                      
 *  03/22/15    1.3     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                          
 *                                              1.  MOC-0171-06. All references to fault logs, including menu options and directory 
 *                                                  names to be replaced by 'data streams' for all projects.
 *                                              
 *                                      Modifications
 *                                      1.  Changed the Text property of the 'Show Fault Log' context menu to "Show Data &Stream".
 *                                      
 *  04/21/15    1.4     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 4800010525-CU2/19.03.2015. 
 *                                      
 *                                          1.  NK-U-6505 Section 1.7.6. Data Sorting Capabilities. The proposal is to include an additional column in the spreadsheet
 *                                              that is used to view event logs (File/Open/Event Logs) that identifies the event log associated with the entry e.g.
 *                                              Maintenance, Engineering etc and allow the user to sort by this column. The format of the event log will obviously also
 *                                              have to be modified to include this information.
 *                                              
 *                                      2.  The height of the event variable user control and the DataGridView Row Height must be increased to allow characters such as
 *                                          'g', 'j', 'p', 'q', 'y' to be displayed correctly when using the default font.
 *                                              
 *                                      Modifications
 *                                      1.  Modified the AutoSizeRowsMode property of the DataGridView to 'DataGridViewAutoSizeRowsMode.AllCells'. - Ref.: 2.
 *                                      2.  The Width property of the 'Car Identifier' column of the DataGridView was reduced from 70 to 50 and the Width
 *                                          property of the 'Log' column of the DataGridView was set to 170. - Ref.: 1.1. This ensures that the width of the 
 *                                          Log column is sufficient to accommodate the log names of the event logs associated with the R188 project.
 *
 */
#endregion --- Revision History ---

namespace Event.Forms
{
    partial class FormViewEventLog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.m_ContextMenuStripDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_MenuItemShowDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemShowFaultLog = new System.Windows.Forms.ToolStripMenuItem();
            this.m_PanelEventVariableList = new System.Windows.Forms.Panel();
            this.m_PanelEventVariables = new System.Windows.Forms.Panel();
            this.m_PanelEventVariableHeader = new System.Windows.Forms.Panel();
            this.m_LegendEventVariables = new System.Windows.Forms.Label();
            this.m_PanelDataGridViewEventLog = new System.Windows.Forms.Panel();
            this.m_DataGridViewEventLog = new System.Windows.Forms.DataGridView();
            this.m_DataGridViewTextColumnEventIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnIdentifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnCarIdentifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnLog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnEventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextBoxColumnDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnDayOfWeek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnStreamAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewImageColumnStreamAvailable = new System.Windows.Forms.DataGridViewImageColumn();
            this.m_TabControl.SuspendLayout();
            this.m_TabPage1.SuspendLayout();
            this.m_ContextMenuStripDataGridView.SuspendLayout();
            this.m_PanelEventVariables.SuspendLayout();
            this.m_PanelEventVariableHeader.SuspendLayout();
            this.m_PanelDataGridViewEventLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewEventLog)).BeginInit();
            this.SuspendLayout();
            // 
            // m_TabControl
            // 
            this.m_TabControl.Location = new System.Drawing.Point(0, 90);
            this.m_TabControl.Size = new System.Drawing.Size(1200, 610);
            // 
            // m_PanelInformation
            // 
            this.m_PanelInformation.Location = new System.Drawing.Point(0, 57);
            // 
            // m_TabPage1
            // 
            this.m_TabPage1.Controls.Add(this.m_PanelEventVariables);
            this.m_TabPage1.Controls.Add(this.m_PanelDataGridViewEventLog);
            this.m_TabPage1.Padding = new System.Windows.Forms.Padding(12);
            // 
            // m_ContextMenuStripDataGridView
            // 
            this.m_ContextMenuStripDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemShowDefinition,
            this.m_MenuItemShowFaultLog});
            this.m_ContextMenuStripDataGridView.Name = "m_ContextMenuStripDataGridView";
            this.m_ContextMenuStripDataGridView.Size = new System.Drawing.Size(171, 48);
            // 
            // m_MenuItemShowDefinition
            // 
            this.m_MenuItemShowDefinition.Image = global::Event.Properties.Resources.Help;
            this.m_MenuItemShowDefinition.Name = "m_MenuItemShowDefinition";
            this.m_MenuItemShowDefinition.Size = new System.Drawing.Size(170, 22);
            this.m_MenuItemShowDefinition.Text = "Show &Definition";
            this.m_MenuItemShowDefinition.Click += new System.EventHandler(this.m_MenuItemShowDefinition_Click);
            // 
            // m_MenuItemShowFaultLog
            // 
            this.m_MenuItemShowFaultLog.Image = global::Event.Properties.Resources.DataStream;
            this.m_MenuItemShowFaultLog.Name = "m_MenuItemShowFaultLog";
            this.m_MenuItemShowFaultLog.Size = new System.Drawing.Size(170, 22);
            this.m_MenuItemShowFaultLog.Text = "Show Data &Stream";
            this.m_MenuItemShowFaultLog.Click += new System.EventHandler(this.m_MenuItemShowFaultLog_Click);
            // 
            // m_PanelEventVariableList
            // 
            this.m_PanelEventVariableList.AutoSize = true;
            this.m_PanelEventVariableList.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelEventVariableList.Location = new System.Drawing.Point(0, 31);
            this.m_PanelEventVariableList.Name = "m_PanelEventVariableList";
            this.m_PanelEventVariableList.Size = new System.Drawing.Size(376, 0);
            this.m_PanelEventVariableList.TabIndex = 0;
            // 
            // m_PanelEventVariables
            // 
            this.m_PanelEventVariables.AutoScroll = true;
            this.m_PanelEventVariables.Controls.Add(this.m_PanelEventVariableList);
            this.m_PanelEventVariables.Controls.Add(this.m_PanelEventVariableHeader);
            this.m_PanelEventVariables.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_PanelEventVariables.Location = new System.Drawing.Point(512, 12);
            this.m_PanelEventVariables.Name = "m_PanelEventVariables";
            this.m_PanelEventVariables.Size = new System.Drawing.Size(376, 560);
            this.m_PanelEventVariables.TabIndex = 0;
            this.m_PanelEventVariables.Visible = false;
            // 
            // m_PanelEventVariableHeader
            // 
            this.m_PanelEventVariableHeader.AutoSize = true;
            this.m_PanelEventVariableHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_PanelEventVariableHeader.BackColor = System.Drawing.Color.Transparent;
            this.m_PanelEventVariableHeader.Controls.Add(this.m_LegendEventVariables);
            this.m_PanelEventVariableHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelEventVariableHeader.Location = new System.Drawing.Point(0, 0);
            this.m_PanelEventVariableHeader.Name = "m_PanelEventVariableHeader";
            this.m_PanelEventVariableHeader.Padding = new System.Windows.Forms.Padding(3);
            this.m_PanelEventVariableHeader.Size = new System.Drawing.Size(376, 31);
            this.m_PanelEventVariableHeader.TabIndex = 0;
            // 
            // m_LegendEventVariables
            // 
            this.m_LegendEventVariables.BackColor = System.Drawing.Color.LightSteelBlue;
            this.m_LegendEventVariables.Location = new System.Drawing.Point(13, 0);
            this.m_LegendEventVariables.Margin = new System.Windows.Forms.Padding(0);
            this.m_LegendEventVariables.Name = "m_LegendEventVariables";
            this.m_LegendEventVariables.Size = new System.Drawing.Size(200, 28);
            this.m_LegendEventVariables.TabIndex = 0;
            this.m_LegendEventVariables.Text = "Event Variable Values";
            this.m_LegendEventVariables.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_PanelDataGridViewEventLog
            // 
            this.m_PanelDataGridViewEventLog.AutoScroll = true;
            this.m_PanelDataGridViewEventLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_PanelDataGridViewEventLog.Controls.Add(this.m_DataGridViewEventLog);
            this.m_PanelDataGridViewEventLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_PanelDataGridViewEventLog.Location = new System.Drawing.Point(12, 12);
            this.m_PanelDataGridViewEventLog.Name = "m_PanelDataGridViewEventLog";
            this.m_PanelDataGridViewEventLog.Size = new System.Drawing.Size(500, 560);
            this.m_PanelDataGridViewEventLog.TabIndex = 0;
            // 
            // m_DataGridViewEventLog
            // 
            this.m_DataGridViewEventLog.AllowUserToAddRows = false;
            this.m_DataGridViewEventLog.AllowUserToDeleteRows = false;
            this.m_DataGridViewEventLog.AllowUserToResizeColumns = false;
            this.m_DataGridViewEventLog.AllowUserToResizeRows = false;
            this.m_DataGridViewEventLog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.m_DataGridViewEventLog.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_DataGridViewEventLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_DataGridViewEventLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.m_DataGridViewEventLog.ColumnHeadersHeight = 28;
            this.m_DataGridViewEventLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.m_DataGridViewEventLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.m_DataGridViewTextColumnEventIndex,
            this.m_DataGridViewTextColumnIdentifier,
            this.m_DataGridViewTextColumnCarIdentifier,
            this.m_DataGridViewTextColumnLog,
            this.m_DataGridViewTextColumnEventName,
            this.m_DataGridViewTextBoxColumnDateTime,
            this.m_DataGridViewTextColumnDate,
            this.m_DataGridViewTextColumnTime,
            this.m_DataGridViewTextColumnDayOfWeek,
            this.m_DataGridViewTextColumnStreamAvailable,
            this.m_DataGridViewImageColumnStreamAvailable});
            this.m_DataGridViewEventLog.ContextMenuStrip = this.m_ContextMenuStripDataGridView;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewEventLog.DefaultCellStyle = dataGridViewCellStyle4;
            this.m_DataGridViewEventLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DataGridViewEventLog.Location = new System.Drawing.Point(0, 0);
            this.m_DataGridViewEventLog.MultiSelect = false;
            this.m_DataGridViewEventLog.Name = "m_DataGridViewEventLog";
            this.m_DataGridViewEventLog.ReadOnly = true;
            this.m_DataGridViewEventLog.RowHeadersVisible = false;
            this.m_DataGridViewEventLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_DataGridViewEventLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_DataGridViewEventLog.ShowCellToolTips = false;
            this.m_DataGridViewEventLog.Size = new System.Drawing.Size(498, 558);
            this.m_DataGridViewEventLog.StandardTab = true;
            this.m_DataGridViewEventLog.TabIndex = 1;
            this.m_DataGridViewEventLog.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_DataGridViewEventLog_CellContentDoubleClick);
            this.m_DataGridViewEventLog.SelectionChanged += new System.EventHandler(this.m_DataGridViewEventLog_SelectionChanged);
            this.m_DataGridViewEventLog.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.m_DataGridViewEventLog_SortCompare);
            this.m_DataGridViewEventLog.Sorted += new System.EventHandler(this.m_DataGridViewEventLog_Sorted);
            // 
            // m_DataGridViewTextColumnEventIndex
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_DataGridViewTextColumnEventIndex.DefaultCellStyle = dataGridViewCellStyle2;
            this.m_DataGridViewTextColumnEventIndex.HeaderText = "Index";
            this.m_DataGridViewTextColumnEventIndex.Name = "m_DataGridViewTextColumnEventIndex";
            this.m_DataGridViewTextColumnEventIndex.ReadOnly = true;
            this.m_DataGridViewTextColumnEventIndex.Visible = false;
            this.m_DataGridViewTextColumnEventIndex.Width = 60;
            // 
            // m_DataGridViewTextColumnIdentifier
            // 
            this.m_DataGridViewTextColumnIdentifier.HeaderText = "Identifier";
            this.m_DataGridViewTextColumnIdentifier.Name = "m_DataGridViewTextColumnIdentifier";
            this.m_DataGridViewTextColumnIdentifier.ReadOnly = true;
            this.m_DataGridViewTextColumnIdentifier.Visible = false;
            this.m_DataGridViewTextColumnIdentifier.Width = 60;
            // 
            // m_DataGridViewTextColumnCarIdentifier
            // 
            this.m_DataGridViewTextColumnCarIdentifier.HeaderText = "[Car ID]";
            this.m_DataGridViewTextColumnCarIdentifier.Name = "m_DataGridViewTextColumnCarIdentifier";
            this.m_DataGridViewTextColumnCarIdentifier.ReadOnly = true;
            this.m_DataGridViewTextColumnCarIdentifier.Visible = false;
            this.m_DataGridViewTextColumnCarIdentifier.Width = 60;
            // 
            // m_DataGridViewTextColumnLog
            // 
            this.m_DataGridViewTextColumnLog.HeaderText = "[Log]";
            this.m_DataGridViewTextColumnLog.Name = "m_DataGridViewTextColumnLog";
            this.m_DataGridViewTextColumnLog.ReadOnly = true;
            this.m_DataGridViewTextColumnLog.Visible = false;
            this.m_DataGridViewTextColumnLog.Width = 180;
            // 
            // m_DataGridViewTextColumnEventName
            // 
            this.m_DataGridViewTextColumnEventName.HeaderText = "[Event Name]";
            this.m_DataGridViewTextColumnEventName.Name = "m_DataGridViewTextColumnEventName";
            this.m_DataGridViewTextColumnEventName.ReadOnly = true;
            this.m_DataGridViewTextColumnEventName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.m_DataGridViewTextColumnEventName.Width = 270;
            // 
            // m_DataGridViewTextBoxColumnDateTime
            // 
            this.m_DataGridViewTextBoxColumnDateTime.HeaderText = "DateTime";
            this.m_DataGridViewTextBoxColumnDateTime.Name = "m_DataGridViewTextBoxColumnDateTime";
            this.m_DataGridViewTextBoxColumnDateTime.ReadOnly = true;
            this.m_DataGridViewTextBoxColumnDateTime.Visible = false;
            // 
            // m_DataGridViewTextColumnDate
            // 
            this.m_DataGridViewTextColumnDate.HeaderText = "[Date]";
            this.m_DataGridViewTextColumnDate.Name = "m_DataGridViewTextColumnDate";
            this.m_DataGridViewTextColumnDate.ReadOnly = true;
            this.m_DataGridViewTextColumnDate.Width = 75;
            // 
            // m_DataGridViewTextColumnTime
            // 
            this.m_DataGridViewTextColumnTime.HeaderText = "Time";
            this.m_DataGridViewTextColumnTime.Name = "m_DataGridViewTextColumnTime";
            this.m_DataGridViewTextColumnTime.ReadOnly = true;
            this.m_DataGridViewTextColumnTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewTextColumnTime.Width = 65;
            // 
            // m_DataGridViewTextColumnDayOfWeek
            // 
            this.m_DataGridViewTextColumnDayOfWeek.HeaderText = "Day";
            this.m_DataGridViewTextColumnDayOfWeek.Name = "m_DataGridViewTextColumnDayOfWeek";
            this.m_DataGridViewTextColumnDayOfWeek.ReadOnly = true;
            this.m_DataGridViewTextColumnDayOfWeek.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewTextColumnDayOfWeek.Width = 40;
            // 
            // m_DataGridViewTextColumnStreamAvailable
            // 
            this.m_DataGridViewTextColumnStreamAvailable.HeaderText = "Stream Available";
            this.m_DataGridViewTextColumnStreamAvailable.Name = "m_DataGridViewTextColumnStreamAvailable";
            this.m_DataGridViewTextColumnStreamAvailable.ReadOnly = true;
            this.m_DataGridViewTextColumnStreamAvailable.Visible = false;
            this.m_DataGridViewTextColumnStreamAvailable.Width = 40;
            // 
            // m_DataGridViewImageColumnStreamAvailable
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.NullValue = null;
            this.m_DataGridViewImageColumnStreamAvailable.DefaultCellStyle = dataGridViewCellStyle3;
            this.m_DataGridViewImageColumnStreamAvailable.HeaderText = "Stream";
            this.m_DataGridViewImageColumnStreamAvailable.Name = "m_DataGridViewImageColumnStreamAvailable";
            this.m_DataGridViewImageColumnStreamAvailable.ReadOnly = true;
            this.m_DataGridViewImageColumnStreamAvailable.ToolTipText = "A graphic indicates that a data stream is available.";
            this.m_DataGridViewImageColumnStreamAvailable.Width = 80;
            // 
            // FormViewEventLog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormViewEventLog";
            this.Text = "Diagnostics/Event Log";
            this.Shown += new System.EventHandler(this.FormViewEventLog_Shown);
            this.Controls.SetChildIndex(this.m_PanelInformation, 0);
            this.Controls.SetChildIndex(this.m_TabControl, 0);
            this.m_TabControl.ResumeLayout(false);
            this.m_TabPage1.ResumeLayout(false);
            this.m_ContextMenuStripDataGridView.ResumeLayout(false);
            this.m_PanelEventVariables.ResumeLayout(false);
            this.m_PanelEventVariables.PerformLayout();
            this.m_PanelEventVariableHeader.ResumeLayout(false);
            this.m_PanelDataGridViewEventLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewEventLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        /// <summary>
        /// Reference to the <c>DataGridView</c> control.
        /// </summary>
        protected System.Windows.Forms.DataGridView m_DataGridViewEventLog;

        /// <summary>
        /// Reference to the <c>Panel</c> control containing the <c>DataGridView</c> control.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelDataGridViewEventLog;

        /// <summary>
        /// Reference to the <c>Panel</c> associated with the event variables.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelEventVariables;

        /// <summary>
        /// Reference to the <c>Panel</c> associated with the event variable list.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelEventVariableList;

        private System.Windows.Forms.ContextMenuStrip m_ContextMenuStripDataGridView;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemShowDefinition;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemShowFaultLog;
        private System.Windows.Forms.Panel m_PanelEventVariableHeader;
        private System.Windows.Forms.Label m_LegendEventVariables;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnEventIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnIdentifier;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnCarIdentifier;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnEventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextBoxColumnDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnDayOfWeek;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnStreamAvailable;
        private System.Windows.Forms.DataGridViewImageColumn m_DataGridViewImageColumnStreamAvailable;
    }
}