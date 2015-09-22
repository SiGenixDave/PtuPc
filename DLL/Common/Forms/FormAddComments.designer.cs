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
 *  Project:    PTU Application
 * 
 *  File name:  FormAddComments.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/30/10    1.1     K.McD           1.  Added revision history.
 * 
 *  01/31/11    1.2     K.McD           1.  Modified the names of a number of variables.
 *
 */
#endregion --- Revision History ---

namespace Common.Forms
{
    partial class FormAddComments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddComments));
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_GroupBoxComments = new System.Windows.Forms.GroupBox();
            this.m_TextBoxComments = new System.Windows.Forms.TextBox();
            this.m_GroupBoxUserName = new System.Windows.Forms.GroupBox();
            this.m_TextBoxUserName = new System.Windows.Forms.TextBox();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxComments.SuspendLayout();
            this.m_GroupBoxUserName.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonOK.Location = new System.Drawing.Point(264, 360);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonOK.TabIndex = 1;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_BtnOK_Click);
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxComments);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxUserName);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_PanelOuter.Size = new System.Drawing.Size(347, 349);
            this.m_PanelOuter.TabIndex = 2;
            // 
            // m_GroupBoxComments
            // 
            this.m_GroupBoxComments.Controls.Add(this.m_TextBoxComments);
            this.m_GroupBoxComments.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_GroupBoxComments.Location = new System.Drawing.Point(10, 64);
            this.m_GroupBoxComments.Name = "m_GroupBoxComments";
            this.m_GroupBoxComments.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxComments.Size = new System.Drawing.Size(327, 273);
            this.m_GroupBoxComments.TabIndex = 3;
            this.m_GroupBoxComments.TabStop = false;
            this.m_GroupBoxComments.Text = "Comments (Ctrl-Tab to Exit)";
            // 
            // m_TextBoxComments
            // 
            this.m_TextBoxComments.AcceptsReturn = true;
            this.m_TextBoxComments.AcceptsTab = true;
            this.m_TextBoxComments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_TextBoxComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TextBoxComments.Location = new System.Drawing.Point(10, 23);
            this.m_TextBoxComments.Multiline = true;
            this.m_TextBoxComments.Name = "m_TextBoxComments";
            this.m_TextBoxComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_TextBoxComments.Size = new System.Drawing.Size(307, 238);
            this.m_TextBoxComments.TabIndex = 1;
            // 
            // m_GroupBoxUserName
            // 
            this.m_GroupBoxUserName.Controls.Add(this.m_TextBoxUserName);
            this.m_GroupBoxUserName.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupBoxUserName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_GroupBoxUserName.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxUserName.Name = "m_GroupBoxUserName";
            this.m_GroupBoxUserName.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxUserName.Size = new System.Drawing.Size(327, 48);
            this.m_GroupBoxUserName.TabIndex = 2;
            this.m_GroupBoxUserName.TabStop = false;
            this.m_GroupBoxUserName.Text = "Name";
            // 
            // m_TextBoxUserName
            // 
            this.m_TextBoxUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.m_TextBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_TextBoxUserName.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TextBoxUserName.Location = new System.Drawing.Point(10, 23);
            this.m_TextBoxUserName.Name = "m_TextBoxUserName";
            this.m_TextBoxUserName.Size = new System.Drawing.Size(307, 13);
            this.m_TextBoxUserName.TabIndex = 1;
            // 
            // FormAddComments
            // 
            this.AcceptButton = this.m_ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.m_ButtonOK;
            this.ClientSize = new System.Drawing.Size(347, 396);
            this.Controls.Add(this.m_PanelOuter);
            this.Controls.Add(this.m_ButtonOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddComments";
            this.Text = "Add User Comments";
            this.Shown += new System.EventHandler(this.FormAddComments_Shown);
            this.m_PanelOuter.ResumeLayout(false);
            this.m_GroupBoxComments.ResumeLayout(false);
            this.m_GroupBoxComments.PerformLayout();
            this.m_GroupBoxUserName.ResumeLayout(false);
            this.m_GroupBoxUserName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_TextBoxUserName;
        private System.Windows.Forms.TextBox m_TextBoxComments;
        private System.Windows.Forms.Button m_ButtonOK;
        private System.Windows.Forms.Panel m_PanelOuter;
        private System.Windows.Forms.GroupBox m_GroupBoxComments;
        private System.Windows.Forms.GroupBox m_GroupBoxUserName;

    }
}