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
 *  Project:    Watch
 * 
 *  File name:  FormViewWatch.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McDonald      First Release.
 * 
 *  08/27/10    1.1     K.McD           1.  Removed the workset ComboBox and associated controls as these are now inherited from the FormPTU class.
 * 
 *  10/06/10    1.2     K.McD           1.  Bug fix SNCR001.18. Added registration of the KeyDown event handler.
 */
#endregion --- Revision History ---

namespace Watch.Forms
{
    partial class FormViewWatch
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
            this.m_TabControl.SuspendLayout();
            this.m_PanelInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_LabelProgressBar
            // 
            this.m_LabelProgressBar.ForeColor = System.Drawing.Color.Red;
            this.m_LabelProgressBar.Location = new System.Drawing.Point(616, 8);
            this.m_LabelProgressBar.Size = new System.Drawing.Size(59, 13);
            this.m_LabelProgressBar.Text = "Recording:";
            // 
            // m_ProgressBar
            // 
            this.m_ProgressBar.Location = new System.Drawing.Point(682, 12);
            // 
            // m_TabControl
            // 
            this.m_TabControl.Location = new System.Drawing.Point(0, 90);
            this.m_TabControl.Size = new System.Drawing.Size(1200, 610);
            // 
            // m_PanelInformation
            // 
            this.m_PanelInformation.Location = new System.Drawing.Point(0, 57);
            // 
            // m_TabPage1
            // 
            this.m_TabPage1.Click += new System.EventHandler(this.TabPage_Click);
            // 
            // FormViewWatch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormViewWatch";
            this.Text = "View/Watch Window";
            this.Shown += new System.EventHandler(this.FormViewWatch_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormViewWatch_KeyDown);
            this.m_TabControl.ResumeLayout(false);
            this.m_PanelInformation.ResumeLayout(false);
            this.m_PanelInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}