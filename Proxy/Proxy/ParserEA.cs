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
            //msg = FIRST_SL=tokensList[0],tokensList[1],tokensList[2]....:exponentsList[0],exponentsList[1],exponentsList[2]....;SECOND_SL
            Dictionary<BigInteger, List<List<BigInteger>>> dict = new Dictionary<BigInteger, List<List<BigInteger>>>();

            string[] dictionaryElem = msg.Split(';');
            for (int i = 0; i < dictionaryElem.Length; i++)
            {

                string[] words = dictionaryElem[i].Split('=');
                BigInteger SL = new BigInteger(words[0]);
                List<List<BigInteger>> mainList = new List<List<BigInteger>>();

                string[] token = words[1].Split(':');
                //token[0] contains tokenList 
                //token[1] contains exponentsList


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
                case Constants.SL_TOKENS:
                    if (parseSLTokensDictionaryFromEA(elem[1]))
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
