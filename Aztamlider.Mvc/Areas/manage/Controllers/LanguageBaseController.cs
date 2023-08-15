using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aztamlider.Core.Entites;
using Aztamlider.Data.Datacontext;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.Helper;
using System.Data;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Services.Interfaces.Area.LanguageBases;
using Aztamlider.Services.HelperService.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class LanguageBaseController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly ILanguageBaseCreateServices _languageBaseCreateServices;
        private readonly ILanguageBaseEditServices _LanguageBaseEditServices;
        private readonly ILanguageBaseIndexServices _LanguageBaseIndexServices;

        public LanguageBaseController(ILoggerServices loggerServices, UserManager<AppUser> userManager, DataContext context, ILanguageBaseCreateServices languageBaseCreateServices, ILanguageBaseEditServices LanguageBaseEditServices, ILanguageBaseIndexServices LanguageBaseIndexServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _context = context;
            _languageBaseCreateServices = languageBaseCreateServices;
            _LanguageBaseEditServices = LanguageBaseEditServices;
            _LanguageBaseIndexServices = LanguageBaseIndexServices;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;
            ViewBag.Search = search;

            var LanguageBases = _LanguageBaseIndexServices.SearchCheck(search);

            LanguageBaseIndexViewModel LanguageBaseIndexVM = new LanguageBaseIndexViewModel
            {
                PagenatedItems = PagenetedList<LanguageBase>.Create(LanguageBases, page, 6),
            };

            return View(LanguageBaseIndexVM);
        }

        public  IActionResult Create()
        {
            LanguageBaseCreateDto LanguageBaseCreateDto = new LanguageBaseCreateDto();

            return View(LanguageBaseCreateDto);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(LanguageBaseCreateDto LanguageBaseCreateDto)
        {
            LanguageBaseCreateDto LanguageBaseDto = new LanguageBaseCreateDto();

            try
            {
                var LanguageBase = await _languageBaseCreateServices.CreateLanguageBase(LanguageBaseCreateDto);


                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("LanguageBase", "Create", user.FullName, user.UserName, LanguageBaseCreateDto.Key);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(LanguageBaseDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(LanguageBaseDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(LanguageBaseDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(LanguageBaseDto);
            }


            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(LanguageBaseDto);
            }

            return RedirectToAction("index", "LanguageBase");

        }



        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _LanguageBaseEditServices.IsExists(id);
                await _LanguageBaseEditServices.GetSearch(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }

            return View(await _LanguageBaseEditServices.GetSearch(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LanguageBaseEditDto LanguageBaseEdit)
        {
            try
            {
                await _LanguageBaseEditServices.LanguageBaseEdit(LanguageBaseEdit);

                //Logger
                var product = await _LanguageBaseEditServices.GetSearch(LanguageBaseEdit.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("LanguageBase", "Edit", user.FullName, user.UserName, product.Key);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(LanguageBaseEdit);
            }
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(LanguageBaseEdit);
            }
            catch (UserNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(LanguageBaseEdit);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(LanguageBaseEdit);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "LanguageBase");
        }

    }
}
