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
 *  File name:  FormPTU.Designer.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  04/27/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/26/10    1.1     K.McD           1.  Added the ToolStripComboBox and ToolStripLabel controls to the ToolStrip.
 * 
 *  10/06/10    1.2     K.McD           1.  Renamed the [Esc-Exit] function key [Esc-Home].
 * 
 *  11/16/10    1.3     K.McD           1.  Added support for the Form_Resize() event.
 *  
 *  07/25/11    1.4     K.McD           1.  Changed the modifier of m_ToolStripFunctionKeysPTU to protected.
 *  
 *  08/10/11    1.5     K.McD           1.  Adjusted the DropDownHeight property of the ToolStripComboBox control.
 *                                      2.  Changed the BackColor property of one or more controls to Transparent.
 *                                      3.  Added a BackgroundImage to the Panel control used to display information.
 *                                      4.  Minor adjustments to the Size and Location properties of one or more controls.
 *                                      
 *  08/05/13    1.6     K.McD           1.  Modified the text associated with the escape key from 'Esc - Home' to 'Esc' to prevent 
 *                                          overflow when using larger fonts.
 *                                          
 *  03/17/15    1.7     K.McD           References
 *                                      1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order TBD.
 *  
 *                                          1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                              Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *                                                  
 *                                                  1.  MOC-0171-06. All references to fault logs, including menu options and directory 
 *                                                      names to be replaced by 'data streams' for all projects.
 *                                                  
 *                                                  2.  MOC-0171-18. The ‘Time’ legend will be reserved for system information time, the legend 
 *                                                      ‘PC Time’ will be used when displaying the PC time.
 *                                                  
 *                                                  3.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context
 *                                                      menu options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                                  
 *                                                  4.  MOC-0171-30. The ‘F4 – Rec.’ Function key will toggle between the industry standard icons
 *                                                      representing stop and start recording.
 *                                                  
 *                                          2.  Updated Resources with a number of premium 24 x 24 images purchased from Iconfinder.
 *                                          
 *                                      Modifications
 *                                      1.   The ImageAlign and ImageScaling properties of all function keys were modified to use: 
 *                                           System.Drawing.ContentAlignment.TopCenter and System.Windows.Forms.ToolStripItemImageScaling.None
 *                                           respectively. Ref.: 1.1.4, 1.2.
 *                                           
 *                                      2.  The ToolTipText properties of the Esc key and function keys F1, F2 were modified to:
 *                                          "Esc - [Close Window]", "[Show Software User Manual]" and "[Capture PTU Window]" respectively.
 *                                          Ref.: 1.1.4.
 *                                          
 *                                      3.  The Image property of the Escape key was modified to use the 28 x 28 Help_28 image (.png) instead of
 *                                          the Help icon (Help.ico). Ref.: 1.1.4.
 */

/*
 *  05/13/15    1.8     K.McD           References
 *                                      1.  Hide the ToolTip text for the [First], [Next], [Previous] and [Last] function keys on the FormDataStreamReplay form as
 *                                          they obscures the [Frame No.] information label.
 *                                          
 *                                      Modifications
 *                                      1.  Set the AutoToolTip property of all ToolStripButton controls to false.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;

namespace Common.Forms
{
    partial class FormPTU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPTU));
            this.m_ToolStripFunctionKeysPTU = new System.Windows.Forms.ToolStrip();
            this.m_TSBEsc = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorEsc = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF1 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF2 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF3 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF4 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF5 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF5 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF6 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF6 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF7 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF7 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF8 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF8 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF9 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF9 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF10 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF10 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF11 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF11 = new System.Windows.Forms.ToolStripSeparator();
            this.m_TSBF12 = new System.Windows.Forms.ToolStripButton();
            this.m_TSSeparatorF12 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.m_ToolStripLegendComboBox1 = new System.Windows.Forms.ToolStripLabel();
            this.m_PanelInformation = new System.Windows.Forms.Panel();
            this.m_TableLayoutPanelInformationLabels = new System.Windows.Forms.TableLayoutPanel();
            this.m_Legend2 = new System.Windows.Forms.Label();
            this.m_Label1 = new System.Windows.Forms.Label();
            this.m_Legend1 = new System.Windows.Forms.Label();
            this.m_Legend4 = new System.Windows.Forms.Label();
            this.m_Label2 = new System.Windows.Forms.Label();
            this.m_Legend3 = new System.Windows.Forms.Label();
            this.m_Label3 = new System.Windows.Forms.Label();
            this.m_Label4 = new System.Windows.Forms.Label();
            this.m_Legend5 = new System.Windows.Forms.Label();
            this.m_Label5 = new System.Windows.Forms.Label();
            this.m_Label6 = new System.Windows.Forms.Label();
            this.m_Legend6 = new System.Windows.Forms.Label();
            this.m_TabControl = new System.Windows.Forms.TabControl();
            this.m_TabPage1 = new System.Windows.Forms.TabPage();
            this.m_ToolStripFunctionKeysPTU.SuspendLayout();
            this.m_PanelInformation.SuspendLayout();
            this.m_TableLayoutPanelInformationLabels.SuspendLayout();
            this.m_TabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ToolStripFunctionKeysPTU
            // 
            this.m_ToolStripFunctionKeysPTU.BackColor = System.Drawing.SystemColors.Control;
            this.m_ToolStripFunctionKeysPTU.GripMargin = new System.Windows.Forms.Padding(0);
            this.m_ToolStripFunctionKeysPTU.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_ToolStripFunctionKeysPTU.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_TSBEsc,
            this.m_TSSeparatorEsc,
            this.m_TSBF1,
            this.m_TSSeparatorF1,
            this.m_TSBF2,
            this.m_TSSeparatorF2,
            this.m_TSBF3,
            this.m_TSSeparatorF3,
            this.m_TSBF4,
            this.m_TSSeparatorF4,
            this.m_TSBF5,
            this.m_TSSeparatorF5,
            this.m_TSBF6,
            this.m_TSSeparatorF6,
            this.m_TSBF7,
            this.m_TSSeparatorF7,
            this.m_TSBF8,
            this.m_TSSeparatorF8,
            this.m_TSBF9,
            this.m_TSSeparatorF9,
            this.m_TSBF10,
            this.m_TSSeparatorF10,
            this.m_TSBF11,
            this.m_TSSeparatorF11,
            this.m_TSBF12,
            this.m_TSSeparatorF12,
            this.m_ToolStripComboBox1,
            this.m_ToolStripLegendComboBox1});
            this.m_ToolStripFunctionKeysPTU.Location = new System.Drawing.Point(0, 0);
            this.m_ToolStripFunctionKeysPTU.Name = "m_ToolStripFunctionKeysPTU";
            this.m_ToolStripFunctionKeysPTU.Padding = new System.Windows.Forms.Padding(2);
            this.m_ToolStripFunctionKeysPTU.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.m_ToolStripFunctionKeysPTU.ShowItemToolTips = false;
            this.m_ToolStripFunctionKeysPTU.Size = new System.Drawing.Size(1200, 57);
            this.m_ToolStripFunctionKeysPTU.TabIndex = 0;
            this.m_ToolStripFunctionKeysPTU.Visible = false;
            // 
            // m_TSBEsc
            // 
            this.m_TSBEsc.AutoSize = false;
            this.m_TSBEsc.AutoToolTip = false;
            this.m_TSBEsc.Image = global::Common.Properties.Resources.Home;
            this.m_TSBEsc.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBEsc.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBEsc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBEsc.Name = "m_TSBEsc";
            this.m_TSBEsc.Size = new System.Drawing.Size(70, 50);
            this.m_TSBEsc.Tag = "Esc";
            this.m_TSBEsc.Text = "Esc";
            this.m_TSBEsc.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBEsc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBEsc.ToolTipText = "Esc - [Close Window]";
            this.m_TSBEsc.Click += new System.EventHandler(this.Escape_Click);
            // 
            // m_TSSeparatorEsc
            // 
            this.m_TSSeparatorEsc.Name = "m_TSSeparatorEsc";
            this.m_TSSeparatorEsc.Size = new System.Drawing.Size(6, 53);
            // 
            // m_TSBF1
            // 
            this.m_TSBF1.AutoSize = false;
            this.m_TSBF1.AutoToolTip = false;
            this.m_TSBF1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_TSBF1.Image = global::Common.Properties.Resources.Help;
            this.m_TSBF1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF1.Name = "m_TSBF1";
            this.m_TSBF1.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF1.Tag = "F1";
            this.m_TSBF1.Text = "F1-Help";
            this.m_TSBF1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF1.ToolTipText = "F1 - [Show Software User Manual]";
            this.m_TSBF1.Click += new System.EventHandler(this.F1_Click);
            // 
            // m_TSSeparatorF1
            // 
            this.m_TSSeparatorF1.Name = "m_TSSeparatorF1";
            this.m_TSSeparatorF1.Size = new System.Drawing.Size(6, 53);
            // 
            // m_TSBF2
            // 
            this.m_TSBF2.AutoSize = false;
            this.m_TSBF2.AutoToolTip = false;
            this.m_TSBF2.Image = global::Common.Properties.Resources.Print;
            this.m_TSBF2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF2.Name = "m_TSBF2";
            this.m_TSBF2.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF2.Tag = "F2";
            this.m_TSBF2.Text = "F2-Print";
            this.m_TSBF2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF2.ToolTipText = "F2 - [Capture PTU Window]";
            this.m_TSBF2.Click += new System.EventHandler(this.F2_Click);
            // 
            // m_TSSeparatorF2
            // 
            this.m_TSSeparatorF2.Name = "m_TSSeparatorF2";
            this.m_TSSeparatorF2.Size = new System.Drawing.Size(6, 53);
            // 
            // m_TSBF3
            // 
            this.m_TSBF3.AutoSize = false;
            this.m_TSBF3.AutoToolTip = false;
            this.m_TSBF3.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF3.Image")));
            this.m_TSBF3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF3.Name = "m_TSBF3";
            this.m_TSBF3.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF3.Tag = "F3";
            this.m_TSBF3.Text = "F3";
            this.m_TSBF3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF3.ToolTipText = "F3";
            this.m_TSBF3.Visible = false;
            this.m_TSBF3.Click += new System.EventHandler(this.F3_Click);
            // 
            // m_TSSeparatorF3
            // 
            this.m_TSSeparatorF3.Name = "m_TSSeparatorF3";
            this.m_TSSeparatorF3.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF3.Visible = false;
            // 
            // m_TSBF4
            // 
            this.m_TSBF4.AutoSize = false;
            this.m_TSBF4.AutoToolTip = false;
            this.m_TSBF4.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF4.Image")));
            this.m_TSBF4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF4.Name = "m_TSBF4";
            this.m_TSBF4.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF4.Tag = "F4";
            this.m_TSBF4.Text = "F4";
            this.m_TSBF4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF4.Visible = false;
            this.m_TSBF4.Click += new System.EventHandler(this.F4_Click);
            // 
            // m_TSSeparatorF4
            // 
            this.m_TSSeparatorF4.Name = "m_TSSeparatorF4";
            this.m_TSSeparatorF4.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF4.Visible = false;
            // 
            // m_TSBF5
            // 
            this.m_TSBF5.AutoSize = false;
            this.m_TSBF5.AutoToolTip = false;
            this.m_TSBF5.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF5.Image")));
            this.m_TSBF5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF5.Name = "m_TSBF5";
            this.m_TSBF5.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF5.Tag = "F5";
            this.m_TSBF5.Text = "F5";
            this.m_TSBF5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF5.Visible = false;
            this.m_TSBF5.Click += new System.EventHandler(this.F5_Click);
            // 
            // m_TSSeparatorF5
            // 
            this.m_TSSeparatorF5.Name = "m_TSSeparatorF5";
            this.m_TSSeparatorF5.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF5.Visible = false;
            // 
            // m_TSBF6
            // 
            this.m_TSBF6.AutoSize = false;
            this.m_TSBF6.AutoToolTip = false;
            this.m_TSBF6.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF6.Image")));
            this.m_TSBF6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF6.Name = "m_TSBF6";
            this.m_TSBF6.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF6.Tag = "F6";
            this.m_TSBF6.Text = "F6";
            this.m_TSBF6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF6.Visible = false;
            this.m_TSBF6.Click += new System.EventHandler(this.F6_Click);
            // 
            // m_TSSeparatorF6
            // 
            this.m_TSSeparatorF6.Name = "m_TSSeparatorF6";
            this.m_TSSeparatorF6.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF6.Visible = false;
            // 
            // m_TSBF7
            // 
            this.m_TSBF7.AutoSize = false;
            this.m_TSBF7.AutoToolTip = false;
            this.m_TSBF7.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF7.Image")));
            this.m_TSBF7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF7.Name = "m_TSBF7";
            this.m_TSBF7.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF7.Tag = "F7";
            this.m_TSBF7.Text = "F7";
            this.m_TSBF7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF7.Visible = false;
            this.m_TSBF7.Click += new System.EventHandler(this.F7_Click);
            // 
            // m_TSSeparatorF7
            // 
            this.m_TSSeparatorF7.Name = "m_TSSeparatorF7";
            this.m_TSSeparatorF7.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF7.Visible = false;
            // 
            // m_TSBF8
            // 
            this.m_TSBF8.AutoSize = false;
            this.m_TSBF8.AutoToolTip = false;
            this.m_TSBF8.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF8.Image")));
            this.m_TSBF8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF8.Name = "m_TSBF8";
            this.m_TSBF8.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF8.Tag = "F8";
            this.m_TSBF8.Text = "F8";
            this.m_TSBF8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF8.Visible = false;
            this.m_TSBF8.Click += new System.EventHandler(this.F8_Click);
            // 
            // m_TSSeparatorF8
            // 
            this.m_TSSeparatorF8.Name = "m_TSSeparatorF8";
            this.m_TSSeparatorF8.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF8.Visible = false;
            // 
            // m_TSBF9
            // 
            this.m_TSBF9.AutoSize = false;
            this.m_TSBF9.AutoToolTip = false;
            this.m_TSBF9.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF9.Image")));
            this.m_TSBF9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF9.Name = "m_TSBF9";
            this.m_TSBF9.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF9.Tag = "F9";
            this.m_TSBF9.Text = "F9";
            this.m_TSBF9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF9.Visible = false;
            this.m_TSBF9.Click += new System.EventHandler(this.F9_Click);
            // 
            // m_TSSeparatorF9
            // 
            this.m_TSSeparatorF9.Name = "m_TSSeparatorF9";
            this.m_TSSeparatorF9.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF9.Visible = false;
            // 
            // m_TSBF10
            // 
            this.m_TSBF10.AutoSize = false;
            this.m_TSBF10.AutoToolTip = false;
            this.m_TSBF10.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF10.Image")));
            this.m_TSBF10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF10.Name = "m_TSBF10";
            this.m_TSBF10.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF10.Tag = "F10";
            this.m_TSBF10.Text = "F10";
            this.m_TSBF10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF10.Visible = false;
            this.m_TSBF10.Click += new System.EventHandler(this.F10_Click);
            // 
            // m_TSSeparatorF10
            // 
            this.m_TSSeparatorF10.Name = "m_TSSeparatorF10";
            this.m_TSSeparatorF10.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF10.Visible = false;
            // 
            // m_TSBF11
            // 
            this.m_TSBF11.AutoSize = false;
            this.m_TSBF11.AutoToolTip = false;
            this.m_TSBF11.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF11.Image")));
            this.m_TSBF11.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF11.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF11.Name = "m_TSBF11";
            this.m_TSBF11.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF11.Tag = "F11";
            this.m_TSBF11.Text = "F11";
            this.m_TSBF11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF11.Visible = false;
            this.m_TSBF11.Click += new System.EventHandler(this.F11_Click);
            // 
            // m_TSSeparatorF11
            // 
            this.m_TSSeparatorF11.Name = "m_TSSeparatorF11";
            this.m_TSSeparatorF11.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF11.Visible = false;
            // 
            // m_TSBF12
            // 
            this.m_TSBF12.AutoSize = false;
            this.m_TSBF12.AutoToolTip = false;
            this.m_TSBF12.Image = ((System.Drawing.Image)(resources.GetObject("m_TSBF12.Image")));
            this.m_TSBF12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_TSBF12.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_TSBF12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_TSBF12.Name = "m_TSBF12";
            this.m_TSBF12.Size = new System.Drawing.Size(70, 50);
            this.m_TSBF12.Tag = "F12";
            this.m_TSBF12.Text = "F12";
            this.m_TSBF12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_TSBF12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.m_TSBF12.Visible = false;
            this.m_TSBF12.Click += new System.EventHandler(this.F12_Click);
            // 
            // m_TSSeparatorF12
            // 
            this.m_TSSeparatorF12.Name = "m_TSSeparatorF12";
            this.m_TSSeparatorF12.Size = new System.Drawing.Size(6, 53);
            this.m_TSSeparatorF12.Visible = false;
            // 
            // m_ToolStripComboBox1
            // 
            this.m_ToolStripComboBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_ToolStripComboBox1.DropDownHeight = 460;
            this.m_ToolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ToolStripComboBox1.DropDownWidth = 200;
            this.m_ToolStripComboBox1.IntegralHeight = false;
            this.m_ToolStripComboBox1.Margin = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.m_ToolStripComboBox1.Name = "m_ToolStripComboBox1";
            this.m_ToolStripComboBox1.Size = new System.Drawing.Size(200, 23);
            this.m_ToolStripComboBox1.Visible = false;
            this.m_ToolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.m_ToolStripComboBox1_SelectedIndexChanged);
            // 
            // m_ToolStripLegendComboBox1
            // 
            this.m_ToolStripLegendComboBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_ToolStripLegendComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.m_ToolStripLegendComboBox1.Margin = new System.Windows.Forms.Padding(0);
            this.m_ToolStripLegendComboBox1.Name = "m_ToolStripLegendComboBox1";
            this.m_ToolStripLegendComboBox1.Size = new System.Drawing.Size(114, 15);
            this.m_ToolStripLegendComboBox1.Text = "Legend ComboBox1";
            this.m_ToolStripLegendComboBox1.Visible = false;
            // 
            // m_PanelInformation
            // 
            this.m_PanelInformation.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_PanelInformation.BackgroundImage = global::Common.Properties.Resources.LightMetallic;
            this.m_PanelInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_PanelInformation.Controls.Add(this.m_TableLayoutPanelInformationLabels);
            this.m_PanelInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_PanelInformation.Location = new System.Drawing.Point(0, 57);
            this.m_PanelInformation.Name = "m_PanelInformation";
            this.m_PanelInformation.Size = new System.Drawing.Size(1200, 33);
            this.m_PanelInformation.TabIndex = 0;
            // 
            // m_TableLayoutPanelInformationLabels
            // 
            this.m_TableLayoutPanelInformationLabels.BackColor = System.Drawing.Color.Transparent;
            this.m_TableLayoutPanelInformationLabels.ColumnCount = 12;
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Legend2, 0, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Label1, 0, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Legend1, 0, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Legend4, 6, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Label2, 3, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Legend3, 4, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Label3, 5, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Label4, 7, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Legend5, 8, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Label5, 9, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Label6, 11, 0);
            this.m_TableLayoutPanelInformationLabels.Controls.Add(this.m_Legend6, 10, 0);
            this.m_TableLayoutPanelInformationLabels.Location = new System.Drawing.Point(0, 0);
            this.m_TableLayoutPanelInformationLabels.Name = "m_TableLayoutPanelInformationLabels";
            this.m_TableLayoutPanelInformationLabels.RowCount = 1;
            this.m_TableLayoutPanelInformationLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_TableLayoutPanelInformationLabels.Size = new System.Drawing.Size(907, 31);
            this.m_TableLayoutPanelInformationLabels.TabIndex = 0;
            // 
            // m_Legend2
            // 
            this.m_Legend2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_Legend2.AutoSize = true;
            this.m_Legend2.Location = new System.Drawing.Point(220, 9);
            this.m_Legend2.Name = "m_Legend2";
            this.m_Legend2.Size = new System.Drawing.Size(0, 13);
            this.m_Legend2.TabIndex = 0;
            this.m_Legend2.Visible = false;
            // 
            // m_Label1
            // 
            this.m_Label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_Label1.Location = new System.Drawing.Point(74, 4);
            this.m_Label1.Name = "m_Label1";
            this.m_Label1.Size = new System.Drawing.Size(70, 23);
            this.m_Label1.TabIndex = 0;
            this.m_Label1.Tag = "1";
            this.m_Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_Label1.Visible = false;
            // 
            // m_Legend1
            // 
            this.m_Legend1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_Legend1.AutoSize = true;
            this.m_Legend1.Location = new System.Drawing.Point(68, 9);
            this.m_Legend1.Name = "m_Legend1";
            this.m_Legend1.Size = new System.Drawing.Size(0, 13);
            this.m_Legend1.TabIndex = 0;
            this.m_Legend1.Visible = false;
            // 
            // m_Legend4
            // 
            this.m_Legend4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_Legend4.AutoSize = true;
            this.m_Legend4.Location = new System.Drawing.Point(524, 9);
            this.m_Legend4.Name = "m_Legend4";
            this.m_Legend4.Size = new System.Drawing.Size(0, 13);
            this.m_Legend4.TabIndex = 0;
            this.m_Legend4.Visible = false;
            // 
            // m_Label2
            // 
            this.m_Label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_Label2.Location = new System.Drawing.Point(226, 4);
            this.m_Label2.Name = "m_Label2";
            this.m_Label2.Size = new System.Drawing.Size(70, 23);
            this.m_Label2.TabIndex = 5;
            this.m_Label2.Tag = "2";
            this.m_Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_Label2.Visible = false;
            // 
            // m_Legend3
            // 
            this.m_Legend3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_Legend3.AutoSize = true;
            this.m_Legend3.Location = new System.Drawing.Point(372, 9);
            this.m_Legend3.Name = "m_Legend3";
            this.m_Legend3.Size = new System.Drawing.Size(0, 13);
            this.m_Legend3.TabIndex = 0;
            this.m_Legend3.Visible = false;
            // 
            // m_Label3
            // 
            this.m_Label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_Label3.Location = new System.Drawing.Point(378, 4);
            this.m_Label3.Name = "m_Label3";
            this.m_Label3.Size = new System.Drawing.Size(70, 23);
            this.m_Label3.TabIndex = 0;
            this.m_Label3.Tag = "3";
            this.m_Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_Label3.Visible = false;
            // 
            // m_Label4
            // 
            this.m_Label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_Label4.Location = new System.Drawing.Point(530, 4);
            this.m_Label4.Name = "m_Label4";
            this.m_Label4.Size = new System.Drawing.Size(70, 23);
            this.m_Label4.TabIndex = 0;
            this.m_Label4.Tag = "4";
            this.m_Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_Label4.Visible = false;
            // 
            // m_Legend5
            // 
            this.m_Legend5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_Legend5.AutoSize = true;
            this.m_Legend5.Location = new System.Drawing.Point(676, 9);
            this.m_Legend5.Name = "m_Legend5";
            this.m_Legend5.Size = new System.Drawing.Size(0, 13);
            this.m_Legend5.TabIndex = 0;
            this.m_Legend5.Visible = false;
            // 
            // m_Label5
            // 
            this.m_Label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_Label5.Location = new System.Drawing.Point(682, 4);
            this.m_Label5.Name = "m_Label5";
            this.m_Label5.Size = new System.Drawing.Size(70, 23);
            this.m_Label5.TabIndex = 0;
            this.m_Label5.Tag = "5";
            this.m_Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_Label5.Visible = false;
            // 
            // m_Label6
            // 
            this.m_Label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_Label6.Location = new System.Drawing.Point(834, 4);
            this.m_Label6.Name = "m_Label6";
            this.m_Label6.Size = new System.Drawing.Size(70, 23);
            this.m_Label6.TabIndex = 0;
            this.m_Label6.Tag = "6";
            this.m_Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_Label6.Visible = false;
            // 
            // m_Legend6
            // 
            this.m_Legend6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_Legend6.AutoSize = true;
            this.m_Legend6.Location = new System.Drawing.Point(828, 9);
            this.m_Legend6.Name = "m_Legend6";
            this.m_Legend6.Size = new System.Drawing.Size(0, 13);
            this.m_Legend6.TabIndex = 0;
            this.m_Legend6.Visible = false;
            // 
            // m_TabControl
            // 
            this.m_TabControl.Controls.Add(this.m_TabPage1);
            this.m_TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TabControl.Location = new System.Drawing.Point(0, 90);
            this.m_TabControl.Name = "m_TabControl";
            this.m_TabControl.SelectedIndex = 0;
            this.m_TabControl.Size = new System.Drawing.Size(1200, 610);
            this.m_TabControl.TabIndex = 1;
            // 
            // m_TabPage1
            // 
            this.m_TabPage1.AutoScroll = true;
            this.m_TabPage1.Location = new System.Drawing.Point(4, 22);
            this.m_TabPage1.Name = "m_TabPage1";
            this.m_TabPage1.Size = new System.Drawing.Size(1192, 584);
            this.m_TabPage1.TabIndex = 0;
            this.m_TabPage1.Text = "Tab Page 1";
            this.m_TabPage1.UseVisualStyleBackColor = true;
            // 
            // FormPTU
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.ControlBox = false;
            this.Controls.Add(this.m_TabControl);
            this.Controls.Add(this.m_PanelInformation);
            this.Controls.Add(this.m_ToolStripFunctionKeysPTU);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(3000, 2000);
            this.MinimizeBox = false;
            this.Name = "FormPTU";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FormPTU";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.FormPTU_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPTU_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormPTU_KeyUp);
            this.Resize += new System.EventHandler(this.FormPTU_Resize);
            this.m_ToolStripFunctionKeysPTU.ResumeLayout(false);
            this.m_ToolStripFunctionKeysPTU.PerformLayout();
            this.m_PanelInformation.ResumeLayout(false);
            this.m_TableLayoutPanelInformationLabels.ResumeLayout(false);
            this.m_TableLayoutPanelInformationLabels.PerformLayout();
            this.m_TabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// Reference to the <c>TabControl</c> control.
        /// </summary>
        protected System.Windows.Forms.TabControl m_TabControl;

        /// <summary>
        /// Reference to the <c>Panel</c> containing the information labels.
        /// </summary>
        protected System.Windows.Forms.Panel m_PanelInformation;

        /// <summary>
        /// Reference to the <c>ToolStrip</c> control.
        /// </summary>
        protected System.Windows.Forms.ToolStrip m_ToolStripFunctionKeysPTU;

        /// <summary>
        /// Reference to the only <c>ToolStripComboBox</c> control associated with the <c>ToolStrip</c>.
        /// </summary>
        protected System.Windows.Forms.ToolStripComboBox m_ToolStripComboBox1;

        /// <summary>
        /// Reference to the <c>ToolStripLabel</c> showing the legend associated with the <c>ComboBox</c> control.
        /// </summary>
        protected System.Windows.Forms.ToolStripLabel m_ToolStripLegendComboBox1;

        /// <summary>
        /// Reference to the first <c>TabPage</c> control associated with the <c>TabControl</c>.
        /// </summary>
        protected System.Windows.Forms.TabPage m_TabPage1;

        private System.Windows.Forms.ToolStripButton m_TSBEsc;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorEsc;
        private System.Windows.Forms.ToolStripButton m_TSBF1;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF1;
        private System.Windows.Forms.ToolStripButton m_TSBF2;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF2;
        private System.Windows.Forms.ToolStripButton m_TSBF3;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF3;
        private System.Windows.Forms.ToolStripButton m_TSBF4;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF4;
        private System.Windows.Forms.ToolStripButton m_TSBF5;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF5;
        private System.Windows.Forms.ToolStripButton m_TSBF6;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF6;
        private System.Windows.Forms.ToolStripButton m_TSBF7;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF7;
        private System.Windows.Forms.ToolStripButton m_TSBF8;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF8;
        private System.Windows.Forms.ToolStripButton m_TSBF9;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF9;
        private System.Windows.Forms.ToolStripButton m_TSBF10;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF10;
        private System.Windows.Forms.ToolStripButton m_TSBF11;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF11;
        private System.Windows.Forms.ToolStripButton m_TSBF12;
        private System.Windows.Forms.ToolStripSeparator m_TSSeparatorF12;

        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanelInformationLabels;
        private System.Windows.Forms.Label m_Legend1;
        private System.Windows.Forms.Label m_Label1;
        private System.Windows.Forms.Label m_Legend2;
        private System.Windows.Forms.Label m_Label2;
        private System.Windows.Forms.Label m_Legend3;
        private System.Windows.Forms.Label m_Label3;
        private System.Windows.Forms.Label m_Legend4;
        private System.Windows.Forms.Label m_Label4;
        private System.Windows.Forms.Label m_Legend5;
        private System.Windows.Forms.Label m_Label5;
        private System.Windows.Forms.Label m_Label6;
        private System.Windows.Forms.Label m_Legend6;
    }
}