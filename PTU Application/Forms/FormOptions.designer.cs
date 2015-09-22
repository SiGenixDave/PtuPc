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
 *  File name:  FormPTUOptions.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

using System;

namespace Bombardier.PTU.Forms
{
    partial class FormOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_FontDialog = new System.Windows.Forms.FontDialog();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_TabPageThirdPartySoftware = new System.Windows.Forms.TabPage();
            this.m_GroupBoxBrowser = new System.Windows.Forms.GroupBox();
            this.m_TextBoxBrowser = new System.Windows.Forms.TextBox();
            this.m_ButtonSpecifyBrowser = new System.Windows.Forms.Button();
            this.m_ButtonDefaultApplications = new System.Windows.Forms.Button();
            this.m_TabPageFont = new System.Windows.Forms.TabPage();
            this.m_ButtonDefaultFont = new System.Windows.Forms.Button();
            this.m_GroupBoxFonts = new System.Windows.Forms.GroupBox();
            this.m_LabelFontSize = new System.Windows.Forms.Label();
            this.m_LabelFontName = new System.Windows.Forms.Label();
            this.m_LegendSize = new System.Windows.Forms.Label();
            this.m_LegendFont = new System.Windows.Forms.Label();
            this.m_ButtonSelectFont = new System.Windows.Forms.Button();
            this.m_TabControl = new System.Windows.Forms.TabControl();
            this.m_TabPageThirdPartySoftware.SuspendLayout();
            this.m_GroupBoxBrowser.SuspendLayout();
            this.m_TabPageFont.SuspendLayout();
            this.m_GroupBoxFonts.SuspendLayout();
            this.m_TabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonOK.Location = new System.Drawing.Point(304, 297);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonOK.TabIndex = 1;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_BtnOK_Click);
            // 
            // m_FontDialog
            // 
            this.m_FontDialog.AllowScriptChange = false;
            this.m_FontDialog.AllowSimulations = false;
            this.m_FontDialog.AllowVectorFonts = false;
            this.m_FontDialog.AllowVerticalFonts = false;
            this.m_FontDialog.FontMustExist = true;
            this.m_FontDialog.MaxSize = 14;
            this.m_FontDialog.MinSize = 7;
            this.m_FontDialog.ScriptsOnly = true;
            this.m_FontDialog.ShowEffects = false;
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Location = new System.Drawing.Point(383, 297);
            this.m_ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            this.m_ButtonCancel.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonCancel.TabIndex = 0;
            this.m_ButtonCancel.TabStop = false;
            this.m_ButtonCancel.Text = "Cancel";
            this.m_ButtonCancel.UseVisualStyleBackColor = true;
            this.m_ButtonCancel.Click += new System.EventHandler(this.m_BtnCancel_Click);
            // 
            // m_TabPageThirdPartySoftware
            // 
            this.m_TabPageThirdPartySoftware.BackColor = System.Drawing.SystemColors.Window;
            this.m_TabPageThirdPartySoftware.Controls.Add(this.m_GroupBoxBrowser);
            this.m_TabPageThirdPartySoftware.Controls.Add(this.m_ButtonDefaultApplications);
            this.m_TabPageThirdPartySoftware.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageThirdPartySoftware.Name = "m_TabPageThirdPartySoftware";
            this.m_TabPageThirdPartySoftware.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_TabPageThirdPartySoftware.Size = new System.Drawing.Size(442, 250);
            this.m_TabPageThirdPartySoftware.TabIndex = 2;
            this.m_TabPageThirdPartySoftware.Text = "Third Party Software";
            // 
            // m_GroupBoxBrowser
            // 
            this.m_GroupBoxBrowser.BackColor = System.Drawing.Color.Transparent;
            this.m_GroupBoxBrowser.Controls.Add(this.m_TextBoxBrowser);
            this.m_GroupBoxBrowser.Controls.Add(this.m_ButtonSpecifyBrowser);
            this.m_GroupBoxBrowser.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupBoxBrowser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_GroupBoxBrowser.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxBrowser.Name = "m_GroupBoxBrowser";
            this.m_GroupBoxBrowser.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxBrowser.Size = new System.Drawing.Size(422, 55);
            this.m_GroupBoxBrowser.TabIndex = 2;
            this.m_GroupBoxBrowser.TabStop = false;
            this.m_GroupBoxBrowser.Text = "Browser";
            // 
            // m_TextBoxBrowser
            // 
            this.m_TextBoxBrowser.Location = new System.Drawing.Point(10, 20);
            this.m_TextBoxBrowser.Name = "m_TextBoxBrowser";
            this.m_TextBoxBrowser.ReadOnly = true;
            this.m_TextBoxBrowser.Size = new System.Drawing.Size(353, 20);
            this.m_TextBoxBrowser.TabIndex = 0;
            this.m_TextBoxBrowser.TabStop = false;
            // 
            // m_ButtonSpecifyBrowser
            // 
            this.m_ButtonSpecifyBrowser.BackColor = System.Drawing.Color.White;
            this.m_ButtonSpecifyBrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_ButtonSpecifyBrowser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_ButtonSpecifyBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ButtonSpecifyBrowser.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_ButtonSpecifyBrowser.Image = global::Bombardier.PTU.Properties.Resources.Modify;
            this.m_ButtonSpecifyBrowser.Location = new System.Drawing.Point(383, 15);
            this.m_ButtonSpecifyBrowser.Name = "m_ButtonSpecifyBrowser";
            this.m_ButtonSpecifyBrowser.Size = new System.Drawing.Size(26, 30);
            this.m_ButtonSpecifyBrowser.TabIndex = 1;
            this.m_ButtonSpecifyBrowser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_ButtonSpecifyBrowser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_ButtonSpecifyBrowser.UseVisualStyleBackColor = false;
            this.m_ButtonSpecifyBrowser.Click += new System.EventHandler(this.m_BtnSpecifyBrowser_Click);
            // 
            // m_ButtonDefaultApplications
            // 
            this.m_ButtonDefaultApplications.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_ButtonDefaultApplications.Location = new System.Drawing.Point(10, 210);
            this.m_ButtonDefaultApplications.Name = "m_ButtonDefaultApplications";
            this.m_ButtonDefaultApplications.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonDefaultApplications.TabIndex = 0;
            this.m_ButtonDefaultApplications.TabStop = false;
            this.m_ButtonDefaultApplications.Text = "&Default";
            this.m_ButtonDefaultApplications.UseVisualStyleBackColor = true;
            this.m_ButtonDefaultApplications.Click += new System.EventHandler(this.m_BtnDefaultApplications_Click);
            // 
            // m_TabPageFont
            // 
            this.m_TabPageFont.BackColor = System.Drawing.SystemColors.Window;
            this.m_TabPageFont.Controls.Add(this.m_ButtonDefaultFont);
            this.m_TabPageFont.Controls.Add(this.m_GroupBoxFonts);
            this.m_TabPageFont.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageFont.Name = "m_TabPageFont";
            this.m_TabPageFont.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_TabPageFont.Size = new System.Drawing.Size(442, 250);
            this.m_TabPageFont.TabIndex = 0;
            this.m_TabPageFont.Text = "Font";
            // 
            // m_ButtonDefaultFont
            // 
            this.m_ButtonDefaultFont.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_ButtonDefaultFont.Location = new System.Drawing.Point(10, 210);
            this.m_ButtonDefaultFont.Name = "m_ButtonDefaultFont";
            this.m_ButtonDefaultFont.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonDefaultFont.TabIndex = 0;
            this.m_ButtonDefaultFont.TabStop = false;
            this.m_ButtonDefaultFont.Text = "&Default";
            this.m_ButtonDefaultFont.UseVisualStyleBackColor = true;
            this.m_ButtonDefaultFont.Click += new System.EventHandler(this.m_BtnDefaultFont_Click);
            // 
            // m_GroupBoxFonts
            // 
            this.m_GroupBoxFonts.Controls.Add(this.m_LabelFontSize);
            this.m_GroupBoxFonts.Controls.Add(this.m_LabelFontName);
            this.m_GroupBoxFonts.Controls.Add(this.m_LegendSize);
            this.m_GroupBoxFonts.Controls.Add(this.m_LegendFont);
            this.m_GroupBoxFonts.Controls.Add(this.m_ButtonSelectFont);
            this.m_GroupBoxFonts.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupBoxFonts.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_GroupBoxFonts.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxFonts.Name = "m_GroupBoxFonts";
            this.m_GroupBoxFonts.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxFonts.Size = new System.Drawing.Size(422, 84);
            this.m_GroupBoxFonts.TabIndex = 0;
            this.m_GroupBoxFonts.TabStop = false;
            this.m_GroupBoxFonts.Text = "Font";
            // 
            // m_LabelFontSize
            // 
            this.m_LabelFontSize.AutoSize = true;
            this.m_LabelFontSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_LabelFontSize.Location = new System.Drawing.Point(292, 33);
            this.m_LabelFontSize.Name = "m_LabelFontSize";
            this.m_LabelFontSize.Size = new System.Drawing.Size(51, 13);
            this.m_LabelFontSize.TabIndex = 0;
            this.m_LabelFontSize.Text = "Font Size";
            // 
            // m_LabelFontName
            // 
            this.m_LabelFontName.AutoSize = true;
            this.m_LabelFontName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_LabelFontName.Location = new System.Drawing.Point(99, 33);
            this.m_LabelFontName.Name = "m_LabelFontName";
            this.m_LabelFontName.Size = new System.Drawing.Size(59, 13);
            this.m_LabelFontName.TabIndex = 0;
            this.m_LabelFontName.Text = "Font Name";
            // 
            // m_LegendSize
            // 
            this.m_LegendSize.AutoSize = true;
            this.m_LegendSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_LegendSize.Location = new System.Drawing.Point(247, 33);
            this.m_LegendSize.Name = "m_LegendSize";
            this.m_LegendSize.Size = new System.Drawing.Size(30, 13);
            this.m_LegendSize.TabIndex = 0;
            this.m_LegendSize.Text = "Size:";
            // 
            // m_LegendFont
            // 
            this.m_LegendFont.AutoSize = true;
            this.m_LegendFont.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_LegendFont.Location = new System.Drawing.Point(52, 33);
            this.m_LegendFont.Name = "m_LegendFont";
            this.m_LegendFont.Size = new System.Drawing.Size(31, 13);
            this.m_LegendFont.TabIndex = 0;
            this.m_LegendFont.Text = "&Font:";
            // 
            // m_ButtonSelectFont
            // 
            this.m_ButtonSelectFont.BackColor = System.Drawing.Color.White;
            this.m_ButtonSelectFont.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_ButtonSelectFont.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_ButtonSelectFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ButtonSelectFont.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_ButtonSelectFont.Image = global::Bombardier.PTU.Properties.Resources.Font;
            this.m_ButtonSelectFont.Location = new System.Drawing.Point(10, 23);
            this.m_ButtonSelectFont.Name = "m_ButtonSelectFont";
            this.m_ButtonSelectFont.Size = new System.Drawing.Size(26, 30);
            this.m_ButtonSelectFont.TabIndex = 1;
            this.m_ButtonSelectFont.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_ButtonSelectFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_ButtonSelectFont.UseVisualStyleBackColor = false;
            this.m_ButtonSelectFont.Click += new System.EventHandler(this.m_BtnSelectFont_Click);
            // 
            // m_TabControl
            // 
            this.m_TabControl.Controls.Add(this.m_TabPageFont);
            this.m_TabControl.Controls.Add(this.m_TabPageThirdPartySoftware);
            this.m_TabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TabControl.Location = new System.Drawing.Point(10, 10);
            this.m_TabControl.Name = "m_TabControl";
            this.m_TabControl.SelectedIndex = 0;
            this.m_TabControl.Size = new System.Drawing.Size(450, 276);
            this.m_TabControl.TabIndex = 2;
            this.m_TabControl.SelectedIndexChanged += new System.EventHandler(this.m_TabControl_SelectedIndexChanged);
            // 
            // FormOptions
            // 
            this.AcceptButton = this.m_ButtonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(470, 333);
            this.Controls.Add(this.m_ButtonCancel);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_TabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormOptions";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.Text = "Options";
            this.m_TabPageThirdPartySoftware.ResumeLayout(false);
            this.m_GroupBoxBrowser.ResumeLayout(false);
            this.m_GroupBoxBrowser.PerformLayout();
            this.m_TabPageFont.ResumeLayout(false);
            this.m_GroupBoxFonts.ResumeLayout(false);
            this.m_GroupBoxFonts.PerformLayout();
            this.m_TabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_ButtonOK;
        private System.Windows.Forms.FontDialog m_FontDialog;
        private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.TabPage m_TabPageThirdPartySoftware;
        private System.Windows.Forms.GroupBox m_GroupBoxBrowser;
        private System.Windows.Forms.TextBox m_TextBoxBrowser;
        private System.Windows.Forms.Button m_ButtonSpecifyBrowser;
        private System.Windows.Forms.Button m_ButtonDefaultApplications;
        private System.Windows.Forms.TabPage m_TabPageFont;
        private System.Windows.Forms.Button m_ButtonDefaultFont;
        private System.Windows.Forms.GroupBox m_GroupBoxFonts;
        private System.Windows.Forms.Label m_LegendSize;
        private System.Windows.Forms.Label m_LegendFont;
        private System.Windows.Forms.Label m_LabelFontSize;
        private System.Windows.Forms.Label m_LabelFontName;
        private System.Windows.Forms.Button m_ButtonSelectFont;
        private System.Windows.Forms.TabControl m_TabControl;
    }
}