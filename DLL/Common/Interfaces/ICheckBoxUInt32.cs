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
 *  File name:  ICheckBoxUInt32.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/21/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;

namespace Common
{
    /// <summary>
    /// An interface to display the state of the individual flags of a <c>uint</c> bitmask watch variable using an array of <c>CheckBox</c> controls and to 
    /// convert the <c>Checked</c> state of the individual controls in the array back to a <c>uint</c> value.
    /// </summary>
    public interface ICheckBoxUInt32
    {
        #region --- Methods ---
        /// <summary>
        /// Set the <c>Text</c> property of each <c>CheckBox</c> control associated with the <c>CheckBoxes</c> property to the flag description associated 
        /// with the specified bitmask watch variable.
        /// </summary>
        /// <remarks>The <c>CheckBoxes</c> property must be defined prior to making this call.</remarks>
        /// <param name="oldIdentifier">The watch variable old identifier associated with the bitmask watch variable.</param>
        void SetText(short oldIdentifier);

        /// <summary>
        /// Set the <c>Checked</c> property of the <c>CheckBox</c> controls associated with the <c>CheckBoxes</c> property so that they represent 
        /// the specified unsigned integer value.
        /// </summary>
        /// <remarks>The <c>CheckBoxes</c> property must be defined prior to making this call.</remarks>
        /// <param name="value">The value that is to be represented by the Checked properties of the <c>CheckBox</c> controls  associated with the 
        /// <c>CheckBoxes</c> property.</param>
        void SetChecked(uint value);

        /// <summary>
        /// Set the BackColor and ForeColor properties of the <c>CheckBox</c> controls associated with the <c>CheckBoxes</c> property so that they reflect 
        /// the state of the <c>Checked</c> property of each control.
        /// </summary>
        /// <remarks>The <c>CheckBoxes</c> property must be defined prior to making this call.</remarks>
        void SetColors();

        /// <summary>
        /// Convert the <c>Checked</c> property states of the <c>CheckBox</c> controls associated with the <c>CheckBoxes</c> property to a <c>uint</c> value.
        /// </summary>
        /// <remarks>The <c>CheckBoxes</c> property must be defined prior to making this call.</remarks>
        /// <returns>A <c>uint</c> value corresponding to the <c>Checked</c> property states of the <c>CheckBox</c> controls associated with the 
        /// <c>CheckBoxes</c> property.</returns>
        uint ToValue();
        #endregion --- Methods ---

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
        CheckBox[] CheckBoxes { get; set; }
        #endregion --- Properties ---
    }
}
