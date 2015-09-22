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
 *  File name:  FormPlotDefine.cs
 * 
 *  Revision History
 *  ----------------
 *
 *  Date        Version Author          Comments
 *  08/31/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 * 
 *  10/11/11    1.0.1	Sean.D			1.  Added a call to UpdateCount() in m_ButtonRestoreDefaults_Click to ensure the counts match.
 *  
 *  04/01/15    1.1     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  MOC-0171-28. A check will be included as part of the ‘Save’ procedure to ensure that an empty workset cannot be saved.
 *
 *                                      Modifications
 *                                      1.  Modified the Add() and Remove() methods to call the EnableApplyAndOKButtons() method instead of just
 *                                          setting the 'Enabled' property of the 'Apply' Button to true. This ensures that a full check is made on the state of the
 *                                          workset whenever the Button controls are pressed.
 *                                      2.  De-registered and then re-registered the TextChanged event handler to ensure that the TextChanged event was not triggered
 *                                          in the constructor.
 *                                      3.  Modified the constructor to call the DisableApplyAndOKButtons(() method instead of just disabling the Apply button.
 *                                      4.  Modified the m_ButtonRestoreDefaults_Click() event handler to call the EnableApplyAndOKButtons()/DisableApplyAndOKButtons()
 *                                          methods instead of just enabling/disabling the Apply button.
 *                                      6.  Modified the  m_MenuItemConfigureBitmaskPlot_Click() event handler and the Add() and Remove() methods to call the
 *                                          EnableApplyAndOKButtons() method instead of just enabling the Apply button.
 *                                      7.  Added the EnableApplyAndOKButtons() override to simply enable the Apply and OK buttons without checking on the
 *                                          status of the workset i.e. is the name valid, is it currently in use and is there one or more watch variables.
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
 *                                      2.  SNCR - R188 PTU [20-Mar-2015] Item 18. If the user uses the dragdrop feature of FormPlotDefine to modify the plot layout, 
 *                                          the OK and Apply buttons are not enabled.
 *                                      
 *                                      Modifications
 *                                      1.  Modified the m_ListBoxTarget_DragDrop() method to call the EnableApplyAndOKButtons() method in order to ensure that
 *                                          the OK and Apply buttons are enabled if the user uses the dragdrop method to transfer watch variables from the 'Available'
 *                                          ListBox to to the 'WorkSet Layout' ListBox. Ref.: 2.
 *                                          
 *                                      2.  Added the Shown event handler to check whether the LogType is LogType.DataStream and, if so, to configure the workset layout
 *                                          ListBoxes etc to conform to the specification given in reference 1.1.1 above for 'Create/Edit Workset' dialog boxes.
 *                                          Ref.: 1.1.1.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

using Common.Configuration;
using Common.Properties;

namespace Common.Forms
{
    /// <summary>
    /// A form class to allow the user to define how saved watch files and fault logs are to be plotted.
    /// </summary>
    public partial class FormPlotDefine : FormWorksetDefine
    {
        #region --- Member Variables ---
        /// <summary>
        /// The workset associated with the saved watch data.
        /// </summary>
        private Workset_t m_Workset;

        /// <summary>
        /// An array of <c>TextBox</c> controls representing the <c>TextBox</c> controls used to input the header information associated with each column of the workset.
        /// </summary>
        private TextBox[] m_TextBoxHeaders;

        /// <summary>
        /// An array of <c>ListBox</c> controls representing the <c>ListBox</c> controls used to define the watch variables associated with each <c>PlotTabPage</c>of the 
        /// workset.
        /// </summary>
        private ListBox[] m_ListBoxes;

        /// <summary>
        /// Flag to control whether the default plot layout is to be restored. True, if the default plot values are to be restored; otherwise, false.
        /// </summary>
        private bool m_RestoreDefault;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the form. Zero parameter constructor.
        /// </summary>
        public FormPlotDefine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the form.
        /// </summary>
        /// <param name="workset">The workset associated with the saved data.</param>
        public FormPlotDefine(Workset_t workset)
        {
            InitializeComponent();

            m_Workset = workset;

            #region - [Reposition Controls to 'No Row Header' Position] -
            // By default, FormWorksetDefine now positions the ListBox controls associated with the 'Workset Layout' GroupBox in the include row header position.
            // As this form does not display row headers, the location and size of these controls must be adjusted to the no row header position.
            this.m_ListBox1.Location = new System.Drawing.Point(7, 61);
            this.m_ListBox1.Size = new System.Drawing.Size(274, 264);
            this.m_ListBox2.Location = new System.Drawing.Point(7, 61);
            this.m_ListBox2.Size = new System.Drawing.Size(274, 264);
            this.m_ListBox3.Location = new System.Drawing.Point(7, 61);
            this.m_ListBox3.Size = new System.Drawing.Size(274, 264);

            // Reposition the 'Count' Label.
            this.m_LabelCount1.Location = new System.Drawing.Point(7, 328);
            this.m_LabelCount2.Location = new System.Drawing.Point(7, 328);
            this.m_LabelCount3.Location = new System.Drawing.Point(7, 328);

            // Reposition the 'Watch Variable Description' Label.
            this.m_LabelListBox1ColumnHeader.Location = new System.Drawing.Point(8, 44);
            this.m_LabelListBox2ColumnHeader.Location = new System.Drawing.Point(8, 44);
            this.m_LabelListBox3ColumnHeader.Location = new System.Drawing.Point(8, 44);
            #endregion - [Reposition Controls to 'No Row Header' Position] -

            m_TextBoxHeaders = new TextBox[Workset.ColumnCountMaxRecordedWatch] {m_TextBoxHeader1, m_TextBoxHeader2, m_TextBoxHeader3} ;
            m_ListBoxes = new ListBox[Workset.ColumnCountMaxRecordedWatch] {m_ListBox1, m_ListBox2, m_ListBox3 };

            // Set the flag to indicate that the form has been called in order to edit a new workset.
            m_CreateMode = false;

            // Ensure that the TextChanged event is not triggered as a result of specifying the Text property of the TextBox control.
            m_TextBoxName.TextChanged -= new EventHandler(m_TextBoxName_TextChanged);
            m_TextBoxName.Text = m_Workset.Name;
            m_TextBoxName.TextChanged += new EventHandler(m_TextBoxName_TextChanged);

            m_TextBoxName.Enabled = false;

            // Update the security description to reflect the security level of the selected workset.
            m_TextBoxSecurityLevel.Text = Security.GetSecurityDescription(m_Workset.SecurityLevel);

            m_WatchItems = m_Workset.WatchItems; 

            UpdateListBoxAvailable(m_WatchItems);

            // Update the m_ListBoxSelected reference to point at the ListBox control associated with the default TabPage.
            m_ListBoxSelected = m_ListBox1;

            // Check whether the PlotTabPages array has been defined.
            if (m_Workset.PlotTabPages == null)
            {
                // No - Use the Column field of the workset to define the current configuration.
                for (int columnIndex = 0; columnIndex < m_Workset.Column.Length; columnIndex++)
                {
                    m_TextBoxHeaders[columnIndex].Text = m_Workset.Column[columnIndex].HeaderText;
                    WatchItemAddRange(m_ListBoxes[columnIndex], m_Workset.Column[columnIndex]);
                }

                m_ListItemCount = m_Workset.Count;

                // No need to enable the 'Restore Default ...' button as the default plot layout is currently active.
                m_ButtonRestoreDefault.Enabled = false;
            }
            else
            {
                // Yes - Use the PlotTabPages field of the workset to define the current configuration.
                m_ListItemCount = 0;
                for (int plotTabPageIndex = 0; plotTabPageIndex < m_Workset.PlotTabPages.Length; plotTabPageIndex++)
                {
                    m_TextBoxHeaders[plotTabPageIndex].Text = m_Workset.PlotTabPages[plotTabPageIndex].HeaderText;
                    WatchItemAddRange(m_ListBoxes[plotTabPageIndex], m_Workset.PlotTabPages[plotTabPageIndex]);
                    m_ListItemCount += m_ListBoxes[plotTabPageIndex].Items.Count;
                }

                // Allow the user to restore the default plot layout.
                m_ButtonRestoreDefault.Enabled = true;
            }

            UpdateCount();

            // OK/Cancel/Apply Buttons. The Apply button is not applicable to this form.
            m_ButtonOK.Location = m_ButtonCancel.Location;
            m_ButtonCancel.Location = m_ButtonApply.Location;
            m_ButtonApply.Visible = false;

            DisableApplyAndOKButtons(string.Empty);

            // Default the DialogResult value to be anything other than DialogResult.Cancel or DialogResult.Yes.
            DialogResult = DialogResult.No;

            // Enable the context menu that allows the user to configure the bitmask plots.
            m_MenuItemConfigureBitmaskPlot.Visible = true;

            // Register the event handler for the DataUpdate event. This is raised whenever the workset parameters are modified.
            DataUpdate += new EventHandler(FormPlotDefine_DataUpdate);
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

                    // De-register the event handler for the DataUpdate event.
                    DataUpdate -= new EventHandler(FormPlotDefine_DataUpdate);
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                m_TextBoxHeaders = null;
                m_ListBoxes = null;

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
        /// The event handler for the <c>Shown</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormPlotDefine_Shown(object sender, EventArgs e)
        {
            if (CalledFrom == null)
            {
                return;
            }

            WatchFile_t watchFile =  ((IWatchFile)CalledFrom).WatchFile;
            DataStreamTypeParameters_t datastreamTypeParameters = watchFile.DataStream.DataStreamTypeParameters;
            
            // Check whether the workset is a data stream rather than a recorded watch file or a simulated data stream and, if so, hide the TabPages,
            // Labels and TextBoxes that aren't required and update the TabPage header text.
            if (watchFile.DataStream.LogType.Equals(LogType.DataStream))
            {
                // Only one column is required fot this workset so delete the tab pages associated with columns 2 and 3.
                m_TabControlColumn.TabPages.Remove(m_TabPageColumn2);
                m_TabControlColumn.TabPages.Remove(m_TabPageColumn3);
                m_LabelCountTotal.Visible = false;
                m_LegendHeader1.Visible = false;
                m_TextBoxHeader1.Enabled = false;
                m_TextBoxHeader1.Visible = false;
                m_TabPageColumn1.Text = Resources.HeaderTextDataStream;
            }
        }

        /// <summary>
        /// Event handler for the <c>DataUpdate</c> event raised by the FormWorksetDefine class. This event is raised if the workset parameters are modified.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void FormPlotDefine_DataUpdate(object sender, EventArgs e)
        {
            m_RestoreDefault = false;

            // Allow the user to restore the default plot layout.
            m_ButtonRestoreDefault.Enabled = true;
        }

        #region - [Buttons] -
        /// <summary>
        /// Event handler for the Apply button <c>Click</c> event. Update the workset.
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

            IWatchFile iWatchFile = CalledFrom as IWatchFile;
            Debug.Assert(iWatchFile != null, "FormPlotDefine.m_ButtonApply_Click() - [iWatchFile != null]");
            
            Cursor = Cursors.WaitCursor;

            // Yes - Update the WatchFile property with the new workset.
            WatchFile_t watchFile = iWatchFile.WatchFile;
            if (m_RestoreDefault == true)
            {
                watchFile.DataStream.Workset.PlotTabPages = null;
            }
            else
            {
                Workset_t workset = ConvertToWorkset(m_Workset.Name);
                watchFile.DataStream.Workset = workset;
            }
            iWatchFile.WatchFile = watchFile;
            iWatchFile.SaveWatchFile();

            m_ButtonApply.Enabled = false;

            // Let the calling form know that the watchfile has been modified.
            DialogResult = DialogResult.Yes;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the Cancel button <c>Click</c> event. Close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            base.m_ButtonCancel_Click(sender, e);
        }

        /// <summary>
        /// Event handler for the 'Show All' button <c>Click</c> event. Set the PlotTabPages field of the workset to the default values i.e. base the layout of the plot 
        /// screen upon the contents of the Column field of the workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonRestoreDefaults_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            IWatchFile iWatchFile = CalledFrom as IWatchFile;
            Debug.Assert(iWatchFile != null, "FormPlotDefine.m_ButtonRestoreDefaults_Click() - [iWatchFile != null]");

            Cursor = Cursors.WaitCursor;
            m_RestoreDefault = true;
            m_ButtonRestoreDefault.Enabled = false;

            // Restore the default plot values.
            for (int columnIndex = 0; columnIndex < m_Workset.Column.Length; columnIndex++)
            {
                m_TextBoxHeaders[columnIndex].Text = m_Workset.Column[columnIndex].HeaderText;
                WatchItemAddRange(m_ListBoxes[columnIndex], m_Workset.Column[columnIndex]);
            }

            m_ListItemCount = m_Workset.Count;

            // Yes - Update the WatchFile property with the new workset.
            WatchFile_t watchFile = iWatchFile.WatchFile;
            if (watchFile.DataStream.Workset.PlotTabPages != null)
            {
                EnableApplyAndOKButtons();
            }
            else
            {
                DisableApplyAndOKButtons(string.Empty);
            }

			UpdateCount();

            Cursor = Cursors.Default;
        }
        #endregion - [Buttons] -

        #region - [Context Menu] -
        /// <summary>
        /// Event handler for the context menu <c>Opened</c> event. Check whether the selected watch variable is a bitmask and, if so, enable the 'Configure 
        /// Bitmask Plot' context menu; otherwise, disable it.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ContextMenuColumns_Opened(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Default to the 'Configure Bitmask Plot' context menu being disabled.
            m_MenuItemConfigureBitmaskPlot.Enabled = false;
            if (m_ListBoxSelected.SelectedItems.Count == 1)
            {
                int oldIdentifier = ((WatchItem_t)m_ListBoxSelected.SelectedItem).OldIdentifier;

                // Get the watch variable associated with the selected item.
                WatchVariable watchVariable;
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                    if (watchVariable != null)
                    {
                        // Check whether the selected watch variable is a bitmask.
                        if (watchVariable.IsBitMask == true)
                        {
                            m_MenuItemConfigureBitmaskPlot.Enabled = true;
                        }
                    }
                }
                catch (Exception)
                {
                    // Ensure that an exception is not thrown.
                }
            }
        }

        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Configure Bitmask Plot' context menu option. Show the form that allows the user to define 
        /// which bits of the selected bitmask watch variable are to be plotted.
        /// </summary>
        /// <remarks>This menu option is only relevant to the child form used to configure the plot layout.</remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_MenuItemConfigureBitmaskPlot_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_ListBoxSelected.SelectedItems.Count == 1)
            {
                int oldIdentifier = ((WatchItem_t)m_ListBoxSelected.SelectedItem).OldIdentifier;

                // Get the watch variable associated with the selected item.
                WatchVariable watchVariable;
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                    if (watchVariable != null)
                    {
                        // Check whether the selected watch variable is a bitmask.
                        if (watchVariable.IsBitMask == true)
                        {
                            try
                            {
                                // Yes - Show the form that allows the user to configure the plot.
                                FormPlotDefineBitmask formPlotDefineBitmask = new FormPlotDefineBitmask(ref m_ListBoxes[m_TabControlColumn.SelectedIndex]);

                                // The CalledFrom property is used to allow the called form to reference back to this form.
                                formPlotDefineBitmask.CalledFrom = this;
                                DialogResult dialogResult = formPlotDefineBitmask.ShowDialog();

                                // Check whether the bitmask value has been modified.
                                if (dialogResult == DialogResult.Yes)
                                {
                                    EnableApplyAndOKButtons();
                                    OnDataUpdate(this, new EventArgs());
                                }
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(Resources.MBTBitmaskOnly, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        #endregion - [Context Menu] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        #region - [FormWorksetDefine] -
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

            // ----------------------------------------------------------------------------------
            // For each selected item: (a) add this to the destination list; (b) delete from the source list and (c) update the removed flag.
            // ----------------------------------------------------------------------------------
            int oldIdentifier;
            for (int index = 0; index < count; ++index)
            {
                // Add the item to the destination list.
                m_ListBoxSelected.Items.Add(selectedObjectCollection[index]);

                // Keep track of the number of watch elements that have been added to the workset.
                m_ListItemCount++;

                oldIdentifier = ((WatchItem_t)selectedObjectCollection[index]).OldIdentifier;

                // Keep track of which watch variables have been removed.
                m_WatchItems[oldIdentifier].Added = true;

                AddSupplementalFields(oldIdentifier);
            }

            UpdateCount();

            // Scroll to the end of the list.
            m_ListBoxSelected.SetSelected(m_ListBoxSelected.Items.Count - 1, true);
            m_ListBoxSelected.ClearSelected();

            m_ButtonApply.Enabled = true;
            EnableApplyAndOKButtons();
            OnDataUpdate(this, new EventArgs());
        }

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

            Cursor = Cursors.WaitCursor;

            // ------------------------------------------------------------------------------------------------------------------------------
            // For each selected item: (a) add this to the destination list; (b) delete from the source list and (c) update the removed flag.
            // ------------------------------------------------------------------------------------------------------------------------------
            int oldIdentifier;
            for (int index = 0; index < count; ++index)
            {
                // Add the item to the destination list.
                listDestination.Items.Add(listSource.SelectedItems[index]);

                // Keep track of the number of watch elements that have been added to the workset.
                m_ListItemCount++;

                oldIdentifier = ((WatchItem_t)listSource.SelectedItems[index]).OldIdentifier;

                // Keep track of which watch variables have been removed.
                m_WatchItems[oldIdentifier].Added = true;

                AddSupplementalFields(oldIdentifier);
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

            EnableApplyAndOKButtons();
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
            // For each selected item, delete it from the source list.
            // ------------------------------------------------------------------------------------------------
            for (int index = 0; index < count; ++index)
            {
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

            EnableApplyAndOKButtons();
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

            m_LabelAvailableCount.Text = Resources.LegendAvailable + CommonConstants.Colon + m_ListBoxAvailable.Items.Count.ToString();

            // Watch variable count associated with each column of the workset.
            int countColumn1 = m_ListBox1.Items.Count;
            int countColumn2 = m_ListBox2.Items.Count;
            int countColumn3 = m_ListBox3.Items.Count;
            m_LabelCount1.Text = "Count" + CommonConstants.Colon + countColumn1.ToString();
            m_LabelCount2.Text = "Count" + CommonConstants.Colon + countColumn2.ToString();
            m_LabelCount3.Text = "Count" + CommonConstants.Colon + countColumn3.ToString();

            // Total number of watch variables in the workset.
            m_LabelCountTotal.Text = Resources.LegendTotalCount + CommonConstants.Colon + m_ListItemCount.ToString();
        }

        /// <summary>
        /// Update the <c>ListBox</c> control that displays the list of available watch variables that match the current search criteria with the list of currently 
        /// available watch variables, sorted alpha-numerically. 
        /// </summary>
        protected override void UpdateListBoxAvailable(WatchItem_t[] watchItems)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            // The list of watch items that are to be added to the ListBox control. The type parameter must be an object type rather than a WatchItem_t type so that
            // the ToArray() method can be used to generate an object[] which can then be added to existing the ListBox.Items using the AddRange method.
            List<object> watchItemList = new List<object>();

            WatchItem_t watchItem;
            for (short oldIdentifier = 0; oldIdentifier < watchItems.Length; oldIdentifier++)
            {
                watchItem = watchItems[oldIdentifier];

                // Ensure that the DisplayMask field is set to display all bits if the watch varable is a bitmask watch variable.
                watchItem.DisplayMask = uint.MaxValue;

                // If the watch variable has been added to the workset, add it to the list of available watch variables, provided that it exists in the new 
                // data dictionary.
                if ((watchItem.Exists == true) &&
                    (watchItem.Added == true))
                {
                    watchItemList.Add(watchItem);
                }
            }

            // Sort the list alpha-numerically.
            watchItemList.Sort(CompareWatchItemsByVariableName);

            // Now search the list for those variables that match the search text.
            List<object> searchItemList = watchItemList.FindAll(ContainsSearchText);

            // Add the items to the ListBox control.
            m_ListBoxAvailable.SuspendLayout();
            m_ListBoxAvailable.Items.Clear();
            m_ListBoxAvailable.Items.AddRange(searchItemList.ToArray());
            m_ListBoxAvailable.PerformLayout();

            // Update the watch variable count.
            m_LabelAvailableCount.Text = Resources.LegendAvailable + CommonConstants.Colon + m_ListBoxAvailable.Items.Count.ToString();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Enable the Apply and OK Buttons without carrying out any checks on the workset. 
        /// </summary>
        protected override void EnableApplyAndOKButtons()
        {
             // Check whether the watch variable count is within the permitted limits.
            if (m_ListItemCount <= EntryCountMax)
            {
                m_ButtonOK.Enabled = true;
                m_ButtonApply.Enabled = true;
            }
        }

        /// <summary>
        /// Convert the current user setting to a workset.
        /// </summary>
        /// <param name="worksetName">The name of the workset.</param>
        /// <returns>The user settings converted to a workset.</returns>
        private Workset_t ConvertToWorkset(string worksetName)
        {
            Workset_t workset = new Workset_t();
            workset.Replicate(m_Workset);

            // This attribute is used to define the plot screen layout and is defined by the user when displaying the saved data file.
            workset.PlotTabPages = new PlotTabPage_t[workset.Column.Length];
            for (int tabPageindex = 0; tabPageindex < workset.Column.Length; tabPageindex++)
            {
                workset.PlotTabPages[tabPageindex].HeaderText = m_TextBoxHeaders[tabPageindex].Text;
                workset.PlotTabPages[tabPageindex].OldIdentifierList = new List<short>();
                workset.PlotTabPages[tabPageindex].DisplayMaskList = new List<uint>();

                for (int index = 0; index < m_ListBoxes[tabPageindex].Items.Count; index++)
                {
                    workset.PlotTabPages[tabPageindex].OldIdentifierList.Add(((WatchItem_t)m_ListBoxes[tabPageindex].Items[index]).OldIdentifier);
                    workset.PlotTabPages[tabPageindex].DisplayMaskList.Add(((WatchItem_t)m_ListBoxes[tabPageindex].Items[index]).DisplayMask);
                }
            }

            return workset;
        }
        #endregion - [FormWorksetDefine] -

        /// <summary>
        /// Add the watch variables defined in the specified list of old identifiers to the specified <c>ListBox</c> control.
        /// </summary>
        /// <param name="listBox">The <c>ListBox</c> to which the items are to be added.</param>
        /// <param name="plotTabPage">The plot tab page of the workset that is to be added to the <c>ListBox</c> control.</param>
        protected void WatchItemAddRange(ListBox listBox, PlotTabPage_t plotTabPage)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            List<short> oldIdentifierList = plotTabPage.OldIdentifierList;
            List<uint> displayMaskList = plotTabPage.DisplayMaskList;

            Debug.Assert(oldIdentifierList.Count == displayMaskList.Count, "FormPlotDefine.WatchItemAddRange() - oldIdentifierList.Count == displayMaskList.Count[]");

            listBox.Items.Clear();
            listBox.SuspendLayout();

            WatchItem_t watchItem;
            short oldIdentifier;
            uint displayMask;
            for (int index = 0; index < oldIdentifierList.Count; index++)
            {
                watchItem = new WatchItem_t();
                oldIdentifier = oldIdentifierList[index];
                displayMask = displayMaskList[index];
                watchItem.OldIdentifier = oldIdentifier;

                // The DisplayMask field is only applicable to bitmask watch variables and is used to define which bits of the bitmask watch variable.
                watchItem.DisplayMask = displayMask;
                watchItem.Added = true;
                listBox.Items.Add(watchItem);
            }

            listBox.PerformLayout();
            Cursor = Cursors.Default;
        }
        #endregion --- Methods ---

		#region --- Properties ---
		/// <summary>
		/// For plots, we allow MaxInt entries, essentially no limit.
		/// </summary>
		public override int EntryCountMax
		{
			get { return int.MaxValue; }
		}
		#endregion
	}
}
