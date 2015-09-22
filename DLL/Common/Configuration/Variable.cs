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
 *  File name:  Variable.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/13/10    1.0     K.McDonald      First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;
using System.Xml.Serialization;

namespace Common.Configuration
{
    /// <summary>
    /// A class that is used to store the fields that are common to all variable types associated with the vehicle control unit (VCU).
    /// </summary>
    [Serializable]
    [XmlRoot()]
    public class Variable : Record
    {
        #region --- Member Variables ---
        /// <summary>
        /// The name of the variable name.
        /// </summary>
        private string m_Name;

        /// <summary>
        /// The data type of the variable.
        /// </summary>
        private DataType_e m_DataType;

        /// <summary>
        /// The type of variable: scalar; enumerator or bitmask.
        /// </summary>
        private VariableType m_VariableType;

        /// <summary>
        /// The scaling factor that is to be used to convert the raw value to engineering units.
        /// </summary>
        private double m_ScaleFactor;

        /// <summary>
        /// The enumerator/bit mask identifier that is to be used when displaying the description/flags corresponding to the value field of the variable.
        /// </summary>
        private int m_EnumBitIdentifier;

        /// <summary>
        /// A flag that indicates whether the variable is a bit mask. True, if the variable is a bit mask; otherwise, false.
        /// </summary>
        private bool m_IsBitMask;

        /// <summary>
        /// The engineering units associated with the variable.
        /// </summary>
        /// <remarks>This is only relevant to scalar variables.</remarks>
        private string m_Units;

        /// <summary>
        /// The format string to be used when displaying the value field of the variable.
        /// </summary>
        private string m_FormatString;

        /// <summary>
        /// The value of the variable retrieved from the target.
        /// </summary>
        private double m_ValueFromTarget;

        /// <summary>
        /// The data type of the variable retrieved from the target.
        /// </summary>
        private short m_DataTypeFromTarget;
        #endregion --- Member Variables ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        [XmlElement()]
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        /// <summary>
        /// Gets or sets the data type of the variable.
        /// </summary>
        [XmlElement()]
        public DataType_e DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }

        /// <summary>
        /// Gets or sets the type of variable: scalar, bitmask or enumerator.
        /// </summary>
        [XmlElement()]
        public VariableType VariableType
        {
            get { return m_VariableType; }
            set { m_VariableType = value; }
        }

        /// <summary>
        /// Gets or sets the scaling factor that is to be used to convert the raw value to engineering units.
        /// </summary>
        [XmlElement()]
        public double ScaleFactor
        {
            get { return m_ScaleFactor; }
            set { m_ScaleFactor = value; }
        }

        /// <summary>
        /// Gets or sets the enumerator/bit mask identifier that is to be used when displaying the description/flags corresponding to the variable value.
        /// </summary>
        [XmlElement()]
        public int EnumBitIdentifier
        {
            get { return m_EnumBitIdentifier; }
            set { m_EnumBitIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the flag that indicates whether the variable is a bit mask. True, if the variable is a bit; otherwise, false.
        /// </summary>
        [XmlElement()]
        public bool IsBitMask
        {
            get { return m_IsBitMask; }
            set { m_IsBitMask = value; }
        }

        /// <summary>
        /// Gets or sets the engineering units associated with the variable.
        /// </summary>
        [XmlElement()]
        public string Units
        {
            get { return m_Units; }
            set { m_Units = value; }
        }

        /// <summary>
        /// Gets or sets the format string to be used when displaying the value field of the variable.
        /// </summary>
        [XmlElement()]
        public string FormatString
        {
            get { return m_FormatString; }
            set { m_FormatString = value; }
        }

        /// <summary>
        /// Gets or sets the value of the variable retrieved from the target.
        /// </summary>
        [XmlElement()]
        public double ValueFromTarget
        {
            get { return m_ValueFromTarget; }
            set { m_ValueFromTarget = value; }
        }

        /// <summary>
        /// Gets or sets the data type of the variable retrieved from the target.
        /// </summary>
        [XmlElement()]
        public short DataTypeFromTarget
        {
            get { return m_DataTypeFromTarget; }
            set { m_DataTypeFromTarget = value; }
        }
        #endregion --- Properties ---
    }
}
