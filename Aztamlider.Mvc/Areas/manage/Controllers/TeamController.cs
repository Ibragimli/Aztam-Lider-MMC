using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.EntityFrameworkCore;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class TeamController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILoggerServices _loggerServices;
        private readonly IAdminTeamIndexServices _adminTeamIndexServices;
        private readonly IAdminTeamDeleteServices _adminTeamDeleteServices;
        private readonly IAdminTeamEditServices _adminTeamEditServices;
        private readonly IAdminTeamCreateServices _adminTeamCreateServices;

        public TeamController(UserManager<AppUser> userManager, ILoggerServices loggerServices, IAdminTeamIndexServices adminTeamIndexServices, IAdminTeamDeleteServices adminTeamDeleteServices, IAdminTeamEditServices adminTeamEditServices, IAdminTeamCreateServices adminTeamCreateServices)
        {
            _userManager = userManager;
            _loggerServices = loggerServices;
            _adminTeamIndexServices = adminTeamIndexServices;
            _adminTeamDeleteServices = adminTeamDeleteServices;
            _adminTeamEditServices = adminTeamEditServices;
            _adminTeamCreateServices = adminTeamCreateServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            TeamIndexViewModel TeamIndexVM = new TeamIndexViewModel();
            try
            {
                var Team = _adminTeamIndexServices.GetTeam(name);
                TeamIndexVM = new TeamIndexViewModel
                {
                    Teams = PagenetedList<Team>.Create(Team, page, 5),
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
            return View(TeamIndexVM);
        }
        public IActionResult Create()
        {
            TeamCreateDto TeamCreateDto = new TeamCreateDto();

            return View(TeamCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(TeamCreateDto TeamCreateDto)
        {
            TeamCreateDto TeamDto = new TeamCreateDto();

            try
            {
                var Team = await _adminTeamCreateServices.CreateTeam(TeamCreateDto);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Team", "Create", user.FullName, user.UserName, Team.Name);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(TeamDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(TeamDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(TeamDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(TeamDto);
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("TeamCreateDto.ImageFiles", ex.Message);
                return View(TeamDto);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("TeamCreateDto.ImageFiles", ex.Message);
                return View(TeamDto);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(TeamDto);
            }

            return RedirectToAction("index", "Team");

        }


        public async Task<IActionResult> Edit(int id)
        {
            TeamEditViewModel TeamEditVM = new TeamEditViewModel();

            try
            {
                TeamEditVM = new TeamEditViewModel()
                {
                    Team = await _adminTeamEditServices.GetTeam(id),
                };



            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", TeamEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", TeamEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(TeamEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Team Team)
        {
            TeamEditViewModel TeamEditVM = new TeamEditViewModel();

            try
            {
                TeamEditVM = new TeamEditViewModel()
                {
                    Team = await _adminTeamEditServices.GetTeam(Team.Id),
                };

                await _adminTeamEditServices.EditTeam(Team);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Team", "Edit", user.FullName, user.UserName, TeamEditVM.Team.Name);
                //Logger
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", TeamEditVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", TeamEditVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", TeamEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", TeamEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", TeamEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", TeamEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", TeamEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", TeamEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "Team");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminTeamDeleteServices.DeleteTeam(id);

                //Logger
                var product = await _adminTeamEditServices.GetTeam(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Team", "Edit", user.FullName, user.UserName, product.Name);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (ItemUseException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                //return RedirectToAction("index", "notfound");
            }
            TempData["Success"] = ("Sənəd silindi!");
            return Ok();
        }
    }
}
