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

        public Proxy(Logs logs, Configuration conf, Server server)
        {
            this.logs = logs;
            this.configuration = conf;
            this.server = server;
            this.serialNumberTokens = new Dictionary<BigInteger,List<BigInteger>>();
            this.serialNumberAndSR = new Dictionary<BigInteger, BigInteger>();
            this.numberOfVoters = this.configuration.NumOfVoters;
        }


        public void generateSR()
        {
            SRList = new List<BigInteger>();
            SRList = SerialNumberGenerator.generateListOfSerialNumber(this.numberOfVoters, Constants.NUMBER_OF_BITS_SR);
            logs.addLog(Constants.SR_GEN_SUCCESSFULLY, true, Constants.LOG_INFO);
        }


        internal void connectSRandSL()
        {
            for(int i=0; i<this.SRList.Count; i++)
            {
                this.serialNumberAndSR.Add(serialNumberTokens.ElementAt(i).Key, SRList[i]);
            }
            logs.addLog(Constants.SR_CONNECTED_WITH_SL, true, Constants.LOG_INFO);
        }

        public void sendSLAndSR()
        {

        }
    }
}
