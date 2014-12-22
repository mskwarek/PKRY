using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ElectionAuthority
{
    public partial class Form1 : Form
    {
        private Logs logs;
        private Server serverClient; // server for clients (voters)
        private Server serverProxy; // server for proxy
        private Configuration configuration;

        private ElectionAuthority electionAuthority;

        public Form1()
        {
            InitializeComponent();
            setColumnWidth();            
            logs = new Logs(this.logsListView);
            configuration = new Configuration(this.logs);
            serverClient = new Server(this.logs);

            serverProxy = new Server(this.logs);
        }

        private void startElectionAuthorityButton_Click(object sender, EventArgs e)
        {
            this.serverClient.startServer(configuration.ElectionAuthorityPortClient);
            this.serverProxy.startServer(configuration.ElectionAuthorityPortProxy);
            this.startElectionAuthorityButton.Enabled = false;
            this.sendSLTokensAndTokensButton.Enabled = true;
            
            this.electionAuthority.loadCandidateList(openFileDialog.FileName);
            this.electionAuthority.generateDate(); //method generate Serial number (SL), permutations of candidate list and tokens
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            configuration.loadConfiguration(openFileDialog.FileName);
            enableButtonAfterConfiguration();
            electionAuthority = new ElectionAuthority(this.logs, this.configuration, this.serverClient, this.serverProxy);
        }


        private void enableButtonAfterConfiguration()
        {
            this.startElectionAuthorityButton.Enabled = true;
            this.configButton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.serverClient.stopServer();
            this.serverProxy.stopServer();
        }

        private void setColumnWidth()
        {
            this.logColumn.Width = this.logsListView.Width - 5;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.electionAuthority.sendSLAndTokensToProxy();
            //this.sendSLTokensAndTokensButton.Enabled = false;
        }
    }
}
