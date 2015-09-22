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
 *  File name:  WatchControlLayout.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  09/29/10    1.1     K.McD           1.  Configured the AttributeFlags property of the WatchControl derived user controls within the method ConfigureWatchControls().
 * 
 *  03/28/11    1.2     K.McD           1.  Modified the name of a number of local variables and parameters.
 *                                      2.  Modified the ConfigureWatchControls class to use the old identifier fiels of the watch variable.
 *                                      
 *  05/23/11    1.3     K.McD           1.  Corrected an XML tag.
 *                                      2.  Applied the 'Organize Usings/Remove and Sort' context menu.
 *                                      
 *  08/02/13    1.4     K.McD           1.  Increased the width, in pixels, of the name field associated with the watch control from 200 to 230 to prevent some of the longer 
 *                                          variable names being displayed incorrectly. 
 *                                          
 *                                          In the WatchControl user control, the label associated with the variable name has the TextAlign property set to MiddleLeft and the 
 *                                          AutoEllipsis property set to True. If the variable name text, when printed using the selected font, exceeds the width of the label 
 *                                          the text is automatically aligned to TopLeft, truncated and an ellipsis i.e. '...' is appended to the text to show that it has been 
 *                                          truncated. Although the text is still readable, it appears vertically misaligned.
 *                                          
 *  08/05/13    1.5     K.McD           1.  Changed the WidthWatchControlVariableNameField constant to 210 to optimize the size of the the Watch Window user controls for a  
 *                                          display resolution of 1280 x 960.
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Common.Communication;
using Common.Configuration;
using Common.UserControls;

namespace Common
{
    /// <summary>
    /// A class to support configuration, drawing and layout of multiple <c>WatchControl</c> derived user controls.
    /// </summary>
    public class WatchControlLayout
    {
        #region --- Constants ---
        #region - [Heights] -
        /// <summary>
        /// The height, in pixels, of the watch variable user control. Value: 23.
        /// </summary>
        public const int HeightWatchControl = 23;
        #endregion - [Heights] -

        #region - [Margins] -
        /// <summary>
        /// The left margin to be applied to <see>WatchControl</see> derived user controls. Value: 5.
        /// </summary>
        public const int MarginLeftWatchControl = 5;

        /// <summary>
        /// The right margin to be applied to <see>WatchControl</see> derived user controls. Value: 5.
        /// </summary>
        public const int MarginRightWatchControl = 5;

        /// <summary>
        /// The top margin to be applied to <see>WatchControl</see> derived user controls. Value: 1.
        /// </summary>
        public const int MarginTopWatchControl = 1;

        /// <summary>
        /// The bottom margin to be applied to <see>WatchControl</see> derived user controls. Value: 1.
        /// </summary>
        public const int MarginBottomWatchControl = 1;
        #endregion - [Margins] -

        #region - [Widths] -
        /// <summary>
        /// The width, in pixels, of the variable name field of the watch variable user control. Value: 210.
        /// </summary>
        public const int WidthWatchControlVariableNameField = 210;

        /// <summary>
        /// The width, in pixels, of the value field of the watch variable user control. Value: 130.
        /// </summary>
        public const int WidthWatchControlValueField = 130;

        /// <summary>
        /// The width, in pixels, of the units field of the watch variable user control. Value: 60.
        /// </summary>
        public const int WidthWatchControlUnitsField = 60;
        #endregion - [Widths] -
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Flag to indicate whether the class has been disposed of. True, indicates that the class has already been disposed of; otherwise, false.
        /// </summary>
        protected bool m_IsDisposed;

        /// <summary>
        /// Reference to the <c>HistoricDataManager</c> class, this supports the displaying of historic data and allows the time range to be zoomed in and out.
        /// </summary>
        protected IHistoricDataManager m_HistoricDataManager;

        /// <summary>
        /// Reference to the <c>Form</c> which instantiated this class.
        /// </summary>
        private Form m_Form;

        /// <summary>
        /// The tab index to be assigned to the user control.
        /// </summary>
        private static int m_TabIndex = 0;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="form">Reference to the form which instantiated the class.</param>
        public WatchControlLayout(Form form)
        {
            m_Form = form;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="form">Reference to the form which instantiated the class.</param>
        /// <param name="historicDataManager">Reference to the <c>HistoricDataManager</c> object associated with the calling form.</param>
        public WatchControlLayout(Form form, IHistoricDataManager historicDataManager)
            : this(form)
        {
            m_HistoricDataManager = historicDataManager;
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Writes the specified header text to the specified panel.
        /// </summary>
        /// <remarks>If <paramref name="headerText"/> is <c>null</c> or an empty string no headers are added.</remarks>
        /// <param name="headerText">The header text.</param>
        /// <param name="panel">Reference to the panel to which the header text is to be added.</param>
        /// <param name="watchControlSize">The structure defining the size related parameters of the label, <see cref="VariableControlSize_t"/>.</param>
        public void WriteColumnHeaders(string headerText, Panel panel, VariableControlSize_t watchControlSize)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            if ((headerText == string.Empty) || (headerText == null) || (panel == null))
            {
                return;
            }

            Label lblColumnHeader = new Label();
            lblColumnHeader.Text = headerText;
            lblColumnHeader.Size = watchControlSize.Size;
            lblColumnHeader.BackColor = Color.LightSteelBlue;
            lblColumnHeader.TextAlign = ContentAlignment.MiddleLeft;
            lblColumnHeader.Location = new System.Drawing.Point(watchControlSize.Margin.Left, 0);

            // Add the Label to the display panel.
            panel.Controls.Add(lblColumnHeader);
        }

        /// <summary>
        /// Instantiates a new <c>WatchControl</c> derived user control and configures the properties for each element of <paramref name="watchControls"/> based upon the 
        /// watch variables specified by <paramref name="oldIdentifierList"/>. Each user control is then added to the <c>Controls</c> property of <paramref name="panel"/>.
        /// </summary>
        /// <remarks>The length of the array should be equal to the count value associated with the list.</remarks>
        /// <param name="watchControls">The array of watch variable user controls that are to be configured.</param>
        /// <param name="panel">Reference to the <c>Panel</c> to which the user controls are to be added.</param>
        /// <param name="watchControlSize">The structure defining the size related parameters of the user control, <see cref="VariableControlSize_t"/>.</param>
        /// <param name="oldIdentifierList">The list of watch variable old identifiers.</param>
        /// <exception cref="ArgumentException">Thrown if the number of watch identifier entries in the list is incompatible with the length of the user control array.</exception>
        public void ConfigureWatchControls(WatchControl[] watchControls, Panel panel, VariableControlSize_t watchControlSize, List<short> oldIdentifierList)
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            Debug.Assert(oldIdentifierList.Count == watchControls.Length);

            // Work out the spacing between consecutive lines.
            int rowSpacing = watchControlSize.Size.Height + watchControlSize.Margin.Vertical;

            // Initialize the user controls.
            short oldIdentifier;
            WatchVariable watchVariable;
            for (int rowIndex = 0; rowIndex < oldIdentifierList.Count; rowIndex++)
            {
                oldIdentifier = oldIdentifierList[rowIndex];
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                    if (watchVariable == null)
                    {
                        // The specified watch variable is not defined in the current data dictionary therefore display an empty user control showing the 'not-defined' text.
                        watchControls[rowIndex] = new WatchControl();
                    }
                    else
                    {
                        switch (watchVariable.VariableType)
                        {
                            case VariableType.Scalar:
                                watchControls[rowIndex] = new WatchScalarControl();
                                break;
                            case VariableType.Enumerator:
                                watchControls[rowIndex] = new WatchEnumeratorControl();
                                break;
                            case VariableType.Bitmask:
                                watchControls[rowIndex] = new WatchBitmaskControl();
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception)
                {
                    watchVariable = null;

                    // The specified watch variable is not defined in the current data dictionary therefore display an empty user control showing the 'not-defined' text.
                    watchControls[rowIndex] = new WatchControl();
                }

                watchControls[rowIndex].WidthVariableNameField = watchControlSize.WidthVariableNameField;
                watchControls[rowIndex].WidthValueField = watchControlSize.WidthValueField;
                watchControls[rowIndex].WidthUnitsField = watchControlSize.WidthUnitsField;

                watchControls[rowIndex].ClientForm = m_Form;
                watchControls[rowIndex].TabIndex = m_TabIndex;
                watchControls[rowIndex].Location = new System.Drawing.Point(watchControlSize.Margin.Left, (rowIndex + 1) * rowSpacing);

                watchControls[rowIndex].ForeColorValueFieldZero = Color.ForestGreen;
                watchControls[rowIndex].ForeColorValueFieldNonZero = Color.ForestGreen;

                watchControls[rowIndex].Identifier = oldIdentifier;

                if (watchVariable == null)
                {
                    watchControls[rowIndex].AttributeFlags = AttributeFlags.PTUD_NOTUSED;
                }
                else
                {
                    watchControls[rowIndex].AttributeFlags = (AttributeFlags)watchVariable.AttributeFlags;
                }

                watchControls[rowIndex].Value = 0;

                // Add the user control to the specified panel.
                panel.Controls.Add(watchControls[rowIndex]);
            }
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or set the tab index counter.
        /// </summary>
        public static int TabIndex
        {
            get { return m_TabIndex; }
            set { m_TabIndex = value; }
        }
        #endregion --- Properties ---
    }
}
