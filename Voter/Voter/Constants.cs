using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voter
{
    /// <summary>
    /// constants used in project
    /// </summary>
    class Constants
    {

        public const int LOG_INFO = 0;
        public const int LOG_MESSAGE = 1;
        public const int LOG_ERROR = 2;

        public const int BALLOTSIZE = 4;

        public const string LOCALHOST = "localhost";
        public const string CONNECTION_PASS = "Voter connected successfully to ";
        public const string CONNECTION_FAILED = "Voter could not connect to ";
        public const string CONNECTION_DISCONNECTED = "Voter disconnected from Election Authority";
        public const string CONNECTION_DISCONNECTED_ERROR = "Error occured during disconnecting Voter from Election Authority";

        public const string ELECTION_AUTHORITY = "Election Authority";
        public const string PROXY = "Proxy";
        public const string ID = "ID";
        public const string ELECTION_AUTHORITY_IP = "electionAuthorityIP";
        public const string ELECTION_AUTHORITY_PORT = "electionAuthorityPort";
        public const string PROXY_IP = "proxyIP";
        public const string PROXY_PORT = "proxyPort";
        public const string NAME = "name";
        public const string CONFIGURATION_LOADED_FROM = "Configuration loaded from file: ";
        public const string NUMBEROFVOTERS = "numberOfVoters";
        public const string VOTE_DONE = "Vote accepted.";
        public const string VOTE_ERROR = "Vote error, you've already voted to this candidate";
        
        
        public const string GET_SL_AND_SR="GET_SL_AND_SR";
        public const string SL_AND_SR = "SL_AND_SR";
        public const string SR_AND_SR_RECEIVED = "SL and SR received correctly from Proxy";
        public const string GET_CANDIDATE_LIST = "GET_CANDIDATE_LIST";
        public const string CONNECTION_SUCCESSFUL = "CONNECTION_SUCCESSFUL";
        public const string CANDIDATE_LIST_RESPONSE = "CANDIDATE_LIST_RESPONSE";
        public const string CONNECTED = "CONNECTED";
        public const string GET_YES_NO_POSITION = "GET_YES_NO_POSITION";
        public const string YES_NO_POSITION = "YES_NO_POSITION";
        public const string VOTE_FINISH = "Process of voting done. Congratulations!";
        public const string VOTE = "VOTE";
        public const string SIGNED_COLUMNS_TOKEN = "SIGNED_COLUMNS_TOKEN";
        public const string SIGNED_COLUMNS_TOKEN_RECEIVED = "Signed blind columns received from Proxy.";
    }
}
