using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Org.BouncyCastle.Math;

namespace Voter
{
    class Voter
    {
        private Configuration configuration;
        private Client proxyClient;
        private Confirmation confirm;

        public Client ProxyClient
        {
            get { return proxyClient; }
        }

        private Client electionAuthorityClient;
        public Client ElectionAuthorityClient
        {
            get { return electionAuthorityClient; }
        }

        private VoterBallot voterBallot;
        public VoterBallot VoterBallot
        {
            get { return voterBallot; }
        }

        private Form1 form;

        public Voter(Configuration configuration,Form1 form, Confirmation confirm)
        {
            this.configuration = configuration;
            this.form = form;
            this.proxyClient = new Client(this.configuration.Name, this);
            this.electionAuthorityClient = new Client(this.configuration.Name, this);
            this.voterBallot = new VoterBallot(this.configuration.NumberOfCandidates);
            this.confirm = confirm;
        }

        public void requestForSLandSR()
        {
            string msg = NetworkLib.Constants.GET_SL_AND_SR + "&" + this.configuration.Name;
            this.proxyClient.sendMessage(msg);
        }

        public void disableSLAndSRButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableSLAndSRButton();
                })
            );
        }

        public void requestForCandidatesList()
        {
            string msg = NetworkLib.Constants.GET_CANDIDATE_LIST +"&" +this.configuration.Name+ "=" + this.voterBallot.SL.ToString();
            this.electionAuthorityClient.sendMessage(msg);
        }

        public void disableConnectionProxyButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableConectionProxyButton();

                }));
        }

        public void disableConnectionEAButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableConnectionEAButton();
                }));
        }

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

        private void disableGetCandidateListButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableGetCandidateListButton();
                }));
                
        }

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

        public void sendVoteToProxy()
        {
            int[,] table = this.voterBallot.Voted;
            string message = NetworkLib.Constants.VOTE + "&" + this.configuration.Name + ";";
            for (int i = 0; i < table.GetLength(0); i++)
            {

                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (j == table.GetLength(1) - 1 && i == table.GetLength(0) - 1)
                    {
                        message = message + table[i, j].ToString();
                    }
                    else if (j == table.GetLength(1) - 1 && i != table.GetLength(0) - 1)
                    {
                        message = message + table[i, j].ToString() + ";";
                    }
                    else
                    {
                        message = message + table[i, j].ToString() + ":";
                    }
                }
                //vote wyglada tak: VOTE&Voter0;1:0:0:0;1:0:0:0;0:0:0:1;0:0:0:1;0:0:0:1
            }
            message = message + ";" + confirm.Index.ToString();
            
            this.proxyClient.sendMessage(message);

        }

        public void setConfirm(int column)
        {
            for(int i=0; i<this.voterBallot.Voted.GetLength(0); i++)
            {
                this.confirm.Column += this.voterBallot.Voted[i,column];
            }
            
            confirm.ColumnNumber = column+1;
        }

        public void saveSignedColumnAndToken(string message)
        {
            string[] words = message.Split(';');

            this.voterBallot.SignedBlindColumn = new BigInteger(words[0]);
            this.voterBallot.Token = new BigInteger(words[1]);

            this.confirm.SignedColumn = this.voterBallot.SignedBlindColumn;
            this.confirm.Token = this.voterBallot.Token;

            Utils.Logs.addLog("Client", NetworkLib.Constants.SIGNED_COLUMNS_TOKEN_RECEIVED, true, NetworkLib.Constants.LOG_INFO, true);

            this.confirm.addConfirm();
        }
    }
}
