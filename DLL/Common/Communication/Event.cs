using System;
using System.Text;
using Common.Communication;
using VcuComm;

namespace Common.Communication
{
    /// <summary>
    /// 
    /// </summary>
    public class EventGen
    {
        /// <summary>
        /// 
        /// </summary>
        private ICommDevice m_CommDevice;

        /// <summary>
        /// 
        /// </summary>
        const UInt16 MAX_FAULT_BUFFER_SIZE = 4096;

        /// <summary>
        /// 
        /// </summary>
        const Int16 MAX_TASKS = 120;
        
        /// <summary>
        /// 
        /// </summary>
        const Int16 MAX_EVENTS_PER_TASK = 100;

        /// <summary>
        /// 
        /// </summary>
        const Int16 MAX_NUM_FAULTS = 1000;

        /// <summary>
        /// 
        /// </summary>
        const Int16 MAX_FAULT_SIZE_BYTES = 256;

        /// <summary>
        /// 
        /// </summary>
        private Byte[] m_RxMessage = new Byte[MAX_FAULT_BUFFER_SIZE];

        /// <summary>
        /// 
        /// </summary>
        private Byte[][] m_faultStorage = new Byte[MAX_NUM_FAULTS][];


        /// <summary>
        /// 
        /// </summary>
        private VcuCommunication m_VcuCommunication;

        /// <summary>
        /// 
        /// </summary>
        private ProtocolPTU.GetFaultDataRes m_FaultDataFromTarget;

        /// <summary>
        /// 
        /// </summary>
        private Int16 m_CurrentNumberOfFaults;

        /// <summary>
        /// 
        /// </summary>
        private EventGen()
        {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        public EventGen(ICommDevice device)
        {
            m_CommDevice = device;
            m_VcuCommunication = new VcuCommunication();
        }

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
        /// <param name="enable"></param>
        /// <returns></returns>
        public CommunicationError SetFaultLog (Boolean enable)
        {
            Byte faultLogEnable = (Byte)((enable == true) ? 1 : 0);

        	ProtocolPTU.SetFaultLogReq request = new ProtocolPTU.SetFaultLogReq(faultLogEnable);

            CommunicationError commError = m_VcuCommunication.SendCommandToEmbedded(m_CommDevice, request);

	        return commError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Oldest"></param>
        /// <param name="Newest"></param>
        /// <returns></returns>
        public CommunicationError GetFaultIndices(out UInt32 Oldest, out UInt32 Newest)
        {
            Oldest = UInt32.MaxValue;
            Newest = UInt32.MaxValue;

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
        /// <param name="NumberOfFaults"></param>
        /// <param name="getFaultData"></param>
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

        public CommunicationError GetFaultHdr(Int16	index, ref Int16 faultnum, ref Int16 tasknum,
	                                          ref String Flttime, ref String Fltdate, ref Int16 datalognum)
        {
	        /* Check the Validity of the desired index */
	        if (index >= m_CurrentNumberOfFaults)
	        {
		        Flttime = "N/A";
		        Fltdate = "N/A";
		        datalognum = -1;
		        faultnum 	= 0;
		        tasknum	= 0;
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

	        /* Check Date */
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


        public CommunicationError GetFaultVar(Int16 FaultIndex, Int16 NumberOfVariables, Int16 []VariableType, Double []VariableValue)
        {
	        /* Check the Validity of the desired index */
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
                        Char cVal = BitConverter.ToChar(m_faultStorage[FaultIndex], variableOffset);
                        if (m_CommDevice.IsTargetBigEndian())
                        {
                            cVal = Utils.ReverseByteOrder(cVal);
                        }
                        VariableValue[var] = (Double)cVal;
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



        public CommunicationError SetFaultFlags(UInt16 TaskNumber, UInt16 FaultNumber, Byte EnableFlag, Byte DatalogFlag)
        {
	        ProtocolPTU.SetFaultFlagReq request  = new ProtocolPTU.SetFaultFlagReq(TaskNumber, FaultNumber, EnableFlag, DatalogFlag);

            Byte []txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            m_CommDevice.SendMessageToTarget(txMessage);

            return CommunicationError.Success;
        }

        public CommunicationError GetStreamInformation(UInt16 StreamNumber, ref ProtocolPTU.GetStreamInfoRes response, Int16 []VariableIndex, Int16 []VariableType)
        {
	        UInt16 Counter;
	        ProtocolPTU.GetStreamInfoReq request = new ProtocolPTU.GetStreamInfoReq(StreamNumber);

            Byte []txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());
            m_CommDevice.SendMessageToTarget(txMessage);

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);

            response.Information.NumberOfVariables = BitConverter.ToUInt16(m_RxMessage, 8);
            response.Information.NumberOfSamples = BitConverter.ToUInt16(m_RxMessage, 10);
            response.Information.SampleRate = BitConverter.ToUInt16(m_RxMessage, 12);

            if (m_CommDevice.IsTargetBigEndian())
            {
                response.Information.NumberOfVariables = Utils.ReverseByteOrder(response.Information.NumberOfVariables);
                response.Information.NumberOfSamples = Utils.ReverseByteOrder(response.Information.NumberOfVariables);
                response.Information.SampleRate = Utils.ReverseByteOrder(response.Information.NumberOfVariables);
            }

#if TODO            
            if (*NumberOfVariables > MAXDLVARIABLES)
			        *NumberOfVariables = MAXDLVARIABLES;

            for (Counter = 0; Counter < *NumberOfVariables; Counter++)
		    {
			    VariableIndex[Counter] =
				    MAPINT(Response.Information.StreamVariableInfo[Counter].StreamVariable);
			    VariableType[Counter] =
				    MAPINT(Response.Information.StreamVariableInfo[Counter].StreamVariableType);
		    }
#endif
            return CommunicationError.Success;
        }


        public CommunicationError LoadFaultLog(out Int16 NumberOfFaults, out UInt32 OldestIndex, out UInt32 NewestIndex)
        {

            NumberOfFaults = Int16.MaxValue;
            OldestIndex = UInt32.MaxValue;
            NewestIndex = UInt32.MaxValue;

            CommunicationError  commError;

	        /* LOOP ONCE ... EXIT ON ERROR */
	        do
	        {
		        m_CurrentNumberOfFaults	= 0;
                UInt32 FaultCounter = 0;

		        /* Disable Fault Logging */
                commError = SetFaultLog (false);
		        if (commError != CommunicationError.Success)
                {
                    break;
                }

		        /* Get Fault Log Indexes */
                commError = GetFaultIndices (out OldestIndex, out NewestIndex);
		        if (commError != CommunicationError.Success)
                {
                    break;
                }

		        /* Check if  Fault Log is Empty */
		        if ( (OldestIndex == UInt32.MaxValue) && (NewestIndex == UInt32.MaxValue) )
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


			        /* Loop through the fault buffer, pulling out the size and data */
			        /* for each fault */
			        for (Int32 Index = 0; Index < m_FaultDataFromTarget.BufferSize;)
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
					        /* Fault Buffer is corrupt beyond hope at this point */
					        commError = CommunicationError.UnknownError;
					        break;
				        }

                        /* Increment the Index to point to the size of the next fault */
				        Index += (FaultSize + 2);
			        }


                } while ((FaultCounter < RemoteFaults) && (commError != CommunicationError.UnknownError));

		        /* Force the Return Code so we can extract all valid faults */
		        commError = CommunicationError.Success;

		        /* Save the number of good faults we retrieved */
		        NumberOfFaults = m_CurrentNumberOfFaults;

	        } while (false);

	        /* Enable Fault Logging here in case we left the while loop early */
	        SetFaultLog(true);

	        return commError;

        }

        public CommunicationError CheckFaultlogger(ref Int16 PassedNumOfFaults, ref UInt32 orig_new)
        {
            UInt32 		OldestIndex = UInt32.MaxValue;
            UInt32      NewestIndex = UInt32.MaxValue;
	        Int16		RemoteFaults = -1;
            CommunicationError  commError;
            UInt32      FaultIndex;

	        /* LOOP ONCE ... EXIT ON ERROR */
	        do
	        {
		        /* Disable Fault Logging */
                commError = SetFaultLog (false);
		        if (commError != CommunicationError.Success)
                {
                    break;
                }

		        /* Get Fault Log Indexes */
                commError = GetFaultIndices (out OldestIndex, out NewestIndex);
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

		        /* Check if Fault Log is Empty */
		        if ((NewestIndex == UInt32.MaxValue) && (OldestIndex == UInt32.MaxValue))
		        {
			        RemoteFaults = 0;
			        break;
		        }

		        /* Compute number of Faults */
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

		        /* Enable Fault Logging */
		        commError = SetFaultLog(true);
			    if (commError != CommunicationError.Success)
                {
				    break;
                }

		        /* Loop thru the fault buffer, pulling out the size and data */
		        /* for each fault */
		        for (Int32 Index = 0; Index < m_FaultDataFromTarget.BufferSize;)
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
                        /* Fault Buffer is corrupt beyond hope at this point */
                        commError = CommunicationError.UnknownError;
                        break;
                    }

			        /* Increment the Index to point to the size of the next fault */
			        Index += (FaultSize + 2);
		        }

	        } while (false);

	        /* Enable Fault Logging here in case we left the while loop early */
	        commError = SetFaultLog(true);

	        if ((commError == CommunicationError.Success) && (RemoteFaults > 0))
	        {
		        orig_new			= NewestIndex;
		        PassedNumOfFaults	= m_CurrentNumberOfFaults;
	        }

	        return commError;
        }

    }
}