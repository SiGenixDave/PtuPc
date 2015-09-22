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
 *  File name:  FormShowEventHistory.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/06/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  04/08/11    1.1     K.McD           1.  Removed some text that had been commented out.
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

using Common.Communication;
using Common.Configuration;
using Event.Communication;
using Event.Properties;

namespace Event.Forms
{
    /// <summary>
    /// A form to show the cumulative and recent number of events that have occurred for each of the available event types asociated with the current log.
    /// </summary>
    public partial class FormShowEventHistory : FormConfigureEventFlags
    {
        #region --- Constants ---
        #region - [Context Menu Item Indices] -
        /// <summary>
        /// The context menu item index associated with the 'Enabled' menu option. Value: 1.
        /// </summary>
        private const int ContextMenuItemIndexEnabled = 1;

        /// <summary>
        /// The context menu item index associated with the 'Stream Triggered' menu option. Value: 2.
        /// </summary>
        private const int ContextMenuItemIndexStreamTriggered = 2;
        #endregion - [Context Menu Item Indices] -
        #endregion --- Constants ---

        #region --- Constructors ---
        /// <summary>
        /// Initilize a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormShowEventHistory()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Initializes a new instance of the class. 
        /// </summary>
        /// <param name="communicationInterface">The communication interface that is to be used to communicate with the VCU.</param>
        /// <param name="log">The current event log.</param>
        public FormShowEventHistory(ICommunicationParent communicationInterface, Log log)
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
            m_DataGridViewTextColumnEnableEvent.Visible = false;
            m_DataGridViewTextColumnStreamTriggered.Visible = false;
            m_DataGridViewTextColumnCumulativeHistory.Visible = true;
            m_DataGridViewTextColumnRecentHistory.Visible = true;

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

            #region - [Buttons] -
            // Only the OK button is required for this form, move the position of the OK button so that it hides the cancel button.
            m_ButtonCancel.Visible = false;
            m_ButtonOK.Location = m_ButtonCancel.Location;
            #endregion - [Buttons] -

            #region - [Context Menu] -
            // Disable those context menu options that are not applicable to this form.
            m_ContextMenuStripFlags.Items[ContextMenuItemIndexEnabled].Visible = false;
            m_ContextMenuStripFlags.Items[ContextMenuItemIndexStreamTriggered].Visible = false;
            #endregion - [Context Menu] -

            // ------------------------------------------------------------------
            // Get the list of events associated with the current event log.
            // ------------------------------------------------------------------
            List<EventRecord> foundEventRecordList = Lookup.EventTable.RecordList.FindAll(delegate(EventRecord eventRecord)
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

            try
            {
                FormGetFltHistInfo formGetFltHistInfo = new FormGetFltHistInfo(CommunicationInterface, foundEventRecordList, log);
                formGetFltHistInfo.CalledFrom = this;
                formGetFltHistInfo.ShowDialog();
            }
            catch (Exception)
            {
                throw new CommunicationException(Resources.EMGetFltHistInfoFailed);
            }

            AddList(EventStatusList);
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
    }
}