using Microsoft.AspNetCore.Mvc;
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
    }
}
