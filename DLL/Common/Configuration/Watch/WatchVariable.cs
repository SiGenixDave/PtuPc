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
 *  File name:  WatchVariable.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/13/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  03/28/11    1.1     K.McD           1.  Added the [Serializable] attribute.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Configuration
{
    /// <summary>
    /// A class to store the fields associated with a watch variable.
    /// </summary>
    [Serializable]
    public class WatchVariable : Variable
    {
        #region --- Member Variables ---
        /// <summary>
        /// The variable name used by the embedded software.
        /// </summary>
        private string m_EmbeddedName;

        /// <summary>
        /// The old watch variable identifier.
        /// </summary>
        private short m_OldIdentifier;

        /// <summary>
        /// The attribute flags associated with the watch variable: PTUD_BBRAM; PTUD_DESC1; PTUD_DESC2; PTUD_DESC3; PTUD_PSSWD1; PTUD_PSSWD2; PTUD_READONLY.
        /// </summary>
        private int m_AttributeFlags;

        /// <summary>
        /// The maximum engineering value that is to be displayed on the chart recorder.
        /// </summary>
        private double m_MaxChartScale;

        /// <summary>
        /// The minimum engineering value that is to be displayed on the chart recorder.
        /// </summary>
        private double m_MinChartScale;

        /// <summary>
        /// The maximum value that the user can set the watch variable to.
        /// </summary>
        private double m_MaxModifyValue;

        /// <summary>
        /// The minimum value that the user can set the watch variable to.
        /// </summary>
        private double m_MinModifyValue;
        #endregion --- Member Variables ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the variable name used by the embedded software.
        /// </summary>
        public string EmbeddedName
        {
            get { return m_EmbeddedName; }
            set { m_EmbeddedName = value; }
        }

        /// <summary>
        /// Gets or sets the old watch variable identifier.
        /// </summary>
        public short OldIdentifier
        {
            get { return m_OldIdentifier; }
            set { m_OldIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the attribute flags associated with the watch variable: PTUD_BBRAM; PTUD_DESC1; PTUD_DESC2; PTUD_DESC3; PTUD_PSSWD1; PTUD_PSSWD2; PTUD_READONLY.
        /// </summary>
        public int AttributeFlags
        {
            get { return m_AttributeFlags; }
            set { m_AttributeFlags = value; }
        }

        /// <summary>
        /// Gets or sets the maximum engineering value that is to be displayed on the chart recorder.
        /// </summary>
        public double MaxChartScale
        {
            get { return m_MaxChartScale; }
            set { m_MaxChartScale = value; }
        }

        /// <summary>
        /// Gets or sets the minimum engineering value that is to be displayed on the chart recorder.
        /// </summary>
        public double MinChartScale
        {
            get { return m_MinChartScale; }
            set { m_MinChartScale = value; }
        }

        /// <summary>
        /// Gets or sets the maximum value that the user can set the watch variable to.
        /// </summary>
        public double MaxModifyValue
        {
            get { return m_MaxModifyValue; }
            set { m_MaxModifyValue = value; }
        }

        /// <summary>
        /// Gets or sets the minimum value that the user can set the watch variable to.
        /// </summary>
        public double MinModifyValue
        {
            get { return m_MinModifyValue; }
            set { m_MinModifyValue = value; }
        }
        #endregion --- Properties ---
    }
}
