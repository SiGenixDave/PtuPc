#region --- Revision History ---
/*
 * 
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  ScreenCapture.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  03/25/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  07/29/11    1.1     K.McD           1.  Corrected a number of XML tags.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CodeProject.GraphComponents
{
	/// <summary>
	/// Class to provide support functions to capture the entire desktop or a particular window and save it to disk in the specified format.
	/// </summary>
	public class ScreenCapture
	{
        /// <summary>
        /// Capture a screen shot of the entire desktop.
        /// </summary>
        /// <returns>The desktop as an <c>Image</c> object.</returns>
        public Image CaptureScreen() 
        {
            return CaptureWindow( User32.GetDesktopWindow() );
        }

        /// <summary>
        /// Capture a screen shot of a specific window.
        /// </summary>
        /// <param name="handle">The window handle.</param>
        /// <remarks>The window <c>handle</c>is obtained using the <c>Handle</c>property of the <c>Form</c> class.</remarks>
        /// <returns>The window as an <c>Image</c> object.</returns>
        public Image CaptureWindow(IntPtr handle)
        {
            // Get the device context of the source window.
            IntPtr hdcSrc = User32.GetWindowDC(handle);

            // Get the size of the window.
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle,ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;

            // Create device context for the destination.
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);

            // Create a bitmap of the source window.
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);

            // Select the bitmap object.
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);

            // Bit Block Transfer - BITBLT the data.
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);

            // Restore selection.
            GDI32.SelectObject(hdcDest, hOld);

            // Clean up. 
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);

            // Create an Image object corresponding to the bitmap.
            Image image = Image.FromHbitmap(hBitmap);

            // Free up the bitmap object.
            GDI32.DeleteObject(hBitmap);

            return image;
        }

        /// <summary>
        /// Capture a screen shot of a specific window and saves it to disk.
        /// </summary>
        /// <param name="handle">The window handle.</param>
        /// <param name="fullFilename">The fully qualified file name.</param>
        /// <param name="imageFormat">The graphics format associated with the saved file e.g. png, jpg, bmp etc.</param>
        /// <remarks>The window <c>handle</c>is obtained using the <c>Handle</c>property of the <c>Form</c> class.</remarks>
        public void CaptureWindowToFile(IntPtr handle, string fullFilename, ImageFormat imageFormat) 
        {
            Image image = CaptureWindow(handle);
            image.Save(fullFilename, imageFormat);
        }

        /// <summary>
        /// Capture a screen shot of the entire desktop and save it to disk.
        /// </summary>
        /// <param name="fullFilename">The fully qualified file name.</param>
        /// <param name="imageFormat">The graphics format associated with the saved file e.g. png, jpg, bmp etc.</param>
        public void CaptureScreenToFile(string fullFilename, ImageFormat imageFormat) 
        {
            Image image = CaptureScreen();
            image.Save(fullFilename, imageFormat);
        }
       
        /// <summary>
        /// Helper class containing the required Gdi32 API functions.
        /// </summary>
        private class GDI32
        {
            /// <summary>
            /// -
            /// </summary>
            public const int SRCCOPY = 0x00CC0020;

            /// <summary>
            /// Bit block transfer.
            /// </summary>
            /// <param name="hObject">The device context of the target.</param>
            /// <param name="nXDest">The X coordinate of the device context where the bitmap is to be copied from.</param>
            /// <param name="nYDest">The Y coordinate of the device context where the bitmap is to be copied from.</param>
            /// <param name="nWidth">The width of the bitmap.</param>
            /// <param name="nHeight">The height of the bitmap.</param>
            /// <param name="hObjectSource">The device context of the source.</param>
            /// <param name="nXSrc">The X coordinate of the device context where the bitmap is to be copied to.</param>
            /// <param name="nYSrc">The y coordinate of the device context where the bitmap is to be copied to.</param>
            /// <param name="dwRop">-</param>
            /// <returns>A flag to indicate whether bit block transfer was successful. True, if the bit block transfer was sucessfully deleted; otherwise, false.</returns>
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject,int nXDest,int nYDest,
                int nWidth,int nHeight,IntPtr hObjectSource,
                int nXSrc,int nYSrc,int dwRop);

            /// <summary>
            /// Create a compatible bitmap.
            /// </summary>
            /// <param name="hDC">The device context of the source.</param>
            /// <param name="nWidth">The width of the bitmap.</param>
            /// <param name="nHeight">The height of the bitmap.</param>
            /// <returns>Pointer to the bitmap image.</returns>
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

            /// <summary>
            /// Create a new device context that is compatible with specified device context.
            /// </summary>
            /// <param name="hDC">The source device context.</param>
            /// <returns>A new device context that is compatible with the source device context.</returns>
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

            /// <summary>
            /// Delete the specified device context.
            /// </summary>
            /// <param name="hDC">The device context.</param>
            /// <returns>A flag to indicate whether the device context was successfully deleted. True, if the device context was sucessfully deleted; otherwise, false.</returns>
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hObject"></param>
            /// <returns>A flag to indicate whether the object was successfully deleted. True, if the object was sucessfully deleted; otherwise, false.</returns>
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hDC"></param>
            /// <param name="hObject"></param>
            /// <returns></returns>
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }
 
        /// <summary>
        /// Helper class containing the required User32 API functions
        /// </summary>
        private class User32
        {
            /// <summary>
            /// 
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            /// <summary>
            /// Get the handle of the user's desktop.
            /// </summary>
            /// <returns>The handle of the user desktop.</returns>
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();

            /// <summary>
            /// Get the device context of the specified window.
            /// </summary>
            /// <param name="hWnd">The window handle.</param>
            /// <returns>-</returns>
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);

            /// <summary>
            /// Release the device context associated with the specified window.
            /// </summary>
            /// <param name="hWnd">The window handle.</param>
            /// <param name="hDC">The device context associated with the window.</param>
            /// <returns>-</returns>
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

            /// <summary>
            /// Get the <c>RECT</c> associated with the specified window. Used to get the width and height of the window.
            /// </summary>
            /// <param name="hWnd">The window handle.</param>
            /// <param name="rect">The rectangle associated with the specified window.</param>
            /// <returns>-</returns>
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
	}
}
