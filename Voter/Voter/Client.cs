using NetworkLib;
using Utils;

namespace Voter
{
    class Client
    {
        private NetworkLib.Client client;
        private string myName;

        public NetworkLib.Client.NewMsgHandler newMessageHandler { get; set; }

        private bool connected;
        public bool Connected
        {
            get { return connected; }
        }

        public Client(string name,  Voter voter)
        {
            this.myName = name;
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
            Utils.Logs.addLog("Client", Constants.NEW_MSG_RECIVED + " " + myArgs.Message, true, Constants.LOG_INFO, true);
            parseMessage(myArgs.Message);
        }

        private void parseMessage(string msg)
        {
            string[] elem = msg.Split('&');
            Message message = Messages.ClientMessageFactory.generateMessage(elem[0]);
            message.Parse(this, elem[1]);
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
