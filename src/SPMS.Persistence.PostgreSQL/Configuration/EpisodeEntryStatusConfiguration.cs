using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL.Configuration
{
    public class EpisodeEntryStatusConfiguration : IEntityTypeConfiguration<EpisodeEntryStatus>
    {
        public void Configure(EntityTypeBuilder<EpisodeEntryStatus> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");

            builder.Property(x => x.Name).HasMaxLength(150);
        }
    }
}