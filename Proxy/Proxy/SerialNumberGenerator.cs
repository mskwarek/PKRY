using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Org.BouncyCastle.Math;

namespace Proxy
{
    /// <summary>
    /// Generates serial numbers used in EA
    /// </summary>
    class SerialNumberGenerator
    {
        /// <summary>
        /// instance sng (singleton)
        /// </summary>
        private SerialNumberGenerator sng;

        /// <summary>
        /// list of serial numbers
        /// </summary>
        private List<BigInteger> listOfSerialNumbers;

        /// <summary>
        /// counter of serials
        /// </summary>
        int counter = 0;

        /// <summary>
        /// singleton, private constructor
        /// </summary>
        private SerialNumberGenerator(){}


        /// <summary>
        /// generate SL for election
        /// </summary>
        /// <param name="numberOfSerials">number of serials to generate</param>
        /// <param name="numberOfBits">bit size of serial</param>
        /// <returns>list of serial numbers</returns>
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

        /// <summary>
        /// gettins sng instance
        /// </summary>
        /// <returns>sng instance</returns>
        public SerialNumberGenerator getInstance()
        {
            if (sng == null)
            {
                sng = new SerialNumberGenerator(Constants.NUM_OF_CANDIDATES, Constants.NUMBER_OF_BITS_SR);
            }
            return sng;
        }

        /// <summary>
        /// generate next SR
        /// </summary>
        /// <returns>SR</returns>
        public BigInteger getNextSr()
        {
           
            BigInteger nextSr = listOfSerialNumbers[counter];
            counter++;

            return nextSr;
        }

        /// <summary>
        /// generates list of serial numbers
        /// </summary>
        /// <param name="numberOfSerials">quantity of serial numbers</param>
        /// <param name="numberOfBits">bit length of serial number</param>
        /// <returns>list of shuffled serial numbers</returns>
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

        /// <summary>
        /// get yes/no position at ballot 
        /// </summary>
        /// <param name="numberOfVoters">quantity of voters</param>
        /// <param name="numberOfCandidates">quantity of candidates></param>
        /// <returns>list of yes no positions</returns>
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
                                        //It will be easy to split
                        str = str + random.ToString() + ":";
                    else
                        str += random.ToString();
                }
                list.Add(str);
            }
            
            return list;
        }
    }
}
