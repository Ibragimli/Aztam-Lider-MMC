using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class ProjectCreateViewModel
    {
        public ProjectCreateDto ProjectCreateDto { get; set; }
        public IEnumerable<ProjectType> ProjectTypes { get; set; }

    }
}
