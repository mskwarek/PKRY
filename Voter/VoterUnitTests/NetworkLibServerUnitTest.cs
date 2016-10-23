using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Sockets;
using System.Threading;

namespace VoterUnitTests
{
    public class NetworkLibServer
    {
        public NetworkLib.Server server;
        public NetworkLibServer()
        {
            server = new NetworkLib.Server(30003);
        }
        
    }
    [TestClass]
    public class NetworkLibServerUnitTest : NetworkLibServer
    {
        TcpClient ID;
        [TestMethod]
        public void ServerTest()
        {
            Assert.IsTrue(server.isStarted());
        }

        [TestMethod]
        public void ListenForMessageTest()
        {
            server.OnNewClientRequest += Server_OnNewMessageRecived;
            NetworkLib.Client client = new NetworkLib.Client("localhost", "30003");
            Assert.IsTrue(client.isConnected());
            Thread.Sleep(1000);
            client.sendMessage("Client//");
            Thread.Sleep(1000);
            server.sendMessage(this.ID, "test");
            server.stopServer();
        }

        [TestMethod]
        public void ClientTest()
        {
            NetworkLib.Client client = new NetworkLib.Client("127.0.0.1", "30003");
            Assert.IsTrue(client.isConnected());
            client.stopService();
        }

        private void Server_OnNewMessageRecived(object myObject, NetworkLib.ClientArgs myArgs)
        {
            this.ID = myArgs.ID;
        }

        [TestMethod]
        public void ClientConnectionTest()
        {
            //server.StartListeningForClients();
            server.stopServer();
        } 
    }
}
