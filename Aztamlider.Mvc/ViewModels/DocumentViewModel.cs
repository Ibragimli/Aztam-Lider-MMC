using Aztamlider.Core.Entites;

namespace Aztamlider.Mvc.ViewModels
{
    public class DocumentViewModel
    {
        public IEnumerable<LanguageBase> LanguageBases { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Document> Documents { get; set; }
    }
}
