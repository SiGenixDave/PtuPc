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
 *  File name:  Table.Generic.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *          
 *  01/26/11    1.1     K.McD           1.  Changed the access modifiers asociated with the m_Records and m_RecordList member variables.
 */
#endregion --- Revision History ---

using System;
using System.Data;
using System.Collections.Generic;

namespace Common.Configuration
{
    /// <summary>
    /// Generic abstract class to help the programmer create child classes that simplify access to all primary key data tables in the data dictionary.
    /// </summary>
    /// <typeparam name="R">The class/interface that defines the fields associated with the records of the data table.</typeparam>
    /// <typeparam name="T">The primary key data table, <seealso cref="Record"/>, of the data dictionary that is to be processed.</typeparam>
    public abstract class Table<R, T> where R : Record where T : DataTable
    {
        #region --- Member Variables ---
        /// <summary>
        /// The maximum value of the primary key identifier, <seealso cref="Record"/>, in the specified data table.
        /// </summary>
        protected int m_IdentifierMax;

        /// <summary>
        /// An array of the records in the specified table. The array index corresponds to the record identifier.
        /// </summary>
        protected R[] m_Records;

        /// <summary>
        /// A generic list of the records in the specified table, ordered by record identifier.
        /// </summary>
        protected List<R> m_RecordList;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Table(T dataTable)
        {
            if (dataTable == null)
            {
                return;
            }

            m_Records = BuildDataTable(dataTable);

            // Convert the variable array to a generic list for convenience and manipulation.
            m_RecordList = new List<R>();
            m_RecordList.AddRange(m_Records);
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Builds an array of the records contained within the specified data table and determines the maximum value of the primary key identifier associated with the 
        /// data table.
        /// </summary>
        /// <param name="dataTable">The primary key data table of the data dictionary that is to be processed.</param>
        /// <returns>An array of the records in the specified data table.</returns>
        protected virtual R[] BuildDataTable(T dataTable)
        {
            return null;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets an indexed array of the records in the specified data table. The array index corresponds to the unique identifier.
        /// </summary>
        public R[] Items
        {
            get { return m_Records; }
        }

        /// <summary>
        /// Gets a generic list of the records in the specified data table, ordered by unique identifier.
        /// </summary>
        public List<R> RecordList
        {
            get { return m_RecordList; }
        }

        /// <summary>
        /// Gets the maximum value of the primary key identifier, <seealso cref="Record"/>, in the specified data table
        /// </summary>
        public int IdentifierMax
        {
            get { return m_IdentifierMax; }
        }
        #endregion --- Properties ---
    }
}
