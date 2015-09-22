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
 *  Project:    Communication
 * 
 *  File name:  FormSelectTargetLogic.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

namespace Bombardier.PTU.Forms
{
    partial class FormSelectTarget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectTarget));
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_TxtStatusInformation = new System.Windows.Forms.TextBox();
            this.m_GroupLogicList = new System.Windows.Forms.GroupBox();
            this.m_ListBoxAvailableLogicControllers = new System.Windows.Forms.ListBox();
            this.m_BtnOK = new System.Windows.Forms.Button();
            this.m_Cancel = new System.Windows.Forms.Button();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupLogicList.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_TxtStatusInformation);
            this.m_PanelOuter.Controls.Add(this.m_GroupLogicList);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_PanelOuter.Size = new System.Drawing.Size(476, 185);
            this.m_PanelOuter.TabIndex = 2;
            // 
            // m_TxtStatusInformation
            // 
            this.m_TxtStatusInformation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_TxtStatusInformation.Location = new System.Drawing.Point(10, 151);
            this.m_TxtStatusInformation.Name = "m_TxtStatusInformation";
            this.m_TxtStatusInformation.Size = new System.Drawing.Size(450, 13);
            this.m_TxtStatusInformation.TabIndex = 0;
            this.m_TxtStatusInformation.TabStop = false;
            // 
            // m_GroupLogicList
            // 
            this.m_GroupLogicList.Controls.Add(this.m_ListBoxAvailableLogicControllers);
            this.m_GroupLogicList.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupLogicList.Location = new System.Drawing.Point(10, 10);
            this.m_GroupLogicList.Name = "m_GroupLogicList";
            this.m_GroupLogicList.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupLogicList.Size = new System.Drawing.Size(456, 128);
            this.m_GroupLogicList.TabIndex = 0;
            this.m_GroupLogicList.TabStop = false;
            this.m_GroupLogicList.Text = "Available Logic Controllers";
            // 
            // m_ListBoxAvailableLogicControllers
            // 
            this.m_ListBoxAvailableLogicControllers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_ListBoxAvailableLogicControllers.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_ListBoxAvailableLogicControllers.FormattingEnabled = true;
            this.m_ListBoxAvailableLogicControllers.Location = new System.Drawing.Point(10, 23);
            this.m_ListBoxAvailableLogicControllers.Name = "m_ListBoxAvailableLogicControllers";
            this.m_ListBoxAvailableLogicControllers.Size = new System.Drawing.Size(436, 91);
            this.m_ListBoxAvailableLogicControllers.TabIndex = 0;
            // 
            // m_BtnOK
            // 
            this.m_BtnOK.Location = new System.Drawing.Point(312, 196);
            this.m_BtnOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_BtnOK.Name = "m_BtnOK";
            this.m_BtnOK.Size = new System.Drawing.Size(73, 25);
            this.m_BtnOK.TabIndex = 1;
            this.m_BtnOK.Text = "OK";
            this.m_BtnOK.UseVisualStyleBackColor = true;
            this.m_BtnOK.Click += new System.EventHandler(this.m_BtnOK_Click);
            // 
            // m_Cancel
            // 
            this.m_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_Cancel.Location = new System.Drawing.Point(393, 196);
            this.m_Cancel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_Cancel.Name = "m_Cancel";
            this.m_Cancel.Size = new System.Drawing.Size(73, 25);
            this.m_Cancel.TabIndex = 0;
            this.m_Cancel.TabStop = false;
            this.m_Cancel.Text = "Cancel";
            this.m_Cancel.UseVisualStyleBackColor = true;
            this.m_Cancel.Click += new System.EventHandler(this.m_Cancel_Click);
            // 
            // FormSelectTarget
            // 
            this.AcceptButton = this.m_BtnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_Cancel;
            this.ClientSize = new System.Drawing.Size(476, 232);
            this.Controls.Add(this.m_Cancel);
            this.Controls.Add(this.m_BtnOK);
            this.Controls.Add(this.m_PanelOuter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSelectTarget";
            this.Text = "Select Target Logic";
            this.Shown += new System.EventHandler(this.FormSelectTargetLogic_Shown);
            this.m_PanelOuter.ResumeLayout(false);
            this.m_PanelOuter.PerformLayout();
            this.m_GroupLogicList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_PanelOuter;
        private System.Windows.Forms.Button m_BtnOK;
        private System.Windows.Forms.GroupBox m_GroupLogicList;
        private System.Windows.Forms.Button m_Cancel;
        private System.Windows.Forms.ListBox m_ListBoxAvailableLogicControllers;
        private System.Windows.Forms.TextBox m_TxtStatusInformation;
    }
}