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
 *  File name:  EventVariable.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/13/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  03/21/11    1.1     K.McD           1.  Added the CopyTo() method to allow the selected event variable to be copied to a newly instantiated event variable.
 */
#endregion --- Revision History ---

using System;
using System.Xml.Serialization;

namespace Common.Configuration
{
    /// <summary>
    /// A class to store the fields associated with an event variable.
    /// </summary>
    [Serializable]
    [XmlRoot()]
    public class EventVariable : Variable
    {
        #region --- Member Variables ---
        /// <summary>
        /// The conversion factor that is to be applied to the event variable.
        /// </summary>
        private double m_ConversionFactor;
        #endregion --- Member Variables ---

        #region --- Methods ---
        /// <summary>
        /// Copy all property values to the specified event variable.
        /// </summary>
        /// <param name="eventVariable">The event variable that the property values are to be copied to.</param>
        public void CopyTo(ref EventVariable eventVariable)
        {
            // EventVariable
            eventVariable.ConversionFactor = ConversionFactor;

            // Variable
            eventVariable.Name = Name;
            eventVariable.DataType = DataType;
            eventVariable.VariableType = VariableType;
            eventVariable.ScaleFactor = ScaleFactor;
            eventVariable.EnumBitIdentifier = EnumBitIdentifier;
            eventVariable.IsBitMask = IsBitMask;
            eventVariable.Units = Units;
            eventVariable.FormatString = FormatString;
            eventVariable.ValueFromTarget = ValueFromTarget;
            eventVariable.DataTypeFromTarget = DataTypeFromTarget;
            
            // Record
            eventVariable.Identifier = Identifier;
            eventVariable.HelpIndex = HelpIndex;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the conversion factor that is to be applied to the event variable.
        /// </summary>
        [XmlElement]
        public double ConversionFactor
        {
            get { return m_ConversionFactor; }
            set { m_ConversionFactor = value; }
        }
        #endregion --- Properties ---
    }
}
