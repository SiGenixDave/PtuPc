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
 *  File name:  FormWorksetDefineWatch.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/16/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/26/10    1.1     K.McD           1.  Created the ConvertToWorkset() method.
 *                                      2.  Used the Array.Copy() method to copy the watch item array.
 * 
 *  03/17/11    1.2     K.McD           1.  Auto-modified as a result of a property name change associated with the Common.Security class.
 * 
 *  03/28/11    1.3     K.McD           1.  Bug fix SNCR001.112. Modified to use the old identifier field of the data dictionary, rather than the watch identifier 
 *                                          field, to define the watch variables that are to be included in the workset as these remain consistent following a 
 *                                          data dictionary update.
 * 
 *                                      2.  Auto-modified as a result of a number of name changes to the properties and methods of external classes.
 *                                      3.  Removed the Save() method as the class now uses the Save() method of the parent class.
 * 
 *  04/27/11    1.4     K.McD           1.  Auto-modified as a result of a name change to an inherited member variable.
 *  
 *  05/23/11    1.5     K.McD           1.  Modified to accommodate the signature change to the FormWorksetDefine.WatchItemsAddRange() method.
 *                                      2.  Applied the 'Organize Usings/Remove and Sort' context menu option.
 *                                      
 *  06/21/11    1.6     K.McD           1.  Auto-modified as a result of a name change to an inherited member variable.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Common;
using Common.Configuration;
using Common.Forms;
using Watch.Properties;

namespace Watch.Forms
{
    /// <summary>
    /// Form to allow the user to define: (a) the watch variables that are associated with a workset used to view and record watch variables and (b) the order and 
    /// column in which they are to be displayed.
    /// </summary>
    public partial class FormWorksetDefineWatch : FormWorksetDefine
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes an new instance of the form. Zero parameter constructor, required by Visual Studio.
        /// </summary>
        public FormWorksetDefineWatch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes an new instance of the form. This constructor is used when a new workset is being created. Populates the 'Available' 
        /// <c>ListBox</c> controls with the appropriate watch variables.
        /// </summary>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        public FormWorksetDefineWatch(WorksetCollection worksetCollection)
            : base(worksetCollection)
        {
            InitializeComponent();

            // Initialize the column header text boxes, use the default values.
            m_TextBoxHeader1.Text = m_WorksetCollection.Worksets[0].Column[0].HeaderText;
            m_TextBoxHeader2.Text = m_WorksetCollection.Worksets[0].Column[1].HeaderText;
            m_TextBoxHeader3.Text = m_WorksetCollection.Worksets[0].Column[2].HeaderText;
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
        public FormWorksetDefineWatch(WorksetCollection worksetCollection, Workset_t workset, bool applyVisible)
            : base(worksetCollection, workset, applyVisible)
        {
            InitializeComponent();

            // Initialize the column header text boxes, use the default values.
            m_TextBoxHeader1.Text = workset.Column[0].HeaderText;
            m_TextBoxHeader2.Text = workset.Column[1].HeaderText;
            m_TextBoxHeader3.Text = workset.Column[2].HeaderText;

            // ------------------------------------
            // Update the 'Column' ListBox controls.
            // -------------------------------------
            WatchItemAddRange(m_ListBox1, workset.Column[0]);
            WatchItemAddRange(m_ListBox2, workset.Column[1]);
            WatchItemAddRange(m_ListBox3, workset.Column[2]);

            UpdateCount();
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
        #endregion --- Delegated Methods ---

        #region - [Methods] -
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

            // Watch variable count associated with each column of the workset.
            int countColumn1 = m_ListBox1.Items.Count;
            int countColumn2 = m_ListBox2.Items.Count;
            int countColumn3 = m_ListBox3.Items.Count;
            m_LabelCount1.Text = Resources.LegendCount + CommonConstants.Colon + countColumn1.ToString();
            m_LabelCount2.Text = Resources.LegendCount + CommonConstants.Colon + countColumn2.ToString();
            m_LabelCount3.Text = Resources.LegendCount + CommonConstants.Colon + countColumn3.ToString();

            // Total number of watch variables in the workset.
            m_LabelCountTotal.Text = Resources.LegendTotalCount + CommonConstants.Colon + m_ListItemCount.ToString() +
                                     CommonConstants.Space + Resources.LegendOf + CommonConstants.Space + m_WorksetCollection.EntryCountMax.ToString();
        }

        /// <summary>
        /// Convert the current user setting to a workset.
        /// </summary>
        /// <param name="worksetName">The name of the workset.</param>
        /// <returns>The user settings converted to a workset.</returns>
        private Workset_t ConvertToWorkset(string worksetName)
        {
            // --------------------------------------------------------------------------
            // Copy the definitions to a new workset and update the WorksetManager class.
            // --------------------------------------------------------------------------
            Workset_t workset = new Workset_t();

            workset.Name = worksetName;
            workset.SampleMultiple = Workset_t.DefaultSampleMultiple;
            workset.CountMax = m_WorksetCollection.EntryCountMax;
            workset.SecurityLevel = Security.SecurityLevelCurrent;

            // Copy the old identifiers defined in each of the columns.
            workset.Column = new Column_t[m_WorksetCollection.ColumnCountMax];
            workset.Column[0].HeaderText = m_TextBoxHeader1.Text;
            workset.Column[1].HeaderText = m_TextBoxHeader2.Text;
            workset.Column[2].HeaderText = m_TextBoxHeader3.Text;

            // Copy the old identifiers stored in each colum of the workset.
            for (int index = 0; index < m_WorksetCollection.ColumnCountMax; index++)
            {
                workset.Column[index].OldIdentifierList = new List<short>();
            }

            for (int index = 0; index < m_ListBox1.Items.Count; index++)
            {
                workset.Column[0].OldIdentifierList.Add(((WatchItem_t)m_ListBox1.Items[index]).OldIdentifier);
            }

            for (int index = 0; index < m_ListBox2.Items.Count; index++)
            {
                workset.Column[1].OldIdentifierList.Add(((WatchItem_t)m_ListBox2.Items[index]).OldIdentifier);
            }

            for (int index = 0; index < m_ListBox3.Items.Count; index++)
            {
                workset.Column[2].OldIdentifierList.Add(((WatchItem_t)m_ListBox3.Items[index]).OldIdentifier);
            }

            // Copy the watchItems list.
            workset.WatchItems = new WatchItem_t[m_WatchItems.Length];
            Array.Copy(m_WatchItems, workset.WatchItems, m_WatchItems.Length);

            // Create the WatchElementList property from the column entries.
            workset.WatchElementList = new List<short>();
            short oldIdentifier;
            WatchVariable watchVariable;
            for (int columnIndex = 0; columnIndex < workset.Column.Length; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < workset.Column[columnIndex].OldIdentifierList.Count; rowIndex++)
                {
                    oldIdentifier = workset.Column[columnIndex].OldIdentifierList[rowIndex];
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
            }

            workset.WatchElementList.Sort();
            workset.Count = workset.WatchElementList.Count;

            if (workset.Count != m_ListItemCount)
            {
                throw new ArgumentException(Resources.EMWorksetIntegrityCheckFailed, "FormWorksetDefineFaultLog.ConvertToWorkset() - [workset.WatchElements.Count]");
            }

            return workset;
        }
        #endregion - [Methods] -
    }
}