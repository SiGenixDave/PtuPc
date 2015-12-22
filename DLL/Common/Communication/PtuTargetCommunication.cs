using System;
using VcuComm;

namespace Common.Communication
{
    /// <summary>
    ///
    /// </summary>
    internal class PtuTargetCommunication
    {
        /// <summary>
        /// This method is used to send a command to the embedded PTU target using the type of
        /// device specified in the argument. The difference between this method and the 3 parameter
        /// method of the same name is that this method is used when there is no payload with the command.
        /// </summary>
        /// <param name="commDevice">The comm device used to communicate with target</param>
        /// <param name="packetRequestType">The command sent to the target</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SendCommandToEmbedded(ICommDevice commDevice, ProtocolPTU.PacketType packetRequestType)
        {
            // Send the SOM and receive it
            CommunicationError commError = (CommunicationError)commDevice.SendReceiveSOM();

            // Verify the sending and receiving of SOM is /RX OK
            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            // Create the message header for a command and command type; "null" as 1st argument indicates no payload
            ProtocolPTU.DataPacketProlog dpp = new ProtocolPTU.DataPacketProlog();
            Byte[] txMessage = dpp.GetByteArray(null, packetRequestType, ProtocolPTU.ResponseType.COMMANDREQUEST, commDevice.IsTargetBigEndian());

            // Send the command to the target
            Int32 errorCode = commDevice.SendMessageToTarget(txMessage);

            // Verify the command was sent without errors
            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            // Since no return data is expected, verify the embedded target responds with an Acknowledge (implicit
            // acknowledge with TCP, but 232 has no such entity
            errorCode = commDevice.ReceiveTargetAcknowledge();
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;
        }

        /// <summary>
        /// This method is used to send a command to the embedded PTU target using the type of
        /// device specified in the argument. The difference between this method and the 2 parameter
        /// method of the same name is that this method is used when there is a payload with the command.
        /// </summary>
        /// <param name="commDevice">The comm device used to communicate with target</param>
        /// <param name="requestObj">This object is a request that already has the all of the necessary payload
        /// parameters ready to be formed into a message to be sent to embedded target</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SendCommandToEmbedded(ICommDevice commDevice, ICommRequest requestObj)
        {
            // Send the SOM and receive it
            CommunicationError commError = (CommunicationError)commDevice.SendReceiveSOM();

            // Verify the sending and receiving of SOM is /RX OK
            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            // Create the message header and payload for a command and command type
            Byte[] txMessage = requestObj.GetByteArray(commDevice.IsTargetBigEndian());

            // Send the command and payload to the target
            Int32 errorCode = commDevice.SendMessageToTarget(txMessage);

            // Verify the command was sent without errors
            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            // Since no return data is expected, verify the embedded target responds with an Acknowledge (implicit
            // acknowledge with TCP, but 232 has no such entity
            errorCode = commDevice.ReceiveTargetAcknowledge();
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;
        }

        /// <summary>
        /// This method is used to send a data request to the embedded PTU target using the type of
        /// device specified in the argument. The difference between this method and the method of the same name
        /// is that this method is used when there is a payload with the data request.
        /// </summary>
        /// <param name="commDevice">The comm device used to communicate with target</param>
        /// <param name="requestObj">This object is a request that already has the all of the necessary payload
        /// parameters ready to be formed into a message to be sent to embedded target</param>
        /// <param name="rxMessage">Used to store the response from the embedded target</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SendDataRequestToEmbedded(ICommDevice commDevice, ICommRequest requestObj, Byte[] rxMessage)
        {
            // Send the SOM and receive it
            CommunicationError commError = (CommunicationError)commDevice.SendReceiveSOM();

            // Verify the sending and receiving of SOM is /RX OK
            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            // Create the message header and payload for a command and command type
            Byte[] txMessage = requestObj.GetByteArray(commDevice.IsTargetBigEndian());

            // Send the command and payload to the target
            Int32 errorCode = commDevice.SendMessageToTarget(txMessage);
            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            // Verify the target responds with data
            errorCode = commDevice.ReceiveTargetDataPacket(rxMessage);
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;
        }

        /// <summary>
        /// This method is used to send a data request to the embedded PTU target using the type of
        /// device specified in the argument. The difference between this method and the method of the same name
        /// is that this method is used when there is NO payload with the data request.
        /// </summary>
        /// <param name="commDevice">The comm device used to communicate with target</param>
        /// <param name="packetRequestType">The command sent to the target</param>
        /// <param name="rxMessage">Used to store the response from the embedded target</param>
        /// <returns>CommunicationError.Success (0) if all is well; otherwise another enumeration which is less than 0</returns>
        public CommunicationError SendDataRequestToEmbedded(ICommDevice commDevice, ProtocolPTU.PacketType packetRequestType, Byte[] rxMessage)
        {
            // Send the SOM and receive it
            CommunicationError commError = (CommunicationError)commDevice.SendReceiveSOM();

            // Verify the sending and receiving of SOM is /RX OK
            if (commError != CommunicationError.Success)
            {
                return commError;
            }

            // Create the message header for a command and command type; "null" as 1st argument indicates no payload
            ProtocolPTU.DataPacketProlog dpp = new ProtocolPTU.DataPacketProlog();
            Byte[] txMessage = dpp.GetByteArray(null, packetRequestType, ProtocolPTU.ResponseType.DATAREQUEST, commDevice.IsTargetBigEndian());

            // Send the command to the target
            Int32 errorCode = commDevice.SendMessageToTarget(txMessage);
            if (errorCode < 0)
            {
                return CommunicationError.BadRequest;
            }

            // Verify the target responds with data
            errorCode = commDevice.ReceiveTargetDataPacket(rxMessage);
            if (errorCode < 0)
            {
                return CommunicationError.BadResponse;
            }

            return CommunicationError.Success;
        }
    }
}