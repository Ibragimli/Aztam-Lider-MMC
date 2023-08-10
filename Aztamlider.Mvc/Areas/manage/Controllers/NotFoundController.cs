using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]

    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
