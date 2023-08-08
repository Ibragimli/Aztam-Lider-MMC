using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.References
{
    public interface IAdminReferenceCreateServices
    {
        Task<Reference> CreateProject(ReferenceCreateDto ReferenceCreateDto);
        Task DtoCheck(ReferenceCreateDto ReferenceCreateDto);
        public Task CreateImageFormFile(List<IFormFile> imageFiles, int Id);
        public Task<IEnumerable<ServiceType>> GetAllServiceTypes();
        public Task<IEnumerable<ServiceName>> GetAllServiceNames();

    }
}
