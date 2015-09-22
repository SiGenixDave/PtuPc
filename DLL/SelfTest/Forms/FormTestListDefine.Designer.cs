#region --- Revision History ---
/*
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
 *  File name:  FormTestListDefine.Designer.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author      Comments
 *  06/03/11    1.0     K.McD       1.  First entry into TortoiseSVN.
 *  
 *  07/26/11    1.1     K.McD       1.  Added the m_PanelHelpWindowTestList and m_PanelHelpWindowAvailable panels to help position the Windows help
 *                                      window.
 *  
 *  10/26/11    1.2     K.McD       1.  Enabled the TabStop property of the OK button.
 *  
 *  03/23/15    1.3     K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *  
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                          Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *
 *                                           1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                               options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                               
 *                                  Modifications
 *                                  1.  Changed the form title to 'Modify Test List.
 *
 *  
 * 
 *  
 */
#endregion --- Revision History ---

namespace SelfTest.Forms
{
    partial class FormTestListDefine
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
            this.m_LegendSelectedTests = new System.Windows.Forms.Label();
            this.m_ComboBoxTestList = new System.Windows.Forms.ComboBox();
            this.m_PanelHelpWindowTestList = new System.Windows.Forms.Panel();
            this.m_PanelHelpWindowAvailable = new System.Windows.Forms.Panel();
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
            // m_PanelOuter
            // 
            this.m_PanelOuter.Controls.Add(this.m_ComboBoxTestList);
            this.m_PanelOuter.Controls.Add(this.m_PanelHelpWindowTestList);
            this.m_PanelOuter.Controls.Add(this.m_PanelHelpWindowAvailable);
            this.m_PanelOuter.Size = new System.Drawing.Size(748, 476);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_PanelHelpWindowAvailable, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_PanelHelpWindowTestList, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_ComboBoxTestList, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_LegendName, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_GroupBoxAvailable, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_GroupBoxWorkset, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_LegendSecurity, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_TextBoxSecurityLevel, 0);
            this.m_PanelOuter.Controls.SetChildIndex(this.m_TextBoxName, 0);
            // 
            // m_LegendName
            // 
            this.m_LegendName.Size = new System.Drawing.Size(53, 13);
            this.m_LegendName.Text = "&Test List: ";
            // 
            // m_TextBoxName
            // 
            this.m_TextBoxName.Size = new System.Drawing.Size(244, 20);
            this.m_TextBoxName.Visible = false;
            // 
            // m_LegendSecurity
            // 
            this.m_LegendSecurity.Visible = false;
            // 
            // m_TextBoxSecurityLevel
            // 
            this.m_TextBoxSecurityLevel.Visible = false;
            // 
            // m_GroupBoxWorkset
            // 
            this.m_GroupBoxWorkset.Size = new System.Drawing.Size(383, 402);
            this.m_GroupBoxWorkset.Text = "Selected Self Tests";
            // 
            // m_TabControlColumn
            // 
            this.m_TabControlColumn.Size = new System.Drawing.Size(308, 341);
            this.m_TabControlColumn.TabStop = false;
            // 
            // m_TabPageColumn1
            // 
            this.m_TabPageColumn1.Controls.Add(this.m_LegendSelectedTests);
            this.m_TabPageColumn1.Size = new System.Drawing.Size(300, 315);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelCount1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LegendHeader1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1RowHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelListBox1ColumnHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_TextBoxHeader1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LegendSelectedTests, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1, 0);
            // 
            // m_TabPageColumn2
            // 
            this.m_TabPageColumn2.Size = new System.Drawing.Size(300, 315);
            // 
            // m_TabPageColumn3
            // 
            this.m_TabPageColumn3.Size = new System.Drawing.Size(300, 315);
            // 
            // m_ListBox1
            // 
            this.m_ListBox1.Location = new System.Drawing.Point(8, 24);
            this.m_ListBox1.Size = new System.Drawing.Size(282, 264);
            this.m_ListBox1.TabIndex = 1;
            this.m_ListBox1.SelectedIndexChanged += new System.EventHandler(this.m_ListBox1_SelectedIndexChanged);
            // 
            // m_LabelCount1
            // 
            this.m_LabelCount1.Location = new System.Drawing.Point(5, 291);
            // 
            // m_LabelCountTotal
            // 
            this.m_LabelCountTotal.Location = new System.Drawing.Point(9, 372);
            this.m_LabelCountTotal.Visible = false;
            // 
            // m_LegendHeader1
            // 
            this.m_LegendHeader1.Visible = false;
            // 
            // m_TextBoxHeader1
            // 
            this.m_TextBoxHeader1.Visible = false;
            // 
            // m_GroupBoxAvailable
            // 
            this.m_GroupBoxAvailable.Location = new System.Drawing.Point(401, 54);
            this.m_GroupBoxAvailable.Size = new System.Drawing.Size(332, 402);
            this.m_GroupBoxAvailable.Text = "Test Categories";
            // 
            // m_TabControlAvailable
            // 
            this.m_TabControlAvailable.Size = new System.Drawing.Size(308, 341);
            this.m_TabControlAvailable.TabStop = true;
            this.m_TabControlAvailable.SelectedIndexChanged += new System.EventHandler(this.m_TabControlAvailable_SelectedIndexChanged);
            // 
            // m_TabPageAll
            // 
            this.m_TabPageAll.Size = new System.Drawing.Size(300, 315);
            // 
            // m_ListBoxAvailable
            // 
            this.m_ListBoxAvailable.Location = new System.Drawing.Point(8, 24);
            this.m_ListBoxAvailable.Size = new System.Drawing.Size(282, 264);
            // 
            // m_LabelAvailableCount
            // 
            this.m_LabelAvailableCount.Location = new System.Drawing.Point(5, 291);
            // 
            // m_ButtonRemove
            // 
            this.m_ButtonRemove.Location = new System.Drawing.Point(240, 366);
            // 
            // m_ButtonMoveUp
            // 
            this.m_ButtonMoveUp.Location = new System.Drawing.Point(325, 141);
            // 
            // m_ButtonMoveDown
            // 
            this.m_ButtonMoveDown.Location = new System.Drawing.Point(325, 187);
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Location = new System.Drawing.Point(502, 487);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.Location = new System.Drawing.Point(581, 487);
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(660, 487);
            this.m_ButtonApply.Visible = false;
            // 
            // m_ButtonAdd
            // 
            this.m_ButtonAdd.Location = new System.Drawing.Point(12, 366);
            // 
            // m_ListBox1RowHeader
            // 
            this.m_ListBox1RowHeader.Size = new System.Drawing.Size(30, 208);
            this.m_ListBox1RowHeader.Visible = false;
            // 
            // m_LabelListBox1ColumnHeader
            // 
            this.m_LabelListBox1ColumnHeader.Visible = false;
            // 
            // m_LabelListBoxAvailableColumnHeader
            // 
            this.m_LabelListBoxAvailableColumnHeader.Location = new System.Drawing.Point(5, 8);
            // 
            // m_LegendSelectedTests
            // 
            this.m_LegendSelectedTests.AutoSize = true;
            this.m_LegendSelectedTests.Location = new System.Drawing.Point(5, 8);
            this.m_LegendSelectedTests.Name = "m_LegendSelectedTests";
            this.m_LegendSelectedTests.Size = new System.Drawing.Size(0, 13);
            this.m_LegendSelectedTests.TabIndex = 0;
            // 
            // m_ComboBoxTestList
            // 
            this.m_ComboBoxTestList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxTestList.FormattingEnabled = true;
            this.m_ComboBoxTestList.Location = new System.Drawing.Point(85, 16);
            this.m_ComboBoxTestList.Name = "m_ComboBoxTestList";
            this.m_ComboBoxTestList.Size = new System.Drawing.Size(245, 21);
            this.m_ComboBoxTestList.TabIndex = 1;
            this.m_ComboBoxTestList.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxTestList_SelectedIndexChanged);
            // 
            // m_PanelHelpWindowTestList
            // 
            this.m_PanelHelpWindowTestList.Location = new System.Drawing.Point(394, 0);
            this.m_PanelHelpWindowTestList.Name = "m_PanelHelpWindowTestList";
            this.m_PanelHelpWindowTestList.Size = new System.Drawing.Size(352, 471);
            this.m_PanelHelpWindowTestList.TabIndex = 4;
            // 
            // m_PanelHelpWindowAvailable
            // 
            this.m_PanelHelpWindowAvailable.Location = new System.Drawing.Point(-8, 0);
            this.m_PanelHelpWindowAvailable.Name = "m_PanelHelpWindowAvailable";
            this.m_PanelHelpWindowAvailable.Size = new System.Drawing.Size(402, 471);
            this.m_PanelHelpWindowAvailable.TabIndex = 5;
            // 
            // FormTestListDefine
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(748, 523);
            this.Name = "FormTestListDefine";
            this.Text = "Modify Test List";
            this.Controls.SetChildIndex(this.m_ButtonOK, 0);
            this.Controls.SetChildIndex(this.m_ButtonCancel, 0);
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

        private System.Windows.Forms.Label m_LegendSelectedTests;
        private System.Windows.Forms.ComboBox m_ComboBoxTestList;
        private System.Windows.Forms.Panel m_PanelHelpWindowTestList;
        private System.Windows.Forms.Panel m_PanelHelpWindowAvailable;
    }
}