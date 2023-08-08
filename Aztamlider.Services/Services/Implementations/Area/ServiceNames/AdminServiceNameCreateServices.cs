using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ServiceNames;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ServiceNames
{
    public class AdminServiceNameCreateServices : IAdminServiceNameCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminServiceNameCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<ServiceName> CreateServiceName(ServiceNameCreateDto ServiceNameCreateDto)
        {
            await DtoCheck(ServiceNameCreateDto);
            var ServiceName = _mapper.Map<ServiceName>(ServiceNameCreateDto);

            await _unitOfWork.ServiceNameRepository.InsertAsync(ServiceName);
            await _unitOfWork.CommitAsync();

            return ServiceName;
        }

        private async Task DtoCheck(ServiceNameCreateDto ServiceNameCreateDto)
        {
            if (ServiceNameCreateDto.NameAz == null || ServiceNameCreateDto.NameEn == null)
            {
                throw new ItemNullException("Xidmət adını qeyd edin!");
            }
            if (ServiceNameCreateDto.NameAz?.Length < 3 || ServiceNameCreateDto.NameEn?.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu minimum 3 ola bilər");
            }
            if (ServiceNameCreateDto.NameAz?.Length > 150 || ServiceNameCreateDto.NameEn?.Length > 150)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu maksimum 150 ola bilər");
            }
        }
    }
}
