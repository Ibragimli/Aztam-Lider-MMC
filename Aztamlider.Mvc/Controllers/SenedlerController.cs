using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Services.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Controllers
{
    public class SenedlerController : Controller
    {
        private readonly IDocumentServices _documentServices;

        public SenedlerController(IDocumentServices documentServices)
        {
            _documentServices = documentServices;
        }
        public async Task<IActionResult> Lisenziyalar()
        {

            DocumentViewModel documentVM = new DocumentViewModel();
            try
            {
                documentVM = new DocumentViewModel
                {
                    LanguageBases = await _documentServices.GetLanguageBase(),
                    Licenses = await _documentServices.GetDocumentLicenses(),
                    Settings = await _documentServices.GetSettings(),
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", documentVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", documentVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(documentVM);
        }
        public async Task<IActionResult> Sertifikatlar()
        {

            DocumentViewModel documentVM = new DocumentViewModel();
            try
            {
                documentVM = new DocumentViewModel
                {
                    LanguageBases = await _documentServices.GetLanguageBase(),
                    Certificates = await _documentServices.GetDocumentCertificates(),
                    Settings = await _documentServices.GetSettings(),
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", documentVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", documentVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(documentVM);
        }
    }
}
