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
 *  File name:  FormChangeBitmask.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/21/10    1.0     K.McDonald      First Release.
 * 
 *  09/30/10    1.1     K.McD           1.  Added Alt key access to the '&New Value' group box.
 *  
 *  04/02/15    1.2     K.McD           1.  SNCR - R188 PTU [20 Mar 2015] Item 2. ItemModified the Size and Location properties of the TableLayoutPanel to accommodate
 *                                          the (Bitmask) label.
 *
 */
#endregion --- Revision History ---

namespace Common.UserControls
{
    partial class FormChangeBitmask
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
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonApply = new System.Windows.Forms.Button();
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_GroupBoxNewValue = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelNewValue = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelNewValueUnits = new System.Windows.Forms.Label();
            this.m_NumericUpDownNewValue = new System.Windows.Forms.NumericUpDown();
            this.m_GroupBoxCurrentValue = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelCurrentValue = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelCurrentValue = new System.Windows.Forms.Label();
            this.m_LabelCurrentValueUnits = new System.Windows.Forms.Label();
            this.m_GroupBoxFormat = new System.Windows.Forms.GroupBox();
            this.m_RadioButtonDecimal = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonHex = new System.Windows.Forms.RadioButton();
            this.m_GroupBoxBitValues = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelBitValues = new System.Windows.Forms.TableLayoutPanel();
            this.m_CheckBox00 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox01 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox02 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox03 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox04 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox05 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox06 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox07 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox08 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox09 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox10 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox11 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox12 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox13 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox14 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox15 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox16 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox17 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox18 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox19 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox20 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox21 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox22 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox23 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox24 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox25 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox26 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox27 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox28 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox29 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox30 = new System.Windows.Forms.CheckBox();
            this.m_CheckBox31 = new System.Windows.Forms.CheckBox();
            this.m_Legend00 = new System.Windows.Forms.Label();
            this.m_Legend01 = new System.Windows.Forms.Label();
            this.m_Legend02 = new System.Windows.Forms.Label();
            this.m_Legend03 = new System.Windows.Forms.Label();
            this.m_Legend04 = new System.Windows.Forms.Label();
            this.m_Legend05 = new System.Windows.Forms.Label();
            this.m_Legend06 = new System.Windows.Forms.Label();
            this.m_Legend07 = new System.Windows.Forms.Label();
            this.m_Legend08 = new System.Windows.Forms.Label();
            this.m_Legend09 = new System.Windows.Forms.Label();
            this.m_Legend10 = new System.Windows.Forms.Label();
            this.m_Legend11 = new System.Windows.Forms.Label();
            this.m_Legend12 = new System.Windows.Forms.Label();
            this.m_Legend13 = new System.Windows.Forms.Label();
            this.m_Legend14 = new System.Windows.Forms.Label();
            this.m_Legend15 = new System.Windows.Forms.Label();
            this.m_Legend16 = new System.Windows.Forms.Label();
            this.m_Legend17 = new System.Windows.Forms.Label();
            this.m_Legend18 = new System.Windows.Forms.Label();
            this.m_Legend19 = new System.Windows.Forms.Label();
            this.m_Legend20 = new System.Windows.Forms.Label();
            this.m_Legend21 = new System.Windows.Forms.Label();
            this.m_Legend22 = new System.Windows.Forms.Label();
            this.m_Legend23 = new System.Windows.Forms.Label();
            this.m_Legend24 = new System.Windows.Forms.Label();
            this.m_Legend25 = new System.Windows.Forms.Label();
            this.m_Legend26 = new System.Windows.Forms.Label();
            this.m_Legend27 = new System.Windows.Forms.Label();
            this.m_Legend28 = new System.Windows.Forms.Label();
            this.m_Legend29 = new System.Windows.Forms.Label();
            this.m_Legend30 = new System.Windows.Forms.Label();
            this.m_Legend31 = new System.Windows.Forms.Label();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxNewValue.SuspendLayout();
            this.m_TableLayoutPanelNewValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownNewValue)).BeginInit();
            this.m_GroupBoxCurrentValue.SuspendLayout();
            this.m_TableLayoutPanelCurrentValue.SuspendLayout();
            this.m_GroupBoxFormat.SuspendLayout();
            this.m_GroupBoxBitValues.SuspendLayout();
            this.m_TableLayoutPanelBitValues.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Location = new System.Drawing.Point(298, 616);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonOK.TabIndex = 4;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Location = new System.Drawing.Point(377, 616);
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
            this.m_ButtonApply.Location = new System.Drawing.Point(456, 616);
            this.m_ButtonApply.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonApply.Name = "m_ButtonApply";
            this.m_ButtonApply.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonApply.TabIndex = 0;
            this.m_ButtonApply.TabStop = false;
            this.m_ButtonApply.Text = "&Apply";
            this.m_ButtonApply.UseVisualStyleBackColor = true;
            this.m_ButtonApply.Click += new System.EventHandler(this.m_ButtonApply_Click);
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxNewValue);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxCurrentValue);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxFormat);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxBitValues);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_PanelOuter.Size = new System.Drawing.Size(539, 605);
            this.m_PanelOuter.TabIndex = 1;
            this.m_PanelOuter.TabStop = true;
            // 
            // m_GroupBoxNewValue
            // 
            this.m_GroupBoxNewValue.Controls.Add(this.m_TableLayoutPanelNewValue);
            this.m_GroupBoxNewValue.Location = new System.Drawing.Point(10, 83);
            this.m_GroupBoxNewValue.Name = "m_GroupBoxNewValue";
            this.m_GroupBoxNewValue.Size = new System.Drawing.Size(304, 67);
            this.m_GroupBoxNewValue.TabIndex = 2;
            this.m_GroupBoxNewValue.TabStop = false;
            this.m_GroupBoxNewValue.Text = "&New Value";
            // 
            // m_TableLayoutPanelNewValue
            // 
            this.m_TableLayoutPanelNewValue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelNewValue.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelNewValue.ColumnCount = 2;
            this.m_TableLayoutPanelNewValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 216F));
            this.m_TableLayoutPanelNewValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.m_TableLayoutPanelNewValue.Controls.Add(this.m_LabelNewValueUnits, 1, 0);
            this.m_TableLayoutPanelNewValue.Controls.Add(this.m_NumericUpDownNewValue, 0, 0);
            this.m_TableLayoutPanelNewValue.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelNewValue.Name = "m_TableLayoutPanelNewValue";
            this.m_TableLayoutPanelNewValue.RowCount = 1;
            this.m_TableLayoutPanelNewValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelNewValue.Size = new System.Drawing.Size(284, 32);
            this.m_TableLayoutPanelNewValue.TabIndex = 1;
            // 
            // m_LabelNewValueUnits
            // 
            this.m_LabelNewValueUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelNewValueUnits.AutoEllipsis = true;
            this.m_LabelNewValueUnits.AutoSize = true;
            this.m_LabelNewValueUnits.Location = new System.Drawing.Point(221, 9);
            this.m_LabelNewValueUnits.Name = "m_LabelNewValueUnits";
            this.m_LabelNewValueUnits.Size = new System.Drawing.Size(43, 13);
            this.m_LabelNewValueUnits.TabIndex = 0;
            this.m_LabelNewValueUnits.Text = "<Units>";
            this.m_LabelNewValueUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_NumericUpDownNewValue
            // 
            this.m_NumericUpDownNewValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_NumericUpDownNewValue.Location = new System.Drawing.Point(4, 6);
            this.m_NumericUpDownNewValue.Name = "m_NumericUpDownNewValue";
            this.m_NumericUpDownNewValue.Size = new System.Drawing.Size(210, 20);
            this.m_NumericUpDownNewValue.TabIndex = 1;
            this.m_NumericUpDownNewValue.ThousandsSeparator = true;
            this.m_NumericUpDownNewValue.ValueChanged += new System.EventHandler(this.m_NumericUpDown_ValueChanged);
            this.m_NumericUpDownNewValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_NumericUpDown_KeyPress);
            // 
            // m_GroupBoxCurrentValue
            // 
            this.m_GroupBoxCurrentValue.Controls.Add(this.m_TableLayoutPanelCurrentValue);
            this.m_GroupBoxCurrentValue.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxCurrentValue.Name = "m_GroupBoxCurrentValue";
            this.m_GroupBoxCurrentValue.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxCurrentValue.Size = new System.Drawing.Size(304, 67);
            this.m_GroupBoxCurrentValue.TabIndex = 0;
            this.m_GroupBoxCurrentValue.TabStop = false;
            this.m_GroupBoxCurrentValue.Text = "Current Value";
            // 
            // m_TableLayoutPanelCurrentValue
            // 
            this.m_TableLayoutPanelCurrentValue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelCurrentValue.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelCurrentValue.ColumnCount = 2;
            this.m_TableLayoutPanelCurrentValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 216F));
            this.m_TableLayoutPanelCurrentValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.m_TableLayoutPanelCurrentValue.Controls.Add(this.m_LabelCurrentValue, 0, 0);
            this.m_TableLayoutPanelCurrentValue.Controls.Add(this.m_LabelCurrentValueUnits, 1, 0);
            this.m_TableLayoutPanelCurrentValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelCurrentValue.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelCurrentValue.Name = "m_TableLayoutPanelCurrentValue";
            this.m_TableLayoutPanelCurrentValue.RowCount = 1;
            this.m_TableLayoutPanelCurrentValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelCurrentValue.Size = new System.Drawing.Size(284, 32);
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
            this.m_LabelCurrentValueUnits.Location = new System.Drawing.Point(221, 9);
            this.m_LabelCurrentValueUnits.Name = "m_LabelCurrentValueUnits";
            this.m_LabelCurrentValueUnits.Size = new System.Drawing.Size(43, 13);
            this.m_LabelCurrentValueUnits.TabIndex = 0;
            this.m_LabelCurrentValueUnits.Text = "<Units>";
            this.m_LabelCurrentValueUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_GroupBoxFormat
            // 
            this.m_GroupBoxFormat.Controls.Add(this.m_RadioButtonDecimal);
            this.m_GroupBoxFormat.Controls.Add(this.m_RadioButtonHex);
            this.m_GroupBoxFormat.Location = new System.Drawing.Point(433, 10);
            this.m_GroupBoxFormat.Name = "m_GroupBoxFormat";
            this.m_GroupBoxFormat.Size = new System.Drawing.Size(98, 140);
            this.m_GroupBoxFormat.TabIndex = 1;
            this.m_GroupBoxFormat.TabStop = false;
            this.m_GroupBoxFormat.Text = "Format";
            // 
            // m_RadioButtonDecimal
            // 
            this.m_RadioButtonDecimal.AutoSize = true;
            this.m_RadioButtonDecimal.Location = new System.Drawing.Point(10, 23);
            this.m_RadioButtonDecimal.Name = "m_RadioButtonDecimal";
            this.m_RadioButtonDecimal.Size = new System.Drawing.Size(63, 17);
            this.m_RadioButtonDecimal.TabIndex = 0;
            this.m_RadioButtonDecimal.Text = "&Decimal";
            this.m_RadioButtonDecimal.UseVisualStyleBackColor = true;
            this.m_RadioButtonDecimal.CheckedChanged += new System.EventHandler(this.m_RadioButtonDecimal_CheckedChanged);
            // 
            // m_RadioButtonHex
            // 
            this.m_RadioButtonHex.AutoSize = true;
            this.m_RadioButtonHex.Checked = true;
            this.m_RadioButtonHex.Location = new System.Drawing.Point(10, 46);
            this.m_RadioButtonHex.Name = "m_RadioButtonHex";
            this.m_RadioButtonHex.Size = new System.Drawing.Size(44, 17);
            this.m_RadioButtonHex.TabIndex = 0;
            this.m_RadioButtonHex.TabStop = true;
            this.m_RadioButtonHex.Text = "&Hex";
            this.m_RadioButtonHex.UseVisualStyleBackColor = true;
            // 
            // m_GroupBoxBitValues
            // 
            this.m_GroupBoxBitValues.Controls.Add(this.m_TableLayoutPanelBitValues);
            this.m_GroupBoxBitValues.Location = new System.Drawing.Point(10, 156);
            this.m_GroupBoxBitValues.Name = "m_GroupBoxBitValues";
            this.m_GroupBoxBitValues.Size = new System.Drawing.Size(519, 437);
            this.m_GroupBoxBitValues.TabIndex = 3;
            this.m_GroupBoxBitValues.TabStop = false;
            this.m_GroupBoxBitValues.Text = "&Flags";
            // 
            // m_TableLayoutPanelBitValues
            // 
            this.m_TableLayoutPanelBitValues.ColumnCount = 4;
            this.m_TableLayoutPanelBitValues.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelBitValues.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.m_TableLayoutPanelBitValues.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.m_TableLayoutPanelBitValues.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox00, 1, 0);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox01, 1, 1);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox02, 1, 2);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox03, 1, 3);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox04, 1, 4);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox05, 1, 5);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox06, 1, 6);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox07, 1, 7);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox08, 1, 8);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox09, 1, 9);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox10, 1, 10);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox11, 1, 11);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox12, 1, 12);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox13, 1, 13);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox14, 1, 14);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox15, 1, 15);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox16, 3, 0);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox17, 3, 1);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox18, 3, 2);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox19, 3, 3);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox20, 3, 4);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox21, 3, 5);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox22, 3, 6);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox23, 3, 7);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox24, 3, 8);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox25, 3, 9);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox26, 3, 10);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox27, 3, 11);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox28, 3, 12);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox29, 3, 13);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox30, 3, 14);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_CheckBox31, 3, 15);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend00, 0, 0);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend01, 0, 1);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend02, 0, 2);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend03, 0, 3);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend04, 0, 4);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend05, 0, 5);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend06, 0, 6);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend07, 0, 7);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend08, 0, 8);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend09, 0, 9);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend10, 0, 10);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend11, 0, 11);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend12, 0, 12);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend13, 0, 13);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend14, 0, 14);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend15, 0, 15);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend16, 2, 0);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend17, 2, 1);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend18, 2, 2);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend19, 2, 3);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend20, 2, 4);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend21, 2, 5);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend22, 2, 6);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend23, 2, 7);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend24, 2, 8);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend25, 2, 9);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend26, 2, 10);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend27, 2, 11);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend28, 2, 12);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend29, 2, 13);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend30, 2, 14);
            this.m_TableLayoutPanelBitValues.Controls.Add(this.m_Legend31, 2, 15);
            this.m_TableLayoutPanelBitValues.Location = new System.Drawing.Point(6, 19);
            this.m_TableLayoutPanelBitValues.Name = "m_TableLayoutPanelBitValues";
            this.m_TableLayoutPanelBitValues.RowCount = 16;
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelBitValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_TableLayoutPanelBitValues.Size = new System.Drawing.Size(500, 400);
            this.m_TableLayoutPanelBitValues.TabIndex = 1;
            this.m_TableLayoutPanelBitValues.TabStop = true;
            // 
            // m_CheckBox00
            // 
            this.m_CheckBox00.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox00.AutoEllipsis = true;
            this.m_CheckBox00.Location = new System.Drawing.Point(33, 1);
            this.m_CheckBox00.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox00.Name = "m_CheckBox00";
            this.m_CheckBox00.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox00.TabIndex = 0;
            this.m_CheckBox00.Text = "Undefined 0";
            this.m_CheckBox00.UseVisualStyleBackColor = true;
            this.m_CheckBox00.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox01
            // 
            this.m_CheckBox01.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox01.AutoEllipsis = true;
            this.m_CheckBox01.Location = new System.Drawing.Point(33, 26);
            this.m_CheckBox01.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox01.Name = "m_CheckBox01";
            this.m_CheckBox01.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox01.TabIndex = 0;
            this.m_CheckBox01.TabStop = false;
            this.m_CheckBox01.Text = "Undefined 1";
            this.m_CheckBox01.UseVisualStyleBackColor = true;
            this.m_CheckBox01.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox02
            // 
            this.m_CheckBox02.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox02.AutoEllipsis = true;
            this.m_CheckBox02.Location = new System.Drawing.Point(33, 51);
            this.m_CheckBox02.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox02.Name = "m_CheckBox02";
            this.m_CheckBox02.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox02.TabIndex = 0;
            this.m_CheckBox02.TabStop = false;
            this.m_CheckBox02.Text = "Undefined 2";
            this.m_CheckBox02.UseVisualStyleBackColor = true;
            this.m_CheckBox02.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox03
            // 
            this.m_CheckBox03.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox03.AutoEllipsis = true;
            this.m_CheckBox03.Location = new System.Drawing.Point(33, 76);
            this.m_CheckBox03.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox03.Name = "m_CheckBox03";
            this.m_CheckBox03.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox03.TabIndex = 0;
            this.m_CheckBox03.TabStop = false;
            this.m_CheckBox03.Text = "Undefined 3";
            this.m_CheckBox03.UseVisualStyleBackColor = true;
            this.m_CheckBox03.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox04
            // 
            this.m_CheckBox04.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox04.AutoEllipsis = true;
            this.m_CheckBox04.Location = new System.Drawing.Point(33, 101);
            this.m_CheckBox04.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox04.Name = "m_CheckBox04";
            this.m_CheckBox04.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox04.TabIndex = 0;
            this.m_CheckBox04.TabStop = false;
            this.m_CheckBox04.Text = "Undefined 4";
            this.m_CheckBox04.UseVisualStyleBackColor = true;
            this.m_CheckBox04.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox05
            // 
            this.m_CheckBox05.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox05.AutoEllipsis = true;
            this.m_CheckBox05.Location = new System.Drawing.Point(33, 126);
            this.m_CheckBox05.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox05.Name = "m_CheckBox05";
            this.m_CheckBox05.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox05.TabIndex = 0;
            this.m_CheckBox05.TabStop = false;
            this.m_CheckBox05.Text = "Undefined 5";
            this.m_CheckBox05.UseVisualStyleBackColor = true;
            this.m_CheckBox05.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox06
            // 
            this.m_CheckBox06.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox06.AutoEllipsis = true;
            this.m_CheckBox06.Location = new System.Drawing.Point(33, 151);
            this.m_CheckBox06.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox06.Name = "m_CheckBox06";
            this.m_CheckBox06.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox06.TabIndex = 0;
            this.m_CheckBox06.TabStop = false;
            this.m_CheckBox06.Text = "Undefined 6";
            this.m_CheckBox06.UseVisualStyleBackColor = true;
            this.m_CheckBox06.CheckStateChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox07
            // 
            this.m_CheckBox07.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox07.AutoEllipsis = true;
            this.m_CheckBox07.Location = new System.Drawing.Point(33, 176);
            this.m_CheckBox07.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox07.Name = "m_CheckBox07";
            this.m_CheckBox07.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox07.TabIndex = 0;
            this.m_CheckBox07.TabStop = false;
            this.m_CheckBox07.Text = "Undefined 7";
            this.m_CheckBox07.UseVisualStyleBackColor = true;
            this.m_CheckBox07.CheckStateChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox08
            // 
            this.m_CheckBox08.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox08.AutoEllipsis = true;
            this.m_CheckBox08.Location = new System.Drawing.Point(33, 201);
            this.m_CheckBox08.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox08.Name = "m_CheckBox08";
            this.m_CheckBox08.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox08.TabIndex = 0;
            this.m_CheckBox08.TabStop = false;
            this.m_CheckBox08.Text = "Undefined 8";
            this.m_CheckBox08.UseVisualStyleBackColor = true;
            this.m_CheckBox08.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox09
            // 
            this.m_CheckBox09.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox09.AutoEllipsis = true;
            this.m_CheckBox09.Location = new System.Drawing.Point(33, 226);
            this.m_CheckBox09.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox09.Name = "m_CheckBox09";
            this.m_CheckBox09.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox09.TabIndex = 0;
            this.m_CheckBox09.TabStop = false;
            this.m_CheckBox09.Text = "Undefined 9";
            this.m_CheckBox09.UseVisualStyleBackColor = true;
            this.m_CheckBox09.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox10
            // 
            this.m_CheckBox10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox10.AutoEllipsis = true;
            this.m_CheckBox10.Location = new System.Drawing.Point(33, 251);
            this.m_CheckBox10.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox10.Name = "m_CheckBox10";
            this.m_CheckBox10.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox10.TabIndex = 0;
            this.m_CheckBox10.TabStop = false;
            this.m_CheckBox10.Text = "Undefined 10";
            this.m_CheckBox10.UseVisualStyleBackColor = true;
            this.m_CheckBox10.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox11
            // 
            this.m_CheckBox11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox11.AutoEllipsis = true;
            this.m_CheckBox11.Location = new System.Drawing.Point(33, 276);
            this.m_CheckBox11.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox11.Name = "m_CheckBox11";
            this.m_CheckBox11.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox11.TabIndex = 0;
            this.m_CheckBox11.TabStop = false;
            this.m_CheckBox11.Text = "Undefined 11";
            this.m_CheckBox11.UseVisualStyleBackColor = true;
            this.m_CheckBox11.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox12
            // 
            this.m_CheckBox12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox12.AutoEllipsis = true;
            this.m_CheckBox12.Location = new System.Drawing.Point(33, 301);
            this.m_CheckBox12.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox12.Name = "m_CheckBox12";
            this.m_CheckBox12.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox12.TabIndex = 0;
            this.m_CheckBox12.TabStop = false;
            this.m_CheckBox12.Text = "Undefined 12";
            this.m_CheckBox12.UseVisualStyleBackColor = true;
            this.m_CheckBox12.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox13
            // 
            this.m_CheckBox13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox13.AutoEllipsis = true;
            this.m_CheckBox13.Location = new System.Drawing.Point(33, 326);
            this.m_CheckBox13.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox13.Name = "m_CheckBox13";
            this.m_CheckBox13.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox13.TabIndex = 0;
            this.m_CheckBox13.TabStop = false;
            this.m_CheckBox13.Text = "Undefined 13";
            this.m_CheckBox13.UseVisualStyleBackColor = true;
            this.m_CheckBox13.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox14
            // 
            this.m_CheckBox14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox14.AutoEllipsis = true;
            this.m_CheckBox14.Location = new System.Drawing.Point(33, 351);
            this.m_CheckBox14.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox14.Name = "m_CheckBox14";
            this.m_CheckBox14.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox14.TabIndex = 0;
            this.m_CheckBox14.TabStop = false;
            this.m_CheckBox14.Text = "Undefined 14";
            this.m_CheckBox14.UseVisualStyleBackColor = true;
            this.m_CheckBox14.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox15
            // 
            this.m_CheckBox15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox15.AutoEllipsis = true;
            this.m_CheckBox15.Location = new System.Drawing.Point(33, 376);
            this.m_CheckBox15.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox15.Name = "m_CheckBox15";
            this.m_CheckBox15.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox15.TabIndex = 0;
            this.m_CheckBox15.TabStop = false;
            this.m_CheckBox15.Text = "Undefined 15";
            this.m_CheckBox15.UseVisualStyleBackColor = true;
            this.m_CheckBox15.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox16
            // 
            this.m_CheckBox16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox16.AutoEllipsis = true;
            this.m_CheckBox16.Location = new System.Drawing.Point(283, 1);
            this.m_CheckBox16.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox16.Name = "m_CheckBox16";
            this.m_CheckBox16.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox16.TabIndex = 0;
            this.m_CheckBox16.TabStop = false;
            this.m_CheckBox16.Text = "Undefined 16";
            this.m_CheckBox16.UseVisualStyleBackColor = true;
            this.m_CheckBox16.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox17
            // 
            this.m_CheckBox17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox17.AutoEllipsis = true;
            this.m_CheckBox17.Location = new System.Drawing.Point(283, 26);
            this.m_CheckBox17.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox17.Name = "m_CheckBox17";
            this.m_CheckBox17.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox17.TabIndex = 0;
            this.m_CheckBox17.TabStop = false;
            this.m_CheckBox17.Text = "Undefined 17";
            this.m_CheckBox17.UseVisualStyleBackColor = true;
            this.m_CheckBox17.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox18
            // 
            this.m_CheckBox18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox18.AutoEllipsis = true;
            this.m_CheckBox18.Location = new System.Drawing.Point(283, 51);
            this.m_CheckBox18.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox18.Name = "m_CheckBox18";
            this.m_CheckBox18.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox18.TabIndex = 0;
            this.m_CheckBox18.TabStop = false;
            this.m_CheckBox18.Text = "Undefined 18";
            this.m_CheckBox18.UseVisualStyleBackColor = true;
            this.m_CheckBox18.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox19
            // 
            this.m_CheckBox19.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox19.AutoEllipsis = true;
            this.m_CheckBox19.Location = new System.Drawing.Point(283, 76);
            this.m_CheckBox19.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox19.Name = "m_CheckBox19";
            this.m_CheckBox19.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox19.TabIndex = 0;
            this.m_CheckBox19.TabStop = false;
            this.m_CheckBox19.Text = "Undefined 19";
            this.m_CheckBox19.UseVisualStyleBackColor = true;
            this.m_CheckBox19.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox20
            // 
            this.m_CheckBox20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox20.AutoEllipsis = true;
            this.m_CheckBox20.Location = new System.Drawing.Point(283, 101);
            this.m_CheckBox20.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox20.Name = "m_CheckBox20";
            this.m_CheckBox20.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox20.TabIndex = 0;
            this.m_CheckBox20.TabStop = false;
            this.m_CheckBox20.Text = "Undefined 20";
            this.m_CheckBox20.UseVisualStyleBackColor = true;
            this.m_CheckBox20.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox21
            // 
            this.m_CheckBox21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox21.AutoEllipsis = true;
            this.m_CheckBox21.Location = new System.Drawing.Point(283, 126);
            this.m_CheckBox21.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox21.Name = "m_CheckBox21";
            this.m_CheckBox21.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox21.TabIndex = 0;
            this.m_CheckBox21.TabStop = false;
            this.m_CheckBox21.Text = "Undefined 21";
            this.m_CheckBox21.UseVisualStyleBackColor = true;
            this.m_CheckBox21.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox22
            // 
            this.m_CheckBox22.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox22.AutoEllipsis = true;
            this.m_CheckBox22.Location = new System.Drawing.Point(283, 151);
            this.m_CheckBox22.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox22.Name = "m_CheckBox22";
            this.m_CheckBox22.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox22.TabIndex = 0;
            this.m_CheckBox22.TabStop = false;
            this.m_CheckBox22.Text = "Undefined 22";
            this.m_CheckBox22.UseVisualStyleBackColor = true;
            this.m_CheckBox22.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox23
            // 
            this.m_CheckBox23.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox23.AutoEllipsis = true;
            this.m_CheckBox23.Location = new System.Drawing.Point(283, 176);
            this.m_CheckBox23.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox23.Name = "m_CheckBox23";
            this.m_CheckBox23.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox23.TabIndex = 0;
            this.m_CheckBox23.TabStop = false;
            this.m_CheckBox23.Text = "Undefined 23";
            this.m_CheckBox23.UseVisualStyleBackColor = true;
            this.m_CheckBox23.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox24
            // 
            this.m_CheckBox24.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox24.AutoEllipsis = true;
            this.m_CheckBox24.Location = new System.Drawing.Point(283, 201);
            this.m_CheckBox24.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox24.Name = "m_CheckBox24";
            this.m_CheckBox24.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox24.TabIndex = 0;
            this.m_CheckBox24.TabStop = false;
            this.m_CheckBox24.Text = "Undefined 24";
            this.m_CheckBox24.UseVisualStyleBackColor = true;
            this.m_CheckBox24.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox25
            // 
            this.m_CheckBox25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox25.AutoEllipsis = true;
            this.m_CheckBox25.Location = new System.Drawing.Point(283, 226);
            this.m_CheckBox25.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox25.Name = "m_CheckBox25";
            this.m_CheckBox25.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox25.TabIndex = 0;
            this.m_CheckBox25.TabStop = false;
            this.m_CheckBox25.Text = "Undefined 25";
            this.m_CheckBox25.UseVisualStyleBackColor = true;
            this.m_CheckBox25.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox26
            // 
            this.m_CheckBox26.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox26.AutoEllipsis = true;
            this.m_CheckBox26.Location = new System.Drawing.Point(283, 251);
            this.m_CheckBox26.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox26.Name = "m_CheckBox26";
            this.m_CheckBox26.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox26.TabIndex = 0;
            this.m_CheckBox26.TabStop = false;
            this.m_CheckBox26.Text = "Undefined 26";
            this.m_CheckBox26.UseVisualStyleBackColor = true;
            this.m_CheckBox26.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox27
            // 
            this.m_CheckBox27.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox27.AutoEllipsis = true;
            this.m_CheckBox27.Location = new System.Drawing.Point(283, 276);
            this.m_CheckBox27.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox27.Name = "m_CheckBox27";
            this.m_CheckBox27.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox27.TabIndex = 0;
            this.m_CheckBox27.TabStop = false;
            this.m_CheckBox27.Text = "Undefined 27";
            this.m_CheckBox27.UseVisualStyleBackColor = true;
            this.m_CheckBox27.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox28
            // 
            this.m_CheckBox28.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox28.AutoEllipsis = true;
            this.m_CheckBox28.Location = new System.Drawing.Point(283, 301);
            this.m_CheckBox28.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox28.Name = "m_CheckBox28";
            this.m_CheckBox28.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox28.TabIndex = 0;
            this.m_CheckBox28.TabStop = false;
            this.m_CheckBox28.Text = "Undefined 23";
            this.m_CheckBox28.UseVisualStyleBackColor = true;
            this.m_CheckBox28.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox29
            // 
            this.m_CheckBox29.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox29.AutoEllipsis = true;
            this.m_CheckBox29.Location = new System.Drawing.Point(283, 326);
            this.m_CheckBox29.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox29.Name = "m_CheckBox29";
            this.m_CheckBox29.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox29.TabIndex = 0;
            this.m_CheckBox29.TabStop = false;
            this.m_CheckBox29.Text = "Undefined 29";
            this.m_CheckBox29.UseVisualStyleBackColor = true;
            this.m_CheckBox29.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox30
            // 
            this.m_CheckBox30.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox30.AutoEllipsis = true;
            this.m_CheckBox30.Location = new System.Drawing.Point(283, 351);
            this.m_CheckBox30.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox30.Name = "m_CheckBox30";
            this.m_CheckBox30.Size = new System.Drawing.Size(217, 23);
            this.m_CheckBox30.TabIndex = 0;
            this.m_CheckBox30.TabStop = false;
            this.m_CheckBox30.Text = "Undefined 30";
            this.m_CheckBox30.UseVisualStyleBackColor = true;
            this.m_CheckBox30.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_CheckBox31
            // 
            this.m_CheckBox31.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_CheckBox31.AutoEllipsis = true;
            this.m_CheckBox31.Location = new System.Drawing.Point(283, 377);
            this.m_CheckBox31.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.m_CheckBox31.Name = "m_CheckBox31";
            this.m_CheckBox31.Size = new System.Drawing.Size(217, 21);
            this.m_CheckBox31.TabIndex = 0;
            this.m_CheckBox31.TabStop = false;
            this.m_CheckBox31.Text = "Undefined 31";
            this.m_CheckBox31.UseVisualStyleBackColor = true;
            this.m_CheckBox31.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_Legend00
            // 
            this.m_Legend00.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend00.AutoSize = true;
            this.m_Legend00.Location = new System.Drawing.Point(3, 6);
            this.m_Legend00.Name = "m_Legend00";
            this.m_Legend00.Size = new System.Drawing.Size(13, 13);
            this.m_Legend00.TabIndex = 0;
            this.m_Legend00.Text = "0";
            // 
            // m_Legend01
            // 
            this.m_Legend01.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend01.AutoSize = true;
            this.m_Legend01.Location = new System.Drawing.Point(3, 31);
            this.m_Legend01.Name = "m_Legend01";
            this.m_Legend01.Size = new System.Drawing.Size(13, 13);
            this.m_Legend01.TabIndex = 0;
            this.m_Legend01.Text = "1";
            // 
            // m_Legend02
            // 
            this.m_Legend02.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend02.AutoSize = true;
            this.m_Legend02.Location = new System.Drawing.Point(3, 56);
            this.m_Legend02.Name = "m_Legend02";
            this.m_Legend02.Size = new System.Drawing.Size(13, 13);
            this.m_Legend02.TabIndex = 0;
            this.m_Legend02.Text = "2";
            // 
            // m_Legend03
            // 
            this.m_Legend03.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend03.AutoSize = true;
            this.m_Legend03.Location = new System.Drawing.Point(3, 81);
            this.m_Legend03.Name = "m_Legend03";
            this.m_Legend03.Size = new System.Drawing.Size(13, 13);
            this.m_Legend03.TabIndex = 0;
            this.m_Legend03.Text = "3";
            // 
            // m_Legend04
            // 
            this.m_Legend04.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend04.AutoSize = true;
            this.m_Legend04.Location = new System.Drawing.Point(3, 106);
            this.m_Legend04.Name = "m_Legend04";
            this.m_Legend04.Size = new System.Drawing.Size(13, 13);
            this.m_Legend04.TabIndex = 0;
            this.m_Legend04.Text = "4";
            // 
            // m_Legend05
            // 
            this.m_Legend05.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend05.AutoSize = true;
            this.m_Legend05.Location = new System.Drawing.Point(3, 131);
            this.m_Legend05.Name = "m_Legend05";
            this.m_Legend05.Size = new System.Drawing.Size(13, 13);
            this.m_Legend05.TabIndex = 0;
            this.m_Legend05.Text = "5";
            // 
            // m_Legend06
            // 
            this.m_Legend06.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend06.AutoSize = true;
            this.m_Legend06.Location = new System.Drawing.Point(3, 156);
            this.m_Legend06.Name = "m_Legend06";
            this.m_Legend06.Size = new System.Drawing.Size(13, 13);
            this.m_Legend06.TabIndex = 0;
            this.m_Legend06.Text = "6";
            // 
            // m_Legend07
            // 
            this.m_Legend07.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend07.AutoSize = true;
            this.m_Legend07.Location = new System.Drawing.Point(3, 181);
            this.m_Legend07.Name = "m_Legend07";
            this.m_Legend07.Size = new System.Drawing.Size(13, 13);
            this.m_Legend07.TabIndex = 0;
            this.m_Legend07.Text = "7";
            // 
            // m_Legend08
            // 
            this.m_Legend08.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend08.AutoSize = true;
            this.m_Legend08.Location = new System.Drawing.Point(3, 206);
            this.m_Legend08.Name = "m_Legend08";
            this.m_Legend08.Size = new System.Drawing.Size(13, 13);
            this.m_Legend08.TabIndex = 0;
            this.m_Legend08.Text = "8";
            // 
            // m_Legend09
            // 
            this.m_Legend09.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend09.AutoSize = true;
            this.m_Legend09.Location = new System.Drawing.Point(3, 231);
            this.m_Legend09.Name = "m_Legend09";
            this.m_Legend09.Size = new System.Drawing.Size(13, 13);
            this.m_Legend09.TabIndex = 0;
            this.m_Legend09.Text = "9";
            // 
            // m_Legend10
            // 
            this.m_Legend10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend10.AutoSize = true;
            this.m_Legend10.Location = new System.Drawing.Point(3, 256);
            this.m_Legend10.Name = "m_Legend10";
            this.m_Legend10.Size = new System.Drawing.Size(19, 13);
            this.m_Legend10.TabIndex = 0;
            this.m_Legend10.Text = "10";
            // 
            // m_Legend11
            // 
            this.m_Legend11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend11.AutoSize = true;
            this.m_Legend11.Location = new System.Drawing.Point(3, 281);
            this.m_Legend11.Name = "m_Legend11";
            this.m_Legend11.Size = new System.Drawing.Size(19, 13);
            this.m_Legend11.TabIndex = 0;
            this.m_Legend11.Text = "11";
            // 
            // m_Legend12
            // 
            this.m_Legend12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend12.AutoSize = true;
            this.m_Legend12.Location = new System.Drawing.Point(3, 306);
            this.m_Legend12.Name = "m_Legend12";
            this.m_Legend12.Size = new System.Drawing.Size(19, 13);
            this.m_Legend12.TabIndex = 0;
            this.m_Legend12.Text = "12";
            // 
            // m_Legend13
            // 
            this.m_Legend13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend13.AutoSize = true;
            this.m_Legend13.Location = new System.Drawing.Point(3, 331);
            this.m_Legend13.Name = "m_Legend13";
            this.m_Legend13.Size = new System.Drawing.Size(19, 13);
            this.m_Legend13.TabIndex = 0;
            this.m_Legend13.Text = "13";
            // 
            // m_Legend14
            // 
            this.m_Legend14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend14.AutoSize = true;
            this.m_Legend14.Location = new System.Drawing.Point(3, 356);
            this.m_Legend14.Name = "m_Legend14";
            this.m_Legend14.Size = new System.Drawing.Size(19, 13);
            this.m_Legend14.TabIndex = 0;
            this.m_Legend14.Text = "14";
            // 
            // m_Legend15
            // 
            this.m_Legend15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend15.AutoSize = true;
            this.m_Legend15.Location = new System.Drawing.Point(3, 381);
            this.m_Legend15.Name = "m_Legend15";
            this.m_Legend15.Size = new System.Drawing.Size(19, 13);
            this.m_Legend15.TabIndex = 0;
            this.m_Legend15.Text = "15";
            // 
            // m_Legend16
            // 
            this.m_Legend16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend16.AutoSize = true;
            this.m_Legend16.Location = new System.Drawing.Point(253, 6);
            this.m_Legend16.Name = "m_Legend16";
            this.m_Legend16.Size = new System.Drawing.Size(19, 13);
            this.m_Legend16.TabIndex = 0;
            this.m_Legend16.Text = "16";
            // 
            // m_Legend17
            // 
            this.m_Legend17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend17.AutoSize = true;
            this.m_Legend17.Location = new System.Drawing.Point(253, 31);
            this.m_Legend17.Name = "m_Legend17";
            this.m_Legend17.Size = new System.Drawing.Size(19, 13);
            this.m_Legend17.TabIndex = 0;
            this.m_Legend17.Text = "17";
            // 
            // m_Legend18
            // 
            this.m_Legend18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend18.AutoSize = true;
            this.m_Legend18.Location = new System.Drawing.Point(253, 56);
            this.m_Legend18.Name = "m_Legend18";
            this.m_Legend18.Size = new System.Drawing.Size(19, 13);
            this.m_Legend18.TabIndex = 0;
            this.m_Legend18.Text = "18";
            // 
            // m_Legend19
            // 
            this.m_Legend19.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend19.AutoSize = true;
            this.m_Legend19.Location = new System.Drawing.Point(253, 81);
            this.m_Legend19.Name = "m_Legend19";
            this.m_Legend19.Size = new System.Drawing.Size(19, 13);
            this.m_Legend19.TabIndex = 0;
            this.m_Legend19.Text = "19";
            // 
            // m_Legend20
            // 
            this.m_Legend20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend20.AutoSize = true;
            this.m_Legend20.Location = new System.Drawing.Point(253, 106);
            this.m_Legend20.Name = "m_Legend20";
            this.m_Legend20.Size = new System.Drawing.Size(19, 13);
            this.m_Legend20.TabIndex = 0;
            this.m_Legend20.Text = "20";
            // 
            // m_Legend21
            // 
            this.m_Legend21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend21.AutoSize = true;
            this.m_Legend21.Location = new System.Drawing.Point(253, 131);
            this.m_Legend21.Name = "m_Legend21";
            this.m_Legend21.Size = new System.Drawing.Size(19, 13);
            this.m_Legend21.TabIndex = 0;
            this.m_Legend21.Text = "21";
            // 
            // m_Legend22
            // 
            this.m_Legend22.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend22.AutoSize = true;
            this.m_Legend22.Location = new System.Drawing.Point(253, 156);
            this.m_Legend22.Name = "m_Legend22";
            this.m_Legend22.Size = new System.Drawing.Size(19, 13);
            this.m_Legend22.TabIndex = 0;
            this.m_Legend22.Text = "22";
            // 
            // m_Legend23
            // 
            this.m_Legend23.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend23.AutoSize = true;
            this.m_Legend23.Location = new System.Drawing.Point(253, 181);
            this.m_Legend23.Name = "m_Legend23";
            this.m_Legend23.Size = new System.Drawing.Size(19, 13);
            this.m_Legend23.TabIndex = 0;
            this.m_Legend23.Text = "23";
            // 
            // m_Legend24
            // 
            this.m_Legend24.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend24.AutoSize = true;
            this.m_Legend24.Location = new System.Drawing.Point(253, 206);
            this.m_Legend24.Name = "m_Legend24";
            this.m_Legend24.Size = new System.Drawing.Size(19, 13);
            this.m_Legend24.TabIndex = 0;
            this.m_Legend24.Text = "24";
            // 
            // m_Legend25
            // 
            this.m_Legend25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend25.AutoSize = true;
            this.m_Legend25.Location = new System.Drawing.Point(253, 231);
            this.m_Legend25.Name = "m_Legend25";
            this.m_Legend25.Size = new System.Drawing.Size(19, 13);
            this.m_Legend25.TabIndex = 0;
            this.m_Legend25.Text = "25";
            // 
            // m_Legend26
            // 
            this.m_Legend26.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend26.AutoSize = true;
            this.m_Legend26.Location = new System.Drawing.Point(253, 256);
            this.m_Legend26.Name = "m_Legend26";
            this.m_Legend26.Size = new System.Drawing.Size(19, 13);
            this.m_Legend26.TabIndex = 0;
            this.m_Legend26.Text = "26";
            // 
            // m_Legend27
            // 
            this.m_Legend27.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend27.AutoSize = true;
            this.m_Legend27.Location = new System.Drawing.Point(253, 281);
            this.m_Legend27.Name = "m_Legend27";
            this.m_Legend27.Size = new System.Drawing.Size(19, 13);
            this.m_Legend27.TabIndex = 0;
            this.m_Legend27.Text = "27";
            // 
            // m_Legend28
            // 
            this.m_Legend28.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend28.AutoSize = true;
            this.m_Legend28.Location = new System.Drawing.Point(253, 306);
            this.m_Legend28.Name = "m_Legend28";
            this.m_Legend28.Size = new System.Drawing.Size(19, 13);
            this.m_Legend28.TabIndex = 0;
            this.m_Legend28.Text = "28";
            // 
            // m_Legend29
            // 
            this.m_Legend29.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend29.AutoSize = true;
            this.m_Legend29.Location = new System.Drawing.Point(253, 331);
            this.m_Legend29.Name = "m_Legend29";
            this.m_Legend29.Size = new System.Drawing.Size(19, 13);
            this.m_Legend29.TabIndex = 0;
            this.m_Legend29.Text = "29";
            // 
            // m_Legend30
            // 
            this.m_Legend30.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend30.AutoSize = true;
            this.m_Legend30.Location = new System.Drawing.Point(253, 356);
            this.m_Legend30.Name = "m_Legend30";
            this.m_Legend30.Size = new System.Drawing.Size(19, 13);
            this.m_Legend30.TabIndex = 0;
            this.m_Legend30.Text = "30";
            // 
            // m_Legend31
            // 
            this.m_Legend31.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Legend31.AutoSize = true;
            this.m_Legend31.Location = new System.Drawing.Point(253, 381);
            this.m_Legend31.Name = "m_Legend31";
            this.m_Legend31.Size = new System.Drawing.Size(19, 13);
            this.m_Legend31.TabIndex = 0;
            this.m_Legend31.Text = "31";
            // 
            // FormChangeBitmask
            // 
            this.AcceptButton = this.m_ButtonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(539, 652);
            this.Controls.Add(this.m_ButtonApply);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_ButtonCancel);
            this.Controls.Add(this.m_PanelOuter);
            this.Name = "FormChangeBitmask";
            this.m_PanelOuter.ResumeLayout(false);
            this.m_GroupBoxNewValue.ResumeLayout(false);
            this.m_TableLayoutPanelNewValue.ResumeLayout(false);
            this.m_TableLayoutPanelNewValue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownNewValue)).EndInit();
            this.m_GroupBoxCurrentValue.ResumeLayout(false);
            this.m_TableLayoutPanelCurrentValue.ResumeLayout(false);
            this.m_TableLayoutPanelCurrentValue.PerformLayout();
            this.m_GroupBoxFormat.ResumeLayout(false);
            this.m_GroupBoxFormat.PerformLayout();
            this.m_GroupBoxBitValues.ResumeLayout(false);
            this.m_TableLayoutPanelBitValues.ResumeLayout(false);
            this.m_TableLayoutPanelBitValues.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Reference to the outer <c>Panel</c> control.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelOuter;

        /// <summary>
        /// Reference to the OK <c>Button</c> control.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonOK;

        /// <summary>
        /// Reference to the Cancel <c>Button</c> control.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonCancel;

        /// <summary>
        /// Reference to the Apply <c>Button</c> control.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonApply;

        /// <summary>
        /// Reference to the <c>GroupBox</c> control that allows the user to specify the display format.
        /// </summary>
        protected System.Windows.Forms.GroupBox m_GroupBoxFormat;

        /// <summary>
        /// Reference to the <c>GroupBox</c> control that displays the current value.
        /// </summary>
        protected System.Windows.Forms.GroupBox m_GroupBoxCurrentValue;

        /// <summary>
        /// Reference to the <c>GroupBox</c> control that displays the new value.
        /// </summary>
        protected System.Windows.Forms.GroupBox m_GroupBoxNewValue;

        /// <summary>
        /// Reference to the <c>GroupBox</c> control that displays the <c>CheckBox</c> controls representing the individual flags of the bitmask.
        /// </summary>
        protected System.Windows.Forms.GroupBox m_GroupBoxBitValues;

        private System.Windows.Forms.RadioButton m_RadioButtonHex;
        private System.Windows.Forms.RadioButton m_RadioButtonDecimal;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelCurrentValue;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelNewValue;
        private System.Windows.Forms.Label m_LabelCurrentValue;
        private System.Windows.Forms.Label m_LabelCurrentValueUnits;
        private System.Windows.Forms.Label m_LabelNewValueUnits;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownNewValue;

        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelBitValues;
        private System.Windows.Forms.CheckBox m_CheckBox00;
        private System.Windows.Forms.CheckBox m_CheckBox01;
        private System.Windows.Forms.CheckBox m_CheckBox02;
        private System.Windows.Forms.CheckBox m_CheckBox03;
        private System.Windows.Forms.CheckBox m_CheckBox04;
        private System.Windows.Forms.CheckBox m_CheckBox05;
        private System.Windows.Forms.CheckBox m_CheckBox06;
        private System.Windows.Forms.CheckBox m_CheckBox07;
        private System.Windows.Forms.CheckBox m_CheckBox08;
        private System.Windows.Forms.CheckBox m_CheckBox09;
        private System.Windows.Forms.CheckBox m_CheckBox10;
        private System.Windows.Forms.CheckBox m_CheckBox11;
        private System.Windows.Forms.CheckBox m_CheckBox12;
        private System.Windows.Forms.CheckBox m_CheckBox13;
        private System.Windows.Forms.CheckBox m_CheckBox14;
        private System.Windows.Forms.CheckBox m_CheckBox15;
        private System.Windows.Forms.CheckBox m_CheckBox16;
        private System.Windows.Forms.CheckBox m_CheckBox17;
        private System.Windows.Forms.CheckBox m_CheckBox18;
        private System.Windows.Forms.CheckBox m_CheckBox19;
        private System.Windows.Forms.CheckBox m_CheckBox20;
        private System.Windows.Forms.CheckBox m_CheckBox21;
        private System.Windows.Forms.CheckBox m_CheckBox22;
        private System.Windows.Forms.CheckBox m_CheckBox23;
        private System.Windows.Forms.CheckBox m_CheckBox24;
        private System.Windows.Forms.CheckBox m_CheckBox25;
        private System.Windows.Forms.CheckBox m_CheckBox26;
        private System.Windows.Forms.CheckBox m_CheckBox27;
        private System.Windows.Forms.CheckBox m_CheckBox28;
        private System.Windows.Forms.CheckBox m_CheckBox29;
        private System.Windows.Forms.CheckBox m_CheckBox30;
        private System.Windows.Forms.CheckBox m_CheckBox31;
        private System.Windows.Forms.Label m_Legend00;
        private System.Windows.Forms.Label m_Legend01;
        private System.Windows.Forms.Label m_Legend02;
        private System.Windows.Forms.Label m_Legend03;
        private System.Windows.Forms.Label m_Legend04;
        private System.Windows.Forms.Label m_Legend05;
        private System.Windows.Forms.Label m_Legend06;
        private System.Windows.Forms.Label m_Legend07;
        private System.Windows.Forms.Label m_Legend08;
        private System.Windows.Forms.Label m_Legend09;
        private System.Windows.Forms.Label m_Legend10;
        private System.Windows.Forms.Label m_Legend11;
        private System.Windows.Forms.Label m_Legend12;
        private System.Windows.Forms.Label m_Legend13;
        private System.Windows.Forms.Label m_Legend14;
        private System.Windows.Forms.Label m_Legend15;
        private System.Windows.Forms.Label m_Legend16;
        private System.Windows.Forms.Label m_Legend17;
        private System.Windows.Forms.Label m_Legend18;
        private System.Windows.Forms.Label m_Legend19;
        private System.Windows.Forms.Label m_Legend20;
        private System.Windows.Forms.Label m_Legend21;
        private System.Windows.Forms.Label m_Legend22;
        private System.Windows.Forms.Label m_Legend23;
        private System.Windows.Forms.Label m_Legend24;
        private System.Windows.Forms.Label m_Legend25;
        private System.Windows.Forms.Label m_Legend26;
        private System.Windows.Forms.Label m_Legend27;
        private System.Windows.Forms.Label m_Legend28;
        private System.Windows.Forms.Label m_Legend29;
        private System.Windows.Forms.Label m_Legend30;
        private System.Windows.Forms.Label m_Legend31;
    }
}