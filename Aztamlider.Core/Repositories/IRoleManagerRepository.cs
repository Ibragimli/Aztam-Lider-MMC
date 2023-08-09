using Aztamlider.Core.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.Repositories
{
    public interface IRoleManagerRepository : IRepository<RoleManager<IdentityRole>>
    {
    }
}
