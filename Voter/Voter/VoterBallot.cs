using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Voter
{
    class VoterBallot
    {
        private int NumberOfCandidates;
        private int [,] voted;
        private BigInteger Sl;
        private BigInteger Sr;

        public VoterBallot(int numbOfCand)
        {
            NumberOfCandidates = numbOfCand;
            voted = new int[numbOfCand, Constants.BALLOTSIZE];
        }

        public bool vote(int x, int y)
        {
            if (voteInRowDone(x, y))
            {
                return false;
            }
            else
            {
                voted[x, y] = 1;
                return true;
            }
            
        }

        private bool voteInRowDone(int x, int y)
        {
            for (int i = 0; i < Constants.BALLOTSIZE; i++)
            {
                if (voted[x, i] != 0)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
