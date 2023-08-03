using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Projects
{
    public interface IAdminProjectDeleteServices
    {
        public Task DeleteProject(int id);
    }
}
