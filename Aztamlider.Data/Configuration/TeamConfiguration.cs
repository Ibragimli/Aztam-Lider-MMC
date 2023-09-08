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
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(x => x.PositionAz).HasMaxLength(150).IsRequired();
            builder.Property(x => x.PositionEn).HasMaxLength(150).IsRequired();
            builder.Property(x => x.DescriptionAz).HasMaxLength(450).IsRequired();
            builder.Property(x => x.DescriptionEn).HasMaxLength(450).IsRequired();
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

        }
    }
}
