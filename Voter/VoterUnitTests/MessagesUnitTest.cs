using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace VoterUnitTests
{
    public static class MessagesTypes
    {
        public static Dictionary<string, object> messages_headers = new Dictionary<string, object>() {
                { NetworkLib.Constants.SL_AND_SR, new Voter.Messages.MessageSLSR()},
                { NetworkLib.Constants.CONNECTION_SUCCESSFUL, new Voter.Messages.MessageProxyConnected()},
                { NetworkLib.Constants.CONNECTED, new Voter.Messages.MessageEaConnected() },
                { NetworkLib.Constants.CANDIDATE_LIST_RESPONSE, new Voter.Messages.MessageCandidateList() },
                { NetworkLib.Constants.SIGNED_COLUMNS_TOKEN, new Voter.Messages.MessageSignedColumnAndToken() },
                { "DEFAULT_UNKNOWN", new NetworkLib.BlankMessage() } };

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
                Assert.AreEqual(Voter.Messages.ClientMessageFactory.generateMessage(message_header.Key).GetType(), message_header.Value.GetType());
            }
        }
    }
}
