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
        private UInt16 m_CurrentNumberOfFaults;

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
        public CommunicationError GetFaultIndices(ref UInt32 Oldest, ref UInt32 Newest)
        {
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

#if DAS
typedef struct date_time_type
{
    unsigned char  hr;
    unsigned char  min;
    unsigned char  sec;
    unsigned char  month;
    unsigned char  day;
    unsigned char  year;
} DATETIMETYPE;

struct flthdr
{
    UINT16			faultnum;
    UINT16			tasknum;
    unsigned long	index;
    DATETIMETYPE	datetime;
    UINT16			datalognum;
};

        public CommunicationError GetFaultHdr(Int16	index, ref Int16 faultnum, ref Int16 tasknum,
	                                          ref String Flttime, ref String Fltdate, ref Int16 datalognum)
        {
	        //struct flthdr	FaultHeader;
	        String			TempString;
	        Byte            []FaultBuffer = new Byte[MAX_FAULT_BUFFER_SIZE];
	        Int16			FaultStartLocation;

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

            m_faultStorage[index][0];

	        /* Check Time */
	        if (VerifyTime(	(INT16)FaultHeader.datetime.hr,
					        (INT16)FaultHeader.datetime.min,
					        (INT16)FaultHeader.datetime.sec))
	        {
		        wsprintf((_TCHAR *)TempString, "%.2d:%.2d:%.2d",
				         (INT16)FaultHeader.datetime.hr,
				         (INT16)FaultHeader.datetime.min,
				         (INT16)FaultHeader.datetime.sec);
	        }
	        else
	        {
		        _tcscpy_s(TempString, "N/A");
	        }

	        // The Flttime and Fltdate output parameters must be passed as a BSTR type for them 
	        // to be accessible in managed C#. Used the '_bstr_t()' approach as there were problems 
	        // associated with SysReAllocString().

	        //VBSetHlstr(&Flttime, TempString, strlen(TempString));
	        //SysReAllocString(Flttime, (BSTR)TempString);
	        *Flttime = _bstr_t(TempString).copy();

	        /* Check Date */
	        if (VerifyDate(	(INT16)FaultHeader.datetime.month,
					        (INT16)FaultHeader.datetime.day,
					        (INT16)FaultHeader.datetime.year))
	        {
		        wsprintf(	(_TCHAR *)TempString,
		        "%.2d/%.2d/%.2d",
		        (INT16)FaultHeader.datetime.month,
		        (INT16)FaultHeader.datetime.day,
		        (INT16)FaultHeader.datetime.year );
	        }
	        else
	        {
		        _tcscpy_s(TempString, "N/A");
	        }

	        //SysReAllocString(Fltdate, (BSTR)TempString);
	        *Fltdate = _bstr_t(TempString).copy();
	        *datalognum = MAPINT((INT16)FaultHeader.datalognum);
	        *faultnum 	= MAPINT((INT16)FaultHeader.faultnum);
	        *tasknum	= MAPINT((INT16)FaultHeader.tasknum);

	        return CommunicationError.Success;
        }

#endif



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


        public CommunicationError LoadFaultLog(ref UInt16 NumberOfFaults, ref UInt32 OldestIndex, ref UInt32 NewestIndex)
        {

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
                commError = GetFaultIndices (ref OldestIndex, ref NewestIndex);
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
                            m_faultStorage[m_CurrentNumberOfFaults] = new Byte[FaultSize];
                            // Copy all data into newly created array
                            Buffer.BlockCopy(m_FaultDataFromTarget.Buffer, Index, m_faultStorage[m_CurrentNumberOfFaults], 0, FaultSize);

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
    }
}