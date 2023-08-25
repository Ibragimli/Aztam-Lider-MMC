using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Projects
{
    public interface IAdminProjectEditServices
    {
        public Task<Project> GetProject(int id);
        public Task EditProject(Project Project);
        public Task<IEnumerable<ProjectType>> GetAllProjectTypes();


    }
}
