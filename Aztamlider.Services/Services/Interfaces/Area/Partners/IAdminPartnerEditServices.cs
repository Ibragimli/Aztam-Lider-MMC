using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Partners
{
    public interface IAdminPartnerEditServices
    {
        public Task<Partner> GetPartner(int id);
        public Task EditPartner(Partner Partner);

    }
}
