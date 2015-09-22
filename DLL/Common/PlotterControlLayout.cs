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
 *  File name:  PlotterControlLayout.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McDonald      First Release.
 * 
 *  08/19/10    1.1     K.McD           1.  Included support for plotting individual flags/bits associated with bitmask watch variables.
 *                                      2.  Modified design to use IPlotterWatch, IPlotterBitmask and IPlotterScalar interface definitions.
 * 
 *  08/20/10    1.2     K.McD           1.  Include support for the BITMASK_DEMO conditional compilation symbol.
 * 
 *  08/27/10    1.3     K.McD           1.  Modified SetBreakPoints() method to round the breakpoint time down to the nearest 10ms.
 * 
 *  10/06/10    1.4     K.McD           1.  Renamed a number of variables and methods for consistency.
 *                                      2.  Replaced a number of absolute numeric values with constants.
 * 
 *  11/19/10    1.5     K.McD           1.  Modified the PlotWatchValues() method so that a check is made that the watch element index is less than or equal to
 *                                          the CountMax property of the workset associated with the historic data manager class.
 * 
 *  01/19/11    1.6     K.McD           1.  Corrected the exception text in the method PlotWatchValues().
 * 
 *  01/26/11    1.7     K.McD           1.  Auto-modified as a result of the property name changes associated with the Parameter class.
 * 
 *  03/18/11    1.8     K.McD           1.  Modified a number of XML tags and the names of a number of parameters and local variables.
 *                                      2.  Included a try/catch block in the ConstructPlotterControl() method.
 *                                      3.  Included a try/catch block in the ConstructLogicAnalyzerControl() method.
 *                                      4.  Modified the PlotWatchValues() method to use the old identifier field of the watch variable.
 *                                      5.  Removed support for the BITMASK_DEMO conditional compilation symbol.
 * 
 *  03/31/11    1.9     K.McD           1.  Added support for the PlotterEnumerator user control.
 *                                      2.  Modified a number of XML tags and comments.
 *                                      3.  Renamed a number of parameters.
 *
 *  10/01/11    1.10    K.McD           1.  PlotWatchValues(). Corrected the implementation of the check to determine if the bit associated with 
 *                                          the PlotterBitmask control is asserted.
 *
 *	10/05/11	1.11	Sean.D			1.	Add the m_listPlotters and m_MultiCursor member variables as well as public property MultiCursor.
 *										2.	Added the OnPlotMouseMoved function to handle updating the MouseHoverCoordinates for all of the plots
 *											on the current display.
 *										3.	Modified ConstructPlotterScalar, ConstructPlotterEnumerator, and ConstructLogicAnalyzerControl to
 *											store a reference in m_listPlotters and to set their MouseMove event.
 *											
 *  10/07/11    1.12    K.McD           1.  Reverted back to revision 1.10 as the changes implemented in revision 1.11 are not required for the current 
 *                                          multiple cursor implementation.
 *                                          
 *                                      2.  Added the constants that define the height for the scalar, enumerator and bitmask plotter controls.
 *                                      
 *  11/07/11    1.12.1  Sean.D          1.  Added check in ContextMenu_Opened for object type to prevent casting errors if a plot is selected and the user right-clicks
 *											on the title.
 *											
 *  12/01/11    1.12.2  K.McD           1.  Added constants for the offset values required to access the Size, Font and BackColor properties from the parameter object
 *                                          array passed to the SetAestheticProperties() method.
 *                                          
 *                                      2.  Added check to skip the method logic if the plotterWatch parameter was null in the: SetAestheticProperties(), 
 *                                          SetRangeProperties() and Reset() methods.
 *                                          
 *                                      3.  Added check to the SetAestheticProperties() method to skip the method logic if the length of the parameter array was 
 *                                          invalid.
 *                                          
 *  05/12/15    1.14    K.McD           References
 *                                      1.  SNCR - R188 PTU [20 Mar 2015] Item 17. If the user uses the ‘Remove Selected Plot(s)’ context menu option to remove one or more
 *                                          plots from the 'File/Open/Watch File', 'File/Open/Data Stream', or 'File/Open/Simulated Data Stream' screen, the
 *                                          ‘Modified Layout’ status message is not displayed until the screen is closed and then re-loaded from disk.
 *                                      
 *                                      Modifications
 *                                      1.  Added the RemoveSelected event and corresponding OnRemoveSelected() method.
 *                                      2.  Modified the ConstructPlotterScalar(), ConstructPlotterEnumerator() and ConstructLogicAnalyzerControl() methods to
 *                                          attach the OnRemoveSelected() method/handler to the Click event of the RemoveSelectedPlot context menu of each Plotter
 *                                          derived user control.
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;

using CodeProject.GraphComponents;
using Common.UserControls;
using Common.Configuration;
using Common.Communication;
using Common.Properties;

namespace Common
{
    /// <summary>
    /// A class to support configuration, drawing and layout of multiple <c>PlotterControl</c> derived user controls.
    /// </summary>
    public class PlotterControlLayout
    {
        #region --- Events ---
        /// <summary>
        /// Occurs when the plotter range has been changed.
        /// </summary>
        public event EventHandler RangeChanged;

        /// <summary>
        /// Occurs when the plotter range has been reset.
        /// </summary>
        public event EventHandler RangeReset;

        /// <summary>
        /// Occurs when the 'Remove Selected Plot(s)' context menu is selected.
        /// </summary>
        public event EventHandler RemoveSelected;

        /// <summary>
        /// Occurs when the 'Zoom' context menu option is selected.
        /// </summary>
        public event EventHandler ZoomSelected;
        #endregion --- Events ---

        #region --- Constants ---
        #region - [Parameter Array Offsets] -
        /// <summary>
        /// Offset into the parameter object array corresponding to the Size parameter of the Scalar user control. Value: 0.
        /// </summary>
        private const int OffsetSizeScalar = 0;

        /// <summary>
        /// Offset into the parameter object array corresponding to the Size parameter of the Enumerator user control. Value: 1.
        /// </summary>
        private const int OffsetSizeEnumerator = 1;

        /// <summary>
        /// Offset into the parameter object array corresponding to the Size parameter of the Bitmask user control. Value: 2.
        /// </summary>
        private const int OffsetSizeBitmask = 2;

        /// <summary>
        /// Offset into the parameter object array corresponding to the Font parameter. Value: 3.
        /// </summary>
        private const int OffsetFont = 3;

        /// <summary>
        /// Offset into the parameter object array corresponding to the BackColor parameter. Value: 4.
        /// </summary>
        private const int OffsetBackColor = 4;
        #endregion - [Parameter Array Offsets] -

        /// <summary>
        /// The format string, converted to lower case, used in the data dictionary to represent a general number. Value: "general number".
        /// </summary>
        protected const string FormatStringFieldGeneralNumber = "general number";

        /// <summary>
        /// The format string, converted to lower case, used in the data dictionary to represent a hexadecimal number. Value: "hexadecimal".
        /// </summary>
        protected const string FormatStringFieldHexadecimal = "hexadecimal";

        /// <summary>
        /// The type name associated with the <c>LogicAnalyzerControl</c> user control. Value: "LogicAnalyzerControl".
        /// </summary>
        public const string KeyLogicAnalyzerControlTypeName = "LogicAnalyzerControl";

        /// <summary>
        /// The type name associated with the <c>PlotterControl</c> user control. Value: "PlotterControl".
        /// </summary>
        public const string KeyPlotterControlTypeName = "PlotterControl";

        /// <summary>
        /// The key associated with the 'Reset Range' context menu option. Value: "m_ToolStripMenuItemResetRange".
        /// </summary>
        private const string KeyMenuItemResetRange = "m_ToolStripMenuItemResetRange";

        /// <summary>
        /// The key associated with the 'Zoom' context menu option. Value: "m_ToolStripMenuItemZoom".
        /// </summary>
        private const string KeyMenuItemZoom = "m_ToolStripMenuItemZoom";

        /// <summary>
        /// The interval, as a multiple of the <c>DataInterval</c>, used to trigger a new breakpoint in the plot. Value: 5.
        /// </summary>
        private const int BreakpointTriggerIntervalAsMultiple = 5;

        /// <summary>
        /// The resolution, in ms, to be applied to breakpoint values. Value: 10.
        /// </summary>
        private const int BreakpointResolutionMs = 10;

        #region - [Graph Graduations] -
        /// <summary>
        /// The default number of graduations on the X axis. Value: 10.
        /// </summary>
        public const int DefaultGraduationsX = 10;

        /// <summary>
        /// The default number of graduations on the Y axis. Value: 5.
        /// </summary>
        public const int DefaultGraduationsY = 5;
        #endregion - [Graph Graduations] -

        #region - [Heights] -
        /// <summary>
        /// The height, in pixels, of the plotter user control. Value: 120.
        /// </summary>
        public const int HeightPlotterControl = 120;

        /// <summary>
        /// The height, in pixels, of the logic analyzer user control. Value: 70.
        /// </summary>
        public const int HeightLogicAnalyzerControl = 70;

        /// <summary>
        /// The height, in pixels, of the enumerator plotter user control. Value: 70.
        /// </summary>
        public const int HeightEnumeratorPlotterControl = 70;
        #endregion - [Heights] -

        #region - [Margins] -
        /// <summary>
        /// The left margin to be applied to <see>PlotterControl</see> derived user controls. Value: 30.
        /// </summary>
        public const int MarginLeftUserControl = 30;

        /// <summary>
        /// The right margin to be applied to <see>PlotterControl</see> derived user controls. Value: 30.
        /// </summary>
        public const int MarginRightUserControl = 30;

        /// <summary>
        /// The top margin to be applied to <see>PlotterControl</see> derived user controls. Value: CommonConstants.NotUsed.
        /// </summary>
        public const int MarginTopUserControl = CommonConstants.NotUsed;

        /// <summary>
        /// The bottom margin to be applied to <see>PlotterControl</see> derived user controls. Value: CommonConstants.NotUsed.
        /// </summary>
        public const int MarginBottomUserControl = CommonConstants.NotUsed;
        #endregion - [Margins] -
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Flag to indicate whether the class has been disposed of. True, indicates that the class has already been disposed of; otherwise, false.
        /// </summary>
        protected bool m_IsDisposed;

        /// <summary>
        /// Reference to the <c>HistoricDataManager</c> class, this supports the displaying of historic data and allows the time range to be zoomed in and out.
        /// </summary>
        protected IHistoricDataManager m_HistoricDataManager;

        /// <summary>
        /// Reference to the <c>Form</c> which instantiated this class.
        /// </summary>
        private Form m_Form;

        /// <summary>
        /// The tab index to be assigned to the user control.
        /// </summary>
        private static int m_TabIndex = 0;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="form">Reference to the form which instantiated the class.</param>
        public PlotterControlLayout(Form form)
        {
            m_Form = form;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="form">Reference to the form which instantiated the class.</param>
        /// <param name="historicDataManager">Reference to the <c>HistoricDataManager</c> object associated with the calling form.</param>
        public PlotterControlLayout(Form form, IHistoricDataManager historicDataManager) : this(form)
        {
            m_HistoricDataManager = historicDataManager;
        }
        #endregion --- Constructors ---

        #region --- Delegated Methods ---
        /// <summary>
        /// Event handler for the context menu 'Opened' event. This method is called whenever the user right-clicks on any of the plotter user controls; if a select 
        /// new time span operation has been initiated on any of the user controls, the new context menu will be shown in the disabled state for all user controls 
        /// except the one that on which the 'select new time span operation' has been initiated.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void ContextMenu_Opened(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }                        

            // If a select new time span operation has been initiated on any of the components, disable the context menus associated with all but this component.
            if (PlotterRangeSelection.LastPlotterRangeSelectionState == PlotterRangeSelectionState.NewPlotterRangeSelected ||
                PlotterRangeSelection.LastPlotterRangeSelectionState == PlotterRangeSelectionState.StartTimeSelected ||
                PlotterRangeSelection.LastPlotterRangeSelectionState == PlotterRangeSelectionState.StopTimeSelected)
            {
				// If sender and SourceControl have the correct types, manipulate the ContextMenuStrip
				if (sender is ContextMenuStrip && ((ContextMenuStrip)sender).SourceControl is Plotter)
				{
					if (((Plotter)((ContextMenuStrip)sender).SourceControl).CurrentRangeSelection.PlotterRangeSelectionState == PlotterRangeSelectionState.InitialState)
					{
						// If the component which raised the event is still in the initial state, disable the context menu.
						((Plotter)((ContextMenuStrip)sender).SourceControl).ContextMenuStrip.Enabled = false;
					}
					else
					{
						((Plotter)((ContextMenuStrip)sender).SourceControl).ContextMenuStrip.Enabled = true;
					}
				}
            }
        }

        #region - [Event Management] -
        /// <summary>
        /// Raises a <c>RangeChanged</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="eventArgs">Parameter passed from the object that raised the event.</param>
        protected void OnRangeChanged(object sender, EventArgs eventArgs)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            if (RangeChanged != null)
            {
                RangeChanged(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises a <c>RangeReset</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="eventArgs">Parameter passed from the object that raised the event.</param>
        protected void OnRangeReset(object sender, EventArgs eventArgs)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            if (RangeReset != null)
            {
                RangeReset(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises a <c>RemoveSelected</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="eventArgs">Parameter passed from the object that raised the event.</param>
        protected void OnRemoveSelected(object sender, EventArgs eventArgs)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            if (RemoveSelected != null)
            {
                RemoveSelected(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises a <c>ZoomSelected</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="eventArgs">Parameter passed from the object that raised the event.</param>
        protected void OnZoomSelected(object sender, EventArgs eventArgs)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            if (ZoomSelected != null)
            {
                // Only initiate a zoom if valid start and stop times have been selected.
                if (PlotterRangeSelection.LastPlotterRangeSelectionState == PlotterRangeSelectionState.ZoomSelected)
                {
                    ZoomSelected(sender, eventArgs);
                }
            }
        }
        #endregion - [Event Management] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Instantiates and initializes a new <c>PlotterScalar</c> user control and then adds the event delegates to the control.
        /// </summary>
        /// <param name="oldIdentifier">The old identifier of the scalar watch variables that is to be plotted.</param>
        /// <param name="upperDisplayValue">The upper display limit of the plot.</param>
        /// <param name="lowerDisplayValue">The lower display limit of the plot.</param>
        /// <returns>The instantiated <c>PlotterControl</c> if the specified watch variable exists; otherwise, null.</returns>
        public PlotterScalar ConstructPlotterScalar(short oldIdentifier, double upperDisplayValue, double lowerDisplayValue)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return null;
            }

            PlotterScalar plotterControl;
            try
            {
                plotterControl = new PlotterScalar(oldIdentifier, (float)upperDisplayValue, (float)lowerDisplayValue);
            }
            catch (Exception)
            {
                return null;
            }

            #region - Events -
            plotterControl.Plot.StartTimeChanged += new EventHandler(OnRangeChanged);
            plotterControl.Plot.StopTimeChanged += new EventHandler(OnRangeChanged);
            plotterControl.ContextMenuStrip.Items[KeyMenuItemResetRange].Click += new EventHandler(OnRangeReset);
            plotterControl.ContextMenuStrip.Items[KeyMenuItemZoom].Click += new EventHandler(OnZoomSelected);
            plotterControl.ContextMenuStrip.Opened += new EventHandler(ContextMenu_Opened);
            plotterControl.RemoveSelectedPlot.Click += new EventHandler(OnRemoveSelected);
            #endregion - Events -

            // --------------------------------------------------------------------------------------------------------------------
            // Set the ValueFormat property of the control according to the value of the FORMATSTRING field of the data dictionary.
            // --------------------------------------------------------------------------------------------------------------------

            // Check that the specified watch variable exists.
            WatchVariable watchVariable;
            string valueFormat = string.Empty;
            try
            {
                watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                if (watchVariable == null)
                {
                    throw new ArgumentException(Resources.MBTWatchVariableNotDefined, "oldIdentifier");
                }

                if (watchVariable.FormatString.ToLower() == FormatStringFieldHexadecimal)
                {
                    valueFormat = Plotter.FormatStringHex;
                    plotterControl.Plot.ValueFormat = valueFormat;
                }
            }
            catch (Exception)
            {
                // Ensure that an exception isn't thrown, run with the default value for the ValueFormat field.
            }

            return plotterControl;
        }

        /// <summary>
        /// Instantiates and initializes a new <c>PlotterEnumerator</c> user control and then adds: (a) the event delegates and (b) a reference to the calling form, to 
        /// the control.
        /// </summary>
        /// <param name="oldIdentifier">The old identifier of the enumerator watch variables that is to be plotted.</param>
        /// <param name="upperDisplayValue">The upper display limit of the plot.</param>
        /// <param name="lowerDisplayValue">The lower display limit of the plot.</param>
        /// <returns>The instantiated <c>PlotterControl</c> if the specified watch variable exists; otherwise, null.</returns>
        public PlotterEnumerator ConstructPlotterEnumerator(short oldIdentifier, double upperDisplayValue, double lowerDisplayValue)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return null;
            }

            PlotterEnumerator plotterEnumerator;
            try
            {
                plotterEnumerator = new PlotterEnumerator(oldIdentifier, (float)upperDisplayValue, (float)lowerDisplayValue);
            }
            catch (Exception)
            {
                return null;
            }

            #region - Events -
            plotterEnumerator.Plot.StartTimeChanged += new EventHandler(OnRangeChanged);
            plotterEnumerator.Plot.StopTimeChanged += new EventHandler(OnRangeChanged);
            plotterEnumerator.ContextMenuStrip.Items[KeyMenuItemResetRange].Click += new EventHandler(OnRangeReset);
            plotterEnumerator.ContextMenuStrip.Items[KeyMenuItemZoom].Click += new EventHandler(OnZoomSelected);
            plotterEnumerator.ContextMenuStrip.Opened += new EventHandler(ContextMenu_Opened);
            plotterEnumerator.RemoveSelectedPlot.Click += new EventHandler(OnRemoveSelected);
            #endregion - Events -

            // Add a reference to the client form for the enumerator plotter control as the control accesses the MainWindow.Enumeration property to determine 
            // whether the actual value of the enumerator is to be displayed or the corresponding enumerated text.
            plotterEnumerator.ClientForm = m_Form;

            return plotterEnumerator;
        }

        /// <summary>
        /// Instantiates and initializes a new <c>PlotterBitmask</c> user control and then adds the event delegates to the control.
        /// </summary>
        /// <param name="oldIdentifier">The old identifier of the bitmask watch variable that is to be plotted.</param>
        /// <param name="bit">The bit of the bitmask that is to be plotted.</param>
        /// <returns>The instantiated <c>LogicAnalyzerControl</c> if the specified watch variable exists; otherwise, null.</returns>
        public PlotterBitmask ConstructLogicAnalyzerControl(short oldIdentifier, byte bit)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return null;
            }

            PlotterBitmask logicAnalyzerControl;
            try
            {
                logicAnalyzerControl = new PlotterBitmask(oldIdentifier, bit);
            }
            catch (Exception)
            {
                return null;
            }

            #region - Events -
            logicAnalyzerControl.Plot.StartTimeChanged += new EventHandler(OnRangeChanged);
            logicAnalyzerControl.Plot.StopTimeChanged += new EventHandler(OnRangeChanged);
            logicAnalyzerControl.ContextMenuStrip.Items[KeyMenuItemResetRange].Click += new EventHandler(OnRangeReset);
            logicAnalyzerControl.ContextMenuStrip.Items[KeyMenuItemZoom].Click += new EventHandler(OnZoomSelected);
            logicAnalyzerControl.ContextMenuStrip.Opened += new EventHandler(ContextMenu_Opened);
            logicAnalyzerControl.RemoveSelectedPlot.Click += new EventHandler(OnRemoveSelected);
            #endregion - Events -

            return logicAnalyzerControl;
        }

        /// <summary>
        /// Set the range properties of the specified user control to the values contained within the <c>PlotterRangeSelection</c> static structure.
        /// </summary>
        /// <param name="plotterWatch">The <c>IPlotterWatch</c> derived user control that is to have its range properties modified.</param>
        /// <param name="parameter">Not used, this should be set to null.</param>
        public void SetRangeProperties(IPlotterWatch plotterWatch, object parameter)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            if (plotterWatch == null)
            {
                return;
            }

            plotterWatch.StartTime = PlotterRangeSelection.StartTime;
            plotterWatch.TimeDisplayStyle = TimeAxisStyle.Absolute;
            plotterWatch.XRange = PlotterRangeSelection.TimeSpan;
            plotterWatch.Plot.DataIntervalMs = PlotterRangeSelection.DataIntervalMs;
            plotterWatch.Plot.GraduationsX = DefaultGraduationsX;
        }

        /// <summary>
        /// Set the <c>BackColor</c>, <c>Font</c> and <c>Size</c> properties of the specified user control to the values specified in the 
        /// <paramref name="parameter"/> object array.
        /// </summary>
        /// <param name="plotterWatch">The <c>IPlotterWatch</c> derived user control that is to have its range properties set.</param>
        /// <param name="parameter">The parameters, as an object array.</param>
        public void SetAestheticProperties(IPlotterWatch plotterWatch, object parameter)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            if (plotterWatch == null)
            {
                return;
            }

            object[] parameters = parameter as object[];
            if (parameters == null)
            {
                return;
            }

            if (parameters.Length < OffsetBackColor + 1)
            {
                return;
            }

            // Pop the parameter off the stack.
            Size plotterScalarSize = (Size)parameters[OffsetSizeScalar];
            Size plotterEnumeratorSize = (Size)parameters[OffsetSizeEnumerator];
            Size plotterBitmaskSize = (Size)parameters[OffsetSizeBitmask];
            Font font = (Font)parameters[OffsetFont] as Font;
            Color backColor = (Color)parameters[OffsetBackColor];

            plotterWatch.BackColor = backColor;
            plotterWatch.Font = font;

            // Update the Size property of the control.
            if ((plotterWatch as IPlotterScalar) != null)
            {
                plotterWatch.Size = plotterScalarSize;
            }
            else if ((plotterWatch as IPlotterEnumerator) != null)
            {
                plotterWatch.Size = plotterEnumeratorSize;
            }
            else if ((plotterWatch as IPlotterBitmask) != null)
            {
                plotterWatch.Size = plotterBitmaskSize;
            }
        }

        /// <summary>
        /// Call the <c>ResetPlotter</c> or <c>ResetLogicAnalyzer</c> method, as appropriate, on the <c>Plot</c> property associated with the user control. This clears 
        /// the graph display and channel lists so that the whole plotting process can be repeated.
        /// </summary>
        /// <param name="plotterWatch">The <c>IPlotterWatch</c> derived user control that is to have its range properties set.</param>
        /// <param name="parameter">The parameters, as an object array.</param>
        public void Reset(IPlotterWatch plotterWatch, object parameter)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            if (plotterWatch == null)
            {
                return;
            }

            ResetPlotter(plotterWatch.Plot);
        }

        /// <summary>
        /// Reset the specified plotter user control. Clears the graph display and channel lists so that the whole plotting process can be repeated.
        /// </summary>
        /// <remarks>The plotter user control must previously have been initialized prior to calling this method.</remarks>
        /// <param name="plotter">Reference to the <c>Plotter</c> user control that is to be reset.</param>
        public void ResetPlotter(Plotter plotter)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            // Update those properties affected by changes in the time span of the plot.
            plotter.StartTime = PlotterRangeSelection.StartTime;
            plotter.XRange = PlotterRangeSelection.TimeSpan;

            plotter.PlotIntervalMs = PlotterRangeSelection.PlotIntervalMs;

            // Erase the graph and initialize the channels to start the whole plotting process once again.
            plotter.Reset();
        }

        /// <summary>
        /// Get the elapsed times, in ms, between the RTC time of each frame of historic data and the Start Time of the plot.
        /// </summary>
        /// <param name="startTime">The start time of the plot.</param>
        /// <param name="historicDataManager">Reference to the historic data that is to be plotted.</param>
        /// <returns>An array containing the elapsed time, in ms, since the specified start time of the plot for each frame of historic data.</returns>
        public long[] GetElapsedTimes(DateTime startTime, IHistoricDataManager historicDataManager)
        {
            long[] elapsedTime = new long[historicDataManager.FramesToDisplay.Count];
            long timeFromStart = 0;

            // Round down to the nearest data interval.
            long adjust;
            for (int frame = 0; frame < m_HistoricDataManager.FramesToDisplay.Count; frame++)
            {
                timeFromStart = (long)m_HistoricDataManager.FramesToDisplay[frame].CurrentDateTime.Subtract(startTime).TotalMilliseconds;
                adjust = timeFromStart % 10;
                timeFromStart -= adjust;
                elapsedTime[frame] = (timeFromStart >= 0) ? timeFromStart : 0;
            }
            return elapsedTime;
        }

        /// <summary>
        /// Plot the historic watch values stored within <paramref name="historicDataManager"/> using the <c>IPlotterWatch</c> derived plotter user controls associated
        /// with the specified <c>TableLayoutControlCollection</c>.
        /// </summary>
        /// <param name="controlCollection">Reference to the <c>TableLayoutControlCollection</c> containing the <c>IPlotterWatch</c> derived plotter controls used 
        /// to plot the historic data.</param>
        /// <param name="historicDataManager">Reference to the <c>HistoricDataManager</c> class containing the data that is to be plotted.</param>
        /// <param name="elapsedTime">The elapsed time, in ms, since the start of the plot for each frame that is to be plotted.</param>
        public void PlotWatchValues(TableLayoutControlCollection controlCollection, IHistoricDataManager historicDataManager, long[] elapsedTime)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            // Skip, if there are no plotter controls associated with the TableLayoutPanel.
            if (controlCollection.Count == 0)
            {
                return;
            }

            short oldIdentifier, watchElementIndex;
            IPlotterWatch plotterWatch;
            WatchVariable watchVariable;
            for (int index = 0; index < controlCollection.Count; index++)
            {
                plotterWatch = controlCollection[index] as IPlotterWatch;

                // Skip, if the plotter user control has not been configured.
                if (plotterWatch == null)
                {
                    continue;
                }

                oldIdentifier = plotterWatch.Identifier;

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

                if (watchElementIndex > historicDataManager.Workset.CountMax)
                {
                    throw new ArgumentOutOfRangeException("watchElementIndex", "PlotterControlLayout.PlotWatchValues()");
                }

                plotterWatch.Start();

                if ((plotterWatch as IPlotterScalar) != null)
                {
                    double engineeringValue;
                    for (int frame = 0; frame < historicDataManager.FramesToDisplay.Count; frame++)
                    {
                        engineeringValue = watchVariable.ScaleFactor * historicDataManager.FramesToDisplay[frame].WatchElements[watchElementIndex].Value;
                        plotterWatch.Channels[0].SetYTValue((float)engineeringValue, elapsedTime[frame]);
                    }
                }
                else if ((plotterWatch as IPlotterEnumerator) != null)
                {
                    double engineeringValue;
                    for (int frame = 0; frame < historicDataManager.FramesToDisplay.Count; frame++)
                    {
                        engineeringValue = watchVariable.ScaleFactor * historicDataManager.FramesToDisplay[frame].WatchElements[watchElementIndex].Value;
                        plotterWatch.Channels[0].SetYTValue((float)engineeringValue, elapsedTime[frame]);
                    }
                }
                else if ((plotterWatch as IPlotterBitmask) != null)
                {
                    IPlotterBitmask plotterBitmask = plotterWatch as IPlotterBitmask;
                    byte bit = plotterBitmask.Bit;
                    ulong bitmask = (ulong)0x01 << bit;
                    bool logicState;
                    for (int frame = 0; frame < historicDataManager.FramesToDisplay.Count; frame++)
                    {
                        if (((ulong)historicDataManager.FramesToDisplay[frame].WatchElements[watchElementIndex].Value & bitmask) == bitmask)
                        {
                            logicState = true;
                        }
                        else
                        {
                            logicState = false;
                        }

                        plotterBitmask.Channels[0].SetState(logicState, elapsedTime[frame]);
                    }
                }
                plotterWatch.Stop();
                plotterWatch.UpdateDisplay();
            }
        }

        /// <summary>
        /// Reset the channel of each <c>IPlotterWatch</c> derived plotter control associated with the specified <c>TableLayoutControlCollection</c>.
        /// </summary>
        /// <param name="controlCollection">Reference to the <c>TableLayoutControlCollection</c> containing the <c>IPlotterWatch</c> derived user controls.</param>
        public void ResetChannel(TableLayoutControlCollection controlCollection)
        {
            for (int index = 0; index < controlCollection.Count; index++)
            {
                if ((controlCollection[index] as IPlotterWatch) != null)
                {
                    (controlCollection[index] as IPlotterWatch).Channels[0].Reset();
                }
            }
        }

        /// <summary>
        /// Check whether there were any breaks in communication with the VCU and set the breakpoint values of each plotter controls accordingly. This allows the plot 
        /// to be drawn using a transparent pen for periods where there was a break in transmission.
        /// </summary>
        /// <param name="controlCollection">Reference to the <c>TableLayoutControlCollection</c> containing the <c>IPlotterWatch</c> derived user controls.</param>
        /// <param name="startTime">The start time of the plot.</param>
        /// <param name="historicDataManager">Reference to the <c>HistoricDataManager</c> class containing the data that is to be plotted.</param>
        public void SetBreakpoints(TableLayoutControlCollection controlCollection, DateTime startTime, IHistoricDataManager historicDataManager)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            // Skip, if there are no plotter controls associated with the TableLayoutPanel.
            if (controlCollection.Count == 0)
            {
                return;
            }
            
            // The time, in ms since the start of the plot, where a break in the sequence was detected.
            long breakpoint;

            // The DateTime corresponding to the previous entry, used to determine a break in the log sequence for running logs.
            DateTime previousEntryDateTime = new DateTime();

            // The interval between concurrent frames above which the plot is considered to have been interrupted i.e a new breakpoint has been triggered.
            TimeSpan breakpointTriggerInterval = new TimeSpan(0, 0, 0, 0, BreakpointTriggerIntervalAsMultiple * Parameter.IntervalWatchMs);

            long adjust;
            for (int frame = 0; frame < m_HistoricDataManager.FramesToDisplay.Count; frame++)
            {
                // Don't perform comparison on the first entry.
                if (frame != 0)
                {
                    if (historicDataManager.FramesToDisplay[frame].CurrentDateTime.Subtract(previousEntryDateTime) > breakpointTriggerInterval)
                    {
                        // Detected a break in the log sequence, record the previous entry as a breakpoint, round down to the nearest 10ms.
                        breakpoint = (long)previousEntryDateTime.Subtract(startTime).TotalMilliseconds;
                        adjust = breakpoint % BreakpointResolutionMs;
                        breakpoint -= adjust;
                        
                        // Record the breakPoint for each plotter.
                        for (int index = 0; index < controlCollection.Count; index++)
                        {
                            // Skip, if the plotter user control has not been configured.
                            if ((controlCollection[index] as IPlotterWatch) == null)
                            {
                                continue;
                            }

                            (controlCollection[index] as IPlotterWatch).Channels[0].SetBreakPoint(breakpoint);
                        }
                    }
                }
                previousEntryDateTime = m_HistoricDataManager.FramesToDisplay[frame].CurrentDateTime;
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or set the tab index counter.
        /// </summary>
        public static int TabIndex
        {
            get { return m_TabIndex; }
            set { m_TabIndex = value; }
        }
        #endregion --- Properties ---
    }
}
