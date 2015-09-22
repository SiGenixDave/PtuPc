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
 *  File name:  FormShowFlagsSelfTest.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/28/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;

using Common.Forms;
using Common.Configuration;

namespace Common.UserControls
{
    /// <summary>
    /// Form to display the state of the individual bits within a bit mask self test variable.
    /// </summary>
    public partial class FormShowFlagsSelfTest : FormShowFlags
    {
        #region --- Member Variables ---
        /// <summary>
        /// The self test variable identifier associated with the bitmask user control that called this form.
        /// </summary>
        private readonly short m_SelfTestVariableIdentifier;

        /// <summary>
        /// The name of the self test variable that appears in the form title.
        /// </summary>
        private readonly string m_SelfTestVariableName;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the form. Zero parameter constructor.
        /// </summary>
        public FormShowFlagsSelfTest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new instance of the form.
        /// </summary>
        /// <param name="variableControl">Reference to the user control that called this form.</param>
        public FormShowFlagsSelfTest(VariableControl variableControl)
        {
            InitializeComponent();

            m_VariableControl = variableControl;

            // Add this form to the list of opened dialog forms associated with the client form.
            m_ClientAsFormPTU = m_VariableControl.ClientForm as FormPTU;
            if (m_ClientAsFormPTU != null)
            {
                m_ClientAsFormPTU.OpenedDialogBoxList.Add(this);
            }

            m_SelfTestVariableIdentifier = (short)m_VariableControl.Identifier;
            m_SelfTestVariableName = m_VariableControl.VariableNameFieldText;

            // Get a list of the current state of each flag and keep a record to determine if there is a change in state.
            List<FlagState_t> flagStateList = Lookup.SelfTestVariableTable.GetFlagStateList(m_SelfTestVariableIdentifier, m_PreviousValue = GetValue());

            m_TableLayoutPanel = ConstructLayoutPanel();
            m_PanelFlagList.Controls.Add(m_TableLayoutPanel);
            ConfigureTableLayoutPanel(m_TableLayoutPanel, flagStateList);

            UpdateTitle(m_SelfTestVariableName, GetValue());
            UpdateFlagStates(m_TableLayoutPanel.Controls, flagStateList);
            CheckHeight();
            PositionTheForm(m_VariableControl);
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
    }
}