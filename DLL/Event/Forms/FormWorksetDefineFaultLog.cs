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
 *  File name:  FormWorksetDefineFaultLog.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/16/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/26/11    1.1     K.McD           1.  Auto-modified as a result of the property name changes associated with the Parameter class.
 * 
 *  02/21/11    1.2     K.McD           1.  Renamed the WriteSampleInterval() method to UpdateSampleMultiple().
 *                                      2.  Modified the UpdateCount() method such that the total count of watch variables added to the workset is not updated.
 *                                          As the fault log worksets only have a single column this information is redundant, also, the expected number of 
 *                                          watch variables is not known and will vary depending upon which log the data stream is associated with.
 *                                      3.  Removed the text box controls associated with the sample interval and the frame interval, the form now only displays the 
 *                                          sample multiple associated with the datastream.
 * 
 *  02/28/11    1.3     K.McD           1.  Modified the signature associated with the ConvertToWorkset() method.
 *                                      2.  Auto-modified as a result of a number of resource name changes.
 * 
 *  03/17/11    1.4     K.McD           1.  Auto-modified as a result of property name changes associated with the Common.Security class.
 * 
 *  03/28/11    1.5     K.McD           1.  Bug fix SNCR001.112. Modified to use the old identifier field of the data dictionary, rather than the watch identifier 
 *                                          field, to define the watch variables that are to be included in the workset as these remain consistent following a 
 *                                          data dictionary update.
 * 
 *                                      2.  Auto-modified as a result of a number of name changes to the properties and methods of external classes.
 *                                      
 *  05/23/11    1.6     K.McD           1.  Modified a constructor to accommodate the signature change to the FormWorksetDefine.WatchItemAddRange() method.
 *                                      2.  Auto-modified as a result of a name change to a TabControl user control.
 *                                      
 *  06/21/11    1.7     K.McD           1.  Auto-modified as a result of a name change to an inherited member variable.
 *  
 *  07/24/13    1.8     K.McD           1.  Included update of the 'Total Count' label in the UpdateCount() method.
 *                                      2.  Added code to all non zero parameter constructors to set the Visible property of the 'Total Count' and 'Count' labels
 *                                          depending upon whether the project supports multiple datastream types or not. 
 *                                           
 *                                          If the project supports multiple datastream types, the 'Total Count' label is not shown and the 'Count' label is shown,
 *                                          and vice-versa. This helps avoid confusion, as the upper limit of the 'Total Count' label displays the number of
 *                                          watch variables asssociated with the datastream type that has tha largest number of watch variables and may not apply
 *                                          to the current workset.
 *                                          
 *  03/13/15    1.9     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                              1.  MOC-0171-26. Kawasaki pointed out that Chart Recorder tabs are identified as ‘COLUMNS’ and requested that this
 *                                                  be changed.
 *                                                  
 *                                              2.  MOC-0171-28. A check will be included as part of the ‘Save’ procedure to ensure that an empty workset cannot be saved.
 *                                              
 *                                      Modifications
 *                                      1.  Ref.: 1.1.1.
 *                                          1.  Modified both non zero parameter constructors to check whether the project supports multiple data stream types or whether
 *                                              the number of parameters supported by the data stream exceeds the number that can be displayed on the TabPage without
 *                                              requiring scroll bars and calls AddRowHeader() or NoRowHeader() as appropriate.
 *                                          
 *                                          2.  Included the constant WatchSizeFaultLogMax which defines the number of parameters that can be displayed on the TabPage
 *                                              without requiring scroll bars.
 *                                              
 *                                      2.  Ref.: 1.1.2.
 *                                          1.  Modified the m_NumericUpDownSampleMultiple_ValueChanged() method to call the EnableApplyAndOKButtons() method. This
 *                                              ensures that a full check is made on the state of the workset whenever the 'Sample Multiple' value is changed.
 *  
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Common;
using Common.Configuration;
using Common.Forms;
using Event.Properties;

namespace Event.Forms
{
    /// <summary>
    /// Form to allow the user to define: (a) the watch variables that are associated with a fault log data stream (b) the order in which they are to be displayed and 
    /// (c) the multiple of the recording interval at which the data is recorded.
    /// </summary>
    public partial class FormWorksetDefineFaultLog : FormWorksetDefine
    {
        #region --- Constants ---
        /// <summary>
        /// The .NET format string used to display the sample interval.
        /// </summary>
        protected const string FormatStringNumericString = "###,###,##0.####";

        /// <summary>
        /// The maximum FaultLog/DataStream WatchSize that can be displayed using the 'Row Header' <c>ListBox</c>. Value: 16.  
        /// </summary>
        private const int WatchSizeFaultLogMax = 16;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The multiple of the recording interval at which the data is to be recorded.
        /// </summary>
        protected short m_SampleMultiple;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes an new instance of the form. Zero parameter constructor, required by Visual Studio.
        /// </summary>
        public FormWorksetDefineFaultLog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes an new instance of the form. This constructor is used when a new workset is being created. Populates the 'Available' 
        /// <c>ListBox</c> controls with the appropriate watch variables.
        /// </summary>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        public FormWorksetDefineFaultLog(WorksetCollection worksetCollection)
            : base(worksetCollection)
        {
            InitializeComponent();

            // Check whether the 'Row Header' ListBox can be used to display the channel numbers. This is only possible if
            // the project doesn't support multiple data stream types and the number of parameters supported by the data
            // stream can be displayed on the TabPage without the need for scroll bars i.e. <= WatchSizeFaultLogMax.
            if ((Parameter.SupportsMultipleDataStreamTypes == false) && (Parameter.WatchSizeFaultLog <= WatchSizeFaultLogMax))
            {
                AddRowHeader();
            }
            else
            {
                NoRowHeader();
            }

            // Only one column is required fot this workset so delete the tab pages associated with columns 2 and 3.
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn2);
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn3);

            // Initialize the column header text boxes, use the default values.
            m_TextBoxHeader1.Text = m_WorksetCollection.Worksets[0].Column[0].HeaderText;

            UpdateSampleMultiple(1);
        }

        /// <summary>
        /// <para>
        /// Initializes an new instance of the form for use when EDITing a workset. Populates the 'Configuration' ListBoxes with the data associated with the 
        /// specified configuration and populates the 'Available' ListBoxes with the remaining data i.e. the difference between the configuration and the default data.
        /// </para>
        /// <para>
        /// If the <paramref name="applyVisible"/> parameter is set to true the form will include an apply button so that the user can update the workset without closing
        /// the form between updates. This is especially useful when modifying the active workset while the workset is on display.
        /// </para>
        /// </summary>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        /// <param name="workset">The workset that is to be edited.</param>
        /// <param name="applyVisible">Flag to specify whether the Apply button is to be visible; True, displays the Apply button, false, hides the Apply button.</param>
        public FormWorksetDefineFaultLog(WorksetCollection worksetCollection, Workset_t workset, bool applyVisible)
            : base(worksetCollection, workset, applyVisible)
        {
            InitializeComponent();

            // Check whether the 'Row Header' ListBox can be used to display the channel numbers. This is only possible if
            // the project doesn't support multiple data stream types and the number of parameters supported by the data
            // stream can be displayed on the TabPage without the need for scroll bars i.e. <= WatchSizeFaultLogMax.
            if ((Parameter.SupportsMultipleDataStreamTypes == false) && (Parameter.WatchSizeFaultLog <= WatchSizeFaultLogMax))
            {
                AddRowHeader();
            }
            else
            {
                NoRowHeader();
            }

            // Only one column is required fot this workset so delete the tab pages associated with columns 2 and 3.
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn2);
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn3);

            // Initialize the column header text boxes, use the default values.
            m_TextBoxHeader1.Text = workset.Column[0].HeaderText;

            // ------------------------------------
            // Update the 'Column' ListBox control.
            // -------------------------------------
            WatchItemAddRange(m_ListBox1, workset.Column[0]);
            UpdateCount();

            UpdateSampleMultiple(workset.SampleMultiple);
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Cleanup(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.

                #region --- Windows Form Designer Variables ---
                // Detach the event handler delegates.

                // Set the Windows Form Designer Variables to null.

                #endregion --- Windows Form Designer Variables ---
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
            }
            finally
            {
                base.Cleanup(disposing);
            }
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        /// <summary>
        /// Event handler for the Apply button <c>Click</c> event. Add the defined workset to the to list of available worksets.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ButtonApply_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // ------------------------------------------
            // Copy the definitions to a new workset and update the WorksetManager class.
            // ------------------------------------------
            Cursor = Cursors.WaitCursor;
            Workset_t workset;
            workset = ConvertToWorkset(m_TextBoxName.Text);

            if (m_CreateMode)
            {
                m_WorksetCollection.Add(workset);
            }
            else
            {
                m_WorksetCollection.Edit(workset.Name, workset);
            }

            Save();

            m_ButtonApply.Enabled = false;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the <c>ValueChanged</c> event associated with the <c>NumericUpDown</c> control. Update the member variable that records the sample multiple 
        /// and raise a 'DataUpdate' event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_NumericUpDownSampleMultiple_ValueChanged(object sender, EventArgs e)
        {
            m_SampleMultiple = (short)m_NumericUpDownSampleMultiple.Value;

            EnableApplyAndOKButtons();
            OnDataUpdate(this, new EventArgs());
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Update the sample multiple <c>NumericUpDown</c> control and member variable with the specified sample multiple value.
        /// </summary>
        /// <param name="sampleMultiple">The sample multiple.</param>
        protected void UpdateSampleMultiple(short sampleMultiple)
        {
            m_SampleMultiple = sampleMultiple;

            // Include a try/catch block in case the sample multiple is invalid.
            try
            {
                m_NumericUpDownSampleMultiple.Value = m_SampleMultiple;
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.EMSampleMultipleInvalid, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_SampleMultiple = 1;
                m_NumericUpDownSampleMultiple.Value = m_SampleMultiple;
            }
        }

        /// <summary>
        /// Update the count labels that show the number of watch variables that are available and the number that have been added to each column of the workset.
        /// </summary>
        /// <remarks>The worksets associated with fault log data stream parameter only support a single column and the maximum count will vary depending upon the data 
        /// stream type used by the associated event log.</remarks>
        protected override void UpdateCount()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_LabelAvailableCount.Text = Resources.LegendAvailable + CommonConstants.Colon + m_ListBoxAvailable.Items.Count.ToString();

            int countColumn1 = m_ListBox1.Items.Count;
            m_LabelCount1.Text = Resources.LegendCount + CommonConstants.Colon + countColumn1.ToString();

            // Total number of watch variables in the workset.
            m_LabelCountTotal.Text = Resources.LegendTotalCount + CommonConstants.Colon + countColumn1.ToString() +
                                     CommonConstants.Space + Resources.LegendOf + CommonConstants.Space + EntryCountMax.ToString();
        }

        /// <summary>
        /// Convert the current user settings to a workset.
        /// </summary>
        /// <param name="worksetName">The name of the workset.</param>
        /// <returns>The user settings converted to a workset.</returns>
        protected Workset_t ConvertToWorkset(string worksetName)
        {
            // --------------------------------------------------------------------------
            // Copy the definitions to a new workset and update the WorksetManager class.
            // --------------------------------------------------------------------------
            Workset_t workset = new Workset_t();

            workset.Name = worksetName;
            workset.SampleMultiple = m_SampleMultiple;
            workset.CountMax = m_WorksetCollection.EntryCountMax;
            workset.SecurityLevel = Security.SecurityLevelCurrent;

            // Copy the old identifiers defined in Column[0].
            workset.Column = new Column_t[1];
            workset.Column[0].HeaderText = m_TextBoxHeader1.Text;
            workset.Column[0].OldIdentifierList = new List<short>();
            for (int index = 0; index < m_ListBox1.Items.Count; index++)
            {
                workset.Column[0].OldIdentifierList.Add(((WatchItem_t)m_ListBox1.Items[index]).OldIdentifier);
            }

            // Copy the watchItems list.
            workset.WatchItems = new WatchItem_t[m_WatchItems.Length];
            Array.Copy(m_WatchItems, workset.WatchItems, m_WatchItems.Length);

            // Create the WatchElementList property from the column entries.
            workset.WatchElementList = new List<short>();
            short oldIdentifier;
            WatchVariable watchVariable;
            
            for (int rowIndex = 0; rowIndex < workset.Column[0].OldIdentifierList.Count; rowIndex++)
            {
                oldIdentifier = workset.Column[0].OldIdentifierList[rowIndex];
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                    if (watchVariable == null)
                    {
                        workset.WatchElementList.Add(CommonConstants.WatchIdentifierNotDefined);
                    }
                    else
                    {
                        workset.WatchElementList.Add(watchVariable.Identifier);
                    }
                }
                catch (Exception)
                {
                    workset.WatchElementList.Add(CommonConstants.WatchIdentifierNotDefined);
                }
            }
            workset.WatchElementList.Sort();
            workset.Count = workset.WatchElementList.Count;

            if (workset.Count != m_ListItemCount)
            {
                throw new ArgumentException(Resources.EMWorksetIntegrityCheckFailed, "FormWorksetDefineFaultLog.ConvertToWorkset() - [workset.WatchElements.Count]");
            }

            return workset;
        }
        #endregion --- Methods ---
    }
}