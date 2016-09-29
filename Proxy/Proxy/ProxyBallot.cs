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
    class ProxyBallot
    {
        private Utils.Logs logs;                                              
        private RsaKeyParameters pubKey;                                
        private RsaKeyParameters privKey;                             
        private BigInteger[] r;    
        
        private BigInteger sl;
        public BigInteger SL
        {
            get { return sl; }
        }
        private BigInteger sr;
        private string yesNoPos;                                          
        public string YesNoPos
        {
            set { yesNoPos = value; }
        }

        private int[,] vote;                                            
        public int[,] Vote
        {
            set { vote = value; }
        }

        private int[,] ballotMatrix;    
            
        private List<string> columns;

        private List<BigInteger> signedColumns;
        public List<BigInteger> SignedColumns
        {
            set { signedColumns = value; }
            get { return signedColumns; }

        }

        private int confirmationColumn;
        public int ConfirmationColumn
        {
            get { return confirmationColumn; }
            set { confirmationColumn = value; }
        }

        private List<BigInteger> tokensList;
        public List<BigInteger> TokensList
        {
            set { tokensList = value; }
            get { return tokensList; }
        }

        private List<BigInteger> exponentsList;
        public List<BigInteger> ExponentsList
        {
            set { exponentsList = value; }
            get { return exponentsList; }
        }

        public ProxyBallot(Utils.Logs logs, BigInteger SL, BigInteger SR)
        {
            this.sl =  SL;
            this.sr = SR;
            this.logs = logs;
            this.tokensList = new List<BigInteger>();
            this.exponentsList = new List<BigInteger>();

            KeyGenerationParameters para = new KeyGenerationParameters(new SecureRandom(), 1024);
            RsaKeyPairGenerator keyGen = new RsaKeyPairGenerator();
            keyGen.Init(para);
            r = null;
            
            //generate key pair and get keys
            AsymmetricCipherKeyPair keypair = keyGen.GenerateKeyPair();
            privKey = (RsaKeyParameters)keypair.Private;
            pubKey = (RsaKeyParameters)keypair.Public;

        }

        public BigInteger[] prepareDataToSend()
        {
            BigInteger[] toSend = new BigInteger[NetworkLib.Constants.BALLOT_SIZE];
            r = new BigInteger[NetworkLib.Constants.BALLOT_SIZE];
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

        public string[] unblindSignedData(BigInteger[] signedData)
        {
            string[] unblinded = new string[NetworkLib.Constants.BALLOT_SIZE];

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

                    //WYSŁAć NORMALNA KOLUMNE, BO WIEMY ZE NIE OSZUKA
                    if (correctUnblindedColumns == NetworkLib.Constants.BALLOT_SIZE)
                        this.logs.addLog(NetworkLib.Constants.ALL_COLUMNS_UNBLINDED_CORRECTLY, true, NetworkLib.Constants.LOG_INFO, true);
                    else
                        this.logs.addLog(NetworkLib.Constants.CORRECT_SIGNATURE, true, NetworkLib.Constants.LOG_INFO, true);

                }
                else{
                    this.logs.addLog(NetworkLib.Constants.WRONG_SIGNATURE, true, NetworkLib.Constants.LOG_ERROR, true);
                }
            }
            return unblinded;
        }

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

        public void generateAndSplitBallotMatrix()
        {
            
            string[] position = this.yesNoPos.Split(':');
            this.ballotMatrix = new int[this.vote.GetLength(0), this.vote.GetLength(1)];
            this.columns = new List<string>();
            for (int i = 0; i < NetworkLib.Constants.NUM_OF_CANDIDATES; i++)
            {
                for (int j = 0; j < NetworkLib.Constants.BALLOT_SIZE; j++)
                {
                    //mark every non-clicked "No" button
                    if (vote[i, j] != 1 && j != Convert.ToInt32(position[i]))
                    {
                        this.ballotMatrix[i, j] = 1;
                    }
                }
            }

            //rewrite colums from ballot matrix to another array
            for (int j = 0; j < NetworkLib.Constants.BALLOT_SIZE; j++)
            {
                string temp = null;
                for (int i = 0; i < NetworkLib.Constants.NUM_OF_CANDIDATES; i++)
                {
                    temp += ballotMatrix[i, j];
                }
                columns.Add(temp);
            }
        }
    }
}
