using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Services
{
    public interface IAdminServiceIndexServices
    {
        public IQueryable<Service> GetService(string name);
    }
}
