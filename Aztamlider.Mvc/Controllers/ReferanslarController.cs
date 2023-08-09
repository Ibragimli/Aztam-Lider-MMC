using Aztamlider.Core.Entites;
using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Helper;
using Aztamlider.Services.Services.Interfaces.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Aztamlider.Mvc.Controllers
{
    public class ReferanslarController : Controller
    {
        private readonly IReferenceIndexServices _referenceIndexServices;

        public ReferanslarController(IReferenceIndexServices referenceIndexServices)
        {
            _referenceIndexServices = referenceIndexServices;
        }
        public async Task<IActionResult> Index(int page = 1, int serviceId = 0)
        {

            ReferenceViewModel referenceIndexVM = new ReferenceViewModel();
            try
            {
                ViewBag.Page = page;
                if (serviceId != 0)
                    ViewBag.ServiceId = serviceId;
                
                referenceIndexVM = new ReferenceViewModel
                {
                    LanguageBases = await _referenceIndexServices.GetLanguageBase(),
                    ReferenceImage = await _referenceIndexServices.GetReferenceImages(),
                    References = PagenetedList<Reference>.Create(_referenceIndexServices.GetReferences(serviceId), page, 9),
                    Settings = await _referenceIndexServices.GetSettings(),
                    ServiceTypes = await _referenceIndexServices.GetServiceTypes(),
                    Services = await _referenceIndexServices.GetServices(),
                    ServiceNames = await _referenceIndexServices.GetServiceNames(),
                    ReferencesCount = await _referenceIndexServices.GetReferencesCount()
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return View("index", referenceIndexVM);

            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return View("index", referenceIndexVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(referenceIndexVM);
        }
        public async Task<IActionResult> Referans(int id)
        {

            ReferenceViewModel referenceIndexVM = new ReferenceViewModel();
            try
            {
                referenceIndexVM = new ReferenceViewModel
                {
                    LanguageBases = await _referenceIndexServices.GetLanguageBase(),
                    ReferenceImage = await _referenceIndexServices.GetReferenceImages(),
                    Reference = await _referenceIndexServices.GetReference(id),
                    Settings = await _referenceIndexServices.GetSettings(),
                    ServiceTypes = await _referenceIndexServices.GetServiceTypes(),
                    ServiceNames = await _referenceIndexServices.GetServiceNames(),
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", referenceIndexVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", referenceIndexVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(referenceIndexVM);
        }
    }
}
