using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Proxy
{
    class Proxy
    {
        private Logs logs;
        private Configuration configuration;

        //serial number SR
        private List<BigInteger> sr_List;
        private List<ProxyBallot> proxyBallots;

        private int numberOfVoters;

        private Dictionary<BigInteger, List<BigInteger>> serialNumberTokens; //dictionary contains serialNumber and tokens connected with that SL
        public Dictionary<BigInteger, List<BigInteger>> SerialNumberTokens
        {
            get { return this.serialNumberTokens; }
            set {this.serialNumberTokens = value;}
        }


        public Proxy(Logs logs, Configuration conf)
        {
            this.logs = logs;
            this.configuration = conf;
            this.serialNumberTokens = new Dictionary<BigInteger,List<BigInteger>>();
            //getting numb of voters from EA?
            //this.numberOfVoters = Convert.ToInt32(this.configuration.NumberOfVoters);
        }

    }
}
