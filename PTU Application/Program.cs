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
 *  Project:    PTU Application
 * 
 *  File name:  Program.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  03/22/10    1.0     K.McD           First release.
 *  
 *  07/27/15    1.1     K.McD           References
 *                                      1.  An informal review of version 6.11 of the PTU concluded that, where possible - i.e. if the PTU is started from a shortcut
 *                                          that passes the project identifier as a shortcut parameter, the project specific PTU initialization should be carried out
 *                                          in the MDI Form contructor that has the parameter string array as its signature rather than by the LoadDictionary() method.
 *                                          This streamlines the display construction of the Control Panel associated with the R188 project. In the 6.11 implementation
 *                                          the CTA layout is momentarily displayed before the Control Panel is drawn, however by initializing the project specific
 *                                          features in the constructor the Control Panel associated with the R188 project is drawn immediately and the CTA layout
 *                                          is not shown at all.
 *  
 *                                      Modifications
 *                                      1.  Introduced a do/while statement that checks whether the Restart static flag property of the MDI interface is asserted
 *                                          and, if so, restarts the PTU application. In conjunction with this Restart property, two new methods have been added to
 *                                          the IMainWindow interface, Close() and SetRestart(). Together, these allow any class that can access the IMainWindow
 *                                          interface to initiate a restart of the PTU application.
 *                                          
 *                                          The reason for these changes in terms of reference 1 is that having this feature allows the, time consuming, section of
 *                                          code that disposes of the WibuBox timer and Control Panel to be removed from the LoadDictionary() method. The code was
 *                                          originally included to reset the PTU back to its start-up state in the event that a new data dictionary was selected by the
 *                                          user.  The code is no longer required as the 'File/Select Data Dictionary' menu option now loads the data dictionary and
 *                                          restarts the PTU.
 */
#endregion --- Revision History ---

using System; 
using System.Windows.Forms;
using System.Threading;

namespace Bombardier.PTU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the PTU application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            do
            {
                // Check if any parameters have been passed to the PTU.
                if (args.Length > 0)
                {
                    Application.Run(new MdiPTU(args));
                }
                else
                {
                    Application.Run(new MdiPTU());
                }
            }
            while (MdiPTU.Restart == true);

            Application.Exit();
        }
    }
}