using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;
using System.Reflection.Metadata;
using Team = Aztamlider.Core.Entites.Team;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class TeamIndexViewModel
    {
        public PagenetedList<Team> Teams { get; set; }
    }
}
