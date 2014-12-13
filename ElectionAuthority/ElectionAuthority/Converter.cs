using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ElectionAuthority
{
    class Converter
    {

        public static string convertDictionaryToString(string flag, Dictionary<BigInteger, List<BigInteger>> dictionary)
        {
            //It convert dictionary to. String will look like that:
            //s=flag&firstKey=valueFirst,valueSecond,..,lastValue&secondKey=valueFirst,valueSecond ....
            string s = flag;
            foreach (BigInteger bigInt in dictionary.Keys)
            {
                s = s+ "&"+bigInt+"=";
                List<BigInteger> list = new List<BigInteger>(dictionary[bigInt]);
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != list.Count - 1)
                        s = s + list[i] + ",";
                    else
                        s = s + list[i];
                }
                foreach (BigInteger bigIntInsideList in list)
                {
                   
                }
            }

            return s;


        }

    }
}
