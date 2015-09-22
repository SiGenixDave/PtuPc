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
 *  File name:  FormGetFltHistInfo.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/06/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *
 */
#endregion --- Revision History ---

namespace Event.Forms
{
    partial class FormGetFltHistInfo
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
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ProgressBarDownload = new System.Windows.Forms.ProgressBar();
            this.m_LegendProgress = new System.Windows.Forms.Label();
            this.m_PictureRetrieve = new System.Windows.Forms.PictureBox();
            this.m_LabelDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureRetrieve)).BeginInit();
            this.SuspendLayout();
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Enabled = false;
            this.m_ButtonCancel.Location = new System.Drawing.Point(300, 9);
            this.m_ButtonCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            this.m_ButtonCancel.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonCancel.TabIndex = 0;
            this.m_ButtonCancel.TabStop = false;
            this.m_ButtonCancel.Text = "Cancel";
            this.m_ButtonCancel.UseVisualStyleBackColor = true;
            this.m_ButtonCancel.Click += new System.EventHandler(this.m_ButtonCancel_Click);
            // 
            // m_ProgressBarDownload
            // 
            this.m_ProgressBarDownload.BackColor = System.Drawing.SystemColors.Control;
            this.m_ProgressBarDownload.Location = new System.Drawing.Point(12, 94);
            this.m_ProgressBarDownload.Margin = new System.Windows.Forms.Padding(0);
            this.m_ProgressBarDownload.Maximum = 500;
            this.m_ProgressBarDownload.Name = "m_ProgressBarDownload";
            this.m_ProgressBarDownload.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_ProgressBarDownload.Size = new System.Drawing.Size(357, 5);
            this.m_ProgressBarDownload.Step = 1;
            this.m_ProgressBarDownload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_ProgressBarDownload.TabIndex = 0;
            // 
            // m_LegendProgress
            // 
            this.m_LegendProgress.AutoSize = true;
            this.m_LegendProgress.Location = new System.Drawing.Point(9, 79);
            this.m_LegendProgress.Name = "m_LegendProgress";
            this.m_LegendProgress.Size = new System.Drawing.Size(48, 13);
            this.m_LegendProgress.TabIndex = 4;
            this.m_LegendProgress.Text = "Progress";
            // 
            // m_PictureRetrieve
            // 
            this.m_PictureRetrieve.ErrorImage = null;
            this.m_PictureRetrieve.Image = global::Event.Properties.Resources.FileRetrieve;
            this.m_PictureRetrieve.Location = new System.Drawing.Point(12, 9);
            this.m_PictureRetrieve.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_PictureRetrieve.Name = "m_PictureRetrieve";
            this.m_PictureRetrieve.Size = new System.Drawing.Size(226, 33);
            this.m_PictureRetrieve.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_PictureRetrieve.TabIndex = 0;
            this.m_PictureRetrieve.TabStop = false;
            // 
            // m_LabelDescription
            // 
            this.m_LabelDescription.AutoSize = true;
            this.m_LabelDescription.Location = new System.Drawing.Point(9, 57);
            this.m_LabelDescription.Name = "m_LabelDescription";
            this.m_LabelDescription.Size = new System.Drawing.Size(47, 13);
            this.m_LabelDescription.TabIndex = 0;
            this.m_LabelDescription.Text = "<Name>";
            // 
            // FormGetFltHistInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_ButtonCancel;
            this.ClientSize = new System.Drawing.Size(385, 111);
            this.ControlBox = false;
            this.Controls.Add(this.m_ButtonCancel);
            this.Controls.Add(this.m_PictureRetrieve);
            this.Controls.Add(this.m_LegendProgress);
            this.Controls.Add(this.m_ProgressBarDownload);
            this.Controls.Add(this.m_LabelDescription);
            this.Name = "FormGetFltHistInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download Event History";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FormGetStream_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureRetrieve)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// Reference to the <c>PictureBox</c> associated with the Retrieve graphic.
        /// </summary>
        protected System.Windows.Forms.PictureBox m_PictureRetrieve;

        /// <summary>
        /// Reference to the Cancel <c>Button</c>.
        /// </summary>
        protected System.Windows.Forms.Button m_ButtonCancel;

        /// <summary>
        /// Reference to the Download <c>ProgressBar</c>.
        /// </summary>
        protected System.Windows.Forms.ProgressBar m_ProgressBarDownload;

        /// <summary>
        /// Reference to the 'Progress' legend.
        /// </summary>
        protected System.Windows.Forms.Label m_LegendProgress;

        /// <summary>
        /// Reference to the <c>Label</c> associated with the name of the data stream that is being downloaded.
        /// </summary>
        protected System.Windows.Forms.Label m_LabelDescription;
    }
}