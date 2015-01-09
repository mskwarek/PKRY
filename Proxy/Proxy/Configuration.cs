using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Proxy
{

    /// <summary>
    /// Configuration class - loads configuration from file and sets up proxy 
    /// </summary>
    class Configuration
    {
        private Logs logs;

        /// <summary>
        /// Proxy ID
        /// </summary>
        private string proxyID;
        public string ProxyID
        {
            get { return proxyID; }
        }

        /// <summary>
        /// Proxy port
        /// </summary>
        private string proxyPort;
        public string ProxyPort
        {
            get { return proxyPort; }
        }

        /// <summary>
        /// Election authority IP
        /// </summary>
        private string electionAuthorityIP;
        public string ElectionAuthorityIP
        {
            get { return electionAuthorityIP; }
        }

        /// <summary>
        /// EA port
        /// </summary>
        private string electionAuthorityPort;
        public string ElectionAuthorityPort
        {
            get { return electionAuthorityPort; }
        }


        /// <summary>
        /// number of voters for this proxy
        /// </summary>
        private int numOfVoters;
        public int NumOfVoters
        {
            get { return numOfVoters; }
        }

        /// <summary>
        /// number of candidates in election
        /// </summary>
        private int numOfCandidates;
        public int NumOfCandidates
        {
            get { return numOfCandidates; }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logs">log instance</param>
        public Configuration(Logs logs)
        {
            this.logs = logs;
        }

        /// <summary>
        /// reads config from xml configuration file
        /// </summary>
        /// <param name="xml">xml document</param>
        /// <returns> list of string with config</returns>
        private List<String> readConfig(XmlDocument xml)
        {

            List<String> list = new List<String>();

            foreach (XmlNode xnode in xml.SelectNodes("//Proxy[@ID]"))
            {
                string proxyId = xnode.Attributes[Constants.ID].Value;
                list.Add(proxyId);
                string proxyPort = xnode.Attributes[Constants.PROXY_PORT].Value;
                list.Add(proxyPort);
                string electionAuthorityIP = xnode.Attributes[Constants.ELECTION_AUTHORITY_IP].Value;
                list.Add(electionAuthorityIP);
                string electionAuthorityPort = xnode.Attributes[Constants.ELECTION_AUTHORITY_PORT].Value;
                list.Add(electionAuthorityPort);
                string numberOfVoters = xnode.Attributes[Constants.NUMBER_OF_VOTERS].Value;
                list.Add(numberOfVoters);
                string numberOfCandidates = xnode.Attributes[Constants.NUMBER_OF_CANDIDATES].Value;
                list.Add(numberOfCandidates);
            }

            return list;

        }

        /// <summary>
        /// load configuration from path given by user
        /// </summary>
        /// <param name="path">path to configuration</param>
        /// <returns>loading end status</returns>
        public bool loadConfiguration(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(path);
                List<String> conf = new List<String>();
                conf = readConfig(xml);

                this.proxyID = conf[0];
                this.proxyPort = conf[1];
                this.electionAuthorityIP = conf[2];
                this.electionAuthorityPort = conf[3];
                this.numOfVoters = Convert.ToInt32(conf[4]);
                this.numOfCandidates = Convert.ToInt32(conf[5]);

                string[] filePath = path.Split('\\');
                logs.addLog(Constants.CONFIGURATION_LOADED_FROM + filePath[filePath.Length - 1], true, Constants.LOG_INFO);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
                return false;
            }


        }
    }
}
