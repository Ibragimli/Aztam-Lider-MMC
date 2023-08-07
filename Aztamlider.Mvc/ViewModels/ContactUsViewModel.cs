using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.User;

namespace Aztamlider.Mvc.ViewModels
{
    public class ContactUsViewModel
    {
        public ContactUsCreateDto ContactUsCreateDto { get; set; }
        public IEnumerable<LanguageBase> LanguageBases { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
    }
}
