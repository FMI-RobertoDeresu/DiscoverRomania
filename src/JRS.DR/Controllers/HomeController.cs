using Microsoft.AspNetCore.Mvc;

namespace JRS.DR.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}