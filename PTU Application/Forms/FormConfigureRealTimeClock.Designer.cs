#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    PTU Application
 * 
 *  File name:  FormConfigureDateTime.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/31/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

namespace Bombardier.PTU.Forms
{
    partial class FormConfigureRealTimeClock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfigureRealTimeClock));
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_GroupBoxVCUDateTime = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelDateTime = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelTime = new System.Windows.Forms.Label();
            this.m_LabelDate = new System.Windows.Forms.Label();
            this.m_LegendTime = new System.Windows.Forms.Label();
            this.m_LegendDate = new System.Windows.Forms.Label();
            this.m_GroupBoxConfigureDateTime = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelDateTimePicker = new System.Windows.Forms.TableLayoutPanel();
            this.m_DateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.m_DateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.m_RadioButtonAuto = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonManual = new System.Windows.Forms.RadioButton();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_ButtonApply = new System.Windows.Forms.Button();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxVCUDateTime.SuspendLayout();
            this.m_TableLayoutPanelDateTime.SuspendLayout();
            this.m_GroupBoxConfigureDateTime.SuspendLayout();
            this.m_TableLayoutPanelDateTimePicker.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxVCUDateTime);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxConfigureDateTime);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_PanelOuter.Size = new System.Drawing.Size(345, 245);
            this.m_PanelOuter.TabIndex = 2;
            this.m_PanelOuter.TabStop = true;
            // 
            // m_GroupBoxVCUDateTime
            // 
            this.m_GroupBoxVCUDateTime.Controls.Add(this.m_TableLayoutPanelDateTime);
            this.m_GroupBoxVCUDateTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupBoxVCUDateTime.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxVCUDateTime.Name = "m_GroupBoxVCUDateTime";
            this.m_GroupBoxVCUDateTime.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxVCUDateTime.Size = new System.Drawing.Size(325, 90);
            this.m_GroupBoxVCUDateTime.TabIndex = 0;
            this.m_GroupBoxVCUDateTime.TabStop = false;
            this.m_GroupBoxVCUDateTime.Text = "Vehicle Control Unit Date/Time";
            // 
            // m_TableLayoutPanelDateTime
            // 
            this.m_TableLayoutPanelDateTime.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelDateTime.ColumnCount = 2;
            this.m_TableLayoutPanelDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.m_TableLayoutPanelDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LabelTime, 1, 1);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LabelDate, 1, 0);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LegendTime, 0, 1);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LegendDate, 0, 0);
            this.m_TableLayoutPanelDateTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelDateTime.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelDateTime.Margin = new System.Windows.Forms.Padding(5);
            this.m_TableLayoutPanelDateTime.Name = "m_TableLayoutPanelDateTime";
            this.m_TableLayoutPanelDateTime.RowCount = 2;
            this.m_TableLayoutPanelDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDateTime.Size = new System.Drawing.Size(305, 55);
            this.m_TableLayoutPanelDateTime.TabIndex = 0;
            // 
            // m_LabelTime
            // 
            this.m_LabelTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelTime.AutoSize = true;
            this.m_LabelTime.Location = new System.Drawing.Point(117, 34);
            this.m_LabelTime.Name = "m_LabelTime";
            this.m_LabelTime.Size = new System.Drawing.Size(0, 13);
            this.m_LabelTime.TabIndex = 4;
            // 
            // m_LabelDate
            // 
            this.m_LabelDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelDate.AutoSize = true;
            this.m_LabelDate.Location = new System.Drawing.Point(117, 7);
            this.m_LabelDate.Name = "m_LabelDate";
            this.m_LabelDate.Size = new System.Drawing.Size(0, 13);
            this.m_LabelDate.TabIndex = 3;
            // 
            // m_LegendTime
            // 
            this.m_LegendTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendTime.AutoSize = true;
            this.m_LegendTime.Location = new System.Drawing.Point(4, 34);
            this.m_LegendTime.Name = "m_LegendTime";
            this.m_LegendTime.Size = new System.Drawing.Size(33, 13);
            this.m_LegendTime.TabIndex = 0;
            this.m_LegendTime.Text = "Time:";
            // 
            // m_LegendDate
            // 
            this.m_LegendDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendDate.AutoSize = true;
            this.m_LegendDate.Location = new System.Drawing.Point(4, 7);
            this.m_LegendDate.Name = "m_LegendDate";
            this.m_LegendDate.Size = new System.Drawing.Size(33, 13);
            this.m_LegendDate.TabIndex = 0;
            this.m_LegendDate.Text = "Date:";
            // 
            // m_GroupBoxConfigureDateTime
            // 
            this.m_GroupBoxConfigureDateTime.Controls.Add(this.m_TableLayoutPanelDateTimePicker);
            this.m_GroupBoxConfigureDateTime.Controls.Add(this.m_RadioButtonAuto);
            this.m_GroupBoxConfigureDateTime.Controls.Add(this.m_RadioButtonManual);
            this.m_GroupBoxConfigureDateTime.Location = new System.Drawing.Point(10, 106);
            this.m_GroupBoxConfigureDateTime.Name = "m_GroupBoxConfigureDateTime";
            this.m_GroupBoxConfigureDateTime.Size = new System.Drawing.Size(325, 127);
            this.m_GroupBoxConfigureDateTime.TabIndex = 1;
            this.m_GroupBoxConfigureDateTime.TabStop = false;
            this.m_GroupBoxConfigureDateTime.Text = "Configure Date and Time";
            // 
            // m_TableLayoutPanelDateTimePicker
            // 
            this.m_TableLayoutPanelDateTimePicker.AutoSize = true;
            this.m_TableLayoutPanelDateTimePicker.ColumnCount = 1;
            this.m_TableLayoutPanelDateTimePicker.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_TableLayoutPanelDateTimePicker.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_TableLayoutPanelDateTimePicker.Controls.Add(this.m_DateTimePickerDate, 0, 0);
            this.m_TableLayoutPanelDateTimePicker.Controls.Add(this.m_DateTimePickerTime, 0, 1);
            this.m_TableLayoutPanelDateTimePicker.Location = new System.Drawing.Point(10, 61);
            this.m_TableLayoutPanelDateTimePicker.Name = "m_TableLayoutPanelDateTimePicker";
            this.m_TableLayoutPanelDateTimePicker.RowCount = 2;
            this.m_TableLayoutPanelDateTimePicker.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_TableLayoutPanelDateTimePicker.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_TableLayoutPanelDateTimePicker.Size = new System.Drawing.Size(144, 54);
            this.m_TableLayoutPanelDateTimePicker.TabIndex = 2;
            this.m_TableLayoutPanelDateTimePicker.TabStop = true;
            // 
            // m_DateTimePickerDate
            // 
            this.m_DateTimePickerDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_DateTimePickerDate.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.m_DateTimePickerDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.m_DateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_DateTimePickerDate.Location = new System.Drawing.Point(3, 3);
            this.m_DateTimePickerDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_DateTimePickerDate.MaxDate = new System.DateTime(2069, 12, 31, 0, 0, 0, 0);
            this.m_DateTimePickerDate.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.m_DateTimePickerDate.Name = "m_DateTimePickerDate";
            this.m_DateTimePickerDate.Size = new System.Drawing.Size(138, 20);
            this.m_DateTimePickerDate.TabIndex = 1;
            this.m_DateTimePickerDate.ValueChanged += new System.EventHandler(this.m_DateTimePicker_ValueChanged);
            // 
            // m_DateTimePickerTime
            // 
            this.m_DateTimePickerTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_DateTimePickerTime.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.m_DateTimePickerTime.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.m_DateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.m_DateTimePickerTime.Location = new System.Drawing.Point(3, 30);
            this.m_DateTimePickerTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_DateTimePickerTime.MaxDate = new System.DateTime(2069, 12, 31, 0, 0, 0, 0);
            this.m_DateTimePickerTime.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.m_DateTimePickerTime.Name = "m_DateTimePickerTime";
            this.m_DateTimePickerTime.ShowUpDown = true;
            this.m_DateTimePickerTime.Size = new System.Drawing.Size(138, 20);
            this.m_DateTimePickerTime.TabIndex = 2;
            this.m_DateTimePickerTime.ValueChanged += new System.EventHandler(this.m_DateTimePicker_ValueChanged);
            // 
            // m_RadioButtonAuto
            // 
            this.m_RadioButtonAuto.AutoSize = true;
            this.m_RadioButtonAuto.Checked = true;
            this.m_RadioButtonAuto.Location = new System.Drawing.Point(10, 18);
            this.m_RadioButtonAuto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_RadioButtonAuto.Name = "m_RadioButtonAuto";
            this.m_RadioButtonAuto.Size = new System.Drawing.Size(122, 17);
            this.m_RadioButtonAuto.TabIndex = 1;
            this.m_RadioButtonAuto.TabStop = true;
            this.m_RadioButtonAuto.Text = "&Synchronize with PC";
            this.m_RadioButtonAuto.UseVisualStyleBackColor = true;
            this.m_RadioButtonAuto.CheckedChanged += new System.EventHandler(this.m_RadioButtonAuto_CheckedChanged);
            // 
            // m_RadioButtonManual
            // 
            this.m_RadioButtonManual.AutoSize = true;
            this.m_RadioButtonManual.Location = new System.Drawing.Point(10, 39);
            this.m_RadioButtonManual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_RadioButtonManual.Name = "m_RadioButtonManual";
            this.m_RadioButtonManual.Size = new System.Drawing.Size(98, 17);
            this.m_RadioButtonManual.TabIndex = 1;
            this.m_RadioButtonManual.Text = "&Manual Update";
            this.m_RadioButtonManual.UseVisualStyleBackColor = true;
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Location = new System.Drawing.Point(179, 256);
            this.m_ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            this.m_ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonCancel.TabIndex = 0;
            this.m_ButtonCancel.TabStop = false;
            this.m_ButtonCancel.Text = "Cancel";
            this.m_ButtonCancel.UseVisualStyleBackColor = true;
            this.m_ButtonCancel.Click += new System.EventHandler(this.m_ButtonCancel_Click);
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Location = new System.Drawing.Point(98, 256);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonOK.TabIndex = 1;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(260, 256);
            this.m_ButtonApply.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonApply.Name = "m_ButtonApply";
            this.m_ButtonApply.Size = new System.Drawing.Size(75, 23);
            this.m_ButtonApply.TabIndex = 0;
            this.m_ButtonApply.TabStop = false;
            this.m_ButtonApply.Text = "&Apply";
            this.m_ButtonApply.UseVisualStyleBackColor = true;
            this.m_ButtonApply.Click += new System.EventHandler(this.m_ButtonApply_Click);
            // 
            // FormConfigureDateTime
            // 
            this.AcceptButton = this.m_ButtonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(345, 292);
            this.Controls.Add(this.m_ButtonApply);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_ButtonCancel);
            this.Controls.Add(this.m_PanelOuter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormConfigureDateTime";
            this.Text = "Configure Date / Time";
            this.Shown += new System.EventHandler(this.FormConfigureDateTime_Shown);
            this.m_PanelOuter.ResumeLayout(false);
            this.m_GroupBoxVCUDateTime.ResumeLayout(false);
            this.m_TableLayoutPanelDateTime.ResumeLayout(false);
            this.m_TableLayoutPanelDateTime.PerformLayout();
            this.m_GroupBoxConfigureDateTime.ResumeLayout(false);
            this.m_GroupBoxConfigureDateTime.PerformLayout();
            this.m_TableLayoutPanelDateTimePicker.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_PanelOuter;
        private System.Windows.Forms.GroupBox m_GroupBoxConfigureDateTime;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelDateTimePicker;
        private System.Windows.Forms.DateTimePicker m_DateTimePickerDate;
        private System.Windows.Forms.DateTimePicker m_DateTimePickerTime;
        private System.Windows.Forms.RadioButton m_RadioButtonAuto;
        private System.Windows.Forms.RadioButton m_RadioButtonManual;
        private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.Button m_ButtonOK;
        private System.Windows.Forms.GroupBox m_GroupBoxVCUDateTime;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelDateTime;
        private System.Windows.Forms.Label m_LegendTime;
        private System.Windows.Forms.Label m_LegendDate;
        private System.Windows.Forms.Button m_ButtonApply;
        private System.Windows.Forms.Label m_LabelTime;
        private System.Windows.Forms.Label m_LabelDate;
    }
}