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
 *  Project:    Common
 * 
 *  File name:  CommunicationParentOffline.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/19/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  07/25/11    1.1     K.McD           1.  Changed the values returned in the call to the GetEmbeddedInformation() method.
 *  
 *  08/10/11    1.2     K.McD           1.  Added the constants associated with the call to the GetEmbeddedInformation methods.
 *                                      2.  Included a delay before returning from the DownloadChartRecorderWorkset() method to simulate actual
 *                                          download.
 *                                      
 *  08/24/11    1.3     K.McD           1.  Removed unnecessary constants.
 *  
 *  10/10/11    1.4     K.McD           1.  Changed the modifiers associated with a number of constant strings.
 *                                      2.  Made the car identifier variable static.
 *                                      
 *  05/13/15    1.5     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *                                      
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘WibuBox: [Present | Not Present]’. 
 *  
 *                                      Modifications
 *                                      1.  In order to accommodate the additional status labels, the size of the original status labels was reduced. The CarIdentifier
 *                                          constant value, which represents the car-identifier text that is displayed when the PTU is not connected to a physical
 *                                          car, was changed from "No Car" to "-" so that the status message "Car id.: -" would fit inside the status label.
 */
#endregion --- Revision History ---

using System;
using System.Threading;
using Common.Configuration;
using VcuComm;

namespace Common.Communication
{
    /// <summary>
    /// Base class to simulate communication with the target hardware.
    /// </summary>
    public class CommunicationParentOffline : ICommunicationParent
    {
        #region --- Constants ---
        #region - [GetEmbeddedInformation] -
        /// <summary>
        /// The version number of the embedded software. Value: "NO VERSION".
        /// </summary>
        protected const string Version = "NO VERSION";

        /// <summary>
        /// The car identifier. Value: "-".
        /// </summary>
        protected const string CarIdentifier = "-";

        /// <summary>
        /// The sub-system name. Value: "Bombardier Transportation Offline Mode".
        /// </summary>
        protected const string SubSystemName = "Bombardier Transportation Offline Mode";

        /// <summary>
        /// The project identifier. Value: "Bombardier".
        /// </summary>
        protected const string ProjectIdentifier = "Bombardier";

        /// <summary>
        /// The conversion mask. Value: 0.0.
        /// </summary>
        protected const double ConversionMask = 0.0;
        #endregion - [GetEmbeddedInformation] -

        /// <summary>
        /// The interval, in ms, to wait before returning from the DownloadChartRecorderWorkset() method. Value: 5 sec.
        /// </summary>
        private const int SleepIntervalMsDownloadChartRecorderWorkset = 5000;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// A table of the current watch elements, i.e. those watch variables that are currently monitored by the target hardware, mapped by element
        /// index.
        /// </summary>
        /// <remarks>Each watch element contains the: watch identifier; corresponding data type and current value of the watch variable being
        /// monitored.</remarks>
        protected WatchElement_t[] m_WatchElements;

        /// <summary>
        /// The communication settings associated with the selected target.
        /// </summary>
        protected CommunicationSetting_t m_CommunicationSetting;

        /// <summary>
        /// The current chart recorder mode.
        /// </summary>
        protected static ChartMode m_ChartMode = ChartMode.ZeroOutputMode;

        /// <summary>
        /// The car identifier.
        /// </summary>
        protected static string m_CarID = CarIdentifier;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        public CommunicationParentOffline()
        {
            // Initialize the watch element array.
            m_WatchElements = new WatchElement_t[Parameter.WatchSize];

            m_CommunicationSetting = new CommunicationSetting_t();
        }

        /// <summary>
        /// Initialize a new instance of the class and set the <c>CommunicationSetting</c> property to the specified communication setting.
        /// </summary>
        /// <param name="communicationSetting">The communication setting that is to be used to initialize the <c>CommunicationSetting</c> property.
        /// </param>
        public CommunicationParentOffline(CommunicationSetting_t communicationSetting) : this()
        {
            m_CommunicationSetting = communicationSetting;
        }

        /// <summary>
        /// Initialize a new instance of the class and set the properties and member variables to those values associated with the specified
        /// communication interface.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface containing the properties and member variables that are to be
        /// used to initialize the class.</param>
        public CommunicationParentOffline(ICommunicationParent communicationInterface)
        {
            // Initialize the watch element array.
            m_WatchElements = new WatchElement_t[Parameter.WatchSize];

            m_CommunicationSetting = communicationInterface.CommunicationSetting;
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Initialize the target hardware communication port.
        /// </summary>
        /// <param name="communicationsSetting">The communication settings.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.InitCommunication() method is not
        /// CommunicationError.Success.</exception>
        public virtual void InitCommunication(CommunicationSetting_t communicationsSetting)
        {
        }

        /// <summary>
        /// Close the target hardware communication port.
        /// </summary>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.CloseCommunication() method is not
        /// CommunicationError.Success.</exception>
        public virtual void CloseCommunication(Protocol protocol)
        {
        }

        /// <summary>
        /// Get the configuration information associated with the target hardware.
        /// </summary>
        /// <param name="targetConfiguration">The target configuration information retrieved from the target.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetEmbeddedInformation() method is
        /// not CommunicationError.Success.</exception>
        public void GetEmbeddedInformation(out TargetConfiguration_t targetConfiguration)
        {
            targetConfiguration = new TargetConfiguration_t();

            targetConfiguration.Version = Version;
            targetConfiguration.CarIdentifier = CarIdentifier;
            targetConfiguration.SubSystemName = SubSystemName;
            targetConfiguration.ProjectIdentifier = ProjectIdentifier;
            targetConfiguration.ConversionMask = ConversionMask;
        }

        /// <summary>
        /// Get the mode of the chart recorder. 
        /// </summary>
        /// <returns>The mode of the chart recorder: ramp, zero-output, full-scale, data.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.GetChartMode() method is not
        /// CommunicationError.Success.</exception>
        public ChartMode GetChartMode()
        {
            return m_ChartMode;
        }

        /// <summary>
        /// Set the mode of the chart recorder.
        /// </summary>
        /// <param name="chartMode">The required mode of the chart recorder.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.SetChartMode() method is not
        /// CommunicationError.Success.</exception>
        public void SetChartMode(ChartMode chartMode)
        {
            m_ChartMode = chartMode;
        }

        /// <summary>
        /// Assign the specified watch variable to the specified chart recorder channel index.
        /// </summary>
        /// <param name="channelIndex">The chart recorder channel index.</param>
        /// <param name="watchIdentifier">The watch identifier of the watch variable that is to be assigned to the channel.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from a call to the PTUDLL32.SetChartIndex() method is not
        /// CommunicationError.Success.</exception>
        public void SetChartIndex(short channelIndex, short watchIdentifier)
        {
        }

        /// <summary>
        /// Set the chart scaling for the specified watch variable.
        /// </summary>
        /// <param name="watchIdentifier">The watch identifier of the watch variables that is to be scaled.</param>
        /// <param name="maxChartScale">The watch variable engineering value associated with the maximum Y axis value.</param>
        /// <param name="minChartScale">The watch variable engineering value associated with the minimum Y axis value.</param>
        /// <exception cref="CommunicationException">Thrown if the error code returned from a call to the PTUDLL32.SetChartScale() method is not
        /// CommunicationError.Success.</exception>
        /// <exception cref="ArgumentException">Thrown if the specified <paramref name="watchIdentifier"/> does not exist in the current data
        /// dictionary.</exception>"
        public void SetChartScale(short watchIdentifier, double maxChartScale, double minChartScale)
        {
        }

        /// <summary>
        /// Download the specified chart recorder workset to the VCU.
        /// </summary>
        /// <param name="workset">The workset that is to be downloaded to the VCU.</param>
        public void DownloadChartRecorderWorkset(Workset_t workset)
        {
            Thread.Sleep(SleepIntervalMsDownloadChartRecorderWorkset);
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the communication settings associated with the selected target.
        /// </summary>
        public CommunicationSetting_t CommunicationSetting
        {
            get { return m_CommunicationSetting; }
            set { m_CommunicationSetting = value; }
        }

        /// <summary>
        /// Gets the communication device used to communicate with the selected VCU. intentionally
        /// return null since an actual communication device is not used when offline
        /// </summary>
        public ICommDevice CommDevice
        {
            get { return null; }
        }

        /// <summary>
        /// TOOD
        /// </summary>
        public EventStreamMarshal EventStreamMarshall
        {
            get { return null; }
        }

        /// <summary>
        /// TOOD
        /// </summary>
        public WatchClockMarshal WatchClockMarshall
        {
            get { return null; }
        }

        /// <summary>
        /// TOOD
        /// </summary>
        public SelfTestMarshal SelfTestMarshall
        {
            get { return null; }
        }

        #endregion --- Properties ---
    }
}
