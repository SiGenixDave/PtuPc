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
 *  Project:    Watch
 * 
 *  File name:  FormWorksetDefineChartRecorder.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/11/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

namespace Watch.Forms
{
    partial class FormWorksetDefineChartRecorder
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
            this.m_ListBoxChartScaleMin = new System.Windows.Forms.ListBox();
            this.m_ListBoxChartScaleMax = new System.Windows.Forms.ListBox();
            this.m_ListBox1ChartScaleMaxHeader = new System.Windows.Forms.ListBox();
            this.m_ListBox1UnitsHeader = new System.Windows.Forms.ListBox();
            this.m_ListBoxUnits = new System.Windows.Forms.ListBox();
            this.m_ListBox1ChartScaleMinHeader = new System.Windows.Forms.ListBox();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxWorkset.SuspendLayout();
            this.m_TabControlWorkset.SuspendLayout();
            this.m_TabPageColumn1.SuspendLayout();
            this.m_TabPageColumn2.SuspendLayout();
            this.m_TabPageColumn3.SuspendLayout();
            this.m_GroupBoxAvailable.SuspendLayout();
            this.m_TabControlAvailable.SuspendLayout();
            this.m_TabPageAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.Size = new System.Drawing.Size(872, 530);
            // 
            // m_LegendSecurity
            // 
            this.m_LegendSecurity.Location = new System.Drawing.Point(559, 20);
            // 
            // m_TextBoxSecurityLevel
            // 
            this.m_TextBoxSecurityLevel.Location = new System.Drawing.Point(629, 17);
            // 
            // m_GroupBoxWorkset
            // 
            this.m_GroupBoxWorkset.Size = new System.Drawing.Size(532, 461);
            this.m_GroupBoxWorkset.Text = "Chart Recorder Watch Variables";
            // 
            // m_TabControlWorkset
            // 
            this.m_TabControlWorkset.Size = new System.Drawing.Size(457, 235);
            // 
            // m_TabPageColumn1
            // 
            this.m_TabPageColumn1.Controls.Add(this.m_ListBoxUnits);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBoxChartScaleMax);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBoxChartScaleMin);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBox1UnitsHeader);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBox1ChartScaleMaxHeader);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBox1ChartScaleMinHeader);
            this.m_TabPageColumn1.Size = new System.Drawing.Size(449, 209);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1ChartScaleMinHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1ChartScaleMaxHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1UnitsHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBoxChartScaleMin, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBoxChartScaleMax, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBoxUnits, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1RowHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelCount1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_TextBoxHeader1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LegendHeader1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1, 0);
            // 
            // m_TabPageColumn2
            // 
            this.m_TabPageColumn2.Size = new System.Drawing.Size(449, 209);
            // 
            // m_TabPageColumn3
            // 
            this.m_TabPageColumn3.Size = new System.Drawing.Size(449, 209);
            // 
            // m_ListBox1
            // 
            this.m_ListBox1.Size = new System.Drawing.Size(237, 108);
            this.m_ListBox1.TabIndex = 1;
            // 
            // m_LabelCount1
            // 
            this.m_LabelCount1.Location = new System.Drawing.Point(12, 182);
            // 
            // m_LabelCountTotal
            // 
            this.m_LabelCountTotal.Location = new System.Drawing.Point(28, 258);
            this.m_LabelCountTotal.Visible = false;
            // 
            // m_LegendHeader1
            // 
            this.m_LegendHeader1.Visible = false;
            // 
            // m_TextBoxHeader1
            // 
            this.m_TextBoxHeader1.Visible = false;
            // 
            // m_GroupBoxAvailable
            // 
            this.m_GroupBoxAvailable.Location = new System.Drawing.Point(550, 54);
            // 
            // m_ButtonRemove
            // 
            this.m_ButtonRemove.Location = new System.Drawing.Point(385, 261);
            // 
            // m_ButtonMoveUp
            // 
            this.m_ButtonMoveUp.Location = new System.Drawing.Point(474, 122);
            // 
            // m_ButtonMoveDown
            // 
            this.m_ButtonMoveDown.Location = new System.Drawing.Point(474, 168);
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Location = new System.Drawing.Point(629, 541);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.Location = new System.Drawing.Point(706, 540);
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(783, 541);
            // 
            // m_ListBoxChartScaleMin
            // 
            this.m_ListBoxChartScaleMin.BackColor = System.Drawing.SystemColors.Menu;
            this.m_ListBoxChartScaleMin.FormattingEnabled = true;
            this.m_ListBoxChartScaleMin.Location = new System.Drawing.Point(271, 71);
            this.m_ListBoxChartScaleMin.Name = "m_ListBoxChartScaleMin";
            this.m_ListBoxChartScaleMin.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBoxChartScaleMin.Size = new System.Drawing.Size(50, 108);
            this.m_ListBoxChartScaleMin.TabIndex = 0;
            this.m_ListBoxChartScaleMin.TabStop = false;
            // 
            // m_ListBoxChartScaleMax
            // 
            this.m_ListBoxChartScaleMax.BackColor = System.Drawing.SystemColors.Menu;
            this.m_ListBoxChartScaleMax.FormattingEnabled = true;
            this.m_ListBoxChartScaleMax.Location = new System.Drawing.Point(320, 71);
            this.m_ListBoxChartScaleMax.Name = "m_ListBoxChartScaleMax";
            this.m_ListBoxChartScaleMax.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBoxChartScaleMax.Size = new System.Drawing.Size(50, 108);
            this.m_ListBoxChartScaleMax.TabIndex = 0;
            this.m_ListBoxChartScaleMax.TabStop = false;
            // 
            // m_ListBox1ChartScaleMaxHeader
            // 
            this.m_ListBox1ChartScaleMaxHeader.BackColor = System.Drawing.SystemColors.Menu;
            this.m_ListBox1ChartScaleMaxHeader.FormattingEnabled = true;
            this.m_ListBox1ChartScaleMaxHeader.Items.AddRange(new object[] {
            "Max."});
            this.m_ListBox1ChartScaleMaxHeader.Location = new System.Drawing.Point(320, 50);
            this.m_ListBox1ChartScaleMaxHeader.Name = "m_ListBox1ChartScaleMaxHeader";
            this.m_ListBox1ChartScaleMaxHeader.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBox1ChartScaleMaxHeader.Size = new System.Drawing.Size(50, 30);
            this.m_ListBox1ChartScaleMaxHeader.TabIndex = 0;
            this.m_ListBox1ChartScaleMaxHeader.TabStop = false;
            // 
            // m_ListBox1UnitsHeader
            // 
            this.m_ListBox1UnitsHeader.BackColor = System.Drawing.SystemColors.Menu;
            this.m_ListBox1UnitsHeader.FormattingEnabled = true;
            this.m_ListBox1UnitsHeader.Items.AddRange(new object[] {
            "Units"});
            this.m_ListBox1UnitsHeader.Location = new System.Drawing.Point(369, 50);
            this.m_ListBox1UnitsHeader.Name = "m_ListBox1UnitsHeader";
            this.m_ListBox1UnitsHeader.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBox1UnitsHeader.Size = new System.Drawing.Size(60, 30);
            this.m_ListBox1UnitsHeader.TabIndex = 0;
            this.m_ListBox1UnitsHeader.TabStop = false;
            // 
            // m_ListBoxUnits
            // 
            this.m_ListBoxUnits.BackColor = System.Drawing.SystemColors.Menu;
            this.m_ListBoxUnits.FormattingEnabled = true;
            this.m_ListBoxUnits.Location = new System.Drawing.Point(369, 71);
            this.m_ListBoxUnits.Name = "m_ListBoxUnits";
            this.m_ListBoxUnits.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBoxUnits.Size = new System.Drawing.Size(60, 108);
            this.m_ListBoxUnits.TabIndex = 0;
            this.m_ListBoxUnits.TabStop = false;
            // 
            // m_ListBox1ChartScaleMinHeader
            // 
            this.m_ListBox1ChartScaleMinHeader.BackColor = System.Drawing.SystemColors.Menu;
            this.m_ListBox1ChartScaleMinHeader.FormattingEnabled = true;
            this.m_ListBox1ChartScaleMinHeader.Items.AddRange(new object[] {
            "Min."});
            this.m_ListBox1ChartScaleMinHeader.Location = new System.Drawing.Point(271, 50);
            this.m_ListBox1ChartScaleMinHeader.Name = "m_ListBox1ChartScaleMinHeader";
            this.m_ListBox1ChartScaleMinHeader.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBox1ChartScaleMinHeader.Size = new System.Drawing.Size(50, 30);
            this.m_ListBox1ChartScaleMinHeader.TabIndex = 0;
            this.m_ListBox1ChartScaleMinHeader.TabStop = false;
            // 
            // FormWorksetDefineChartRecorder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(872, 581);
            this.Name = "FormWorksetDefineChartRecorder";
            this.Text = "Create / Edit - Chart Recorder Workset";
            this.Controls.SetChildIndex(this.m_ButtonApply, 0);
            this.Controls.SetChildIndex(this.m_ButtonOK, 0);
            this.Controls.SetChildIndex(this.m_ButtonCancel, 0);
            this.Controls.SetChildIndex(this.m_PanelOuter, 0);
            this.m_PanelOuter.ResumeLayout(false);
            this.m_PanelOuter.PerformLayout();
            this.m_GroupBoxWorkset.ResumeLayout(false);
            this.m_GroupBoxWorkset.PerformLayout();
            this.m_TabControlWorkset.ResumeLayout(false);
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
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ListBox m_ListBoxChartScaleMax;
        private System.Windows.Forms.ListBox m_ListBoxChartScaleMin;
        private System.Windows.Forms.ListBox m_ListBox1UnitsHeader;
        private System.Windows.Forms.ListBox m_ListBox1ChartScaleMaxHeader;
        private System.Windows.Forms.ListBox m_ListBoxUnits;
        private System.Windows.Forms.ListBox m_ListBox1ChartScaleMinHeader;
    }
}