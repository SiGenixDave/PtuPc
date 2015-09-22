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
 *  File name:  FormSetSecurityLevel.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/23/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/18/11    1.1     K.McD           1.  Modified the constructor such that only the security levels available to the client are added to the Items property of the 
 *                                          ComboBox control 
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;

using Common.Configuration;
using Common.Properties;

namespace Common.Forms
{
    /// <summary>
    /// Form to allow the user to modify the security level associated with a workset.
    /// </summary>
    public partial class FormSetSecurityLevel : FormPTUDialog
    {
        #region --- Member Variables ---
        /// <summary>
        /// The selected security level of the workset.
        /// </summary>
        private SecurityLevel m_SecurityLevel;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormSetSecurityLevel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class. Loads the <c>ComboBox</c> control with the security level descriptions associated with the project and sets the 
        /// <c>Text</c> property of the <c>ComboBox</c> control to the description corresponding to the specified security level.
        /// </summary>
        /// <param name="name">The name of the workset.</param>
        /// <param name="securityLevelCurrent">The current security level of the workset.</param>
        public FormSetSecurityLevel(string name, SecurityLevel securityLevelCurrent)
        {
            InitializeComponent();

            m_LabelWorkset.Text = name;

            // Populate the ComboBox control with the available security levels.
            for (short securityLevel = (short)Security.SecurityLevelBase; securityLevel <= (short)Security.SecurityLevelHighest; securityLevel++ )
            {
                string description = Security.GetSecurityDescription((SecurityLevel)securityLevel);
                m_ComboBoxSecurityLevel.Items.Add(description);
            }

            switch (securityLevelCurrent)
            {
                case SecurityLevel.Level0:
                    m_ComboBoxSecurityLevel.Text = Security.DescriptionLevel0;
                    break;
                case SecurityLevel.Level1:
                    m_ComboBoxSecurityLevel.Text = Security.DescriptionLevel1;
                    break;
                case SecurityLevel.Level2:
                    m_ComboBoxSecurityLevel.Text = Security.DescriptionLevel2;
                    break;
                case SecurityLevel.Level3:
                    m_ComboBoxSecurityLevel.Text = Security.DescriptionLevel3;
                    break;
                default:
                    m_ComboBoxSecurityLevel.Text = SecurityLevel.Undefined.ToString();
                    break;
            }
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
        /// Event handler for the Canel button <c>Click</c> event. Close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Event handler for the OK button <c>Click</c> event. Set the <c>SecuityLevel</c> property to the selected security level.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonOK_Click(object sender, EventArgs e)
        {
            if (m_ComboBoxSecurityLevel.Text.Equals(Security.DescriptionLevel0))
            {
                m_SecurityLevel = SecurityLevel.Level0;
            }
            else if (m_ComboBoxSecurityLevel.Text.Equals(Security.DescriptionLevel1))
            {
                m_SecurityLevel = SecurityLevel.Level1;
            }
            else if (m_ComboBoxSecurityLevel.Text.Equals(Security.DescriptionLevel2))
            {
                m_SecurityLevel = SecurityLevel.Level2;
            }
            else
            {
                m_SecurityLevel = SecurityLevel.Undefined;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion --- Delegated Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets the selected security level of the workset.
        /// </summary>
        public SecurityLevel SecurityLevel
        {
            get { return m_SecurityLevel; }
        }
        #endregion --- Properties ---
    }
}