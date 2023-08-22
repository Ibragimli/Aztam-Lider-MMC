using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Helper;
using Aztamlider.Services.Services.Interfaces.Area.Careers;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Viewer")]

    public class CareerController : Controller
    {
        private readonly IAdminCareerIndexServices _adminCareerIndexServices;

        public CareerController(IAdminCareerIndexServices adminCareerIndexServices)
        {
            _adminCareerIndexServices = adminCareerIndexServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            CareerIndexViewModel CareerIndexVM = new CareerIndexViewModel();
            try
            {
                var Career = _adminCareerIndexServices.GetPoster(name);
                CareerIndexVM = new CareerIndexViewModel
                {
                    Careers = PagenetedList<Career>.Create(Career, page, 10),
                };
            }
            catch (NotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }

            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(CareerIndexVM);
        }

    }
}
