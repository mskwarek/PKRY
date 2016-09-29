using NetworkLib;
using Utils;

namespace Voter
{
    class Client
    {
        private NetworkLib.Client client;
        private Utils.Logs logs;
        private string myName;

        public NetworkLib.Client.NewMsgHandler newMessageHandler { get; set; }

        private bool connected;
        public bool Connected
        {
            get { return connected; }
        }

        private Parser parser;

        public Client(string name, Utils.Logs logs,  Voter voter)
        {
            this.logs = logs;
            this.myName = name;
            this.parser = new Parser(this.logs, voter);
            this.connected = false;
        }

        public bool connect(string ip, string port, string target)
        {
            client = new NetworkLib.Client(ip, port);
            newMessageHandler = new NetworkLib.Client.NewMsgHandler(displayMessageReceived);
            client.OnNewMessageRecived += newMessageHandler;
            sendMyName();
            return true;
        }

        private void sendMyName()
        {
            client.sendMessage("//NAME// " + myName);  
        }

        private void displayMessageReceived(object myObject, MessageArgs myArgs)
        {
            logs.addLog(Constants.NEW_MSG_RECIVED + " " + myArgs.Message, true, Constants.LOG_INFO, true);
            parser.parseMessage(myArgs.Message);
        }

        public void disconnect(bool error = false)
        {
            client.stopService();
        }

        public void sendMessage(string msg)
        {
            client.sendMessage(msg);
        }
    }
}
