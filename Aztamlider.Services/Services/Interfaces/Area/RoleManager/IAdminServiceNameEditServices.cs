using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
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
        public Task<IdentityRole> GetRoleManager(string Id);
        public Task EditRoleManager(RoleManagerEditDto roleManagerEditDto);

    }
}
