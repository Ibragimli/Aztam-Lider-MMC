using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.User;
using Aztamlider.Services.Helper;
using Aztamlider.Services.Services.Interfaces.User;
using Aztamlider.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Controllers
{
    public class ElaqeController : Controller
    {
        private readonly IContactUsCreateServices _contactUsCreateServices;
        private readonly IEmailServices _emailServices;
        private readonly IContactUsIndexServices _contactUsIndexServices;

        public ElaqeController(IContactUsCreateServices contactUsCreateServices, IEmailServices emailServices, IContactUsIndexServices contactUsIndexServices)
        {
            _contactUsCreateServices = contactUsCreateServices;
            _emailServices = emailServices;
            _contactUsIndexServices = contactUsIndexServices;
        }
        public async Task<IActionResult> Index()
        {

            ContactUsViewModel contactUsVM = new ContactUsViewModel();
            try
            {
                contactUsVM = new ContactUsViewModel
                {
                    LanguageBases = await _contactUsIndexServices.GetLanguageBase(),
                    Settings = await _contactUsIndexServices.GetSettings(),
                    ContactUsCreateDto = new ContactUsCreateDto(),
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", contactUsVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", contactUsVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(contactUsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsCreateDto contactUsCreateDto)
        {
            ContactUsViewModel contactUsVM = new ContactUsViewModel();

            try
            {
                contactUsVM = new ContactUsViewModel
                {
                    LanguageBases = await _contactUsIndexServices.GetLanguageBase(),
                    Settings = await _contactUsIndexServices.GetSettings(),
                    ContactUsCreateDto = new ContactUsCreateDto(),
                };

                await _contactUsCreateServices.ValuesCheck(contactUsCreateDto);



                await _contactUsCreateServices.PhoneNumberCheck(contactUsCreateDto.PhoneNumber);
                await _contactUsCreateServices.EmailCheck(contactUsCreateDto.Email);
                await _contactUsCreateServices.ContactUsCreate(contactUsCreateDto);


                //await _emailServices.Send("info@aztamlider.az", "Aztamlider elaqe mesaji", body);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return View("index", contactUsVM);
            }
            catch (ItemFormatException ex)
            {
                TempData["Error"] = (ex.Message);
                return View("index", contactUsVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            TempData["Success"] = ("Müraciətiniz göndərildi");
            return RedirectToAction("index", "elaqe");
        }

    }
}
