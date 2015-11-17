using VcuComm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System;
using System.Diagnostics;
using Common;
using Common.Communication;



namespace VcuCommUnitTest
{
    [TestClass]
    public class UnitTest1
    {
#if DAS
        [TestMethod]
        public void TestMethod1()
        {
            TCP device = new TCP();
            device.Open("127.0.0.1");

            Comm comm = new Comm(device);

            Double []values = new Double[40];
            short []types = new short[40];
            comm.UpdateWatchElements(1, values, types);

        }
#endif

#if !DAS
        [TestMethod]
        public void TestMethod1()
        {
#if TCP
            TCP device = new TCP();
            device.Open("127.0.0.1");
            //device.Open("10.0.1.21");
#else            
            Serial device = new Serial();
            device.Open("COM2,19200,none,8,1");
#endif            
            
            CommGen comm = new CommGen(device);

            CommunicationError errorCode;
            //errorCode = comm.SetCarID(0x1234);
            //if (errorCode != CommunicationError.Success)
            //{
            //    Debug.Print("Set Car ID Failed --- Error: " + errorCode);
            //}

            VcuComm.ProtocolPTU.GetEmbeddedInfoRes emdRes = new VcuComm.ProtocolPTU.GetEmbeddedInfoRes();
            errorCode = comm.GetEmbeddedInformation(ref emdRes);
            if (errorCode != CommunicationError.Success)
            {
                Debug.Print("Read Embedded Info Failed --- Error: " + errorCode);
            }
            else
            {
                Debug.Print(emdRes.SoftwareVersion);
                Debug.Print("Error Message = " + device.Error);
                Debug.Print("Exception Message = " + device.ExceptionMessage);
            }
        }
#endif

#if DAS
        [TestMethod]
        public void TestMethod1()
        {
            String[] serPorts = Serial.GetAvailableSerialPorts();

            foreach (String s in serPorts)
            {
                Debug.WriteLine(s);
            }

            Serial sp = new Serial();
            String cdo = serPorts[0] + ", 19200, none, 8, 1";
            sp.Open(cdo);
            Int16 result = sp.SendStartOfMessage();
            if (result < 0)
            {
                Debug.WriteLine(sp.SerialError);
                Debug.WriteLine(sp.ExceptionMessage);
            }
            else
            {
                Debug.WriteLine("SOM sent successfully");
            }
            sp.Close();

            TCP tcpDevice = new TCP();
            Comm comm = new Comm(tcpDevice);

            tcpDevice.Open("localhost");

            Protocol.GetEmbeddedInfoRes emdRes = new Protocol.GetEmbeddedInfoRes();
            comm.GetEmbeddedInformation(ref emdRes);

            Debug.Print(emdRes.SoftwareVersion);

        }
#endif

        
#if DAS
        [TestMethod]
        public void TestMethod4()
        {
            TCP tcpDevice1 = new TCP();
            TCP tcpDevice2 = new TCP();

            Protocol.SetWatchElementReq setWatch = new Protocol.SetWatchElementReq(1,2);

            Protocol.DataPacketProlog dp = new Protocol.DataPacketProlog();

            Byte []myTCP = dp.GetByteArray (null, (Protocol.PacketType)(0x1234), (Protocol.ResponseType)(0x5678), false);
            myTCP = dp.GetByteArray(null, (Protocol.PacketType)(0x1122), (Protocol.ResponseType)(0x3344), true);


            tcpDevice1.Open("www.google.com");
            tcpDevice2.Open("localhost");
            tcpDevice2.SendStartOfMessage();
            tcpDevice2.ReceiveStartOfMessage();
           
            
            tcpDevice2.Close(null);
        }
#endif
    }

}