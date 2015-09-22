#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Event
 * 
 *  File name:  FormConfigureFaultLogParametersNew.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/16/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/28/11    1.1     K.McD           1.  Modified the title text.
 *                                      2.  Removed the security level text box control as this is now inherited from the parent class.
 *                                      3.  Now hides the legend and text box associated with the workset name and overlays this with the combo box
 *                                          control which displays the current workset and allows the user to select a new workset.
 *                                          
 *  10/26/11    1.2     K.McD           1.  Moved the location of the Panel control associated with the status message.
 *  
 *  07/24/13    1.3     K.McD           1.  Set the Visible properties of the 'Total Count' and 'Count' labels to true and false respectively.
 *  
 *  03/13/15    1.4     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC,  Kawasaki Rail
 *                                              Car and NYTC on 12th April 2013 - MOC-0171:
 *                                               
 *                                              1.  MOC-0171-06. All references to fault logs, including menu options and directory names to be
 *                                                  replaced by 'data streams' for all projects.
 *                                                  
 *                                              2.  MOC-0171-26. Kawasaki pointed out that Chart Recorder tabs are identified as ‘COLUMNS’ and requested that this
 *                                                  be changed.
 *                                                   
 *                                      Modifications
 *                                      1.  Modified the title text in the InitializeComponent() method from 'Configure - Fault Log Parameters' to
 *                                          'Configure - Data Stream'.
 *                                      2.  Removed the Text property of the 'Workset Layout' GroupBox control. Now displays 'Workset Layout'.
 *                                      3.  Changed the text associated with the TabPage header to 'Data Stream'.
 *                                      4.  Added the AddRowHeader() and NoRowHeader() methods to set the TabPage control properties according to
 *                                          whether the Row Header ListBox is to be used or not.
 *                                      5.  No longer repositions/resizes the following controls in the 'Windows Form Designer generated code' region:
 *                                          m_ListBox1, m_LabelListBox1ColumnHeader, and m_LabelCount1, this is now done in the NoRowHeader() method. 
 *                                      6.  No longer populates the m_ListBox1RowHeader control, this is now done in the AddRowHeader() method.
 *                                      7.  Now sets the Visible property of the m_LabelCountTotal control to false by default.
 *                                      8.  Now no longer sets the Visible property of the m_LabelCount1 control to false by default. By default,
 *                                          this is now true.
 *
 *
 */
#endregion --- Revision History ---

using Common.Configuration;

namespace Event.Forms
{
    partial class FormConfigureFaultLogParameters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Changes the TabPage title to 'Channel No.', populates the 'Row Header' <c>ListBox</c> with the channel numbers and sets the Visible properties
        /// of the 'Total Count' <c>Label</c> and 'Row Header' <c>ListBox</c> to true. This is used when the project does not support multiple data stream
        /// types and the number of parameters supported by the workset dos not exceed the number that can be displayed on the <c>TabPage</c> without the use
        /// of scroll bars.
        /// </summary>
        private void AddRowHeader()
        {
            m_LabelCountTotal.Visible = true;

            object[] rowHeader = new object[Parameter.WatchSizeFaultLog];
            for (int row = 0; row < Parameter.WatchSizeFaultLog; row++)
            {
                rowHeader[row] = (object)((row + 1).ToString());
            }
            m_ListBox1RowHeader.Items.AddRange(rowHeader);
            this.m_ListBox1RowHeader.Visible = true;
        }

        /// <summary>
        /// Resizes and repositions the 'Data Stream Parameters' <c>TabPage</c> controls for when the 'Row Header' <c>ListBox</c> is not used i.e. 
        /// if the project supports multiple data stream types or the number of parameters supported by the workset exceeds the number that can be 
        /// displayed on the <c>TabPage</c> without the use of scroll bars.
        /// </summary>
        /// <remarks>The 'Total Count' <c>Label</c> i.e. 'Total Count: nn of 20', can't be displayed if the project supports multiple data stream types
        /// as the upper limit is the maximum number of watch variables and may not apply to the current workset, which could be confusing to the 
        /// operator.</remarks>
        private void NoRowHeader()
        {
            // Resize and reposition the ListBox control.
            this.m_ListBox1.Location = new System.Drawing.Point(8, 61);
            this.m_ListBox1.Size = new System.Drawing.Size(274, 264);

            // Reposition the 'Count' Label.
            this.m_LabelCount1.Location = new System.Drawing.Point(8, 328);

            // Reposition the 'Watch Variable Description' Label.
            this.m_LabelListBox1ColumnHeader.Location = new System.Drawing.Point(8, 44);

            // Reposition the Sample Multiple controls.
            this.m_NumericUpDownSampleMultiple.Location = new System.Drawing.Point(117, 12);
            this.m_LegendSampleMultiple.Location = new System.Drawing.Point(8, 14);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_LegendSampleMultiple = new System.Windows.Forms.Label();
            this.m_NumericUpDownSampleMultiple = new System.Windows.Forms.NumericUpDown();
            this.m_PanelOuter.SuspendLayout();
            this.m_GroupBoxWorkset.SuspendLayout();
            this.m_TabControlColumn.SuspendLayout();
            this.m_TabPageColumn1.SuspendLayout();
            this.m_TabPageColumn2.SuspendLayout();
            this.m_TabPageColumn3.SuspendLayout();
            this.m_GroupBoxAvailable.SuspendLayout();
            this.m_TabControlAvailable.SuspendLayout();
            this.m_TabPageAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSampleMultiple)).BeginInit();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.Size = new System.Drawing.Size(703, 500);
            // 
            // m_LegendName
            // 
            this.m_LegendName.Size = new System.Drawing.Size(53, 13);
            this.m_LegendName.Text = "Workset: ";
            // 
            // m_TextBoxName
            // 
            this.m_TextBoxName.Location = new System.Drawing.Point(78, 17);
            this.m_TextBoxName.Size = new System.Drawing.Size(179, 20);
            this.m_TextBoxName.Visible = false;
            // 
            // m_TabPageColumn1
            // 
            this.m_TabPageColumn1.Controls.Add(this.m_NumericUpDownSampleMultiple);
            this.m_TabPageColumn1.Controls.Add(this.m_LegendSampleMultiple);
            this.m_TabPageColumn1.Text = "Data Stream";
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelCount1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LegendHeader1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1RowHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_ListBox1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LabelListBox1ColumnHeader, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_TextBoxHeader1, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_LegendSampleMultiple, 0);
            this.m_TabPageColumn1.Controls.SetChildIndex(this.m_NumericUpDownSampleMultiple, 0);
            // 
            // m_LabelCountTotal
            // 
            this.m_LabelCountTotal.Visible = false;
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Location = new System.Drawing.Point(462, 564);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.Location = new System.Drawing.Point(541, 564);
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(620, 564);
            // 
            // m_ListBox1RowHeader
            // 
            this.m_ListBox1RowHeader.Visible = false;
            // 
            // m_PanelStatusMessage
            // 
            this.m_PanelStatusMessage.Location = new System.Drawing.Point(10, 561);
            // 
            // m_LegendSampleMultiple
            // 
            this.m_LegendSampleMultiple.AutoSize = true;
            this.m_LegendSampleMultiple.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendSampleMultiple.Location = new System.Drawing.Point(27, 14);
            this.m_LegendSampleMultiple.Name = "m_LegendSampleMultiple";
            this.m_LegendSampleMultiple.Size = new System.Drawing.Size(84, 13);
            this.m_LegendSampleMultiple.TabIndex = 3;
            this.m_LegendSampleMultiple.Text = "Sample Multiple:";
            // 
            // m_NumericUpDownSampleMultiple
            // 
            this.m_NumericUpDownSampleMultiple.Location = new System.Drawing.Point(136, 12);
            this.m_NumericUpDownSampleMultiple.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.m_NumericUpDownSampleMultiple.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_NumericUpDownSampleMultiple.Name = "m_NumericUpDownSampleMultiple";
            this.m_NumericUpDownSampleMultiple.Size = new System.Drawing.Size(47, 20);
            this.m_NumericUpDownSampleMultiple.TabIndex = 4;
            this.m_NumericUpDownSampleMultiple.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_NumericUpDownSampleMultiple.ValueChanged += new System.EventHandler(this.m_NumericUpDownSampleMultiple_ValueChanged);
            // 
            // FormConfigureFaultLogParameters
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(703, 600);
            this.Name = "FormConfigureFaultLogParameters";
            this.Text = "Configure - Data Stream";
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
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSampleMultiple)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// Refrence to the 'Sample Multiple' <c>Label</c>.
        /// </summary>
        private System.Windows.Forms.Label m_LegendSampleMultiple;

        /// <summary>
        /// Reference to the <c>NumericUpDown</c> control.
        /// </summary>
        private System.Windows.Forms.NumericUpDown m_NumericUpDownSampleMultiple;
    }
}