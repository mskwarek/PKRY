using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Math;

namespace Proxy
{
    class ParserEA
    {
        Logs logs;
        Proxy proxy;

        public ParserEA(Logs logs, Proxy proxy)
        {
            this.logs = logs;
            this.proxy = proxy;
        }

        private bool parseSLTokensDictionaryFromEA(string msg)
        {
            Dictionary<BigInteger, List<BigInteger>> dict = new Dictionary<BigInteger, List<BigInteger>>();

            string[] dictionaryElem = msg.Split('&');
            for (int i = 1; i < dictionaryElem.Length; i++)
            {
                string[] elem = dictionaryElem[i].Split('=');
                BigInteger key = new BigInteger(elem[0]);
                string[] dictValues = elem[1].Split(',');
                List<BigInteger> list = new List<BigInteger>();
                foreach (string s in dictValues)
                {
                    list.Add(new BigInteger(s));
                }

                dict.Add(key, list);
            }

            this.proxy.SerialNumberTokens = dict;
            this.proxy.connectSRandSL();
            return true;
        }

        public void parseMessageFromEA(string msg)
        {
            string[] elem = msg.Split('&');
            switch (elem[0])
            {
                case Constants.SL_TOKENS:
                    if (parseSLTokensDictionaryFromEA(msg))
                        this.proxy.Client.sendMessage(Constants.SL_RECEIVED_SUCCESSFULLY + "&");
                    this.logs.addLog(Constants.SL_RECEIVED, true, Constants.LOG_INFO, true);
                    break;
                case Constants.CONNECTED:
                    this.proxy.disableConnectElectionAuthorityButton();
                    this.logs.addLog(Constants.PROXY_CONNECTED_TO_EA, true, Constants.LOG_INFO, true);
                    break;


                case Constants.SIGNED_PROXY_BALLOT:
                    this.proxy.saveSignedBallot(elem[1]);
                    break;
            }


        }
    }
}
