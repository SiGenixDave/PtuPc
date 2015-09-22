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
 *  File name:  MenuInterfaceWatch.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/29/10    1.0     K.McDonald      First Release.
 * 
 *  08/20/10    1.1     K.McD           1.  Modified method OpenDataStreamFile() to ensure that the wait cursor on the main application window was displayed correctly.
 * 
 *  10/06/10    1.2     K.McD           1.  Ensure that ALL child forms are closed before calling any of the menu interface methods.
 * 
 *  10/25/10    1.3     K.McD           1.  Added the ReplaceWatch() method to allow the user to modify the workset that is currently on display.
 * 
 *  10/29/10    1.4     K.McD           1.  Modified the ReplaceWatch() method so that it calls the form to define the watch variables using the constructor that
 *                                          shows the Apply button.
 * 
 *  11/15/10    1.5     K.McD           1.  Now sets the cursor to the wait cursor before closing any child forms that may be open on all appropriate menu interface
 *                                          calls.
 *                                      2.  Renamed a number of variables.
 *                                      3.  Changes resulting from the change to the WatchFile_t structure.
 * 
 *  11/17/10    1.6     K.McD           1.  Changes resulting from the replacement of the WorksetManager class.
 * 
 *  11/19/10    1.7     K.McD           1.  Added the ConfigureWorksetsWatchWindow() method to configure the worksets used to view and record watch variables.
 * 
 *  12/01/10    1.8     K.McD           1.  Modifications resulting from the renaming of a number of external constants and properties.
 *                                      2.  The OpenDataStreamFile() method now inherits from the parent class.
 * 
 *  01/06/11    1.9     K.McD           1.  Modifications resulting from the renaming of the OpenDtaStreamFile() method to ShowDAtaStream().
 * 
 *  01/27/11    1.10    K.McD           1.  Bug fix - SNCR001.88. Modified the ShowDataStreamFile() method such that the selected data stream file is only shown 
 *                                          if the selected file is valid.
 * 
 *  02/27/11    1.11    K.McD           1.  Auto-modified as a result of a number of class name changes.
 *                                      2.  Renamed a number of local variables.
 * 
 *  04/27/11    1.12    K.McD           1.  Added the methods associated with the configuration of the chart recorder and the setting of the current mode of the 
 *                                          chart recorder.
 *                                          
 *  07/20/11    1.13    K.McD           1.  Added the SetChartMode() method and modified the ConfigureChartModeFullScale, ConfigureChartModeZeroOutput(), 
 *                                          ConfigureChartModeRamp() and ConfigureChartModeData() methods to call this method. This method checks the mode of 
 *                                          the PTU and instantiates the appropriate ICommunicationWatch interface.
 *                                          
 *  08/07/13    1.14    K.McD           1.  In those methods where it is applicable, the cursor was set to the wait cursor after the call to the
 *                                          MainWindow.CloseChildForms() method as if any child forms are open, the cursor may be set to the default cursor as part of
 *                                          the call to the Exit() method within the child form.
 *                                          
 *  04/16/15    1.15    K.McD           References
 *              
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                          
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, Kawasaki Rail Car and NYTC on
 *                                              12th April 2013 - MOC-0171:
 *                                              
 *                                              1.  MOC-0171-06. All references to 'Fault Logs', including menu options and directory names to be replaced by
 *                                                  'Data Streams' for all projects.
 *
 *                                          2.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be
 *                                              modified to meet the KRC specification. This modification will be specific to the KRC project; for all other projects,
 *                                              the current naming convention will still apply.
 *                                      
 *                                      Modifications
 *                                      1.  Auto-update following renaming of the FileHandling.LogType.SimulatedDataStream enumerator. Ref.: 1.1.1, 1.2.
 *
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Common;
using Common.Communication;
using Common.Configuration;
using Common.Forms;
using Watch.Communication;
using Watch.Forms;
using Watch.Properties;

namespace Watch
{
    /// <summary>
    /// Methods called by the menu options associated with the viewing and recording of watch variables - Watch.dll.
    /// </summary>
    public class MenuInterfaceWatch : MenuInterface
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="mainWindow">Reference to the main application window interface.</param>
        public MenuInterfaceWatch(IMainWindow mainWindow) : base(mainWindow)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        #region - [File] -
        /// <summary>
        /// Call the <c>OpenDataStreamFile</c> method specifying the parameters associated with a simulated fault log.
        /// </summary>
        public void OpenSimulatedFaultLog()
        {
            MainWindow.CloseChildForms();
            MainWindow.Cursor = Cursors.WaitCursor;

            try
            {
                ShowDataStreamFile(Resources.FileDialogOpenTitleSimulatedFaultLog, CommonConstants.ExtensionSimulatedFaultLog,
                                   Resources.FileDialogOpenFilterSimulatedFaultLog, InitialDirectory.SimulatedFaultLogsRead);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Call the <c>OpenDataStreamFile</c> method specifying the parameters associated with a recorded watch file.
        /// </summary>
        public void OpenRecordedWatchFile()
        {
            MainWindow.CloseChildForms();
            MainWindow.Cursor = Cursors.WaitCursor;

            try
            {
                ShowDataStreamFile(Resources.FileDialogOpenTitleRecordedWatchFile, CommonConstants.ExtensionWatchFile,
                                   Resources.FileDialogOpenFilterRecordedWatchFile, InitialDirectory.WatchFilesRead);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Ask the user to select a data-stream file, de-serialized this to the appropriate object type and then display the data using the 
        /// <c>FormDataStreamPlot</c> class.
        /// </summary>
        /// <param name="title">The title that is to appear on the <c>OpenFileDialog</c> form.</param>
        /// <param name="defaultExtension">The default extension associated with the type of log.</param>
        /// <param name="filterText">The filter text. Used to filter the list of available files.</param>
        /// <param name="initialDirectory">The initial directory that will be show.</param>
        /// <returns>A flag to indicate whether a valid watch file was selected. True, indicates that the selected file was valid; otherwise, false.</returns>
        public override bool ShowDataStreamFile(string title, string defaultExtension, string filterText, string initialDirectory)
        {
            // Default to the selected file being invalid.
            bool selectedFileIsValid = false;

            selectedFileIsValid = base.ShowDataStreamFile(title, defaultExtension, filterText, initialDirectory);
            if (selectedFileIsValid == true)
            {
                // The data-stream file is valid , display it using the appropriate form.
                MainWindow.Cursor = Cursors.WaitCursor;

                FormDataStreamPlot formDataStreamPlot;
                if (m_WatchFile.DataStream.LogType == LogType.SimulatedDataStream)
                {
                    formDataStreamPlot = new FormOpenSimulatedFaultLog(m_WatchFile);
                    MainWindow.ShowMdiChild(formDataStreamPlot);
                }
                else if (m_WatchFile.DataStream.LogType == LogType.Watch)
                {
                    formDataStreamPlot = new FormOpenWatch(m_WatchFile);
                    MainWindow.ShowMdiChild(formDataStreamPlot);
                }
                else
                {
                    MessageBox.Show(Resources.MBTLogFileTypeNotSupported, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MainWindow.Cursor = Cursors.Default;
            }

            return selectedFileIsValid;
        }
        #endregion - [File] -

        #region - [View] -
        /// <summary>
        /// Show the multiple document interface child form which displays and records the watch variables.
        /// </summary>
        public void ViewWatchWindow()
        {
            MainWindow.CloseChildForms();
            MainWindow.Cursor = Cursors.WaitCursor;
            
            try
            {
                FormViewWatch formViewViewWatch = new FormViewWatch(MainWindow.CommunicationInterface);
                MainWindow.ShowMdiChild(formViewViewWatch);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Show the dialog form to allow the user to modify the current workset.
        /// </summary>
        public void ReplaceWatch()
        {
            MainWindow.Cursor = Cursors.WaitCursor;
            try
            {
                // Call the constructor that displays the Apply button on the form.
                FormWorksetDefineWatch formWorksetDefineWatch = new FormWorksetDefineWatch(Workset.RecordedWatch, Workset.RecordedWatch.ActiveWorkset, true);
                MainWindow.ShowDialog(formWorksetDefineWatch);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion - [View] -

        #region - [Configure] -
        #region - [Worksets] -
        /// <summary>
        /// Show the form which allows the user the configure the worksets associated with the viewing and recording of watch variables.
        /// </summary>
        public void ConfigureWorksetsWatchWindow()
        {
            MainWindow.Cursor = Cursors.WaitCursor;
            try
            {
                FormWorksetManagerWatch formWorksetManagerWatch = new FormWorksetManagerWatch(Workset.RecordedWatch);
                MainWindow.ShowDialog(formWorksetManagerWatch);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Show the form which allows the user the configure the worksets associated with the chart recorder.
        /// </summary>
        public void ConfigureWorksetsChartRecorder()
        {
            MainWindow.Cursor = Cursors.WaitCursor;
            try
            {
                FormWorksetManagerChartRecorder formWorksetManagerChartRecorder = new FormWorksetManagerChartRecorder(Workset.ChartRecorder);
                MainWindow.ShowDialog(formWorksetManagerChartRecorder);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion - [Worksets] -

        #region - [Chart Recorder/Chart Mode] -
        /// <summary>
        /// Show the form which allows the user the configure the chart recorder.
        /// </summary>
        public void ConfigureChartRecorder()
        {
            MainWindow.Cursor = Cursors.WaitCursor;

            // The PTU must be in on-line mode in order to configure the chart recorder.
            Debug.Assert(MainWindow.CommunicationInterface != null, "MenuInterfaceWatch.ConfigureChartRecorder() - [MainWindow.CommunicationInterface != null]");

            try
            {
                FormConfigureChartRecorder formConfigureChartRecorder = new FormConfigureChartRecorder(MainWindow.CommunicationInterface, MainWindow, Workset.ChartRecorder);
                MainWindow.ShowDialog(formConfigureChartRecorder);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Set the chart recorder mode to be data mode.
        /// </summary>
        public void ConfigureChartModeData()
        {
            SetChartMode(ChartMode.DataMode);
        }

        /// <summary>
        /// Set the chart recorder mode to be ramp mode.
        /// </summary>
        public void ConfigureChartModeRamp()
        {
            SetChartMode(ChartMode.RampMode);
        }

        /// <summary>
        /// Set the chart recorder mode to be zero output.
        /// </summary>
        public void ConfigureChartModeZeroOutput()
        {
            SetChartMode(ChartMode.ZeroOutputMode);
        }

        /// <summary>
        /// Set the chart recorder mode to be full scale.
        /// </summary>
        public void ConfigureChartModeFullScale()
        {
            SetChartMode(ChartMode.FullScaleMode);
        }

        /// <summary>
        /// Set the mode of the chart recorder to the specified mode.
        /// </summary>
        /// <param name="chartMode">The requested chart recorder mode.</param>
        private void SetChartMode(ChartMode chartMode)
        {
            ICommunicationWatch communicationInterface;
            if (MainWindow.CommunicationInterface.CommunicationSetting.Protocol == Protocol.SIMULATOR)
            {
                communicationInterface = new CommunicationWatchOffline(MainWindow.CommunicationInterface);
            }
            else
            {
                communicationInterface = new CommunicationWatch(MainWindow.CommunicationInterface);
            }

            PauseCommunication<ICommunicationWatch>(communicationInterface, true);
            communicationInterface.SetChartMode(chartMode);
            PauseCommunication<ICommunicationWatch>(communicationInterface, false);
            return;
        }
        #endregion - [Chart Recorder/Chart Mode] -
        #endregion - [Configure] -
        #endregion --- Methods ---
    }
}
