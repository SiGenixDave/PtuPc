#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    PTU Application
 * 
 *  File name:  FormConfigureDateTime.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/31/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  09/30/10    1.1     K.McD           1.  Date and time display now updated using the DataUpdate event published by the for used to display the
 *                                          system information.
 * 
 *  10/15/10    1.2     K.McD           1.  Modified to use the generic version of PTUDialog<>.
 * 
 *  03/21/11    1.3     K.McD           1.  Modified the design such that the form is now self sufficient i.e. it is no longer called from the
 *                                          FormShowSystemInformation class.
 * 
 *                                              (a) Modified the signature of the constructor to pass a reference to the communication interface that
 *                                                  is to be used to communicate with the VCU.
 * 
 *                                              (b) Added the display update timer to retrieve and display the real time clock value.
 *                                              
 *  07/20/11    1.4     K.McD           1.  Modified the signature of the constructor to use the ICommunicationParent interface.
 *  
 *  08/24/11    1.5     K.McD           1.  Modified the DisplayUpdate() method to stop the display update timer and to close the form if an exception
 *                                          is thrown by the call to the method that retrieves the time and date information from the target.
 *                                          
 *                                      2.  Modified the Apply button event handler to stop the display update timer and to close the form if an
 *                                          exception is thrown by the call to the method that sets the time and date information.
 *                                          
 *                                      3.  Modified the form Shown event handler to include a check that the update timer reference is still valid
 *                                          before attempting to start the timer as the preceding DisplayUpdate() method has the potential to dispose
 *                                          of the timer.
 *                                          
 *                                      4.  Modified the Cleanup() method to include a check that the timer reference is valid before attempting to
 *                                          de-register the Tick event. 
 *  
 *  10/07/11    1.6     Sean.D			1.  Modified constructor to invoke CommunicationApplicationOffline when in Offline mode.
 *  
 *  02/27/14    1.7     K.McD           1.  Changed the name to FormConfigureRealTimeClock to be consistent with the MenuInterfaceApplication class.
 *  
 *  03/25/15    1.8     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                          
 *                                          1.  Changes to allow the PTU to handle both 2 and 4 character year codes.
 *                                          
 *                                      2.  The YearCodeSize field of the CONFIGUREPTU table of the project PTU configuration (.e1) database has been deleted and replaced
 *                                          by the Use4DigitYearCode flag corresponding to bit 0 of the FunctionFlags bitmask field. This field is a bitmask that specifies
 *                                          which of the programmable function options are to be enabled for the current project. So far, the following flags have been
 *                                          defined:
 *
 *                                          Bit 0   -   Use4DigitYearCode - Flag to specify whether the project VCU uses a 2 or 4 digit year code. True, if the project
 *                                                      uses a 4 digit year code; otherwise, false.
 *                                          Bit 1   -   ShowEventLogType - Flag to specify whether the event log type field is to be shown when saved event logs are
 *                                                      displayed. True, if the log type is to be displayed; otherwise, false.
 *                                          .
 *                                          .
 *                                          .
 *                                          Bit 7   -   Not Used.
 *                                          
 *                                      Modifications    
 *                                      1.  Modified the DateTimePicker.MaxDate and MinDate values in the constructor if the VCU uses a 4 digit year code. Ref.:1.1.
 *                                      2.  Added the Parameter.Use4DigitYearCode property to the GetTimeDate() call within the DisplayUpdate() method. Ref.: 2.
 *                                      3.  Added the Parameter.Use4DigitYearCode property to the SetTimeDate() call in the 'Apply' button event handler. Ref.: 2.
 *                                      4.  Added a zero parameter constructor.
 *                                      5.  Replaced the m_Use4DigitYearCode flag with the Parameter.Use4DigitYearCode derived from bit 0 of the FunctionFlags field.
 *                                          Ref.: 1.1, 2.
 *
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Bombardier.PTU.Communication;
using Bombardier.PTU.Properties;
using Common.Communication;
using Common.Configuration;
using Common.Forms;

namespace Bombardier.PTU.Forms
{
    /// <summary>
    /// Form used to configure the VCU date and time.
    /// </summary>
    public partial class FormConfigureRealTimeClock : FormPTUDialog, ICommunicationInterface<ICommunicationApplication>
    {
        #region --- Constants ---
        /// <summary>
        /// The display update interval, in ms. Value: 1,000 ms.
        /// </summary>
        private const int IntervalDisplayUpdate = 1000;

        /// <summary>
        /// The format string to be used when displaying the current time. Value: "HH:mm:ss".
        /// </summary>
        private const string FormatStringTime = "HH:mm:ss";

        #region - [DateTimePicker Values] -
        /// <summary>
        /// The maximum value for the Month property of a .NET DateTime object. Value: 12.
        /// </summary>
        private const int MaxMonth = 12;

        /// <summary>
        /// The minimum value for the Month property of a .NET DateTime object. Value: 1
        /// </summary>
        private const int MinMonth = 1;

        /// <summary>
        /// The maximum value for the Day property of a .NET DateTime object. Value: 31.
        /// </summary>
        private const int MaxDay = 31;

        /// <summary>
        /// The minimum value for the Day property of a .NET DateTime object. Value: 1.
        /// </summary>
        private const int MinDay = 1;

        /// <summary>
        /// The maximum value for the Year property of a .NET DateTime object if the VCU supports a 2 digit year code. Value: 2069.
        /// </summary>
        private const int MaxYear2DigitYearCode = 2069;

        /// <summary>
        /// The minimum value for the Year property of a .NET DateTime object if the VCU supports a 2 digit year code. Value: 1970.
        /// </summary>
        private const int MinYear2DigitYearCode = 1970;

        /// <summary>
        /// The maximum value for the Year property of a .NET DateTime object if the VCU supports a 4 digit year code. Value: 2199.
        /// </summary>
        private const int MaxYear4DigitYearCode = 2199;

        /// <summary>
        /// The minimum value for the Year property of a .NET DateTime object if the VCU supports a 4 digit year code. Value: 1900.
        /// </summary>
        private const int MinYear4DigitYearCode = 1900;
        #endregion - [DateTimePicker Values] -
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The System.Windows.Forms timer used to manage the display update.
        /// </summary>
        private System.Windows.Forms.Timer m_TimerDisplayUpdate;

        /// <summary>
        /// The date and time retrieved from the VCU as a .NET <c>DateTime</c> object.
        /// </summary>
        private DateTime m_DateTime;

        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        private ICommunicationApplication m_CommunicationInterface;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor, required by Visual Studio.
        /// </summary>
        public FormConfigureRealTimeClock()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class. 
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface that is to be used to communicate with the VCU.</param>
        public FormConfigureRealTimeClock(ICommunicationParent communicationInterface) : this()
        {
            if (communicationInterface is CommunicationParent)
			{
                CommunicationInterface = new CommunicationApplication(communicationInterface);
			}
			else
	        {
                CommunicationInterface = new CommunicationApplicationOffline(communicationInterface);
			}

            Debug.Assert(CommunicationInterface != null);

            // Initialize the DateTime picker controls.
            m_DateTimePickerDate.Value = DateTime.Now;
            m_DateTimePickerTime.Value = DateTime.Now;

            // If the VCU uses a 4 digit year code, change the MaxDate and MinDate values of the DateTimePicker from the current default values. The default values
            // are MinDate = 1st Jan 1970 and MaxDate = 31st Dec 2069.
            if (Parameter.Use4DigitYearCode == true)
            {
                m_DateTimePickerDate.MaxDate = new DateTime(MaxYear4DigitYearCode, MaxMonth, MaxDay);
                m_DateTimePickerDate.MinDate = new DateTime(MinYear4DigitYearCode, MinMonth, MinDay);
            }
            else
            {
                m_DateTimePickerDate.MaxDate = new DateTime(MaxYear2DigitYearCode, MaxMonth, MaxDay);
                m_DateTimePickerDate.MinDate = new DateTime(MinYear2DigitYearCode, MinMonth, MinDay);
            }

            m_TableLayoutPanelDateTimePicker.Enabled = false;

            #region - [Display Timer] -
            m_TimerDisplayUpdate = new System.Windows.Forms.Timer();
            m_TimerDisplayUpdate.Tick += new EventHandler(DisplayUpdate);
            m_TimerDisplayUpdate.Interval = IntervalDisplayUpdate;
            m_TimerDisplayUpdate.Enabled = true;
            m_TimerDisplayUpdate.Stop();
            #endregion - [Display Timer] -
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Cleanup(bool disposing)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            try
            {
                // Ensure that the display update timer is stopped.
                if (m_TimerDisplayUpdate != null)
                {
                    m_TimerDisplayUpdate.Stop();
                }

                // Resume polling of the VCU.
                PauseCommunication<ICommunicationApplication>(CommunicationInterface, false);

                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    if (m_TimerDisplayUpdate != null)
                    {
                        m_TimerDisplayUpdate.Tick -= new EventHandler(DisplayUpdate);
                        m_TimerDisplayUpdate.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to
                // null.
                m_TimerDisplayUpdate = null;

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
        #region - [Form] -
        /// <summary>
        /// Event handler for the form <c>Shown</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormConfigureDateTime_Shown(object sender, EventArgs e)
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

            // ------------------------------------------------------------------------------------------------------------------------------------
            // Do not initiate any communication requests until the communication associated with any Mdi child forms that are open have been
            // suspended.
            // ------------------------------------------------------------------------------------------------------------------------------------
            Debug.Assert(CommunicationInterface != null);

            PauseCommunication<ICommunicationApplication>(CommunicationInterface, true);

            DisplayUpdate(this, new EventArgs());

            // The DisplayUpdate() method has the potential to close the form, therefore to avoid a NullReferenceException, ensure that the timer is
            // still instantiated before attempting to start it.
            if (m_TimerDisplayUpdate != null)
            {
                m_TimerDisplayUpdate.Start();
            }
        }
        #endregion - [Form] -

        #region - [Buttons] -
        /// <summary>
        /// Event handler for the OK button <c>Click</c> event. Simulate the user pressing the Apply button and then close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonOK_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Check whether a change is still to be applied and, if so, apply the change.
            if (m_ButtonApply.Enabled)
            {
                m_ButtonApply_Click(this, new EventArgs());
            }

            Close();
        }

        /// <summary>
        /// Event handler for the Cancel button <c>Click</c> event. Close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Close();
        }

        /// <summary>
        /// Event handler for the Apply button <c>Click</c> event. Update the date and time on the VCU according to which radio button option has been
        /// selected.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonApply_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            DateTime newDateTime;
            if (m_RadioButtonAuto.Checked == true)
            {
                // Update the target hardware using the PC time.
                newDateTime = DateTime.Now;
            }
            else
            {
                newDateTime = new DateTime(m_DateTimePickerDate.Value.Year, m_DateTimePickerDate.Value.Month, m_DateTimePickerDate.Value.Day,
                                           m_DateTimePickerTime.Value.Hour, m_DateTimePickerTime.Value.Minute, m_DateTimePickerTime.Value.Second);
            }

            try
            {
                CommunicationInterface.SetTimeDate(Parameter.Use4DigitYearCode, newDateTime);
            }
            catch (CommunicationException)
            {
                m_TimerDisplayUpdate.Stop();
                MessageBox.Show(Resources.MBTDateTimeSetFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            m_ButtonApply.Enabled = false;
            DialogResult = DialogResult.Yes;
        }
        #endregion - [Buttons] -

        /// <summary>
        /// Event handler for the <c>CheckChanged</c> event associated with the radio buttons. Enable/Disable the <c>DateTimePicker</c> controls
        /// depending upon which radio button is selected.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_RadioButtonAuto_CheckedChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_TableLayoutPanelDateTimePicker.Enabled = m_RadioButtonManual.Checked;
            m_ButtonApply.Enabled = true;
        }

        /// <summary>
        /// Event handler for the <c>ValueChanged</c> event associated with either the date or time <c>DateTimePicker</c> control. Re-enable the apply
        /// button if the user changes the date or time.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Only re-enable the apply button if in manual update mode.
            if (m_RadioButtonManual.Checked == true)
            {
                m_ButtonApply.Enabled = true;
            }
        }

        /// <summary>
        /// Event handler associated with the timer <c>Tick</c> event. Get the time and date from the VCU and update the corresponding form labels.
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

            DateTime dateTime = DateTime.Now;
            try
            {
                CommunicationInterface.GetTimeDate(Parameter.Use4DigitYearCode, out dateTime);
            }
            catch (CommunicationException)
            {
                m_TimerDisplayUpdate.Stop();
                MessageBox.Show(Resources.MBTDateTimeGetFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainWindow.WriteStatusMessage(string.Empty);
                Close();
                return;
            }

            MainWindow.BlinkUpdateIcon();
            m_LabelDate.Text = dateTime.ToShortDateString();
            m_LabelTime.Text = dateTime.ToString(FormatStringTime);
            m_DateTime = dateTime;

            if (m_RadioButtonAuto.Checked == true)
            {
                // Reset the DateTime picker to the PC Time and Date properties.
                m_DateTimePickerDate.Value = DateTime.Now;
                m_DateTimePickerTime.Value = DateTime.Now;
            }
        }
        #endregion --- Delegated Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the target.
        /// </summary>
        /// <remarks>This property is set by the child class, if appropriate.</remarks>
        public ICommunicationApplication CommunicationInterface
        {
            get { return m_CommunicationInterface; }
            set { m_CommunicationInterface = value; }
        }
        #endregion --- Properties ---
    }
}