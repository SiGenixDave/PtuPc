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
 *  File name:  WorksetItem.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/17/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/17/10    1.1     K.McD           1.  Bug fix - SNCR001.16. Modified to take into account the re-design of the form used to manage worksets so that it is scrollable.
 * 
 *  11/26/10    1.2     K.McD           1.  Added support for the displaying the workset security level.
 * 
 *  12/01/10    1.3     K.McD           1.  Modified the constructor so that the security level description is obtained by calling the GetSecurityDescription() method of
 *                                          the Security class.
 *                                          
 *  05/23/11    1.4     K.McD           1.  Added another signature to the constructor to support those DataGridView controls that don't have a column showing 
 *                                          the default workset.
 *                                          
 *                                      2.  Applied the 'Organize Usings/Remove and Sort' context menu.
 *
 */
#endregion --- Revision History ---

using System.Windows.Forms;

namespace Common.Configuration
{
    /// <summary>
    /// Workset <c>ListView</c> item definition.
    /// </summary>
    public class WorksetItem : ListViewItem
    {
        #region --- Member Variables ---
        /// <summary>
        /// The workset associated with the item.
        /// </summary>
        private Workset_t m_Workset;
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Inializes a new instance of the class.
        /// </summary>
        public WorksetItem()
        {
        }

        /// <summary>
        /// Inializes a new instance of the class. Used to create an item that has an empty string for the default workset column text.
        /// </summary>
        /// <param name="workset">The workset associated with the item.</param>
        public WorksetItem(Workset_t workset)
        {
            m_Workset = workset;
            base.Text = m_Workset.Name;

            // Default column text string is empty.
            base.SubItems.Add(string.Empty);
        }

        /// <summary>
        /// Inializes a new instance of the class. Used to create an item with the specified default workset column text.
        /// </summary>
        /// <param name="workset">The workset associated with the item.</param>
        /// <param name="defaultText">The text to be displayed in the default workset column.</param>
        public WorksetItem(Workset_t workset, string defaultText)
        {
            m_Workset = workset;
            base.Text = m_Workset.Name;

            // Default column text string defined by defaultText.
            base.SubItems.Add(defaultText);
        }

        /// <summary>
        /// Inializes a new instance of the class. Used to create an item that shows a tick icon next to the workset name, depending upon 
        /// the state of the showTick parameter. True, to display a tick icon; otherwise, false. The tick is used to show that the workset 
        /// is the default workset.
        /// </summary>
        public WorksetItem(Workset_t workset, bool showTick)
        {
            m_Workset = workset;
            base.Text = m_Workset.Name;

            base.ImageIndex = showTick ? 1 : 0;
        }

        /// <summary>
        /// Inializes a new instance of the class. Used to create an item that shows a tick icon next to the workset name, depending upon 
        /// the state of the showTick parameter. True, to display a tick icon; otherwise, false. The tick is used to show that the workset 
        /// is the default workset. Also displays the description corresponding to the specified workset security level.
        /// </summary>
        public WorksetItem(Workset_t workset, bool showTick, SecurityLevel securityLevel)
        {
            m_Workset = workset;
            base.Text = m_Workset.Name;

            base.ImageIndex = showTick ? 1 : 0;

            string securityLevelDescription = new Security().GetSecurityDescription(securityLevel);
            base.SubItems.Add(securityLevelDescription);
        }

        /// <summary>
        /// Inializes a new instance of the class. Used to create an item that shows a tick icon next to the workset name, depending upon 
        /// the state of the showTick parameter. True, to display a tick icon; otherwise, false. The tick is used to show that the workset 
        /// is the default workset. Also displays the description corresponding to the specified workset security level.
        /// </summary>
        public WorksetItem(Workset_t workset, SecurityLevel securityLevel)
        {
            m_Workset = workset;
            base.Text = m_Workset.Name;

            string securityLevelDescription = new Security().GetSecurityDescription(securityLevel);
            base.SubItems.Add(securityLevelDescription);
        }
        #endregion --- Constructors ---

        #region --- Properties ---
        /// <summary>
        /// The workset associated with the item.
        /// </summary>
        public Workset_t Workset
        {
            get { return m_Workset; }
        }
        #endregion --- Properties ---
    }
}