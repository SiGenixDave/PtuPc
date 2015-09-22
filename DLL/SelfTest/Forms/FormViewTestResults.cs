#region --- Revision History ---
/*
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly
 *  prohibited. Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Watch
 * 
 *  File name:  FormViewTestResults.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/10/11    1.0     K.McD           1.  First entry into TortoiseSVN.
 *  
 *  07/25/11    1.1     K.McD           1.  Added support for interactive tests.
 *  
 *  07/26/11    1.2     K.McD           1.  Entered into TortoiseSVN, however, the form is not yet complete. Changes include:
 *                                              (a) Added a thread sleep during the communication loop to reduce processor loading.
 *                                              (b) Modified the sleep interval that allows the wait cursor to be displayed before starting execution
 *                                                  of the self tests.
 *                                              (c) Removed the ResultRowMax constant.
 *                                              
 *  07/29/11    1.2     K.McD           1.  Replaced the ToolStripComboBox control with a ComboBox control.
 *                                      2.  Moved the position of a number of methods.
 *                                      3.  Auto-modified as a result of a number of name changes to some of the Toolbox controls.
 *                                      4.  Added support to allow the user to view individual bits of the bitmask self test variables.
 *                                      5.  Now uses a GroupBox control to contain the interactive test information.
 *                                      6.  Renamed one or more constants.
 *                                      7.  Initialized the loop count to 1 every time the loop forever CheckBox control is selected.
 *                                      8.  Added screen capture support.
 *                                      
 *  08/04/11    1.3     K.McD           1.  Modified a number of XML tags and comments.
 *                                      2.  Added the ConvertUInt32() method to convert the UInt32 data corresponding the the self test variable values
 *                                          to the appropriate signed data type.
 *                                      3.  Restructured the class to make it easier to collect the results on a seperate thread on a later date, if
 *                                          deemed necessary.
 *                                      
 *  08/10/11    1.4     Sean.D          1.  Added support for offline mode. Modified the constructor to conditionally choose CommunicationSelfTest or
 *                                          CommunicationSelfTestOffline depending upon the current mode.
 *                                          
 *                                      2.  Modified the implementation to collect the test results using a Windows timer. Removed the
 *                                          DisplaySelfTestResults() method and replaced this with the: m_TimerGetResultsStart(),
 *                                          m_TimerGetResultsStop() and m_TimerGetResultsTick() mehods.
 *                                          
 *  08/24/11    1.5     K.McD           1.  Disables the Execute button if in online mode.
 *                                      2.  Changed the image associated with the Execute button.
 *                                      3.  Modified the ExitSelfTestTask() method to correctly set the mode before exiting the self test screen.
 *                                      4.  Modified the GetSelfTestMessage() method to set the message to be Resources.EMResultFailed regardless of
 *                                          the state of the result parameter.
 *                                          
 *  10/26/11    1.6     K.McD           1.  SNCR002.41. Added a check to the F2 function key event handler to ensure that the event handler code is
 *                                          ignored if the Enabled property of the control is not asserted.
 *                                          
 *  11/23/11    1.7     K.McD           1.  Ensured that all event handler methods were detached.
 *                                      2.  Modified the Escape_Click() and Exit() methods to ensure that no member variables were referenced after the
 *                                          Close() method had been called.
 *                                          
 *  06/04/12    1.8     Sean.D          1.  Modified ExitSelfTestTask() to drop into Offline mode if there's a communications error.
 *  
 *  07/04/13    1.9     K.McD           1.  Modified the class constructor such that the call to ExitSelfTestTask(), issued in case the Self Test task
 *                                          on the hardware is already running, is not made if the project identifier corresponds to the NYCT - R188
 *                                          project i.e. the embedded software is running on a COM-C device. If this call is made while connected to
 *                                          the COM-C hardware the PTU does not enter self test mode and hangs.
 *                                          
 *                                      2.  Modified ProcessSTCountResult() and ProcessSTListResult() such that any value for the testResult parameter
 *                                          other than ResultPassed (0) is treated as a fail.
 *                                          
 *  03/23/15    1.10    K.McD       References
 *                                  1.  Upgrade the PTU software to extend the support for the R188 project as defined in purchase order
 *                                      4800010525-CU2/19.03.2015.
 *  
 *                                      1.  Changes to address the items outlined in the minutes of the meeting between BTPC, 
 *                                          Kawasaki Rail Car and NYTC on 12th April 2013 - MOC-0171:
 *
 *                                           1.  MOC-0171-27. All occurrences of the ‘Edit’ legend, including function key legends and context menu
 *                                               options will be replaced with the ‘Modify’ legend on ALL projects.
 *                                               
 *                                  Modifications
 *                                  1.  Changed the image resource for the Modify function key.
 *                                  2.  Corrected the name of the CommunicationInterface.StartSelfTestTask() method.
 *                                  
 *  07/23/15    1.11    K.McD       References
 *                                  1.  Part 1 of the upgrade to the Chicago 5000 PTU software that allows the user to download the configuration and help files for
 *                                      a particular Chicago 5000 vehicle control unit (VCU) via an ethernet connection to the FTP (File Transfer Protocol) server
 *                                      running on the VCU. The scope of Part 1 of the upgrade is defined in purchase order 4800011369-CU2 07.07.2015.
 *                                      
 *                                      The upgrade is implemented in two parts, the first part, Part 1, replaces the existing screens and logic with those outlined
 *                                      in slides 6, 7, 8 and 9 of the PowerPoint presentation '076_CTA - PTU file pullback from VCU - 20150127.pptx', but does NOT
 *                                      implement the file transfer; it merely calls an empty external batch file from within the PTU application. The second stage,
 *                                      Part 2, implements the batch file that downloads the configuration and help files from the Vehicle Control Unit (VCU) to the
 *                                      appropriate directory on the PTU computer. As described in the PowerPoint Presentation, this download is only carried out if the
 *                                      appropriate configuration file is not already present on the PTU computer.
 *                                      
 *                                  2.  Bug Fix - SNCR - R188 PTU [20-Mar-2015] Item 24. On selecting the ‘Exit’ function key on the ‘Diagnostics/Event Log’ and
 *                                      the ‘Diagnostics/Self Tests’ screens, the cursor doesn’t go to the Cursors.WaitCursor cursor on the R188 project.
 *                                      
 *                                  Modifications
 *                                  1.  Removed the references to the WriteProgressBarLegend(string.Empty) method and the TaskProgressBar property of the 
 *                                      IMainWindow interface from the Exit() method as these no longer exist. The progress bar used to display the recording and playback
 *                                      of data streams now appears in the 'Information' Panel of the FormWatch Form. The progress bar was moved to allow the
 *                                      status message display to be extended to support some of the longer messages required to support the upgrade shown above.
 *                                      - Ref.: 1.
 *                                      
 *                                  2.  Modified the Exit() method to use ‘Cursor.Current = Cursors.WaitCursor’ rather than ‘this.Cursor = Cursors.WaitCursor’.
 *                                      - Ref.: 2.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Common;
using Common.Communication;
using Common.Configuration;
using Common.Forms;
using Common.UserControls;
using SelfTest.Communication;
using SelfTest.Properties;

namespace SelfTest.Forms
{
    #region --- Structures ---
    /// <summary>
    /// A structure to store the result information associated with a particular test. 
    /// </summary>
    public struct Result_t
    {
        #region - [Fields] -
        /// <summary>
        /// The index value of the <c>List</c> or <c>Array</c> where the result information is stored.
        /// </summary>
        public int Index;

        /// <summary>
        /// The self test number of the test to which the result information applies.
        /// </summary>
		public short TestNumber;

        /// <summary>
        /// The test case identifier associated with the result information.
        /// </summary>
		public short TestCase;

        /// <summary>
        /// A flag to indicate whether the current test passed or failed. True if the test passed; otherwise, false.
        /// </summary>
        public bool Passed;

        /// <summary>
        /// The number of times in the current run that the test has passed.
        /// </summary>
		public int PassCount;

        /// <summary>
        /// The number of times in the current run that the test has failed.
        /// </summary>
		public int FailCount;

        /// <summary>
        /// The number of times in the current run that the test has been executed.
        /// </summary>
        public int ExecutionCount;
        #endregion - [Fields] -

        #region - [Methods] -
        /// <summary>
        /// Copy all field values to the specified <c>Result_t</c> structure.
        /// </summary>
        /// <param name="result">The <c>Result_t</c> structure that the property values are to be copied to.</param>
        public void CopyTo(ref Result_t result)
        {
            result.Index = Index;
            result.TestNumber = TestNumber;
            result.TestCase = TestCase;
            result.Passed = Passed;
            result.PassCount = PassCount;
            result.FailCount = FailCount;
            result.ExecutionCount = ExecutionCount;
        }
        #endregion - [Methods] -
    }
    #endregion --- Structures ---

    /// <summary>
    /// A form to execute those self tests defined in the active test list and to collect and display the results in both list and pass/fail format.
    /// </summary>
    public partial class FormViewTestResults : FormPTU, ICommunicationInterface<ICommunicationSelfTest>
    {
        #region --- Constants ---
        /// <summary>
        /// The tick interval, in ms, associated with the timer used to retrieve the test results. Value: 100 ms. 
        /// </summary>
        private const int IntervalMsTimerGetResults = 100;

        /// <summary>
        /// The value of the resultAvailable parameter returned from the GetSelfTestResult() method corresponding to a valid result being available. Value: 1.
        /// </summary>
        private short ResultAvailable = 1;

        /// <summary>
        /// The value of the testResult parameter returned from the GetSelfTestResult() method corresponding to the test having passed. Value: 1.
        /// </summary>
        private short ResultPassed = 1;

        /// <summary>
        /// The value of the testResult parameter returned from the GetSelfTestResult() method corresponding to the test having failed. Value: 0.
        /// </summary>
        private short ResultFailed = 0;

        /// <summary>
        /// The value of the result parameter returned from the StartSelfTestTask() and ExitSelfestTask() methods if the call was successful. Value: 1.
        /// </summary>
        private short ResultSuccess = 1;

        /// <summary>
        /// The value of the result parameter returned from the StartSelfTestTask() and ExitSelfestTask() methods if the call was unsuccessful and the error message 
        /// is defined by the <c>SELFTESTERRMESS</c> table of the data dictionary. In this case, the reason parameter corresponds to the <c>ERRID</c> field of 
        /// the <c>SELFTESTERRMESS</c> table. Value: 2.
        /// </summary>
        private short ResultDefinedByDataDictionary = 2;

        /// <summary>
        /// The value that must be passed to the CommunicationSelfTest.UpdateSTLoopCount() method in order to make the self test list run continuously. Value: short.MaxValue.
        /// </summary>
        private const short LoopForever = short.MaxValue;

        #region - [ConvertUInt32] -
        /// <summary>
        /// The value corresponding to 2^32. Value: 4,294,967,296.
        /// </summary>
        private const double Math2Power32 = 4294967296;

        /// <summary>
        /// The value corresponding to 2^31. Value: 2,147,483,648.
        /// </summary>
        private const double Math2Power31 = 2147483648;

        /// <summary>
        /// The value corresponding to 2^15. Value: 32,768.
        /// </summary>
        private const double Math2Power15 = 32768;

        /// <summary>
        /// The value corresponding to 2^7. Value: 128.
        /// </summary>
        private const double Math2Power7 = 128;
        #endregion - [ConvertUInt32] -

        #region - [DataGridView Column Indices] -
        /// <summary>
        /// The column index associated with the self test number. Value: 0;
        /// </summary>
        private const int ColumnIndexSelfTestNumber = 0;

        /// <summary>
        /// The column index associated with the test case value. Value: 1.
        /// </summary>
        private const int ColumnIndexTestCase = 1;

        /// <summary>
        /// The column index associated with the self test description. Value: 2.
        /// </summary>
        private const int ColumnIndexDescription = 2;

        /// <summary>
        /// The column index associated with the test results. Value: 3.
        /// </summary>
        private const int ColumnIndexResult = 3;

        /// <summary>
        /// The column index associated with the pass count. Value: 4.
        /// </summary>
        private const int ColumnIndexPassCount = 4;

        /// <summary>
        /// The column index associated with the fail count. Value: 5.
        /// </summary>
        private const int ColumnIndexFailCount = 5;

        /// <summary>
        /// The column index associated with the number of times a particular test has been executed in the current run. Value: 6;
        /// </summary>
        private const int ColumnIndexExecutionCount = 6;
        #endregion - [DataGridView Column Indices] -

        #region - [Heights] -
        /// <summary>
        /// The height, in pixels, of the self test variable user control. Value: 22.
        /// </summary>
        public const int HeightSelfTestControl = 22;
        #endregion - [Heights] -

        #region - [Margins] -
        /// <summary>
        /// The right margin to be applied to the <c>DataGridView</c> control. Value: 2.
        /// </summary>
        public const int MarginRightDataGridViewControl = 2;

        /// <summary>
        /// The left margin associated with the self test variable user control. Value: 10.
        /// </summary>
        public const int MarginLeftSelfTestControl = 0;

        /// <summary>
        /// The right margin associated with the self test variable user control. Value: 2.
        /// </summary>
        public const int MarginRightSelfTestControl = 0;

        /// <summary>
        /// The top margin associated with the self test variable user control. Value: 2.
        /// </summary>
        public const int MarginTopSelfTestControl = 3;

        /// <summary>
        /// The bottom margin associated with the self test variable user control. Value: 2.
        /// </summary>
        public const int MarginBottomSelfTestControl = 2;
        #endregion - [Margins] -

        #region - [Widths] -
        /// <summary>
        /// The width to allow for a verical scrollbar.
        /// </summary>
        public const int WidthScrollBar = 28;

        /// <summary>
        /// The combined width of the left and right borders around the self test variables panel. Used to size the help window panel.
        /// </summary>
        public const int WidthBorder = 12;

        /// <summary>
        /// The width, in pixels, of the variable name field of the self test variable user control. Value: 250.
        /// </summary>
        public const int WidthSelfTestControlVariableNameField = 250;

        /// <summary>
        /// The width, in pixels, of the value field of the self test variable user control. Value: 160.
        /// </summary>
        public const int WidthSelfTestControlValueField = 160;

        /// <summary>
        /// The width, in pixels, of the units field of the self test variable user control. Value: 75.
        /// </summary>
        public const int WidthSelfTestControlUnitsField = 75;
        #endregion - [Widths] -
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// A flag to indicate whether the PTU is in online or offline mode. True, if the PTU is in online mode; otherise, false.
        /// </summary>
        private bool m_IsOnline;

        /// <summary>
        /// Reference to the selected communication interface.
        /// </summary>
        private ICommunicationSelfTest m_CommunicationInterface;

        /// <summary>
        /// Timer used to collect the test results.
        /// </summary>
        private System.Windows.Forms.Timer m_TimerGetResults = new System.Windows.Forms.Timer();

        /// <summary>
        /// A flag to indicate whether a test run is currently active. True, if a test run is active; otherwise, false.
        /// </summary>
        private bool m_TestsActive;

        #region - [ProcessSTInteractiveResult] -
        /// <summary>
        /// A flag that indicates whether this is the first call in the current run to the ProcessSTInteractiveResult() method. True, if this is the first 
        /// call; otherwise, false.
        /// </summary>
        private bool m_FirstPassProcessSTInteractiveResult = true;

        /// <summary>
        /// The test number associated with the previous interactive test.
        /// </summary>
        private short m_PreviousInteractiveTestNumber = CommonConstants.NotDefined;

        /// <summary>
        /// The test case associated with the previous set of results.
        /// </summary>
        private short m_PreviousTestCase = CommonConstants.NotDefined;

        /// <summary>
        /// The <c>SelfTestRecord</c> associated with the current interactive test.
        /// </summary>
        private SelfTestRecord m_InteractiveTestRecord;
        #endregion - [ProcessSTInteractiveResult] -

        #region - [Test Lists] -
        /// <summary>
        /// A list of the test numbers associated with the tests that are defined in the active test list.
        /// </summary>
        private List<short> m_TestList = new List<short>();

        /// <summary>
        /// Reference to the user defined test list.
        /// </summary>
        private TestListRecord m_UserDefinedTestListRecord;

        /// <summary>
        /// Reference to the selected test list record.
        /// </summary>
        private TestListRecord m_TestListRecord;
        #endregion - [Test Lists] -

        #region - [Display] -
        /// <summary>
        /// A list of the test results associated with the DataGridView that is used to display the results in list format.
        /// </summary>
        private List<Result_t> m_ListResultList = new List<Result_t>();

        /// <summary>
        /// A list of the test results associated with the DataGridView that is used to display the results in pass/fail format.
        /// </summary>
        /// <remarks>This is used by the ProcessSTPassFailResult() method.</remarks>
        private List<Result_t> m_PassFailResultList = new List<Result_t>();

        /// <summary>
        /// Reference to the structure that defines the size of the self test variable user controls.
        /// </summary>
        private VariableControlSize_t m_SelfTestVariableControlSize;
        #endregion - [Display] -

        #region - [Loop Count] -
        /// <summary>
        /// The number of times that the tests defined in the active test list are to be executed.
        /// </summary>
        private short m_LoopCount;

        /// <summary>
        /// A flag that indicates the state of the Enabled property of the <c>NumericUpDown</c> control just prior to starting the self tests.
        /// </summary>
        private bool m_NumericUpDownLoopCountEnabledState;

        /// <summary>
        /// A flag that indicates the state of the Enabled property of the 'Loop Forever' <c>CheckBox</c> just prior to starting the self tests.
        /// </summary>
        private bool m_CheckBoxLoopForeverEnabledState;
        #endregion - [Loop Count] -
        #endregion --- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initialize a new instance of the class. Zero parameter constructor.
        /// </summary>
        public FormViewTestResults()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new instance of the class
        /// </summary>
        /// <param name="communicationInterface">Reference to the communicaton interface that is to be used to communicate with the VCU.</param>
        /// <param name="mainWindow">Reference to the main application window.</param>
        /// <remarks>The reference to the main application window is passed as a parameter rather than relying on the usual mechanism, associated with
        /// the MdiParent property, implemented in the FormPTU parent class as the ExecuteSelfTests() method, included in the constructor, displays
        /// status messages on the main application window.</remarks>
        public FormViewTestResults(ICommunicationParent communicationInterface, IMainWindow mainWindow)
        {
            InitializeComponent();

            // Initialize the communication interface.
            if (communicationInterface is CommunicationParent)
            {
                CommunicationInterface = new CommunicationSelfTest(communicationInterface);
                m_IsOnline = true;
            }
            else
            {
                CommunicationInterface = new CommunicationSelfTestOffline(communicationInterface);
            }
            Debug.Assert(CommunicationInterface != null, "FormViewTestResults.Ctor() - [CommunicationInterface != null]");

            MainWindow = mainWindow;
            Debug.Assert(mainWindow != null, "FormViewTestResults.Ctor() - [mainWindow != null]");

            // If the hardware is anything other than the COM-C unit that is used on the NYCT project, issue a request to 
            // exit the self test task, just in case it may be running. If this call is made while connected to a COM-C unit 
            // the PTU does not enter self test mode and hangs.
            if (Parameter.ProjectInformation.ProjectIdentifier != CommonConstants.ProjectIdNYCT)
            {
                ExitSelfTestTask();
            }

            StartSelfTestTask();

            #region - [Size Definitions] -
            m_SelfTestVariableControlSize = new VariableControlSize_t();
            m_SelfTestVariableControlSize.Margin.Left = MarginLeftSelfTestControl;
            m_SelfTestVariableControlSize.Margin.Right = MarginRightSelfTestControl;
            m_SelfTestVariableControlSize.Margin.Top = MarginTopSelfTestControl;
            m_SelfTestVariableControlSize.Margin.Bottom = MarginBottomSelfTestControl;
            m_SelfTestVariableControlSize.WidthVariableNameField = WidthSelfTestControlVariableNameField;
            m_SelfTestVariableControlSize.WidthValueField = WidthSelfTestControlValueField;
            m_SelfTestVariableControlSize.WidthUnitsField = WidthSelfTestControlUnitsField;
            m_SelfTestVariableControlSize.Height = HeightSelfTestControl;
            #endregion - [Size Definitions] -

            #region - [Function Keys] -
            // Escape - Exit
            // F1 - Help
            // F2 - Print
            // F3 - Execute
            // F4 - Abort
            // F5 - User Defined
            // F6 - Continue - Now moved to the interactive test GroupBox control.
            DisplayFunctionKey(F3, Resources.FunctionKeyTextExecute, Resources.FunctionKeyToolTipExecute, Resources.SelfTest);

            // Only enable the Execute button if the PTU is in online mode.
            if (m_IsOnline == true)
            {
                F3.Enabled = true;
            }
            else
            {
                F3.Enabled = false;
            }

            DisplayFunctionKey(F4, Resources.FunctionKeyTextAbort, Resources.FunctionKeyToolTipAbort, Resources.Abort);

            // Don't enable the 'Abort' key until the tests are active.
            F4.Enabled = false;

            DisplayFunctionKey(F5, Resources.FunctionKeyTextEdit, Resources.FunctionKeyToolTipEdit, Resources.Modify);

            // The continue key has been moved to the self test variables panel as it is proving very difficult to get keyboard focus while the
            // interactive test help window is on display.
            // DisplayFunctionKey(F6, Resources.FunctionKeyTextContinue, Resources.FunctionKeyToolTipContinue, Resources.MoveLast);

            // Don't enable the 'Continue' key until the the results of an interactive test are received.
            F6.Enabled = false;
            #endregion - [Function Keys] -

            #region - [ComboBox] -
            ComboBoxAddTestLists(ref m_ComboBoxTestList);
            #endregion - [ComboBox] -

            #region - [Timer] -
            // The timer used to retrieve the test results.
            m_TimerGetResults.Interval = IntervalMsTimerGetResults;
            m_TimerGetResults.Enabled = false;
            m_TimerGetResults.Tick += new EventHandler(m_TimerGetResults_Tick);
            #endregion - [Timer] -

            // Select the default test list record.
            TestListRecord = (TestListRecord)m_ComboBoxTestList.Items[0];

            // Update the status information.
            MainWindow.WriteStatusMessage(string.Empty);
            MainWindow.ShowBusyAnimation = false;

            m_TestsActive = false;
        }
        #endregion --- Constructors ---

        #region --- Cleanup ---
        /// <summary>
        /// Clean up the resources used by the form.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Cleanup(bool disposing)
        {
            // Ensure that the help window is closed.
            WinHlp32.HideHelpWindow(this.Handle.ToInt32());

            // If the test run is active, issue the command to abort the test run.
            if (m_TestsActive == true)
            {
                AbortSelfTests();
            }

            ExitSelfTestTask();

            // Ensure that the 'busy' animation and status message are cleared.
            if (MainWindow != null)
            {
                MainWindow.ShowBusyAnimation = false;
                MainWindow.WriteStatusMessage(string.Empty);
            }

            try
            {
                if (disposing)
                {
                    // Cleanup managed objects by calling their Dispose() methods.
                    if (components != null)
                    {
                        components.Dispose();
                    }

                    if (m_TimerGetResults != null)
                    {
                        // Detach the event handler method.
                        m_TimerGetResults.Tick -= new EventHandler(m_TimerGetResults_Tick);
                        m_TimerGetResults.Dispose();
                    }
                }

                // Whether called by consumer code or the garbage collector free all unmanaged resources and set the value of managed data members to null.
                if (m_TestList != null)
                {
                    m_TestList.Clear();
                }

                if (m_ListResultList != null)
                {
                    m_ListResultList.Clear();
                }

                if (m_PassFailResultList != null)
                {
                    m_PassFailResultList.Clear();
                }

                m_CommunicationInterface = null;
                m_TestListRecord = null;
                m_TestList = null;
                m_PassFailResultList = null;
                m_UserDefinedTestListRecord = null;
                m_TimerGetResults = null;

                #region - [Detach the event handler methods.] -
                this.m_ToolStripButtonInteractiveTestContinue.Click -= new EventHandler(this.F6_Click);
                this.m_ToolStripSeparatorInteractiveTestContinue.Click -= new EventHandler(this.F6_Click);
                this.m_ToolStripButtonInteractiveTestAbort.Click -= new EventHandler(this.F4_Click);
                this.m_DataGridViewListResult.CellContentDoubleClick -= new DataGridViewCellEventHandler(this.m_DataGridViewListResult_CellContentDoubleClick);
                this.m_DataGridViewListResult.SelectionChanged -= new EventHandler(this.m_DataGridView_SelectionChanged);
                this.m_ContextMenuStripDataGridViewListResult.Opened -= new EventHandler(this.m_ContextMenuStripDataGridViewListResult_Opened);
                this.m_ToolStripMenuItemListResultShowDefinition.Click -= new EventHandler(this.m_ToolStripMenuItemListResultShowDefinition_Click);
                this.m_ToolStripMenuItemListResultFaultSummary.Click -= new EventHandler(this.m_ToolStripMenuItemListResultTestCaseAnalysis_Click);
                this.m_CheckBoxLoopForever.CheckedChanged -= new EventHandler(this.m_CheckBoxLoopForever_CheckedChanged);
                this.m_DataGridViewTestList.CellContentDoubleClick -= new DataGridViewCellEventHandler(this.m_DataGridViewTestList_CellContentDoubleClick);
                this.m_DataGridViewTestList.SelectionChanged -= new System.EventHandler(this.m_DataGridView_SelectionChanged);
                this.m_ToolStripMenuItemTestListShowDefinition.Click -= new EventHandler(this.m_ToolStripMenuItemTestListShowDefinition_Click);
                this.Shown -= new EventHandler(this.FormViewTestResults_Shown);
                #endregion - [Detach the event handler methods.] -
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
        /// <summary>
        /// Event handler for the <c>Shown</c> event.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void FormViewTestResults_Shown(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Update();

            // Ensure that an exception isn't thrown when a child form is opened in the Visual Studio development environment.
            if (MainWindow == null)
            {
                return;
            }

            #region - [Panels] -
            m_GroupBoxInteractiveTest.Width = m_SelfTestVariableControlSize.Size.Width + WidthScrollBar;

            m_PanelWindowsHelpTestList.Width = m_SelfTestVariableControlSize.Size.Width;
            m_PanelWindowsHelpListResult.Width = m_GroupBoxInteractiveTest.Width + WidthBorder;
            m_PanelWindowsHelpPassFailResult.Width = m_SelfTestVariableControlSize.Size.Width;
            #endregion - [Panels] -
        }
        #endregion - [Form] -

        #region - [Function Keys] -
        #region - [Escape] -
        /// <summary>
        /// Event handler for the escape key <c>Click</c> event. Close the form.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void Escape_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (Escape.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            Escape.Checked = true;

            // This is implemented purely for aesthetics.
            m_NumericUpDownLoopCount.Hide();
            m_CheckBoxLoopForever.Hide();
            m_LegendLoopCount.Hide();

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            Escape.Checked = false;
            Cursor = Cursors.Default;

            Exit();
        }
        #endregion - [Escape] -

        #region - [F2-Print] -
        /// <summary>
        /// Event handler for the 'F2-Print' button <c>Click</c> event. Capture the window and save the image to the specified file.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F2_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F2.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F2.Checked = true;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            ScreenCaptureType = ScreenCaptureType.SelfTest;
            base.F2_Click(sender, e);

            F2.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F2-Print] -

        #region - [F3-Execute] -
        /// <summary>
        /// Event handler for the 'F3-Execute' key <c>Click</c> event. Execute the selected tests and display the results.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F3_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F3.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Check whether the user has already started the tests.
            if (F3.Checked == true)
            {
                AbortSelfTests();
                Escape.Enabled = true;
                F3.Checked = false;
                Cursor = Cursors.Default;
                return;
            }

            F3.Checked = true;
            SetEnabled(false);

            // Display the tab page that shows the results in list format.
            m_TabControl.SelectTab(m_TabPageListResult);

            // Clear the status message.
            if (MainWindow != null)
            {
                MainWindow.WriteStatusMessage(string.Empty);
            }

            // Initialize the variables used by the ProcessSTInteractiveResult() method.
            m_FirstPassProcessSTInteractiveResult = true;
            m_PreviousInteractiveTestNumber = CommonConstants.NotDefined;
            m_PreviousTestCase = CommonConstants.NotDefined;

            try
            {
                ExecuteSelfTests(m_TestList);
            }
            catch (CommunicationException communicationException)
            {
                MessageBox.Show(communicationException.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            // If the help window is on display then hide it.
            WinHlp32.HideHelpWindow(this.Handle.ToInt32());

            GetResultsStart();
        }
        #endregion - [F3-Execute] -

        #region - [F4-Abort] -
        /// <summary>
        /// Event handler for the 'F4-Abort' key <c>Click</c> event. Abort the current self test sequence.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F4_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Ignore this so that the Abort ToolStripButton on the interactive results panel works correctly.
            /*
            // Skip if the key isn't enabled.
            if (F4.Enabled == false)
            {
                return;
            }
            */

            Cursor = Cursors.WaitCursor;
            F4.Checked = true;
            AbortSelfTests();
            F4.Checked = false;
            F4.Enabled = false;
            Cursor = Cursors.Default;

            // Allow the user to edit the test list.
            F5.Enabled = true;
            
            // If the help window is on display then hide it.
            WinHlp32.HideHelpWindow(this.Handle.ToInt32());
        }
        #endregion - [F4-Abort] -

        #region - [F5-Edit] -
        /// <summary>
        /// Event handler for the 'F5-Edit' key <c>Click</c> event. Configure a user defined test list.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F5_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F5.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F5.Checked = true;

            WinHlp32.HideHelpWindow(this.Handle.ToInt32());

            try
            {
                FormTestListDefine formTestListDefine = new FormTestListDefine(TestListRecord);
                formTestListDefine.CalledFrom = this;
                formTestListDefine.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                F5.Checked = false;
                Cursor = Cursors.Default;
            }
        }
        #endregion - [F5-Edit] -

        #region - [F6-Continue] -
        /// <summary>
        /// Event handler for the 'F6-Continue' key <c>Click</c> event. Used to continue to the next test case during an interactive test.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected override void F6_Click(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Skip if the key isn't enabled.
            if (F6.Enabled == false)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            F6.Checked = true;
            m_ToolStripButtonInteractiveTestContinue.Checked = true;
            SendOperatorAcknowledge();
            F6.Checked = false;
            m_ToolStripButtonInteractiveTestContinue.Checked = false;
            F6.Enabled = false;
            Cursor = Cursors.Default;
        }
        #endregion - [F6-Continue] -
        #endregion - [Function Keys] -

        #region - [ComboBox] -
        /// <summary>
        /// Event handler for the <c>ComboBox</c> control <c>SelectedIndexChanged</c> event. Load the selected test list.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event. In this case the value will be null as the sender is a static class.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        protected void m_ComboBoxTestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            TestListRecord testListRecord = (TestListRecord)m_ComboBoxTestList.SelectedItem;

            // If the user selected the user defined test list, use the updated version of the user defined test record.
            if (testListRecord.Identifier == short.MaxValue)
            {
                TestListRecord = m_UserDefinedTestListRecord;
            }
            else
            {
                TestListRecord = testListRecord;
            }

            // Ensure that the help window is closed.
            WinHlp32.HideHelpWindow(this.Handle.ToInt32());

            Cursor = Cursors.Default;
        }
        #endregion - [ComboBox] -

        #region - [Context Menu] -
        /// <summary>
        /// Event handler for the<c>Opened</c> event of the <c>ContextMenuStrip</c> linked to the <c>DataGridView</c> control used to display the results in 
        /// pass/fail format. Set the Enabled property of the 'Test Case Analysis' context menu option according to the state of the selected row.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ContextMenuStripDataGridViewListResult_Opened(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Check that at least one row has been selected.
            if (m_DataGridViewListResult.SelectedRows.Count <= 0)
            {
                return;
            }

            // Get the selected row.
            DataGridViewRow dataGridViewRow = m_DataGridViewListResult.SelectedRows[0];

            // Only display the test case analysis if the test failed.
            string resultText = (string)dataGridViewRow.Cells[ColumnIndexResult].Value;
            if (resultText.Equals(Resources.TextFailed))
            {
                m_ToolStripMenuItemListResultFaultSummary.Enabled = true;
            }
            else
            {
                m_ToolStripMenuItemListResultFaultSummary.Enabled = false;
            }
        }

        /// <summary>
        /// Event handler for the 'Show Definition' context menu associated with the <c>DataGridView</c> control used to display the selected tests. Show 
        /// the help information for the test associated with the selected row of the <c>DataGridView</c>.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemTestListShowDefinition_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ShowTestDefinition(m_DataGridViewTestList, m_PanelWindowsHelpTestList);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the 'Show Definition' context menu associated with the <c>DataGridView</c> control used to display the results in list format. Show 
        /// the help information for the test associated with the selected row of the <c>DataGridView</c>.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemListResultShowDefinition_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ShowTestDefinition(m_DataGridViewListResult, m_PanelWindowsHelpListResult);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the 'Test Case Analysis' context menu associated with the <c>DataGridView</c> control used to display the results in list format. Show 
        /// the test case help information for the test associated with the selected row of the <c>DataGridView</c> if it is available.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemListResultTestCaseAnalysis_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ShowTestMessage(m_DataGridViewListResult, m_PanelWindowsHelpListResult);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the 'Show Definition' context menu associated with the <c>DataGridView</c> control used to display the results in pass/fail format. 
        /// Show the help information for the test associated with the selected row of the <c>DataGridView</c>.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_ToolStripMenuItemPassFailResultShowDefinition_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ShowTestDefinition(m_DataGridViewPassFailResult, m_PanelWindowsHelpPassFailResult);
            Cursor = Cursors.Default;
        }
        #endregion - [Context Menu] -

        #region - [DataGridView] -
        /// <summary>
        /// Event handler for the <c>CellContentDoubleClick</c> event associated with <c>DataGridView</c> control used to display the selected tests. Show 
        /// the help information for the test associated with the selected row of the <c>DataGridView</c>.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_DataGridViewTestList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ShowTestDefinition(m_DataGridViewTestList, m_PanelWindowsHelpTestList);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the <c>CellContentDoubleClick</c> event associated with <c>DataGridView</c> control used to display the results in list format. If the 
        /// user selected the pass or fail count columns of the <c>DataGridView</c> control, show the test case help information for the test associated with the 
        /// selected row of the <c>DataGridView</c> if it is available; otherwise, show the help information for the test.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_DataGridViewListResult_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool isAvailable = false;

            Cursor = Cursors.WaitCursor;

            // Only display the test case help if the user selected the pass count or fail count columns.
            if (e.ColumnIndex == ColumnIndexResult)
            {
                isAvailable = ShowTestMessage(m_DataGridViewListResult, m_PanelWindowsHelpListResult);
            }

            if (isAvailable == false)
            {
                ShowTestDefinition(m_DataGridViewListResult, m_PanelWindowsHelpListResult);
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the <c>CellContentDoubleClick</c> event associated with <c>DataGridView</c> control used to display the results in pass/fail format. 
        /// Show the help information for the test associated with the selected row of the <c>DataGridView</c>.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_DataGridViewPassFailResult_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ShowTestDefinition(m_DataGridViewPassFailResult, m_PanelWindowsHelpPassFailResult);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event handler for the <c>SelectionChanged</c> event associated with all <c>DataGridView</c> controls. Close the Windows help window.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            WinHlp32.HideHelpWindow(this.Handle.ToInt32());
        }

        /// <summary>
        /// Event handler for the <c>SelectedIndexChanged</c> event associated with the <c>TabControl</c>. Close the Windows help window.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
                WinHlp32.HideHelpWindow(this.Handle.ToInt32());
        }
        #endregion - [DataGridView] -

        #region - [Timer] -
        /// <summary>
        /// Event handler for the timer <c>Tick</c> event. Check whether a new test result is available and, if so, process it.
        /// </summary>
        private void m_TimerGetResults_Tick(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            short resultAvailable, messageIdentifier, testNumber, testCase, testResult, variableCount;
            MessageMode messageMode;
            TruckInformation truckInformation;
            SelfTestRecord interactiveTestRecord = new SelfTestRecord();
            InteractiveResults_t[] interactiveResults;

            try
            {
                CommunicationInterface.GetSelfTestResult(out resultAvailable, out messageMode, out messageIdentifier, out testCase, out testResult, out truckInformation, out variableCount, out interactiveResults);
                if (MainWindow != null)
                {
                    MainWindow.BlinkUpdateIcon();
                }

                if (resultAvailable == ResultAvailable)
                {
                    // Now that the first result is available set the cursor to the default cursor.
                    Cursor = Cursors.Default;

                    if (MainWindow != null)
                    {
                        MainWindow.WriteStatusMessage(Resources.SMCollectingResults);
                    }

                    switch (messageMode)
                    {
                        case MessageMode.Special:
                            GetResultsStop();
                            ProcessSTSpecialMessage(messageIdentifier);
                            break;
                        case MessageMode.Interactive:
                            testNumber = messageIdentifier;
                            ProcessSTInteractiveResult(testNumber, testCase, variableCount, interactiveResults);
                            break;
                        case MessageMode.Brief:
                        case MessageMode.Detailed:
                            testNumber = messageIdentifier;
                            ProcessSTListResult(testNumber, testCase, testResult);
                            break;
                        default:
                            GetResultsStop();
                            Debug.Assert(false, "FormViewTestResults.DisplaySelfTestResults() - [messageMode value - not supported.]");
                            break;
                    }
                }
                return;
            }
            catch (CommunicationException)
            {
                GetResultsStop();

                // If the help window is on display then hide it.
                WinHlp32.HideHelpWindow(this.Handle.ToInt32());

                // Update the status information.
                MainWindow.WriteStatusMessage(string.Empty);
                m_TestsActive = false;
                MainWindow.ShowBusyAnimation = false;

                MessageBox.Show(Resources.MBTSTResultFailed, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion - [Timer] -

        /// <summary>
        /// Event handler for the <c>CheckedChanged</c> event associated with the <c>CheckBox</c> control that is used to force the self tests to loop continuously.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="e">Parameter passed from the object that raised the event.</param>
        private void m_CheckBoxLoopForever_CheckedChanged(object sender, EventArgs e)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (m_CheckBoxLoopForever.Checked == true)
            {
                m_NumericUpDownLoopCount.Enabled = false;
                m_NumericUpDownLoopCount.Value = 1;
            }
            else
            {
                m_NumericUpDownLoopCount.Enabled = true;
            }
        }
        #endregion --- Delegated Methods ---

        #region --- Methods ---
        /// <summary>
        /// Close the form cleanly. Simulates the user pressing the Exit button.
        /// </summary>
        public override void Exit()
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            Escape.Checked = true;

            if (MainWindow != null)
            {
                // Clear the status message.
                MainWindow.WriteStatusMessage(string.Empty);
            }

            Escape.Checked = false;

            base.Exit();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Set the Enabled property of those controls that need to be inhibited while the self tests are running.
        /// </summary>
        /// <param name="state">The state that the Enabled properties are to be set to.</param>
        private void SetEnabled(bool state)
        {
            // Main menu options.
            if (MainWindow != null)
            {
                MainWindow.MenuStrip.Enabled = state;
            }

            m_ComboBoxTestList.Enabled = state;

            // Escape key.
            Escape.Enabled = state;

            // Help key.
            F1.Enabled = state;

            // Print key.
            F2.Enabled = state;

            // Edit key.
            F5.Enabled = state;

            // If the properties are to be set to the disabled state i.e. the self tests are about to be run, keep a record of the current state of the
            // Enabled property of the NumericUpDown and CheckBox controls.
            if (state == false)
            {
                m_CheckBoxLoopForeverEnabledState = m_CheckBoxLoopForever.Enabled;
                m_NumericUpDownLoopCountEnabledState = m_NumericUpDownLoopCount.Enabled;
                m_CheckBoxLoopForever.Enabled = state;
                m_NumericUpDownLoopCount.Enabled = state;
            }
            else
            {
                m_CheckBoxLoopForever.Enabled = m_CheckBoxLoopForeverEnabledState;
                m_NumericUpDownLoopCount.Enabled = m_NumericUpDownLoopCountEnabledState;
            }
        }

        #region - [Timer] -
        /// <summary>
        /// Start the timer that collects the test results. 
        /// </summary>
        private void GetResultsStart()
        {
            m_TimerGetResults.Enabled = true;
            m_TimerGetResults.Start();
        }

        /// <summary>
        /// Stop the timer that collects the test results.
        /// </summary>
        private void GetResultsStop()
        {
            m_TimerGetResults.Stop();
            m_TimerGetResults.Enabled = false;
        }
        #endregion - [Timer] -

        #region - [Process Results] -
        /// <summary>
        /// Process the result for use in the pass/fail count presentation. Update the <c>DataGridView</c> used to display the results in pass/fail format with 
        /// the latest result retrieved from the VCU.
        /// </summary>
        /// <param name="testNumber">The test number retrieved from the VCU.</param>
        /// <param name="testResult">The test result retrieved from the VCU.</param>
        /// <param name="executionCount">The number of times the test associated with the result has been executed during the current run.</param>
        private void ProcessSTCountResult(short testNumber, short testResult, out int executionCount)
        {
            executionCount = 0;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Check whether the currest test has been executed at least once during the current run.
            Result_t foundResult = m_PassFailResultList.Find(delegate(Result_t searchResult) { return searchResult.TestNumber == testNumber; });

            // Create a new structure to store the pass/fail information.
            Result_t passFailResult = new Result_t();
            if (foundResult.TestNumber == 0)
            {
                // No, initialize the pass/fail information for the current result and add it to the pass/fail result list.
                passFailResult.Index = m_PassFailResultList.Count;
                passFailResult.TestNumber = testNumber;
                passFailResult.TestCase = CommonConstants.NotDefined;

                if (testResult == ResultPassed)
                {
                    passFailResult.Passed = true;
                    passFailResult.PassCount = 1;
                }
                else if (testResult == ResultFailed)
                {
                    passFailResult.FailCount = 1;
                }
                else
                {
                    passFailResult.FailCount = 1;
                }

                passFailResult.ExecutionCount = 1;

                // Add the new structure to the list used to display the results in pass/fail format.
                m_PassFailResultList.Add(passFailResult);
            }
            else
            {
                // Yes, update the existing entry in the pass/fail result list with the latest pass/fail information for the current result.
                foundResult.CopyTo(ref passFailResult);
                passFailResult.ExecutionCount++;
                if (testResult == ResultPassed)
                {
                    passFailResult.Passed = true;
                    passFailResult.PassCount++;
                }
                else if (testResult == ResultFailed)
                {
                    passFailResult.Passed = false;
                    passFailResult.FailCount++;
                }
                else
                {
                    passFailResult.Passed = false;
                    passFailResult.FailCount++;
                }

                m_PassFailResultList.RemoveAt(foundResult.Index);
                m_PassFailResultList.Insert(foundResult.Index, passFailResult);
            }

            executionCount = passFailResult.ExecutionCount;

            // --------------------------------
            // Update the DataGridView control.
            // --------------------------------
            DataGridViewRow dataGridViewRow;
            dataGridViewRow = ConvertToDataGridViewRow(passFailResult);

            // Alert the user if one or more of the current tests have failed.
            if (passFailResult.FailCount > 0)
            {
                dataGridViewRow.Cells[ColumnIndexFailCount].Style.BackColor = Color.Red;
            }

            // Check whether the result corresponds to the first occurrence of the test during the current run.
            if (passFailResult.ExecutionCount == 1)
            {
                // Yes, add the row to the DataGridView control.
                m_DataGridViewPassFailResult.Rows.Add(dataGridViewRow);
            }
            else
            {
                // No, update the existing row of the DataGridView control.
                m_DataGridViewPassFailResult.Rows.RemoveAt(passFailResult.Index);
                m_DataGridViewPassFailResult.Rows.Insert(passFailResult.Index, dataGridViewRow);
            }
        }

        /// <summary>
        /// Process the result for use in the list presentation. Update the <c>DataGridView</c> used to display the results in list format with the latest result 
        /// retrieved from the VCU.
        /// </summary>
        /// <param name="testNumber">The test number of the test associated with the result.</param>
        /// <param name="testCase">The test case retrieved from the VCU.</param>
        /// <param name="testResult">The test result retrieved from the VCU.</param>
        internal void ProcessSTListResult(short testNumber, short testCase, short testResult)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (MainWindow != null)
            {
                MainWindow.BlinkUpdateIcon();
            }

            // Disable the F6-Continue key.
            F6.Enabled = false;

            // Enable the standard Abort key.
            F4.Enabled = true;

            CloseShowFlags();

            // Check that the testNumber value is valid.
            try
            {
                SelfTestRecord selfTestRecord = Lookup.SelfTestTableBySelfTestNumber.Items[testNumber];
                if (selfTestRecord == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return; ;
            }

            int executionCount;
            ProcessSTCountResult(testNumber, testResult, out executionCount);

            // Create a new structure to store the result in list format.
            Result_t listResult = new Result_t();
            listResult.Index = m_ListResultList.Count;
            listResult.TestNumber = testNumber;
            listResult.TestCase = testCase;

            if (testResult == ResultPassed)
            {
                listResult.Passed = true;
                listResult.PassCount = 1;
            }
            else if (testResult == ResultFailed)
            {
                listResult.PassCount = 0;
            }
            else
            {
                listResult.PassCount = 0;
            }

            // Update the execution count associated with the current test.
            listResult.ExecutionCount = executionCount;

            // Add the new structure to the list used to display the results in list format.
            m_ListResultList.Add(listResult);

            // --------------------------------
            // Update the DataGridView control.
            // --------------------------------
            DataGridViewRow dataGridViewRow = ConvertToDataGridViewRow(listResult);

            // Alert the user if the current test failed.
            if (testResult == ResultFailed)
            {
                dataGridViewRow.Cells[ColumnIndexResult].Style.BackColor = Color.Red;
            }

            // Add the row to the DataGridView control.
            m_DataGridViewListResult.Rows.Add(dataGridViewRow);
        }

        /// <summary>
        /// Process the result of the interactive test.
        /// </summary>
        /// <param name="testNumber">The test number of the interactive test associated with the result.</param>
        /// <param name="testCase">The current test case.</param>
        /// <param name="variableCount">The number of self test variables associated with the interactive test.</param>
        /// <param name="interactiveResults">The array of self test variable values retrieved from the VCU.</param>
        internal void ProcessSTInteractiveResult(short testNumber, short testCase, short variableCount, InteractiveResults_t[] interactiveResults)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Disable the standard Abort key.
            F4.Enabled = false;

            if (m_FirstPassProcessSTInteractiveResult == true)
            {
                // Disable the DataGridView control.
                m_DataGridViewListResult.Enabled = false;

                // Enable the F6-Continue key.
                F6.Enabled = true;

                m_FirstPassProcessSTInteractiveResult = false;
            }
            
            // If the test number has changed since the previous call create a new self test record corresponding to the test number.
            if (testNumber != m_PreviousInteractiveTestNumber)
            {
                // Get the template of the self test record associated with the test number.
                try
                {
                    SelfTestRecord selfTestRecordTemplate = Lookup.SelfTestTableBySelfTestNumber.Items[testNumber];
                    if (selfTestRecordTemplate == null)
                    {
                        throw new ArgumentException();
                    }

                    // Copy the existing template to a new record.
                    m_InteractiveTestRecord = new SelfTestRecord(selfTestRecordTemplate.Identifier);
                    m_InteractiveTestRecord.SelfTestVariableList = Lookup.SelfTestTableBySelfTestNumber.CreateSelfTestVariableList(m_InteractiveTestRecord.SelfTestNumber);

                    // Ensure that the self test variable panel is shown.
                    m_GroupBoxInteractiveTest.Visible = true;

                    ConfigureSelfTestControls(m_PanelInteractiveTestVariableList, m_SelfTestVariableControlSize, m_InteractiveTestRecord.SelfTestVariableList);
                }
                catch (Exception)
                {
                    m_InteractiveTestRecord = null;
                }
            }

            // If either the test number or test case values have changed show the appropriate test message.
            if ((testNumber != m_PreviousInteractiveTestNumber) || (testCase != m_PreviousTestCase))
            {
                // Check that both values are greater than or equal to 1.
                if ((testNumber < 1) || (testCase < 1))
                {
                    throw new ArgumentException();
                }

                int helpIndex = Lookup.SelfTestTable.GetTestMessageHelpIndex(testNumber, testCase);

                // If the help index for the record is defined, show the help information.
                if (helpIndex != CommonConstants.NotDefined)
                {
                    WinHlp32.ShowHelpWindow(m_PanelInteractiveTestHelp.Handle.ToInt32(), helpIndex, WinHlp32.HWND_TOPMOST);
                }

                m_PreviousInteractiveTestNumber = testNumber;
                m_PreviousTestCase = testCase;

                // Enable the 'F6-Continue' function key again.
                F6.Enabled = true;
            }

            if (m_InteractiveTestRecord == null)
            {
                return;
            }

            Debug.Assert(m_InteractiveTestRecord.SelfTestVariableList.Count == variableCount, "FormViewTestResults.ProcessSTInteractiveResult() - [m_InteractiveTestRecord.SelfTestVariableList.Count == variableCount]");

            // Update the test record with the current values of the self test variables.
            for (int selfTestVariableIndex = 0; selfTestVariableIndex < variableCount; selfTestVariableIndex++)
            {
                // The Value field of the InteractiveResults_t structure is a UInt32 value represented as a double, to display the correct value this must be converted 
                // to the value associated with the data type of the self test variable.
                m_InteractiveTestRecord.SelfTestVariableList[selfTestVariableIndex].ValueFromTarget = ConvertUInt32(interactiveResults[selfTestVariableIndex].Value, m_InteractiveTestRecord.SelfTestVariableList[selfTestVariableIndex].DataType);
            }

            m_GroupBoxInteractiveTest.Text = m_InteractiveTestRecord.SelfTestNumber.ToString() + CommonConstants.BindingTestNumber + m_InteractiveTestRecord.Description + CommonConstants.BindingMessage + "[" + testCase.ToString() + "]";

            DisplaySelfTestVariableValues(m_InteractiveTestRecord);

            m_ToolStripInteractiveTestVCUCommands.Focus();
        }

        /// <summary>
        /// Process the special message.
        /// </summary>
        /// <param name="messageIdentifier">The message identifier returned from the call to the GetSelfTestResult() method.</param>
        internal void ProcessSTSpecialMessage(short messageIdentifier)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            if (MainWindow != null)
            {
                MainWindow.BlinkUpdateIcon();
            }

            // If the help window is on display then hide it.
            WinHlp32.HideHelpWindow(this.Handle.ToInt32());

            // Hide the self test variable panel.
            m_GroupBoxInteractiveTest.Visible = false;

            // Enable the DataGridViewControl.
            m_DataGridViewListResult.Enabled = true;

            // Disable the F6-Continue key.
            F6.Enabled = false;

            string statusMessage = string.Empty;
            SpecialMessageIdentifier specialMessageIdentifier = (SpecialMessageIdentifier)messageIdentifier;
            switch (specialMessageIdentifier)
            {
                case SpecialMessageIdentifier.TestComplete:
                    statusMessage = Resources.SMComplete;
                    break;
                case SpecialMessageIdentifier.TestAborted:
                    statusMessage = Resources.SMUserAbort;
                    break;
                case SpecialMessageIdentifier.ExitSelfTest:
                    statusMessage = Resources.SMLogicAbort;
                    break;
                default:
                    Debug.Assert(false, "FormViewTestResults.ProcessSTSpecialMessage() - [specialMessageIdentifier value - not supported.]");
                    break;
            }

            Escape.Enabled = true;

            // Disable the Abort key.
            F4.Enabled = false;

            // Update the status information.
            MainWindow.WriteStatusMessage(statusMessage);
            m_TestsActive = false;
            MainWindow.ShowBusyAnimation = false;

            SetEnabled(true);

            F3.Checked = false;
            Cursor = Cursors.Default;
        }
        #endregion - [Process Results] -

        /// <summary>
        /// Update the <c>DataGridView</c> used to display the tests defined in the active test list with the specified list.
        /// </summary>
        /// <param name="testList">A list of the test numbers associated with the active test list.</param>
        private void UpdateDataGridViewTestList(List<short> testList)
        {
            // Display the TabPage control that shows the tests defined in the active test list.
            m_TabControl.SelectedTab = m_TabPage1;

            // Clear the status message.
            MainWindow.WriteStatusMessage(string.Empty);

            // Clear the DataGridView controls.
            m_DataGridViewTestList.Rows.Clear();
            m_DataGridViewListResult.Rows.Clear();
            m_DataGridViewPassFailResult.Rows.Clear();

            // Add the test number and description associated with each test number contained in the specified list. 
            short testNumber;
            SelfTestRecord selfTestRecord;
            DataGridViewRow dataGridViewRow;
            Result_t testDefinition;
            for (int rowIndex = 0; rowIndex < testList.Count; rowIndex++)
            {
                testNumber = testList[rowIndex];

                // Get the self test record associated with the test number.
                try
                {
                    selfTestRecord = Lookup.SelfTestTableBySelfTestNumber.RecordList[testNumber];
                    if (selfTestRecord == null)
                    {
                        throw new Exception();
                    }

                    testDefinition = new Result_t();
                    testDefinition.Index = rowIndex;
                    testDefinition.TestNumber = testNumber;
                    testDefinition.TestCase = CommonConstants.NotDefined;
                    testDefinition.Passed = false;
                    testDefinition.PassCount = 0;
                    testDefinition.FailCount = 0;
                    testDefinition.ExecutionCount = 0;

                    dataGridViewRow = ConvertToDataGridViewRow(testDefinition);
                    m_DataGridViewTestList.Rows.Add(dataGridViewRow);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Convert the specified self test result to a <c>DataGridViewRow</c>.
        /// </summary>
        /// <param name="result">The latest result.</param>
        /// <returns>The specified self test result converted to a <c>DataGridViewRow</c>.</returns>
        protected DataGridViewRow ConvertToDataGridViewRow(Result_t result)
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return dataGridViewRow;
            }

            string resultText;
            if (result.Passed == true)
            {
                resultText = Resources.TextPassed;
            }
            else
            {
                resultText = Resources.TextFailed;
            }

            string testDescription, testNumberText;
            if (result.TestNumber <= 0)
            {
                result.TestNumber = 0;
                testNumberText = result.TestNumber.ToString();
                testDescription = CommonConstants.TestNotDefinedString;
            }
            else
            {
                testNumberText = result.TestNumber.ToString();
                try
                {
                    SelfTestRecord selfTestRecord = Lookup.SelfTestTableBySelfTestNumber.Items[result.TestNumber];
                    if (selfTestRecord == null)
                    {
                        throw new Exception();
                    }

                    testDescription = selfTestRecord.Description;
                }
                catch (Exception)
                {
                    testDescription = CommonConstants.TestNotDefinedString;
                }
            }

            dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.CreateCells(m_DataGridViewListResult, new object[] {
                                                                            (object)testNumberText,
                                                                            (object)result.TestCase.ToString(),
                                                                            (object)testDescription,
                                                                            (object)resultText,
                                                                            (object)result.PassCount.ToString(),
                                                                            (object)result.FailCount.ToString(),
                                                                            (object)result.ExecutionCount.ToString(CommonConstants.FormatStringNumeric)
                                                                            });
            return dataGridViewRow;
        }

        /// <summary>
        /// Convert the specified test list record to a generic list of test numbers.
        /// </summary>
        /// <param name="testListRecord">The test list record containing the list of tests that are to be executed.</param>
        private List<short> ConvertToTestList(TestListRecord testListRecord)
        {
            List<short> testList = new List<short>();

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return testList;
            }

            SelfTestRecord selfTestRecord;
            for (int testIndex = 0; testIndex < testListRecord.SelfTestRecordList.Count; testIndex++)
            {
                selfTestRecord = testListRecord.SelfTestRecordList[testIndex];
                testList.Add(selfTestRecord.SelfTestNumber);
            }

            return testList;
        }

        /// <summary>
        /// Add the pre-defined test lists defined in the <c>TESTLISTS</c> table of the data dictionary to the specified <c>ComboBox</c> control.
        /// </summary>
        /// <param name="comboBox">The <c>ComboBox</c> control that it to be processed.</param>
        private void ComboBoxAddTestLists(ref ComboBox comboBox)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Debug.Assert(comboBox != null, "FormViewTestResults.ComboBoxAddTestLists() - [comboBox != null]");

            comboBox.Items.Clear();

            // Get the pre-configured test lists.
            TestListRecord testListRecord;
            for (int testListIndex = 0; testListIndex < Lookup.TestListTable.RecordList.Count; testListIndex++)
            {
                if (Lookup.TestListTable.RecordList[testListIndex] != null)
                {
                    testListRecord = Lookup.TestListTable.RecordList[testListIndex];
                    comboBox.Items.Add(testListRecord);
                }
            }

            // Create the empty, user defined test list.
            m_UserDefinedTestListRecord = new TestListRecord();
            m_UserDefinedTestListRecord.Identifier = short.MaxValue;
            m_UserDefinedTestListRecord.HelpIndex = CommonConstants.NotDefined;
            m_UserDefinedTestListRecord.Description = Resources.TextUserDefined;
            m_UserDefinedTestListRecord.Attribute = 0;
            m_UserDefinedTestListRecord.SelfTestRecordList = new List<SelfTestRecord>();

            // Add the user defined test list.
            comboBox.Items.Add(m_UserDefinedTestListRecord);
        }

        /// <summary>
        /// Get the error message corresponding to the specified self test special message parameters.
        /// </summary>
        /// <param name="result">The result parameter of the self test special message.</param>
        /// <param name="reason">The reason parameter of the self test special message.</param>
        /// <returns>The self test error message corresponding to the specified self test special message parameters.</returns>
        private string GetSelfTestMessage(short result, short reason)
        {
            string message = string.Empty;

            if (result != ResultDefinedByDataDictionary)
            {
                message = Resources.EMResultFailed;
                return message;
            }
            else
            {
                message = Resources.EMResultFailed;
            }

            return message;
        }

        #region - [SelfTestVariables] -
        /// <summary>
        /// Configure the specified self test variable user controls. The individual controls are laid out on the panel similar to rows on a DataGridView control. The 
        /// first entry in the list, selfTestVariableList[0], is positioned at row 0, the second at row 1 etc. To configure all self test variables defined in the 
        /// list specify a start index of zero.
        /// </summary>
        /// <param name="panel">The panel to which the self test controls are to be added.</param>
        /// <param name="selfTestControlSize">The structure that is used to define the size of each self test variable user control.</param>
        /// <param name="selfTestVariableList">A list of the self test variables that are to be displayed.</param>
        protected void ConfigureSelfTestControls(Panel panel, VariableControlSize_t selfTestControlSize, List<SelfTestVariable> selfTestVariableList)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // Work out the spacing between consecutive rows.
            int rowSpacing = selfTestControlSize.Size.Height + selfTestControlSize.Margin.Vertical;

            panel.Controls.Clear();

            SelfTestControl selfTestVariableControl;
            short identifier;
            for (short index = 0; index < selfTestVariableList.Count; index++)
            {
                switch (selfTestVariableList[index].VariableType)
                {
                    case VariableType.Scalar:
                        selfTestVariableControl = new SelfTestScalarControl();
                        break;
                    case VariableType.Enumerator:
                        selfTestVariableControl = new SelfTestEnumeratorControl();
                        break;
                    case VariableType.Bitmask:
                        selfTestVariableControl = new SelfTestBitmaskControl();
                        break;
                    default:
                        selfTestVariableControl = new SelfTestControl();
                        break;
                }

                identifier = selfTestVariableList[index].Identifier;

                selfTestVariableControl.WidthVariableNameField = selfTestControlSize.WidthVariableNameField;
                selfTestVariableControl.WidthValueField = selfTestControlSize.WidthValueField;
                selfTestVariableControl.WidthUnitsField = selfTestControlSize.WidthUnitsField;

                selfTestVariableControl.ClientForm = this;
                selfTestVariableControl.TabIndex = 0;

                selfTestVariableControl.Location = new System.Drawing.Point(selfTestControlSize.Margin.Left, index * rowSpacing);

                selfTestVariableControl.ForeColorValueFieldZero = Color.ForestGreen;
                selfTestVariableControl.ForeColorValueFieldNonZero = Color.ForestGreen;

                selfTestVariableControl.Identifier = identifier;
                selfTestVariableControl.Value = 0;

                panel.Controls.Add(selfTestVariableControl);
            }
        }

        /// <summary>
        /// Display the self test variable values associated with the specified self test record.
        /// </summary>
        /// <param name="selfTestRecord">The self test record containing the self test variables that are to be displayed.</param>
        protected void DisplaySelfTestVariableValues(SelfTestRecord selfTestRecord)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            // --------------------------------------------------------------------------------------------------
            // Display the engineering values using the pre-configured user controls.
            // --------------------------------------------------------------------------------------------------
            for (int index = 0; index < selfTestRecord.SelfTestVariableList.Count; index++)
            {
                switch (selfTestRecord.SelfTestVariableList[index].VariableType)
                {
                    case VariableType.Scalar:
                        (m_PanelInteractiveTestVariableList.Controls[index] as SelfTestScalarControl).Value = (selfTestRecord.SelfTestVariableList[index].ValueFromTarget);
                        break;
                    case VariableType.Enumerator:
                        (m_PanelInteractiveTestVariableList.Controls[index] as SelfTestEnumeratorControl).Value = (selfTestRecord.SelfTestVariableList[index].ValueFromTarget);
                        break;
                    case VariableType.Bitmask:
                        (m_PanelInteractiveTestVariableList.Controls[index] as SelfTestBitmaskControl).Value = (selfTestRecord.SelfTestVariableList[index].ValueFromTarget);
                        break;
                    default:
                        Debug.Assert(false, "FormViewTestResults.DisplaySelfTestVariableValues() - [selfTestRecord.SelfTestVariableList[index].VariableType value - not supported.]");
                        break;
                }
            }
        }

        /// <summary>
        /// <para>
        /// Convert the UInt32 value represented by the <paramref name="value"/> parameter to the value associated with the specified data type.
        /// </para>
        /// <para>
        /// All self test variable values are passed back from PTUDLL32.dll as UInt32 values converted to double format. In order to display the correct value this 
        /// UInt32 value must be converted to the value associated with the data type of the variable, as defined by the <c>DataType</c> property. For example, if the 
        /// data type is of type Int16 bit 15 of the UInt32 value represents the sign bit, for Int32 types bit 31 represents the sign bit etc.
        /// </para>
        /// </summary>
        /// <remarks>The negative value equivalent of a positive number and vice-versa is represented by the two's complement of that number i.e. all bits are inverted 
        /// and 1 is added to the result. E.g. if the UInt value is 0x0000 0003 i.e 3, -3 is represented by 0xFFFF FFFD</remarks>
        /// <returns>The UInt32 value converted to the specified data type and cast to a double.</returns>
        private double ConvertUInt32(double value, DataType_e dataType)
        {
            double convertedValue = 0;

            switch ((DataType_e)dataType)
            {
                case DataType_e.u08:
                case DataType_e.u16:
                case DataType_e.u32:
                    // No conversion is necessary for unsigned values.
                    convertedValue = value;
                    break;
                case DataType_e.i08:
                    if (value < Math2Power7)
                    {
                        // No conversion is necessary as the sign bit is not asserted.
                        convertedValue = value;
                    }
                    else
                    {
                        // Convert to a signed value.
                        convertedValue = -Math2Power32 + value;
                    }
                    break;
                case DataType_e.i16:
                    if (value < Math2Power15)
                    {
                        // No conversion is necessary as the sign bit is not asserted.
                        convertedValue = value;
                    }
                    else
                    {
                        // Convert to a signed value.
                        convertedValue = -Math2Power32 + value;
                    }
                    break;
                case DataType_e.i32:
                    if (value < Math2Power31)
                    {
                        // No conversion is necessary as the sign bit is not asserted.
                        convertedValue = value;
                    }
                    else
                    {
                        // Convert to a signed value.
                        convertedValue = -Math2Power32 + value;
                    }
                    break;
                default:
                    // Unknown type.
                    convertedValue = value;
                    break;
            }

            return convertedValue;
        }
        #endregion - [SelfTestVariables] -

        #region - [Help] -
        /// <summary>
        /// Show the Windows help window associated with the test definition for the selected row of the specified <c>DataGridView</c> control.
        /// </summary>
        /// <remarks>If multiple rows are selected, only the first row that was selected will be processed.</remarks>
        /// <param name="dataGridView">The <c>DataGridView</c> control that currently has focus.</param>
        /// <param name="panel">Reference to the <c>Panel</c> where the Windows help window is to be displayed.</param>
        private void ShowTestDefinition(DataGridView dataGridView, Panel panel)
        {
            // Check that at least one row has been selected.
            if (dataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            DataGridViewRow dataGridViewRow = dataGridView.SelectedRows[0];
            try
            {
                // Get the self test record corresponding to the selected row.
                short testNumber = short.Parse((string)dataGridViewRow.Cells[ColumnIndexSelfTestNumber].Value);
                Debug.Assert(testNumber >= 0, "FormViewTestResult.ShowHelp() - [testNumber >= 0]");

                // Find the self test record corresponding to the test number.
                SelfTestRecord selfTestRecord = Lookup.SelfTestTableBySelfTestNumber.Items[testNumber];
                if (selfTestRecord == null)
                {
                    throw new Exception();
                }

                if (selfTestRecord.HelpIndex != CommonConstants.NotDefined)
                {
                    WinHlp32.ShowHelpWindow(panel.Handle.ToInt32(), selfTestRecord.HelpIndex, WinHlp32.HWND_TOP);
                }
            }
            catch (Exception)
            {
                // Cannot show help information associated with this row.
            }
        }

        /// <summary>
        /// Show the Windows help window for the test message associated with the selected row of the specified <c>DataGridView</c> - if it is avaialable.
        /// </summary>
        /// <param name="dataGridView">The <c>DataGridView</c> control for which the test case help is to be shown.</param>
        /// <param name="panel">Reference to the <c>Panel</c> where the Windows help window is to be displayed.</param>
        /// <remarks>If multiple rows are selected, only the first row that was selected will be processed.</remarks>
        /// <returns>A flag to indicate whether the test message was displayed.</returns>
        private bool ShowTestMessage(DataGridView dataGridView, Panel panel)
        {
            bool isAvailable = false;

            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return isAvailable;
            }

            // Check that at least one row has been selected.
            if (dataGridView.SelectedRows.Count <= 0)
            {
                return isAvailable;
            }

            DataGridViewRow dataGridViewRow = dataGridView.SelectedRows[0];

            // Only display the test message if the test failed.
            string resultText = (string)dataGridViewRow.Cells[ColumnIndexResult].Value;
            if (resultText.Equals(Resources.TextFailed))
            {
                try
                {
                    // Get the test case and test number associated with the selected test.
                    short testNumber = short.Parse((string)dataGridViewRow.Cells[ColumnIndexSelfTestNumber].Value);
                    short testCase = short.Parse((string)dataGridViewRow.Cells[ColumnIndexTestCase].Value);

                    // Check that both values are greater than or equal to 1.
                    if ((testNumber < 1) || (testCase < 1))
                    {
                        throw new ArgumentException();
                    }

                    int helpIndex = Lookup.SelfTestTable.GetTestMessageHelpIndex(testNumber, testCase);

                    // If the help index for the record is defined, show the help information.
                    if (helpIndex != CommonConstants.NotDefined)
                    {
                        WinHlp32.ShowHelpWindow(panel.Handle.ToInt32(), helpIndex, WinHlp32.HWND_TOP);
                    }

                    isAvailable = true;
                }
                catch (Exception)
                {
                    // Cannot show help information associated with this row.
                }
            }

            return isAvailable;
        }
        #endregion - [Help] -

        #region - [VCU Commands] -
        /// <summary>
        /// Send the command to execute the self tests associated with the specified array of self test numbers.
        /// </summary>
        /// <param name="testList">The list of self test numbers associated with the tests that are to be executed.</param>
        /// <exception cref="InvalidOperationException">Thrown if the VCU cannot enter self test mode for some reason.</exception>
        /// <exception cref="CommunicationException">Thrown if a valid reply is not received from the VCU in response to a command request.</exception>
        private void ExecuteSelfTests(List<short> testList)
        {
            // Skip, if the Dispose() method has been called.
            if (IsDisposed)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Ensure that the DataGridView controls and associated lists are cleared.
            m_DataGridViewPassFailResult.Rows.Clear();
            m_PassFailResultList.Clear();

            m_DataGridViewListResult.Rows.Clear();
            m_ListResultList.Clear();

            if (m_CheckBoxLoopForever.Checked == true)
            {
                m_LoopCount = LoopForever;
            }
            else
            {
                m_LoopCount = (short)m_NumericUpDownLoopCount.Value;
            }

            try
            {
                CommunicationInterface.UpdateSTMode(SelfTestMode.Engineering);
                MainWindow.BlinkUpdateIcon();
            }
            catch (CommunicationException)
            {
                throw new CommunicationException(Resources.EMUpdateSTModeFailed);
            }
            Application.DoEvents();

            short[] selectedTests;
            selectedTests = testList.ToArray();
            try
            {
                CommunicationInterface.UpdateSTTestList((short)selectedTests.Length, selectedTests);
                MainWindow.BlinkUpdateIcon();
            }
            catch (CommunicationException)
            {
                throw new CommunicationException(Resources.EMUpdateSTTestListFailed);
            }
            Application.DoEvents();

            try
            {
                CommunicationInterface.UpdateSTLoopCount(m_LoopCount);
                MainWindow.BlinkUpdateIcon();
            }
            catch (CommunicationException)
            {
                throw new CommunicationException(Resources.EMUpdateSTLoopCountFailed);
            }
            Application.DoEvents();

            try
            {
                CommunicationInterface.ExecuteSTTestList(TruckInformation.XY);
                MainWindow.BlinkUpdateIcon();
            }
            catch (CommunicationException)
            {
                throw new CommunicationException(Resources.EMExecuteSTTestListFailed);
            }
            Application.DoEvents();

            // Enable the 'Abort' function key.
            F4.Enabled = true;

            // Update the status information.
            MainWindow.WriteStatusMessage(Resources.SMRunning);
            m_TestsActive = true;
            MainWindow.ShowBusyAnimation = true;
        }

        /// <summary>
        /// Send the command to start the self test task.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the result returned from the call is not ResultSuccess.</exception>
        private void StartSelfTestTask()
        {
            short result, reason;
            try
            {
                CommunicationInterface.StartSelfTestTask(out result, out reason);
                MainWindow.BlinkUpdateIcon();
                if (result != ResultSuccess)
                {
                    throw new InvalidOperationException(GetSelfTestMessage(result, reason));
                }

                MainWindow.SetMode(Mode.SelfTest);
            }
            catch (CommunicationException)
            {
                throw new CommunicationException(Resources.EMStartSelfestTaskFailed);
            }
        }

        /// <summary>
        /// Send the command to abort the current test run.
        /// </summary>
        private void AbortSelfTests()
        {
            try
            {
                CommunicationInterface.AbortSTSequence();
                MainWindow.BlinkUpdateIcon();
            }
            catch (CommunicationException communicationException)
            {
                MessageBox.Show(Resources.MBTAbortSTSequenceFailed + CommonConstants.NewLine + CommonConstants.NewLine +
                                communicationException.CommunicationError, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Send the acknowledge command.
        /// </summary>
        private void SendOperatorAcknowledge()
        {
            try
            {
                CommunicationInterface.SendOperatorAcknowledge();
                MainWindow.BlinkUpdateIcon();
            }
            catch (CommunicationException communicationException)
            {
                MessageBox.Show(Resources.MBTSendOperatorAcknowledgeFailed + CommonConstants.NewLine + CommonConstants.NewLine +
                                communicationException.CommunicationError, Resources.MBCaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Ensure that the help window is closed.
            WinHlp32.HideHelpWindow(this.Handle.ToInt32());
        }

        /// <summary>
        /// Issue the command to exit self test mode.
        /// </summary>
        private void ExitSelfTestTask()
        {
            short result, reason;
            try
            {
                CommunicationInterface.ExitSelfTestTask(out result, out reason);
                MainWindow.BlinkUpdateIcon();

                // Show the user that the VCU has exited from self test mode.
                if (m_IsOnline == true)
                {
                    MainWindow.SetMode(Mode.Online);
                }
                else
                {
                    MainWindow.SetMode(Mode.Offline);
                }
            }
            catch (CommunicationException)
            {
                // Ensure that an exception isn't thrown, then drop into Offline mode since we've been unable to communicate.
				MainWindow.SetMode(Mode.Offline);
            }
        }
        #endregion - [VCU Commands] -
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the communication interface that is to be used to communicate with the VCU.
        /// </summary>
        public ICommunicationSelfTest CommunicationInterface
        {
            get { return m_CommunicationInterface; }
            set { m_CommunicationInterface = value; }
        }

        /// <summary>
        /// Gets or sets the active test list record.
        /// </summary>
        public TestListRecord TestListRecord
        {
            get { return m_TestListRecord; }
            set
            {
                m_TestListRecord = value;

                // Only allow the user to edit the test list if it is the user defined test list.
                if (m_TestListRecord.Identifier == short.MaxValue)
                {
                    m_UserDefinedTestListRecord = m_TestListRecord;
                }

                m_TestList = ConvertToTestList(m_TestListRecord);
                UpdateDataGridViewTestList(m_TestList);

                // ----------------------------------------------------------
                // Display the name of the test list on the ComboBox control.
                // ----------------------------------------------------------
                // Ensure that the SelectionChanged event is not triggered as a result of specifying the Text property of the ComboBox control.
                m_ComboBoxTestList.SelectedIndexChanged -= new EventHandler(m_ComboBoxTestList_SelectedIndexChanged);
                m_ComboBoxTestList.Text = m_TestListRecord.Description;
                m_ComboBoxTestList.SelectedIndexChanged += new EventHandler(m_ComboBoxTestList_SelectedIndexChanged);
            }
        }
        #endregion --- Properties ---
    }
}
