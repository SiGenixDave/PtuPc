#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Watch
 * 
 *  File name:  FormOpenSimulatedFaultLog.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/16/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  03/13/15    1.1     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order TBD.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                          
 *                                              1.  MOC-0171-06. All references to fault logs, including menu options and directory 
 *                                                  names to be replaced by 'data streams' for all projects.
 *                                      Modifications
 *                                      1.  Changed the title text in the InitializeComponent() method to 'Open/Simulated Data Stream'.
 */
#endregion --- Revision History ---

namespace Watch.Forms
{
    partial class FormOpenSimulatedFaultLog
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
            // FormOpenSimulatedFaultLog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormOpenSimulatedFaultLog";
            this.Text = "Open/Simulated Data Stream";
            this.m_PanelSupplementalInformation.ResumeLayout(false);
            this.m_TabControl.ResumeLayout(false);
            this.m_PanelInformation.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}