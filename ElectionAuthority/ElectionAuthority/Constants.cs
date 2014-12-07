using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectionAuthority
{
    class Constants
    {
        public const int LOG_INFO = 0;
        public const int LOG_MESSAGE = 1;
        public const int LOG_ERROR = 2;

        public const string ID = "ID";
        public const string ELECTION_AUTHORITY_PORT = "electionAuthorityPort";
        public const string CONFIGURATION_LOADED_FROM = "Configuration loaded from file: ";

        public const string PATH_TO_CONFIG = @"Config\ElectionAuthority.xml";

        public const string SERVER_STARTED_CORRECTLY = "Election Authority started working correctly";
        public const string SERVER_UNABLE_TO_START = "Election Authority unable to start working";
        public const string UNKNOWN = "Unknown";
        public const string DISCONNECTED_NODE = "Someone has been disconnected";
    }
}
