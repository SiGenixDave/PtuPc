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
 *  File name:  WatchControlSize.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/29/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *
 */
#endregion --- Revision History ---

using System;
using System.Drawing;

namespace Common.UserControls
{
    #region - [Structures] -
    /// <summary>
    /// The structure used to define the size of any user control derived from the <c>VariableControl</c> user control.
    /// </summary>
    [Serializable]
    public struct VariableControlSize_t
    {
        /// <summary>
        /// The margins around the control.
        /// </summary>
        public Margin_t Margin;

        /// <summary>
        /// The width of the variable name field of the user control, in pixels.
        /// </summary>
        public int WidthVariableNameField;

        /// <summary>
        /// The width of the value field of the user control, in pixels.
        /// </summary>
        public int WidthValueField;

        /// <summary>
        /// The width of the units field of the user control, in pixels.
        /// </summary>
        public int WidthUnitsField;

        /// <summary>
        /// The height of the user control, in pixels.
        /// </summary>
        public int Height;

        /// <summary>
        /// Gets the <c>Size</c> of the user control.
        /// </summary>
        public Size Size
        {
            get { return new Size(WidthVariableNameField + WidthValueField + WidthUnitsField, Height); }
        }
    }

    /// <summary>
    /// The structure used to define the size of any user control.
    /// </summary>
    [Serializable]
    public struct UserControlSize_t
    {
        /// <summary>
        /// The margins to be applied to the control.
        /// </summary>
        public Margin_t Margin;

        /// <summary>
        /// The <c>Size</c> of the user control, in pixels.
        /// </summary>
        public Size Size;
    }

    /// <summary>
    /// The margins around the usercontrol.
    /// </summary>
    [Serializable]
    public struct Margin_t
    {
        /// <summary>
        /// Left hand side.
        /// </summary>
        private int left;

        /// <summary>
        /// Top.
        /// </summary>
        private int top;

        /// <summary>
        /// Right hand side.
        /// </summary>
        private int right;

        /// <summary>
        /// Bottom.
        /// </summary>
        private int bottom;

        /// <summary>
        /// The combined top and bottom margin.
        /// </summary>
        private int vertical;

        /// <summary>
        /// The combined left and right margin.
        /// </summary>
        private int horizontal;

        /// <summary>
        /// Gets or sets the left margin.
        /// </summary>
        public int Left
        {
            get { return (left); }
            set
            {
                left = value;
                horizontal = Left + right;
            }
        }

        /// <summary>
        /// Gets or sets the right margin.
        /// </summary>
        public int Right
        {
            get { return (right); }
            set
            {
                right = value;
                horizontal = Left + right;
            }
        }

        /// <summary>
        /// Gets the combined left and right margin.
        /// </summary>
        public int Horizontal
        {
            get { return horizontal; }
        }

        /// <summary>
        /// Gets or sets the top margin.
        /// </summary>
        public int Top
        {
            get { return (top); }
            set
            {
                top = value;
                vertical = top + bottom;
            }
        }

        /// <summary>
        /// Gets or sets the bottom margin.
        /// </summary>
        public int Bottom
        {
            get { return (bottom); }
            set
            {
                bottom = value;
                vertical = top + bottom;
            }
        }

        /// <summary>
        /// Gets the combined top and bottom margin.
        /// </summary>
        public int Vertical
        {
            get { return (vertical); }
        }
    }
    #endregion - [Structures] -
}
