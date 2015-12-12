using System;
using VcuComm;

namespace Common.Communication
{
    /// <summary>
    ///
    /// </summary>
    public class EventStreamMarshal
    {
        #region --- Constants ---

        /// <summary>
        ///
        /// </summary>
        private const Int16 MAX_DL_VARIABLES = 256;

        /// <summary>
        ///
        /// </summary>
        private const Int16 MAX_EVENTS_PER_TASK = 100;

        /// <summary>
        ///
        /// </summary>
        private const UInt16 MAX_FAULT_BUFFER_SIZE = 4096;

        /// <summary>
        ///
        /// </summary>
        private const Int16 MAX_FAULT_SIZE_BYTES = 256;

        /// <summary>
        ///
        /// </summary>
        private const Int16 MAX_NUM_FAULTS = 1000;

        /// <summary>
        ///
        /// </summary>
        private const Int16 MAX_TASKS = 120;


        private const UInt32 EMPTY_FAULT_BUFFER = UInt32.MaxValue;

        #endregion --- Constants ---

        #region --- Member Variables ---

        /// <summary>
        ///
        /// </summary>
        private ICommDevice m_CommDevice;

        /// <summary>
        ///
        /// </summary>
        private Int16 m_CurrentNumberOfFaults;

        /// <summary>
        ///
        /// </summary>
        private ProtocolPTU.GetFaultDataRes m_FaultDataFromTarget;

        /// <summary>
        ///
        /// </summary>
        private Byte[][] m_faultStorage = new Byte[MAX_NUM_FAULTS][];

        /// <summary>
        ///
        /// </summary>
        private Byte[] m_RxMessage = new Byte[MAX_FAULT_BUFFER_SIZE];

        /// <summary>
        ///
        /// </summary>
        private VcuCommunication m_VcuCommunication;

        #endregion --- Member Variables ---

        #region --- Constructors ---

        /// <summary>
        ///
        /// </summary>
        /// <param name="device"></param>
        public EventStreamMarshal(ICommDevice device)
        {
            m_CommDevice = device;
            m_VcuCommunication = new VcuCommunication();
        }

        /// <summary>
        ///
        /// </summary>
        private EventStreamMarshal()
        { }

        #endregion --- Constructors ---

        #region --- Methods ---

        #region --- Public Methods ---

        /// <summary>
        ///
        /// </summary>
        /// <param name="NewEventLogNumber"></param>
        /// <param name="DataRecordingRate"></param>
        /// <param name="ChangeStatus"></param>
        /// <param name="MaxTasks"></param>
        /// <param name="MaxEventsPerTask"></param>
        /// <returns></returns>
        public CommunicationError ChangeEventLog(Int16 NewEventLogNumber, ref Int16 DataRecordingRate, ref Int16 ChangeStatus,
                                                 ref Int16 MaxTasks, ref Int16 MaxEventsPerTask)
        {
            ProtocolPTU.ChangeEventLogReq request = new ProtocolPTU.ChangeEventLogReq(NewEventLogNumber);

            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, request, m_RxMessage);

            if (commError == CommunicationError.Success)
            {
                ChangeStatus = BitConverter.ToInt16(m_RxMessage, 8);
                DataRecordingRate = BitConverter.ToInt16(m_RxMessage, 10);
                MaxTasks = BitConverter.ToInt16(m_RxMessage, 12);
                MaxEventsPerTask = BitConverter.ToInt16(m_RxMessage, 14);

                if (m_CommDevice.IsTargetBigEndian())
                {
                    ChangeStatus = Utils.ReverseByteOrder(ChangeStatus);
                    DataRecordingRate = Utils.ReverseByteOrder(DataRecordingRate);
                    MaxTasks = Utils.ReverseByteOrder(MaxTasks);
                    MaxEventsPerTask = Utils.ReverseByteOrder(MaxEventsPerTask);
                }
            }

            if (MaxTasks >= MAX_TASKS)
            {
                MaxTasks = MAX_TASKS - 1;
            }

            if (MaxEventsPerTask >= MAX_EVENTS_PER_TASK)
            {
                MaxEventsPerTask = MAX_EVENTS_PER_TASK - 1;
            }

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="PassedNumOfFaults"></param>
        /// <param name="orig_new"></param>
        /// <returns></returns>
        public CommunicationError CheckFaultlogger(ref Int16 PassedNumOfFaults, ref UInt32 orig_new)
        {
            UInt32 OldestIndex = EMPTY_FAULT_BUFFER;
            UInt32 NewestIndex = EMPTY_FAULT_BUFFER;
            Int16 RemoteFaults = -1;
            CommunicationError commError;
            UInt32 FaultIndex;

            // LOOP ONCE ... EXIT ON ERROR
            do
            {
                // Disable Fault Logging
                commError = SetFaultLog(false);
                if (commError != CommunicationError.Success)
                {
                    break;
                }

                // Get Fault Log Indexes
                commError = GetFaultIndices(out OldestIndex, out NewestIndex);
                if (commError != CommunicationError.Success)
                {
                    break;
                }

                if (orig_new == 0xFFFFFFFF)
                {
                    FaultIndex = OldestIndex;
                }
                else
                {
                    FaultIndex = (UInt32)(orig_new + 1);
                }

                // Check if Fault Log is Empty
                if ((NewestIndex == EMPTY_FAULT_BUFFER) && (OldestIndex == EMPTY_FAULT_BUFFER))
                {
                    RemoteFaults = 0;
                    break;
                }

                // Compute number of Faults
                RemoteFaults = (Int16)(NewestIndex - FaultIndex + 1);
                if (RemoteFaults == 0)
                {
                    break;
                }

                commError = GetFaultData((UInt32)FaultIndex, (UInt16)RemoteFaults);
                if (commError != CommunicationError.Success)
                {
                    break;
                }

                if (m_FaultDataFromTarget.BufferSize == 0)
                {
                    break;
                }

                // Enable Fault Logging
                commError = SetFaultLog(true);
                if (commError != CommunicationError.Success)
                {
                    break;
                }

                // Loop through the fault buffer, pulling out the size and data for each fault
                Int32 Index = 0;
                while (Index < m_FaultDataFromTarget.BufferSize)
                {
                    Int16 FaultSize;
                    // Get the size of the next fault
                    FaultSize = BitConverter.ToInt16(m_FaultDataFromTarget.Buffer, Index);

                    // Allocate jagged array dynamically and store fault data there
                    if (FaultSize < MAX_FAULT_SIZE_BYTES && FaultSize > 0)
                    {
                        // Add new member with size "FaultSize" to jagged 2 dimensional array
                        m_faultStorage[m_CurrentNumberOfFaults] = new Byte[FaultSize + 2];
                        // Copy all data into newly created array
                        Buffer.BlockCopy(m_FaultDataFromTarget.Buffer, Index, m_faultStorage[m_CurrentNumberOfFaults], 0, FaultSize + 2);

                        m_CurrentNumberOfFaults++;
                    }
                    else
                    {
                        // Fault Buffer is corrupt beyond hope at this point
                        commError = CommunicationError.UnknownError;
                        break;
                    }

                    // Increment the Index to point to the size of the next fault
                    Index += (FaultSize + 2);
                }
            } while (false);

            // Enable Fault Logging here in case we left the while loop early
            commError = SetFaultLog(true);

            if ((commError == CommunicationError.Success) && (RemoteFaults > 0))
            {
                orig_new = NewestIndex;
                PassedNumOfFaults = m_CurrentNumberOfFaults;
            }

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public CommunicationError ClearEvent()
        {
            CommunicationError commError = 
                m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, ProtocolPTU.PacketType.CLEAR_EVENTLOG);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="NumberOfVariables"></param>
        /// <param name="NumberOfSamples"></param>
        /// <param name="SampleRate"></param>
        /// <param name="VariableIndex"></param>
        /// <param name="VariableType"></param>
        /// <returns></returns>
        public CommunicationError GetDefaultStreamInformation(out Int16 NumberOfVariables, out Int16 NumberOfSamples, out Int16 SampleRate,
                                                               Int16[] VariableIndex, Int16[] VariableType)
        {
            NumberOfVariables = -1;
            NumberOfSamples = -1;
            SampleRate = -1;

            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_DEFAULT_STREAM, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            NumberOfVariables = BitConverter.ToInt16(m_RxMessage, 8);
            NumberOfSamples = BitConverter.ToInt16(m_RxMessage, 10);
            SampleRate = BitConverter.ToInt16(m_RxMessage, 12);

            if (m_CommDevice.IsTargetBigEndian())
            {
                NumberOfVariables = Utils.ReverseByteOrder(NumberOfVariables);
                NumberOfSamples = Utils.ReverseByteOrder(NumberOfSamples);
                SampleRate = Utils.ReverseByteOrder(SampleRate);
            }

            if (NumberOfVariables > MAX_DL_VARIABLES)
            {
                NumberOfVariables = MAX_DL_VARIABLES;
            }

            for (Int16 Counter = 0; Counter < NumberOfVariables; Counter++)
            {
                VariableIndex[Counter] = BitConverter.ToInt16(m_RxMessage, 14 + (Counter * 4));
                VariableType[Counter] = BitConverter.ToInt16(m_RxMessage, 16 + (Counter * 4));

                if (m_CommDevice.IsTargetBigEndian())
                {
                    VariableIndex[Counter] = Utils.ReverseByteOrder(VariableIndex[Counter]);
                    VariableType[Counter] = Utils.ReverseByteOrder(VariableType[Counter]);
                }
            }

            return CommunicationError.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="CurrentEventLog"></param>
        /// <param name="NumberEventLogs"></param>
        /// <returns></returns>
        public CommunicationError GetEventLog(out Int16 CurrentEventLog, out Int16 NumberEventLogs)
        {
            CurrentEventLog = -1;
            NumberEventLogs = -1;
            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_EVENT_LOG);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            CurrentEventLog = BitConverter.ToInt16(m_RxMessage, 8);
            NumberEventLogs = BitConverter.ToInt16(m_RxMessage, 10);

            if (m_CommDevice.IsTargetBigEndian())
            {
                CurrentEventLog = Utils.ReverseByteOrder(CurrentEventLog);
                NumberEventLogs = Utils.ReverseByteOrder(NumberEventLogs);
            }

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="FaultIndex"></param>
        /// <param name="NumberOfFaults"></param>
        /// <returns></returns>
        public CommunicationError GetFaultData(UInt32 FaultIndex, UInt16 NumberOfFaults)
        {
            ProtocolPTU.GetFaultDataReq request = new ProtocolPTU.GetFaultDataReq(FaultIndex, NumberOfFaults);

            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, request, m_RxMessage);

            if (commError == CommunicationError.Success)
            {
                m_FaultDataFromTarget.BufferSize = BitConverter.ToUInt16(m_RxMessage, 8);
                if (m_CommDevice.IsTargetBigEndian())
                {
                    m_FaultDataFromTarget.BufferSize = Utils.ReverseByteOrder(m_FaultDataFromTarget.BufferSize);
                }

                if (m_FaultDataFromTarget.BufferSize > MAX_FAULT_BUFFER_SIZE)
                {
                    //TODO return error
                }

                if (m_FaultDataFromTarget.BufferSize == 0)
                {
                    //TODO return noERROR
                }

                m_FaultDataFromTarget.Buffer = new Byte[m_FaultDataFromTarget.BufferSize];

                // Copy the entire response into the fault data buffer
                Buffer.BlockCopy(m_RxMessage, 10, m_FaultDataFromTarget.Buffer, 0, m_FaultDataFromTarget.BufferSize);
            }

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="faultnum"></param>
        /// <param name="tasknum"></param>
        /// <param name="Flttime"></param>
        /// <param name="Fltdate"></param>
        /// <param name="datalognum"></param>
        /// <returns></returns>
        public CommunicationError GetFaultHdr(Int16 index, ref Int16 faultnum, ref Int16 tasknum,
                                              ref String Flttime, ref String Fltdate, ref Int16 datalognum)
        {
            // Check the Validity of the desired index
            if (index >= m_CurrentNumberOfFaults)
            {
                Flttime = "N/A";
                Fltdate = "N/A";
                datalognum = -1;
                faultnum = 0;
                tasknum = 0;
                return CommunicationError.UnknownError;
            }

            const UInt16 DATE_OFFSET_IN_FAULT_LOG = 10;

            Byte hour = m_faultStorage[index][DATE_OFFSET_IN_FAULT_LOG];
            Byte minute = m_faultStorage[index][DATE_OFFSET_IN_FAULT_LOG + 1];
            Byte second = m_faultStorage[index][DATE_OFFSET_IN_FAULT_LOG + 2];
            Byte month = m_faultStorage[index][DATE_OFFSET_IN_FAULT_LOG + 3];
            Byte day = m_faultStorage[index][DATE_OFFSET_IN_FAULT_LOG + 4];
            Byte year = m_faultStorage[index][DATE_OFFSET_IN_FAULT_LOG + 5];

            if (m_CommDevice.IsTargetBigEndian())
            {
                hour = Utils.ReverseByteOrder(hour);
                minute = Utils.ReverseByteOrder(minute);
                second = Utils.ReverseByteOrder(second);
                month = Utils.ReverseByteOrder(month);
                day = Utils.ReverseByteOrder(day);
                year = Utils.ReverseByteOrder(year);
            }

            // Check Time
            if (VerifyTime(hour, minute, second))
            {
                Flttime = hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
            }
            else
            {
                Flttime = "N/A";
            }

            // Check Date
            if (VerifyDate(month, day, year))
            {
                Fltdate = month.ToString("D2") + "/" + day.ToString("D2") + "/" + year.ToString("D2");
            }
            else
            {
                Fltdate = "N/A";
            }

            faultnum = BitConverter.ToInt16(m_faultStorage[index], 2);
            tasknum = BitConverter.ToInt16(m_faultStorage[index], 4);
            datalognum = BitConverter.ToInt16(m_faultStorage[index], 18);

            if (m_CommDevice.IsTargetBigEndian())
            {
                faultnum = Utils.ReverseByteOrder(faultnum);
                tasknum = Utils.ReverseByteOrder(tasknum);
                datalognum = Utils.ReverseByteOrder(datalognum);
            }

            return CommunicationError.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Oldest"></param>
        /// <param name="Newest"></param>
        /// <returns></returns>
        public CommunicationError GetFaultIndices(out UInt32 Oldest, out UInt32 Newest)
        {
            Oldest = EMPTY_FAULT_BUFFER;
            Newest = EMPTY_FAULT_BUFFER;

            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_FAULT_INDICES, m_RxMessage);

            if (commError == CommunicationError.Success)
            {
                Newest = BitConverter.ToUInt32(m_RxMessage, 8);
                Oldest = BitConverter.ToUInt32(m_RxMessage, 12);

                if (m_CommDevice.IsTargetBigEndian())
                {
                    Newest = Utils.ReverseByteOrder(Newest);
                    Oldest = Utils.ReverseByteOrder(Oldest);
                }
            }

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="FaultIndex"></param>
        /// <param name="NumberOfVariables"></param>
        /// <param name="VariableType"></param>
        /// <param name="VariableValue"></param>
        /// <returns></returns>
        public CommunicationError GetFaultVar(Int16 FaultIndex, Int16 NumberOfVariables, Int16[] VariableType, Double[] VariableValue)
        {
            // Check the Validity of the desired index
            if (FaultIndex >= m_CurrentNumberOfFaults)
            {
                return CommunicationError.UnknownError;
            }

            UInt16 variableOffset = 18;

            for (Int16 var = 0; var < NumberOfVariables; var++)
            {
                switch ((ProtocolPTU.VariableType)VariableType[var])
                {
                    case ProtocolPTU.VariableType.UINT_8_TYPE:
                        Byte bVal = m_faultStorage[FaultIndex][variableOffset];
                        if (m_CommDevice.IsTargetBigEndian())
                        {
                            bVal = Utils.ReverseByteOrder(bVal);
                        }
                        VariableValue[var] = (Double)bVal;
                        variableOffset += sizeof(Byte);
                        break;

                    case ProtocolPTU.VariableType.INT_8_TYPE:
                        SByte signedByte = (SByte)m_faultStorage[FaultIndex][variableOffset];
                        if (m_CommDevice.IsTargetBigEndian())
                        {
                            signedByte = Utils.ReverseByteOrder(signedByte);
                        }
                        VariableValue[var] = (Double)signedByte;
                        variableOffset += sizeof(Char);
                        break;

                    case ProtocolPTU.VariableType.UINT_16_TYPE:
                        UInt16 u16 = BitConverter.ToUInt16(m_faultStorage[FaultIndex], variableOffset);
                        if (m_CommDevice.IsTargetBigEndian())
                        {
                            u16 = Utils.ReverseByteOrder(u16);
                        }
                        VariableValue[var] = (Double)u16;
                        variableOffset += sizeof(UInt16);
                        break;

                    case ProtocolPTU.VariableType.INT_16_TYPE:
                        Int16 i16 = BitConverter.ToInt16(m_faultStorage[FaultIndex], variableOffset);
                        if (m_CommDevice.IsTargetBigEndian())
                        {
                            i16 = Utils.ReverseByteOrder(i16);
                        }
                        VariableValue[var] = (Double)i16;
                        variableOffset += sizeof(Int16);
                        break;

                    case ProtocolPTU.VariableType.UINT_32_TYPE:
                        UInt32 u32 = BitConverter.ToUInt32(m_faultStorage[FaultIndex], variableOffset);
                        if (m_CommDevice.IsTargetBigEndian())
                        {
                            u32 = Utils.ReverseByteOrder(u32);
                        }
                        VariableValue[var] = (Double)u32;
                        variableOffset += sizeof(UInt32);
                        break;

                    case ProtocolPTU.VariableType.INT_32_TYPE:
                        Int32 i32 = BitConverter.ToInt32(m_faultStorage[FaultIndex], variableOffset);
                        if (m_CommDevice.IsTargetBigEndian())
                        {
                            i32 = Utils.ReverseByteOrder(i32);
                        }
                        VariableValue[var] = (Double)i32;
                        variableOffset += sizeof(Int32);
                        break;

                    default:
                        return CommunicationError.UnknownError;
                }
            }

            return CommunicationError.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Valid"></param>
        /// <param name="EnableFlag"></param>
        /// <param name="TriggerFlag"></param>
        /// <param name="EntryCount"></param>
        /// <returns></returns>
        public CommunicationError GetFltFlagInfo(Int16[] Valid, Int16[] EnableFlag, Int16[] TriggerFlag, Int16 EntryCount)
        {
            Byte[] message1 = new Byte[2048];

            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_FAULT_FLAG, message1);
            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            Int16 NumberOfWords;
            NumberOfWords = BitConverter.ToInt16(m_RxMessage, 8);
            if (m_CommDevice.IsTargetBigEndian())
            {
                NumberOfWords = Utils.ReverseByteOrder(NumberOfWords);
            }
            NumberOfWords /= 2;

            Byte[] message2 = new Byte[2048];
            commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, ProtocolPTU.PacketType.GET_STREAM_FLAG, message2);
            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            // TODO the code below begs for refactoring (maybe nested loop or something
            // Loop through all the TaskId/FaultId Combinations and set/reset a bit for each one
            UInt16 mask = 0x0001;
            Int16 Counter = 0;
            for (Int16 NumberOfEntries = 0; NumberOfEntries < EntryCount; NumberOfEntries++)
            {
                Int16 Index = (Int16)(NumberOfEntries / 16);

                UInt16 enableFlag = BitConverter.ToUInt16(message1, 10 + (Index * 2));
                UInt16 datalogFlag = BitConverter.ToUInt16(message2, 10 + (Index * 2));

                if (m_CommDevice.IsTargetBigEndian())
                {
                    enableFlag = Utils.ReverseByteOrder(enableFlag);
                    datalogFlag = Utils.ReverseByteOrder(datalogFlag);
                }

                if ((Index < NumberOfWords) && (Valid[NumberOfEntries] != 0))
                {
                    EnableFlag[Counter] = (Int16)(((enableFlag & mask) != 0) ? 1 : 0);
                    TriggerFlag[Counter] = (Int16)(((datalogFlag & mask) != 0) ? 1 : 0);
                    Counter++;
                }

                if (mask == 0x8000)
                {
                    mask = 0x0001;
                }
                else
                {
                    mask = (UInt16)(mask << 1);
                }
            }

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Valid"></param>
        /// <param name="StaticHistory"></param>
        /// <param name="DynamicHistory"></param>
        /// <param name="MaxTasks"></param>
        /// <param name="MaxEventsPerTask"></param>
        /// <returns></returns>
        public CommunicationError GetFltHistInfo(Int16[] Valid, Int16[] StaticHistory, Int16[] DynamicHistory,
                                                 Int16 MaxTasks, Int16 MaxEventsPerTask)
        {
            Int16 NumberOfEntries = 0;

            // Loop through all the legal TaskId/FaultId Combinations and pull the histories for each combination
            for (Int16 TaskId = 0; TaskId < MaxTasks; TaskId++)
            {
                for (Int16 EventId = 0; EventId < MaxEventsPerTask; EventId++)
                {
                    if (Valid[(TaskId * MaxEventsPerTask) + EventId] != 0)
                    {
                        ProtocolPTU.GetFaultHistoryReq request = new ProtocolPTU.GetFaultHistoryReq((UInt16)TaskId, (UInt16)EventId);

                        CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, request, m_RxMessage);

                        if (commError != CommunicationError.Success)
                        {
                            return commError;
                        }

                        StaticHistory[NumberOfEntries] = BitConverter.ToInt16(m_RxMessage, 8);
                        DynamicHistory[NumberOfEntries] = BitConverter.ToInt16(m_RxMessage, 10);
                        if (m_CommDevice.IsTargetBigEndian())
                        {
                            StaticHistory[NumberOfEntries] = Utils.ReverseByteOrder(StaticHistory[NumberOfEntries]);
                            DynamicHistory[NumberOfEntries] = Utils.ReverseByteOrder(DynamicHistory[NumberOfEntries]);
                        }
                        NumberOfEntries++;
                    }
                }
            }

            return CommunicationError.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="StreamNumber"></param>
        /// <param name="DatalogBuffer"></param>
        /// <param name="TimeOrigin"></param>
        /// <param name="NumberOfVariables"></param>
        /// <param name="NumberOfSamples"></param>
        /// <param name="VariableType"></param>
        /// <returns></returns>
        public CommunicationError GetStream(Int16 StreamNumber, Int32[] DatalogBuffer, out Int16 TimeOrigin,
                                            Int16 NumberOfVariables, Int16 NumberOfSamples, Int16[] VariableType)
        {
            TimeOrigin = -1;

            ProtocolPTU.GetDatalogBufferReq request = new ProtocolPTU.GetDatalogBufferReq((UInt16)StreamNumber);

            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, request, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            TimeOrigin = BitConverter.ToInt16(m_RxMessage, 8);
            UInt16 SourceSize = BitConverter.ToUInt16(m_RxMessage, 10);

            if (m_CommDevice.IsTargetBigEndian())
            {
                TimeOrigin = Utils.ReverseByteOrder(TimeOrigin);
                SourceSize = Utils.ReverseByteOrder(SourceSize);
            }

            // Initialize Counters
            UInt16 ByteCount = 12;
            UInt16 DestCount = 0;

            // Loop through Source Buffer
            while (ByteCount < SourceSize)
            {
                // Loop through the variables
                for (UInt16 Index = 0; Index < (UInt16)NumberOfVariables; Index++)
                {
                    // Make sure we don't go over destination buffer limits
                    if (DestCount >= NumberOfSamples * NumberOfVariables)
                    {
                        //TODO return E_STREAM_CORRUPT;
                    }

                    // Grab number of bytes depending on variable type
                    switch ((ProtocolPTU.VariableType)VariableType[Index])
                    {
                        case ProtocolPTU.VariableType.INT_8_TYPE:
                            SByte i8 = (SByte)m_RxMessage[ByteCount];
                            DatalogBuffer[DestCount++] = (Int32)i8;
                            ByteCount++;
                            break;

                        case ProtocolPTU.VariableType.UINT_8_TYPE:
                            Byte u8 = (Byte)m_RxMessage[ByteCount];
                            DatalogBuffer[DestCount++] = (Int32)u8;
                            ByteCount++;
                            break;

                        case ProtocolPTU.VariableType.INT_16_TYPE:
                            Int16 i16 = BitConverter.ToInt16(m_RxMessage, ByteCount);
                            if (m_CommDevice.IsTargetBigEndian())
                            {
                                Utils.ReverseByteOrder(i16);
                            }
                            DatalogBuffer[DestCount++] = (Int32)i16;
                            ByteCount += 2;
                            break;

                        case ProtocolPTU.VariableType.UINT_16_TYPE:
                            UInt16 u16 = BitConverter.ToUInt16(m_RxMessage, ByteCount);
                            if (m_CommDevice.IsTargetBigEndian())
                            {
                                Utils.ReverseByteOrder(u16);
                            }
                            DatalogBuffer[DestCount++] = (Int32)u16;
                            ByteCount += 2;
                            break;

                        case ProtocolPTU.VariableType.INT_32_TYPE:
                            Int32 i32 = BitConverter.ToInt32(m_RxMessage, ByteCount);
                            if (m_CommDevice.IsTargetBigEndian())
                            {
                                Utils.ReverseByteOrder(i32);
                            }
                            DatalogBuffer[DestCount++] = i32;
                            ByteCount += 4;
                            break;

                        case ProtocolPTU.VariableType.UINT_32_TYPE:
                            UInt32 u32 = BitConverter.ToUInt32(m_RxMessage, ByteCount);
                            if (m_CommDevice.IsTargetBigEndian())
                            {
                                Utils.ReverseByteOrder(u32);
                            }
                            DatalogBuffer[DestCount++] = (Int32)u32;
                            ByteCount += 4;
                            break;

                        default:
                            //TODO return E_STREAM_CORRUPT;
                            break;
                    }
                }
                //TODO Not sure why this is here and just bump the counter up by 4 for all data sizes
                // Account for left over bytes
                if ((ByteCount % 4) != 0)
                {
                    ByteCount += (UInt16)(4 - (ByteCount % 4));
                }
            }

            return CommunicationError.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="StreamNumber"></param>
        /// <param name="NumberOfVariables"></param>
        /// <param name="NumberOfSamples"></param>
        /// <param name="SampleRate"></param>
        /// <param name="VariableIndex"></param>
        /// <param name="VariableType"></param>
        /// <returns></returns>
        public CommunicationError GetStreamInformation(Int16 StreamNumber, out Int16 NumberOfVariables, out Int16 NumberOfSamples,
                                                        out Int16 SampleRate, Int16[] VariableIndex, Int16[] VariableType)
        {
            NumberOfVariables = -1;
            NumberOfSamples = -1;
            SampleRate = -1;

            ProtocolPTU.GetStreamInfoReq request = new ProtocolPTU.GetStreamInfoReq(StreamNumber);
            CommunicationError commError = m_VcuCommunication.SendDataRequestToEmbedded(m_CommDevice, request, m_RxMessage);

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            NumberOfVariables = BitConverter.ToInt16(m_RxMessage, 8);
            NumberOfSamples = BitConverter.ToInt16(m_RxMessage, 10);
            SampleRate = BitConverter.ToInt16(m_RxMessage, 12);

            if (m_CommDevice.IsTargetBigEndian())
            {
                NumberOfVariables = Utils.ReverseByteOrder(NumberOfVariables);
                NumberOfSamples = Utils.ReverseByteOrder(NumberOfVariables);
                SampleRate = Utils.ReverseByteOrder(NumberOfVariables);
            }

            if (NumberOfVariables > MAX_DL_VARIABLES)
            {
                NumberOfVariables = MAX_DL_VARIABLES;
            }

            for (UInt16 Counter = 0; Counter < NumberOfVariables; Counter++)
            {
                VariableIndex[Counter] = BitConverter.ToInt16(m_RxMessage, (Counter * 4) + 14);
                VariableType[Counter] = BitConverter.ToInt16(m_RxMessage, (Counter * 4) + 16);
                if (m_CommDevice.IsTargetBigEndian())
                {
                    VariableIndex[Counter] = Utils.ReverseByteOrder(VariableIndex[Counter]);
                    VariableType[Counter] = Utils.ReverseByteOrder(VariableType[Counter]);
                }
            }

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public CommunicationError InitializeEventLog()
        {
            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, ProtocolPTU.PacketType.INITIALIZE_EVENTLOG);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="NumberOfFaults"></param>
        /// <param name="OldestIndex"></param>
        /// <param name="NewestIndex"></param>
        /// <returns></returns>
        public CommunicationError LoadFaultLog(out Int16 NumberOfFaults, out UInt32 OldestIndex, out UInt32 NewestIndex)
        {
            NumberOfFaults = Int16.MaxValue;
            OldestIndex = EMPTY_FAULT_BUFFER;
            NewestIndex = EMPTY_FAULT_BUFFER;

            CommunicationError commError;

            // LOOP ONCE ... EXIT ON ERROR
            do
            {
                m_CurrentNumberOfFaults = 0;
                UInt32 FaultCounter = 0;

                // Disable Fault Logging
                commError = SetFaultLog(false);
                if (commError != CommunicationError.Success)
                {
                    break;
                }

                // Get Fault Log Indexes
                commError = GetFaultIndices(out OldestIndex, out NewestIndex);
                if (commError != CommunicationError.Success)
                {
                    break;
                }

                // Check if  Fault Log is Empty
                // TODO Make const out of Empty Fault Log (EMPTY_FAULT_BUFFER)
                if ((OldestIndex == EMPTY_FAULT_BUFFER) && (NewestIndex == EMPTY_FAULT_BUFFER))
                {
                    NumberOfFaults = 0;
                    break;
                }

                UInt32 RemoteFaults;
                if (NewestIndex < OldestIndex)
                {
                    RemoteFaults = 0x10000 + NewestIndex - OldestIndex + 1;
                }
                else
                {
                    RemoteFaults = NewestIndex - OldestIndex + 1;
                }

                if (RemoteFaults == 0)
                {
                    break;
                }

                // GetFaultData can only get a max of MAXFAULTBUFFERSIZE bytes of Data. So if there are
                // more than faults in the Fault Log than this it has do this several times to get ALL the fault data
                do
                {
                    // Get the Fault Data
                    commError = GetFaultData((OldestIndex + FaultCounter) & 0xFFFF, (UInt16)(RemoteFaults - FaultCounter));
                    if (commError != CommunicationError.Success)
                    {
                        break;
                    }

                    if (m_FaultDataFromTarget.BufferSize == 0)
                    {
                        break;
                    }

                    // Loop through the fault buffer, pulling out the size and data for each fault
                    for (Int32 Index = 0; Index < m_FaultDataFromTarget.BufferSize; )
                    {
                        FaultCounter++;

                        Int16 FaultSize;
                        // Get the size of the next fault
                        FaultSize = BitConverter.ToInt16(m_FaultDataFromTarget.Buffer, Index);

                        if (m_CommDevice.IsTargetBigEndian())
                        {
                            FaultSize = Utils.ReverseByteOrder(FaultSize);
                        }

                        // Allocate jagged array dynamically and store fault data there
                        if (FaultSize < MAX_FAULT_SIZE_BYTES && FaultSize > 0)
                        {
                            // Add new member with size "FaultSize" to jagged 2 dimensional array
                            m_faultStorage[m_CurrentNumberOfFaults] = new Byte[FaultSize + 2];
                            // Copy all data into newly created array
                            Buffer.BlockCopy(m_FaultDataFromTarget.Buffer, Index, m_faultStorage[m_CurrentNumberOfFaults], 0, FaultSize + 2);

                            m_CurrentNumberOfFaults++;
                        }
                        else
                        {
                            // Fault Buffer is corrupt beyond hope at this point
                            commError = CommunicationError.UnknownError;
                            break;
                        }

                        // Increment the Index to point to the size of the next fault
                        Index += (FaultSize + 2);
                    }
                } while ((FaultCounter < RemoteFaults) && (commError != CommunicationError.UnknownError));

                // Force the Return Code so we can extract all valid faults
                commError = CommunicationError.Success;

                // Save the number of good faults we retrieved
                NumberOfFaults = m_CurrentNumberOfFaults;
            } while (false);

            // Enable Fault Logging here in case we left the while loop early
            SetFaultLog(true);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="NumberOfVariables"></param>
        /// <param name="SampleRate"></param>
        /// <param name="VariableIndex"></param>
        /// <returns></returns>
        public CommunicationError SetDefaultStreamInformation(Int16 NumberOfVariables, Int16 SampleRate, Int16[] VariableIndex)
        {
            if (NumberOfVariables > MAX_DL_VARIABLES)
            {
                NumberOfVariables = MAX_DL_VARIABLES;
            }

            ProtocolPTU.SetStreamInfoReq request = new ProtocolPTU.SetStreamInfoReq(NumberOfVariables, SampleRate, VariableIndex);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="TaskNumber"></param>
        /// <param name="FaultNumber"></param>
        /// <param name="EnableFlag"></param>
        /// <param name="DatalogFlag"></param>
        /// <returns></returns>
        public CommunicationError SetFaultFlags(Int16 TaskNumber, Int16 FaultNumber, Int16 EnableFlag, Int16 DatalogFlag)
        {
            ProtocolPTU.SetFaultFlagReq request = new ProtocolPTU.SetFaultFlagReq(TaskNumber, FaultNumber, EnableFlag, DatalogFlag);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public CommunicationError SetFaultLog(Boolean enable)
        {
            Byte faultLogEnable = (Byte)((enable == true) ? 1 : 0);

            ProtocolPTU.SetFaultLogReq request = new ProtocolPTU.SetFaultLogReq(faultLogEnable);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

            return commError;
        }

        #endregion --- Public Methods ---

        #region --- Private Methods ---

        /// <summary>
        ///
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private Boolean VerifyDate(Byte month, Byte day, Byte year)
        {
            if ((month < 1) || (month > 12))
            {
                return false;
            }

            if ((day < 1) || (day > 31))
            {
                return false;
            }

            if ((year < 00) || (year > 99))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hr"></param>
        /// <param name="min"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        private Boolean VerifyTime(Byte hr, Byte min, Byte sec)
        {
            if ((hr < 0) || (hr > 23))
            {
                return false;
            }

            if ((min < 0) || (min > 59))
            {
                return false;
            }

            if ((sec < 0) || (sec > 59))
            {
                return false;
            }

            return true;
        }

        #endregion --- Private Methods ---

        #endregion --- Methods ---
    }
}