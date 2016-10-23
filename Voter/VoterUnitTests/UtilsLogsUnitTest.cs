using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;

namespace VoterUnitTests
{
    [TestClass]
    public class UtilsLogsUnitTest
    {
        [TestMethod]
        public void ColorTest()
        {
            Utils.Logs.addLog("UnitTest", "test msg", true, NetworkLib.Constants.LOG_MESSAGE, true);
            Utils.Logs.addLog("UnitTest", "test error", true, NetworkLib.Constants.LOG_ERROR, true);
            Utils.Logs.addLog("UnitTest", "test info", true, NetworkLib.Constants.LOG_INFO, true);
            Utils.Logs.addLog("UnitTest", "test info", true, 3, true);
            Utils.Logs.addLog("UnitTest", "test", true, NetworkLib.Constants.LOG_MESSAGE, false);
        }
    }
}
