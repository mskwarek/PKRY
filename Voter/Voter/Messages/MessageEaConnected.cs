using NetworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voter.Messages
{
    class MessageEaConnected : Message
    {
        public MessageEaConnected()
        { }

        public override void Parse(object subject, string msg)
        {
            Voter voter = (Voter)subject;
            voter.disableConnectionEAButton();
        }
    }
}
