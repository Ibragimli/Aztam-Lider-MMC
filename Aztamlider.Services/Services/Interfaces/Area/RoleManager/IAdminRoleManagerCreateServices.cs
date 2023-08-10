using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.RoleManagers
{
    public interface IAdminRoleManagerCreateServices
    {
        Task CreateRoleManager(RoleManagerCreateDto RoleManagerCreateDto);

    }
}
