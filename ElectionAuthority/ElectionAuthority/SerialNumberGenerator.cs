using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Security.Cryptography;

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
                    listOfSerialNumber.Add(BigInteger.Abs(startValue + 1));
                }
                else
                {
                    listOfSerialNumber.Add(BigInteger.Abs(listOfSerialNumber[i - 1]+1));
                }

            }

            Extentions.Shuffle(listOfSerialNumber);
            return listOfSerialNumber;
        }
    }
}
