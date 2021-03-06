﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proxy
{
    public partial class Form1 : Form
    {
        private Configuration configuration;
        private Proxy proxy;

        public Form1()
        {
            InitializeComponent();
            this.configuration = new Configuration();
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            loadConfig(openFileDialog.FileName);
            enableButtonsAfterConfiguration();         
        }

        public void loadConfig(string configPath)
        {
            configuration.loadConfiguration(configPath);
            this.proxy = new Proxy(this.configuration, this);
        }

        private void connectElectionAuthorityButton_Click(object sender, EventArgs e)
        {
            connectToEA();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.proxy != null)
            {
                if (this.proxy.Server != null)
                {
                    this.proxy.Server.stopServer();
                }
                if (this.proxy.Client != null)
                {
                    this.proxy.Client.disconnectFromElectionAuthority();
                }
            }        
        }

        private void startProxyButton_Click(object sender, EventArgs e)
        {
            startProxyService();
            this.startProxyButton.Enabled = false;
            this.configButton.Enabled = false;
        }

        public void startProxyService()
        {

            this.proxy.Server.startServer(configuration.ProxyPort);

            if (proxy.Server.isStarted())
            {
                Utils.Logs.addLog("Proxy", NetworkLib.Constants.SERVER_STARTED_CORRECTLY, true, NetworkLib.Constants.LOG_INFO, true);
            }
            else
            {
                Utils.Logs.addLog("Proxy", NetworkLib.Constants.SERVER_UNABLE_TO_START, true, NetworkLib.Constants.LOG_ERROR, true);
            }


            this.proxy.generateSR();
            this.proxy.generateYesNoPosition();


        }

        public void connectToEA()
        {
            this.proxy.Client.connect(configuration.ElectionAuthorityIP, configuration.ElectionAuthorityPort);
        }

        private void enableButtonsAfterConfiguration()
        {
            this.startProxyButton.Enabled = true;
            this.connectElectionAuthorityButton.Enabled = true;
        }

        public void disableConnectElectionAuthorityButton()
        {
            this.connectElectionAuthorityButton.Enabled = false;
        }
    }
}
