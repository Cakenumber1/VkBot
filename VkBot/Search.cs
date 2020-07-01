using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using CsQuery;
using CsQuery.ExtensionMethods.Internal;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization.Internal;
using VkNet.Exception;

namespace VkBot
{
    public class Search
    {

        
        Tuple<string, string, string>[] val = new Tuple<string, string, string>[5];
        Tuple<string, string>[] regs = new Tuple<string, string>[30];
        public List<string> answ = new List<string>();
        //public List<string> log = new List<string>();
        public string[] sorters = new string[8];
        public bool flag = false;

        public Search()
        {
            if(flag) answ.Add("class Search creation started");
            sorters[0] = "недорог";
            sorters[1] = "дешев";
            sorters[2] = "купи";
            sorters[3] = "покуп";
            sorters[4] = "приобре";
            sorters[5] = "прода";
            sorters[6] = "дорого";
            sorters[7] = "дороже";

            val[0] = new Tuple<string, string, string>("CNY", "Юань", "19");
            val[1] = new Tuple<string, string, string>("JPY", "Иена","36" );
            val[2] = new Tuple<string, string, string>("EUR", "Евро","4" );
            val[3] = new Tuple<string, string, string>("GBP", "Фунт", "8");
            val[4] = new Tuple<string, string, string>("USD", "Доллар", "1");

            regs[0] = new Tuple<string, string>("Россия", "");
            regs[1] = new Tuple<string, string>("Москва", "moskva");
            regs[2] = new Tuple<string, string>("Спб", "sankt-peterburg");
            regs[3] = new Tuple<string, string>("Екб", "ekaterinburg");
            regs[4] = new Tuple<string, string>("Оренбург", "orenburg");
            regs[5] = new Tuple<string, string>("Новосибирск", "novosibirsk");
            regs[6] = new Tuple<string, string>("Томск", "tomsk");
            regs[7] = new Tuple<string, string>("Омск", "omsk");
            regs[8] = new Tuple<string, string>("Челябинск", "chelyabinsk");
            regs[9] = new Tuple<string, string>("Ростов", "rostov-na-donu");
            regs[10] = new Tuple<string, string>("Красноярск", "krasnoyarsk");
            regs[11] = new Tuple<string, string>("Воронеж", "voronezh");
            regs[12] = new Tuple<string, string>("Волгоград", "volgograd");
            regs[13] = new Tuple<string, string>("Краснодар", "krasnodar");
            regs[14] = new Tuple<string, string>("Саратов", "saratov");
            regs[15] = new Tuple<string, string>("Владивосток", "vladivostok");
            regs[16] = new Tuple<string, string>("Ижевск", "izhevsk");
            regs[17] = new Tuple<string, string>("Барнаул", "barnaul");
            regs[18] = new Tuple<string, string>("Иркутск", "irkutsk");
            regs[19] = new Tuple<string, string>("Ульяновск", "ulyanovsk");
            regs[20] = new Tuple<string, string>("Хабаровск", "habarovsk");
            regs[21] = new Tuple<string, string>("Казань", "kazan");
            regs[22] = new Tuple<string, string>("Самара", "samara");
            regs[23] = new Tuple<string, string>("Махачкала", "mahachkala");
            regs[24] = new Tuple<string, string>("Пермь", "perm");
            regs[25] = new Tuple<string, string>("Тюмень", "tumen");
            regs[26] = new Tuple<string, string>("Тольятти", "tolyatti");
            regs[27] = new Tuple<string, string>("Ярославль", "yaroslavl");

            regs[28] = new Tuple<string, string>("Уфа", "ufa");
            regs[29] = new Tuple<string, string>("Нижний Новгород", "nizhniy-novgorod");
            if (flag) answ.Add("class Search creating finished succesfully");
        }
        public string sortn = "";
        public string v1;
        public string v2;
        public string r;
        public string logsCall(string mess)
        {
            if (mess.EndsWith(" vdhfzvasdv123"))
            {
                flag = true;
                return mess.Substring(0, mess.Length - 14);
            }
            return mess;
                // vdhfzvasdv123
        }
        public bool command(string val1)
        {
            if (flag) answ.Add("command func started with "+ val1);
            if (val1 == "!help")
            {
                answ.Add(">Для получения информации о боте введите \"!гайд\"");
                answ.Add(">Для получения списка городов введите \"!города\"");
                answ.Add(">Для получения информации по конкретному городу\nвведите \"!город *Название города*\"");
                answ.Add(">Для получения списка валют введите \"!валюты\"");
                answ.Add(">Для получения справки по поиску курса валюты\nза определенную дату введите\"!цб\"");
                if (flag) answ.Add("command !help call");
                return true;
            }
            if (val1 == "!гайд")
            {
                answ.Add("Укажите в сообщении валюту, информацию о которой желаете получить.");
                answ.Add("(Опционально)Укажите в сообщении город, чтобы данные были локальными, а не по всей России");
                answ.Add("(Опционально)Укажите в сообщении желаемое действие с валютой для добавления критерия сортировки");
                if (flag) answ.Add("command !гайд call");
                return true;
            }
            if (val1 == "!города")
            {
                for (int i = 1; i < 30; i++)
                {
                    answ.Add(regs[i].Item1);

                }
                if (flag) answ.Add("command !города call");
                return true;
            }
            if (val1.StartsWith("!город") && (val1.Length > 9))
            {
                v1 = val1.Split(' ', 2)[1];
                getReg();
                if (answ[0] == "Информация по Рф")
                    answ[0] = "Город \""+v1+"\" не найден в базе";
                else
                    answ[0] += " найден в базе";
                if (flag) answ.Add("command !город " +v1+" call");
                return true;
            }
            if (val1 == "!валюты")
            {
                for (int i = 0; i < 5; i++)
                {
                    answ.Add(val[i].Item1 + " или " + val[i].Item2);

                }
                if (flag) answ.Add("command !валюты call");
                return true;
            }
            if (val1 == "!цб")
            {
                answ.Add("Введите \"!цб *Валюта(3 символа)* *дата*\" для получения курса валюты определенного цб за введенную дату");
                answ.Add("Пример \"!цб EUR 11.11.2019\"");
                if (flag) answ.Add("command !цб call");
                return true;
            }
            if (val1.StartsWith("!цб") && (val1.Length == 18))
            {
                if (flag) answ.Add("command !цб " + val1.Substring(4, val1.Length - 4) + " call");
                searchDate(val1.Substring(4, val1.Length - 4));
                return true;
            }
            return false;

        }
        public string getVal(string val1)
        {
            if (flag) answ.Add("getVal func started with "+val1);
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
                        if (flag) answ.Add(val[t].Item1 + " " + val[t].Item2 + " (100 ед)");
                        sortn = val[t].Item3;
                    }
                    else
                    {
                        answ.Add(val[t].Item1 + " " + val[t].Item2);
                        if(flag) answ.Add(val[t].Item1 + " " + val[t].Item2);
                        sortn = val[t].Item3;
                    }
                    return v2;
                }
            }
            if (flag) answ.Add("getVall nothing found");
            return v2;
        }
        public string getReg()
        {
            if (flag) answ.Add("getReg func started");
            if (v1.ToLower().Contains(regs[1].Item1.Substring(0, regs[1].Item1.Length - 2).ToLower()))
            {
                r = regs[1].Item2;
                answ.Add("г. " + regs[1].Item1);
                return r;
            }
            if (v1.ToLower().Contains(regs[2].Item1.ToLower())|| v1.ToLower().Contains("питер") || v1.ToLower().Contains("санкт-петербург"))
            {
                r = regs[2].Item2;
                answ.Add("г. " + regs[2].Item1);
                return r;
            }
            if (v1.ToLower().Contains(regs[3].Item1.ToLower()) || v1.ToLower().Contains("екатеринбург"))
            {
                r = regs[2].Item2;
                answ.Add("г. " + regs[3].Item1);
                return r;
            }
            for (int t = 4; t < 21; t++)
            {
                if (v1.ToLower().Contains(regs[t].Item1.ToLower()))
                {
                    r = regs[t].Item2;
                    answ.Add("г. " + regs[t].Item1);
                    return r;
                }
            }
            for (int t = 21; t < 28; t++)
            {
                if (v1.ToLower().Contains(regs[t].Item1.Substring(0, regs[t].Item1.Length - 1).ToLower()))
                {
                    r = regs[t].Item2;
                    answ.Add("г. " + regs[t].Item1);
                    return r;
                }
            }
            if (v1.ToLower().Contains(regs[28].Item1.Substring(0, regs[28].Item1.Length - 1).ToLower()))
            {
                int z = v1.ToLower().LastIndexOf(regs[28].Item1.Substring(0, regs[28].Item1.Length - 1).ToLower()) + 2;
                if (v1.Length >= z + 1)
                {
                    if (v1[z] == 'а' || v1[z] == 'ы' || v1[z] == 'е' || v1[z] == 'е')
                    {
                        r = regs[28].Item2;
                        answ.Add("г. " + regs[28].Item1);
                        return r;
                    }
                }
                if (v1.Length >= z + 2)
                {
                    if (v1[z] == 'и' && v1[z + 1] == 'м')
                    {
                        r = regs[28].Item2;
                        answ.Add("г. " + regs[28].Item1);
                        return r;
                    }
                }
            }
            if (v1.ToLower().Contains(regs[29].Item1.Substring(7, regs[29].Item1.Length - 7).ToLower()))
            {
                r = regs[29].Item2;
                answ.Add("г. " + regs[29].Item1);
                return r;
            }
            answ.Add("Информация по Рф");
            if (flag) answ.Add("City not found");
            return r;
        }
        public int getSorttype()
        {
            if (flag) answ.Add("getSorttype func started");
            for (int t = 0; t < 8; t++)
            {
                if (v1.ToLower().Contains(sorters[t]))
                {
                    answ.Add("Результат отсортирован по ");
                    if (t <= 4)
                    {
                        answ.Add("цене продажи валюты на руки");
                    }
                    else
                    {
                        answ.Add("цене продажи валюты в банк");
                    }
                    return t;
                }
                
            }
            if (flag) answ.Add("Sorttype not found");
            return 100;
        }

        public string printResult()
        {
            if (flag) answ.Add("printResult func started");
            string res = "";
            if (answ.Count > 0)
            {
                for (int i = 0; i < answ.Count; i++)
                {
                    res += answ[i] + '\n';
                }
                return res;
            }
            if (flag) answ.Add("empty answer");
            return "Чтобы получить справку по командам напишите \"!help\"";
        }
        public void searchOth(string mess)
        {
            if (flag) answ.Add("searchOth func started");
            bool check = command(mess);
            if (check)
            {
                if (flag) answ.Add("bool for command == true");
            }
            else
            {
                if (flag) answ.Add("bool for command == false");
                string curVal = getVal(mess);
                int count = 0;
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
                    if (z <= 4 )
                    {
                        curUrl += "?sort=buy_course_"+sortn;
                    }
                    if (z > 4 && z <=8 )
                    {
                        curUrl += "?sort=-sell_course_"+sortn;
                    }
                    if (flag) answ.Add(curUrl);
                    CQ dom0 = CQ.CreateFromUrl(curUrl);
                    int tr = 0;
                    foreach (IDomObject obj in dom0.Find("td"))
                    {
                        if (tr == 0 && count < 5)
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
                            count++;
                            continue;
                        }
                    }
                }
            }
        }

        public void searchDate(string mess)
        {
            if (flag) answ.Add("searchDate func started with string "+mess);
            string urlMain = "http://www.cbr.ru/currency_base/daily/?UniDbQuery.Posted=True&UniDbQuery.To=";
            string[] inf = mess.Split(' ', 2);
            if (inf[0].Length != 3 && inf[1].Length != 10)
            {
                if (flag) answ.Add("wrong format for !цб search");
            }
            else
            {
                if(Convert.ToInt32(inf[1].Substring(6, inf[1].Length - 6)) < 1993)
                {
                    answ.Add("Курсы волют предоставляются начиная с 1993 года");
                }
                for (int t = 0; t < 5; t++)
                {
                    if (inf[0] == val[t].Item1)
                    {
                        v2 = val[t].Item1;
                        answ.Add("Курс " + val[t].Item2 + " (" + val[t].Item1 + ")");
                    }
                }
                string curUrl = urlMain + inf[1];
                if (flag) answ.Add(curUrl);
                CQ dom0 = CQ.CreateFromUrl(curUrl);
                string realDate = "";
                foreach (IDomObject obj in dom0.Find("button"))
                {
                    if(obj.ClassName== "datepicker-filter_button")
                    {
                        realDate = obj.Cq().Text();
                        answ.Add("За " + realDate + " от Цб");
                        break;
                    }

                }
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
                        if (realDate != inf[1])
                        {
                            answ.Add("Проверьте корректость введенных данных " + inf[1]);
                        }
                        break;
                    }
                }
            }
        }
    }
}
