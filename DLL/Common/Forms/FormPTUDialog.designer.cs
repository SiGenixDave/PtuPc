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
 *  File name:  FormPTUDialog.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McDonald      First Release.
 * 
 *  08/25/10    1.1     K.McD           1.  Removed registration of the Activated event.
 * 
 *  08/26/10    1.2     K.McD           1.  Added registration of the FormClosing event.
 * 
 *  04/07/11    1.3     K.McD           1.  Modified the AutoScaleMode property to Inherit.
 *
 */
#endregion --- Revision History ---

using System;

using Common.Communication; 

namespace Common.Forms
{
    partial class FormPTUDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region --- Disposal ---
        /// <summary>
        /// Disposes of the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            lock (this)
            {
                if (m_IsDisposed == false)
                {
                    Cleanup(disposing);
                    m_IsDisposed = true;

                    base.Dispose(disposing);

                    // Because the Dispose method performs all necessary cleanup, ensure the garbage collector does not call the class destructor.
                    GC.SuppressFinalize(this);
                }
            }
        }
        #endregion --- Disposal ---

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormPTUDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(339, 255);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPTUDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Shown += new System.EventHandler(this.FormPTUDialog_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPTUDialog_FormClosing);
            this.ResumeLayout(false);

        }
        #endregion
    }
}