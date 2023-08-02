using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aztamlider.Core.Entites;
using Aztamlider.Data.Datacontext;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Helper;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Services.Interfaces.Area;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin")]
    public class ImageSettingController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageSettingEditServices _ImageSettingEditServices;
        private readonly IImageSettingIndexServices _ImageSettingIndexServices;

        public ImageSettingController(DataContext context, IImageSettingEditServices ImageSettingEditServices, IImageSettingIndexServices ImageSettingIndexServices)
        {
            _context = context;
            _ImageSettingEditServices =ImageSettingEditServices;
            _ImageSettingIndexServices =ImageSettingIndexServices;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;

            var ImageSettings = _ImageSettingIndexServices.SearchCheck(search);

            ImageSettingIndexViewModel ImageSettingIndexVM = new ImageSettingIndexViewModel
            {
                PagenatedItems = PagenetedList<ImageSetting>.Create(ImageSettings, page, 6),
            };

            return View(ImageSettingIndexVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _ImageSettingEditServices.IsExists(id);
                await _ImageSettingEditServices.GetSearch(id);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");

            }

            return View(await _ImageSettingEditServices.GetSearch(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ImageSettingEditDto ImageSettingEdit)
        {
            try
            {
                await _ImageSettingEditServices.ImageSettingEdit(ImageSettingEdit);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(ImageSettingEdit);
            }
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(ImageSettingEdit);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(ImageSettingEdit);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "ImageSetting");
        }
    }
}
