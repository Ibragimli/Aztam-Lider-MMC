using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Core.Entites
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }

    }
}
