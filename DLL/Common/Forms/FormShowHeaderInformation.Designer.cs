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
 *  Project:    Common
 * 
 *  File name:  FormShowHeaderInformation.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/13/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/30/10    1.1     K.McD           1.  Renamed a number of variables.
 *                                      2.  Documentation changes to a numer of XML tags.
 *                                      3.  Layout changes.
 *                                      
 *  10/02/11    1.2     K.McD           1.  No longer sets the ReadOnly property of the 'Comments' TextBox to true.
 *                                      2.  Changed the name of the event handler associated with the OK button Click event.
 *
 */
#endregion --- Revision History ---

namespace Common.Forms
{
    partial class FormShowHeaderInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowHeaderInformation));
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_TabControl = new System.Windows.Forms.TabControl();
            this.m_TabPageGeneral = new System.Windows.Forms.TabPage();
            this.m_GroupBoxVCU = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelVCU = new System.Windows.Forms.TableLayoutPanel();
            this.m_LegendVersionVCU = new System.Windows.Forms.Label();
            this.m_LabelVersionVCU = new System.Windows.Forms.Label();
            this.m_LegendProjectIdentifierVCU = new System.Windows.Forms.Label();
            this.m_LabelSubsystemVCU = new System.Windows.Forms.Label();
            this.m_LabelProjectIdentifierVCU = new System.Windows.Forms.Label();
            this.m_LegendSubsystemVCU = new System.Windows.Forms.Label();
            this.m_LabelCarIdentifierVCU = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_GroupBoxPTU = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelPTU = new System.Windows.Forms.TableLayoutPanel();
            this.m_LegendVersionPTU = new System.Windows.Forms.Label();
            this.m_LabelVersionPTU = new System.Windows.Forms.Label();
            this.m_GroupBoxDD = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelDD = new System.Windows.Forms.TableLayoutPanel();
            this.m_LegendVersionDD = new System.Windows.Forms.Label();
            this.m_LegendProjectIdentifierDD = new System.Windows.Forms.Label();
            this.m_LabelProjectIdentifierDD = new System.Windows.Forms.Label();
            this.m_LabelVersionDD = new System.Windows.Forms.Label();
            this.m_LegendNameDD = new System.Windows.Forms.Label();
            this.m_LabelNameDD = new System.Windows.Forms.Label();
            this.m_GroupBoxDDB = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelDDB = new System.Windows.Forms.TableLayoutPanel();
            this.m_LegendVersionDDB = new System.Windows.Forms.Label();
            this.m_LabelVersionDDB = new System.Windows.Forms.Label();
            this.m_TabPageUserComments = new System.Windows.Forms.TabPage();
            this.m_GroupBoxComments = new System.Windows.Forms.GroupBox();
            this.m_TextBoxComments = new System.Windows.Forms.TextBox();
            this.m_GroupBoxUser = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelDateTime = new System.Windows.Forms.TableLayoutPanel();
            this.m_LegendTime = new System.Windows.Forms.Label();
            this.m_LegendDate = new System.Windows.Forms.Label();
            this.m_LegendUser = new System.Windows.Forms.Label();
            this.m_LabelUser = new System.Windows.Forms.Label();
            this.m_LabelDate = new System.Windows.Forms.Label();
            this.m_LabelTime = new System.Windows.Forms.Label();
            this.m_TabControl.SuspendLayout();
            this.m_TabPageGeneral.SuspendLayout();
            this.m_GroupBoxVCU.SuspendLayout();
            this.m_TableLayoutPanelVCU.SuspendLayout();
            this.m_GroupBoxPTU.SuspendLayout();
            this.m_TableLayoutPanelPTU.SuspendLayout();
            this.m_GroupBoxDD.SuspendLayout();
            this.m_TableLayoutPanelDD.SuspendLayout();
            this.m_GroupBoxDDB.SuspendLayout();
            this.m_TableLayoutPanelDDB.SuspendLayout();
            this.m_TabPageUserComments.SuspendLayout();
            this.m_GroupBoxComments.SuspendLayout();
            this.m_GroupBoxUser.SuspendLayout();
            this.m_TableLayoutPanelDateTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonOK.Location = new System.Drawing.Point(331, 462);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonOK.TabIndex = 1;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_TabControl
            // 
            this.m_TabControl.Controls.Add(this.m_TabPageGeneral);
            this.m_TabControl.Controls.Add(this.m_TabPageUserComments);
            this.m_TabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TabControl.Location = new System.Drawing.Point(10, 10);
            this.m_TabControl.Name = "m_TabControl";
            this.m_TabControl.SelectedIndex = 0;
            this.m_TabControl.Size = new System.Drawing.Size(408, 441);
            this.m_TabControl.TabIndex = 2;
            // 
            // m_TabPageGeneral
            // 
            this.m_TabPageGeneral.Controls.Add(this.m_GroupBoxVCU);
            this.m_TabPageGeneral.Controls.Add(this.m_GroupBoxPTU);
            this.m_TabPageGeneral.Controls.Add(this.m_GroupBoxDD);
            this.m_TabPageGeneral.Controls.Add(this.m_GroupBoxDDB);
            this.m_TabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageGeneral.Name = "m_TabPageGeneral";
            this.m_TabPageGeneral.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_TabPageGeneral.Size = new System.Drawing.Size(400, 415);
            this.m_TabPageGeneral.TabIndex = 0;
            this.m_TabPageGeneral.Text = "General";
            this.m_TabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // m_GroupBoxVCU
            // 
            this.m_GroupBoxVCU.Controls.Add(this.m_TableLayoutPanelVCU);
            this.m_GroupBoxVCU.Location = new System.Drawing.Point(10, 263);
            this.m_GroupBoxVCU.Name = "m_GroupBoxVCU";
            this.m_GroupBoxVCU.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxVCU.Size = new System.Drawing.Size(380, 140);
            this.m_GroupBoxVCU.TabIndex = 1;
            this.m_GroupBoxVCU.TabStop = false;
            this.m_GroupBoxVCU.Text = "Vehicle Control Unit";
            // 
            // m_TableLayoutPanelVCU
            // 
            this.m_TableLayoutPanelVCU.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelVCU.ColumnCount = 2;
            this.m_TableLayoutPanelVCU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.m_TableLayoutPanelVCU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelVCU.Controls.Add(this.m_LegendVersionVCU, 0, 1);
            this.m_TableLayoutPanelVCU.Controls.Add(this.m_LabelVersionVCU, 1, 1);
            this.m_TableLayoutPanelVCU.Controls.Add(this.m_LegendProjectIdentifierVCU, 0, 3);
            this.m_TableLayoutPanelVCU.Controls.Add(this.m_LabelSubsystemVCU, 1, 0);
            this.m_TableLayoutPanelVCU.Controls.Add(this.m_LabelProjectIdentifierVCU, 1, 3);
            this.m_TableLayoutPanelVCU.Controls.Add(this.m_LegendSubsystemVCU, 0, 0);
            this.m_TableLayoutPanelVCU.Controls.Add(this.m_LabelCarIdentifierVCU, 1, 2);
            this.m_TableLayoutPanelVCU.Controls.Add(this.label1, 0, 2);
            this.m_TableLayoutPanelVCU.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelVCU.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelVCU.Name = "m_TableLayoutPanelVCU";
            this.m_TableLayoutPanelVCU.RowCount = 4;
            this.m_TableLayoutPanelVCU.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelVCU.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelVCU.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelVCU.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelVCU.Size = new System.Drawing.Size(360, 105);
            this.m_TableLayoutPanelVCU.TabIndex = 0;
            // 
            // m_LegendVersionVCU
            // 
            this.m_LegendVersionVCU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendVersionVCU.AutoSize = true;
            this.m_LegendVersionVCU.Location = new System.Drawing.Point(4, 33);
            this.m_LegendVersionVCU.Name = "m_LegendVersionVCU";
            this.m_LegendVersionVCU.Size = new System.Drawing.Size(45, 13);
            this.m_LegendVersionVCU.TabIndex = 0;
            this.m_LegendVersionVCU.Text = "Version:";
            // 
            // m_LabelVersionVCU
            // 
            this.m_LabelVersionVCU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelVersionVCU.AutoSize = true;
            this.m_LabelVersionVCU.Location = new System.Drawing.Point(115, 33);
            this.m_LabelVersionVCU.Name = "m_LabelVersionVCU";
            this.m_LabelVersionVCU.Size = new System.Drawing.Size(54, 13);
            this.m_LabelVersionVCU.TabIndex = 0;
            this.m_LabelVersionVCU.Text = "<Version>";
            // 
            // m_LegendProjectIdentifierVCU
            // 
            this.m_LegendProjectIdentifierVCU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendProjectIdentifierVCU.AutoSize = true;
            this.m_LegendProjectIdentifierVCU.Location = new System.Drawing.Point(4, 85);
            this.m_LegendProjectIdentifierVCU.Name = "m_LegendProjectIdentifierVCU";
            this.m_LegendProjectIdentifierVCU.Size = new System.Drawing.Size(86, 13);
            this.m_LegendProjectIdentifierVCU.TabIndex = 2;
            this.m_LegendProjectIdentifierVCU.Text = "Project Identifier:";
            // 
            // m_LabelSubsystemVCU
            // 
            this.m_LabelSubsystemVCU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelSubsystemVCU.AutoSize = true;
            this.m_LabelSubsystemVCU.Location = new System.Drawing.Point(115, 7);
            this.m_LabelSubsystemVCU.Name = "m_LabelSubsystemVCU";
            this.m_LabelSubsystemVCU.Size = new System.Drawing.Size(70, 13);
            this.m_LabelSubsystemVCU.TabIndex = 3;
            this.m_LabelSubsystemVCU.Text = "<Subsystem>";
            // 
            // m_LabelProjectIdentifierVCU
            // 
            this.m_LabelProjectIdentifierVCU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelProjectIdentifierVCU.AutoSize = true;
            this.m_LabelProjectIdentifierVCU.Location = new System.Drawing.Point(115, 85);
            this.m_LabelProjectIdentifierVCU.Name = "m_LabelProjectIdentifierVCU";
            this.m_LabelProjectIdentifierVCU.Size = new System.Drawing.Size(95, 13);
            this.m_LabelProjectIdentifierVCU.TabIndex = 4;
            this.m_LabelProjectIdentifierVCU.Text = "<Project Identifier>";
            // 
            // m_LegendSubsystemVCU
            // 
            this.m_LegendSubsystemVCU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendSubsystemVCU.AutoSize = true;
            this.m_LegendSubsystemVCU.Location = new System.Drawing.Point(4, 7);
            this.m_LegendSubsystemVCU.Name = "m_LegendSubsystemVCU";
            this.m_LegendSubsystemVCU.Size = new System.Drawing.Size(61, 13);
            this.m_LegendSubsystemVCU.TabIndex = 1;
            this.m_LegendSubsystemVCU.Text = "Subsystem:";
            // 
            // m_LabelCarIdentifierVCU
            // 
            this.m_LabelCarIdentifierVCU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelCarIdentifierVCU.AutoSize = true;
            this.m_LabelCarIdentifierVCU.Location = new System.Drawing.Point(115, 59);
            this.m_LabelCarIdentifierVCU.Name = "m_LabelCarIdentifierVCU";
            this.m_LabelCarIdentifierVCU.Size = new System.Drawing.Size(78, 13);
            this.m_LabelCarIdentifierVCU.TabIndex = 6;
            this.m_LabelCarIdentifierVCU.Text = "<Car Identifier>";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Car Identifier:";
            // 
            // m_GroupBoxPTU
            // 
            this.m_GroupBoxPTU.Controls.Add(this.m_TableLayoutPanelPTU);
            this.m_GroupBoxPTU.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupBoxPTU.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxPTU.Name = "m_GroupBoxPTU";
            this.m_GroupBoxPTU.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxPTU.Size = new System.Drawing.Size(380, 60);
            this.m_GroupBoxPTU.TabIndex = 0;
            this.m_GroupBoxPTU.TabStop = false;
            this.m_GroupBoxPTU.Text = "Portable Test Unit";
            // 
            // m_TableLayoutPanelPTU
            // 
            this.m_TableLayoutPanelPTU.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelPTU.ColumnCount = 2;
            this.m_TableLayoutPanelPTU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.m_TableLayoutPanelPTU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelPTU.Controls.Add(this.m_LegendVersionPTU, 0, 0);
            this.m_TableLayoutPanelPTU.Controls.Add(this.m_LabelVersionPTU, 1, 0);
            this.m_TableLayoutPanelPTU.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelPTU.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelPTU.Name = "m_TableLayoutPanelPTU";
            this.m_TableLayoutPanelPTU.RowCount = 1;
            this.m_TableLayoutPanelPTU.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelPTU.Size = new System.Drawing.Size(360, 25);
            this.m_TableLayoutPanelPTU.TabIndex = 0;
            // 
            // m_LegendVersionPTU
            // 
            this.m_LegendVersionPTU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendVersionPTU.AutoSize = true;
            this.m_LegendVersionPTU.Location = new System.Drawing.Point(4, 7);
            this.m_LegendVersionPTU.Name = "m_LegendVersionPTU";
            this.m_LegendVersionPTU.Size = new System.Drawing.Size(45, 13);
            this.m_LegendVersionPTU.TabIndex = 0;
            this.m_LegendVersionPTU.Text = "Version:";
            // 
            // m_LabelVersionPTU
            // 
            this.m_LabelVersionPTU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelVersionPTU.AutoSize = true;
            this.m_LabelVersionPTU.Location = new System.Drawing.Point(115, 7);
            this.m_LabelVersionPTU.Name = "m_LabelVersionPTU";
            this.m_LabelVersionPTU.Size = new System.Drawing.Size(54, 13);
            this.m_LabelVersionPTU.TabIndex = 0;
            this.m_LabelVersionPTU.Text = "<Version>";
            // 
            // m_GroupBoxDD
            // 
            this.m_GroupBoxDD.Controls.Add(this.m_TableLayoutPanelDD);
            this.m_GroupBoxDD.Location = new System.Drawing.Point(10, 142);
            this.m_GroupBoxDD.Name = "m_GroupBoxDD";
            this.m_GroupBoxDD.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxDD.Size = new System.Drawing.Size(380, 115);
            this.m_GroupBoxDD.TabIndex = 0;
            this.m_GroupBoxDD.TabStop = false;
            this.m_GroupBoxDD.Text = "Data Dictionary";
            // 
            // m_TableLayoutPanelDD
            // 
            this.m_TableLayoutPanelDD.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelDD.ColumnCount = 2;
            this.m_TableLayoutPanelDD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.m_TableLayoutPanelDD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelDD.Controls.Add(this.m_LegendVersionDD, 0, 1);
            this.m_TableLayoutPanelDD.Controls.Add(this.m_LegendProjectIdentifierDD, 0, 2);
            this.m_TableLayoutPanelDD.Controls.Add(this.m_LabelProjectIdentifierDD, 1, 2);
            this.m_TableLayoutPanelDD.Controls.Add(this.m_LabelVersionDD, 1, 1);
            this.m_TableLayoutPanelDD.Controls.Add(this.m_LegendNameDD, 0, 0);
            this.m_TableLayoutPanelDD.Controls.Add(this.m_LabelNameDD, 1, 0);
            this.m_TableLayoutPanelDD.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelDD.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelDD.Name = "m_TableLayoutPanelDD";
            this.m_TableLayoutPanelDD.RowCount = 3;
            this.m_TableLayoutPanelDD.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDD.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDD.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDD.Size = new System.Drawing.Size(360, 80);
            this.m_TableLayoutPanelDD.TabIndex = 0;
            // 
            // m_LegendVersionDD
            // 
            this.m_LegendVersionDD.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendVersionDD.AutoSize = true;
            this.m_LegendVersionDD.Location = new System.Drawing.Point(4, 33);
            this.m_LegendVersionDD.Name = "m_LegendVersionDD";
            this.m_LegendVersionDD.Size = new System.Drawing.Size(45, 13);
            this.m_LegendVersionDD.TabIndex = 0;
            this.m_LegendVersionDD.Text = "Version:";
            // 
            // m_LegendProjectIdentifierDD
            // 
            this.m_LegendProjectIdentifierDD.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendProjectIdentifierDD.AutoSize = true;
            this.m_LegendProjectIdentifierDD.Location = new System.Drawing.Point(4, 59);
            this.m_LegendProjectIdentifierDD.Name = "m_LegendProjectIdentifierDD";
            this.m_LegendProjectIdentifierDD.Size = new System.Drawing.Size(86, 13);
            this.m_LegendProjectIdentifierDD.TabIndex = 2;
            this.m_LegendProjectIdentifierDD.Text = "Project Identifier:";
            // 
            // m_LabelProjectIdentifierDD
            // 
            this.m_LabelProjectIdentifierDD.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelProjectIdentifierDD.AutoSize = true;
            this.m_LabelProjectIdentifierDD.Location = new System.Drawing.Point(115, 59);
            this.m_LabelProjectIdentifierDD.Name = "m_LabelProjectIdentifierDD";
            this.m_LabelProjectIdentifierDD.Size = new System.Drawing.Size(95, 13);
            this.m_LabelProjectIdentifierDD.TabIndex = 4;
            this.m_LabelProjectIdentifierDD.Text = "<Project Identifier>";
            // 
            // m_LabelVersionDD
            // 
            this.m_LabelVersionDD.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelVersionDD.AutoSize = true;
            this.m_LabelVersionDD.Location = new System.Drawing.Point(115, 33);
            this.m_LabelVersionDD.Name = "m_LabelVersionDD";
            this.m_LabelVersionDD.Size = new System.Drawing.Size(54, 13);
            this.m_LabelVersionDD.TabIndex = 0;
            this.m_LabelVersionDD.Text = "<Version>";
            // 
            // m_LegendNameDD
            // 
            this.m_LegendNameDD.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendNameDD.AutoSize = true;
            this.m_LegendNameDD.Location = new System.Drawing.Point(4, 7);
            this.m_LegendNameDD.Name = "m_LegendNameDD";
            this.m_LegendNameDD.Size = new System.Drawing.Size(38, 13);
            this.m_LegendNameDD.TabIndex = 1;
            this.m_LegendNameDD.Text = "Name:";
            // 
            // m_LabelNameDD
            // 
            this.m_LabelNameDD.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelNameDD.AutoSize = true;
            this.m_LabelNameDD.Location = new System.Drawing.Point(115, 7);
            this.m_LabelNameDD.Name = "m_LabelNameDD";
            this.m_LabelNameDD.Size = new System.Drawing.Size(47, 13);
            this.m_LabelNameDD.TabIndex = 3;
            this.m_LabelNameDD.Text = "<Name>";
            // 
            // m_GroupBoxDDB
            // 
            this.m_GroupBoxDDB.Controls.Add(this.m_TableLayoutPanelDDB);
            this.m_GroupBoxDDB.Location = new System.Drawing.Point(10, 76);
            this.m_GroupBoxDDB.Name = "m_GroupBoxDDB";
            this.m_GroupBoxDDB.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxDDB.Size = new System.Drawing.Size(380, 60);
            this.m_GroupBoxDDB.TabIndex = 0;
            this.m_GroupBoxDDB.TabStop = false;
            this.m_GroupBoxDDB.Text = "Data Dictionary Builder";
            // 
            // m_TableLayoutPanelDDB
            // 
            this.m_TableLayoutPanelDDB.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelDDB.ColumnCount = 2;
            this.m_TableLayoutPanelDDB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.m_TableLayoutPanelDDB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelDDB.Controls.Add(this.m_LegendVersionDDB, 0, 0);
            this.m_TableLayoutPanelDDB.Controls.Add(this.m_LabelVersionDDB, 1, 0);
            this.m_TableLayoutPanelDDB.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelDDB.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelDDB.Name = "m_TableLayoutPanelDDB";
            this.m_TableLayoutPanelDDB.RowCount = 1;
            this.m_TableLayoutPanelDDB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_TableLayoutPanelDDB.Size = new System.Drawing.Size(360, 25);
            this.m_TableLayoutPanelDDB.TabIndex = 0;
            // 
            // m_LegendVersionDDB
            // 
            this.m_LegendVersionDDB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendVersionDDB.AutoSize = true;
            this.m_LegendVersionDDB.Location = new System.Drawing.Point(4, 6);
            this.m_LegendVersionDDB.Name = "m_LegendVersionDDB";
            this.m_LegendVersionDDB.Size = new System.Drawing.Size(45, 13);
            this.m_LegendVersionDDB.TabIndex = 0;
            this.m_LegendVersionDDB.Text = "Version:";
            // 
            // m_LabelVersionDDB
            // 
            this.m_LabelVersionDDB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelVersionDDB.AutoSize = true;
            this.m_LabelVersionDDB.Location = new System.Drawing.Point(115, 6);
            this.m_LabelVersionDDB.Name = "m_LabelVersionDDB";
            this.m_LabelVersionDDB.Size = new System.Drawing.Size(54, 13);
            this.m_LabelVersionDDB.TabIndex = 0;
            this.m_LabelVersionDDB.Text = "<Version>";
            // 
            // m_TabPageUserComments
            // 
            this.m_TabPageUserComments.Controls.Add(this.m_GroupBoxComments);
            this.m_TabPageUserComments.Controls.Add(this.m_GroupBoxUser);
            this.m_TabPageUserComments.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageUserComments.Name = "m_TabPageUserComments";
            this.m_TabPageUserComments.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_TabPageUserComments.Size = new System.Drawing.Size(400, 415);
            this.m_TabPageUserComments.TabIndex = 1;
            this.m_TabPageUserComments.Text = "User Comments";
            this.m_TabPageUserComments.UseVisualStyleBackColor = true;
            // 
            // m_GroupBoxComments
            // 
            this.m_GroupBoxComments.Controls.Add(this.m_TextBoxComments);
            this.m_GroupBoxComments.Location = new System.Drawing.Point(10, 131);
            this.m_GroupBoxComments.Name = "m_GroupBoxComments";
            this.m_GroupBoxComments.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxComments.Size = new System.Drawing.Size(378, 203);
            this.m_GroupBoxComments.TabIndex = 2;
            this.m_GroupBoxComments.TabStop = false;
            this.m_GroupBoxComments.Text = "Comments";
            // 
            // m_TextBoxComments
            // 
            this.m_TextBoxComments.AcceptsTab = true;
            this.m_TextBoxComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TextBoxComments.Location = new System.Drawing.Point(10, 23);
            this.m_TextBoxComments.Multiline = true;
            this.m_TextBoxComments.Name = "m_TextBoxComments";
            this.m_TextBoxComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_TextBoxComments.Size = new System.Drawing.Size(358, 168);
            this.m_TextBoxComments.TabIndex = 0;
            // 
            // m_GroupBoxUser
            // 
            this.m_GroupBoxUser.Controls.Add(this.m_TableLayoutPanelDateTime);
            this.m_GroupBoxUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupBoxUser.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxUser.Name = "m_GroupBoxUser";
            this.m_GroupBoxUser.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxUser.Size = new System.Drawing.Size(380, 115);
            this.m_GroupBoxUser.TabIndex = 1;
            this.m_GroupBoxUser.TabStop = false;
            // 
            // m_TableLayoutPanelDateTime
            // 
            this.m_TableLayoutPanelDateTime.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelDateTime.ColumnCount = 2;
            this.m_TableLayoutPanelDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.m_TableLayoutPanelDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LegendTime, 0, 2);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LegendDate, 0, 1);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LegendUser, 0, 0);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LabelUser, 1, 0);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LabelDate, 1, 1);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LabelTime, 1, 2);
            this.m_TableLayoutPanelDateTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelDateTime.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelDateTime.Name = "m_TableLayoutPanelDateTime";
            this.m_TableLayoutPanelDateTime.RowCount = 3;
            this.m_TableLayoutPanelDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDateTime.Size = new System.Drawing.Size(360, 80);
            this.m_TableLayoutPanelDateTime.TabIndex = 0;
            // 
            // m_LegendTime
            // 
            this.m_LegendTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendTime.AutoSize = true;
            this.m_LegendTime.Location = new System.Drawing.Point(4, 59);
            this.m_LegendTime.Name = "m_LegendTime";
            this.m_LegendTime.Size = new System.Drawing.Size(33, 13);
            this.m_LegendTime.TabIndex = 3;
            this.m_LegendTime.Text = "Time:";
            // 
            // m_LegendDate
            // 
            this.m_LegendDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendDate.AutoSize = true;
            this.m_LegendDate.Location = new System.Drawing.Point(4, 33);
            this.m_LegendDate.Name = "m_LegendDate";
            this.m_LegendDate.Size = new System.Drawing.Size(33, 13);
            this.m_LegendDate.TabIndex = 1;
            this.m_LegendDate.Text = "Date:";
            // 
            // m_LegendUser
            // 
            this.m_LegendUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendUser.AutoSize = true;
            this.m_LegendUser.Location = new System.Drawing.Point(4, 7);
            this.m_LegendUser.Name = "m_LegendUser";
            this.m_LegendUser.Size = new System.Drawing.Size(32, 13);
            this.m_LegendUser.TabIndex = 0;
            this.m_LegendUser.Text = "User:";
            // 
            // m_LabelUser
            // 
            this.m_LabelUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelUser.AutoSize = true;
            this.m_LabelUser.Location = new System.Drawing.Point(62, 7);
            this.m_LabelUser.Name = "m_LabelUser";
            this.m_LabelUser.Size = new System.Drawing.Size(41, 13);
            this.m_LabelUser.TabIndex = 0;
            this.m_LabelUser.Text = "<User>";
            // 
            // m_LabelDate
            // 
            this.m_LabelDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelDate.AutoSize = true;
            this.m_LabelDate.Location = new System.Drawing.Point(62, 33);
            this.m_LabelDate.Name = "m_LabelDate";
            this.m_LabelDate.Size = new System.Drawing.Size(42, 13);
            this.m_LabelDate.TabIndex = 2;
            this.m_LabelDate.Text = "<Date>";
            // 
            // m_LabelTime
            // 
            this.m_LabelTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelTime.AutoSize = true;
            this.m_LabelTime.Location = new System.Drawing.Point(62, 59);
            this.m_LabelTime.Name = "m_LabelTime";
            this.m_LabelTime.Size = new System.Drawing.Size(42, 13);
            this.m_LabelTime.TabIndex = 4;
            this.m_LabelTime.Text = "<Time>";
            // 
            // FormShowHeaderInformation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonOK;
            this.ClientSize = new System.Drawing.Size(428, 498);
            this.Controls.Add(this.m_TabControl);
            this.Controls.Add(this.m_ButtonOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShowHeaderInformation";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.Text = "Show Header Information";
            this.m_TabControl.ResumeLayout(false);
            this.m_TabPageGeneral.ResumeLayout(false);
            this.m_GroupBoxVCU.ResumeLayout(false);
            this.m_TableLayoutPanelVCU.ResumeLayout(false);
            this.m_TableLayoutPanelVCU.PerformLayout();
            this.m_GroupBoxPTU.ResumeLayout(false);
            this.m_TableLayoutPanelPTU.ResumeLayout(false);
            this.m_TableLayoutPanelPTU.PerformLayout();
            this.m_GroupBoxDD.ResumeLayout(false);
            this.m_TableLayoutPanelDD.ResumeLayout(false);
            this.m_TableLayoutPanelDD.PerformLayout();
            this.m_GroupBoxDDB.ResumeLayout(false);
            this.m_TableLayoutPanelDDB.ResumeLayout(false);
            this.m_TableLayoutPanelDDB.PerformLayout();
            this.m_TabPageUserComments.ResumeLayout(false);
            this.m_GroupBoxComments.ResumeLayout(false);
            this.m_GroupBoxComments.PerformLayout();
            this.m_GroupBoxUser.ResumeLayout(false);
            this.m_TableLayoutPanelDateTime.ResumeLayout(false);
            this.m_TableLayoutPanelDateTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_GroupBoxDDB;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelDDB;
        private System.Windows.Forms.Label m_LegendVersionDDB;
        private System.Windows.Forms.Label m_LabelVersionDDB;
        private System.Windows.Forms.GroupBox m_GroupBoxPTU;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelPTU;
        private System.Windows.Forms.Label m_LegendVersionPTU;
        private System.Windows.Forms.Label m_LabelVersionPTU;
        private System.Windows.Forms.Button m_ButtonOK;
        private System.Windows.Forms.GroupBox m_GroupBoxDD;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelDD;
        private System.Windows.Forms.Label m_LegendVersionDD;
        private System.Windows.Forms.Label m_LabelVersionDD;
        private System.Windows.Forms.GroupBox m_GroupBoxVCU;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelVCU;
        private System.Windows.Forms.Label m_LegendVersionVCU;
        private System.Windows.Forms.Label m_LabelVersionVCU;
        private System.Windows.Forms.Label m_LegendSubsystemVCU;
        private System.Windows.Forms.Label m_LegendProjectIdentifierVCU;
        private System.Windows.Forms.Label m_LabelSubsystemVCU;
        private System.Windows.Forms.Label m_LabelProjectIdentifierVCU;
        private System.Windows.Forms.Label m_LegendNameDD;
        private System.Windows.Forms.Label m_LegendProjectIdentifierDD;
        private System.Windows.Forms.Label m_LabelNameDD;
        private System.Windows.Forms.Label m_LabelProjectIdentifierDD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label m_LabelCarIdentifierVCU;
        private System.Windows.Forms.TabControl m_TabControl;
        private System.Windows.Forms.TabPage m_TabPageGeneral;
        private System.Windows.Forms.TabPage m_TabPageUserComments;
        private System.Windows.Forms.GroupBox m_GroupBoxUser;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelDateTime;
        private System.Windows.Forms.Label m_LegendDate;
        private System.Windows.Forms.Label m_LegendUser;
        private System.Windows.Forms.Label m_LabelUser;
        private System.Windows.Forms.GroupBox m_GroupBoxComments;
        private System.Windows.Forms.Label m_LabelDate;
        private System.Windows.Forms.TextBox m_TextBoxComments;
        private System.Windows.Forms.Label m_LegendTime;
        private System.Windows.Forms.Label m_LabelTime;
    }
}