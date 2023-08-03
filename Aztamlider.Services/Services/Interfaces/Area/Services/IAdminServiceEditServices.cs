using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Services
{

    public interface IAdminServiceEditServices
    {
        public Task<Service> GetService(int id);
        public Task EditService(Service Service);
        public Task<IEnumerable<ServiceImage>> GetImages(int id);

    }
}
