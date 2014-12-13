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


        public Proxy(Logs logs, Configuration conf)
        {
            this.logs = logs;
            this.configuration = conf;

            //getting numb of voters from EA?
            //this.numberOfVoters = Convert.ToInt32(this.configuration.NumberOfVoters);
        }
    }
}
