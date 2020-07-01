using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using VkBot;

namespace VkBot.Test
{
    [TestClass]
    public class TestLogs
    {
        string[] test = { "Битва на Неретве vdhfzvasdv123", "vdhfzvasdv123 vdhfzvasdv123", "vdhfzvasdv123", " vdhfzvasdv123 ", "" };
        string[] exp = { "Битва на Неретве", "vdhfzvasdv123", "vdhfzvasdv123", " ", "" };
        Search s = new Search();

        [TestMethod]
        public void logsTest0()
        {
            Assert.AreEqual(s.logsCall(test[0]), exp[0]);


        }
        [TestMethod]
        public void logsTest1()
        {
            Assert.AreEqual(s.logsCall(test[1]), exp[1]);

        }
        [TestMethod]
        public void logsTest2()
        {
            Assert.AreEqual(s.logsCall(test[2]), exp[2]);


        }
        [TestMethod]
        public void logsTest3()
        {

            Assert.AreNotEqual(s.logsCall(test[3]), exp[3]);

        }
        [TestMethod]
        public void logsTest4()
        {

            Assert.AreEqual(s.logsCall(test[4]), exp[4]);

        }
    }
}
