using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ElectionAuthority
{
    /// <summary>
    /// additional function for our program
    /// </summary>
    public static class Extentions
    {
        

        /// <summary>
        /// shuffling lists (used ie. for generation SL)
        /// </summary>
        /// <typeparam name="T">type of list</typeparam>
        /// <param name="list">list which is going to be shuffled</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
