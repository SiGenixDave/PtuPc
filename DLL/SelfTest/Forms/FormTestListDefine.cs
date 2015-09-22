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
 *  File name:  FormTestListDefine.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/03/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  07/25/11    1.1     K.McD           1.  Position the help window within the bounds of the GroupBox controls.
 *  
 *  07/26/11    1.2     K.McD           1.  Use the m_PanelHelpWindowTestList and m_PanelHelpWindowAvailable panels to display the Windows help window.
 *  
 *  09/30/11    1.3     K.McD           1.  Modified the Remove() method to use the RemoveAt() method of the ListBox class rather than the Remove() method. This is 
 *                                          neccessary when the ListBox may contain multiple copies of the same item as the RemoveAt will target the specified index 
 *                                          whereas the Remove will target the first occurrence of the item, which may not neccessarily be the selected item.
 *  
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Common;
using Common.Configuration;
using Common.Forms;
using SelfTest.Properties;

namespace SelfTest.Forms
{
    /// <summary>
    /// A structure to store the fields associated with an individual test item. Used when adding self tests to controls such as <c>ListBox</c> controls etc.
    /// </summary>
    [Serializable]
    public struct TestItem_t
    {
        #region --- Constants ---
        /// <summary>
        /// The format provider used to display the self test number associated with the current self test. Value: "###".
        /// </summary>
        private const string FormatProviderSelftTestNumber = "###";
        #endregion --- Constants ---

        /// <summary>
        /// The self test identifier. Please note, a test list identifier of short.MaxValue is reserved for the user defined test list.
        /// </summary>
        public short SelfTestIdentifier;

        /// <summary>
        /// The self test number.
        /// </summary>
        public short SelfTestNumber;

        /// <summary>
        /// A flag to indicate whether the self test has been added to the test list. True, indicates that the the self test has been added to the 
        /// test list; otherwise, false.
        /// </summary>
        public bool Added;

        /// <summary>
        /// A flag to indicate whether the self test exists in the current data dictionary. True, indicates that the self exists; otherwise, false.
        /// </summary>
        public bool Exists;

        /// <summary>
        /// Overrides the ToString() method to return the name/description associated with the self test.
        /// </summary>
        /// <returns>The name/description of the self test.</returns>
        public override string ToString()
        {
            try
            {
                SelfTestRecord selfTestRecord = Lookup.SelfTestTable.Items[SelfTestIdentifier];
                if (selfTestRecord == null)
                {
                    return CommonConstants.TestNotDefinedString;
                }
                else
                {
                    return selfTestRecord.SelfTestNumber.ToString(FormatProviderSelftTestNumber) + CommonConstants.BindingMessage + selfTestRecord.Description;
                }
            }
            catch (Exception)
            {
                return CommonConstants.TestNotDefinedString;
            }
        }
    }

    /// <summary>
    /// Form to allow the user to configure which self tests are to be executed.
    /// </summary>
    public partial class FormTestListDefine : FormWorksetDefine
    {
        #region --- Constants ---
        /// <summary>
        /// The key that is to be used to select the <c>ListBox</c> controls that display the tests associated with each test category. Value: "m_ListBoxAvailable".
        /// </summary>
        private const string KeyListBoxAvailable = "m_ListBoxAvailable";

        /// <summary>
        /// The key that is to be used to select the <c>Label</c> controls that display the number of tests associated with each test category. Value: "m_LabelAvailableCount".
        /// </summary>
        private const string KeyLabelAvailableCount = "m_LabelAvailableCount";
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// An array of <c>TestItem_t</c> structures for each test defined in the data dictionary, sorted by self test identifer. Used to keep track of which 
        /// tests have been added to the list of selected tests.
        /// </summary>
        private TestItem_t[] m_TestItems;

        /// <summary>
        /// Reference to the selected test list record.
        /// </summary>
        private TestListRecord m_TestListRecord;

        /// <summary>
        /// Reference to the user defined test list record.
        /// </summary>
        private TestListRecord m_UserDefinedTestListRecord;

        /// <summary>
        /// Reference to the form that called this form.
        /// </summary>
        private FormViewTestResults m_CalledFromAsFormViewTestResults;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize an new instance of the form. Zero parameter constructor. 
        /// </summary>
        public FormTestListDefine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize an new instance of the form.
        /// </summary>
        /// <param name="testListRecord">Reference to the currently selected test list record.</param>
        public FormTestListDefine(TestListRecord testListRecord)
        {
            InitializeComponent();

            // Move the position of the OK and Cancel buttons as the Apply button is not required.
            m_ButtonOK.Location = m_ButtonCancel.Location;
            m_ButtonCancel.Location = m_ButtonApply.Location;

            // Only one column is required for this form so delete the tab pages associated with columns 2 and 3.
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn2);
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn3);
            m_TextBoxName.Visible = false;

            // Register the event handler for the DataUpdate event. This is raised whenever the current list of tests is modified.
            DataUpdate += new EventHandler(FormWorksetDefine_DataUpdate);

            m_TestListRecord = testListRecord;

            // Check whether the specified test list record was the user defined test list record and, if so, update the user defined test list record.
            if (m_TestListRecord.Identifier == short.MaxValue)
            {
                m_UserDefinedTestListRecord = ConstructEmptyUserDefinedTestListRecord();
                m_UserDefinedTestListRecord.SelfTestRecordList = m_TestListRecord.SelfTestRecordList;
            }

            ComboBoxAddTestLists(m_ComboBoxTestList);

            // Display the name of the selected test list record in the TabPage header.
            m_TabPageColumn1.Text = m_TestListRecord.Description;

            // Display the name of the current test list record on the ComboBox control.
            // Ensure that the SelectionChanged event is not triggered as a result of specifying the Text property of the ComboBox control.
            m_ComboBoxTestList.SelectedIndexChanged -= new EventHandler(m_ComboBoxTestList_SelectedIndexChanged);
            m_ComboBoxTestList.Text = m_TestListRecord.Description;
            m_ComboBoxTestList.SelectedIndexChanged += new EventHandler(m_ComboBoxTestList_SelectedIndexChanged);

            m_TestItems = ConstructTestItemArray();

            UpdateListBoxAvailable();
            TabPage selectedTabPage = m_TabControlAvailable.TabPages[0];
            m_ListBoxAvailable = (ListBox)selectedTabPage.Controls[KeyListBoxAvailable + selectedTabPage.Tag.ToString()];
            m_ListBoxSelected = m_ListBox1;

            LoadTestList(m_TestListRecord);
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
                // Close the help window if one is open.
                WinHlp32.HideHelpWindow(Handle.ToInt32());

                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    // Detach the event handlers.
                    TabPage tabPage;
                    ListBox listBox;
                    for (int tabPageIndex = 0; tabPageIndex < m_TabControlAvailable.TabPages.Count; tabPageIndex++)
                    {
                        tabPage =  m_TabControlAvailable.TabPages[tabPageIndex];
                        listBox = (ListBox)tabPage.Controls[KeyListBoxAvailable + tabPage.Tag.ToString()];
                        listBox.SelectedIndexChanged -= new EventHandler(this.ListBoxAvailable_SelectedIndexChanged);
                        listBox.DoubleClick -= new System.EventHandler(this.m_ButtonAdd_Click);
                        listBox.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseDown);
                        listBox.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseMove);
                        listBox.MouseUp -= new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseUp);
                        listBox.Items.Clear();
                    }
                    m_TabControlAvailable.TabPages.Clear();

                    // De-register the event handler for the DataUpdate event.
                    DataUpdate -= new EventHandler(FormWorksetDefine_DataUpdate);
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                m_TestItems = null;
                m_TestListRecord = null;
                m_UserDefinedTestListRecord = null;

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
        #region - [DragDrop] -
        /// <summary>
        /// Event handler for the <c>DragDrop</c> event associated with the target <c>ListBox</c>. The drag-drop operation is complete, move the selected items.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ListBoxTarget_DragDrop(object sender, DragEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the user is not allowed to modify the workset.
            if (ModifyEnabled == false)
            {
                return;
            }

            ListBox.SelectedObjectCollection selectedObjectCollection = (ListBox.SelectedObjectCollection)e.Data.GetData(typeof(ListBox.SelectedObjectCollection));
            int count = selectedObjectCollection.Count;

            if ((m_ListItemCount + count) > Parameter.SizeTestList)
            {
                MessageBox.Show(Resources.MBTSelfTestsMaxExceeded, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ---------------------------------------------------------------------------------------
            // For each selected item: (a) add this to the destination list and update the added flag.
            // ---------------------------------------------------------------------------------------
            int selfTestIdentifier;
            for (int index = 0; index < count; ++index)
            {
                // Add the item to the destination list.
                m_ListBoxSelected.Items.Add(selectedObjectCollection[index]);

                // Keep track of the number of tests that have been added to the test list.
                m_ListItemCount++;

                selfTestIdentifier = ((TestItem_t)selectedObjectCollection[index]).SelfTestIdentifier;

                // Keep track of which watch variables have been removed.
                m_TestItems[selfTestIdentifier].Added = true;

                AddSupplementalFields(selfTestIdentifier);
            }

            UpdateCount();

            // Scroll to the end of the list.
            m_ListBoxSelected.SetSelected(m_ListBoxSelected.Items.Count - 1, true);
            m_ListBoxSelected.ClearSelected();

            OnDataUpdate(this, new EventArgs());
            m_ButtonApply.Enabled = true;
        }
        #endregion - [DragDrop] -

        #region - [Buttons] -
        /// <summary>
        /// Event handler associated with the 'Execute' button <c>Click</c> event. Execute the selected self tests.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ButtonOK_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            m_CalledFromAsFormViewTestResults = CalledFrom as FormViewTestResults;
            Debug.Assert(m_CalledFromAsFormViewTestResults != null, "FormTestListDefine.m_ButtonOK_Click() - [m_CalledFromAsFormViewTestResults != null]");

            // Update the SelfTestRecord property of the calling form with the currently defined test list record.
            m_CalledFromAsFormViewTestResults.TestListRecord = m_TestListRecord;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler associated with the 'Cance' button <c>Click</c> event. Exit self test mode and close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            base.m_ButtonCancel_Click(sender, e);
        }
        #endregion - [Buttons] -

        #region - [TabControl] -
        /// <summary>
        /// Event handler for the 'Available' <c>TabControl</c> <c>SelectedIndexChanged</c> event. Activate the <c>ListBox</c> control associated with the 
        /// selected tab page. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TabControlAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            TabPage tabPage = m_TabControlAvailable.TabPages[m_TabControlAvailable.SelectedIndex];
            m_ListBoxAvailable = (ListBox)tabPage.Controls[KeyListBoxAvailable + tabPage.Tag.ToString()];
            WinHlp32.HideHelpWindow(Handle.ToInt32());
        }
        #endregion - [TabControl] -

        #region - [Context Menu] -
        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Show Definition' context menu option attached to the <c>ListBox</c> containing the self tests  
        /// that have been added to the test list.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_MenuItemShowDefinitionColumn_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_ListBoxSelected.SelectedItems.Count == 1)
            {
                int selfTestIdentifier = ((TestItem_t)m_ListBoxSelected.SelectedItem).SelfTestIdentifier;

                // Get the help index associated with the selected test.
                SelfTestRecord selfTestRecord;
                try
                {
                    selfTestRecord = Lookup.SelfTestTable.Items[selfTestIdentifier];
                    if (selfTestRecord != null)
                    {
                        int helpIndex = selfTestRecord.HelpIndex;

                        // If the help index exists, show the help topic associated with the index.
                        if (helpIndex != CommonConstants.NotFound)
                        {
                            WinHlp32.ShowHelpWindow(m_PanelHelpWindowTestList.Handle.ToInt32(), helpIndex, WinHlp32.HWND_TOP);
                        }
                    }
                }
                catch (Exception)
                {
                    // Ensure that an exception is not thrown.
                }
            }
            else if (m_ListBoxSelected.SelectedItems.Count > 1)
            {
                MessageBox.Show(Resources.MBTWorksetDefineMultipleSelection, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Show Definition' context menu option attached to the <c>ListBox</c> containing the available 
        /// self tests.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_MenuItemShowDefinitionAll_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_ListBoxAvailable.SelectedItems.Count == 1)
            {
                int selfTestIdentifier = ((TestItem_t)m_ListBoxAvailable.SelectedItem).SelfTestIdentifier;

                // Get the help index associated with the selected test.
                SelfTestRecord selfTestRecord;
                try
                {
                    selfTestRecord = Lookup.SelfTestTable.Items[selfTestIdentifier];
                    if (selfTestRecord != null)
                    {
                        int helpIndex = selfTestRecord.HelpIndex;

                        // If the help index exists, show the help topic associated with the index.
                        if (helpIndex != CommonConstants.NotFound)
                        {
                            WinHlp32.ShowHelpWindow(m_PanelHelpWindowAvailable.Handle.ToInt32(), helpIndex, WinHlp32.HWND_TOP);
                        }
                    }
                }
                catch (Exception)
                {
                    // Ensure that an exception is not thrown.
                }
            }
            else if (m_ListBoxAvailable.SelectedItems.Count > 1)
            {
                MessageBox.Show(Resources.MBTWorksetDefineMultipleSelection, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion - [Context Menu] -

        /// <summary>
        /// Event handler for the <c>SelectedIndexChanged</c> event associated with the <c>ComboBox</c> control used to select one of the pre-defined test lists.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ComboBoxTestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            TestListRecord testListRecord = (TestListRecord)m_ComboBoxTestList.SelectedItem;
            m_TestListRecord = testListRecord;

            // Display the name of the selected test list in the TabPage header.
            m_TabPageColumn1.Text = m_TestListRecord.Description;

            LoadTestList(m_TestListRecord);

            // If the help window is open then hide it.
            WinHlp32.HideHelpWindow(Handle.ToInt32());
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the <c>SelectedIndexChanged</c> event associated with the <c>ListBox</c> control that display the list of tests that 
        /// are to be executed. Close the help window.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinHlp32.HideHelpWindow(Handle.ToInt32());
        }

        /// <summary>
        /// Event handler for the <c>SelectedIndexChanged</c> event associated with the <c>ListBox</c> controls that display the list of available tests. 
        /// Close the help window.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void ListBoxAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            WinHlp32.HideHelpWindow(Handle.ToInt32());
        }

        #region - [Data Update] -
        /// <summary>
        /// Event handler for the <c>DataUpdate</c> event raised by the FormWorksetDefine class. This event is raised if the current test list is modified.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void FormWorksetDefine_DataUpdate(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Check whether the current test list is one of the pre-defined test lists.
            if (m_TestListRecord.Identifier != short.MaxValue)
            {
                // Yes,  change the Text property of the ComboBox control to reflect that the user has changed the tests associated with the current pre defined 
                // test list, however, leave the currently defined tests as they are.

                // Detach and then re-attach the 'SelectedIndexChanged' event handler to ensure that the event handler is not called when the Text property is modified.
                m_ComboBoxTestList.SelectedIndexChanged -= new System.EventHandler(m_ComboBoxTestList_SelectedIndexChanged);
                m_ComboBoxTestList.Text = m_UserDefinedTestListRecord.Description;
                m_ComboBoxTestList.SelectedIndexChanged += new System.EventHandler(m_ComboBoxTestList_SelectedIndexChanged);

                // Copy the fields of the user defined TestListRecord to the active test list record.
                m_TestListRecord = m_UserDefinedTestListRecord;

                // Display the name of the test list in the TabPage header.
                m_TabPageColumn1.Text = m_UserDefinedTestListRecord.Description;
            }
            
            // Keep the SelfTestRecordList properties of the TestListRecords up to date.
            m_UserDefinedTestListRecord.SelfTestRecordList = ConvertToSelfTestRecordList(m_ListBox1);
            m_TestListRecord.SelfTestRecordList = ConvertToSelfTestRecordList(m_ListBox1);

            WinHlp32.HideHelpWindow(Handle.ToInt32());
        }
        #endregion - [Data Update] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        #region - [FormWorksetDefine] -
        /// <summary>
        /// Add the selected items from the specified source <c>ListBox</c> control to the specified destination <c>ListBox</c> control.
        /// </summary>
        /// <param name="listSource">The source <c>ListBox</c> control.</param>
        /// <param name="listDestination">the destination <c>ListBox</c> control.</param>
        protected override void Add(ref ListBox listSource, ref ListBox listDestination)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            int count = listSource.SelectedItems.Count;
            int currentIndex = listSource.SelectedIndex;

            // Skip, if no items have been selected.
            if (count == 0)
            {
                return;
            }

            if ((m_ListItemCount + count) > Parameter.SizeTestList)
            {
                MessageBox.Show(Resources.MBTSelfTestsMaxExceeded, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor = Cursors.WaitCursor;

            // ---------------------------------------------------------------------------------------
            // For each selected item: (a) add this to the destination list and update the added flag.
            // ---------------------------------------------------------------------------------------
            int selfTestIdentifier;
            for (int index = 0; index < count; ++index)
            {
                // Add the item to the destination list.
                listDestination.Items.Add(listSource.SelectedItems[index]);

                // Keep track of the number of tests that have been added to the test list.
                m_ListItemCount++;

                selfTestIdentifier = ((TestItem_t)listSource.SelectedItems[index]).SelfTestIdentifier;

                // Keep track of which watch variables have been removed.
                m_TestItems[selfTestIdentifier].Added = true;

                AddSupplementalFields(selfTestIdentifier);
            }

            UpdateCount();

            // Highlight the next item for processing if the list is not empty.
            if (listSource.Items.Count > 0)
            {
                // Bounds checking.
                if (currentIndex < listSource.Items.Count)
                {
                    listSource.SelectedIndex = currentIndex;
                }
                else if (currentIndex == listSource.Items.Count)
                {
                    listSource.SelectedIndex = currentIndex - 1;
                }
            }

            // Scroll to the end of the list.
            listDestination.SetSelected(listDestination.Items.Count - 1, true);
            listDestination.ClearSelected();

            m_ButtonApply.Enabled = true;
            OnDataUpdate(this, new EventArgs());
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Remove the selected item(s) from the specified source <c>ListBox</c> control and add the item(s) to the specified destination <c>ListBox</c> control, in the
        /// correct watch identifier order.
        /// </summary>
        /// <remarks>This method should only be used when removing entries from the workset.</remarks>
        /// <param name="listSource">The source <c>ListBox</c> control.</param>
        /// <param name="listDestination">The destination <c>ListBox</c> control.</param>
        protected override void Remove(ref ListBox listSource, ref ListBox listDestination)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            int count = listSource.SelectedItems.Count;
            int currentIndex = listSource.SelectedIndex;

            // Skip, if no items have been selected.
            if (count == 0)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // ------------------------------------------------------------------------------------------------
            // For each selected item: (a) add the item to the destination list and (b) delete it from the source list.
            // ------------------------------------------------------------------------------------------------
            for (int index = 0; index < count; ++index)
            {
                try
                {
                    // Update the array of test items that have been added to the list. Note: the test item array will be incorrect if the user has added multiple 
                    // copies of the test. A better approach would be to check the number of occurrences of the test and then only clear the added flag if the 
                    // test only appears once. This is not an issue as, unlike the standard workset definitions, the number of available tests remains fixed.
                    m_TestItems[((TestItem_t)listSource.SelectedItems[index]).SelfTestIdentifier].Added = false;
                }
                catch (Exception)
                {
                    // Ensure that an exception isn't thrown.
                }

                // Keep track of the number of watch elements that have been added to the workset.
                m_ListItemCount--;
            }

            // Remember the selected item list contents will change every time an item is deleted. The best approach is to repeatedly remove the selected items collection 
            // for index = 0 by the number of items added to the destination list.
            int removeAtIndex;
            for (int index = 0; index < count; ++index)
            {
                // Get the index of the ListBox item that is currently being removed.
                removeAtIndex = listSource.SelectedIndices[0];
                listSource.Items.RemoveAt(removeAtIndex);

                RemoveSupplementalFields(removeAtIndex);
            }

            UpdateCount();

            // Highlight the next item for processing if the list is not empty.
            if (listSource.Items.Count > 0)
            {
                // Bounds checking.
                if (currentIndex < listSource.Items.Count)
                {
                    listSource.SelectedIndex = currentIndex;
                }
                else if (currentIndex == listSource.Items.Count)
                {
                    listSource.SelectedIndex = currentIndex - 1;
                }
            }

            m_ButtonApply.Enabled = true;
            OnDataUpdate(this, new EventArgs());
            Cursor = Cursors.Default;
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

            m_LabelCount1.Text = Resources.LegendCount + CommonConstants.Colon + m_ListItemCount.ToString();

            // Update the counts associated with the group lists; in reality, these are fixed.
            TabPage tabPage;
            Label label;
            ListBox listBox;
            for (int tabPageIndex = 0; tabPageIndex < m_TabControlAvailable.TabPages.Count; tabPageIndex++)
            {
                tabPage = m_TabControlAvailable.TabPages[tabPageIndex];
                label = (Label)tabPage.Controls[KeyLabelAvailableCount + tabPage.Tag.ToString()];
                listBox = (ListBox)tabPage.Controls[KeyListBoxAvailable + tabPage.Tag.ToString()];
                label.Text = Resources.LegendCount + CommonConstants.Colon + listBox.Items.Count.ToString();
            }
        }
        #endregion - [FormWorksetDefine] -

        /// <summary>
        /// Add the self tests defined in the specified test list the specified <c>ListBox</c> control.
        /// </summary>
        /// <param name="listBox">The <c>ListBox</c> to which the items are to be added.</param>
        /// <param name="testListRecord">The test list that is to be added to the <c>ListBox</c> control.</param>
        private void TestItemAddRange(ListBox listBox, TestListRecord testListRecord)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            listBox.Items.Clear();
            listBox.SuspendLayout();

            TestItem_t testItem;
            short selfTestIdentifier, selfTestNumber;
            for (int index = 0; index < testListRecord.SelfTestRecordList.Count; index++)
            {
                testItem = new TestItem_t();
                selfTestIdentifier = testListRecord.SelfTestRecordList[index].Identifier;
                selfTestNumber = testListRecord.SelfTestRecordList[index].SelfTestNumber;
                testItem.SelfTestIdentifier = selfTestIdentifier;
                testItem.SelfTestNumber = selfTestNumber;
                testItem.Added = true;
                listBox.Items.Add(testItem);
            }

            listBox.PerformLayout();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Update the <c>ListBox</c> controls associated with each test category/group with the self tests that have been defined in the data dictionary. The self 
        /// tests associated with each test category/group are defined in the <c>GROUPLIST</c> and <c>GROUPLISTIDS</c> tables of the data dictionary. The member variable 
        /// <c>m_TestItems</c> must be initialized before calling this method.
        /// </summary>
        private void UpdateListBoxAvailable()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            ConfigureTabControl(m_TabControlAvailable);

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Configure the specified <c>TabControl</c> so that it can be used to select between the different self test groups that are defined in the 
        /// data dictionary. (1) Create a tab page for every self test group that has been defined in the <c>GROUPLIST</c> table of the data dictionary (2) add a 
        /// <c>ListBox</c> control  to each <c>TabPage</c>  and (3) populate the <c>ListBox</c> controls with the self tests associated with each self 
        /// test group.
        /// </summary>
        /// <param name="tabControl">The <c>TabControl</c> that is to be configured.</param>
        private void ConfigureTabControl(TabControl tabControl)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_TabControlAvailable.TabPages.Clear();

            // Create a tab page for each self test group defined in the 'GROUPLIST' table of the data dictionary.
            TabPage tabPage;
            for (int groupListIndex = 0; groupListIndex < Lookup.GroupListTable.RecordList.Count; groupListIndex++)
            {
                // Only add a tab page to the list if the group list is valid.
                if (Lookup.GroupListTable.RecordList[groupListIndex] != null)
                {
                    tabPage = ConstructTabPage(Lookup.GroupListTable.RecordList[groupListIndex]);
                    tabControl.TabPages.Add(tabPage);
                }
            }
        }

        /// <summary>
        /// Instantiate and initialize a new <c>TabPage</c> control to display the self tests associated with a particular self test group.
        /// </summary>
        /// <remarks>The member variable <c>m_TestItems</c> must be initialized before calling this method.</remarks>
        /// <param name="groupListRecord">Reference to the group list associated with the TabPage.</param>
        /// <returns>The initialized <c>TabPage</c> control.</returns>
        private TabPage ConstructTabPage(GroupListRecord groupListRecord)
        {
            TabPage tabPage = new TabPage(groupListRecord.Description);

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return tabPage;
            }

            // Configure the ListBox control.
            ListBox listBox = new ListBox();
            listBox.Location = new System.Drawing.Point(8, 24);
            listBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            listBox.Name = KeyListBoxAvailable + groupListRecord.Identifier.ToString();
            listBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            listBox.Size = new System.Drawing.Size(282, 264);
            listBox.TabIndex = 1;
            listBox.SelectedIndexChanged += new EventHandler(ListBoxAvailable_SelectedIndexChanged);
            listBox.DoubleClick += new System.EventHandler(this.m_ButtonAdd_Click);
            listBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseDown);
            listBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseMove);
            listBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseUp);
            listBox.ContextMenuStrip = m_ContextMenuAll;

            Label legendAvailableTests = new Label();
            legendAvailableTests.AutoSize = true;
            legendAvailableTests.Text = Resources.LegendAvailableTests;
            legendAvailableTests.Location = new Point(5, 8);

            Label labelCount = new Label();
            labelCount.Name = KeyLabelAvailableCount + groupListRecord.Identifier.ToString();
            labelCount.AutoSize = true;
            labelCount.Location = new Point(5, 291);

            // ---------------------------------------------------------------
            // Add the self tests associated with the current self test group.
            // ---------------------------------------------------------------
            // The list of test items that are to be added to the ListBox control. The type parameter must be an object type rather than a TestItem_t type so that
            // the ToArray() method can be used to generate an object[] which can then be added to existing the ListBox.Items using the AddRange() method.
            List<object> testItemList = new List<object>();
            TestItem_t testItem;
            SelfTestRecord selfTestRecord;
            for (int index = 0; index < groupListRecord.SelfTestRecordList.Count; index++)
            {
                selfTestRecord = groupListRecord.SelfTestRecordList[index];

                // Check that the self test exists.
                try
                {
                    testItem = m_TestItems[selfTestRecord.Identifier];
                }
                catch (Exception)
                {
                    continue;
                }

                if ((testItem.Exists == true) &&
                    (testItem.Added == false))
                {
                    testItemList.Add(testItem);
                }
            }

            listBox.SuspendLayout();
            listBox.Items.Clear();
            listBox.Items.AddRange(testItemList.ToArray());
            listBox.PerformLayout();

            // Configure the TabPage.
            tabPage.Tag = groupListRecord.Identifier;
            tabPage.AutoScroll = true;
            tabPage.BackColor = Color.FromKnownColor(KnownColor.Window);
            tabPage.Controls.Add(legendAvailableTests);
            tabPage.Controls.Add(listBox);
            tabPage.Controls.Add(labelCount);

            return tabPage;
        }

        /// <summary>
        /// Instantiate and initialize a new array of <c>TestItem_t</c> structures based upon the tests that are defined in the <c>SELFTEST</c> table of the data 
        /// dictionary.
        /// </summary>
        /// <returns>An array of <c>TestItem_t</c> structures based upon the self tests defined in the <c>SELFTEST</c> table of the data dictionary.</returns>
        private TestItem_t[] ConstructTestItemArray()
        {
            // Instantiate and initialize a new array of TestItem_t structures.
            TestItem_t[] testItems = new TestItem_t[Lookup.SelfTestTable.RecordList.Count];

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return testItems;
            }

            TestItem_t testItem;
            SelfTestRecord selfTestRecord;
            for (short selfTestIdentifier = 0; selfTestIdentifier < Lookup.SelfTestTable.RecordList.Count; selfTestIdentifier++)
            {
                testItem = new TestItem_t();
                testItem.SelfTestIdentifier = selfTestIdentifier;
                testItem.Added = false;

                try
                {
                    selfTestRecord = Lookup.SelfTestTable.Items[selfTestIdentifier];

                    if (selfTestRecord == null)
                    {
                        testItem.Exists = false;
                    }
                    else
                    {
                        testItem.Exists = true;
                        testItem.SelfTestNumber = selfTestRecord.SelfTestNumber;
                    }
                }
                catch (Exception)
                {
                    testItem.Exists = false;
                }

                testItems[selfTestIdentifier] = testItem;
            }

            return (testItems);
        }

        /// <summary>
        /// Construct an empty, user defined test list record.
        /// </summary>
        /// <returns>An empty user defined test list record.</returns>
        private TestListRecord ConstructEmptyUserDefinedTestListRecord()
        {
            TestListRecord testListRecord;
            testListRecord = new TestListRecord();
            testListRecord.Identifier = short.MaxValue;
            testListRecord.HelpIndex = CommonConstants.NotDefined;
            testListRecord.Description = Resources.TextUserDefined;
            testListRecord.Attribute = 0;
            testListRecord.SelfTestRecordList = new List<SelfTestRecord>();
            return (testListRecord);
        }

        /// <summary>
        /// Add the pre-defined test lists defined in the <c>TESTLISTS</c> table of the data dictionary to the specified <c>ComboBox</c> control.
        /// </summary>
        /// <param name="comboBox">The <c>ComboBox</c> control that it to be processed.</param>
        private void ComboBoxAddTestLists(ComboBox comboBox)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Debug.Assert(comboBox != null, "FormTestListDefine.ComboBoxAddTestLists() - [comboBox != null]");

            comboBox.Items.Clear();

            // Get the pre-configured test lists.
            TestListRecord testListRecord;
            for (int testListIndex = 0; testListIndex < Lookup.TestListTable.RecordList.Count; testListIndex++)
            {
                if (Lookup.TestListTable.RecordList[testListIndex] != null)
                {
                    testListRecord = Lookup.TestListTable.RecordList[testListIndex];
                    comboBox.Items.Add(testListRecord);
                }
            }

            // If the user defined test list record hasn't been defined, create an empty, user defined test list record.
            if (m_UserDefinedTestListRecord == null)
            {
                m_UserDefinedTestListRecord = ConstructEmptyUserDefinedTestListRecord();
            }

            // Add the user defined test list record.
            comboBox.Items.Add(m_UserDefinedTestListRecord);
        }

        /// <summary>
        /// Convert the test items associated with the specified <c>ListBox</c> control to a list of self test records.
        /// </summary>
        /// <param name="listBox">The <c>ListBox</c> control containing the tests.</param>
        /// <returns>A generic list of self test records corresponding to the test items contained in the specified <c>ListBox</c> control.</returns>
        private List<SelfTestRecord> ConvertToSelfTestRecordList(ListBox listBox)
        {
            List<SelfTestRecord> selfTestRecordList = new List<SelfTestRecord>();

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return selfTestRecordList;
            }

            // ----------------------------------------------------------------
            // Convert the currently selected tests to a SelfTestRecord object.
            // ----------------------------------------------------------------
            TestItem_t testItem;
            short selfTestNumber;
            SelfTestRecord selfTestRecord;
            for (int index = 0; index < m_ListBox1.Items.Count; index++)
            {
                testItem = (TestItem_t)m_ListBox1.Items[index];
                selfTestNumber = testItem.SelfTestNumber;
                try
                {
                    selfTestRecord = Lookup.SelfTestTableBySelfTestNumber.Items[selfTestNumber];
                    if (selfTestRecord == null)
                    {
                        throw new Exception();
                    }
                    selfTestRecordList.Add(selfTestRecord);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return selfTestRecordList;
        }

        /// <summary>
        /// Load the specified test list into the <c>ListBox</c> control used to display the selected self tests.
        /// </summary>
        /// <param name="testListRecord">The test list that is to be loaded.</param>
        private void LoadTestList(TestListRecord testListRecord)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_ListItemCount = testListRecord.SelfTestRecordList.Count;

            m_TestItems = ConstructTestItemArray();
            SelfTestRecord selfTestRecord;
            for (int testIndex = 0; testIndex < m_ListItemCount; testIndex++)
            {
                selfTestRecord = testListRecord.SelfTestRecordList[testIndex];
                m_TestItems[selfTestRecord.Identifier].Added = true;
            }

            TestItemAddRange(m_ListBox1, testListRecord);
            UpdateCount();
        }
        #endregion --- Methods ---
    }
}
