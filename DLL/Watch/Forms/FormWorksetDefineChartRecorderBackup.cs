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
 *  File name:  FormWorksetDefineChartRecorder.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/11/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Common;
using Common.Forms;
using Common.Configuration;
using Watch.Properties;

namespace Watch.Forms
{
    /// <summary>
    /// Form to allow the user to configure a chart recorder workset.
    /// </summary>
    public partial class FormWorksetDefineChartRecorderBackup : FormWorksetDefine
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes an new instance of the form. Zero parameter constructor, required by Visual Studio.
        /// </summary>
        public FormWorksetDefineChartRecorderBackup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes an new instance of the form. This constructor is used when a new workset is being created. Populates the 'Available' 
        /// <c>ListBox</c> controls with the appropriate watch variables.
        /// </summary>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        public FormWorksetDefineChartRecorderBackup(WorksetCollection worksetCollection)
            : base(worksetCollection)
        {
            InitializeComponent();

            // Only one column is required fot this workset so delete the tab pages associated with columns 2 and 3.
            m_TabControlWorkset.TabPages.Remove(m_TabPageColumn2);
            m_TabControlWorkset.TabPages.Remove(m_TabPageColumn3);

            // Initialize the column header text boxes, use the default values.
            m_TextBoxHeader1.Text = m_WorksetCollection.Worksets[0].Column[0].HeaderText;

            // ----------------------------
            // OK, Cancel and Apply buttons.
            // ----------------------------
            // Hide the Apply button in this mode and move the position of the OK and Cancel buttons.
            m_ButtonApply.Visible = false;
            m_ButtonOK.Location = m_ButtonCancel.Location;
            m_ButtonCancel.Location = m_ButtonApply.Location;

            // Enable the context menu that allows the user to configure the chart scaling.
            m_MenuItemChangeChartScaleFactor.Visible = true;

            m_ListBox1RowHeader.Items.Clear();
            m_ListBox1RowHeader.Items.AddRange(new object[] {
            " 1",
            " 2",
            " 3",
            " 4",
            " 5",
            " 6",
            " 7",
            " 8"});
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
        public FormWorksetDefineChartRecorderBackup(WorksetCollection worksetCollection, Workset_t workset, bool applyVisible)
            : base(worksetCollection, workset, applyVisible)
        {
            InitializeComponent();

            // Only one column is required fot this workset so delete the tab pages associated with columns 2 and 3.
            m_TabControlWorkset.TabPages.Remove(m_TabPageColumn2);
            m_TabControlWorkset.TabPages.Remove(m_TabPageColumn3);

            // Initialize the column header text boxes, use the default values.
            m_TextBoxHeader1.Text = workset.Column[0].HeaderText;

            // ------------------------------------
            // Update the ListBox controls.
            // -------------------------------------
            WatchItemAddRange(workset.Column[0]);
            UpdateCount();

            // --------------------------
            // OK, Cancel and Apply buttons.
            // ----------------------------
            if (applyVisible == false)
            {
                // Hide the Apply button and move the position of the OK and Cancel buttons.
                m_ButtonApply.Visible = false;
                m_ButtonOK.Location = m_ButtonCancel.Location;
                m_ButtonCancel.Location = m_ButtonApply.Location;
            }
            else
            {
                // Display the Apply button.
                m_ButtonApply.Visible = true;

                // Disable the Apply button until the user modifies the workset.
                m_ButtonApply.Enabled = false;
            }

            // Enable the context menu that allows the user to configure the chart scaling.
            m_MenuItemChangeChartScaleFactor.Visible = true;
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
        #region - [Buttons] -
        /// <summary>
        /// Event handler associated with the 'Move Up' button <c>Click</c> event. Move the selected item up the list.
        /// </summary>
        /// <remarks>Only one item may be selected at a time. If more than one item is selected the action will be ignored.</remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ButtonMoveUp_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ListBox.SelectedIndexCollection selectedIndices = m_ListBoxSelected.SelectedIndices;
            int index;
            object previousItem;
            for (int selection = 0; selection < selectedIndices.Count; ++selection)
            {
                index = selectedIndices[selection];

                // Bounds checking.
                if (index >= 1)
                {
                    previousItem = m_ListBoxSelected.Items[index - 1];
                    m_ListBoxSelected.Items.Remove(previousItem);
                    m_ListBoxSelected.Items.Insert(index, previousItem);
                    m_ListBoxSelected.SetSelected(index - 1, true);

                    previousItem = m_ListBoxChartScaleMin.Items[index - 1];
                    m_ListBoxChartScaleMin.Items.RemoveAt(index - 1);
                    m_ListBoxChartScaleMin.Items.Insert(index, previousItem);

                    previousItem = m_ListBoxChartScaleMax.Items[index - 1];
                    m_ListBoxChartScaleMax.Items.RemoveAt(index - 1);
                    m_ListBoxChartScaleMax.Items.Insert(index, previousItem);

                    previousItem = m_ListBoxUnits.Items[index - 1];
                    m_ListBoxUnits.Items.RemoveAt(index - 1);
                    m_ListBoxUnits.Items.Insert(index, previousItem);
                }
                else
                {
                    break;
                }
            }

            m_ButtonApply.Enabled = true;
            OnDataUpdate(this, new EventArgs());
        }

        /// <summary>
        /// Event handler associated with the 'Move Down' button <c>Click</c> event. Move the selected item down the list.
        /// </summary>
        /// <remarks>Only one item may be selected at a time. If more than one item is selected the action will be ignored.</remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ButtonMoveDown_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ListBox.SelectedIndexCollection selectedIndices = m_ListBoxSelected.SelectedIndices;
            int index;
            object moveItem;
            for (int selection = selectedIndices.Count - 1; selection >= 0; --selection)
            {
                index = selectedIndices[selection];

                // Bounds checking.
                if (index < m_ListBoxSelected.Items.Count - 1)
                {
                    moveItem = m_ListBoxSelected.Items[index];
                    m_ListBoxSelected.Items.Remove(moveItem);
                    m_ListBoxSelected.Items.Insert(index + 1, moveItem);
                    m_ListBoxSelected.SelectedItem = moveItem;

                    moveItem = m_ListBoxChartScaleMin.Items[index];
                    m_ListBoxChartScaleMin.Items.RemoveAt(index);
                    m_ListBoxChartScaleMin.Items.Insert(index + 1, moveItem);

                    moveItem = m_ListBoxChartScaleMax.Items[index];
                    m_ListBoxChartScaleMax.Items.RemoveAt(index);
                    m_ListBoxChartScaleMax.Items.Insert(index + 1, moveItem);

                    moveItem = m_ListBoxUnits.Items[index];
                    m_ListBoxUnits.Items.RemoveAt(index);
                    m_ListBoxUnits.Items.Insert(index + 1, moveItem);
                }
                else
                {
                    break;
                }
            }

            m_ButtonApply.Enabled = true;
            OnDataUpdate(this, new EventArgs());
        }

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
        #endregion - [Buttons] -

        #region - [Context Menu] -
        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Change Chart Scale' context menu option attached to the <c>ListBox</c> containing the watch 
        /// variables that have been added to the workset. Calls the form which allows the user to modify the chart scaling associated with the selected channel.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_MenuItemChangeChartScaleFactor_Click(object sender, EventArgs e)
        {
            int selectedChannelIndex = m_ListBox1.SelectedIndex;

            short oldIdentifier = ((WatchItem_t)m_ListBox1.Items[selectedChannelIndex]).OldIdentifier;

            try
            {
                FormChangeChartScale formChangeChartScale = new FormChangeChartScale(m_ListBox1);
                formChangeChartScale.CalledFrom = this;
                formChangeChartScale.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion - [Context Menu] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Add any supplemental workset fields specific to the child class. Add the chart recorder scaling information.
        /// </summary>
        /// <param name="oldIdentifier">The old identifier of the watch variable that is to be added.</param>
        protected override void AddSupplementalFields(int oldIdentifier)
        {
            WatchVariable watchVariable;
            try
            {
                watchVariable = Lookup.WatchVariableTableByOldIdentifier.RecordList[oldIdentifier];
                if (watchVariable == null)
                {
                    throw new Exception();
                }

                m_ListBoxChartScaleMin.Items.Add(watchVariable.MinChartScale);
                m_ListBoxChartScaleMax.Items.Add(watchVariable.MaxChartScale);
                m_ListBoxUnits.Items.Add(watchVariable.Units);
            }
            catch (Exception)
            {
                m_ListBoxChartScaleMin.Items.Add(double.NaN);
                m_ListBoxChartScaleMax.Items.Add(double.NaN);
                m_ListBoxUnits.Items.Add(CommonConstants.VariableNotDefinedUnitsString);
            }

            return;
        }

        /// <summary>
        /// Remove any supplemental workset fields specific to the child class. Remove the chart recorder scaling information.
        /// </summary>
        /// <param name="removeAtIndex">The index of the ListBox row that is to be removed.</param>
        protected override void RemoveSupplementalFields(int removeAtIndex)
        {
            m_ListBoxChartScaleMin.Items.RemoveAt(removeAtIndex);
            m_ListBoxChartScaleMax.Items.RemoveAt(removeAtIndex);
            m_ListBoxUnits.Items.RemoveAt(removeAtIndex);
        }

        /// <summary>
        /// Update the count labels that show the number of watch variables that are available and the number that have been added to each column of the workset.
        /// </summary>
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
            m_LabelCountTotal.Text = Resources.LegendTotalCount + CommonConstants.Colon + m_WatchElementCount.ToString() +
                                     CommonConstants.Space + Resources.LegendOf + CommonConstants.Space + m_WorksetCollection.EntryCountMax.ToString();
        }

        /// <summary>
        /// Add the watch variables defined in the specified workset column to the <c>ListBox</c> controls that display the description and the chart recorder scaling 
        /// information for each chart recorder channel.
        /// </summary>
        /// <param name="worksetColumn">The column of the workset that is to be added to the <c>ListBox</c> control.</param>
        protected void WatchItemAddRange(Column_t worksetColumn)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            m_ListBox1.Items.Clear();
            m_ListBoxChartScaleMin.Items.Clear();
            m_ListBoxChartScaleMax.Items.Clear();
            m_ListBoxUnits.Items.Clear();

            m_ListBox1.SuspendLayout();
            m_ListBoxChartScaleMin.SuspendLayout();
            m_ListBoxChartScaleMax.SuspendLayout();
            m_ListBoxUnits.SuspendLayout();

            WatchItem_t watchItem;
            short oldIdentifier;
            WatchVariable watchVariable;
            ChartScale_t chartScale;
            for (int index = 0; index < worksetColumn.OldIdentifierList.Count; index++)
            {
                watchItem = new WatchItem_t();
                oldIdentifier = worksetColumn.OldIdentifierList[index];
                watchItem.OldIdentifier = oldIdentifier;
                watchItem.Added = true;
                m_ListBox1.Items.Add(watchItem);

                // Check whether the watch variable exists.
                try
                {
                    watchVariable = watchVariable = Lookup.WatchVariableTableByOldIdentifier.RecordList[oldIdentifier];
                    if (watchVariable == null)
                    {
                        throw new Exception();
                    }

                    // Check whether the chart scaling for the current watch variable has been defined.
                    try
                    {
                        chartScale = worksetColumn.ChartScaleList[index];
                    }
                    catch (Exception)
                    {
                        // No - Set up the default chart scaling for the watch variable based upon the data dictionary.
                        chartScale = new ChartScale_t();
                        chartScale.ChartScaleMax = watchVariable.MaxChartScale;
                        chartScale.ChartScaleMin = watchVariable.MinChartScale;
                        chartScale.Units = watchVariable.Units;
                    }
                }
                catch (Exception)
                {
                    // Watch variable does not exist.
                    chartScale.ChartScaleMax = double.NaN;
                    chartScale.ChartScaleMin = double.NaN;
                    chartScale.Units = CommonConstants.VariableNotDefinedUnitsString;
                }

                m_ListBoxChartScaleMin.Items.Add(chartScale.ChartScaleMin);
                m_ListBoxChartScaleMax.Items.Add(chartScale.ChartScaleMax);
                m_ListBoxUnits.Items.Add(chartScale.Units);
            }

            m_ListBox1.PerformLayout();
            m_ListBoxChartScaleMin.PerformLayout();
            m_ListBoxChartScaleMax.PerformLayout();
            m_ListBoxUnits.PerformLayout();

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Convert the current user setting to a workset.
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
            workset.SampleMultiple = Workset_t.DefaultSampleMultiple;
            workset.CountMax = m_WorksetCollection.EntryCountMax;
            workset.SecurityLevel = Security.SecurityLevelCurrent;

            // Copy the old identifiers defined in Column[0].
            workset.Column = new Column_t[1];
            workset.Column[0].HeaderText = m_TextBoxHeader1.Text;
            workset.Column[0].OldIdentifierList = new List<short>();
            workset.Column[0].ChartScaleList = new List<ChartScale_t>();
            for (int index = 0; index < m_ListBox1.Items.Count; index++)
            {
                workset.Column[0].OldIdentifierList.Add(((WatchItem_t)m_ListBox1.Items[index]).OldIdentifier);
            }

            // Copy the chart scale values.
            ChartScale_t chartScale;
            for (int index = 0; index < m_ListBox1.Items.Count; index++)
            {
                chartScale.ChartScaleMin = (double)m_ListBoxChartScaleMin.Items[index];
                chartScale.ChartScaleMax = (double)m_ListBoxChartScaleMax.Items[index];
                chartScale.Units = (string)m_ListBoxUnits.Items[index];
                workset.Column[0].ChartScaleList.Add(chartScale);
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
                        workset.WatchElementList.Add(CommonConstants.VariableNotDefinedWatchIdentifier);
                    }
                    else
                    {
                        workset.WatchElementList.Add(watchVariable.Identifier);
                    }
                }
                catch (Exception)
                {
                    workset.WatchElementList.Add(CommonConstants.VariableNotDefinedWatchIdentifier);
                }
            }
            workset.WatchElementList.Sort();
            workset.Count = workset.WatchElementList.Count;

            if (workset.Count != m_WatchElementCount)
            {
                throw new ArgumentException(Resources.EMWorksetIntegrityCheckFailed, "FormWorksetDefineFaultLog.ConvertToWorkset() - [workset.WatchElements.Count]");
            }

            return workset;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Get the reference to the <c>ListBox</c> control that contains the minimum chart scale values for each channel of the chart recorder.
        /// </summary>
        internal ListBox ListBoxChartScaleMin
        {
            get { return m_ListBoxChartScaleMin; }
        }

        /// <summary>
        /// Get the reference to the <c>ListBox</c> control that contains the minimum chart scale values for each channel of the chart recorder.
        /// </summary>
        internal ListBox ListBoxChartScaleMax
        {
            get { return m_ListBoxChartScaleMax; }
        }
        #endregion --- Properties ---
    }
}