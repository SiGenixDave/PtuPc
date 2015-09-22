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
 *  File name:  FormPTUDialog.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  03/31/10    1.0     K.McDonald      First Release.
 * 
 *  08/25/10    1.1     K.McD           1.  Added code to assert the Pause property of any multiple-document interface (MDI) child forms that are open if the 
 *                                          CommunicationInterface property is defined. Also clears the Pause property on disposal. This will suspend communication 
 *                                          requests from the Mdi child if the form inherited from this form intends to communicate with the target hardware.
 *                                      2.  Inherited ApplicationWindow renamed to MainWindow.
 *                                      3.  Added the CommunicationInterface property.
 * 
 *  08/26/10    1.2     K.McD           1.  Added the FormClosing() event handler which will call the Dispose() method of the form. This ensures that the form is disposed
 *                                          of immediately.
 * 
 *  08/30/10    1.3     K.McD           1.  Modified the 'Shown' event handler so that polling is only suspended an any child forms if the dialog form is called from
 *                                          the main application window.
 * 
 *  09/29/10    1.4     K.McD           1.  Added the Pause property. This flag used to control the display update. 
 * 
 *  10/06/10    1.5     K.McD           1.  Removed the Pause property, this feature is now implemented using the IPollTarget interface.
 *                                      2.  Modified the implementation of version 1.1 item 1 so that the an attempt to suspend polling will only be made if the child class 
 *                                          implements the IPollTarget property.
 * 
 *  11/02/10    1.6     K.McD           1.  Added the PauseCommunication() method.
 * 
 *  02/14/11    1.7     K.McD           1.  Replaced any reference to the ISecurity interface with the Security class.
 * 
 *  03/18/11    1.8     K.McD           1.  Modified the PauseCommunication() method to update the status message box of the main application window.
 * 
 *  04/27/11    1.9     K.McD           1.  Included a Debug.Assert() statement in the PauseCommunication() method.
 *                                      2.  Modified a number of comments and XML tags associated with the PauseCommunication() method.
 *                                      
 *  05/23/11    1.10    K.McD           1.  Removed the call to Debug.Assert(), to check whether the MainWindow propery had been defined, from the PauseCommunication 
 *                                          method.
 *                                          
 *  08/24/11    1.10.1  K.McD           1.  Modified to accommodate the changes to the signature of the IPollTarget.SetPauseAndWait() method.
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

using Common.Configuration;
using Common.Communication;
using Common;
using Common.Properties;

namespace Common.Forms
{
    /// <summary>
    /// Parent form for the PTU dialog box form.
    /// </summary>
    public partial class FormPTUDialog : Form
    {
        #region --- Member Variables ---
        /// <summary>
        /// Reference to the main application window i.e. the multi document interface reference.
        /// </summary>
        protected IMainWindow m_MainWindow;

        /// <summary>
        /// Flag to indicate that the form was instantiated by the Visual Studio development environment.
        /// </summary>
        /// <remarks>This flag can be inspected in the <c>Shown</c> event handler to ensure that inappropriate classes are not instantiated under these circumstances.</remarks>
        protected bool m_VisualStudio;

        /// <summary>
        /// Flag to indicate whether the Dispose() method has been called. True indicates that the Dispose() method has been called; otherwise, false.
        /// </summary>
        private bool m_IsDisposed;

        /// <summary>
        /// Reference to the <c>Security</c> class associated with the PTU.
        /// </summary>
        private Security m_Security;

        /// <summary>
        /// Reference to the form that called this dialog.
        /// </summary>
        private Form m_CalledFrom;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the form.
        /// </summary>
        public FormPTUDialog()
        {
            InitializeComponent();

            m_Security = new Security();

            // Only update the font if the specified font is not null.
            if (Parameter.Font != null)
            {
                this.Font = Parameter.Font;
            }
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
                m_CalledFrom = null;
                m_MainWindow = null;
                m_Security = null;

                #region --- Windows Form Designer Variables ---
                // Detach the event handler delegates.
               
                // Set the Windows Form Designer Variables to null.
                #endregion --- Windows Form Designer Variables ---
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
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
        protected virtual void FormPTUDialog_Shown(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Check whether the CalledFrom property has been set.
            if (CalledFrom == null)
            {
                return;
            }

            // If the CalledFrom property has been set, cast to IMainWindow. If the calling form was not derived from IMainWindow, the MainWindow property will remain null.
            m_MainWindow = CalledFrom as IMainWindow;
        }

        /// <summary>
        /// Event handler for the <c>FormClosing</c> event. Force disposal of the form.
        /// </summary>
        /// <remarks>If the user closes the form using the [X] icon at the top right hand side of the form the Dispose() method is not always called immediately which
        /// can cause proplems on dialog forms which communicate with the target hardware.
        /// </remarks>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormPTUDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Dispose();
        }
        #endregion - [Form] -
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Suspend or resume polling of the vehicle control unit by the multiple document interface child form that is currently being displayed.
        /// </summary>
        /// <remarks>
        /// If the MainWindow property of this form i.e. the reference to the main application window is not defined then the call to this method will be ignored.
        /// </remarks>
        /// <typeparam name="T">The communication interface type.</typeparam>
        /// <param name="communicationInterface">Reference to the communication interface.</param>
        /// <param name="pause">A flag to control whether polling of the VCU by the multiple document interface child form is to be resumed or suspended. True, 
        /// suspends polling of the VCU; false, resumes polling of the VCU.</param>
        protected void PauseCommunication<T>(T communicationInterface, bool pause) where T : ICommunicationParent
        {
            // Check that the specified communication interface has been initialized.
            if (communicationInterface != null)
            {
                // Only suspend polling if the dialog box was called from the main application window. If called from another dialog box, assume that the calling
                // dialog box has taken care of suspending the polling.
                if (MainWindow != null)
                {
                    // Check whether any multiple document interface child forms are running.
                    if (MainWindow.MdiChildren.Length > 0)
                    {
                        // Check which of the multiple document interface child forms implement the IPollTarget interface i.e. actually poll the VCU.
                        for (int child = 0; child < MainWindow.MdiChildren.Length; child++)
                        {
                            if (MainWindow.MdiChildren[child] as IPollTarget != null)
                            {
                                if (pause == false)
                                {
                                    // Resume polling - Clear the pause property.
                                    (MainWindow.MdiChildren[child] as IPollTarget).Pause = false;
                                    MainWindow.WriteStatusMessage(string.Empty);
                                }
                                else
                                {
                                    // Pause polling - Call the SetPauseAndWait() method.
                                    (MainWindow.MdiChildren[child] as IPollTarget).SetPauseAndWait(CommonConstants.TimeoutMsPauseFeedback);
                                    MainWindow.WriteStatusMessage(Resources.SMPaused);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
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

        /// <summary>
        /// Gets or sets the reference to the form that called this dialog.
        /// </summary>
        public Form CalledFrom
        {
            get { return m_CalledFrom; }
            set { m_CalledFrom = value;}
        }

        /// <summary>
        /// Gets the reference to the main application window.
        /// </summary>
        protected IMainWindow MainWindow
        {
            get { return m_MainWindow; }
        }

        /// <summary>
        /// Gets the reference to the system security class.
        /// </summary>
        protected Security Security
        {
            get { return m_Security; }
        }
        #endregion --- Properties ---
    } 
}