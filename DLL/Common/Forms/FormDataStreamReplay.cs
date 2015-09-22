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
 *  File name:  FormDataStreamReplay.cs
 * 
 *  Revision History
 *  ----------------
 */

/* 
 *  Date        Version Author          Comments
 *  08/10/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/13/10    1.1     K.McD           1.  Included event handler for the 'F12-Header Information' function key. This displays the header information associated with the
 *                                          file.
 * 
 *  08/16/10    1.1     K.McD           1.  Bug fix SNCR 001.009. If the user exits the replay screen directly rather than returning back via the YT plot screen, 
 *                                          the function keys are not restored correctly.
 * 
 *  08/20/10    1.2     K.McD           1.  Changed image associated with F4.
 *                                      2.  Included support for a data update event, raised whenever the data on display is updated.
 *                                      3.  Included a number of #if STUB statements to support Aug sprint review.
 * 
 *  08/20/10    1.3     K.McD           1.  Include support for the BITMASK_DEMO conditional compilation symbol.
 * 
 *  11/16/10    1.4     K.McD           1.  Added checks as to whether the Dispose() method had been called in a number of member methods.
 *                                      2.  Changes resulting from the modification to the WatchFile_t structure.
 *                                      3.  Moved the configuration of the display panel and update of the user control values into the Form_Shown() event 
 *                                          handler to ensure that the user controls draw the values in the correct font when displaying the first frame.
 *                                      4.  Suspend play mode if the user presses any of the other function keys.
 *  
 *  11/17/10    1.5     K.McD           1.  Modified the UpdateWatchControlValues method() so that the upper limit of the outer for() loop was specified using the length
 *                                          of the watch control array rather than a constant.
 * 
 *  01/06/11    1.6     K.McD           1.  Bug fix - SNCR001.19. Disabled the 'Change Value' context menu option and associated double-click option associated with each 
 *                                          of the user controls associated with this form.
 * 
 *  01/10/11    1.7     K.McD           1.  Bug fix. Modified the code to initialize the timer used to play back the recorded data.
 * 
 *  01/26/11    1.8     K.McD           1.  Auto-modified as a result of the property name changes associated with the Parameter class.
 *
 *  02/03/11    1.9     K.McD           1.  Standardized the function key event handlers to: display the wait cursor, enable the Checked property of the function key and 
 *                                          clear any status message.
 * 
 *  02/14/11    1.10    K.McD           1.  Removed the member variable used to store the current workset, now included in the parent class.
 *                                      2.  Removed any references that modified the MainWindow.TaskProgressBar.Visible property as the progress bar is now permanently 
 *                                          on display.
 * 
 *  02/28/11    1.11    K.McD           1.  Renamed this form.
 *                                      2.  Modified a number of XML tags.
 *                                      3.  Modified the constructor to check whether the calling form was itself called from a multiple document interface (MDI) child 
 *                                          form or from the main application and display the appropriate text and image associated with the escape key. If called from
 *                                          the  main application, the escape key will return the user home, however, if the calling form is called from an MDI child
 *                                          form the escape key will return the user back to the MDI child form.
 *                                      4.  Modified the event handler for the escape key to check whether the the calling form was itself called from a multiple
 *                                          document interface (MDI) child form and, if so, to restore the keys associated with the MDI child form and then show
 *                                          that form rather than returning the user home.
 *                                      5.  Removed the checks to ensure that the MainWindow is defined before calling the MainWindow.WriteStatusMessage() method.
 *                                      6.  Included support to manage the Visible property of the main application progress bar as this is no longer permanently on 
 *                                          display.
 * 
 *  03/01/11    1.11.1  K.McD           1.  Modified the event handler for the 'F5-Trip' function key such that, if an entry corresponding to the time of the trip 
 *                                          is not found, the 'F5-Trip' function key is unchecked and the cursor is set to the default cursor.
 * 
 *  03/28/11    1.11.2  K.McD           1.  Auto-modified as a result of name changes to a number of enumerator fields. 
 *                                      2.  Modified the UpdateWatchControlValues() method to use the old identifier field of the watch variables defined in the 
 *                                          workset structure.
 *                                      3.  Removed support for the BITMASK_DEMO conditional compilation symbol.
 * 
 *  04/08/11    1.11.3  K.McD           1.  Auto-modified as a result of name changes to the ScreenCaptureType enumerator.
 *  
 *  10/02/11    1.11.4  K.McD           1.  Modified the F4_Click() method:
 *                                              (a) Modified the name of a local variable.
 *                                              (b) Updated the WatchFile property of the calling class to ensure that any modifications to the WatchFile.Comments 
 *                                                  property while displaying the Replay form are reflected when the user changes to the Plot display.
 *                                              (c) Updated the OpenDialogBoxList property of the calling form to ensure that any bitmask displays that are deliberately 
 *                                                  left open when the user switches back to the Plot display will be closed when the user exits the Plot display.
 *                                                  
 *                                      2.  Refactored the implementation of the IWatchFile interface.
 *                                      
 *  10/25/11    1.11.5  K.McD           1.  Bug fix - SNCR002.41. Included a check in the event handler for each ToolStripButton Click event to ensure that the logic 
 *                                          associated with the event handler is ignored if the control isn't enabled. This check is required because the event handler 
 *                                          is also called by the FormPTU.Form_KeyDown() event handler whenever the function key associated with the ToolStripButton 
 *                                          control is pressed. Consequently, in the previous implementation, the action associated with the ToolStripButton would be 
 *                                          carried out whenever the user pressed the corresponding function key, regardless of whether the ToolStropButton control 
 *                                          was enabled or not.
 *                                          
 *  11/23/11    1.17.2  K.McD           1.  Ensured that all event handler methods were detached and that the component designer variables were set to null on disposal.
 *                                      2.  Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after the Close() 
 *                                          method had been called.
 *                                          
 */

/*
 *  03/17/15    1.18    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *         
 *                                          1.  Updated Resources with a number of premium 24 x 24 images purchased from Iconfinder. 
 *                                          
 *                                      Modifications
 *                                      1.  Modified the Image property associated with the F4 and F12 function keys.
 *                                      2.  Corrected the ToolTipText associated with the F3-Save function key.
 */

/*
 *  04/16/15    1.19    K.McD           References
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
 *                                          2.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified
 *                                              to meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the
 *                                              current naming convention will still apply.
 *                                              
 *                                      2.  SNCR - R188 PTU [20 Mar 2015] Item 10. If, while viewing a saved datasteam in replay mode, that has been accessed from the
 *                                          'Open/Event Log' screen, the user opens one or more 'Show Flags' dialog boxes and then selects the 'Back' (Esc) key, these
 *                                          dialog boxes are not automatically closed. If the user then tries to close these dialog boxes once the PTU has returned to the
 *                                          'Open/Event Log' screen, an exception is thrown. On further investigation it was discovered that these dialog boxes are not
 *                                          automatically closed if the user uses the F4 key to toggle between the 'Replay' and the'YTPlot' screens.
 *                                      
 *                                      Modifications
 *                                      1.  Auto-update following the renaming of some elements of the FileHandling.LogType enumerator. Ref.: 1.1.1, 1.2.
 *                                      2.  Added a call to the CloseShowFlags() method in the F4_Click() event handler and within the section of code that determines
 *                                          that the form was called from another multiple document interface child form in the Escape_Click() event handler.
 */

/*
 *  05/13/15    1.20    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                          
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                              1.  MOC-0171-30. The ‘F4 – Rec.’ Function key will toggle between the industry standard icons
 *                                                  representing stop and start recording.
 *                                      
 *                                          2.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘WibuBox: [Present | Not Present]’.
 *                                              
 *                                      2.  SNCR - R188 PTU [20 Mar 2015] Item 17. If the user uses the ‘Remove Selected Plot(s)’ context menu option to remove one or more
 *                                          plots from the 'File/Open/Watch File', 'File/Open/Data Stream', or 'File/Open/Simulated Data Stream' screen, the
 *                                          ‘Modified Layout’ status message is not displayed until the screen is closed and then re-loaded from disk. Also, if the user
 *                                          toggles between the Replay and Plot screens the ‘Modified Layout’ status message is incorrectly cleared.
 *                                              
 *                                      Modifications
 *                                      1.  In order to accommodate the additional status labels, it is neccessary to move the 'Recording/Playback' progress bar from
 *                                          the ToolStripProgressBar control on the main window to the ProgressBar control inherited from FormWatch. Modified all methods
 *                                          that referenced the ToolStripProgressBar control to reference the ProgressBar inherited from FormWatch instead. Ref.: 1.2.
 *                                          
 *                                      2.  Modified the F4_Click() event handler to write 'Modified Layout' in Red on White to the status message label. Ref.: 2.
 *                                      
 *                                      3.  Modified the event handler associated with the [F11-Play] function key to toggle the image, tooltip and text associated with
 *                                          the F11 function key between [F11-Play] and [F11-Pause]. Ref.: 1.1.
 *                                          
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CodeProject.GraphComponents;
using Common.Communication;
using Common.Configuration;
using Common.Properties;
using Common.UserControls;

namespace Common.Forms
{
    /// <summary>
    /// Form to step through the individual frames of data contained within the historic data manager class. The form is called from the form which is used to plot
    /// the Y value against time values contained within the historic data manager class for the watch variables defined by the workset associated with the saved
    /// data file.
    /// </summary>
    public partial class FormDataStreamReplay : FormWatch, IWatchFile
    {
        #region --- Interfaces ---
        #region - [IWatchFile] -
        #region - [Member Variables] -
        /// <summary>
        /// The recorded watch data.
        /// </summary>
        private WatchFile_t m_WatchFile;
        #endregion - [Member Variables] -

        #region - [Methods] -
        /// <summary>
        /// Save the recorded watch data to disk. 
        /// </summary>
        public void SaveWatchFile()
        {
            FileHandling.Serialize<WatchFile_t>(WatchFile.FullFilename, WatchFile, FileHandling.FormatType.Binary);
        }
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets or sets the recorded watch data.
        /// </summary>
        public WatchFile_t WatchFile
        {
            get { return m_WatchFile; }
            set { m_WatchFile = value; }
        }
        #endregion - [Properties] -
        #endregion - [IWatchFile] -
        #endregion --- Interfaces ---

        #region --- Constants ---
        /// <summary>
        /// Initial interval, in ms, for the playback timer. Value: 100ms.
        /// </summary>
        private const int IntervalTimerPlayInitialValue = 100;

        /// <summary>
        /// The step interval, in ms, corresponding to the ALT key modifier applied to F7/F8. Value: 30,000 ms i.e. 30 seconds.
        /// </summary>
        private const int AltKeyStep = 30000;

        /// <summary>
        /// The step interval, in ms, associated with the CTRL key modifier applied to F7/F8. Value: 5,000 ms i.e. 5 seconds.
        /// </summary>
        private const int ControlKeyStep = 5000;

        /// <summary>
        /// The step interval, in ms, associated with the SHIFT key modifier applied to F7/F8. Value: 1,000 ms i.e. 1 second.
        /// </summary>
        private const int ShiftKeyStep = 1000;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Reference to the <c>HistoricDataManager</c> class containing the de-serialized data frames.
        /// </summary>
        private IHistoricDataManager m_HistoricDataManager;

        /// <summary>
        /// Timer that is used to playback the saved data in real time.
        /// </summary>
        private System.Windows.Forms.Timer m_TimerPlay;

        /// <summary>
        /// Flag to indicate whether the user has selected to play the saved data in real time.
        /// </summary>
        private bool m_Play = false;

        /// <summary>
        /// The frame index of the record that is to be played.
        /// </summary>
        private int m_Index;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor, required for Visual Studio.
        /// </summary>
        public FormDataStreamReplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="historicDataManager">Reference to the <c>HistoricDataManager</c> object containing the data frames that are to be displayed.</param>
        /// <param name="watchFile">The saved watch data file.</param>
        public FormDataStreamReplay(IHistoricDataManager historicDataManager, WatchFile_t watchFile ) : base(watchFile.DataStream.Workset)
        {
            InitializeComponent();

            WatchFile = watchFile;
            m_Workset = WatchFile.DataStream.Workset;
            m_HistoricDataManager = historicDataManager;

            // Instantiate the class that helps manage the user controls.
            m_WatchControlLayout = new WatchControlLayout(this, m_HistoricDataManager);

            #region - [Function Keys] -
            // Escape - Exit
            // F1 - Help
            // F2 - Print
            // F3 - Save (Not  Used)
            // F4 - YTPlot
            // F5 - Trip
            // F6 - First
            // F7 - Previous
            // F8 - Next
            // F9 - Last
            // F11 - Play
            // F12 - Header Information
            DisplayFunctionKey(F3, Resources.FunctionKeyTextSave, Resources.FunctionKeyToolTipSave, Resources.Save);
            F3.Enabled = false;
            DisplayFunctionKey(F4, Resources.FunctionKeyTextYTPlot, Resources.FunctionKeyToolTipYTPlot, Resources.YTPlot);

            // Only display the trip function key if the historic data is a fault log or simulated fault log.
            if ((m_HistoricDataManager.LogType == LogType.DataStream) || (m_HistoricDataManager.LogType == LogType.SimulatedDataStream))
            {
                DisplayFunctionKey(F5, Resources.FunctionKeyTextTrip, Resources.FunctionKeyToolTipTrip, Resources.Trip);
            }

            DisplayFunctionKey(F6, Resources.FunctionKeyTextFirst, Resources.FunctionKeyToolTipFirst, Resources.MoveFirst);
            DisplayFunctionKey(F7, Resources.FunctionKeyTextPrevious, Resources.FunctionKeyToolTipPrevious, Resources.MovePrevious);
            DisplayFunctionKey(F8, Resources.FunctionKeyTextNext, Resources.FunctionKeyToolTipNext, Resources.MoveNext);
            DisplayFunctionKey(F9, Resources.FunctionKeyTextLast, Resources.FunctionKeyToolTipLast, Resources.MoveLast);
            DisplayFunctionKey(F11, Resources.FunctionKeyTextPlay, Resources.FunctionKeyToolTipPlay, Resources.Play);
            DisplayFunctionKey(F12, Resources.FunctionKeyTextInfo, Resources.FunctionKeyToolTipInfo, Resources.FileInformation);
            #endregion - [Function Keys] -

            #region - [InformationLabels/Legend] -
            // InformationLabel 1  - Date
            // InformationLabel 2  - Start Time
            // InformationLabel 3  - Stop Time
            // InformationLabel 4  - Duration
            // InformationLabel 5  - Time
            // InformationLabel 6  - Frame
            DisplayLabel(InformationLabel1, Resources.InformationLegendDate, Color.MintCream);
            DisplayLabel(InformationLabel2, Resources.InformationLegendStartTime, Color.PaleGreen);
            DisplayLabel(InformationLabel3, Resources.InformationLegendStopTime, Color.LightCoral);
            DisplayLabel(InformationLabel4, Resources.InformationLegendDuration, Color.Khaki);
            DisplayLabel(InformationLabel5, Resources.InformationLegendRTC, Color.FromKnownColor(KnownColor.GradientInactiveCaption));
            DisplayLabel(InformationLabel6, Resources.InformationLegendFrame, Color.FromKnownColor(KnownColor.Info));
            #endregion - [InformationLabels/Legend] -

            #region - [Title] -
            Text =  Resources.TitleReplay + CommonConstants.Colon + WatchFile.Filename;
            #endregion - [Title] -

            #region - [Play Timer] -
            m_TimerPlay = new System.Windows.Forms.Timer();
            m_TimerPlay.Tick += new EventHandler(DisplayNextFrame);
            m_TimerPlay.Interval = IntervalTimerPlayInitialValue;
            m_TimerPlay.Enabled = true;
            m_TimerPlay.Stop();
            #endregion - [Play Timer] -
            
            UpdateStatusLabels();

            // Update the tab with the name of the workset.
            m_TabControl.TabPages[m_TabControl.SelectedIndex].Text = m_Workset.Name;

            // Display the values associated with the first frame.
            m_Index = 0;

            // Frame.
            InformationLabel6.Text = m_Index.ToString();

            // Don't allow the user to attempt to modify watch variable data values from this form.
            VariableControl.ReadOnly = true;
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
                    // Method called by consumer code. Call the Dispose method of any managed data members that implement the dispose method.
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    if (m_TimerPlay != null)
                    {
                        m_TimerPlay.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data 
                // members to null.
                m_TimerPlay = null;
                m_HistoricDataManager = null;

                #region - [Detach the event handler methods.] -
                this.Shown -= new System.EventHandler(this.FormDataStreamReplay_Shown);
                #endregion - [Detach the event handler methods.] -
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
            }

            base.Cleanup(disposing);
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        #region - [Form] -
        /// <summary>
        /// Event handler for the <c>Shown</c> event. (1) If the calling form was called from a multi document interface child form, change the text and image associated 
        /// with the  escape key as if this is the case the escape key will return the user to that form rather than home, (2) Initialize the progress bar; (3) add the 
        /// watch controls associated with each workgroup to the appropriate <c>TabPage</c> control; (4) show the <c>TabPage</c> corresponding to the active workgroup
        /// and (5) start the display update timer. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void FormDataStreamReplay_Shown(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Ensure that an exception isn't thrown when a child form is opened in the Visual Studio development environment.
            if (MainWindow == null)
            {
                return;
            }

            
            // Get the reference to the form that called this form, i.e. the form used to plot the Y value against time.
            FormDataStreamPlot formOpenDataStream = CalledFrom as FormDataStreamPlot;
            Debug.Assert(formOpenDataStream != null);

            // Check whether that form was called from another multiple document interface child form and, if so, modify the text and image associated with the
            // escape key.
            if (formOpenDataStream.CalledFrom != null)
            {
                DisplayFunctionKey(Escape, Resources.FunctionKeyTextEsc, Resources.FunctionKeyToolTipEsc, Resources.Edit_Undo);
            }

            // Configure the watch controls for the selected workset.
            ConfigureDisplayPanel(m_Workset, m_TabPage1, m_WatchControlSize);
            UpdateWatchControlValues(m_HistoricDataManager.FramesToDisplay[m_Index]);

            // Initialize the Playback ProgressBar.
            m_LabelProgressBar.Text = Resources.LegendPlayback;
            m_LabelProgressBar.Visible = true;

            m_ProgressBar.Maximum = m_HistoricDataManager.FramesToDisplay.Count;
            m_ProgressBar.Minimum = 0;
            m_ProgressBar.Value = 0;
            m_ProgressBar.Visible = true;

            Update();
        }
        #endregion - [Form] -

        #region - [Function Keys] -
        #region - [Escape] -
        /// <summary>
        /// Event handler for the escape key <c>Click</c> event. Close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void Escape_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (Escape.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            Escape.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // Get the reference to the form that called this form, i.e. the form used to plot the Y value against time.
            FormDataStreamPlot formOpenDataStream = CalledFrom as FormDataStreamPlot;
            Debug.Assert(formOpenDataStream != null);

            // Check whether that form was called from another multiple document interface child form and, if so, show it; otherwise, exit.
            if (formOpenDataStream.CalledFrom != null)
            {
                CloseShowFlags();

                // Clear the progress bar.
                m_ProgressBar.Value = 0;
                m_ProgressBar.Visible = false;
                m_LabelProgressBar.Text = string.Empty;
                m_LabelProgressBar.Visible = false;

                // Restore the function keys associated with the form that called this form.
                MainWindow.ToolStripFunctionKeys.SuspendLayout();
                MainWindow.ToolStripFunctionKeys.Items.Clear();
                MainWindow.ToolStripFunctionKeys.Items.AddRange(formOpenDataStream.ToolStripItemCollectionCalledFrom);
                MainWindow.ToolStripFunctionKeys.ResumeLayout(false);
                MainWindow.ToolStripFunctionKeys.PerformLayout();
                MainWindow.ToolStripFunctionKeys.Update();

                formOpenDataStream.CalledFrom.Show();
                formOpenDataStream.CalledFrom.Update();

                Escape.Checked = false;
                Cursor = Cursors.Default;

                Close();
            }
            else
            {
                Exit();
            }
        }
        #endregion - [Escape] -

        #region - [F2-Print] -
        /// <summary>
        /// Event handler for the 'F2-Print' button <c>Click</c> event. Capture the window and save the image to the specified file.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F2_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F2.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F2.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // If currently in play mode, turn it off.
            if (m_Play == true)
            {
                // Stop the play timer.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
            }

            ScreenCaptureType = ScreenCaptureType.Replay;
            base.F2_Click(sender, e);

            F2.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F2-Print] -

        #region - [F4-YTPlot] -
        /// <summary>
        /// Event handler for the 'User1' ToolStrip button. Shows the YTPLOT form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F4_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F4.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F4.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            CloseShowFlags();

            // If currently in play mode, turn it off.
            if (m_Play == true)
            {
                // Stop the play timer.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
            }

            // Allow the user to modify watch variable data values.
            VariableControl.ReadOnly = false;

            // Get the reference to the form used to plot the Y value against time.
            FormDataStreamPlot formDataStreamPlot = CalledFrom as FormDataStreamPlot;
            Debug.Assert(formDataStreamPlot != null);

            // Clear the progress bar.
            m_ProgressBar.Value = 0;
            m_ProgressBar.Visible = false;
            m_LabelProgressBar.Text = string.Empty;
            m_LabelProgressBar.Visible = false;

            // Restore the function keys associated with the form that called this form.
            MainWindow.ToolStripFunctionKeys.SuspendLayout();
            MainWindow.ToolStripFunctionKeys.Items.Clear();
            MainWindow.ToolStripFunctionKeys.Items.AddRange(m_ToolStripItemCollectionCalledFrom);
            MainWindow.ToolStripFunctionKeys.ResumeLayout(false);
            MainWindow.ToolStripFunctionKeys.PerformLayout();
            MainWindow.ToolStripFunctionKeys.Update();

            // Update the watchfile structure just in case the user has modified it.
            formDataStreamPlot.WatchFile = WatchFile;
            formDataStreamPlot.OpenedDialogBoxList = OpenedDialogBoxList;

            // Check whether the current plot uses the default plot layout and update the status message accordingly.
            if (formDataStreamPlot.IsDefaultPlotLayout == true)
            {
                MainWindow.WriteStatusMessage(string.Empty);
            }
            else
            {
                MainWindow.WriteStatusMessage(Resources.SMModifiedPlotLayout, Color.White, Color.Red);
            }

            formDataStreamPlot.Show();
            formDataStreamPlot.Update();

            F4.Checked = false;
            Cursor = Cursors.Default;

            Close();
        }
        #endregion - [F4-YTPlot] -

        #region - [F5-Trip] -
        /// <summary>
        /// Event handler for the F5 function key. Dispays the entry corresponding to the time of the actual trip.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F5_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F5.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F5.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // If currently in play mode, turn it off.
            if (m_Play == true)
            {
                // Stop the play timer.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
            }

            m_Index = m_HistoricDataManager.ContainsTime(PlotterRangeSelection.TripTime);
            if (m_Index == HistoricDataManager.NotFound)
            {
                MessageBox.Show(Resources.MBTFrameNotFound, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                F5.Checked = false;
                Cursor = Cursors.Default;
                return;
            }

            UpdateWatchControlValues(m_HistoricDataManager.FramesToDisplay[m_Index]);

            m_ProgressBar.Value = m_Index;

            // Frame number.
            InformationLabel6.Text = m_Index.ToString();

            F5.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F5-Trip] -

        #region - [F6-First] -
        /// <summary>
        /// Event handler for the F6 function key. Displays the first data entry.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F6_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F6.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F6.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // If currently in play mode, turn it off.
            if (m_Play == true)
            {
                // Stop the play timer.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
            }

            m_Index = 0;
            UpdateWatchControlValues(m_HistoricDataManager.FramesToDisplay[0]);

            m_ProgressBar.Value = m_Index;

            // Frame number.
            InformationLabel6.Text = m_Index.ToString();

            F6.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F6-First] -

        #region - [F7-Previous] -
        /// <summary>
        /// Event handler for the F7 function key. Displays the data associated with the previous frame.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F7_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F7.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F7.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // If currently in play mode, turn it off.
            if (m_Play == true)
            {
                // Stop the play timer.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
            }

            // Reference to the Modifiers property of the KeyEventArgs.
            Keys modifiers;

            // Check whether the method was called by the ToolStripButton or the KeyPress. 
            if (sender.GetType().Equals(typeof(ToolStripButton)))
            {
                // Include Try/Catch block in case LastKeyEventArgs is null.
                try
                {
                    modifiers = this.LastKeyEventArgs.Modifiers;
                }
                catch (Exception)
                {
                    modifiers = Keys.None;
                }
            }
            else
            {
                modifiers = ((KeyEventArgs)e).Modifiers;
            }

            // Increment the index by the appropriate number of frames, depending upon whether any modifier keys have been depressed.
            switch (modifiers)
            {
                case Keys.Alt:
                    m_Index -= (int)(AltKeyStep / Parameter.IntervalWatchMs);
                    break;
                case Keys.Control:
                    m_Index -= (int)(ControlKeyStep / Parameter.IntervalWatchMs);
                    break;
                case Keys.Shift:
                    m_Index -= (int)(ShiftKeyStep / Parameter.IntervalWatchMs);
                    break;
                default:
                    m_Index--;
                    break;
            }

            // Ensure that the index doesn't stray outside the valid limits.
            if (m_Index < 0)
            {
                m_Index = 0;
            }

            UpdateWatchControlValues(m_HistoricDataManager.FramesToDisplay[m_Index]);

            m_ProgressBar.Value = m_Index;

            // Frame number.
            InformationLabel6.Text = m_Index.ToString();

            F7.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F7-Previous] -

        #region - [F8-Next] -
        /// <summary>
        /// Event handler for the F8 function key. Displays the data associated with the next frame.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F8_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F8.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F8.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // If currently in play mode, turn it off.
            if (m_Play == true)
            {
                // Stop the play timer.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
            }

            // Reference to the Modifiers property of the KeyEventArgs.
            Keys modifiers;

            // Check whether the method was called by the ToolStripButton or the KeyPress. 
            if (sender.GetType().Equals(typeof(ToolStripButton)))
            {
                // Include Try/Catch block in case LastKeyEventArgs is null.
                try
                {
                    modifiers = this.LastKeyEventArgs.Modifiers;
                }
                catch (Exception)
                {
                    modifiers = Keys.None;
                }
            }
            else
            {
                modifiers = ((KeyEventArgs)e).Modifiers;
            }

            // Increment the index by the appropriate number of frames, depending upon whether any modifier keys have been depressed.
            switch (modifiers)
            {
                case Keys.Alt:
                    m_Index += (int)(AltKeyStep / Parameter.IntervalWatchMs);
                    break;
                case Keys.Control:
                    m_Index += (int)(ControlKeyStep / Parameter.IntervalWatchMs);
                    break;
                case Keys.Shift:
                    m_Index += (int)(ShiftKeyStep / Parameter.IntervalWatchMs);
                    break;
                default:
                    m_Index++;
                    break;
            }

            // Ensure that the index doesn't stray outside of the limits.
            if (m_Index >= m_HistoricDataManager.FramesToDisplay.Count)
            {
                m_Index = m_HistoricDataManager.FramesToDisplay.Count - 1;
            }

            UpdateWatchControlValues(m_HistoricDataManager.FramesToDisplay[m_Index]);

            m_ProgressBar.Value = m_Index;

            // Frame number.
            InformationLabel6.Text = m_Index.ToString();

            F8.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F8-Next] -

        #region - [F9-Last] -
        /// <summary>
        /// Event handler for the F9 function key. Displays the data associated with the last frame.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F9_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F9.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F9.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // If currently in play mode, turn it off.
            if (m_Play == true)
            {
                // Stop the play timer.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
            }

            m_Index = m_HistoricDataManager.FramesToDisplay.Count - 1;
            UpdateWatchControlValues(m_HistoricDataManager.FramesToDisplay[m_Index]);

            m_ProgressBar.Value = m_Index;

            // Frame number.
            InformationLabel6.Text = m_Index.ToString();

            F9.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F9-Last] -

        #region - [F11-Play] -
        /// <summary>
        /// Event handler for the F11 function key. Starts and stops the timer which plays the data frames in real time. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F11_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F11.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            if (m_Play == false)
            {
                // Play currently off, turn on.
                m_TimerPlay.Start();
                F11.Checked = true;
                m_Play = true;
                DisplayFunctionKey(F11, Resources.FunctionKeyTextPause, Resources.FunctionKeyToolTipPauseReplay, Resources.Pause);
            }
            else
            {
                // Play currently on, turn off.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
                DisplayFunctionKey(F11, Resources.FunctionKeyTextPlay, Resources.FunctionKeyToolTipPlay, Resources.Play);
            }

            Cursor = Cursors.Default;
        }
        #endregion - [F11-Play] -

        #region - [F12-Header Information] -
        /// <summary>
        /// Event handler for F12 function key. Shows the form whixh displays the file header information.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F12_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F12.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F12.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // If currently in play mode, turn it off.
            if (m_Play == true)
            {
                // Stop the play timer.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
            }

            FormShowHeaderInformation formShowHeaderInformation = new FormShowHeaderInformation(WatchFile.Header);
            formShowHeaderInformation.CalledFrom = this;
            formShowHeaderInformation.ShowDialog();

            F12.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F12-Header Information] -
        #endregion - [Function Keys] -

        #region - [TabControl Events]
        /// <summary>
        /// Event handler for the <c>Click</c> event associated with each <c>TabPage</c>. (1) Sets the focus to the <c>TabPage</c>, this ensures that the <c>Leave</c>
        /// event for the selected user control is triggered.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void TabPage_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_TabControl.SelectedTab.Focus();
        }
        #endregion - [TabControl Events]

        #region - [Timer Events] -
        /// <summary>
        /// Called periodically by the System.Windows.Forms.Timer to step through the data frames in real time.
        /// </summary>
        /// <remarks>The timer interval is adjusted each pass so that the playback should, more or less, match real time.</remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void DisplayNextFrame(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Get a reference time for the start of the timer event.
            DateTime startEvent = DateTime.Now;

            m_Index++;
            if (m_Index >= m_HistoricDataManager.FramesToDisplay.Count)
            {
                // Stop the play timer.
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
                m_Index = 0;
                DisplayFunctionKey(F11, Resources.FunctionKeyTextPlay, Resources.FunctionKeyToolTipPlay, Resources.Play);
            }
            else
            {
                UpdateWatchControlValues(m_HistoricDataManager.FramesToDisplay[m_Index]);

                m_ProgressBar.Value = m_Index;

                // Frame number.
                InformationLabel6.Text = m_Index.ToString();
               
                // Reschedule this event event depending upon the time between successive frames. This will enable accurate playback times
                // regardless of time interval between successive data frames.
                if (m_Index < m_HistoricDataManager.FramesToDisplay.Count - 1)
                {
                    // Work out the time interval, in ms, to the start of the next frame.
                    DateTime currentFrame = m_HistoricDataManager.FramesToDisplay[m_Index].CurrentDateTime;
                    DateTime nextFrame = m_HistoricDataManager.FramesToDisplay[m_Index + 1].CurrentDateTime;
                    TimeSpan frameInterval = nextFrame.Subtract(currentFrame);

                    // Get duration since the start of this event. 
                    TimeSpan elapsedTime = DateTime.Now.Subtract(startEvent);

                    // Work out when to schedule the next timer event.
                    int timerInterval = frameInterval.Milliseconds - elapsedTime.Milliseconds;
                    try
                    {
                        // Try setting the interval to the new schedule, however, include a catch just in case the processor is 
                        // heavily overloaded.
                        m_TimerPlay.Interval = timerInterval;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        m_TimerPlay.Interval = IntervalTimerPlayInitialValue;
                    }
                }
            }
        }
        #endregion - [Timer Events] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Close the form cleanly. Simulates the user pressing the Exit button.
        /// </summary>
        public override void Exit()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            Escape.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // If currently in play mode, turn it off.
            if (m_Play == true)
            {
                // Ensure that the play
                m_TimerPlay.Stop();
                F11.Checked = false;
                m_Play = false;
            }

            // Clear the progress bar.
            m_ProgressBar.Value = 0;
            m_ProgressBar.Visible = false;
            m_LabelProgressBar.Text = string.Empty;
            m_LabelProgressBar.Visible = false;
            //MainWindow.WriteProgressBarLegend(string.Empty);

            // Allow the user to modify watch variable data values.
            VariableControl.ReadOnly = false;

            Escape.Checked = false;
            Cursor = Cursors.Default;

            base.Exit();
        }

        /// <summary>
        /// Update the start time, stop time and time-span status labels from the data contained within the <see cref="PlotterRangeSelection"/> structure.
        /// </summary>
        private void UpdateStatusLabels()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();

            // Date.
            InformationLabel1.Text = PlotterRangeSelection.StartTime.ToShortDateString();

            // Start Time.
            InformationLabel2.Text = PlotterRangeSelection.StartTime.ToString(CommonConstants.FormatStringTimeSec);

            // Stop Time.
            InformationLabel3.Text = PlotterRangeSelection.StopTime.ToString(CommonConstants.FormatStringTimeSec);

            stringBuilder.AppendFormat("{0:D2}:", PlotterRangeSelection.TimeSpan.Hours);
            stringBuilder.AppendFormat("{0:D2}:", PlotterRangeSelection.TimeSpan.Minutes);
            stringBuilder.AppendFormat("{0:D2}.", PlotterRangeSelection.TimeSpan.Seconds);
            stringBuilder.AppendFormat("{0:D1}", PlotterRangeSelection.TimeSpan.Milliseconds / 10);

            // Duration.
            InformationLabel4.Text = stringBuilder.ToString();
        }

        /// <summary>
        /// Update the Value property for each of the watch controls associated with the workset with the values stored in the specified frame.
        /// </summary>
        protected void UpdateWatchControlValues(WatchFrame_t frame)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // RTC - Real time clock.
            InformationLabel5.Text = frame.CurrentDateTime.ToString(CommonConstants.FormatStringTimeCs);

            // Update the watch values for each column of the workset.
            short oldIdentifier, watchElementIndex;
            WatchVariable watchVariable;
            for (int column = 0; column < m_WatchControls.Length; column++)
            {
                for (int index = 0; index < m_WatchControls[column].Length; index++)
                {
                    oldIdentifier = (short)m_WatchControls[column][index].Identifier;

                    try
                    {
                        watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];

                        // Check whether the watch variable exists.
                        if (watchVariable == null)
                        {
                            // No, skip this entry.
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    // Get the watch element index associated with the old identifier.
                    watchElementIndex = m_HistoricDataManager.GetWatchElementIndex(oldIdentifier);

                    // Check whether the old watch file contains watch values associated with this old identifier.
                    if (watchElementIndex == CommonConstants.NotDefined)
                    {
                        continue;
                    }
                    Debug.Assert(watchElementIndex < frame.WatchElements.Length);

                    m_WatchControls[column][index].Value = frame.WatchElements[watchElementIndex].Value;
                }
            }

            OnDataUpdate(this, new EventArgs());
        }
        #endregion --- Methods ---
    }
}