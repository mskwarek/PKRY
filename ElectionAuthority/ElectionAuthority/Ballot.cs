using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectionAuthority
{
    class Ballot
    {
        private RsaKeyParameters privKey;
        private BigInteger sl;
        public BigInteger SL
        {
            get { return sl; }
        }
        private BigInteger[] tokens;
        public BigInteger[] Tokens
        {
            get { return tokens; }
        }
        private BigInteger pubKeyModulus;
        public BigInteger PubKeyModulus
        {
            set { pubKeyModulus = value; }
        }

        private BigInteger[] signedColumn;
        public BigInteger[] SignedColumn
        {
            get { return signedColumn; }
        }

        private BigInteger[] blindColumn;
        public BigInteger[] BlindColumn
        {
            set { blindColumn = value; }
        }

        private List<BigInteger> permutation;
        public List<BigInteger> Permutation
        {
            set { permutation = value; }
            get { return permutation; }
        }

        private List<BigInteger> inversePermutation;
        public List<BigInteger> InversePermutation
        {
            set { inversePermutation = value; }
            get { return inversePermutation; }
        }

        public Ballot(BigInteger SL, BigInteger[] tokens)
        {
            this.sl = SL;
            this.tokens = tokens;
        }



        //Method to sing each column in ballotMatrix
        public void signColumn()
        {

            KeyGenerationParameters para = new KeyGenerationParameters(new SecureRandom(), 1024);
	        //generate the RSA key pair
	        RsaKeyPairGenerator keyGen = new RsaKeyPairGenerator(); 
	        //initialise the KeyGenerator with a random number.
	        keyGen.Init(para);
	        AsymmetricCipherKeyPair keypair = keyGen.GenerateKeyPair();
	        privKey = (RsaKeyParameters)keypair.Private;


            BigInteger[] signed = new BigInteger[Constants.BALLOT_SIZE];
            int i = 0;


            Console.WriteLine("pubModu = " + pubKeyModulus);
            foreach (BigInteger column in blindColumn)
            {
                signed[i] = blindColumn[i].ModPow(privKey.Exponent, tokens[i]);
                i++;
            }

            this.signedColumn = signed;
        }
    }
}
