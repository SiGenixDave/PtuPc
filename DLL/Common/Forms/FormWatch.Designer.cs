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
 *  File name:  FormWatch.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/10/10    1.0     K.McD           First Release.
 *  
 *  05/13/15    1.1     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘WibuBox: [Present | Not Present]’.
 *                                              
 *                                      Modifications
 *                                      1.  In order to accommodate the additional status labels, it is neccessary to move the 'Recording/Playback' progress bar from
 *                                          the status strip on the main window to the Panel control at to top of this form.
 *                                          
 *                                      2.  Added a ProgressBar control and a Label Control to the Panel control, m_PanelInformation. These will be repositioned by the
 *                                          child forms.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Forms
{
    /// <summary>
    /// Form to view the live watch variable data retrieved from the target hardware.
    /// </summary>
    partial class FormWatch
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
            this.m_LabelProgressBar = new System.Windows.Forms.Label();
            this.m_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.m_TabControl.SuspendLayout();
            this.m_PanelInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_TabControl
            // 
            this.m_TabControl.Location = new System.Drawing.Point(0, 90);
            this.m_TabControl.Size = new System.Drawing.Size(1200, 610);
            // 
            // m_PanelInformation
            // 
            this.m_PanelInformation.Controls.Add(this.m_ProgressBar);
            this.m_PanelInformation.Controls.Add(this.m_LabelProgressBar);
            this.m_PanelInformation.Location = new System.Drawing.Point(0, 57);
            this.m_PanelInformation.Controls.SetChildIndex(this.m_LabelProgressBar, 0);
            this.m_PanelInformation.Controls.SetChildIndex(this.m_ProgressBar, 0);
            // 
            // m_LabelProgressBar
            // 
            this.m_LabelProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelProgressBar.AutoSize = true;
            this.m_LabelProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelProgressBar.Location = new System.Drawing.Point(926, 8);
            this.m_LabelProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.m_LabelProgressBar.Name = "m_LabelProgressBar";
            this.m_LabelProgressBar.Size = new System.Drawing.Size(54, 13);
            this.m_LabelProgressBar.TabIndex = 0;
            this.m_LabelProgressBar.Text = "Playback:";
            this.m_LabelProgressBar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_LabelProgressBar.Visible = false;
            // 
            // m_ProgressBar
            // 
            this.m_ProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_ProgressBar.Location = new System.Drawing.Point(986, 12);
            this.m_ProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.m_ProgressBar.Name = "m_ProgressBar";
            this.m_ProgressBar.Size = new System.Drawing.Size(70, 6);
            this.m_ProgressBar.Step = 1;
            this.m_ProgressBar.TabIndex = 0;
            this.m_ProgressBar.Visible = false;
            // 
            // FormWatch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormWatch";
            this.Text = "Parent";
            this.m_TabControl.ResumeLayout(false);
            this.m_PanelInformation.ResumeLayout(false);
            this.m_PanelInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The text that is to be displayed next to the ProgressBar control.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelProgressBar;

        /// <summary>
        /// The ProgressBar control. Used to display progress of the playback or recording.
        /// </summary>
        protected System.Windows.Forms.ProgressBar m_ProgressBar;
    }
}