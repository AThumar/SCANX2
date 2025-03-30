using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();  // ✅ Loads Dashboard.cshtml
    }
}
