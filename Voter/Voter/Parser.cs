using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Voter
{
    class Parser
    {
        private Voter voter;
        private Logs logs;

        public Parser(Logs logs, Voter voter)
        {
            this.voter = voter;
            this.logs = logs;

        }

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
                case Constants.YES_NO_POSITION:
                    saveYesNoPosition(elem[1]);
                    break;


            }


        }

        private void saveYesNoPosition(string position)
        {
            this.voter.saveYesNoPositon(position);
        }

        private void disableConnectionEAButton()
        {
            this.voter.disableConnectionEAButton(); 
        }

        private void saveCandidateList(string list)
        {
            this.voter.saveCandidateList(list);
        }

        private void disableConnectionProxyButton()
        {
            this.voter.disableConnectionProxyButton();

        }

        private void saveSLAndSR(string msg)
        {
            string[] elem = msg.Split('=');
            BigInteger SL = BigInteger.Parse(elem[0]);
            BigInteger SR = BigInteger.Parse(elem[1]);

            this.voter.VoterBallot.SL = SL;
            Console.WriteLine("SL = " + SL);
            this.voter.VoterBallot.SR = SR;
            Console.WriteLine("SR = " + SR);

            logs.addLog(Constants.SR_AND_SR_RECEIVED, true, Constants.LOG_INFO, true);
            this.voter.disableSLAndSRButton();
        }
    }
}
