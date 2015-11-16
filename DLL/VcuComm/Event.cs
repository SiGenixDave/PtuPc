using System;
using System.Text;
using Common.Communication;

namespace VcuComm
{
    public class Event
    {
        private ICommDevice m_CommDevice;

        private Byte[] m_RxMessage = new Byte[4096];

        const UInt16 MAX_FAULT_BUFFER_SIZE = 4096;

        private Event()
        {}

        public Event(ICommDevice device)
        {
            m_CommDevice = device;
        }

        private CommunicationError SetFaultLog (Boolean enable)
        {
            Byte faultLogEnable = (Byte)((enable == true) ? 1 : 0);

        	Protocol.SetFaultLogReq request = new Protocol.SetFaultLogReq(faultLogEnable);

            Byte[] txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());
            m_CommDevice.SendMessageToTarget(txMessage);

	        return CommunicationError.Success;

        }

        private CommunicationError GetFaultIndices(ref UInt32 Oldest, ref UInt32 Newest)
        {
            Protocol.DataPacketProlog request = new Protocol.DataPacketProlog();

            Byte[] txMessage = request.GetByteArray(null, Protocol.PacketType.GET_FAULT_INDICES, Protocol.ResponseType.COMMANDREQUEST, m_CommDevice.IsTargetBigEndian());
            m_CommDevice.SendMessageToTarget(txMessage);

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);

            Oldest = BitConverter.ToUInt32(m_RxMessage, 8);
            Newest = BitConverter.ToUInt32(m_RxMessage, 12);

            return CommunicationError.Success;
        }

        private CommunicationError GetFaultData(UInt32 FaultIndex, UInt16 NumberOfFaults, ref Protocol.GetFaultDataRes getFaultData)
        {

            Protocol.GetFaultDataReq request = new Protocol.GetFaultDataReq(FaultIndex, NumberOfFaults);
            Byte []txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());
            m_CommDevice.SendMessageToTarget(txMessage);

            m_CommDevice.ReceiveTargetDataPacket(m_RxMessage);

            getFaultData.BufferSize = BitConverter.ToUInt16(m_RxMessage, 8);
            if (m_CommDevice.IsTargetBigEndian())
            {
                Utils.ReverseByteOrder(getFaultData.BufferSize);
            }

            if (getFaultData.BufferSize > MAX_FAULT_BUFFER_SIZE)
            {
                //TODO return error
            }

            if (getFaultData.BufferSize == 0)
            {
                //TODO return noERROR
            }

            // Copy the entire response into the fault data buffer 

		    /* Load the Fault Buffer with the latest Faults */
            for (UInt16 index = 0; index < getFaultData.BufferSize; index++)
		    {
                // TODO investigate GlobalIndex updates and address it
                //getFaultData[GlobalIndex + Counter] = Response.Buffer[Counter];
                getFaultData.Buffer[index] = m_RxMessage[index + 8 + sizeof(UInt16)];
            }


            return CommunicationError.Success;
        }

        public CommunicationError SetFaultFlags(UInt16 TaskNumber, UInt16 FaultNumber, Byte EnableFlag, Byte DatalogFlag)
        {
	        Protocol.SetFaultFlagReq request  = new Protocol.SetFaultFlagReq(TaskNumber, FaultNumber, EnableFlag, DatalogFlag);

            Byte []txMessage = request.GetByteArray(m_CommDevice.IsTargetBigEndian());

            m_CommDevice.SendMessageToTarget(txMessage);

            return CommunicationError.Success;
        }

        public CommunicationError GetStreamInformation(UInt16 StreamNumber, ref Protocol.GetStreamInfoRes response, Int16 []VariableIndex, Int16 []VariableType)
        {
	        UInt16 Counter;
	        Protocol.GetStreamInfoReq request = new Protocol.GetStreamInfoReq(StreamNumber);

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


#if DAS
        public CommunicationError LoadFaultLog(ref UInt16 NumberOfFaults, ref UInt32 OldestIndex, ref UInt32 NewestIndex)
        {
	            Int16					FaultSize;
	            Int16					ReturnCode;
	            Int16			 		Index;
	            UInt32		            RemoteFaults;
	            UInt32                  FaultCounter;
	            Int16                   BufferSize;
	            char                    []FaultBuffer;
	            Int16                   FaultStartLocation;

	            /* LOOP ONCE ... EXIT ON ERROR */
	            do
	            {
		            CurrentNumberOfFaults	= 0;
		            GlobalIndex				= 0;
		            ReturnCode 				= NOERROR;
		            hFBuf 					= NULL;
		            FaultCounter 			= 0;
		            LocationSize			= MAXNUMOFFAULTS * 2;
		            hFaultStartLocation		= GlobalAlloc(GMEM_MOVEABLE | GMEM_ZEROINIT,
			            LocationSize);
		            if (hFaultStartLocation == NULL)
		            {
			            ReturnCode = E_MEM_ALLOC;
			            break;
		            }
		            FaultStartLocation = (INT16 *)GlobalLock(hFaultStartLocation);

		            /* Disable Fault Logging */
		            if (ReturnCode = SetFaultLog(DISABLE)) break;

		            /* Get Fault Log Indices */
		            if (ReturnCode = GetFaultIndices(OldestIndex, NewestIndex))
			            break;

		            /* Check if  Fault Log is Empty */
		            if ( (*OldestIndex == 0xffffffff) &&
			            (*NewestIndex == 0xffffffff) )
		            {
			            *NumberOfFaults = 0;
			            break;
		            }

        // #ifndef DAS_FIX
		            if (*NewestIndex < *OldestIndex)
		            {
			            RemoteFaults = 0x10000 + *NewestIndex - *OldestIndex + 1;
		            }
		            else
		            {
			            RemoteFaults = *NewestIndex - *OldestIndex + 1;
		            }

		            if (RemoteFaults == 0)
		            {
			            break;
		            }

            //#else

		            /* Compute number of Faults */
		            if ((RemoteFaults = (*NewestIndex - *OldestIndex + 1)) == 0)
			            break;
            //#endif
		            /* GetFaultData can only get a max of MAXFAULTBUFFERSIZE bytes of */
		            /* Data. So if there are more than faults in the Fault Log than */
		            /* this it has do this several times to get ALL the fault data */
		            do
		            {
			            /* Get the Fault Data */
			            if (ReturnCode = GetFaultData(*OldestIndex + FaultCounter,
				            RemoteFaults - FaultCounter,
				            &BufferSize))
				            break;

			            if (BufferSize == 0) break;

			            FaultBuffer = (_TUCHAR *)GlobalLock(hFBuf);

			            /* Loop thru the fault buffer, pulling out the size and data */
			            /* for each fault */
			            for (Index = 0; Index < BufferSize;)
			            {
				            FaultCounter++;

				            /* Get the size of the next fault */
				            FaultSize = MAPINT(*(INT16 *)&FaultBuffer[Index + GlobalIndex]);

				            /* Add the fault to the FDT */
				            if (FaultSize < MAXFAULTSIZE && FaultSize > 0)
				            {
					            FaultStartLocation[CurrentNumberOfFaults] = Index + (INT16)GlobalIndex;
 					            if (++CurrentNumberOfFaults >= (LocationSize / 2))
					            {
						            GlobalUnlock(hFaultStartLocation);
						            LocationSize += (MAXNUMOFFAULTS * 2);
						            hFaultStartLocation = GlobalReAlloc(hFaultStartLocation,
							            LocationSize,
							            GMEM_MOVEABLE|GMEM_ZEROINIT);
						            FaultStartLocation = (INT16 *)GlobalLock(hFaultStartLocation);
					            }
				            }
				            else
				            {
					            /* Fault Buffer is corrupt beyond hope at this point */
					            ReturnCode = E_FLT_CORRUPT;
					            break;
				            }

				            /* Increment the Index to point to the size of the next fault */
				            Index += (FaultSize + 2);
			            }

			            /* Increment the Global Index to maintain the size of the buffer */
			            GlobalIndex += Index;

		            } while ((FaultCounter < RemoteFaults) && (ReturnCode == NOERROR));

		            /* Force the Return Code so we can extract all valid faults */
		            ReturnCode = NOERROR;

		            /* Save the number of good faults we retrieved */
		            *NumberOfFaults = CurrentNumberOfFaults;

	            } while (FALSE);

	            /* Enable Fault Logging here in case we left the while loop early */
	            SetFaultLog(ENABLE);

	            if (hFBuf != NULL) GlobalUnlock(hFBuf);
	            if (hFaultStartLocation != NULL) GlobalUnlock(hFaultStartLocation);

	            return ReturnCode;
            }

        }




        public CommunicationError GetEmbeddedInformation(ref Protocol.GetEmbeddedInfoRes getEmbInfo)
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.GET_EMBEDDED_INFORMATION, Protocol.ResponseType.DATAREQUEST, false);

            m_CommDevice.SendDataToTarget(txMessage);

            Byte[] rxMessage;
            m_CommDevice.ReceiveTargetDataPacket(out rxMessage);

            // Map rxMessage to GetEmbeddedInfoRes;
            getEmbInfo.SoftwareVersion = Encoding.UTF8.GetString(rxMessage, 8, 41).Replace("\0", String.Empty);
            getEmbInfo.CarID = Encoding.UTF8.GetString(rxMessage, 49, 11).Replace("\0", String.Empty);
            getEmbInfo.SubSystemName = Encoding.UTF8.GetString(rxMessage, 60, 41).Replace("\0", String.Empty);
            getEmbInfo.IdentifierString = Encoding.UTF8.GetString(rxMessage, 101, 5).Replace("\0", String.Empty);
            getEmbInfo.ConfigurationMask = BitConverter.ToUInt32(rxMessage, 106);

            return CommunicationError.Success;
        }

        public CommunicationError GetChartMode(ref Protocol.GetChartModeRes getChartMode)
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.GET_CHART_MODE, Protocol.ResponseType.DATAREQUEST, false);

            m_CommDevice.SendDataToTarget(txMessage);
            
            Byte[] rxMessage;
            m_CommDevice.ReceiveTargetDataPacket(out rxMessage);


            // Map rxMessage to GetEmbeddedInfoRes;
            getChartMode.CurrentChartMode = rxMessage[8];

            return CommunicationError.Success;
        }

        public CommunicationError StartClock()
        {
            Protocol.DataPacketProlog dpp = new Protocol.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, Protocol.PacketType.START_CLOCK, Protocol.ResponseType.COMMANDREQUEST, false);

            m_CommDevice.SendDataToTarget(txMessage);

            return CommunicationError.Success;
        }
#endif        
    }
}