using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace veriPlusProjects.Extensions
{
    public class RunCurrency
    {
        //Currency currency;
        //DateTime endDate = DateTime.Now.AddDays(1).AddSeconds(-1);
        //Timer GetTimer = new Timer();
        static void GetCurrencyLive()
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

            do
            {
                int Interval;
                int MinRemaining = 59 - DateTime.Now.Minute;
                int SecRemaining = 59 - DateTime.Now.Second;
                Interval = ((MinRemaining * 60) + SecRemaining) * 1000;

                do
                {
                    if (Interval < 1)
                        Interval = 120000;
                }
                while (Interval < 1);

                DayOfWeek Dow = DateTime.Now.DayOfWeek;
                DateTime CheckHour = DateTime.Now.Date.Add(new TimeSpan(8, 59, 59));

                if (!(Dow == DayOfWeek.Saturday || Dow == DayOfWeek.Sunday))
                {

                    if (DateTime.Now > Currency.INSTALLATION_DATE && DateTime.Now > CheckHour)
                    {
                        if (DateTime.Now.Hour == 9)
                        {
                            GetCurrencyLive();
                        }
                    }
                }
                Thread.Sleep(Interval);
            }
            while (true);


        }

        public static void StartThread()
        {
            Thread _THREAD = new Thread(new ThreadStart(CheckClock));
            _THREAD.Start();
        }


    }
}
