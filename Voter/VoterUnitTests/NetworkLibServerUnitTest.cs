using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VoterUnitTests
{
    public class NetworkLibServer
    {
        public NetworkLib.Server server;
        public NetworkLibServer()
        {
            server = new NetworkLib.Server(30002);
        }
        
    }
    [TestClass]
    public class NetworkLibServerUnitTest : NetworkLibServer
    {
        [TestMethod]
        public void ServerTest()
        {
            Assert.IsTrue(server.isStarted());
            server.stopServer();
        }

        [TestMethod]
        public void ListenForMessageTest()
        {
            //server.StartListeningForClients();
            server.stopServer();
        }

        [TestMethod]
        public void ClientConnectionTest()
        {
            server.StartListeningForClients();

        } 
    }
}
