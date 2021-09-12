using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using veriPlusProjects.Extensions;

namespace veriPlusProjects.Controllers
{
    public class HomeController : Controller
    {
        public string _search;
        private readonly IHttpClientFactory _clientFactory;
        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetCurrencyExc()
        {
            var GetCurrency = new CurrencyVM
            {
                USD = Currency.USD,
                EUR = Currency.EUR,
                GBP = Currency.GBP,
                DATE = Currency.DATE,
                INSTALLATION_DATE = Currency.INSTALLATION_DATE
            };
            return Json(GetCurrency);
        }
        [HttpGet]
        public JsonResult MovieSearching(string Search)
        {
            _search = Search;
            WebEngine();
            return Json(data);
        }

        public async Task WebEngine()
        {
            var HttpClient = HttpClientFactory.Create();

            //var Url = OmdbConf.BaseUrl + search + "apikey=" + OmdbConf.ApiKey;

            var Url = "https://www.google.com.tr";

            var Data = HttpClient.GetStringAsync(Url);
        }

    }
}
