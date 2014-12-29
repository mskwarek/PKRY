using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectionAuthority
{
    class Constants
    {
        //===========ZMIENIC = WCZYTYWAC Z PLIKU!================
        public const int BALLOT_SIZE = 4;

        public const int LOG_INFO = 0;
        public const int LOG_MESSAGE = 1;
        public const int LOG_ERROR = 2;

        public const string ID = "ID";
        public const string ELECTION_AUTHORITY_PORT_CLIENT = "electionAuthorityPortForClient";
        public const string ELECTION_AUTHORITY_PORT_PROXY = "electionAuthorityPortForProxy";
        public const string CONFIGURATION_LOADED_FROM = "Configuration loaded from file: ";
        public const string NUMBER_OF_VOTERS = "numberOfVoters";

        public const string PATH_TO_CONFIG = @"Config\ElectionAuthority.xml";

        public const string CANDIDATE_LIST = "CandidateList.xml";
        public const string SERVER_STARTED_CORRECTLY = "Election Authority started working correctly";
        public const string SERVER_UNABLE_TO_START = "Election Authority unable to start working";
        public const string UNKNOWN = "Unknown";
        public const string DISCONNECTED_NODE = "Someone has been disconnected";


        public const string CANDIDATE_LIST_SUCCESSFUL = "Candidate list loaded successfully";
        public const string PERMUTATION_GEN_SUCCESSFULLY = "Permuration generated successfully";
        public const string SERIAL_NUMBER_GEN_SUCCESSFULLY = "Serial number list generated successfully";
        public const string SL_CONNECTED_WITH_PERMUTATION = "Serial numbers connected with permutation";

        public const int NUMBER_OF_BITS_SL = 64;
        public const int NUMBER_OF_TOKENS = 4;
        public const string TOKENS_GENERATED_SUCCESSFULLY = "Tokens generated successfully";
        public const int NUMBER_OF_BITS_TOKEN =32;
        public const string SL_CONNECTED_WITH_TOKENS = "Serial numbers connected with tokens";
        public static string SL_TOKENS = "SL_TOKENS";
        public const string SL_RECEIVED_SUCCESSFULLY = "SL_RECEIVED_SUCCESSFULLY";
        public const string SL_AND_SR_SENT_SUCCESSFULLY = "SL sent successfully to Proxy";
        public const string PROXY ="PROXY";
        public static string CONNECTED = "CONNECTED";

        public const string GET_CANDIDATE_LIST = "GET_CANDIDATE_LIST";
        public const string CANDIDATE_LIST_RESPONSE = "CANDIDATE_LIST_RESPONSE";
        public const string BLIND_PROXY_BALLOT = "BLIND_PROXY_BALLOT";
        public const string BLIND_PROXY_BALLOT_RECEIVED = "Blind ballot received from voter with ID: ";
        public const string SIGNED_PROXY_BALLOT = "SIGNED_PROXY_BALLOT";
        public const string SIGNED_BALLOT_MATRIX_SENT = "SIGNED_BALLOT_MATRIX_SENT";
        public const string GENERATE_INVERSE_PERMUTATION = "Inverse permutation generated";
        public const string SL_CONNECTED_WITH_INVERSE_PERMUTATION = "Serial numbers connected with inverse permutation";
    }
}
