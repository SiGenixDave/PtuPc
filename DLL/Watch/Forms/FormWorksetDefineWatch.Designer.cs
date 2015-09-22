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
 *  Project:    Watch
 * 
 *  File name:  FormWorksetDefineWatch.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/16/10    1.0     K.McDonald      1.  First entry into TortoiseSVN.
 *  
 *  03/13/15    1.1     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                          
 *                                              1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                                  options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                                  
 *                                      Modifications
 *                                      1.  Changed the title text in the InitializeComponent() method to 'Create / Modify - Watch Window Workset'. - Ref.: 1.1.1.
 *
 */
#endregion --- Revision History ---

namespace Watch.Forms
{
    partial class FormWorksetDefineWatch
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
            // m_ListBox1
            // 
            this.m_ListBox1.Location = new System.Drawing.Point(7, 61);
            this.m_ListBox1.Size = new System.Drawing.Size(274, 264);
            // 
            // m_ListBox2
            // 
            this.m_ListBox2.Location = new System.Drawing.Point(7, 61);
            this.m_ListBox2.Size = new System.Drawing.Size(274, 264);
            // 
            // m_ListBox3
            // 
            this.m_ListBox3.Location = new System.Drawing.Point(7, 61);
            this.m_ListBox3.Size = new System.Drawing.Size(274, 264);
            // 
            // m_LabelCount1
            // 
            this.m_LabelCount1.Location = new System.Drawing.Point(7, 328);
            // 
            // m_LabelCount2
            // 
            this.m_LabelCount2.Location = new System.Drawing.Point(7, 328);
            // 
            // m_LabelCount3
            // 
            this.m_LabelCount3.Location = new System.Drawing.Point(7, 328);
            // 
            // m_ListBox1RowHeader
            // 
            this.m_ListBox1RowHeader.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.m_ListBox1RowHeader.Visible = false;
            // 
            // m_ListBox3RowHeader
            // 
            this.m_ListBox3RowHeader.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.m_ListBox3RowHeader.Visible = false;
            // 
            // m_ListBox2RowHeader
            // 
            this.m_ListBox2RowHeader.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            // 
            // m_LabelListBox1ColumnHeader
            // 
            this.m_LabelListBox1ColumnHeader.Location = new System.Drawing.Point(7, 44);
            // 
            // m_LabelListBox2ColumnHeader
            // 
            this.m_LabelListBox2ColumnHeader.Location = new System.Drawing.Point(7, 44);
            // 
            // m_LabelListBox3ColumnHeader
            // 
            this.m_LabelListBox3ColumnHeader.Location = new System.Drawing.Point(7, 44);
            // 
            // FormWorksetDefineWatch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(703, 547);
            this.Name = "FormWorksetDefineWatch";
            this.Text = "Create / Modify - Watch Window Workset";
            this.Controls.SetChildIndex(this.m_PanelStatusMessage, 0);
            this.Controls.SetChildIndex(this.m_ButtonCancel, 0);
            this.Controls.SetChildIndex(this.m_ButtonApply, 0);
            this.Controls.SetChildIndex(this.m_PanelOuter, 0);
            this.Controls.SetChildIndex(this.m_ButtonOK, 0);
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
    }
}