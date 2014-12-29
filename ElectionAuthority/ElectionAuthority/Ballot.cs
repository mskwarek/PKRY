using Org.BouncyCastle.Math;
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


        public Ballot(BigInteger SL, BigInteger[] tokens)
        {
            this.sl = SL;
            this.tokens = tokens;
        }



        //Method to sing each column in ballotMatrix
        public void signColumn()
        {
            BigInteger[] signed = new BigInteger[Constants.BALLOT_SIZE];
            int i = 0;

            foreach (BigInteger column in blindColumn)
            {
                signed[i] = blindColumn[i].ModPow(tokens[i], pubKeyModulus);
                i++;
            }

            this.signedColumn = signed;
        }
    }
}
