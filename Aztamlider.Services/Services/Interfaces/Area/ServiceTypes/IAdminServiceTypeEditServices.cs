using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.ServiceTypes
{
    public interface IAdminServiceTypeEditServices
    {
        public Task<ServiceType> GetServiceType(int id);
        public Task EditServiceType(ServiceType ServiceType);

    }
}
