using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.RoleManagers
{
    public interface IAdminRoleManagerDeleteServices
    {
        public Task DeleteRoleManager(int id);
    }
}
