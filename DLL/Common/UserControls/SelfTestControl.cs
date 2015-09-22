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
 *  File name:  SelfTestControl.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/07/21    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
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
    /// The self test variable user control. This user control is the parent of all the user controls that are used to display the fields associated with the VCU 
    /// self test variables 
    /// </summary>
    /// <remarks>The value can be the live value retrieved from the target hardware or the value retrieved from a saved data file.</remarks>
    public partial class SelfTestControl : VariableControl
    {
        #region --- Constructors ---
        /// <summary>
        /// Initializes anew instance of the class.
        /// </summary>
        public SelfTestControl()
        {
            InitializeComponent();
        }
        #endregion --- Constructors ---

        #region --- Delegated Methods ---
        #region - [Context Menu] -
        /// <summary>
        /// Event handler for the 'Show Definition' context menu option <c>Click</c> event. Call the ShowHelpPopup() method.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_MenuItemShowDefinition_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            ShowHelpPopup();
        }
        #endregion - [Context Menu] -
        #endregion --- Delegated Methods ---

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
                this.m_LabelNameField.DoubleClick -= new System.EventHandler(this.m_MenuItemShowDefinition_Click);
                this.m_MenuItemShowDefinition.Click -= new System.EventHandler(this.m_MenuItemShowDefinition_Click);
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
        /// Show the event variable definition using the Windows help pop-up.
        /// </summary>
        protected void ShowHelpPopup()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Get the help index associated with the event variable.
            int helpIndex = Lookup.SelfTestVariableTable.Items[Identifier].HelpIndex;
            
            // If the help index exists, show the help topic associated with the index.
            if (helpIndex != CommonConstants.NotFound)
            {
                WinHlp32.ShowPopup(this.Handle.ToInt32(), helpIndex);
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the watch identifier of the watch variable associated with the control and updates the name and units field accordingly.
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
                    m_LabelNameField.Text = Lookup.SelfTestVariableTable.Items[m_Identifier].Name;
                    m_LabelUnitsField.Text = Lookup.SelfTestVariableTable.Items[m_Identifier].Units;
                    General.GetDecimalPlaces(Lookup.SelfTestVariableTable.Items[m_Identifier].ScaleFactor, out m_DecimalPlaces);
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
