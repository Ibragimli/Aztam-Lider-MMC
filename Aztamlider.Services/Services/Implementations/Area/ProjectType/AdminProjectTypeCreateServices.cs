using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ProjectTypes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ProjectTypes
{
    public class AdminProjectTypeCreateServices : IAdminProjectTypeCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminProjectTypeCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<ProjectType> CreateProjectType(ProjectTypeCreateDto ProjectTypeCreateDto)
        {
            await DtoCheck(ProjectTypeCreateDto);
            var ProjectType = _mapper.Map<ProjectType>(ProjectTypeCreateDto);

            await _unitOfWork.ProjectTypeRepository.InsertAsync(ProjectType);
            await _unitOfWork.CommitAsync();

            return ProjectType;
        }

        private async Task DtoCheck(ProjectTypeCreateDto ProjectTypeCreateDto)
        {
            if (ProjectTypeCreateDto.NameAz == null || ProjectTypeCreateDto.NameEn == null)
            {
                throw new ItemNullException("Xidmət adını qeyd edin!");
            }
            if (ProjectTypeCreateDto.NameAz?.Length < 3 || ProjectTypeCreateDto.NameEn?.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu minimum 3 ola bilər");
            }
            if (ProjectTypeCreateDto.NameAz?.Length > 150 || ProjectTypeCreateDto.NameEn?.Length > 150)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu maksimum 150 ola bilər");
            }
        }
    }
}
