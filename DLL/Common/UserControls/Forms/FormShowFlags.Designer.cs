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
 *  File name:  FormShowFlags.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McDonald      First Release.
 * 
 *  08/20/10    1.1     K.McD           1.  Minor variable name changes - m_Lbl... to m_Label..., m_Btn... to m_Button... etc.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.UserControls
{
    partial class FormShowFlags
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
            this.m_PanelFlagList = new System.Windows.Forms.Panel();
            this.m_PanelWatchVariable = new System.Windows.Forms.Panel();
            this.m_LabelVariableName = new System.Windows.Forms.Label();
            this.m_ButtonClose = new System.Windows.Forms.Button();
            this.m_PanelWatchVariable.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelFlagList
            // 
            this.m_PanelFlagList.AutoScroll = true;
            this.m_PanelFlagList.AutoSize = true;
            this.m_PanelFlagList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_PanelFlagList.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelFlagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PanelFlagList.Location = new System.Drawing.Point(0, 29);
            this.m_PanelFlagList.Name = "m_PanelFlagList";
            this.m_PanelFlagList.Size = new System.Drawing.Size(320, 228);
            this.m_PanelFlagList.TabIndex = 0;
            // 
            // m_PanelWatchVariable
            // 
            this.m_PanelWatchVariable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_PanelWatchVariable.BackColor = System.Drawing.Color.LightSteelBlue;
            this.m_PanelWatchVariable.Controls.Add(this.m_LabelVariableName);
            this.m_PanelWatchVariable.Controls.Add(this.m_ButtonClose);
            this.m_PanelWatchVariable.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelWatchVariable.Location = new System.Drawing.Point(0, 0);
            this.m_PanelWatchVariable.Name = "m_PanelWatchVariable";
            this.m_PanelWatchVariable.Size = new System.Drawing.Size(320, 29);
            this.m_PanelWatchVariable.TabIndex = 0;
            this.m_PanelWatchVariable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_PanelWatchVariable_MouseMove);
            this.m_PanelWatchVariable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_PanelWatchVariable_MouseDown);
            this.m_PanelWatchVariable.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_PanelWatchVariable_MouseUp);
            // 
            // m_LabelVariableName
            // 
            this.m_LabelVariableName.AutoEllipsis = true;
            this.m_LabelVariableName.AutoSize = true;
            this.m_LabelVariableName.Location = new System.Drawing.Point(46, 6);
            this.m_LabelVariableName.Name = "m_LabelVariableName";
            this.m_LabelVariableName.Size = new System.Drawing.Size(88, 13);
            this.m_LabelVariableName.TabIndex = 0;
            this.m_LabelVariableName.Text = "<Variable Name>";
            this.m_LabelVariableName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_LabelVariableName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_PanelWatchVariable_MouseMove);
            this.m_LabelVariableName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_PanelWatchVariable_MouseDown);
            this.m_LabelVariableName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_PanelWatchVariable_MouseUp);
            // 
            // m_ButtonClose
            // 
            this.m_ButtonClose.BackColor = System.Drawing.Color.Red;
            this.m_ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonClose.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_ButtonClose.Image = global::Common.Properties.Resources.Close;
            this.m_ButtonClose.Location = new System.Drawing.Point(3, 1);
            this.m_ButtonClose.Name = "m_ButtonClose";
            this.m_ButtonClose.Size = new System.Drawing.Size(37, 23);
            this.m_ButtonClose.TabIndex = 0;
            this.m_ButtonClose.UseVisualStyleBackColor = false;
            this.m_ButtonClose.Click += new System.EventHandler(this.m_ButtonClose_Click);
            // 
            // FormShowFlags
            // 
            this.AcceptButton = this.m_ButtonClose;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.m_ButtonClose;
            this.ClientSize = new System.Drawing.Size(320, 257);
            this.ControlBox = false;
            this.Controls.Add(this.m_PanelFlagList);
            this.Controls.Add(this.m_PanelWatchVariable);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "FormShowFlags";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.m_PanelWatchVariable.ResumeLayout(false);
            this.m_PanelWatchVariable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// Reference to the <c>Panel</c> containing the list of flags.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelFlagList;

        private System.Windows.Forms.Button m_ButtonClose;
        private System.Windows.Forms.Panel m_PanelWatchVariable;
        private System.Windows.Forms.Label m_LabelVariableName;
    }
}