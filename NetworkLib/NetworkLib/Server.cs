using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetworkLib
{
    public class Server
    {
        public ASCIIEncoding encoder;
        public NetworkStream stream;
        public TcpListener serverSocket;
        public Thread serverThread;
        public List<TcpClient> clientSocket;

        public Server(int port)
        {
            this.encoder = new ASCIIEncoding();
            this.clientSocket = new List<TcpClient>();
            if (serverSocket == null && serverThread == null)
            {
                this.serverSocket = new TcpListener(IPAddress.Any, port);
                this.serverThread = new Thread(new ThreadStart(StartListeningForClients));
                this.serverThread.Start();
                //logs.addLog(Constants.CLOUD_STARTED_CORRECTLY, true, Constants.LOG_INFO, true);        
            }
            else
            {
                //logs.addLog(Constants.CLOUD_STARTED_ERROR, true, Constants.LOG_ERROR, true);
                //return false;
                throw new Exception("server has been started");
            }
        }

        ~Server()
        {
            this.stopServer();
        }

        public void StartListeningForClients()
        {
            try
            {
                this.serverSocket.Start();
                ListenForClients();
            }
            catch (Exception e)
            {
                this.stopServer();
                Console.WriteLine(e.StackTrace);
            }
        }

        private void ListenForClients()
        {
            while(true)
            { 
                TcpClient clientSocket = this.serverSocket.AcceptTcpClient();
                ClientArgs args = new ClientArgs();

                args.ID = clientSocket;

                Thread clientThread = new Thread(new ParameterizedThreadStart(ListenForMessage));
                clientThread.Start(clientSocket);

                this.clientSocket.Add(clientSocket);
                OnNewClientRequest(this, args);


            }
        }

        protected void ListenForMessage(object client)
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

                notifyListeners(encoder.GetString(message, 0, bytesRead), clientSocket);
            }
            if (serverSocket != null)
            {
                try
                {
                    endConnection(clientSocket);
                }
                catch
                {
                }
            }
        }

        private void notifyListeners(string signal, TcpClient clientSocket)
        {
            MessageArgs myArgs = new MessageArgs(signal, clientSocket);
            OnNewMessageRecived(this, myArgs);
        }

        public delegate void NewMsgHandler(object myObject, MessageArgs myArgs);
        public event NewMsgHandler OnNewMessageRecived;

        public delegate void NewClientHandler(object myObject, ClientArgs myArgs);
        public event NewClientHandler OnNewClientRequest;

        public bool isStarted()
        {
            return serverSocket != null;
        }

        public void endConnection(TcpClient client)
        {
            client.GetStream().Close();
            client.Close();
            clientSocket.Remove(client);
        }

        public void stopServer()
        {
            this.disconnectAllClients();
            stopServerThread();
        }

        private void disconnectAllClients()
        {
            foreach (TcpClient client in clientSocket)
            {
                try
                {
                    endConnection(client);
                }
                catch
                {
                    Console.WriteLine("Problems with disconnecting clients from cloud");
                }
            }
        }

        private void stopServerThread()
        {
            try
            {
                serverSocket.Stop();
                if (serverThread.IsAlive)
                {
                    serverThread.Abort();
                    serverThread.Join();
                }
            }
            catch
            {
                Console.WriteLine("Unable to stop server");
            }
        }

        public void sendMessage(TcpClient client, string msg)
        {
            if (serverSocket != null)
            {
                stream = null;
                if (client != null)
                {
                    try
                    {
                        stream = client.GetStream();
                        byte[] buffer = encoder.GetBytes(msg);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }
        }
    }
}
