using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
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

        [HttpPost]
        public JsonResult MovieSearching(OmdbModel omdbModel)
        {
            return Json(omdbModel);
        }
        public JsonResult Search(string Search)
        {
            /*
            _search = Search;
            var Url = OmdbConf.BaseUrl + Search + "&apikey=" + OmdbConf.ApiKey;
            var JsonClient = new WebClient().DownloadString(Url);
            var OmdbModel = JsonConvert.DeserializeObject<OmdbModel>(JsonClient);
            */


            return Json(Search);
        }

        [HttpGet]
        public IActionResult Index(string Search)
        {
            if (string.IsNullOrWhiteSpace(Search))
                return View("Index");
            else
            {

                /*
                var Url = OmdbConf.BaseUrl + Search + "&apikey=" + OmdbConf.ApiKey;
                var JsonClient = new WebClient().DownloadString(Url);
                var OmdbModel = JsonConvert.DeserializeObject<OmdbModel>(JsonClient);*/
                TestModel testModel = new TestModel();
                var OmdbTestModel = new OmdbModel()
                {
                    Actors = testModel.Actors,
                    Awards = testModel.Awards,
                    BoxOffice = testModel.BoxOffice,
                    Country = testModel.Country,
                    Director = testModel.Director,
                    DVD = testModel.DVD,
                    Genre = testModel.Genre,
                    imdbID = testModel.imdbID,
                    imdbRating = testModel.imdbRating,
                    imdbVotes = testModel.imdbVotes,
                    Language = testModel.Language,
                    Metascore = testModel.Metascore,
                    Plot = testModel.Plot,
                    Poster = testModel.Poster,
                    Production = testModel.Production,
                    Rated = testModel.Rated,
                    Ratings = testModel.Ratings,
                    Released = testModel.Released,
                    Response = testModel.Response,
                    Runtime = testModel.Runtime,
                    Title = testModel.Title,
                    Type = testModel.Type,
                    Website = testModel.Website,
                    Writer = testModel.Writer,
                    Year = testModel.Year
                };
                return View("Search", OmdbTestModel);
            }

        }

        public class TestModel
        {
            public string Title = "Star Wars";
            public string Year = "1977";
            public string Rated = "PG";
            public DateTime Released = new DateTime(1977, 05, 25);
            public string Runtime = "121 min";
            public string Genre = "Action, Adventure, Fantasy";
            public string Director = "George Lucas";
            public string Writer = "George Lucas";
            public string Actors = "Mark Hamill, Harrison Ford, Carrie Fisher";
            public string Plot = "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader";
            public string Language = "English";
            public string Country = "United States, United Kingdom";
            public string Awards = "Won 7 Oscars. 63 wins & 29 nominations total";
            public string Poster = "https://m.media-amazon.com/images/M/MV5BNzVlY2MwMjktM2E4OS00Y2Y3LWE3ZjctYzhkZGM3YzA1ZWM2XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg";
            public List<OmdbRatings> Ratings { get; set; }
            public int Metascore = 90;
            public double imdbRating = 8.6;
            public string imdbVotes = "1,271,153";
            public string imdbID = "tt0076759";
            public string Type = "movie";
            public DateTime DVD = new DateTime(2005, 12, 06);
            public string BoxOffice = "$460,998,507";
            public string Production = "Lucasfilm Ltd.";
            public string Website = "N/A";
            public bool Response = true;
        }

    }
}
