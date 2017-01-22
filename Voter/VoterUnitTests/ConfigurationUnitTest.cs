using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voter;
using System.IO;
using System.Reflection;

namespace VoterUnitTests
{
    public static class ConfigurationUtils
    {
        private static string getFileFromRelatedPath(string relatedPathToFile)
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var iconPath = Path.Combine(outPutDirectory, relatedPathToFile);
            return new Uri(iconPath).LocalPath;
        }
        public static string getProperConfigPath()
        {
            return getFileFromRelatedPath("..\\..\\..\\..\\Config\\VoterTest.xml");
        }

        public static string getProperEAConfigPath()
        {
            return getFileFromRelatedPath("..\\..\\..\\..\\Config\\ElectionAuthority.xml");
        }

        public static string getNotProperConfigPath()
        {
            return getFileFromRelatedPath("..\\..\\..\\Config\\VoterTest.xml");
        }

        public static string getProperEACandidateList()
        {
            return getFileFromRelatedPath("..\\..\\..\\..\\Config\\CandidateList.xml");
        }

        public static string getProperProxyConfigPath()
        {
            return getFileFromRelatedPath("..\\..\\..\\..\\Config\\Proxy.xml");
        }
    }
    public class ConfigurationBase
    {
        protected Configuration configuration;
        public ConfigurationBase()
        {
            configuration = new Configuration();
        }
    }

    [TestClass]
    public class ConfigurationUnitTest : ConfigurationBase
    {
        [TestMethod]
        public void NonproperConfigurationTest()
        {
            Assert.IsFalse(configuration.loadConfiguration(ConfigurationUtils.getNotProperConfigPath()));
        }

        [TestMethod]
        public void ProperConfigurationTest()
        {
            string dupa = (ConfigurationUtils.getProperConfigPath());
            Assert.IsTrue(configuration.loadConfiguration(ConfigurationUtils.getProperConfigPath()));
        }

        [TestMethod]
        public void VoterIDTest()
        {
            Assert.IsTrue(configuration.loadConfiguration(ConfigurationUtils.getProperConfigPath()));
            Assert.AreEqual(configuration.VoterID, "Voter");
        }

        [TestMethod]
        public void VoterNameTest()
        {
            Assert.IsTrue(configuration.loadConfiguration(ConfigurationUtils.getProperConfigPath()));
            Assert.AreEqual(configuration.Name, "Voter0");
        }

        [TestMethod]
        public void VoterNumberOfCandidatesTest()
        {
            Assert.IsTrue(configuration.loadConfiguration(ConfigurationUtils.getProperConfigPath()));
            Assert.AreEqual(configuration.NumberOfCandidates, 5);
        }

        [TestMethod]
        public void VoterProxyIPTest()
        {
            Assert.IsTrue(configuration.loadConfiguration(ConfigurationUtils.getProperConfigPath()));
            Assert.AreEqual(configuration.ProxyIP, "localhost");
        }

        [TestMethod]
        public void VoterProxyPortTest()
        {
            Assert.IsTrue(configuration.loadConfiguration(ConfigurationUtils.getProperConfigPath()));
            Assert.AreEqual(configuration.ProxyPort, "16000");
        }

        [TestMethod]
        public void VoterEAIPTest()
        {
            Assert.IsTrue(configuration.loadConfiguration(ConfigurationUtils.getProperConfigPath()));
            Assert.AreEqual(configuration.ElectionAuthorityIP, "localhost");
        }

        [TestMethod]
        public void VoterEAPortTest()
        {
            Assert.IsTrue(configuration.loadConfiguration(ConfigurationUtils.getProperConfigPath()));
            Assert.AreEqual(configuration.ElectionAuthorityPort, "15000");
        }
    }
}
