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
     public class CareerRepository : Repository<Career>, ICareerRepository
    {
        private readonly DataContext _context;

        public CareerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}

