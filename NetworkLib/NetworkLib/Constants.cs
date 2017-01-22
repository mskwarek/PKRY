﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLib
{
    public class Constants
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
        public const string PROXY = "PROXY";
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
        public const string GET_SL_AND_SR = "GET_SL_AND_SR";
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
        public static string NEW_MSG_RECIVED;
        public const int BALLOT_SIZE = 4;     
        public const string NUMBER_OF_VOTERS = "numberOfVoters";
        public const string NUMBER_OF_CANDIDATES = "numberOfCandidates";
        public const string PATH_TO_CONFIG = @"Config\ElectionAuthority.xml";
        public const string SERVER_STARTED_CORRECTLY = "Proxy started working correctly";
        public const string SERVER_UNABLE_TO_START = "Proxy unable to start working";
        public const string UNKNOWN = "Unknown";
        public const string DISCONNECTED_NODE = "Someone has been disconnected";
        public const string SR_GEN_SUCCESSFULLY = "Serial numers SR generated successfully";
        public const int NUMBER_OF_BITS_SR = 64;
        public const int NUM_OF_CANDIDATES = 5;
        public const string SL_TOKENS = "SL_TOKENS"; //used to recognize message from EA
        public const string RECEIVED_FROM_EA = "Date received from EA";
        public const string SR_CONNECTED_WITH_SL = "SR connected with serial numbers SL";
        public const string ERROR_SEND_SL_AND_SR = "Unable to send SL and SR, because they are not ready";
        public const string SL_RECEIVED_SUCCESSFULLY = "SL_RECEIVED_SUCCESSFULLY";
        public const string VOTER_CONNECTED = "Voter connected successfully to Proxy";
        public const string PROXY_CONNECTED_TO_EA = "Proxy connected successfully to EA";
        public const string SL_RECEIVED = "Proxy received SL from EA";
        public const string YES_NO_POSITION_GEN_SUCCESSFULL = "Yes and No position generated successfully";
        public const string VOTE_RECEIVED = "Vote received from voter with ID: ";
        public const string BALLOT_MATRIX_GEN = "Ballot matrix generated for voter with ID: ";
        public const string BLIND_PROXY_BALLOT = "BLIND_PROXY_BALLOT";
        public const string SIGNED_PROXY_BALLOT = "SIGNED_PROXY_BALLOT";
        public const string SIGNED_COLUMNS_RECEIVED = "Signed columns received from EA and saved.";
        public const string WRONG_SIGNATURE = "Wrong signature!";
        public const string CORRECT_SIGNATURE = "Correct signature!";
        public const string ALL_COLUMNS_UNBLINDED_CORRECTLY = "Correct signature! All columns unblinded correctly!";
        public const string UNBLINED_BALLOT_MATRIX = "UNBLINED_BALLOT_MATRIX";
        public const string YES_NO_POSITION_SAVED_TO_FILE = @"YesNoPosition save to file Logs\yesNoPosition.txt";
        public const string ELECTION_AUTHORITY_PORT_CLIENT = "electionAuthorityPortForClient";
        public const string ELECTION_AUTHORITY_PORT_PROXY = "electionAuthorityPortForProxy";
        public const string CANDIDATE_LIST = "CandidateList.xml";
        public const string CANDIDATE_LIST_SUCCESSFUL = "Candidate list loaded successfully";
        public const string PERMUTATION_GEN_SUCCESSFULLY = "Permuration generated successfully";
        public const string SERIAL_NUMBER_GEN_SUCCESSFULLY = "Serial number list generated successfully";
        public const string SL_CONNECTED_WITH_PERMUTATION = "Serial numbers connected with permutation";
        public const int NUMBER_OF_BITS_SL = 64;
        public const int NUMBER_OF_TOKENS = 4;
        public const string TOKENS_GENERATED_SUCCESSFULLY = "Tokens generated successfully";
        public const int NUMBER_OF_BITS_TOKEN = 512;
        public const string SL_CONNECTED_WITH_TOKENS = "Serial numbers connected with tokens";
        public const string SL_AND_SR_SENT_SUCCESSFULLY = "SL sent successfully to Proxy";
        public const string BLIND_PROXY_BALLOT_RECEIVED = "Blind ballot received from voter with ID: ";
        public const string SIGNED_BALLOT_MATRIX_SENT = "SIGNED_BALLOT_MATRIX_SENT";
        public const string GENERATE_INVERSE_PERMUTATION = "Inverse permutation generated";
        public const string SL_CONNECTED_WITH_INVERSE_PERMUTATION = "Serial numbers connected with inverse permutation";
        public const string UNBLINED_BALLOT_MATRIX_RECEIVED = "Unblined ballot matrix received from Proxy.";
        public const string BIT_COMMITMENT_OK = "Checking bit commitment correct";
        public const string BIT_COMMITMENT_FAIL = "Checking bit commitment incorrect";
        public const string UNABLE_TO_STOP_VOTING = "UNABLE_TO_STOP_VOTING";
        public const string VOTIGN_STOPPED = "Votign stopped successfully";
    }
}
