using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Voter
{
    /// <summary>
    /// loading config from file
    /// </summary>
    class Configuration
    {
        /// <summary>
        /// allows to collect and display logs
        /// </summary>
        private Logs logs;

        /// <summary>
        /// Voter ID - unique name of Voter
        /// </summary>
        private string voterID;
        public string VoterID
        {
            get { return voterID; }
        }

        /// <summary>
        /// IP of Election Authority application - it's loaded from configuration xml file
        /// </summary>
        private string electionAuthorityIP;
        public string ElectionAuthorityIP
        {
            get { return electionAuthorityIP; }
        }
        /// <summary>
        /// port on which Election Authority application is running - it's loaded from configuration xml file
        /// </summary>
        private string electionAuthorityPort;
        public string ElectionAuthorityPort
        {
            get { return electionAuthorityPort; }
        }

        /// <summary>
        /// IP of Proxy application - it's loaded from configuration xml file
        /// </summary>
        private string proxyIP;
        public string ProxyIP
        {
            get { return proxyIP; }
        }
        /// <summary>
        /// port on which Proxy application is running - it's loaded from configuration xml file
        /// </summary>
        private string proxyPort;
        public string ProxyPort
        {
            get { return proxyPort; }
        }
        /// <summary>
        /// number of candidates which are on the voting list
        /// </summary>
        private int numberOfCandidates;

        public int NumberOfCandidates
        {
            get { return numberOfCandidates; }
        }
        /// <summary>
        /// name of voter - it's unique name of voter loaded from confiuration xml file
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// use to load configuration from xml file
        /// </summary>
        /// <param name="logs">display messages in logs</param>
        public Configuration(Logs logs)
        {
            this.logs = logs;
        }
        /// <summary>
        /// reads configuration from xml file 
        /// </summary>
        /// <param name="xml">file choosen by user to load configuration</param>
        /// <returns></returns>
        private List<String> readConfig(XmlDocument xml)
        {
            
            List<String> list = new List<String>();
            
            foreach (XmlNode xnode in xml.SelectNodes("//Voter[@ID]"))
            {
                string voterId = xnode.Attributes[Constants.ID].Value;
                list.Add(voterId);
                string electionAuthorityIP = xnode.Attributes[Constants.ELECTION_AUTHORITY_IP].Value;
                list.Add(electionAuthorityIP);
                string electionAuthorityPort = xnode.Attributes[Constants.ELECTION_AUTHORITY_PORT].Value;
                list.Add(electionAuthorityPort);
                string proxyIP = xnode.Attributes[Constants.PROXY_IP].Value;
                list.Add(proxyIP);
                string proxyPort = xnode.Attributes[Constants.PROXY_PORT].Value;
                list.Add(proxyPort);
                string numberOfVoters = xnode.Attributes[Constants.NUMBEROFVOTERS].Value;
                list.Add(numberOfVoters);
                string name = xnode.Attributes[Constants.NAME].Value;
                list.Add(name);
            }

            return list;

        }

        /// <summary>
        /// save loaded configuration in parameters
        /// </summary>
        /// <param name="path">path to file with configuration</param>
        /// <returns>true if configuration is loaded successfully</returns>
        public bool loadConfiguration(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(path);
                List<String> voterConf = new List<String>();
                voterConf = readConfig(xml);

                this.voterID = voterConf[0];
                this.electionAuthorityIP = voterConf[1];
                this.electionAuthorityPort = voterConf[2];
                this.proxyIP = voterConf[3];
                this.proxyPort = voterConf[4];
                this.numberOfCandidates = Convert.ToInt32(voterConf[5]);
                this.name = voterConf[6];


                string[] filePath = path.Split('\\');
                logs.addLog(Constants.CONFIGURATION_LOADED_FROM + filePath[filePath.Length - 1], true, Constants.LOG_INFO, true);
                return true;
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp);
                return false;
            }
            
            
        }

    }
}
