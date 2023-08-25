using Aztamlider.Core.Entites;
using Aztamlider.Core.Repositories;
using Aztamlider.Data.Datacontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Data.Repositories
{

    public class ProjectTypeRepository : Repository<ProjectType>, IProjectTypeRepository
    {
        private readonly DataContext _context;

        public ProjectTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
