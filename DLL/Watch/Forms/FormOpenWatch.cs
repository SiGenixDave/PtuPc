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
 *  File name:  FormOpenWatch.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/16/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/06/11    1.1     K.McD           1.  Display both the F12-Info and F3-Save function keys, however, disable the F3-Save key.
 *                                      2.  Modified the title.
 * 
 *  02/27/11    1.2     K.McD           1.  Renamed this form and modified the title.
 *  
 *  05/23/11    1.3     K.McD           1.  Corrected the  XML tag associated with the InitializePlotterRangeSelection() method.
 *  
 *  09/30/11    1.4     K.McD           1.  Replaced any references to the inherited m_WatchFile variable with the inherited WatchFile property reference.
 *  
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Windows.Forms;
using CodeProject.GraphComponents;

using Common;
using Common.Configuration;
using Common.Forms;
using Watch.Properties;

namespace Watch.Forms
{
    /// <summary>
    /// Form to plot the historic watch variable values contained within a recorded watch variable data stream.
    /// </summary>
    public partial class FormOpenWatch : FormDataStreamPlot
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormOpenWatch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class. Initialize: (1) Any additional function keys or information labels that are required; (2) the <c>Form</c> and 
        /// <c>TabPage</c> titles; (3) the time axis and (4) the information label values.
        /// </summary>
        /// <param name="watchFile">The structure containing the fault log data that is to be plotted.</param>
        public FormOpenWatch(WatchFile_t watchFile)
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
            Text = string.Format(Resources.TitleOpenWatchFile, WatchFile.Filename);
            #endregion - [Title] -

            // Display any available supplemental information.
            m_PanelSupplementalInformation.Visible = true;
            m_LabelSupplementalInformation.Text = WatchFile.DataStream.Workset.Name;
            m_LegendSupplementalInformation.Text = Resources.LegendWorkset;

            // The trip time is not relevant to recorded watch variables therefore set it to the invalid state.
            DateTime tripTime = Parameter.InvalidDateTime;

            // Initialize the structure used to manage the plotter range.
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
            PlotterRangeSelection.InitialStartTime = m_HistoricDataManager.FramesToDisplay[0].CurrentDateTime;
            PlotterRangeSelection.InitialStopTime = m_HistoricDataManager.FramesToDisplay[m_HistoricDataManager.FramesToDisplay.Count - 1].CurrentDateTime;

            // Copy the initial values to the current values and calculate the time span.
            PlotterRangeSelection.Reset();
        }

        /// <summary>
        /// Plot the historic data for all watch variables associated with the specified <c>TabControl</c>. Include a check for breaks in communication with the VCU.
        /// </summary>
        protected override void PlotHistoricData(TabControl tabControl)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            for (int tabIndex = 0; tabIndex < tabControl.TabPages.Count; tabIndex++)
            {
                tableLayoutPanel = tabControl.TabPages[tabIndex].Controls[KeyTableLayoutPanel] as TableLayoutPanel;
                Debug.Assert(tableLayoutPanel != null);

                m_PlotterControlLayout.ResetChannel(tableLayoutPanel.Controls);

                // Check for breaks in communication with the VCU.
                m_PlotterControlLayout.SetBreakpoints(tableLayoutPanel.Controls, PlotterRangeSelection.StartTime, m_HistoricDataManager);

                long[] elapsedTime = m_PlotterControlLayout.GetElapsedTimes(PlotterRangeSelection.StartTime, m_HistoricDataManager);
                m_PlotterControlLayout.PlotWatchValues(tableLayoutPanel.Controls, m_HistoricDataManager, elapsedTime);
            }
        }
        #endregion --- Methods ---
    }
}