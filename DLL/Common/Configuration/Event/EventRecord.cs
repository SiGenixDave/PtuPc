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
 *  File name:  EventRecord.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/20/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/04/10    1.1     K.McD           1.  Added the EventIndex property.
 *                                      2.  Minor changes to a number of XML tags.
 *                                      3.  Now initializes the event variable list in the constructor.
 * 
 *  01/18/11    1.2     K.McD           1.  Bug fix SNCR001.79 - In previous releases, the contents of the data streams associated with some of the events 
 *                                          occasionally had all watch variable values set to 0. This bug has been addressed by correcting the stream number value 
 *                                          passed to the call to the PTUDLL32.GetStream() method in Event.CommunicationEvent.GetStream(). 
 * 
 *                                          The data stream number associated with each event record is determined as follows. Starting from the oldest event record, 
 *                                          whenever an event record is found that has an associated data stream, the stream number for the event record is incremented 
 *                                          from a starting value of 0. Any event record that does not have an associated data stream will have the stream number set 
 *                                          to CommonConstants.NotUsed. 
 * 
 *                                          As part of this bug fix the StreamNumber property was added to this class.
 * 
 *  03/21/11    1.3     K.McD           1.  Auto-modified as a result of a method name change associated with the EventTable class.
 *  
 *  06/22/11    1.4     K.McD           1.  Corrected a number of XML tags.
 *                                      2.  Modified the constructor such that the properties are initilaized directly.
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.Configuration
{
    /// <summary>
    /// A class to store the fields associated with a vehicle control unit system event.
    /// </summary>
    [Serializable]
    [XmlRoot()]
    public class EventRecord : Record
    {
        #region --- Member Variables ---
        #region - [Data Dictionary] -
        /// <summary>
        /// The event identifier associated with the event.
        /// </summary>
        private short m_EventIdentifier;

        /// <summary>
        /// The description of the event.
        /// </summary>
        private string m_Description;

        /// <summary>
        /// The task identifier associated with the event.
        /// </summary>
        private int m_TaskIdentifier;

        /// <summary>
        /// The log identifier associated with the event.
        /// </summary>
        private int m_LogIdentifier;

        /// <summary>
        /// The structure identifier associated with the event.
        /// </summary>
        private int m_StructureIdentifier;

        /// <summary>
        /// A list of the event variables that are associated with the event.
        /// </summary>
        /// <remarks>
        /// These consist of the event variables that are collected for each event plus the event specific variables.
        /// </remarks>
        private List<EventVariable> m_EventVariableList;

        /// <summary>
        /// The car identifier.
        /// </summary>
        private String m_CarIdentifier;
        #endregion - [Data Dictionary] -

        #region - [VCU] -
        /// <summary>
        /// The event index associated with the record.
        /// </summary>
        private short m_EventIndex;

        /// <summary>
        /// The time of the event.
        /// </summary>
        private string m_Time;

        /// <summary>
        /// The date of the event.
        /// </summary>
        private string m_Date;

        /// <summary>
        /// The date and time of the event as a .NET <c>DateTime</c> object.
        /// </summary>
        private DateTime m_DateTime;

        /// <summary>
        /// A flag that indicates whether a stream has been saved for the event. True, indicates that a stream has been saved for the event; false, if no stream is 
        /// available.
        /// </summary>
        private bool m_StreamSaved;

        /// <summary>
        /// The stream number value that is to be used to access the datastream associated with this record.
        /// </summary>
        /// <remarks>The stream number will be set to CommonConstants.NotUsed if there is no datastream associated with the record.</remarks>
        private short m_StreamNumber;
        #endregion - [VCU] -
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class. Zero parameter constructor.
        /// </summary>
        public EventRecord()
        {
        }

        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        /// <param name="identifier">The identifier assoiated with the record.</param>
        public EventRecord(short identifier)
        {
            Identifier = identifier;
            EventIdentifier = Lookup.EventTable.Items[identifier].EventIdentifier;
            Description = Lookup.EventTable.Items[identifier].Description;
            TaskIdentifier = Lookup.EventTable.Items[identifier].TaskIdentifier;
            HelpIndex = Lookup.EventTable.Items[identifier].HelpIndex;
            LogIdentifier = Lookup.EventTable.Items[identifier].LogIdentifier;
            StructureIdentifier = Lookup.EventTable.Items[identifier].StructureIdentifier;
            EventVariableList = Lookup.EventTable.CreateEventVariableList(Identifier);
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        #region - [Data Dictionary] -
        /// <summary>
        /// Gets or sets the event identifier associated with the event.
        /// </summary>
        [XmlElement()]
        public short EventIdentifier
        {
            get { return m_EventIdentifier; }
            set { m_EventIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the description of the event.
        /// </summary>
        [XmlElement()]
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        /// <summary>
        /// Gets or sets the task identifier associated with the event.
        /// </summary>
        [XmlElement()]
        public int TaskIdentifier
        {
            get { return m_TaskIdentifier; }
            set { m_TaskIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the log identifier associated with the event.
        /// </summary>
        [XmlElement()]
        public int LogIdentifier
        {
            get { return m_LogIdentifier; }
            set { m_LogIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the structure identifier associated with the event.
        /// </summary>
        [XmlElement()]
        public int StructureIdentifier
        {
            get { return m_StructureIdentifier; }
            set { m_StructureIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the list of event variables that are associated with the event.
        /// </summary>
        /// <remarks>
        /// These consist of the event variables that are collected for each event plus the event specific variables.
        /// </remarks>
        [XmlElement()]
        public List<EventVariable> EventVariableList
        {
            get { return m_EventVariableList; }
            set { m_EventVariableList = value; }
        }

        /// <summary>
        /// Gets or sets the car identifier.
        /// </summary>
        [XmlElement()]
        public String CarIdentifier
        {
            get { return m_CarIdentifier; }
            set { m_CarIdentifier = value; }
        }
        #endregion - [Data Dictionary] -

        #region - [VCU] -
        /// <summary>
        /// Gets or sets the event index associated with the record.
        /// </summary>
        [XmlElement()]
        public short EventIndex
        {
            get { return m_EventIndex; }
            set { m_EventIndex = value; }
        }

        /// <summary>
        /// Gets or sets the time of the event.
        /// </summary>
        [XmlElement()]
        public string Time
        {
            get { return m_Time; }
            set { m_Time = value; }
        }

        /// <summary>
        /// Gets or sets the date of the event.
        /// </summary>
        [XmlElement()]
        public string Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        /// <summary>
        /// Gets or sets the date and time of the event as a .NET <c>DateTime</c> object.
        /// </summary>
        [XmlElement()]
        public DateTime DateTime
        {
            get { return m_DateTime; }
            set { m_DateTime = value; }
        }

        /// <summary>
        /// Gets or sets the flag that indicates whether a stream has been saved for the event.
        /// </summary>
        [XmlElement()]
        public bool StreamSaved
        {
            get { return m_StreamSaved; }
            set { m_StreamSaved = value; }
        }

        /// <summary>
        /// Gets or sets the stream number value that is to be used to access the datastream associated with this record.
        /// </summary>
        [XmlElement()]
        public short StreamNumber
        {
            get { return m_StreamNumber; }
            set { m_StreamNumber = value; }
        }
        #endregion - [VCU] -
        #endregion --- Properties ---
    }
}
