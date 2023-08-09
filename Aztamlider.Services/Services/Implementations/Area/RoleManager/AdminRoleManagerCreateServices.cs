using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Data.UnitOfWork;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminRoleManagerCreateServices(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }


        public async Task CreateRoleManager(RoleManagerCreateDto roleManagerCreateDto)
        {
            await DtoCheck(roleManagerCreateDto);
            await _roleManager.CreateAsync(new IdentityRole(roleManagerCreateDto.Role));
            await _unitOfWork.CommitAsync();


        }

        private async Task DtoCheck(RoleManagerCreateDto roleManagerCreateDto)
        {
            if (roleManagerCreateDto.Role == null)
                throw new ItemNullException("Role adını qeyd edin!");

            if (roleManagerCreateDto.Role?.Length < 1)
                throw new ValueFormatExpception("Role adının uzunluğu minimum 1 ola bilər");

            var roleExist = _roleManager.FindByNameAsync(roleManagerCreateDto.Role);

            if (roleExist.Result != null)
                throw new ItemAlreadyException("Role database-də mövcüddur!");
        }
    }
}
