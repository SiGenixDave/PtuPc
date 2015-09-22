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
 *  File name:  ControlPanel.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  06/22/15    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */

/*
 *  07/28/15    1.1     K.McD           References
 *                                      1.   NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 2.  Addition of the Control Panel window.
 *  
 *                                      Modifications
 *                                      1. Included the SecurityLevel and Mode StatusLabels within the scope of the Control Panel.
 *                                      2. Renamed the 'CarIdentifier' Label and Legend to 'CarNumber'.
 */

/*
 *  08/11/15    1.2     K.McD       References
 *                                  1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                      from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                      
 *                                      1.  The legends on the control panel buttons and labels are to be modified to match those specified in the KRC PTE Uniform
 *                                          Interface Specification Rev. B.
 *                                          
 *                                  Modifications
 *                                  1.  Changed the legends associated with the control panel buttons and labels to match the KRC PTE Uniform Interface Specification
 *                                      Rev. B. Also changed the positioning of the labels and buttons to match the specification.
 * 
 */
#endregion --- Revision History ---

using System;

namespace Bombardier.PTU
{
    partial class ControlPanel
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
                    GC.SuppressFinalize(this);
                }
            }
        }
        #endregion --- Disposal ---

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_LabelProjectTitle = new System.Windows.Forms.Label();
            this.m_PanelPTUStatus = new System.Windows.Forms.Panel();
            this.m_TableLayoutPanelPTUStatus = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelSecurityLevel = new System.Windows.Forms.Label();
            this.m_LegendSecurityLevel = new System.Windows.Forms.Label();
            this.m_LabelMode = new System.Windows.Forms.Label();
            this.m_LegendMode = new System.Windows.Forms.Label();
            this.m_LabelWibuBoxStatus = new System.Windows.Forms.Label();
            this.m_LabelConnection = new System.Windows.Forms.Label();
            this.m_LabelLocation = new System.Windows.Forms.Label();
            this.m_LabelSubsystem = new System.Windows.Forms.Label();
            this.m_LabelCarNumber = new System.Windows.Forms.Label();
            this.m_LegendLog = new System.Windows.Forms.Label();
            this.m_LegendCarNumber = new System.Windows.Forms.Label();
            this.m_LegendSubsystem = new System.Windows.Forms.Label();
            this.m_LegendLocation = new System.Windows.Forms.Label();
            this.m_LegendConnection = new System.Windows.Forms.Label();
            this.m_LegendWibuBox = new System.Windows.Forms.Label();
            this.m_LabelLogStatus = new System.Windows.Forms.Label();
            this.m_LegendPTUStatus = new System.Windows.Forms.Label();
            this.m_PanelMaintenance = new System.Windows.Forms.Panel();
            this.m_TableLayoutPanelMaintenance = new System.Windows.Forms.TableLayoutPanel();
            this.m_ButtonMaintenanceSpare1 = new System.Windows.Forms.Button();
            this.m_ButtonMaintenanceSpare2 = new System.Windows.Forms.Button();
            this.m_ButtonViewSystemInformation = new System.Windows.Forms.Button();
            this.m_ButtonDiagnosticsEventLog = new System.Windows.Forms.Button();
            this.m_ButtonViewWatchWindow = new System.Windows.Forms.Button();
            this.m_ButtonDiagnosticsSelfTest = new System.Windows.Forms.Button();
            this.m_LegendMaintenance = new System.Windows.Forms.Label();
            this.m_PanelAdministration = new System.Windows.Forms.Panel();
            this.m_TableLayoutPanelAdministration = new System.Windows.Forms.TableLayoutPanel();
            this.m_ButtonDiagnosticsInitializeEventLogs = new System.Windows.Forms.Button();
            this.m_ButtonAdministrationSWUpload = new System.Windows.Forms.Button();
            this.m_ButtonPasswordProtection = new System.Windows.Forms.Button();
            this.m_LegendAdministration = new System.Windows.Forms.Label();
            this.m_PanelControl = new System.Windows.Forms.Panel();
            this.m_TableLayoutPanelControl = new System.Windows.Forms.TableLayoutPanel();
            this.m_ButtonExit = new System.Windows.Forms.Button();
            this.m_ButtonHelp = new System.Windows.Forms.Button();
            this.m_LegendControl = new System.Windows.Forms.Label();
            this.m_PanelBackground = new System.Windows.Forms.Panel();
            this.m_PanelPTUStatus.SuspendLayout();
            this.m_TableLayoutPanelPTUStatus.SuspendLayout();
            this.m_PanelMaintenance.SuspendLayout();
            this.m_TableLayoutPanelMaintenance.SuspendLayout();
            this.m_PanelAdministration.SuspendLayout();
            this.m_TableLayoutPanelAdministration.SuspendLayout();
            this.m_PanelControl.SuspendLayout();
            this.m_TableLayoutPanelControl.SuspendLayout();
            this.m_PanelBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_LabelProjectTitle
            // 
            this.m_LabelProjectTitle.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelProjectTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_LabelProjectTitle.ForeColor = System.Drawing.SystemColors.Info;
            this.m_LabelProjectTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LabelProjectTitle.Location = new System.Drawing.Point(10, 0);
            this.m_LabelProjectTitle.Name = "m_LabelProjectTitle";
            this.m_LabelProjectTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.m_LabelProjectTitle.Size = new System.Drawing.Size(168, 50);
            this.m_LabelProjectTitle.TabIndex = 0;
            this.m_LabelProjectTitle.Text = "NYCT R188";
            this.m_LabelProjectTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_PanelPTUStatus
            // 
            this.m_PanelPTUStatus.AutoSize = true;
            this.m_PanelPTUStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_PanelPTUStatus.BackColor = System.Drawing.SystemColors.Info;
            this.m_PanelPTUStatus.Controls.Add(this.m_TableLayoutPanelPTUStatus);
            this.m_PanelPTUStatus.Controls.Add(this.m_LegendPTUStatus);
            this.m_PanelPTUStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelPTUStatus.Location = new System.Drawing.Point(10, 50);
            this.m_PanelPTUStatus.Name = "m_PanelPTUStatus";
            this.m_PanelPTUStatus.Size = new System.Drawing.Size(168, 191);
            this.m_PanelPTUStatus.TabIndex = 0;
            // 
            // m_TableLayoutPanelPTUStatus
            // 
            this.m_TableLayoutPanelPTUStatus.AutoSize = true;
            this.m_TableLayoutPanelPTUStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelPTUStatus.ColumnCount = 2;
            this.m_TableLayoutPanelPTUStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.45238F));
            this.m_TableLayoutPanelPTUStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.54762F));
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LabelSecurityLevel, 1, 6);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LegendSecurityLevel, 0, 6);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LabelMode, 1, 7);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LegendMode, 0, 7);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LabelWibuBoxStatus, 1, 5);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LabelConnection, 1, 4);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LabelLocation, 1, 3);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LabelSubsystem, 1, 2);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LabelCarNumber, 1, 1);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LegendLog, 0, 0);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LegendCarNumber, 0, 1);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LegendSubsystem, 0, 2);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LegendLocation, 0, 3);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LegendConnection, 0, 4);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LegendWibuBox, 0, 5);
            this.m_TableLayoutPanelPTUStatus.Controls.Add(this.m_LabelLogStatus, 1, 0);
            this.m_TableLayoutPanelPTUStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelPTUStatus.Location = new System.Drawing.Point(0, 31);
            this.m_TableLayoutPanelPTUStatus.Name = "m_TableLayoutPanelPTUStatus";
            this.m_TableLayoutPanelPTUStatus.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.m_TableLayoutPanelPTUStatus.RowCount = 9;
            this.m_TableLayoutPanelPTUStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelPTUStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelPTUStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelPTUStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelPTUStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelPTUStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelPTUStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelPTUStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelPTUStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelPTUStatus.Size = new System.Drawing.Size(168, 160);
            this.m_TableLayoutPanelPTUStatus.TabIndex = 0;
            // 
            // m_LabelSecurityLevel
            // 
            this.m_LabelSecurityLevel.AutoSize = true;
            this.m_LabelSecurityLevel.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelSecurityLevel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_LabelSecurityLevel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LabelSecurityLevel.Location = new System.Drawing.Point(73, 121);
            this.m_LabelSecurityLevel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LabelSecurityLevel.Name = "m_LabelSecurityLevel";
            this.m_LabelSecurityLevel.Size = new System.Drawing.Size(0, 13);
            this.m_LabelSecurityLevel.TabIndex = 0;
            this.m_LabelSecurityLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LegendSecurityLevel
            // 
            this.m_LegendSecurityLevel.AutoSize = true;
            this.m_LegendSecurityLevel.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_LegendSecurityLevel.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_LegendSecurityLevel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendSecurityLevel.Location = new System.Drawing.Point(37, 121);
            this.m_LegendSecurityLevel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LegendSecurityLevel.Name = "m_LegendSecurityLevel";
            this.m_LegendSecurityLevel.Size = new System.Drawing.Size(36, 13);
            this.m_LegendSecurityLevel.TabIndex = 0;
            this.m_LegendSecurityLevel.Text = "Login:";
            this.m_LegendSecurityLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LabelMode
            // 
            this.m_LabelMode.AutoSize = true;
            this.m_LabelMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelMode.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_LabelMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LabelMode.Location = new System.Drawing.Point(73, 140);
            this.m_LabelMode.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LabelMode.Name = "m_LabelMode";
            this.m_LabelMode.Size = new System.Drawing.Size(0, 13);
            this.m_LabelMode.TabIndex = 0;
            this.m_LabelMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LegendMode
            // 
            this.m_LegendMode.AutoSize = true;
            this.m_LegendMode.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_LegendMode.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_LegendMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendMode.Location = new System.Drawing.Point(36, 140);
            this.m_LegendMode.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LegendMode.Name = "m_LegendMode";
            this.m_LegendMode.Size = new System.Drawing.Size(37, 13);
            this.m_LegendMode.TabIndex = 0;
            this.m_LegendMode.Text = "Mode:";
            this.m_LegendMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LabelWibuBoxStatus
            // 
            this.m_LabelWibuBoxStatus.AutoSize = true;
            this.m_LabelWibuBoxStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelWibuBoxStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_LabelWibuBoxStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LabelWibuBoxStatus.Location = new System.Drawing.Point(73, 102);
            this.m_LabelWibuBoxStatus.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LabelWibuBoxStatus.Name = "m_LabelWibuBoxStatus";
            this.m_LabelWibuBoxStatus.Size = new System.Drawing.Size(0, 13);
            this.m_LabelWibuBoxStatus.TabIndex = 0;
            this.m_LabelWibuBoxStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelConnection
            // 
            this.m_LabelConnection.AutoSize = true;
            this.m_LabelConnection.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelConnection.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_LabelConnection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LabelConnection.Location = new System.Drawing.Point(73, 83);
            this.m_LabelConnection.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LabelConnection.Name = "m_LabelConnection";
            this.m_LabelConnection.Size = new System.Drawing.Size(0, 13);
            this.m_LabelConnection.TabIndex = 0;
            this.m_LabelConnection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelLocation
            // 
            this.m_LabelLocation.AutoSize = true;
            this.m_LabelLocation.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelLocation.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_LabelLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LabelLocation.Location = new System.Drawing.Point(73, 64);
            this.m_LabelLocation.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LabelLocation.Name = "m_LabelLocation";
            this.m_LabelLocation.Size = new System.Drawing.Size(0, 13);
            this.m_LabelLocation.TabIndex = 0;
            this.m_LabelLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelSubsystem
            // 
            this.m_LabelSubsystem.AutoSize = true;
            this.m_LabelSubsystem.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelSubsystem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_LabelSubsystem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LabelSubsystem.Location = new System.Drawing.Point(73, 45);
            this.m_LabelSubsystem.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LabelSubsystem.Name = "m_LabelSubsystem";
            this.m_LabelSubsystem.Size = new System.Drawing.Size(0, 13);
            this.m_LabelSubsystem.TabIndex = 0;
            this.m_LabelSubsystem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LabelCarNumber
            // 
            this.m_LabelCarNumber.AutoSize = true;
            this.m_LabelCarNumber.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelCarNumber.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_LabelCarNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LabelCarNumber.Location = new System.Drawing.Point(73, 26);
            this.m_LabelCarNumber.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LabelCarNumber.Name = "m_LabelCarNumber";
            this.m_LabelCarNumber.Size = new System.Drawing.Size(0, 13);
            this.m_LabelCarNumber.TabIndex = 0;
            this.m_LabelCarNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LegendLog
            // 
            this.m_LegendLog.AutoSize = true;
            this.m_LegendLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_LegendLog.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_LegendLog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendLog.Location = new System.Drawing.Point(45, 7);
            this.m_LegendLog.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LegendLog.Name = "m_LegendLog";
            this.m_LegendLog.Size = new System.Drawing.Size(28, 13);
            this.m_LegendLog.TabIndex = 0;
            this.m_LegendLog.Text = "Log:";
            this.m_LegendLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LegendCarNumber
            // 
            this.m_LegendCarNumber.AutoSize = true;
            this.m_LegendCarNumber.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_LegendCarNumber.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_LegendCarNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendCarNumber.Location = new System.Drawing.Point(27, 26);
            this.m_LegendCarNumber.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LegendCarNumber.Name = "m_LegendCarNumber";
            this.m_LegendCarNumber.Size = new System.Drawing.Size(46, 13);
            this.m_LegendCarNumber.TabIndex = 0;
            this.m_LegendCarNumber.Text = "Car No.:";
            this.m_LegendCarNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LegendSubsystem
            // 
            this.m_LegendSubsystem.AutoSize = true;
            this.m_LegendSubsystem.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_LegendSubsystem.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_LegendSubsystem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendSubsystem.Location = new System.Drawing.Point(12, 45);
            this.m_LegendSubsystem.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LegendSubsystem.Name = "m_LegendSubsystem";
            this.m_LegendSubsystem.Size = new System.Drawing.Size(61, 13);
            this.m_LegendSubsystem.TabIndex = 0;
            this.m_LegendSubsystem.Text = "Subsystem:";
            this.m_LegendSubsystem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LegendLocation
            // 
            this.m_LegendLocation.AutoSize = true;
            this.m_LegendLocation.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_LegendLocation.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_LegendLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendLocation.Location = new System.Drawing.Point(22, 64);
            this.m_LegendLocation.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LegendLocation.Name = "m_LegendLocation";
            this.m_LegendLocation.Size = new System.Drawing.Size(51, 13);
            this.m_LegendLocation.TabIndex = 0;
            this.m_LegendLocation.Text = "Location:";
            this.m_LegendLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LegendConnection
            // 
            this.m_LegendConnection.AutoSize = true;
            this.m_LegendConnection.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_LegendConnection.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_LegendConnection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendConnection.Location = new System.Drawing.Point(9, 83);
            this.m_LegendConnection.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LegendConnection.Name = "m_LegendConnection";
            this.m_LegendConnection.Size = new System.Drawing.Size(64, 13);
            this.m_LegendConnection.TabIndex = 0;
            this.m_LegendConnection.Text = "Connection:";
            this.m_LegendConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LegendWibuBox
            // 
            this.m_LegendWibuBox.AutoSize = true;
            this.m_LegendWibuBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_LegendWibuBox.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_LegendWibuBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendWibuBox.Location = new System.Drawing.Point(17, 102);
            this.m_LegendWibuBox.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LegendWibuBox.Name = "m_LegendWibuBox";
            this.m_LegendWibuBox.Size = new System.Drawing.Size(56, 13);
            this.m_LegendWibuBox.TabIndex = 0;
            this.m_LegendWibuBox.Text = "Wibu-Key:";
            this.m_LegendWibuBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_LabelLogStatus
            // 
            this.m_LabelLogStatus.AutoSize = true;
            this.m_LabelLogStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelLogStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_LabelLogStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LabelLogStatus.Location = new System.Drawing.Point(73, 7);
            this.m_LabelLogStatus.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.m_LabelLogStatus.Name = "m_LabelLogStatus";
            this.m_LabelLogStatus.Size = new System.Drawing.Size(0, 13);
            this.m_LabelLogStatus.TabIndex = 0;
            this.m_LabelLogStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_LegendPTUStatus
            // 
            this.m_LegendPTUStatus.BackColor = System.Drawing.SystemColors.HotTrack;
            this.m_LegendPTUStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_LegendPTUStatus.ForeColor = System.Drawing.SystemColors.Info;
            this.m_LegendPTUStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendPTUStatus.Location = new System.Drawing.Point(0, 0);
            this.m_LegendPTUStatus.Name = "m_LegendPTUStatus";
            this.m_LegendPTUStatus.Size = new System.Drawing.Size(168, 31);
            this.m_LegendPTUStatus.TabIndex = 0;
            this.m_LegendPTUStatus.Text = "PTE STATUS";
            this.m_LegendPTUStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_PanelMaintenance
            // 
            this.m_PanelMaintenance.AutoSize = true;
            this.m_PanelMaintenance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_PanelMaintenance.BackColor = System.Drawing.SystemColors.Info;
            this.m_PanelMaintenance.Controls.Add(this.m_TableLayoutPanelMaintenance);
            this.m_PanelMaintenance.Controls.Add(this.m_LegendMaintenance);
            this.m_PanelMaintenance.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelMaintenance.Location = new System.Drawing.Point(10, 241);
            this.m_PanelMaintenance.Name = "m_PanelMaintenance";
            this.m_PanelMaintenance.Size = new System.Drawing.Size(168, 267);
            this.m_PanelMaintenance.TabIndex = 0;
            // 
            // m_TableLayoutPanelMaintenance
            // 
            this.m_TableLayoutPanelMaintenance.AutoSize = true;
            this.m_TableLayoutPanelMaintenance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelMaintenance.ColumnCount = 1;
            this.m_TableLayoutPanelMaintenance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelMaintenance.Controls.Add(this.m_ButtonMaintenanceSpare1, 0, 8);
            this.m_TableLayoutPanelMaintenance.Controls.Add(this.m_ButtonMaintenanceSpare2, 0, 8);
            this.m_TableLayoutPanelMaintenance.Controls.Add(this.m_ButtonViewSystemInformation, 0, 0);
            this.m_TableLayoutPanelMaintenance.Controls.Add(this.m_ButtonDiagnosticsEventLog, 0, 1);
            this.m_TableLayoutPanelMaintenance.Controls.Add(this.m_ButtonViewWatchWindow, 0, 3);
            this.m_TableLayoutPanelMaintenance.Controls.Add(this.m_ButtonDiagnosticsSelfTest, 0, 2);
            this.m_TableLayoutPanelMaintenance.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelMaintenance.Location = new System.Drawing.Point(0, 31);
            this.m_TableLayoutPanelMaintenance.Name = "m_TableLayoutPanelMaintenance";
            this.m_TableLayoutPanelMaintenance.Padding = new System.Windows.Forms.Padding(12, 4, 4, 4);
            this.m_TableLayoutPanelMaintenance.RowCount = 9;
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelMaintenance.Size = new System.Drawing.Size(168, 236);
            this.m_TableLayoutPanelMaintenance.TabIndex = 0;
            // 
            // m_ButtonMaintenanceSpare1
            // 
            this.m_ButtonMaintenanceSpare1.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonMaintenanceSpare1.Enabled = false;
            this.m_ButtonMaintenanceSpare1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_ButtonMaintenanceSpare1.Location = new System.Drawing.Point(15, 159);
            this.m_ButtonMaintenanceSpare1.Name = "m_ButtonMaintenanceSpare1";
            this.m_ButtonMaintenanceSpare1.Size = new System.Drawing.Size(136, 32);
            this.m_ButtonMaintenanceSpare1.TabIndex = 0;
            this.m_ButtonMaintenanceSpare1.TabStop = false;
            this.m_ButtonMaintenanceSpare1.Text = "Other Functio&ns";
            this.m_ButtonMaintenanceSpare1.UseVisualStyleBackColor = true;
            this.m_ButtonMaintenanceSpare1.Click += new System.EventHandler(this.m_ButtonMaintenanceSpare1_Click);
            // 
            // m_ButtonMaintenanceSpare2
            // 
            this.m_ButtonMaintenanceSpare2.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonMaintenanceSpare2.Enabled = false;
            this.m_ButtonMaintenanceSpare2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_ButtonMaintenanceSpare2.Location = new System.Drawing.Point(15, 197);
            this.m_ButtonMaintenanceSpare2.Name = "m_ButtonMaintenanceSpare2";
            this.m_ButtonMaintenanceSpare2.Size = new System.Drawing.Size(136, 32);
            this.m_ButtonMaintenanceSpare2.TabIndex = 0;
            this.m_ButtonMaintenanceSpare2.TabStop = false;
            this.m_ButtonMaintenanceSpare2.Text = "Spare 2";
            this.m_ButtonMaintenanceSpare2.UseVisualStyleBackColor = true;
            this.m_ButtonMaintenanceSpare2.Visible = false;
            this.m_ButtonMaintenanceSpare2.Click += new System.EventHandler(this.m_ButtonMaintenanceSpare2_Click);
            // 
            // m_ButtonViewSystemInformation
            // 
            this.m_ButtonViewSystemInformation.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonViewSystemInformation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_ButtonViewSystemInformation.Location = new System.Drawing.Point(15, 7);
            this.m_ButtonViewSystemInformation.Name = "m_ButtonViewSystemInformation";
            this.m_ButtonViewSystemInformation.Size = new System.Drawing.Size(136, 32);
            this.m_ButtonViewSystemInformation.TabIndex = 0;
            this.m_ButtonViewSystemInformation.TabStop = false;
            this.m_ButtonViewSystemInformation.Text = "System SW &Version";
            this.m_ButtonViewSystemInformation.UseVisualStyleBackColor = true;
            this.m_ButtonViewSystemInformation.Click += new System.EventHandler(this.m_ButtonViewSystemInformation_Click);
            // 
            // m_ButtonDiagnosticsEventLog
            // 
            this.m_ButtonDiagnosticsEventLog.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonDiagnosticsEventLog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_ButtonDiagnosticsEventLog.Location = new System.Drawing.Point(15, 45);
            this.m_ButtonDiagnosticsEventLog.Name = "m_ButtonDiagnosticsEventLog";
            this.m_ButtonDiagnosticsEventLog.Size = new System.Drawing.Size(136, 32);
            this.m_ButtonDiagnosticsEventLog.TabIndex = 0;
            this.m_ButtonDiagnosticsEventLog.TabStop = false;
            this.m_ButtonDiagnosticsEventLog.Text = "Lo&g";
            this.m_ButtonDiagnosticsEventLog.UseVisualStyleBackColor = true;
            this.m_ButtonDiagnosticsEventLog.Click += new System.EventHandler(this.m_ButtonDiagnosticsEventLog_Click);
            // 
            // m_ButtonViewWatchWindow
            // 
            this.m_ButtonViewWatchWindow.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonViewWatchWindow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_ButtonViewWatchWindow.Location = new System.Drawing.Point(15, 121);
            this.m_ButtonViewWatchWindow.Name = "m_ButtonViewWatchWindow";
            this.m_ButtonViewWatchWindow.Size = new System.Drawing.Size(136, 32);
            this.m_ButtonViewWatchWindow.TabIndex = 0;
            this.m_ButtonViewWatchWindow.TabStop = false;
            this.m_ButtonViewWatchWindow.Text = "Data &Monitoring";
            this.m_ButtonViewWatchWindow.UseVisualStyleBackColor = true;
            this.m_ButtonViewWatchWindow.Click += new System.EventHandler(this.m_ButtonViewWatchWindow_Click);
            // 
            // m_ButtonDiagnosticsSelfTest
            // 
            this.m_ButtonDiagnosticsSelfTest.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonDiagnosticsSelfTest.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_ButtonDiagnosticsSelfTest.Location = new System.Drawing.Point(15, 83);
            this.m_ButtonDiagnosticsSelfTest.Name = "m_ButtonDiagnosticsSelfTest";
            this.m_ButtonDiagnosticsSelfTest.Size = new System.Drawing.Size(136, 32);
            this.m_ButtonDiagnosticsSelfTest.TabIndex = 0;
            this.m_ButtonDiagnosticsSelfTest.TabStop = false;
            this.m_ButtonDiagnosticsSelfTest.Text = "Te&st";
            this.m_ButtonDiagnosticsSelfTest.UseVisualStyleBackColor = true;
            this.m_ButtonDiagnosticsSelfTest.Click += new System.EventHandler(this.m_ButtonDiagnosticsSelfTest_Click);
            // 
            // m_LegendMaintenance
            // 
            this.m_LegendMaintenance.BackColor = System.Drawing.SystemColors.HotTrack;
            this.m_LegendMaintenance.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_LegendMaintenance.ForeColor = System.Drawing.SystemColors.Info;
            this.m_LegendMaintenance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendMaintenance.Location = new System.Drawing.Point(0, 0);
            this.m_LegendMaintenance.Name = "m_LegendMaintenance";
            this.m_LegendMaintenance.Size = new System.Drawing.Size(168, 31);
            this.m_LegendMaintenance.TabIndex = 1;
            this.m_LegendMaintenance.Text = "MAINTENANCE";
            this.m_LegendMaintenance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_PanelAdministration
            // 
            this.m_PanelAdministration.AutoSize = true;
            this.m_PanelAdministration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_PanelAdministration.BackColor = System.Drawing.SystemColors.Info;
            this.m_PanelAdministration.Controls.Add(this.m_TableLayoutPanelAdministration);
            this.m_PanelAdministration.Controls.Add(this.m_LegendAdministration);
            this.m_PanelAdministration.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelAdministration.Location = new System.Drawing.Point(10, 508);
            this.m_PanelAdministration.Name = "m_PanelAdministration";
            this.m_PanelAdministration.Size = new System.Drawing.Size(168, 153);
            this.m_PanelAdministration.TabIndex = 0;
            // 
            // m_TableLayoutPanelAdministration
            // 
            this.m_TableLayoutPanelAdministration.AutoSize = true;
            this.m_TableLayoutPanelAdministration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelAdministration.ColumnCount = 1;
            this.m_TableLayoutPanelAdministration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelAdministration.Controls.Add(this.m_ButtonDiagnosticsInitializeEventLogs, 0, 0);
            this.m_TableLayoutPanelAdministration.Controls.Add(this.m_ButtonAdministrationSWUpload, 0, 1);
            this.m_TableLayoutPanelAdministration.Controls.Add(this.m_ButtonPasswordProtection, 0, 2);
            this.m_TableLayoutPanelAdministration.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelAdministration.Location = new System.Drawing.Point(0, 31);
            this.m_TableLayoutPanelAdministration.Name = "m_TableLayoutPanelAdministration";
            this.m_TableLayoutPanelAdministration.Padding = new System.Windows.Forms.Padding(12, 4, 4, 4);
            this.m_TableLayoutPanelAdministration.RowCount = 3;
            this.m_TableLayoutPanelAdministration.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelAdministration.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelAdministration.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_TableLayoutPanelAdministration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_TableLayoutPanelAdministration.Size = new System.Drawing.Size(168, 122);
            this.m_TableLayoutPanelAdministration.TabIndex = 0;
            // 
            // m_ButtonDiagnosticsInitializeEventLogs
            // 
            this.m_ButtonDiagnosticsInitializeEventLogs.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonDiagnosticsInitializeEventLogs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_ButtonDiagnosticsInitializeEventLogs.Location = new System.Drawing.Point(15, 7);
            this.m_ButtonDiagnosticsInitializeEventLogs.Name = "m_ButtonDiagnosticsInitializeEventLogs";
            this.m_ButtonDiagnosticsInitializeEventLogs.Size = new System.Drawing.Size(136, 32);
            this.m_ButtonDiagnosticsInitializeEventLogs.TabIndex = 0;
            this.m_ButtonDiagnosticsInitializeEventLogs.TabStop = false;
            this.m_ButtonDiagnosticsInitializeEventLogs.Text = "Log &Erasure";
            this.m_ButtonDiagnosticsInitializeEventLogs.UseVisualStyleBackColor = true;
            this.m_ButtonDiagnosticsInitializeEventLogs.Click += new System.EventHandler(this.m_ButtonDiagnosticsInitializeEventLogs_Click);
            // 
            // m_ButtonAdministrationSWUpload
            // 
            this.m_ButtonAdministrationSWUpload.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonAdministrationSWUpload.Enabled = false;
            this.m_ButtonAdministrationSWUpload.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_ButtonAdministrationSWUpload.Location = new System.Drawing.Point(15, 45);
            this.m_ButtonAdministrationSWUpload.Name = "m_ButtonAdministrationSWUpload";
            this.m_ButtonAdministrationSWUpload.Size = new System.Drawing.Size(136, 32);
            this.m_ButtonAdministrationSWUpload.TabIndex = 0;
            this.m_ButtonAdministrationSWUpload.TabStop = false;
            this.m_ButtonAdministrationSWUpload.Text = "SW &Upload";
            this.m_ButtonAdministrationSWUpload.UseVisualStyleBackColor = true;
            this.m_ButtonAdministrationSWUpload.Click += new System.EventHandler(this.m_AdministrationSpare2_Click);
            // 
            // m_ButtonPasswordProtection
            // 
            this.m_ButtonPasswordProtection.BackColor = System.Drawing.SystemColors.Control;
            this.m_ButtonPasswordProtection.Enabled = false;
            this.m_ButtonPasswordProtection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_ButtonPasswordProtection.Location = new System.Drawing.Point(15, 83);
            this.m_ButtonPasswordProtection.Name = "m_ButtonPasswordProtection";
            this.m_ButtonPasswordProtection.Size = new System.Drawing.Size(136, 32);
            this.m_ButtonPasswordProtection.TabIndex = 0;
            this.m_ButtonPasswordProtection.TabStop = false;
            this.m_ButtonPasswordProtection.Text = "Other Functio&ns";
            this.m_ButtonPasswordProtection.UseVisualStyleBackColor = true;
            this.m_ButtonPasswordProtection.Click += new System.EventHandler(this.m_ButtonAdministrationPasswordProtection_Click);
            // 
            // m_LegendAdministration
            // 
            this.m_LegendAdministration.BackColor = System.Drawing.SystemColors.HotTrack;
            this.m_LegendAdministration.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_LegendAdministration.ForeColor = System.Drawing.SystemColors.Info;
            this.m_LegendAdministration.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendAdministration.Location = new System.Drawing.Point(0, 0);
            this.m_LegendAdministration.Name = "m_LegendAdministration";
            this.m_LegendAdministration.Size = new System.Drawing.Size(168, 31);
            this.m_LegendAdministration.TabIndex = 0;
            this.m_LegendAdministration.Text = "ADMINISTRATION";
            this.m_LegendAdministration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_PanelControl
            // 
            this.m_PanelControl.AutoSize = true;
            this.m_PanelControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_PanelControl.BackColor = System.Drawing.Color.Transparent;
            this.m_PanelControl.Controls.Add(this.m_TableLayoutPanelControl);
            this.m_PanelControl.Controls.Add(this.m_LegendControl);
            this.m_PanelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelControl.Location = new System.Drawing.Point(10, 661);
            this.m_PanelControl.Name = "m_PanelControl";
            this.m_PanelControl.Size = new System.Drawing.Size(168, 48);
            this.m_PanelControl.TabIndex = 0;
            // 
            // m_TableLayoutPanelControl
            // 
            this.m_TableLayoutPanelControl.AutoSize = true;
            this.m_TableLayoutPanelControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.m_TableLayoutPanelControl.BackColor = System.Drawing.Color.Transparent;
            this.m_TableLayoutPanelControl.ColumnCount = 2;
            this.m_TableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.80952F));
            this.m_TableLayoutPanelControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.19048F));
            this.m_TableLayoutPanelControl.Controls.Add(this.m_ButtonExit, 1, 0);
            this.m_TableLayoutPanelControl.Controls.Add(this.m_ButtonHelp, 0, 0);
            this.m_TableLayoutPanelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_TableLayoutPanelControl.Location = new System.Drawing.Point(0, 10);
            this.m_TableLayoutPanelControl.Name = "m_TableLayoutPanelControl";
            this.m_TableLayoutPanelControl.RowCount = 1;
            this.m_TableLayoutPanelControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelControl.Size = new System.Drawing.Size(168, 38);
            this.m_TableLayoutPanelControl.TabIndex = 0;
            // 
            // m_ButtonExit
            // 
            this.m_ButtonExit.Location = new System.Drawing.Point(86, 3);
            this.m_ButtonExit.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.m_ButtonExit.Name = "m_ButtonExit";
            this.m_ButtonExit.Size = new System.Drawing.Size(72, 32);
            this.m_ButtonExit.TabIndex = 1;
            this.m_ButtonExit.TabStop = false;
            this.m_ButtonExit.Text = "E&xit";
            this.m_ButtonExit.UseVisualStyleBackColor = true;
            this.m_ButtonExit.Click += new System.EventHandler(this.m_ButtonExit_Click);
            // 
            // m_ButtonHelp
            // 
            this.m_ButtonHelp.Location = new System.Drawing.Point(5, 3);
            this.m_ButtonHelp.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.m_ButtonHelp.Name = "m_ButtonHelp";
            this.m_ButtonHelp.Size = new System.Drawing.Size(72, 32);
            this.m_ButtonHelp.TabIndex = 0;
            this.m_ButtonHelp.TabStop = false;
            this.m_ButtonHelp.Text = "&Help";
            this.m_ButtonHelp.UseVisualStyleBackColor = true;
            this.m_ButtonHelp.Click += new System.EventHandler(this.m_ButtonHelp_Click);
            // 
            // m_LegendControl
            // 
            this.m_LegendControl.BackColor = System.Drawing.Color.Transparent;
            this.m_LegendControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_LegendControl.ForeColor = System.Drawing.SystemColors.Info;
            this.m_LegendControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_LegendControl.Location = new System.Drawing.Point(0, 0);
            this.m_LegendControl.Name = "m_LegendControl";
            this.m_LegendControl.Size = new System.Drawing.Size(168, 10);
            this.m_LegendControl.TabIndex = 0;
            this.m_LegendControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_PanelBackground
            // 
            this.m_PanelBackground.BackColor = System.Drawing.Color.SteelBlue;
            this.m_PanelBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_PanelBackground.Controls.Add(this.m_PanelControl);
            this.m_PanelBackground.Controls.Add(this.m_PanelAdministration);
            this.m_PanelBackground.Controls.Add(this.m_PanelMaintenance);
            this.m_PanelBackground.Controls.Add(this.m_PanelPTUStatus);
            this.m_PanelBackground.Controls.Add(this.m_LabelProjectTitle);
            this.m_PanelBackground.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_PanelBackground.Location = new System.Drawing.Point(0, 0);
            this.m_PanelBackground.Name = "m_PanelBackground";
            this.m_PanelBackground.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.m_PanelBackground.Size = new System.Drawing.Size(190, 879);
            this.m_PanelBackground.TabIndex = 1;
            // 
            // ControlPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.m_PanelBackground);
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(192, 879);
            this.m_PanelPTUStatus.ResumeLayout(false);
            this.m_PanelPTUStatus.PerformLayout();
            this.m_TableLayoutPanelPTUStatus.ResumeLayout(false);
            this.m_TableLayoutPanelPTUStatus.PerformLayout();
            this.m_PanelMaintenance.ResumeLayout(false);
            this.m_PanelMaintenance.PerformLayout();
            this.m_TableLayoutPanelMaintenance.ResumeLayout(false);
            this.m_PanelAdministration.ResumeLayout(false);
            this.m_PanelAdministration.PerformLayout();
            this.m_TableLayoutPanelAdministration.ResumeLayout(false);
            this.m_PanelControl.ResumeLayout(false);
            this.m_PanelControl.PerformLayout();
            this.m_TableLayoutPanelControl.ResumeLayout(false);
            this.m_PanelBackground.ResumeLayout(false);
            this.m_PanelBackground.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_LabelProjectTitle;
        private System.Windows.Forms.Panel m_PanelPTUStatus;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelPTUStatus;
        private System.Windows.Forms.Label m_LabelWibuBoxStatus;
        private System.Windows.Forms.Label m_LabelConnection;
        private System.Windows.Forms.Label m_LabelLocation;
        private System.Windows.Forms.Label m_LabelSubsystem;
        private System.Windows.Forms.Label m_LabelCarNumber;
        private System.Windows.Forms.Label m_LegendLog;
        private System.Windows.Forms.Label m_LegendCarNumber;
        private System.Windows.Forms.Label m_LegendSubsystem;
        private System.Windows.Forms.Label m_LegendLocation;
        private System.Windows.Forms.Label m_LegendConnection;
        private System.Windows.Forms.Label m_LegendWibuBox;
        private System.Windows.Forms.Label m_LabelLogStatus;
        private System.Windows.Forms.Label m_LegendPTUStatus;
        private System.Windows.Forms.Panel m_PanelMaintenance;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelMaintenance;
        private System.Windows.Forms.Button m_ButtonMaintenanceSpare1;
        private System.Windows.Forms.Button m_ButtonMaintenanceSpare2;
        private System.Windows.Forms.Button m_ButtonViewWatchWindow;
        private System.Windows.Forms.Button m_ButtonViewSystemInformation;
        private System.Windows.Forms.Button m_ButtonDiagnosticsSelfTest;
        private System.Windows.Forms.Button m_ButtonDiagnosticsEventLog;
        private System.Windows.Forms.Label m_LegendMaintenance;
        private System.Windows.Forms.Panel m_PanelAdministration;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelAdministration;
        private System.Windows.Forms.Button m_ButtonAdministrationSWUpload;
        private System.Windows.Forms.Button m_ButtonPasswordProtection;
        private System.Windows.Forms.Button m_ButtonDiagnosticsInitializeEventLogs;
        private System.Windows.Forms.Label m_LegendAdministration;
        private System.Windows.Forms.Panel m_PanelControl;
        private System.Windows.Forms.Label m_LegendControl;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelControl;
        private System.Windows.Forms.Panel m_PanelBackground;
        private System.Windows.Forms.Button m_ButtonHelp;
        private System.Windows.Forms.Button m_ButtonExit;
        private System.Windows.Forms.Label m_LabelMode;
        private System.Windows.Forms.Label m_LegendMode;
        private System.Windows.Forms.Label m_LegendSecurityLevel;
        private System.Windows.Forms.Label m_LabelSecurityLevel;


    }
}
