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
 *  File name:  FormWorksetDefineChartRecorder.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/11/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/30/11    1.1     K.McD           1.  Set the BackColor property of the 'Chart Recorder ...' legend to Transparent.
 *                                      2.  Cleared the FormatString property of the Y axis upper and lower limit ListBox controls.
 *                                      3.  Minor adjustments the the Size and Location properties of a number of controls.
 *                                      
 *  07/07/11    1.2     K.McD           1.  Set the TabStop property of the 'Column' TabControl to false.
 *  
 *  03/13/15    1.3     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                          
 *                                              1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                                  options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                                  
 *                                              2  MOC-0171-26. Kawasaki pointed out that Chart Recorder tabs are identified as ‘COLUMNS’ and requested that this
 *                                                 be changed.
 *                                                  
 *                                      Modifications
 *                                      1.  Changed the title text in the InitializeComponent() method to 'Create / Modify - Chart Recorder Workset'.
 *                                      2.  Changed the header text associated with the 'Column 1' tab-page to "Chart Recorder".
 *                                      3.  Added ListBoxTitle Label and set it to 'Channel List'.
 *
 */
#endregion --- Revision History ---

namespace Watch.Forms
{
    partial class FormWorksetDefineChartRecorder
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
            this.m_ListBoxChartScaleUpperLimit = new System.Windows.Forms.ListBox();
            this.m_ListBoxChartScaleLowerLimit = new System.Windows.Forms.ListBox();
            this.m_ListBoxUnits = new System.Windows.Forms.ListBox();
            this.m_LabelListBoxChartScaleUpperLimitColumnHeader = new System.Windows.Forms.Label();
            this.m_LabelListBoxChartScaleLowerLimitColumnHeader = new System.Windows.Forms.Label();
            this.m_LabelListBoxUnitsColumnHeader = new System.Windows.Forms.Label();
            this.m_LegendListBoxTitle = new System.Windows.Forms.Label();
            this.m_LabelListBoxTitle = new System.Windows.Forms.Label();
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
            this.m_PanelOuter.Size = new System.Drawing.Size(871, 500);
            // 
            // m_TextBoxName
            // 
            this.m_TextBoxName.Size = new System.Drawing.Size(404, 20);
            // 
            // m_LegendSecurity
            // 
            this.m_LegendSecurity.Location = new System.Drawing.Point(565, 20);
            // 
            // m_TextBoxSecurityLevel
            // 
            this.m_TextBoxSecurityLevel.Location = new System.Drawing.Point(627, 17);
            // 
            // m_GroupBoxWorkset
            // 
            this.m_GroupBoxWorkset.Size = new System.Drawing.Size(541, 313);
            // 
            // m_TabControlColumn
            // 
            this.m_TabControlColumn.Size = new System.Drawing.Size(467, 251);
            this.m_TabControlColumn.TabStop = false;
            // 
            // m_TabPageColumn1
            // 
            this.m_TabPageColumn1.Controls.Add(this.m_LabelListBoxTitle);
            this.m_TabPageColumn1.Controls.Add(this.m_LabelListBoxUnitsColumnHeader);
            this.m_TabPageColumn1.Controls.Add(this.m_LabelListBoxChartScaleUpperLimitColumnHeader);
            this.m_TabPageColumn1.Controls.Add(this.m_LabelListBoxChartScaleLowerLimitColumnHeader);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBoxUnits);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBoxChartScaleUpperLimit);
            this.m_TabPageColumn1.Controls.Add(this.m_ListBoxChartScaleLowerLimit);
            this.m_TabPageColumn1.Controls.Add(this.m_LegendListBoxTitle);
            this.m_TabPageColumn1.Size = new System.Drawing.Size(459, 225);
            this.m_TabPageColumn1.Text = "Chart Recorder";
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1RowHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelCount1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LegendHeader1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_TextBoxHeader1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LegendListBoxTitle, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBoxChartScaleLowerLimit, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBoxChartScaleUpperLimit, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBoxUnits, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelListBox1ColumnHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelListBoxChartScaleLowerLimitColumnHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelListBoxChartScaleUpperLimitColumnHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelListBoxUnitsColumnHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelListBoxTitle, 0);
            // 
            // m_TabPageColumn2
            // 
            this.m_TabPageColumn2.Size = new System.Drawing.Size(459, 225);
            // 
            // m_TabPageColumn3
            // 
            this.m_TabPageColumn3.Size = new System.Drawing.Size(459, 225);
            // 
            // m_ListBox1
            // 
            this.m_ListBox1.Size = new System.Drawing.Size(252, 134);
            this.m_ListBox1.TabIndex = 1;
            // 
            // m_LabelCount1
            // 
            this.m_LabelCount1.Location = new System.Drawing.Point(27, 198);
            // 
            // m_LabelCountTotal
            // 
            this.m_LabelCountTotal.Location = new System.Drawing.Point(9, 274);
            this.m_LabelCountTotal.Visible = false;
            // 
            // m_LegendHeader1
            // 
            this.m_LegendHeader1.Visible = false;
            // 
            // m_TextBoxHeader1
            // 
            this.m_TextBoxHeader1.TabIndex = 0;
            this.m_TextBoxHeader1.TabStop = false;
            this.m_TextBoxHeader1.Visible = false;
            // 
            // m_GroupBoxAvailable
            // 
            this.m_GroupBoxAvailable.Location = new System.Drawing.Point(559, 54);
            // 
            // m_ButtonRemove
            // 
            this.m_ButtonRemove.Location = new System.Drawing.Point(399, 277);
            // 
            // m_ButtonMoveUp
            // 
            this.m_ButtonMoveUp.Location = new System.Drawing.Point(483, 138);
            // 
            // m_ButtonMoveDown
            // 
            this.m_ButtonMoveDown.Location = new System.Drawing.Point(483, 184);
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Location = new System.Drawing.Point(630, 511);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.Location = new System.Drawing.Point(709, 511);
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(788, 511);
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
            "8"});
            this.m_ListBox1RowHeader.Size = new System.Drawing.Size(30, 130);
            // 
            // m_LabelListBox1ColumnHeader
            // 
            this.m_LabelListBox1ColumnHeader.AutoSize = false;
            this.m_LabelListBox1ColumnHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.m_LabelListBox1ColumnHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_LabelListBox1ColumnHeader.Location = new System.Drawing.Point(30, 39);
            this.m_LabelListBox1ColumnHeader.Size = new System.Drawing.Size(252, 23);
            // 
            // m_ListBoxChartScaleUpperLimit
            // 
            this.m_ListBoxChartScaleUpperLimit.BackColor = System.Drawing.SystemColors.Window;
            this.m_ListBoxChartScaleUpperLimit.FormattingEnabled = true;
            this.m_ListBoxChartScaleUpperLimit.Location = new System.Drawing.Point(335, 61);
            this.m_ListBoxChartScaleUpperLimit.Name = "m_ListBoxChartScaleUpperLimit";
            this.m_ListBoxChartScaleUpperLimit.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBoxChartScaleUpperLimit.Size = new System.Drawing.Size(55, 134);
            this.m_ListBoxChartScaleUpperLimit.TabIndex = 0;
            this.m_ListBoxChartScaleUpperLimit.TabStop = false;
            // 
            // m_ListBoxChartScaleLowerLimit
            // 
            this.m_ListBoxChartScaleLowerLimit.BackColor = System.Drawing.SystemColors.Window;
            this.m_ListBoxChartScaleLowerLimit.FormattingEnabled = true;
            this.m_ListBoxChartScaleLowerLimit.Location = new System.Drawing.Point(281, 61);
            this.m_ListBoxChartScaleLowerLimit.Name = "m_ListBoxChartScaleLowerLimit";
            this.m_ListBoxChartScaleLowerLimit.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBoxChartScaleLowerLimit.Size = new System.Drawing.Size(55, 134);
            this.m_ListBoxChartScaleLowerLimit.TabIndex = 0;
            this.m_ListBoxChartScaleLowerLimit.TabStop = false;
            // 
            // m_ListBoxUnits
            // 
            this.m_ListBoxUnits.BackColor = System.Drawing.SystemColors.Window;
            this.m_ListBoxUnits.FormattingEnabled = true;
            this.m_ListBoxUnits.Location = new System.Drawing.Point(389, 61);
            this.m_ListBoxUnits.Name = "m_ListBoxUnits";
            this.m_ListBoxUnits.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_ListBoxUnits.Size = new System.Drawing.Size(60, 134);
            this.m_ListBoxUnits.TabIndex = 0;
            this.m_ListBoxUnits.TabStop = false;
            // 
            // m_LabelListBoxChartScaleUpperLimitColumnHeader
            // 
            this.m_LabelListBoxChartScaleUpperLimitColumnHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.m_LabelListBoxChartScaleUpperLimitColumnHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_LabelListBoxChartScaleUpperLimitColumnHeader.Location = new System.Drawing.Point(335, 39);
            this.m_LabelListBoxChartScaleUpperLimitColumnHeader.Name = "m_LabelListBoxChartScaleUpperLimitColumnHeader";
            this.m_LabelListBoxChartScaleUpperLimitColumnHeader.Size = new System.Drawing.Size(55, 23);
            this.m_LabelListBoxChartScaleUpperLimitColumnHeader.TabIndex = 0;
            this.m_LabelListBoxChartScaleUpperLimitColumnHeader.Text = "Upper";
            this.m_LabelListBoxChartScaleUpperLimitColumnHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelListBoxChartScaleLowerLimitColumnHeader
            // 
            this.m_LabelListBoxChartScaleLowerLimitColumnHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.m_LabelListBoxChartScaleLowerLimitColumnHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_LabelListBoxChartScaleLowerLimitColumnHeader.Location = new System.Drawing.Point(281, 39);
            this.m_LabelListBoxChartScaleLowerLimitColumnHeader.Name = "m_LabelListBoxChartScaleLowerLimitColumnHeader";
            this.m_LabelListBoxChartScaleLowerLimitColumnHeader.Size = new System.Drawing.Size(55, 23);
            this.m_LabelListBoxChartScaleLowerLimitColumnHeader.TabIndex = 0;
            this.m_LabelListBoxChartScaleLowerLimitColumnHeader.Text = "Lower";
            this.m_LabelListBoxChartScaleLowerLimitColumnHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelListBoxUnitsColumnHeader
            // 
            this.m_LabelListBoxUnitsColumnHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.m_LabelListBoxUnitsColumnHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_LabelListBoxUnitsColumnHeader.Location = new System.Drawing.Point(389, 39);
            this.m_LabelListBoxUnitsColumnHeader.Name = "m_LabelListBoxUnitsColumnHeader";
            this.m_LabelListBoxUnitsColumnHeader.Size = new System.Drawing.Size(60, 23);
            this.m_LabelListBoxUnitsColumnHeader.TabIndex = 0;
            this.m_LabelListBoxUnitsColumnHeader.Text = "Units";
            this.m_LabelListBoxUnitsColumnHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LegendListBoxTitle
            // 
            this.m_LegendListBoxTitle.AutoSize = true;
            this.m_LegendListBoxTitle.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendListBoxTitle.Location = new System.Drawing.Point(27, 22);
            this.m_LegendListBoxTitle.Name = "m_LegendListBoxTitle";
            this.m_LegendListBoxTitle.Size = new System.Drawing.Size(0, 13);
            this.m_LegendListBoxTitle.TabIndex = 6;
            // 
            // m_LabelListBoxTitle
            // 
            this.m_LabelListBoxTitle.AutoSize = true;
            this.m_LabelListBoxTitle.Location = new System.Drawing.Point(27, 22);
            this.m_LabelListBoxTitle.Name = "m_LabelListBoxTitle";
            this.m_LabelListBoxTitle.Size = new System.Drawing.Size(65, 13);
            this.m_LabelListBoxTitle.TabIndex = 7;
            this.m_LabelListBoxTitle.Text = "Channel List";
            // 
            // FormWorksetDefineChartRecorder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(871, 547);
            this.Name = "FormWorksetDefineChartRecorder";
            this.Text = "Create / Modify - Chart Recorder Workset";
            this.Controls.SetChildIndex(this.m_PanelStatusMessage, 0);
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

        /// <summary>
        /// Reference to the 'Units' <c>ListBox</c>.
        /// </summary>
        private System.Windows.Forms.ListBox m_ListBoxUnits;

        /// <summary>
        /// Reference to the 'Chart Scale/Lower Limit' <c>ListBox</c>.
        /// </summary>
        private System.Windows.Forms.ListBox m_ListBoxChartScaleLowerLimit;

        /// <summary>
        /// Reference to the 'Chart Scale/Upper Limit' <c>ListBox</c>.
        /// </summary>
        private System.Windows.Forms.ListBox m_ListBoxChartScaleUpperLimit;

        /// <summary>
        /// Reference to the 'Chart Scale/Upper Limit' Header <c>Label</c>.
        /// </summary>
        private System.Windows.Forms.Label m_LabelListBoxChartScaleUpperLimitColumnHeader;

        /// <summary>
        /// Reference to the 'Units' Header <c>Label</c>.
        /// </summary>
        private System.Windows.Forms.Label m_LabelListBoxUnitsColumnHeader;

        /// <summary>
        /// Reference to the 'Chart Scale/Lower Limit' Header <c>Label</c>.
        /// </summary>
        private System.Windows.Forms.Label m_LabelListBoxChartScaleLowerLimitColumnHeader;

        /// <summary>
        /// Reference to the 'Title' Legend.
        /// </summary>
        private System.Windows.Forms.Label m_LegendListBoxTitle;

        /// <summary>
        /// Reference to the <c>ListBox</c> Title <c>Label.</c>
        /// </summary>
        private System.Windows.Forms.Label m_LabelListBoxTitle;
    }
}