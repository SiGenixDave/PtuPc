using VcuComm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System;
using System.Diagnostics;
using Common;


namespace VcuCommUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Serial serialDevice = new Serial();
            Comm comm = new Comm(serialDevice);

            serialDevice.Open("COM1,19200,none,8,1");

            Protocol.GetEmbeddedInfoRes emdRes = new Protocol.GetEmbeddedInfoRes();
            comm.GetEmbeddedInformation(ref emdRes);

            Debug.Print(emdRes.SoftwareVersion);

        }

#if DAS
        [TestMethod]
        public void TestMethod2()
        {
            TCP tcpDevice = new TCP();
            Comm comm = new Comm(tcpDevice);

            tcpDevice.Open("localhost");

            comm.SetCarID(0x1234);

        }
        
        [TestMethod]
        public void TestMethod3()
        {
            TCP tcpDevice = new TCP();
            Comm comm = new Comm(tcpDevice);

            tcpDevice.Open("localhost");

            Protocol.GetEmbeddedInfoRes emdRes = new Protocol.GetEmbeddedInfoRes();
            comm.GetEmbeddedInformation(ref emdRes);

            Debug.Print(emdRes.SoftwareVersion);

        }
        
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