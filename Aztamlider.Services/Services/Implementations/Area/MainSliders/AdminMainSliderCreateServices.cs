using AutoMapper;
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
    public class AdminMainSliderCreateServices : IAdminMainSliderCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminMainSliderCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<MainSlider> CreateMainSlider(MainSliderCreateDto MainSliderCreateDto)
        {
            await DtoCheck(MainSliderCreateDto);
            var MainSlider = _mapper.Map<MainSlider>(MainSliderCreateDto);
            MainSlider.Image = _manageImageHelper.FileSave(MainSliderCreateDto.ImageFile, "MainSliders");
            await _unitOfWork.MainSliderRepository.InsertAsync(MainSlider);
            await _unitOfWork.CommitAsync();

            return MainSlider;
        }

        private async Task DtoCheck(MainSliderCreateDto MainSliderCreateDto)
        {
            if (MainSliderCreateDto.TitleAz?.Length < 3 || MainSliderCreateDto.TitleEn?.Length < 3)
            {
                throw new ValueFormatExpception("Slider adının uzunluğu minimum 3 ola bilər");
            }
            if (MainSliderCreateDto.TitleEn?.Length > 100 || MainSliderCreateDto.TitleAz?.Length > 100)
            {
                throw new ValueFormatExpception("Slider adının uzunluğu maksimum 100 ola bilər");
            }
            if (MainSliderCreateDto.DescriptionEn?.Length > 200 || MainSliderCreateDto.DescriptionAz?.Length > 200)
            {
                throw new ValueFormatExpception("Slider təsvir uzunluğu maksimum 200 ola bilər");
            }

            if (MainSliderCreateDto.ImageFile == null)
            {
                throw new ItemNullException("Şəkil əlavə edin!");
            }
            if (MainSliderCreateDto.ImageFile != null)
                _manageImageHelper.PosterCheck(MainSliderCreateDto.ImageFile);
        }
    }
}
