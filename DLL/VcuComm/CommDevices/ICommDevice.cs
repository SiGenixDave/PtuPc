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
        /// 
        /// </summary>
        ProtocolPTU.Errors Error
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        String ExceptionMessage
        {
            get;
        }


        /// <summary>
        /// Attempts to opens a port and establish communication with a target device
        /// </summary>
        /// <param name="commaDelimitedOptions">string contains comma delimited options based on the port being opened</param>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 Open(String commaDelimitedOptions);

        /// <summary>
        /// Closes the hardware device (TCP connection, RS-232 port, etc.)
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 Close();

        /// <summary>
        /// Sends and receives the Start Of Message (SOM) to/from the target
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 SendReceiveSOM();
 
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
        Int32 ReceiveTargetDataPacket(Byte[] rxMessage);

        /// <summary>
        /// The target is responsible for reporting whether it is a big or little endian machine. The start of
        /// message received from the target indicates the machine type of target
        /// </summary>
        /// <remarks>It is imperative that the calling function perform all error checking prior to invoking this
        /// method. That includes verification that the transmitted SOM was echoed before making assumptions that there
        /// is an embedded PTU connected.
        /// </remarks>
        /// <returns>true if target is Big Endian; false otherwise</returns>
        bool IsTargetBigEndian();

        /// <summary>
        /// Reads the data from the  port and verifies the target acknowledged the message. Target acknowledges
        /// the message sent from the application when no data is sent back from the target (i.e. a command was sent)
        /// </summary>
        /// <returns>less than 0 if any failure occurs; greater than or equal to 0 if successful</returns>
        Int32 ReceiveTargetAcknowledge();


    }
}