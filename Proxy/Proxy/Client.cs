﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using NetworkLib;

namespace Proxy
{
    class Client
    {
        private Utils.Logs logs;
        private ParserEA parserEA;
        private NetworkLib.Client client;
        public NetworkLib.Client.NewMsgHandler newMessageHandler { get; set; }

        public Client(Utils.Logs logs, Proxy proxy)
        {
            this.logs = logs;
            this.parserEA = new ParserEA(this.logs, proxy);
        }

        public bool connect(string ip, string port)
        {
            client = new NetworkLib.Client(ip, port);
            newMessageHandler = new NetworkLib.Client.NewMsgHandler(displayMessageReceived);
            client.OnNewMessageRecived += newMessageHandler;
            sendMyName();
            return true;
        }

        private void sendMyName()
        {
            client.sendMessage("//NAME// " + "PROXY");
        }
        private void displayMessageReceived(object myObject, MessageArgs myArgs)
        {
            logs.addLog(Constants.NEW_MSG_RECIVED + " " + myArgs.Message, true, Constants.LOG_INFO, true);
            parserEA.parseMessageFromEA(myArgs.Message);
        }

        public void disconnectFromElectionAuthority(bool error = false)
        {
            client.stopService();
        }

        public void sendMessage(string msg)
        {
            client.sendMessage(msg);
        }
    }
}
