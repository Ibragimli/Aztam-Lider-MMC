using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.UserManagers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.UserManagers
{
    public class AdminUserManagerIndexServices : IAdminUserManagerIndexServices
    {
        private readonly UserManager<AppUser> _UserManager;

        public AdminUserManagerIndexServices(UserManager<AppUser> UserManager)
        {
            _UserManager = UserManager;
        }
        public IQueryable<AppUser> GetUserManager(string name)
        {
            var Users = _UserManager.Users.Where(x => x.UserName != null).AsQueryable();


            if (Users != null)

                Users = Users.Where(i => EF.Functions.Like(i.FullName, $"%{name}%"));

            return Users;
        }
       
    }
}
