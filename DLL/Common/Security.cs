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
 *  File name:  Security.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  03/31/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  02/14/11    1.1     K.McD           1.  Added the SecurityLevel enumerator; moved from ISecurity.
 *                                      2.  No longer inherits from ISecurity.
 *                                      3.  Removed the GetSecurityDescription() static method.
 * 
 *  03/18/11    1.2     K.McD           1.  Modified a number of XML tags and comments.
 *                                      2.  Added support for security levels 0 through to 3.
 *                                      3.  Added the SecurityConfiguration_t structure to store the parameters defined in the Security table of the data dictionary.
 *                                      4.  Replaced the DefaultHashCodeDeveloper constant with DefaultHashCodeLevel3 and replaced the HashCodeDeveloper property with 
 *                                          the HashCodeLevel3 property.
 *                                      5.  Added the SecurityLevelBase, SecurityLevelHighest and DescriptionLevel3 properties.
 *                                      6.  Removed the readonly qualifier from a number of member variables.
 *                                      7.  Added the Initialize() static method the initialize the properties to the values defined in the Security table of the  
 *                                          data dictionary.
 *                                      8.  Removed the SetDescriptionsToDefault() method.
 *                                      9.  Renamed the Level property and associated member variable to SecurityLevelCurrent.
 *
 *  02/07/12	1.2.1	Sean.D			1.	Modified the hash codes provided for DefaultHashCodeLevel1, DefaultHashCodeLevel2, and DefaultHashCodeLevel3 to fit new
 *											hashing algorithm.
 *										2.	Removed a comment on DefaultHashCodeLevel3 which was inaccurate.
 *										3.	Changed Initialize() to draw the default hash codes for levels 1 and 2 from the constants rather than the settings file.
 *										4.	Modified GetHashCode(string) to use a modified SHA256 to get the hash code instead of using String.GetHashCode to avoid
 *											an error where 64 bit machines calculate the hash differently.
 *											
 *  02/14/12	1.2.2	Sean.D			1.	Reverting Change 3 above. Apparently the settings file is how we change the passwords from within the application.
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;

using Common.Configuration;
using Common.Properties;

namespace Common
{
    /// <summary>
    /// Defines the security clearance level.
    /// </summary>
    public enum SecurityLevel
    {
        /// <summary>
        /// Level 0
        /// </summary>
        Level0 = 0,

        /// <summary>
        /// Level 1
        /// </summary>
        Level1 = 1,

        /// <summary>
        /// Level 2
        /// </summary>
        Level2 = 2,

        /// <summary>
        /// Level 3
        /// </summary>
        Level3 = 3,

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 4
    }

    /// <summary>
    /// Structure to store the parameters used to configure the Security class.
    /// </summary>
    public struct SecurityConfiguration_t
    {
        #region --- Member Variables ---
        /// <summary>
        /// The description associated with security level 0.
        /// </summary>
        private string m_DescriptionLevel0;

        /// <summary>
        /// The description associated with security level 1.
        /// </summary>
        private string m_DescriptionLevel1;

        /// <summary>
        /// The description associated with security level 2.
        /// </summary>
        private string m_DescriptionLevel2;

        /// <summary>
        /// The description associated with security level 3.
        /// </summary>
        private string m_DescriptionLevel3;

        /// <summary>
        /// The base security level, i.e. the security level that the PTU is set to on startup.
        /// </summary>
        private SecurityLevel m_SecurityLevelBase;

        /// <summary>
        /// The highest security level appropriate to the client.
        /// </summary>
        private SecurityLevel m_SecurityLevelHighest;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the structure.
        /// </summary>
        /// <param name="descriptionLevel0">The description associated with security level 0.</param>
        /// <param name="descriptionLevel1">The description associated with security level 1.</param>
        /// <param name="descriptionLevel2">The description associated with security level 2.</param>
        /// <param name="descriptionLevel3">The description associated with security level 3.</param>
        /// <param name="securityLevelBase">The base security level, i.e. the security level that the PTU is set to on startup.</param>
        /// <param name="securityLevelHighest">The highest security level appropriate to the client.</param>
        public SecurityConfiguration_t(string descriptionLevel0, string descriptionLevel1, string descriptionLevel2, string descriptionLevel3,
                                       SecurityLevel securityLevelBase, SecurityLevel securityLevelHighest)
        {
            m_DescriptionLevel0 = descriptionLevel0;
            m_DescriptionLevel1 = descriptionLevel1;
            m_DescriptionLevel2 = descriptionLevel2;
            m_DescriptionLevel3 = descriptionLevel3;
            m_SecurityLevelBase = securityLevelBase;
            m_SecurityLevelHighest = securityLevelHighest;
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the base security level, i.e. the security level that the PTU is set to on startup.
        /// </summary>
        public SecurityLevel SecurityLevelBase
        {
            get { return m_SecurityLevelBase; }
            set { m_SecurityLevelBase = value; }
        }

        /// <summary>
        /// Gets or sets the highest security level appropriate to the client.
        /// </summary>
        public SecurityLevel SecurityLevelHighest
        {
            get { return m_SecurityLevelHighest; }
            set { m_SecurityLevelHighest = value; }
        }

        /// <summary>
        /// Get or sets the description associated with security level 0.
        /// </summary>
        public string DescriptionLevel0
        {
            get { return m_DescriptionLevel0; }
            set { m_DescriptionLevel0 = value; }
        }

        /// <summary>
        /// Get or sets the description associated with security level 1.
        /// </summary>
        public string DescriptionLevel1
        {
            get { return m_DescriptionLevel1; }
            set { m_DescriptionLevel1 = value; }
        }

        /// <summary>
        /// Get or sets the description associated with security level 2.
        /// </summary>
        public string DescriptionLevel2
        {
            get { return m_DescriptionLevel2; }
            set { m_DescriptionLevel2 = value; }
        }

        /// <summary>
        /// Get or sets the description associated with security level 3.
        /// </summary>
        public string DescriptionLevel3
        {
            get { return m_DescriptionLevel3; }
            set { m_DescriptionLevel3 = value; }
        }
        #endregion --- Properties ---
    }

    /// <summary>
    /// Class to manage the security permissions.
    /// </summary>
    public class Security
    {
        #region --- Constants ---
        /// <summary>
        /// The default password hash code associated with security level 0, not used.
        /// </summary>
        private const long DefaultHashCodeLevel0 = 0;

        /// <summary>
        /// The default password hash code associated with security level 1.
        /// </summary>
		private const long DefaultHashCodeLevel1 = 1793506752712787170;

        /// <summary>
        /// The default password hash code associated with security level 2.
        /// </summary>
		private const long DefaultHashCodeLevel2 = 1232229187721023276;

        /// <summary>
        /// The default password hash code associated with security level 3.
        /// </summary>
		private const long DefaultHashCodeLevel3 = 8374202343715184881;
        #endregion --- Constants ---

        #region --- Member Variables
        /// <summary>
        /// The base security level, i.e. the security level that the PTU is set to on startup.
        /// </summary>
        private static SecurityLevel m_SecurityLevelBase;

        /// <summary>
        /// The highest security level appropriate to the client.
        /// </summary>
        private static SecurityLevel m_SecurityLevelHighest;

        /// <summary>
        /// The default description associated with the security level 0.
        /// </summary>
        private static string m_DefaultDescriptionLevel0;

        /// <summary>
        /// The default description associated with the security level 1.
        /// </summary>
        private static string m_DefaultDescriptionLevel1;

        /// <summary>
        /// The default description associated with the level security 2.
        /// </summary>
        private static string m_DefaultDescriptionLevel2;

        /// <summary>
        /// The default description associated with the level security 3.
        /// </summary>
        private static string m_DefaultDescriptionLevel3;

        /// <summary>
        /// An array containing the default password hash codes for each security level.
        /// </summary>
        private readonly long[] m_DefaultHashCodeArray = { DefaultHashCodeLevel0, DefaultHashCodeLevel1, DefaultHashCodeLevel2, DefaultHashCodeLevel3 };

        /// <summary>
        /// An array containing the password hash codes for each security level.
        /// </summary>
        private static long[] m_HashCodeArray = { DefaultHashCodeLevel0, DefaultHashCodeLevel1, DefaultHashCodeLevel2, DefaultHashCodeLevel3 };

        /// <summary>
        /// An array containing the default descriptions for each security level.
        /// </summary>
        private static string[] m_DefaultDescriptionArray;

        /// <summary>
        /// An array containing the descriptions for each security level.
        /// </summary>
        private static string[] m_DescriptionArray;

        /// <summary>
        /// The current security level.
        /// </summary>
        private static SecurityLevel m_SecurityLevelCurrent;

        /// <summary>
        /// The description associated with the current security level.
        /// </summary>
        private static string m_Description;

        /// <summary>
        /// The hash code associated with the current security level.
        /// </summary>
        private static long m_HashCode = DefaultHashCodeLevel0;
        #endregion --- Member Variables

        #region --- Constructors ---
        /// <summary>
        /// Static constructor. Initialize the class properties.
        /// </summary>
        static Security()
        {
            Initialize();
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Initialize the class properties using the security configuration parameters defined in the data dictionary.
        /// </summary>
        public static void Initialize()
        {
            m_HashCodeArray[0] = DefaultHashCodeLevel0;
			m_HashCodeArray[1] = Settings.Default.HashCodeLevel1;
			m_HashCodeArray[2] = Settings.Default.HashCodeLevel2;
			m_HashCodeArray[3] = DefaultHashCodeLevel3;

            m_DefaultDescriptionLevel0 = Parameter.SecurityConfiguration.DescriptionLevel0;
            m_DefaultDescriptionLevel1 = Parameter.SecurityConfiguration.DescriptionLevel1;
            m_DefaultDescriptionLevel2 = Parameter.SecurityConfiguration.DescriptionLevel2;
            m_DefaultDescriptionLevel3 = Parameter.SecurityConfiguration.DescriptionLevel3;

            m_DefaultDescriptionArray = new string[] { m_DefaultDescriptionLevel0, m_DefaultDescriptionLevel1, m_DefaultDescriptionLevel2, m_DefaultDescriptionLevel3 };
            m_DescriptionArray = new string[] { m_DefaultDescriptionLevel0, m_DefaultDescriptionLevel1, m_DefaultDescriptionLevel2, m_DefaultDescriptionLevel3 };

            m_SecurityLevelCurrent = Parameter.SecurityConfiguration.SecurityLevelBase;
            m_Description = m_DescriptionArray[(short)m_SecurityLevelCurrent];
            m_HashCode = m_HashCodeArray[(short)m_SecurityLevelCurrent];

            m_SecurityLevelBase = Parameter.SecurityConfiguration.SecurityLevelBase;
            m_SecurityLevelHighest = Parameter.SecurityConfiguration.SecurityLevelHighest;
        }

        /// <summary>
        /// Get the description corresponding to the specified security level.
        /// </summary>
        /// <param name="securityLevel">The security level for which the description is required.</param>
        /// <returns>The security description corresponding to the specified security level.</returns>
        public string GetSecurityDescription(SecurityLevel securityLevel)
        {
            // Get the description associated with the workset security level.
            string securityLevelDescription;
            switch (securityLevel)
            {
                case SecurityLevel.Level0:
                    securityLevelDescription = DescriptionLevel0;
                    break;
                case SecurityLevel.Level1:
                    securityLevelDescription = DescriptionLevel1;
                    break;
                case SecurityLevel.Level2:
                    securityLevelDescription = DescriptionLevel2;
                    break;
                case SecurityLevel.Level3:
                    securityLevelDescription = DescriptionLevel3;
                    break;
                default:
                    securityLevelDescription = string.Empty;
                    break;
            }
            return securityLevelDescription;
        }

        /// <summary>
        /// Sets the description associated with each security level to the specified values.
        /// </summary>
        /// <param name="descriptionLevel0">The description associated with security level 0.</param>
        /// <param name="descriptionLevel1">The description associated with security level 1.</param>
        /// <param name="descriptionLevel2">The description associated with security level 2.</param>
        /// <param name="descriptionLevel3">The description associated with security level 2.</param>
        public virtual void SetDescriptions(string descriptionLevel0, string descriptionLevel1, string descriptionLevel2, string descriptionLevel3)
        {
            m_DescriptionArray[0] = descriptionLevel0;
            m_DescriptionArray[1] = descriptionLevel1;
            m_DescriptionArray[2] = descriptionLevel2;
            m_DescriptionArray[3] = descriptionLevel3;
        }

        /// <summary>
        /// Sets the current security clearance level to the specified value.
        /// </summary>
        /// <param name="securityLevel">The new security level.</param>
        public virtual void SetLevel(SecurityLevel securityLevel)
        {
            m_SecurityLevelCurrent = securityLevel;

            byte level = (byte)m_SecurityLevelCurrent;
            m_Description = m_DescriptionArray[level];
            m_HashCode = m_HashCodeArray[level];
        }

        /// <summary>
        /// Sets the password hash code for the specified security level to the specified value and updates the application settings.
        /// </summary>
        /// <param name="securityLevel">The security level for which the password hash code is to be set.</param>
        /// <param name="newHashCode">The new password hash code.</param>
        public virtual void SetHashCode(SecurityLevel securityLevel, long newHashCode)
        {
            byte level = (byte)securityLevel;
            m_HashCodeArray[level] = newHashCode;

            SaveHashCode(securityLevel, newHashCode);
        }

        /// <summary>
        /// Sets the password hash code for the specified security level to the default value and updates the application settings.
        /// </summary>
        /// <param name="securityLevel">The security level for which the password hash code is to be reset.</param>
        public virtual void SetHashCodeToDefault(SecurityLevel securityLevel)
        {
            byte level = (byte)securityLevel;
            m_HashCodeArray[level] = m_DefaultHashCodeArray[level];

            SaveHashCode(securityLevel, m_HashCodeArray[level]);
        }

        /// <summary>
        /// Saves the hash code associated with the specified security level to the user settings.
        /// </summary>
        /// <param name="securityLevel">The security level associated with the hash code.</param>
        /// <param name="hashCode">The password hash code.</param>
        public virtual void SaveHashCode(SecurityLevel securityLevel, long hashCode)
        {
            // Update the settings.
            switch (securityLevel)
            {
                case SecurityLevel.Level0:
                    break;
                case SecurityLevel.Level1:
                    Settings.Default.HashCodeLevel1 = hashCode;
                    break;
                case SecurityLevel.Level2:
                    Settings.Default.HashCodeLevel2 = hashCode;
                    break;
                case SecurityLevel.Level3:
                    Settings.Default.HashCodeLevel3 = hashCode;
                    break;
                default:
                    break;
            }
            Settings.Default.Save();
        }

        /// <summary>
        /// Gets the hash code corresponding to the specified password string.
        /// </summary>
        /// <remarks>This is used during development to determine the hashcode associated with the desired default password.</remarks>
        /// <param name="password">The password string for which the hash code is required.</param>
        /// <returns>The password hash code.</returns>
        public long GetHashCode(string password)
        {
			Int64 hashCode = 0;
			if (!string.IsNullOrEmpty(password))
			{
				//Unicode Encode Covering all characterset
				byte[] byteContents = System.Text.Encoding.Unicode.GetBytes(password);
				System.Security.Cryptography.SHA256 hash =
					new System.Security.Cryptography.SHA256Managed();
				byte[] hashText = hash.ComputeHash(byteContents);
				//32Byte hashText separate
				//hashCodeStart = 0~7  8Byte
				//hashCodeMedium = 8~23  8Byte
				//hashCodeEnd = 24~31  8Byte
				//and Fold
				Int64 hashCodeStart = BitConverter.ToInt64(hashText, 0);
				Int64 hashCodeMedium = BitConverter.ToInt64(hashText, 8);
				Int64 hashCodeEnd = BitConverter.ToInt64(hashText, 24);
				hashCode = hashCodeStart ^ hashCodeMedium ^ hashCodeEnd;
			}

			return hashCode;
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the base security level, i.e. the security level that the PTU is set to on startup.
        /// </summary>
        public SecurityLevel SecurityLevelBase
        {
            get { return m_SecurityLevelBase; }
            set { m_SecurityLevelBase = value; }
        }

        /// <summary>
        /// Gets or sets the highest security level appropriate to the client.
        /// </summary>
        public SecurityLevel SecurityLevelHighest
        {
            get { return m_SecurityLevelHighest; }
            set { m_SecurityLevelHighest = value; }
        }

        /// <summary>
        /// Gets or sets the description associated with security level 0.
        /// </summary>
        public string DescriptionLevel0
        {
            get { return m_DescriptionArray[0]; }
            set { m_DescriptionArray[0] = value; }
        }

        /// <summary>
        /// Gets or sets the description associated with security level 1.
        /// </summary>
        public string DescriptionLevel1
        {
            get { return m_DescriptionArray[1]; }
            set { m_DescriptionArray[1] = value; }
        }

        /// <summary>
        /// Gets or sets the description associated with security level 2.
        /// </summary>
        public string DescriptionLevel2
        {
            get { return m_DescriptionArray[2]; }
            set { m_DescriptionArray[2] = value; }
        }

        /// <summary>
        /// Gets or sets the description associated with security level 3.
        /// </summary>
        public string DescriptionLevel3
        {
            get { return m_DescriptionArray[3]; }
            set { m_DescriptionArray[3] = value; }
        }

        /// <summary>
        /// Gets or sets the password hash code associated with security level 0.
        /// </summary>
        public long HashCodeLevel0
        {
            get { return m_HashCodeArray[0]; }
            set { SetHashCode(SecurityLevel.Level0, value); }
        }

        /// <summary>
        /// Gets or sets the password hash code associated with security level 1.
        /// </summary>
        public long HashCodeLevel1
        {
            get { return m_HashCodeArray[1]; }
            set { SetHashCode(SecurityLevel.Level1, value); }
        }

        /// <summary>
        /// Gets or sets the password hash code associated with security level 2.
        /// </summary>
        public long HashCodeLevel2
        {
            get { return m_HashCodeArray[2]; }
            set { SetHashCode(SecurityLevel.Level2, value); }
        }

        /// <summary>
        /// Gets or sets the password hash code associated with security level 3.
        /// </summary>
        public long HashCodeLevel3
        {
            get { return m_HashCodeArray[3]; }
            set { SetHashCode(SecurityLevel.Level3, value); }
        }

        /// <summary>
        /// Gets or sets the current security level.
        /// </summary>
        public SecurityLevel SecurityLevelCurrent
        {
            get { return m_SecurityLevelCurrent; }
            set { SetLevel(value); }
        }

        /// <summary>
        /// Gets the description associated with the current security level.
        /// </summary>
        public string Description
        {
            get { return m_Description; }
        }

        /// <summary>
        /// Gets the password hash code associated with the current security level.
        /// </summary>
        public long HashCode
        {
            get { return m_HashCode; }
        }
        #endregion --- Properties ---
    }
}
