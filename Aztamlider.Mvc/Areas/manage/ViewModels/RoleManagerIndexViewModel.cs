using Aztamlider.Core.Entites;
using Aztamlider.Services.Helper;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class RoleManagerIndexViewModel
    {
        public PagenetedList<IdentityRole> RoleManagers { get; set; }
    }
}
