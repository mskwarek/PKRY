using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Org.BouncyCastle.Math;

namespace Proxy
{
    class SerialNumberGenerator
    {
        private SerialNumberGenerator sng;
        private List<BigInteger> listOfSerialNumbers;
        int counter = 0;

        private SerialNumberGenerator(){}
        private SerialNumberGenerator(int numberOfSerials, int numberOfBits)
        {
            listOfSerialNumbers = new List<BigInteger>();
            Random random = new Random();
            byte[] data = new byte[numberOfBits];
            random.NextBytes(data);

            BigInteger startValue = new BigInteger(data);

            for (int i = 0; i < numberOfSerials; i++)
            {
                if (i == 0)
                {
                    listOfSerialNumbers.Add(startValue.Add(new BigInteger("1")));
                }
                else
                {
                    listOfSerialNumbers.Add(listOfSerialNumbers[i - 1].Add(new BigInteger("1")));
                }

            }

            Extentions.Shuffle(listOfSerialNumbers);
        }

        public SerialNumberGenerator getInstance()
        {
            if (sng == null)
            {
                sng = new SerialNumberGenerator(Constants.NUMBER_OF_CANDIDATES, Constants.NUMBER_OF_BITS_SR);
            }
            return sng;
        }

        public BigInteger getNextSr()
        {
           
            BigInteger nextSr = listOfSerialNumbers[counter];
            counter++;

            return nextSr;
        }


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
                    listOfSerialNumber.Add(startValue.Add(new BigInteger("1")));
                }
                else 
                {
                    listOfSerialNumber.Add(listOfSerialNumber[i - 1].Add(new BigInteger("1")));
                }

            }

            Extentions.Shuffle(listOfSerialNumber);
            return listOfSerialNumber;
        }
    }
}
