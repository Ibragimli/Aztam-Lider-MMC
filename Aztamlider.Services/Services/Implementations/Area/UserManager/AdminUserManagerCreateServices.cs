﻿using AutoMapper;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Data.UnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.UserManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.UserManagers
{
    public class AdminUserManagerCreateServices : IAdminUserManagerCreateServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminUserManagerCreateServices(UserManager<AppUser> UserManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = UserManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }


        public async Task CreateUserManager(UserManagerCreateDto userManagerCreateDto)
        {
            await DtoCheck(userManagerCreateDto);
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == userManagerCreateDto.RoleId);
            AppUser newAdmin = new AppUser { LoginAttemptCount = 5, FullName = userManagerCreateDto.Fullname, IsAdmin = userManagerCreateDto.IsAdmin, UserName = userManagerCreateDto.Username, RoleName = role.Name };
            var admin = await _userManager.CreateAsync(newAdmin, userManagerCreateDto.Password);
            if (!admin.Succeeded)
                throw new ValueFormatExpception(admin.Errors.FirstOrDefault().Description);
            var resultRole = await _userManager.AddToRoleAsync(newAdmin, role.Name);
            if (!resultRole.Succeeded)
                throw new ValueFormatExpception(resultRole.Errors.FirstOrDefault().Description);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            var roles = await _roleManager.Roles.Where(x => x.Name != null).ToListAsync();
            return roles;
        }

        private async Task DtoCheck(UserManagerCreateDto userManagerCreateDto)
        {
            if (userManagerCreateDto.Username == null)
                throw new ItemNullException("Username  qeyd edin!");
            if (userManagerCreateDto.Password == null)
                throw new ItemNullException("Password  qeyd edin!");
            if (userManagerCreateDto.Fullname == null)
                throw new ItemNullException("Ad  qeyd edin!");
            if (userManagerCreateDto.RoleId == null)
                throw new ItemNullException("Role  qeyd edin!");

            if (userManagerCreateDto.Username?.Length < 4)
                throw new ValueFormatExpception("Username uzunluğu minimum 4 ola bilər");

            var UserExist = _userManager.FindByNameAsync(userManagerCreateDto.Username);

            if (UserExist.Result != null)
                throw new ItemAlreadyException("Username database-də mövcüddur!");
        }
    }
}
