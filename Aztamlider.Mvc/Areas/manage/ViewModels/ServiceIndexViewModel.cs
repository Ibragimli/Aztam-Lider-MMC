using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class ServiceIndexViewModel
    {
        public PagenetedList<Service> Services { get; set; }
    }
}
