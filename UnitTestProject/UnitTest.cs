using Microsoft.VisualStudio.TestTools.UnitTesting;
using bellatrixLogTest;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMessageToFile()
        {
            var testJob = new JobLogger(true, false, false, true, false, false);
            testJob.LogMessage("", true, false, false);
        }

        [TestMethod]
        public void TestMessageToConsole()
        {
            var testJob = new JobLogger(false, true, false, true, false, false);
            testJob.LogMessage("", true, false, false);
        }

        [TestMethod]
        public void TestMessageToDataBase()
        {
            var testJob = new JobLogger(false, false, true, true, false, false);
            testJob.LogMessage("", true, false, false);
        }

        [TestMethod]
        public void TestWarningToFile()
        {
            var testJob = new JobLogger(true, false, false, false, true, false);
            testJob.LogMessage("", false, true, false);
        }
        [TestMethod]
        public void TestWarningToConsole()
        {
            var testJob = new JobLogger(false, true, false, false, true, false);
            testJob.LogMessage("", false, true, false);
        }
        [TestMethod]
        public void TestWarningToDataBase()
        {
            var testJob = new JobLogger(false, false, true, false, true, false);
            testJob.LogMessage("", false, true, false);
        }
        [TestMethod]
        public void TestErrorToFile()
        {
            var testJob = new JobLogger(true, false, false, false, false, true);
            testJob.LogMessage("", false, false, true);
        }
        [TestMethod]
        public void TestErrorToConsole()
        {
            var testJob = new JobLogger(false, true, false, false, false, true);
            testJob.LogMessage("", false, false, true);
        }
        [TestMethod]
        public void TestErrorToDataBase()
        {
            var testJob = new JobLogger(false, false, true, false, false, true);
            testJob.LogMessage("", false, false, true);
        }
    }
}
