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
 *  File name:  SelfTestVariable.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  10/13/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;
using System.Xml.Serialization;

namespace Common.Configuration
{
    /// <summary>
    /// A class to store the fields associated with a self test variable.
    /// </summary>
    [Serializable]
    [XmlRoot()]
    public class SelfTestVariable : Variable
    {
        #region --- Member Variables ---
        /// <summary>
        /// The variable name used by the embedded software.
        /// </summary>
        private string m_EmbeddedName;
        #endregion --- Member Variables ---

        #region --- Methods ---
        /// <summary>
        /// Copy all property values to the specified self test variable.
        /// </summary>
        /// <param name="selfTestVariable">The self test variable that the property values are to be copied to.</param>
        public void CopyTo(ref SelfTestVariable selfTestVariable)
        {
            // Self Test Variable
            selfTestVariable.EmbeddedName = EmbeddedName;

            // Variable
            selfTestVariable.Name = Name;
            selfTestVariable.DataType = DataType;
            selfTestVariable.VariableType = VariableType;
            selfTestVariable.ScaleFactor = ScaleFactor;
            selfTestVariable.EnumBitIdentifier = EnumBitIdentifier;
            selfTestVariable.IsBitMask = IsBitMask;
            selfTestVariable.Units = Units;
            selfTestVariable.FormatString = FormatString;
            selfTestVariable.ValueFromTarget = ValueFromTarget;
            selfTestVariable.DataTypeFromTarget = DataTypeFromTarget;

            // Record
            selfTestVariable.Identifier = Identifier;
            selfTestVariable.HelpIndex = HelpIndex;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the variable name used by the embedded software.
        /// </summary>
        public string EmbeddedName
        {
            get { return m_EmbeddedName; }
            set { m_EmbeddedName = value; }
        }
        #endregion --- Properties ---
    }
}
