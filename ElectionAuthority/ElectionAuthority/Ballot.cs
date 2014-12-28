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
        private BigInteger[] tokens;
        private BigInteger pubKeyModulus;

        public BigInteger pubKeyMod
        {
            set { pubKeyModulus = value; }
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

        private BigInteger[] SignColumn()
        {
            BigInteger[] signed = new BigInteger[Constants.BALLOT_SIZE];
            int i = 0;

            foreach (BigInteger column in blindColumn)
            {
                signed[i] = blindColumn[i].ModPow(tokens[i], pubKeyModulus);
                i++;
            }

            return signed;
        }
    }
}
