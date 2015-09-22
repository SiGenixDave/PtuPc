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
 *  File name:  FormConfigure.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/15/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/29/11    1.1     K.McD           1.  Moved and re-sized the ComboBox control used to select the workset.
 *                                      2.  No longer moves and re-sizes the TextBox control used to display the workset.
 *                                      
 *  03/17/15    1.2     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order TBD.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                                  1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context
 *                                                  menu options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                                  
 *                                      2.  Updated Resources with a number of premium 28 x 28 images purchased from Iconfinder.
 *                                      
 *                                  Modifications
 *                                  1.  Replaced the Image, Text and ToolTipText properties of m_TSBEdit to 'Modify' in the InitializeComponent()
 *                                      method.
 *                                      
 *                                  2.  Updated the images for the ToolStrip buttons to 28 x 28 iconfinder images and changed the ImageAlign and
 *                                      ImageScaling properties to match those of FormPTU i.e. TopAlign and None respectively.
 *                                      
 *                                  3.  Updated the ToolTipText propeties of the ToolsStrip buttons.
 */
#endregion --- Revision History ---

namespace Common.Forms
{
    partial class FormConfigure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfigure));
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_ToolStrip = new System.Windows.Forms.ToolStrip();
            this.m_TSBDownload = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorDownload = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBSave = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorSave = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBEdit = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorEdit = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBNew = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorNew = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBCopy = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorCopy = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBRename = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorRename = new System.Windows.Forms.ToolStripSeparator();
            this.m_ComboBoxWorkset = new System.Windows.Forms.ComboBox();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxWorkset.SuspendLayout();
            this.m_TabControlColumn.SuspendLayout();
            this.m_TabPageColumn1.SuspendLayout();
            this.m_TabPageColumn2.SuspendLayout();
            this.m_TabPageColumn3.SuspendLayout();
            this.m_GroupBoxAvailable.SuspendLayout();
            this.m_TabControlAvailable.SuspendLayout();
            this.m_TabPageAll.SuspendLayout();
            this.m_ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.Controls.Add(this.m_ComboBoxWorkset);
            this.m_PanelOuter.Location = new System.Drawing.Point(0, 53);
            this.m_PanelOuter.Size = new System.Drawing.Size(703, 508);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_TextBoxName, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_LegendName, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_GroupBoxAvailable, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_GroupBoxWorkset, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_TextBoxSecurityLevel, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_LegendSecurity, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_ComboBoxWorkset, 0);
            // 
            // m_TabControlColumn
            // 
            this.m_TabControlColumn.TabStop = false;
            // 
            // m_LegendHeader1
            // 
            this.m_LegendHeader1.Visible = false;
            // 
            // m_TextBoxHeader1
            // 
            this.m_TextBoxHeader1.Visible = false;
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Location = new System.Drawing.Point(470, 572);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.Location = new System.Drawing.Point(547, 572);
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(624, 572);
            // 
            // m_PanelStatusMessage
            // 
            this.m_PanelStatusMessage.Location = new System.Drawing.Point(10, 569);
            // 
            // m_ToolStrip
            // 
            this.m_ToolStrip.BackgroundImage = global::Common.Properties.Resources.LightMetallic;
            this.m_ToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_TSBDownload,
            this.m_TSSeparatorDownload,
            this.m_TSBSave,
            this.m_TSSeparatorSave,
            this.m_TSBEdit,
            this.m_TSSeparatorEdit,
            this.m_TSBNew,
            this.m_TSSeparatorNew,
            this.m_TSBCopy,
            this.m_TSSeparatorCopy,
            this.m_TSBRename,
            this.m_TSSeparatorRename});
            this.m_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.m_ToolStrip.Name = "m_ToolStrip";
            this.m_ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.m_ToolStrip.Size = new System.Drawing.Size(703, 53);
            this.m_ToolStrip.TabIndex = 0;
            // 
            // m_TSBDownload
            // 
            this.m_TSBDownload.AutoSize = false;
            this.m_TSBDownload.Enabled = false;
            this.m_TSBDownload.Image = global::Common.Properties.Resources.Download;
            this.m_TSBDownload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBDownload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBDownload.Name = "m_TSBDownload";
            this.m_TSBDownload.Size = new System.Drawing.Size(70, 50);
            this.m_TSBDownload.Text = "&Download";
            this.m_TSBDownload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBDownload.ToolTipText = "[Download the Workset to the Vehicle Control Unit]";
            this.m_TSBDownload.Click += new System.EventHandler(this.m_TSBDownload_Click);
            // 
            // m_TSSeparatorDownload
            // 
            this.m_TSSeparatorDownload.Name = "m_TSSeparatorDownload";
            this.m_TSSeparatorDownload.Size = new System.Drawing.Size(6, 53);
            // 
            // m_TSBSave
            // 
            this.m_TSBSave.AutoSize = false;
            this.m_TSBSave.Enabled = false;
            this.m_TSBSave.Image = global::Common.Properties.Resources.Save;
            this.m_TSBSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBSave.Name = "m_TSBSave";
            this.m_TSBSave.Size = new System.Drawing.Size(70, 50);
            this.m_TSBSave.Text = "&Save";
            this.m_TSBSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBSave.ToolTipText = "[Save the Workset]";
            this.m_TSBSave.Click += new System.EventHandler(this.m_TSBSave_Click);
            // 
            // m_TSSeparatorSave
            // 
            this.m_TSSeparatorSave.Name = "m_TSSeparatorSave";
            this.m_TSSeparatorSave.Size = new System.Drawing.Size(6, 53);
            // 
            // m_TSBEdit
            // 
            this.m_TSBEdit.AutoSize = false;
            this.m_TSBEdit.Enabled = false;
            this.m_TSBEdit.Image = global::Common.Properties.Resources.Modify;
            this.m_TSBEdit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBEdit.Name = "m_TSBEdit";
            this.m_TSBEdit.Size = new System.Drawing.Size(70, 50);
            this.m_TSBEdit.Text = "&Modify";
            this.m_TSBEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBEdit.ToolTipText = "[Modify the Workset]";
            this.m_TSBEdit.Click += new System.EventHandler(this.m_TSBEdit_Click);
            // 
            // m_TSSeparatorEdit
            // 
            this.m_TSSeparatorEdit.Name = "m_TSSeparatorEdit";
            this.m_TSSeparatorEdit.Size = new System.Drawing.Size(6, 53);
            // 
            // m_TSBNew
            // 
            this.m_TSBNew.AutoSize = false;
            this.m_TSBNew.Enabled = false;
            this.m_TSBNew.Image = global::Common.Properties.Resources.CreateNew;
            this.m_TSBNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBNew.Name = "m_TSBNew";
            this.m_TSBNew.Size = new System.Drawing.Size(70, 50);
            this.m_TSBNew.Text = "&New";
            this.m_TSBNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBNew.ToolTipText = "[Create a New Workset]";
            this.m_TSBNew.Click += new System.EventHandler(this.m_TSBNew_Click);
            // 
            // m_TSSeparatorNew
            // 
            this.m_TSSeparatorNew.Name = "m_TSSeparatorNew";
            this.m_TSSeparatorNew.Size = new System.Drawing.Size(6, 53);
            // 
            // m_TSBCopy
            // 
            this.m_TSBCopy.AutoSize = false;
            this.m_TSBCopy.Enabled = false;
            this.m_TSBCopy.Image = global::Common.Properties.Resources.Copy;
            this.m_TSBCopy.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBCopy.Name = "m_TSBCopy";
            this.m_TSBCopy.Size = new System.Drawing.Size(70, 50);
            this.m_TSBCopy.Text = "&Copy";
            this.m_TSBCopy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBCopy.ToolTipText = "[Copy the Workset]";
            this.m_TSBCopy.Click += new System.EventHandler(this.m_TSBCopy_Click);
            // 
            // m_TSSeparatorCopy
            // 
            this.m_TSSeparatorCopy.Name = "m_TSSeparatorCopy";
            this.m_TSSeparatorCopy.Size = new System.Drawing.Size(6, 53);
            // 
            // m_TSBRename
            // 
            this.m_TSBRename.AutoSize = false;
            this.m_TSBRename.Enabled = false;
            this.m_TSBRename.Image = global::Common.Properties.Resources.Rename;
            this.m_TSBRename.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBRename.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBRename.Name = "m_TSBRename";
            this.m_TSBRename.Size = new System.Drawing.Size(70, 50);
            this.m_TSBRename.Text = "&Rename";
            this.m_TSBRename.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBRename.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBRename.ToolTipText = "[Rename the Workset]";
            this.m_TSBRename.Click += new System.EventHandler(this.m_TSBRename_Click);
            // 
            // m_TSSeparatorRename
            // 
            this.m_TSSeparatorRename.Name = "m_TSSeparatorRename";
            this.m_TSSeparatorRename.Size = new System.Drawing.Size(6, 53);
            // 
            // m_ComboBoxWorkset
            // 
            this.m_ComboBoxWorkset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxWorkset.FormattingEnabled = true;
            this.m_ComboBoxWorkset.Location = new System.Drawing.Point(73, 17);
            this.m_ComboBoxWorkset.Name = "m_ComboBoxWorkset";
            this.m_ComboBoxWorkset.Size = new System.Drawing.Size(237, 21);
            this.m_ComboBoxWorkset.TabIndex = 1;
            // 
            // FormConfigure
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(703, 609);
            this.Controls.Add(this.m_ToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormConfigure";
            this.Text = "Configure";
            this.Controls.SetChildIndex(this.m_PanelStatusMessage, 0);
            this.Controls.SetChildIndex(this.m_ButtonCancel, 0);
            this.Controls.SetChildIndex(this.m_ButtonApply, 0);
            this.Controls.SetChildIndex(this.m_ButtonOK, 0);
            this.Controls.SetChildIndex(this.m_ToolStrip, 0);
            this.Controls.SetChildIndex(this.m_PanelOuter, 0);
            this.m_PanelOuter.ResumeLayout(false);
            this.m_PanelOuter.PerformLayout();
            this.m_GroupBoxWorkset.ResumeLayout(false);
            this.m_GroupBoxWorkset.PerformLayout();
            this.m_TabControlColumn.ResumeLayout(false);
            this.m_TabPageColumn1.ResumeLayout(false);
            this.m_TabPageColumn1.PerformLayout();
            this.m_TabPageColumn2.ResumeLayout(false);
            this.m_TabPageColumn2.PerformLayout();
            this.m_TabPageColumn3.ResumeLayout(false);
            this.m_TabPageColumn3.PerformLayout();
            this.m_GroupBoxAvailable.ResumeLayout(false);
            this.m_TabControlAvailable.ResumeLayout(false);
            this.m_TabPageAll.ResumeLayout(false);
            this.m_TabPageAll.PerformLayout();
            this.m_ToolStrip.ResumeLayout(false);
            this.m_ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// Reference to the <c>ToolTip</c> control.
        /// </summary>
        protected System.Windows.Forms.ToolTip m_ToolTip;

        /// <summary>
        /// Reference to the <c>ToolStrip</c> control.
        /// </summary>
        protected System.Windows.Forms.ToolStrip m_ToolStrip;

        /// <summary>
        /// Reference to the 'Save' <c>ToolStripButton</c>.
        /// </summary>
        /// <remarks>Allow the FormChangeChartScale class to modify the Enabled property of this <c>ToolStripButton</c> control.</remarks>
        public System.Windows.Forms.ToolStripButton m_TSBSave;

        /// <summary>
        /// Reference to the 'Edit' <c>ToolStripButton</c>.
        /// </summary>
        protected System.Windows.Forms.ToolStripButton m_TSBEdit;

        /// <summary>
        /// Reference to the 'New' <c>ToolStripButton</c>.
        /// </summary>
        protected System.Windows.Forms.ToolStripButton m_TSBNew;

        /// <summary>
        /// Reference to the 'Copy' <c>ToolStripButton</c>.
        /// </summary>
        protected System.Windows.Forms.ToolStripButton m_TSBCopy;

        /// <summary>
        /// Reference to the 'Rename' <c>ToolStripButton</c>.
        /// </summary>
        protected System.Windows.Forms.ToolStripButton m_TSBRename;

        /// <summary>
        /// Reference to the 'Download' <c>ToolStripButton</c>.
        /// </summary>
        protected System.Windows.Forms.ToolStripButton m_TSBDownload;

        /// <summary>
        /// Reference to the <c>ComboBox</c> control used to select the chart recorder workset.
        /// </summary>
        protected System.Windows.Forms.ComboBox m_ComboBoxWorkset;

        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorSave;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorEdit;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorNew;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorCopy;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorRename;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorDownload;
    }
}