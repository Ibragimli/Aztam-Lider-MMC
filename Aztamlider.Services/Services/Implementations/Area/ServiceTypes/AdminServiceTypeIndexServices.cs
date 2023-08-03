using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.ServiceTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ServiceTypes
{
    public class AdminServiceTypeIndexServices : IAdminServiceTypeIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminServiceTypeIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<ServiceType> GetServiceType(string name)
        {
            var poster = _unitOfWork.ServiceTypeRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.NameAz, $"%{name}%"));

            return poster;
        }
    }
}
