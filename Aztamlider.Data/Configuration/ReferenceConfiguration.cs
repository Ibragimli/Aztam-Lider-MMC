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
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired(true);
            builder.Property(x => x.SquareMetr).IsRequired();
            builder.Property(x => x.Date).HasMaxLength(40).IsRequired();
            builder.Property(x => x.Location).HasMaxLength(120).IsRequired();
            builder.Property(x => x.BuildingType).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.ServiceTypeId).IsRequired();

        }
    }
}
