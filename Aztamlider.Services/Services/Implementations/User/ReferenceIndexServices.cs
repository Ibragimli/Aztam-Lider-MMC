using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Helper;
using Aztamlider.Services.Services.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Aztamlider.Services.Services.Implementations.User
{
    public class ReferenceIndexServices : IReferenceIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReferenceIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<LanguageBase>> GetLanguageBase()
        {
            return await _unitOfWork.LanguageBaseRepository.GetAllAsync(x => !x.IsDelete);

        }
        public async Task<IEnumerable<Setting>> GetSettings()
        {
            return await _unitOfWork.SettingRepository.GetAllAsync(x => !x.IsDelete);
        }

        public IQueryable<Reference> GetReferencesCompleted(int serviceId)
        {
            var name = "";
            var reference = _unitOfWork.ReferenceRepository.asQueryable();
            reference = reference.Where(x => !x.IsDelete && x.Status);
            if (serviceId != 0)
                reference = reference.Where(x => x.ServiceNameId == serviceId);
            else
            {
                reference = reference.Where(x => !x.IsDelete);
            }
            if (name != null)
                reference = reference.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));
            return reference;
        }
        public IQueryable<Reference> GetReferencesOthers(int serviceId)
        {
            var name = "";
            var reference = _unitOfWork.ReferenceRepository.asQueryable();
            reference = reference.Where(x => !x.IsDelete && !x.Status);

            if (serviceId != 0)
                reference = reference.Where(x => x.ServiceNameId == serviceId);
            else
            {
                reference = reference.Where(x => !x.IsDelete);
            }
            if (name != null)
                reference = reference.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));
            return reference;
        }
        public async Task<IEnumerable<ReferenceImage>> GetReferenceImages()
        {
            return await _unitOfWork.ReferenceImageRepository.GetAllAsync(x => !x.IsDelete, "Reference");

        }

        public async Task<Reference> GetReference(int id)
        {
            var reference = await _unitOfWork.ReferenceRepository.GetAsync(x => !x.IsDelete && x.Id == id, "ReferenceImages");
            if (reference == null)
                throw new ItemNotFoundException("Referans tapılmadı");
            return reference;
        }

        public async Task<IEnumerable<ServiceType>> GetServiceTypes()
        {
            return await _unitOfWork.ServiceTypeRepository.GetAllAsync(x => !x.IsDelete);

        }
        public async Task<IEnumerable<Reference>> GetReferencesCompletedCount()
        {
            return await _unitOfWork.ReferenceRepository.GetAllAsync(x => !x.IsDelete && x.Status);
        }
      
        public async Task<IEnumerable<ServiceName>> GetServiceNames()
        {
            return await _unitOfWork.ServiceNameRepository.GetAllAsync(x => !x.IsDelete);

        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _unitOfWork.ServiceRepository.GetAllAsync(x => !x.IsDelete);
        }

        public async Task<IEnumerable<Reference>> GetReferencesOtherCount()
        {
            return await _unitOfWork.ReferenceRepository.GetAllAsync(x => !x.IsDelete && !x.Status);

        }
    }
}
