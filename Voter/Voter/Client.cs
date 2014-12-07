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


        public Client(Logs logs)
        {
            this.encoder = new ASCIIEncoding();
            this.logs = logs;
        }


        public bool connect(string ip, string port)
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
                logs.addLog(Constants.CONNECTION_PASS, true, Constants.LOG_INFO, true);
                return true;
            }
            else
            {
                client = null;
                logs.addLog(Constants.CONNECTION_FAILED, true, Constants.LOG_ERROR, true);
                return false;
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

                logs.addLog(encoder.GetString(message, 0, bytesRead), true, Constants.LOG_MESSAGE, true);
            }
            if (client != null)
            {
                disconnectFromElectionAuthority(true);
            }
        }

        public void disconnectFromElectionAuthority(bool error = false)
        {
            if (client != null)
            {
                client.GetStream().Close();
                client.Close();
                client = null;
                if (!error)
                {
                    logs.addLog(Constants.CONNECTION_DISCONNECTED, true, Constants.LOG_INFO, true);
                }
                else
                {
                    logs.addLog(Constants.CONNECTION_DISCONNECTED_ERROR, true, Constants.LOG_ERROR, true);
                    //form.Invoke(new MethodInvoker(delegate() { form.buttonsEnabled(); }));
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
