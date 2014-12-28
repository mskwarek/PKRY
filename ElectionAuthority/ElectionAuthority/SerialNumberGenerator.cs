using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Org.BouncyCastle.Math;

namespace ElectionAuthority
{
    class SerialNumberGenerator
    {
        public static List<BigInteger> generateListOfSerialNumber(int numberOfSerials, int numberOfBits)
        {

            List<BigInteger> listOfSerialNumber = new List<BigInteger>();
            //Random random = new Random();
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
    }
}
