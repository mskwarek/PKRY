using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ElectionAuthority
{
    class Configuration
    {

        private string electionAuthorityID;
        public string ElectionAuthorityID
        {
            get { return electionAuthorityID; }
        }

        private string electionAuthorityPortClient;
        public string ElectionAuthorityPortClient
        {
            get { return electionAuthorityPortClient; }
        }

        private string electionAuthorityPortProxy;
        public string ElectionAuthorityPortProxy
        {
            get { return electionAuthorityPortProxy; }
        }

        private string numberOfVoters;
        public string NumberOfVoters
        {
            get { return numberOfVoters; }
        }


        public Configuration()
        {
        }

        private List<String> readConfig(XmlDocument xml)
        {

            List<String> list = new List<String>();

            foreach (XmlNode xnode in xml.SelectNodes("//ElectionAuthority[@ID]"))
            {
                string voterId = xnode.Attributes[NetworkLib.Constants.ID].Value;
                list.Add(voterId);
                string electionAuthorityPortClient = xnode.Attributes[NetworkLib.Constants.ELECTION_AUTHORITY_PORT_CLIENT].Value;
                list.Add(electionAuthorityPortClient);
                string electionAuthorityPortProxy = xnode.Attributes[NetworkLib.Constants.ELECTION_AUTHORITY_PORT_PROXY].Value;
                list.Add(electionAuthorityPortProxy);
                string numberOfVoters = xnode.Attributes[NetworkLib.Constants.NUMBER_OF_VOTERS].Value;
                list.Add(numberOfVoters);
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

                this.electionAuthorityID = conf[0];
                this.electionAuthorityPortClient = conf[1];
                this.electionAuthorityPortProxy = conf[2];
                this.numberOfVoters = conf[3];

                string[] filePath = path.Split('\\');
                Utils.Logs.addLog("EA", NetworkLib.Constants.CONFIGURATION_LOADED_FROM + filePath[filePath.Length - 1], true, NetworkLib.Constants.LOG_INFO);
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
