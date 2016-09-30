using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace ElectionAuthority
{
    class Auditor
    {
        private BigInteger[] commitedPermutation;

        public BigInteger[] CommitedPermatation
        {
            set { commitedPermutation = value; }
            get { return commitedPermutation; }
        }

        public Auditor()
        {
        }

        public bool checkPermutation(RsaKeyParameters privateKey, RsaKeyParameters publicKey, BigInteger[] explicitPermutation)
        {
            int i=0;
            foreach (BigInteger partPermutation in explicitPermutation)
            {

                // verify using RSA formula
                if (!partPermutation.Equals(commitedPermutation[i].ModPow(privateKey.Exponent, publicKey.Modulus)))
                {
                    return false;
                }
                i++;
                
            }

            return true;
        } 
    }
}
