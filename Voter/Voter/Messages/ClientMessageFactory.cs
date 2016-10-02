using NetworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voter.Messages
{
    public static class ClientMessageFactory
    {
        public static Message generateMessage(string message_type)
        {
            Message msg;
            switch (message_type)
            {
                case NetworkLib.Constants.SL_AND_SR: //message from Proxy which contains sl and sr number
                    msg = new MessageSLSR();
                    break;
                case NetworkLib.Constants.CONNECTION_SUCCESSFUL:
                    msg = new MessageProxyConnected();
                    break;
                case NetworkLib.Constants.CONNECTED:
                    msg = new MessageEaConnected();
                    break;
                case NetworkLib.Constants.CANDIDATE_LIST_RESPONSE:
                    msg = new MessageCandidateList();
                    break;
                case NetworkLib.Constants.SIGNED_COLUMNS_TOKEN:
                    msg = new MessageSignedColumnAndToken();
                    break;
                default:
                    msg = new BlankMessage();
                    break;
            }

            return msg;
        }
    }
}
