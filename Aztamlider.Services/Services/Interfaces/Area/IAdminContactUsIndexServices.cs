using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;

namespace Aztamlider.Services.Services.Interfaces.Area
{
    public interface IAdminContactUsIndexServices
    {
        public IQueryable<ContactUs> GetPoster(string name);
    }
}
