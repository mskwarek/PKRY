using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Voter
{
    class VoterBallot
    {
        private int numberOfCandidates;
        private int [,] voted;
        public int[,] Voted
        {
            get { return voted; }
        }
        private BigInteger sl;
        public BigInteger SL
        {
            set { sl = value; }
            get { return sl; }
        }
        private BigInteger sr;
        public BigInteger SR
        {
            set { sr = value; }
            get { return sr; }
        }

        private int numOfVotes;

        public VoterBallot(int numbOfCand)
        {
            numberOfCandidates = numbOfCand;
            numOfVotes = 0;
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
                numOfVotes += 1;
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



        public bool voteDone()
        {
            if (numOfVotes == this.numberOfCandidates)
                return true;
            else
                return false;
        }
    }
}
