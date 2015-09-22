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
 *  File name:  FormChangeChartScale.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/18/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

namespace Watch.Forms
{
    /// <summary>
    /// Form to allow the user to modify the scaling of individual channels of the chart recorder.
    /// </summary>
    partial class FormChangeChartScale
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
            this.m_TableLayoutPanelNewChartScale = new System.Windows.Forms.TableLayoutPanel();
            this.m_LegendLowerLimit = new System.Windows.Forms.Label();
            this.m_TextBoxNewChartScaleMin = new System.Windows.Forms.TextBox();
            this.m_LabelUnitsMin = new System.Windows.Forms.Label();
            this.m_TextBoxNewChartScaleMax = new System.Windows.Forms.TextBox();
            this.m_LegendUpperLimit = new System.Windows.Forms.Label();
            this.m_LabelUnitsMax = new System.Windows.Forms.Label();
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_LabelDataFormat = new System.Windows.Forms.Label();
            this.m_LegendNewChartScale = new System.Windows.Forms.Label();
            this.m_LegendDefaultChartScale = new System.Windows.Forms.Label();
            this.m_TableLayoutPanelDefaultChartScale = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelDefaultChartScaleMin = new System.Windows.Forms.Label();
            this.m_LabelDefaultUnits = new System.Windows.Forms.Label();
            this.m_LegendTo = new System.Windows.Forms.Label();
            this.m_LabelDefaultChartScaleMax = new System.Windows.Forms.Label();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonApply = new System.Windows.Forms.Button();
            this.m_TableLayoutPanelNewChartScale.SuspendLayout();
            this.m_PanelOuter.SuspendLayout();
            this.m_TableLayoutPanelDefaultChartScale.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_TableLayoutPanelNewChartScale
            // 
            this.m_TableLayoutPanelNewChartScale.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelNewChartScale.ColumnCount = 3;
            this.m_TableLayoutPanelNewChartScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.m_TableLayoutPanelNewChartScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.m_TableLayoutPanelNewChartScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.m_TableLayoutPanelNewChartScale.Controls.Add(this.m_LegendLowerLimit, 0, 1);
            this.m_TableLayoutPanelNewChartScale.Controls.Add(this.m_TextBoxNewChartScaleMin, 1, 1);
            this.m_TableLayoutPanelNewChartScale.Controls.Add(this.m_LabelUnitsMin, 2, 1);
            this.m_TableLayoutPanelNewChartScale.Controls.Add(this.m_TextBoxNewChartScaleMax, 1, 0);
            this.m_TableLayoutPanelNewChartScale.Controls.Add(this.m_LegendUpperLimit, 0, 0);
            this.m_TableLayoutPanelNewChartScale.Controls.Add(this.m_LabelUnitsMax, 2, 0);
            this.m_TableLayoutPanelNewChartScale.Location = new System.Drawing.Point(10, 86);
            this.m_TableLayoutPanelNewChartScale.Name = "m_TableLayoutPanelNewChartScale";
            this.m_TableLayoutPanelNewChartScale.RowCount = 2;
            this.m_TableLayoutPanelNewChartScale.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelNewChartScale.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelNewChartScale.Size = new System.Drawing.Size(291, 64);
            this.m_TableLayoutPanelNewChartScale.TabIndex = 0;
            this.m_TableLayoutPanelNewChartScale.TabStop = true;
            // 
            // m_LegendLowerLimit
            // 
            this.m_LegendLowerLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_LegendLowerLimit.Location = new System.Drawing.Point(4, 32);
            this.m_LegendLowerLimit.Name = "m_LegendLowerLimit";
            this.m_LegendLowerLimit.Size = new System.Drawing.Size(91, 31);
            this.m_LegendLowerLimit.TabIndex = 0;
            this.m_LegendLowerLimit.Text = "Lower Limit:";
            this.m_LegendLowerLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_TextBoxNewChartScaleMin
            // 
            this.m_TextBoxNewChartScaleMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TextBoxNewChartScaleMin.Location = new System.Drawing.Point(121, 37);
            this.m_TextBoxNewChartScaleMin.Margin = new System.Windows.Forms.Padding(5);
            this.m_TextBoxNewChartScaleMin.Name = "m_TextBoxNewChartScaleMin";
            this.m_TextBoxNewChartScaleMin.Size = new System.Drawing.Size(96, 20);
            this.m_TextBoxNewChartScaleMin.TabIndex = 2;
            this.m_TextBoxNewChartScaleMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_TextBoxNewChartScaleMin.WordWrap = false;
            this.m_TextBoxNewChartScaleMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxM_KeyPress);
            this.m_TextBoxNewChartScaleMin.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // m_LabelUnitsMin
            // 
            this.m_LabelUnitsMin.Location = new System.Drawing.Point(226, 32);
            this.m_LabelUnitsMin.Name = "m_LabelUnitsMin";
            this.m_LabelUnitsMin.Size = new System.Drawing.Size(48, 30);
            this.m_LabelUnitsMin.TabIndex = 0;
            this.m_LabelUnitsMin.Text = " ... ";
            this.m_LabelUnitsMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_TextBoxNewChartScaleMax
            // 
            this.m_TextBoxNewChartScaleMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TextBoxNewChartScaleMax.Location = new System.Drawing.Point(121, 6);
            this.m_TextBoxNewChartScaleMax.Margin = new System.Windows.Forms.Padding(5);
            this.m_TextBoxNewChartScaleMax.Name = "m_TextBoxNewChartScaleMax";
            this.m_TextBoxNewChartScaleMax.Size = new System.Drawing.Size(96, 20);
            this.m_TextBoxNewChartScaleMax.TabIndex = 1;
            this.m_TextBoxNewChartScaleMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_TextBoxNewChartScaleMax.WordWrap = false;
            this.m_TextBoxNewChartScaleMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxM_KeyPress);
            this.m_TextBoxNewChartScaleMax.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // m_LegendUpperLimit
            // 
            this.m_LegendUpperLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_LegendUpperLimit.Location = new System.Drawing.Point(4, 1);
            this.m_LegendUpperLimit.Name = "m_LegendUpperLimit";
            this.m_LegendUpperLimit.Size = new System.Drawing.Size(91, 30);
            this.m_LegendUpperLimit.TabIndex = 0;
            this.m_LegendUpperLimit.Text = "Upper Limit:";
            this.m_LegendUpperLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelUnitsMax
            // 
            this.m_LabelUnitsMax.Location = new System.Drawing.Point(226, 1);
            this.m_LabelUnitsMax.Name = "m_LabelUnitsMax";
            this.m_LabelUnitsMax.Size = new System.Drawing.Size(48, 28);
            this.m_LabelUnitsMax.TabIndex = 0;
            this.m_LabelUnitsMax.Text = " ... ";
            this.m_LabelUnitsMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_LabelDataFormat);
            this.m_PanelOuter.Controls.Add(this.m_LegendNewChartScale);
            this.m_PanelOuter.Controls.Add(this.m_LegendDefaultChartScale);
            this.m_PanelOuter.Controls.Add(this.m_TableLayoutPanelDefaultChartScale);
            this.m_PanelOuter.Controls.Add(this.m_TableLayoutPanelNewChartScale);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Size = new System.Drawing.Size(311, 162);
            this.m_PanelOuter.TabIndex = 1;
            // 
            // m_LabelDataFormat
            // 
            this.m_LabelDataFormat.AutoSize = true;
            this.m_LabelDataFormat.Location = new System.Drawing.Point(140, 70);
            this.m_LabelDataFormat.Name = "m_LabelDataFormat";
            this.m_LabelDataFormat.Size = new System.Drawing.Size(13, 13);
            this.m_LabelDataFormat.TabIndex = 0;
            this.m_LabelDataFormat.Text = "()";
            // 
            // m_LegendNewChartScale
            // 
            this.m_LegendNewChartScale.AutoSize = true;
            this.m_LegendNewChartScale.Location = new System.Drawing.Point(10, 70);
            this.m_LegendNewChartScale.Name = "m_LegendNewChartScale";
            this.m_LegendNewChartScale.Size = new System.Drawing.Size(122, 13);
            this.m_LegendNewChartScale.TabIndex = 0;
            this.m_LegendNewChartScale.Text = "New Chart Scale Values";
            // 
            // m_LegendDefaultChartScale
            // 
            this.m_LegendDefaultChartScale.AutoSize = true;
            this.m_LegendDefaultChartScale.Location = new System.Drawing.Point(10, 10);
            this.m_LegendDefaultChartScale.Name = "m_LegendDefaultChartScale";
            this.m_LegendDefaultChartScale.Size = new System.Drawing.Size(179, 13);
            this.m_LegendDefaultChartScale.TabIndex = 0;
            this.m_LegendDefaultChartScale.Text = "Default Min/Max Chart Scale Values";
            // 
            // m_TableLayoutPanelDefaultChartScale
            // 
            this.m_TableLayoutPanelDefaultChartScale.AutoSize = true;
            this.m_TableLayoutPanelDefaultChartScale.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelDefaultChartScale.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelDefaultChartScale.ColumnCount = 4;
            this.m_TableLayoutPanelDefaultChartScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.m_TableLayoutPanelDefaultChartScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDefaultChartScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.m_TableLayoutPanelDefaultChartScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.m_TableLayoutPanelDefaultChartScale.Controls.Add(this.m_LabelDefaultChartScaleMin, 0, 0);
            this.m_TableLayoutPanelDefaultChartScale.Controls.Add(this.m_LabelDefaultUnits, 3, 0);
            this.m_TableLayoutPanelDefaultChartScale.Controls.Add(this.m_LegendTo, 1, 0);
            this.m_TableLayoutPanelDefaultChartScale.Controls.Add(this.m_LabelDefaultChartScaleMax, 2, 0);
            this.m_TableLayoutPanelDefaultChartScale.Location = new System.Drawing.Point(10, 26);
            this.m_TableLayoutPanelDefaultChartScale.Name = "m_TableLayoutPanelDefaultChartScale";
            this.m_TableLayoutPanelDefaultChartScale.RowCount = 1;
            this.m_TableLayoutPanelDefaultChartScale.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelDefaultChartScale.Size = new System.Drawing.Size(291, 32);
            this.m_TableLayoutPanelDefaultChartScale.TabIndex = 0;
            // 
            // m_LabelDefaultChartScaleMin
            // 
            this.m_LabelDefaultChartScaleMin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelDefaultChartScaleMin.AutoEllipsis = true;
            this.m_LabelDefaultChartScaleMin.AutoSize = true;
            this.m_LabelDefaultChartScaleMin.Location = new System.Drawing.Point(4, 9);
            this.m_LabelDefaultChartScaleMin.Name = "m_LabelDefaultChartScaleMin";
            this.m_LabelDefaultChartScaleMin.Size = new System.Drawing.Size(46, 13);
            this.m_LabelDefaultChartScaleMin.TabIndex = 0;
            this.m_LabelDefaultChartScaleMin.Text = "<Value>";
            this.m_LabelDefaultChartScaleMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LabelDefaultUnits
            // 
            this.m_LabelDefaultUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelDefaultUnits.AutoEllipsis = true;
            this.m_LabelDefaultUnits.AutoSize = true;
            this.m_LabelDefaultUnits.Location = new System.Drawing.Point(226, 9);
            this.m_LabelDefaultUnits.Name = "m_LabelDefaultUnits";
            this.m_LabelDefaultUnits.Size = new System.Drawing.Size(43, 13);
            this.m_LabelDefaultUnits.TabIndex = 0;
            this.m_LabelDefaultUnits.Text = "<Units>";
            this.m_LabelDefaultUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LegendTo
            // 
            this.m_LegendTo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_LegendTo.AutoEllipsis = true;
            this.m_LegendTo.AutoSize = true;
            this.m_LegendTo.Location = new System.Drawing.Point(103, 9);
            this.m_LegendTo.Name = "m_LegendTo";
            this.m_LegendTo.Size = new System.Drawing.Size(16, 13);
            this.m_LegendTo.TabIndex = 0;
            this.m_LegendTo.Text = "to";
            this.m_LegendTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelDefaultChartScaleMax
            // 
            this.m_LabelDefaultChartScaleMax.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelDefaultChartScaleMax.AutoEllipsis = true;
            this.m_LabelDefaultChartScaleMax.AutoSize = true;
            this.m_LabelDefaultChartScaleMax.Location = new System.Drawing.Point(128, 9);
            this.m_LabelDefaultChartScaleMax.Name = "m_LabelDefaultChartScaleMax";
            this.m_LabelDefaultChartScaleMax.Size = new System.Drawing.Size(46, 13);
            this.m_LabelDefaultChartScaleMax.TabIndex = 0;
            this.m_LabelDefaultChartScaleMax.Text = "<Value>";
            this.m_LabelDefaultChartScaleMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Location = new System.Drawing.Point(64, 173);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonOK.TabIndex = 2;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Location = new System.Drawing.Point(145, 173);
            this.m_ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            this.m_ButtonCancel.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonCancel.TabIndex = 0;
            this.m_ButtonCancel.TabStop = false;
            this.m_ButtonCancel.Text = "Cancel";
            this.m_ButtonCancel.UseVisualStyleBackColor = true;
            this.m_ButtonCancel.Click += new System.EventHandler(this.m_ButtonCancel_Click);
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Enabled = false;
            this.m_ButtonApply.Location = new System.Drawing.Point(226, 173);
            this.m_ButtonApply.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonApply.Name = "m_ButtonApply";
            this.m_ButtonApply.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonApply.TabIndex = 0;
            this.m_ButtonApply.TabStop = false;
            this.m_ButtonApply.Text = "&Apply";
            this.m_ButtonApply.UseVisualStyleBackColor = true;
            this.m_ButtonApply.Click += new System.EventHandler(this.m_ButtonApply_Click);
            // 
            // FormChangeChartScale
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(311, 207);
            this.Controls.Add(this.m_ButtonApply);
            this.Controls.Add(this.m_ButtonCancel);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_PanelOuter);
            this.Name = "FormChangeChartScale";
            this.Shown += new System.EventHandler(this.FormChangeChartScale_Shown);
            this.m_TableLayoutPanelNewChartScale.ResumeLayout(false);
            this.m_TableLayoutPanelNewChartScale.PerformLayout();
            this.m_PanelOuter.ResumeLayout(false);
            this.m_PanelOuter.PerformLayout();
            this.m_TableLayoutPanelDefaultChartScale.ResumeLayout(false);
            this.m_TableLayoutPanelDefaultChartScale.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelNewChartScale;
        private System.Windows.Forms.Label m_LegendLowerLimit;
        private System.Windows.Forms.Label m_LabelUnitsMax;
        private System.Windows.Forms.Label m_LegendUpperLimit;
        private System.Windows.Forms.Label m_LabelUnitsMin;
        private System.Windows.Forms.Panel m_PanelOuter;
        private System.Windows.Forms.Button m_ButtonOK;
        private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.Button m_ButtonApply;
        private System.Windows.Forms.TextBox m_TextBoxNewChartScaleMax;
        private System.Windows.Forms.TextBox m_TextBoxNewChartScaleMin;
        private System.Windows.Forms.Label m_LegendDefaultChartScale;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelDefaultChartScale;
        private System.Windows.Forms.Label m_LabelDefaultChartScaleMin;
        private System.Windows.Forms.Label m_LabelDefaultUnits;
        private System.Windows.Forms.Label m_LegendTo;
        private System.Windows.Forms.Label m_LabelDefaultChartScaleMax;
        private System.Windows.Forms.Label m_LegendNewChartScale;
        private System.Windows.Forms.Label m_LabelDataFormat;
    }
}