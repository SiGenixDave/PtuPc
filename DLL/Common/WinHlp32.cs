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
 *  File name:  WinHlp32.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/13/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  06/21/11    1.1     K.McD           1.  Added the ShowHelpWindow() method. This will show the specified help information using the standard Windows help window.
 *  
 *  07/07/11    1.2     K.McD           1.  Added support for the HideHelpWindow() method.
 *                                      2.  Modified a number of XML comments.
 *                                      
 *  07/25/11    1.3     K.McD           1.  Added the following User32 methods: SetWindowPos(), FindWindow(), GetWindowRect(), SetActiveWindow() and EnableWindow().
 *                                      2.  Added constants to support the methods defined in 1.
 *                                      3.  Added margin constants to support positioning of the help window.
 *                                      4.  Modified the ShowHelpWindow() method to position the help window within the boundary of the form/control associated with the 
 *                                          specified handle.
 *                                          
 *  07/26/11    1.3.1   K.McD           1.  Modified the signature associated with the EnableWindow() method.
 *                                      2.  Changed the MarginLeftHelpWindow constant.
 *                                      3.  Modified the signature of the ShowHelpWindow() method to allow the programmer to specify the hWndInsertAfter parameter.
 *                                      
 *  08/04/11    1.3.2   K.McD           1.  Modified the ShowHelpWindow() method to ensure that the Z order position of the help window is specified in a seperate 
 *                                          call to the SetWindowPos() method.
 *                                          
 *  10/14/11    1.3.3   K.McD           1.  SNCR002.42. Modified the ShowHelpWindow(), ShowPopup() and HideHelpWindow() methods to ensure that they don't throw an 
 *                                          exception if the class hasn't been initialized; instead, they simply ignore the request. The problem with the previous 
 *                                          implementation was that an exception was thrown if the user selected any of the 'Show Definition' context menu options for 
 *                                          a data dictionary that did not have an associated diagnostic help file.
 *                                          
 *                                      2.  SNCR002.43. Ensured that the IsInitialized flag is cleared at the beginning of the Initialize() method in case the user 
 *                                          switches from a data dictionary that has an associated diagnostic help file to one that doesn't - via the 
 *                                          'File/Select Data Dictinary' menu option. With the previous implementation all 'Show Definition' context menu options 
 *                                          for the new data dictionary would display the help information associated with the previous data dictionary.
 *                                          
 *                                      3.  Corrected the check statement associated with the IsInitialized flag in the Close() method.
 *                                      
 *  04/10/15    1.4     K.McD           1.  SNCR - R188 PTU [20 Mar 2015] Item 9. Do not throw an exception if the flag returned from a call to the
 *                                          WinHelp engine isn't true.
 */
#endregion --- Revision History ---

using System;
using System.IO;
using System.Runtime.InteropServices;
using Common.Properties;

namespace Common
{
    /// <summary>
    /// Class to support the display of Windows '.hlp' files using the WinHlp32 help program available from the Microsoft Download Centre.
    /// </summary>
    public class WinHlp32
    {
        #region - [DLL Imports] -
        /// <summary>
        /// Launches Windows Help (Winhelp.exe) and passes additional data that indicates the nature of the help requested by the application.
        /// </summary>
        /// <remarks>Before closing the window that requested help, the application must call WinHelp with the uCommand parameter set to HELP_QUIT. Until all applications
        /// have done this, Windows Help will not terminate. Note that calling Windows Help with the HELP_QUIT command is not necessary if you used the HELP_CONTEXTPOPUP 
        /// command to start Windows Help. This function fails if called from any context but the current user.</remarks>
        /// <param name="hwnd">A handle to the window requesting help. The WinHelp function uses this handle to keep track of which applications have requested help. 
        /// If the uCommand parameter specifies HELP_CONTEXTMENU or HELP_WM_HELP, hWndMain identifies the control requesting help.</param>
        /// <param name="lpHelpFile">The address of a null-terminated string containing the path, if necessary, and the name of the Help file that WinHelp is to
        /// display.</param>
        /// <param name="wCommand">The type of help requested.</param>
        /// <param name="dwData">Additional data. The value used depends on the value of the uCommand parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern unsafe bool WinHelp(int hwnd, string lpHelpFile, short wCommand, int dwData);

        /// <summary>
        /// Get the handle to the window with the specified name.
        /// </summary>
        /// <param name="className">Specify <c>null</c>.</param>
        /// <param name="windowName">The name of the window.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern unsafe int FindWindow(string className, string windowName);

        /// <summary>
        /// Set the position of the window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order. This parameter must be a window handle or one of 
        /// the values defined by the SetWindowsPos - hWndInsertAfter constant values.</param>
        /// <param name="x">The new position of the left side of the window, in client coordinates.</param>
        /// <param name="y">The new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">The new width of the window, in pixels.</param>
        /// <param name="cy">The new height of the window, in pixels. </param>
        /// <param name="uFlags">The window sizing and positioning flags. This parameter can be a combination of the values defined by the SetWindowsPos - uFlags 
        /// constant values.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern unsafe int SetWindowPos(int hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        /// <summary>
        /// Get the size of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="windowRect">The size of the window.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern unsafe int GetWindowRect(int hWnd, out Rect windowRect );
        #endregion - [DLL Imports] -

        #region --- Structures ---
        /// <summary>
        /// A structure to store the size of the window that was retrieved using the user32.GetWindowRect() method.
        /// </summary>
        public struct Rect
        {
            /// <summary>
            /// The position of the left side of the window, in client coordinates.
            /// </summary>
            public int Left;

            /// <summary>
            /// >The position of the top of the window, in client coordinates.
            /// </summary>
            public int Top;

            /// <summary>
            /// The position of the right side of the window, in client coordinates.
            /// </summary>
            public int Right;

            /// <summary>
            /// The position of the bottom of the window, in client coordinates.
            /// </summary>
            public int Bottom;
        }
        #endregion --- Structures ---

        #region --- Constants ---
        #region - [Margins] -
        /// <summary>
        /// The right margin associated with the Windows help window. Value: 2.
        /// </summary>
        public const int MarginRightHelpWindow = 2;

        /// <summary>
        /// The left margin associated with the Windows help window. Value: 2.
        /// </summary>
        public const int MarginLeftHelpWindow = 2;

        /// <summary>
        /// The top margin associated with the Windows help window. Value: 2.
        /// </summary>
        public const int MarginTopHelpWindow = 2;

        /// <summary>
        /// The bottom margin associated with the Windows help window. Value: 10.
        /// </summary>
        public const int MarginBottomHelpWindow = 2;
        #endregion - [Margins] -

        #region - [WinHelp] -
        /// <summary>
        /// Inform Windows Help that it is no longer needed. If no other applications have asked for help, Windows closes Windows Help. Value: 2.
        /// </summary>
        public const short HelpQuit = 2;

        /// <summary>
        /// Display the topic identified by the specified context identifier defined in the [MAP] section of the .hpj file in a pop-up window. Value: 8.
        /// </summary>
        public const short HelpContextPopup = 8;

        /// <summary>
        /// Display the topic identified by the specified context identifier defined in the [MAP] section of the .hpj file. Value: 1.
        /// </summary>
        public const short HelpContext = 1;

        /// <summary>
        /// Set the position of the subsequent pop-up window. The dwData parameter must contain the position data. Use MAKELONG to concatenate the horizontal and 
        /// vertical coordinates into a single value. The pop-up window is positioned as if the mouse cursor were at the specified point when the pop-up window was
        /// invoked. Value: 0xD.
        /// </summary>
        public const short HelpSetPopupPosition = 0xd;

        /// <summary>
        /// Display the Windows Help window, if it is minimized or in memory, and sets its size and position as specified. The deData parameter must contain the address 
        /// of a HELPWININFO structure that specifies the size and position of either a primary or secondary Help window. Value: 0x203.
        /// </summary>
        public const short HelpSetWindowPosition = 0x203;
        #endregion - [WinHelp] -

        #region - [SetWindowPos] -
        /// <summary>
        /// SetWindowsPos - hWndInsertAfter. Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its 
        /// topmost status and is placed at the bottom of all other windows. Value: 1.
        /// </summary>
        public const int HWND_BOTTOM = 1;

        /// <summary>
        /// SetWindowsPos - hWndInsertAfter. Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the 
        /// window is already a non-topmost window. Value: -2.
        /// </summary>
        public const int HWND_NOTOPMOST = -2;

        /// <summary>
        /// SetWindowsPos - hWndInsertAfter. Places the window at the top of the Z order. Value: 0.
        /// </summary>
        public const int HWND_TOP = 0;

        /// <summary>
        /// SetWindowsPos - hWndInsertAfter. Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated. 
        /// Value: -1.
        /// </summary>
        public const int HWND_TOPMOST = -1;

        /// <summary>
        /// SetWindowsPos - uFlags. Retains the current Z order (ignores hWndInsertAfter parameter). Value: 0x0004.
        /// </summary>
        public const uint SWP_NOZORDER = 0x0004;

        /// <summary>
        /// SetWindowsPos - uFlags. Displays the window. Value: 0x0040.
        /// </summary>
        public const uint SWP_SHOWWINDOW = 0x0040;

        /// <summary>
        /// SetWindowsPos - uFlags. Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or 
        /// non-topmost group (depending on the setting of the hWndInsertAfter parameter). Value: 0x0010.
        /// </summary>
        public const uint SWP_NOACTIVATE = 0x0010;

        /// <summary>
        /// SetWindowsPos - uFlags. Retains the current size (ignores the cx and cy parameters). Value: 0x0001.
        /// </summary>
        public const uint SWP_NOSIZE = 0x0001;

        /// <summary>
        /// SetWindowsPos - uFlags. Retains the current position (ignores X and Y parameters). Value: 0x0002.
        /// </summary>
        public const uint SWP_NOMOVE = 0x0002;
        #endregion - [SetWindowPos] -

        #region - [FindWindow] -
        /// <summary>
        /// The name of the Windows help window.
        /// </summary>
        private const string WindowsHelpWindowName = "Windows Help";
        #endregion - [FindWindow] -
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// The fully qualified filename of the help file (.hlp).
        /// </summary>
        private static string m_FullQualifiedHelpFilename;

        /// <summary>
        /// A flag to indicate whether the WinHlp32 help engine has been initialized. True, if the help engine has been initialized; otherwise, false.
        /// </summary>
        private static bool m_IsInitialized;
        #endregion --- Member Variables ---

        #region --- Methods ----
        /// <summary>
        /// Initialize the WinHlp32 help engine.
        /// </summary>
        /// <param name="helpFilename">The fully qualified filename of the help  file.</param>
        /// <exception cref="ArgumentException">Thrown if the help file does not exist or the extension is invalid.</exception>
        public static void Initialize(string helpFilename)
        {
            // As we are trying to load a new diagnostic help file, ensure that the IsInitialized flag is clear. SNCR002.43.
            m_IsInitialized = false;

            // Check that the specified help file exists and has the correct extension.
            FileInfo fileInfo = new FileInfo(helpFilename);
            if (fileInfo.Exists == false)
            {
                throw new ArgumentException(Resources.EMFileNotFound, "helpFilename");
            }
            else if (fileInfo.Extension != CommonConstants.ExtensionHelpFile)
            {
                throw new ArgumentException(Resources.EMHelpFileFormatInvalid, "helpFilename");
            }
            else
            {
                m_FullQualifiedHelpFilename = helpFilename;
                m_IsInitialized = true;
            }
        }

        /// <summary>
        /// Close the WinHlp32 help engine.
        /// </summary>
        /// <param name="winHandle">A handle to the window making the request.</param>
        public static void Close(int winHandle)
        {
            // Only Close the help engine if it has been initialized.
            if (IsInitialized == true)
            {
                bool success = WinHelp(winHandle, m_FullQualifiedHelpFilename, HelpQuit, 0);
                if (success != true)
                {
                    // No need to throw an exception as the help system will show a window explaining why the call failed.
                }
            }
        }

        /// <summary>
        /// Show the help topic in a separate window within the form/control associated with the specified handle.
        /// </summary>
        /// <remarks>The WinHlp32 help engine must have been initialized prior to calling this method.</remarks>
        /// <param name="winHandle">A handle to the control/form that defines the boundary of the help window.</param>
        /// <param name="hWndInsertAfter">A value that defines the Z order of the help window. HWND_BOTTOM, HWND_NOTOPMOST, HWND_TOP, HWND_TOPMOST.</param>
        /// <param name="helpIndex">The help index corresponding to the topic that is to be shown in the help window.</param>
        public static void ShowHelpWindow(int winHandle, int helpIndex, int hWndInsertAfter)
        {
            Rect rect;
            int x, y, cx, cy;
            int hwnd;

            if (IsInitialized == true)
            {
                // Show the Windows help window.
                bool success = WinHelp(winHandle, m_FullQualifiedHelpFilename, HelpContext, helpIndex);
                if (success != true)
                {
                    // No need to throw an exception as the help system will show a window explaining why the call failed.
                }

                // Get the size of the Windows help window.
                GetWindowRect(winHandle, out rect);
                x = rect.Left + MarginLeftHelpWindow;
                y = rect.Top + MarginTopHelpWindow;
                cx = rect.Right - rect.Left - (MarginLeftHelpWindow + MarginRightHelpWindow);
                cy = rect.Bottom - rect.Top - (MarginBottomHelpWindow + MarginTopHelpWindow);

                // Get the handle of the Windows help window.
                hwnd = FindWindow(null, WindowsHelpWindowName);

                // Position the window.
                SetWindowPos(hwnd, HWND_TOP, x, y, cx, cy, SWP_NOZORDER);

                // Set the window Z order to be defined by the hWndInsertAfter parameter.
                SetWindowPos(hwnd, hWndInsertAfter, 0, 0, 0, 0, SWP_SHOWWINDOW | SWP_NOACTIVATE | SWP_NOSIZE | SWP_NOMOVE);
            }
        }

        /// <summary>
        /// Hide the help topic.
        /// </summary>
        /// <remarks>The WinHlp32 help engine must have been initialized prior to calling this method.</remarks>
        /// <param name="winHandle">A handle to the window requesting help.</param>
        public static void HideHelpWindow(int winHandle)
        {
            if (IsInitialized == true)
            {
                bool success = WinHelp(winHandle, m_FullQualifiedHelpFilename, HelpQuit, 0);
                if (success != true)
                {
                    // No need to throw an exception as the help system will show a window explaining why the call failed.
                }
            }
        }

        /// <summary>
        /// Show the help topic in a pop-up window.
        /// </summary>
        /// <remarks>The WinHlp32 help engine must have been initialized prior to calling this method.</remarks>
        /// <param name="winHandle">A handle to the window requesting help.</param>
        /// <param name="helpIndex">The help index corresponding to the topic that is to be shown in the pop-up window.</param>
        public static void ShowPopup(int winHandle, int helpIndex)
        {
            if (IsInitialized == true)
            {
                bool success = WinHelp(winHandle, m_FullQualifiedHelpFilename, HelpContextPopup, helpIndex);
                if (success != true)
                {
                    // No need to throw an exception as the help system will show a window explaining why the call failed.
                }
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the flag that indicates whether the WinHlp32 help engine has been initialized. True, if the help engine has been initialized; otherwise, false.
        /// </summary>
        public static bool IsInitialized
        {
            get { return m_IsInitialized; }
        }
        #endregion --- Properties ---
    }
}