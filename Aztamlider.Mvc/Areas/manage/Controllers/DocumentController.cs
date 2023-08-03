using Aztamlider.Core.Entites;
using Aztamlider.Mvc.Areas.manage.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]

    public class DocumentController : Controller
    {
        private readonly IAdminServiceTypeIndexServices _adminDocumentIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminServiceTypeDeleteServices _adminDocumentDeleteServices;
        private readonly IAdminServiceTypeEditServices _adminDocumentEditServices;
        private readonly IAdminServiceTypeCreateServices _adminDocumentCreateServices;

        public DocumentController(IAdminServiceTypeIndexServices adminDocumentIndexServices, IManageImageHelper manageImageHelper, IAdminServiceTypeDeleteServices adminDocumentDeleteServices, IAdminServiceTypeEditServices adminDocumentEditServices, IAdminServiceTypeCreateServices adminDocumentCreateServices)
        {
            _adminDocumentIndexServices = adminDocumentIndexServices;
            _manageImageHelper = manageImageHelper;
            _adminDocumentDeleteServices = adminDocumentDeleteServices;
            _adminDocumentEditServices = adminDocumentEditServices;
            _adminDocumentCreateServices = adminDocumentCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
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
        public async Task<IActionResult> Create()
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
