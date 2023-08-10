using Aztamlider.Core.Entites;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Helper;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;
using Document = Aztamlider.Core.Entites.Document;

namespace Aztamlider.Mvc.Areas.manage.ViewModels
{
    public class UserManagerCreateViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public UserManagerCreateDto UserManagerCreateDto { get; set; }
    }
}
