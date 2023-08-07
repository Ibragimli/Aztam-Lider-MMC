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
        public async Task<IActionResult> Index(int page = 1)
        {

            ReferenceViewModel referenceIndexVM = new ReferenceViewModel();
            try
            {
                referenceIndexVM = new ReferenceViewModel
                {
                    LanguageBases = await _referenceIndexServices.GetLanguageBase(),
                    ReferenceImage = await _referenceIndexServices.GetReferenceImages(),
                    References = PagenetedList<Reference>.Create(_referenceIndexServices.GetReferences(), page, 5),
                    Settings = await _referenceIndexServices.GetSettings(),
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
                return View("index",  referenceIndexVM);
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
