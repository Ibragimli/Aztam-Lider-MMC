using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Services
{
    public interface IAdminServiceCreateServices
    {
        Task<Service> CreateProject(ServiceCreateDto ServiceCreateDto);
        public Task CreateImageFormFile(List<IFormFile> imageFiles, int Id);
    }
}
