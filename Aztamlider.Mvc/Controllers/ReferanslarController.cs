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
        public async Task<IActionResult> Tamamlanmislar(int page = 1, int serviceId = 0)
        {
            ReferenceViewModel referenceIndexVM = new ReferenceViewModel();
            try
            {
                ViewBag.Page = page;
                if (serviceId != 0)
                    ViewBag.ServiceId = serviceId;
                ViewBag.ServiceActiveId = serviceId;
                referenceIndexVM = new ReferenceViewModel
                {
                    LanguageBases = await _referenceIndexServices.GetLanguageBase(),
                    ReferenceImage = await _referenceIndexServices.GetReferenceImages(),
                    ReferencesCompleted = PagenetedList<Reference>.Create(_referenceIndexServices.GetReferencesCompleted(serviceId), page, 15),
                    Settings = await _referenceIndexServices.GetSettings(),
                    ServiceTypes = await _referenceIndexServices.GetServiceTypes(),
                    Services = await _referenceIndexServices.GetServices(),
                    ServiceNames = await _referenceIndexServices.GetServiceNames(),
                    ReferencesCount = await _referenceIndexServices.GetReferencesCompletedCount()
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
        public async Task<IActionResult> Diger(int page = 1, int serviceId = 0)
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
                    ReferencesOthers = PagenetedList<Reference>.Create(_referenceIndexServices.GetReferencesOthers(serviceId), page, 15),
                    Settings = await _referenceIndexServices.GetSettings(),
                    ServiceTypes = await _referenceIndexServices.GetServiceTypes(),
                    Services = await _referenceIndexServices.GetServices(),
                    ServiceNames = await _referenceIndexServices.GetServiceNames(),
                    ReferencesCount = await _referenceIndexServices.GetReferencesOtherCount()
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
