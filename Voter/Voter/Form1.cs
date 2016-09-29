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
        private Voter voter;
        private List<TextBox> textBoxes;
        public List<TextBox> TextBoxes
        {
            get { return textBoxes; }
        }
        private Confirmation confirmation;
        private List<Button[]> voteButtons;
        public List<Button[]> VoteButtons
        {
            get { return voteButtons; }
        }
        public Form1()
        {
            
            InitializeComponent();
            setColumnWidth();
            this.logs = new Logs(this.logsListView);
            this.confirmation = new Confirmation(this.ConfBox);
            this.configuration = new Configuration(this.logs);
            this.textBoxes = new List<TextBox>();
            this.voteButtons = new List<Button[]>();

        }

        private void EAConnectButton_Click(object sender, EventArgs e)
        {
            this.voter.ElectionAuthorityClient.connect(this.configuration.ElectionAuthorityIP, this.configuration.ElectionAuthorityPort, NetworkLib.Constants.ELECTION_AUTHORITY);
        //    var EA = new NetworkLib.Client(this.configuration.ElectionAuthorityIP, this.configuration.ElectionAuthorityPort);
          //  newMessageHandler = new NetworkLib.Client.NewMsgHandler(newMessageRecived);
           // EA.OnNewMessageRecived += newMessageHandler;


            this.configButton.Enabled = false;
            
        }


        private void voteButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            //Console.WriteLine(clickedButton);
            String[] words = clickedButton.Name.Split(';');
            if (this.voter.VoterBallot.vote(Convert.ToInt32(words[1]), Convert.ToInt32(words[2])))
            {
                logs.addLog(NetworkLib.Constants.VOTE_DONE, true, NetworkLib.Constants.LOG_INFO, true);
                if (this.voter.VoterBallot.voteDone())
                {
                    logs.addLog(NetworkLib.Constants.VOTE_FINISH, true, NetworkLib.Constants.LOG_INFO, true);
                    this.disableVoteButtons();
                    this.confirmationBox.Enabled = true;

                }

            }
            else
            {
                logs.addLog(NetworkLib.Constants.VOTE_ERROR, true, NetworkLib.Constants.LOG_ERROR, true);
            }

            //Console.WriteLine(words[0] );

        }

        private void disableVoteButtons()
        {
            for (int i=0; i<this.voteButtons.Count; i++)
            {
                for (int j = 0; j < this.voteButtons[i].Length; j++)
                {
                    this.voteButtons[i].ElementAt(j).Enabled = false;
                }
            }
        }

        private void setColumnWidth()
        {
            this.logColumn.Width = this.logsListView.Width - 5;
        }

        private void Form1_FormClsing(object sender, FormClosingEventArgs e)
        {
            if (this.voter != null)
            {

                if (this.voter.ElectionAuthorityClient.Connected)
                    this.voter.ElectionAuthorityClient.disconnect();

                if (this.voter.ProxyClient.Connected)
                    this.voter.ProxyClient.disconnect();
            }
        }

        private void ProxyConnectButton_Click(object sender, EventArgs e)
        {
            this.voter.ProxyClient.connect(configuration.ProxyIP, configuration.ProxyPort, NetworkLib.Constants.PROXY);
            this.getSLandSRButton.Enabled = true;
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            configuration.loadConfiguration(openFileDialog.FileName);
            enableButtonsAfterLoadingConfiguration();
            
            this.voter = new Voter(this.logs, this.configuration,this, this.confirmation);
            addFieldsForCandidates(configuration.NumberOfCandidates);

        }

        private void addFieldsForCandidates(int NumberOfCandidates)
        {
            for (int i = 0; i < NumberOfCandidates; i++)
            {
                TextBox newTextBox = new TextBox();
                textBoxes.Add(newTextBox);

                Button[] newVoteButtons = new Button[NetworkLib.Constants.BALLOTSIZE];
                for (int it = 0; it < NetworkLib.Constants.BALLOTSIZE; it++)
                {
                    Button newCandidateButton = new Button();
                    newVoteButtons[it] = newCandidateButton;
                }

                voteButtons.Add(newVoteButtons);
                this.panel1.Controls.Add(newTextBox);
                this.textBoxes[i].Location = new System.Drawing.Point(23, 18 + i * 40);
                this.textBoxes[i].Multiline = true;
                this.textBoxes[i].Name = "Candidate nr" + i;
                this.textBoxes[i].Size = new System.Drawing.Size(200, 40);
                this.textBoxes[i].TabIndex = 0;

                for (int j = 0; j < NetworkLib.Constants.BALLOTSIZE; j++)
                {
                    //this.EAConnectButton.Enabled = false;
                    this.voteButtons[i].ElementAt(j).Location = new System.Drawing.Point(240 + j * 75, 17 + i * 40);
                    this.voteButtons[i].ElementAt(j).Name = "Candidate_nr;" + i + ";" + j;
                    this.voteButtons[i].ElementAt(j).Size = new System.Drawing.Size(70, 40);
                    this.voteButtons[i].ElementAt(j).TabIndex = 0;
                    this.voteButtons[i].ElementAt(j).Text = Convert.ToString(j);
                    this.voteButtons[i].ElementAt(j).Enabled = false;
                    this.voteButtons[i].ElementAt(j).UseVisualStyleBackColor = true;
                    this.voteButtons[i].ElementAt(j).Click += new System.EventHandler(voteButton_Click);
                    this.panel1.Controls.Add(voteButtons[i].ElementAt(j));
                }
            }


        }

        private void enableButtonsAfterLoadingConfiguration()
        {
            this.ProxyConnectButton.Enabled = true;
            this.EAConnectButton.Enabled = true;
               
        }

        private void getSLandSRButton_Click(object sender, EventArgs e)
        {
            this.voter.requestForSLandSR();
        }

        public void disableSLAndSRButton()
        {
            this.getSLandSRButton.Enabled = false;
            this.getCandidateListButton.Enabled = true;
        }

        private void getCandidateListButton_Click(object sender, EventArgs e)
        {
            this.voter.requestForCandidatesList();
        }

        public void disableConectionProxyButton()
        {
            this.ProxyConnectButton.Enabled = false;

        }

        public void disableConnectionEAButton()
        {
            this.EAConnectButton.Enabled = false;
        }

        public void disableGetCandidateListButton()
        {
            this.getCandidateListButton.Enabled = false;
            //if (this.getYesNoPositionButton.Enabled == false)
            //    this.sendVoteButton.Enabled = true;
        }

        private void sendVoteButton_Click(object sender, EventArgs e)
        {
            this.voter.sendVoteToProxy();
            this.sendVoteButton.Enabled = false;
            this.confirmationBox.Enabled = false;
                 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.voter.setConfirm(this.confirmationBox.SelectedIndex);
            this.sendVoteButton.Enabled = true;
        }

        public int confBox { get; set; }
    }
}
