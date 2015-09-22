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
 *  Project:    PTU Application
 * 
 *  File name:  CommunicationApplication.cs
 * 
 *  Revision History
 *  ----------------
 */

#region - [1.0 to 1.5] -
/*
 *  Date        Version Author          Comments
 *  09/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  10/15/10    1.1     K.McD           1.  Added the communication methods used in the main PTU application i.e.: GetTimeDate(), setTimeDate(),
 *                                          SetCarID() and ScanPort().
 *                                      2.  Now inherits from the CommunicationParent class.
 * 
 *  02/14/11    1.2     K.McD           1.  Removed unused constructors.
 *  
 *  07/20/11    1.3     K.McD           1.  Modified the signature of the constructor to use the ICommunicationParent interface.
 *  
 *  07/24/11    1.4     K.McD           1.  Modified each method to use the standard pattern, shown in the CommunicationParent class, in order to
 *                                          improve error handling in the event that communications to the target hardware is lost.
 *                                          
 *  02/27/14    1.5     K.McD           1.  Changed the parameter modifiers from 'ref' to 'out' in the PTUDLL32.GetTimeDate() call in the GetTimeDate()
 *                                          method.
 */
#endregion - [1.0 to 1.5] -

#region - [1.6] -
/*                                                                                  
 *  03/22/15    1.6     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                          4800010525-CU2/19.03.2015.
 *  
 *                                          1.  Changes outlined in the email to Mark Smorul on 30th May 2014 – PTUDLL32 modifications 
 *                                              to support both 32 and 64 bit architecture.
 *                                              
 *                                          2.  Implement changes to allow the PTU to handle both 2 and 4 character date codes.
 *                                          
 *                                      Modifications     
 *                                      1.  Modified each of the methods to check that the function delegate has been initialized prior to its use.
 *                                          
 *                                      2.  Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted, 
 *                                          the method was modified to a check that the Mutex has been initialized prior to its use.
 *                                          
 *                                      3.  Replaced all calls to the methods within PTUDLL32.dll with calls via function delegates. This allows 
 *                                          support for both 32 and 64 bit systems.
 *                                          
 *                                      8.  Where a method uses a 'Mutex'to ensure that communication with the target hardware is not interrupted, 
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
 *                                              throw new CommunicationException(" ... ", errorCode);
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
 *                                          
 *                                      9.  Initialized the local errorCode variable to CommunicationError.UnknownError value for each of the
 *                                          communication methods within the class.
 *                                          
 *                                      10. Added the ConvertYearTo4DigitFormat() and ConvertYearTo2DigitFormat() methods plus their associated constants.
 */
#endregion - [1.6] -

#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Bombardier.PTU.Properties;
using Common;
using Common.Communication;
using Common.Configuration;

namespace Bombardier.PTU.Communication
{
    /// <summary>
    /// Class to manage communication with the target hardare with respect to the main PTU application.
    /// </summary>
    public class CommunicationApplication : CommunicationParent, ICommunicationApplication
    {
        #region --- Constants ---
        /// <summary>
        /// <para>The boundary year value for 2 digit year codes.</para><para>If the 2 digit year code returned from the VCU is within the supported range
        /// (00 to 99) and is above or equal to the value of <c>YearBoundary</c> then the 4 digit year code is deemed to be associated with the 20th Century.</para>
        /// <para>If the year code is below <c>YearBoundary</c> then the 2 digit year code is associated with the 21st Century. This ensures that the PTU displays
        /// the correct date between 1970 - 2069. Value: 70.</para>
        /// </summary>
        private const int YearBoundary = 70;

        /// <summary>
        /// The value to be added to the 2 digit year code if the year is associated with the 20th Century. Value: 1900.
        /// </summary>
        private const int Century20th = 1900;

        /// <summary>
        /// The value to be added to the 2 digit year code if the year is associated with the 21st Century. Value: 2000.
        /// </summary>
        private const int Century21st = 2000;

        /// <summary>
        /// The maximum supported value for 2 digit year codes. Value: 99.
        /// </summary>
        private const int YearCode2DigitMax = 99;

        /// <summary>
        /// The minimum supported value for 2 digit year codes. Value: 0.
        /// </summary>
        private const int YearCode2DigitMin = 0;

        /// <summary>
        /// The maximum supported 4 digit year code on a 2 digit year code VCU. Value: 2069.
        /// </summary>
        private const int YearCode4DigitMax = 2069;

        /// <summary>
        /// The minimum supported 4 digit year code on a 2 digit year code VCU. Value: 1970.
        /// </summary>
        private const int YearCode4DigitMin = 1970;

        /// <summary>
        /// <para>The value to represent that the SetTimeDate() and GetTimeDate() use4DigitYearCode flag parameter is True.</para><para>This parameter specifies whether
        /// the Vehicle Control Unit uses a 2 or 4 digit year code. True, if it uses a 4 digit year code; otherwise, false. Value: 0x01.</para>
        /// </summary>
        protected const short Use4DigitYearCodeTrue = 0x01;

        /// <summary>
        /// <para>The value to represent that the SetTimeDate() and GetTimeDate() use4DigitYearCode flag parameter is False.</para><para>This parameter\
        /// specifies whether the Vehicle Control Unit uses a 2 or 4 digit year code. True, if it uses a 4 digit year code; otherwise, false. Value: 0x00.</para>
        /// </summary>
        protected const short Use4DigitYearCodeFalse = 0x00;
        #endregion --- Constants ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class.
        /// </summary>
        public CommunicationApplication() : base()
        {
        }

        /// <summary>
        /// Initialize a new instance of the class and set the properties and member variables to those values associated with the specified
        /// communication interface.
        /// </summary>
        /// <param name="communicationInterface">Reference to the communication interface containing the properties and member variables that are to be
        /// used to initialize the class.</param>
        public CommunicationApplication(ICommunicationParent communicationInterface)
            : base(communicationInterface)
        {
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Get the time and date from the target hardware.
        /// </summary>
        /// <param name="use4DigitYearCode">A flag that specifies whether the Vehicle Control Unit uses a 2 or 4 digit year code. True, if it
        /// uses a 4 digit year code; otherwise, false.</param>
        /// <param name="dateTime">The time and date as a .NET <c>DateTime</c> object.</param>
        public void GetTimeDate(bool use4DigitYearCode, out DateTime dateTime)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_GetTimeDate != null, "CommunicationApplication.GetTimeDate() - [m_GetTimeDate != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationApplication.GetTimeDate() - [m_MutexCommuncationInterface != null]");

            // Set the use4DigitYearCodeAsShort parameter according to whether the VCU uses 2 or 4 digit year code. 
            short use4DigitYearCodeAsShort = (use4DigitYearCode == true) ? Use4DigitYearCodeTrue : Use4DigitYearCodeFalse;

            short year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;
            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_GetTimeDate(use4DigitYearCodeAsShort, out year, out month, out day, out hour, out minute, out second);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMTimeDateGetFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMTimeDateGetFailed, errorCode);
            }

            // If the VCU uses 2 digit year codes the year value must be converted to a 4 digit code.
            if (use4DigitYearCode == false)
            {
                year = ConvertYearTo4DigitFormat(year);
            }

            try
            {
                dateTime = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                // Set the value to the value which is used to represent an invalid date an time value.
                dateTime = DateTime.MinValue;
            }
        }

        /// <summary>
        /// Set the time and date on the target hardware.
        /// </summary>
        /// <param name="use4DigitYearCode">A flag that specifies whether the Vehicle Control Unit uses a 2 or 4 digit year code. True, if it
        /// uses a 4 digit year code; otherwise, false.</param>
        /// <param name="dateTime">The time and date as a .NET <c>DateTime</c> object.</param>
        public void SetTimeDate(bool use4DigitYearCode, DateTime dateTime)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_SetTimeDate != null, "CommunicationApplication.SetTimeDate() - [m_SetTimeDate != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationApplication.SetTimeDate() - [m_MutexCommuncationInterface != null]");

            // Set the parameters according to whether the VCU uses 2 or 4 digit year code. 
            short use4DigitYearCodeAsShort = (use4DigitYearCode == true) ? Use4DigitYearCodeTrue : Use4DigitYearCodeFalse;
            short year = (use4DigitYearCode == true) ? (short)dateTime.Year : ConvertYearTo2DigitFormat((short)dateTime.Year);

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_SetTimeDate(use4DigitYearCodeAsShort, year, (short)dateTime.Month, (short)dateTime.Day, (short)dateTime.Hour,
                                                                                   (short)dateTime.Minute, (short)dateTime.Second);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMTimeDateSetFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMTimeDateSetFailed, errorCode);
            }
        }

        /// <summary>
        /// Set the car identifier.
        /// </summary>
        /// <param name="carIdentifier">The car identfier.</param>
        public void SetCarID(string carIdentifier)
        {
            // Check that the function delegate has been initialized.
            Debug.Assert(m_SetCarID != null, "CommunicationApplication.SetCarID() - [m_SetCarID != null]");
            Debug.Assert(m_MutexCommuncationInterface != null, "CommunicationApplication.SetCarID() - [m_MutexCommuncationInterface != null]");

            CommunicationError errorCode = CommunicationError.UnknownError;
            try
            {
                m_MutexCommuncationInterface.WaitOne(DefaultMutexWaitDurationMs, false);
                errorCode = (CommunicationError)m_SetCarID(carIdentifier);
            }
            catch (Exception)
            {
                errorCode = CommunicationError.SystemException;
                throw new CommunicationException(Resources.EMCarIDSetFailed, errorCode);
            }
            finally
            {
                m_MutexCommuncationInterface.ReleaseMutex();
            }

            if (errorCode != CommunicationError.Success)
            {
                throw new CommunicationException(Resources.EMCarIDSetFailed, errorCode);
            }
        }

        /// <summary>
        /// Scan the specified serial communication port to determine if it is connected to a target logic controller. If a target is found
        /// the target configuration information is written to the output parameter <paramref name="targetConfiguration"/>.
        /// </summary>
        /// <param name="communicationSetting">The communication settings that are to be used to communicate with the target.</param>
        /// <param name="targetConfiguration">The target configuration information returned from the target hardware if a target is found.</param>
        /// <returns>A flag to indicate whether a target was found; true, if a target was found, otherwise, false.</returns>
        /// <exception cref="CommunicationException">Thrown if the error code returned from the call to the PTUDLL32.InitCommunication() method is not
        /// CommunicationError.Success.</exception>
        public bool ScanPort(CommunicationSetting_t communicationSetting, out TargetConfiguration_t targetConfiguration)
        {
            // Flag to indicate whether target hardware was found; true, if target was found, otherwise, false.
            bool targetFound = false;

            try
            {
                InitCommunication(communicationSetting);
                GetEmbeddedInformation(out targetConfiguration);
                targetFound = true;
            }
            catch (InvalidOperationException)
            {
                targetConfiguration = new TargetConfiguration_t();
                return targetFound;
            }
            finally
            {
				CloseCommunication(communicationSetting.Protocol);
            }

            return targetFound;
        }

        /// <summary>
        /// <para>Convert a 2 digit year code that is within the supported range to its equivalent 4 digit year code.</para>
        /// <para>Any 2 digit year code that is within the supported range, 00 to 99, and is >= 70 is converted to (1900 + 2 digit year code) and any value that
        /// is less than 70 is converted to (2000 + 2 digit year code) e.g. 00 --> 2000, 69 --> 2069, 70 --> 1970, 99 --> 1999.</para><para>Any 2 digit year code
        /// that is > 99 returns a value of 2069 and any value less than 0 returns a value of 1970.</para>
        /// </summary>
        /// <param name="year">A 2 digit year code that is within the supported range. Valid 2 digit year codes are in the range 00 to 99.</param>
        /// <returns>The 4 digit year code corresponding to the specified 2 digit year code e.g. 00 --> 2000, 69 --> 2069, 70 --> 1970, 99 --> 1999 etc.</returns>
        private short ConvertYearTo4DigitFormat(short year)
        {
            // Check the boundary conditions.
            if (year > YearCode2DigitMax)
            {
                year = YearCode4DigitMax;
            }
            else if (year < YearCode2DigitMin)
            {
                year = YearCode4DigitMin;
            }
            else
            {
                if (year >= YearBoundary)
                {
                    year += Century20th;
                }
                else
                {
                    year += Century21st;
                }
            }

            return year;
        }

        /// <summary>
        /// <para>Convert a 4 digit year code that is within the supported range to its equivalent 2 digit year code.</para>
        /// <para>Any 4 digit year code that is within the supported range, 1970 to 2069, but less than 2000 is converted to (4 digit year code - 1900) and any value that
        /// is >= 2000 is converted to (4 digit year code - 2000) e.g. 1970 --> 70, 1999 --> 99, 2000 --> 00, 2069 --> 69.</para><para>Any 4 digit year code that is
        /// outside the supported range is converted to <c>YearBoundary</c> i.e. 70.</para> 
        /// </summary>
        /// <param name="year">A 4 digit year code that is within the supported range. Supported 4 digit year codes are in the range 1970 to 2069.</param>
        /// <returns>The 2 digit year code corresponding to the specified 4 digit year code e.g. 1970 --> 70, 1999 --> 99, 2000 --> 00, 2069 --> 69.</returns>
        private short ConvertYearTo2DigitFormat(short year)
        {
            // Check the boundary conditions.
            if ((year > YearCode4DigitMax) || (year < YearCode4DigitMin))
            {
                year = YearBoundary;
            }
            else
            {
                if (year < Century21st)
                {
                    year -= Century20th;
                }
                else
                {
                    year -= Century21st;
                }
            }

            return year;
        }
        #endregion --- Methods ---
    }
}
