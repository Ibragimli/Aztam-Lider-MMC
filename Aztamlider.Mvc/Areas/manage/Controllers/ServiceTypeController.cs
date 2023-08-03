using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ServiceTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]

    public class ServiceTypeController : Controller
    {
        private readonly IAdminServiceTypeIndexServices _adminServiceTypeIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminServiceTypeDeleteServices _adminServiceTypeDeleteServices;
        private readonly IAdminServiceTypeEditServices _adminServiceTypeEditServices;
        private readonly IAdminServiceTypeCreateServices _adminServiceTypeCreateServices;

        public ServiceTypeController(IAdminServiceTypeIndexServices adminServiceTypeIndexServices, IManageImageHelper manageImageHelper, IAdminServiceTypeDeleteServices adminServiceTypeDeleteServices, IAdminServiceTypeEditServices adminServiceTypeEditServices, IAdminServiceTypeCreateServices adminServiceTypeCreateServices)
        {
            _adminServiceTypeIndexServices = adminServiceTypeIndexServices;
            _manageImageHelper = manageImageHelper;
            _adminServiceTypeDeleteServices = adminServiceTypeDeleteServices;
            _adminServiceTypeEditServices = adminServiceTypeEditServices;
            _adminServiceTypeCreateServices = adminServiceTypeCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            ServiceTypeIndexViewModel ServiceTypeIndexVM = new ServiceTypeIndexViewModel();
            try
            {
                var ServiceType = _adminServiceTypeIndexServices.GetServiceType(name);
                ServiceTypeIndexVM = new ServiceTypeIndexViewModel
                {
                    ServiceTypes = PagenetedList<ServiceType>.Create(ServiceType, page, 5),
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
            return View(ServiceTypeIndexVM);
        }
        public async Task<IActionResult> Create()
        {
            ServiceTypeCreateDto ServiceTypeCreateDto = new ServiceTypeCreateDto();

            return View(ServiceTypeCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceTypeCreateDto ServiceTypeCreateDto)
        {
            ServiceTypeCreateDto ServiceTypeDto = new ServiceTypeCreateDto();


            try
            {
                var ServiceType = await _adminServiceTypeCreateServices.CreateServiceType(ServiceTypeCreateDto);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ServiceTypeDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ServiceTypeDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(ServiceTypeDto);
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("ServiceTypeCreateDto.ImageFiles", ex.Message);
                return View(ServiceTypeDto);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("ServiceTypeCreateDto.ImageFiles", ex.Message);
                return View(ServiceTypeDto);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(ServiceTypeDto);
            }

            return RedirectToAction("index", "ServiceType");

        }


        public async Task<IActionResult> Edit(int id)
        {
            ServiceTypeEditViewModel ServiceTypeEditVM = new ServiceTypeEditViewModel();

            try
            {
                ServiceTypeEditVM = new ServiceTypeEditViewModel()
                {
                    ServiceType = await _adminServiceTypeEditServices.GetServiceType(id),
                };

            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ServiceTypeEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ServiceTypeEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(ServiceTypeEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceType ServiceType)
        {
            ServiceTypeEditViewModel ServiceTypeEditVM = new ServiceTypeEditViewModel();

            try
            {
                ServiceTypeEditVM = new ServiceTypeEditViewModel()
                {
                    ServiceType = await _adminServiceTypeEditServices.GetServiceType(ServiceType.Id),
                };


                await _adminServiceTypeEditServices.EditServiceType(ServiceType);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceTypeEditVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceTypeEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceTypeEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceTypeEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceTypeEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceTypeEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ServiceTypeEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "ServiceType");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminServiceTypeDeleteServices.DeleteServiceType(id);
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
