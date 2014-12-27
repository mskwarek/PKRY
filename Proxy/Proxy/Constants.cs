using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proxy
{
    class Constants
    {
        public const int LOG_INFO = 0;
        public const int LOG_MESSAGE = 1;
        public const int LOG_ERROR = 2;

        public const string ID = "ID";
        public const string PROXY_PORT = "proxyPort";
        public const string ELECTION_AUTHORITY_IP = "electionAuthorityIP";
        public const string ELECTION_AUTHORITY_PORT = "electionAuthorityPort";
        public const string NUMBER_OF_VOTERS = "numberOfVoters";
        public const string NUMBER_OF_CANDIDATES = "numberOfCandidates";
        public const string CONFIGURATION_LOADED_FROM = "Configuration loaded from file: ";
         

        public const string LOCALHOST = "localhost";
        public const string CONNECTION_PASS = "Proxy connected with Election Authority correctly";
        public const string CONNECTION_FAILED = "Proxy could not connect to Election Authority";
        public const string CONNECTION_DISCONNECTED = "Proxy disconnected from Election Authority";
        public const string CONNECTION_DISCONNECTED_ERROR = "Error occured during disconnecting Proxy from Election Authority";

        public const string PATH_TO_CONFIG = @"Config\ElectionAuthority.xml";

        public const string SERVER_STARTED_CORRECTLY = "Proxy started working correctly";
        public const string SERVER_UNABLE_TO_START = "Proxy unable to start working";
        public const string UNKNOWN = "Unknown";
        public const string DISCONNECTED_NODE = "Someone has been disconnected";

        public const string SR_GEN_SUCCESSFULLY = "Serial numers SR generated successfully";


        public const int NUMBER_OF_BITS_SR = 64;
        public const int NUM_OF_CANDIDATES = 5;
        


        public const string SL_TOKENS = "SL_TOKENS"; //used to recognize message from EA
        public const string RECEIVED_FROM_EA ="Date received from EA";
        public const string SR_CONNECTED_WITH_SL = "SR connected with serial numbers SL";

        public const string GET_SL_AND_SR = "GET_SL_AND_SR";
        public const string SL_AND_SR = "SL_AND_SR";
        public const string ERROR_SEND_SL_AND_SR="Unable to send SL and SR, because they are not ready";
        public const string SL_RECEIVED_SUCCESSFULLY = "SL_RECEIVED_SUCCESSFULLY";
        public const string CONNECTION_SUCCESSFUL = "CONNECTION_SUCCESSFUL";
        public const string VOTER_CONNECTED = "Voter connected successfully to Proxy";
        public const string CONNECTED = "CONNECTED";
        public const string PROXY_CONNECTED_TO_EA= "Proxy connected successfully to EA";
        public const string SL_RECEIVED = "Proxy received SL from EA";
        public const int BALLOT_SIZE = 4;
        public const string GET_YES_NO_POSITION = "GET_YES_NO_POSITION";
        public const string YES_NO_POSITION_GEN_SUCCESSFULL= "Yes and No position generated successfully";
        public const string YES_NO_POSITION = "YES_NO_POSITION";
    }
}
