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
 *  File name:  AutoScale.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/17/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  03/28/11    1.1     K.McD           1.  Modified to use the old identifier field of the watch variables defined in the workset.
 *                                      2.  Added the WatchVariable and OldIdentifier properties.
 *                                      3.  Include checks that a valid watch variable has been defined when accessing the structure properties.
 * 
 *
 */
#endregion --- Revision History ---

using System;

using Common;
using Common.Configuration;

namespace Common.Communication
{
    /// <summary>
    /// Structure containing the fields required to auto-scale the plot of an individual watch element.
    /// </summary>
    [Serializable]
    public struct AutoScale_t
    {
        #region --- Constants ---
        /// <summary>
        /// The percentage of the range between the upper and lower engineering values to allow for the upper and lower display limits.
        /// </summary>
        private const float MarginAsPercentOfRange = (float)0.05;
        #endregion --- Constants ---

        #region - [Member Variables] -
        /// <summary>
        /// The watch variable associated with this auto-scale information, this is derived using the old identifier value.
        /// </summary>
        private WatchVariable m_WatchVariable;

        /// <summary>
        /// The watch identifier associated with this auto-scale information.
        /// </summary>
        private short m_WatchIdentifier;

        /// <summary>
        /// The old identifier associated with this auto-scale information.
        /// </summary>
        private short m_OldIdentifier;

        /// <summary>
        /// The watch element index associated with this auto-scale information.
        /// </summary>
        private short m_WatchElementIndex;

        /// <summary>
        /// The maximum raw value of the watch element over the duration of the log.
        /// </summary>
        private double m_MaximumRaw;

        /// <summary>
        /// The minimum raw value of the watch element over the duration of the log.
        /// </summary>
        private double m_MinimumRaw;

        /// <summary>
        /// The scale factor associated with the watch identifier.
        /// </summary>
        private double m_ScaleFactor;

        /// <summary>
        /// The upper margin, in engineering units, between the maximum value of the plot and the upper display limit.
        /// </summary>
        private float m_MarginUpper;

        /// <summary>
        /// The lower margin, in engineering units, between the minimum value of the plot and the lower display limit.
        /// </summary>
        private float m_MarginLower;
        #endregion - [Member Variables] -

        #region - [Methods] -
        /// <summary>
        /// Update the margins between the maximum plot value and the upper display limit and the minimum plot values and the lower display limit.
        /// </summary>
        private void UpdateMargins()
        {
            if (m_WatchVariable == null)
            {
                m_MarginUpper = float.NaN;
                m_MarginLower = float.NaN;
                return;
            }

            switch (m_WatchVariable.VariableType)
            {
                case VariableType.Scalar:
                    if (RangeRaw == 0)
                    {
                        if (m_MaximumRaw == 0)
                        {
                            // The scalar value is zero over the duration of the plot, allow +/- 1.0 above and below the plot.
                            m_MarginUpper = (float)1.0;
                        }
                        else
                        {
                            // The scalar is static over the duration of the plot, allow a fixed, pre-defined % of the actual value above and below the plot.
                            m_MarginUpper = ((float)m_MaximumRaw * (float)m_ScaleFactor) * MarginAsPercentOfRange;

                            // Ensure that the upper and lower margins are always positive.
                            if (m_MarginUpper < 0)
                            {
                                m_MarginUpper *= (float)(-1);
                            }
                        }
                    }
                    else
                    {
                        // Allow a fixed, pre-defined %  of the range of the values above and below the plot.
                        m_MarginUpper = RangeEng * MarginAsPercentOfRange;
                    }
                    m_MarginLower = m_MarginUpper;
                    break;
                case VariableType.Enumerator:
                    // Allow a margin of 1 unit at the top of the chart for enumerator watch variables, the lower limit should always be zero.
                    m_MarginUpper = (float)1.0;
                    m_MarginLower = 0;
                    break;
                case VariableType.Bitmask:
                    // Allow a margin of 1 unit at the top of the chart for bitmask watch variables, the lower limit should always be zero.
                    m_MarginUpper = (float)1.0;
                    m_MarginLower = 0;
                    break;
                default:
                    throw new ArgumentException("AutoScale_t.GetMargin()", "watchVariable.VariableType");

            }
        }
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets or sets the maximum raw value of the watch element.
        /// </summary>
        public double MaximumRaw
        {
            get { return m_MaximumRaw; }
            set { m_MaximumRaw = value; }
        }

        /// <summary>
        /// Gets or sets the minimum raw value of the watch element.
        /// </summary>
        public double MinimumRaw
        {
            get { return m_MinimumRaw; }
            set { m_MinimumRaw = value; }
        }

        /// <summary>
        /// Gets the upper display limit of the plot in engineering units.
        /// </summary>
        public float UpperDisplayLimitEng
        {
            get
            {
                if (m_WatchVariable == null)
                {
                    return float.NaN;
                }
                else
                {
                    return ((float)m_MaximumRaw * (float)m_ScaleFactor) + m_MarginUpper;
                }
            }
        }

        /// <summary>
        /// Gets the lower display limit of the plot in engineering units.
        /// </summary>
        public float LowerDisplayLimitEng
        {
            get
            {
                if (m_WatchVariable == null)
                {
                    return float.NaN;
                }
                else
                {
                    switch (m_WatchVariable.VariableType)
                    {
                        case VariableType.Scalar:
                            return ((float)m_MinimumRaw * (float)m_ScaleFactor) - m_MarginLower;
                        case VariableType.Enumerator:
                        case VariableType.Bitmask:
                            return 0.0f;
                        default:
                            throw new ArgumentException("AutoScale_t.LowerDisplayLimit()", "LookupTable.Watch.Items[m_WatchIdentifier].VariableType");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the difference between the maximum and minimum raw values. 
        /// </summary>
        public double RangeRaw
        {
            get
            {
                return m_MaximumRaw - m_MinimumRaw;
            }
        }

        /// <summary>
        /// Gets the difference, in engineering units, between the upper and lower display limits.
        /// </summary>
        public float RangeEng
        {
            get
            {
                if (m_WatchVariable == null)
                {
                    return float.NaN;
                }
                else
                {
                    return ((float)m_MaximumRaw - (float)m_MinimumRaw) * (float)m_ScaleFactor;
                }
            }
        }

        /// <summary>
        /// Gets or sets the scaling factor of the watch element.
        /// </summary>
        public double ScaleFactor
        {
            get { return m_ScaleFactor; }
            set
            {
                if (m_WatchVariable == null)
                {
                    m_ScaleFactor = double.NaN;
                }
                else
                {
                    m_ScaleFactor = value;
                    UpdateMargins();
                }
            }
        }

        /// <summary>
        /// Gets or sets the watch identifier associated with this auto-scale information.
        /// </summary>
        public short WatchIdentifier
        {
            get { return m_WatchIdentifier; }
            set
            {
                m_WatchIdentifier = value;
            }
        }

        /// <summary>
        /// Gets or sets the old identifier associated with this auto-scale information.
        /// </summary>
        public short OldIdentifier
        {
            get { return m_OldIdentifier; }
            set
            {
                m_OldIdentifier = value;

                try
                {
                    m_WatchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[m_OldIdentifier];
                    if (m_WatchVariable == null)
                    {
                        m_MarginUpper = float.NaN;
                        m_MarginLower = float.NaN;
                        m_ScaleFactor = (double)1.0;
                        return;
                    }
                }
                catch (Exception)
                {
                    m_MarginUpper = float.NaN;
                    m_MarginLower = float.NaN;
                    m_ScaleFactor = (double)1.0;
                    return;
                }

                UpdateMargins();
            }
        }

        /// <summary>
        /// Gets or sets the watch element index associated with this auto-scale information.
        /// </summary>
        public short WatchElementIndex
        {
            get { return m_WatchElementIndex; }
            set { m_WatchElementIndex = value; }
        }

        /// <summary>
        /// Gets the watch variable associated with this auto-sale information.
        /// </summary>
        public WatchVariable WatchVariable
        {
            get { return m_WatchVariable; }
        }
        #endregion - [Properties] -
    }
}
