using NetworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voter.Messages
{
    class MessageProxyConnected : Message
    {
        public MessageProxyConnected()
        { }

        public override void Parse(object subject, string msg)
        {
            Voter voter = (Voter)subject;
            voter.disableConnectionProxyButton();
        }
    }
}
