using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class ReferenceController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminReferenceIndexServices _adminReferenceIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminReferenceDeleteServices _adminReferenceDeleteServices;
        private readonly IAdminReferenceEditServices _adminReferenceEditServices;
        private readonly IAdminReferenceCreateServices _adminReferenceCreateServices;
        public ReferenceController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminReferenceIndexServices adminReferenceIndexServices, IManageImageHelper manageImageHelper, IAdminReferenceDeleteServices adminReferenceDeleteServices, IAdminReferenceEditServices adminReferenceEditServices, IAdminReferenceCreateServices adminReferenceCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminReferenceIndexServices = adminReferenceIndexServices;
            _manageImageHelper = manageImageHelper;
            _adminReferenceDeleteServices = adminReferenceDeleteServices;
            _adminReferenceEditServices = adminReferenceEditServices;
            _adminReferenceCreateServices = adminReferenceCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            ReferenceIndexViewModel ReferenceIndexVM = new ReferenceIndexViewModel();
            try
            {
                var reference = _adminReferenceIndexServices.GetReference(name);
                ReferenceIndexVM = new ReferenceIndexViewModel
                {
                    References = PagenetedList<Reference>.Create(reference, page, 5),
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
            return View(ReferenceIndexVM);
        }
        public async Task<IActionResult> Create()
        {

            ReferenceCreateDto referenceCreateDto = new ReferenceCreateDto();
            ReferenceCreateViewModel referenceCreateVM = new ReferenceCreateViewModel();

            try
            {
                referenceCreateVM = new ReferenceCreateViewModel()
                {
                    ReferenceCreateDto = referenceCreateDto,
                    ServiceTypes = await _adminReferenceCreateServices.GetAllServiceTypes(),
                    ServiceNames = await _adminReferenceCreateServices.GetAllServiceNames(),
                };
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(referenceCreateVM);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(referenceCreateVM);
            }

            return View(referenceCreateVM);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(ReferenceCreateDto ReferenceCreateDto)
        {

            ReferenceCreateDto referenceCreateDto = new ReferenceCreateDto();
            ReferenceCreateViewModel referenceCreateVM = new ReferenceCreateViewModel();

            try
            {
                referenceCreateVM = new ReferenceCreateViewModel()
                {
                    ReferenceCreateDto = referenceCreateDto,
                    ServiceTypes = await _adminReferenceCreateServices.GetAllServiceTypes(),
                    ServiceNames = await _adminReferenceCreateServices.GetAllServiceNames(),

                };
                await _adminReferenceCreateServices.DtoCheck(ReferenceCreateDto);
                // CheckImage
                _manageImageHelper.ImagesCheck(ReferenceCreateDto.ImageFiles);
                var Reference = await _adminReferenceCreateServices.CreateProject(ReferenceCreateDto);
                await _adminReferenceCreateServices.CreateImageFormFile(ReferenceCreateDto.ImageFiles, Reference.Id);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Reference", "Create", user.FullName, user.UserName, ReferenceCreateDto.Name);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(referenceCreateVM);
            }

            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(referenceCreateVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(referenceCreateVM);
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("ReferenceCreateDto.ImageFiles", ex.Message);
                return View(referenceCreateVM);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("ReferenceCreateDto.ImageFiles", ex.Message);
                return View(referenceCreateVM);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(referenceCreateVM);
            }
            TempData["Success"] = ("Proses uğursuz oldu!");
            return RedirectToAction("index", "Reference");

        }
        public async Task<IActionResult> Edit(int id)
        {
            ReferenceEditViewModel ReferenceEditVM = new ReferenceEditViewModel();

            try
            {
                ReferenceEditVM = new ReferenceEditViewModel()
                {
                    Reference = await _adminReferenceEditServices.GetReference(id),
                    ReferenceImages = await _adminReferenceEditServices.GetImages(id),
                    ServiceTypes = await _adminReferenceEditServices.GetAllServiceTypes(),
                    ServiceNames = await _adminReferenceCreateServices.GetAllServiceNames(),

                };

            }
            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", ReferenceEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(ReferenceEditVM);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(Reference Reference)
        {
            ReferenceEditViewModel ReferenceEditVM = new ReferenceEditViewModel();

            try
            {
                ReferenceEditVM = new ReferenceEditViewModel()
                {
                    Reference = await _adminReferenceEditServices.GetReference(Reference.Id),
                    ReferenceImages = await _adminReferenceEditServices.GetImages(Reference.Id),
                    ServiceTypes = await _adminReferenceEditServices.GetAllServiceTypes(),
                    ServiceNames = await _adminReferenceCreateServices.GetAllServiceNames(),

                };
                await _adminReferenceEditServices.EditReference(Reference);

                //Logger
                var product = await _adminReferenceEditServices.GetReference(Reference.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Reference", "Edit", user.FullName, user.UserName, product.Name);
            }

            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ReferenceEditVM);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ReferenceEditVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ReferenceEditVM);
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ReferenceEditVM);
            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ReferenceEditVM);
            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", ReferenceEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "Reference");


        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminReferenceDeleteServices.DeleteReference(id);

                //Logger
                var product = await _adminReferenceEditServices.GetReference(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Reference", "Delete", user.FullName, user.UserName, product.Name);
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
            TempData["Success"] = ("Elan silindi!");
            return Ok();
        }
    }
}
