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
 *  File name:  EventBitmaskControl.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

namespace Common.UserControls
{
    partial class EventBitmaskControl
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
            this.m_ContextMenuBitmask = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_MenuItemShowDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemShowFlags = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ContextMenuBitmask.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_LabelNameField
            // 
            this.m_LabelNameField.DoubleClick += new System.EventHandler(this.m_MenuItemShowDefinition_Click);
            // 
            // m_LabelUnitsField
            // 
            this.m_LabelUnitsField.DoubleClick += new System.EventHandler(this.m_MenuItemShowFlags_DoubleClick);
            // 
            // m_ContextMenuBitmask
            // 
            this.m_ContextMenuBitmask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemShowDefinition,
            this.m_MenuItemShowFlags});
            this.m_ContextMenuBitmask.Name = "m_ContextMenuBitmask";
            this.m_ContextMenuBitmask.Size = new System.Drawing.Size(159, 48);
            // 
            // m_MenuItemShowDefinition
            // 
            this.m_MenuItemShowDefinition.Image = global::Common.Properties.Resources.Help;
            this.m_MenuItemShowDefinition.Name = "m_MenuItemShowDefinition";
            this.m_MenuItemShowDefinition.Size = new System.Drawing.Size(158, 22);
            this.m_MenuItemShowDefinition.Text = "Show &Definition";
            this.m_MenuItemShowDefinition.Click += new System.EventHandler(this.m_MenuItemShowDefinition_Click);
            // 
            // m_MenuItemShowFlags
            // 
            this.m_MenuItemShowFlags.Name = "m_MenuItemShowFlags";
            this.m_MenuItemShowFlags.Size = new System.Drawing.Size(158, 22);
            this.m_MenuItemShowFlags.Text = "Show &Flags";
            this.m_MenuItemShowFlags.DoubleClick += new System.EventHandler(this.m_MenuItemShowFlags_DoubleClick);
            // 
            // EventBitmaskControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ContextMenuStrip = this.m_ContextMenuBitmask;
            this.Name = "EventBitmaskControl";
            this.Controls.SetChildIndex(this.m_LabelNameField, 0);
            this.Controls.SetChildIndex(this.m_LabelValueField, 0);
            this.Controls.SetChildIndex(this.m_LabelUnitsField, 0);
            this.m_ContextMenuBitmask.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip m_ContextMenuBitmask;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemShowDefinition;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemShowFlags;
    }
}
