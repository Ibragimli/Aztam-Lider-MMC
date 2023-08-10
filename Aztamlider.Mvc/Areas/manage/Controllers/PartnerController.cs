using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Partners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class PartnerController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminPartnerIndexServices _adminPartnerIndexServices;
        private readonly IAdminPartnerDeleteServices _adminPartnerDeleteServices;
        private readonly IAdminPartnerEditServices _adminPartnerEditServices;
        private readonly IAdminPartnerCreateServices _adminPartnerCreateServices;

        public PartnerController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminPartnerIndexServices adminPartnerIndexServices, IAdminPartnerDeleteServices adminPartnerDeleteServices, IAdminPartnerEditServices adminPartnerEditServices, IAdminPartnerCreateServices adminPartnerCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminPartnerIndexServices = adminPartnerIndexServices;
            _adminPartnerDeleteServices = adminPartnerDeleteServices;
            _adminPartnerEditServices = adminPartnerEditServices;
            _adminPartnerCreateServices = adminPartnerCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            PartnerIndexViewModel PartnerIndexVM = new PartnerIndexViewModel();
            try
            {
                var Partner = _adminPartnerIndexServices.GetPartner(name);
                PartnerIndexVM = new PartnerIndexViewModel
                {
                    Partners = PagenetedList<Partner>.Create(Partner, page, 5),
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
            return View(PartnerIndexVM);
        }
        public async Task<IActionResult> Create()
        {
            PartnerCreateDto PartnerCreateDto = new PartnerCreateDto();

            return View(PartnerCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(PartnerCreateDto PartnerCreateDto)
        {
            PartnerCreateDto PartnerDto = new PartnerCreateDto();
            try
            {
                var Partner = await _adminPartnerCreateServices.CreatePartner(PartnerCreateDto);
                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Partner", "Create", user.FullName, user.UserName, PartnerCreateDto.ImageFile.FileName);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(PartnerDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(PartnerDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(PartnerDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(PartnerDto);
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("PartnerCreateDto.ImageFiles", ex.Message);
                return View(PartnerDto);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("PartnerCreateDto.ImageFiles", ex.Message);
                return View(PartnerDto);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(PartnerDto);
            }

            return RedirectToAction("index", "Partner");

        }


        public async Task<IActionResult> Edit(int id)
        {
            PartnerEditViewModel PartnerEditVM = new PartnerEditViewModel();

            try
            {
                PartnerEditVM = new PartnerEditViewModel()
                {
                    Partner = await _adminPartnerEditServices.GetPartner(id),
                };

            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", PartnerEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", PartnerEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(PartnerEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Partner Partner)
        {
            PartnerEditViewModel PartnerEditVM = new PartnerEditViewModel();

            try
            {
                PartnerEditVM = new PartnerEditViewModel()
                {
                    Partner = await _adminPartnerEditServices.GetPartner(Partner.Id),
                };


                await _adminPartnerEditServices.EditPartner(Partner);

                //Logger
                var product = await _adminPartnerEditServices.GetPartner(Partner.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Partner", "Edit", user.FullName, user.UserName, product.Image);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", PartnerEditVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", PartnerEditVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", PartnerEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", PartnerEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", PartnerEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", PartnerEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", PartnerEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", PartnerEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "Partner");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminPartnerDeleteServices.DeletePartner(id);
                //Logger
                var product = await _adminPartnerEditServices.GetPartner(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Partner", "Delete", user.FullName, user.UserName, product.Image);
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
