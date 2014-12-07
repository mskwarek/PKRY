using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Voter
{
    public partial class Form1 : Form
    {

        private Logs logs;
        private Configuration configuration;

        private Client electionAuthorityClient;
        private Client proxyClient;

        public Form1()
        {
            InitializeComponent();
            setColumnWidth();
            this.logs = new Logs(this.logsListView);
            this.configuration = new Configuration(this.logs);
            this.electionAuthorityClient = new Client(this.logs);
            this.proxyClient = new Client(this.logs);
        }

        private void EAConnectButton_Click(object sender, EventArgs e)
        {
            this.electionAuthorityClient.connect(this.configuration.ElectionAuthorityIP, this.configuration.ElectionAuthorityPort, Constants.ELECTION_AUTHORITY);
            this.configButton.Enabled = false;
        }

        private void setColumnWidth()
        {
            this.logColumn.Width = this.logsListView.Width - 5;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.electionAuthorityClient.disconnectFromElectionAuthority();
        }

        private void ProxyConnectButton_Click(object sender, EventArgs e)
        {
            this.proxyClient.connect(configuration.ProxyIP, configuration.ProxyPort, Constants.PROXY);

        }

        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            configuration.loadConfiguration(openFileDialog.FileName);
            enableButtonsAfterLoadingConfiguration();
        }

        private void enableButtonsAfterLoadingConfiguration()
        {
            this.EAConnectButton.Enabled = true;
            this.ProxyConnectButton.Enabled = true;
        }

    }
}
