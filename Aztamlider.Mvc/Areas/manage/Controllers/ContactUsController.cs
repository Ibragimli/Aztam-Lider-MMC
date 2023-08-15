using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Helper;
using Aztamlider.Services.Services.Interfaces.Area.ContactUs;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Viewer")]

    public class ContactUsController : Controller
    {
        private readonly IAdminContactUsIndexServices _adminContactUsIndexServices;

        public ContactUsController(IAdminContactUsIndexServices adminContactUsIndexServices)
        {
            _adminContactUsIndexServices = adminContactUsIndexServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            ContactUsIndexViewModel ContactUsIndexVM = new ContactUsIndexViewModel();
            try
            {
                var contactUs = _adminContactUsIndexServices.GetPoster(name);
                ContactUsIndexVM = new ContactUsIndexViewModel
                {
                    ContactUs = PagenetedList<ContactUs>.Create(contactUs, page, 5),
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
            return View(ContactUsIndexVM);
        }

    }
}
