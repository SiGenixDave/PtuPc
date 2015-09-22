#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Watch
 * 
 *  File name:  CommunicationWatchOffline.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/19/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  08/10/11    1.1     Sean.D          1.  Added the code for the SetWatchElement() method.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Common.Communication;
using Common.Configuration;

namespace Watch.Communication
{
    /// <summary>
    /// Class to simulate communication with the target hardware with respect to the watch sub-system.
    /// </summary>
    public class CommunicationWatchOffline : CommunicationParentOffline, ICommunicationWatch
    {
        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class and set the properties and member variables to those values associated with the specified
        /// communication interface.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface containing the properties and member variables that are to be
        /// used to initialize the class.</param>
        public CommunicationWatchOffline(ICommunicationParent communicationInterface)
            : base(communicationInterface)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Write the specified data to the watch variable specified by the <paramref name="dictionaryIndex"/> parameter.
        /// </summary>
        /// <param name="dictionaryIndex">The dictionary index.</param>
        /// <param name="dataType">The data type.</param>
        /// <param name="data">The data.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.CloseCommunication() method is not
        /// CommunicationError.Success.</exception>
        public void SendVariable(short dictionaryIndex, short dataType, double data)
        {
            try
            {
                Lookup.WatchVariableTable.Items[dictionaryIndex].DataTypeFromTarget = dataType;
                Lookup.WatchVariableTable.Items[dictionaryIndex].ValueFromTarget = data;
            }
            catch(Exception)
            {
                // Do nothing, just ensure that an exception isn't thrown.
            }
        }

        /// <summary>
        /// Map the watch identifiers listed in <paramref name="watchElementList"/> to the watch element array monitored by the target hardware.
        /// </summary>
        /// <remarks> The number of watch identifiers in the list must mot exceed <c>WatchSize</c>.</remarks>
        /// <param name="watchElementList">The list containing the watch identifiers that are to be mapped to each element of the watch element
        /// array.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetWatchElements() method is not
        /// CommunicationError.Success.</exception>
        public void SetWatchElements(List<short> watchElementList)
        {
            // Skip, if the parameter isn't defined.
            if (watchElementList == null)
            {
                return;
            }

            Debug.Assert(watchElementList.Count <= Parameter.WatchSize,
                                                "CommunicationWatch.SetWatchElements - [watchElementList.Count <= Parameter.WatchSize]");

            short[] watchElements = new short[Parameter.WatchSize];
            Array.Copy(watchElementList.ToArray(), watchElements, watchElementList.Count);

            // Keep a record up the new mapping beween the each element index and the watch identifier.
            for (short elementIndex = 0; elementIndex < Parameter.WatchSize; elementIndex++)
            {
                m_WatchElements[elementIndex].WatchIdentifier = watchElements[elementIndex];

                // Required to map between the watch identifier and the element index.
                m_WatchElements[elementIndex].ElementIndex = elementIndex;
            }
        }

        /// <summary>
        /// Map an individual watch identifier to a specific watch element.
        /// </summary>
        /// <param name="elementIndex">The index of the watch element array that is to be set.</param>
        /// <param name="watchIdentifier">The watch identifier corresponding to the watch variable that is to be mapped into the watch element table at
        /// the specified index.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetWatchElement() method is not
        /// CommunicationError.Success.</exception>
        public void SetWatchElement(short elementIndex, short watchIdentifier)
        {
            m_WatchElements[elementIndex].WatchIdentifier = watchIdentifier;
            m_WatchElements[elementIndex].ElementIndex = elementIndex;
        }

        /// <summary>
        /// Retrieve the watch elements from the target hardware.
        /// </summary>
        /// <remarks>The watch elements are the watch values that are being monitored by the target hardware as defined by the <c>SetWatchElements()
        /// </c> method.</remarks>
        /// <returns>The retrieved watch element table, if successful; otherwise, null.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.UpdateElements() method is not
        /// CommunicationError.Success.</exception>
        public WatchElement_t[] UpdateWatchElements(bool forceUpdate)
        {
            WatchElement_t[] watchElements = new WatchElement_t[Parameter.WatchSize];

            double[] watchValues = new double[Parameter.WatchSize];
            short[] watchDataTypes = new short[Parameter.WatchSize];

            // Initialize the data values and data types.
            WatchVariable watchVariable;
            for (int elementIndex = 0; elementIndex < Parameter.WatchSize; elementIndex++)
            {
                try
                {
                    watchVariable = Lookup.WatchVariableTable.Items[m_WatchElements[elementIndex].WatchIdentifier];
                    if (watchVariable == null)
                    {
                        throw new Exception();
                    }
                    watchValues[elementIndex] = watchVariable.ValueFromTarget;
                    watchDataTypes[elementIndex] = watchVariable.DataTypeFromTarget;
                }
                catch (Exception)
                {
                    watchValues[elementIndex] = 0;
                    watchDataTypes[elementIndex] = 0;
                }
            }

            // Map the values retrieved from the target hardware into the watch element table.
            for (short elementIndex = 0; elementIndex < Parameter.WatchSize; elementIndex++)
            {
                watchElements[elementIndex] = new WatchElement_t();
                watchElements[elementIndex].Value = watchValues[elementIndex];
                watchElements[elementIndex].DataType = watchDataTypes[elementIndex];
                watchElements[elementIndex].WatchIdentifier = m_WatchElements[elementIndex].WatchIdentifier;
                watchElements[elementIndex].ElementIndex = elementIndex;
            }

            return watchElements;
        }

        /// <summary>
        /// Update the watch variable lookup table with the latest watch element data retrieved from the targer hardware.
        /// </summary>
        /// <param name="watchElements">The watch element table retrieved from the target hardware.</param>
        public void UpdateWatchVariableTable(WatchElement_t[] watchElements)
        {
            short watchIdentifier;
            for (int elementIndex = 0; elementIndex < watchElements.Length; elementIndex++)
            {
                watchIdentifier = watchElements[elementIndex].WatchIdentifier;

                try
                {
                    Lookup.WatchVariableTable.Items[watchIdentifier].ValueFromTarget = watchElements[elementIndex].Value;
                    Lookup.WatchVariableTable.Items[watchIdentifier].DataTypeFromTarget = watchElements[elementIndex].DataType;
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        #region - [Chart Recorder] -
        /// <summary>
        /// Get the watch variable identifiers of the watch variables that are currently assigned to the chart recorder channels.
        /// </summary>
        /// <returns>An array of the watch variable identifiers corresponding to the watch variables that are currently assigned to the chart recorder
        /// channels.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetChartIndex() method is not
        /// CommunicationError.Success.</exception>
        public short[] GetChartConfiguration()
        {
            short[] watchIdentifiers = new short[Parameter.WatchSizeChartRecorder];
            return watchIdentifiers;
        }

        /// <summary>
        /// Configure the chart recorder channels i.e. assign each channel of the chart recorder to a specific watch variable.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from a call to the PTUDLL32.SetChartIndex() method is not
        /// CommunicationError.Success.</exception>
        public void ConfigureChartRecorderChannels(short[] watchIdentifiers)
        {
        }
        #endregion - [Chart Recorder] -
        #endregion --- Methods ---
    }
}
