using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
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
    //public class AdminRoleManagerIndexServices : IAdminRoleManagerIndexServices
    //{
    //    private readonly IUnitOfWork _unitOfWork;

    //    public AdminRoleManagerIndexServices(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }
    //    public IQueryable<RoleManager<IdentityRole>> GetRoleManager(string name)
    //    {
    //        var poster = _unitOfWork.RoleManagerRepository.asQueryable();
    //        poster = poster.Where(x => !x.IsDelete);

    //        if (name != null)
    //            poster = poster.Where(i => EF.Functions.Like(i, $"%{name}%"));

    //        return poster;
    //    }
    //}
}
