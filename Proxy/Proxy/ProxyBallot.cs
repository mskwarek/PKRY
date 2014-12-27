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
        private RsaKeyParameters pubKey;                                //pub key to blind sign
        private RsaKeyParameters privKey;                               //priv Key to blind signature
        private SerialNumberGenerator sng;                              //generator SRand SL
        private BigInteger r;                                           //random blinding factor
        private BigInteger SL;
        private BigInteger SR;
        private List<int> yesPos;                                       //position of "yes" answer
        private int[,] vote;                                            //vote from voter
        private int[,] ballotMatrix;                                    //ballot matrix just fo proxy operations
        private List<string> columns;
        private List<Org.BouncyCastle.Math.BigInteger[]> tokens;
        
       
        public ProxyBallot(int[,] vote)
        {
            this.vote = vote;
            sng = sng.getInstance();
            this.SR = sng.getNextSr();
            ballotMatrix = new int [Configuration.candidates, Configuration.ballotSize];
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

        private void randomYesPos(){
            Random rnd = new Random();
            yesPos.Add(rnd.Next(0, Configuration.ballotSize-1));
        }

        public void getEaData(){
            //getting sl and tokens from Ea
        }

        public void sendVoterData()
        {
            //sending SL + SR to voter; 
            //AND "YES" POSITION - vector?


        }

        public void getVote()
        {
            //getting vote from Voter
        }

        private BigInteger[] prepareDataToSend()
        {
            BigInteger[] toSend = new BigInteger[Constants.BALLOT_SIZE];

            //blinding columns, prepare to signature
            this.generateAndSplitBallotMatrix();
            int i=0;
            foreach (string column in columns)
            {
                BigInteger toBlind = new BigInteger(System.Text.Encoding.ASCII.GetBytes(column));
                BigInteger e = pubKey.Exponent;
                BigInteger d = privKey.Exponent;

                SecureRandom random = new SecureRandom();
                byte[] randomBytes = new byte[10];
                
                BigInteger n = pubKey.Modulus;
                BigInteger gcd = null;
                BigInteger one = new BigInteger("1");

                //check that gcd(r,n) = 1 && r < n && r > 1
                do
                {
                    random.NextBytes(randomBytes);
                    r = new BigInteger(1, randomBytes);
                    gcd = r.Gcd(n);
                    Console.WriteLine("gcd: " + gcd);
                }
                while (!gcd.Equals(one) || r.CompareTo(n) >= 0 || r.CompareTo(one) <= 0);

                //********************* BLIND ************************************
                BigInteger b = ((r.ModPow(e, n)).Multiply(toBlind)).Mod(n);
                toSend[i] = b;
                i++;
            }
            return toSend;
        }

        private string[] unblindSignedData(BigInteger[] signedData)
        {
            string[] unblinded = new string[Constants.BALLOT_SIZE];
            BigInteger e = pubKey.Exponent;
            BigInteger n = pubKey.Modulus;

            for (int i = 0; i < signedData.Length; i++)
            {
                BigInteger s = ((r.ModInverse(n)).Multiply(signedData[i])).Mod(n);
                String str = System.Text.Encoding.ASCII.GetString(s.ModPow(e, n).ToByteArray());
            }
            return unblinded;
        }


        private void generateAndSplitBallotMatrix(){
            string temp;

            for (int i = 0; i < Constants.NUM_OF_CANDIDATES; i++)
            {
                for (int j = 0; j < Constants.BALLOT_SIZE; j++)
                {
                    //mark every non-clicked "No" button
                    if (vote[i, j] != 1 && j != yesPos[i])
                    {
                        ballotMatrix[i, j] = 1;
                    }
                }
            }

            //rewrite colums from ballot matrix to another array
            for (int j = 0; j < Constants.BALLOT_SIZE; j++)
            {
                temp = "";
                for (int i = 0; i < Constants.NUM_OF_CANDIDATES; i++)
                {
                    temp += ballotMatrix[i, j];
                }
                columns.Add(temp);
            }
        }
    }
}
