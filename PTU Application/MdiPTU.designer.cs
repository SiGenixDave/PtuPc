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
 *  Project:    PTU Application
 * 
 *  File name:  MdiPTU.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McDonald      First Release.
 * 
 *  08/18/10    1.1     K.McD           1.  Added the self-test menu options.
 * 
 *  08/18/10    1.2     K.McD           1.  Removed a number of menu options that were no longer required.
 * 
 *  09/30/10    1.3     K.McD           1.  Registered an event handler for the Click event associated with the 'Configuration/Enumeration' menu option.
 * 
 *  10/11/10    1.4     K.McD           1.  Bug fix SNCR001.27. Ensure that any menu options that are not yet implemented display a message box informing the user of
 *                                          their current status.
 * 
 *  10/11/10    1.5     K.McD           1.  Corrected the location of the user message status strip back to (390,0). The reason why this keeps changing is, as yet,
 *                                          unclear.
 * 
 *  02/14/11    1.6     K.McD           1.  Included to 'Tools/Debug Mode' menu option.
 *                                      2.  Removed the legend associated with the data update digital user control.
 *                                      3.  Removed the legend associated with the progress bar.
 * 
 *  02/28/11    1.7     K.McD           1.  Auto-modified as a result of the changes to the menu system.
 *  
 *  07/25/11    1.8     K.McD           1.  Implemented the following changes in an attempt to resolve the problem of returning focus to the function 
 *                                          keys after the help window has been displayed during the interactive tests.
 *                                              (a) The TabStop property of the ToolStripFunctionKeys control was set to true.
 *                                              (b) The DoubleBuffered property of the form was set to true.
 *                                              
 *  08/25/11    1.9     K.McD           1.  Removed the diagnostic mode ToolStripButton control.
 *                                      2.  Added support for the KeyDown and KeyUp events.
 *                                      
 *  10/26/11    1.10    K.McD           1.  Removed the 'File/Open/Screen Capture' menu option and associated separator.
 *  
 *  06/13/15    1.11    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                          
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘WibuBox: [Present | Not Present]’.
 *                                      
 *                                      Modifications
 *                                      1.  Auto-Update as result of name changes to the ToolStripStatusLabel and StatusStrip controls. Ref.: 1.1.
 *                                      2.  Changed the definition of the TaskProgressBar property to ToolStripProgressBar. Ref.: 1.1.
 *                                      3.  Changed the Location property of the following controls:
 *                                      
 *                                          1.  m_LegendRx                      - (1040,10)
 *                                          2.  m_DigitalControlPacketReceived  - (1060,7)
 *                                          3.  m_PictureBoxBusy                - (1009,4)
 *                                          
 *  07/27/15    1.12    K.McD           References
 *                                      1.  Part 1 of the upgrade to the Chicago 5000 PTU software that allows the user to download the configuration and help files for
 *                                          a particular Chicago 5000 vehicle control unit (VCU) via an ethernet connection to the FTP (File Transfer Protocol) server
 *                                          running on the VCU. The scope of Part 1 of the upgrade is defined in purchase order 4800011369-CU2 07.07.2015.
 *                                      
 *                                          The upgrade is implemented in two parts, the first part, Part 1, replaces the existing screens and logic with those outlined
 *                                          in slides 6, 7, 8 and 9 of the PowerPoint presentation '076_CTA - PTU file pullback from VCU - 20150127.pptx', but does NOT
 *                                          implement the file transfer; it merely calls an empty external batch file from within the PTU application. The second stage,
 *                                          Part 2, implements the batch file that downloads the configuration and help files from the Vehicle Control Unit (VCU) to the
 *                                          appropriate directory on the PTU computer. As described in the PowerPoint Presentation, this download is only carried out
 *                                          if the appropriate configuration file is not already present on the PTU computer.
 *                                      
 *                                      Modifications
 *                                      1.  Removed the m_LegendProgressBar ToolStripStatusLabel and the m_ProgressBar ToolStripProgressBar controls. The progress
 *                                          bar used to display the recording and playback of data streams now appears in the 'Information' Panel of the FormWatch
 *                                          Form. The progress bar was moved to allow the status message display to be extended to support some of the longer messages
 *                                          required to support the upgrade shown above.
 */
#endregion --- Revision History ---

namespace Bombardier.PTU
{
    partial class MdiPTU
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region --- Disposal ---
        /// <summary>
        /// Disposes of the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            lock (this)
            {
                if (m_IsDisposed == false)
                {
                    Cleanup(disposing);
                    m_IsDisposed = true;

                    base.Dispose(disposing);

                    // Because the Dispose method performs all necessary cleanup, ensure the garbage collector does not call the class destructor.
                    System.GC.SuppressFinalize(this);
                }
            }
        }
        #endregion --- Disposal ---

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiPTU));
            this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemFileOpenWatchFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorFileOpenWatchEvaluateVariables = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemFileOpenSimulatedFaultLog = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemFileOpenFaultLog = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemFileOpenEventLog = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorFileOpenEnd = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemFileSelectDataDictionary = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorFileOpenDataDictionary = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorFileEnd = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemViewWatchWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorViewWatchWindow = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemViewSystemInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorViewEnd = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemDiagnostics = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemDiagnosticsSelfTests = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorDiagnosticsSelfTest = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemDiagnosticsEventLog = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorDiagnosticsEventLog = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemDiagnosticsInitializeEventLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorDiagnosticsEnd = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemConfigure = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureWorksets = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureWorksetsWatchWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureWorksetsFaultLog = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureWorksetsChartRecorder = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorConfigureWorksets = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemConfigureRealTimeClock = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorConfigureRealTimeClock = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemConfigurePasswordProtection = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorConfigurePasswordProtection = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemConfigureChartRecorder = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureChartMode = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureChartModeData = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureChartModeRamp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureChartModeZeroOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemConfigureChartModeFullScale = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorConfigureChartMode = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemConfigureEnumeration = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorConfigureEnd = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemToolsConvertEngineeringFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorToolsConvertEngineeringDatabase = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemToolsDebugMode = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorToolsDebug = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorToolsEnd = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemHelpPTUHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorHelpPTUHelp = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemHelpAboutPTU = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SeparatorHelpEnd = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuItemLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.m_PanelStatus = new System.Windows.Forms.Panel();
            this.m_PictureBoxBusy = new System.Windows.Forms.PictureBox();
            this.m_LegendRx = new System.Windows.Forms.Label();
            this.m_StatusStrip = new System.Windows.Forms.StatusStrip();
            this.m_StatusLabelMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusLabelCarNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusLabelSecurityLevel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusLabelLogStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusLabelWibuBoxStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_LegendStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusLabelMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_TSBOnline = new System.Windows.Forms.ToolStripButton();
            this.m_SeparatorOnline = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripFunctionKeys = new System.Windows.Forms.ToolStrip();
            this.m_SeparatorOnlineLHS = new System.Windows.Forms.ToolStripSeparator();
            this.m_SeparatorOfflineLHS = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBOffline = new System.Windows.Forms.ToolStripButton();
            this.m_SeparatorOffline = new System.Windows.Forms.ToolStripSeparator();
            this.m_DigitalControlPacketReceived = new Common.UserControls.DigitalControl();
            this.m_MenuStrip.SuspendLayout();
            this.m_PanelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxBusy)).BeginInit();
            this.m_StatusStrip.SuspendLayout();
            this.m_ToolStripFunctionKeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MenuStrip
            // 
            this.m_MenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.m_MenuStrip.BackgroundImage = global::Bombardier.PTU.Properties.Resources.MetallicRaised;
            this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemFile,
            this.m_MenuItemView,
            this.m_MenuItemDiagnostics,
            this.m_MenuItemConfigure,
            this.m_MenuItemTools,
            this.m_MenuItemHelp,
            this.m_MenuItemLogin});
            resources.ApplyResources(this.m_MenuStrip, "m_MenuStrip");
            this.m_MenuStrip.Name = "m_MenuStrip";
            this.m_MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // m_MenuItemFile
            // 
            this.m_MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemFileOpen,
            this.m_MenuItemFileSelectDataDictionary,
            this.m_SeparatorFileOpenDataDictionary,
            this.m_MenuItemFileExit,
            this.m_SeparatorFileEnd});
            this.m_MenuItemFile.Name = "m_MenuItemFile";
            resources.ApplyResources(this.m_MenuItemFile, "m_MenuItemFile");
            // 
            // m_MenuItemFileOpen
            // 
            this.m_MenuItemFileOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemFileOpenWatchFile,
            this.m_SeparatorFileOpenWatchEvaluateVariables,
            this.m_MenuItemFileOpenSimulatedFaultLog,
            this.m_MenuItemFileOpenFaultLog,
            this.m_MenuItemFileOpenEventLog,
            this.m_SeparatorFileOpenEnd});
            this.m_MenuItemFileOpen.Image = global::Bombardier.PTU.Properties.Resources.FolderOpen;
            this.m_MenuItemFileOpen.Name = "m_MenuItemFileOpen";
            resources.ApplyResources(this.m_MenuItemFileOpen, "m_MenuItemFileOpen");
            // 
            // m_MenuItemFileOpenWatchFile
            // 
            this.m_MenuItemFileOpenWatchFile.Image = global::Bombardier.PTU.Properties.Resources.Watch;
            this.m_MenuItemFileOpenWatchFile.Name = "m_MenuItemFileOpenWatchFile";
            resources.ApplyResources(this.m_MenuItemFileOpenWatchFile, "m_MenuItemFileOpenWatchFile");
            this.m_MenuItemFileOpenWatchFile.Click += new System.EventHandler(this.m_MenuItemFileOpenWatchFile_Click);
            // 
            // m_SeparatorFileOpenWatchEvaluateVariables
            // 
            this.m_SeparatorFileOpenWatchEvaluateVariables.Name = "m_SeparatorFileOpenWatchEvaluateVariables";
            resources.ApplyResources(this.m_SeparatorFileOpenWatchEvaluateVariables, "m_SeparatorFileOpenWatchEvaluateVariables");
            // 
            // m_MenuItemFileOpenSimulatedFaultLog
            // 
            this.m_MenuItemFileOpenSimulatedFaultLog.Image = global::Bombardier.PTU.Properties.Resources.DataStreamSimulated;
            this.m_MenuItemFileOpenSimulatedFaultLog.Name = "m_MenuItemFileOpenSimulatedFaultLog";
            resources.ApplyResources(this.m_MenuItemFileOpenSimulatedFaultLog, "m_MenuItemFileOpenSimulatedFaultLog");
            this.m_MenuItemFileOpenSimulatedFaultLog.Click += new System.EventHandler(this.m_MenuItemFileOpenSimulatedFaultLog_Click);
            // 
            // m_MenuItemFileOpenFaultLog
            // 
            resources.ApplyResources(this.m_MenuItemFileOpenFaultLog, "m_MenuItemFileOpenFaultLog");
            this.m_MenuItemFileOpenFaultLog.Name = "m_MenuItemFileOpenFaultLog";
            this.m_MenuItemFileOpenFaultLog.Click += new System.EventHandler(this.m_MenuItemFileOpenFaultLog_Click);
            // 
            // m_MenuItemFileOpenEventLog
            // 
            this.m_MenuItemFileOpenEventLog.Image = global::Bombardier.PTU.Properties.Resources.Eventlog;
            this.m_MenuItemFileOpenEventLog.Name = "m_MenuItemFileOpenEventLog";
            resources.ApplyResources(this.m_MenuItemFileOpenEventLog, "m_MenuItemFileOpenEventLog");
            this.m_MenuItemFileOpenEventLog.Click += new System.EventHandler(this.m_MenuItemFileOpenEventLog_Click);
            // 
            // m_SeparatorFileOpenEnd
            // 
            this.m_SeparatorFileOpenEnd.Name = "m_SeparatorFileOpenEnd";
            resources.ApplyResources(this.m_SeparatorFileOpenEnd, "m_SeparatorFileOpenEnd");
            // 
            // m_MenuItemFileSelectDataDictionary
            // 
            this.m_MenuItemFileSelectDataDictionary.Image = global::Bombardier.PTU.Properties.Resources.Configuration;
            this.m_MenuItemFileSelectDataDictionary.Name = "m_MenuItemFileSelectDataDictionary";
            resources.ApplyResources(this.m_MenuItemFileSelectDataDictionary, "m_MenuItemFileSelectDataDictionary");
            this.m_MenuItemFileSelectDataDictionary.Click += new System.EventHandler(this.m_MenuItemFileOpenDataDictionary_Click);
            // 
            // m_SeparatorFileOpenDataDictionary
            // 
            this.m_SeparatorFileOpenDataDictionary.Name = "m_SeparatorFileOpenDataDictionary";
            resources.ApplyResources(this.m_SeparatorFileOpenDataDictionary, "m_SeparatorFileOpenDataDictionary");
            // 
            // m_MenuItemFileExit
            // 
            this.m_MenuItemFileExit.Image = global::Bombardier.PTU.Properties.Resources.HotPlug;
            this.m_MenuItemFileExit.Name = "m_MenuItemFileExit";
            resources.ApplyResources(this.m_MenuItemFileExit, "m_MenuItemFileExit");
            this.m_MenuItemFileExit.Click += new System.EventHandler(this.m_MenuItemFileExit_Click);
            // 
            // m_SeparatorFileEnd
            // 
            this.m_SeparatorFileEnd.Name = "m_SeparatorFileEnd";
            resources.ApplyResources(this.m_SeparatorFileEnd, "m_SeparatorFileEnd");
            // 
            // m_MenuItemView
            // 
            this.m_MenuItemView.BackColor = System.Drawing.SystemColors.Control;
            this.m_MenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemViewWatchWindow,
            this.m_SeparatorViewWatchWindow,
            this.m_MenuItemViewSystemInformation,
            this.m_SeparatorViewEnd});
            this.m_MenuItemView.Name = "m_MenuItemView";
            resources.ApplyResources(this.m_MenuItemView, "m_MenuItemView");
            // 
            // m_MenuItemViewWatchWindow
            // 
            this.m_MenuItemViewWatchWindow.Image = global::Bombardier.PTU.Properties.Resources.Watch;
            this.m_MenuItemViewWatchWindow.Name = "m_MenuItemViewWatchWindow";
            resources.ApplyResources(this.m_MenuItemViewWatchWindow, "m_MenuItemViewWatchWindow");
            this.m_MenuItemViewWatchWindow.Click += new System.EventHandler(this.m_MenuItemWatchViewWatchWindow_Click);
            // 
            // m_SeparatorViewWatchWindow
            // 
            this.m_SeparatorViewWatchWindow.Name = "m_SeparatorViewWatchWindow";
            resources.ApplyResources(this.m_SeparatorViewWatchWindow, "m_SeparatorViewWatchWindow");
            // 
            // m_MenuItemViewSystemInformation
            // 
            this.m_MenuItemViewSystemInformation.Image = global::Bombardier.PTU.Properties.Resources.Information;
            this.m_MenuItemViewSystemInformation.Name = "m_MenuItemViewSystemInformation";
            resources.ApplyResources(this.m_MenuItemViewSystemInformation, "m_MenuItemViewSystemInformation");
            this.m_MenuItemViewSystemInformation.Click += new System.EventHandler(this.m_MenuItemViewSystemInformation_Click);
            // 
            // m_SeparatorViewEnd
            // 
            this.m_SeparatorViewEnd.Name = "m_SeparatorViewEnd";
            resources.ApplyResources(this.m_SeparatorViewEnd, "m_SeparatorViewEnd");
            // 
            // m_MenuItemDiagnostics
            // 
            this.m_MenuItemDiagnostics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemDiagnosticsSelfTests,
            this.m_SeparatorDiagnosticsSelfTest,
            this.m_MenuItemDiagnosticsEventLog,
            this.m_SeparatorDiagnosticsEventLog,
            this.m_MenuItemDiagnosticsInitializeEventLogs,
            this.m_SeparatorDiagnosticsEnd});
            this.m_MenuItemDiagnostics.Name = "m_MenuItemDiagnostics";
            resources.ApplyResources(this.m_MenuItemDiagnostics, "m_MenuItemDiagnostics");
            // 
            // m_MenuItemDiagnosticsSelfTests
            // 
            this.m_MenuItemDiagnosticsSelfTests.Image = global::Bombardier.PTU.Properties.Resources.SelfTest;
            this.m_MenuItemDiagnosticsSelfTests.Name = "m_MenuItemDiagnosticsSelfTests";
            resources.ApplyResources(this.m_MenuItemDiagnosticsSelfTests, "m_MenuItemDiagnosticsSelfTests");
            this.m_MenuItemDiagnosticsSelfTests.Click += new System.EventHandler(this.m_MenuItemDiagnosticsSelfTests_Click);
            // 
            // m_SeparatorDiagnosticsSelfTest
            // 
            this.m_SeparatorDiagnosticsSelfTest.Name = "m_SeparatorDiagnosticsSelfTest";
            resources.ApplyResources(this.m_SeparatorDiagnosticsSelfTest, "m_SeparatorDiagnosticsSelfTest");
            // 
            // m_MenuItemDiagnosticsEventLog
            // 
            this.m_MenuItemDiagnosticsEventLog.Image = global::Bombardier.PTU.Properties.Resources.Eventlog;
            this.m_MenuItemDiagnosticsEventLog.Name = "m_MenuItemDiagnosticsEventLog";
            resources.ApplyResources(this.m_MenuItemDiagnosticsEventLog, "m_MenuItemDiagnosticsEventLog");
            this.m_MenuItemDiagnosticsEventLog.Click += new System.EventHandler(this.m_MenuItemDiagnosticsEventLog_Click);
            // 
            // m_SeparatorDiagnosticsEventLog
            // 
            this.m_SeparatorDiagnosticsEventLog.Name = "m_SeparatorDiagnosticsEventLog";
            resources.ApplyResources(this.m_SeparatorDiagnosticsEventLog, "m_SeparatorDiagnosticsEventLog");
            // 
            // m_MenuItemDiagnosticsInitializeEventLogs
            // 
            this.m_MenuItemDiagnosticsInitializeEventLogs.Image = global::Bombardier.PTU.Properties.Resources.Initialize;
            this.m_MenuItemDiagnosticsInitializeEventLogs.Name = "m_MenuItemDiagnosticsInitializeEventLogs";
            resources.ApplyResources(this.m_MenuItemDiagnosticsInitializeEventLogs, "m_MenuItemDiagnosticsInitializeEventLogs");
            this.m_MenuItemDiagnosticsInitializeEventLogs.Click += new System.EventHandler(this.m_MenuItemDiagnosticsInitializeEventLogs_Click);
            // 
            // m_SeparatorDiagnosticsEnd
            // 
            this.m_SeparatorDiagnosticsEnd.Name = "m_SeparatorDiagnosticsEnd";
            resources.ApplyResources(this.m_SeparatorDiagnosticsEnd, "m_SeparatorDiagnosticsEnd");
            // 
            // m_MenuItemConfigure
            // 
            this.m_MenuItemConfigure.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemConfigureWorksets,
            this.m_SeparatorConfigureWorksets,
            this.m_MenuItemConfigureRealTimeClock,
            this.m_SeparatorConfigureRealTimeClock,
            this.m_MenuItemConfigurePasswordProtection,
            this.m_SeparatorConfigurePasswordProtection,
            this.m_MenuItemConfigureChartRecorder,
            this.m_MenuItemConfigureChartMode,
            this.m_SeparatorConfigureChartMode,
            this.m_MenuItemConfigureEnumeration,
            this.m_SeparatorConfigureEnd});
            this.m_MenuItemConfigure.Name = "m_MenuItemConfigure";
            resources.ApplyResources(this.m_MenuItemConfigure, "m_MenuItemConfigure");
            // 
            // m_MenuItemConfigureWorksets
            // 
            this.m_MenuItemConfigureWorksets.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemConfigureWorksetsWatchWindow,
            this.m_MenuItemConfigureWorksetsFaultLog,
            this.m_MenuItemConfigureWorksetsChartRecorder});
            resources.ApplyResources(this.m_MenuItemConfigureWorksets, "m_MenuItemConfigureWorksets");
            this.m_MenuItemConfigureWorksets.Name = "m_MenuItemConfigureWorksets";
            // 
            // m_MenuItemConfigureWorksetsWatchWindow
            // 
            this.m_MenuItemConfigureWorksetsWatchWindow.Image = global::Bombardier.PTU.Properties.Resources.Watch;
            this.m_MenuItemConfigureWorksetsWatchWindow.Name = "m_MenuItemConfigureWorksetsWatchWindow";
            resources.ApplyResources(this.m_MenuItemConfigureWorksetsWatchWindow, "m_MenuItemConfigureWorksetsWatchWindow");
            this.m_MenuItemConfigureWorksetsWatchWindow.Click += new System.EventHandler(this.m_MenuItemConfigureWorksetsWatchWindow_Click);
            // 
            // m_MenuItemConfigureWorksetsFaultLog
            // 
            resources.ApplyResources(this.m_MenuItemConfigureWorksetsFaultLog, "m_MenuItemConfigureWorksetsFaultLog");
            this.m_MenuItemConfigureWorksetsFaultLog.Name = "m_MenuItemConfigureWorksetsFaultLog";
            this.m_MenuItemConfigureWorksetsFaultLog.Click += new System.EventHandler(this.m_MenuItemConfigureWorksetsFaultLog_Click);
            // 
            // m_MenuItemConfigureWorksetsChartRecorder
            // 
            this.m_MenuItemConfigureWorksetsChartRecorder.Name = "m_MenuItemConfigureWorksetsChartRecorder";
            resources.ApplyResources(this.m_MenuItemConfigureWorksetsChartRecorder, "m_MenuItemConfigureWorksetsChartRecorder");
            this.m_MenuItemConfigureWorksetsChartRecorder.Click += new System.EventHandler(this.m_MenuItemConfigureWorksetsChartRecorder_Click);
            // 
            // m_SeparatorConfigureWorksets
            // 
            this.m_SeparatorConfigureWorksets.Name = "m_SeparatorConfigureWorksets";
            resources.ApplyResources(this.m_SeparatorConfigureWorksets, "m_SeparatorConfigureWorksets");
            // 
            // m_MenuItemConfigureRealTimeClock
            // 
            this.m_MenuItemConfigureRealTimeClock.Image = global::Bombardier.PTU.Properties.Resources.Time;
            this.m_MenuItemConfigureRealTimeClock.Name = "m_MenuItemConfigureRealTimeClock";
            resources.ApplyResources(this.m_MenuItemConfigureRealTimeClock, "m_MenuItemConfigureRealTimeClock");
            this.m_MenuItemConfigureRealTimeClock.Click += new System.EventHandler(this.m_MenuItemConfigureRealTimeClock_Click);
            // 
            // m_SeparatorConfigureRealTimeClock
            // 
            this.m_SeparatorConfigureRealTimeClock.Name = "m_SeparatorConfigureRealTimeClock";
            resources.ApplyResources(this.m_SeparatorConfigureRealTimeClock, "m_SeparatorConfigureRealTimeClock");
            // 
            // m_MenuItemConfigurePasswordProtection
            // 
            this.m_MenuItemConfigurePasswordProtection.Image = global::Bombardier.PTU.Properties.Resources.Keys;
            this.m_MenuItemConfigurePasswordProtection.Name = "m_MenuItemConfigurePasswordProtection";
            resources.ApplyResources(this.m_MenuItemConfigurePasswordProtection, "m_MenuItemConfigurePasswordProtection");
            this.m_MenuItemConfigurePasswordProtection.Click += new System.EventHandler(this.m_MenuItemConfigurePasswordProtection_Click);
            // 
            // m_SeparatorConfigurePasswordProtection
            // 
            this.m_SeparatorConfigurePasswordProtection.Name = "m_SeparatorConfigurePasswordProtection";
            resources.ApplyResources(this.m_SeparatorConfigurePasswordProtection, "m_SeparatorConfigurePasswordProtection");
            // 
            // m_MenuItemConfigureChartRecorder
            // 
            this.m_MenuItemConfigureChartRecorder.Name = "m_MenuItemConfigureChartRecorder";
            resources.ApplyResources(this.m_MenuItemConfigureChartRecorder, "m_MenuItemConfigureChartRecorder");
            this.m_MenuItemConfigureChartRecorder.Click += new System.EventHandler(this.m_MenuItemConfigureChartRecorder_Click);
            // 
            // m_MenuItemConfigureChartMode
            // 
            this.m_MenuItemConfigureChartMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemConfigureChartModeData,
            this.m_MenuItemConfigureChartModeRamp,
            this.m_MenuItemConfigureChartModeZeroOutput,
            this.m_MenuItemConfigureChartModeFullScale});
            this.m_MenuItemConfigureChartMode.Name = "m_MenuItemConfigureChartMode";
            resources.ApplyResources(this.m_MenuItemConfigureChartMode, "m_MenuItemConfigureChartMode");
            // 
            // m_MenuItemConfigureChartModeData
            // 
            this.m_MenuItemConfigureChartModeData.Name = "m_MenuItemConfigureChartModeData";
            resources.ApplyResources(this.m_MenuItemConfigureChartModeData, "m_MenuItemConfigureChartModeData");
            this.m_MenuItemConfigureChartModeData.Click += new System.EventHandler(this.m_MenuItemConfigureChartModeData_Click);
            // 
            // m_MenuItemConfigureChartModeRamp
            // 
            this.m_MenuItemConfigureChartModeRamp.Name = "m_MenuItemConfigureChartModeRamp";
            resources.ApplyResources(this.m_MenuItemConfigureChartModeRamp, "m_MenuItemConfigureChartModeRamp");
            this.m_MenuItemConfigureChartModeRamp.Click += new System.EventHandler(this.m_MenuItemConfigureChartModeRamp_Click);
            // 
            // m_MenuItemConfigureChartModeZeroOutput
            // 
            this.m_MenuItemConfigureChartModeZeroOutput.Name = "m_MenuItemConfigureChartModeZeroOutput";
            resources.ApplyResources(this.m_MenuItemConfigureChartModeZeroOutput, "m_MenuItemConfigureChartModeZeroOutput");
            this.m_MenuItemConfigureChartModeZeroOutput.Click += new System.EventHandler(this.m_MenuItemConfigureChartModeZeroOutput_Click);
            // 
            // m_MenuItemConfigureChartModeFullScale
            // 
            this.m_MenuItemConfigureChartModeFullScale.Name = "m_MenuItemConfigureChartModeFullScale";
            resources.ApplyResources(this.m_MenuItemConfigureChartModeFullScale, "m_MenuItemConfigureChartModeFullScale");
            this.m_MenuItemConfigureChartModeFullScale.Click += new System.EventHandler(this.m_MenuItemConfigureChartModeFullScale_Click);
            // 
            // m_SeparatorConfigureChartMode
            // 
            this.m_SeparatorConfigureChartMode.Name = "m_SeparatorConfigureChartMode";
            resources.ApplyResources(this.m_SeparatorConfigureChartMode, "m_SeparatorConfigureChartMode");
            // 
            // m_MenuItemConfigureEnumeration
            // 
            this.m_MenuItemConfigureEnumeration.Name = "m_MenuItemConfigureEnumeration";
            resources.ApplyResources(this.m_MenuItemConfigureEnumeration, "m_MenuItemConfigureEnumeration");
            this.m_MenuItemConfigureEnumeration.Click += new System.EventHandler(this.m_MenuItemConfigureEnumeration_Click);
            // 
            // m_SeparatorConfigureEnd
            // 
            this.m_SeparatorConfigureEnd.Name = "m_SeparatorConfigureEnd";
            resources.ApplyResources(this.m_SeparatorConfigureEnd, "m_SeparatorConfigureEnd");
            // 
            // m_MenuItemTools
            // 
            this.m_MenuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemToolsConvertEngineeringFile,
            this.m_SeparatorToolsConvertEngineeringDatabase,
            this.m_MenuItemToolsDebugMode,
            this.m_SeparatorToolsDebug,
            this.m_MenuItemToolsOptions,
            this.m_SeparatorToolsEnd});
            this.m_MenuItemTools.Name = "m_MenuItemTools";
            resources.ApplyResources(this.m_MenuItemTools, "m_MenuItemTools");
            // 
            // m_MenuItemToolsConvertEngineeringFile
            // 
            this.m_MenuItemToolsConvertEngineeringFile.Image = global::Bombardier.PTU.Properties.Resources.Function;
            this.m_MenuItemToolsConvertEngineeringFile.Name = "m_MenuItemToolsConvertEngineeringFile";
            resources.ApplyResources(this.m_MenuItemToolsConvertEngineeringFile, "m_MenuItemToolsConvertEngineeringFile");
            this.m_MenuItemToolsConvertEngineeringFile.Click += new System.EventHandler(this.m_MenuItemToolsConvertEngineeringDatabase);
            // 
            // m_SeparatorToolsConvertEngineeringDatabase
            // 
            this.m_SeparatorToolsConvertEngineeringDatabase.Name = "m_SeparatorToolsConvertEngineeringDatabase";
            resources.ApplyResources(this.m_SeparatorToolsConvertEngineeringDatabase, "m_SeparatorToolsConvertEngineeringDatabase");
            // 
            // m_MenuItemToolsDebugMode
            // 
            resources.ApplyResources(this.m_MenuItemToolsDebugMode, "m_MenuItemToolsDebugMode");
            this.m_MenuItemToolsDebugMode.Name = "m_MenuItemToolsDebugMode";
            this.m_MenuItemToolsDebugMode.Click += new System.EventHandler(this.m_MenuItemToolsDebugMode_Click);
            // 
            // m_SeparatorToolsDebug
            // 
            this.m_SeparatorToolsDebug.Name = "m_SeparatorToolsDebug";
            resources.ApplyResources(this.m_SeparatorToolsDebug, "m_SeparatorToolsDebug");
            // 
            // m_MenuItemToolsOptions
            // 
            this.m_MenuItemToolsOptions.Image = global::Bombardier.PTU.Properties.Resources.Options;
            this.m_MenuItemToolsOptions.Name = "m_MenuItemToolsOptions";
            resources.ApplyResources(this.m_MenuItemToolsOptions, "m_MenuItemToolsOptions");
            this.m_MenuItemToolsOptions.Click += new System.EventHandler(this.m_MenuItemToolsOptions_Click);
            // 
            // m_SeparatorToolsEnd
            // 
            this.m_SeparatorToolsEnd.Name = "m_SeparatorToolsEnd";
            resources.ApplyResources(this.m_SeparatorToolsEnd, "m_SeparatorToolsEnd");
            // 
            // m_MenuItemHelp
            // 
            this.m_MenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemHelpPTUHelp,
            this.m_SeparatorHelpPTUHelp,
            this.m_MenuItemHelpAboutPTU,
            this.m_SeparatorHelpEnd});
            this.m_MenuItemHelp.Name = "m_MenuItemHelp";
            resources.ApplyResources(this.m_MenuItemHelp, "m_MenuItemHelp");
            // 
            // m_MenuItemHelpPTUHelp
            // 
            this.m_MenuItemHelpPTUHelp.Name = "m_MenuItemHelpPTUHelp";
            resources.ApplyResources(this.m_MenuItemHelpPTUHelp, "m_MenuItemHelpPTUHelp");
            this.m_MenuItemHelpPTUHelp.Click += new System.EventHandler(this.m_MenuItemHelpPTUHelp_Click);
            // 
            // m_SeparatorHelpPTUHelp
            // 
            this.m_SeparatorHelpPTUHelp.Name = "m_SeparatorHelpPTUHelp";
            resources.ApplyResources(this.m_SeparatorHelpPTUHelp, "m_SeparatorHelpPTUHelp");
            // 
            // m_MenuItemHelpAboutPTU
            // 
            this.m_MenuItemHelpAboutPTU.Image = global::Bombardier.PTU.Properties.Resources.Help;
            this.m_MenuItemHelpAboutPTU.Name = "m_MenuItemHelpAboutPTU";
            resources.ApplyResources(this.m_MenuItemHelpAboutPTU, "m_MenuItemHelpAboutPTU");
            this.m_MenuItemHelpAboutPTU.Click += new System.EventHandler(this.m_MenuItemHelpAboutPTU_Click);
            // 
            // m_SeparatorHelpEnd
            // 
            this.m_SeparatorHelpEnd.Name = "m_SeparatorHelpEnd";
            resources.ApplyResources(this.m_SeparatorHelpEnd, "m_SeparatorHelpEnd");
            // 
            // m_MenuItemLogin
            // 
            this.m_MenuItemLogin.Name = "m_MenuItemLogin";
            resources.ApplyResources(this.m_MenuItemLogin, "m_MenuItemLogin");
            this.m_MenuItemLogin.Click += new System.EventHandler(this.m_MenuItemLogin_Click);
            // 
            // m_PanelStatus
            // 
            this.m_PanelStatus.BackColor = System.Drawing.SystemColors.Control;
            this.m_PanelStatus.BackgroundImage = global::Bombardier.PTU.Properties.Resources.LightMetallic;
            resources.ApplyResources(this.m_PanelStatus, "m_PanelStatus");
            this.m_PanelStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_PanelStatus.Controls.Add(this.m_PictureBoxBusy);
            this.m_PanelStatus.Controls.Add(this.m_DigitalControlPacketReceived);
            this.m_PanelStatus.Controls.Add(this.m_LegendRx);
            this.m_PanelStatus.Controls.Add(this.m_StatusStrip);
            this.m_PanelStatus.Name = "m_PanelStatus";
            // 
            // m_PictureBoxBusy
            // 
            resources.ApplyResources(this.m_PictureBoxBusy, "m_PictureBoxBusy");
            this.m_PictureBoxBusy.BackColor = System.Drawing.Color.Transparent;
            this.m_PictureBoxBusy.Image = global::Bombardier.PTU.Properties.Resources.fSEARCH_00;
            this.m_PictureBoxBusy.Name = "m_PictureBoxBusy";
            this.m_PictureBoxBusy.TabStop = false;
            // 
            // m_LegendRx
            // 
            resources.ApplyResources(this.m_LegendRx, "m_LegendRx");
            this.m_LegendRx.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendRx.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LegendRx.Name = "m_LegendRx";
            // 
            // m_StatusStrip
            // 
            this.m_StatusStrip.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_StatusStrip, "m_StatusStrip");
            this.m_StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_StatusLabelMode,
            this.m_StatusLabelCarNumber,
            this.m_StatusLabelSecurityLevel,
            this.m_StatusLabelLogStatus,
            this.m_StatusLabelWibuBoxStatus,
            this.m_LegendStatusMessage,
            this.m_StatusLabelMessage});
            this.m_StatusStrip.Name = "m_StatusStrip";
            this.m_StatusStrip.ShowItemToolTips = true;
            this.m_StatusStrip.SizingGrip = false;
            this.m_StatusStrip.Stretch = false;
            // 
            // m_StatusLabelMode
            // 
            resources.ApplyResources(this.m_StatusLabelMode, "m_StatusLabelMode");
            this.m_StatusLabelMode.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusLabelMode.Image = global::Bombardier.PTU.Properties.Resources.ModeDiagnostic;
            this.m_StatusLabelMode.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.m_StatusLabelMode.Name = "m_StatusLabelMode";
            // 
            // m_StatusLabelCarNumber
            // 
            resources.ApplyResources(this.m_StatusLabelCarNumber, "m_StatusLabelCarNumber");
            this.m_StatusLabelCarNumber.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusLabelCarNumber.Image = global::Bombardier.PTU.Properties.Resources.CarNumber;
            this.m_StatusLabelCarNumber.Margin = new System.Windows.Forms.Padding(0, 3, 3, 1);
            this.m_StatusLabelCarNumber.Name = "m_StatusLabelCarNumber";
            // 
            // m_StatusLabelSecurityLevel
            // 
            resources.ApplyResources(this.m_StatusLabelSecurityLevel, "m_StatusLabelSecurityLevel");
            this.m_StatusLabelSecurityLevel.BackColor = System.Drawing.Color.Transparent;
            this.m_StatusLabelSecurityLevel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusLabelSecurityLevel.Image = global::Bombardier.PTU.Properties.Resources.User;
            this.m_StatusLabelSecurityLevel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 1);
            this.m_StatusLabelSecurityLevel.Name = "m_StatusLabelSecurityLevel";
            // 
            // m_StatusLabelLogStatus
            // 
            resources.ApplyResources(this.m_StatusLabelLogStatus, "m_StatusLabelLogStatus");
            this.m_StatusLabelLogStatus.BackColor = System.Drawing.SystemColors.Info;
            this.m_StatusLabelLogStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusLabelLogStatus.Image = global::Bombardier.PTU.Properties.Resources.Eventlog;
            this.m_StatusLabelLogStatus.Margin = new System.Windows.Forms.Padding(0, 3, 3, 1);
            this.m_StatusLabelLogStatus.Name = "m_StatusLabelLogStatus";
            // 
            // m_StatusLabelWibuBoxStatus
            // 
            resources.ApplyResources(this.m_StatusLabelWibuBoxStatus, "m_StatusLabelWibuBoxStatus");
            this.m_StatusLabelWibuBoxStatus.BackColor = System.Drawing.Color.DarkCyan;
            this.m_StatusLabelWibuBoxStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusLabelWibuBoxStatus.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_StatusLabelWibuBoxStatus.Image = global::Bombardier.PTU.Properties.Resources.Keys;
            this.m_StatusLabelWibuBoxStatus.Margin = new System.Windows.Forms.Padding(0, 3, 3, 1);
            this.m_StatusLabelWibuBoxStatus.Name = "m_StatusLabelWibuBoxStatus";
            // 
            // m_LegendStatusMessage
            // 
            this.m_LegendStatusMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_LegendStatusMessage.Margin = new System.Windows.Forms.Padding(20, 3, 3, 1);
            this.m_LegendStatusMessage.Name = "m_LegendStatusMessage";
            resources.ApplyResources(this.m_LegendStatusMessage, "m_LegendStatusMessage");
            // 
            // m_StatusLabelMessage
            // 
            resources.ApplyResources(this.m_StatusLabelMessage, "m_StatusLabelMessage");
            this.m_StatusLabelMessage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusLabelMessage.Margin = new System.Windows.Forms.Padding(0, 3, 3, 1);
            this.m_StatusLabelMessage.Name = "m_StatusLabelMessage";
            // 
            // m_TSBOnline
            // 
            resources.ApplyResources(this.m_TSBOnline, "m_TSBOnline");
            this.m_TSBOnline.AutoToolTip = false;
            this.m_TSBOnline.BackColor = System.Drawing.SystemColors.Control;
            this.m_TSBOnline.Image = global::Bombardier.PTU.Properties.Resources.COMPort;
            this.m_TSBOnline.Name = "m_TSBOnline";
            this.m_TSBOnline.Click += new System.EventHandler(this.m_TSBOnline_Click);
            // 
            // m_SeparatorOnline
            // 
            this.m_SeparatorOnline.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_SeparatorOnline.Name = "m_SeparatorOnline";
            resources.ApplyResources(this.m_SeparatorOnline, "m_SeparatorOnline");
            // 
            // m_ToolStripFunctionKeys
            // 
            this.m_ToolStripFunctionKeys.BackColor = System.Drawing.SystemColors.Control;
            this.m_ToolStripFunctionKeys.BackgroundImage = global::Bombardier.PTU.Properties.Resources.LightMetallic;
            resources.ApplyResources(this.m_ToolStripFunctionKeys, "m_ToolStripFunctionKeys");
            this.m_ToolStripFunctionKeys.GripMargin = new System.Windows.Forms.Padding(0);
            this.m_ToolStripFunctionKeys.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_ToolStripFunctionKeys.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_SeparatorOnlineLHS,
            this.m_TSBOnline,
            this.m_SeparatorOnline,
            this.m_SeparatorOfflineLHS,
            this.m_TSBOffline,
            this.m_SeparatorOffline});
            this.m_ToolStripFunctionKeys.Name = "m_ToolStripFunctionKeys";
            this.m_ToolStripFunctionKeys.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // m_SeparatorOnlineLHS
            // 
            this.m_SeparatorOnlineLHS.Name = "m_SeparatorOnlineLHS";
            resources.ApplyResources(this.m_SeparatorOnlineLHS, "m_SeparatorOnlineLHS");
            // 
            // m_SeparatorOfflineLHS
            // 
            this.m_SeparatorOfflineLHS.Name = "m_SeparatorOfflineLHS";
            resources.ApplyResources(this.m_SeparatorOfflineLHS, "m_SeparatorOfflineLHS");
            // 
            // m_TSBOffline
            // 
            resources.ApplyResources(this.m_TSBOffline, "m_TSBOffline");
            this.m_TSBOffline.AutoToolTip = false;
            this.m_TSBOffline.Image = global::Bombardier.PTU.Properties.Resources.Offline;
            this.m_TSBOffline.Name = "m_TSBOffline";
            this.m_TSBOffline.Click += new System.EventHandler(this.m_TSBOffline_Click);
            // 
            // m_SeparatorOffline
            // 
            this.m_SeparatorOffline.Name = "m_SeparatorOffline";
            resources.ApplyResources(this.m_SeparatorOffline, "m_SeparatorOffline");
            // 
            // m_DigitalControlPacketReceived
            // 
            resources.ApplyResources(this.m_DigitalControlPacketReceived, "m_DigitalControlPacketReceived");
            this.m_DigitalControlPacketReceived.BackColorOff = System.Drawing.SystemColors.Window;
            this.m_DigitalControlPacketReceived.BackColorOn = System.Drawing.Color.Orange;
            this.m_DigitalControlPacketReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_DigitalControlPacketReceived.DigitalControlText = global::Bombardier.PTU.Properties.Resources.LabelModeDiagnostic;
            this.m_DigitalControlPacketReceived.ForeColorOff = System.Drawing.SystemColors.ControlText;
            this.m_DigitalControlPacketReceived.ForeColorOn = System.Drawing.SystemColors.ControlText;
            this.m_DigitalControlPacketReceived.IntervalBlinkMs = 100;
            this.m_DigitalControlPacketReceived.Name = "m_DigitalControlPacketReceived";
            this.m_DigitalControlPacketReceived.State = false;
            this.m_DigitalControlPacketReceived.TabStop = false;
            // 
            // MdiPTU
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.m_ToolStripFunctionKeys);
            this.Controls.Add(this.m_MenuStrip);
            this.Controls.Add(this.m_PanelStatus);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.m_MenuStrip;
            this.Name = "MdiPTU";
            this.Shown += new System.EventHandler(this.MdiPTU_Shown);
            this.ResizeEnd += new System.EventHandler(this.MdiPTU_ResizeEnd);
            this.FontChanged += new System.EventHandler(this.MdiPTU_FontChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MdiPTU_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MdiPTU_KeyUp);
            this.m_MenuStrip.ResumeLayout(false);
            this.m_MenuStrip.PerformLayout();
            this.m_PanelStatus.ResumeLayout(false);
            this.m_PanelStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxBusy)).EndInit();
            this.m_StatusStrip.ResumeLayout(false);
            this.m_StatusStrip.PerformLayout();
            this.m_ToolStripFunctionKeys.ResumeLayout(false);
            this.m_ToolStripFunctionKeys.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.MenuStrip m_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemFileOpen;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemFileOpenWatchFile;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemFileExit;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemView;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemTools;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemHelpAboutPTU;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemLogin;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemFileOpenEventLog;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorFileOpenWatchEvaluateVariables;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorFileEnd;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorHelpEnd;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemHelpPTUHelp;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorHelpPTUHelp;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemFileOpenFaultLog;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorFileOpenEnd;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemDiagnostics;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemDiagnosticsSelfTests;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemDiagnosticsEventLog;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemDiagnosticsInitializeEventLogs;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorDiagnosticsSelfTest;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorDiagnosticsEventLog;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorDiagnosticsEnd;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigure;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureRealTimeClock;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorConfigureRealTimeClock;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigurePasswordProtection;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorConfigurePasswordProtection;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureEnumeration;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorConfigureEnd;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemToolsOptions;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorToolsEnd;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemToolsConvertEngineeringFile;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorToolsConvertEngineeringDatabase;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureWorksets;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemFileSelectDataDictionary;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorFileOpenDataDictionary;
        private System.Windows.Forms.Panel m_PanelStatus;
        private Common.UserControls.DigitalControl m_DigitalControlPacketReceived;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemFileOpenSimulatedFaultLog;
        private System.Windows.Forms.ToolStripButton m_TSBOnline;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorOnline;
        private System.Windows.Forms.ToolStrip m_ToolStripFunctionKeys;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorConfigureWorksets;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureWorksetsWatchWindow;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureWorksetsFaultLog;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemToolsDebugMode;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorToolsDebug;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemViewWatchWindow;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorViewWatchWindow;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemViewSystemInformation;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorViewEnd;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureWorksetsChartRecorder;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureChartRecorder;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorConfigureChartMode;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureChartMode;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureChartModeData;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureChartModeRamp;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureChartModeZeroOutput;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemConfigureChartModeFullScale;
        private System.Windows.Forms.Label m_LegendRx;
        private System.Windows.Forms.PictureBox m_PictureBoxBusy;
        private System.Windows.Forms.ToolStripButton m_TSBOffline;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorOffline;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorOnlineLHS;
        private System.Windows.Forms.ToolStripSeparator m_SeparatorOfflineLHS;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusLabelMode;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusLabelCarNumber;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusLabelSecurityLevel;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusLabelLogStatus;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusLabelWibuBoxStatus;
        private System.Windows.Forms.ToolStripStatusLabel m_LegendStatusMessage;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusLabelMessage;
        private System.Windows.Forms.StatusStrip m_StatusStrip;
    }
}

