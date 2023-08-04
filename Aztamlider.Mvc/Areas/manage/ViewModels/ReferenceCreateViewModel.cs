using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class ReferenceCreateViewModel
    {
        public ReferenceCreateDto ReferenceCreateDto { get; set; }
        public IEnumerable<ServiceType> ServiceTypes { get; set; }

    }
}
