using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace ElectionAuthority
{
    class Permutation
    {
        public static List<BigInteger> generatePermutation(int candidateQuantity)
        {
            List<BigInteger> permutation = new List<BigInteger>();
            for (int i = 1; i <= candidateQuantity; i++)
            {
                permutation.Add(i);

            }
            //shuffle BigInt List
            Extentions.Shuffle(permutation);

            return permutation;
        }

        //we create a permutation matrix in way described on "http://pl.wikipedia.org/wiki/Permutacja"
        private static int[,] generatePermutationMatrix(List<BigInteger> permutation)
        {
            int candidateQuantity = permutation.Count;
            //Function allow us to get a reverse permutation. I follow the method shown at: "http://pl.wikipedia.org/wiki/Permutacja"
            int[,] tab = new int[candidateQuantity, candidateQuantity];
            int[] defaultList = new int[candidateQuantity];
            //prepare default sequence = {1....m}
            for (int i = 0; i < candidateQuantity; i++)
            {
                defaultList[i] = i + 1;
            }
            //prepare default matrix with 0 
            for (int i = 0; i < candidateQuantity; i++)
            {
                for (int j = 0; j < candidateQuantity; j++)
                {
                    tab[i, j] = 0;
                }
            }

            //we have to put "1" in each A(i,j)
            for (int i = 0; i < candidateQuantity; i++)
            {
                tab[defaultList[i] - 1, (int)permutation[i] - 1] = 1;
            }
            return tab;
        }

        private static int[,] transposeMatrix(int[,] m)
        {
            int[,] temp = new int[m.GetLength(0), m.GetLength(1)];
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(0); j++)
                    temp[j, i] = m[i, j];
            return temp;
        }

        //Find inverse permuatation using a table method
        public static List<BigInteger> getInversePermutation(List<BigInteger> permutation)
        {
            int[,] tab = Permutation.generatePermutationMatrix(permutation);

            int[,] tabInv = Permutation.transposeMatrix(tab);
            List<BigInteger> inversePermutation = new List<BigInteger>();

            for (int i = 0; i < tabInv.GetLength(1); i++)
            {
                for (int j = 0; j < tabInv.GetLength(0); j++)
                {
                    if (tabInv[i, j] == 1)
                    {
                        inversePermutation.Add(j + 1);
                    }
                }
            }
            return inversePermutation;
        }
	
    }
}
