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
    /// <summary>
    /// graphical user interface 
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// display logs information in console, allow user to undestand what's going on in application 
        /// </summary>
        private Logs logs;
        /// <summary>
        /// load configuration from xml file
        /// </summary>
        private Configuration configuration;
        /// <summary>
        /// contains logic of voter application 
        /// </summary>
        private Voter voter;
        /// <summary>
        /// list of text boxes which are used to show candidates name and surname
        /// </summary>
        private List<TextBox> textBoxes;
        /// <summary>
        /// TextBoxes property which allow to get the list
        /// </summary>
        public List<TextBox> TextBoxes
        {
            get { return textBoxes; }
        }
        /// <summary>
        /// confirmation of casted vote
        /// </summary>
        private Confirmation confirmation;
        /// <summary>
        /// list of buttons which are used to cast a vote
        /// </summary>
        private List<Button[]> voteButtons;
        /// <summary>
        /// VoteButtons property which allow to get the list
        /// </summary>
        public List<Button[]> VoteButtons
        {
            get { return voteButtons; }
        }

        /// <summary>
        /// constructor of form
        /// </summary>
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
        /// <summary>
        /// connects with Eletion Authority application 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EAConnectButton_Click(object sender, EventArgs e)
        {
            this.voter.ElectionAuthorityClient.connect(this.configuration.ElectionAuthorityIP, this.configuration.ElectionAuthorityPort, Constants.ELECTION_AUTHORITY);
            this.configButton.Enabled = false;
            
        }

        /// <summary>
        /// click on user vote button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void voteButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            //Console.WriteLine(clickedButton);
            String[] words = clickedButton.Name.Split(';');
            if (this.voter.VoterBallot.vote(Convert.ToInt32(words[1]), Convert.ToInt32(words[2])))
            {
                logs.addLog(Constants.VOTE_DONE, true, Constants.LOG_INFO, true);
                if (this.voter.VoterBallot.voteDone())
                {
                    logs.addLog(Constants.VOTE_FINISH, true, Constants.LOG_INFO, true);
                    this.disableVoteButtons();
                    this.confirmationBox.Enabled = true;

                }

            }
            else
            {
                logs.addLog(Constants.VOTE_ERROR, true, Constants.LOG_ERROR, true);
            }

            //Console.WriteLine(words[0] );

        }

        /// <summary>
        /// disabled voting buttons
        /// </summary>
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

        /// <summary>
        /// set width of columns in log console
        /// </summary>
        private void setColumnWidth()
        {
            this.logColumn.Width = this.logsListView.Width - 5;
        }

        /// <summary>
        /// form closing actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// connectes with Proxy application 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProxyConnectButton_Click(object sender, EventArgs e)
        {
            this.voter.ProxyClient.connect(configuration.ProxyIP, configuration.ProxyPort, Constants.PROXY);
            this.getSLandSRButton.Enabled = true;
        }

        /// <summary>
        /// load configuration from xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        /// <summary>
        /// accept xml file which was choosen a configuration file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            configuration.loadConfiguration(openFileDialog.FileName);
            enableButtonsAfterLoadingConfiguration();
            
            this.voter = new Voter(this.logs, this.configuration,this, this.confirmation);
            addFieldsForCandidates(configuration.NumberOfCandidates);

        }

        /// <summary>
        /// load a candidates detailes to text boxes
        /// </summary>
        /// <param name="NumberOfCandidates"></param>
        private void addFieldsForCandidates(int NumberOfCandidates)
        {
            for (int i = 0; i < NumberOfCandidates; i++)
            {
                TextBox newTextBox = new TextBox();
                textBoxes.Add(newTextBox);

                Button[] newVoteButtons = new Button[Constants.BALLOTSIZE];
                for (int it = 0; it < Constants.BALLOTSIZE; it++)
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

                for (int j = 0; j < Constants.BALLOTSIZE; j++)
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
        /// <summary>
        /// enable buttons after loading configuration 
        /// </summary>
        private void enableButtonsAfterLoadingConfiguration()
        {
            this.ProxyConnectButton.Enabled = true;
            this.EAConnectButton.Enabled = true;
               
        }
        /// <summary>
        /// sent request to get SL and SR number to Proxy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getSLandSRButton_Click(object sender, EventArgs e)
        {
            this.voter.requestForSLandSR();
        }


        /// <summary>
        /// disable SL and SR buttons
        /// </summary>
        public void disableSLAndSRButton()
        {
            this.getSLandSRButton.Enabled = false;
            this.getCandidateListButton.Enabled = true;
        }


        /// <summary>
        /// send request for a candidate list to Proxy 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getCandidateListButton_Click(object sender, EventArgs e)
        {
            this.voter.requestForCandidatesList();
        }

        /// <summary>
        /// disable connection with Proxy button 
        /// </summary>
        public void disableConectionProxyButton()
        {
            this.ProxyConnectButton.Enabled = false;

        }

        /// <summary>
        /// disable connection with Election Authority button
        /// </summary>
        public void disableConnectionEAButton()
        {
            this.EAConnectButton.Enabled = false;
        }


        /// <summary>
        /// disable Get Candidate List button 
        /// </summary>
        public void disableGetCandidateListButton()
        {
            this.getCandidateListButton.Enabled = false;
            //if (this.getYesNoPositionButton.Enabled == false)
            //    this.sendVoteButton.Enabled = true;
        }


        /// <summary>
        /// send a casted vote to Proxy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendVoteButton_Click(object sender, EventArgs e)
        {
            this.voter.sendVoteToProxy();
            this.sendVoteButton.Enabled = false;
            this.confirmationBox.Enabled = false;
                 
        }

        /// <summary>
        /// represents a confirmation combo box click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.voter.setConfirm(this.confirmationBox.SelectedIndex);
            this.sendVoteButton.Enabled = true;
        }
        /// <summary>
        /// confBox property which allow to set and get value of confirmation box
        /// </summary>
        public int confBox { get; set; }
    }
}
