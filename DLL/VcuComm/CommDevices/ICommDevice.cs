using System;

namespace VcuComm
{
    /// <summary>
    /// Interface used to abstract calls so that any hardware connection (RS-232, TCP, etc) implements
    /// these same methods.
    /// </summary>
    public interface ICommDevice
    {

        /// <summary>
        /// Attempts to opens a port and establish communication with a target device
        /// </summary>
        /// <param name="commaDelimitedOptions">string contains comma delimited options based on the port being opened</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 Open(String commaDelimitedOptions);

        /// <summary>
        /// Sends the start of message delimiter to the target
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 SendStartOfMessage();

        /// <summary>
        /// Received the start of message delimiter from the target. The value of the start of message indicates
        /// the "endianness" of the target.
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 ReceiveStartOfMessage();

        /// <summary>
        /// Sends a message to the target
        /// </summary>
        /// <param name="txMessage">message that is sent to the target</param>
        /// <returns>less than 0 if any failure occurs; number of bytes sent otherwise</returns>
        Int32 SendMessageToTarget(Byte[] txMessage);

        /// <summary>
        /// Receives a message from the target
        /// </summary>
        /// <param name="rxMessage">the message received from the target is placed here</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 ReceiveTargetDataPacket(Byte[] rxMessage, out Int32 bytesReceived);

        /// <summary>
        /// Receives an acknowledge from the target
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 ReceiveTargetAcknowledge();

        /// <summary>
        /// Closes the hardware device (TCP connection, RS-232 port, etc.)
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 Close();

        /// <summary>
        /// Determines if the target is a big endian or little endian machine
        /// </summary>
        /// <returns>true if target is Big Endian; false otherwise</returns>
        Boolean IsTargetBigEndian();
    }
}