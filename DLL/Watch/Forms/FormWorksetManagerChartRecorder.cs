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
 *  Project:    Watch
 * 
 *  File name:  FormWorksetManagerChartRecorder.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/11/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  05/23/11    1.2     K.McD           1.  Added the call to the InitializeComponent() method to the zero parameter constructor.
 *                                      2.  Applied the 'Organize Usings/Remove and Sort' context menu option.
 *                                      3.  Modified the constructor to ensure that the default workset column is displayed.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;

using Common.Configuration;
using Common.Forms;

namespace Watch.Forms
{
    /// <summary>
    /// Form to manage the the worksets associated with the chart recorder. Allows the user to: edit, add and delete individual worksets or 
    /// set any of the worksets to be the default workset.
    /// </summary>
    public partial class FormWorksetManagerChartRecorder : FormWorksetManager
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor, required by Visual Studio.
        /// </summary>
        public FormWorksetManagerChartRecorder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class. Records the securty level of the user and enables/disables the 'Set as Default' context menu option accordingly.
        /// </summary>
        /// <param name="worksetCollection">The workset collection that is to be managed.</param>
        public FormWorksetManagerChartRecorder(WorksetCollection worksetCollection)
            : base(worksetCollection)
        {
            InitializeComponent();

            // Display the default workset column.
            m_ColumnHeaderWorsetName.ImageIndex = ImageIndexBookmark;
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

        #region - [Methods] -
        /// <summary>
        /// Call the form which allows the user to edit the selected workset.
        /// </summary>
        /// <param name="selectedItem">The selected workset item.</param>
        protected override void EditSelectedWorkset(WorksetItem selectedItem)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Show the form which allows the user to edit the workset.
            FormWorksetDefineChartRecorder formWorksetDefineChartRecorder = new FormWorksetDefineChartRecorder(m_WorksetCollection, selectedItem.Workset, false);

            // Allow the shown form to re-highlight the selected configuration before it closes.
            formWorksetDefineChartRecorder.CalledFrom = this;
            formWorksetDefineChartRecorder.ShowDialog();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Call the form which allows the user to create a new workset.
        /// </summary>
        protected override void CreateNewWorkset()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            FormWorksetDefineChartRecorder formWorksetDefineChartRecorder = new FormWorksetDefineChartRecorder(m_WorksetCollection);

            // Allow the shown form to re-highlight the selected configuration before it closes.
            formWorksetDefineChartRecorder.CalledFrom = this;
            formWorksetDefineChartRecorder.ShowDialog();
            Cursor = Cursors.Default;
        }
        #endregion - [Methods] -
    }
}