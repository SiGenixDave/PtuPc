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
 *  File name:  FormConfigureEventFlags.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  03/31/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  04/08/11    1.1     K.McD           1.  Modified a number of XML tags.
 *                                      2.  Renamed a number of local variables.
 *                                      3.  Modified the m_ButtonOK_Click() event handler as follows: (a) added cursor control statements and (b) included a check as 
 *                                          to whether the object has been disposed of.
 *                                      4.  Moved the 'Size' region in the constructor.
 *                                      
 *  07/20/11    1.2     K.McD           1.  Modified the signature of the constructor to use the ICommunicationParent interface.
 *  
 *  08/10/11    1.3     Sean.D          1.  Included support for offline mode. Modified the constructor to conditionally choose CommunicationEvent or CommunicationEventOffline.
 *  
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

using Common;
using Common.Communication;
using Common.Configuration;
using Common.Forms;
using Event.Communication;
using Event.Properties;

namespace Event.Forms
{
    #region --- Structures ---
    /// <summary>
    /// A structure containing the status information associated with a particular event.
    /// </summary>
    /// <remarks>A generic list of these structures is constructed whenever an event log is downloaded from the VCU. This allows the user to obtain a 
    /// summary of the cumulative and recent number of occurrences of each event and to control whether: (a) the event is enabled and (b) whether the event is 
    /// to trigger the recording of a data stream.</remarks>
    public struct EventStatus_t
    {
        /// <summary>
        /// The index element associated with this structure.
        /// </summary>
        public int Index;

        /// <summary>
        /// The Identifier property of the event record associated with this structure. 
        /// </summary>
        /// <remarks>The event record associated with this field is accessed by setting the index of the RecordList property of the EventTable class to be equal 
        /// to the value of this field e.g. Lookup.EventTable.RecordList[Identifier].Description.</remarks>
        public short Identifier;

        /// <summary>
        /// A flag to control whether the event associated with this structure is enabled for the current event log.
        /// </summary>
        public bool Enabled;

        /// <summary>
        /// A flag to control whether the event associated with this structure is to trigger the recording of a fault log data stream.
        /// </summary>
        public bool StreamTriggered;

        /// <summary>
        /// The total number of occurrences of the event associated with this structure.
        /// </summary>
        public short CumulativeHistoryCount;

        /// <summary>
        /// The recent number of occurrences of the event associated with this structure.
        /// </summary>
        public short RecentHistoryCount;

        /// <summary>
        /// A flag that indicates whether any of the properties associated with this structure have been modified by the user.
        /// </summary>
        public bool Modified;
    }
    #endregion --- Structures ---

    /// <summary>
    /// A form to allow the user to configure the event flags associated with the current log. The event flags define which of the available event types associated with 
    /// an event log are enabled and which will trigger the recording of a fault log.
    /// </summary>
    public partial class FormConfigureEventFlags : FormPTUDialog, ICommunicationInterface<ICommunicationEvent>
    {
        #region --- Constants ---
        /// <summary>
        /// The format string used to display the Identifier property of the event record. Value = "0000".
        /// </summary>
        private const string FormatStringInteger = "0000";

        #region - [Margins] -
        /// <summary>
        /// The right margin to be applied to the <c>DataGridView</c> control. Value: 20.
        /// </summary>
        public const int MarginRightDataGridViewControl = 20;

        /// <summary>
        /// The right margin to be applied to the <c>Panel</c> control associated with the <c>DataGridView</c>. Value: 30.
        /// </summary>
        public const int MarginRightPanelControl = 30;
        #endregion - [Margins] -

        #region - [DataGridView Column Indices] -
        /// <summary>
        /// The column index associated with the event index of the <c>DataGridView</c> control. Value: 0.
        /// </summary>
        private const int ColumnIndexEventIndex = 0;

        /// <summary>
        /// The column index associated with the event description field of the <c>DataGridView</c> control. Value: 1.
        /// </summary>
        private const int ColumnIndexEventDescription = 1;

        /// <summary>
        /// The column index associated with the enable event field of the <c>DataGridView</c> control. Value: 2.
        /// </summary>
        private const int ColumnIndexEventEnabled = 2;

        /// <summary>
        /// The column index associated with the stream triggered field of the <c>DataGridView</c> control. Value: 3.
        /// </summary>
        private const int ColumnIndexStreamTriggered = 3;

        /// <summary>
        /// The column index associated with the cumulative history field of the <c>DataGridView</c> control. Value: 4.
        /// </summary>
        private const int ColumnIndexCumulativeHistory = 4;

        /// <summary>
        /// The column index associated with the recent history field of the <c>DataGridView</c> control. Value: 5.
        /// </summary>
        private const int ColumnIndexRecentHistory = 5;
        #endregion - [DataGridView Column Indices] -
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        protected ICommunicationEvent m_CommunicationInterface;

        /// <summary>
        /// An event status list associated with the current event log;
        /// </summary>
        protected List<EventStatus_t> m_EventStatusList;

        /// <summary>
        /// The text to be displayed if the state of the boolean property is true.
        /// </summary>
        private readonly string m_TextTrue = Resources.TextSet;

        /// <summary>
        /// The text to be displayed if the state of the boolean property is false.
        /// </summary>
        private readonly string m_TextFalse = Resources.TextReset;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormConfigureEventFlags()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="communicationInterface">The communication interface that is to be used to communicate with the VCU.</param>
        /// <param name="log">The current event log.</param>
        public FormConfigureEventFlags(ICommunicationParent communicationInterface, Log log)
        {
            InitializeComponent();

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

            #region - [Size] -
            // Get the combined width of all visible DataGridView columns.
            int dataGridViewWidth = 0;
            DataGridViewColumn dataGridViewColumn;
            for (int columnIndex = 0; columnIndex < m_DataGridViewEventStatus.Columns.Count; columnIndex++)
            {
                dataGridViewColumn = m_DataGridViewEventStatus.Columns[columnIndex];
                if (dataGridViewColumn.Visible == true)
                {
                    dataGridViewWidth += dataGridViewColumn.Width;
                }
            }

            m_PanelDataGridViewEventStatus.Width = dataGridViewWidth + MarginRightDataGridViewControl;

            Width = m_PanelDataGridViewEventStatus.Width + MarginRightPanelControl;
            #endregion - [Size] -

            // ------------------------------------------------------------------
            // Get the list of events associated with the current event log.
            // ------------------------------------------------------------------
            List<EventRecord> foundEventRecordList;
            foundEventRecordList = Lookup.EventTable.RecordList.FindAll(delegate(EventRecord eventRecord)
            {
                // Include a try/catch block in case an event record has not been defined.
                try
                {
                    // The LOGID field of the EVENTS table actually uses the log index value which is equal to the log identifier - 1.
                    return (eventRecord.LogIdentifier == log.Identifier - 1);
                }
                catch (Exception)
                {
                    return false;
                }
            });

            foundEventRecordList.Sort(CompareByTaskIDByEventIDAscending);

            // ---------------------------------------------------------------
            // Get the event flag information associated with the current log.
            // ---------------------------------------------------------------
            int eventCount = (short)foundEventRecordList.Count;
            int maxEventCount = log.MaxEventsPerTask * log.MaxTasks;

            // Initialize the validFlags array to define which of the events are used in the current log.
            short[] validFlags = new short[maxEventCount];
            int eventIndex;
            for (int index = 0; index < foundEventRecordList.Count; index++)
            {
                eventIndex = foundEventRecordList[index].TaskIdentifier * log.MaxEventsPerTask + foundEventRecordList[index].EventIdentifier;
                validFlags[eventIndex] = CommonConstants.True;
            }

            short[] enabledFlags = new short[eventCount];
            short[] streamTriggeredFlags = new short[eventCount];
            try
            {
                CommunicationInterface.GetFltFlagInfo(validFlags, ref enabledFlags, ref streamTriggeredFlags, (short)maxEventCount);
            }
            catch (Exception)
            {
                throw new CommunicationException(Resources.EMGetFltFlagInfoFailed);
            }

            // ---------------------------------
            // Initialise the event status list.
            // ---------------------------------
            m_EventStatusList = new List<EventStatus_t>(eventCount);
            EventStatus_t eventStatus;
            for (int eventStatusIndex = 0; eventStatusIndex < eventCount; eventStatusIndex++)
            {
                eventStatus = new EventStatus_t();
                eventStatus.Index = eventStatusIndex;
                eventStatus.Identifier = foundEventRecordList[eventStatusIndex].Identifier;

                if (enabledFlags[eventStatusIndex] == CommonConstants.True)
                {
                    eventStatus.Enabled = true;
                }

                if (streamTriggeredFlags[eventStatusIndex] == CommonConstants.True)
                {
                    eventStatus.StreamTriggered = true;
                }

                m_EventStatusList.Add(eventStatus);
            }

            AddList(m_EventStatusList);
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

                    if (m_EventStatusList != null)
                    {
                        m_EventStatusList.Clear();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                m_EventStatusList = null;
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
        #region - [Buttons] -
        /// <summary>
        /// Event handler for the OK button <c>Click</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_ButtonOK_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            List<EventStatus_t> modifiedEventFlagList = new List<EventStatus_t>();

            // Check which of the event status structure elements have been modified.
            modifiedEventFlagList = EventStatusList.FindAll(delegate(EventStatus_t eventStatus)
            {
                return (eventStatus.Modified == true);
            });

            if (modifiedEventFlagList.Count >= 1)
            {
                DialogResult dialogResult = MessageBox.Show(Resources.MBTEventFlagsChanged, Resources.MBCaptionInformation, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                Update();
                if (dialogResult == DialogResult.Yes)
                {
                    // Update the VCU with the latest modifications.
                    for (int index = 0; index < modifiedEventFlagList.Count; index++)
                    {
                        EventRecord eventRecord;
                        try
                        {
                            eventRecord = Lookup.EventTable.RecordList[modifiedEventFlagList[index].Identifier];
                            if (eventRecord == null)
                            {
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        // Update the VCU.
                        short enabledFlag;
                        if (modifiedEventFlagList[index].Enabled == true)
                        {
                            enabledFlag = CommonConstants.True;
                        }
                        else
                        {
                            enabledFlag = 0;
                        }

                        short streamTriggeredFlag;
                        if (modifiedEventFlagList[index].StreamTriggered == true)
                        {
                            streamTriggeredFlag = CommonConstants.True;
                        }
                        else
                        {
                            streamTriggeredFlag = 0;
                        }

                        try
                        {
                            CommunicationInterface.SetFaultFlags((short)eventRecord.TaskIdentifier, (short)eventRecord.EventIdentifier, enabledFlag, streamTriggeredFlag);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(string.Format(Resources.MBTSetFaultFlagsFailed, eventRecord.Description), Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the Cancel button <c>Click</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion - [Buttons] -

        #region - [DataGridView] -
        /// <summary>
        /// Event handler for the <c>DataGridView</c> control <c>CellClick</c> event. Toggle the state of the event flag property corresponding to the selected cell. This 
        /// is only applicable if the selected cell corresponds to the Enabled property or the StreamTriggered property.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_DataGridViewFlags_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            EventStatus_t eventStatus;
            DataGridViewRow dataGridViewRow;
            if (GetEventStatusElement(out eventStatus, out dataGridViewRow) == true)
            {
                // Check which column has been selected.
                if (e.ColumnIndex == ColumnIndexEventEnabled)
                {
                    // Toggle the state of the Enabled property of the event flag.
                    eventStatus.Enabled = !eventStatus.Enabled;
                    eventStatus.Modified = true;

                    // Get the index of the matched event flag in the list and replace the entry with the modified value.
                    int index = eventStatus.Index;
                    EventStatusList.RemoveAt(index);
                    EventStatusList.Insert(index, eventStatus);

                    // Update the 'Enable Event' column of the DataGridView control.
                    if (eventStatus.Enabled == true)
                    {
                        dataGridViewRow.Cells[ColumnIndexEventEnabled].Value = (object)m_TextTrue;
                    }
                    else
                    {
                        dataGridViewRow.Cells[ColumnIndexEventEnabled].Value = (object)m_TextFalse;
                    }
                }
                else if (e.ColumnIndex == ColumnIndexStreamTriggered)
                {
                    // Toggle the state of the StreamTriggered property of the event flag.
                    eventStatus.StreamTriggered = !eventStatus.StreamTriggered;
                    eventStatus.Modified = true;

                    // Get the index of the matched event flag in the list and replace the entry with the modified value.
                    int index = eventStatus.Index;
                    EventStatusList.RemoveAt(index);
                    EventStatusList.Insert(index, eventStatus);

                    // Update the 'Stream Triggered' column of the DataGridView control.
                    if (eventStatus.StreamTriggered == true)
                    {
                        dataGridViewRow.Cells[ColumnIndexStreamTriggered].Value = (object)m_TextTrue;
                    }
                    else
                    {
                        dataGridViewRow.Cells[ColumnIndexStreamTriggered].Value = (object)m_TextFalse;
                    }
                }
            }
            
            Cursor = Cursors.Default;
        }
        #endregion - [DataGridView] -

        #region - [Context Menu] -
        /// <summary>
        /// Event handler for the 'Show Definition' context menu. Show the help information associated with the event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemShowDefinition_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            EventStatus_t eventStatus;
            DataGridViewRow dataGridViewRow;
            if (GetEventStatusElement(out eventStatus, out dataGridViewRow) == true)
            {
                // Get the help index associated with the event index.
                int helpIndex;
                try
                {
                    helpIndex = Lookup.EventTable.RecordList[eventStatus.Identifier].HelpIndex;
                }
                catch (Exception)
                {
                    helpIndex = CommonConstants.NotDefined;
                }

                if (helpIndex != CommonConstants.NotDefined)
                {
                    WinHlp32.ShowPopup(this.Handle.ToInt32(), helpIndex);
                }
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the context menu 'Opened' event. Check the 'Enabled' and 'Stream Triggered' context menu options depending upon the state of the 
        /// corresponding properties of the event status element associated with the selected row of the <c>DataGridView</c> control.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ContextMenuStripFlags_Opened(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            EventStatus_t eventStatus;
            DataGridViewRow dataGridViewRow;
            if (GetEventStatusElement(out eventStatus, out dataGridViewRow) == true)
            {
                m_MenuItemEnabled.Checked = eventStatus.Enabled;
                m_MenuItemStreamTriggered.Checked = eventStatus.StreamTriggered;
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handled for the 'Enabled' context menu. Toggle the state of the Enabled property of the event flag associated with the selected record.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemEnabled_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            EventStatus_t eventStatus;
            DataGridViewRow dataGridViewRow;
            if (GetEventStatusElement(out eventStatus, out dataGridViewRow) == true)
            {
                // Toggle the state of the Enabled property of the event flag.
                eventStatus.Enabled = !eventStatus.Enabled;
                eventStatus.Modified = true;

                // Get the index of the matched event flag in the list and replace the entry with the modified value.
                int index = eventStatus.Index;
                EventStatusList.RemoveAt(index);
                EventStatusList.Insert(index, eventStatus);

                // Update the 'Enable Event' column of the DataGridView control.
                if (eventStatus.Enabled == true)
                {
                    dataGridViewRow.Cells[ColumnIndexEventEnabled].Value = (object)m_TextTrue;
                }
                else
                {
                    dataGridViewRow.Cells[ColumnIndexEventEnabled].Value = (object)m_TextFalse;
                }
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handled for the 'Stream Triggered' context menu. Toggle the state of the StreamTriggered property of the event flag associated with the selected record.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemStreamTriggered_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            EventStatus_t eventStatus;
            DataGridViewRow dataGridViewRow;
            if (GetEventStatusElement(out eventStatus, out dataGridViewRow) == true)
            {
                // Toggle the state of the StreamTriggered property of the event flag.
                eventStatus.StreamTriggered = !eventStatus.StreamTriggered;
                eventStatus.Modified = true;

                // Get the index of the matched event flag in the list and replace the entry with the modified value.
                int index = eventStatus.Index;
                EventStatusList.RemoveAt(index);
                EventStatusList.Insert(index, eventStatus);

                // Update the 'Stream Triggered' column of the DataGridView control.
                if (eventStatus.StreamTriggered == true)
                {
                    dataGridViewRow.Cells[ColumnIndexStreamTriggered].Value = (object)m_TextTrue;
                }
                else
                {
                    dataGridViewRow.Cells[ColumnIndexStreamTriggered].Value = (object)m_TextFalse;
                }
            }

            Cursor = Cursors.Default;
        }
        #endregion - [Context Menu] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Compare the two specified <c>EventRecord</c> types and return an integer value that indicates their relative positions in the sort order. This value is used 
        /// to sort the list of event records by TaskIdentifier followed by EventIdentifier, in ascending order.
        /// </summary>
        /// <param name="eventRecordA">The first event record that is to be compared.</param>
        /// <param name="eventRecordB">The second event record that is to be compared.</param>
        /// <returns>Less than zero if eventRecordA is lower than eventRecordB in the required sort order, 0 if eventRecordA is equal to eventRecordB in the required sort 
        /// order and greater than zero if eventRecordA is higher that eventRecordB in the required sort order.</returns>
        protected int CompareByTaskIDByEventIDAscending(EventRecord eventRecordA, EventRecord eventRecordB)
        {
            int result = 0;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return result;
            }

            if (eventRecordA == null)
            {
                if (eventRecordB == null)
                {
                    // If A is null and B is null, the records are equal.
                    result = 0;
                }
                else
                {
                    // If A is null and B is not null, B is higher in the sort order.
                    result = -1;
                }
            }
            else
            {
                if (eventRecordB == null)
                {
                    // If A is not null and B is null, A is higher in the sort order.
                    return -1;
                }
                else
                {
                    if (eventRecordA.TaskIdentifier == eventRecordB.TaskIdentifier)
                    {
                        if (eventRecordA.EventIdentifier == eventRecordB.EventIdentifier)
                        {
                            result = 0;
                        }
                        else if (eventRecordA.EventIdentifier > eventRecordB.EventIdentifier)
                        {
                            result = 1;
                        }
                        else if (eventRecordA.EventIdentifier < eventRecordB.EventIdentifier)
                        {
                            result = -1;
                        }
                    }
                    else if (eventRecordA.TaskIdentifier > eventRecordB.TaskIdentifier)
                    {
                        result = 1;
                    }
                    else if (eventRecordA.TaskIdentifier < eventRecordB.TaskIdentifier)
                    {
                        result = -1;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Add the specified event status list to the <c>DataGridView</c> control. If the specified list does not contain any records, no action will be taken.
        /// </summary>
        /// <param name="eventStatusList">The list of event status structures that are to be added to the <c>DataGridView</c> control.</param>
        protected void AddList(List<EventStatus_t> eventStatusList)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (eventStatusList.Count <= 0)
            {
                return;
            }

            // Add the specified event status list to the DataGridView control.
            m_DataGridViewEventStatus.SuspendLayout();
            EventStatus_t eventStatus;
            for (short eventStatusIndex = 0; eventStatusIndex < eventStatusList.Count; eventStatusIndex++)
            {
                eventStatus = eventStatusList[eventStatusIndex];

                DataGridViewRow dataGridViewRow = Convert(eventStatus);

                // Add the new row to the DataGridView control.
                m_DataGridViewEventStatus.Rows.Add(dataGridViewRow);
            }
            m_DataGridViewEventStatus.ResumeLayout(true);
        }

        /// <summary>
        /// Convert the specified event status structure to a <c>DataGridViewRow</c> so that it may be added to the <c>DataGridView</c> control used to display the 
        /// event status information.
        /// </summary>
        /// <param name="eventStatus">The event status structure that is to be converted.</param>
        /// <returns>The specified event status structure converted to a <c>DataGridViewRow</c>.</returns>
        private DataGridViewRow Convert(EventStatus_t eventStatus)
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return dataGridViewRow;
            }

            string description;

            // Get the description of the event type associated with the event flag.
            try
            {
                EventRecord eventRecord = Lookup.EventTable.RecordList[eventStatus.Identifier];
                if (eventRecord == null)
                {
                    description = string.Empty;
                }

                description = eventRecord.Description;
            }
            catch(Exception)
            {
                description = string.Empty;
            }

            string enableEventText;
            if (eventStatus.Enabled == true)
            {
                enableEventText = m_TextTrue;
            }
            else
            {
                enableEventText = m_TextFalse;
            }

            string streamTriggeredText;
            if (eventStatus.StreamTriggered == true)
            {
                streamTriggeredText = m_TextTrue;
            }
            else
            {
                streamTriggeredText = m_TextFalse;
            }

            dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.CreateCells(m_DataGridViewEventStatus, new object[]   {
                                                                                (object)eventStatus.Identifier.ToString(FormatStringInteger),
                                                                                (object)description,
                                                                                (object)enableEventText,
                                                                                (object)streamTriggeredText,
                                                                                (object)eventStatus.CumulativeHistoryCount.ToString(),
                                                                                (object)eventStatus.RecentHistoryCount.ToString()
                                                                            });
            return dataGridViewRow;
        }

        /// <summary>
        /// Get the element of the <c>EventStatusList</c> property corresponding to the selected row of the <c>DataGridView</c> control.
        /// </summary>
        /// <param name="eventStatus">The event status element corresponding to the selected row of the <c>DataGridView</c> control.</param>
        /// <param name="dataGridViewRow">The selected row of the <c>DataGridView</c> control.</param>
        /// <returns>A flag to indicate whether an event status element matching the selected row of the DataGridView control was found. True, if a matching element 
        /// was found; otherwise, false.</returns>
        private bool GetEventStatusElement(out EventStatus_t eventStatus, out DataGridViewRow dataGridViewRow)
        {
            eventStatus = new EventStatus_t();
            dataGridViewRow = new DataGridViewRow();

            // Flag to indicate whether an event flag matching the selected row of the DataGridView control was found.
            bool matchFound = false;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return matchFound;
            }

            // Ensure that a row has been selected.
            if (m_DataGridViewEventStatus.SelectedRows.Count <= 0)
            {
                return matchFound;
            }

            // Get the selected row of the DataGridView control.
            dataGridViewRow = m_DataGridViewEventStatus.SelectedRows[0];
            if (dataGridViewRow != null)
            {
                // Get the event index of the event flag associated with the selected record.
                short eventIndex = short.Parse((string)dataGridViewRow.Cells[ColumnIndexEventIndex].Value);

                // Ensure that the event index is a positive value.
                if (eventIndex < 0)
                {
                    return matchFound;
                }

                // Find the event flag associated with the selected event index.
                eventStatus = m_EventStatusList.Find(delegate(EventStatus_t currentEventFlag)
                {
                    return (currentEventFlag.Identifier == eventIndex);
                });

                if (eventStatus.Identifier == eventIndex)
                {
                    matchFound = true;
                }
            }

            return matchFound;
        }
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

        /// <summary>
        /// Gets or sets the event status list associated with the current event log;
        /// </summary>
        internal List<EventStatus_t> EventStatusList
        {
            get { return m_EventStatusList; }
            set { m_EventStatusList = value; }
        }
        #endregion --- Properties ---
    }
}