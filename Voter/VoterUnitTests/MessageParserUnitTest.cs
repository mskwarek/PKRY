using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace VoterUnitTests
{
    [TestClass]
    public class MessageParserUnitTest
    {
        [TestMethod]
        public void MessagesParsingUnitTest()
        {
            ListView lw = new ListView();
            Voter.Form1 form = new Voter.Form1();
            form.Show();
            Voter.Configuration config = new Voter.Configuration();
            config.loadConfiguration(ConfigurationUtils.getProperConfigPath());
            Voter.Voter voter = new Voter.Voter(config, form, new Voter.Confirmation(lw));
            foreach (var message_header in MessagesTypes.messages_headers)
            {
                NetworkLib.Message msg = Voter.Messages.ClientMessageFactory.generateMessage(message_header.Key);
                msg.Parse(voter, message_header.Value.Typical_message_to_parse);
            }
            form.Close();
        }
    }
}
