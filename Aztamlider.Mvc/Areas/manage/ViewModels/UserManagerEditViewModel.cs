using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class UserManagerEditViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public UserManagerEditDto UserManagerEditDto { get; set; }
        public string RoleName { get; set; }
    }
}
