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
 *  File name:  FormShowHeaderInformation.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  08/13/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/25/10    1.1     K.McD           1.  Added cleanup code region for consistency.
 * 
 *  01/05/11    1.2     K.McD           1.  Modified the signature of the constructor so that a Header_t structure is passed as a parameter instead of the WatchFile_t 
 *                                          structure.
 * 
 *  04/07/11    1.3     K.McD           1.  SNCR001.115 - Bug fix. Display the saved username rather then the current Windows username.
 * 
 *  10/02/11    1.4     K.McD           1.  Added the Header_t member variable to record the header information passed in the constructor.
 *                                      2.  Renamed the event handler associated with the OK button Click event and added support to save the comments to the IWatchFile 
 *                                          interface associated with the calling form.
 * 
 *  10/05/11    1.5		Sean.D          1.  SNCR002.12 - Bug fix. Changed constructor to run a regular expression replacement for lone Line Feed characters.
 *  
 *  10/10/11    1.6     Sean.D          1   SNCR002.12. - Improved the regular expression logic to find consecutive multiple carriage returns/line feeds.
 * 
 *  10/11/11    1.5.1	Sean.D          1.  SNCR002.12 - Bug fix. Modified code in constructor to check for native NewLine and to catch consecutive \n characters.
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Common.Forms
{
	/// <summary>
	/// Form to show the header information associated with a data file.
	/// </summary>
	public partial class FormShowHeaderInformation : FormPTUDialog
	{
        #region --- Member Variables ---
        /// <summary>
        /// The existing header information.
        /// </summary>
        private Header_t m_Header;
        #endregion --- Member Variables ---

		#region --- Constructors ---
		/// <summary>
		/// Initializes a new instance of the class. Zero parameter constructor, required for Visual Studio.
		/// </summary>
		public FormShowHeaderInformation()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes a new instance of the class. Zero parameter constructor.
		/// </summary>
		public FormShowHeaderInformation(Header_t header)
		{
			InitializeComponent();

			m_LabelVersionPTU.Text = header.ProductVersion;
			m_LabelVersionDDB.Text = header.ProjectInformation.DataDictionaryBuilderVersion;

			m_LabelVersionDD.Text = header.ProjectInformation.Version;
			m_LabelNameDD.Text = header.ProjectInformation.DataDictionaryName;
			m_LabelProjectIdentifierDD.Text = header.ProjectInformation.ProjectIdentifier;

			m_LabelVersionVCU.Text = header.TargetConfiguration.Version;
			m_LabelSubsystemVCU.Text = header.TargetConfiguration.SubSystemName;
			m_LabelProjectIdentifierVCU.Text = header.TargetConfiguration.ProjectIdentifier;
			m_LabelCarIdentifierVCU.Text = header.TargetConfiguration.CarIdentifier;

			m_LabelUser.Text = header.UserName;
			m_LabelDate.Text = header.DateTimeCreated.ToShortDateString();
			m_LabelTime.Text = header.DateTimeCreated.ToShortTimeString();

			// Windows machines handle newlines by inserting a carriage-return line-feed. Linux-based systems just use a
			// line feed, as does XML encoding. Therefore, we check to see if the environment we're working in uses the
			// Windows schema. If so, we cycle through until all cases of a non-carriage return followed by a line-feed
			// is replaced by the character followed by an Environment.NewLine.
			if (Environment.NewLine == "\r\n")
			{
				Regex regEx = new Regex("([^\r])\n");

				while (regEx.Match(header.Comments).Value != String.Empty)
				{
					header.Comments = regEx.Replace(header.Comments, "$1" + Environment.NewLine);
				}
			}

			m_TextBoxComments.Text = header.Comments;
		

            m_Header = header;
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
				}

				// Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.

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
        /// Event handler for the OK button <c>Click</c> event. Closes the form.
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

            // Check whether the comments text of the header has been modified.
            if (m_TextBoxComments.Text != m_Header.Comments)
            {
                Cursor = Cursors.WaitCursor;

                // Check whether the calling form implements the IWatchFile interface.
                IWatchFile iWatchFile = CalledFrom as IWatchFile;
                if (iWatchFile != null)
                {
                    // Yes - Update the WatchFile property with the current header.
                    WatchFile_t watchFile = iWatchFile.WatchFile;
                    watchFile.Header.Comments = m_TextBoxComments.Text;
                    iWatchFile.WatchFile = watchFile;
                    iWatchFile.SaveWatchFile();
                }
                else
                {
                    // No - Check whether the calling form implements the IEventLogFIle interface.
                    IEventLogFile iEventLogFile = CalledFrom as IEventLogFile;
                    if (iEventLogFile != null)
                    {
                        // Yes - Update the EventLogFile property with the current header.
                        EventLogFile_t eventLogFile = iEventLogFile.EventLogFile;
                        eventLogFile.Header.Comments = m_TextBoxComments.Text;
                        iEventLogFile.EventLogFile = eventLogFile;
                        iEventLogFile.SaveEventLogFile();
                    }
                }

                Cursor = Cursors.Default;
            }

			Close();
		}
		#endregion --- Delegated Methods ---
	}
}