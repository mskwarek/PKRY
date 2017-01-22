using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Math;

namespace Proxy
{
    class ParserEA
    {
        Proxy proxy;

        public ParserEA(Proxy proxy)
        {
            this.proxy = proxy;
        }

        private bool parseSLTokensDictionaryFromEA(string msg)
        {
            Dictionary<BigInteger, List<List<BigInteger>>> dict = new Dictionary<BigInteger, List<List<BigInteger>>>();

            string[] dictionaryElem = msg.Split(';');
            for (int i = 0; i < dictionaryElem.Length; i++)
            {

                string[] words = dictionaryElem[i].Split('=');
                BigInteger SL = new BigInteger(words[0]);
                List<List<BigInteger>> mainList = new List<List<BigInteger>>();

                string[] token = words[1].Split(':');


                string[] tokenList = token[0].Split(',');
                List<BigInteger> firstList = new List<BigInteger>();
                foreach (string str in tokenList)
                {
                    firstList.Add(new BigInteger(str));
                }
                mainList.Add(firstList);


                string[] exponentsList = token[1].Split(',');
                List<BigInteger> secondList = new List<BigInteger>();
                foreach (string str in exponentsList)
                {
                    secondList.Add(new BigInteger(str));
                }
                mainList.Add(secondList);


                dict.Add(SL, mainList);


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
                case NetworkLib.Constants.SL_TOKENS:
                    if (parseSLTokensDictionaryFromEA(elem[1]))
                        this.proxy.Client.sendMessage(NetworkLib.Constants.SL_RECEIVED_SUCCESSFULLY + "&");
                    Utils.Logs.addLog("Client", NetworkLib.Constants.SL_RECEIVED, true, NetworkLib.Constants.LOG_INFO, true);
                    break;
                case NetworkLib.Constants.CONNECTED:
                    this.proxy.disableConnectElectionAuthorityButton();
                    Utils.Logs.addLog("Client", NetworkLib.Constants.PROXY_CONNECTED_TO_EA, true, NetworkLib.Constants.LOG_INFO, true);
                    break;
                case NetworkLib.Constants.SIGNED_PROXY_BALLOT:
                    this.proxy.saveSignedBallot(elem[1]);
                    break;
            }
        }
    }
}
