#region --- Revision History ---
/*
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    PTU Application
 * 
 *  File name:  FormWorksetDefine.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/19/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/28/11    1.1     K.McD           1.  Added a text box to show the security level of the current workset.
 *                                      2.  Added the 'Security' legend.
 *                                      
 *  06/29/11    1.2     K.McD           1.  Modified the BackColor property associated with the form labels to be Transparent.
 *                                      2.  Minor adjustments the the Size and Location properties of a number of controls.
 *                                      3.  Set the ReadOnly and Enabled  properties of the TextBox controls used to display the workset name and the
 *                                          security level to False when displaying a pre-defined workset so that the ForeColor intensity is
 *                                          consistent.
 *                                          
 *  10/26/11    1.3    K.McD           1.  Changed the modifier of the Panel control associated with the status message to protected.
 *
 *  06/04/12    1.4    Sean.D          1.  I don't believe I directly changed anything, but it's marked as changed, so checking it in.
 *
 *  03/17/15    1.5   K.McD            References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                              1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                                  options will be replaced with the ‘Modify’ legend on ALL projects.
 *
 *                                      Modifications
 *                                      1.  Changed the form title to 'Create / Modify Workset' in the InitializeComponent() method.
 *                                      2.  Renamed GroupBox to 'Data Dictionary' instead of 'Available Watch Variables'.
 *                                      3.  Renamed Text propert of 'All' TabPage to 'Available Watch Variables'.
 *                                      4.  Moved the X coordinate position of the 'Name' legend.
 *                                      5.  Removed the ToolTipText for the TabPage header text.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Forms
{
    partial class FormWorksetDefine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorksetDefine));
            this.m_ContextMenuAll = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_MenuItemShowDefinitionAll = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ContextMenuColumns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_MenuItemShowDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemChangeChartScaleFactor = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureBitmaskPlot = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_TextBoxSecurityLevel = new System.Windows.Forms.TextBox();
            this.m_TextBoxName = new System.Windows.Forms.TextBox();
            this.m_LegendSecurity = new System.Windows.Forms.Label();
            this.m_GroupBoxWorkset = new System.Windows.Forms.GroupBox();
            this.m_LabelCountTotal = new System.Windows.Forms.Label();
            this.m_TabControlColumn = new System.Windows.Forms.TabControl();
            this.m_TabPageColumn1 = new System.Windows.Forms.TabPage();
            this.m_TextBoxHeader1 = new System.Windows.Forms.TextBox();
            this.m_LabelListBox1ColumnHeader = new System.Windows.Forms.Label();
            this.m_ListBox1 = new System.Windows.Forms.ListBox();
            this.m_ListBox1RowHeader = new System.Windows.Forms.ListBox();
            this.m_LegendHeader1 = new System.Windows.Forms.Label();
            this.m_LabelCount1 = new System.Windows.Forms.Label();
            this.m_TabPageColumn2 = new System.Windows.Forms.TabPage();
            this.m_TextBoxHeader2 = new System.Windows.Forms.TextBox();
            this.m_LabelListBox2ColumnHeader = new System.Windows.Forms.Label();
            this.m_ListBox2 = new System.Windows.Forms.ListBox();
            this.m_ListBox2RowHeader = new System.Windows.Forms.ListBox();
            this.m_LegendHeader2 = new System.Windows.Forms.Label();
            this.m_LabelCount2 = new System.Windows.Forms.Label();
            this.m_TabPageColumn3 = new System.Windows.Forms.TabPage();
            this.m_TextBoxHeader3 = new System.Windows.Forms.TextBox();
            this.m_ListBox3 = new System.Windows.Forms.ListBox();
            this.m_LabelListBox3ColumnHeader = new System.Windows.Forms.Label();
            this.m_ListBox3RowHeader = new System.Windows.Forms.ListBox();
            this.m_LegendHeader3 = new System.Windows.Forms.Label();
            this.m_LabelCount3 = new System.Windows.Forms.Label();
            this.m_ButtonMoveUp = new System.Windows.Forms.Button();
            this.m_ButtonMoveDown = new System.Windows.Forms.Button();
            this.m_ButtonRemove = new System.Windows.Forms.Button();
            this.m_GroupBoxAvailable = new System.Windows.Forms.GroupBox();
            this.m_TabControlAvailable = new System.Windows.Forms.TabControl();
            this.m_TabPageAll = new System.Windows.Forms.TabPage();
            this.m_ButtonClear = new System.Windows.Forms.Button();
            this.m_TextBoxSearch = new System.Windows.Forms.TextBox();
            this.m_LabelListBoxAvailableColumnHeader = new System.Windows.Forms.Label();
            this.m_ListBoxAvailable = new System.Windows.Forms.ListBox();
            this.m_LegendSearch = new System.Windows.Forms.Label();
            this.m_LabelAvailableCount = new System.Windows.Forms.Label();
            this.m_ButtonAdd = new System.Windows.Forms.Button();
            this.m_LegendName = new System.Windows.Forms.Label();
            this.m_ButtonApply = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_LabelStatusMessage = new System.Windows.Forms.Label();
            this.m_LegendImageStatusMessage = new System.Windows.Forms.Label();
            this.m_PanelStatusMessage = new System.Windows.Forms.Panel();
            this.m_ContextMenuAll.SuspendLayout();
            this.m_ContextMenuColumns.SuspendLayout();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxWorkset.SuspendLayout();
            this.m_TabControlColumn.SuspendLayout();
            this.m_TabPageColumn1.SuspendLayout();
            this.m_TabPageColumn2.SuspendLayout();
            this.m_TabPageColumn3.SuspendLayout();
            this.m_GroupBoxAvailable.SuspendLayout();
            this.m_TabControlAvailable.SuspendLayout();
            this.m_TabPageAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ErrorProvider)).BeginInit();
            this.m_PanelStatusMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ContextMenuAll
            // 
            this.m_ContextMenuAll.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemShowDefinitionAll});
            this.m_ContextMenuAll.Name = "m_ContextMenu";
            this.m_ContextMenuAll.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.m_ContextMenuAll.Size = new System.Drawing.Size(159, 26);
            // 
            // m_MenuItemShowDefinitionAll
            // 
            this.m_MenuItemShowDefinitionAll.Image = global::Common.Properties.Resources.Help;
            this.m_MenuItemShowDefinitionAll.Name = "m_MenuItemShowDefinitionAll";
            this.m_MenuItemShowDefinitionAll.Size = new System.Drawing.Size(158, 22);
            this.m_MenuItemShowDefinitionAll.Text = "Show &Definition";
            this.m_MenuItemShowDefinitionAll.Click += new System.EventHandler(this.m_MenuItemShowDefinitionAll_Click);
            // 
            // m_ContextMenuColumns
            // 
            this.m_ContextMenuColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemShowDefinition,
            this.m_MenuItemChangeChartScaleFactor,
            this.m_MenuItemConfigureBitmaskPlot});
            this.m_ContextMenuColumns.Name = "m_ContextMenu";
            this.m_ContextMenuColumns.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.m_ContextMenuColumns.Size = new System.Drawing.Size(214, 70);
            this.m_ContextMenuColumns.Opened += new System.EventHandler(this.m_ContextMenuColumns_Opened);
            // 
            // m_MenuItemShowDefinition
            // 
            this.m_MenuItemShowDefinition.Image = global::Common.Properties.Resources.Help;
            this.m_MenuItemShowDefinition.Name = "m_MenuItemShowDefinition";
            this.m_MenuItemShowDefinition.Size = new System.Drawing.Size(213, 22);
            this.m_MenuItemShowDefinition.Text = "Show &Definition";
            this.m_MenuItemShowDefinition.Click += new System.EventHandler(this.m_MenuItemShowDefinitionColumn_Click);
            // 
            // m_MenuItemChangeChartScaleFactor
            // 
            this.m_MenuItemChangeChartScaleFactor.Name = "m_MenuItemChangeChartScaleFactor";
            this.m_MenuItemChangeChartScaleFactor.Size = new System.Drawing.Size(213, 22);
            this.m_MenuItemChangeChartScaleFactor.Text = "&Change Chart Scale Factor";
            this.m_MenuItemChangeChartScaleFactor.Visible = false;
            this.m_MenuItemChangeChartScaleFactor.Click += new System.EventHandler(this.m_MenuItemChangeChartScaleFactor_Click);
            // 
            // m_MenuItemConfigureBitmaskPlot
            // 
            this.m_MenuItemConfigureBitmaskPlot.Name = "m_MenuItemConfigureBitmaskPlot";
            this.m_MenuItemConfigureBitmaskPlot.Size = new System.Drawing.Size(213, 22);
            this.m_MenuItemConfigureBitmaskPlot.Text = "Configure &Bitmask Plot";
            this.m_MenuItemConfigureBitmaskPlot.Visible = false;
            this.m_MenuItemConfigureBitmaskPlot.Click += new System.EventHandler(this.m_MenuItemConfigureBitmaskPlot_Click);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Location = new System.Drawing.Point(541, 511);
            this.m_ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            this.m_ButtonCancel.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonCancel.TabIndex = 0;
            this.m_ButtonCancel.TabStop = false;
            this.m_ButtonCancel.Text = "Cancel";
            this.m_ButtonCancel.UseVisualStyleBackColor = true;
            this.m_ButtonCancel.Click += new System.EventHandler(this.m_ButtonCancel_Click);
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_TextBoxSecurityLevel);
            this.m_PanelOuter.Controls.Add(this.m_TextBoxName);
            this.m_PanelOuter.Controls.Add(this.m_LegendSecurity);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxWorkset);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxAvailable);
            this.m_PanelOuter.Controls.Add(this.m_LegendName);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Size = new System.Drawing.Size(703, 500);
            this.m_PanelOuter.TabIndex = 1;
            this.m_PanelOuter.TabStop = true;
            // 
            // m_TextBoxSecurityLevel
            // 
            this.m_TextBoxSecurityLevel.Enabled = false;
            this.m_TextBoxSecurityLevel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_TextBoxSecurityLevel.Location = new System.Drawing.Point(459, 17);
            this.m_TextBoxSecurityLevel.Name = "m_TextBoxSecurityLevel";
            this.m_TextBoxSecurityLevel.Size = new System.Drawing.Size(218, 20);
            this.m_TextBoxSecurityLevel.TabIndex = 0;
            this.m_TextBoxSecurityLevel.TabStop = false;
            // 
            // m_TextBoxName
            // 
            this.m_TextBoxName.Location = new System.Drawing.Point(86, 17);
            this.m_TextBoxName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_TextBoxName.Name = "m_TextBoxName";
            this.m_TextBoxName.Size = new System.Drawing.Size(291, 20);
            this.m_TextBoxName.TabIndex = 1;
            this.m_TextBoxName.TextChanged += new System.EventHandler(this.m_TextBoxName_TextChanged);
            // 
            // m_LegendSecurity
            // 
            this.m_LegendSecurity.AutoSize = true;
            this.m_LegendSecurity.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendSecurity.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LegendSecurity.Location = new System.Drawing.Point(397, 20);
            this.m_LegendSecurity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_LegendSecurity.Name = "m_LegendSecurity";
            this.m_LegendSecurity.Size = new System.Drawing.Size(51, 13);
            this.m_LegendSecurity.TabIndex = 0;
            this.m_LegendSecurity.Text = "Security: ";
            // 
            // m_GroupBoxWorkset
            // 
            this.m_GroupBoxWorkset.Controls.Add(this.m_LabelCountTotal);
            this.m_GroupBoxWorkset.Controls.Add(this.m_TabControlColumn);
            this.m_GroupBoxWorkset.Controls.Add(this.m_ButtonMoveUp);
            this.m_GroupBoxWorkset.Controls.Add(this.m_ButtonMoveDown);
            this.m_GroupBoxWorkset.Controls.Add(this.m_ButtonRemove);
            this.m_GroupBoxWorkset.Location = new System.Drawing.Point(10, 54);
            this.m_GroupBoxWorkset.Name = "m_GroupBoxWorkset";
            this.m_GroupBoxWorkset.Size = new System.Drawing.Size(375, 434);
            this.m_GroupBoxWorkset.TabIndex = 2;
            this.m_GroupBoxWorkset.TabStop = false;
            this.m_GroupBoxWorkset.Text = "Workset Layout";
            // 
            // m_LabelCountTotal
            // 
            this.m_LabelCountTotal.AutoSize = true;
            this.m_LabelCountTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LabelCountTotal.Location = new System.Drawing.Point(13, 394);
            this.m_LabelCountTotal.Margin = new System.Windows.Forms.Padding(3);
            this.m_LabelCountTotal.Name = "m_LabelCountTotal";
            this.m_LabelCountTotal.Size = new System.Drawing.Size(35, 13);
            this.m_LabelCountTotal.TabIndex = 0;
            this.m_LabelCountTotal.Text = "Count";
            this.m_LabelCountTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_TabControlColumn
            // 
            this.m_TabControlColumn.Controls.Add(this.m_TabPageColumn1);
            this.m_TabControlColumn.Controls.Add(this.m_TabPageColumn2);
            this.m_TabControlColumn.Controls.Add(this.m_TabPageColumn3);
            this.m_TabControlColumn.Location = new System.Drawing.Point(12, 20);
            this.m_TabControlColumn.Multiline = true;
            this.m_TabControlColumn.Name = "m_TabControlColumn";
            this.m_TabControlColumn.SelectedIndex = 0;
            this.m_TabControlColumn.Size = new System.Drawing.Size(300, 372);
            this.m_TabControlColumn.TabIndex = 0;
            this.m_TabControlColumn.SelectedIndexChanged += new System.EventHandler(this.m_TabControlColumns_SelectedIndexChanged);
            // 
            // m_TabPageColumn1
            // 
            this.m_TabPageColumn1.BackColor = System.Drawing.SystemColors.Window;
            this.m_TabPageColumn1.Controls.Add(this.m_TextBoxHeader1);
            this.m_TabPageColumn1.Controls.Add(this.m_LabelListBox1ColumnHeader);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBox1);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBox1RowHeader);
            this.m_TabPageColumn1.Controls.Add(this.m_LegendHeader1);
            this.m_TabPageColumn1.Controls.Add(this.m_LabelCount1);
            this.m_TabPageColumn1.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageColumn1.Name = "m_TabPageColumn1";
            this.m_TabPageColumn1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_TabPageColumn1.Size = new System.Drawing.Size(292, 346);
            this.m_TabPageColumn1.TabIndex = 0;
            this.m_TabPageColumn1.Text = "Column 1";
            // 
            // m_TextBoxHeader1
            // 
            this.m_TextBoxHeader1.Location = new System.Drawing.Point(60, 12);
            this.m_TextBoxHeader1.Name = "m_TextBoxHeader1";
            this.m_TextBoxHeader1.Size = new System.Drawing.Size(222, 20);
            this.m_TextBoxHeader1.TabIndex = 1;
            this.m_TextBoxHeader1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxColumnHeader_KeyPress);
            // 
            // m_LabelListBox1ColumnHeader
            // 
            this.m_LabelListBox1ColumnHeader.AutoSize = true;
            this.m_LabelListBox1ColumnHeader.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelListBox1ColumnHeader.Location = new System.Drawing.Point(27, 44);
            this.m_LabelListBox1ColumnHeader.Name = "m_LabelListBox1ColumnHeader";
            this.m_LabelListBox1ColumnHeader.Size = new System.Drawing.Size(136, 13);
            this.m_LabelListBox1ColumnHeader.TabIndex = 0;
            this.m_LabelListBox1ColumnHeader.Text = "Watch Variable Description";
            this.m_LabelListBox1ColumnHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_ListBox1
            // 
            this.m_ListBox1.AllowDrop = true;
            this.m_ListBox1.ContextMenuStrip = this.m_ContextMenuColumns;
            this.m_ListBox1.FormattingEnabled = true;
            this.m_ListBox1.Location = new System.Drawing.Point(30, 61);
            this.m_ListBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_ListBox1.Name = "m_ListBox1";
            this.m_ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_ListBox1.Size = new System.Drawing.Size(252, 264);
            this.m_ListBox1.TabIndex = 2;
            this.m_ListBox1.UseTabStops = false;
            this.m_ListBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragDrop);
            this.m_ListBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragEnter);
            // 
            // m_ListBox1RowHeader
            // 
            this.m_ListBox1RowHeader.BackColor = System.Drawing.SystemColors.Window;
            this.m_ListBox1RowHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_ListBox1RowHeader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_ListBox1RowHeader.FormattingEnabled = true;
            this.m_ListBox1RowHeader.Location = new System.Drawing.Point(8, 63);
            this.m_ListBox1RowHeader.Name = "m_ListBox1RowHeader";
            this.m_ListBox1RowHeader.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBox1RowHeader.Size = new System.Drawing.Size(30, 260);
            this.m_ListBox1RowHeader.TabIndex = 0;
            this.m_ListBox1RowHeader.TabStop = false;
            // 
            // m_LegendHeader1
            // 
            this.m_LegendHeader1.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendHeader1.Location = new System.Drawing.Point(7, 7);
            this.m_LegendHeader1.Name = "m_LegendHeader1";
            this.m_LegendHeader1.Size = new System.Drawing.Size(48, 30);
            this.m_LegendHeader1.TabIndex = 0;
            this.m_LegendHeader1.Text = "Header Text:";
            // 
            // m_LabelCount1
            // 
            this.m_LabelCount1.AutoSize = true;
            this.m_LabelCount1.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelCount1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LabelCount1.Location = new System.Drawing.Point(27, 328);
            this.m_LabelCount1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_LabelCount1.Name = "m_LabelCount1";
            this.m_LabelCount1.Size = new System.Drawing.Size(35, 13);
            this.m_LabelCount1.TabIndex = 0;
            this.m_LabelCount1.Text = "Count";
            this.m_LabelCount1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_TabPageColumn2
            // 
            this.m_TabPageColumn2.BackColor = System.Drawing.SystemColors.Window;
            this.m_TabPageColumn2.Controls.Add(this.m_TextBoxHeader2);
            this.m_TabPageColumn2.Controls.Add(this.m_LabelListBox2ColumnHeader);
            this.m_TabPageColumn2.Controls.Add(this.m_ListBox2);
            this.m_TabPageColumn2.Controls.Add(this.m_ListBox2RowHeader);
            this.m_TabPageColumn2.Controls.Add(this.m_LegendHeader2);
            this.m_TabPageColumn2.Controls.Add(this.m_LabelCount2);
            this.m_TabPageColumn2.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageColumn2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_TabPageColumn2.Name = "m_TabPageColumn2";
            this.m_TabPageColumn2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_TabPageColumn2.Size = new System.Drawing.Size(292, 346);
            this.m_TabPageColumn2.TabIndex = 1;
            this.m_TabPageColumn2.Text = "Column 2";
            // 
            // m_TextBoxHeader2
            // 
            this.m_TextBoxHeader2.Location = new System.Drawing.Point(60, 12);
            this.m_TextBoxHeader2.Name = "m_TextBoxHeader2";
            this.m_TextBoxHeader2.Size = new System.Drawing.Size(222, 20);
            this.m_TextBoxHeader2.TabIndex = 1;
            this.m_TextBoxHeader2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxColumnHeader_KeyPress);
            // 
            // m_LabelListBox2ColumnHeader
            // 
            this.m_LabelListBox2ColumnHeader.AutoSize = true;
            this.m_LabelListBox2ColumnHeader.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelListBox2ColumnHeader.Location = new System.Drawing.Point(27, 44);
            this.m_LabelListBox2ColumnHeader.Name = "m_LabelListBox2ColumnHeader";
            this.m_LabelListBox2ColumnHeader.Size = new System.Drawing.Size(136, 13);
            this.m_LabelListBox2ColumnHeader.TabIndex = 0;
            this.m_LabelListBox2ColumnHeader.Text = "Watch Variable Description";
            this.m_LabelListBox2ColumnHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_ListBox2
            // 
            this.m_ListBox2.AllowDrop = true;
            this.m_ListBox2.ContextMenuStrip = this.m_ContextMenuColumns;
            this.m_ListBox2.FormattingEnabled = true;
            this.m_ListBox2.Location = new System.Drawing.Point(30, 61);
            this.m_ListBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_ListBox2.Name = "m_ListBox2";
            this.m_ListBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_ListBox2.Size = new System.Drawing.Size(252, 264);
            this.m_ListBox2.TabIndex = 2;
            this.m_ListBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragDrop);
            this.m_ListBox2.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragEnter);
            // 
            // m_ListBox2RowHeader
            // 
            this.m_ListBox2RowHeader.BackColor = System.Drawing.SystemColors.Window;
            this.m_ListBox2RowHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_ListBox2RowHeader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_ListBox2RowHeader.FormattingEnabled = true;
            this.m_ListBox2RowHeader.Location = new System.Drawing.Point(8, 63);
            this.m_ListBox2RowHeader.Name = "m_ListBox2RowHeader";
            this.m_ListBox2RowHeader.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBox2RowHeader.Size = new System.Drawing.Size(32, 260);
            this.m_ListBox2RowHeader.TabIndex = 0;
            this.m_ListBox2RowHeader.TabStop = false;
            // 
            // m_LegendHeader2
            // 
            this.m_LegendHeader2.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendHeader2.Location = new System.Drawing.Point(7, 7);
            this.m_LegendHeader2.Name = "m_LegendHeader2";
            this.m_LegendHeader2.Size = new System.Drawing.Size(45, 30);
            this.m_LegendHeader2.TabIndex = 0;
            this.m_LegendHeader2.Text = "Header Text:";
            // 
            // m_LabelCount2
            // 
            this.m_LabelCount2.AutoSize = true;
            this.m_LabelCount2.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelCount2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LabelCount2.Location = new System.Drawing.Point(27, 328);
            this.m_LabelCount2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_LabelCount2.Name = "m_LabelCount2";
            this.m_LabelCount2.Size = new System.Drawing.Size(35, 13);
            this.m_LabelCount2.TabIndex = 0;
            this.m_LabelCount2.Text = "Count";
            this.m_LabelCount2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_TabPageColumn3
            // 
            this.m_TabPageColumn3.BackColor = System.Drawing.SystemColors.Window;
            this.m_TabPageColumn3.Controls.Add(this.m_TextBoxHeader3);
            this.m_TabPageColumn3.Controls.Add(this.m_ListBox3);
            this.m_TabPageColumn3.Controls.Add(this.m_LabelListBox3ColumnHeader);
            this.m_TabPageColumn3.Controls.Add(this.m_ListBox3RowHeader);
            this.m_TabPageColumn3.Controls.Add(this.m_LegendHeader3);
            this.m_TabPageColumn3.Controls.Add(this.m_LabelCount3);
            this.m_TabPageColumn3.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageColumn3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_TabPageColumn3.Name = "m_TabPageColumn3";
            this.m_TabPageColumn3.Size = new System.Drawing.Size(292, 346);
            this.m_TabPageColumn3.TabIndex = 2;
            this.m_TabPageColumn3.Text = "Column 3";
            // 
            // m_TextBoxHeader3
            // 
            this.m_TextBoxHeader3.Location = new System.Drawing.Point(60, 12);
            this.m_TextBoxHeader3.Name = "m_TextBoxHeader3";
            this.m_TextBoxHeader3.Size = new System.Drawing.Size(222, 20);
            this.m_TextBoxHeader3.TabIndex = 1;
            this.m_TextBoxHeader3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxColumnHeader_KeyPress);
            // 
            // m_ListBox3
            // 
            this.m_ListBox3.AllowDrop = true;
            this.m_ListBox3.ContextMenuStrip = this.m_ContextMenuColumns;
            this.m_ListBox3.FormattingEnabled = true;
            this.m_ListBox3.Location = new System.Drawing.Point(30, 61);
            this.m_ListBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_ListBox3.Name = "m_ListBox3";
            this.m_ListBox3.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_ListBox3.Size = new System.Drawing.Size(252, 264);
            this.m_ListBox3.TabIndex = 2;
            this.m_ListBox3.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragDrop);
            this.m_ListBox3.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragEnter);
            // 
            // m_LabelListBox3ColumnHeader
            // 
            this.m_LabelListBox3ColumnHeader.AutoSize = true;
            this.m_LabelListBox3ColumnHeader.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelListBox3ColumnHeader.Location = new System.Drawing.Point(27, 44);
            this.m_LabelListBox3ColumnHeader.Name = "m_LabelListBox3ColumnHeader";
            this.m_LabelListBox3ColumnHeader.Size = new System.Drawing.Size(136, 13);
            this.m_LabelListBox3ColumnHeader.TabIndex = 0;
            this.m_LabelListBox3ColumnHeader.Text = "Watch Variable Description";
            this.m_LabelListBox3ColumnHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_ListBox3RowHeader
            // 
            this.m_ListBox3RowHeader.BackColor = System.Drawing.SystemColors.Window;
            this.m_ListBox3RowHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_ListBox3RowHeader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_ListBox3RowHeader.FormattingEnabled = true;
            this.m_ListBox3RowHeader.Location = new System.Drawing.Point(8, 63);
            this.m_ListBox3RowHeader.Name = "m_ListBox3RowHeader";
            this.m_ListBox3RowHeader.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBox3RowHeader.Size = new System.Drawing.Size(32, 260);
            this.m_ListBox3RowHeader.TabIndex = 0;
            this.m_ListBox3RowHeader.TabStop = false;
            // 
            // m_LegendHeader3
            // 
            this.m_LegendHeader3.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendHeader3.Location = new System.Drawing.Point(7, 7);
            this.m_LegendHeader3.Name = "m_LegendHeader3";
            this.m_LegendHeader3.Size = new System.Drawing.Size(45, 30);
            this.m_LegendHeader3.TabIndex = 0;
            this.m_LegendHeader3.Text = "Header Text:";
            // 
            // m_LabelCount3
            // 
            this.m_LabelCount3.AutoSize = true;
            this.m_LabelCount3.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelCount3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LabelCount3.Location = new System.Drawing.Point(27, 328);
            this.m_LabelCount3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_LabelCount3.Name = "m_LabelCount3";
            this.m_LabelCount3.Size = new System.Drawing.Size(35, 13);
            this.m_LabelCount3.TabIndex = 0;
            this.m_LabelCount3.Text = "Count";
            this.m_LabelCount3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_ButtonMoveUp
            // 
            this.m_ButtonMoveUp.BackColor = System.Drawing.Color.Transparent;
            this.m_ButtonMoveUp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_ButtonMoveUp.Image = global::Common.Properties.Resources.MoveUp;
            this.m_ButtonMoveUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_ButtonMoveUp.Location = new System.Drawing.Point(317, 197);
            this.m_ButtonMoveUp.Name = "m_ButtonMoveUp";
            this.m_ButtonMoveUp.Size = new System.Drawing.Size(50, 40);
            this.m_ButtonMoveUp.TabIndex = 0;
            this.m_ButtonMoveUp.TabStop = false;
            this.m_ButtonMoveUp.Text = "&Up";
            this.m_ButtonMoveUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_ButtonMoveUp.UseVisualStyleBackColor = false;
            this.m_ButtonMoveUp.Click += new System.EventHandler(this.m_ButtonMoveUp_Click);
            // 
            // m_ButtonMoveDown
            // 
            this.m_ButtonMoveDown.BackColor = System.Drawing.Color.Transparent;
            this.m_ButtonMoveDown.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_ButtonMoveDown.Image = global::Common.Properties.Resources.MoveDown;
            this.m_ButtonMoveDown.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_ButtonMoveDown.Location = new System.Drawing.Point(317, 243);
            this.m_ButtonMoveDown.Name = "m_ButtonMoveDown";
            this.m_ButtonMoveDown.Size = new System.Drawing.Size(50, 40);
            this.m_ButtonMoveDown.TabIndex = 0;
            this.m_ButtonMoveDown.TabStop = false;
            this.m_ButtonMoveDown.Text = "Do&wn";
            this.m_ButtonMoveDown.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_ButtonMoveDown.UseVisualStyleBackColor = false;
            this.m_ButtonMoveDown.Click += new System.EventHandler(this.m_ButtonMoveDown_Click);
            // 
            // m_ButtonRemove
            // 
            this.m_ButtonRemove.BackColor = System.Drawing.Color.Transparent;
            this.m_ButtonRemove.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_ButtonRemove.Image = global::Common.Properties.Resources.Remove;
            this.m_ButtonRemove.Location = new System.Drawing.Point(232, 398);
            this.m_ButtonRemove.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_ButtonRemove.Name = "m_ButtonRemove";
            this.m_ButtonRemove.Size = new System.Drawing.Size(80, 25);
            this.m_ButtonRemove.TabIndex = 0;
            this.m_ButtonRemove.TabStop = false;
            this.m_ButtonRemove.Text = "&Remove";
            this.m_ButtonRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.m_ButtonRemove.UseVisualStyleBackColor = false;
            this.m_ButtonRemove.Click += new System.EventHandler(this.m_ButtonRemove_Click);
            // 
            // m_GroupBoxAvailable
            // 
            this.m_GroupBoxAvailable.Controls.Add(this.m_TabControlAvailable);
            this.m_GroupBoxAvailable.Controls.Add(this.m_ButtonAdd);
            this.m_GroupBoxAvailable.Location = new System.Drawing.Point(391, 54);
            this.m_GroupBoxAvailable.Name = "m_GroupBoxAvailable";
            this.m_GroupBoxAvailable.Size = new System.Drawing.Size(302, 434);
            this.m_GroupBoxAvailable.TabIndex = 3;
            this.m_GroupBoxAvailable.TabStop = false;
            this.m_GroupBoxAvailable.Text = "Data Dictionary";
            // 
            // m_TabControlAvailable
            // 
            this.m_TabControlAvailable.Controls.Add(this.m_TabPageAll);
            this.m_TabControlAvailable.Location = new System.Drawing.Point(12, 20);
            this.m_TabControlAvailable.Name = "m_TabControlAvailable";
            this.m_TabControlAvailable.SelectedIndex = 0;
            this.m_TabControlAvailable.Size = new System.Drawing.Size(278, 372);
            this.m_TabControlAvailable.TabIndex = 0;
            this.m_TabControlAvailable.TabStop = false;
            // 
            // m_TabPageAll
            // 
            this.m_TabPageAll.BackColor = System.Drawing.SystemColors.Window;
            this.m_TabPageAll.Controls.Add(this.m_ButtonClear);
            this.m_TabPageAll.Controls.Add(this.m_TextBoxSearch);
            this.m_TabPageAll.Controls.Add(this.m_LabelListBoxAvailableColumnHeader);
            this.m_TabPageAll.Controls.Add(this.m_ListBoxAvailable);
            this.m_TabPageAll.Controls.Add(this.m_LegendSearch);
            this.m_TabPageAll.Controls.Add(this.m_LabelAvailableCount);
            this.m_TabPageAll.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageAll.Name = "m_TabPageAll";
            this.m_TabPageAll.Size = new System.Drawing.Size(270, 346);
            this.m_TabPageAll.TabIndex = 3;
            this.m_TabPageAll.Text = "Available Watch Variables";
            // 
            // m_ButtonClear
            // 
            this.m_ButtonClear.BackColor = System.Drawing.SystemColors.Window;
            this.m_ButtonClear.FlatAppearance.BorderSize = 0;
            this.m_ButtonClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.m_ButtonClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.m_ButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ButtonClear.Image = global::Common.Properties.Resources.Clear;
            this.m_ButtonClear.Location = new System.Drawing.Point(242, 14);
            this.m_ButtonClear.Name = "m_ButtonClear";
            this.m_ButtonClear.Size = new System.Drawing.Size(15, 15);
            this.m_ButtonClear.TabIndex = 0;
            this.m_ButtonClear.TabStop = false;
            this.m_ButtonClear.UseVisualStyleBackColor = false;
            this.m_ButtonClear.Click += new System.EventHandler(this.m_ButtonClear_Click);
            // 
            // m_TextBoxSearch
            // 
            this.m_TextBoxSearch.BackColor = System.Drawing.SystemColors.Window;
            this.m_TextBoxSearch.Location = new System.Drawing.Point(52, 12);
            this.m_TextBoxSearch.Name = "m_TextBoxSearch";
            this.m_TextBoxSearch.Size = new System.Drawing.Size(187, 20);
            this.m_TextBoxSearch.TabIndex = 1;
            this.m_TextBoxSearch.TextChanged += new System.EventHandler(this.m_TxtSearch_TextChanged);
            // 
            // m_LabelListBoxAvailableColumnHeader
            // 
            this.m_LabelListBoxAvailableColumnHeader.AutoSize = true;
            this.m_LabelListBoxAvailableColumnHeader.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelListBoxAvailableColumnHeader.Location = new System.Drawing.Point(5, 44);
            this.m_LabelListBoxAvailableColumnHeader.Name = "m_LabelListBoxAvailableColumnHeader";
            this.m_LabelListBoxAvailableColumnHeader.Size = new System.Drawing.Size(136, 13);
            this.m_LabelListBoxAvailableColumnHeader.TabIndex = 0;
            this.m_LabelListBoxAvailableColumnHeader.Text = "Watch Variable Description";
            this.m_LabelListBoxAvailableColumnHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_ListBoxAvailable
            // 
            this.m_ListBoxAvailable.ContextMenuStrip = this.m_ContextMenuAll;
            this.m_ListBoxAvailable.FormattingEnabled = true;
            this.m_ListBoxAvailable.Location = new System.Drawing.Point(8, 61);
            this.m_ListBoxAvailable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_ListBoxAvailable.Name = "m_ListBoxAvailable";
            this.m_ListBoxAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_ListBoxAvailable.Size = new System.Drawing.Size(252, 264);
            this.m_ListBoxAvailable.TabIndex = 2;
            this.m_ListBoxAvailable.DoubleClick += new System.EventHandler(this.m_ButtonAdd_Click);
            this.m_ListBoxAvailable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseDown);
            this.m_ListBoxAvailable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseMove);
            this.m_ListBoxAvailable.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseUp);
            // 
            // m_LegendSearch
            // 
            this.m_LegendSearch.AutoSize = true;
            this.m_LegendSearch.Location = new System.Drawing.Point(5, 15);
            this.m_LegendSearch.Name = "m_LegendSearch";
            this.m_LegendSearch.Size = new System.Drawing.Size(41, 13);
            this.m_LegendSearch.TabIndex = 0;
            this.m_LegendSearch.Text = "Search";
            // 
            // m_LabelAvailableCount
            // 
            this.m_LabelAvailableCount.AutoSize = true;
            this.m_LabelAvailableCount.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelAvailableCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LabelAvailableCount.Location = new System.Drawing.Point(5, 328);
            this.m_LabelAvailableCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_LabelAvailableCount.Name = "m_LabelAvailableCount";
            this.m_LabelAvailableCount.Size = new System.Drawing.Size(50, 13);
            this.m_LabelAvailableCount.TabIndex = 0;
            this.m_LabelAvailableCount.Text = "Available";
            // 
            // m_ButtonAdd
            // 
            this.m_ButtonAdd.BackColor = System.Drawing.Color.Transparent;
            this.m_ButtonAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_ButtonAdd.Image = global::Common.Properties.Resources.Add;
            this.m_ButtonAdd.Location = new System.Drawing.Point(12, 398);
            this.m_ButtonAdd.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.m_ButtonAdd.Name = "m_ButtonAdd";
            this.m_ButtonAdd.Size = new System.Drawing.Size(80, 25);
            this.m_ButtonAdd.TabIndex = 0;
            this.m_ButtonAdd.TabStop = false;
            this.m_ButtonAdd.Text = "&Add";
            this.m_ButtonAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_ButtonAdd.UseVisualStyleBackColor = false;
            this.m_ButtonAdd.Click += new System.EventHandler(this.m_ButtonAdd_Click);
            // 
            // m_LegendName
            // 
            this.m_LegendName.AutoSize = true;
            this.m_LegendName.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LegendName.Location = new System.Drawing.Point(16, 20);
            this.m_LegendName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_LegendName.Name = "m_LegendName";
            this.m_LegendName.Size = new System.Drawing.Size(41, 13);
            this.m_LegendName.TabIndex = 0;
            this.m_LegendName.Text = "Name: ";
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(620, 511);
            this.m_ButtonApply.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonApply.Name = "m_ButtonApply";
            this.m_ButtonApply.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonApply.TabIndex = 0;
            this.m_ButtonApply.TabStop = false;
            this.m_ButtonApply.Text = "&Apply";
            this.m_ButtonApply.UseVisualStyleBackColor = true;
            this.m_ButtonApply.Click += new System.EventHandler(this.m_ButtonApply_Click);
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_ButtonOK.Location = new System.Drawing.Point(463, 511);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonOK.TabIndex = 2;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_ErrorProvider
            // 
            this.m_ErrorProvider.ContainerControl = this;
            // 
            // m_LabelStatusMessage
            // 
            this.m_LabelStatusMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_LabelStatusMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_LabelStatusMessage.Location = new System.Drawing.Point(27, 1);
            this.m_LabelStatusMessage.Name = "m_LabelStatusMessage";
            this.m_LabelStatusMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_LabelStatusMessage.Size = new System.Drawing.Size(413, 28);
            this.m_LabelStatusMessage.TabIndex = 0;
            this.m_LabelStatusMessage.Text = "<Message>";
            this.m_LabelStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LegendImageStatusMessage
            // 
            this.m_LegendImageStatusMessage.Image = global::Common.Properties.Resources.Warning;
            this.m_LegendImageStatusMessage.Location = new System.Drawing.Point(1, 4);
            this.m_LegendImageStatusMessage.Name = "m_LegendImageStatusMessage";
            this.m_LegendImageStatusMessage.Size = new System.Drawing.Size(24, 23);
            this.m_LegendImageStatusMessage.TabIndex = 1;
            // 
            // m_PanelStatusMessage
            // 
            this.m_PanelStatusMessage.Controls.Add(this.m_LegendImageStatusMessage);
            this.m_PanelStatusMessage.Controls.Add(this.m_LabelStatusMessage);
            this.m_PanelStatusMessage.Location = new System.Drawing.Point(10, 508);
            this.m_PanelStatusMessage.Name = "m_PanelStatusMessage";
            this.m_PanelStatusMessage.Size = new System.Drawing.Size(443, 32);
            this.m_PanelStatusMessage.TabIndex = 3;
            // 
            // FormWorksetDefine
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(703, 547);
            this.Controls.Add(this.m_PanelStatusMessage);
            this.Controls.Add(this.m_PanelOuter);
            this.Controls.Add(this.m_ButtonApply);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_ButtonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "FormWorksetDefine";
            this.Text = "Create / Modify Workset";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormWorksetDefine_KeyDown);
            this.m_ContextMenuAll.ResumeLayout(false);
            this.m_ContextMenuColumns.ResumeLayout(false);
            this.m_PanelOuter.ResumeLayout(false);
            this.m_PanelOuter.PerformLayout();
            this.m_GroupBoxWorkset.ResumeLayout(false);
            this.m_GroupBoxWorkset.PerformLayout();
            this.m_TabControlColumn.ResumeLayout(false);
            this.m_TabPageColumn1.ResumeLayout(false);
            this.m_TabPageColumn1.PerformLayout();
            this.m_TabPageColumn2.ResumeLayout(false);
            this.m_TabPageColumn2.PerformLayout();
            this.m_TabPageColumn3.ResumeLayout(false);
            this.m_TabPageColumn3.PerformLayout();
            this.m_GroupBoxAvailable.ResumeLayout(false);
            this.m_TabControlAvailable.ResumeLayout(false);
            this.m_TabPageAll.ResumeLayout(false);
            this.m_TabPageAll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ErrorProvider)).EndInit();
            this.m_PanelStatusMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Reference to the Remove watch variable <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelOuter;

        /// <summary>
        /// Reference to the <c>Label</c> corresponding to the Workset legend.
        /// </summary>
        protected System.Windows.Forms.Label m_LegendName;

        /// <summary>
        /// Reference to the <c>TextBox</c> used to specify/display the name of the workset.
        /// </summary>
        protected System.Windows.Forms.TextBox m_TextBoxName;

        /// <summary>
        /// Reference to the 'Security' legend.
        /// </summary>
        protected System.Windows.Forms.Label m_LegendSecurity;

        /// <summary>
        /// Reference to the <c>TextBox</c> contol used to display the security level.
        /// </summary>
        protected System.Windows.Forms.TextBox m_TextBoxSecurityLevel;

        /// <summary>
        /// Reference to the <c>GroupBox</c> control associated with the list of watch variables in the workset.
        /// </summary>
        protected System.Windows.Forms.GroupBox m_GroupBoxWorkset;

        /// <summary>
        /// Reference to the <c>TabControl</c> used to define the workset column entries.
        /// </summary>
        protected System.Windows.Forms.TabControl m_TabControlColumn;

        /// <summary>
        /// Reference to the <c>TabPage</c> used to define the entries associated with column 1 of the workset.
        /// </summary>
        protected System.Windows.Forms.TabPage m_TabPageColumn1;

        /// <summary>
        /// Reference to the <c>TabPage</c> used to define the entries associated with column 2 of the workset.
        /// </summary>
        protected System.Windows.Forms.TabPage m_TabPageColumn2;

        /// <summary>
        /// Reference to the <c>TabPage</c> used to define the entries associated with column 3 of the workset.
        /// </summary>
        protected System.Windows.Forms.TabPage m_TabPageColumn3;

        /// <summary>
        /// Reference to the <c>ListBox</c> control that displays the entries associated with column 1.
        /// </summary>
        protected System.Windows.Forms.ListBox m_ListBox1;

        /// <summary>
        /// Reference to the <c>ListBox</c> control that displays the entries associated with column 2.
        /// </summary>
        protected System.Windows.Forms.ListBox m_ListBox2;

        /// <summary>
        /// Reference to the <c>ListBox</c> control that displays the entries associated with column 3.
        /// </summary>
        protected System.Windows.Forms.ListBox m_ListBox3;

        /// <summary>
        /// Reference to the <c>Label</c> that display the number of entries associated with column 1.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelCount1;

        /// <summary>
        /// Reference to the <c>Label</c> that display the number of entries associated with column 2.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelCount2;

        /// <summary>
        /// Reference to the <c>Label</c> that display the number of entries associated with column 3.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelCount3;

        /// <summary>
        /// Reference to the <c>Label</c> that displays the total number of entries that have been added to the workset.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelCountTotal;

        /// <summary>
        /// Reference to the Header legend associated with column 1.
        /// </summary>
        protected System.Windows.Forms.Label m_LegendHeader1;

        /// <summary>
        /// Reference to the <c>TextBox</c> used to specify/display the header text associated with column 1.
        /// </summary>
        protected System.Windows.Forms.TextBox m_TextBoxHeader1;

        /// <summary>
        /// Reference to the Header legend associated with column 2.
        /// </summary>
        protected System.Windows.Forms.Label m_LegendHeader2;

        /// <summary>
        /// Reference to the <c>TextBox</c> used to specify/display the header text associated with column 2.
        /// </summary>
        protected System.Windows.Forms.TextBox m_TextBoxHeader2;

        /// <summary>
        /// Reference to the Header legend associated with column 3.
        /// </summary>
        protected System.Windows.Forms.Label m_LegendHeader3;

        /// <summary>
        /// Reference to the <c>TextBox</c> used to specify/display the header text associated with column 3.
        /// </summary>
        protected System.Windows.Forms.TextBox m_TextBoxHeader3;

        /// <summary>
        /// Reference to the <c>GroupBox</c> control associated with the list of available watch variables.
        /// </summary>
        protected System.Windows.Forms.GroupBox m_GroupBoxAvailable;

        /// <summary>
        /// Reference to the <c>TabControl</c> associated with the list of available watch variables.
        /// </summary>
        protected System.Windows.Forms.TabControl m_TabControlAvailable;

        /// <summary>
        /// Reference to the <c>TabPage</c> associated with the list of available watch variables.
        /// </summary>
        protected System.Windows.Forms.TabPage m_TabPageAll;

        /// <summary>
        /// Reference to the <c>ListBox</c> control that displays the available entries.
        /// </summary>
        protected System.Windows.Forms.ListBox m_ListBoxAvailable;

        /// <summary>
        /// Reference to the <c>Label</c> that display the number of available entries.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelAvailableCount;

        /// <summary>
        /// Reference to the 'Add' <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonRemove;

        /// <summary>
        /// Reference to the 'Move Up' <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonMoveUp;

        /// <summary>
        /// Reference to the 'Move Down' <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonMoveDown;

        /// <summary>
        /// Reference to the OK <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonOK;

        /// <summary>
        /// Reference to the Cancel <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonCancel;

        /// <summary>
        /// Reference to the Apply <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonApply;

        /// <summary>
        /// Reference to the 'Remove' <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonAdd;

        /// <summary>
        /// Reference to the 'Columns' <c>ContextMenuStrip</c> control.
        /// </summary>
        protected System.Windows.Forms.ContextMenuStrip m_ContextMenuColumns;

        /// <summary>
        /// Reference to the 'Available' <c>ContextMenuStrip</c> control.
        /// </summary>
        protected System.Windows.Forms.ContextMenuStrip m_ContextMenuAll;

        /// <summary>
        /// Reference to the 'Change Chart Scale' <c>ToolStripMenuItem</c>.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_MenuItemChangeChartScaleFactor;

        /// <summary>
        ///  Reference to the 'Configure Bitmask Plots' <c>ToolStripMenuItem</c>.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureBitmaskPlot;

        /// <summary>
        /// Reference to the <c>ListBox</c> control displaying the row header text for column 1.
        /// </summary>
        protected System.Windows.Forms.ListBox m_ListBox1RowHeader;

        /// <summary>
        /// Reference to the <c>ListBox</c> control displaying the row header text for column 1.
        /// </summary>
        protected System.Windows.Forms.ListBox m_ListBox3RowHeader;

        /// <summary>
        /// Reference to the <c>ListBox</c> control displaying the row header text for column 1.
        /// </summary>
        protected System.Windows.Forms.ListBox m_ListBox2RowHeader;

        /// <summary>
        /// Reference to the header <c>Label</c> associated with the <c>ListBox</c> control displaying the items defined in column 1 of the workset.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelListBox1ColumnHeader;

        /// <summary>
        /// Reference to the header <c>Label</c> associated with the <c>ListBox</c> control displaying the items defined in column 2 of the workset.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelListBox2ColumnHeader;

        /// <summary>
        /// Reference to the header <c>Label</c> associated with the <c>ListBox</c> control displaying the items defined in column 3 of the workset.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelListBox3ColumnHeader;

        /// <summary>
        /// Reference to the header <c>Label</c> associated with the <c>ListBox</c> control displaying the available items.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelListBoxAvailableColumnHeader;

        /// <summary>
        /// Reference to the <c>Panel</c> control associated with the status message.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelStatusMessage;

        /// <summary>
        /// Reference to the 'Clear/Search' <c>Button</c>.
        /// </summary>
        private System.Windows.Forms.Button m_ButtonClear;

        /// <summary>
        /// Reference to the 'Search' <c>Label</c>.
        /// </summary>
        private System.Windows.Forms.Label m_LegendSearch;

        /// <summary>
        /// Reference to the 'Search' <c>TextBox</c>.
        /// </summary>
        private System.Windows.Forms.TextBox m_TextBoxSearch;

        /// <summary>
        /// Reference to the 'Show Definition' <c>ToolStripMenuItem</c>.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemShowDefinition;

        /// <summary>
        /// Reference to the 'Show Definition All' <c>ToolStripMenuItem</c>.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemShowDefinitionAll;

        /// <summary>
        /// Reference to the <c>ErrorProvider</c> control.
        /// </summary>
        private System.Windows.Forms.ErrorProvider m_ErrorProvider;

        /// <summary>
        /// Reference to the 'Status Message' <c>Label</c>.
        /// </summary>
        private System.Windows.Forms.Label m_LabelStatusMessage;

        /// <summary>
        /// Reference to the 'Status Message' image <c>Label</c>.
        /// </summary>
        private System.Windows.Forms.Label m_LegendImageStatusMessage;
    }
}