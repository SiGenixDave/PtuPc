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
 *  File name:  FormChangeEnumerator.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/21/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

using Common.Communication;
using Common.Configuration;
using Common.Properties;

namespace Common.UserControls
{
    partial class FormChangeEnumerator
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
            this.m_ComboBoxNewValue = new System.Windows.Forms.ComboBox();
            this.m_PanelOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.Controls.Add(this.m_ComboBoxNewValue);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_ComboBoxNewValue, 0);
            // 
            // m_ComboBoxNewValue
            // 
            this.m_ComboBoxNewValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxNewValue.Location = new System.Drawing.Point(24, 186);
            this.m_ComboBoxNewValue.Name = "m_ComboBoxNewValue";
            this.m_ComboBoxNewValue.Size = new System.Drawing.Size(215, 21);
            this.m_ComboBoxNewValue.Sorted = true;
            this.m_ComboBoxNewValue.TabIndex = 1;
            this.m_ComboBoxNewValue.SelectedValueChanged += new System.EventHandler(this.m_ComboBox_SelectedValueChanged);
            // 
            // FormChangeEnumerator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(332, 280);
            this.Name = "FormChangeEnumerator";
            this.m_PanelOuter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox m_ComboBoxNewValue;
    }
}