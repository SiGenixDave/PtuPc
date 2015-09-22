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
 *  Project:    Common
 * 
 *  File name:  DigitalControl.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/14/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.UserControls
{
    partial class DigitalControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region --- Disposal ---
        /// <summary>
        /// Destructor / Finalizer. Because Dispose calls GC.SuppressFinalize, this method is called by the garbage collection process only
        /// if the consumer of the object doesn't call Dispose as it should.
        /// </summary>
        ~DigitalControl()
        {
            Dispose(false);
        }

        /// <summary>
        /// Public implementation of the IDisposable.Dispose method, called by the consumer of the object in order to free unmanaged resources
        /// deterministically.
        /// </summary>
        public new void Dispose()
        {
            // Call the protected Dispose overload and pass a value of 'true' to indicate that the Dispose is being called by consumer code, not
            // by the garbage collector.
            Dispose(true);

            // Because the Dispose method performs all necessary cleanup, ensure the garbage collector does not call the class destructor.
            GC.SuppressFinalize(this);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (!m_IsDisposed)
            {
                try
                {
                    if (disposing)
                    {
                        if (components != null)
                        {
                            components.Dispose();
                        }

                        // Detach the event handler.
                        m_BlinkTimer.Tick -= TimerExpired;

                        if (m_BlinkTimer != null)
                        {
                            m_BlinkTimer.Dispose();
                        }
                    }

                    // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data 
                    // members to null.
                    m_BlinkTimer = null;

                    #region --- Windows Form Designer Variables ---
                    // Detach the Windows Form Designer event handler delegates.
                    this.m_MenuItemDiagnosticInformation.Click -= m_MenuItemDiagnosticInformation_Click;
                    this.m_MenuItemCancelSelection.Click -= m_MenuItemCancelSelection_Click;
                    this.Click -= DigitalControl_Click;
                    this.Leave -= DigitalControl_Leave;

                    // Set the Windows Form Designer variables to null.
                    m_MenuItemDiagnosticInformation = null;
                    m_ContextMenu = null;
                    m_ContextMenu = null;
                    #endregion --- Windows Form Designer Variables ---
                }
                catch (Exception)
                {
                    // Don't do anything, just ensure that we don't raise an exception.
                }
                base.Dispose(disposing);
            }
            m_IsDisposed = true;
        }
        #endregion --- Disposal ---

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_MenuItemDiagnosticInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemCancelSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.m_LblDigital = new System.Windows.Forms.Label();
            this.m_ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ContextMenu
            // 
            this.m_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemDiagnosticInformation,
            this.m_MenuItemCancelSelection});
            this.m_ContextMenu.Name = "m_ContextMenu";
            this.m_ContextMenu.Size = new System.Drawing.Size(197, 48);
            this.m_ContextMenu.TabStop = true;
            // 
            // m_MenuItemDiagnosticInformation
            // 
            this.m_MenuItemDiagnosticInformation.Name = "m_MenuItemDiagnosticInformation";
            this.m_MenuItemDiagnosticInformation.Size = new System.Drawing.Size(196, 22);
            this.m_MenuItemDiagnosticInformation.Text = "&Diagnostic Information";
            this.m_MenuItemDiagnosticInformation.Visible = false;
            this.m_MenuItemDiagnosticInformation.Click += new System.EventHandler(this.m_MenuItemDiagnosticInformation_Click);
            // 
            // m_MenuItemCancelSelection
            // 
            this.m_MenuItemCancelSelection.Name = "m_MenuItemCancelSelection";
            this.m_MenuItemCancelSelection.Size = new System.Drawing.Size(196, 22);
            this.m_MenuItemCancelSelection.Text = "&Cancel Selection";
            this.m_MenuItemCancelSelection.Click += new System.EventHandler(this.m_MenuItemCancelSelection_Click);
            // 
            // m_LblDigital
            // 
            this.m_LblDigital.AutoEllipsis = true;
            this.m_LblDigital.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LblDigital.Location = new System.Drawing.Point(0, 0);
            this.m_LblDigital.Name = "m_LblDigital";
            this.m_LblDigital.Size = new System.Drawing.Size(200, 23);
            this.m_LblDigital.TabIndex = 0;
            this.m_LblDigital.Text = "<Flag Name> Field";
            this.m_LblDigital.Click += new System.EventHandler(this.DigitalControl_Click);
            this.m_LblDigital.Leave += new System.EventHandler(this.DigitalControl_Leave);
            // 
            // DigitalControl
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            //this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ContextMenuStrip = this.m_ContextMenu;
            this.Controls.Add(this.m_LblDigital);
            this.Name = "DigitalControl";
            this.Size = new System.Drawing.Size(200, 23);
            this.Click += new System.EventHandler(this.DigitalControl_Click);
            this.GotFocus += new System.EventHandler(this.DigitalControl_GotFocus);
            this.Leave += new System.EventHandler(this.DigitalControl_Leave);
            this.m_ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Reference to the context menu associated with this user control.
        /// </summary>
        protected System.Windows.Forms.ContextMenuStrip m_ContextMenu;

        /// <summary>
        /// Reference to the 'Diagnostic Information' menu option of the context menu.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_MenuItemDiagnosticInformation;
        
        /// <summary>
        /// Reference to the 'Cancel Selection' menu option of the context menu.
        /// </summary>
        protected System.Windows.Forms.ToolStripMenuItem m_MenuItemCancelSelection;
        private System.Windows.Forms.Label m_LblDigital;
    }
}
