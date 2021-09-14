using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace veriPlusProjects.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            Response.Cookies.Delete("search_0");
            Response.Cookies.Delete("search_1");
            Response.Cookies.Delete("search_2");
            Response.Cookies.Delete("search_3");
            Response.Cookies.Delete("search_4");
            Response.Cookies.Delete("search_5");

            return Redirect("/");
        }
    }
}
