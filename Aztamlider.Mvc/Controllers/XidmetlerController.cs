using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Services.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Controllers
{
    public class XidmetlerController : Controller
    {
        private readonly IServiceIndexServices _serviceIndexServices;

        public XidmetlerController(IServiceIndexServices serviceIndexServices)
        {
            _serviceIndexServices = serviceIndexServices;
        }
        public async Task<IActionResult> Index()
        {

            ServicesIndexViewModel serviceIndexVM = new ServicesIndexViewModel();
            try
            {
                serviceIndexVM = new ServicesIndexViewModel
                {
                    LanguageBases = await _serviceIndexServices.GetLanguageBase(),
                    Services = await _serviceIndexServices.GetServices(),
                    Settings = await _serviceIndexServices.GetSettings(),
                    ServiceImages = await _serviceIndexServices.GetServiceImages(),
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", serviceIndexVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", serviceIndexVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(serviceIndexVM);
        }
    }
}
