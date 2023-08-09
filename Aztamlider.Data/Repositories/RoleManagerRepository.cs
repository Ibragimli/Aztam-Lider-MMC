using Aztamlider.Core.Entites;
using Aztamlider.Core.Repositories;
using Aztamlider.Data.Datacontext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Data.Repositories
{

    public class RoleManagerRepository : Repository<RoleManager<IdentityRole>>, IRoleManagerRepository
    {
        private readonly DataContext _context;

        public RoleManagerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
