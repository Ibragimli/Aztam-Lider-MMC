using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Aztamlider.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}