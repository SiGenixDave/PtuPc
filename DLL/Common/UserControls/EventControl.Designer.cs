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
 *  File name:  EventControl.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  04/21/15    1.1     K.McD           References
 *                                      1.  The height of the event variable user control must be increased to allow characters such as 'g', 'j', 'p', 'q', 'y' to be 
 *                                          displayed correctly when using the default font.
 *                                      Modifications
 *                                      1.  The Padding, Size and TextAlign properties of the Name, Value and Units Label controls were changes to: ((0),(0),(0)),
 *                                          ((200, 28), (100, 28), (100, 28)) and (MiddleLeft, MiddleRight and MiddleCenter) respectively.
 *                                      2.  The Size property of the control was modified to (400, 28).
 * 
 */
#endregion --- Revision History ---

namespace Common.UserControls
{
    partial class EventControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_MenuItemShowDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_LabelNameField
            // 
            this.m_LabelNameField.BackColor = System.Drawing.Color.WhiteSmoke;
            this.m_LabelNameField.Padding = new System.Windows.Forms.Padding(0);
            this.m_LabelNameField.Size = new System.Drawing.Size(200, 28);
            this.m_LabelNameField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_LabelNameField.DoubleClick += new System.EventHandler(this.m_MenuItemShowDefinition_Click);
            // 
            // m_LabelValueField
            // 
            this.m_LabelValueField.BackColor = System.Drawing.Color.WhiteSmoke;
            this.m_LabelValueField.ForeColor = System.Drawing.Color.ForestGreen;
            this.m_LabelValueField.Padding = new System.Windows.Forms.Padding(0);
            this.m_LabelValueField.Size = new System.Drawing.Size(100, 28);
            this.m_LabelValueField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LabelUnitsField
            // 
            this.m_LabelUnitsField.BackColor = System.Drawing.Color.WhiteSmoke;
            this.m_LabelUnitsField.Padding = new System.Windows.Forms.Padding(0);
            this.m_LabelUnitsField.Size = new System.Drawing.Size(100, 28);
            this.m_LabelUnitsField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_ContextMenu
            // 
            this.m_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemShowDefinition});
            this.m_ContextMenu.Name = "m_ContextMenu";
            this.m_ContextMenu.Size = new System.Drawing.Size(159, 26);
            // 
            // m_MenuItemShowDefinition
            // 
            this.m_MenuItemShowDefinition.Image = global::Common.Properties.Resources.Help;
            this.m_MenuItemShowDefinition.Name = "m_MenuItemShowDefinition";
            this.m_MenuItemShowDefinition.Size = new System.Drawing.Size(158, 22);
            this.m_MenuItemShowDefinition.Text = "Show &Definition";
            this.m_MenuItemShowDefinition.Click += new System.EventHandler(this.m_MenuItemShowDefinition_Click);
            // 
            // EventControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackColorValueFieldNonZero = System.Drawing.Color.WhiteSmoke;
            this.BackColorValueFieldZero = System.Drawing.Color.WhiteSmoke;
            this.ContextMenuStrip = this.m_ContextMenu;
            this.ForeColorValueFieldNonZero = System.Drawing.Color.ForestGreen;
            this.ForeColorValueFieldZero = System.Drawing.Color.ForestGreen;
            this.Name = "EventControl";
            this.Size = new System.Drawing.Size(400, 28);
            this.Controls.SetChildIndex(this.m_LabelNameField, 0);
            this.Controls.SetChildIndex(this.m_LabelValueField, 0);
            this.Controls.SetChildIndex(this.m_LabelUnitsField, 0);
            this.m_ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip m_ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemShowDefinition;
    }
}
