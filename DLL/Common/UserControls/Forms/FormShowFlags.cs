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
 *  File name:  FormShowFlags.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/20/10    1.1     K.McD           1.  Modified to show the states of ALL flags associated with the selected bitmask varable.
 *                                      2.  Modified to use the TableLayoutPanel control to layout the list of flags.
 *                                      3.  Modified to link the display update to an event raised by the client form whenever the data is updated.
 *                                      4.  Registers the form with the client form so that the Dispose() method of this form is called whenever the client form 
 *                                          is disposed of.
 * 
 *  08/20/10    1.2     K.McD           1.  Corrected XML tag.
 *                                      2.  Changed HeightAdjust constant from 30 to 25 px.
 * 
 *  08/30/10    1.3     K.McD           1.  SNCR PTU-001.13 - Changed the background color of the asserted state to yellow.
 * 
 *  09/29/10    1.4     K.McD           1.  Minor changes to variable names, method names and XML tags. No functional changes.
 * 
 *  10/06/10    1.5     K.McD           1.  Renamed a number of variables for consistency.
 * 
 *  10/15/10    1.6     K.McD           1.  Modified to use the generic version of PTUDialog<>.
 * 
 *  03/21/11    1.7     K.McD           1.  Added a zero parameter constructor.
 * 
 *                                      2.  Modified the signature of the constructor to use a 'VariableControl' rather than 'WatchBitmaskControl' in order to 
 *                                          make the form more generic.
 *                                      3.  Modified the modifiers associated with a number of methods and variables to 'protected' so that they could be used 
 *                                          in the FormShowFlagsEvent child class.
 *                                      4.  Removed the readonly qualifier from the member variable used to save the reference to the calling user control.
 *                                      5.  Modified the name of the member variable used to save the reference to the calling user control.
 * 
 *  03/28/11    1.8     K.McD           1.  Modified to use the old identifier of the watch variable associated with the control that called this form.
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;

using Common.Forms;
using Common.Configuration;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// Form to display the state of the individual bits within a bit mask watch variable.
    /// </summary>
    public partial class FormShowFlags : FormPTUDialog
    {
        #region --- Constants ---
        /// <summary>
        /// The key associated with the <c>TableLayoutPanel</c> control for each tab page. Value: "m_TableLayoutPanel".
        /// </summary>
        private const string KeyTableLayoutPanel = "m_TableLayoutPanel";

        /// <summary>
        /// The format string to display a value using hexadecimal format e.g. 0F0A. Value: "X".
        /// </summary>
        private const string FormatStringHex ="X";

        /// <summary>
        /// The string that is to appear before a value displayed in hexadecimal format e.g.0x0A. Value: "0x".
        /// </summary>
        private const string FormatStringHexHeader = "0x";

        /// <summary>
        /// The interval, in ms, between successive display updates. Value: 500ms.
        /// </summary>
        private const int IntervalDisplayUpdateMs = 200;

        /// <summary>
        /// The binding string used when appending a supplemental message to an existing message. Value: " - ".
        /// </summary>
        private const string BindingMessage = " - ";

        #region - [Layout] -
        /// <summary>
        /// The Y coordinate adjustment to be applied when determining the preferred location. Value 15px.
        /// </summary>
        private const int AdjustY = 15;

        /// <summary>
        /// The X coordinate adjustment to be applied when determining the preferred location. Value: 15px.
        /// </summary>
        private const int AdjustX = 15;

        /// <summary>
        /// The X coordinate adjustment to be applied when flipping the form over to the other side of the control. Value: 20px. 
        /// </summary>
        private const int AdjustXFlipForm = 20;

        /// <summary>
        /// The top margin between the label and the next control. Value: 1px.
        /// </summary>
        private const int MarginTopLabel = 1;

        /// <summary>
        /// The bottom margin between the label and the next control. Value: 1px.
        /// </summary>
        private const int MarginBottomLabel = 1;

        /// <summary>
        /// The padding arount the label. Value 2px.
        /// </summary>
        private const int PaddingAll = 2;

        /// <summary>
        /// The horizontal padding associated with the layout panel, used to determine the size of the form. Value: 8px.
        /// </summary>
        private const int PaddingTableLayoutPanelHorizontal = 8;

        /// <summary>
        /// The horizontal padding associated with the layout panel, used to determine the size of the form. Value: 25px.
        /// </summary>
        private const int PaddingTableLayoutPanelWithScrollBarHorizontal = 25;

        /// <summary>
        /// The vertical padding associated with the layout panel, used to determine the size of the form. Value: 6px.
        /// </summary>
        private const int PaddingTableLayoutPanelVertical = 6;

        /// <summary>
        /// Vertical form border associated with a sizeable form. Value: 10px.
        /// </summary>
        private const int BorderSizeableVertical = 10;

        /// <summary>
        /// Horizontal form border associated with a sizeable form. Value: 10 px.
        /// </summary>
        private const int BorderSizeableHorizontal = 10;

        /// <summary>
        /// The height to subtract from the the primary screen display height when adjusting the form height to fit within the bounds of the primry screen. Value: 25.
        /// </summary>
        private const int HeightAdjust = 25;

        /// <summary>
        /// The width of each label, in pixels. Value: 250px.
        /// </summary>
        private const int WidthLabel = 250;

        /// <summary>
        /// The height of each label, in pixels. Value: 23px.
        /// </summary>
        private const int HeightLabel = 23;
        #endregion - [Layout] -
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The previous value of the <c>BitmaskUserControl</c> that showed this form. Used to determine if the value has changed since the previous update.
        /// </summary>
        protected uint m_PreviousValue;

        /// <summary>
        /// Reference to the <c>TableLayoutPanel</c> control used to layout the list of flags.
        /// </summary>
        protected TableLayoutPanel m_TableLayoutPanel;

        /// <summary>
        /// Reference to the bit mask user control that showed this form.
        /// </summary>
        protected VariableControl m_VariableControl;

        /// <summary>
        /// Reference to the watch variable associated with the user control that called this form.
        /// </summary>
        protected readonly WatchVariable m_WatchVariable;

        /// <summary>
        /// Reference to the client form as type <c>FormPTU</c>.
        /// </summary>
        protected FormPTU m_ClientAsFormPTU;

        /// <summary>
        /// The old identifier of the bitmask watch variable associated with the user control that called this form.
        /// </summary>
        private readonly short m_OldIdentifier;

        /// <summary>
        /// The name of the watch variable that appears in the form title.
        /// </summary>
        private readonly string m_WatchVariableName;

        /// <summary>
        /// Reference to the client form as type <c>IDataUpdate</c>.
        /// </summary>
        private readonly IDataUpdate m_ClientAsIDataUpdate;

        /// <summary>
        /// Tracks whether the form is in drag mode. If it is, mouse movement over the control will be translated into form movements.
        /// </summary>
        private bool m_Dragging;

        /// <summary>
        /// Stores the offset where the control is clicked.
        /// </summary>
        private Point m_PointClicked;

        /// <summary>
        /// The <c>BackColor</c> associated with the flag if the flag is in the asserted state. Value: Color.Red.
        /// </summary>
        private readonly Color m_BackColorAssertedState = Color.Yellow;

        /// <summary>
        /// The <c>ForeColor</c> associated with the flag if the flag is in the asserted state. Value: Color.Black.
        /// </summary>
        private readonly Color m_ForeColorAssertedState = Color.Black;

        /// <summary>
        /// The <c>BackColor</c> associated with the flag if the flag is NOT in the asserted state i.e. it is clear. Value: Color.WhiteSmoke.
        /// </summary>
        private readonly Color m_BackColorClearState = Color.WhiteSmoke;

        /// <summary>
        /// The <c>ForeColor</c> associated with the flag if the flag is NOT in the asserted state i.e. it is clear. Value: Color.ForrestGreen.
        /// </summary>
        private readonly Color m_ForeColorClearState = Color.ForestGreen;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the form. Zero parameter constructor.
        /// </summary>
        public FormShowFlags()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the form. (1) Positions the form; (2) displays the watch variable and current value; (3) displays the flag state 
        /// corresponding to the current value of the watch variable.
        /// </summary>
        /// <param name="bitmaskControl">Reference to the bit mask control that called this form.</param>
        public FormShowFlags(VariableControl bitmaskControl)
        {
            InitializeComponent();

            m_VariableControl = bitmaskControl;

            // Add this form to the list of opened dialog forms associated with the client form.
            m_ClientAsFormPTU = m_VariableControl.ClientForm as FormPTU;
            if (m_ClientAsFormPTU != null)
            {
                m_ClientAsFormPTU.OpenedDialogBoxList.Add(this);
            }

            // Register the event handler for the data update event.
            m_ClientAsIDataUpdate = m_VariableControl.ClientForm as IDataUpdate;
            if (m_ClientAsIDataUpdate != null)
            {
                m_ClientAsIDataUpdate.DataUpdate += new EventHandler(DataUpdate);
            }

            m_OldIdentifier = (short)m_VariableControl.Identifier;
            try
            {
                m_WatchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[m_OldIdentifier];
                if (m_WatchVariable == null)
                {
                    throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
                }
            }
            catch (Exception)
            {
                throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
            }

            m_WatchVariableName = m_VariableControl.VariableNameFieldText;
            
            // Get a list of the current state of each flag and keep a record to determine if there is a change in state.
            List<FlagState_t> flagStateList = Lookup.WatchVariableTableByOldIdentifier.GetFlagStateList(m_OldIdentifier, m_PreviousValue = GetValue());

            m_TableLayoutPanel = ConstructLayoutPanel();
            m_PanelFlagList.Controls.Add(m_TableLayoutPanel);
            ConfigureTableLayoutPanel(m_TableLayoutPanel, flagStateList);

            UpdateTitle(m_WatchVariableName, GetValue());
            UpdateFlagStates(m_TableLayoutPanel.Controls, flagStateList);
            CheckHeight();
            PositionTheForm(m_VariableControl);
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
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    if (m_ClientAsIDataUpdate != null)
                    {
                        // De-register the event handler for the data update event.
                        m_ClientAsIDataUpdate.DataUpdate -= new EventHandler(DataUpdate);
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                m_TableLayoutPanel = null;
                m_VariableControl = null;
                m_ClientAsFormPTU = null;

                #region --- Windows Form Designer Variables ---
                // Detach the event handler delegates.

                // Set the Windows Form Designer Variables to null.

                #endregion --- Windows Form Designer Variables ---
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
        /// <summary>
        /// Event handler for the close button <c>Click</c> event. Closes the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonClose_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Remove this form from the list of opened dialog forms associated with the client form.
            m_ClientAsFormPTU.OpenedDialogBoxList.Remove(this);

            // Possibly because the ControlBox property of this form is not set, calling the Close() method does not result in the Dispose() method being called.
            // Instead of calling the Close() method call to the Dispose() method instead.
            Dispose();
        }

        /// <summary>
        /// Event handler associated with the <c>DataUpdate</c> event. Determine if the bitmask control value has changed and, if so, update flags state.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void DataUpdate(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            uint value = GetValue();
            if (value != m_PreviousValue)
            {
                // Get a list of the current state of each flag and keep a record to determine if there is a change in state.
                List<FlagState_t> flagStateList = Lookup.WatchVariableTableByOldIdentifier.GetFlagStateList(m_OldIdentifier, m_PreviousValue = value);

                UpdateTitle(m_WatchVariableName, value);
                UpdateFlagStates(m_TableLayoutPanel.Controls, flagStateList);
            }
        }

        #region - [Dragging Operation] -
        /// <summary>
        /// <c>MouseDown</c> event handler. Initiates the dragging process.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_PanelWatchVariable_MouseDown(object sender, MouseEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                m_Dragging = true;
                m_PointClicked = new Point(e.X, e.Y);
            }
            else
            {
                m_Dragging = false;
            }
        }

        /// <summary>
        /// <c>MouseMove</c> event handler. Processes dragging movements if the form is in drag mode.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_PanelWatchVariable_MouseMove(object sender, MouseEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_Dragging)
            {
                Point pointMoveTo;

                // Find the current mouse position in screen coordinates.
                pointMoveTo = this.PointToScreen(new Point(e.X, e.Y));

                pointMoveTo.Offset(-m_PointClicked.X, -m_PointClicked.Y);

                // Move the form;
                this.Location = pointMoveTo;
            }
        }

        /// <summary>
        /// <c>MouseUp</c> event handler. Terminates the drag process.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_PanelWatchVariable_MouseUp(object sender, MouseEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_Dragging = false;
        }
        #endregion - [Dragging Operation] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Instantiates and initializes a new <c>TableLayoutPanel</c> control. This panel will be used to layout the plotter controls required to display the 
        /// watch variable data associated with the specified display column of the workset.
        /// </summary>
        /// <returns>The initialized <c>TableLayoutPanel</c> control.</returns>
        protected TableLayoutPanel ConstructLayoutPanel()
        {
            TableLayoutPanel tableLayoutPanel;
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Name = KeyTableLayoutPanel;
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.AutoSize = true;
            tableLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            tableLayoutPanel.Visible = false;
            tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            return tableLayoutPanel;
        }

        /// <summary>
        /// Add the flag labels used to display the flag information to the specified layout panel.
        /// </summary>
        /// <param name="tableLayoutPanel">The layout panel to which the flag labels are to be added.</param>
        /// <param name="flagStateList">The list defining the flags that are to be shown.</param>
        protected void ConfigureTableLayoutPanel(TableLayoutPanel tableLayoutPanel, List<FlagState_t> flagStateList)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Adjust the size of the flag labels to take into account the cancel button.
            Size labelSize = new Size(WidthLabel + m_ButtonClose.Size.Width, HeightLabel);
            Padding margin = new Padding(0, MarginTopLabel, 0, MarginBottomLabel);
            Padding padding = new Padding(PaddingAll);

            // Clear the layout panel.
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = 0;

            // Configure each of the labels.
            Label label;
            for (int index = 0; index < flagStateList.Count; index++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, labelSize.Height));
                tableLayoutPanel.RowCount++;

                label = new Label();
                label.Text = flagStateList[index].Description;
                label.AutoEllipsis = true;
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.Margin = margin;
                label.Padding = padding;
                label.Size = labelSize;
                tableLayoutPanel.Controls.Add(label);
            }

            tableLayoutPanel.Visible = true;
        }

        /// <summary>
        /// Get the current value of the bitmask variable associated with the user control that called this form.
        /// </summary>
        /// <returns>The current value of the bitmask watch variable.</returns>
        protected uint GetValue()
        {
            Debug.Assert(m_VariableControl != null);
            return (uint)m_VariableControl.Value;
        }

        /// <summary>
        /// Update the form title, this shown the watch variable name and the current value.
        /// </summary>
        protected void UpdateTitle(string watchVariableName, uint value)
        {
            m_LabelVariableName.Text = watchVariableName + BindingMessage + FormatStringHexHeader + value.ToString(FormatStringHex);
        }

        /// <summary>
        /// Update the <c>BackColor</c> and <c>ForeColor</c> properties  of each flag to reflect the current state.
        /// </summary>
        /// <param name="flagStateList">The list defining the current flag states.</param>
        /// <param name="controlCollection">The collection of label controls used to display the flag states.</param>
        protected void UpdateFlagStates(TableLayoutControlCollection controlCollection, List<FlagState_t> flagStateList)
        {
            byte bit;
            bool logicState;
            for (int index = 0; index < flagStateList.Count; index++)
            {
                bit = flagStateList[index].Bit;

                // Get the flag state associated with the bit.
                logicState = flagStateList.Find(delegate(FlagState_t flagState) { return flagState.Bit == bit; }).State;

                if (logicState == true)
                {
                    controlCollection[index].ForeColor = m_ForeColorAssertedState;
                    controlCollection[index].BackColor = m_BackColorAssertedState;
                }
                else
                {
                    controlCollection[index].ForeColor = m_ForeColorClearState;
                    controlCollection[index].BackColor = m_BackColorClearState;
                }
            }
        }

        /// <summary>
        /// Check whether the the auto-sized height of the <c>TableLayoutPanel</c> exceeds the height of the primary screen and, if so, adjusts it appropriately such 
        /// that the panel containing the flags will scroll.
        /// </summary>
        protected void CheckHeight()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_TableLayoutPanel.Height > Screen.PrimaryScreen.WorkingArea.Height - HeightAdjust)
            {
                this.Height = Screen.PrimaryScreen.WorkingArea.Height - HeightAdjust;
                this.Width = m_TableLayoutPanel.Width + PaddingTableLayoutPanelWithScrollBarHorizontal + BorderSizeableHorizontal;
            }
            else
            {
                this.Height = m_TableLayoutPanel.Height + m_PanelWatchVariable.Height + PaddingTableLayoutPanelVertical + BorderSizeableVertical;
                this.Width = m_TableLayoutPanel.Width + PaddingTableLayoutPanelHorizontal + BorderSizeableHorizontal;
            }
        }

        /// <summary>
        /// Position the form relative to the position of the bitmask control that called the form.
        /// </summary>
        /// <param name="bitmaskControl">The bitmask user control that alled this form.</param>
        protected void PositionTheForm(VariableControl bitmaskControl)
        {
            // Work out the preferred location where the form is to be positioned, this is to the right of the bit mask control so that it is aligned with the
            // selected control.

            // Get the location of the bitmask control in screen coordinates.
            Point preferredLocation = m_VariableControl.PointToScreen(bitmaskControl.ClientForm.Location);

            // Offset this by the width of the control plus a few minor X and Y adjustments.
            preferredLocation.Offset(new Point(bitmaskControl.Size.Width + AdjustX, AdjustY));

            int xCoordinate = preferredLocation.X;
            int yCoordinate = preferredLocation.Y;
            int formWidth = this.Size.Width;
            int formHeight = this.Size.Height;

            // Ensure that the form remains within the bounds of the screen.
            if ((yCoordinate + formHeight) > Screen.PrimaryScreen.WorkingArea.Height)
            {
                yCoordinate = Screen.PrimaryScreen.WorkingArea.Height - formHeight;
            }

            // Check whether the whole form can still fit on the screen and, if not, flip over to the other side of the control.
            if ((xCoordinate + formWidth) > Screen.PrimaryScreen.WorkingArea.Width)
            {
                // Flip over to the other side of the control.
                xCoordinate -= (formWidth + m_VariableControl.Size.Width + AdjustXFlipForm);
            }

            this.Location = new Point(xCoordinate, yCoordinate);
        }
        #endregion --- Methods ---
    }
}
