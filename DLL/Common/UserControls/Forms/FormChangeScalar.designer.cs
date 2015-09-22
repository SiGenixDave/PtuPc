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
 *  File name:  FormChangeScalar.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/21/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

namespace Common.UserControls
{
    partial class FormChangeScalar
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
            this.m_NumericUpDownNewValue = new System.Windows.Forms.NumericUpDown();
            this.m_PanelOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownNewValue)).BeginInit();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.Controls.Add(this.m_NumericUpDownNewValue);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_NumericUpDownNewValue, 0);
            // 
            // m_NumericUpDownNewValue
            // 
            this.m_NumericUpDownNewValue.Location = new System.Drawing.Point(24, 186);
            this.m_NumericUpDownNewValue.Name = "m_NumericUpDownNewValue";
            this.m_NumericUpDownNewValue.Size = new System.Drawing.Size(215, 20);
            this.m_NumericUpDownNewValue.TabIndex = 1;
            this.m_NumericUpDownNewValue.ThousandsSeparator = true;
            this.m_NumericUpDownNewValue.ValueChanged += new System.EventHandler(this.m_NumericUpDown_ValueChanged);
            this.m_NumericUpDownNewValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_NumericUpDownNewValue_KeyPress);
            // 
            // FormChangeScalar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(332, 280);
            this.Name = "FormChangeScalar";
            this.m_PanelOuter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownNewValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown m_NumericUpDownNewValue;

    }
}