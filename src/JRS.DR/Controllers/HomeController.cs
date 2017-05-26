using Microsoft.AspNetCore.Mvc;

namespace JRS.DR.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Auth", "Admin");
        }
    }
}