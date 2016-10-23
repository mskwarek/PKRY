using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ElectionAuthority
{
    class Parser
    {
        private ElectionAuthority electionAuthority;

        public Parser(ElectionAuthority electionAuthority)
        {
            this.electionAuthority = electionAuthority;
        }

        public bool parseMessage(string msg)
        {
            string[] words = msg.Split('&');
            switch (words[0])
            {
                case NetworkLib.Constants.SL_RECEIVED_SUCCESSFULLY:
                    Utils.Logs.addLog("EA", NetworkLib.Constants.SL_AND_SR_SENT_SUCCESSFULLY, true, NetworkLib.Constants.LOG_INFO, true);
                    this.electionAuthority.disableSendSLTokensAndTokensButton();
                    return true;
                case NetworkLib.Constants.GET_CANDIDATE_LIST:
                    string[] str = words[1].Split('=');
                    string name = str[0];
                    BigInteger SL = new BigInteger(str[1]);
                    this.electionAuthority.getCandidateListPermuated(name, SL);
                    return true;
                case NetworkLib.Constants.BLIND_PROXY_BALLOT:
                    this.electionAuthority.saveBlindBallotMatrix(words[1]);
                    return true;
                case NetworkLib.Constants.UNBLINED_BALLOT_MATRIX:
                    this.electionAuthority.saveUnblindedBallotMatrix(words[1]);
                    return true;
                default:
                    return false;
            }
        }
    }
}
