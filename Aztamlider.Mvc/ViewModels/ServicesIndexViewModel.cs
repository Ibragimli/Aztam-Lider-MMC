using Aztamlider.Core.Entites;

namespace Aztamlider.Mvc.ViewModels
{
    public class ServicesIndexViewModel
    {
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<ServiceImage> ServiceImages { get; set; }
        public IEnumerable<LanguageBase> LanguageBases { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
    }
}
