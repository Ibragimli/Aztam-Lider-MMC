using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class ServiceController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminServiceIndexServices _adminServiceIndexServices;
        private readonly IAdminServiceDeleteServices _adminServiceDeleteServices;
        private readonly IAdminServiceEditServices _adminServiceEditServices;
        private readonly IAdminServiceCreateServices _adminServiceCreateServices;

        public ServiceController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminServiceIndexServices adminServiceIndexServices, IAdminServiceDeleteServices adminServiceDeleteServices, IAdminServiceEditServices adminServiceEditServices, IAdminServiceCreateServices adminServiceCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminServiceIndexServices = adminServiceIndexServices;
            _adminServiceDeleteServices = adminServiceDeleteServices;
            _adminServiceEditServices = adminServiceEditServices;
            _adminServiceCreateServices = adminServiceCreateServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            ServiceIndexViewModel ServiceIndexVM = new ServiceIndexViewModel();
            try
            {
                var service = _adminServiceIndexServices.GetService(name);
                ServiceIndexVM = new ServiceIndexViewModel
                {
                    Services = PagenetedList<Service>.Create(service, page, 5),
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
            return View(ServiceIndexVM);
        }
        public IActionResult Create()
        {

            ServiceCreateDto serviceCreateDto = new ServiceCreateDto();
            return View(serviceCreateDto);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateDto serviceCreateDto)
        {
            try
            {
                var Service = await _adminServiceCreateServices.CreateProject(serviceCreateDto);
                await _adminServiceCreateServices.CreateImageFormFile(serviceCreateDto.ImageFiles, Service.Id);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Service", "Create", user.FullName, user.RoleName, serviceCreateDto.TitleAz);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
          
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("ServiceCreateDto.ImageFiles", ex.Message);
                return View();
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("ServiceCreateDto.ImageFiles", ex.Message);
                return View();
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View();
            }

            return RedirectToAction("index", "Service");

        }


        public async Task<IActionResult> Edit(int id)
        {
            ServiceEditViewModel ServiceEditVM = new ServiceEditViewModel();

            try
            {
                ServiceEditVM = new ServiceEditViewModel()
                {
                    Service = await _adminServiceEditServices.GetService(id),
                    ServiceImages = await _adminServiceEditServices.GetImages(id),
                };

            }
            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ServiceEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(ServiceEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Service Service)
        {
            ServiceEditViewModel ServiceEditVM = new ServiceEditViewModel();

            try
            {
                ServiceEditVM = new ServiceEditViewModel()
                {
                    Service = await _adminServiceEditServices.GetService(Service.Id),
                    ServiceImages = await _adminServiceEditServices.GetImages(Service.Id),
                };

                await _adminServiceEditServices.EditService(Service);

                //Logger
                var product = await _adminServiceEditServices.GetService(Service.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Service", "Edit", user.FullName, user.RoleName, product.TitleAz);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceEditVM);

            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "Service");


        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminServiceDeleteServices.DeleteService(id);

                //Logger
                var product = await _adminServiceEditServices.GetService(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Service", "Delete", user.FullName, user.RoleName, product.TitleAz);
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
            TempData["Success"] = ("Elan silindi!");
            return Ok();
        }
    }
}
