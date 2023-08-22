using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Services.Interfaces.Area.Careers;

namespace Aztamlider.Services.Services.Implementations.Area.Career
{
    public class AdminCareerIndexServices : IAdminCareerIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminCareerIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Aztamlider.Core.Entites.Career> GetPoster(string name)
        {
            var poster = _unitOfWork.CareerRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.Fullname, $"%{name}%"));


            return poster;
        }
    }
}
