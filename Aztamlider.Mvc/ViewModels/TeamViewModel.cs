using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.User;

namespace Aztamlider.Mvc.ViewModels
{
    public class TeamViewModel
    {
        public IEnumerable<LanguageBase> LanguageBases { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public CareerPostDto CareerPostDto { get; set; }

    }
}
