using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ElectionAuthority
{
    class Ballot
    {

        private BigInteger sl;
        private BigInteger[] tokens;

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
    }
}
