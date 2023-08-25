using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.ProjectTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.ProjectTypes
{
    public class AdminProjectTypeIndexServices : IAdminProjectTypeIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminProjectTypeIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<ProjectType> GetProjectType(string name)
        {
            var poster = _unitOfWork.ProjectTypeRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.NameAz, $"%{name}%"));

            return poster;
        }
    }
}
