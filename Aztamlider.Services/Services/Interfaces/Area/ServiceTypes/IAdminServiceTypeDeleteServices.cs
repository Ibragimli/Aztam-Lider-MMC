using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.ServiceTypes
{
    public interface IAdminServiceTypeDeleteServices
    {
        public Task DeleteServiceType(int id);
    }
}
