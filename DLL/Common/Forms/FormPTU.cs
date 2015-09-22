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
 *  File name:  FormPTU.cs
 * 
 *  Revision History
 *  ----------------
 */

/* 
 *  Date        Version Author          Comments
 *  04/26/10    1.0     K.McDonald      First Release.
 * 
 *  08/16/10    1.1     K.McD           1.  Bug fix SNCR 001.009. If the user exits the replay screen directly rather than returning back via the YT plot screen, 
 *                                          the function keys are not restored correctly.
 * 
 *  08/18/10    1.2     K.McD           1.  Minor documentation changes.
 * 
 *  08/20/10    1.3     K.McD           1.  Added the OpenedDialogBoxList property to keep track of any dialog boxes which register with the form This allows the
 *                                          form to close any dialog boxes that are still open wnenever the forms Dispose method is called.
 * 
 *  08/14/10    1.4     K.McD           1.  Minor documentation changes and renaming of variables.
 * 
 *  08/24/10    1.5     K.McD           1.  Added support to enable the child form programmer to modify and restore the Enabled property of individual menu options.
 * 
 *                                              (a) Added structure ToolStripMenuItemEnabled_t.
 *                                              (b) Added member variable List<ToolStripMenuItemEnabled_t> m_ToolStripMenuItemEnabledList.
 *                                              (c) Added the methods RestoreMenuOptionEnabled() and ModifyMenuOptionEnabled().
 * 
 *  08/25/10    1.6     K.McD           1.  Added the Pause property to allow other dialog forms that need to communicate with the target to suspend/pause communication 
 *                                          requests to the target until the dialog form is disposed of.
 * 
 *  08/26/10    1.7     K.McD           1.  Added the virtual event handler for the ToolStripComboBox SelectedIndexChanged event.
 *                                      2.  Added a check for IDisposed in the Cleanup() method.
 * 
 *  09/30/10    1.8     K.McD           1.  Made the MainWindow property public. This allows any class, that has a reference to a form of type FormPTU, to access any of 
 *                                          the properties associated with the main application window. This modification was made specifically to allow the 
 *                                          enumerator user control to determine the state of the Enumeration property of the main application window.
 * 
 *  10/06/10    1.9     K.McD           1.  Removed the Pause property and associated PauseChanged event, this feature is now implemented using the IPollTarget interface.
 *                                      2.  Included the CloseAllDialogBoxForms() method to simplify the process of closing any dialog box forms that are currently open.
 *                                      3.  Include the font of the combo box control used to select the workset in the list of controls included in the ChangeFont()
 *                                          method.
 * 
 *  11/09/10    1.10    K.McD           1.  Added a virtual event handler for the form Resize event.
 *                                      2.  Now uses the Checked property of the F1 key when displaying the help file.
 * 
 *  11/16/10    1.11    K.McD           1.  Added support for the Form_Resize event.
 * 
 *  01/31/11    1.12    K.McD           1.  Modified the DisplayFunctionKey() method so that it only displays the tool tip text if the specified text string is not empty.
 *                                          This modification was carried out as the tool tip text associated with the first, next, previous and last buttons on the 
 *                                          replay screen obscured the information labels.
 * 
 *  02/14/11    1.13    K.McD           1.  Modified a number of XML tags and renamed a number of methods.
 *                                      2.  Bug fix - SNCR001.100/53. Added the UpdateMenu() virtual method; this re-applies any form specific changes to the main menu 
 *                                          that were applied after the form was instantiated. Used following a change to the security level of the user and this action 
 *                                          will 'reset' the main menu to its initial state.
 *                                      3.  Modified the ToolStripMenuItemEnabled_t structure to include member variables that record both the current and new values of 
 *                                          the Enabled property of the menu item.
 *                                      4.  Bug fix. Corrected the RestoreMenuOptionEnabled() method so that the menu items are restored in reverse order in case the 
 *                                          menu options have been modified more than once, otherwise the final result will be incorrect.
 * 
 *  02/28/11    1.14    K.McD           1.  Added the ToolStripItemCollectionCalledFrom property to record the menu items associated with the recording form.
 *                                      2.  Renamed the Escape ToolStripButton control.
 *                                      3.  Modified a number of XML tags.
 *                                      4.  Modified the DisplayFunctionKey() method such that the Visible property of the separator associated with the specified 
 *                                          ToolStripButton control will only be set if the separator control is found.
 *                                      5.  Added calls to the SuspendLayout() and PerformLayout() methods in the escape key event handler and Exit() method to improve 
 *                                          aesthetics.
 * 
 *  03/18/11    1.14.1  K.McD           1.  Renamed method CloseAllDialogBoxForms() to CloseShowFlags().
 * 
 *  03/28/11    1.14.2  K.McD           1.  Included support for the 'Print Screen' function key.
 * 
 *  04/08/11    1.14.3  K.McD           1.  Renamed a number of the ScreenCaptureType enumerator fields.
 *                                      2.  Modified the F2-Print event handler such that the cursor was set to the wait cursor after the user has selected the file name.
 * 
 *  04/27/11    1.14.4  K.McD           1.  Removed a TODO comment from the DisplayFunctionKey() method.
 *  
 *  07/07/11    1.15    K.McD           1.  Modified the MainWindow property to allow the property to be set.
 *  
 *  07/29/11    1.15.1  K.McD           1.  Corrected the event handler associated with the 'F2-Print' function key to include the capture type in the extension of the 
 *                                          default filename.
 *                                          
 *                                      2.  Added support for the self test screen in the ScreenCaptureType enumerator.
 *                                      
 *  10/02/11    1.15.2  K.McD           1.  Removed the call to the CloseShowFlags() method in the Cleanup() method. This call is now made in the individual child classes 
 *                                          as the user may want the state of the individual bits of a bitmask watch variable to remain on display when switching from the 
 *                                          Replay form to the Plot form.
 *                                          
 *                                      2.  Modified the OpenedDialogBoxList property so that it is read/write.
 * 
 *	10/12/11	1.15.3	Sean.D			1. Made a very minor correction in the summary comment of FormPTU.
 *	
 *  10/25/11    1.15.4  K.McD           1.  Bug fix - SNCR002.41. Modified the Shown event handler and the Exit() method to clear and assert the KeyPreview property of 
 *                                          the IMainWindow interface respectively. This ensures that any Key related events are not routed to the KeyDown event handler 
 *                                          of the main application window when the child form is on display. In the previous implementation, pressing the F2 function key 
 *                                          while displaying the watch window would acively toggle the Online ToolStripButton defined on the main application window.
 *                                          
 *                                      2.  Bug fix - SNCR002.41. Included a check in the event handler for each ToolStripButton Click event to ensure that the logic 
 *                                          associated with the event handler is ignored if the control isn't enabled. This check is required because the event handler 
 *                                          is also called by the FormPTU.Form_KeyDown() event handler whenever the function key associated with the ToolStripButton 
 *                                          control is pressed. Consequently, in the previous implementation, the action associated with the ToolStripButton would be 
 *                                          carried out whenever the user pressed the corresponding function key, regardless of whether the ToolStropButton control 
 *                                          was enabled or not.
 *                                          
 *                                      3.  Removed the redundant ToolStripFunctionKeysPTU property.
 *                                      
 *                                      4.  Removed the redundant m_PanelInformation_Click() and m_ToolStrip_Click() event handlers as these did not appear to move 
 *                                          the focus away from a selected user control.
 *	11/14/11	1.15.5	Sean.D			1.	In Cleanup, added code to remove the Shown event and added checks to ensure MainWindow and m_ToolStripFunctionKeysPTU are
 *											not null prior to accessing them.
 *											
 *  11/23/11    1.15.6  K.McD           1.  Ensured that all event handler methods were detached and that the component designer variables were set to null on disposal.
 *
 */

/*
 *  04/15/15    1.16    K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order 4800010525-CU2/19.03.2015.
 * 
 *                                          1.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be modified
 *                                              to meet the KRC specification. This modification will be specific to the KRC project; for all other projects, the
 *                                              current naming convention will still apply.
 *                                              
 *                                      2.  Although only relevant to Bombardier Field Service Engineers that support a number of PTU projects, the Software User Manual
 *                                          and Release Notes documents are to be made project specific by prepending the project identifier to the file name e.g.
 *                                          'R8PR.Portable Test Unit - Release Notes.pdf', 'CTPA.Portable Test Unit - Release Notes.pdf' etc.
 *                                      
 *                                      Modifications
 *                                      1.  Modified the signature in the call to the General.DeriveName() method in the 'F2_Click' event handler. The original
 *                                          signature has been removed in order to simplify file naming. - Ref. 1.1.
 *                                      2.  Modified the F1_Click() event handler to include the project identifier in the filename for the Software User Manual.
 *                                          - Ref.: 2.
 *                                      3.  Added the call to CloseShowFlags() method in the Exit() method.
 *                                      
 *  05/13/15    1.17    K.McD           References
 *                                      1.  Hide the ToolTip text for the [First], [Next], [Previous] and [Last] function keys on the FormDataStreamReplay form as
 *                                          they obscures the [Frame No.] information label.
 *                                          
 *                                      Modifications
 *                                      1.  Modify the DisplayFunctionKey() method to actively set the ToolTipText property to string.Empty if the toolTip parameter is
 *                                          string.Empty.
 */

/*
 *  08/12/15    1.18    K.McD           References
 *                                      1.  Changes resulting from documents: 'PTU MOC Findings - .docx' and 'PTU Installation on 64-bit Machine_v1-08022015.docx' sent
 *                                          from Atul Chaudhari on 4th Aug 2015 and the follow up email sent on 5th Aug 2015.
 *                                      
 *                                          1.  On the R188 project only, all references to 'PTU' are to be replaced with 'PTE' and all occurrences of 'Portable Test Unit'
 *                                              are to be replaced with 'Portable Test Equipment'. Where support for multiple legends is not possible, 'Portable Test
 *                                              Application' is to be used as an alternative to 'Portable Test Unit'/'Portable Test Equipment.
 *                                              
 *                                      Modifications
 *                                      1.  Modified the event handler for the 'F1-Help' function key to include support for both 'Portable Test Unit - Software User
 *                                          Manual' and 'Portable Test Equipment - Software User Manual'.
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Collections.Generic;

using CodeProject.GraphComponents;
using Common;
using Common.Communication;
using Common.Configuration;
using Common.UserControls;
using Common.Properties;

namespace Common.Forms
{
    /// <summary>
    /// The screen type associated with the image.
    /// </summary>
    public enum ScreenCaptureType
    {
        /// <summary>
        /// Live watch variable screen.
        /// </summary>
        Watch,

        /// <summary>
        /// Event log screen.
        /// </summary>
        Event,

        /// <summary>
        /// Plot of saved watch variable values against time screen.
        /// </summary>
        Plot,

        /// <summary>
        /// Replay of saved watch variables screen.
        /// </summary>
        Replay,

        /// <summary>
        /// Self test screen.
        /// </summary>
        SelfTest,

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined
    }

    /// <summary>
    /// A structure to store the key access string along with the new and current state of the Enabled property of <c>ToolStripMenuItem</c> control.
    /// </summary>
    /// <remarks>Allows the original state of the Enabled property to be restored when the child form is disposed of.</remarks>
    public struct ToolStripMenuItemEnabled_t
    {
        /// <summary>
        /// The key used to access the <c>ToolStripMenuItem</c> i.e. the string value of the <c>Name</c> property.
        /// </summary>
        public string Key;

        /// <summary>
        /// The current state of the <c>Enabled</c> property associated with the <c>ToolStripMenuItem</c> represended by the Key text string.
        /// </summary>
        public bool EnabledCurrent;

        /// <summary>
        /// The new state of the <c>Enabled</c> property associated with the <c>ToolStripMenuItem</c> represended by the Key text string.
        /// </summary>
        public bool EnabledNew;
    }

    /// <summary>
    /// Base form from which ALL PTU child forms should be derived.
    /// </summary>
    public partial class FormPTU : Form
    {
        #region --- Constants ---
        /// <summary>
        /// The key that is to be used to identify the control as a tool strip separator control. Value: "m_TSSeparator".
        /// </summary>
        private const string KeyToolStripSeparator = "m_TSSeparator";

        /// <summary>
        /// The name that is to be used to identify the control as a legend label. Value: "m_Legend".
        /// </summary>
        private const string LegendName = "m_Legend";

        /// <summary>
        /// The delay, in ms, to allow the save dialog box to clear when capturing the screen. Value: 2000 ms.
        /// </summary>
        private const int SleepCapture = 2000;
        
        /// <summary>
        /// The <c>SaveFileDialog</c>, <c>FilterIndex</c> property corresponding to JPG format. Value: 1.
        /// </summary>
        private const int FilterIndexJPG = 1;
        
        /// <summary>
        /// The <c>SaveFileDialog</c>, <c>FilterIndex</c> property corresponding to BMP format. Value: 2.
        /// </summary>
        private const int FilterIndexBMP = 2;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Flag to indicate whether the Dispose() method has been called. True indicates that the Dispose() method has been called; otherwise, false.
        /// </summary>
        private bool m_IsDisposed;

        /// <summary>
        /// Reference to the form that called this child form.
        /// </summary>
        /// <remarks>This form is not usually called by the multiple document interface parent form but by the form used to view the Y value agains time plot
        /// of the data contained withing the historic data manager class.</remarks>
        protected Form m_CalledFrom;

        /// <summary>
        /// Reference to the main application window.
        /// </summary>
        private IMainWindow m_MainWindow;

        /// <summary>
        /// Reference to the KeyEventArgs object associated with the last recorded key press. 
        /// </summary>
        private KeyEventArgs m_KeyEventArgs;

        /// <summary>
        /// The type of screen that is being captured.
        /// </summary>
        private ScreenCaptureType m_ScreenCaptureType;

        /// <summary>
        /// A list of any dialog boxes associated with the child form that remain open.
        /// </summary>
        private List<Form> m_OpenedDialogBoxList = new List<Form>();

        /// <summary>
        /// A list of the original state of the Enabled property for each menu option that is disabled/enabled by the child form.
        /// </summary>
        /// <remarks>Allows the original state of the Enabled property to be restored when the child form is disposed of.</remarks>
        protected List<ToolStripMenuItemEnabled_t> m_ToolStripMenuItemEnabledList = new List<ToolStripMenuItemEnabled_t>();

        #region - [ToolStrip Manipulation] -
        /// <summary>
        /// The collection of tool strip items associated with the calling form.
        /// </summary>
        protected ToolStripItemCollection m_ToolStripItemCollectionCalledFrom;

        /// <summary>
        /// The collection of tool strip items associated with this form.
        /// </summary>
        private ToolStripItemCollection m_ToolStripItemCollectionCurrent;
        #endregion - [ToolStrip Manipulation] -

        #region - [Security] -
        /// <summary>
        /// Reference to the <c>Security</c> class associated with the PTU.
        /// </summary>
        private Security m_Security;
        #endregion - [Security] -
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the form.
        /// </summary>
        public FormPTU()
        {
            InitializeComponent();

            m_Security = new Security();
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Cleanup(bool disposing)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            try
            {
                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
				if (MainWindow != null)
				{
					MainWindow.FontChanged -= new System.EventHandler(ChangeFont);
                    m_MainWindow = null;
				}

                if (m_OpenedDialogBoxList != null)
                {
                    m_OpenedDialogBoxList.Clear();
                    m_OpenedDialogBoxList = null;
                }

                if (m_ToolStripMenuItemEnabledList != null)
                {
                    m_ToolStripMenuItemEnabledList.Clear();
                    m_ToolStripMenuItemEnabledList = null;
                }

                if (m_ToolStripItemCollectionCalledFrom != null)
                {
                    m_ToolStripItemCollectionCalledFrom.Clear();
                    m_ToolStripItemCollectionCalledFrom = null;
                }

                if (m_ToolStripItemCollectionCurrent != null)
                {
                    m_ToolStripItemCollectionCurrent.Clear();
                    m_ToolStripItemCollectionCurrent = null;
                }

                m_KeyEventArgs = null;
                m_MainWindow = null;
                m_Security = null;
                m_CalledFrom = null;

				// This is to prevent a situation where the toolstrip maintains an automatically subscribed UserPreferenceChanged event.
				if (m_ToolStripFunctionKeysPTU != null)
				{
					m_ToolStripFunctionKeysPTU.Visible = false;
				}

                #region - [Detach the event handler methods.] -
                this.m_TSBEsc.Click -= new EventHandler(this.Escape_Click);
                this.m_TSBF1.Click -= new EventHandler(this.F1_Click);
                this.m_TSBF2.Click -= new EventHandler(this.F2_Click);
                this.m_TSBF3.Click -= new EventHandler(this.F3_Click);
                this.m_TSBF4.Click -= new EventHandler(this.F4_Click);
                this.m_TSBF5.Click -= new EventHandler(this.F5_Click);
                this.m_TSBF6.Click -= new EventHandler(this.F6_Click);
                this.m_TSBF7.Click -= new EventHandler(this.F7_Click);
                this.m_TSBF8.Click -= new EventHandler(this.F8_Click);
                this.m_TSBF9.Click -= new EventHandler(this.F9_Click);
                this.m_TSBF10.Click -= new EventHandler(this.F10_Click);
                this.m_TSBF11.Click -= new EventHandler(this.F11_Click);
                this.m_TSBF12.Click -= new EventHandler(this.F12_Click);
                this.m_ToolStripComboBox1.SelectedIndexChanged -= new EventHandler(this.m_ToolStripComboBox1_SelectedIndexChanged);
                this.Shown -= new EventHandler(this.FormPTU_Shown);
                this.KeyDown -= new KeyEventHandler(this.FormPTU_KeyDown);
                this.KeyUp -= new KeyEventHandler(this.FormPTU_KeyUp);
                this.Resize -= new EventHandler(this.FormPTU_Resize);
                #endregion - [Detach the event handler methods.] -

                #region - [Component Designer Variables] -
                this.m_ToolStripFunctionKeysPTU = null;
                this.m_TSBEsc = null;
                this.m_TSSeparatorEsc = null;
                this.m_TSBF1 = null;
                this.m_TSSeparatorF1 = null;
                this.m_TSBF2 = null;
                this.m_TSSeparatorF2 = null;
                this.m_TSBF3 = null;
                this.m_TSSeparatorF3 = null;
                this.m_TSBF4 = null;
                this.m_TSSeparatorF4 = null;
                this.m_TSBF5 = null;
                this.m_TSSeparatorF5 = null;
                this.m_TSBF6 = null;
                this.m_TSSeparatorF6 = null;
                this.m_TSBF7 = null;
                this.m_TSSeparatorF7 = null;
                this.m_TSBF8 = null;
                this.m_TSSeparatorF8 = null;
                this.m_TSBF9 = null;
                this.m_TSSeparatorF9 = null;
                this.m_TSBF10 = null;
                this.m_TSSeparatorF10 = null;
                this.m_TSBF11 = null;
                this.m_TSSeparatorF11 = null;
                this.m_TSBF12 = null;
                this.m_TSSeparatorF12 = null;
                this.m_ToolStripComboBox1 = null;
                this.m_ToolStripLegendComboBox1 = null;
                this.m_PanelInformation = null;
                this.m_TableLayoutPanelInformationLabels = null;
                this.m_Legend2 = null;
                this.m_Label1 = null;
                this.m_Legend1 = null;
                this.m_Legend4 = null;
                this.m_Label2 = null;
                this.m_Legend3 = null;
                this.m_Label3 = null;
                this.m_Label4 = null;
                this.m_Legend5 = null;
                this.m_Label5 = null;
                this.m_Label6 = null;
                this.m_Legend6 = null;
                this.m_TabControl = null;
                this.m_TabPage1 = null;
                #endregion - [Component Designer Variables] -
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
            }
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        #region - Form Events -
        /// <summary>
        /// Event handler for the <c>Shown</c> event. Functionality is defined in the child form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void FormPTU_Shown(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Update();

            // Ensure that an exception isn't thrown when a child form is opened in the Visual Studio development environment.
            if (MdiParent == null)
            {
                return;
            }

            // Get the reference to the main application window.
            m_MainWindow = MdiParent as IMainWindow;

            #region - [Font] -
            // Ensure that the Font is set to that of the PTU main application window.
            ChangeFont(this, new EventArgs());

            // Subscribe to the FontChaged event in the PTU main application window.
            MainWindow.FontChanged += new System.EventHandler(ChangeFont);
            #endregion - [Font] -

            // Get the current function key tool strip items.
            m_ToolStripItemCollectionCalledFrom = General.GetToolStripItemCollection(MainWindow.ToolStripFunctionKeys);
            Debug.Assert(m_ToolStripItemCollectionCalledFrom != null);

            // Get the function key tool strip items associated with this form.
            m_ToolStripItemCollectionCurrent = General.GetToolStripItemCollection(m_ToolStripFunctionKeysPTU);
            Debug.Assert(m_ToolStripItemCollectionCurrent != null);

            // Display the function keys associated with this form.
            MainWindow.ToolStripFunctionKeys.SuspendLayout();
            MainWindow.ToolStripFunctionKeys.Items.Clear();
            MainWindow.ToolStripFunctionKeys.Items.AddRange(m_ToolStripItemCollectionCurrent);
            MainWindow.ToolStripFunctionKeys.ResumeLayout(false);
            MainWindow.ToolStripFunctionKeys.PerformLayout();
            MainWindow.ToolStripFunctionKeys.Update();

            MainWindow.KeyPreview = false;
        }

        /// <summary>
        /// Event handler for the form <c>Resize</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void FormPTU_Resize(object sender, EventArgs e)
        {
        }
        #endregion - Form Events -

        #region - Key Events -
        /// <summary>
        /// Event handler for the <c>KeyDown</c> event. Maps the Function keys to the <c>ToolStrip</c> buttons.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormPTU_KeyDown(object sender, KeyEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Escape_Click(sender, e);
                    break;
                case Keys.F1:
                    F1_Click(sender, e);
                    break;
                case Keys.F2:
                    F2_Click(sender, e);
                    break;
                case Keys.F3:
                    F3_Click(sender, e);
                    break;
                case Keys.F4:
                    F4_Click(sender, e);
                    break;
                case Keys.F5:
                    F5_Click(sender, e);
                    break;
                case Keys.F6:
                    F6_Click(sender, e);
                    break;
                case Keys.F7:
                    F7_Click(sender, e);
                    break;
                case Keys.F8:
                    F8_Click(sender, e);
                    break;
                case Keys.F9:
                    F9_Click(sender, e);
                    break;
                case Keys.F10:
                    F10_Click(sender, e);
                    break;
                case Keys.F11:
                    F11_Click(sender, e);
                    break;
                case Keys.F12:
                    F12_Click(sender, e);
                    break;
                default:
                    break;
            }
            // Keep a record of the KeyEventArgs object.
            m_KeyEventArgs = e;
        }

        /// <summary>
        /// Event handler for the <c>KeyUp</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormPTU_KeyUp(object sender, KeyEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Update the KeyEventArgs object.
            m_KeyEventArgs = null;
        }
        #endregion - Key Events -

        #region - [Escape] -
        /// <summary>
        /// Event handler for the 'Escape' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void Escape_Click(object sender, EventArgs e)
        {

        }
        #endregion [Escape]

        #region - [F1-Help] -
        /// <summary>
        /// Event handler for the 'F1-Help' key.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void F1_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F1.Enabled == false)
            {
                return;
            }

            F1.Checked = true;

            // Determine the filename of the help document associated with this project.
            string filenameSoftwareUserManual = string.Empty;
            switch (Parameter.ProjectInformation.ProjectIdentifier)
            {
                case CommonConstants.ProjectIdNYCT:
                    filenameSoftwareUserManual = Resources.FilenameSoftwareUserManualPTE;
                    break;
                default:
                    filenameSoftwareUserManual = Resources.FilenameSoftwareUserManualPTU;
                    break;
            }

            // Check that the help document exists.
            string fullyQualifiedFilenameSoftwareUserManual =   DirectoryManager.PathPTUApplicationStartup + Resources.PathRelativeDocumentation +
                                                                CommonConstants.BindingFilename + Parameter.ProjectInformation.ProjectIdentifier + CommonConstants.Period +
                                                                filenameSoftwareUserManual;

            FileInfo helpDocumentFileInfo = new FileInfo(fullyQualifiedFilenameSoftwareUserManual);
            if (helpDocumentFileInfo.Exists)
            {

                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = Settings.Default.PathBrowser;
                processStartInfo.Arguments = fullyQualifiedFilenameSoftwareUserManual;

                //Call the Process.Start method to open the PDF file in the specified browser.
                try
                {
                    System.Diagnostics.Process.Start(processStartInfo);
                }
                catch (Exception)
                {
                    MessageBox.Show(Resources.MBTHelpFail, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    F1.Checked = false;
                }
            }
            else
            {
                MessageBox.Show(Resources.MBTHelpFileNotFound, Resources.MBCaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                F1.Checked = false;
            }
        }
        #endregion - [F1-Help] -

        #region - [F2-Print] -
        /// <summary>
        /// Event handler for the 'F2-Print Screen' key. Captures the PTU application window and saves the image to the specified file.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F2_Click(object sender, EventArgs e)
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

            ImageFormat imageFormat;
            DateTime createdTime = DateTime.Now;
            string extension = CommonConstants.Period + m_ScreenCaptureType.ToString() + CommonConstants.ExtensionJPeg;
            string defaultFilename = General.DeriveName(FileHeader.HeaderCurrent.TargetConfiguration.CarIdentifier, createdTime, extension, string.Empty);
            string fullFilename = General.FileDialogSaveImageFile(defaultFilename, InitialDirectory.ScreenCaptureFilesWrite, out imageFormat);
            if (fullFilename != string.Empty)
            {
                FileInfo fileInfo = new FileInfo(fullFilename);

                // Update the initial directory with the path of the selected file.
                InitialDirectory.ScreenCaptureFilesWrite = fileInfo.Directory.ToString();
                this.Refresh();
                Thread.Sleep(SleepCapture);

                // Copy the PTU window to to the specified file using the ScreenCapture class.
                ScreenCapture screenCapture = new ScreenCapture();
                screenCapture.CaptureWindowToFile(this.MdiParent.Handle, fullFilename, imageFormat);
            }

            Cursor = Cursors.Default;
        }
        #endregion - [F2-Print] -

        #region - [F3] -
        /// <summary>
        /// Event handler for the 'F3' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F3_Click(object sender, EventArgs e)
        {
            
        }
        #endregion [F3]

        #region - [F4] -
        /// <summary>
        /// Event handler for the 'F4' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F4_Click(object sender, EventArgs e)
        {
            ;
        }
        #endregion [F4]

        #region - [F5] -
        /// <summary>
        /// Event handler for the 'F5' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F5_Click(object sender, EventArgs e)
        {
            ;
        }
        #endregion [F5]

        #region - [F6] -
        /// <summary>
        /// Event handler for the 'F6' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F6_Click(object sender, EventArgs e)
        {
            ;
        }
        #endregion [F6]

        #region - [F7] -
        /// <summary>
        /// Event handler for the 'F7' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F7_Click(object sender, EventArgs e)
        {
            ;
        }
        #endregion [F7]

        #region - [F8] -
        /// <summary>
        /// Event handler for the 'F8' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F8_Click(object sender, EventArgs e)
        {
            ;
        }
        #endregion [F8]

        #region - [F9] -
        /// <summary>
        /// Event handler for the 'F9' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F9_Click(object sender, EventArgs e)
        {
            ;
        }
        #endregion [F9]

        #region - [F10] -
        /// <summary>
        /// Event handler for the 'F10' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F10_Click(object sender, EventArgs e)
        {
            ;
        }
        #endregion [F10]

        #region - [F11] -
        /// <summary>
        /// Event handler for the 'F11' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F11_Click(object sender, EventArgs e)
        {
            ;
        }
        #endregion [F11]

        #region - [F12] -
        /// <summary>
        /// Event handler for the 'F12' key. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void F12_Click(object sender, EventArgs e)
        {
            ;
        }
        #endregion - [F12] -

        /// <summary>
        /// Change the font of this form and all controls associated with the form to the same font as the main window.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        public void ChangeFont(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Change the font associated with various controls associated with this form.
            Font = MainWindow.Font;
            m_ToolStripFunctionKeysPTU.Font = Font;
            m_ToolStripComboBox1.Font = Font;
        }

        /// <summary>
        /// Event handler for the <c>SelectedIndexChanged</c> event associated with the <c>ToolStripComboBox</c> control. The actions are defined in the child class.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected virtual void m_ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Close the form cleanly. Simulates the user pressing the Exit button.
        /// </summary>
        public virtual void Exit()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            this.SuspendLayout();

            // Ensure that the Leave event of any user control is processed as soon as the button is pressed.
            Focus();

            if (MainWindow != null)
            {
                // Restore the function keys.
                MainWindow.ToolStripFunctionKeys.Items.Clear();
                MainWindow.ToolStripFunctionKeys.Items.AddRange(MainWindow.ToolStripItemCollectionMainWindow);
                MainWindow.ToolStripFunctionKeys.Update();

                MainWindow.KeyPreview = true;
            }

            RestoreMenuEnabled();
            CloseShowFlags();
            this.PerformLayout();
            Close();
        }

        /// <summary>
        /// Close any FormShowFlags forms that are currently open. The FormShowFlags form displays the state of the individual bits associated with a bit mask variable .
        /// </summary>
        protected void CloseShowFlags()
        {
            for (int formIndex = 0; formIndex < OpenedDialogBoxList.Count; formIndex++)
            {
                OpenedDialogBoxList[formIndex].Dispose();
            }
        }

        /// <summary>
        /// Displays the specified function key and associated separator and sets the Text and Image properties of the key.
        /// </summary>
        /// <remarks>For this method to work correctly, the <c>Tag</c> field of the function key must contain the function key reference i.e. Esc, F1, F2 etc and 
        /// the control name of the associated separator must be derived using this e.g. m_TSSeparatorF1 etc.</remarks>
        /// <param name="functionKey">The function key to be displayed.</param>
        /// <param name="text">The text that is to appear on the function key.</param>
        /// <param name="toolTip">The tool-tip associated with the function key.</param>
        /// <param name="image">The image that is to appear on the function key.</param>
        protected void DisplayFunctionKey(ToolStripButton functionKey, string text, string toolTip, Image image)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            functionKey.Text = functionKey.Tag as string + "-" + text;

            if (toolTip != string.Empty)
            {
                functionKey.ToolTipText = functionKey.Tag as string + " - " + toolTip;
            }
            else
            {
                functionKey.ToolTipText = string.Empty;
            }

            functionKey.Image = image;
            functionKey.Visible = true;

            // Make the associated separator visible. Note: The Tag field of the function key contains the function key reference i.e. Esc, F1, F2 etc and is used to 
            // determine the name of the associated separator control.
            string separatorName = KeyToolStripSeparator + functionKey.Tag as string;
            int index = m_ToolStripFunctionKeysPTU.Items.IndexOfKey(separatorName);

            // For some reason the control m_TSSeparatorEsc cannot be found .
            // Only access the separator control if it was found.
            if (index != CommonConstants.NotFound)
            {
                ToolStripItem toolStripItem = m_ToolStripFunctionKeysPTU.Items[index];
                toolStripItem.Visible = true;
            }
        }

        /// <summary>
        /// Displays the specified label and associated legend and sets the background colour of the label.
        /// </summary>
        /// <param name="label">The label that is to be displayed.</param>
        /// <param name="legendText">The legend text that is to appear to the left of the label.</param>
        /// <param name="color">The background colour of the label.</param>
        protected void DisplayLabel(Label label, string legendText, Color color)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            label.BackColor = color;
            label.Visible = true;

            // Write the legend text in the assocoated label.
            string legendName = LegendName + label.Tag as string;
            int index = m_TableLayoutPanelInformationLabels.Controls.IndexOfKey(legendName);
            Label legend = m_TableLayoutPanelInformationLabels.Controls[index] as Label;
            Debug.Assert(legend != null);
            legend.Text = legendText;
            legend.Visible = true;
        }

        /// <summary>
        /// Restore the <c>Enabled</c> property of all of the menu options that have been modified by the child form to their original state.
        /// </summary>
        protected void RestoreMenuEnabled()
        {
            // Must restore in reverse order is case a menu option has been modified more than once, otherwise the final result will be incorrect.
            for (int index = m_ToolStripMenuItemEnabledList.Count; index > 0; index--)
            {
                ToolStripMenuItem toolStripMenuItem = General.GetToolStripMenuItem(MainWindow.MenuStrip, m_ToolStripMenuItemEnabledList[index - 1].Key);
                toolStripMenuItem.Enabled = m_ToolStripMenuItemEnabledList[index - 1].EnabledCurrent;
            }
        }

        /// <summary>
        /// Set the <c>Enabled</c> property of the specified menu option to the specified/new state and keep a record of the original/current state of the property 
        /// so that the property can be restored to its original state when the child form is disposed of.
        /// </summary>
        /// <param name="key">The key text string associated with the menu option that is to be modified.</param>
        /// <param name="enabled">The state to which the <c>Enabled</c> property is to be set.</param>
        protected void SetMenuEnabled(string key, bool enabled)
        {
            Debug.Assert(MainWindow != null, "FormPTU.ModifyMenuOptionEnabled() - [MainWindow != null]");

            ToolStripMenuItem toolStripMenuItem = General.GetToolStripMenuItem(MainWindow.MenuStrip, key);
            if (toolStripMenuItem != null)
            {
                ToolStripMenuItemEnabled_t toolStripMenuItemEnabled = new ToolStripMenuItemEnabled_t();
                toolStripMenuItemEnabled.Key = key;
                toolStripMenuItemEnabled.EnabledCurrent = toolStripMenuItem.Enabled;
                toolStripMenuItemEnabled.EnabledNew = enabled;
                m_ToolStripMenuItemEnabledList.Add(toolStripMenuItemEnabled);

                // Now set the Enabled property of the specified menu option to the specified state.
                toolStripMenuItem.Enabled = enabled;
            }
        }

        /// <summary>
        /// Process any form specific changes to the main menu resulting from a change in the security level of the user.
        /// </summary>
        /// <param name="security">Reference to the security class.</param>
        public virtual void UpdateMenu(Security security)
        {
            // Apply the form specific menu changes that have been applied since the form was instantiated.
            for (int index = 0; index < m_ToolStripMenuItemEnabledList.Count; index++)
            {
                ToolStripMenuItem toolStripMenuItem = General.GetToolStripMenuItem(MainWindow.MenuStrip, m_ToolStripMenuItemEnabledList[index].Key);
                toolStripMenuItem.Enabled = m_ToolStripMenuItemEnabledList[index].EnabledNew;
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        #region - [IsDisposed] -
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
        #endregion - [IsDisposed] -

        #region - [MainWindow] -
        /// <summary>
        /// Gets or sets the reference to the main application window.
        /// </summary>
        public IMainWindow MainWindow
        {
            get { return m_MainWindow; }
            set { m_MainWindow = value; }
        }
        #endregion - [MainWindow] -

        #region - [Function Keys] -
        #region -[Escape]-
        /// <summary>
        /// Gets the reference to the escape key.
        /// </summary>
        protected ToolStripButton Escape
        {
            get { return m_TSBEsc; }
        }
        #endregion -[Escape]-

        #region -[F1]-
        /// <summary>
        /// Gets the reference to the F1 function key.
        /// </summary>
        protected ToolStripButton F1
        {
            get 
            { 
                return m_TSBF1; 
            }
        }
        #endregion -[F1]-

        #region -[F2]-
        /// <summary>
        /// Gets the reference to the F2 function key.
        /// </summary>
        protected ToolStripButton F2
        {
            get { return m_TSBF2; }
        }
        #endregion -[F2]-

        #region -[F3]-
        /// <summary>
        /// Gets the reference to the F3 function key.
        /// </summary>
        protected ToolStripButton F3
        {
            get { return m_TSBF3; }
        }
        #endregion -[F3]-

        #region -[F4]-
        /// <summary>
        /// Gets the reference to the F4 function key.
        /// </summary>
        protected ToolStripButton F4
        {
            get { return m_TSBF4; }
        }
        #endregion -[F4]-

        #region -[F5]-
        /// <summary>
        /// Gets the reference to the F5 function key.
        /// </summary>
        protected ToolStripButton F5
        {
            get { return m_TSBF5; }
        }
        #endregion -[F5]-

        #region -[F6]-
        /// <summary>
        /// Gets the reference to the F6 function key.
        /// </summary>
        protected ToolStripButton F6
        {
            get { return m_TSBF6; }
        }
        #endregion -[F6]-

        #region -[F7]-
        /// <summary>
        /// Gets the reference to the F7 function key.
        /// </summary>
        protected ToolStripButton F7
        {
            get { return m_TSBF7; }
        }
        #endregion -[F7]-

        #region -[F8]-
        /// <summary>
        /// Gets the reference to the F8 function key.
        /// </summary>
        protected ToolStripButton F8
        {
            get { return m_TSBF8; }
        }
        #endregion -[F8]-

        #region -[F9]-
        /// <summary>
        /// Gets the reference to the F9 function key.
        /// </summary>
        protected ToolStripButton F9
        {
            get { return m_TSBF9; }
        }
        #endregion -[F9]-

        #region -[F10]-
        /// <summary>
        /// Gets the reference to the F10 function key.
        /// </summary>
        protected ToolStripButton F10
        {
            get { return m_TSBF10; }
        }
        #endregion -[F10]-

        #region -[F11]-
        /// <summary>
        /// Gets the reference to the F11 function key.
        /// </summary>
        protected ToolStripButton F11
        {
            get { return m_TSBF11; }
        }
        #endregion -[F11]-

        #region -[F12]-
        /// <summary>
        /// Gets the reference to the F12 function key.
        /// </summary>
        protected ToolStripButton F12
        {
            get { return m_TSBF12; }
        }
        #endregion -[F12]-
        #endregion - [Function Keys]-

        #region - [Labels/Legends] -
        /// <summary>
        /// Gets the <c>Label</c> associated with the legend for InformationLabel 1.
        /// </summary>
        protected Label Legend1
        {
            get { return m_Legend1; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with the legend for InformationLabel 2.
        /// </summary>
        protected Label Legend2
        {
            get { return m_Legend2; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with the legend for InformationLabel 3.
        /// </summary>
        protected Label Legend3
        {
            get { return m_Legend3; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with the legend for InformationLabel 4.
        /// </summary>
        protected Label Legend4
        {
            get { return m_Legend4; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with the legend for InformationLabel 5.
        /// </summary>
        protected Label Legend5
        {
            get { return m_Legend5; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with the legend for InformationLabel 6.
        /// </summary>
        protected Label Legend6
        {
            get { return m_Legend6; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with InformationLabel 1.
        /// </summary>
        protected Label InformationLabel1
        {
            get { return m_Label1; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with InformationLabel 2.
        /// </summary>
        protected Label InformationLabel2
        {
            get { return m_Label2; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with InformationLabel 3.
        /// </summary>
        protected Label InformationLabel3
        {
            get { return m_Label3; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with InformationLabel 4.
        /// </summary>
        protected Label InformationLabel4
        {
            get { return m_Label4; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with InformationLabel 5.
        /// </summary>
        protected Label InformationLabel5
        {
            get { return m_Label5; }
        }

        /// <summary>
        /// Gets the <c>Label</c> associated with InformationLabel 6.
        /// </summary>
        protected Label InformationLabel6
        {
            get { return m_Label6; }
        }
        #endregion - [Labels/Legends] -

        #region - [ScreenCaptureType] -
        /// <summary>
        /// Gets or sets the type of display that is being captured to disk.
        /// </summary>
        protected ScreenCaptureType ScreenCaptureType
        {
            get { return m_ScreenCaptureType; }
            set { m_ScreenCaptureType = value; }
        }
        #endregion - [ScreenCaptureType] -

        #region - [LastKeyEventArgs] -
        /// <summary>
        /// Gets the the <c>KeyEventArgs</c> object associated with the last recorded key press.
        /// </summary>
        protected KeyEventArgs LastKeyEventArgs
        {
            get { return m_KeyEventArgs; }
        }
        #endregion - [LastKeyEventArgs] -

        #region - [Security]-
        /// <summary>
        /// Gets the reference to the system security class.
        /// </summary>
        protected Security Security
        {
            get { return m_Security; }
        }
        #endregion - [Security] -

        #region - [CalledFrom] -
        /// <summary>
        /// Gets or sets the reference to the form that called this form.
        /// </summary>
        /// <remarks>This form is not usually called by the multiple document interface parent form but by the form used to view the Y value agains time plot
        /// of the data contained withing the historic data manager class.</remarks>
        public Form CalledFrom
        {
            get { return m_CalledFrom; }
            set { m_CalledFrom = value;}
        }
        #endregion - [CalledFrom] -

        #region - [ToolStripItemCollectionCalledFrom] -
        /// <summary>
        /// Gets the collection of tool strip items associated with the calling form.
        /// </summary>
        public ToolStripItemCollection ToolStripItemCollectionCalledFrom
        {
            get { return m_ToolStripItemCollectionCalledFrom; }
        }
        #endregion - [ToolStripItemCollectionCalledFrom] -

        #region - [OpenedDialogBoxList] -
        /// <summary>
        /// Gets or sets the list of any dialog boxes associated with this form that remain open.
        /// </summary>
        public List<Form> OpenedDialogBoxList
        {
            get { return m_OpenedDialogBoxList;}
            set { m_OpenedDialogBoxList = value; }
        }
        #endregion - [OpenedDialogBoxList] -
        #endregion --- Properties ---
    }
}
