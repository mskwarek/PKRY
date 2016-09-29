using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace ElectionAuthority
{
    class Server
    {
        private TcpListener serverSocket;
        private Thread serverThread;
        private Dictionary<TcpClient, string> clientSockets;
        private ASCIIEncoding encoder;

        /// <summary>
        /// allows to collect and display logs - information in console
        /// </summary>
        private Logs logs;

        /// <summary>
        /// allows to parse received messages
        /// </summary>
        private Parser parser;

        /// <summary>
        /// server which allows to communicate with other processes 
        /// </summary>
        /// <param name="logs">allows to collect and display logs - information in console</param>
        /// <param name="electionAuthority">represents class where is main logic of application</param>
        public Server(Logs logs, ElectionAuthority electionAuthority)
        {
            clientSockets = new Dictionary<TcpClient, string>();
            this.encoder = new ASCIIEncoding();
            this.logs = logs;
            this.parser = new Parser(this.logs, electionAuthority);
        }
        /// <summary>
        /// allow to start server
        /// </summary>
        /// <param name="port">number of port on which server is running, this information comes from configuration xml file</param>
        /// <returns>returns true when server started successfully</returns>
        public bool startServer(string port)
        {
            int runningPort = Convert.ToInt32(port);
            Console.WriteLine(runningPort);
            if (serverSocket == null && serverThread == null)
            {
                try
                {
                    this.serverSocket = new TcpListener(IPAddress.Any, runningPort);
                    this.serverThread = new Thread(new ThreadStart(ListenForClients));
                    this.serverThread.Start();
                }
                catch(Exception)
                {
                    Console.WriteLine("Exception during starting server -  ElectionAuthority");
                }
                logs.addLog(NetworkLib.Constants.SERVER_STARTED_CORRECTLY, true, NetworkLib.Constants.LOG_INFO, true);
                return true;
            }
            else
            {
                logs.addLog(NetworkLib.Constants.SERVER_UNABLE_TO_START, true, NetworkLib.Constants.LOG_ERROR, true);
                return false;
            }
        }
        /// <summary>
        /// listen for comming clients, when request comes it is connected with server (another thread is started)
        /// </summary>
        private void ListenForClients()
        {
            try
            {
                this.serverSocket.Start();
            }
            catch (SocketException)
            {
                Console.WriteLine("Troubles to start listening for clients");
            }
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
        /// <summary>
        /// displays received message from client 
        /// </summary>
        /// <param name="client">name of client, each client has it own name which is loaded from config file</param>
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
                if (clientSockets[clientSocket].Equals(NetworkLib.Constants.UNKNOWN))
                {
                    updateClientName(clientSocket, signal);
                    sendMessage(clientSockets[clientSocket], NetworkLib.Constants.CONNECTED);
                }
                else
                {
                    logs.addLog(signal, true, NetworkLib.Constants.LOG_MESSAGE, true); //do usuniecia ale narazie widzim co leci w komuniakcji
                    this.parser.parseMessage(signal);
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
                catch (Exception)
                {
                    Console.WriteLine("Troubles with displaying received message");
                }

                logs.addLog(NetworkLib.Constants.DISCONNECTED_NODE, true, NetworkLib.Constants.LOG_ERROR, true);
            }

        }
        /// <summary>
        /// stops server
        /// </summary>
        public void stopServer()
        {
            try
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
            catch(Exception)
            {
                Console.WriteLine("Exception during closing server -  ElectionAuthority");
            }
        }
        /// <summary>
        /// sends message to client 
        /// </summary>
        /// <param name="name">name of client which we want to send a message</param>
        /// <param name="msg">message which we want to send</param>
        public void sendMessage(string name, string msg)
        {
            if (serverSocket != null)
            {
                NetworkStream stream = null;
                TcpClient client = null;
                List<TcpClient> clientsList = clientSockets.Keys.ToList();
                for (int i = 0; i < clientsList.Count; i++)
                {
                    if (clientSockets[clientsList[i]].Equals(name))
                    {
                        client = clientsList[i];
                        break;
                    }
                }

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
        /// updates a client name
        /// </summary>
        /// <param name="client">tcp socket which client is connected to</param>
        /// <param name="signal">client's name</param>
        private void updateClientName(TcpClient client, string signal)
        {
            if (signal.Contains("//NAME// "))
            {
                string[] tmp = signal.Split(' ');
                clientSockets[client] = tmp[1];
            }
        }
    }
}
