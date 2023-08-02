using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Core.Repositories;
using Aztamlider.Data.Repositories;
using Aztamlider.Data.UnitOfWork;
using Aztamlider.Services.HelperService.Implementations;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Profiles;
using MailKit;

namespace Aztamlider.Mvc.ServiceExtentions
{
    public static class ServiceScopeExtention
    {
        public static void AddServiceScopeExtention(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IImageSettingRepository, ImageSettingRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IManageImageHelper, ManageImageHelper>();
            services.AddScoped<IImageValue, ImageValue>();
            services.AddAutoMapper(opt => { opt.AddProfile(new AppProfile()); });
            services.AddScoped<IContactUsRepository, ContactUsRepository>();


            //services.AddScoped<IEmailSettingEditServices, EmailSettingEditServices>();
            //services.AddScoped<IEmailSettingIndexServices, EmailSettingIndexServices>();

            //services.AddScoped<IImageSettingIndexServices, ImageSettingIndexServices>();
            //services.AddScoped<IImageSettingEditServices, ImageSettingEditServices>();

            //services.AddScoped<IContactUsCreateServices, ContactUsCreateServices>();

            //services.AddScoped<IHomeIndexServices, HomeIndexServices>();

            //services.AddScoped<IAdminContactUsIndexServices, AdminContactUsIndexServices>();


            //services.AddScoped<IAdminLoginServices, AdminLoginServices>();

            //services.AddScoped<ILayoutServices, LayoutServices>();
            //services.AddScoped<ICareerServices, CareerServices>();
        }
    }
}
