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
        private Configuration configuration;

        private ElectionAuthority electionAuthority;

        public Form1()
        {
            InitializeComponent();
            setColumnWidth();            
            logs = new Logs(this.logsListView);
            configuration = new Configuration(this.logs);
            
        }

        private void startElectionAuthorityButton_Click(object sender, EventArgs e)
        {
            this.electionAuthority.ServerClient.startServer(configuration.ElectionAuthorityPortClient);
            this.electionAuthority.ServerProxy.startServer(configuration.ElectionAuthorityPortProxy);
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
            electionAuthority = new ElectionAuthority(this.logs, this.configuration,this);
        }


        private void enableButtonAfterConfiguration()
        {
            this.startElectionAuthorityButton.Enabled = true;
            this.configButton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.electionAuthority != null)
            {
                if (this.electionAuthority.ServerProxy != null)
                    this.electionAuthority.ServerClient.stopServer();
                if (this.electionAuthority.ServerClient != null)
                    this.electionAuthority.ServerProxy.stopServer();
            }
            
        }

        private void setColumnWidth()
        {
            this.logColumn.Width = this.logsListView.Width - 5;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.electionAuthority.sendSLAndTokensToProxy();
            this.finishVotingButton.Enabled = true;
        }

        public void disableSendSLTokensAndTokensButton()
        {
            this.sendSLTokensAndTokensButton.Enabled = false;
        }

        private void finishVotingButton_Click(object sender, EventArgs e)
        {
            this.electionAuthority.disbaleProxy();
            this.finishVotingButton.Enabled = false;
            this.countVotesButton.Enabled = true;
        }

        private void countVotesButton_Click(object sender, EventArgs e)
        {
            this.electionAuthority.countVotes();
            this.countVotesButton.Enabled = false;
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }
    }
}
