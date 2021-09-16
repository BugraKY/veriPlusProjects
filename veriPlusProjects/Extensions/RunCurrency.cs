using System;
using System.Threading;
using System.Xml;

namespace veriPlusProjects.Extensions
{
    public class RunCurrency
    {

        static DayOfWeek Dow;
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
            Currency.INSTALLATION_DATE = DateTime.Now.Date.Add(new TimeSpan(8, 59, 59)).AddDays(1);
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                Currency.DATE = DateTime.Now.Date.Add(new TimeSpan(9, 0, 0)).AddDays(-1);
                Currency.INSTALLATION_DATE = DateTime.Now.Date.Add(new TimeSpan(8, 59, 59)).AddDays(2);
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                Currency.DATE = DateTime.Now.Date.Add(new TimeSpan(9, 0, 0)).AddDays(-2);
            else
                Currency.DATE = DateTime.Now;

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

                Dow = DateTime.Now.DayOfWeek;
                DateTime CheckHour = DateTime.Now.Date.Add(new TimeSpan(8, 59, 59));

                if (Currency.NonFirstSooh == false)
                {
                    Currency.NonFirstSooh = true;
                    GetCurrencyLive();
                }
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
