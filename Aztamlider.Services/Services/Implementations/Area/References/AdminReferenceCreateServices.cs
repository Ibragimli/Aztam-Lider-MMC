using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.References;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.References
{
    public class AdminReferenceCreateServices : IAdminReferenceCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminReferenceCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<Reference> CreateProject(ReferenceCreateDto ReferenceCreateDto)
        {

            var Reference = _mapper.Map<Reference>(ReferenceCreateDto);
            await _unitOfWork.ReferenceRepository.InsertAsync(Reference);
            await _unitOfWork.CommitAsync();
            return Reference;
        }

        public async Task DtoCheck(ReferenceCreateDto ReferenceCreateDto)
        {
            if (ReferenceCreateDto == null)
                throw new ItemNullException("Xəta baş verdi.");
            if (!await _unitOfWork.ServiceTypeRepository.IsExistAsync(x => x.Id == ReferenceCreateDto.ServiceTypeId))
                throw new ItemNotFoundException("Xidmət növü tapılmadı");
            if (ReferenceCreateDto.ImageFiles == null)
            {
                throw new ImageNullException("Şəkil əlavə edin");
            }
            if (ReferenceCreateDto.Name == null)
            {
                throw new ItemFormatException("Referans adı əlavə edin!");
            }
            if (ReferenceCreateDto.Date == null)
            {
                throw new ItemFormatException("Vaxt əlavə edin!");
            }
            if (ReferenceCreateDto.Location == null)
            {
                throw new ItemFormatException("Location əlavə edin!");
            }
            if (ReferenceCreateDto.SquareMetr == 0)
            {
                throw new ItemFormatException("Tikinti sahəsi əlavə edin!");
            }
            if (ReferenceCreateDto.BuildingType == null)
            {
                throw new ItemFormatException("Bina növü əlavə edin!");
            }
            if (ReferenceCreateDto.ServiceTypeId == 0)
            {
                throw new ItemFormatException("Təsvir əlavə edin!");
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
                ReferenceImage ReferenceImage = new ReferenceImage
                {
                    IsPoster = posterStatus,
                    ReferenceId = Id,
                    Image = _manageImageHelper.FileSave(image, "References"),
                };
                await _unitOfWork.ReferenceImageRepository.InsertAsync(ReferenceImage);
                i++;
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ServiceType>> GetAllServiceTypes()
        {
            var serviceTypes = await _unitOfWork.ServiceTypeRepository.GetAllAsync(x => !x.IsDelete);
            return serviceTypes;
        }

       
    }
}
