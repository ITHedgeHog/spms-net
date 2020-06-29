using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL.Configuration
{
    public class EpisodeStatusConfiguration : IEntityTypeConfiguration<EpisodeStatus>
    {
        public void Configure(EntityTypeBuilder<EpisodeStatus> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");

            builder.Property(x => x.Name).HasMaxLength(255);
        }
    }
}