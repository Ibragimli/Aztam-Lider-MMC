using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.MainSliders;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.MainSliders
{
    public class AdminMainSliderEditServices : IAdminMainSliderEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminMainSliderEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditMainSlider(MainSlider MainSlider)
        {
            bool checkBool = false;

            var oldMainSlider = await GetMainSlider(MainSlider.Id);
            if (oldMainSlider == null)
                throw new ItemNullException("Layihə tapılmadı!");

            await Check(MainSlider);

            if (ImageChange(MainSlider, oldMainSlider) == 1)
                checkBool = true;


            if (oldMainSlider.TitleAz != MainSlider.TitleAz)
            {
                oldMainSlider.TitleAz = MainSlider.TitleAz;
                checkBool = true;
            }
            if (oldMainSlider.TitleEn != MainSlider.TitleEn)
            {
                oldMainSlider.TitleEn = MainSlider.TitleEn;
                checkBool = true;
            }

            if (oldMainSlider.DescriptionAz != MainSlider.DescriptionAz)
            {
                oldMainSlider.DescriptionAz = MainSlider.DescriptionAz;
                checkBool = true;
            }

            if (oldMainSlider.DescriptionEn != MainSlider.DescriptionEn)
            {
                oldMainSlider.DescriptionEn = MainSlider.DescriptionEn;
                checkBool = true;
            }


            oldMainSlider.ModifiedDate = DateTime.UtcNow.AddHours(4);

            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<MainSlider> GetMainSlider(int id)
        {
            var MainSlider = await _unitOfWork.MainSliderRepository.GetAsync(x => x.Id == id);
            return MainSlider;
        }

        private int ImageChange(MainSlider MainSlider, MainSlider projectExist)
        {
            if (MainSlider.ImageFile != null)
            {
                var posterImageFile = MainSlider.ImageFile;

                if (projectExist.Image == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "MainSliders");
                _manageImageHelper.DeleteFile(projectExist.Image, "MainSliders");
                projectExist.Image = filename;
                return 1;
            }
            return 0;
        }



        private async Task Check(MainSlider MainSlider)
        {
            if (MainSlider.TitleAz?.Length < 3 || MainSlider.TitleEn?.Length < 3)
            {
                throw new ValueFormatExpception("Slider adının uzunluğu minimum 3 ola bilər");
            }
            if (MainSlider.TitleEn?.Length > 100 || MainSlider.TitleAz?.Length > 100)
            {
                throw new ValueFormatExpception("Slider adının uzunluğu maksimum 100 ola bilər");
            }
            if (MainSlider.DescriptionEn?.Length > 200 || MainSlider.DescriptionAz?.Length > 200)
            {
                throw new ValueFormatExpception("Slider təsvir uzunluğu maksimum 200 ola bilər");
            }
       
            if (MainSlider.ImageFile != null)
                _manageImageHelper.PosterCheck(MainSlider.ImageFile);

        }
    }
}
