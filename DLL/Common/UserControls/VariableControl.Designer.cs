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
 *  File name:  VariableControl.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/02/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  08/05/13    1.1     K.McD           1.  For all Label controls that make up the VariableControl user control, changed the TextAlign property from MiddleLeft/MiddleRight to 
 *                                          TopLeft/TopRight, as appropriate, and modified the Padding property to 0,5,0,0. This prevents misalignment of the display
 *                                          when the width of the text string associated with the label exceeds the width of the label.
 *
 */
#endregion --- Revision History ---

using System;
using System.Windows.Forms;

namespace Common.UserControls
{
    partial class VariableControl : UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region --- Disposal ---
        /// <summary>
        /// Destructor / Finalizer. Because Dispose() calls the GC.SuppressFinalize() method, this method is called by the garbage collection process only
        /// if the consumer of the object doesn't call the Dispose() method, as it should.
        /// </summary>
        ~VariableControl()
        {
            Dispose(false);
        }

        /// <summary>
        /// Public implementation of the IDisposable.Dispose method, called by the consumer of the object in order to free unmanaged resources
        /// deterministically.
        /// </summary>
        public new void Dispose()
        {
            // Call the protected Dispose overload and pass a value of 'true' to indicate that the Dispose is being called by consumer code, not
            // by the garbage collector.
            Dispose(true);

            // Because the Dispose method performs all necessary cleanup, ensure the garbage collector does not call the class destructor.
            GC.SuppressFinalize(this);
        }

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
                }
            }
        }
        #endregion --- Disposal ---

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_LabelNameField = new System.Windows.Forms.Label();
            this.m_LabelValueField = new System.Windows.Forms.Label();
            this.m_LabelUnitsField = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_LabelNameField
            // 
            this.m_LabelNameField.AutoEllipsis = true;
            this.m_LabelNameField.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelNameField.Location = new System.Drawing.Point(0, 0);
            this.m_LabelNameField.Margin = new System.Windows.Forms.Padding(0);
            this.m_LabelNameField.Name = "m_LabelNameField";
            this.m_LabelNameField.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.m_LabelNameField.Size = new System.Drawing.Size(200, 23);
            this.m_LabelNameField.TabIndex = 0;
            this.m_LabelNameField.Text = "<VariableName> Field";
            this.m_LabelNameField.Click += new System.EventHandler(this.WatchControl_Click);
            this.m_LabelNameField.Leave += new System.EventHandler(this.WatchControl_Leave);
            // 
            // m_LabelValueField
            // 
            this.m_LabelValueField.AutoEllipsis = true;
            this.m_LabelValueField.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelValueField.Location = new System.Drawing.Point(200, 0);
            this.m_LabelValueField.Margin = new System.Windows.Forms.Padding(0);
            this.m_LabelValueField.Name = "m_LabelValueField";
            this.m_LabelValueField.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.m_LabelValueField.Size = new System.Drawing.Size(100, 23);
            this.m_LabelValueField.TabIndex = 0;
            this.m_LabelValueField.Text = "<Value> Field";
            this.m_LabelValueField.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.m_LabelValueField.Click += new System.EventHandler(this.WatchControl_Click);
            this.m_LabelValueField.Leave += new System.EventHandler(this.WatchControl_Leave);
            // 
            // m_LabelUnitsField
            // 
            this.m_LabelUnitsField.AutoEllipsis = true;
            this.m_LabelUnitsField.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_LabelUnitsField.Location = new System.Drawing.Point(300, 0);
            this.m_LabelUnitsField.Margin = new System.Windows.Forms.Padding(0);
            this.m_LabelUnitsField.Name = "m_LabelUnitsField";
            this.m_LabelUnitsField.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.m_LabelUnitsField.Size = new System.Drawing.Size(100, 23);
            this.m_LabelUnitsField.TabIndex = 0;
            this.m_LabelUnitsField.Text = "<Units> Field";
            this.m_LabelUnitsField.Click += new System.EventHandler(this.WatchControl_Click);
            this.m_LabelUnitsField.Leave += new System.EventHandler(this.WatchControl_Leave);
            // 
            // VariableControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.m_LabelUnitsField);
            this.Controls.Add(this.m_LabelValueField);
            this.Controls.Add(this.m_LabelNameField);
            this.Margin = new System.Windows.Forms.Padding(5, 1, 5, 1);
            this.Name = "VariableControl";
            this.Size = new System.Drawing.Size(400, 23);
            this.BackColorChanged += new System.EventHandler(this.WatchControl_BackColorChanged);
            this.ForeColorChanged += new System.EventHandler(this.WatchControl_ForeColorChanged);
            this.Click += new System.EventHandler(this.WatchControl_Click);
            this.GotFocus += new System.EventHandler(this.WatchControl_GotFocus);
            this.Leave += new System.EventHandler(this.WatchControl_Leave);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The label corresponding to the variable name field.
        /// </summary>
        protected Label m_LabelNameField;

        /// <summary>
        /// The label corresponding to the value field.
        /// </summary>
        protected Label m_LabelValueField;

        /// <summary>
        /// The label corresponding to the units field.
        /// </summary>
        protected Label m_LabelUnitsField;
    }
}
