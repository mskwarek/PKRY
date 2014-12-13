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
        private Server server;
        private Client client;
        
        private Configuration configuration;

        private ParserEA parserEA;

        private Proxy proxy;
        
        public Form1()
        {
            InitializeComponent();
            this.logs = new Logs(this.logsListView);
            this.configuration = new Configuration(this.logs);
            this.server = new Server(this.logs);
            this.proxy = new Proxy(this.logs, this.configuration, this.server);
            this.parserEA = new ParserEA(this.logs, this.proxy);
            
            this.client = new Client(this.logs, this.parserEA);
            
            

        }

        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            configuration.loadConfiguration(openFileDialog.FileName);
            enableButtonsAfterConfiguration();
            
        }

        private void connectElectionAuthorityButton_Click(object sender, EventArgs e)
        {
            this.client.connect(configuration.ElectionAuthorityIP, configuration.ElectionAuthorityPort);
            this.connectElectionAuthorityButton.Enabled = false;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.server.stopServer();
            this.client.disconnectFromElectionAuthority();
        }

        private void startProxyButton_Click(object sender, EventArgs e)
        {
            this.server.startServer(configuration.ProxyPort);
            this.proxy.generateSR();


            this.startProxyButton.Enabled = false;
            this.configButton.Enabled = false;
        }

        private void enableButtonsAfterConfiguration()
        {
            this.startProxyButton.Enabled = true;
            this.connectElectionAuthorityButton.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.proxy.sendSLAndSR();
        }
        
    }
}
