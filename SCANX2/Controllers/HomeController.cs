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
        public IActionResult ViewPDF(string fileName)
        {
            ViewBag.FilePath = Url.Content("~/pdfs/" + fileName); // Ensure the PDF exists in /wwwroot/pdfs/
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Features()
        {
            return View();
        }
    }
}
