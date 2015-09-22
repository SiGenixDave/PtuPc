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
 *  File name:  WorksetCollection.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  11/17/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  11/26/10    1.1     K.McD           1.  Added the WorksetCollectionType enumerator.
 *                                      2.  Renamed a number of variables and structures.
 *                                      3.  Added the WoksetCollectionType, EntryCountMax and ColumnCountMax variables to the WorksetCollectionFile_t structure.
 *                                      4.  Added the CountMax, SecurityLevel and SampleMultiple variables to the Workset_t structure.
 *                                      5.  Bug fix - SNCR001.60. Added the Replicate() method to the Workset_t structure to produce an independent copy of a workset.
 *                                      6.  Added the ColumnCountMax, EntryCountMax and WorksetCollectionType properties to the WorksetCollection class.
 *                                      7.  Added a number of additional constructor signatures to the WorksetCollection class.
 * 
 *  12/31/10    1.2     K.McD           1.  Added the GetWatchIdentifierLocation() method.
 *                                      2.  Modified the CreateWatchItems() method of the Workset_t structure to use the currentDataDictionaryRecordCount parameter.
 *                                      3.  Modified the Replicate() method of the Workset_t structure to use the length of the WatchItems property of the workset 
 *                                          that is to be copied rather than the number of records in the current data dictionary when initializing the WatchItems variable.
 *                                      4.  Modified the Load() method so that it attempts to update the WatchItems property of all worksets in the de-serialized workset 
 *                                          file if the workset is not compatible with the current data dictionary.
 *                                      5.  Added the UpdateWatchItems() method.
 *                                      6.  Modified the CreateBaselineWorkset() so that it adds the first EntryCountMax watch identifiers that are contained within 
 *                                          the data dictionary.
 *                                      7.  Replaced the ValidateWorksetFile() method with CheckCompatibility() method.
 *                                      8.  Removed the ActiveWorksetModified event and replaced it with the WorksetCollectionModified event, this event is raised whenever 
 *                                          any element of the workset collection is modified.
 * 
 *  03/28/11    1.3     K.McD           1.  Modified a number of XML tags.
 *                                      2.  Modified the names of a number of parameters and methods.
 *                                      3.  SNCR001.112. Major modifications throughout to use the old identifier field rather than the watch identifier field to 
 *                                          record the watch variables that are to be included in the workset as these remain consistent through a data 
 *                                          dictionary upgrade whereas the watch identifier fields do not.
 * 
 *  04/27/11    1.4     K.McD           1.  Changed the modifier of the CreateWatchItems() method of the Workset_t structure to public.
 * 
 *  05/16/11    1.5     K.McD           1.  Included the ChartScale_t structure to store the scaling information associated with a chart recorder channel.
 *                                      2.  Added a list of ChartScale_t structures to the Column_t structure to record the scaling information associated with 
 *                                          each channel of the chart recorder workset.
 *                                      3.  Modified the Replicate() method of the Workset_t structure to include the chart recorder scaling information.
 *                                      4.  Modified the constructor of the Workset_t structure to initialize the chart scaling information for the specified 
 *                                          list of watch variables to the values defined in the data dictionary.
 *                                      5.  Applied the 'Organize Usings/Remove and Sort' context menu.
 *                                      
 *  06/22/11    1.6     K.McD           1.  Modified one or more XML tags.
 *  
 *	09/26/11	1.7 	Sean.D			1.	Added a Contains() method to check whether the workset collection contains a workset with the specified name.
 *										2.	Modified the Add() method so that it only adds non-duplicate entries and returns a bool regarding
 *											success.
 *									
 *  10/02/11    1.8     K.McD           1.  Added support to allow the user to modify the plot layout of saved watch data files.
 *									
 *  10/12/11    1.8.1	Sean.D			1.  Made the CreateBaselineWorkset() method public so that it can be used to create a new baseline workset when all existing 
 *                                          worksets contain more watch variables than are currently supported.
 *										2.	Modified CheckCompatibility() to check whether the workset has an upper limit of watch variables different from the data dictionary.
 *										3.	Modified Update() to set the correct upper limit watch size according to the data dictionary and notify the user if doing so will potentially
 *											lose data.
 *											
 *  10/24/11    1.8.2   K.McD           1.  Added a ThreadSleep() statement to the CheckCompatibility() method after the call to the De-Serialize() method to give the 
 *                                          file time to close properly.
 *                                          
 *                                      2.  Moved the error reporting from the CheckCompatibility() method to the update method. Also included consistency checking 
 *                                          on the CountMax parameter of the individual worksets within the collection.
 *                                                  
 *                                      3.  Removed the 'List<short> invalidOldIdentifierList' parameter from the signature of the Load() and CheckCompatibility() 
 *                                          methods. Error reporting is now carried out directly within the Update() method rather than passing the information 
 *                                          back through the call stack.
 *                                          
 *                                      4.  Refactored the error reporting. Created the GetIncompatibleOldIdentifierList(), GetIncompatibleWorksetList and 
 *                                          ReportIncompatibleWorksets() methods.
 *                                          
 *                                      5.  Major modifications to the Update() method.
 *                                              (a) Now responsble for reporting workset incompatibilities with the current data dictionary.
 *                                              (b) Now updates the EntryCountMax field of the workset collection and the CountMax fields of individual worksets within 
 *                                                  the collection with the values appropriate to the current data dictionary.
 *                                              (c) Now updates the baseline workset to be compatible with the current data dictionary.                                     
 */

#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

using Common.Properties;

namespace Common.Configuration
{
    #region --- Enumerators ---
    /// <summary>
    /// The type of workset collection.
    /// </summary>
    public enum WorksetCollectionType
    {
        /// <summary>
        /// The workset collection defining the watch variables that are to be displayed and recorded when viewing watch data.
        /// </summary>
        RecordedWatch,

        /// <summary>
        /// The workset collection defining the stream parameters, i.e. watch variables, that are to be recorded within a fault log data stream.
        /// </summary>
        FaultLog,

        /// <summary>
        /// The workset defining the watch variables that are to be recorded by the chart recorder.
        /// </summary>
        Chart,

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined
    }
    #endregion --- Enumerators ---

    #region --- Structures ---
    /// <summary>
    /// The file structure that is serialized to disk.
    /// </summary>
    [Serializable]
    public struct WorksetCollectionFile_t
    {
        /// <summary>
        /// The project information associated with the worksets.
        /// </summary>
        public DataDictionaryInformation_t ProjectInformation;

        /// <summary>
        /// A list of the worksets associated with the project.
        /// </summary>
        public List<Workset_t> WorksetList;

        /// <summary>
        /// The index of the active workset.
        /// </summary>
        public int ActiveIndex;

        /// <summary>
        /// The index of the default workset.
        /// </summary>
        public int DefaultIndex;

        /// <summary>
        /// The type of workset collection e.g. Watch, FaultLog, Chart, SelfTest.
        /// </summary>
        public WorksetCollectionType WorksetCollectionType;

        /// <summary>
        /// The maximum number of entries that each workset can support.
        /// </summary>
        public short EntryCountMax;

        /// <summary>
        /// The maximum number of display columns that each workset can support.
        /// </summary>
        public short ColumnCountMax;
    }

    /// <summary>
    /// The workset definition.
    /// </summary>
    [Serializable]
    public struct Workset_t
    {
        #region --- Constants ---
        /// <summary>
        /// The default multiple of the recording interval at which the data is recorded. 
        /// </summary>
        /// <remarks>
        /// This is only relevant to the fault log worksets, however, it must be initialized in all worksets.
        /// </remarks>
        public const short DefaultSampleMultiple = 1;
        #endregion --- Constants ---

        #region - [Member Variables] -
        /// <summary>
        /// The name of the workset.
        /// </summary>
        public string Name;

        /// <summary>
        /// The number of watch variables that are included in the workset.
        /// </summary>
        public int Count;

        /// <summary>
        /// A list of the watch identifiers corresponding to the watch elements that are to be processed by the VCU when displaying the current workset.
        /// </summary>
        /// <remarks>The watch elements correspond to the block of watch variables that are retrieved from the target hardware.</remarks>
        public List<short> WatchElementList;

        /// <summary>
        /// An array used to determine which watch variables have been added to the workset and whether they exist in the current data dictionary.
        /// </summary>
        public WatchItem_t[] WatchItems;

        /// <summary>
        /// An array containing the information required to construct each column of the real time/replay display when viewing a workset.
        /// </summary>
        public Column_t[] Column;

        /// <summary>
        /// An array containing the information required to construct each tab page of the plot display when plotting a workset.
        /// </summary>
        /// <remarks>This variable is included to allow the user to modify the order in which the watch variables associated with saved data are plotted. As such, 
        /// the contents of this structure are not checked when comparing worksets. Whenever the definition of the workset is modified the definitions conatined 
        /// in this variable will be initialized to the values defined in the 'Column' array.</remarks>
        public PlotTabPage_t[] PlotTabPages;

        /// <summary>
        /// The maximum number of watch variables supported by the workset.
        /// </summary>
        public int CountMax;

        /// <summary>
        /// The security level associated with the workset.
        /// </summary>
        public SecurityLevel SecurityLevel;

        /// <summary>
        /// The multiple of the recording interval at which the data is recorded. 
        /// </summary>
        /// <remarks>
        /// This is only relevant to the fault log worksets.
        /// </remarks>
        public short SampleMultiple;
        #endregion - [Member Variables] -

        #region - [Constructors] -
        /// <summary>
        /// Initialize a new instance of the structure. Creates a new workset based upon the specified name and list of watch identifiers.
        /// </summary>
        /// <param name="name">The name of the workset.</param>
        /// <param name="watchIdentifierList">The list of watch identifiers that are to be used to initialize the workset.</param>
        /// <param name="entryCountMax">The maximum number of entries that the workset can support.</param>
        /// <param name="columnCountMax">The maximum number of display columns that the workset can support.</param>
        /// <param name="securityLevel">The security level associated with the workset.</param>
        /// <remarks>
        /// All watch identifiers contained within the specified list will appear in the first column of the workset in the order that they appear in the list. The 
        /// watch element list is sorted by watch identifier value in ascending order.
        /// </remarks>
        public Workset_t(string name, List<short> watchIdentifierList, short entryCountMax, short columnCountMax, SecurityLevel securityLevel)
        {
            Debug.Assert(name != string.Empty, "Workset_t.Ctor() - [name != string.Empty]");
            Debug.Assert(watchIdentifierList != null, "Workset_t.Ctor() - [watchElementList != null]");
            Debug.Assert((watchIdentifierList.Count > 0), "Workset_t.Ctor() - [watchElementList.Count > 0");

            Name = name;
            SampleMultiple = DefaultSampleMultiple;
            CountMax = entryCountMax;
            SecurityLevel = securityLevel;

            // Create the WatchElementList property.
            WatchElementList = new List<short>();
            short watchIdentifier;
            for (int watchIdentifierIndex = 0; watchIdentifierIndex < watchIdentifierList.Count; watchIdentifierIndex++)
            {
                watchIdentifier = watchIdentifierList[watchIdentifierIndex];
                WatchElementList.Add(watchIdentifier);
            }

            WatchElementList.Sort();
            Count = WatchElementList.Count;

            #region - [Column] -
            Column = new Column_t[columnCountMax];
            for (int columnIndex = 0; columnIndex < columnCountMax; columnIndex++)
            {
                Column[columnIndex] = new Column_t();
                Column[columnIndex].HeaderText = (columnIndex == 0) ? Resources.LegendColumn + CommonConstants.Space + (columnIndex + 1).ToString() : string.Empty;
                Column[columnIndex].OldIdentifierList = new List<short>();
                Column[columnIndex].ChartScaleList = new List<ChartScale_t>();
            }

            // Add the old identifier values of the watch variables defined in the watch element list to the first column of the workset.
            WatchVariable watchVariable;
            ChartScale_t chartScale;
            for (short watchIdentifierIndex = 0; watchIdentifierIndex < WatchElementList.Count; watchIdentifierIndex++)
            {
                chartScale = new ChartScale_t();
               
                try
                {
                    watchVariable = Lookup.WatchVariableTable.Items[watchIdentifierList[watchIdentifierIndex]];
                    if (watchVariable == null)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Column[0].OldIdentifierList.Add(watchVariable.OldIdentifier);

                        // Set up the default chart scaling from the values in the data dictionary.
                        chartScale.ChartScaleUpperLimit = watchVariable.MaxChartScale;
                        chartScale.ChartScaleLowerLimit = watchVariable.MinChartScale;
                        chartScale.Units = watchVariable.Units;
                        Column[0].ChartScaleList.Add(chartScale);
                    }
                }
                catch (Exception)
                {
                    Column[0].OldIdentifierList.Add(CommonConstants.OldIdentifierNotDefined);

                    // Set the chart scaling values to represent an invalid entry.
                    chartScale.ChartScaleLowerLimit = double.NaN;
                    chartScale.ChartScaleUpperLimit = double.NaN;
                    chartScale.Units = CommonConstants.ChartScaleUnitsNotDefinedString;
                    Column[0].ChartScaleList.Add(chartScale);
                }
            }
            #endregion - [Column] -

            // This attribute is used to define the plot screen layout and is defined by the user when displaying the saved data file.
            PlotTabPages = null;

            WatchItems = new WatchItem_t[Lookup.WatchVariableTableByOldIdentifier.Items.Length];

            // Create the WatchItems property from the list of watch elements.
            WatchItems = CreateWatchItems(WatchElementList, Lookup.WatchVariableTableByOldIdentifier.Items.Length);
        }
        #endregion - [Constructors] -

        #region - [Methods] -
        /// <summary>
        /// Create an array of watch items based upon the specified list of watch elements.
        /// </summary>
        /// <param name="watchElementList">The list containing the watch elements.</param>
        /// <param name="oldIdentifierCount">The number of entries in the old identifier lookup table associated with the current data dictionary.</param>
        /// <returns>The array of watch items showing which watch variables have been added to the workset.</returns>
        public WatchItem_t[] CreateWatchItems(List<short> watchElementList, int oldIdentifierCount)
        {
            WatchItem_t[] watchItems = new WatchItem_t[oldIdentifierCount];
            WatchItem_t watchItem;
            WatchVariable watchVariable;

            // Create an array of watch items where the Added property of each watch item is set to false.
            for (short oldIdentifier = 0; oldIdentifier < oldIdentifierCount; oldIdentifier++)
            {
                watchItem = new WatchItem_t();
                watchItem.OldIdentifier = oldIdentifier;
                watchItem.Added = false;

                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                    if (watchVariable == null)
                    {
                        watchItem.Exists = false;
                    }
                    else
                    {
                        watchItem.Exists = true;
                    }
                }
                catch(Exception)
                {
                    watchItem.Exists = false;
                }

                watchItems[oldIdentifier] = watchItem;
            }

            // Now update the Added property to reflect the list of watch elements.
            for (short watchElementIndex = 0; watchElementIndex < WatchElementList.Count; watchElementIndex++)
            {
                try
                {
                    // Get the old identifier corresponding to the watch element and update the appropriate element of the WatchItems array.
                    watchVariable = Lookup.WatchVariableTable.Items[watchElementList[watchElementIndex]];
                    if (watchVariable == null)
                    {
                        continue;
                    }

                    watchItems[watchVariable.OldIdentifier].Added = true;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return watchItems;
        }

        /// <summary>
        /// Returns the name of the workset.
        /// </summary>
        /// <returns>The name of the workset.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Replicate the specified workset i.e. produce a copy of the workset that is completely independent of the original.
        /// </summary>
        /// <param name="workset">The workset that is to be copied.</param>
        public void Replicate(Workset_t workset)
        {
            Name = workset.Name;
            SampleMultiple = workset.SampleMultiple;
            CountMax = workset.CountMax;
            SecurityLevel = workset.SecurityLevel;

            // Copy WatchElementList.
            WatchElementList = new List<short>();
            short watchIdentifier;
            for (int watchElementIndex = 0; watchElementIndex < workset.WatchElementList.Count; watchElementIndex++)
            {
                watchIdentifier = workset.WatchElementList[watchElementIndex];
                WatchElementList.Add(watchIdentifier);
            }
            Count = WatchElementList.Count;

            #region - [Column] -
            Column = new Column_t[workset.Column.Length];
            for (int columnIndex = 0; columnIndex < workset.Column.Length; columnIndex++)
            {
                Column[columnIndex] = new Column_t();
                Column[columnIndex].HeaderText = workset.Column[columnIndex].HeaderText;
                Column[columnIndex].OldIdentifierList = new List<short>();

                short oldIdentifier;
                for (int rowIndex = 0; rowIndex < workset.Column[columnIndex].OldIdentifierList.Count; rowIndex++)
                {
                    oldIdentifier = workset.Column[columnIndex].OldIdentifierList[rowIndex];
                    Column[columnIndex].OldIdentifierList.Add(oldIdentifier);
                }

                // Only replicate the chart recorder scaling information if it has been defined.
                if (workset.Column[columnIndex].ChartScaleList == null)
                {
                    Column[columnIndex].ChartScaleList = null;
                }
                else
                {
                    Column[columnIndex].ChartScaleList = new List<ChartScale_t>();
                    ChartScale_t chartScale;
                    for (int rowIndex = 0; rowIndex < workset.Column[columnIndex].ChartScaleList.Count; rowIndex++)
                    {
                        chartScale = workset.Column[columnIndex].ChartScaleList[rowIndex];
                        Column[columnIndex].ChartScaleList.Add(chartScale);
                    }
                }
            }
            #endregion - [Column] -

            #region - [PlotTabPages] -
            // Only replicate the tab page plot information if it has been defined.
            if (workset.PlotTabPages == null)
            {
                PlotTabPages = null;
            }
            else
            {
                PlotTabPages = new PlotTabPage_t[workset.PlotTabPages.Length];

                // Copy the TabPagePlot information to the new array.
                Array.Copy(workset.PlotTabPages, PlotTabPages, workset.PlotTabPages.Length);
            }
            #endregion - [PlotTabPages] -

            WatchItems = new WatchItem_t[workset.WatchItems.Length];

            // Copy the WatchItems property to the new array.
            Array.Copy(workset.WatchItems, WatchItems, WatchItems.Length);
        }

        /// <summary>
        /// Get the column and row index where the watch variable associated with the specified old identifier is located.
        /// </summary>
        /// <remarks>
        /// If the specified watch variable cannot be found, the <c>columnIndex</c> and <c>rowIndex</c> output parameters are set to 'NotFound'.
        /// </remarks>
        /// <param name="oldIdentifier">The watch variable old identifier.</param>
        /// <param name="columnIndex">The column index associated with the specified watch variable.</param>
        /// <param name="rowIndex">The row index associated with the specified watch variable.</param>
        public void GetWatchVariableLocation(short oldIdentifier, out short columnIndex, out short rowIndex)
        {
            for (columnIndex = 0; columnIndex < Column.Length; columnIndex++)
            {
                for (rowIndex = 0; rowIndex < Column[columnIndex].OldIdentifierList.Count; rowIndex++)
                {
                    if (Column[columnIndex].OldIdentifierList[rowIndex] == oldIdentifier)
                    {
                        return;
                    }
                }
            }
            columnIndex = CommonConstants.NotFound;
            rowIndex = CommonConstants.NotFound;
        }
        #endregion - [Methods] -
    }

    /// <summary>
    /// An individual watch variable list item. Used when adding watch variables to controls such as <c>ListBox</c> controls etc.
    /// </summary>
    [Serializable]
    public struct WatchItem_t
    {
        /// <summary>
        /// The old identifier of the watch variable.
        /// </summary>
        public short OldIdentifier;

        /// <summary>
        /// The display mask that is to be applied to bitmask watch variables.
        /// </summary>
        /// <remarks>This is only applicable to bitmask watch variables and is only used when defining the plot screen layout.</remarks>
        public uint DisplayMask;

        /// <summary>
        /// Flag to indicate whether the watch variable has been added to the workset. True, indicates that the the watch variable has been added to the 
        /// workset; otherwise, false.
        /// </summary>
        public bool Added;

        /// <summary>
        /// Flag to indicate whether the watch variable exists in the current data dictionary. True, indicates that the watch variable exists; otherwise, false.
        /// </summary>
        public bool Exists;

        /// <summary>
        /// Overrides the ToString() method to return the name/description associated with the watch variable.
        /// </summary>
        /// <returns>The name/description of the watch variable.</returns>
        public override string ToString()
        {
            try
            {
                WatchVariable watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[OldIdentifier];
                if (watchVariable == null)
                {
                    return CommonConstants.VariableNotDefinedString;
                }
                else
                {
                    return watchVariable.Name;
                }
            }
            catch (Exception)
            {
                return CommonConstants.VariableNotDefinedString;
            }
        }
    }

    /// <summary>
    /// A structure that defines the header, old identifiers and chart scale values associated with an individual column of the real time or replay display.
    /// </summary>
    [Serializable]
    public struct Column_t
    {
        /// <summary>
        /// The text that is to be displayed in the column header.
        /// </summary>
        public string HeaderText;

        /// <summary>
        /// A list of the watch variable old identifiers that are to be displayed in the workset column. The old identifiers are used as these remain fixed between 
        /// iterations of the data dictionary in the event that watch variables are removed.
        /// </summary>
        public List<short> OldIdentifierList;

        /// <summary>
        /// A list of the chart scaling information associated with each channel of the chart recorder.
        /// </summary>
        public List<ChartScale_t> ChartScaleList; 
    }

    /// <summary>
    /// A structure that defines the header, old identifiers and display masks associated with an individual tab page of the plot display.
    /// </summary>
    [Serializable]
    public struct PlotTabPage_t
    {
        /// <summary>
        /// The tab page header text.
        /// </summary>
        public string HeaderText;

        /// <summary>
        /// A list of the old identifiers corresponding to the watch variables that are to be displayed in the plot tab page. The old identifiers are used as these 
        /// remain fixed between iterations of the data dictionary in the event that watch variables are removed.
        /// </summary>
        public List<short> OldIdentifierList;

        /// <summary>
        /// The display mask that is to be applied when plotting Bitmask watch variables. For scalar and enumerator watch values the value of the mask will be 
        /// 0xFFFFFFFF, however, for bitmask watch variables the corresponding bit will be set if the plot associated with the bit is to be plotted; otherwise it 
        /// will be clear.
        /// </summary>
        public List<uint> DisplayMaskList;
    }

    /// <summary>
    /// Scaling information for the chart recorder channel.
    /// </summary>
    [Serializable]
    public struct ChartScale_t
    {
        /// <summary>
        /// The upper limit of the Y axis associated with the chart recorder channel.
        /// </summary>
        public double ChartScaleUpperLimit;

        /// <summary>
        /// The lower limit of the Y axis associated with the chart recorder channel.
        /// </summary>
        public double ChartScaleLowerLimit;

        /// <summary>
        /// The engineering units associated with the chart recorder channel.
        /// </summary>
        public string Units;
    }
    #endregion --- Structures ---

    /// <summary>
    /// A class to manage the collection of worksets associated with individual sub-systems.
    /// </summary>
    [Serializable]
    public class WorksetCollection
    {
        #region --- Constants ---
        /// <summary>
        /// The sleep period, in ms, after the workset file is de-serialized to allow the file to close. 
        /// </summary>
        private const int SleepDeSerialize = 100;
        #endregion --- Constants ---

        #region --- Events ---
        /// <summary>
        /// Raised whenever the workset collection is modified.
        /// </summary>
        public event EventHandler WorksetCollectionModified;
        #endregion --- Events ---

        #region --- Member Variables ---
        /// <summary>
        /// The workset file structure that is serialized to disk.
        /// </summary>
        private WorksetCollectionFile_t m_WorksetCollectionFile_t;

        /// <summary>
        /// The file name of the file containing the workset definitions.
        /// </summary>
        private string m_Filename = string.Empty;
        #endregion -- Member Variables ---

        #region --- Constructors ---
        /// <summary>
        /// Initializes a new instance of the class. Call the Initialize() static method.
        /// </summary>
        /// <param name="worksetCollectionType">The type of workset collection.</param>
        /// <param name="entryCountMax">The maximum number of entries that each workset can support.</param>
        /// <param name="columnCountMax">The maximum number of display columns that each workset can support.</param>
        public WorksetCollection(WorksetCollectionType worksetCollectionType, short entryCountMax, short columnCountMax)
        {
            Initialize(worksetCollectionType, entryCountMax, columnCountMax);
        }
        #endregion --- Constructors ---

        #region --- Methods ---
        /// <summary>
        /// Initializes a new instance of the class.
        /// <list type="number">
        /// <item><description>Clear the existing list of worksets.</description></item>
        /// <item><description>Create a baseline workset and add this to the list of worksets.</description></item>
        /// <item><description>Set the newly created baseline workset to be the default workset.</description></item>
        /// <item><description>Copy the project information specified in the data dictionary to the project information field.</description></item>
        /// </list>
        /// </summary>
        /// <param name="worksetCollectionType">The type of workset collection.</param>
        /// <param name="entryCountMax">The maximum number of entries that each workset can support.</param>
        /// <param name="columnCountMax">The maximum number of display columns that each workset can support.</param>
        public void Initialize(WorksetCollectionType worksetCollectionType, short entryCountMax, short columnCountMax)
        {
            m_WorksetCollectionFile_t = new WorksetCollectionFile_t();
            m_WorksetCollectionFile_t.ProjectInformation = Parameter.ProjectInformation;
            m_WorksetCollectionFile_t.WorksetCollectionType = worksetCollectionType;
            m_WorksetCollectionFile_t.EntryCountMax = entryCountMax;
            m_WorksetCollectionFile_t.ColumnCountMax = columnCountMax;

            // Instantiate the list of workset structures.
            m_WorksetCollectionFile_t.WorksetList = new List<Workset_t>();

            // Create a new baseline workset i.e. one containing ALL watch variable references, ordered by watch identifier.
            Workset_t workset = new Workset_t();
            workset = CreateBaselineWorkset();

            // Add this baseline workset to the list.
            m_WorksetCollectionFile_t.WorksetList.Add(workset);

            // Set the active index to point at the baseline workset.
            m_WorksetCollectionFile_t.ActiveIndex = 0;

            // Set the default index to point at the baseline workset.
            m_WorksetCollectionFile_t.DefaultIndex = 0;
        }

        /// <summary>
        /// Load and validate the <see cref="WorksetCollectionFile_t"/> structure contained within the specified workset file.
        /// </summary>
        /// <param name="fullFilename">The fully qualified file name of the workset file.</param>
        public void Load(string fullFilename)
        {
            // Flag to indicate whether the workset is compatible with the current data dictionary i.e. the number of watch identifiers associated with the workset file 
            // is the same as the number of watch identifiers in the current data dictionary.
            bool isCompatible = false;

            FileInfo fileInfo = new FileInfo(fullFilename);
            if (fileInfo.Exists == true)
            {
                m_Filename = fileInfo.Name;
            }

            // De-serialize the workset file.
            m_WorksetCollectionFile_t = Deserialize(fullFilename);
            Thread.Sleep(SleepDeSerialize);

            // Check whether the de-serialized workset file is compatible with the current data dictionary.
            isCompatible = CheckCompatibility(m_WorksetCollectionFile_t);
            if (isCompatible == false)
            {
                // No, update the WatchItems and WatchElementList properies of each workset in the collection to be compatible with the new data dictionary.
                Update(ref m_WorksetCollectionFile_t);

                // Serialize the updated workset file to disk.
                Serialize(fullFilename);
            }

            // set the active workset to be the baseline workset.
            m_WorksetCollectionFile_t.ActiveIndex = 0;
        }

        /// <summary>
        /// Replace the specified workset with the new workset. If the workset specified by <paramref name="worksetName"/> cannot be found, no action is taken.
        /// </summary>
        /// <param name="worksetName">The name of the workset that is to be edited.</param>
        /// <param name="newWorkset">The new workset <seealso cref="Workset_t"/>.</param>
        public void Edit(string worksetName, Workset_t newWorkset)
        {
            // Local copy of active workset index, just in case things are disturbed.
            int activeIndex;

            // Preserve the index of the active workset in case this is disturbed by the operation.
            activeIndex = m_WorksetCollectionFile_t.ActiveIndex;

            // Get the names of the worksets included in the collection.
            object[] worksetNames = GetWorksetNames();

            // Check each entry in the collection for the specified name.
            for (int index = 0; index < m_WorksetCollectionFile_t.WorksetList.Count; index++)
            {
                if ((string)worksetNames[index] == worksetName)
                {
                    // Remove the existing workset.
                    m_WorksetCollectionFile_t.WorksetList.RemoveAt(index);

                    // Insert the new workset at the same index.
                    m_WorksetCollectionFile_t.WorksetList.Insert(index, newWorkset);

                    // Re-establish the status-quo.
                    m_WorksetCollectionFile_t.ActiveIndex = activeIndex;

                    break;
                }
            }
        }

        /// <summary>
        /// Add the specified workset.
        /// </summary>
        /// <param name="workset">The workset that is to be added to the workset collection.</param>
        /// <returns>True, if the specified workset was successfully added to the collection; otherise, false.</returns>
        public bool Add(Workset_t workset)
        {
            // Check whether the workset collection contains a workset with the same name as the specified workset. 
            if (m_WorksetCollectionFile_t.WorksetList.Contains(workset) == false)
            {
                // No - add the workset.
                m_WorksetCollectionFile_t.WorksetList.Add(workset);
                return true;
            }
            else
            {
                // Yes - Ignore the request and inform the user.
                return false;
            }
        }

		/// <summary>
        /// Check whether the workset collection contains a workset with the specified name.
        /// </summary>
        /// <param name="name">The name of the workset.</param>
		public bool Contains(string name)
		{
			foreach (Workset_t workset in m_WorksetCollectionFile_t.WorksetList)
			{
				if (workset.Name == name)
				{
					return true;
				}
			}

			return false;
		}

        /// <summary>
        /// Reset the active workset to the default workset.
        /// </summary>
        public void Reset()
        {
            m_WorksetCollectionFile_t.ActiveIndex = m_WorksetCollectionFile_t.DefaultIndex;
        }

        /// <summary>
        /// Get an object array of the workset names.
        /// </summary>
        /// <returns>An list containing the workset names.</returns>
        private string[] GetWorksetNames()
        {
            string[] worksetNames = new string[m_WorksetCollectionFile_t.WorksetList.Count];

            for (int index = 0; index < m_WorksetCollectionFile_t.WorksetList.Count; index++)
            {
                worksetNames[index] = (m_WorksetCollectionFile_t.WorksetList[index]).Name;
            }
            return worksetNames;
        }

        /// <summary>
        /// Serialize, using the SOAP format, the workset collection file structure.
        /// </summary>
        /// <param name="fullFilename">The fully qualified file name of the file where the serialized data is to be saved.</param>
        public void Serialize(string fullFilename)
        {
            // Provides functionality for formatting serialized objects.
            BinaryFormatter formatter = new BinaryFormatter();

            // Exposes a stream around a file, supporting both synchronous and asynchronous read and write operations.
            Stream fileStream;

            fileStream = new FileStream(fullFilename, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                formatter.Serialize(fileStream, m_WorksetCollectionFile_t);
            }
            finally
            {
                fileStream.Close();

                // Raise an ActiveWorksetModified event.
                OnWorksetCollectionModified(null, new EventArgs());
            }
        }

        /// <summary>
        /// Set the active workset to the specified workset. If the specified workset name cannot be found, no action is taken.
        /// </summary>
        /// <param name="worksetName">The name of the workset.</param>
        public void SetActiveWorkset(string worksetName)
        {
            // Get the names of the worksets included in the collection.
            object[] worksetNames = GetWorksetNames();

            // Check each entry in the collection for the specified name.
            for (int index = 0; index < m_WorksetCollectionFile_t.WorksetList.Count; index++)
            {
                if ((string)worksetNames[index] == worksetName)
                {
                    m_WorksetCollectionFile_t.ActiveIndex = index;
                    break;
                }
            }
        }

        /// <summary>
        /// Set the default workset to the specified workset. If the workset specified by <paramref name="worksetName"/> cannot be found, no action is taken.
        /// </summary>
        /// <param name="worksetName">The name of the workset.</param>
        public void SetDefaultWorkset(string worksetName)
        {
            // Get the names of the worksets included in the collection.
            object[] worksetNames = GetWorksetNames();

            // Check each entry in the collection for the specified name.
            for (int index = 0; index < m_WorksetCollectionFile_t.WorksetList.Count; index++)
            {
                if ((string)worksetNames[index] == worksetName)
                {
                    m_WorksetCollectionFile_t.DefaultIndex = index;
                    break;
                }
            }
        }

        /// <summary>
        /// Remove the specified workset. If the workset specified by <paramref name="worksetName"/> cannot be found, no action is taken.
        /// </summary>
        /// <param name="worksetName">The name of the workset that is to be removed.</param>
        public void Remove(string worksetName)
        {
            // Get the names of the worksets included in the collection.
            object[] worksetNames = GetWorksetNames();

            // Check each entry in the collection for the specified name.
            for (int index = 0; index < m_WorksetCollectionFile_t.WorksetList.Count; index++)
            {
                if ((string)worksetNames[index] == worksetName)
                {
                    if (index == m_WorksetCollectionFile_t.DefaultIndex)
                    {
                        // If the user deletes the default workset, set the new default workset to be the baseline workset.
                        m_WorksetCollectionFile_t.DefaultIndex = 0;
                        if (index == ActiveIndex)
                        {
                            // if the deleted workset is also the active workset set the new active workset to be the baseline workset.
                            m_WorksetCollectionFile_t.ActiveIndex = 0;
                        }
                        else
                        {
                            if (index < m_WorksetCollectionFile_t.ActiveIndex)
                            {
                                // As the deleted workset is below the active workset the active workset index value must be decremented by 1.
                                m_WorksetCollectionFile_t.ActiveIndex -= 1;
                            }
                            else if (index > m_WorksetCollectionFile_t.ActiveIndex)
                            {
                                // No need to worry about changing the active workset index as it will be unaffected by the deletion.
                            }
                        }
                    }
                    else
                    {
                        // The deleted workset is NOT the default workset.
                        if (index == m_WorksetCollectionFile_t.ActiveIndex)
                        {
                            // The deleted workset was the active workset, therefore set the new active workset to the default workset.
                            // Before setting the active workset to the default workset check whether the the deleted workset was
                            // below the default workset in the list, if so, the default workset index must be decremented by 1.
                            if (index < m_WorksetCollectionFile_t.DefaultIndex)
                            {
                                m_WorksetCollectionFile_t.DefaultIndex -= 1;
                            }
                            else if (index > m_WorksetCollectionFile_t.DefaultIndex)
                            {
                                // No need to worry about changing the default workset index as it will be unaffected by the deletion. 
                            }
                            m_WorksetCollectionFile_t.ActiveIndex = m_WorksetCollectionFile_t.DefaultIndex;
                        }
                        else
                        {
                            // Deleted workset was neither the active nor default workset.
                            if (index < m_WorksetCollectionFile_t.ActiveIndex)
                            {
                                if (index < DefaultIndex)
                                {
                                    // The deleted workset index is below both the active and default workset indices. Decrement both the active and 
                                    // default workset indices by 1.
                                    m_WorksetCollectionFile_t.ActiveIndex -= 1;
                                    m_WorksetCollectionFile_t.DefaultIndex -= 1;
                                }
                                else if (index > m_WorksetCollectionFile_t.DefaultIndex)
                                {
                                    // The deleted workset index is below the active workset index but above the default workset index. Decrement the active workset index
                                    // by 1.
                                    m_WorksetCollectionFile_t.ActiveIndex -= 1;
                                }
                            }
                            else if (index > ActiveIndex)
                            {
                                if (index < DefaultIndex)
                                {
                                    // The deleted workset index is above the active workset index but below the default workset index. Decrement the default workset index
                                    // by 1.
                                    m_WorksetCollectionFile_t.DefaultIndex = 1;
                                }
                                else if (index > m_WorksetCollectionFile_t.DefaultIndex)
                                {
                                    // No need to worry about changing the default and active workset indices as the deleted workset index is above both 
                                    // the active and default workset indices in the list.
                                }
                            }
                        }
                    }

                    // Remove the workset from the list.
                    m_WorksetCollectionFile_t.WorksetList.RemoveAt(index);

                    break;
                }
            }
        }

        /// <summary>
        /// Create the baseline workset. The baseline workset is a workset that displays the first <c>WatchSize</c> watch variables in the data dictionary. It's primary 
        /// purpose is to ensure that there is always at least one workset available. Under normal circumstances, once the user has defined a set of worksets the 
        /// baseline workset has no purpose.
        /// </summary>
        /// <returns>The baseline workset.</returns>
        public Workset_t CreateBaselineWorkset()
        {
            // Create a list of the first WatchSize watch variables that appear in the data dictionary.
            List<short> watchElementList = new List<short>();
            string name;
            for (short watchIdentifier = 0; watchElementList.Count < EntryCountMax; watchIdentifier++)
            {
                // Check that the watch identifier exists in the current data dictionary.
                try
                {
                    name = Lookup.WatchVariableTable.Items[watchIdentifier].Name;
                    watchElementList.Add(watchIdentifier);
                }
                catch (NullReferenceException)
                {
                    continue;
                }
            }

            // Instantiate a new workset structure.
            Workset_t workset = new Workset_t(Resources.NameBaselineWorkset, watchElementList, EntryCountMax, ColumnCountMax, SecurityLevel.Level2);
            return workset;
        }

        /// <summary>
        /// Check whether the specified workset file is compatible with the current data dictionary. For the workset file to be compatible: (a) no watch variables can 
        /// have been added to or deleted from the data dictionary since the workset was created/updated and (b) the the CountMax fields of all individual worksets and 
        /// the EntryCountMax field of the <c>WorksetCollectionFile_t</c> structure must be consistent with the <c>WatchSize</c> defined in the current data dictionary.
        /// </summary>
        /// <param name="worksetCollectionFile">The workset file retrieved from disk that is to be checked.</param>
        /// <returns>A flag to indicate whether the workset file is compatible with the current data dictionary. True, if the workset file is compatible; 
        /// otherwise, false.</returns>
        private bool CheckCompatibility(WorksetCollectionFile_t worksetCollectionFile)
        {
            bool isCompatible = false;

            // Get the number of watch variables and the number of oldIdentifiers associated with the current data dictionary and the data dictionary associated with 
            // the workset.
            int dataDictionaryOldIdentifierCount = Lookup.WatchVariableTableByOldIdentifier.RecordList.Count;
            int dataDictionaryWatchIdentifierCount = Lookup.WatchVariableTable.RecordList.Count;
            int worksetOldIdentifierCount = worksetCollectionFile.WorksetList[0].WatchItems.Length;
            int worksetWatchIdentifierCount = 0;
            for (int elementIndex = 0; elementIndex < worksetOldIdentifierCount; elementIndex++)
            {
                if (worksetCollectionFile.WorksetList[0].WatchItems[elementIndex].Exists == true)
                {
                    worksetWatchIdentifierCount++;
                }
            }

            // Check whether any watch variables have been added to or deleted from the data dictionary since the workset was created.
            if ((dataDictionaryOldIdentifierCount != worksetOldIdentifierCount) ||
                (dataDictionaryWatchIdentifierCount != worksetWatchIdentifierCount))
            {
                isCompatible = false;
                return isCompatible;
            }

            // Only carry out watch size processing if the workset collection corresponds to a watch window workset.
            if (worksetCollectionFile.WorksetCollectionType == Configuration.WorksetCollectionType.RecordedWatch)
            {
                // Yes - Check whether the EntryCountMax field of the workset collection needs updating
                if (worksetCollectionFile.EntryCountMax != Parameter.WatchSize)
                {
                    isCompatible = false;
                    return isCompatible;
                }
                else
                {
                    // Check whether any of the individual workset CountMax fields need updating.
                    foreach (Workset_t workset in worksetCollectionFile.WorksetList)
                    {
                        if (workset.CountMax != Parameter.WatchSize)
                        {
                            isCompatible = false;
                            return isCompatible;
                        }
                    }
                }
            }

            isCompatible = true;
            return isCompatible;
        }

        /// <summary>
        /// For the specified workset file update: (a) the baseline workset; (b) the WatchItems, WatchElementList and CountMax properties of all other worksets and (c) 
        /// the EntryCountMax field to be compatible with the new data dictionary. Also report any worksets that: (a) contain more watch values than are permitted 
        /// or (b) include one or more invalid old identifier references.
        /// </summary>
        /// <param name="worksetCollectionFile">The structure containing the de-serialized workset file.</param>
        private void Update(ref WorksetCollectionFile_t worksetCollectionFile)
        {
            // Get the number of watch variables and the number of oldIdentifiers associated with the current data dictionary and the data dictionary associated with 
            // the workset.
            int dataDictionaryOldIdentifierCount = Lookup.WatchVariableTableByOldIdentifier.RecordList.Count;
            int dataDictionaryWatchIdentifierCount = Lookup.WatchVariableTable.RecordList.Count;
            int worksetOldIdentifierCount = worksetCollectionFile.WorksetList[0].WatchItems.Length;
            int worksetWatchIdentifierCount = 0;
            for (int elementIndex = 0; elementIndex < worksetOldIdentifierCount; elementIndex++)
            {
                if (worksetCollectionFile.WorksetList[0].WatchItems[elementIndex].Exists == true)
                {
                    worksetWatchIdentifierCount++;
                }
            }

            int watchVariablesAddedTo = 0;
            int watchVariablesDeletedFrom = 0;
            if (dataDictionaryOldIdentifierCount > worksetOldIdentifierCount)
            {
                watchVariablesAddedTo = dataDictionaryOldIdentifierCount - worksetOldIdentifierCount;
                watchVariablesDeletedFrom = watchVariablesAddedTo - (dataDictionaryWatchIdentifierCount - worksetWatchIdentifierCount);
            }
            else if (worksetOldIdentifierCount > dataDictionaryOldIdentifierCount)
            {
                watchVariablesDeletedFrom = worksetOldIdentifierCount - dataDictionaryOldIdentifierCount;
                watchVariablesAddedTo = watchVariablesDeletedFrom - (worksetWatchIdentifierCount - dataDictionaryWatchIdentifierCount);
            }

            // Check whether the WatchItems property of the worksets in the specified workset collection need be updated i.e. check whether any watch variables have been 
            // added to or deleted from the the data dictionary since the workset was created.
            List<short> invalidOldIdentifierList;
            List<Workset_t> invalidOldIdentifierWorksetList, invalidWatchSizeWorksetList;
            if ((dataDictionaryOldIdentifierCount != worksetOldIdentifierCount) ||
                (dataDictionaryWatchIdentifierCount != worksetWatchIdentifierCount))
            {
                #region - [Error Reporting] -
                // Check whether any watch variables have been deleted from the current data dictionary since the worksets were defined.
                if (watchVariablesDeletedFrom > 0)
                {
                    // Yes - Get a list of any old identifiers in the workset file that are incompatible with the current data dictionary.
                    invalidOldIdentifierList = GetIncompatibleOldIdentifierList(worksetCollectionFile);
                    if (invalidOldIdentifierList.Count > 0)
                    {
                        // Get the list of worksets that are effected.
                        invalidOldIdentifierWorksetList = GetIncompatibleWorksetList(worksetCollectionFile, invalidOldIdentifierList);

                        if (invalidOldIdentifierWorksetList.Count > 0)
                        {
                            ReportIncompatibleWorksets(invalidOldIdentifierWorksetList, invalidOldIdentifierList, worksetCollectionFile.WorksetCollectionType); 
                        }
                    }
                }
                #endregion - [Error Reporting] -

                #region - [WatchItems/WatchElementList Update] -
                // ------------------------------------------------------------------------------------
                // Update the WatchItems and WatchElementList fields of all of the workset in the file.
                // ------------------------------------------------------------------------------------
                for (short worksetIndex = 0; worksetIndex < worksetCollectionFile.WorksetList.Count; worksetIndex++)
                {
                    // Replicate the workset.
                    Workset_t workset = new Workset_t();
                    workset.Replicate(worksetCollectionFile.WorksetList[worksetIndex]);

                    //Create a new WatchItems propery for the current workset based upon the current data dictionary with the Added property of all elements set to false.
                    workset.WatchItems = new WatchItem_t[dataDictionaryOldIdentifierCount];
                    WatchItem_t watchItem;
                    for (short watchItemIndex = 0; watchItemIndex < dataDictionaryOldIdentifierCount; watchItemIndex++)
                    {
                        watchItem = new WatchItem_t();
                        watchItem.OldIdentifier = watchItemIndex;
                        watchItem.Added = false;

                        try
                        {
                            // Check whether the watch variable exists and set the Exists property of the watch item appropriately.
                            WatchVariable watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[watchItemIndex];
                            watchItem.Exists = (watchVariable == null) ? false : true;
                        }
                        catch (Exception)
                        {
                            watchItem.Exists = false;
                        }

                        workset.WatchItems[watchItemIndex] = watchItem;
                    }

                    // Update the WatchElementList and the WatchItems properties of the workset using the old identifier values stored in each column of the workset.
                    workset.WatchElementList = new List<short>();
                    short oldIdentifier;
                    for (int columnIndex = 0; columnIndex < workset.Column.Length; columnIndex++)
                    {
                        for (int rowIndex = 0; rowIndex < workset.Column[columnIndex].OldIdentifierList.Count; rowIndex++)
                        {
                            // Get the old identifier value.
                            oldIdentifier = workset.Column[columnIndex].OldIdentifierList[rowIndex];

                            // Check whether the watch variable associated with the old identifier value exists in the current workset.
                            try
                            {
                                WatchVariable watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifier];
                                if (watchVariable == null)
                                {
                                    // No, add the watch identifier value used to represent the watch variable not being defined in the current data dictionary.
                                    workset.WatchElementList.Add(CommonConstants.WatchIdentifierNotDefined);
                                }
                                else
                                {
                                    // Add the watch identifier of the watch variable corresponding to the specified old identifier to the list of watch identifiers.
                                    workset.WatchElementList.Add(watchVariable.Identifier);

                                    // Update the Exists and Added properties of the WatchItem element corresponding to the specified old identifier.
                                    workset.WatchItems[oldIdentifier].Added = true;
                                    workset.WatchItems[oldIdentifier].Exists = true;
                                }
                            }
                            catch (Exception)
                            {
                                // No, add the watch identifier value used to represent the watch variable not being defined in the current data dictionary.
                                workset.WatchElementList.Add(CommonConstants.WatchIdentifierNotDefined);
                            }
                        }
                    }

                    workset.WatchElementList.Sort();

                    // Replace the workset.
                    worksetCollectionFile.WorksetList[worksetIndex] = workset;
                    #endregion - [WatchItems/WatchElementList Update] -
                }
            }

            // Only carry out watch size processing if the workset collection corresponds to a watch window workset.
            if (worksetCollectionFile.WorksetCollectionType == Configuration.WorksetCollectionType.RecordedWatch)
            {
                #region - [Error Reporting] -
                invalidWatchSizeWorksetList = GetIncompatibleWorksetList(worksetCollectionFile, Parameter.WatchSize);
                if (invalidWatchSizeWorksetList.Count > 0)
                {
                    ReportIncompatibleWorksets(invalidWatchSizeWorksetList);
                }
                #endregion - [Error Reporting] -

                #region - [WatchSize Update] -
                // Update the EntryCountMax field of the workset file, if required.
                if (worksetCollectionFile.EntryCountMax != Parameter.WatchSize)
                {
                    worksetCollectionFile.EntryCountMax = Parameter.WatchSize;
                }

                // Update the baseline workset and the EntryCount max field of individual worksets, if required.
                for (int worksetIndex = 0; worksetIndex < worksetCollectionFile.WorksetList.Count; worksetIndex++)
                {
                    Workset_t workset;
                    if (worksetCollectionFile.WorksetList[worksetIndex].CountMax != Parameter.WatchSize)
                    {
                        // If the workset is the baseline workset, update it.
                        if (worksetIndex == 0)
                        {
                            workset = CreateBaselineWorkset();

                        }
                        else
                        {
                            workset = worksetCollectionFile.WorksetList[worksetIndex];
                            workset.CountMax = Parameter.WatchSize;
                        }

                        worksetCollectionFile.WorksetList[worksetIndex] = workset;
                    }
                }
                #endregion - [WatchSize Update] -
            }
        }

        /// <summary>
        /// De-serialize the serialized data.  
        /// </summary>
        /// <param name="fullFilename">The fully qualified file name of the file where the serialized data is stored.</param>
        /// <returns>The <c>WorksetCollectionFile_t</c> structure that was de-serialized from disk.</returns>
        private WorksetCollectionFile_t Deserialize(string fullFilename)
        {
            // Provides functionality for formatting serialized objects.
            BinaryFormatter formatter = new BinaryFormatter();

            // Exposes a stream around a file supporting both synchronous and asynchronous read and write operations.
            Stream fileStream;

            // Open the file containing the serialized data.
            fileStream = new FileStream(fullFilename, FileMode.Open);
            try
            {
                return (WorksetCollectionFile_t)formatter.Deserialize(fileStream);
            }
            finally
            {
                fileStream.Close();
            }
        }

        #region - [Error Reporting] -
        /// <summary>
        /// Get a list of those old identifiers that are included in the workset file but are not defined in the current data dictionary. 
        /// </summary>
        /// <param name="worksetCollectionFile">The structure containing the de-serialized workset file.</param>
        /// <returns>A list of those old identifiers that are included in the workset file but are not defined in the current data dictionary.</returns>
        private static List<short> GetIncompatibleOldIdentifierList(WorksetCollectionFile_t worksetCollectionFile)
        {
            // Create a list of all unique old identifiers used in the workset collection.
            List<short> oldIdentifierList = new List<short>();
            short oldIdentifier;
            for (short worksetIndex = 0; worksetIndex < worksetCollectionFile.WorksetList.Count; worksetIndex++)
            {
                for (int columnIndex = 0; columnIndex < worksetCollectionFile.WorksetList[worksetIndex].Column.Length; columnIndex++)
                {
                    for (int rowIndex = 0; rowIndex < worksetCollectionFile.WorksetList[worksetIndex].Column[columnIndex].OldIdentifierList.Count; rowIndex++)
                    {
                        oldIdentifier = worksetCollectionFile.WorksetList[worksetIndex].Column[columnIndex].OldIdentifierList[rowIndex];
                        short returnedValue = oldIdentifierList.Find(delegate(short value) { return value == oldIdentifier; });

                        if (returnedValue == 0)
                        {
                            oldIdentifierList.Add(oldIdentifier);
                        }
                    }
                }
            }

            oldIdentifierList.Sort();

            // Now check if any of the watch variables corresponding to these old identifier values are no longer defined in the database.
            List<short> invalidOldIdentifierList = new List<short>();
            WatchVariable watchVariable;
            for (int index = 0; index < oldIdentifierList.Count; index++)
            {
                try
                {
                    watchVariable = Lookup.WatchVariableTableByOldIdentifier.Items[oldIdentifierList[index]];
                    if (watchVariable == null)
                    {
                        invalidOldIdentifierList.Add(oldIdentifierList[index]);
                    }
                }
                catch (Exception)
                {
                    invalidOldIdentifierList.Add(oldIdentifierList[index]);
                }
            }

            return invalidOldIdentifierList;
        }

        /// <summary>
        /// Get a list of those worksets that contain one or more old identifier references that are not defined in the current data dictionary.
        /// </summary>
        /// <param name="worksetCollectionFile">The structure containing the de-serialized workset file.</param>
        /// <param name="incompatibleOldIdentifierList">A list of list of those old identifiers that are included in the workset file but are not defined in the 
        /// current data dictionary.</param>
        /// <returns>A list of those worksets that contain one or more old identifier references that are not defined in the current data dictionary.</returns>
        private static List<Workset_t> GetIncompatibleWorksetList(WorksetCollectionFile_t worksetCollectionFile, List<short> incompatibleOldIdentifierList)
        {
            // Generate a list of the worksets that contain the specified invalid old identifiers.
            List<Workset_t> incompatibleWorksetList = worksetCollectionFile.WorksetList.FindAll(delegate(Workset_t searchWorkset)
            {
                foreach (short value in incompatibleOldIdentifierList)
                {
                    for (int columnIndex = 0; columnIndex < searchWorkset.Column.Length; columnIndex++)
                    {
                        if (searchWorkset.Column[columnIndex].OldIdentifierList.Contains(value))
                        {
                            return true;
                        }
                    }
                }
                return false;
            });

            // Remove the baseline workset as this is corrected automatically.
            Workset_t workset = worksetCollectionFile.WorksetList[0];
            if (incompatibleWorksetList.Contains(workset) == true)
            {
                incompatibleWorksetList.Remove(workset);
            }

            return incompatibleWorksetList;
        }

        /// <summary>
        /// Get a list of those worksets that contain more watch variables than the specified watch size.
        /// </summary>
        /// <param name="worksetCollectionFile">The structure containing the de-serialized workset file.</param>
        /// <param name="watchSize">The permitted number of watch variables, this is defined by the <c>WatchSize</c> field in the <c>ConfigurePTU</c> table of the 
        /// data dictionary.</param>
        /// <returns>A list of those worksets that contain more watch variables than the specified watch size.</returns>
        private static List<Workset_t> GetIncompatibleWorksetList(WorksetCollectionFile_t worksetCollectionFile, short watchSize)
        {
            List<Workset_t> incompatibleWorksetList = new List<Workset_t>();
            foreach (Workset_t workset in worksetCollectionFile.WorksetList)
            {
                // Skip the baseline workset as this is corrected automatically.
                if (workset.Name.Equals(Resources.NameBaselineWorkset))
                {
                    continue;
                }

                if (workset.Count > watchSize)
                {
                    incompatibleWorksetList.Add(workset);
                }
            }

            return incompatibleWorksetList;
        }

        /// <summary>
        /// Report those worksets that contain too many watch variables.
        /// </summary>
        /// <param name="invalidWatchSizeWorksetList">A list of those worksets that contain more watch variables than the specified watch size.</param>
        private static void ReportIncompatibleWorksets(List<Workset_t> invalidWatchSizeWorksetList)
        {
            // Build and format the list of worksets that are effected.
            StringBuilder stringBuilder = new StringBuilder(Environment.NewLine);
            stringBuilder.AppendLine();
            for (int index = 0; index < invalidWatchSizeWorksetList.Count; index++)
            {
                string lineTerminator = (index == invalidWatchSizeWorksetList.Count - 1) ? CommonConstants.Period : CommonConstants.Comma;
                stringBuilder.AppendLine(CommonConstants.SpaceX4 + (index + 1).ToString(CommonConstants.FormatStringListNumbering) + CommonConstants.FullStop + invalidWatchSizeWorksetList[index].Name + lineTerminator);
            }
            stringBuilder.AppendLine();


            // Notify the user of the worksets that contain too many watch variables.
            System.Windows.Forms.MessageBox.Show(string.Format(Resources.MBTWorksetsWatchSizeExceeded, Resources.NameConfigureWorksetsWatchWindowMenuOption) + stringBuilder.ToString() + Resources.MBTInstructionConfigureWorksets, 
                                                 Resources.MBCaptionWarning, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Report those worksets that contain one or more old identifier references that are not defined in the current data dictionary.
        /// </summary>
        /// <param name="invalidOldIdentifierWorksetList">A list of those worksets that contain one or more old identifier references that are not defined in the 
        /// current data dictionary.</param>
        /// <param name="invalidOldIdentifierList">A list of the old identifier references included in the workset file that are not defined in the data dictionary.</param>
        /// <param name="worksetCollectionType">The type of workset being processed.</param>
        private static void ReportIncompatibleWorksets(List<Workset_t> invalidOldIdentifierWorksetList, List<short> invalidOldIdentifierList, WorksetCollectionType worksetCollectionType)
        {
            // Build and format the list of worksets that are effected.
            StringBuilder stringBuilder = new StringBuilder(Environment.NewLine);
            stringBuilder.AppendLine();
            for (int index = 0; index < invalidOldIdentifierWorksetList.Count; index++)
            {
                string lineTerminator = (index == invalidOldIdentifierWorksetList.Count - 1) ? CommonConstants.Period : CommonConstants.Comma;
                stringBuilder.AppendLine(CommonConstants.SpaceX4 + (index + 1).ToString(CommonConstants.FormatStringListNumbering) + CommonConstants.FullStop + invalidOldIdentifierWorksetList[index].Name + lineTerminator);
            }
            stringBuilder.AppendLine();
            string invalidOldIdentifierWorksetListText = stringBuilder.ToString();

            // Build and format the list of invalid old identifiers.
            stringBuilder = new StringBuilder(string.Empty);
            for (int index = 0; index < invalidOldIdentifierList.Count; index++)
            {
                string lineTerminator = (index == invalidOldIdentifierList.Count - 1) ? CommonConstants.Period : CommonConstants.Comma;
                stringBuilder.Append(invalidOldIdentifierList[index].ToString() + lineTerminator);
            }
            string invalidOldIdentifierListText = stringBuilder.ToString(); 

            string worksetCollectionTypeText;
            switch (worksetCollectionType)
            {
                case WorksetCollectionType.RecordedWatch:
                    worksetCollectionTypeText = Resources.NameConfigureWorksetsWatchWindowMenuOption;
                    break;
                case WorksetCollectionType.FaultLog:
                    worksetCollectionTypeText = Resources.NameConfigureWorksetsFaultLogMenuOption;
                    break;
                case WorksetCollectionType.Chart:
                    worksetCollectionTypeText = Resources.NameConfigureWorksetsChartRecorderMenuOption;
                    break;
                default:
                    worksetCollectionTypeText = string.Empty;
                    Debug.Assert(false, "ReportIncompatibleWorksets - [invalid worksetCollectionType]");
                    break;
            }
            
            // Notify the user of the worksets that contain too many watch variables.
            System.Windows.Forms.MessageBox.Show(string.Format(Resources.MBTWorksetsOldIdentifiersInvalid, worksetCollectionTypeText) + invalidOldIdentifierWorksetListText +
                                                 string.Format(Resources.MBTInvalidOldIdentifierList, invalidOldIdentifierListText) + Environment.NewLine + Environment.NewLine +
                                                 Resources.MBTInstructionConfigureWorksets,
                                                 Resources.MBCaptionWarning, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }
        #endregion - [Error Reporting] -

        #region - [Event Management] -
        /// <summary>
        /// Raise a <c>WorksetCollectionModified</c> event. 
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event.</param>
        /// <param name="eventArgs">Parameter passed from the object that raised the event.</param>
        private void OnWorksetCollectionModified(object sender, EventArgs eventArgs)
        {
            if (WorksetCollectionModified != null)
            {
                WorksetCollectionModified(sender, eventArgs);
            }
        }
        #endregion - [Event Management] -
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the project information structure <see cref="DataDictionaryInformation_t"/>.
        /// </summary>
        public DataDictionaryInformation_t ProjectInformation
        {
            set { m_WorksetCollectionFile_t.ProjectInformation = value; }
            get { return m_WorksetCollectionFile_t.ProjectInformation; }
        }

        /// <summary>
        /// Gets the name of the active workset.
        /// </summary>
        public string ActiveName
        {
            get { return m_WorksetCollectionFile_t.WorksetList[m_WorksetCollectionFile_t.ActiveIndex].Name; }
        }

        /// <summary>
        /// Gets or sets the index of the active workset.
        /// </summary>
        public int ActiveIndex
        {
            get { return m_WorksetCollectionFile_t.ActiveIndex; }
            set
            {
                if ((value >= 0) && (value < m_WorksetCollectionFile_t.WorksetList.Count))
                {
                    m_WorksetCollectionFile_t.ActiveIndex = value;
                }
            }
        }

        /// <summary>
        /// Gets the name of the default workset.
        /// </summary>
        public string DefaultName
        {
            get { return m_WorksetCollectionFile_t.WorksetList[m_WorksetCollectionFile_t.DefaultIndex].Name; }
        }

        /// <summary>
        /// Gets or sets the index of the default workset.
        /// </summary>
        public int DefaultIndex
        {
            get { return m_WorksetCollectionFile_t.DefaultIndex; }
            set
            {
                if ((value >= 0) && (value < m_WorksetCollectionFile_t.WorksetList.Count))
                {
                    m_WorksetCollectionFile_t.DefaultIndex = value;
                }
            }
        }

        /// <summary>
        /// Gets the active workset.
        /// </summary>
        public Workset_t ActiveWorkset
        {
            get { return m_WorksetCollectionFile_t.WorksetList[ActiveIndex]; }
        }

        /// <summary>
        /// Gets the list of the available workset structures <see cref="Workset_t"/>.
        /// </summary>
        public List<Workset_t> Worksets
        {
            get { return m_WorksetCollectionFile_t.WorksetList; }
        }

        /// <summary>
        /// Gets the maximum number of display columns that each workset can support.
        /// </summary>
        public short ColumnCountMax
        {
            get { return m_WorksetCollectionFile_t.ColumnCountMax; }
        }

        /// <summary>
        /// Gets the maximum number of entries that each workset can support.
        /// </summary>
        public short EntryCountMax
        {
            get { return m_WorksetCollectionFile_t.EntryCountMax; }
        }

        /// <summary>
        /// Gets the type of workset collection e.g. Watch, FaultLog, Chart, SelfTest.
        /// </summary>
        public WorksetCollectionType WorksetCollectionType
        {
            get { return m_WorksetCollectionFile_t.WorksetCollectionType; }
        }

        /// <summary>
        /// Gets the file name of the file containing the workset definitions.
        /// </summary>
        public string Filename
        {
            get { return m_Filename; }
        }
        #endregion --- Properties ---
    }
}