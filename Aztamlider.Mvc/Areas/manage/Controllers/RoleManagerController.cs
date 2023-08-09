using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    public class RoleManagerController : Controller
    {
        //private readonly IAdminRoleManagerIndexServices _adminRoleManagerIndexServices;
        //private readonly IAdminRoleManagerDeleteServices _adminRoleManagerDeleteServices;
        //private readonly IAdminRoleManagerEditServices _adminRoleManagerEditServices;
        //private readonly IAdminRoleManagerCreateServices _adminRoleManagerCreateServices;

        //public RoleManagerController(IAdminRoleManagerIndexServices adminRoleManagerIndexServices, IAdminRoleManagerDeleteServices adminRoleManagerDeleteServices, IAdminRoleManagerEditServices adminRoleManagerEditServices, IAdminRoleManagerCreateServices adminRoleManagerCreateServices)
        //{
        //    _adminRoleManagerIndexServices = adminRoleManagerIndexServices;
        //    _adminRoleManagerDeleteServices = adminRoleManagerDeleteServices;
        //    _adminRoleManagerEditServices = adminRoleManagerEditServices;
        //    _adminRoleManagerCreateServices = adminRoleManagerCreateServices;
        //}
        //public async Task<IActionResult> Index(int page = 1, string name = null)
        //{
        //    RoleManagerIndexViewModel RoleManagerIndexVM = new RoleManagerIndexViewModel();
        //    try
        //    {
        //        var RoleManager = _adminRoleManagerIndexServices.GetRoleManager(name);
        //        RoleManagerIndexVM = new RoleManagerIndexViewModel
        //        {
        //            RoleManagers = PagenetedList<RoleManager<IdentityRole>>.Create(RoleManager, page, 5),
        //        };
        //    }
        //    catch (NotFoundException)
        //    {
        //        return RedirectToAction("index", "notfound");
        //    }

        //    catch (Exception)
        //    {
        //        return RedirectToAction("index", "notfound");
        //    }
        //    return View(RoleManagerIndexVM);
        //}
        //public async Task<IActionResult> Create()
        //{
        //    RoleManagerCreateDto roleManagerCreateDto = new RoleManagerCreateDto();

        //    return View(roleManagerCreateDto);
        //}
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<IActionResult> Create(RoleManagerCreateDto roleManagerCreateDto)
        //{
        //    RoleManagerCreateDto roleManagerDto = new RoleManagerCreateDto();

        //    try
        //    {
        //        var RoleManager = await _adminRoleManagerCreateServices.CreateRoleManager(roleManagerDto);
        //    }
        //    catch (ItemNullException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View(roleManagerCreateDto);
        //    }
        //    catch (ItemNotFoundException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View(roleManagerCreateDto);
        //    }
        //    catch (ValueFormatExpception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View(roleManagerCreateDto);
        //    }
        //    catch (ImageFormatException ex)
        //    {
        //        ModelState.AddModelError("RoleManagerCreateDto.ImageFiles", ex.Message);
        //        return View(roleManagerCreateDto);
        //    }
        //    catch (ImageNullException ex)
        //    {
        //        ModelState.AddModelError("RoleManagerCreateDto.ImageFiles", ex.Message);
        //        return View(roleManagerCreateDto);
        //    }

        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        //TempData["Error"] = ("Proses uğursuz oldu!");
        //        return View(roleManagerCreateDto);
        //    }

        //    return RedirectToAction("index", "RoleManager");

        //}


        //public async Task<IActionResult> Edit(int id)
        //{
        //    RoleManagerEditViewModel roleManagerEditVM = new RoleManagerEditViewModel();

        //    try
        //    {
        //        roleManagerEditVM = new RoleManagerEditViewModel()
        //        {
        //            RoleManager = await _adminRoleManagerEditServices.GetRoleManager(id),
        //        };

        //    }
        //    catch (NotFoundException)
        //    {
        //        return RedirectToAction("Index", "notfound");
        //    }
        //    catch (ItemNullException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Index", roleManagerEditVM);
        //    }
        //    catch (ItemNotFoundException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Index", roleManagerEditVM);
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("Index", "notfound");
        //    }
        //    return View(roleManagerEditVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(RoleManager<IdentityRole> roleManager)
        //{
        //    RoleManagerEditViewModel roleManagerEditVM = new RoleManagerEditViewModel();

        //    try
        //    {
        //        roleManagerEditVM = new RoleManagerEditViewModel()
        //        {
        //            RoleManager = await _adminRoleManagerEditServices.GetRoleManager(roleManager),
        //        };


        //        await _adminRoleManagerEditServices.EditRoleManager(roleManager);
        //    }

        //    catch (NotFoundException)
        //    {

        //        return RedirectToAction("Index", "notfound");
        //    }
        //    catch (ItemNullException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Edit", roleManagerEditVM);
        //    }
        //    catch (ValueFormatExpception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Edit", roleManagerEditVM);
        //    }
        //    catch (ItemNotFoundException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Edit", roleManagerEditVM);

        //    }
        //    catch (ImageNullException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Edit", roleManagerEditVM);

        //    }
        //    catch (ImageFormatException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Edit", roleManagerEditVM);

        //    }
        //    catch (ImageCountException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Edit", roleManagerEditVM);

        //    }
        //    catch (ValueAlreadyExpception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Edit", roleManagerEditVM);

        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("Index", "notfound");
        //    }
        //    TempData["Success"] = ("Proses uğurlu oldu");
        //    return RedirectToAction("Index", "RoleManager");
        //}

        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        await _adminRoleManagerDeleteServices.DeleteRoleManager(id);
        //    }
        //    catch (ItemNotFoundException ex)
        //    {
        //        TempData["Error"] = (ex.Message);
        //        return Ok();
        //    }
        //    catch (ItemUseException ex)
        //    {
        //        TempData["Error"] = (ex.Message);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //        //return RedirectToAction("index", "notfound");
        //    }
        //    TempData["Success"] = ("Sənəd silindi!");
        //    return Ok();
        //}
    }

}
