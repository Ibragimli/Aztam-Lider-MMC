using Aztamlider.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aztamlider.Data.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.TitleAz).HasMaxLength(120).IsRequired();
            builder.Property(x => x.TitleEn).HasMaxLength(120).IsRequired();
            builder.Property(x => x.DescriptionAz).HasMaxLength(3000).IsRequired();
            builder.Property(x => x.DescriptionEn).HasMaxLength(3000).IsRequired();

        }
    }
}
