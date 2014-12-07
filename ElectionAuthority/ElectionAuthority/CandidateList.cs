using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ElectionAuthority
{
    class CandidateList
    {
        private Logs logs;
        public CandidateList(Logs logs)
        {
            this.logs = logs;
        }

        public List<string> loadCanidateList(string path)
        {
            List<string> candidate = new List<string>();
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(path);
                string nodeName = "//Candidates/Candidate";
                
                foreach (XmlNode xnode in xml.SelectNodes(nodeName))
                {
                    string input = xnode.Attributes[Constants.ID].Value;
                    candidate.Add(input);
                }

                logs.addLog(Constants.CANDIDATE_LIST_SUCCESSFUL, true, Constants.LOG_INFO, true);
                
            }
            catch (Exception)
            {

            }
            return candidate;
        }


        public string getPathToCandidateList(string path)
        {
            string[] split = path.Split('\\');
            split[split.Length-1] =Constants.CANDIDATE_LIST;
            string pathToCandidateList = "";
            for (int i = 0; i < split.Length; i++)
            {
                if (i == split.Length -1)
                    pathToCandidateList += split[i];
                else
                    pathToCandidateList = pathToCandidateList + split[i] + "\\";
            }
            
            
            return pathToCandidateList;

        }
    }
}
