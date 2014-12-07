using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ElectionAuthority
{
    class Configuration
    {
        private Logs logs;

        private string electionAuthorityID;
        public string ElectionAuthorityID
        {
            get { return electionAuthorityID; }
        }

        private string electionAuthorityPort;
        public string ElectionAuthorityPort
        {
            get { return electionAuthorityPort; }
        }

        public Configuration(Logs logs)
        {
            this.logs = logs;
        }

        private List<String> readConfig(XmlDocument xml)
        {

            List<String> list = new List<String>();

            foreach (XmlNode xnode in xml.SelectNodes("//ElectionAuthority[@ID]"))
            {
                string voterId = xnode.Attributes[Constants.ID].Value;
                list.Add(voterId);
                string electionAuthorityPort = xnode.Attributes[Constants.ELECTION_AUTHORITY_PORT].Value;
                list.Add(electionAuthorityPort);
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
                this.electionAuthorityPort = conf[1];

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
