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
            IP ip1 = new IP();
            Comm comm = new Comm(ip1);

            ip1.InitializeSockets("localhost");

            comm.SetCarID(0x1234);

        }
        
        [TestMethod]
        public void TestMethod2()
        {
            IP ip1 = new IP();
            Comm comm = new Comm(ip1);

            ip1.InitializeSockets("localhost");

            Protocol.GetEmbeddedInfoRes emdRes = new Protocol.GetEmbeddedInfoRes();
            comm.GetEmbeddedInformation(ref emdRes);

            Debug.Print(emdRes.SoftwareVersion);

        }
        
        [TestMethod]
        public void TestMethod3()
        {
            IP ip1 = new IP();
            IP ip2 = new IP();

            Protocol.SetWatchElementReq setWatch = new Protocol.SetWatchElementReq(1,2);

            Protocol.DataPacketProlog dp = new Protocol.DataPacketProlog();

            Byte []myTCP = dp.GetByteArray (null, (Protocol.PacketType)(0x1234), (Protocol.ResponseType)(0x5678), false);
            myTCP = dp.GetByteArray(null, (Protocol.PacketType)(0x1122), (Protocol.ResponseType)(0x3344), true);


            ip1.InitializeSockets("www.google.com");
            ip2.InitializeSockets("localhost");
            ip2.SendStartOfMessage();
            ip2.ReceiveStartOfMessage();
           
            
            ip2.TerminateSocket();
        }
    }
}