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
 *  Project:    SelfTest
 * 
 *  File name:  MenuInterfaceSelfTest.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  05/25/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/22/11    1.1     K.McD           1.  Added the logic associated with the ConfigureSelfTests() method.
 *  
 *  08/07/13    1.2     K.McD           1.  In those methods where it is applicable, the cursor was set to the wait cursor after the call to the
 *                                          MainWindow.CloseChildForms() method as if any child forms are open, the cursor may be set to the default cursor as part of the
 *                                          call to the Exit() method within the child form.
 *
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Windows.Forms;

using Common;
using SelfTest.Forms;
using SelfTest.Properties;

namespace SelfTest
{
    /// <summary>
    /// Methods called by the menu options associated with the self-test sub-system  - SelfTest.dll.
    /// </summary>
    public class MenuInterfaceSelfTest : MenuInterface
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="mainWindow">Reference to the main application window interface.</param>
        public MenuInterfaceSelfTest(IMainWindow mainWindow)
            : base(mainWindow)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Show the dialog box which allows the user to configure which self tests are to be run.
        /// </summary>
        public void ConfigureSelfTests()
        {
            MainWindow.CloseChildForms();
            MainWindow.Cursor = Cursors.WaitCursor;

            try
            {
                FormViewTestResults formViewTestResults = new FormViewTestResults(MainWindow.CommunicationInterface, MainWindow);
                MainWindow.ShowMdiChild(formViewTestResults);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MainWindow.Cursor = Cursors.Default;
            }
        }
        #endregion --- Methods ---
    }
}
