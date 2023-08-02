using Aztamlider.Core.Entites;
using Aztamlider.Data.Datacontext;
using Microsoft.AspNetCore.Identity;

namespace Aztamlider.Mvc.ServiceExtentions
{
    public static class IdentityServiceExtention
    {
        public static void AddIdentityServiceExtention(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = false;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<DataContext>();

        }
    }
}
