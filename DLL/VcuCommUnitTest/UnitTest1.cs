using VcuComm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System;
using System.Diagnostics;


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

            Protocol.GetEmbeddedInfoRes emdRes = comm.GetEmbeddedInformation();

            Debug.Print(emdRes.SoftwareVersion);

        }
        
        [TestMethod]
        public void TestMethod2()
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