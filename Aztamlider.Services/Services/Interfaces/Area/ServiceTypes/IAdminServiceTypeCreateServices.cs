using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.ServiceTypes
{
    public interface IAdminServiceTypeCreateServices
    {
        Task<ServiceType> CreateServiceType(ServiceTypeCreateDto ServiceTypeCreateDto);

    }
}
