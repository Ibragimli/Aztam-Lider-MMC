using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class ReferenceEditViewModel
    {
        public Reference Reference { get; set; }
        public IEnumerable<ReferenceImage> ReferenceImages { get; set; }
        public ICollection<ServiceType> ServiceTypes { get; set; }

    }
}
