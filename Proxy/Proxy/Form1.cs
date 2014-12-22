using System;
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
        private Logs logs;
        
        
        private Configuration configuration;

        private ParserEA parserEA;
        private ParserClient parserClient;

        private Proxy proxy;
        
        public Form1()
        {
            InitializeComponent();
            this.logs = new Logs(this.logsListView);
            this.configuration = new Configuration(this.logs);

           
            
            

        }

        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            configuration.loadConfiguration(openFileDialog.FileName);
            enableButtonsAfterConfiguration();
            this.proxy = new Proxy(this.logs, this.configuration);

        }

        private void connectElectionAuthorityButton_Click(object sender, EventArgs e)
        {
            this.proxy.Client.connect(configuration.ElectionAuthorityIP, configuration.ElectionAuthorityPort);
            this.connectElectionAuthorityButton.Enabled = false;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.proxy.Server != null)
            {
                this.proxy.Server.stopServer();
                this.proxy.Client.disconnectFromElectionAuthority();
            }
            
        }

        private void startProxyButton_Click(object sender, EventArgs e)
        {
            this.proxy.Server.startServer(configuration.ProxyPort);
            this.proxy.generateSR();


            this.startProxyButton.Enabled = false;
            this.configButton.Enabled = false;
        }

        private void enableButtonsAfterConfiguration()
        {
            this.startProxyButton.Enabled = true;
            this.connectElectionAuthorityButton.Enabled = true;
        }

        private void sendSLAndSR_Click(object sender, EventArgs e)
        {

        }
        
    }
}
