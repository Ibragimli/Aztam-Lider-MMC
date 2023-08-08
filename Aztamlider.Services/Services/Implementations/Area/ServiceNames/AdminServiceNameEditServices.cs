using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.ServiceNames;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ServiceNames
{
    public class AdminServiceNameEditServices : IAdminServiceNameEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminServiceNameEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditServiceName(ServiceName ServiceName)
        {
            bool checkBool = false;

            var oldServiceName = await GetServiceName(ServiceName.Id);
            if (oldServiceName == null)
                throw new ItemNullException("Xidmət tapılmadı!");

            await Check(ServiceName);

            if (oldServiceName.NameAz != ServiceName.NameAz)
            {
                oldServiceName.NameAz = ServiceName.NameAz;
                checkBool = true;
            }
            if (oldServiceName.NameEn != ServiceName.NameEn)
            {
                oldServiceName.NameEn = ServiceName.NameEn;
                checkBool = true;
            }

            oldServiceName.ModifiedDate = DateTime.UtcNow.AddHours(4);

            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<ServiceName> GetServiceName(int id)
        {
            var ServiceName = await _unitOfWork.ServiceNameRepository.GetAsync(x => x.Id == id);
            return ServiceName;
        }


        private async Task Check(ServiceName ServiceName)
        {

            if (ServiceName.NameAz == null || ServiceName.NameEn == null)
            {
                throw new ItemNullException("Xidmət adını qeyd edin!");
            }
            if (ServiceName.NameAz?.Length < 3 || ServiceName.NameEn?.Length < 3)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu minimum 3 ola bilər");
            }
            if (ServiceName.NameAz?.Length > 150 || ServiceName.NameEn?.Length > 150)
            {
                throw new ValueFormatExpception("Xidmət adının uzunluğu maksimum 150 ola bilər");
            }

        }
    }
}
