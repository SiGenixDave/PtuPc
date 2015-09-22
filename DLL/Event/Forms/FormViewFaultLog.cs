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
 *  File name:  FormViewFaultLog.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/13/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  12/01/10    1.1     K.McD           1.  Modified to inherit from the FormViewSavedFaultLog class.
 * 
 *  01/06/11    1.2     K.McD           1.  Modified to inherit directly from the FormViewDataStream class.
 *                                      2.  Display both the F12-Info and F3-Save function keys and enable the F3-Save key.
 *                                      3.  Added the methods required to override the virtual methods defined in FormViewDataStream.
 *                                      4.  Modified the call to the DeriveName() method in the F3_Click() event handler to accommodate the signature change.
 * 
 *  02/03/11    1.3     K.McD           1.  Standardized the function key event handlers to: display the wait cursor, enable the Checked property of the function key and 
 *                                          clear any status message.
 * 
 *                                      2.  Modifications resulting from the name changes associated with a number of methods in the FileHandling class.
 * 
 *  02/21/11    1.4     K.McD           1.  Modified to accommodate the change in signature of the General.DeriveName() method.
 * 
 *  02/28/11    1.5     K.McD           1.  Auto-modified as a result of class name changes.
 *                                      2.  Modified the title text.
 *                                      3.  Added an override to the event handler for the Shown event of the parent class to modify the image and text of the 
 *                                          escape key ToolStrip button if this form was called from the form used to display the event log rather than the main 
 *                                          application window as under these circumstances the escape key will return the user to that form rather than home.
 * 
 *  03/28/11    1.6     K.McD           1.  Modified the constructor to include a try/catch block when determining the time of the trip.
 *  
 *  09/30/11    1.7     K.McD           1.  Replaced all references the inherited m_WatchFile variable with a reference to the inherited WatchFile property.
 *                                      2.  Removed the overridden PlotHistoricData() method as this is no longer required.
 *                                      
 *  10/26/11    1.8     K.McD           1.  SNCR002.41. Added checks to the event handlers associated with all ToolStripButton controls to ensure that the event handler 
 *                                          code is ignored if the Enabled property of the control is not asserted.
 *
 *	12/06/11	1.9		Sean.D			1.	Modified parameterized constructor to check to be sure that the tabpages are populated before setting the name of the first.
 *	
 *  07/16/13    1.10    K.McD           1.  Added try/catch block to the F3 function key event handler to prevent an exception being thrown in the event that the number of records 
 *                                          in m_HistoricDataManager.AllFrames is less that the TripIndex value of the current DataStream type. This can occur if the DataStreamType field 
 *                                          of the Logs table in the .E1 database is set up incorrectly.
 *                                          
 *  08/06/13    1.11    K.McD           1.  Disabled the 'F5-Edit' function key in the contructor as changing the plot layout is not supported on live fault log data. This feature is 
 *                                          only available once the fault log has been saved to disk.
 *                                          
 *                                      2.  Modified the 'Shown' event handler to hide the 'Remove Selected Plot(s)' context menu option as this is not supported on live fault log 
 *                                          data. This feature is only available once the fault log has been saved to disk.  
 *                                          
 *  
 *
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CodeProject.GraphComponents;

using Common;
using Common.Configuration;
using Common.Forms;
using Common.UserControls;
using Event.Properties;

namespace Event.Forms
{
    /// <summary>
    /// Form to plot the historic data associated with a downloaded fault log.
    /// </summary>
    public partial class FormViewFaultLog : FormDataStreamPlot
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormViewFaultLog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class. Initialize: (1) Any additional function keys or information labels that are required; (2) the <c>Form</c> and 
        /// <c>TabPage</c> titles; (3) the time axis and (4) the information label values.
        /// </summary>
        /// <param name="watchFile">The structure containing the fault log data that is to be plotted.</param>
        public FormViewFaultLog(WatchFile_t watchFile) 
            : base(watchFile)
        {
            InitializeComponent();

            #region - [Function Keys] -
            DisplayFunctionKey(F3, Resources.FunctionKeyTextSave, Resources.FunctionKeyToolTipSaveFaultLog, Resources.Save);
            F3.Enabled = true;

            // Disable the 'F5-Edit' function key as changing the plot layout is not supported on live fault log data. This feature is 
            // only available once the fault log has been saved to disk.
            F5.Enabled = false;
            #endregion - [Function Keys] -

            #region - [InformationLabels/Legend] -
            #endregion - [InformationLabels/Legend] -

            #region - [Title] -
            Text = string.Format(Resources.TitleFaultLog, WatchFile.DataStream.EventDescription);
            #endregion - [Title] 

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

            // Hide the 'Remove Selected Plot(s)' context menu option as this is not supported on live fault log data. This feature
            // is only available once the fault log has been saved to disk.

            // Reference to the TableLayoutPanel associated with each TabPage.
            TableLayoutPanel tableLayoutPanel;
            for (int index = 0; index < m_TabControl.TabPages.Count; index++)
            {
                tableLayoutPanel = m_TabControl.TabPages[index].Controls[KeyTableLayoutPanel] as TableLayoutPanel;
                for (int rowIndex = 0; rowIndex < tableLayoutPanel.RowCount; rowIndex++)
                {
                    IPlotterWatch plotterWatch = (tableLayoutPanel.Controls[rowIndex] as IPlotterWatch);
                    plotterWatch.RemoveSelectedPlot.Enabled = false;
                    plotterWatch.RemoveSelectedPlot.Visible = false;
                }
            }
        }
        #endregion - [Form] -

        #region - [Function Keys] -
        #region - [F3-Save] -
        /// <summary>
        /// Event handler for the F3 <c>Click</c> event. Save the fault log to disk.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F3_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F3.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F3.Checked = true;

            // Clear the status message.
            if (MainWindow != null)
            {
                MainWindow.WriteStatusMessage(string.Empty);
            }

            // Show the form that allows the user to add user comments to the header information.
            // Get the time of the trip.
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

            FormAddComments formAddComments = new FormAddComments(FileHeader.HeaderCurrent, tripTime);
            formAddComments.CalledFrom = this;
            formAddComments.ShowDialog();

            WatchFile_t watchFile = WatchFile;
            watchFile.Header = formAddComments.Header;
            WatchFile = watchFile;

            // ----------------------
            // Save the file to disk.
            // ----------------------
            // For consistency, use the DeriveName() method to derive the default filename of the simulated fault log file that is to be saved to disk.
            string defaultFilename = General.DeriveName(FileHeader.HeaderCurrent.TargetConfiguration.CarIdentifier, tripTime, CommonConstants.ExtensionFaultLog, WatchFile.DataStream.EventDescription);
            string fullyQualifiedFaulLogFile = General.FileDialogSaveFaultLog(defaultFilename, InitialDirectory.FaultLogsWrite);
            if (fullyQualifiedFaulLogFile != string.Empty)
            {
                // Serialize the data to the specified file.
                FileHandling.Serialize<WatchFile_t>(fullyQualifiedFaulLogFile, WatchFile, FileHandling.FormatType.Binary);

                // Update the initial directory with the path of the selected file.
                InitialDirectory.FaultLogsWrite = Path.GetDirectoryName(fullyQualifiedFaulLogFile);
            }

            F3.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F3-Save] -
        #endregion - [Function Keys] -
        #endregion --- Delegated Methods ---

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