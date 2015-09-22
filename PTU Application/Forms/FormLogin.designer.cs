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
 *  File name:  FormSecurity.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McDonald      First Release.
 * 
 *  09/29/10    1.1     K.McD           1. Added new Bombardier logo.
 *
 */
#endregion --- Revision History ---

namespace Bombardier.PTU.Forms
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.m_LegendPassword = new System.Windows.Forms.Label();
            this.m_TextBoxPassword = new System.Windows.Forms.TextBox();
            this.m_PanelLogo = new System.Windows.Forms.Panel();
            this.m_PictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.m_PanelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_LegendPassword
            // 
            this.m_LegendPassword.AutoSize = true;
            this.m_LegendPassword.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendPassword.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_LegendPassword.Location = new System.Drawing.Point(10, 154);
            this.m_LegendPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_LegendPassword.Name = "m_LegendPassword";
            this.m_LegendPassword.Size = new System.Drawing.Size(56, 13);
            this.m_LegendPassword.TabIndex = 5;
            this.m_LegendPassword.Text = "Password:";
            // 
            // m_TextBoxPassword
            // 
            this.m_TextBoxPassword.BackColor = System.Drawing.SystemColors.Window;
            this.m_TextBoxPassword.Location = new System.Drawing.Point(100, 151);
            this.m_TextBoxPassword.Margin = new System.Windows.Forms.Padding(2);
            this.m_TextBoxPassword.Name = "m_TextBoxPassword";
            this.m_TextBoxPassword.Size = new System.Drawing.Size(241, 20);
            this.m_TextBoxPassword.TabIndex = 1;
            this.m_TextBoxPassword.UseSystemPasswordChar = true;
            this.m_TextBoxPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TxtPassword_KeyPress);
            // 
            // m_PanelLogo
            // 
            this.m_PanelLogo.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelLogo.Controls.Add(this.m_PictureBoxLogo);
            this.m_PanelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelLogo.Location = new System.Drawing.Point(0, 0);
            this.m_PanelLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_PanelLogo.Name = "m_PanelLogo";
            this.m_PanelLogo.Size = new System.Drawing.Size(374, 139);
            this.m_PanelLogo.TabIndex = 2;
            // 
            // m_PictureBoxLogo
            // 
            this.m_PictureBoxLogo.Image = global::Bombardier.PTU.Properties.Resources.BombardierLogo;
            this.m_PictureBoxLogo.Location = new System.Drawing.Point(10, 3);
            this.m_PictureBoxLogo.Name = "m_PictureBoxLogo";
            this.m_PictureBoxLogo.Size = new System.Drawing.Size(334, 130);
            this.m_PictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_PictureBoxLogo.TabIndex = 0;
            this.m_PictureBoxLogo.TabStop = false;
            // 
            // FormLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(374, 191);
            this.Controls.Add(this.m_TextBoxPassword);
            this.Controls.Add(this.m_LegendPassword);
            this.Controls.Add(this.m_PanelLogo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "FormLogin";
            this.Text = "Login";
            this.Shown += new System.EventHandler(this.FormLogin_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormAccount_KeyPress);
            this.m_PanelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_LegendPassword;
        private System.Windows.Forms.TextBox m_TextBoxPassword;
        private System.Windows.Forms.Panel m_PanelLogo;
        private System.Windows.Forms.PictureBox m_PictureBoxLogo;
    }
}