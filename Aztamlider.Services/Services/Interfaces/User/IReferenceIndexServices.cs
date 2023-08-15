using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.User
{
    public interface IReferenceIndexServices
    {
        public Task<IEnumerable<Setting>> GetSettings();
        public Task<IEnumerable<ServiceType>> GetServiceTypes();
        public Task<IEnumerable<Service>> GetServices();
        public Task<IEnumerable<ServiceName>> GetServiceNames();
        public Task<IEnumerable<LanguageBase>> GetLanguageBase();
        public IQueryable<Reference> GetReferencesCompleted(int serviceId);
        public IQueryable<Reference> GetReferencesOthers(int serviceId);
        public Task<IEnumerable<Reference>> GetReferencesCompletedCount();
        public Task<IEnumerable<Reference>> GetReferencesOtherCount();
        public Task<Reference> GetReference(int id);
        public Task<IEnumerable<ReferenceImage>> GetReferenceImages();
    }
}
