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
    public class CareerConfiguration : IEntityTypeConfiguration<Career>
    {
        public void Configure(EntityTypeBuilder<Career> builder)
        {
            builder.Property(x => x.Message).IsRequired(false);
            builder.Property(x => x.Fullname).IsRequired(false);
            builder.Property(x => x.PhoneNumber).IsRequired(false);

        }
    }
}
