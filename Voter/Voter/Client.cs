using NetworkLib;
using Utils;

namespace Voter
{
    public class Client
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

        public bool connect(string ip, string port, string target, NetworkLib.Client.NewMsgHandler msgHandler)
        {
            client = new NetworkLib.Client(ip, port);
            client.OnNewMessageRecived += msgHandler;
            sendMyName();
            return true;
        }

        private void sendMyName()
        {
            client.sendMessage("//NAME// " + myName);  
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
