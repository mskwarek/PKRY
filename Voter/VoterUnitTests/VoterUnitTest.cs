using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.Threading;

namespace VoterUnitTests
{
    public class TestVoter
    {
        ListView lw;
        Voter.Form1 form;
        Voter.Configuration config;
        public Voter.Voter voter;
        protected NetworkLib.Server ea;
        protected NetworkLib.Server proxy;

        public TestVoter()
        {
            lw = new ListView();
            form = new Voter.Form1();
            form.Show();
            config = new Voter.Configuration();
            config.loadConfiguration(ConfigurationUtils.getProperConfigPath());
            voter = new Voter.Voter(config, form, new Voter.Confirmation(lw));
            ea = new NetworkLib.Server(15000);
            proxy = new NetworkLib.Server(16000);
            Thread.Sleep(1000);
        }

        ~TestVoter()
        {
            form.Close();
            ea.stopServer();
            proxy.stopServer();
        }
    }

    [TestClass]
    public class VoterUnitTest : TestVoter
    {
        [TestMethod]
        public void GetProxyClientTest()
        {
            Assert.IsNotNull(voter.ProxyClient);
            Assert.IsTrue(voter.ProxyClient.Connected);
            voter.ClientConnect();
        }

        [TestMethod]
        public void GetEAClientTest()
        {
            Assert.IsNotNull(voter.ElectionAuthorityClient);
            Assert.IsTrue(voter.ElectionAuthorityClient.Connected);
            voter.ClientEAConnect();
        }
        [TestMethod]
        public void SLSRRequest()
        {
            voter.ClientConnect();
            voter.ClientEAConnect();
            voter.requestForSLandSR();
            //ea.sendMessage()
            voter.ElectionAuthorityClient.disconnect();
        }

        [TestMethod]
        public void requestCandidateList()
        {
            voter.ClientConnect();
            voter.ClientEAConnect();
            voter.requestForCandidatesList();
            voter.ProxyClient.disconnect();
        }
    }
}
