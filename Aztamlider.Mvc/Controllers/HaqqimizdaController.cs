using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Services.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Controllers
{
    public class HaqqimizdaController : Controller
    {
        private readonly IAboutUsIndexServices _aboutUsIndexServices;

        public HaqqimizdaController(IAboutUsIndexServices aboutUsIndexServices)
        {
            _aboutUsIndexServices = aboutUsIndexServices;
        }
        public async Task<IActionResult> Index()
        {

            AboutUsViewModel aboutUsVM = new AboutUsViewModel();
            try
            {
                aboutUsVM = new AboutUsViewModel
                {
                    LanguageBases = await _aboutUsIndexServices.GetLanguageBase(),
                    Settings = await _aboutUsIndexServices.GetSettings(),
                    Services = await _aboutUsIndexServices.GetServices(),
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", aboutUsVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", aboutUsVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(aboutUsVM);
        }
    }
}
