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
     
        public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        private readonly DataContext _context;

        public PartnerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
