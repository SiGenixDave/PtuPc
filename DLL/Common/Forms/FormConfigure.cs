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
 *  File name:  FormConfigure.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/15/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  05/23/11    1.1     K.McD           1.  Corrected a number of calls to the Debug.Assert() method.
 *                                      2.  Corrected a number of XML tags.
 *                                      3.  Replaced the ToolStripComboBox control with a standard ComboBox control.
 *                                      4.  Renamed the CompareWorkset() method to CompareWorksetNoChartScale() to emphasise that the comparison does not 
 *                                          include chart recorder scaling information.
 *                                      5.  Created a new CompareWorkset() method that includes a comparison of the chart recorder scaling information.
 *                                      6.  Modified the LoadWorkset() method to accommodate the signature change to the FormWorksetDefine.WatchItemAddRange() method.
 *                                      7.  Auto-modified as a result of a name change to an inherited TabControl.
 *                                      8.  Modified the contructor to sort out the display and positioning of the OK/Cancel/Apply buttons.
 *                                      9.  Replaced the OK/Apply buttons with the 'Download' tool strip button.
 *                                      10. Added a virtual event handler for the 'Download' button.
 *                                      
 *  05/24/11    1.2     K.McD           1.  Added the DownloadWorkset() virtual method.
 *  
 *  05/26/11    1.3     K.McD           1.  Bug fix. Added the ClearListBoxColumns() virtual method to allow child classes to clear multiple ListBox controls, if 
 *                                          required.
 *                                      2.  Modified the CreateNewWorkset() method to call the ClearListBoxColumns() virtual method rather than clearing the 
 *                                          ListBox items directly.
 *                                      3.  Corrected the CompareWorkset() method.
 *                                      
 *  06/21/11    1.4     K.McD           1.  Corrected a number of comments and XML tags.
 *                                      2.  Auto-modified as a result of a name change to an inherited member variable.
 *                                      3.  Modified the 'SelectedIndexChanged' event handler for the workset selection 'ComboBox' control to display the name of 
 *                                          the selected workset in the header of the 'TabPage'. 
 *  07/11/11    1.5     K.McD           1.  Removed the command to force Focus when the selected index changes on the ComboBoxWorkset control.
 *  
 *  08/05/11    1.6     K.McD           1.  Modified to support the form which allows the user to configure the fault log parameters.
 *                                              (a) Changed the modifiers associated with the ConfigurationModified() and CompareName() methods to private.
 *                                              (b) Made the LoadWorkset() method virtual.
 *                                              (c) Removed the CompareWorksetNoChartScale() methods.
 *                                              (d) Modified the CompareWorkset() method such that the chart scaling information is now excluded from the workset
 *                                                  comparison. 
 *                                              
 *  10/02/11    1.7     K.McD           1.  Added a check as to whether a call to the Dispose() method has been called to the DataUpdate event handler.
 *  
 *	10/05/11	1.8		Sean.D			1.	Fixed a small bug in CompareWorkset() where an exception was raised when the OldIdentifier list in the given column of
 *	                                        worksetToCompare had fewer items than workset. Added a check to indicate the worksets are different if the counts differ.
 *	                                        
 *  04/02/15    1.9     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                              1.  MOC-0171-28. A check will be included as part of the ‘Save’ procedure to ensure that an empty workset cannot be saved.
 *                                          
 *                                                  As a result of a review of the software, it is proposed that it should only be possible to save a workset if the
 *                                                  following criteria are met:
 *                                              
 *                                                      1.  The workset must contain at least one watch variable.
 *                                                      2.  The workset must have a valid name that is not currently in use.
 *                                          
 *                                      Modifications
 *                                      1.  Included a call to the ClearStatusMessage() method in the constructor.
 *                                      2.  Modified the  m_ComboBoxWorkset_SelectedIndexChanged() method so that the TabPage header is not updated with  the
 *                                          workset name when a new workset is selected.
 *                                      3.  Modified the  m_TSBEdit_Click(() event handler so that it disables the Save button until after the user modifies the
 *                                          empty workset.
 *                                      4.  Modified the  m_TSBNew, m_TSBCopy_Click() and m_TSBRename() event handlers to to check whether the workset name
 *                                          is in use and to disable the Save button until after the user modifies the workset.
 *                                      5.  Modified the  SetModifyState() method to call to the ClearStatusMessage() method immediately before the return statement.
 *                                      6.  Added the EnableApplAndOKButtons() override to set m_TSBSave rather than the Apply and OK buttons.
 *                                      7.  Removed the FormWorksetDefine_DataUpdate() method and the registration of this against the DataUpdate event.
 *                                      8.  Wherever the Text property of the 'm_TextBoxName' TextBox was changed the code was modified to ensure that this didn't
 *                                          trigger the TextChanged() event handler by de-registering the event handler prior to setting the property and then
 *                                          re-registering it again.
 *                                      9.  Replaced the m_NoDefinedWorkset flag with the m_UseTextBoxAsNameSource flag.
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
    /// Parent class for the forms that are used to configure the chart recorder and fault log data streams.
    /// </summary>
    public partial class FormConfigure : FormWorksetDefine
    {
        #region --- Enumerators ---
        /// <summary>
        /// The current modify state of the form.
        /// </summary>
        protected enum ModifyState
        {
            /// <summary>
            /// Configure.
            /// </summary>
            Configure,

            /// <summary>
            /// Edit mode.
            /// </summary>
            Edit,

            /// <summary>
            /// Create mode.
            /// </summary>
            Create,

            /// <summary>
            /// Copy mode.
            /// </summary>
            Copy,

            /// <summary>
            /// Rename mode.
            /// </summary>
            Rename,

            /// <summary>
            /// Undefined.
            /// </summary>
            Undefined
        }
        #endregion --- Enumerators ---

        #region --- Member Variables ---
        /// <summary>
        /// The workset that is to be compared with the worksets contained within the list.
        /// </summary>
        protected Workset_t m_WorksetToCompare;

        /// <summary>
        /// The workset that was initially uploaded from the VCU.
        /// </summary>
        protected Workset_t m_WorksetFromVCU;

        /// <summary>
        /// A record of the selected workset.
        /// </summary>
        protected Workset_t m_SelectedWorkset;

        /// <summary>
        /// The current state of the form.
        /// </summary>
        protected ModifyState m_ModifyState = ModifyState.Undefined;

        /// <summary>
        /// <para>A flag that specifies whether the workset name should be derived from the TextBox or the ComboBox control when the workset is
        /// saved.</para><para>True, if the TextBox is the source of the workset name; otherwise, false, if the ComboBox is the source of the workset name.
        /// Initialized to false.</para>
        /// </summary>
        protected bool m_UseTextBoxAsNameSource = false;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes an new instance of the form. Zero parameter constructor.
        /// </summary>
        public FormConfigure()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes an new instance of the form.
        /// </summary>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        public FormConfigure(WorksetCollection worksetCollection)
            : base(worksetCollection)
        {
            InitializeComponent();

            // Only one column is required for this workset so delete the tab pages associated with columns 2 and 3.
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn2);
            m_TabControlColumn.TabPages.Remove(m_TabPageColumn3);

            ComboBoxAddWorksets(m_ComboBoxWorkset, m_WorksetCollection);

            // OK/Apply/Cancel Buttons.
            m_ButtonCancel.Location = m_ButtonApply.Location;
            m_ButtonApply.Visible = false;
            m_ButtonOK.Visible = false;

            // Workset Selection.
            m_LegendName.Text = Resources.LegendWorkset;
            m_TextBoxName.Visible = false;

            ClearStatusMessage();
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
        /// Event handler for <c>ComboBox</c> control <c>SelectedIndexChanged</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_ComboBoxWorkset_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            m_ComboBoxWorkset.DropDownStyle = ComboBoxStyle.DropDownList;

            // Load the selected workset.
            Workset_t workset = (Workset_t)m_ComboBoxWorkset.SelectedItem;
            m_SelectedWorkset = workset;

            m_CreateMode = false;
            m_UseTextBoxAsNameSource = false;
            m_TextBoxName.Enabled = false;

            SetModifyState(ModifyState.Configure);

            if (ConfigurationModified() == true)
            {
                m_TSBDownload.Enabled = true;
            }
            else
            {
                m_TSBDownload.Enabled = false;
            }

            Cursor = Cursors.Default;
        }

        #region - [ToolStripButtons] -
        /// <summary>
        /// Event handler for the 'Download' <c>ToolStripButton</c> <c>Click</c> event. The logic is defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_TSBDownload_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Event handler for the 'Save' <c>ToolStripButton</c> <c>Click</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_TSBSave_Click(object sender, EventArgs e)
        {
            string userMessage = string.Empty;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            m_TSBSave.Checked = true;

            // Convert the downloaded chart configuration to a workset format.
            Workset_t workset;
            if (m_UseTextBoxAsNameSource == true)
            {
                workset = ConvertToWorkset(m_TextBoxName.Text);
            }
            else
            {
                workset = ConvertToWorkset(m_ComboBoxWorkset.Text);
            }

            m_WorksetToCompare = workset;
            Workset_t matchedWorkset;

            switch (m_ModifyState)
            {
                case ModifyState.Rename:
                    // Check whether the name already exists. 
                    matchedWorkset = m_WorksetCollection.Worksets.Find(CompareName);
                    if (matchedWorkset.WatchItems != null)
                    {
                        // Yes, inform the user.
                        MessageBox.Show(Resources.MBTWorksetNameExists, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_TSBSave.Checked = false;
                        Cursor = Cursors.Default;
                        return;
                    }
                    break;
                default:
                    // Check whether the workset identifiers currently exists.
                    matchedWorkset = m_WorksetCollection.Worksets.Find(CompareWorkset);

                    // Check whether the current parameters, excluding name and security level, match those of an existing workset.
                    if (matchedWorkset.WatchItems != null)
                    {
                        // Yes - check whether the names are identical.
                        if (workset.Name == matchedWorkset.Name)
                        {
                            MessageBox.Show(string.Format(Resources.MBTWorksetExists, workset.Name), Resources.MBCaptionInformation, MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(string.Format(Resources.MBTWorksetIdentical, matchedWorkset.Name), Resources.MBCaptionInformation, MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        }

                        m_TSBSave.Checked = false;
                        Cursor = Cursors.Default;
                        return;
                    }
                    break;
            }
            
            // Check whether the workset is an existing workset that is to be modified or a new workset. 
            if (m_CreateMode == true)
            {
                // The workset is a new workset, check whether the name already exists.
                matchedWorkset = m_WorksetCollection.Worksets.Find(CompareName);
                if (matchedWorkset.WatchItems != null)
                {
                    // Yes, inform the user.
                    MessageBox.Show(Resources.MBTWorksetNameExists, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_TSBSave.Checked = false;
                    Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    // No,save the new workset.
                    m_WorksetCollection.Add(workset);
                    ComboBoxAddWorksets(m_ComboBoxWorkset, m_WorksetCollection);
                    m_ComboBoxWorkset.Text = workset.Name;
                    userMessage = string.Format(Resources.MBTWorksetCreationSuccess, workset.Name);
                    SetModifyState(ModifyState.Configure);
                }
            }
            else
            {
                if (Security.SecurityLevelCurrent < ((Workset_t)m_ComboBoxWorkset.SelectedItem).SecurityLevel)
                {
                    MessageBox.Show(Resources.MBTInsufficientPrivilegesModify, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_TSBSave.Checked = false;
                    Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    // Ask the user for confirmation.
                    string worksetName = string.Empty;
                    DialogResult dialogResult;
                    switch (m_ModifyState)
                    {
                        case ModifyState.Rename:
                            worksetName = m_ComboBoxWorkset.Text;
                            dialogResult = MessageBox.Show(string.Format(Resources.MBTQueryWorksetRename, worksetName), Resources.MBCaptionQuestion,
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            userMessage = string.Format(Resources.MBTWorksetRenameSuccess, worksetName, workset.Name);
                            break;
                        case ModifyState.Edit:
                            worksetName = workset.Name;
                            dialogResult = MessageBox.Show(string.Format(Resources.MBTConfirmWorksetModify, worksetName), Resources.MBCaptionQuestion,
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            userMessage = string.Format(Resources.MBTWorksetUpdateSuccess, workset.Name);
                            break;
                        default:
                            m_TSBSave.Checked = false;
                            Cursor = Cursors.Default;
                            return;
                    }

                    if (dialogResult == DialogResult.No)
                    {
                        m_TSBSave.Checked = false;
                        Cursor = Cursors.Default;
                        return;
                    }

                    Update();
                    m_WorksetCollection.Edit(worksetName, workset);
                    ComboBoxAddWorksets(m_ComboBoxWorkset, m_WorksetCollection);
                    m_ComboBoxWorkset.Text = workset.Name;
                    m_SelectedWorkset = workset;
                    SetModifyState(ModifyState.Configure);
                }
            }

            Save();

            // Allow the user to select any of the available worksets. 
            m_TextBoxName.Visible = false;
            m_TextBoxName.Enabled = false;
            m_ComboBoxWorkset.Text = m_TextBoxName.Text;
            m_ComboBoxWorkset.Visible = true;

            m_TSBSave.Checked = false;

            // Inform the user of a successful update.
            MessageBox.Show(userMessage, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the 'Edit' <c>ToolStripButton</c> <c>Click</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_TSBEdit_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Toggle between the edit state and the configure state.
            if (m_ModifyState == ModifyState.Edit)
            {
                SetModifyState(ModifyState.Configure);
            }
            else
            {
                // Check whether the current workset is the default workset.
                if (m_SelectedWorkset.Name == Resources.NameBaselineWorkset)
                {
                    MessageBox.Show(Resources.MBTUnauthorizedEditBaselineWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor = Cursors.Default;
                    return;
                }
                
                if (m_SelectedWorkset.Name == m_WorksetCollection.DefaultName)
                {
                    DialogResult dialogResult = MessageBox.Show(Resources.MBTQueryEditDefaultWorkset, Resources.MBCaptionConfirm, MessageBoxButtons.YesNo,
                                                                MessageBoxIcon.Question);
                    Update();
                    if (dialogResult == DialogResult.No)
                    {
                        Cursor = Cursors.Default;
                        return;
                    }
                }

                SetModifyState(ModifyState.Edit);

                // No need to check whether the workset name is in use when entering Edit mode.
                // EnableApplyAndOKButtons();

                // Disable the Save button until the user modifies the workset. 
                m_TSBSave.Enabled = false;
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the 'New' <c>ToolStripButton</c> <c>Click</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_TSBNew_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Toggle between create state and the configure state.
            if (m_ModifyState == ModifyState.Create)
            {
                SetModifyState(ModifyState.Configure);
            }
            else
            {
                SetModifyState(ModifyState.Create);

                // On this occasion, only used to check whether the default workset name is already in use.
                EnableApplyAndOKButtons();

                // Disable the Save button until the user modifies the empty workset. 
                m_TSBSave.Enabled = false;
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the 'Copy' <c>ToolStripButton</c> <c>Click</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_TSBCopy_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Toggle between copy state and the configure state.
            if (m_ModifyState == ModifyState.Copy)
            {
                SetModifyState(ModifyState.Configure);
            }
            else
            {
                SetModifyState(ModifyState.Copy);

                // On this occasion, only used to check whether the workset name '{original name} - Copy' is already in use.
                EnableApplyAndOKButtons();

                // Disable the Save button until the user modifies the workset. 
                m_TSBSave.Enabled = false;
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the 'Rename' <c>ToolStripButton</c> <c>Click</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_TSBRename_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Toggle between the rename state and the configure state.
            if (m_ModifyState == ModifyState.Rename)
            {
                SetModifyState(ModifyState.Configure);
            }
            else
            {
                if (m_SelectedWorkset.Name == Resources.NameBaselineWorkset)
                {
                    MessageBox.Show(Resources.MBTUnauthorizedRenameBaselineWorkset, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor = Cursors.Default;
                    return;
                }

                SetModifyState(ModifyState.Rename);

                // No need to check whether the workset name is in use when entering Rename mode.
                // EnableApplyAndOKButtons();

                // Disable the Save button until the user renames workset. 
                m_TSBSave.Enabled = false;
            }

            Cursor = Cursors.Default;
        }
        #endregion - [ToolStripButtons] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Download the specified workset to the VCU. The logic is performed in the child class.
        /// </summary>
        /// <param name="workset">The workset that is to be downloaded to the VCU.</param>
        /// <returns>A flag that indicates whether the workset was successfully downloaded to the VCU. True, if the VCU update was successful; otherwise, false.</returns>
        protected virtual bool DownloadWorkset(Workset_t workset)
        {
            // A flag that indicates whether the VCU update was successful.
            bool updateSuccess = false;

            return updateSuccess;
        }

        /// <summary>
        /// Set the modify state to the specified state.
        /// </summary>
        /// <remarks>The Enabled property of the menu option that allows the user to modify the Y axis limits of the individual chart recorder channels is linked 
        /// directly to the ModifyEnabled property of the parent class.</remarks>
        /// <param name="modifyState">The required modify state.</param>
        protected virtual void SetModifyState(ModifyState modifyState)
        {
            m_ModifyState = modifyState;

            if (m_ModifyState == ModifyState.Configure)
            {
                ClearCheckedEditNewCopyRename();

                ModifyEnabled = false;
                SetEnabledEditNewCopyRename(true);

                // Load the previously selected workset.
                LoadWorkset(m_SelectedWorkset);

                // Display the name of the previously selected workset.
                // Ensure that the TextChanged event is not triggered as a result of specifying the Text property of the TextBox control.
                m_TextBoxName.TextChanged -= new EventHandler(m_TextBoxName_TextChanged);
                m_TextBoxName.Text = m_SelectedWorkset.Name;
                m_TextBoxName.TextChanged += new EventHandler(m_TextBoxName_TextChanged);

                // Don't allow the user to change the name of the workset.
                m_TextBoxName.Visible = false;
                m_TextBoxName.Enabled = false;
                m_ComboBoxWorkset.Visible = true;

                // Use the ComboBox as the source of the workset name if it is saved.
                m_UseTextBoxAsNameSource = false;

                // Show the security level of the current user.
                m_TextBoxSecurityLevel.Text = Security.GetSecurityDescription(m_SelectedWorkset.SecurityLevel);

                if (ConfigurationModified() == true)
                {
                    m_TSBDownload.Enabled = true;
                }
                else
                {
                    m_TSBDownload.Enabled = false;
                }
            }
            else
            {
                // Disable the New, Copy, Edit, Delete and Rename buttons.
                SetEnabledEditNewCopyRename(false);

                // Disable the Download button.
                m_TSBDownload.Enabled = false;

                // Use the TextBox as the source of the workset name if it is saved.
                m_UseTextBoxAsNameSource = true;

                switch (modifyState)
                {
                    case ModifyState.Rename:
                        m_TSBRename.Checked = true;
                        m_CreateMode = false;

                        ModifyEnabled = false;

                        // Enable the Rename button.
                        m_TSBRename.Enabled = true;

                        // Allow the user to save the name change.
                        m_TSBSave.Enabled = true;

                        // Allow the user to edit the workset name.
                        m_TextBoxName.Visible = true;
                        m_TextBoxName.Enabled = true;
                        m_ComboBoxWorkset.Visible = false;
                        break;

                    case ModifyState.Create:
                        m_TSBNew.Checked = true;
                        m_CreateMode = true;

                        ModifyEnabled = true;

                        // Enable the New button.
                        m_TSBNew.Enabled = true;

                        // Display the default name for a new workset.
                        // Ensure that the TextChanged event is not triggered as a result of specifying the Text property of the TextBox control.
                        m_TextBoxName.TextChanged -= new EventHandler(m_TextBoxName_TextChanged);
                        m_TextBoxName.Text = Resources.NameNewWorksetDefault;
                        m_TextBoxName.TextChanged += new EventHandler(m_TextBoxName_TextChanged);

                        // Allow the user to edit the workset name.
                        m_TextBoxName.Visible = true;
                        m_TextBoxName.Enabled = true;
                        m_ComboBoxWorkset.Visible = false;

                        // Show the security level of the current user.
                        m_TextBoxSecurityLevel.Text = Security.Description;

                        CreateNewWorkset();
                        break;
                    case ModifyState.Edit:
                        m_TSBEdit.Checked = true;
                        m_CreateMode = false;

                        ModifyEnabled = true;

                        // Enable the Edit button.
                        m_TSBEdit.Enabled = true;

                        // Display the workset name, however, don't allow the user to edit the name.
                        m_TextBoxName.Visible = true;
                        m_ComboBoxWorkset.Visible = false;
                        break;
                    case ModifyState.Copy:
                        m_TSBCopy.Checked = true;
                        m_CreateMode = true;

                        ModifyEnabled = true;

                        // Enable the Copy button.
                        m_TSBCopy.Enabled = true;

                        // Display the default name of the copied workset.
                        // Ensure that the TextChanged event is not triggered as a result of specifying the Text property of the TextBox control.
                        m_TextBoxName.TextChanged -= new EventHandler(m_TextBoxName_TextChanged);
                        m_TextBoxName.Text = string.Format(Resources.NameWorksetCopy, m_SelectedWorkset.Name);
                        m_TextBoxName.TextChanged += new EventHandler(m_TextBoxName_TextChanged);

                        // Allow the user to edit the workset name.
                        m_TextBoxName.Visible = true;
                        m_TextBoxName.Enabled = true;
                        m_ComboBoxWorkset.Visible = false;

                        // Show the security level of the current user.
                        m_TextBoxSecurityLevel.Text = Security.Description;
                        break;
                    default:
                        break;
                }
            }

            // The Enabled property of the menu option that allows the user to modify the Y axis limits of the individual chart recorder channels is linked 
            // directly to the Enabled property of the ModifyEnabled property of the parent class.
            m_MenuItemChangeChartScaleFactor.Enabled = ModifyEnabled;

            ClearStatusMessage();
        }

        /// <summary>
        /// Convert the current user settings to a workset.
        /// </summary>
        /// <param name="worksetName">The name of the workset.</param>
        /// <returns>The user settings converted to a workset.</returns>
        protected virtual Workset_t ConvertToWorkset(string worksetName)
        {
            Workset_t workset = new Workset_t();

            workset.Name = worksetName;
            workset.SampleMultiple = Workset_t.DefaultSampleMultiple;
            workset.CountMax = m_WorksetCollection.EntryCountMax;
            workset.SecurityLevel = Security.SecurityLevelCurrent;

            #region - [Column] -
            workset.Column = new Column_t[1];
            workset.Column[0].HeaderText = m_TextBoxHeader1.Text;
            workset.Column[0].OldIdentifierList = new List<short>();

            #region - [OldIdentifierList] -
            for (int index = 0; index < m_ListBox1.Items.Count; index++)
            {
                workset.Column[0].OldIdentifierList.Add(((WatchItem_t)m_ListBox1.Items[index]).OldIdentifier);
            }
            #endregion - [OldIdentifierList] -
            #endregion - [Column] -

            #region - [WatchItems] -
            workset.WatchItems = new WatchItem_t[m_WatchItems.Length];
            Array.Copy(m_WatchItems, workset.WatchItems, m_WatchItems.Length);
            #endregion - [WatchItems] -

            #region - [WatchElementList] -
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
            #endregion - [WatchElementList] -

            #region - [Count] -
            workset.Count = workset.WatchElementList.Count;

            if (workset.Count != m_ListItemCount)
            {
                throw new ArgumentException(Resources.EMWorksetIntegrityCheckFailed, "FormWorksetDefineFaultLog.ConvertToWorkset() - [workset.WatchElements.Count]");
            }
            #endregion - [Count] -

            return workset;
        }

        /// <summary>
        /// Clear the items in the 'Column' ListBox controls.
        /// </summary>
        protected virtual void ClearListBoxColumnItems()
        {
            m_ListBox1.Items.Clear();
        }

        /// <summary>
        /// Predicate function called by the <c>List.Find()</c> method to return a workset that matches the specified workset, ignoring the Name, SecurityLevel, 
        /// HeaderText and TabPagePlots fields of each workset. 
        /// </summary>
        /// <param name="workset">The list item that is to be processed.</param>
        /// <returns>True, if the specified item meets the logic requirements given in the function; otherwise false.</returns>
        protected virtual bool CompareWorkset(Workset_t workset)
        {
            Workset_t worksetToCompare = m_WorksetToCompare;

            // ----------------------------------------------------------------------
            // Ignore the name and security level as these are not stored on the VCU.
            // ----------------------------------------------------------------------
            if ((workset.SampleMultiple != worksetToCompare.SampleMultiple) ||
                (workset.CountMax != worksetToCompare.CountMax))
            {
                return false;
            }

            #region - [Column] -
            if (workset.Column.Length != worksetToCompare.Column.Length)
            {
                return false;
            }

            #region - [OldIdentifierList] -
            for (int columnIndex = 0; columnIndex < workset.Column.Length; columnIndex++)
            {
                // --------------------------------------------------------
                // Ignore the header text as this is not stored on the VCU.
                // --------------------------------------------------------

				// Check to be sure both lists have the same number of Old Identifiers.
				// At one point, this could lead to an exception if the user tried to create a Chart Recorder with no items.
				if (workset.Column[columnIndex].OldIdentifierList.Count != worksetToCompare.Column[columnIndex].OldIdentifierList.Count)
				{
					return false;
				}

                for (int index = 0; index < workset.Column[columnIndex].OldIdentifierList.Count; index++)
                {
                    if (workset.Column[columnIndex].OldIdentifierList[index] != worksetToCompare.Column[columnIndex].OldIdentifierList[index])
                    {
                        return false;
                    }
                }
            }
            #endregion - [OldIdentifierList] -
            #endregion - [Column] -

            #region - [WatchItems] -
            if (workset.WatchItems.Length != worksetToCompare.WatchItems.Length)
            {
                return false;
            }

            for (int index = 0; index < workset.WatchItems.Length; index++)
            {
                if (workset.WatchItems[index].Added != worksetToCompare.WatchItems[index].Added)
                {
                    return false;
                }
            }
            #endregion - [WatchItems] -

            #region - [WatchElementList] -
            if (workset.WatchElementList.Count != worksetToCompare.WatchElementList.Count)
            {
                return false;
            }

            for (int index = 0; index < workset.WatchElementList.Count; index++)
            {
                if (workset.WatchElementList[index] != worksetToCompare.WatchElementList[index])
                {
                    return false;
                }
            }
            #endregion - [WatchElementList] -

            #region - [Count] -
            if (workset.Count != worksetToCompare.Count)
            {
                return false;
            }
            #endregion - [Count] -

            return true;
        }

        /// <summary>
        /// Load the specified workset.
        /// </summary>
        /// <param name="workset">The workset that is to be processed.</param>
        protected virtual void LoadWorkset(Workset_t workset)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Ensure that the TextChanged event is not triggered as a result of specifying the Text property of the TextBox control.
            m_TextBoxName.TextChanged -= new EventHandler(m_TextBoxName_TextChanged);
            m_TextBoxName.Text = workset.Name;
            m_TextBoxName.TextChanged += new EventHandler(m_TextBoxName_TextChanged);

            m_TextBoxHeader1.Text = workset.Column[0].HeaderText;

            m_ListItemCount = workset.Count;

            m_WatchItems = new WatchItem_t[workset.WatchItems.Length];
            Array.Copy(workset.WatchItems, m_WatchItems, workset.WatchItems.Length);

            UpdateListBoxAvailable(m_WatchItems);

            // ------------------------------------
            // Update the 'Column' ListBox control.
            // -------------------------------------
            WatchItemAddRange(m_ListBox1, workset.Column[0]);
            UpdateCount();

            m_TextBoxSecurityLevel.Text = Security.GetSecurityDescription(workset.SecurityLevel);
        }

        /// <summary>
        /// Enable the 'Save' button and clear the status message provided at least one watch variable has been selected and the workset name has been
        /// defined; otherwise, disable the 'Save' button and write a status message.
        /// </summary>
        protected override void EnableApplyAndOKButtons()
        {
            // Check whether the watch variable count is within the permitted limits and that the 'm_ModifyState' variable is not undefined. This ensures
            // that the following code is only processed after the form has been instantiated.
            if ((m_ListItemCount <= EntryCountMax) && m_ModifyState != ModifyState.Undefined)
            {
                // Only enable the OK and Apply buttons if the workset has at least one watch variable and a valid workset name has been defined.    
                if (m_ListItemCount > 0)
                {
                    // Yes - The workset contains at least one watch variable.

                    // Check whether the workset name has been defined.
                    if (m_TextBoxName.Text != string.Empty)
                    {
                        // Don't enable the 'Save' Button if in Configuration mode.
                        if (m_ModifyState != ModifyState.Configure)
                        {
                            // Yes - Enable the Save button and clear the status message.
                            m_TSBSave.Enabled = true;

                            ClearStatusMessage();
                        }
                    }
                    else
                    {
                        // No - The workset contains at least one watch variable but the workset name has not been defined. Disable the Apply and OK buttons.
                        m_TSBSave.Enabled = false;
                        WriteStatusMessage(Resources.SMWorksetNameNotDefined);
                    }
                }
                else
                {
                    // No - The workset doesn't contain any watch variables.

                    // check whether the workset name has been defined.
                    string message = string.Empty;
                    if (m_TextBoxName.Text != string.Empty)
                    {
                        message = Resources.SMWorksetWatchCountTooSmall;
                    }
                    else
                    {
                        // The workset must be given a name and contain at least one watch variable. Unfortunately, this status message is too long
                        // to be displayed within the control so it has been changed to Resources.SMWorksetNameNotDefined.
                        message = Resources.SMWorksetNameNotDefined;
                    }

                    m_TSBSave.Enabled = false;
                    WriteStatusMessage(message);
                }

                if (m_WorksetCollection != null)
                {
                    // Check whether the current workset name is already in use provide the user is not in configuration mode.
                    if ((m_ModifyState != ModifyState.Edit) &&
                        (m_ModifyState != ModifyState.Configure) &&
                        (m_WorksetCollection.Contains(m_TextBoxName.Text.Trim()) == true))
                    {
                        m_TSBSave.Enabled = false;
                        WriteStatusMessage(string.Format(Resources.SMWorksetNameExists, m_TextBoxName.Text));
                    }
                }
            }
        }

        /// <summary>
        /// Compare the two worksets, ignoring the Name, SecurityLevel, HeaderText and TabPagePlots fields of each workset.
        /// </summary>
        /// <param name="worksetA">The first workset that is to be compared.</param>
        /// <param name="worksetB">The second workset that is to be compared.</param>
        /// <returns>True, if the worksets are identical; otherwise, false.</returns>
        protected bool CompareWorkset(Workset_t worksetA, Workset_t worksetB)
        {
            m_WorksetToCompare = worksetB;
            return CompareWorkset(worksetA);
        }

        /// <summary>
        /// Set the Enabled property of the Edit, New, Copy and Rename buttons to the specified state. If the specified state is true, then the user must have 
        /// sufficient privileges in order to enable the edit and rename buttons.
        /// </summary>
        /// <remarks>The member variable <c>m_SelectedWorkset</c> must be initialized before calling this method.</remarks>
        /// <param name="enabled">The required state of the Enabled property.</param>
        protected void SetEnabledEditNewCopyRename(bool enabled)
        {
            m_TSBSave.Enabled = false;

            m_TSBNew.Enabled = enabled;
            m_TSBCopy.Enabled = enabled;

            if ((enabled == true) && (Security.SecurityLevelCurrent >= m_SelectedWorkset.SecurityLevel))
            {
                m_TSBEdit.Enabled = true;
                m_TSBRename.Enabled = true;
                return;
            }

            m_TSBEdit.Enabled = false;
            m_TSBRename.Enabled = false;
            return;
        }

        /// <summary>
        /// Predicate function called by the <c>List.Find()</c> method to return a workset that matches the Name variable of the m_WorksetToCompare workset.
        /// </summary>
        /// <param name="workset">The workset list item that is to be processed.</param>
        /// <returns>True if the specified item meets the logic requirements given in the function; otherwise false.</returns>
        private bool CompareName(Workset_t workset)
        {
            bool match = false;
            Workset_t worksetToCompare = m_WorksetToCompare;

            if (worksetToCompare.Name == workset.Name)
            {
                match = true;
            }

            return match;
        }

        /// <summary>
        /// Compare the current chart recorder channel settings with the workset that was downloaded from the VCU and return a flag that indicates whether the current 
        /// settings are different from the downloaded workset.
        /// </summary>
        /// <returns></returns>
        private bool ConfigurationModified()
        {
            bool configurationModified = true;

            // Convert the current user settings to a workset.
            Workset_t workset = ConvertToWorkset(string.Empty);

            // Check whether the workset associated with the current settings matches the workset that was initially downloaded from the VCU.
            if (CompareWorkset(workset, m_WorksetFromVCU) == true)
            {
                configurationModified = false;
            }

            return configurationModified;
        }

        /// <summary>
        /// Create a new, empty chart recorder/fault log workset.
        /// </summary>
        private void CreateNewWorkset()
        {
            // WatchItem - populate the array defining which watch variables have been added to the workset.
            m_WatchItems = new WatchItem_t[Lookup.WatchVariableTableByOldIdentifier.RecordList.Count];
            WatchItem_t watchItem;
            WatchVariable watchVariable;
            for (short oldIdentifier = 0; oldIdentifier < Lookup.WatchVariableTableByOldIdentifier.RecordList.Count; oldIdentifier++)
            {
                watchItem = new WatchItem_t();
                watchItem.OldIdentifier = oldIdentifier;
                watchItem.Added = false;

                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];

                    if (watchVariable == null)
                    {
                        watchItem.Exists = false;
                    }
                    else
                    {
                        watchItem.Exists = true;
                    }
                }
                catch (Exception)
                {
                    watchItem.Exists = false;
                }

                m_WatchItems[oldIdentifier] = watchItem;
            }

            UpdateListBoxAvailable(m_WatchItems);
            ClearListBoxColumnItems();
            m_ListItemCount = 0;
            UpdateCount();
        }

        /// <summary>
        /// Add the worksets contained within the specified workset collection to the <c>Items</c> property of the specified <c>ComboBox</c> control.
        /// </summary>
        /// <param name="comboBox">The <c>ComboBox</c> control that it to be processed.</param>
        /// <param name="worksetCollection">The workset collection containing the worksets that are to be added.</param>
        private void ComboBoxAddWorksets(ComboBox comboBox, WorksetCollection worksetCollection)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Debug.Assert(worksetCollection != null, "FormConfigure.ComboBoxAddWorksets() - [worksetCollection != null]");
            Debug.Assert(comboBox != null, "FormConfigure.ComboBoxAddWorksets() - [comboBox != null]");

            comboBox.Items.Clear();

            // Get the list of available worksets, the first entry should be the active workset. An entry should not be created for the baseline workset unless
            // it is defined as the active workset.
            for (int itemIndex = 0, worksetIndex = worksetCollection.ActiveIndex; itemIndex < worksetCollection.Worksets.Count; itemIndex++)
            {
                if (worksetCollection.ActiveIndex != 0)
                {
                    // Baseline workset is NOT the active workset, therefore don't create an entry for it.
                    if (worksetIndex == 0)
                    {
                        worksetIndex++;
                        worksetIndex %= worksetCollection.Worksets.Count;
                        continue;
                    }
                }

                comboBox.Items.Add(worksetCollection.Worksets[worksetIndex++]);

                // Add the remaining worksets, using a round-robin approach.
                worksetIndex %= worksetCollection.Worksets.Count;
            }
        }

        /// <summary>
        /// Clear the Checked property of the Edit, New, Copy and Rename buttons to the specified state.
        /// </summary>
        private void ClearCheckedEditNewCopyRename()
        {
            m_TSBEdit.Checked = false;
            m_TSBNew.Checked = false;
            m_TSBCopy.Checked = false;
            m_TSBRename.Checked = false;
        }
        #endregion --- Methods ---
    }
}