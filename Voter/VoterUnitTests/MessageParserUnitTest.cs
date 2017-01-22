using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace VoterUnitTests
{
    [TestClass]
    public class MessageParserUnitTest : TestVoter
    {
        [TestMethod]
        public void MessagesParsingUnitTest()
        {
            foreach (var message_header in MessagesTypes.messages_headers)
            {
                NetworkLib.Message msg = Voter.Messages.ClientMessageFactory.generateMessage(message_header.Key);
                msg.Parse(voter, message_header.Value.Typical_message_to_parse);
            }
        }
    }
}
