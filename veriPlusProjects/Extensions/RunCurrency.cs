using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace veriPlusProjects.Extensions
{
    public class RunCurrency
    {
        //Currency currency;
        //DateTime endDate = DateTime.Now.AddDays(1).AddSeconds(-1);
        static void GetCurrency()
        {
            var ExcRate = "http://www.tcmb.gov.tr/kurlar/today.xml";
            XmlDocument Xml = new XmlDocument();
            Xml.Load(ExcRate);

            var Usd = Xml.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/ForexSelling").InnerXml;
            var Eur = Xml.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/ForexSelling").InnerXml;
            var Gbp = Xml.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/ForexSelling").InnerXml;

            Currency.USD = Usd;
            Currency.EUR = Eur;
            Currency.GBP = Gbp;
            Currency.DATE = DateTime.Now;
            Currency.INSTALLATION_DATE = DateTime.Now.Date.Add(new TimeSpan(8, 59, 59)).AddDays(1);

        }

        public static void CheckClock()
        {
            DayOfWeek Dow = DateTime.Now.DayOfWeek;
            //DateTime Hour = DateTime.Now.Date.Add(new TimeSpan(8, 59, 59));
            DateTime CheckHour = DateTime.Now.Date.Add(new TimeSpan(8, 59, 59));
            if (!(Dow == DayOfWeek.Saturday || Dow == DayOfWeek.Sunday))
            {
                if (Currency.DATE.Hour == 0)
                {
                    GetCurrency();
                }
                else
                {

                    if (DateTime.Now > Currency.INSTALLATION_DATE && DateTime.Now > CheckHour)
                    {

                        GetCurrency();
                    }
                    else
                    {
                        //varolan kur verisini göster.
                    }
                }
            }



        }

    }
}
