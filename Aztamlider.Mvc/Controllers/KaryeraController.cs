using Aztamlider.Mvc.ViewModels;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.User;
using Aztamlider.Services.Services.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Aztamlider.Mvc.Controllers
{
    public class KaryeraController : Controller
    {
        private readonly ICareerIndexServices _careerIndexServices;
        private readonly ICareerServices _careerServices;

        public KaryeraController(ICareerIndexServices careerIndexServices, ICareerServices careerServices)
        {
            _careerIndexServices = careerIndexServices;
            _careerServices = careerServices;
        }
        public async Task<IActionResult> Index()
        {
            CareerViewModel careerVM = new CareerViewModel();
            try
            {

                careerVM = new CareerViewModel()
                {
                    CareerPostDto = new CareerPostDto(),
                    Settings = await _careerIndexServices.GetSettings(),
                    LanguageBases = await _careerIndexServices.GetLanguageBase(),

                };

            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);

                return View("index", careerVM);
            }
            catch (ItemFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("index", careerVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);

                return View("index", careerVM);
            }
            catch (Exception )
            {
                //TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            return View(careerVM);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CareerSend(CareerPostDto CareerPostDto)
        {
            CareerViewModel careerVM = new CareerViewModel();
            try
            {

                careerVM = new CareerViewModel()
                {
                    CareerPostDto = new CareerPostDto(),
                    Settings = await _careerIndexServices.GetSettings(),
                    LanguageBases = await _careerIndexServices.GetLanguageBase(),
                };
                _careerServices.CheckValue(CareerPostDto);
                await _careerServices.SendCV(CareerPostDto);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);

                return View("index", careerVM);
            }
            catch (ItemFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["Error"] = (ex.Message);
                return View("index", careerVM);
            }
            catch (ItemNullException ex)
            {
                TempData["Error"] = (ex.Message);

                return View("index", careerVM);
            }
            catch (Exception)
            {
                //TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            TempData["Success"] = "Sorğunuz göndərildi";
            return RedirectToAction("index", "karyera");

        }


    }
}
