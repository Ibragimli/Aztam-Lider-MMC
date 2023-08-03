using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.Partners;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Partners
{
    public class AdminMainSliderIndexServices : IAdminPartnerIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminMainSliderIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Partner> GetPartner(string name)
        {
            var poster = _unitOfWork.PartnerRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.Image, $"%{name}%"));

            return poster;
        }
    }
}
