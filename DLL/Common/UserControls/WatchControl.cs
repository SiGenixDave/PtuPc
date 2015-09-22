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
 *  File name:  WatchControl.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  03/18/11    1.1     K.McD           1.  Added the ShowHelpPopup() method to display the diagnostic help information associated with the current watch identifier.
 * 
 *                                      2.  Included a try/catch block in the Identifier property to ensure that an exception is not thrown if an invalid identifier 
 *                                          is specified.
 * 
 *  03/28/11    1.2     K.McD           1.  Modified to use the old identifier to specify the watch variable.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

using Common.Configuration;
using Common.Properties;

namespace Common.UserControls
{
    /// <summary>
    /// The watch variable user control. This user control is the parent of all the user controls that are used to display the fields associated with the VCU 
    /// watch variables 
    /// </summary>
    /// <remarks>The value can be the live value retrieved from the target hardware or the value retrieved from a saved data file.</remarks>
    public partial class WatchControl : VariableControl
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes anew instance of the class.
        /// </summary>
        public WatchControl()
        {
            InitializeComponent();
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

                #region - [Detach the event handler methods.] -
                #endregion - [Detach the event handler methods.] -
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

        #region --- Methods ---
        /// <summary>
        /// Show the watch variable definition using the Windows help pop-up.
        /// </summary>
        protected void ShowHelpPopup()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Get the help index associated with the watch variable.
            int helpIndex;
            try
            {
                helpIndex = Lookup.WatchVariableTableByOldIdentifier.Items[Identifier].HelpIndex;
            }
            catch (Exception)
            {
                helpIndex = CommonConstants.NotFound;
            }

            // If the help index exists, show the help topic associated with the index.
            if (helpIndex != CommonConstants.NotFound)
            {
                WinHlp32.ShowPopup(this.Handle.ToInt32(), helpIndex);
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the old identifier of the watch variable associated with the control and updates the name and units field accordingly.
        /// </summary>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Data"),
        Description("The watch variable identifier associated with the control.")
        ]
        public override int Identifier
        {
            get { return m_Identifier; }
            set
            {
                m_Identifier = value;

                try
                {
                    WatchVariable watchVariable;
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[m_Identifier];
                    if (watchVariable == null)
                    {
                        throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
                    }
                    else
                    {
                        m_LabelNameField.Text = watchVariable.Name;
                        m_LabelUnitsField.Text = watchVariable.Units;
                        General.GetDecimalPlaces(watchVariable.ScaleFactor, out m_DecimalPlaces);
                    }
                }
                catch (Exception)
                {
                    m_LabelNameField.Text = CommonConstants.VariableNotDefinedString;
                    m_LabelUnitsField.Text = CommonConstants.VariableNotDefinedString;
                    m_DecimalPlaces = 0;
                }
            }
        }
        #endregion --- Properties ---
    }
}
