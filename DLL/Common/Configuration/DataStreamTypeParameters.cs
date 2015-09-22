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
 *  File name:  DataStreamTypeParameters.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  01/24/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/28/11    1.1     K.McD           1.  Removed the Description, ParameterCount and SampleCount member variables as these are no longer used. The equivalent 
 *                                          values are now obtained from the GetStreamInformation() method of the PTUDLL32 dynamic link library.
 *                                          
 *  07/24/13    1.2     K.McD           1.  Added the WatchVariablesMax and Name fields.
 *
 */
#endregion --- Revision History ---

using System;

namespace Common.Configuration
{
    /// <summary>
    /// Structure containing the fields that define the different types of data streams that can be logged.
    /// </summary>
    [Serializable]
    public struct DataStreamTypeParameters_t
    {
        #region --- Member Variables ---
        /// <summary>
        /// The primary key identifier associated with this type of data stream.
        /// </summary>
        public short Identifier;

        /// <summary>
        /// The sample index corresponding to the time of the trip.
        /// </summary>
        public int TripIndex;

        /// <summary>
        /// The maximum number of watch variables associated with the data stream.
        /// </summary>
        public short WatchVariablesMax;

        /// <summary>
        /// The name of the datastream type.
        /// </summary>
        public string Name;
        #endregion --- Member Variables ---

        #region --- Methods ---
        /// <summary>
        /// Set the parameters to those associated with the default data stream type.
        /// </summary>
        /// <remarks>The parameters associated with the default data stream type are defined in the Parameter class.</remarks>
        public void SetToDefaulDataStreamType()
        {
            Identifier = Parameter.DefaultDataStreamTypeIdentifier;
            TripIndex = Parameter.DefaultDataStreamTypeTripIndex;
            WatchVariablesMax = Parameter.DefaultDataStreamTypeWatchVariablesMax;
            Name = string.Empty;
        }
        #endregion --- Methods ---

    }
}
