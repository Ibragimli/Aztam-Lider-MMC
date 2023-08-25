using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class DashboardViewModel
    {
        public int ContactUsCount { get; set; }
        public int CareerCount { get; set; }
        public List<int> CareerMonthCount { get; set; }
        public List<int> ContactMonthCount { get; set; }
    }
}
