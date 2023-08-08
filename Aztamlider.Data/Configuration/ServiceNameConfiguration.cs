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
    public class ServiceNameConfiguration : IEntityTypeConfiguration<ServiceName>
    {
        public void Configure(EntityTypeBuilder<ServiceName> builder)
        {
            builder.Property(x => x.NameAz).HasMaxLength(150).IsRequired();
            builder.Property(x => x.NameEn).HasMaxLength(150).IsRequired();

        }
    }
}
