using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Math;

namespace Voter
{
    /// <summary>
    /// parsing recived messages
    /// </summary>
    class Parser
    {
        /// <summary>
        /// parser connected with 
        /// </summary>
        private Voter voter;
       
        /// <summary>
        /// allows to collect and display logs
        /// </summary>
        private Logs logs;

        
        /// <summary>
        /// parser's constructor
        /// </summary>
        /// <param name="logs">log instance</param>
        /// <param name="voter">voter instance</param>
        public Parser(Logs logs, Voter voter)
        {
            this.voter = voter;
            this.logs = logs;

        }

        /// <summary>
        /// parses message
        /// </summary>
        /// <param name="msg">recived message</param>
        /// <returns>parsing result</returns>
        public void parseMessage(string msg)
        {
            string[] elem = msg.Split('&');
            switch (elem[0])
            {
                case Constants.SL_AND_SR: //message from Proxy which contains sl and sr number
                    saveSLAndSR(elem[1]);
                    break;

                case Constants.CONNECTION_SUCCESSFUL:
                    disableConnectionProxyButton();
                    break;
                case Constants.CONNECTED:
                    disableConnectionEAButton();
                    break;
                case Constants.CANDIDATE_LIST_RESPONSE:
                    saveCandidateList(elem[1]);
                    break;
                case Constants.SIGNED_COLUMNS_TOKEN:
                    saveSignedColumnAndToken(elem[1]);
                    break;

            }

        }

        /// <summary>
        /// saves signed column and token
        /// </summary>
        /// <param name="message">recived message</param>
        private void saveSignedColumnAndToken(string message)
        {
            this.voter.saveSignedColumnAndToken(message);
        }


        /// <summary>
        /// disables connection button
        /// </summary>
        private void disableConnectionEAButton()
        {
            this.voter.disableConnectionEAButton(); 
        }

        /// <summary>
        /// saves candidates list
        /// </summary>
        /// <param name="list">list of candidates</param>
        private void saveCandidateList(string list)
        {
            this.voter.saveCandidateList(list);
        }

        /// <summary>
        /// disables connection to proxy button
        /// </summary>
        private void disableConnectionProxyButton()
        {
            this.voter.disableConnectionProxyButton();

        }

        /// <summary>
        /// saves SR and SL which voter gets
        /// </summary>
        /// <param name="msg"></param>
        private void saveSLAndSR(string msg)
        {
            string[] elem = msg.Split('=');
            BigInteger SL = new BigInteger(elem[0]);

            BigInteger SR = new BigInteger(elem[1]);

            this.voter.VoterBallot.SL = SL;
            Console.WriteLine("SL = " + SL);
            this.voter.VoterBallot.SR = SR;
            Console.WriteLine("SR = " + SR);

            logs.addLog(Constants.SR_AND_SR_RECEIVED, true, Constants.LOG_INFO, true);
            this.voter.disableSLAndSRButton();
        }
    }
}
