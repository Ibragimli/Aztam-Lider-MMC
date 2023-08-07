using Aztamlider.Core.Entites;

namespace Aztamlider.Mvc.ViewModels
{
    public class AboutUsViewModel
    {
        public IEnumerable<LanguageBase> LanguageBases { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}
