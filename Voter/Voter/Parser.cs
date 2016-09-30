using System;
using Org.BouncyCastle.Math;

namespace Voter
{

    class Parser
    {
        private Voter voter;

        public Parser(Voter voter)
        {
            this.voter = voter;
        }

        public void parseMessage(string msg)
        {
            string[] elem = msg.Split('&');
            switch (elem[0])
            {
                case NetworkLib.Constants.SL_AND_SR: //message from Proxy which contains sl and sr number
                    saveSLAndSR(elem[1]);
                    break;

                case NetworkLib.Constants.CONNECTION_SUCCESSFUL:
                    disableConnectionProxyButton();
                    break;
                case NetworkLib.Constants.CONNECTED:
                    disableConnectionEAButton();
                    break;
                case NetworkLib.Constants.CANDIDATE_LIST_RESPONSE:
                    saveCandidateList(elem[1]);
                    break;
                case NetworkLib.Constants.SIGNED_COLUMNS_TOKEN:
                    saveSignedColumnAndToken(elem[1]);
                    break;

            }

        }

        private void saveSignedColumnAndToken(string message)
        {
            this.voter.saveSignedColumnAndToken(message);
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

            saveSL(elem[0]);
            saveSR(elem[1]);

            Utils.Logs.addLog("Client", NetworkLib.Constants.SR_AND_SR_RECEIVED, true, NetworkLib.Constants.LOG_INFO, true);
            this.voter.disableSLAndSRButton();
        }

        private void saveSL(string item_to_save)
        {
            BigInteger SL = new BigInteger(item_to_save);
            this.voter.VoterBallot.SL = SL;
            Console.WriteLine("SL = " + SL);
        }

        private void saveSR(string item_to_save)
        {
            BigInteger SR = new BigInteger(item_to_save);
            this.voter.VoterBallot.SR = SR;
            Console.WriteLine("SR = " + SR);
        }
    }
}
