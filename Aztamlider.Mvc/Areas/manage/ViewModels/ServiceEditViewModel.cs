using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class ServiceEditViewModel
    {
        public Service Service { get; set; }
        public ICollection<ServiceImage> ServiceImages { get; set; }

    }
}
