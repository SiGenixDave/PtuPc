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
 *  File name:  ICommunicationWatch.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  04/27/11    1.1     K.McD           1.  Added the communication methods required to configure the chart recorder and set the mode of the chart recorder.
 *  
 *  05/24/11    1.2     K.McD           1.  Moved the SetChartMode(), SetChartIndex() and SetChartScale() methods to the ICommunicationParent interface.
 *  
 *  07/25/11    1.3     K.McD           1.  Removed the ConvertToWorkset() method as it is no longer used.
 *  
 *  03/03/15    2.0     K.McD           1.  Removed the  SetWatchElement() method as it is not used.
 *
 */
#endregion --- Revision History ---

using Common.Configuration;

namespace Common.Communication
{
    /// <summary>
    /// An interface to define the communication methods associated with the watch sub-system - Watch.dll.
    /// </summary>
    public interface ICommunicationWatch : ICommunicationParent
    {
        /// <summary>
        /// Write the specified data to the watch variable specified by the <paramref name="dictionaryIndex"/> parameter.
        /// </summary>
        /// <param name="dictionaryIndex">The disctionary index.</param>
        /// <param name="dataType">The data type.</param>
        /// <param name="data">The data.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.CloseCommunication() method is not CommunicationError.Success.</exception>
        void SendVariable(short dictionaryIndex, short dataType, double data);

        /// <summary>
        /// Map the watch identifiers listed in <paramref name="watchElementList"/> to the watch element array monitored by the target hardware.
        /// </summary>
        /// <remarks> The number of watch identifiers in the list must mot exceed <c>WatchSize</c>.</remarks>
        /// <param name="watchElementList">The list containing the watch identifiers that are to be mapped to each element of the watch element array.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetWatchElements() method is not
        /// CommunicationError.Success.</exception>
        void SetWatchElements(System.Collections.Generic.List<short> watchElementList);

        /// <summary>
        /// Retrieve the watch elements from the target hardware.
        /// </summary>
        /// <remarks>The watch elements are the watch values that are being monitored by the target hardware as defined by the <c>SetWatchElements()> method.</c>
        /// </remarks>
        /// <returns>The retrieved watch element table, if successful; otherwise, null.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.UpdateElements() method is not
        /// CommunicationError.Success.</exception>
        WatchElement_t[] UpdateWatchElements(bool forceUpdate);

        /// <summary>
        /// Update the watch variable lookup table with the latest watch element data retrieved from the targer hardware.
        /// </summary>
        /// <param name="watchElements">The watch element table retrieved from the target hardware.</param>
        void UpdateWatchVariableTable(Common.Communication.WatchElement_t[] watchElements);

        /// <summary>
        /// Get the watch variable identifiers of the watch variables that are currently assigned to the chart recorder channels.
        /// </summary>
        /// <returns>An array of the watch variable identifiers corresponding to the watch variables that are currently assigned to the chart recorder channels.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetChartIndex() method is not
        /// CommunicationError.Success.</exception>
        short[] GetChartConfiguration();

        /// <summary>
        /// Configure the chart recorder channels i.e. assign each channel of the chart recorder to a specific watch variable.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from a call to the PTUDLL32.SetChartIndex() method is not
        /// CommunicationError.Success.</exception>
        void ConfigureChartRecorderChannels(short[] watchIdentifiers);
    }
}
