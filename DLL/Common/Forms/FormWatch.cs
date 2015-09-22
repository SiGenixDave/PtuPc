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
 *  File name:  FormWatch.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/10/10    1.0     K.McDonald      First Release.
 * 
 *  08/20/10    1.1     K.McD           1.  Included support for a data update event, raised whenever the data on display is updated.
 *                                      2.  Included a number of #if STUB statements to support Aug sprint review.
 * 
 *  08/20/10    1.2     K.McD           1.  Include support for the BITMASK_DEMO conditional compilation symbol.
 * 
 *  11/16/10    1.3     K.McD           1.  Removed the Exit() method.
 * 
 *  11/17/10    1.4     K.McD           1.  Modified the ConfigureDisplayPanel method() so that the array sizes were specified using the Length of the Column 
 *                                          property of the workset rather than a constant.
 * 
 *                                      2.  Modified the DisposeOfUserControls() method so that the upper limit of the outer for() loop was specified using the length
 *                                          of the watch control array rather than a constant.
 * 
 *  11/18/10    1.5     K.McD           1.  Modified the UpdateWatchControlValues() method so that the upper limit of the outer for() loop was specified using the length
 *                                          of the watch control array rather than a constant.
 * 
 *  02/14/11    1.6     K.McD           1.  Added a watch variable to store the currently slected workset.
 * 
 *  03/28/11    1.7     K.McD           1.  Modified the ConfigureDisplayPanel() method to use the Hide() and Show() method of the Panel control to improve the display.
 *                                      2.  Renamed a number of local variables. 
 *                                      3.  Modified the UpdateWatchControlValues() method to use the old identifier field od the watch variables.
 *                                      
 *  05/23/11    1.8     K.McD           1.  Corrected an XML tag.
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

using Common;
using Common.Configuration;
using Common.UserControls;

namespace Common.Forms
{
    /// <summary>
    /// Form to view and record the watch variables defined by the <c>WorksetManager</c> class.
    /// </summary>
    public partial class FormWatch : FormPTU, IDataUpdate
    {
        #region --- Events ---
        /// <summary>
        /// Occurs whenever the data is updated.
        /// </summary>
        public event EventHandler DataUpdate;
        #endregion --- Events ---

        #region --- Constants ---
        /// <summary>
        /// The type name associated with the <c>BitmaskControl</c> user control.
        /// </summary>
        protected const string KeyBitmaskControlTypeName = "BitmaskControl";
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The current workset;
        /// </summary>
        protected Workset_t m_Workset;

        /// <summary>
        /// A jagged array of watch control user controls. Contains the watch controls associated with each column of every workset.
        /// </summary>
        protected WatchControl[][] m_WatchControls;

        /// <summary>
        /// Reference to the class which supports the configuration, drawing and layout of multiple <c>WatchControl</c> derived user controls.
        /// </summary>
        protected WatchControlLayout m_WatchControlLayout;

        /// <summary>
        /// Size related parameters associated with <c>WatchControl</c> derived user controls.
        /// </summary>
        protected VariableControlSize_t m_WatchControlSize;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor, required for Visual Studio.
        /// </summary>
        public FormWatch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form to view the watch variables in real time.
        /// </summary>
        public FormWatch(Workset_t workset) 
        {
            InitializeComponent();

            m_Workset = workset;

            #region - [WatchControlSize Definitions] -
            m_WatchControlSize = new VariableControlSize_t();
            m_WatchControlSize.Margin.Left = WatchControlLayout.MarginLeftWatchControl;
            m_WatchControlSize.Margin.Right = WatchControlLayout.MarginRightWatchControl;
            m_WatchControlSize.Margin.Top = WatchControlLayout.MarginTopWatchControl;
            m_WatchControlSize.Margin.Bottom = WatchControlLayout.MarginBottomWatchControl;
            m_WatchControlSize.WidthVariableNameField = WatchControlLayout.WidthWatchControlVariableNameField;
            m_WatchControlSize.WidthValueField = WatchControlLayout.WidthWatchControlValueField;
            m_WatchControlSize.WidthUnitsField = WatchControlLayout.WidthWatchControlUnitsField;
            m_WatchControlSize.Height = WatchControlLayout.HeightWatchControl;
            #endregion - [WatchControlSize Definitions] -

            // Instantiate the class that helps manage the user controls.
            m_WatchControlLayout = new WatchControlLayout(this);

            ConfigureDisplayPanel(m_Workset, m_TabPage1, m_WatchControlSize);
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
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data 
                // members to null.
                DisposeOfUserControls();

                m_WatchControlLayout = null;
                m_WatchControls = null;
               
                #region - [Detach the event handler methods.] -
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
        #region - [TabControl Events] -
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
        #endregion - [TabControl Events] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Configure the watch user controls required to display the specified <paramref name="workset"/> and then add these to the specified 
        /// <paramref name="displayPanel"/>.
        /// </summary>
        /// <param name="workset">The workset that is to be used to configure the watch user controls.</param>
        /// <param name="displayPanel">The <c>Panel</c> to which the configured watch user controls are to be added.</param>
        /// <param name="watchControlSize">The size to make each watch user control.</param>
        /// <remarks>This method uses the <c>UserControl</c> class to: (a) layout and initialize all of the controls required to display the watch variables 
        /// associated with the specified worset and (b) add these to the tabpage/panel associated with the display.
        /// </remarks>
        protected void ConfigureDisplayPanel(Workset_t workset, Panel displayPanel, VariableControlSize_t watchControlSize)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Clear the panel to which the controls are to be added.
            displayPanel.Hide();
            displayPanel.Controls.Clear();

            // Instantiate the watch control jagged array.
            m_WatchControls = new WatchControl[workset.Column.Length][];
            for (int columnIndex = 0; columnIndex < workset.Column.Length; columnIndex++)
            {
                m_WatchControls[columnIndex] = new WatchControl[workset.Column[columnIndex].OldIdentifierList.Count];
            }

            // Create a separate panel for each display column.
            Panel[] panelColumn = new Panel[workset.Column.Length];
            for (int columnIndex = 0; columnIndex < workset.Column.Length; columnIndex++)
            {
                panelColumn[columnIndex] = new Panel();
                panelColumn[columnIndex].AutoSize = true;
                panelColumn[columnIndex].Location = new Point(columnIndex * (watchControlSize.Size.Width + watchControlSize.Margin.Horizontal), 0);
                m_WatchControlLayout.WriteColumnHeaders(workset.Column[columnIndex].HeaderText, panelColumn[columnIndex], watchControlSize);
                m_WatchControlLayout.ConfigureWatchControls(m_WatchControls[columnIndex], panelColumn[columnIndex], watchControlSize,
                                                            workset.Column[columnIndex].OldIdentifierList);
                displayPanel.Controls.Add(panelColumn[columnIndex]);
            }
            displayPanel.Show();
        }

        /// <summary>
        /// This method updates the Value property for each of the watch controls associated with the selected workset with the latest values retrieved from the 
        /// target hardware.
        /// </summary>
        protected void UpdateWatchControlValues()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            int oldIdentifier;
            WatchVariable watchVariable;
            for (int column = 0; column < m_WatchControls.Length; column++)
            {
                for (int index = 0; index < m_WatchControls[column].Length; index++)
                {
                    oldIdentifier = m_WatchControls[column][index].Identifier;
                    try
                    {
                        watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];

                        // If the watch variable is not defined, set the value to be NaN i.e. 'Not a Number'.
                        if (watchVariable == null)
                        {
                            m_WatchControls[column][index].Value = double.NaN;
                        }
                        else
                        {
                            m_WatchControls[column][index].Value = Lookup.WatchVariableTable.Items[watchVariable.Identifier].ValueFromTarget;
                        }
                    }
                    catch (Exception)
                    {
                        m_WatchControls[column][index].Value = double.NaN;
                    }
                }
            }

            OnDataUpdate(this, new EventArgs());
        }

        /// <summary>
        /// Dispose of the user control associated with each element of the user control jagged array.
        /// </summary>
        private void DisposeOfUserControls()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_WatchControls != null)
            {
                for (int columnIndex = 0; columnIndex < m_WatchControls.Length; columnIndex++)
                {
                    for (int index = 0; index < m_WatchControls[columnIndex].Length; index++)
                    {
                        if (m_WatchControls[columnIndex][index] != null)
                        {
                            m_WatchControls[columnIndex][index].Dispose();
                            m_WatchControls[columnIndex][index] = null;
                        }
                    }
                    m_WatchControls[columnIndex] = null;
                }
            }
            m_WatchControls = null;
        }

        #region - [Event Management] -
        /// <summary>
        /// Raise a <c>DataUpdate</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="eventArgs">Parameter passed from the object that raised the event.</param>
        protected void OnDataUpdate(object sender, EventArgs eventArgs)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (DataUpdate != null)
            {
                DataUpdate(sender, eventArgs);
            }
        }
        #endregion - [Event Management] -
        #endregion --- Methods ---
    }
}