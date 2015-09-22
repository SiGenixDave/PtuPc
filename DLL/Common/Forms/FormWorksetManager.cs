#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit.
 * 
 *  Project:    PTU Application
 * 
 *  File name:  FormWorksetManager.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/26/10    1.1     K.McD           1.  Bug fix - SNCR001.38. Status is now displayed using a single ListView control.
 *                                      2.  SNCR001.29 Added support for the SecurityLevel tag associated with the Workset_t structure.
 *                                      3.  Added support for the 'Override Security' context menu option.
 *                                      4.  Made a number of methods virtual.
 * 
 *  02/28/11    1.2     K.McD           1.  Added an event handler for the context menu Opened event so that the Enabled property of each context menu could be 
 *                                          set or cleared depending upon the selected context menu and current security level.
 *                                      2.  Auto-modified as a result of a number of resource name changes.
 *                                      3.  Modified the event handler for the 'Set As Default' context menu option to ensure that the security level of the 
 *                                          workset that has been set to be the default workset is set to the highest security level.
 * 
 *  03/18/11    1.3     K.McD           1.  Modified a number of comments and XML tags.
 *                                      2.  Auto-modified as a result of the name changes to a number of properties associated with the Security class.
 * 
 *  03/28/11    1.4     K.McD           1.  Auto-modified as a result of name changes to a number of methods assoiated with internal classes.
 *                                      2.  Implemented the Save() method as this is no longer overwritten in the child classes.
 * 
 *  05/23/11    1.5     K.McD           1.  Renamed the event handler for the OK button 'Click' event.
 *  
 *  05/23/11    1.5.1   K.McD           1.  Added the  ImageIndexBookmark constant.
 *  
 *  08/17/11    1.5.2   K.McD           1.  SNCR002.21. No longer displays a warning message if the user attempts to edit the default workset.
 * 
 *
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

using Common;
using Common.Configuration;
using Common.Properties;

namespace Common.Forms
{
    /// <summary>
    /// Form to manage the the worksets associated with a project. Allows the user to: edit, add and delete individual worksets or set any of the worksets to be the default 
    /// workset.
    /// </summary>
    public partial class FormWorksetManager : FormPTUDialog
    {
        #region --- Constants ---
        /// <summary>
        /// The image index associated with the bookmark image. Value: 2.
        /// </summary>
        protected const int ImageIndexBookmark = 2;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The selected index of the ListView.
        /// </summary>
        private int m_SelectedIndex;

        /// <summary>
        /// The workset collection that is to be managed.
        /// </summary>
        protected WorksetCollection m_WorksetCollection;
        #endregion --- Member Variables ---

        #region ---- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor, required by Visual Studio.
        /// </summary>
        public FormWorksetManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class. Records the securty level of the user and enables/disables the 'Set as Default' context menu option accordingly.
        /// </summary>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        public FormWorksetManager(WorksetCollection worksetCollection)
        {
            InitializeComponent();

            m_WorksetCollection = worksetCollection;

            Debug.Assert(Security != null);

            // Check that the user has sufficient privileges to modify the default setting account.
            if (Security.SecurityLevelCurrent >= Security.SecurityLevelHighest)
            {
                m_ContextMenuItemSetAsDefault.Enabled = true;
                m_ContextMenuItemOverrideSecurity.Enabled = true;
            }
            else
            {
                m_ContextMenuItemSetAsDefault.Enabled = false;
                m_ContextMenuItemOverrideSecurity.Enabled = false;
            }
        }
        #endregion ---- Constructors ---

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

                    m_WorksetCollection = null;
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
        #region - Form -
        /// <summary>
        /// Event handler for the <c>Activated</c> event. Populates the <c>ListView</c> control with the currently defined worksets.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormIODisplay_Activated(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            UpdateListView();
            m_ListView.Focus();
        }
        #endregion - Form -

        #region - [Context Menu Options] -
        /// <summary>
        /// Event handler for the context menu <c>Opened</c> event. Set the <c>Enabled</c> property of the Edit, Rename and Delete context menu options appropriate to 
        /// the current security level and selected workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ContextMenu_Opened(object sender, EventArgs e)
        {
            // Local reference to the selected item.
            WorksetItem selectedItem = new WorksetItem();

            // The index value of the selected workset.
            int selectedIndex;

            // Get the selected workset and index.
            try
            {
                selectedItem = (WorksetItem)m_ListView.SelectedItems[0];
                selectedIndex = m_ListView.SelectedIndices[0];
            }
            catch (Exception)
            {
                selectedItem = null;
                selectedIndex = 0;
            }

            // Check that a workset has been selected.
            if (selectedItem == null)
            {
                return;
            }

            // If the selected workset: (a) is the baseline workset, (b) has a security level higher than the current security level or (c) is the default 
            // workset and the current security level is not the highest security level, disable the Edit, Rename and Delete context menu options; otherwise enable 
            // the menu options.
            if ((selectedIndex == 0) ||
                (Security.SecurityLevelCurrent < selectedItem.Workset.SecurityLevel) ||
                ((selectedIndex == m_WorksetCollection.DefaultIndex) && (Security.SecurityLevelCurrent < Security.SecurityLevelHighest)))
            {
                m_ContextMenuItemEdit.Enabled = false;
                m_ContextMenuItemDelete.Enabled = false;
                m_ContextMenuItemRename.Enabled = false;
            }
            else
            {
                m_ContextMenuItemEdit.Enabled = true;
                m_ContextMenuItemDelete.Enabled = true;
                m_ContextMenuItemRename.Enabled = true;
            }
        }

        /// <summary>
        /// Event handler for the 'Edit' context menu option <c>Click</c> event. Edits the selected workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ContextMenuItemEdit_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Local reference to the selected item.
            WorksetItem selectedItem = new WorksetItem();

            // The index value of the selected workset.
            int selectedIndex;

            try
            {
                selectedItem = (WorksetItem)m_ListView.SelectedItems[0];
                selectedIndex = m_ListView.SelectedIndices[0];
            }
            catch (Exception)
            {
                selectedItem = null;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            // Check that an item has been selected.
            if (selectedItem == null)
            {
                MessageBox.Show(Resources.MBTInstructionSelectWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check that the baseline workset hasn't been selected.
            if (m_ListView.SelectedIndices[0] == 0)
            {
                MessageBox.Show(Resources.MBTUnauthorizedEditBaselineWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // If the security level of the current user is less than that of the workset then don't allow the workset to be deleted.
            if (Security.SecurityLevelCurrent < selectedItem.Workset.SecurityLevel)
            {
                MessageBox.Show(Resources.MBTUnauthorizedEditWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check if the selected setting is the default setting.
            if (m_ListView.SelectedIndices[0] == m_WorksetCollection.DefaultIndex)
            {
                if (Security.SecurityLevelCurrent < Security.SecurityLevelHighest)
                {
                    MessageBox.Show(Resources.MBTUnauthorizedEditDefaultWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            EditSelectedWorkset(selectedItem);
        }

        /// <summary>
        /// Event handler for the 'New' context menu option <c>Click</c> event. Shows the form which allows the user to define a new workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ContextMenuItemNew_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            CreateNewWorkset();
        }

        /// <summary>
        /// Event handler for the 'Copy' context menu option <c>Click</c> event. Copies an existing workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ContextMenuItemCopy_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Local reference to the selected item.
            WorksetItem selectedItem = new WorksetItem();

            // The index value of the selected workset.
            int selectedIndex;

            try
            {
                selectedItem = (WorksetItem)m_ListView.SelectedItems[0];
                selectedIndex = m_ListView.SelectedIndices[0];
            }
            catch (Exception)
            {
                selectedItem = null;
                selectedIndex = 0;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            // Check that an item has been selected.
            if (selectedItem == null)
            {
                MessageBox.Show(Resources.MBTInstructionSelectWorkset, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Create a temporary workset.
            Workset_t copyOfWorkset = new Workset_t();
            copyOfWorkset.Replicate(selectedItem.Workset);
            
            // Override the security level to be the security level of the current user.
            copyOfWorkset.SecurityLevel = Security.SecurityLevelCurrent;

            // Override the name of the workset.
            copyOfWorkset.Name = selectedItem.Workset.Name + CommonConstants.BindingMessage + Resources.TextCopy;
            m_WorksetCollection.Add(copyOfWorkset);
            Cursor = Cursors.Default;

            // Save the modified object to disk.
            Save();
            UpdateListView();
        }

        /// <summary>
        /// Event handler for the 'Delete' context menu option <c>Click</c> event. Deletes the selected workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ContextMenuItemDelete_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Local reference to the selected item.
            WorksetItem selectedItem = new WorksetItem();

            // The index value of the selected workset.
            int selectedIndex;

            try
            {
                selectedItem = (WorksetItem)m_ListView.SelectedItems[0];
                selectedIndex = m_ListView.SelectedIndices[0];
            }
            catch (Exception)
            {
                selectedItem = null;
                selectedIndex = 0;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            // Check that an item has been selected.
            if (selectedItem == null)
            {
                MessageBox.Show(Resources.MBTInstructionSelectWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check that the baseline workset hasn't been selected.
            if (m_ListView.SelectedIndices[0] == 0)
            {
                MessageBox.Show(Resources.MBTUnauthorizedDeleteBaselineWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // If the security level of the current user is less than that of the workset then don't allow the workset to be deleted.
            if (Security.SecurityLevelCurrent < selectedItem.Workset.SecurityLevel)
            {
                MessageBox.Show(Resources.MBTUnauthorizedDeleteWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check if the selected setting is the default setting.
            if (m_ListView.SelectedIndices[0] == m_WorksetCollection.DefaultIndex)
            {
                if (Security.SecurityLevelCurrent < Security.SecurityLevelHighest)
                {
                    MessageBox.Show(Resources.MBTUnauthorizedDeleteDefaultWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(Resources.MBTQueryDeleteDefaultWorkset, Resources.MBCaptionInformation, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    Update();
                    if (dialogResult != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }
            else
            {
                // Get the user to confirm the selected deletion.
                DialogResult dialogResult = MessageBox.Show(string.Format(Resources.MBTQueryDeleteWorkset, ((WorksetItem)m_ListView.SelectedItems[0]).Workset.Name), Resources.MBCaptionInformation, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                Update();
                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }
            }

            m_WorksetCollection.Remove(selectedItem.Workset.Name);

            // Save the modified object to disk.
            Save();
            UpdateListView();
        }

        /// <summary>
        /// Event handler for the 'Rename' context menu option <c>Click</c> event. Renames the selected workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ContextMenuItemRename_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Local reference to the selected item.
            WorksetItem selectedItem = new WorksetItem();

            try
            {
                selectedItem = (WorksetItem)m_ListView.SelectedItems[0];
                m_SelectedIndex = m_ListView.SelectedIndices[0];
            }
            catch (Exception)
            {
                selectedItem = null;
                m_SelectedIndex = 0;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            // Check that an item has been selected.
            if (selectedItem == null)
            {
                MessageBox.Show(Resources.MBTInstructionSelectWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check that the baseline configuration hasn't been selected.
            if (m_ListView.SelectedIndices[0] == 0)
            {
                MessageBox.Show(Resources.MBTUnauthorizedRenameBaselineWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // If the security level of the current user is less than that of the workset then don't allow the workset to be renamed.
            if (Security.SecurityLevelCurrent < selectedItem.Workset.SecurityLevel)
            {
                MessageBox.Show(Resources.MBTUnauthorizedRenameWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Allow the user to modify the name of the workset.
            m_ListView.Items[m_SelectedIndex].BeginEdit();
        }

        /// <summary>
        /// Event handler for the <c>AfterLabelEdit</c> event. Updates the <c>workset</c> class with the new name of the workset and then saves the changes to disk.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if a valid selection hasn't been made or the baseline workset has been selected.
            if ((e.Label != null) && (e.Item != 0) && (e.Label != string.Empty))
            {
                // get the selected workset.
                Workset_t workset = ((WorksetItem)m_ListView.Items[e.Item]).Workset;

                // Update the name field of the workset.
                workset.Name = e.Label.Trim();

                for (int index = 0; index < m_ListView.Items.Count; index++)
                {
                    if (((WorksetItem)m_ListView.Items[index]).Workset.Name == workset.Name)
                    {
                        MessageBox.Show(Resources.MBTWorksetNameExists, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.CancelEdit = true;
                        return;
                    }
                }

                // Update the workset class.
                m_WorksetCollection.Worksets[e.Item] = workset;

                // Save the modified object to disk.
                Save();

                // Update the ListView.
                UpdateListView();
            }
            else
            {
                e.CancelEdit = true;
            }
        }

        /// <summary>
        /// Event handler for the 'Set As Default' context menu option <c>Click</c> event. Sets the selected workset as the default workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ContextMenuItemSetAsDefault_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Local reference to the selected item.
            WorksetItem selectedItem = new WorksetItem();

            // The index value of the selected workset.
            int selectedIndex;

            try
            {
                selectedItem = (WorksetItem)m_ListView.SelectedItems[0];
                selectedIndex = m_ListView.SelectedIndices[0];
            }
            catch (Exception)
            {
                selectedItem = null;
                selectedIndex = 0;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            // Check that an item has been selected.
            if (selectedItem == null)
            {
                MessageBox.Show(Resources.MBTInstructionSelectWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Update the object reference with the new default setting.
            m_WorksetCollection.SetDefaultWorkset(selectedItem.Workset.Name);

            // Ensure that the security level of the default workset is set to the appropriate level.
            if (selectedItem.Workset.SecurityLevel < Security.SecurityLevelHighest)
            {
                // Copy the selected workset
                Workset_t workset = new Workset_t();
                workset = selectedItem.Workset;

                // Modify the security level of the workset to be the highest security level.
                workset.SecurityLevel = Security.SecurityLevelHighest;

                // Replace the existing workset with the workset with the modified security level.
                m_WorksetCollection.Edit(selectedItem.Workset.Name, workset);
            }

            // Save the modified object to disk.
            Save();
            UpdateListView();
        }

        /// <summary>
        /// Event handler for the 'Override Security' context menu option <c>Click</c> event. Sets the security level of the selected workset 
        /// to the specified value.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ContextMenuItemOverrideSecurity_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Local reference to the selected item.
            WorksetItem selectedItem = new WorksetItem();

            // The index value of the selected workset.
            int selectedIndex;

            try
            {
                selectedItem = (WorksetItem)m_ListView.SelectedItems[0];
                selectedIndex = m_ListView.SelectedIndices[0];
            }
            catch (Exception)
            {
                selectedItem = null;
                selectedIndex = 0;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            // Check that an item has been selected.
            if (selectedItem == null)
            {
                MessageBox.Show(Resources.MBTInstructionSelectWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormSetSecurityLevel FormSetSecurityLevel = new FormSetSecurityLevel(selectedItem.Workset.Name, selectedItem.Workset.SecurityLevel);
            FormSetSecurityLevel.CalledFrom = this;
            DialogResult dialogResult = FormSetSecurityLevel.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                // Copy the selected workset
                Workset_t workset = new Workset_t();
                workset = selectedItem.Workset;

                // Modify the security level of the workset to the selected security level.
                workset.SecurityLevel = FormSetSecurityLevel.SecurityLevel;

                // Replace the existing workset with the workset with the modified security level.
                m_WorksetCollection.Edit(selectedItem.Workset.Name, workset);

                // Save the modified object to disk.
                Save();
                UpdateListView();
            }
        }
        #endregion - [Context Menu Options] -

        /// <summary>
        /// Event handler for the OK button <c>Click</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonOK_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Close();
        }

        /// <summary>
        /// Event handler for the list view control <c>DoubleClick</c> event. Simulate the user having selected the 'Edit' context menu option.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ListView_DoubleClick(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_ContextMenuItemEdit_Click(sender, e);
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Save the workset information to disk.
        /// </summary>
        protected void Save()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            try
            {
                // Generate the name of the file where the serialized data is to be stored, this derived from the project Identifier. If this is empty 
                // use the default filename.
                string filename;
                string qualifier = "." + m_WorksetCollection.WorksetCollectionType.ToString();
                if (m_WorksetCollection.ProjectInformation.ProjectIdentifier != string.Empty)
                {
                    filename = m_WorksetCollection.ProjectInformation.ProjectIdentifier + qualifier + CommonConstants.ExtensionWorksetFile;
                }
                else
                {
                    filename = Resources.FileMnemonicDefaultWorkset + qualifier + CommonConstants.ExtensionWorksetFile;
                }

                // Serialize the new configuration to the specified fully qualified file.
                m_WorksetCollection.Serialize(DirectoryManager.PathWorksetFiles + CommonConstants.BindingFilename + filename);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Call the form which allows the user to edit the selected workset.
        /// </summary>
        /// <param name="selectedItem">The selected workset item.</param>
        protected virtual void EditSelectedWorkset(WorksetItem selectedItem)
        {
        }

        /// <summary>
        /// Call the form which allows the user to create a new workset.
        /// </summary>
        protected virtual void CreateNewWorkset()
        {
        }

        /// <summary>
        /// Updates the ListView control with the list of available worksets.
        /// </summary>
        private void UpdateListView()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Clear the list view items.
            m_ListView.Items.Clear();

            // Add the updates list view items.
            for (int index = 0; index < m_WorksetCollection.Worksets.Count; index++)
            {
                // Display the worksets and show which is the default workset.
                bool isDefaultWorkset = (index == m_WorksetCollection.DefaultIndex) ? true : false;

                // Get the security level associated with the workset.
                SecurityLevel securityLevel = m_WorksetCollection.Worksets[index].SecurityLevel;
                m_ListView.Items.Add(new WorksetItem(m_WorksetCollection.Worksets[index], isDefaultWorkset, securityLevel));
            }

            Cursor = Cursors.Default;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Reference to the <c>ListView</c> associated with the form.
        /// </summary>
        public System.Windows.Forms.ListView ListView
        {
            get { return m_ListView; }
        }
        #endregion --- Properties ---
    }
}