using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.ServiceNames
{
    public interface IAdminServiceNameEditServices
    {
        public Task<ServiceName> GetServiceName(int id);
        public Task EditServiceName(ServiceName ServiceName);

    }
}
