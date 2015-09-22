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
 *  File name:  FormWorksetManager.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/14/11    1.1     K.McD           1.  Added the GroupBox control associated with the notes to the user.
 * 
 *  05/16/11    1.2     K.McD           1.  Changed the modifiers of a number of controls to protected so that any reference to the default workset can be removed in 
 *                                          child forms.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Forms
{
    partial class FormWorksetManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorksetManager));
            this.m_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_ContextMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ContextMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorNew = new System.Windows.Forms.ToolStripSeparator();
            this.m_ContextMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorCopy = new System.Windows.Forms.ToolStripSeparator();
            this.m_ContextMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ContextMenuItemRename = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorRename = new System.Windows.Forms.ToolStripSeparator();
            this.m_ContextMenuItemSetAsDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ContextMenuItemOverrideSecurity = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_PanelOuter = new System.Windows.Forms.Panel();
            this.m_GroupBoxNotes = new System.Windows.Forms.GroupBox();
            this.m_TextBoxNotes = new System.Windows.Forms.TextBox();
            this.m_ListView = new System.Windows.Forms.ListView();
            this.m_ColumnHeaderWorsetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_ColumnHeaderSecurityLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_ContextMenu.SuspendLayout();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ContextMenu
            // 
            this.m_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ContextMenuItemEdit,
            this.m_ContextMenuItemNew,
            this.m_SeparatorNew,
            this.m_ContextMenuItemCopy,
            this.m_SeparatorCopy,
            this.m_ContextMenuItemDelete,
            this.m_ContextMenuItemRename,
            this.m_SeparatorRename,
            this.m_ContextMenuItemSetAsDefault,
            this.m_ContextMenuItemOverrideSecurity});
            this.m_ContextMenu.Name = "m_ContextMenu";
            resources.ApplyResources(this.m_ContextMenu, "m_ContextMenu");
            this.m_ContextMenu.Opened += new System.EventHandler(this.m_ContextMenu_Opened);
            // 
            // m_ContextMenuItemEdit
            // 
            this.m_ContextMenuItemEdit.Image = global::Common.Properties.Resources.Modify;
            this.m_ContextMenuItemEdit.Name = "m_ContextMenuItemEdit";
            resources.ApplyResources(this.m_ContextMenuItemEdit, "m_ContextMenuItemEdit");
            this.m_ContextMenuItemEdit.Click += new System.EventHandler(this.m_ContextMenuItemEdit_Click);
            // 
            // m_ContextMenuItemNew
            // 
            this.m_ContextMenuItemNew.Image = global::Common.Properties.Resources.CreateNew;
            this.m_ContextMenuItemNew.Name = "m_ContextMenuItemNew";
            resources.ApplyResources(this.m_ContextMenuItemNew, "m_ContextMenuItemNew");
            this.m_ContextMenuItemNew.Click += new System.EventHandler(this.m_ContextMenuItemNew_Click);
            // 
            // m_SeparatorNew
            // 
            this.m_SeparatorNew.Name = "m_SeparatorNew";
            resources.ApplyResources(this.m_SeparatorNew, "m_SeparatorNew");
            // 
            // m_ContextMenuItemCopy
            // 
            this.m_ContextMenuItemCopy.Image = global::Common.Properties.Resources.Copy;
            this.m_ContextMenuItemCopy.Name = "m_ContextMenuItemCopy";
            resources.ApplyResources(this.m_ContextMenuItemCopy, "m_ContextMenuItemCopy");
            this.m_ContextMenuItemCopy.Click += new System.EventHandler(this.m_ContextMenuItemCopy_Click);
            // 
            // m_SeparatorCopy
            // 
            this.m_SeparatorCopy.Name = "m_SeparatorCopy";
            resources.ApplyResources(this.m_SeparatorCopy, "m_SeparatorCopy");
            // 
            // m_ContextMenuItemDelete
            // 
            this.m_ContextMenuItemDelete.Image = global::Common.Properties.Resources.Delete;
            this.m_ContextMenuItemDelete.Name = "m_ContextMenuItemDelete";
            resources.ApplyResources(this.m_ContextMenuItemDelete, "m_ContextMenuItemDelete");
            this.m_ContextMenuItemDelete.Click += new System.EventHandler(this.m_ContextMenuItemDelete_Click);
            // 
            // m_ContextMenuItemRename
            // 
            this.m_ContextMenuItemRename.Image = global::Common.Properties.Resources.Rename;
            this.m_ContextMenuItemRename.Name = "m_ContextMenuItemRename";
            resources.ApplyResources(this.m_ContextMenuItemRename, "m_ContextMenuItemRename");
            this.m_ContextMenuItemRename.Click += new System.EventHandler(this.m_ContextMenuItemRename_Click);
            // 
            // m_SeparatorRename
            // 
            this.m_SeparatorRename.Name = "m_SeparatorRename";
            resources.ApplyResources(this.m_SeparatorRename, "m_SeparatorRename");
            // 
            // m_ContextMenuItemSetAsDefault
            // 
            this.m_ContextMenuItemSetAsDefault.Image = global::Common.Properties.Resources.Bookmark;
            this.m_ContextMenuItemSetAsDefault.Name = "m_ContextMenuItemSetAsDefault";
            resources.ApplyResources(this.m_ContextMenuItemSetAsDefault, "m_ContextMenuItemSetAsDefault");
            this.m_ContextMenuItemSetAsDefault.Click += new System.EventHandler(this.m_ContextMenuItemSetAsDefault_Click);
            // 
            // m_ContextMenuItemOverrideSecurity
            // 
            this.m_ContextMenuItemOverrideSecurity.Image = global::Common.Properties.Resources.Keys;
            this.m_ContextMenuItemOverrideSecurity.Name = "m_ContextMenuItemOverrideSecurity";
            resources.ApplyResources(this.m_ContextMenuItemOverrideSecurity, "m_ContextMenuItemOverrideSecurity");
            this.m_ContextMenuItemOverrideSecurity.Click += new System.EventHandler(this.m_ContextMenuItemOverrideSecurity_Click);
            // 
            // m_ImageList
            // 
            this.m_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ImageList.ImageStream")));
            this.m_ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.m_ImageList.Images.SetKeyName(0, "Blank.png");
            this.m_ImageList.Images.SetKeyName(1, "TickHS.png");
            this.m_ImageList.Images.SetKeyName(2, "Book_openHS.png");
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.UseVisualStyleBackColor = true;
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelOuter.Controls.Add(this.m_GroupBoxNotes);
            this.m_PanelOuter.Controls.Add(this.m_ListView);
            resources.ApplyResources(this.m_PanelOuter, "m_PanelOuter");
            this.m_PanelOuter.Name = "m_PanelOuter";
            // 
            // m_GroupBoxNotes
            // 
            this.m_GroupBoxNotes.Controls.Add(this.m_TextBoxNotes);
            resources.ApplyResources(this.m_GroupBoxNotes, "m_GroupBoxNotes");
            this.m_GroupBoxNotes.Name = "m_GroupBoxNotes";
            this.m_GroupBoxNotes.TabStop = false;
            // 
            // m_TextBoxNotes
            // 
            this.m_TextBoxNotes.BackColor = System.Drawing.SystemColors.Window;
            this.m_TextBoxNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.m_TextBoxNotes, "m_TextBoxNotes");
            this.m_TextBoxNotes.Name = "m_TextBoxNotes";
            this.m_TextBoxNotes.ReadOnly = true;
            this.m_TextBoxNotes.TabStop = false;
            // 
            // m_ListView
            // 
            this.m_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColumnHeaderWorsetName,
            this.m_ColumnHeaderSecurityLevel});
            this.m_ListView.ContextMenuStrip = this.m_ContextMenu;
            resources.ApplyResources(this.m_ListView, "m_ListView");
            this.m_ListView.FullRowSelect = true;
            this.m_ListView.GridLines = true;
            this.m_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_ListView.LabelEdit = true;
            this.m_ListView.MultiSelect = false;
            this.m_ListView.Name = "m_ListView";
            this.m_ListView.ShowGroups = false;
            this.m_ListView.SmallImageList = this.m_ImageList;
            this.m_ListView.UseCompatibleStateImageBehavior = false;
            this.m_ListView.View = System.Windows.Forms.View.Details;
            this.m_ListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.m_ListView_AfterLabelEdit);
            this.m_ListView.DoubleClick += new System.EventHandler(this.m_ListView_DoubleClick);
            // 
            // m_ColumnHeaderWorsetName
            // 
            resources.ApplyResources(this.m_ColumnHeaderWorsetName, "m_ColumnHeaderWorsetName");
            // 
            // m_ColumnHeaderSecurityLevel
            // 
            resources.ApplyResources(this.m_ColumnHeaderSecurityLevel, "m_ColumnHeaderSecurityLevel");
            // 
            // FormWorksetManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.m_ButtonOK;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.m_PanelOuter);
            this.Controls.Add(this.m_ButtonOK);
            this.Name = "FormWorksetManager";
            this.Activated += new System.EventHandler(this.FormIODisplay_Activated);
            this.m_ContextMenu.ResumeLayout(false);
            this.m_PanelOuter.ResumeLayout(false);
            this.m_GroupBoxNotes.ResumeLayout(false);
            this.m_GroupBoxNotes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Reference to the <c>ContextMenuStrip</c> control.
        /// </summary>
        protected System.Windows.Forms.ContextMenuStrip m_ContextMenu;

        /// <summary>
        /// Reference to the 'Edit' context menu.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_ContextMenuItemEdit;

        /// <summary>
        /// Reference to the 'New' context menu.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_ContextMenuItemNew;

        /// <summary>
        /// Reference to the 'Copy' context menu.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_ContextMenuItemCopy;

        /// <summary>
        /// Reference to the 'Delete' context menu.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_ContextMenuItemDelete;

        /// <summary>
        /// Reference to the 'Rename' context menu.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_ContextMenuItemRename;

        /// <summary>
        /// Reference to the 'Set As Default' context menu.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_ContextMenuItemSetAsDefault;

        /// <summary>
        /// Reference to the 'Override Security' context menu.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_ContextMenuItemOverrideSecurity;

        /// <summary>
        /// Reference to the <c>ListView</c> control used to display the available worksets.
        /// </summary>
        protected System.Windows.Forms.ListView m_ListView;

        /// <summary>
        /// Reference to the <c>ColumnHeader</c> control associated with the workset names.
        /// </summary>
        protected System.Windows.Forms.ColumnHeader m_ColumnHeaderWorsetName;

        /// <summary>
        /// Reference to the <c>ColumnHeader</c> control associated with the security level of the workset.
        /// </summary>
        protected System.Windows.Forms.ColumnHeader m_ColumnHeaderSecurityLevel;

        /// <summary>
        /// Reference to the <c>TextBox</c> control containing the notes to the user.
        /// </summary>
        protected System.Windows.Forms.TextBox m_TextBoxNotes;

        /// <summary>
        /// Reference to the <c>ImageList</c> control.
        /// </summary>
        protected System.Windows.Forms.ImageList m_ImageList;

        private System.Windows.Forms.ToolStripSeparator m_SeparatorNew;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorCopy;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorRename;
        private System.Windows.Forms.Panel m_PanelOuter;
        private System.Windows.Forms.GroupBox m_GroupBoxNotes;
        private System.Windows.Forms.Button m_ButtonOK;

    }
}