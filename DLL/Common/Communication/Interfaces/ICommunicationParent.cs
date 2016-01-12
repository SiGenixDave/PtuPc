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
 *  File name:  ICommunicationParent.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  10/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/14/11    1.1     K.McD           1.  Removed the WatchSize parameter.
 * 
 *  04/27/11    1.2     K.McD           1.  Added the GetChartMode() method as this is required by both the main PTU application and by the Watch.dll.
 *  
 *  05/24/11    1.3     K.McD           1.  Moved the SetChartMode(), SetChartIndex() and SetChartScale() methods from the ICommunicationWatch interface.
 *                                      2.  Added to DownloadChartRecorderWorkset() method.
 *
 */
#endregion --- Revision History ---

using Common.Configuration;
using VcuComm;

namespace Common.Communication
{
    /// <summary>
    /// A base interface to define the communication methods required to communicate with a vehicle control unit (VCU).
    /// </summary>
    public interface ICommunicationParent
    {
        #region - [Methods] -
        /// <summary>
        /// Initialize the communication port.
        /// </summary>
        /// <param name="communicationsSetting">The communication settings.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.InitCommunication() method is not
        /// CommunicationError.Success.</exception>
        void InitCommunication(CommunicationSetting_t communicationsSetting);

        /// <summary>
        /// Close the communication port.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.CloseCommunication() method is not
        /// CommunicationError.Success.</exception>
        void CloseCommunication(Protocol protocol);

        /// <summary>
        /// Get the embedded software information.
        /// </summary>
        /// <param name="targetConfiguration">The target configuration information retrieved from the target.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetEmbeddedInformation() method is not
        /// CommunicationError.Success.</exception>
        void GetEmbeddedInformation(out TargetConfiguration_t targetConfiguration);

        /// <summary>
        /// Get the mode of the chart recorder. 
        /// </summary>
        /// <returns>The mode of the chart recorder: ramp, zero-output, full-scale, data.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetChartMode() method is not
        /// CommunicationError.Success.</exception>
        ChartMode GetChartMode();

        /// <summary>
        /// Set the mode of the chart recorder.
        /// </summary>
        /// <param name="chartMode">The required mode of the chart recorder.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetChartMode() method is not
        /// CommunicationError.Success.</exception>
        void SetChartMode(ChartMode chartMode);

        /// <summary>
        /// Assign the specified watch variable to the specified chart recorder channel index.
        /// </summary>
        /// <param name="channelIndex">The chart recorder channel index.</param>
        /// <param name="watchIdentifier">The watch identifier of the watch variable that is to be assigned to the channel.</param>
        void SetChartIndex(short channelIndex, short watchIdentifier);

        /// <summary>
        /// Set the chart scaling for the specified watch variable.
        /// </summary>
        /// <param name="watchIdentifier">The watch identifier of the watch variables that is to be scaled.</param>
        /// <param name="maxChartScale">The watch variable engineering value associated with the maximum Y axis value.</param>
        /// <param name="minChartScale">The watch variable engineering value associated with the minimum Y axis value.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from a call to the PTUDLL32.SetChartScale() method is not
        /// CommunicationError.Success.</exception>
        void SetChartScale(short watchIdentifier, double maxChartScale, double minChartScale);

        /// <summary>
        /// Download the specified chart recorder workset.
        /// </summary>
        /// <param name="workset">The workset that is to be downloaded to the VCU.</param>
        void DownloadChartRecorderWorkset(Workset_t workset);
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets or sets the communication settings associated with the selected VCU.
        /// </summary>
        CommunicationSetting_t CommunicationSetting { get; set; }

        /// <summary>
        /// Gets the communication device used to communicate with the selected VCU.
        /// </summary>
        ICommDevice CommDevice { get; }

        /// <summary>
        /// TODO
        /// </summary>
        WatchClockMarshal WatchClockMarshall { get; }

        /// <summary>
        /// TODO
        /// </summary>
        EventStreamMarshal EventStreamMarshall { get; }

        /// <summary>
        /// TODO
        /// </summary>
        SelfTestMarshal SelfTestMarshall { get; }

        #endregion - [Properties] -
    }
}
