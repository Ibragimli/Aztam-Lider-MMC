using Aztamlider.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Services.Services.Interfaces.Area.Teams
{
    public interface IAdminTeamEditServices
    {
        public Task<Team> GetTeam(int id);
        public Task EditTeam(Team Team);

    }
}
