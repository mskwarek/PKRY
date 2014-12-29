using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Math;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Proxy
{
    class Proxy
    {
        private Logs logs;
        private Configuration configuration;
        private Form1 form;
        private Server server;
        public Server Server
        {
            get { return server; }
        }
        private Client client;
        public Client Client
        {
            get { return client; }
        }
        //serial number SR
        private List<BigInteger> SRList;
        private Dictionary<string, ProxyBallot> proxyBallots; //string is a name of voter, ProxyBallot contains all necesary information like SL, SR, yesNoPosition etc.

        private int numberOfVoters;

        private Dictionary<BigInteger, List<BigInteger>> serialNumberTokens; //dictionary contains serialNumber and tokens connected with that SL
        public Dictionary<BigInteger, List<BigInteger>> SerialNumberTokens
        {
            get { return this.serialNumberTokens; }
            set {this.serialNumberTokens = value;}
        }

        private Dictionary<BigInteger, BigInteger> serialNumberAndSR; //connects serialNumbers SL and SR 
        private static int numOfSentSLandSR = 0; //number of SL and SR sent to voter, incremented when request comes from voter
        private static int numOfSentYesNo = 0; //numer of YesNo position sent to voter, incremented when request comes from voter

        private List<string> yesNoPosition; //its list which contains position of YES and NO buttons on ballot of each voter


        public Proxy(Logs logs, Configuration conf, Form1 form)
        {
            this.logs = logs;
            this.configuration = conf;
            this.form = form;
            this.server = new Server(this.logs,this);
            this.client = new Client(this.logs, this);



            this.serialNumberTokens = new Dictionary<BigInteger,List<BigInteger>>();
            this.SRList = new List<BigInteger>();
            this.serialNumberAndSR = new Dictionary<BigInteger, BigInteger>();
            this.proxyBallots = new Dictionary<string, ProxyBallot>();
        }


        public void generateSR()
        {
            this.numberOfVoters = this.configuration.NumOfVoters;
            Console.WriteLine("num of voters" + this.numberOfVoters);
            this.SRList = SerialNumberGenerator.generateListOfSerialNumber(this.numberOfVoters, Constants.NUMBER_OF_BITS_SR);
            logs.addLog(Constants.SR_GEN_SUCCESSFULLY, true, Constants.LOG_INFO);

        }

        public void generateYesNoPosition()
        {
            this.yesNoPosition = new List<string>();
            this.yesNoPosition = SerialNumberGenerator.getYesNoPosition(this.configuration.NumOfVoters, this.configuration.NumOfCandidates);
            logs.addLog(Constants.YES_NO_POSITION_GEN_SUCCESSFULL, true, Constants.LOG_INFO);
        }

        public void connectSRandSL()
        {
            for(int i=0; i<this.SRList.Count; i++)
            {
                this.serialNumberAndSR.Add(serialNumberTokens.ElementAt(i).Key, SRList[i]);
            }
            logs.addLog(Constants.SR_CONNECTED_WITH_SL, true, Constants.LOG_INFO, true);

        }

        public void sendSLAndSR(string name)
        {
            if (this.serialNumberAndSR != null && this.serialNumberAndSR.Count != 0)
            {
                BigInteger SL = this.serialNumberAndSR.ElementAt(numOfSentSLandSR).Key;
                BigInteger SR = this.serialNumberAndSR.ElementAt(numOfSentSLandSR).Value;
                this.proxyBallots.Add(name, new ProxyBallot(SL, SR));
                string msg = Constants.SL_AND_SR + "&" + SL.ToString()
                     + "=" + SR.ToString();
                numOfSentSLandSR += 1;
                this.server.sendMessage(name, msg);
            }
            else
            {
                this.logs.addLog(Constants.ERROR_SEND_SL_AND_SR, true, Constants.LOG_ERROR, true);
            }
            

        }

        public void disableConnectElectionAuthorityButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableConnectElectionAuthorityButton();
                }));
        }

        public void sendYesNoPosition(string name)
        {
            if (this.yesNoPosition != null)
            {
                string position = this.yesNoPosition.ElementAt(numOfSentYesNo);
                this.proxyBallots[name].YesNoPos = position;
                string msg = Constants.YES_NO_POSITION + "&" + position;
                numOfSentYesNo += 1;
                this.server.sendMessage(name, msg);
            }
        }

        public void saveVote(string message)
        {
            //message = 'name;first_row;second_row .....;last_row'
            //first_row = 'x:y:z:v'
            int[,] vote = new int[this.configuration.NumOfCandidates, 4];
            string[] words = message.Split(';');
            string name = words[0];
            for (int i = 1; i < words.Length; i++)
            {
                string[] row = words[i].Split(':');
                for (int k = 0; k < row.Length; k++)
                {
                    vote[i-1,k] = Convert.ToInt32(row[k]);
                    Console.WriteLine(vote[i - 1, k]);
                }

            }


            this.proxyBallots[name].Vote = vote;
            this.logs.addLog(Constants.VOTE_RECEIVED + name, true, Constants.LOG_INFO, true);
            this.proxyBallots[name].generateAndSplitBallotMatrix();
            this.logs.addLog(Constants.BALLOT_MATRIX_GEN + name, true, Constants.LOG_INFO, true);
            BigInteger[] blindProxyBallot = this.proxyBallots[name].prepareDataToSend();
            
            string SL = this.proxyBallots[name].SL.ToString();
            string tokens = prepareTokens(this.proxyBallots[name].SL);
            string columns = prepareBlindProxyBallot(blindProxyBallot);
            string pubKeyModulus = this.proxyBallots[name].PubKey.Modulus.ToString();
            //msg = BLIND_PROXY_BALLOT&name;pubKeyModulus;SL_number;token1,token2,token3,token4;col1,col2,col3,col4
            string msg = Constants.BLIND_PROXY_BALLOT + "&" + name + ";" + pubKeyModulus + ";"  + SL + ";" + tokens + columns ;

            this.client.sendMessage(msg);
        }

        private string prepareBlindProxyBallot(BigInteger[] blindProxyBallot)
        {
            string columns = null;
            for (int i = 0; i < blindProxyBallot.Length; i++)
            {
                if (i != blindProxyBallot.Length - 1)
                    columns = columns + blindProxyBallot[i].ToString() + ",";
                else
                    columns += blindProxyBallot[i].ToString();
            }

            return columns;
        }

        private string prepareTokens(BigInteger SL)
        {
            string tokens = null;
            List<BigInteger> tokenTable = this.serialNumberTokens[SL];
            for (int i = 0; i < tokenTable.Count; i++)
            {
                if (i != tokenTable.Count - 1)
                    tokens = tokens + tokenTable[i].ToString() + ",";
                else
                    tokens = tokens + tokenTable[i].ToString() + ";";
            }
            return tokens;
        }


        public void saveSignedBallot(string message)
        {
            string[] words = message.Split(';');

            foreach (string s in words)
            {
                Console.WriteLine(s);
            }


            string name = words[0];

            string[] signedColumns = words[1].Split(',');
            List<string> signedColumnsList = new List<string>();
            foreach (string s in signedColumns)
            {
                signedColumnsList.Add(s);
            }

            this.proxyBallots[name].SignedColumns = signedColumnsList;
        
        }
    }
}
