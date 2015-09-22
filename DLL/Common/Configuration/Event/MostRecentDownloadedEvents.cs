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
 *  File name:  MostRecentDownloadedEvents.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/13/15    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.Configuration
{
    /// <summary>
    /// A class to store an array of the most recent downloaded event associated with each car. Each array element corresponds to the most recent downloaded event
    /// associated with the car number which equates to the index value e.g. DownloadedEvents[8012] corresponds to the most recent downloaded event associated with
    /// car 8012.
    /// </summary>
    [Serializable]
    [XmlRoot()]
    public class MostRecentDownloadedEvents
    {
        #region - [Constants] -
        /// <summary>
        /// The minimum valid value of the car number.
        /// </summary>
        private const short CarNumberMinValue = 0;

        /// <summary>
        /// The maximum valid value of the car number.
        /// </summary>
        private const short CarNumberMaxValue = 9999;
        #endregion - [Constants] -

        #region --- Member Variables ---
        /// <summary>
        /// An array of the most recent downloaded event associated with each car.
        /// </summary>
        private EventRecord[] m_MostRecentDownloadedEvents = new EventRecord[CarNumberMaxValue + 1];
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class. This constructor instantiates each element of the 'DownloadedEvents' array with a new EventRecord.
        /// </summary>
        public MostRecentDownloadedEvents()
        {
            for (short car = CarNumberMinValue; car <= CarNumberMaxValue; car++)
            {
                m_MostRecentDownloadedEvents[car] = new EventRecord();
            }
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the array of the most recent downloaded event associated with each car.
        /// </summary>
        public EventRecord[] DownloadedEvents
        {
            get { return m_MostRecentDownloadedEvents; }
            set { m_MostRecentDownloadedEvents = value; }
        }
        #endregion --- Properties ---
    }
}
