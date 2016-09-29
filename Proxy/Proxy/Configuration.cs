using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Proxy
{
    class Configuration
    {
        private Utils.Logs logs;
        private string proxyID;
        public string ProxyID
        {
            get { return proxyID; }
        }

        private string proxyPort;
        public string ProxyPort
        {
            get { return proxyPort; }
        }

        private string electionAuthorityIP;
        public string ElectionAuthorityIP
        {
            get { return electionAuthorityIP; }
        }

        private string electionAuthorityPort;
        public string ElectionAuthorityPort
        {
            get { return electionAuthorityPort; }
        }

        private int numOfVoters;
        public int NumOfVoters
        {
            get { return numOfVoters; }
        }

        private int numOfCandidates;
        public int NumOfCandidates
        {
            get { return numOfCandidates; }
        }

        public Configuration(Utils.Logs logs)
        {
            this.logs = logs;
        }

        private List<String> readConfig(XmlDocument xml)
        {

            List<String> list = new List<String>();

            foreach (XmlNode xnode in xml.SelectNodes("//Proxy[@ID]"))
            {
                string proxyId = xnode.Attributes[NetworkLib.Constants.ID].Value;
                list.Add(proxyId);
                string proxyPort = xnode.Attributes[NetworkLib.Constants.PROXY_PORT].Value;
                list.Add(proxyPort);
                string electionAuthorityIP = xnode.Attributes[NetworkLib.Constants.ELECTION_AUTHORITY_IP].Value;
                list.Add(electionAuthorityIP);
                string electionAuthorityPort = xnode.Attributes[NetworkLib.Constants.ELECTION_AUTHORITY_PORT].Value;
                list.Add(electionAuthorityPort);
                string numberOfVoters = xnode.Attributes[NetworkLib.Constants.NUMBER_OF_VOTERS].Value;
                list.Add(numberOfVoters);
                string numberOfCandidates = xnode.Attributes[NetworkLib.Constants.NUMBER_OF_CANDIDATES].Value;
                list.Add(numberOfCandidates);
            }

            return list;

        }

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
                logs.addLog(NetworkLib.Constants.CONFIGURATION_LOADED_FROM + filePath[filePath.Length - 1], true, NetworkLib.Constants.LOG_INFO);
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
