using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voter
{
    class Constants
    {

        public const int LOG_INFO = 0;
        public const int LOG_MESSAGE = 1;
        public const int LOG_ERROR = 2;

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
        public const string CONFIGURATION_LOADED_FROM = "Configuration loaded from file: ";
    }
}
