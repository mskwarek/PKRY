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


        private Dictionary<TcpClient, string> clientSockets;
        public Dictionary<TcpClient, string> ClientSockets
        {
            get { return clientSockets; }
        }

        private Utils.Logs logs;
        private ParserClient parserClient;

        public Server(Utils.Logs logs, Proxy proxy)
        {
            clientSockets = new Dictionary<TcpClient, string>();
            this.logs = logs;
            this.parserClient = new ParserClient(this.logs, proxy);
        }

        public bool startServer(string port)
        {
            try
            {
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
            logs.addLog(NetworkLib.Constants.VOTER_CONNECTED, true, NetworkLib.Constants.LOG_MESSAGE, true);

            try
            {
                updateClientName(e.ID, e.NodeName); //clients as first message send his id
                string msg = NetworkLib.Constants.CONNECTION_SUCCESSFUL + "&";
                sendMessage(clientSockets[e.ID], msg);
                logs.addLog(NetworkLib.Constants.VOTER_CONNECTED, true, NetworkLib.Constants.LOG_MESSAGE, true);
            }
            catch
            {

            }

        }

        private void newMessageRecived(object a, NetworkLib.MessageArgs e)
        {
            logs.addLog(NetworkLib.Constants.VOTER_CONNECTED, true, NetworkLib.Constants.LOG_MESSAGE, true);

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


        public void stopServer()
        {
            this.server.stopServer();
        }

        public void sendMessage(string name, string msg)
        {
            server.sendMessage(getTcpClient(name), msg);
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
