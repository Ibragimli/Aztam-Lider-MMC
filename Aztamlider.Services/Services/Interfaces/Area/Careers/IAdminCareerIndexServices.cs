using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;

namespace Aztamlider.Services.Services.Interfaces.Area.Careers
{
    public interface IAdminCareerIndexServices
    {
        public IQueryable<Aztamlider.Core.Entites.Career> GetPoster(string name);
    }
}
