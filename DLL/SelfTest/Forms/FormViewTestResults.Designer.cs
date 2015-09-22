#region --- Revision History ---
/*
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited.  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Watch
 * 
 *  File name:  FormViewTestResults.Designer.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/10/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  07/29/11    1.1     K.McD           1.  Minor changes to the presentation of the interactive test panel. Now uses a GroupBox control. 
 *                                      2.  Renamed a number of Toolbox controls.
 *                                      
 *  08/10/11    1.2     K.McD           1.  Set the BackgroundImage property of the ToolStrip control associated with the Abort and Continue buttons
 *                                          for the interactive tests.
 *                                      2.  Minor adjustments to the Size and Location property of one or more controls.
 *                                      3.  Set the BackColor property of one or more controls to Transparent.
 *                                      
 *  12/01/11    1.3     K.McD           1.  Asserted the Visible property of the ToolStrip control associated with the interactive test Abort/Continue
 *                                          buttons.
 *                                          
 *  03/23/15    1.4     K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *  
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                          Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *
 *                                           1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                               options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                               
 *                                  Modifications
 *                                  1.   Auto-Update: this.m_ContextMenuStripDataGridViewListResult.Size = new System.Drawing.Size(184, 48) to 
 *                                       this.m_ContextMenuStripDataGridViewListResult.Size = new System.Drawing.Size(184, 70) to accommodate the 
 *                                       size change of the 'Modify' image resource.
 *  
 *
 */
#endregion --- Revision History ---

namespace SelfTest.Forms
{
    partial class FormViewTestResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormViewTestResults));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.m_TabPageListResult = new System.Windows.Forms.TabPage();
            this.m_PanelWindowsHelpListResult = new System.Windows.Forms.Panel();
            this.m_GroupBoxInteractiveTest = new System.Windows.Forms.GroupBox();
            this.m_PanelInteractiveTestVariableList = new System.Windows.Forms.Panel();
            this.m_PanelInteractiveTestVCUCommands = new System.Windows.Forms.Panel();
            this.m_ToolStripInteractiveTestVCUCommands = new System.Windows.Forms.ToolStrip();
            this.m_ToolStripButtonInteractiveTestContinue = new System.Windows.Forms.ToolStripButton();
            this.m_ToolStripSeparatorInteractiveTestContinue = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripButtonInteractiveTestAbort = new System.Windows.Forms.ToolStripButton();
            this.m_ToolStripSeparatorInteractiveTestAbort = new System.Windows.Forms.ToolStripSeparator();
            this.m_PanelInteractiveTestVariableHeader = new System.Windows.Forms.Panel();
            this.m_LegendSelfTestVariableValues = new System.Windows.Forms.Label();
            this.m_PanelInteractiveTestHelp = new System.Windows.Forms.Panel();
            this.m_PanelDataGridViewListResult = new System.Windows.Forms.Panel();
            this.m_DataGridViewListResult = new System.Windows.Forms.DataGridView();
            this.m_DataGridViewListResultTextBoxColumnTestNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewListResultTextBoxColumnTestCase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewListResultTextBoxColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewListResultTextBoxColumnResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewListResultTextBoxColumnPassCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewListResultTextBoxColumnFailCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewListResultTextBoxColumnExecutionCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_ContextMenuStripDataGridViewListResult = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ToolStripMenuItemListResultShowDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItemListResultFaultSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.m_NumericUpDownLoopCount = new System.Windows.Forms.NumericUpDown();
            this.m_LegendLoopCount = new System.Windows.Forms.Label();
            this.m_CheckBoxLoopForever = new System.Windows.Forms.CheckBox();
            this.m_TabPagePassFailResult = new System.Windows.Forms.TabPage();
            this.m_PanelWindowsHelpPassFailResult = new System.Windows.Forms.Panel();
            this.m_PanelDataGridViewPassFailResult = new System.Windows.Forms.Panel();
            this.m_DataGridViewPassFailResult = new System.Windows.Forms.DataGridView();
            this.m_DataGridViewPassFailResultTextBoxColumnTestNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewPassFailResultTextBoxColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewPassFailResultTextBoxColumnResults = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewPassFailResultTextBoxColumnPassCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewPassFailResultTextBoxColumnFailCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_ContextMenuStripDataGridViewPassFailResult = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ToolStripMenuItemPassFailResultShowDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.m_PanelDataGridViewTestList = new System.Windows.Forms.Panel();
            this.m_DataGridViewTestList = new System.Windows.Forms.DataGridView();
            this.m_DataGridViewTestListTextBoxColumnTestNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTestListTextBoxColumnTestCase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTestListTextBoxColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTestListTextBoxColumnResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTestListTextBoxColumnPassCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTestListTextBoxColumnFailCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_DataGridViewTestListTextBoxColumnExecutionCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_ContextMenuStripDataGridViewTestList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ToolStripMenuItemTestListShowDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.m_PanelWindowsHelpTestList = new System.Windows.Forms.Panel();
            this.m_ComboBoxTestList = new System.Windows.Forms.ComboBox();
            this.m_LegendTestList = new System.Windows.Forms.Label();
            this.m_TabControl.SuspendLayout();
            this.m_PanelInformation.SuspendLayout();
            this.m_TabPage1.SuspendLayout();
            this.m_TabPageListResult.SuspendLayout();
            this.m_PanelWindowsHelpListResult.SuspendLayout();
            this.m_GroupBoxInteractiveTest.SuspendLayout();
            this.m_PanelInteractiveTestVCUCommands.SuspendLayout();
            this.m_ToolStripInteractiveTestVCUCommands.SuspendLayout();
            this.m_PanelInteractiveTestVariableHeader.SuspendLayout();
            this.m_PanelDataGridViewListResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewListResult)).BeginInit();
            this.m_ContextMenuStripDataGridViewListResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLoopCount)).BeginInit();
            this.m_TabPagePassFailResult.SuspendLayout();
            this.m_PanelDataGridViewPassFailResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewPassFailResult)).BeginInit();
            this.m_ContextMenuStripDataGridViewPassFailResult.SuspendLayout();
            this.m_PanelDataGridViewTestList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewTestList)).BeginInit();
            this.m_ContextMenuStripDataGridViewTestList.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_TabControl
            // 
            this.m_TabControl.Controls.Add(this.m_TabPageListResult);
            this.m_TabControl.Controls.Add(this.m_TabPagePassFailResult);
            this.m_TabControl.Location = new System.Drawing.Point(0, 90);
            this.m_TabControl.Margin = new System.Windows.Forms.Padding(12);
            this.m_TabControl.Size = new System.Drawing.Size(1200, 610);
            this.m_TabControl.TabIndex = 2;
            this.m_TabControl.SelectedIndexChanged += new System.EventHandler(this.m_TabControl_SelectedIndexChanged);
            this.m_TabControl.Controls.SetChildIndex(this.m_TabPagePassFailResult, 0);
            this.m_TabControl.Controls.SetChildIndex(this.m_TabPageListResult, 0);
            this.m_TabControl.Controls.SetChildIndex(this.m_TabPage1, 0);
            // 
            // m_PanelInformation
            // 
            this.m_PanelInformation.Controls.Add(this.m_ComboBoxTestList);
            this.m_PanelInformation.Controls.Add(this.m_LegendTestList);
            this.m_PanelInformation.Controls.Add(this.m_CheckBoxLoopForever);
            this.m_PanelInformation.Controls.Add(this.m_LegendLoopCount);
            this.m_PanelInformation.Controls.Add(this.m_NumericUpDownLoopCount);
            this.m_PanelInformation.Location = new System.Drawing.Point(0, 57);
            this.m_PanelInformation.TabIndex = 1;
            this.m_PanelInformation.Controls.SetChildIndex(this.m_NumericUpDownLoopCount, 0);
            this.m_PanelInformation.Controls.SetChildIndex(this.m_LegendLoopCount, 0);
            this.m_PanelInformation.Controls.SetChildIndex(this.m_CheckBoxLoopForever, 0);
            this.m_PanelInformation.Controls.SetChildIndex(this.m_LegendTestList, 0);
            this.m_PanelInformation.Controls.SetChildIndex(this.m_ComboBoxTestList, 0);
            // 
            // m_TabPage1
            // 
            this.m_TabPage1.Controls.Add(this.m_PanelWindowsHelpTestList);
            this.m_TabPage1.Controls.Add(this.m_PanelDataGridViewTestList);
            this.m_TabPage1.Padding = new System.Windows.Forms.Padding(12);
            this.m_TabPage1.Text = "Selected Tests";
            // 
            // m_TabPageListResult
            // 
            this.m_TabPageListResult.AutoScroll = true;
            this.m_TabPageListResult.BackColor = System.Drawing.SystemColors.Window;
            this.m_TabPageListResult.Controls.Add(this.m_PanelWindowsHelpListResult);
            this.m_TabPageListResult.Controls.Add(this.m_PanelDataGridViewListResult);
            this.m_TabPageListResult.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageListResult.Name = "m_TabPageListResult";
            this.m_TabPageListResult.Padding = new System.Windows.Forms.Padding(12);
            this.m_TabPageListResult.Size = new System.Drawing.Size(1192, 584);
            this.m_TabPageListResult.TabIndex = 1;
            this.m_TabPageListResult.Text = "Result [List Format]";
            // 
            // m_PanelWindowsHelpListResult
            // 
            this.m_PanelWindowsHelpListResult.Controls.Add(this.m_GroupBoxInteractiveTest);
            this.m_PanelWindowsHelpListResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_PanelWindowsHelpListResult.Location = new System.Drawing.Point(492, 12);
            this.m_PanelWindowsHelpListResult.Name = "m_PanelWindowsHelpListResult";
            this.m_PanelWindowsHelpListResult.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.m_PanelWindowsHelpListResult.Size = new System.Drawing.Size(362, 560);
            this.m_PanelWindowsHelpListResult.TabIndex = 1;
            // 
            // m_GroupBoxInteractiveTest
            // 
            this.m_GroupBoxInteractiveTest.Controls.Add(this.m_PanelInteractiveTestVariableList);
            this.m_GroupBoxInteractiveTest.Controls.Add(this.m_PanelInteractiveTestVCUCommands);
            this.m_GroupBoxInteractiveTest.Controls.Add(this.m_PanelInteractiveTestVariableHeader);
            this.m_GroupBoxInteractiveTest.Controls.Add(this.m_PanelInteractiveTestHelp);
            this.m_GroupBoxInteractiveTest.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_GroupBoxInteractiveTest.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_GroupBoxInteractiveTest.Location = new System.Drawing.Point(10, 0);
            this.m_GroupBoxInteractiveTest.Name = "m_GroupBoxInteractiveTest";
            this.m_GroupBoxInteractiveTest.Size = new System.Drawing.Size(350, 560);
            this.m_GroupBoxInteractiveTest.TabIndex = 0;
            this.m_GroupBoxInteractiveTest.TabStop = false;
            this.m_GroupBoxInteractiveTest.Visible = false;
            // 
            // m_PanelInteractiveTestVariableList
            // 
            this.m_PanelInteractiveTestVariableList.AutoScroll = true;
            this.m_PanelInteractiveTestVariableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PanelInteractiveTestVariableList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_PanelInteractiveTestVariableList.Location = new System.Drawing.Point(3, 243);
            this.m_PanelInteractiveTestVariableList.Name = "m_PanelInteractiveTestVariableList";
            this.m_PanelInteractiveTestVariableList.Size = new System.Drawing.Size(344, 264);
            this.m_PanelInteractiveTestVariableList.TabIndex = 0;
            // 
            // m_PanelInteractiveTestVCUCommands
            // 
            this.m_PanelInteractiveTestVCUCommands.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.m_PanelInteractiveTestVCUCommands.Controls.Add(this.m_ToolStripInteractiveTestVCUCommands);
            this.m_PanelInteractiveTestVCUCommands.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_PanelInteractiveTestVCUCommands.Location = new System.Drawing.Point(3, 507);
            this.m_PanelInteractiveTestVCUCommands.Name = "m_PanelInteractiveTestVCUCommands";
            this.m_PanelInteractiveTestVCUCommands.Padding = new System.Windows.Forms.Padding(1);
            this.m_PanelInteractiveTestVCUCommands.Size = new System.Drawing.Size(344, 50);
            this.m_PanelInteractiveTestVCUCommands.TabIndex = 0;
            // 
            // m_ToolStripInteractiveTestVCUCommands
            // 
            this.m_ToolStripInteractiveTestVCUCommands.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_ToolStripInteractiveTestVCUCommands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ToolStripInteractiveTestVCUCommands.BackgroundImage")));
            this.m_ToolStripInteractiveTestVCUCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ToolStripInteractiveTestVCUCommands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_ToolStripInteractiveTestVCUCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripButtonInteractiveTestContinue,
            this.m_ToolStripSeparatorInteractiveTestContinue,
            this.m_ToolStripButtonInteractiveTestAbort,
            this.m_ToolStripSeparatorInteractiveTestAbort});
            this.m_ToolStripInteractiveTestVCUCommands.Location = new System.Drawing.Point(1, 1);
            this.m_ToolStripInteractiveTestVCUCommands.Name = "m_ToolStripInteractiveTestVCUCommands";
            this.m_ToolStripInteractiveTestVCUCommands.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.m_ToolStripInteractiveTestVCUCommands.Size = new System.Drawing.Size(342, 48);
            this.m_ToolStripInteractiveTestVCUCommands.TabIndex = 0;
            // 
            // m_ToolStripButtonInteractiveTestContinue
            // 
            this.m_ToolStripButtonInteractiveTestContinue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_ToolStripButtonInteractiveTestContinue.AutoSize = false;
            this.m_ToolStripButtonInteractiveTestContinue.AutoToolTip = false;
            this.m_ToolStripButtonInteractiveTestContinue.Image = global::SelfTest.Properties.Resources.Continue;
            this.m_ToolStripButtonInteractiveTestContinue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_ToolStripButtonInteractiveTestContinue.Name = "m_ToolStripButtonInteractiveTestContinue";
            this.m_ToolStripButtonInteractiveTestContinue.Size = new System.Drawing.Size(70, 45);
            this.m_ToolStripButtonInteractiveTestContinue.Text = "Continue";
            this.m_ToolStripButtonInteractiveTestContinue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_ToolStripButtonInteractiveTestContinue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_ToolStripButtonInteractiveTestContinue.Click += new System.EventHandler(this.F6_Click);
            // 
            // m_ToolStripSeparatorInteractiveTestContinue
            // 
            this.m_ToolStripSeparatorInteractiveTestContinue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_ToolStripSeparatorInteractiveTestContinue.Name = "m_ToolStripSeparatorInteractiveTestContinue";
            this.m_ToolStripSeparatorInteractiveTestContinue.Size = new System.Drawing.Size(6, 48);
            this.m_ToolStripSeparatorInteractiveTestContinue.Click += new System.EventHandler(this.F6_Click);
            // 
            // m_ToolStripButtonInteractiveTestAbort
            // 
            this.m_ToolStripButtonInteractiveTestAbort.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_ToolStripButtonInteractiveTestAbort.AutoSize = false;
            this.m_ToolStripButtonInteractiveTestAbort.AutoToolTip = false;
            this.m_ToolStripButtonInteractiveTestAbort.Image = global::SelfTest.Properties.Resources.Abort;
            this.m_ToolStripButtonInteractiveTestAbort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_ToolStripButtonInteractiveTestAbort.Name = "m_ToolStripButtonInteractiveTestAbort";
            this.m_ToolStripButtonInteractiveTestAbort.Size = new System.Drawing.Size(70, 45);
            this.m_ToolStripButtonInteractiveTestAbort.Text = "Abort";
            this.m_ToolStripButtonInteractiveTestAbort.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_ToolStripButtonInteractiveTestAbort.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_ToolStripButtonInteractiveTestAbort.Click += new System.EventHandler(this.F4_Click);
            // 
            // m_ToolStripSeparatorInteractiveTestAbort
            // 
            this.m_ToolStripSeparatorInteractiveTestAbort.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_ToolStripSeparatorInteractiveTestAbort.Name = "m_ToolStripSeparatorInteractiveTestAbort";
            this.m_ToolStripSeparatorInteractiveTestAbort.Size = new System.Drawing.Size(6, 48);
            // 
            // m_PanelInteractiveTestVariableHeader
            // 
            this.m_PanelInteractiveTestVariableHeader.AutoSize = true;
            this.m_PanelInteractiveTestVariableHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_PanelInteractiveTestVariableHeader.Controls.Add(this.m_LegendSelfTestVariableValues);
            this.m_PanelInteractiveTestVariableHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelInteractiveTestVariableHeader.Location = new System.Drawing.Point(3, 216);
            this.m_PanelInteractiveTestVariableHeader.Name = "m_PanelInteractiveTestVariableHeader";
            this.m_PanelInteractiveTestVariableHeader.Size = new System.Drawing.Size(344, 27);
            this.m_PanelInteractiveTestVariableHeader.TabIndex = 0;
            // 
            // m_LegendSelfTestVariableValues
            // 
            this.m_LegendSelfTestVariableValues.BackColor = System.Drawing.Color.LightSteelBlue;
            this.m_LegendSelfTestVariableValues.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_LegendSelfTestVariableValues.Location = new System.Drawing.Point(0, 0);
            this.m_LegendSelfTestVariableValues.Name = "m_LegendSelfTestVariableValues";
            this.m_LegendSelfTestVariableValues.Padding = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.m_LegendSelfTestVariableValues.Size = new System.Drawing.Size(344, 27);
            this.m_LegendSelfTestVariableValues.TabIndex = 0;
            this.m_LegendSelfTestVariableValues.Text = "Self Test Variable Values";
            // 
            // m_PanelInteractiveTestHelp
            // 
            this.m_PanelInteractiveTestHelp.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelInteractiveTestHelp.Location = new System.Drawing.Point(3, 16);
            this.m_PanelInteractiveTestHelp.Name = "m_PanelInteractiveTestHelp";
            this.m_PanelInteractiveTestHelp.Size = new System.Drawing.Size(344, 200);
            this.m_PanelInteractiveTestHelp.TabIndex = 0;
            // 
            // m_PanelDataGridViewListResult
            // 
            this.m_PanelDataGridViewListResult.AutoScroll = true;
            this.m_PanelDataGridViewListResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_PanelDataGridViewListResult.Controls.Add(this.m_DataGridViewListResult);
            this.m_PanelDataGridViewListResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_PanelDataGridViewListResult.Location = new System.Drawing.Point(12, 12);
            this.m_PanelDataGridViewListResult.Name = "m_PanelDataGridViewListResult";
            this.m_PanelDataGridViewListResult.Size = new System.Drawing.Size(480, 560);
            this.m_PanelDataGridViewListResult.TabIndex = 0;
            // 
            // m_DataGridViewListResult
            // 
            this.m_DataGridViewListResult.AllowUserToAddRows = false;
            this.m_DataGridViewListResult.AllowUserToDeleteRows = false;
            this.m_DataGridViewListResult.AllowUserToResizeColumns = false;
            this.m_DataGridViewListResult.AllowUserToResizeRows = false;
            this.m_DataGridViewListResult.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_DataGridViewListResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_DataGridViewListResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.m_DataGridViewListResult.ColumnHeadersHeight = 28;
            this.m_DataGridViewListResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.m_DataGridViewListResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.m_DataGridViewListResultTextBoxColumnTestNumber,
            this.m_DataGridViewListResultTextBoxColumnTestCase,
            this.m_DataGridViewListResultTextBoxColumnDescription,
            this.m_DataGridViewListResultTextBoxColumnResult,
            this.m_DataGridViewListResultTextBoxColumnPassCount,
            this.m_DataGridViewListResultTextBoxColumnFailCount,
            this.m_DataGridViewListResultTextBoxColumnExecutionCount});
            this.m_DataGridViewListResult.ContextMenuStrip = this.m_ContextMenuStripDataGridViewListResult;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewListResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.m_DataGridViewListResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DataGridViewListResult.Location = new System.Drawing.Point(0, 0);
            this.m_DataGridViewListResult.MultiSelect = false;
            this.m_DataGridViewListResult.Name = "m_DataGridViewListResult";
            this.m_DataGridViewListResult.ReadOnly = true;
            this.m_DataGridViewListResult.RowHeadersVisible = false;
            this.m_DataGridViewListResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_DataGridViewListResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_DataGridViewListResult.ShowCellToolTips = false;
            this.m_DataGridViewListResult.Size = new System.Drawing.Size(478, 558);
            this.m_DataGridViewListResult.StandardTab = true;
            this.m_DataGridViewListResult.TabIndex = 1;
            this.m_DataGridViewListResult.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_DataGridViewListResult_CellContentDoubleClick);
            this.m_DataGridViewListResult.SelectionChanged += new System.EventHandler(this.m_DataGridView_SelectionChanged);
            // 
            // m_DataGridViewListResultTextBoxColumnTestNumber
            // 
            this.m_DataGridViewListResultTextBoxColumnTestNumber.HeaderText = "Test No.";
            this.m_DataGridViewListResultTextBoxColumnTestNumber.Name = "m_DataGridViewListResultTextBoxColumnTestNumber";
            this.m_DataGridViewListResultTextBoxColumnTestNumber.ReadOnly = true;
            this.m_DataGridViewListResultTextBoxColumnTestNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewListResultTextBoxColumnTestNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewListResultTextBoxColumnTestNumber.ToolTipText = "The self test identifier associated with the test.";
            this.m_DataGridViewListResultTextBoxColumnTestNumber.Width = 60;
            // 
            // m_DataGridViewListResultTextBoxColumnTestCase
            // 
            this.m_DataGridViewListResultTextBoxColumnTestCase.HeaderText = "Test Case";
            this.m_DataGridViewListResultTextBoxColumnTestCase.Name = "m_DataGridViewListResultTextBoxColumnTestCase";
            this.m_DataGridViewListResultTextBoxColumnTestCase.ReadOnly = true;
            this.m_DataGridViewListResultTextBoxColumnTestCase.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewListResultTextBoxColumnTestCase.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewListResultTextBoxColumnTestCase.ToolTipText = "The test case value associated with the current result.";
            this.m_DataGridViewListResultTextBoxColumnTestCase.Visible = false;
            this.m_DataGridViewListResultTextBoxColumnTestCase.Width = 5;
            // 
            // m_DataGridViewListResultTextBoxColumnDescription
            // 
            this.m_DataGridViewListResultTextBoxColumnDescription.HeaderText = "Description";
            this.m_DataGridViewListResultTextBoxColumnDescription.Name = "m_DataGridViewListResultTextBoxColumnDescription";
            this.m_DataGridViewListResultTextBoxColumnDescription.ReadOnly = true;
            this.m_DataGridViewListResultTextBoxColumnDescription.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewListResultTextBoxColumnDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewListResultTextBoxColumnDescription.ToolTipText = "A description of the test.";
            this.m_DataGridViewListResultTextBoxColumnDescription.Width = 250;
            // 
            // m_DataGridViewListResultTextBoxColumnResult
            // 
            this.m_DataGridViewListResultTextBoxColumnResult.HeaderText = "Result";
            this.m_DataGridViewListResultTextBoxColumnResult.Name = "m_DataGridViewListResultTextBoxColumnResult";
            this.m_DataGridViewListResultTextBoxColumnResult.ReadOnly = true;
            this.m_DataGridViewListResultTextBoxColumnResult.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewListResultTextBoxColumnResult.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewListResultTextBoxColumnResult.ToolTipText = "The test result (Pass/Fail).";
            this.m_DataGridViewListResultTextBoxColumnResult.Width = 70;
            // 
            // m_DataGridViewListResultTextBoxColumnPassCount
            // 
            this.m_DataGridViewListResultTextBoxColumnPassCount.HeaderText = "# Pass";
            this.m_DataGridViewListResultTextBoxColumnPassCount.Name = "m_DataGridViewListResultTextBoxColumnPassCount";
            this.m_DataGridViewListResultTextBoxColumnPassCount.ReadOnly = true;
            this.m_DataGridViewListResultTextBoxColumnPassCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewListResultTextBoxColumnPassCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewListResultTextBoxColumnPassCount.ToolTipText = "The pass count for the current run.";
            this.m_DataGridViewListResultTextBoxColumnPassCount.Visible = false;
            this.m_DataGridViewListResultTextBoxColumnPassCount.Width = 5;
            // 
            // m_DataGridViewListResultTextBoxColumnFailCount
            // 
            this.m_DataGridViewListResultTextBoxColumnFailCount.HeaderText = "# Fail";
            this.m_DataGridViewListResultTextBoxColumnFailCount.Name = "m_DataGridViewListResultTextBoxColumnFailCount";
            this.m_DataGridViewListResultTextBoxColumnFailCount.ReadOnly = true;
            this.m_DataGridViewListResultTextBoxColumnFailCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewListResultTextBoxColumnFailCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewListResultTextBoxColumnFailCount.ToolTipText = "The fail count for the current run.";
            this.m_DataGridViewListResultTextBoxColumnFailCount.Visible = false;
            this.m_DataGridViewListResultTextBoxColumnFailCount.Width = 5;
            // 
            // m_DataGridViewListResultTextBoxColumnExecutionCount
            // 
            this.m_DataGridViewListResultTextBoxColumnExecutionCount.HeaderText = "#";
            this.m_DataGridViewListResultTextBoxColumnExecutionCount.Name = "m_DataGridViewListResultTextBoxColumnExecutionCount";
            this.m_DataGridViewListResultTextBoxColumnExecutionCount.ReadOnly = true;
            this.m_DataGridViewListResultTextBoxColumnExecutionCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewListResultTextBoxColumnExecutionCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewListResultTextBoxColumnExecutionCount.ToolTipText = "The number of times the test has been executed in the current run.";
            this.m_DataGridViewListResultTextBoxColumnExecutionCount.Width = 70;
            // 
            // m_ContextMenuStripDataGridViewListResult
            // 
            this.m_ContextMenuStripDataGridViewListResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItemListResultShowDefinition,
            this.m_ToolStripMenuItemListResultFaultSummary});
            this.m_ContextMenuStripDataGridViewListResult.Name = "m_ContextMenuStripDataGridView";
            this.m_ContextMenuStripDataGridViewListResult.Size = new System.Drawing.Size(184, 48);
            this.m_ContextMenuStripDataGridViewListResult.Opened += new System.EventHandler(this.m_ContextMenuStripDataGridViewListResult_Opened);
            // 
            // m_ToolStripMenuItemListResultShowDefinition
            // 
            this.m_ToolStripMenuItemListResultShowDefinition.Image = global::SelfTest.Properties.Resources.Help;
            this.m_ToolStripMenuItemListResultShowDefinition.Name = "m_ToolStripMenuItemListResultShowDefinition";
            this.m_ToolStripMenuItemListResultShowDefinition.Size = new System.Drawing.Size(183, 22);
            this.m_ToolStripMenuItemListResultShowDefinition.Text = "Show Test &Definition";
            this.m_ToolStripMenuItemListResultShowDefinition.Click += new System.EventHandler(this.m_ToolStripMenuItemListResultShowDefinition_Click);
            // 
            // m_ToolStripMenuItemListResultFaultSummary
            // 
            this.m_ToolStripMenuItemListResultFaultSummary.Name = "m_ToolStripMenuItemListResultFaultSummary";
            this.m_ToolStripMenuItemListResultFaultSummary.Size = new System.Drawing.Size(183, 22);
            this.m_ToolStripMenuItemListResultFaultSummary.Text = "&Fault Summary";
            this.m_ToolStripMenuItemListResultFaultSummary.Click += new System.EventHandler(this.m_ToolStripMenuItemListResultTestCaseAnalysis_Click);
            // 
            // m_NumericUpDownLoopCount
            // 
            this.m_NumericUpDownLoopCount.Location = new System.Drawing.Point(74, 6);
            this.m_NumericUpDownLoopCount.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.m_NumericUpDownLoopCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_NumericUpDownLoopCount.Name = "m_NumericUpDownLoopCount";
            this.m_NumericUpDownLoopCount.Size = new System.Drawing.Size(44, 20);
            this.m_NumericUpDownLoopCount.TabIndex = 0;
            this.m_NumericUpDownLoopCount.TabStop = false;
            this.m_NumericUpDownLoopCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_LegendLoopCount
            // 
            this.m_LegendLoopCount.AutoSize = true;
            this.m_LegendLoopCount.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendLoopCount.Location = new System.Drawing.Point(0, 9);
            this.m_LegendLoopCount.Name = "m_LegendLoopCount";
            this.m_LegendLoopCount.Size = new System.Drawing.Size(62, 13);
            this.m_LegendLoopCount.TabIndex = 0;
            this.m_LegendLoopCount.Text = "Loo&p Count";
            // 
            // m_CheckBoxLoopForever
            // 
            this.m_CheckBoxLoopForever.AutoSize = true;
            this.m_CheckBoxLoopForever.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxLoopForever.Location = new System.Drawing.Point(150, 8);
            this.m_CheckBoxLoopForever.Name = "m_CheckBoxLoopForever";
            this.m_CheckBoxLoopForever.Size = new System.Drawing.Size(89, 17);
            this.m_CheckBoxLoopForever.TabIndex = 0;
            this.m_CheckBoxLoopForever.TabStop = false;
            this.m_CheckBoxLoopForever.Text = "Loop Fore&ver";
            this.m_CheckBoxLoopForever.UseVisualStyleBackColor = false;
            this.m_CheckBoxLoopForever.CheckedChanged += new System.EventHandler(this.m_CheckBoxLoopForever_CheckedChanged);
            // 
            // m_TabPagePassFailResult
            // 
            this.m_TabPagePassFailResult.AutoScroll = true;
            this.m_TabPagePassFailResult.BackColor = System.Drawing.SystemColors.Window;
            this.m_TabPagePassFailResult.Controls.Add(this.m_PanelWindowsHelpPassFailResult);
            this.m_TabPagePassFailResult.Controls.Add(this.m_PanelDataGridViewPassFailResult);
            this.m_TabPagePassFailResult.Location = new System.Drawing.Point(4, 22);
            this.m_TabPagePassFailResult.Name = "m_TabPagePassFailResult";
            this.m_TabPagePassFailResult.Padding = new System.Windows.Forms.Padding(12);
            this.m_TabPagePassFailResult.Size = new System.Drawing.Size(1192, 584);
            this.m_TabPagePassFailResult.TabIndex = 2;
            this.m_TabPagePassFailResult.Text = "Result [Pass/Fail Format]";
            // 
            // m_PanelWindowsHelpPassFailResult
            // 
            this.m_PanelWindowsHelpPassFailResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_PanelWindowsHelpPassFailResult.Location = new System.Drawing.Point(492, 12);
            this.m_PanelWindowsHelpPassFailResult.Name = "m_PanelWindowsHelpPassFailResult";
            this.m_PanelWindowsHelpPassFailResult.Size = new System.Drawing.Size(400, 560);
            this.m_PanelWindowsHelpPassFailResult.TabIndex = 0;
            // 
            // m_PanelDataGridViewPassFailResult
            // 
            this.m_PanelDataGridViewPassFailResult.AutoScroll = true;
            this.m_PanelDataGridViewPassFailResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_PanelDataGridViewPassFailResult.Controls.Add(this.m_DataGridViewPassFailResult);
            this.m_PanelDataGridViewPassFailResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_PanelDataGridViewPassFailResult.Location = new System.Drawing.Point(12, 12);
            this.m_PanelDataGridViewPassFailResult.Name = "m_PanelDataGridViewPassFailResult";
            this.m_PanelDataGridViewPassFailResult.Size = new System.Drawing.Size(480, 560);
            this.m_PanelDataGridViewPassFailResult.TabIndex = 3;
            // 
            // m_DataGridViewPassFailResult
            // 
            this.m_DataGridViewPassFailResult.AllowUserToAddRows = false;
            this.m_DataGridViewPassFailResult.AllowUserToDeleteRows = false;
            this.m_DataGridViewPassFailResult.AllowUserToResizeColumns = false;
            this.m_DataGridViewPassFailResult.AllowUserToResizeRows = false;
            this.m_DataGridViewPassFailResult.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_DataGridViewPassFailResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_DataGridViewPassFailResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.m_DataGridViewPassFailResult.ColumnHeadersHeight = 28;
            this.m_DataGridViewPassFailResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.m_DataGridViewPassFailResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.m_DataGridViewPassFailResultTextBoxColumnTestNumber,
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase,
            this.m_DataGridViewPassFailResultTextBoxColumnDescription,
            this.m_DataGridViewPassFailResultTextBoxColumnResults,
            this.m_DataGridViewPassFailResultTextBoxColumnPassCount,
            this.m_DataGridViewPassFailResultTextBoxColumnFailCount,
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount});
            this.m_DataGridViewPassFailResult.ContextMenuStrip = this.m_ContextMenuStripDataGridViewPassFailResult;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewPassFailResult.DefaultCellStyle = dataGridViewCellStyle4;
            this.m_DataGridViewPassFailResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DataGridViewPassFailResult.Location = new System.Drawing.Point(0, 0);
            this.m_DataGridViewPassFailResult.MultiSelect = false;
            this.m_DataGridViewPassFailResult.Name = "m_DataGridViewPassFailResult";
            this.m_DataGridViewPassFailResult.ReadOnly = true;
            this.m_DataGridViewPassFailResult.RowHeadersVisible = false;
            this.m_DataGridViewPassFailResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_DataGridViewPassFailResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_DataGridViewPassFailResult.ShowCellToolTips = false;
            this.m_DataGridViewPassFailResult.Size = new System.Drawing.Size(478, 558);
            this.m_DataGridViewPassFailResult.StandardTab = true;
            this.m_DataGridViewPassFailResult.TabIndex = 2;
            this.m_DataGridViewPassFailResult.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_DataGridViewPassFailResult_CellContentDoubleClick);
            this.m_DataGridViewPassFailResult.SelectionChanged += new System.EventHandler(this.m_DataGridView_SelectionChanged);
            // 
            // m_DataGridViewPassFailResultTextBoxColumnTestNumber
            // 
            this.m_DataGridViewPassFailResultTextBoxColumnTestNumber.HeaderText = "Test No.";
            this.m_DataGridViewPassFailResultTextBoxColumnTestNumber.Name = "m_DataGridViewPassFailResultTextBoxColumnTestNumber";
            this.m_DataGridViewPassFailResultTextBoxColumnTestNumber.ReadOnly = true;
            this.m_DataGridViewPassFailResultTextBoxColumnTestNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewPassFailResultTextBoxColumnTestNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewPassFailResultTextBoxColumnTestNumber.ToolTipText = "The self test identifier associated with the test.";
            this.m_DataGridViewPassFailResultTextBoxColumnTestNumber.Width = 60;
            // 
            // m_DataGridViewPassFailResultTextBoxColumnTestCase
            // 
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase.HeaderText = "Test Case";
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase.Name = "m_DataGridViewPassFailResultTextBoxColumnTestCase";
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase.ReadOnly = true;
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase.ToolTipText = "The test case value associated with the current result.";
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase.Visible = false;
            this.m_DataGridViewPassFailResultTextBoxColumnTestCase.Width = 5;
            // 
            // m_DataGridViewPassFailResultTextBoxColumnDescription
            // 
            this.m_DataGridViewPassFailResultTextBoxColumnDescription.HeaderText = "Description";
            this.m_DataGridViewPassFailResultTextBoxColumnDescription.Name = "m_DataGridViewPassFailResultTextBoxColumnDescription";
            this.m_DataGridViewPassFailResultTextBoxColumnDescription.ReadOnly = true;
            this.m_DataGridViewPassFailResultTextBoxColumnDescription.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewPassFailResultTextBoxColumnDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewPassFailResultTextBoxColumnDescription.ToolTipText = "A description of the test.";
            this.m_DataGridViewPassFailResultTextBoxColumnDescription.Width = 250;
            // 
            // m_DataGridViewPassFailResultTextBoxColumnResults
            // 
            this.m_DataGridViewPassFailResultTextBoxColumnResults.HeaderText = "Results";
            this.m_DataGridViewPassFailResultTextBoxColumnResults.Name = "m_DataGridViewPassFailResultTextBoxColumnResults";
            this.m_DataGridViewPassFailResultTextBoxColumnResults.ReadOnly = true;
            this.m_DataGridViewPassFailResultTextBoxColumnResults.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewPassFailResultTextBoxColumnResults.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewPassFailResultTextBoxColumnResults.ToolTipText = "The test result (Pass/Fail).";
            this.m_DataGridViewPassFailResultTextBoxColumnResults.Visible = false;
            this.m_DataGridViewPassFailResultTextBoxColumnResults.Width = 5;
            // 
            // m_DataGridViewPassFailResultTextBoxColumnPassCount
            // 
            this.m_DataGridViewPassFailResultTextBoxColumnPassCount.HeaderText = "# Pass";
            this.m_DataGridViewPassFailResultTextBoxColumnPassCount.Name = "m_DataGridViewPassFailResultTextBoxColumnPassCount";
            this.m_DataGridViewPassFailResultTextBoxColumnPassCount.ReadOnly = true;
            this.m_DataGridViewPassFailResultTextBoxColumnPassCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewPassFailResultTextBoxColumnPassCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewPassFailResultTextBoxColumnPassCount.ToolTipText = "The pass count for the current run.";
            this.m_DataGridViewPassFailResultTextBoxColumnPassCount.Width = 70;
            // 
            // m_DataGridViewPassFailResultTextBoxColumnFailCount
            // 
            this.m_DataGridViewPassFailResultTextBoxColumnFailCount.HeaderText = "# Fail";
            this.m_DataGridViewPassFailResultTextBoxColumnFailCount.Name = "m_DataGridViewPassFailResultTextBoxColumnFailCount";
            this.m_DataGridViewPassFailResultTextBoxColumnFailCount.ReadOnly = true;
            this.m_DataGridViewPassFailResultTextBoxColumnFailCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewPassFailResultTextBoxColumnFailCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewPassFailResultTextBoxColumnFailCount.ToolTipText = "The fail count for the current run.";
            this.m_DataGridViewPassFailResultTextBoxColumnFailCount.Width = 70;
            // 
            // m_DataGridViewPassFailResultTextBoxColumnExecutionCount
            // 
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount.HeaderText = "#";
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount.Name = "m_DataGridViewPassFailResultTextBoxColumnExecutionCount";
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount.ReadOnly = true;
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount.ToolTipText = "The number of times the test has been executed in the current run.";
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount.Visible = false;
            this.m_DataGridViewPassFailResultTextBoxColumnExecutionCount.Width = 5;
            // 
            // m_ContextMenuStripDataGridViewPassFailResult
            // 
            this.m_ContextMenuStripDataGridViewPassFailResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItemPassFailResultShowDefinition});
            this.m_ContextMenuStripDataGridViewPassFailResult.Name = "m_ContextMenuStripDataGridView";
            this.m_ContextMenuStripDataGridViewPassFailResult.Size = new System.Drawing.Size(184, 48);
            // 
            // m_ToolStripMenuItemPassFailResultShowDefinition
            // 
            this.m_ToolStripMenuItemPassFailResultShowDefinition.Image = global::SelfTest.Properties.Resources.Help;
            this.m_ToolStripMenuItemPassFailResultShowDefinition.Name = "m_ToolStripMenuItemPassFailResultShowDefinition";
            this.m_ToolStripMenuItemPassFailResultShowDefinition.Size = new System.Drawing.Size(183, 22);
            this.m_ToolStripMenuItemPassFailResultShowDefinition.Text = "Show Test &Definition";
            this.m_ToolStripMenuItemPassFailResultShowDefinition.Click += new System.EventHandler(this.m_ToolStripMenuItemPassFailResultShowDefinition_Click);
            // 
            // m_PanelDataGridViewTestList
            // 
            this.m_PanelDataGridViewTestList.AutoScroll = true;
            this.m_PanelDataGridViewTestList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_PanelDataGridViewTestList.Controls.Add(this.m_DataGridViewTestList);
            this.m_PanelDataGridViewTestList.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_PanelDataGridViewTestList.Location = new System.Drawing.Point(12, 12);
            this.m_PanelDataGridViewTestList.Name = "m_PanelDataGridViewTestList";
            this.m_PanelDataGridViewTestList.Size = new System.Drawing.Size(480, 560);
            this.m_PanelDataGridViewTestList.TabIndex = 0;
            // 
            // m_DataGridViewTestList
            // 
            this.m_DataGridViewTestList.AllowUserToAddRows = false;
            this.m_DataGridViewTestList.AllowUserToDeleteRows = false;
            this.m_DataGridViewTestList.AllowUserToResizeColumns = false;
            this.m_DataGridViewTestList.AllowUserToResizeRows = false;
            this.m_DataGridViewTestList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_DataGridViewTestList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_DataGridViewTestList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.m_DataGridViewTestList.ColumnHeadersHeight = 28;
            this.m_DataGridViewTestList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.m_DataGridViewTestList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.m_DataGridViewTestListTextBoxColumnTestNumber,
            this.m_DataGridViewTestListTextBoxColumnTestCase,
            this.m_DataGridViewTestListTextBoxColumnDescription,
            this.m_DataGridViewTestListTextBoxColumnResult,
            this.m_DataGridViewTestListTextBoxColumnPassCount,
            this.m_DataGridViewTestListTextBoxColumnFailCount,
            this.m_DataGridViewTestListTextBoxColumnExecutionCount});
            this.m_DataGridViewTestList.ContextMenuStrip = this.m_ContextMenuStripDataGridViewTestList;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewTestList.DefaultCellStyle = dataGridViewCellStyle6;
            this.m_DataGridViewTestList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DataGridViewTestList.Location = new System.Drawing.Point(0, 0);
            this.m_DataGridViewTestList.MultiSelect = false;
            this.m_DataGridViewTestList.Name = "m_DataGridViewTestList";
            this.m_DataGridViewTestList.ReadOnly = true;
            this.m_DataGridViewTestList.RowHeadersVisible = false;
            this.m_DataGridViewTestList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_DataGridViewTestList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_DataGridViewTestList.ShowCellToolTips = false;
            this.m_DataGridViewTestList.Size = new System.Drawing.Size(478, 558);
            this.m_DataGridViewTestList.StandardTab = true;
            this.m_DataGridViewTestList.TabIndex = 1;
            this.m_DataGridViewTestList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_DataGridViewTestList_CellContentDoubleClick);
            this.m_DataGridViewTestList.SelectionChanged += new System.EventHandler(this.m_DataGridView_SelectionChanged);
            // 
            // m_DataGridViewTestListTextBoxColumnTestNumber
            // 
            this.m_DataGridViewTestListTextBoxColumnTestNumber.HeaderText = "Test No.";
            this.m_DataGridViewTestListTextBoxColumnTestNumber.Name = "m_DataGridViewTestListTextBoxColumnTestNumber";
            this.m_DataGridViewTestListTextBoxColumnTestNumber.ReadOnly = true;
            this.m_DataGridViewTestListTextBoxColumnTestNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewTestListTextBoxColumnTestNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewTestListTextBoxColumnTestNumber.ToolTipText = "The self test identifier associated with the test.";
            this.m_DataGridViewTestListTextBoxColumnTestNumber.Width = 60;
            // 
            // m_DataGridViewTestListTextBoxColumnTestCase
            // 
            this.m_DataGridViewTestListTextBoxColumnTestCase.HeaderText = "Test Case";
            this.m_DataGridViewTestListTextBoxColumnTestCase.Name = "m_DataGridViewTestListTextBoxColumnTestCase";
            this.m_DataGridViewTestListTextBoxColumnTestCase.ReadOnly = true;
            this.m_DataGridViewTestListTextBoxColumnTestCase.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewTestListTextBoxColumnTestCase.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewTestListTextBoxColumnTestCase.ToolTipText = "The test case value associated with the current result.";
            this.m_DataGridViewTestListTextBoxColumnTestCase.Visible = false;
            this.m_DataGridViewTestListTextBoxColumnTestCase.Width = 5;
            // 
            // m_DataGridViewTestListTextBoxColumnDescription
            // 
            this.m_DataGridViewTestListTextBoxColumnDescription.HeaderText = "Description";
            this.m_DataGridViewTestListTextBoxColumnDescription.Name = "m_DataGridViewTestListTextBoxColumnDescription";
            this.m_DataGridViewTestListTextBoxColumnDescription.ReadOnly = true;
            this.m_DataGridViewTestListTextBoxColumnDescription.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewTestListTextBoxColumnDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewTestListTextBoxColumnDescription.ToolTipText = "A description of the test.";
            this.m_DataGridViewTestListTextBoxColumnDescription.Width = 390;
            // 
            // m_DataGridViewTestListTextBoxColumnResult
            // 
            this.m_DataGridViewTestListTextBoxColumnResult.HeaderText = "Result";
            this.m_DataGridViewTestListTextBoxColumnResult.Name = "m_DataGridViewTestListTextBoxColumnResult";
            this.m_DataGridViewTestListTextBoxColumnResult.ReadOnly = true;
            this.m_DataGridViewTestListTextBoxColumnResult.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewTestListTextBoxColumnResult.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewTestListTextBoxColumnResult.ToolTipText = "The test result (Pass/Fail).";
            this.m_DataGridViewTestListTextBoxColumnResult.Visible = false;
            this.m_DataGridViewTestListTextBoxColumnResult.Width = 5;
            // 
            // m_DataGridViewTestListTextBoxColumnPassCount
            // 
            this.m_DataGridViewTestListTextBoxColumnPassCount.HeaderText = "# Pass";
            this.m_DataGridViewTestListTextBoxColumnPassCount.Name = "m_DataGridViewTestListTextBoxColumnPassCount";
            this.m_DataGridViewTestListTextBoxColumnPassCount.ReadOnly = true;
            this.m_DataGridViewTestListTextBoxColumnPassCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewTestListTextBoxColumnPassCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewTestListTextBoxColumnPassCount.ToolTipText = "The pass count for the current run.";
            this.m_DataGridViewTestListTextBoxColumnPassCount.Visible = false;
            this.m_DataGridViewTestListTextBoxColumnPassCount.Width = 5;
            // 
            // m_DataGridViewTestListTextBoxColumnFailCount
            // 
            this.m_DataGridViewTestListTextBoxColumnFailCount.HeaderText = "# Fail";
            this.m_DataGridViewTestListTextBoxColumnFailCount.Name = "m_DataGridViewTestListTextBoxColumnFailCount";
            this.m_DataGridViewTestListTextBoxColumnFailCount.ReadOnly = true;
            this.m_DataGridViewTestListTextBoxColumnFailCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewTestListTextBoxColumnFailCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewTestListTextBoxColumnFailCount.ToolTipText = "The fail count for the current run.";
            this.m_DataGridViewTestListTextBoxColumnFailCount.Visible = false;
            this.m_DataGridViewTestListTextBoxColumnFailCount.Width = 5;
            // 
            // m_DataGridViewTestListTextBoxColumnExecutionCount
            // 
            this.m_DataGridViewTestListTextBoxColumnExecutionCount.HeaderText = "#";
            this.m_DataGridViewTestListTextBoxColumnExecutionCount.Name = "m_DataGridViewTestListTextBoxColumnExecutionCount";
            this.m_DataGridViewTestListTextBoxColumnExecutionCount.ReadOnly = true;
            this.m_DataGridViewTestListTextBoxColumnExecutionCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.m_DataGridViewTestListTextBoxColumnExecutionCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_DataGridViewTestListTextBoxColumnExecutionCount.ToolTipText = "The number of times the test has been executed in the current run.";
            this.m_DataGridViewTestListTextBoxColumnExecutionCount.Visible = false;
            this.m_DataGridViewTestListTextBoxColumnExecutionCount.Width = 5;
            // 
            // m_ContextMenuStripDataGridViewTestList
            // 
            this.m_ContextMenuStripDataGridViewTestList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItemTestListShowDefinition});
            this.m_ContextMenuStripDataGridViewTestList.Name = "m_ContextMenuStripDataGridView";
            this.m_ContextMenuStripDataGridViewTestList.Size = new System.Drawing.Size(184, 26);
            // 
            // m_ToolStripMenuItemTestListShowDefinition
            // 
            this.m_ToolStripMenuItemTestListShowDefinition.Image = global::SelfTest.Properties.Resources.Help;
            this.m_ToolStripMenuItemTestListShowDefinition.Name = "m_ToolStripMenuItemTestListShowDefinition";
            this.m_ToolStripMenuItemTestListShowDefinition.Size = new System.Drawing.Size(183, 22);
            this.m_ToolStripMenuItemTestListShowDefinition.Text = "Show Test &Definition";
            this.m_ToolStripMenuItemTestListShowDefinition.Click += new System.EventHandler(this.m_ToolStripMenuItemTestListShowDefinition_Click);
            // 
            // m_PanelWindowsHelpTestList
            // 
            this.m_PanelWindowsHelpTestList.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_PanelWindowsHelpTestList.Location = new System.Drawing.Point(492, 12);
            this.m_PanelWindowsHelpTestList.Name = "m_PanelWindowsHelpTestList";
            this.m_PanelWindowsHelpTestList.Size = new System.Drawing.Size(400, 560);
            this.m_PanelWindowsHelpTestList.TabIndex = 0;
            // 
            // m_ComboBoxTestList
            // 
            this.m_ComboBoxTestList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ComboBoxTestList.FormattingEnabled = true;
            this.m_ComboBoxTestList.Location = new System.Drawing.Point(963, 5);
            this.m_ComboBoxTestList.Name = "m_ComboBoxTestList";
            this.m_ComboBoxTestList.Size = new System.Drawing.Size(200, 21);
            this.m_ComboBoxTestList.TabIndex = 1;
            // 
            // m_LegendTestList
            // 
            this.m_LegendTestList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_LegendTestList.AutoSize = true;
            this.m_LegendTestList.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendTestList.Location = new System.Drawing.Point(908, 8);
            this.m_LegendTestList.Name = "m_LegendTestList";
            this.m_LegendTestList.Size = new System.Drawing.Size(50, 13);
            this.m_LegendTestList.TabIndex = 0;
            this.m_LegendTestList.Text = "Test List:";
            // 
            // FormViewTestResults
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormViewTestResults";
            this.Text = "Display Self Test Results";
            this.Shown += new System.EventHandler(this.FormViewTestResults_Shown);
            this.Controls.SetChildIndex(this.m_PanelInformation, 0);
            this.Controls.SetChildIndex(this.m_TabControl, 0);
            this.m_TabControl.ResumeLayout(false);
            this.m_PanelInformation.ResumeLayout(false);
            this.m_PanelInformation.PerformLayout();
            this.m_TabPage1.ResumeLayout(false);
            this.m_TabPageListResult.ResumeLayout(false);
            this.m_PanelWindowsHelpListResult.ResumeLayout(false);
            this.m_GroupBoxInteractiveTest.ResumeLayout(false);
            this.m_GroupBoxInteractiveTest.PerformLayout();
            this.m_PanelInteractiveTestVCUCommands.ResumeLayout(false);
            this.m_PanelInteractiveTestVCUCommands.PerformLayout();
            this.m_ToolStripInteractiveTestVCUCommands.ResumeLayout(false);
            this.m_ToolStripInteractiveTestVCUCommands.PerformLayout();
            this.m_PanelInteractiveTestVariableHeader.ResumeLayout(false);
            this.m_PanelDataGridViewListResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewListResult)).EndInit();
            this.m_ContextMenuStripDataGridViewListResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLoopCount)).EndInit();
            this.m_TabPagePassFailResult.ResumeLayout(false);
            this.m_PanelDataGridViewPassFailResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewPassFailResult)).EndInit();
            this.m_ContextMenuStripDataGridViewPassFailResult.ResumeLayout(false);
            this.m_PanelDataGridViewTestList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_DataGridViewTestList)).EndInit();
            this.m_ContextMenuStripDataGridViewTestList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.NumericUpDown m_NumericUpDownLoopCount;
        private System.Windows.Forms.Label m_LegendLoopCount;
        private System.Windows.Forms.CheckBox m_CheckBoxLoopForever;

        private System.Windows.Forms.ContextMenuStrip m_ContextMenuStripDataGridViewTestList;
        private System.Windows.Forms.ContextMenuStrip m_ContextMenuStripDataGridViewListResult;
        private System.Windows.Forms.ContextMenuStrip m_ContextMenuStripDataGridViewPassFailResult;

        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemTestListShowDefinition;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemListResultShowDefinition;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemPassFailResultShowDefinition;

        private System.Windows.Forms.Panel m_PanelDataGridViewTestList;
        private System.Windows.Forms.DataGridView m_DataGridViewTestList;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTestListTextBoxColumnTestNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTestListTextBoxColumnTestCase;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTestListTextBoxColumnDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTestListTextBoxColumnResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTestListTextBoxColumnPassCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTestListTextBoxColumnFailCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewTestListTextBoxColumnExecutionCount;

        private System.Windows.Forms.TabPage m_TabPageListResult;
        private System.Windows.Forms.Panel m_PanelDataGridViewListResult;
        private System.Windows.Forms.DataGridView m_DataGridViewListResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewListResultTextBoxColumnTestNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewListResultTextBoxColumnTestCase;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewListResultTextBoxColumnDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewListResultTextBoxColumnResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewListResultTextBoxColumnPassCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewListResultTextBoxColumnFailCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewListResultTextBoxColumnExecutionCount;

        private System.Windows.Forms.TabPage m_TabPagePassFailResult;
        private System.Windows.Forms.Panel m_PanelDataGridViewPassFailResult;
        private System.Windows.Forms.DataGridView m_DataGridViewPassFailResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewPassFailResultTextBoxColumnTestNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewPassFailResultTextBoxColumnTestCase;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewPassFailResultTextBoxColumnDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewPassFailResultTextBoxColumnResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewPassFailResultTextBoxColumnPassCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewPassFailResultTextBoxColumnFailCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_DataGridViewPassFailResultTextBoxColumnExecutionCount;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItemListResultFaultSummary;
        private System.Windows.Forms.Panel m_PanelWindowsHelpTestList;
        private System.Windows.Forms.Panel m_PanelWindowsHelpPassFailResult;
        private System.Windows.Forms.Panel m_PanelWindowsHelpListResult;
        private System.Windows.Forms.Label m_LegendTestList;
        private System.Windows.Forms.ComboBox m_ComboBoxTestList;
        private System.Windows.Forms.GroupBox m_GroupBoxInteractiveTest;
        private System.Windows.Forms.Panel m_PanelInteractiveTestHelp;
        private System.Windows.Forms.Panel m_PanelInteractiveTestVCUCommands;
        private System.Windows.Forms.Panel m_PanelInteractiveTestVariableList;
        private System.Windows.Forms.Panel m_PanelInteractiveTestVariableHeader;
        private System.Windows.Forms.Label m_LegendSelfTestVariableValues;
        private System.Windows.Forms.ToolStrip m_ToolStripInteractiveTestVCUCommands;
        private System.Windows.Forms.ToolStripButton m_ToolStripButtonInteractiveTestContinue;
        private System.Windows.Forms.ToolStripSeparator m_ToolStripSeparatorInteractiveTestContinue;
        private System.Windows.Forms.ToolStripButton m_ToolStripButtonInteractiveTestAbort;
        private System.Windows.Forms.ToolStripSeparator m_ToolStripSeparatorInteractiveTestAbort;
    }
}