using Aztamlider.Data.Datacontext;
using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.User;
using Aztamlider.Services.Resources;
using Aztamlider.Services.Services.Interfaces;
using Aztamlider.Services.Services.Interfaces.User;
using MailKit;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace Aztamlider.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private LanguageService _localization;
        private readonly IContactUsCreateServices _contactUsCreateServices;
        private readonly IEmailServices _emailServices;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IHomeIndexServices _homeIndexServices;

        public HomeController(LanguageService localization, IContactUsCreateServices contactUsCreateServices, IEmailServices emailServices, IStringLocalizerFactory stringLocalizerFactory, IHomeIndexServices homeIndexServices)
        {
            _localization = localization;
            _contactUsCreateServices = contactUsCreateServices;
            _emailServices = emailServices;
            _stringLocalizerFactory = stringLocalizerFactory;
            _homeIndexServices = homeIndexServices;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            try
            {
                homeViewModel = new HomeViewModel
                {
                    LanguageBases = await _homeIndexServices.GetLanguageBase(),
                    MainSliders = await _homeIndexServices.MainSliders(),
                    Partners = await _homeIndexServices.Partners(),

                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", homeViewModel);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", homeViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(homeViewModel);
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}