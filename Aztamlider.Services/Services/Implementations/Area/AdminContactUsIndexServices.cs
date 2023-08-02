using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Services.Interfaces.Area;

namespace Aztamlider.Services.Services.Implementations.Area
{
    public class AdminContactUsIndexServices : IAdminContactUsIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminContactUsIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<ContactUs> GetPoster(string name)
        {
            var poster = _unitOfWork.ContactUsRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.Fullname, $"%{name}%"));


            return poster;
        }
    }
}
