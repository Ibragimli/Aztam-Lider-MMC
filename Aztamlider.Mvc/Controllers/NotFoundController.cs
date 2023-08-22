
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
