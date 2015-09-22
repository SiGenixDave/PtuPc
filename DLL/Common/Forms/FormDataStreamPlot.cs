#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Common
 * 
 *  File name:  FormDataStreamPlot.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  06/11/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/13/10    1.1     K.McD           1.  Included event handler for the 'F12-Header Information' function key. This displays the header information
 *                                          associated with the file.
 * 
 *  08/19/10    1.1     K.McD           1.  Included support for plotting individual flags/bits associated with bitmask watch variables.
 *                                      2.  Modified design to use IPlotterWatch, IPlotterBitmask and IPlotterScalar interface definitions.
 * 
 *  11/16/10    1.2     K.McD           1.  Modified to be an abstract class. All forms used to view data streams inherit from this form.
 * 
 *  11/17/10    1.3     K.McD           1.  Modified the ConfigureTabControl() method to use the Length of the Column array to determine the number of
 *                                          columns in the workset rather than a constant.
 * 
 *  11/18/10    1.4     K.McD           1.  Modified so that the form is no longer an abstract class as child forms cannot be shown in the Visual
 *                                          Studio designer if they are derived from an abstract form.
 * 
 *  01/06/11    1.5     K.McD           1.  No longer displays the F12-Info function key by default.
 *                                      2.  Modified the Exit() method to write the car identifier associated with the current header rather than an
 *                                          empty string.
 * 
 *  01/26/11    1.6     K.McD           1.  Modified the Debug.Assert() check on the frame interval to be less stringent.
 * 
 *  02/03/11    1.7     K.McD           1.  Standardized the function key event handlers to: display the wait cursor, enable the Checked property of
 *                                          the function key and clear any status message.
 * 
 *  02/28/11    1.8     K.McD           1.  Renamed this form.
 *                                      2.  Auto-modified as a result of a number of resource and class name changes.
 *                                      3.  Modified the names of a number of member and local variables.
 *                                      4.  Removed the checks to ensure that the MainWindow is defined before calling the
 *                                          MainWindow.WriteStatusMessage() method.
 *                                      5.  Added calls to the SuspendLayout() and PerformLayout() methods in the Exit() method and the Escape key
 *                                          event handler to improve aesthetics.
 * 
 *  03/28/11    1.9     K.McD           1.  Auto-modified as a result of name changes to a number of properties associated with the Workset_t
 *                                          structure.
 *                                      2.  Modified the ConfigureTableLayoutPanel() method to use the old identifier field of the watch variables
 *                                          stored in the workset.
 * 
 *  03/31/11    1.10    K.McD           1.  SNCR001.025. Added support for the enumerator user control.
 * 
 *  04/08/11    1.11    K.McD           1.  Included the event handler for the F2-Print function key.
 *  
 *  05/23/11    1.12    K.McD           1.  Applied the 'Organize Usings/Remove and Sort' context menu.
 *                                      2.  Corrected a number of XML tags.
 *                                      
 *  06/21/11    1.13    K.McD           1.  Corrected a comment. No functional changes.
 *  
 *  10/02/11    1.14    K.McD           1.  Refactored the implementation of the IWatchFile interface.
 *                                      2.  Added support for the Edit function key. This key calls the form which allows the user to define the
 *                                          layout of the Plot 
 *                                          display.
 *                                      3.  Moved a number of virtual methods and added logic to the virtual PlotHistoricData() method.
 *                                      4.  Removed the call to clear the status message from a number of delegated methods.
 *                                      5.  Added a call to the CloseShowFlags() method in the: F4_Click() and Exit() methods.
 *                                      6.  Modified the ConfigureTabControl() method to use the PlotTabPages property of the workset.
 *                                      7.  Modified the ConfigureTableLayoutPanel() method to display only those bits of a bitmask watch variable
 *                                          that have been specified in the PlotTabPages property of the workset.
 *                                      8.  Changed the modifiers of a number of methods.
 *                                      
 *	10/05/11	1.15	Sean.D			1.	Set the F5 ToolTip to refer to a Resource string.
 *										2.	Added the F6 key and event-handler to switch between regular and Multi cursor settings.
 *										
 *  10/07/11    1.16    K.McD           1.  Included support to allow the user to remove individual plots from the display by means of the Remove
 *                                          context menu option associated with each of the plotter user controls.
 *                                          
 *                                              (a) Added the ConstructPlotTabPages() method.
 *                                              (b) Modified the Exit() method and the F5-Edit event handler to check whether the user has actively
 *                                                  removed any plots from the display and, if so, to update the PlotTabPages property of the workset
 *                                                  associated with the current watch file to reflect the changes.
 *                                                  
 *                                      2.  Renamed a local variable within the ConfigureTableLayoutPanel() method.
 *                                      3.  Modified the Shown event handler to size the scalar, enumerator and bitmask plot controls based upon the
 *                                          height defined in the PlotterControlLayout class rather than upon the size of the form.
 *                                          
 *  10/14/11    1.17    K.McD           1.  Modified the constructor to set the static MultiCursor property of the Plotter user control to be false to
 *                                          ensure that the single cursor option is used as the default.
 *                                          
 *                                      2.  Changed the Image associated with the multi-cursor function key.
 *                                      
 *                                      3.  Bug fix - SNCR002.41. Included a check in the event handler for each ToolStripButton Click event to ensure
 *                                          that the logic associated with the event handler is ignored if the control isn't enabled. This check is
 *                                          required because the event handler is also called by the FormPTU.Form_KeyDown() event handler whenever the
 *                                          function key associated with the ToolStripButton control is pressed. Consequently, in the previous
 *                                          implementation, the action associated with the ToolStripButton would be carried out whenever the user
 *                                          pressed the corresponding function key, regardless of whether the ToolStropButton control was enabled or
 *                                          not.
 *                                      
 *  11/14/11	1.17.1	Sean.D			1.	In Cleanup, added code to remove event handlers from m_PlotterControlLayout, set code to set the
 *                                          m_TabControl to null, and moved the setting of variables to null after the code for removing the event
 *                                          handlers.
 *										2.	In Exit, added code to cycle through the tabs and call Dispose on each Plotter on the panels on the tabs.
 *										
 *  11/23/11    1.17.2  K.McD           1.  Ensured that all event handler methods were detached and that the component designer variables were set to
 *                                          null on disposal.
 *                                      2.  Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after
 *                                          the Close() method had been called.
 *                                          
 *  12/01/11    1.17.3  K.McD           1.  Corrected the F5_Click() method to ensure that the HistoricDataManager class is reset if the user has
 *                                          reset the plot layout to the default setting.
 *                                          
 *                                      2.  Modified the CallFunction() method.
 *                                      
 *                                              1.  Included a check to continue to the next TabPage control if the tableLayoutPanel object associated
 *                                                  with the current TabPage control is null.
 *                                                  
 *                                              2.  Modified the check within the for loop to exit the loop when the current row index is >= the
 *                                                  number of controls associated with the TableLayoutPanel control.
 *                                                 
 *   03/17/15    1.18   K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order TBD.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                              1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                                  options will be replaced with the ‘Modify’ legend on ALL projects.
 *
 *                                      Modifications
 *                                      1.  Modified the constructor to load the 'Modify' image for function key F5.
 *                                      2.  Modified the constructor to load the 'Replay' image for function key F4.
 *                                      
 *  03/17/15    1.19   K.McD           References    
 *                                      1.  SNCR - R188 PTU [20 Mar 2015] Item 17. If the user uses the ‘Remove Selected Plot(s)’ context menu option to remove one or more
 *                                          plots from the 'File/Open/Watch File', 'File/Open/Data Stream', or 'File/Open/Simulated Data Stream' screen, the
 *                                          ‘Modified Layout’ status message is not displayed until the screen is closed and then re-loaded from disk.
 *
 *                                      Modifications
 *                                      1.  Modified the Shown event handler to write the status message 'Modified Layout' in Red on White if the plot layout has
 *                                          been modified; otherwise, string.Empty.
 *                                          
 *                                      2.  Added the IsDefaultPlotLayout property. True, if the layout is the default layout; otherwise, false, if it has been modified.
 *                                      
 *                                      3.  Added the RemoveSelected() event handler and attached this to the RemoveSelected event associated with the PlotterControlLayout
 *                                          class. This handler writes 'Modified Layout' in Red on White to the status message label and is called whenever one or more
 *                                          plots are removed from the screen using the 'Remove SElected Plot(s)' context menu.
 *                                          
 *                                      4.  Modified the DisposeOfUserControls() method to detach the event handlers associated with the PlotterControlLayout class.  
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Form to plot recorded watch values.
    /// </summary>
    public partial class FormDataStreamPlot : FormPTU, IWatchFile
    {
        #region --- Interfaces ---
        #region - [IWatchFile] -
        #region - [Member Variables] -
        /// <summary>
        /// The recorded watch data.
        /// </summary>
        protected WatchFile_t m_WatchFile;
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

        #region --- Delegates ---
        /// <summary>
        /// Genral purpose delegate used to modify the properties of the specified <c>IPlotterWatch</c> derived user control.
        /// </summary>
        /// <remarks>All parameters are passed as an object array.</remarks>
        /// <param name="plotterWatch">The <c>IPlotterWatch</c> derived user control that is to have its properties modified.</param>
        /// <param name="parameters">The parameters that are to be passed to the delegated function.</param>
        public delegate void ModifyPlotterWatch(IPlotterWatch plotterWatch, object parameters);
        #endregion --- Delegates ---

        #region --- Constants ---
        /// <summary>
        /// The key associated with the <c>TableLayoutPanel</c> control for each tab page. Value: "m_TableLayoutPanel".
        /// </summary>
        protected const string KeyTableLayoutPanel = "m_TableLayoutPanel";

        /// <summary>
        /// The preferred height, in pixels, of each row of the <c>TableLayoutPanel</c> control. Value: 100 px.
        /// </summary>
        private const int PreferredHeight = 100;

        /// <summary>
        /// The resolution, in ms, when specifying the start and stop times of a file that is to be plotted. Value: 10 ms.
        /// </summary>
        private const int ResolutionLogFileMs = 10;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Reference to the <c>HistoricDataManager</c> class - used to support zooming in and out of the plot time range.
        /// </summary>
        protected IHistoricDataManager m_HistoricDataManager;

        /// <summary>
        /// The format string to be applied when displaying time values.
        /// </summary>
        protected string m_TimeFormatString = "HH:mm:ss.ff";

        /// <summary>
        /// Reference to the class that supports the configuration, drawing and layout of multiple <c>PlotterControl</c> derived user controls.
        /// </summary>
        protected PlotterControlLayout m_PlotterControlLayout;

        /// <summary>
        /// Reference to the form which allows the user to inspect individual data frames. This is called using function key F4.
        /// </summary>
        private FormDataStreamReplay m_FormDataStreamReplay;

        /// <summary>
        /// Flag to indicate whether the current plot layout is the default layout i.e. the layout is based upon the watch variables defined in the
        /// Column field of the workset. True, if the current plot layout is the default layout; otherwise, false.
        /// </summary>
        private bool m_IsDefaultLayout;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor required by Visual Studio.
        /// </summary>
        public FormDataStreamPlot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="watchFile">The structure containing the data stream that is to be plotted.</param>
        public FormDataStreamPlot(WatchFile_t watchFile)
        {
            InitializeComponent();

            // Make the file accessible to all members methods.
            WatchFile = watchFile;
            Debug.Assert(watchFile.DataStream.FrameIntervalMs > 0, "FormDataStreamPlot.Ctor() - [watchFile.DataStream.FrameIntervalMs > 0");

            m_HistoricDataManager = new HistoricDataManager(WatchFile);

            // Instantiate the class that supports the plotting of historic data.
            m_PlotterControlLayout = new PlotterControlLayout(this, m_HistoricDataManager);

            #region - [UserControl Event Handlers] -
            m_PlotterControlLayout.RangeChanged += new EventHandler(RangeChanged);
            m_PlotterControlLayout.RangeReset += new EventHandler(RangeReset);
            m_PlotterControlLayout.ZoomSelected += new EventHandler(ZoomSelected);
            m_PlotterControlLayout.RemoveSelected += new EventHandler(RemoveSelected);
            #endregion - [Event Handlers] -

            ConfigureTabControl(m_TabControl, WatchFile);

            #region - [Function Keys] -
            // Escape - Exit
            // F1 - Help
            // F2 - Print
            // F4 - Replay
            // F5 - Configure Plot
			// F6 - MultiCursor
            DisplayFunctionKey(F4, Resources.FunctionKeyTextReplay, Resources.FunctionKeyToolTipReplay, Resources.Replay);
            DisplayFunctionKey(F5, Resources.FunctionKeyTextEdit, Resources.FunctionKeyToolTipEditPlotLayout, Resources.Modify);
			DisplayFunctionKey(F6, Resources.FunctionKeyTextMultiCursor, Resources.FunctionKeyToolTipMultiCursor, Resources.MultiCursor);
            #endregion - [Function Keys] -

            #region - [InformationLabels/Legend] -
            // InformationLabel 1  - Date
            // InformationLabel 2  - Start Time
            // InformationLabel 3  - Stop Time
            // InformationLabel 4  - Duration
            DisplayLabel(InformationLabel1, Resources.InformationLegendDate, Color.MintCream);
            DisplayLabel(InformationLabel2, Resources.InformationLegendStartTime, Color.LightGreen);
            DisplayLabel(InformationLabel3, Resources.InformationLegendStopTime, Color.Red);
            DisplayLabel(InformationLabel4, Resources.InformationLegendDuration, Color.Khaki);
            #endregion - [InformationLabels/Legend] -

            // Ensure that the single cursor option is used as the default.
            Plotter.MultiCursor = false;
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
                m_PlotterControlLayout = null;
                m_HistoricDataManager = null;
                m_FormDataStreamReplay = null;

                #region - [Detach the event handler methods.] -
                this.Shown -= new EventHandler(this.FormDataStreamPlot_Shown);
                #endregion - [Detach the event handler methods.] -

                #region - [Component Designer Variables] -
                this.m_PanelSupplementalInformation = null;
                this.m_LabelSupplementalInformation = null;
                this.m_LegendSupplementalInformation = null;
                #endregion - [Component Designer Variables] -
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
        /// Event handler for the 'Shown' event. Called every time the form is shown, similar to the 'Activated' event, however, unlike the
        /// 'Activated' event, the dimensions of the form will now be valid making this method more useful for positioning components.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void FormDataStreamPlot_Shown(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Update();

            // Ensure that an exception isn't thrown when a child form is opened in the Visual Studio development environment.
            if (MainWindow == null)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // ---------------------------------------------
            // Configure the plotter user controls.
            // ---------------------------------------------
            Debug.Assert(MainWindow != null);
            MainWindow.WriteStatusMessage(Resources.SMConfiguringPlotterControls);

            ModifyPlotterWatch setRange = new ModifyPlotterWatch(m_PlotterControlLayout.SetRangeProperties);
            ModifyPlotterWatch setAesthetics = new ModifyPlotterWatch(m_PlotterControlLayout.SetAestheticProperties);

            // Set the range parameters.
            CallFunction(m_TabControl, setRange, null);

            #region - [UserControlSize_t Definitions] -
            Debug.Assert(MainWindow != null);

            // The margins are the same for all plotter controls, however, the height varies for each control type.
            UserControlSize_t plotterScalarSize = new UserControlSize_t();
            plotterScalarSize.Margin.Left = PlotterControlLayout.MarginLeftUserControl;
            plotterScalarSize.Margin.Right = PlotterControlLayout.MarginRightUserControl;
            plotterScalarSize.Margin.Top = PlotterControlLayout.MarginTopUserControl;
            plotterScalarSize.Margin.Bottom = PlotterControlLayout.MarginBottomUserControl;
            plotterScalarSize.Size = new Size(MainWindow.DisplayRectangle.Width - plotterScalarSize.Margin.Horizontal, PlotterControlLayout.HeightPlotterControl);

            UserControlSize_t plotterEnumeratorSize;
            plotterEnumeratorSize = plotterScalarSize;
            plotterEnumeratorSize.Size = new Size(MainWindow.DisplayRectangle.Width - plotterEnumeratorSize.Margin.Horizontal,
                                                  PlotterControlLayout.HeightEnumeratorPlotterControl);
            
            UserControlSize_t plotterBitmaskSize;
            plotterBitmaskSize = plotterScalarSize;
            plotterBitmaskSize.Size = new Size(MainWindow.DisplayRectangle.Width - plotterBitmaskSize.Margin.Horizontal,
                                               PlotterControlLayout.HeightLogicAnalyzerControl);
            #endregion - [UserControlSize_t Definitions] -

            // Set the size, font and background color.
            CallFunction(m_TabControl, setAesthetics, new object[] { plotterScalarSize.Size, plotterEnumeratorSize.Size, plotterBitmaskSize.Size,
                         Parameter.Font, Color.White });

            // Set the trip time, if appropriate.
            SetTripTime(m_TabControl, PlotterRangeSelection.TripTime);

            LayoutPanelsVisible(m_TabControl, true);

            // ---------------------------------------------
            // Plot the historic data.
            // ---------------------------------------------
            MainWindow.WriteStatusMessage(Resources.SMPlottingData);
            PlotHistoricData(m_TabControl);

            if (m_IsDefaultLayout == true)
            {
                MainWindow.WriteStatusMessage(string.Empty);
            }
            else
            {
                MainWindow.WriteStatusMessage(Resources.SMModifiedPlotLayout, Color.White, Color.Red);
            }

            // Write the car identifier associated with the data stream.
            MainWindow.WriteCarIdentifier(WatchFile.Header.TargetConfiguration.CarIdentifier);
            Cursor = Cursors.Default;
        }
        #endregion - [Form] -

        #region - [Function Keys] -
        #region - [Escape] -
        /// <summary>
        ///  Event handler for the Exit button 'Click' event. Tidy up any loose ends.
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

            // Check whether this form was called from another multiple document interface child form and, if so, show it.
            if (CalledFrom != null)
            {
                // Restore the function keys associated with the form that called this form.
                MainWindow.ToolStripFunctionKeys.SuspendLayout();
                MainWindow.ToolStripFunctionKeys.Items.Clear();
                MainWindow.ToolStripFunctionKeys.Items.AddRange(m_ToolStripItemCollectionCalledFrom);
                MainWindow.ToolStripFunctionKeys.ResumeLayout(false);
                MainWindow.ToolStripFunctionKeys.PerformLayout();
                MainWindow.ToolStripFunctionKeys.Update();

                CalledFrom.Show();
                CalledFrom.Update();

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

            ScreenCaptureType = ScreenCaptureType.Plot;
            base.F2_Click(sender, e);

            F2.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F2-Print] -

        #region - [F4-Replay] -
        /// <summary>
        /// Event handler for the F4 function key. Hides this form and displays the form which allows the user to step through individual data frames.
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

            try
            {
                m_FormDataStreamReplay = new FormDataStreamReplay(m_HistoricDataManager, WatchFile);
                m_FormDataStreamReplay.MdiParent = MdiParent;

                // The CalledFrom property is used to allow the called form to reference back to this form.
                m_FormDataStreamReplay.CalledFrom = this;
                m_FormDataStreamReplay.Show();
                Hide();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            F4.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F4-Replay] -

        #region - [F5 - Edit] -
        /// <summary>
        /// Event handler for the F5 function key. Displays the dialog box that allows the user to configure the screen layout. 
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

            try
            {
                // -------------------------------------------------------------------------------------------------------------------------
                // Check whether one or more plots have been removed by the user and, if so, update the PlotTabPages property of the workset
                // associated with the current watch file.
                // -------------------------------------------------------------------------------------------------------------------------
                bool itemsRemoved;
                PlotTabPage_t[] plotTabPages = ConstructPlotTabPages(m_TabControl, out itemsRemoved);
                if (itemsRemoved == true)
                {
                    // Update the WatchFile property with the new workset.
                    WatchFile_t watchFile = WatchFile;
                    watchFile.DataStream.Workset.PlotTabPages = plotTabPages;
                    WatchFile = watchFile;
                    SaveWatchFile();
                }

                FormPlotDefine formPlotDefine = new FormPlotDefine(WatchFile.DataStream.Workset);

                // The CalledFrom property is used to allow the called form to reference back to this form.
                formPlotDefine.CalledFrom = this;
                DialogResult dialogResult = formPlotDefine.ShowDialog();

                // Check whether the PlotTabPages field of the workset has been modified.
                if (dialogResult == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    // ----------------------------------------------------------------------
                    // Re-draw the plot based upon the modified Workset.PlotTabPages setting.
                    // ----------------------------------------------------------------------
                    m_TabControl.Hide();
                    m_TabControl.SuspendLayout();
                    ConfigureTabControl(m_TabControl, WatchFile);
                    m_HistoricDataManager.Reset();
                    FormDataStreamPlot_Shown(this, new EventArgs());
                    m_TabControl.PerformLayout();
                    m_TabControl.Show();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            F5.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F5 - Edit] -

		#region - [F6 - MultiCursor] -
		/// <summary>
		/// Event handler for the F6 function key. Toggle between single cursor (default) and simultaneous cursor display. 
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

            Plotter.MultiCursor = !Plotter.MultiCursor;
			F6.Checked = Plotter.MultiCursor;
		}
		#endregion - [F6 - MultiCursor] -

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

            FormShowHeaderInformation formShowHeaderInformation = new FormShowHeaderInformation(WatchFile.Header);
            formShowHeaderInformation.CalledFrom = this;
            formShowHeaderInformation.ShowDialog();
            F12.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F12-Header Information] -
        #endregion - [Function Keys] -

        #region - [UserControl Event Handlers] -
        /// <summary>
        /// Event handler for the <c>RangeReset</c> event associated with the <c>ViewDataStream</c> class. Resets the time-span to the initial state
        /// and re-plots the data values for all watch variables.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void RangeReset(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_HistoricDataManager.Reset();
            RePlotHistoricData();
        }

        /// <summary>
        /// Event handler for the <c>ZoomSelected</c> event associated with the <c>ViewDataStream</c> class. Updates the <c>StartTime</c> and
        /// <c>StopTime</c> properties of the <c>HistoricDataManager</c> class with the values selected by the user and re-plots the data values of
        /// all frames between the specified start and stop times for all watch variables.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void ZoomSelected(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_HistoricDataManager.StartTime = PlotterRangeSelection.StartTime;
            m_HistoricDataManager.StopTime = PlotterRangeSelection.StopTime;
            m_HistoricDataManager.UpdateFrames();
            RePlotHistoricData();
        }

        /// <summary>
        /// Event handler for the <c>RangeChanged</c> event associated with the <c>ViewDataStream</c> class. Calls the <c>UpdateStatusLabels()
        /// </c> method.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void RangeChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            UpdateStatusLabels();
        }

        /// <summary>
        /// Event handler for the <c>RemoveSelected</c> event. This event handler is called when one or more plots are removed using the 'Remove Selected Plot(s)' context menu. The
        /// event handler informs the user that the plot layout has been modified.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void RemoveSelected(object sender, EventArgs e)
        {
            MainWindow.WriteStatusMessage(Resources.SMModifiedPlotLayout, Color.White, Color.Red);
        }
        #endregion - [UserControl Event Handlers] -

        #region - [TabControl Events]
        /// <summary>
        /// Event handler for the <c>Click</c> event associated with each <c>TabPage</c>. (1) Sets the focus to the selected <c>TabPage</c>, this
        /// ensures that the <c>Leave</c> event for the selected user control is triggered.
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
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Close the form cleanly.
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

            this.SuspendLayout();

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // -------------------------------------------------------------------------------------------------------------------------------------
            // Check whether one or more plots have been removed by the user and, if so, update the PlotTabPages property of the workset associated
            // with the current watch file.
            // -------------------------------------------------------------------------------------------------------------------------------------
            bool itemsRemoved;
            PlotTabPage_t[] plotTabPages = ConstructPlotTabPages(m_TabControl, out itemsRemoved);
            if (itemsRemoved == true)
            {
                // Update the WatchFile property with the new workset.
                WatchFile_t watchFile = WatchFile;
                watchFile.DataStream.Workset.PlotTabPages = plotTabPages;
                WatchFile = watchFile;
                SaveWatchFile();
            }

            MainWindow.WriteCarIdentifier(FileHeader.HeaderCurrent.TargetConfiguration.CarIdentifier);
            DisposeOfUserControls();
            this.PerformLayout();

            Escape.Checked = false;
            Cursor = Cursors.Default;

            base.Exit();
        }

        #region - [Virtual Methods] -
        /// <summary>
        /// Update the start time, stop time and time-span status labels from the data contained within the <see cref="PlotterRangeSelection"/>
        /// structure.
        /// </summary>
        protected virtual void UpdateStatusLabels()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Date.
            InformationLabel1.Text = PlotterRangeSelection.StartTime.ToShortDateString();

            // Start Time.
            InformationLabel2.Text = PlotterRangeSelection.StartTime.ToString(m_TimeFormatString);

            // Stop Time.
            InformationLabel3.Text = PlotterRangeSelection.StopTime.ToString(m_TimeFormatString);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0:D2}:", PlotterRangeSelection.TimeSpan.Hours);
            stringBuilder.AppendFormat("{0:D2}:", PlotterRangeSelection.TimeSpan.Minutes);
            stringBuilder.AppendFormat("{0:D2}.", PlotterRangeSelection.TimeSpan.Seconds);
            stringBuilder.AppendFormat("{0:D1}", PlotterRangeSelection.TimeSpan.Milliseconds / 10);

            // Duration.
            InformationLabel4.Text = stringBuilder.ToString();
        }

        /// <summary>
        /// Set the TripTime property of the plotter controls associated with the specified <c>TabControl</c>.
        /// </summary>
        /// <remarks>
        /// This method is not relevant to recorded watch variable data.
        /// </remarks>
        /// <param name="tabControl">The <c>TabControl</c> that is to be processed.</param>
        /// <param name="tripTime">The time of the trip that initiated the log.</param>
        protected virtual void SetTripTime(TabControl tabControl, DateTime tripTime)
        {
        }

        /// <summary>
        /// Initialize the <see cref="PlotterRangeSelection"/> structure using the watch variable data loaded into the
        /// <see cref="HistoricDataManager"/> class. 
        /// If the data to be displayed is not a fault log or a simulated fault log, <paramref name="tripTime"/> will be ignored.
        /// </summary>
        /// <param name="tripTime">The time of the trip if the data represents a fault log or a simulated fault log; otherwise DateTime.Now.</param>
        protected virtual void InitializePlotterRangeSelection(DateTime tripTime)
        {
        }

        /// <summary>
        /// Plot the historic data for all watch variables associated with the specified <c>TabControl</c>.
        /// </summary>
        protected virtual void PlotHistoricData(TabControl tabControl)
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

                long[] elapsedTime = m_PlotterControlLayout.GetElapsedTimes(PlotterRangeSelection.StartTime, m_HistoricDataManager);
                m_PlotterControlLayout.PlotWatchValues(tableLayoutPanel.Controls, m_HistoricDataManager, elapsedTime);
            }
        }
        #endregion - [Virtual Methods] -

        /// <summary>
        /// Configure the specified <c>TabControl</c> control so that it can be used to plot the watch values saved within the specified file.
        /// (1) Create a tab page for every display column in the workset that contains watch variables, (2) add a layout panel to each of the tab
        /// pages and (3) add configured plotter user controls corresponding to the watch variables associated with the display column to each layout
        /// panel. The size and range information is not configured at this stage.
        /// </summary>
        /// <param name="tabControl">The <c>TabControl</c> that is to be configured.</param>
        /// <param name="watchFile">The watch file containing the data that is to be plotted.</param>
        private void ConfigureTabControl(TabControl tabControl, WatchFile_t watchFile)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Load the workset associated with the saved log file.
            Workset_t workset = watchFile.DataStream.Workset;

            // Load the auto-scaling information associated with the saved log file.
            AutoScale_t[] autoScaleWatchValues = watchFile.DataStream.AutoScaleWatchValues;

            tabControl.TabPages.Clear();

            // Use the Tag field of the control to record the log type.
            tabControl.Tag = WatchFile.DataStream.LogType;

            // Check whether the TabPagePlots attribute of the workset is defined and set the flag that indicates whether the plot layout is the
            // default layout accordingly.
            m_IsDefaultLayout = false;
            if (workset.PlotTabPages == null)
            {
                m_IsDefaultLayout = true;

                // Update the information used to plot the workset values to match the column definitions of the workset.
                workset.PlotTabPages = new PlotTabPage_t[workset.Column.Length];
                for (int columnIndex = 0; columnIndex < workset.Column.Length; columnIndex++)
                {
                    workset.PlotTabPages[columnIndex].HeaderText = workset.Column[columnIndex].HeaderText;
                    workset.PlotTabPages[columnIndex].OldIdentifierList = new List<short>();
                    workset.PlotTabPages[columnIndex].DisplayMaskList = new List<uint>();
                    for (int rowIndex = 0; rowIndex < workset.Column[columnIndex].OldIdentifierList.Count; rowIndex++)
                    {
                        workset.PlotTabPages[columnIndex].OldIdentifierList.Add(workset.Column[columnIndex].OldIdentifierList[rowIndex]);
                        workset.PlotTabPages[columnIndex].DisplayMaskList.Add(uint.MaxValue);
                    }
                }
            }

            // Create a tab page for tab page definition defined within the TabPagePlots attribute of the workset.
            TableLayoutPanel tableLayoutPanel;
            TabPage tabPage;
            for (int tabPageIndex = 0; tabPageIndex < workset.PlotTabPages.Length; tabPageIndex++)
            {
                // Only add a tab page to the list if the tab page definition contains watch identifiers.
                if (workset.PlotTabPages[tabPageIndex].OldIdentifierList.Count != 0)
                {
                    tableLayoutPanel = ConstructLayoutPanel(tabPageIndex);
                    ConfigureTableLayoutPanel(tableLayoutPanel, workset, autoScaleWatchValues, tabPageIndex);
                    tabPage = ConstructTabPage(workset.PlotTabPages[tabPageIndex].HeaderText, tableLayoutPanel);
                    tabControl.TabPages.Add(tabPage);
                }
            }
        }

        /// <summary>
        /// Add the plotter controls used to plot the watch variables associated with the specified column of the workset to the specified
        /// <c>TableLayoutPanel</c>.
        /// </summary>
        /// <param name="tableLayoutPanel">The <c>TableLayoutPanel</c> that is to be configured.</param>
        /// <param name="workset">The workset containing the watch variables.</param>
        /// <param name="autoScaleWatchValues">The auto-scale values associated with the watch variables contained within the workset.</param>
        /// <param name="tabPageIndex">The index of the tab page definition that is to be processed.</param>
        private void ConfigureTableLayoutPanel(TableLayoutPanel tableLayoutPanel, Workset_t workset, AutoScale_t[] autoScaleWatchValues,
                                               int tabPageIndex)
        {
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = 0;

            // Add the row style and plotter controls to the layout panel.
            short oldIdentifier, watchElementIndex;
            WatchVariable watchVariable;
            PlotterScalar plotterScalar;
            PlotterBitmask plotterBitmask;
            PlotterEnumerator plotterEnumerator;
            uint displayMask;
            for (int oldIdentifierListIndex = 0; oldIdentifierListIndex < workset.PlotTabPages[tabPageIndex].OldIdentifierList.Count;
                 oldIdentifierListIndex++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, PreferredHeight));

                oldIdentifier = workset.PlotTabPages[tabPageIndex].OldIdentifierList[oldIdentifierListIndex];
                // TODO FormDataStreamPlot.ConfigureTableLayoutPanel(). Discuss with John Paul about a check for oldIdentifier = 1 representing not
                // valid.

                displayMask = workset.PlotTabPages[tabPageIndex].DisplayMaskList[oldIdentifierListIndex]; 

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

                // Get the watch element index associated with the old identifier identifier.
                watchElementIndex = m_HistoricDataManager.GetWatchElementIndex(oldIdentifier);

                // Check whether the old watch file contains watch values associated with this old identifier.
                if (watchElementIndex == CommonConstants.NotDefined)
                {
                    continue;
                }

                switch (watchVariable.VariableType)
                {
                    case VariableType.Scalar:
                        // Create a new row for the scalar watch variable plot.
                        tableLayoutPanel.RowCount++;

                        // Instantiate and configure a plotter control to plot the watch variable.
                        plotterScalar = 
                            m_PlotterControlLayout.ConstructPlotterScalar(oldIdentifier,
                                                                                      autoScaleWatchValues[watchElementIndex].UpperDisplayLimitEng,
                                                                                      autoScaleWatchValues[watchElementIndex].LowerDisplayLimitEng);

                        // Add the configured control to the TableLayoutPanel.
                        if (plotterScalar != null)
                        {
                            tableLayoutPanel.Controls.Add(plotterScalar);
                        }
                        break;
                    case VariableType.Enumerator:
                        // Create a new row for the enumerator watch variable plot.
                        tableLayoutPanel.RowCount++;

                        // Instantiate and configure a plotter control to plot the watch variable.
                        plotterEnumerator = 
                            m_PlotterControlLayout.ConstructPlotterEnumerator(oldIdentifier,
                                                                              autoScaleWatchValues[watchElementIndex].UpperDisplayLimitEng,
                                                                              autoScaleWatchValues[watchElementIndex].LowerDisplayLimitEng);

                        // Add the configured control to the TableLayoutPanel.
                        if (plotterEnumerator != null)
                        {
                            tableLayoutPanel.Controls.Add(plotterEnumerator);
                        }
                        break;
                    case VariableType.Bitmask:
                        byte bitCount = (byte)Lookup.WatchVariableTableByOldIdentifier.GetBitCount(oldIdentifier);

                        ulong bitMask;
                        for (byte bitIndex = 0; bitIndex < bitCount; bitIndex++)
                        {
                            // Skip, if the flag description associated with the current bit is empty.
                            if (Lookup.WatchVariableTableByOldIdentifier.GetFlagDescription(oldIdentifier, bitIndex) == string.Empty)
                            {
                                continue;
                            }

                            // Skip if the bit is not set in the display mask.
                            bitMask = (ulong)0x01 << bitIndex;
                            if ((displayMask & bitMask) != bitMask)
                            {
                                continue;
                            }

                            // Create a new row for the bitmask watch variable plot.
                            tableLayoutPanel.RowCount++;

                            // Instantiate and configure a logic analyzer control to plot the watch variable.
                            plotterBitmask = m_PlotterControlLayout.ConstructLogicAnalyzerControl(oldIdentifier, bitIndex);

                            // Add the configured control to the TableLayoutPanel.
                            if (plotterBitmask != null)
                            {
                                tableLayoutPanel.Controls.Add(plotterBitmask);
                            }
                        }
                        break;
                    default:
                        throw new InvalidEnumArgumentException("LookupTable.Watch.Items[watchIdentifier].VariableType",
                                                               (int)watchVariable.VariableType, typeof(VariableType));
                }
            }
        }

        /// <summary>
        /// Calls the specified delegated function for each row of the <c>TableLayoutPanel</c> control associated with each <c>TabPage</c> of the
        /// specified <c>TabControl</c>.
        /// </summary>
        /// <param name="tabControl">The <c>TabControl</c> that is to be processed.</param>
        /// <param name="functionDelegate">The delegate for the function that is to be called.</param>
        /// <param name="parameter">The parameters, as an object array, that are to be passsed to the delegate.</param>
        private void CallFunction(TabControl tabControl, ModifyPlotterWatch functionDelegate, object parameter)
        {
            // Reference to the TableLayoutPanel associated with each TabPage.
            TableLayoutPanel tableLayoutPanel;
            for (int index = 0; index < tabControl.TabPages.Count; index++)
            {
                tableLayoutPanel = tabControl.TabPages[index].Controls[KeyTableLayoutPanel] as TableLayoutPanel;
                if (tableLayoutPanel == null)
                {
                    continue;
                }

                for (int rowIndex = 0; rowIndex < tableLayoutPanel.Controls.Count; rowIndex++)
                {
                    functionDelegate(tableLayoutPanel.Controls[rowIndex] as IPlotterWatch, parameter);
                }
            }
        }

        /// <summary>
        /// Instantiates and initializes a new <c>TabPage</c> control and then adds the specified layout panel to the tab page.
        /// </summary>
        /// <param name="text">The text that is to appear in the tab.</param>
        /// <param name="tableLayoutPanel">The <c>TableLayoutPanel</c> that is to be added to the <c>TabPAge</c> control.</param>
        /// <returns>The initialized <c>TabPage</c> control.</returns>
        private TabPage ConstructTabPage(string text, TableLayoutPanel tableLayoutPanel)
        {
            TabPage tabPage;
            tabPage = new TabPage();
            tabPage.Text = text;
            tabPage.AutoScroll = true;
            tabPage.BackColor = Color.FromKnownColor(KnownColor.Window);
            tabPage.Click += new System.EventHandler(this.TabPage_Click);
            tabPage.Controls.Add(tableLayoutPanel);
            return tabPage;
        }

        /// <summary>
        /// Instantiates and initializes a new <c>TableLayoutPanel</c> control. This panel will be used to layout the plotter controls required to
        /// display the watch variable data associated with the tab page definition of the workset corresponding to the specified tab page definition
        /// index.
        /// </summary>
        /// <param name="tabPagePlotIndex">The index of the workset tab page definition that is associated with the <c>TableLayoutPanel</c> control.
        /// </param>
        /// <returns>The initialized <c>TableLayoutPanel</c> control.</returns>
        private TableLayoutPanel ConstructLayoutPanel(int tabPagePlotIndex)
        {
            TableLayoutPanel tableLayoutPanel;
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Name = KeyTableLayoutPanel;

            // Use the Tag field to record the display column index.
            tableLayoutPanel.Tag = tabPagePlotIndex;

            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.AutoSize = true;
            //tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            tableLayoutPanel.Visible = false;
            tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            return tableLayoutPanel;
        }

        /// <summary>
        /// Show the <c>TableLayoutPanel</c> controls associated with each <c>TabPage</c> of the specified <c>TabControl</c>.
        /// </summary>
        /// <param name="tabControl">The <c>TabControl</c> containing the <c>TableLayoutPanel</c> controls.</param>
        /// <param name="visible">Flag to control the <c>Visible</c> property of each <c>TableLayoutPanel</c> control.</param>
        private void LayoutPanelsVisible(TabControl tabControl, bool visible)
        {
            // Reference to the TableLayoutPanel associated with the current TabPage.
            TableLayoutPanel tableLayoutPanel;
            for (int index = 0; index < tabControl.TabPages.Count; index++)
            {
                tableLayoutPanel = tabControl.TabPages[index].Controls[KeyTableLayoutPanel] as TableLayoutPanel;
                tableLayoutPanel.Visible = visible;
            }
        }

        /// <summary>
        /// Re-plot the data values associated with the updated <c>HistoricDataManager></c> properties and update the status information.
        /// </summary>
        private void RePlotHistoricData()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            UpdateStatusLabels();

            // ---------------------------------------------
            // Re-plot the data.
            // ---------------------------------------------
            MainWindow.WriteStatusMessage(Resources.SMPlottingData);

            // Reset each of the plotter controls.
            ModifyPlotterWatch reset = m_PlotterControlLayout.Reset;
            CallFunction(m_TabControl, reset, null);

            PlotHistoricData(m_TabControl);

            MainWindow.WriteStatusMessage(string.Empty);
        }

        /// <summary>
        /// Instantiates and initializes an array of <c>PlotTabPage_t</c> structures to match the current plot display settings. This can then be used
        /// to set the <c>PlotTabPages</c> property of the workset associated with the current watch file.
        /// </summary>
        /// <param name="tabControl">The <c>TabControl</c> that is to be processed.</param>
        /// <param name="itemsRemoved">A flag to indicate that one or more items have been removed by the user.</param>
        /// <returns>An array of <c>PlotTabPage_t</c> structures corresponding to the current plot display settings.</returns>
        private PlotTabPage_t[] ConstructPlotTabPages(TabControl tabControl, out bool itemsRemoved)
        {
            itemsRemoved = false;

            // Initialize the PlotTabPage_t[] array.
            PlotTabPage_t[] plotTabPages = new PlotTabPage_t[tabControl.TabPages.Count];
            for (int tabIndex = 0; tabIndex < tabControl.TabPages.Count; tabIndex++)
            {
                plotTabPages[tabIndex].HeaderText = tabControl.TabPages[tabIndex].Text;
                plotTabPages[tabIndex].OldIdentifierList = new List<short>();
                plotTabPages[tabIndex].DisplayMaskList = new List<uint>();
            }

            TableLayoutPanel tableLayoutPanel;
            for (int tabIndex = 0; tabIndex < tabControl.TabPages.Count; tabIndex++)
            {
                // Get the TableLayoutPanel control assoiated with the current TabPage.
                tableLayoutPanel = tabControl.TabPages[tabIndex].Controls[KeyTableLayoutPanel] as TableLayoutPanel;
                Debug.Assert(tableLayoutPanel != null, "FormDataStreamPlot.ConstructPlotTabPages - [tableLayoutPanel != null]");

                // Skip if there are no plotter controls associated with the TableLayoutPanel.
                TableLayoutControlCollection controlCollection = tableLayoutPanel.Controls;
                if (controlCollection.Count == 0)
                {
                    continue; ;
                }

                short oldIdentifierPrevious = CommonConstants.NotDefined;
                for (int controlIndex = 0; controlIndex < controlCollection.Count; controlIndex++)
                {
                    // Get the old identifier associated with the plot.
                    IPlotterWatch plotterWatch = controlCollection[controlIndex] as IPlotterWatch;

                    // Skip if the control does not implement the IPlotterWatch interface.
                    if (plotterWatch == null)
                    {
                        continue;
                    }

                    short oldIdentifier = plotterWatch.Identifier;

                    // Check whether the old identifier is valid.
                    try
                    {
                        WatchVariable watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];

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

                    if ((plotterWatch as IPlotterScalar) != null)
                    {
                        if (plotterWatch.Removed == false)
                        {
                            plotTabPages[tabIndex].OldIdentifierList.Add(oldIdentifier);
                            plotTabPages[tabIndex].DisplayMaskList.Add(uint.MaxValue);
                        }
                        else
                        {
                            itemsRemoved = true;
                        }

                        oldIdentifierPrevious = oldIdentifier;
                    }
                    else if ((plotterWatch as IPlotterEnumerator) != null)
                    {
                        if (plotterWatch.Removed == false)
                        {
                            plotTabPages[tabIndex].OldIdentifierList.Add(oldIdentifier);
                            plotTabPages[tabIndex].DisplayMaskList.Add(uint.MaxValue);
                        }
                        else
                        {
                            itemsRemoved = true;
                        }

                        oldIdentifierPrevious = oldIdentifier;
                    }
                    else if ((plotterWatch as IPlotterBitmask) != null)
                    {
                        if (plotterWatch.Removed == false)
                        {
                            // A flag to indicate whether the plot is associated with: (true) a new bitmask watch variable or (false) another bit of
                            // the current bitmask watch variable.
                            bool newBitmask;

                            // Check whether this represents a new bitmask watch variable or the plot of another bit of an existing bitmask watch
                            // variable.
                            if (oldIdentifier != oldIdentifierPrevious)
                            {
                                newBitmask = true;
                                oldIdentifierPrevious = oldIdentifier;
                            }
                            else
                            {
                                newBitmask = false;
                            }

                            // Determine which bit of the bitmask the plot represents.
                            ulong bitMask = (ulong)0x01 << (plotterWatch as IPlotterBitmask).Bit;
                            if (newBitmask == true)
                            {
                                plotTabPages[tabIndex].OldIdentifierList.Add(oldIdentifier);
                                plotTabPages[tabIndex].DisplayMaskList.Add((uint)bitMask);
                            }
                            else
                            {
                                // Get the value for the last entry in the DisplayMask list.
                                int count = plotTabPages[tabIndex].DisplayMaskList.Count;
                                uint displayMask = plotTabPages[tabIndex].DisplayMaskList[count - 1];

                                // Assert the current bit.
                                displayMask |= (uint)bitMask;
                                plotTabPages[tabIndex].DisplayMaskList.RemoveAt(count - 1);
                                plotTabPages[tabIndex].DisplayMaskList.Add(displayMask);
                            }
                        }
                        else
                        {
                            itemsRemoved = true;
                        }
                    }
                }
            }

            return plotTabPages;
        }

        /// <summary>
        /// Dispose of the user control associated with each <c>TabPage</c> of the <c>TabControl</c>.
        /// </summary>
        private void DisposeOfUserControls()
        {
            if (m_PlotterControlLayout != null)
            {
                m_PlotterControlLayout.RangeChanged -= new EventHandler(RangeChanged);
                m_PlotterControlLayout.RangeReset -= new EventHandler(RangeReset);
                m_PlotterControlLayout.ZoomSelected -= new EventHandler(ZoomSelected);
                m_PlotterControlLayout.RemoveSelected -= new EventHandler(RemoveSelected);
                m_PlotterControlLayout = null;
            }

            // Detatch the event handler methods associated with each iPlotterWatch control.
            foreach (TabPage tabPage in m_TabControl.TabPages)
            {
                // There should only be one panel per TabPage.
                foreach (Panel panel in tabPage.Controls)
                {
                    // Dispose of each iPlotter control.
                    int count = panel.Controls.Count;
                    for (int controlIndex = 0; controlIndex < count; controlIndex++)
                    {
                        IPlotterWatch iPlotterWatch = (IPlotterWatch)panel.Controls[0];
                        iPlotterWatch.Dispose();
                    }
                }
            }

            m_TabControl.TabPages.Clear();
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the flag that indicates whether the current plot layout is the default layout i.e. the layout is based upon the watch variables defined in the
        /// Column field of the workset. True, if the current plot layout is the default layout; otherwise, false.
        /// </summary>
        public bool IsDefaultPlotLayout
        {
            get { return m_IsDefaultLayout; }
        }
        #endregion --- Properties ---
    }
}