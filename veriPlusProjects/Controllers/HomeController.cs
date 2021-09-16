using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using veriPlusProjects.Extensions;

namespace veriPlusProjects.Controllers
{
    public class HomeController : Controller
    {
        public List<string> StringList { get; set; }
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
      
        [HttpGet]
        public IActionResult Index(string Search)
        {
            int CountingSearch = 0;

            if (Request.Cookies["CountingSeach"] == null)
                Response.Cookies.Append("CountingSeach", CountingSearch.ToString());
            else
                CountingSearch = Convert.ToInt32(Request.Cookies["CountingSeach"]);

            if (string.IsNullOrWhiteSpace(Search))
                return View("Index");
            else
            {
                var Url = OmdbConf.BaseUrl + Search + "&apikey=" + OmdbConf.ApiKey;
                var JsonClient = new WebClient().DownloadString(Url);
                var OmdbDatas = JsonConvert.DeserializeObject<OmdbModel>(JsonClient);

                StringList = new List<string>();
                for (int i = 0; i < CountingSearch + 1; i++)
                {
                    var Searchval = Request.Cookies["search_" + i.ToString()];
                    if (Searchval != null)
                        StringList.Add(Searchval);
                    else
                    {
                        if (StringList.Contains(Search))
                        {
                            var ListIndex = StringList.IndexOf(Search);
                            string Val = null;

                            for (int x = ListIndex; x < CountingSearch; x++)
                            {
                                if (StringList[x] == Search)
                                    Val = StringList[x];
                                if (x < CountingSearch - 1 && CountingSearch > 1)
                                {
                                    StringList[x] = StringList[x + 1];
                                    Response.Cookies.Append("search_" + (x).ToString(), StringList[x + 1]);
                                }
                            }
                            StringList[CountingSearch - 1] = Val;
                            Response.Cookies.Append("search_" + (CountingSearch - 1).ToString(), Search);
                            break;
                        }
                        if (CountingSearch < 5)
                        {
                            CountingSearch++;
                            Response.Cookies.Append("search_" + i.ToString(), Search);
                            StringList.Add(Search);
                            Response.Cookies.Append("CountingSeach", (i + 1).ToString());
                        }
                        else
                        {
                            for (int x = 0; x < 4; x++)
                            {
                                StringList[x] = StringList[x + 1];
                                Response.Cookies.Append("search_" + (x).ToString(), StringList[x + 1]);
                            }
                            StringList[4] = Search;
                            Response.Cookies.Append("search_" + 4, Search);
                        }

                    }

                }
                
                var OmdbModel = new OmdbModel()
                {
                    Actors = OmdbDatas.Actors,
                    Awards = OmdbDatas.Awards,
                    BoxOffice = OmdbDatas.BoxOffice,
                    Country = OmdbDatas.Country,
                    Director = OmdbDatas.Director,
                    DVD = OmdbDatas.DVD,
                    Genre = OmdbDatas.Genre,
                    imdbID = OmdbDatas.imdbID,
                    imdbRating = OmdbDatas.imdbRating,
                    imdbVotes = OmdbDatas.imdbVotes,
                    Language = OmdbDatas.Language,
                    Metascore = OmdbDatas.Metascore,
                    Plot = OmdbDatas.Plot,
                    Poster = OmdbDatas.Poster,
                    Production = OmdbDatas.Production,
                    Rated = OmdbDatas.Rated,
                    Ratings = OmdbDatas.Ratings,
                    Released = OmdbDatas.Released,
                    Response = OmdbDatas.Response,
                    Runtime = OmdbDatas.Runtime,
                    Title = OmdbDatas.Title,
                    Type = OmdbDatas.Type,
                    Website = OmdbDatas.Website,
                    Writer = OmdbDatas.Writer,
                    Year = OmdbDatas.Year,
                    StringList = StringList,
                    Search = Search,
                    CountingSeach = CountingSearch
                };

                return View("Search", OmdbModel);
            }

        }
        public IActionResult DeleteSearchCookies()
        {
            Response.Cookies.Delete("search_0");
            Response.Cookies.Delete("search_1");
            Response.Cookies.Delete("search_2");
            Response.Cookies.Delete("search_3");
            Response.Cookies.Delete("search_4");
            Response.Cookies.Delete("CountingSeach");

            return Redirect("/");
        }
        
    }
}
