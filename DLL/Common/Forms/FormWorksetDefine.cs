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
 *  File name:  FormWorksetDefine.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  
 */
/*
 *  05/19/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/16/10    1.1     K.McD           1.  Included a try/catch block in the constructor in case an exception is thrown while the form is being viewed in the 
 *                                          Visual Studio designer.
 * 
 *  11/26/10    1.2     K.McD           1.  Included support for the DataUpdate interface.
 *                                      2.  Made a number of methods virtual to support inheritance. 
 *                                      3.  Renamed a number of methods and variables.
 * 
 *  01/06/11    1.3     K.McD           1.  Modified the signature of the GetWatchItems() method so that the record count of the current data dictionary is passed as a
 *                                          parameter.
 * 
 *  02/28/11    1.4     K.McD           1.  Added the TextBox control to show the security level of the current workset.
 *                                      2.  Moved the location of the GetWatchItems() method.
 * 
 *  03/18/11    1.5     K.McD           1.  Added support for the WinHlp32 help engine to display the help diagnostic information for the watch variables.
 * 
 *  03/28/11    1.6     K.McD           1.  Major modifications to use the old identifier field of the watch variables stored in the workset.
 *                                      2.  Added a try/catch block to the Remove() method in case the watch item associated with the specified old identifier is null. 
 *                                      3.  Implemented the Save() method as this is no longer overwritten in the child classes.
 *                                      4.  Removed the GetWatchItems() method.
 * 
 *  04/27/11    1.7     K.McD           1.  Included support for the ModifyEnabled property.
 *                                      2.  Modified a number of XML tags.
 *                                      3.  Auto-modified as a result of a name change to an inherited member variable.
 *                                      4.  Renamed a number of event handlers to be compatible with the coding standards.
 * 
 *  05/23/11    1.8     K.McD           1.  Significant modifications to support the child classes associated with the definition of chart recorder worksets:
 *  
 *                                          a.  Added the AddSupplementalFields() and RemoveSupplementalFields() methods to add/remove any additional workset fields
 *                                              defined in a child class.
 * 
 *                                          b.  Modified the Add()/Remove() and m_ListBoxTarget_DragDrop() methods to accommodate the new methods defined in item 1.
 * 
 *                                          c.  Made the m_ButtonMoveUp_Click() and m_ButtonMoveDown_Click() methods virtual so that any supplemental fields defined in 
 *                                              the child class may be accommodated.
 * 
 *                                          d.  Added the m_MenuItemChangeChartScaleFactor_Click() virtual method to allow the user to configure the chart recorder 
 *                                              scaling information in the chart recorder worksets.
 *                                              
 *                                      2.  Applied the 'Organize Usings/Remove and Sort' context menu.
 *                                      
 *  06/21/11    1.9     K.McD           1.  Modified the name of a protected member variable.
 *                                      2.  Re-ordered a number of the class methods.
 *                                      3.  Made the following methods virtual so that the logic can be modified in any child classes:
 *                                      
 *                                              a.  m_MenuItemShowDefinitionColumn_Click()
 *                                              b.  m_MenuItemShowDefinitionAll_Click()
 *                                              c.  Add()
 *                                              d.  Remove() 
 *                                              
 *                                      4.  Renamed a local variable.
 *                                      
 *  08/05/11    1.9.1   K.McD           1.  Corrected the XML tag associated with the ModifyEnabled property.
 *                                      
 *	09/26/11	1.9.2	Sean.D/K.McD    1.	Added the event handler m_TextBoxName_TextChanged to disable the OK Button when the text in the TextBox control either 
 *	                                        matches the name of an existing workset or is empty. The event handler also displays a warning message if the specified 
 *	                                        text matches an existing workset.
 *	                                    2.  Corrected a number of XML tags and comments and renamed a number of: local variables, parameters and methods.
 *	                                    3.  Changed the modifiers of a number of methods.
 *	                                    4.  Modified the WatchItemAddRange() method to initialize the DisplayMask field of the WatchItem_t structure.
 *	                                    5.  Added a number of virtual methods associated with the context menus to support child classes.
 *                                      
 *  10/10/11    1.9.3   K.McD           1.  bug fix. Ensured that the status message label is only visible if the text in the text box matches an existing 
 *                                          workset name.
 *                                          
 *	10/12/11	1.9.4	Sean.D			1.	Added the ClearStatusMessage(), DisableApplyButton(), DisableOKButton(), and ReenableApplyAndOKButtons() methods.
 *										2.	Modified constructors to call ClearStatusMessage to ensure that the status message stays clear. I was getting the 
 *											occasional anomalous result where the error icon started displayed.
 *										3.	Modified the constructor which took in a workset to disable the Apply button if there are more variables than 
 *											we can handle.
 *										4.	Added an EntryMax property to track the maximum number of allowed entries, by default based on the EntryMax of the 
 *											current workset.
 *										5.	Modified m_ListBoxTarget_DragDrop method to refer to the EntryMax property rather than the EntryMax of the workset directly.
 *										
 *  10/25/11    1.9.5   K.McD           1.  Re-organized the layout of the file i.e. location of a number of methods.
 *                                      2.  Modified the names of a number of methods.
 *                                      3.  Made the status message icon a separate Label control and included this and the original status label within a Panel control 
 *                                          to improve layout of multi-line messages.
 *                                      4.  Removed the redundant call to the ReenableApplyAndOKButtons() method from the m_ButtonRemove_Click() method.
 *                                      5.  Replaced direct text to resource references in the constructor.
 *                                      
 *  10/26/11    1.9.6   K.McD           1.  Changed the modifier of the ClearStatusMessage() method to protected and added the WriteStatusMessage() method to allow 
 *                                          child classes to write status messages.
 *                                          
 * 
 *  06/04/12    1.9.7   Sean.D          1.  Added the Protected m_RemovedItems member variable to track what items we've removed so that they can be re-added if the
 *                                          dialog is canceled.
 *                                      2.  Modified m_ButtonCancel_Click to re-add removed items if the Cancel button is pressed.
 *                                      3.  Modified the Add method to properly track the removed items.
 *                                      
 *  07/24/13    1.10    K.McD           1.  Allow the EntryCountMax property to be updated by child classes by adding the, protected, m_EntryCountMax variable,
 *                                          initializing this to m_WorksetCollection.EntryCountMax in the constructors and defining the EntryCountMax property to return
 *                                          this value rather than m_WorksetCollection.EntryCountMax. 
 *                                          
 *                                          This requirement is only associated with projects that do not record a fixed number of watch variables for each event log.
 *                                          Under these circumstances, the EntryCountMax property is set to the number of watch variables associated with the event log
 *                                          that can record the largest number of watch variables. Consequently, the list of fault log worksets will consist of worksets
 *                                          associated with each datastream type. When the user configures the fault log parameters, using a child of this form, it is
 *                                          possible that a workset is selected that exceeds the permitted number of watch variables for the current event log, however, 
 *                                          by allowiing the form to update the EntryCountMax property it is possible to manage this much better.
 */
/*
 *   03/17/15    1.11   K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                              1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                                  options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                                  
 *                                              2.  MOC-0171-28. A check will be included as part of the ‘Save’ procedure to ensure that an empty workset cannot be saved.
 *                                          
 *                                                  As a result of a review of the software, it is proposed that it should only be possible to save a workset if the
 *                                                  following criteria are met:
 *                                              
 *                                                      1.  The workset must contain at least one watch variable.
 *                                                      2.  The workset must have a valid name that is not currently in use.
 *                                                      
 *                                      2.  SNCR - R188 PTU [20 Mar 2015] Item 3. Menu option 'Configuration/Worksets/Data Stream'. If, while editing a workset, the user
 *                                          removes one or more watch variables from the workset and then selects the 'Cancel' button; when the workset is re-opened,
 *                                          the 'Available' ListBox contains the watch variables that were previosly removed from the workset.
 *                                          
 *                                      Modifications
 *                                      1.  Ref.: 1.1.1. Changed the form title to '"Create / Modify Workset' in the InitializeComponent() method.
 *                                      2.  Ref.: 1.1.2.
 *                                              1.  De-registered the 'Windows Form Designer' generated events in the Cleanup() method.
 *                                              2.  Modified the non zero parameter constructors to ensure that the TextChanged event handler is not called
 *                                                  when the Text property of the  'm_TextBoxName' TextBox is set by the constructor.
 *                                              3.  Modified the 'Create' and 'Edit' constructors to call the EnableApplyAndOKButtons() method rather than the
 *                                                  ClearStatusMessage() method.
 *                                              4.  Modified the TextChanged() method to simply call the EnableApplyAndOKButtons() method.
 *                                              5.  Changed the modifiers of the DisableApplyAndOKButtons() and EnableApplyAndOKButtons() methods to protected and
 *                                                  made the EnableApplyAndOKButtons() method virtual.
 *                                              6.  Modified the EnableApplyAndOKButtons() method from simply enabling the Apply and OK buttons to checking the
 *                                                  status of the workset i.e. does it have a valid name, is the name in use, does it have at least one watch
 *                                                  variable and setting the Enabled property of the Apply and OK buttons accordingly.
 *                                      3.  Ref.: 2.
 *                                              1.  Renamed the 'm_RemovedWatchVariables' ArrayList to 'm_AddedWatchVariables'.
 *                                              2.  Added the m_RemovedWatchVariables ArrayList to track any watch variables that are removed from the
 *                                                  workset. If the user selects the 'Cancel' button, these watch variables can then be safely restored to
 *                                                  the workset.
 *                                              3.  Modified the m_ButtonCancel_Click() method to safely restore the workset back to its initial contents.
 *                                              4.  Modified the Remove() method to properly track the watch variables that are removed from the workset.
 *                                              5.  Modified the m_ListBoxTarget_DragDrop() method to track the watch variables that have been added to the workset.
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Configuration;
using Common.Properties;

namespace Common.Forms
{
    /// <summary>
    /// Form to allow the user to define: (a) the watch variables that are associated with a workset and (b) the order and column in which they are to be displayed.
    /// </summary>
    public partial class FormWorksetDefine : FormPTUDialog, IDataUpdate
    {
        #region --- Events ---
        /// <summary>
        /// Occurs whenever the workset is updated.
        /// </summary>
        public event EventHandler DataUpdate;
        #endregion --- Events ---

        #region --- Constants ---
        /// <summary>
        /// The maximum number of characters associated with the header text. Value: 50.
        /// </summary>
        protected const int HeaderLengthMax = 50;
        #endregion --- Constants ---

        #region --- Member Variables ---
        #region - [DragDrop Variables] -
        /// <summary>
        /// The screenOffset is used to account for any desktop bands that may be at the top or left side of the screen when determining when to cancel the drag-drop 
        /// operation.
        /// </summary>
        private Point m_ScreenOffset;

        /// <summary>
        /// Used to determine if a drag-drop operation has been started. If the mouse moves outside of this rectangle with the left-mouse key still depressed then
        /// a drag-drop operation has been deemed to have started.
        /// </summary>
        private Rectangle m_DragBoxFromMouseDown;
        #endregion - [DragDrop Variables] -
        
        /// <summary>
        /// Flag indicating whether the form is to be used to: (a) create a new workset or (b) edit an existing workset. True, indicates create new workset; otherwise, 
        /// false to indicate edit an existing workset.
        /// </summary>
        protected bool m_CreateMode;

        /// <summary>
        /// An array of all watch variables contained within the data dictionary, sorted by watch identifer. Used to keep track of which watch identifiers have been 
        /// added to the workset.
        /// </summary>
        protected WatchItem_t[] m_WatchItems;

        /// <summary>
        /// The combined number of list items associated with the target <c>ListBox</c> controls.
        /// </summary>
        protected int m_ListItemCount = 0;

        /// <summary>
        /// The workset collection that is to be managed.
        /// </summary>
        protected WorksetCollection m_WorksetCollection;

        /// <summary>
        /// A reference to the <c>ListBox</c> associated with the currently selected 'Column' <c>TabPage</c>.
        /// </summary>
        protected ListBox m_ListBoxSelected;

        /// <summary>
        /// A flag that controls whether the user is able to modify the current workset. True, to allow the user to modify the workset; otherwise, false.
        /// </summary>
        private bool m_ModifyEnabled = true;

        /// <summary>
        /// The default name for any new workset that is created.
        /// </summary>
        protected readonly string m_DefaultNewWorksetName;

		/// <summary>
		/// A list of the watch variables that have been added to the workset. Used to correct any changes made by the user if the Cancel button is selected.
		/// </summary>
        /// <remarks>If the code gets changed to not close the form when clicking OK or Apply, this could become an issue because we are not currently explicitly
        /// clearing the list in such cases.</remarks>
		protected System.Collections.ArrayList m_AddedWatchVariables = new System.Collections.ArrayList();

        /// <summary>
        /// A list of the watch variables that have been removed from the workset. Used to correct any changes made by the user if the Cancel button is selected.
        /// </summary>
        /// <remarks>If the code gets changed to not close the form when clicking OK or Apply, this could become an issue because we are not currently explicitly
        /// clearing the list in such cases.</remarks>
        protected System.Collections.ArrayList m_RemovedWatchVariables = new System.Collections.ArrayList();

        /// <summary>
        /// The maximum number of watch variables that the workset can support.
        /// </summary>
        protected int m_EntryCountMax;
		#endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes an new instance of the form. Zero parameter constructor, required by Visual Studio.
        /// </summary>
        public FormWorksetDefine()
        {
            InitializeComponent();

            ClearStatusMessage();
        }

        /// <summary>
        /// Initializes an new instance of the form. This constructor is used when a new workset is being created. Populates the 'Available' 
        /// <c>ListBox</c> controls with the appropriate watch variables.
        /// </summary>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        public FormWorksetDefine(WorksetCollection worksetCollection)
        {
            InitializeComponent();

            m_WorksetCollection = worksetCollection;
            m_EntryCountMax = m_WorksetCollection.EntryCountMax;

            // Set the flag to indicate that the form has been called in order to add a new workset.
            m_CreateMode = true;

            // Use the default new name for the workset.
            m_DefaultNewWorksetName = Resources.NameNewWorksetDefault;

            // Ensure that the TextChanged event is not triggered as a result of specifying the Text property of the TextBox control.
            m_TextBoxName.TextChanged -= new EventHandler(m_TextBoxName_TextChanged);
            m_TextBoxName.Text = m_DefaultNewWorksetName;
            m_TextBoxName.TextChanged += new EventHandler(m_TextBoxName_TextChanged);

            // Update the security description to reflect the current security level.
            m_TextBoxSecurityLevel.Text = Security.Description;
            
            // ---------
            // WatchItem
            // ---------
            // Populate the array defining which watch variables have been added to the workset.
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
                catch(Exception)
                {
                    watchItem.Exists = false;
                }

                m_WatchItems[oldIdentifier] = watchItem;
            }

            UpdateListBoxAvailable(m_WatchItems);
            UpdateCount();

            // Update the m_ListBoxSelected reference to point at the ListBox control associated with the default TabPage.
            m_ListBoxSelected = m_ListBox1;
            
            // ----------------------------
            // OK, Cancel and Apply buttons.
            // ----------------------------
            // Hide the Apply button in this mode and move the position of the OK and Cancel buttons.
            m_ButtonApply.Visible = false;
            m_ButtonOK.Location = m_ButtonCancel.Location;
            m_ButtonCancel.Location = m_ButtonApply.Location;

            EnableApplyAndOKButtons();
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
        public FormWorksetDefine(WorksetCollection worksetCollection, Workset_t workset, bool applyVisible)
        {
            InitializeComponent();

            m_WorksetCollection = worksetCollection;
            m_EntryCountMax = m_WorksetCollection.EntryCountMax;

            // Set the flag to indicate that the form has been called in order to edit a new workset.
            m_CreateMode = false;

            // Ensure that the TextChanged event is not triggered as a result of specifying the Text property of the TextBox control.
            m_TextBoxName.TextChanged -= new EventHandler(m_TextBoxName_TextChanged);
            m_TextBoxName.Text = workset.Name;
            m_TextBoxName.TextChanged += new EventHandler(m_TextBoxName_TextChanged);
            
            m_TextBoxName.Enabled = false;

            // Update the security description to reflect the security level of the selected workset.
            m_TextBoxSecurityLevel.Text = Security.GetSecurityDescription(workset.SecurityLevel);
            
            m_ListItemCount = workset.Count;
            m_WatchItems = workset.WatchItems;

            UpdateListBoxAvailable(m_WatchItems);

            // Update the m_ListBoxSelected reference to point at the ListBox control associated with the default TabPage.
            m_ListBoxSelected = m_ListBox1;

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
            }

            // Check whether the watch variable count is within the permitted limits.
            if (m_ListItemCount > EntryCountMax)
            {
                // Disable the Apply and OK buttons and show the specified error message.
                DisableApplyAndOKButtons(Resources.MBTWorksetWatchSizeExceeded);
            }
            else
            {
                // Don't enable the OK and Apply buttons until the workset has been modified.
                DisableApplyAndOKButtons(string.Empty);
            }
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

                    if (m_ListBoxSelected != null)
                    {
                        m_ListBoxSelected.ClearSelected();
                        m_ListBox1.Dispose();
                    }

                    if (m_AddedWatchVariables != null)
                    {
                        m_AddedWatchVariables.Clear();
                    }

                    if (m_RemovedWatchVariables != null)
                    {
                        m_RemovedWatchVariables.Clear();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                m_ListBoxSelected = null;
                m_WatchItems = null;
                m_WorksetCollection = null;
                m_AddedWatchVariables = null;
                m_RemovedWatchVariables = null;

                #region --- Windows Form Designer Variables ---
                // Detach the event handler delegates.
                this.m_MenuItemShowDefinitionAll.Click -= new System.EventHandler(this.m_MenuItemShowDefinitionAll_Click);
                this.m_ContextMenuColumns.Opened -= new System.EventHandler(this.m_ContextMenuColumns_Opened);
                this.m_MenuItemShowDefinition.Click -= new System.EventHandler(this.m_MenuItemShowDefinitionColumn_Click);
                this.m_MenuItemChangeChartScaleFactor.Click -= new System.EventHandler(this.m_MenuItemChangeChartScaleFactor_Click);
                this.m_MenuItemConfigureBitmaskPlot.Click -= new System.EventHandler(this.m_MenuItemConfigureBitmaskPlot_Click);
                this.m_ButtonCancel.Click -= new System.EventHandler(this.m_ButtonCancel_Click);
                this.m_TextBoxName.TextChanged -= new System.EventHandler(this.m_TextBoxName_TextChanged);
                this.m_TabControlColumn.SelectedIndexChanged -= new System.EventHandler(this.m_TabControlColumns_SelectedIndexChanged);
                this.m_TextBoxHeader1.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.TextBoxColumnHeader_KeyPress);
                this.m_ListBox1.DragDrop -= new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragDrop);
                this.m_ListBox1.DragEnter -= new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragEnter);
                this.m_TextBoxHeader2.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.TextBoxColumnHeader_KeyPress);
                this.m_ListBox2.DragDrop -= new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragDrop);
                this.m_ListBox2.DragEnter -= new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragEnter);
                this.m_TextBoxHeader3.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.TextBoxColumnHeader_KeyPress);
                this.m_ListBox3.DragDrop -= new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragDrop);
                this.m_ListBox3.DragEnter -= new System.Windows.Forms.DragEventHandler(this.m_ListBoxTarget_DragEnter);
                this.m_ButtonMoveUp.Click -= new System.EventHandler(this.m_ButtonMoveUp_Click);
                this.m_ButtonMoveDown.Click -= new System.EventHandler(this.m_ButtonMoveDown_Click);
                this.m_ButtonRemove.Click -= new System.EventHandler(this.m_ButtonRemove_Click);
                this.m_ButtonClear.Click -= new System.EventHandler(this.m_ButtonClear_Click);
                this.m_TextBoxSearch.TextChanged -= new System.EventHandler(this.m_TxtSearch_TextChanged);
                this.m_ListBoxAvailable.DoubleClick -= new System.EventHandler(this.m_ButtonAdd_Click);
                this.m_ListBoxAvailable.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseDown);
                this.m_ListBoxAvailable.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseMove);
                this.m_ListBoxAvailable.MouseUp -= new System.Windows.Forms.MouseEventHandler(this.m_ListBoxSource_MouseUp);
                this.m_ButtonAdd.Click -= new System.EventHandler(this.m_ButtonAdd_Click);
                this.m_ButtonApply.Click -= new System.EventHandler(this.m_ButtonApply_Click);
                this.m_ButtonOK.Click -= new System.EventHandler(this.m_ButtonOK_Click);
                this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.FormWorksetDefine_KeyDown);


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
        #region - DragDrop -
        /// <summary>
        /// Event handler for the <c>DragDrop</c> event associated with the target <c>ListBox</c>. The drag-drop operation is complete, move the selected items.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ListBoxTarget_DragDrop(object sender, DragEventArgs e)
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

            if ((m_ListItemCount + count) > EntryCountMax)
            {
                MessageBox.Show(Resources.MBTWorksetDefineMaxExceeded, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

            // Remember the selected item list contents will change every time an item is deleted. Best approach is
            // to repeatedly remove selected item collection index = 0 by the number of items added to the destination list.
            for (int index = 0; index < count; ++index)
            {
                // Keep track of these changes in case the user selects the cancel key.
                m_AddedWatchVariables.Add(selectedObjectCollection[0]);

                m_ListBoxAvailable.Items.Remove(selectedObjectCollection[0]);
            }

            UpdateCount();

            // Scroll to the end of the list.
            m_ListBoxSelected.SetSelected(m_ListBoxSelected.Items.Count - 1, true);
            m_ListBoxSelected.ClearSelected();

            EnableApplyAndOKButtons();
            OnDataUpdate(this, new EventArgs());
        }

        /// <summary>
        /// Event handler for the <c>MouseDown</c> event associated the source <c>ListBox</c>. Keep a record of the drag-drop rectangle for the current mouse
        /// coordinates to determine if a drag-drop operation is about to take place.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_ListBoxSource_MouseDown(object sender, MouseEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedItems.Count > 0)
            {
                // Remember the point where the mouse down occurred. The DragSize indicates the size that the mouse can move before a drag event should be started.
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being at the center of the rectangle.
                m_DragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                m_DragBoxFromMouseDown = Rectangle.Empty;
            }
        }

        /// <summary>
        /// Event handler for the <c>MouseUp</c> event associated with the source <c>ListBox</c>. Reset the drag-drop operation.
        /// </summary>
         /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_ListBoxSource_MouseUp(object sender, MouseEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Reset the drag rectangle when the mouse button is raised.
            m_DragBoxFromMouseDown = Rectangle.Empty;
        }

        /// <summary>
        /// Event handler for the <c>MouseMove</c> event associated with the source <c>ListBox</c>. Check if a drag-drop operation has been started and, if so, call
        /// the DoDragDrop() method of the <c>ListBox</c>.
        /// </summary>
         /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_ListBoxSource_MouseMove(object sender, MouseEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ListBox listBox = (ListBox)sender;
            
            // Check that the left mouse butten is still depressed.
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if ((m_DragBoxFromMouseDown != Rectangle.Empty) && (m_DragBoxFromMouseDown.Contains(e.X, e.Y) == false))
                {
                    // The screenOffset is used to account for any desktop bands that may be at the top or left side of the screen when 
                    // determining when to cancel the drag drop operation.
                    m_ScreenOffset = SystemInformation.WorkingArea.Location;
                    
                    DragDropEffects dropEffect = listBox.DoDragDrop(listBox.SelectedItems, DragDropEffects.Move);
                }
            }
        }

        /// <summary>
        /// Evnt handler for the <c>DragEnter</c> event associated with the target <c>ListBox</c>.
        /// </summary>
       /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ListBoxTarget_DragEnter(object sender, DragEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (e.Data.GetDataPresent(typeof(ListBox.SelectedObjectCollection)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        #endregion - DragDrop -

        #region - [Buttons] -
        /// <summary>
        /// Event handler for the OK button <c>Click</c> event. Add the defined workset to the to list of available worksets.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ButtonOK_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_ButtonApply.Enabled == true)
            {
                m_ButtonApply_Click(this, new EventArgs());
            }

            Close();
        }

        /// <summary>
        /// Event handler for the Cancel button <c>Click</c> event. Close the form and restore the <c>m_WatchItems</c> array to its initial state.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // The 'm_WatchItems' array need only be reverted back to its initial state if the form is being used to modify an existing workset.
            if (m_CreateMode == false)
            {
                // Clear the 'Added' properties of any 'WatchItem_t' elements of the m_WatchItems array that were added to the workset prior to the 'Cancel'button being
                // selected.
                foreach (WatchItem_t watchItem in m_AddedWatchVariables)
                {
                    short oldIdentifier = watchItem.OldIdentifier;

                    // Mark that it is no longer being used.
                    m_WatchItems[oldIdentifier].Added = false;
                }

                // Set the 'Added' properties of any 'WatchItem_t' elements of the m_WatchItems array that were removed from the workset prior to the 'Cancel'button being
                // selected.
                foreach (WatchItem_t watchItem in m_RemovedWatchVariables)
                {
                    short oldIdentifier = watchItem.OldIdentifier;

                    // Mark that it is in use.
                    m_WatchItems[oldIdentifier].Added = true;
                }

                m_AddedWatchVariables.Clear();
                m_RemovedWatchVariables.Clear();
            }

            Close();
        }

        /// <summary>
        /// Event handler for the Apply button <c>Click</c> event. Add the defined workset to the to list of available worksets.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ButtonApply_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Event handler associated with the 'Move Up' button <c>Click</c> event. Move the selected item up the list.
        /// </summary>
        /// <remarks>Only one item may be selected at a time. If more than one item is selected the action will be ignored.</remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ButtonMoveUp_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ListBox.SelectedIndexCollection selectedIndices = m_ListBoxSelected.SelectedIndices;
            int index;
            for (int selection = 0; selection < selectedIndices.Count; ++selection)
            {
                index = selectedIndices[selection];

                // Bounds checking.
                if (index >= 1)
                {
                    object moveItem = m_ListBoxSelected.Items[index];
                    object previousItem = m_ListBoxSelected.Items[index - 1];
                    m_ListBoxSelected.Items.Remove(previousItem);
                    m_ListBoxSelected.Items.Insert(index, previousItem);
                    m_ListBoxSelected.SetSelected(index - 1, true);
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
        protected virtual void m_ButtonMoveDown_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ListBox.SelectedIndexCollection selectedIndices = m_ListBoxSelected.SelectedIndices;
            int index;
            for (int selection = selectedIndices.Count - 1; selection >= 0; --selection)
            {
                index = selectedIndices[selection];
                // Bounds checking.
                if (index < m_ListBoxSelected.Items.Count - 1)
                {
                    object moveItem = m_ListBoxSelected.Items[index];
                    m_ListBoxSelected.Items.Remove(moveItem);
                    m_ListBoxSelected.Items.Insert(index + 1, moveItem);
                    m_ListBoxSelected.SelectedItem = moveItem;
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
        /// Event handler for the 'Clear Search' button <c>Click</c> event. Clears the search textbox.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonClear_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_TextBoxSearch.Text = string.Empty;
            m_ButtonClear.Visible = false;
        }

        /// <summary>
        /// Event handler for the Add button <c>Click</c> event. Add the selected watch variables to the workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_ButtonAdd_Click(object sender, EventArgs e)
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

            Add(ref m_ListBoxAvailable, ref m_ListBoxSelected);
        }

        /// <summary>
        /// Event handler for the Remove button <c>Click</c> event. Remove the selected watch variables from the workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_ButtonRemove_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Remove(ref m_ListBoxSelected, ref m_ListBoxAvailable);
        }
        #endregion - [Buttons] -

        #region - [TabControl] -
        /// <summary>
        /// Event handler for the 'Column' <c>TabControl</c> <c>SelectedIndexChanged</c> event. Activate the <c>ListBox</c> control associated with the 
        /// selected tab page. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TabControlColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            switch (m_TabControlColumn.SelectedIndex)
            {
                case 0:
                    m_ListBoxSelected = m_ListBox1;
                    break;
                case 1:
                    m_ListBoxSelected = m_ListBox2;
                    break;
                case 2:
                    m_ListBoxSelected = m_ListBox3;
                    break;
                default:
                    break;
            }
        }
        #endregion - [TabControl] -

        #region - [Key Events] -
        /// <summary>
        /// Event handler for key events. If the user has pressed the delete key, removes the selected item from the list that currently has focus (source) and moves it 
        /// to the curently active destination list.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormWorksetDefine_KeyDown(object sender, KeyEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Delete:
                    // Skip, if the user is not allowed to modify the workset.
                    if (ModifyEnabled == false)
                    {
                        return;
                    }

                    if (m_ListBoxAvailable.Focused)
                    {
                        m_ButtonAdd_Click(sender, new EventArgs());
                    }
                    else if (m_ListBoxSelected.Focused)
                    {
                        m_ButtonRemove_Click(sender, new EventArgs());
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion - [Key Events] -

        #region - [TextBox] -
        /// <summary>
        /// Event handler for the column header TextBox controls <c>KeyPress</c> event. Checks if the user entered a [CR] and, if so, moves the focus to the next
        /// <c>TextBox</c> control on the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void TextBoxColumnHeader_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (e.KeyChar.Equals('\r'))
            {
                // Reference to the TextBox which raised the event.
                TextBox textBox = (TextBox)sender;

                // If TextBox is multi-line, ignore the [CR].
                if (!textBox.Multiline)
                {
                    // Create a new CancelEventArgs object and validate the user input.
                    System.ComponentModel.CancelEventArgs cancelEventArgs = new System.ComponentModel.CancelEventArgs();
                    TextBox_Validating(sender, cancelEventArgs);
                }
            }

            EnableApplyAndOKButtons();
        }

        /// <summary>
        /// Event handler for ALL TextBox <c>Validating</c> events. Validates the user entry and warns the user, using an ErrorProvider control, if
        /// the value entered is outside the valid range.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void TextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Reference to the TextBox which raised the event.
            TextBox textBox = (TextBox)sender;

            m_ErrorProvider.SetIconAlignment((Control)sender, ErrorIconAlignment.MiddleRight);
            if (textBox.Text.Length > HeaderLengthMax)
            {
                m_ErrorProvider.SetError((Control)sender, "Must not exceed " + textBox.Tag.ToString() + " characters.");

                // Don't allow the user to continue until data entry is valid.
                e.Cancel = true;
            }
            else
            {
                m_ErrorProvider.Clear();
            }

            OnDataUpdate(this, new EventArgs());
        }

        /// <summary>
        /// Event handler for the 'Search' TextBox <c>TextChanged</c> event. Filters the list of available watch variable names using the text entered into the 
        /// 'Search' text box.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            UpdateListBoxAvailable(m_WatchItems);
            m_ButtonClear.Visible = true;
        }

        /// <summary>
        /// Event handler for the 'Name' TextBox <c>TextChanged</c> event. If the form is in create mode, check whether the specified workset name exists and if so, 
        /// prevent the user from saving the workset by disabling the OK button.
        /// </summary>
        /// <remarks>As the Apply button is only visible when editing an existing workset, the enabled state of the Apply button is not modified by this method.</remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_TextBoxName_TextChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            EnableApplyAndOKButtons();
        }
        #endregion - [TextBox] -

        #region - [Context Menu] -
        /// <summary>
        /// Event handler for the context menu <c>Opened</c> event. The logic is defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ContextMenuColumns_Opened(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Show Definition' context menu option attached to the <c>ListBox</c> containing the watch 
        /// variables that have been added to the workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_MenuItemShowDefinitionColumn_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_ListBoxSelected.SelectedItems.Count == 1)
            {
                int oldIdentifier = ((WatchItem_t)m_ListBoxSelected.SelectedItem).OldIdentifier;

                // Get the help index associated with the watch variable.
                WatchVariable watchVariable;
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                    if (watchVariable != null)
                    {
                        int helpIndex = watchVariable.HelpIndex;

                        // If the help index exists, show the help topic associated with the index.
                        if (helpIndex != CommonConstants.NotFound)
                        {
                            WinHlp32.ShowPopup(this.Handle.ToInt32(), helpIndex);
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

        #region - [Child Class Context Menu Options] -
        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Change Chart Scale' context menu option attached to the <c>ListBox</c> containing the watch 
        /// variables that have been added to the workset. The logic is defined in the child class. 
        /// </summary>
        /// <remarks>This menu option is only relevant to the child form used to configure the chart recorder.</remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_MenuItemChangeChartScaleFactor_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Configure Bitmask Plot' context menu option. The logic is defined in the child class.
        /// </summary>
        /// <remarks>This menu option is only relevant to the child form used to configure the plot layout.</remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_MenuItemConfigureBitmaskPlot_Click(object sender, EventArgs e)
        {
        }
        #endregion - [Child Class Context Menu Options] -

        /// <summary>
        /// Event handler for the <c>Click</c> event associated with the 'Show Definition' context menu option attached to the <c>ListBox</c> containing the available 
        /// watch variables.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_MenuItemShowDefinitionAll_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_ListBoxAvailable.SelectedItems.Count == 1)
            {
                int oldIdentifier = ((WatchItem_t)m_ListBoxAvailable.SelectedItem).OldIdentifier;

                // Get the help index associated with the watch variable.
                WatchVariable watchVariable;
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                    if (watchVariable != null)
                    {
                        int helpIndex = watchVariable.HelpIndex;

                        // If the help index exists, show the help topic associated with the index.
                        if (helpIndex != CommonConstants.NotFound)
                        {
                            WinHlp32.ShowPopup(this.Handle.ToInt32(), helpIndex);
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
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Add the selected items from the specified source <c>ListBox</c> control to the specified destination <c>ListBox</c> control.
        /// </summary>
        /// <param name="listSource">The source <c>ListBox</c> control.</param>
        /// <param name="listDestination">the destination <c>ListBox</c> control.</param>
        protected virtual void Add(ref ListBox listSource, ref ListBox listDestination)
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

			if ((m_ListItemCount + count) > EntryCountMax)
            {
                MessageBox.Show(Resources.MBTWorksetDefineMaxExceeded, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor = Cursors.WaitCursor;

            // ------------------------------------------------------------------------------------------------------------------------------
            // For each selected item: (a) add this to the destination list; (b) delete from the source list and (c) update the added flag.
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

            // Remember the selected item list contents will change every time an item is deleted. The best approach is to repeatedly remove the selected items
            // collection for index = 0 by the number of items added to the destination list.
            for (int index = 0; index < count; ++index)
            {
                // Keep track of these changes in case the user selects the cancel key.
				m_AddedWatchVariables.Add(listSource.SelectedItems[0]);

                listSource.Items.Remove(listSource.SelectedItems[0]);
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
        /// Remove the selected item(s) from the specified source <c>ListBox</c> control and add the item(s) to the specified destination <c>ListBox</c> control,
        /// in the correct watch identifier order.
        /// </summary>
        /// <remarks>This method should only be used when removing entries from the workset.</remarks>
        /// <param name="listSource">The source <c>ListBox</c> control.</param>
        /// <param name="listDestination">The destination <c>ListBox</c> control.</param>
        protected virtual void Remove(ref ListBox listSource, ref ListBox listDestination)
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
                    // Update the list of available watch variables.
                    m_WatchItems[((WatchItem_t)listSource.SelectedItems[index]).OldIdentifier].Added = false;
                }
                catch (Exception)
                {
                    // Ensure that an exception isn't thrown.
                }

                // Keep track of the number of watch elements that have been added to the workset.
                m_ListItemCount--;
            }

            // Remember the selected item list contents will change every time an item is deleted. The best approach is to repeatedly remove the selected items
            // collection for index = 0 by the number of items added to the destination list.
            int removeAtIndex;
            for (int index = 0; index < count; ++index)
            {
                // Keep track of these changes in case the user selects the cancel key.
                m_RemovedWatchVariables.Add(listSource.SelectedItems[0]);

                // Get the index of the ListBox item that is currently being removed.
                removeAtIndex = listSource.SelectedIndices[0];
                listSource.Items.Remove(listSource.SelectedItems[0]);

                RemoveSupplementalFields(removeAtIndex);
            }

            UpdateListBoxAvailable(m_WatchItems);
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
        /// Add any supplemental workset fields specific to the child class. The logic is defined in the child class.
        /// </summary>
        /// <param name="oldIdentifier">The old identifier of the watch variable that is to be added.</param>
        protected virtual void AddSupplementalFields(int oldIdentifier)
        {
        }

        /// <summary>
        /// Remove any supplemental workset fields specific to the child class. The logic is defined in the child class.
        /// </summary>
        /// <param name="removeAtIndex">The index of the ListBox row that is to be removed.</param>
        protected virtual void RemoveSupplementalFields(int removeAtIndex)
        {
        }

        /// <summary>
        /// Update the count labels that show the number of watch variables that are available and the number that have been added to each column of the workset.
        /// </summary>
        protected virtual void UpdateCount()
        {
        }

        /// <summary>
        /// Add the watch variables defined in the specified list of old identifiers to the specified <c>ListBox</c> control.
        /// </summary>
        /// <param name="listBox">The <c>ListBox</c> to which the items are to be added.</param>
        /// <param name="worksetColumn">The column of the workset that is to be added to the <c>ListBox</c> control.</param>
        protected virtual void WatchItemAddRange(ListBox listBox, Column_t worksetColumn)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            List<short> oldIdentifierList = worksetColumn.OldIdentifierList;

            listBox.Items.Clear();
            listBox.SuspendLayout();

            WatchItem_t watchItem;
            short oldIdentifier;
            for (int index = 0; index < oldIdentifierList.Count; index++)
            {
                watchItem = new WatchItem_t();
                oldIdentifier = oldIdentifierList[index];
                watchItem.OldIdentifier = oldIdentifier;
                watchItem.Added = true;

                // The DisplayMask field is only applicable to bitmask watch variables and is used to define which bits of the bitmask watch variable
                // are to be plotted.
                watchItem.DisplayMask = uint.MaxValue;
                listBox.Items.Add(watchItem);
            }

            listBox.PerformLayout();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Update the <c>ListBox</c> control that displays the list of available watch variables that match the current search criteria with the list of currently 
        /// available watch variables, sorted alpha-numerically. 
        /// </summary>
        protected virtual void UpdateListBoxAvailable(WatchItem_t[] watchItems)
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

                // If the watch variable hasn't already been added to the workset, add it to the list of available watch variables, provided that it exists in the new 
                // data dictionary.
                if ((watchItem.Exists == true) && 
                    (watchItem.Added == false))
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
        /// Save the workset information to disk.
        /// </summary>
        protected void Save()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            try
            {
                // Generate the name of the file where the serialized data is to be stored, this is derived from the project identifier. If this is empty 
                // use the default filename.
                string filename;
                string qualifier = CommonConstants.Period + m_WorksetCollection.WorksetCollectionType.ToString();
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
        }

        #region - [Search/Sort] -
        /// <summary>
        /// Comparison function used to compare alphanumerically the watch variable names associated with the specified <seealso cref="WatchItem_t"/> parameters. 
        /// Although the parameters are passed as objects they are cast to <seealso cref="WatchItem_t"/> types.
        /// </summary>
        /// <remarks>The parameters must be passed as object types rather than <c>WatchItem_t</c> types as the generic list that is to be sorted is an array of 
        /// objects.</remarks>
        /// <param name="first">The first <c>WatchItem_t</c> parameter, as an object reference.</param>
        /// <param name="second">The second <c>WatchItem_t</c> parameter, as an object reference.</param>
        /// <returns>-1 if the watch variable name associated with <paramref name="first"/> is less than the watch variable name associated with 
        /// <paramref name="second"/>; 0 is the watch variable names associated with both comparands are equal and +1 if the watch variable name associated
        /// with <paramref name="first"/> is greater than the watch variable name associated with <paramref name="second"/>.</returns>
        protected int CompareWatchItemsByVariableName(object first, object second)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return 0;
            }

            // Get the watch identifiers associated with each of the comparands. 
            int oldIdentifierFirstComparand = ((WatchItem_t)first).OldIdentifier;
            int oldIdentifierSecondComparand = ((WatchItem_t)second).OldIdentifier;

            // Now get the watch variable names corresponding to each watch identifier.
            string watchNameFirstComparand = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifierFirstComparand].Name;
            string watchNameSecondComparand = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifierSecondComparand].Name;
            
            // Now do the comparison.
            return watchNameFirstComparand.CompareTo(watchNameSecondComparand);
        }

        /// <summary>
        /// Predicate function called by the <c>List.FindAll()</c> method to determine if the watch variable name associated with the OldIdentifier field of the 
        /// specified item in the list 'Contains' the text specified in the search text box.
        /// </summary>
        /// <param name="item">The list item that is to be processed.</param>
        /// <returns>True if the specified item meets the logic requirements given in the function; otherwise false.</returns>
        protected bool ContainsSearchText(object item)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return false;
            }

            // Determine the watch identifier of the item.
            int oldIdentifier = ((WatchItem_t)item).OldIdentifier;

            // Flag to indicate whether the watch variable names associated with the specified item contains the text string associated with the search text box - not 
            // case sensitive.
            bool match = Lookup.WatchVariableTableByOldIdentifier.Items[((WatchItem_t)item).OldIdentifier].Name.ToLower().Contains(m_TextBoxSearch.Text.ToLower());
            return match;
        }
        #endregion - [Search/Sort] -

        #region - [Status Message/Buttons] -
        /// <summary>
        /// Clear the status message label.
        /// </summary>
        protected void ClearStatusMessage()
        {
            m_PanelStatusMessage.Visible = false;
            m_LegendImageStatusMessage.Image = null;
            m_LabelStatusMessage.Text = string.Empty;
        }

        /// <summary>
        /// Write the specified message to the status message label.
        /// </summary>
        protected void WriteStatusMessage(string message)
        {
            m_PanelStatusMessage.Visible = true;
            m_LegendImageStatusMessage.Image = Resources.Warning;
            m_LabelStatusMessage.Text = message;
        }

        /// <summary>
        /// Disable the Apply and OK buttons and, if <c>message != string.Empty</c>, display the specified status message.
        /// </summary>
        /// <param name="message">The message text. If the message is string.Empty no warning icon or message text will be displayed.</param>
        protected void DisableApplyAndOKButtons(string message)
        {
            // Disable the Apply and OK buttons.
            m_ButtonApply.Enabled = false;
            m_ButtonOK.Enabled = false;

            // If a status message has been specified, inform the user.
            if (message != string.Empty)
            {
                WriteStatusMessage(message);
            }
            else
            {
                ClearStatusMessage();
            }
        }

        /// <summary>
        /// Enable the Apply and OK buttons and clear the status message provided at least one watch variable has been selected and the workset name has been
        /// defined; otherwise, disable the Apply and OK buttons and write a status message.
        /// </summary>
        protected virtual void EnableApplyAndOKButtons()
        {
            // Check whether the watch variable count is within the permitted limits.
            if (m_ListItemCount <= EntryCountMax)
            {
                // Only enable the OK and Apply buttons if the workset has at least one watch variable and a valid workset name has been defined.    
                if (m_ListItemCount > 0)
                {
                    // Yes - The workset contains at least one watch variable.
                    
                    // Check whether the workset name has been defined.
                    if (m_TextBoxName.Text != string.Empty)
                    {
                        // Yes - Enable the Apply and OK buttons and clear the status message.
                        m_ButtonApply.Enabled = true;
                        m_ButtonOK.Enabled = true;

                        ClearStatusMessage();
                    }
                    else
                    {
                        // No - The workset contains at least one watch variable but the workset name has not been defined. Disable the Apply and OK buttons.
                        DisableApplyAndOKButtons(Resources.SMWorksetNameNotDefined);
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

                    DisableApplyAndOKButtons(message);
                }

                if (m_WorksetCollection != null)
                {
                    // If the form is being used to create a new workset, check whether the current workset name is already in use.
                    if ((m_CreateMode == true) && (m_WorksetCollection.Contains(m_TextBoxName.Text.Trim()) == true))
                    {
                        DisableApplyAndOKButtons(string.Format(Resources.SMWorksetNameExists, m_TextBoxName.Text));
                    }
                }
            }
        }
        #endregion - [Status Message/Buttons] -

        #region - [Event Management] -
        /// <summary>
        /// Raise a <c>DataUpdate</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="eventArgs">Parameter passed from the object that raised the event.</param>
        protected void OnDataUpdate(object sender, EventArgs eventArgs)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (DataUpdate != null)
            {
                DataUpdate(sender, eventArgs);
            }
        }
        #endregion - [Event Management] -
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the default name for any new workset that is created.
        /// </summary>
        protected string DefaultNewWorksetName
        {
            get { return m_DefaultNewWorksetName; }
        }

        /// <summary>
        /// Gets or sets the flag that controls whether the user is able to modify the current workset. True, to allow the user to modify the workset; otherwise, false.
        /// </summary>
        protected bool ModifyEnabled
        {
            get { return m_ModifyEnabled; }
            set 
            { 
                m_ModifyEnabled = value;
                m_ButtonAdd.Enabled = m_ModifyEnabled;
                m_ButtonRemove.Enabled = m_ModifyEnabled;
                m_ButtonMoveDown.Enabled = m_ModifyEnabled;
                m_ButtonMoveUp.Enabled = m_ModifyEnabled;
            }
        }

		/// <summary>
		/// Gets the maximum number of watch variables that the workset can support.
		/// </summary>
		public virtual int EntryCountMax
		{
			get { return m_EntryCountMax; }
		}

        #endregion --- Properties ---
		
	}
}