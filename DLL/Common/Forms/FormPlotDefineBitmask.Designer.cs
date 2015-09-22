namespace Common.Forms
{
    partial class FormPlotDefineBitmask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlotDefineBitmask));
            this.m_ButtonClearAll = new System.Windows.Forms.Button();
            this.m_ButtonInvert = new System.Windows.Forms.Button();
            this.m_PanelOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_PanelOuter
            // 
            this.m_PanelOuter.Size = new System.Drawing.Size(539, 459);
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.Location = new System.Drawing.Point(299, 470);
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.Location = new System.Drawing.Point(379, 470);
            // 
            // m_ButtonApply
            // 
            this.m_ButtonApply.Location = new System.Drawing.Point(458, 470);
            // 
            // m_GroupBoxBitValues
            // 
            this.m_GroupBoxBitValues.Location = new System.Drawing.Point(10, 10);
            // 
            // m_ButtonClearAll
            // 
            this.m_ButtonClearAll.Location = new System.Drawing.Point(10, 470);
            this.m_ButtonClearAll.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonClearAll.Name = "m_ButtonClearAll";
            this.m_ButtonClearAll.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonClearAll.TabIndex = 5;
            this.m_ButtonClearAll.Text = "&Clear All";
            this.m_ButtonClearAll.UseVisualStyleBackColor = true;
            this.m_ButtonClearAll.Click += new System.EventHandler(this.m_ButtonClearAll_Click);
            // 
            // m_ButtonInvert
            // 
            this.m_ButtonInvert.Location = new System.Drawing.Point(89, 470);
            this.m_ButtonInvert.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.m_ButtonInvert.Name = "m_ButtonInvert";
            this.m_ButtonInvert.Size = new System.Drawing.Size(73, 25);
            this.m_ButtonInvert.TabIndex = 6;
            this.m_ButtonInvert.Text = "&Invert";
            this.m_ButtonInvert.UseVisualStyleBackColor = true;
            this.m_ButtonInvert.Click += new System.EventHandler(this.m_ButtonInvert_Click);
            // 
            // FormPlotDefineBitmask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 506);
            this.Controls.Add(this.m_ButtonInvert);
            this.Controls.Add(this.m_ButtonClearAll);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPlotDefineBitmask";
            this.Text = "FormPlotDefineBitmask";
            this.Controls.SetChildIndex(this.m_ButtonClearAll, 0);
            this.Controls.SetChildIndex(this.m_PanelOuter, 0);
            this.Controls.SetChildIndex(this.m_ButtonCancel, 0);
            this.Controls.SetChildIndex(this.m_ButtonOK, 0);
            this.Controls.SetChildIndex(this.m_ButtonApply, 0);
            this.Controls.SetChildIndex(this.m_ButtonInvert, 0);
            this.m_PanelOuter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_ButtonClearAll;
        private System.Windows.Forms.Button m_ButtonInvert;
    }
}