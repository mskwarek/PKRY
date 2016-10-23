using NetworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Voter.Messages
{
    public class MessageCandidateList : Message
    {
        public MessageCandidateList()
        { }

        public override void Parse(object subject, string msg)
        {
            Voter voter = (Voter)subject;
            voter.saveCandidateList(msg);
            voter.showCandidates();
        }
    }
}
