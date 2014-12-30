using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ElectionAuthority
{
    class Parser
    {
        private Logs logs;
        private ElectionAuthority electionAuthority;

        public Parser(Logs logs, ElectionAuthority electionAuthority)
        {
            this.logs = logs;
            this.electionAuthority = electionAuthority;
        }


        public bool parseMessage(string msg)
        {
            Console.WriteLine("w paresMessage mamy " + msg);
            string[] words = msg.Split('&');
            switch (words[0])

            {
                case Constants.SL_RECEIVED_SUCCESSFULLY:
                    this.logs.addLog(Constants.SL_AND_SR_SENT_SUCCESSFULLY, true, Constants.LOG_INFO, true);
                    this.electionAuthority.disableSendSLTokensAndTokensButton();
                    return true;
                case Constants.GET_CANDIDATE_LIST:
                    string[] str = words[1].Split('=');
                    string name = str[0];
                    BigInteger SL = new BigInteger(str[1]);
                    this.electionAuthority.getCandidateListPermuated(name, SL);
                    return true;
                case Constants.BLIND_PROXY_BALLOT:
                    this.electionAuthority.saveBlindBallotMatrix(words[1]);
                    return true;
                case Constants.UNBLINED_BALLOT_MATRIX:
                    this.electionAuthority.saveUnblindedBallotMatrix(words[1]);
                    return true;
            }


            return false;
        }
    }
}
