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
 *  Project:    Event
 * 
 *  File name:  FormOpenEventLog.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  12/03/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/28/11    1.1     K.McD           1.  Renamed this file.
 *                                      2.  Removed the GroupBox controls for aesthetic reasons.
 * 
 */
#endregion --- Revision History ---

namespace Event.Forms
{
    partial class FormOpenEventLog
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
            this.m_PanelEventVariables.SuspendLayout();
            this.m_TabControl.SuspendLayout();
            this.m_TabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelDataGridViewEventLog
            // 
            this.m_PanelDataGridViewEventLog.Size = new System.Drawing.Size(655, 560);
            // 
            // m_PanelEventVariables
            // 
            this.m_PanelEventVariables.Location = new System.Drawing.Point(667, 12);
            this.m_PanelEventVariables.Size = new System.Drawing.Size(0, 560);
            // 
            // m_PanelEventVariableList
            // 
            this.m_PanelEventVariableList.Size = new System.Drawing.Size(0, 0);
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
            // FormOpenEventLog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormOpenEventLog";
            this.Text = "Open/Event Log";
            this.Shown += new System.EventHandler(this.FormOpenEventLog_Shown);
            this.m_PanelEventVariables.ResumeLayout(false);
            this.m_PanelEventVariables.PerformLayout();
            this.m_TabControl.ResumeLayout(false);
            this.m_TabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}