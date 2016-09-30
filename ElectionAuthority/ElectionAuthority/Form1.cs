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
    /// <summary>
    /// Class which shows a GUI
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// load configuration from configuration xml file
        /// </summary>
        private Configuration configuration;

        /// <summary>
        /// main logic of Election Authority application 
        /// </summary>
        private ElectionAuthority electionAuthority;


        /// <summary>
        /// constructor which creates Graphical User interface
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            setColumnWidth();            
            configuration = new Configuration();
            
        }

        /// <summary>
        /// starts Election Authority 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startElectionAuthorityButton_Click(object sender, EventArgs e)
        {
            this.electionAuthority.ServerClient.startServer(configuration.ElectionAuthorityPortClient);
            this.electionAuthority.ServerProxy.startServer(configuration.ElectionAuthorityPortProxy);
            this.startElectionAuthorityButton.Enabled = false;
            this.sendSLTokensAndTokensButton.Enabled = true;
            
            this.electionAuthority.loadCandidateList(openFileDialog.FileName);
            this.electionAuthority.generateDate(); //method generate Serial number (SL), permutations of candidate list and tokens
        }

        /// <summary>
        /// open window to load configuration file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        /// <summary>
        /// accept chosen configuration file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            configuration.loadConfiguration(openFileDialog.FileName);
            enableButtonAfterConfiguration();
            electionAuthority = new ElectionAuthority(this.configuration,this);
        }

        /// <summary>
        /// enable buttons after loading configuration 
        /// </summary>
        private void enableButtonAfterConfiguration()
        {
            this.startElectionAuthorityButton.Enabled = true;
            this.configButton.Enabled = false;
        }

        /// <summary>
        /// actions done when form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// set width of column in log console
        /// </summary>
        private void setColumnWidth()
        {
            this.logColumn.Width = this.logsListView.Width - 5;
        }

        /// <summary>
        /// sends SL and tokens lists to Proxy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.electionAuthority.sendSLAndTokensToProxy();
        }


        /// <summary>
        /// disable sendSLAndTokensButton and enable finishVoting button
        /// </summary>
        public void disableSendSLTokensAndTokensButton()
        {
            this.sendSLTokensAndTokensButton.Enabled = false;
            this.finishVotingButton.Enabled = true;
        }

        /// <summary>
        /// finish voting process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void finishVotingButton_Click(object sender, EventArgs e)
        {
            this.electionAuthority.disbaleProxy();
            this.finishVotingButton.Enabled = false;
            this.countVotesButton.Enabled = true;
        }

        /// <summary>
        /// coutns votes after voting 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countVotesButton_Click(object sender, EventArgs e)
        {
            this.electionAuthority.countVotes();
            this.countVotesButton.Enabled = false;
        }

    }
}
