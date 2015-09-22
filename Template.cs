#region --- Revision History ---
/*
 *		 1		   2         3         4         5         6         7         8         9         10        11        12        13        14        15        16        1
 *  56789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|123456789|
 *  
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    <project>
 * 
 *  File name:  <filename>.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  10/13/10    1.0     K.McDonald      1.  First entry into TortoiseSVN.
 *
 */
#endregion --- Revision History ---

using System;

namespace Bombardier.PTU
{
    /// <summary>
    /// Summary description for Template.
    /// </summary>
    public class Template
    {
        #region --- Enumerators ---
        #endregion --- Enumerators ---

        #region --- Structures ---
        #endregion --- Structures ---

        #region --- Constants ---
        #endregion --- Constants ---

        #region --- Member Variables ---
        #endregion --- Member Variables ---

        #region --- Constructors ---
        
        public Template()
        {
            InitializeComponent();
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Cleanup(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.

                #region --- Windows Form Designer Variables ---
                // Detach the event handler delegates.

                // Set the Windows Form Designer Variables to null.

                #endregion --- Windows Form Designer Variables ---
            }
            catch (Exception)
            {
                // Don't do anything, just ensure that an exception isn't thrown.
            }
            finally
            {
                base.Cleanup(disposing);
            }
        }
        #endregion --- Cleanup ---

        #region --- Delegated Methods ---
        #region - [Form] -
        #endregion - [Form] -

        #endregion --- Delegated Methods ---

        #region --- Methods ---
        #endregion --- Methods ---

        #region --- Properties ---
        #endregion --- Properties ---
    }
}
