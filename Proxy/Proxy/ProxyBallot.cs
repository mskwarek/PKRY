using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Crypto.Digests;

namespace Proxy
{
    /// <summary>
    /// proxy ballot - one ballot (represents one voter) with all serialns, keys and data used in election
    /// </summary>
    class ProxyBallot
    {
        /// <summary>
        /// logs 
        /// </summary>
        private Logs logs;                                              

        /// <summary>
        /// pub key to blind sign
        /// </summary>
        private RsaKeyParameters pubKey;                                
    
        /// <summary>
        /// priv Key to blind signature
        /// </summary>
        private RsaKeyParameters privKey;                             

        /// <summary>
        /// random blinding factor
        /// </summary>
        private BigInteger[] r;    
        
        /// <summary>
        /// SL connected to proxy ballot
        /// </summary>
        private BigInteger sl;
        public BigInteger SL
        {
            get { return sl; }
        }
        /// <summary>
        /// SR connected to proxy ballot
        /// </summary>
        private BigInteger sr;

        /// <summary>
        /// position of "yes" answer
        /// </summary>
        private string yesNoPos;                                          
        public string YesNoPos
        {
            set { yesNoPos = value; }
        }
        
        /// <summary>
        /// vote from voter
        /// </summary>
        private int[,] vote;                                            
        public int[,] Vote
        {
            set { vote = value; }
        }
        

        /// <summary>
        /// ballot matrix (select every "no" which was not clicked by voter)
        /// </summary>
        private int[,] ballotMatrix;    
        
        /// <summary>
        /// columns as string
        /// </summary>                
        private List<string> columns;

        /// <summary>
        /// signed columns recived from EA
        /// </summary>
        private List<BigInteger> signedColumns;
        public List<BigInteger> SignedColumns
        {
            set { signedColumns = value; }
            get { return signedColumns; }

        }

        /// <summary>
        /// Confirmation which was chosen by voter
        /// </summary>
        private int confirmationColumn;
        public int ConfirmationColumn
        {
            get { return confirmationColumn; }
            set { confirmationColumn = value; }
        }
        
        /// <summary>
        /// List of tokens connected with SL
        /// </summary>
        private List<BigInteger> tokensList;
        public List<BigInteger> TokensList
        {
            set { tokensList = value; }
            get { return tokensList; }
        }

        /// <summary>
        /// exponent list (parameters) connected to the SL
        /// </summary>
        private List<BigInteger> exponentsList;
        public List<BigInteger> ExponentsList
        {
            set { exponentsList = value; }
            get { return exponentsList; }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logs">log instance</param>
        /// <param name="SL">SL number which will connected to the proxy ballot</param>
        /// <param name="SR">SR number which will connected to the proxy ballot</param>
        public ProxyBallot(Logs logs, BigInteger SL, BigInteger SR)
        {
            this.sl =  SL;
            this.sr = SR;
            this.logs = logs;
            this.tokensList = new List<BigInteger>();
            this.exponentsList = new List<BigInteger>();
            
            //sng = sng.getInstance();
            //this.SR = sng.getNextSr();
            //init keyPair generator
            KeyGenerationParameters para = new KeyGenerationParameters(new SecureRandom(), 1024);
            RsaKeyPairGenerator keyGen = new RsaKeyPairGenerator();
            keyGen.Init(para);
            r = null;
            
            //generate key pair and get keys
            AsymmetricCipherKeyPair keypair = keyGen.GenerateKeyPair();
            privKey = (RsaKeyParameters)keypair.Private;
            pubKey = (RsaKeyParameters)keypair.Public;

        }

        
        /// <summary>
        /// prepares data to send
        /// </summary>
        /// <returns>blinded columns </returns>
        public BigInteger[] prepareDataToSend()
        {
            BigInteger[] toSend = new BigInteger[Constants.BALLOT_SIZE];
            r = new BigInteger[Constants.BALLOT_SIZE];
            //blinding columns, prepare to signature

            int i=0;
            foreach (string column in columns)
            {
                BigInteger toBlind = new BigInteger(column);
                BigInteger e = pubKey.Exponent;
                BigInteger d = privKey.Exponent;

                SecureRandom random = new SecureRandom();
                byte[] randomBytes = new byte[10];
                
                //BigInteger n = pubKey.Modulus;
                BigInteger n = tokensList[i];
                BigInteger gcd = null;
                BigInteger one = new BigInteger("1");

                //check that gcd(r,n) = 1 && r < n && r > 1
                do
                {
                    random.NextBytes(randomBytes);
                    r[i] = new BigInteger(1, randomBytes);
                    gcd = r[i].Gcd(n);
                    Console.WriteLine("gcd: " + gcd);
                }
                while (!gcd.Equals(one) || r[i].CompareTo(n) >= 0 || r[i].CompareTo(one) <= 0);

                //********************* BLIND ************************************
                BigInteger b = ((r[i].ModPow(e, n)).Multiply(toBlind)).Mod(n);
                toSend[i] = b;

                i++;
            }
            return toSend;
        }

        /// <summary>
        /// unblind signed data (for one ballot)
        /// </summary>
        /// <param name="signedData">signed data recived from EA</param>
        /// <returns>unblinded data (columns)</returns>
        public string[] unblindSignedData(BigInteger[] signedData)
        {
            string[] unblinded = new string[Constants.BALLOT_SIZE];

            for (int i = 0; i < signedData.Length; i++)
            {
                BigInteger explicitData = new BigInteger(columns[i]);
                BigInteger n = tokensList[i];
                BigInteger e = exponentsList[i];

                BigInteger signed = ((r[i].ModInverse(n)).Multiply(signedData[i])).Mod(n);
                
                
                BigInteger check = signed.ModPow(e, n);
                int correctUnblindedColumns = 0; //used to now if all columns are unblinded correctly
                if(explicitData.Equals(check))
                {
                    correctUnblindedColumns += 1;
                    String str = check.ToString();
                    String correctString =  checkZeros(str);
                    unblinded[i] = correctString;
                    Console.WriteLine("Odslepiona co marcinek zapomniał: " + unblinded[i]);

                    //WYSŁAć NORMALNA KOLUMNE, BO WIEMY ZE NIE OSZUKA
                    if (correctUnblindedColumns == Constants.BALLOT_SIZE)
                        this.logs.addLog(Constants.ALL_COLUMNS_UNBLINDED_CORRECTLY, true, Constants.LOG_INFO, true);
                    else
                        this.logs.addLog(Constants.CORRECT_SIGNATURE, true, Constants.LOG_INFO, true);

                }
                else{
                    this.logs.addLog(Constants.WRONG_SIGNATURE, true, Constants.LOG_ERROR, true);
                }
            }
            return unblinded;
        }

        /// <summary>
        /// complete vote with 0 at the begin
        /// </summary>
        /// <param name="str">message</param>
        /// <returns>correct message</returns>
        private string checkZeros(string str)
        {
            if (str.Length == this.vote.GetLength(0))
                return str;
            else
            {
                int neccessaryZeros = this.vote.GetLength(0) - str.Length;
                string zeros = null;
                for (int i = 0; i < neccessaryZeros; i++)
                {
                    zeros += "0";
                }
                string column = zeros + str;

                return column;
            }       
        }


        /// <summary>
        /// generate ballot martix and split it (in column order)
        /// </summary>
        public void generateAndSplitBallotMatrix()
        {
            
            string[] position = this.yesNoPos.Split(':');
            this.ballotMatrix = new int[this.vote.GetLength(0), this.vote.GetLength(1)];
            this.columns = new List<string>();
            for (int i = 0; i < Constants.NUM_OF_CANDIDATES; i++)
            {
                for (int j = 0; j < Constants.BALLOT_SIZE; j++)
                {
                    //mark every non-clicked "No" button
                    if (vote[i, j] != 1 && j != Convert.ToInt32(position[i]))
                    {
                        this.ballotMatrix[i, j] = 1;
                    }
                }
            }

            //rewrite colums from ballot matrix to another array
            for (int j = 0; j < Constants.BALLOT_SIZE; j++)
            {
                string temp = null;
                for (int i = 0; i < Constants.NUM_OF_CANDIDATES; i++)
                {
                    temp += ballotMatrix[i, j];
                }
                columns.Add(temp);
            }
        }
    }
}
