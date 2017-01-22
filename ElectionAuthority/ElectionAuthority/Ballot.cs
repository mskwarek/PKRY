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
        private BigInteger sl;
        public BigInteger SL
        {
            get { return sl; }
        }

        private List<BigInteger> tokenList;
        public List<BigInteger> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        private List<BigInteger> exponentsList;
        public List<BigInteger> ExponentsList
        {
            get { return exponentsList; }
            set { exponentsList = value; }
        }

        private List<BigInteger> signatureFactor;
        public List<BigInteger> SignatureFactor
        {
            get { return signatureFactor; }
            set { signatureFactor = value; }
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

        private string[,] unblindedBallot;
        public string[,] UnblindedBallot
        {
            set { unblindedBallot = value; }
            get { return unblindedBallot; }
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

        public Ballot(BigInteger SL)
        {
            this.sl = SL;          
        }

        public void signColumn()
        {
            BigInteger[] signed = new BigInteger[NetworkLib.Constants.BALLOT_SIZE];
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
