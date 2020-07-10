using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL.Configuration
{
    public class EpisodeEntryConfiguration : IEntityTypeConfiguration<EpisodeEntry>
    {
        public void Configure(EntityTypeBuilder<EpisodeEntry> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");

            builder.Property(x => x.Title).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.Location).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.Timeline).IsRequired(false).HasMaxLength(255);

            builder.Property(x => x.Content).IsRequired(false);
        }
    }
}