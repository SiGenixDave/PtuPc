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
 *  Project:    Event
 * 
 *  File name:  FormConfigureEventFlags.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  03/31/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  10/26/11    1.1     K.McD           1.  Asserted the 'StandardTab' property of the DataGridViewControl to ensure normal Tab operation. 
 *
 */
#endregion --- Revision History ---

namespace Event.Forms
{
    partial class FormConfigureEventFlags
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
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_PanelDataGridViewEventStatus = new System.Windows.Forms.Panel();
            this.m_DataGridViewEventStatus = new System.Windows.Forms.DataGridView();
            this.m_DataGridViewTextColumnEventIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnEventDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnEnableEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnStreamTriggered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnCumulativeHistory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTextColumnRecentHistory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_ContextMenuStripFlags = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_MenuItemShowDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemStreamTriggered = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_PanelOuter.SuspendLayout();
            this.m_PanelDataGridViewEventStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewEventStatus)).BeginInit();
            this.m_ContextMenuStripFlags.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_PanelOuter.Controls.Add(this.m_PanelDataGridViewEventStatus);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_PanelOuter.Size = new System.Drawing.Size(554, 496);
            this.m_PanelOuter.TabIndex = 1;
            this.m_PanelOuter.TabStop = true;
            // 
            // m_PanelDataGridViewEventStatus
            // 
            this.m_PanelDataGridViewEventStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_PanelDataGridViewEventStatus.Controls.Add(this.m_DataGridViewEventStatus);
            this.m_PanelDataGridViewEventStatus.Location = new System.Drawing.Point(12, 12);
            this.m_PanelDataGridViewEventStatus.Name = "m_PanelDataGridViewEventStatus";
            this.m_PanelDataGridViewEventStatus.Size = new System.Drawing.Size(529, 469);
            this.m_PanelDataGridViewEventStatus.TabIndex = 1;
            // 
            // m_DataGridViewEventStatus
            // 
            this.m_DataGridViewEventStatus.AllowUserToAddRows = false;
            this.m_DataGridViewEventStatus.AllowUserToDeleteRows = false;
            this.m_DataGridViewEventStatus.AllowUserToResizeColumns = false;
            this.m_DataGridViewEventStatus.AllowUserToResizeRows = false;
            this.m_DataGridViewEventStatus.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_DataGridViewEventStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_DataGridViewEventStatus.ColumnHeadersHeight = 36;
            this.m_DataGridViewEventStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.m_DataGridViewEventStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.m_DataGridViewTextColumnEventIndex,
            this.m_DataGridViewTextColumnEventDescription,
            this.m_DataGridViewTextColumnEnableEvent,
            this.m_DataGridViewTextColumnStreamTriggered,
            this.m_DataGridViewTextColumnCumulativeHistory,
            this.m_DataGridViewTextColumnRecentHistory});
            this.m_DataGridViewEventStatus.ContextMenuStrip = this.m_ContextMenuStripFlags;
            this.m_DataGridViewEventStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DataGridViewEventStatus.Location = new System.Drawing.Point(0, 0);
            this.m_DataGridViewEventStatus.MultiSelect = false;
            this.m_DataGridViewEventStatus.Name = "m_DataGridViewEventStatus";
            this.m_DataGridViewEventStatus.ReadOnly = true;
            this.m_DataGridViewEventStatus.RowHeadersVisible = false;
            this.m_DataGridViewEventStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_DataGridViewEventStatus.Size = new System.Drawing.Size(527, 467);
            this.m_DataGridViewEventStatus.StandardTab = true;
            this.m_DataGridViewEventStatus.TabIndex = 1;
            this.m_DataGridViewEventStatus.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_DataGridViewFlags_CellClick);
            // 
            // m_DataGridViewTextColumnEventIndex
            // 
            this.m_DataGridViewTextColumnEventIndex.HeaderText = "Event Index";
            this.m_DataGridViewTextColumnEventIndex.Name = "m_DataGridViewTextColumnEventIndex";
            this.m_DataGridViewTextColumnEventIndex.ReadOnly = true;
            this.m_DataGridViewTextColumnEventIndex.Visible = false;
            // 
            // m_DataGridViewTextColumnEventDescription
            // 
            this.m_DataGridViewTextColumnEventDescription.HeaderText = "Event Name";
            this.m_DataGridViewTextColumnEventDescription.Name = "m_DataGridViewTextColumnEventDescription";
            this.m_DataGridViewTextColumnEventDescription.ReadOnly = true;
            this.m_DataGridViewTextColumnEventDescription.Width = 280;
            // 
            // m_DataGridViewTextColumnEnableEvent
            // 
            this.m_DataGridViewTextColumnEnableEvent.HeaderText = "Enable Event";
            this.m_DataGridViewTextColumnEnableEvent.Name = "m_DataGridViewTextColumnEnableEvent";
            this.m_DataGridViewTextColumnEnableEvent.ReadOnly = true;
            this.m_DataGridViewTextColumnEnableEvent.Width = 80;
            // 
            // m_DataGridViewTextColumnStreamTriggered
            // 
            this.m_DataGridViewTextColumnStreamTriggered.HeaderText = "Stream Triggered";
            this.m_DataGridViewTextColumnStreamTriggered.Name = "m_DataGridViewTextColumnStreamTriggered";
            this.m_DataGridViewTextColumnStreamTriggered.ReadOnly = true;
            this.m_DataGridViewTextColumnStreamTriggered.Width = 80;
            // 
            // m_DataGridViewTextColumnCumulativeHistory
            // 
            this.m_DataGridViewTextColumnCumulativeHistory.HeaderText = "Cumulative History";
            this.m_DataGridViewTextColumnCumulativeHistory.Name = "m_DataGridViewTextColumnCumulativeHistory";
            this.m_DataGridViewTextColumnCumulativeHistory.ReadOnly = true;
            this.m_DataGridViewTextColumnCumulativeHistory.Visible = false;
            this.m_DataGridViewTextColumnCumulativeHistory.Width = 80;
            // 
            // m_DataGridViewTextColumnRecentHistory
            // 
            this.m_DataGridViewTextColumnRecentHistory.HeaderText = "Recent History";
            this.m_DataGridViewTextColumnRecentHistory.Name = "m_DataGridViewTextColumnRecentHistory";
            this.m_DataGridViewTextColumnRecentHistory.ReadOnly = true;
            this.m_DataGridViewTextColumnRecentHistory.Visible = false;
            this.m_DataGridViewTextColumnRecentHistory.Width = 80;
            // 
            // m_ContextMenuStripFlags
            // 
            this.m_ContextMenuStripFlags.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemShowDefinition,
            this.m_MenuItemEnabled,
            this.m_MenuItemStreamTriggered});
            this.m_ContextMenuStripFlags.Name = "m_ContextMenuStripFlags";
            this.m_ContextMenuStripFlags.Size = new System.Drawing.Size(166, 70);
            this.m_ContextMenuStripFlags.Opened += new System.EventHandler(this.m_ContextMenuStripFlags_Opened);
            // 
            // m_MenuItemShowDefinition
            // 
            this.m_MenuItemShowDefinition.Image = global::Event.Properties.Resources.Help;
            this.m_MenuItemShowDefinition.Name = "m_MenuItemShowDefinition";
            this.m_MenuItemShowDefinition.Size = new System.Drawing.Size(165, 22);
            this.m_MenuItemShowDefinition.Text = "Show &Definition";
            this.m_MenuItemShowDefinition.Click += new System.EventHandler(this.m_MenuItemShowDefinition_Click);
            // 
            // m_MenuItemEnabled
            // 
            this.m_MenuItemEnabled.Name = "m_MenuItemEnabled";
            this.m_MenuItemEnabled.Size = new System.Drawing.Size(165, 22);
            this.m_MenuItemEnabled.Text = "&Enabled";
            this.m_MenuItemEnabled.Click += new System.EventHandler(this.m_MenuItemEnabled_Click);
            // 
            // m_MenuItemStreamTriggered
            // 
            this.m_MenuItemStreamTriggered.Name = "m_MenuItemStreamTriggered";
            this.m_MenuItemStreamTriggered.Size = new System.Drawing.Size(165, 22);
            this.m_MenuItemStreamTriggered.Text = "&Stream Triggered";
            this.m_MenuItemStreamTriggered.Click += new System.EventHandler(this.m_MenuItemStreamTriggered_Click);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Location = new System.Drawing.Point(468, 507);
            this.m_ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            this.m_ButtonCancel.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonCancel.TabIndex = 0;
            this.m_ButtonCancel.TabStop = false;
            this.m_ButtonCancel.Text = "Cancel";
            this.m_ButtonCancel.UseVisualStyleBackColor = true;
            this.m_ButtonCancel.Click += new System.EventHandler(this.m_ButtonCancel_Click);
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_ButtonOK.Location = new System.Drawing.Point(389, 507);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonOK.TabIndex = 2;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // FormConfigureEventFlags
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(554, 543);
            this.Controls.Add(this.m_PanelOuter);
            this.Controls.Add(this.m_ButtonCancel);
            this.Controls.Add(this.m_ButtonOK);
            this.Name = "FormConfigureEventFlags";
            this.Text = "Configure Event Flags";
            this.m_PanelOuter.ResumeLayout(false);
            this.m_PanelDataGridViewEventStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewEventStatus)).EndInit();
            this.m_ContextMenuStripFlags.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Reference to the OK <c>Button</c> control.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonOK;

        /// <summary>
        /// Reference to the Cancel <c>Button</c> control.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonCancel;

        /// <summary>
        /// Reference to the <c>DataGridView</c> control used to display the event status.
        /// </summary>
        protected System.Windows.Forms.DataGridView m_DataGridViewEventStatus;

        /// <summary>
        /// Reference to the <c>Panel</c> control associated with the <c>DataGridView</c> control.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelDataGridViewEventStatus;

        /// <summary>
        /// Reference to the 'Cumulative History' <c>DataGridViewTextBoxColumn</c> column of the <c>DataGridView</c> control.
        /// </summary>
        protected System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnCumulativeHistory;

        /// <summary>
        /// Reference to the 'Recent History' <c>DataGridViewTextBoxColumn</c> column of the <c>DataGridView</c> control.
        /// </summary>
        protected System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnRecentHistory;

        /// <summary>
        /// Reference to the 'Enable Event' <c>DataGridViewTextBoxColumn</c> column of the <c>DataGridView</c> control.
        /// </summary>
        protected System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnEnableEvent;

        /// <summary>
        /// Reference to the 'Stream Triggered' <c>DataGridViewTextBoxColumn</c> column of the <c>DataGridView</c> control.
        /// </summary>
        protected System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnStreamTriggered;

        /// <summary>
        /// Reference to the <c>ContextMenuStrip</c> control associated with the <c>DataGridView</c> control.
        /// </summary>
        protected System.Windows.Forms.ContextMenuStrip m_ContextMenuStripFlags;

        private System.Windows.Forms.Panel m_PanelOuter;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemShowDefinition;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemEnabled;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemStreamTriggered;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnEventIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTextColumnEventDescription;
    }
}