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
 *  File name:  FormOpenFaultLog.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/29/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/06/11    1.1     K.McD           1.  Display both the F12-Info and F3-Save function keys, however, disable the F3-Save key.
 *                                      2.  Included the selected filename in the title.
 * 
 *  01/26/11    1.2     K.McD           1.  Modified to the constructor to accommodate the restructuring of the DataStream_t structure when determining the trip time 
 *                                          of the log.
 * 
 *  02/03/11    1.3     K.McD           1.  Bug fix. Modified the constructor to ensure thet the F3-Save function key is disabled.
 * 
 *  02/28/11    1.4     K.McD           1.  Renamed this form.
 *                                      2.  Auto-modified as a result of class name changes.
 *                                      3.  Modified the title text.
 *                                      4.  Added an override to the event handler for the Shown event of the parent class to modify the image and text of the 
 *                                          escape key ToolStrip button if this form was called from the form used to display the event log rather than the main 
 *                                          application window as under these circumstances the escape key will return the user to that form rather than home.
 * 
 *  03/28/11    1.5     K.McD           1.  Modified the constructor to include a try/catch block when determining the time of the trip.
 *  
 *  09/30/11    1.6     K.McD           1.  Replaced all references the inherited m_WatchFile variable with a reference to the inherited WatchFile property.
 *                                      2.  Removed the overridded PlotHistoricData() method as this is no longer required. 
 *
 *	12/06/11	1.7		Sean.D			1.	Modified parameterized constructor to check to be sure that the tabpages are populated before setting the name of the first.
 *
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Windows.Forms;
using CodeProject.GraphComponents;

using Common;
using Common.Forms;
using Common.UserControls;
using Event.Properties;

namespace Event.Forms
{
    /// <summary>
    /// Form to plot the historic data associated with a saved fault log.
    /// </summary>
    public partial class FormOpenFaultLog : FormDataStreamPlot
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormOpenFaultLog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class. Initialize: (1) Any additional function keys or information labels that are required; (2) the <c>Form</c> and 
        /// <c>TabPage</c> titles; (3) the time axis and (4) the information label values.
        /// </summary>
        /// <param name="watchFile">The structure containing the fault log data that is to be plotted.</param>
        public FormOpenFaultLog(WatchFile_t watchFile)
            : base(watchFile)
        {
            InitializeComponent();

            #region - [Function Keys] -
            DisplayFunctionKey(F3, Resources.FunctionKeyTextSave, Resources.FunctionKeyToolTipSaveFaultLog, Resources.Save);
            F3.Enabled = false;
            DisplayFunctionKey(F12, Resources.FunctionKeyTextInfo, Resources.FunctionKeyToolTipInfo, Resources.FileInformation);
            #endregion - [Function Keys] -

            #region - [InformationLabels/Legend] -
            #endregion - [InformationLabels/Legend] -

            #region - [Title] -
            Text = string.Format(Resources.TitleOpenFaultLog, WatchFile.Filename);
            #endregion - [Title] -

			// Tab page title.
			if (m_TabControl != null && m_TabControl.TabPages.Count > 0)
			{
				m_TabControl.TabPages[0].Text = watchFile.DataStream.EventDescription;
			}

            // Initialize the structure used to manage the plotter range.
            DateTime tripTime;
            try
            {
                tripTime = m_HistoricDataManager.AllFrames[WatchFile.DataStream.DataStreamTypeParameters.TripIndex].CurrentDateTime;
            }
            catch (Exception)
            {
                TimeSpan postTripMs = new TimeSpan(0, 0, 0, WatchFile.DataStream.DurationPostTripMs);
                tripTime = m_HistoricDataManager.AllFrames[m_HistoricDataManager.AllFrames.Count - 1].CurrentDateTime.Subtract(postTripMs);
            }

            InitializePlotterRangeSelection(tripTime);

            // Update the status labels with the plotter range information.
            UpdateStatusLabels();
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

                // Method called by consumer code. Call the Dispose method of any managed data members that implement the dispose method.
                // Cleanup managed objects by calling their Dispose() methods.
                if (disposing)
                {
                    if (components != null)
                    {
                        components.Dispose();
                    }
                }
                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data 
                // members to null.

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
        #region - [Form] -
        /// <summary>
        /// Event handler for the form <c>Shown</c> event.  Before calling the <c>Shown</c> event handler associated with the parent form, check whether this form was 
        /// called from the form used to display the event log and, if so, modify the image and text associated with the escape key as if this is the case, pressing 
        /// this key should return the user to the event log form rather than home.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void FormDataStreamPlot_Shown(object sender, EventArgs e)
        {
            FormViewEventLog calledFromAsFormViewEventLog = CalledFrom as FormViewEventLog;
            if (calledFromAsFormViewEventLog != null)
            {
                DisplayFunctionKey(Escape, Resources.FunctionKeyTextEsc, Resources.FunctionKeyToolTipEsc, Resources.Edit_Undo);
            }

            base.FormDataStreamPlot_Shown(sender, e);
        }
        #endregion - [Form] -
        #endregion --- Delegated Methods --

        #region --- Methods ---
        /// <summary>
        /// Initialize the <see cref="PlotterRangeSelection"/> structure using the watch variable data loaded into the <see cref="HistoricDataManager"/> class. 
        /// If the data to be displayed is not a fault log or a simulated fault log, <paramref name="tripTime"/> will be ignored.
        /// </summary>
        /// <param name="tripTime">The time of the trip if the data represents a fault log or a simulated fault log; otherwise DateTime.Now.</param>
        protected override void InitializePlotterRangeSelection(DateTime tripTime)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            PlotterRangeSelection.DataIntervalMs = WatchFile.DataStream.FrameIntervalMs;
            PlotterRangeSelection.TripTime = tripTime;
            PlotterRangeSelection.InitialStartTime = m_HistoricDataManager.FramesToDisplay[0].CurrentDateTime;
            PlotterRangeSelection.InitialStopTime = m_HistoricDataManager.FramesToDisplay[m_HistoricDataManager.FramesToDisplay.Count - 1].CurrentDateTime;

            // Copy the initial values to the current values and calculate the time span.
            PlotterRangeSelection.Reset();
        }

        /// <summary>
        /// Set the TripTime property of the plotter controls associated with the specified <c>TabControl</c>.
        /// </summary>
        /// <param name="tabControl">The <c>TabControl</c> that is to be processed.</param>
        /// <param name="tripTime">The time of the trip that initiated the log.</param>
        protected override void SetTripTime(TabControl tabControl, DateTime tripTime)
        {
            // Reference to the TableLayoutPanel associated with each TabPage.
            TableLayoutPanel tableLayoutPanel;
            for (int index = 0; index < tabControl.TabPages.Count; index++)
            {
                tableLayoutPanel = tabControl.TabPages[index].Controls[KeyTableLayoutPanel] as TableLayoutPanel;
                for (int rowIndex = 0; rowIndex < tableLayoutPanel.RowCount; rowIndex++)
                {
                    (tableLayoutPanel.Controls[rowIndex] as IPlotterWatch).IsFaultLog = true;
                    (tableLayoutPanel.Controls[rowIndex] as IPlotterWatch).TripTime = tripTime;
                }
            }
        }
        #endregion --- Methods ---
    }
}