using Microsoft.AspNetCore.Http;
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
        string[] SearchList = new string[5];

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index()
        {
            DeleteSearchCookies();
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

                /*
                CookieOptions Cookie = new CookieOptions();
                Cookie.Expires = DateTime.Now.AddDays(90);*/

                //SearchList = new List<string>();
                //DeleteSearchCookies();
                /*
                if (Request.Cookies.Count < 6)
                    Response.Cookies.Append("search_0", Search);
                else if(Request.Cookies.Count < 7)*/

                for (int i = 0; i < Request.Cookies.Count - 4; i++)
                {
                    var val = Request.Cookies["search_" + i.ToString()];
                    if (val != null)
                        SearchList[i] = val;
                    else
                    {
                        if (SearchList[4]== null)
                        {
                            Response.Cookies.Append("search_" + i.ToString(), Search);
                            SearchList[i] = Search;
                        }
                        else
                        {
                            for (int x = 0; x < 4; x++)
                            {
                                SearchList[x] = SearchList[x + 1];
                                Response.Cookies.Append("search_" + (x).ToString(), SearchList[x+1]);
                            }
                            Response.Cookies.Append("search_" + 4, Search);
                            SearchList[4] = Search;
                        }

                    }

                }
                TestModel testModel = new TestModel();

                var SearchCookiesVM = new SearchCookies()
                {
                    Search_0 = SearchList[0],
                    Search_1 = SearchList[1],
                    Search_2 = SearchList[2],
                    Search_3 = SearchList[3],
                    Search_4 = SearchList[4]
                };
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
                OmdbTestModel.Searches = SearchCookiesVM;

                return View("Search", OmdbTestModel);
            }

        }
        public void DeleteSearchCookies()
        {
            Response.Cookies.Delete("search_0");
            Response.Cookies.Delete("search_1");
            Response.Cookies.Delete("search_2");
            Response.Cookies.Delete("search_3");
            Response.Cookies.Delete("search_4");
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
