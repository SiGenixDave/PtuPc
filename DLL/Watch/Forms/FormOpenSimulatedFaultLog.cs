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
 *  File name:  FormOpenSimulatedFaultLog.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/16/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/06/11    1.1     K.McD           1.  Added a number of constants.
 *                                      2.  Display both the F12-Info and F3-Save function keys, however, disable the F3-Save key.
 * 
 *  01/26/11    1.2     K.McD           1.  Modified the constructor to accommodate the restructuring of the DataStream_t structure.
 * 
 *  02/27/11    1.3     K.McD           1.  Renamed this form and modified the title.
 * 
 *  03/28/11    1.4     K.McD           1.  Modified the constructor to include a try/catch block when determining the trip time.
 *  
 *  05/23/11    1.5     K.McD           1.  Applied the 'Organize Usings/Remove and Sort' context menu option.
 *                                      2.  Corrected the XML tag associated with the InitialzePlotterRangeSelection() method.
 *                                      
 *  09/31/11    1.6     K.McD           1.  Replaced any references to the inherited m_WatchFile variable with the inherited WatchFile property reference.
 *                                      2.  Removed the overridden PlotHistoricData() method as this is no longer required.
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Windows.Forms;
using CodeProject.GraphComponents;

using Common;
using Common.Forms;
using Common.UserControls;
using Watch.Properties;

namespace Watch.Forms
{
    /// <summary>
    /// Form to plot the historic data associated with a saved simulated fault log.
    /// </summary>
    public partial class FormOpenSimulatedFaultLog : FormDataStreamPlot
    {
        #region --- Constants ---
        /// <summary>
        /// The time resolution, in ms, of the log start and stop times.
        /// </summary>
        /// <remarks>
        /// The time is currently rounded down to the nearest resoulution i.e. 10.479 would be rounded down to 10.470 ms.
        /// </remarks>
        private const short ResolutionLogFileMs = 10;
        #endregion --- Constants ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormOpenSimulatedFaultLog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class. Initialize: (1) Any additional function keys or information labels that are required; (2) the <c>Form</c> and 
        /// <c>TabPage</c> titles; (3) the time axis and (4) the information label values.
        /// </summary>
        /// <param name="watchFile">The structure containing the fault log data that is to be plotted.</param>
        public FormOpenSimulatedFaultLog(WatchFile_t watchFile)
            : base(watchFile)
        {
            InitializeComponent();

            #region - [Function Keys] -
            DisplayFunctionKey(F3, Resources.FunctionKeyTextSave, Resources.FunctionKeyToolTipSave, Resources.Save);
            F3.Enabled = false;
            DisplayFunctionKey(F12, Resources.FunctionKeyTextInfo, Resources.FunctionKeyToolTipInfo, Resources.FileInformation);
            #endregion - [Function Keys] -

            #region - [InformationLabels/Legend] -
            #endregion - [InformationLabels/Legend] -

            #region - [Title] -
            Text = string.Format(Resources.TitleOpenSimulatedFaultLog, WatchFile.Filename);
            #endregion - [Title] -

            // Display any available supplemental information.
            m_PanelSupplementalInformation.Visible = true;
            m_LabelSupplementalInformation.Text = WatchFile.DataStream.Workset.Name;
            m_LegendSupplementalInformation.Text = Resources.LegendWorkset;

            // Initialize the structure used to manage the plotter range.
            DateTime tripTime;
            try
            {
                tripTime = m_HistoricDataManager.AllFrames[WatchFile.DataStream.DataStreamTypeParameters.TripIndex].CurrentDateTime;
            }
            catch (Exception)
            {
                TimeSpan postTripMs = new TimeSpan(0, 0, 0, 0, WatchFile.DataStream.DurationPostTripMs);
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

            // Round the start time down to the nearest resolution.
            int msComponent = m_HistoricDataManager.FramesToDisplay[0].CurrentDateTime.Millisecond;
            int adjust = msComponent % ResolutionLogFileMs;
            PlotterRangeSelection.InitialStartTime = m_HistoricDataManager.FramesToDisplay[0].CurrentDateTime.Subtract(new TimeSpan(0, 0, 0, 0, adjust));

            // Round the stop time up to the nearest resolution.
            msComponent = m_HistoricDataManager.FramesToDisplay[m_HistoricDataManager.FramesToDisplay.Count - 1].CurrentDateTime.Millisecond;
            adjust = msComponent % ResolutionLogFileMs;
            if (adjust > 0)
            {
                adjust = PlotterRangeSelection.DataIntervalMs - adjust;
            }
            PlotterRangeSelection.InitialStopTime = m_HistoricDataManager.FramesToDisplay[m_HistoricDataManager.FramesToDisplay.Count - 1].CurrentDateTime.Add(new TimeSpan(0, 0, 0, 0, adjust));

            // Set the start time of the trip recording to be the exact duration of the fault-log before the stop time.
            int exactDurationMs = WatchFile.DataStream.DurationMs - WatchFile.DataStream.FrameIntervalMs;
            PlotterRangeSelection.InitialStartTime = PlotterRangeSelection.InitialStopTime.Subtract(new TimeSpan(0, 0, 0, (int)(exactDurationMs / 1000), (int)(exactDurationMs % 1000)));

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