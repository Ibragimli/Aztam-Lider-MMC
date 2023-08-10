using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ServiceNames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class ServiceNameController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminServiceNameIndexServices _adminServiceNameIndexServices;
        private readonly IAdminServiceNameDeleteServices _adminServiceNameDeleteServices;
        private readonly IAdminServiceNameEditServices _adminServiceNameEditServices;
        private readonly IAdminServiceNameCreateServices _adminServiceNameCreateServices;

        public ServiceNameController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminServiceNameIndexServices adminServiceNameIndexServices, IAdminServiceNameDeleteServices adminServiceNameDeleteServices, IAdminServiceNameEditServices adminServiceNameEditServices, IAdminServiceNameCreateServices adminServiceNameCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminServiceNameIndexServices = adminServiceNameIndexServices;
            _adminServiceNameDeleteServices = adminServiceNameDeleteServices;
            _adminServiceNameEditServices = adminServiceNameEditServices;
            _adminServiceNameCreateServices = adminServiceNameCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            ServiceNameIndexViewModel ServiceNameIndexVM = new ServiceNameIndexViewModel();
            try
            {
                var ServiceName = _adminServiceNameIndexServices.GetServiceName(name);
                ServiceNameIndexVM = new ServiceNameIndexViewModel
                {
                    ServiceNames = PagenetedList<ServiceName>.Create(ServiceName, page, 5),
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
            return View(ServiceNameIndexVM);
        }
        public async Task<IActionResult> Create()
        {
            ServiceNameCreateDto serviceNameCreateDto = new ServiceNameCreateDto();

            return View(serviceNameCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceNameCreateDto ServiceNameCreateDto)
        {
            ServiceNameCreateDto ServiceNameDto = new ServiceNameCreateDto();


            try
            {
                var ServiceName = await _adminServiceNameCreateServices.CreateServiceName(ServiceNameCreateDto);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("ServiceName", "Create", user.FullName, user.UserName, ServiceNameCreateDto.NameAz);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ServiceNameDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ServiceNameDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ServiceNameDto);
            }

            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ServiceNameDto);
            }


            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("ServiceNameCreateDto.ImageFiles", ex.Message);
                return View(ServiceNameDto);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("ServiceNameCreateDto.ImageFiles", ex.Message);
                return View(ServiceNameDto);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(ServiceNameDto);
            }

            return RedirectToAction("index", "ServiceName");

        }


        public async Task<IActionResult> Edit(int id)
        {
            ServiceNameEditViewModel ServiceNameEditVM = new ServiceNameEditViewModel();

            try
            {
                ServiceNameEditVM = new ServiceNameEditViewModel()
                {
                    ServiceName = await _adminServiceNameEditServices.GetServiceName(id),
                };

            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ServiceNameEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ServiceNameEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(ServiceNameEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceName ServiceName)
        {
            ServiceNameEditViewModel ServiceNameEditVM = new ServiceNameEditViewModel();

            try
            {
                ServiceNameEditVM = new ServiceNameEditViewModel()
                {
                    ServiceName = await _adminServiceNameEditServices.GetServiceName(ServiceName.Id),
                };


                await _adminServiceNameEditServices.EditServiceName(ServiceName);

                //Logger
                var product = await _adminServiceNameEditServices.GetServiceName(ServiceName.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("ServiceName", "Edit", user.FullName, user.UserName, product.NameAz);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceNameEditVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceNameEditVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceNameEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceNameEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceNameEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceNameEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceNameEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceNameEditVM);

            }

            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "ServiceName");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminServiceNameDeleteServices.DeleteServiceName(id);
                //Logger
                var product = await _adminServiceNameEditServices.GetServiceName(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("ServiceName", "Delete", user.FullName, user.UserName, product.NameAz);
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
