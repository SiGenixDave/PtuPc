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
 *  File name:  PlotterScalar.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/09/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  03/28/11    1.1     K.McD           1.  Modified to use the old identifier field to specify the watch variable associated with the control.
 *                                      2.  Renamed the WatchIdentifier property to OldIdentifier.
 * 
 *  03/31/11    1.2     K.McD           1.  Added a Debug.Assert to the constructor the ensure that the oldIdentifier parameter corresponds to a scalar watch variable.
 *                                      2.  Modified a number of XML tags.
 *                                      
 *  10/07/11    1.3     K.McD           1.  Added support for the Remove context menu option. This menu option removes the control from the current plot display.
 *  
 *  10/25/11    1.4     K.McD           1.  Added support for the 'Show Definition' context menu option.
 *                                      2.  Changed the order of the context menu options so that the 'Show Definition' menu option was the first option. This was 
 *                                          implemented to improve the positioning of the help window when the definition was displayed.
 *                                      3.  Made the Removed property read/write so that it may be manipulated remotely.
 *                                      4.  Modified the layout of a number of properties.
 *                                      5.  Added the SetHighlight() and ShowHelpPopup() methods.
 *                                      6.  Removed the redundant m_ButtonYAxisPlus_Click() and m_ButtonYAxisMinus_Click() event handlers.
 *                                      7.  Modified the m_ToolStripMenuItemRemove_Click() event handler to remove all selected Plotter derived user controls.
 *                                      8.  Added the event handler for the MouseDown event.
 *                                      9.  Modified the Size property such that the width of the Description label spans the full width of the user control up to 
 *                                          the Units label. This ensures that the SetHighlight() method highlights the full width of the user control.
 *                                      10. Added the context menu Opened event handler to ensure that only the 'Remove Selected Plot(s)' context menu option is 
 *                                          enabled if multiple controls are selected when the user opens the context menu.
 *                                          
 *  08/06/13    1.5     K.McD           1.  Added the 'Remove Selected Plot(s)' ToolStripMenuItem control as a property to allow client programs to access the properties of this 
 *                                          context menu option.
 *
 */
#endregion --- Revision History ---

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using CodeProject.GraphComponents;
using Common.Configuration;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// A user control to plot the value of a specified scalar watch variable over a specified time period.
    /// </summary>
    public partial class PlotterScalar : UserControl, IPlotterScalar
    {
        #region --- Constants ---
        /// <summary>
        /// The key string used to access the 'Remove Selected Plot(s)' context menu item. Value: "m_ToolStripMenuItemRemoveSelected".
        /// </summary>
        private const string KeyToolStripMenuItemRemoveSelected = "m_ToolStripMenuItemRemoveSelected";

        /// <summary>
        /// The X axis border to allow when positioning the limit changing buttons. Value: 10.
        /// </summary>
        private const int ButtonXBorder = 10;

        /// <summary>
        /// The border between the location of the Units label and the size of the control.
        /// </summary>
        private const int BorderXUnits = 50;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Flag to indicate whether the object has been disposed. True, indicates that the object has already been disposed; otherwise, false.
        /// </summary>
        private bool m_IsDisposed;

        /// <summary>
        /// The text associated with the description field.
        /// </summary>
        private string m_Description = string.Empty;

        /// <summary>
        /// The engineering units associated with the Y axis.
        /// </summary>
        private string m_Units = string.Empty;

        /// <summary>
        /// The watch variable identifier associated with the control.
        /// </summary>
        private short m_Identifier = 0;

        /// <summary>
        /// A flag that indicates whether the user has removed this plot from the display.
        /// </summary>
        private bool m_Removed;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Initializes the description, units and the upper and lower limits, in engineering units, of the Y axis.
        /// </summary>
        /// <param name="oldIdentifier">The old identifier of the scalar watch variable that is to be plotted.</param>
        /// <param name="upperDisplayLimit">The upper limit, in engineering units, of the Y axis.</param>
        /// <param name="lowerDisplayLimit">The lower limit, in engineering units, of the Y axis.</param>
        /// <exception cref="InvalidOperationException">Thrown if the specified watch variable has not been defined in the data dictionary.</exception>
        public PlotterScalar(short oldIdentifier, float upperDisplayLimit, float lowerDisplayLimit) : this()
        {
            WatchVariable watchVariable;
            try
            {
                watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];

                // Check whether the watch variable exists.
                if (watchVariable == null)
                {
                    throw new ArgumentException(Resources.MBTWatchVariableNotDefined, "oldIdentifier");
                }

                Debug.Assert(watchVariable.VariableType == VariableType.Scalar, "PlotterScalar.Ctor() - [watchVariable.VariableType == VariableType.Scalar]");
            }
            catch (Exception)
            {
                throw new ArgumentException(Resources.MBTWatchVariableNotDefined, "oldIdentifier");
            }

            Identifier = oldIdentifier;
            Description = watchVariable.Name;
            Units = watchVariable.Units;

            #region - [Channel Configuration] -
            m_Plotter.AnalogueType = AnalogueType.SingleAnalogue;
            Channels[0].Enabled = true;
            Channels[0].YAxisName = watchVariable.Units;
            Channels[0].Enabled = true;
            Channels[0].ChannelColor = Color.ForestGreen;
            Channels[0].MaximumValue = upperDisplayLimit;
            Channels[0].MinimumValue = lowerDisplayLimit;
            #endregion - [Channel Configuration] -
        }

        /// <summary>
        /// Initializes a new instance of the UserControl.
        /// </summary>
        public PlotterScalar()
        {
            InitializeComponent();

            // Initialize the ContextMenuStrip control.
            ContextMenuStrip = m_Plotter.ContextMenuStrip;
            ContextMenuStrip.Items.Insert(0, m_ToolStripSeparatorPlotLayoutEnd);
            ContextMenuStrip.Items.Insert(0, m_ToolStripMenuItemRemoveSelected);
            ContextMenuStrip.Items.Insert(0, m_ToolStripMenuItemShowDefinition);

            ContextMenuStrip.Opened +=new EventHandler(ContextMenuStrip_Opened);
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the user control.
        /// </summary>
        /// <param name="disposing">True, to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Cleanup(bool disposing)
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

                    // Run the dispose methods on the attached Plot and ContextMenuStrip objects.
                    if (this.Plot != null)
                    {
                        this.Plot.Dispose();
                    }

                    if (this.ContextMenuStrip != null)
                    {
                        this.ContextMenuStrip.Dispose();
                        ContextMenuStrip = null;
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data 
                // members to null.
                #region - [Detach the event handler methods.] -
                this.ContextMenuStrip.Opened -= new EventHandler(ContextMenuStrip_Opened);
                this.m_ToolStripMenuItemRemoveSelected.Click -= new System.EventHandler(this.m_ToolStripMenuItemRemoveSelected_Click);
                this.m_ToolStripMenuItemShowDefinition.Click -= new System.EventHandler(this.m_ToolStripMenuItemShowDefinition_Click);
                this.m_Plotter.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.m_Plotter_MouseDown);
                #endregion - [Detach the event handler methods.] -

                #region - [Component Designer Variables] -
                this.components = null;
                this.m_LabelDescription = null;
                this.m_LabelUnits = null;
                this.m_ButtonYAxisPlus = null;
                this.m_ButtonYAxisMinus = null;
                this.m_ContextMenuStripPlotLayout = null;
                this.m_ToolStripSeparatorPlotLayoutBegin = null;
                this.m_ToolStripMenuItemShowDefinition = null;
                this.m_ToolStripMenuItemRemoveSelected = null;
                this.m_ToolStripSeparatorPlotLayoutEnd = null;
                this.m_Plotter = null;
                #endregion - [Component Designer Variables] -
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that we don't raise an exception.
            }
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        /// <summary>
        /// Event handler for 'Remove' context menu option. Remove all of the selected controls from the plot display.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemRemoveSelected_Click(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            IPlotterWatch plotterAsIPlotterWatch;
            for (int selectedIndex = 0; selectedIndex < Plotter.SelectedControlList.Count; selectedIndex++)
            {
                plotterAsIPlotterWatch = Plotter.SelectedControlList[selectedIndex] as IPlotterWatch;
                if (plotterAsIPlotterWatch != null)
                {
                    plotterAsIPlotterWatch.SetHighlight(false);
                    plotterAsIPlotterWatch.Removed = true;
                    plotterAsIPlotterWatch.Visible = false;
                }
            }

            Plotter.SelectedControlList.Clear();
        }

        /// <summary>
        /// Event handler for the 'Show Definition' context menu. Show the diagnostic help information associated with the control.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemShowDefinition_Click(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ShowHelpPopup();
        }

        /// <summary>
        /// Event handler for the <c>MouseDown</c> event associated with the <c>Plotter</c> control. Check the state of the control key and if this is pressed, add or 
        /// remove the control from the list of selected controls, depending upon whether the control is already in the list of selected controls. If the control 
        /// key is clear then clear the existing list and add the control to the new list.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_Plotter_MouseDown(object sender, MouseEventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if this was raised by any button other than the left mouse button e.g. context menu button etc.
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }

            if (ModifierKeys == Keys.Control)
            {
                // Check whether the user control is in the list of selected controls.
                if (Plotter.SelectedControlList.Contains(this) == true)
                {
                    // Yes - Remove the control from the list.
                    Plotter.SelectedControlList.Remove(this);
                    SetHighlight(false);
                }
                else
                {
                    // No - Add the control to the list.
                    Plotter.SelectedControlList.Add(this);
                    SetHighlight(true);
                }
            }
            else
            {
                // Clear the previously selected user controls.
                foreach (object plotter in Plotter.SelectedControlList)
                {
                    IPlotterWatch plotterAsIPlotterWatch = plotter as IPlotterWatch;
                    if (plotterAsIPlotterWatch != null)
                    {
                        plotterAsIPlotterWatch.SetHighlight(false);
                    }
                }

                Plotter.SelectedControlList.Clear();

                // If the user control is not in the list of selected controls, add the user control to the list.
                Plotter.SelectedControlList.Add(this);
                SetHighlight(true);
            }
        }

        /// <summary>
        /// Event handler for the context menu Opened event. Check whether multiple controls are selected and, if so, ensure that only the 'Remove Selected Plot(s)' 
        /// context menu option is enabled.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void ContextMenuStrip_Opened(object sender, EventArgs e)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (Plotter.SelectedControlList.Count > 1)
            {
                // Disable all context menu options.
                foreach (ToolStripItem toolStripItem in ContextMenuStrip.Items)
                {
                    toolStripItem.Enabled = false;
                }

                // Now enable the 'Remove Selected Plot(s)' context menu option.
                ContextMenuStrip.Items[KeyToolStripMenuItemRemoveSelected].Enabled = true;
            }
            else
            {
                // Enable all context menu options.
                foreach (ToolStripItem toolStripItem in ContextMenuStrip.Items)
                {
                    toolStripItem.Enabled = true;
                }
            }
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Start plotting.
        /// </summary>
        public void Start()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_Plotter.Start();
        }

        /// <summary>
		/// Stop plotting. The user can now view the graphs
		/// </summary>
        public void Stop()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_Plotter.Stop();
        }

        /// <summary>
        /// Update the graph display. Call this method once all the data values have been set so that the changes are displayed on the graph.
        /// </summary>		
        public void UpdateDisplay()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_Plotter.UpdateDisplay();
        }

        /// <summary>
        /// Reset the <c>Plotter</c> user control. Erases the graph and gets ready to start the whole plotting process again.
        /// </summary>
        public void Reset()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_Plotter.Reset();
        }

        /// <summary>
        /// Set the <c>UserControl</c> to the specified highlighted state.
        /// </summary>
        /// <param name="value">True, to highlight the <c>UserControl</c>; otherwise, false.</param>
        public void SetHighlight(bool value)
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (value == true)
            {
                m_LabelDescription.BackColor = Color.FromKnownColor(KnownColor.Highlight);
                m_LabelDescription.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
                m_LabelUnits.BackColor = Color.FromKnownColor(KnownColor.Highlight);
                m_LabelUnits.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
            }
            else
            {
                m_LabelDescription.BackColor = Color.FromKnownColor(KnownColor.Transparent);
                m_LabelDescription.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                m_LabelUnits.BackColor = Color.FromKnownColor(KnownColor.Transparent);
                m_LabelUnits.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            }
        }

        /// <summary>
        /// Show the watch variable definition using the Windows help pop-up.
        /// </summary>
        protected void ShowHelpPopup()
        {
            // Skip if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Get the help index associated with the watch variable.
            int helpIndex;
            try
            {
                helpIndex = Lookup.WatchVariableTableByOldIdentifier.Items[Identifier].HelpIndex;
            }
            catch (Exception)
            {
                helpIndex = CommonConstants.NotFound;
            }

            // If the help index exists, show the help topic associated with the index.
            if (helpIndex != CommonConstants.NotFound)
            {
                WinHlp32.ShowPopup(this.Handle.ToInt32(), helpIndex);
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the flag which indicates whether the Dispose() method has been called. True, indicates that the Dispose() method has been called; otherwise, 
        /// false.
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

        /// <summary>
        /// Gets the reference to <c>Plotter</c> user control; enables the programmer to access the <c>Plotter</c> properties and events.
        /// </summary>
        [
        Browsable(false)
        ]
        public Plotter Plot
        {
            get { return m_Plotter; }
        }

        /// <summary>
        /// Gets or sets the start time of the plot.
        /// </summary>
        [
        Browsable(false)
        ]
        public DateTime StartTime
        {
            get { return m_Plotter.StartTime; }
            set { m_Plotter.StartTime = value; }
        }

        /// <summary>
        /// Gets or sets the time of the actal trip.
        /// </summary>
        /// <remarks>Will display a vertical line at the time of the actual trip.</remarks>
        [
        Browsable(false)
        ]
        public DateTime TripTime
        {
            get { return m_Plotter.TripTime; }
            set { m_Plotter.TripTime = value; }
        }

        /// <summary>
        /// Gets or sets the flag used to indicate that the data corresponds to a fault log.
        /// </summary>
        /// <remarks>If the data corresponds to a fault log a vertical line will be drawn at the time specified by the <c>TripTime</c> property.</remarks>
        public bool IsFaultLog
        {
            get { return m_Plotter.IsFaultLog; }
            set { m_Plotter.IsFaultLog = value; }
        }

        /// <summary>
        /// Gets or sets the time span of the X axis.
        /// </summary>
        [
        Browsable(false)
        ]
        public TimeSpan XRange
        {
            get { return m_Plotter.XRange; }
            set { m_Plotter.XRange = value; }
        }

        /// <summary>
        /// Gets or sets the display style of the time axis.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The display style of the time axis."),
        ]
        public TimeAxisStyle TimeDisplayStyle
        {
            get { return m_Plotter.TimeDisplayStyle; }
            set { m_Plotter.TimeDisplayStyle = value; }
        }

        /// <summary>
        /// Gets or sets the time interval, in ms, between successive plots. Used when displaying live data.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Design"),
        Description("The time interval, in ms, between successive plots. Used when displaying live data."),
        ]
        public int PlotIntervalMs
        {
            get { return m_Plotter.PlotIntervalMs; }
            set { m_Plotter.PlotIntervalMs = value; }
        }

        /// <summary>
        /// Gets or sets the description text associated with the watch variable.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Design"),
        Description("The description text associated with the IO point being plotted."),
        ]
        public string Description
        {
            get { return m_Description; }
            set 
            { 
                m_Description = value;
                m_LabelDescription.Text = m_Description;
            }
        }

        /// <summary>
        /// Gets or sets the engineering units associated with the watch variable.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Design"),
        Description("The engineering units associated with the IO point."),
        ]
        public string Units
        {
            get { return m_Units; }
            set 
            { 
                m_Units = value;
                m_LabelUnits.Text = m_Units;
            }
        }

        /// <summary>
        /// Gets or sets the watch variable identifier associated with the control.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Design"),
        Description("The watch variable identifier associated with the control."),
        ]
        public short Identifier
        {
            get { return m_Identifier; }
            set { m_Identifier = value; }
        }

        /// <summary>
        /// Gets the channel collection associated with the plot.
        /// </summary>
        [
        Browsable(false),
        ]
        public ChannelCollection Channels
        {
            get { return m_Plotter.Channels; }
        }

        /// <summary>
        /// Gets or sets the size of the control, in pixels.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The size of the control, in pixels.")
        ]
        public new Size Size
        {
            get { return base.Size; }
            set
            {
                // Work out the offset for the Y location of the limit changing (plus) button.
                int YOffsetLimitChangingPlus = m_Plotter.Height - m_ButtonYAxisPlus.Location.Y;

                // Adjust the size of the plotter to the specified size and adjust the location of the Units label.
                m_Plotter.Size = value;
                m_LabelUnits.Location = new Point(value.Width - m_LabelUnits.Width, m_LabelUnits.Location.Y);

                // Adjust the width of the Description label.
                m_LabelDescription.Size = new Size(m_LabelUnits.Location.X - m_LabelDescription.Location.X, m_LabelDescription.Height);

                // Get the width and height of the limit changing buttons.
                int buttonHeight = m_ButtonYAxisMinus.Height;
                int buttonWidth = m_ButtonYAxisMinus.Width;

                // Position the limit changing buttons.
                m_ButtonYAxisMinus.Location = new Point(value.Width - ButtonXBorder, m_ButtonYAxisMinus.Location.Y);
                m_ButtonYAxisPlus.Location = new Point(value.Width - ButtonXBorder, value.Height - YOffsetLimitChangingPlus);

                // Adjust the size of the UserControl.
                base.Size = new Size(m_Plotter.Size.Width + buttonWidth, m_Plotter.Size.Height);
            }
        }

        /// <summary>
        /// Gets or sets the flag that indicates whether the user has removed this plot from the display.
        /// </summary>
        [
        Browsable(false),
        ]
        public bool Removed
        {
            get { return m_Removed; }
            set { m_Removed = value; }
        }

        /// <summary>
        /// Gets the ToolStripMenuItem control associated with the 'Remove Selected Plot(s)' context menu option.
        /// </summary>
        [
        Browsable(false)
        ]
        public ToolStripMenuItem RemoveSelectedPlot
        {
            get { return m_ToolStripMenuItemRemoveSelected; }
        }
        #endregion --- Properties ---
    }
}
