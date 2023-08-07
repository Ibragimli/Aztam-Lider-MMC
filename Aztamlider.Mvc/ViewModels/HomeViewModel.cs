using Aztamlider.Core.Entites;

namespace Aztamlider.Mvc.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<MainSlider> MainSliders { get; set; }
        public IEnumerable<Partner> Partners { get; set; }
        public IEnumerable<LanguageBase> LanguageBases { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
    }
}
