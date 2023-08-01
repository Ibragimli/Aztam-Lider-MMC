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
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(x => x.TitleEn).HasMaxLength(150).IsRequired();
            builder.Property(x => x.TitleAz).HasMaxLength(150).IsRequired();
            builder.Property(x => x.DescriptionAz).HasMaxLength(3500).IsRequired();
            builder.Property(x => x.DescriptionEn).HasMaxLength(3500).IsRequired();

        }
    }
}
