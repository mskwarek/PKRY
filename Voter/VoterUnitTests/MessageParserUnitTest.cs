using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VoterUnitTests
{
    [TestClass]
    public class MessageParserUnitTest
    {
        [TestMethod]
        public void MessagesParsingUnitTest()
        {
            foreach (var message_header in MessagesTypes.messages_headers)
            {
                NetworkLib.Message msg = Voter.Messages.ClientMessageFactory.generateMessage(message_header.Key);
                msg.Parse(null, null); //TODO
            }
        }
    }
}
