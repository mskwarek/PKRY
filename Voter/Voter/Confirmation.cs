using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voter
{
    class Confirmation
    {
        private int numColumn;
        private BigInteger column;
        private BigInteger token;
        private BigInteger signedColumn;


        public Confirmation(int number)
        {
            this.numColumn = number;
        }

        public BigInteger Column
        {
            set { column = value; }
        }

        public BigInteger Token
        {
            set { token = value; }
        }

        public BigInteger SignedColumn
        {
            set { signedColumn = value; }
        }

        public int Index
        {
            get { return numColumn; }
        }
    }
}
