using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace VkBot.Test
{
    [TestClass]
    public class UnitTest2
    {
        string[] test = { "Битва на Неретве", "asdasd vdhfzvasdv123"};
        string[] exp = { "Битва на Неретве", "asdasd" };
        Search s = new Search();
        string g = "Запрос будет выполнен корректно, если в нем будет указана 1 валюта и 1 город.";

        [TestMethod]
        public void bothAddAndPrintTest0()
        {
            s.answAdd(test[0]);
            
            Assert.AreEqual(s.printResult(), exp[0] + '\n');


        }
        [TestMethod]
        public void bothAddAndPrintTest1()
        {

            Assert.AreEqual(s.printResult(), "Чтобы получить справку по командам напишите \"!help\"");

        }
        [TestMethod]
        public void bothAddAndPrintTest2()
        {
            s.logsCall(test[1]);

            Assert.AreEqual(s.printResult(), "printResult func started"+'\n');

        }

    }
}
