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
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(x => x.NameAz).HasMaxLength(250).IsRequired(true);
            builder.Property(x => x.NameEn).HasMaxLength(250).IsRequired(true);
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.PDF).IsRequired();

        }
    }
}
