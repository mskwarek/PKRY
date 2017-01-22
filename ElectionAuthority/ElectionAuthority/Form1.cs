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
        private Configuration configuration;
        private ElectionAuthority electionAuthority;
        
        public Form1()
        {
            InitializeComponent();
            setColumnWidth();            
            configuration = new Configuration();   
        }

        private void startElectionAuthorityButton_Click(object sender, EventArgs e)
        {
            this.startElectionAuthorityButton.Enabled = false;
            this.sendSLTokensAndTokensButton.Enabled = true;
            startEaService();
        }

        public void startEaService()
        {
            this.electionAuthority.startServices();
            this.electionAuthority.generateDate(); //method generate Serial number (SL), permutations of candidate list and tokens
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loadConfig(openFileDialog.FileName);
            enableButtonAfterConfiguration();
        }

        public void loadConfig(string filename)
        {
            configuration.loadConfiguration(filename);
            electionAuthority = new ElectionAuthority(this.configuration, this);
            this.electionAuthority.loadCandidateList("C:\\Users\\mskwarek\\Documents\\Visual Studio 2015\\Projects\\PKRY\\Config\\CandidateList.xml");
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
                {
                    this.electionAuthority.ServerClient.stopServer();
                }
                if (this.electionAuthority.ServerClient != null)
                {
                    this.electionAuthority.ServerProxy.stopServer();
                }
            }           
        }

        private void setColumnWidth()
        {
            this.logColumn.Width = this.logsListView.Width - 5;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            senSLAndSR();
        }

        public void senSLAndSR()
        {
            this.electionAuthority.sendSLAndTokensToProxy();
        }

        public void disableSendSLTokensAndTokensButton()
        {
            this.sendSLTokensAndTokensButton.Enabled = false;
            this.finishVotingButton.Enabled = true;
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

    }
}
