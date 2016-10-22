using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace VoterUnitTests
{
    public class MessageFeatures
    {
        private NetworkLib.Message message;
        private string typical_message_to_parse = "";
        public MessageFeatures(NetworkLib.Message msg, string typical_message_to_parse)
        {
            this.message = msg;
            this.typical_message_to_parse = typical_message_to_parse;
        }
        public NetworkLib.Message Message
        {
            get { return this.message; }
        }
        public string Typical_message_to_parse
        {
            get { return this.typical_message_to_parse; }
        }
    }
    public static class MessagesTypes
    {
        public static Dictionary<string, MessageFeatures> messages_headers = new Dictionary<string, MessageFeatures>() {
                { NetworkLib.Constants.CONNECTION_SUCCESSFUL, new MessageFeatures(new Voter.Messages.MessageProxyConnected(), "" )},
                { NetworkLib.Constants.CONNECTED, new MessageFeatures(new Voter.Messages.MessageEaConnected(), "" )},
                { NetworkLib.Constants.CANDIDATE_LIST_RESPONSE, new MessageFeatures(new Voter.Messages.MessageCandidateList(), "M;A;R;C;I I;N" ) },
                { NetworkLib.Constants.SL_AND_SR, new MessageFeatures(new Voter.Messages.MessageSLSR(), "100=101" )},
                { NetworkLib.Constants.SIGNED_COLUMNS_TOKEN, new MessageFeatures(new Voter.Messages.MessageSignedColumnAndToken(), "1111111;222222" ) },
                { "DEFAULT_UNKNOWN", new MessageFeatures(new NetworkLib.BlankMessage(), "" ) } };

    }
    [TestClass]
    public class MessagesUnitTest
    {
        [TestMethod]
        public void MessagesFactoryUnitTest()
        {

            foreach (var message_header in MessagesTypes.messages_headers)
            {
                Assert.IsNotNull(Voter.Messages.ClientMessageFactory.generateMessage(message_header.Key));
                Assert.AreEqual(Voter.Messages.ClientMessageFactory.generateMessage(message_header.Key).GetType(), message_header.Value.Message.GetType());
            }
        }
    }
}
