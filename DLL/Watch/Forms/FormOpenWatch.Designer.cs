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
 *  File name:  FormOpenWatch.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/16/10    1.0     K.McDonald      1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

namespace Watch.Forms
{
    partial class FormOpenWatch
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
            this.m_PanelSupplementalInformation.SuspendLayout();
            this.m_TabControl.SuspendLayout();
            this.m_PanelInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_TabControl
            // 
            this.m_TabControl.Location = new System.Drawing.Point(0, 86);
            this.m_TabControl.Size = new System.Drawing.Size(1200, 614);
            // 
            // m_PanelInformation
            // 
            this.m_PanelInformation.Location = new System.Drawing.Point(0, 53);
            // 
            // FormOpenWatch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormOpenWatch";
            this.Text = "Open/Watch File";
            this.m_PanelSupplementalInformation.ResumeLayout(false);
            this.m_TabControl.ResumeLayout(false);
            this.m_PanelInformation.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}