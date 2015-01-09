using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Math;

namespace Voter
{
    /// <summary>
    /// Voter ballot (vote's data as SL, SR, vote, yes/no position)
    /// </summary>
    class VoterBallot
    {
        /// <summary>
        /// number of candidates
        /// </summary>
        private int numberOfCandidates;

        /// <summary>
        /// vote as 0-1 2nd size array 
        /// </summary>
        private int [,] voted;
        public int[,] Voted
        {
            get { return voted; }
        }

        /// <summary>
        /// serial number sl
        /// </summary>
        private BigInteger sl;
        public BigInteger SL
        {
            set { sl = value; }
            get { return sl; }
        }

        /// <summary>
        /// serial number sR
        /// </summary>
        private BigInteger sr;
        public BigInteger SR
        {
            set { sr = value; }
            get { return sr; }
        }

        /// <summary>
        /// voter int
        /// </summary>
        private int numOfVotes;

        /// <summary>
        /// token
        /// </summary>
        private BigInteger token;
        public BigInteger Token
        {
            set { token = value; }
            get { return token; }
        }

        /// <summary>
        /// signed blind column
        /// </summary>
        private BigInteger signedBlindColumn;
        public BigInteger SignedBlindColumn
        {
            set { signedBlindColumn = value; }
            get { return signedBlindColumn; }
        }


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="numbOfCand"></param>
        public VoterBallot(int numbOfCand)
        {
            numberOfCandidates = numbOfCand;
            numOfVotes = 0;
            voted = new int[numbOfCand, Constants.BALLOTSIZE];
        }

        /// <summary>
        /// try-to-vote for x,y position
        /// </summary>
        /// <param name="x">1st dimension</param>
        /// <param name="y">2nd dimension</param>
        /// <returns>result of try-to-vote action</returns>
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

        /// <summary>
        /// checks if vote in row is done
        /// </summary>
        /// <param name="x">1st dimension</param>
        /// <param name="y">2nd dimension</param>
        /// <returns>checking result</returns>
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


        /// <summary>
        /// checks if voting is done
        /// </summary>
        /// <returns>if voting is done</returns>
        public bool voteDone()
        {
            if (numOfVotes == this.numberOfCandidates)
            {
                return true;
            }
            else
                return false;
        }
    }
}
