using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsQuery;

namespace VkBot
{
    public class Search
    {

            Tuple<string, string>[] a = new Tuple<string, string>[6];

            public List<string> answ = new List<string>();
            public Search()
            {
            a[0] = new Tuple<string, string>("CHF", "Франк");
            a[1] = new Tuple<string, string>("JPY", "Йена");
            a[2] = new Tuple<string, string>("EUR", "Евро");
            a[3] = new Tuple<string, string>("GBP", "Фунт");
            a[4] = new Tuple<string, string>("USD", "Доллар");
            a[5] = new Tuple<string, string>("CNY", "Юань");
            }

            public void searchSPB(string val)
            {
                string s1 = val;
                string s2 = "";
                bool flag = false;
                for (int t = 0; t < 6; t++)
                {
                    if (s1 == a[t].Item1)
                    {
                        s2 = a[t].Item2;
                        flag = true;
                    }

                }
                if (flag)
                {

                    string[] ans = new string[5];
                    CQ dom = CQ.CreateFromUrl("https://www.bspb.ru/cash-rates/#");
                    int i = 0;
                    foreach (IDomObject obj in dom.Find("td"))
                    {
                        if (i < 0)
                        {
                            break;
                        }
                        if (i == 0)
                        {
                            if (obj.InnerText == (val))
                            {
                                i = 6;
                            }
                        }
                        else
                        {
                            switch (i)
                            {
                                case 1:
                                    if (s1 == "CNY")
                                    {
                                        answ.Insert(2, "Курс ЦБ " + (Convert.ToDouble(obj.InnerText) / 10).ToString());
                                    }
                                    else
                                    {
                                        answ.Insert(2, "Курс ЦБ " + obj.InnerText);
                                    }
                                    i = i - 2;
                                    break;
                                case 2:
                                    if (s1 == "CNY")
                                    {
                                        answ.Add((Convert.ToDouble(obj.InnerText) / 10).ToString());
                                    }
                                    else
                                    {
                                        answ.Add(obj.InnerText);
                                    }
                                    i--;
                                    break;
                                case 3:
                                    answ.Add("bankspb");
                                    if (s1 == "CNY")
                                    {
                                        answ.Add((Convert.ToDouble(obj.InnerText) / 10).ToString());
                                    }
                                    else
                                    {
                                        answ.Add(obj.InnerText);
                                    }
                                    i--;
                                    break;
                                case 4:
                                    if (s1 == "CNY")
                                    {
                                        answ.Add("Цена за " + (Convert.ToInt32(obj.InnerText) / 10) + " шт");
                                    }
                                    else
                                    {
                                        answ.Add("Цена за " + obj.InnerText + " шт");
                                    }
                                    i--;
                                    break;
                                case 5:
                                    answ.Add(s2);
                                    i--;
                                    break;
                                case 6:
                                    i--;
                                    break;
                                default:
                                    break;

                            }
                        }


                    }
                }
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
                    return "Введи нормальное что-то \nВот список: CHF, JPY, EUR, GBP, USD, CNY";
                }
            }
            public void searchOth(string val)
            {

                string[] urls = new string[5];
                urls[0] = "https://ru.myfin.by/bank/sberbank/currency/sankt-peterburg";
                urls[1] = "https://ru.myfin.by/bank/vtb/currency/sankt-peterburg";
                urls[2] = "https://ru.myfin.by/bank/gazprombank/currency/sankt-peterburg";
                urls[3] = "https://ru.myfin.by/bank/alfabank/currency/sankt-peterburg";
                urls[4] = "https://ru.myfin.by/bank/mkb/currency/sankt-peterburg";
                string s1 = val;
                string s2 = "";
                for (int t = 0; t < 6; t++)
                {
                    if (s1 == a[t].Item1)
                    {
                        s2 = a[t].Item2;
                    }

                }
                for (int j = 0; j < urls.Length; j++)
                {
                    //string[] ans2 = new string[3];
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
                                    answ.Add(urls[j]);
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
