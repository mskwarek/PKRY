using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace ElectionAuthority
{
    /// <summary>
    /// Generates serial numbers used in EA
    /// </summary>
    class SerialNumberGenerator
    {
        /// <summary>
        /// generate SL for election
        /// </summary>
        /// <param name="numberOfSerials">number of serials to generate</param>
        /// <param name="numberOfBits">bit size of serial</param>
        /// <returns>list of serial numbers</returns>
        public static List<BigInteger> generateListOfSerialNumber(int numberOfSerials, int numberOfBits)
        {

            List<BigInteger> listOfSerialNumber = new List<BigInteger>();
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            byte[] data = new byte[numberOfBits];
            random.GetBytes(data);

            BigInteger startValue = new BigInteger(data);
            for (int i = 0; i < numberOfSerials; i++)
            {
                if (i == 0)
                {
                    listOfSerialNumber.Add(startValue.Add(new BigInteger(1.ToString())).Abs());
                }
                else
                {
                    listOfSerialNumber.Add((listOfSerialNumber[i - 1].Add(new BigInteger(1.ToString()))).Abs());
                }

            }

            Extentions.Shuffle(listOfSerialNumber);
            return listOfSerialNumber;
        }

        /// <summary>
        /// generate pre tokens (key pair) for election
        /// </summary>
        /// <param name="numberOfSerials">number of serials to generate</param>
        /// <param name="numberOfBits">bit size of serial</param>
        /// <returns>list of pre tokens</returns>
        public static List<AsymmetricCipherKeyPair> generatePreTokens(int numberOfSerials, int numberOfBits)
        {
            List<AsymmetricCipherKeyPair> preTokens = new List<AsymmetricCipherKeyPair>();
            for (int i = 0; i < numberOfSerials; i++)
            {
                KeyGenerationParameters para = new KeyGenerationParameters(new SecureRandom(), numberOfBits);
                //generate the RSA key pair
                RsaKeyPairGenerator keyGen = new RsaKeyPairGenerator();
                //initialise the KeyGenerator with a random number.
                keyGen.Init(para);
                AsymmetricCipherKeyPair keypair = keyGen.GenerateKeyPair();
                preTokens.Add(keypair);
            }

            return preTokens;
        }
    }
}
