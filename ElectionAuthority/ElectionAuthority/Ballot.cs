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
        //class represents one ballot and has information about it

        //SL number
        private BigInteger sl;
        public BigInteger SL
        {
            get { return sl; }
        }

        //tokens
        private List<BigInteger> tokenList;
        public List<BigInteger> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        //exponents (for blind signature)
        private List<BigInteger> exponentsList;
        public List<BigInteger> ExponentsList
        {
            get { return exponentsList; }
            set { exponentsList = value; }
        }

        //every ballot has its own signature factor (such as tokens)
        private List<BigInteger> signatureFactor;
        public List<BigInteger> SignatureFactor
        {
            get { return signatureFactor; }
            set { signatureFactor = value; }
        }

        //public keys, for blind signature too
        private BigInteger pubKeyModulus;
        public BigInteger PubKeyModulus
        {
            set { pubKeyModulus = value; }
        }


        //signed column (EA signature)
        private BigInteger[] signedColumn;
        public BigInteger[] SignedColumn
        {
            get { return signedColumn; }
        }

        //blind colmun recived from proxy
        private BigInteger[] blindColumn;
        public BigInteger[] BlindColumn
        {
            set { blindColumn = value; }
        }

        //inblinded ballot, next step in blind signature
        private string[,] unblindedBallot;
        public string[,] UnblindedBallot
        {
            set { unblindedBallot = value; }
            get { return unblindedBallot; }
        }

        //permutation connected to ballot (so SL too)
        private List<BigInteger> permutation;
        public List<BigInteger> Permutation
        {
            set { permutation = value; }
            get { return permutation; }
        }

        //inverse permutation for each ballot
        private List<BigInteger> inversePermutation;
        public List<BigInteger> InversePermutation
        {
            set { inversePermutation = value; }
            get { return inversePermutation; }
        }

        public Ballot(BigInteger SL)
        {
            this.sl = SL;
            
        }


        //Method to sing each column in ballotMatrix
        public void signColumn()
        {
            BigInteger[] signed = new BigInteger[Constants.BALLOT_SIZE];
            int i = 0;
            foreach (BigInteger column in blindColumn)
            {
                signed[i] = column.ModPow(signatureFactor[i], tokenList[i]);
                i++;
            }

            this.signedColumn = signed;
        }
    }
}
