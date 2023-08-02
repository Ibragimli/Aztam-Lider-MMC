using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class EditViewModel
    {
        public PagenetedList<Document> Document { get; set; }
    }
}
