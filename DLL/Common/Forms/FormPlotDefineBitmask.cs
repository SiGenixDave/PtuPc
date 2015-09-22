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
 *  File name:  FormPlotDefineBitmask.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  09/23/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using System.Diagnostics;

using Common.Configuration;
using Common.UserControls;
using Common.Properties;

namespace Common.Forms
{
    /// <summary>
    /// Form used to define which bits of a bitmask watch variable are to be plotted.
    /// </summary>
    public partial class FormPlotDefineBitmask : FormChangeBitmask
    {
        #region --- Member Variables ---
        /// <summary>
        /// The display mask value associated with the selected bitmask watch variable.
        /// </summary>
        private uint m_DisplayMask;

        /// <summary>
        /// Reference to the <c>ListBox</c> control.
        /// </summary>
        private ListBox m_ListBox;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Zero parameter constructor. Required for Visual Studio.
        /// </summary>
        public FormPlotDefineBitmask()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        public FormPlotDefineBitmask(ref ListBox listBox)
        {
            InitializeComponent();

            m_ListBox = listBox;
            if (m_ListBox.SelectedItems.Count != 1)
            {
                throw new Exception(Resources.EMListBoxMultipleSelectionNotSupported);
            }

            m_OldIdentifier = ((WatchItem_t)m_ListBox.SelectedItem).OldIdentifier;
            m_DisplayMask = ((WatchItem_t)m_ListBox.SelectedItem).DisplayMask;
            try
            {
                m_WatchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[m_OldIdentifier];
                if (m_WatchVariable == null)
                {
                    throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
                }
            }
            catch (Exception)
            {
                throw new ArgumentException(Resources.MBTWatchVariableNotDefined);
            }

            Debug.Assert(m_WatchVariable.VariableType == VariableType.Bitmask, "FormPlotDefineBitmask.Ctor() - [m_WatchVariable.VariableType == VariableType.Bitmask]");

            Text = m_WatchVariable.Name;

            #region - [ICheckBoxUInt32] -
            m_ICheckBoxUInt32 = new CheckBoxUInt32();
            CheckBox[] checkBoxes;
            ConfigureCheckBoxes(out checkBoxes);
            m_ICheckBoxUInt32.CheckBoxes = checkBoxes;
            m_ICheckBoxUInt32.SetText(m_OldIdentifier);
            m_ICheckBoxUInt32.SetChecked((uint)m_DisplayMask);
            #endregion - [ICheckBoxUInt32] -

            m_GroupBoxCurrentValue.Visible = false;
            m_GroupBoxNewValue.Visible = false;
            m_GroupBoxFormat.Visible = false;

            // OK, Cancel, Apply
            m_ButtonOK.Location = m_ButtonCancel.Location;
            m_ButtonCancel.Location = m_ButtonApply.Location;
            m_ButtonApply.Visible = false;

            // Now that the display has been initialized, disable the apply button. This will only be re-enabled when the user has modified one or more Checked 
            // properties.
            m_ButtonApply.Enabled = false;

            // Set the default DialogResult value.
            DialogResult = DialogResult.No;
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
        #region - [Buttons] -
        /// <summary>
        /// Event handler for the Apply button <c>Click</c> event. Apply the changes.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void m_ButtonApply_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the ICheckBoxUInt32 interface has not yet been instantiated.
            if (m_ICheckBoxUInt32 == null)
            {
                return;
            }

            WatchItem_t selectedWatchItem = (WatchItem_t)m_ListBox.SelectedItem;
            m_DisplayMask = m_ICheckBoxUInt32.ToValue();
            if (m_DisplayMask != selectedWatchItem.DisplayMask)
            {
                selectedWatchItem.DisplayMask = m_DisplayMask;
                m_ListBox.Items[m_ListBox.SelectedIndex] = selectedWatchItem;

                // Let the calling form know that the display mask value has been modified.
                DialogResult = DialogResult.Yes;
            }
        }

        /// <summary>
        /// Event handler for the 'Clear All' button <c>Click</c> event. Clear the <c>Checked</c> property of all <c>CheckBox</c> controls.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonClearAll_Click(object sender, EventArgs e)
        {
            if (m_ICheckBoxUInt32 != null)
            {
                m_ICheckBoxUInt32.SetChecked(0);
            }

            m_ButtonApply.Enabled = true;
        }

        /// <summary>
        /// Event handler for the 'Invert' button <c>Click</c> event. Invert the <c>Checked</c> property of all <c>CheckBox</c> controls.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ButtonInvert_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the ICheckBoxUInt32 interface has not yet been instantiated.
            if (m_ICheckBoxUInt32 == null)
            {
                return;
            }

            uint value = m_ICheckBoxUInt32.ToValue();

            // Exclusive OR to invert the bits.
            value ^= uint.MaxValue;
            m_ICheckBoxUInt32.SetChecked(value);
            m_ButtonApply.Enabled = true;
        }
        #endregion - [Buttons] -

        /// <summary>
        /// Event handler for each <c>CheckBox</c> control associated with the <c>CheckBoxes</c> property of the <c>ICheckBoxUInt32</c> interface.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        public override void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip, if the ICheckBoxUInt32 interface has not yet been instantiated.
            if (m_ICheckBoxUInt32 == null)
            {
                return;
            }

            m_ICheckBoxUInt32.SetColors();
            m_ButtonApply.Enabled = true;
        }
        #endregion --- Delegated Methods ---
    }
}
