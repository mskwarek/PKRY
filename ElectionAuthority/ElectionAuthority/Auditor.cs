using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Math;

namespace ElectionAuthority
{
    class Auditor
    {

        private Logs logs;
        private BigInteger[] blindPermutation;
        public BigInteger[] BlindPermatation
        {
            set { blindPermutation = value; }
            get { return blindPermutation; }
        }


        public Auditor(Logs logs)
        {
            this.logs = logs;

        }

        





    }
}
