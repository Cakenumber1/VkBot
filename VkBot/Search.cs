using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using CsQuery;
using CsQuery.ExtensionMethods.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VkBot
{
    public class Search
    {

        
        Tuple<string, string, string>[] val = new Tuple<string, string, string>[5];
        Tuple<string, string>[] regs = new Tuple<string, string>[30];
        public List<string> answ = new List<string>();
        public string[] sorters = new string[4];

        public Search()
        {
            sorters[0] = "недорог";
            sorters[1] = "дешев";
            sorters[2] = "доступн";
            sorters[3] = "дорог";

            val[0] = new Tuple<string, string, string>("CNY", "Юань", "19");
            val[1] = new Tuple<string, string, string>("JPY", "Иена","36" );
            val[2] = new Tuple<string, string, string>("EUR", "Евро","4" );
            val[3] = new Tuple<string, string, string>("GBP", "Фунт", "8");
            val[4] = new Tuple<string, string, string>("USD", "Доллар", "1");

            regs[0] = new Tuple<string, string>("Россия", "");
            regs[1] = new Tuple<string, string>("Москва", "moskva");
            regs[2] = new Tuple<string, string>("Санкт-Перербург", "sankt-peterburg");
            regs[3] = new Tuple<string, string>("Екатеринбург", "ekaterinburg");
            regs[4] = new Tuple<string, string>("Казань", "kazan");
            regs[5] = new Tuple<string, string>("Оренбург", "orenburg");
            regs[6] = new Tuple<string, string>("Новосибирск", "novosibirsk");
            regs[7] = new Tuple<string, string>("Томск", "tomsk");
            regs[8] = new Tuple<string, string>("Самара", "samara");
            regs[9] = new Tuple<string, string>("Челябинск", "chelyabinsk");
            regs[10] = new Tuple<string, string>("Ростов", "rostov-na-donu");
            regs[11] = new Tuple<string, string>("Уфа", "ufa");
            regs[12] = new Tuple<string, string>("Красноярск", "krasnoyarsk");
            regs[13] = new Tuple<string, string>("Пермь", "perm");
            regs[14] = new Tuple<string, string>("Воронеж", "voronezh");
            regs[15] = new Tuple<string, string>("Волгоград", "volgograd");
            regs[16] = new Tuple<string, string>("Краснодар", "krasnodar");
            regs[17] = new Tuple<string, string>("Саратов", "saratov");
            regs[18] = new Tuple<string, string>("Тюмень", "tumen");
            regs[19] = new Tuple<string, string>("Тольятти", "tolyatti");
            regs[20] = new Tuple<string, string>("Ижевск", "izhevsk");
            regs[21] = new Tuple<string, string>("Барнаул", "barnaul");
            regs[22] = new Tuple<string, string>("Иркутск", "irkutsk");
            regs[23] = new Tuple<string, string>("Ульяновск", "ulyanovsk");
            regs[24] = new Tuple<string, string>("Хабаровск", "habarovsk");
            regs[25] = new Tuple<string, string>("Ярославль", "yaroslavl");
            regs[26] = new Tuple<string, string>("Владивосток", "vladivostok");
            regs[27] = new Tuple<string, string>("Махачкала", "mahachkala");
            regs[28] = new Tuple<string, string>("Омск", "omsk");
            regs[29] = new Tuple<string, string>("Нижний Новгород", "nizhniy-novgorod");

        }
        public string sortn = "";
        public string v1;
        public string v2;
        public string r;
        public bool command(string val1)
        {
            if (val1 == "!help")
            {
                answ.Add("Для получения информации о боте введите \"!гайд\"");
                answ.Add("Для получения списка городов введите \"!города\"");
                answ.Add("Для получения списка валют введите \"!валюты\"");
                answ.Add("Для получения справки по поиску курса валюты за определенную дату введите\"!цб\"");
                return true;
            }
            if (val1 == "!гайд")
            {
                answ.Add("Укажите в сообщении валюту, информацию о которой желаете получить.");
                answ.Add("(Опционально)Укажите в сообщении город, чтобы данные были локальными, а не по всей России");
                return true;
            }
            if (val1 == "!города")
            {
                for (int i = 1; i < 30; i++)
                {
                    answ.Add(regs[i].Item1);

                }

                return true;
            }
            if (val1 == "!валюты")
            {
                for (int i = 0; i < 5; i++)
                {
                    answ.Add(val[i].Item1 + " или " + val[i].Item2);

                }
                return true;
            }
            if (val1 == "!цб")
            {
                answ.Add("Введите \"!цб *Валюта(3 символа)* *дата*\" для получения курса валюты определенного цб за введенную дату");
                answ.Add("Пример \"!цб EUR 11.11.2019\"");
                return true;
            }
            if (val1.StartsWith("!цб") && (val1.Length > 5))
            {
                //val1.Substring(4, val1.Length - 4);
                //Console.WriteLine(val1.Substring(4, val1.Length - 4));
                searchDate(val1.Substring(4, val1.Length - 4));
                return true;
            }

            return false;

        }
        public string getVal(string val1)
        {
            v1 = val1;
            for (int t = 0; t < 5; t++)
            {
                if (v1.ToLower().Contains(val[t].Item1.ToLower()))
                {
                    v2 = val[t].Item1;
                    if (val[t].Item1 == "JPY")
                    {
                        answ.Add(val[t].Item1 + " " + val[t].Item2 + " (100 ед)");
                        sortn = val[t].Item3;
                    }
                    else
                    {
                        answ.Add(val[t].Item1 + " " + val[t].Item2);
                        sortn = val[t].Item3;
                    }
                    return v2;
                }
            }
            for (int t = 0; t < 5; t++)
            {
                if (v1.ToLower().Contains(val[t].Item2.Substring(0, val[t].Item2.Length - 1).ToLower()))
                {
                    v2 = val[t].Item1;
                    if (val[t].Item1 == "JPY")
                    {
                        answ.Add(val[t].Item1 + " " + val[t].Item2 + " (100 ед)");
                        sortn = val[t].Item3;
                    }
                    else
                    {
                        answ.Add(val[t].Item1 + " " + val[t].Item2);
                        sortn = val[t].Item3;
                    }
                    return v2;
                }
            }
            return v2;
        }
        public string getReg()
        {
            for (int t = 0; t < 29; t++)
            {
                if (v1.ToLower().Contains(regs[t].Item1.Substring(0, regs[t].Item1.Length - 1).ToLower()))
                {
                    r = regs[t].Item2;
                    answ.Add("г. " + regs[t].Item1);
                    return r;
                }

            }

            if (v1.ToLower().Contains(regs[29].Item1.Substring(7, regs[29].Item1.Length - 7).ToLower()))
            {
                r = regs[29].Item2;
                answ.Add("г. " + regs[29].Item1);
                return r;
            }
            answ.Add("Информация по Рф");
            return r;
        }
        public int getSorttype()
        {
            for (int t = 0; t < 4; t++)
            {
                if (v1.ToLower().Contains(sorters[t]))
                {

                    return t;
                }

            }
            return -1;
        }
        public string printResult()
        {
            string res = "";
            if (answ.Count > 0)
            {
                for (int i = 0; i < answ.Count; i++)
                {
                    res += answ[i] + '\n';
                }
                return res;
            }
            return "Чтобы получить справку по командам напишите \"!help\"";
        }
        public void searchOth(string mess)
        {

            bool check = command(mess);
            if (check)
            {

            }
            else
            {
                string curVal = getVal(mess);
                if (!curVal.IsNullOrEmpty())
                {
                    
                    string urlMain = "https://ru.myfin.by/currency";
                    string curReg = getReg();
                    string curUrl = urlMain + "/" + curVal.ToLower() + "/" + curReg;
                    if (curReg.IsNullOrEmpty())
                    {
                        curUrl = curUrl.Substring(0, curUrl.Length - 1);
                    }
                    else
                    {

                    }
                    int z = getSorttype();
                    if (z == 0 || z == 1 || z == 2 )
                    {
                        curUrl += "?sort=buy_course_"+sortn;
                    }
                    if (z == 3)
                    {
                        curUrl += "?sort=-sell_course_"+sortn;
                    }
                    CQ dom0 = CQ.CreateFromUrl(curUrl);
                    answ.Add(curUrl);
                    int tr = 0;
                    foreach (IDomObject obj in dom0.Find("td"))
                    {
                        if (tr == 0)
                        {
                            if (obj.ClassName == "bank_name")
                            {
                                answ.Add(obj.Cq().Text());
                                tr = 2;
                                continue;
                            }
                        }
                        if (tr == 2)
                        {
                            answ.Add(obj.Cq().Text());
                            tr--;
                            continue;
                        }
                        if (tr == 1)
                        {
                            answ.Add(obj.Cq().Text());
                            tr--;
                            continue;
                        }
                    }
                }
            }
        }

        public void searchDate(string mess)
        {
            string urlMain = "http://www.cbr.ru/currency_base/daily/?UniDbQuery.Posted=True&UniDbQuery.To=";
            string[] inf = mess.Split(' ', 2);
            if (inf[0].Length != 3 && inf[1].Length != 10)
            {

            }
            else
            {
                for (int t = 0; t < 5; t++)
                {
                    if (inf[0] == val[t].Item1)
                    {
                        v2 = val[t].Item1;
                        answ.Add("Курс " + val[t].Item2 + " (" + val[t].Item1 + ") на " + inf[1] + " от Цб:");
                    }
                }
                string curUrl = urlMain + inf[1];
                CQ dom0 = CQ.CreateFromUrl(curUrl);
                int tr = 0;
                int amount = 1;
                foreach (IDomObject obj in dom0.Find("td"))
                {
                    if (tr == 0)
                    {
                        if (obj.Cq().Text() == inf[0])
                        {
                            tr = 3;
                            continue;
                        }
                    }
                    if (tr == 3)
                    {
                        amount = Convert.ToInt32(obj.Cq().Text());
                        tr--;
                        continue;
                    }
                    if (tr == 2)
                    {
                        tr--;
                        continue;
                    }
                    if (tr == 1)
                    {
                        answ.Add(obj.Cq().Text() + " руб. за " + amount + " ед.");
                        break;
                    }
                }
            }
        }
    }
}
