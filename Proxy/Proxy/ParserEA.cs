using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

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

        private void parseDictionaryFromEA(string msg)
        {
            Dictionary<BigInteger, List<BigInteger>> dict = new Dictionary<BigInteger, List<BigInteger>>();

            string[] dictionaryElem = msg.Split('&');
            for (int i = 1; i < dictionaryElem.Length; i++)
            {
                string[] elem = dictionaryElem[i].Split('=');
                BigInteger key = BigInteger.Parse(elem[0]);
                string[] dictValues = elem[1].Split(',');
                List<BigInteger> list = new List<BigInteger>();
                foreach (string s in dictValues)
                {
                    list.Add(BigInteger.Parse(s));
                }

                dict.Add(key, list);
            }

            this.proxy.SerialNumberTokens = dict;

            foreach (BigInteger b in this.proxy.SerialNumberTokens.Keys)
            {
                Console.WriteLine(b);
                foreach(BigInteger val in this.proxy.SerialNumberTokens[b])
                {
                    Console.WriteLine(val);
                }
            }
        }

        public void parseMessageFromEA(string msg)
        {
            string[] elem = msg.Split('&');
            switch (elem[0])
            {
                case Constants.SL_TOKENS:
                    parseDictionaryFromEA(msg);
                    break;


            }


        }
    }
}
