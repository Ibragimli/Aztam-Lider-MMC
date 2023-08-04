using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.References
{
    public interface IAdminReferenceEditServices
    {
        public Task<Reference> GetReference(int id);
        public Task EditReference(Reference Reference);
        public Task<IEnumerable<ReferenceImage>> GetImages(int id);
        public Task<IEnumerable<ServiceType>> GetAllServiceTypes();

    }
}
