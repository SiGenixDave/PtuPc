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
 *  File name:  FormConfigurePasswords.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  06/02/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

namespace Bombardier.PTU.Forms
{
    partial class FormConfigurePasswordProtection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfigurePasswordProtection));
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_GroupBoxSecurityLevel = new System.Windows.Forms.GroupBox();
            this.m_ComboBoxSecurityLevelDescription = new System.Windows.Forms.ComboBox();
            this.m_GroupBoxPassword = new System.Windows.Forms.GroupBox();
            this.m_GroupBoxVerifyPassword = new System.Windows.Forms.GroupBox();
            this.m_LegendVerifyPassword = new System.Windows.Forms.Label();
            this.m_LegendNewPassword = new System.Windows.Forms.Label();
            this.m_TextBoxVerifyPassword = new System.Windows.Forms.TextBox();
            this.m_TextBoxNewPassword = new System.Windows.Forms.TextBox();
            this.m_RadioButtonNewPassword = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonResetPassword = new System.Windows.Forms.RadioButton();
            this.m_ButtonApply = new System.Windows.Forms.Button();
            this.m_ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxSecurityLevel.SuspendLayout();
            this.m_GroupBoxPassword.SuspendLayout();
            this.m_GroupBoxVerifyPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Location = new System.Drawing.Point(272, 256);
            this.m_ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            this.m_ButtonCancel.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonCancel.TabIndex = 0;
            this.m_ButtonCancel.TabStop = false;
            this.m_ButtonCancel.Text = "Cancel";
            this.m_ButtonCancel.UseVisualStyleBackColor = true;
            this.m_ButtonCancel.Click += new System.EventHandler(this.m_BtnCancel_Click);
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxSecurityLevel);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxPassword);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_PanelOuter.Size = new System.Drawing.Size(356, 245);
            this.m_PanelOuter.TabIndex = 1;
            this.m_PanelOuter.TabStop = true;
            // 
            // m_GroupBoxSecurityLevel
            // 
            this.m_GroupBoxSecurityLevel.Controls.Add(this.m_ComboBoxSecurityLevelDescription);
            this.m_GroupBoxSecurityLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupBoxSecurityLevel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_GroupBoxSecurityLevel.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxSecurityLevel.Name = "m_GroupBoxSecurityLevel";
            this.m_GroupBoxSecurityLevel.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxSecurityLevel.Size = new System.Drawing.Size(336, 58);
            this.m_GroupBoxSecurityLevel.TabIndex = 1;
            this.m_GroupBoxSecurityLevel.TabStop = false;
            this.m_GroupBoxSecurityLevel.Text = "Security Level";
            // 
            // m_ComboBoxSecurityLevelDescription
            // 
            this.m_ComboBoxSecurityLevelDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_ComboBoxSecurityLevelDescription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxSecurityLevelDescription.FormattingEnabled = true;
            this.m_ComboBoxSecurityLevelDescription.Location = new System.Drawing.Point(10, 23);
            this.m_ComboBoxSecurityLevelDescription.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_ComboBoxSecurityLevelDescription.Name = "m_ComboBoxSecurityLevelDescription";
            this.m_ComboBoxSecurityLevelDescription.Size = new System.Drawing.Size(316, 21);
            this.m_ComboBoxSecurityLevelDescription.TabIndex = 1;
            // 
            // m_GroupBoxPassword
            // 
            this.m_GroupBoxPassword.Controls.Add(this.m_GroupBoxVerifyPassword);
            this.m_GroupBoxPassword.Controls.Add(this.m_RadioButtonNewPassword);
            this.m_GroupBoxPassword.Controls.Add(this.m_RadioButtonResetPassword);
            this.m_GroupBoxPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_GroupBoxPassword.Location = new System.Drawing.Point(10, 74);
            this.m_GroupBoxPassword.Name = "m_GroupBoxPassword";
            this.m_GroupBoxPassword.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxPassword.Size = new System.Drawing.Size(334, 159);
            this.m_GroupBoxPassword.TabIndex = 2;
            this.m_GroupBoxPassword.TabStop = false;
            this.m_GroupBoxPassword.Text = "Password";
            // 
            // m_GroupBoxVerifyPassword
            // 
            this.m_GroupBoxVerifyPassword.Controls.Add(this.m_LegendVerifyPassword);
            this.m_GroupBoxVerifyPassword.Controls.Add(this.m_LegendNewPassword);
            this.m_GroupBoxVerifyPassword.Controls.Add(this.m_TextBoxVerifyPassword);
            this.m_GroupBoxVerifyPassword.Controls.Add(this.m_TextBoxNewPassword);
            this.m_GroupBoxVerifyPassword.Enabled = false;
            this.m_GroupBoxVerifyPassword.Location = new System.Drawing.Point(10, 71);
            this.m_GroupBoxVerifyPassword.Name = "m_GroupBoxVerifyPassword";
            this.m_GroupBoxVerifyPassword.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxVerifyPassword.Size = new System.Drawing.Size(314, 76);
            this.m_GroupBoxVerifyPassword.TabIndex = 2;
            this.m_GroupBoxVerifyPassword.TabStop = false;
            this.m_GroupBoxVerifyPassword.Text = "Test";
            // 
            // m_LegendVerifyPassword
            // 
            this.m_LegendVerifyPassword.AutoSize = true;
            this.m_LegendVerifyPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LegendVerifyPassword.Location = new System.Drawing.Point(10, 47);
            this.m_LegendVerifyPassword.Name = "m_LegendVerifyPassword";
            this.m_LegendVerifyPassword.Size = new System.Drawing.Size(82, 13);
            this.m_LegendVerifyPassword.TabIndex = 3;
            this.m_LegendVerifyPassword.Text = "Verify Password";
            // 
            // m_LegendNewPassword
            // 
            this.m_LegendNewPassword.AutoSize = true;
            this.m_LegendNewPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LegendNewPassword.Location = new System.Drawing.Point(10, 23);
            this.m_LegendNewPassword.Name = "m_LegendNewPassword";
            this.m_LegendNewPassword.Size = new System.Drawing.Size(78, 13);
            this.m_LegendNewPassword.TabIndex = 2;
            this.m_LegendNewPassword.Text = "New Password";
            // 
            // m_TextBoxVerifyPassword
            // 
            this.m_TextBoxVerifyPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_TextBoxVerifyPassword.Location = new System.Drawing.Point(108, 44);
            this.m_TextBoxVerifyPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_TextBoxVerifyPassword.Name = "m_TextBoxVerifyPassword";
            this.m_TextBoxVerifyPassword.Size = new System.Drawing.Size(196, 20);
            this.m_TextBoxVerifyPassword.TabIndex = 2;
            this.m_TextBoxVerifyPassword.UseSystemPasswordChar = true;
            this.m_TextBoxVerifyPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TxtVerify_KeyPress);
            this.m_TextBoxVerifyPassword.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // m_TextBoxNewPassword
            // 
            this.m_TextBoxNewPassword.AcceptsReturn = true;
            this.m_TextBoxNewPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_TextBoxNewPassword.Location = new System.Drawing.Point(108, 20);
            this.m_TextBoxNewPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_TextBoxNewPassword.Name = "m_TextBoxNewPassword";
            this.m_TextBoxNewPassword.Size = new System.Drawing.Size(196, 20);
            this.m_TextBoxNewPassword.TabIndex = 1;
            this.m_TextBoxNewPassword.UseSystemPasswordChar = true;
            this.m_TextBoxNewPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TxtNew_KeyPress);
            this.m_TextBoxNewPassword.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // m_RadioButtonNewPassword
            // 
            this.m_RadioButtonNewPassword.AutoSize = true;
            this.m_RadioButtonNewPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_RadioButtonNewPassword.Location = new System.Drawing.Point(10, 49);
            this.m_RadioButtonNewPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_RadioButtonNewPassword.Name = "m_RadioButtonNewPassword";
            this.m_RadioButtonNewPassword.Size = new System.Drawing.Size(130, 17);
            this.m_RadioButtonNewPassword.TabIndex = 1;
            this.m_RadioButtonNewPassword.TabStop = true;
            this.m_RadioButtonNewPassword.Text = "&Create New Password";
            this.m_RadioButtonNewPassword.UseVisualStyleBackColor = true;
            // 
            // m_RadioButtonResetPassword
            // 
            this.m_RadioButtonResetPassword.AutoSize = true;
            this.m_RadioButtonResetPassword.Checked = true;
            this.m_RadioButtonResetPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_RadioButtonResetPassword.Location = new System.Drawing.Point(10, 28);
            this.m_RadioButtonResetPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_RadioButtonResetPassword.Name = "m_RadioButtonResetPassword";
            this.m_RadioButtonResetPassword.Size = new System.Drawing.Size(102, 17);
            this.m_RadioButtonResetPassword.TabIndex = 1;
            this.m_RadioButtonResetPassword.TabStop = true;
            this.m_RadioButtonResetPassword.Text = "&Reset to Default";
            this.m_RadioButtonResetPassword.UseVisualStyleBackColor = true;
            this.m_RadioButtonResetPassword.CheckedChanged += new System.EventHandler(this.m_RadioReset_CheckedChanged);
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(193, 256);
            this.m_ButtonApply.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonApply.Name = "m_ButtonApply";
            this.m_ButtonApply.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonApply.TabIndex = 0;
            this.m_ButtonApply.TabStop = false;
            this.m_ButtonApply.Text = "&Apply";
            this.m_ButtonApply.UseVisualStyleBackColor = true;
            this.m_ButtonApply.Click += new System.EventHandler(this.m_BtnApply_Click);
            // 
            // m_ErrorProvider
            // 
            this.m_ErrorProvider.ContainerControl = this;
            // 
            // FormConfigurePassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(356, 292);
            this.Controls.Add(this.m_PanelOuter);
            this.Controls.Add(this.m_ButtonApply);
            this.Controls.Add(this.m_ButtonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormConfigurePassword";
            this.Text = "Configure System Passwords";
            this.m_PanelOuter.ResumeLayout(false);
            this.m_GroupBoxSecurityLevel.ResumeLayout(false);
            this.m_GroupBoxPassword.ResumeLayout(false);
            this.m_GroupBoxPassword.PerformLayout();
            this.m_GroupBoxVerifyPassword.ResumeLayout(false);
            this.m_GroupBoxVerifyPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox m_ComboBoxSecurityLevelDescription;
        private System.Windows.Forms.GroupBox m_GroupBoxPassword;
        private System.Windows.Forms.TextBox m_TextBoxNewPassword;
        private System.Windows.Forms.RadioButton m_RadioButtonResetPassword;
        private System.Windows.Forms.RadioButton m_RadioButtonNewPassword;
        private System.Windows.Forms.Button m_ButtonApply;
        private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.TextBox m_TextBoxVerifyPassword;
        private System.Windows.Forms.ErrorProvider m_ErrorProvider;
        private System.Windows.Forms.Label m_LegendVerifyPassword;
        private System.Windows.Forms.Label m_LegendNewPassword;
        private System.Windows.Forms.GroupBox m_GroupBoxVerifyPassword;
        private System.Windows.Forms.GroupBox m_GroupBoxSecurityLevel;
        private System.Windows.Forms.Panel m_PanelOuter;
    }
}