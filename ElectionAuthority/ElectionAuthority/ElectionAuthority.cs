using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;

namespace ElectionAuthority
{
    class ElectionAuthority
    {
        ASCIIEncoding encoder;
        private Logs logs;
        private Form1 form;
        private Server serverClient; // server for clients (voters)
        public Server ServerClient
        {
            get { return serverClient; }
        }
        private Server serverProxy; // server for proxy
        public Server ServerProxy
        {
            get { return serverProxy; }
        }
        private CandidateList candidateList;
        private List<String> candidateDefaultList;
        private Configuration configuration;
        //permutation PI
        private Permutation permutation;
        private List<List<BigInteger>> permutationsList;
        private List<List<BigInteger>> inversePermutationList;

        //serial number SL
        private List<BigInteger> serialNumberList;

        //tokens, one SL has four tokens
        private List<List<BigInteger>> tokensList;          //for (un)blinding (proxy and EA)
        private List<List<BigInteger>> exponentsList;       //for blinding (proxy)
        private List<List<BigInteger>> signatureFactor;     //for signature (EA)

        //map which connect serialNumber and permuataion
        private Dictionary<BigInteger, List<BigInteger>> dictionarySLPermuation;
        private Dictionary<BigInteger, List<BigInteger>> dictionarySLInversePermutation;
        private Dictionary<BigInteger, List<List<BigInteger>>> dictionarySLTokens;

        private Dictionary<string, Ballot> ballots;

        private int numberOfVoters;

        public ElectionAuthority(Logs logs, Configuration configuration, Form1 form)
        {
            this.encoder = new ASCIIEncoding();
            this.logs = logs;
            this.configuration = configuration;
            this.form = form;
            //server for Clients
            this.serverClient = new Server(this.logs,this);

            
            //server for Proxy
            this.serverProxy = new Server(this.logs,this);

            this.numberOfVoters = Convert.ToInt32(this.configuration.NumberOfVoters);
            permutation = new Permutation(this.logs);

            this.ballots = new Dictionary<string, Ballot>();
        }

        public void loadCandidateList(string pathToElectionAuthorityConfig)
        {
            //pathToElectionAuthorityConfig it's a path to file which contains ElectionAuthority config
            //we have to rewrite one to be suitiable for list candidate xml
            candidateDefaultList = new List<String>();
            candidateList = new CandidateList(this.logs);

            string pathToCandidateList = candidateList.getPathToCandidateList(pathToElectionAuthorityConfig);
            candidateDefaultList = candidateList.loadCanidateList(pathToCandidateList);
        }

        //** Start methods to generate tokens and permutation 
        private void generatePermutation()
        {
            
            permutationsList = new List<List<BigInteger>>();
            for (int i = 0; i < this.numberOfVoters; i++)
            {
                this.permutationsList.Add(new List<BigInteger>(this.permutation.generatePermutation(candidateDefaultList.Count)));
            }

            connectSerialNumberAndPermutation();
            generateInversePermutation();
            logs.addLog(Constants.PERMUTATION_GEN_SUCCESSFULLY, true, Constants.LOG_INFO);
            
        }

        private void generateInversePermutation()
        {
            this.inversePermutationList = new List<List<BigInteger>>();
            for (int i = 0; i < this.numberOfVoters; i++)
            {
                this.inversePermutationList.Add(this.permutation.getInversePermutation(this.permutationsList[i]));
            }
            logs.addLog(Constants.GENERATE_INVERSE_PERMUTATION, true, Constants.LOG_INFO, true);
            connectSerialNumberAndInversePermutation();
        
        }

        private void connectSerialNumberAndInversePermutation()
        {
            dictionarySLInversePermutation = new Dictionary<BigInteger, List<BigInteger>>();
            for (int i = 0; i < this.serialNumberList.Count; i++)
            {
                dictionarySLInversePermutation.Add(this.serialNumberList[i], this.inversePermutationList[i]);
            }
            logs.addLog(Constants.SL_CONNECTED_WITH_INVERSE_PERMUTATION, true, Constants.LOG_INFO,true);
        }

        private void generateSerialNumber()
        {
            serialNumberList = new List<BigInteger>();
            serialNumberList = SerialNumberGenerator.generateListOfSerialNumber(this.numberOfVoters, Constants.NUMBER_OF_BITS_SL);
            
            logs.addLog(Constants.SERIAL_NUMBER_GEN_SUCCESSFULLY, true, Constants.LOG_INFO, true);
        }

        private void generateTokens()
        {
            this.tokensList = new List<List<BigInteger>>();
            this.exponentsList = new List<List<BigInteger>>();
            this.signatureFactor = new List<List<BigInteger>>();

            List<List<AsymmetricCipherKeyPair>>  PreTokensList = new List<List<AsymmetricCipherKeyPair>>();
            for (int i = 0; i < this.numberOfVoters; i++)
            { // we use the same method like to generate serial number, there is another random generator used inside this method
                List<AsymmetricCipherKeyPair> preToken = new List<AsymmetricCipherKeyPair>(SerialNumberGenerator.generatePreTokens(4, Constants.NUMBER_OF_BITS_TOKEN));
                PreTokensList.Add(preToken);
                List<BigInteger> tokens = new List<BigInteger>();
                List<BigInteger> exps = new List<BigInteger>();
                List<BigInteger> signFactor = new List<BigInteger>();

                foreach (AsymmetricCipherKeyPair token in preToken)
                {
                    RsaKeyParameters publicKey = (RsaKeyParameters)token.Public;
                    RsaKeyParameters privKey = (RsaKeyParameters)token.Private;
                    tokens.Add(publicKey.Modulus);
                    exps.Add(publicKey.Exponent);
                    signFactor.Add(privKey.Exponent);
                }
                this.tokensList.Add(tokens);
                this.exponentsList.Add(exps);
                this.signatureFactor.Add(signFactor);
            }
            logs.addLog(Constants.TOKENS_GENERATED_SUCCESSFULLY, true, Constants.LOG_INFO, true);
            connectSerialNumberAndTokens();

        }

        private void connectSerialNumberAndPermutation()
        {
            dictionarySLPermuation = new Dictionary<BigInteger, List<BigInteger>>();
            for (int i = 0; i < this.serialNumberList.Count; i++)
            {
                dictionarySLPermuation.Add(this.serialNumberList[i], this.permutationsList[i]);
            }
            logs.addLog(Constants.SL_CONNECTED_WITH_PERMUTATION, true, Constants.LOG_INFO);
            
        }

        private void connectSerialNumberAndTokens()
        {
            this.dictionarySLTokens = new Dictionary<BigInteger, List<List<BigInteger>>>();
            for (int i = 0; i < this.serialNumberList.Count; i++)
            {
                List<List<BigInteger>> tokens = new List<List<BigInteger>>();
                tokens.Add(tokensList[i]);
                tokens.Add(exponentsList[i]);
                tokens.Add(signatureFactor[i]);
                this.dictionarySLTokens.Add(this.serialNumberList[i], tokens);
            }


            

            logs.addLog(Constants.SL_CONNECTED_WITH_TOKENS, true, Constants.LOG_INFO);
        }

        public void generateDate()
        {
            generateSerialNumber();
            generatePermutation();
            generateTokens();
        }
        //** End methods to generate tokens and permutation 

        public void sendSLAndTokensToProxy()
        {
            //before sending we have to convert dictionary to string. We use our own conversion to recoginize message in proxy and reparse it to dictionary
            // msg = SL_TOKENS&FIRST_SL=tokensList[0],tokensList[1],tokensList[2]....:exponentsList[0],exponentsList[1],exponentsList[2]....;SECOND_SL

            string msg = Constants.SL_TOKENS + "&";
            for (int i =0; i<this.serialNumberList.Count;i++)
            {

                msg = msg + this.serialNumberList[i].ToString() + "=";
                for (int j =0; j<this.tokensList[i].Count;j++)
                {
                    if (j == this.tokensList[i].Count - 1)
                        msg = msg + this.tokensList[i][j].ToString() + ":";

                    else
                        msg = msg + this.tokensList[i][j].ToString() + ",";

                }

                for (int j = 0; j < this.exponentsList[i].Count; j++)
                {
                    if (j == this.exponentsList[i].Count - 1)
                        msg += this.exponentsList[i][j].ToString();

                    else
                        msg = msg + this.exponentsList[i][j].ToString() + ",";

                }

                if (i != this.serialNumberList.Count -1 )
                    msg += ";";

            }
            this.serverProxy.sendMessage(Constants.PROXY, msg);
        }

        public void disableSendSLTokensAndTokensButton()
        {
            this.form.Invoke(new MethodInvoker(delegate()
                {
                    this.form.disableSendSLTokensAndTokensButton();
                }));

        }

        public void getCandidateListPermuated(string name, BigInteger SL)
        {
            List<BigInteger> permutation = new List<BigInteger>();
            permutation = this.dictionarySLPermuation[SL];

            List<String> candidateList = new List<string>();

            for (int i = 0; i < this.candidateDefaultList.Count; i++)
            {
                int index = permutation[i].IntValue;
                candidateList.Add(candidateDefaultList[index-1]);
            }

            string candidateListString = Constants.CANDIDATE_LIST_RESPONSE + "&";

            for(int i =0; i<candidateList.Count;i++)
            {
                if (i < candidateList.Count - 1)
                    candidateListString += candidateList[i] + ";";
                else
                    candidateListString += candidateList[i];
            }

            this.serverClient.sendMessage(name, candidateListString);

        }

        public void saveBlindBallotMatrix(string message)
        {
            string[] words = message.Split(';');
            string name = words[0];

            BigInteger pubKeyModulus = new BigInteger(words[1]);
            BigInteger SL = new BigInteger(words[2]);

            List<BigInteger> tokenList = new List<BigInteger>();
            string[] strTokens = words[3].Split(',');
            for(int i =0; i<strTokens.Length; i++)
            {
                tokenList.Add(new BigInteger(strTokens[i]));
            }


            List<BigInteger> exponentList = new List<BigInteger>();
            string[] strExpo = words[4].Split(',');
            for (int i = 0; i < strExpo.Length; i++)
            {
                exponentList.Add(new BigInteger(strExpo[i]));
            }


            BigInteger[] columns = new BigInteger[4];
            string[] strColumns = words[4].Split(',');
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = new BigInteger(strColumns[i]);
            }

            this.ballots.Add(name, new Ballot(SL));
            
            this.ballots[name].BlindColumn = columns;
            this.ballots[name].Permutation = this.dictionarySLPermuation[SL];
            this.ballots[name].InversePermutation = this.dictionarySLInversePermutation[SL];
            this.ballots[name].TokenList = tokenList;
            this.ballots[name].ExponentsList = exponentList;
            this.ballots[name].SignatureFactor = this.dictionarySLTokens[SL][2];

            this.ballots[name].PubKeyModulus = pubKeyModulus;
            this.logs.addLog(Constants.BLIND_PROXY_BALLOT_RECEIVED + name, true, Constants.LOG_INFO, true);

            this.signColumn(name);
   

        }

        private void signColumn(string name)
        {
            //msg = BLIND_PROXY_BALLOT&name;signCol1,signCol2,signCol3,signCol4
            this.ballots[name].signColumn();
            string signColumns = null;
            
            for (int i =0 ;i<this.ballots[name].SignedColumn.Length;i++)
            {
                if (i == this.ballots[name].SignedColumn.Length -1)
                    signColumns += this.ballots[name].SignedColumn[i].ToString();
                else
                    signColumns = signColumns + this.ballots[name].SignedColumn[i].ToString() + ",";
            }


            string msg = Constants.SIGNED_PROXY_BALLOT + "&" + name + ";" + signColumns;
            this.serverProxy.sendMessage(Constants.PROXY, msg);
            this.logs.addLog(Constants.SIGNED_BALLOT_MATRIX_SENT, true, Constants.LOG_INFO, true);
        
        }

        
    }
}

