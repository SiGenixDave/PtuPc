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
 *  File name:  FormShowSystemInformation.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/25/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  09/30/10    1.1     K.McD           1.  Changed the Alt key access on the button to configure the VCU time to T.
 *                                      2.  Removed the button to configure the car identifier.
 * 
 *  02/28/11    1.2     K.McD           1.  Removed the button used to call the form that allows the user to set the VCU date and time.
 *
 *  07/28/15    1.3     K.McD           References
 *                                      1.  Informal NSEL review of naming conventions.
 *                                      
 *                                      Modifications
 *                                      1.  Changed the 'Car Identifier' Label and Legend to 'Car Number' for consistency.
 */
#endregion --- Revision History ---

namespace Bombardier.PTU.Forms
{
    partial class FormShowSystemInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowSystemInformation));
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_GroupBoxVCUDateTime = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanelDateTime = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelTime = new System.Windows.Forms.Label();
            this.m_LabelDate = new System.Windows.Forms.Label();
            this.m_LegendTime = new System.Windows.Forms.Label();
            this.m_LegendDate = new System.Windows.Forms.Label();
            this.m_GroupBoxParameters = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelCarNumber = new System.Windows.Forms.Label();
            this.m_LabelSoftwareVersion = new System.Windows.Forms.Label();
            this.m_LegendLogicType = new System.Windows.Forms.Label();
            this.m_LegendCarNumber = new System.Windows.Forms.Label();
            this.m_LegendSoftwareVersion = new System.Windows.Forms.Label();
            this.m_LabelLogicType = new System.Windows.Forms.Label();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxVCUDateTime.SuspendLayout();
            this.m_TableLayoutPanelDateTime.SuspendLayout();
            this.m_GroupBoxParameters.SuspendLayout();
            this.m_TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonOK.Location = new System.Drawing.Point(343, 243);
            this.m_ButtonOK.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonOK.TabIndex = 1;
            this.m_ButtonOK.Text = "OK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxVCUDateTime);
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxParameters);
            this.m_PanelOuter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.m_PanelOuter.Name = "m_PanelOuter";
            this.m_PanelOuter.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_PanelOuter.Size = new System.Drawing.Size(428, 232);
            this.m_PanelOuter.TabIndex = 2;
            // 
            // m_GroupBoxVCUDateTime
            // 
            this.m_GroupBoxVCUDateTime.Controls.Add(this.m_TableLayoutPanelDateTime);
            this.m_GroupBoxVCUDateTime.Location = new System.Drawing.Point(10, 131);
            this.m_GroupBoxVCUDateTime.Name = "m_GroupBoxVCUDateTime";
            this.m_GroupBoxVCUDateTime.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxVCUDateTime.Size = new System.Drawing.Size(406, 89);
            this.m_GroupBoxVCUDateTime.TabIndex = 0;
            this.m_GroupBoxVCUDateTime.TabStop = false;
            this.m_GroupBoxVCUDateTime.Text = "Vehicle Control Unit Date/Time";
            // 
            // m_TableLayoutPanelDateTime
            // 
            this.m_TableLayoutPanelDateTime.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanelDateTime.ColumnCount = 2;
            this.m_TableLayoutPanelDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.m_TableLayoutPanelDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LabelTime, 1, 1);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LabelDate, 1, 0);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LegendTime, 0, 1);
            this.m_TableLayoutPanelDateTime.Controls.Add(this.m_LegendDate, 0, 0);
            this.m_TableLayoutPanelDateTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelDateTime.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanelDateTime.Margin = new System.Windows.Forms.Padding(5);
            this.m_TableLayoutPanelDateTime.Name = "m_TableLayoutPanelDateTime";
            this.m_TableLayoutPanelDateTime.RowCount = 2;
            this.m_TableLayoutPanelDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanelDateTime.Size = new System.Drawing.Size(386, 54);
            this.m_TableLayoutPanelDateTime.TabIndex = 0;
            // 
            // m_LabelTime
            // 
            this.m_LabelTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelTime.AutoSize = true;
            this.m_LabelTime.Location = new System.Drawing.Point(117, 33);
            this.m_LabelTime.Name = "m_LabelTime";
            this.m_LabelTime.Size = new System.Drawing.Size(0, 13);
            this.m_LabelTime.TabIndex = 9;
            // 
            // m_LabelDate
            // 
            this.m_LabelDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelDate.AutoSize = true;
            this.m_LabelDate.Location = new System.Drawing.Point(117, 7);
            this.m_LabelDate.Name = "m_LabelDate";
            this.m_LabelDate.Size = new System.Drawing.Size(0, 13);
            this.m_LabelDate.TabIndex = 8;
            // 
            // m_LegendTime
            // 
            this.m_LegendTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendTime.AutoSize = true;
            this.m_LegendTime.Location = new System.Drawing.Point(4, 33);
            this.m_LegendTime.Name = "m_LegendTime";
            this.m_LegendTime.Size = new System.Drawing.Size(33, 13);
            this.m_LegendTime.TabIndex = 0;
            this.m_LegendTime.Text = "Time:";
            // 
            // m_LegendDate
            // 
            this.m_LegendDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendDate.AutoSize = true;
            this.m_LegendDate.Location = new System.Drawing.Point(4, 7);
            this.m_LegendDate.Name = "m_LegendDate";
            this.m_LegendDate.Size = new System.Drawing.Size(33, 13);
            this.m_LegendDate.TabIndex = 2;
            this.m_LegendDate.Text = "Date:";
            // 
            // m_GroupBoxParameters
            // 
            this.m_GroupBoxParameters.Controls.Add(this.m_TableLayoutPanel);
            this.m_GroupBoxParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_GroupBoxParameters.Location = new System.Drawing.Point(10, 10);
            this.m_GroupBoxParameters.Name = "m_GroupBoxParameters";
            this.m_GroupBoxParameters.Padding = new System.Windows.Forms.Padding(10, 10, 10, 12);
            this.m_GroupBoxParameters.Size = new System.Drawing.Size(408, 115);
            this.m_GroupBoxParameters.TabIndex = 0;
            this.m_GroupBoxParameters.TabStop = false;
            // 
            // m_TableLayoutPanel
            // 
            this.m_TableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.m_TableLayoutPanel.ColumnCount = 2;
            this.m_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.m_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanel.Controls.Add(this.m_LabelCarNumber, 1, 2);
            this.m_TableLayoutPanel.Controls.Add(this.m_LabelSoftwareVersion, 1, 1);
            this.m_TableLayoutPanel.Controls.Add(this.m_LegendLogicType, 0, 0);
            this.m_TableLayoutPanel.Controls.Add(this.m_LegendCarNumber, 0, 2);
            this.m_TableLayoutPanel.Controls.Add(this.m_LegendSoftwareVersion, 0, 1);
            this.m_TableLayoutPanel.Controls.Add(this.m_LabelLogicType, 1, 0);
            this.m_TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanel.Location = new System.Drawing.Point(10, 23);
            this.m_TableLayoutPanel.Margin = new System.Windows.Forms.Padding(5);
            this.m_TableLayoutPanel.Name = "m_TableLayoutPanel";
            this.m_TableLayoutPanel.RowCount = 3;
            this.m_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.m_TableLayoutPanel.Size = new System.Drawing.Size(388, 80);
            this.m_TableLayoutPanel.TabIndex = 0;
            // 
            // m_LabelCarNumber
            // 
            this.m_LabelCarNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelCarNumber.AutoSize = true;
            this.m_LabelCarNumber.Location = new System.Drawing.Point(117, 59);
            this.m_LabelCarNumber.Name = "m_LabelCarNumber";
            this.m_LabelCarNumber.Size = new System.Drawing.Size(0, 13);
            this.m_LabelCarNumber.TabIndex = 7;
            // 
            // m_LabelSoftwareVersion
            // 
            this.m_LabelSoftwareVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelSoftwareVersion.AutoSize = true;
            this.m_LabelSoftwareVersion.Location = new System.Drawing.Point(117, 33);
            this.m_LabelSoftwareVersion.Name = "m_LabelSoftwareVersion";
            this.m_LabelSoftwareVersion.Size = new System.Drawing.Size(0, 13);
            this.m_LabelSoftwareVersion.TabIndex = 6;
            // 
            // m_LegendLogicType
            // 
            this.m_LegendLogicType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendLogicType.AutoSize = true;
            this.m_LegendLogicType.Location = new System.Drawing.Point(4, 7);
            this.m_LegendLogicType.Name = "m_LegendLogicType";
            this.m_LegendLogicType.Size = new System.Drawing.Size(63, 13);
            this.m_LegendLogicType.TabIndex = 0;
            this.m_LegendLogicType.Text = "Logic Type:";
            // 
            // m_LegendCarNumber
            // 
            this.m_LegendCarNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendCarNumber.AutoSize = true;
            this.m_LegendCarNumber.Location = new System.Drawing.Point(4, 59);
            this.m_LegendCarNumber.Name = "m_LegendCarNumber";
            this.m_LegendCarNumber.Size = new System.Drawing.Size(66, 13);
            this.m_LegendCarNumber.TabIndex = 4;
            this.m_LegendCarNumber.Text = "Car Number:";
            // 
            // m_LegendSoftwareVersion
            // 
            this.m_LegendSoftwareVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LegendSoftwareVersion.AutoSize = true;
            this.m_LegendSoftwareVersion.Location = new System.Drawing.Point(4, 33);
            this.m_LegendSoftwareVersion.Name = "m_LegendSoftwareVersion";
            this.m_LegendSoftwareVersion.Size = new System.Drawing.Size(90, 13);
            this.m_LegendSoftwareVersion.TabIndex = 2;
            this.m_LegendSoftwareVersion.Text = "Software Version:";
            // 
            // m_LabelLogicType
            // 
            this.m_LabelLogicType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_LabelLogicType.AutoSize = true;
            this.m_LabelLogicType.Location = new System.Drawing.Point(117, 7);
            this.m_LabelLogicType.Name = "m_LabelLogicType";
            this.m_LabelLogicType.Size = new System.Drawing.Size(0, 13);
            this.m_LabelLogicType.TabIndex = 5;
            // 
            // FormShowSystemInformation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonOK;
            this.ClientSize = new System.Drawing.Size(428, 279);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_PanelOuter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShowSystemInformation";
            this.Text = "System Information";
            this.Shown += new System.EventHandler(this.FormShowSystemInformation_Shown);
            this.m_PanelOuter.ResumeLayout(false);
            this.m_GroupBoxVCUDateTime.ResumeLayout(false);
            this.m_TableLayoutPanelDateTime.ResumeLayout(false);
            this.m_TableLayoutPanelDateTime.PerformLayout();
            this.m_GroupBoxParameters.ResumeLayout(false);
            this.m_TableLayoutPanel.ResumeLayout(false);
            this.m_TableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_PanelOuter;
        private System.Windows.Forms.Button m_ButtonOK;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanel;
        private System.Windows.Forms.Label m_LegendCarNumber;
        private System.Windows.Forms.Label m_LegendSoftwareVersion;
        private System.Windows.Forms.Label m_LegendLogicType;
        private System.Windows.Forms.GroupBox m_GroupBoxVCUDateTime;
        private System.Windows.Forms.GroupBox m_GroupBoxParameters;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelDateTime;
        private System.Windows.Forms.Label m_LegendTime;
        private System.Windows.Forms.Label m_LegendDate;
        private System.Windows.Forms.Label m_LabelCarNumber;
        private System.Windows.Forms.Label m_LabelSoftwareVersion;
        private System.Windows.Forms.Label m_LabelLogicType;
        private System.Windows.Forms.Label m_LabelTime;
        private System.Windows.Forms.Label m_LabelDate;
    }
}