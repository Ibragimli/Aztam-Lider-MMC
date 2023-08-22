using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.UserManagers
{
    public interface IAdminUserManagerEditServices
    {
        public Task<AppUser> GetUserManager(string Id);
        public Task EditUserManager(UserManagerEditDto UserManagerEditDto);
        public Task<string> RoleName(string id);
        public Task RestartLoginAttempCount(string id);

    }
}
