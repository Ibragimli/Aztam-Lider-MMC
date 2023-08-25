using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.Services.Interfaces.Area.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Implementations.Area.Projects
{
    public class AdminProjectIndexServices : IAdminProjectIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminProjectIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Project> GetProject(string name)
        {
            var poster = _unitOfWork.ProjectRepository.asQueryable("ProjectType");
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.TitleAz, $"%{name}%"));

            return poster;
        }
    }
}
