using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.RoleManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.RoleManagers
{
    public class AdminRoleManagerCreateServices : IAdminRoleManagerCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminRoleManagerCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<RoleManager<IdentityRole>> CreateRoleManager(RoleManagerCreateDto RoleManagerCreateDto)
        {
            await DtoCheck(RoleManagerCreateDto);
            var RoleManager = _mapper.Map<RoleManager<IdentityRole>>(RoleManagerCreateDto);

            await _unitOfWork.RoleManagerRepository.InsertAsync(RoleManager);
            await _unitOfWork.CommitAsync();

            return RoleManager;
        }

        private async Task DtoCheck(RoleManagerCreateDto RoleManagerCreateDto)
        {
            if (RoleManagerCreateDto.NameAz == null || RoleManagerCreateDto.NameEn == null)
            {
                throw new ItemNullException("Xidmət adını qeyd edin!");
            }
            if (RoleManagerCreateDto.NameAz?.Length < 3 || RoleManagerCreateDto.NameEn?.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu minimum 3 ola bilər");
            }
            if (RoleManagerCreateDto.NameAz?.Length > 150 || RoleManagerCreateDto.NameEn?.Length > 150)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu maksimum 150 ola bilər");
            }
        }
    }
}
