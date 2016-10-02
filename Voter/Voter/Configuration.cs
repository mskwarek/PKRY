using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Voter
{
  
    public class Configuration
    {
        private string voterID;
        public string VoterID
        {
            get { return voterID; }
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

        private string proxyIP;
        public string ProxyIP
        {
            get { return proxyIP; }
        }

        private string proxyPort;
        public string ProxyPort
        {
            get { return proxyPort; }
        }

        private int numberOfCandidates;

        public int NumberOfCandidates
        {
            get { return numberOfCandidates; }
        }

        private string name;
        public string Name
        {
            get { return name; }
        }

        public Configuration()
        {
        }

        private List<String> readConfig(XmlDocument xml)
        {
            
            List<String> list = new List<String>();
            
            foreach (XmlNode xnode in xml.SelectNodes("//Voter[@ID]"))
            {
                string voterId = xnode.Attributes[NetworkLib.Constants.ID].Value;
                list.Add(voterId);
                string electionAuthorityIP = xnode.Attributes[NetworkLib.Constants.ELECTION_AUTHORITY_IP].Value;
                list.Add(electionAuthorityIP);
                string electionAuthorityPort = xnode.Attributes[NetworkLib.Constants.ELECTION_AUTHORITY_PORT].Value;
                list.Add(electionAuthorityPort);
                string proxyIP = xnode.Attributes[NetworkLib.Constants.PROXY_IP].Value;
                list.Add(proxyIP);
                string proxyPort = xnode.Attributes[NetworkLib.Constants.PROXY_PORT].Value;
                list.Add(proxyPort);
                string numberOfVoters = xnode.Attributes[NetworkLib.Constants.NUMBEROFVOTERS].Value;
                list.Add(numberOfVoters);
                string name = xnode.Attributes[NetworkLib.Constants.NAME].Value;
                list.Add(name);
            }

            return list;

        }

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
                Utils.Logs.addLog("Client", NetworkLib.Constants.CONFIGURATION_LOADED_FROM + filePath[filePath.Length - 1], true, NetworkLib.Constants.LOG_INFO, true);
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
