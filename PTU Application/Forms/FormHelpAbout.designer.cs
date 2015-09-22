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
 *  File name:  FormHelpAbout.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/30/10    1.0     K.McD           1.  First entry into TortoiseSVN. 
 * 
 *  09/29/10    1.1     K.McD           1.  Added the new Bombardier logo.
 *  
 *  10/10/11    1.2     K.McD           1.  Renamed the LinkLabel control that takes the user to the Bombardier web site.
 *                                      2.  Added a LinkLabel control to display the release notes.
 *                                      
 *  08/05/13    1.3     K.McD           1.  Replaced the 'Operating System' legend with 'O.S.' to prevent an overlap when using slightly larger fonts.
 *
 */
#endregion --- Revision History ---

using System;

namespace Bombardier.PTU.Forms
{
    partial class FormHelpAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHelpAbout));
            this.m_BtnOK = new System.Windows.Forms.Button();
            this.m_PanelBackground = new System.Windows.Forms.Panel();
            this.m_LinkLabelBombardierTransportation = new System.Windows.Forms.LinkLabel();
            this.m_GroupDataDictionary = new System.Windows.Forms.GroupBox();
            this.m_LinkLabelReleaseNotes = new System.Windows.Forms.LinkLabel();
            this.m_LblLitVersion = new System.Windows.Forms.Label();
            this.m_LblLitName = new System.Windows.Forms.Label();
            this.m_LblDataDictionaryVersion = new System.Windows.Forms.Label();
            this.m_LblDataDictionaryName = new System.Windows.Forms.Label();
            this.m_GroupCopyright = new System.Windows.Forms.GroupBox();
            this.m_LblCopyright = new System.Windows.Forms.Label();
            this.m_LblRights = new System.Windows.Forms.Label();
            this.m_GroupInformation = new System.Windows.Forms.GroupBox();
            this.m_LblLitOperatingSystem = new System.Windows.Forms.Label();
            this.m_LblOS = new System.Windows.Forms.Label();
            this.m_LblProductName = new System.Windows.Forms.Label();
            this.m_LblLiteralBuildNo = new System.Windows.Forms.Label();
            this.m_LblProductVersion = new System.Windows.Forms.Label();
            this.m_PictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.m_PanelBackground.SuspendLayout();
            this.m_GroupDataDictionary.SuspendLayout();
            this.m_GroupCopyright.SuspendLayout();
            this.m_GroupInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_BtnOK
            // 
            this.m_BtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_BtnOK.Location = new System.Drawing.Point(401, 369);
            this.m_BtnOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_BtnOK.Name = "m_BtnOK";
            this.m_BtnOK.Size = new System.Drawing.Size(73, 25);
            this.m_BtnOK.TabIndex = 1;
            this.m_BtnOK.Text = "OK";
            this.m_BtnOK.UseVisualStyleBackColor = true;
            this.m_BtnOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_PanelBackground
            // 
            this.m_PanelBackground.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelBackground.Controls.Add(this.m_LinkLabelBombardierTransportation);
            this.m_PanelBackground.Controls.Add(this.m_GroupDataDictionary);
            this.m_PanelBackground.Controls.Add(this.m_GroupCopyright);
            this.m_PanelBackground.Controls.Add(this.m_GroupInformation);
            this.m_PanelBackground.Controls.Add(this.m_PictureBoxLogo);
            this.m_PanelBackground.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelBackground.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_PanelBackground.Location = new System.Drawing.Point(0, 0);
            this.m_PanelBackground.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_PanelBackground.Name = "m_PanelBackground";
            this.m_PanelBackground.Size = new System.Drawing.Size(484, 359);
            this.m_PanelBackground.TabIndex = 2;
            // 
            // m_LinkLabelBombardierTransportation
            // 
            this.m_LinkLabelBombardierTransportation.AutoSize = true;
            this.m_LinkLabelBombardierTransportation.Location = new System.Drawing.Point(16, 80);
            this.m_LinkLabelBombardierTransportation.Name = "m_LinkLabelBombardierTransportation";
            this.m_LinkLabelBombardierTransportation.Size = new System.Drawing.Size(27, 13);
            this.m_LinkLabelBombardierTransportation.TabIndex = 2;
            this.m_LinkLabelBombardierTransportation.TabStop = true;
            this.m_LinkLabelBombardierTransportation.Text = "Link";
            this.m_LinkLabelBombardierTransportation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_LinkLabelBombardierTransportation_LinkClicked);
            // 
            // m_GroupDataDictionary
            // 
            this.m_GroupDataDictionary.Controls.Add(this.m_LinkLabelReleaseNotes);
            this.m_GroupDataDictionary.Controls.Add(this.m_LblLitVersion);
            this.m_GroupDataDictionary.Controls.Add(this.m_LblLitName);
            this.m_GroupDataDictionary.Controls.Add(this.m_LblDataDictionaryVersion);
            this.m_GroupDataDictionary.Controls.Add(this.m_LblDataDictionaryName);
            this.m_GroupDataDictionary.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_GroupDataDictionary.Location = new System.Drawing.Point(10, 211);
            this.m_GroupDataDictionary.Name = "m_GroupDataDictionary";
            this.m_GroupDataDictionary.Size = new System.Drawing.Size(464, 73);
            this.m_GroupDataDictionary.TabIndex = 0;
            this.m_GroupDataDictionary.TabStop = false;
            this.m_GroupDataDictionary.Text = "Project";
            // 
            // m_LinkLabelReleaseNotes
            // 
            this.m_LinkLabelReleaseNotes.ActiveLinkColor = System.Drawing.Color.Purple;
            this.m_LinkLabelReleaseNotes.AutoSize = true;
            this.m_LinkLabelReleaseNotes.Location = new System.Drawing.Point(12, 50);
            this.m_LinkLabelReleaseNotes.Name = "m_LinkLabelReleaseNotes";
            this.m_LinkLabelReleaseNotes.Size = new System.Drawing.Size(77, 13);
            this.m_LinkLabelReleaseNotes.TabIndex = 3;
            this.m_LinkLabelReleaseNotes.TabStop = true;
            this.m_LinkLabelReleaseNotes.Text = "Release Notes";
            this.m_LinkLabelReleaseNotes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_LinkLabelReleaseNotes_LinkClicked);
            // 
            // m_LblLitVersion
            // 
            this.m_LblLitVersion.AutoSize = true;
            this.m_LblLitVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblLitVersion.Location = new System.Drawing.Point(12, 35);
            this.m_LblLitVersion.Name = "m_LblLitVersion";
            this.m_LblLitVersion.Size = new System.Drawing.Size(45, 13);
            this.m_LblLitVersion.TabIndex = 0;
            this.m_LblLitVersion.Text = "Version:";
            // 
            // m_LblLitName
            // 
            this.m_LblLitName.AutoSize = true;
            this.m_LblLitName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblLitName.Location = new System.Drawing.Point(12, 20);
            this.m_LblLitName.Name = "m_LblLitName";
            this.m_LblLitName.Size = new System.Drawing.Size(38, 13);
            this.m_LblLitName.TabIndex = 0;
            this.m_LblLitName.Text = "Name:";
            // 
            // m_LblDataDictionaryVersion
            // 
            this.m_LblDataDictionaryVersion.AutoSize = true;
            this.m_LblDataDictionaryVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblDataDictionaryVersion.Location = new System.Drawing.Point(115, 35);
            this.m_LblDataDictionaryVersion.Name = "m_LblDataDictionaryVersion";
            this.m_LblDataDictionaryVersion.Size = new System.Drawing.Size(42, 13);
            this.m_LblDataDictionaryVersion.TabIndex = 0;
            this.m_LblDataDictionaryVersion.Text = "Version";
            // 
            // m_LblDataDictionaryName
            // 
            this.m_LblDataDictionaryName.AutoSize = true;
            this.m_LblDataDictionaryName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblDataDictionaryName.Location = new System.Drawing.Point(115, 20);
            this.m_LblDataDictionaryName.Name = "m_LblDataDictionaryName";
            this.m_LblDataDictionaryName.Size = new System.Drawing.Size(35, 13);
            this.m_LblDataDictionaryName.TabIndex = 0;
            this.m_LblDataDictionaryName.Text = "Name";
            // 
            // m_GroupCopyright
            // 
            this.m_GroupCopyright.Controls.Add(this.m_LblCopyright);
            this.m_GroupCopyright.Controls.Add(this.m_LblRights);
            this.m_GroupCopyright.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_GroupCopyright.Location = new System.Drawing.Point(10, 289);
            this.m_GroupCopyright.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_GroupCopyright.Name = "m_GroupCopyright";
            this.m_GroupCopyright.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_GroupCopyright.Size = new System.Drawing.Size(463, 58);
            this.m_GroupCopyright.TabIndex = 0;
            this.m_GroupCopyright.TabStop = false;
            this.m_GroupCopyright.Text = "Copyright";
            // 
            // m_LblCopyright
            // 
            this.m_LblCopyright.AutoSize = true;
            this.m_LblCopyright.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblCopyright.Location = new System.Drawing.Point(12, 20);
            this.m_LblCopyright.Name = "m_LblCopyright";
            this.m_LblCopyright.Size = new System.Drawing.Size(51, 13);
            this.m_LblCopyright.TabIndex = 0;
            this.m_LblCopyright.Text = "Copyright";
            // 
            // m_LblRights
            // 
            this.m_LblRights.AutoSize = true;
            this.m_LblRights.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblRights.Location = new System.Drawing.Point(12, 35);
            this.m_LblRights.Name = "m_LblRights";
            this.m_LblRights.Size = new System.Drawing.Size(37, 13);
            this.m_LblRights.TabIndex = 0;
            this.m_LblRights.Text = "Rights";
            // 
            // m_GroupInformation
            // 
            this.m_GroupInformation.Controls.Add(this.m_LblOS);
            this.m_GroupInformation.Controls.Add(this.m_LblLitOperatingSystem);
            this.m_GroupInformation.Controls.Add(this.m_LblProductName);
            this.m_GroupInformation.Controls.Add(this.m_LblLiteralBuildNo);
            this.m_GroupInformation.Controls.Add(this.m_LblProductVersion);
            this.m_GroupInformation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_GroupInformation.Location = new System.Drawing.Point(10, 123);
            this.m_GroupInformation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_GroupInformation.Name = "m_GroupInformation";
            this.m_GroupInformation.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_GroupInformation.Size = new System.Drawing.Size(464, 83);
            this.m_GroupInformation.TabIndex = 0;
            this.m_GroupInformation.TabStop = false;
            this.m_GroupInformation.Text = "Product Information";
            // 
            // m_LblLitOperatingSystem
            // 
            this.m_LblLitOperatingSystem.AutoEllipsis = true;
            this.m_LblLitOperatingSystem.AutoSize = true;
            this.m_LblLitOperatingSystem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblLitOperatingSystem.Location = new System.Drawing.Point(12, 60);
            this.m_LblLitOperatingSystem.Name = "m_LblLitOperatingSystem";
            this.m_LblLitOperatingSystem.Size = new System.Drawing.Size(34, 13);
            this.m_LblLitOperatingSystem.TabIndex = 0;
            this.m_LblLitOperatingSystem.Text = "O.S. :";
            // 
            // m_LblOS
            // 
            this.m_LblOS.AutoSize = true;
            this.m_LblOS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblOS.Location = new System.Drawing.Point(116, 60);
            this.m_LblOS.Name = "m_LblOS";
            this.m_LblOS.Size = new System.Drawing.Size(28, 13);
            this.m_LblOS.TabIndex = 0;
            this.m_LblOS.Text = "O.S.";
            // 
            // m_LblProductName
            // 
            this.m_LblProductName.AutoSize = true;
            this.m_LblProductName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblProductName.Location = new System.Drawing.Point(12, 20);
            this.m_LblProductName.Name = "m_LblProductName";
            this.m_LblProductName.Size = new System.Drawing.Size(35, 13);
            this.m_LblProductName.TabIndex = 0;
            this.m_LblProductName.Text = "Name";
            // 
            // m_LblLiteralBuildNo
            // 
            this.m_LblLiteralBuildNo.AutoSize = true;
            this.m_LblLiteralBuildNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblLiteralBuildNo.Location = new System.Drawing.Point(12, 45);
            this.m_LblLiteralBuildNo.Name = "m_LblLiteralBuildNo";
            this.m_LblLiteralBuildNo.Size = new System.Drawing.Size(53, 13);
            this.m_LblLiteralBuildNo.TabIndex = 0;
            this.m_LblLiteralBuildNo.Text = "Build No.:";
            // 
            // m_LblProductVersion
            // 
            this.m_LblProductVersion.AutoSize = true;
            this.m_LblProductVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LblProductVersion.Location = new System.Drawing.Point(116, 45);
            this.m_LblProductVersion.Name = "m_LblProductVersion";
            this.m_LblProductVersion.Size = new System.Drawing.Size(42, 13);
            this.m_LblProductVersion.TabIndex = 0;
            this.m_LblProductVersion.Text = "Version";
            // 
            // m_PictureBoxLogo
            // 
            this.m_PictureBoxLogo.Image = global::Bombardier.PTU.Properties.Resources.BombardierLogo;
            this.m_PictureBoxLogo.Location = new System.Drawing.Point(10, 3);
            this.m_PictureBoxLogo.Name = "m_PictureBoxLogo";
            this.m_PictureBoxLogo.Size = new System.Drawing.Size(319, 124);
            this.m_PictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_PictureBoxLogo.TabIndex = 3;
            this.m_PictureBoxLogo.TabStop = false;
            // 
            // FormHelpAbout
            // 
            this.AcceptButton = this.m_BtnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_BtnOK;
            this.ClientSize = new System.Drawing.Size(484, 405);
            this.Controls.Add(this.m_BtnOK);
            this.Controls.Add(this.m_PanelBackground);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHelpAbout";
            this.Text = "About the Portable Test Unit";
            this.m_PanelBackground.ResumeLayout(false);
            this.m_PanelBackground.PerformLayout();
            this.m_GroupDataDictionary.ResumeLayout(false);
            this.m_GroupDataDictionary.PerformLayout();
            this.m_GroupCopyright.ResumeLayout(false);
            this.m_GroupCopyright.PerformLayout();
            this.m_GroupInformation.ResumeLayout(false);
            this.m_GroupInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel m_PanelBackground;
        private System.Windows.Forms.Label m_LblProductName;
        private System.Windows.Forms.Label m_LblLiteralBuildNo;
        private System.Windows.Forms.Label m_LblCopyright;
        private System.Windows.Forms.Label m_LblRights;
        private System.Windows.Forms.Label m_LblProductVersion;
        private System.Windows.Forms.GroupBox m_GroupCopyright;
        private System.Windows.Forms.GroupBox m_GroupInformation;
        private System.Windows.Forms.Label m_LblOS;
        private System.Windows.Forms.GroupBox m_GroupDataDictionary;
        private System.Windows.Forms.Label m_LblDataDictionaryName;
        private System.Windows.Forms.Label m_LblDataDictionaryVersion;
        private System.Windows.Forms.LinkLabel m_LinkLabelBombardierTransportation;
        private System.Windows.Forms.Label m_LblLitName;
        private System.Windows.Forms.Label m_LblLitVersion;
        private System.Windows.Forms.Label m_LblLitOperatingSystem;
        private System.Windows.Forms.Button m_BtnOK;
        private System.Windows.Forms.PictureBox m_PictureBoxLogo;
        private System.Windows.Forms.LinkLabel m_LinkLabelReleaseNotes;
    }
}