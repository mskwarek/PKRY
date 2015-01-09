using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Proxy
{
    /// <summary>
    /// server for proxy application 
    /// </summary>
    class Server
    {
        /// <summary>
        /// server socket on which server is running
        /// </summary>
        private TcpListener serverSocket;

        /// <summary>
        /// server thread 
        /// </summary>
        private Thread serverThread;

        /// <summary>
        /// dictionary which contains a client sockets
        /// </summary>
        private Dictionary<TcpClient, string> clientSockets;
        public Dictionary<TcpClient, string> ClientSockets
        {
            get { return clientSockets; }
        }
        /// <summary>
        /// encoder used to encode a messages from bytes to string and again 
        /// </summary>
        private ASCIIEncoding encoder;
        /// <summary>
        /// display logs in console
        /// </summary>
        private Logs logs;
        /// <summary>
        /// pares message from Client
        /// </summary>
        private ParserClient parserClient;

        /// <summary>
        /// main logic of Proxy application 
        /// </summary>
        private Proxy proxy;

        /// <summary>
        /// defualt constructor
        /// </summary>
        /// <param name="logs">display messages in logs console</param>
        /// <param name="proxy">main logic of Proxy application</param>
        public Server(Logs logs, Proxy proxy)
        {
            clientSockets = new Dictionary<TcpClient, string>();
            this.encoder = new ASCIIEncoding();
            this.logs = logs;
            this.parserClient = new ParserClient(this.logs, proxy);
        }

        
        /// <summary>
        /// starts a server 
        /// </summary>
        /// <param name="port">number of port on which server is running</param>
        /// <returns>true when server started successfully</returns>
        public bool startServer(string port)
        {
            int runningPort = Convert.ToInt32(port);
            if (serverSocket == null && serverThread == null)
            {
                this.serverSocket = new TcpListener(IPAddress.Any, runningPort);
                this.serverThread = new Thread(new ThreadStart(ListenForClients));
                this.serverThread.Start();
                logs.addLog(Constants.SERVER_STARTED_CORRECTLY, true, Constants.LOG_INFO, true);
                return true;
            }
            else
            {
                logs.addLog(Constants.SERVER_UNABLE_TO_START, true, Constants.LOG_ERROR, true);
                return false;
            }
        }
        /// <summary>
        /// listen for client 
        /// </summary>
        private void ListenForClients()
        {
            this.serverSocket.Start();
            while (true)
            {
                try
                {
                    TcpClient clientSocket = this.serverSocket.AcceptTcpClient();
                    clientSockets.Add(clientSocket, Constants.UNKNOWN);
                    Thread clientThread = new Thread(new ParameterizedThreadStart(displayMessageReceived));
                    clientThread.Start(clientSocket);
                }
                catch
                {
                    break;
                }
            }
        }


        /// <summary>
        /// display message received from clients
        /// </summary>
        /// <param name="client"></param>
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

                string signal = encoder.GetString(message, 0, bytesRead);
                if (clientSockets[clientSocket].Equals(Constants.UNKNOWN))
                {
                    updateClientName(clientSocket, signal); //clients as first message send his id
                    string msg = Constants.CONNECTION_SUCCESSFUL + "&";
                    sendMessage(clientSockets[clientSocket], msg);
                    logs.addLog(Constants.VOTER_CONNECTED, true, Constants.LOG_MESSAGE, true);
                }
                else
                {
                    this.parserClient.parseMessageFromClient(signal);
                    logs.addLog(signal, true, Constants.LOG_MESSAGE, true);
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
                logs.addLog(Constants.DISCONNECTED_NODE, true, Constants.LOG_ERROR, true);
            }

            }
        
        /// <summary>
        /// stop server 
        /// </summary>
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

        /// <summary>
        /// send message to client
        /// </summary>
        /// <param name="name">client's name</param>
        /// <param name="msg">message</param>
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

        /// <summary>
        /// udpate client name in dictionary 
        /// </summary>
        /// <param name="client">TcpClient</param>
        /// <param name="signal">client name</param>
        private void updateClientName(TcpClient client, string signal)
        {
            if (signal.Contains("//NAME// "))
            {
                string[] tmp = signal.Split(' ');
                clientSockets[client] = tmp[1];
            }
        }

        /// <summary>
        /// get's TCP Client connected with name of client
        /// </summary>
        /// <param name="name">client name</param>
        /// <returns>TCP Client connected with given client name</returns>
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
