using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.EntityFrameworkCore;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class DocumentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILoggerServices _loggerServices;
        private readonly IAdminDocumentIndexServices _adminDocumentIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminDocumentDeleteServices _adminDocumentDeleteServices;
        private readonly IAdminDocumentEditServices _adminDocumentEditServices;
        private readonly IAdminDocumentCreateServices _adminDocumentCreateServices;

        public DocumentController(UserManager<AppUser> userManager, ILoggerServices loggerServices, IAdminDocumentIndexServices adminDocumentIndexServices, IManageImageHelper manageImageHelper, IAdminDocumentDeleteServices adminDocumentDeleteServices, IAdminDocumentEditServices adminDocumentEditServices, IAdminDocumentCreateServices adminDocumentCreateServices)
        {
            _userManager = userManager;
            _loggerServices = loggerServices;
            _adminDocumentIndexServices = adminDocumentIndexServices;
            _manageImageHelper = manageImageHelper;
            _adminDocumentDeleteServices = adminDocumentDeleteServices;
            _adminDocumentEditServices = adminDocumentEditServices;
            _adminDocumentCreateServices = adminDocumentCreateServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            DocumentIndexViewModel DocumentIndexVM = new DocumentIndexViewModel();
            try
            {
                var Document = _adminDocumentIndexServices.GetDocument(name);
                DocumentIndexVM = new DocumentIndexViewModel
                {
                    Documents = PagenetedList<Document>.Create(Document, page, 5),
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
            return View(DocumentIndexVM);
        }
        public IActionResult Create()
        {
            DocumentCreateDto DocumentCreateDto = new DocumentCreateDto();

            return View(DocumentCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(DocumentCreateDto DocumentCreateDto)
        {
            DocumentCreateDto DocumentDto = new DocumentCreateDto();

            try
            {
                var Document = await _adminDocumentCreateServices.CreateDocument(DocumentCreateDto);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Document", "Create", user.FullName, user.UserName, Document.NameAz);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(DocumentDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(DocumentDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(DocumentDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(DocumentDto);
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("DocumentCreateDto.ImageFiles", ex.Message);
                return View(DocumentDto);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("DocumentCreateDto.ImageFiles", ex.Message);
                return View(DocumentDto);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(DocumentDto);
            }

            return RedirectToAction("index", "Document");

        }


        public async Task<IActionResult> Edit(int id)
        {
            DocumentEditViewModel DocumentEditVM = new DocumentEditViewModel();

            try
            {
                DocumentEditVM = new DocumentEditViewModel()
                {
                    Document = await _adminDocumentEditServices.GetDocument(id),
                };



            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", DocumentEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", DocumentEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(DocumentEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Document Document)
        {
            DocumentEditViewModel DocumentEditVM = new DocumentEditViewModel();

            try
            {
                DocumentEditVM = new DocumentEditViewModel()
                {
                    Document = await _adminDocumentEditServices.GetDocument(Document.Id),
                };

                await _adminDocumentEditServices.EditDocument(Document);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Document", "Edit", user.FullName, user.UserName, DocumentEditVM.Document.NameAz);
                //Logger
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", DocumentEditVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", DocumentEditVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", DocumentEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", DocumentEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", DocumentEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", DocumentEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", DocumentEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", DocumentEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "Document");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminDocumentDeleteServices.DeleteDocument(id);

                //Logger
                var product = await _adminDocumentEditServices.GetDocument(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Document", "Edit", user.FullName, user.UserName, product.NameAz);
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
