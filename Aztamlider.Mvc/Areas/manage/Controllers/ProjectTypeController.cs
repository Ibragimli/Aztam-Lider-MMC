using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ProjectTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class ProjectTypeController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminProjectTypeIndexServices _adminProjectTypeIndexServices;
        private readonly IAdminProjectTypeDeleteServices _adminProjectTypeDeleteServices;
        private readonly IAdminProjectTypeEditServices _adminProjectTypeEditServices;
        private readonly IAdminProjectTypeCreateServices _adminProjectTypeCreateServices;

        public ProjectTypeController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminProjectTypeIndexServices adminProjectTypeIndexServices, IAdminProjectTypeDeleteServices adminProjectTypeDeleteServices, IAdminProjectTypeEditServices adminProjectTypeEditServices, IAdminProjectTypeCreateServices adminProjectTypeCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminProjectTypeIndexServices = adminProjectTypeIndexServices;
            _adminProjectTypeDeleteServices = adminProjectTypeDeleteServices;
            _adminProjectTypeEditServices = adminProjectTypeEditServices;
            _adminProjectTypeCreateServices = adminProjectTypeCreateServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            ProjectTypeIndexViewModel ProjectTypeIndexVM = new ProjectTypeIndexViewModel();
            try
            {
                var ProjectType = _adminProjectTypeIndexServices.GetProjectType(name);
                ProjectTypeIndexVM = new ProjectTypeIndexViewModel
                {
                    ProjectTypes = PagenetedList<ProjectType>.Create(ProjectType, page, 5),
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
            return View(ProjectTypeIndexVM);
        }
        public IActionResult Create()
        {
            ProjectTypeCreateDto ProjectTypeCreateDto = new ProjectTypeCreateDto();

            return View(ProjectTypeCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(ProjectTypeCreateDto ProjectTypeCreateDto)
        {
            ProjectTypeCreateDto ProjectTypeDto = new ProjectTypeCreateDto();


            try
            {
                var ProjectType = await _adminProjectTypeCreateServices.CreateProjectType(ProjectTypeCreateDto);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("ProjectType", "Create", user.FullName, user.UserName, ProjectTypeCreateDto.NameAz);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ProjectTypeDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ProjectTypeDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ProjectTypeDto);
            }

            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ProjectTypeDto);
            }


            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("ProjectTypeCreateDto.ImageFiles", ex.Message);
                return View(ProjectTypeDto);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("ProjectTypeCreateDto.ImageFiles", ex.Message);
                return View(ProjectTypeDto);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(ProjectTypeDto);
            }

            return RedirectToAction("index", "ProjectType");

        }


        public async Task<IActionResult> Edit(int id)
        {
            ProjectTypeEditViewModel ProjectTypeEditVM = new ProjectTypeEditViewModel();

            try
            {
                ProjectTypeEditVM = new ProjectTypeEditViewModel()
                {
                    ProjectType = await _adminProjectTypeEditServices.GetProjectType(id),
                };

            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ProjectTypeEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ProjectTypeEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(ProjectTypeEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectType ProjectType)
        {
            ProjectTypeEditViewModel ProjectTypeEditVM = new ProjectTypeEditViewModel();

            try
            {
                ProjectTypeEditVM = new ProjectTypeEditViewModel()
                {
                    ProjectType = await _adminProjectTypeEditServices.GetProjectType(ProjectType.Id),
                };


                await _adminProjectTypeEditServices.EditProjectType(ProjectType);

                //Logger
                var product = await _adminProjectTypeEditServices.GetProjectType(ProjectType.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("ProjectType", "Edit", user.FullName, user.UserName, product.NameAz);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectTypeEditVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectTypeEditVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectTypeEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectTypeEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectTypeEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectTypeEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectTypeEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ProjectTypeEditVM);

            }

            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "ProjectType");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminProjectTypeDeleteServices.DeleteProjectType(id);
                //Logger
                var product = await _adminProjectTypeEditServices.GetProjectType(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("ProjectType", "Delete", user.FullName, user.UserName, product.NameAz);
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
