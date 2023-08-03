using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Services
{
    public class AdminServiceIndexServices : IAdminServiceIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminServiceIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Service> GetService(string name)
        {
            var poster = _unitOfWork.ServiceRepository.asQueryable("ServiceImages");
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.TitleAz, $"%{name}%"));

            return poster;
        }
    }
}
