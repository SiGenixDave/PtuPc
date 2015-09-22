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
 *  Project:    Watch
 * 
 *  File name:  FormViewWatch.cs
 * 
 *  Revision History
 *  ----------------
 *
 */

#region - [1.0 - 1.11]
/*
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/25/10    1.1     K.McD           1.  Added the event handler for the PauseChanged event.
 *                                      2.  Pause property now inherited from the FormPTUDialog class - removed the m_Pause member variable.
 *                                      3.  Added a number of calls to the ModifyMenuOptionEnabled() method to modify the enabled property of a number
 *                                          of menu options.
 *                                      4.  Updated the WatchControl.InvalidValue property to according to the communication status.
 *                                      5.  Removed the event handler associated with non-fatal communication faults.
 * 
 *  08/30/10    1.2     K.McD           1.  Replaced the ComboBox used to select the workset with the ToolStripComboBox inherited from the FormPTU
 *                                          class.
 * 
 *  09/29/10    1.3     K.McD           1.  Configured the CalledFrom property for the form which adds user comments in the SaveSimulatedFaultLog()
 *                                          method.
 * 
 *  10/04/10    1.4     K.McD           1.  Bug fix SNCR001.21. Included support for the <IPollTarget> interface.
 *                                      2.  Bug fix SNCR001.14. Ensure that all forms used to view the status of individual bitmask bits are closed when
 *                                          the user changes the active workset.
 *                                      3.  Bug fix SNCR001.23. Ensure that the 'Configure/System Information' menu option is disabled while data is
 *                                          being recorded.
 * 
 *  10/15/10    1.5     K.McD           1.  Modified to support the CommunicationWatch class rather than ICommunication.
 * 
 *  10/25/10    1.6     K.McD           1.  Added support for the static ActiveWorksetModified event associated with the WorksetManager class.
 * 
 *  10/29/10    1.7     K.McD           1.  Added support for the 'Edit' key.
 *                                      2.  Replaced CommunicationWatch with ICommunicationWatch.
 *                                      3.  No longer disables the 'Configure/Worksets' menu option.
 * 
 *  11/15/10    1.8     K.McD           1.  Renamed a number of variables and XML tags - no functional changes.
 *                                      2.  Now uses a data stream member variable to generate and store the simulated fault log.
 *                                      3.  Changes resulting from changes to the watch file structure.
 *                                      4.  Disable the edit workset function key while data is being recorded.
 *                                      5.  Use the checked property of the function keys, where possible, to give operator feedback.
 *                                      6.  Disable the edit workset function key if the baseline workset is the active workset.
 * 
 *  12/01/10    1.9     K.McD           1.  Bug fix - SNCR001.61. Modified the F3_Click() event handler so that the initialization of the watch file is
 *                                          now carried out before setting up the function key properties.
 * 
 *                                      2.  Modification resulting from the the renaming of a number of external constants and properties.
 * 
 *  01/06/10    1.10    K.McD           1.  Added a number of constants.
 *                                      2.  Included the MutexCommunicationInterface property to help control access to the communication interface.
 *                                      3.  Implemented the ComboBoxUpdateWorksets() method to update the worksets associated with a specified
 *                                          ToolStripComboBox control.
 *                                      4.  Modifications resulting from the name change of the ActiveWorksetModified event to
 *                                          WorksetCollectionModified.
 *                                      5.  Polling of the target is now controlled using the Pause property rather than calls to StartPolling() and
 *                                          StopPolling().
 *                                      6.  Now updates the Items property of the ToolStripComboBox control with the updated worksets if a
 *                                          WorksetCollectionModified event is raised.
 * 
 *  01/12/11    1.1     K.McD           1.  Bug fix SNCR001.84. Added the second parameter to the call to the System.Threading.WaitHandle.WaitOne()
 *                                          method, as advised by the following blog entries: 
 *                                              (1) http://www.mikeplate.com/missingmethodexception-and-waitone/ and (2) 
 *                                              (2) http://blog.darrenstokes.com/2009/03/30/watch-out-for-those-waitone-overloads-when-you-need-backwards-compatibility
 * 
 *  01/26/11    1.2     K.McD           1.  Auto-modified as a result of the property name changes associated with the Parameter class.
 * 
 *  01/31/11    1.3     K.McD           1.  Modified the record function key event handler to ensure that the function key is checked as soon as it is
 *                                          pressed.
 * 
 *                                      2.  Modified to accommodate the communication mutex introduced into the Common.CommunicationParent class in
 *                                          version 1.11 of Common.dll.
 * 
 *  02/03/11    1.4     K.McD           1.  Standardized the function key event handlers to: display the wait cursor, enable the Checked property of the
 *                                          function key and clear any status message.
 *                                      2.  Modified the Exit() method to check whether recording is in progress and, if so, simulate the user pressing
 *                                          the F3-Record key to stop recording before closing the form.
 *                                      3.  Modified the F3-Record function key event handler so that the escape key is no longer disabled while in
 *                                          record mode.
 *                                      4.  Modified the F3-Record function key event handler so that the user does not have access to any of the main
 *                                          menu options while in record mode.
 * 
 *  02/14/11    1.5     K.McD           1.  Modifications resulting from the name change to SetMenuEnabled() in the parent class.
 *                                      2.  Removed any code that changed the value of the MainWindow.TaskProgressBar.Visible property and this progress
 *                                          bar is now permanently visible.
 *                                      3.  Modified the LoadWorkset() method to use the inherited member variable used to store the selected workset.
 *                                      4.  Moved the check to determine if the current workset is the baseline workset from the ComboBox
 *                                          SelectedIndexChanged event  handler to the LoadWorkset() method.
 * 
 *  02/21/11    1.6     K.McD           1.  Modified the F3_Click() and SaveSimulatedFaultLog() methods to included the name of the workset in the
 *                                          default filename.
 * 
 *  02/27/11    1.7     K.McD           1.  Renamed this form.
 *                                      2.  Auto-modified as a result of the name changes associated with a number of menu access keys.
 *                                      3.  Removed the check to ensure that the MainWindow is defined before calling the
 *                                          MainWindow.WriteStatusMessage() method.
 *                                      4.  Bug fix SNCR001.105. Modified the event handler associated with the F5-Edit function key to check that the
 *                                          user has 
 *                                          sufficient privileges before showing the dialog box that allows the user to edit the current workset.
 *                                      5.  Added the UpdateMenu() method to process any form specific changes to the menu system or function keys
 *                                          resulting from a change in the security level of the user.
 *                                      6.  Modified the class such that the progress bar is only displayed when data is being recorded.
 *                                      7.  Modified the LoadWorkset() method such that the F5-Edit function key is only enabled if the security level
 *                                          allows the user to modify the current workset.
 *                                      8.  Removed the reference to the 'Watch/Replace Watch' menu option as this is no longer used.
 * 
 *  03/17/11    1.8     K.McD           1.  Auto-modified as a result of property name changes associated with the Common.Security class.
 *                                      2.  Auto-modified as a result of a method name change in the Common.FormPTU class.
 * 
 *  03/28/11    1.9     K.McD           1.  Renamed a number of variables.
 *                                      2.  Auto-modified as a result of name changes to the ScreenCapture enumerators.
 * 
 *  04/27/11    1.9.1   K.McD           1.  Minor modification to the event handler associated with the ComboBox control SelectedIndexChanged event to
 *                                          ensure that focus is moved away from the ComboBox control as soon as possible after the user has selected
 *                                          the new entry in the ComboBox list.
 *                                          
 *  07/07/11    1.9.2   K.McD           1.  Modified the Escape_Click() method to ignore the request if the Enabled property of the Escape
 *                                          ToolStripButton is not aasserted e.g. if the user presses the Esc key while the Escape ToolStripButton is
 *                                          disabled.
 *                                          
 *  07/20/11    1.9.3   K.McD           1.  Modified the signature of the constructor to use the ICommunicationParent interface.
 *                                      2.  Modified the constructor to check the mode of the PTU and instantiate the appropriate ICommunicationWatch
 *                                          interface.
 *                                      
 *  08/04/11    1.9.4   K.McD           1.  Replaced the reference to the Pause property in the PauseFeedback property with a reference to the m_Pause
 *                                          member variable.
 *  
 *  08/10/11    1.10    Sean.D          1.  Added support for offline mode. Modified the constructor to conditionally choose CommunicationWatch or
 *                                          CommunicationWatchOffline.
 *                                      2.  Very minor, added mention of F5-Edit to comments in the constructor.
 *                                      
 *  08/22/11    1.10.1  K.McD           1.  Added a number of constants and renamed a number of variables.
 *                                      2.  Modified the SetPauseAndWait() method to include a timeout.
 *                                      3.  Included support to monitor the watchdog associated with the thread that is responsible for communication
 *                                          with the target.
 *                                      4.  Included checks on the success of the call to the IPollTarget.SetPauseAndWait() method in the
 *                                          m_ToolStripComboBox1_SelectedIndexChanged() and the WorksetManager_WorksetCollectionModified() methods.
 *                                      5.  Changed the order of the PortLocked and ReadTimeout checks in the DisplayUpdate() method.
 *                                      
 *  09/30/11    1.10.2  K.McD           1.  Added a call to the FormPTU.CloseFlags() method to the Exit() method as this call was removed from the
 *                                          Cleanup() method in the FormPTU class.
 *                                          
 *                                      2.  Refactored the implementation of the IPollTarget and ICommunicationInterface interfaces.
 *
 *  10/19/11    1.10.3  Sean.D			1.	Modified FormViewWatch_Shown() to ensure that a valid workset exists prior to trying to select the first
 *                                          item in the combo box.
 *										2.	Added a check in ComboBoxUpdateWorksets() as to whether a given workset has a valid number of watch
 *										    variables. If no valid worksets exist, it tries to create a new baseline with the correct number of
 *										    variables.
 *                                      
 *  10/19/11    1.10.4  K.McD           1.  Added an event handler for the Click event associated with the TabPage control to move focus away from the
 *                                          currently selected user control.
 *                                          
 *                                      2.  SNCR002.41. Added checks to the event handlers associated with all ToolStripButton controls to ensure that
 *                                          the event handler code is ignored if the Enabled property of the control is not asserted.
 *                                          
 *                                      3.  Replaced text strings with resource references and added icons etc to MessageBox.Show() calls.
 *                                      4.  Initialized the postfixNumber to the current WatchSize in the ComboBoxUpdateWorksets() method rather than 2.
 *                                      
 *  11/23/11    1.10.4  K.McD           1.  Ensured that all event handler methods were detached.
 *                                      2.  Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after the
 *                                          Close() method had been called.
 *                                          
 *  07/26/13    1.11    K.McD           1.  Modified the Exit() method to close the communication port and to set the mode to configuration mode if
 *                                          there is a communication fault or the watchdog has tripped i.e. the port is locked.
 */
#endregion - [1.0 - 1.11] -

#region - [1.12] -
/*                                            
 *  03/11/15    1.12   K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order TBD.
 *   
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                          Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                              1.  MOC-0171-30. The ‘F4 – Rec.’ Function key will toggle between the industry standard icons
 *                                                  representing stop and start recording.
 *                                              
 *                                  Modifications
 *                                  1.  Modified the event handler for the [F2-Print] key so that it no longer clears the status message box.
 *                                      The [F2-Print] function key is only enabled when the user selects the [F4-Pause] function key to pause the
 *                                      display update. On entering the pause state, the status message box informs the user that it is in this state
 *                                      by displaying 'Paused'. If the [F2-Print] function key was to clear the status message box, the user may 
 *                                      forget that the PTU is in the paused state on completion of the window capture.
 *                                      
 *                                  2.  Modified the [F4-Pause] event handler so that the ToolStrip Function Key Text, ToolTip and Image toggles 
 *                                      between 'F4-Pause | F4 - [Pause] | Pause.png' and 'F4-Cont. | F4 - [Continue] | Play.png'. Also disabled the
 *                                      [F5-Modify], [Modify Current Workset], key, updated the handler so that some main menu options are now disabled
 *                                      in pause mode and, ensured that the [Esc], [Close Window] key is no longer disabled in pause mode. 
 *                                      
 *                                      As the user can now press the [Esc] button while the display is paused, modified the Exit() method to 
 *                                      simulate a key press of the [F4-Pause] button to ensure that the main menu etc is re-enabled on exit.
 *                                     
 *                                  3.  Modified the [F3-Rec.] event handler so that the ToolStrip Function Key Text, ToolTip and Image toggles 
 *                                      between 'F3-Rec | F3 - [Record] | Record.png' and 'F4-Stop | F4 - [Stop Recording] |  RecordStop.png'. Also
 *                                      now disables only some of the main menu options while data is being recorded.
 *                                  
 */
#endregion - [1.12] -

#region - [1.13] -
/*
 *  04/16/15    1.13    K.McD       References
 *              
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *                                          
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, Kawasaki Rail Car and NYTC on
 *                                          12th April 2013 - MOC-0171:
 *                                              
 *                                          1.  MOC-0171-06. All references to 'Fault Logs', including menu options and directory names to be replaced by 'Data Streams'
 *                                              for all projects.
 *
 *                                      2.  NK-U-6505 Section 5.1.3. Log File Naming. The filename of all event logs, fault logs, real time data etc will be
 *                                              modified to meet the KRC specification. This modification will be specific to the KRC project; for all other projects,
 *                                              the current naming convention will still apply.
 *                                              
 *                                      3.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                          labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                          ‘Wibu-Key: [Present | Not Present]’.
 *                                          
 *                                  2.  SNCR - R188 PTU [20 Mar 2015] - Item 10. Bug associated with closing of the FormShowFlags dialog boxes.
 *                                      
 *                                  Modifications
 *                                  1.  Auto-update following renaming of the FileHandling.LogType.SimulatedDataStream enumerator. - Ref.: 1.1.1, 1.2.
 *                                  2.  Removed the call to CloseShowFlags() in the Exit() method as this is now included in base.Exit(). - Ref.: 2.
 *                                  3.  In order to make room for the additional status labels mentioned in 1.2, above, the progress of the recording is now
 *                                      shown using the parent ProgressBar control rather than the MainWindow.TaskProgressBar. - Ref.: 1.3.
 */
#endregion - [1.13] -

#region - [1.14] -
/*
 *  07/13/15    1.14    K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *                                      
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                          Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                          
 *                                          1.  MOC-0171-29/34. Kawasaki requested that any function that is not available to the current mode, either Maintenance
 *                                              or Administrative, should not be displayed, or be 'greyed out'. BTPC agreed to review the software to ensure that
 *                                              non-active menu items are ‘greyed out’ or not shown.
 *                                              
 *                                              As a result of a further review, it is proposed that the following modifications are carried out:
 *                                              
 *                                                  1.  The 'Select Data Dictionary' menu option should only visible when logged in as a BT engineer (Factory).
 *                                                      Also when visible, it should only be enabled when not in onlide, offline or self test mode.
 *                                                      
 *                                                  2.  When in Self Test mode only the 'File/Exit', 'Help' and 'Login' main menu options should be enabled.
 *                                                  
 *                                                  3.  When displaying event logs only the File/Exit', 'Configure/Worksets/Data Stream', 'Configure/Enumeration',
 *                                                      'Help', and 'Login' main menu options should be enabled.
 *                                                          
 *                                                  4.  When displaying the Watch Window only the 'File/Exit', 'Configure/Worksets/Data Stream', 'Configure/Enumeration',
 *                                                      'Help', and 'Login' main menu options should be enabled.
 *                                      
 *                                  Modifications
 *                                  1.  Added calls to the SetMenuEnabled() method in the Shown event handler to set the Enabled properties of the menu options to those
 *                                      defined by 1.1.1.4.
 *                                      
 *                                  2.  Removed the SetMenuEnabled() calls associated with the 'Pause' and 'Record' function keys and these menu options are now
 *                                      already disabled whenever the window is shown.
 */
#endregion - [1.14] -

#region - [1.15] -
/*
 *  07/28/15    1.15    K.McD       References
 *                                  1.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 23. In the Rev. 1.25 implementation, if the ‘F5-Modify’ function key is disabled,
 *                                      because, for example, the user is in Maintenance mode and the active workset is at the Engineering security level, and the
 *                                      user selects either the ‘F4-Pause’ or the ‘F3-Rec’ function key; the ‘F5-Modify’ function key remains disabled while the
 *                                      PTU is in the record or the paused state, however, on returning to the normal state, it becomes active. This is incorrect
 *                                      and should be corrected in the next revision.
 *                                      
 *                                  2.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 24. On selecting the ‘Exit’ function key on the ‘Diagnostics/Event Log’ and
 *                                      the ‘Diagnostics/Self Tests’ screens, the cursor doesn’t go to the Cursors.WaitCursor cursor on the R188 project.
 *  
 *                                  Modifications
 *                                  1.  Modified the ‘F4-Pause’ and ‘F3-Record’ function key handlers to only enable the ‘F5-Modify’ function key on leaving the
 *                                      record or paused state, if the current security level is greater than or equal to the security level of the active workset.
 *                                      - Ref.: 1.
 *                                     
 *                                  2.  Modified the Exit() method to use ‘Cursor.Current = Cursors.WaitCursor’ rather than ‘this.Cursor = Cursors.WaitCursor’.
 *                                      - Ref.: 2.
 * 
 */
#endregion - [1.15] -

#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Common;
using Common.Communication;
using Common.Configuration;
using Common.Forms;
using Common.UserControls;
using Watch.Communication;
using Watch.Properties;

namespace Watch.Forms
{
    /// <summary>
    /// Form to view and record the watch variables defined by the <c>WorksetManager</c> class.
    /// </summary>
    public partial class FormViewWatch : FormWatch, IPollTarget, ICommunicationInterface<ICommunicationWatch>
    {
        #region --- Interfaces ---
        #region - [ICommunicationInterface<>] -
        #region - [Member Variables ] -
        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        private ICommunicationWatch m_CommunicationInterface;
        #endregion - [Member Variables ] -

        #region - [Properties] -
        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the target.
        /// </summary>
        public ICommunicationWatch CommunicationInterface
        {
            get { return m_CommunicationInterface; }
            set { m_CommunicationInterface = value; }
        }
        #endregion - [Properties] -
        #endregion - [ICommunicationInterface<>] -

        #region - [IPollTarget] -
        #region - [Member Variables] -
        /// <summary>
        /// A flag to control the display update. True, stops the display update i.e pauses the display; false, re-starts the display update.
        /// </summary>
        private bool m_Pause = false;

        /// <summary>
        /// Reference to the class responsible for polling the target hardware and recording the watch values.
        /// </summary>
        private ThreadPollWatch m_ThreadPollWatch;
        #endregion - [Member Variables] -

        #region - [Methods] -
        /// <summary>
        /// Start polling the target hardware.
        /// </summary>
        public void StartPolling()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ThreadPollWatch = new ThreadPollWatch(CommunicationInterface, CyclicQueueSizeRecord, CyclicQueueSizeSimulatedFaultLog, this);
            ThreadPollWatch.Start();
        }

        /// <summary>
        /// Stop polling the target hardware. 
        /// </summary>
        /// <remarks>Ignores the request if the class used to poll the target hardware is null.</remarks>
        public void StopPolling()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (ThreadPollWatch != null)
            {
                ThreadPollWatch.Dispose();
            }
        }

        /// <summary>
        /// Set the Pause property and wait until the feedback signal is received or until the timeout has elapsed.
        /// </summary>
        /// <param name="timeoutMs">The timeout period, in ms.</param>
        /// <returns>A flag to indicate whether the pause feedback signal was asserted within the specified timeout. True, if the pause feedback signal was asserted 
        /// within the specified timeout; otherwise, false.</returns>
        public bool SetPauseAndWait(int timeoutMs)
        {
            // Return true if the thread is not yet instantiated.
            bool pauseFeedbackAsserted = true;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return pauseFeedbackAsserted;
            }

            // Skip, if the ThreadPollWatch class has not been instantiated.
            if (ThreadPollWatch == null)
            {
                return pauseFeedbackAsserted;
            }

            Pause = true;

            // Wait until the feedback signal is asserted or for timeout.
            DateTime startTime = DateTime.Now;
            while (PauseFeedback == false && (DateTime.Now < startTime.Add(new TimeSpan(0, 0, 0, 0, timeoutMs))))
            {
                Thread.Sleep(CommonConstants.SleepMsPauseFeedback);
            }

            if (PauseFeedback == false)
            {
                pauseFeedbackAsserted = false;

                // Clear the Pause property if the feedback was not asserted within the timeout period.
                Pause = false;
            }

            return pauseFeedbackAsserted;
        }
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets or sets the flag that controls the polling of the target hardware. True, inhibits polling of the target hardware; otherwise, false,
        /// resumes polling.
        /// </summary>
        public bool Pause
        {
            set
            {
                m_Pause = value;

                // If the ThreadPollWatch class has been instantiated update the Pause property of that class.
                if (ThreadPollWatch != null)
                {
                    ThreadPollWatch.Pause = m_Pause;
                }
            }
            get
            {
                // Check whether the ThreadPollWatch class has been instantiated.
                if (ThreadPollWatch != null)
                {
                    // Yes, return the Pause property of that class.
                    return ThreadPollWatch.Pause;
                }
                else
                {
                    return m_Pause;
                }

            }
        }

        /// <summary>
        /// Gets the feedback flag that indicates whether polling of the target hardware has been suspended.  
        /// </summary>
        /// <remarks>This flag is asserted when the <c>ThreadPollWatch</c> class has entered the pause state, i.e. the current communication request is
        /// complete and no further requests will be issued until the pause property has been cleared.</remarks>
        public bool PauseFeedback
        {
            get
            {
                // If the ThreadPollWatch class has been instantiated return the PauseFeedback property of that class.
                if (ThreadPollWatch != null)
                {
                    return ThreadPollWatch.PauseFeedback;
                }
                else
                {
                    // Just report back the state of the Pause property.
                    return m_Pause;
                }
            }
        }

        /// <summary>
        /// Gets or sets the reference to the class responsible for polling the target hardware and recording the watch values.
        /// </summary>
        private ThreadPollWatch ThreadPollWatch
        {
            get { return m_ThreadPollWatch; }
            set { m_ThreadPollWatch = value; }
        }
        #endregion - [Properties] -
        #endregion - [IPollTarget] -
        #endregion --- Interfaces ---

        #region --- Constants ---
        /// <summary>
        /// The interval, in ms, between successive display updates. Value: 500ms.
        /// </summary>
        private const int IntervalMsDisplayUpdate = 500;

        /// <summary>
        /// The countdown value associated the watchdog trip. Value: 10.
        /// </summary>
        private int WatchdogTripCountdown = 10;
        #endregion --- Constants ---

        #region --- Member Variables ---
        #region - [Display Related] -
        /// <summary>
        /// The System.Windows.Forms timer used to manage the display update.
        /// </summary>
        private System.Windows.Forms.Timer m_TimerDisplayUpdate;
        #endregion - [Display Related] -

        #region - [Communication] -
        /// <summary>
        /// A record of the packet count. Used to determine if polling is active so that the packet received icon can be blinked in a thread-safe way.
        /// </summary>
        private long m_PacketCount;

        #region - [Watchdog] -
        /// <summary>
        /// A record of the watchdog count. Used to determine if the thread on which the polling is carried out has locked.
        /// </summary>
        private int m_Watchdog;

        /// <summary>
        /// A flag that indicates whether a watchdog trip has occurred.
        /// </summary>
        private bool m_WatchdogTrip;

        /// <summary>
        /// The countdown to the watchdog trip.
        /// </summary>
        private int m_WatchdogTripCountdown;
        #endregion - [Watchdog] -

        /// <summary>
        /// A flag that indicates whether a communication fault has been detected.
        /// </summary>
        private bool m_CommunicationFault;
        #endregion - [Communication] -

        #region - [Data Recording Variables] -
        /// <summary>
        /// The timer used to manage the simulated fault log recording.
        /// </summary>
        private System.Windows.Forms.Timer m_TimerSimulatedFaultLog;

        /// <summary>
        /// Flag to control data recording. True, starts recording of the watch variable data; false, stops recording of the data.
        /// </summary>
        protected bool m_Record = false;

        /// <summary>
        /// The time when recording of the watch variable data was started.
        /// </summary>
        private DateTime m_StartTime;

        /// <summary>
        /// The fully qualified file name of the file used to record the watch data, excluding the extension.
        /// </summary>
        private string m_FullyQualifiedFilename;

        /// <summary>
        /// The recorded watch data that is serialized to disk.
        /// </summary>
        private WatchFile_t m_WatchFileRecordedData;

        /// <summary>
        /// The size of the cyclic queue used to store recorded watch data.
        /// </summary>
        private int m_CyclicQueueSizeRecord;

        /// <summary>
        /// The size of the cyclic queue used to store simulated fault log data.
        /// </summary>
        private int m_CyclicQueueSizeSimulatedFaultLog;

        /// <summary>
        /// The data stream associated with a simulated fault log.
        /// </summary>
        private DataStream_t m_DataStreamSimulatedFaultLog;
        #endregion - [Data Recording Variables] -
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor, required for Visual Studio.
        /// </summary>
        public FormViewWatch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form to view the watch variables in real time.
        /// </summary>
        public FormViewWatch(ICommunicationParent communicationInterface)
            : base(Workset.RecordedWatch.ActiveWorkset)
        {
            InitializeComponent();

            // Initialize the communication interface.
            if (communicationInterface is CommunicationParent)
            {
                CommunicationInterface = new CommunicationWatch(communicationInterface);
            }
            else
            {
                CommunicationInterface = new CommunicationWatchOffline(communicationInterface);
            }
            Debug.Assert(CommunicationInterface != null);

            #region - [Function Keys] -
            // Escape - Exit
            // F1 - Help
            // F2 - Print
            // F3 - Record
            // F4 - Pause
            // F5 - Edit
            DisplayFunctionKey(F3, Resources.FunctionKeyTextRecord, Resources.FunctionKeyToolTipRecord, Resources.Record);
            DisplayFunctionKey(F4, Resources.FunctionKeyTextPause, Resources.FunctionKeyToolTipPause, Resources.Pause);
            DisplayFunctionKey(F5, Resources.FunctionKeyTextEdit, Resources.FunctionKeyToolTipEdit, Resources.Modify);

            // - [Print] - Disable printing until the display is paused. 
            F2.Enabled = false;
            #endregion - [Function Keys] -

            #region - [InformationLabels/Legend] -
            // InformationLabel 1  - Date
            // InformationLabel 2  - Time
            // InformationLabel 3  - Rec. Start
            // InformationLabel 4  - Duration
            DisplayLabel(InformationLabel1, Resources.InformationLegendDate, Color.MintCream);
            DisplayLabel(InformationLabel2, Resources.InformationLegendTime, Color.LightSteelBlue);

            // These labels aren't displayed until the user starts recording data.
            DisplayLabel(InformationLabel3, Resources.InformationLegendRecordingStartTime, Color.LightGreen);
            InformationLabel3.Visible = false;
            Legend3.Visible = false;

            DisplayLabel(InformationLabel4, Resources.InformationLegendDuration, Color.Khaki);
            InformationLabel4.Visible = false;
            Legend4.Visible = false;
            #endregion - [InformationLabels/Legend] -

            #region - [ComboBox] -
            m_ToolStripComboBox1.Visible = true;
            m_ToolStripLegendComboBox1.Text = Resources.LegendWorkset;
            m_ToolStripLegendComboBox1.Visible = true;

            ComboBoxUpdateWorksets(ref m_ToolStripComboBox1, Workset.RecordedWatch);
            #endregion - [ComboBox] -

            #region - [Display Timer] -
            m_TimerDisplayUpdate = new System.Windows.Forms.Timer();
            m_TimerDisplayUpdate.Tick += new EventHandler(DisplayUpdate);
            m_TimerDisplayUpdate.Interval = IntervalMsDisplayUpdate;
            m_TimerDisplayUpdate.Enabled = true;
            m_TimerDisplayUpdate.Stop();
            #endregion - [Display Timer] -

            // Initialize the watchdog trip countdown.
            m_WatchdogTripCountdown = WatchdogTripCountdown;

            #region - [Simulated Fault Log] -
            m_TimerSimulatedFaultLog = new System.Windows.Forms.Timer();
            m_TimerSimulatedFaultLog.Tick += new EventHandler(SaveSimulatedFaultLog);
            m_TimerSimulatedFaultLog.Enabled = true;
            m_TimerSimulatedFaultLog.Stop();

            m_DataStreamSimulatedFaultLog = new DataStream_t(LogType.SimulatedDataStream, Parameter.DurationMsSimulatedFaultLog, 
                                                             Parameter.DurationPostTripMsSimulatedFaultLog, Parameter.IntervalWatchMs);

            Debug.Assert(m_DataStreamSimulatedFaultLog.SampleCount > 1, "FormViewWatch.Ctor() - [m_DataStreamSimulatedFaultLog.PointCount > 1]");
            CyclicQueueSizeSimulatedFaultLog = m_DataStreamSimulatedFaultLog.SampleCount;
            #endregion - [Simulated Fault Log] -

            #region - [Recorded Watch Values] -
            // Initialize the structure that saves the watch data to disk.
            m_WatchFileRecordedData = new WatchFile_t();
            m_WatchFileRecordedData.DataStream = new DataStream_t(Parameter.IntervalWatchMs); 

            // Determine the size of the cyclic queue used to store the recorded watch data.
            int pollsPerMinute = (int)CommonConstants.MinuteMs / Parameter.IntervalWatchMs;
            CyclicQueueSizeRecord = pollsPerMinute * Parameter.DurationPageMinutes;
            #endregion - [Recorded Watch Values] -

            // Register the WorksetCollectionModified event handler.
            Workset.RecordedWatch.WorksetCollectionModified += new EventHandler(WorksetManager_WorksetCollectionModified);
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

                    // Detach the event handlers.
                    m_TimerDisplayUpdate.Tick -= new EventHandler(DisplayUpdate);
                    m_TimerSimulatedFaultLog.Tick -= new EventHandler(SaveSimulatedFaultLog);
                    Workset.RecordedWatch.WorksetCollectionModified -= new EventHandler(WorksetManager_WorksetCollectionModified);

                    if (m_TimerDisplayUpdate != null)
                    {
                        m_TimerDisplayUpdate.Dispose();
                    }

                    if (m_TimerSimulatedFaultLog != null)
                    {
                        m_TimerSimulatedFaultLog.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data 
                // members to null.
                m_TimerDisplayUpdate = null;
                m_TimerSimulatedFaultLog = null;
                ThreadPollWatch = null;
                CommunicationInterface = null;

                #region - [Detach the event handler methods.] -
                this.m_TabPage1.Click -= new System.EventHandler(this.TabPage_Click);
                this.Shown -= new System.EventHandler(this.FormViewWatch_Shown);
                this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.FormViewWatch_KeyDown);
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
        /// Event handler for the <c>Shown</c> event. (1) Initialize the progress bar; (2) add the watch controls associated with each workgroup to the 
        /// appropriate <c>TabPage</c> control; (3) show the <c>TabPage</c> corresponding to the active workgroup and (4) start the display update
        /// timer. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FormViewWatch_Shown(object sender, EventArgs e)
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

            // Update the form specific changes to the main menu options.
            SetMenuEnabled(CommonConstants.KeyMenuItemFileOpen, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemView, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemDiagnostics, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureWorksetsFaultLog, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureWorksetsChartRecorder, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureRealTimeClock, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigurePasswordProtection, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureChartRecorder, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemConfigureChartMode, false);
            SetMenuEnabled(CommonConstants.KeyMenuItemTools, false);

            // Initialize the ProgressBar.
            m_ProgressBar.Maximum = CyclicQueueSizeRecord;
            m_ProgressBar.Minimum = 0;
            m_ProgressBar.Value = 0;

            // Ensures that the active workset is selected and displayed by raising a SelectedIndexChanged event.
			if (m_ToolStripComboBox1.Items.Count > 0)
			{
				m_ToolStripComboBox1.Text = ((Workset_t)m_ToolStripComboBox1.Items[0]).Name;
				m_ToolStripComboBox1.Enabled = true;
			}
			else
			{	// This case should not happen, but it's good to do error checking in case something changes.
				m_ToolStripComboBox1.Text = Resources.MBTValidWorksetNotFound;
				m_ToolStripComboBox1.Enabled = false;
			}

            // Only start polling and timer update if the communication interface has been specified.
            if (CommunicationInterface != null)
            {
                StartPolling();
            }

            Cursor = Cursors.Default;
        }
        #endregion - [Form] -

        #region - [Key Events] -
        /// <summary>
        /// Event handler for the KeyDown event. If the space bar has been pressed, initiate fault logging. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormViewWatch_KeyDown(object sender, KeyEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Space:
                    // Simulated fault log trigger event, start the fault log timer.
                    m_TimerSimulatedFaultLog.Stop();
                    m_TimerSimulatedFaultLog.Interval = m_DataStreamSimulatedFaultLog.DurationPostTripMs;
                    m_TimerSimulatedFaultLog.Start();
                    break;
                default:
                    break;
            }
        }
        #endregion - [Key Events] -

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

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            Exit();
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

            // Clear the status message. Removed Rev. 1.12. The [F2-Print] function key is only enabled when the user selects the [F4-Pause] function
            // key to pause the display update. On entering the pause state, the status message box informs the user that it is in this state by
            // displaying 'Paused'. If this function key were to clear the status message box, the user may forget that the PTU is in the paused state
            // on completion of the window capture.

            // MainWindow.WriteStatusMessage(string.Empty);

            ScreenCaptureType = ScreenCaptureType.Watch;
            base.F2_Click(sender, e);

            F2.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F2-Print] -

        #region - [F3-Record] -
        /// <summary>
        /// Event handler for the 'F3-Record' button <c>Click</c> event. Manage all aspects of the user pressing the 'F3-Record' button: (a) update
        /// the text and image; (b) enable/disable any other buttons, as appropriate and (c) assert or clear the recording flag, as required.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F3_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F3.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            if (m_Record == false)
            {
                #region - [WatchFile Initialization] -
                // Check this button.
                F3.Checked = true;

                // Rev.: 1.12. Display 'Stop Recording' ToolStrip Function Key Text, Tooltip and Image.
                DisplayFunctionKey(F3, Resources.FunctionKeyTextStop, Resources.FunctionKeyToolTipStop, Resources.RecordStop);

                DateTime startRecordTime = DateTime.Now;

                // Ask the user to specify a filename.
                m_FullyQualifiedFilename =
                    General.FileDialogSaveRecordedWatchFile(General.DeriveName(FileHeader.HeaderCurrent.TargetConfiguration.CarIdentifier, 
                                                                               startRecordTime, CommonConstants.ExtensionWatchFile, 
                                                                               m_Workset.Name),
                                                            InitialDirectory.WatchFilesWrite);

                // Cancel, if the user did not specify a filename.
                if (m_FullyQualifiedFilename == string.Empty)
                {
                    Cursor = Cursors.Default;

                    // Rev.: 1.12. Show initial ToolStrip Function Key Text, Tooltip and Image.
                    F3.Checked = false;
                    DisplayFunctionKey(F3, Resources.FunctionKeyTextRecord, Resources.FunctionKeyToolTipRecord, Resources.Record);
                    return;
                }

                // Update the initial directory with the path of the selected file.
                InitialDirectory.WatchFilesWrite = Path.GetDirectoryName(m_FullyQualifiedFilename);

                // Show the form to allow the user to add user comments to the header information.
                FormAddComments formAddComments = new FormAddComments(FileHeader.HeaderCurrent, startRecordTime);
                formAddComments.CalledFrom = this;
                formAddComments.ShowDialog();

                // Add the header information to the structure.
                m_WatchFileRecordedData.Header = formAddComments.Header;
                #endregion - [WatchFile Initialization] -

                // Dont allow the user to change the active workset while data is being recorded.
                m_ToolStripComboBox1.Enabled = false;

                #region - [Function Keys] -
                // Disable the pause button.
                F4.Enabled = false;

                // Disable the edit workset key.
                F5.Enabled = false;
                #endregion - [Function Keys] -

                m_Record = true;

                // Keep a record of the start time.
                m_StartTime = DateTime.Now;

                // Label 3 - Rec Start.
                InformationLabel3.Text = m_StartTime.ToString(CommonConstants.FormatStringTimeSec);
            }
            else
            {
                SaveRecordedWatchData(m_FullyQualifiedFilename, m_WatchFileRecordedData);

                // Allow the user to change the active workset.
                m_ToolStripComboBox1.Enabled = true;

                #region - [Function Keys] -
                // Uncheck this button. Rev.: 1.12. Show initial ToolStrip Function Key Text, Tooltip and Image.
                F3.Checked = false;
                DisplayFunctionKey(F3, Resources.FunctionKeyTextRecord, Resources.FunctionKeyToolTipRecord, Resources.Record);

                // Enable the pause button.
                F4.Enabled = true;

                // Enable the edit workset key if the user has sufficient privileges to do so.
                if (Security.SecurityLevelCurrent >= m_Workset.SecurityLevel)
                {
                    F5.Enabled = true;
                }
                #endregion - [Function Keys] -

                m_Record = false;
            }

            ReportRecordingStatus(m_Record);

            // Update the Record property to reflect the current state.
            ThreadPollWatch.Record = m_Record;

            Cursor = Cursors.Default;
        }
        #endregion - [F3-Record] -

        #region - [F4-Pause] -
        /// <summary>
        /// Event handler for the 'F4-Pause' button <c>Click</c> event. Manages all aspects of the user pressing the 'F4-Pause' button: (a) updates
        /// the text and image; (b) enables/disables any other buttons, as appropriate and (c) inhibits or resumes the screen update, as appropriate.
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

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            if (Pause == false)
            {
                // Dont allow the user to change the active workset while the display update is paused.
                m_ToolStripComboBox1.Enabled = false;

                #region - [Function Keys] -
                // Check this button.
                F4.Checked = true;

                // Rev.: 1.12 Display 'Continue' ToolStrip Function Key Text, Tooltip and Image.
                DisplayFunctionKey(F4, Resources.FunctionKeyTextContinue, Resources.FunctionKeyToolTipContinue, Resources.Play);

                // Disable the record button.
                F3.Enabled = false;

                // Disable the edit workset key.
                F5.Enabled = false;

                // Enable the print button.
                F2.Enabled = true;
                #endregion - [Function Keys] -

                Pause = true;

                // Suspend the display update.
                m_TimerDisplayUpdate.Enabled = false;

                // Update the status message.
                MainWindow.WriteStatusMessage(Resources.SMFunctionKeyPause, Color.White, Color.Red);
            }
            else
            {
                // Allow the user to change the active workset.
                m_ToolStripComboBox1.Enabled = true;

                #region - [Function Keys] -
                // Rev.: 1.12. Display initial ToolStrip Function Key Text, Tooltip and Image.
                F4.Checked = false;
                DisplayFunctionKey(F4, Resources.FunctionKeyTextPause, Resources.FunctionKeyToolTipPause, Resources.Pause);

                // Enable the record button.
                F3.Enabled = true;

                // Enable the edit workset key if the user has sufficient privileges to do so.
                if (Security.SecurityLevelCurrent >= m_Workset.SecurityLevel)
                {
                    F5.Enabled = true;
                }

                // Disable the print button.
                F2.Enabled = false;
                #endregion - [Function Keys] -

                Pause = false;

                // Re-start the display update.
                m_TimerDisplayUpdate.Enabled = true;

                // Update the status message.
                MainWindow.WriteStatusMessage(string.Empty);
            }

            Cursor = Cursors.Default;
        }
        #endregion - [F4-Pause] -

        #region - [F5 - Edit] -
        /// <summary>
        /// Event handler for the 'F5-Edit' button <c>Click</c> event. Displays the dialog form which allows the user to edit the current workset.
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

            Debug.Assert(MainWindow != null);

            // Check that the user has sufficient privileges.
            if (Security.SecurityLevelCurrent >= m_Workset.SecurityLevel)
            {
                MenuInterfaceWatch menuInterfaceWatch = new MenuInterfaceWatch(MainWindow);
                menuInterfaceWatch.ReplaceWatch();
            }
            else
            {
                MessageBox.Show(Resources.MBTInsufficientPrivilegesEdit, Resources.MBCaptionInformation, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

            F5.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F5 - Edit] -
        #endregion - [Function Keys] -

        #region - [Timer Events] -
        /// <summary>
        /// Called periodically by the System.Windows.Forms.Timer event. Updates: (a) the status information and (b) the <c>Value</c> property of all
        /// user controls associated with this form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void DisplayUpdate(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Update the local variables with the appropriate property values of the thread that is responsible for VCU communications.
            int watchdog, recordCount;
            bool communicationFault;
            long packetCount;
            if (ThreadPollWatch != null)
            {
                watchdog = ThreadPollWatch.Watchdog;
                communicationFault = ThreadPollWatch.CommunicationFault;
                packetCount = ThreadPollWatch.PacketCount;
                recordCount = (int)ThreadPollWatch.RecordCount;
            }
            else
            {
                // Skip, if the thread has not been instantiated.
                return;
            }

            #region - [Port Locked] -
            // ------------------------------------------
            // Check if the communication port is locked.
            // ------------------------------------------
            bool watchdogTrip = false;
            if (watchdog == m_Watchdog)
            {
                // Don't assert the watchdog trip flag until the countdown has elapsed. 
                if (m_WatchdogTripCountdown <= 0)
                {
                    watchdogTrip = true;
                }
                else
                {
                    m_WatchdogTripCountdown--;
                }
            }
            else
            {
                m_WatchdogTripCountdown = WatchdogTripCountdown;
                m_Watchdog = watchdog;
            }

            // Only update on transitions of the flag.
            if (watchdogTrip != m_WatchdogTrip)
            {
                if (watchdogTrip == true)
                {
                    MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultPortLocked, Color.Red, Color.Black);

                    // Disable the display until the fault has been cleared.
                    WatchControl.InvalidValue = true;
                }
                else
                {
                    // Restore the display.
                    WatchControl.InvalidValue = false;

                    MainWindow.WriteStatusMessage(string.Empty);
                }
                m_WatchdogTrip = watchdogTrip;
            }
            #endregion - [Port Locked] -

            #region - [ReadTimeout] -
            // Only update on transitions of the flag.
            if (communicationFault != m_CommunicationFault)
            {
                if (communicationFault == true)
                {
                    // Disable the display until the fault has been cleared.
                    MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                    WatchControl.InvalidValue = true;
                }
                else
                {
                    // Restore the display.
                    MainWindow.WriteStatusMessage(string.Empty);
                    WatchControl.InvalidValue = false;
                }
                m_CommunicationFault = communicationFault;
            }
            #endregion - [ReadTimeout] -

            // Update the status information.
            // Label 1 - Date.
            InformationLabel1.Text = DateTime.Now.ToShortDateString();

            // Label 2 - Time.
            InformationLabel2.Text = DateTime.Now.ToString(CommonConstants.FormatStringTimeSec);

            // Update the recording fields.
            if (m_Record == true)
            {
                StringBuilder stringBuilder = new StringBuilder();
                TimeSpan duration = DateTime.Now.Subtract(m_StartTime);
                stringBuilder.AppendFormat("{0:D2}:", duration.Hours);
                stringBuilder.AppendFormat("{0:D2}:", duration.Minutes);
                stringBuilder.AppendFormat("{0:D2}", duration.Seconds);
                // Label 4 - Duration.
                InformationLabel4.Text = stringBuilder.ToString();
            }

            m_ProgressBar.Value = recordCount;

            // Blink the icon to show that watch data is being updated.
            if (packetCount != m_PacketCount)
            {
                MainWindow.BlinkUpdateIcon();
                m_PacketCount = packetCount;
            }

            UpdateWatchControlValues();
        }

        /// <summary>
        /// Called by the System.Windows.Forms.Timer Tick event. Save the simulated fault log data to disk.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void SaveSimulatedFaultLog(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_TimerSimulatedFaultLog.Stop();

            // Evaluate the time of the trip.            
            DateTime tripTime = DateTime.Now.Subtract(new TimeSpan(0, 0, 0, 0, m_DataStreamSimulatedFaultLog.DurationPostTripMs));

            // Clone the current simulated fault log queue to minimize the lock time.
            WatchFrame_t[] cyclicQueueArray;
            cyclicQueueArray = ThreadPollWatch.CyclicQueueLogArray;

            // Create storage for the watch values retrieved from the target hardware.
            List<WatchFrame_t> savedWatchElementsList = new List<WatchFrame_t>();

            // Add the frames from the cyclic queue to the list.
            for (int index = 0; index < cyclicQueueArray.Length; index++)
            {
                savedWatchElementsList.Add(cyclicQueueArray[index]);
            }

            m_DataStreamSimulatedFaultLog.WatchFrameList = savedWatchElementsList;
            m_DataStreamSimulatedFaultLog.Workset = Workset.RecordedWatch.ActiveWorkset;
            m_DataStreamSimulatedFaultLog.ConfigureAutoScale();

            // Show the form that allows the user to add user comments to the header information.
            FormAddComments formAddComments = new FormAddComments(FileHeader.HeaderCurrent, tripTime);
            formAddComments.CalledFrom = this;
            formAddComments.ShowDialog();

            // Create the structure for the simulated fault log information that is to be serialized to disk.
            WatchFile_t simulatedFaultLogFile = new WatchFile_t(formAddComments.Header, m_DataStreamSimulatedFaultLog);

            // ----------------------
            // Save the file to disk.
            // ----------------------
            // For consistency, use the DeriveName() method to derive the default filename of the simulated fault log file that is to be saved to disk.
            string defaultFilename = General.DeriveName(FileHeader.HeaderCurrent.TargetConfiguration.CarIdentifier, tripTime, 
                                                        CommonConstants.ExtensionSimulatedFaultLog, m_Workset.Name);

            string fullyQualifiedSimulatedFaulLogFile = General.FileDialogSaveSimulatedFaultLog(defaultFilename,
                                                        InitialDirectory.SimulatedFaultLogsWrite);
            if (fullyQualifiedSimulatedFaulLogFile != string.Empty)
            {
                // Serialize the data to the specified file.
                FileHandling.Serialize<WatchFile_t>(fullyQualifiedSimulatedFaulLogFile, simulatedFaultLogFile, FileHandling.FormatType.Binary);

                // Update the initial directory with the path of the selected file.
                InitialDirectory.SimulatedFaultLogsWrite = Path.GetDirectoryName(fullyQualifiedSimulatedFaulLogFile);
            }
        }
        #endregion - [Timer Events] -

        #region - [ComboBox Events] -
        /// <summary>
        /// Event handler for the <c>ToolStripComboBox</c> control <c>SelectedIndexChanged</c> event. Stops updating the display and polling the target
        /// and then loads and displays the selected workset.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event. In this case the value will be null as the sender is a static 
        /// class.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Clear the highlight on the comboBox.
            m_TabControl.Focus();

            CloseShowFlags();

            Update();

            WatchControl.InvalidValue = true;
            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                // It is possible that a call to this method is made indirectly from the constructor therefore check that the MainWindow reference is
                // valid before writing the status message.
                if (MainWindow != null)
                {
                    MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                }
                Cursor = Cursors.Default;
                return;
            }

            Debug.Assert(m_TimerDisplayUpdate != null, "FormViewWatch.m_ToolStripComboBox1_SelectedIndexChanged() - [m_TimerDisplayUpdate != null]");
            m_TimerDisplayUpdate.Stop();

            // Set the selected workset as the active workset.
            Workset_t workset = (Workset_t)m_ToolStripComboBox1.SelectedItem;
            Workset.RecordedWatch.SetActiveWorkset(workset.Name);
            LoadWorkset(workset);

            Pause = false;
            WatchControl.InvalidValue = false;

            // Enable the update of the watch values.
            m_TimerDisplayUpdate.Start();

            Cursor = Cursors.Default;
        }
        #endregion - [ComboBox Events] -

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

        /// <summary>
        /// Event handler for the <c>WorksetCollectionModified</c> event associated with the <c>WorksetCollection</c> class. Dynamically updates the
        /// ToolStripComboBox control with the updated worksets.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event. In this case the value will be null as the sender is a static 
        /// class.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void WorksetManager_WorksetCollectionModified(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            WatchControl.InvalidValue = true;
            if (SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback) == false)
            {
                MainWindow.WriteStatusMessage(Resources.SMCommunicationFaultReadTimeout, Color.Red, Color.Black);
                return;
            }

            Debug.Assert(m_TimerDisplayUpdate != null, "FormViewWatch.WorksetManager_WorksetCollectionModified() - [m_TimerDisplayUpdate != null]");
            m_TimerDisplayUpdate.Stop();

            ComboBoxUpdateWorksets(ref m_ToolStripComboBox1, Workset.RecordedWatch);

            // Update the Text field of the ToolStripComboBox, however, don't raise a SelectedIndexChanged event.
            m_ToolStripComboBox1.SelectedIndexChanged -= new EventHandler(m_ToolStripComboBox1_SelectedIndexChanged);
            m_ToolStripComboBox1.Text = Workset.RecordedWatch.ActiveWorkset.Name;
            m_ToolStripComboBox1.SelectedIndexChanged += new EventHandler(m_ToolStripComboBox1_SelectedIndexChanged);

            LoadWorkset(Workset.RecordedWatch.ActiveWorkset);

            Pause = false;
            WatchControl.InvalidValue = false;

            // Enable the update of the watch values.
            m_TimerDisplayUpdate.Start();
        }
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

            Cursor.Current = Cursors.WaitCursor;
            Escape.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // Check if the PTU is currently recording data and, if so, stop.
            if (m_Record == true)
            {
                // Simulate the user pressing the F3-Record key.
                F3_Click(this, new EventArgs());
            }

            // Rev 1.12. Check if the PTU is currently paused and, if so, continue.
            if (m_Pause == true)
            {
                // Simulate the user pressing the F4-Pause key.
                F4_Click(this, new EventArgs());
            }

            if (m_TimerDisplayUpdate != null)
            {
                m_TimerDisplayUpdate.Enabled = false;
            }

            m_TabControl.TabPages.Clear();
            Update();

            if (CommunicationInterface != null)
            {
                SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback);
                StopPolling();

                // If there are problems with the communication link, set the PTU to configuration mode and close the communication port.
                if (m_CommunicationFault == true || m_WatchdogTrip == true)
                {
                    CommunicationInterface.CloseCommunication(CommunicationInterface.CommunicationSetting.Protocol);
                    MainWindow.SetMode(Mode.Configuration);
                }
            }

            // Clear the progress bar.
            m_LabelProgressBar.Text = string.Empty;
            m_ProgressBar.Visible = false;

            Escape.Checked = false;
            Cursor.Current = Cursors.Default;

            base.Exit();
        }

        /// <summary>
        /// Process any form specific changes to the menu system or function keys resulting from a change in the security level of the user. Check
        /// whether the new securitly level allows the user to edit the current workset.
        /// </summary>
        /// <param name="security">Reference to the security class.</param>
        public override void UpdateMenu(Security security)
        {
            // Don't allow the user to edit the workset if: (a) the selected workset is the baseline workset or (b) the current security level is less
            // than the security level of the workset.
            if (Security.SecurityLevelCurrent < m_Workset.SecurityLevel)
            {
                F5.Enabled = false;
            }
            else
            {
                F5.Enabled = true;
            }
            
            base.UpdateMenu(security);
        }

        /// <summary>
        /// Add (1) the watch values associated with the cyclic buffer used to store recorded watch values and (2) the max/min values for each watch
        /// element to the structure specified by parameter <paramref name="watchFile"/> and serialize the structure to disk using the specified
        /// filename.
        /// </summary>
        private void SaveRecordedWatchData(string fullFilename, WatchFile_t watchFile)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Clone the current simulated fault log queue to minimize the lock time.
            WatchFrame_t[] cyclicQueueArray;
            cyclicQueueArray = ThreadPollWatch.CyclicQueueRecordArray;

            // Create storage for the watch values retrieved from the target hardware.
            List<WatchFrame_t> savedWatchElementsList = new List<WatchFrame_t>();

            // Add the frames from the cyclic queue to the list.
            for (int index = 0; index < cyclicQueueArray.Length; index++)
            {
                savedWatchElementsList.Add(cyclicQueueArray[index]);
            }

            // Add the watch values to the file.
            watchFile.DataStream.WatchFrameList = savedWatchElementsList;

            // Add the auto-scale information for each watch element over the period of the recording to the file.
            watchFile.DataStream.AutoScaleWatchValues = ThreadPollWatch.AutoScaleWatchValues;
            Debug.Assert(watchFile.DataStream.Workset.WatchElementList.Count <= watchFile.DataStream.AutoScaleWatchValues.Length);

            watchFile.DataStream.EvaluateAutoScaleLimits();

            // ----------------------
            // Serialize the file structure to disk.
            // ----------------------
            FileHandling.Serialize<WatchFile_t>(fullFilename, watchFile, FileHandling.FormatType.Binary);
        }

        /// <summary>
        /// Report the current recording status.
        /// </summary>
        /// <param name="isRecording">Flag to indicate whether the watch data is currently being recorded.</param>
        private void ReportRecordingStatus(bool isRecording)
        {
            // Label3 - Rec. Start
            InformationLabel3.Visible = isRecording;
            Legend3.Visible = isRecording;

            // Label 4 - Duration
            InformationLabel4.Visible = isRecording;
            Legend4.Visible = isRecording;

            // Update the status message.
            m_ProgressBar.Visible = (isRecording) ? true : false;
            m_LabelProgressBar.Text = (isRecording) ? Resources.LegendRecording : string.Empty;
            m_LabelProgressBar.ForeColor = Color.Red;
            m_LabelProgressBar.Visible = true;
            
            // Reset the progress bar.
            m_ProgressBar.Value = 0;
        }

        /// <summary>
        /// Load and display the specified workset.
        /// </summary>
        /// <remarks>Polling of the target hardware and the display update must be suspended before calling this method.</remarks>
        /// <param name="workset">The workset that is to be loaded.</param>
        private void LoadWorkset(Workset_t workset)
        {
            m_Workset = workset;

            // Update the workset associated with the recorded watch data data stream.
            m_WatchFileRecordedData.DataStream.Workset = m_Workset;

            // Configure the watch controls for the selected workset.
            ConfigureDisplayPanel(m_Workset, m_TabPage1, m_WatchControlSize);

            // Update the tab with the name of the workset.
            m_TabControl.TabPages[m_TabControl.SelectedIndex].Text = m_Workset.Name;

            // Don't allow the user to edit the workset if the selected workset is the baseline workset or the current security level is less than the
            // security level of the workset.
            if ((m_Workset.Name.ToLower().Equals(Resources.NameBaselineWorkset.ToLower())) ||
                (Security.SecurityLevelCurrent < m_Workset.SecurityLevel))
            {
                F5.Enabled = false;
            }
            else
            {
                F5.Enabled = true;
            }

            Update();

            // Set the watch elements of the new workset.
            try
            {
                CommunicationInterface.SetWatchElements(m_Workset.WatchElementList);
            }
            catch (Exception)
            {
                // Do nothing, just ensure that an exception isn't thrown.
            }
        }

        /// <summary>
        /// Update the worksets associated with the <c>Items</c> property of the specified <c>ToolStripComboBox</c> control.
        /// </summary>
        /// <param name="comboBox">The <c>ToolStripComboBox</c> control that it to be processed.</param>
        /// <param name="worksetCollection">The workset collection containing the updated worksets.</param>
        private void ComboBoxUpdateWorksets(ref ToolStripComboBox comboBox, WorksetCollection worksetCollection)
        {
            Debug.Assert(worksetCollection != null, "FormViewWatch.ComboBoxAddWorksets() - [worksetCollection != null]");
            Debug.Assert(comboBox != null, "FormViewWatch.ComboBoxAddWorksets() - [comboBox != null]");

            comboBox.Items.Clear();

            // Get the list of available worksets, the first entry should be the active workset. An entry should not be created for the baseline workset
            // unless it is defined as the active workset.
            for (int itemIndex = 0, worksetIndex = worksetCollection.ActiveIndex; itemIndex < worksetCollection.Worksets.Count; itemIndex++)
            {
                if (worksetCollection.ActiveIndex != 0)
                {
                    // Baseline workset is NOT the active workset, therefore don't create an entry for it.
                    if (worksetIndex == 0)
                    {
                        worksetIndex++;
                        worksetIndex %= worksetCollection.Worksets.Count;
                        continue;
                    }
                }

				// Only add those worksets that can be displayed i.e. exclude any worksets where the number of watch variables exceeds the current watch
                // size.
				if (worksetCollection.Worksets[worksetIndex].WatchElementList.Count <= Parameter.WatchSize)
				{
					comboBox.Items.Add(worksetCollection.Worksets[worksetIndex]);
				}
				worksetIndex++;

                // Add the remaining worksets, using a round-robin approach.
                worksetIndex %= worksetCollection.Worksets.Count;
            }

            // Check whether the workset collection contains at least one workset that can be displayed. This is a 'belt and braces' approach as the
            // processing in
            // the Update() method of the WorksetCollection class should ensure that the Baseline workset is always displayable.
			if (comboBox.Items.Count <= 0)
			{
                // No - Add another baseline workset to support the current watchsize.
				Workset_t workset = worksetCollection.CreateBaselineWorkset();
				workset.SecurityLevel = SecurityLevel.Level1;

                string backupWorksetName = Resources.NameBaselineWorkset;
                int postfixNumber = Parameter.WatchSize;
				do
				{
					workset.Name = backupWorksetName + CommonConstants.Space + "(" + postfixNumber++ + ")";
				}
                while (worksetCollection.Contains(workset.Name));

                MessageBox.Show(Resources.MBTValidWorksetNotFound + CommonConstants.Space + String.Format(Resources.MBTLoadingBaselineWorkset,
                                workset.Name), Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				worksetCollection.Add(workset);
				comboBox.Items.Add(workset);
				worksetCollection.SetActiveWorkset(workset.Name);
			}
        } 
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the size of the cyclic queue used to store recorded watch data.
        /// </summary>
        private int CyclicQueueSizeRecord
        {
            get { return m_CyclicQueueSizeRecord; }
            set { m_CyclicQueueSizeRecord = value; }
        }

        /// <summary>
        /// Gets the size of the cyclic queue used to store the simulated fault log data.
        /// </summary>
        private int CyclicQueueSizeSimulatedFaultLog
        {
            get { return m_CyclicQueueSizeSimulatedFaultLog; }
            set { m_CyclicQueueSizeSimulatedFaultLog = value; }
        }
        #endregion --- Properties ---
    }
}