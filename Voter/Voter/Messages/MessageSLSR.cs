using NetworkLib;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voter.Messages
{
    class MessageSLSR : Message
    {
        public MessageSLSR()
        {

        }

        public override void Parse(object subject, string msg)
        {
            Voter voter = (Voter)subject;
            string[] elem = msg.Split('=');

            saveSL(voter, elem[0]);
            saveSR(voter, elem[1]);

            Utils.Logs.addLog("Client", NetworkLib.Constants.SR_AND_SR_RECEIVED, true, NetworkLib.Constants.LOG_INFO, true);
            voter.disableSLAndSRButton();
        }

        private void saveSL(Voter voter, string item_to_save)
        {
            BigInteger SL = new BigInteger(item_to_save);
            voter.VoterBallot.SL = SL;
            Utils.Logs.addLog("Client", "SL = " + SL, true, NetworkLib.Constants.LOG_INFO, true);
        }

        private void saveSR(Voter voter, string item_to_save)
        {
            BigInteger SR = new BigInteger(item_to_save);
            voter.VoterBallot.SR = SR;
            Console.WriteLine("SR = " + SR);
        }
    }
}
