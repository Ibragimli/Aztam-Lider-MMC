using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Core.Repositories;
using Aztamlider.Data.Repositories;
using Aztamlider.Data.UnitOfWork;
using Aztamlider.Services.HelperService.Implementations;
using Aztamlider.Services.HelperService.Interfaces;
using Aztamlider.Services.Profiles;
using Aztamlider.Services.Services.Implementations;
using Aztamlider.Services.Services.Implementations.Area;
using Aztamlider.Services.Services.Implementations.Area.Career;
using Aztamlider.Services.Services.Implementations.Area.ContactUs;
using Aztamlider.Services.Services.Implementations.Area.Dashboard;
using Aztamlider.Services.Services.Implementations.Area.Documents;
using Aztamlider.Services.Services.Implementations.Area.ImageSettings;
using Aztamlider.Services.Services.Implementations.Area.LanguageBases;
using Aztamlider.Services.Services.Implementations.Area.Loggers;
using Aztamlider.Services.Services.Implementations.Area.Login;
using Aztamlider.Services.Services.Implementations.Area.MainSliders;
using Aztamlider.Services.Services.Implementations.Area.Partners;
using Aztamlider.Services.Services.Implementations.Area.Projects;
using Aztamlider.Services.Services.Implementations.Area.ProjectTypes;
using Aztamlider.Services.Services.Implementations.Area.References;
using Aztamlider.Services.Services.Implementations.Area.RoleManagers;
using Aztamlider.Services.Services.Implementations.Area.ServiceNames;
using Aztamlider.Services.Services.Implementations.Area.Services;
using Aztamlider.Services.Services.Implementations.Area.ServiceTypes;
using Aztamlider.Services.Services.Implementations.Area.Settings;
using Aztamlider.Services.Services.Implementations.Area.Teams;
using Aztamlider.Services.Services.Implementations.Area.UserManagers;
using Aztamlider.Services.Services.Implementations.User;
using Aztamlider.Services.Services.Interfaces;
using Aztamlider.Services.Services.Interfaces.Area.Careers;
using Aztamlider.Services.Services.Interfaces.Area.ContactUs;
using Aztamlider.Services.Services.Interfaces.Area.Dashboard;
using Aztamlider.Services.Services.Interfaces.Area.Documents;
using Aztamlider.Services.Services.Interfaces.Area.EmailSettings;
using Aztamlider.Services.Services.Interfaces.Area.ImageSettings;
using Aztamlider.Services.Services.Interfaces.Area.LanguageBases;
using Aztamlider.Services.Services.Interfaces.Area.Loggers;
using Aztamlider.Services.Services.Interfaces.Area.Login;
using Aztamlider.Services.Services.Interfaces.Area.MainSliders;
using Aztamlider.Services.Services.Interfaces.Area.Partners;
using Aztamlider.Services.Services.Interfaces.Area.Projects;
using Aztamlider.Services.Services.Interfaces.Area.ProjectTypes;
using Aztamlider.Services.Services.Interfaces.Area.References;
using Aztamlider.Services.Services.Interfaces.Area.RoleManagers;
using Aztamlider.Services.Services.Interfaces.Area.ServiceNames;
using Aztamlider.Services.Services.Interfaces.Area.Services;
using Aztamlider.Services.Services.Interfaces.Area.ServiceTypes;
using Aztamlider.Services.Services.Interfaces.Area.Settings;
using Aztamlider.Services.Services.Interfaces.Area.Teams;
using Aztamlider.Services.Services.Interfaces.Area.UserManagers;
using Aztamlider.Services.Services.Interfaces.User;
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


            services.AddScoped<IEmailSettingEditServices, EmailSettingEditServices>();
            services.AddScoped<IEmailSettingIndexServices, EmailSettingIndexServices>();

            services.AddScoped<IImageSettingIndexServices, ImageSettingIndexServices>();
            services.AddScoped<IImageSettingEditServices, ImageSettingEditServices>();

            services.AddScoped<ISettingEditServices, SettingEditServices>();
            services.AddScoped<ISettingIndexServices, SettingIndexServices>();

            services.AddScoped<IContactUsCreateServices, ContactUsCreateServices>();

            services.AddScoped<IHomeIndexServices, HomeIndexServices>();
            services.AddScoped<IEmailServices, EmailServices>();

            services.AddScoped<IAdminContactUsIndexServices, AdminContactUsIndexServices>();

            services.AddScoped<IAdminServiceTypeIndexServices, AdminServiceTypeIndexServices>();
            services.AddScoped<IAdminServiceTypeEditServices, AdminServiceTypeEditServices>();
            services.AddScoped<IAdminServiceTypeCreateServices, AdminServiceTypeCreateServices>();
            services.AddScoped<IAdminServiceTypeDeleteServices, AdminServiceTypeDeleteServices>();

            services.AddScoped<IAdminDocumentIndexServices, AdminDocumentIndexServices>();
            services.AddScoped<IAdminDocumentEditServices, AdminDocumentEditServices>();
            services.AddScoped<IAdminDocumentCreateServices, AdminDocumentCreateServices>();
            services.AddScoped<IAdminDocumentDeleteServices, AdminDocumentDeleteServices>();

            services.AddScoped<IAdminMainSliderIndexServices, AdminMainSliderIndexServices>();
            services.AddScoped<IAdminMainSliderEditServices, AdminMainSliderEditServices>();
            services.AddScoped<IAdminMainSliderCreateServices, AdminMainSliderCreateServices>();
            services.AddScoped<IAdminMainSliderDeleteServices, AdminMainSliderDeleteServices>();

            services.AddScoped<IAdminPartnerIndexServices, AdminPartnerIndexServices>();
            services.AddScoped<IAdminPartnerEditServices, AdminPartnerEditServices>();
            services.AddScoped<IAdminPartnerCreateServices, AdminPartnerCreateServices>();
            services.AddScoped<IAdminPartnerDeleteServices, AdminPartnerDeleteServices>();

            services.AddScoped<IAdminProjectIndexServices, AdminProjectIndexServices>();
            services.AddScoped<IAdminProjectEditServices, AdminProjectEditServices>();
            services.AddScoped<IAdminProjectCreateServices, AdminProjectCreateServices>();
            services.AddScoped<IAdminProjectDeleteServices, AdminProjectDeleteServices>();

            services.AddScoped<IAdminReferenceIndexServices, AdminReferenceIndexServices>();
            services.AddScoped<IAdminReferenceEditServices, AdminReferenceEditServices>();
            services.AddScoped<IAdminReferenceCreateServices, AdminReferenceCreateServices>();
            services.AddScoped<IAdminReferenceDeleteServices, AdminReferenceDeleteServices>();

            services.AddScoped<IAdminServiceIndexServices, AdminServiceIndexServices>();
            services.AddScoped<IAdminServiceEditServices, AdminServiceEditServices>();
            services.AddScoped<IAdminServiceCreateServices, AdminServiceCreateServices>();
            services.AddScoped<IAdminServiceDeleteServices, AdminServiceDeleteServices>();

            services.AddScoped<ILanguageBaseRepository, LanguageBaseRepository>();
            services.AddScoped<ILanguageBaseIndexServices, LanguageBaseIndexServices>();
            services.AddScoped<ILanguageBaseCreateServices, LanguageBaseCreateServices>();
            services.AddScoped<ILanguageBaseEditServices, LanguageBaseEditServices>();

            services.AddScoped<ISettingIndexServices, SettingIndexServices>();
            services.AddScoped<IAdminServiceEditServices, AdminServiceEditServices>();

            services.AddScoped<IAdminLoginServices, AdminLoginServices>();

            services.AddScoped<IServiceIndexServices, ServiceIndexServices>();
            services.AddScoped<IReferenceIndexServices, ReferenceIndexServices>();
            services.AddScoped<IAboutUsIndexServices, AboutUsIndexServices>();
            services.AddScoped<IDocumentServices, DocumentServices>();

            services.AddScoped<IContactUsIndexServices, ContactUsIndexServices>();
            services.AddScoped<ICareerIndexServices, CareerIndexServices>();
            services.AddScoped<IProjectIndexServices, ProjectIndexServices>();

            services.AddScoped<IServiceNameRepository, ServiceNameRepository>();
            services.AddScoped<IAdminServiceNameCreateServices, AdminServiceNameCreateServices>();
            services.AddScoped<IAdminServiceNameDeleteServices, AdminServiceNameDeleteServices>();
            services.AddScoped<IAdminServiceNameEditServices, AdminServiceNameEditServices>();
            services.AddScoped<IAdminServiceNameIndexServices, AdminServiceNameIndexServices>();

            services.AddScoped<IAdminRoleManagerCreateServices, AdminRoleManagerCreateServices>();
            services.AddScoped<IAdminRoleManagerDeleteServices, AdminRoleManagerDeleteServices>();
            services.AddScoped<IAdminRoleManagerEditServices, AdminRoleManagerEditServices>();
            services.AddScoped<IAdminRoleManagerIndexServices, AdminRoleManagerIndexServices>();


            services.AddScoped<IAdminUserManagerCreateServices, AdminUserManagerCreateServices>();
            services.AddScoped<IAdminUserManagerDeleteServices, AdminUserManagerDeleteServices>();
            services.AddScoped<IAdminUserManagerEditServices, AdminUserManagerEditServices>();
            services.AddScoped<IAdminUserManagerIndexServices, AdminUserManagerIndexServices>();

            services.AddScoped<IAdminTeamCreateServices, AdminTeamCreateServices>();
            services.AddScoped<IAdminTeamDeleteServices, AdminTeamDeleteServices>();
            services.AddScoped<IAdminTeamEditServices, AdminTeamEditServices>();
            services.AddScoped<IAdminTeamIndexServices, AdminTeamIndexServices>();
            services.AddScoped<ITeamServices, TeamServices>();

            services.AddScoped<IAdminCareerIndexServices, AdminCareerIndexServices>();
            services.AddScoped<IDashboardServices, DashboardServices>();


            services.AddScoped<IAdminLoggerIndexServices, AdminLoggerIndexServices>();
            services.AddScoped<ILoggerServices, LoggerServices>();

            services.AddScoped<ISettingCreateServices, SettingCreateServices>();


            services.AddScoped<IAdminProjectTypeCreateServices, AdminProjectTypeCreateServices>();
            services.AddScoped<IAdminProjectTypeDeleteServices, AdminProjectTypeDeleteServices>();
            services.AddScoped<IAdminProjectTypeEditServices, AdminProjectTypeEditServices>();
            services.AddScoped<IAdminProjectTypeIndexServices, AdminProjectTypeIndexServices>();

            services.AddScoped<ILayoutServices, LayoutServices>();
            services.AddScoped<ICareerServices, CareerServices>();
        }
    }
}
