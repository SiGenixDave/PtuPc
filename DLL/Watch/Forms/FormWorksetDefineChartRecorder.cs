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
 *  05/23/11    1.1     K.McD           1.  Significant modifications to accommodate the changes to the FormWorksetDefine parent class introduced in version 1.16 of 
 *                                          Common.dll. These were made to allow the user to define, store and download the chart recorder scaling information 
 *                                          associated with each chart recorder workset.
 *                                          
 *                                              a.  Added support for the IChartScale interface.
 *                                              b.  Auto-modified the name of an inherited TabControl.
 *                                              c.  Overrode the event handlers for the 'Move Up' and 'Move Down' buttons to include the chart scale information.
 *                                              d.  Added the event handler for 'Change Chart Scale' context menu option.
 *                                              e.  Added the AddSupplementalFields() and RemoveSupplementalFields() methods.
 *                                              f.  Overrode the WatchItemsAddRange() method.
 *                                              
 *  05/24/11    1.2     K.McD           1.  Corrected the chart recorder scaling assignments in the ConvertToWorkset() method.
 *                                      2.  Added the overridden DownloadWorkset() method and refactored the class to use this method.
 *                                      3.  Corrected the chart recorder scaling assignments in the overridden WatchItemsAddRange() method.
 *                                      
 *  05/26/11    1.3     K.McD           1.  Modified the AddSupplementalFields() method such that, if the watch variable does not exist, the string representing 
 *                                          'not defined' is displayed in the ListBox controls associated with the upper and lower Y axis limits rather than 'NaN'.
 *                                          
 *                                      2.  Modified the WatchItemsAddRange() method such that, if the value for the upper and lower Y axis limits stored in the 
 *                                          workset is 'double.NaN', the string representing 'not defined' is displayed in the ListBox controls rather than 'NaN'.
 *                                          
 *                                      3.  Bug fix. Added an override to the ClearListBoxColumnItems() method  to ensure that all ListBox controls associated with 
 *                                          the workset are cleared.
 *                                          
 *                                      4.  Modified the ConvertToWorkset() method to ensure that the upper and lower limits of the Y axis values are converted back 
 *                                          to 'NaN' if the value displayed in the corresponding ListBox control is the string representation of 'not defined'.
 *                                          
 *  06/21/11    1.4     K.McD           1.  Auto-modified as a result of a name change to an inherited member variable.
 *  
 *  06/31/11    1.5     K.McD           1.  Added support for hexadecimal Y axis limits. The ListBox controls that are used to display the upper and lower limits now 
 *                                          store the limits as text rather than double values.
 *                                          
 *                                              (a) Modified the AddSupplementalFields() method to check whether the Y axis limits are to be displayed in hexadecimal or 
 *                                                  decimal format and to add the default chart scale values, as text strings, to the Items property of the ListBox 
 *                                                  controls associated with the upper and lower Y axis limits.
 *                                                  
 *                                              (b) Modified the WatchItemAddRange() method to check whether the Y axis limits are to be displayed in hexadecimal or 
 *                                                  decimal format and to add the chart scale values, as text string, to the Items property of the ListBox controls 
 *                                                  associated with the upper and lower Y axis limits.
 *                                                  
 *                                              (c) Modified the ConvertToWorkset() method to include support for hexadecimal values and to take into account the fact 
 *                                                  that the ListBox controls associated with the upper and lower Y axis limits now store the chart scale values as 
 *                                                  text strings.
 *                                                  
 *                                              (d) Removed the superfluous code that determined the old identifier associated with the selected watch variable from the 
 *                                                  m_MenuItemChangeChartScaleFactor_Click() method.
 *                                                  
 *  04/01/15    1.6     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  MOC-0171-28. A check will be included as part of the ‘Save’ procedure to ensure that an empty workset cannot be saved.
 *                                          
 *                                      2.  SNCR - R188 PTU [20 Mar 2015] Item 6. Bug associated with saving chart recorder worksets that contain watch variables
 *                                          that have different types of FormatString values e.g. 'General Number' and 'Hexadecimal'.
 *
 *                                      Modifications
 *                                      1.  Ref.: 1.1. Modified the 'Move Up' and 'Move Down' Button event handlers to call the EnableApplyAndOKButtons() method
 *                                          instead of just setting the 'Enabled' property of the 'Apply' Button to true. This ensures that a full check is made
 *                                          on the state of the workset whenever the Button controls are pressed.
 *                                          
 *                                      2.  Ref.: 2. Modified the ConvertToWorkset() and the WatchItemsAddRange() methods to set the local hexFormat variable to
 *                                          either true or false, depending upon the value of the 'FormatString' field. 
 *                                          
 *                                          Previously, it had not been updated on every pass of the 'for' loop so if the workset contained a watch variable
 *                                          that was formatted as 'Hexadecimal' followed by a watch variable that was formatted as 'General Number', the
 *                                          second watch variables would have been processed as a hexadecimal number.
 *  
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

using Common;
using Common.Configuration;
using Common.Forms;
using Watch.Properties;

namespace Watch.Forms
{
    /// <summary>
    /// Form to allow the user to define a chart recorder workset.
    /// </summary>
    public partial class FormWorksetDefineChartRecorder : FormWorksetDefine, IChartScale
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes an new instance of the form. Zero parameter constructor.
        /// </summary>
        public FormWorksetDefineChartRecorder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes an new instance of the form. This constructor is used when a new workset is being created. Populates the 'Available' 
        /// <c>ListBox</c> controls with the appropriate watch variables.
        /// </summary>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        public FormWorksetDefineChartRecorder(WorksetCollection worksetCollection)
            : base(worksetCollection)
        {
            InitializeComponent();

            // Only one column is required fot this workset so delete the tab pages associated with columns 2 and 3.
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn2);
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn3);

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
        public FormWorksetDefineChartRecorder(WorksetCollection worksetCollection, Workset_t workset, bool applyVisible)
            : base(worksetCollection, workset, applyVisible)
        {
            InitializeComponent();

            // Only one column is required fot this workset so delete the tab pages associated with columns 2 and 3.
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn2);
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn3);

            // Initialize the column header text boxes, use the default values.
            m_TextBoxHeader1.Text = workset.Column[0].HeaderText;

            // ------------------------------------
            // Update the ListBox controls.
            // -------------------------------------
            WatchItemAddRange(m_ListBox1, workset.Column[0]);
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

                    previousItem = m_ListBoxChartScaleUpperLimit.Items[index - 1];
                    m_ListBoxChartScaleUpperLimit.Items.RemoveAt(index - 1);
                    m_ListBoxChartScaleUpperLimit.Items.Insert(index, previousItem);

                    previousItem = m_ListBoxChartScaleLowerLimit.Items[index - 1];
                    m_ListBoxChartScaleLowerLimit.Items.RemoveAt(index - 1);
                    m_ListBoxChartScaleLowerLimit.Items.Insert(index, previousItem);

                    previousItem = m_ListBoxUnits.Items[index - 1];
                    m_ListBoxUnits.Items.RemoveAt(index - 1);
                    m_ListBoxUnits.Items.Insert(index, previousItem);
                }
                else
                {
                    break;
                }
            }

            EnableApplyAndOKButtons();
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

                    moveItem = m_ListBoxChartScaleUpperLimit.Items[index];
                    m_ListBoxChartScaleUpperLimit.Items.RemoveAt(index);
                    m_ListBoxChartScaleUpperLimit.Items.Insert(index + 1, moveItem);

                    moveItem = m_ListBoxChartScaleLowerLimit.Items[index];
                    m_ListBoxChartScaleLowerLimit.Items.RemoveAt(index);
                    m_ListBoxChartScaleLowerLimit.Items.Insert(index + 1, moveItem);

                    moveItem = m_ListBoxUnits.Items[index];
                    m_ListBoxUnits.Items.RemoveAt(index);
                    m_ListBoxUnits.Items.Insert(index + 1, moveItem);
                }
                else
                {
                    break;
                }
            }

            EnableApplyAndOKButtons();
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
        #region - [FormWorksetDefine] -
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

                if (watchVariable.FormatString.ToLower().Equals(CommonConstants.DDFormatStringHex))
                {
                    m_ListBoxChartScaleLowerLimit.Items.Add(CommonConstants.HexValueIdentifier + ((uint)watchVariable.MinChartScale).ToString(CommonConstants.FormatStringHex));
                    m_ListBoxChartScaleUpperLimit.Items.Add(CommonConstants.HexValueIdentifier + ((uint)watchVariable.MaxChartScale).ToString(CommonConstants.FormatStringHex));
                }
                else
                {
                    m_ListBoxChartScaleLowerLimit.Items.Add(watchVariable.MinChartScale.ToString(CommonConstants.FormatStringNumeric));
                    m_ListBoxChartScaleUpperLimit.Items.Add(watchVariable.MaxChartScale.ToString(CommonConstants.FormatStringNumeric));
                }

                m_ListBoxUnits.Items.Add(watchVariable.Units);
            }
            catch (Exception)
            {
                m_ListBoxChartScaleLowerLimit.Items.Add(CommonConstants.ChartScaleValueNotDefinedString);
                m_ListBoxChartScaleUpperLimit.Items.Add(CommonConstants.ChartScaleValueNotDefinedString);
                m_ListBoxUnits.Items.Add(CommonConstants.ChartScaleUnitsNotDefinedString);
            }

            return;
        }

        /// <summary>
        /// Remove any supplemental workset fields specific to the child class. Remove the chart recorder scaling information.
        /// </summary>
        /// <param name="removeAtIndex">The index of the ListBox row that is to be removed.</param>
        protected override void RemoveSupplementalFields(int removeAtIndex)
        {
            m_ListBoxChartScaleUpperLimit.Items.RemoveAt(removeAtIndex);
            m_ListBoxChartScaleLowerLimit.Items.RemoveAt(removeAtIndex);
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
            m_LabelCountTotal.Text = Resources.LegendTotalCount + CommonConstants.Colon + m_ListItemCount.ToString() +
                                     CommonConstants.Space + Resources.LegendOf + CommonConstants.Space + m_WorksetCollection.EntryCountMax.ToString();
        }

        /// <summary>
        /// Add the watch variables defined in the specified workset column to the <c>ListBox</c> controls that display the description and the chart recorder scaling 
        /// information for each chart recorder channel.
        /// </summary>
        /// <param name="listBox">The <c>ListBox</c> to which the items are to be added.</param>
        /// <param name="worksetColumn">The column of the workset that is to be added to the <c>ListBox</c> control.</param>
        protected override void WatchItemAddRange(ListBox listBox, Column_t worksetColumn)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            listBox.Items.Clear();
            m_ListBoxChartScaleUpperLimit.Items.Clear();
            m_ListBoxChartScaleLowerLimit.Items.Clear();
            m_ListBoxUnits.Items.Clear();

            listBox.SuspendLayout();
            m_ListBoxChartScaleUpperLimit.SuspendLayout();
            m_ListBoxChartScaleLowerLimit.SuspendLayout();
            m_ListBoxUnits.SuspendLayout();

            bool hexFormat = false;
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
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.RecordList[oldIdentifier];
                    if (watchVariable == null)
                    {
                        throw new Exception();
                    }

                    // Determine the format of the watch variable.
                    hexFormat = (watchVariable.FormatString.ToLower().Equals(CommonConstants.DDFormatStringHex)) ? true : false;

                    // Check whether the chart scaling for the current watch variable has been defined.
                    try
                    {
                        chartScale = worksetColumn.ChartScaleList[index];
                    }
                    catch (Exception)
                    {
                        // No - Set up the default chart scaling for the watch variable based upon the data dictionary.
                        chartScale = new ChartScale_t();
                        chartScale.ChartScaleLowerLimit = watchVariable.MinChartScale;
                        chartScale.ChartScaleUpperLimit = watchVariable.MaxChartScale;
                        chartScale.Units = watchVariable.Units;
                    }
                }
                catch (Exception)
                {
                    // Watch variable does not exist.
                    chartScale.ChartScaleLowerLimit = double.NaN;
                    chartScale.ChartScaleUpperLimit = double.NaN;
                    chartScale.Units = CommonConstants.ChartScaleUnitsNotDefinedString;
                }

                // Rather tha displaying 'NaN' if the chart scale values are undefined, display the default string used to represent a chart scale value that is not defined.
                if (chartScale.ChartScaleLowerLimit.Equals(double.NaN))
                {
                    m_ListBoxChartScaleLowerLimit.Items.Add(CommonConstants.ChartScaleValueNotDefinedString);
                }
                else
                {
                    if (hexFormat == true)
                    {
                        m_ListBoxChartScaleLowerLimit.Items.Add(CommonConstants.HexValueIdentifier + ((uint)chartScale.ChartScaleLowerLimit).ToString(CommonConstants.FormatStringHex));
                    }
                    else
                    {
                        m_ListBoxChartScaleLowerLimit.Items.Add(chartScale.ChartScaleLowerLimit.ToString(CommonConstants.FormatStringNumeric));
                    }
                }

                if (chartScale.ChartScaleUpperLimit.Equals(double.NaN))
                {
                    m_ListBoxChartScaleUpperLimit.Items.Add(CommonConstants.ChartScaleValueNotDefinedString);
                }
                else
                {
                    if (hexFormat == true)
                    {
                        m_ListBoxChartScaleUpperLimit.Items.Add(CommonConstants.HexValueIdentifier + ((uint)chartScale.ChartScaleUpperLimit).ToString(CommonConstants.FormatStringHex));
                    }
                    else
                    {
                        m_ListBoxChartScaleUpperLimit.Items.Add(chartScale.ChartScaleUpperLimit.ToString(CommonConstants.FormatStringNumeric));
                    }
                }

                m_ListBoxUnits.Items.Add(chartScale.Units);
            }

            listBox.PerformLayout();
            m_ListBoxChartScaleLowerLimit.PerformLayout();
            m_ListBoxChartScaleUpperLimit.PerformLayout();
            m_ListBoxUnits.PerformLayout();

            Cursor = Cursors.Default;
        }
        #endregion - [FormWorksetDefine] -

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

            #region - [Column] -
            workset.Column = new Column_t[1];
            workset.Column[0].HeaderText = m_TextBoxHeader1.Text;
            workset.Column[0].OldIdentifierList = new List<short>();
            workset.Column[0].ChartScaleList = new List<ChartScale_t>();

            #region - [OldIdentifierList] -
            for (int index = 0; index < m_ListBox1.Items.Count; index++)
            {
                workset.Column[0].OldIdentifierList.Add(((WatchItem_t)m_ListBox1.Items[index]).OldIdentifier);
            }
            #endregion - [OldIdentifierList] -

            #region - [ChartScaleList] -
            // The old identifer associated with the current watch variable.
            short oldIdentifier;

            // The current watch variable.
            WatchVariable watchVariable;

            // A flag to indicate whether the current watch variable is to be displayed in hexadecimal format. True, if the variable is to be displayed in hex format.
            bool hexFormat = false;

            // The upper and lower chart scale values as a text string.
            string chartScaleLowerLimitText, chartScaleUpperLimitText;
            ChartScale_t chartScale = new ChartScale_t();

            // The current culture information. Used to parse hexadecimal values.
            CultureInfo provider = new CultureInfo(CommonConstants.CultureInfoString);

            // A flag to indicate whether the parse operationwas successful. True, if the parse was successful.
            bool successfulParse;
            for (int index = 0; index < m_ListBox1.Items.Count; index++)
            {
                // Get the watch variable associated with the current item.
                oldIdentifier = ((WatchItem_t)m_ListBox1.Items[index]).OldIdentifier;
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                    if (watchVariable == null)
                    {
                        throw new Exception();
                    }

                    // Determine the format of the watch variable.
                    hexFormat = (watchVariable.FormatString.ToLower().Equals(CommonConstants.DDFormatStringHex)) ? true : false;

                    chartScaleLowerLimitText = (string)m_ListBoxChartScaleLowerLimit.Items[index];
                    chartScaleUpperLimitText = (string)m_ListBoxChartScaleUpperLimit.Items[index];

                    if (m_ListBoxChartScaleLowerLimit.Items[index].Equals(CommonConstants.ChartScaleValueNotDefinedString))
                    {
                        chartScale.ChartScaleLowerLimit = double.NaN;
                    }
                    else
                    {
                        if (hexFormat == true)
                        {
                            uint lowerLimitAsUInt32;
                            string strippedChartScaleLowerLimitText;

                            // Strip out the leading HexValueIdentifier.
                            Debug.Assert(chartScaleLowerLimitText.Contains(CommonConstants.HexValueIdentifier), "FormConfigureChartRecorder.ConvertToWorkset() - [chartScaleLowerLimitText.Contains(HexValueIdentifier)]");
                            strippedChartScaleLowerLimitText = chartScaleLowerLimitText.Remove(0, CommonConstants.HexValueIdentifier.Length);

                            // Check that the value entered is a valid 32 bit hexadecimal value.
                            successfulParse = UInt32.TryParse(strippedChartScaleLowerLimitText, NumberStyles.HexNumber, provider, out lowerLimitAsUInt32);
                            if (successfulParse == false)
                            {
                                chartScale.ChartScaleLowerLimit = double.NaN;
                            }
                            else
                            {
                                chartScale.ChartScaleLowerLimit = lowerLimitAsUInt32;
                            }
                        }
                        else
                        {
                            double lowerLimitAsDouble;

                            // Check that the value entered is a valid 32 bit decimal value.
                            successfulParse = double.TryParse(chartScaleLowerLimitText, out lowerLimitAsDouble);
                            if (successfulParse == false)
                            {
                                chartScale.ChartScaleLowerLimit = double.NaN;
                            }
                            else
                            {
                                chartScale.ChartScaleLowerLimit = lowerLimitAsDouble;
                            }
                        }
                    }

                    if (m_ListBoxChartScaleUpperLimit.Items[index].Equals(CommonConstants.ChartScaleValueNotDefinedString))
                    {
                        chartScale.ChartScaleUpperLimit = double.NaN;
                    }
                    else
                    {
                        if (hexFormat == true)
                        {
                            uint upperLimitAsUInt32;
                            string strippedChartScaleUpperLimitText;

                            // Strip out the leading HexValueIdentifier.
                            Debug.Assert(chartScaleUpperLimitText.Contains(CommonConstants.HexValueIdentifier), "FormConfigureChartRecorder.ConvertToWorkset() - [chartScaleUpperLimitText.Contains(HexValueIdentifier)]");
                            strippedChartScaleUpperLimitText = chartScaleUpperLimitText.Remove(0, CommonConstants.HexValueIdentifier.Length);

                            // Check that the value entered is a valid 32 bit hexadecimal value.
                            successfulParse = UInt32.TryParse(strippedChartScaleUpperLimitText, NumberStyles.HexNumber, provider, out upperLimitAsUInt32);
                            if (successfulParse == false)
                            {
                                chartScale.ChartScaleUpperLimit = double.NaN;
                            }
                            else
                            {
                                chartScale.ChartScaleUpperLimit = upperLimitAsUInt32;
                            }
                        }
                        else
                        {
                            double upperLimitAsDouble;

                            // Check that the value entered is a valid 32 bit decimal value.
                            successfulParse = double.TryParse(chartScaleUpperLimitText, out upperLimitAsDouble);
                            if (successfulParse == false)
                            {
                                chartScale.ChartScaleUpperLimit = double.NaN;
                            }
                            else
                            {
                                chartScale.ChartScaleUpperLimit = upperLimitAsDouble;
                            }
                        }
                    }

                    chartScale.Units = (string)m_ListBoxUnits.Items[index];
                    workset.Column[0].ChartScaleList.Add(chartScale);
                }
                catch (Exception)
                {
                    // The watch variable does not exist, add an empty chart scale value.
                    chartScale.ChartScaleLowerLimit = double.NaN;
                    chartScale.ChartScaleUpperLimit = double.NaN;
                    chartScale.Units = CommonConstants.ChartScaleUnitsNotDefinedString;
                    workset.Column[0].ChartScaleList.Add(chartScale);
                }
            }
            #endregion - [ChartScaleList] -
            #endregion - [Column] -

            #region - [WatchItems] -
            workset.WatchItems = new WatchItem_t[m_WatchItems.Length];
            Array.Copy(m_WatchItems, workset.WatchItems, m_WatchItems.Length);
            #endregion - [WatchItems] -

            #region - [WatchElementList] -
            workset.WatchElementList = new List<short>();
   
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
            #endregion - [WatchElementList] -

            #region - [Count] -
            workset.Count = workset.WatchElementList.Count;

            if (workset.Count != m_ListItemCount)
            {
                throw new ArgumentException(Resources.EMWorksetIntegrityCheckFailed, "FormWorksetDefineChartRecorder.ConvertToWorkset() - [workset.WatchElements.Count]");
            }
            #endregion - [Count] -

            return workset;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Get the reference to the <c>ListBox</c> control that contains the lower limits of the Y axis for each chart recorder channel.
        /// </summary>
        public ListBox ListBoxChartScaleLowerLimit
        {
            get { return m_ListBoxChartScaleLowerLimit; }
        }

        /// <summary>
        /// Get the reference to the <c>ListBox</c> control that contains the upper limits of the Y axis for each chart recorder channel.
        /// </summary>
        public ListBox ListBoxChartScaleUpperLimit
        {
            get { return m_ListBoxChartScaleUpperLimit; }
        }
        #endregion --- Properties ---
    }
}