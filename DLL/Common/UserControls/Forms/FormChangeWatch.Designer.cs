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
 *  File name:  FormChangeWatch.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/21/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

namespace Common.UserControls
{
    partial class FormChangeWatch
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
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_GroupBoxNewValue = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelNewValue = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelNewValueUnits = new System.Windows.Forms.Label();
            this.m_GroupBoxCurrentValue = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelCurrentValue = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelCurrentValue = new System.Windows.Forms.Label();
            this.m_LabelCurrentValueUnits = new System.Windows.Forms.Label();
            this.m_GroupBoxAllowableRange = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelAllowableRange = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelAllowableRangeLowerValue = new System.Windows.Forms.Label();
            this.m_LabelAllowableRangeUnits = new System.Windows.Forms.Label();
            this.m_LegendTo = new System.Windows.Forms.Label();
            this.m_LabelAllowableRangeUpperValue = new System.Windows.Forms.Label();
            this.m_ButtonApply = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxNewValue.SuspendLayout();
            this.m_TableLayoutPanelNewValue.SuspendLayout();
            this.m_GroupBoxCurrentValue.SuspendLayout();
            this.m_TableLayoutPanelCurrentValue.SuspendLayout();
            this.m_GroupBoxAllowableRange.SuspendLayout();
            this.m_TableLayoutPanelAllowableRange.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxNewValue);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxCurrentValue);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxAllowableRange);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_PanelOuter.Size = new System.Drawing.Size(332, 235);
            this.m_PanelOuter.TabIndex = 1;
            this.m_PanelOuter.TabStop = true;
            // 
            // m_GroupBoxNewValue
            // 
            this.m_GroupBoxNewValue.Controls.Add(this.m_TableLayoutPanelNewValue);
            this.m_GroupBoxNewValue.Location = new System.Drawing.Point(10, 156);
            this.m_GroupBoxNewValue.Name = "m_GroupBoxNewValue";
            this.m_GroupBoxNewValue.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxNewValue.Size = new System.Drawing.Size(312, 67);
            this.m_GroupBoxNewValue.TabIndex = 0;
            this.m_GroupBoxNewValue.TabStop = false;
            this.m_GroupBoxNewValue.Text = "New Value";
            // 
            // m_TableLayoutPanelNewValue
            // 
            this.m_TableLayoutPanelNewValue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelNewValue.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelNewValue.ColumnCount = 2;
            this.m_TableLayoutPanelNewValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 221F));
            this.m_TableLayoutPanelNewValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.m_TableLayoutPanelNewValue.Controls.Add(this.m_LabelNewValueUnits, 1, 0);
            this.m_TableLayoutPanelNewValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelNewValue.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelNewValue.Name = "m_TableLayoutPanelNewValue";
            this.m_TableLayoutPanelNewValue.RowCount = 1;
            this.m_TableLayoutPanelNewValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelNewValue.Size = new System.Drawing.Size(292, 32);
            this.m_TableLayoutPanelNewValue.TabIndex = 1;
            // 
            // m_LabelNewValueUnits
            // 
            this.m_LabelNewValueUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelNewValueUnits.AutoEllipsis = true;
            this.m_LabelNewValueUnits.AutoSize = true;
            this.m_LabelNewValueUnits.Location = new System.Drawing.Point(226, 9);
            this.m_LabelNewValueUnits.Name = "m_LabelNewValueUnits";
            this.m_LabelNewValueUnits.Size = new System.Drawing.Size(43, 13);
            this.m_LabelNewValueUnits.TabIndex = 0;
            this.m_LabelNewValueUnits.Text = "<Units>";
            this.m_LabelNewValueUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_GroupBoxCurrentValue
            // 
            this.m_GroupBoxCurrentValue.Controls.Add(this.m_TableLayoutPanelCurrentValue);
            this.m_GroupBoxCurrentValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupBoxCurrentValue.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxCurrentValue.Name = "m_GroupBoxCurrentValue";
            this.m_GroupBoxCurrentValue.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxCurrentValue.Size = new System.Drawing.Size(312, 67);
            this.m_GroupBoxCurrentValue.TabIndex = 0;
            this.m_GroupBoxCurrentValue.TabStop = false;
            this.m_GroupBoxCurrentValue.Text = "Current Value";
            // 
            // m_TableLayoutPanelCurrentValue
            // 
            this.m_TableLayoutPanelCurrentValue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelCurrentValue.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelCurrentValue.ColumnCount = 2;
            this.m_TableLayoutPanelCurrentValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 221F));
            this.m_TableLayoutPanelCurrentValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.m_TableLayoutPanelCurrentValue.Controls.Add(this.m_LabelCurrentValue, 0, 0);
            this.m_TableLayoutPanelCurrentValue.Controls.Add(this.m_LabelCurrentValueUnits, 1, 0);
            this.m_TableLayoutPanelCurrentValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelCurrentValue.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelCurrentValue.Name = "m_TableLayoutPanelCurrentValue";
            this.m_TableLayoutPanelCurrentValue.RowCount = 1;
            this.m_TableLayoutPanelCurrentValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelCurrentValue.Size = new System.Drawing.Size(292, 32);
            this.m_TableLayoutPanelCurrentValue.TabIndex = 0;
            // 
            // m_LabelCurrentValue
            // 
            this.m_LabelCurrentValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelCurrentValue.AutoEllipsis = true;
            this.m_LabelCurrentValue.AutoSize = true;
            this.m_LabelCurrentValue.Location = new System.Drawing.Point(4, 9);
            this.m_LabelCurrentValue.Name = "m_LabelCurrentValue";
            this.m_LabelCurrentValue.Size = new System.Drawing.Size(46, 13);
            this.m_LabelCurrentValue.TabIndex = 0;
            this.m_LabelCurrentValue.Text = "<Value>";
            this.m_LabelCurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelCurrentValueUnits
            // 
            this.m_LabelCurrentValueUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelCurrentValueUnits.AutoEllipsis = true;
            this.m_LabelCurrentValueUnits.AutoSize = true;
            this.m_LabelCurrentValueUnits.Location = new System.Drawing.Point(226, 9);
            this.m_LabelCurrentValueUnits.Name = "m_LabelCurrentValueUnits";
            this.m_LabelCurrentValueUnits.Size = new System.Drawing.Size(43, 13);
            this.m_LabelCurrentValueUnits.TabIndex = 0;
            this.m_LabelCurrentValueUnits.Text = "<Units>";
            this.m_LabelCurrentValueUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_GroupBoxAllowableRange
            // 
            this.m_GroupBoxAllowableRange.Controls.Add(this.m_TableLayoutPanelAllowableRange);
            this.m_GroupBoxAllowableRange.Location = new System.Drawing.Point(10, 83);
            this.m_GroupBoxAllowableRange.Name = "m_GroupBoxAllowableRange";
            this.m_GroupBoxAllowableRange.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxAllowableRange.Size = new System.Drawing.Size(312, 67);
            this.m_GroupBoxAllowableRange.TabIndex = 0;
            this.m_GroupBoxAllowableRange.TabStop = false;
            this.m_GroupBoxAllowableRange.Text = "Allowable Range";
            // 
            // m_TableLayoutPanelAllowableRange
            // 
            this.m_TableLayoutPanelAllowableRange.AutoSize = true;
            this.m_TableLayoutPanelAllowableRange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelAllowableRange.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelAllowableRange.ColumnCount = 4;
            this.m_TableLayoutPanelAllowableRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.m_TableLayoutPanelAllowableRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelAllowableRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.m_TableLayoutPanelAllowableRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.m_TableLayoutPanelAllowableRange.Controls.Add(this.m_LabelAllowableRangeLowerValue, 0, 0);
            this.m_TableLayoutPanelAllowableRange.Controls.Add(this.m_LabelAllowableRangeUnits, 3, 0);
            this.m_TableLayoutPanelAllowableRange.Controls.Add(this.m_LegendTo, 1, 0);
            this.m_TableLayoutPanelAllowableRange.Controls.Add(this.m_LabelAllowableRangeUpperValue, 2, 0);
            this.m_TableLayoutPanelAllowableRange.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelAllowableRange.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelAllowableRange.Name = "m_TableLayoutPanelAllowableRange";
            this.m_TableLayoutPanelAllowableRange.RowCount = 1;
            this.m_TableLayoutPanelAllowableRange.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelAllowableRange.Size = new System.Drawing.Size(292, 32);
            this.m_TableLayoutPanelAllowableRange.TabIndex = 0;
            // 
            // m_LabelAllowableRangeLowerValue
            // 
            this.m_LabelAllowableRangeLowerValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelAllowableRangeLowerValue.AutoEllipsis = true;
            this.m_LabelAllowableRangeLowerValue.AutoSize = true;
            this.m_LabelAllowableRangeLowerValue.Location = new System.Drawing.Point(4, 9);
            this.m_LabelAllowableRangeLowerValue.Name = "m_LabelAllowableRangeLowerValue";
            this.m_LabelAllowableRangeLowerValue.Size = new System.Drawing.Size(46, 13);
            this.m_LabelAllowableRangeLowerValue.TabIndex = 0;
            this.m_LabelAllowableRangeLowerValue.Text = "<Value>";
            this.m_LabelAllowableRangeLowerValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelAllowableRangeUnits
            // 
            this.m_LabelAllowableRangeUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelAllowableRangeUnits.AutoEllipsis = true;
            this.m_LabelAllowableRangeUnits.AutoSize = true;
            this.m_LabelAllowableRangeUnits.Location = new System.Drawing.Point(226, 9);
            this.m_LabelAllowableRangeUnits.Name = "m_LabelAllowableRangeUnits";
            this.m_LabelAllowableRangeUnits.Size = new System.Drawing.Size(43, 13);
            this.m_LabelAllowableRangeUnits.TabIndex = 0;
            this.m_LabelAllowableRangeUnits.Text = "<Units>";
            this.m_LabelAllowableRangeUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // m_LabelAllowableRangeUpperValue
            // 
            this.m_LabelAllowableRangeUpperValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelAllowableRangeUpperValue.AutoEllipsis = true;
            this.m_LabelAllowableRangeUpperValue.AutoSize = true;
            this.m_LabelAllowableRangeUpperValue.Location = new System.Drawing.Point(128, 9);
            this.m_LabelAllowableRangeUpperValue.Name = "m_LabelAllowableRangeUpperValue";
            this.m_LabelAllowableRangeUpperValue.Size = new System.Drawing.Size(46, 13);
            this.m_LabelAllowableRangeUpperValue.TabIndex = 0;
            this.m_LabelAllowableRangeUpperValue.Text = "<Value>";
            this.m_LabelAllowableRangeUpperValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(247, 246);
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
            this.m_ButtonOK.Location = new System.Drawing.Point(89, 246);
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
            this.m_ButtonCancel.Location = new System.Drawing.Point(168, 246);
            this.m_ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            this.m_ButtonCancel.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonCancel.TabIndex = 0;
            this.m_ButtonCancel.TabStop = false;
            this.m_ButtonCancel.Text = "Cancel";
            this.m_ButtonCancel.UseVisualStyleBackColor = true;
            this.m_ButtonCancel.Click += new System.EventHandler(this.m_ButtonCancel_Click);
            // 
            // FormChangeWatch
            // 
            this.AcceptButton = this.m_ButtonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(332, 282);
            this.Controls.Add(this.m_ButtonApply);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_ButtonCancel);
            this.Controls.Add(this.m_PanelOuter);
            this.Name = "FormChangeWatch";
            this.m_PanelOuter.ResumeLayout(false);
            this.m_GroupBoxNewValue.ResumeLayout(false);
            this.m_TableLayoutPanelNewValue.ResumeLayout(false);
            this.m_TableLayoutPanelNewValue.PerformLayout();
            this.m_GroupBoxCurrentValue.ResumeLayout(false);
            this.m_TableLayoutPanelCurrentValue.ResumeLayout(false);
            this.m_TableLayoutPanelCurrentValue.PerformLayout();
            this.m_GroupBoxAllowableRange.ResumeLayout(false);
            this.m_GroupBoxAllowableRange.PerformLayout();
            this.m_TableLayoutPanelAllowableRange.ResumeLayout(false);
            this.m_TableLayoutPanelAllowableRange.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_GroupBoxNewValue;
        private System.Windows.Forms.Label m_LabelNewValueUnits;
        private System.Windows.Forms.GroupBox m_GroupBoxCurrentValue;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelCurrentValue;
        private System.Windows.Forms.Label m_LabelCurrentValue;
        private System.Windows.Forms.Label m_LabelCurrentValueUnits;
        private System.Windows.Forms.GroupBox m_GroupBoxAllowableRange;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelAllowableRange;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelNewValue;
        private System.Windows.Forms.Label m_LegendTo;
        private System.Windows.Forms.Label m_LabelAllowableRangeUnits;

        /// <summary>
        /// Referece to the outer <c>Panel</c>.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelOuter;

        /// <summary>
        /// Reference to to OK <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonOK;

        /// <summary>
        /// Reference to to Cancel <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonCancel;

        /// <summary>
        /// Reference to to Apply <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonApply;

        /// <summary>
        /// Reference to the <c>Label</c> used to display the value of the upper range.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelAllowableRangeUpperValue;

        /// <summary>
        /// Reference to the <c>Label</c> used to display the value of the lower range.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelAllowableRangeLowerValue;
    }
}