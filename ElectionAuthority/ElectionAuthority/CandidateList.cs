using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ElectionAuthority
{
    /// <summary>
    /// class loads candidate list from txt file
    /// </summary>
    class CandidateList
    {

        /// <summary>
        /// allows to collect and display logs
        /// </summary>
        private Logs logs;

        /// <summary>
        /// condidate list constructor
        /// </summary>
        /// <param name="logs">logs instance</param>
        public CandidateList(Logs logs)
        {
            this.logs = logs;
        }

        /// <summary>
        /// loading cadidate list
        /// </summary>
        /// <param name="path">path to txt file</param>
        /// <returns>List of strings with candidates</returns>
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
                Console.WriteLine("Wyjatek w loadCandidateList");
            }
            return candidate;
        }

        /// <summary>
        /// gets path to txt file with candidates list
        /// </summary>
        /// <param name="path">path to txt file</param>
        /// <returns>path to file</returns>
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
