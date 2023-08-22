using Aztamlider.Core.Entites;
using Aztamlider.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Data.Datacontext
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ImageSetting> ImageSettings { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<MainSlider> MainSliders { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<ReferenceImage> ReferenceImages { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<LanguageBase> LanguageBases { get; set; }
        public DbSet<Logger> Loggers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Career> Careers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(SettingConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
