#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    Common
 * 
 *  File name:  FormShowSystemInformation.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/25/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  09/30/10    1.1     K.McD           1.  Raises a DataUpdate event whenever the date/time is updated.
 *                                      2.  Add the DateTime property to record the latest date time received from the VCU.
 * 
 *  10/06/10    1.2     K.McD           1.  Added the Pause property to allow the form used to configure the date and time to suspend polling of the VCU for date/time
 *                                          information. May consider implementing the full-blown IPollTarget interface at a later date.
 * 
 *  10/15/10    1.3     K.McD           1.  Modified to use the CommunicationApplication class.
 * 
 *  02/14/11    1.4     K.McD           1.  Added the GroupBox associated with the notes written to the user.
 *                                      2.  Request - SNCR001.40. Only enable the button used to show the form which allows the user to configure the real time clock
 *                                          if the user is logged into engineering mode.
 * 
 *  02/28/11    1.5     K.McD           1.  Modified the event handler for the Shown event to inform the user that the data update has been paused if a multiple
 *                                          document interface (MDI) child form is currently on display.
 * 
 *                                      2.  Modified the Exit() method to clear the status message that inform the user that the data update has been paused.
 * 
 *                                      3.  Removed any reference to the button that calls the form to allow the user to update the VCU dat and time.
 * 
 * 03/21/11     1.6     K.McD           1.  Removed to button to display the form used to configure the VCU real time clock.
 *                                      2.  Removed the DataUpdate event.
 *                                      3.  Removed the Pause property.
 * 
 *  04/27/11    1.7     K.McD           1.  Modified the Cleanup() method to set m_CommunicationInterface to null.
 *  
 *  07/20/11    1.8     K.McD           1.  Modified the signature of the constructor to use the ICommunicationParent interface.
 *  
 *  08/23/11    1.9     K.McD           1.  Modified the form 'Shown' event handler to close the form if a communication exception is thrown by the call to the 
 *                                          method that retrieves the target configuration.
 *                                          
 *                                      2.  Modified the form Shown event handler to include a check that the update timer reference is still valid before attempting
 *                                          to start the timer as the preceding DisplayUpdate() method has the potential to dispose of the timer.
 *                                          
 *                                      3.  Modified the DisplayUpdate() method to stop the display update timer and to close the form if an exception is thrown 
 *                                          by the call to the method that retrieves the time and date information from the target.
 *                                          
 *                                      4.  Modified the Cleanup() method to include a check that the timer reference is valid before attempting to de-register the 
 *                                          Tick event.
 *                                          
 *  10/07/11    1.10	Sean.D			1.  Modified constructor to invoke CommunicationApplicationOffline when in Offline mode.
 *  
 *  03/25/15    1.11    K.McD           References
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
 *                                      3.  Added the Parameter.Use4DigitYearCode property in the call to GetTimeDate() within the DisplayUpdate() method. Ref.: 1.1, 2.
 *                                      4.  Rationalized the constructors.
 *                                      
 *  07/28/15    1.12     K.McD          References
 *                                      1.  Auto-Update.
 *                                      
 *                                      Modifications
 *                                      1.  Automatic update resulting from changing the name of the 'Car Identifier' Label to m_LabelCarNumber.
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
    /// Form to allow the user to view the system information loaded into the target hardware.
    /// </summary>
    public partial class FormShowSystemInformation : FormPTUDialog, ICommunicationInterface<ICommunicationApplication>
    {
        #region --- Constants ---
        /// <summary>
        /// The format string to be used when displaying the current time. Value: "HH:mm:ss".
        /// </summary>
        private const string FormatStringTime = "HH:mm:ss";

        /// <summary>
        /// The display update interval, in ms. Value: 1,000 ms.
        /// </summary>
        private const int IntervalDisplayUpdate = 1000;
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
        public FormShowSystemInformation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface that is to be used to communicate with the VCU.</param>
        public FormShowSystemInformation(ICommunicationParent communicationInterface) : this()
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

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                m_TimerDisplayUpdate = null;
                m_CommunicationInterface = null;

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
        /// Event handler for the <c>Shown</c> event. Get the target configuration from the VCU and update the appropriate form labels.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormShowSystemInformation_Shown(object sender, EventArgs e)
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
            // Do not initiate any communication requests until the communication associated with any Mdi child forms that are open have been suspended.
            // ------------------------------------------------------------------------------------------------------------------------------------
            Debug.Assert(CommunicationInterface != null);

            PauseCommunication<ICommunicationApplication>(CommunicationInterface, true);

            TargetConfiguration_t targetConfiguration = new TargetConfiguration_t();
            try
            {
                CommunicationInterface.GetEmbeddedInformation(out targetConfiguration);
            }
            catch (CommunicationException communicationException)
            {
                MessageBox.Show(communicationException.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainWindow.WriteStatusMessage(string.Empty);
                Close();
                return;
            }

            m_LabelLogicType.Text = targetConfiguration.SubSystemName;
            m_LabelSoftwareVersion.Text = targetConfiguration.Version;
            m_LabelCarNumber.Text = targetConfiguration.CarIdentifier;

            DisplayUpdate(this, new EventArgs());

            // The DisplayUpdate() method has the potential to close the form, therefore to avoid a NullReferenceException, ensure that the timer is still 
            // instantiated before attempting to start it.
            if (m_TimerDisplayUpdate != null)
            {
                m_TimerDisplayUpdate.Start();
            }
        }
        #endregion - [Form] -

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
        }

        #region - [Buttons] -
        /// <summary>
        /// Event handler for the OK button <c>Click</c> event. Close the form.
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

            MainWindow.WriteStatusMessage(string.Empty);
            Close();
        }
        #endregion - [Buttons] -
        #endregion --- Delegated Methods ---

        #region --- Properties ---

        /// <summary>
        /// Gets the date and time retrieved from the VCU as a .NET <c>DateTime</c> object.
        /// </summary>
        public DateTime DateTime
        {
            get { return m_DateTime; }
        }

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
