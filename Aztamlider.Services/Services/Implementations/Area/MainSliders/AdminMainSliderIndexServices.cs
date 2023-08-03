using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.MainSliders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.MainSliders
{
    public class AdminProjectIndexServices : IAdminMainSliderIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminProjectIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<MainSlider> GetMainSlider(string name)
        {
            var poster = _unitOfWork.MainSliderRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.TitleAz, $"%{name}%"));

            return poster;
        }
    }
}
