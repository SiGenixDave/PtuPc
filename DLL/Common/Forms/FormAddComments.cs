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
 *  File name:  FormAddComments.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  06/10/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  08/13/10    1.1     K.McD           1.  Moved the GetUserName() method to the General class.
 *  01/31/11    1.2     K.McD           1.  Made a number of properties write enabled.
 *                                      2.  Included a form Shown event handler which displays the current Comments property.
 *                                      
 *  05/23/11    1.3     K.McD           1.  Corrected a number of XML tags.
 *                                      
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Common.Configuration;

namespace Common.Forms
{
    /// <summary>
    /// Class to enable the user to add the username and comments fields to the specified file header.
    /// </summary>
    public partial class FormAddComments : FormPTUDialog
    {
        #region --- Member Variables ---
        /// <summary>
        /// Class copy of the header parameter.
        /// </summary>
        private Header_t m_Header;

        /// <summary>
        /// Class copy of parameter of the date and time parameter.
        /// </summary>
        private DateTime m_DateTimeCreated;

        /// <summary>
        /// The name of the user.
        /// </summary>
        private string m_UserName;

        /// <summary>
        /// The comments entered into the Comments <c>TextBox</c>.
        /// </summary>
        private string m_Comments;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="header">The header to which the user entered data is to be appended.</param>
        /// <param name="dateTimeCreated">The date and time to be used as the reference for the date and time that the file was created.</param>
        public FormAddComments(Header_t header, DateTime dateTimeCreated)
        {
            InitializeComponent();

            m_TextBoxUserName.Text = General.GetUsername();

            // Copy header information into the appropriate member variable.
            m_Header = header;

            // Copy the date and time when the file was created into the appropriate member variable.
            m_DateTimeCreated = dateTimeCreated;
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
        #region - [Form] -
        /// <summary>
        /// Event handler for the <c>Shown</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormAddComments_Shown(object sender, EventArgs e)
        {
            m_TextBoxComments.Text = Comments;
        }
        #endregion - [Form] -

        /// <summary>
        /// Event handler for the OK button <c>Click</c> event. Updates the properties and then closes the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_BtnOK_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Update the UserName and Comments properties with the user data.
            m_UserName = m_TextBoxUserName.Text;
            m_Comments = m_TextBoxComments.Text;
            
            // Update the header properties.
            m_Header.DateTimeCreated = m_DateTimeCreated;
            m_Header.UserName = m_UserName;
            m_Header.Comments = m_Comments;

            // Update the PTU ProductVersion recorded in the header. 
            m_Header.ProductVersion = Application.ProductVersion;
            Close();
        }
        #endregion --- Delegated Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the name, as entered into the Name <c>TextBox</c>.
        /// </summary>
        public string UserName
        {
            get { return m_UserName; }
        }

        /// <summary>
        /// Gets or sets the comments field of the form.
        /// </summary>
        public string Comments
        {
            get { return m_Comments; }
            set { m_Comments = value; }
        }

        /// <summary>
        /// Gets or sets the date and time, as a .NET <c>DateTime</c> object, to be used as the reference for the creation date and time of the log file.
        /// </summary>
        public DateTime DateTimeCreated
        {
            get { return m_DateTimeCreated; }
            set { m_DateTimeCreated = value; }
        }

        /// <summary>
        /// Gets or sets the header associated with the form.
        /// </summary>
        public Header_t Header
        {
            get { return m_Header; }
            set 
            { 
                m_Header = value;

                // Now update the other properties to the values contained within the new header.
                m_UserName = m_Header.UserName;
                Comments = m_Header.Comments;
                DateTimeCreated = m_Header.DateTimeCreated;
            }
        }
        #endregion --- Properties ---
    }
}