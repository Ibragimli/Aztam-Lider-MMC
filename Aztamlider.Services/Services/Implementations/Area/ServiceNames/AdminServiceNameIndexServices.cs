using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.ServiceNames;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ServiceNames
{
    public class AdminServiceNameIndexServices : IAdminServiceNameIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminServiceNameIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<ServiceName> GetServiceName(string name)
        {
            var poster = _unitOfWork.ServiceNameRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.NameAz, $"%{name}%"));

            return poster;
        }
    }
}
