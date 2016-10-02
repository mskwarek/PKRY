using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VoterUnitTests
{
    public class VoterBase
    {
        Voter.Voter voter;
        public VoterBase()
        {
            //voter = new Voter.Voter();
        }
    }
    [TestClass]
    public class VoterUnitTest : VoterBase
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
