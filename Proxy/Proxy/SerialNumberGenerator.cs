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

            Utils.Extentions.Shuffle(listOfSerialNumbers);
        }

        public SerialNumberGenerator getInstance()
        {
            if (sng == null)
            {
                sng = new SerialNumberGenerator(NetworkLib.Constants.NUM_OF_CANDIDATES, NetworkLib.Constants.NUMBER_OF_BITS_SR);
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

            Utils.Extentions.Shuffle(listOfSerialNumber);
            return listOfSerialNumber;
        }

        public static List<string> getYesNoPosition(int numberOfVoters, int numberOfCandidates)
        {
            Random rnd = new Random();
            List<string> list = new List<string>();
            int range = 4;
            for (int k = 0; k < numberOfVoters; k++)
            {
                string str = null;
                for (int i = 0; i < numberOfCandidates; i++)
                {
                    int random = rnd.Next(0, range);
                    if (i != numberOfCandidates - 1) // we use this if to create string looks like "number:number:number:number". 
                    {                  //It will be easy to split
                        str = str + random.ToString() + ":";
                    }
                    else
                    {
                        str += random.ToString();
                    }
                }
                list.Add(str);
            }
            
            return list;
        }
    }
}
