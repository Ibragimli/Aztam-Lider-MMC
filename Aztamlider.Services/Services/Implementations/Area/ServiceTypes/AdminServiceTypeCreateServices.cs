using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ServiceTypes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ServiceTypes
{
    public class AdminServiceTypeCreateServices : IAdminServiceTypeCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminServiceTypeCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<ServiceType> CreateServiceType(ServiceTypeCreateDto ServiceTypeCreateDto)
        {
            await DtoCheck(ServiceTypeCreateDto);
            var ServiceType = _mapper.Map<ServiceType>(ServiceTypeCreateDto);

            await _unitOfWork.ServiceTypeRepository.InsertAsync(ServiceType);
            await _unitOfWork.CommitAsync();

            return ServiceType;
        }

        private async Task DtoCheck(ServiceTypeCreateDto ServiceTypeCreateDto)
        {
            if (ServiceTypeCreateDto.NameAz == null || ServiceTypeCreateDto.NameEn == null)
            {
                throw new ItemNullException("Xidmət adını qeyd edin!");
            }
            if (ServiceTypeCreateDto.NameAz?.Length < 3 || ServiceTypeCreateDto.NameEn?.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu minimum 3 ola bilər");
            }
            if (ServiceTypeCreateDto.NameAz?.Length > 150 || ServiceTypeCreateDto.NameEn?.Length > 150)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu maksimum 150 ola bilər");
            }
        }
    }
}
