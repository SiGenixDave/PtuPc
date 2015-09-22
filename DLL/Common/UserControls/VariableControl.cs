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
 *  File name:  VariableControl.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  12/30/10    1.1     K.McD           2.  Bug fix - SNCR001.19. Included a static ReadOnly property which forces the user control to be read-only.
 * 
 *  03/18/11    1.2     K.McD           1.  Auto-modified as a result of name changes to a number of properties associated with the Security class.
 *  
 *  05/23/11    1.3     K.McD           1.  Corrected an XML tag.
 *                                      2.  Applied the 'Organize Usings/Remove and Sort' context menu.
 * 
 */
#endregion --- Revision History ---

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Common.Communication;

namespace Common.UserControls
{
    /// <summary>
    /// The generic VCU variable user control. This user control is the parent of all the user controls that are used to display the fields associated 
    /// with all VCU variables 
    /// </summary>
    /// <remarks>The value can be the live value retrieved from the target hardware or the value retrieved from a saved data file.</remarks>
    public partial class VariableControl : UserControl
    {
        #region --- Constants ---
        /// <summary>
        /// The .NET format string used to display the Value property of the scalar user control.
        /// </summary>
        protected const string FormatStringNumericString = "###,###,##0.####";

        /// <summary>
        /// Identifier used to identify a value as a hexadecimal value.
        /// </summary>
        protected const string HexValueIdentifier = "0x";

        /// <summary>
        /// The format string, converted to lower case, used in the data dictionary to represent a general number. Value: "general number";
        /// </summary>
        protected const string FormatStringFieldGeneralNumber = "general number";

        /// <summary>
        /// The format string, converted to lower case, used in the data dictionary to represent a hexadecimal number. Value: "hexadecimal";
        /// </summary>
        protected const string FormatStringFieldHexadecimal = "hexadecimal";

        /// <summary>
        /// The format string to display a value using hexadecimal format e.g. 0F0A. Value: "X".
        /// </summary>
        protected const string FormatStringHex = "X";
        
        /// <summary>
        /// The default width of the variable name field. Value: 200.
        /// </summary>
        protected const int DefaultWidthVariableNameField = 200;

        /// <summary>
        /// The default width of the value field. Value: 100.
        /// </summary>
        private const int DefaultWidthValueField = 100;

        /// <summary>
        /// The default width of the units field. Value: 100.
        /// </summary>
        private const int DefaultWidthUnitsField = 100;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Flag to indicate whether the Dispose() method has been called. True indicates that the Dispose() method has been called; otherwise, false.
        /// </summary>
        private bool m_IsDisposed;

        #region - [Static Colours] -
        /// <summary>
        /// Background colour associated with highlighted text.
        /// </summary>
        protected static Color m_Highlight = Color.FromKnownColor(KnownColor.Highlight);

        /// <summary>
        /// Foreground colour associated with highlighted text.
        /// </summary>
        protected static Color m_HighlightText = Color.FromKnownColor(KnownColor.HighlightText);
        #endregion - [Static Colours] -

        #region - [Colours] -
        /// <summary>
        /// The background colour associated with the variable name field and the units field.
        /// </summary>
        protected Color m_BackColor;

        /// <summary>
        /// The foreground colour associated with the variable name field and the units field. 
        /// </summary>
        protected Color m_ForeColor;

        /// <summary>
        /// The background colour associated with the value field for a value of zero.
        /// </summary>
        protected Color m_BackColorValueFieldZero;

        /// <summary>
        /// The foreground colour associated with the value field for a value of zero.
        /// </summary>
        protected Color m_ForeColorValueFieldZero;

        /// <summary>
        /// The background colour associated with the value field for any value other than zero.
        /// </summary>
        protected Color m_BackColorValueFieldNonZero;

        /// <summary>
        /// The foreground colour associated with the value field for a value other than zero.
        /// </summary>
        protected Color m_ForeColorValueFieldNonZero;
        #endregion - [Colours] -

        /// <summary>
        /// Reference to the client form.
        /// </summary>
        protected Form m_ClientForm;

        /// <summary>
        /// The <c>Font</c> associated with the client form.
        /// </summary>
        protected Font m_ClientFont;

        /// <summary>
        /// The identifier of the watch variable associated with the control.
        /// </summary>
        protected int m_Identifier;

        /// <summary>
        /// The variable attribute flags that are mapped to this control.
        /// </summary>
        protected AttributeFlags m_AttributeFlags;

        /// <summary>
        /// A flag to indicate whether the variable mapped to the control is write-enabled.
        /// </summary>
        protected bool m_WriteEnabled;

        /// <summary>
        /// The value of the variable that has been mapped to this control.
        /// </summary>
        protected double m_Value;

        /// <summary>
        /// The number of decimal places to be used when displaying the data.
        /// </summary>
        protected int m_DecimalPlaces;

        /// <summary>
        /// The previous value of the <c>SecurityLevel</c> enumerator for this control.
        /// </summary>
        private SecurityLevel m_PreviousSecurityLevel;

        /// <summary>
        /// A static flag to indicate that the data associated with the <c>Value</c> field is currently invalid. This will apply to all user controls derived from this class.
        /// </summary>
        private static bool m_InvalidValue;

        /// <summary>
        /// A static control flag used to force all user controls derived from this user control to be read-only. This will apply to all user controls derived from this class.
        /// </summary>
        private static bool m_ReadOnly;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the user control.
        /// </summary>
        public VariableControl()
        {
            InitializeComponent();

            // Keep a record of the background and foreground colours.
            m_BackColor = this.BackColor;
            m_ForeColor = this.ForeColor;

            // Set the width of the labels to the default values.
            m_LabelNameField.Width = DefaultWidthVariableNameField;
            m_LabelValueField.Width = DefaultWidthValueField;
            m_LabelUnitsField.Width = DefaultWidthUnitsField;

            // Set the width of the user control.
            Width = m_LabelNameField.Width + m_LabelValueField.Width + m_LabelUnitsField.Width;
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Cleanup(bool disposing)
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
                this.m_LabelNameField.Click -= new System.EventHandler(this.WatchControl_Click);
                this.m_LabelNameField.Leave -= new System.EventHandler(this.WatchControl_Leave);
                this.m_LabelValueField.Click -= new System.EventHandler(this.WatchControl_Click);
                this.m_LabelValueField.Leave -= new System.EventHandler(this.WatchControl_Leave);
                this.m_LabelUnitsField.Click -= new System.EventHandler(this.WatchControl_Click);
                this.m_LabelUnitsField.Leave -= new System.EventHandler(this.WatchControl_Leave);
                this.GotFocus -= new System.EventHandler(this.WatchControl_GotFocus);
                this.Leave -= new System.EventHandler(this.WatchControl_Leave);
                this.ForeColorChanged -= new System.EventHandler(this.WatchControl_ForeColorChanged);
                this.Click -= new System.EventHandler(this.WatchControl_Click);
                #endregion - [Detach the event handler methods.] -
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
            }
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        /// <summary>
        /// Event handler for the user control <c>Click</c> event. Sets the focus to the current user control to ensure that: (a) the <c>Leave</c> event handler for
        /// the previously selected user control is called and (b) the <c>GotFocus</c> event handler for this control is called.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void WatchControl_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Ensure that the 'Leave' event handler is called for the previously selected control.
            this.Focus();
        }

        /// <summary>
        /// Event handler for the user control <c>GotFocus</c>  event. Highlights the user control.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void WatchControl_GotFocus(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Highlight the control.
            m_LabelNameField.BackColor = m_Highlight;
            m_LabelNameField.ForeColor = m_HighlightText;
            m_LabelValueField.BackColor = m_Highlight;
            m_LabelValueField.ForeColor = m_HighlightText;
            m_LabelUnitsField.BackColor = m_Highlight;
            m_LabelUnitsField.ForeColor = m_HighlightText;
        }

        /// <summary>
        /// Event handler for the user control <c>Leave</c> event. Sets the background and foreground colours back to the original values.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void WatchControl_Leave(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_LabelNameField.BackColor = m_BackColor;
            m_LabelNameField.ForeColor = m_ForeColor;

            // Set the colours of the value field according to the actual value.
            m_LabelValueField.BackColor = (m_Value == 0) ? m_BackColorValueFieldZero : m_BackColorValueFieldNonZero;
            m_LabelValueField.ForeColor = (m_Value == 0) ? m_ForeColorValueFieldZero : m_ForeColorValueFieldNonZero;

            m_LabelUnitsField.BackColor = m_BackColor;
            m_LabelUnitsField.ForeColor = m_ForeColor;
        }

        /// <summary>
        /// Event handler for the <c>ForeColorChanged</c> event. Ensure that the <c>ForeColor</c> properties of the variable name field and units field labels are 
        /// updated and that the default forecolor variable is updated.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void WatchControl_ForeColorChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_LabelNameField.ForeColor = this.ForeColor;
            m_LabelUnitsField.ForeColor = this.ForeColor;
            m_ForeColor = this.ForeColor;
        }

        /// <summary>
        /// Event handler for the <c>BackColorChanged</c> event. Ensure that the <c>BackColor</c> properties of the variable name field and units field labels are 
        /// updated and the default backcolor variable is updated.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void WatchControl_BackColorChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            m_LabelNameField.BackColor = this.BackColor;
            m_LabelUnitsField.BackColor = this.BackColor;
            m_BackColor = this.BackColor;
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// If the read-only property is not asserted, check the write-enabled status of the watch variable and set/clear the <c>WriteEnabled</c> property accordingly. 
        /// The current state is determined by the specified <paramref name="attributeFlags"/> and <paramref name="securityLevel"/> parameters. True, indicates that the 
        /// watch variable is currently write enabled; false, indicates that the watch variable is currently read-only.
        /// </summary>
        /// <param name="attributeFlags">The attribute flags associated with the watch variable.</param>
        /// <param name="securityLevel">The current security level.</param>
        private void CheckWriteEnabledStatus(AttributeFlags attributeFlags, SecurityLevel securityLevel)
        {
            // Local flag used to determine if the watch variable is currently write enabled.
            bool writeEnabled;

            // Check whether the ReadOnly property is asserted.
            if (ReadOnly == true)
            {
                // Yes - Force the user control to be read-only.
                writeEnabled = false;
            }
            else
            {
                // Check whether the read-only attribute is set.
                if ((attributeFlags & AttributeFlags.PTUD_READONLY) == AttributeFlags.PTUD_READONLY)
                {
                    // Read-only watch variable.
                    writeEnabled = false;
                }
                else
                {
                    // Check whether the current security level is below that required to modify the watch variable.
                    if (((attributeFlags & AttributeFlags.PTUD_PSSWD1) == AttributeFlags.PTUD_PSSWD1) && (securityLevel < SecurityLevel.Level1))
                    {
                        // Read-only - security level is too low.
                        writeEnabled = false;
                    }
                    else if (((attributeFlags & AttributeFlags.PTUD_PSSWD2) == AttributeFlags.PTUD_PSSWD2) && (securityLevel < SecurityLevel.Level2))
                    {
                        // Read-only - security level is too low.
                        writeEnabled = false;
                    }
                    else
                    {
                        // Write enabled - display value using bold text.
                        m_LabelValueField.Font = new Font(ClientForm.Font, FontStyle.Bold);
                        writeEnabled = true;
                    }
                }
            }

            SetWriteEnabledProperty(writeEnabled);
        }

        /// <summary>
        /// Set the state of the <c>WriteEnabled</c> property and perform any associated logic.
        /// </summary>
        /// <param name="writeEnabled">The required state of the WriteEnabled property.</param>
        protected virtual void SetWriteEnabledProperty(bool writeEnabled)
        {
            // Update the flag.
            m_WriteEnabled = writeEnabled;

            // Set the FontStyle according to the state.
            if (m_WriteEnabled == true)
            {
                m_LabelValueField.Font = new Font(ClientForm.Font, FontStyle.Bold);
            }
            else
            {
                m_LabelValueField.Font = new Font(ClientForm.Font, FontStyle.Regular);
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the flag which indicates whether the Dispose() method has been called. True, indicates that the Dispose() method has been called; otherwise, 
        /// false.
        /// </summary>
        protected new bool IsDisposed
        {
            get
            {
                lock (this)
                {
                    return m_IsDisposed;
                }
            }

            set
            {
                lock (this)
                {
                    m_IsDisposed = value;
                }
            }
        }

        #region - [Text Fields] -
        /// <summary>
        /// Gets or sets the text that appears in the variable name field.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The text that appears in the variable name field.")
        ]
        public string VariableNameFieldText
        {
            get { return m_LabelNameField.Text; }
            set { m_LabelNameField.Text = value; }
        }

        /// <summary>
        /// Gets or sets the text that appears in the value field.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The text that appears in the value field.")
        ]
        public string ValueFieldText
        {
            get { return m_LabelValueField.Text; }
            set { m_LabelValueField.Text = value; }
        }

        /// <summary>
        /// Gets or sets the text that appears in the units field.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The text that appears in the units field.")
        ]
        public string UnitsFieldText
        {
            get { return m_LabelUnitsField.Text; }
            set { m_LabelUnitsField.Text = value; }
        }
        #endregion - [Text Fields] -

        #region - [Value Field Colours] -
        /// <summary>
        /// Gets or sets the background colour of the the value field for a value of zero.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The background colour of the the value field for a value of zero.")
        ]
        public Color BackColorValueFieldZero
        {
            get { return m_BackColorValueFieldZero; }
            set 
            { 
                m_BackColorValueFieldZero = value;
                m_LabelValueField.BackColor = m_BackColorValueFieldZero;
            }
        }

        /// <summary>
        /// Gets or sets the foreground colour of the value field for a value of zero.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The foreground colour of the the value field for a value of zero.")
        ]
        public Color ForeColorValueFieldZero
        {
            get { return m_ForeColorValueFieldZero; }
            set 
            { 
                m_ForeColorValueFieldZero = value;
                m_LabelValueField.ForeColor = m_ForeColorValueFieldZero;
            }
        }

        /// <summary>
        /// Gets or sets the background colour of the the value field for any value other than zero.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The background colour of the the value field for any value other than zero.")
        ]
        public Color BackColorValueFieldNonZero
        {
            get { return m_BackColorValueFieldNonZero; }
            set { m_BackColorValueFieldNonZero = value; }
        }

        /// <summary>
        /// Gets or sets the foreground colour of the value field for any value other than zero.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The foreground colour of the the value field for any value other than zero.")
        ]
        public Color ForeColorValueFieldNonZero
        {
            get { return m_ForeColorValueFieldNonZero; }
            set { m_ForeColorValueFieldNonZero = value; }
        }
        #endregion - [Value Field Colours] -

        #region - [Width] -
        /// <summary>
        /// Gets or sets the width of the variable name field, in pixels.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The width of the variable name field, in pixels.")
        ]
        public int WidthVariableNameField
        {
            get { return m_LabelNameField.Width; }
            set 
            { 
                m_LabelNameField.Width = value;
                Width = m_LabelNameField.Width + m_LabelValueField.Width + m_LabelUnitsField.Width;
            }
        }

        /// <summary>
        /// Gets or sets the width of the value field, in pixels.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The width of the value field, in pixels.")
        ]
        public int WidthValueField
        {
            get { return m_LabelValueField.Width; }
            set 
            {
                m_LabelValueField.Width = value;
                Width = m_LabelNameField.Width + m_LabelValueField.Width + m_LabelUnitsField.Width;
            }
        }

        /// <summary>
        /// Gets or sets the width of the units field, in pixels.
        /// </summary>
        [
        Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Appearance"),
        Description("The width of the units field, in pixels.")
        ]
        public int WidthUnitsField
        {
            get { return m_LabelUnitsField.Width; }
            set 
            { 
                m_LabelUnitsField.Width = value;
                Width = m_LabelNameField.Width + m_LabelValueField.Width + m_LabelUnitsField.Width;
            }
        }
        #endregion - [Width] -

        /// <summary>
        /// Gets or sets the current value of the variable.
        /// </summary>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Data"),
        Description("The current value.")
        ]
        public virtual double Value
        {
            get { return m_Value; }
            set
            {
                // Check whether the security level has changed and, if so, update the write-enabled flag.
                SecurityLevel securityLevel = new Security().SecurityLevelCurrent;
                if (securityLevel != m_PreviousSecurityLevel)
                {
                    // The security level has changed.
                    CheckWriteEnabledStatus(m_AttributeFlags, securityLevel);
                    m_PreviousSecurityLevel = securityLevel;
                }

                // Check whether the user has changed the user font.
                if (m_ClientForm.Font != m_ClientFont)
                {
                    m_ClientFont = m_ClientForm.Font;

                    // Use this as a means to update the font of the label used to display the value of the watch variable.
                    SetWriteEnabledProperty(m_WriteEnabled);
                }
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the watch variable associated with the control.
        /// </summary>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Data"),
        Description("The variable identifier associated with the control.")
        ]
        public virtual int Identifier
        {
            get { return m_Identifier; }
            set { m_Identifier = value; }
        }

        /// <summary>
        /// Gets or sets the variable attribute flags associated with the control.
        /// </summary>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Data"),
        Description("The variable attribute flags associated with the control.")
        ]
        public AttributeFlags AttributeFlags
        {
            get { return m_AttributeFlags; }
            set
            {
                m_AttributeFlags = value;
                CheckWriteEnabledStatus(m_AttributeFlags, new Security().SecurityLevelCurrent);
            }
        }

        /// <summary>
        /// Gets the state of the write-enabled flag. True, if the watch variable value can be updated; otherwise, false, to indicate that the value is read-only.
        /// </summary>
        /// <remarks>The state of the write-enabled flag is determined by the attribute flags associated with the variable and the current security level.</remarks>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Data"),
        Description("The state of the write-enabled flag.")
        ]
        public bool WriteEnabled
        {
            get { return m_WriteEnabled; }
        }

        /// <summary>
        /// Gets or sets the client form associated with the control.
        /// </summary>
        [
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Category("Data"),
        Description("The client form associated with the control.")
        ]
        public virtual Form ClientForm
        {
            get { return m_ClientForm; }
            set 
            { 
                m_ClientForm = value;

                if (m_ClientForm != null)
                {
                    m_ClientFont = m_ClientForm.Font;
                }
            }
        }

        /// <summary>
        /// Gets or sets the static control flag used to force the data associated with the <c>Value</c> field to be marked as invalid. This will apply to all user controls derived  
        /// from this class.
        /// </summary>
        public static bool InvalidValue
        {
            get { return m_InvalidValue; }
            set { m_InvalidValue = value; }
        }

        /// <summary>
        /// Gets or sets the the static control flag used to force all user controls derived from this user control to be read-only. This will apply to all user controls derived  
        /// from this class.
        /// </summary>
        public static bool ReadOnly
        {
            get { return m_ReadOnly; }
            set { m_ReadOnly = value; }
        }
        #endregion --- Properties ---
    }
}

