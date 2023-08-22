using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Services.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Controllers
{
    public class KomandamizController : Controller
    {
        private readonly ITeamServices _TeamServices;

        public KomandamizController(ITeamServices TeamServices)
        {
            _TeamServices = TeamServices;
        }
        public async Task<IActionResult> Index()
        {

            TeamViewModel TeamVM = new TeamViewModel();
            try
            {
                TeamVM = new TeamViewModel
                {
                    LanguageBases = await _TeamServices.GetLanguageBase(),
                    Teams = await _TeamServices.GetTeams(),
                    Settings = await _TeamServices.GetSettings(),
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

    }
}
