using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace ElectionAuthority
{
    public class Server
    {
        private TcpListener serverSocket;
        private Thread serverThread;
        private Dictionary<TcpClient, string> clientSockets;
        private ASCIIEncoding encoder;
        private Parser parser;

        public Server(ElectionAuthority electionAuthority)
        {
            clientSockets = new Dictionary<TcpClient, string>();
            this.encoder = new ASCIIEncoding();
            this.parser = new Parser(electionAuthority);
        }

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
                Utils.Logs.addLog("EA", NetworkLib.Constants.SERVER_STARTED_CORRECTLY, true, NetworkLib.Constants.LOG_INFO, true);
                return true;
            }
            else
            {
                Utils.Logs.addLog("EA", NetworkLib.Constants.SERVER_UNABLE_TO_START, true, NetworkLib.Constants.LOG_ERROR, true);
                return false;
            }
        }

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
                    Utils.Logs.addLog("EA", signal, true, NetworkLib.Constants.LOG_MESSAGE, true); //do usuniecia ale narazie widzim co leci w komuniakcji
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

                Utils.Logs.addLog("EA", NetworkLib.Constants.DISCONNECTED_NODE, true, NetworkLib.Constants.LOG_ERROR, true);
            }

        }

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
