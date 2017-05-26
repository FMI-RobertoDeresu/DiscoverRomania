using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace JRS.DR.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Code(string id)
        {
            var customHttpStatuCodesPages = new[] { "400", "401", "403", "404", "500" };

            return customHttpStatuCodesPages.Any(x => x == id)
                ? View(id)
                : View("Index");
        }
    }
}