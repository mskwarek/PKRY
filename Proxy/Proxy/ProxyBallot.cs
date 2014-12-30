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
        private Logs logs;                                              //logs 
        private RsaKeyParameters pubKey;                                //pub key to blind sign
        public RsaKeyParameters PubKey
        {
            get{return pubKey;}
        }
        private RsaKeyParameters privKey;                               //priv Key to blind signature
        private SerialNumberGenerator sng;                              //generator SRand SL **********TO CHYBA TERAZ JUZ NIE POTRZEBNE***********
        private BigInteger[] r;                                         //random blinding factor
        private BigInteger sl;
        public BigInteger SL
        {
            get { return sl; }
        }
        private BigInteger sr;
        private string yesNoPos;                                          //position of "yes" answer
        public string YesNoPos
        {
            set { yesNoPos = value; }
        }
        
        private int[,] vote;                                            //vote from voter
        public int[,] Vote
        {
            set { vote = value; }
        }
        


        private int[,] ballotMatrix;                                    //ballot matrix just fo proxy operations
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
                Console.WriteLine("r = " + r[i]);
                Console.WriteLine("blinded"+i+" = " + b);
                i++;
            }
            return toSend;
        }

        public string[] unblindSignedData(BigInteger[] signedData)
        {
            string[] unblinded = new string[Constants.BALLOT_SIZE];
            //BigInteger e = pubKey.Exponent;
            //BigInteger n = pubKey.Modulus;
           // BigInteger d = privKey.Exponent;

            for (int i = 0; i < signedData.Length; i++)
            {
                BigInteger explicitData = new BigInteger(columns[i]);
                BigInteger n = tokensList[i];
                BigInteger e = exponentsList[i];
               /*
                Console.WriteLine(i);
                Console.WriteLine("input = " + signedData[i]);
                Console.WriteLine("e = " + e);
                Console.WriteLine("n = " + n);
                Console.WriteLine("r = " + r[i]);
                *///unblind sign
                BigInteger signed = ((r[i].ModInverse(n)).Multiply(signedData[i])).Mod(n);
                Console.WriteLine("signedUnblind = " + signed);

                //BigInteger s = ((r.ModInverse(n)).Multiply(signedData[i])).Mod(n);
                
                //Console.WriteLine("s = " + s);
                BigInteger check = signed.ModPow(e, n);
                //Console.WriteLine("explicitData = " + explicitData);
                //Console.WriteLine("check = " + check);
                if(explicitData.Equals(check))
                {
                    //BigInteger check = signed.ModPow(e, n);
                    String str = check.ToString();
                    //WYSŁAć NORMALNA KOLUMNE, BO WIEMY ZE NIE OSZUKA
                    this.logs.addLog(Constants.CORRECT_SIGNATURE, true, Constants.LOG_INFO, true);
                //^ WYWALA WYJATEK
                }
                else{
                    this.logs.addLog(Constants.WRONG_SIGNATURE, true, Constants.LOG_ERROR, true);
                }
            }
            return unblinded;
        }



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
