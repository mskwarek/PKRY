using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.Threading;
using ElectionAuthority;

namespace VoterUnitTests
{
    public class TestVoter
    {
        ListView lw;
        Voter.Form1 form;
        Voter.Configuration config;
        public Voter.Voter voter;
        //protected NetworkLib.Server ea;
        //protected NetworkLib.Server proxy;

        public TestVoter()
        {
            lw = new ListView();
            form = new Voter.Form1();
            form.Show();
            config = new Voter.Configuration();
            config.loadConfiguration(ConfigurationUtils.getProperConfigPath());
            voter = new Voter.Voter(config, form, new Voter.Confirmation(lw));
            //ea = new NetworkLib.Server(15000);
            //proxy = new NetworkLib.Server(16000);
            Thread.Sleep(1000);
        }

        ~TestVoter()
        {
            form.Close();
            //ea.stopServer();
            //proxy.stopServer();
        }
    }

    [TestClass]
    public class VoterUnitTest : TestVoter
    {
        [TestMethod]
        public void GetProxyClientTest()
        {
            //Assert.IsNotNull(voter.ProxyClient);
            //voter.ClientConnect();
            //Assert.IsTrue(voter.ProxyClient.Connected);        
        }

        [TestMethod]
        public void GetEAClientTest()
        {
            //Assert.IsNotNull(voter.ElectionAuthorityClient);
            //Assert.IsTrue(voter.ElectionAuthorityClient.Connected);
            //voter.ClientEAConnect();
        }
        [TestMethod]
        public void SLSRRequest()
        {
            //voter.ClientConnect();
            //voter.ClientEAConnect();
            //voter.requestForSLandSR();
            //Thread.Sleep(1000);
            //ea.sendMessage()
            //voter.ElectionAuthorityClient.disconnect();
            //voter.ProxyClient.disconnect();
        }

        [TestMethod]
        public void requestCandidateList()
        {
            ElectionAuthority.Form1 form = new ElectionAuthority.Form1();
            form.loadConfig(ConfigurationUtils.getProperEAConfigPath());
            form.startEaService();
            Thread.Sleep(100);
            Proxy.Form1 formProxy = new Proxy.Form1();
            formProxy.loadConfig(ConfigurationUtils.getProperProxyConfigPath());
            formProxy.startProxyService();
            formProxy.connectToEA();

            Thread.Sleep(100);
            voter.ClientConnect();
            voter.ClientEAConnect();
            Thread.Sleep(100);

            Assert.IsTrue(voter.ElectionAuthorityClient.Connected);
            Assert.IsTrue(voter.ProxyClient.Connected);
            form.senSLAndSR();
            Thread.Sleep(1000);
            voter.requestForSLandSR();
            Thread.Sleep(1000);
            voter.requestForCandidatesList();
            voter.ProxyClient.disconnect();
        }
    }
}
