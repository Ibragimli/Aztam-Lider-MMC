using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin")]

    public class LoggerController : Controller
    {
        private readonly IAdminLoggerIndexServices _adminLoggerIndexServices;

        public LoggerController(IAdminLoggerIndexServices adminLoggerIndexServices)
        {
            _adminLoggerIndexServices = adminLoggerIndexServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            LoggerIndexViewModel LoggerIndexVM = new LoggerIndexViewModel();
            try
            {
                var Logger = _adminLoggerIndexServices.GetLogger(name);
                LoggerIndexVM = new LoggerIndexViewModel
                {
                    Loggers = PagenetedList<Logger>.Create(Logger, page, 5),
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
            return View(LoggerIndexVM);
        }
    }
}
