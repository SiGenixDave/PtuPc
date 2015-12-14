using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VcuComm;

namespace Common.Communication
{
    class PtuTargetCommunication
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commDevice"></param>
        /// <param name="requestObj"></param>
        /// <param name="rxMessage"></param>
        /// <returns></returns>
        public CommunicationError SendDataRequestToEmbedded(ICommDevice commDevice, ICommRequest requestObj, Byte []rxMessage)
        {

            // Send the SOM and receive it
            CommunicationError commError = (CommunicationError)commDevice.SendReceiveSOM();

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            Byte[] txMessage = requestObj.GetByteArray(commDevice.IsTargetBigEndian());

            Int32 errorCode = commDevice.SendMessageToTarget(txMessage);
            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            errorCode = commDevice.ReceiveTargetDataPacket(rxMessage);
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commDevice"></param>
        /// <param name="packetRequestType"></param>
        /// <param name="rxMessage"></param>
        /// <returns></returns>
        public CommunicationError SendDataRequestToEmbedded(ICommDevice commDevice, ProtocolPTU.PacketType packetRequestType, Byte []rxMessage)
        {

            // Send the SOM and receive it
            CommunicationError commError = (CommunicationError)commDevice.SendReceiveSOM();

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            ProtocolPTU.DataPacketProlog dpp = new ProtocolPTU.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, packetRequestType, ProtocolPTU.ResponseType.DATAREQUEST, commDevice.IsTargetBigEndian());

            Int32 errorCode = commDevice.SendMessageToTarget(txMessage);

            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            errorCode = commDevice.ReceiveTargetDataPacket(rxMessage);
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;
        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="txMessage"></param>
        /// <returns></returns>
        public CommunicationError SendCommandToEmbedded(ICommDevice commDevice, ProtocolPTU.PacketType packetRequestType)
        {
            // Send the SOM and receive it
            CommunicationError commError = (CommunicationError)commDevice.SendReceiveSOM();

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            ProtocolPTU.DataPacketProlog dpp = new ProtocolPTU.DataPacketProlog();

            Byte[] txMessage = dpp.GetByteArray(null, packetRequestType, ProtocolPTU.ResponseType.COMMANDREQUEST, commDevice.IsTargetBigEndian());

            Int32 errorCode = commDevice.SendMessageToTarget(txMessage);

            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            errorCode = commDevice.ReceiveTargetAcknowledge();
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="txMessage"></param>
        /// <returns></returns>
        public CommunicationError SendCommandToEmbedded(ICommDevice commDevice, ICommRequest requestObj)
        {
            // Send the SOM and receive it
            CommunicationError commError = (CommunicationError)commDevice.SendReceiveSOM();

            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            Byte[] txMessage = requestObj.GetByteArray(commDevice.IsTargetBigEndian());

            Int32 errorCode = commDevice.SendMessageToTarget(txMessage);
            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            errorCode = commDevice.ReceiveTargetAcknowledge();
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;
        }

    }
}
