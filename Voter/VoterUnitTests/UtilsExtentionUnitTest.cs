using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace VoterUnitTests
{
    [TestClass]
    public class UtilsExtentionUnitTest
    {
        [TestMethod]
        public void TestShuffle()
        {
            List<int> permutation = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Utils.Extentions.Shuffle(permutation);
        }
    }
}
