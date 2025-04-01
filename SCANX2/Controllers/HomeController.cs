using Microsoft.AspNetCore.Mvc;

namespace SCANX2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Pricing()
        {
            return View();
        }

    }
}
