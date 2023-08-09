using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.RoleManagers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.RoleManagers
{
    //public class AdminRoleManagerDeleteServices : IAdminRoleManagerDeleteServices
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IManageImageHelper _manageImageHelper;

    //    public AdminRoleManagerDeleteServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _manageImageHelper = manageImageHelper;
    //    }

    //    public async Task DeleteRoleManager(string id)
    //    {
    //        bool check = false;
    //        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>();
    //        roleManager.Roles.Where(x => x.Id == "saa");
        
    //        //if (await _unitOfWork.ReferenceRepository.IsExistAsync(x => x.RoleManagerId == project.Id))
    //        //    throw new ItemUseException("Bu xidmət növü referanslarda istifadə olunduğu üçün silmək mümkün olmadı!");

    //        check = true;
    //        if (check)
    //        {
    //            //_unitOfWork.RoleManagerRepository.Remove(project);
    //            await _unitOfWork.CommitAsync();
    //        }

    //    }
    //}
}
