using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace VkBot.Test
{
    [TestClass]
    public class TestSearch
    {
        string[] test = { "!цб", "!город спб", "!цб GBP 22.22.2022" };
        string[] exp = { "Битва на Неретве" };
        Search s = new Search();
        string g = "Запрос будет выполнен корректно, если в нем будет указана 1 валюта и 1 город.";

        [TestMethod]
        public void searchTest0()
        {
            s.searchOth(test[0]);
            string o1 = "Введите \"!цб *Валюта(3 символа)* *дата*\" для получения курса валюты определенного цб за введенную дату" + '\n' + "Пример \"!цб EUR 11.11.2019\""+'\n';
            Assert.AreEqual(s.printResult(), o1);

        }
        [TestMethod]
        public void searchTest1()
        {
            s.searchOth(test[1]);
            string o2 = "г. Спб найден в базе"+'\n';
            Assert.AreEqual(s.printResult(), o2);

        }
        [TestMethod]
        public void searchTest2()
        {
            s.searchOth(test[2]);
            string o3 = "Курс Фунт (GBP)"+ '\n' + "За 02.07.2020 от Цб"+ '\n' + "87,3965 руб. за 1 ед." +'\n' + "Проверьте корректость введенных данных 22.22.2022"+'\n';
            Assert.AreEqual(s.printResult(), o3);

        }
        [TestMethod]
        public void searchTest3()
        {
            s.searchOth("");
            string o3 = "Запрос будет выполнен корректно, если в нем будет указана 1 валюта и 1 город." + '\n';
            Assert.AreEqual(s.printResult(), o3);

        }

    }
}
