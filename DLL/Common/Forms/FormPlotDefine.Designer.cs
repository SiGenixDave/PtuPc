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
 *  File name:  FormPlotDefine.Designer.cs
 * 
 *  Revision History
 *  ----------------
 *
 *  Date        Version Author          Comments
 *  08/31/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  03/17/15    1.1     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order TBD.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                              1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                                  options will be replaced with the ‘Modify’ legend on ALL projects.
 *
 *                                       Modifications
 *                                      1.  Changed the form title to 'Modify -  Plot Layout' in the InitializeComponent() method.
 */

/*
 *  05/13/15    1.2     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *  
 *                                              1.  MOC-0171-26. Kawasaki pointed out that Chart Recorder tabs are identified as ‘COLUMNS’ and requested that this
 *                                                  be changed.
 *                                              
 *                                                  For Chart Recorder and Data Stream worksets, the GroupBox will now be titled 'Workset Layout', as per the
 *                                                  'Watch Window' workset, and the TabPage header will show the type of workset i.e. 'Data Stream' or
 *                                                  'Chart Recorder'. For the Data Stream 'Create/Edit Workset' and 'Configure' dialog boxes, if the project does
 *                                                  not support multiple data stream types and the number of data stream channels does not exceed 16, it is proposed to
 *                                                  add a row header showing the channel numbers 1 - 16. If the project supports multiple data streams or the number
 *                                                  of parameters exceeds 16 then the row header will not be shown. The form used to define the plot layout of
 *                                                  data stream files will also display the type of workset in the TabPage header.
 *                                                  
 *                                      Modifications
 *                                      1.  Set the AutoScaleMode to Inherit rather than Font.
 *                                      2.  Attached the FormPlotDefine_Shown() event handler to the Shown event. Ref.: 1.1.1.
 * 
 */
#endregion --- Revision History ---

namespace Common.Forms
{
    partial class FormPlotDefine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlotDefine));
            this.m_ButtonRestoreDefault = new System.Windows.Forms.Button();
            this.configureBitmaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxWorkset.SuspendLayout();
            this.m_TabControlColumn.SuspendLayout();
            this.m_TabPageColumn1.SuspendLayout();
            this.m_TabPageColumn2.SuspendLayout();
            this.m_TabPageColumn3.SuspendLayout();
            this.m_GroupBoxAvailable.SuspendLayout();
            this.m_TabControlAvailable.SuspendLayout();
            this.m_TabPageAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ButtonRestoreDefault
            // 
            this.m_ButtonRestoreDefault.Location = new System.Drawing.Point(10, 511);
            this.m_ButtonRestoreDefault.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonRestoreDefault.Name = "m_ButtonRestoreDefault";
            this.m_ButtonRestoreDefault.Size = new System.Drawing.Size(116, 25);
            this.m_ButtonRestoreDefault.TabIndex = 0;
            this.m_ButtonRestoreDefault.TabStop = false;
            this.m_ButtonRestoreDefault.Text = "Restore Default ...";
            this.m_ButtonRestoreDefault.UseVisualStyleBackColor = true;
            this.m_ButtonRestoreDefault.Click += new System.EventHandler(this.m_ButtonRestoreDefaults_Click);
            // 
            // configureBitmaskToolStripMenuItem
            // 
            this.configureBitmaskToolStripMenuItem.Name = "configureBitmaskToolStripMenuItem";
            this.configureBitmaskToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.configureBitmaskToolStripMenuItem.Text = "Configure Bitmask";
            // 
            // FormPlotDefine
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(703, 547);
            this.Controls.Add(this.m_ButtonRestoreDefault);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPlotDefine";
            this.Text = "Modify -  Plot Layout";
            this.Shown += new System.EventHandler(this.FormPlotDefine_Shown);
            this.Controls.SetChildIndex(this.m_PanelStatusMessage, 0);
            this.Controls.SetChildIndex(this.m_ButtonRestoreDefault, 0);
            this.Controls.SetChildIndex(this.m_ButtonCancel, 0);
            this.Controls.SetChildIndex(this.m_ButtonOK, 0);
            this.Controls.SetChildIndex(this.m_ButtonApply, 0);
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
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Reference to the 'Restore Default' <c>Button</c>.
        /// </summary>
        private System.Windows.Forms.Button m_ButtonRestoreDefault;

        /// <summary>
        /// Reference to the 'Configure Bitmask' <c>ToolStripMenuItem</c>.
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem configureBitmaskToolStripMenuItem;
    }
}