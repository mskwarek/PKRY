using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Proxy
{
    class Server
    {
        private NetworkLib.Server server;
        private List<TcpClient> sockests;
        NetworkLib.Server.NewClientHandler reqListener;
        NetworkLib.Server.NewMsgHandler msgListener;

        private TcpListener serverSocket;
        private Thread serverThread;
        private Dictionary<TcpClient, string> clientSockets;
        public Dictionary<TcpClient, string> ClientSockets
        {
            get { return clientSockets; }
        }
        private ASCIIEncoding encoder;
        private Utils.Logs logs;
        private ParserClient parserClient;

        public Server(Utils.Logs logs, Proxy proxy)
        {
            clientSockets = new Dictionary<TcpClient, string>();
            this.encoder = new ASCIIEncoding();
            this.logs = logs;
            this.parserClient = new ParserClient(this.logs, proxy);
        }

        public bool startServer(string port)
        {
            try
            {
                Console.WriteLine(Convert.ToInt32(port));
                server = new NetworkLib.Server(Convert.ToInt32(port));
                sockests = new List<TcpClient>();
                reqListener = new NetworkLib.Server.NewClientHandler(newClientRequest);
                msgListener = new NetworkLib.Server.NewMsgHandler(newMessageRecived);
                server.OnNewClientRequest += reqListener;
                server.OnNewMessageRecived += msgListener;
                if (server.isStarted())
                {
                    logs.addLog(NetworkLib.Constants.SERVER_STARTED_CORRECTLY, true, NetworkLib.Constants.LOG_INFO, true);
                }
                else
                {
                    logs.addLog(NetworkLib.Constants.SERVER_UNABLE_TO_START, true, NetworkLib.Constants.LOG_ERROR, true);
                }
            }
            catch
            {
                logs.addLog(NetworkLib.Constants.SERVER_UNABLE_TO_START, true, NetworkLib.Constants.LOG_ERROR, true);
            }

            return true;
        }

        private void newClientRequest(object a, NetworkLib.ClientArgs e)
        {

        }

        private void newMessageRecived(object a, NetworkLib.MessageArgs e)
        {
            try
            {
                if (clientSockets[e.ID].Equals(NetworkLib.Constants.UNKNOWN))
                {
                    updateClientName(e.ID, e.Message); //clients as first message send his id
                    string msg = NetworkLib.Constants.CONNECTION_SUCCESSFUL + "&";
                    sendMessage(clientSockets[e.ID], msg);
                    logs.addLog(NetworkLib.Constants.VOTER_CONNECTED, true, NetworkLib.Constants.LOG_MESSAGE, true);
                }
            }
    
            catch
            {
                updateClientName(e.ID, e.Message); //clients as first message send his id
                string msg = NetworkLib.Constants.CONNECTION_SUCCESSFUL + "&";
                sendMessage(clientSockets[e.ID], msg);
                logs.addLog(NetworkLib.Constants.VOTER_CONNECTED, true, NetworkLib.Constants.LOG_MESSAGE, true);

                return;
            }     
               
            this.parserClient.parseMessageFromClient(e.Message);
            logs.addLog(e.Message, true, NetworkLib.Constants.LOG_MESSAGE, true);
            
        }

        private void ListenForClients()
        {
            this.serverSocket.Start();
            while (true)
            {
                try
                {
                    TcpClient clientSocket = this.serverSocket.AcceptTcpClient();
                    clientSockets.Add(clientSocket, NetworkLib.Constants.UNKNOWN);
                    Thread clientThread = new Thread(new ParameterizedThreadStart(displayMessageReceived));
                    clientThread.Start(clientSocket);
                }
                catch
                {
                    break;
                }
            }
        }

        private void displayMessageReceived(object client)
        {
            TcpClient clientSocket = (TcpClient)client;
            NetworkStream stream = clientSocket.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (stream.CanRead)
            {
                bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                {
                    break;
                }

                
            }
            if (serverSocket != null)
            {
                try
                {
                    clientSocket.GetStream().Close();
                    clientSocket.Close();
                    clientSockets.Remove(clientSocket);
                }
                catch
                {
                }
                logs.addLog(NetworkLib.Constants.DISCONNECTED_NODE, true, NetworkLib.Constants.LOG_ERROR, true);
            }

            }

        public void stopServer()
        {
            foreach (TcpClient clientSocket in clientSockets.Keys.ToList())
            {
                clientSocket.GetStream().Close();
                clientSocket.Close();
                clientSockets.Remove(clientSocket);
            }
            if (serverSocket != null)
            {
                serverSocket.Stop();
            }
            serverSocket = null;
            serverThread = null;
        }

        public void sendMessage(string name, string msg)
        {
            for (int i = 0; i < clientSockets.Count; i++)
            {
                Console.WriteLine("nazwy clientow " + clientSockets.ElementAt(i).Value.ToString()); 
            }


            if (serverSocket != null)
            {
                NetworkStream stream = null;
                TcpClient client = getTcpClient(name);
                
                

                if (client != null)
                {
                    if (client.Connected)
                    {
                        stream = client.GetStream();
                        byte[] buffer = encoder.GetBytes(msg);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                    }
                    else
                    {
                        stream.Close();
                        clientSockets.Remove(client);
                    }
                }
            }
        }

        private void updateClientName(TcpClient client, string signal)
        {
            if (signal.Contains("//NAME// "))
            {
                string[] tmp = signal.Split(' ');
                clientSockets[client] = tmp[1];
            }
        }

        private TcpClient getTcpClient(string name)
        {
            TcpClient client = null;
            List<TcpClient> clientsList = clientSockets.Keys.ToList();
            for (int i = 0; i < clientsList.Count; i++)
            {
                if (clientSockets[clientsList[i]].Equals(name))
                {
                    client = clientsList[i];
                    return client;
                }
            }
            return null;
        }
    }
}
