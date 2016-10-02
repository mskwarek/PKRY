using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voter;
using System.Windows.Forms;
using Org.BouncyCastle.Math;

namespace VoterUnitTests
{
    public class ConfirmationUnitTestBase
    {
        protected Voter.Confirmation confirmation; 

        public ConfirmationUnitTestBase()
        {
            ListView lw = new ListView();
            confirmation = new Voter.Confirmation(lw);
        }
    }

    [TestClass]
    public class ConfirmationUnitTest : ConfirmationUnitTestBase
    {
        [TestMethod]
        public void TestConfirmation()
        {
            confirmation.ColumnNumber = 1;
            Assert.AreEqual(confirmation.ColumnNumber, 1);
        }

        [TestMethod]
        public void TestColumn()
        {
            string testColumn = "1111";
            confirmation.Column = testColumn;
            Assert.AreEqual(testColumn, confirmation.Column);
        }

        [TestMethod]
        public void TestToken()
        {
            BigInteger testToken = new BigInteger("20001");
            confirmation.Token = testToken;
            Assert.AreEqual(testToken, confirmation.Token);
        }

        [TestMethod]
        public void TestSignedColumn()
        {
            BigInteger testSignedColumn = new BigInteger("20001");
            confirmation.SignedColumn = testSignedColumn;
            Assert.AreEqual(testSignedColumn, confirmation.SignedColumn);
        }

        [TestMethod]
        public void TestAddingLogs()
        {
            confirmation.addConfirm();
            Assert.AreEqual(4, confirmation.getLogsCounter());
        }


    }
}
