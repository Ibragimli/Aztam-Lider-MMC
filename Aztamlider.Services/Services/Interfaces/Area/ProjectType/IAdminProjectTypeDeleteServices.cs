using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.ProjectTypes
{
    public interface IAdminProjectTypeDeleteServices
    {
        public Task DeleteProjectType(int id);
    }
}
