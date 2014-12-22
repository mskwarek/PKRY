using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Voter
{
    class Client
    {
        private ASCIIEncoding encoder;
        private TcpClient client;
        private NetworkStream stream;
        private Thread clientThread;
        private Logs logs;
        private string myName;
        private bool connected; // set when client is conected with proxy or EA
        public bool Connected
        {
            get { return connected; }
        }
        
        private Parser parser; //parser for both EA and Proxy message, because we don't 
        //want to create to clients class 

        public Client(string name, Logs logs,  Voter voter)
        {
            this.encoder = new ASCIIEncoding();
            this.logs = logs;
            this.myName = name;
            this.parser = new Parser(this.logs, voter);
            this.connected = false;
        }


        public bool connect(string ip, string port, string target)
        {

            client = new TcpClient();
            IPAddress ipAddress;
            if (ip.Contains(Constants.LOCALHOST))
            {
                ipAddress = IPAddress.Loopback;
            }
            else
            {
                ipAddress = IPAddress.Parse(ip);
            }

            try
            {
                int dstPort = Convert.ToInt32(port);
                client.Connect(new IPEndPoint(ipAddress, dstPort));
            }
            catch { }
            
            if (client.Connected)
            {
                stream = client.GetStream();
                clientThread = new Thread(new ThreadStart(displayMessageReceived));
                clientThread.Start();
                sendMyName();
                connected = true;
                logs.addLog(Constants.CONNECTION_PASS + target, true, Constants.LOG_INFO, true);
                return true;
            }
            else
            {
                client = null;
                logs.addLog(Constants.CONNECTION_FAILED + target, true, Constants.LOG_ERROR, true);
                return false;
            }
        }

        private void sendMyName()
        {
            {
                byte[] buffer = encoder.GetBytes("//NAME// " + myName);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
        }

        private void displayMessageReceived()
        {
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
                string msg = encoder.GetString(message, 0, bytesRead);
                logs.addLog(msg, true, Constants.LOG_MESSAGE, true);
                this.parser.parseMessage(msg);
            }
            if (client != null)
            {
                disconnect(true);
            }
        }

        public void disconnect(bool error = false)
        {
            if (client != null)
            {
                try
                {
                    client.GetStream().Close();
                    client.Close();
                    client = null;
                }
                catch
                {
                    Console.WriteLine(Constants.CONNECTION_DISCONNECTED);
                }

                if (!error)
                {
                    logs.addLog(Constants.CONNECTION_DISCONNECTED, true, Constants.LOG_INFO, true);
                }
                else
                {
                    logs.addLog(Constants.CONNECTION_DISCONNECTED_ERROR, true, Constants.LOG_ERROR, true);
                }
            }
        }

        public void sendMessage(string msg)
        {
            if (client != null && client.Connected && msg != "")
            {
                byte[] buffer = encoder.GetBytes(msg);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
        }
    }
}
