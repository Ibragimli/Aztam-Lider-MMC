using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.User;
using Aztamlider.Services.Services.Implementations.User;
using Aztamlider.Services.Services.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Controllers
{
    public class KomandamizController : Controller
    {
        private readonly ITeamServices _teamServices;
        private readonly ICareerServices _careerServices;

        public KomandamizController(ITeamServices TeamServices, ICareerServices careerServices)
        {
            _teamServices = TeamServices;
            _careerServices = careerServices;
        }
        public async Task<IActionResult> Index()
        {

            TeamViewModel TeamVM = new TeamViewModel();
            try
            {
                TeamVM = new TeamViewModel
                {
                    LanguageBases = await _teamServices.GetLanguageBase(),
                    Teams = await _teamServices.GetTeams(),
                    Settings = await _teamServices.GetSettings(),
                    CareerPostDto = new CareerPostDto(),

                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", TeamVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", TeamVM);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(TeamVM);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CareerSend(CareerPostDto CareerPostDto)
        {
            TeamViewModel teamVM = new TeamViewModel();
            try
            {

                teamVM = new TeamViewModel()
                {
                    CareerPostDto = new CareerPostDto(),
                    Settings = await _teamServices.GetSettings(),
                    LanguageBases = await _teamServices.GetLanguageBase(),
                    Teams = await _teamServices.GetTeams(),

                };
                _careerServices.CheckValue(CareerPostDto);
                await _careerServices.SendCV(CareerPostDto);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);

                return View("index", teamVM);
            }
            catch (ItemFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["Error"] = (ex.Message);
                return View("index", teamVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);

                return View("index", teamVM);
            }
            catch (Exception)
            {
                //TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            TempData["Success"] = "Sorğunuz göndərildi";
            return RedirectToAction("index", "komandamiz");
        }
    }
}
