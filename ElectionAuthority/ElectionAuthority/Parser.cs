using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ElectionAuthority
{
    /// <summary>
    /// parsing messages recived form clients
    /// </summary>
    class Parser
    {
        
        /// <summary>
        /// allows to collect and display logs
        /// </summary>
        private Logs logs;

        /// <summary>
        /// parser connected to election authority
        /// </summary>
        private ElectionAuthority electionAuthority;

        /// <summary>
        /// parser's constructor
        /// </summary>
        /// <param name="logs">log instance</param>
        /// <param name="electionAuthority">election authority instance</param>
        public Parser(Logs logs, ElectionAuthority electionAuthority)
        {
            this.logs = logs;
            this.electionAuthority = electionAuthority;
        }

        /// <summary>
        /// parses message
        /// </summary>
        /// <param name="msg">recived message</param>
        /// <returns>parsing result</returns>
        public bool parseMessage(string msg)
        {

            string[] words = msg.Split('&');
            switch (words[0])

            {
                case NetworkLib.Constants.SL_RECEIVED_SUCCESSFULLY:
                    this.logs.addLog(NetworkLib.Constants.SL_AND_SR_SENT_SUCCESSFULLY, true, NetworkLib.Constants.LOG_INFO, true);
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
            }


            return false;
        }
    }
}
