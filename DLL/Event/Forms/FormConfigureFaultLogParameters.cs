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
 *  File name:  FormConfigureFaultLogParametersNew.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/16/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/06/11    1.1     K.McD           1.  Added the m_WorksetFromVCU member variable to store a copy of the stream parameters retrieved from the VCU in workset format.
 *                                      2.  Modified the event handler for the Apply button Click event to include a check to determine if the data stream 
 *                                          parameters have been changed. If the parameters have not been changed no action is taken; otherwise: (a) the event log is 
 *                                          saved to disk and then cleared and (b) the updated data stream parameters are downloaded to the VCU.
 *                                      3.  Included an additional signature for the CompareWorkset() method.
 * 
 *  01/31/11    1.2     K.McD           1.  Modified the Apply button event handler to use the ClearCurrentEventLog() method of the FormViewEventLog class to clear 
 *                                          the current event log.
 * 
 *  02/21/11    1.3     K.McD           1.  Modified the event handler for the Apply button such that it no longer clears the current log before downloading the new 
 *                                          parameters.
 *                                      2.  Auto-modified as a result of the method name change to UpdateSampleMultiple().
 * 
 *  02/28/11    1.4     K.McD           1.  Modified the constructor such that the name of the current workset is shown on the combo box control.
 *                                      2.  Modified to accommodate the signature change associated with the ConvertToWorkset() method.
 *                                      3.  Modified the event handler for the Save function key to ask for confirmation before updating a saved workset.
 * 
 *  03/17/11    1.5     K.McD           1.  Auto-modified as a result of property name changes associated with the Common.Security class.
 * 
 *  03/28/11    1.6     K.McD           1.  Auto-modified as a result of a number of name changes to the properties and methods of external classes.
 * 
 *  04/27/11    1.7     K.McD           1.  Renamed to FormConfigureFaultLogParameters from FormSetupStream.
 *                                      2.  Auto-modified as a result of a name change to a member variable inherited from the parent class.
 *                                      3.  Added a check as to whether the class has been disposed of to a number of methods.
 *                                      4.  Auto-modified as a result of name changes to a number of resources.
 *                                      5.  Modified the tool tip text depending upon the state of the form.
 *                                      6.  Modified the DropDownStyle property of the ComboBox depending upon whether the workset exists or not.
 *                                      
 *  05/23/11    1.8     K.McD           1.  Modified the LoadWorkset() method to accommodate the signature change to the FormWorksetDefine.WatchItemAddRange() method.
 *  
 *  06/21/11    1.9     K.McD           1.  Auto-modified as a result of a name change to an inherited member variable.
 *  
 *  07/20/11    1.10    K.McD           1.  Modified the signature of the constructor to use the ICommunicationParent interface.
 *  
 *  08/05/11    2.0     K.McD           1.  Major changes, now inherits from the FormConfigure class.
 *  
 *  08/10/11    2.1     Sean.D          1.  Included support for offline mode. Modified the constructor to conditionally choose CommunicationEvent or
 *                                          CommunicationEventOffline.
 *  
 *  12/01/11    2.2     K.McD           1.  Set the CountMax property of the workset to Parameter.WatchSizeFaultLogMax in the ConvertToWorkset() method.
 *                                      2.  Modified the m_NumericUpDownSampleMultiple_ValueChanged() method such that the DataUpdate event is only triggered if 
 *                                          the ModifyEnabled property is asserted.
 *                                          
 *  07/24/13    2.3     K.McD           1.  Included update of the 'Total Count' label in the UpdateCount() method.
 *                                      2.  Automatic update when all references to the Parameter.WatchSizeFaultLogMax constant were replaced by references to the
 *                                          Parameter.WatchSizeFaultLog property.
 *                                      3.  Modified the event handler for the Download key to check whether the number of watch variables associated with the current
 *                                          workset exceeds the number supported by the current event log.
 *                                      4.  Modified the signature of the constructor to include a reference to the current event log and used this to update the
 *                                          EntryCountMax property of the class.
 *                                          
 *  03/26/15    2.4     K.McD           1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                              
 *                                              1. MOC-0171-26. Kawasaki pointed out that Chart Recorder tabs are identified as ‘COLUMNS’ and requested that this
 *                                                 be changed.
 *                                                 
 *                                              2.  MOC-0171-28. A check will be included as part of the ‘Save’ procedure to ensure that an empty workset cannot be saved.
 *                                              
 *                                      2.  SNCR - R188 PTU [20 Mar 2015] Item 7. While attempting to configure a data stream, the set of parameters that were downloaded
 *                                          from the VCU were not defined in an existing workset, consequently FormConfigureFaultLogParameters entered 'Create' mode.
 *                                          While in this mode, the PTU displayed the workset parameters that were downloaded from the VCU and gave the user the
 *                                          opportunity to name the workset but not the opportunity to Save the workset. Modify the code to ensure that the new workset
 *                                          can be saved.
 *                                                  
 *                                      Modifications
 *                                      1.  Ref.: 1.1.1.
 *                                          1.  Modified the constructor so that the TabPage header associated with column 1 is not updated with the workset name.
 *                                      
 *                                          2.  Modified the constructor to check whether the project supports multiple data stream types or whether
 *                                              the number of parameters supported by the data stream exceeds the number that can be displayed on the TabPage without
 *                                              requiring scroll bars and call AddRowHeader() or NoRowHeader() as appropriate.
 *                                          
 *                                          3.  Included the constant WatchSizeFaultLogMax which defines the number of parameters that can be displayed on the TabPage
 *                                              without requiring scroll bars.
 *                                              
 *                                          4.  Modified the constructor to disable all ToolStrip buttons, except the Save button, if the workset downloaded from the
 *                                              Vehicle Control Unit does not already exist. Also removed any references to the  m_NoDefinedWorkset flag as this is
 *                                              no longer used. It has been replaced by the m_UseTextBoxAsNameSource flag.
 *                                              
 *                                      2.  Ref.: 1.1.2, 2.
 *                                          1.  Modified the m_NumericUpDownSampleMultiple_ValueChanged() method to call the EnableApplyAndOKButtons() method. This
 *                                              ensures that a full check is made on the state of the workset whenever the 'Sample Multiple' value is changed.
 *                                          2.  Wherever the Text property of the 'm_TextBoxName' TextBox was changed the code was modified to ensure that this didn't
 *                                              trigger the TextChanged() event handler by de-registering the event handler prior to setting the property and then
 *                                              re-registering it again.
 *                                          3.  Modified the constructor to set the state of the m_NoDefinedWorkset flag to true if the workset downloaded from the
 *                                              VCU did not match an existing workset. It had previously, incorrectly been set to false.
 *                                          4.  Modified the constructor to enable or disable the Save ToolStrip button depending upon whether the workset
 *                                              downloaded from the VCU matched an existing workset or not. Enabled if the downloaded workset did not exist; otherwise,
 *                                              false.
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

using Common;
using Common.Forms;
using Common.Communication;
using Common.Configuration;
using Event.Communication;
using Event.Properties;

namespace Event.Forms
{
    /// <summary>
    /// Form to allow the user to define: (a) the watch variables that are associated with a fault log data stream (b) the order in which they are to be displayed and 
    /// (c) the multiple of the recording interval at which the data is to be recorded.
    /// </summary>
    public partial class FormConfigureFaultLogParameters : FormConfigure, ICommunicationInterface<ICommunicationEvent>
    {
        #region --- Constants ---
        /// <summary>
        /// The maximum FaultLog/DataStream WatchSize that can be displayed using the 'Row Header' <c>ListBox</c>. Value: 16.  
        /// </summary>
        private const int WatchSizeFaultLogMax = 16;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        private ICommunicationEvent m_CommunicationInterface;

        /// <summary>
        /// The multiple of the recording interval at which the data is to be recorded.
        /// </summary>
        protected short m_SampleMultiple;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes an new instance of the form. Zero parameter constructor.
        /// </summary>
        public FormConfigureFaultLogParameters()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes an new instance of the form. Defines the communication interface and then downloads the current default data stream parameters and displays these 
        /// on the form.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface that is to be used to communicate with the VCU.</param>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        /// <param name="log">The selected event log.</param>
        public FormConfigureFaultLogParameters(ICommunicationParent communicationInterface, WorksetCollection worksetCollection, Log log)
            : base(worksetCollection)
        {
            InitializeComponent();

            // Move the position of the Cancel buttons.
            m_ButtonCancel.Location = m_ButtonApply.Location;
            
            // Initialize the communication interface.
            if (communicationInterface is CommunicationParent)
            {
                CommunicationInterface = new CommunicationEvent(communicationInterface);
            }
            else
            {
                CommunicationInterface = new CommunicationEventOffline(communicationInterface);
            }
            Debug.Assert(CommunicationInterface != null);

            // Don't allow the user to edit the workset until the security level of the workset has been established.
            ModifyEnabled = false;
            m_NumericUpDownSampleMultiple.Enabled = ModifyEnabled;
                        
            // ----------------------------------------------------
            // Get the current data stream parameters from the VCU.
            // ----------------------------------------------------
            short variableCount, pointCount, sampleMultiple;
            short[] watchIdentifiers, dataTypes;
            try
            {
                CommunicationInterface.GetDefaultStreamInformation(out variableCount, out pointCount, out sampleMultiple, out watchIdentifiers, out dataTypes);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MBTGetDefaultStreamParametersFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Convert the parameters to a workset.
            Workset_t workset = CommunicationInterface.ConvertToWorkset("GetDefaultStreamInformation()", watchIdentifiers, sampleMultiple);

            // Search the list of worksets for a match.
            m_WorksetFromVCU = workset;
            m_WorksetToCompare = workset;
            Workset_t matchedWorkset = Workset.FaultLog.Worksets.Find(CompareWorkset);

            // Check whether a match was found.
            if (matchedWorkset.WatchItems == null)
            {
                // No - Create a new workset.
                workset.Name = DefaultNewWorksetName;

                // Set the flag to indicate that the workset does not exist and that any changes will be saved as a new workset.
                m_CreateMode = true;
                m_ComboBoxWorkset.DropDownStyle = ComboBoxStyle.DropDown;

                // Ensure that the TextChanged event is not triggered as a result of specifying the Text property of the TextBox control.
                m_TextBoxName.TextChanged -= new EventHandler(m_TextBoxName_TextChanged);
                m_TextBoxName.Text = workset.Name;
                m_TextBoxName.TextChanged += new EventHandler(m_TextBoxName_TextChanged);

                m_TextBoxName.Enabled = true;
            }
            else
            {
                // Yes - Update the name and security level of the workset.
                workset = matchedWorkset;

                // Set the flag to indicate that the form already exists and any changes will result in the existing workset being modified.
                m_CreateMode = false;
                m_ComboBoxWorkset.DropDownStyle = ComboBoxStyle.DropDownList;

                // Ensure that the TextChanged event is not triggered as a result of specifying the Text property of the TextBox control.
                m_TextBoxName.TextChanged -= new EventHandler(m_TextBoxName_TextChanged);
                m_TextBoxName.Text = workset.Name;
                m_TextBoxName.TextChanged += new EventHandler(m_TextBoxName_TextChanged);

                m_TextBoxName.Enabled = false;
            }

            // Keep a record of the selected workset. This must be set up before the call to SetEnabledEditNewCopyRename().
            m_SelectedWorkset = workset;

            SetEnabledEditNewCopyRename(true);

            // Display the name of the default workset on the ComboBox control.
            // Ensure that the SelectionChanged event is not triggered as a result of specifying the Text property of the ComboBox control.
            m_ComboBoxWorkset.SelectedIndexChanged -= new EventHandler(m_ComboBoxWorkset_SelectedIndexChanged);
            m_ComboBoxWorkset.Text = workset.Name;
            m_ComboBoxWorkset.SelectedIndexChanged += new EventHandler(m_ComboBoxWorkset_SelectedIndexChanged);

            LoadWorkset(workset);

            // Update the EntryCountMax property to reflect the actual number of watch variables associated with the current event log.
            m_EntryCountMax = log.DataStreamTypeParameters.WatchVariablesMax;
            UpdateCount();

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

            // Disable all options other than save if the downloaded workset doesn't exist.
            if (m_CreateMode == true)
            {
                SetEnabledEditNewCopyRename(false);
            }

            // Allow the user to save the workset if it doesn't already exist.
            m_TSBSave.Enabled = (m_CreateMode == true) ? true : false;
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
                m_CommunicationInterface = null;

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
        /// Event handler for the 'Download' <c>ToolStripButton</c> <c>Click</c> event. Download the selected chart recorder workset to the VCU.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_TSBDownload_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            m_TSBDownload.Checked = true;

            Workset_t workset;
            workset = ConvertToWorkset(m_ComboBoxWorkset.Text);

            // Check whether the number of watch variables defined in the woorkset exceeds the number that are
            // supported by the current event log.
            if (workset.Count > EntryCountMax)
            {
                MessageBox.Show(Resources.MBTFaultLogWorksetWatchVariablesMaxExceeded + CommonConstants.NewPara + Resources.MBTFaultLogWorksetEditRequest,
                                Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // ---------------------------------------------------------------------
                // Check whether the user has modified the fault log parameter configuration.
                // ---------------------------------------------------------------------
                if (CompareWorkset(workset, m_WorksetFromVCU) == false)
                {
                    // Yes - Ask for confirmation and then download the chart recorder configuration to the VCU.
                    DialogResult dialogResult = MessageBox.Show(Resources.MBTQueryModifyFaultLogParameters, Resources.MBCaptionInformation, MessageBoxButtons.YesNo,
                                                                MessageBoxIcon.Information);
                    Update();
                    if (dialogResult == DialogResult.Yes)
                    {
                        bool updateSuccess = DownloadWorkset(workset);
                        if (updateSuccess == false)
                        {
                            MessageBox.Show(Resources.MBTModifyFaultLogParametersFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            m_TSBDownload.Checked = false;
                            Cursor = Cursors.Default;
                            return;
                        }

                        // Update the record of the chart recorder channel configuration that was retrieved from the VCU.
                        m_WorksetFromVCU = workset;

                        MessageBox.Show(Resources.MBTModifyFaultLogParametersSuccess, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_TSBDownload.Enabled = false;
                    }
                }
            }

            m_TSBDownload.Checked = false;
            Cursor = Cursors.Default;
            return;
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

            // Only generate an update event if the ModifyEnabled property is asserted.
            if (ModifyEnabled == true)
            {
                EnableApplyAndOKButtons();
                OnDataUpdate(this, new EventArgs());
            }
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        #region - [FormWorksetDefine] -
        /// <summary>
        /// Update the count label that shows the number of watch variables that have been added to the chart configuration workset.
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
            m_LabelCountTotal.Text = Resources.LegendTotalCount + CommonConstants.Colon + countColumn1.ToString() +
                                     CommonConstants.Space + Resources.LegendOf + CommonConstants.Space + EntryCountMax.ToString();
        }
        #endregion - [FormWorksetDefine] -

        #region - [FormConfigure] -
        /// <summary>
        /// Download the specified fault log workset to the VCU.
        /// </summary>
        /// <param name="workset">The workset that is to be downloaded to the VCU.</param>
        /// <returns>A flag that indicates whether the workset was successfully downloaded to the VCU. True, if the VCU update was successful; otherwise, false.</returns>
        protected override bool DownloadWorkset(Workset_t workset)
        {
            // A flag that indicates whether the chart recorder update was successful.
            bool updateSuccess;

            try
            {
                CommunicationInterface.SetDefaultStreamInformation(workset.SampleMultiple, workset.Column[0].OldIdentifierList);
                updateSuccess = true;
            }
            catch (Exception)
            {
                updateSuccess = false;
            }

            return updateSuccess;
        }

        /// <summary>
        /// Set the modify state to the specified state.
        /// </summary>
        /// <param name="modifyState">The required modify state.</param>
        protected override void SetModifyState(ModifyState modifyState)
        {
            base.SetModifyState(modifyState);
            switch (m_ModifyState)
            {
                case ModifyState.Configure:
                    Text = Resources.TitleConfigureFaultLogParameters;
                    break;
                case ModifyState.Edit:
                    Text = Resources.TitleEditFaultLogWorksets;
                    break;
                case ModifyState.Create:
                    Text = Resources.TitleCreateFaultLogWorkset;
                    break;
                case ModifyState.Copy:
                    Text = Resources.TitleCopyFaultLogWorkset;
                    break;
                case ModifyState.Rename:
                    Text = Resources.TitleRenameFaultLogWorkset;
                    break;
                default:
                    break;
            }

            m_NumericUpDownSampleMultiple.Enabled = ModifyEnabled;
        }

        /// <summary>
        /// Convert the current user settings to a workset.
        /// </summary>
        /// <param name="worksetName">The name of the workset.</param>
        /// <returns>The user settings converted to a workset.</returns>
        protected override Workset_t ConvertToWorkset(string worksetName)
        {
            Workset_t workset = base.ConvertToWorkset(worksetName);

            #region - [SampleMultiple] -
            workset.SampleMultiple = m_SampleMultiple;
            workset.CountMax = Parameter.WatchSizeFaultLog;
            #endregion - [SampleMultiple] -

            return workset;
        }

        /// <summary>
        /// Load the specified workset.
        /// </summary>
        /// <param name="workset">The workset that is to be processed.</param>
        protected override void LoadWorkset(Workset_t workset)
        {
            base.LoadWorkset(workset);

            UpdateSampleMultiple(workset.SampleMultiple);
        }

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
        #endregion - [FormConfigure] -
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the target.
        /// </summary>
        /// <remarks>This property is set by the child class, if appropriate.</remarks>
        public ICommunicationEvent CommunicationInterface
        {
            get { return m_CommunicationInterface; }
            set { m_CommunicationInterface = value; }
        }
        #endregion --- Properties ---
    }
}