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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsCreateDto contactUsCreateDto, int newItemId = 2)
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            try
            {
                homeViewModel = new HomeViewModel
                {

                };

                //await _contactUsCreateServices.ValuesCheck(contactUsCreateDto);

                //Email
                string body = string.Empty;

                using (StreamReader reader = new StreamReader("wwwroot/templates/contactEmail.html"))
                {
                    body = reader.ReadToEnd();
                }

                //await _contactUsCreateServices.PhoneNumberCheck(contactUsCreateDto.PhoneNumber);
                //await _contactUsCreateServices.EmailCheck(contactUsCreateDto.Email);
                //await _contactUsCreateServices.ContactUsCreate(contactUsCreateDto);

                //body = body.Replace("{{phonenumber}}", contactUsCreateDto.PhoneNumber);
                //body = body.Replace("{{fullname}}", contactUsCreateDto.Fullname);
                //body = body.Replace("{{email}}", contactUsCreateDto.Email);
                //body = body.Replace("{{message}}", contactUsCreateDto.Message);
                //await _emailServices.Send("info@Aztamlider.az", "Xarıbulbul elaqe mesaji", body);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", homeViewModel);
            }
            catch (ItemFormatException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", homeViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            TempData["Success"] = ("Məktub göndərildi");
            return View("index", homeViewModel);
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