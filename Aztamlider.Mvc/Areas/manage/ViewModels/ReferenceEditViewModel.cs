using Aztamlider.Core.Entites;
using Aztamlider.Data.Migrations;
using Aztamlider.Services.Helper;
using Microsoft.AspNetCore.Hosting.Server;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class ReferenceEditViewModel
    {
        public Reference Reference { get; set; }
        public IEnumerable<ReferenceImage> ReferenceImages { get; set; }
        public IEnumerable<ServiceType> ServiceTypes { get; set; }
        public IEnumerable<ServiceName> ServiceNames { get; set; }

    }
}
