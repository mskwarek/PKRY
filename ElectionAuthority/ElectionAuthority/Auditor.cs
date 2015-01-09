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
    /// <summary>
    /// class is used to verify if EA did not cheat
    /// </summary>
    class Auditor
    {

        /// <summary>
        /// allows to collect and display logs
        /// </summary>
        private Logs logs;

        /// <summary>
        /// Big Integer array of commited permutation recieved from EA
        ///
        /// </summary>
        private BigInteger[] commitedPermutation;

        /// <summary>
        /// set and get commited permutation
        /// </summary>
        public BigInteger[] CommitedPermatation
        {
            set { commitedPermutation = value; }
            get { return commitedPermutation; }
        }

        /// <summary>
        /// Auditor's constructor
        /// </summary>
        /// <param name="logs">transfered log instance </param>
        public Auditor(Logs logs)
        {
            this.logs = logs;

        }

        /// <summary>
        /// checking the correctness of permutation
        /// </summary>
        /// <param name="privateKey">private key used for bit commitment</param>
        /// <param name="publicKey">public key used for bit commitment</param>
        /// <param name="explicitPermutation">used permutation (as open text)</param>
        /// <returns></returns>
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
