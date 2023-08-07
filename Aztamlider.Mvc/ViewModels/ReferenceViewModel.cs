using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;

namespace Aztamlider.Mvc.ViewModels
{
    public class ReferenceViewModel
    {
        public PagenetedList<Reference> References { get; set; }
        public Reference Reference { get; set; }
        public IEnumerable<ReferenceImage> ReferenceImage { get; set; }
        public IEnumerable<LanguageBase> LanguageBases { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
    }
}
