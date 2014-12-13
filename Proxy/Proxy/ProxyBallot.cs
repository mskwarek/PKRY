using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Proxy
{
    class ProxyBallot
    {
        private SerialNumberGenerator sng;
        private BigInteger SL;
        private BigInteger SR;
        private List<BigInteger> answerPos;
        private int[,] vote;
        private int[,] ballotMatrix;
        private List<BigInteger[]> tokens;
        
        public ProxyBallot()
        {
            sng = sng.getInstance();
            this.SR = sng.getNextSr();
            ballotMatrix = new int [Configuration.candidates, Configuration.ballotSize];
        }

        private void randomAnswerPos(){
            Random rnd = new Random();
            answerPos.Add(rnd.Next(0, Configuration.ballotSize-1));
        }

        public void getEaData(){
            //getting sl and tokens from Ea
        }

        public void sendVoterData()
        {
            //sending SL + SR to voter; 
            //AND "YES" POSITION - vector?
        }

        public void getVote()
        {
            //getting vote from Voter
        }

        private void generateAndSplitBallotMatrix(){
            BigInteger[] temp = new BigInteger[Configuration.ballotSize];

            for (int i = 0; i < Configuration.candidates; i++)
            {
                for (int j = 0; j < Configuration.ballotSize; j++)
                {
                    if (vote[i, j] != 1 && j != answerPos[i])
                    {
                        ballotMatrix[i, j] = 1;
                    }
                }
            }
            for (int j = 0; j < Configuration.ballotSize; j++)
            {
                for (int i = 0; i < Configuration.candidates; i++)
                {
                    temp[i] = ballotMatrix[i, j];
                }
                tokens.Add(temp);
            }
        }
    }
}
