using Aztamlider.Core.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.RoleManagers
{
    public interface IAdminRoleManagerEditServices
    {
        public Task<RoleManager<IdentityRole>> GetRoleManager(int id);
        public Task EditRoleManager(RoleManager<IdentityRole> RoleManager);

    }
}
