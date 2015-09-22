#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    WibuKey
 * 
 *  File name:  MenuInterfaceWibuKey.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  05/25/13    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  02/26/14    1.1     K.McD           1.  Added a number of <remarks> XML tags.
 *  
 *  05/06/15    1.2     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *
 *                                          1.  NK-U-6505 Section 2.2. Mandatory MDI Global Screen Representation - Part 1. The proposal is to add additional status
 *                                              labels to the status bar at the bottom of the PTU screen to include ‘Log: [Saved | Unsaved]’ and
 *                                              ‘WibuBox: [Present | Not Present]’.
 *                                              
 *                                      Modifications
 *                                      1.  Modified the WibuBoxCheckIfRemoved() method so that the software behaves in exactly the same way for a development WibuBox
 *                                          dongle as it does for a client WibuBox dongle.
 *                                          
 *                                      2.  Modified the signature of the WibuBoxCheckForValidEntry() method to include a suppressMessageBox flag. If this flag is
 *                                          asserted, the method does not call the MessageBox.Show() method to inform the user that a WibuBox was not found or was
 *                                          invalid.
 *                                          
 *  07/24/15    1.3     K.McD           References
 *                                      1.  An informal review of version 6.11 of the PTU concluded that, where possible - i.e. if the PTU is started from a shortcut
 *                                          that passes the project identifier as a shortcut parameter, the project specific PTU initialization should be carried out
 *                                          in the MDI Form contructor that has the parameter string array as its signature rather than by the LoadDictionary() method.
 *                                          This streamlines the display construction of the Control Panel associated with the R188 project. In the 6.11 implementation
 *                                          the CTA layout is momentarily displayed before the Control Panel is drawn, however by initializing the project specific
 *                                          features in the constructor the Control Panel associated with the R188 project is drawn immediately and the CTA layout
 *                                          is not shown at all.
 *  
 *                                      Modifications
 *                                      1.  Modified both the WibuBoxCheckIfRemoved() and WibuBoxCheckForValidEntry() methods so that they did not rely upon the
 *                                          Parameter class for the WibuBox UserCode and SlotId. This allows them to be used in the constructor of the MdiPTU
 *                                          Multiple Document Interface Form before the Parameter class is initialized. The UserCode and SlotId for the 
 *                                          WibuBox are now derived from member variables that are initialized from constants by the WibuBoxCheckIfRequired() method
 *                                          defined in the parent class, much like the WibuBox FirmCode.
 *                                          
 *                                      2.  Renamed m_WibuBoxDevelopment to m_WibuBoxIsDevelopment.
 *                                      
 *                                      3.  Renamed the flag m_WibuBoxIsRequired to m_WibuBoxIsInitialized and included a check that this flag is asserted
 *                                          in both the WibuBoxCheckIfRemoved() and WibuBoxCheckForValidEntry() methods as these methods will both fail if the
 *                                          call to the WibuBoxCheckIfRequired() method in the Common.MenuInterface() parent class is not made as it is here
 *                                          where the UserCode and SlotId for the WibuBox are initialized. Also changed the error message and the name of the
 *                                          resource displaying the error message from Resources.MBTWibuBoxNotRequired to Resources.MBTWibuBoxNotInitilized.
 *  
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;
using Common;
using WibuKey.Properties;
using WIBUKEYLib;

namespace WibuKey
{
    /// <summary>
    /// The interface between the main PTU application and the WibuKey sub-system  - WibuKey.dll.
    /// </summary>
    public class MenuInterfaceWibuKey : MenuInterface
    {
        #region --- Constants ---
        /// <summary>
        /// A string containing the data that is to be encrypted. Value: "12345678".
        /// </summary>
        private const string EncryptionData = "12345678";

        /// <summary>
        /// The user code of the development WibuBox. Value: 600615.
        /// </summary>
        private const int WkDevUser = 600615;

        /// <summary>
        /// The firm code of the development WibuBox. Value: 10.
        /// </summary>
        private const int WkDevFirm = 10;

        /// <summary>
        /// The slot number where the firm and user codes are programmed for the development dongle. Value: 4.
        /// </summary>
        /// <remarks>This value must be the same as the slot number that is used to program the client firm and user codes.</remarks>
        private const int WkDevSlot = 4;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// A flag to indicate whether a WibuBox device with the correct Firm and User codes programmed into the appropriate slot has been found. True, if a valid WibuBox
        /// has been found; otherwise, false. 
        /// </summary>
        private bool m_WibuBoxValid = false;

        /// <summary>
        /// A flag that indicates whether a WibuBox device has been found. True, if a WibuBox device has been found; otherwise, false.
        /// </summary>
        private bool m_WibuBoxFound = false;

        /// <summary>
        /// A flag to indicate whether the WibuBox has been removed. True, if the WibuBox has been removed; otherwise, false.
        /// </summary>
        private bool m_WibuBoxRemoved = true;

        /// <summary>
        /// A reference to the WibuKey Component Object Model (COM) Application Programmer Interface (API).
        /// </summary>
        private Wibukey m_WibuKey;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="mainWindow">Reference to the main application window interface.</param>
        public MenuInterfaceWibuKey(IMainWindow mainWindow)
            : base(mainWindow)
        {
            // Instantiate a new WibuKey object.
            m_WibuKey = new Wibukey();
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Check whether the WibuBox/U+ device has been removed from the port. This method can only be called following a successful call to the
        /// WibuBoxCheckForValidEntry() method i.e. the call determined that a valid WibuBox is connected to the USB or parallel port of the computer.
        /// </summary>
        /// <remarks>This method is called at regular intervals if the current project supports WibuBox/U+ hardware and the user is logged 
        /// into the Engineering account. If the method indicates that the WibuBox/U+ hardware has been removed the user is automatically logged out of the Engineering
        /// account.</remarks>
        /// <returns>A flag to indicate whether the WibuBox has been removed from the port. True, if the WibuBox has been removed; otherwise, false.</returns>
        public bool WibuBoxCheckIfRemoved()
        {
            // Default to the WibuBox having been removed.
            m_WibuBoxRemoved = true;

            try
            {
                // This method should only be called if the WibuBox security device has been initialized by calling the WibuBoxCheckIfRequired() method.
                if (m_WibuBoxIsInitialized == false)
                {
                    throw new InvalidOperationException(Resources.MBTWibuBoxNotInitialized);
                }

                // Ensure that the WibuKey data is refreshed.
                m_WibuKey.PurgeAll();

                // Check whether the user logged in using a development WibuBox.
                if (m_WibuBoxIsDevelopment == true)
                {
                    m_WibuKey.FirmCode = WkDevFirm;
                    m_WibuKey.UserCode = WkDevUser;
                }
                else
                {
                    m_WibuKey.FirmCode = m_FirmCode;
                    m_WibuKey.UserCode = m_UserCode;
                }

                // Check that a WibuBox with the specified Firm and User codes is connected to the system.
                m_WibuKey.CheckBox();

                if (m_WibuKey.LastErrorCode == 0)
                {
                    // A valid WibuBox was found.
                    m_WibuBoxRemoved = false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return m_WibuBoxRemoved;
        }

        /// <summary>
        /// <para>Check whether a WibuBox/U+ device is connected to either a USB or Parallel port and, if so, whether the appropriate slot has been programmed with
        /// the correct Firm and User Codes.</para>
        /// <para>This method is called during PTU start-up, in order to update the WibuBox status label, and whenever the 'Login' menu option is selected, provided
        /// the current project supports WibuBox/U+ hardware.</para>
        /// <para>The dialog box used to enter the Engineering password is only displayed if this method indicates that a valid WibuBox/U+ device was found.</para>
        /// </summary>
        /// <param name="suppressMessageBox">A flag that is used to control <c>MessageBox.Show()</c> reporting. True, if the method is to suppress<c>MessageBox.Show()</c>
        /// reporting; otherwise, false.</param>
        /// <returns>A flag to indicate whether a valid WibuBox device was found. True, if a valid WibuBox was found; otherwise, false.</returns>
        public bool WibuBoxCheckForValidEntry(bool suppressMessageBox)
        {
            m_WibuBoxFound = false;
            m_WibuBoxValid = false;

            try
            {
                // This method should only be called if the WibuBox security device has been initialized by calling the WibuBoxCheckIfRequired() method.
                if (m_WibuBoxIsInitialized == false)
                {
                    throw new InvalidOperationException(Resources.MBTWibuBoxNotInitialized);
                }

                // Ensure that the WibuKey data is refreshed.
                m_WibuKey.PurgeAll();

                // ------------------------------------
                // Scan each WibuBox in each subsystem.
                // ------------------------------------

                // Just access local subsystems i.e. LPT1 and USB.
                m_WibuKey.UsedSubsystems = (int)WkSubsystemType.wkSbLocal;
                m_WibuKey.UsedSubsystem.MoveType = (int)WkSubsystemMoveType.wkSbMvLocal;
                m_WibuKey.UsedSubsystem.MoveFirst();

                // Check for errors.
                if (m_WibuKey.LastErrorCode != 0)
                {
                    throw new InvalidOperationException(m_WibuKey.LastErrorText);
                }

                // The Boolean BOF property (“Begin Of File”) is True only if the navigation location is before the first element of the subsystem list
                // (and no information is available). Otherwise this property is always False.
                if (m_WibuKey.UsedSubsystem.BOF == true)
                {
                    // There were no local WibuBox subsystems found i.e. the computer had no USB or Parallel ports.
                    throw new InvalidOperationException(Resources.MBTWibuKeySubsystemNotFound);
                }

                // Subsystem scan.
                while (m_WibuKey.UsedSubsystem.EOF == false)
                {
                    m_WibuKey.UsedWibuBox.MoveType = (int)WkBoxMoveType.wkBxMvNonEmpty;
                    m_WibuKey.UsedWibuBox.MoveFirst();

                    // Check for errors.
                    if (m_WibuKey.LastErrorCode != 0)
                    {
                        throw new InvalidOperationException(m_WibuKey.LastErrorText);
                    }

                    // The Boolean BOF property (“Begin Of File”) is True only if the navigation location is before the first
                    // element of the WibuBox list (and no information is available). Otherwise this property is always
                    // False.
                    if (m_WibuKey.UsedWibuBox.BOF == true)
                    {
                        // No WibuBoxes were found in the current subsystem, move to the next subsystem.
                        m_WibuKey.UsedSubsystem.MoveNext();
                        continue;
                    }
                    else
                    {
                        // At least one WibuBox was found in the current subsystem.
                        m_WibuBoxFound = true;
                    }

                    // Scan each WibuBox in the current subsystem.
                    while (m_WibuKey.UsedWibuBox.EOF == false)
                    {
                        // Only check the appropriate slot of the WibuBox.
                        m_WibuKey.UsedBoxEntry.Index = m_SlotId;
                        m_WibuKey.UsedBoxEntry.MoveToIndex();

                        // Check for errors.
                        if (m_WibuKey.LastErrorCode != 0)
                        {
                            throw new InvalidOperationException(m_WibuKey.LastErrorText); ;
                        }

                        // -----------------------------------------------------------------------------------
                        // Check whether the Firm and User Codes associated with the specified slot are valid.
                        // -----------------------------------------------------------------------------------
                        /* Note: A valid WibuBox can be either: (a) a WibuBox that has been programmed, in the appropriate slot, with the client Firm and 
                         * User codes that are associated with the current project or (b) a development WibuBox. The slot, Firm code and User code associated 
                         * with the development WibuBox are not published and are only known to the author, for security reasons.
                         * 
                         * For the client WibuBox check, the retrieved WibuBox User code is checked against the value stored in the data dictionary, however, the 
                         * retrieved Firm code is checked against the variable m_FirmCode. This is initialized to the client value that is associated with the current 
                         * project by the WibuBoxCheckIfRequired() method. This method has been adopted for security reasons.
                         * 
                         * For the development WibuBox check, the Security class is used to get the hashcode associated with the retrieved WibuBox Firm and User 
                         * codes and these are checked aginst the hashcodes stored in the Settings file. This approach is adopted for security reasons as it means 
                         * the Firm and User codes associated with the development WibuBox need not be published in the source code.
                         */
                        Security security = new Security();

                        // Include the Firm and User codes associated with the development dongle.
                        if ((m_WibuKey.UsedBoxEntry.FirmCode == m_FirmCode && m_WibuKey.UsedBoxEntry.UserCode == m_UserCode) || 
                            (security.GetHashCode(m_WibuKey.UsedBoxEntry.FirmCode.ToString()) == Settings.Default.HashCodeWkDevFirm  &&
                             security.GetHashCode(m_WibuKey.UsedBoxEntry.UserCode.ToString()) == Settings.Default.HashCodeWkDevUser))
                        {
                            // Yes, the correct Firm and User Codes were found at the specified slot.
                            // Perform an encrytion and decryption to verify that the WibuBox is working correctly.
                            m_WibuKey.AlgorithmVersion = (int)WkAlgorithmVersion.wkAlgVersion2;
                            m_WibuKey.FirmCode = m_WibuKey.UsedBoxEntry.FirmCode;
                            m_WibuKey.UserCode = m_WibuKey.UsedBoxEntry.UserCode;
                            m_WibuKey.TextData = EncryptionData;
                            m_WibuKey.Encrypt();

                            // Check for errors.
                            if (m_WibuKey.LastErrorCode != 0)
                            {
                                // Invalid encryption.
                                throw new InvalidOperationException(m_WibuKey.LastErrorText); ;
                            }

                            m_WibuKey.Decrypt();
                            if (m_WibuKey.TextData != EncryptionData)
                            {
                                // Invalid encryption.
                                throw new InvalidOperationException(Resources.MBTWibuKeyEncryptionInvalid);
                            }
                            else
                            {
                                // Set the flag that identifies whether a valid WibuBox has been found and terminate the scan.
                                m_WibuBoxValid = true;

                                // Check whether the user logged in using a client WibuBox or development WibuBox.
                                if (security.GetHashCode(m_WibuKey.FirmCode.ToString()) == Settings.Default.HashCodeWkDevFirm)
                                {
                                    m_WibuBoxIsDevelopment = true;
                                }
                                break;
                            }
                        }

                        // Check the next WibuBox in the current sub-system.
                        m_WibuKey.UsedWibuBox.MoveNext();
                    }

                    // If a valid WibuBox was found, terminate the scan; otherwise, check the next subsystem.
                    if (m_WibuBoxValid == true)
                    {
                        // A valid WibuBox was found, terminate the scan of the sub-systems.
                        break;
                    }
                    else
                    {
                        // Check the next sub-system.
                        m_WibuKey.UsedSubsystem.MoveNext();
                    }
                }

                // Check whether MessageBox reporting is enabled.
                if (suppressMessageBox == false)
                {
                    if ((m_WibuBoxFound == true) && (m_WibuBoxValid == false))
                    {
                        // A WibuBox was found, however, the appropriate slot was not programmed with the correct Firm and User codes.
                        MessageBox.Show(Resources.MBTWibuBoxFoundButInvalid + CommonConstants.NewPara + Resources.MBTWibuBoxUserMessage, Resources.MBCaptionWarning,
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (m_WibuBoxFound == false)
                    {
                        // A WibuBox was not found.
                        MessageBox.Show(Resources.MBTWibuBoxNotFound + CommonConstants.NewPara + Resources.MBTWibuBoxUserMessage, Resources.MBCaptionWarning,
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return m_WibuBoxValid;
        }
        #endregion --- Methods ---
    }
}