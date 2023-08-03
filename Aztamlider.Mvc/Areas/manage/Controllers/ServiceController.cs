using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]

    public class ServiceController : Controller
    {
        private readonly IAdminServiceIndexServices _adminServiceIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminServiceDeleteServices _adminServiceDeleteServices;
        private readonly IAdminServiceEditServices _adminServiceEditServices;
        private readonly IAdminServiceCreateServices _adminServiceCreateServices;

        public ServiceController(IAdminServiceIndexServices adminServiceIndexServices, IManageImageHelper manageImageHelper, IAdminServiceDeleteServices adminServiceDeleteServices, IAdminServiceEditServices adminServiceEditServices, IAdminServiceCreateServices adminServiceCreateServices)
        {
            _adminServiceIndexServices = adminServiceIndexServices;
            _manageImageHelper = manageImageHelper;
            _adminServiceDeleteServices = adminServiceDeleteServices;
            _adminServiceEditServices = adminServiceEditServices;
            _adminServiceCreateServices = adminServiceCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
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
            TempData["Success"] = ("Elan silindi!");
            return Ok();
        }
    }
}
