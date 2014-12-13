using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Net.Sockets;

namespace ElectionAuthority
{
    class ElectionAuthority
    {
        ASCIIEncoding encoder;
        private Logs logs;
        //server for client
        private Server serverClient;

        //server for proxy
        private Server serverProxy;

        private CandidateList candidateList;
        private List<String> candidateDefaultList;
        private Configuration configuration;
        //permutation PI
        private Permutation permutation;
        private List<List<BigInteger>> permutationsList;

        //serial number SL
        private List<BigInteger> serialNumberList;

        //tokens, one SL has four tokens
        private List<List<BigInteger>> tokensList;

        //map which connect serialNumber and permuataion
        private Dictionary<BigInteger, List<BigInteger>> dictionarySLPermuation;
        private Dictionary<BigInteger, List<BigInteger>> dictionarySLTokens;

        private int numberOfVoters;

        public ElectionAuthority(Logs logs, Configuration configuration, Server serverClient, Server serverProxy)
        {
            this.encoder = new ASCIIEncoding();
            this.logs = logs;
            this.configuration = configuration;
            //server for Clients
            this.serverClient = serverClient;

            //server for Proxy
            this.serverProxy = serverProxy;

            this.numberOfVoters = Convert.ToInt32(this.configuration.NumberOfVoters);
            permutation = new Permutation(this.logs);
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
            
            logs.addLog(Constants.PERMUTATION_GEN_SUCCESSFULLY, true, Constants.LOG_INFO);
            connectSerialNumberAndPermutation();
        }

        private void generateSerialNumber()
        {
            serialNumberList = new List<BigInteger>();
            serialNumberList = SerialNumberGenerator.generateListOfSerialNumber(this.numberOfVoters, Constants.NUMBER_OF_BITS_SL);
            logs.addLog(Constants.SERIAL_NUMBER_GEN_SUCCESSFULLY, true, Constants.LOG_INFO);
        }

        private void generateTokens()
        {
            this.tokensList = new List<List<BigInteger>>();
            for (int i = 0; i < this.numberOfVoters; i++)
            { // we use the same method like to generate serial number, there is another random generator used inside this method
                this.tokensList.Add(new List<BigInteger>(SerialNumberGenerator.generateListOfSerialNumber(4,Constants.NUMBER_OF_BITS_TOKEN)));
            }
            logs.addLog(Constants.TOKENS_GENERATED_SUCCESSFULLY, true, Constants.LOG_INFO);
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
            this.dictionarySLTokens = new Dictionary<BigInteger, List<BigInteger>>();
            for (int i = 0; i < this.serialNumberList.Count; i++)
            {
                this.dictionarySLTokens.Add(this.serialNumberList[i], this.tokensList[i]);
            }
            for (int i = 0; i < this.serialNumberList.Count; i++)
            {
                Console.WriteLine("klucz");
                Console.WriteLine(this.dictionarySLTokens.ElementAt(i).Key);
                Console.WriteLine("tokeny");
                foreach (BigInteger b in this.dictionarySLTokens.ElementAt(i).Value)
                {
                    Console.WriteLine(b);
                }

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
            string serialNumberAndTokens = Converter.convertDictionaryToString(Constants.SL_TOKENS, this.dictionarySLTokens);
            this.serverProxy.sendMessage(Constants.UNKNOWN, serialNumberAndTokens);
        }
    }
}

