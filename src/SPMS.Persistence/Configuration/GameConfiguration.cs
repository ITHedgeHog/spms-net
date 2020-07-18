using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");

            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.SiteTitle).IsRequired();
            builder.Property(x => x.Disclaimer).IsRequired();
            builder.Property(x => x.GameKey).IsRequired(false);
            builder.Property(x => x.IsTest).HasDefaultValue(false);
            builder.Property(x => x.Uuid).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.IsSpiderable).HasDefaultValue(true);
            builder.Property(x => x.Theme).IsRequired().HasDefaultValue("Default");

        }
    }
}