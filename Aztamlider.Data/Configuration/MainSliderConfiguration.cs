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
    public class MainSliderConfiguration : IEntityTypeConfiguration<MainSlider>
    {
        public void Configure(EntityTypeBuilder<MainSlider> builder)
        {
            builder.Property(x => x.DescriptionAz).HasMaxLength(200).IsRequired();
            builder.Property(x => x.DescriptionEn).HasMaxLength(200).IsRequired();
            builder.Property(x => x.TitleEn).HasMaxLength(100).IsRequired();
            builder.Property(x => x.TitleAz).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Image).IsRequired();
        }
    }
}
