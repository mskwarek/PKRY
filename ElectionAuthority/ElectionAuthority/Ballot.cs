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
    /// <summary>
    /// class represents one ballot and has information about it
    /// </summary>
    class Ballot
    {
      
        /// <summary>
        /// SL number - BigInteger for voter which let bind permutation, tokens and apropiate candidate list 
        /// </summary>
        private BigInteger sl;
        public BigInteger SL
        {
            get { return sl; }
        }

        /// <summary>
        /// tokens used for blind signature - it's public key's modulus
        /// </summary>
        private List<BigInteger> tokenList;
        public List<BigInteger> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        /// <summary>
        /// exponents (for blind signature) - public key's exponent
        /// </summary>
        private List<BigInteger> exponentsList;
        public List<BigInteger> ExponentsList
        {
            get { return exponentsList; }
            set { exponentsList = value; }
        }

        /// <summary>
        /// every ballot has its own signature factor (such as tokens) - private key's exponent
        /// </summary>
        private List<BigInteger> signatureFactor;
        public List<BigInteger> SignatureFactor
        {
            get { return signatureFactor; }
            set { signatureFactor = value; }
        }

        /// <summary>
        /// signed column (EA signature)
        /// </summary>
        private BigInteger[] signedColumn;
        public BigInteger[] SignedColumn
        {
            get { return signedColumn; }
        }

        /// <summary>
        /// blind colmun recived from proxy
        /// </summary>
        private BigInteger[] blindColumn;
        public BigInteger[] BlindColumn
        {
            set { blindColumn = value; }
        }

        /// <summary>
        /// unblinded ballot, next step in blind signature
        /// </summary>
        private string[,] unblindedBallot;
        public string[,] UnblindedBallot
        {
            set { unblindedBallot = value; }
            get { return unblindedBallot; }
        }

        /// <summary>
        /// permutation connected to ballot (so SL too)
        /// </summary>
        private List<BigInteger> permutation;
        public List<BigInteger> Permutation
        {
            set { permutation = value; }
            get { return permutation; }
        }

        /// <summary>
        /// inverse permutation for each ballot
        /// </summary>
        private List<BigInteger> inversePermutation;
        public List<BigInteger> InversePermutation
        {
            set { inversePermutation = value; }
            get { return inversePermutation; }
        }

        /// <summary>
        /// ballot's constructor
        /// </summary>
        /// <param name="SL">serial (list of candidate) number</param>
        public Ballot(BigInteger SL)
        {
            this.sl = SL;
            
        }


        /// <summary>
        /// Method to sing each column in ballotMatrix
        /// </summary>
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
