using Aztamlider.Core.Entites;

namespace Aztamlider.Mvc.ViewModels
{
    public class ProjectViewModel
    {
        public IEnumerable<LanguageBase> LanguageBases { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
        public IEnumerable<Project> MepDesignProjects { get; set; }
        public IEnumerable<Project> ConstructionProjects { get; set; }
    }
}
