using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.Configuration
{
    public class BiographyConfiguration : IEntityTypeConfiguration<Biography>
    {
        public void Configure(EntityTypeBuilder<Biography> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");
        }
    }
}
