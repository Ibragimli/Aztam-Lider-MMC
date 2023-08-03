using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aztamlider.Core.Entites;
using Aztamlider.Data.Datacontext;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.Helper;
using System.Data;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Services.Interfaces.Area.Settings;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Editor")]

    public class SettingController : Controller
    {
        private readonly DataContext _context;
        private readonly ISettingEditServices _SettingEditServices;
        private readonly ISettingIndexServices _SettingIndexServices;

        public SettingController(DataContext context, ISettingEditServices SettingEditServices, ISettingIndexServices SettingIndexServices)
        {
            _context = context;
            _SettingEditServices = SettingEditServices;
            _SettingIndexServices = SettingIndexServices;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;
            ViewBag.Search = search;

            var Settings = _SettingIndexServices.SearchCheck(search);

            SettingIndexViewModel SettingIndexVM = new SettingIndexViewModel
            {
                PagenatedItems = PagenetedList<Setting>.Create(Settings, page, 6),
            };

            return View(SettingIndexVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _SettingEditServices.IsExists(id);
                await _SettingEditServices.GetSearch(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }

            return View(await _SettingEditServices.GetSearch(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SettingEditDto SettingEdit)
        {
            try
            {
                await _SettingEditServices.SettingEdit(SettingEdit);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(SettingEdit);
            }
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(SettingEdit);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(SettingEdit);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "Setting");
        }

    }
}
