using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ServiceTypes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ServiceTypes
{
    public class AdminServiceTypeEditServices : IAdminServiceTypeEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminServiceTypeEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditServiceType(ServiceType ServiceType)
        {
            bool checkBool = false;

            var oldServiceType = await GetServiceType(ServiceType.Id);
            if (oldServiceType == null)
                throw new ItemNullException("Xidmət tapılmadı!");

            await Check(ServiceType);

            if (oldServiceType.NameAz != ServiceType.NameAz)
            {
                oldServiceType.NameAz = ServiceType.NameAz;
                checkBool = true;
            }
            if (oldServiceType.NameEn != ServiceType.NameEn)
            {
                oldServiceType.NameEn = ServiceType.NameEn;
                checkBool = true;
            }

            oldServiceType.ModifiedDate = DateTime.UtcNow.AddHours(4);

            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<ServiceType> GetServiceType(int id)
        {
            var ServiceType = await _unitOfWork.ServiceTypeRepository.GetAsync(x => x.Id == id);
            return ServiceType;
        }


        private async Task Check(ServiceType ServiceType)
        {

            if (ServiceType.NameAz == null || ServiceType.NameEn == null)
            {
                throw new ItemNullException("Xidmət adını qeyd edin!");
            }
            if (ServiceType.NameAz?.Length < 3 || ServiceType.NameEn?.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu minimum 3 ola bilər");
            }
            if (ServiceType.NameAz?.Length > 150 || ServiceType.NameEn?.Length > 150)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu maksimum 150 ola bilər");
            }

        }
    }
}
