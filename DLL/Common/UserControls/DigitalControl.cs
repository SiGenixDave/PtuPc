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
 *  File name:  DigitalControl.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McDonald      First Release.
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Drawing;

using Common.Configuration;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// Multi-purpose label user control including: pre-defined ON and OFF state background and foreground colours; support for diagnostic help 
    /// context menu option and a blink facility.
    /// </summary>
    public partial class DigitalControl : UserControl
    {
        /// <summary>
        /// Delegate used to invoke a call to any standard method that does not require any parameters.
        /// </summary>
        protected delegate void InvokeNoParameterMethod();

        #region --- Constants ---
        /// <summary>
        /// The default interval, in ms, between successive blinks.
        /// </summary>
        private const int DefaultIntervalBlinkMs = 100;
        #endregion --- Constants ---

        #region --- Member Variables ---
        #region - [Static Variables] -
        #region - [Static Colours] -
        /// <summary>
        /// The highlight colour associated with the ON state.
        /// </summary>
        private static Color m_Highlight = Color.FromKnownColor(KnownColor.Highlight);

        /// <summary>
        /// The highlight text colour associated with the ON state.
        /// </summary>
        private static Color m_HighlightText = Color.FromKnownColor(KnownColor.HighlightText);
        #endregion - [Static Colours] -

        /// <summary>
        /// The fully qualified filename of the diagnostic help file.
        /// </summary>
        private static string m_DiagnosticHelpFileName;

        /// <summary>
        /// Flag to indiate whether the diagnostic help file exists. True, if the diagnostic help file exists; otherwise, false.
        /// </summary>
        private static bool m_DiagnosticHelpExists;
        #endregion - [Static Variables] -

        #region --- [Colours] ---
        /// <summary>
        /// The background colour associated with the ON state.
        /// </summary>
        //private Color m_BackColorOn = m_DefaultBackColorOn;
        private Color m_BackColorOn;

        /// <summary>
        /// The foreground colour associated with the ON state.
        /// </summary>
        //private Color m_ForeColorOn = m_DefaultForeColorOn;
        private Color m_ForeColorOn = Color.FromKnownColor(KnownColor.ControlText);

        /// <summary>
        /// The background colour associated with the OFF state.  
        /// </summary>
        //private Color m_BackColorOff = m_DefaultBackColorOff;
        private Color m_BackColorOff;

        /// <summary>
        /// The foreground colour associated with the OFF state.
        /// </summary>
        //private Color m_ForeColorOff = m_DefaultForeColorOff;
        private Color m_ForeColorOff = Color.FromKnownColor(KnownColor.ControlText);
        #endregion --- [Colours] ---

        /// <summary>
        /// Flag to indicate whether the Dispose() method has been called. True indicates that the Dispose() method has been called; otherwise, false.
        /// </summary>
        private bool m_IsDisposed;

        /// <summary>
        /// True, if the <c>State</c> property is currently in the ON state; otherwise, false.
        /// </summary>
        private bool m_IsOn;
        
        /// <summary>
        /// The duration of the blink, in ms.
        /// </summary>
        private int m_BlinkDurationMs = DefaultIntervalBlinkMs;

        /// <summary>
        /// The current digital IO state.
        /// </summary>
        private bool m_State;

        /// <summary>
        /// Timer to control the blink interval.
        /// </summary>
        private System.Windows.Forms.Timer m_BlinkTimer;
        #endregion --- Private Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Initializes the blink timer and background/foreground colours.
        /// </summary>
        public DigitalControl()
        {
            InitializeComponent();

            // Show the usercontrol in the OFF state, by default.
            //m_LblDigital.BackColor = m_BackColorOff;
            //m_LblDigital.ForeColor = m_ForeColorOff;

            // Initialize the flash timer.
            m_BlinkTimer = new System.Windows.Forms.Timer();
            m_BlinkTimer.Tick += new EventHandler(TimerExpired);

            // Enable / Disable the 'Diagnostic Information' context menu depending upon whether the diagnostic help file exists.
            m_MenuItemDiagnosticInformation.Enabled = (m_DiagnosticHelpExists) ? true : false;

            // Show the context menu options.
            m_MenuItemDiagnosticInformation.Visible = m_MenuItemDiagnosticInformation.Enabled;
            m_MenuItemCancelSelection.Visible = m_MenuItemDiagnosticInformation.Enabled;
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Cleanup(bool disposing)
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
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        /// <summary>
        /// Event handler for the Click event. Sets the focus to the panel on which the control has been placed and highlights the control to 
        /// indicate that the control has been selected.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void DigitalControl_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Ensure that the 'Leave' event handler is called for the previously selected control.
            this.Focus();
        }

        /// <summary>
        /// Event handler for the user control <c>GotFocus</c>  event. Highlights the user control.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void DigitalControl_GotFocus(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Highlight the control.
            m_LblDigital.BackColor = m_Highlight;
            m_LblDigital.ForeColor = m_HighlightText;
        }

        /// <summary>
        /// Event handler for the Leave event. Turns the control highlight off.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void DigitalControl_Leave(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Update the current background and foreground colours.
            if (this.State == true)
            {
                m_LblDigital.BackColor = m_BackColorOn;
                m_LblDigital.ForeColor = m_ForeColorOn;
            }
            else
            {
                m_LblDigital.BackColor = m_BackColorOff;
                m_LblDigital.ForeColor = m_ForeColorOff;
            }
        }

        #region - [CONTEXT MENU] -
        /// <summary>
        /// Event handler for the Diagnostic Information context menu option <c>Click</c>event. Turns the control highlight off and displays the 
        /// diagnostic information corresponding to the channel reference of the selected control.
        /// </summary>
        /// <remarks>The diagnostic help information is derived using the channel address which is stored within the Tag field of the control.</remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemDiagnosticInformation_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Turn the highlight off.
            /*
            this.BackColorOn = m_DefaultBackColorOn;
            this.ForeColorOn = m_DefaultForeColorOn;
            this.BackColor = m_DefaultBackColorOff;
            this.ForeColor = m_DefaultForeColorOff;

            // Update the current background and foreground colours.
            if (this.State == true)
            {
                this.BackColor = m_DefaultBackColorOn;
                this.ForeColor = m_DefaultForeColorOn;
            }
            else
            {
                this.BackColor = m_DefaultBackColorOff;
                this.ForeColor = m_DefaultForeColorOff;
            }
            */

            // Ensure that the ShowHelp method is not called if the diagnostic help file does not exist.
            if (m_DiagnosticHelpExists)
            {
                try
                {
                    int channel = (int)this.Tag;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Event handler for the 'Cancel Selection' context menu option <c>Click</c> event. Cancels the current selection.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemCancelSelection_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            /*
            // Turn the highlight off.
            this.BackColorOn = m_DefaultBackColorOn;
            this.ForeColorOn = m_DefaultForeColorOn;
            this.BackColor = m_DefaultBackColorOff;
            this.ForeColor = m_DefaultForeColorOff;

            // Update the current background and foreground colours.
            if (this.State == true)
            {
                this.BackColor = m_DefaultBackColorOn;
                this.ForeColor = m_DefaultForeColorOn;
            }
            else
            {
                this.BackColor = m_DefaultBackColorOff;
                this.ForeColor = m_DefaultForeColorOff;
            }
            */
        }
        #endregion - [CONTEXT MENU] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        #region - [Static] -
        /// <summary>
        /// Checks whether the specified diagnostic help file exists and, if so, generates and stores the fully qualified file name and updates the internal flag
        /// which indicates whether the help file exists.
        /// </summary>
        /// <remarks>If diagnostic help is to be supported then this static method must be called prior to instantiation.</remarks>
        /// <param name="diagnosticHelpName">The file name of the diagnostic help file.</param>
        private static void CheckForDiagnosticHelp(string diagnosticHelpName)
        {
            // -------------------------------------------------------- //
            // Check the status of the compiled diagnostic help file 
            // -------------------------------------------------------- //

            // Generate the fully qualified file name of the diagnostic help file.
            m_DiagnosticHelpFileName = DirectoryManager.PathDiagnosticHelpFiles + @"\" + diagnosticHelpName;

            FileInfo fileInfo = new FileInfo(m_DiagnosticHelpFileName);
            m_DiagnosticHelpExists = fileInfo.Exists;
        }
        #endregion - [Static] -

        /// <summary>
        /// Sets the background and foreground colours of the control to reflect the state of the user control.
        /// </summary>
        /// <param name="state">The state of the user control.</param>
        public void SetState(bool state)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (state == true)
            {
                m_LblDigital.BackColor = m_BackColorOn;
                m_LblDigital.ForeColor = m_ForeColorOn;
            }
            else
            {
                m_LblDigital.BackColor = m_BackColorOff;
                m_LblDigital.ForeColor = m_ForeColorOff;
            }
        }

        /// <summary>
        /// Calls the Blink() method in a ThreadSafe way.
        /// </summary>
        public void BlinkThreadSafe()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ISynchronizeInvoke synchronizer = m_LblDigital;
            if (synchronizer.InvokeRequired == false)
            {
                Blink();
            }
            else
            {
                synchronizer.Invoke(new InvokeNoParameterMethod(Blink), null);
            }
        }

        /// <summary>
        /// Initiates a blink by changing the background colour to the ON colour and then initiating the flash timer. On expiration of the blink
        /// timer the background will revert to the OFF colour, see the event handler for the timer expired event.
        /// </summary>
        public void Blink()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Only set to the ON state if the state is currently OFF.
            if (m_IsOn == false)
            {
                m_LblDigital.BackColor = m_BackColorOn;
                m_BlinkTimer.Start();
                m_IsOn = true;
            }
        }

        /// <summary>
        /// Event handler for the timer expired event. Reverts the background colour to the OFF colour and stops the blink timer. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void TimerExpired(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_LblDigital.BackColor = m_BackColorOff;
            m_BlinkTimer.Stop();
            m_LblDigital.Invalidate();
            m_IsOn = false;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the flag which indicates whether the Dispose() method has been called. True indicates that the Dispose() method has been called; otherwise, false.
        /// </summary>
        protected new bool IsDisposed
        {
            get
            {
                lock (this)
                {
                    return m_IsDisposed;
                }
            }

            set
            {
                lock (this)
                {
                    m_IsDisposed = value;
                }
            }
        }

        #region - [Colours] -
        /// <summary>
        /// Gets or sets the background colour of the user control.
        /// </summary>
        /// <remarks>Override the <c>BackColor</c> property of the user control and set the <c>Browsable</c> reflection property to false so that it
        /// doesn't appear in the property window.</remarks>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The background colour of the user control")
        ]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Gets or sets the foreground colour of the user control.
        /// </summary>
        /// <remarks>Override the <c>BackColor</c> property of the user control and set the <c>Browsable</c> reflection property to false so that it
        /// doesn't appear in the property window.</remarks>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The foreground colour of the user control")
        ]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Gets or sets the background colour associated with the OFF state.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The background colour associated with the OFF state.")
        ] 
        public Color BackColorOff
        {
            get { return m_BackColorOff; }
            set 
            {
                m_BackColorOff = value;

                // Update the property window.
                SetState(m_State);
            }
        }

        /// <summary>
        /// Gets or sets the foreground colour associated with the OFF state.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The foreground colour associated with the OFF state.")
        ]
        public Color ForeColorOff
        {
            get { return m_ForeColorOff; }
            set 
            { 
                m_ForeColorOff = value;

                // Update the property window.
                SetState(m_State);
            }
        }

        /// <summary>
        /// Gets or sets the background colour associated with the ON state.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The background colour associated with the ON state.")
        ] 
        public Color BackColorOn
        {
            get { return m_BackColorOn; }
            set 
            { 
                m_BackColorOn = value;

                // Update the property window.
                SetState(m_State);
            }
        }

        /// <summary>
        /// Gets or sets the foreground colour associated with the ON state.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The foreground colour associated with the ON state.")
        ]
        public Color ForeColorOn
        {
            get { return m_ForeColorOn; }
            set 
            { 
                m_ForeColorOn = value;

                // Update the property window.
                SetState(m_State);
            }
        }
        #endregion - [Colours] -

        /// <summary>
        /// Gets or sets the duration of the blink, in ms.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The duration of the blink, in ms.")
        ] 
        public int IntervalBlinkMs
        {
            get { return m_BlinkDurationMs; }
            set 
            { 
                m_BlinkDurationMs = value;
                m_BlinkTimer.Interval = m_BlinkDurationMs;              // Update the timer interval.
            }
        }

        /// <summary>
        /// Gets or sets the current state of the user control i.e. true (ON) or false (OFF).
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The current state of the user control i.e. true (ON) or false (OFF).")
        ]
        public bool State
        {
            get { return m_State; }
            set
            {
                m_State = value;
                SetState(m_State);
            }
        }

        /// <summary>
        /// Gets or sets the text associated with the control.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The text associated with the control.")
        ]
        public string DigitalControlText
        {
            get { return m_LblDigital.Text; ; }
            set { m_LblDigital.Text = value; }
        }
        #endregion Properties ---
    }
}
