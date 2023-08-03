using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Implementations;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Services
{
    public class AdminServiceCreateServices : IAdminServiceCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminServiceCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<Service> CreateProject(ServiceCreateDto ServiceCreateDto)
        {
            DtoCheck(ServiceCreateDto);
            _manageImageHelper.ImagesCheck(ServiceCreateDto.ImageFiles);

            var Service = _mapper.Map<Service>(ServiceCreateDto);
            await _unitOfWork.ServiceRepository.InsertAsync(Service);
            await _unitOfWork.CommitAsync();
            return Service;
        }

        private void DtoCheck(ServiceCreateDto ServiceCreateDto)
        {
            // CheckImage
            if (ServiceCreateDto == null)
                throw new ItemNullException("Xəta baş verdi.");
         
            if (ServiceCreateDto.ImageFiles == null)
            {
                throw new ImageNullException("Şəkil əlavə edin");
            }
            if (ServiceCreateDto.TitleAz.Length < 3 || ServiceCreateDto.TitleEn.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu minimum 3 ola bilər");
            }
            if (ServiceCreateDto.TitleAz.Length > 150 || ServiceCreateDto.TitleEn.Length > 150)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu maksimum 150 ola bilər");
            }
            if (ServiceCreateDto.DescriptionAz.Length > 3500 || ServiceCreateDto.DescriptionEn.Length > 3500)
            {
                throw new ValueFormatExpception("Xidmət təsvirinin uzunluğu maksimum 3500 ola bilər");
            }
            if (ServiceCreateDto.DescriptionAz.Length < 3 || ServiceCreateDto.DescriptionEn.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət təsvirinin uzunluğu minimum 3 ola bilər");
            }
        }
        public async Task CreateImageFormFile(List<IFormFile> imageFiles, int Id)
        {
            int i = 1;
            bool posterStatus;
            foreach (var image in imageFiles)
            {
                posterStatus = false;
                if (i == 1)
                    posterStatus = true;
                ServiceImage ServiceImage = new ServiceImage
                {
                    IsPoster = posterStatus,
                    ServiceId = Id,
                    Image = _manageImageHelper.FileSave(image, "Services"),
                };
                await _unitOfWork.ServiceImageRepository.InsertAsync(ServiceImage);
                i++;
            }
            await _unitOfWork.CommitAsync();
        }
    }
}
