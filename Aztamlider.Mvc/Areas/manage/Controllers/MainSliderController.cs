using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.MainSliders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class MainSliderController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminMainSliderIndexServices _adminMainSliderIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminMainSliderDeleteServices _adminMainSliderDeleteServices;
        private readonly IAdminMainSliderEditServices _adminMainSliderEditServices;
        private readonly IAdminMainSliderCreateServices _adminMainSliderCreateServices;

        public MainSliderController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminMainSliderIndexServices adminMainSliderIndexServices, IManageImageHelper manageImageHelper, IAdminMainSliderDeleteServices adminMainSliderDeleteServices, IAdminMainSliderEditServices adminMainSliderEditServices, IAdminMainSliderCreateServices adminMainSliderCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminMainSliderIndexServices = adminMainSliderIndexServices;
            _manageImageHelper = manageImageHelper;
            _adminMainSliderDeleteServices = adminMainSliderDeleteServices;
            _adminMainSliderEditServices = adminMainSliderEditServices;
            _adminMainSliderCreateServices = adminMainSliderCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            MainSliderIndexViewModel MainSliderIndexVM = new MainSliderIndexViewModel();
            try
            {
                var MainSlider = _adminMainSliderIndexServices.GetMainSlider(name);
                MainSliderIndexVM = new MainSliderIndexViewModel
                {
                    MainSliders = PagenetedList<MainSlider>.Create(MainSlider, page, 5),
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
            return View(MainSliderIndexVM);
        }
        public async Task<IActionResult> Create()
        {
            MainSliderCreateDto MainSliderCreateDto = new MainSliderCreateDto();

            return View(MainSliderCreateDto);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(MainSliderCreateDto MainSliderCreateDto)
        {
            MainSliderCreateDto MainSliderDto = new MainSliderCreateDto();
            try
            {
                var MainSlider = await _adminMainSliderCreateServices.CreateMainSlider(MainSliderCreateDto);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("MainSlider", "Create", user.FullName, user.UserName, MainSliderCreateDto.TitleAz);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(MainSliderDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(MainSliderDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(MainSliderDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(MainSliderDto);
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("MainSliderCreateDto.ImageFiles", ex.Message);
                return View(MainSliderDto);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("MainSliderCreateDto.ImageFiles", ex.Message);
                return View(MainSliderDto);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(MainSliderDto);
            }

            return RedirectToAction("index", "MainSlider");

        }


        public async Task<IActionResult> Edit(int id)
        {
            MainSliderEditViewModel MainSliderEditVM = new MainSliderEditViewModel();

            try
            {
                MainSliderEditVM = new MainSliderEditViewModel()
                {
                    MainSlider = await _adminMainSliderEditServices.GetMainSlider(id),
                };

            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", MainSliderEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", MainSliderEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(MainSliderEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MainSlider MainSlider)
        {
            MainSliderEditViewModel MainSliderEditVM = new MainSliderEditViewModel();

            try
            {
                MainSliderEditVM = new MainSliderEditViewModel()
                {
                    MainSlider = await _adminMainSliderEditServices.GetMainSlider(MainSlider.Id),
                };

                await _adminMainSliderEditServices.EditMainSlider(MainSlider);

                //Logger
                var product = await _adminMainSliderEditServices.GetMainSlider(MainSlider.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("MainSlider", "Edit", user.FullName, user.UserName, product.TitleAz);
            }

            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MainSliderEditVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MainSliderEditVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MainSliderEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MainSliderEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MainSliderEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MainSliderEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MainSliderEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MainSliderEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "MainSlider");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminMainSliderDeleteServices.DeleteMainSlider(id);

                //Logger
                var product = await _adminMainSliderEditServices.GetMainSlider(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("MainSlider", "Delete", user.FullName, user.UserName, product.TitleAz);
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
