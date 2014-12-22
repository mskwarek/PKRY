using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Net.Sockets;

namespace Proxy
{
    class Proxy
    {
        private Logs logs;
        private Configuration configuration;

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
        private List<ProxyBallot> proxyBallots;

        private int numberOfVoters;

        private Dictionary<BigInteger, List<BigInteger>> serialNumberTokens; //dictionary contains serialNumber and tokens connected with that SL
        public Dictionary<BigInteger, List<BigInteger>> SerialNumberTokens
        {
            get { return this.serialNumberTokens; }
            set {this.serialNumberTokens = value;}
        }

        private Dictionary<BigInteger, BigInteger> serialNumberAndSR; //connects serialNumbers SL and SR 
        private static int numOfSentSLandSR = 0; //number of SL and SR send to voter, incremented when request comes from voter


        public Proxy(Logs logs, Configuration conf)
        {
            this.logs = logs;
            this.configuration = conf;

            this.server = new Server(this.logs,this);

            this.client = new Client(this.logs, this);



            this.serialNumberTokens = new Dictionary<BigInteger,List<BigInteger>>();
            this.SRList = new List<BigInteger>();
            this.serialNumberAndSR = new Dictionary<BigInteger, BigInteger>();
            
        }


        public void generateSR()
        {
            this.numberOfVoters = this.configuration.NumOfVoters;
            Console.WriteLine("num of voters" + this.numberOfVoters);
            this.SRList = SerialNumberGenerator.generateListOfSerialNumber(this.numberOfVoters, Constants.NUMBER_OF_BITS_SR);
            logs.addLog(Constants.SR_GEN_SUCCESSFULLY, true, Constants.LOG_INFO);

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
                string msg = Constants.SL_AND_SR + "&" + this.serialNumberAndSR.ElementAt(numOfSentSLandSR).Key.ToString()
                     + "=" +
                    this.serialNumberAndSR.ElementAt(numOfSentSLandSR).Value.ToString();
                //msg = SL_AND_SR&keySL = valueSR
                numOfSentSLandSR += 1;
                this.server.sendMessage(name, msg);
            }
            else
            {
                this.logs.addLog(Constants.ERROR_SEND_SL_AND_SR, true, Constants.LOG_ERROR, true);
            }
            

        }
    }
}
