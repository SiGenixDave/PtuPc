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
 *  File name:  CommunicationWatch.cs
 *
 *  Revision History
 *  ----------------
 */

#region - [1.0 to 1.12] -

/*
 *  Date        Version Author          Comments
 *  09/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *
 *  09/29/10    1.1     K.McD           1.  Added the SendVariable() method.
 *                                      2.  Modified the layout of the code to be consistent with ICommunication.
 *
 *  10/15/10    1.2     K.McD           1.  Removed those communication methods not related to the watch subsystem.
 *                                      2.  Now inherits from the CommunicationParent class.
 *
 *  01/16/11    1.3     K.McD           1.  Bug fix - SNCR001.86. Under the previous implementation, if a workset contained less than WatchSize watch
 *                                          variables, there was
 *                                          a possibility that the display values associated with one or more watch variables would, under certain
 *                                          circumstances, be incorrect. This bug was caused because not all elements of the m_WatchElements array were
 *                                          updated in the SetWatchElements() method if the new workset being displayed contained less than WatchSize
 *                                          entries.
 *
 *                                          This problem was addressed by making the following modifications:
 *                                              (a) Used the Array.Copy() method to copy the watchElementList parameter to the initialized
 *                                                  watchElements array in the SetWatchElements() method. This ensures that all WatchSize watch
 *                                                  elements are updated when making a call to the PTUDLL32.SetWatchElements() method. The watch
 *                                                  identifiers of those watch elements that are not used are set to 0.
 *
 *                                              (b) Modified the for loop within the SetWatchElements() method to update all elements of the
 *                                                  m_WatchElements array.
 *
 *                                      2.  Removed the SimulateCommunicationLink conditional compilation.
 *
 *  01/31/11    1.4     K.McD           1.  Included support for the mutex, introduced in version 1.11 of Common.dll, used to control read/write access
 *                                          to the communication port.
 *
 *  02/14/11    1.5     K.McD           1.  Removed unused constructors.
 *                                      2.  Added support for debug mode.
 *
 *  03/28/11    1.6     K.McD           1.  Included a try/catch block in the UpdateWatchVariableTable() method, just in case.
 *
 *  04/27/11    1.7     K.McD           1.  Included the communication methods required to configure the chart recorder.
 *                                      2.  Auto-modified as a result of name changes to a number of resources.
 *
 *  05/23/11    1.8     K.McD           1.  Modified the XML tag associated with the ConvertToWorkset() method.
 *
 *  05/24/11    1.9     K.McD           1.  Moved the SetChartMode(), SetChartIndex and SetChartScale() methods to the CommunicationParent class.
 *
 *  05/26/11    1.10    K.McD           1.  Modified the ConvertToWorkset class such that the chart recorder upper and lower limits are set to
 *                                          'double.NaN', i.e. undefined, as these are not retrieved from the VCU and are unknown.
 *
 *  07/20/11    1.11    K.McD           1.  Corrected a number of XML tags and comments.
 *                                      2.  Removed the ConvertToWorkset() method as it is no longer used.
 *                                      3.  Modified the signature of the constructor to use the ICommunicationParent interface.
 *
 *  02/27/14    1.12    K.McD           1.  Minor correction to the <remarks> XML tag in the SetWatchElements() method.
 *                                      2.  Modified the call to PTUDLL32.GetChartIndex() in the GetChartConfiguration() method to match the
 */

#endregion - [1.0 to 1.12] -

#region - [1.13] -

/*
 *  03/11/15    1.13     K.McD          References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *
 *                                          1.  Implement changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications
 *                                              to support both 32 and 64 bit architecture.
 *
 *                                      Modifications
 *                                      1.  Replaced all calls to the methods within PTUDLL32.dll with calls via function delegates. This allows
 *                                          support for both 32 and 64 bit systems.
 *
 *                                      2.  Modified each of the methods to check that the function delegate has been initialized prior to its use.
 *
 *                                      3.  Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted,
 *                                          the method was modified to a check that the Mutex has been initialized prior to its use.
 *
 *                                      4.  Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted,
 *                                          a 'finally' block was added to each 'try' block to ensure that the Mutex is released even if an exception
 *                                          occurs. The code pattern was modified to use the following template:
 *
 *                                          CommunicationError errorCode = CommunicationError.UnknownError;
 *                                          try
 *                                          {
 *                                              m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
 *                                              errorCode = (CommunicationError)m_<function-name>( ... );
 *                                          }
 *                                          catch (Exception)
 *                                          {
 *                                              errorCode = CommunicationError.SystemException;
 *                                              throw new CommunicationException(Resources.EMGetTargetConfigurationFailed, errorCode);
 *                                          }
 *                                          finally
 *                                          {
 *                                              m_MutexCommuncationInterface.ReleaseMutex();
 *                                          }
 *
 *                                          if (DebugMode.Enabled == true)
 *                                          {
 *                                              ...
 *                                          }
 *
 *                                          if (errorCode != CommunicationError.Success)
 *                                          {
 *                                              throw new CommunicationException("<function-name>", errorCode);
 *                                          }
 */

#endregion - [1.13] -

#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Common;
using Common.Communication;
using Common.Configuration;
using Watch.Properties;

namespace Watch.Communication
{
    /// <summary>
    /// Class to communicate with the target hardware with respect to the watch sub-system.
    /// </summary>
    public class CommunicationWatch : CommunicationParent, ICommunicationWatch
    {
        #region --- Constructors ---

        /// <summary>
        /// Initialize a new instance of the class and set the function delegates, properties and member variables to those values associated with the
        /// specified communication interface.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface containing the properties and member variables that are to be
        /// used to initialize the class.</param>
        public CommunicationWatch(ICommunicationParent communicationInterface)
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
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationWatch.SendVariable() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_WatchClockMarshal.SendVariable(dictionaryIndex, dataType, data);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMSendVariableFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMSendVariableFailed, errorCode);
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.SendVariable_t sendVariable = new DebugMode.SendVariable_t(dictionaryIndex, dataType, data, errorCode);
                DebugMode.Write(sendVariable.ToXML());
            }
        }

        /// <summary>
        /// Map the watch identifiers listed in <paramref name="watchElementList"/> to the watch element array monitored by the target hardware.
        /// </summary>
        /// <remarks> The number of watch identifiers in the list must not exceed <c>WatchSize</c>.</remarks>
        /// <param name="watchElementList">The list containing the watch identifiers that are to be mapped to each element of the watch element array.
        /// </param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetWatchElements() method is not
        /// CommunicationError.Success.</exception>
        public void SetWatchElements(List<short> watchElementList)
        {
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationWatch.SetWatchElements() - [m_MutexCommuncationInterface != null]");

            // Skip, if the parameter isn't defined.
            if (watchElementList == null)
            {
                return;
            }

            Debug.Assert(watchElementList.Count <= Parameter.WatchSize,
                         "CommunicationWatch.SetWatchElements - [watchElementList.Count <= Parameter.WatchSize]");

            short[] watchElements = new short[Parameter.WatchSize];
            Array.Copy(watchElementList.ToArray(), watchElements, watchElementList.Count);

            // Send the mapping to the target hardware.
            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = m_WatchClockMarshal.SetWatchElements(watchElements);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMSetWatchElementsFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.SetWatchElements_t setWatchElements = new DebugMode.SetWatchElements_t(watchElements, errorCode);
                DebugMode.Write(setWatchElements.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMSetWatchElementsFailed, errorCode);
            }

            // Keep a record up the new mapping between the each element index and the watch identifier.
            for (short elementIndex = 0; elementIndex < Parameter.WatchSize; elementIndex++)
            {
                m_WatchElements[elementIndex].WatchIdentifier = watchElements[elementIndex];

                // Required to map between the watch identifier and the element index.
                m_WatchElements[elementIndex].ElementIndex = elementIndex;
            }
        }

        /// <summary>
        /// Retrieve the watch elements from the target hardware.
        /// </summary>
        /// <remarks>The watch elements are the watch values that are being monitored by the target hardware as defined by the
        /// <c>SetWatchElements()> method.</c></remarks>
        /// <returns>The retrieved watch element table, if successful; otherwise, null.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the VcuCommunication32/VcuCommunication64
        /// UpdateElements() method is not CommunicationError.Success.</exception>
        public WatchElement_t[] UpdateWatchElements(bool forceUpdate)
        {
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationWatch.UpdateWatchElements() - [m_MutexCommuncationInterface != null]");

            WatchElement_t[] watchElements = new WatchElement_t[Parameter.WatchSize];
            double[] watchValues = new double[Parameter.WatchSize];
            short[] watchDataTypes = new short[Parameter.WatchSize];

            short forceUpdateAsShort = (forceUpdate == true) ? ForceUpdateTrue : ForceUpdateFalse;
            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_WatchClockMarshal.UpdateWatchElements(forceUpdateAsShort, watchValues, watchDataTypes);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMUpdateWatchElementsFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (DebugMode.Enabled == true)
            {
                DebugMode.UpdateWatchElements_t updateWatchElements = new DebugMode.UpdateWatchElements_t(forceUpdateAsShort, watchValues,
                                                                                                          watchDataTypes, errorCode);
                DebugMode.Write(updateWatchElements.ToXML());
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMUpdateWatchElementsFailed, errorCode);
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

        /// <summary>
        /// Get the watch variable identifiers of the watch variables that are currently assigned to the chart recorder channels.
        /// </summary>
        /// <returns>An array of the watch variable identifiers corresponding to the watch variables that are currently assigned to the chart recorder
        /// channels.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetChartIndex() method is not
        /// CommunicationError.Success.</exception>
        public short[] GetChartConfiguration()
        {
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationWatch.GetChartConfiguration() - [m_MutexCommuncationInterface != null]");

            short[] watchIdentifiers = new short[Parameter.WatchSizeChartRecorder];
            short chartRecorderChannel;
            CommunicationError errorCode = CommunicationError.UnknownError;
            for (short chartIndex = 0; chartIndex < Parameter.WatchSizeChartRecorder; chartIndex++)
            {
                try
                {
                    m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                    errorCode = m_WatchClockMarshal.GetChartIndex(chartIndex, ref watchIdentifiers[chartIndex]);
                }
                catch (Exception)
                {
                    errorCode = CommunicationError.SystemException;
                    chartRecorderChannel = (short)(chartIndex + 1);
                    throw new CommunicationException(string.Format(Resources.EMGetChartRecorderWatchIDFailed, chartRecorderChannel.ToString()),
                                                     errorCode);
                }
                finally
                {
                    m_MutexCommuncationInterface.ReleaseMutex();
                }

                if (errorCode != CommunicationError.Success)
                {
                    chartRecorderChannel = (short)(chartIndex + 1);
                    throw new CommunicationException(string.Format(Resources.EMGetChartRecorderWatchIDFailed, chartRecorderChannel.ToString()),
                                                     errorCode);
                }
            }

            return watchIdentifiers;
        }

        /// <summary>
        /// Configure the chart recorder channels i.e. assign each channel of the chart recorder to a specific watch variable.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from a call to the PTUDLL32.SetChartIndex() method is not
        /// CommunicationError.Success.</exception>
        public void ConfigureChartRecorderChannels(short[] watchIdentifiers)
        {
            Debug.Assert(watchIdentifiers != null, "CommunicationWatch.ConfigureChartRecorderChannels() - [watchIdentifiers != null]");
            Debug.Assert(watchIdentifiers.Length == Parameter.WatchSizeChartRecorder,
                         "CommunicationWatch.ConfigureChartRecorderChannels() - [watchIdentifiers.Length == Parameter.WatchSizeChartRecorder]");
            Debug.Assert(m_MutexCommuncationInterface != null,
                         "CommunicationWatch.ConfigureChartRecorderChannels() - [m_MutexCommuncationInterface != null]");

            short chartRecorderChannel;
            CommunicationError errorCode = CommunicationError.UnknownError;
            for (short channelIndex = 0; channelIndex < Parameter.WatchSizeChartRecorder; channelIndex++)
            {
                try
                {
                    m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                    errorCode = m_WatchClockMarshal.SetChartIndex(channelIndex, watchIdentifiers[channelIndex]);
                }
                catch (Exception)
                {
                    errorCode = CommunicationError.SystemException;
                    chartRecorderChannel = (short)(channelIndex + 1);
                    throw new CommunicationException(string.Format(Resources.EMGetChartRecorderWatchIDFailed, chartRecorderChannel.ToString()),
                                                     errorCode);
                }
                finally
                {
                    m_MutexCommuncationInterface.ReleaseMutex();
                }

                if (errorCode != CommunicationError.Success)
                {
                    chartRecorderChannel = (short)(channelIndex + 1);
                    throw new CommunicationException(string.Format(Resources.EMSetChartRecorderWatchIDFailed, chartRecorderChannel.ToString()),
                                                     errorCode);
                }
            }
        }

        #endregion --- Methods ---
    }
}