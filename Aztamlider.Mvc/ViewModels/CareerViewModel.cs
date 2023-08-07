using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.User;

namespace Aztamlider.Mvc.ViewModels
{
    public class CareerViewModel
    {
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<LanguageBase> LanguageBases { get; set; }
        public CareerPostDto CareerPostDto { get; set; }
    }
}
