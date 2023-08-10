using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class ProjectController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminProjectIndexServices _adminProjectIndexServices;
        private readonly IAdminProjectDeleteServices _adminProjectDeleteServices;
        private readonly IAdminProjectEditServices _adminProjectEditServices;
        private readonly IAdminProjectCreateServices _adminProjectCreateServices;

        public ProjectController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminProjectIndexServices adminProjectIndexServices, IAdminProjectDeleteServices adminProjectDeleteServices, IAdminProjectEditServices adminProjectEditServices, IAdminProjectCreateServices adminProjectCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminProjectIndexServices = adminProjectIndexServices;
            _adminProjectDeleteServices = adminProjectDeleteServices;
            _adminProjectEditServices = adminProjectEditServices;
            _adminProjectCreateServices = adminProjectCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            ProjectIndexViewModel ProjectIndexVM = new ProjectIndexViewModel();
            try
            {
                var Project = _adminProjectIndexServices.GetProject(name);
                ProjectIndexVM = new ProjectIndexViewModel
                {
                    Projects = PagenetedList<Project>.Create(Project, page, 5),
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
            return View(ProjectIndexVM);
        }
        public async Task<IActionResult> Create()
        {
            ProjectCreateDto ProjectCreateDto = new ProjectCreateDto();

            return View(ProjectCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateDto ProjectCreateDto)
        {
            ProjectCreateDto ProjectDto = new ProjectCreateDto();


            try
            {
                var Project = await _adminProjectCreateServices.CreateProject(ProjectCreateDto);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Project", "Create", user.FullName, user.UserName, ProjectCreateDto.TitleAz);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ProjectDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ProjectDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ProjectDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ProjectDto);
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("ProjectCreateDto.ImageFiles", ex.Message);
                return View(ProjectDto);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("ProjectCreateDto.ImageFiles", ex.Message);
                return View(ProjectDto);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(ProjectDto);
            }

            return RedirectToAction("index", "Project");

        }


        public async Task<IActionResult> Edit(int id)
        {
            ProjectEditViewModel ProjectEditVM = new ProjectEditViewModel();

            try
            {
                ProjectEditVM = new ProjectEditViewModel()
                {
                    Project = await _adminProjectEditServices.GetProject(id),
                };

            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ProjectEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ProjectEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(ProjectEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Project Project)
        {
            ProjectEditViewModel ProjectEditVM = new ProjectEditViewModel();

            try
            {
                ProjectEditVM = new ProjectEditViewModel()
                {
                    Project = await _adminProjectEditServices.GetProject(Project.Id),
                };

                await _adminProjectEditServices.EditProject(Project);

                //Logger
                var product = await _adminProjectEditServices.GetProject(Project.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Project", "Edit", user.FullName, user.UserName, product.TitleAz);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectEditVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectEditVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "Project");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminProjectDeleteServices.DeleteProject(id);

                //Logger
                var product = await _adminProjectEditServices.GetProject(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Project", "Delete", user.FullName, user.UserName, product.TitleAz);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (ItemUseException ex)
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
