using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Aztamlider.Core.Entites;
using Aztamlider.Data.Datacontext;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Helper;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Services.Interfaces.Area.EmailSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Aztamlider.Services.HelperService.Interfaces;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class EmailSettingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILoggerServices _loggerServices;
        private readonly DataContext _context;
        private readonly IEmailSettingEditServices _emailSettingEditServices;
        private readonly IEmailSettingIndexServices _emailSettingIndexServices;

        public EmailSettingController(UserManager<AppUser> userManager, ILoggerServices loggerServices, DataContext context, IEmailSettingEditServices emailSettingEditServices, IEmailSettingIndexServices emailSettingIndexServices)
        {
            _userManager = userManager;
            _loggerServices = loggerServices;
            _context = context;
            _emailSettingEditServices = emailSettingEditServices;
            _emailSettingIndexServices = emailSettingIndexServices;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;

            var EmailSettings = _emailSettingIndexServices.SearchCheck(search);

            EmailSettingIndexViewModel EmailSettingIndexVM = new EmailSettingIndexViewModel
            {
                PagenatedItems = PagenetedList<EmailSetting>.Create(EmailSettings, page, 6),
            };

            return View(EmailSettingIndexVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _emailSettingEditServices.IsExists(id);
                await _emailSettingEditServices.GetSearch(id);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");

            }

            return View(await _emailSettingEditServices.GetSearch(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmailSettingEditDto EmailSettingEdit)
        {
            try
            {
                await _emailSettingEditServices.EmailSettingEdit(EmailSettingEdit);

                //Logger
                var emailSetting = await _emailSettingEditServices.GetSearch(EmailSettingEdit.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("EmailSetting", "Edit", user.FullName, user.RoleName, EmailSettingEdit.Key);

            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(EmailSettingEdit);
            }
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(EmailSettingEdit);
            }
            catch (UserNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(EmailSettingEdit);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(EmailSettingEdit);
            }

            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "EmailSetting");
        }
    }
}
