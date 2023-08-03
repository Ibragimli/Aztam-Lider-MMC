using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]

    public class ProjectController : Controller
    {
        private readonly IAdminProjectIndexServices _adminProjectIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminProjectDeleteServices _adminProjectDeleteServices;
        private readonly IAdminProjectEditServices _adminProjectEditServices;
        private readonly IAdminProjectCreateServices _adminProjectCreateServices;

        public ProjectController(IAdminProjectIndexServices adminProjectIndexServices, IManageImageHelper manageImageHelper, IAdminProjectDeleteServices adminProjectDeleteServices, IAdminProjectEditServices adminProjectEditServices, IAdminProjectCreateServices adminProjectCreateServices)
        {
            _adminProjectIndexServices = adminProjectIndexServices;
            _manageImageHelper = manageImageHelper;
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
