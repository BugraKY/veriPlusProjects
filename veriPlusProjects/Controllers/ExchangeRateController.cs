using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using veriPlusProjects.Extensions;

namespace veriPlusProjects.Controllers
{
    public class ExchangeRateController : Controller
    {
        public IActionResult Index()
        {
            RunCurrency.CheckClock();
            return View();
        }
        /*
        [HttpGet]
        public JsonResult GetResult()
        {
            var ExcRate = "http://www.tcmb.gov.tr/kurlar/today.xml";
            XmlDocument Xml = new XmlDocument();
            Xml.Load(ExcRate);

            var Usd = Xml.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/ForexSelling").InnerXml;
            var Eur = Xml.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/ForexSelling").InnerXml;
            var Gbp = Xml.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/ForexSelling").InnerXml;

            Currency Currencies = new Currency()
            {
                USD = Usd,
                EUR = Eur,
                GBP = Gbp

            };
            return new JsonResult { Data = Currencies, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }*/
    }
}
