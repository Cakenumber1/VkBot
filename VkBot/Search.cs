using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using CsQuery;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VkBot
{
    public class Search
    {

            Tuple<string, string>[] a = new Tuple<string, string>[5];

            public List<string> answ = new List<string>();
            public Search()
            {
            a[0] = new Tuple<string, string>("CHF", "Франк");
            a[1] = new Tuple<string, string>("JPY", "Йена");
            a[2] = new Tuple<string, string>("EUR", "Евро");
            a[3] = new Tuple<string, string>("GBP", "Фунт");
            a[4] = new Tuple<string, string>("USD", "Доллар");
            }


        public string printResult()
            {
                string res = "";
                if (answ.Count > 4)
                {
                    for (int i = 0; i < answ.Count; i++)
                    {
                        res += answ[i] + '\n';
                    }
                return res;
                }
                else
                {
                    return "Введи нормальное что-то \nВот список: CHF, JPY, EUR, GBP, USD";
                }
            }
        public void searchOth(string val)
        {

            string[] urls = new string[6];
            urls[0] = "https://ru.myfin.by/bank/sberbank/currency/sankt-peterburg";
            urls[1] = "https://ru.myfin.by/bank/vtb/currency/sankt-peterburg";
            urls[2] = "https://ru.myfin.by/bank/gazprombank/currency/sankt-peterburg";
            urls[3] = "https://ru.myfin.by/bank/alfabank/currency/sankt-peterburg";
            urls[4] = "https://ru.myfin.by/bank/mkb/currency/sankt-peterburg";
            urls[5] = "https://ru.myfin.by/bank/bspb/currency/sankt-peterburg";
            string s1 = val;
            string s2 = "";
            bool flag = false;
            for (int t = 0; t < 5; t++)
            {
                if (s1 == a[t].Item1)
                {
                    s2 = a[t].Item2;
                    flag = true;
                }

            }
            if (flag)
            {
                CQ dom0 = CQ.CreateFromUrl("https://ru.myfin.by/bank/sberbank/currency/sankt-peterburg");
                int tr = 0;
                foreach (IDomObject obj in dom0.Find("td"))
                {
                    if (tr == 0)
                    {
                        if (obj.InnerHTML.Contains(s1.ToLower()))
                        {
                            tr = 3;
                        }
                    }
                    if (tr > 1)
                    {
                        tr--;
                    }
                    if (tr == 1)
                    {
                        answ.Add("Курс Цб:\n" + obj.InnerText);
                        break;
                    }

                }
                for (int j = 0; j < urls.Length; j++)
                {
                    
                    CQ dom = CQ.CreateFromUrl(urls[j]);
                    int i = 0;
                    foreach (IDomObject obj in dom.Find("td"))
                    {
                        if (i < 0)
                        {
                            break;
                        }
                        if (i == 0)
                        {
                            if (obj.InnerHTML.Contains(s1.ToLower()))
                            {
                                i = 2;
                            }
                        }
                        else
                        {
                            switch (i)
                            {
                                case 1:
                                    answ.Add(obj.InnerText);
                                    i = i - 2;
                                    break;
                                case 2:
                                    string[] qq = urls[j].Split('/');
                                    answ.Add(qq[4].ToUpper());
                                    answ.Add(obj.InnerText);
                                    i--;
                                    break;
                                default:
                                    break;

                            }
                        }


                    }

                }
            }
        }
        
        
    }
}
