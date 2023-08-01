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
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        private readonly DataContext _context;

        public ContactUsRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
