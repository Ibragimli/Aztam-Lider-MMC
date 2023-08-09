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
    public class ReferenceConfiguration : IEntityTypeConfiguration<Reference>
    {
        public void Configure(EntityTypeBuilder<Reference> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Orderer).HasMaxLength(100).IsRequired();
            builder.Property(x => x.SquareMetr).IsRequired();
            builder.Property(x => x.Date).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LocationAz).HasMaxLength(120).IsRequired();
            builder.Property(x => x.LocationEn).HasMaxLength(120).IsRequired();
            builder.Property(x => x.BuildingTypeEn).HasMaxLength(150).IsRequired();
            builder.Property(x => x.BuildingTypeAz).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.ServiceTypeId).IsRequired();

        }
    }
}
