using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Org.BouncyCastle.Math;

namespace Voter
{

    /// <summary>
    /// Voter class - getting vote from person
    /// </summary>
    class Voter
    {
        /// <summary>
        /// allows to collect and display logs
        /// </summary>
        private Logs logs;

        /// <summary>
        /// Configuration from file
        /// </summary>
        private Configuration configuration;

        /// <summary>
        /// voter proxy client class
        /// </summary>
        private Client proxyClient;

        /// <summary>
        /// confirmation for voter
        /// </summary>
        private Confirmation confirm;

        public Client ProxyClient
        {
            get { return proxyClient; }
        }

        /// <summary>
        /// election authority client for voter
        /// </summary>
        private Client electionAuthorityClient;
        public Client ElectionAuthorityClient
        {
            get { return electionAuthorityClient; }
        }

        /// <summary>
        /// voter ballot
        /// </summary>
        private VoterBallot voterBallot;
        public VoterBallot VoterBallot
        {
            get { return voterBallot; }
        }

        /// <summary>
        /// form application
        /// </summary>
        private Form1 form;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logs">log instance</param>
        /// <param name="configuration">configuration loaded</param>
        /// <param name="form">form application</param>
        /// <param name="confirm">confirmation for voter</param>
        public Voter(Logs logs, Configuration configuration,Form1 form, Confirmation confirm)
        {
            this.logs = logs;
            this.configuration = configuration;
            this.form = form;
            this.proxyClient = new Client(this.configuration.Name, this.logs, this);
            this.electionAuthorityClient = new Client(this.configuration.Name, this.logs, this);
            this.voterBallot = new VoterBallot(this.configuration.NumberOfCandidates);
            this.confirm = confirm;
        }

        /// <summary>
        /// request for SL and SR (voter to proxy)
        /// </summary>
        public void requestForSLandSR()
        {
            string msg = Constants.GET_SL_AND_SR + "&" + this.configuration.Name;
            this.proxyClient.sendMessage(msg);
        }


        /// <summary>
        /// disable SL and SR getting button
        /// </summary>
        public void disableSLAndSRButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableSLAndSRButton();
                }
                ));
        }

        /// <summary>
        /// request for candidate list (voter to EA)
        /// </summary>
        public void requestForCandidatesList()
        {
            string msg = Constants.GET_CANDIDATE_LIST +"&" +this.configuration.Name+ "=" + this.voterBallot.SL.ToString();
            this.electionAuthorityClient.sendMessage(msg);
        }

        /// <summary>
        /// disable connection to proxy button
        /// </summary>
        public void disableConnectionProxyButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableConectionProxyButton();

                }));
        }

        /// <summary>
        /// disable connection to ea button
        /// </summary>
        public void disableConnectionEAButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableConnectionEAButton();
                }));
        }

        /// <summary>
        /// saves cadidates list
        /// </summary>
        /// <param name="msg">recived message</param>
        public void saveCandidateList(string msg)
        {
            string[] list = msg.Split(';');
            for(int i=0;i<list.Length;i++)
            {
                this.form.Invoke(new MethodInvoker(delegate()
                    {
                        this.form.TextBoxes[i].Text = list[i];
                        this.form.TextBoxes[i].Enabled = false;
                    }));
            }
            disableGetCandidateListButton();
            enableVotingButtons();
        }

        /// <summary>
        /// disable get candidate list button
        /// </summary>
        private void disableGetCandidateListButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableGetCandidateListButton();
                }));
                
        }

        /// <summary>
        /// save yes/no position
        /// </summary>
        private void enableVotingButtons()
        {
            for (int i = 0; i < this.configuration.NumberOfCandidates; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.form.Invoke(new MethodInvoker(delegate()
                        {
                            this.form.VoteButtons[i].ElementAt(j).Enabled = true;
                        }));
                }

            }

        }


        /// <summary>
        /// sends vote to proxy (ie. message: VOTE& Voter_name;1:0:0:0;1:0:0:0;0:0:0:1;0:0:0:1;0:0:0:1)
        /// </summary>
        public void sendVoteToProxy()
        {
            int[,] table = this.voterBallot.Voted;
            string message = Constants.VOTE + "&" + this.configuration.Name + ";";
            for (int i = 0; i < table.GetLength(0); i++)
            {

                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (j == table.GetLength(1) - 1 && i == table.GetLength(0)-1)
                        message = message + table[i, j].ToString();
                    else if (j == table.GetLength(1) - 1 && i != table.GetLength(0)-1)
                        message = message + table[i, j].ToString()+ ";";
                    else
                        message = message + table[i, j].ToString() + ":";
                }
                //vote wyglada tak: VOTE&Voter0;1:0:0:0;1:0:0:0;0:0:0:1;0:0:0:1;0:0:0:1
            }
            message = message + ";" + confirm.Index.ToString();
            
            this.proxyClient.sendMessage(message);

        }

        /// <summary>
        /// sets confirmation
        /// </summary>
        /// <param name="column">confirmation column choosed by voter</param>
        public void setConfirm(int column)
        {
            for(int i=0; i<this.voterBallot.Voted.GetLength(0); i++){
                this.confirm.Column += this.voterBallot.Voted[i,column];
            }
            
            confirm.ColumnNumber = column+1;
        }


        /// <summary>
        /// save signed column and token
        /// </summary>
        /// <param name="message">recived message</param>
        public void saveSignedColumnAndToken(string message)
        {
            string[] words = message.Split(';');

            this.voterBallot.SignedBlindColumn = new BigInteger(words[0]);
            this.voterBallot.Token = new BigInteger(words[1]);

            this.confirm.SignedColumn = this.voterBallot.SignedBlindColumn;
            this.confirm.Token = this.voterBallot.Token;

            this.logs.addLog(Constants.SIGNED_COLUMNS_TOKEN_RECEIVED, true, Constants.LOG_INFO, true);

            this.confirm.addConfirm(true);
        }
    }
}
