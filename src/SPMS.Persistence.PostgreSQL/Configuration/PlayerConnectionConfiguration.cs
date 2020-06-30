using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL.Configuration
{
    public class PlayerConnectionConfiguration : IEntityTypeConfiguration<PlayerConnection>
    {
        public void Configure(EntityTypeBuilder<PlayerConnection> builder)
        {
            builder.HasKey(e => e.ConnectionId);

        }
    }

    public class SeriesConfiguration : IEntityTypeConfiguration<Series>
    {
        public void Configure(EntityTypeBuilder<Series> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(x => x.Title).HasMaxLength(200);

        }
    }
}