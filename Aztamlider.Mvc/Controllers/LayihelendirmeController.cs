using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Services.Implementations.User;
using Aztamlider.Services.Services.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Controllers
{
    public class LayihelendirmeController : Controller
    {
        private readonly IProjectIndexServices _projectIndexServices;

        public LayihelendirmeController(IProjectIndexServices projectIndexServices)
        {
            _projectIndexServices = projectIndexServices;
        }
        public async Task<IActionResult> MEPDesign()
        {
            ProjectViewModel projectVM = new ProjectViewModel();
            try
            {
                projectVM = new ProjectViewModel
                {
                    LanguageBases = await _projectIndexServices.GetLanguageBase(),
                    Settings = await _projectIndexServices.GetSettings(),
                    MepDesignProjects = await _projectIndexServices.GetMepDesignProjects(),

                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return View("MEPDesign", projectVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return View("MEPDesign", projectVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(projectVM);
        }

        //public async Task<IActionResult> Tikinti()
        //{
        //    ProjectViewModel projectVM = new ProjectViewModel();
        //    try
        //    {
        //        projectVM = new ProjectViewModel
        //        {
        //            LanguageBases = await _projectIndexServices.GetLanguageBase(),
        //            Settings = await _projectIndexServices.GetSettings(),
        //            ConstructionProjects = await _projectIndexServices.GetConstructionProjects(),

        //        };
        //    }
        //    catch (ItemNotFoundException ex)
        //    {
        //        TempData["Error"] = (ex.Message);
        //        return View("Tikinti", projectVM);
        //    }
        //    catch (ItemNullException ex)
        //    {
        //        TempData["Error"] = (ex.Message);
        //        return View("Tikinti", projectVM);
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("index", "notfound");
        //    }
        //    return View(projectVM);
        //}

    }
}
