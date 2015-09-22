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
 *  File name:  FormSetSecurityLevel.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/23/10    1.0     K.McDonald      1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

namespace Common.Forms
{
    partial class FormSetSecurityLevel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetSecurityLevel));
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_LabelWorkset = new System.Windows.Forms.Label();
            this.m_LegendWorkset = new System.Windows.Forms.Label();
            this.m_LegendSecurityLevel = new System.Windows.Forms.Label();
            this.m_ComboBoxSecurityLevel = new System.Windows.Forms.ComboBox();
            this.m_PanelOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Location = new System.Drawing.Point(228, 95);
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
            this.m_ButtonOK.Location = new System.Drawing.Point(149, 95);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonOK.TabIndex = 2;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_LabelWorkset);
            this.m_PanelOuter.Controls.Add(this.m_LegendWorkset);
            this.m_PanelOuter.Controls.Add(this.m_LegendSecurityLevel);
            this.m_PanelOuter.Controls.Add(this.m_ComboBoxSecurityLevel);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Size = new System.Drawing.Size(316, 84);
            this.m_PanelOuter.TabIndex = 1;
            this.m_PanelOuter.TabStop = true;
            // 
            // m_LabelWorkset
            // 
            this.m_LabelWorkset.AutoSize = true;
            this.m_LabelWorkset.Location = new System.Drawing.Point(107, 20);
            this.m_LabelWorkset.Name = "m_LabelWorkset";
            this.m_LabelWorkset.Size = new System.Drawing.Size(35, 13);
            this.m_LabelWorkset.TabIndex = 2;
            this.m_LabelWorkset.Text = "label1";
            // 
            // m_LegendWorkset
            // 
            this.m_LegendWorkset.AutoSize = true;
            this.m_LegendWorkset.Location = new System.Drawing.Point(10, 20);
            this.m_LegendWorkset.Name = "m_LegendWorkset";
            this.m_LegendWorkset.Size = new System.Drawing.Size(50, 13);
            this.m_LegendWorkset.TabIndex = 0;
            this.m_LegendWorkset.Text = "Workset:";
            // 
            // m_LegendSecurityLevel
            // 
            this.m_LegendSecurityLevel.AutoSize = true;
            this.m_LegendSecurityLevel.Location = new System.Drawing.Point(10, 46);
            this.m_LegendSecurityLevel.Name = "m_LegendSecurityLevel";
            this.m_LegendSecurityLevel.Size = new System.Drawing.Size(74, 13);
            this.m_LegendSecurityLevel.TabIndex = 1;
            this.m_LegendSecurityLevel.Text = "&Security Level";
            // 
            // m_ComboBoxSecurityLevel
            // 
            this.m_ComboBoxSecurityLevel.FormattingEnabled = true;
            this.m_ComboBoxSecurityLevel.Location = new System.Drawing.Point(110, 43);
            this.m_ComboBoxSecurityLevel.Name = "m_ComboBoxSecurityLevel";
            this.m_ComboBoxSecurityLevel.Size = new System.Drawing.Size(191, 21);
            this.m_ComboBoxSecurityLevel.TabIndex = 1;
            // 
            // FormSetSecurityLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(316, 131);
            this.Controls.Add(this.m_ButtonCancel);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_PanelOuter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSetSecurityLevel";
            this.Text = "Set Workset Security Level";
            this.m_PanelOuter.ResumeLayout(false);
            this.m_PanelOuter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox m_ComboBoxSecurityLevel;
        private System.Windows.Forms.Label m_LegendSecurityLevel;
        private System.Windows.Forms.Panel m_PanelOuter;
        private System.Windows.Forms.Button m_ButtonOK;
        private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.Label m_LegendWorkset;
        private System.Windows.Forms.Label m_LabelWorkset;
    }
}