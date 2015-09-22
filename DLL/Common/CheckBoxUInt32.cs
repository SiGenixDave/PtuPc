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
 *  File name:  CheckBoxUInt32.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/21/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Common.Configuration;

namespace Common
{
    /// <summary>
    /// A class to display the state of the individual flags of a <c>uint</c> bitmask watch variable using an array of <c>CheckBox</c> controls and to 
    /// convert the <c>Checked</c> state of the individual controls in the array back to a <c>uint</c> value.
    /// </summary>
    public class CheckBoxUInt32 : ICheckBoxUInt32
    {
        #region - [Member Variables] -
        /// <summary>
        /// Reference to the 32 element array of <c>CheckBox</c> controls where each <c>CheckBox</c> control represents an individual bit of a 32 bit bitmask
        /// watch variable.
        /// </summary>
        private CheckBox[] m_CheckBoxes;

        /// <summary>
        /// A flag to indicate that the interface has been initialized. True, if the interface has been initialized; otherwise, false.
        /// </summary>
        private bool m_IsInitialized;

        #region - [Colors] -
        /// <summary>
        /// The <c>BackColor</c> associated with the flag if the flag is in the asserted state. Value: Color.Red.
        /// </summary>
        private readonly Color m_BackColorAssertedState = Color.Yellow;

        /// <summary>
        /// The <c>ForeColor</c> associated with the flag if the flag is in the asserted state. Value: Color.Black.
        /// </summary>
        private readonly Color m_ForeColorAssertedState = Color.Black;

        /// <summary>
        /// The <c>BackColor</c> associated with the flag if the flag is NOT in the asserted state i.e. it is clear. Value: Color.WhiteSmoke.
        /// </summary>
        private readonly Color m_BackColorClearState = Color.WhiteSmoke;

        /// <summary>
        /// The <c>ForeColor</c> associated with the flag if the flag is NOT in the asserted state i.e. it is clear. Value: Color.ForrestGreen.
        /// </summary>
        private readonly Color m_ForeColorClearState = Color.ForestGreen;
        #endregion - [Colors] -
        #endregion - [Member Variables] -

        #region - [Methods] -
        /// <summary>
        /// Set the <c>Text</c> property of each <c>CheckBox</c> control associated with the <c>CheckBoxes</c> property to the flag description associated 
        /// with the specified bitmask watch variable.
        /// </summary>
        /// <remarks>The <c>CheckBoxes</c> property must be defined prior to making this call.</remarks>
        /// <param name="oldIdentifier">The watch variable old identifier associated with the bitmask watch variable.</param>
        public void SetText(short oldIdentifier)
        {
            // Skip, if the interface hasn't been initialized.
            if (m_IsInitialized == false)
            {
                return;
            }

            // Get the list of flag descriptions and states corresponding to the current value.
            List<FlagState_t> flagStateList = Lookup.WatchVariableTableByOldIdentifier.GetFlagStateList(oldIdentifier, 0);
            byte bitIndex;
            for (int flagIndex = 0; flagIndex < flagStateList.Count; flagIndex++)
            {
                bitIndex = flagStateList[flagIndex].Bit;
                m_CheckBoxes[bitIndex].Text = flagStateList[flagIndex].Description;
            }
        }

        /// <summary>
        /// Set the <c>Checked</c> property of the <c>CheckBox</c> controls associated with the <c>CheckBoxes</c> property so that they represent 
        /// the specified unsigned integer value.
        /// </summary>
        /// <remarks>The <c>CheckBoxes</c> property must be defined prior to making this call.</remarks>
        /// <param name="value">The value that is to be represented by the Checked properties of the <c>CheckBox</c> controls  associated with the 
        /// <c>CheckBoxes</c> property.</param>
        public void SetChecked(uint value)
        {
            // Skip, if the interface hasn't been initialized.
            if (m_IsInitialized == false)
            {
                return;
            }

            uint bitCount = sizeof(uint) * CommonConstants.BitsPerByte;
            ulong bitmask;
            for (byte bitIndex = 0; bitIndex < bitCount; bitIndex++)
            {
                bitmask = (ulong)0x01 << bitIndex;

                // Check whether the current bit is set or clear.
                if ((value & bitmask) == bitmask)
                {
                    m_CheckBoxes[bitIndex].Checked = true;
                }
                else
                {
                    m_CheckBoxes[bitIndex].Checked = false;
                }
            }

            SetColors();
        }

        /// <summary>
        /// Set the BackColor and ForeColor properties of the <c>CheckBox</c> controls associated with the <c>CheckBoxes</c> property so that they reflect 
        /// the state of the <c>Checked</c> property of each control.
        /// </summary>
        /// <remarks>The <c>CheckBoxes</c> property must be defined prior to making this call.</remarks>
        public void SetColors()
        {
            // Skip, if the interface hasn't been initialized.
            if (m_IsInitialized == false)
            {
                return;
            }

            uint bitCount = sizeof(uint) * CommonConstants.BitsPerByte;
            for (byte bitIndex = 0; bitIndex < bitCount; bitIndex++)
            {
                // Check the state of the Checked property.
                if (m_CheckBoxes[bitIndex].Checked == true)
                {
                    // Asserted state.
                    m_CheckBoxes[bitIndex].ForeColor = m_ForeColorAssertedState;
                    m_CheckBoxes[bitIndex].BackColor = m_BackColorAssertedState;
                }
                else
                {
                    // Clear state.
                    m_CheckBoxes[bitIndex].ForeColor = m_ForeColorClearState;
                    m_CheckBoxes[bitIndex].BackColor = m_BackColorClearState;
                }
            }
        }

        /// <summary>
        /// Convert the <c>Checked</c> property states of the <c>CheckBox</c> controls associated with the <c>CheckBoxes</c> property to a <c>uint</c> value.
        /// </summary>
        /// <remarks>The <c>CheckBoxes</c> property must be defined prior to making this call.</remarks>
        /// <returns>A <c>uint</c> value corresponding to the <c>Checked</c> property states of the <c>CheckBox</c> controls associated with the 
        /// <c>CheckBoxes</c> property.</returns>
        public uint ToValue()
        {
            // Skip, if the interface hasn't been initialized.
            if (m_IsInitialized == false)
            {
                return 0;
            }

            uint convertedValue = 0;
            for (byte bitIndex = 0; bitIndex < m_CheckBoxes.Length; bitIndex++)
            {
                if (m_CheckBoxes[bitIndex].Checked == true)
                {
                    convertedValue += (uint)(0x01 << bitIndex);
                }
            }
            return convertedValue;
        }
        #endregion - [Methods] -

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the reference to the 32 element array of <c>CheckBox</c> controls where each <c>CheckBox</c> control represents an individual bit of 
        /// a 32 bit bitmask watch variable.
        /// </summary>
        /// <example>
        /// // Create an array of CheckBox controls to simplify access.
        /// CheckBoxUInt32 = new CheckBox[32]  {   m_CheckBox00, m_CheckBox01, m_CheckBox02, m_CheckBox03, m_CheckBox04, m_CheckBox05, m_CheckBox06, m_CheckBox07, 
        ///                                        m_CheckBox08, m_CheckBox09, m_CheckBox10, m_CheckBox11, m_CheckBox12, m_CheckBox13, m_CheckBox14, m_CheckBox15, 
        ///                                        m_CheckBox16, m_CheckBox17, m_CheckBox18, m_CheckBox19, m_CheckBox20, m_CheckBox21, m_CheckBox22, m_CheckBox23, 
        ///                                        m_CheckBox24, m_CheckBox25, m_CheckBox26, m_CheckBox27, m_CheckBox28, m_CheckBox29, m_CheckBox30, m_CheckBox31,
        ///                                    };
        ///                                        
        /// where each <c>CheckBox</c> control is instantiated as follows:
        /// 
        ///     CheckBox m_CheckBoxXX = new CheckBox();
        ///     m_CheckBoxXX.Anchor = System.Windows.Forms.AnchorStyles.Left;
        ///     m_CheckBoxXX.AutoEllipsis = true;
        ///     m_CheckBoxXX.Location = new System.Drawing.Point([X], [Y]);
        ///     m_CheckBoxXX.Name = "m_CheckBoxXX";
        ///     m_CheckBoxXX.Size = new System.Drawing.Size([width], [height]);
        ///     m_CheckBoxXX.TabIndex = 0;
        ///     m_CheckBoxXX.TabStop = false;
        ///     m_CheckBoxXX.Text = "Undefined XX";
        ///     m_CheckBoxXX.CheckedChanged += new System.EventHandler(CheckBoxUInt32_CheckedChanged);
        ///     
        /// </example>
        public CheckBox[] CheckBoxes
        {
            get { return m_CheckBoxes; }
            set 
            {
                if (value != null)
                {
                    m_CheckBoxes = value;
                    m_IsInitialized = true;
                }
            }
        }
        #endregion --- Properties ---
    }
}
